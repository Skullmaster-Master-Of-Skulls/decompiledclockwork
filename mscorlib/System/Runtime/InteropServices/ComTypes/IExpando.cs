using System;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000576 RID: 1398
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IExpando : IReflect
	{
		// Token: 0x060033F4 RID: 13300
		FieldInfo AddField(string name);

		// Token: 0x060033F5 RID: 13301
		PropertyInfo AddProperty(string name);

		// Token: 0x060033F6 RID: 13302
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x060033F7 RID: 13303
		void RemoveMember(MemberInfo m);
	}
}
