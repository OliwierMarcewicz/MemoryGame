using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MemoryGrid : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField]
    private int width = 4;
    [SerializeField]
    private int height = 4;
    [SerializeField]
    private float cellSize = 1f;
    [Header("Colors")]

    [SerializeField] List<Color> colors = new List<Color>();
    
    [Header("Tile Settings")]
    [SerializeField]
    private GameObject tilePrefab;
    [Header("Tile Reveal Delay")]
    [SerializeField]
    private float tileDelay = 3f;
    [SerializeField]
    private float TimeRandomizer = 0.5f;


    private PlayerRoundConstraits playerRoundConstraits;

    private int currentColorIndex = 0;



    private List<MemoryTile> tiles = new();

    private void OnEnable()
    {
        StateMachine.Instance.OnStateChanged += HandleStateChanged;
    }
    private void OnDisable()
    {
        StateMachine.Instance.OnStateChanged -= HandleStateChanged;
    }
    private void Awake()
    {
        playerRoundConstraits = FindFirstObjectByType <PlayerRoundConstraits>();
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void HandleStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Preview:
                RandomizeTileColors();
                break;
            case GameState.Playing:
                HideAllTiles();
                break;
            case GameState.Results:
                RevealAllTiles();
                break;
        }
    }

    
    private void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = transform.position + new Vector3(x * cellSize, 0f, y * cellSize);
                GameObject tileObject = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                MemoryTile tile = tileObject.GetComponent<MemoryTile>();
                tiles.Add(tile);
            }
        }
    }

    private IEnumerator RandomizeTileColorsCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < tileDelay)
        {
            
            foreach (var tile in tiles)
            {
                
                tile.SetId(GetRandomColorNumber());
                tile.SetColor(colors[tile.Id]);
                tile.RevealColor();
                
            }
            yield return new WaitForSeconds(TimeRandomizer); // Optional delay for visual effect
            elapsedTime += TimeRandomizer;
        }
        MemoryGameMachine.Instance.StartGame();
        // StartCoroutine(RandomTest());
    }
    // private IEnumerator RandomTest(){
    //     float elapsedTime = 0f;
    //     while (elapsedTime < 2f){
    //         yield return new WaitForSeconds(0.5f);
    //         elapsedTime += 0.5f;
    //     }
    //     MemoryGameMachine.Instance.EndGame();
    // }


    private void RandomizeTileColors()
    {
        StartCoroutine(RandomizeTileColorsCoroutine());  
    }
    

    private void HideAllTiles()
    {
        foreach (var tile in tiles)
        {
            tile.HideColor();
        }
        playerSetUp();
    }
    private void RevealAllTiles()
    {
        foreach (var tile in tiles)
        {
            // tile.RevealColor();
            // (tile.Id == currentColorIndex ? (System.Action)tile.RevealColor : () => { })();
            if (tile.Id == playerRoundConstraits.GetColorId)
            {
                tile.RevealColor();
            }
            
        }
    }
    private int GetRandomColorNumber()
    {
        return Random.Range(0, colors.Count);
    }

    private void playerSetUp()
    {
        currentColorIndex = Random.Range(0, colors.Count);
        playerRoundConstraits.SetColorId(currentColorIndex);
        playerRoundConstraits.SetColor(colors[currentColorIndex]);
        countMaxScore();
    }

    private void countMaxScore()
    {
        int maxScore = 0;
        foreach (var tile in tiles)
        {
            if (tile.Id == playerRoundConstraits.GetColorId)
            {
                maxScore++;
            }
        }
        ScoreManager.Instance.SetMaxScore(maxScore);
    }
}