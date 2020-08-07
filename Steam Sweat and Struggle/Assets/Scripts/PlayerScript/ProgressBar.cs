using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{

    [SerializeField]
    private GameObject full;
    [SerializeField]
    private GameObject progress;

    // Start is called before the first frame update
    void Start()
    {
        SetProgress(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    protected void SetProgress(object v)
    {
        float value = (float) v;
        if (value<0.01f) {
            progress.SetActive(false);
        } else {
            progress.SetActive(true);
            Vector3 scale = progress.transform.localScale;
            progress.transform.localScale = new Vector3(value, scale.y, scale.z);
        }
    }
}
