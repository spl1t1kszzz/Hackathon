namespace Hackathon;

public class HmCounter
{
    public static double CountHm(List<int> numbers)
    {
        return numbers.Count / numbers.Sum(number => 1d / number);
    }
}