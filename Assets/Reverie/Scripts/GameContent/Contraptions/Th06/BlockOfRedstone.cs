using MVZ2.Vanilla.Entities;
using PVZEngine.Damages;
using PVZEngine.Entities;

namespace Reverie.GameContent.Contraptions
{
    public class BlockOfRedstone : ContraptionBehaviour
    {
        public BlockOfRedstone(string nsp, string name) : base(nsp, name)
        {
        }

        public override void Init(Entity entity)
        {
            base.Init(entity);
        }

        protected override void UpdateAI(Entity entity)
        {
            base.UpdateAI(entity);
        }


        protected override void UpdateLogic(Entity entity)
        {
            base.UpdateLogic(entity);
        }


        public override void PostDeath(Entity entity, DeathInfo deathInfo)
        {
            base.PostDeath(entity, deathInfo);
        }
    }
}