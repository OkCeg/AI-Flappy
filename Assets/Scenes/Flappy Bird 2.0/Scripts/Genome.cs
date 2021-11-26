using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genome
{
    public List<ConnectionGene> genes = new List<ConnectionGene>();
    public List<Node> nodes = new List<Node>();
    public int layers = 2;
    public int nextNodeNum = 0;
    public int biasNodeNum = 0;

    //nodes in order of the neural network
    public List<Node> network = new List<Node>();

    public int inputs;
    public int outputs;


    public Genome(int inputNum, int outputNum, bool crossover)
    {
        inputs = inputNum;
        outputs = outputNum;

        //don't do anything else in constructor if crossover genome
        if (crossover)
        {
            return;
        }

        //nodes 0 to inputs - 1
        for (int i = 0; i < inputs; i++)
        {
            nodes.Add(new Node(i));
            nodes[i].layer = 0;
            nextNodeNum++;
        }

        //nodes inputs to inputs + outputs - 1
        for (int i = inputs; i < inputs + outputs; i++)
        {
            nodes.Add(new Node(i));
            nodes[i].layer = 1;
            nextNodeNum++;
        }

        //bias node creation
        nodes.Add(new Node(nextNodeNum));
        biasNodeNum = nextNodeNum;
        nextNodeNum++;
        nodes[biasNodeNum].layer = 0;
    }

    public Node GetNode(int nodeNum)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].number == nodeNum)
            {
                return nodes[i];
            }
        }

        return null;
    }

    public void ConnectNodes()
    {
        //clear all connections
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].outputConnections.Clear();
        }

        //reassign connections
        for (int i = 0; i < genes.Count; i++)
        {
            genes[i].fromNode.outputConnections.Add(genes[i]);
        }
    }

    public List<float> FeedForward(float[] visionValues)
    {
        //input values into input nodes
        for (int i = 0; i < inputs; i++)
        {
            nodes[i].outputValue = visionValues[i];
        }

        //bias is 1 (value sent changed by connection gene)
        nodes[biasNodeNum].outputValue = 1;

        //engage all network nodes
        for (int i = 0; i < network.Count; i++)
        {
            network[i].Engage();
        }

        //return the outputs (only 1 in this program)
        List<float> returnValues = new List<float>();

        for (int i = 0; i < outputs; i++)
        {
            returnValues.Add(nodes[inputs + i].outputValue);
        }

        //reset values for next time
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].inputSum = 0;
        }

        return returnValues;
    }

    public void GenerateNetwork()
    {
        ConnectNodes();

        network.Clear();

        //add nodes in order of layers
        for (int i = 0; i < layers; i++)
        {
            for (int j = 0; j < nodes.Count; j++)
            {
                if (nodes[j].layer == i)
                {
                    network.Add(nodes[j]);
                }
            }
        }
    }

    public int GetInnovationNumber(List<ConnectionHistory> history, Node fromNode, Node toNode)
    {
        //test if the mutation has been seen before
        bool isNew = true;
        int connectionNumber = All.NextConnectionNum;

        for (int i = 0; i < history.Count; i++)
        {
            //asks if the mutation has been seen before in the history
            if (history[i].Matches(this, fromNode, toNode))
            {
                isNew = false;
                connectionNumber = history[i].innovationNo;

                //stop the loop once found
                break;
            }
        }

        if (isNew)
        {
            List<int> newInnoNums = new List<int>();

            for (int i = 0; i < genes.Count; i++)
            {
                newInnoNums.Add(genes[i].innovationNo);
            }

            history.Add(new ConnectionHistory(fromNode.number, toNode.number, connectionNumber, newInnoNums));
            All.NextConnectionNum++;
        }

        return connectionNumber;
    }

    public Genome Clone()
    {
        //nodes will be assigned, so crossover is set to true
        Genome copy = new Genome(inputs, outputs, true);

        //copy nodes
        for (int i = 0; i < nodes.Count; i++)
        {
            copy.nodes.Add(nodes[i].Clone());
        }

        //copy genes
        for (int i = 0; i < genes.Count; i++)
        {
            copy.genes.Add(genes[i].Clone(copy.GetNode(genes[i].fromNode.number), Clone().GetNode(genes[i].toNode.number)));
        }

        //copy layers and node numbers
        copy.layers = layers;
        copy.nextNodeNum = nextNodeNum;
        copy.biasNodeNum = biasNodeNum;

        copy.ConnectNodes();

        return copy;
    }

    //Mutation time (yay)------------------------------------------------------------------------------------------------------------------------

    //fully connected networks cannot have further connections
    public bool FullyConnected()
    {
        int maxPossibleConnections = 0;
        List<int> nodesInLayers = new List<int>();

        //populate the list of the nodes in layers
        for (int i = 0; i < layers; i++)
        {
            nodesInLayers.Add(0);
        }

        //assign the proper values to the list
        for (int i = 0; i < nodes.Count; i++)
        {
            nodesInLayers[nodes[i].layer]++;
        }

        //multiply the number of nodes in each layer by the number of nodes that occurs in a higher layer
        for (int i = 0; i < layers - 1; i++)
        {
            int nodesHigher = 0;

            for (int j = i + 1; j < layers; j++)
            {
                nodesHigher += nodesInLayers[j];
            }

            maxPossibleConnections += nodesInLayers[i] * nodesHigher;
        }

        //full if the gene count is equal (or somehow greater) than maximum possible connections
        return genes.Count >= maxPossibleConnections;
    }

    //for AddConnection method (checks if nodes are valid for a connection)
    public bool NotValidConnection(int num1, int num2)
    {
        return nodes[num1].layer == nodes[num2].layer || nodes[num1].IsConnectedTo(nodes[num2]);
    }

    public void AddConnection(List<ConnectionHistory> history)
    {
        //cannot add connection for fully connected network
        if (FullyConnected())
        {
            return;
        }

        int randomNodeNum1 = Random.Range(0, nodes.Count);
        int randomNodeNum2 = Random.Range(0, nodes.Count);

        while (NotValidConnection(randomNodeNum1, randomNodeNum2))
        {
            randomNodeNum1 = Random.Range(0, nodes.Count);
            randomNodeNum2 = Random.Range(0, nodes.Count);
        }

        //make sure nodeNum1 layer is less than nodeNum2 layer
        int temp = 0;
        if (nodes[randomNodeNum1].layer > nodes[randomNodeNum2].layer)
        {
            temp = randomNodeNum2;
            randomNodeNum2 = randomNodeNum1;
            randomNodeNum1 = temp;
        }

        int connectionInnoNum = GetInnovationNumber(history, nodes[randomNodeNum1], nodes[randomNodeNum2]);
        genes.Add(new ConnectionGene(nodes[randomNodeNum1], nodes[randomNodeNum2], Random.Range(-1f, 1f), connectionInnoNum));

        ConnectNodes();
    }

    public void AddNode(List<ConnectionHistory> history)
    {
        //there must be a connection for a node to fit in between
        if (genes.Count == 0)
        {
            AddConnection(history);
            return;
        }

        //select connection to be replaced
        int randomConnection = Random.Range(0, genes.Count);

        //the bias node cannot be selected (except when there is no other gene)
        while (genes[randomConnection].fromNode == nodes[biasNodeNum] && genes.Count != 1)
        {
            randomConnection = Random.Range(0, genes.Count);
        }

        //disable it to make room for node in between
        genes[randomConnection].geneEnabled = false;

        //add the node
        int newConnectionNodeNum = nextNodeNum;
        nodes.Add(new Node(newConnectionNodeNum));
        nextNodeNum++;

        //add connection to node with weight of 1
        int newInnoNum = GetInnovationNumber(history, genes[randomConnection].fromNode,
            GetNode(newConnectionNodeNum));

        genes.Add(new ConnectionGene(genes[randomConnection].fromNode, GetNode(newConnectionNodeNum),
            1, newInnoNum));

        //add connection from node with the same weight before the connection disable
        newInnoNum = GetInnovationNumber(history, GetNode(newConnectionNodeNum),
            genes[randomConnection].toNode);

        genes.Add(new ConnectionGene(GetNode(newConnectionNodeNum), genes[randomConnection].toNode,
            genes[randomConnection].weight, newInnoNum));

        //assign a higher layer to the new node (everything else will be moved later in this method)
        GetNode(newConnectionNodeNum).layer = genes[randomConnection].fromNode.layer + 1;

        //connect the bias node to the new node with weight 0 (for further mutations)
        newInnoNum = GetInnovationNumber(history, nodes[biasNodeNum], GetNode(newConnectionNodeNum));

        genes.Add(new ConnectionGene(nodes[biasNodeNum], GetNode(newConnectionNodeNum), 0, newInnoNum));

        //adjust all layers (unless a layer in the proper section has been created previously)
        if (GetNode(newConnectionNodeNum).layer == genes[randomConnection].toNode.layer)
        {
            //everything but new node
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                //only move if the layer is higher than the new node layer
                if (nodes[i].layer >= GetNode(newConnectionNodeNum).layer)
                {
                    nodes[i].layer++;
                }
            }

            layers++;
        }

        ConnectNodes();
    }

    public void Mutate(List<ConnectionHistory> history)
    {
        //add a connection if there is none
        if (genes.Count == 0)
        {
            AddConnection(history);
        }

        //weight mutation chance: 80%
        if (Random.value < 0.8f)
        {
            for (int i = 0; i < genes.Count; i++)
            {
                genes[i].MutateWeight();
            }
        }

        //new connection mutation chance: 5%
        if (Random.value < 0.05f)
        {
            AddConnection(history);
        }

        //new node mutation chance: 1%
        if (Random.value < 0.01f)
        {
            AddNode(history);
        }
    }

    //Crossover time (yay)------------------------------------------------------------------------------------------------------------------------

    //whether or not a gene has a matching input innovation number
    public int MatchingGene(Genome parent2, int innovationNum)
    {
        for (int i = 0; i < parent2.genes.Count; i++)
        {
            if (parent2.genes[i].innovationNo == innovationNum)
            {
                return i;
            }
        }

        return -1;
    }

    //crossover is called when this genome is better than the other parent
    public Genome Crossover(Genome parent2)
    {
        Genome child = new Genome(inputs, outputs, true);

        child.layers = layers;
        child.nextNodeNum = nextNodeNum;
        child.biasNodeNum = biasNodeNum;

        //to clone from later
        List<ConnectionGene> childGenes = new List<ConnectionGene>();

        //whether or not each gene is enabled or not
        List<bool> enabledList = new List<bool>();

        //inherit genes
        for (int i = 0; i < genes.Count; i++)
        {
            //changes enable state throughout this loop
            bool setEnabled = true;

            int parent2GeneNum = MatchingGene(parent2, genes[i].innovationNo);

            //if genes have had a common history
            if (parent2GeneNum != -1)
            {
                //if either of the two genes are disabled
                if (!genes[i].geneEnabled || !parent2.genes[i].geneEnabled)
                {
                    //disable gene chance: 75%
                    if (Random.value < 0.75)
                    {
                        setEnabled = false;
                    }
                }

                //50-50 to get gene from either parent (since they share the same gene)
                if (Random.value < 0.5)
                {
                    //values will be copied later (no need to worry about reference allocations)
                    childGenes.Add(genes[i]);
                }
                else
                {
                    childGenes.Add(parent2.genes[parent2GeneNum]);
                }
            }
            //disjoint or excess gene (no match)
            else
            {
                childGenes.Add(genes[i]);
                setEnabled = genes[i].geneEnabled;
            }

            enabledList.Add(setEnabled);
        }

        //structure is same as this parent
        for (int i = 0; i < nodes.Count; i++)
        {
            child.nodes.Add(nodes[i].Clone());
        }

        //add the connections
        for (int i = 0; i < childGenes.Count; i++)
        {
            child.genes.Add(childGenes[i].Clone(child.GetNode(childGenes[i].fromNode.number),
                child.GetNode(childGenes[i].toNode.number)));

            child.genes[i].geneEnabled = enabledList[i];
        }

        child.ConnectNodes();

        //new baby (pog)
        return child;
    }
}
