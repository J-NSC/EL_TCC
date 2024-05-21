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
    bool playerCollider = false;
    [SerializeField] List<Transform> positions;

    [SerializeField] IndexVIscondeSO indexs;
    public int letterIndex;
    
    public Sing viscondeInstatiated;
    public List<Sing> foundSings;

    [SerializeField] int positionIndex;
    
    public delegate void EnabledMovPlayeHandle(bool actived);
    public static event EnabledMovPlayeHandle enableMovPlayer;

    void Awake()
    {

        // if (viscondeInstatiated == null)
        // {
        //     viscondeInstatiated = this;
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
        //
        // DontDestroyOnLoad(gameObject);

        viscondeAnim = GetComponent<Animator>();
    }

    void Start()
    {
        textComponete.text = String.Empty;
        dialogBox.SetActive(false);
        
        Sing[] sing = FindObjectsOfType<Sing>();
        
        foreach (Sing obj in sing)
        {
            foundSings.Add(obj);
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
      

        
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)) && playerCollider)
        {
            NextLine();
        }
        
        if (positions.Count == 0 )
        {
            Debug.Log("fim do dialogo");
            gameObject.SetActive(false); 
        }else
            transform.position = positions[0].position;
        
    }

    IEnumerator typeLine()
    {

        textComponete.text += lines[0].lines[letterIndex];
        viscondeAnim.SetBool("Talking", true);
        viscondeAnim.SetBool("Smoke", false);
        yield return new WaitForSeconds(1f);
    }

    void NextLine()
    {
        if (letterIndex < lines[0].lines.Count -1)
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


    void removPositionsAndLines()
    {
        lines.RemoveAt(0);
        positions.RemoveAt(0);
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
}
