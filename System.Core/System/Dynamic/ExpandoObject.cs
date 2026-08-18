using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace System.Dynamic
{
	// Token: 0x020000C7 RID: 199
	[__DynamicallyInvokable]
	public sealed class ExpandoObject : IDynamicMetaObjectProvider, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, INotifyPropertyChanged
	{
		// Token: 0x060005D6 RID: 1494 RVA: 0x00011D2C File Offset: 0x0000FF2C
		[__DynamicallyInvokable]
		public ExpandoObject()
		{
			this._data = ExpandoObject.ExpandoData.Empty;
			this.LockObject = new object();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00011D4C File Offset: 0x0000FF4C
		internal bool TryGetValue(object indexClass, int index, string name, bool ignoreCase, out object value)
		{
			ExpandoObject.ExpandoData data = this._data;
			if (data.Class != indexClass || ignoreCase)
			{
				index = data.Class.GetValueIndex(name, ignoreCase, this);
				if (index == -2)
				{
					throw Error.AmbiguousMatchInExpandoObject(name);
				}
			}
			if (index == -1)
			{
				value = null;
				return false;
			}
			object obj = data[index];
			if (obj == ExpandoObject.Uninitialized)
			{
				value = null;
				return false;
			}
			value = obj;
			return true;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00011DB4 File Offset: 0x0000FFB4
		internal void TrySetValue(object indexClass, int index, object value, string name, bool ignoreCase, bool add)
		{
			object lockObject = this.LockObject;
			ExpandoObject.ExpandoData expandoData;
			object obj;
			lock (lockObject)
			{
				expandoData = this._data;
				if (expandoData.Class != indexClass || ignoreCase)
				{
					index = expandoData.Class.GetValueIndex(name, ignoreCase, this);
					if (index == -2)
					{
						throw Error.AmbiguousMatchInExpandoObject(name);
					}
					if (index == -1)
					{
						int num = ignoreCase ? expandoData.Class.GetValueIndexCaseSensitive(name) : index;
						if (num != -1)
						{
							index = num;
						}
						else
						{
							ExpandoClass newClass = expandoData.Class.FindNewClass(name);
							expandoData = this.PromoteClassCore(expandoData.Class, newClass);
							index = expandoData.Class.GetValueIndexCaseSensitive(name);
						}
					}
				}
				obj = expandoData[index];
				if (obj == ExpandoObject.Uninitialized)
				{
					this._count++;
				}
				else if (add)
				{
					throw Error.SameKeyExistsInExpando(name);
				}
				expandoData[index] = value;
			}
			PropertyChangedEventHandler propertyChanged = this._propertyChanged;
			if (propertyChanged != null && value != obj)
			{
				propertyChanged(this, new PropertyChangedEventArgs(expandoData.Class.Keys[index]));
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00011ED4 File Offset: 0x000100D4
		internal bool TryDeleteValue(object indexClass, int index, string name, bool ignoreCase, object deleteValue)
		{
			object lockObject = this.LockObject;
			ExpandoObject.ExpandoData data;
			lock (lockObject)
			{
				data = this._data;
				if (data.Class != indexClass || ignoreCase)
				{
					index = data.Class.GetValueIndex(name, ignoreCase, this);
					if (index == -2)
					{
						throw Error.AmbiguousMatchInExpandoObject(name);
					}
				}
				if (index == -1)
				{
					return false;
				}
				object obj = data[index];
				if (obj == ExpandoObject.Uninitialized)
				{
					return false;
				}
				if (deleteValue != ExpandoObject.Uninitialized && !object.Equals(obj, deleteValue))
				{
					return false;
				}
				data[index] = ExpandoObject.Uninitialized;
				this._count--;
			}
			PropertyChangedEventHandler propertyChanged = this._propertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(data.Class.Keys[index]));
			}
			return true;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00011FC0 File Offset: 0x000101C0
		internal bool IsDeletedMember(int index)
		{
			return index != this._data.Length && this._data[index] == ExpandoObject.Uninitialized;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00011FE5 File Offset: 0x000101E5
		internal ExpandoClass Class
		{
			get
			{
				return this._data.Class;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00011FF4 File Offset: 0x000101F4
		private ExpandoObject.ExpandoData PromoteClassCore(ExpandoClass oldClass, ExpandoClass newClass)
		{
			object lockObject = this.LockObject;
			ExpandoObject.ExpandoData data;
			lock (lockObject)
			{
				if (this._data.Class == oldClass)
				{
					this._data = this._data.UpdateClass(newClass);
				}
				data = this._data;
			}
			return data;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00012058 File Offset: 0x00010258
		internal void PromoteClass(object oldClass, object newClass)
		{
			this.PromoteClassCore((ExpandoClass)oldClass, (ExpandoClass)newClass);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001206D File Offset: 0x0001026D
		[__DynamicallyInvokable]
		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return new ExpandoObject.MetaExpando(parameter, this);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00012076 File Offset: 0x00010276
		private void TryAddMember(string key, object value)
		{
			ContractUtils.RequiresNotNull(key, "key");
			this.TrySetValue(null, -1, value, key, false, true);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001208F File Offset: 0x0001028F
		private bool TryGetValueForKey(string key, out object value)
		{
			return this.TryGetValue(null, -1, key, false, out value);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001209C File Offset: 0x0001029C
		private bool ExpandoContainsKey(string key)
		{
			return this._data.Class.GetValueIndexCaseSensitive(key) >= 0;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x000120B5 File Offset: 0x000102B5
		[__DynamicallyInvokable]
		ICollection<string> IDictionary<string, object>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return new ExpandoObject.KeyCollection(this);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x000120BD File Offset: 0x000102BD
		[__DynamicallyInvokable]
		ICollection<object> IDictionary<string, object>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return new ExpandoObject.ValueCollection(this);
			}
		}

		// Token: 0x17000147 RID: 327
		[__DynamicallyInvokable]
		object IDictionary<string, object>.this[string key]
		{
			[__DynamicallyInvokable]
			get
			{
				object result;
				if (!this.TryGetValueForKey(key, out result))
				{
					throw Error.KeyDoesNotExistInExpando(key);
				}
				return result;
			}
			[__DynamicallyInvokable]
			set
			{
				ContractUtils.RequiresNotNull(key, "key");
				this.TrySetValue(null, -1, value, key, false, false);
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00012101 File Offset: 0x00010301
		[__DynamicallyInvokable]
		void IDictionary<string, object>.Add(string key, object value)
		{
			this.TryAddMember(key, value);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001210C File Offset: 0x0001030C
		[__DynamicallyInvokable]
		bool IDictionary<string, object>.ContainsKey(string key)
		{
			ContractUtils.RequiresNotNull(key, "key");
			ExpandoObject.ExpandoData data = this._data;
			int valueIndexCaseSensitive = data.Class.GetValueIndexCaseSensitive(key);
			return valueIndexCaseSensitive >= 0 && data[valueIndexCaseSensitive] != ExpandoObject.Uninitialized;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001214F File Offset: 0x0001034F
		[__DynamicallyInvokable]
		bool IDictionary<string, object>.Remove(string key)
		{
			ContractUtils.RequiresNotNull(key, "key");
			return this.TryDeleteValue(null, -1, key, false, ExpandoObject.Uninitialized);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001216B File Offset: 0x0001036B
		[__DynamicallyInvokable]
		bool IDictionary<string, object>.TryGetValue(string key, out object value)
		{
			return this.TryGetValueForKey(key, out value);
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00012175 File Offset: 0x00010375
		[__DynamicallyInvokable]
		int ICollection<KeyValuePair<string, object>>.Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0001217D File Offset: 0x0001037D
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<string, object>>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00012180 File Offset: 0x00010380
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
		{
			this.TryAddMember(item.Key, item.Value);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00012198 File Offset: 0x00010398
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<string, object>>.Clear()
		{
			object lockObject = this.LockObject;
			ExpandoObject.ExpandoData data;
			lock (lockObject)
			{
				data = this._data;
				this._data = ExpandoObject.ExpandoData.Empty;
				this._count = 0;
			}
			PropertyChangedEventHandler propertyChanged = this._propertyChanged;
			if (propertyChanged != null)
			{
				int i = 0;
				int num = data.Class.Keys.Length;
				while (i < num)
				{
					if (data[i] != ExpandoObject.Uninitialized)
					{
						propertyChanged(this, new PropertyChangedEventArgs(data.Class.Keys[i]));
					}
					i++;
				}
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00012240 File Offset: 0x00010440
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
		{
			object objA;
			return this.TryGetValueForKey(item.Key, out objA) && object.Equals(objA, item.Value);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00012270 File Offset: 0x00010470
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			ContractUtils.RequiresNotNull(array, "array");
			ContractUtils.RequiresArrayRange<KeyValuePair<string, object>>(array, arrayIndex, this._count, "arrayIndex", "Count");
			object lockObject = this.LockObject;
			lock (lockObject)
			{
				foreach (KeyValuePair<string, object> keyValuePair in ((IEnumerable<KeyValuePair<string, object>>)this))
				{
					array[arrayIndex++] = keyValuePair;
				}
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00012308 File Offset: 0x00010508
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
		{
			return this.TryDeleteValue(null, -1, item.Key, false, item.Value);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00012324 File Offset: 0x00010524
		[__DynamicallyInvokable]
		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			ExpandoObject.ExpandoData data = this._data;
			return this.GetExpandoEnumerator(data, data.Version);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00012348 File Offset: 0x00010548
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			ExpandoObject.ExpandoData data = this._data;
			return this.GetExpandoEnumerator(data, data.Version);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00012369 File Offset: 0x00010569
		private IEnumerator<KeyValuePair<string, object>> GetExpandoEnumerator(ExpandoObject.ExpandoData data, int version)
		{
			int num;
			for (int i = 0; i < data.Class.Keys.Length; i = num + 1)
			{
				if (this._data.Version != version || data != this._data)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
				object obj = data[i];
				if (obj != ExpandoObject.Uninitialized)
				{
					yield return new KeyValuePair<string, object>(data.Class.Keys[i], obj);
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060005F4 RID: 1524 RVA: 0x00012386 File Offset: 0x00010586
		// (remove) Token: 0x060005F5 RID: 1525 RVA: 0x0001239F File Offset: 0x0001059F
		[__DynamicallyInvokable]
		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			[__DynamicallyInvokable]
			add
			{
				this._propertyChanged = (PropertyChangedEventHandler)Delegate.Combine(this._propertyChanged, value);
			}
			[__DynamicallyInvokable]
			remove
			{
				this._propertyChanged = (PropertyChangedEventHandler)Delegate.Remove(this._propertyChanged, value);
			}
		}

		// Token: 0x040005AD RID: 1453
		internal readonly object LockObject;

		// Token: 0x040005AE RID: 1454
		private ExpandoObject.ExpandoData _data;

		// Token: 0x040005AF RID: 1455
		private int _count;

		// Token: 0x040005B0 RID: 1456
		internal static readonly object Uninitialized = new object();

		// Token: 0x040005B1 RID: 1457
		internal const int AmbiguousMatchFound = -2;

		// Token: 0x040005B2 RID: 1458
		internal const int NoMatch = -1;

		// Token: 0x040005B3 RID: 1459
		private PropertyChangedEventHandler _propertyChanged;

		// Token: 0x02000314 RID: 788
		private sealed class KeyCollectionDebugView
		{
			// Token: 0x06001AB7 RID: 6839 RVA: 0x000621C1 File Offset: 0x000603C1
			public KeyCollectionDebugView(ICollection<string> collection)
			{
				this.collection = collection;
			}

			// Token: 0x170004F3 RID: 1267
			// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x000621D0 File Offset: 0x000603D0
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public string[] Items
			{
				get
				{
					string[] array = new string[this.collection.Count];
					this.collection.CopyTo(array, 0);
					return array;
				}
			}

			// Token: 0x04000E3C RID: 3644
			private ICollection<string> collection;
		}

		// Token: 0x02000315 RID: 789
		[DebuggerTypeProxy(typeof(ExpandoObject.KeyCollectionDebugView))]
		[DebuggerDisplay("Count = {Count}")]
		private class KeyCollection : ICollection<string>, IEnumerable<string>, IEnumerable
		{
			// Token: 0x06001AB9 RID: 6841 RVA: 0x000621FC File Offset: 0x000603FC
			internal KeyCollection(ExpandoObject expando)
			{
				object lockObject = expando.LockObject;
				lock (lockObject)
				{
					this._expando = expando;
					this._expandoVersion = expando._data.Version;
					this._expandoCount = expando._count;
					this._expandoData = expando._data;
				}
			}

			// Token: 0x06001ABA RID: 6842 RVA: 0x0006226C File Offset: 0x0006046C
			private void CheckVersion()
			{
				if (this._expando._data.Version != this._expandoVersion || this._expandoData != this._expando._data)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
			}

			// Token: 0x06001ABB RID: 6843 RVA: 0x0006229F File Offset: 0x0006049F
			public void Add(string item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06001ABC RID: 6844 RVA: 0x000622A6 File Offset: 0x000604A6
			public void Clear()
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06001ABD RID: 6845 RVA: 0x000622B0 File Offset: 0x000604B0
			public bool Contains(string item)
			{
				object lockObject = this._expando.LockObject;
				bool result;
				lock (lockObject)
				{
					this.CheckVersion();
					result = this._expando.ExpandoContainsKey(item);
				}
				return result;
			}

			// Token: 0x06001ABE RID: 6846 RVA: 0x00062304 File Offset: 0x00060504
			public void CopyTo(string[] array, int arrayIndex)
			{
				ContractUtils.RequiresNotNull(array, "array");
				ContractUtils.RequiresArrayRange<string>(array, arrayIndex, this._expandoCount, "arrayIndex", "Count");
				object lockObject = this._expando.LockObject;
				lock (lockObject)
				{
					this.CheckVersion();
					ExpandoObject.ExpandoData data = this._expando._data;
					for (int i = 0; i < data.Class.Keys.Length; i++)
					{
						if (data[i] != ExpandoObject.Uninitialized)
						{
							array[arrayIndex++] = data.Class.Keys[i];
						}
					}
				}
			}

			// Token: 0x170004F4 RID: 1268
			// (get) Token: 0x06001ABF RID: 6847 RVA: 0x000623B4 File Offset: 0x000605B4
			public int Count
			{
				get
				{
					this.CheckVersion();
					return this._expandoCount;
				}
			}

			// Token: 0x170004F5 RID: 1269
			// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x000623C2 File Offset: 0x000605C2
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001AC1 RID: 6849 RVA: 0x000623C5 File Offset: 0x000605C5
			public bool Remove(string item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06001AC2 RID: 6850 RVA: 0x000623CC File Offset: 0x000605CC
			public IEnumerator<string> GetEnumerator()
			{
				int i = 0;
				int j = this._expandoData.Class.Keys.Length;
				while (i < j)
				{
					this.CheckVersion();
					if (this._expandoData[i] != ExpandoObject.Uninitialized)
					{
						yield return this._expandoData.Class.Keys[i];
					}
					int num = i;
					i = num + 1;
				}
				yield break;
			}

			// Token: 0x06001AC3 RID: 6851 RVA: 0x000623DB File Offset: 0x000605DB
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000E3D RID: 3645
			private readonly ExpandoObject _expando;

			// Token: 0x04000E3E RID: 3646
			private readonly int _expandoVersion;

			// Token: 0x04000E3F RID: 3647
			private readonly int _expandoCount;

			// Token: 0x04000E40 RID: 3648
			private readonly ExpandoObject.ExpandoData _expandoData;
		}

		// Token: 0x02000316 RID: 790
		private sealed class ValueCollectionDebugView
		{
			// Token: 0x06001AC4 RID: 6852 RVA: 0x000623E3 File Offset: 0x000605E3
			public ValueCollectionDebugView(ICollection<object> collection)
			{
				this.collection = collection;
			}

			// Token: 0x170004F6 RID: 1270
			// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x000623F4 File Offset: 0x000605F4
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					object[] array = new object[this.collection.Count];
					this.collection.CopyTo(array, 0);
					return array;
				}
			}

			// Token: 0x04000E41 RID: 3649
			private ICollection<object> collection;
		}

		// Token: 0x02000317 RID: 791
		[DebuggerTypeProxy(typeof(ExpandoObject.ValueCollectionDebugView))]
		[DebuggerDisplay("Count = {Count}")]
		private class ValueCollection : ICollection<object>, IEnumerable<object>, IEnumerable
		{
			// Token: 0x06001AC6 RID: 6854 RVA: 0x00062420 File Offset: 0x00060620
			internal ValueCollection(ExpandoObject expando)
			{
				object lockObject = expando.LockObject;
				lock (lockObject)
				{
					this._expando = expando;
					this._expandoVersion = expando._data.Version;
					this._expandoCount = expando._count;
					this._expandoData = expando._data;
				}
			}

			// Token: 0x06001AC7 RID: 6855 RVA: 0x00062490 File Offset: 0x00060690
			private void CheckVersion()
			{
				if (this._expando._data.Version != this._expandoVersion || this._expandoData != this._expando._data)
				{
					throw Error.CollectionModifiedWhileEnumerating();
				}
			}

			// Token: 0x06001AC8 RID: 6856 RVA: 0x000624C3 File Offset: 0x000606C3
			public void Add(object item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06001AC9 RID: 6857 RVA: 0x000624CA File Offset: 0x000606CA
			public void Clear()
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06001ACA RID: 6858 RVA: 0x000624D4 File Offset: 0x000606D4
			public bool Contains(object item)
			{
				object lockObject = this._expando.LockObject;
				bool result;
				lock (lockObject)
				{
					this.CheckVersion();
					ExpandoObject.ExpandoData data = this._expando._data;
					for (int i = 0; i < data.Class.Keys.Length; i++)
					{
						if (object.Equals(data[i], item))
						{
							return true;
						}
					}
					result = false;
				}
				return result;
			}

			// Token: 0x06001ACB RID: 6859 RVA: 0x00062558 File Offset: 0x00060758
			public void CopyTo(object[] array, int arrayIndex)
			{
				ContractUtils.RequiresNotNull(array, "array");
				ContractUtils.RequiresArrayRange<object>(array, arrayIndex, this._expandoCount, "arrayIndex", "Count");
				object lockObject = this._expando.LockObject;
				lock (lockObject)
				{
					this.CheckVersion();
					ExpandoObject.ExpandoData data = this._expando._data;
					for (int i = 0; i < data.Class.Keys.Length; i++)
					{
						if (data[i] != ExpandoObject.Uninitialized)
						{
							array[arrayIndex++] = data[i];
						}
					}
				}
			}

			// Token: 0x170004F7 RID: 1271
			// (get) Token: 0x06001ACC RID: 6860 RVA: 0x00062600 File Offset: 0x00060800
			public int Count
			{
				get
				{
					this.CheckVersion();
					return this._expandoCount;
				}
			}

			// Token: 0x170004F8 RID: 1272
			// (get) Token: 0x06001ACD RID: 6861 RVA: 0x0006260E File Offset: 0x0006080E
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001ACE RID: 6862 RVA: 0x00062611 File Offset: 0x00060811
			public bool Remove(object item)
			{
				throw Error.CollectionReadOnly();
			}

			// Token: 0x06001ACF RID: 6863 RVA: 0x00062618 File Offset: 0x00060818
			public IEnumerator<object> GetEnumerator()
			{
				ExpandoObject.ExpandoData data = this._expando._data;
				int num;
				for (int i = 0; i < data.Class.Keys.Length; i = num + 1)
				{
					this.CheckVersion();
					object obj = data[i];
					if (obj != ExpandoObject.Uninitialized)
					{
						yield return obj;
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x06001AD0 RID: 6864 RVA: 0x00062627 File Offset: 0x00060827
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04000E42 RID: 3650
			private readonly ExpandoObject _expando;

			// Token: 0x04000E43 RID: 3651
			private readonly int _expandoVersion;

			// Token: 0x04000E44 RID: 3652
			private readonly int _expandoCount;

			// Token: 0x04000E45 RID: 3653
			private readonly ExpandoObject.ExpandoData _expandoData;
		}

		// Token: 0x02000318 RID: 792
		private class MetaExpando : DynamicMetaObject
		{
			// Token: 0x06001AD1 RID: 6865 RVA: 0x0006262F File Offset: 0x0006082F
			public MetaExpando(Expression expression, ExpandoObject value) : base(expression, BindingRestrictions.Empty, value)
			{
			}

			// Token: 0x06001AD2 RID: 6866 RVA: 0x00062640 File Offset: 0x00060840
			private DynamicMetaObject BindGetOrInvokeMember(DynamicMetaObjectBinder binder, string name, bool ignoreCase, DynamicMetaObject fallback, Func<DynamicMetaObject, DynamicMetaObject> fallbackInvoke)
			{
				ExpandoClass @class = this.Value.Class;
				int valueIndex = @class.GetValueIndex(name, ignoreCase, this.Value);
				ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "value");
				Expression test = Expression.Call(typeof(RuntimeOps).GetMethod("ExpandoTryGetValue"), new Expression[]
				{
					this.GetLimitedSelf(),
					Expression.Constant(@class, typeof(object)),
					Expression.Constant(valueIndex),
					Expression.Constant(name),
					Expression.Constant(ignoreCase),
					parameterExpression
				});
				DynamicMetaObject dynamicMetaObject = new DynamicMetaObject(parameterExpression, BindingRestrictions.Empty);
				if (fallbackInvoke != null)
				{
					dynamicMetaObject = fallbackInvoke(dynamicMetaObject);
				}
				dynamicMetaObject = new DynamicMetaObject(Expression.Block(new ParameterExpression[]
				{
					parameterExpression
				}, new Expression[]
				{
					Expression.Condition(test, dynamicMetaObject.Expression, fallback.Expression, typeof(object))
				}), dynamicMetaObject.Restrictions.Merge(fallback.Restrictions));
				return this.AddDynamicTestAndDefer(binder, this.Value.Class, null, dynamicMetaObject);
			}

			// Token: 0x06001AD3 RID: 6867 RVA: 0x00062763 File Offset: 0x00060963
			public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				return this.BindGetOrInvokeMember(binder, binder.Name, binder.IgnoreCase, binder.FallbackGetMember(this), null);
			}

			// Token: 0x06001AD4 RID: 6868 RVA: 0x0006278C File Offset: 0x0006098C
			public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				return this.BindGetOrInvokeMember(binder, binder.Name, binder.IgnoreCase, binder.FallbackInvokeMember(this, args), (DynamicMetaObject value) => binder.FallbackInvoke(value, args, null));
			}

			// Token: 0x06001AD5 RID: 6869 RVA: 0x00062800 File Offset: 0x00060A00
			public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				ContractUtils.RequiresNotNull(value, "value");
				ExpandoClass expandoClass;
				int num;
				ExpandoClass classEnsureIndex = this.GetClassEnsureIndex(binder.Name, binder.IgnoreCase, this.Value, out expandoClass, out num);
				return this.AddDynamicTestAndDefer(binder, expandoClass, classEnsureIndex, new DynamicMetaObject(Expression.Call(typeof(RuntimeOps).GetMethod("ExpandoTrySetValue"), new Expression[]
				{
					this.GetLimitedSelf(),
					Expression.Constant(expandoClass, typeof(object)),
					Expression.Constant(num),
					Expression.Convert(value.Expression, typeof(object)),
					Expression.Constant(binder.Name),
					Expression.Constant(binder.IgnoreCase)
				}), BindingRestrictions.Empty));
			}

			// Token: 0x06001AD6 RID: 6870 RVA: 0x000628D8 File Offset: 0x00060AD8
			public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
			{
				ContractUtils.RequiresNotNull(binder, "binder");
				int valueIndex = this.Value.Class.GetValueIndex(binder.Name, binder.IgnoreCase, this.Value);
				Expression expression = Expression.Call(typeof(RuntimeOps).GetMethod("ExpandoTryDeleteValue"), this.GetLimitedSelf(), Expression.Constant(this.Value.Class, typeof(object)), Expression.Constant(valueIndex), Expression.Constant(binder.Name), Expression.Constant(binder.IgnoreCase));
				DynamicMetaObject dynamicMetaObject = binder.FallbackDeleteMember(this);
				DynamicMetaObject succeeds = new DynamicMetaObject(Expression.IfThen(Expression.Not(expression), dynamicMetaObject.Expression), dynamicMetaObject.Restrictions);
				return this.AddDynamicTestAndDefer(binder, this.Value.Class, null, succeeds);
			}

			// Token: 0x06001AD7 RID: 6871 RVA: 0x000629AC File Offset: 0x00060BAC
			public override IEnumerable<string> GetDynamicMemberNames()
			{
				ExpandoObject.ExpandoData expandoData = this.Value._data;
				ExpandoClass klass = expandoData.Class;
				int num;
				for (int i = 0; i < klass.Keys.Length; i = num + 1)
				{
					object obj = expandoData[i];
					if (obj != ExpandoObject.Uninitialized)
					{
						yield return klass.Keys[i];
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x06001AD8 RID: 6872 RVA: 0x000629BC File Offset: 0x00060BBC
			private DynamicMetaObject AddDynamicTestAndDefer(DynamicMetaObjectBinder binder, ExpandoClass klass, ExpandoClass originalClass, DynamicMetaObject succeeds)
			{
				Expression expression = succeeds.Expression;
				if (originalClass != null)
				{
					expression = Expression.Block(Expression.Call(null, typeof(RuntimeOps).GetMethod("ExpandoPromoteClass"), this.GetLimitedSelf(), Expression.Constant(originalClass, typeof(object)), Expression.Constant(klass, typeof(object))), succeeds.Expression);
				}
				return new DynamicMetaObject(Expression.Condition(Expression.Call(null, typeof(RuntimeOps).GetMethod("ExpandoCheckVersion"), this.GetLimitedSelf(), Expression.Constant(originalClass ?? klass, typeof(object))), expression, binder.GetUpdateExpression(expression.Type)), this.GetRestrictions().Merge(succeeds.Restrictions));
			}

			// Token: 0x06001AD9 RID: 6873 RVA: 0x00062A80 File Offset: 0x00060C80
			private ExpandoClass GetClassEnsureIndex(string name, bool caseInsensitive, ExpandoObject obj, out ExpandoClass klass, out int index)
			{
				ExpandoClass @class = this.Value.Class;
				index = @class.GetValueIndex(name, caseInsensitive, obj);
				if (index == -2)
				{
					klass = @class;
					return null;
				}
				if (index == -1)
				{
					ExpandoClass expandoClass = @class.FindNewClass(name);
					klass = expandoClass;
					index = expandoClass.GetValueIndexCaseSensitive(name);
					return @class;
				}
				klass = @class;
				return null;
			}

			// Token: 0x06001ADA RID: 6874 RVA: 0x00062AD5 File Offset: 0x00060CD5
			private Expression GetLimitedSelf()
			{
				if (TypeUtils.AreEquivalent(base.Expression.Type, base.LimitType))
				{
					return base.Expression;
				}
				return Expression.Convert(base.Expression, base.LimitType);
			}

			// Token: 0x06001ADB RID: 6875 RVA: 0x00062B07 File Offset: 0x00060D07
			private BindingRestrictions GetRestrictions()
			{
				return BindingRestrictions.GetTypeRestriction(this);
			}

			// Token: 0x170004F9 RID: 1273
			// (get) Token: 0x06001ADC RID: 6876 RVA: 0x00062B0F File Offset: 0x00060D0F
			public new ExpandoObject Value
			{
				get
				{
					return (ExpandoObject)base.Value;
				}
			}
		}

		// Token: 0x02000319 RID: 793
		private class ExpandoData
		{
			// Token: 0x170004FA RID: 1274
			internal object this[int index]
			{
				get
				{
					return this._dataArray[index];
				}
				set
				{
					this._version++;
					this._dataArray[index] = value;
				}
			}

			// Token: 0x170004FB RID: 1275
			// (get) Token: 0x06001ADF RID: 6879 RVA: 0x00062B3F File Offset: 0x00060D3F
			internal int Version
			{
				get
				{
					return this._version;
				}
			}

			// Token: 0x170004FC RID: 1276
			// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x00062B47 File Offset: 0x00060D47
			internal int Length
			{
				get
				{
					return this._dataArray.Length;
				}
			}

			// Token: 0x06001AE1 RID: 6881 RVA: 0x00062B51 File Offset: 0x00060D51
			private ExpandoData()
			{
				this.Class = ExpandoClass.Empty;
				this._dataArray = new object[0];
			}

			// Token: 0x06001AE2 RID: 6882 RVA: 0x00062B70 File Offset: 0x00060D70
			internal ExpandoData(ExpandoClass klass, object[] data, int version)
			{
				this.Class = klass;
				this._dataArray = data;
				this._version = version;
			}

			// Token: 0x06001AE3 RID: 6883 RVA: 0x00062B90 File Offset: 0x00060D90
			internal ExpandoObject.ExpandoData UpdateClass(ExpandoClass newClass)
			{
				if (this._dataArray.Length >= newClass.Keys.Length)
				{
					this[newClass.Keys.Length - 1] = ExpandoObject.Uninitialized;
					return new ExpandoObject.ExpandoData(newClass, this._dataArray, this._version);
				}
				int index = this._dataArray.Length;
				object[] array = new object[ExpandoObject.ExpandoData.GetAlignedSize(newClass.Keys.Length)];
				Array.Copy(this._dataArray, array, this._dataArray.Length);
				ExpandoObject.ExpandoData expandoData = new ExpandoObject.ExpandoData(newClass, array, this._version);
				expandoData[index] = ExpandoObject.Uninitialized;
				return expandoData;
			}

			// Token: 0x06001AE4 RID: 6884 RVA: 0x00062C22 File Offset: 0x00060E22
			private static int GetAlignedSize(int len)
			{
				return len + 7 & -8;
			}

			// Token: 0x04000E46 RID: 3654
			internal static ExpandoObject.ExpandoData Empty = new ExpandoObject.ExpandoData();

			// Token: 0x04000E47 RID: 3655
			internal readonly ExpandoClass Class;

			// Token: 0x04000E48 RID: 3656
			private readonly object[] _dataArray;

			// Token: 0x04000E49 RID: 3657
			private int _version;
		}
	}
}
