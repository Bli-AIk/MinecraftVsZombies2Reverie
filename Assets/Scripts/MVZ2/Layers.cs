﻿using System;
using UnityEngine;

namespace MVZ2
{
    public static class Layers
    {
        public static readonly int DEFAULT = LayerMask.NameToLayer("Default");
        public static readonly int GRID = LayerMask.NameToLayer("Grid");
        public static readonly int RAYCAST_RECEIVER = LayerMask.NameToLayer("RaycastReceiver");
        public static readonly int PICKUP = LayerMask.NameToLayer("Pickup");
        public static readonly int LIGHT = LayerMask.NameToLayer("Light");
        public static LayerMask GetMask(params int[] layers)
        {
            if (layers == null)
            {
                throw new ArgumentNullException("layers");
            }

            int num = 0;
            foreach (var num2 in layers)
            {
                num |= 1 << num2;
            }
            return num;
        }
    }
}
