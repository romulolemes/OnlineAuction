using System;

namespace OnlineAuction.API.Models
{
    public class AuctionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal InitialValue { get; set; }
        public bool IsUsed { get; set; }
        public string User { get; set; }
        public DateTimeOffset InitialDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
