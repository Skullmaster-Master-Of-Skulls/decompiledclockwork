using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001A0C RID: 6668
	[XmlRoot("Menu")]
	[ToolboxItem(false)]
	public class RadSchedulerContextMenu : RadContextMenu, IMarkableStateManager, IStateManager
	{
		// Token: 0x17004DE8 RID: 19944
		// (get) Token: 0x06010229 RID: 66089 RVA: 0x0039F698 File Offset: 0x0039D898
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override ContextMenuTargetCollection Targets
		{
			get
			{
				throw new InvalidOperationException("RadSchedulerContextMenu does not support targets");
			}
		}

		// Token: 0x0601022A RID: 66090 RVA: 0x0039F6A4 File Offset: 0x0039D8A4
		protected override void ResolveControlTargetIds()
		{
		}

		// Token: 0x0601022B RID: 66091 RVA: 0x0039F6A6 File Offset: 0x0039D8A6
		protected override void DescribeTargets(IScriptDescriptor descriptor)
		{
		}

		// Token: 0x140001E1 RID: 481
		// (add) Token: 0x0601022C RID: 66092 RVA: 0x0039F6A8 File Offset: 0x0039D8A8
		// (remove) Token: 0x0601022D RID: 66093 RVA: 0x0039F6B4 File Offset: 0x0039D8B4
		[Browsable(false)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override event RadMenuEventHandler ItemClick
		{
			add
			{
				throw new InvalidOperationException("ItemClick event is not valid for RadSchedulerContextMenu. Use the ContextMenuItemClick event of RadScheduler instead.");
			}
			remove
			{
			}
		}

		// Token: 0x17004DE9 RID: 19945
		// (get) Token: 0x0601022E RID: 66094 RVA: 0x0039F6B6 File Offset: 0x0039D8B6
		// (set) Token: 0x0601022F RID: 66095 RVA: 0x0039F6BD File Offset: 0x0039D8BD
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Bindable(false)]
		[Description("OnClientItemClicking is not available for RadSchedulerContextMenu. Use the OnClientContextMenuItemClicking property of RadScheduler instead.")]
		[ClientControlEvent(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string OnClientItemClicking
		{
			get
			{
				return "";
			}
			set
			{
				throw new InvalidOperationException("OnClientItemClicking is not available for RadSchedulerContextMenu. Use the OnClientContextMenuItemClicking property of RadScheduler instead.");
			}
		}

		// Token: 0x17004DEA RID: 19946
		// (get) Token: 0x06010230 RID: 66096 RVA: 0x0039F6C9 File Offset: 0x0039D8C9
		// (set) Token: 0x06010231 RID: 66097 RVA: 0x0039F6D0 File Offset: 0x0039D8D0
		[ClientControlEvent(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Description("OnClientItemClicked is not available for RadSchedulerContextMenu. Use the OnClientContextMenuItemClicked property of RadScheduler instead.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Browsable(false)]
		[Bindable(false)]
		public override string OnClientItemClicked
		{
			get
			{
				return "";
			}
			set
			{
				throw new InvalidOperationException("OnClientItemClicked is not available for RadSchedulerContextMenu. Use the OnClientContextMenuItemClicked property of RadScheduler instead.");
			}
		}

		// Token: 0x17004DEB RID: 19947
		// (get) Token: 0x06010232 RID: 66098 RVA: 0x0039F6DC File Offset: 0x0039D8DC
		// (set) Token: 0x06010233 RID: 66099 RVA: 0x0039F6E3 File Offset: 0x0039D8E3
		[Bindable(false)]
		[Description("OnClientShowing is not available for RadSchedulerContextMenu. Use the OnClientContextMenuShowing property of RadScheduler instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent(false)]
		[Browsable(false)]
		public override string OnClientShowing
		{
			get
			{
				return "";
			}
			set
			{
				throw new InvalidOperationException("OnClientShowing is not available for RadSchedulerContextMenu. Use the OnClientContextMenuShowing property of RadScheduler instead.");
			}
		}

		// Token: 0x17004DEC RID: 19948
		// (get) Token: 0x06010234 RID: 66100 RVA: 0x0039F6EF File Offset: 0x0039D8EF
		// (set) Token: 0x06010235 RID: 66101 RVA: 0x0039F6F6 File Offset: 0x0039D8F6
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Browsable(false)]
		[Bindable(false)]
		[Description("OnClientShown is not available for RadSchedulerContextMenu. Use the OnClientContextMenuShown property of RadScheduler instead.")]
		[ClientControlEvent(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string OnClientShown
		{
			get
			{
				return "";
			}
			set
			{
				throw new InvalidOperationException("OnClientShown is not available for RadSchedulerContextMenu. Use the OnClientContextMenuShown property of RadScheduler instead.");
			}
		}

		// Token: 0x06010236 RID: 66102 RVA: 0x0039F702 File Offset: 0x0039D902
		void IMarkableStateManager.SetDirty()
		{
			this.ViewState.SetDirty(true);
			base.Children.SetDirty();
			base.ControlStyle.SetDirty();
		}

		// Token: 0x17004DED RID: 19949
		// (get) Token: 0x06010237 RID: 66103 RVA: 0x0039F726 File Offset: 0x0039D926
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return base.IsTrackingViewState;
			}
		}

		// Token: 0x06010238 RID: 66104 RVA: 0x0039F730 File Offset: 0x0039D930
		void IStateManager.LoadViewState(object state)
		{
			object[] array = (object[])state;
			this.LoadViewState(array[0]);
			((IStateManager)base.Children).LoadViewState(array[1]);
			this.ID = (string)array[2];
		}

		// Token: 0x06010239 RID: 66105 RVA: 0x0039F76C File Offset: 0x0039D96C
		object IStateManager.SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)base.Children).SaveViewState(),
				this.ID
			};
		}

		// Token: 0x0601023A RID: 66106 RVA: 0x0039F7A1 File Offset: 0x0039D9A1
		void IStateManager.TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)base.Children).TrackViewState();
		}

		// Token: 0x0601023B RID: 66107 RVA: 0x0039F7B4 File Offset: 0x0039D9B4
		protected override void LoadTargetsViewState(object[] viewState)
		{
		}

		// Token: 0x0601023C RID: 66108 RVA: 0x0039F7B6 File Offset: 0x0039D9B6
		protected override object SaveTargetsViewState()
		{
			return null;
		}

		// Token: 0x0601023D RID: 66109 RVA: 0x0039F7B9 File Offset: 0x0039D9B9
		protected override void TrackTargetsViewState()
		{
		}
	}
}
