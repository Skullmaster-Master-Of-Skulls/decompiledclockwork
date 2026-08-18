using System;
using System.Collections.Generic;

namespace NLog.Internal.Fakeables
{
	// Token: 0x02000082 RID: 130
	public class AppDomainWrapper : IAppDomain
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x000095D0 File Offset: 0x000077D0
		public AppDomainWrapper(AppDomain appDomain)
		{
			this.BaseDirectory = appDomain.BaseDirectory;
			this.ConfigurationFile = appDomain.SetupInformation.ConfigurationFile;
			string privateBinPath = appDomain.SetupInformation.PrivateBinPath;
			this.PrivateBinPath = (string.IsNullOrEmpty(privateBinPath) ? new string[0] : appDomain.SetupInformation.PrivateBinPath.Split(new char[]
			{
				';'
			}, StringSplitOptions.RemoveEmptyEntries));
			this.FriendlyName = appDomain.FriendlyName;
			this.Id = appDomain.Id;
			appDomain.ProcessExit += this.OnProcessExit;
			appDomain.DomainUnload += this.OnDomainUnload;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000967C File Offset: 0x0000787C
		public static AppDomainWrapper CurrentDomain
		{
			get
			{
				return new AppDomainWrapper(AppDomain.CurrentDomain);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00009688 File Offset: 0x00007888
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x00009690 File Offset: 0x00007890
		public string BaseDirectory { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00009699 File Offset: 0x00007899
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x000096A1 File Offset: 0x000078A1
		public string ConfigurationFile { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000096AA File Offset: 0x000078AA
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x000096B2 File Offset: 0x000078B2
		public IEnumerable<string> PrivateBinPath { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x000096BB File Offset: 0x000078BB
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x000096C3 File Offset: 0x000078C3
		public string FriendlyName { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x000096CC File Offset: 0x000078CC
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x000096D4 File Offset: 0x000078D4
		public int Id { get; private set; }

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000447 RID: 1095 RVA: 0x000096E0 File Offset: 0x000078E0
		// (remove) Token: 0x06000448 RID: 1096 RVA: 0x00009718 File Offset: 0x00007918
		public event EventHandler<EventArgs> ProcessExit;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000449 RID: 1097 RVA: 0x00009750 File Offset: 0x00007950
		// (remove) Token: 0x0600044A RID: 1098 RVA: 0x00009788 File Offset: 0x00007988
		public event EventHandler<EventArgs> DomainUnload;

		// Token: 0x0600044B RID: 1099 RVA: 0x000097C0 File Offset: 0x000079C0
		private void OnDomainUnload(object sender, EventArgs e)
		{
			EventHandler<EventArgs> domainUnload = this.DomainUnload;
			if (domainUnload != null)
			{
				domainUnload(sender, e);
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000097E0 File Offset: 0x000079E0
		private void OnProcessExit(object sender, EventArgs eventArgs)
		{
			EventHandler<EventArgs> processExit = this.ProcessExit;
			if (processExit != null)
			{
				processExit(sender, eventArgs);
			}
		}
	}
}
