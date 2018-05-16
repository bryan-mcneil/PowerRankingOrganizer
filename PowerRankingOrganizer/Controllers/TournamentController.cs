using System.Linq;
using System.Web.Mvc;
using PowerRankingOrganizer.Models;
using PowerRankingOrganizer.Statics;

namespace PowerRankingOrganizer.Controllers
{
    public class TournamentController : Controller
    {
        private ApplicationDbContext _context;
        public TournamentController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var tournaments = ApiCaller.CallTournamentApi();

            return View(tournaments);
        }

        public ActionResult Details(int? id)
        {
            var tournaments = ApiCaller.CallTournamentApi();

            var tournament = tournaments.SingleOrDefault(t => t.id == id);

            if (tournament == null)
            {
                return HttpNotFound();
            }

            return View(tournament);
        }
    }
}