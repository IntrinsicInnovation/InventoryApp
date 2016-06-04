# InventoryApp
Inventory App project.  This uses Web API, Signal R for  notifications, and Angular to display notifications.  

This is a solution containing two .net 4.5.2 projects.  The first project is 'Inventory App'.  It has functions to add, delete and modify inventory items, which are called products.  The inventory app api is at api/inventory.  There is also Signal R implementation.  This allows notifications to sent (in this case) from the server to all clients to view notifications such as expired items, and the result of any deletions of inventory items.

The second project is called 'InventoryApp.Tests', which contains unit tests including the most important ones:

GetAllProducts_ShouldReturnAllProducts()
PostProduct_ShouldReturnSameProduct()
DeleteProduct_ShouldReturnOK()


- To run the unit tests, simply launch Visual Studio 2015, and click on the test menu, then select 'run' and 'All Tests'

- To see the notifications being sent from the server, simply launch the solution by hitting 'F5'.  Click the button to delete the item, and wait for other notifications to appear regarding expired items.





