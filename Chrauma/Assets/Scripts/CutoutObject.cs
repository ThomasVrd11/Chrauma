using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private LayerMask wallMask;
    private Camera mainCamera;
    private HashSet<Renderer> lastAffectedRenderers = new HashSet<Renderer>();
    private Vector3 lastPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        lastPosition = targetObject.position;
    }

    void Update()
    {
        if (ShouldUpdateCutout())
        {
            UpdateCutout();
        lastPosition = targetObject.position;

        }
    }

    private bool ShouldUpdateCutout()
    {
        float distanceMoved = Vector3.Distance(lastPosition, targetObject.position);
        return distanceMoved > 0.5f;
    }
    // Nico je comprends rien serieux 
    
    private void UpdateCutout()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);
        Debug.DrawRay(transform.position, offset, Color.red);

        HashSet<Renderer> currentlyAffectedRenderers = new HashSet<Renderer>();

        foreach (var hit in hitObjects)
        {
            ProcessAllRenderers(hit.transform, (renderer) =>
            {
                currentlyAffectedRenderers.Add(renderer);
                ApplyCutout(renderer, cutoutPos, 0.15f, 0.05f);
            });
        }

        ResetOldCutouts(currentlyAffectedRenderers);
        lastAffectedRenderers = currentlyAffectedRenderers;
    }

    private void ApplyCutout(Renderer renderer, Vector2 cutoutPos, float size, float falloff)
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(propBlock);
        propBlock.SetVector("_CutoutPos", new Vector4(cutoutPos.x, cutoutPos.y, 0, 0));
        propBlock.SetFloat("_CutoutSize", size);
        propBlock.SetFloat("_FalloffSize", falloff);
        renderer.SetPropertyBlock(propBlock);
    }

    private void ResetOldCutouts(HashSet<Renderer> currentlyAffectedRenderers)
    {
        foreach (var renderer in lastAffectedRenderers)
        {
            if (!currentlyAffectedRenderers.Contains(renderer))
            {
                MaterialPropertyBlock propBlockDone = new MaterialPropertyBlock();
                renderer.GetPropertyBlock(propBlockDone);
                propBlockDone.SetFloat("_CutoutSize", 0);
                renderer.SetPropertyBlock(propBlockDone);
            }
        }
    }

    private void ProcessAllRenderers(Transform root, System.Action<Renderer> process)
    {
        Renderer renderer = root.GetComponent<Renderer>();
        if (renderer != null)
        {
            process(renderer);
        }
        foreach (Transform child in root)
        {
            ProcessAllRenderers(child, process);
        }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
    //     Vector3 offset = targetObject.position - transform.position;
    //     RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);
    //     Debug.DrawRay(transform.position, offset, Color.red);

    //     HashSet<Renderer> currentlyAffectedRenderers = new HashSet<Renderer>();

    //     MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
    //     foreach (var hit in hitObjects)
    //     {
    //         ProcessAllRenderers(hit.transform, (renderer) =>
    //         {
    //             currentlyAffectedRenderers.Add(renderer);
    //             MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
    //             renderer.GetPropertyBlock(propBlock);  // Load current properties into the block
    //             propBlock.SetVector("_CutoutPos", new Vector4(cutoutPos.x, cutoutPos.y, 0, 0));
    //             propBlock.SetFloat("_CutoutSize", 0.15f);
    //             propBlock.SetFloat("_FalloffSize", 0.05f);
    //             renderer.SetPropertyBlock(propBlock);
    //         });
    //     }
    //     foreach (var renderer in lastAffectedRenderers)
    //     {
    //         if (!currentlyAffectedRenderers.Contains(renderer))
    //         {
    //             MaterialPropertyBlock propBlockDone = new MaterialPropertyBlock();
    //             renderer.GetPropertyBlock(propBlockDone);
    //             propBlockDone.SetFloat("_CutoutSize", 0);
    //             renderer.SetPropertyBlock(propBlockDone);
    //         }
    //     }
    //     lastAffectedRenderers = currentlyAffectedRenderers;
    // }
    // // Helper method to process all renderers in the hierarchy
    // private void ProcessAllRenderers(Transform root, System.Action<Renderer> process)
    // {
    //     Renderer renderer = root.GetComponent<Renderer>();
    //     if (renderer != null)
    //     {
    //         Debug.Log("Processing renderer on: " + root.name); // Output which GameObject is being processed
    //         process(renderer);
    //     }
    //     foreach (Transform child in root)
    //     {
    //         ProcessAllRenderers(child, process);
    //     }
    // }
}
