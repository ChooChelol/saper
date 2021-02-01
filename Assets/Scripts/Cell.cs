using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour
{
    public int BombsAround;
    public bool isBomb;
    public bool isOpened;
    public bool MayOpened;
    public Sprite Ico;
    private SpriteRenderer _spriteRenderer;
    public List<Cell> MayOpenedCells;

    private void Awake()
    {
        MayOpenedCells = new List<Cell>();

    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_spriteRenderer.sprite = Ico;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Cell>() != null)
        {
            if(other.gameObject.GetComponent<Cell>().isBomb)
                BombsAround++;
            else
                MayOpenedCells.Add(other.gameObject.GetComponent<Cell>());
        }
    }

    private void EndGame()
    {
        foreach (var cell in FindObjectsOfType<Cell>())
            if (cell.isBomb)
                cell.MarkCell(MarkersCell.M);
            else
                cell.MarkCell((MarkersCell)cell.BombsAround);
    }

    private void OpenClearCell()
    {
        
    }

    private void FlaggingCell()
    {
        MarkCell(MarkersCell.F);
    }

    public void MarkCell(MarkersCell markersCell)
    {
        Ico = Resources.Load<Sprite>($"Sprites/MINESWEEPER_{(int)markersCell}");
        Debug.Log(markersCell);
        _spriteRenderer.sprite = Ico;
        isOpened = true;

    }

    private void OnMouseDown()
    {
        if (isBomb)
        {
            if (Input.GetMouseButtonDown(0)) EndGame();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                MarkCell((MarkersCell)BombsAround);
                if (BombsAround <= 1) OpenEasyCells();
            }
        }
    }

    private void OpenEasyCells()
    {
        foreach (var mayOpenedCell in MayOpenedCells.Where(t => t.BombsAround == 0)
            .Where(t => t.isOpened == false))
        {
            mayOpenedCell.MarkCell((MarkersCell) mayOpenedCell.BombsAround);
            foreach (var openedCell in mayOpenedCell.MayOpenedCells)
            {
                openedCell.MarkCell((MarkersCell) openedCell.BombsAround);
                mayOpenedCell.OpenEasyCells();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!isBomb) Handles.Label(transform.position - new Vector3(.1f,-.1f), BombsAround.ToString());
    }
}

public enum MarkersCell
{
    B0 = 0,
    B1 = 1,
    B2 = 2,
    B3 = 3,
    B4 = 4,
    B5 = 5,
    B6 = 6,
    B7 = 7,
    B8 = 8,
    F = 9,
    M = 10,
}
