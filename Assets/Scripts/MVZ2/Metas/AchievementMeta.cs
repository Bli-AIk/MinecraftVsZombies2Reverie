﻿using System.Xml;
using MVZ2.IO;
using MVZ2Logic;
using PVZEngine;

namespace MVZ2.Metas
{
    public class AchievementMeta
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public SpriteReference Icon { get; private set; }
        public NamespaceID Unlock { get; private set; }
        public static AchievementMeta FromXmlNode(XmlNode node, string defaultNsp)
        {
            var id = node.GetAttribute("id");
            var name = node.GetAttribute("name");
            var description = node.GetAttribute("description");
            var icon = node.GetAttributeSpriteReference("icon", defaultNsp);
            var unlock = node.GetAttributeNamespaceID("unlock", defaultNsp);
            return new AchievementMeta()
            {
                ID = id,
                Name = name,
                Description = description,
                Icon = icon,
                Unlock = unlock,
            };
        }
    }
}
