using System;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x020002B8 RID: 696
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class AppDomainInfoEnum : IAppDomainInfoEnum
	{
		// Token: 0x0600240A RID: 9226 RVA: 0x0009A42B File Offset: 0x0009942B
		internal AppDomainInfoEnum(AppDomainInfo[] appDomainInfos)
		{
			this._appDomainInfos = appDomainInfos;
			this._curPos = -1;
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x0009A441 File Offset: 0x00099441
		public int Count()
		{
			return this._appDomainInfos.Length;
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x0009A44B File Offset: 0x0009944B
		public IAppDomainInfo GetData()
		{
			return this._appDomainInfos[this._curPos];
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x0009A45A File Offset: 0x0009945A
		public bool MoveNext()
		{
			this._curPos++;
			return this._curPos < this._appDomainInfos.Length;
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x0009A47D File Offset: 0x0009947D
		public void Reset()
		{
			this._curPos = -1;
		}

		// Token: 0x04001C2D RID: 7213
		private AppDomainInfo[] _appDomainInfos;

		// Token: 0x04001C2E RID: 7214
		private int _curPos;
	}
}
