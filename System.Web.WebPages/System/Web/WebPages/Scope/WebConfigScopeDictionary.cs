using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Configuration;
using System.Web.WebPages.Resources;

namespace System.Web.WebPages.Scope
{
	// Token: 0x0200007C RID: 124
	internal class WebConfigScopeDictionary : IDictionary<object, object>, ICollection<KeyValuePair<object, object>>, IEnumerable<KeyValuePair<object, object>>, IEnumerable
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0000C640 File Offset: 0x0000A840
		public WebConfigScopeDictionary() : this(WebConfigurationManager.AppSettings)
		{
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		public WebConfigScopeDictionary(NameValueCollection appSettings)
		{
			this._items = new Lazy<Dictionary<object, object>>(delegate()
			{
				Dictionary<object, object> dictionary = new Dictionary<object, object>(ScopeStorageComparer.Instance);
				foreach (string text in appSettings.AllKeys)
				{
					dictionary[text] = appSettings[text];
				}
				return dictionary;
			});
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000C6E2 File Offset: 0x0000A8E2
		private IDictionary<object, object> Items
		{
			get
			{
				return this._items.Value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000C6EF File Offset: 0x0000A8EF
		public ICollection<object> Keys
		{
			get
			{
				return this.Items.Keys;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		public ICollection<object> Values
		{
			get
			{
				return this.Items.Values;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000C709 File Offset: 0x0000A909
		public int Count
		{
			get
			{
				return this.Items.Count;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000C716 File Offset: 0x0000A916
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000C5 RID: 197
		public object this[object key]
		{
			get
			{
				object result;
				this.TryGetValue(key, out result);
				return result;
			}
			set
			{
				throw new NotSupportedException(WebPageResources.StateStorage_ScopeIsReadOnly);
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000C740 File Offset: 0x0000A940
		public bool TryGetValue(object key, out object value)
		{
			return this.Items.TryGetValue(key, out value);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000C74F File Offset: 0x0000A94F
		public IEnumerator<KeyValuePair<object, object>> GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000C75C File Offset: 0x0000A95C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000C764 File Offset: 0x0000A964
		public void Add(object key, object value)
		{
			throw new NotSupportedException(WebPageResources.StateStorage_ScopeIsReadOnly);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000C770 File Offset: 0x0000A970
		public bool ContainsKey(object key)
		{
			return this.Items.ContainsKey(key);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000C77E File Offset: 0x0000A97E
		public bool Remove(object key)
		{
			throw new NotSupportedException(WebPageResources.StateStorage_ScopeIsReadOnly);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000C78A File Offset: 0x0000A98A
		public void Add(KeyValuePair<object, object> item)
		{
			throw new NotSupportedException(WebPageResources.StateStorage_ScopeIsReadOnly);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000C796 File Offset: 0x0000A996
		public void Clear()
		{
			throw new NotSupportedException(WebPageResources.StateStorage_ScopeIsReadOnly);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000C7A2 File Offset: 0x0000A9A2
		public bool Contains(KeyValuePair<object, object> item)
		{
			return this.Items.Contains(item);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		public void CopyTo(KeyValuePair<object, object>[] array, int arrayIndex)
		{
			this.Items.CopyTo(array, arrayIndex);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000C7BF File Offset: 0x0000A9BF
		public bool Remove(KeyValuePair<object, object> item)
		{
			throw new NotSupportedException(WebPageResources.StateStorage_ScopeIsReadOnly);
		}

		// Token: 0x04000118 RID: 280
		private readonly Lazy<Dictionary<object, object>> _items;
	}
}
