using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E27 RID: 3623
	internal interface IPersistentMedia
	{
		// Token: 0x0600895F RID: 35167
		T Get<T>(string key) where T : class;

		// Token: 0x06008960 RID: 35168
		void Add<T>(string key, T item) where T : class;
	}
}
