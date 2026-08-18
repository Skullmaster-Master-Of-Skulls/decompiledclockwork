using System;
using System.Collections.Generic;

namespace AutoMapper
{
	// Token: 0x02000018 RID: 24
	public interface IMappingOperationOptions
	{
		// Token: 0x060000BA RID: 186
		void ConstructServicesUsing(Func<Type, object> constructor);

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BB RID: 187
		IDictionary<string, object> Items { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BC RID: 188
		// (set) Token: 0x060000BD RID: 189
		bool DisableCache { get; set; }

		// Token: 0x060000BE RID: 190
		void BeforeMap(Action<object, object> beforeFunction);

		// Token: 0x060000BF RID: 191
		void AfterMap(Action<object, object> afterFunction);
	}
}
