using System;
using System.Collections.Generic;

namespace System.Web.WebPages.ApplicationParts
{
	// Token: 0x0200000B RID: 11
	internal class DictionaryBasedVirtualPathFactory : IVirtualPathFactory
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002D3E File Offset: 0x00000F3E
		internal void RegisterPath(string virtualPath, Func<object> factory)
		{
			this._factories[virtualPath] = factory;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D4D File Offset: 0x00000F4D
		public bool Exists(string virtualPath)
		{
			return this._factories.ContainsKey(virtualPath);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D5B File Offset: 0x00000F5B
		public object CreateInstance(string virtualPath)
		{
			return this._factories[virtualPath]();
		}

		// Token: 0x04000017 RID: 23
		private Dictionary<string, Func<object>> _factories = new Dictionary<string, Func<object>>(StringComparer.OrdinalIgnoreCase);
	}
}
