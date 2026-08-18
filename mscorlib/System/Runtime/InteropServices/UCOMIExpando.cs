using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000543 RID: 1347
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IExpando instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface UCOMIExpando : UCOMIReflect
	{
		// Token: 0x0600336C RID: 13164
		FieldInfo AddField(string name);

		// Token: 0x0600336D RID: 13165
		PropertyInfo AddProperty(string name);

		// Token: 0x0600336E RID: 13166
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x0600336F RID: 13167
		void RemoveMember(MemberInfo m);
	}
}
