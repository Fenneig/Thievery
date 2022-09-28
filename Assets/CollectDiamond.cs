using Creatures.Hero;
using UI.Windows;
using UnityEngine;
using UnityEngine.UI;

public class CollectDiamond : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private Text _scoreText;
    [SerializeField] private VictoryWindow _victoryScreen;

    public void Collect()
    {
        _hero.Score++;
        _scoreText.text = $"{_hero.Score.ToString()} / 7";
    }

    public void VictoryCheck()
    {
        if (_hero.Score == 7)
        {
            _victoryScreen.Show();
        }
    }
}
