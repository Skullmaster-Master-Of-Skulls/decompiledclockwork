using System;

namespace System.Web.UI.Design
{
	// Token: 0x0200006A RID: 106
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SupportsPreviewControlAttribute : Attribute
	{
		// Token: 0x06000319 RID: 793 RVA: 0x00010808 File Offset: 0x0000EA08
		public SupportsPreviewControlAttribute(bool supportsPreviewControl)
		{
			this._supportsPreviewControl = supportsPreviewControl;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00010817 File Offset: 0x0000EA17
		public bool SupportsPreviewControl
		{
			get
			{
				return this._supportsPreviewControl;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0001081F File Offset: 0x0000EA1F
		public override int GetHashCode()
		{
			return this._supportsPreviewControl.GetHashCode();
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0001082C File Offset: 0x0000EA2C
		public override bool IsDefaultAttribute()
		{
			return this.Equals(SupportsPreviewControlAttribute.Default);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001083C File Offset: 0x0000EA3C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			SupportsPreviewControlAttribute supportsPreviewControlAttribute = obj as SupportsPreviewControlAttribute;
			return supportsPreviewControlAttribute != null && supportsPreviewControlAttribute.SupportsPreviewControl == this._supportsPreviewControl;
		}

		// Token: 0x04000169 RID: 361
		private bool _supportsPreviewControl;

		// Token: 0x0400016A RID: 362
		public static readonly SupportsPreviewControlAttribute Default = new SupportsPreviewControlAttribute(false);
	}
}
