using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;

// Token: 0x0200044B RID: 1099
[CLSCompliant(false)]
[sprᵴ(MsoRecords.msofbtRegroupItems)]
internal class spr\u262B : spr\u1D3B
{
	// Token: 0x06004227 RID: 16935 RVA: 0x00251458 File Offset: 0x00250458
	public spr\u262B(spr\u1D3B A_0) : base(A_0)
	{
	}

	// Token: 0x06004228 RID: 16936 RVA: 0x0025146C File Offset: 0x0025046C
	public spr\u262B(spr\u1D3B A_0, byte[] A_1, int A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06004229 RID: 16937 RVA: 0x00251484 File Offset: 0x00250484
	public override void ᜀ(Stream A_0, int A_1, List<int> A_2, List<List<BiffRecordRaw>> A_3)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_74:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			num = 3;
			break;
		}
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				A_0.Write(this.ᜀ, 0, this.m_iLength);
				num = 2;
				continue;
			case 1:
				if (this.m_iLength > 0)
				{
					num = 0;
					continue;
				}
				return;
			case 2:
				return;
			}
			break;
		}
		this.m_iLength = ((this.ᜀ != null) ? this.ᜀ.Length : 0);
		goto IL_74;
	}

	// Token: 0x0600422A RID: 16938 RVA: 0x00251534 File Offset: 0x00250534
	public override void ᜀ(Stream A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					this.ᜀ = new byte[this.m_iLength];
					A_0.Read(this.ᜀ, 0, this.m_iLength);
					num = 1;
					continue;
				}
				break;
			case 1:
				return;
			}
			if (true)
			{
			}
			if (this.m_iLength <= 0)
			{
				break;
			}
			num = 0;
		}
	}

	// Token: 0x04001D47 RID: 7495
	private new byte[] ᜀ;
}
