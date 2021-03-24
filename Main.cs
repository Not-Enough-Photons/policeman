using UnityEngine;
using MelonLoader;

namespace Policeman
{
    public static class BuildInfo
    {
        public const string Name = "police man"; // Name of the Mod.  (MUST BE SET)
        public const string Description = "he follow you"; // Description for the Mod.  (Set as null if none)
        public const string Author = "adamdev, Not Enough Photons"; // Author of the Mod.  (MUST BE SET)
        public const string Company = "Not Enough Photons"; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class Policeman : MelonMod
    {
        private AssetBundle policeBundle;
        private AssetBundle policeAudioBundle;
        private AssetBundle policeAnimBundle;

        private GameObject police;

        private CopAI ai;
        private CopFootSensor[] footSensors = new CopFootSensor[2];
        private CopFootstepBehaviour footsteps;
        private CopSpeechBehaviour spch;
        private CopPunchBehaviour punch;
        private CopHandSensor[] handSensors = new CopHandSensor[2];

        private Transform eye;
        private Transform speech;
        private Transform mdl;
        private Transform lFoot;
        private Transform rFoot;
        private Transform lHand;
        private Transform rHand;

        private static string path = "UserData/police man";

        public override void OnApplicationStart()
        {
            RegisterTypesInIL2CPP();

            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);

            policeBundle = AssetBundle.LoadFromFile($"{path}/police.pack");
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            FixObject();
        }

        public override void OnUpdate()
        {
            if(police != null || speech != null)
            {
                speech.position = police.transform.Find("AI/Collider/Body/cop@T-Pose/Root.001/Root1/Pelvis/Spine/Spine1/Neck").position;
                lFoot.position = police.transform.Find("AI/Collider/Body/cop@T-Pose/Root.001/Root1/Pelvis/Spine/L Thigh/L Calf/L Foot/LFootstep").position;
                rFoot.position = police.transform.Find("AI/Collider/Body/cop@T-Pose/Root.001/Root1/Pelvis/Spine/R Thigh/R Calf/R Foot/RFootstep").position;
            }
        }

        private void FixObject()
        {
            police = GameObject.Instantiate(policeBundle.LoadAsset("Cop"), Vector3.up * 1f, Quaternion.identity).Cast<GameObject>();

            BoneworksModdingToolkit.Shaders.ReplaceWithValveVRStandard(police);

            eye = police.transform.Find("AI/Eye");
            speech = police.transform.Find("AI/Speech");
            mdl = police.transform.Find("AI/Collider/Body/cop@T-Pose");
            lFoot = police.transform.Find("AI/Collider/Body/cop@T-Pose/Root.001/Root1/Pelvis/Spine/L Thigh/L Calf/L Foot/LFootstep");
            rFoot = police.transform.Find("AI/Collider/Body/cop@T-Pose/Root.001/Root1/Pelvis/Spine/R Thigh/R Calf/R Foot/RFootstep");
            lHand = police.transform.Find("AI/Collider/Body/cop@T-Pose/Root.001/Root1/Pelvis/Spine/Spine1/Neck/Bip01 L Clavicle/L UpperArm/L Forearm/L Hand/L Finger");
            rHand = police.transform.Find("AI/Collider/Body/cop@T-Pose/Root.001/Root1/Pelvis/Spine/Spine1/Neck/Bip01 R Clavicle/R UpperArm/R Forearm/R Hand/R Finger");

            ai = police.AddComponent<CopAI>();
            footsteps = police.AddComponent<CopFootstepBehaviour>();
            spch = police.AddComponent<CopSpeechBehaviour>();
            punch = police.AddComponent<CopPunchBehaviour>();

            footSensors[0] = lFoot.gameObject.AddComponent<CopFootSensor>();
            footSensors[1] = rFoot.gameObject.AddComponent<CopFootSensor>();
            handSensors[0] = lHand.gameObject.AddComponent<CopHandSensor>();
            handSensors[1] = rHand.gameObject.AddComponent<CopHandSensor>();

            ai.eyePos = eye;
            ai.animator = mdl.GetComponent<Animator>();
            ai.animator.runtimeAnimatorController = policeBundle.LoadAsset("CopAnim").Cast<RuntimeAnimatorController>();

            footsteps.feet = new Transform[]
            {
                lFoot,
                rFoot
            };

            footsteps.sources = new AudioSource[]
            {
                lFoot.GetComponent<AudioSource>(),
                rFoot.GetComponent<AudioSource>()
            };

            punch.sensors = handSensors;

            footsteps.sounds = new AudioClip[]
            {
                policeBundle.LoadAsset("foot1").Cast<GameObject>().GetComponent<AudioSource>().clip,
                policeBundle.LoadAsset("foot2").Cast<GameObject>().GetComponent<AudioSource>().clip,
                policeBundle.LoadAsset("foot3").Cast<GameObject>().GetComponent<AudioSource>().clip,
                policeBundle.LoadAsset("foot4").Cast<GameObject>().GetComponent<AudioSource>().clip,
                policeBundle.LoadAsset("foot5").Cast<GameObject>().GetComponent<AudioSource>().clip
            };

            spch.speechClips = new AudioClip[]
            {
                policeBundle.LoadAsset("response1").Cast<GameObject>().GetComponent<AudioSource>().clip,
                policeBundle.LoadAsset("response2").Cast<GameObject>().GetComponent<AudioSource>().clip,
                policeBundle.LoadAsset("response3").Cast<GameObject>().GetComponent<AudioSource>().clip,
                policeBundle.LoadAsset("response4").Cast<GameObject>().GetComponent<AudioSource>().clip,
                policeBundle.LoadAsset("response5").Cast<GameObject>().GetComponent<AudioSource>().clip,
                policeBundle.LoadAsset("response6").Cast<GameObject>().GetComponent<AudioSource>().clip,
            };

            spch.speechSource = speech.GetComponent<AudioSource>();
        }

        private void RegisterTypesInIL2CPP()
        {
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<CopAI>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<CopFootSensor>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<CopFootstepBehaviour>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<CopSpeechBehaviour>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<CopPunchBehaviour>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<CopHandSensor>();
        }
    }
}