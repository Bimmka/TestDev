using System;

using UnityEngine;

using JetBrains.Annotations;

namespace CookingPrototype.Kitchen {
	[RequireComponent(typeof(FoodPlace))]
	public sealed class FoodTrasher : MonoBehaviour {

		FoodPlace _place	     = null;
		float     _timer         = 0f;
		float     _lastTimeTap   = 0f;
		float	  _tapTimeOffset = 0.5f;

		void Start() {
			_place = GetComponent<FoodPlace>();
			_timer = Time.realtimeSinceStartup;
			_lastTimeTap = Time.time;
		}

		/// <summary>
		/// Освобождает место по двойному тапу если еда на этом месте сгоревшая.
		/// </summary>
		[UsedImplicitly]
		public void TryTrashFood() {

			if ( (Time.time - _lastTimeTap) < _tapTimeOffset ) {
				if ( _place.CurFood != null )
					if ( _place.CurFood.CurStatus == Food.FoodStatus.Overcooked ) _place.FreePlace();
			}
			_lastTimeTap = Time.time;

		}
	}
}
