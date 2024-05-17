using System;
using System.Collections.Generic;
using Classes;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    // This class is used for managing annotations in the scene, intended for editor use only
    public class AnnotationManager : MonoBehaviour
    {
        // Constants for annotation appearance
        public const float CapSize = 0.15f;
        public const float CapMargin = 0.2f;
        public static readonly Vector3 AnnotationOffset = Vector3.right * (CapSize + CapMargin);
        
        // List of annotations to be displayed in the scene
        public List<Annotation> annotations;

        // This method is called when the script is reset or added to a GameObject
        private void Reset()
        {
            // Tag the GameObject as "EditorOnly" so it doesn't appear in builds
            gameObject.tag = "EditorOnly";
        }

#if UNITY_EDITOR
        // This method is called to draw Gizmos in the editor
        private void OnDrawGizmos()
        {
            // Only draw Gizmos if the current GameObject is not selected
            if (UnityEditor.Selection.activeGameObject != gameObject)
            {
                // Save the current color of Handles
                var gizmoColor = Handles.color;
                // Set a semi-transparent white color for the annotations
                Handles.color = new Color(1, 1, 1, 0.75f);

                // Draw each annotation in the annotations list
                foreach (var annotation in annotations)
                {
                    // Calculate the position of the annotation with an offset
                    var pos = annotation.position + Quaternion.LookRotation(Camera.current.transform.forward, Camera.current.transform.up) * AnnotationManager.AnnotationOffset;
                    // Draw the annotation text at the calculated position
                    UnityEditor.Handles.Label(pos, annotation.text);
                }
                
                // Restore the original color of Handles
                Handles.color = gizmoColor;
            }
        }
#endif
    }
}
