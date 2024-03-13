using UnityEngine;
using System.Threading.Tasks;

namespace Powerlifter.Utilities
{
    public class Initialization : MonoBehaviour
    {
        async void Start()
        {
            await Task.Delay(2000);
            Application.targetFrameRate = 60;
            Destroy(gameObject);
        }
    }
}
