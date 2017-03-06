/*
* UniOSC
* Copyright © 2014-2015 Stefan Schlupek
* All rights reserved
* info@monoflow.org
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using OSCsharp.Data;


namespace UniOSC{

	/// <summary>
	/// Uni OSC scale game object.
	/// </summary>
	[AddComponentMenu("UniOSC/ScaleGameObject")]
	public class UniOSCScaleGameObject :  UniOSCEventTarget {

		[HideInInspector]
		public Transform transformToScale;
		public float scaleFactor = 1;
		public AudioSource ads;

		private Vector3 _scale;
		private float _data;


		void Awake(){

		}

		private void _Init(){
			if(transformToScale == null){
				Transform hostTransform = GetComponent<Transform>();
				if(hostTransform != null) transformToScale = hostTransform;
			}
			ads = GetComponent<AudioSource> ();
		}
	
		public override void OnEnable(){
			_Init();
			base.OnEnable();
		}

		public override void OnOSCMessageReceived(UniOSCEventArgs args)
		{

			OscMessage msg = (OscMessage)args.Packet;
			if (msg.Data.Count < 1)
				return;
			string success = (string)msg.Data [2];

			if (success.Equals ("1")) {
				//guess right
				ads.Play();
				success = "0";
			}

		}

	}
}