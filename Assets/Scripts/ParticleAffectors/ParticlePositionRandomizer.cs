//author: Cory Barr, but copied from some web site
//Attach this to a particle emmitter.
//not all that useful. Written to see if I could affect a particle emitter


using UnityEngine;
using System.Collections;

public class ParticlePositionRandomizer : MonoBehaviour {
	
	int recalcRate = 90; //how many frames to wait before recalcuting trajectories
	int recalcCounter = 0;
	
	// Use this for initialization
	void Start () {
//	    p = (ParticleEmitter)(GameObject.Find("StrangeParticles").GetComponent(typeof(ParticleEmitter)));
//    	particles = p.particles;	
		print("Starting particle emissions");
	}
	
	// Update is called once per frame
	void Update () {
/*		for (int i=0; i < particles.GetUpperBound(0); i++) {
    	    particles[i].position = Vector3.Lerp(particles[i].position,transform.position,Time.deltaTime / 2.0f);
	    }
	   	p.particles = particles;
		
		return;
		 */
		recalcCounter++;
		if (recalcCounter == recalcRate) {
			recalcCounter = 0;
			Particle[] starparticles = GetComponent<ParticleEmitter>().particles;

			int numParticles = starparticles.GetUpperBound(0);
			print("numParticles: " + numParticles);
			for(int i = 0; i < numParticles; i++) {

				starparticles[i].position = new Vector3(Random.value*10.0f, Random.value*10.0f, Random.value*10.0f);
				starparticles[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}
			GetComponent<ParticleEmitter>().particles = starparticles;
		}
	}
}

