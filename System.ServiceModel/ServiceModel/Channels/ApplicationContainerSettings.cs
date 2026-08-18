using System;
using System.Globalization;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200083C RID: 2108
	public sealed class ApplicationContainerSettings
	{
		// Token: 0x06004ECA RID: 20170 RVA: 0x0011F3EA File Offset: 0x0011D5EA
		internal ApplicationContainerSettings()
		{
			this.PackageFullName = null;
			this.sessionId = -1;
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x0011F400 File Offset: 0x0011D600
		private ApplicationContainerSettings(ApplicationContainerSettings source)
		{
			this.PackageFullName = source.PackageFullName;
			this.sessionId = source.sessionId;
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x06004ECC RID: 20172 RVA: 0x0011F420 File Offset: 0x0011D620
		// (set) Token: 0x06004ECD RID: 20173 RVA: 0x0011F428 File Offset: 0x0011D628
		public string PackageFullName { get; set; }

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06004ECE RID: 20174 RVA: 0x0011F431 File Offset: 0x0011D631
		// (set) Token: 0x06004ECF RID: 20175 RVA: 0x0011F439 File Offset: 0x0011D639
		public int SessionId
		{
			get
			{
				return this.sessionId;
			}
			set
			{
				if (value < -1)
				{
					throw FxTrace.Exception.Argument("value", SR.GetString("SessionValueInvalid", new object[]
					{
						value
					}));
				}
				this.sessionId = value;
			}
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06004ED0 RID: 20176 RVA: 0x0011F46F File Offset: 0x0011D66F
		internal bool TargetingAppContainer
		{
			get
			{
				return !string.IsNullOrEmpty(this.PackageFullName);
			}
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x0011F47F File Offset: 0x0011D67F
		internal ApplicationContainerSettings Clone()
		{
			return new ApplicationContainerSettings(this);
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x0011F488 File Offset: 0x0011D688
		internal string GetConnectionGroupSuffix()
		{
			string result = string.Empty;
			if (AppContainerInfo.IsAppContainerSupported && this.TargetingAppContainer)
			{
				result = string.Format(CultureInfo.InvariantCulture, ";SessionId={0};PackageFullName={1}", new object[]
				{
					this.SessionId,
					this.PackageFullName
				});
			}
			return result;
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x0011F4D8 File Offset: 0x0011D6D8
		internal bool IsMatch(ApplicationContainerSettings applicationContainerSettings)
		{
			return applicationContainerSettings != null && !(this.PackageFullName != applicationContainerSettings.PackageFullName) && this.sessionId == applicationContainerSettings.sessionId;
		}

		// Token: 0x04003104 RID: 12548
		public const int CurrentSession = -1;

		// Token: 0x04003105 RID: 12549
		public const int ServiceSession = 0;

		// Token: 0x04003106 RID: 12550
		private const string GroupNameSuffixFormat = ";SessionId={0};PackageFullName={1}";

		// Token: 0x04003107 RID: 12551
		private int sessionId;
	}
}
