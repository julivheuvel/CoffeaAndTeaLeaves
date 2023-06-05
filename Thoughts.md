
like coffee or tea into list
=========

rate & comment on drinks
=========

follow users posts and drinks
=========

click on drinks for view
=========

add drinks to cart for purchase
=========

review purchase history
=========

<!-- ? ===========================================  -->


> =========
> Current tables
> =========
> - User
> - LoginUser
> - Friendship - User One-To-One
> - ContactInfo
> - Payment
> - Order
> - Coffee


<!-- ? ===========================================  -->


> =========
> User
> =========
> - UserId
> - FirstName
> - LastName
> - Username
> - Email
> - Birthday
> - Password
> - ConfirmPassword

> =========
> Friendship
> =========
> - FriendshipId

> =========
> LoginUser
> =========
> - [NotMapped]
> - LoginUsername
> - LoginPassword

> =========
> Pin
> =========
> - PinId
> - Content

> =========
> ContactInfo - for ordering
> =========
<!-- One user to many contactInfos stored -->
> - ContactInfoId
> - FirstName - can pre-fill with user info
> - LastName - can pre-fill with user info
> - Email - look into emailing capabilities
> - StreetAddress
> - SecondaryStreetAddress
> - City
> - Country
> - ZipCode
> - PostalCode

> =========
> Payment
> =========
<!-- One user to many payments stored -->
> - PaymentId
> - FirstName - can pre-fill with contact info
> - LastName - can pre-fill with contact info
> - ZipCode - can pre-fill with contact info
> - CreditCardNum
> - ExpirationDate
> - SecurityCode

> =========
> Order
> =========
<!-- One user to many orders -->
<!-- many orders to many coffees -->
> - OrderId - pk
> - UserId - fk
> - List<Coffee>


> =========
> Coffee
> =========
> - CoffeeId
> - Name
> - Price
> - ImageUrl
> - Region
> - Roast
> - Rating
>
>
>
>

<!-- ========= -->
<!-- tea - TBD-->
<!-- ========= -->