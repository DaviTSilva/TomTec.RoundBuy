using System;
using System.Collections.Generic;
using System.Text;
using TomTec.RoundBuy.Models;
using TomTec.RoundBuy.Lib.Utils;
using System.Linq;

namespace TomTec.RoundBuy.Business
{
    static public class ProductValidator
    {
        static public void Validate(this Product product)
        {
            if (product.Title.ContainsProfanity())
                throw new InvalidOperationException($"[INVALID \"{nameof(Product).ToUpper()}\"! WRONG VALUE FOR FIELD \"{nameof(Product.Title).ToUpper()}\"]: The product title can not contain profane words!");
            
            if (product.Color.ContainsProfanity())
                throw new InvalidOperationException($"[INVALID \"{nameof(Product).ToUpper()}\"! WRONG VALUE FOR FIELD \"{nameof(Product.Color).ToUpper()}\"]: The product color name can not contain profane words!");

            if (product.Model.ContainsProfanity())
                throw new InvalidOperationException($"[INVALID \"{nameof(Product).ToUpper()}\"! WRONG VALUE FOR FIELD \"{nameof(Product.Model).ToUpper()}\"]: The product model can not contain profane words!");

            if (product.TechnicalInfos.Any(x => x.InfoSeparetedBySemicolon.ContainsProfanity()))
                throw new InvalidOperationException($"[INVALID \"{nameof(Product).ToUpper()}\"! WRONG VALUE FOR FIELD \"{nameof(Product.TechnicalInfos).ToUpper()}\"]: The product technical info can not contain profane words!");

            if(string.IsNullOrEmpty(product.Title))
                throw new InvalidOperationException($"[INVALID \"{nameof(Product).ToUpper()}\"! WRONG VALUE FOR FIELD \"{nameof(Product.Title).ToUpper()}\"]: The product title can not be empty!");
        }
    }
}
