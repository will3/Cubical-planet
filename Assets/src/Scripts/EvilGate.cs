﻿using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class EvilGate : MonoBehaviour, IBlock
	{
		private readonly BlockCoord _blockCoord = new BlockCoord();

		private Billboard billboard;

		private Cooldown spawnCooldown = new Cooldown(2.0f);

		#region IBlock implementation

		public BlockCoord blockCoord {
			get {
				return _blockCoord;
			}
		}

		#endregion

		void Start() {
			if (blockCoord.surface == null) {
				Destroy (gameObject);
			}
			billboard = GetComponentInChildren<Billboard> ();
			Debug.Assert (billboard != null);
		}

		void Update () {
			spawnCooldown.Update ();
			billboard.up = Game.Instance.Planet.gameObject.transform.TransformDirection (blockCoord.surface.normal);

			var planet = Game.Instance.Planet;
			var connections = blockCoord.surface.connections;
			if (connections.Count > 0) {
				var index = (int)Math.Floor (UnityEngine.Random.Range (0.0f, connections.Count));
				var randomConnection = connections [index];
				var otherSurface = randomConnection.OtherSurface (blockCoord.surface);
				if (spawnCooldown.Ready ()) {
					planet.Create (Prefabs.Spider, otherSurface);
				}
			}
		}
	}
}
