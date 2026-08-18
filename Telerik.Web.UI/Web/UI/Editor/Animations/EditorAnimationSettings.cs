using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor.Animations
{
	// Token: 0x02000275 RID: 629
	public class EditorAnimationSettings : StateManager
	{
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0004D12D File Offset: 0x0004B32D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public EditorToolbarAnimation ToolbarAnimation
		{
			get
			{
				if (this._toolBarAnimation == null)
				{
					this._toolBarAnimation = new EditorToolbarAnimation();
				}
				return this._toolBarAnimation;
			}
		}

		// Token: 0x040005F8 RID: 1528
		private EditorToolbarAnimation _toolBarAnimation;
	}
}
