using System;
using System.Reflection;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000034 RID: 52
	public interface IRequiredMemberSelector
	{
		// Token: 0x06000184 RID: 388
		bool IsRequiredMember(MemberInfo member);
	}
}
