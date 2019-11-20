
using System.Threading.Tasks;

namespace Core.EventBus.Interfaces
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
