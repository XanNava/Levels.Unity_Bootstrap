using System.Linq;
using System.Text;

using Levels.Unity;
using Levels.Universal.DI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour {
	public static readonly StringBuilder prelogs = new StringBuilder();
	public static Unity.Logging.Logger logger;

	public UnityEvent OnSubsystemRegistration_Hook;
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	public static void OnSubsystemRegistration() {
		prelogs.AppendLine("[START] - Bootstrap.OnSubsystemRegistration");

		Registrations();
	}

	private static void Registrations() {

	}
}


#if UNITY_EDITOR
[InitializeOnLoad]
public partial class BootstrapPlayValidator {
	static BootstrapPlayValidator() {
		EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
	}

	private static void OnPlayModeStateChanged(PlayModeStateChange state) {
		Debug.Log(PlayModeStateChange.ExitingEditMode);

		if (state == PlayModeStateChange.ExitingEditMode) {
			CheckBootstrapScene();
		}
	}

	private static void CheckBootstrapScene() {
		Scene scene = SceneManager.GetSceneByName("Bootstrap");

		Debug.Log("[VALID] : Bootstrap.Unity - " + !scene.IsValid());
		if (!scene.IsValid() || (scene.IsValid() && !scene.isLoaded)) {
			Debug.Log(EditorBuildSettings.scenes.Where((s) => { return s.path.Contains("Bootstrap"); }));
			var holder = EditorBuildSettings.scenes.Where((s) => { return s.path.Contains("Bootstrap"); });
			foreach (var h in holder) {
				Debug.Log("[LOAD] : " + h.path);
				scene = EditorSceneManager.OpenScene(h.path, OpenSceneMode.Additive);
			}
		}

		EditorSceneManager.MoveSceneBefore(scene, SceneManager.GetSceneAt(0));
		EditorSceneManager.SetActiveScene(scene);
	}
}
#endif