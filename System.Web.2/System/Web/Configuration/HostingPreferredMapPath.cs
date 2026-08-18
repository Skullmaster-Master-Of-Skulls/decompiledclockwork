using System;
using System.Web.Hosting;

namespace System.Web.Configuration
{
	// Token: 0x020006F1 RID: 1777
	internal class HostingPreferredMapPath : IConfigMapPath
	{
		// Token: 0x0600554C RID: 21836 RVA: 0x0012A8C0 File Offset: 0x00128AC0
		internal static IConfigMapPath GetInstance()
		{
			IConfigMapPath instance = IISMapPath.GetInstance();
			IConfigMapPath configMapPath = HostingEnvironment.ConfigMapPath;
			if (configMapPath == null || instance.GetType() == configMapPath.GetType())
			{
				return instance;
			}
			return new HostingPreferredMapPath(instance, configMapPath);
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x0012A8F8 File Offset: 0x00128AF8
		private HostingPreferredMapPath(IConfigMapPath iisConfigMapPath, IConfigMapPath hostingConfigMapPath)
		{
			this._iisConfigMapPath = iisConfigMapPath;
			this._hostingConfigMapPath = hostingConfigMapPath;
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x0012A910 File Offset: 0x00128B10
		public string GetMachineConfigFilename()
		{
			string machineConfigFilename = this._hostingConfigMapPath.GetMachineConfigFilename();
			if (string.IsNullOrEmpty(machineConfigFilename))
			{
				machineConfigFilename = this._iisConfigMapPath.GetMachineConfigFilename();
			}
			return machineConfigFilename;
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x0012A940 File Offset: 0x00128B40
		public string GetRootWebConfigFilename()
		{
			string rootWebConfigFilename = this._hostingConfigMapPath.GetRootWebConfigFilename();
			if (string.IsNullOrEmpty(rootWebConfigFilename))
			{
				rootWebConfigFilename = this._iisConfigMapPath.GetRootWebConfigFilename();
			}
			return rootWebConfigFilename;
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x0012A96E File Offset: 0x00128B6E
		public void GetPathConfigFilename(string siteID, string path, out string directory, out string baseName)
		{
			this._hostingConfigMapPath.GetPathConfigFilename(siteID, path, out directory, out baseName);
			if (string.IsNullOrEmpty(directory))
			{
				this._iisConfigMapPath.GetPathConfigFilename(siteID, path, out directory, out baseName);
			}
		}

		// Token: 0x06005551 RID: 21841 RVA: 0x0012A999 File Offset: 0x00128B99
		public void GetDefaultSiteNameAndID(out string siteName, out string siteID)
		{
			this._hostingConfigMapPath.GetDefaultSiteNameAndID(out siteName, out siteID);
			if (string.IsNullOrEmpty(siteID))
			{
				this._iisConfigMapPath.GetDefaultSiteNameAndID(out siteName, out siteID);
			}
		}

		// Token: 0x06005552 RID: 21842 RVA: 0x0012A9BE File Offset: 0x00128BBE
		public void ResolveSiteArgument(string siteArgument, out string siteName, out string siteID)
		{
			this._hostingConfigMapPath.ResolveSiteArgument(siteArgument, out siteName, out siteID);
			if (string.IsNullOrEmpty(siteID))
			{
				this._iisConfigMapPath.ResolveSiteArgument(siteArgument, out siteName, out siteID);
			}
		}

		// Token: 0x06005553 RID: 21843 RVA: 0x0012A9E8 File Offset: 0x00128BE8
		public string MapPath(string siteID, string path)
		{
			string text = this._hostingConfigMapPath.MapPath(siteID, path);
			if (string.IsNullOrEmpty(text))
			{
				text = this._iisConfigMapPath.MapPath(siteID, path);
			}
			return text;
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x0012AA1C File Offset: 0x00128C1C
		public string GetAppPathForPath(string siteID, string path)
		{
			string appPathForPath = this._hostingConfigMapPath.GetAppPathForPath(siteID, path);
			if (appPathForPath == null)
			{
				appPathForPath = this._iisConfigMapPath.GetAppPathForPath(siteID, path);
			}
			return appPathForPath;
		}

		// Token: 0x04002CBA RID: 11450
		private IConfigMapPath _iisConfigMapPath;

		// Token: 0x04002CBB RID: 11451
		private IConfigMapPath _hostingConfigMapPath;
	}
}
