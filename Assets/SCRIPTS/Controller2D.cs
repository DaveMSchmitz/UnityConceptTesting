using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour
{
    const  float skinWidth = .015f;

    public BoxCollider2D _collider;
    private RaycastOrigins raycastOrigins;

    public int _horizontalRays = 4;
    public int _verticalRays = 4;
    public LayerMask collisionMask;
    public CollisionInfo Collision;
    
    private float _horizontalRaySpace;
    private float _verticalRaySpace;

    // Use this for initialization
    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        CalcRaySpace();
    }

    public void Move(Vector3 velocity)
    {
        UpdateRaycastOrigins();
        Collision.Reset();

        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);

        }

        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);

        }

        transform.Translate(velocity);
    }

    void HorizontalCollisions(ref Vector3 velocity) {
        float xDirection = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < _horizontalRays; i++) {
            Vector2 rayOrigin = (xDirection == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (_horizontalRaySpace * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * xDirection, rayLength, collisionMask);

            if (hit) {
                velocity.x = (hit.distance - skinWidth) * xDirection;
                rayLength = hit.distance;

                Collision.left = xDirection == -1;
                Collision.right = xDirection == 1;
            }

            Debug.DrawRay(rayOrigin, Vector2.right * xDirection * rayLength, Color.green);
        }
    }
    void VerticalCollisions(ref Vector3 velocity)
    {
        float yDirection = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth; 

        for (int i = 0; i < _verticalRays; i++)
        {
            Vector2 rayOrigin = (yDirection == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (_verticalRaySpace * i + velocity.x);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * yDirection, rayLength, collisionMask);

            if (hit)
            {
                velocity.y = (hit.distance-skinWidth) * yDirection;
                rayLength = hit.distance;

                Collision.below = yDirection == -1;
                Collision.above = yDirection == 1;
            }
       
            Debug.DrawRay(rayOrigin, Vector2.up * yDirection *rayLength, Color.green);
        }
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = _collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight= new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);

    }

    void CalcRaySpace()
    {
        Bounds bounds = _collider.bounds;
        bounds.Expand(skinWidth * -2);

        _horizontalRays = Mathf.Clamp(_horizontalRays, 2, int.MaxValue);
        _verticalRays = Mathf.Clamp(_verticalRays, 2, int.MaxValue);

        _horizontalRaySpace = bounds.size.y / (_horizontalRays - 1);
        _verticalRaySpace = bounds.size.x / (_verticalRays - 1);

    }

    private struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool right, left;

        public void Reset()
        {
            above = below = false;
            right = left = false;
        }
    }
}

