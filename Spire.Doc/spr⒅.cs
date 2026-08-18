using System;
using System.Drawing.Imaging;
using System.IO;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;

// Token: 0x02000235 RID: 565
internal class spr\u2485
{
	// Token: 0x06001AF6 RID: 6902 RVA: 0x001C4AB8 File Offset: 0x001C3AB8
	internal spr\u2485(MemoryStream A_0)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x001C4AE0 File Offset: 0x001C3AE0
	internal MemoryStream ᜂ()
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

	// Token: 0x06001AF8 RID: 6904 RVA: 0x001C4B24 File Offset: 0x001C3B24
	internal sprᤉ ᜁ()
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

	// Token: 0x06001AF9 RID: 6905 RVA: 0x001C4B68 File Offset: 0x001C3B68
	internal int ᜀ(DocPicture A_0, int A_1, int A_2)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_89;
			case 1:
				goto IL_61;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_61;
				}
				if (false)
				{
				}
				break;
			}
			if (!A_0.PictureShape.ᜊ().ᜃ().ᜀ())
			{
				num = 1;
				continue;
			}
			break;
			IL_61:
			this.ᜁ = A_0.PictureShape.ᜊ().ᜀ();
			if (true)
			{
			}
			num = 0;
		}
		IL_89:
		this.ᜁ.ᜀ(A_1, A_2, A_0.HeightScale, A_0.WidthScale);
		this.ᜁ.ᜣ = 0;
		this.ᜁ.ᜄ = 100;
		this.ᜁ.ᜈ = 8;
		spr\u2459 spr_u = new spr\u2459(A_0.Document);
		spr_u.ᜀ(A_0);
		long position = this.ᜀ.Position;
		this.ᜀ.Position += 68L;
		spr_u.ᜂ(this.ᜀ);
		int num2 = (int)this.ᜀ.Position;
		this.ᜁ.ᜂ = (int)((long)num2 - position);
		this.ᜁ.ᜃ = 68;
		this.ᜀ.Position = position;
		this.ᜁ.ᜁ(this.ᜀ);
		this.ᜀ.Position = (long)num2;
		return num2;
	}

	// Token: 0x06001AFA RID: 6906 RVA: 0x001C4CD4 File Offset: 0x001C3CD4
	internal int ᜀ(sprẛ A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u2459 spr_u = A_0.ᜀ();
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_236;
					case 1:
						if (A_0.ᜆ() != null)
						{
							num = 5;
							continue;
						}
						goto IL_26B;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_53;
						}
						goto Block_4;
					case 3:
						num = 1;
						continue;
					case 4:
						if (A_0.ᜊ() != null)
						{
							num = 3;
							continue;
						}
						goto IL_26B;
					case 5:
					{
						long position = this.ᜀ.Position;
						this.ᜀ.Position += 68L;
						BinaryWriter binaryWriter = new BinaryWriter(this.ᜀ);
						binaryWriter.Write(A_0.ᜆ());
						int num2 = (int)this.ᜀ.Position;
						A_0.ᜊ().ᜂ = (int)((long)num2 - position);
						A_0.ᜊ().ᜃ = 68;
						this.ᜀ.Position = position;
						A_0.ᜊ().ᜁ(this.ᜀ);
						this.ᜀ.Position = (long)num2;
						num = 2;
						continue;
					}
					case 6:
					{
						long position2 = this.ᜀ.Position;
						this.ᜀ.Position += 68L;
						spr_u.ᜂ(this.ᜀ);
						int num3 = (int)this.ᜀ.Position;
						A_0.ᜊ().ᜂ = (int)((long)num3 - position2);
						A_0.ᜊ().ᜃ = 68;
						this.ᜀ.Position = position2;
						A_0.ᜊ().ᜁ(this.ᜀ);
						this.ᜀ.Position = (long)num3;
						num = 0;
						continue;
					}
					case 7:
						goto IL_AC;
					case 8:
						num = 10;
						continue;
					case 9:
						goto IL_53;
					case 10:
						if (A_0.ᜊ().ᜃ == 68)
						{
							num = 6;
							continue;
						}
						A_0.ᜊ().ᜁ(this.ᜀ);
						spr_u.ᜂ(this.ᜀ);
						num = 7;
						continue;
					}
					break;
					IL_53:
					if (spr_u != null)
					{
						num = 8;
					}
					else
					{
						num = 4;
					}
				}
			}
			IL_AC:
			goto IL_26B;
			Block_4:
			if (false)
			{
			}
			goto IL_26B;
			IL_236:
			if (true)
			{
			}
			IL_26B:
			return (int)this.ᜀ.Position;
		}
	}

	// Token: 0x06001AFB RID: 6907 RVA: 0x001C4F58 File Offset: 0x001C3F58
	internal int ᜀ(TextBoxFormat A_0)
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
		spr\u2459 spr_u = new spr\u2459(A_0.Document);
		spr_u.ᜋ();
		new sprᤉ();
		int a_ = (int)Math.Round((double)(A_0.Height * 20f));
		int a_2 = (int)Math.Round((double)(A_0.Width * 20f));
		this.ᜁ.ᜀ(a_, a_2, 100f, 100f);
		long num = (long)((int)this.ᜀ.Position);
		this.ᜀ.Position += 68L;
		spr_u.ᜂ(this.ᜀ);
		long num2 = (long)((int)this.ᜀ.Position);
		this.ᜁ.ᜂ = (int)(num2 - num);
		this.ᜁ.ᜃ = 68;
		this.ᜁ.ᜄ = 100;
		this.ᜁ.ᜈ = 2;
		this.ᜀ.Position = num;
		this.ᜁ.ᜁ(this.ᜀ);
		this.ᜀ.Position = num2;
		return (int)this.ᜀ.Position;
	}

	// Token: 0x06001AFC RID: 6908 RVA: 0x001C5098 File Offset: 0x001C4098
	private void ᜀ()
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
		this.ᜁ.ᜂ += 205;
		this.ᜁ.ᜃ = 68;
		this.ᜁ.ᜁ(this.ᜀ);
	}

	// Token: 0x04001E99 RID: 7833
	private MemoryStream ᜀ;

	// Token: 0x04001E9A RID: 7834
	private sprᤉ ᜁ = new sprᤉ();

	// Token: 0x04001E9B RID: 7835
	private Metafile ᜂ;
}
