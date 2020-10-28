
using UnityEngine;

public class Aim : MonoBehaviour
{
    int yaw = 100;
    float Xyaw;
    float Yyaw;
    float rotateToY = 0;
    [SerializeField]
    private Transform player;
    [SerializeField]
    public Camera cam;
    // Update is called once per frame
    void Update()
    {

        #region Look
        Xyaw = Input.GetAxis("Mouse X");
        Yyaw = Input.GetAxis("Mouse Y");
        rotateToY -= Yyaw;
        rotateToY = Mathf.Clamp(rotateToY, -70, 80);
        player.Rotate(Vector3.up, Xyaw * Time.deltaTime * yaw);
        transform.localRotation = Quaternion.Euler(rotateToY, 0, 0);
        #endregion

    }

}
