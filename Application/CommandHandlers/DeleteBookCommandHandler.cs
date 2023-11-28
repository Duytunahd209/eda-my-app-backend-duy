using MediatR;
using my_app_backend.Application.Commands;
using my_app_backend.Domain.AggregateModel.BookAggregate;
using my_app_backend.Domain.SeedWork.Models;

namespace my_app_backend.Application.CommandHandlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result>
    {
        private readonly IBookEventStore _bookEventStore;
        public DeleteBookCommandHandler(IBookEventStore bookEventStore)
        {
            _bookEventStore = bookEventStore;
        }
        public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var aggregate = await _bookEventStore.Get(request.Id);
                aggregate.DeleteBook();
                await _bookEventStore.Delete(aggregate);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error($"Exception happened: {ex}");
            }
        }
    }
}
