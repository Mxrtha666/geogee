using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class countryCheck : MonoBehaviour
{
    public Texture2D tex;
    public TextMeshProUGUI text;
    public float raydistance;
    public float delay;
    public string currentCountry;
    public string objectiveCountry;
    public float d;
    public string[] countries;
    
    // Start is called before the first frame update
    void Start()
    {
        StartObjective();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * raydistance, Color.yellow);

        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
        {
            Vector3 vertex = hit.point;

            float latitude = Vector3.Angle(Vector3.up, vertex);
            float angle = Vector3.Angle(new Vector2(1f, 0f), new Vector2(vertex.x, vertex.z));
            if (vertex.z < 0)
            {
                angle = 360 - angle;
            }
            float longitude = angle / 360;

            float indeks = ((tex.GetPixelBilinear(longitude, latitude / 180).r - 1) / d) + 1;

            //print(tex.GetPixelBilinear(longitude, latitude / 180).r);
            //print(Mathf.RoundToInt(indeks));

            if (countries[Mathf.RoundToInt(indeks)] == "256")
            {
                currentCountry = "Ocean";
            }
            else
            {
                currentCountry = countries[Mathf.RoundToInt(indeks)];
            }
        }    
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckObjective();
        }
    }

    public void StartObjective()
    {
        objectiveCountry = countries[Random.Range(1, 171)];
        text.text = objectiveCountry;
    }

    public void CheckObjective()
    {
        StartCoroutine(delayCheck());
    }

    IEnumerator delayCheck()
    {
        yield return new WaitForSeconds(delay);

        if (currentCountry == objectiveCountry)
        {
            print("yea");
            StartObjective();
        }
        else
        {
            print("Game Over Nigger");
        }
    }
}
