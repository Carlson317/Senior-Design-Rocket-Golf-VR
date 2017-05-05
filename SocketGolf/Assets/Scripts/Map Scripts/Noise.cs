using UnityEngine;
using System.Collections;

public static class Noise {
	public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float noise, float persistance, float amplitude, int offsetX, int offsetY) {
		float[,] noiseMap = new float[mapWidth, mapHeight];

		Vector2 octaveOffsets = new Vector2 (offsetX, offsetY);

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				float sampleX = (x - (mapWidth / 2f)) / noise * amplitude + octaveOffsets.x;
				float sampleY = (y - (mapHeight / 2f)) / noise * amplitude + octaveOffsets.y;
				float perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;

				if (perlinValue > maxNoiseHeight) {
					maxNoiseHeight = perlinValue;
				} else if (perlinValue < minNoiseHeight) {
					minNoiseHeight = perlinValue;
				}
				noiseMap [x, y] = perlinValue;
			}
		}

		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				noiseMap [x, y] = Mathf.InverseLerp (minNoiseHeight, maxNoiseHeight, noiseMap [x, y]);
			}
		}
	
		return noiseMap;
	}
}