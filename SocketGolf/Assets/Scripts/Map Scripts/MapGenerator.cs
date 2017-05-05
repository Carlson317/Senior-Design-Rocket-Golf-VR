using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;

public class MapGenerator : MonoBehaviour {
    const int mapChunkSize = 50;

	public float[,] heightMap;
	public TerrainType[] regions;
	public static float noise = 0.0f;
	public static float persistence = 0.0f;
	public static float amplitude = 0.0f;
	public static int randX = 0;
	public static int randY = 0;
	public static bool mapMade;

	void Start(){
		 noise = 0.0f;
		 persistence = 0.0f;
		 amplitude = 0.0f;
		 randX = 0;
		 randY = 0;
		 mapMade = false;
	}
	void Update (){
		if (noise > 19.0f && !mapMade) {
			setHeightMap ();
			DisplayMap ();
			mapMade = true;
		}
	}

	public void setHeightMap() {
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

[System.Serializable]
public struct TerrainType {
    public string name;
    public float height;
    public Color color;
    public PhysicMaterial physics;
} 