using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Xml;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000527 RID: 1319
	internal sealed class XmlHierarchyData : IHierarchyData, ICustomTypeDescriptor
	{
		// Token: 0x060042C2 RID: 17090 RVA: 0x000D9C48 File Offset: 0x000D7E48
		internal XmlHierarchyData(XmlHierarchicalEnumerable parent, XmlNode item)
		{
			this._parent = parent;
			this._item = item;
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x000D9C5E File Offset: 0x000D7E5E
		private string CreateRecursivePath(XmlNode node)
		{
			if (node.ParentNode == null)
			{
				return string.Empty;
			}
			return this.CreateRecursivePath(node.ParentNode) + this.FindNodePosition(node);
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x000D9C88 File Offset: 0x000D7E88
		private string FindNodePosition(XmlNode node)
		{
			XmlNodeList childNodes = node.ParentNode.ChildNodes;
			int num = 0;
			for (int i = 0; i < childNodes.Count; i++)
			{
				if (childNodes[i].NodeType == XmlNodeType.Element)
				{
					num++;
				}
				if (childNodes[i] == node)
				{
					return "/*[position()=" + Convert.ToString(num, CultureInfo.InvariantCulture) + "]";
				}
			}
			throw new ArgumentException(SR.GetString("XmlHierarchyData_CouldNotFindNode"));
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x000D9CFB File Offset: 0x000D7EFB
		public override string ToString()
		{
			return this._item.Name;
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x060042C6 RID: 17094 RVA: 0x000D9D08 File Offset: 0x000D7F08
		bool IHierarchyData.HasChildren
		{
			get
			{
				return this._item.HasChildNodes;
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x060042C7 RID: 17095 RVA: 0x000D9D15 File Offset: 0x000D7F15
		object IHierarchyData.Item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x060042C8 RID: 17096 RVA: 0x000D9D20 File Offset: 0x000D7F20
		string IHierarchyData.Path
		{
			get
			{
				if (this._path == null)
				{
					if (this._parent != null)
					{
						if (this._parent.Path == null)
						{
							this._parent.Path = this.CreateRecursivePath(this._item.ParentNode);
						}
						this._path = this._parent.Path + this.FindNodePosition(this._item);
					}
					else
					{
						this._path = this.CreateRecursivePath(this._item);
					}
				}
				return this._path;
			}
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x060042C9 RID: 17097 RVA: 0x000D9CFB File Offset: 0x000D7EFB
		string IHierarchyData.Type
		{
			get
			{
				return this._item.Name;
			}
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x000D9DA2 File Offset: 0x000D7FA2
		IHierarchicalEnumerable IHierarchyData.GetChildren()
		{
			return new XmlHierarchicalEnumerable(this._item.ChildNodes);
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x000D9DB4 File Offset: 0x000D7FB4
		IHierarchyData IHierarchyData.GetParent()
		{
			XmlNode parentNode = this._item.ParentNode;
			if (parentNode == null)
			{
				return null;
			}
			return new XmlHierarchyData(null, parentNode);
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x000D9AA5 File Offset: 0x000D7CA5
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x000A9C25 File Offset: 0x000A7E25
		string ICustomTypeDescriptor.GetClassName()
		{
			return base.GetType().Name;
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x0000298D File Offset: 0x00000B8D
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x0000298D File Offset: 0x00000B8D
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x0000298D File Offset: 0x00000B8D
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x000D9DD9 File Offset: 0x000D7FD9
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor("#Name");
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x0000298D File Offset: 0x00000B8D
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x0000298D File Offset: 0x00000B8D
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return null;
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x0000298D File Offset: 0x00000B8D
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attrs)
		{
			return null;
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x0009ED75 File Offset: 0x0009CF75
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x000D9DE8 File Offset: 0x000D7FE8
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attrFilter)
		{
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			list.Add(new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor("#Name"));
			list.Add(new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor("#Value"));
			list.Add(new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor("#InnerText"));
			XmlAttributeCollection attributes = this._item.Attributes;
			if (attributes != null)
			{
				for (int i = 0; i < attributes.Count; i++)
				{
					list.Add(new XmlHierarchyData.XmlHierarchyDataPropertyDescriptor(attributes[i].Name));
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x000D9E6D File Offset: 0x000D806D
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			if (pd is XmlHierarchyData.XmlHierarchyDataPropertyDescriptor)
			{
				return this;
			}
			return null;
		}

		// Token: 0x04002586 RID: 9606
		private XmlNode _item;

		// Token: 0x04002587 RID: 9607
		private XmlHierarchicalEnumerable _parent;

		// Token: 0x04002588 RID: 9608
		private string _path;

		// Token: 0x020009E2 RID: 2530
		private class XmlHierarchyDataPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x06006CFB RID: 27899 RVA: 0x001863A1 File Offset: 0x001845A1
			public XmlHierarchyDataPropertyDescriptor(string name) : base(name, null)
			{
				this._name = name;
			}

			// Token: 0x17001E01 RID: 7681
			// (get) Token: 0x06006CFC RID: 27900 RVA: 0x001863B2 File Offset: 0x001845B2
			public override Type ComponentType
			{
				get
				{
					return typeof(XmlHierarchyData);
				}
			}

			// Token: 0x17001E02 RID: 7682
			// (get) Token: 0x06006CFD RID: 27901 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001E03 RID: 7683
			// (get) Token: 0x06006CFE RID: 27902 RVA: 0x00186146 File Offset: 0x00184346
			public override Type PropertyType
			{
				get
				{
					return typeof(string);
				}
			}

			// Token: 0x06006CFF RID: 27903 RVA: 0x00007722 File Offset: 0x00005922
			public override bool CanResetValue(object o)
			{
				return false;
			}

			// Token: 0x06006D00 RID: 27904 RVA: 0x001863C0 File Offset: 0x001845C0
			public override object GetValue(object o)
			{
				XmlHierarchyData xmlHierarchyData = o as XmlHierarchyData;
				if (xmlHierarchyData != null)
				{
					string name = this._name;
					if (name == "#Name")
					{
						return xmlHierarchyData._item.Name;
					}
					if (name == "#Value")
					{
						return xmlHierarchyData._item.Value;
					}
					if (name == "#InnerText")
					{
						return xmlHierarchyData._item.InnerText;
					}
					XmlAttributeCollection attributes = xmlHierarchyData._item.Attributes;
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

			// Token: 0x06006D01 RID: 27905 RVA: 0x00006164 File Offset: 0x00004364
			public override void ResetValue(object o)
			{
			}

			// Token: 0x06006D02 RID: 27906 RVA: 0x00006164 File Offset: 0x00004364
			public override void SetValue(object o, object value)
			{
			}

			// Token: 0x06006D03 RID: 27907 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool ShouldSerializeValue(object o)
			{
				return true;
			}

			// Token: 0x04003A00 RID: 14848
			private string _name;
		}
	}
}
