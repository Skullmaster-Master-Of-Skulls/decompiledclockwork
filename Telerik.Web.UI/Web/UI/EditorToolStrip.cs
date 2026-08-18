using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001085 RID: 4229
	[ParseChildren(true, "Tools")]
	public class EditorToolStrip : EditorToolBase
	{
		// Token: 0x0600A9FF RID: 43519 RVA: 0x0024DF6F File Offset: 0x0024C16F
		public EditorToolStrip()
		{
		}

		// Token: 0x0600AA00 RID: 43520 RVA: 0x0024DF77 File Offset: 0x0024C177
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorToolStrip(string name) : this()
		{
			this.Name = name;
		}

		// Token: 0x0600AA01 RID: 43521 RVA: 0x0024DF86 File Offset: 0x0024C186
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorToolStrip(EditorTool tool) : this(tool.Name)
		{
			this.ShowText = tool.ShowText;
			this.Text = tool.Text;
		}

		// Token: 0x17003692 RID: 13970
		// (get) Token: 0x0600AA02 RID: 43522 RVA: 0x0024DFAC File Offset: 0x0024C1AC
		// (set) Token: 0x0600AA03 RID: 43523 RVA: 0x0024DFB0 File Offset: 0x0024C1B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override EditorToolType Type
		{
			get
			{
				return EditorToolType.ToolStrip;
			}
			set
			{
			}
		}

		// Token: 0x17003693 RID: 13971
		// (get) Token: 0x0600AA04 RID: 43524 RVA: 0x0024DFB2 File Offset: 0x0024C1B2
		// (set) Token: 0x0600AA05 RID: 43525 RVA: 0x0024DFC4 File Offset: 0x0024C1C4
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string Name
		{
			get
			{
				return base.GetViewStateValue<string>("Name", string.Empty);
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x17003694 RID: 13972
		// (get) Token: 0x0600AA06 RID: 43526 RVA: 0x0024DFD7 File Offset: 0x0024C1D7
		// (set) Token: 0x0600AA07 RID: 43527 RVA: 0x0024DFE5 File Offset: 0x0024C1E5
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public virtual bool ShowText
		{
			get
			{
				return base.GetViewStateValue<bool>("ShowText", false);
			}
			set
			{
				base.ViewState["ShowText"] = value;
			}
		}

		// Token: 0x17003695 RID: 13973
		// (get) Token: 0x0600AA08 RID: 43528 RVA: 0x0024DFFD File Offset: 0x0024C1FD
		// (set) Token: 0x0600AA09 RID: 43529 RVA: 0x0024E00F File Offset: 0x0024C20F
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string Text
		{
			get
			{
				return base.GetViewStateValue<string>("Text", string.Empty);
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x17003696 RID: 13974
		// (get) Token: 0x0600AA0A RID: 43530 RVA: 0x0024E022 File Offset: 0x0024C222
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public virtual EditorToolCollection Tools
		{
			get
			{
				if (this._tools == null)
				{
					this._tools = new EditorToolCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._tools).TrackViewState();
					}
				}
				return this._tools;
			}
		}

		// Token: 0x0600AA0B RID: 43531 RVA: 0x0024E050 File Offset: 0x0024C250
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Tools).LoadViewState(array[1]);
		}

		// Token: 0x0600AA0C RID: 43532 RVA: 0x0024E07C File Offset: 0x0024C27C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Tools).SaveViewState()
			};
		}

		// Token: 0x0600AA0D RID: 43533 RVA: 0x0024E0AA File Offset: 0x0024C2AA
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Tools).TrackViewState();
		}

		// Token: 0x0600AA0E RID: 43534 RVA: 0x0024E0BD File Offset: 0x0024C2BD
		internal override void SetDirty()
		{
			base.SetDirty();
			this.Tools.SetDirty();
		}

		// Token: 0x04002DBA RID: 11706
		private EditorToolCollection _tools;
	}
}
