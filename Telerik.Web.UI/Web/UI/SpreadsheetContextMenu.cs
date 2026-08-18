using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200089C RID: 2204
	[ToolboxItem(false)]
	public class SpreadsheetContextMenu : RadContextMenu, IMarkableStateManager, IStateManager
	{
		// Token: 0x17001ADC RID: 6876
		// (get) Token: 0x060051FB RID: 20987 RVA: 0x000FF703 File Offset: 0x000FD903
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override ContextMenuTargetCollection Targets
		{
			get
			{
				throw new InvalidOperationException("SpreadsheetContextMenu does not support targets");
			}
		}

		// Token: 0x17001ADD RID: 6877
		// (get) Token: 0x060051FC RID: 20988 RVA: 0x000FF70F File Offset: 0x000FD90F
		// (set) Token: 0x060051FD RID: 20989 RVA: 0x000FF717 File Offset: 0x000FD917
		internal bool IsDefault { get; set; }

		// Token: 0x060051FE RID: 20990 RVA: 0x000FF720 File Offset: 0x000FD920
		protected override void ResolveControlTargetIds()
		{
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x000FF722 File Offset: 0x000FD922
		protected override void DescribeTargets(IScriptDescriptor descriptor)
		{
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x000FF724 File Offset: 0x000FD924
		void IMarkableStateManager.SetDirty()
		{
			this.ViewState.SetDirty(true);
			base.Children.SetDirty();
			base.ControlStyle.SetDirty();
		}

		// Token: 0x17001ADE RID: 6878
		// (get) Token: 0x06005201 RID: 20993 RVA: 0x000FF748 File Offset: 0x000FD948
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return base.IsTrackingViewState;
			}
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x000FF750 File Offset: 0x000FD950
		void IStateManager.LoadViewState(object state)
		{
			object[] array = (object[])state;
			this.LoadViewState(array[0]);
			((IStateManager)base.Children).LoadViewState(array[1]);
			this.ID = (string)array[2];
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x000FF78C File Offset: 0x000FD98C
		object IStateManager.SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)base.Children).SaveViewState(),
				this.ID
			};
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x000FF7C1 File Offset: 0x000FD9C1
		void IStateManager.TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)base.Children).TrackViewState();
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x000FF7D4 File Offset: 0x000FD9D4
		protected override void LoadTargetsViewState(object[] viewState)
		{
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x000FF7D6 File Offset: 0x000FD9D6
		protected override object SaveTargetsViewState()
		{
			return null;
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x000FF7D9 File Offset: 0x000FD9D9
		protected override void TrackTargetsViewState()
		{
		}
	}
}
