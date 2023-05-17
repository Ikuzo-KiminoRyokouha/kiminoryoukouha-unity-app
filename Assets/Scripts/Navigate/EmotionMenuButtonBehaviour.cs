using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionMenuButtonBehaviour : MonoBehaviour
{

    private bool visible = false;
    public GameObject MenuDetailAnchor;
    public GameObject FloatActionMenu;
    public GameObject MainMenuAnchor;
    public GameObject CancleActionMenu;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClick()
    {
        if (!visible) showMenuDetail();
        else hideMenuDetail();
    }
    private void showMenuDetail()
    {
        visible = true;
        // 메뉴 디테일 보여주기
        MenuDetailAnchor.SetActive(true);
        CancleActionMenu.SetActive(true);

        // 원래 메인메뉴 숨기기
        FloatActionMenu.SetActive(false);
        MainMenuAnchor.SetActive(false);
    }

    private void hideMenuDetail()
    {
        visible = false;
        // 메뉴 디테일 숨기기
        MenuDetailAnchor.SetActive(false);
        CancleActionMenu.SetActive(false);

        // 원래 메인메뉴 보여주기
        FloatActionMenu.SetActive(true);
        MainMenuAnchor.SetActive(true);
    }
}
