using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;

// Token: 0x020004E1 RID: 1249
[sprᵴ(MsoRecords.msoUnknown)]
[CLSCompliant(false)]
internal class sprᢦ : spr\u1D3B
{
	// Token: 0x06004CAA RID: 19626 RVA: 0x002ED390 File Offset: 0x002EC390
	public sprᢦ(spr\u1D3B A_0) : base(A_0)
	{
	}

	// Token: 0x06004CAB RID: 19627 RVA: 0x002ED3A4 File Offset: 0x002EC3A4
	public sprᢦ(spr\u1D3B A_0, byte[] A_1, int A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06004CAC RID: 19628 RVA: 0x002ED3BC File Offset: 0x002EC3BC
	public override void ᜀ(Stream A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					this.ᜀ = new byte[this.m_iLength];
					A_0.Read(this.ᜀ, 0, this.m_iLength);
					num = 0;
					continue;
				}
				if (this.m_iLength <= 0)
				{
					break;
				}
				num = 2;
			}
			break;
		}
		}
	}

	// Token: 0x06004CAD RID: 19629 RVA: 0x002ED458 File Offset: 0x002EC458
	public override void ᜀ(Stream A_0, int A_1, List<int> A_2, List<List<BiffRecordRaw>> A_3)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					A_0.Write(this.ᜀ, 0, this.m_iLength);
					num = 0;
					continue;
				case 2:
					if (true)
					{
					}
					break;
				}
				if (this.m_iLength <= 0)
				{
					break;
				}
				num = 1;
			}
			break;
		}
		}
	}

	// Token: 0x06004CAE RID: 19630 RVA: 0x002ED4E0 File Offset: 0x002EC4E0
	public virtual bool ᜀ()
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return true;
	}
}
