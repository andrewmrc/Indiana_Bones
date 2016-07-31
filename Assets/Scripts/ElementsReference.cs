using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IndianaBones
{

	public class ElementsReference : MonoBehaviour {

		// Singleton Implementation
		protected static ElementsReference _self;
		public static ElementsReference Self
		{
			get
			{
				if (_self == null)
					_self = FindObjectOfType(typeof(ElementsReference)) as ElementsReference;
				return _self;
			}
		}

		public GameObject canvasUI;
		public GameObject canvasGameOver;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}

}
