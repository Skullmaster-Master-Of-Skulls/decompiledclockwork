using System;

// Token: 0x02000228 RID: 552
internal struct spr\u23DB
{
	// Token: 0x06001A25 RID: 6693 RVA: 0x001AB3A4 File Offset: 0x001AA3A4
	internal spr\u23DB(bool A_0, int A_1, int A_2)
	{
		this.ᜀ = A_0;
		this.ᜂ = A_2;
		this.ᜁ = A_1;
		if (this.ᜁ > this.ᜂ)
		{
			this.ᜃ = spr\u19CB.PrepareTableState.EnterTable;
			return;
		}
		if (this.ᜁ < this.ᜂ)
		{
			this.ᜃ = spr\u19CB.PrepareTableState.LeaveTable;
			return;
		}
		this.ᜃ = spr\u19CB.PrepareTableState.NoChange;
	}

	// Token: 0x06001A26 RID: 6694 RVA: 0x001AB400 File Offset: 0x001AA400
	internal spr\u23DB(bool A_0, int A_1, int A_2, bool A_3)
	{
		this.ᜀ = A_0;
		this.ᜂ = A_2;
		this.ᜁ = A_1;
		if (this.ᜁ > this.ᜂ)
		{
			this.ᜃ = spr\u19CB.PrepareTableState.EnterTable;
			return;
		}
		if (this.ᜁ < this.ᜂ)
		{
			this.ᜃ = spr\u19CB.PrepareTableState.LeaveTable;
			return;
		}
		if (A_0 && A_3 && this.ᜁ == this.ᜂ)
		{
			this.ᜃ = spr\u19CB.PrepareTableState.LeaveTable;
			this.ᜁ = 0;
			return;
		}
		this.ᜃ = spr\u19CB.PrepareTableState.NoChange;
	}

	// Token: 0x04001DF1 RID: 7665
	internal bool ᜀ;

	// Token: 0x04001DF2 RID: 7666
	internal int ᜁ;

	// Token: 0x04001DF3 RID: 7667
	internal int ᜂ;

	// Token: 0x04001DF4 RID: 7668
	internal spr\u19CB.PrepareTableState ᜃ;
}
