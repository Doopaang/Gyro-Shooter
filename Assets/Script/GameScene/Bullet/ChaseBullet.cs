using UnityEngine;

public class ChaseBullet : Bullet
{
    private float rotateSpeed;

    private Vector3 vec;
    private float targetDistance;

    void Start()
    {
        targetDistance = (-GameManager.Instance.Player.transform.position).magnitude;
    }

    void Update()
    {
        vec = -transform.position;

        if (GameManager.Instance.Player != null &&
            vec.magnitude < targetDistance)
        {
            Quaternion rotate = Quaternion.LookRotation(Vector3.forward, GameManager.Instance.Player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, rotateSpeed * Time.timeScale);
        }
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void SetBullet(float floatValue)
    {
        rotateSpeed = floatValue * 0.1f;
    }
}