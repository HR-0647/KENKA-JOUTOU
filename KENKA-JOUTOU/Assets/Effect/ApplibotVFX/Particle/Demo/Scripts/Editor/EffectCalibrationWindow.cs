//------------------------------------------------
// <copyright file="EffectCalibrationWindow.cs">
// Copyright (c) Applibot, Inc. All right reserved
// </copyright>
// <summary>EffectCalibrationWindow</summary>
//------------------------------------------------
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

namespace Applibot.VFX
{
	/// <summary>
	/// EffectCalibrationWindow
	/// </summary>
	public class EffectCalibrationWindow 
	{

		#region define

		private const string DIRECTORY_PATH			= "ApplibotVFX/Particle/Demo";
		private const float WIDTH					= 300;
		private const float HEIGHT					= 100;
		private const string KEYWORD_INVERSE_RGB	= "CALIBRATION_FLOOR_INVERSE_RGB";

		private static Rect _windowSize			= new Rect(8, 24, WIDTH, HEIGHT);
		private static bool　_enabled			= false;

		#endregion

		#region method

		[InitializeOnLoadMethod]
		private static void SampleSceneEdit(){
			SceneView.onSceneGUIDelegate += OnSceneGUI;
			EditorSceneManager.sceneOpened		+= (scene, mode) => _enabled = GetIsTargetScene(scene);
			EditorApplication.delayCall			+= () => _enabled = GetIsTargetScene(EditorSceneManager.GetActiveScene());
		}

		private static void OnSceneGUI(SceneView sceneView){
			if (!_enabled) {
				return;
			}
			GUILayout.Window(1, _windowSize, DrawWindow, ObjectNames.NicifyVariableName(typeof(EffectCalibrationWindow).Name));
		}

		private static void DrawWindow(int id)
		{
			// Dark Mode有効化トグル
			using (var s = new EditorGUI.ChangeCheckScope()) {
				var inverse		= EditorGUILayout.ToggleLeft("Dark Mode", Shader.IsKeywordEnabled(KEYWORD_INVERSE_RGB));
				if (s.changed) {
					if (inverse) {
						Shader.EnableKeyword(KEYWORD_INVERSE_RGB);
					}
					else {
						Shader.DisableKeyword(KEYWORD_INVERSE_RGB);
					}
				}
			}
		}
	
		private static bool GetIsTargetScene(Scene scene)
		{
			return scene.path.Contains(DIRECTORY_PATH);
		}

		#endregion

	}
}