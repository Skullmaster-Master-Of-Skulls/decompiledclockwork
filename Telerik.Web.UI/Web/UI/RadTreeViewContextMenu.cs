using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001B59 RID: 7001
	[ToolboxItem(false)]
	[XmlRoot("Menu")]
	public class RadTreeViewContextMenu : RadContextMenu, IMarkableStateManager, IStateManager
	{
		// Token: 0x170052C5 RID: 21189
		// (get) Token: 0x06010F4A RID: 69450 RVA: 0x003C0F81 File Offset: 0x003BF181
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ContextMenuTargetCollection Targets
		{
			get
			{
				throw new InvalidOperationException("RadTreeViewContextMenu does not support targets");
			}
		}

		// Token: 0x06010F4B RID: 69451 RVA: 0x003C0F8D File Offset: 0x003BF18D
		protected override void ResolveControlTargetIds()
		{
		}

		// Token: 0x06010F4C RID: 69452 RVA: 0x003C0F8F File Offset: 0x003BF18F
		protected override void DescribeTargets(IScriptDescriptor descriptor)
		{
		}

		// Token: 0x140001E4 RID: 484
		// (add) Token: 0x06010F4D RID: 69453 RVA: 0x003C0F91 File Offset: 0x003BF191
		// (remove) Token: 0x06010F4E RID: 69454 RVA: 0x003C0F9D File Offset: 0x003BF19D
		[Browsable(false)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override event RadMenuEventHandler ItemClick
		{
			add
			{
				throw new InvalidOperationException("ItemClick event is not valid for RadTreeViewContextMenu. Use the ContextMenuItemClick event of RadTreeView instead.");
			}
			remove
			{
			}
		}

		// Token: 0x170052C6 RID: 21190
		// (get) Token: 0x06010F4F RID: 69455 RVA: 0x003C0F9F File Offset: 0x003BF19F
		// (set) Token: 0x06010F50 RID: 69456 RVA: 0x003C0FA6 File Offset: 0x003BF1A6
		[ClientControlEvent(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Description("OnClientItemClicking is not available for RadTreeViewContextMenu. Use the OnClientContextMenuItemClicking property of RadTreeView instead.")]
		public override string OnClientItemClicking
		{
			get
			{
				return "";
			}
			set
			{
				throw new InvalidOperationException("OnClientItemClicking is not available for RadTreeViewContextMenu. Use the OnClientContextMenuItemClicking property of RadTreeView instead.");
			}
		}

		// Token: 0x170052C7 RID: 21191
		// (get) Token: 0x06010F51 RID: 69457 RVA: 0x003C0FB2 File Offset: 0x003BF1B2
		// (set) Token: 0x06010F52 RID: 69458 RVA: 0x003C0FB9 File Offset: 0x003BF1B9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Description("OnClientItemClicked is not available for RadTreeViewContextMenu. Use the OnClientContextMenuItemClicked property of RadTreeView instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent(false)]
		public override string OnClientItemClicked
		{
			get
			{
				return "";
			}
			set
			{
				throw new InvalidOperationException("OnClientItemClicked is not available for RadTreeViewContextMenu. Use the OnClientContextMenuItemClicked property of RadTreeView instead.");
			}
		}

		// Token: 0x170052C8 RID: 21192
		// (get) Token: 0x06010F53 RID: 69459 RVA: 0x003C0FC5 File Offset: 0x003BF1C5
		// (set) Token: 0x06010F54 RID: 69460 RVA: 0x003C0FCC File Offset: 0x003BF1CC
		[Browsable(false)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Description("OnClientShowing is not available for RadTreeViewContextMenu. Use the OnClientContextMenuShowing property of RadTreeView instead.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent(false)]
		public override string OnClientShowing
		{
			get
			{
				return "";
			}
			set
			{
				throw new InvalidOperationException("OnClientShowing is not available for RadTreeViewContextMenu. Use the OnClientContextMenuShowing property of RadTreeView instead.");
			}
		}

		// Token: 0x170052C9 RID: 21193
		// (get) Token: 0x06010F55 RID: 69461 RVA: 0x003C0FD8 File Offset: 0x003BF1D8
		// (set) Token: 0x06010F56 RID: 69462 RVA: 0x003C0FDF File Offset: 0x003BF1DF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("OnClientShown is not available for RadTreeViewContextMenu. Use the OnClientContextMenuShown property of RadTreeView instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Bindable(false)]
		[ClientControlEvent(false)]
		public override string OnClientShown
		{
			get
			{
				return "";
			}
			set
			{
				throw new InvalidOperationException("OnClientShown is not available for RadTreeViewContextMenu. Use the OnClientContextMenuShown property of RadTreeView instead.");
			}
		}

		// Token: 0x06010F57 RID: 69463 RVA: 0x003C0FEB File Offset: 0x003BF1EB
		void IMarkableStateManager.SetDirty()
		{
			this.ViewState.SetDirty(true);
			base.Children.SetDirty();
			base.ControlStyle.SetDirty();
		}

		// Token: 0x170052CA RID: 21194
		// (get) Token: 0x06010F58 RID: 69464 RVA: 0x003C100F File Offset: 0x003BF20F
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return base.IsTrackingViewState;
			}
		}

		// Token: 0x06010F59 RID: 69465 RVA: 0x003C1018 File Offset: 0x003BF218
		void IStateManager.LoadViewState(object state)
		{
			object[] array = (object[])state;
			this.LoadViewState(array[0]);
			((IStateManager)base.Children).LoadViewState(array[1]);
			this.ID = (string)array[2];
		}

		// Token: 0x06010F5A RID: 69466 RVA: 0x003C1054 File Offset: 0x003BF254
		object IStateManager.SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)base.Children).SaveViewState(),
				this.ID
			};
		}

		// Token: 0x06010F5B RID: 69467 RVA: 0x003C1089 File Offset: 0x003BF289
		void IStateManager.TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)base.Children).TrackViewState();
		}

		// Token: 0x06010F5C RID: 69468 RVA: 0x003C109C File Offset: 0x003BF29C
		protected override void LoadTargetsViewState(object[] viewState)
		{
		}

		// Token: 0x06010F5D RID: 69469 RVA: 0x003C109E File Offset: 0x003BF29E
		protected override object SaveTargetsViewState()
		{
			return null;
		}

		// Token: 0x06010F5E RID: 69470 RVA: 0x003C10A1 File Offset: 0x003BF2A1
		protected override void TrackTargetsViewState()
		{
		}
	}
}
