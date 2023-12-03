using MediatR;

namespace my_app_backend.Domain.AggregateModel.BookAggregate.Events
{
    public class BookDeletedEvent : BookEvent, IBookEvent
    {
        public Guid Id { get; set; }
        public Guid BookId { get; internal set; }
    }
}
