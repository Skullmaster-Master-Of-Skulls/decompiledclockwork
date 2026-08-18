using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x0200002D RID: 45
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataSourceViewSchemaConverter : TypeConverter
	{
		// Token: 0x06000172 RID: 370 RVA: 0x0000BC9C File Offset: 0x00009E9C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000BCB3 File Offset: 0x00009EB3
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

		// Token: 0x06000174 RID: 372 RVA: 0x0000C82A File Offset: 0x0000AA2A
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return this.GetStandardValues(context, null);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000C834 File Offset: 0x0000AA34
		public virtual TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context, Type typeFilter)
		{
			string[] array = null;
			if (context != null)
			{
				IDataSourceViewSchemaAccessor dataSourceViewSchemaAccessor = context.Instance as IDataSourceViewSchemaAccessor;
				if (dataSourceViewSchemaAccessor != null)
				{
					IDataSourceViewSchema dataSourceViewSchema = dataSourceViewSchemaAccessor.DataSourceViewSchema as IDataSourceViewSchema;
					if (dataSourceViewSchema != null)
					{
						IDataSourceFieldSchema[] fields = dataSourceViewSchema.GetFields();
						string[] array2 = new string[fields.Length];
						int num = 0;
						for (int i = 0; i < fields.Length; i++)
						{
							if ((typeFilter != null && fields[i].DataType == typeFilter) || typeFilter == null)
							{
								array2[num] = fields[i].Name;
								num++;
							}
						}
						array = new string[num];
						Array.Copy(array2, array, num);
					}
				}
				if (array == null)
				{
					array = new string[0];
				}
				Array.Sort(array, Comparer.Default);
			}
			return new TypeConverter.StandardValuesCollection(array);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000C8F1 File Offset: 0x0000AAF1
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return context != null && context.Instance is IDataSourceViewSchemaAccessor;
		}
	}
}
