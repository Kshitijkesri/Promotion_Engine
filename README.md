# Promotion_Engine

This Project use Azure Service Bus Queue.


Flow:- 
Product Cart Send message to Azure Service Bus Queue with Product information and total price after promotion discount.
The Checkout service read the message from queue in doing that it maintain session for every user as well as detect duplicate information.

Please run the project by selecting multiple project 
1) Promotion_Engine_Checkout:- Create the queue and start receiving the message 
2) Promotion_Engine.ProductReader:- Send the product information
