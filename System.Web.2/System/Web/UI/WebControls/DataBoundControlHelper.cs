using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Compilation;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003AE RID: 942
	internal static class DataBoundControlHelper
	{
		// Token: 0x06002D90 RID: 11664 RVA: 0x00094F8C File Offset: 0x0009318C
		public static Control FindControl(Control control, string controlID)
		{
			Control control2 = control;
			Control control3 = null;
			if (control == control.Page)
			{
				return control.FindControl(controlID);
			}
			while (control3 == null && control2 != control.Page)
			{
				control2 = control2.NamingContainer;
				if (control2 == null)
				{
					throw new HttpException(SR.GetString("DataBoundControlHelper_NoNamingContainer", new object[]
					{
						control.GetType().Name,
						control.ID
					}));
				}
				control3 = control2.FindControl(controlID);
			}
			return control3;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x00094FFC File Offset: 0x000931FC
		public static bool CompareStringArrays(string[] stringA, string[] stringB)
		{
			if (stringA == null && stringB == null)
			{
				return true;
			}
			if (stringA == null || stringB == null)
			{
				return false;
			}
			if (stringA.Length != stringB.Length)
			{
				return false;
			}
			for (int i = 0; i < stringA.Length; i++)
			{
				if (!string.Equals(stringA[i], stringB[i], StringComparison.Ordinal))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x00095044 File Offset: 0x00093244
		internal static bool IsBindableType(Type type, bool enableEnums)
		{
			if (type == null)
			{
				return false;
			}
			Type underlyingType = Nullable.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			if (type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type == typeof(decimal) || type == typeof(Guid) || type == typeof(DateTimeOffset) || type == typeof(TimeSpan))
			{
				return true;
			}
			BindableTypeAttribute bindableTypeAttribute = (BindableTypeAttribute)TypeDescriptor.GetAttributes(type)[typeof(BindableTypeAttribute)];
			if (bindableTypeAttribute != null)
			{
				return bindableTypeAttribute.IsBindable;
			}
			return enableEnums && type.IsEnum;
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x00095118 File Offset: 0x00093318
		internal static void ExtractValuesFromBindableControls(IOrderedDictionary dictionary, Control container)
		{
			IBindableControl bindableControl = container as IBindableControl;
			if (bindableControl != null)
			{
				bindableControl.ExtractValues(dictionary);
			}
			foreach (object obj in container.Controls)
			{
				Control container2 = (Control)obj;
				DataBoundControlHelper.ExtractValuesFromBindableControls(dictionary, container2);
			}
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x00095184 File Offset: 0x00093384
		internal static void EnableDynamicData(INamingContainer control, string entityTypeName)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (DataBoundControlHelper.s_enableDynamicDataMethod == null)
			{
				Type type = Assembly.Load("System.Web.DynamicData, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35").GetType("System.Web.UI.DataControlExtensions");
				DataBoundControlHelper.s_enableDynamicDataMethod = type.GetMethod("EnableDynamicData", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					typeof(INamingContainer),
					typeof(Type)
				}, null);
			}
			Type type2 = BuildManager.GetType(entityTypeName, false);
			if (type2 != null)
			{
				DataBoundControlHelper.s_enableDynamicDataMethod.Invoke(null, new object[]
				{
					control,
					type2
				});
			}
		}

		// Token: 0x04001F91 RID: 8081
		private static MethodInfo s_enableDynamicDataMethod;
	}
}
