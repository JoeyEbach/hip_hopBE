using System.ComponentModel.DataAnnotations;
using hip_hop.Models;

namespace hip_hop.Models;

	public class Order
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool Status { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public int OrderTypeId { get; set; }
		public OrderType OrderType { get; set; }
		public DateTime? DateClosed { get; set; }
		public int? PaymentTypeId { get; set; }
		public PaymentType PaymentType { get; set; }
		public decimal Tip { get; set; }
		public decimal TotalAndTip => OrderTotal + Tip;
		public List<OrderItem> Items { get; set; } = new List<OrderItem>();
		public decimal OrderTotal
			{
				get
				{
					 if (Items != null)
					 {
						 return Items.Sum(orderItem => orderItem.Item.ItemPrice);
					 }
					 return 0;

            
				 }
			}

	}


