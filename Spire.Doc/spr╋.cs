using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x0200017A RID: 378
internal class spr\u254B : spr\u2562
{
	// Token: 0x06000D3F RID: 3391 RVA: 0x000DBD48 File Offset: 0x000DAD48
	internal override int ᜀ()
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
		return 34 + this.ᜅ.ᜊ() + 2 + this.ᜇ.\u170D() + 2 + this.ᜉ.ᜀ();
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x000DBDB0 File Offset: 0x000DADB0
	public void ᜀ(Stream A_0, bool A_1)
	{
		byte[] array;
		byte[] array2;
		for (;;)
		{
			array = new byte[34];
			A_0.Read(array, 0, 34);
			sprṵ.ᜀ().ᜀ(array, this.ᜄ);
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B1;
				case 1:
					goto IL_143;
				case 2:
					goto IL_B1;
				case 3:
					goto IL_AC;
				case 4:
					this.ᜆ = this.ᜋ;
					num = 3;
					continue;
				case 5:
					if (!A_1)
					{
						this.ᜊ = this.ᜄ.ᜏ;
						num = 2;
						continue;
					}
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
						num = 6;
						continue;
					}
					break;
				case 6:
					this.ᜄ.ᜏ = this.ᜊ;
					num = 0;
					continue;
				case 7:
					if (A_1)
					{
						num = 4;
						continue;
					}
					this.ᜋ = this.ᜆ;
					num = 1;
					continue;
				}
				break;
				IL_B1:
				array2 = new byte[(int)(this.ᜄ.ᜏ * 2)];
				A_0.Read(array2, 0, array2.Length);
				this.ᜅ.ᜀ(array2);
				A_0.Read(array, 0, 2);
				this.ᜆ = BitConverter.ToUInt16(array, 0);
				num = 7;
			}
		}
		IL_AC:
		IL_143:
		array2 = new byte[(int)(this.ᜆ * 4)];
		A_0.Read(array2, 0, array2.Length);
		this.ᜇ.ᜀ(array2);
		A_0.Read(array, 0, 2);
		this.ᜈ = BitConverter.ToUInt16(array, 0);
		array2 = new byte[(int)(this.ᜈ * 8)];
		A_0.Read(array2, 0, array2.Length);
		this.ᜉ.ᜀ(array2);
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x000DBF88 File Offset: 0x000DAF88
	internal override int ᜀ(byte[] A_0, int A_1)
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
		sprṵ.ᜀ().ᜀ(this.ᜄ, A_0, A_1, 34);
		int num = 34;
		A_1 += 34;
		int num2 = this.ᜅ.ᜀ(A_0, A_1);
		A_1 += num2;
		num += num2;
		BitConverter.GetBytes(this.ᜆ).CopyTo(A_0, A_1);
		A_1 += 2;
		num += 2;
		num2 = this.ᜇ.ᜀ(A_0, A_1);
		A_1 += num2;
		num += num2;
		BitConverter.GetBytes(this.ᜈ).CopyTo(A_0, A_1);
		A_1 += 2;
		num += 2;
		num2 = this.ᜉ.ᜀ(A_0, A_1);
		A_1 += num2;
		return num + num2;
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x000DC05C File Offset: 0x000DB05C
	internal override void ᜁ(byte[] A_0, int A_1)
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		throw new Exception(ClipboardData.b("㱧ɩ५乭ᵯ᝱sṵ᝷ṹ屻ᅽꊁ揄憐﶑望뚕벛쾟횡蒣쾥얧\udaa9삫쮭\uddafힱ\udab3습\uddb7\udeb9銻", a_));
	}

	// Token: 0x04001476 RID: 5238
	private new const int ᜀ = 32;

	// Token: 0x04001477 RID: 5239
	private new const int ᜁ = 2;

	// Token: 0x04001478 RID: 5240
	private new const int ᜂ = 4;

	// Token: 0x04001479 RID: 5241
	private new const int ᜃ = 8;

	// Token: 0x0400147A RID: 5242
	internal new spr\u23FB ᜄ = new spr\u23FB();

	// Token: 0x0400147B RID: 5243
	internal sprṭ ᜅ = new sprṭ();

	// Token: 0x0400147C RID: 5244
	internal ushort ᜆ;

	// Token: 0x0400147D RID: 5245
	internal spr\u2484 ᜇ = new spr\u2484();

	// Token: 0x0400147E RID: 5246
	internal ushort ᜈ;

	// Token: 0x0400147F RID: 5247
	internal sprᦃ ᜉ = new sprᦃ();

	// Token: 0x04001480 RID: 5248
	private ushort ᜊ;

	// Token: 0x04001481 RID: 5249
	private ushort ᜋ;
}
