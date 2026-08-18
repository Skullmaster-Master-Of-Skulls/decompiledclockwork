using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Web
{
	// Token: 0x02000053 RID: 83
	internal sealed class DynamicModuleRegistry
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x00007BF8 File Offset: 0x00005DF8
		public void Add(Type moduleType)
		{
			if (moduleType == null)
			{
				throw new ArgumentNullException("moduleType");
			}
			if (!typeof(IHttpModule).IsAssignableFrom(moduleType))
			{
				string message = string.Format(CultureInfo.CurrentCulture, SR.GetString("DynamicModuleRegistry_TypeIsNotIHttpModule"), new object[]
				{
					moduleType
				});
				throw new ArgumentException(message, "moduleType");
			}
			object lockObj = this._lockObj;
			lock (lockObj)
			{
				if (this._entriesReadonly)
				{
					throw new InvalidOperationException(SR.GetString("DynamicModuleRegistry_ModulesAlreadyInitialized"));
				}
				this._entries.Add(new DynamicModuleRegistryEntry(DynamicModuleRegistry.MakeUniqueModuleName(moduleType), moduleType.AssemblyQualifiedName));
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00007CB8 File Offset: 0x00005EB8
		public ICollection<DynamicModuleRegistryEntry> LockAndFetchList()
		{
			object lockObj = this._lockObj;
			ICollection<DynamicModuleRegistryEntry> entries;
			lock (lockObj)
			{
				this._entriesReadonly = true;
				entries = this._entries;
			}
			return entries;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00007D04 File Offset: 0x00005F04
		private static string MakeUniqueModuleName(Type moduleType)
		{
			return string.Format(CultureInfo.InvariantCulture, "__DynamicModule_{0}_{1}", new object[]
			{
				moduleType.AssemblyQualifiedName,
				Guid.NewGuid()
			});
		}

		// Token: 0x0400015B RID: 347
		private const string _moduleNameFormat = "__DynamicModule_{0}_{1}";

		// Token: 0x0400015C RID: 348
		private readonly List<DynamicModuleRegistryEntry> _entries = new List<DynamicModuleRegistryEntry>();

		// Token: 0x0400015D RID: 349
		private bool _entriesReadonly;

		// Token: 0x0400015E RID: 350
		private readonly object _lockObj = new object();
	}
}
