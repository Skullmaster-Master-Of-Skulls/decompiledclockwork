using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x02000282 RID: 642
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class AppManagerAppDomainFactory : IAppManagerAppDomainFactory
	{
		// Token: 0x06002113 RID: 8467 RVA: 0x000912FB File Offset: 0x000902FB
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public AppManagerAppDomainFactory()
		{
			this._appManager = ApplicationManager.GetApplicationManager();
			this._appManager.Open();
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x0009131C File Offset: 0x0009031C
		[return: MarshalAs(UnmanagedType.Interface)]
		public object Create(string appId, string appPath)
		{
			object result;
			try
			{
				if (appPath[0] == '.')
				{
					FileInfo fileInfo = new FileInfo(appPath);
					appPath = fileInfo.FullName;
				}
				if (!StringUtil.StringEndsWith(appPath, '\\'))
				{
					appPath += "\\";
				}
				ISAPIApplicationHost appHost = new ISAPIApplicationHost(appId, appPath, false);
				ISAPIRuntime isapiruntime = (ISAPIRuntime)this._appManager.CreateObjectInternal(appId, typeof(ISAPIRuntime), appHost, false, null);
				isapiruntime.StartProcessing();
				result = new ObjectHandle(isapiruntime);
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x000913A8 File Offset: 0x000903A8
		public void Stop()
		{
			this._appManager.Close();
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x000913B5 File Offset: 0x000903B5
		internal static string ConstructSimpleAppName(string virtPath)
		{
			if (virtPath.Length <= 1)
			{
				return "root";
			}
			return virtPath.Substring(1).ToLower(CultureInfo.InvariantCulture).Replace('/', '_');
		}

		// Token: 0x04001AEF RID: 6895
		private ApplicationManager _appManager;
	}
}
