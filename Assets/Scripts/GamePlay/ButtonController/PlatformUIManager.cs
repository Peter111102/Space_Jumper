using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlatformUIManager : MonoBehaviour
{
    [System.Serializable]
    public class PlatformSlot
    {
        public int maxUses = 10;
        public float cooldown = 1f;
        public PlatformData data;
        public Button button;
        public Image cooldownImage;
        public Text countText;
        [HideInInspector] public int currentStock;
        [HideInInspector] public float currentCD;
    }

    public List<PlatformSlot> slots; // Configura i 5 slot nell'Inspector
    public GameObject platformPrefab;
    public Transform limitY; // Vecchio "Constante"

    private PlatformSlot selectedSlot;

    void Start()
    {
        foreach (var slot in slots)
        {
            slot.currentStock = slot.maxUses;
            slot.button.onClick.AddListener(() => selectedSlot = slot);
            UpdateUI(slot);
        }
    }

    void Update()
    {
        HandleCooldowns();

        if (selectedSlot != null && Input.GetKeyDown(KeyCode.Mouse0))
        {
            TrySpawn();
        }
    }

    void HandleCooldowns()
    {
        foreach (var slot in slots)
        {
            if (slot.currentCD > 0)
            { 
                slot.currentCD -= Time.deltaTime;
                slot.cooldownImage.fillAmount = slot.currentCD / slot.cooldown;
            }
        }
    }

    void TrySpawn()
    {
        Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mPos.y < limitY.position.y || selectedSlot.currentCD > 0 || selectedSlot.currentStock <= 0) return;

        mPos.z = 0;
        mPos.x = Mathf.Clamp(mPos.x, -2f, 2f);

        GameObject go = Instantiate(platformPrefab, mPos, Quaternion.identity);
        go.GetComponent<PlatformController>().Setup(selectedSlot.data);

        selectedSlot.currentStock--;
        selectedSlot.currentCD = selectedSlot.cooldown;
        UpdateUI(selectedSlot);
        selectedSlot = null; // Deseleziona
    }

    void UpdateUI(PlatformSlot slot)
    {
        slot.countText.text = "x" + slot.currentStock;
        slot.button.interactable = slot.currentStock > 0;
    }
}