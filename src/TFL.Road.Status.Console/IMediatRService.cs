using System.Threading.Tasks;

namespace TFL.Road.Status.Console
{
    public interface IMediatRService
    {
        Task SendRequest();
    }
}
