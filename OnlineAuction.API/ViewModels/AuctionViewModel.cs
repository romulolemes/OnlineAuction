﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.API.ViewModels
{
    public class AuctionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal InitialValue { get; set; }
        public bool IsUsed { get; set; }
        public string User { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
