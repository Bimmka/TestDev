using UnityEngine;
using UnityEngine.UI;
using CookingPrototype.Controllers;

using TMPro;
using System;

namespace CookingPrototype.UI {
	[RequireComponent(typeof(CanvasGroup))]
	public sealed class StartWindow : MonoBehaviour {
		public TMP_Text GoalText = null;
		public Button OkButton = null;

		bool _isInit = false;

		private CanvasGroup windowCanvasGroup;

		private GameplayController gc;

		public static Action GameStarted; 

		private void Awake() {
			windowCanvasGroup = GetComponent<CanvasGroup>();
			Hide();
			CustomersController.CalculatedTotalOrder += EnableShow;

		}

		private void OnDisable() {
			CustomersController.CalculatedTotalOrder -= EnableShow;
		}

		public void Init() {
			 gc = GameplayController.Instance;
			_isInit = true;
			OkButton.onClick.AddListener(gc.StartGame);

		}

		private void EnableShow() {
			if ( !_isInit ) {
				Init();
			}

			GoalText.text = $"{gc.OrdersTarget}";
			ChangeCanvasGroupState(1f, true, true);

		}

		public void Hide() {
			ChangeCanvasGroupState(0f, false, false);
		}

		public void StartGame() {
			GameStarted?.Invoke();
			Hide();
		}

		private void ChangeCanvasGroupState(float alpha, bool raycast, bool interactable) {
			windowCanvasGroup.alpha = alpha;
			windowCanvasGroup.blocksRaycasts = raycast;
			windowCanvasGroup.interactable = interactable;
		}
	}
}
