using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMapGenerator : MonoBehaviour {
	const int mapChunkSize = 50;

	public float[,] heightMap;
	public TerrainType[] regions;
	public static bool mapMade;

	void Start (){
		int rand = Random.Range (0, 2);
		float randHole = Random.Range (102.0f, 120.0f);
		if (rand == 0)
			randHole *= -1;
		TreeSpawner.randomX = randHole;
		TreeSpawner.randTree = rand;
		setHeightMap ();
		DisplayMap ();
		mapMade = true;
	}

	public void setHeightMap() {
		float noise = Random.Range (20.0f, 30.0f);
		float persistence = Random.Range (.45f, .9f);
		float amplitude = Random.Range (.9f, 2.4f);
		int randX = Random.Range (-10000, 10000);
		int randY = Random.Range (-10000, 10000);
		heightMap = Noise.GenerateNoiseMap (mapChunkSize, mapChunkSize, noise, persistence, amplitude, randX, randY);
	}

	public Color[] getColorMap() {
		Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
		for (int y = 0; y < mapChunkSize; y++) {
			for (int x = 0; x < mapChunkSize; x++) {
				float currentHeight = heightMap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight <= regions [i].height) {
						colorMap [y * mapChunkSize + x] = regions [i].color;
						break;
					}
				}
			}
		}
		return colorMap;
	}

	public void DisplayMap (){
		AnimationCurve curve = AnimationCurve.EaseInOut (0, 0, 1, 1);
		MapDisplay display = FindObjectOfType<MapDisplay> ();
		display.DrawMesh (MeshGenerator.GenerateTerrainMesh (heightMap, curve), TextureGenerator.TextureFromColorMap (getColorMap(), mapChunkSize, mapChunkSize));
	}
}
