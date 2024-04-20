using System.ComponentModel.DataAnnotations;
using hip_hop.Models;

namespace hip_hop.Models;

	public class Item
	{
		public int Id { get; set; }
		public string OrderItem { get; set; }
		public decimal ItemPrice { get; set; }
	    public List<OrderItem> Order { get; set; } = new List<OrderItem>();
	}
