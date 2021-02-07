using UnityEngine;
using UnityEngine.UI;

using CookingPrototype.Controllers;

using TMPro;

namespace CookingPrototype.UI {
	[RequireComponent(typeof(CanvasGroup))]
	public sealed class TopUI : MonoBehaviour {
		public Image    OrdersBar          = null;
		public TMP_Text OrdersCountText    = null;
		public TMP_Text CustomersCountText = null;

		private CanvasGroup windowCanvasGroup;

		private void Awake() {
			windowCanvasGroup = GetComponent<CanvasGroup>();
			ChangeCanvasGroupState(0f, false, false);
		}

		void Start() {
			GameplayController .Instance.TotalOrdersServedChanged       += OnOrdersChanged;
			CustomersController.Instance.TotalCustomersGeneratedChanged += OnCustomersChanged;
			StartWindow.GameStarted += ActivateBar;
			LoseWindow.PlayerLose += DisableBar;
			OnOrdersChanged();
			OnCustomersChanged();
		}

		void OnDestroy() {
			if ( GameplayController.Instance ) {
				GameplayController.Instance.TotalOrdersServedChanged -= OnOrdersChanged;
			}

			if ( CustomersController.Instance ) {
				CustomersController.Instance.TotalCustomersGeneratedChanged -= OnCustomersChanged;
			}

			StartWindow.GameStarted -= ActivateBar;
			LoseWindow.PlayerLose -= DisableBar;
		}

		void OnCustomersChanged() {
			var cc = CustomersController.Instance;
			CustomersCountText.text = (cc.CustomersTargetNumber - cc.TotalCustomersGenerated).ToString();
		}

		void OnOrdersChanged() {
			var gc = GameplayController.Instance;
			OrdersCountText.text = $"{gc.TotalOrdersServed}/{gc.OrdersTarget}";
			OrdersBar.fillAmount = (float) gc.TotalOrdersServed / gc.OrdersTarget;
		}

		private void ChangeCanvasGroupState(float alpha, bool raycast, bool interactable) {
			windowCanvasGroup.alpha = alpha;
			windowCanvasGroup.blocksRaycasts = raycast;
			windowCanvasGroup.interactable = interactable;
		}

		private void ActivateBar() {
			ChangeCanvasGroupState(1f, false, false);
		}
		private void DisableBar() {
			ChangeCanvasGroupState(0f, false, false);
		}
	}
}
