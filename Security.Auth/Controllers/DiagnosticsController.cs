// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.Security.Auth.Identify.UI;
using System.Linq;

namespace OnlineAuction.Security.Auth.Controllers
{
    [SecurityHeaders]
    [Authorize]
    public class DiagnosticsController : Controller
    {
        public IActionResult Index()
        {
            var localAddresses = new string[] { "127.0.0.1", "::1", HttpContext.Connection.LocalIpAddress.ToString() };
            if (localAddresses.Contains(HttpContext.Connection.RemoteIpAddress.ToString()))
            {
                return View();
            }

            return NotFound();
        }
    }
}