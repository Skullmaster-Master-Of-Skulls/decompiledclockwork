using System;
using System.IO;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007BD RID: 1981
	internal class ISAPIApplicationHost : MarshalByRefObject, IApplicationHost
	{
		// Token: 0x06005EFF RID: 24319 RVA: 0x001482A0 File Offset: 0x001464A0
		internal ISAPIApplicationHost(string appIdOrVirtualPath, string physicalPath, bool validatePhysicalPath, IProcessHostSupportFunctions functions, string iisVersion = null)
		{
			this._iisVersion = iisVersion;
			this._functions = functions;
			if (this._functions == null)
			{
				ProcessHost defaultHost = ProcessHost.DefaultHost;
				if (defaultHost != null)
				{
					this._functions = defaultHost.SupportFunctions;
					if (this._functions != null)
					{
						HostingEnvironment.SupportFunctions = this._functions;
					}
				}
			}
			IServerConfig defaultDomainInstance = ServerConfig.GetDefaultDomainInstance(this._iisVersion);
			if (StringUtil.StringStartsWithIgnoreCase(appIdOrVirtualPath, "/LM/W3SVC/"))
			{
				this._appId = appIdOrVirtualPath;
				this._virtualPath = VirtualPath.Create(ISAPIApplicationHost.ExtractVPathFromAppId(this._appId));
				this._siteID = ISAPIApplicationHost.ExtractSiteIdFromAppId(this._appId);
				this._siteName = defaultDomainInstance.GetSiteNameFromSiteID(this._siteID);
			}
			else
			{
				this._virtualPath = VirtualPath.Create(appIdOrVirtualPath);
				this._appId = ISAPIApplicationHost.GetDefaultAppIdFromVPath(this._virtualPath.VirtualPathString);
				this._siteID = "1";
				this._siteName = defaultDomainInstance.GetSiteNameFromSiteID(this._siteID);
			}
			if (physicalPath == null)
			{
				this._physicalPath = defaultDomainInstance.MapPath(this, this._virtualPath);
			}
			else
			{
				this._physicalPath = physicalPath;
			}
			if (validatePhysicalPath && !Directory.Exists(this._physicalPath))
			{
				throw new HttpException(SR.GetString("Invalid_IIS_app", new object[]
				{
					appIdOrVirtualPath
				}));
			}
		}

		// Token: 0x06005F00 RID: 24320 RVA: 0x001483D6 File Offset: 0x001465D6
		internal ISAPIApplicationHost(string appIdOrVirtualPath, string physicalPath, bool validatePhysicalPath) : this(appIdOrVirtualPath, physicalPath, validatePhysicalPath, null, null)
		{
		}

		// Token: 0x06005F01 RID: 24321 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06005F02 RID: 24322 RVA: 0x001483E3 File Offset: 0x001465E3
		string IApplicationHost.GetVirtualPath()
		{
			return this._virtualPath.VirtualPathString;
		}

		// Token: 0x06005F03 RID: 24323 RVA: 0x001483F0 File Offset: 0x001465F0
		string IApplicationHost.GetPhysicalPath()
		{
			return this._physicalPath;
		}

		// Token: 0x06005F04 RID: 24324 RVA: 0x001483F8 File Offset: 0x001465F8
		IConfigMapPathFactory IApplicationHost.GetConfigMapPathFactory()
		{
			return new ISAPIConfigMapPathFactory();
		}

		// Token: 0x06005F05 RID: 24325 RVA: 0x00148400 File Offset: 0x00146600
		IntPtr IApplicationHost.GetConfigToken()
		{
			if (this._functions != null)
			{
				return this._functions.GetConfigToken(this._appId);
			}
			IntPtr result = IntPtr.Zero;
			IServerConfig defaultDomainInstance = ServerConfig.GetDefaultDomainInstance(this._iisVersion);
			string name;
			string password;
			bool uncUser = defaultDomainInstance.GetUncUser(this, this._virtualPath, out name, out password);
			if (uncUser)
			{
				try
				{
					string text;
					result = IdentitySection.CreateUserToken(name, password, out text);
				}
				catch
				{
				}
			}
			return result;
		}

		// Token: 0x06005F06 RID: 24326 RVA: 0x00148470 File Offset: 0x00146670
		string IApplicationHost.GetSiteName()
		{
			return this._siteName;
		}

		// Token: 0x06005F07 RID: 24327 RVA: 0x00148478 File Offset: 0x00146678
		string IApplicationHost.GetSiteID()
		{
			return this._siteID;
		}

		// Token: 0x06005F08 RID: 24328 RVA: 0x00006164 File Offset: 0x00004364
		void IApplicationHost.MessageReceived()
		{
		}

		// Token: 0x17001B66 RID: 7014
		// (get) Token: 0x06005F09 RID: 24329 RVA: 0x00148480 File Offset: 0x00146680
		internal string AppId
		{
			get
			{
				return this._appId;
			}
		}

		// Token: 0x06005F0A RID: 24330 RVA: 0x00148488 File Offset: 0x00146688
		private static string ExtractVPathFromAppId(string id)
		{
			int num = 0;
			for (int i = 1; i < 5; i++)
			{
				num = id.IndexOf('/', num + 1);
				if (num < 0)
				{
					break;
				}
			}
			if (num < 0)
			{
				return "/";
			}
			return id.Substring(num);
		}

		// Token: 0x06005F0B RID: 24331 RVA: 0x001484C4 File Offset: 0x001466C4
		private static string GetDefaultAppIdFromVPath(string virtualPath)
		{
			if (virtualPath.Length == 1 && virtualPath[0] == '/')
			{
				return "/LM/W3SVC/1/ROOT";
			}
			return "/LM/W3SVC/1/ROOT" + virtualPath;
		}

		// Token: 0x06005F0C RID: 24332 RVA: 0x001484EC File Offset: 0x001466EC
		private static string ExtractSiteIdFromAppId(string id)
		{
			int length = "/LM/W3SVC/".Length;
			int num = id.IndexOf('/', length);
			if (num <= 0)
			{
				return "1";
			}
			return id.Substring(length, num - length);
		}

		// Token: 0x17001B67 RID: 7015
		// (get) Token: 0x06005F0D RID: 24333 RVA: 0x00148522 File Offset: 0x00146722
		internal IProcessHostSupportFunctions SupportFunctions
		{
			get
			{
				return this._functions;
			}
		}

		// Token: 0x06005F0E RID: 24334 RVA: 0x0014852C File Offset: 0x0014672C
		internal string ResolveRootWebConfigPath()
		{
			string result = null;
			if (this._functions != null)
			{
				result = this._functions.GetRootWebConfigFilename();
			}
			return result;
		}

		// Token: 0x04003188 RID: 12680
		private string _appId;

		// Token: 0x04003189 RID: 12681
		private string _siteID;

		// Token: 0x0400318A RID: 12682
		private string _siteName;

		// Token: 0x0400318B RID: 12683
		private VirtualPath _virtualPath;

		// Token: 0x0400318C RID: 12684
		private string _physicalPath;

		// Token: 0x0400318D RID: 12685
		private IProcessHostSupportFunctions _functions;

		// Token: 0x0400318E RID: 12686
		private string _iisVersion;

		// Token: 0x0400318F RID: 12687
		private const int MAX_PATH = 260;

		// Token: 0x04003190 RID: 12688
		private const string LMW3SVC_PREFIX = "/LM/W3SVC/";

		// Token: 0x04003191 RID: 12689
		private const string DEFAULT_SITEID = "1";

		// Token: 0x04003192 RID: 12690
		private const string DEFAULT_APPID_PREFIX = "/LM/W3SVC/1/ROOT";
	}
}
