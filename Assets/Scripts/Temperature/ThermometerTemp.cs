using UnityEngine;
using UnityEngine.UI;

public class ThermometerTemp : MonoBehaviour
{
    public float temperatureChangeSpeed = 1.0f;
    public float currentTemperature;
    public TimeManager timeManager;

    public float minVendemiaireTemp = 12.7f;
    public float maxVendemiaireTemp = 21.1f;
    public float minBrumaireTemp = 9.6f;
    public float maxBrumaireTemp = 16.3f;
    public float minFrimaireTemp = 5.8f;
    public float maxFrimaireTemp = 10.8f;
    public float minNivoseTemp = 3.4f;
    public float maxNivoseTemp = 7.5f;
    public float minPluvioseTemp = 2.7f;
    public float maxPluvioseTemp = 7.2f;
    public float minVentoseTemp = 5.3f;
    public float maxVentoseTemp = 12.2f;
    public float minGerminalTemp = 7.3f;
    public float maxGerminalTemp = 15.6f;
    public float minFlorealTemp = 7.3f;
    public float maxFlorealTemp = 15.6f;
    public float minPrairialTemp = 10.9f;
    public float maxPrairialTemp = 19.6f;
    public float minMessidorTemp = 13.8f;
    public float maxMessidorTemp = 22.7f;
    public float minThermidorTemp = 15.8f;
    public float maxThermidorTemp = 25.2f;
    public float minFructidorTemp = 15.7f;
    public float maxFructidorTemp = 0.0f;
    public float targetTemperature;

    
    [SerializeField]
    private Image thermometerBarFill;

    void Start(){
        // Initial temperature
        // currentTemperature = Random.Range(minTemp, maxTemp);
    }

    void Update(){
        // Variation de la température
        // float targetTemperature = Random.Range(minTemp, maxTemp);
        // Mettez à jour l'indicateur visuel en fonction de la température actuelle
        UpdateVisualIndicator(currentTemperature);
    }

    void UpdateVisualIndicator(float temperature){
        // Mettez en œuvre la mise à jour de votre indicateur visuel ici (p. ex. rotation de l'aiguille, changement de couleur, etc.).
    }

    public void RefreshTemp(){

        if(timeManager.month == 1){
            targetTemperature = Random.Range(minVendemiaireTemp, maxVendemiaireTemp);
        }
        if(timeManager.month == 2){
            targetTemperature = Random.Range(minBrumaireTemp, maxBrumaireTemp);
        }
        if(timeManager.month == 3){
            targetTemperature = Random.Range(minFrimaireTemp, maxFrimaireTemp);
        }
        if(timeManager.month == 4){
            targetTemperature = Random.Range(minNivoseTemp, maxNivoseTemp);  
        }
        if(timeManager.month == 5){
            targetTemperature = Random.Range(minPluvioseTemp, maxPluvioseTemp);
        }
        if(timeManager.month == 6){
            targetTemperature = Random.Range(minVentoseTemp, maxVentoseTemp);
        }
        if(timeManager.month == 7){
            targetTemperature = Random.Range(minGerminalTemp, maxGerminalTemp);
        }
        if(timeManager.month == 8){
            targetTemperature = Random.Range(minFlorealTemp, maxFlorealTemp);  
        }
        if(timeManager.month == 9){
            targetTemperature = Random.Range(minPrairialTemp, maxPrairialTemp);
        }
        if(timeManager.month == 10){
            targetTemperature = Random.Range(minMessidorTemp, maxMessidorTemp);
        }
        if(timeManager.month == 11){
            targetTemperature = Random.Range(minThermidorTemp, maxThermidorTemp);
        }
        if(timeManager.month == 12){
            targetTemperature = Random.Range(minFructidorTemp, maxFructidorTemp);  
        }
        if(timeManager.month == 13){
            targetTemperature = Random.Range(minVendemiaireTemp, maxVendemiaireTemp);  
        }


        // currentTemperature = Mathf.Lerp(currentTemperature, targetTemperature, temperatureChangeSpeed * Time.deltaTime);
        currentTemperature = targetTemperature;

        thermometerBarFill.fillAmount = (currentTemperature / 100) * 4;

    }
}