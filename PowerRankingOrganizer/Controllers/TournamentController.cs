using System.Linq;
using System.Web.Mvc;
//using PowerRankingOrganizer.Models;
using PowerRankingOrganizer.Statics;

namespace PowerRankingOrganizer.Controllers
{
    public class TournamentController : Controller
    {
        //private ApplicationDbContext _context;
        //public TournamentController()
        //{
            //_context = new ApplicationDbContext();
        //}

        //protected override void Dispose(bool disposing)
        //{
            //_context.Dispose();
        //}

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

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var tournaments = ApiCaller.CallTournamentApi();

            var tournament = tournaments.SingleOrDefault(t => t.id == id);

            if (tournament == null)
                return HttpNotFound();

            return View(tournament);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                ApiCaller.DeleteTournament(id);

                return RedirectToAction("Index", "Tournament");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}