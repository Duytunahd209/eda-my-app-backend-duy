using MediatR;
using my_app_backend.Domain.SeedWork.Models;

namespace my_app_backend.Application.Commands
{
    public class DeleteBookCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
