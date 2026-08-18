using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x0200050C RID: 1292
	[ComVisible(true)]
	internal class HotCommandsAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x060054C2 RID: 21698 RVA: 0x00163512 File Offset: 0x00161712
		public HotCommandsAccessibleObject(HotCommands owningHotCommands, PropertyGrid parentPropertyGrid) : base(owningHotCommands)
		{
			this._parentPropertyGrid = parentPropertyGrid;
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x00163522 File Offset: 0x00161722
		internal override void ClearOwnerControlInternal()
		{
			this._parentPropertyGrid = null;
			base.ClearOwnerControlInternal();
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x00163534 File Offset: 0x00161734
		internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			if (base.IsOwnerControlDestroyed())
			{
				return null;
			}
			PropertyGridAccessibleObject propertyGridAccessibleObject = this._parentPropertyGrid.AccessibilityObject as PropertyGridAccessibleObject;
			if (propertyGridAccessibleObject != null)
			{
				UnsafeNativeMethods.IRawElementProviderFragment rawElementProviderFragment = propertyGridAccessibleObject.ChildFragmentNavigate(this, direction);
				if (rawElementProviderFragment != null)
				{
					return rawElementProviderFragment;
				}
			}
			return base.FragmentNavigate(direction);
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x0015ED4C File Offset: 0x0015CF4C
		internal override object GetPropertyValue(int propertyID)
		{
			if (propertyID == 30003)
			{
				return 50033;
			}
			if (propertyID == 30005)
			{
				return this.Name;
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x04003729 RID: 14121
		private PropertyGrid _parentPropertyGrid;
	}
}
