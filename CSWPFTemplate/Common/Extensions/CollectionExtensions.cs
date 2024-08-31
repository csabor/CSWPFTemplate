namespace CSWPFTemplate.Common.Extensions
{
    /// <summary>
    /// Extensions to add convenient methods to the ICollection implementations
    /// </summary>
    internal static class CollectionExtensions
    {
        /// <summary>
        /// Removes all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the entries contained in the provided collection</typeparam>
        /// <param name="collection">The collection to remove elements from.</param>
        /// <param name="predicate">The delegate that defines the conditions of the elements to remove.</param>
        /// <returns>The number of elements removed from the collection.</returns>
        public static int RemoveAll<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            List<T> itemsToRemove = collection.Where(predicate).ToList();
            foreach (T item in itemsToRemove)
            {
                collection.Remove(item);
            }
            return itemsToRemove.Count;
        }

        /// <summary>
        /// Adds the elements of the specified enumerable to the end of the collection.
        /// </summary>
        /// <typeparam name="T">The type of the entries contained in the provided collection and enumerable.</typeparam>
        /// <param name="collection">The collection to add elements to.</param>
        /// <param name="items">An enumerable containing the items to add.</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
