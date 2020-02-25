using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Order.Domain.DomainHandler;

namespace Order.Api.Controllers
{
    public class ApiController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;


        protected ApiController(DomainNotificationHandler notifications)
        {
            _notifications = notifications;
        }

        protected IEnumerable<string> Notifications => _notifications.GetNotifications();

        protected new IActionResult Response(object result = null)
        {
            var lst = _notifications.GetNotifications();

            return Ok(new
            {
                success = true,
                data = result,
                notification = lst
            });
        }
    }
}