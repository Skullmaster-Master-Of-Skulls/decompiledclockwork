using System;
using System.IO;
using Spire.Doc.Documents;

// Token: 0x020002BC RID: 700
internal class spr\u225B : spr\u23F8
{
	// Token: 0x0600261E RID: 9758 RVA: 0x0025D1A0 File Offset: 0x0025C1A0
	internal spr\u225B()
	{
		this.ᜉ = new byte[9];
	}

	// Token: 0x0600261F RID: 9759 RVA: 0x0025D1C0 File Offset: 0x0025C1C0
	internal spr\u225B(Stream A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x06002620 RID: 9760 RVA: 0x0025D1DC File Offset: 0x0025C1DC
	internal void ᜁ(Stream A_0)
	{
		int num;
		for (;;)
		{
			long position = A_0.Position;
			spr\u23F8.ᜁ(A_0, this.ᜀ);
			A_0.WriteByte((byte)this.ᜁ);
			num = 0;
			num |= (int)this.ᜂ;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_117;
				case 1:
					num |= (this.ᜅ ? 16 : 0);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_117;
					default:
						if (false)
						{
						}
						num2 = 4;
						continue;
					}
					break;
				case 2:
					num |= (this.ᜃ ? 4 : 0);
					num2 = 5;
					continue;
				case 3:
					num |= (this.ᜇ ? 64 : 0);
					num2 = 0;
					continue;
				case 4:
					num |= (this.ᜆ ? 32 : 0);
					num2 = 3;
					continue;
				case 5:
					num |= (this.ᜄ ? 8 : 0);
					if (true)
					{
					}
					num2 = 1;
					continue;
				}
				break;
			}
		}
		IL_117:
		num |= (this.ᜈ ? 128 : 0);
		A_0.WriteByte((byte)num);
		A_0.Write(this.ᜉ, 0, this.ᜉ.Length);
		A_0.WriteByte((byte)this.ᜊ);
		spr\u23F8.ᜁ(A_0, this.ᜋ);
		spr\u23F8.ᜁ(A_0, this.ᜌ);
		A_0.WriteByte((byte)this.ᜎ.ᜢ().ᜇ());
		A_0.WriteByte((byte)this.ᜏ.ᜪ().ᜇ());
		spr\u23F8.ᜀ(A_0, (ushort)this.\u170D);
		this.ᜏ.ᜪ().ᜄ(A_0);
		this.ᜎ.ᜢ().ᜄ(A_0);
		spr\u23F8.ᜀ(A_0, this.ᜐ);
	}

	// Token: 0x06002621 RID: 9761 RVA: 0x0025D3C4 File Offset: 0x0025C3C4
	private void ᜀ(Stream A_0)
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
		long position = A_0.Position;
		this.ᜉ = new byte[9];
		this.ᜀ = (int)spr\u23F8.ᜃ(A_0);
		this.ᜁ = (ListPatternType)A_0.ReadByte();
		int num = A_0.ReadByte();
		this.ᜂ = (ListNumberAlignment)((byte)(num & 3));
		this.ᜃ = ((num & 4) != 0);
		this.ᜄ = ((num & 8) != 0);
		this.ᜅ = ((num & 16) != 0);
		this.ᜆ = ((num & 32) != 0);
		this.ᜇ = ((num & 64) != 0);
		this.ᜈ = ((num & 128) != 0);
		this.ᜉ = base.ᜂ(A_0, 9);
		this.ᜊ = (FollowCharacterType)A_0.ReadByte();
		this.ᜋ = (int)spr\u23F8.ᜃ(A_0);
		this.ᜌ = (int)spr\u23F8.ᜃ(A_0);
		int a_ = A_0.ReadByte();
		int a_2 = A_0.ReadByte();
		this.\u170D = (int)spr\u23F8.ᜅ(A_0);
		this.ᜎ = new sprℵ(null);
		this.ᜏ = new sprᨽ();
		this.ᜀ(a_2, A_0, false);
		this.ᜀ(a_, A_0, true);
		this.ᜐ = spr\u23F8.ᜀ(A_0);
	}

	// Token: 0x06002622 RID: 9762 RVA: 0x0025D520 File Offset: 0x0025C520
	private void ᜀ(int A_0, Stream A_1, bool A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = 6;
				for (;;)
				{
					sprḍ sprḍ;
					byte[] array;
					sprḍ sprḍ2;
					switch (num2)
					{
					case 0:
						goto IL_112;
					case 1:
						if (A_0 - num <= 1)
						{
							num2 = 0;
							continue;
						}
						for (;;)
						{
							spr\u1CC1 spr_u1CC = new spr\u1CC1();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_85;
							}
						}
						IL_85:
						if (false)
						{
						}
						num2 = 10;
						continue;
					case 2:
						goto IL_F1;
					case 3:
						goto IL_F1;
					case 4:
						if (!A_2)
						{
							num2 = 9;
							continue;
						}
						num2 = 8;
						continue;
					case 5:
						sprḍ = this.ᜏ.ᜪ();
						goto IL_11C;
					case 6:
						if (A_0 != 0)
						{
							num2 = 7;
							continue;
						}
						return;
					case 7:
						num2 = 4;
						continue;
					case 8:
						sprḍ = this.ᜎ.ᜢ();
						goto IL_11C;
					case 9:
						num2 = 5;
						continue;
					case 10:
					{
						spr\u1CC1 spr_u1CC;
						try
						{
							num = spr_u1CC.ᜁ(array, num);
							goto IL_AF;
						}
						catch
						{
							num = A_0;
							goto IL_AF;
						}
						goto IL_F1;
						IL_AF:
						sprḍ2.ᜆ(spr_u1CC);
						num2 = 3;
						continue;
					}
					}
					break;
					IL_F1:
					num2 = 1;
					continue;
					IL_11C:
					sprḍ2 = sprḍ;
					array = new byte[A_0];
					A_1.Read(array, 0, A_0);
					num2 = 2;
				}
			}
			IL_112:
			if (true)
			{
			}
			return;
		}
	}

	// Token: 0x04002213 RID: 8723
	internal new int ᜀ;

	// Token: 0x04002214 RID: 8724
	internal new ListPatternType ᜁ;

	// Token: 0x04002215 RID: 8725
	internal new ListNumberAlignment ᜂ;

	// Token: 0x04002216 RID: 8726
	internal new bool ᜃ;

	// Token: 0x04002217 RID: 8727
	internal new bool ᜄ;

	// Token: 0x04002218 RID: 8728
	internal new bool ᜅ;

	// Token: 0x04002219 RID: 8729
	internal bool ᜆ;

	// Token: 0x0400221A RID: 8730
	internal bool ᜇ;

	// Token: 0x0400221B RID: 8731
	internal bool ᜈ;

	// Token: 0x0400221C RID: 8732
	internal byte[] ᜉ;

	// Token: 0x0400221D RID: 8733
	internal FollowCharacterType ᜊ;

	// Token: 0x0400221E RID: 8734
	internal int ᜋ;

	// Token: 0x0400221F RID: 8735
	internal int ᜌ;

	// Token: 0x04002220 RID: 8736
	internal new int \u170D;

	// Token: 0x04002221 RID: 8737
	internal sprℵ ᜎ;

	// Token: 0x04002222 RID: 8738
	internal sprᨽ ᜏ;

	// Token: 0x04002223 RID: 8739
	internal string ᜐ;
}
