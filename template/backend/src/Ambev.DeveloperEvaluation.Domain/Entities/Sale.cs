using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity, ISale
    {
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
        public bool IsCancelled { get; set; }
        public Guid IdBranch { get; set; }
        string ISale.Id => Id.ToString();
        public SaleStatus Status { get; set; }
        public Guid IdUser { get; set; }

        public void ApplyCancelle()
        {
            IsCancelled = true;
        }
        public void ApplyDiscount()
        {
            foreach (var item in SaleItems)
            {
                if (item.Quantity > 20)
                {
                    throw new InvalidOperationException("Não é possível vender acima de 20 itens idênticos.");
                }

                if (item.Quantity >= 10)
                {
                    item.Discount = 0.20m; // 20% de desconto
                }
                else if (item.Quantity >= 4)
                {
                    item.Discount = 0.10m; // 10% de desconto
                }
                else
                {
                    item.Discount = 0m; // Sem desconto para menos de 4 itens
                }

                item.TotalAmount = item.Discount == 0 ? item.Quantity * item.UnitPrice * (1 - item.Discount) : item.Quantity * item.UnitPrice;
            }

            // Atualiza o TotalAmount com a soma dos UnitPrice dos itens
            TotalAmount = SaleItems.Sum(i => i.Quantity * i.UnitPrice * (1 - i.Discount));
        }

        public void AddSaleItem(SaleItem item)
        {
            if (item.Quantity <= 0)
            {
                throw new ArgumentException("A quantidade de itens deve ser positiva.");
            }

            SaleItems.Add(item);
            // Atualiza o TotalAmount com a soma dos UnitPrice dos itens
            TotalAmount = SaleItems.Sum(i => i.Quantity * i.UnitPrice * (1 - i.Discount));
        }

        public void UpdateTotalAmount()
        {
            TotalAmount = SaleItems.Sum(i => i.Quantity * i.UnitPrice * (1 - i.Discount));
        }
    }

    public class SaleItem
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
    }
}
