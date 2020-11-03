using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Tic.Models;
using Newtonsoft.Json;

namespace Tic.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            State state = new State();
            string stateJson = JsonConvert.SerializeObject(state);
            HttpContext.Session.SetString("state", stateJson);
            return View();
        }

        public IActionResult Update(string id)
        {
            string stateString = HttpContext.Session.GetString("state") ?? "";
            State state = JsonConvert.DeserializeObject<State>(stateString);

            state.GetType().GetProperty(id).SetValue(state, state.Character);

            state.TurnNumber += 1;

            string newCharacter = state.TurnNumber % 2 == 0 ? "X" : "O";
            state.Character = newCharacter;

            string newStateString = JsonConvert.SerializeObject(state);
            HttpContext.Session.SetString("state", newStateString);

            bool isWinner = false;

            string tl = state.TopLeft;
            string tm = state.TopMiddle;
            string tr = state.TopRight;

            string cl = state.CenterLeft;
            string cm = state.CenterMiddle;
            string cr = state.CenterRight;

            string bl = state.BottomLeft;
            string bm = state.BottomMiddle;
            string br = state.BottomRight;

            string[] topRow = { tl, tm, tr };
            string[] middleRow = { cl, cm, cr };
            string[] bottomRow = { bl, bm, br };

            string[] leftCol = { tl, cl, bl };
            string[] middleCol = { tm, cm, bm };
            string[] rightCol = { tr, cr, br };

            string[] lrDiag = { tl, cm, br };
            string[] rlDiag = { tr, cm, bl };

            string[] xWin = { "X", "X", "X" };
            string[] oWin = { "O", "O", "O" };

            if (
                (Enumerable.SequenceEqual(topRow, xWin) || Enumerable.SequenceEqual(topRow, oWin)) ||
                (Enumerable.SequenceEqual(middleRow, xWin) || Enumerable.SequenceEqual(middleRow, oWin)) ||
                (Enumerable.SequenceEqual(bottomRow, xWin) || Enumerable.SequenceEqual(bottomRow, oWin)) ||
                (Enumerable.SequenceEqual(leftCol, xWin) || Enumerable.SequenceEqual(leftCol, oWin)) ||
                (Enumerable.SequenceEqual(middleCol, xWin) || Enumerable.SequenceEqual(middleCol, oWin)) ||
                (Enumerable.SequenceEqual(rightCol, xWin) || Enumerable.SequenceEqual(rightCol, oWin)) ||
                (Enumerable.SequenceEqual(lrDiag, xWin) || Enumerable.SequenceEqual(lrDiag, oWin)) ||
                (Enumerable.SequenceEqual(rlDiag, xWin) || Enumerable.SequenceEqual(rlDiag, oWin))
            )
            {
                isWinner = true;
            }

            if (isWinner == true)
            {
                string sString = HttpContext.Session.GetString("state") ?? "";
                State s = JsonConvert.DeserializeObject<State>(sString);
                s.TurnNumber += 1;

                string newChar = s.TurnNumber % 2 == 0 ? "X" : "O";
                s.Character = newChar;

                string newSString = JsonConvert.SerializeObject(s);
                HttpContext.Session.SetString("state", newSString);

                return RedirectToAction("Winner");
            }
            else
            {
                return View();
            }
            
        }

        public IActionResult Winner()
        {
            return View();
        }
    }   
}
