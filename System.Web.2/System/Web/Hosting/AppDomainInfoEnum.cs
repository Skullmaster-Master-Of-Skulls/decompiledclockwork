using System;

namespace System.Web.Hosting
{
	// Token: 0x020007DE RID: 2014
	public class AppDomainInfoEnum : IAppDomainInfoEnum
	{
		// Token: 0x0600604A RID: 24650 RVA: 0x0014C74C File Offset: 0x0014A94C
		internal AppDomainInfoEnum(AppDomainInfo[] appDomainInfos)
		{
			this._appDomainInfos = appDomainInfos;
			this._curPos = -1;
		}

		// Token: 0x0600604B RID: 24651 RVA: 0x0014C762 File Offset: 0x0014A962
		public int Count()
		{
			return this._appDomainInfos.Length;
		}

		// Token: 0x0600604C RID: 24652 RVA: 0x0014C76C File Offset: 0x0014A96C
		public IAppDomainInfo GetData()
		{
			return this._appDomainInfos[this._curPos];
		}

		// Token: 0x0600604D RID: 24653 RVA: 0x0014C77B File Offset: 0x0014A97B
		public bool MoveNext()
		{
			this._curPos++;
			return this._curPos < this._appDomainInfos.Length;
		}

		// Token: 0x0600604E RID: 24654 RVA: 0x0014C79E File Offset: 0x0014A99E
		public void Reset()
		{
			this._curPos = -1;
		}

		// Token: 0x04003249 RID: 12873
		private AppDomainInfo[] _appDomainInfos;

		// Token: 0x0400324A RID: 12874
		private int _curPos;
	}
}
