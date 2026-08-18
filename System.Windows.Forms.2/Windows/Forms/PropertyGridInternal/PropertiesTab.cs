using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms.Design;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000512 RID: 1298
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class PropertiesTab : PropertyTab
	{
		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x060054F9 RID: 21753 RVA: 0x001644C8 File Offset: 0x001626C8
		public override string TabName
		{
			get
			{
				return SR.GetString("PBRSToolTipProperties");
			}
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x060054FA RID: 21754 RVA: 0x001644D4 File Offset: 0x001626D4
		public override string HelpKeyword
		{
			get
			{
				return "vs.properties";
			}
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x001644DC File Offset: 0x001626DC
		public override PropertyDescriptor GetDefaultProperty(object obj)
		{
			PropertyDescriptor propertyDescriptor = base.GetDefaultProperty(obj);
			if (propertyDescriptor == null)
			{
				PropertyDescriptorCollection properties = this.GetProperties(obj);
				if (properties != null)
				{
					for (int i = 0; i < properties.Count; i++)
					{
						if ("Name".Equals(properties[i].Name))
						{
							propertyDescriptor = properties[i];
							break;
						}
					}
				}
			}
			return propertyDescriptor;
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x001427F5 File Offset: 0x001409F5
		public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
		{
			return this.GetProperties(null, component, attributes);
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x00164534 File Offset: 0x00162734
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
		{
			if (attributes == null)
			{
				attributes = new Attribute[]
				{
					BrowsableAttribute.Yes
				};
			}
			if (context == null)
			{
				return TypeDescriptor.GetProperties(component, attributes);
			}
			TypeConverter typeConverter = (context.PropertyDescriptor == null) ? TypeDescriptor.GetConverter(component) : context.PropertyDescriptor.Converter;
			if (typeConverter == null || !typeConverter.GetPropertiesSupported(context))
			{
				return TypeDescriptor.GetProperties(component, attributes);
			}
			return typeConverter.GetProperties(context, component, attributes);
		}
	}
}
