using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200056F RID: 1391
	public abstract class ToolZone : WebZone, IPostBackEventHandler
	{
		// Token: 0x06004684 RID: 18052 RVA: 0x000E9238 File Offset: 0x000E7438
		protected ToolZone(ICollection associatedDisplayModes)
		{
			if (associatedDisplayModes == null || associatedDisplayModes.Count == 0)
			{
				throw new ArgumentNullException("associatedDisplayModes");
			}
			this._associatedDisplayModes = new WebPartDisplayModeCollection();
			foreach (object obj in associatedDisplayModes)
			{
				WebPartDisplayMode value = (WebPartDisplayMode)obj;
				this._associatedDisplayModes.Add(value);
			}
			this._associatedDisplayModes.SetReadOnly("ToolZone_DisplayModesReadOnly");
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x000E92CC File Offset: 0x000E74CC
		protected ToolZone(WebPartDisplayMode associatedDisplayMode)
		{
			if (associatedDisplayMode == null)
			{
				throw new ArgumentNullException("associatedDisplayMode");
			}
			this._associatedDisplayModes = new WebPartDisplayModeCollection();
			this._associatedDisplayModes.Add(associatedDisplayMode);
			this._associatedDisplayModes.SetReadOnly("ToolZone_DisplayModesReadOnly");
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x06004686 RID: 18054 RVA: 0x000E930A File Offset: 0x000E750A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebPartDisplayModeCollection AssociatedDisplayModes
		{
			get
			{
				return this._associatedDisplayModes;
			}
		}

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x06004687 RID: 18055 RVA: 0x000E9314 File Offset: 0x000E7514
		protected virtual bool Display
		{
			get
			{
				if (base.WebPartManager != null)
				{
					WebPartDisplayModeCollection associatedDisplayModes = this.AssociatedDisplayModes;
					if (associatedDisplayModes != null)
					{
						return associatedDisplayModes.Contains(base.WebPartManager.DisplayMode);
					}
				}
				return false;
			}
		}

		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x06004688 RID: 18056 RVA: 0x000E9346 File Offset: 0x000E7546
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("ToolZone_EditUIStyle")]
		public Style EditUIStyle
		{
			get
			{
				if (this._editUIStyle == null)
				{
					this._editUIStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._editUIStyle).TrackViewState();
					}
				}
				return this._editUIStyle;
			}
		}

		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x06004689 RID: 18057 RVA: 0x000E9374 File Offset: 0x000E7574
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("ToolZone_HeaderCloseVerb")]
		public virtual WebPartVerb HeaderCloseVerb
		{
			get
			{
				if (this._headerCloseVerb == null)
				{
					this._headerCloseVerb = new WebPartHeaderCloseVerb();
					this._headerCloseVerb.EventArgument = "headerClose";
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._headerCloseVerb).TrackViewState();
					}
				}
				return this._headerCloseVerb;
			}
		}

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x0600468A RID: 18058 RVA: 0x000E93B2 File Offset: 0x000E75B2
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("ToolZone_HeaderVerbStyle")]
		public Style HeaderVerbStyle
		{
			get
			{
				if (this._headerVerbStyle == null)
				{
					this._headerVerbStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._headerVerbStyle).TrackViewState();
					}
				}
				return this._headerVerbStyle;
			}
		}

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x0600468B RID: 18059 RVA: 0x000E93E0 File Offset: 0x000E75E0
		// (set) Token: 0x0600468C RID: 18060 RVA: 0x0008B81D File Offset: 0x00089A1D
		[Localizable(true)]
		[WebSysDefaultValue("")]
		[WebCategory("Behavior")]
		[WebSysDescription("ToolZone_InstructionText")]
		public virtual string InstructionText
		{
			get
			{
				string text = (string)this.ViewState["InstructionText"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["InstructionText"] = value;
			}
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x0600468D RID: 18061 RVA: 0x000E940D File Offset: 0x000E760D
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("ToolZone_InstructionTextStyle")]
		public Style InstructionTextStyle
		{
			get
			{
				if (this._instructionTextStyle == null)
				{
					this._instructionTextStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._instructionTextStyle).TrackViewState();
					}
				}
				return this._instructionTextStyle;
			}
		}

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x0600468E RID: 18062 RVA: 0x000E943B File Offset: 0x000E763B
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[WebSysDescription("ToolZone_LabelStyle")]
		public Style LabelStyle
		{
			get
			{
				if (this._labelStyle == null)
				{
					this._labelStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._labelStyle).TrackViewState();
					}
				}
				return this._labelStyle;
			}
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x0600468F RID: 18063 RVA: 0x000E9469 File Offset: 0x000E7669
		// (set) Token: 0x06004690 RID: 18064 RVA: 0x000E947B File Offset: 0x000E767B
		[Bindable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Visible
		{
			get
			{
				return this.Display && base.Visible;
			}
			set
			{
				if (!base.DesignMode)
				{
					throw new InvalidOperationException(SR.GetString("ToolZone_CantSetVisible"));
				}
			}
		}

		// Token: 0x06004691 RID: 18065
		protected abstract void Close();

		// Token: 0x06004692 RID: 18066 RVA: 0x000E9498 File Offset: 0x000E7698
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array.Length != 7)
			{
				throw new ArgumentException(SR.GetString("ViewState_InvalidViewState"));
			}
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.EditUIStyle).LoadViewState(array[1]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.HeaderCloseVerb).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.HeaderVerbStyle).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.InstructionTextStyle).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.LabelStyle).LoadViewState(array[6]);
			}
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OnDisplayModeChanged(object sender, WebPartDisplayModeEventArgs e)
		{
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x000E9538 File Offset: 0x000E7738
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			WebPartManager webPartManager = base.WebPartManager;
			if (webPartManager != null)
			{
				webPartManager.DisplayModeChanged += this.OnDisplayModeChanged;
				webPartManager.SelectedWebPartChanged += this.OnSelectedWebPartChanged;
			}
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OnSelectedWebPartChanged(object sender, WebPartEventArgs e)
		{
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x000E957C File Offset: 0x000E777C
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (string.Equals(eventArgument, "headerClose", StringComparison.OrdinalIgnoreCase) && this.HeaderCloseVerb.Visible && this.HeaderCloseVerb.Enabled)
			{
				this.Close();
			}
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x000E95B9 File Offset: 0x000E77B9
		protected override void RenderFooter(HtmlTextWriter writer)
		{
			writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "4px");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderVerbs(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x000E95E0 File Offset: 0x000E77E0
		protected override void RenderHeader(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "2");
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			TitleStyle headerStyle = base.HeaderStyle;
			if (!headerStyle.IsEmpty)
			{
				Style style = new Style();
				if (!headerStyle.ForeColor.IsEmpty)
				{
					style.ForeColor = headerStyle.ForeColor;
				}
				style.Font.CopyFrom(headerStyle.Font);
				if (!headerStyle.Font.Size.IsEmpty)
				{
					style.Font.Size = new FontUnit(new Unit(100.0, UnitType.Percentage));
				}
				if (!style.IsEmpty)
				{
					style.AddAttributesToRender(writer, this);
				}
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			HorizontalAlign horizontalAlign = headerStyle.HorizontalAlign;
			if (horizontalAlign != HorizontalAlign.NotSet)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(HorizontalAlign));
				writer.AddAttribute(HtmlTextWriterAttribute.Align, converter.ConvertToString(horizontalAlign));
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.Write(this.HeaderText);
			writer.RenderEndTag();
			WebPartVerb headerCloseVerb = this.HeaderCloseVerb;
			if (headerCloseVerb.Visible)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				ZoneLinkButton zoneLinkButton = new ZoneLinkButton(this, headerCloseVerb.EventArgument);
				zoneLinkButton.Text = headerCloseVerb.Text;
				zoneLinkButton.ImageUrl = headerCloseVerb.ImageUrl;
				zoneLinkButton.ToolTip = headerCloseVerb.Description;
				zoneLinkButton.Enabled = headerCloseVerb.Enabled;
				zoneLinkButton.Page = this.Page;
				zoneLinkButton.ApplyStyle(this.HeaderVerbStyle);
				zoneLinkButton.RenderControl(writer);
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void RenderVerbs(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x000E97B4 File Offset: 0x000E79B4
		internal void RenderVerbsInternal(HtmlTextWriter writer, ICollection verbs)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in verbs)
			{
				WebPartVerb webPartVerb = (WebPartVerb)obj;
				if (webPartVerb.Visible)
				{
					arrayList.Add(webPartVerb);
				}
			}
			if (arrayList.Count > 0)
			{
				bool flag = true;
				foreach (object obj2 in arrayList)
				{
					WebPartVerb verb = (WebPartVerb)obj2;
					if (!flag)
					{
						writer.Write("&nbsp;");
					}
					this.RenderVerb(writer, verb);
					flag = false;
				}
			}
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x000E9880 File Offset: 0x000E7A80
		protected virtual void RenderVerb(HtmlTextWriter writer, WebPartVerb verb)
		{
			string eventArgument = verb.EventArgument;
			WebControl webControl;
			if (this.VerbButtonType == ButtonType.Button)
			{
				webControl = new ZoneButton(this, eventArgument)
				{
					Text = verb.Text
				};
			}
			else
			{
				ZoneLinkButton zoneLinkButton = new ZoneLinkButton(this, eventArgument);
				zoneLinkButton.Text = verb.Text;
				if (this.VerbButtonType == ButtonType.Image)
				{
					zoneLinkButton.ImageUrl = verb.ImageUrl;
				}
				webControl = zoneLinkButton;
			}
			webControl.ApplyStyle(base.VerbStyle);
			webControl.ToolTip = verb.Description;
			webControl.Enabled = verb.Enabled;
			webControl.Page = this.Page;
			webControl.RenderControl(writer);
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x000E9918 File Offset: 0x000E7B18
		protected override object SaveViewState()
		{
			object[] array = new object[]
			{
				base.SaveViewState(),
				(this._editUIStyle != null) ? ((IStateManager)this._editUIStyle).SaveViewState() : null,
				null,
				(this._headerCloseVerb != null) ? ((IStateManager)this._headerCloseVerb).SaveViewState() : null,
				(this._headerVerbStyle != null) ? ((IStateManager)this._headerVerbStyle).SaveViewState() : null,
				(this._instructionTextStyle != null) ? ((IStateManager)this._instructionTextStyle).SaveViewState() : null,
				(this._labelStyle != null) ? ((IStateManager)this._labelStyle).SaveViewState() : null
			};
			for (int i = 0; i < 7; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x000E99C8 File Offset: 0x000E7BC8
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._editUIStyle != null)
			{
				((IStateManager)this._editUIStyle).TrackViewState();
			}
			if (this._headerCloseVerb != null)
			{
				((IStateManager)this._headerCloseVerb).TrackViewState();
			}
			if (this._headerVerbStyle != null)
			{
				((IStateManager)this._headerVerbStyle).TrackViewState();
			}
			if (this._instructionTextStyle != null)
			{
				((IStateManager)this._instructionTextStyle).TrackViewState();
			}
			if (this._labelStyle != null)
			{
				((IStateManager)this._labelStyle).TrackViewState();
			}
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x000E9A3A File Offset: 0x000E7C3A
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x040026A5 RID: 9893
		private const string headerCloseEventArgument = "headerClose";

		// Token: 0x040026A6 RID: 9894
		private const int baseIndex = 0;

		// Token: 0x040026A7 RID: 9895
		private const int editUIStyleIndex = 1;

		// Token: 0x040026A8 RID: 9896
		private const int headerCloseVerbIndex = 3;

		// Token: 0x040026A9 RID: 9897
		private const int headerVerbStyleIndex = 4;

		// Token: 0x040026AA RID: 9898
		private const int instructionTextStyleIndex = 5;

		// Token: 0x040026AB RID: 9899
		private const int labelStyleIndex = 6;

		// Token: 0x040026AC RID: 9900
		private const int viewStateArrayLength = 7;

		// Token: 0x040026AD RID: 9901
		private Style _editUIStyle;

		// Token: 0x040026AE RID: 9902
		private WebPartVerb _headerCloseVerb;

		// Token: 0x040026AF RID: 9903
		private Style _headerVerbStyle;

		// Token: 0x040026B0 RID: 9904
		private Style _instructionTextStyle;

		// Token: 0x040026B1 RID: 9905
		private Style _labelStyle;

		// Token: 0x040026B2 RID: 9906
		private WebPartDisplayModeCollection _associatedDisplayModes;
	}
}
