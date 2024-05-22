using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sing : MonoBehaviour
{
    public GameObject dialogBox;
    public TextMeshProUGUI textComponete;
    public List<ViscondeTexSO> lines;
    public Animator viscondeAnim;
    bool playerCollider;
    [SerializeField] List<Transform> positions;

    [SerializeField] IndexVIscondeSO indexViscondeSO;
    public int letterIndex;
    
    [SerializeField] int positionIndex = 0;
    [SerializeField] int lineIndex = 0;
    
    public delegate void EnabledMovPlayeHandle(bool actived);
    public static event EnabledMovPlayeHandle enableMovPlayer;

    public delegate void SaveIndexPositionHandle(int indexPos, int indeLine);
    public static event SaveIndexPositionHandle saveIndexPosition;
    
    public delegate (int,int) LoadIndexPositionHandle();
    public static event LoadIndexPositionHandle loadIndexPosition;

    void Awake()
    {
        viscondeAnim = GetComponent<Animator>();
        
     
    }

    void Start()
    {
        textComponete.text = String.Empty;
        dialogBox.SetActive(false);

        foreach (LoadIndexPositionHandle handle in loadIndexPosition.GetInvocationList())
        {
            (int positions, int line) = handle();
            positionIndex = positions;
            lineIndex = line;
        }
    }
    
    void StartDialogue()
    {
        textComponete.text = String.Empty;
        letterIndex = 0;
        StartCoroutine(typeLine());
    }

    void Update()
    {
        
        if (indexViscondeSO.levelLoad == 0)
        {
            ResetIndex();
            indexViscondeSO.levelLoad++;
        }
        
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)) && playerCollider)
        {
            NextLine();
        }
        
        if (positionIndex > positions.Count)
        {
            gameObject.SetActive(false); 
        }else
            transform.position = positions[positionIndex].position;
    }

    IEnumerator typeLine()
    {

        textComponete.text += lines[lineIndex].lines[letterIndex];
        viscondeAnim.SetBool("Talking", true);
        viscondeAnim.SetBool("Smoke", false);
        yield return new WaitForSeconds(1f);
    }

    void NextLine()
    {
        if (letterIndex < lines[lineIndex].lines.Count -1)
        {
            letterIndex++;
            textComponete.text = String.Empty;
            StartCoroutine(typeLine());
        }
        else
        {
            dialogBox.SetActive(false);
            
            viscondeAnim.SetBool("Talking", false);
            viscondeAnim.SetBool("Smoke", true);

        
        }
    }

    void AlterIndexPosAndLine()
    {
        
        lineIndex++;
        positionIndex++;
        Debug.Log("asdasdasdasdas");
        saveIndexPosition?.Invoke(positionIndex, lineIndex);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerCollider = true;
            dialogBox.SetActive(true);
            StartDialogue();
            enableMovPlayer?.Invoke(false);            
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            playerCollider = false;
            letterIndex = 0;
            dialogBox.SetActive(false);
            viscondeAnim.SetBool("Talking", false);
            viscondeAnim.SetBool("Smoke", true);
            StopCoroutine(typeLine());
            enableMovPlayer?.Invoke(true);
        }
    }

    void ResetIndex()
    {
        positionIndex = 0;
        lineIndex = 0;
        saveIndexPosition?.Invoke(positionIndex, lineIndex);
    }
}
