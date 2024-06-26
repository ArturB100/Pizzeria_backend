namespace Pizzeria.Model
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstLine { get; set; }
        public string City { get; set; }
        public string SecondLine { get; set; }
        public string Zipcode { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is Address other)
            {
                return FirstLine == other.FirstLine &&
                    City == other.City &&
                    SecondLine == other.SecondLine &&
                    Zipcode == other.Zipcode;
            }
            return false;
        }

    }
}
