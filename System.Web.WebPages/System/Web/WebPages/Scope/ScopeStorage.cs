using System;
using System.Collections.Generic;

namespace System.Web.WebPages.Scope
{
	// Token: 0x02000079 RID: 121
	public static class ScopeStorage
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000C4B8 File Offset: 0x0000A6B8
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		public static IScopeStorageProvider CurrentProvider
		{
			get
			{
				return ScopeStorage._stateStorageProvider ?? ScopeStorage._defaultStorageProvider;
			}
			set
			{
				ScopeStorage._stateStorageProvider = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		public static IDictionary<object, object> CurrentScope
		{
			get
			{
				return ScopeStorage.CurrentProvider.CurrentScope;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000C4DC File Offset: 0x0000A6DC
		public static IDictionary<object, object> GlobalScope
		{
			get
			{
				return ScopeStorage.CurrentProvider.GlobalScope;
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000C504 File Offset: 0x0000A704
		public static IDisposable CreateTransientScope(IDictionary<object, object> context)
		{
			IDictionary<object, object> currentContext = ScopeStorage.CurrentScope;
			ScopeStorage.CurrentProvider.CurrentScope = context;
			return new DisposableAction(delegate()
			{
				ScopeStorage.CurrentProvider.CurrentScope = currentContext;
			});
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000C53E File Offset: 0x0000A73E
		public static IDisposable CreateTransientScope()
		{
			return ScopeStorage.CreateTransientScope(new ScopeStorageDictionary(ScopeStorage.CurrentScope));
		}

		// Token: 0x04000111 RID: 273
		private static readonly IScopeStorageProvider _defaultStorageProvider = new StaticScopeStorageProvider();

		// Token: 0x04000112 RID: 274
		private static IScopeStorageProvider _stateStorageProvider;
	}
}
