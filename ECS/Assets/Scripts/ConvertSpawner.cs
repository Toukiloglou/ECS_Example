using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using UnityEngine;

public class ConvertSpawner : MonoBehaviour
{

    [SerializeField] private GameObject gameObjectPrefab;
    [SerializeField] int xSize = 10;
    [SerializeField] int ySize = 10;
    [Range(0.1f, 5.0f)] [SerializeField] float spacing = 1.5f;

    private Entity entityPrefab;    
    private EntityManager entityManager;

    void Start()
    {
        
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(
                        gameObjectPrefab,
                        GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null));

        InstatiateGrid(xSize, ySize, spacing);
            
    }

    private void InstantiateEntity(float3 position) {
        Entity myEntity = entityManager.Instantiate(entityPrefab);
        entityManager.SetComponentData(myEntity, new Translation {
            Value = position 
        });
    }


    private void InstatiateGrid(int dimX, int dimY, float spacing = 1f) {
        for (int i = 0; i < dimX; i++) {
            for (int j = 0; j < dimY; j++) {
                InstantiateEntity(new float3(i * spacing, j * spacing, 0f));
            }
        }
    }


}
