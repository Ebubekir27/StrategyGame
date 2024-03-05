using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScrollViewController : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform viewPortTransfrom;
    public RectTransform contentPanelTransfrom;
    public VerticalLayoutGroup VerticalLayoutGroup;
    public RectTransform[] ItemList;
    Vector2 _oldVelocity;
    bool _isUpdated;
    private void Start()
    {
        int itemsToAdd = Mathf.CeilToInt(viewPortTransfrom.rect.height / ItemList[0].rect.height + VerticalLayoutGroup.spacing);
        contentPanelTransfrom.localPosition = new Vector3(
            contentPanelTransfrom.localPosition.x,
            (-ItemList[0].rect.height+VerticalLayoutGroup.spacing)* itemsToAdd,
            contentPanelTransfrom.localPosition.z);
    }
    void Update()
    {
        if (_isUpdated)
        {
            _isUpdated = false;
            scrollRect.velocity=_oldVelocity;
        }

        if (contentPanelTransfrom.localPosition.y>0)
        {
            Canvas.ForceUpdateCanvases();
            _oldVelocity = scrollRect.velocity;
            contentPanelTransfrom.localPosition -= new Vector3(0,ItemList.Length * (ItemList[0].rect.height + VerticalLayoutGroup.spacing), 0);
            _isUpdated=true;
        }
        if (contentPanelTransfrom.localPosition.y <   - (ItemList.Length * (ItemList[0].rect.width+VerticalLayoutGroup.spacing)))
        {
            Canvas.ForceUpdateCanvases();
            _oldVelocity = scrollRect.velocity;
            contentPanelTransfrom.localPosition += new Vector3(0, ItemList.Length * (ItemList[0].rect.height + VerticalLayoutGroup.spacing), 0);
            _isUpdated = true;
        }
    }
}
