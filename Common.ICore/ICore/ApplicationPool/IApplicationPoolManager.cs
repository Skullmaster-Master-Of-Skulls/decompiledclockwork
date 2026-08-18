using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.ICore.ApplicationPool
{
	// Token: 0x020000D3 RID: 211
	public interface IApplicationPoolManager : IBaseOperationContext<ApplicationPoolOperationContext>
	{
		// Token: 0x06000692 RID: 1682
		void SetApplicationPoolSettings(string sitename, string vDir, string appPoolName);

		// Token: 0x06000693 RID: 1683
		bool CreateApplicationPoolIfNotExists();

		// Token: 0x06000694 RID: 1684
		void SetApplicationPoolToDefaultWebSiteApplication(string vDir, params string[] protocols);

		// Token: 0x06000695 RID: 1685
		void SetApplicationPoolToWebApplication(string vDir, string siteName, params string[] protocols);

		// Token: 0x06000696 RID: 1686
		void StartApplicationPool(bool waitForStarting = false);

		// Token: 0x06000697 RID: 1687
		void StopApplicationPool(bool waitForStopping = false);

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000698 RID: 1688
		// (set) Token: 0x06000699 RID: 1689
		bool Enable32BitAppOnWin64 { get; set; }

		// Token: 0x0600069A RID: 1690
		void SetApplicationPoolRecyclingScheduler(TimeSpan ts);

		// Token: 0x0600069B RID: 1691
		void SetApplicationPoolManagedRuntimeVersion(string manageRuntimeVersion);
	}
}
