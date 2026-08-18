using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Globalization;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000C1 RID: 193
	public class DataSourceIDConverter : TypeConverter
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x0000BC9C File Offset: 0x00009E9C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000BCB3 File Offset: 0x00009EB3
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

		// Token: 0x06000626 RID: 1574 RVA: 0x00020F80 File Offset: 0x0001F180
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			string[] values = null;
			if (context != null)
			{
				WebFormsRootDesigner webFormsRootDesigner = null;
				IDesignerHost designerHost = (IDesignerHost)context.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					IComponent rootComponent = designerHost.RootComponent;
					if (rootComponent != null)
					{
						webFormsRootDesigner = (designerHost.GetDesigner(rootComponent) as WebFormsRootDesigner);
					}
				}
				if (webFormsRootDesigner != null && !webFormsRootDesigner.IsDesignerViewLocked)
				{
					IComponent component = context.Instance as IComponent;
					if (component == null)
					{
						DesignerActionList designerActionList = context.Instance as DesignerActionList;
						if (designerActionList != null)
						{
							component = designerActionList.Component;
						}
					}
					IList<IComponent> allComponents = ControlHelper.GetAllComponents(component, new ControlHelper.IsValidComponentDelegate(this.IsValidDataSource));
					List<string> list = new List<string>();
					foreach (IComponent component2 in allComponents)
					{
						Control control = component2 as Control;
						if (control != null && !string.IsNullOrEmpty(control.ID) && !list.Contains(control.ID))
						{
							list.Add(control.ID);
						}
					}
					list.Sort(StringComparer.OrdinalIgnoreCase);
					list.Insert(0, SR.GetString("DataSourceIDChromeConverter_NoDataSource"));
					list.Add(SR.GetString("DataSourceIDChromeConverter_NewDataSource"));
					values = list.ToArray();
				}
			}
			return new TypeConverter.StandardValuesCollection(values);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000210D4 File Offset: 0x0001F2D4
		protected virtual bool IsValidDataSource(IComponent component)
		{
			Control control = component as Control;
			return control != null && !string.IsNullOrEmpty(control.ID) && component is IDataSource;
		}
	}
}
