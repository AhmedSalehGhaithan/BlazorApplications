namespace DependencyLibrary
{
    public class DemoDependency: IDependencyInBlazer
    {
        public string GiveRandomNumber()
        {
            int randomNumber = Random.Shared.Next(1,100);
            return $"The random number is {randomNumber}";
        }

    }
}
