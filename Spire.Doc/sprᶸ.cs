using System;
using System.Drawing;
using System.IO;
using Spire.Doc.Core.Escher;

// Token: 0x02000362 RID: 866
internal class spr\u1DB8 : spr\u2096
{
	// Token: 0x06002E84 RID: 11908 RVA: 0x002C2138 File Offset: 0x002C1138
	public spr\u1DB8()
	{
		this.ᜀ = new byte[16];
		this.ᜁ = new byte[16];
	}

	// Token: 0x06002E85 RID: 11909 RVA: 0x002C2168 File Offset: 0x002C1168
	public byte[] ᜂ()
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

	// Token: 0x06002E86 RID: 11910 RVA: 0x002C21AC File Offset: 0x002C11AC
	public void ᜀ(byte[] A_0)
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

	// Token: 0x06002E87 RID: 11911 RVA: 0x002C21F0 File Offset: 0x002C11F0
	public byte[] ᜁ()
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

	// Token: 0x06002E88 RID: 11912 RVA: 0x002C2234 File Offset: 0x002C1234
	public void ᜁ(byte[] A_0)
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

	// Token: 0x06002E89 RID: 11913 RVA: 0x002C2278 File Offset: 0x002C1278
	public byte ᜄ()
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

	// Token: 0x06002E8A RID: 11914 RVA: 0x002C22BC File Offset: 0x002C12BC
	public void ᜀ(byte A_0)
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

	// Token: 0x06002E8B RID: 11915 RVA: 0x002C2300 File Offset: 0x002C1300
	public MemoryStream ᜀ()
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
		return this.ᜃ;
	}

	// Token: 0x06002E8C RID: 11916 RVA: 0x002C2344 File Offset: 0x002C1344
	public void ᜀ(MemoryStream A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06002E8D RID: 11917 RVA: 0x002C2388 File Offset: 0x002C1388
	public override Image ᜀ(Stream A_0, int A_1, bool A_2)
	{
		int num;
		for (;;)
		{
			if (true)
			{
			}
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				A_0.Read(this.ᜂ(), 0, this.ᜂ().Length);
				num = 16;
				num2 = 0;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (A_2)
					{
						num2 = 1;
						continue;
					}
					goto IL_98;
				case 1:
					A_0.Read(this.ᜁ(), 0, this.ᜁ().Length);
					num += 16;
					num2 = 2;
					continue;
				case 2:
					goto IL_96;
				}
				break;
			}
		}
		IL_96:
		IL_98:
		this.ᜀ((byte)A_0.ReadByte());
		this.ᜀ(null);
		num++;
		byte[] array = new byte[A_1 - num];
		A_0.Read(array, 0, array.Length);
		this.ᜀ(new MemoryStream(array, 0, array.Length));
		return new Bitmap(this.ᜀ());
	}

	// Token: 0x06002E8E RID: 11918 RVA: 0x002C2478 File Offset: 0x002C1478
	internal override void ᜀ(Stream A_0, MemoryStream A_1, MSOBlipType A_2, byte[] A_3)
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
		this.ᜀ(A_0, A_1.Length, A_2, A_3);
		byte[] array = new byte[A_1.Length];
		A_1.Position = 0L;
		A_1.Read(array, 0, array.Length);
		A_0.Write(array, 0, array.Length);
	}

	// Token: 0x06002E8F RID: 11919 RVA: 0x002C24F0 File Offset: 0x002C14F0
	private void ᜀ(Stream A_0, long A_1, MSOBlipType A_2, byte[] A_3)
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
		spr\u224B spr_u224B = new spr\u224B();
		spr_u224B.ᜀ(MSOFBT.msofbtBSE);
		spr_u224B.ᜁ(6U);
		spr_u224B.ᜂ(2U);
		spr_u224B.ᜀ((uint)(A_1 + 61L));
		spr_u224B.ᜀ(A_0);
		spr\u1D43 spr_u1D = new spr\u1D43();
		spr_u1D.ᜀ(A_2);
		spr_u1D.ᜁ(A_2);
		A_3.CopyTo(spr_u1D.ᜀ(), 0);
		spr_u1D.ᜀ(MSOBlipUsage.msoblipUsageDefault);
		spr_u1D.ᜂ(0);
		spr_u1D.ᜂ((uint)(A_1 + 25L));
		spr_u1D.ᜀ(68U);
		spr_u1D.ᜁ(1U);
		spr_u1D.ᜀ(255);
		spr_u1D.ᜀ(0);
		spr_u1D.ᜁ(0);
		spr_u1D.ᜀ(A_0);
		spr_u224B = new spr\u224B();
		spr_u224B.ᜀ((uint)A_1 + 17U);
		spr_u224B.ᜀ(MSOFBT.msofbtBlipFirst + (int)A_2);
		spr_u224B.ᜁ(1760U);
		spr_u224B.ᜂ(0U);
		spr_u224B.ᜀ(A_0);
		A_0.Write(A_3, 0, A_3.Length);
		A_0.WriteByte(byte.MaxValue);
	}

	// Token: 0x06002E90 RID: 11920 RVA: 0x002C2618 File Offset: 0x002C1618
	internal override void \u170D()
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
		base.\u170D();
		this.ᜀ = null;
		this.ᜁ = null;
		this.ᜃ.Close();
		this.ᜃ = null;
	}

	// Token: 0x040026CA RID: 9930
	private new byte[] ᜀ;

	// Token: 0x040026CB RID: 9931
	private new byte[] ᜁ;

	// Token: 0x040026CC RID: 9932
	private new byte ᜂ;

	// Token: 0x040026CD RID: 9933
	private new MemoryStream ᜃ;
}
