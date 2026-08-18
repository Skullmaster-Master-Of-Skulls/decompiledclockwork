using System;

// Token: 0x0200000D RID: 13
[AttributeUsage(AttributeTargets.Module, AllowMultiple = false)]
internal sealed class BidIdentityAttribute : Attribute
{
	// Token: 0x0600009B RID: 155 RVA: 0x00056474 File Offset: 0x00055874
	internal BidIdentityAttribute(string idStr)
	{
		this._identity = idStr;
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x0600009C RID: 156 RVA: 0x00056494 File Offset: 0x00055894
	internal string IdentityString
	{
		get
		{
			return this._identity;
		}
	}

	// Token: 0x040000FC RID: 252
	private string _identity;
}
