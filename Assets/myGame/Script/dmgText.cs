using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class dmgText : MonoBehaviour
{
    TextMeshPro tmp;
    Color alpha;
    void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    void OnEnable()
    {
        Invoke("DestroyObject", 2);
        alpha = new Color32(255, 50, 50, 255);
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
        alpha.a = Mathf.Lerp(alpha.a, 0f, Time.deltaTime * 2);
        tmp.color = alpha;
    }

    void DestroyObject()
    {
        ObjectPool.Instance.ReturnObject(gameObject);
    }
}
