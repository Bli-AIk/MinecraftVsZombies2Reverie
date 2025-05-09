﻿using System.Collections.Generic;
using System.Linq;
using PVZEngine.Level;

namespace PVZEngine.Auras
{
    public class AuraEffectList
    {
        public void Add(LevelEngine level, AuraEffect auraEffect)
        {
            auraEffects.Add(auraEffect);
        }
        public bool Remove(LevelEngine level, AuraEffect auraEffect)
        {
            return auraEffects.Remove(auraEffect);
        }
        public void PostAdd()
        {
            foreach (var auraEffect in auraEffects)
            {
                auraEffect.PostAdd();
            }
        }
        public void PostRemove()
        {
            foreach (var auraEffect in auraEffects)
            {
                auraEffect.PostRemove();
            }
        }
        public AuraEffect Get<T>() where T : AuraEffectDefinition
        {
            return auraEffects.FirstOrDefault(a => a.Definition is T);
        }
        public AuraEffect Get(AuraEffectDefinition auraDef)
        {
            return auraEffects.FirstOrDefault(a => a.Definition == auraDef);
        }
        public AuraEffect[] GetAll()
        {
            return auraEffects.ToArray();
        }
        public void Clear()
        {
            foreach (var auraEffect in auraEffects)
            {
                auraEffect.PostRemove();
            }
            auraEffects.Clear();
        }
        public void Update()
        {
            foreach (var aura in auraEffects)
            {
                aura.UpdateAuraInterval();
            }
        }
        public void LoadFromSerializable(LevelEngine level, IEnumerable<SerializableAuraEffect> effects)
        {
            if (effects == null)
                return;
            foreach (var aura in auraEffects)
            {
                var seri = effects.FirstOrDefault(e => e.id == aura.ID);
                aura.LoadFromSerializable(level, seri);
            }
        }
        private List<AuraEffect> auraEffects = new List<AuraEffect>();
    }
}
