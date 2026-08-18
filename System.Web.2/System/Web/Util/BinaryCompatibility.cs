using System;
using System.Runtime.Versioning;

namespace System.Web.Util
{
	// Token: 0x020001C5 RID: 453
	internal sealed class BinaryCompatibility
	{
		// Token: 0x06001736 RID: 5942 RVA: 0x00048FB8 File Offset: 0x000471B8
		static BinaryCompatibility()
		{
			TelemetryLogger.LogTargetFramework(BinaryCompatibility.Current.TargetFramework);
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00048FE8 File Offset: 0x000471E8
		public BinaryCompatibility(FrameworkName frameworkName)
		{
			Version version = VersionUtil.FrameworkDefault;
			if (frameworkName != null && frameworkName.Identifier == ".NETFramework")
			{
				version = frameworkName.Version;
			}
			this.TargetFramework = version;
			this.TargetsAtLeastFramework45 = (version >= VersionUtil.Framework45);
			this.TargetsAtLeastFramework451 = (version >= VersionUtil.Framework451);
			this.TargetsAtLeastFramework452 = (version >= VersionUtil.Framework452);
			this.TargetsAtLeastFramework46 = (version >= VersionUtil.Framework46);
			this.TargetsAtLeastFramework461 = (version >= VersionUtil.Framework461);
			this.TargetsAtLeastFramework463 = (version >= VersionUtil.Framework463);
			this.TargetsAtLeastFramework472 = (version >= VersionUtil.Framework472);
			this.TargetsAtLeastFramework48 = (version >= VersionUtil.Framework48);
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x000490B2 File Offset: 0x000472B2
		// (set) Token: 0x06001739 RID: 5945 RVA: 0x000490BA File Offset: 0x000472BA
		public bool TargetsAtLeastFramework45 { get; private set; }

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x000490C3 File Offset: 0x000472C3
		// (set) Token: 0x0600173B RID: 5947 RVA: 0x000490CB File Offset: 0x000472CB
		public bool TargetsAtLeastFramework451 { get; private set; }

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x000490D4 File Offset: 0x000472D4
		// (set) Token: 0x0600173D RID: 5949 RVA: 0x000490DC File Offset: 0x000472DC
		public bool TargetsAtLeastFramework452 { get; private set; }

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x000490E5 File Offset: 0x000472E5
		// (set) Token: 0x0600173F RID: 5951 RVA: 0x000490ED File Offset: 0x000472ED
		public bool TargetsAtLeastFramework46 { get; private set; }

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x000490F6 File Offset: 0x000472F6
		// (set) Token: 0x06001741 RID: 5953 RVA: 0x000490FE File Offset: 0x000472FE
		public bool TargetsAtLeastFramework461 { get; private set; }

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x00049107 File Offset: 0x00047307
		// (set) Token: 0x06001743 RID: 5955 RVA: 0x0004910F File Offset: 0x0004730F
		public bool TargetsAtLeastFramework463 { get; private set; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x00049118 File Offset: 0x00047318
		// (set) Token: 0x06001745 RID: 5957 RVA: 0x00049120 File Offset: 0x00047320
		public bool TargetsAtLeastFramework472 { get; private set; }

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x00049129 File Offset: 0x00047329
		// (set) Token: 0x06001747 RID: 5959 RVA: 0x00049131 File Offset: 0x00047331
		public bool TargetsAtLeastFramework48 { get; private set; }

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x0004913A File Offset: 0x0004733A
		// (set) Token: 0x06001749 RID: 5961 RVA: 0x00049142 File Offset: 0x00047342
		public Version TargetFramework { get; private set; }

		// Token: 0x040016F1 RID: 5873
		internal const string TargetFrameworkKey = "ASPNET_TARGETFRAMEWORK";

		// Token: 0x040016F2 RID: 5874
		public static readonly BinaryCompatibility Current = new BinaryCompatibility(AppDomain.CurrentDomain.GetData("ASPNET_TARGETFRAMEWORK") as FrameworkName);
	}
}
