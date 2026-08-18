using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Xml.XPath;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000523 RID: 1315
	internal sealed class XmlDataSourceNodeDescriptor : ICustomTypeDescriptor, IXPathNavigable
	{
		// Token: 0x060042AA RID: 17066 RVA: 0x000D9A96 File Offset: 0x000D7C96
		public XmlDataSourceNodeDescriptor(XmlNode node)
		{
			this._node = node;
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x000D9AA5 File Offset: 0x000D7CA5
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x000A9C25 File Offset: 0x000A7E25
		string ICustomTypeDescriptor.GetClassName()
		{
			return base.GetType().Name;
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x0000298D File Offset: 0x00000B8D
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x0000298D File Offset: 0x00000B8D
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x0000298D File Offset: 0x00000B8D
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x0000298D File Offset: 0x00000B8D
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x0000298D File Offset: 0x00000B8D
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x0000298D File Offset: 0x00000B8D
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return null;
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x0000298D File Offset: 0x00000B8D
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attrs)
		{
			return null;
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x0009ED75 File Offset: 0x0009CF75
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x000D9AAC File Offset: 0x000D7CAC
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

		// Token: 0x060042B6 RID: 17078 RVA: 0x000D9B01 File Offset: 0x000D7D01
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			if (pd is XmlDataSourceNodeDescriptor.XmlDataSourcePropertyDescriptor)
			{
				return this;
			}
			return null;
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x000D9B0E File Offset: 0x000D7D0E
		XPathNavigator IXPathNavigable.CreateNavigator()
		{
			return this._node.CreateNavigator();
		}

		// Token: 0x04002580 RID: 9600
		private XmlNode _node;

		// Token: 0x020009DF RID: 2527
		private class XmlDataSourcePropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x06006CE5 RID: 27877 RVA: 0x00186129 File Offset: 0x00184329
			public XmlDataSourcePropertyDescriptor(string name) : base(name, null)
			{
				this._name = name;
			}

			// Token: 0x17001DF9 RID: 7673
			// (get) Token: 0x06006CE6 RID: 27878 RVA: 0x0018613A File Offset: 0x0018433A
			public override Type ComponentType
			{
				get
				{
					return typeof(XmlDataSourceNodeDescriptor);
				}
			}

			// Token: 0x17001DFA RID: 7674
			// (get) Token: 0x06006CE7 RID: 27879 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001DFB RID: 7675
			// (get) Token: 0x06006CE8 RID: 27880 RVA: 0x00186146 File Offset: 0x00184346
			public override Type PropertyType
			{
				get
				{
					return typeof(string);
				}
			}

			// Token: 0x06006CE9 RID: 27881 RVA: 0x00007722 File Offset: 0x00005922
			public override bool CanResetValue(object o)
			{
				return false;
			}

			// Token: 0x06006CEA RID: 27882 RVA: 0x00186154 File Offset: 0x00184354
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

			// Token: 0x06006CEB RID: 27883 RVA: 0x00006164 File Offset: 0x00004364
			public override void ResetValue(object o)
			{
			}

			// Token: 0x06006CEC RID: 27884 RVA: 0x00006164 File Offset: 0x00004364
			public override void SetValue(object o, object value)
			{
			}

			// Token: 0x06006CED RID: 27885 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool ShouldSerializeValue(object o)
			{
				return true;
			}

			// Token: 0x040039F9 RID: 14841
			private string _name;
		}
	}
}
