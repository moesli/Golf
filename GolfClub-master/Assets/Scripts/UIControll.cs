using UnityEngine;
using UnityEngine.UIElements;

public class UIControll : MonoBehaviour
{
    public ClubControll clubControll;
    // Referenz auf die Füllung des Fortschrittsbalkens
    private ProgressBar progressBar;
    private float chargePercentage;
    private float maxChargeTime = 8f;
    private bool load = false;

    private void Start(){
        progressBar = GetComponent<UIDocument>().rootVisualElement
            .Q<VisualElement>("Footer")
            .Q<VisualElement>("Center")
            .Q<ProgressBar>("ChargeBar");
    }

    private void Update(){
        if (clubControll.isCharging)
        {
            if(load){
                progressBar.value = 0f;
            }

            load = false;

            // Aktualisieren der Breite des Fortschrittsbalkens basierend auf chargeTime
            chargePercentage = clubControll.chargeTime / maxChargeTime;
            progressBar.value = chargePercentage;
        }
        else
        {
            // Setzen Sie die Breite des Fortschrittsbalkens auf 0, wenn nicht aufgeladen wird
            load = true;
        }
    }

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button bDriver = root.Q<Button>("Driver");
        Button bIron = root.Q<Button>("Iron");
        Button bPutter = root.Q<Button>("Putter");


        bDriver.clicked += () => clubControll.clubType = ClubControll.ClubType.Driver;
        bIron.clicked += () => clubControll.clubType = ClubControll.ClubType.Iron;
        bPutter.clicked += () => clubControll.clubType = ClubControll.ClubType.Putter;
    }

}
