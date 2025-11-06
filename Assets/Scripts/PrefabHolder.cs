using UnityEngine;

public class PrefabHolder : MonoBehaviour
{
    private static PrefabHolder _i;

    public static PrefabHolder I
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<PrefabHolder>("PrefabStorer"));
            return _i;
        }
    }

    public Transform dmgTxt;
}
