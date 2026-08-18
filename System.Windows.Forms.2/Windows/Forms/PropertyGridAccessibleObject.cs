using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200032E RID: 814
	[ComVisible(true)]
	internal class PropertyGridAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x0600351A RID: 13594 RVA: 0x000F1587 File Offset: 0x000EF787
		public PropertyGridAccessibleObject(PropertyGrid owningPropertyGrid) : base(owningPropertyGrid)
		{
			this._owningPropertyGrid = owningPropertyGrid;
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x000F1597 File Offset: 0x000EF797
		internal override void ClearOwnerControlInternal()
		{
			this._owningPropertyGrid = null;
			base.ClearOwnerControlInternal();
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x000F15A8 File Offset: 0x000EF7A8
		internal override UnsafeNativeMethods.IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
		{
			if (base.IsOwnerControlDestroyed())
			{
				return null;
			}
			Point point = this._owningPropertyGrid.PointToClient(new Point((int)x, (int)y));
			Control elementFromPoint = this._owningPropertyGrid.GetElementFromPoint(point);
			if (elementFromPoint != null)
			{
				return elementFromPoint.AccessibilityObject;
			}
			return base.ElementProviderFromPoint(x, y);
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x000F15F4 File Offset: 0x000EF7F4
		internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			if (direction != UnsafeNativeMethods.NavigateDirection.FirstChild)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.LastChild)
				{
					int childFragmentCount = this.GetChildFragmentCount();
					if (childFragmentCount > 0)
					{
						return this.GetChildFragment(childFragmentCount - 1);
					}
				}
				return base.FragmentNavigate(direction);
			}
			return this.GetChildFragment(0);
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x000F1630 File Offset: 0x000EF830
		internal UnsafeNativeMethods.IRawElementProviderFragment ChildFragmentNavigate(AccessibleObject childFragment, UnsafeNativeMethods.NavigateDirection direction)
		{
			switch (direction)
			{
			case UnsafeNativeMethods.NavigateDirection.Parent:
				return this;
			case UnsafeNativeMethods.NavigateDirection.NextSibling:
			{
				int childFragmentCount = this.GetChildFragmentCount();
				int childFragmentIndex = this.GetChildFragmentIndex(childFragment);
				int num = childFragmentIndex + 1;
				if (childFragmentCount > num)
				{
					return this.GetChildFragment(num);
				}
				return null;
			}
			case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
			{
				int childFragmentCount = this.GetChildFragmentCount();
				int childFragmentIndex = this.GetChildFragmentIndex(childFragment);
				if (childFragmentIndex > 0)
				{
					return this.GetChildFragment(childFragmentIndex - 1);
				}
				return null;
			}
			default:
				return null;
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x000F1694 File Offset: 0x000EF894
		internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
		{
			get
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				ToolStripControlHost toolStripControlHost = base.Owner.ToolStripControlHost;
				ToolStrip toolStrip = (toolStripControlHost != null) ? toolStripControlHost.Owner : null;
				if (toolStrip != null && toolStrip.IsHandleCreated)
				{
					return toolStrip.AccessibilityObject;
				}
				return this;
			}
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000F16D8 File Offset: 0x000EF8D8
		internal AccessibleObject GetChildFragment(int index)
		{
			if (base.IsOwnerControlDestroyed() || index < 0)
			{
				return null;
			}
			if (this._owningPropertyGrid.ToolbarVisible)
			{
				if (index == 0)
				{
					return this._owningPropertyGrid.ToolbarAccessibleObject;
				}
				index--;
			}
			if (this._owningPropertyGrid.GridViewVisible)
			{
				if (index == 0)
				{
					return this._owningPropertyGrid.GridViewAccessibleObject;
				}
				index--;
			}
			if (this._owningPropertyGrid.CommandsVisible)
			{
				if (index == 0)
				{
					return this._owningPropertyGrid.HotCommandsAccessibleObject;
				}
				index--;
			}
			if (this._owningPropertyGrid.HelpVisible && index == 0)
			{
				return this._owningPropertyGrid.HelpAccessibleObject;
			}
			return null;
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000F1774 File Offset: 0x000EF974
		internal int GetChildFragmentCount()
		{
			if (base.IsOwnerControlDestroyed())
			{
				return 0;
			}
			int num = 0;
			if (this._owningPropertyGrid.ToolbarVisible)
			{
				num++;
			}
			if (this._owningPropertyGrid.GridViewVisible)
			{
				num++;
			}
			if (this._owningPropertyGrid.CommandsVisible)
			{
				num++;
			}
			if (this._owningPropertyGrid.HelpVisible)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000F17D2 File Offset: 0x000EF9D2
		internal override UnsafeNativeMethods.IRawElementProviderFragment GetFocus()
		{
			return this.GetFocused();
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000F17DC File Offset: 0x000EF9DC
		internal int GetChildFragmentIndex(AccessibleObject controlAccessibleObject)
		{
			int childFragmentCount = this.GetChildFragmentCount();
			for (int i = 0; i < childFragmentCount; i++)
			{
				AccessibleObject childFragment = this.GetChildFragment(i);
				if (childFragment == controlAccessibleObject)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000F180B File Offset: 0x000EFA0B
		internal override object GetPropertyValue(int propertyID)
		{
			if (propertyID == 30005)
			{
				return this.Name;
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000F1823 File Offset: 0x000EFA23
		internal override bool IsPatternSupported(int patternId)
		{
			return !base.IsOwnerControlDestroyed() && (patternId == 10018 || base.IsPatternSupported(patternId));
		}

		// Token: 0x04001F44 RID: 8004
		private PropertyGrid _owningPropertyGrid;
	}
}
