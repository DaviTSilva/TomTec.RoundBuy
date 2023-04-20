using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.DTOs.v1
{
    public class AnnouncementDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int MinimalSaleQuantity { get; set; }
        public string AlternativeAddressSteet { get; set; }
        public string AlternativeAddressNumber { get; set; }
        public string AlternativeAddressAdditionalInformation { get; set; }
        public string AlternativeAddressPostalCode { get; set; }
        public string AlternativeAddressCity { get; set; }
        public string AlternativeAddressStateOrProvince { get; set; }
        public string AlternativeAddressCountryName { get; set; }
        public MainProductDto MainProduct { get; set; }
        public ICollection<ProductDto> Products { get; set; }

        public Announcement ToModel(int userId)
        {
            var productPacks = new List<ProductPack>();

            ICollection<TechnicalInfo> mainTechnicalInfos = (ICollection<TechnicalInfo>)this.MainProduct.TechnicalInfos.Select(x => new TechnicalInfo()
            {
                Title = x.Title,
                InfoSeparetedBySemicolon = x.InfoSeparetedBySemicolon,
                CreationDate = DateTime.UtcNow
            }).ToList();

            ICollection<Image> mainImages = (ICollection<Image>)this.MainProduct.Images.Select(x => new Image()
            {
                AltText = x.AltText,
                Url = x.Url,
                CreationDate = DateTime.UtcNow,
            }).ToList();

            productPacks.Add(new ProductPack() 
            {
                Quantity = this.MainProduct.Quantity,
                Product = new Product()
                {
                    Title = this.MainProduct.Title,
                    Model = this.MainProduct.Model,
                    Color = this.MainProduct.Color,
                    Price = this.MainProduct.Price,
                    TechnicalInfos = mainTechnicalInfos,
                    Images = mainImages,
                    CreationDate = DateTime.UtcNow,
                },
                CreationDate = DateTime.UtcNow,
            });

            if (this.Products.Count > 0)
            {
                productPacks.AddRange(this.Products.Select(x => new ProductPack()
                {
                    Quantity = x.Quantity,
                    Product = new Product() 
                    {
                        Title = string.IsNullOrEmpty(x.Title)? this.MainProduct.Title : x.Title,
                        Model = string.IsNullOrEmpty(x.Model)? this.MainProduct.Model : x.Model,
                        Color = string.IsNullOrEmpty(x.Color)? this.MainProduct.Color : x.Color,
                        Price = x.Price == 0? this.MainProduct.Price : x.Price,
                        TechnicalInfos = mainTechnicalInfos,
                        Images = x.Images == null? mainImages : (ICollection<Image>)x.Images.Select(y => new Image()
                        {
                            AltText = y.AltText,
                            Url = y.Url,
                            CreationDate = DateTime.UtcNow,
                        }),
                        CreationDate = DateTime.UtcNow,
                    },
                    CreationDate = DateTime.UtcNow,
                }));
            }         

            return new Announcement()
            {
                AdvertiserUserId = userId,
                Title = this.Title,
                Description = this.Description,
                AlternativeAddress = !string.IsNullOrEmpty(this.AlternativeAddressSteet)? 
                    new Address() 
                    { 
                        Street = this.AlternativeAddressSteet,
                        Number = this.AlternativeAddressNumber,
                        AdditionalInformation = this.AlternativeAddressAdditionalInformation,
                        PostalCode = this.AlternativeAddressPostalCode,
                        City = this.AlternativeAddressCity,
                        StateOrProvince = this.AlternativeAddressStateOrProvince,
                        CountryName = this.AlternativeAddressCountryName,
                        CreationDate = DateTime.UtcNow,
                    }
                    : null,
                ProductPacks = productPacks,
                IsActive = true,
                IsAvailable = true,
                IsAllSold = false,
                Comments = null,
                Ratings = null,
            };
        }

    }

    public class MainProductDto
    {
        public int Quantity { get; set; }
        public string Title { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public ICollection<TechnicalInfoDto> TechnicalInfos { get; set; }
        public ICollection<ImageDto> Images { get; set; }

    }

    public class ProductDto
    {
        public int Quantity { get; set; }
        public string Title { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public ICollection<ImageDto> Images { get; set; }

    }

    public class TechnicalInfoDto
    {
        public string Title { get; set; }
        public string InfoSeparetedBySemicolon { get; set; }
    }

    public class ImageDto 
    {
        public string AltText { get; set; }
        public string Url { get; set; }
    }

}
