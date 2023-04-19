using System.Diagnostics;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using NRKernal;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using Debug = UnityEngine.Debug;

public class Confirm : MonoBehaviour
{
    public static Confirm plane;
    public ReticleBehaviour Reticle;

    public Vector3 ReticleVector;

    [SerializeField]
    private GameObject ConfirmPrefab;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (plane == null)
        {
            plane = this;
        }
        else if (plane != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // SceneManager.LoadScene("MainScene");

    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "PlaneDetectScene")
        {
            if (ConfirmPrefab != null && WasTapped() && Reticle.CurrentPlane != null)
            {
                ReticleVector = Reticle.transform.position;
                Instantiate(ConfirmPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }

    private bool WasTapped()
    {
        return NRInput.GetButtonDown(ControllerButton.TRIGGER);
    }
}
