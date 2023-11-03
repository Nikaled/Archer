using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;

public class ArcherController : MonoBehaviour
{
    [SerializeField] Transform projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] float launchForce;
    [SerializeField] float trajectoryTimeStep;
    [SerializeField] int trajectoryStepCount;
    [SerializeField] GameObject archer;
    [SerializeField] GameObject arrow;
    [SerializeField] ArcherAnimation archerAnimation;

    Vector2 velocity, startMousePosition, currentMousePosition;
    Quaternion defaultRotation;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            archerAnimation.AttackStart(1);
        }
        if (Input.GetMouseButton(0))
        {
            arrow.SetActive(true);
            archerAnimation.AttackTarget(0);

            currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            velocity = (startMousePosition - currentMousePosition) * launchForce;
            DrawTrajectory();
            RotateArcher(archer);
        }
        if (Input.GetMouseButtonUp(0))
        {
            FireProjectile();
            ClearTrajectory();
            archerAnimation.AttackFinish(0);
            arrow.SetActive(false);
            UnRotate(archer);
            archerAnimation.Idle(1);
        }
    }
    void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[trajectoryStepCount];
        for (int i = 0; i < trajectoryStepCount; i++)
        {
            float time = i * trajectoryTimeStep;
            Vector3 CurrentDotPosition = (Vector2)spawnPoint.position + velocity * time + 0.5f * Physics2D.gravity*time*time;

            positions[i] = CurrentDotPosition;
        }
        lineRenderer.positionCount = trajectoryStepCount;
        lineRenderer.SetPositions(positions);
    }
    void RotateArcher(GameObject Archer)
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        defaultRotation = Archer.transform.rotation;
        Archer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void UnRotate(GameObject Archer)
    {
        Archer.transform.rotation = defaultRotation;
    }
    private void FireProjectile()
    {
        Transform projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = velocity;

    }
    void ClearTrajectory()
    {
        lineRenderer.positionCount = 0;

    }
}

