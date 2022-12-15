 using Spawners.Enemy;
 using UnityEngine;
 using UnityEngine.AddressableAssets;

 namespace Data.Static
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        [field: SerializeField] public MonsterTypeId MonsterTypeId { get; private set; }
        
        [field: SerializeField] public string Name { get; private set; }
        [field: Range(1, 100)]
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: Range(1, 100)]
        [field: SerializeField] public float MaxHealthPoints { get; private set; }
        [field: Range(1, 30)]
        [field: SerializeField] public float Damage { get; private set; }
        [field: Range(0.1f, 2)]
        [field: SerializeField] public float EffectiveDistance { get; private set; }
        [field: Range(0.1f, 2)]
        [field: SerializeField] public float Cleavage { get; private set; }
        [field: Range(0.1f, 10)]
        [field: SerializeField] public float AttackCooldown { get; private set; }
        
        [field: Range(0.1f, 10)]
        [field: SerializeField] public float MovementSpeed { get; private set; }

        [field: SerializeField] public int MinLoot { get; private set; }
        [field: SerializeField] public int MaxLoot { get; private set; }
        
        [field: SerializeField] public AssetReferenceGameObject PrefabReference { get; private set; }
    }
}