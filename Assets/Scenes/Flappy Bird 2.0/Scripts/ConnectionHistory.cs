using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//complete
public class ConnectionHistory
{
    public int fromNum;
    public int toNum;
    public int innovationNo;
    public List<int> innoNumbers;

    public ConnectionHistory(int from, int to, int inno, List<int> innoArr)
    {
        fromNum = from;
        toNum = to;
        innovationNo = inno;
        innoNumbers = new List<int>(innoArr);
    }

    public bool Matches(Genome genome, Node fromNode, Node toNode)
    {
        //whether or not the history matches the exact genome (see for loop)
        if (genome.genes.Count == innoNumbers.Count)
        {
            //whether the connection is the same
            if (fromNode.number == fromNum && toNode.number == toNum)
            {
                for (int i = 0; i < genome.genes.Count; i++)
                {
                    if (!Contains(innoNumbers, genome.genes[i].innovationNo))
                    {
                        return false;
                    }
                }

                //innoNumbers match up with gene innovation numbers and have the same connection path
                return true;
            }
        }

        return false;
    }

    //used in Matches method
    public bool Contains(List<int> arr, int value)
    {
        for (int i = 0; i < arr.Count; i++)
        {
            if (arr[i] == value)
            {
                return true;
            }
        }

        return false;
    }
}
