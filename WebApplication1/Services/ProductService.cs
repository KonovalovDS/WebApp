using WebApplication1.Mappers;
using WebApplication1.DTOs;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Services {
    public class ProductService : IProductService{
        private readonly AppDbContext _appDbContext;

        public ProductService(AppDbContext productDbContext) {
            _appDbContext = productDbContext;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync() {
            var products = await _appDbContext.Products.ToListAsync();
            return ProductMapper.ToDtoList(products);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id) {
            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null) return null;
            return ProductMapper.ToDto(product);
        }

        public async Task<bool> IsAvailableAsync(OrderDetailsDto order) {
            var items = OrderMapper.ToModel(order).Items;
            foreach (var item in items) {
                var product = await _appDbContext.Products.FindAsync(item.Id);
                if (product == null || product.Quantity < item.Quantity) return false;
            }
            return true;
        }
        public async Task ReserveProductsAsync(OrderDetailsDto order) {
            var items = OrderMapper.ToModel(order).Items;
            foreach (var item in items) {
                var product = await _appDbContext.Products.FindAsync(item.Id);
                if (product != null) {
                    product.Quantity -= item.Quantity;
                    _appDbContext.Products.Update(product);
                }
            }
            await _appDbContext.SaveChangesAsync();
        }

        public async Task AddProductAsync(Product product) {
            _appDbContext.Products.Add(product);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ProductResponseDto> DeleteProductByIdAsync(int id) {
            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null) {
                return new ProductResponseDto {
                    Success = false,
                    Message = "Product not found"
                };
            }
            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();
            return new ProductResponseDto {
                Success = true,
                Message = "Product successfully deleted"
            };
        }
    }
}
