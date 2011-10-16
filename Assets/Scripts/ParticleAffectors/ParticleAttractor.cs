using UnityEngine;
using System.Collections;

public class ParticleAttractor : MonoBehaviour {
	
	public ParticleEmitter p;
	public int recalcRate = 1; //how many frames to wait before recalcuting trajectories
	public float attraction = 0.5f;
	
	private int recalcCounter = 0;
	private Vector3 attractorPosition;
	
	// Use this for initialization
	void Start () {
		attractorPosition = transform.position;
	}
	
	private double GetVectorMagnitude(Vector3 v3) {
		return(System.Math.Sqrt(v3.x * v3.x + 
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
	
	
	// Update is called once per frame
	void Update () {
		recalcCounter++;
		attractorPosition = transform.position;
		
		if (recalcCounter == recalcRate) {
			recalcCounter = 0;
			Particle[] starparticles = p.particles;

			int numParticles = starparticles.GetUpperBound(0);
			for(int i = 0; i < numParticles; i++) {				
				Vector3 partVelocity = starparticles[i].velocity;
				double magnitude = GetVectorMagnitude(partVelocity);
				Vector3 normalizedParticleVec = NormalizeVector(partVelocity, magnitude);
				Vector3 vecToAttractor = attractorPosition - starparticles[i].position;
				Vector3 normalizedVecToAttractor = NormalizeVector(vecToAttractor);
				float m = (float) magnitude;
				Vector3 newVecToAttractor = new Vector3(normalizedVecToAttractor.x * m,
				                                        normalizedVecToAttractor.y * m,
				                                        normalizedVecToAttractor.z * m);
				starparticles[i].velocity = newVecToAttractor;
			}
			p.particles = starparticles;
		}	
	}
}
