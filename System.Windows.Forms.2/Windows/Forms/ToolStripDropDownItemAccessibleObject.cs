using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003C2 RID: 962
	[ComVisible(true)]
	public class ToolStripDropDownItemAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
	{
		// Token: 0x06004121 RID: 16673 RVA: 0x00115B8C File Offset: 0x00113D8C
		public ToolStripDropDownItemAccessibleObject(ToolStripDropDownItem item) : base(item)
		{
			this.owner = item;
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x00115B9C File Offset: 0x00113D9C
		internal override void ClearOwnerItem()
		{
			this.owner = null;
			base.ClearOwnerItem();
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06004123 RID: 16675 RVA: 0x00115BAC File Offset: 0x00113DAC
		public override AccessibleRole Role
		{
			get
			{
				if (base.IsOwnerItemCleared())
				{
					return AccessibleRole.MenuItem;
				}
				AccessibleRole accessibleRole = base.Owner.AccessibleRole;
				if (accessibleRole != AccessibleRole.Default)
				{
					return accessibleRole;
				}
				return AccessibleRole.MenuItem;
			}
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x00115BD8 File Offset: 0x00113DD8
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public override void DoDefaultAction()
		{
			ToolStripDropDownItem toolStripDropDownItem = base.Owner as ToolStripDropDownItem;
			if (toolStripDropDownItem != null && toolStripDropDownItem.HasDropDownItems)
			{
				toolStripDropDownItem.ShowDropDown();
				return;
			}
			base.DoDefaultAction();
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x00115C0C File Offset: 0x00113E0C
		internal override bool IsIAccessibleExSupported()
		{
			return !base.IsOwnerItemCleared() && (!AccessibilityImprovements.Level3 || this.owner.Parent == null || (!this.owner.Parent.IsInDesignMode && !this.owner.Parent.IsTopInDesignMode)) && ((this.owner != null && AccessibilityImprovements.Level1) || base.IsIAccessibleExSupported());
		}

		// Token: 0x06004126 RID: 16678 RVA: 0x00115C74 File Offset: 0x00113E74
		internal override bool IsPatternSupported(int patternId)
		{
			return !base.IsOwnerItemCleared() && ((patternId == 10005 && this.owner.HasDropDownItems) || base.IsPatternSupported(patternId));
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x00115CA0 File Offset: 0x00113EA0
		internal override object GetPropertyValue(int propertyID)
		{
			if (base.IsOwnerItemCleared())
			{
				return false;
			}
			if (AccessibilityImprovements.Level3 && propertyID == 30022 && this.owner != null && this.owner.Owner is ToolStripDropDown)
			{
				return !((ToolStripDropDown)this.owner.Owner).Visible;
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x00016280 File Offset: 0x00014480
		internal override void Expand()
		{
			this.DoDefaultAction();
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x00115D0C File Offset: 0x00113F0C
		internal override void Collapse()
		{
			if (base.IsOwnerItemCleared())
			{
				return;
			}
			if (this.owner != null && this.owner.DropDown != null && this.owner.DropDown.Visible)
			{
				this.owner.DropDown.Close();
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x0600412A RID: 16682 RVA: 0x00115D59 File Offset: 0x00113F59
		internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
		{
			get
			{
				if (base.IsOwnerItemCleared())
				{
					return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
				}
				if (!this.owner.DropDown.Visible)
				{
					return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
				}
				return UnsafeNativeMethods.ExpandCollapseState.Expanded;
			}
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x00115D7A File Offset: 0x00113F7A
		public override AccessibleObject GetChild(int index)
		{
			if (this.owner == null || !this.owner.HasDropDownItems)
			{
				return null;
			}
			return this.owner.DropDown.AccessibilityObject.GetChild(index);
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x00115DAC File Offset: 0x00113FAC
		public override int GetChildCount()
		{
			if (this.owner == null || !this.owner.HasDropDownItems)
			{
				return -1;
			}
			if (AccessibilityImprovements.Level3 && this.ExpandCollapseState == UnsafeNativeMethods.ExpandCollapseState.Collapsed)
			{
				return 0;
			}
			if (this.owner.DropDown.LayoutRequired)
			{
				LayoutTransaction.DoLayout(this.owner.DropDown, this.owner.DropDown, PropertyNames.Items);
			}
			return this.owner.DropDown.AccessibilityObject.GetChildCount();
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x00115E28 File Offset: 0x00114028
		internal int GetChildFragmentIndex(ToolStripItem.ToolStripItemAccessibleObject child)
		{
			if (this.owner == null || child.IsOwnerItemCleared() || this.owner.DropDownItems == null)
			{
				return -1;
			}
			for (int i = 0; i < this.owner.DropDownItems.Count; i++)
			{
				if (this.owner.DropDownItems[i].Available && child.Owner == this.owner.DropDownItems[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x00115EA4 File Offset: 0x001140A4
		internal int GetChildFragmentCount()
		{
			if (this.owner == null || this.owner.DropDownItems == null)
			{
				return -1;
			}
			int num = 0;
			for (int i = 0; i < this.owner.DropDownItems.Count; i++)
			{
				if (this.owner.DropDownItems[i].Available)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x00115F04 File Offset: 0x00114104
		internal AccessibleObject GetChildFragment(int index, UnsafeNativeMethods.NavigateDirection direction = UnsafeNativeMethods.NavigateDirection.NextSibling)
		{
			if (base.IsOwnerItemCleared())
			{
				return null;
			}
			ToolStrip.ToolStripAccessibleObject toolStripAccessibleObject = this.owner.DropDown.AccessibilityObject as ToolStrip.ToolStripAccessibleObject;
			if (toolStripAccessibleObject != null)
			{
				return toolStripAccessibleObject.GetChildFragment(index, false, direction);
			}
			return null;
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x00115F40 File Offset: 0x00114140
		internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			if (this.owner == null || this.owner.DropDown == null)
			{
				return null;
			}
			switch (direction)
			{
			case UnsafeNativeMethods.NavigateDirection.NextSibling:
			case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
			{
				ToolStripDropDown toolStripDropDown = this.owner.Owner as ToolStripDropDown;
				if (toolStripDropDown != null)
				{
					int num = toolStripDropDown.Items.IndexOf(this.owner);
					if (num == -1)
					{
						return null;
					}
					num += ((direction == UnsafeNativeMethods.NavigateDirection.NextSibling) ? 1 : -1);
					if (num < 0 || num >= toolStripDropDown.Items.Count)
					{
						return null;
					}
					ToolStripItem toolStripItem = toolStripDropDown.Items[num];
					ToolStripControlHost toolStripControlHost = toolStripItem as ToolStripControlHost;
					if (toolStripControlHost != null)
					{
						return toolStripControlHost.ControlAccessibilityObject;
					}
					return toolStripItem.AccessibilityObject;
				}
				break;
			}
			case UnsafeNativeMethods.NavigateDirection.FirstChild:
			{
				int childCount = this.GetChildCount();
				if (childCount > 0)
				{
					return this.GetChildFragment(0, direction);
				}
				return null;
			}
			case UnsafeNativeMethods.NavigateDirection.LastChild:
			{
				int childCount = this.GetChildCount();
				if (childCount > 0)
				{
					return this.GetChildFragment(childCount - 1, direction);
				}
				return null;
			}
			}
			return base.FragmentNavigate(direction);
		}

		// Token: 0x040024F5 RID: 9461
		private ToolStripDropDownItem owner;
	}
}
