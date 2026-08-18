using System;
using System.Reflection;

namespace System.Web.UI
{
	// Token: 0x0200007C RID: 124
	public class ScriptResourceDefinition : IScriptResourceDefinition
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0001976B File Offset: 0x0001796B
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x0001977C File Offset: 0x0001797C
		public string CdnDebugPath
		{
			get
			{
				return this._cdnDebugPath ?? string.Empty;
			}
			set
			{
				this._cdnDebugPath = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00019785 File Offset: 0x00017985
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00019796 File Offset: 0x00017996
		public string CdnPath
		{
			get
			{
				return this._cdnPath ?? string.Empty;
			}
			set
			{
				this._cdnPath = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0001979F File Offset: 0x0001799F
		internal string CdnDebugPathSecureConnection
		{
			get
			{
				if (this._cdnDebugPathSecureConnection == null)
				{
					this._cdnDebugPathSecureConnection = this.GetSecureCdnPath(this.CdnDebugPath);
				}
				return this._cdnDebugPathSecureConnection;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000197C1 File Offset: 0x000179C1
		internal string CdnPathSecureConnection
		{
			get
			{
				if (this._cdnPathSecureConnection == null)
				{
					this._cdnPathSecureConnection = this.GetSecureCdnPath(this.CdnPath);
				}
				return this._cdnPathSecureConnection;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000197E3 File Offset: 0x000179E3
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x000197EB File Offset: 0x000179EB
		public bool CdnSupportsSecureConnection
		{
			get
			{
				return this._cdnSupportsSecureConnection;
			}
			set
			{
				this._cdnSupportsSecureConnection = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x000197F4 File Offset: 0x000179F4
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00019805 File Offset: 0x00017A05
		public string LoadSuccessExpression
		{
			get
			{
				return this._loadSuccessExpression ?? string.Empty;
			}
			set
			{
				this._loadSuccessExpression = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001980E File Offset: 0x00017A0E
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x0001981F File Offset: 0x00017A1F
		public string DebugPath
		{
			get
			{
				return this._debugPath ?? string.Empty;
			}
			set
			{
				this._debugPath = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00019828 File Offset: 0x00017A28
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x00019839 File Offset: 0x00017A39
		public string Path
		{
			get
			{
				return this._path ?? string.Empty;
			}
			set
			{
				this._path = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00019842 File Offset: 0x00017A42
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x0001984A File Offset: 0x00017A4A
		public Assembly ResourceAssembly
		{
			get
			{
				return this._resourceAssembly;
			}
			set
			{
				this._resourceAssembly = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00019853 File Offset: 0x00017A53
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x00019864 File Offset: 0x00017A64
		public string ResourceName
		{
			get
			{
				return this._resourceName ?? string.Empty;
			}
			set
			{
				this._resourceName = value;
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00019870 File Offset: 0x00017A70
		private string GetSecureCdnPath(string unsecurePath)
		{
			string result = string.Empty;
			if (!string.IsNullOrEmpty(unsecurePath))
			{
				if (this._cdnSupportsSecureConnection)
				{
					if (unsecurePath.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
					{
						result = "https" + unsecurePath.Substring(4);
					}
					else
					{
						result = string.Empty;
					}
				}
				else
				{
					result = string.Empty;
				}
			}
			return result;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x000198C4 File Offset: 0x00017AC4
		string IScriptResourceDefinition.CdnPathSecureConnection
		{
			get
			{
				return this.CdnPathSecureConnection;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x000198CC File Offset: 0x00017ACC
		string IScriptResourceDefinition.CdnDebugPathSecureConnection
		{
			get
			{
				return this.CdnDebugPathSecureConnection;
			}
		}

		// Token: 0x040001E8 RID: 488
		private string _path;

		// Token: 0x040001E9 RID: 489
		private string _debugPath;

		// Token: 0x040001EA RID: 490
		private string _resourceName;

		// Token: 0x040001EB RID: 491
		private Assembly _resourceAssembly;

		// Token: 0x040001EC RID: 492
		private string _cdnPath;

		// Token: 0x040001ED RID: 493
		private string _cdnDebugPath;

		// Token: 0x040001EE RID: 494
		private string _cdnPathSecureConnection;

		// Token: 0x040001EF RID: 495
		private string _cdnDebugPathSecureConnection;

		// Token: 0x040001F0 RID: 496
		private bool _cdnSupportsSecureConnection;

		// Token: 0x040001F1 RID: 497
		private string _loadSuccessExpression;
	}
}
