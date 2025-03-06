using System.Collections.Generic;
using LuxImperium.Models;

namespace LuxImperium.Services.Actions
{
    public interface IAction
    {
        List<ActionExecuteResult> Execute();
        List<int> Channels { get; set; }
    }
}