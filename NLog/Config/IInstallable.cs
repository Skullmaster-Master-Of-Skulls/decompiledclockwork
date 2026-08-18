using System;

namespace NLog.Config
{
	// Token: 0x0200004B RID: 75
	public interface IInstallable
	{
		// Token: 0x06000160 RID: 352
		void Install(InstallationContext installationContext);

		// Token: 0x06000161 RID: 353
		void Uninstall(InstallationContext installationContext);

		// Token: 0x06000162 RID: 354
		bool? IsInstalled(InstallationContext installationContext);
	}
}
