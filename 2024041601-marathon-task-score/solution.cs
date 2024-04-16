int solution(int marathonLength, int maxScore, int submissions, int successfulSubmissionTime) {
    
    if (successfulSubmissionTime < 0)
    {
        return 0;
    }

    // If you solve a task on your first attempt within the first minute, you get maxScore points.
    if (submissions == 1 && successfulSubmissionTime <= 1)
    {
        return maxScore;
    }

    var initialScore = getScoreOnSubmissionTime(marathonLength, maxScore, successfulSubmissionTime);    
    var unsuccessfulScore = getUnsuccessfulAttemptScore(submissions, maxScore);
    
    var score = maxScore - initialScore - unsuccessfulScore;
    
    if (score < (maxScore / 2))
    {
        return maxScore / 2;
    }
    else
    {
        return score;
    }
}

int getScoreOnSubmissionTime(int marathonLength, int maxScore, int successfulSubmissionTime)
{
    decimal a = successfulSubmissionTime;
    decimal b = maxScore / 2;
    decimal c = (decimal) 1 / marathonLength;
    return (int) (a * b * c);
}

int getUnsuccessfulAttemptScore(int submissions, int maxScore)
{
    return 10 * (submissions -1);
}
