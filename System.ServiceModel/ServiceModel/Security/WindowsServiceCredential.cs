using System;

namespace System.ServiceModel.Security
{
	// Token: 0x02000341 RID: 833
	public sealed class WindowsServiceCredential
	{
		// Token: 0x06001E38 RID: 7736 RVA: 0x00070233 File Offset: 0x0006E433
		internal WindowsServiceCredential()
		{
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00070242 File Offset: 0x0006E442
		internal WindowsServiceCredential(WindowsServiceCredential other)
		{
			this.allowAnonymousLogons = other.allowAnonymousLogons;
			this.includeWindowsGroups = other.includeWindowsGroups;
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x00070275 File Offset: 0x0006E475
		// (set) Token: 0x06001E3B RID: 7739 RVA: 0x0007027D File Offset: 0x0006E47D
		public bool AllowAnonymousLogons
		{
			get
			{
				return this.allowAnonymousLogons;
			}
			set
			{
				this.ThrowIfImmutable();
				this.allowAnonymousLogons = value;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x0007028C File Offset: 0x0006E48C
		// (set) Token: 0x06001E3D RID: 7741 RVA: 0x00070294 File Offset: 0x0006E494
		public bool IncludeWindowsGroups
		{
			get
			{
				return this.includeWindowsGroups;
			}
			set
			{
				this.ThrowIfImmutable();
				this.includeWindowsGroups = value;
			}
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x000702A3 File Offset: 0x0006E4A3
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000702AC File Offset: 0x0006E4AC
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001E72 RID: 7794
		private bool allowAnonymousLogons;

		// Token: 0x04001E73 RID: 7795
		private bool includeWindowsGroups = true;

		// Token: 0x04001E74 RID: 7796
		private bool isReadOnly;
	}
}
