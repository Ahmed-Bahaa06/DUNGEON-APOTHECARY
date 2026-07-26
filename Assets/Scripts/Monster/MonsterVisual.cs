using UnityEngine;

public class MonsterVisual : MonoBehaviour
{
    [SerializeField] private Monster monster;
    [SerializeField] private GameObject InfectedVisual;
    [SerializeField] private GameObject healedVisual;

    private float lastInputX;
    private float lastInputY;

    private Animator unhealedAnimator;
    private Animator healedAnimator;

    private Animator currentAnimator;

    private void Awake()
    {
        unhealedAnimator = InfectedVisual.GetComponent<Animator>();
        healedAnimator = healedVisual.GetComponent<Animator>();

        InfectedVisual.SetActive(true);
        healedVisual.SetActive(false);

        currentAnimator = unhealedAnimator;
    }

    private void OnEnable()
    {
        monster.OnHealed += Monster_OnHealed;
    }

    private void Update()
    {
        UpdateVisual(monster.Movement.MoveDirection);
    }

    private void OnDisable()
    {
        monster.OnHealed -= Monster_OnHealed;
    }

    private void Monster_OnHealed()
    {
        currentAnimator.SetTrigger("IsHealing");
    }


    public void OnAnimationHealing_Finished()
    {
        InfectedVisual.SetActive(false);
        healedVisual.SetActive(true);

        currentAnimator = healedAnimator;

        currentAnimator.SetFloat("LastInputX", lastInputX);
        currentAnimator.SetFloat("LastInputY", lastInputY);
    }
    public void UpdateVisual(Vector2 moveDirection)
    {

        bool isMoving = moveDirection.sqrMagnitude > 0;
        currentAnimator.SetBool("IsWalking", isMoving);

        currentAnimator.SetFloat("InputX", moveDirection.x);
        currentAnimator.SetFloat("InputY", moveDirection.y);

        if (isMoving)
        {
            lastInputX = moveDirection.x;
            lastInputY = moveDirection.y;
        }

        currentAnimator.SetFloat("LastInputX", lastInputX);
        currentAnimator.SetFloat("LastInputY", lastInputY);
    }
}
