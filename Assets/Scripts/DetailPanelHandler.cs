using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ObsessVR.DetailPanel;
using ObsessVR.Animation;

public class DetailPanelHandler : MonoBehaviour
{
    public HotSpotModel hotSpot;
    public Sprite CancelImage;
    public Sprite PreviousImage;
    public Sprite PlayImage;
    public Sprite PauseImage;
    public GameObject modelSpawnPoint;
    public float rotationSpeed = 1.0f;

    private bool isVideoPlaying = false;
    private bool isViewingModel = false;
    private bool isVideoPaused = false;
    
    private List<Sprite> imageLists;
    private int imageIndex = 0;

    private GameObject backObj;
    private GameObject tubeObj;
    private GameObject modelObj;
    private GameObject videoObj;
    private GameObject videoButtonObj;
    private GameObject modelButtonObj;
    private GameObject zoomButtonObj;

    private static string modelName = "MODEL_OBJECT";
    private static string videoObjName = "VIDEO_PLAYER";
    private static string videoInitialUrl = "jar:file://${Application.dataPath}!/assets/";
    private Quaternion targetRotation;
    private Vector3 targetScale;
    public bool clockRotate = false;
    public bool counterClockRotate = false;
    public bool zoomIn = false;
    public bool zoomOut = false;

    public void Awake()
    {
        modelButtonObj = transform.Find("Canvas").Find("Model").gameObject;
        videoButtonObj = transform.Find("Canvas").Find("Video").gameObject;
        zoomButtonObj = transform.Find("Canvas").Find("ZoomButtons").gameObject;
        videoObj = transform.Find(videoObjName).gameObject;
        backObj = transform.Find("Canvas").Find("Back").gameObject;
        tubeObj = transform.Find("Tube").gameObject;
        tubeObj.SetActive(false);
        zoomButtonObj.SetActive(false);
        videoObj.SetActive(false);
    }

    public void Start()
    {
        if (hotSpot != null)
        {
            transform.Find("Canvas").Find("Plate").Find("Brand").GetComponent<Text>().text = hotSpot.brandName;
            transform.Find("Canvas").Find("Plate").Find("Product").GetComponent<Text>().text = hotSpot.productName;
            transform.Find("Canvas").Find("Plate").Find("Price").GetComponent<Text>().text = "$" + hotSpot.price.ToString("F2");

            ImageLoad();
        } else
        {
            Debug.LogError("There is no HotSpot information in the detail panel");
        }
    }

    //Use Update for smooth rotate animation
    public void Update()
    {
        if (modelObj != null)
        {
            if (counterClockRotate ^ clockRotate)
            {
                if (counterClockRotate)
                {
                    targetRotation *= Quaternion.AngleAxis(-1, Vector3.forward);
                }
                else if (clockRotate)
                {
                    targetRotation *= Quaternion.AngleAxis(1, Vector3.forward);
                }
                modelObj.transform.rotation = Quaternion.Lerp(modelObj.transform.rotation, targetRotation, Time.time * rotationSpeed);
            }

            if (zoomIn ^ zoomOut)
            {
                targetScale = modelObj.transform.localScale;
                if (zoomIn)
                {
                    targetScale +=  Vector3.one * 0.1f;
                }
                else if (zoomOut)
                {
                    targetScale -= Vector3.one * 0.1f;
                }
                modelObj.transform.localScale = Vector3.Lerp(modelObj.transform.localScale, targetScale, Time.time * rotationSpeed);
            }
        }
    }

    private void ImageLoad()
    {
        if (hotSpot.imagePaths.Count > 0)
        {
            imageLists = new List<Sprite>();
            foreach (string tempPath in hotSpot.imagePaths)
            {
                imageLists.Add(Resources.Load<Sprite>(tempPath));
            }
            transform.Find("Canvas").Find("Back").Find("Picture").GetComponent<Image>().sprite = Resources.Load<Sprite>(hotSpot.imagePaths[imageIndex]);
        }
    }

    public void NextButton()
    {
        if (!isViewingModel)
        {
            if (imageLists.Count - 1 > imageIndex)
            {
                transform.Find("Canvas").Find("Back").Find("Picture").GetComponent<Image>().sprite = imageLists[imageIndex + 1];
                imageIndex++;
            }
        }
        else
        {
            counterClockRotate = true;
        }
    }

    public void NextButtonOut()
    {
        counterClockRotate = false;
    }

    public void PreviousButton()
    {
        if (!isViewingModel)
        {
            if (imageIndex > 0)
            {
                transform.Find("Canvas").Find("Back").Find("Picture").GetComponent<Image>().sprite = imageLists[imageIndex - 1];
                imageIndex--;
            }
        }
        else
        {
            clockRotate = true;
        }
    }

    public void PreviousButtonOut()
    {
        clockRotate = false;
    }

    public void ZoomInButton()
    {
        zoomIn = true;
    }

    public void ZoomInButtonOut()
    {
        zoomIn = false;
    }

    public void ZoomOutButton()
    {
        zoomOut = true;
    }

    public void ZoomOutButtonOut()
    {
        zoomOut = false;
    }

    public void VideoButton()
    {
        if (!isVideoPlaying) {
            //start playing video
            modelButtonObj.SetActive(false);
            videoObj.SetActive(true);
            //transform.Find("Canvas").Find("Video").GetComponent<Image>().sprite = PauseImage;

            videoObj.GetComponentInChildren<GvrVideoPlayerTexture>().videoURL = videoInitialUrl + hotSpot.videoPath;
            videoObj.GetComponentInChildren<GvrVideoPlayerTexture>().ReInitializeVideo();
            
            transform.Find("Canvas").Find("Cancel").GetComponent<Image>().sprite = PreviousImage;
            isVideoPlaying = true;
        }
        else 
        {
            videoObj.GetComponentInChildren<GvrVideoPlayerTexture>().Pause();
            videoObj.SetActive(false);
            isVideoPlaying = false;

            //while playing video
            if (!isVideoPaused)
            {
                //pause video
                //transform.Find("Canvas").Find("Video").GetComponent<Image>().sprite = PlayImage;
                //isVideoPaused = true;
            }
            else
            {
                //play video
                //transform.Find("Canvas").Find("Video").GetComponent<Image>().sprite = PauseImage;
                //isVideoPaused = false;
            }
        }
    }

    public void ModelButton()
    {
        videoButtonObj.SetActive(false);
        modelButtonObj.SetActive(false);
        zoomButtonObj.SetActive(true);

        if (!isViewingModel)
        {
            backObj.SetActive(false);
            tubeObj.SetActive(true);
            transform.Find("Canvas").Find("Cancel").GetComponent<Image>().sprite = PreviousImage;
            isViewingModel = true;

            AppearModel();
        }
    }

    public void CancelButton()
    {
        if (isVideoPlaying || isViewingModel)
        {
            if (isVideoPlaying)
            {
                transform.Find("Canvas").Find("Video").GetComponent<Image>().sprite = PlayImage;
                transform.Find("Canvas").Find("Cancel").GetComponent<Image>().sprite = CancelImage;
                videoObj.SetActive(false);
                isVideoPlaying = false;
            }
            if (isViewingModel)
            {
                DeleteModel();
                backObj.SetActive(true);
                tubeObj.SetActive(false);
                counterClockRotate = false;
                clockRotate = false;
                transform.Find("Canvas").Find("Cancel").GetComponent<Image>().sprite = CancelImage;
                isViewingModel = false;
            }

            modelButtonObj.SetActive(true);
            videoButtonObj.SetActive(true);
            zoomButtonObj.SetActive(false);
        }
        else
        {
            StartCoroutine(DisappearDetailPanelWithAnimation());
        }
    }

    private void AppearModel()
    {
        GameObject model = Instantiate(Resources.Load<GameObject>(hotSpot.model3DPath));
        Vector3 oriPos = model.transform.position;
        Quaternion oriRot = model.transform.rotation;
        model.name = modelName;
        model.transform.parent = modelSpawnPoint.transform;
        model.transform.localPosition = oriPos;
        model.transform.localRotation = oriRot;

        modelObj = model;
        targetRotation = modelObj.transform.rotation;
    }

    private void DeleteModel()
    {
        Destroy(GameObject.Find(modelName));
    }

    IEnumerator DisappearDetailPanelWithAnimation()
    {
        AnimationHandler.DisappearAnimationTrigger(transform.GetComponent<Animator>());
        yield return new WaitForSeconds(0.4f);
        DetailPanelCreateHandler.FindDetailPanelAndDestroyIt();
    }
}
