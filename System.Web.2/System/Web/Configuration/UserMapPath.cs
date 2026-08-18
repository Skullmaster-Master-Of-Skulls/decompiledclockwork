using System;
using System.Configuration;
using System.IO;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x0200076C RID: 1900
	public class UserMapPath : IConfigMapPath
	{
		// Token: 0x06005B8E RID: 23438 RVA: 0x0013D4CA File Offset: 0x0013B6CA
		public UserMapPath(ConfigurationFileMap fileMap) : this(fileMap, true)
		{
		}

		// Token: 0x06005B8F RID: 23439 RVA: 0x0013D4D4 File Offset: 0x0013B6D4
		internal UserMapPath(ConfigurationFileMap fileMap, bool pathsAreLocal)
		{
			this._pathsAreLocal = pathsAreLocal;
			if (!string.IsNullOrEmpty(fileMap.MachineConfigFilename))
			{
				if (this._pathsAreLocal)
				{
					this._machineConfigFilename = Path.GetFullPath(fileMap.MachineConfigFilename);
				}
				else
				{
					this._machineConfigFilename = fileMap.MachineConfigFilename;
				}
			}
			if (string.IsNullOrEmpty(this._machineConfigFilename))
			{
				this._machineConfigFilename = HttpConfigurationSystem.MachineConfigurationFilePath;
				this._rootWebConfigFilename = HttpConfigurationSystem.RootWebConfigurationFilePath;
			}
			else
			{
				this._rootWebConfigFilename = Path.Combine(Path.GetDirectoryName(this._machineConfigFilename), "web.config");
			}
			this._webFileMap = (fileMap as WebConfigurationFileMap);
			if (this._webFileMap != null)
			{
				if (!string.IsNullOrEmpty(this._webFileMap.Site))
				{
					this._siteName = this._webFileMap.Site;
					this._siteID = this._webFileMap.Site;
				}
				else
				{
					this._siteName = WebConfigurationHost.DefaultSiteName;
					this._siteID = "1";
				}
				if (this._pathsAreLocal)
				{
					foreach (object obj in this._webFileMap.VirtualDirectories)
					{
						string virtualDirectory = (string)obj;
						VirtualDirectoryMapping virtualDirectoryMapping = this._webFileMap.VirtualDirectories[virtualDirectory];
						virtualDirectoryMapping.Validate();
					}
				}
				VirtualDirectoryMapping virtualDirectoryMapping2 = this._webFileMap.VirtualDirectories[null];
				if (virtualDirectoryMapping2 != null)
				{
					this._rootWebConfigFilename = Path.Combine(virtualDirectoryMapping2.PhysicalDirectory, virtualDirectoryMapping2.ConfigFileBaseName);
					this._webFileMap.VirtualDirectories.Remove(null);
				}
			}
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x0013D670 File Offset: 0x0013B870
		private bool IsSiteMatch(string site)
		{
			return string.IsNullOrEmpty(site) || StringUtil.EqualsIgnoreCase(site, this._siteName) || StringUtil.EqualsIgnoreCase(site, this._siteID);
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x0013D698 File Offset: 0x0013B898
		private VirtualDirectoryMapping GetPathMapping(VirtualPath path, bool onlyApps)
		{
			if (this._webFileMap == null)
			{
				return null;
			}
			string text = path.VirtualPathStringNoTrailingSlash;
			VirtualDirectoryMapping virtualDirectoryMapping;
			for (;;)
			{
				virtualDirectoryMapping = this._webFileMap.VirtualDirectories[text];
				if (virtualDirectoryMapping != null && (!onlyApps || virtualDirectoryMapping.IsAppRoot))
				{
					break;
				}
				if (text == "/")
				{
					goto Block_4;
				}
				int num = text.LastIndexOf('/');
				if (num == 0)
				{
					text = "/";
				}
				else
				{
					text = text.Substring(0, num);
				}
			}
			return virtualDirectoryMapping;
			Block_4:
			return null;
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x0013D708 File Offset: 0x0013B908
		private string GetPhysicalPathForPath(string path, VirtualDirectoryMapping mapping)
		{
			int length = mapping.VirtualDirectory.Length;
			string text;
			if (path.Length == length)
			{
				text = mapping.PhysicalDirectory;
			}
			else
			{
				string text2;
				if (path[length] == '/')
				{
					text2 = path.Substring(length + 1);
				}
				else
				{
					text2 = path.Substring(length);
				}
				text2 = text2.Replace('/', '\\');
				text = Path.Combine(mapping.PhysicalDirectory, text2);
			}
			if (this._pathsAreLocal && FileUtil.IsSuspiciousPhysicalPath(text))
			{
				throw new HttpException(SR.GetString("Cannot_map_path", new object[]
				{
					path
				}));
			}
			return text;
		}

		// Token: 0x06005B93 RID: 23443 RVA: 0x0013D795 File Offset: 0x0013B995
		public string GetMachineConfigFilename()
		{
			return this._machineConfigFilename;
		}

		// Token: 0x06005B94 RID: 23444 RVA: 0x0013D79D File Offset: 0x0013B99D
		public string GetRootWebConfigFilename()
		{
			return this._rootWebConfigFilename;
		}

		// Token: 0x06005B95 RID: 23445 RVA: 0x0013D7A5 File Offset: 0x0013B9A5
		public void GetPathConfigFilename(string siteID, string path, out string directory, out string baseName)
		{
			this.GetPathConfigFilename(siteID, VirtualPath.Create(path), out directory, out baseName);
		}

		// Token: 0x06005B96 RID: 23446 RVA: 0x0013D7B8 File Offset: 0x0013B9B8
		private void GetPathConfigFilename(string siteID, VirtualPath path, out string directory, out string baseName)
		{
			directory = null;
			baseName = null;
			if (!this.IsSiteMatch(siteID))
			{
				return;
			}
			VirtualDirectoryMapping pathMapping = this.GetPathMapping(path, false);
			if (pathMapping == null)
			{
				return;
			}
			directory = this.GetPhysicalPathForPath(path.VirtualPathString, pathMapping);
			if (directory == null)
			{
				return;
			}
			baseName = pathMapping.ConfigFileBaseName;
		}

		// Token: 0x06005B97 RID: 23447 RVA: 0x0013D800 File Offset: 0x0013BA00
		public void GetDefaultSiteNameAndID(out string siteName, out string siteID)
		{
			siteName = this._siteName;
			siteID = this._siteID;
		}

		// Token: 0x06005B98 RID: 23448 RVA: 0x0013D812 File Offset: 0x0013BA12
		public void ResolveSiteArgument(string siteArgument, out string siteName, out string siteID)
		{
			if (this.IsSiteMatch(siteArgument))
			{
				siteName = this._siteName;
				siteID = this._siteID;
				return;
			}
			siteName = siteArgument;
			siteID = null;
		}

		// Token: 0x06005B99 RID: 23449 RVA: 0x0013D834 File Offset: 0x0013BA34
		public string MapPath(string siteID, string path)
		{
			return this.MapPath(siteID, VirtualPath.Create(path));
		}

		// Token: 0x06005B9A RID: 23450 RVA: 0x0013D844 File Offset: 0x0013BA44
		private string MapPath(string siteID, VirtualPath path)
		{
			string result;
			string text;
			this.GetPathConfigFilename(siteID, path, out result, out text);
			return result;
		}

		// Token: 0x06005B9B RID: 23451 RVA: 0x0013D860 File Offset: 0x0013BA60
		public string GetAppPathForPath(string siteID, string path)
		{
			VirtualPath appPathForPath = this.GetAppPathForPath(siteID, VirtualPath.Create(path));
			if (appPathForPath == null)
			{
				return null;
			}
			return appPathForPath.VirtualPathString;
		}

		// Token: 0x06005B9C RID: 23452 RVA: 0x0013D88C File Offset: 0x0013BA8C
		private VirtualPath GetAppPathForPath(string siteID, VirtualPath path)
		{
			if (!this.IsSiteMatch(siteID))
			{
				return null;
			}
			VirtualDirectoryMapping pathMapping = this.GetPathMapping(path, true);
			if (pathMapping == null)
			{
				return null;
			}
			return pathMapping.VirtualDirectoryObject;
		}

		// Token: 0x04003040 RID: 12352
		private string _machineConfigFilename;

		// Token: 0x04003041 RID: 12353
		private string _rootWebConfigFilename;

		// Token: 0x04003042 RID: 12354
		private string _siteName;

		// Token: 0x04003043 RID: 12355
		private string _siteID;

		// Token: 0x04003044 RID: 12356
		private WebConfigurationFileMap _webFileMap;

		// Token: 0x04003045 RID: 12357
		private bool _pathsAreLocal;
	}
}
