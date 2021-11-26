using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//complete
public class ConnectionGene
{
    public bool geneEnabled = true;

    public Node fromNode;
    public Node toNode;
    public float weight;
    public int innovationNo;

    public ConnectionGene(Node from, Node to, float nodeWeight, int inno)
    {
        fromNode = from;
        toNode = to;
        weight = nodeWeight;
        innovationNo = inno;
    }

    public void MutateWeight()
    {
        //completely change the weight 10% of the time
        if (Random.value < 0.1f)
        {
            weight = Random.Range(-1f, 1f);
        }
        //otherwise, slightly change the value and clamp to (-1, 1)
        else
        {
            weight += Random.Range(-1f, 1f) / 50;
            weight = Mathf.Clamp(weight, -1f, 1f);
        }
    }

    //copy gene values into the next gene
    public ConnectionGene Clone(Node fromNode, Node toNode)
    {
        //parameters are necessary to properly assign nodes from a parent to child
        ConnectionGene copy = new ConnectionGene(fromNode, toNode, this.weight, this.innovationNo);
        copy.geneEnabled = this.geneEnabled;
        return copy;
    }
}
