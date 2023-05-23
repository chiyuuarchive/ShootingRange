using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MergeSorter : MonoBehaviour
{
    public static List<Score> MergeSort(List<Score> list)
    {
        // If the list has only one element, it doesn't need to be sorted
        if (list.Count <= 1)
        {
            return list;
        }
        // Split the list into two halves
        int middleIndex = list.Count / 2;
        List<Score> leftList = new List<Score>(list.GetRange(0, middleIndex));
        List<Score> rightList = new List<Score>(list.GetRange(middleIndex, list.Count - middleIndex));

        // Recursively sort the two halves
        leftList = MergeSort(leftList);
        rightList = MergeSort(rightList);

        // Merge the two sorted halves
        return Merge(leftList, rightList);
    }

    private static List<Score> Merge(List<Score> leftList, List<Score> rightList)
    {
        int leftIndex = 0;
        int rightIndex = 0;
        int resultIndex = 0;
        List<Score> result = new List<Score>(leftList.Count + rightList.Count);

        // Compare elements from left and right lists and merge them into the result list
        while (leftIndex < leftList.Count && rightIndex < rightList.Count)
        {
            if (leftList[leftIndex].score >= rightList[rightIndex].score)
            {
                result.Add(leftList[leftIndex]);
                leftIndex++;
            }
            else
            {
                result.Add(rightList[rightIndex]);
                rightIndex++;
            }
            resultIndex++;
        }

        // Copy any remaining elements from the left list
        while (leftIndex < leftList.Count)
        {
            result.Add(leftList[leftIndex]);
            leftIndex++;
            resultIndex++;
        }

        // Copy any remaining elements from the right list
        while (rightIndex < rightList.Count)
        {
            result.Add(rightList[rightIndex]);
            rightIndex++;
            resultIndex++;
        }

        return result;
    }
}
