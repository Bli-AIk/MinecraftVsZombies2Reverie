﻿using System;
using System.Linq;
using MVZ2Logic.Saves;
using PVZEngine;

namespace MVZ2.Vanilla.Saves
{
    public class VanillaSaveData : ModSaveData
    {
        public VanillaSaveData(string spaceName) : base(spaceName)
        {
        }
        protected override SerializableModSaveData CreateSerializable()
        {
            return new SerializableVanillaSaveData()
            {
                version = 0,
                lastMapID = LastMapID,
                mapTalkID = MapTalkID,
                money = money,
                lastSelection = LastSelection
            };
        }
        public void LoadSerializable(SerializableVanillaSaveData serializable)
        {
            LoadFromSerializable(serializable);
            LastMapID = serializable.lastMapID;
            MapTalkID = serializable.mapTalkID;
            money = serializable.money;
            LastSelection = serializable.lastSelection;
        }

        public int GetMoney()
        {
            return money;
        }
        public void SetMoney(int value)
        {
            money = Math.Clamp(value, 0, 999990);
        }
        public int GetBlueprintSlots()
        {
            return MIN_BLUEPRINT_SLOTS + blueprintSlotUnlocks.Count(u => IsUnlocked(u));
        }
        public int GetArtifactSlots()
        {
            return MIN_ARTIFACT_SLOTS + artifactSlotUnlocks.Count(u => IsUnlocked(u));
        }
        public int GetStarshardSlots()
        {
            return MIN_STARSHARD_SLOTS + starshardSlotUnlocks.Count(u => IsUnlocked(u));
        }
        public const int MIN_BLUEPRINT_SLOTS = 6;
        public const int MIN_ARTIFACT_SLOTS = 1;
        public const int MIN_STARSHARD_SLOTS = 3;
        public NamespaceID LastMapID { get; set; }
        public NamespaceID MapTalkID { get; set; }
        public BlueprintSelection LastSelection { get; set; }
        private static string[] blueprintSlotUnlocks = new string[]
        {
            VanillaUnlockNames.blueprintSlot1,
            VanillaUnlockNames.blueprintSlot2,
            VanillaUnlockNames.blueprintSlot3,
            VanillaUnlockNames.blueprintSlot4,
        };
        private static string[] artifactSlotUnlocks = new string[]
        {
            VanillaUnlockNames.artifactSlot1,
            VanillaUnlockNames.artifactSlot2,
        };
        private static string[] starshardSlotUnlocks = new string[]
        {
            VanillaUnlockNames.starshardSlot1,
            VanillaUnlockNames.starshardSlot2,
        };
        private int money;
    }
    [Serializable]
    public class SerializableVanillaSaveData : SerializableModSaveData
    {
        public VanillaSaveData Deserialize()
        {
            var saveData = new VanillaSaveData(spaceName);
            saveData.LoadSerializable(this);
            return saveData;
        }
        public NamespaceID lastMapID;
        public NamespaceID mapTalkID;
        public int money;
        public BlueprintSelection lastSelection;
        [Obsolete]
        public int artifactSlots;
        [Obsolete]
        public int blueprintSlots;
        [Obsolete]
        public int starshardSlots;
    }
}
