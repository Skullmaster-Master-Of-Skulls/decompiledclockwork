using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000542 RID: 1346
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IReflect instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface UCOMIReflect
	{
		// Token: 0x06003360 RID: 13152
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06003361 RID: 13153
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x06003362 RID: 13154
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x06003363 RID: 13155
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x06003364 RID: 13156
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x06003365 RID: 13157
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x06003366 RID: 13158
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06003367 RID: 13159
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x06003368 RID: 13160
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x06003369 RID: 13161
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x0600336A RID: 13162
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600336B RID: 13163
		Type UnderlyingSystemType { get; }
	}
}
