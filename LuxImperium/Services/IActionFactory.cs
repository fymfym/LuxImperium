using LuxImperium.Models;
using LuxImperium.Services.Actions;

namespace LuxImperium.Services
{
    public interface IActionFactory
    {
        IAction Build(DmxAction action);
    }
}