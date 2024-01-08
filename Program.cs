using GelberGroupTakeHome;
public class Program
{
    /// <summary>
    /// Runner for the program 
    /// </summary>
    public static void Main(string[] args)
    {
        Console.WriteLine("Please enter a file location: ");

        string testFileLocation = Console.ReadLine();

        TrainFileReader fileReader = new TrainFileReader(testFileLocation);
        try
        {
            TrainController controller = fileReader.GetTrainController();
            List<Passenger> passengers = fileReader.GetPassengers();

            List<Passenger> result = controller.Simulate(passengers);

            foreach (Passenger pas in result)
            {
                Console.WriteLine(pas.ToString());
            }
        } catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}

