using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;


namespace NotifyApp.Orchestration
{
    public interface IContextMenuProvider
    {
        List<MenuItem> GetMenu();
    }
}
