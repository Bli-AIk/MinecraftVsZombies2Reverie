using PVZEngine;

namespace MVZ2.Reverie.GameContent.Contraptions
{
    public static class ReverieContraptionNames
    {
        public const string blockOfRedstone = "block_of_redstone";
    }

    public static class ReverieContraptionID
    {
        public static readonly NamespaceID blockOfRedstone = Get(ReverieContraptionNames.blockOfRedstone);
        
        private static NamespaceID Get(string name)
        {
            return new NamespaceID(ReverieMod.spaceName, name);
        }
    }
}