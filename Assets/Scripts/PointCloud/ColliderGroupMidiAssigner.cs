using UnityEngine;
using System.Collections;
using System;

public class ColliderGroupMidiAssigner : MonoBehaviour {
	public int midiNoteOffset = 70;
	public ColliderGroup colliderGroup;
	
	// This callback is generated by ColliderGroup
	void OnAllCollidersCreated() {		
		//print("num colliders is " + CubesAL.Count);
		//ArrayList cubesAL = colliderGroup.CubesAL;
		
		Transform[,,] colliders = colliderGroup.Colliders;
		
		int colliderCount = 0;
		for (int x = 0; x < colliders.GetLength(0); x++) {
			for (int y = 0; y < colliders.GetLength(1); y++) {
				for (int z = 0; z < colliders.GetLength(2); z++) {
					Transform cur_collider = colliders[x, y, z];
					MidiSender cur_midi_sender = cur_collider.GetComponent(typeof(MidiSender)) as MidiSender;
					cur_midi_sender.midiNote = colliderCount + midiNoteOffset;
					cur_midi_sender.midiNote = cur_midi_sender.midiNote % 127;
					if (cur_midi_sender.midiNote < 50) { 
						cur_midi_sender.midiNote = 50 + cur_midi_sender.midiNote;
					}
					colliderCount++;
				}
			}
		}
	}
}
