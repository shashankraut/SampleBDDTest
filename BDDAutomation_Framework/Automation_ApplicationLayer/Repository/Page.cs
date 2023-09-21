using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_ApplicationLayer.Repository
{
    public class Page
    {
        public static LoginPage LoginPage
        {
            get
            {
                return new LoginPage();
            }
        }

        public static InventoryPage InventoryPage
        {
            get
            {
                return new InventoryPage();
            }
        }

        public static ShoppingCartPage ShoppingCartPage
        {
            get
            {
                return new ShoppingCartPage();

            }
        }

        public static CheckoutYourInformationPage CheckoutStepOnePage
        {
            get
            {
                return new CheckoutYourInformationPage();
            }
        }

        public static CheckoutOverviewPage CheckoutStepTwoPage
        {
            get
            {
                return new CheckoutOverviewPage();
            }
        }
    }
}
