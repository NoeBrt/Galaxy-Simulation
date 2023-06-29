using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode
{
    public List<Star> bodiesStored;
    private int maxObjectCount;
    public TreeNode[] childs;
    private float bound;
    private Vector3 position;

    private List<Star> returnedObjects;
    private List<Star> cellObjects;
    private float Mass { get; set; }
    private Vector3 CenterOfMass { get; set; }



    // Start is called before the first frame update
    public TreeNode(int maxSize, Vector3 position, float bound)
    {
        this.bound = bound;
        maxObjectCount = maxSize;
        this.position = position;
        childs = new TreeNode[8];
        bodiesStored = new List<Star>(maxSize);
    }
    public void InsertToNode(Star star)
    {
        if (childs[0] != null)
        {
            int indexChild = GetIndexToInsertObject(star.transform.position);
            if (indexChild > -1)
            {
                childs[indexChild].InsertToNode(star);
            }
            return;
        }

        bodiesStored.Add(star);

        if (bodiesStored.Count > maxObjectCount)
        {
            if (childs[0] == null)
            {
                float subBound = bound / 2f;
                float x = position.x;
                float y = position.y;
                float z = position.z;

                childs[0] = new TreeNode(maxObjectCount, new Vector3(x + subBound, y-subBound, z-subBound), subBound);
                childs[1] = new TreeNode(maxObjectCount, new Vector3(x-subBound, y-subBound, z + subBound), subBound);
                childs[2] = new TreeNode(maxObjectCount, new Vector3(x-subBound, y + subBound, z-subBound), subBound);
                childs[3] = new TreeNode(maxObjectCount, new Vector3(x + subBound, y + subBound, z-subBound), subBound);

                childs[4] = new TreeNode(maxObjectCount, new Vector3(x + subBound, y-subBound, z + subBound), subBound);
                childs[5] = new TreeNode(maxObjectCount, new Vector3(x-subBound, y-subBound, z + subBound), subBound);
                childs[6] = new TreeNode(maxObjectCount, new Vector3(x-subBound, y + subBound, z + subBound), subBound);
                childs[7] = new TreeNode(maxObjectCount, new Vector3(x + subBound, y + subBound, z + subBound), subBound);
            }

            for (int i = bodiesStored.Count - 1; i >= 0; i--)
            {
                Star storedBody = bodiesStored[i];
                int indexChild = GetIndexToInsertObject(storedBody.transform.position);
                if (indexChild > -1)
                {
                    childs[indexChild].InsertToNode(storedBody);
                    bodiesStored.RemoveAt(i);
                }
            }
        }
    }

    public void Remove(Star objectToRemove)
    {
        if (ContainsLocation(objectToRemove.transform.position))
        {
            bodiesStored.Remove(objectToRemove);
            for (int i = 0; i < 4; i++)
            {
                if (childs[i] != null)

                    childs[i].Remove(objectToRemove);
            }
        }
    }

    public void ComputeMassDistribution(float BlackHoleMass)
    {
        if (bodiesStored.Count == 1)
        {
            Mass = 1;
            CenterOfMass = bodiesStored[0].transform.position;
        }
        else
        {
            foreach (Star star in bodiesStored)
            {
                CenterOfMass += star.transform.position;
            }
            Mass = bodiesStored.Count;

            for (int i = 0; i < 4; i++)
            {
                if (childs[i] != null)
                {
                    childs[i].ComputeMassDistribution(BlackHoleMass);
                    Mass += childs[i].Mass;
                    CenterOfMass += childs[i].Mass * childs[i].CenterOfMass;
                }
            }
            Mass += BlackHoleMass;
            CenterOfMass += Vector3.zero * BlackHoleMass;
            CenterOfMass /= Mass;

        }


    }
    public void Clear()
    {
        bodiesStored.Clear();

        for (int i = 0; i < childs.Length; i++)
        {
            if (childs[i] != null)
            {
                childs[i].Clear();
                childs[i] = null;
            }
        }
    }
    public bool ContainsLocation(Vector3 location)
    {
        float minX = position.x - bound;
        float minY = position.y - bound;
        float minZ = position.z - bound;
        float maxX = position.x + bound;
        float maxY = position.y + bound;
        float maxZ = position.z + bound;

        return (location.x >= minX && location.x <= maxX &&
                location.y >= minY && location.y <= maxY &&
                location.z >= minZ && location.z <= maxZ);
    }

    private int GetIndexToInsertObject(Vector3 location)
    {
        for (int i = 0; i < 8; i++)
        {
            if (childs[i].ContainsLocation(location))
            {
                return i;
            }
        }
        return -1;
    }
    public void DrawDebug()
    {
       Vector3 cubeSize = new Vector3(bound * 2, bound * 2, bound * 2);
Gizmos.DrawWireCube(position, cubeSize);


        if (childs[0] != null)
        {
            for (int i = 0; i < childs.Length; i++)
            {
                if (childs[i] != null)
                {
                    childs[i].DrawDebug();
                }
            }
        }
    }
    public Vector3 CalculateTreeForce(Star star, float teta)
    {
        Vector3 acceleration = Vector3.zero;
        if (bodiesStored.Count == 1)
        {
            acceleration = getAcceleration(star.transform.position, bodiesStored[0].transform.position, 1);
        }
        else
        {
            //distance star-center of mass
            float r = Vector3.Distance(CenterOfMass, star.transform.position);
            //height of node
            float d = bound;
            //d/r < teta
            if (d / r < teta)
            {
                acceleration = getAcceleration(star.transform.position, CenterOfMass, Mass);
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    if (childs[i] != null)
                        acceleration += childs[i].CalculateTreeForce(star, teta);
                }

            }
        }
        return acceleration;
    }


    public Vector3 getAcceleration(Vector3 body1, Vector3 body2, float k)
    {
        if (body1 == body2)
            return Vector3.zero;
        return (body2 - body1).normalized * k / Mathf.Pow(Vector3.Distance(body1, body2), 2);

    }
}
