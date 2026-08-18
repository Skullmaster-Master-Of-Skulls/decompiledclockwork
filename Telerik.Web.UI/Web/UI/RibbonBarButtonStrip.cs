using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000F37 RID: 3895
	[XmlRoot("ButtonStrip")]
	[ParseChildren(ChildrenAsProperties = true)]
	public class RibbonBarButtonStrip : RibbonBarItem, IXmlSerializable
	{
		// Token: 0x17002EFC RID: 12028
		// (get) Token: 0x0600947F RID: 38015 RVA: 0x00214166 File Offset: 0x00212366
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RibbonBarButtonCollection Buttons
		{
			get
			{
				if (this._buttons == null)
				{
					this._buttons = new RibbonBarButtonCollection();
					this._buttons.Container = this;
				}
				return this._buttons;
			}
		}

		// Token: 0x06009480 RID: 38016 RVA: 0x00214190 File Offset: 0x00212390
		public List<RibbonBarButton> GetVisibleButtons()
		{
			List<RibbonBarButton> list = new List<RibbonBarButton>();
			foreach (RibbonBarButton ribbonBarButton in this.Buttons)
			{
				if (ribbonBarButton.Visible)
				{
					list.Add(ribbonBarButton);
				}
			}
			return list;
		}

		// Token: 0x17002EFD RID: 12029
		// (get) Token: 0x06009481 RID: 38017 RVA: 0x002141F4 File Offset: 0x002123F4
		// (set) Token: 0x06009482 RID: 38018 RVA: 0x002141FC File Offset: 0x002123FC
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
				this.Buttons.ParentWebControl = this;
			}
		}

		// Token: 0x17002EFE RID: 12030
		// (get) Token: 0x06009483 RID: 38019 RVA: 0x00214235 File Offset: 0x00212435
		public override RibbonBarItemType ItemType
		{
			get
			{
				return RibbonBarItemType.ButtonStrip;
			}
		}

		// Token: 0x17002EFF RID: 12031
		// (get) Token: 0x06009484 RID: 38020 RVA: 0x00214238 File Offset: 0x00212438
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return base.Renderer.TagKey;
			}
		}

		// Token: 0x06009485 RID: 38021 RVA: 0x00214245 File Offset: 0x00212445
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarButtonStripLiteRenderer(this);
			}
			return new RibbonBarButtonStripClassicRenderer(this);
		}

		// Token: 0x06009486 RID: 38022 RVA: 0x00214262 File Offset: 0x00212462
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x06009487 RID: 38023 RVA: 0x00214270 File Offset: 0x00212470
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			foreach (RibbonBarButton ribbonBarButton in this.Buttons)
			{
				ribbonBarButton.ImageRenderingMode = RibbonBarImageRenderingMode.Clip;
				ribbonBarButton.Size = RibbonBarItemSize.Small;
				ribbonBarButton.Enabled = (this.Enabled && ribbonBarButton.Enabled);
				ribbonBarButton.ShouldRenderButtonStripClasses = true;
			}
		}

		// Token: 0x06009488 RID: 38024 RVA: 0x002142F0 File Offset: 0x002124F0
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06009489 RID: 38025 RVA: 0x002142FC File Offset: 0x002124FC
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x0600948A RID: 38026 RVA: 0x00214305 File Offset: 0x00212505
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x0600948B RID: 38027 RVA: 0x0021430E File Offset: 0x0021250E
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			this.ReadXmlForButtons(reader);
		}

		// Token: 0x0600948C RID: 38028 RVA: 0x00214328 File Offset: 0x00212528
		protected virtual void ReadXmlForButtons(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "ButtonStrip")
				{
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name != "ButtonStrip" && reader.Name != "Button")
				{
					return;
				}
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					string name;
					if ((name = reader.Name) != null)
					{
						if (!(name == "Button"))
						{
							if (name == "ToggleButton")
							{
								XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarToggleButton));
								RibbonBarToggleButton button = (RibbonBarToggleButton)xmlSerializer.Deserialize(reader);
								this.Buttons.Add(button);
							}
						}
						else
						{
							XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarButton));
							RibbonBarButton button2 = (RibbonBarButton)xmlSerializer.Deserialize(reader);
							this.Buttons.Add(button2);
						}
					}
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x0600948D RID: 38029 RVA: 0x0021442C File Offset: 0x0021262C
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForButtons(writer);
		}

		// Token: 0x0600948E RID: 38030 RVA: 0x00214448 File Offset: 0x00212648
		protected virtual void WriteXmlForButtons(XmlWriter writer)
		{
			foreach (RibbonBarItem ribbonBarItem in this.Buttons)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(ribbonBarItem.GetType());
				xmlSerializer.Serialize(writer, ribbonBarItem);
			}
		}

		// Token: 0x04002A8D RID: 10893
		private RibbonBarButtonCollection _buttons;

		// Token: 0x04002A8E RID: 10894
		private WebControl _parentWebControl;
	}
}
