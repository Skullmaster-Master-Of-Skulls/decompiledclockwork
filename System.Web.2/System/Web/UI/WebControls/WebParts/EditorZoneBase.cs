using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200053C RID: 1340
	public abstract class EditorZoneBase : ToolZone
	{
		// Token: 0x0600445D RID: 17501 RVA: 0x000E267F File Offset: 0x000E087F
		protected EditorZoneBase() : base(WebPartManager.EditDisplayMode)
		{
		}

		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x0600445E RID: 17502 RVA: 0x000E268C File Offset: 0x000E088C
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("EditorZoneBase_ApplyVerb")]
		public virtual WebPartVerb ApplyVerb
		{
			get
			{
				if (this._applyVerb == null)
				{
					this._applyVerb = new WebPartEditorApplyVerb();
					this._applyVerb.EventArgument = "apply";
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._applyVerb).TrackViewState();
					}
				}
				return this._applyVerb;
			}
		}

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x0600445F RID: 17503 RVA: 0x000E26CA File Offset: 0x000E08CA
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("EditorZoneBase_CancelVerb")]
		public virtual WebPartVerb CancelVerb
		{
			get
			{
				if (this._cancelVerb == null)
				{
					this._cancelVerb = new WebPartEditorCancelVerb();
					this._cancelVerb.EventArgument = "cancel";
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._cancelVerb).TrackViewState();
					}
				}
				return this._cancelVerb;
			}
		}

		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x06004460 RID: 17504 RVA: 0x000E2708 File Offset: 0x000E0908
		protected override bool Display
		{
			get
			{
				return base.Display && this.WebPartToEdit != null;
			}
		}

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x06004461 RID: 17505 RVA: 0x000E271D File Offset: 0x000E091D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public EditorPartChrome EditorPartChrome
		{
			get
			{
				if (this._editorPartChrome == null)
				{
					this._editorPartChrome = this.CreateEditorPartChrome();
				}
				return this._editorPartChrome;
			}
		}

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x06004462 RID: 17506 RVA: 0x000E273C File Offset: 0x000E093C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public EditorPartCollection EditorParts
		{
			get
			{
				if (this._editorParts == null)
				{
					WebPart webPartToEdit = this.WebPartToEdit;
					EditorPartCollection existingEditorParts = null;
					if (webPartToEdit != null && webPartToEdit != null)
					{
						existingEditorParts = ((IWebEditable)webPartToEdit).CreateEditorParts();
					}
					EditorPartCollection editorPartCollection = new EditorPartCollection(existingEditorParts, this.CreateEditorParts());
					if (!base.DesignMode)
					{
						foreach (object obj in editorPartCollection)
						{
							EditorPart editorPart = (EditorPart)obj;
							if (string.IsNullOrEmpty(editorPart.ID))
							{
								throw new InvalidOperationException(SR.GetString("EditorZoneBase_NoEditorPartID"));
							}
						}
					}
					this._editorParts = editorPartCollection;
					this.EnsureChildControls();
				}
				return this._editorParts;
			}
		}

		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x06004463 RID: 17507 RVA: 0x000E27F4 File Offset: 0x000E09F4
		// (set) Token: 0x06004464 RID: 17508 RVA: 0x000DD332 File Offset: 0x000DB532
		[WebSysDefaultValue("EditorZoneBase_DefaultEmptyZoneText")]
		public override string EmptyZoneText
		{
			get
			{
				string text = (string)this.ViewState["EmptyZoneText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("EditorZoneBase_DefaultEmptyZoneText");
			}
			set
			{
				this.ViewState["EmptyZoneText"] = value;
			}
		}

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x06004465 RID: 17509 RVA: 0x000E2828 File Offset: 0x000E0A28
		// (set) Token: 0x06004466 RID: 17510 RVA: 0x000E285A File Offset: 0x000E0A5A
		[Localizable(true)]
		[WebCategory("Behavior")]
		[WebSysDefaultValue("EditorZoneBase_DefaultErrorText")]
		[WebSysDescription("EditorZoneBase_ErrorText")]
		public virtual string ErrorText
		{
			get
			{
				string text = (string)this.ViewState["ErrorText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("EditorZoneBase_DefaultErrorText");
			}
			set
			{
				this.ViewState["ErrorText"] = value;
			}
		}

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x06004467 RID: 17511 RVA: 0x000E2870 File Offset: 0x000E0A70
		// (set) Token: 0x06004468 RID: 17512 RVA: 0x0009AB69 File Offset: 0x00098D69
		[WebSysDefaultValue("EditorZoneBase_DefaultHeaderText")]
		public override string HeaderText
		{
			get
			{
				string text = (string)this.ViewState["HeaderText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("EditorZoneBase_DefaultHeaderText");
			}
			set
			{
				this.ViewState["HeaderText"] = value;
			}
		}

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x06004469 RID: 17513 RVA: 0x000E28A4 File Offset: 0x000E0AA4
		// (set) Token: 0x0600446A RID: 17514 RVA: 0x0008B81D File Offset: 0x00089A1D
		[WebSysDefaultValue("EditorZoneBase_DefaultInstructionText")]
		public override string InstructionText
		{
			get
			{
				string text = (string)this.ViewState["InstructionText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("EditorZoneBase_DefaultInstructionText");
			}
			set
			{
				this.ViewState["InstructionText"] = value;
			}
		}

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x0600446B RID: 17515 RVA: 0x000E28D6 File Offset: 0x000E0AD6
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("EditorZoneBase_OKVerb")]
		public virtual WebPartVerb OKVerb
		{
			get
			{
				if (this._okVerb == null)
				{
					this._okVerb = new WebPartEditorOKVerb();
					this._okVerb.EventArgument = "ok";
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._okVerb).TrackViewState();
					}
				}
				return this._okVerb;
			}
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x0600446C RID: 17516 RVA: 0x000E2914 File Offset: 0x000E0B14
		protected WebPart WebPartToEdit
		{
			get
			{
				if (base.WebPartManager != null && base.WebPartManager.DisplayMode == WebPartManager.EditDisplayMode)
				{
					return base.WebPartManager.SelectedWebPart;
				}
				return null;
			}
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x000E2940 File Offset: 0x000E0B40
		private void ApplyAndSyncChanges()
		{
			WebPart webPartToEdit = this.WebPartToEdit;
			if (webPartToEdit != null)
			{
				EditorPartCollection editorParts = this.EditorParts;
				foreach (object obj in editorParts)
				{
					EditorPart editorPart = (EditorPart)obj;
					if (editorPart.Display && editorPart.Visible && editorPart.ChromeState == PartChromeState.Normal && !editorPart.ApplyChanges())
					{
						this._applyError = true;
					}
				}
				if (!this._applyError)
				{
					foreach (object obj2 in editorParts)
					{
						EditorPart editorPart2 = (EditorPart)obj2;
						editorPart2.SyncChanges();
					}
				}
			}
		}

		// Token: 0x0600446E RID: 17518 RVA: 0x000E2A20 File Offset: 0x000E0C20
		protected override void Close()
		{
			if (base.WebPartManager != null)
			{
				base.WebPartManager.EndWebPartEditing();
			}
		}

		// Token: 0x0600446F RID: 17519 RVA: 0x000E2A38 File Offset: 0x000E0C38
		protected internal override void CreateChildControls()
		{
			ControlCollection controls = this.Controls;
			controls.Clear();
			WebPart webPartToEdit = this.WebPartToEdit;
			foreach (object obj in this.EditorParts)
			{
				EditorPart editorPart = (EditorPart)obj;
				if (webPartToEdit != null)
				{
					editorPart.SetWebPartToEdit(webPartToEdit);
					editorPart.SetWebPartManager(base.WebPartManager);
				}
				editorPart.SetZone(this);
				controls.Add(editorPart);
			}
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x000E2AC8 File Offset: 0x000E0CC8
		protected virtual EditorPartChrome CreateEditorPartChrome()
		{
			return new EditorPartChrome(this);
		}

		// Token: 0x06004471 RID: 17521
		protected abstract EditorPartCollection CreateEditorParts();

		// Token: 0x06004472 RID: 17522 RVA: 0x000E2AD0 File Offset: 0x000E0CD0
		protected void InvalidateEditorParts()
		{
			this._editorParts = null;
			base.ChildControlsCreated = false;
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x000E2AE0 File Offset: 0x000E0CE0
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array.Length != 4)
			{
				throw new ArgumentException(SR.GetString("ViewState_InvalidViewState"));
			}
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.ApplyVerb).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.CancelVerb).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.OKVerb).LoadViewState(array[3]);
			}
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x000E2B57 File Offset: 0x000E0D57
		protected override void OnDisplayModeChanged(object sender, WebPartDisplayModeEventArgs e)
		{
			this.InvalidateEditorParts();
			base.OnDisplayModeChanged(sender, e);
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x000E2B67 File Offset: 0x000E0D67
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.EditorPartChrome.PerformPreRender();
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x000E2B7C File Offset: 0x000E0D7C
		protected override void OnSelectedWebPartChanged(object sender, WebPartEventArgs e)
		{
			if (base.WebPartManager != null && base.WebPartManager.DisplayMode == WebPartManager.EditDisplayMode)
			{
				this.InvalidateEditorParts();
				if (e.WebPart != null)
				{
					foreach (object obj in this.EditorParts)
					{
						EditorPart editorPart = (EditorPart)obj;
						editorPart.SyncChanges();
					}
				}
			}
			base.OnSelectedWebPartChanged(sender, e);
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x000E2C04 File Offset: 0x000E0E04
		protected override void RaisePostBackEvent(string eventArgument)
		{
			if (string.Equals(eventArgument, "apply", StringComparison.OrdinalIgnoreCase))
			{
				if (this.ApplyVerb.Visible && this.ApplyVerb.Enabled && this.WebPartToEdit != null)
				{
					this.ApplyAndSyncChanges();
					return;
				}
			}
			else if (string.Equals(eventArgument, "cancel", StringComparison.OrdinalIgnoreCase))
			{
				if (this.CancelVerb.Visible && this.CancelVerb.Enabled && this.WebPartToEdit != null)
				{
					this.Close();
					return;
				}
			}
			else if (string.Equals(eventArgument, "ok", StringComparison.OrdinalIgnoreCase))
			{
				if (this.OKVerb.Visible && this.OKVerb.Enabled && this.WebPartToEdit != null)
				{
					this.ApplyAndSyncChanges();
					if (!this._applyError)
					{
						this.Close();
						return;
					}
				}
			}
			else
			{
				base.RaisePostBackEvent(eventArgument);
			}
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x000DD993 File Offset: 0x000DBB93
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			base.Render(writer);
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x000E2CD4 File Offset: 0x000E0ED4
		protected override void RenderBody(HtmlTextWriter writer)
		{
			base.RenderBodyTableBeginTag(writer);
			if (base.DesignMode)
			{
				base.RenderDesignerRegionBeginTag(writer, Orientation.Vertical);
			}
			if (this.HasControls())
			{
				bool flag = true;
				this.RenderInstructionText(writer, ref flag);
				if (this._applyError)
				{
					this.RenderErrorText(writer, ref flag);
				}
				EditorPartChrome editorPartChrome = this.EditorPartChrome;
				foreach (object obj in this.EditorParts)
				{
					EditorPart editorPart = (EditorPart)obj;
					if (editorPart.Display && editorPart.Visible)
					{
						writer.RenderBeginTag(HtmlTextWriterTag.Tr);
						if (!flag)
						{
							writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingTop, "0");
						}
						else
						{
							flag = false;
						}
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						editorPartChrome.RenderEditorPart(writer, editorPart);
						writer.RenderEndTag();
						writer.RenderEndTag();
					}
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "0");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			else
			{
				this.RenderEmptyZoneText(writer);
			}
			if (base.DesignMode)
			{
				WebZone.RenderDesignerRegionEndTag(writer);
			}
			WebZone.RenderBodyTableEndTag(writer);
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x000E2E0C File Offset: 0x000E100C
		private void RenderEmptyZoneText(HtmlTextWriter writer)
		{
			string emptyZoneText = this.EmptyZoneText;
			if (!string.IsNullOrEmpty(emptyZoneText))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, "top");
				Style emptyZoneTextStyle = base.EmptyZoneTextStyle;
				if (!emptyZoneTextStyle.IsEmpty)
				{
					emptyZoneTextStyle.AddAttributesToRender(writer, this);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.Write(emptyZoneText);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x000E2E70 File Offset: 0x000E1070
		private void RenderErrorText(HtmlTextWriter writer, ref bool firstCell)
		{
			string errorText = this.ErrorText;
			if (!string.IsNullOrEmpty(errorText))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				firstCell = false;
				Label label = new Label();
				label.Text = errorText;
				label.Page = this.Page;
				label.ApplyStyle(base.ErrorStyle);
				label.RenderControl(writer);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x000E2ED8 File Offset: 0x000E10D8
		private void RenderInstructionText(HtmlTextWriter writer, ref bool firstCell)
		{
			string instructionText = this.InstructionText;
			if (!string.IsNullOrEmpty(instructionText))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				firstCell = false;
				Label label = new Label();
				label.Text = instructionText;
				label.Page = this.Page;
				label.ApplyStyle(base.InstructionTextStyle);
				label.RenderControl(writer);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x000E2F3F File Offset: 0x000E113F
		protected override void RenderVerbs(HtmlTextWriter writer)
		{
			base.RenderVerbsInternal(writer, new WebPartVerb[]
			{
				this.OKVerb,
				this.CancelVerb,
				this.ApplyVerb
			});
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x000E2F6C File Offset: 0x000E116C
		protected override object SaveViewState()
		{
			object[] array = new object[]
			{
				base.SaveViewState(),
				(this._applyVerb != null) ? ((IStateManager)this._applyVerb).SaveViewState() : null,
				(this._cancelVerb != null) ? ((IStateManager)this._cancelVerb).SaveViewState() : null,
				(this._okVerb != null) ? ((IStateManager)this._okVerb).SaveViewState() : null
			};
			for (int i = 0; i < 4; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x000E2FE8 File Offset: 0x000E11E8
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._applyVerb != null)
			{
				((IStateManager)this._applyVerb).TrackViewState();
			}
			if (this._cancelVerb != null)
			{
				((IStateManager)this._cancelVerb).TrackViewState();
			}
			if (this._okVerb != null)
			{
				((IStateManager)this._okVerb).TrackViewState();
			}
		}

		// Token: 0x04002629 RID: 9769
		private EditorPartCollection _editorParts;

		// Token: 0x0400262A RID: 9770
		private const int baseIndex = 0;

		// Token: 0x0400262B RID: 9771
		private const int applyVerbIndex = 1;

		// Token: 0x0400262C RID: 9772
		private const int cancelVerbIndex = 2;

		// Token: 0x0400262D RID: 9773
		private const int okVerbIndex = 3;

		// Token: 0x0400262E RID: 9774
		private const int viewStateArrayLength = 4;

		// Token: 0x0400262F RID: 9775
		private WebPartVerb _applyVerb;

		// Token: 0x04002630 RID: 9776
		private WebPartVerb _cancelVerb;

		// Token: 0x04002631 RID: 9777
		private WebPartVerb _okVerb;

		// Token: 0x04002632 RID: 9778
		private bool _applyError;

		// Token: 0x04002633 RID: 9779
		private EditorPartChrome _editorPartChrome;

		// Token: 0x04002634 RID: 9780
		private const string applyEventArgument = "apply";

		// Token: 0x04002635 RID: 9781
		private const string cancelEventArgument = "cancel";

		// Token: 0x04002636 RID: 9782
		private const string okEventArgument = "ok";
	}
}
