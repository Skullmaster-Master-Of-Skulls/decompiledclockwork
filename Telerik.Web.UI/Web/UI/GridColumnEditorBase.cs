using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000364 RID: 868
	[TelerikToolboxCategory("Data")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ToolboxBitmap(typeof(GridBoolColumnEditor), "Telerik.Web.UI.Grid.png")]
	public abstract class GridColumnEditorBase : Control, IGridColumnEditor
	{
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x0005D296 File Offset: 0x0005B496
		protected bool InitializedInContainer
		{
			get
			{
				return this.initializedInContainer;
			}
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x0005D29E File Offset: 0x0005B49E
		protected override void AddParsedSubObject(object obj)
		{
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x0005D2A0 File Offset: 0x0005B4A0
		public virtual void SetOwner(IGridEditableColumn owner)
		{
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x0005D2A2 File Offset: 0x0005B4A2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual Control ContainerControl
		{
			get
			{
				return this._containerControl;
			}
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x0005D2AA File Offset: 0x0005B4AA
		public void InitializeInControl(Control containerControl)
		{
			if (this.initializedInContainer)
			{
				this.ControlsCreated = false;
				this.initializedInContainer = false;
			}
			this._containerControl = containerControl;
			this.EnsureControlsCreated();
			this.AddControlsToContainer();
			this.initializedInContainer = true;
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0005D2DC File Offset: 0x0005B4DC
		public void InitializeFromControl(Control containerControl)
		{
			this._containerControl = containerControl;
			this.LoadControlsFromContainer();
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x0005D2EB File Offset: 0x0005B4EB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool IsInitialized
		{
			get
			{
				return this.ContainerControl != null;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0005D2F9 File Offset: 0x0005B4F9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool IsInEditMode
		{
			get
			{
				return this._containerControl != null && this.ContainerItem.IsInEditMode;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x0005D310 File Offset: 0x0005B510
		// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x0005D318 File Offset: 0x0005B518
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected bool ControlsCreated
		{
			get
			{
				return this._controlsCreated;
			}
			set
			{
				this._controlsCreated = value;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x0005D321 File Offset: 0x0005B521
		protected virtual GridEditableItem ContainerItem
		{
			get
			{
				if (this.ContainerControl != null)
				{
					return GridColumn.GetBindingParentItem(this.ContainerControl) as GridEditableItem;
				}
				return null;
			}
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0005D33D File Offset: 0x0005B53D
		protected virtual void EnsureControlsCreated()
		{
			if (this.ControlsCreated)
			{
				return;
			}
			this.CreateControls();
			this.ControlsCreated = true;
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x0005D355 File Offset: 0x0005B555
		internal virtual void CopySettingsFrom(IGridColumnEditor editor)
		{
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x0005D357 File Offset: 0x0005B557
		protected virtual void CreateControls()
		{
		}

		// Token: 0x06001DED RID: 7661
		protected abstract void AddControlsToContainer();

		// Token: 0x06001DEE RID: 7662
		protected abstract void LoadControlsFromContainer();

		// Token: 0x04000762 RID: 1890
		private Control _containerControl;

		// Token: 0x04000763 RID: 1891
		private bool _controlsCreated;

		// Token: 0x04000764 RID: 1892
		private bool initializedInContainer;
	}
}
