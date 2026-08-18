using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Spire.Doc;
using Spire.Doc.Documents;

// Token: 0x020001E3 RID: 483
internal class spr\u230C : spr\u2304
{
	// Token: 0x06001507 RID: 5383 RVA: 0x001567DC File Offset: 0x001557DC
	public SizeF[] ᜀ(spr\u1937[] A_0)
	{
		SizeF[] array;
		for (;;)
		{
			array = new SizeF[A_0.Length];
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return array;
				case 1:
					goto IL_2B;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2B;
					default:
						if (false)
						{
						}
						if (num >= array.Length)
						{
							num2 = 0;
							continue;
						}
						array[num] = this.ᜀ(A_0[num]);
						num++;
						num2 = 3;
						continue;
					}
					break;
				case 3:
					goto IL_35;
				}
				break;
				IL_35:
				num2 = 2;
				continue;
				IL_2B:
				if (true)
				{
				}
				goto IL_35;
			}
		}
		return array;
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x00156884 File Offset: 0x00155884
	private SizeF ᜀ(spr\u1937 A_0)
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
		sprᾔ a_ = this.ᜀ(A_0);
		sprṏ a_2 = new sprṏ(new spr\u1F9B(A_0), a_, null);
		float a_3 = sprᣛ.ᜀ(A_0);
		return sprᣛ.ᜀ(a_2, a_3).Size;
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x001568EC File Offset: 0x001558EC
	private sprᾔ ᜀ(DocumentObject A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			sprᢋ sprᢋ;
			sprᾔ sprᾔ;
			for (;;)
			{
				sprᢋ sprᢋ2;
				switch (num)
				{
				case 0:
				{
					DocumentObject documentObject;
					if (documentObject != null)
					{
						num = 10;
						continue;
					}
					goto IL_176;
				}
				case 1:
					goto IL_67;
				case 2:
					goto IL_12F;
				case 3:
				{
					if (sprᢋ == null)
					{
						num = 2;
						continue;
					}
					if (true)
					{
					}
					DocumentObject documentObject = sprᢋ.ParentObject;
					sprᾔ = sprᾔ.ᜀ();
					num = 0;
					continue;
				}
				case 4:
					sprᢋ2 = null;
					goto IL_113;
				case 5:
					if (A_0.DocumentObjectType != DocumentObjectType.ShapeGroup)
					{
						num = 12;
						continue;
					}
					num = 8;
					continue;
				case 6:
					goto IL_9E;
				case 8:
					sprᢋ2 = (sprᢋ)A_0;
					goto IL_113;
				case 9:
				{
					DocumentObject documentObject;
					sprᾔ = this.ᜀ(documentObject);
					num = 6;
					continue;
				}
				case 10:
					num = 11;
					continue;
				case 11:
				{
					DocumentObject documentObject;
					if (documentObject.DocumentObjectType == DocumentObjectType.ShapeGroup)
					{
						num = 9;
						continue;
					}
					goto IL_176;
				}
				case 12:
					num = 4;
					continue;
				}
				if (A_0.DocumentObjectType == DocumentObjectType.Shape)
				{
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				IL_113:
				sprᢋ = sprᢋ2;
				num = 3;
			}
			IL_67:
			return this.ᜀ(A_0.ParentObject);
			IL_9E:
			goto IL_176;
			IL_12F:
			return sprᾔ.ᜀ();
			IL_176:
			SizeF a_ = sprᢋ.ᝡ();
			spr\u25FD spr_u25FD = spr\u1BA8.ᜀ(sprᢋ, a_, false);
			spr_u25FD.ᜀ(sprᾔ.ᜂ(), MatrixOrder.Append);
			spr\u25FD spr_u25FD2 = spr\u1BA8.ᜀ(sprᢋ, a_, true);
			spr_u25FD2.ᜀ(sprᾔ.ᜂ(), MatrixOrder.Append);
			SizeF sizeF = spr\u1BA8.ᜀ(sprᢋ, sprᾔ.ᜃ(), sprᾔ.ᜁ());
			float width = sizeF.Width;
			float height = sizeF.Height;
			float a_2 = a_.Width / (float)sprᢋ.\u1776() * width;
			float a_3 = a_.Height / (float)sprᢋ.ឍ() * height;
			float a_4 = (float)(sprᢋ.ម() + (double)sprᾔ.ᜄ());
			return new sprᾔ(spr_u25FD, a_2, a_3, a_4);
		}
		}
	}
}
