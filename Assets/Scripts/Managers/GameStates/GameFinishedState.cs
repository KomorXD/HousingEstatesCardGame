using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//! Class responsible for controlling end game state
public class GameFinishedState : IGameState
{
    private float startTS;
    private float delay = 2.0f;

    /**
     * Initializes internal data and HUD
     */
    public GameFinishedState()
    {
        CameraMoveScript cms = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CameraMoveScript>();
        cms.enabled = false;
        cms.holderTransform.DOMoveY(cms.holderTransform.position.y + 1000.0f, delay).SetEase(Ease.InSine);

        GameHUDManager ghm = GameHUDManager.Instance;
        ghm.SetInteractive(false);
        ghm.transform.DOMoveX(-1000.0f, delay).SetEase(Ease.InSine);

        startTS = Time.time;
    }

    //! Called each frame, updating state
    public void Update()
    {
        if(Time.time - startTS >= delay)
        {
            ChangeScene();
        }
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

        GameData.Instance.PlayerWon = requirementsMet;
        SceneManager.LoadSceneAsync("FinishedScene", LoadSceneMode.Single);
    }
}
