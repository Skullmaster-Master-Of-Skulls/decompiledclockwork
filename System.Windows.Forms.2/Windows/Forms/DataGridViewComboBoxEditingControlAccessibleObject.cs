using System;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020001CC RID: 460
	internal class DataGridViewComboBoxEditingControlAccessibleObject : ComboBox.ComboBoxUiaProvider
	{
		// Token: 0x06002052 RID: 8274 RVA: 0x0009B839 File Offset: 0x00099A39
		public DataGridViewComboBoxEditingControlAccessibleObject(DataGridViewComboBoxEditingControl ownerControl) : base(ownerControl)
		{
			this.ownerControl = ownerControl;
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x0009B849 File Offset: 0x00099A49
		internal override void ClearOwnerControlInternal()
		{
			this.ownerControl = null;
			base.ClearOwnerControlInternal();
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x0009B858 File Offset: 0x00099A58
		public override AccessibleObject Parent
		{
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this._parentAccessibleObject;
			}
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x0009B860 File Offset: 0x00099A60
		internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			if (direction != UnsafeNativeMethods.NavigateDirection.Parent)
			{
				return base.FragmentNavigate(direction);
			}
			IDataGridViewEditingControl dataGridViewEditingControl = base.Owner as IDataGridViewEditingControl;
			if (dataGridViewEditingControl != null && dataGridViewEditingControl.EditingControlDataGridView.EditingControl == dataGridViewEditingControl)
			{
				return this._parentAccessibleObject;
			}
			return null;
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x000110B1 File Offset: 0x0000F2B1
		internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
		{
			get
			{
				IDataGridViewEditingControl dataGridViewEditingControl = base.Owner as IDataGridViewEditingControl;
				if (dataGridViewEditingControl == null)
				{
					return null;
				}
				DataGridView editingControlDataGridView = dataGridViewEditingControl.EditingControlDataGridView;
				if (editingControlDataGridView == null)
				{
					return null;
				}
				return editingControlDataGridView.AccessibilityObject;
			}
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x0009B89D File Offset: 0x00099A9D
		internal override bool IsPatternSupported(int patternId)
		{
			if (base.IsOwnerControlDestroyed())
			{
				return false;
			}
			if (patternId == 10005)
			{
				return this.ownerControl.DropDownStyle > ComboBoxStyle.Simple;
			}
			return base.IsPatternSupported(patternId);
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x0009B8C7 File Offset: 0x00099AC7
		internal override object GetPropertyValue(int propertyID)
		{
			if (propertyID == 30028)
			{
				return this.IsPatternSupported(10005);
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x0009B8E9 File Offset: 0x00099AE9
		internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
		{
			get
			{
				if (base.IsOwnerControlDestroyed())
				{
					return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
				}
				if (!this.ownerControl.DroppedDown)
				{
					return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
				}
				return UnsafeNativeMethods.ExpandCollapseState.Expanded;
			}
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x0009B905 File Offset: 0x00099B05
		internal override void SetParent(AccessibleObject parent)
		{
			this._parentAccessibleObject = parent;
		}

		// Token: 0x04000D9A RID: 3482
		private DataGridViewComboBoxEditingControl ownerControl;

		// Token: 0x04000D9B RID: 3483
		private AccessibleObject _parentAccessibleObject;
	}
}
