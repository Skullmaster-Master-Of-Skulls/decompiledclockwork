using System;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;
using Spire.Pdf.General.Paper.Base;

// Token: 0x02000187 RID: 391
internal abstract class spr\u171F
{
	// Token: 0x06000DB3 RID: 3507 RVA: 0x000E3A64 File Offset: 0x000E2A64
	internal spr\u171F()
	{
		this.ᜀ = new spr\u2410();
	}

	// Token: 0x06000DB4 RID: 3508 RVA: 0x000E3A84 File Offset: 0x000E2A84
	internal spr\u171F(EsRecordType A_0, int A_1) : this()
	{
		this.ᜀ.ᜀ(A_0);
		this.ᜀ.ᜂ(A_1);
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x000E3AB0 File Offset: 0x000E2AB0
	public virtual string ᜅ()
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return string.Format(ClipboardData.b("፧婩ᅫ䉭偯㩱ᅳ᝵ᱷό๻䑽ﭿ뎁旅ꪅꢇ캉揄ꢑ꒕", a_), base.GetType().Name, this.ᜀ.ToString(), sprὊ.ᜁ(this.ᜂ));
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x000E3B28 File Offset: 0x000E2B28
	internal void ᜀ(BinaryReader A_0, sprά A_1)
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
		this.ᜀ(new spr\u2410(A_0), A_0, A_1);
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x000E3B74 File Offset: 0x000E2B74
	internal void ᜀ(spr\u2410 A_0, BinaryReader A_1, sprά A_2)
	{
		for (;;)
		{
			this.ᜀ = A_0;
			this.ᜁ = A_2;
			int num = (int)A_1.BaseStream.Position;
			this.ᜀ(A_1);
			long position = A_1.BaseStream.Position;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_71;
						}
					}
					IL_71:
					if (false)
					{
					}
					A_1.BaseStream.Position = (long)num;
					this.ᜂ = A_1.ReadBytes(this.ᜀ.ᜄ());
					if (true)
					{
					}
					num2 = 2;
					continue;
				case 1:
					if (spr\u171F.ᜃ)
					{
						num2 = 0;
						continue;
					}
					return;
				case 2:
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x000E3C3C File Offset: 0x000E2C3C
	internal int ᜃ(BinaryWriter A_0)
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
		int num = (int)A_0.BaseStream.Position;
		this.ᜆ().ᜀ(A_0);
		int num2 = (int)A_0.BaseStream.Position;
		this.ᜀ(A_0);
		int num3 = (int)A_0.BaseStream.Position;
		this.ᜆ().ᜀ(num3 - num2);
		A_0.BaseStream.Seek((long)num, SeekOrigin.Begin);
		this.ᜆ().ᜀ(A_0);
		A_0.BaseStream.Seek((long)num3, SeekOrigin.Begin);
		return num3 - num;
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x000E3CEC File Offset: 0x000E2CEC
	internal spr\u2410 ᜆ()
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

	// Token: 0x06000DBA RID: 3514
	protected abstract void ᜀ(BinaryReader A_0);

	// Token: 0x06000DBB RID: 3515
	protected abstract void ᜀ(BinaryWriter A_0);

	// Token: 0x06000DBC RID: 3516 RVA: 0x000E3D30 File Offset: 0x000E2D30
	protected void ᜀ(WarningTypeCore A_0, string A_1)
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
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x000E3D6C File Offset: 0x000E2D6C
	protected sprά ᜄ()
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

	// Token: 0x06000DBE RID: 3518 RVA: 0x000E3DB0 File Offset: 0x000E2DB0
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u171F()
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
	}

	// Token: 0x0400171E RID: 5918
	private spr\u2410 ᜀ;

	// Token: 0x0400171F RID: 5919
	private sprά ᜁ;

	// Token: 0x04001720 RID: 5920
	private byte[] ᜂ;

	// Token: 0x04001721 RID: 5921
	internal static bool ᜃ;
}
