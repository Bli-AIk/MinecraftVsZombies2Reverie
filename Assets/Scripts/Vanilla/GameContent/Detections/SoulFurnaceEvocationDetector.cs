﻿using MVZ2.Vanilla.Detections;
using MVZ2.Vanilla.Entities;
using PVZEngine.Entities;
using UnityEngine;

namespace MVZ2.GameContent.Detections
{
    public class SoulFurnaceEvocationDetector : Detector
    {
        protected override Bounds GetDetectionBounds(Entity self)
        {
            var sizeX = 800;
            var sizeY = self.GetScaledSize().y;
            var sizeZ = 800;
            var source = self.Position;
            var centerX = source.x + sizeX * 0.5f * self.GetFacingX();
            var centerY = source.y + sizeY * 0.5f;
            var centerZ = source.z;
            return new Bounds(new Vector3(centerX, centerY, centerZ), new Vector3(sizeX, sizeY, sizeZ));
        }
    }
}
