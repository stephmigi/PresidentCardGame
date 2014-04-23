namespace President.ObjectModel
{
    public static class EnumHelpers
    {
        /// <summary>Gets the next order of a given starting order</summary>
        /// <param name="order">The order</param>
        /// <returns><see cref="Order"/>The next order</returns>
        public static Order GetNextOrder(this Order order)
        {
            return order == Order.Right ? Order.Top : ++order;
        }
    }
}
