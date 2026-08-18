using System;
using System.Diagnostics;

// Token: 0x02000031 RID: 49
[AttributeUsage(AttributeTargets.Method)]
[Conditional("CODE_ANALYSIS")]
internal sealed class BidMethodAttribute : Attribute
{
	// Token: 0x06000129 RID: 297 RVA: 0x000385B8 File Offset: 0x000379B8
	internal BidMethodAttribute()
	{
		this.m_enabled = true;
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600012A RID: 298 RVA: 0x000385D4 File Offset: 0x000379D4
	// (set) Token: 0x0600012B RID: 299 RVA: 0x000385E8 File Offset: 0x000379E8
	public bool Enabled
	{
		get
		{
			return this.m_enabled;
		}
		set
		{
			this.m_enabled = value;
		}
	}

	// Token: 0x040000C1 RID: 193
	private bool m_enabled;
}
