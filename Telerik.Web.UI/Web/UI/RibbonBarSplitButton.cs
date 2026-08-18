using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000F39 RID: 3897
	[XmlRoot("SplitButton")]
	[ParseChildren(ChildrenAsProperties = true)]
	public class RibbonBarSplitButton : RibbonBarMenuBaseItem, IXmlSerializable
	{
		// Token: 0x17002F02 RID: 12034
		// (get) Token: 0x0600949B RID: 38043 RVA: 0x00214568 File Offset: 0x00212768
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

		// Token: 0x0600949C RID: 38044 RVA: 0x00214590 File Offset: 0x00212790
		public IList<RibbonBarButton> GetVisibleButtons()
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

		// Token: 0x0600949D RID: 38045 RVA: 0x002145F4 File Offset: 0x002127F4
		public RibbonBarButton FindButtonByValue(string value)
		{
			foreach (RibbonBarButton ribbonBarButton in this.Buttons)
			{
				if (ribbonBarButton.Value.Equals(value))
				{
					return ribbonBarButton;
				}
			}
			return null;
		}

		// Token: 0x17002F03 RID: 12035
		// (get) Token: 0x0600949E RID: 38046 RVA: 0x00214658 File Offset: 0x00212858
		public override RibbonBarItemType ItemType
		{
			get
			{
				return RibbonBarItemType.SplitButton;
			}
		}

		// Token: 0x17002F04 RID: 12036
		// (get) Token: 0x0600949F RID: 38047 RVA: 0x0021465B File Offset: 0x0021285B
		// (set) Token: 0x060094A0 RID: 38048 RVA: 0x0021467C File Offset: 0x0021287C
		[DefaultValue(false)]
		public bool EnableButtonSelection
		{
			get
			{
				return (bool)(this.ViewState["EnableButtonSelection"] ?? false);
			}
			set
			{
				this.ViewState["EnableButtonSelection"] = value;
			}
		}

		// Token: 0x17002F05 RID: 12037
		// (get) Token: 0x060094A1 RID: 38049 RVA: 0x00214694 File Offset: 0x00212894
		// (set) Token: 0x060094A2 RID: 38050 RVA: 0x002146B5 File Offset: 0x002128B5
		[DefaultValue(-1)]
		public int SelectedButtonIndex
		{
			get
			{
				return (int)(this.ViewState["SelectedButtonIndex"] ?? -1);
			}
			set
			{
				this.ViewState["SelectedButtonIndex"] = value;
			}
		}

		// Token: 0x060094A3 RID: 38051 RVA: 0x002146CD File Offset: 0x002128CD
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarSplitButtonLiteRenderer(this);
			}
			return new RibbonBarSplitButtonClassicRenderer(this);
		}

		// Token: 0x17002F06 RID: 12038
		// (get) Token: 0x060094A4 RID: 38052 RVA: 0x002146EA File Offset: 0x002128EA
		// (set) Token: 0x060094A5 RID: 38053 RVA: 0x002146F2 File Offset: 0x002128F2
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

		// Token: 0x060094A6 RID: 38054 RVA: 0x0021472B File Offset: 0x0021292B
		internal bool IsValidButtonIndex(int index)
		{
			return index > -1 && index < this.Buttons.Count;
		}

		// Token: 0x17002F07 RID: 12039
		// (get) Token: 0x060094A7 RID: 38055 RVA: 0x00214744 File Offset: 0x00212944
		internal int ResolvedSelectedButtonIndex
		{
			get
			{
				int num = this.SelectedButtonIndex;
				if (num == -1)
				{
					num = ((this.Buttons.Count > 0) ? 0 : -1);
				}
				return num;
			}
		}

		// Token: 0x17002F08 RID: 12040
		// (get) Token: 0x060094A8 RID: 38056 RVA: 0x00214770 File Offset: 0x00212970
		internal RibbonBarButton SelectedButton
		{
			get
			{
				if (!this.IsValidButtonIndex(this.ResolvedSelectedButtonIndex))
				{
					return null;
				}
				return this.Buttons[this.ResolvedSelectedButtonIndex];
			}
		}

		// Token: 0x17002F09 RID: 12041
		// (get) Token: 0x060094A9 RID: 38057 RVA: 0x00214793 File Offset: 0x00212993
		internal override string RibbonBarItemTypeCssClass
		{
			get
			{
				return "rrbSplitButton";
			}
		}

		// Token: 0x17002F0A RID: 12042
		// (get) Token: 0x060094AA RID: 38058 RVA: 0x0021479C File Offset: 0x0021299C
		internal string CurrentImageUrl
		{
			get
			{
				string text = base.ImageUrl;
				bool flag = this.SelectedButton != null;
				if (flag)
				{
					text = (string.IsNullOrEmpty(text) ? this.SelectedButton.ImageUrl : text);
				}
				return text;
			}
		}

		// Token: 0x17002F0B RID: 12043
		// (get) Token: 0x060094AB RID: 38059 RVA: 0x002147D8 File Offset: 0x002129D8
		internal string CurrentDisabledImageUrl
		{
			get
			{
				string text = base.DisabledImageUrl;
				bool flag = this.SelectedButton != null;
				if (flag)
				{
					text = (string.IsNullOrEmpty(text) ? this.SelectedButton.DisabledImageUrl : text);
				}
				return text;
			}
		}

		// Token: 0x17002F0C RID: 12044
		// (get) Token: 0x060094AC RID: 38060 RVA: 0x00214814 File Offset: 0x00212A14
		internal string CurrentImageUrlLarge
		{
			get
			{
				string text = base.ImageUrlLarge;
				bool flag = this.SelectedButton != null;
				if (flag)
				{
					text = (string.IsNullOrEmpty(text) ? this.SelectedButton.ImageUrlLarge : text);
				}
				return text;
			}
		}

		// Token: 0x17002F0D RID: 12045
		// (get) Token: 0x060094AD RID: 38061 RVA: 0x00214850 File Offset: 0x00212A50
		internal string CurrentDisabledImageUrlLarge
		{
			get
			{
				string text = base.DisabledImageUrlLarge;
				bool flag = this.SelectedButton != null;
				if (flag)
				{
					text = (string.IsNullOrEmpty(text) ? this.SelectedButton.DisabledImageUrlLarge : text);
				}
				return text;
			}
		}

		// Token: 0x17002F0E RID: 12046
		// (get) Token: 0x060094AE RID: 38062 RVA: 0x0021488C File Offset: 0x00212A8C
		private RibbonBarImageRenderingMode GetDropDownImageRenderingModeResolved
		{
			get
			{
				RibbonBarImageRenderingMode result = RibbonBarImageRenderingMode.Dual;
				foreach (RibbonBarButton ribbonBarButton in this.Buttons)
				{
					if (ribbonBarButton.ImageRenderingMode != RibbonBarImageRenderingMode.Clip)
					{
						result = RibbonBarImageRenderingMode.Dual;
						break;
					}
					result = RibbonBarImageRenderingMode.Clip;
				}
				return result;
			}
		}

		// Token: 0x17002F0F RID: 12047
		// (get) Token: 0x060094AF RID: 38063 RVA: 0x002148EC File Offset: 0x00212AEC
		private RibbonBarImageRenderingMode GetSplitButtonImageRenderingModeResolved
		{
			get
			{
				RibbonBarImageRenderingMode result = RibbonBarImageRenderingMode.Dual;
				if (string.IsNullOrEmpty(base.ImageUrlLarge) && string.IsNullOrEmpty(base.DisabledImageUrlLarge))
				{
					if (string.IsNullOrEmpty(base.ImageUrl) && string.IsNullOrEmpty(base.DisabledImageUrl))
					{
						result = RibbonBarImageRenderingMode.Dual;
					}
					else
					{
						result = RibbonBarImageRenderingMode.Clip;
					}
				}
				return result;
			}
		}

		// Token: 0x17002F10 RID: 12048
		// (get) Token: 0x060094B0 RID: 38064 RVA: 0x00214938 File Offset: 0x00212B38
		private RibbonBarImageRenderingMode ImageRenderingModeResolved
		{
			get
			{
				RibbonBarImageRenderingMode result;
				if (this.Buttons.Count > 0)
				{
					if (this.GetSplitButtonImageRenderingModeResolved == RibbonBarImageRenderingMode.Dual || this.GetDropDownImageRenderingModeResolved == RibbonBarImageRenderingMode.Dual)
					{
						result = RibbonBarImageRenderingMode.Dual;
					}
					else
					{
						result = RibbonBarImageRenderingMode.Clip;
					}
				}
				else
				{
					result = this.GetSplitButtonImageRenderingModeResolved;
				}
				if (this.Buttons.Count > 0 && string.IsNullOrEmpty(base.ImageUrlLarge) && string.IsNullOrEmpty(base.DisabledImageUrlLarge) && string.IsNullOrEmpty(base.ImageUrl) && string.IsNullOrEmpty(base.DisabledImageUrl))
				{
					result = this.GetDropDownImageRenderingModeResolved;
				}
				return result;
			}
		}

		// Token: 0x17002F11 RID: 12049
		// (get) Token: 0x060094B1 RID: 38065 RVA: 0x002149C0 File Offset: 0x00212BC0
		// (set) Token: 0x060094B2 RID: 38066 RVA: 0x00214A16 File Offset: 0x00212C16
		public override RibbonBarImageRenderingMode ImageRenderingMode
		{
			get
			{
				if (this.ViewState["ImageRenderingMode"] != null)
				{
					return (RibbonBarImageRenderingMode)this.ViewState["ImageRenderingMode"];
				}
				RibbonBarImageRenderingMode result = base.RibbonBar.ImageRenderingMode;
				if (base.RibbonBar.ImageRenderingMode == RibbonBarImageRenderingMode.Auto)
				{
					result = this.ImageRenderingModeResolved;
				}
				return result;
			}
			set
			{
				this.ViewState["ImageRenderingMode"] = value;
			}
		}

		// Token: 0x060094B3 RID: 38067 RVA: 0x00214A2E File Offset: 0x00212C2E
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x060094B4 RID: 38068 RVA: 0x00214A3A File Offset: 0x00212C3A
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x060094B5 RID: 38069 RVA: 0x00214A43 File Offset: 0x00212C43
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer);
		}

		// Token: 0x060094B6 RID: 38070 RVA: 0x00214A4C File Offset: 0x00212C4C
		public void ReadXml(XmlReader reader)
		{
			XmlPersister.Deserialize(this, base.Attributes, null, reader);
			this.ReadXmlForSplitButtons(reader);
		}

		// Token: 0x060094B7 RID: 38071 RVA: 0x00214A64 File Offset: 0x00212C64
		protected virtual void ReadXmlForSplitButtons(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "SplitButton")
				{
					return;
				}
				if (reader.NodeType == XmlNodeType.Element && reader.Name != "SplitButton" && reader.Name != "Button")
				{
					return;
				}
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(RibbonBarButton));
					RibbonBarButton button = (RibbonBarButton)xmlSerializer.Deserialize(reader);
					this.Buttons.Add(button);
					reader.MoveToContent();
				}
			}
		}

		// Token: 0x060094B8 RID: 38072 RVA: 0x00214B0F File Offset: 0x00212D0F
		protected void WriteXml(XmlWriter writer)
		{
			XmlPersister.SerializePropertiesAsAttributes(this, writer);
			XmlPersister.SerializeAttributeCollectionAsAttributes(base.Attributes, writer);
			this.WriteXmlForSplitButtons(writer);
		}

		// Token: 0x060094B9 RID: 38073 RVA: 0x00214B2C File Offset: 0x00212D2C
		protected virtual void WriteXmlForSplitButtons(XmlWriter writer)
		{
			foreach (RibbonBarButton ribbonBarButton in this.Buttons)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(ribbonBarButton.GetType());
				xmlSerializer.Serialize(writer, ribbonBarButton);
			}
		}

		// Token: 0x04002A90 RID: 10896
		private WebControl _parentWebControl;

		// Token: 0x04002A91 RID: 10897
		private RibbonBarButtonCollection _buttons;
	}
}
