using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance { get; private set; }

    // Références aux autres gestionnaires
    public SimulationParameter SimulationParameter { get; private set; }
    public UIManager UIManager { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        // Assumer que SimulationManager et UIManager sont attachés au même GameObject
        SimulationParameter = GetComponent<SimulationParameter>();
        UIManager = GetComponent<UIManager>();
    }

    private void Start()
    {
        // Configurez les éléments UI au démarrage du jeu
        //UIManager.Init();
        SimulationParameter.Init();
    }

    // Autres méthodes liées à la gestion du jeu
}
