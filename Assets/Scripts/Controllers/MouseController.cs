using UnityEngine;

namespace CityBuilderTest
{
    /// <summary>
    /// The mouse controller class.
    /// </summary>
    public class MouseController : IController
    {
        public bool HasFired()
        {
            return Input.GetKeyDown(KeyCode.Mouse0);
        }

        public Vector2 Position()
        {
            return Input.mousePosition;
        }
    }
}

