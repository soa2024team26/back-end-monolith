﻿INSERT INTO payments."OrderItems"(
	"Id", "ItemId", "ItemName", "Price", "ShoppingCartId", "IsBought", "IsBundle")
	VALUES (-1, -1, 'bundle1', 100, -1, false, true);
INSERT INTO payments."OrderItems"(
	"Id", "ItemId", "ItemName", "Price", "ShoppingCartId", "IsBought", "IsBundle")
	VALUES (-2, -2, 'bundle2', 200, -1, false, true);
INSERT INTO payments."OrderItems"(
	"Id", "ItemId", "ItemName", "Price", "ShoppingCartId", "IsBought", "IsBundle")
	VALUES (-3, -3, 'bundle3', 300, -1, false, true);

INSERT INTO payments."OrderItems"(
	"Id", "ItemId", "ItemName", "Price", "ShoppingCartId", "IsBought", "IsBundle")
	VALUES (-4, 1, 'tura1', 200, -3, true, false);
INSERT INTO payments."OrderItems"(
	"Id", "ItemId", "ItemName", "Price", "ShoppingCartId", "IsBought", "IsBundle")
	VALUES (-5, 2, 'tura2', 300, -3, true, false);