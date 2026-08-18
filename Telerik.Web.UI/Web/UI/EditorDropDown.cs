using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Editor;

namespace Telerik.Web.UI
{
	// Token: 0x02001856 RID: 6230
	[ParseChildren(true, "Items")]
	public class EditorDropDown : EditorTool
	{
		// Token: 0x0600F272 RID: 62066 RVA: 0x003741E7 File Offset: 0x003723E7
		public EditorDropDown()
		{
		}

		// Token: 0x0600F273 RID: 62067 RVA: 0x003741FA File Offset: 0x003723FA
		public EditorDropDown(string name) : base(name)
		{
		}

		// Token: 0x0600F274 RID: 62068 RVA: 0x00374210 File Offset: 0x00372410
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorDropDown(EditorTool tool) : base(tool.Name, tool.ShortCut)
		{
			this.Visible = tool.Visible;
			this.Enabled = tool.Enabled;
			this.Text = tool.Text;
			this._attributes = tool.Attributes;
		}

		// Token: 0x0600F275 RID: 62069 RVA: 0x0037426A File Offset: 0x0037246A
		public EditorDropDown(EditorToolStrip tool) : this(new EditorTool(tool))
		{
			this._attributes = tool.Attributes;
		}

		// Token: 0x1700492D RID: 18733
		// (get) Token: 0x0600F276 RID: 62070 RVA: 0x00374284 File Offset: 0x00372484
		// (set) Token: 0x0600F277 RID: 62071 RVA: 0x00374287 File Offset: 0x00372487
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override EditorToolType Type
		{
			get
			{
				return EditorToolType.DropDown;
			}
			set
			{
			}
		}

		// Token: 0x1700492E RID: 18734
		// (get) Token: 0x0600F278 RID: 62072 RVA: 0x00374289 File Offset: 0x00372489
		[DefaultValue(true)]
		public override bool ShowText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700492F RID: 18735
		// (get) Token: 0x0600F279 RID: 62073 RVA: 0x0037428C File Offset: 0x0037248C
		// (set) Token: 0x0600F27A RID: 62074 RVA: 0x003742DF File Offset: 0x003724DF
		[DefaultValue(typeof(Unit), "")]
		public virtual Unit Width
		{
			get
			{
				if (!this._width.IsEmpty)
				{
					return this._width;
				}
				if (this.Attributes["width"] == null)
				{
					return Unit.Empty;
				}
				return EditorDropDown.ParseUnit(this.Attributes["width"], Unit.Empty);
			}
			set
			{
				this._width = value;
				if (!this._width.IsEmpty)
				{
					this.Attributes["width"] = this._width.ToString();
				}
			}
		}

		// Token: 0x0600F27B RID: 62075 RVA: 0x00374318 File Offset: 0x00372518
		private static Unit ParseUnit(string value, Unit defaultValue)
		{
			Unit result;
			try
			{
				result = Unit.Parse(value);
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x17004930 RID: 18736
		// (get) Token: 0x0600F27C RID: 62076 RVA: 0x00374344 File Offset: 0x00372544
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual EditorDropDownItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new EditorDropDownItemCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._items).TrackViewState();
					}
				}
				return this._items;
			}
		}

		// Token: 0x0600F27D RID: 62077 RVA: 0x00374374 File Offset: 0x00372574
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Items).LoadViewState(array[1]);
		}

		// Token: 0x0600F27E RID: 62078 RVA: 0x003743A0 File Offset: 0x003725A0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Items).SaveViewState()
			};
		}

		// Token: 0x0600F27F RID: 62079 RVA: 0x003743CE File Offset: 0x003725CE
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Items).TrackViewState();
		}

		// Token: 0x0600F280 RID: 62080 RVA: 0x003743E1 File Offset: 0x003725E1
		internal override void SetDirty()
		{
			base.SetDirty();
			this.Items.SetDirty();
		}

		// Token: 0x17004931 RID: 18737
		// (get) Token: 0x0600F281 RID: 62081 RVA: 0x003743F4 File Offset: 0x003725F4
		public override Telerik.Web.UI.Editor.AttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					return base.Attributes;
				}
				return this._attributes;
			}
		}

		// Token: 0x17004932 RID: 18738
		// (get) Token: 0x0600F282 RID: 62082 RVA: 0x0037440B File Offset: 0x0037260B
		protected override string AnchorCssClass
		{
			get
			{
				return this.Renderer.CssClassString;
			}
		}

		// Token: 0x17004933 RID: 18739
		// (get) Token: 0x0600F283 RID: 62083 RVA: 0x00374418 File Offset: 0x00372618
		protected override IEditorToolRenderer Renderer
		{
			get
			{
				if (this.toolRenderer == null)
				{
					this.toolRenderer = RendererFactory.GetRenderer(this);
				}
				return this.toolRenderer;
			}
		}

		// Token: 0x040045C5 RID: 17861
		private Unit _width = Unit.Empty;

		// Token: 0x040045C6 RID: 17862
		private EditorDropDownItemCollection _items;

		// Token: 0x040045C7 RID: 17863
		private readonly Telerik.Web.UI.Editor.AttributeCollection _attributes;
	}
}
