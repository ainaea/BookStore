using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Presentation.DTOs;
using BookStore.Presentation.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Presentation.Controllers
{
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<IdentityUser> userManager;

        public CartController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetCart([FromQuery] Guid userId)
        {
            //returns the pending orders in cart for a user
            var cart = GetCurrentCart(userId);
            if (cart == null)
                return Ok(MapToCartDTO(new Cart(userId)));
            return Ok(MapToCartDTO(cart));
        }

        private Cart? GetCurrentCart(Guid userId)
        {
            return unitOfWork.Carts.GetAll(c => c.UserId == userId)?.FirstOrDefault(c => c.PaymentStatus == PaymentEnum.Pending);
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] CartedBookDTO dto)
        {
            //to add a particular item/book to cart
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //var cart = GetCurrentCart(userId)/* ?? new Cart(userId)*/;      //see if user has a pending cart
            CartedBook? thisItemInCart = LocateInCart(dto);
            if (thisItemInCart == null)
            {
                thisItemInCart!.Quantity += dto.Quantity;
            }
            unitOfWork.CartedBooks.Update(thisItemInCart);
            return Ok();
        }

        private CartedBook? LocateInCart(CartedBookDTO dto)
        {
            return unitOfWork.CartedBooks.GetAll(cb => cb.CartId == dto.CartId && cb.BookId == dto.BookId)?.FirstOrDefault();
            //book has been previously carted
        }

        [HttpPost]
        public IActionResult DeleteFromCart([FromBody] CartedBookDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            CartedBook? thisItemInCart = LocateInCart(dto);
            if (thisItemInCart == null)
            {
                thisItemInCart!.Quantity += dto.Quantity;
            }
            unitOfWork.CartedBooks.Remove(thisItemInCart);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            if (currentUser == null)
                return Forbid();
            var cart = unitOfWork.Carts.GetAll(c => c.UserId.ToString() == currentUser!.Id && c.PaymentStatus == PaymentEnum.Pending)?.FirstOrDefault();
            if (cart == null)
                return NotFound("No item found in cart");

            bool paymentResponse = true; //to be replaced by a response from payment service

            if(paymentResponse)
            {
                cart.PaymentStatus = PaymentEnum.Successful;
                unitOfWork.Carts.Update(cart);
                return Ok();
            }
            return BadRequest("Transaction failed due to invalid data");

        }

        public static Cart MapToCart(CartDTO dto,Guid userId, Cart? model)
        {
            return new Cart(model == null ? userId : model.Id)
            {
                Id = model == null ? new Guid() : model.Id,
                PaymentStatus = model == null ? PaymentEnum.Pending : dto.PaymentStatus
            };
        }
        public static CartDTO MapToCartDTO(Cart model)
        {
            return new CartDTO
            {
                Id = model.Id,
                UserId = model.UserId,
                PaymentStatus = model.PaymentStatus
            };
        }
    }    
}
