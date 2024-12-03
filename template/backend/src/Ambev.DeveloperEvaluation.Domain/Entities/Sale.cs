using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale in the system with details about the transaction.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class Sale : BaseEntity, ISale
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale.
        /// </summary>
        public int SaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the date when the sale was made.
        /// Must be a valid date and time.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the sale.
        /// Must be a positive decimal value.
        /// </summary>
        public decimal TotalAmount { get; set; }


        ///// <summary>
        ///// Gets or sets the branch where the sale was made.
        ///// </summary>
        //public Branch Branch { get; set; }

        /// <summary>
        /// Gets or sets the collection of products included in the sale.
        /// </summary>
        public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

        /// <summary>
        /// Gets or sets a value indicating whether the sale is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }
        /// <summary>
        /// Gets or sets the customer associated with the sale.
        /// </summary>
        public int CustomerId  { get; set; }

        public DateTime Date { get; set; }

        public int UserId => CustomerId;

        public object BranchId { get; set; }

        string ISale.Id => Id.ToString();

   
    }

    /// <summary>
    /// Represents an item in a sale.
    /// </summary>
    public class SaleItem
    {
        /// <summary>
        /// Gets or sets the product associated with the sale item.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in the sale.
        /// Must be a positive integer.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product in the sale.
        /// Must be a positive decimal value.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount applied to the sale item.
        /// Must be a decimal value between 0 and 1.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the sale item after discount.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sale item is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }
    }
}
