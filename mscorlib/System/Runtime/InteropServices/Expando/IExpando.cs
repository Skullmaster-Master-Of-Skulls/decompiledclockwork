using System;
using System.Reflection;

namespace System.Runtime.InteropServices.Expando
{
	// Token: 0x0200059D RID: 1437
	[ComVisible(true)]
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	public interface IExpando : IReflect
	{
		// Token: 0x06003473 RID: 13427
		FieldInfo AddField(string name);

		// Token: 0x06003474 RID: 13428
		PropertyInfo AddProperty(string name);

		// Token: 0x06003475 RID: 13429
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x06003476 RID: 13430
		void RemoveMember(MemberInfo m);
	}
}
