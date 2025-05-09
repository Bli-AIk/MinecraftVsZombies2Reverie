﻿using System;

namespace PVZEngine
{
    public struct PropertyKeyString
    {
        public string propertyKey;

        public PropertyKeyString(string propertyKey)
        {
            this.propertyKey = propertyKey;
        }

        public override bool Equals(object obj)
        {
            return obj is PropertyKeyString key &&
                   propertyKey == key.propertyKey;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(propertyKey);
        }
        public static bool IsValid(PropertyKeyString key)
        {
            return !string.IsNullOrEmpty(key.propertyKey);
        }
        public static bool operator ==(PropertyKeyString lhs, PropertyKeyString rhs)
        {
            return lhs.propertyKey == rhs.propertyKey;
        }
        public static bool operator !=(PropertyKeyString lhs, PropertyKeyString rhs)
        {
            return lhs.propertyKey != rhs.propertyKey;
        }
        public static implicit operator PropertyKeyString(string key)
        {
            return new PropertyKeyString(key);
        }
        public static implicit operator string(PropertyKeyString meta)
        {
            return meta.propertyKey;
        }
    }
}
