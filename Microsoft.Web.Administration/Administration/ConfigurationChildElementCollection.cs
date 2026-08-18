using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000020 RID: 32
	[DebuggerDisplay("Count = {Count}")]
	public sealed class ConfigurationChildElementCollection : ICollection, IEnumerable<ConfigurationElement>, IEnumerable
	{
		// Token: 0x06000177 RID: 375 RVA: 0x00005C44 File Offset: 0x00004C44
		internal ConfigurationChildElementCollection(Configuration configuration, IAppHostChildElementCollection childElements)
		{
			this._elements = new List<ConfigurationElement>((int)childElements.Count);
			for (uint num = 0U; num < childElements.Count; num += 1U)
			{
				IAppHostElement appHostElement = childElements[num];
				ConfigurationElement configurationElement;
				if (appHostElement.Collection != null)
				{
					configurationElement = new ConfigurationElementCollection();
				}
				else
				{
					configurationElement = new ConfigurationElement();
				}
				configurationElement.Initialize(configuration, appHostElement);
				this._elements.Add(configurationElement);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005CB0 File Offset: 0x00004CB0
		private ConfigurationChildElementCollection()
		{
			this._elements = new List<ConfigurationElement>();
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00005CC3 File Offset: 0x00004CC3
		public int Count
		{
			get
			{
				return this._elements.Count;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00005CD0 File Offset: 0x00004CD0
		internal static ConfigurationChildElementCollection Empty
		{
			get
			{
				if (ConfigurationChildElementCollection._empty == null)
				{
					ConfigurationChildElementCollection._empty = new ConfigurationChildElementCollection();
				}
				return ConfigurationChildElementCollection._empty;
			}
		}

		// Token: 0x170000B1 RID: 177
		public ConfigurationElement this[int index]
		{
			get
			{
				return this._elements[index];
			}
		}

		// Token: 0x170000B2 RID: 178
		public ConfigurationElement this[string name]
		{
			get
			{
				foreach (ConfigurationElement configurationElement in this._elements)
				{
					if (string.Equals(configurationElement.ElementTagName, name, StringComparison.OrdinalIgnoreCase))
					{
						return configurationElement;
					}
				}
				return null;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005D5C File Offset: 0x00004D5C
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this._elements).CopyTo(array, index);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005D6B File Offset: 0x00004D6B
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this._elements).IsSynchronized;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00005D78 File Offset: 0x00004D78
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this._elements).SyncRoot;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005D85 File Offset: 0x00004D85
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005D8D File Offset: 0x00004D8D
		public IEnumerator<ConfigurationElement> GetEnumerator()
		{
			return this._elements.GetEnumerator();
		}

		// Token: 0x0400005F RID: 95
		private List<ConfigurationElement> _elements;

		// Token: 0x04000060 RID: 96
		private static ConfigurationChildElementCollection _empty;
	}
}
