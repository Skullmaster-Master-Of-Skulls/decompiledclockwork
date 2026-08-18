using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200001C RID: 28
	[DebuggerDisplay("Count = {Count}")]
	public sealed class ConfigurationAttributeCollection : ICollection, IEnumerable<ConfigurationAttribute>, IEnumerable
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00005818 File Offset: 0x00004818
		internal ConfigurationAttributeCollection(IAppHostPropertyCollection properties, ConfigurationElement parentElement)
		{
			int count = (int)properties.Count;
			this._attributes = new List<ConfigurationAttribute>(count);
			for (int i = 0; i < count; i++)
			{
				IAppHostProperty property = properties[i];
				this._attributes.Add(new ConfigurationAttribute(property, parentElement));
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005869 File Offset: 0x00004869
		public int Count
		{
			get
			{
				return this._attributes.Count;
			}
		}

		// Token: 0x17000094 RID: 148
		public ConfigurationAttribute this[int index]
		{
			get
			{
				return this._attributes[index];
			}
		}

		// Token: 0x17000095 RID: 149
		public ConfigurationAttribute this[string name]
		{
			get
			{
				foreach (ConfigurationAttribute configurationAttribute in this._attributes)
				{
					if (string.Equals(configurationAttribute.Name, name, StringComparison.OrdinalIgnoreCase))
					{
						return configurationAttribute;
					}
				}
				return null;
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000058E8 File Offset: 0x000048E8
		public IEnumerator<ConfigurationAttribute> GetEnumerator()
		{
			return this._attributes.GetEnumerator();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000058FA File Offset: 0x000048FA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005902 File Offset: 0x00004902
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this._attributes).CopyTo(array, index);
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005911 File Offset: 0x00004911
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this._attributes).IsSynchronized;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000591E File Offset: 0x0000491E
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this._attributes).SyncRoot;
			}
		}

		// Token: 0x04000059 RID: 89
		private List<ConfigurationAttribute> _attributes;
	}
}
