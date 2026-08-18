using System;

// Token: 0x02000111 RID: 273
[AttributeUsage(AttributeTargets.Module, AllowMultiple = false)]
internal sealed class BidIdentityAttribute : Attribute
{
	// Token: 0x0600114C RID: 4428 RVA: 0x00233078 File Offset: 0x00232478
	internal BidIdentityAttribute(string idStr)
	{
		this._identity = idStr;
	}

	// Token: 0x17000242 RID: 578
	// (get) Token: 0x0600114D RID: 4429 RVA: 0x00233098 File Offset: 0x00232498
	internal string IdentityString
	{
		get
		{
			return this._identity;
		}
	}

	// Token: 0x04000B68 RID: 2920
	private string _identity;
}
