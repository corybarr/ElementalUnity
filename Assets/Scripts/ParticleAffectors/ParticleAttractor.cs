using UnityEngine;
using System.Collections;

public class ParticleAttractor : MonoBehaviour {
	
	public ParticleEmitter p;
	public int recalcRate = 1; //how many frames to wait before recalculating trajectories
	public float attraction = 0.01f;
	
	private int recalcCounter = 0;
	private Vector3 attractorPosition;
	
	// Use this for initialization
	void Start () {
		attractorPosition = transform.position;
	}
	
	private float GetVectorMagnitude(Vector3 v3) {
		return((float) System.Math.Sqrt(v3.x * v3.x + 
				                v3.y * v3.y + 
				                v3.z * v3.z));
	}
	
	private Vector3 NormalizeVector(Vector3 v3) {
		float magnitude = (float) GetVectorMagnitude(v3);
		return(NormalizeVector(v3, magnitude));
	}

	private Vector3 NormalizeVector(Vector3 v3, double magnitude) {
		float m = (float) magnitude;
		v3.x = v3.x / m;
		v3.y = v3.y / m;
		v3.z = v3.z / m;
		return(v3);
	}

	void Update () {
		recalcCounter++;
		attractorPosition = transform.position;
		
		if (recalcCounter == recalcRate) {
			recalcCounter = 0;
			Particle[] starparticles = p.particles;

			int numParticles = starparticles.GetUpperBound(0);
			for(int i = 0; i < numParticles; i++) {				
				Vector3 currentVelocity = starparticles[i].velocity;
				float magnitude = GetVectorMagnitude(currentVelocity);
				Vector3 normalizedCurVec = NormalizeVector(currentVelocity, magnitude);
				Vector3 vecToAttractor = attractorPosition - starparticles[i].position;
				Vector3 normalizedVecToAttractor = NormalizeVector(vecToAttractor);

				Vector3 averagedVector = attraction * normalizedVecToAttractor + 
					(1.0f - attraction) * normalizedCurVec;
				Vector3 normalizedAveragedVec = NormalizeVector(averagedVector);
				
				starparticles[i].velocity = normalizedAveragedVec * magnitude;
			}
			p.particles = starparticles;
		}	
	}
}
