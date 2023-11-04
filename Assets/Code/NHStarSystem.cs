﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ViewHorizons
{
    /// <summary>
    /// Class containing data related to New Horizons systems, from https://github.com/Outer-Wilds-New-Horizons/new-horizons/blob/main/NewHorizons/External/Configs/StarSystemConfig.cs
    /// </summary>
    [JsonObject(Title = "Star System")]
    public class NHStarSystem : MonoBehaviour
    {
        /// <summary>
        /// In this system should the player be able to rotate their map camera freely or be stuck above the plane of the solar system?
        /// </summary>
        public bool freeMapAngle;

        /// <summary>
        /// When well past the furthest orbit, should the player be summoned back to the star?
        /// </summary>
        public bool returnToSolarSystemWhenTooFar;

        /// <summary>
        /// An override value for the far clip plane. Allows you to see farther.
        /// </summary>
        public float farClipPlaneOverride;

        /// <summary>
        /// Whether this system can be warped to via the warp drive. If you set factRequiredForWarp, this will be true.
        /// </summary>
        [DefaultValue(true)] public bool canEnterViaWarpDrive = true;

        /// <summary>
        /// Do you want a clean slate for this star system? Or will it be a modified version of the original.
        /// </summary>
        [DefaultValue(true)] public bool destroyStockPlanets = true;

        /// <summary>
        /// Should the time loop be enabled in this system?
        /// </summary>
        [DefaultValue(true)] public bool enableTimeLoop = true;

        /// <summary>
        /// The FactID that must be revealed before it can be warped to. Don't set `canEnterViaWarpDrive` to `false` if
        /// you're using this, because it will be overwritten.
        /// </summary>
        public string factRequiredForWarp;

        /// <summary>
        /// The duration of the time loop in minutes. This is the time the sun explodes. End Times plays 85 seconds before this time, and your memories get sent back about 40 seconds after this time.
        /// </summary>
        [DefaultValue(22f)] public float loopDuration = 22f;

        /// <summary>
        /// Should the player not be able to view the map in this system?
        /// </summary>
        public bool mapRestricted;

        /// <summary>
        /// Customize the skybox for this system
        /// </summary>
        public SkyboxModule Skybox;

        /// <summary>
        /// Set to `true` if you want to spawn here after dying, not Timber Hearth. You can still warp back to the main star system.
        /// </summary>
        public bool startHere;

        /// <summary>
        /// Set to `true` if you want the player to stay in this star system if they die in it.
        /// </summary>
        public bool respawnHere;

        [Obsolete("travelAudioClip is deprecated, please use travelAudio instead")]
        public string travelAudioClip;

        [Obsolete("travelAudioFilePath is deprecated, please use travelAudio instead")]
        public string travelAudioFilePath;

        /// <summary>
        /// The audio that will play when travelling in space. Can be a path to a .wav/.ogg/.mp3 file, or taken from the AudioClip list.
        /// </summary>
        public string travelAudio;

        /// <summary>
        /// Configure warping to this system with the vessel
        /// </summary>
        public VesselModule Vessel;

        [Obsolete("coords is deprecated, please use Vessel.coords instead")]
        public NomaiCoordinates coords;

        [Obsolete("vesselPosition is deprecated, please use Vessel.vesselSpawn.position instead")]
        public MVector3 vesselPosition;

        [Obsolete("vesselRotation is deprecated, please use Vessel.vesselSpawn.rotation instead")]
        public MVector3 vesselRotation;

        [Obsolete("warpExitPosition is deprecated, please use Vessel.warpExit.position instead")]
        public MVector3 warpExitPosition;

        [Obsolete("warpExitRotation is deprecated, please use Vessel.warpExit.rotation instead")]
        public MVector3 warpExitRotation;

        /// <summary>
        /// Manually layout ship log entries in detective mode
        /// </summary>
        public EntryPositionInfo[] entryPositions;

        /// <summary>
        /// A list of fact IDs to reveal when the game starts.
        /// </summary>
        public string[] initialReveal;

        /// <summary>
        /// List colors of curiosity entries
        /// </summary>
        public CuriosityColorInfo[] curiosities;

        /// <summary>
        /// Extra data that may be used by extension mods
        /// </summary>
        public object extras;

        public class NomaiCoordinates
        {
            [MinLength(2)]
            [MaxLength(6)]
            public int[] x;

            [MinLength(2)]
            [MaxLength(6)]
            public int[] y;

            [MinLength(2)]
            [MaxLength(6)]
            public int[] z;
        }

        [JsonObject]
        public class SkyboxModule
        {

            /// <summary>
            /// Whether to destroy the star field around the player
            /// </summary>
            public bool destroyStarField;

            /// <summary>
            /// Whether to use a cube for the skybox instead of a smooth sphere
            /// </summary>
            public bool useCube;

            /// <summary>
            /// Relative filepath to the texture to use for the skybox's positive X direction
            /// </summary>
            public string rightPath;

            /// <summary>
            /// Relative filepath to the texture to use for the skybox's negative X direction
            /// </summary>
            public string leftPath;

            /// <summary>
            /// Relative filepath to the texture to use for the skybox's positive Y direction
            /// </summary>
            public string topPath;

            /// <summary>
            /// Relative filepath to the texture to use for the skybox's negative Y direction
            /// </summary>
            public string bottomPath;

            /// <summary>
            /// Relative filepath to the texture to use for the skybox's positive Z direction
            /// </summary>
            public string frontPath;

            /// <summary>
            /// Relative filepath to the texture to use for the skybox's negative Z direction
            /// </summary>
            public string backPath;
        }

        [JsonObject]
        public class VesselModule
        {
            /// <summary>
            /// Coordinates that the vessel can use to warp to your solar system.
            /// </summary>
            public NomaiCoordinates coords;

            /// <summary>
            /// A ship log fact which will make a prompt appear showing the coordinates when you're in the Vessel.
            /// </summary>
            public string promptFact;

            /// <summary>
            /// Whether the vessel should spawn in this system even if it wasn't used to warp to it. This will automatically power on the vessel.
            /// </summary>
            public bool alwaysPresent;

            /// <summary>
            /// Whether to always spawn the player on the vessel, even if it wasn't used to warp to the system.
            /// </summary>
            public bool spawnOnVessel;

            /// <summary>
            /// Whether the vessel should have physics enabled. Defaults to false if parentBody is set, and true otherwise.
            /// </summary>
            public bool? hasPhysics;

            /// <summary>
            /// Whether the vessel should have a zero-gravity volume around it. Defaults to false if parentBody is set, and true otherwise.
            /// </summary>
            public bool? hasZeroGravityVolume;

            /// <summary>
            /// The location that the vessel will warp to.
            /// </summary>
            public VesselInfo vesselSpawn;

            /// <summary>
            /// The location that you will be teleported to when you exit the vessel through the black hole.
            /// </summary>
            public WarpExitInfo warpExit;

            
            [Obsolete("vesselPosition is deprecated, use vesselSpawn.position instead")] public MVector3 vesselPosition;
            [Obsolete("vesselRotation is deprecated, use vesselSpawn.rotation instead")] public MVector3 vesselRotation;
            [Obsolete("warpExitPosition is deprecated, use vesselSpawn.position instead")] public MVector3 warpExitPosition;
            [Obsolete("warpExitRotation is deprecated, use vesselSpawn.rotation instead")] public MVector3 warpExitRotation;
            

            // We aren't running any code in the game, we just need to hold this data, therefore I'm commenting out the inheritances for now.
            [JsonObject]
            public class VesselInfo// : GeneralSolarSystemPropInfo
            {
            }

            [JsonObject]
            public class WarpExitInfo// : GeneralSolarSystemPropInfo
            {
                /// <summary>
                /// If set, keeps the warp exit attached to the vessel. Overrides `parentPath`.
                /// </summary>
                public bool attachToVessel;
            }
        }
    }
}