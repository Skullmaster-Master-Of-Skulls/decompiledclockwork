using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000022 RID: 34
	public sealed class ConfigurationElementSchemaCollection : IEnumerable<ConfigurationElementSchema>, ICollection, IEnumerable
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00005E8C File Offset: 0x00004E8C
		internal ConfigurationElementSchemaCollection(IAppHostElementSchemaCollection schemas)
		{
			uint count = schemas.Count;
			this._schemas = new List<ConfigurationElementSchema>((int)count);
			for (uint num = 0U; num < count; num += 1U)
			{
				ConfigurationElementSchema item = new ConfigurationElementSchema(schemas[num]);
				this._schemas.Add(item);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00005EDC File Offset: 0x00004EDC
		public int Count
		{
			get
			{
				return this._schemas.Count;
			}
		}

		// Token: 0x170000BC RID: 188
		public ConfigurationElementSchema this[int index]
		{
			get
			{
				return this._schemas[index];
			}
		}

		// Token: 0x170000BD RID: 189
		public ConfigurationElementSchema this[string name]
		{
			get
			{
				foreach (ConfigurationElementSchema configurationElementSchema in this._schemas)
				{
					if (string.Equals(configurationElementSchema.Name, name, StringComparison.OrdinalIgnoreCase))
					{
						return configurationElementSchema;
					}
				}
				return null;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005F5C File Offset: 0x00004F5C
		public IEnumerator<ConfigurationElementSchema> GetEnumerator()
		{
			return this._schemas.GetEnumerator();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005F6E File Offset: 0x00004F6E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00005F76 File Offset: 0x00004F76
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this._schemas).IsSynchronized;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00005F83 File Offset: 0x00004F83
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this._schemas).SyncRoot;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005F90 File Offset: 0x00004F90
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this._schemas).CopyTo(array, index);
		}

		// Token: 0x04000065 RID: 101
		private List<ConfigurationElementSchema> _schemas;
	}
}
