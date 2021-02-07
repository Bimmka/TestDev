using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CookingPrototype.UI;
using CookingPrototype.Controllers;
namespace CookingPrototype.Kitchen {
	public sealed class AutoFoodFiller : MonoBehaviour {
		public string                  FoodName = null;
		public List<AbstractFoodPlace> Places   = new List<AbstractFoodPlace>();

		private bool isPlaying = false;
		private void Start() {
			StartWindow.GameStarted += StartCooking;
			GameplayController.GameRestarted += StopCooking;
		}
		private void OnDisable() {
			StartWindow.GameStarted -= StartCooking;
			GameplayController.GameRestarted -= StopCooking;
		}

		private void StartCooking() {
			isPlaying = true;
			StartCoroutine(Cooking());
		}

		private void StopCooking() {
			isPlaying = false;
			StopAllCoroutines();
		}

		private IEnumerator Cooking() {
			while ( isPlaying ) {
				foreach ( var place in Places ) {
					place.TryPlaceFood(new Food(FoodName));
				}
				yield return null;
			}
				
			
		}
	}
}
