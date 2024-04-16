# Problem Statement

For each trainer we collect two pieces of information per task [answerTime, correctness], where correctness is 1 if the answer was correct, -1 if the answer was wrong, and 0 if no answer was given. In this case, the bot's correct answer time for a given task would be the average of the answer times from the trainers who answered correctly. Given all of the training information for a specific task, calculate the bot's answer time.

Source: Code Signal

## Examples

1. For trainingData = [[3, 1],
                [6, 1],
                [4, 1],
                [5, 1]]
the output should be solution(trainingData) = 4.5.
All four trainers have solved the task correctly, so the answer is (3 + 6 + 4 + 5) / 4 = 4.5.

2. For trainingData = [[4, 1],
                [4, -1],
                [0, 0],
                [6, 1]]
the output should be solution(trainingData) = 5.0.
Only the 1st and the 4th trainers (1-based) submitted correct solutions, so the answer is (4 + 6) / 2 = 5.0.

3. For trainingData = [[4, -1],
                [0, 0],
                [5, -1]]
the output should be solution(trainingData) = 0.0.