using System;
using System.Runtime.InteropServices;
using Spire.CompoundFile.Doc.Native;

// Token: 0x0200044E RID: 1102
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
[Guid("0000010e-0000-0000-C000-000000000046")]
[CLSCompliant(false)]
internal class spr᥉ : spr\u22EC
{
	// Token: 0x06003D10 RID: 15632 RVA: 0x0038D834 File Offset: 0x0038C834
	public uint ᜁ(ref sprῙ A_0, ref spr\u1CFC A_1)
	{
		while (this.ᜀ.Count <= 0)
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
				return 2147745892U;
			}
		}
		if (true)
		{
		}
		spr\u1CE6 spr_u1CE = (spr\u1CE6)this.ᜀ[0];
		A_0 = spr_u1CE.ᜂ();
		A_1 = spr_u1CE.ᜀ();
		return 0U;
	}

	// Token: 0x06003D11 RID: 15633 RVA: 0x0038D8B0 File Offset: 0x0038C8B0
	public uint ᜀ(ref sprῙ A_0, ref spr\u1CFC A_1)
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
		return 2147483649U;
	}

	// Token: 0x06003D12 RID: 15634 RVA: 0x0038D8F0 File Offset: 0x0038C8F0
	public uint ᜀ(ref sprῙ A_0)
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
		return 2147483649U;
	}

	// Token: 0x06003D13 RID: 15635 RVA: 0x0038D930 File Offset: 0x0038C930
	public uint ᜀ(ref sprῙ A_0, ref sprῙ A_1)
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
		return 2147483649U;
	}

	// Token: 0x06003D14 RID: 15636 RVA: 0x0038D970 File Offset: 0x0038C970
	public uint ᜀ(ref sprῙ A_0, ref spr\u1CFC A_1, int A_2)
	{
		sprῙ a_;
		spr\u1CFC a_2;
		for (;;)
		{
			a_ = A_0;
			a_2 = A_1;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_E3;
				case 1:
					Marshal.AddRef(a_2.ᜁ);
					num = 0;
					continue;
				case 2:
					if (A_2 <= 0)
					{
						goto IL_169;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E3;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 3:
					if (IntPtr.Zero != A_1.ᜂ)
					{
						num = 6;
						continue;
					}
					goto IL_169;
				case 4:
					goto IL_6A;
				case 5:
					if (IntPtr.Zero != A_1.ᜁ)
					{
						num = 10;
						continue;
					}
					goto IL_6A;
				case 6:
					Marshal.Release(A_1.ᜂ);
					num = 9;
					continue;
				case 7:
					if (IntPtr.Zero != a_2.ᜁ)
					{
						num = 1;
						continue;
					}
					goto IL_FE;
				case 8:
					num = 5;
					continue;
				case 9:
					goto IL_FC;
				case 10:
					Marshal.Release(A_1.ᜁ);
					num = 4;
					continue;
				}
				break;
				IL_6A:
				num = 3;
				continue;
				IL_FE:
				a_2.ᜂ = IntPtr.Zero;
				if (true)
				{
				}
				num = 2;
				continue;
				IL_E3:
				goto IL_FE;
			}
		}
		IL_FC:
		IL_169:
		this.ᜀ.Add(new spr\u1CE6(DATADIR.DATADIR_SET, a_2, a_));
		return 0U;
	}

	// Token: 0x06003D15 RID: 15637 RVA: 0x0038DAFC File Offset: 0x0038CAFC
	public uint ᜀ(uint A_0, ref spr\u2318 A_1)
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
		A_1 = this.ᜀ;
		return 0U;
	}

	// Token: 0x06003D16 RID: 15638 RVA: 0x0038DB40 File Offset: 0x0038CB40
	public uint ᜀ(ref sprῙ A_0, uint A_1, IntPtr A_2, ref uint A_3)
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
		return 2147745795U;
	}

	// Token: 0x06003D17 RID: 15639 RVA: 0x0038DB80 File Offset: 0x0038CB80
	public uint ᜀ(uint A_0)
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
		return 2147745795U;
	}

	// Token: 0x06003D18 RID: 15640 RVA: 0x0038DBC0 File Offset: 0x0038CBC0
	public uint ᜀ(ref IntPtr A_0)
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
		return 2147745795U;
	}

	// Token: 0x04002C20 RID: 11296
	private spr\u2556 ᜀ = new spr\u2556();
}
