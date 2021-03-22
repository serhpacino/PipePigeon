
using UnityEngine;
using UnityEditor;


public class PipeEditor : EditorWindow
{

    Sprite sprite;
    Color color = new Color(0,0,0,255);
    string objectName;
    
    [MenuItem("Tools/Pipe Editor")]
    static void OpenWindow()
    {
        GetWindow(typeof(PipeEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Pipe Editor Tool", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        objectName = EditorGUILayout.TextField("Object Name", objectName);
        sprite = EditorGUILayout.ObjectField("Sprite", sprite, typeof(Sprite), false) as Sprite;
        color= EditorGUILayout.ColorField("Color",color );
        if (GUILayout.Button("Create a Prefab"))
        {
            CreatePrefab();
        }
    }
    private void CreatePrefab()
    {
        GameObject firstPipe = new GameObject();
        GameObject secondPipe = new GameObject();
        CreateChildObject(firstPipe, false);
        CreateChildObject(secondPipe, true);
        
        GameObject prefab = new GameObject();
        var boxcolliderPrefab = prefab.AddComponent<BoxCollider2D>();
        boxcolliderPrefab.size = new Vector2(1.2f, 12);
        boxcolliderPrefab.offset = new Vector2(5f, 1.5f);
        boxcolliderPrefab.isTrigger = true;
        var rigidbodyPrefab = prefab.AddComponent<Rigidbody2D>();
        rigidbodyPrefab.bodyType = RigidbodyType2D.Kinematic;
        var scriptPrefab = prefab.AddComponent<Pipe>();
        firstPipe.transform.SetParent(prefab.transform, false);
        firstPipe.transform.position = new Vector3(5, -2, 0);
        secondPipe.transform.SetParent(prefab.transform, false);
        secondPipe.transform.position = new Vector3(5, 5, 0);
        string localPath = "Assets/Resources/Prefabs/" + objectName + ".prefab";
        // Make sure the file name is unique, in case an existing Prefab has the same name.
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        // Create the new Prefab.
        PrefabUtility.SaveAsPrefabAsset(prefab, localPath);
        
    }

    private void CreateChildObject(GameObject gameObject, bool flipY)
    {
        var boxcolliderObject = gameObject.AddComponent<BoxCollider2D>();
        boxcolliderObject.size = new Vector2(1.004f, 4.9f);
        var spriteObject = gameObject.AddComponent<SpriteRenderer>();
        spriteObject.sprite = sprite;
        Color spriteColor = gameObject.GetComponent<SpriteRenderer>().color;
        spriteColor = color;
        gameObject.GetComponent<SpriteRenderer>().color = spriteColor;

        //spriteObject.color= color;
        spriteObject.flipY = flipY;
        spriteObject.drawMode = SpriteDrawMode.Sliced;
        spriteObject.size = new Vector2(1.2f, 5.03f);
        var rigidbodyObject = gameObject.AddComponent<Rigidbody2D>();
        rigidbodyObject.gravityScale = 0;
        rigidbodyObject.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
