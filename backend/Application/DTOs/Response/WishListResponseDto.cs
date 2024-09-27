namespace Application.DTOs.Response
{
    public class WishListResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public bool State { get; set; }
    }
}
