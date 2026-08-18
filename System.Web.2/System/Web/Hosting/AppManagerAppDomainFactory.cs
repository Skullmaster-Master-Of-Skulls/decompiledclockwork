using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007A0 RID: 1952
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public sealed class AppManagerAppDomainFactory : IAppManagerAppDomainFactory
	{
		// Token: 0x06005CB5 RID: 23733 RVA: 0x00140990 File Offset: 0x0013EB90
		public AppManagerAppDomainFactory()
		{
			this._appManager = ApplicationManager.GetApplicationManager();
			this._appManager.Open();
		}

		// Token: 0x06005CB6 RID: 23734 RVA: 0x001409B0 File Offset: 0x0013EBB0
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
			catch (Exception ex)
			{
				throw;
			}
			return result;
		}

		// Token: 0x06005CB7 RID: 23735 RVA: 0x00140A3C File Offset: 0x0013EC3C
		public void Stop()
		{
			this._appManager.Close();
		}

		// Token: 0x06005CB8 RID: 23736 RVA: 0x00140A4C File Offset: 0x0013EC4C
		internal static string ConstructSimpleAppName(string virtPath, bool isDevEnvironment)
		{
			string tempFilesPostfix = AppManagerAppDomainFactory.GetTempFilesPostfix();
			if (virtPath.Length > 1)
			{
				return (virtPath.Substring(1) + tempFilesPostfix).ToLower(CultureInfo.InvariantCulture).Replace('/', '_');
			}
			if (!BuildManagerHost.InClientBuildManager && isDevEnvironment)
			{
				return "vs" + tempFilesPostfix;
			}
			return "root" + tempFilesPostfix;
		}

		// Token: 0x06005CB9 RID: 23737 RVA: 0x00140AAC File Offset: 0x0013ECAC
		private static string GetTempFilesPostfix()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("XSP_TEMPDIR_POSTFIX", EnvironmentVariableTarget.Process);
			if (!AppManagerAppDomainFactory.IsValidPostfix(environmentVariable))
			{
				return "";
			}
			return environmentVariable;
		}

		// Token: 0x06005CBA RID: 23738 RVA: 0x00140AD4 File Offset: 0x0013ECD4
		private static bool IsValidPostfix(string postfix)
		{
			if (string.IsNullOrWhiteSpace(postfix))
			{
				return false;
			}
			char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
			return postfix.IndexOfAny(invalidFileNameChars) < 0;
		}

		// Token: 0x040030D3 RID: 12499
		private ApplicationManager _appManager;
	}
}
