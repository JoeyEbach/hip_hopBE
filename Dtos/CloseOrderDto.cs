using System;
namespace hip_hop.Dtos;

	public class CloseOrderDto
	{
		public int OrderId { get; set; }
		public int PaymentTypeId { get; set; }
		public decimal Tip { get; set; }
	}


