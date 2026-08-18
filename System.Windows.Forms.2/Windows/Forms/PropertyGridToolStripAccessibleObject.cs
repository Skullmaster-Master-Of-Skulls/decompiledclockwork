using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000330 RID: 816
	[ComVisible(true)]
	internal class PropertyGridToolStripAccessibleObject : ToolStrip.ToolStripAccessibleObject
	{
		// Token: 0x06003529 RID: 13609 RVA: 0x000F185D File Offset: 0x000EFA5D
		public PropertyGridToolStripAccessibleObject(PropertyGridToolStrip owningPropertyGridToolStrip, PropertyGrid parentPropertyGrid) : base(owningPropertyGridToolStrip)
		{
			this._parentPropertyGrid = parentPropertyGrid;
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x000F186D File Offset: 0x000EFA6D
		internal override void ClearOwnerControlInternal()
		{
			this._parentPropertyGrid = null;
			base.ClearOwnerControlInternal();
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000F187C File Offset: 0x000EFA7C
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

		// Token: 0x0600352C RID: 13612 RVA: 0x000F18BC File Offset: 0x000EFABC
		internal override object GetPropertyValue(int propertyID)
		{
			if (propertyID == 30003)
			{
				return 50021;
			}
			if (propertyID == 30005)
			{
				return this.Name;
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x04001F46 RID: 8006
		private PropertyGrid _parentPropertyGrid;
	}
}
