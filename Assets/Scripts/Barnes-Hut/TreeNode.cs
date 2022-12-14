using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode
{
    public List<Star> bodiesStored;
    private int maxObjectCount;
    public TreeNode[] childs;
    private Rect bounds;
    private List<Star> returnedObjects;
    private List<Star> cellObjects;
    private float Mass { get; set; }
    private Vector3 CenterOfMass { get; set; }



    // Start is called before the first frame update
    public TreeNode(int maxSize, Rect bounds)
    {
        this.bounds = bounds;
        maxObjectCount = maxSize;
        childs = new TreeNode[4];
        bodiesStored = new List<Star>(maxSize);
    }
    public void insertToNode(Star star)
    {
        if (childs[0] != null)
        {
            int indexChild = GetIndexToInsertObject(new Vector2(star.transform.position.x, star.transform.position.z));
            if (indexChild > -1)
            {
                childs[indexChild].insertToNode(star);
            }
            return;
        }
        bodiesStored.Add(star);
        if (bodiesStored.Count > maxObjectCount)
        {
            if (childs[0] == null)
            {
                float subWidth = (bounds.width / 2f);
                float subHeight = (bounds.height / 2f);
                float x = bounds.x;
                float y = bounds.y;
                childs[0] = new TreeNode(maxObjectCount, new Rect(x + subWidth, y, subWidth, subHeight));
                childs[1] = new TreeNode(maxObjectCount, new Rect(x, y, subWidth, subHeight));
                childs[2] = new TreeNode(maxObjectCount, new Rect(x, y + subHeight, subWidth, subHeight));
                childs[3] = new TreeNode(maxObjectCount, new Rect(x + subWidth, y + subHeight, subWidth, subHeight));
            }
            int i = bodiesStored.Count - 1;
            while (i >= 0)
            {
                Star storedBody = bodiesStored[i];
                int indexChild = GetIndexToInsertObject(new Vector2(storedBody.transform.position.x, storedBody.transform.position.z));
                if (indexChild > -1)
                {
                    childs[indexChild].insertToNode(storedBody);
                }
                bodiesStored.RemoveAt(i);
                i--;
            }
        }
    }

    public void Remove(Star objectToRemove)
    {
        if (ContainsLocation(new Vector2(objectToRemove.transform.position.x, objectToRemove.transform.position.z)))
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
    public bool ContainsLocation(Vector2 location)
    {
        return bounds.Contains(location);
    }

    private int GetIndexToInsertObject(Vector2 location)
    {
        for (int i = 0; i < 4; i++)
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
        Gizmos.DrawLine(new Vector3(bounds.x, 0, bounds.y), new Vector3(bounds.x, 0, bounds.y + bounds.height));
        Gizmos.DrawLine(new Vector3(bounds.x, 0, bounds.y), new Vector3(bounds.x + bounds.width, 0, bounds.y));
        Gizmos.DrawLine(new Vector3(bounds.x + bounds.width, 0, bounds.y), new Vector3(bounds.x + bounds.width, 0, bounds.y + bounds.height));
        Gizmos.DrawLine(new Vector3(bounds.x, 0, bounds.y + bounds.height), new Vector3(bounds.x + bounds.width, 0, bounds.y + bounds.height));
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
            float d = bounds.height;
            //d/r < teta
            if (d / r < teta)
            {
                acceleration = getAcceleration(star.transform.position, CenterOfMass, Mass);
            }
            else
            {
                for (int i = 0; i < 4; i++)
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
