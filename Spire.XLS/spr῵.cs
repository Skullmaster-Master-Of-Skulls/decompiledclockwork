using System;
using System.IO;
using System.Runtime.InteropServices;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003AC RID: 940
internal class spr\u1FF5 : spr\u1FDC
{
	// Token: 0x060038EC RID: 14572 RVA: 0x001FBC7C File Offset: 0x001FAC7C
	public spr\u1FF5(sprᮯ A_0, string A_1)
	{
		int a_ = 2;
		base..ctor(A_1);
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("䬷丹主嬽ℿ⽁", a_));
		}
		this.ᜀ = A_0;
	}

	// Token: 0x060038ED RID: 14573 RVA: 0x001FBCBC File Offset: 0x001FACBC
	public virtual int ᜁ(byte[] A_0, int A_1, int A_2)
	{
		int num2;
		for (;;)
		{
			IL_40:
			this.ᜀ(A_0, A_1, A_2);
			if (true)
			{
			}
			int num = 5;
			for (;;)
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
					byte[] array;
					byte[] array2;
					switch (num)
					{
					case 0:
						Buffer.BlockCopy(array, 0, A_0, A_1, num2);
						num = 1;
						continue;
					case 1:
						return num2;
					case 2:
						if (A_1 != 0)
						{
							num = 0;
							continue;
						}
						return num2;
					case 3:
						goto IL_66;
					case 4:
						array2 = A_0;
						goto IL_88;
					case 5:
						if (A_1 == 0)
						{
							num = 3;
							continue;
						}
						num = 6;
						continue;
					case 6:
						array2 = new byte[A_2];
						goto IL_88;
					}
					goto IL_40;
					IL_88:
					array = array2;
					uint num3 = 0U;
					this.ᜀ.ᜀ(array, (uint)A_2, ref num3);
					num2 = (int)num3;
					this.ᜁ += (long)((ulong)num3);
					num = 2;
					continue;
				}
				}
				IL_66:
				num = 4;
			}
		}
		return num2;
	}

	// Token: 0x060038EE RID: 14574 RVA: 0x001FBDB0 File Offset: 0x001FADB0
	public virtual void ᜂ(byte[] A_0, int A_1, int A_2)
	{
		byte[] array;
		for (;;)
		{
			this.ᜀ(A_0, A_1, A_2);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (A_1 == 0)
					{
						num = 2;
						continue;
					}
					goto IL_3E;
				case 1:
					goto IL_8C;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3E;
					default:
						if (false)
						{
						}
						array = A_0;
						num = 1;
						continue;
					}
					break;
				case 3:
					goto IL_57;
				}
				break;
				IL_3E:
				array = new byte[A_2];
				Buffer.BlockCopy(A_0, A_1, array, 0, A_2);
				num = 3;
			}
		}
		IL_57:
		IL_8C:
		uint num2 = 0U;
		this.ᜀ.ᜁ(array, (uint)A_2, ref num2);
		this.ᜁ += (long)((ulong)num2);
	}

	// Token: 0x060038EF RID: 14575 RVA: 0x001FBE6C File Offset: 0x001FAE6C
	public virtual long ᜀ(long A_0, SeekOrigin A_1)
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
		long result;
		this.ᜀ.ᜀ(A_0, A_1, out result);
		this.ᜁ = result;
		return result;
	}

	// Token: 0x060038F0 RID: 14576 RVA: 0x001FBEC0 File Offset: 0x001FAEC0
	public virtual void ᜁ(long A_0)
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
		this.ᜀ.ᜀ((ulong)A_0);
	}

	// Token: 0x060038F1 RID: 14577 RVA: 0x001FBF08 File Offset: 0x001FAF08
	public virtual long ᜅ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_3F:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_69;
			case 2:
				if (true)
				{
				}
				spr\u1FF5.ᜂ = true;
				num = 1;
				continue;
			}
			break;
		}
		if (!spr\u1FF5.ᜂ)
		{
			goto IL_3F;
		}
		IL_69:
		long result;
		this.ᜀ.ᜀ(0L, SeekOrigin.End, out result);
		this.ᜀ.ᜀ(this.ᜁ, SeekOrigin.Begin, out this.ᜁ);
		return result;
	}

	// Token: 0x060038F2 RID: 14578 RVA: 0x001FBFAC File Offset: 0x001FAFAC
	public virtual long ᜃ()
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
		return this.ᜁ;
	}

	// Token: 0x060038F3 RID: 14579 RVA: 0x001FBFF0 File Offset: 0x001FAFF0
	public override void ᜀ(long A_0)
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
		this.ᜁ = this.Seek(A_0, SeekOrigin.Begin);
	}

	// Token: 0x060038F4 RID: 14580 RVA: 0x001FC03C File Offset: 0x001FB03C
	public virtual bool ᜀ()
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
		return true;
	}

	// Token: 0x060038F5 RID: 14581 RVA: 0x001FC078 File Offset: 0x001FB078
	public virtual bool ᜂ()
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
		return true;
	}

	// Token: 0x060038F6 RID: 14582 RVA: 0x001FC0B4 File Offset: 0x001FB0B4
	public virtual bool ᜁ()
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

	// Token: 0x060038F7 RID: 14583 RVA: 0x001FC0F0 File Offset: 0x001FB0F0
	public virtual void ᜄ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜀ.ᜀ(0U);
	}

	// Token: 0x060038F8 RID: 14584 RVA: 0x001FC138 File Offset: 0x001FB138
	protected override void ᜀ(bool A_0)
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
		base.Dispose(A_0);
		this.ᜀ.ᜀ(0U);
		Marshal.ReleaseComObject(this.ᜀ);
		this.ᜀ = null;
		this.ᜁ = -1L;
	}

	// Token: 0x060038F9 RID: 14585 RVA: 0x001FC1A4 File Offset: 0x001FB1A4
	private void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 0;
		for (;;)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_2 < 0)
					{
						num = 2;
						continue;
					}
					return;
				case 1:
					if (A_1 + A_2 > A_0.Length)
					{
						num = 5;
						continue;
					}
					num = 3;
					continue;
				case 2:
					goto IL_FD;
				case 3:
					if (A_1 < 0)
					{
						num = 6;
						continue;
					}
					num = 0;
					continue;
				case 5:
					goto IL_9D;
				case 6:
					goto IL_80;
				case 7:
					goto IL_44;
				}
				if (A_0 == null)
				{
					num = 7;
				}
				else
				{
					num = 1;
				}
			}
			IL_44:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_C9;
			}
		}
		IL_80:
		if (true)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("夵帷尹伻嬽㐿", a_));
		IL_9D:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("眵䨷䠹崻䜽怿ㅁⵃ㱅ⵇ晉汋⅍㙏㑑❓㍕ⱗ穙㵛そџ䉡ࡣͥ٧൩ᡫ٭偯ᙱ᭳፵୷ᑹ孻੽ꁿ겋ﲓ뚕ﮝ튟", a_));
		IL_C9:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("吵䴷尹娻嬽㈿", a_));
		IL_FD:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("娵崷吹嬻䨽⠿", a_));
	}

	// Token: 0x060038FA RID: 14586 RVA: 0x001FC2C4 File Offset: 0x001FB2C4
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u1FF5()
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
		spr\u1FF5.ᜃ = 0;
	}

	// Token: 0x0400190B RID: 6411
	private new sprᮯ ᜀ;

	// Token: 0x0400190C RID: 6412
	private long ᜁ;

	// Token: 0x0400190D RID: 6413
	private static bool ᜂ;

	// Token: 0x0400190E RID: 6414
	private static int ᜃ;
}
