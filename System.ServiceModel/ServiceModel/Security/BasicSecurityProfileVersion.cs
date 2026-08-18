using System;

namespace System.ServiceModel.Security
{
	// Token: 0x02000280 RID: 640
	[__DynamicallyInvokable]
	public abstract class BasicSecurityProfileVersion
	{
		// Token: 0x06001256 RID: 4694 RVA: 0x000438BC File Offset: 0x00041ABC
		internal BasicSecurityProfileVersion()
		{
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x000438C4 File Offset: 0x00041AC4
		[__DynamicallyInvokable]
		public static BasicSecurityProfileVersion BasicSecurityProfile10
		{
			[__DynamicallyInvokable]
			get
			{
				return BasicSecurityProfileVersion.BasicSecurityProfile10BasicSecurityProfileVersion.Instance;
			}
		}

		// Token: 0x02000B1D RID: 2845
		private class BasicSecurityProfile10BasicSecurityProfileVersion : BasicSecurityProfileVersion
		{
			// Token: 0x17001A05 RID: 6661
			// (get) Token: 0x06006FA0 RID: 28576 RVA: 0x0019E476 File Offset: 0x0019C676
			public static BasicSecurityProfileVersion.BasicSecurityProfile10BasicSecurityProfileVersion Instance
			{
				get
				{
					return BasicSecurityProfileVersion.BasicSecurityProfile10BasicSecurityProfileVersion.instance;
				}
			}

			// Token: 0x06006FA1 RID: 28577 RVA: 0x0019E47D File Offset: 0x0019C67D
			public override string ToString()
			{
				return "BasicSecurityProfile10";
			}

			// Token: 0x04003FDB RID: 16347
			private static BasicSecurityProfileVersion.BasicSecurityProfile10BasicSecurityProfileVersion instance = new BasicSecurityProfileVersion.BasicSecurityProfile10BasicSecurityProfileVersion();
		}
	}
}
