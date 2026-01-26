using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Events;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Application.Events;

/// <summary>
/// Handler for SaleCancelledEvent.
/// In a real-world scenario, this would publish to a message broker (RabbitMQ, Kafka, etc.).
/// For this implementation, we log the event for demonstration purposes.
/// </summary>
public class SaleCancelledEventHandler : INotificationHandler<SaleCancelledEvent>
{
    private readonly ILogger<SaleCancelledEventHandler> _logger;

    /// <summary>
    /// Initializes a new instance of SaleCancelledEventHandler.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public SaleCancelledEventHandler(ILogger<SaleCancelledEventHandler> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handles the SaleCancelledEvent.
    /// </summary>
    /// <param name="notification">The sale cancelled event.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
    {
        // Serialize event to JSON for logging
        var eventJson = JsonSerializer.Serialize(notification, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        _logger.LogWarning(
            "========== SALE CANCELLED EVENT ==========\n" +
            "Event Type: SaleCancelled\n" +
            "Sale ID: {SaleId}\n" +
            "Sale Number: {SaleNumber}\n" +
            "Customer: {CustomerName}\n" +
            "Branch: {BranchName}\n" +
            "Total Amount: {TotalAmount:C}\n" +
            "Cancellation Reason: {CancellationReason}\n" +
            "Cancelled At: {CancelledAt:yyyy-MM-dd HH:mm:ss}\n" +
            "Original Sale Date: {OriginalSaleDate:yyyy-MM-dd HH:mm:ss}\n" +
            "Cancelled By: {CancelledBy}\n" +
            "Event Payload:\n{EventJson}\n" +
            "==========================================",
            notification.SaleId,
            notification.SaleNumber,
            notification.CustomerName,
            notification.BranchName,
            notification.TotalAmount,
            notification.CancellationReason,
            notification.CancelledAt,
            notification.OriginalSaleDate,
            notification.CancelledBy ?? "System",
            eventJson);

        await Task.CompletedTask;
    }
}