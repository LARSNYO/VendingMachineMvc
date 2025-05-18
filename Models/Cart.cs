// using testmvcapp.Data;
// using Microsoft.AspNetCore.Http;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using System;
// using System.Collections.Generic;
// using System.Linq;

// namespace testmvcapp.Models
// {
//     public class Cart
//     {
//         private readonly TestDbContext _context;

//         public Cart(TestDbContext context)
//         {
//             _context = context;
//         }

//         public string Id { get; set; }
//         public List<CartItem> CartItems { get; set; }

//         public static Cart GetCart(IServiceProvider services)
//         {
//             ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

//             var context = services.GetService<TestDbContext>();
//             string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();

//             session.SetString("Id", cartId);

//             return new Cart(context) { Id = cartId };
//         }

//         public CartItem GetCartItem(Drink drink)
//         {
//             return _context.CartItems.SingleOrDefault(ci =>
//                 ci.Drink.Id == drink.Id && ci.CartId == Id);
//         }

//         public void AddToCart(Drink drink, int quantity)
//         {
//             var cartItem = GetCartItem(drink);

//             int totalIncart = cartItem?.Quantity ?? 0;
//             int availableStock = drink.Quantity;

//             if(totalIncart + quantity > availableStock)
//             {
//                 return;
//             }

//             if (cartItem == null)
//             {
//                 cartItem = new CartItem
//                 {
//                     Drink = drink,
//                     Quantity = quantity,
//                     CartId = Id
//                 };

//                 _context.CartItems.Add(cartItem);
//             }
//             else
//             {
//                 cartItem.Quantity += quantity;
//             }
//             _context.SaveChanges();
//         }

//         public int ReduceQuantity(Drink drink)
//         {
//             var cartItem = GetCartItem(drink);
//             var remainingQuantity = 0;

//             if (cartItem != null)
//             {
//                 if (cartItem.Quantity > 1)
//                 {
//                     remainingQuantity = --cartItem.Quantity;
//                 }
//                 else
//                 {
//                     _context.CartItems.Remove(cartItem);
//                 }
//             }
//             _context.SaveChanges();

//             return remainingQuantity;
//         }

//         public int IncreaseQuantity(Drink drink)
//         {
//             var cartItem = GetCartItem(drink);
//             var remainingQuantity = 0;

//             if (cartItem != null)
//             {
//                 if (cartItem.Quantity < drink.Quantity)
//                 {
//                     remainingQuantity = ++cartItem.Quantity;
//                     _context.SaveChanges();
//                 }
//             }

//             return remainingQuantity;
//         }

//         public void RemoveFromCart(Drink drink)
//         {
//             var cartItem = GetCartItem(drink);

//             if (cartItem != null)
//             {
//                 _context.CartItems.Remove(cartItem);
//             }
//             _context.SaveChanges();
//         }

//         public void ClearCart()
//         {
//             var cartItems = _context.CartItems.Where(ci => ci.CartId == Id);

//             _context.CartItems.RemoveRange(cartItems);

//             _context.SaveChanges();
//         }

//         public List<CartItem> GetAllCartItems()
//         {
//             return CartItems ??
//                 (CartItems = _context.CartItems.Where(ci => ci.CartId == Id)
//                     .Include(ci => ci.Drink)
//                     .ToList());
//         }

//         public int GetCartTotal()
//         {
//             return _context.CartItems
//                 .Where(ci => ci.CartId == Id)
//                 .Select(ci => ci.Drink.Price * ci.Quantity)
//                 .Sum();
//         }
//     }
// }