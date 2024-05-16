using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public bool isSwitched = false;
    public Image background1;
    public Image background2;
    public Animator animator;

    public Sprite[] backgrounds; // Array to store different background sprites

    private int currentIndex = 0; // Index to keep track of the current background

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial background if you have any default image
        if (backgrounds.Length > 0)
        {
            SetImage(backgrounds[currentIndex]);
        }
    }

    public void SwitchImage()
    {
        currentIndex++;
        if (currentIndex >= backgrounds.Length)
        {
            currentIndex = 0; // Reset index if it exceeds array length
        }

        if (!isSwitched)
        {
            background2.sprite = backgrounds[currentIndex];
            animator.SetTrigger("SwitchFirst");
        }
        else
        {
            background1.sprite = backgrounds[currentIndex];
            animator.SetTrigger("SwitchSecond");
        }
        isSwitched = !isSwitched;
    }

    public void SetImage(Sprite sprite)
    {
        if (!isSwitched)
        {
            background1.sprite = sprite;
        }
        else
        {
            background2.sprite = sprite;
        }
    }
}