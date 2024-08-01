using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    void Renew(string achievementName);
}
public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}
public class AchievementManager : IObserver
{
    //TODO : 딕셔너리를 Json파일로 저장 / 로드 해야할듯
    private Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();
    public void Renew(string achievementName)
    {
        if (achievements.TryGetValue(achievementName, out Achievement achievement) && !achievement.IsUnlocked)
        {
            achievement.Unlock();
            if (achievement.NextAchievement != null)
            {
                achievements.Add(achievement.NextAchievement.Name, achievement.NextAchievement);
                Debug.Log("새로운 업적 : " + achievement.NextAchievement.Name);
            }
        }
    }
    public void InitAchievements()
    {
        Achievement achievement1_3 = new Achievement("20점 달성", "점수 20점 얻기");
        Achievement achievement1_2 = new Achievement("15점 달성", "점수 15점 얻기", achievement1_3);
        Achievement achievement1_1 = new Achievement("10점 달성", "점수 10점 얻기", achievement1_2);

        achievements.Add(achievement1_1.Name, achievement1_1);
    }
}
public class Achievement
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsUnlocked { get; private set; }
    public Achievement NextAchievement { get; private set; }

    public Achievement(string name, string description, Achievement nextAchievement = null)
    {
        Name = name;
        Description = description;
        IsUnlocked = false;
        NextAchievement = nextAchievement;
    }

    public void Unlock()
    {
        IsUnlocked = true;
        Debug.Log("업적 달성 : " + Name);
    }
}
