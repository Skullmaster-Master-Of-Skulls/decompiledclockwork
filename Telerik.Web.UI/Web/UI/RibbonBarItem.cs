using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020007C5 RID: 1989
	[ToolboxItem(false)]
	public abstract class RibbonBarItem : WebControl, IRibbonBarSubComponent, IRibbonBarGroupHostedItem
	{
		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x06004558 RID: 17752 RVA: 0x000DB1D8 File Offset: 0x000D93D8
		// (set) Token: 0x06004559 RID: 17753 RVA: 0x000DB1E0 File Offset: 0x000D93E0
		public IRibbonBarSubComponent Container { get; internal set; }

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x0600455A RID: 17754 RVA: 0x000DB1E9 File Offset: 0x000D93E9
		public override Control Parent
		{
			get
			{
				return this.RibbonBar;
			}
		}

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x000DB1F1 File Offset: 0x000D93F1
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

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x0600455C RID: 17756 RVA: 0x000DB208 File Offset: 0x000D9408
		// (set) Token: 0x0600455D RID: 17757 RVA: 0x000DB210 File Offset: 0x000D9410
		public virtual WebControl ParentWebControl
		{
			get
			{
				return this._parentWebControl;
			}
			internal set
			{
				this._parentWebControl = value;
				if (!this._parentWebControl.Controls.Contains(this))
				{
					this._parentWebControl.Controls.Add(this);
				}
			}
		}

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x0600455E RID: 17758 RVA: 0x000DB23D File Offset: 0x000D943D
		// (set) Token: 0x0600455F RID: 17759 RVA: 0x000DB245 File Offset: 0x000D9445
		public virtual RibbonBarGroup Group
		{
			get
			{
				return this._group;
			}
			internal set
			{
				this._group = value;
			}
		}

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x06004560 RID: 17760 RVA: 0x000DB24E File Offset: 0x000D944E
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

		// Token: 0x06004561 RID: 17761 RVA: 0x000DB26A File Offset: 0x000D946A
		protected virtual IRenderer CreateControlRenderer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004562 RID: 17762 RVA: 0x000DB274 File Offset: 0x000D9474
		internal void BaseAddAttributesToRender(HtmlTextWriter writer)
		{
			bool enabled = this.Enabled;
			this.Enabled = true;
			base.AddAttributesToRender(writer);
			this.Enabled = enabled;
		}

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x06004563 RID: 17763
		public abstract RibbonBarItemType ItemType { get; }

		// Token: 0x04001206 RID: 4614
		private IRenderer _renderer;

		// Token: 0x04001207 RID: 4615
		private WebControl _parentWebControl;

		// Token: 0x04001208 RID: 4616
		private RibbonBarGroup _group;
	}
}
