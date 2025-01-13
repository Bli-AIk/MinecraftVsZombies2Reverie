using MVZ2.GameContent.Areas;
using MVZ2.GameContent.Pickups;
using MVZ2.Vanilla;
using MVZ2.Vanilla.Audios;
using MVZ2.Vanilla.Entities;
using MVZ2.Vanilla.Level;
using MVZ2Logic.Level;
using PVZEngine;
using PVZEngine.Entities;
using PVZEngine.Level;
using Tools;

namespace MVZ2.GameContent.Effects
{
    [Definition(VanillaEffectNames.miner)]
    public class Miner : EffectBehaviour
    {
        #region ���з���
        public Miner(string nsp, string name) : base(nsp, name)
        {

        }
        public override void Init(Entity entity)
        {
            base.Init(entity);
            var timer = new FrameTimer(START_TIME);
            SetProductTimer(entity, timer);
            entity.SetShadowHidden(false);
        }
        public override void Update(Entity entity)
        {
            base.Update(entity);
            if (!entity.Level.IsNoProduction())
            {
                var timer = GetProductTimer(entity);
                timer.Run();
                if (timer.Expired)
                {
                    entity.Produce(VanillaPickupID.redstone);
                    entity.PlaySound(VanillaSoundID.throwSound);
                    timer.ResetTime(PRODUCE_TIME);
                }
                entity.SetAnimationBool("Open", true);
            }
            else
            {
                entity.SetAnimationBool("Open", false);
            }
        }
        public static FrameTimer GetProductTimer(Entity entity) => entity.GetBehaviourField<FrameTimer>(ID, PROP_PRODUCE_TIMER);
        public static void SetProductTimer(Entity entity, FrameTimer value) => entity.SetBehaviourField(ID, PROP_PRODUCE_TIMER, value);
        #endregion

        #region �����ֶ�

        private static readonly NamespaceID ID = VanillaEffectID.miner;
        public const string PROP_IS_OPEN = "isOpen";
        public const string PROP_PRODUCE_TIMER = "produceTimer";
        public const int START_TIME = 120;
        public const int PRODUCE_TIME = 300;
        #endregion
    }
}