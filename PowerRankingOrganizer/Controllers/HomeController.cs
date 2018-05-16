using System;
using System.Linq;
using System.Web.Mvc;
using PowerRankingOrganizer.Models;
using PowerRankingOrganizer.Statics;

namespace PowerRankingOrganizer.Controllers
{
    //API Bf12lHlDtX2syMb4TkwA4E8fS7ly4zWn1YvtfzNR
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var pr = _context.CurrentPlayers.OrderByDescending(p => p.PowerRank).Take(8).ToList();

            return View(pr);
        }

        public ActionResult ResetResult()
        {
            var resetPlayers = _context.CurrentPlayers;

            foreach (var player in resetPlayers)
            {
                player.SetWins = 0;
                player.SetLoses = 0;
                player.MatchWins = 0;
                player.MatchLoses = 0;
                player.PowerRank = 0;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult UpdatePlayerInfo()
        {
            var tournaments = ApiCaller.CallTournamentApi();

            foreach (var tournament in tournaments)
            {
                if (tournament.completed_at == null)
                    continue;

                if (!tournament.game_name.Equals("Super Smash Bros. Melee"))
                    continue;

                var participants = ApiCaller.CallParticipantApi(tournament.id);

                foreach (var participant in participants)
                {
                    var player = _context.Players.SingleOrDefault(
                        p => p.Name.ToLower().Equals(participant.name.ToLower()));
                    if (player == null)
                        continue;
                    var updateSetWinCheck = player.SetWins;
                    var updateSetLoseCheck = player.SetLoses;

                    var current = _context.CurrentPlayers.SingleOrDefault(
                        c => c.Name.ToLower().Equals(participant.name.ToLower()));                   
                    if (current == null)
                        continue;

                    if (player.LastUpdated.HasValue)
                    {
                        if (player.LastUpdated >= tournament.completed_at)
                            continue;
                    }

                    if (current.LastUpdated.HasValue)
                    {
                        if (current.LastUpdated >= tournament.completed_at)
                            continue;
                    }

                    var matches = ApiCaller.CallMatchApi(tournament.id, participant.id);

                    foreach (var match in matches)
                    {
                        // Helps track index used to get score for scores array
                        var winningScoreId = 0;
                        var losingScoreId = 1;
                        if (participant.id == match.player2_id)
                        {
                            winningScoreId = 1;
                            losingScoreId = 0;
                        }

                        //Get Split Scores into an int[]
                        int[] scores = Array.ConvertAll(match.scores_csv.Split('-'), Int32.Parse);
                        if (participant.id == match.winner_id)
                        {
                            player.SetWins++;
                            current.SetWins++;

                            player.MatchWins += scores[winningScoreId];
                            player.MatchLoses += scores[losingScoreId];
                            current.MatchWins += scores[winningScoreId];
                            current.MatchLoses += scores[losingScoreId];
                        }
                        else if (participant.id == match.loser_id)
                        {
                            player.SetLoses++;
                            current.SetLoses++;

                            player.MatchWins += scores[winningScoreId];
                            player.MatchLoses += scores[losingScoreId];
                            current.MatchWins += scores[winningScoreId];
                            current.MatchLoses += scores[losingScoreId];
                        }
                    }

                    if (player.SetWins == updateSetWinCheck && player.SetLoses == updateSetLoseCheck)
                    {
                        continue;
                    }

                    player.LastUpdated = tournament.completed_at;
                    current.LastUpdated = tournament.completed_at;

                    _context.SaveChanges();
                }
            }

            foreach (var player in _context.Players)
            {
                var power = Convert.ToInt32(CalculatePowerRank(player.SetWins, player.SetLoses,
                    player.MatchWins, player.MatchLoses));

                player.PowerRank = power;
            }

            foreach (var player in _context.CurrentPlayers)
            {
                var power = Convert.ToInt32(CalculatePowerRank(player.SetWins, player.SetLoses,
                    player.MatchWins, player.MatchLoses));

                player.PowerRank = power;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private double CalculatePowerRank(int winScore, int loseScore, int gameWin, int gameLose)
        {
            var powerScore = 0.0;
            var powerGame = 0.0;
            var win = Convert.ToDouble(winScore);
            var lose = Convert.ToDouble(loseScore);
            var gwin = Convert.ToDouble(gameWin);
            var glose = Convert.ToDouble(gameLose);

            if (winScore != 0)
            {
                powerScore = (win / (win + lose)) * 1000;
            }

            if (gameWin != 0)
            {
                powerGame = (gwin / (gwin + glose)) * 100;
            }

            if (powerScore + powerGame > 1000)
            {
                return 1000;
            }

            return powerScore + powerGame;
        }
    }
}