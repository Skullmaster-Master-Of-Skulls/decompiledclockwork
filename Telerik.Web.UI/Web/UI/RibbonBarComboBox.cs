using System;
using System.ComponentModel;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000E5A RID: 3674
	[XmlRoot("ComboBox")]
	[ParseChildren(ChildrenAsProperties = true)]
	public class RibbonBarComboBox : RibbonBarDropDown, IXmlSerializable
	{
		// Token: 0x17002C07 RID: 11271
		// (get) Token: 0x06008B4F RID: 35663 RVA: 0x001FB2CC File Offset: 0x001F94CC
		internal override string ItemCssClass
		{
			get
			{
				return "rrbComboBox";
			}
		}

		// Token: 0x17002C08 RID: 11272
		// (get) Token: 0x06008B50 RID: 35664 RVA: 0x001FB2D3 File Offset: 0x001F94D3
		internal override string InnerCssClass
		{
			get
			{
				return "rrbCBInner";
			}
		}

		// Token: 0x17002C09 RID: 11273
		// (get) Token: 0x06008B51 RID: 35665 RVA: 0x001FB2DA File Offset: 0x001F94DA
		internal override string InputCssClass
		{
			get
			{
				return "rrbCBInput radPreventDecorate";
			}
		}

		// Token: 0x17002C0A RID: 11274
		// (get) Token: 0x06008B52 RID: 35666 RVA: 0x001FB2E1 File Offset: 0x001F94E1
		public override RibbonBarItemType ItemType
		{
			get
			{
				return RibbonBarItemType.ComboBox;
			}
		}

		// Token: 0x06008B53 RID: 35667 RVA: 0x001FB2E4 File Offset: 0x001F94E4
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarComboBoxLiteRenderer(this);
			}
			return new RibbonBarComboBoxClassicRenderer(this);
		}

		// Token: 0x17002C0B RID: 11275
		// (get) Token: 0x06008B54 RID: 35668 RVA: 0x001FB304 File Offset: 0x001F9504
		// (set) Token: 0x06008B55 RID: 35669 RVA: 0x001FB350 File Offset: 0x001F9550
		[DefaultValue("")]
		public string Text
		{
			get
			{
				string result;
				if (base.SelectedIndex != -1)
				{
					result = base.Items[base.SelectedIndex].Text;
				}
				else
				{
					result = (string)this.ViewState["Text"];
				}
				return result;
			}
			set
			{
				this.ViewState["Text"] = value;
				base.SelectedIndex = -1;
			}
		}

		// Token: 0x06008B56 RID: 35670 RVA: 0x001FB36C File Offset: 0x001F956C
		protected override void ReadXmlForListItems(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "ComboBox")
				{
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name != "ComboBox" && reader.Name != "ListItem")
				{
					return;
				}
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarListItem));
					RibbonBarListItem item = (RibbonBarListItem)xmlSerializer.Deserialize(reader);
					base.Items.Add(item);
					reader.MoveToContent();
				}
			}
		}
	}
}
