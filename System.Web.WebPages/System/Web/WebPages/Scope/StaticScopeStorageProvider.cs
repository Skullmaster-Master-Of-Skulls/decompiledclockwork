using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System.Web.WebPages.Scope
{
	// Token: 0x0200007B RID: 123
	public class StaticScopeStorageProvider : IScopeStorageProvider
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000C600 File Offset: 0x0000A800
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0000C611 File Offset: 0x0000A811
		public IDictionary<object, object> CurrentScope
		{
			get
			{
				return this._currentContext ?? StaticScopeStorageProvider._defaultContext;
			}
			set
			{
				this._currentContext = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000C61A File Offset: 0x0000A81A
		public IDictionary<object, object> GlobalScope
		{
			get
			{
				return StaticScopeStorageProvider._defaultContext;
			}
		}

		// Token: 0x04000116 RID: 278
		private static readonly IDictionary<object, object> _defaultContext = new ScopeStorageDictionary(null, new ConcurrentDictionary<object, object>(ScopeStorageComparer.Instance));

		// Token: 0x04000117 RID: 279
		private IDictionary<object, object> _currentContext;
	}
}
