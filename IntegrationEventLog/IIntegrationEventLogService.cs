using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.EventBus;
using Microsoft.EntityFrameworkCore.Storage;

namespace IntegrationEventLog
{
    public interface IIntegrationEventLogService
    {
        Task MarkEventAsFailedAsync(Guid eventId);
        Task MarkEventAsInProgressAsync(Guid eventId);
        Task MarkEventAsPublishedAsync(Guid eventId);
        Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);
        Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);
    }
}