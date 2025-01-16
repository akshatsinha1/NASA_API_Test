using UnityEngine;

public class PlanetSetter : MonoBehaviour
{
    public GameObject sun;
    public GameObject mercury;
    public GameObject venus;
    public GameObject earth;
    public GameObject mars;
    public GameObject jupiter;
    public GameObject saturn;
    public GameObject uranus;
    public GameObject neptune;

    void Start()
    {
        // Position the Sun at the origin
        sun.transform.position = new Vector3(0, 0, 0);

        // Set positions for each planet
        mercury.transform.localPosition = new Vector3(1.42f, -3.82f, -0.37f);
        venus.transform.localPosition = new Vector3(-2.95f, 7.04f, 0.38f);
        earth.transform.localPosition = new Vector3(-1.34f, 9.77f, 0.0f);
        mars.transform.localPosition = new Vector3(13.21f, 4.95f, -0.37f);
        jupiter.transform.localPosition = new Vector3(48.25f, -12.86f, -1.16f);
        saturn.transform.localPosition = new Vector3(79.75f, 46.23f, -4.0f);
        uranus.transform.localPosition = new Vector3(145.11f, -127.55f, -1.39f);
        neptune.transform.localPosition = new Vector3(299.58f, -47.49f, -6.17f);
    }
}
