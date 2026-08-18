using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Web.UI.Design.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x02000026 RID: 38
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataMemberConverter : TypeConverter
	{
		// Token: 0x06000139 RID: 313 RVA: 0x0000BC9C File Offset: 0x00009E9C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000BCB3 File Offset: 0x00009EB3
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

		// Token: 0x0600013B RID: 315 RVA: 0x0000C030 File Offset: 0x0000A230
		private DesignerDataSourceView GetView(IDesigner dataBoundControlDesigner)
		{
			DataBoundControlDesigner dataBoundControlDesigner2 = dataBoundControlDesigner as DataBoundControlDesigner;
			if (dataBoundControlDesigner2 != null)
			{
				return dataBoundControlDesigner2.DesignerView;
			}
			BaseDataListDesigner baseDataListDesigner = dataBoundControlDesigner as BaseDataListDesigner;
			if (baseDataListDesigner != null)
			{
				return baseDataListDesigner.DesignerView;
			}
			RepeaterDesigner repeaterDesigner = dataBoundControlDesigner as RepeaterDesigner;
			if (repeaterDesigner != null)
			{
				return repeaterDesigner.DesignerView;
			}
			return null;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000C074 File Offset: 0x0000A274
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			string[] array = null;
			if (context != null)
			{
				IComponent component = context.Instance as IComponent;
				if (component != null)
				{
					ISite site = component.Site;
					if (site != null)
					{
						IDesignerHost designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
						if (designerHost != null)
						{
							IDesigner designer = designerHost.GetDesigner(component);
							DesignerDataSourceView view = this.GetView(designer);
							if (view != null)
							{
								IDataSourceDesigner dataSourceDesigner = view.DataSourceDesigner;
								if (dataSourceDesigner != null)
								{
									string[] viewNames = dataSourceDesigner.GetViewNames();
									if (viewNames != null)
									{
										array = new string[viewNames.Length];
										viewNames.CopyTo(array, 0);
									}
								}
							}
							if (array == null && designer != null && designer is IDataSourceProvider)
							{
								IDataSourceProvider dataSourceProvider = designer as IDataSourceProvider;
								object obj = null;
								if (dataSourceProvider != null)
								{
									obj = dataSourceProvider.GetSelectedDataSource();
								}
								if (obj != null)
								{
									array = DesignTimeData.GetDataMembers(obj);
								}
							}
						}
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

		// Token: 0x0600013D RID: 317 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000BE1C File Offset: 0x0000A01C
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return context != null && context.Instance is IComponent;
		}
	}
}
