using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Editor.Animations
{
	// Token: 0x02000276 RID: 630
	public class EditorToolbarAnimation : StateManager
	{
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0004D150 File Offset: 0x0004B350
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x0004D171 File Offset: 0x0004B371
		[Description("Gets or sets the type of animation that will be used for the current animation.")]
		[DefaultValue(EditorToolBarAnimationType.Fade)]
		[Category("Behavior")]
		public EditorToolBarAnimationType Type
		{
			get
			{
				return (EditorToolBarAnimationType)(base.ViewState["Type"] ?? EditorToolBarAnimationType.Fade);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0004D189 File Offset: 0x0004B389
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x0004D1AB File Offset: 0x0004B3AB
		[Category("Behavior")]
		[DefaultValue(100)]
		[Description("Sets/gets the duration of the animation in milliseconds.")]
		public int Duration
		{
			get
			{
				return (int)(base.ViewState["Duration"] ?? 100);
			}
			set
			{
				base.ViewState["Duration"] = value;
			}
		}
	}
}
