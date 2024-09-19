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
    public List<GameObject> list1 = new List<GameObject>(); // �̹��� ���� ��Ÿ�� ������Ʈ
    public List<AudioClip> list2 = new List<AudioClip>(); // ù ��° �Ҹ� ����Ʈ
    public List<AudioClip> list3 = new List<AudioClip>(); // �� ��° �Ҹ� ����Ʈ
    public List<GameObject> list4 = new List<GameObject>(); // �ؽ�Ʈ ����Ʈ

    public GameObject effectPrefab; // ����Ʈ ������

    public Button playButton1; // ù ��° ��ư
    public Button playButton2; // �� ��° ��ư

    private Dictionary<string, GameObject> dict1 = new Dictionary<string, GameObject>();
    private Dictionary<string, AudioClip> dict2 = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> dict3 = new Dictionary<string, AudioClip>(); // �� ��° �Ҹ��� ���� ��ųʸ�
    private Dictionary<string, GameObject> dict4 = new Dictionary<string, GameObject>(); // �ؽ�Ʈ ��ųʸ�
    private Dictionary<string, GameObject> instantiatedEffects = new Dictionary<string, GameObject>(); // ����Ʈ�� ���� ��ųʸ�

    private string currentTrackedImageName; // ���� Ʈ��ŷ�� �̹��� �̸�

    void Start()
    {
        foreach (GameObject o in list1)
        {
            dict1.Add(o.name, o);
            o.SetActive(false);  // �ʱ⿡�� ��Ȱ��ȭ
        }

        foreach (AudioClip o in list2)
        {
            dict2.Add(o.name, o);
        }

        foreach (AudioClip o in list3)
        {
            dict3.Add(o.name, o);
        }

        foreach(GameObject tt in list4)
        {
            dict4.Add(tt.name, tt);
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
                if (o.CompareTag("Rotate180"))
                {
                    o.transform.rotation = t.transform.rotation * Quaternion.Euler(0, 180, 0);
                }
                else if (o.CompareTag("Rotate-90"))
                {
                    o.transform.rotation = t.transform.rotation * Quaternion.Euler(-90, 0, 0);
                }
                else if (o.CompareTag("Rotate90"))
                {
                    o.transform.rotation = t.transform.rotation * Quaternion.Euler(90, 0, 0);
                }
                else if (o.CompareTag("Rotate00180"))
                {
                    o.transform.rotation = t.transform.rotation * Quaternion.Euler(0, 0, 180);
                }
                else
                {
                    o.transform.rotation = t.transform.rotation;
                }
                //o.transform.rotation = t.transform.rotation * Quaternion.Euler(0, 180, 0);
                //o.transform.rotation = t.transform.rotation;
                o.SetActive(true);

                // ����Ʈ �߰�
                if (!instantiatedEffects.ContainsKey(name))
                {
                    GameObject effect = Instantiate(effectPrefab, t.transform.position, t.transform.rotation);
                    effect.transform.SetParent(o.transform);
                    ParticleSystem particleSystem = effect.GetComponent<ParticleSystem>();
                    if (particleSystem != null)
                    {
                        particleSystem.Play();
                    }
                    instantiatedEffects[name] = effect;
                }

                currentTrackedImageName = name; // ���� Ʈ��ŷ�� �̹��� ������Ʈ
            }
            else
            {
                o.SetActive(false);

                if (currentTrackedImageName == name)
                {
                    currentTrackedImageName = null; // �̹����� Ʈ��ŷ���� ���� ��� �ʱ�ȭ
                }
            }
        }
    }

    void UpdateText(ARTrackedImage t)
    {
        string name = t.referenceImage.name;
        if (dict4.TryGetValue(name, out GameObject o))
        {
            if (t.trackingState == TrackingState.Tracking)
            {
                o.SetActive(true);

                currentTrackedImageName = name; // ���� Ʈ��ŷ�� �̹��� ������Ʈ
            }
            else
            {
                o.SetActive(false);

                if (currentTrackedImageName == name)
                {
                    currentTrackedImageName = null; // �̹����� Ʈ��ŷ���� ���� ��� �ʱ�ȭ
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
            UpdateText(t);
            
        }

        foreach (ARTrackedImage t in args.updated)
        {
            UpdateImage(t);
            UpdateText(t);
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
