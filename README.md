Order Service

Order Service with two endoints.

v1/Orser/availableondate

As input takes list of object with following properties: ProductId, PostalCode and DeliveryDay. Each product delivery request in the list must be unique. Returns list of confirmed orders if orders can be delivered on requested dates. If delivery on that day is done with electric vehicle that IsGreenDelivery is set to true. Returns empty list if non of the orders can not be confirmed - delivered on requested dates. If required product is not in the list of available products respons is 400 - BadRequest (available products from ProductId=1 to ProductId=10). Output is list of object with following properties: ProductId, PostalCode, DeliveryDay and IsGreenDelivery;

v1/Orders/availablefordelivery

As input takes list of object with following properties: ProductId and PostalCode. Returns List of available days for delivery of particular product. If delivery on that day is done with electric vehicle that IsGreenDelivery is set to true.  If required product is not in the list of available products respons is 400 - BadRequest. (available products from ProductId=1 to ProductId=10). Output is list of object with following properties: ProductId, PostalCode, DeliveryDay and IsGreenDelivery;

For both endpoints same business rules apply.
