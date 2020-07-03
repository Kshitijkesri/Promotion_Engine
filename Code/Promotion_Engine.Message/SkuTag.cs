using System;

namespace Promotion_Engine.Message
{
    public class SkuTag
    {
        public string SkuId { get; set; }
        public int TotalPrice { get; set; }

        public string Product { get; set; }

        public int Price { get; set; }


        public SkuTag()
        {
            SkuId = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"Product:{ Product }\tPrice:${ Price }";
        }
    }

    public enum Price
    {
        A=50,
        B=30,
        C=20,
        D=15
    }

    public enum PromotionType
    {
        Onproduct,
        percentageDiscount,
        OverAllDiscount,
        Others
    }
}
