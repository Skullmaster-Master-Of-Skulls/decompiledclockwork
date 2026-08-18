using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000077 RID: 119
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class TypeEnumerableViewSchema : BaseTypeViewSchema
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x00012324 File Offset: 0x00010524
		public TypeEnumerableViewSchema(string viewName, Type type) : base(viewName, type)
		{
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00012330 File Offset: 0x00010530
		protected override Type GetRowType(Type objectType)
		{
			if (objectType.IsArray)
			{
				return objectType.GetElementType();
			}
			PropertyInfo[] properties = objectType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			foreach (PropertyInfo propertyInfo in properties)
			{
				ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
				if (indexParameters.Length != 0)
				{
					return propertyInfo.PropertyType;
				}
			}
			return null;
		}
	}
}
