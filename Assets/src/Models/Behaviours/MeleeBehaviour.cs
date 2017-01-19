﻿using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Dijkstras;

namespace AssemblyCSharp
{
	public class MeleeBehaviour : MonoBehaviour, ICharacterBehaviour, CritterAnimationEvents.Listener
	{
		private Character character;

		private Movement movement;

		private Targeting targeting;

		private Animator animator;

		private CritterAnimationEvents animationEvents;

		private bool exitAttackTrigger = false;

		public void DidExitAttack ()
		{
			exitAttackTrigger = true;
		}

		public void DidEnterAny()
		{
			exitAttackTrigger = false;
		}
			
		public void Start() {
			character = GetComponent<Character> ();
			Debug.Assert (character != null);
			animator = GetComponentInChildren<Animator> ();
			Debug.Assert (animator != null);

			movement = new Movement (character);
			targeting = new Targeting (character);

			animationEvents = animator.GetBehaviour<CritterAnimationEvents> ();
			animationEvents.listener = this;
		}

		public void Update() {
			movement.StepPath ();
			animator.SetWalking (movement.Walking);
		}

		public void Idle ()
		{
		}

		public void Patrol ()
		{
			movement.Patrol ();
		}

		public bool Chase (Character targetCharacter)
		{
			var planet = Game.Instance.Planet;

			var target = targetCharacter.CurrentSurface;

			movement.Move (target, true);

			// If next to target
			if (movement.Done) {
				var connection = planet.Terrian.ConnectionBetween (character.CurrentSurface, target);
			
				if (connection != null) {
					return true;
				}
			}

			return false;
		}

		public bool Attack (Character targetCharacter)
		{
			if (exitAttackTrigger) {
				exitAttackTrigger = false;
				character.Damage (targetCharacter);
				return true;
			}

			animator.TriggerAttack ();

			return false;
		}

		public Character FindTarget ()
		{
			return targeting.GetTargets().FirstOrDefault ();
		}
	}
}

