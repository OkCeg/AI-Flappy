using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//complete
public class Node
{
    public float inputSum = 0;
    public float outputValue = 0;
    public List<ConnectionGene> outputConnections = new List<ConnectionGene>();
    public int layer = 0;

    public int number;

    public Node(int no)
    {
        number = no;
    }

    //sends outputs to the inputs of connected nodes
    public void Engage()
    {
        //no sigmoid for non-output nodes
        if (layer != 0)
        {
            outputValue = Sigmoid(inputSum);
        }

        for (int i = 0; i < outputConnections.Count; i++)
        {
            if (outputConnections[i].geneEnabled)
            {
                outputConnections[i].toNode.inputSum += outputConnections[i].weight * outputValue;
            }
        }
    }

    public float Sigmoid(float value)
    {
        //4.9 to have higher chance to flap in Player class
        return 1.0f / (1.0f + Mathf.Pow(Mathf.Exp(1), -4.9f * value));
    }

    public bool IsConnectedTo(Node node2)
    {
        //nodes in the same layer cannot be connected
        if (node2.layer == layer)
        {
            return false;
        }

        //check if any of node2's outputs are connected to this node
        if (node2.layer < layer)
        {
            for (int i = 0; i < node2.outputConnections.Count; i++)
            {
                if (node2.outputConnections[i].toNode == this)
                {
                    return true;
                }
            }
        }
        //check if any of this node's outputs are connected to node2
        else
        {
            for (int i = 0; i < outputConnections.Count; i++)
            {
                if (outputConnections[i].toNode == node2)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public Node Clone()
    {
        Node copy = new Node(number);
        copy.layer = layer;
        return copy;
    }
}
