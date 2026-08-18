using System;
using System.Collections;
using System.ComponentModel;
using System.Design;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000FE RID: 254
	internal static class RenderOuterTableHelper
	{
		// Token: 0x060008E6 RID: 2278 RVA: 0x00033D28 File Offset: 0x00031F28
		internal static bool IsAnyPropertyOnOuterTableChanged(IComponent component, bool isFormView)
		{
			if (isFormView)
			{
				return RenderOuterTableHelper.IsAnyPropertyOnOuterTableChangedHelper(component, RenderOuterTableHelper.formViewStylePropertiesOnOuterTable) || RenderOuterTableHelper.IsAnyPropertyOnOuterTableChangedHelper(((FormView)component).Font, RenderOuterTableHelper.fontStyleProperties);
			}
			return RenderOuterTableHelper.IsAnyPropertyOnOuterTableChangedHelper(component, RenderOuterTableHelper.loginStylePropertiesOnOuterTable);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00033D60 File Offset: 0x00031F60
		private static bool IsAnyPropertyOnOuterTableChangedHelper(object component, string[] propertiesOnOuterTable)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
			foreach (string name in propertiesOnOuterTable)
			{
				PropertyDescriptor propertyDescriptor = properties[name];
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)propertyDescriptor.Attributes[typeof(DefaultValueAttribute)];
				if (defaultValueAttribute != null && !object.Equals(defaultValueAttribute.Value, propertyDescriptor.GetValue(component)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00033DCC File Offset: 0x00031FCC
		internal static void SetRenderOuterTable(bool value, ControlDesigner designer, bool isFormView)
		{
			IComponent component = designer.Component;
			IRenderOuterTableControl control = (IRenderOuterTableControl)component;
			if (value != control.RenderOuterTable)
			{
				if (!value && RenderOuterTableHelper.IsAnyPropertyOnOuterTableChanged(component, isFormView))
				{
					DialogResult dialogResult = UIServiceHelper.ShowMessage(component.Site, SR.GetString("RenderOuterTable_RemoveOuterTableWarning"), SR.GetString("RenderOuterTable_RemoveOuterTableCaption", new object[]
					{
						control.GetType().Name,
						control.ID
					}), MessageBoxButtons.YesNo);
					if (dialogResult == DialogResult.No)
					{
						return;
					}
					ControlDesigner.InvokeTransactedChange(component, delegate(object context)
					{
						bool result;
						try
						{
							PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
							string[] array = isFormView ? RenderOuterTableHelper.formViewStylePropertiesOnOuterTable : RenderOuterTableHelper.loginStylePropertiesOnOuterTable;
							if (isFormView)
							{
								((FormView)control).ControlStyle.Reset();
							}
							foreach (string name in array)
							{
								PropertyDescriptor propertyDescriptor = properties[name];
								propertyDescriptor.ResetValue(component);
							}
							result = true;
						}
						catch (Exception ex)
						{
							result = false;
						}
						return result;
					}, null, SR.GetString("RenderOuterTableHelper_ResetProperties"));
				}
				control.RenderOuterTable = value;
				TypeDescriptor.Refresh(component);
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00033EB8 File Offset: 0x000320B8
		internal static void SetupRenderOuterTable(IDictionary properties, IComponent component, bool useFormViewStyleProperties, Type designerType)
		{
			if (properties["RenderOuterTable"] != null)
			{
				if (!((IRenderOuterTableControl)component).RenderOuterTable)
				{
					string[] array;
					if (useFormViewStyleProperties)
					{
						array = RenderOuterTableHelper.formViewStylePropertiesOnOuterTable;
					}
					else
					{
						array = RenderOuterTableHelper.loginStylePropertiesOnOuterTable;
					}
					foreach (string key in array)
					{
						PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[key];
						properties[key] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
						{
							BrowsableAttribute.No
						});
					}
				}
				PropertyDescriptor oldPropertyDescriptor = (PropertyDescriptor)properties["RenderOuterTable"];
				properties["RenderOuterTable"] = TypeDescriptor.CreateProperty(designerType, oldPropertyDescriptor, new Attribute[]
				{
					RefreshPropertiesAttribute.All
				});
			}
		}

		// Token: 0x0400054E RID: 1358
		internal static readonly string[] fontStyleProperties = new string[]
		{
			"Bold",
			"Italic",
			"Name",
			"Names",
			"Overline",
			"Size",
			"Strikeout",
			"Underline"
		};

		// Token: 0x0400054F RID: 1359
		internal static readonly string[] formViewStylePropertiesOnOuterTable = new string[]
		{
			"BackImageUrl",
			"CellPadding",
			"CellSpacing",
			"GridLines",
			"HorizontalAlign",
			"BackColor",
			"BorderColor",
			"BorderWidth",
			"BorderStyle",
			"CssClass",
			"Font",
			"ForeColor",
			"Height",
			"Width"
		};

		// Token: 0x04000550 RID: 1360
		internal static readonly string[] loginStylePropertiesOnOuterTable = new string[]
		{
			"BorderPadding",
			"BackColor",
			"BorderColor",
			"BorderWidth",
			"BorderStyle",
			"CssClass",
			"ForeColor",
			"Height",
			"Width"
		};
	}
}
