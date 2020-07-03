using System;

namespace Promotion_Engine.Config
{
    public  class AccountDetails
    {
        //ToDo: Enter a valid Serivce Bus connection string
        public static string ConnectionString = "Endpoint=sb://promotionengine.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=wb0PEcqkTZYqeEri2syKXw9latXcHTRFag7rg1QyQQI=";
        public static string QueueName = "promotionenginecheckout";
    }
}
