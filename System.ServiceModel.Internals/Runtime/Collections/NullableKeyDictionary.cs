using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.Collections
{
	// Token: 0x0200004F RID: 79
	internal class NullableKeyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x06000308 RID: 776 RVA: 0x000107EC File Offset: 0x0000E9EC
		public NullableKeyDictionary()
		{
			this.innerDictionary = new Dictionary<TKey, TValue>();
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000309 RID: 777 RVA: 0x000107FF File Offset: 0x0000E9FF
		public int Count
		{
			get
			{
				return this.innerDictionary.Count + (this.isNullKeyPresent ? 1 : 0);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600030A RID: 778 RVA: 0x000031F5 File Offset: 0x000013F5
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00010819 File Offset: 0x0000EA19
		public ICollection<TKey> Keys
		{
			get
			{
				return new NullableKeyDictionary<TKey, TValue>.NullKeyDictionaryKeyCollection<TKey, TValue>(this);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00010821 File Offset: 0x0000EA21
		public ICollection<TValue> Values
		{
			get
			{
				return new NullableKeyDictionary<TKey, TValue>.NullKeyDictionaryValueCollection<TKey, TValue>(this);
			}
		}

		// Token: 0x1700007A RID: 122
		public TValue this[TKey key]
		{
			get
			{
				if (key != null)
				{
					return this.innerDictionary[key];
				}
				if (this.isNullKeyPresent)
				{
					return this.nullKeyValue;
				}
				throw Fx.Exception.AsError(new KeyNotFoundException());
			}
			set
			{
				if (key == null)
				{
					this.isNullKeyPresent = true;
					this.nullKeyValue = value;
					return;
				}
				this.innerDictionary[key] = value;
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00010884 File Offset: 0x0000EA84
		public void Add(TKey key, TValue value)
		{
			if (key != null)
			{
				this.innerDictionary.Add(key, value);
				return;
			}
			if (this.isNullKeyPresent)
			{
				throw Fx.Exception.Argument("key", InternalSR.NullKeyAlreadyPresent);
			}
			this.isNullKeyPresent = true;
			this.nullKeyValue = value;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000108D2 File Offset: 0x0000EAD2
		public bool ContainsKey(TKey key)
		{
			if (key != null)
			{
				return this.innerDictionary.ContainsKey(key);
			}
			return this.isNullKeyPresent;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000108F0 File Offset: 0x0000EAF0
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				bool result = this.isNullKeyPresent;
				this.isNullKeyPresent = false;
				this.nullKeyValue = default(TValue);
				return result;
			}
			return this.innerDictionary.Remove(key);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001092D File Offset: 0x0000EB2D
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key != null)
			{
				return this.innerDictionary.TryGetValue(key, out value);
			}
			if (this.isNullKeyPresent)
			{
				value = this.nullKeyValue;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00010963 File Offset: 0x0000EB63
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00010979 File Offset: 0x0000EB79
		public void Clear()
		{
			this.isNullKeyPresent = false;
			this.nullKeyValue = default(TValue);
			this.innerDictionary.Clear();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0001099C File Offset: 0x0000EB9C
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			if (item.Key != null)
			{
				return this.innerDictionary.Contains(item);
			}
			if (!this.isNullKeyPresent)
			{
				return false;
			}
			if (item.Value != null)
			{
				TValue value = item.Value;
				return value.Equals(this.nullKeyValue);
			}
			return this.nullKeyValue == null;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00010A0C File Offset: 0x0000EC0C
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.innerDictionary.CopyTo(array, arrayIndex);
			if (this.isNullKeyPresent)
			{
				array[arrayIndex + this.innerDictionary.Count] = new KeyValuePair<TKey, TValue>(default(TKey), this.nullKeyValue);
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00010A55 File Offset: 0x0000EC55
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			if (item.Key != null)
			{
				return this.innerDictionary.Remove(item);
			}
			if (this.Contains(item))
			{
				this.isNullKeyPresent = false;
				this.nullKeyValue = default(TValue);
				return true;
			}
			return false;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00010A91 File Offset: 0x0000EC91
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this.innerDictionary)
			{
				yield return keyValuePair;
			}
			if (this.isNullKeyPresent)
			{
				yield return new KeyValuePair<TKey, TValue>(default(TKey), this.nullKeyValue);
			}
			yield break;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<TKey, TValue>>)this).GetEnumerator();
		}

		// Token: 0x040001AC RID: 428
		private bool isNullKeyPresent;

		// Token: 0x040001AD RID: 429
		private TValue nullKeyValue;

		// Token: 0x040001AE RID: 430
		private IDictionary<TKey, TValue> innerDictionary;

		// Token: 0x02000093 RID: 147
		private class NullKeyDictionaryKeyCollection<TypeKey, TypeValue> : ICollection<TypeKey>, IEnumerable<TypeKey>, IEnumerable
		{
			// Token: 0x0600043A RID: 1082 RVA: 0x00013A64 File Offset: 0x00011C64
			public NullKeyDictionaryKeyCollection(NullableKeyDictionary<TypeKey, TypeValue> nullKeyDictionary)
			{
				this.nullKeyDictionary = nullKeyDictionary;
			}

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x0600043B RID: 1083 RVA: 0x00013A74 File Offset: 0x00011C74
			public int Count
			{
				get
				{
					int num = this.nullKeyDictionary.innerDictionary.Keys.Count;
					if (this.nullKeyDictionary.isNullKeyPresent)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x0600043C RID: 1084 RVA: 0x00002940 File Offset: 0x00000B40
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600043D RID: 1085 RVA: 0x00013AA9 File Offset: 0x00011CA9
			public void Add(TypeKey item)
			{
				throw Fx.Exception.AsError(new NotSupportedException(InternalSR.KeyCollectionUpdatesNotAllowed));
			}

			// Token: 0x0600043E RID: 1086 RVA: 0x00013AA9 File Offset: 0x00011CA9
			public void Clear()
			{
				throw Fx.Exception.AsError(new NotSupportedException(InternalSR.KeyCollectionUpdatesNotAllowed));
			}

			// Token: 0x0600043F RID: 1087 RVA: 0x00013ABF File Offset: 0x00011CBF
			public bool Contains(TypeKey item)
			{
				if (item != null)
				{
					return this.nullKeyDictionary.innerDictionary.Keys.Contains(item);
				}
				return this.nullKeyDictionary.isNullKeyPresent;
			}

			// Token: 0x06000440 RID: 1088 RVA: 0x00013AEC File Offset: 0x00011CEC
			public void CopyTo(TypeKey[] array, int arrayIndex)
			{
				this.nullKeyDictionary.innerDictionary.Keys.CopyTo(array, arrayIndex);
				if (this.nullKeyDictionary.isNullKeyPresent)
				{
					array[arrayIndex + this.nullKeyDictionary.innerDictionary.Keys.Count] = default(TypeKey);
				}
			}

			// Token: 0x06000441 RID: 1089 RVA: 0x00013AA9 File Offset: 0x00011CA9
			public bool Remove(TypeKey item)
			{
				throw Fx.Exception.AsError(new NotSupportedException(InternalSR.KeyCollectionUpdatesNotAllowed));
			}

			// Token: 0x06000442 RID: 1090 RVA: 0x00013B43 File Offset: 0x00011D43
			public IEnumerator<TypeKey> GetEnumerator()
			{
				foreach (TypeKey typeKey in this.nullKeyDictionary.innerDictionary.Keys)
				{
					yield return typeKey;
				}
				IEnumerator<TypeKey> enumerator = null;
				if (this.nullKeyDictionary.isNullKeyPresent)
				{
					TypeKey typeKey2 = default(TypeKey);
				}
				yield break;
				yield break;
			}

			// Token: 0x06000443 RID: 1091 RVA: 0x00013B52 File Offset: 0x00011D52
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<TypeKey>)this).GetEnumerator();
			}

			// Token: 0x040002B7 RID: 695
			private NullableKeyDictionary<TypeKey, TypeValue> nullKeyDictionary;
		}

		// Token: 0x02000094 RID: 148
		private class NullKeyDictionaryValueCollection<TypeKey, TypeValue> : ICollection<TypeValue>, IEnumerable<TypeValue>, IEnumerable
		{
			// Token: 0x06000444 RID: 1092 RVA: 0x00013B5A File Offset: 0x00011D5A
			public NullKeyDictionaryValueCollection(NullableKeyDictionary<TypeKey, TypeValue> nullKeyDictionary)
			{
				this.nullKeyDictionary = nullKeyDictionary;
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x06000445 RID: 1093 RVA: 0x00013B6C File Offset: 0x00011D6C
			public int Count
			{
				get
				{
					int num = this.nullKeyDictionary.innerDictionary.Values.Count;
					if (this.nullKeyDictionary.isNullKeyPresent)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000446 RID: 1094 RVA: 0x00002940 File Offset: 0x00000B40
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000447 RID: 1095 RVA: 0x00013BA1 File Offset: 0x00011DA1
			public void Add(TypeValue item)
			{
				throw Fx.Exception.AsError(new NotSupportedException(InternalSR.ValueCollectionUpdatesNotAllowed));
			}

			// Token: 0x06000448 RID: 1096 RVA: 0x00013BA1 File Offset: 0x00011DA1
			public void Clear()
			{
				throw Fx.Exception.AsError(new NotSupportedException(InternalSR.ValueCollectionUpdatesNotAllowed));
			}

			// Token: 0x06000449 RID: 1097 RVA: 0x00013BB8 File Offset: 0x00011DB8
			public bool Contains(TypeValue item)
			{
				return this.nullKeyDictionary.innerDictionary.Values.Contains(item) || (this.nullKeyDictionary.isNullKeyPresent && this.nullKeyDictionary.nullKeyValue.Equals(item));
			}

			// Token: 0x0600044A RID: 1098 RVA: 0x00013C0C File Offset: 0x00011E0C
			public void CopyTo(TypeValue[] array, int arrayIndex)
			{
				this.nullKeyDictionary.innerDictionary.Values.CopyTo(array, arrayIndex);
				if (this.nullKeyDictionary.isNullKeyPresent)
				{
					array[arrayIndex + this.nullKeyDictionary.innerDictionary.Values.Count] = this.nullKeyDictionary.nullKeyValue;
				}
			}

			// Token: 0x0600044B RID: 1099 RVA: 0x00013BA1 File Offset: 0x00011DA1
			public bool Remove(TypeValue item)
			{
				throw Fx.Exception.AsError(new NotSupportedException(InternalSR.ValueCollectionUpdatesNotAllowed));
			}

			// Token: 0x0600044C RID: 1100 RVA: 0x00013C65 File Offset: 0x00011E65
			public IEnumerator<TypeValue> GetEnumerator()
			{
				foreach (TypeValue typeValue in this.nullKeyDictionary.innerDictionary.Values)
				{
					yield return typeValue;
				}
				IEnumerator<TypeValue> enumerator = null;
				if (this.nullKeyDictionary.isNullKeyPresent)
				{
					yield return this.nullKeyDictionary.nullKeyValue;
				}
				yield break;
				yield break;
			}

			// Token: 0x0600044D RID: 1101 RVA: 0x00013C74 File Offset: 0x00011E74
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<TypeValue>)this).GetEnumerator();
			}

			// Token: 0x040002B8 RID: 696
			private NullableKeyDictionary<TypeKey, TypeValue> nullKeyDictionary;
		}
	}
}
