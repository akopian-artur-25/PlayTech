using System;
using System.Collections.Generic;
using System.Text;

namespace PlayTech.Shared.Utils
{
    public class KeyValue<TKey, TValue>
    {
        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public KeyValue()
        {
        }

        public KeyValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public KeyValue<TKey, TValue> Clone()
        {
            return new KeyValue<TKey, TValue>(Key, Value);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append('[');
            if (Key != null)
            {
                builder.Append(Key);
            }
            builder.Append(", ");
            if (Value != null)
            {
                builder.Append(Value);
            }
            builder.Append(']');
            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is KeyValue<TKey, TValue> pair && Equals(Key, pair.Key);
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}
