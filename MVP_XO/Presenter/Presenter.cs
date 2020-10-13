using MVP_XO.AI;
using MVP_XO.Interfaces;
using MVP_XO.Model;
using System;
using System.Threading;

namespace MVP_XO.Presenter
{
    sealed class GamePresenter
    {
        IView _Iview;
        Game game;
        Difficulty difficulty;
        Turn whoIsFirst;

        public GamePresenter(IView _Iview)
        {
            this._Iview = _Iview;
            _Iview.ClickEvent += _Iview_ClickEvent;
            _Iview.HighDifficultyClick += _Imenu_HighDifficultyClick;
            _Iview.NormalDifficultyClick += _Imenu_NormalDifficultyClick;
            _Iview.LowDifficultyClick += _Imenu_LowDifficultyClick;
            _Iview.StartClick += _Imenu_StartClick;
            _Iview.ManFirstEvent += _Iview_ManFirstEvent;
            _Iview.CompFirstEvent += _Iview_CompFirstEvent;

            game = new Game();
            game.CompVictoryEvent += Game_CompVictoryEvent;
            game.HumanVictoryEvent += Game_HumanVictoryEvent;
            game.NobodyVictoryEvent += Game_NobodyVictoryEvent;
            game.CompMoveEvent += Game_CompMoveEvent;
            game.ManMoveEvent += Game_ManMoveEvent;
        }

        private void Game_ManMoveEvent(int x, int y, Turn first)
        {
            _Iview.SetHuman(x, y, first);
        }

        private void Game_CompMoveEvent(int x, int y, Turn first)
        {
            _Iview.SetComp(x, y, first);
        }

        private void Game_NobodyVictoryEvent()
        {
            _Iview.GameIsOver();
        }

        private void Game_HumanVictoryEvent((int, int) first, (int, int) second, (int, int) third, Turn whoIsFirst)
        {
            _Iview.HumanVictory(first, second, third, whoIsFirst);
        }

        private void Game_CompVictoryEvent((int, int) first, (int, int) second, (int, int) third, Turn whoIsFirst)
        {
            _Iview.CompVictory(first, second, third, whoIsFirst);
        }

        private void _Iview_CompFirstEvent(object sender, EventArgs e)
        {
            whoIsFirst = Turn.Computer;
        }

        private void _Iview_ManFirstEvent(object sender, EventArgs e)
        {
            whoIsFirst = Turn.Human;
        }

        private void _Imenu_StartClick(object sender, EventArgs e)
        {
            game.SetGameOptions(difficulty, whoIsFirst);
        }

        private void _Imenu_LowDifficultyClick(object sender, EventArgs e)
        {
            difficulty = Difficulty.Easy;
        }

        private void _Imenu_NormalDifficultyClick(object sender, EventArgs e)
        {
            difficulty = Difficulty.Normal;
        }

        private void _Imenu_HighDifficultyClick(object sender, EventArgs e)
        {
            difficulty = Difficulty.Hard;
        }

        private void _Iview_ClickEvent(int x, int y)
        {
            game.Move(x, y);
        }
    }
}
