using System.Diagnostics;
using EETDotNetTrainingBatch2.RealtimeNotification.Hubs;
using EETDotNetTrainingBatch2.RealtimeNotification.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EETDotNetTrainingBatch2.RealtimeNotification.Controllers
{
    public class HomeController : Controller
    {
    
        private readonly IHubContext<NotificationHub> _hubContext;

        public HomeController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> SendAnnouncement(string title, string content)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveAnnouncement", title, content);
            return Ok();
        }
    }
}
