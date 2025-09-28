using EETDotNetTrainingBatch2.RealtimeNotification.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EETDotNetTrainingBatch2.RealtimeNotification.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public AnnouncementController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(AnnouncementRequestModel requestModel)
        {
           await _hubContext.Clients.All.SendAsync("ReceiveAnnouncement", requestModel.Title, requestModel.Content);
            return RedirectToAction("Index");
        }
    }

   
}

public class AnnouncementRequestModel
{
       public string Title { get; set; }
    public string Content { get; set; }
}
