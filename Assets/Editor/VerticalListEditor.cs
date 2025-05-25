using UnityEngine;
using UnityEditor;

public class VerticalListEditor : EditorWindow
{
    private GameObject parentObject;
    private float topY = 5f;
    private float bottomY = -5f;

    [MenuItem("Tools/Vertical List Organizer")]
    public static void ShowWindow()
    {
        GetWindow<VerticalListEditor>("Vertical List Organizer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Organize Children Vertically", EditorStyles.boldLabel);

        parentObject = (GameObject)EditorGUILayout.ObjectField("Parent Object", parentObject, typeof(GameObject), true);
        topY = EditorGUILayout.FloatField("Top Y Position", topY);
        bottomY = EditorGUILayout.FloatField("Bottom Y Position", bottomY);

        if (GUILayout.Button("Organize"))
        {
            OrganizeChildren();
        }
    }

    private void OrganizeChildren()
    {
        if (parentObject == null)
        {
            Debug.LogError("No parent object selected.");
            return;
        }

        int childCount = parentObject.transform.childCount;
        if (childCount < 3)
        {
            Debug.LogWarning("Parent must have at least three children.");
            return;
        }

        Transform firstChild = parentObject.transform.GetChild(0);
        Transform lastChild = parentObject.transform.GetChild(childCount - 1);

        firstChild.localPosition = new Vector3(firstChild.localPosition.x, topY, firstChild.localPosition.z);
        lastChild.localPosition = new Vector3(lastChild.localPosition.x, bottomY, lastChild.localPosition.z);

        float step = (topY - bottomY) / (childCount - 1);

        for (int i = 1; i < childCount - 1; i++)
        {
            Transform child = parentObject.transform.GetChild(i);
            float newY = topY - (step * i);
            child.localPosition = new Vector3(child.localPosition.x, newY, child.localPosition.z);
        }
    }
}
