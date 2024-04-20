using System;
namespace hip_hop.Models
{
	public class OrderItem
	{
		public int Id { get; set; }
		public Order Order { get; set; }
		public Item Item { get; set; }
	}
}

