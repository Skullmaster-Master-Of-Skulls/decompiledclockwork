using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x02000295 RID: 661
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementConfigurationAttribute : ManagementMemberAttribute
	{
		// Token: 0x06001822 RID: 6178 RVA: 0x0005737E File Offset: 0x0005557E
		public ManagementConfigurationAttribute()
		{
			this.updateMode = ManagementConfigurationType.Apply;
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x0005738D File Offset: 0x0005558D
		// (set) Token: 0x06001824 RID: 6180 RVA: 0x00057395 File Offset: 0x00055595
		public ManagementConfigurationType Mode
		{
			get
			{
				return this.updateMode;
			}
			set
			{
				this.updateMode = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x0005739E File Offset: 0x0005559E
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x000573A6 File Offset: 0x000555A6
		public Type Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				this._schema = value;
			}
		}

		// Token: 0x04000B92 RID: 2962
		private ManagementConfigurationType updateMode;

		// Token: 0x04000B93 RID: 2963
		private Type _schema;
	}
}
