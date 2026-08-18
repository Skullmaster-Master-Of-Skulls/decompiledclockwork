using System;
using System.ComponentModel;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200044D RID: 1101
	[ControlBuilder(typeof(LabelControlBuilder))]
	[ControlValueProperty("Text")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Text")]
	[ParseChildren(false)]
	[Designer("System.Web.UI.Design.WebControls.LabelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxData("<{0}:Label runat=\"server\" Text=\"Label\"></{0}:Label>")]
	public class Label : WebControl, ITextControl
	{
		// Token: 0x06003528 RID: 13608 RVA: 0x000855C3 File Offset: 0x000837C3
		public Label()
		{
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x000AC69C File Offset: 0x000AA89C
		internal Label(HtmlTextWriterTag tag) : base(tag)
		{
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x0600352A RID: 13610 RVA: 0x000AC6A8 File Offset: 0x000AA8A8
		// (set) Token: 0x0600352B RID: 13611 RVA: 0x000AC6D5 File Offset: 0x000AA8D5
		[DefaultValue("")]
		[IDReferenceProperty]
		[TypeConverter(typeof(AssociatedControlConverter))]
		[WebCategory("Accessibility")]
		[WebSysDescription("Label_AssociatedControlID")]
		[Themeable(false)]
		public virtual string AssociatedControlID
		{
			get
			{
				string text = (string)this.ViewState["AssociatedControlID"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["AssociatedControlID"] = value;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x0600352C RID: 13612 RVA: 0x000AC6E8 File Offset: 0x000AA8E8
		// (set) Token: 0x0600352D RID: 13613 RVA: 0x000AC711 File Offset: 0x000AA911
		internal bool AssociatedControlInControlTree
		{
			get
			{
				object obj = this.ViewState["AssociatedControlNotInControlTree"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["AssociatedControlNotInControlTree"] = value;
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool RequiresLegacyRendering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06003530 RID: 13616 RVA: 0x000AC729 File Offset: 0x000AA929
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.AssociatedControlID.Length != 0)
				{
					return HtmlTextWriterTag.Label;
				}
				return base.TagKey;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06003531 RID: 13617 RVA: 0x000AC744 File Offset: 0x000AA944
		// (set) Token: 0x06003532 RID: 13618 RVA: 0x000A9ECD File Offset: 0x000A80CD
		[Localizable(true)]
		[Bindable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("Label_Text")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public virtual string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (this.HasControls())
				{
					this.Controls.Clear();
				}
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x000AC774 File Offset: 0x000AA974
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string associatedControlID = this.AssociatedControlID;
			if (associatedControlID.Length != 0)
			{
				if (this.AssociatedControlInControlTree)
				{
					Control control = this.FindControl(associatedControlID);
					if (control == null)
					{
						if (!base.DesignMode)
						{
							throw new HttpException(SR.GetString("LabelForNotFound", new object[]
							{
								associatedControlID,
								this.ID
							}));
						}
					}
					else
					{
						writer.AddAttribute(HtmlTextWriterAttribute.For, control.ClientID);
					}
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.For, associatedControlID);
				}
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x000AC7F0 File Offset: 0x000AA9F0
		protected override void AddParsedSubObject(object obj)
		{
			if (this.HasControls())
			{
				base.AddParsedSubObject(obj);
				return;
			}
			if (obj is LiteralControl)
			{
				if (this._textSetByAddParsedSubObject)
				{
					this.Text += ((LiteralControl)obj).Text;
				}
				else
				{
					this.Text = ((LiteralControl)obj).Text;
				}
				this._textSetByAddParsedSubObject = true;
				return;
			}
			string text = this.Text;
			if (text.Length != 0)
			{
				this.Text = string.Empty;
				base.AddParsedSubObject(new LiteralControl(text));
			}
			base.AddParsedSubObject(obj);
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000AC884 File Offset: 0x000AAA84
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				base.LoadViewState(savedState);
				string text = (string)this.ViewState["Text"];
				if (text != null && this.HasControls())
				{
					this.Controls.Clear();
				}
			}
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000AC8C7 File Offset: 0x000AAAC7
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (base.HasRenderingData())
			{
				base.RenderContents(writer);
				return;
			}
			writer.Write(this.Text);
		}

		// Token: 0x040021BA RID: 8634
		private bool _textSetByAddParsedSubObject;
	}
}
