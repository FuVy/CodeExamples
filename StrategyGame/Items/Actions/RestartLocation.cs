using UnityEngine.SceneManagement;
using UnityEngine;

[CreateAssetMenu(menuName = ("Items/Actions/RestartLocation"))]
public class RestartLocation : ItemAction
{
    public override void Apply(ItemTargetType.Targets targets)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
