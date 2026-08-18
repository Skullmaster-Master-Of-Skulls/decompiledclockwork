using System;

// Token: 0x0200002F RID: 47
[AttributeUsage(AttributeTargets.Module, AllowMultiple = false)]
internal sealed class BidIdentityAttribute : Attribute
{
	// Token: 0x06000125 RID: 293 RVA: 0x00038558 File Offset: 0x00037958
	internal BidIdentityAttribute(string idStr)
	{
		this._identity = idStr;
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000126 RID: 294 RVA: 0x00038574 File Offset: 0x00037974
	internal string IdentityString
	{
		get
		{
			return this._identity;
		}
	}

	// Token: 0x040000BF RID: 191
	private string _identity;
}
