using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [Header("Game Manager")]
    [SerializeField] private Transform levelsContainer;
    [SerializeField] private List<LevelControl> levelPrefabs;// Will be changed to addressables.
    [SerializeField] private Dictionary<LevelControl, LevelControl> levels = new Dictionary<LevelControl, LevelControl>();
    private LevelControl currentLevel;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 120;
    }

    public void LoadLevel(int index) 
    {
        currentLevel = GetLevelInstance(levelPrefabs[index]);
        currentLevel.transform.position = Vector3.zero;
        currentLevel.Init();
    }
    public LevelControl GetLevelInstance(LevelControl prefab)
    {
        if (!levels.ContainsKey(prefab))
            CreateLevelInstance(prefab);
        return levels[prefab];
    }
    public void CreateLevelInstance(LevelControl prefab)// Will be changed to addressables.
    {
        LevelControl loadedLevel = Instantiate(prefab.gameObject, levelsContainer).GetComponent<LevelControl>();
        levels.Add(prefab, loadedLevel);
    }
}
