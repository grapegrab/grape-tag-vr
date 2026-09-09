using UnityEngine;

public class HangoutSpaceEnvironment : MonoBehaviour
{
    [SerializeField] private Material skyboxMaterial;
    [SerializeField] private Light mainLight;
    [SerializeField] private float dayNightCycleSpeed = 0.1f;
    
    private float timeOfDay = 0.5f; // 0 = night, 0.5 = noon, 1 = night
    
    private void Start()
    {
        // Set up lighting
        if (mainLight != null)
        {
            mainLight.intensity = 1f;
        }
    }
    
    private void Update()
    {
        // Optional: Cycle between day and night
        // timeOfDay += Time.deltaTime * dayNightCycleSpeed;
        // if (timeOfDay > 1f) timeOfDay = 0f;
        // UpdateLighting();
    }
    
    private void UpdateLighting()
    {
        if (mainLight != null)
        {
            // Vary light intensity based on time of day
            mainLight.intensity = Mathf.Sin(timeOfDay * Mathf.PI) * 1.5f;
            
            // Rotate light based on time
            mainLight.transform.rotation = Quaternion.Euler(timeOfDay * 180f, 0, 0);
        }
    }
    
    public void ChangeEnvironmentTheme(int themeIndex)
    {
        // Switch between different environment themes
        // 0 = park, 1 = beach, 2 = mountain, 3 = urban lounge, etc.
        Debug.Log("Environment theme changed to: " + themeIndex);
    }
}
