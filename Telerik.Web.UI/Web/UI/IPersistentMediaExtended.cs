using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DC2 RID: 3522
	internal interface IPersistentMediaExtended
	{
		// Token: 0x06008371 RID: 33649
		T Get<T>(string key) where T : class;

		// Token: 0x06008372 RID: 33650
		void Add<T>(string key, T item) where T : class;

		// Token: 0x06008373 RID: 33651
		void Remove(string key);
	}
}
