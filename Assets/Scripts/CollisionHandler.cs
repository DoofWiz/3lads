using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You hit the: " + gameObject.name);
                break;
            case "Finish":
                Debug.Log("Congrats! You finished!");
                break;
            case "Fuel":
                Debug.Log("You collected some fuel.");
                break;
            default:
                Debug.Log("Oops. You blew up.");
                break;
        }
    }
}
