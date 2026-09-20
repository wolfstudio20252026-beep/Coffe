using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    private static int days = 1;

    [Header("Settings")]
    [SerializeField] private float secondsPerHour = 10f; 
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text dayText;
    [SerializeField] GameObject panel;

    [Header("Panel settings")]
    [SerializeField] TMP_Text dayTextPanel;
    [SerializeField] TMP_Text nextDayText;
    [SerializeField] TMP_Text clientsText;
    [SerializeField] TMP_Text productsText;



    private int _currentHour = 8;
    private int _currentMinute = 0;

    private float _minuteTimer;
    private float _secondsPerMinute;
    private bool _isWorkDayEnd = false;
    private List<NPC> clients = new List<NPC>();
    
    [HideInInspector] public int countProducts = 0;
    public void AddClient(NPC npc)
    {
        clients.Add(npc);
    }

    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f; 
        dayText.text = $"Day: {days}";
        panel.SetActive(false);
        _secondsPerMinute = secondsPerHour / 60f;
        UpdateTimerUI();
    }

    void Update()
    {
        if (_isWorkDayEnd) return;
        _minuteTimer += Time.deltaTime;

        if (_minuteTimer >= _secondsPerMinute)
        {
            _minuteTimer = 0f;
            _currentMinute++;

            if (_currentMinute >= 60)
            {
                _currentMinute = 0;
                _currentHour++;
            }

            if (_currentHour >= 18)
            {
                _currentHour = 18;
                _currentMinute = 0;
                EndWorkDay();
            }

            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        if (timeText != null)
        {
            timeText.text = $"{_currentHour:D2}:{_currentMinute:D2}";
        }
    }

    private void EndWorkDay()
    {
        _isWorkDayEnd = true;
        Debug.Log("Рабочий день окончен");
        panel.SetActive(true);
        SetPanelTexts();
        Time.timeScale = 0f; 
    }

    private void SetPanelTexts()
    {
        dayTextPanel.text = dayText.text;
        days++;
        nextDayText.text = $"Next day: {days}";
        clientsText.text = $"All clients: {clients.Count}";
        productsText.text = $"All products: {countProducts}";
        clients.Clear();
        countProducts = 0;
    }

    public void NextDay()
    {
        _isWorkDayEnd = false; 
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
