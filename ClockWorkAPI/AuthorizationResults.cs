using System;

// Token: 0x0200009F RID: 159
public class AuthorizationResults
{
	// Token: 0x06000800 RID: 2048 RVA: 0x00030316 File Offset: 0x0002F316
	public AuthorizationResults(bool isAuthorized)
	{
		this.isAuthorized = isAuthorized;
		this.errMsg = "";
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x00030333 File Offset: 0x0002F333
	public AuthorizationResults(string errMsg)
	{
		this.isAuthorized = false;
		this.errMsg = errMsg;
	}

	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x06000802 RID: 2050 RVA: 0x0003034C File Offset: 0x0002F34C
	public bool IsAuthorized
	{
		get
		{
			return this.isAuthorized;
		}
	}

	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x06000803 RID: 2051 RVA: 0x00030364 File Offset: 0x0002F364
	public string ErrMsg
	{
		get
		{
			return this.errMsg;
		}
	}

	// Token: 0x04000403 RID: 1027
	private bool isAuthorized;

	// Token: 0x04000404 RID: 1028
	private string errMsg;
}
