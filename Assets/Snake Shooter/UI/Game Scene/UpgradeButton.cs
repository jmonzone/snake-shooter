using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Text towerNameText;
    [SerializeField] private Image towerImage;
    [SerializeField] private Text priceText;

    public Button PurchaseButton => purchaseButton;
    public Text TowerNameText => towerNameText;
    public Image TowerImage => towerImage;
    public Text PriceText => priceText;
}
