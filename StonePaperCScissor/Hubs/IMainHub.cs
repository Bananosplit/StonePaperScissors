using StonePapperScissorLib;

namespace StonePaperCScissor.Hubs
{
    public interface IMainHub
    {
        Task Send(Message message);
        Task SetName(string name);
        
    }
}