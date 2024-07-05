using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class Track : MonoBehaviour
{
    public ARTrackedImageManager manager;
    public List<GameObject> list1 = new List<GameObject>();
    public List<AudioClip> list2 = new List<AudioClip>();

    private Dictionary<string, GameObject> dict1 = new Dictionary<string, GameObject>();
    private Dictionary<string, AudioClip> dict2 = new Dictionary<string, AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject o in list1)
        {
            dict1.Add(o.name, o);
            o.SetActive(false);  // 초기에는 비활성화
        }

        foreach (AudioClip o in list2)
        {
            dict2.Add(o.name, o);
        }
    }

    void UpdateImage(ARTrackedImage t)
    {
        string name = t.referenceImage.name;

        if (dict1.TryGetValue(name, out GameObject o))
        {
            if (t.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                //트래킹 되고 있는 상태인 경우에만
                o.transform.position = t.transform.position;
                o.transform.rotation = t.transform.rotation;
                o.SetActive(true);
            }
            else
            {
                o.SetActive(false);
            }
        }
    }

    void UpdateSound(ARTrackedImage t)
    {
        string name = t.referenceImage.name;

        if (dict2.TryGetValue(name, out AudioClip sound))
        {
            GetComponent<AudioSource>().PlayOneShot(sound);
        }
    }

    private void OnChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage t in args.added)
        {
            UpdateImage(t);
            UpdateSound(t);
        }

        foreach (ARTrackedImage t in args.updated)
        {
            UpdateImage(t);
        }
    }

    private void OnEnable()
    {
        manager.trackedImagesChanged += OnChanged;
    }

    private void OnDisable()
    {
        manager.trackedImagesChanged -= OnChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
