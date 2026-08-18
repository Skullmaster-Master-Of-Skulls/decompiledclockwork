using System;
using System.Drawing;
using System.IO;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;

// Token: 0x02000292 RID: 658
internal class sprᤎ : sprḋ
{
	// Token: 0x060022E3 RID: 8931 RVA: 0x0023AA7C File Offset: 0x00239A7C
	public sprᤎ(Stream A_0, Encoding A_1, bool A_2, bool A_3) : base(A_0, A_1, A_2)
	{
		this.ᜀ = A_3;
	}

	// Token: 0x060022E4 RID: 8932 RVA: 0x0023AA9C File Offset: 0x00239A9C
	internal void ᜁ(string A_0, object A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 is bool)
					{
						num = 1;
						continue;
					}
					num = 9;
					continue;
				case 1:
					goto IL_D0;
				case 2:
					if (A_1 is Color)
					{
						num = 6;
						continue;
					}
					goto IL_15F;
				case 4:
					return;
				case 5:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_9A;
					}
					break;
				case 6:
					goto IL_F2;
				case 7:
					if (A_1 is int)
					{
						num = 8;
						continue;
					}
					num = 2;
					continue;
				case 8:
					goto IL_115;
				case 9:
					if (A_1 is string)
					{
						num = 5;
						continue;
					}
					num = 7;
					continue;
				}
				if (A_1 == null)
				{
					num = 4;
				}
				else
				{
					num = 0;
				}
			}
			return;
			IL_9A:
			if (false)
			{
			}
			string a_2 = (string)A_1;
			this.ᜂ(A_0, a_2);
			return;
			IL_D0:
			bool a_3 = (bool)A_1;
			this.ᜂ(A_0, a_3);
			return;
			IL_F2:
			Color a_4 = (Color)A_1;
			this.ᜀ(A_0, a_4);
			return;
			IL_115:
			int a_5 = (int)A_1;
			this.ᜂ(A_0, a_5);
			return;
			IL_15F:
			throw new InvalidOperationException(ClipboardData.b("⍵ᙷᅹቻᅽꒃ늑ﾙ늛", a_));
		}
		}
	}

	// Token: 0x060022E5 RID: 8933 RVA: 0x0023AC1C File Offset: 0x00239C1C
	internal void ᜂ(string A_0, string A_1)
	{
		int a_ = 4;
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
					base.ᜉ(A_0);
					base.ᜅ(sprᤎ.ᜀ(A_0) + ClipboardData.b("ᱩ൫ɭ", a_), A_1);
					base.ᜈ();
					num = 2;
					continue;
				case 2:
					return;
				}
				if (!spr\u1CC6.ᜋ(A_1))
				{
					break;
				}
				num = 1;
			}
			break;
		}
		}
	}

	// Token: 0x060022E6 RID: 8934 RVA: 0x0023ACC8 File Offset: 0x00239CC8
	internal void ᜄ(string A_0, string A_1)
	{
		int a_ = 17;
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
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					base.ᜉ(A_0);
					base.ᜅ(sprᤎ.ᜀ(A_0) + ClipboardData.b("Ŷᡸ᝺", a_), A_1);
					base.ᜈ();
					if (true)
					{
					}
					num = 2;
					continue;
				case 2:
					return;
				}
				if (A_1 == null)
				{
					break;
				}
				num = 0;
			}
			break;
		}
		}
	}

	// Token: 0x060022E7 RID: 8935 RVA: 0x0023AD6C File Offset: 0x00239D6C
	internal void ᜃ(string A_0, string A_1)
	{
		int a_ = 11;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (spr\u1CC6.ᜋ(A_1))
			{
				this.ᜂ(A_0, A_1);
				return;
			}
			break;
		}
		if (true)
		{
		}
		throw new InvalidOperationException(ClipboardData.b("ばݲŴቶᑸ୺ॼ᩾ꎂꦈﲊﾌ떔릘춠莢쪤햦覨캪사\udfae얰쪲閴솶\ud8b8ힺ좼\udabeꟄ닆뷈꓌믎뫒ꛔꯘ뻚곜꫞裠釢胤菦짨諪軬賮黰臲釴黶韸鳺\uddfc课渀⌂焄漆氈⬊縌氎礐瘒研瘖㜘", a_));
	}

	// Token: 0x060022E8 RID: 8936 RVA: 0x0023ADD8 File Offset: 0x00239DD8
	internal void ᜀ(string A_0, Color A_1)
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
		this.ᜂ(A_0, spr\u25D1.ᜀ(A_1));
	}

	// Token: 0x060022E9 RID: 8937 RVA: 0x0023AE20 File Offset: 0x00239E20
	internal void ᜂ(string A_0, int A_1)
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
		this.ᜂ(A_0, sprᜌ.\u170D(A_1));
	}

	// Token: 0x060022EA RID: 8938 RVA: 0x0023AE68 File Offset: 0x00239E68
	internal void ᜀ(string A_0, byte[] A_1)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				num = 4;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_68;
				default:
					if (false)
					{
					}
					this.ᜂ(A_0, spr\u25D1.ᜀ(A_1));
					num = 0;
					continue;
				}
				break;
			case 4:
				if (A_1.Length > 0)
				{
					goto IL_68;
				}
				return;
			}
			if (A_1 != null)
			{
				if (true)
				{
				}
				num = 2;
				continue;
			}
			break;
			IL_68:
			num = 3;
		}
	}

	// Token: 0x060022EB RID: 8939 RVA: 0x0023AF04 File Offset: 0x00239F04
	internal void ᜁ(string A_0, int A_1)
	{
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
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.ᜂ(A_0, sprᜌ.\u170D(A_1));
					num = 2;
					continue;
				case 2:
					return;
				}
				if (A_1 <= 0)
				{
					break;
				}
				if (true)
				{
				}
				num = 1;
			}
			break;
		}
		}
	}

	// Token: 0x060022EC RID: 8940 RVA: 0x0023AF80 File Offset: 0x00239F80
	internal virtual void ᜂ(string A_0, bool A_1)
	{
		int a_ = 1;
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
			if (!A_1)
			{
				this.ᜂ(A_0, ClipboardData.b("ࡦཨ൪", a_));
				return;
			}
			break;
		}
		base.ᜋ(A_0);
	}

	// Token: 0x060022ED RID: 8941 RVA: 0x0023AFE8 File Offset: 0x00239FE8
	internal void ᜃ(string A_0, bool A_1)
	{
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
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
					base.ᜋ(A_0);
					if (true)
					{
					}
					num = 0;
					continue;
				}
				if (!A_1)
				{
					break;
				}
				num = 2;
			}
			break;
		}
		}
	}

	// Token: 0x060022EE RID: 8942 RVA: 0x0023B060 File Offset: 0x0023A060
	internal void ᜀ(string A_0, int A_1, int A_2)
	{
		if (true)
		{
		}
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
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.ᜂ(A_0, A_1);
					num = 2;
					continue;
				case 2:
					return;
				}
				if (A_1 == A_2)
				{
					break;
				}
				num = 1;
			}
			break;
		}
		}
	}

	// Token: 0x060022EF RID: 8943 RVA: 0x0023B0D8 File Offset: 0x0023A0D8
	internal void ᜁ(string A_0, params object[] A_1)
	{
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
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					base.ᜈ();
					num = 2;
					continue;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					return;
				}
				if (!this.ᜂ(A_0, A_1))
				{
					break;
				}
				num = 0;
			}
			break;
		}
		}
	}

	// Token: 0x060022F0 RID: 8944 RVA: 0x0023B154 File Offset: 0x0023A154
	internal bool ᜂ(string A_0, params object[] A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_1.Length / 2;
				bool flag = false;
				int num2 = 0;
				int num3 = 4;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_151;
					case 1:
						goto IL_151;
					case 2:
						return true;
					case 3:
					{
						if (num2 >= num)
						{
							num3 = 16;
							continue;
						}
						object obj = A_1[num2 * 2 + 1];
						num3 = 13;
						continue;
					}
					case 4:
						goto IL_72;
					case 5:
					{
						int num4;
						if (num4 >= num)
						{
							num3 = 2;
							continue;
						}
						string a_ = (string)A_1[num4 * 2];
						object a_2 = A_1[num4 * 2 + 1];
						this.ᜂ(a_, a_2);
						num4++;
						num3 = 1;
						continue;
					}
					case 6:
						goto IL_1E4;
					case 7:
					{
						if (true)
						{
						}
						object obj;
						if (!(obj is string))
						{
							goto IL_1E4;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return false;
						default:
							if (false)
							{
							}
							num3 = 12;
							continue;
						}
						break;
					}
					case 8:
						num3 = 7;
						continue;
					case 9:
					{
						if (!flag)
						{
							num3 = 11;
							continue;
						}
						base.ᜉ(A_0);
						int num4 = 0;
						num3 = 0;
						continue;
					}
					case 10:
						goto IL_1B7;
					case 11:
						goto IL_1D5;
					case 12:
						num3 = 14;
						continue;
					case 13:
					{
						object obj;
						if (obj != null)
						{
							num3 = 8;
							continue;
						}
						goto IL_176;
					}
					case 14:
					{
						object obj;
						if (!((string)obj == ""))
						{
							num3 = 6;
							continue;
						}
						goto IL_176;
					}
					case 15:
						goto IL_72;
					case 16:
						goto IL_1B7;
					}
					break;
					IL_72:
					num3 = 3;
					continue;
					IL_151:
					num3 = 5;
					continue;
					IL_176:
					num2++;
					num3 = 15;
					continue;
					IL_1B7:
					num3 = 9;
					continue;
					IL_1E4:
					flag = true;
					num3 = 10;
				}
			}
			return false;
			IL_1D5:
			return false;
		}
	}

	// Token: 0x060022F1 RID: 8945 RVA: 0x0023B358 File Offset: 0x0023A358
	internal void ᜀ(string A_0, object A_1, object A_2)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_66;
				default:
					if (false)
					{
					}
					this.ᜀ(A_0, A_1);
					num = 1;
					continue;
				}
				break;
			case 3:
				if (!A_1.Equals(A_2))
				{
					goto IL_66;
				}
				return;
			}
			if (A_1 != null)
			{
				if (true)
				{
				}
				num = 0;
				continue;
			}
			break;
			IL_66:
			num = 2;
		}
	}

	// Token: 0x060022F2 RID: 8946 RVA: 0x0023B3F4 File Offset: 0x0023A3F4
	internal void ᜀ(string A_0, object A_1)
	{
		int a_ = 16;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_47;
			case 1:
				if (A_1 is Color)
				{
					num = 4;
					continue;
				}
				goto IL_DA;
			case 3:
				num = 0;
				continue;
			case 4:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_35;
				default:
					goto IL_97;
				}
				break;
			}
			goto IL_2D;
			IL_35:
			num = 3;
			continue;
			IL_2D:
			if (A_1 is bool)
			{
				goto IL_35;
			}
			num = 1;
		}
		IL_47:
		this.ᜁ(A_0, ((bool)A_1) ? ClipboardData.b("ɵ", a_) : ClipboardData.b("ၵ", a_));
		return;
		IL_97:
		if (false)
		{
		}
		this.ᜁ(A_0, spr\u23B0.ᜁ((Color)A_1));
		return;
		IL_DA:
		this.ᜂ(A_0, A_1);
	}

	// Token: 0x060022F3 RID: 8947 RVA: 0x0023B4E4 File Offset: 0x0023A4E4
	internal void ᜀ(string A_0, bool A_1, bool A_2)
	{
		int a_ = 16;
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
				num = 3;
				continue;
			case 3:
				base.ᜅ(A_0, A_1 ? ClipboardData.b("ɵ", a_) : ClipboardData.b("ၵ", a_));
				num = 1;
				continue;
			}
			if (A_1 == A_2)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
		}
	}

	// Token: 0x060022F4 RID: 8948 RVA: 0x0023B598 File Offset: 0x0023A598
	internal void ᜂ(string A_0, object A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
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
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1C4;
					case 1:
						goto IL_F0;
					case 2:
						return;
					case 4:
						goto IL_112;
					case 5:
						if (A_1 is double)
						{
							num = 9;
							continue;
						}
						num = 11;
						continue;
					case 6:
						goto IL_A8;
					case 7:
						if (A_1 is Color)
						{
							num = 6;
							continue;
						}
						num = 10;
						continue;
					case 8:
						if (A_1 is string)
						{
							num = 4;
							continue;
						}
						num = 13;
						continue;
					case 9:
						goto IL_15B;
					case 10:
						if (A_1 is DateTime)
						{
							num = 1;
							continue;
						}
						goto IL_1E4;
					case 11:
						if (A_1 is bool)
						{
							num = 0;
							continue;
						}
						num = 7;
						continue;
					case 12:
						goto IL_180;
					case 13:
						if (A_1 is int)
						{
							num = 12;
							continue;
						}
						num = 5;
						continue;
					}
					if (A_1 == null)
					{
						num = 2;
					}
					else
					{
						num = 8;
					}
				}
				return;
				IL_A8:
				if (true)
				{
				}
				Color a_2 = (Color)A_1;
				this.ᜁ(A_0, spr\u25D1.ᜀ(a_2));
				return;
				IL_F0:
				DateTime a_3 = (DateTime)A_1;
				this.ᜁ(A_0, sprᜌ.ᜃ(a_3));
				return;
				IL_112:
				string a_4 = (string)A_1;
				this.ᜁ(A_0, a_4);
				return;
				IL_180:
				int a_5 = (int)A_1;
				this.ᜀ(A_0, a_5);
				return;
				IL_1C4:
				bool a_6 = (bool)A_1;
				this.ᜁ(A_0, a_6);
				return;
				IL_1E4:
				throw new InvalidOperationException(ClipboardData.b("⁴᥶ቸᕺቼࡾꎂﮎ놐ﲘ뮚膠힤캦\udda8캪\udbae얰솲\udcb4햶첸쾺\ud8bc龾곀ꛂ뇄꿆ꛈ꿊", a_));
			}
			}
			IL_15B:
			double a_7 = (double)A_1;
			this.ᜀ(A_0, a_7);
			return;
		}
		}
	}

	// Token: 0x060022F5 RID: 8949 RVA: 0x0023B79C File Offset: 0x0023A79C
	internal void ᜁ(string A_0, object A_1, object A_2)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				A_1 = A_2;
				num = 2;
				continue;
			case 2:
				goto IL_62;
			}
			IL_1C:
			if (true)
			{
			}
			if (A_1 != null)
			{
				break;
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
				num = 1;
				continue;
			}
			goto IL_1C;
		}
		IL_62:
		this.ᜂ(A_0, A_1);
	}

	// Token: 0x060022F6 RID: 8950 RVA: 0x0023B818 File Offset: 0x0023A818
	internal void ᜁ(string A_0, string A_1)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				base.ᜅ(A_0, A_1);
				num = 2;
				continue;
			case 2:
				goto IL_64;
			}
			IL_1C:
			if (!spr\u1CC6.ᜋ(A_1))
			{
				break;
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
				num = 1;
				continue;
			}
			goto IL_1C;
		}
		IL_64:
		if (true)
		{
		}
	}

	// Token: 0x060022F7 RID: 8951 RVA: 0x0023B894 File Offset: 0x0023A894
	internal void ᜀ(string A_0, string A_1, string A_2)
	{
		int num = 2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				this.ᜁ(A_0, A_1);
				num = 1;
				continue;
			case 1:
				return;
			}
			IL_24:
			if (!(A_1 != A_2))
			{
				break;
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
				num = 0;
				continue;
			}
			goto IL_24;
		}
	}

	// Token: 0x060022F8 RID: 8952 RVA: 0x0023B910 File Offset: 0x0023A910
	internal void ᜁ(string A_0, Color A_1)
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
		base.ᜅ(A_0, spr\u25D1.ᜀ(A_1));
	}

	// Token: 0x060022F9 RID: 8953 RVA: 0x0023B958 File Offset: 0x0023A958
	internal void ᜀ(string A_0, int A_1)
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
		base.ᜅ(A_0, sprᜌ.\u170D(A_1));
	}

	// Token: 0x060022FA RID: 8954 RVA: 0x0023B9A0 File Offset: 0x0023A9A0
	internal void ᜁ(string A_0, byte[] A_1)
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
		base.ᜅ(A_0, spr\u25D1.ᜀ(A_1));
	}

	// Token: 0x060022FB RID: 8955 RVA: 0x0023B9E8 File Offset: 0x0023A9E8
	internal void ᜀ(string A_0, double A_1)
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
		base.ᜅ(A_0, sprᜌ.ᜁ(A_1));
	}

	// Token: 0x060022FC RID: 8956 RVA: 0x0023BA30 File Offset: 0x0023AA30
	internal virtual void ᜁ(string A_0, bool A_1)
	{
		int a_ = 13;
		if (true)
		{
		}
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_71;
			case 1:
				num = 0;
				continue;
			case 2:
				goto IL_A9;
			}
			IL_31:
			if (!this.ᜀ)
			{
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
				num = 1;
				continue;
			}
			goto IL_31;
		}
		IL_71:
		base.ᜅ(A_0, A_1 ? ClipboardData.b("ᱲ᭴", a_) : ClipboardData.b("ᱲ፴ᅶ", a_));
		return;
		IL_A9:
		base.ᜅ(A_0, A_1 ? ClipboardData.b("ݲݴɶᱸ", a_) : ClipboardData.b("ᕲᑴ᭶੸Ṻ", a_));
	}

	// Token: 0x060022FD RID: 8957 RVA: 0x0023BB14 File Offset: 0x0023AB14
	internal virtual void ᜀ(string A_0, bool A_1)
	{
		int a_ = 0;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				num = 3;
				continue;
			case 2:
				return;
			case 3:
				base.ᜅ(A_0, this.ᜀ ? ClipboardData.b("॥٧", a_) : ClipboardData.b("ብᩧὩ५", a_));
				if (true)
				{
				}
				num = 2;
				continue;
			}
			if (!A_1)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
		}
	}

	// Token: 0x060022FE RID: 8958 RVA: 0x0023BBCC File Offset: 0x0023ABCC
	protected static object ᜀ(bool A_0)
	{
		if (!A_0)
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
				return null;
			}
		}
		if (true)
		{
		}
		return A_0;
	}

	// Token: 0x060022FF RID: 8959 RVA: 0x0023BC14 File Offset: 0x0023AC14
	private static string ᜀ(string A_0)
	{
		int num = A_0.IndexOf(':');
		if (num <= 0)
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
				return "";
			}
		}
		return A_0.Substring(0, num + 1);
	}

	// Token: 0x06002300 RID: 8960 RVA: 0x0023BC70 File Offset: 0x0023AC70
	internal void ᜀ(string A_0, params object[] A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_1.Length / 2;
				bool flag = false;
				int num2 = 0;
				int num3 = 6;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_12B;
					case 1:
						goto IL_7B;
					case 2:
						goto IL_148;
					case 3:
						goto IL_14A;
					case 4:
						flag = true;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7B;
						default:
							if (false)
							{
							}
							num3 = 1;
							continue;
						}
						break;
					case 5:
					{
						if (!flag)
						{
							num3 = 7;
							continue;
						}
						base.ᜉ(A_0);
						int num4 = 0;
						num3 = 0;
						continue;
					}
					case 6:
						goto IL_14A;
					case 7:
						return;
					case 8:
					{
						int num4;
						if (num4 >= num)
						{
							num3 = 2;
							continue;
						}
						this.ᜀ((string)A_1[num4 * 2], (spr\u2587)A_1[num4 * 2 + 1]);
						num4++;
						num3 = 12;
						continue;
					}
					case 9:
					{
						if (num2 >= num)
						{
							num3 = 11;
							continue;
						}
						spr\u2587 spr_u = (spr\u2587)A_1[num2 * 2 + 1];
						num3 = 10;
						continue;
					}
					case 10:
					{
						spr\u2587 spr_u;
						if (spr_u != null)
						{
							num3 = 4;
							continue;
						}
						num2++;
						num3 = 3;
						continue;
					}
					case 11:
						goto IL_7B;
					case 12:
						goto IL_12B;
					}
					break;
					IL_7B:
					num3 = 5;
					continue;
					IL_12B:
					num3 = 8;
					continue;
					IL_14A:
					if (true)
					{
					}
					num3 = 9;
				}
			}
			return;
			IL_148:
			base.ᜈ();
			return;
		}
	}

	// Token: 0x06002301 RID: 8961 RVA: 0x0023BE08 File Offset: 0x0023AE08
	internal void ᜀ(string A_0, spr\u2587 A_1)
	{
		int a_ = 4;
		int num = 2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (A_1.ᜈ() == BorderStyle.None)
				{
					num = 3;
					continue;
				}
				goto IL_B1;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				goto Block_3;
			}
			if (A_1 == null)
			{
				num = 0;
			}
			else
			{
				num = 1;
			}
		}
		return;
		Block_3:
		if (false)
		{
		}
		this.ᜁ(A_0, new object[]
		{
			ClipboardData.b("ᵩ噫ᡭᅯṱ", a_),
			ClipboardData.b("ѩիɭ", a_)
		});
		return;
		IL_B1:
		this.ᜁ(A_0, A_1);
	}

	// Token: 0x06002302 RID: 8962 RVA: 0x0023BED0 File Offset: 0x0023AED0
	protected virtual void ᜁ(string A_0, spr\u2587 A_1)
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
	}

	// Token: 0x06002303 RID: 8963 RVA: 0x0023BF0C File Offset: 0x0023AF0C
	internal void ᜀ(string A_0, string A_1)
	{
		int a_ = 1;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (true)
				{
				}
				this.ᜁ(A_0, A_1, ClipboardData.b("ͦᅨ੪", a_));
				num = 0;
				continue;
			}
			IL_25:
			if (!spr\u1CC6.ᜋ(A_1))
			{
				break;
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
				num = 1;
				continue;
			}
			goto IL_25;
		}
	}

	// Token: 0x06002304 RID: 8964 RVA: 0x0023BFA0 File Offset: 0x0023AFA0
	protected void ᜁ(string A_0, string A_1, string A_2)
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
		this.ᜁ(A_0, new object[]
		{
			ClipboardData.b("ὧ偩᭫", a_),
			A_1,
			ClipboardData.b("ὧ偩ᡫ᝭o᝱", a_),
			A_2
		});
	}

	// Token: 0x06002305 RID: 8965 RVA: 0x0023C020 File Offset: 0x0023B020
	internal void ᜀ(string A_0, string A_1, string A_2, string A_3, string A_4)
	{
		int a_ = 13;
		for (;;)
		{
			IL_09:
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (spr\u1CC6.ᜋ(A_4))
					{
						num = 5;
						continue;
					}
					return;
				case 1:
					if (!spr\u1CC6.ᜋ(A_2))
					{
						num = 6;
						continue;
					}
					goto IL_D9;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 3:
					return;
				case 5:
					goto IL_D9;
				case 6:
					num = 8;
					continue;
				case 7:
					num = 0;
					continue;
				case 8:
					if (!spr\u1CC6.ᜋ(A_3))
					{
						if (true)
						{
						}
						num = 7;
						continue;
					}
					goto IL_D9;
				}
				if (!spr\u1CC6.ᜋ(A_1))
				{
					num = 2;
					continue;
				}
				IL_D9:
				base.ᜉ(A_0);
				this.ᜀ(ClipboardData.b("Ѳ佴Ͷᙸ୺", a_), A_1);
				this.ᜀ(ClipboardData.b("Ѳ佴᭶ᱸᵺॼ", a_), A_2);
				this.ᜀ(ClipboardData.b("Ѳ佴ᕶᙸེॼၾ", a_), A_3);
				this.ᜀ(ClipboardData.b("Ѳ佴նၸᱺᕼ୾", a_), A_4);
				base.ᜈ();
				num = 3;
			}
		}
	}

	// Token: 0x06002306 RID: 8966 RVA: 0x0023C178 File Offset: 0x0023B178
	internal void ᜂ(string A_0, string A_1, string A_2)
	{
		int a_ = 7;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜁ(A_0, string.Format(ClipboardData.b("ᙬ彮౰彲๴䙶Ѹ", a_), A_1, A_2).TrimEnd(new char[]
		{
			','
		}));
	}

	// Token: 0x06002307 RID: 8967 RVA: 0x0023C1EC File Offset: 0x0023B1EC
	internal void ᜀ(string A_0, string A_1, string A_2, string A_3)
	{
		int a_ = 3;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜁ(A_0, string.Format(ClipboardData.b("ቨ孪ၬ䍮ੰ䉲ࡴ孶ɸ䥺|", a_), A_1, A_2, A_3).TrimEnd(new char[]
		{
			','
		}));
	}

	// Token: 0x06002308 RID: 8968 RVA: 0x0023C260 File Offset: 0x0023B260
	internal void ᜀ(string A_0, PreferredWidth A_1)
	{
		if (A_1 != null)
		{
			if (true)
			{
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
				this.ᜁ(A_0, sprᜌ.\u170D((int)A_1.Value), sprḕ.ᜀ(A_1.Type));
				return;
			}
		}
	}

	// Token: 0x06002309 RID: 8969 RVA: 0x0023C2C0 File Offset: 0x0023B2C0
	internal virtual void ᜀ()
	{
		int a_ = 7;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotImplementedException(ClipboardData.b("⹬nὰၲݴቶ൸Ṻ嵼᱾권ﲎ戀ﲒﮖﶘ뮚토쾢삤쪦첨얪\ud9ac膮", a_));
	}

	// Token: 0x0600230A RID: 8970 RVA: 0x0023C318 File Offset: 0x0023B318
	internal virtual bool ᜁ()
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
		return false;
	}

	// Token: 0x0600230B RID: 8971 RVA: 0x0023C354 File Offset: 0x0023B354
	internal void ᜄ()
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
		this.ᜁ++;
	}

	// Token: 0x0600230C RID: 8972 RVA: 0x0023C3A0 File Offset: 0x0023B3A0
	internal void ᜃ()
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
		this.ᜁ--;
	}

	// Token: 0x0600230D RID: 8973 RVA: 0x0023C3EC File Offset: 0x0023B3EC
	internal bool ᜂ()
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
		return this.ᜁ > 0;
	}

	// Token: 0x04002136 RID: 8502
	private new readonly bool ᜀ;

	// Token: 0x04002137 RID: 8503
	private int ᜁ;
}
