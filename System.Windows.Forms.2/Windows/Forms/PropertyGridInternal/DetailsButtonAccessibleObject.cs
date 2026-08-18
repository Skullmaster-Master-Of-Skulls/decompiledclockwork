using System;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x02000509 RID: 1289
	internal class DetailsButtonAccessibleObject : Control.ControlAccessibleObject
	{
		// Token: 0x0600549C RID: 21660 RVA: 0x00162A7E File Offset: 0x00160C7E
		public DetailsButtonAccessibleObject(DetailsButton owner) : base(owner)
		{
			this.ownerItem = owner;
		}

		// Token: 0x0600549D RID: 21661 RVA: 0x00162A8E File Offset: 0x00160C8E
		internal override void ClearOwnerControlInternal()
		{
			this.ownerItem = null;
			base.ClearOwnerControlInternal();
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x00162A9D File Offset: 0x00160C9D
		internal override bool IsIAccessibleExSupported()
		{
			return !base.IsOwnerControlDestroyed();
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x00162AA8 File Offset: 0x00160CA8
		internal override object GetPropertyValue(int propertyID)
		{
			if (propertyID == 30003)
			{
				return 50000;
			}
			return base.GetPropertyValue(propertyID);
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x00162AC4 File Offset: 0x00160CC4
		internal override bool IsPatternSupported(int patternId)
		{
			return !base.IsOwnerControlDestroyed() && (patternId == 10005 || base.IsPatternSupported(patternId));
		}

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x060054A1 RID: 21665 RVA: 0x00162AE1 File Offset: 0x00160CE1
		internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
		{
			get
			{
				if (base.IsOwnerControlDestroyed() || !this.ownerItem.Expanded)
				{
					return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
				}
				return UnsafeNativeMethods.ExpandCollapseState.Expanded;
			}
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x00162AFB File Offset: 0x00160CFB
		internal override void Expand()
		{
			if (this.ownerItem != null && !this.ownerItem.Expanded)
			{
				this.DoDefaultAction();
			}
		}

		// Token: 0x060054A3 RID: 21667 RVA: 0x00162B18 File Offset: 0x00160D18
		internal override void Collapse()
		{
			if (this.ownerItem != null && this.ownerItem.Expanded)
			{
				this.DoDefaultAction();
			}
		}

		// Token: 0x0400371C RID: 14108
		private DetailsButton ownerItem;
	}
}
