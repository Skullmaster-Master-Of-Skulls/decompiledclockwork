using System;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000220 RID: 544
	internal class DataGridViewTextBoxEditingControlAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x06002367 RID: 9063 RVA: 0x0009B963 File Offset: 0x00099B63
		public DataGridViewTextBoxEditingControlAccessibleObject(DataGridViewTextBoxEditingControl ownerControl) : base(ownerControl)
		{
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x000A89C5 File Offset: 0x000A6BC5
		public override AccessibleObject Parent
		{
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this._parentAccessibleObject;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x000A89D0 File Offset: 0x000A6BD0
		// (set) Token: 0x0600236A RID: 9066 RVA: 0x0001106B File Offset: 0x0000F26B
		public override string Name
		{
			get
			{
				if (base.IsOwnerControlDestroyed())
				{
					return string.Empty;
				}
				string accessibleName = base.Owner.AccessibleName;
				if (accessibleName != null)
				{
					return accessibleName;
				}
				return SR.GetString("DataGridView_AccEditingControlAccName");
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000A8A08 File Offset: 0x000A6C08
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

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x000110B1 File Offset: 0x0000F2B1
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

		// Token: 0x0600236D RID: 9069 RVA: 0x000A8A45 File Offset: 0x000A6C45
		internal override object GetPropertyValue(int propertyID)
		{
			if (propertyID == 30003)
			{
				return 50004;
			}
			if (propertyID == 30005)
			{
				return this.Name;
			}
			if (propertyID != 30043)
			{
				return base.GetPropertyValue(propertyID);
			}
			return true;
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x000A8A81 File Offset: 0x000A6C81
		internal override bool IsPatternSupported(int patternId)
		{
			return !base.IsOwnerControlDestroyed() && (patternId == 10002 || base.IsPatternSupported(patternId));
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x000A8A9E File Offset: 0x000A6C9E
		internal override void SetParent(AccessibleObject parent)
		{
			this._parentAccessibleObject = parent;
		}

		// Token: 0x04000E94 RID: 3732
		private AccessibleObject _parentAccessibleObject;
	}
}
