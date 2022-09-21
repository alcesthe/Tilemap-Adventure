using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Bow : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] float launchForce;
    [SerializeField] Transform shotPoint;

    [SerializeField] GameObject point;
    GameObject[] points;
    [SerializeField] int numberOfPoints;
    [SerializeField] float spaceBetweenPoints;

    
    private Vector2 direction;

    private void Start()
    {
        points = new GameObject[numberOfPoints];
        /*for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - bowPosition;

        direction.y = Mathf.Clamp(direction.y, 0.5f, 1.3f);
        direction.x = Mathf.Abs(direction.x);
        Debug.Log(direction);


        transform.right = direction;

        if (CrossPlatformInputManager.GetButtonDown("Fire1") && (direction.x * transform.parent.gameObject.transform.localScale.x) > Mathf.Epsilon)
        {
            Shoot();
        }

/*        for(int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }*/
    }

    private void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }

    Vector2 PointPosition (float t)
    {
        Vector2 pos = (Vector2)shotPoint.position + (direction.normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return pos;
    }
}
