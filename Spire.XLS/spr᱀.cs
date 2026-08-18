using System;
using System.Runtime.InteropServices;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002CD RID: 717
internal class spr᱀ : IDisposable
{
	// Token: 0x06002C1C RID: 11292 RVA: 0x00189544 File Offset: 0x00188544
	public spr᱀()
	{
	}

	// Token: 0x06002C1D RID: 11293 RVA: 0x00189564 File Offset: 0x00188564
	public spr᱀(int A_0, bool A_1)
	{
		this.ᜀ(A_0, A_1);
	}

	// Token: 0x06002C1E RID: 11294 RVA: 0x0018958C File Offset: 0x0018858C
	protected virtual void ᜂ()
	{
		try
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
			this.ᜀ();
		}
		finally
		{
			base.Finalize();
		}
	}

	// Token: 0x06002C1F RID: 11295 RVA: 0x001895E8 File Offset: 0x001885E8
	public void ᜀ()
	{
		int num = 0;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 1:
				return;
			case 2:
				Marshal.FreeHGlobal(this.ᜀ);
				this.ᜀ = IntPtr.Zero;
				this.ᜁ = 0;
				GC.SuppressFinalize(this);
				if (true)
				{
				}
				num = 1;
				continue;
			}
			while (this.ᜀ != IntPtr.Zero)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 2;
					goto IL_0A;
				}
			}
			break;
		}
	}

	// Token: 0x06002C20 RID: 11296 RVA: 0x00189688 File Offset: 0x00188688
	public int ᜁ(int A_0)
	{
		int a_ = 14;
		int num = 4 * A_0;
		if (num > this.ᜁ)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_38;
				}
			}
			IL_38:
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⵃ⡅ⱇ⽉㑋", a_));
		}
		return Marshal.ReadInt32(this.ᜀ, num);
	}

	// Token: 0x06002C21 RID: 11297 RVA: 0x001896FC File Offset: 0x001886FC
	public int ᜀ(int A_0)
	{
		int a_ = 3;
		int num = 2 * A_0;
		if (num > this.ᜁ)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_38;
				}
			}
			IL_38:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("倸唺夼娾㥀", a_));
		}
		return (int)Marshal.ReadInt16(this.ᜀ, num);
	}

	// Token: 0x06002C22 RID: 11298 RVA: 0x00189770 File Offset: 0x00188770
	public byte ᜂ(int A_0)
	{
		int a_ = 13;
		if (A_0 > this.ᜁ)
		{
			for (;;)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_3C;
				}
			}
			IL_3C:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⩂⭄⍆ⱈ㍊", a_));
		}
		return Marshal.ReadByte(this.ᜀ, A_0);
	}

	// Token: 0x06002C23 RID: 11299 RVA: 0x001897E0 File Offset: 0x001887E0
	public void ᜀ(int A_0, int A_1)
	{
		int a_ = 0;
		int num = 4 * A_0;
		if (num + 4 > this.ᜁ)
		{
			for (;;)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_42;
				}
			}
			IL_42:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("張嘷帹夻䘽", a_));
		}
		Marshal.WriteInt32(this.ᜀ, num, A_1);
	}

	// Token: 0x06002C24 RID: 11300 RVA: 0x00189858 File Offset: 0x00188858
	public void ᜀ(int A_0, short A_1)
	{
		int a_ = 17;
		int num = 2 * A_0;
		if (num + 2 > this.ᜁ)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_3A;
				}
			}
			IL_3A:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⹆❈⽊⡌㝎", a_));
		}
		Marshal.WriteInt16(this.ᜀ, num, A_1);
	}

	// Token: 0x06002C25 RID: 11301 RVA: 0x001898D0 File Offset: 0x001888D0
	public void ᜀ(int A_0, byte A_1)
	{
		int a_ = 0;
		if (A_0 > this.ᜁ)
		{
			for (;;)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_3C;
				}
			}
			IL_3C:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("張嘷帹夻䘽", a_));
		}
		Marshal.WriteByte(this.ᜀ, A_0, A_1);
	}

	// Token: 0x06002C26 RID: 11302 RVA: 0x00189940 File Offset: 0x00188940
	public void ᜀ(int A_0, bool A_1)
	{
		int a_ = 9;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜀ = ((this.ᜀ == IntPtr.Zero) ? (this.ᜀ = Marshal.AllocHGlobal(A_0)) : Marshal.ReAllocHGlobal(this.ᜀ, (IntPtr)A_0));
				if (true)
				{
				}
				num = 3;
				continue;
			case 1:
				goto IL_47;
			case 2:
				goto IL_EC;
			case 3:
				if (A_1)
				{
					num = 4;
					continue;
				}
				goto IL_116;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5D;
				default:
				{
					if (false)
					{
					}
					IntPtr ptrDest = (IntPtr)(this.ᜀ.ToInt64() + (long)this.ᜁ);
					Memory.RtlZeroMemory(ptrDest, A_0 - this.ᜁ);
					num = 2;
					continue;
				}
				}
				break;
			}
			if (A_0 <= 0)
			{
				num = 1;
				continue;
			}
			IL_5D:
			num = 0;
		}
		IL_47:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嘾Հ♂㙄⹆㭈⹊⥌ᱎ㡐⥒ご", a_));
		IL_EC:
		IL_116:
		this.ᜁ = A_0;
	}

	// Token: 0x06002C27 RID: 11303 RVA: 0x00189A6C File Offset: 0x00188A6C
	public void ᜀ(spr᱀ A_0)
	{
		int a_ = 11;
		if (true)
		{
		}
		if (A_0 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				Memory.CopyMemory(this.ᜀ, A_0.ᜀ, this.ᜁ);
				return;
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㉀ⱂい㕆⩈⹊", a_));
	}

	// Token: 0x06002C28 RID: 11304 RVA: 0x00189AE0 File Offset: 0x00188AE0
	public void ᜁ()
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
		Memory.RtlZeroMemory(this.ᜀ, this.ᜁ);
	}

	// Token: 0x04001464 RID: 5220
	private IntPtr ᜀ = IntPtr.Zero;

	// Token: 0x04001465 RID: 5221
	private int ᜁ;
}
