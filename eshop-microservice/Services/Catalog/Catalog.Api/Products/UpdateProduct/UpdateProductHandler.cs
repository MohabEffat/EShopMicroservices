namespace Catalog.Api.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, string ImageUrl, List<string> Categories)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID Is Required!");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name Is Required!")
                .Length(2, 150).WithMessage("Length must be between 2 - 150 character");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Product Description Is Required!");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Product ImageUrl Is Required!");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price Must Be Greater Than 0!");
        }
    }


    public class UpdateProductCommandHandler
        (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductCommandHandler. Handle Called with {@command}", command);
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product == null)
            {
                logger.LogWarning("Product with Id {ProductId} not found.", command.Id);
                throw new NotFoundException($"Product with Id {command.Id} not found.");
            }
            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.ImageUrl = command.ImageUrl;
            product.Categories = command.Categories;
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
