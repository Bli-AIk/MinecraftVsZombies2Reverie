using PVZEngine.Serialization;

namespace PVZEngine
{
    public class Pickup : Entity
    {
        #region ���з���
        internal Pickup(Game level) : base(level)
        {
        }
        public Pickup(Game level, int id, EntityDefinition definition, int seed) : base(level, id, definition, seed)
        {
        }
        protected override SerializableEntity CreateSerializableEntity() => new SerializableEntity();
        #endregion

        #region �����ֶ�
        public override int Type => EntityTypes.PICKUP;
        #endregion
    }

}