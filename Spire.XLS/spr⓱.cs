using System;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;

// Token: 0x0200043E RID: 1086
internal class spr\u24F1 : XlsRange, IMigrantRange
{
	// Token: 0x0600414D RID: 16717 RVA: 0x00249408 File Offset: 0x00248408
	internal spr\u24F1(spr\u1DF5 A_0, IWorksheet A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x0600414E RID: 16718 RVA: 0x00249420 File Offset: 0x00248420
	public new void ᜀ(int A_0, int A_1)
	{
		for (;;)
		{
			this.m_dataValidation = null;
			this.m_rtfString = null;
			this.m_iBottomRow = A_0;
			this.m_iTopRow = A_0;
			this.m_iRightColumn = A_1;
			this.m_iLeftColumn = A_1;
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.m_style != null)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					this.m_style.SetFormatIndex((int)base.ExtendedFormatIndex);
					num = 2;
					continue;
				case 2:
					goto IL_7D;
				}
				break;
			}
		}
		IL_7D:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			break;
		}
	}
}
