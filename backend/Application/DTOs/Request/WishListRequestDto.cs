using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Request
{
    public class WishListRequestDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public bool State { get; set; }

    }
}
