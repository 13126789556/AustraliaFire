using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtionctionCardController : MonoBehaviour
{
    public Sprite BrevicepEx, DevilEx, DunnartEx, EmuEx, GobyEx, KoalaEx, PlatypusEx, QuollEx;
    public bool brevicepExtincted, devilExtincted, dunnartExtincted, emuExtincted, gobyExtincted, koalaExtincted, platypusExtincted, quollExtincted;
    [HideInInspector]
    public GameManager gm;
    private Image image;
    private Vector3 displayPos;
    private float timer;

    private void Awake()
    {
        displayPos = transform.position;
    }

    void Start()
    {
        transform.position = new Vector3(10000, 10000, 0);
        gm = GameManager.Instance;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.brevicepNumber <= 0 && !brevicepExtincted)
        {
            image.sprite = BrevicepEx;
            transform.position = displayPos;
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                brevicepExtincted = true;
                transform.position = new Vector3(10000, 10000, 0);
                timer = 0;
            }
        }
        else if (gm.devilNumber <= 0 && !devilExtincted)
        {
            image.sprite = DevilEx;
            transform.position = displayPos;
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                devilExtincted = true;
                transform.position = new Vector3(10000, 10000, 0);
                timer = 0;
            }
        }
        else if (gm.dunnartNubmer <= 0 && !dunnartExtincted)
        {
            image.sprite = DunnartEx;
            transform.position = displayPos;
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                dunnartExtincted = true;
                transform.position = new Vector3(10000, 10000, 0);
                timer = 0;
            }
        }
        else if (gm.emuNumber <= 0 && !emuExtincted)
        {
            image.sprite = EmuEx;
            transform.position = displayPos;
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                emuExtincted = true;
                transform.position = new Vector3(10000, 10000, 0);
                timer = 0;
            }
        }
        else if (gm.gobyNumber <= 0 && !gobyExtincted)
        {
            image.sprite = GobyEx;
            transform.position = displayPos;
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                gobyExtincted = true;
                transform.position = new Vector3(10000, 10000, 0);
                timer = 0;
            }
        }
        else if (gm.koalaNumber <= 0 && !KoalaEx)
        {
            image.sprite = KoalaEx;
            transform.position = displayPos;
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                koalaExtincted = true;
                transform.position = new Vector3(10000, 10000, 0);
                timer = 0;
            }
        }
        else if (gm.platypusNumber <= 0 && !platypusExtincted)
        {
            image.sprite = PlatypusEx;
            transform.position = displayPos;
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                platypusExtincted = true;
                transform.position = new Vector3(10000, 10000, 0);
                timer = 0;
            }
        }
        else if (gm.quollNumber <= 0 && !quollExtincted)
        {
            image.sprite = QuollEx;
            transform.position = displayPos;
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                quollExtincted = true;
                transform.position = new Vector3(10000, 10000, 0);
                timer = 0;
            }
        }
    }
}
