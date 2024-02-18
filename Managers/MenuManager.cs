using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Vidiya.Elements;

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
