using UnityEngine;

public class URPCityGenerator : MonoBehaviour
{
    public int cityWidth = 8;
    public int cityDepth = 8;
    public float spacing = 8f;

    public Material buildingMat;
    public Material roadMat;
    public Material carMat;

    void Start()
    {
        GenerateCity();
        AddDirectionalLight();
        AddCamera();
    }

    void GenerateCity()
    {
        for (int x = 0; x < cityWidth; x++)
        {
            for (int z = 0; z < cityDepth; z++)
            {
                Vector3 pos = new Vector3(x * spacing, 0, z * spacing);
                CreateRoad(pos);
                CreateBuilding(pos + new Vector3(2.5f, 0, 2.5f));
                if (Random.value > 0.5f)
                    CreateCar(pos + new Vector3(Random.Range(0.5f, 5.5f), 0.5f, Random.Range(0.5f, 5.5f)));
            }
        }
    }

    void CreateRoad(Vector3 position)
    {
        GameObject road = GameObject.CreatePrimitive(PrimitiveType.Plane);
        road.transform.localScale = new Vector3(0.8f, 1, 0.8f);
        road.transform.position = position + new Vector3(4, 0, 4);
        road.name = "Road";
        ApplyMaterial(road, roadMat ?? MakeURPMaterial(Color.gray));
    }

    void CreateBuilding(Vector3 position)
    {
        int floors = Random.Range(3, 10);
        float height = floors * 1.5f;

        GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
        building.transform.localScale = new Vector3(3, height, 3);
        building.transform.position = position + new Vector3(0, height / 2f, 0);
        building.name = "Building";

        ApplyMaterial(building, buildingMat ?? MakeURPMaterial(Random.ColorHSV()));

        GameObject terrace = GameObject.CreatePrimitive(PrimitiveType.Cube);
        terrace.transform.localScale = new Vector3(3.2f, 0.2f, 3.2f);
        terrace.transform.position = building.transform.position + new Vector3(0, height / 2 + 0.1f, 0);
        terrace.name = "Terrace";
        ApplyMaterial(terrace, MakeURPMaterial(Color.black));

        int balconyCount = Random.Range(1, 3);
        for (int i = 0; i < balconyCount; i++)
        {
            GameObject balcony = GameObject.CreatePrimitive(PrimitiveType.Cube);
            balcony.transform.localScale = new Vector3(1, 0.2f, 0.4f);
            float yOffset = Random.Range(1, floors) * 1.5f;
            balcony.transform.position = building.transform.position + new Vector3(1.7f, -height / 2 + yOffset, 0);
            balcony.name = "Balcony";
            ApplyMaterial(balcony, MakeURPMaterial(Color.white));
        }
    }

    void CreateCar(Vector3 position)
    {
        GameObject car = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        car.transform.localScale = new Vector3(1.5f, 0.5f, 1);
        car.transform.position = position;
        car.name = "Car";

        ApplyMaterial(car, carMat ?? MakeURPMaterial(Color.red));
    }

    void AddDirectionalLight()
    {
        GameObject lightObj = new GameObject("Directional Light");
        Light light = lightObj.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1f;
        light.color = Color.white;
        lightObj.transform.rotation = Quaternion.Euler(50, 30, 0);
    }

    void AddCamera()
    {
        GameObject cam = new GameObject("Main Camera");
        Camera camera = cam.AddComponent<Camera>();
        cam.tag = "MainCamera";
        cam.transform.position = new Vector3(cityWidth * spacing / 2, 40, -cityDepth * spacing / 3);
        cam.transform.LookAt(new Vector3(cityWidth * spacing / 2, 0, cityDepth * spacing / 2));
        camera.clearFlags = CameraClearFlags.Skybox;
    }

    Material MakeURPMaterial(Color color)
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null)
        {
            Debug.LogError("URP Shader not found. Is URP correctly set up?");
            return null;
        }

        Material mat = new Material(shader);
        mat.color = color;
        return mat;
    }

    void ApplyMaterial(GameObject obj, Material mat)
    {
        Renderer rend = obj.GetComponent<Renderer>();
        if (rend != null && mat != null)
        {
            rend.material = mat;
        }
    }
}
