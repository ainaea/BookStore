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
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICartManager cartManager;
        private readonly IPaymentService paymentService;

        public CartController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, ICartManager cartManager, IPaymentService paymentService)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.cartManager = cartManager;
            this.paymentService = paymentService;
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
        [Route($"{nameof(AddToCart)}")]
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
        [Route($"{nameof(DeleteFromCart)}")]
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
        [Route($"{nameof(Checkout)}")]
        public async Task<IActionResult> Checkout()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            if (currentUser == null)
                return Forbid();
            var cart = unitOfWork.Carts.GetAll(c => c.UserId.ToString() == currentUser!.Id && c.PaymentStatus == PaymentEnum.Pending)?.FirstOrDefault();
            if (cart == null)
                return NotFound("No item found in cart");
            var cartedBooks = unitOfWork.CartedBooks.GetAll( cb=> cb.CartId == cart.Id, new object[] { typeof(Book)});
            cart.CartedBooks = cartedBooks;     //object to be used to get total cost from cart manager

            decimal totalCost = cartManager.GetCartTotal(cart);
            if (totalCost <= decimal.Zero)
                return NotFound("No valuable found in cart");

            bool paymentResponse = await paymentService.TransferPayment(cart.Id, totalCost);//true; //to be replaced by a response from payment service

            if(paymentResponse)
            {
                cart.PaymentStatus = PaymentEnum.Successful;
                unitOfWork.Carts.Update(cart);
                return Ok();
            }
            return BadRequest("Transaction failed due to invalid data");

        }
        [HttpPost]
        [Route($"{nameof(GetCartHistory)}")]
        public async Task<IActionResult> GetCartHistory()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            if (currentUser == null)
                return Forbid();
            var cart = unitOfWork.Carts.GetAll(c => c.UserId.ToString() == currentUser!.Id && c.PaymentStatus != PaymentEnum.Pending);
            return Ok(cart.Select(c => MapToCartDTO(c)));
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
