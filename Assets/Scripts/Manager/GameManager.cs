using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Fruit fruit;
    // This is a Transform object because it allows us to loop through the children
    public Transform pellets;
    public FruitHistory fruitHistory;

    public int score { get; private set; }
    private int highScore = 0;
    public int lives { get; private set; }
    public int ghostMultiplier { get; private set; } = 1;
    public TimeManager timeManager;
    public MusicManager musicManager;
    public TMP_Text currentScoreText;
    public TMP_Text highScoreText;


    public bool debugGodMode = false;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        Debug.Log(highScore);
        SetLives(3);
        fruit.gameObject.SetActive(false);
        fruit.currentLevel = 0;
        fruitHistory.EmptyHistory();
        NewRound();
    }

    private void NewRound()
    {
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
        timeManager.InitialSpawnFruit(fruit);
    }

    // Only reset ghosts and pacman, not pellets.
    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
        musicManager.changeBGM(SoundManager.GetAudioClip(SoundType.SIREN, 7));
        musicManager.StartBGM();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
        currentScoreText.SetText(score.ToString());
        if (score > highScore)
        {
            highScore = score;
            highScoreText.SetText(highScore.ToString());
        }
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        RenderLivesUI();
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SoundManager.PlaySound(SoundType.EAT, 6, false);
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        if (!debugGodMode)
        {
            this.pacman.gameObject.SetActive(false);
            musicManager.StopBGM();
            SoundManager.PlaySound(SoundType.DEATH, 0, false);

            SetLives(this.lives - 1);

            if (this.lives > 0)
            {
                Invoke(nameof(ResetState), 3.0f);
            }
            else
            {
                GameOver();
            }
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SoundManager.PlaySound(SoundType.EAT, 1, true, 0.5f);
        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet powerPellet)
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(powerPellet.duration);
        }

        musicManager.changeBGM(SoundManager.GetAudioClip(SoundType.EAT, 8));
        PelletEaten(powerPellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), powerPellet.duration);
    }

    public void FruitEaten(Fruit fruit)
    {
        fruit.gameObject.SetActive(false);
        SetScore(score += fruit.fruitScores[fruit.currentLevel]);
        SoundManager.PlaySound(SoundType.EAT, 3, false);
        if (fruit.sprites.Count - 1 > fruit.currentLevel)
        {
            fruit.currentLevel++;
        }

        SetLives(lives + 1);
        Invoke(nameof(SpawnFruit), fruit.duration);
        fruitHistory.AddFruit(fruit.GetComponent<SpriteRenderer>().sprite);
    }

    public void SpawnFruit()
    {
        fruit.Spawn();
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            // If the gameObject is active, it means you haven't eaten all the pellets
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
        musicManager.changeBGM(SoundManager.GetAudioClip(SoundType.SIREN,7));
    }

    private void RenderLivesUI()
    {
        Debug.Log("RenderLivesUI entered");
        GameObject[] livesUI = GameObject.FindGameObjectsWithTag("LifeUI");

        foreach (GameObject item in livesUI)
        {
            item.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (livesUI.Length > 0)
        {
            if (lives <= 5)
            {
                for (int i = 0; i < lives; i++)
                {
                    livesUI[i].GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else
            {
                foreach (GameObject item in livesUI)
                {
                    item.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }

    }

}
