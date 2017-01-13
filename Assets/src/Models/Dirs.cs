﻿using System;
using Cubiquity;
using UnityEngine;

namespace AssemblyCSharp
{
	public enum Dir {
		None = 0,
		Up = 1,
		Down = 2,
		Left = 4,
		Right = 8,
		Forward = 16,
		Back = 32
	}

	public class DirUtils 
	{
		private static Vector3i up = new Vector3i(0, 1, 0);
		private static Vector3i down = new Vector3i(0, -1, 0);
		private static Vector3i left = new Vector3i(-1, 0, 0);
		private static Vector3i right = new Vector3i(1, 0, 0);
		private static Vector3i forward = new Vector3i(0, 0, 1);
		private static Vector3i back = new Vector3i(0, 0, -1);
		private static Vector3i zero = new Vector3i (0, 0, 0);

		public static Vector3i GetUnitVector(Dir dir) {
			switch (dir) {
			case Dir.Up:
				return up;
			case Dir.Down:
				return down;
			case Dir.Left:
				return left;
			case Dir.Right:
				return right;
			case Dir.Forward:
				return forward;
			case Dir.Back:
				return back;
			}
			return zero;
		}

		public static Dir GetDir(Vector3i unitVector) {
			if (unitVector == up) {
				return Dir.Up;
			} else if (unitVector == down) {
				return Dir.Down;
			} else if (unitVector == left) {
				return Dir.Left;
			} else if (unitVector == right) {
				return Dir.Right;
			} else if (unitVector == forward) {
				return Dir.Forward;
			} else if (unitVector == back) {
				return Dir.Back;
			} 
			return Dir.None;
		}

		public static Quaternion GetRotation(Dir dir) {
			switch (dir) {
			case Dir.Up:
				return new Quaternion ();
			case Dir.Down:
				return Quaternion.Euler (180, 0, 0);
			case Dir.Left:
				return Quaternion.Euler (0, 0, 90);
			case Dir.Right:
				return Quaternion.Euler (0, 0, -90);
			case Dir.Forward:
				return Quaternion.Euler(90, 0, 0);
			case Dir.Back:
				return Quaternion.Euler(-90, 0, 0);
			}
			return new Quaternion ();
		}

		public static readonly Dir[] Dirs = new [] { Dir.Up, Dir.Down, Dir.Left, Dir.Right, Dir.Forward, Dir.Back };
	}
}
