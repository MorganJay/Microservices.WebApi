using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Microservice.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Microservice.Features.ProductFeatures.Queries
{
    public class GetProductByIdQuery : IRequest<Models.Product>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Models.Product>
        {
            private readonly IApplicationContext _context;

            public GetProductByIdQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<Models.Product> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Where(p => p.Id == query.Id).FirstOrDefaultAsync();

                return product;
            }
        }
    }
}