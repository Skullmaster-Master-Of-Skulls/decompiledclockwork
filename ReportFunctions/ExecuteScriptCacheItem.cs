using System;
using RemoteLoader;
using ReportFunctions;

// Token: 0x0200001D RID: 29
public class ExecuteScriptCacheItem : IDisposable
{
	// Token: 0x06000245 RID: 581 RVA: 0x00038A28 File Offset: 0x00037A28
	~ExecuteScriptCacheItem()
	{
		this.Dispose(false);
	}

	// Token: 0x06000246 RID: 582 RVA: 0x00038A5C File Offset: 0x00037A5C
	public void Dispose()
	{
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}

	// Token: 0x06000247 RID: 583 RVA: 0x00038A70 File Offset: 0x00037A70
	protected virtual void Dispose(bool disposeManagedResources)
	{
		if (!this.disposed)
		{
			if (disposeManagedResources)
			{
				this.ShutDown();
			}
			this.disposed = true;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000249 RID: 585 RVA: 0x00038AD0 File Offset: 0x00037AD0
	// (set) Token: 0x0600024A RID: 586 RVA: 0x00038AE8 File Offset: 0x00037AE8
	public string ExecuteScript_cachedCode
	{
		get
		{
			return this.executeScript_cachedCode;
		}
		set
		{
			this.executeScript_cachedCode = value;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x0600024B RID: 587 RVA: 0x00038AF4 File Offset: 0x00037AF4
	// (set) Token: 0x0600024C RID: 588 RVA: 0x00038B0C File Offset: 0x00037B0C
	public IRemoteInterface ExecuteScript_cachedRemoteInterface
	{
		get
		{
			return this.executeScript_cachedRemoteInterface;
		}
		set
		{
			this.executeScript_cachedRemoteInterface = value;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x0600024D RID: 589 RVA: 0x00038B18 File Offset: 0x00037B18
	// (set) Token: 0x0600024E RID: 590 RVA: 0x00038B30 File Offset: 0x00037B30
	public AppDomain ExecuteScript_cachedSandboxDomain
	{
		get
		{
			return this.executeScript_cachedSandboxDomain;
		}
		set
		{
			this.executeScript_cachedSandboxDomain = value;
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x0600024F RID: 591 RVA: 0x00038B3C File Offset: 0x00037B3C
	// (set) Token: 0x06000250 RID: 592 RVA: 0x00038B54 File Offset: 0x00037B54
	public string ExecuteScript_cachedTempFile
	{
		get
		{
			return this.executeScript_cachedTempFile;
		}
		set
		{
			this.executeScript_cachedTempFile = value;
		}
	}

	// Token: 0x06000251 RID: 593 RVA: 0x00038B60 File Offset: 0x00037B60
	public void ShutDown()
	{
		if (this.executeScript_cachedSandboxDomain != null)
		{
			ReportFunction.UnloadSandboxDomain(ref this.executeScript_cachedSandboxDomain, ref this.executeScript_cachedRemoteInterface, this.executeScript_cachedTempFile);
			this.executeScript_cachedSandboxDomain = null;
			this.executeScript_cachedRemoteInterface = null;
		}
		this.executeScript_cachedCode = null;
	}

	// Token: 0x04000111 RID: 273
	private string executeScript_cachedCode = null;

	// Token: 0x04000112 RID: 274
	private IRemoteInterface executeScript_cachedRemoteInterface = null;

	// Token: 0x04000113 RID: 275
	private AppDomain executeScript_cachedSandboxDomain = null;

	// Token: 0x04000114 RID: 276
	private string executeScript_cachedTempFile = null;

	// Token: 0x04000115 RID: 277
	private bool disposed = false;
}
