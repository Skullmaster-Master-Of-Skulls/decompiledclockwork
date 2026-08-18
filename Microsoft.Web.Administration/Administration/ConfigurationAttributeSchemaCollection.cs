using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200001E RID: 30
	public sealed class ConfigurationAttributeSchemaCollection : IEnumerable<ConfigurationAttributeSchema>, ICollection, IEnumerable
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00005A18 File Offset: 0x00004A18
		internal ConfigurationAttributeSchemaCollection(IAppHostPropertySchemaCollection schemas)
		{
			uint count = schemas.Count;
			this._schemas = new List<ConfigurationAttributeSchema>((int)count);
			for (uint num = 0U; num < count; num += 1U)
			{
				ConfigurationAttributeSchema item = new ConfigurationAttributeSchema(schemas[num]);
				this._schemas.Add(item);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005A68 File Offset: 0x00004A68
		public int Count
		{
			get
			{
				return this._schemas.Count;
			}
		}

		// Token: 0x170000A6 RID: 166
		public ConfigurationAttributeSchema this[int index]
		{
			get
			{
				return this._schemas[index];
			}
		}

		// Token: 0x170000A7 RID: 167
		public ConfigurationAttributeSchema this[string name]
		{
			get
			{
				foreach (ConfigurationAttributeSchema configurationAttributeSchema in this._schemas)
				{
					if (string.Equals(configurationAttributeSchema.Name, name, StringComparison.OrdinalIgnoreCase))
					{
						return configurationAttributeSchema;
					}
				}
				return null;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005AE8 File Offset: 0x00004AE8
		public IEnumerator<ConfigurationAttributeSchema> GetEnumerator()
		{
			return this._schemas.GetEnumerator();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005AFA File Offset: 0x00004AFA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00005B02 File Offset: 0x00004B02
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this._schemas).IsSynchronized;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00005B0F File Offset: 0x00004B0F
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this._schemas).SyncRoot;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005B1C File Offset: 0x00004B1C
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this._schemas).CopyTo(array, index);
		}

		// Token: 0x0400005B RID: 91
		private List<ConfigurationAttributeSchema> _schemas;
	}
}
