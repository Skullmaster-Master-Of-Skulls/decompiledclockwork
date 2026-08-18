using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x020001EA RID: 490
	public class TempDataDictionary : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x06000EBF RID: 3775 RVA: 0x00026F02 File Offset: 0x00025102
		public TempDataDictionary()
		{
			this._data = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00026F3A File Offset: 0x0002513A
		public int Count
		{
			get
			{
				return this._data.Count;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00026F47 File Offset: 0x00025147
		public ICollection<string> Keys
		{
			get
			{
				return this._data.Keys;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x00026F54 File Offset: 0x00025154
		public ICollection<object> Values
		{
			get
			{
				return this._data.Values;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00026F61 File Offset: 0x00025161
		bool ICollection<KeyValuePair<string, object>>.IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<string, object>>)this._data).IsReadOnly;
			}
		}

		// Token: 0x1700033D RID: 829
		public object this[string key]
		{
			get
			{
				object result;
				if (this.TryGetValue(key, out result))
				{
					this._initialKeys.Remove(key);
					return result;
				}
				return null;
			}
			set
			{
				this._data[key] = value;
				this._initialKeys.Add(key);
			}
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00026FB4 File Offset: 0x000251B4
		public void Keep()
		{
			this._retainedKeys.Clear();
			this._retainedKeys.UnionWith(this._data.Keys);
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00026FD7 File Offset: 0x000251D7
		public void Keep(string key)
		{
			this._retainedKeys.Add(key);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00026FE8 File Offset: 0x000251E8
		public void Load(ControllerContext controllerContext, ITempDataProvider tempDataProvider)
		{
			IDictionary<string, object> dictionary = tempDataProvider.LoadTempData(controllerContext);
			this._data = ((dictionary != null) ? new Dictionary<string, object>(dictionary, StringComparer.OrdinalIgnoreCase) : new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase));
			this._initialKeys = new HashSet<string>(this._data.Keys, StringComparer.OrdinalIgnoreCase);
			this._retainedKeys.Clear();
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00027044 File Offset: 0x00025244
		public object Peek(string key)
		{
			object result;
			this._data.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00027098 File Offset: 0x00025298
		public void Save(ControllerContext controllerContext, ITempDataProvider tempDataProvider)
		{
			this._data.RemoveFromDictionary(delegate(KeyValuePair<string, object> entry, TempDataDictionary tempData)
			{
				string key = entry.Key;
				return !tempData._initialKeys.Contains(key) && !tempData._retainedKeys.Contains(key);
			}, this);
			tempDataProvider.SaveTempData(controllerContext, this._data);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x000270D0 File Offset: 0x000252D0
		public void Add(string key, object value)
		{
			this._data.Add(key, value);
			this._initialKeys.Add(key);
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x000270EC File Offset: 0x000252EC
		public void Clear()
		{
			this._data.Clear();
			this._retainedKeys.Clear();
			this._initialKeys.Clear();
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0002710F File Offset: 0x0002530F
		public bool ContainsKey(string key)
		{
			return this._data.ContainsKey(key);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0002711D File Offset: 0x0002531D
		public bool ContainsValue(object value)
		{
			return this._data.ContainsValue(value);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0002712B File Offset: 0x0002532B
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return new TempDataDictionary.TempDataDictionaryEnumerator(this);
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x00027133 File Offset: 0x00025333
		public bool Remove(string key)
		{
			this._retainedKeys.Remove(key);
			this._initialKeys.Remove(key);
			return this._data.Remove(key);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0002715B File Offset: 0x0002535B
		public bool TryGetValue(string key, out object value)
		{
			this._initialKeys.Remove(key);
			return this._data.TryGetValue(key, out value);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00027177 File Offset: 0x00025377
		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int index)
		{
			((ICollection<KeyValuePair<string, object>>)this._data).CopyTo(array, index);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00027186 File Offset: 0x00025386
		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> keyValuePair)
		{
			this._initialKeys.Add(keyValuePair.Key);
			((ICollection<KeyValuePair<string, object>>)this._data).Add(keyValuePair);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x000271A7 File Offset: 0x000253A7
		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> keyValuePair)
		{
			return ((ICollection<KeyValuePair<string, object>>)this._data).Contains(keyValuePair);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x000271B5 File Offset: 0x000253B5
		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> keyValuePair)
		{
			this._initialKeys.Remove(keyValuePair.Key);
			return ((ICollection<KeyValuePair<string, object>>)this._data).Remove(keyValuePair);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x000271D6 File Offset: 0x000253D6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new TempDataDictionary.TempDataDictionaryEnumerator(this);
		}

		// Token: 0x040003DD RID: 989
		internal const string TempDataSerializationKey = "__tempData";

		// Token: 0x040003DE RID: 990
		private Dictionary<string, object> _data;

		// Token: 0x040003DF RID: 991
		private HashSet<string> _initialKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040003E0 RID: 992
		private HashSet<string> _retainedKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x020001EB RID: 491
		private sealed class TempDataDictionaryEnumerator : IEnumerator<KeyValuePair<string, object>>, IDisposable, IEnumerator
		{
			// Token: 0x06000ED8 RID: 3800 RVA: 0x000271DE File Offset: 0x000253DE
			public TempDataDictionaryEnumerator(TempDataDictionary tempData)
			{
				this._tempData = tempData;
				this._enumerator = this._tempData._data.GetEnumerator();
			}

			// Token: 0x1700033E RID: 830
			// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00027208 File Offset: 0x00025408
			public KeyValuePair<string, object> Current
			{
				get
				{
					KeyValuePair<string, object> result = this._enumerator.Current;
					this._tempData._initialKeys.Remove(result.Key);
					return result;
				}
			}

			// Token: 0x1700033F RID: 831
			// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0002723A File Offset: 0x0002543A
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000EDB RID: 3803 RVA: 0x00027247 File Offset: 0x00025447
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x06000EDC RID: 3804 RVA: 0x00027254 File Offset: 0x00025454
			public void Reset()
			{
				this._enumerator.Reset();
			}

			// Token: 0x06000EDD RID: 3805 RVA: 0x00027261 File Offset: 0x00025461
			void IDisposable.Dispose()
			{
				this._enumerator.Dispose();
			}

			// Token: 0x040003E2 RID: 994
			private IEnumerator<KeyValuePair<string, object>> _enumerator;

			// Token: 0x040003E3 RID: 995
			private TempDataDictionary _tempData;
		}
	}
}
