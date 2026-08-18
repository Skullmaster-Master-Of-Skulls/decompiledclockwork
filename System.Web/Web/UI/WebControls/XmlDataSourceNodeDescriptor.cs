using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Xml.XPath;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200068F RID: 1679
	internal sealed class XmlDataSourceNodeDescriptor : ICustomTypeDescriptor, IXPathNavigable
	{
		// Token: 0x0600525C RID: 21084 RVA: 0x0014CBF9 File Offset: 0x0014BBF9
		public XmlDataSourceNodeDescriptor(XmlNode node)
		{
			this._node = node;
		}

		// Token: 0x0600525D RID: 21085 RVA: 0x0014CC08 File Offset: 0x0014BC08
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x0014CC0F File Offset: 0x0014BC0F
		string ICustomTypeDescriptor.GetClassName()
		{
			return base.GetType().Name;
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x0014CC1C File Offset: 0x0014BC1C
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x0014CC1F File Offset: 0x0014BC1F
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x0014CC22 File Offset: 0x0014BC22
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06005262 RID: 21090 RVA: 0x0014CC25 File Offset: 0x0014BC25
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x0014CC28 File Offset: 0x0014BC28
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x0014CC2B File Offset: 0x0014BC2B
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return null;
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x0014CC2E File Offset: 0x0014BC2E
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attrs)
		{
			return null;
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x0014CC31 File Offset: 0x0014BC31
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x0014CC3C File Offset: 0x0014BC3C
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attrFilter)
		{
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			XmlAttributeCollection attributes = this._node.Attributes;
			if (attributes != null)
			{
				for (int i = 0; i < attributes.Count; i++)
				{
					list.Add(new XmlDataSourceNodeDescriptor.XmlDataSourcePropertyDescriptor(attributes[i].Name));
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x0014CC91 File Offset: 0x0014BC91
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			if (pd is XmlDataSourceNodeDescriptor.XmlDataSourcePropertyDescriptor)
			{
				return this;
			}
			return null;
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x0014CC9E File Offset: 0x0014BC9E
		XPathNavigator IXPathNavigable.CreateNavigator()
		{
			return this._node.CreateNavigator();
		}

		// Token: 0x04002DF6 RID: 11766
		private XmlNode _node;

		// Token: 0x02000690 RID: 1680
		private class XmlDataSourcePropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x0600526A RID: 21098 RVA: 0x0014CCAB File Offset: 0x0014BCAB
			public XmlDataSourcePropertyDescriptor(string name) : base(name, null)
			{
				this._name = name;
			}

			// Token: 0x170014F8 RID: 5368
			// (get) Token: 0x0600526B RID: 21099 RVA: 0x0014CCBC File Offset: 0x0014BCBC
			public override Type ComponentType
			{
				get
				{
					return typeof(XmlDataSourceNodeDescriptor);
				}
			}

			// Token: 0x170014F9 RID: 5369
			// (get) Token: 0x0600526C RID: 21100 RVA: 0x0014CCC8 File Offset: 0x0014BCC8
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170014FA RID: 5370
			// (get) Token: 0x0600526D RID: 21101 RVA: 0x0014CCCB File Offset: 0x0014BCCB
			public override Type PropertyType
			{
				get
				{
					return typeof(string);
				}
			}

			// Token: 0x0600526E RID: 21102 RVA: 0x0014CCD7 File Offset: 0x0014BCD7
			public override bool CanResetValue(object o)
			{
				return false;
			}

			// Token: 0x0600526F RID: 21103 RVA: 0x0014CCDC File Offset: 0x0014BCDC
			public override object GetValue(object o)
			{
				XmlDataSourceNodeDescriptor xmlDataSourceNodeDescriptor = o as XmlDataSourceNodeDescriptor;
				if (xmlDataSourceNodeDescriptor != null)
				{
					XmlAttributeCollection attributes = xmlDataSourceNodeDescriptor._node.Attributes;
					if (attributes != null)
					{
						XmlAttribute xmlAttribute = attributes[this._name];
						if (xmlAttribute != null)
						{
							return xmlAttribute.Value;
						}
					}
				}
				return string.Empty;
			}

			// Token: 0x06005270 RID: 21104 RVA: 0x0014CD1E File Offset: 0x0014BD1E
			public override void ResetValue(object o)
			{
			}

			// Token: 0x06005271 RID: 21105 RVA: 0x0014CD20 File Offset: 0x0014BD20
			public override void SetValue(object o, object value)
			{
			}

			// Token: 0x06005272 RID: 21106 RVA: 0x0014CD22 File Offset: 0x0014BD22
			public override bool ShouldSerializeValue(object o)
			{
				return true;
			}

			// Token: 0x04002DF7 RID: 11767
			private string _name;
		}
	}
}
