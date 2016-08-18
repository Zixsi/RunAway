using UnityEngine;
using System.Collections;

public class SpecialEvent : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject platform;
    public GameObject male;
    public GameObject female;
    public GameObject wrap;

    public AudioSource music;

    private Transform _wrapT;
    private Vector3 _wrapStartPos;
    private Quaternion _wrapStartRot;
    private Transform _maleT;
    private Transform _femaleT;
    private Vector3 _maleStartPos;
    private Quaternion _maleStartRot;
    private Vector3 _femaleStartPos;
    private Quaternion _femaleStartRot;

    void Start ()
    {
        _wrapT = wrap.transform;
        _wrapStartPos = _wrapT.localPosition;
        _wrapStartRot = _wrapT.localRotation;
        _maleT = male.transform;
        _maleStartPos = _maleT.localPosition;
        _maleStartRot = _maleT.localRotation;
        _femaleT = female.transform;
        _femaleStartPos = _femaleT.localPosition;
        _femaleStartRot = _femaleT.localRotation;
    }
	
	void Update ()
    {
	
	}

    public void Run(float time = 0)
    {
        CameraLook cameraLookScript = gameManager.cam.GetComponent<CameraLook>() as CameraLook;
        cameraLookScript.target = null;
        gameManager.cam.transform.position = new Vector3(platform.transform.position.x, 40.0f, -870.0f);

        music.time = time;
        music.Play();
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(Action1());
        yield return StartCoroutine(Dance2());
        yield return StartCoroutine(Dance1());
        yield return StartCoroutine(Action5());
        StartCoroutine(Loop());
    }

    IEnumerator Dance1()
    {
        yield return StartCoroutine(Action2());
        yield return StartCoroutine(Action4());
        yield return StartCoroutine(Action3());
    }

    IEnumerator Dance2()
    {
        for(int i = 0; i < 2; i++)
        {
            yield return StartCoroutine(Action2());
            yield return StartCoroutine(Action3());
            yield return null;
        }
        yield return null;
    }

    // Кружатся на месте
    IEnumerator Action1()
    {
        Debug.Log("Кружатся на месте");

        float deltaTime = 0;
        float fullAngle = 0;
        float angle = 0;
        float maxAngle = (360.0f * 2.0f);
        while(fullAngle < maxAngle)
        {
            deltaTime = Time.deltaTime;
            angle = deltaTime * 200.0f;
            _maleT.Rotate(Vector3.down, angle);
            _femaleT.Rotate(Vector3.down, angle);

            fullAngle += angle;
            yield return null;
        }

        _maleT.localRotation = _maleStartRot;
        _femaleT.localRotation = _femaleStartRot;

        yield return new WaitForSeconds(0.3f);
        yield return null;
    }

    // Идут друг к другу
    IEnumerator Action2()
    {
        Debug.Log("Идут друг к другу");

        float deltaTime = 0;
        float localPos = _femaleStartPos.x;
        while(localPos > 0.5f)
        {
            deltaTime = Time.deltaTime;
            _maleT.Translate(new Vector3(deltaTime, 0));
            _femaleT.Translate(new Vector3(-deltaTime, 0));
            localPos -= deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);
        yield return null;
    }

    // Идут в разные стороны
    IEnumerator Action3()
    {
        Debug.Log("Идут в разные стороны");

        float deltaTime = 0;
        float localPos = _femaleT.localPosition.x;
        while(localPos < 1.5f)
        {
            deltaTime = Time.deltaTime;
            _maleT.Translate(new Vector3(-deltaTime, 0));
            _femaleT.Translate(new Vector3(deltaTime, 0));
            localPos += deltaTime;
            yield return null;
        }

        _maleT.localPosition = _maleStartPos;
        _femaleT.localPosition = _femaleStartPos;

        yield return new WaitForSeconds(0.3f);
        yield return null;
    }

    // Кружатся вместе
    IEnumerator Action4()
    {
        Debug.Log("Кружатся вместе");

        float deltaTime = 0;
        float fullAngle = 0;
        float angle = 0;
        float maxAngle = (360.0f * 2.0f);
        while(fullAngle < maxAngle)
        {
            deltaTime = Time.deltaTime;
            angle = deltaTime * 200.0f;
            _wrapT.Rotate(Vector3.down, angle);

            fullAngle += angle;
            yield return null;
        }

        _wrapT.localRotation = _wrapStartRot;

        yield return new WaitForSeconds(0.3f);
        yield return null;
    }

    // Кружатся кружась
    IEnumerator Action5()
    {
        Debug.Log("Кружатся кружась");

        float deltaTime = 0;
        float fullAngle = 0;
        float angle = 0;
        float maxAngle = (360.0f * 1.0f);
        while(fullAngle < maxAngle)
        {
            deltaTime = Time.deltaTime;
            angle = deltaTime * 50.0f;
            _wrapT.Rotate(Vector3.down, -angle);
            _maleT.Rotate(Vector3.down, angle * 8.0f);
            _femaleT.Rotate(Vector3.down, angle * 8.0f);

            fullAngle += angle;
            yield return null;
        }

        _wrapT.localRotation = _wrapStartRot;
        _maleT.localRotation = _maleStartRot;
        _femaleT.localRotation = _femaleStartRot;

        yield return new WaitForSeconds(0.3f);
        yield return null;
    }

}
