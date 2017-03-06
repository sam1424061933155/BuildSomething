/*
* UniOSC
* Copyright © 2014-2015 Stefan Schlupek
* All rights reserved
* info@monoflow.org
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using OSCsharp.Data;


namespace UniOSC{

	/// <summary>
	/// Dispatcher button that forces a OSCConnection to send a OSC Message.
	/// Two separate states: Down and Up 
	/// </summary>
	[AddComponentMenu("UniOSC/EventDispatcherButton")]
	[ExecuteInEditMode]
	public class UniOSCEventDispatcherIntButton: UniOSCEventDispatcher {

		public int ledValue;

		public int xPos;
		public int yPos;

//		#region public
//		[HideInInspector]
		public int downOSCDataValue=50;
//		[HideInInspector]
		public int upOSCDataValue=0;
//		[HideInInspector]
		public bool showGUI;

		public bool ledOnOff;
//		[HideInInspector]
//		public int xPos;
//		[HideInInspector]
//		public int yPos;
//		#endregion

		#region private
		private bool _btnDown;
		private GUIStyle _gs; 
		#endregion

		public override void Awake()
		{
			base.Awake ();
		}

		public override void OnEnable ()
		{
			base.OnEnable ();
			ClearData();
			AppendData(0f);

		}
		public override void OnDisable ()
		{
			base.OnDisable ();
		}
		void OnGUI(){
			if(!showGUI)return;
			RenderGUI();
		}

		void RenderGUI(){

			_gs = new GUIStyle(GUI.skin.button);
			_gs.fontSize=11;
			//gs.padding = new RectOffset(2,2,2,2);

			GUIScaler.Begin();

			Event e = Event.current;
			GUI.BeginGroup(new Rect((Screen.width/GUIScaler.GuiScale.x)*xPos,(Screen.height/GUIScaler.GuiScale.y)*yPos,(Screen.width/GUIScaler.GuiScale.x),(Screen.height/GUIScaler.GuiScale.y)  ));

			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();

//			Debug.Log((Screen.width/GUIScaler.GuiScale.x)*xPos);


			StringBuilder sb = new StringBuilder();
			sb.AppendLine("");
			sb.AppendLine("Send OSC:");
			sb.AppendLine(String.Format("IP:{0}",oscOutIPAddress));
			sb.AppendLine(String.Format("Address:{0}",oscOutAddress));
			sb.AppendLine(String.Format("Port:{0}",oscOutPort));
			GUIContent buttonText = new GUIContent(sb.ToString());
			Rect buttonRect = GUILayoutUtility.GetRect(buttonText,_gs  ); 
			buttonRect.width *=1.0f;
			buttonRect.height*=1.0f;

			if (e.isMouse && buttonRect.Contains(e.mousePosition)) { 
				if(e.type == EventType.MouseDown){
					
					SendOSCMessageDown();
					Debug.Log("LED: " + ledOnOff);
				}
//				if(e.type == EventType.MouseUp){
//					SendOSCMessageUp();
//				}
			} 

			GUI.Button(buttonRect, buttonText,_gs);

			GUILayout.EndVertical();
			GUI.EndGroup();

			GUIScaler.End();
		}

		/// <summary>
		/// Sends the OSC message with the downOSCDataValue.
		/// </summary>
		public void SendOSCMessageDown(){

			ledOnOff = !ledOnOff;

			if(_OSCeArg.Packet is OscMessage)
			{
				Debug.Log("OscMessage");
				if(ledOnOff){
//					((OscMessage)_OSCeArg.Packet).UpdateDataAt(0, downOSCDataValue);
//					((OscMessage)_OSCeArg.Packet).UpdateDataAt(1, downOSCDataValue);
//					((OscMessage)_OSCeArg.Packet).UpdateDataAt(2, downOSCDataValue);
				}
				else{
//					((OscMessage)_OSCeArg.Packet).UpdateDataAt(0, 0);
//					((OscMessage)_OSCeArg.Packet).UpdateDataAt(1, downOSCDataValue);
//					((OscMessage)_OSCeArg.Packet).UpdateDataAt(2, downOSCDataValue);
				}
			}
			else if(_OSCeArg.Packet is OscBundle)
			{
				Debug.Log("OscBundle");

				foreach (OscMessage m in ((OscBundle)_OSCeArg.Packet).Messages)
				{
					m.UpdateDataAt(0, downOSCDataValue);
				}				
			}


			_SendOSCMessage(_OSCeArg);
		}

		/// <summary>
		/// Sends the OSC message with the upOSCDataValue.
		/// </summary>
		public void SendOSCMessageUp(){
			if(_OSCeArg.Packet is OscMessage)
			{
				((OscMessage)_OSCeArg.Packet).UpdateDataAt(0, upOSCDataValue);
			}
			else if(_OSCeArg.Packet is OscBundle)
			{
				foreach (OscMessage m in ((OscBundle)_OSCeArg.Packet).Messages)
				{
					m.UpdateDataAt(0, upOSCDataValue);
				}              
			}

			_SendOSCMessage(_OSCeArg);
		}


	}
}