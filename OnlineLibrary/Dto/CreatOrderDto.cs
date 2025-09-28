using OnlineLibrary.Model;

namespace OnlineLibrary.Dto
{
    public class CreatOrderDto
    {
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
