using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.FileStorages;
using TechnoPro.Common.Core.Membership;
using TechnoPro.Common.ICore.FileStorages;
using TechnoPro.Common.ICore.Membership;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.IoC
{
	// Token: 0x020000DF RID: 223
	public class CommonCoreConventionInjection : ConventionInjection
	{
		// Token: 0x06000882 RID: 2178 RVA: 0x00038F70 File Offset: 0x00037170
		public CommonCoreConventionInjection()
		{
			this.DefaultObjectMap = new Dictionary<Type, IcwObject>
			{
				{
					typeof(IFilesStorageManager),
					this.RetrieveIcwObject<DbFilesStorageManager>(DefaultLifetime.Transient.ToString())
				},
				{
					typeof(IUserManager),
					this.RetrieveIcwObject<UserManager>(DefaultLifetime.Singleton.ToString())
				},
				{
					typeof(IMembershipManager),
					this.RetrieveIcwObject<MembershipManager>(DefaultLifetime.Singleton.ToString())
				}
			};
		}
	}
}
