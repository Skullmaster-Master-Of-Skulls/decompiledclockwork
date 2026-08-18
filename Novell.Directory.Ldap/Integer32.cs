using System;

// Token: 0x02000050 RID: 80
public class Integer32
{
	// Token: 0x06000311 RID: 785 RVA: 0x000105EC File Offset: 0x0000F5EC
	public Integer32(int ival)
	{
		this._wintv = ival;
	}

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x06000312 RID: 786 RVA: 0x00010608 File Offset: 0x0000F608
	// (set) Token: 0x06000313 RID: 787 RVA: 0x00010620 File Offset: 0x0000F620
	public int intValue
	{
		get
		{
			return this._wintv;
		}
		set
		{
			this._wintv = value;
		}
	}

	// Token: 0x0400017E RID: 382
	private int _wintv;
}
