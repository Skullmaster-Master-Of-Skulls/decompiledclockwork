using System;
using System.Collections;
using System.Drawing;
using Spire.CompoundFile.Doc;

// Token: 0x020002F2 RID: 754
internal class spr\u2420 : sprᢿ
{
	// Token: 0x06002966 RID: 10598 RVA: 0x00292DDC File Offset: 0x00291DDC
	public override void ᜀ(sprᴎ A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			PointF a_;
			PointF a_2;
			float num3;
			for (;;)
			{
				int num2;
				ArrayList arrayList;
				switch (num)
				{
				case 0:
					if (this.ᜀ)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					goto IL_F5;
				case 1:
					num = 3;
					continue;
				case 2:
					return;
				case 3:
					if (this.ᜃ >= this.ᜄ)
					{
						goto IL_143;
					}
					goto IL_F5;
				case 5:
					num = 12;
					continue;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_143;
					default:
						goto IL_1D5;
					}
					break;
				case 7:
					goto IL_154;
				case 8:
					goto IL_14F;
				case 9:
					if (num2 >= arrayList.Count)
					{
						num = 6;
						continue;
					}
					a_ = (PointF)arrayList[num2 - 1];
					a_2 = (PointF)arrayList[num2];
					num3 = spr\u2420.ᜀ(a_, a_2);
					this.ᜃ += num3;
					num = 0;
					continue;
				case 10:
					goto IL_154;
				case 11:
					return;
				case 12:
					if (this.ᜁ)
					{
						num = 2;
						continue;
					}
					goto IL_185;
				case 13:
					if (arrayList.Count < 2)
					{
						num = 11;
						continue;
					}
					num2 = 1;
					num = 7;
					continue;
				}
				if (this.ᜀ)
				{
					num = 5;
					continue;
				}
				goto IL_185;
				IL_F5:
				num2++;
				num = 10;
				continue;
				IL_143:
				num = 8;
				continue;
				IL_154:
				num = 9;
				continue;
				IL_185:
				arrayList = A_0.ᜀ();
				num = 13;
			}
			return;
			IL_14F:
			this.ᜂ = spr\u2420.ᜀ(a_, a_2, num3 - (this.ᜃ - this.ᜄ));
			this.ᜁ = true;
			return;
			IL_1D5:
			if (false)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06002967 RID: 10599 RVA: 0x00292FC4 File Offset: 0x00291FC4
	public override void ᜀ(spr\u17F0 A_0)
	{
		int a_ = 13;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new spr\u1D9C(ClipboardData.b("ㅲၴ൶ၸṺོ彾愈ꮊﶎ뎒ﮔ뮚爵햠莢횤튦\ud9a8\udbaa슬\uddae얰횲톴馶", a_));
	}

	// Token: 0x06002968 RID: 10600 RVA: 0x0029301C File Offset: 0x0029201C
	private float ᜂ(spr\u1926 A_0)
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
		this.ᜁ();
		A_0.ᜀ(this);
		float result = this.ᜃ;
		this.ᜁ();
		return result;
	}

	// Token: 0x06002969 RID: 10601 RVA: 0x00293074 File Offset: 0x00292074
	internal PointF ᜀ(spr\u1926 A_0, float A_1)
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
		float num = this.ᜂ(A_0);
		this.ᜄ = num * A_1;
		this.ᜀ();
		A_0.ᜀ(this);
		return this.ᜂ;
	}

	// Token: 0x0600296A RID: 10602 RVA: 0x002930D4 File Offset: 0x002920D4
	internal static PointF ᜀ(PointF A_0, PointF A_1, float A_2)
	{
		switch (0)
		{
		default:
		{
			float num;
			float num3;
			float num5;
			for (;;)
			{
				IL_47:
				num = A_1.Y - A_0.Y;
				float num2 = A_1.X - A_0.X;
				num3 = num / num2;
				int num4 = 0;
				for (;;)
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
						switch (num4)
						{
						case 0:
							if (num2 == 0f)
							{
								num4 = 1;
								continue;
							}
							if (true)
							{
							}
							num5 = A_2 / (float)Math.Sqrt((double)(1f + num3 * num3));
							num4 = 4;
							continue;
						case 1:
							goto IL_8F;
						case 2:
							num5 = -num5;
							num4 = 3;
							continue;
						case 3:
							goto IL_A0;
						case 4:
							goto IL_CA;
						}
						goto IL_47;
					}
					IL_CA:
					if (A_0.X <= A_1.X)
					{
						goto IL_107;
					}
					num4 = 2;
				}
			}
			IL_8F:
			return new PointF(A_0.X, A_0.Y + A_2 * (float)Math.Sign(num));
			IL_A0:
			IL_107:
			float num6 = num3 * num5;
			return new PointF(A_0.X + num5, A_0.Y + num6);
		}
		}
	}

	// Token: 0x0600296B RID: 10603 RVA: 0x00293208 File Offset: 0x00292208
	internal static float ᜀ(PointF A_0, PointF A_1)
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
		float num = A_1.X - A_0.X;
		float num2 = A_1.Y - A_0.Y;
		return (float)Math.Sqrt((double)(num * num + num2 * num2));
	}

	// Token: 0x0600296C RID: 10604 RVA: 0x00293274 File Offset: 0x00292274
	private void ᜁ()
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
		this.ᜃ = 0f;
		this.ᜀ = false;
		this.ᜁ = false;
	}

	// Token: 0x0600296D RID: 10605 RVA: 0x002932C8 File Offset: 0x002922C8
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
		this.ᜃ = 0f;
		this.ᜀ = true;
		this.ᜁ = false;
	}

	// Token: 0x040023F6 RID: 9206
	private new bool ᜀ;

	// Token: 0x040023F7 RID: 9207
	private new bool ᜁ;

	// Token: 0x040023F8 RID: 9208
	private new PointF ᜂ = PointF.Empty;

	// Token: 0x040023F9 RID: 9209
	private float ᜃ;

	// Token: 0x040023FA RID: 9210
	private float ᜄ;
}
