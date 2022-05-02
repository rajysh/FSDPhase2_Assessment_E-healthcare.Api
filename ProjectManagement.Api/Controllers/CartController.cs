using EHealthcare.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Cart")]
    public class CartController : BaseController<Cart>
    {
        private readonly IBaseRepository<CartItem> CartItemRepository;
        private IBaseRepository<Product> ProductRepository { get; set; }
        private IBaseRepository<User> UserRepository { get; set; }

        public CartController(IBaseRepository<CartItem> _CartRepository, IBaseRepository<Product> _ProductRepository, IBaseRepository<User> _UserRepository)
        {
            CartItemRepository = _CartRepository;
            ProductRepository = _ProductRepository;
            UserRepository = _UserRepository;
        }

        [HttpGet("GetByUserID/{userID}")]
        public IActionResult Get([FromRoute]long userID)
        {
            Cart result = Repository.Get().Where(i => i.OwnerID == userID).FirstOrDefault();
            if (result is null)
            {
                return NoContent();
            }
            else                
            {
                result.Items = CartItemRepository.Get().Where(i => i.CartID == result.ID).ToList();

                if (result.Items is null)
                {
                    result.Items = null;
                }
                else
                {
                    foreach (var item in result.Items)
                    {
                        item.Product = ProductRepository.Get().Where(i => i.ID == item.ProductID).FirstOrDefault();
                        
                    }
                }

                result.Owner = UserRepository.Get().Where(i => i.ID == result.OwnerID).FirstOrDefault();
                if (result.Owner is null)
                {
                    result.Owner = null;
                }
            }

            return Ok(result);
        }

        [Route("PlaceOrder/{userID}")]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromRoute]long userID)
        {
            var userCart = Repository.Get().Where(i => i.OwnerID == userID).FirstOrDefault();

            if (userCart is null)
            {
                userCart = await Repository.Add(new Cart { OwnerID = userID });
            }
            //userCart.Items = CartItemRepository.Get().Where(i => i.CartID == userCart.ID).ToList();
            foreach (var item in userCart.Items)
            {
                CartItemRepository.Delete(item.ID);
            }
            return Ok();
        }

        [Route("Add/{productID}/{userID}")]
        [HttpPost]
        public async Task<IActionResult> Post(long productID, long userID)
        {
            var userCart = Repository.Get().Where(i => i.OwnerID == userID).FirstOrDefault();

            if (userCart is null)
            {
                userCart = await Repository.Add(new Cart { OwnerID = userID });
            }
            var cartItem = new CartItem { CartID = userCart.ID, ProductID = productID };
            var result = CartItemRepository.Add(cartItem);
            return Ok();
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public async override Task<IActionResult> Delete([FromRoute] long id)
        {
            await CartItemRepository.Delete(id);
            return Ok();
        }
    }
}
