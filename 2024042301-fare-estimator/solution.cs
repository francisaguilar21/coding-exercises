// Source of Problem: https://app.codesignal.com/company-challenges/uber/HNQwGHfKAoYsz9KX6

double[] solution(int ride_time, int ride_distance, double[] cost_per_minute, double[] cost_per_mile) {
    // Cost Per Ride: (Cost per minute) * (ride time) + (Cost per mile) * (ride distance)
    // Cost per minute and Cost per mile depend on the car type.
    
    var fareEstimates = new double[cost_per_minute.Length];
    
    for (int index = 0; index <= cost_per_minute.Length - 1; index++)
    {
        fareEstimates[index] = computeFare(ride_time, ride_distance, cost_per_minute[index], cost_per_mile[index]);
    }

    return fareEstimates;
}

double computeFare(int ride_time, int ride_distance, double cost_per_minute, double cost_per_mile)
{
    return ride_time * cost_per_minute + cost_per_mile * ride_distance;
}