using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000F32 RID: 3890
	[XmlRoot("ToggleList")]
	[ParseChildren(ChildrenAsProperties = true)]
	public class RibbonBarToggleList : RibbonBarItem, IXmlSerializable
	{
		// Token: 0x17002EE0 RID: 12000
		// (get) Token: 0x0600943B RID: 37947 RVA: 0x00213C93 File Offset: 0x00211E93
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RibbonBarToggleButtonCollection ToggleButtons
		{
			get
			{
				if (this._buttons == null)
				{
					this._buttons = new RibbonBarToggleButtonCollection();
					this._buttons.Container = this;
				}
				return this._buttons;
			}
		}

		// Token: 0x0600943C RID: 37948 RVA: 0x00213CBC File Offset: 0x00211EBC
		public List<RibbonBarToggleButton> GetVisibleButtons()
		{
			List<RibbonBarToggleButton> list = new List<RibbonBarToggleButton>();
			foreach (RibbonBarToggleButton ribbonBarToggleButton in this.ToggleButtons)
			{
				if (ribbonBarToggleButton.Visible)
				{
					list.Add(ribbonBarToggleButton);
				}
			}
			return list;
		}

		// Token: 0x17002EE1 RID: 12001
		// (get) Token: 0x0600943D RID: 37949 RVA: 0x00213D20 File Offset: 0x00211F20
		public RibbonBarToggleButton ToggledButton
		{
			get
			{
				RibbonBarToggleButton result = null;
				foreach (RibbonBarToggleButton ribbonBarToggleButton in this.ToggleButtons)
				{
					if (ribbonBarToggleButton.Toggled)
					{
						result = ribbonBarToggleButton;
					}
				}
				return result;
			}
		}

		// Token: 0x17002EE2 RID: 12002
		// (get) Token: 0x0600943E RID: 37950 RVA: 0x00213D7C File Offset: 0x00211F7C
		// (set) Token: 0x0600943F RID: 37951 RVA: 0x00213D84 File Offset: 0x00211F84
		public override WebControl ParentWebControl
		{
			get
			{
				return this._parentWebControl;
			}
			internal set
			{
				this._parentWebControl = value;
				if (!this._parentWebControl.Controls.Contains(this))
				{
					this._parentWebControl.Controls.Add(this);
				}
				this.ToggleButtons.ParentWebControl = this;
			}
		}

		// Token: 0x06009440 RID: 37952 RVA: 0x00213DC0 File Offset: 0x00211FC0
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			bool flag = false;
			foreach (RibbonBarToggleButton ribbonBarToggleButton in this.ToggleButtons)
			{
				if (flag)
				{
					ribbonBarToggleButton.Toggled = false;
				}
				else
				{
					flag = ribbonBarToggleButton.Toggled;
					ribbonBarToggleButton.Enabled = (this.Enabled && ribbonBarToggleButton.Enabled);
				}
			}
		}

		// Token: 0x06009441 RID: 37953 RVA: 0x00213E40 File Offset: 0x00212040
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
		}

		// Token: 0x06009442 RID: 37954 RVA: 0x00213E42 File Offset: 0x00212042
		public override void RenderEndTag(HtmlTextWriter writer)
		{
		}

		// Token: 0x17002EE3 RID: 12003
		// (get) Token: 0x06009443 RID: 37955 RVA: 0x00213E44 File Offset: 0x00212044
		public override RibbonBarItemType ItemType
		{
			get
			{
				return RibbonBarItemType.ToggleList;
			}
		}

		// Token: 0x06009444 RID: 37956 RVA: 0x00213E47 File Offset: 0x00212047
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06009445 RID: 37957 RVA: 0x00213E53 File Offset: 0x00212053
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x06009446 RID: 37958 RVA: 0x00213E5C File Offset: 0x0021205C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x06009447 RID: 37959 RVA: 0x00213E65 File Offset: 0x00212065
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			this.ReadXmlForToggleButtons(reader);
		}

		// Token: 0x06009448 RID: 37960 RVA: 0x00213E7C File Offset: 0x0021207C
		protected virtual void ReadXmlForToggleButtons(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "ToggleList")
				{
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name != "ToggleList" && reader.Name != "ToggleButton")
				{
					return;
				}
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarToggleButton));
					RibbonBarToggleButton button = (RibbonBarToggleButton)xmlSerializer.Deserialize(reader);
					this.ToggleButtons.Add(button);
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x06009449 RID: 37961 RVA: 0x00213F27 File Offset: 0x00212127
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForToggleButtons(writer);
		}

		// Token: 0x0600944A RID: 37962 RVA: 0x00213F44 File Offset: 0x00212144
		protected virtual void WriteXmlForToggleButtons(XmlWriter writer)
		{
			foreach (RibbonBarToggleButton ribbonBarToggleButton in this.ToggleButtons)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(ribbonBarToggleButton.GetType());
				xmlSerializer.Serialize(writer, ribbonBarToggleButton);
			}
		}

		// Token: 0x04002A75 RID: 10869
		private RibbonBarToggleButtonCollection _buttons;

		// Token: 0x04002A76 RID: 10870
		private WebControl _parentWebControl;
	}
}
