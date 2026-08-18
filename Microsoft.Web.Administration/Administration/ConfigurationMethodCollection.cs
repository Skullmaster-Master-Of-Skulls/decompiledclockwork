using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000037 RID: 55
	public sealed class ConfigurationMethodCollection : ICollection, IEnumerable<ConfigurationMethod>, IEnumerable
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00006F04 File Offset: 0x00005F04
		internal ConfigurationMethodCollection(Configuration configuration, IAppHostMethodCollection methods)
		{
			uint count = methods.Count;
			this._methods = new List<ConfigurationMethod>((int)count);
			for (uint num = 0U; num < count; num += 1U)
			{
				IAppHostMethod method = methods[num];
				this._methods.Add(new ConfigurationMethod(method));
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00006F54 File Offset: 0x00005F54
		public int Count
		{
			get
			{
				return this._methods.Count;
			}
		}

		// Token: 0x170000CE RID: 206
		public ConfigurationMethod this[int index]
		{
			get
			{
				return this._methods[index];
			}
		}

		// Token: 0x170000CF RID: 207
		public ConfigurationMethod this[string methodName]
		{
			get
			{
				foreach (ConfigurationMethod configurationMethod in this)
				{
					if (configurationMethod != null && string.Equals(configurationMethod.Name, methodName, StringComparison.OrdinalIgnoreCase))
					{
						return configurationMethod;
					}
				}
				return null;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006FCC File Offset: 0x00005FCC
		public IEnumerator<ConfigurationMethod> GetEnumerator()
		{
			return this._methods.GetEnumerator();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006FDE File Offset: 0x00005FDE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._methods.GetEnumerator();
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00006FF0 File Offset: 0x00005FF0
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this._methods).IsSynchronized;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00006FFD File Offset: 0x00005FFD
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this._methods).SyncRoot;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000700A File Offset: 0x0000600A
		void ICollection.CopyTo(Array array, int index)
		{
			this._methods.CopyTo((ConfigurationMethod[])array, index);
		}

		// Token: 0x0400009D RID: 157
		private List<ConfigurationMethod> _methods;
	}
}
