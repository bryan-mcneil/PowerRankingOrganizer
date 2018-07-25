using System;
using System.Linq;
using System.Web.Mvc;
using PowerRankingOrganizer.Models;

namespace PowerRankingOrganizer.Controllers
{
    public class PlayerController : Controller
    {
        private ApplicationDbContext _context;
        public PlayerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Player
        public ActionResult Index()
        {
            return View(_context.CurrentPlayers.ToList());
        }

        public ActionResult Historical()
        {
            return View(_context.Players.ToList());
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Player player)
        {         
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Create", player);
                }

                if (player.Id == 0)
                {
                    player.RegisteredTime = DateTime.Now;
                    player.PowerRank = 0;
                    player.SetWins = 0;
                    player.SetLoses = 0;
                    player.MatchWins = 0;
                    player.MatchLoses = 0;

                    _context.Players.Add(player);

                    var currentPlayerInfo = new CurrentPlayer
                    {
                        Name = player.Name,
                        Main = player.Main,
                        Secondary = player.Secondary,
                        Color = player.Color,
                        RegisteredTime = DateTime.Now,
                        PowerRank = 0,
                        SetWins = 0,
                        SetLoses = 0,
                        MatchWins = 0,
                        MatchLoses = 0,
                    };

                    _context.CurrentPlayers.Add(currentPlayerInfo);
                }
                else
                {
                    var playerInDb = _context.Players.Single(c => c.Id == player.Id);
                    playerInDb.Name = player.Name;
                    playerInDb.Main = player.Main;
                    playerInDb.Secondary = player.Secondary;
                    playerInDb.Color = player.Color;

                    var currentPlayerInDb = _context.CurrentPlayers.Single(c => c.Id == player.Id);
                    currentPlayerInDb.Name = player.Name;
                    currentPlayerInDb.Main = player.Main;
                    currentPlayerInDb.Secondary = player.Secondary;
                    currentPlayerInDb.Color = player.Color;
                }

                _context.SaveChanges();

                return RedirectToAction("Index", "Player");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var player = _context.Players.SingleOrDefault(c => c.Id == id);

            if (player == null)
                return HttpNotFound();

            return View("Create", player);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var player = _context.Players.SingleOrDefault(c => c.Id == id);
            var current = _context.CurrentPlayers.SingleOrDefault(c => c.Id == id);

            if (player == null)
                return HttpNotFound();

            if (current == null)
                return HttpNotFound();

            return View(player);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var player = _context.Players.SingleOrDefault(c => c.Id == id);
                _context.Players.Remove(player ?? throw new InvalidOperationException());

                var current = _context.CurrentPlayers.SingleOrDefault(c => c.Id == id);
                _context.CurrentPlayers.Remove(current ?? throw new InvalidOperationException());

                _context.SaveChanges();

                return RedirectToAction("Index", "Player");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}