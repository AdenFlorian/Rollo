using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DayNightSystem : MonoBehaviour {

	[Range(0f, 1f)]
	public float timeOfDay; // From 0.0f to 1.0f, 0.0f being midnight, 0.5f being noon
	//[Range(0f, 1f)]
	//public float startTime;
	public float dayLength = 60; // Seconds

	public GameObject sunDLight;
	public GameObject moonDLight;
	public float sunIntensity;
	public float moonIntensity;

	public Color daySkyColor;
	public Color nightSkyColor;
	public Color dayAtmosColor;
	public Color nightAtmosColor;

	public AnimationCurve curve;

	public Renderer skyAtmosRenderer;

	void Awake () {
		//timeOfDay = startTime;
	}

	void Start() {
	}
	
	void Update () {
		// Advance time
		timeOfDay += Time.deltaTime / dayLength;
		if (timeOfDay >= 1.0f) {
			timeOfDay -= 1.0f;
		}
		// Rotate DLights
		sunDLight.transform.eulerAngles = new Vector3((timeOfDay * 360f) - 90, 0f, 0f);
		moonDLight.transform.eulerAngles = new Vector3((timeOfDay * 360f) + 90f, 0f, 0f);
		DebugUI.AddWatchLine("TimeOfDay: " + timeOfDay.ToString("P"));
		RenderSettings.fogColor = Color.Lerp(nightAtmosColor, dayAtmosColor, curve.Evaluate(timeOfDay));
		skyAtmosRenderer.sharedMaterial.SetFloat("_timeOfDay", timeOfDay);
		skyAtmosRenderer.sharedMaterial.SetColor("_skyColor", Color.Lerp(nightSkyColor, daySkyColor, curve.Evaluate(timeOfDay)));
		skyAtmosRenderer.sharedMaterial.SetColor("_atmosColor", RenderSettings.fogColor);
		sunDLight.light.intensity = curve.Evaluate(timeOfDay) * sunIntensity;
		moonDLight.light.intensity = (1 - curve.Evaluate(timeOfDay)) * moonIntensity;
	}
}
