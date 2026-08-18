using System;
using System.Drawing;
using Spire.Doc;

// Token: 0x02000315 RID: 789
internal class spr\u24DB
{
	// Token: 0x06002B01 RID: 11009 RVA: 0x002A4D50 File Offset: 0x002A3D50
	internal Color ᜃ()
	{
		while (this.ᜂ != 4278190080U)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				return sprṡ.ᜀ(this.ᜂ);
			}
		}
		return Color.Empty;
	}

	// Token: 0x06002B02 RID: 11010 RVA: 0x002A4DAC File Offset: 0x002A3DAC
	internal void ᜁ(Color A_0)
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
		this.ᜂ = sprṡ.ᜂ(A_0);
	}

	// Token: 0x06002B03 RID: 11011 RVA: 0x002A4DF4 File Offset: 0x002A3DF4
	internal Color ᜂ()
	{
		while (this.ᜃ != 4278190080U)
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
				if (true)
				{
				}
				return sprṡ.ᜀ(this.ᜃ);
			}
		}
		return Color.Empty;
	}

	// Token: 0x06002B04 RID: 11012 RVA: 0x002A4E50 File Offset: 0x002A3E50
	internal void ᜀ(Color A_0)
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
		this.ᜃ = sprṡ.ᜂ(A_0);
	}

	// Token: 0x06002B05 RID: 11013 RVA: 0x002A4E98 File Offset: 0x002A3E98
	internal TextureStyle ᜁ()
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
		return this.ᜄ;
	}

	// Token: 0x06002B06 RID: 11014 RVA: 0x002A4EDC File Offset: 0x002A3EDC
	internal void ᜀ(TextureStyle A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06002B07 RID: 11015 RVA: 0x002A4F20 File Offset: 0x002A3F20
	internal static int ᜀ()
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
		return 2;
	}

	// Token: 0x06002B08 RID: 11016 RVA: 0x002A4F5C File Offset: 0x002A3F5C
	internal spr\u24DB(short A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x06002B09 RID: 11017 RVA: 0x002A4F8C File Offset: 0x002A3F8C
	internal spr\u24DB()
	{
	}

	// Token: 0x06002B0A RID: 11018 RVA: 0x002A4FB8 File Offset: 0x002A3FB8
	internal void ᜀ(short A_0)
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
		this.ᜂ = sprṡ.ᜁ((int)(A_0 & 31));
		this.ᜃ = sprṡ.ᜁ((A_0 & 992) >> 5);
		this.ᜄ = (TextureStyle)(((int)A_0 & 64512) >> 10);
	}

	// Token: 0x06002B0B RID: 11019 RVA: 0x002A5028 File Offset: 0x002A4028
	internal short ᜅ()
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
		int num = (int)(sprṡ.ᜁ[0] | (uint)sprṡ.ᜁ(this.ᜂ));
		num = (int)(sprṡ.ᜁ[0] | (uint)((uint)sprṡ.ᜁ(this.ᜃ) << 5));
		num |= (int)((int)this.ᜄ << 10);
		return (short)num;
	}

	// Token: 0x06002B0C RID: 11020 RVA: 0x002A50A0 File Offset: 0x002A40A0
	internal void ᜀ(byte[] A_0, int A_1)
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
		this.ᜂ = BitConverter.ToUInt32(A_0, A_1);
		this.ᜃ = BitConverter.ToUInt32(A_0, A_1 + 4);
		this.ᜄ = (TextureStyle)BitConverter.ToUInt16(A_0, A_1 + 8);
	}

	// Token: 0x06002B0D RID: 11021 RVA: 0x002A5108 File Offset: 0x002A4108
	internal byte[] ᜄ()
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
		byte[] array = new byte[10];
		byte[] bytes = BitConverter.GetBytes(this.ᜂ);
		bytes.CopyTo(array, 0);
		bytes = BitConverter.GetBytes(this.ᜃ);
		bytes.CopyTo(array, 4);
		bytes = BitConverter.GetBytes((ushort)this.ᜄ);
		bytes.CopyTo(array, 8);
		return array;
	}

	// Token: 0x04002523 RID: 9507
	internal const int ᜀ = 2;

	// Token: 0x04002524 RID: 9508
	internal const int ᜁ = 10;

	// Token: 0x04002525 RID: 9509
	private uint ᜂ = 4278190080U;

	// Token: 0x04002526 RID: 9510
	private uint ᜃ = 4278190080U;

	// Token: 0x04002527 RID: 9511
	private TextureStyle ᜄ;
}
