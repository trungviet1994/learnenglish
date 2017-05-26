//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;

//public class ModeButton : MonoBehaviour
//{
//    public int m_buttons;
//    public float radio;
//    public float multi;
//    public UICategory m_categoryUI;
//    public bool isOpen;
//    public Image m_menuImage;
//    public float fill;
//    public Transform arrow;
//    public Image arrowImage;
//    public GameObject bg;

//    public Text setTitle;

//    private bool runOpen = false;
//    private bool runClose = false;

//    public Image btnImage;

//    public Sprite button1;
//    public Sprite button2;
//    public Sprite button3;


//    void Start()
//    {
//        radio = 90 / m_buttons;
//        arrowImage = arrow.GetComponent<Image>();
//        m_menuImage.fillAmount = 0;
//        arrowImage.fillAmount = 0;
//        m_menuImage.enabled = false;
//    }

//    void Update()
//    {
//        if (runOpen)
//        {
//            OpenMenu();
//        }
//        if (runClose)
//        {
//            CloseMenu();
//        }
//    }

//    public void ChooseMode()
//    {
//        Vector3 inputVector3 = Input.mousePosition - Vector3.zero;
//        float angle = Vector3.Angle(inputVector3, Vector3.right);
//        //Debug.Log((int)angle / (int)radio);
//        if (Vector3.Distance(Input.mousePosition, Vector3.zero) < 500)
//        {
//            Debug.Log(Vector3.Distance(Input.mousePosition, Vector3.zero));
//            SetMode((int)angle / (int)radio);
//            m_categoryUI.SetUpCategory();
//        }
//    }

//    public void SetMode(int index)
//    {
//        Debug.Log("set level");
//        switch (index)
//        {
//            case 0:
//                GamePlayConfig.Instance.ModeLevel = GameConfig.eModeLevel.HARD;
//                DataManager.instance.GetCategoriesInLevel(3);
//                btnImage.sprite = button1;
//                arrow.localRotation = Quaternion.Euler(0, 0, -30);
//                setTitle.text = "Hard Topics";
//                Debug.Log("hard");
//                break;
//            case 1:
//                GamePlayConfig.Instance.ModeLevel = GameConfig.eModeLevel.NORMAL;
//                DataManager.instance.GetCategoriesInLevel(2);
//                btnImage.sprite = button2;
//                arrow.localRotation = Quaternion.Euler(0, 0, 0);
//                setTitle.text = "Normal Topics";
//                Debug.Log("normal");
//                break;
//            case 2:
//                GamePlayConfig.Instance.ModeLevel = GameConfig.eModeLevel.EASY;
//                DataManager.instance.GetCategoriesInLevel(1);
//                btnImage.sprite = button3;
//                arrow.localRotation = Quaternion.Euler(0, 0, 30);
//                setTitle.text = "Easy Topics";
//                Debug.Log("ez");
//                break;
//        }
//        PlayerPrefs.SetInt("lastLevel", index);
//    }

//    [ContextMenu("test")]
//    public void OpenButton()
//    {
//        if (!isOpen)
//        {
//            m_menuImage.enabled = true;
//        }
//        isOpen = !isOpen;
//        runOpen = isOpen;
//        runClose = !runOpen;
//    }
    
//    void OpenMenu()
//    {
//        m_menuImage.fillAmount = Mathf.Lerp(0, 1, fill);
//        arrowImage.fillAmount = Mathf.Lerp(0, 1, fill);
//        fill += Time.deltaTime * multi;
//        if (fill>1)
//        {
//            runOpen = false;
//            fill = 1;
//            m_menuImage.fillAmount = 1;
//            arrowImage.fillAmount = 1;
//            arrow.GetComponent<Image>().enabled = true;
//            bg.SetActive(true);
//        }
//    }

//    void CloseMenu()
//    {
//        bg.SetActive(false);
//        m_menuImage.fillAmount = Mathf.Lerp(0, 1, fill);
//        arrowImage.fillAmount = Mathf.Lerp(0, 1, fill);
//        fill -= Time.deltaTime * multi;
//        if (fill <0)
//        {
//            runClose = false;
//            fill = 0;
//            m_menuImage.fillAmount = 0;
//            arrowImage.fillAmount = 0;
//            m_menuImage.enabled = false;
//            arrow.GetComponent<Image>().enabled = false; 
            
//        }
//    }

//    void OnEnable()
//    {
//        //SetMode(PlayerPrefs.GetInt("lastLevel", m_buttons - 1));
//    }
//}
