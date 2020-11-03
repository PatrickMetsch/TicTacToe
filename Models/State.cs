using System;
namespace Tic.Models
{
    public class State
    {
        public int TurnNumber { get; set; }

        public string Character { get; set; }

        public string TopLeft { get; set; }
        public string TopMiddle { get; set; }
        public string TopRight { get; set; }

        public string CenterLeft { get; set; }
        public string CenterMiddle { get; set; }
        public string CenterRight { get; set; }

        public string BottomLeft { get; set; }
        public string BottomMiddle { get; set; }
        public string BottomRight { get; set; }

        public State()
        {
            this.TurnNumber = 0;
            this.Character = "X";

            this.TopLeft = "";
            this.TopMiddle = "";
            this.TopRight = "";

            this.CenterLeft = "";
            this.CenterMiddle = "";
            this.CenterRight = "";

            this.BottomLeft = "";
            this.BottomMiddle = "";
            this.BottomRight = "";
        }
    }

}
