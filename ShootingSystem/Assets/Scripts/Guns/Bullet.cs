using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    #region Fields
    Timer rangeTimer;

    [SerializeField]
    float speed = 250f;

    Vector3 velocity;

    Vector3 predictedVelocity;

    [SerializeField]
    float distance = 1000;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rangeTimer = Camera.main.GetComponent<Timer>();
        rangeTimer.Duration = (distance / speed) * Time.deltaTime;
        rangeTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if(rangeTimer.Finished)
        {
            Destroy(gameObject);
        }

        Vector3 position = transform.position;
        velocity = transform.forward * speed;
        Vector3 predictedVelocity = velocity + Physics.gravity * Time.deltaTime;
        predictedVelocity = predictedVelocity + Physics.gravity * Time.deltaTime;
        Vector3 predictedDestination = position + predictedVelocity * Time.deltaTime;
        transform.position = predictedDestination;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
