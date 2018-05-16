using System;
using System.ComponentModel.DataAnnotations;


namespace PowerRankingOrganizer.Models
{
    public class Player
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

        public int PowerRank { get; set; }

        public int SetWins { get; set; }

        public int SetLoses { get; set; }

        public int MatchWins { get; set; }

        public int MatchLoses { get; set; }

        public DateTime? LastUpdated { get; set; }
    }

    public enum Characters
    {
        None,
        Bowser,
        CaptainFalcon,
        DonkeyKong,
        DrMario,
        Falco,
        Fox,
        Ganondorf,
        IceClimbers,
        Jigglypuff,
        Kirby,
        Link,
        Luigi,
        Mario,
        Marth,
        Mewtwo,
        MrGameAndWatch,
        Ness,
        Peach,
        Pichu,
        Pikachu,
        Roy,
        Samus,
        Sheik,
        Yoshi,
        YoungLink,
        Zelda  
    }

    public enum Color
    {
        Default,
        Blue,
        Green,
        Red,
        White,
        Black,
        Pink,
        Purple,
        Yellow,
        Brown,
        Orange,
        LightBlue,
    }
}