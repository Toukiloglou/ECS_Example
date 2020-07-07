using System.Collections;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class JobExample : MonoBehaviour
{
    
    void Start()
    {
        DoExample();
    }

    private void DoExample() {

        NativeArray<float> resultArray = new NativeArray<float>(1, Allocator.TempJob);

        SimpleJob myJob = new SimpleJob {
            a = 5f,
            result = resultArray
        };

        JobHandle handle = myJob.Schedule();

        handle.Complete();

        float resultingValue = resultArray[0];
        Debug.Log(resultingValue);

        resultArray.Dispose();
    }

    private struct SimpleJob : IJob {

        public float a;
        public NativeArray<float> result;        
        public void Execute() {
            result[0] = a;
        }
    }


}
