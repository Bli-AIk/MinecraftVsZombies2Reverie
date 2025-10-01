using MVZ2.Vanilla.Entities;
using MVZ2.Vanilla.Grids;
using PVZEngine.Damages;
using PVZEngine.Entities;
using PVZEngine.Level;
using UnityEngine;

namespace MVZ2.Reverie.GameContent.Contraptions
{
    [EntityBehaviourDefinition(ReverieContraptionNames.blockOfRedstone)]
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

            for (var x = -1; x < 2; x++)
            {
                for (var y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                    
                    var poweredEntity = entity.Level
                        .GetGrid(entity.GetColumn() + x, entity.GetLane() + y)
                        ?.GetMainEntity();
                    Debug.Log(poweredEntity);
                }
            }
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