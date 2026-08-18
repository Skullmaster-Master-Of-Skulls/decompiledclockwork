using System;
using System.Drawing;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;

// Token: 0x02000405 RID: 1029
internal class sprṾ
{
	// Token: 0x06003942 RID: 14658 RVA: 0x003546C8 File Offset: 0x003536C8
	public sprṾ(sprᩍ A_0)
	{
		int a_ = 10;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(ClipboardData.b("ͯᩱᕳٵᵷ", a_));
		}
		spr\u1F9B a_2 = new spr\u1F9B(A_0);
		this.ᜁ = spr\u241F.ᜀ(a_2, null);
		this.ᜀ(a_2);
	}

	// Token: 0x06003943 RID: 14659 RVA: 0x00354718 File Offset: 0x00353718
	public SizeF ᜀ()
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
		return this.ᜀ.ᜀ().Size;
	}

	// Token: 0x06003944 RID: 14660 RVA: 0x00354768 File Offset: 0x00353768
	public Size ᜀ(float A_0, float A_1)
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
		return spr\u23C4.ᜀ(this.ᜀ(), A_0, (double)A_1);
	}

	// Token: 0x06003945 RID: 14661 RVA: 0x003547B4 File Offset: 0x003537B4
	public SizeF ᜀ(Graphics A_0, float A_1, float A_2, float A_3)
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
		spr\u23A8 spr_u23A = new spr\u23A8();
		return spr_u23A.ᜀ(this.ᜁ, this.ᜀ(), A_0, A_1, A_2, A_3);
	}

	// Token: 0x06003946 RID: 14662 RVA: 0x0035480C File Offset: 0x0035380C
	public float ᜀ(Graphics A_0, float A_1, float A_2, float A_3, float A_4)
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
		spr\u23A8 spr_u23A = new spr\u23A8();
		return spr_u23A.ᜀ(this.ᜁ, this.ᜀ(), A_0, A_1, A_2, A_3, A_4);
	}

	// Token: 0x06003947 RID: 14663 RVA: 0x00354868 File Offset: 0x00353868
	public void ᜀ(string A_0)
	{
		int a_ = 7;
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
			spr\u1CC6.ᜁ(A_0, ClipboardData.b("୬ٮᵰᙲ㭴ᙶᑸṺ", a_));
			Stream stream = File.Create(A_0);
			try
			{
				if (true)
				{
				}
				this.ᜀ(stream);
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_94;
					case 2:
						((IDisposable)stream).Dispose();
						num = 1;
						continue;
					}
					if (stream == null)
					{
						break;
					}
					num = 2;
				}
				IL_94:;
			}
			break;
		}
		}
	}

	// Token: 0x06003948 RID: 14664 RVA: 0x0035491C File Offset: 0x0035391C
	public void ᜀ(Stream A_0)
	{
		int a_ = 15;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_BF:
			num = 4;
			break;
		default:
			if (false)
			{
			}
			num = 2;
			break;
		}
		SizeF sizeF;
		spr\u1808 a_2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_11D;
			case 1:
			{
				spr\u1DB3 spr_u1DB = new spr\u1DB3(new PointF(0f, 0f), new SizeF(32f, 32f), spr\u1CC6.ᜁ());
				this.ᜁ.ᜁ(spr_u1DB);
				sizeF = spr_u1DB.ᜂ();
				num = 0;
				continue;
			}
			case 3:
				goto IL_64;
			case 4:
				if (this.ᜁ.ᜉ() == 0)
				{
					num = 1;
					continue;
				}
				goto IL_11F;
			case 5:
				goto IL_BF;
			case 6:
				if (sizeF == SizeF.Empty)
				{
					num = 5;
					continue;
				}
				goto IL_11F;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				num = 3;
			}
			else
			{
				a_2 = new spr\u1808();
				sizeF = this.ᜀ();
				num = 6;
			}
		}
		IL_64:
		throw new ArgumentNullException(ClipboardData.b("ٴͶ୸Ṻᱼቾ", a_));
		IL_11D:
		IL_11F:
		sprḪ.ᜀ(this.ᜁ, sizeF, A_0, ImageType.Emf, a_2);
	}

	// Token: 0x06003949 RID: 14665 RVA: 0x00354A58 File Offset: 0x00353A58
	private void ᜀ(sprố A_0)
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
		this.ᜁ.ᜀ(new spr\u25FD(1f, 0f, 0f, 1f, -this.ᜀ.ᜀ().Left, -this.ᜀ.ᜀ().Top));
	}

	// Token: 0x04002AB5 RID: 10933
	private sprố ᜀ;

	// Token: 0x04002AB6 RID: 10934
	private readonly spr\u24A6 ᜁ;
}
