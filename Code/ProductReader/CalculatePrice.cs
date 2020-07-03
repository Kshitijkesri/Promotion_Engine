using Promotion_Engine.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promotion_Engine.ProductReader
{
   public static class   CalculatePrice
    {

        public static int calculateTotalPriceCart(SkuTag[] orderCart, PromotionType promotionType)
        {
            int totalPrice=0;
            switch (promotionType)
            {
                case PromotionType.Onproduct:
                    int A = (orderCart.Select(x => x.Product).Where(y => y.Equals("A")).Count());
                    int B = orderCart.Select(x => x.Product).Where(y => y.Equals("B")).Count();
                    int C = orderCart.Select(x => x.Product).Where(y => y.Equals("C")).Count();
                    int D = orderCart.Select(x => x.Product).Where(y => y.Equals("D")).Count();

                    if(A!=0 && A>3)
                    {
                        var n = A / 3;
                        var r = A % 3;
                        int totalA = n * OnProdcutPromotion.type3A + r*(int)Price.A;
                        totalPrice += totalA;
                        
                      
                    }
                    else
                    { totalPrice += A * (int)Price.A; }

                    if (B != 0 && B > 2)
                    {
                        var n = B / 2;
                        var r = B % 2;
                        int totalB = n * OnProdcutPromotion.type2B + r * (int)Price.B;
                        totalPrice += totalB;
                                              
                    }
                    else
                    { totalPrice += B * (int)Price.B; }

                    
                    if(C>0 && D>0)
                    {
                        if ((C + D) % 2 == 0)
                        {
                            int totalC= (C + D) / 2 * 30;
                            totalPrice += totalC;
                          
                            Console.WriteLine("Total C Price", totalC);
                           
                        }
                        else
                        {
                            var value= ((C + D) / 2);
                            var Cq = (C - value) >0?C- value : 0;
                            var Dp = D - value > 0?D- value : 0;
                            totalPrice += value * 30 + Cq * 20 + Dp * 15;                         
                          
                                                        
                        }

                    }
                   
                    break;
                case PromotionType.OverAllDiscount:
                    Console.WriteLine("Case 2");
                    break;

                case PromotionType.percentageDiscount:
                    Console.WriteLine("Case 2");
                    break;
                case PromotionType.Others:
                    Console.WriteLine("Case 2");
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            return totalPrice;
        }
    }
}
