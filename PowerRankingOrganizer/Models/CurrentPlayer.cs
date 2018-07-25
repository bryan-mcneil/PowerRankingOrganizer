using System;
using System.ComponentModel.DataAnnotations;

namespace PowerRankingOrganizer.Models
{
    public class CurrentPlayer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Joined")]
        [DataType(DataType.Date)]
        public DateTime RegisteredTime { get; set; }

        [Required]
        public Characters Main { get; set; }

        public Characters Secondary { get; set; }

        public Colors Color { get; set; }

        public int PowerRank { get; set; }

        public int SetWins { get; set; }

        public int SetLoses { get; set; }

        public int MatchWins { get; set; }

        public int MatchLoses { get; set; }

        public DateTime? LastUpdated { get; set; }

        public int Bonus { get; set; } = 0;
    }
}