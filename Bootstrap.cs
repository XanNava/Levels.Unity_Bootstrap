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
		Debug.Log("[STATE] - " + state);

		if (state == PlayModeStateChange.ExitingEditMode) {
			ValidateBootstrapScene();
		}
	}

	private static void ValidateBootstrapScene() {
		Scene scene = SceneManager.GetSceneByName("Bootstrap");

		Debug.Log("[VALID] : Bootstrap.Unity - " + !scene.IsValid());
		if (!scene.IsValid() || (scene.IsValid() && !scene.isLoaded)) {
			EditorBuildSettingsScene? bootstrapSettingsScene = EditorBuildSettings.scenes.FirstOrDefault((s) => { return s.path.Contains("Bootstrap"); });

			if (bootstrapSettingsScene == null) {
				Debug.LogError("[ERROR] - No Bootstrap scene in build settings");
			}

			scene = EditorSceneManager.OpenScene(bootstrapSettingsScene.path, OpenSceneMode.Additive);
		}

		EditorSceneManager.MoveSceneBefore(scene, SceneManager.GetSceneAt(0));
		EditorSceneManager.SetActiveScene(scene);
	}
}
#endif
