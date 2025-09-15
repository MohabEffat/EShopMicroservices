namespace Catalog.Api.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, decimal Price, string ImageUrl, List<string> Categories)
    : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name Is Required!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Product Description Is Required!");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Product ImageUrl Is Required!");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price Must Be Greater Than 0!");
        }
    }
    internal class CreateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                ImageUrl = command.ImageUrl,
                Categories = command.Categories
            };
            session.Store(product);

            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }

}



