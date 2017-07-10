using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using System.Text.RegularExpressions;
using FlooringMastery.BLL;

namespace FlooringMastery
{
    class ConsoleIO
    {

        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }

        public static void DisplayProducts(List<Product> products)
        {
            var allProducts = products;

            foreach (var product in allProducts)
            {
                Console.WriteLine(product.ProductType);
            }
        }

        public static void DisplayOrderDetails(Order order, DateTime dateInput)
        {
            Console.WriteLine("*******************************************************************");
            Console.WriteLine($"OrderNumber: {order.OrderNumber} | Order Date: {dateInput.ToShortDateString()}");
            Console.WriteLine($"Customer Name: {order.CustomerName}");
            Console.WriteLine($"State: {order.State}");
            Console.WriteLine($"Product: {order.ProductType}");
            Console.WriteLine($"Materials: {order.MaterialCost:c}");
            Console.WriteLine($"Labor: {order.LaborCost:c}");
            Console.WriteLine($"Tax: {order.Tax:c}");
            Console.WriteLine($"Total: {order.Total:c}");
            Console.WriteLine("*******************************************************************");
        }

        public static int GetOrder(DateTime inputDate)
        {
            bool validOrderChange = false;
            int orderNumber;
            do
            {
                Console.WriteLine($"Enter an order number to edit for {inputDate}.");
                Console.Write("Order Number: ");

                string orderNumberInput = Console.ReadLine();

                if (int.TryParse(orderNumberInput, out orderNumber))
                {
                    Console.Clear();
                    validOrderChange = true;
                    Console.WriteLine("*********************************************");
                    ConsoleIO.DisplayMessage($"Success.  You will be editing order number {orderNumber}.");
                    Console.WriteLine("*********************************************");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    ConsoleIO.DisplayMessage($"Error handling request.  Invalid input.");
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }
            } while (validOrderChange == false);
            return orderNumber;
        }

        public static void removeConfirmation()
        {
            bool confirmation = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Remove Order");
                Console.WriteLine("*********************************");
                Console.WriteLine("Confirm removal?");
                Console.WriteLine("Y for yes, N for no.");
                var confirmInput = Console.ReadLine().ToUpper();

                if (string.IsNullOrEmpty(confirmInput) == true || string.IsNullOrWhiteSpace(confirmInput) == true)
                {
                    ConsoleIO.DisplayMessage("Input cannot be empty!");
                }
                else if (confirmInput == "Y")
                {
                    confirmation = true;
                    ConsoleIO.DisplayMessage("Removal confirmed...");
                    ConsoleIO.DisplayMessage("Order removed.");
                }
                else if (confirmInput == "N")
                {
                    confirmation = true;
                    ConsoleIO.DisplayMessage("Order removal stopped.");
                    Menu.Execute();
                }
                else
                {
                    ConsoleIO.DisplayMessage("Please enter Y or N");
                }

            } while (confirmation == false);
        }

        internal static decimal EditDecimal(decimal area)
        {
            Console.WriteLine("New Area");
            Console.Write("Area : ");
            string editArea = Console.ReadLine();
            decimal newArea = 0m;
            if (decimal.TryParse(editArea, out newArea))
            {
                return newArea;
            }
            else
            {
                return area;
            }
        }


        internal static DateTime DisplayDate()
        {
            bool validDate = false;
            DateTime dateInput;
            do
            {
                Console.WriteLine("Display Orders");
                Console.WriteLine("*********************************");
                Console.WriteLine("Enter a date, ex: 01/01/2017 or 01-01-2017");
                Console.Write("Date: ");
                string input = Console.ReadLine();

                if (DateTime.TryParse(input, out dateInput))
                {
                    validDate = true;
                    Console.WriteLine($"Searching database for orders on {dateInput.ToShortDateString()}.");
                }
                else
                {
                    ConsoleIO.DisplayMessage("Input is not a valid date!");
                    Menu.Execute();
                }

            } while (validDate == false);
            return dateInput;
        }

        public static void DisplayStates(List<State> states)
        {
            var allStates = states;

            foreach (var state in allStates)
            {
                Console.WriteLine(state.StateName);
            }
        }

        public static string GetProduct()
        {
            bool validProductType = false;
            string product = "";

            ProductManager productmanager = ProductManagerFactory.Create();

            do
            {
                Console.WriteLine("Please select a product.");
                Console.Write("Product Type: ");
                string productInput = Console.ReadLine().ToUpper();
                var selectedProduct = productmanager.GetProductInfo(productInput);

                if (string.IsNullOrEmpty(productInput))
                {
                    validProductType = true;
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    ConsoleIO.DisplayMessage($"Product empty.");
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }
                else
                {
                    if (selectedProduct == null)
                    {
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        ConsoleIO.DisplayMessage($"Error handling request.  Unavailable product.");
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                    else
                    {
                        if (productmanager.ProductExistsInFile(selectedProduct.ProductType))
                        {
                            product = productInput;
                            Console.WriteLine("*********************************************");
                            ConsoleIO.DisplayMessage($"You have selected {selectedProduct.ProductType}");
                            Console.WriteLine("*********************************************");
                            validProductType = true;
                        }
                        else
                        {
                            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                            ConsoleIO.DisplayMessage($"Error handling request.  Invalid input.");
                            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        }
                    }
                }
            } while (validProductType == false);
            return product;
        }

        internal static string Confirmation()
        {
            bool confirmation = false;
            string input = "";
            do
            {
                Console.WriteLine("Place the order?  Type (Y)es or (N)o ");
                string orderInput = Console.ReadLine().ToUpper();
                if (string.IsNullOrEmpty(orderInput))
                {
                    ConsoleIO.DisplayMessage("Input cannot be empty.");
                }
                else
                {
                    if (orderInput == "Y")
                    {
                        input = orderInput;
                        confirmation = true;
                        ConsoleIO.DisplayMessage("Order confirmed, saving order to file...");
                    }
                    else if (orderInput == "N")
                    {
                        confirmation = true;
                        ConsoleIO.DisplayMessage("Order placement aborted, returning to main menu.");
                        Menu.Execute();
                    }
                    else
                    {
                        ConsoleIO.DisplayMessage("You must enter Y or N");
                    }
                }
            } while (confirmation == false);
            return input;
        }

        internal static decimal GetDecimal()
        {
            decimal area = 0m;
            string areaInput;
            bool validArea = false;
            do
            {
                Console.WriteLine("Please enter the amount of floorspace you wish to cover. ex: 100.00 210.25 340.90");
                Console.Write("Area: ");
                areaInput = Console.ReadLine();

                if (string.IsNullOrEmpty(areaInput))
                {
                    validArea = true;
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    ConsoleIO.DisplayMessage($"Empty input.");
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }
                else
                {
                    if (decimal.TryParse(areaInput, out area) && decimal.Parse(areaInput) > 100)
                    {
                        validArea = true;
                        Console.WriteLine("*********************************************");
                        ConsoleIO.DisplayMessage($"The area being covered: {areaInput}");
                        Console.WriteLine("*********************************************");
                    }
                    else if (decimal.TryParse(areaInput, out area) && decimal.Parse(areaInput) <= 100)
                    {
                        ConsoleIO.DisplayMessage($"Area must be greater than 100.  You chose {areaInput}.  Please add a minimum of {101 - area} sqft.");
                    }
                    else
                    {
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        ConsoleIO.DisplayMessage($"Error handling request.  Invalid number.");
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                }
            } while (validArea == false);
            return area;
        }

        public static string GetState()
        {
            //Get order state, do not let user pass while the stateinput is invalid
            bool stateInputValid = false;
            string state = "";
            TaxManager taxmanager = TaxManagerFactory.Create(); 
            do
            {
                Console.Clear();
                Console.WriteLine("*********************************");
                Console.WriteLine("Add an Order");
                Console.WriteLine("*********************************");
                Console.WriteLine("Please select the state the work is to be done in.");
                Console.WriteLine("Note that a state not on the list is ineligible for service.");

                var allStatesAvailable = taxmanager.GetAllStates();

                ConsoleIO.DisplayStates(allStatesAvailable);

                string customerStateInput = Console.ReadLine().ToUpper();

                if (string.IsNullOrEmpty(customerStateInput) == true)
                {
                    stateInputValid = true;
                    ConsoleIO.DisplayMessage("Empty input.");
                }
                else
                {
                    var selectedState = taxmanager.GetStateTaxInfo(customerStateInput);

                    if (selectedState == null)
                    {
                        ConsoleIO.DisplayMessage($"{customerStateInput} is not a state we are authorized to work in.");
                    }
                    else
                    {
                        if (taxmanager.StateExistsInFile(selectedState.StateName) == true)
                        {
                            state = customerStateInput;
                            ConsoleIO.DisplayMessage($"Work will be done in {selectedState.StateName}.");
                            stateInputValid = true;
                            
                        }
                        else
                        {
                            ConsoleIO.DisplayMessage($"{customerStateInput} is not a state we are authorized to work in.");
                        }
                    }
                }

            } while (stateInputValid == false);
            return state;
        }


        internal static DateTime GetDate()
        {
            DateTime inputDate;
            DateTime currentDate = DateTime.Now;
            bool validDate = false;
            // Grab the date from user
            do
            {
                Console.WriteLine("Enter a date, ex: 01/01/2017 or 01-01-2017");
                Console.Write("Date: ");
                string input = Console.ReadLine();

                if (DateTime.TryParse(input, out inputDate))
                {
                    if (currentDate.Date > inputDate.Date)
                    {
                        Console.Clear();
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        ConsoleIO.DisplayMessage($"Error handling request.  Must be a future date.");
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                    else if (currentDate.Date == inputDate.Date)
                    {
                        Console.Clear();
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        ConsoleIO.DisplayMessage($"Error handling request.  Cannot current date.");
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                    else
                    {
                        validDate = true;
                        Console.WriteLine("*********************************************");
                        ConsoleIO.DisplayMessage($"Order selected for {inputDate.ToShortDateString()}.");
                        Console.WriteLine("*********************************************");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    ConsoleIO.DisplayMessage($"Error handling request.  Invalid input.");
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }
            } while (validDate == false);

            return inputDate;
        }

        internal static string GetName()
        {

            bool validName = false;
            string nameInput;
            do
            {
                Console.WriteLine("Enter a name, <a-z, A-Z, 0-9>, Spaces & Commas allowed.");
                Console.Write("Name: ");
                nameInput = Console.ReadLine();
                var regexItem = new Regex("^[a-zA-Z0-9 .]*$");
                if (string.IsNullOrEmpty(nameInput))

                {
                    validName = true;
                    Console.Clear();
                    ConsoleIO.DisplayMessage($"Empty Name.");
                }
                else if (!regexItem.IsMatch(nameInput))
                {
                    Console.Clear();
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    ConsoleIO.DisplayMessage($"Error handling request.  Invalid characters detected.");
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }
                else
                {
                    validName = true;
                    Console.WriteLine("*********************************************");
                    ConsoleIO.DisplayMessage($"Customer name set to {nameInput}.");
                    Console.WriteLine("*********************************************");
                }
            } while (validName == false);
            return nameInput;
        }
    }
}
