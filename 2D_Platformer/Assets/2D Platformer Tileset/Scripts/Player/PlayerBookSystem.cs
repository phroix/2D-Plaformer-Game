using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBookSystem : MonoBehaviour
{
    public GameObject book;

    bool bookOpened = false;
    int currentPage = 0;
    public GameObject[] pages;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(gameObject.transform.childCount);
        book.SetActive(false);
        //for(int i = 0; i < book.transform.childCount; i++)
        //{
        //    pages[i] = book.transform.GetChild(i).gameObject;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        DisplayCurrentPage();
        OpenBook();
    }

    private void OpenBook()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            bookOpened = !bookOpened;
            book.SetActive(bookOpened);
        }
    }

    private void DisplayCurrentPage()
    {
        if (pages != null) 
        {
            pages[currentPage].SetActive(true);
            if(currentPage!=0)pages[currentPage-1].SetActive(false);
            if (currentPage != pages.Length-1) pages[currentPage+1].SetActive(false);
        }
    }

    public void NextPage()
    {
        if (currentPage != pages.Length-1) ++currentPage;
    }

    public void PrevPage()
    {
        if (currentPage != 0) --currentPage;
    }
}
