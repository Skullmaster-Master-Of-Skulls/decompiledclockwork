using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x020007D0 RID: 2000
	[ToolboxItem(false)]
	public class RibbonBarCollectionItemBase : WebControl, IRibbonBarSubComponent
	{
		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x060045C5 RID: 17861 RVA: 0x000DBA50 File Offset: 0x000D9C50
		// (set) Token: 0x060045C6 RID: 17862 RVA: 0x000DBA58 File Offset: 0x000D9C58
		public IRibbonBarSubComponent Container { get; internal set; }

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x060045C7 RID: 17863 RVA: 0x000DBA61 File Offset: 0x000D9C61
		public RadRibbonBar RibbonBar
		{
			get
			{
				if (this.Container == null)
				{
					return null;
				}
				return this.Container.RibbonBar;
			}
		}

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x060045C8 RID: 17864 RVA: 0x000DBA78 File Offset: 0x000D9C78
		// (set) Token: 0x060045C9 RID: 17865 RVA: 0x000DBA80 File Offset: 0x000D9C80
		public virtual WebControl ParentWebControl
		{
			get
			{
				return this.parentWebControl;
			}
			internal set
			{
				this.parentWebControl = value;
				if (!this.ParentWebControl.Controls.Contains(this))
				{
					this.ParentWebControl.Controls.Add(this);
				}
			}
		}

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x060045CA RID: 17866 RVA: 0x000DBAAD File Offset: 0x000D9CAD
		protected IRenderer Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = this.CreateControlRenderer();
				}
				return this._renderer;
			}
		}

		// Token: 0x060045CB RID: 17867 RVA: 0x000DBAC9 File Offset: 0x000D9CC9
		protected virtual IRenderer CreateControlRenderer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060045CC RID: 17868 RVA: 0x000DBAD0 File Offset: 0x000D9CD0
		internal void BaseAddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x000DBAD9 File Offset: 0x000D9CD9
		public override void RenderControl(HtmlTextWriter writer)
		{
			((RibbonBarCollectionItemRenderer)this.Renderer).RenderControl(writer);
		}

		// Token: 0x0400120F RID: 4623
		private IRenderer _renderer;

		// Token: 0x04001210 RID: 4624
		internal WebControl parentWebControl;
	}
}
