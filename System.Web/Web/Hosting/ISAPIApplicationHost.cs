using System;
using System.IO;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x0200029B RID: 667
	internal class ISAPIApplicationHost : MarshalByRefObject, IApplicationHost
	{
		// Token: 0x060022DD RID: 8925 RVA: 0x000965C0 File Offset: 0x000955C0
		internal ISAPIApplicationHost(string appIdOrVirtualPath, string physicalPath, bool validatePhysicalPath, IProcessHostSupportFunctions functions)
		{
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
			IServerConfig instance = ServerConfig.GetInstance();
			if (StringUtil.StringStartsWithIgnoreCase(appIdOrVirtualPath, "/LM/W3SVC/"))
			{
				this._appId = appIdOrVirtualPath;
				this._virtualPath = VirtualPath.Create(ISAPIApplicationHost.ExtractVPathFromAppId(this._appId));
				this._siteID = ISAPIApplicationHost.ExtractSiteIdFromAppId(this._appId);
				this._siteName = instance.GetSiteNameFromSiteID(this._siteID);
			}
			else
			{
				this._virtualPath = VirtualPath.Create(appIdOrVirtualPath);
				this._appId = ISAPIApplicationHost.GetDefaultAppIdFromVPath(this._virtualPath.VirtualPathString);
				this._siteID = "1";
				this._siteName = instance.GetSiteNameFromSiteID(this._siteID);
			}
			if (physicalPath == null)
			{
				this._physicalPath = instance.MapPath(this, this._virtualPath);
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

		// Token: 0x060022DE RID: 8926 RVA: 0x000966EA File Offset: 0x000956EA
		internal ISAPIApplicationHost(string appIdOrVirtualPath, string physicalPath, bool validatePhysicalPath) : this(appIdOrVirtualPath, physicalPath, validatePhysicalPath, null)
		{
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x000966F6 File Offset: 0x000956F6
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000966F9 File Offset: 0x000956F9
		string IApplicationHost.GetVirtualPath()
		{
			return this._virtualPath.VirtualPathString;
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x00096706 File Offset: 0x00095706
		string IApplicationHost.GetPhysicalPath()
		{
			return this._physicalPath;
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0009670E File Offset: 0x0009570E
		IConfigMapPathFactory IApplicationHost.GetConfigMapPathFactory()
		{
			return new ISAPIConfigMapPathFactory();
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x00096718 File Offset: 0x00095718
		IntPtr IApplicationHost.GetConfigToken()
		{
			if (this._functions != null)
			{
				return this._functions.GetConfigToken(this._appId);
			}
			IntPtr result = IntPtr.Zero;
			IServerConfig instance = ServerConfig.GetInstance();
			string name;
			string password;
			bool uncUser = instance.GetUncUser(this, this._virtualPath, out name, out password);
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

		// Token: 0x060022E4 RID: 8932 RVA: 0x00096784 File Offset: 0x00095784
		string IApplicationHost.GetSiteName()
		{
			return this._siteName;
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0009678C File Offset: 0x0009578C
		string IApplicationHost.GetSiteID()
		{
			return this._siteID;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x00096794 File Offset: 0x00095794
		void IApplicationHost.MessageReceived()
		{
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x00096796 File Offset: 0x00095796
		internal string AppId
		{
			get
			{
				return this._appId;
			}
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000967A0 File Offset: 0x000957A0
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

		// Token: 0x060022E9 RID: 8937 RVA: 0x000967DC File Offset: 0x000957DC
		private static string GetDefaultAppIdFromVPath(string virtualPath)
		{
			if (virtualPath.Length == 1 && virtualPath[0] == '/')
			{
				return "/LM/W3SVC/1/ROOT";
			}
			return "/LM/W3SVC/1/ROOT" + virtualPath;
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x00096804 File Offset: 0x00095804
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

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x0009683A File Offset: 0x0009583A
		internal IProcessHostSupportFunctions SupportFunctions
		{
			get
			{
				return this._functions;
			}
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x00096844 File Offset: 0x00095844
		internal string ResolveRootWebConfigPath()
		{
			string result = null;
			if (this._functions != null)
			{
				result = this._functions.GetRootWebConfigFilename();
			}
			return result;
		}

		// Token: 0x04001B71 RID: 7025
		private const int MAX_PATH = 260;

		// Token: 0x04001B72 RID: 7026
		private const string LMW3SVC_PREFIX = "/LM/W3SVC/";

		// Token: 0x04001B73 RID: 7027
		private const string DEFAULT_SITEID = "1";

		// Token: 0x04001B74 RID: 7028
		private const string DEFAULT_APPID_PREFIX = "/LM/W3SVC/1/ROOT";

		// Token: 0x04001B75 RID: 7029
		private string _appId;

		// Token: 0x04001B76 RID: 7030
		private string _siteID;

		// Token: 0x04001B77 RID: 7031
		private string _siteName;

		// Token: 0x04001B78 RID: 7032
		private VirtualPath _virtualPath;

		// Token: 0x04001B79 RID: 7033
		private string _physicalPath;

		// Token: 0x04001B7A RID: 7034
		private IProcessHostSupportFunctions _functions;
	}
}
