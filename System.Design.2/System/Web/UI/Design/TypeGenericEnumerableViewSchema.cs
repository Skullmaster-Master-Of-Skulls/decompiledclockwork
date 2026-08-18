using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000079 RID: 121
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class TypeGenericEnumerableViewSchema : BaseTypeViewSchema
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x00012324 File Offset: 0x00010524
		public TypeGenericEnumerableViewSchema(string viewName, Type type) : base(viewName, type)
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000124EC File Offset: 0x000106EC
		protected override Type GetRowType(Type objectType)
		{
			Type type = null;
			if (objectType.IsInterface && objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			{
				type = objectType;
			}
			else
			{
				Type[] interfaces = objectType.GetInterfaces();
				foreach (Type type2 in interfaces)
				{
					if (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(IEnumerable<>))
					{
						type = type2;
						break;
					}
				}
			}
			Type[] genericArguments = type.GetGenericArguments();
			if (genericArguments[0].IsGenericParameter)
			{
				return null;
			}
			return genericArguments[0];
		}
	}
}
