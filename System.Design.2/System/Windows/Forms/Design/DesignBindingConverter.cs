using System;
using System.Collections;
using System.ComponentModel;
using System.Design;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002CC RID: 716
	internal class DesignBindingConverter : TypeConverter
	{
		// Token: 0x06001C4B RID: 7243 RVA: 0x000AA4D2 File Offset: 0x000A86D2
		public override bool CanConvertTo(ITypeDescriptorContext context, Type sourceType)
		{
			return typeof(string) == sourceType;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x000AA4D2 File Offset: 0x000A86D2
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type destType)
		{
			return typeof(string) == destType;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000AA4E4 File Offset: 0x000A86E4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type sourceType)
		{
			DesignBinding designBinding = (DesignBinding)value;
			if (designBinding.IsNull)
			{
				return SR.GetString("DataGridNoneString");
			}
			string text = "";
			if (designBinding.DataSource is IComponent)
			{
				IComponent component = (IComponent)designBinding.DataSource;
				if (component.Site != null)
				{
					text = component.Site.Name;
				}
			}
			if (text.Length == 0)
			{
				if (designBinding.DataSource is IListSource || designBinding.DataSource is IList || designBinding.DataSource is Array)
				{
					text = "(List)";
				}
				else
				{
					string text2 = TypeDescriptor.GetClassName(designBinding.DataSource);
					int num = text2.LastIndexOf('.');
					if (num != -1)
					{
						text2 = text2.Substring(num + 1);
					}
					text = string.Format(CultureInfo.CurrentCulture, "({0})", new object[]
					{
						text2
					});
				}
			}
			return text + " - " + designBinding.DataMember;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000AA5C8 File Offset: 0x000A87C8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = (string)value;
			if (text == null || text.Length == 0 || string.Compare(text, SR.GetString("DataGridNoneString"), true, CultureInfo.CurrentCulture) == 0)
			{
				return DesignBinding.Null;
			}
			int num = text.IndexOf("-");
			if (num == -1)
			{
				throw new ArgumentException(SR.GetString("DesignBindingBadParseString", new object[]
				{
					text
				}));
			}
			string text2 = text.Substring(0, num - 1).Trim();
			string dataMember = text.Substring(num + 1).Trim();
			if (context == null || context.Container == null)
			{
				throw new ArgumentException(SR.GetString("DesignBindingContextRequiredWhenParsing", new object[]
				{
					text
				}));
			}
			IContainer container = DesignerUtils.CheckForNestedContainer(context.Container);
			IComponent component = container.Components[text2];
			if (component != null)
			{
				return new DesignBinding(component, dataMember);
			}
			if (string.Equals(text2, "(List)", StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			throw new ArgumentException(SR.GetString("DesignBindingComponentNotFound", new object[]
			{
				text2
			}));
		}
	}
}
