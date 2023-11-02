using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishedState : IGameState
{
    public GameFinishedState()
    {
        CameraMoveScript cms = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CameraMoveScript>();
        cms.enabled = false;

        cms.holderTransform.DOMoveY(cms.holderTransform.position.y + 1000.0f, 2.0f).SetEase(Ease.InSine);
        Task.Delay(2000).ContinueWith(_ => ChangeScene());
    }

    public void Update()
    {
        ;
    }

    private void ChangeScene()
    {
        bool requirementsMet = true;

        List<DifficultyRequirement> requirements = GameManager.Instance.GameDifficulty.Requirements;
        Dictionary<ParameterCategory, float> parameters = GameManager.Instance.GameParameters;

        foreach(DifficultyRequirement req in requirements)
        {
            float playerPoints = parameters[req.Parameter.Category];

            if (playerPoints < req.Min || playerPoints > req.Max)
            {
                requirementsMet = false;
                break;
            }
        }

        PlayerPrefs.SetInt("player_won", requirementsMet ? 1 : 0);
        SceneManager.LoadSceneAsync("FinishedScene", LoadSceneMode.Single);
    }
}
