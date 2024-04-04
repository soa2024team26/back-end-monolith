INSERT INTO stakeholders."Users" ("Username", "Password", "Role", "IsActive", "Email", "Token")
VALUES
    ('milos', '473287f8298dba7163a897908958f7c0eae733e25d2e027992ea2edc9bed2fa8', 2, true, 'milosssdjuric@yahoo.com', 'b71a0a16-9599-4f4b-9f93-fcfef6d4ad20'),
    ('author', '473287f8298dba7163a897908958f7c0eae733e25d2e027992ea2edc9bed2fa8', 1, true, 'milosssdjuric34@gmail.com', 'a577c7b3-d64e-497c-811d-c3374f8b6de2'),
    ('admin', '473287f8298dba7163a897908958f7c0eae733e25d2e027992ea2edc9bed2fa8', 0, true, 'miloss.geru@gmail.com', 'a3b3bc98-f03b-4ff9-a2f3-6b0e57b62b01');

INSERT INTO stakeholders."Profiles" ("FirstName", "LastName", "ProfilePicture", "Biography", "Motto", "UserId", "IsActive", "Follows", "TourPreference", "QuestionnaireDone", "XP", "IsFirstPurchased", "NumberOfCompletedTours", "RequestSent")
VALUES 
  ('Mina', 'Lastname1', 'https://th.bing.com/th/id/OIG.ey_KYrwhZnirAkSgDhmg', 'A brief biography of Mina.', 'Motto for Mina', 1, true, '{"followers": [], "following": []}', '{"Tags": [], "CarRating": 1, "BoatRating": 1, "Difficulty": 1, "BicycleRating": 1, "WalkingRating": 1}', true, 5, false, 1, false),
  ('Iva', 'Lastname2', 'https://th.bing.com/th/id/OIG.ey_KYrwhZnirAkSgDhmg', 'A brief biography of Iva.', 'Motto for Iva', 2, true, '{"followers": [], "following": []}', '{"Tags": [], "CarRating": 1, "BoatRating": 1, "Difficulty": 1, "BicycleRating": 1, "WalkingRating": 1}', true, 3, false, 1, false),
  ('Mile', 'Lastname3', 'https://th.bing.com/th/id/OIG.ey_KYrwhZnirAkSgDhmg', 'A brief biography of Mile.', 'Motto for Mile', 3, true, '{"followers": [], "following": []}', '{"Tags": [], "CarRating": 1, "BoatRating": 1, "Difficulty": 1, "BicycleRating": 1, "WalkingRating": 1}', true, 2, false, 1, false);

INSERT INTO tours."CheckPoint" ("Latitude", "Longitude", "Name", "Description", "Image", "IsPublic")
VALUES
  (37.7749, -122.4194, 'City Park Entrance', 'Start your journey at the entrance of the city park.', 'https://www.industrialempathy.com/img/remote/ZiClJf-1920w.jpg', true),
  (37.7755, -122.4190, 'Botanical Garden', 'Explore the diverse plant life in the botanical garden.', 'https://www.industrialempathy.com/img/remote/ZiClJf-1920w.jpg', true),
  (37.7758, -122.4185, 'Sculpture Plaza', 'Admire unique sculptures in the scenic plaza.', 'https://www.industrialempathy.com/img/remote/ZiClJf-1920w.jpg', true),
  (37.7761, -122.4179, 'Fountain Square', 'Relax by the beautiful fountain in the center of the square.', 'https://www.industrialempathy.com/img/remote/ZiClJf-1920w.jpg', true),
  (37.7764, -122.4173, 'Café Corner', 'Take a break at a cozy café on the corner of the street.', 'https://www.industrialempathy.com/img/remote/ZiClJf-1920w.jpg', true),
  (37.7767, -122.4168, 'Historical Landmark', 'Learn about the citys history at this historical landmark.', 'https://www.industrialempathy.com/img/remote/ZiClJf-1920w.jpg', true),
  (37.7770, -122.4162, 'Art Gallery', 'Appreciate local art at the contemporary art gallery.', 'https://www.industrialempathy.com/img/remote/ZiClJf-1920w.jpg', true),
  (37.7773, -122.4157, 'Hidden Alleyway', 'Discover a charming hidden alleyway with street art.', 'https://www.industrialempathy.com/img/remote/ZiClJf-1920w.jpg', true),
  (37.7776, -122.4151, 'Rooftop Viewpoint', 'Enjoy a panoramic view of the city from this rooftop.', 'https://www.industrialempathy.com/img/remote/ZiClJf-1920w.jpg', true),
  (37.7779, -122.4146, 'City Square Finale', 'Reach the final checkpoint in the lively city square.', 'https://www.industrialempathy.com/img/remote/ZiClJf-1920w.jpg', true);


INSERT INTO tours."Object" ("Name", "Description", "Image", "Longitude", "Latitude", "IsPublic", "Category") 
VALUES
    ('Public Restroom at Central Park', 'Modern public restroom facility with accessible features', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR0Z_8qJz1C6JP0SzIzWWZgkg9w3IPYRfnZhg&usqp=CAU', 37.7749, -122.4124, true, 1),
    ('City Mall Restroom', 'Clean and well-maintained restroom in City Mall', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR0Z_8qJz1C6JP0SzIzWWZgkg9w3IPYRfnZhg&usqp=CAU', 37.7779, -122.4646, true, 0),
    ('Downtown Plaza WC', 'Convenient restroom in the heart of downtown', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR0Z_8qJz1C6JP0SzIzWWZgkg9w3IPYRfnZhg&usqp=CAU', 37.7771, -122.4936, true, 0),
    ('Gourmet Delights Bistro', 'A cozy restaurant offering gourmet delights', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ4zKuR01phE9NC8JsXP8TiTkpfnuVeCppMYg&usqp=CAU', 37.7779, -122.4846, true, 1),
    ('Seafood Sensations', 'Experience the finest seafood in a waterfront setting', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ4zKuR01phE9NC8JsXP8TiTkpfnuVeCppMYg&usqp=CAU', 37.7749, -122.4346, true, 1),
    ('Spice Paradise', 'Savor exotic flavors at Spice Paradise restaurant', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ4zKuR01phE9NC8JsXP8TiTkpfnuVeCppMYg&usqp=CAU', 37.7779, -122.4756, true, 1),
    ('Central Park Parking Garage', 'Secure and convenient parking near Central Park', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR4a_2ILTJlglpbH6LvmK2c3p8AbUlc-KWYNw&usqp=CAU', 37.7749, -122.4324, true, 2),
    ('City Mall Parking Lot', 'Ample parking space available at City Mall', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR4a_2ILTJlglpbH6LvmK2c3p8AbUlc-KWYNw&usqp=CAU', 37.7779, -122.4936, true, 2),
    ('Downtown Plaza Valet Parking', 'Valet service for hassle-free parking at Downtown Plaza', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR4a_2ILTJlglpbH6LvmK2c3p8AbUlc-KWYNw&usqp=CAU', 37.7769, -122.4946, true, 2),
    ('City Art Installation', 'Captivating public art installation in the city center', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQVtgwde-vuShwwABAvomrXDCWZ-PifJuJ42w&usqp=CAU', 37.7779, -122.4846, true, 3),
    ('Green Oasis Park', 'Tranquil park with lush greenery and scenic views', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQVtgwde-vuShwwABAvomrXDCWZ-PifJuJ42w&usqp=CAU', 37.7779, -122.4796, true, 3),
    ('City Viewpoint Tower', 'Observation tower offering panoramic views of the city', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQVtgwde-vuShwwABAvomrXDCWZ-PifJuJ42w&usqp=CAU', 37.7749, -124.2114, true, 3);

INSERT INTO tours."Equipment" ("Name", "Description")
VALUES
    ('Hiking Boots', 'Sturdy and comfortable boots for hiking in various terrains.'),
    ('Camping Tent', 'A spacious tent suitable for camping adventures.'),
    ('Bicycle', 'A versatile bicycle for cycling tours and exploration.'),
    ('Camera', 'High-quality camera for capturing memorable moments during tours.'),
    ('Binoculars', 'Optical instrument for a closer view of distant objects.'),
    ('Backpack', 'Durable backpack with ample storage for carrying essentials.'),
    ('Kayak', 'Lightweight and maneuverable kayak for water-based tours.'),
    ('Climbing Gear', 'Equipment for climbing enthusiasts for a safe and enjoyable experience.'),
    ('Fishing Rod', 'A fishing rod for those interested in combining fishing with tours.'),
    ('GPS Device', 'Navigation device to ensure you stay on the right path during tours.');



INSERT INTO tours."Tour" (
    "Equipment",
    "Checkpoints",
    "Objects",
    "FootTime",
    "BicycleTime",
    "CarTime",
    "TotalLength",
    "AuthorId",
    "PublishTime",
    "Name",
    "Description",
    "Status",
    "Difficulty",
    "Price",
    "Tags",
    "IsDeleted",
    "Image"
)
VALUES
    (ARRAY[1, 2, 3], ARRAY[1, 2, 3], ARRAY[1, 4, 7,10], 5.5, 3.0, 1.5, 15.8, 3, '2023-11-21 12:14:43'::timestamp with time zone, 'Nature Exploration Tour', 'A scenic tour through beautiful landscapes and forests.', 1, 2, 25.99, ARRAY['tag1', 'tag2'], false, 'image1'),
    (ARRAY[4, 5, 6], ARRAY[4, 5, 6], ARRAY[2, 5, 8,11], 3.0, 2.0, 1.2, 10.2, 3, '2023-11-22 10:31:09'::timestamp with time zone, 'Historical City Walk', 'Explore the rich history of the city through its landmarks and monuments.', 1, 1, 15.50, ARRAY['tag1', 'tag2'], false, 'image2'),
    (ARRAY[7, 8, 9, 10], ARRAY[7, 8, 9, 10], ARRAY[3, 6, 9,12], 4.0, 2.5, 1.0, 12.3, 3, '2023-11-23 09:49:29'::timestamp with time zone, 'Mountain Adventure Trek', 'A challenging trek through mountainous terrain with breathtaking views.', 1, 3, 35.75, ARRAY['tag1', 'tag2'], false, 'image3');
    
