﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lsiovskii_20331.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        /// <summary> 
        /// Список объектов в корзине 
        /// key - идентификатор объекта 
        /// </summary> 
        public Dictionary<int, CartItem> CartItems { get; set; } = new();
        /// <summary> 
        /// Добавить объект в корзину 
        /// </summary> 
        /// <param name="book">Добавляемый объект</param> 
        public virtual void AddToCart(Book book)
        {
            if (CartItems.ContainsKey(book.Id))
            {
                CartItems[book.Id].Qty++;
            }
            else
            {
                CartItems.Add(book.Id, new CartItem
                {
                    Item = book,
                    Qty = 1
                });
            };
        }

        public virtual void RemoveItems(int id)
        {
            CartItems.Remove(id);
        }

        public virtual void ClearAll()
        {
            CartItems.Clear();
        }

        public int Count { get => CartItems.Sum(item => item.Value.Qty); }

        public double TotalPages
        {
            get => CartItems.Sum(item => item.Value.Item.Pages * item.Value.Qty);
        }
    }
}
