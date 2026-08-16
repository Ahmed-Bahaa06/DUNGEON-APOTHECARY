using UnityEngine;

public class MonsterIconVisual : MonoBehaviour
{
    [SerializeField] private Monster monster;
    [SerializeField] private SpriteRenderer cureSprite;
    [SerializeField] private SpriteRenderer ingredientOneSprite;
    [SerializeField] private SpriteRenderer ingredientTwoSprite;
    [SerializeField] private GameObject floatingIconContainer;

    private void Start()
    {
        SetSprite();
    }

    private void OnEnable()
    {
        monster.OnHealed += Hide;
    }

    private void OnDisable()
    {
        monster.OnHealed -= Hide;
    }

    public void ResetVisual()
    {
        floatingIconContainer.SetActive(true);

        SetSprite();
    }

    private void Hide()
    {
        floatingIconContainer.SetActive(false);
    }

    public void SetSprite()
    {
        cureSprite.sprite = monster.Recipe.RequiredCure.sprite;
        ingredientOneSprite.sprite = monster.Recipe.RequiredIngredients[0].sprite;
        ingredientTwoSprite.sprite = monster.Recipe.RequiredIngredients[1].sprite;
    }
}