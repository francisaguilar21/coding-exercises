// Problem Source: LeetCode
// https://leetcode.com/problems/find-the-difference/

public class Solution {
    public char FindTheDifference(string s, string t) {
        foreach (char c in t) {
            if (t.Count(ch => ch == c) > s.Count(ch => ch == c)) {
                return c;
            }
        }

        return ' ';
    }
}