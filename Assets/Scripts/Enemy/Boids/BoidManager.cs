using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    private const int threadGroupSize = 1024;

    private List<Boid> boids = new List<Boid>();
    
    public void Init(List<GameObject> t_boids)
    {
        boids = new List<Boid>();
        
        foreach (var boid in t_boids)
        {
            boids.Add(boid.GetComponent<Boid>());
        }
    }

    public void DeleteBoid(GameObject toDelete)
    {
        boids.Remove(toDelete.GetComponent<Boid>());
    }

    void LateUpdate()
    {
        if (boids.Count > 0)
        {
            int numBoids = boids.Count;
            var boidData = new BoidData[numBoids];

            for (int i = 0; i < boids.Count; i++)
            {
                boidData[i].position = boids[i].position;
                boidData[i].direction = boids[i].forward;
            }

            var boidBuffer = new ComputeBuffer(numBoids, BoidData.Size);
            boidBuffer.SetData(boidData);
            boidBuffer.GetData(boidData);

            for (int i = 0; i < boids.Count; i++)
            {
                boids[i].avgFlockHeading = boidData[i].flockHeading;
                boids[i].centreOfFlockmates = boidData[i].flockCentre;
                boids[i].avgAvoidanceHeading = boidData[i].avoidanceHeading;
                boids[i].numPerceivedFlockmates = boidData[i].numFlockmates;

                boids[i].UpdateBoid();
            }

            boidBuffer.Release();
        }
    }

    public struct BoidData
    {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int numFlockmates;

        public static int Size
        {
            get { return sizeof(float) * 3 * 5 + sizeof(int); }
        }
    }
}