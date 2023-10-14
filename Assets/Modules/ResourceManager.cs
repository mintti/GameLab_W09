using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ResourceManager : MonoBehaviour
{
    #region Singleton
    public static ResourceManager I;
    private void Awake()
    {
        if (I == null)
        {
            I = this;
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private List<Mesh> _unitMeshByLevel;
    public List<Mesh> UnitMeshByLevel => _unitMeshByLevel;
    
    private List<Material> _classColorMaterial;
    public List<Material> ClassColorMaterial => _classColorMaterial;

    [SerializeField] private GameObject _unitPrefab;
    public GameObject UnitPrefab => _unitPrefab;
    
    public List<IEnv> Envs { get; private set; }

    public void Init()
    {
        InitEnv();
        InitClassData();
    }

    #region Init
    private void InitClassData()
    {
        var meshs = Resources.LoadAll<Mesh>("Unit");
        _unitMeshByLevel = new()
        {
            null,
            meshs.FirstOrDefault(x=> x.name.Equals("pawn")),
            meshs.FirstOrDefault(x=> x.name.Equals("bishop")),
            meshs.FirstOrDefault(x=> x.name.Equals("king")),
        };

        _classColorMaterial = new();
        for (int i = 0, cnt = Enum.GetNames(typeof(ClassType)).Length; i < cnt; i++)
        {
            string name = ((ClassType)i).ToString();
            var mater = Resources.Load<Material>($"Unit/{name}/Color");
            _classColorMaterial.Add(mater);
        }
    }
    private void InitEnv()
    {
        Envs = new()
        {
            null,
            new SlimeForest(1, new () { EnemyType.Slime ,EnemyType.AnimalSlime,EnemyType.SteelSlime}, 2, BossType.SlimeKing ),
            new DevilCastle(2, new () { EnemyType.SmallDemon, EnemyType.MediumDemon, EnemyType.LargeDemon}, 2, BossType.DemonKing ),
        };
    }
    #endregion
   

    public IUnit GetUnit(ClassType type)
    {
        return type switch
        {
            ClassType.Knight => new Knight(type, new () { UnitType.Battle , UnitType.Defense }),
            ClassType.Wizard => new Wizard(type, new () { UnitType.Battle ,UnitType.Research }),
            ClassType.Manager => new Manager(type, new () { UnitType.Enigma ,UnitType.Research}),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
    
    public IEnemy GetEnemy(EnemyType type)
    {
        return type switch
        {
            EnemyType.Slime => new Slime(nameof(Slime), 5, 2),
            EnemyType.AnimalSlime => new AnimalSlime(nameof(AnimalSlime), 5, 2),
            EnemyType.SteelSlime => new SteelSlime(nameof(SteelSlime), 5, 2),
            EnemyType.SmallDemon => new SmallDemon(nameof(SmallDemon), 5, 2),
            EnemyType.MediumDemon => new MediumDemon(nameof(MediumDemon), 5, 2),
            EnemyType.LargeDemon => new LargeDemon(nameof(LargeDemon), 5, 2),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public IEnemy GetBoss(BossType type)
    {
        return type switch
        {
            BossType.SlimeKing => new SlimeKing(nameof(SlimeKing), 10, 3),
            BossType.DemonKing => new DemonKing(nameof(DemonKing), 10, 3),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}