using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007EA RID: 2026
	[ComVisible(false)]
	public class SimpleWorkerRequest : HttpWorkerRequest
	{
		// Token: 0x0600609A RID: 24730 RVA: 0x0014DC78 File Offset: 0x0014BE78
		private void ExtractPagePathInfo()
		{
			int num = this._page.IndexOf('/');
			if (num >= 0)
			{
				this._pathInfo = this._page.Substring(num);
				this._page = this._page.Substring(0, num);
			}
		}

		// Token: 0x0600609B RID: 24731 RVA: 0x0014DCBC File Offset: 0x0014BEBC
		private string GetPathInternal(bool includePathInfo)
		{
			string text = this._appVirtPath.Equals("/") ? ("/" + this._page) : (this._appVirtPath + "/" + this._page);
			if (includePathInfo && this._pathInfo != null)
			{
				return text + this._pathInfo;
			}
			return text;
		}

		// Token: 0x0600609C RID: 24732 RVA: 0x0014DD1D File Offset: 0x0014BF1D
		public override string GetUriPath()
		{
			return this.GetPathInternal(true);
		}

		// Token: 0x0600609D RID: 24733 RVA: 0x0014DD26 File Offset: 0x0014BF26
		public override string GetQueryString()
		{
			return this._queryString;
		}

		// Token: 0x0600609E RID: 24734 RVA: 0x0014DD30 File Offset: 0x0014BF30
		public override string GetRawUrl()
		{
			string queryString = this.GetQueryString();
			if (!string.IsNullOrEmpty(queryString))
			{
				return this.GetPathInternal(true) + "?" + queryString;
			}
			return this.GetPathInternal(true);
		}

		// Token: 0x0600609F RID: 24735 RVA: 0x0014DD66 File Offset: 0x0014BF66
		public override string GetHttpVerbName()
		{
			return "GET";
		}

		// Token: 0x060060A0 RID: 24736 RVA: 0x0003634B File Offset: 0x0003454B
		public override string GetHttpVersion()
		{
			return "HTTP/1.0";
		}

		// Token: 0x060060A1 RID: 24737 RVA: 0x0014DD6D File Offset: 0x0014BF6D
		public override string GetRemoteAddress()
		{
			return "127.0.0.1";
		}

		// Token: 0x060060A2 RID: 24738 RVA: 0x00007722 File Offset: 0x00005922
		public override int GetRemotePort()
		{
			return 0;
		}

		// Token: 0x060060A3 RID: 24739 RVA: 0x0014DD6D File Offset: 0x0014BF6D
		public override string GetLocalAddress()
		{
			return "127.0.0.1";
		}

		// Token: 0x060060A4 RID: 24740 RVA: 0x00031B32 File Offset: 0x0002FD32
		public override int GetLocalPort()
		{
			return 80;
		}

		// Token: 0x060060A5 RID: 24741 RVA: 0x0002E5BA File Offset: 0x0002C7BA
		public override IntPtr GetUserToken()
		{
			return IntPtr.Zero;
		}

		// Token: 0x060060A6 RID: 24742 RVA: 0x0014DD74 File Offset: 0x0014BF74
		public override string GetFilePath()
		{
			return this.GetPathInternal(false);
		}

		// Token: 0x060060A7 RID: 24743 RVA: 0x0014DD80 File Offset: 0x0014BF80
		public override string GetFilePathTranslated()
		{
			string text = this._appPhysPath + this._page.Replace('/', '\\');
			InternalSecurityPermissions.PathDiscovery(text).Demand();
			return text;
		}

		// Token: 0x060060A8 RID: 24744 RVA: 0x0014DDB4 File Offset: 0x0014BFB4
		public override string GetPathInfo()
		{
			if (this._pathInfo == null)
			{
				return string.Empty;
			}
			return this._pathInfo;
		}

		// Token: 0x060060A9 RID: 24745 RVA: 0x0014DDCA File Offset: 0x0014BFCA
		public override string GetAppPath()
		{
			return this._appVirtPath;
		}

		// Token: 0x060060AA RID: 24746 RVA: 0x0014DDD2 File Offset: 0x0014BFD2
		public override string GetAppPathTranslated()
		{
			InternalSecurityPermissions.PathDiscovery(this._appPhysPath).Demand();
			return this._appPhysPath;
		}

		// Token: 0x060060AB RID: 24747 RVA: 0x00028752 File Offset: 0x00026952
		public override string GetServerVariable(string name)
		{
			return string.Empty;
		}

		// Token: 0x060060AC RID: 24748 RVA: 0x0014DDEC File Offset: 0x0014BFEC
		public override string MapPath(string path)
		{
			if (!this._hasRuntimeInfo)
			{
				return null;
			}
			string text = null;
			string text2 = this._appPhysPath.Substring(0, this._appPhysPath.Length - 1);
			if (string.IsNullOrEmpty(path) || path.Equals("/"))
			{
				text = text2;
			}
			if (StringUtil.StringStartsWith(path, this._appVirtPath))
			{
				text = text2 + path.Substring(this._appVirtPath.Length).Replace('/', '\\');
			}
			InternalSecurityPermissions.PathDiscovery(text).Demand();
			return text;
		}

		// Token: 0x17001B88 RID: 7048
		// (get) Token: 0x060060AD RID: 24749 RVA: 0x0014DE74 File Offset: 0x0014C074
		public override string MachineConfigPath
		{
			get
			{
				if (this._hasRuntimeInfo)
				{
					string machineConfigurationFilePath = HttpConfigurationSystem.MachineConfigurationFilePath;
					InternalSecurityPermissions.PathDiscovery(machineConfigurationFilePath).Demand();
					return machineConfigurationFilePath;
				}
				return null;
			}
		}

		// Token: 0x17001B89 RID: 7049
		// (get) Token: 0x060060AE RID: 24750 RVA: 0x0014DEA0 File Offset: 0x0014C0A0
		public override string RootWebConfigPath
		{
			get
			{
				if (this._hasRuntimeInfo)
				{
					string rootWebConfigurationFilePath = HttpConfigurationSystem.RootWebConfigurationFilePath;
					InternalSecurityPermissions.PathDiscovery(rootWebConfigurationFilePath).Demand();
					return rootWebConfigurationFilePath;
				}
				return null;
			}
		}

		// Token: 0x17001B8A RID: 7050
		// (get) Token: 0x060060AF RID: 24751 RVA: 0x0014DEC9 File Offset: 0x0014C0C9
		public override string MachineInstallDirectory
		{
			get
			{
				if (this._hasRuntimeInfo)
				{
					InternalSecurityPermissions.PathDiscovery(this._installDir).Demand();
					return this._installDir;
				}
				return null;
			}
		}

		// Token: 0x060060B0 RID: 24752 RVA: 0x00006164 File Offset: 0x00004364
		public override void SendStatus(int statusCode, string statusDescription)
		{
		}

		// Token: 0x060060B1 RID: 24753 RVA: 0x00006164 File Offset: 0x00004364
		public override void SendKnownResponseHeader(int index, string value)
		{
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x00006164 File Offset: 0x00004364
		public override void SendUnknownResponseHeader(string name, string value)
		{
		}

		// Token: 0x060060B3 RID: 24755 RVA: 0x0014DEEB File Offset: 0x0014C0EB
		public override void SendResponseFromMemory(byte[] data, int length)
		{
			this._output.Write(Encoding.Default.GetChars(data, 0, length));
		}

		// Token: 0x060060B4 RID: 24756 RVA: 0x00006164 File Offset: 0x00004364
		public override void SendResponseFromFile(string filename, long offset, long length)
		{
		}

		// Token: 0x060060B5 RID: 24757 RVA: 0x00006164 File Offset: 0x00004364
		public override void SendResponseFromFile(IntPtr handle, long offset, long length)
		{
		}

		// Token: 0x060060B6 RID: 24758 RVA: 0x00006164 File Offset: 0x00004364
		public override void FlushResponse(bool finalFlush)
		{
		}

		// Token: 0x060060B7 RID: 24759 RVA: 0x00006164 File Offset: 0x00004364
		public override void EndOfRequest()
		{
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x0014DF05 File Offset: 0x0014C105
		internal override void UpdateInitialCounters()
		{
			PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.REQUESTS_CURRENT);
			PerfCounters.IncrementCounter(AppPerfCounter.REQUESTS_TOTAL);
		}

		// Token: 0x060060B9 RID: 24761 RVA: 0x0014DF15 File Offset: 0x0014C115
		internal override void UpdateResponseCounters(bool finalFlush, int bytesOut)
		{
			if (HttpRuntime.UseIntegratedPipeline)
			{
				return;
			}
			if (finalFlush)
			{
				PerfCounters.DecrementGlobalCounter(GlobalPerfCounter.REQUESTS_CURRENT);
				PerfCounters.DecrementCounter(AppPerfCounter.REQUESTS_EXECUTING);
			}
			if (bytesOut > 0)
			{
				PerfCounters.IncrementCounterEx(AppPerfCounter.REQUEST_BYTES_OUT, bytesOut);
			}
		}

		// Token: 0x060060BA RID: 24762 RVA: 0x0014DF3C File Offset: 0x0014C13C
		internal override void UpdateRequestCounters(int bytesIn)
		{
			if (HttpRuntime.UseIntegratedPipeline)
			{
				return;
			}
			if (bytesIn > 0)
			{
				PerfCounters.IncrementCounterEx(AppPerfCounter.REQUEST_BYTES_IN, bytesIn);
			}
		}

		// Token: 0x060060BB RID: 24763 RVA: 0x0014DF52 File Offset: 0x0014C152
		private SimpleWorkerRequest()
		{
		}

		// Token: 0x060060BC RID: 24764 RVA: 0x0014DF5C File Offset: 0x0014C15C
		public SimpleWorkerRequest(string page, string query, TextWriter output) : this()
		{
			this._queryString = query;
			this._output = output;
			this._page = page;
			this.ExtractPagePathInfo();
			this._appPhysPath = Thread.GetDomain().GetData(".appPath").ToString();
			this._appVirtPath = Thread.GetDomain().GetData(".appVPath").ToString();
			this._installDir = HttpRuntime.AspInstallDirectoryInternal;
			this._hasRuntimeInfo = true;
		}

		// Token: 0x060060BD RID: 24765 RVA: 0x0014DFD0 File Offset: 0x0014C1D0
		public SimpleWorkerRequest(string appVirtualDir, string appPhysicalDir, string page, string query, TextWriter output) : this()
		{
			if (Thread.GetDomain().GetData(".appPath") != null)
			{
				throw new HttpException(SR.GetString("Wrong_SimpleWorkerRequest"));
			}
			this._appVirtPath = appVirtualDir;
			this._appPhysPath = appPhysicalDir;
			this._queryString = query;
			this._output = output;
			this._page = page;
			this.ExtractPagePathInfo();
			if (!StringUtil.StringEndsWith(this._appPhysPath, '\\'))
			{
				this._appPhysPath += "\\";
			}
			this._hasRuntimeInfo = false;
		}

		// Token: 0x04003261 RID: 12897
		private bool _hasRuntimeInfo;

		// Token: 0x04003262 RID: 12898
		private string _appVirtPath;

		// Token: 0x04003263 RID: 12899
		private string _appPhysPath;

		// Token: 0x04003264 RID: 12900
		private string _page;

		// Token: 0x04003265 RID: 12901
		private string _pathInfo;

		// Token: 0x04003266 RID: 12902
		private string _queryString;

		// Token: 0x04003267 RID: 12903
		private TextWriter _output;

		// Token: 0x04003268 RID: 12904
		private string _installDir;
	}
}
