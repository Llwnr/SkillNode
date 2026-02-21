using System.Threading.Tasks;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public async Task OnSkillSelected()
    {
        await PathAndAreaSelectionMode();
        await ExecuteSkill();
    }

    private async Task ExecuteSkill() { }

    //Right click to select path to start from
    //Left click to select area of effect
    public async Task PathAndAreaSelectionMode(){}
}