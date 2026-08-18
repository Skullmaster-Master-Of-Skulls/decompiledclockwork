using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020001D2 RID: 466
	[ComVisible(true)]
	internal class DataGridViewEditingControlAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x06002074 RID: 8308 RVA: 0x0009B963 File Offset: 0x00099B63
		public DataGridViewEditingControlAccessibleObject(Control ownerControl) : base(ownerControl)
		{
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x0009B96C File Offset: 0x00099B6C
		internal override bool IsIAccessibleExSupported()
		{
			return !base.IsOwnerControlDestroyed() && (AccessibilityImprovements.Level3 || base.IsIAccessibleExSupported());
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x0009B987 File Offset: 0x00099B87
		public override AccessibleObject Parent
		{
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
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
				DataGridViewCell currentCell = editingControlDataGridView.CurrentCell;
				if (currentCell == null)
				{
					return null;
				}
				return currentCell.AccessibilityObject;
			}
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x0009B9B8 File Offset: 0x00099BB8
		internal override bool IsPatternSupported(int patternId)
		{
			if (base.IsOwnerControlDestroyed())
			{
				return false;
			}
			if (AccessibilityImprovements.Level3 && patternId == 10005)
			{
				ComboBox comboBox = base.Owner as ComboBox;
				if (comboBox != null)
				{
					return comboBox.DropDownStyle > ComboBoxStyle.Simple;
				}
			}
			return base.IsPatternSupported(patternId);
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x0009B9FE File Offset: 0x00099BFE
		internal override object GetPropertyValue(int propertyID)
		{
			if (AccessibilityImprovements.Level3 && propertyID == 30028)
			{
				return this.IsPatternSupported(10005);
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x0009BA28 File Offset: 0x00099C28
		internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
		{
			get
			{
				ComboBox comboBox = base.Owner as ComboBox;
				if (comboBox == null)
				{
					return base.ExpandCollapseState;
				}
				if (!comboBox.DroppedDown)
				{
					return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
				}
				return UnsafeNativeMethods.ExpandCollapseState.Expanded;
			}
		}
	}
}
