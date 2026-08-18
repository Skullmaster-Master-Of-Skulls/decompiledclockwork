using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x02000024 RID: 36
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataColumnSelectionConverter : TypeConverter
	{
		// Token: 0x0600012C RID: 300 RVA: 0x0000BC9C File Offset: 0x00009E9C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000BCB3 File Offset: 0x00009EB3
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

		// Token: 0x0600012E RID: 302 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			string[] array = null;
			ArrayList arrayList = new ArrayList();
			if (context != null)
			{
				IComponent component = context.Instance as IComponent;
				if (component != null)
				{
					GridView gridView = component as GridView;
					if (gridView != null)
					{
						if (gridView.AutoGenerateColumns)
						{
							DataFieldConverter dataFieldConverter = new DataFieldConverter();
							TypeConverter.StandardValuesCollection standardValues = dataFieldConverter.GetStandardValues(context);
							foreach (object value in standardValues)
							{
								arrayList.Add(value);
							}
						}
						DataControlFieldCollection columns = gridView.Columns;
						foreach (object obj in columns)
						{
							DataControlField dataControlField = (DataControlField)obj;
							BoundField boundField = dataControlField as BoundField;
							if (boundField != null)
							{
								string dataField = boundField.DataField;
								if (!arrayList.Contains(dataField))
								{
									arrayList.Add(dataField);
								}
							}
						}
						arrayList.Sort();
						array = new string[arrayList.Count];
						arrayList.CopyTo(array, 0);
					}
				}
			}
			return new TypeConverter.StandardValuesCollection(array);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000BE1C File Offset: 0x0000A01C
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return context != null && context.Instance is IComponent;
		}
	}
}
