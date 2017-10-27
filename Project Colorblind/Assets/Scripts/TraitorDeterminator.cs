using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Dante Nardo
/// Last Modified: 10/1/2017
/// Purpose: Puts text on the screen to determine who the traitor is.
/// </summary>
public class TraitorDeterminator : MonoBehaviour
{
    #region TraitorDeterminator Members
    public Text m_redText;
    public Text m_greenText;
    public Text m_blueText;
    private char m_traitor;
    #endregion

    #region TraitorDeterminator Methods
    void Start()
    {
        RandomizeTraitor();
        SetText();
    }

    private void RandomizeTraitor()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                m_traitor = 'r';
                break;
            case 1:
                m_traitor = 'g';
                break;
            case 2:
                m_traitor = 'b';
                break;
        }
    }

    private void SetText()
    {
        switch (m_traitor)
        {
            case 'r':
                m_redText.text = "RED PLAYER: TRICK BLUE INTO DYING 5 TIMES.";
                m_greenText.text = "GREEN PLAYER: GET TO THE END OF THE LEVEL.";
                m_blueText.text = "BLUE PLAYER: GET TO THE END OF THE LEVEL.";
                break;
            case 'g':
                m_redText.text = "RED PLAYER: GET TO THE END OF THE LEVEL.";
                m_greenText.text = "GREEN PLAYER: TRICK RED INTO DYING 5 TIMES.";
                m_blueText.text = "BLUE PLAYER: GET TO THE END OF THE LEVEL.";
                break;
            case 'b':
                m_redText.text = "RED PLAYER: GET TO THE END OF THE LEVEL.";
                m_greenText.text = "GREEN PLAYER: GET TO THE END OF THE LEVEL.";
                m_blueText.text = "BLUE PLAYER: TRICK GREEN INTO DYING 5 TIMES.";
                break;
        }
    }
    #endregion
}
