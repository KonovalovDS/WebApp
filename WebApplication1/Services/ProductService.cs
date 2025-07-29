using WebApplication1.Mappers;
using WebApplication1.DTOs;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Services {
    public class ProductService : IProductService{
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<ProductService> _logger;

        public ProductService(AppDbContext productDbContext, ILogger<ProductService> logger) {
            _appDbContext = productDbContext;
            _logger = logger;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync() {
            _logger.LogInformation("Получен запрос на получение всех товаров");
            var products = await _appDbContext.Products.ToListAsync();
            return ProductMapper.ToDtoList(products);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id) {
            _logger.LogInformation("Получен запрос на получение заказа: {id}", id);
            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null) return null;
            return ProductMapper.ToDto(product);
        }

        public async Task<bool> IsAvailableAsync(OrderDetailsDto order) {
            _logger.LogInformation("Проверка на наличие товаров");
            var items = OrderMapper.ToModel(order).Items;
            foreach (var item in items) {
                var product = await _appDbContext.Products.FindAsync(item.Id);
                if (product == null) {
                    _logger.LogInformation("Товара не существует: {Id}", item.Id);
                    return false;
                }
                if (product.Quantity < item.Quantity) {
                    _logger.LogInformation("Недостаточно товаров {Product}: {Quantity}", product.Name, product.Quantity);
                    return false; 
                }
            }
            _logger.LogInformation("Товаров достаточно для формирования заказа");
            return true;
        }
        public async Task ReserveProductsAsync(OrderDetailsDto order) {
            _logger.LogInformation("Получен запрос на резервирование товаров для формирования заказа");
            var items = OrderMapper.ToModel(order).Items;
            foreach (var item in items) {
                var product = await _appDbContext.Products.FindAsync(item.Id);
                if (product != null) {
                    product.Quantity -= item.Quantity;
                    _appDbContext.Products.Update(product);
                }
            }
            _logger.LogInformation("Товары зарезервированы");
            await _appDbContext.SaveChangesAsync();
        }

        public async Task AddProductAsync(Product product) {
            _logger.LogInformation("Получен запрос на добавление нового товара: {Name}", product.Name);
            _appDbContext.Products.Add(product);
            _logger.LogInformation("Товар создан: {Name}", product.Name);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ProductResponseDto> DeleteProductByIdAsync(int id) {
            _logger.LogInformation("Получен запрос на удаление товара: {Id}", id);
            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null) {
                _logger.LogInformation("Товар не существует: {Id}", id);
                return new ProductResponseDto {
                    Success = false,
                    Message = "Product not found"
                };
            }
            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("Товар удален: {Id}", id);
            return new ProductResponseDto {
                Success = true,
                Message = "Product successfully deleted"
            };
        }
    }
}
