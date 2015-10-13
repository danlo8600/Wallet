namespace Wallet.dbManager
{
    class Product
    {
        public Product()
        {

        }

        private string name = null;

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName() {
            return name;
        }
    }
}