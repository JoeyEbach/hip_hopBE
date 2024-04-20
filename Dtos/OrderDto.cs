using System;
using hip_hop.Models;

namespace hip_hop.Dtos
{
	public class OrderDto
	{
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int OrderTypeId { get; set; }
    }
}

