using System;
using System.Diagnostics;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002C3 RID: 707
internal abstract class spr\u22C6 : BiffRecordRaw, spr\u23A5
{
	// Token: 0x06002AF6 RID: 10998 RVA: 0x0017ED10 File Offset: 0x0017DD10
	public spr\u22C6()
	{
	}

	// Token: 0x06002AF7 RID: 10999 RVA: 0x0017ED24 File Offset: 0x0017DD24
	[DebuggerStepThrough]
	public int \u1714()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜀ;
	}

	// Token: 0x06002AF8 RID: 11000 RVA: 0x0017ED68 File Offset: 0x0017DD68
	[DebuggerStepThrough]
	public void ᜇ(int A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜀ = A_0;
	}

	// Token: 0x06002AF9 RID: 11001 RVA: 0x0017EDAC File Offset: 0x0017DDAC
	[DebuggerStepThrough]
	public int \u1713()
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
		return this.ᜁ;
	}

	// Token: 0x06002AFA RID: 11002 RVA: 0x0017EDF0 File Offset: 0x0017DDF0
	[DebuggerStepThrough]
	public void ᜆ(int A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06002AFB RID: 11003 RVA: 0x0017EE34 File Offset: 0x0017DE34
	public ushort \u1712()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜂ;
	}

	// Token: 0x06002AFC RID: 11004 RVA: 0x0017EE78 File Offset: 0x0017DE78
	public void ᜁ(ushort A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜂ = A_0;
	}

	// Token: 0x06002AFD RID: 11005 RVA: 0x0017EEBC File Offset: 0x0017DEBC
	public virtual void ᜈ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					switch (A_2)
					{
					case ExcelVersion.Version97to2003:
						A_0.WriteUInt16(A_1, (ushort)this.ᜀ);
						A_1 += 2;
						A_0.WriteInt16(A_1, (short)this.ᜁ);
						A_1 += 2;
						num = 3;
						continue;
					case ExcelVersion.Version2007:
					case ExcelVersion.Version2010:
						A_0.WriteInt32(A_1, this.ᜀ);
						A_1 += 4;
						A_0.WriteInt32(A_1, this.ᜁ);
						A_1 += 4;
						num = 2;
						continue;
					default:
						num = 4;
						continue;
					}
					break;
				case 1:
					goto IL_C0;
				case 2:
					goto IL_80;
				case 3:
					goto IL_B3;
				case 4:
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_80:
		IL_B3:
		IL_C0:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_80;
		default:
			if (false)
			{
			}
			A_0.WriteUInt16(A_1, this.ᜂ);
			A_1 += 2;
			this.ᜁ(A_0, A_1, A_2);
			this.m_iLength = this.GetStoreSize(A_2);
			return;
		}
	}

	// Token: 0x06002AFE RID: 11006 RVA: 0x0017EFD0 File Offset: 0x0017DFD0
	public virtual void ᜁ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_79;
				case 1:
					switch (A_3)
					{
					case ExcelVersion.Version97to2003:
						this.ᜀ = (int)A_0.ReadUInt16(A_1);
						A_1 += 2;
						this.ᜁ = (int)A_0.ReadInt16(A_1);
						A_1 += 2;
						if (true)
						{
						}
						num = 2;
						continue;
					case ExcelVersion.Version2007:
					case ExcelVersion.Version2010:
						this.ᜀ = A_0.ReadInt32(A_1);
						A_1 += 4;
						this.ᜁ = A_0.ReadInt32(A_1);
						A_1 += 4;
						num = 0;
						continue;
					default:
						num = 3;
						continue;
					}
					break;
				case 2:
					goto IL_B2;
				case 3:
					num = 4;
					continue;
				case 4:
					goto IL_BF;
				}
				break;
			}
		}
		IL_79:
		IL_B2:
		IL_BF:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_79;
		default:
			if (false)
			{
			}
			this.ᜂ = A_0.ReadUInt16(A_1);
			A_1 += 2;
			this.ᜀ(A_0, A_1, A_3);
			return;
		}
	}

	// Token: 0x06002AFF RID: 11007
	protected abstract void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2);

	// Token: 0x06002B00 RID: 11008
	protected abstract void ᜁ(DataProvider A_0, int A_1, ExcelVersion A_2);

	// Token: 0x06002B01 RID: 11009 RVA: 0x0017F0D8 File Offset: 0x0017E0D8
	public virtual int ᜀ(ExcelVersion A_0)
	{
		int num;
		for (;;)
		{
			IL_14:
			if (true)
			{
			}
			num = base.GetStoreSize(A_0);
			for (;;)
			{
				IL_24:
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (A_0 != ExcelVersion.Version97to2003)
						{
							num2 = 2;
							continue;
						}
						return num;
					case 1:
						return num;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24;
						default:
							if (false)
							{
							}
							num += 4;
							num2 = 1;
							continue;
						}
						break;
					}
					goto IL_14;
				}
			}
		}
		return num;
	}

	// Token: 0x0400142E RID: 5166
	protected new int ᜀ;

	// Token: 0x0400142F RID: 5167
	protected int ᜁ;

	// Token: 0x04001430 RID: 5168
	[CLSCompliant(false)]
	protected ushort ᜂ;
}
