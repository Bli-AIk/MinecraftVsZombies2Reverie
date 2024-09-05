using MVZ2.Vanilla;
using PVZEngine.LevelManaging;

namespace MVZ2.GameContent.Effects
{
    [Definition(EffectNames.mineDebris)]
    public class MineDebris : VanillaEffect
    {
        #region ���з���
        public MineDebris(string nsp, string name) : base(nsp, name)
        {
        }
        public override void Init(Entity entity)
        {
            entity.Timeout = 30;
        }
        public override void Update(Entity entity)
        {
            entity.Timeout--;
            if (entity.Timeout <= 0)
            {
                entity.Remove();
            }
        }
        #endregion
    }
}