using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancePlatform : MonoBehaviour
{

    public GameObject Beam;
    public GameObject Pivot;
    public GameObject Platform1;
    public GameObject Platform2;
    public float Friction;
    public float MaxSpeed;
    public float MaxCWRotation;
    public float MaxCCWRotation;
    public float RestAngle;

    private Vector3 P1OriginaLPos;
    private Vector3 P2OriginaLPos;

    [field: SerializeField]
    private Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        P1OriginaLPos = Platform1.transform.position;
        P2OriginaLPos = Platform2.transform.position;
        Beam.transform.rotation = Quaternion.Euler(0, 0, RestAngle);

    }

    // Update is called once per frame
    void Update()
    {
        // Get mass of rigidbodies standing on platforms
        float mass1 = 0;
        Collider2D[] colliders1 = Physics2D.OverlapBoxAll(Platform1.transform.position + new Vector3(0, 0.5f, 0), Platform1.GetComponent<BoxCollider2D>().size * new Vector3(1.2f, 0.3f, 1f), 0);
        foreach (Collider2D collider in colliders1)
        {
            if (collider.GetComponent<Rigidbody2D>() == null) continue;
            if(collider.transform.name == "Player2"){
                mass1 += collider.GetComponent<Rigidbody2D>().mass;
            } else if (collider.transform.name == "Player1") {
            }
            print(collider.name);
        }

        float mass2 = 0;
        Collider2D[] colliders2 = Physics2D.OverlapBoxAll(Platform2.transform.position + new Vector3(0, 0.5f, 0), Platform2.GetComponent<BoxCollider2D>().size * new Vector3(1.2f, 0.3f, 1f), 0);
        foreach (Collider2D collider in colliders2)
        {
            if (collider.GetComponent<Rigidbody2D>() == null) continue;
            if(collider.transform.name == "Player2"){
                mass2 += collider.GetComponent<Rigidbody2D>().mass;
            } else if (collider.transform.name == "Player1") {
            }
            print(collider.name);
        }

        print(string.Format("Mass1: {0}, Mass2: {1}", mass1, mass2));

        // Calculate torque accouting for distance from pivot
        float torque1 = mass1 * Vector3.Magnitude(Platform1.transform.position - Pivot.transform.position) * (1 - Friction);
        float torque2 = mass2 * Vector3.Magnitude(Platform2.transform.position - Pivot.transform.position) * (1 - Friction);

        // Rotate beam according to torques
        if (Mathf.Abs(torque1 - torque2) > 0.01f)
        {

            rotation.z = Mathf.Clamp(rotation.z + Mathf.Clamp(-4f * (torque1 - torque2) * Time.deltaTime, -MaxSpeed, MaxSpeed), RestAngle - MaxCCWRotation, RestAngle + MaxCWRotation);
            Beam.transform.rotation = Quaternion.Euler(0, 0, rotation.z);

        }
        else
        {

            rotation = new Vector3(0, 0, Mathf.LerpAngle(Beam.transform.eulerAngles.z, RestAngle, Time.deltaTime * MaxSpeed));
            Beam.transform.rotation = Quaternion.Euler(0, 0, rotation.z);

        }
        Platform1.transform.localEulerAngles = new Vector3(0, 0, -Beam.transform.eulerAngles.z);
        Platform2.transform.localEulerAngles = new Vector3(0, 0, -Beam.transform.eulerAngles.z);
    }
}
