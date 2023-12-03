using System.Net.Sockets;
using System.Xml.Linq;
using MediatR;
using my_app_backend.Application.QueryRepositories;
using my_app_backend.Domain.AggregateModel.BookAggregate.Events;
using my_app_backend.Models;
using Newtonsoft.Json;

namespace my_app_backend.Application.EventHandlers
{
    public class DeleteEventHandler : INotificationHandler<BookDeletedEvent>
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<DeleteEventHandler> _logger;
        public DeleteEventHandler(IBookRepository bookRepository, ILogger<DeleteEventHandler> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task Handle(BookDeletedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var rs = await _bookRepository.GetById(notification.BookId);
                if (!rs.IsSuccessful)
                {
                    throw new Exception(rs.Message);
                }

                var book = rs.Data;

                book.Id = notification.BookId;

                var updateRs = await _bookRepository.DeleteById(book.Id);
                if (!updateRs.IsSuccessful)
                {
                    throw new Exception(updateRs.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Equals($"Exception happened: sync to read repository fail for BookDeleteEvent: {JsonConvert.SerializeObject(notification)}, ex: {ex}");
            }
        }
    }
}
