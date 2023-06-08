using UnityEngine;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    private VisualElement root;
    private int score; // Die Anzahl der Schläge
    private Label scoreText;
    private Label scoreboardText;
    private GroupBox courses;
    public Hole hole;

    public enum Hole
    {
        Hole1,   
        Hole2,     
        Hole3    
    }

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        scoreText = root.Q<Label>("Score");
        courses = GetComponent<UIDocument>().rootVisualElement
            .Q<Foldout>("Scoreboard")
            .Q<GroupBox>("Courses");
    }

    private void Start()
    {
        score = 0; // Initialisiere den Punktestand auf 0
        UpdateScoreText(); // Aktualisiere die Anzeige des Punktestands
    }

    private void Update(){

    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Aktualisiere den Text des UI-Textobjekts mit dem aktuellen Punktestand
    }

    public void IncrementScore()
    {
        score++; // Erhöhe den Punktestand um 1
        UpdateScoreText(); // Aktualisiere die Anzeige des Punktestands
    }

    public void ResetScore()
    {
        score = 0; 
        UpdateScoreText(); // Aktualisiere die Anzeige des Punktestands
    }

    public void SaveScore()
    {
        switch(hole)
        {
            case Hole.Hole1:
                scoreboardText = courses.Q<GroupBox>("Course1").Q<Label>("Hole1");
                scoreboardText.text = score.ToString();
                ResetScore();
                hole = Hole.Hole2;
                break;
            case Hole.Hole2:
                scoreboardText = courses.Q<GroupBox>("Course2").Q<Label>("Hole2");
                scoreboardText.text = score.ToString();
                ResetScore();
                hole = Hole.Hole3;
                break;
            case Hole.Hole3:
                scoreboardText = courses.Q<GroupBox>("Course3").Q<Label>("Hole3");
                scoreboardText.text = score.ToString();
                ResetScore();
                hole = Hole.Hole1;
                break;
        }
    }

    public int GetScore()
    {
        return score;
    }
}
