//! Interface for HUD Managers
public interface IHUDManager
{
    //! Should initialize a manager
    public void Init();

    //! Should update UI
    public void UpdateUI();

    //! Should set UI activity
    public void SetActive(bool active);

    //! Should set UI interactivity
    public void SetInteractive(bool interactive);
}
