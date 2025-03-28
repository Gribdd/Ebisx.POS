﻿namespace Ebisx.POS.Presentation.Services.Interface;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
    Task<bool> CreateProductAsync(Product product);
    Task<bool> DeleteProductAsync(Guid id);
}
