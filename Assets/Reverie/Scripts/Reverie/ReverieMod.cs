using MVZ2.Reverie.Saves;
using MVZ2Logic.Games;
using MVZ2Logic.Modding;
using MVZ2Logic.Saves;

namespace MVZ2.Reverie
{
    public class ReverieMod : Mod
    {
        public ReverieMod() : base(spaceName)
        {
        }
        public override void Init(IGlobalGame game)
        {
            base.Init(game);
            RegisterSerializableType<SerializableReverieSaveData>();
        }

        #region 存档
        public override ModSaveData CreateSaveData()
        {
            return new ReverieSaveData(spaceName);
        }
        public override ModSaveData LoadSaveData(string json)
        {
            var serializable = Deserialize<SerializableReverieSaveData>(json);
            return ReverieSaveData.DeserializeFrom(serializable);
        }
        #endregion

        public const string spaceName = "mvz2r";
    }
}