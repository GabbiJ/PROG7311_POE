# PROG7311 POE Part 2

The program **Agri-Energy Connect**  aims to be a web application that will be a platform in which farmers can upload their products that can be viewed by employees. 


# System Requirements

* Windows operating system (Windows 7 or a later version).
* Microsoft Visual Studio 2019 or a later version installed and must be able to run a ASP.Net Web Application using MVC.

# Installing and Starting the Program

## Installing the Program
1. Navigate to the GitHub repository linked at the bottom of this README file.
2. Download the zipped folder containing the program (or clone the repository onto your local machine)
3. Unzip the folder in the desired location on your local device.
## Starting the Program
To start the program the user must have Visual Studio installed on their machine and be able to run a ASP.Net Core web application.

1. Within the unzipped folder navigate to the solution file using the following file path as reference:
**POEPart2ST10034968\POEPart2ST10034968.sln**
2. Open the solution file in visual studio and click the green play button to run the file.
## Logging In
The application will start on the login page in which the user can input their username and password and choose whether they are logging in as an employee or a farmer. If they are correct the user will be allowed to view the home page of the application
## Registering a new account
If the user wishes to create a new account they can follow the following steps:
1. Navigate to the registration page via clicking the link that says "Click here if you don't have an account"
2. On the registration page they can enter the relative credentials and select if they want to register their account as a farmer or an employee. 
3. If the user enters valid credentials their account will be registered and they will be taken to the login page in order to login on their new account
## The Navigation Bar
The navigation bar resides at the top of the page at all times throughout use of the application. The buttons and text change depending on if the user is logged in or not.  
### When the User is Logged Out
When the user is logged out the navigation bar will have buttons that lead the user to the privacy page, register and login page for the web application.

### When the user is Logged In
When the user is logged in the navigation bar will change so that the register and login buttons will be replaced by text saying "Hello (user's first name) (user's surname)" along with logout button that will log out the user when clicked. Additionally a button that will take the user to the home page will also appear on the navigation bar.
## Employee Functions
Upon arriving on the home page 2 buttons will be presented to the employee. One will take the employee to the page to add a farmer user. The second takes the employee to the page to select a farmer in order to view their products. 
### Adding a farmer 
Upon clicking the "Add Farmer" button on home page the employee will be taken to a page where they can add a farmer to the database. To add a farmer the employee can follow the following steps:
1. Enter the details relating to the farmer they wish to add. 
2. Click the submit to add the user to the database. 
3. If the employee enters valid credentials they will be taken back to the home page and the farmer user will be added to the database.
### Viewing Products by a Farmer
Upon clicking the "View Products by a Farmer " button the user will be taken to a page with a list of buttons with each farmer's name, surname and username on it. To view the products that a farmer has uploaded onto the web application the employee can follow the following steps:
1. Click the button with the desired farmer's details on it. 
2. Once the button is clicked the employee will be taken to a page with a table that has the details of all the products that the farmer has uploaded in it.
#### Filtering Farmer Products
The employee can either filter the farmers products by either a date range or by product type.
##### Filter by Date Range
To filter by date range the employee can follow the following steps:
1. Enter a start date and end date and then click the "Filter by Date Range" button.
2. Once the button is clicked only the products that have a production date between the start and end dates entered will be displayed.
##### Filter by Product Type
To filter by product type the employee can follow the following steps:
1. Select a product type to filter the product by then click the "Filter by Product Type" button.
2. Once the button is clicked only the products are the selected product type will be displayed.

## Farmer Functions
Upon arriving on the home page the farmer will be presented the button that will take them to the page to add a product to the database.
### Adding a product
Once the "Add Product" button on the home page is clicked the farmer will be taken to a page to in which they can add a product. To add a product to the database the farmer can follow the following steps:
1. Enter the details relating to the product they wish to add. 
2. Click the submit to add the product to the database. 
3. If the information entered is valid, the farmer will be taken back to the home page and the product will be added to the database.
# Link to GitHub Repository
[GitHub Repository]()

