using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using Telerik.Web.UI.Editor;

namespace Telerik.Web.UI
{
	// Token: 0x02001857 RID: 6231
	[ParseChildren(true, "Items")]
	public class EditorSplitButton : EditorDropDown
	{
		// Token: 0x0600F284 RID: 62084 RVA: 0x00374434 File Offset: 0x00372634
		public EditorSplitButton()
		{
		}

		// Token: 0x0600F285 RID: 62085 RVA: 0x0037443C File Offset: 0x0037263C
		public EditorSplitButton(string name) : base(name)
		{
		}

		// Token: 0x0600F286 RID: 62086 RVA: 0x00374445 File Offset: 0x00372645
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorSplitButton(EditorToolStrip tool) : base(tool)
		{
			this.ShowText = tool.ShowText;
			this.Text = tool.Text;
		}

		// Token: 0x0600F287 RID: 62087 RVA: 0x00374466 File Offset: 0x00372666
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorSplitButton(EditorTool tool) : base(tool)
		{
			this.ShowText = tool.ShowText;
		}

		// Token: 0x17004934 RID: 18740
		// (get) Token: 0x0600F288 RID: 62088 RVA: 0x0037447B File Offset: 0x0037267B
		// (set) Token: 0x0600F289 RID: 62089 RVA: 0x0037447E File Offset: 0x0037267E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override EditorToolType Type
		{
			get
			{
				return EditorToolType.SplitButton;
			}
			set
			{
			}
		}

		// Token: 0x17004935 RID: 18741
		// (get) Token: 0x0600F28A RID: 62090 RVA: 0x00374480 File Offset: 0x00372680
		[DefaultValue(true)]
		public override bool ShowIcon
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17004936 RID: 18742
		// (get) Token: 0x0600F28B RID: 62091 RVA: 0x00374483 File Offset: 0x00372683
		// (set) Token: 0x0600F28C RID: 62092 RVA: 0x00374491 File Offset: 0x00372691
		[DefaultValue(false)]
		public override bool ShowText
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

		// Token: 0x17004937 RID: 18743
		// (get) Token: 0x0600F28D RID: 62093 RVA: 0x003744A9 File Offset: 0x003726A9
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
	}
}
