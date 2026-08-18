using System;
using System.Collections;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape.Ps.Wrapping;
using Spire.Pdf.General.Paper.Base;

// Token: 0x02000159 RID: 345
internal class spr\u2248
{
	// Token: 0x06000985 RID: 2437 RVA: 0x0007FC2C File Offset: 0x0007EC2C
	internal spr\u2248(sprά A_0)
	{
		this.ᜇ = A_0;
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0007FC54 File Offset: 0x0007EC54
	internal void ᜂ(spr\u1B70 A_0)
	{
		int a_ = 15;
		int num = 1;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			case 1:
				goto IL_33;
			default:
				goto IL_33;
			}
			IL_9B:
			num = 0;
			continue;
			IL_33:
			if (false)
			{
			}
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				this.ᜁ.Clear();
				this.ᜂ = null;
				this.ᜀ = WrapWhat.Fill;
				num = 4;
				continue;
			case 2:
				goto IL_66;
			case 3:
				if (this.ᜀ != WrapWhat.Fill)
				{
					goto IL_9B;
				}
				goto IL_BC;
			case 4:
				goto IL_89;
			}
			if (A_0 == null)
			{
				num = 2;
			}
			else
			{
				num = 3;
			}
		}
		IL_66:
		throw new ArgumentNullException(ClipboardData.b("մᙶ൸፺", a_));
		IL_89:
		IL_BC:
		this.ᜀ(A_0);
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0007FD24 File Offset: 0x0007ED24
	internal void ᜀ(byte[] A_0, RectangleF A_1)
	{
		int a_ = 13;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!A_1.IsEmpty)
				{
					goto IL_A7;
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
					num = 3;
					continue;
				}
				break;
			case 1:
				goto IL_46;
			case 3:
				goto IL_91;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 1;
			}
			else
			{
				num = 0;
			}
		}
		IL_46:
		throw new ArgumentNullException(ClipboardData.b("ᩲᡴᙶṸṺ㽼پ", a_));
		IL_91:
		throw new ArgumentNullException(ClipboardData.b("ᩲᡴᙶṸṺ㽼ၾ", a_));
		IL_A7:
		this.ᜀ = WrapWhat.Image;
		this.ᜂ = A_0;
		this.ᜃ = A_1;
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x0007FDF0 File Offset: 0x0007EDF0
	internal void ᜁ(spr\u1B70 A_0)
	{
		int a_ = 1;
		int num = 3;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			case 1:
				goto IL_33;
			default:
				goto IL_33;
			}
			IL_94:
			if (true)
			{
			}
			num = 0;
			continue;
			IL_33:
			if (false)
			{
			}
			switch (num)
			{
			case 0:
				this.ᜁ.Clear();
				this.ᜂ = null;
				this.ᜀ = WrapWhat.Stroke;
				num = 2;
				continue;
			case 1:
				if (this.ᜀ != WrapWhat.Stroke)
				{
					goto IL_94;
				}
				goto IL_BD;
			case 2:
				goto IL_81;
			case 4:
				goto IL_5E;
			}
			if (A_0 == null)
			{
				num = 4;
			}
			else
			{
				num = 1;
			}
		}
		IL_5E:
		throw new ArgumentNullException(ClipboardData.b("ᝦࡨὪլ", a_));
		IL_81:
		IL_BD:
		this.ᜀ(A_0);
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0007FEC4 File Offset: 0x0007EEC4
	internal void ᜀ(spr\u24A6 A_0, RectangleF A_1)
	{
		int a_ = 9;
		if (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentNullException(ClipboardData.b("౮ၰᵲʹᙶ੸", a_));
		}
		IL_50:
		this.ᜁ.Clear();
		this.ᜂ = null;
		this.ᜀ = WrapWhat.Canvas;
		this.ᜄ = A_0;
		this.ᜃ = A_1;
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0007FF48 File Offset: 0x0007EF48
	internal static spr\u24A6 ᜀ(sprᲨ A_0)
	{
		spr\u24A6 spr_u24A = new spr\u24A6();
		if (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3B;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return spr_u24A;
		}
		IL_3B:
		spr\u1B70 spr_u1B = spr\u1B70.ᜀ(A_0.ᜀ(), true);
		spr_u1B.ᜀ(new spr\u23F1(spr\u2262.\u1736, 0.1f));
		spr_u24A.ᜁ(spr_u1B);
		return spr_u24A;
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0007FFBC File Offset: 0x0007EFBC
	internal sprᲨ ᜊ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜉ();
				goto IL_62;
			case 1:
				goto IL_6A;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_62:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				if (this.ᜆ != null)
				{
					goto IL_6C;
				}
				num = 0;
				break;
			}
		}
		IL_6A:
		IL_6C:
		return this.ᜆ;
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0008003C File Offset: 0x0007F03C
	private void ᜀ(spr\u1B70 A_0)
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
		this.ᜁ.Add(A_0);
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x00080084 File Offset: 0x0007F084
	private void ᜉ()
	{
		int a_ = 11;
		try
		{
			for (;;)
			{
				WrapWhat wrapWhat = this.ᜀ;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_F5;
					case 1:
						goto IL_188;
					case 2:
						goto IL_193;
					case 3:
						switch (wrapWhat)
						{
						case WrapWhat.Fill:
							this.ᜈ();
							num = 9;
							continue;
						case WrapWhat.Image:
							this.ᜇ();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_108;
							default:
								if (false)
								{
								}
								num = 11;
								continue;
							}
							break;
						case WrapWhat.Stroke:
							this.ᜈ();
							num = 7;
							continue;
						case WrapWhat.Canvas:
							this.ᜅ();
							num = 0;
							continue;
						default:
							num = 4;
							continue;
						}
						break;
					case 4:
						num = 5;
						continue;
					case 5:
						goto IL_F3;
					case 6:
						if (this.ᜂ == null)
						{
							num = 13;
							continue;
						}
						goto IL_188;
					case 7:
						goto IL_F5;
					case 8:
						goto IL_B4;
					case 9:
						goto IL_F5;
					case 10:
						this.ᜅ = new spr\u1D3C();
						num = 8;
						continue;
					case 11:
						goto IL_F5;
					case 12:
						if (this.ᜅ == null)
						{
							goto IL_108;
						}
						goto IL_B4;
					case 13:
						spr\u2101.ᜀ(this.ᜆ);
						num = 1;
						continue;
					}
					break;
					IL_B4:
					this.ᜆ = spr\u1B69.ᜁ(this.ᜅ);
					num = 6;
					continue;
					IL_F5:
					num = 12;
					continue;
					IL_108:
					num = 10;
					continue;
					IL_188:
					num = 2;
				}
			}
			IL_F3:
			throw new ArgumentOutOfRangeException(ClipboardData.b("♰᭲ᑴͶ⵸ᑺ⩼ൾ", a_));
			IL_193:;
		}
		catch
		{
			this.ᜆ = new sprᲨ(this.ᜄ());
			this.ᜇ.ᜀ(WarningTypeCore.MinorFormattingLoss, WarningSourceCore.Shapes, ClipboardData.b("╰ᩲቴὶ൸孺੼ൾ권杖래뮚좜좠춢스螦\udaa8\udaaa\ud8ac캮쎰횲閴\udeb6ힸ좺즼\udabeꃀꟂ", a_));
		}
		if (true)
		{
		}
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x00080284 File Offset: 0x0007F284
	private void ᜈ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_37:
				spr\u1B70[] array = this.ᜀ();
				int num = 0;
				for (;;)
				{
					IL_40:
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num < array.Length)
							{
								spr\u1B70 a_ = array[num];
								spr\u1D3C a_2 = sprṽ.ᜅ(a_);
								num2 = 2;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_40;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								num2 = 6;
								continue;
							}
							break;
						case 1:
							goto IL_A5;
						case 2:
						{
							if (this.ᜅ == null)
							{
								num2 = 3;
								continue;
							}
							spr\u1D3C a_2;
							this.ᜅ.ᜀ(a_2);
							num2 = 5;
							continue;
						}
						case 3:
						{
							spr\u1D3C a_2;
							this.ᜅ = new spr\u1D3C(a_2);
							num2 = 7;
							continue;
						}
						case 4:
							goto IL_A5;
						case 5:
							goto IL_4B;
						case 6:
							return;
						case 7:
							goto IL_4B;
						}
						goto IL_37;
						IL_4B:
						num++;
						num2 = 4;
						continue;
						IL_A5:
						num2 = 0;
					}
				}
			}
			return;
		}
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x00080398 File Offset: 0x0007F398
	private void ᜇ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				break;
			case 1:
				return;
			case 2:
				if (this.ᜅ != null)
				{
					num = 3;
					continue;
				}
				goto IL_53;
			case 3:
				num = 6;
				continue;
			case 4:
				this.ᜆ();
				num = 8;
				continue;
			case 5:
				if (this.ᜅ.ᜀ() == 0)
				{
					num = 4;
					continue;
				}
				return;
			case 6:
				if (this.ᜅ.ᜀ() != 0)
				{
					num = 1;
					continue;
				}
				goto IL_53;
			case 7:
				this.ᜈ();
				num = 2;
				continue;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_49;
				default:
					goto IL_E7;
				}
				break;
			}
			goto IL_3C;
			IL_49:
			num = 7;
			continue;
			IL_3C:
			if (this.ᜁ.Count != 0)
			{
				goto IL_49;
			}
			IL_53:
			this.ᜅ = spr\u20EF.ᜀ(this.ᜂ, this.ᜃ);
			num = 5;
		}
		IL_E7:
		if (false)
		{
		}
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x000804BC File Offset: 0x0007F4BC
	private void ᜆ()
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
		this.ᜅ = new spr\u1D3C(new sprᲨ(new PointF[]
		{
			new PointF(this.ᜃ.Left, this.ᜃ.Top),
			new PointF(this.ᜃ.Right, this.ᜃ.Top),
			new PointF(this.ᜃ.Right, this.ᜃ.Bottom),
			new PointF(this.ᜃ.Left, this.ᜃ.Bottom)
		}));
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x000805AC File Offset: 0x0007F5AC
	private void ᜅ()
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
		this.ᜅ = spr\u1ADB.ᜁ(this.ᜄ, this.ᜃ);
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x00080600 File Offset: 0x0007F600
	private RectangleF ᜄ()
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
		spr\u24A6 a_ = this.ᜃ();
		spr\u197E spr_u197E = new spr\u197E();
		return spr_u197E.ᜀ(a_);
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x00080650 File Offset: 0x0007F650
	private spr\u24A6 ᜃ()
	{
		int a_ = 17;
		for (;;)
		{
			WrapWhat wrapWhat = this.ᜀ;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_93;
				case 1:
					goto IL_54;
				case 2:
					switch (wrapWhat)
					{
					case WrapWhat.Fill:
					case WrapWhat.Stroke:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_54;
						default:
							goto IL_7B;
						}
						break;
					case WrapWhat.Image:
						goto IL_5E;
					case WrapWhat.Canvas:
						goto IL_95;
					default:
						num = 1;
						continue;
					}
					break;
				}
				break;
				IL_54:
				if (true)
				{
				}
				num = 0;
			}
		}
		IL_5E:
		return this.ᜁ();
		IL_7B:
		if (false)
		{
		}
		return this.ᜂ();
		IL_93:
		throw new ArgumentOutOfRangeException(ClipboardData.b("⁶ᅸ᩺ॼ⭾풂麗", a_));
		IL_95:
		return this.ᜄ;
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x0008070C File Offset: 0x0007F70C
	private spr\u24A6 ᜂ()
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
		spr\u24A6 spr_u24A = new spr\u24A6();
		spr_u24A.ᜀ(this.ᜀ());
		return spr_u24A;
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0008075C File Offset: 0x0007F75C
	private spr\u24A6 ᜁ()
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
		spr\u24A6 spr_u24A = new spr\u24A6();
		spr_u24A.ᜁ(new spr\u1DB3(new PointF(this.ᜃ.Left, this.ᜃ.Top), new SizeF(this.ᜃ.Width, this.ᜃ.Height), this.ᜂ));
		return spr_u24A;
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x000807E8 File Offset: 0x0007F7E8
	private spr\u1B70[] ᜀ()
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
		return (spr\u1B70[])this.ᜁ.ToArray(typeof(spr\u1B70));
	}

	// Token: 0x04001384 RID: 4996
	private WrapWhat ᜀ;

	// Token: 0x04001385 RID: 4997
	private readonly ArrayList ᜁ = new ArrayList();

	// Token: 0x04001386 RID: 4998
	private byte[] ᜂ;

	// Token: 0x04001387 RID: 4999
	private RectangleF ᜃ;

	// Token: 0x04001388 RID: 5000
	private spr\u24A6 ᜄ;

	// Token: 0x04001389 RID: 5001
	private spr\u1D3C ᜅ;

	// Token: 0x0400138A RID: 5002
	private sprᲨ ᜆ;

	// Token: 0x0400138B RID: 5003
	private readonly sprά ᜇ;
}
