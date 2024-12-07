using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI powerUPText;

    public bool showCanMultiJump = false;
    public bool showCanSprint = false;

    private void OnEnable()
    {
        UpdateUIText() ;
    }

    public void UpdateUIText()
    {
        string shownString = "";
        if (showCanMultiJump) shownString += " J";
        if (showCanSprint) shownString += " S";
        powerUPText.text = shownString;
    }

    public void SetCanMultijump(bool canMultiJump)
    {
        showCanMultiJump = canMultiJump;
        UpdateUIText();
    }

    public void SetCanSprint(bool canSprint)
    {
        showCanSprint = canSprint;
        UpdateUIText();
    }
}
