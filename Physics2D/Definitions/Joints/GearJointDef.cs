using Physics2D.Dynamics.Joints;
using Physics2D.Dynamics.Joints.Misc;

namespace Physics2D.Definitions.Joints
{
    public sealed class GearJointDef : JointDef
    {
        public GearJointDef() : base(JointType.Gear)
        {
            SetDefaults();
        }

        /// <summary>The first revolute/prismatic joint attached to the gear joint.</summary>
        public Joint JointA { get; set; }

        /// <summary>The second revolute/prismatic joint attached to the gear joint.</summary>
        public Joint JointB { get; set; }

        /// <summary>The gear ratio.</summary>
        public float Ratio { get; set; }

        public override void SetDefaults()
        {
            JointA = null;
            JointB = null;
            Ratio = 1.0f;
        }
    }
}