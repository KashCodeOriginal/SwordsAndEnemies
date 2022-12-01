using System.Collections;
using UnityEngine;

namespace Services.CoroutineRunner
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}