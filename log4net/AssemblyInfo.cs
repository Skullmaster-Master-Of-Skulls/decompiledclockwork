using System;

namespace log4net
{
	// Token: 0x02000122 RID: 290
	public sealed class AssemblyInfo
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00019FF0 File Offset: 0x000181F0
		public static string Info
		{
			get
			{
				return string.Format("Apache log4net version {0} compiled for {1}{2} {3}", new object[]
				{
					"2.0.8",
					".NET Framework",
					string.Empty,
					4.5m
				});
			}
		}

		// Token: 0x04000314 RID: 788
		public const string Version = "2.0.8";

		// Token: 0x04000315 RID: 789
		public const decimal TargetFrameworkVersion = 4.5m;

		// Token: 0x04000316 RID: 790
		public const string TargetFramework = ".NET Framework";

		// Token: 0x04000317 RID: 791
		public const bool ClientProfile = false;
	}
}
