using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Microservice.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Microservice.Features.ProductFeatures.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Models.Product>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Models.Product>>
        {
            private readonly IApplicationContext _context;

            public GetAllProductsQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Models.Product>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
            {
                var productsList = await _context.Products.ToListAsync();
                if (productsList is null) return null;

                return productsList.AsReadOnly();
            }
        }
    }
}