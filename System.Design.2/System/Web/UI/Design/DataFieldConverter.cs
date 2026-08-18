using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Web.UI.Design.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x02000025 RID: 37
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataFieldConverter : TypeConverter
	{
		// Token: 0x06000132 RID: 306 RVA: 0x0000BC9C File Offset: 0x00009E9C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000BCB3 File Offset: 0x00009EB3
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

		// Token: 0x06000134 RID: 308 RVA: 0x0000BE34 File Offset: 0x0000A034
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

		// Token: 0x06000135 RID: 309 RVA: 0x0000BE78 File Offset: 0x0000A078
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			object[] array = null;
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
								IDataSourceViewSchema dataSourceViewSchema = null;
								try
								{
									dataSourceViewSchema = view.Schema;
								}
								catch (Exception ex)
								{
									IComponentDesignerDebugService componentDesignerDebugService = (IComponentDesignerDebugService)site.GetService(typeof(IComponentDesignerDebugService));
									if (componentDesignerDebugService != null)
									{
										componentDesignerDebugService.Fail(SR.GetString("DataSource_DebugService_FailedCall", new object[]
										{
											"DesignerDataSourceView.Schema",
											ex.Message
										}));
									}
								}
								if (dataSourceViewSchema != null)
								{
									IDataSourceFieldSchema[] fields = dataSourceViewSchema.GetFields();
									if (fields != null)
									{
										array = new object[fields.Length];
										for (int i = 0; i < fields.Length; i++)
										{
											array[i] = fields[i].Name;
										}
									}
								}
							}
							if (array == null && designer != null && designer is IDataSourceProvider)
							{
								IDataSourceProvider dataSourceProvider = designer as IDataSourceProvider;
								IEnumerable enumerable = null;
								if (dataSourceProvider != null)
								{
									enumerable = dataSourceProvider.GetResolvedSelectedDataSource();
								}
								if (enumerable != null)
								{
									PropertyDescriptorCollection dataFields = DesignTimeData.GetDataFields(enumerable);
									if (dataFields != null)
									{
										ArrayList arrayList = new ArrayList();
										foreach (object obj in dataFields)
										{
											PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
											arrayList.Add(propertyDescriptor.Name);
										}
										array = arrayList.ToArray();
									}
								}
							}
						}
					}
				}
			}
			return new TypeConverter.StandardValuesCollection(array);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000BE1C File Offset: 0x0000A01C
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return context != null && context.Instance is IComponent;
		}
	}
}
