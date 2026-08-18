using System;
using System.Reflection;

namespace AutoMapper
{
	// Token: 0x0200001E RID: 30
	public interface IMemberGetter : IMemberResolver, IValueResolver
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E4 RID: 228
		MemberInfo MemberInfo { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E5 RID: 229
		string Name { get; }

		// Token: 0x060000E6 RID: 230
		object GetValue(object source);
	}
}
