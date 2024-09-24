using BepInEx;
using System;
using UnityEngine;
using Utilla;

namespace sevelteFPV {
  [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
  [BepInPlugin("org.sevelte.gorillatag.fpv", "First Person Camera", "1.0.0")]
  
  public class Plugin : BaseUnityPlugin
  {
    public GameObject ShoulderCamera;
    public Camera ActualCamera;
    public GameObject LocalPlayerObject;
    public GameObject LocalPlayerCameraObject;

    public void Update()
    {
      OnFrameRefresh();
    }

    public void OnEnable() { HarmonyPatches.ApplyHarmonyPatches(); }
    public void OnDisable() { HarmonyPatches.RemoveHarmonyPatches(); }

    public void OnGameInitialized(object sender, EventArgs e) { SetupCamera(); }

    public void OnFrameRefresh()
    {
      Vector3 offset = new Vector3(0f, 0f, 0f);
      Vector3 targetPosition = LocalPlayerCameraObject.transform.position + LocalPlayerCameraObject.transform.TransformDirection(offset);

      ShoulderCamera.transform.position = LocalPlayerCameraObject.transform.position;
      Quaternion targetRotation = LocalPlayerCameraObject.transform.rotation;
      ShoulderCamera.transform.rotation = Quaternion.LerpUnclamped(ShoulderCamera.transform.rotation, targetRotation, RotationTime);
    }

    public void SetupCamera() {
      ShoulderCamera = GorillaTagger.Instance.thirdPersonCamera.transform.Find("Shoulder Camera").gameObject;
      LocalPlayerObject = GorillaLocomotion.Player.Instance.gameObject;
      LocalPlayerCameraObject = GorillaTagger.Instance.mainCamera.gameObject;
    }
  }
}

/*
Vector3 offset = new Vector3(0f, 0f, 0f);
Vector3 targetPosition = LocalPlayerCameraObject.transform.position + LocalPlayerCameraObject.transform.TransformDirection(offset);
*/
