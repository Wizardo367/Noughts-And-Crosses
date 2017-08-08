using UnityEngine;
using admob;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
	// Variables
	private const string BannerId = "ca-app-pub-6762597481863539/6026430848";

	// Singleton
	private static AdManager _instance;
	public static AdManager Instance
	{
		get { return _instance ?? (_instance = new AdManager()); }
	}

	private void Awake()
	{
		// Prevent object being destroyed
		DontDestroyOnLoad(gameObject);

		// Instantiate admob
		Admob.Instance().initAdmob(BannerId, "");
		// Set properties
		Admob.Instance().setTesting(true);
		Admob.Instance().setForChildren(true);
		// Show ad
		Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.BOTTOM_CENTER, 0);
	}

	public void Start()
	{
		// Load menu scene
		//SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
	}
}