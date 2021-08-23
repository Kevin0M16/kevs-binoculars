using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.UI;
using CitizenFX.Core.Native;
using static CitizenFX.Core.Native.API;


namespace kevs_binoculars
{
    public class Main : BaseScript
    {        
        public static int PlayerIsReady;
        public static int CurrentWeapon;
        public static int EnterAnimIsStarted;
        public static int EnterAnimIsPlayed;
        public static int ExitAnimIsStarted;
        public static int ExitAnimIsPlayed;
        public static Prop BinocularsObject = (Prop)null;
        public static int ObjectCreated;
        public static Camera BinocularsCamera;
        public static int CamCreated;
        public static float CamFov;
        public static int ScaleformRequested;
        public static int Scaleform;
        public static int NightVisionActivated;
        public static int ThermalVisionActivated;
        public static int ZoomSound;
        public static int PlayerRot;

        public Main()
        {
            Tick += OnTick;

            Main.PlayerIsReady = 0;
            Main.EnterAnimIsStarted = 0;
            Main.EnterAnimIsPlayed = 0;
            Main.ExitAnimIsStarted = 0;
            Main.ExitAnimIsPlayed = 0;
            BinocularsObject = null;
            Main.ObjectCreated = 0;
            Main.CamCreated = 0;
            Main.CamFov = 0;
            Main.ScaleformRequested = 0;
            Main.Scaleform = 0;
            Main.NightVisionActivated = 0;
            Main.ThermalVisionActivated = 0;
            Main.ZoomSound = 0;
            Main.PlayerRot = 0;
        }

        public async Task OnTick()
        {
            OnKeyDown();

            Main.Gamepad();
            Main.EnterAnimStarting();
            Main.EnterAnimPlaying();
            Main.ExitAnimPlaying();
            await Object();
            Main.Cam();
            Main.RequestScaleformMovie();
            Main.ScaleformMovie();
            Main.Vision();
            Main.Sound();
            //Main.PlayerHeading();
            Main.Cancel();
            Main.PlayerControl();

            //DebugMode();

            await Task.FromResult(0);
        }

        private void DebugMode()
        {
            var debugPos = 0.001;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("PlayerIsReady: " + PlayerIsReady);
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("EnterAnimIsStarted: " + EnterAnimIsStarted);
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("EnterAnimIsPlayed: " + EnterAnimIsPlayed);
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("ExitAnimIsStarted: " + ExitAnimIsStarted);
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("ExitAnimIsPlayed: " + ExitAnimIsPlayed);
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("BinocularsObject Exists: " + Prop.Exists(BinocularsObject));
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("CamCreated: " + CamCreated);
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("CamFov: " + CamFov);
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("GetEntityRotation(PlayerPedId(), 2).Z: " + GetEntityRotation(PlayerPedId(), 2).Z);
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("BinocularsCamera.Rotation.Z: " + BinocularsCamera.Rotation.Z);
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("ScaleformRequested: " + ScaleformRequested);
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("Scaleform: " + Scaleform);
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("ObjectCreated: " + ObjectCreated);
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("binoculars_intro: " + IsEntityPlayingAnim(PlayerPedId(), "oddjobs@hunter", "binoculars_intro", 3));
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;

            SetTextFont(0);
            SetTextProportional(true);
            SetTextScale((float)0.00, (float)0.30);
            SetTextColour(255, 255, 255, 255);
            SetTextDropshadow(0, 0, 0, 0, 255);
            SetTextEdge(1, 0, 0, 0, 255);
            SetTextDropShadow();
            SetTextOutline();
            SetTextEntry("STRING");
            AddTextComponentString("binoculars_outro: " + IsEntityPlayingAnim(PlayerPedId(), "oddjobs@hunter", "binoculars_outro", 3));
            DrawText((float)0.575, (float)debugPos);
            debugPos = debugPos + 0.016;
        }

        private void OnKeyDown()
        {
            /*
            if (!IsControlJustReleased(0, 29))
                return;*/

            if (IsControlJustReleased(0, 29))
            {
                if (Main.PlayerIsReady == 0)
                    Main.GetPlayerReady();
                else
                    Main.ExitAnimStarting();
            }

            /*
            if (Settings.KeyboardKeyShift)
            {
                if (!e.Shift || e.KeyCode != Settings.KeyboardKeyToggle)
                    return;

                if (Main.PlayerIsReady == 0)
                    Main.GetPlayerReady();
                else
                    Main.ExitAnimStarting();
            }
            else
            {
                if (e.KeyCode != Settings.KeyboardKeyToggle)
                    return;
                if (Main.PlayerIsReady == 0)
                    Main.GetPlayerReady();
                else
                    Main.ExitAnimStarting();
            }*/
        }


        public static void Gamepad()
        {
            /*
            if (IsControlPressed(2, Settings.ControllerKeyToggle1))
            {
                if (IsControlPressed(2, Settings.ControllerKeyToggle2))
                {
                    if (!IsUsingKeyboard(2))
                    {
                        if (Main.PlayerIsReady == 0)
                          Main.GetPlayerReady();
                        else
                            Main.ExitAnimStarting();
                    }
                }
            }*/
            if (!IsControlJustPressed(0, 25))
            {
                if (!IsDisabledControlJustPressed(0, 25))
                    goto label_10;
            }
            if (Main.PlayerIsReady == 1)
                Main.ExitAnimStarting();

            label_10:
            if (!IsControlJustPressed(0, 202))
            {
                if (!IsDisabledControlJustPressed(0, 202))
                    return;
            }
            if (Main.PlayerIsReady != 1)
                return;

            Main.ExitAnimStarting();
        }

        public static void GetPlayerReady()
        {
            if (Main.PlayerIsReady != 0)
                return;
            if (!IsPlayerControlOn(PlayerId()) || IsPlayerCamControlDisabled() || UpdateOnscreenKeyboard() == 0)
                return;
            if (!IsPlayerFreeForAmbientTask(PlayerId()) || IsCutsceneActive() || IsCutscenePlaying() || IsMissionCompletePlaying())
                return;
            if (IsPlayerInCutscene(PlayerId()) || IsMinigameInProgress())
                return;
            if (IsPlayerDead(PlayerId()))
                return;
            if (IsPlayerBeingArrested(PlayerId(), true))
                return;
            if (!IsPlayerPlaying(PlayerId()))
                return;
            if (IsPedRagdoll(PlayerPedId()))
                return;
            if (IsPedRunningRagdollTask(PlayerPedId()))
                return;
            if (IsPedSwimming(PlayerPedId()))
                return;
            if (IsPedFalling(PlayerPedId()))
                return;
            if (IsPedInParachuteFreeFall(PlayerPedId()))
                return;
            if (IsPedClimbing(PlayerPedId()))
                return;
            if (IsPedVaulting(PlayerPedId()))
                return;
            if (IsPedDiving(PlayerPedId()))
                return;
            if (IsPedJumping(PlayerPedId()))
                return;
            if (IsPedJumpingOutOfVehicle(PlayerPedId()))
                return;
            if (IsPedGoingIntoCover(PlayerPedId()))
                return;
            if (IsPedInCover(PlayerPedId(), false))
                return;
            if (IsPedInMeleeCombat(PlayerPedId()))
                return;
            if (IsPlayerFreeAiming(PlayerId()))
                return;
            if (IsPlayerTargettingAnything(PlayerId()))
                return;
            if (IsPedShooting(PlayerPedId()))
                return;
            if (IsEntityOnFire(PlayerPedId()))
                return;
            if (IsPedReloading(PlayerPedId()))
                return;
            if (IsPedPlantingBomb(PlayerPedId()))
                return;
            if (IsHudComponentActive(19))
                return;
            if (IsPedRunningMobilePhoneTask(PlayerPedId()))
                return;
            if (IsPlayingPhoneGestureAnim(PlayerPedId()) || IsMobilePhoneCallOngoing())
                return;
            if (IsPedInAnyVehicle(PlayerPedId(), true))
                return;
            if (IsPedSittingInAnyVehicle(PlayerPedId()))
                return;
            if (IsPedGettingIntoAVehicle(PlayerPedId()))
                return;

            uint weap = ((uint)GetHashKey("WEAPON_UNARMED"));

            if (GetCurrentPedWeapon(PlayerPedId(), ref weap, true))
            {
                Main.CurrentWeapon = (int)weap;
                SetCurrentPedWeapon(PlayerPedId(), (uint)GetHashKey("WEAPON_UNARMED"), true);
            }
            if (IsPedWearingHelmet(PlayerPedId()))
                RemovePlayerHelmet(PlayerId(), true);

            Main.PlayerIsReady = 1;
            // Debug.WriteLine("PlayerIsReady");
            Debug.WriteLine("kevs-binoculars Enabled");
        }

        public static void EnterAnimStarting()
        {
            if (Main.PlayerIsReady != 1 || Main.EnterAnimIsStarted != 0)
                return;

            SetPlayerControl(PlayerId(), false, 256);

            Ped character = Game.Player.Character;
            character.Task.ClearAllImmediately();
            Wait(100);

            character.Task.PlayAnimation("oddjobs@hunter", "binoculars_intro", 8f, -8f, -1, AnimationFlags.StayInEndFrame, 0.0f);
            Wait(100);

            Main.EnterAnimIsStarted = 1;
            // Debug.WriteLine("EnterAnimIsStarted");
        }

        public static void EnterAnimPlaying()
        {
            if (Main.EnterAnimIsStarted != 1 || Main.EnterAnimIsPlayed != 0)
                return;

            if (!IsEntityPlayingAnim(PlayerPedId(), "oddjobs@hunter", "binoculars_intro", 3))
                return;

            Main.EnterAnimIsPlayed = 1;
            // Debug.WriteLine("EnterAnimIsPlayed");
        }

        public static void ExitAnimStarting()
        {
            if (Main.PlayerIsReady != 1 || Main.EnterAnimIsStarted != 1 || Main.ExitAnimIsStarted != 0 || Main.ObjectCreated != 1)
                return;

            Ped character = Game.Player.Character;
            SetPlayerControl(PlayerId(), true, 256);

            Wait(100);

            character.Task.PlayAnimation("oddjobs@hunter", "binoculars_outro", 8f, -8f, -1, AnimationFlags.None, 0.0f);
            Wait((int)GetAnimDuration("oddjobs@hunter", "binoculars_outro"));
                       
            Main.ExitAnimIsStarted = 1;
            // Debug.WriteLine("ExitAnimIsStarted");
        }

        public static void ExitAnimPlaying()
        {
            if (Main.ExitAnimIsStarted != 1 || Main.ExitAnimIsPlayed != 0)
                return;

            if (!IsEntityPlayingAnim(PlayerPedId(), "oddjobs@hunter", "binoculars_outro", 3))
                return;

            Main.ExitAnimIsPlayed = 1;
            // Debug.WriteLine("ExitAnimIsPlayed");
        }

        public static void AnimReset()
        {
            if (Main.PlayerIsReady != 1 || Main.EnterAnimIsStarted != 1 || Main.ExitAnimIsStarted != 1)
                return;

            if (IsEntityPlayingAnim(PlayerPedId(), "oddjobs@hunter", "binoculars_intro", 3))
                return;

            if (IsEntityPlayingAnim(PlayerPedId(), "oddjobs@hunter", "binoculars_outro", 3))
                return;

            SetCurrentPedWeapon(PlayerPedId(), (uint)Main.CurrentWeapon, true);

            Main.PlayerIsReady = 0;
            Main.EnterAnimIsStarted = 0;
            Main.EnterAnimIsPlayed = 0;
            Main.ExitAnimIsStarted = 0;
            Main.ExitAnimIsPlayed = 0;

            // Debug.WriteLine("AnimReset");
        }

        public async Task Object()
        {
            if (Main.EnterAnimIsStarted == 1 && Main.ObjectCreated == 0)
            {
                if (IsEntityPlayingAnim(PlayerPedId(), "oddjobs@hunter", "binoculars_intro", 3))
                {
                    if (GetEntityAnimCurrentTime(PlayerPedId(), "oddjobs@hunter", "binoculars_intro") > 0.241999998688698)
                    {
                        Vector3 pedBoneCoor = GetPedBoneCoords(PlayerPedId(), (int)Bone.PH_L_Hand, 0.0f, 0.0f, 0.0f);
                        Wait(100);
                        BinocularsObject = await World.CreateProp("prop_binoc_01", new Vector3(pedBoneCoor.X, pedBoneCoor.Y, pedBoneCoor.Z), true, false);
                        Wait(250);

                        if (!IsEntityAttached(PlayerPedId()))
                            AttachEntityToEntity(BinocularsObject.Handle, PlayerPedId(), GetPedBoneIndex(PlayerPedId(), 60309), 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, true, false, false, false, 2, true);

                        Main.ObjectCreated = 1;
                        // Debug.WriteLine("ObjectCreated");
                    }
                }
            }
            if (Main.ExitAnimIsStarted != 1 || Main.ObjectCreated != 1)
                return;

            if (!IsEntityPlayingAnim(PlayerPedId(), "oddjobs@hunter", "binoculars_outro", 3))
                return;

            if (GetEntityAnimCurrentTime(PlayerPedId(), "oddjobs@hunter", "binoculars_outro") <= 0.5)
                return;         
        }

        public static void Cam()
        {
            if (Main.EnterAnimIsStarted == 1 && Main.ExitAnimIsStarted == 0)
            {
                if (IsEntityPlayingAnim(PlayerPedId(), "oddjobs@hunter", "binoculars_intro", 3))
                {
                    if (GetEntityAnimCurrentTime(PlayerPedId(), "oddjobs@hunter", "binoculars_intro") > 0.5 && Main.CamCreated == 0)
                    {
                        DisplayRadar(false);
                        DisplayHud(false);

                        float num = GetEntityHeading(PlayerPedId());
                        Vector3 vector3 = GetEntityCoords(PlayerPedId(), true);

                        if (!Camera.Exists(BinocularsCamera))
                            BinocularsCamera = new Camera(CreateCameraWithParams((uint)GetHashKey("SNIPER_AIM_CAMERA"), vector3.X, vector3.Y, (vector3.Z + 0.6f), 0.0f, 0.0f, num - 360f, GetGameplayCamFov(), true, 2));

                        Wait(100);

                        if (!BinocularsCamera.IsActive)
                            BinocularsCamera.IsActive = true;

                        BinocularsCamera.Rotation = new Vector3(GetGameplayCamCoord().X, GetGameplayCamCoord().Y, num - 180f);

                        RenderScriptCams(true, false, 3000, true, false);
                        Wait(250);

                        Main.ZoomSound = GetSoundId();
                        Main.CamCreated = 1;
                        // Debug.WriteLine("CamCreated");
                    }
                }
            }
            if (Main.ExitAnimIsStarted != 1 || Main.CamCreated != 1)
                return;

            if (GetCamViewModeForContext(0) == 4)
            {
                Vector3 vector3 = BinocularsCamera.Rotation; //GetCamRot(Main.BinocularsCamera.Handle, 2);
                SetEntityRotation(PlayerPedId(), 0.0f, 0.0f, vector3.Z, 2, true);
                SetGameplayCamRawYaw(0.0f);
                SetGameplayCamRawPitch(vector3.X);
            }
            else
            {
                Vector3 vector3 = BinocularsCamera.Rotation; //GetCamRot(Main.BinocularsCamera.Handle, 2);
                SetEntityRotation(PlayerPedId(), 0.0f, 0.0f, vector3.Z, 2, true);
                SetGameplayCamRelativeHeading(0.0f);
            }

            DisplayRadar(true);
            DisplayHud(true);
            RenderScriptCams(false, false, 3000, true, true);

            if (BinocularsCamera.IsActive)
            {
                BinocularsCamera.IsActive = true; // SetCamActive(Main.BinocularsCamera.Handle, false);
                BinocularsCamera.Delete(); // DestroyCam(Main.BinocularsCamera.Handle, true);
            }

            Main.CamCreated = 0;
        }

        public static void RequestScaleformMovie()
        {
            if (Main.ScaleformRequested != 0)
                return;

            if (HasScaleformMovieLoaded(Main.Scaleform))
                return;

            Main.Scaleform = RequestScaleformMovie_2("binoculars");
            Main.ScaleformRequested = 1;
            // Debug.WriteLine("ScaleformRequested");
        }

        public static void ScaleformMovie()
        {
            if (Main.CamCreated != 1)
                return;

            if (IsHelpMessageBeingDisplayed())
                HideHelpTextThisFrame();

            HideHudAndRadarThisFrame();

            if (!HasScaleformMovieLoaded(Main.Scaleform))
                return;

            DrawScaleformMovie(Main.Scaleform, 0.5f, 0.5f, 1f, 1f, (int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, 0, 0);
        }

        public static void Vision()
        {
            if (Main.CamCreated == 1)
            {
                if (!IsControlJustPressed(2, 24))
                {
                    if (!IsDisabledControlJustPressed(2, 24))
                        goto label_19;
                }

                if (Main.NightVisionActivated == 0)
                {
                    if (!GetUsingnightvision())
                        SetNightvision(true);

                    Main.NightVisionActivated = 1;
                }
                else if (Main.NightVisionActivated == 1 && Main.ThermalVisionActivated == 0)
                {
                    if (GetUsingnightvision())
                        SetNightvision(false);

                    if (!GetUsingseethrough())
                        SetSeethrough(true);

                    Main.ThermalVisionActivated = 1;
                }
                else if (Main.NightVisionActivated == 1 && Main.ThermalVisionActivated == 1)
                {
                    if (GetUsingnightvision())
                        SetNightvision(false);

                    if (GetUsingseethrough())
                        SetSeethrough(false);

                    Main.ThermalVisionActivated = 0;
                    Main.NightVisionActivated = 0;
                }
            }

        label_19:
            if (Main.CamCreated != 0 || Main.NightVisionActivated != 1 && Main.ThermalVisionActivated != 1)
                return;

            if (GetUsingnightvision())
                SetNightvision(false);

            if (GetUsingseethrough())
                SetSeethrough(false);

            Main.NightVisionActivated = 0;
            Main.ThermalVisionActivated = 0;
        }

        public static void Sound()
        {
            if (Main.CamCreated == 1)
            {
                if (!IsControlPressed(0, 42))
                {
                    if (!IsDisabledControlPressed(0, 42))
                    {
                        if (!IsControlPressed(0, 43))
                        {
                            if (!IsDisabledControlPressed(0, 43))
                            {
                                if (HasSoundFinished(Main.ZoomSound))
                                    return;

                                StopSound(Main.ZoomSound);
                                return;
                            }
                        }
                    }
                }
                Main.CamFov = BinocularsCamera.FieldOfView; //GetCamFov(Main.BinocularsCamera.Handle);
                if ((double)Main.CamFov > 5.0 && (double)Main.CamFov < 45.0)
                {
                    if (!HasSoundFinished(Main.ZoomSound))
                        return;

                    PlaySoundFrontend(Main.ZoomSound, "Camera_Zoom", "Phone_Soundset_Default", true);
                }
                else
                {
                    if (HasSoundFinished(Main.ZoomSound))
                        return;

                    StopSound(Main.ZoomSound);
                }
            }
            else
            {
                if (HasSoundFinished(Main.ZoomSound))
                    return;

                StopSound(Main.ZoomSound);
            }
        }
        /*
        public static void PlayerHeading()
        {
            if (Main.CamCreated != 1)
                return;

            if (GetEntityRotation(PlayerPedId(), 2).Z == BinocularsCamera.Rotation.Z) //GetCamRot(Main.BinocularsCamera.Handle, 2).Z)
                return;
            
            SetEntityHeading(PlayerPedId(), BinocularsCamera.Rotation.Z);
        }*/

        public static void Cancel()
        {
            if (Main.PlayerIsReady != 1)
                return;

            if (Main.EnterAnimIsStarted == 1 && Main.EnterAnimIsPlayed == 1 && /*Main.BaseAnimIsPlayed == 0 &&*/ Main.ExitAnimIsStarted == 0 && Main.ExitAnimIsPlayed == 0)
            {
                if (!IsEntityPlayingAnim(PlayerPedId(), "oddjobs@hunter", "binoculars_intro", 3))
                    Main.ResetSettings();
            }
            if (Main.ExitAnimIsStarted == 1 && Main.ExitAnimIsPlayed == 1)
            {
                if (!IsEntityPlayingAnim(PlayerPedId(), "oddjobs@hunter", "binoculars_outro", 3))
                    Main.ResetSettings();
            }
            if (!IsPlayerDead(PlayerId()))
            {
                if (!IsPlayerBeingArrested(PlayerId(), true))
                    goto label_13;
            }
            Main.ResetSettings();

        label_13:
            if (!IsPedRagdoll(PlayerPedId()))
            {
                if (!IsPedRunningRagdollTask(PlayerPedId()))
                    goto label_16;
            }
            ClearPedTasks(PlayerPedId());
            Main.ResetSettings();

        label_16:
            if (!IsPedSwimming(PlayerPedId()))
            {
                if (!IsPedFalling(PlayerPedId()))
                {
                    if (!IsPedClimbing(PlayerPedId()))
                    {
                        if (!IsPedVaulting(PlayerPedId()))
                        {
                            if (!IsPedDiving(PlayerPedId()))
                            {
                                if (!IsPedInMeleeCombat(PlayerPedId()))
                                    return;
                            }
                        }
                    }
                }
            }
            ClearPedTasks(PlayerPedId());
            Main.ResetSettings();
        }

        public static void ResetSettings()
        {
            // Debug.WriteLine("ResetSettings");
            if (Main.ObjectCreated == 1)
            {
                if (Prop.Exists(BinocularsObject))
                {
                    var bino = BinocularsObject.Handle;
                    SetEntityAsMissionEntity(bino, true, true);
                    DetachEntity(bino, true, true);
                    DeleteObject(ref bino);

                    Main.ObjectCreated = 0;
                }
            }
            if (Main.CamCreated == 1)
            {
                if (GetCamViewModeForContext(0) == 4)
                {
                    Vector3 vector3 = BinocularsCamera.Rotation;  //Vector3 vector3 = GetCamRot(Main.BinocularsCamera.Handle, 2);
                    SetEntityRotation(PlayerPedId(), 0.0f, 0.0f, vector3.Z, 2, true);
                    SetGameplayCamRawYaw(0.0f);
                    SetGameplayCamRawPitch(vector3.X);
                }
                else
                {
                    Vector3 vector3 = BinocularsCamera.Rotation;  //Vector3 vector3 = GetCamRot(Main.BinocularsCamera.Handle, 2);
                    SetEntityRotation(PlayerPedId(), 0.0f, 0.0f, vector3.Z, 2, true);
                    SetGameplayCamRelativeHeading(0.0f);
                }

                DisplayRadar(true);
                DisplayHud(true);
                RenderScriptCams(false, false, 3000, true, false);

                if (BinocularsCamera.IsActive)
                {
                    //SetCamActive(Main.BinocularsCamera.Handle, false);
                    //DestroyCam(Main.BinocularsCamera.Handle, true);
                    BinocularsCamera.IsActive = false;
                    BinocularsCamera.Delete();
                }
                Main.CamCreated = 0;
            }
            if (GetUsingnightvision())
            {
                SetNightvision(false);
                Main.NightVisionActivated = 0;
            }
            if (GetUsingseethrough())
            {
                SetSeethrough(false);
                Main.ThermalVisionActivated = 0;
            }

            SetPlayerControl(PlayerId(), true, 256);
            SetCurrentPedWeapon(PlayerPedId(), (uint)Main.CurrentWeapon, true);

            Main.PlayerIsReady = 0;
            Main.EnterAnimIsStarted = 0;
            Main.EnterAnimIsPlayed = 0;
            Main.ExitAnimIsStarted = 0;
            Main.ExitAnimIsPlayed = 0;

            Debug.WriteLine("kevs-binoculars Disabled");
        }

        public static void PlayerControl()
        {
            if (Main.CamCreated != 1)
                return;

            DisableControlAction(2, 0, true);
        }
    }
}
