using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This component should be placed on the same GameObject as a SkillInstance.
/// It handles the process of targeting and executing the associated skill.
/// </summary>
[RequireComponent(typeof(SkillInstance))]
public class SkillExecutor : MonoBehaviour {

    // You must assign the character who is casting this skill in the Inspector.
    [Tooltip("The character casting the skill.")]
    [SerializeField] private Character caster;

    private ActionSkill _skillToExecute;
    private bool _isTargeting = false;

    private void Awake() {
        // Subscribe to the OnSkillSet event to get the ActionSkill when it's initialized.
        var skillInstance = GetComponent<SkillInstance>();
        if (skillInstance != null) {
            skillInstance.OnSkillSet += (skill) => {
                _skillToExecute = skill;
            };
        }
    }

    public void Init(Character myCaster) {
        caster = myCaster;
    }

    private void Update() {
        // If we are in targeting mode, wait for the user to click on a target.
        if (_isTargeting) {
            // Left-click to select a target and execute the skill.
            if (Input.GetMouseButtonDown(0)) {
                if (TurnManager.Instance.CurrActiveCharacter && TurnManager.Instance.CurrActiveCharacter != caster) {
                    Debug.LogError("Not your turn");
                    return;
                }
                TryExecuteOnTarget();
            }

            // Right-click to cancel targeting mode.
            if (Input.GetMouseButtonDown(1)) {
                Debug.Log("Targeting cancelled.");
                _isTargeting = false;
            }
        }
    }

    /// <summary>
    /// This public method begins the targeting process. It should be called from a UI element, like a button.
    /// </summary>
    public void BeginTargeting() {
        if (_skillToExecute == null) {
            Debug.LogError("No skill is available to be executed!");
            return;
        }
        if (caster == null) {
            Debug.LogError("Caster has not been assigned in the Inspector!");
            return;
        }

        Debug.Log($"Targeting started for skill: '{_skillToExecute.SkillNode.name}'. Left-click a target, or Right-click to cancel.");
        _isTargeting = true;
    }

    /// <summary>
    /// Performs a raycast from the mouse position to find and attack a target.
    /// </summary>
    private void TryExecuteOnTarget() {
        // Use a Raycast to detect a 2D object with a collider under the mouse cursor.
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null) {
            // Check if the hit object has a Character component.
            Character target = hit.collider.GetComponent<Character>();

            if (target != null) {
                Debug.Log($"Target found: {target.name}. Executing skill.");

                // Create the context for the skill with the caster and the selected target.
                SkillExecutionContext context = new SkillExecutionContext(caster, target);

                // Execute the skill.
                _skillToExecute.Execute(context);

                // Exit targeting mode after successful execution.
                _isTargeting = false;
                TurnManager.Instance.EndTurn(caster);
            } else {
                Debug.Log("Clicked on an object that is not a valid Character. Continue targeting.");
            }
        } else {
             Debug.Log("Clicked on empty space. Continue targeting.");
        }
    }
}