using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class Track : MonoBehaviour
{
    public ARTrackedImageManager manager;
    public List<GameObject> list1 = new List<GameObject>();
    public List<AudioClip> list2 = new List<AudioClip>(); // 첫 번째 소리 리스트
    public List<AudioClip> list3 = new List<AudioClip>(); // 두 번째 소리 리스트
    public Button playButton1; // 첫 번째 버튼
    public Button playButton2; // 두 번째 버튼

    private Dictionary<string, GameObject> dict1 = new Dictionary<string, GameObject>();
    private Dictionary<string, AudioClip> dict2 = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> dict3 = new Dictionary<string, AudioClip>(); // 두 번째 소리를 위한 딕셔너리
    private string currentTrackedImageName; // 현재 트래킹된 이미지 이름

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

        foreach (AudioClip o in list3)
        {
            dict3.Add(o.name, o);
        }

        if (playButton1 != null)
        {
            playButton1.onClick.AddListener(OnPlayButton1Clicked);
        }

        if (playButton2 != null)
        {
            playButton2.onClick.AddListener(OnPlayButton2Clicked);
        }
    }

    void UpdateImage(ARTrackedImage t)
    {
        string name = t.referenceImage.name;

        if (dict1.TryGetValue(name, out GameObject o))
        {
            if (t.trackingState == TrackingState.Tracking)
            {
                o.transform.position = t.transform.position;
                o.transform.rotation = t.transform.rotation;
                o.SetActive(true);

                currentTrackedImageName = name; // 현재 트래킹된 이미지 업데이트
            }
            else
            {
                o.SetActive(false);

                if (currentTrackedImageName == name)
                {
                    currentTrackedImageName = null; // 이미지가 트래킹되지 않을 경우 초기화
                }
            }
        }
    }

    void UpdateSound1(string name)
    {
        if (dict2.TryGetValue(name, out AudioClip sound))
        {
            GetComponent<AudioSource>().PlayOneShot(sound);
        }
    }

    void UpdateSound2(string name)
    {
        if (dict3.TryGetValue(name, out AudioClip sound))
        {
            GetComponent<AudioSource>().PlayOneShot(sound);
        }
    }

    private void OnChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage t in args.added)
        {
            UpdateImage(t);
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

    public void OnPlayButton1Clicked()
    {
        if (!string.IsNullOrEmpty(currentTrackedImageName))
        {
            UpdateSound1(currentTrackedImageName);
        }
    }

    public void OnPlayButton2Clicked()
    {
        if (!string.IsNullOrEmpty(currentTrackedImageName))
        {
            UpdateSound2(currentTrackedImageName);
        }
    }
}
