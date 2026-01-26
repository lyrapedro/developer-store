using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Events;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Application.Events;

/// <summary>
/// Handler for SaleCreatedEvent.
/// In a real-world scenario, this would publish to a message broker (RabbitMQ, Kafka, etc.).
/// For this implementation, we log the event for demonstration purposes.
/// </summary>
public class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
{
    private readonly ILogger<SaleCreatedEventHandler> _logger;

    /// <summary>
    /// Initializes a new instance of SaleCreatedEventHandler.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public SaleCreatedEventHandler(ILogger<SaleCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handles the SaleCreatedEvent.
    /// </summary>
    /// <param name="notification">The sale created event.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        var eventJson = JsonSerializer.Serialize(notification, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        _logger.LogInformation(
            "========== SALE CREATED EVENT ==========\n" +
            "Event Type: SaleCreated\n" +
            "Sale ID: {SaleId}\n" +
            "Sale Number: {SaleNumber}\n" +
            "Customer: {CustomerName} ({CustomerEmail})\n" +
            "Branch: {BranchName}\n" +
            "Total Amount: {TotalAmount:C}\n" +
            "Items Count: {ItemCount}\n" +
            "Created At: {CreatedAt:yyyy-MM-dd HH:mm:ss}\n" +
            "Event Payload:\n{EventJson}\n" +
            "========================================",
            notification.SaleId,
            notification.SaleNumber,
            notification.CustomerName,
            notification.CustomerEmail,
            notification.BranchName,
            notification.TotalAmount,
            notification.ItemCount,
            notification.CreatedAt,
            eventJson);

        await Task.CompletedTask;
    }
}