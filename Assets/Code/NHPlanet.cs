using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UnityEngine;

namespace ViewHorizons
{
    public class NHPlanet
    {
        [JsonObject(Title = "Celestial Body")]
        public class PlanetConfig
        {
            #region Fields
            /// <summary>
            /// Unique name of your planet
            /// </summary>
            [Required]
            public string name;

            /// <summary>
            /// Unique star system containing your planet. If you set this to be a custom solar system remember to add a Spawn module to one of the bodies, or else you can't get to the system.
            /// </summary>
            [DefaultValue("SolarSystem")] public string starSystem = "SolarSystem";

            /// <summary>
            /// Does this config describe a quantum state of a custom planet defined in another file?
            /// </summary>
            public bool isQuantumState;

            /// <summary>
            /// Does this config describe a stellar remnant of a custom star defined in another file?
            /// </summary>
            public bool isStellarRemnant;

            /// <summary>
            /// Should this planet ever be shown on the title screen?
            /// </summary>
            [DefaultValue(true)] public bool canShowOnTitle = true;

            /// <summary>
            /// `true` if you want to delete this planet
            /// </summary>
            public bool destroy;

            /// <summary>
            /// A list of paths to child GameObjects to destroy on this planet
            /// </summary>
            public string[] removeChildren;

            #endregion

            #region Modules
            /// <summary>
            /// Add ambient lights to this body
            /// </summary>
            public AmbientLightModule[] AmbientLights;

            /// <summary>
            /// Generate asteroids around this body
            /// </summary>
            public AsteroidBeltModule AsteroidBelt;

            /// <summary>
            /// Describes this Body's atmosphere
            /// </summary>
            public AtmosphereModule Atmosphere;

            /// <summary>
            /// Base Properties of this Body
            /// </summary>
            public BaseModule Base;

            /// <summary>
            /// Add bramble nodes to this planet and/or make this planet a bramble dimension
            /// </summary>
            public BrambleModule Bramble;

            /// <summary>
            /// Add a cloaking field to this planet
            /// </summary>
            public CloakModule Cloak;

            /// <summary>
            /// Make this body into a focal point (barycenter)
            /// </summary>
            public FocalPointModule FocalPoint;

            /// <summary>
            /// Add funnel from this planet to another
            /// </summary>
            public FunnelModule Funnel;

            /// <summary>
            /// Generate the surface of this planet using a heightmap
            /// </summary>
            public HeightMapModule HeightMap;

            /// <summary>
            /// Add lava to this planet
            /// </summary>
            public LavaModule Lava;

            /// <summary>
            /// Describes this Body's orbit (or lack there of)
            /// </summary>
            public OrbitModule Orbit;

            /// <summary>
            /// Procedural Generation
            /// </summary>
            public ProcGenModule ProcGen;

            /// <summary>
            /// Spawn various objects on this body
            /// </summary>
            public PropModule Props;

            /// <summary>
            /// Reference frame properties of this body
            /// </summary>
            public ReferenceFrameModule ReferenceFrame;

            /// <summary>
            /// Create rings around the planet
            /// </summary>
            public RingModule[] Rings;

            /// <summary>
            /// Add sand to this planet
            /// </summary>
            public SandModule Sand;

            /// <summary>
            /// Add ship log entries to this planet and describe how it looks in map mode
            /// </summary>
            public ShipLogModule ShipLog;

            /// <summary>
            /// Settings for shock effect on planet when the nearest star goes supernova
            /// </summary>
            public ShockEffectModule ShockEffect;

            /// <summary>
            /// Spawn the player at this planet
            /// </summary>
            public SpawnModule Spawn;

            /// <summary>
            /// Make this body a star
            /// </summary>
            public StarModule Star;

            /// <summary>
            /// Add water to this planet
            /// </summary>
            public WaterModule Water;

            /// <summary>
            /// Add particle effects in a field around the planet.
            /// Also known as Vection Fields.
            /// </summary>
            public ParticleFieldModule[] ParticleFields;

            /// <summary>
            /// Add various volumes on this body
            /// </summary>
            public VolumesModule Volumes;

            /// <summary>
            /// Add a comet tail to this body, like the Interloper
            /// </summary>
            public CometTailModule CometTail;

            /// <summary>
            /// Extra data that may be used by extension mods
            /// </summary>
            public object extras;

            #endregion

            #region Obsolete

            [Obsolete("ChildrenToDestroy is deprecated, please use RemoveChildren instead")]
            public string[] childrenToDestroy;

            [Obsolete("Singularity is deprecated, please use Props->singularities")]
            public SingularityModule Singularity;

            [Obsolete("Signal is deprecated, please use Props->signals")]
            public SignalModule Signal;

            [Obsolete("Ring is deprecated, please use Rings")]
            public RingModule Ring;

            #endregion Obsolete

        }
    }
}