using System;
using System.CodeDom;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x0200002B RID: 43
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataSourceConverter : TypeConverter
	{
		// Token: 0x06000153 RID: 339 RVA: 0x0000BC9C File Offset: 0x00009E9C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000BCB3 File Offset: 0x00009EB3
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			if (value.GetType() == typeof(string))
			{
				return (string)value;
			}
			throw base.GetConvertFromException(value);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000C358 File Offset: 0x0000A558
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			object[] array = null;
			if (context != null)
			{
				ArrayList arrayList = new ArrayList();
				IContainer container = context.Container;
				if (container != null)
				{
					ComponentCollection components = container.Components;
					foreach (object obj in ((IEnumerable)components))
					{
						IComponent component = (IComponent)obj;
						if (this.IsValidDataSource(component) && !Marshal.IsComObject(component))
						{
							PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["Modifiers"];
							if (propertyDescriptor != null)
							{
								MemberAttributes memberAttributes = (MemberAttributes)propertyDescriptor.GetValue(component);
								if ((memberAttributes & MemberAttributes.AccessMask) == MemberAttributes.Private)
								{
									continue;
								}
							}
							ISite site = component.Site;
							if (site != null)
							{
								string name = site.Name;
								if (name != null)
								{
									arrayList.Add(name);
								}
							}
						}
					}
				}
				array = arrayList.ToArray();
				Array.Sort(array, Comparer.Default);
			}
			return new TypeConverter.StandardValuesCollection(array);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000C458 File Offset: 0x0000A658
		protected virtual bool IsValidDataSource(IComponent component)
		{
			return component is IEnumerable || component is IListSource;
		}
	}
}
