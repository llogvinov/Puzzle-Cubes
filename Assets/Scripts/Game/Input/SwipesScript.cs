using UnityEngine;
using DG.Tweening;

public class SwipesScript: MonoBehaviour
{
    [SerializeField] private WinChecker _winChecker;
    
    [Space]
    [SerializeField] private float _moveTime = 0.3f;
    
    private Vector2 _leftPosition;
    private Vector2 _rightPosition;

    private int _middleChildIndex;

    private void Start()
    {
        _middleChildIndex = _winChecker.HeadsContainer.childCount / 2;
        
        _leftPosition = _winChecker.HeadsContainer.GetChild(0).position;
        _rightPosition = _winChecker.HeadsContainer.GetChild(_winChecker.HeadsContainer.childCount - 1).position;
    }

    private void OnEnable()
    {
        SliderInpt.SwipedLeft += MoveContainerLeft;
        SliderInpt.SwipedRight += MoveContainerRight;
    }

    private void OnDisable()
    {
        SliderInpt.SwipedLeft -= MoveContainerLeft;
        SliderInpt.SwipedRight -= MoveContainerRight;
    }
    
    private void MoveContainerLeft(Transform container)
    {
        container
            .DOMoveX(-GameUIGenerator.SpriteWidth, _moveTime)
            .OnComplete(() => OnCompleteLeft(container));
    }

    private void MoveContainerRight(Transform container)
    {
        container
            .DOMoveX(GameUIGenerator.SpriteWidth, _moveTime)
            .OnComplete(() => OnCompleteRight(container));
    }
    
    private void OnCompleteLeft(Transform container)
    {
        PlaceContainerBack(container);
        AdjustContainerPartsLeft(container);
        _winChecker.CheckAllPartsMatch(_middleChildIndex);
    }
    
    private void OnCompleteRight(Transform container)
    {
        PlaceContainerBack(container);
        AdjustContainerPartsRight(container);
        _winChecker.CheckAllPartsMatch(_middleChildIndex);
    }

    private void PlaceContainerBack(Transform container)
    {
        container.position = Vector3.zero;
    }

    private void AdjustContainerPartsLeft(Transform container)
    {
        for (int i = 1; i < container.childCount; i++)
        {
            container.GetChild(i).position -= new Vector3(GameUIGenerator.SpriteWidth, 0f, 0f);
        }
        
        var firstChild = container.GetChild(0);
        firstChild.position = _rightPosition;

        firstChild.SetAsLastSibling();
    }
    
    private void AdjustContainerPartsRight(Transform container)
    {
        for (int i = 0; i < container.childCount - 1; i++)
        {
            container.GetChild(i).position += new Vector3(GameUIGenerator.SpriteWidth, 0f, 0f);
        }
        
        var lastChild = container.GetChild(container.childCount - 1);
        lastChild.position = _leftPosition;
        
        lastChild.SetAsFirstSibling();
    }
}