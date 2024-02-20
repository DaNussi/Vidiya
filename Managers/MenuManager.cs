using System;
using System.Windows.Controls;
using Vidiya.Elements.Menus;

namespace Vidiya.Managers
{
    public class MenuManager : Manager
    {
        public UserControl currentMenu = new MainMenu();
        public event EventHandler<UserControl>? MenuChanged;


        public MenuManager(LogManager logger) : base(logger) { }

        public void changeTo(UserControl userControl)
        {
            currentMenu = userControl;
            MenuChanged?.Invoke(this, userControl);
        }

        public override void Init()
        {
            changeTo(new MainMenu());
        }

        public override void Exit()
        {

        }

        public void returnToPrevious()
        {
            changeTo(new MainMenu());
        }
    }
}
