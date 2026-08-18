using System;
using System.Security.Permissions;
using System.Threading;

namespace System.Web.Management
{
	// Token: 0x0200019A RID: 410
	public sealed class WebApplicationInformation
	{
		// Token: 0x060015BD RID: 5565 RVA: 0x00042E98 File Offset: 0x00041098
		internal WebApplicationInformation()
		{
			this._appDomain = Thread.GetDomain().FriendlyName;
			this._trustLevel = HttpRuntime.TrustLevel;
			this._appUrl = HttpRuntime.AppDomainAppVirtualPath;
			try
			{
				this._appPath = HttpRuntime.AppDomainAppPathInternal;
			}
			catch
			{
				this._appPath = null;
			}
			this._machineName = this.GetMachineNameWithAssert();
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x00042F04 File Offset: 0x00041104
		public string ApplicationDomain
		{
			get
			{
				return this._appDomain;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x00042F0C File Offset: 0x0004110C
		public string TrustLevel
		{
			get
			{
				return this._trustLevel;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00042F14 File Offset: 0x00041114
		public string ApplicationVirtualPath
		{
			get
			{
				return this._appUrl;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00042F1C File Offset: 0x0004111C
		public string ApplicationPath
		{
			get
			{
				return this._appPath;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00042F24 File Offset: 0x00041124
		public string MachineName
		{
			get
			{
				return this._machineName;
			}
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00042F2C File Offset: 0x0004112C
		public void FormatToString(WebEventFormatter formatter)
		{
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_application_domain", this.ApplicationDomain));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_trust_level", this.TrustLevel));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_application_virtual_path", this.ApplicationVirtualPath));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_application_path", this.ApplicationPath));
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_machine_name", this.MachineName));
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00042FA7 File Offset: 0x000411A7
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private string GetMachineNameWithAssert()
		{
			return Environment.MachineName;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00042FB0 File Offset: 0x000411B0
		public override string ToString()
		{
			WebEventFormatter webEventFormatter = new WebEventFormatter();
			this.FormatToString(webEventFormatter);
			return webEventFormatter.ToString();
		}

		// Token: 0x0400164C RID: 5708
		private string _appDomain;

		// Token: 0x0400164D RID: 5709
		private string _trustLevel;

		// Token: 0x0400164E RID: 5710
		private string _appUrl;

		// Token: 0x0400164F RID: 5711
		private string _appPath;

		// Token: 0x04001650 RID: 5712
		private string _machineName;
	}
}
