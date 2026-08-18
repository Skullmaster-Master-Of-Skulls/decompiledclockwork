using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Specialized
{
	// Token: 0x020003AE RID: 942
	[Serializable]
	public abstract class NameObjectCollectionBase : ICollection, IEnumerable, ISerializable, IDeserializationCallback
	{
		// Token: 0x06002333 RID: 9011 RVA: 0x000A6F71 File Offset: 0x000A5171
		protected NameObjectCollectionBase() : this(NameObjectCollectionBase.defaultComparer)
		{
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000A6F80 File Offset: 0x000A5180
		protected NameObjectCollectionBase(IEqualityComparer equalityComparer)
		{
			IEqualityComparer keyComparer;
			if (equalityComparer != null)
			{
				keyComparer = equalityComparer;
			}
			else
			{
				IEqualityComparer equalityComparer2 = NameObjectCollectionBase.defaultComparer;
				keyComparer = equalityComparer2;
			}
			this._keyComparer = keyComparer;
			this.Reset();
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000A6FAC File Offset: 0x000A51AC
		protected NameObjectCollectionBase(int capacity, IEqualityComparer equalityComparer) : this(equalityComparer)
		{
			this.Reset(capacity);
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x000A6FBC File Offset: 0x000A51BC
		[Obsolete("Please use NameObjectCollectionBase(IEqualityComparer) instead.")]
		protected NameObjectCollectionBase(IHashCodeProvider hashProvider, IComparer comparer)
		{
			this._keyComparer = new CompatibleComparer(comparer, hashProvider);
			this.Reset();
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000A6FD7 File Offset: 0x000A51D7
		[Obsolete("Please use NameObjectCollectionBase(Int32, IEqualityComparer) instead.")]
		protected NameObjectCollectionBase(int capacity, IHashCodeProvider hashProvider, IComparer comparer)
		{
			this._keyComparer = new CompatibleComparer(comparer, hashProvider);
			this.Reset(capacity);
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000A6FF3 File Offset: 0x000A51F3
		protected NameObjectCollectionBase(int capacity)
		{
			this._keyComparer = StringComparer.InvariantCultureIgnoreCase;
			this.Reset(capacity);
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000A700D File Offset: 0x000A520D
		internal NameObjectCollectionBase(DBNull dummy)
		{
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000A7015 File Offset: 0x000A5215
		protected NameObjectCollectionBase(SerializationInfo info, StreamingContext context)
		{
			this._serializationInfo = info;
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000A7024 File Offset: 0x000A5224
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ReadOnly", this._readOnly);
			if (this._keyComparer == NameObjectCollectionBase.defaultComparer)
			{
				info.AddValue("HashProvider", CompatibleComparer.DefaultHashCodeProvider, typeof(IHashCodeProvider));
				info.AddValue("Comparer", CompatibleComparer.DefaultComparer, typeof(IComparer));
			}
			else if (this._keyComparer == null)
			{
				info.AddValue("HashProvider", null, typeof(IHashCodeProvider));
				info.AddValue("Comparer", null, typeof(IComparer));
			}
			else if (this._keyComparer is CompatibleComparer)
			{
				CompatibleComparer compatibleComparer = (CompatibleComparer)this._keyComparer;
				info.AddValue("HashProvider", compatibleComparer.HashCodeProvider, typeof(IHashCodeProvider));
				info.AddValue("Comparer", compatibleComparer.Comparer, typeof(IComparer));
			}
			else
			{
				info.AddValue("KeyComparer", this._keyComparer, typeof(IEqualityComparer));
			}
			int count = this._entriesArray.Count;
			info.AddValue("Count", count);
			string[] array = new string[count];
			object[] array2 = new object[count];
			for (int i = 0; i < count; i++)
			{
				NameObjectCollectionBase.NameObjectEntry nameObjectEntry = (NameObjectCollectionBase.NameObjectEntry)this._entriesArray[i];
				array[i] = nameObjectEntry.Key;
				array2[i] = nameObjectEntry.Value;
			}
			info.AddValue("Keys", array, typeof(string[]));
			info.AddValue("Values", array2, typeof(object[]));
			info.AddValue("Version", this._version);
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000A71D8 File Offset: 0x000A53D8
		public virtual void OnDeserialization(object sender)
		{
			if (this._keyComparer != null)
			{
				return;
			}
			if (this._serializationInfo == null)
			{
				throw new SerializationException();
			}
			SerializationInfo serializationInfo = this._serializationInfo;
			this._serializationInfo = null;
			bool readOnly = false;
			int num = 0;
			string[] array = null;
			object[] array2 = null;
			IHashCodeProvider hashCodeProvider = null;
			IComparer comparer = null;
			bool flag = false;
			int version = 0;
			SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num2 <= 1573770551U)
				{
					if (num2 <= 1202781175U)
					{
						if (num2 != 891156946U)
						{
							if (num2 == 1202781175U)
							{
								if (name == "ReadOnly")
								{
									readOnly = serializationInfo.GetBoolean("ReadOnly");
								}
							}
						}
						else if (name == "Comparer")
						{
							comparer = (IComparer)serializationInfo.GetValue("Comparer", typeof(IComparer));
						}
					}
					else if (num2 != 1228509323U)
					{
						if (num2 == 1573770551U)
						{
							if (name == "Version")
							{
								flag = true;
								version = serializationInfo.GetInt32("Version");
							}
						}
					}
					else if (name == "KeyComparer")
					{
						this._keyComparer = (IEqualityComparer)serializationInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
					}
				}
				else if (num2 <= 1944240600U)
				{
					if (num2 != 1613443821U)
					{
						if (num2 == 1944240600U)
						{
							if (name == "HashProvider")
							{
								hashCodeProvider = (IHashCodeProvider)serializationInfo.GetValue("HashProvider", typeof(IHashCodeProvider));
							}
						}
					}
					else if (name == "Keys")
					{
						array = (string[])serializationInfo.GetValue("Keys", typeof(string[]));
					}
				}
				else if (num2 != 2370642523U)
				{
					if (num2 == 3790059668U)
					{
						if (name == "Count")
						{
							num = serializationInfo.GetInt32("Count");
						}
					}
				}
				else if (name == "Values")
				{
					array2 = (object[])serializationInfo.GetValue("Values", typeof(object[]));
				}
			}
			if (this._keyComparer == null)
			{
				if (comparer == null || hashCodeProvider == null)
				{
					throw new SerializationException();
				}
				this._keyComparer = new CompatibleComparer(comparer, hashCodeProvider);
			}
			if (array == null || array2 == null)
			{
				throw new SerializationException();
			}
			this.Reset(num);
			for (int i = 0; i < num; i++)
			{
				this.BaseAdd(array[i], array2[i]);
			}
			this._readOnly = readOnly;
			if (flag)
			{
				this._version = version;
			}
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x000A74B6 File Offset: 0x000A56B6
		private void Reset()
		{
			this._entriesArray = new ArrayList();
			this._entriesTable = new Hashtable(this._keyComparer);
			this._nullKeyEntry = null;
			this._version++;
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x000A74ED File Offset: 0x000A56ED
		private void Reset(int capacity)
		{
			this._entriesArray = new ArrayList(capacity);
			this._entriesTable = new Hashtable(capacity, this._keyComparer);
			this._nullKeyEntry = null;
			this._version++;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000A7526 File Offset: 0x000A5726
		private NameObjectCollectionBase.NameObjectEntry FindEntry(string key)
		{
			if (key != null)
			{
				return (NameObjectCollectionBase.NameObjectEntry)this._entriesTable[key];
			}
			return this._nullKeyEntry;
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x000A7547 File Offset: 0x000A5747
		// (set) Token: 0x06002341 RID: 9025 RVA: 0x000A754F File Offset: 0x000A574F
		internal IEqualityComparer Comparer
		{
			get
			{
				return this._keyComparer;
			}
			set
			{
				this._keyComparer = value;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x000A7558 File Offset: 0x000A5758
		// (set) Token: 0x06002343 RID: 9027 RVA: 0x000A7560 File Offset: 0x000A5760
		protected bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
			set
			{
				this._readOnly = value;
			}
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x000A7569 File Offset: 0x000A5769
		protected bool BaseHasKeys()
		{
			return this._entriesTable.Count > 0;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x000A757C File Offset: 0x000A577C
		protected void BaseAdd(string name, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = new NameObjectCollectionBase.NameObjectEntry(name, value);
			if (name != null)
			{
				if (this._entriesTable[name] == null)
				{
					this._entriesTable.Add(name, nameObjectEntry);
				}
			}
			else if (this._nullKeyEntry == null)
			{
				this._nullKeyEntry = nameObjectEntry;
			}
			this._entriesArray.Add(nameObjectEntry);
			this._version++;
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x000A75FC File Offset: 0x000A57FC
		protected void BaseRemove(string name)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			if (name != null)
			{
				this._entriesTable.Remove(name);
				for (int i = this._entriesArray.Count - 1; i >= 0; i--)
				{
					if (this._keyComparer.Equals(name, this.BaseGetKey(i)))
					{
						this._entriesArray.RemoveAt(i);
					}
				}
			}
			else
			{
				this._nullKeyEntry = null;
				for (int j = this._entriesArray.Count - 1; j >= 0; j--)
				{
					if (this.BaseGetKey(j) == null)
					{
						this._entriesArray.RemoveAt(j);
					}
				}
			}
			this._version++;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000A76B4 File Offset: 0x000A58B4
		protected void BaseRemoveAt(int index)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			string text = this.BaseGetKey(index);
			if (text != null)
			{
				this._entriesTable.Remove(text);
			}
			else
			{
				this._nullKeyEntry = null;
			}
			this._entriesArray.RemoveAt(index);
			this._version++;
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x000A7717 File Offset: 0x000A5917
		protected void BaseClear()
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			this.Reset();
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000A7738 File Offset: 0x000A5938
		protected object BaseGet(string name)
		{
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = this.FindEntry(name);
			if (nameObjectEntry == null)
			{
				return null;
			}
			return nameObjectEntry.Value;
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x000A7758 File Offset: 0x000A5958
		protected void BaseSet(string name, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = this.FindEntry(name);
			if (nameObjectEntry != null)
			{
				nameObjectEntry.Value = value;
				this._version++;
				return;
			}
			this.BaseAdd(name, value);
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000A77A8 File Offset: 0x000A59A8
		protected object BaseGet(int index)
		{
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = (NameObjectCollectionBase.NameObjectEntry)this._entriesArray[index];
			return nameObjectEntry.Value;
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x000A77D0 File Offset: 0x000A59D0
		protected string BaseGetKey(int index)
		{
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = (NameObjectCollectionBase.NameObjectEntry)this._entriesArray[index];
			return nameObjectEntry.Key;
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x000A77F8 File Offset: 0x000A59F8
		protected void BaseSet(int index, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			NameObjectCollectionBase.NameObjectEntry nameObjectEntry = (NameObjectCollectionBase.NameObjectEntry)this._entriesArray[index];
			nameObjectEntry.Value = value;
			this._version++;
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x000A7844 File Offset: 0x000A5A44
		public virtual IEnumerator GetEnumerator()
		{
			return new NameObjectCollectionBase.NameObjectKeysEnumerator(this);
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x000A784C File Offset: 0x000A5A4C
		public virtual int Count
		{
			get
			{
				return this._entriesArray.Count;
			}
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000A785C File Offset: 0x000A5A5C
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.GetString("Arg_MultiRank"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("IndexOutOfRange", new object[]
				{
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (array.Length - index < this._entriesArray.Count)
			{
				throw new ArgumentException(SR.GetString("Arg_InsufficientSpace"));
			}
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x000A7906 File Offset: 0x000A5B06
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x000A7928 File Offset: 0x000A5B28
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000A792C File Offset: 0x000A5B2C
		protected string[] BaseGetAllKeys()
		{
			int count = this._entriesArray.Count;
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.BaseGetKey(i);
			}
			return array;
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000A7964 File Offset: 0x000A5B64
		protected object[] BaseGetAllValues()
		{
			int count = this._entriesArray.Count;
			object[] array = new object[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.BaseGet(i);
			}
			return array;
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000A799C File Offset: 0x000A5B9C
		protected object[] BaseGetAllValues(Type type)
		{
			int count = this._entriesArray.Count;
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object[] array = (object[])SecurityUtils.ArrayCreateInstance(type, count);
			for (int i = 0; i < count; i++)
			{
				array[i] = this.BaseGet(i);
			}
			return array;
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002356 RID: 9046 RVA: 0x000A79ED File Offset: 0x000A5BED
		public virtual NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				if (this._keys == null)
				{
					this._keys = new NameObjectCollectionBase.KeysCollection(this);
				}
				return this._keys;
			}
		}

		// Token: 0x04001FC7 RID: 8135
		private const string ReadOnlyName = "ReadOnly";

		// Token: 0x04001FC8 RID: 8136
		private const string CountName = "Count";

		// Token: 0x04001FC9 RID: 8137
		private const string ComparerName = "Comparer";

		// Token: 0x04001FCA RID: 8138
		private const string HashCodeProviderName = "HashProvider";

		// Token: 0x04001FCB RID: 8139
		private const string KeysName = "Keys";

		// Token: 0x04001FCC RID: 8140
		private const string ValuesName = "Values";

		// Token: 0x04001FCD RID: 8141
		private const string KeyComparerName = "KeyComparer";

		// Token: 0x04001FCE RID: 8142
		private const string VersionName = "Version";

		// Token: 0x04001FCF RID: 8143
		private bool _readOnly;

		// Token: 0x04001FD0 RID: 8144
		private ArrayList _entriesArray;

		// Token: 0x04001FD1 RID: 8145
		private IEqualityComparer _keyComparer;

		// Token: 0x04001FD2 RID: 8146
		private volatile Hashtable _entriesTable;

		// Token: 0x04001FD3 RID: 8147
		private volatile NameObjectCollectionBase.NameObjectEntry _nullKeyEntry;

		// Token: 0x04001FD4 RID: 8148
		private NameObjectCollectionBase.KeysCollection _keys;

		// Token: 0x04001FD5 RID: 8149
		private SerializationInfo _serializationInfo;

		// Token: 0x04001FD6 RID: 8150
		private int _version;

		// Token: 0x04001FD7 RID: 8151
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04001FD8 RID: 8152
		private static StringComparer defaultComparer = StringComparer.InvariantCultureIgnoreCase;

		// Token: 0x020007EB RID: 2027
		internal class NameObjectEntry
		{
			// Token: 0x060043FC RID: 17404 RVA: 0x0011E02B File Offset: 0x0011C22B
			internal NameObjectEntry(string name, object value)
			{
				this.Key = name;
				this.Value = value;
			}

			// Token: 0x04003505 RID: 13573
			internal string Key;

			// Token: 0x04003506 RID: 13574
			internal object Value;
		}

		// Token: 0x020007EC RID: 2028
		[Serializable]
		internal class NameObjectKeysEnumerator : IEnumerator
		{
			// Token: 0x060043FD RID: 17405 RVA: 0x0011E041 File Offset: 0x0011C241
			internal NameObjectKeysEnumerator(NameObjectCollectionBase coll)
			{
				this._coll = coll;
				this._version = this._coll._version;
				this._pos = -1;
			}

			// Token: 0x060043FE RID: 17406 RVA: 0x0011E068 File Offset: 0x0011C268
			public bool MoveNext()
			{
				if (this._version != this._coll._version)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
				}
				if (this._pos < this._coll.Count - 1)
				{
					this._pos++;
					return true;
				}
				this._pos = this._coll.Count;
				return false;
			}

			// Token: 0x060043FF RID: 17407 RVA: 0x0011E0CF File Offset: 0x0011C2CF
			public void Reset()
			{
				if (this._version != this._coll._version)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
				}
				this._pos = -1;
			}

			// Token: 0x17000F64 RID: 3940
			// (get) Token: 0x06004400 RID: 17408 RVA: 0x0011E0FB File Offset: 0x0011C2FB
			public object Current
			{
				get
				{
					if (this._pos >= 0 && this._pos < this._coll.Count)
					{
						return this._coll.BaseGetKey(this._pos);
					}
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumOpCantHappen"));
				}
			}

			// Token: 0x04003507 RID: 13575
			private int _pos;

			// Token: 0x04003508 RID: 13576
			private NameObjectCollectionBase _coll;

			// Token: 0x04003509 RID: 13577
			private int _version;
		}

		// Token: 0x020007ED RID: 2029
		[Serializable]
		public class KeysCollection : ICollection, IEnumerable
		{
			// Token: 0x06004401 RID: 17409 RVA: 0x0011E13A File Offset: 0x0011C33A
			internal KeysCollection(NameObjectCollectionBase coll)
			{
				this._coll = coll;
			}

			// Token: 0x06004402 RID: 17410 RVA: 0x0011E149 File Offset: 0x0011C349
			public virtual string Get(int index)
			{
				return this._coll.BaseGetKey(index);
			}

			// Token: 0x17000F65 RID: 3941
			public string this[int index]
			{
				get
				{
					return this.Get(index);
				}
			}

			// Token: 0x06004404 RID: 17412 RVA: 0x0011E160 File Offset: 0x0011C360
			public IEnumerator GetEnumerator()
			{
				return new NameObjectCollectionBase.NameObjectKeysEnumerator(this._coll);
			}

			// Token: 0x17000F66 RID: 3942
			// (get) Token: 0x06004405 RID: 17413 RVA: 0x0011E16D File Offset: 0x0011C36D
			public int Count
			{
				get
				{
					return this._coll.Count;
				}
			}

			// Token: 0x06004406 RID: 17414 RVA: 0x0011E17C File Offset: 0x0011C37C
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException(SR.GetString("Arg_MultiRank"));
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("IndexOutOfRange", new object[]
					{
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (array.Length - index < this._coll.Count)
				{
					throw new ArgumentException(SR.GetString("Arg_InsufficientSpace"));
				}
				foreach (object value in this)
				{
					array.SetValue(value, index++);
				}
			}

			// Token: 0x17000F67 RID: 3943
			// (get) Token: 0x06004407 RID: 17415 RVA: 0x0011E226 File Offset: 0x0011C426
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._coll).SyncRoot;
				}
			}

			// Token: 0x17000F68 RID: 3944
			// (get) Token: 0x06004408 RID: 17416 RVA: 0x0011E233 File Offset: 0x0011C433
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0400350A RID: 13578
			private NameObjectCollectionBase _coll;
		}
	}
}
