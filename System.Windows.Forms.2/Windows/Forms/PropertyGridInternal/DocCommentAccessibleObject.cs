using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020004FE RID: 1278
	[ComVisible(true)]
	internal class DocCommentAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x060053B9 RID: 21433 RVA: 0x0015ECEC File Offset: 0x0015CEEC
		public DocCommentAccessibleObject(DocComment owningDocComment, PropertyGrid parentPropertyGrid) : base(owningDocComment)
		{
			this._parentPropertyGrid = parentPropertyGrid;
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x0015ECFC File Offset: 0x0015CEFC
		internal override void ClearOwnerControlInternal()
		{
			this._parentPropertyGrid = null;
			base.ClearOwnerControlInternal();
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x0015ED0C File Offset: 0x0015CF0C
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

		// Token: 0x060053BC RID: 21436 RVA: 0x0015ED4C File Offset: 0x0015CF4C
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

		// Token: 0x040036CF RID: 14031
		private PropertyGrid _parentPropertyGrid;
	}
}
