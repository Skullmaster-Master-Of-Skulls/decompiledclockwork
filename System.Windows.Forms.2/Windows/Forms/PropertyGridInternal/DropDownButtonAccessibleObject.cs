using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000501 RID: 1281
	[ComVisible(true)]
	internal class DropDownButtonAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x060053CE RID: 21454 RVA: 0x0015F10D File Offset: 0x0015D30D
		public DropDownButtonAccessibleObject(DropDownButton owningDropDownButton) : base(owningDropDownButton)
		{
			this._owningDropDownButton = owningDropDownButton;
			this._owningPropertyGrid = (owningDropDownButton.Parent as PropertyGridView);
			base.UseStdAccessibleObjects(owningDropDownButton.Handle);
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x0015F13A File Offset: 0x0015D33A
		internal override void ClearOwnerControlInternal()
		{
			this._owningPropertyGrid = null;
			this._owningDropDownButton = null;
			base.ClearOwnerControlInternal();
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x0015F150 File Offset: 0x0015D350
		public override void DoDefaultAction()
		{
			if (base.IsOwnerControlDestroyed())
			{
				return;
			}
			this._owningDropDownButton.PerformButtonClick();
		}

		// Token: 0x060053D1 RID: 21457 RVA: 0x0015F168 File Offset: 0x0015D368
		internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			if (base.IsOwnerControlDestroyed())
			{
				return null;
			}
			if (AccessibilityImprovements.Level5)
			{
				if (!this._owningDropDownButton.Visible)
				{
					return null;
				}
				GridEntry selectedGridEntry = this._owningPropertyGrid.SelectedGridEntry;
				PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject propertyDescriptorGridEntryAccessibleObject = ((selectedGridEntry != null) ? selectedGridEntry.AccessibilityObject : null) as PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject;
				if (propertyDescriptorGridEntryAccessibleObject == null)
				{
					return null;
				}
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return propertyDescriptorGridEntryAccessibleObject;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
					return propertyDescriptorGridEntryAccessibleObject.GetNextChildFragment(this);
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
					return propertyDescriptorGridEntryAccessibleObject.GetPreviousChildFragment(this);
				default:
					return base.FragmentNavigate(direction);
				}
			}
			else if (direction == UnsafeNativeMethods.NavigateDirection.Parent && this._owningPropertyGrid.SelectedGridEntry != null && this._owningDropDownButton.Visible)
			{
				GridEntry selectedGridEntry2 = this._owningPropertyGrid.SelectedGridEntry;
				if (selectedGridEntry2 == null)
				{
					return null;
				}
				return selectedGridEntry2.AccessibilityObject;
			}
			else
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.PreviousSibling)
				{
					return this._owningPropertyGrid.EditAccessibleObject;
				}
				return base.FragmentNavigate(direction);
			}
		}

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x060053D2 RID: 21458 RVA: 0x0015F230 File Offset: 0x0015D430
		internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
		{
			get
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				return this._owningPropertyGrid.AccessibilityObject;
			}
		}

		// Token: 0x060053D3 RID: 21459 RVA: 0x0015F248 File Offset: 0x0015D448
		internal override object GetPropertyValue(int propertyID)
		{
			if (propertyID <= 30005)
			{
				if (propertyID == 30003)
				{
					return 50000;
				}
				if (propertyID == 30005)
				{
					return this.Name;
				}
			}
			else
			{
				if (propertyID == 30090)
				{
					return true;
				}
				if (propertyID == 30095)
				{
					return this.Role;
				}
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x060053D4 RID: 21460 RVA: 0x000F1823 File Offset: 0x000EFA23
		internal override bool IsPatternSupported(int patternId)
		{
			return !base.IsOwnerControlDestroyed() && (patternId == 10018 || base.IsPatternSupported(patternId));
		}

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x060053D5 RID: 21461 RVA: 0x0015F2AD File Offset: 0x0015D4AD
		public override AccessibleRole Role
		{
			get
			{
				return AccessibleRole.PushButton;
			}
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x0015F2B1 File Offset: 0x0015D4B1
		internal override void SetFocus()
		{
			if (base.IsOwnerControlDestroyed())
			{
				return;
			}
			base.RaiseAutomationEvent(20005);
			base.SetFocus();
		}

		// Token: 0x040036D2 RID: 14034
		private DropDownButton _owningDropDownButton;

		// Token: 0x040036D3 RID: 14035
		private PropertyGridView _owningPropertyGrid;
	}
}
