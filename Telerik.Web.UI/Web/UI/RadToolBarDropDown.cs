using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200094A RID: 2378
	[ToolboxItem(false)]
	[DefaultProperty("Text")]
	[XmlRoot("DropDown")]
	public class RadToolBarDropDown : RadToolBarItem, IRadToolBarButtonContainer, IRadToolBarItemContainer, IControlItemContainer
	{
		// Token: 0x06005A9F RID: 23199 RVA: 0x001136D6 File Offset: 0x001118D6
		protected override RadToolBarItem.RendererBase CreateRenderer()
		{
			if (base.ToolBar.ResolvedRenderMode == RenderMode.Classic)
			{
				return new RadToolBarDropDown.DropDownRenderer(this);
			}
			return new RadToolBarDropDown.LiteDropDownRenderer(this);
		}

		// Token: 0x06005AA0 RID: 23200 RVA: 0x001136F3 File Offset: 0x001118F3
		protected override ControlItemCollection CreateChildItemCollection()
		{
			return new RadToolBarButtonCollection(this);
		}

		// Token: 0x17001DEC RID: 7660
		// (get) Token: 0x06005AA1 RID: 23201 RVA: 0x001136FB File Offset: 0x001118FB
		internal override RadToolBarItemType ItemType
		{
			get
			{
				return RadToolBarItemType.DropDown;
			}
		}

		// Token: 0x06005AA2 RID: 23202 RVA: 0x001136FE File Offset: 0x001118FE
		protected override void ReadXml(XmlReader reader)
		{
			base.ReadXml(reader);
			base.ReadXmlForChildren(reader);
		}

		// Token: 0x06005AA3 RID: 23203 RVA: 0x0011370E File Offset: 0x0011190E
		protected override void WriteXml(XmlWriter writer)
		{
			base.WriteXml(writer);
			RadToolBarItem.WriteXmlForChildren(writer, this.Buttons);
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x00113723 File Offset: 0x00111923
		protected internal override void SetItemContainer(ControlItemContainer itemContainer)
		{
			base.SetItemContainer(itemContainer);
			this.Buttons.SetItemContainer(itemContainer);
		}

		// Token: 0x06005AA5 RID: 23205 RVA: 0x00113738 File Offset: 0x00111938
		protected override string GetCurrentImageUrl()
		{
			if (string.IsNullOrEmpty(this.ImageUrl))
			{
				return null;
			}
			if (!this.Enabled && !string.IsNullOrEmpty(this.DisabledImageUrl))
			{
				return base.ResolveClientUrl(this.DisabledImageUrl);
			}
			if (!string.IsNullOrEmpty(this.ImageUrl))
			{
				return base.ResolveClientUrl(this.ImageUrl);
			}
			return null;
		}

		// Token: 0x06005AA6 RID: 23206 RVA: 0x00113791 File Offset: 0x00111991
		protected internal override void LoadFromDictionary(IDictionary<string, object> dictionary)
		{
			base.LoadFromDictionary(dictionary);
			if (dictionary.ContainsKey("expandDirection"))
			{
				this.ExpandDirection = (ToolBarDropDownExpandDirection)((int)dictionary["expandDirection"]);
			}
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x001137BD File Offset: 0x001119BD
		protected override void LoadChildViewState(object viewState)
		{
			if (viewState == null)
			{
				base.Children.Clear();
				return;
			}
			((IStateManager)base.Children).LoadViewState(viewState);
		}

		// Token: 0x06005AA8 RID: 23208 RVA: 0x001137DA File Offset: 0x001119DA
		protected override object SaveChildViewState()
		{
			return ((IStateManager)base.Children).SaveViewState();
		}

		// Token: 0x06005AA9 RID: 23209 RVA: 0x001137E7 File Offset: 0x001119E7
		protected override void TrackChildViewState()
		{
			((IStateManager)base.Children).TrackViewState();
		}

		// Token: 0x06005AAA RID: 23210 RVA: 0x001137F4 File Offset: 0x001119F4
		protected override void SetChildrenDirty()
		{
			base.Children.SetDirty();
		}

		// Token: 0x06005AAB RID: 23211 RVA: 0x00113801 File Offset: 0x00111A01
		public RadToolBarDropDown()
		{
		}

		// Token: 0x06005AAC RID: 23212 RVA: 0x00113809 File Offset: 0x00111A09
		public RadToolBarDropDown(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x17001DED RID: 7661
		// (get) Token: 0x06005AAD RID: 23213 RVA: 0x00113818 File Offset: 0x00111A18
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		RadToolBarItemCollection IRadToolBarItemContainer.Items
		{
			get
			{
				throw new InvalidOperationException("The Items property of the RadToolBarDropDown is not available. User Buttons instead.");
			}
		}

		// Token: 0x17001DEE RID: 7662
		// (get) Token: 0x06005AAE RID: 23214 RVA: 0x00113824 File Offset: 0x00111A24
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		public RadToolBarButtonCollection Buttons
		{
			get
			{
				return (RadToolBarButtonCollection)base.Children;
			}
		}

		// Token: 0x17001DEF RID: 7663
		// (get) Token: 0x06005AAF RID: 23215 RVA: 0x00113831 File Offset: 0x00111A31
		// (set) Token: 0x06005AB0 RID: 23216 RVA: 0x00113852 File Offset: 0x00111A52
		[DefaultValue(ToolBarDropDownExpandDirection.Down)]
		[Description("The expand direction of the drop down.")]
		[Category("Layout")]
		public ToolBarDropDownExpandDirection ExpandDirection
		{
			get
			{
				return (ToolBarDropDownExpandDirection)(this.ViewState["ExpandDirection"] ?? ToolBarDropDownExpandDirection.Down);
			}
			set
			{
				this.ViewState["ExpandDirection"] = value;
			}
		}

		// Token: 0x17001DF0 RID: 7664
		// (get) Token: 0x06005AB1 RID: 23217 RVA: 0x0011386A File Offset: 0x00111A6A
		// (set) Token: 0x06005AB2 RID: 23218 RVA: 0x0011388F File Offset: 0x00111A8F
		[Category("Layout")]
		[Bindable(true)]
		[DefaultValue(typeof(Unit), "")]
		public Unit DropDownWidth
		{
			get
			{
				return (Unit)(this.ViewState["DropDownWidth"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DropDownWidth"] = value;
			}
		}

		// Token: 0x17001DF1 RID: 7665
		// (get) Token: 0x06005AB3 RID: 23219 RVA: 0x001138C4 File Offset: 0x00111AC4
		// (set) Token: 0x06005AB4 RID: 23220 RVA: 0x001138E9 File Offset: 0x00111AE9
		[DefaultValue(typeof(Unit), "")]
		[Bindable(true)]
		[Category("Layout")]
		public Unit DropDownHeight
		{
			get
			{
				return (Unit)(this.ViewState["DropDownHeight"] ?? Unit.Empty);
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DropDownHeight"] = value;
			}
		}

		// Token: 0x17001DF2 RID: 7666
		// (get) Token: 0x06005AB5 RID: 23221 RVA: 0x0011391E File Offset: 0x00111B1E
		// (set) Token: 0x06005AB6 RID: 23222 RVA: 0x0011392A File Offset: 0x00111B2A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Value
		{
			get
			{
				throw new Exception("Value property is not supported by RadToolBarDropDown");
			}
			set
			{
				throw new Exception("Value property is not supported by RadToolBarDropDown");
			}
		}

		// Token: 0x17001DF3 RID: 7667
		// (get) Token: 0x06005AB7 RID: 23223 RVA: 0x00113936 File Offset: 0x00111B36
		ControlItemCollection IControlItemContainer.Items
		{
			get
			{
				return base.Children;
			}
		}

		// Token: 0x0200094B RID: 2379
		private class LiteDropDownRenderer : RadToolBarItem.LiteRenderer
		{
			// Token: 0x17001DF4 RID: 7668
			// (get) Token: 0x06005AB8 RID: 23224 RVA: 0x0011393E File Offset: 0x00111B3E
			// (set) Token: 0x06005AB9 RID: 23225 RVA: 0x00113946 File Offset: 0x00111B46
			private Unit DropDownWidth
			{
				get
				{
					return this._dropDownWidth;
				}
				set
				{
					this._dropDownWidth = value;
				}
			}

			// Token: 0x17001DF5 RID: 7669
			// (get) Token: 0x06005ABA RID: 23226 RVA: 0x0011394F File Offset: 0x00111B4F
			private RadToolBarDropDown DropDown
			{
				get
				{
					if (this._dropDown == null)
					{
						this._dropDown = (RadToolBarDropDown)base.Item;
					}
					return this._dropDown;
				}
			}

			// Token: 0x06005ABB RID: 23227 RVA: 0x00113970 File Offset: 0x00111B70
			public LiteDropDownRenderer(RadToolBarDropDown dropDown) : base(dropDown)
			{
			}

			// Token: 0x06005ABC RID: 23228 RVA: 0x0011398C File Offset: 0x00111B8C
			public override void AddAttributesToRender(HtmlTextWriter writer)
			{
				string text = ToolBarStyles.Combine(new string[]
				{
					"rtbLI",
					"rtbItem",
					this.DropDown.OuterCssClass
				});
				string disabledClasses = base.GetDisabledClasses();
				if (!string.IsNullOrEmpty(disabledClasses))
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						disabledClasses
					});
				}
				base.SetClassName(writer, text);
				base.AddAttributesToRender(writer);
			}

			// Token: 0x06005ABD RID: 23229 RVA: 0x001139F9 File Offset: 0x00111BF9
			public override void RenderContents(HtmlTextWriter writer)
			{
				this.RenderLink(writer);
				if (!this.DropDown.DesignMode)
				{
					base.RenderDropDown(writer, this.DropDown.Buttons);
				}
			}

			// Token: 0x06005ABE RID: 23230 RVA: 0x00113A24 File Offset: 0x00111C24
			private void RenderLink(HtmlTextWriter writer)
			{
				if (!this.DropDown.Width.IsEmpty)
				{
					this.DropDownWidth = this.DropDown.Width;
					this.DropDown.Width = Unit.Empty;
				}
				if (!this.DropDown.Height.IsEmpty && this.DropDown.Height.Type == UnitType.Pixel)
				{
					this.DropDownHeight = (int)this.DropDown.Height.Value - 6;
				}
				if (!this.DropDownWidth.IsEmpty)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.DropDownWidth.ToString());
				}
				if (this.DropDownHeight > 0)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.DropDownHeight + "px");
					writer.AddStyleAttribute("line-height", this.DropDownHeight + "px");
				}
				string text;
				if (this.DropDown.ExpandDirection == ToolBarDropDownExpandDirection.Up)
				{
					text = "rtbExpandUp";
				}
				else
				{
					text = "rtbExpandDown";
				}
				string text2 = ToolBarStyles.Combine(new string[]
				{
					this.DropDown.CssClass,
					"rtbButton",
					"rtbMenuButton",
					base.GetInnerItemElementClass(),
					text
				});
				if ((string.IsNullOrEmpty(this.DropDown.Text) || this.DropDown.ShowText == ToolBarShowPosition.OverFlow) && (!string.IsNullOrEmpty(this.DropDown.ImageUrl) || this.DropDown.EnableImageSpriteResolved) && this.DropDown.ShowImage != ToolBarShowPosition.OverFlow)
				{
					text2 = ToolBarStyles.Combine(new string[]
					{
						text2,
						"rtbIconOnly"
					});
				}
				this.DropDown.CssClass = text2;
				this.DropDown.AddAttributes(writer);
				base.ApplyLinkAttributes(writer);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				this.RenderLinkContent(writer);
				writer.RenderEndTag();
			}

			// Token: 0x06005ABF RID: 23231 RVA: 0x00113C1E File Offset: 0x00111E1E
			private void RenderLinkContent(HtmlTextWriter writer)
			{
				base.RenderImageAndTextElements(writer);
				base.RenderChevron(writer);
			}

			// Token: 0x040015E0 RID: 5600
			private Unit _dropDownWidth = Unit.Empty;

			// Token: 0x040015E1 RID: 5601
			private RadToolBarDropDown _dropDown;

			// Token: 0x040015E2 RID: 5602
			private int DropDownHeight = -1;
		}

		// Token: 0x0200094C RID: 2380
		private class DropDownRenderer : RadToolBarItem.Renderer
		{
			// Token: 0x17001DF6 RID: 7670
			// (get) Token: 0x06005AC0 RID: 23232 RVA: 0x00113C2E File Offset: 0x00111E2E
			// (set) Token: 0x06005AC1 RID: 23233 RVA: 0x00113C36 File Offset: 0x00111E36
			private Unit DropDownWidth
			{
				get
				{
					return this._dropDownWidth;
				}
				set
				{
					this._dropDownWidth = value;
				}
			}

			// Token: 0x17001DF7 RID: 7671
			// (get) Token: 0x06005AC2 RID: 23234 RVA: 0x00113C3F File Offset: 0x00111E3F
			private RadToolBarDropDown DropDown
			{
				get
				{
					if (this._dropDown == null)
					{
						this._dropDown = (RadToolBarDropDown)base.Item;
					}
					return this._dropDown;
				}
			}

			// Token: 0x06005AC3 RID: 23235 RVA: 0x00113C60 File Offset: 0x00111E60
			public DropDownRenderer(RadToolBarDropDown dropDown) : base(dropDown)
			{
			}

			// Token: 0x06005AC4 RID: 23236 RVA: 0x00113C7C File Offset: 0x00111E7C
			public override void AddAttributesToRender(HtmlTextWriter writer)
			{
				string text = ToolBarStyles.Combine(new string[]
				{
					"rtbItem",
					"rtbDropDown",
					this.DropDown.OuterCssClass
				});
				string disabledClasses = base.GetDisabledClasses();
				if (!string.IsNullOrEmpty(disabledClasses))
				{
					text = ToolBarStyles.Combine(new string[]
					{
						text,
						disabledClasses
					});
				}
				base.SetClassName(writer, text);
				base.AddAttributesToRender(writer);
			}

			// Token: 0x06005AC5 RID: 23237 RVA: 0x00113CE9 File Offset: 0x00111EE9
			public override void RenderContents(HtmlTextWriter writer)
			{
				this.RenderLink(writer);
				if (!this.DropDown.DesignMode)
				{
					base.RenderDropDown(writer, this.DropDown.Buttons);
				}
			}

			// Token: 0x06005AC6 RID: 23238 RVA: 0x00113D14 File Offset: 0x00111F14
			private void RenderLink(HtmlTextWriter writer)
			{
				if (!this.DropDown.Width.IsEmpty)
				{
					this.DropDownWidth = this.DropDown.Width;
					this.DropDown.Width = Unit.Empty;
				}
				if (!this.DropDown.Height.IsEmpty && this.DropDown.Height.Type == UnitType.Pixel)
				{
					this.DropDownHeight = (int)this.DropDown.Height.Value - 6;
				}
				string text;
				if (this.DropDown.ExpandDirection == ToolBarDropDownExpandDirection.Up)
				{
					text = "rtbExpandUp";
				}
				else
				{
					text = "rtbExpandDown";
				}
				string text2 = ToolBarStyles.Combine(new string[]
				{
					this.DropDown.CssClass,
					"rtbWrap",
					text
				});
				if (string.IsNullOrEmpty(this.DropDown.Text) && (!string.IsNullOrEmpty(this.DropDown.ImageUrl) || this.DropDown.EnableImageSpriteResolved))
				{
					text2 = ToolBarStyles.Combine(new string[]
					{
						text2,
						"rtbIconOnly"
					});
				}
				this.DropDown.CssClass = text2;
				this.DropDown.AddAttributes(writer);
				base.ApplyLinkAttributes(writer);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				this.RenderLinkContent(writer);
				writer.RenderEndTag();
			}

			// Token: 0x06005AC7 RID: 23239 RVA: 0x00113E6C File Offset: 0x0011206C
			private void RenderLinkContent(HtmlTextWriter writer)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbOut");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbMid");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				if (!this.DropDownWidth.IsEmpty)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.DropDownWidth.ToString());
				}
				if (this.DropDownHeight > 0)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.DropDownHeight + "px");
					writer.AddStyleAttribute("line-height", this.DropDownHeight + "px");
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, base.GetInnerItemElementClass());
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				base.RenderImageAndTextElements(writer);
				base.RenderChevron(writer);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
			}

			// Token: 0x040015E3 RID: 5603
			private Unit _dropDownWidth = Unit.Empty;

			// Token: 0x040015E4 RID: 5604
			private RadToolBarDropDown _dropDown;

			// Token: 0x040015E5 RID: 5605
			private int DropDownHeight = -1;
		}
	}
}
