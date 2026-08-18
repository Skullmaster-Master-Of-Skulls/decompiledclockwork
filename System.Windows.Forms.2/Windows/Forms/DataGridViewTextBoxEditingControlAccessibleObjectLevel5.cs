using System;

namespace System.Windows.Forms
{
	// Token: 0x0200010A RID: 266
	internal class DataGridViewTextBoxEditingControlAccessibleObjectLevel5 : TextBoxBase.TextBoxBaseAccessibleObject
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x0001101C File Offset: 0x0000F21C
		public DataGridViewTextBoxEditingControlAccessibleObjectLevel5(DataGridViewTextBoxEditingControl ownerControl) : base(ownerControl)
		{
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00011025 File Offset: 0x0000F225
		public override AccessibleObject Parent
		{
			get
			{
				return this._parentAccessibleObject;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00011030 File Offset: 0x0000F230
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0001106B File Offset: 0x0000F26B
		public override string Name
		{
			get
			{
				if (base.IsOwnerControlDestroyed())
				{
					return SR.GetString("DataGridView_AccEditingControlAccName");
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

		// Token: 0x0600047B RID: 1147 RVA: 0x00011074 File Offset: 0x0000F274
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

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x000110B1 File Offset: 0x0000F2B1
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

		// Token: 0x0600047D RID: 1149 RVA: 0x000110D4 File Offset: 0x0000F2D4
		internal override bool IsPatternSupported(int patternId)
		{
			return !base.IsOwnerControlDestroyed() && (patternId == 10002 || base.IsPatternSupported(patternId));
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000110F1 File Offset: 0x0000F2F1
		internal override void SetParent(AccessibleObject parent)
		{
			this._parentAccessibleObject = parent;
		}

		// Token: 0x040004AB RID: 1195
		private AccessibleObject _parentAccessibleObject;
	}
}
