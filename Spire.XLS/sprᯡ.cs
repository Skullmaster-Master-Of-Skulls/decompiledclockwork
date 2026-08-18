using System;
using System.Collections.Generic;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003D2 RID: 978
[CLSCompliant(false)]
[spr\u2400(FormulaToken.tAttr)]
internal class sprᯡ : spr\u231A
{
	// Token: 0x06003B3E RID: 15166 RVA: 0x00213228 File Offset: 0x00212228
	public sprᯡ()
	{
		this.ᜈ = 0;
		this.ᜉ = 0;
		base.ᜀ(1);
		this.TokenCode = FormulaToken.tAttr;
	}

	// Token: 0x06003B3F RID: 15167 RVA: 0x00213258 File Offset: 0x00212258
	public sprᯡ(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x06003B40 RID: 15168 RVA: 0x00213270 File Offset: 0x00212270
	public sprᯡ(byte A_0, ushort A_1)
	{
		this.TokenCode = FormulaToken.tAttr;
		this.ᜈ = A_0;
		this.ᜉ = A_1;
		base.ᜀ((this.ᜈ == 1) ? 0 : 1);
	}

	// Token: 0x06003B41 RID: 15169 RVA: 0x002132B0 File Offset: 0x002122B0
	public sprᯡ(int A_0, int A_1) : this((byte)A_0, (ushort)A_1)
	{
	}

	// Token: 0x06003B42 RID: 15170 RVA: 0x002132C8 File Offset: 0x002122C8
	public byte ᜋ()
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
		return this.ᜈ;
	}

	// Token: 0x06003B43 RID: 15171 RVA: 0x0021330C File Offset: 0x0021230C
	public ushort ᜈ()
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
		return this.ᜉ;
	}

	// Token: 0x06003B44 RID: 15172 RVA: 0x00213350 File Offset: 0x00212350
	public new void ᜀ(ushort A_0)
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
		this.ᜉ = A_0;
	}

	// Token: 0x06003B45 RID: 15173 RVA: 0x00213394 File Offset: 0x00212394
	public int ᜌ()
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
		return (int)(this.ᜉ & 255);
	}

	// Token: 0x06003B46 RID: 15174 RVA: 0x002133DC File Offset: 0x002123DC
	internal void ᜁ(int A_0)
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
		this.ᜉ = (ushort)((int)(this.ᜉ & 65280) | (A_0 & 255));
	}

	// Token: 0x06003B47 RID: 15175 RVA: 0x00213434 File Offset: 0x00212434
	public int ᜊ()
	{
		if (this.ᜉ())
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return -1;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜉ >> 8;
		}
		return -1;
	}

	// Token: 0x06003B48 RID: 15176 RVA: 0x00213484 File Offset: 0x00212484
	public new void ᜀ(int A_0)
	{
		int a_ = 13;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E4;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 2:
				if (A_0 >= 1)
				{
					num = 1;
					continue;
				}
				goto IL_4E;
			case 3:
				goto IL_92;
			case 4:
				if (true)
				{
				}
				num = 2;
				continue;
			case 5:
				if (A_0 > 255)
				{
					num = 3;
					continue;
				}
				goto IL_CC;
			}
			if (!this.ᜉ())
			{
				goto IL_E4;
			}
			num = 4;
		}
		IL_4E:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㕂⑄⭆㱈⹊", a_), RecordTableEnumerator.b("ᕂ⑄⭆㱈⹊浌ⱎぐ㵒㭔㡖ⵘ筚㽜㩞䅠རdᑦᩨ䭪ᥬݮၰᵲ啴䙶奸᩺፼᭾ꆀﾊﶎ놐ﶔ뮚꾜ꪞ钠趢", a_));
		IL_92:
		goto IL_4E;
		IL_CC:
		this.ᜉ = (ushort)((int)(this.ᜉ & 255) + A_0 << 8);
		return;
		IL_E4:
		throw new NotSupportedException();
	}

	// Token: 0x06003B49 RID: 15177 RVA: 0x0021357C File Offset: 0x0021257C
	public bool ᜇ()
	{
		if (this.ᜉ())
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return false;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return (this.ᜉ & 4) != 0;
		}
		return false;
	}

	// Token: 0x06003B4A RID: 15178 RVA: 0x002135D4 File Offset: 0x002125D4
	public void ᜁ(bool A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				num = 2;
				continue;
			case 2:
				goto IL_64;
			}
			IL_1C:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_1C;
			default:
				if (false)
				{
				}
				if (!this.ᜉ())
				{
					goto IL_86;
				}
				num = 1;
				break;
			}
		}
		IL_64:
		this.ᜉ = (ushort)(A_0 ? ((int)(this.ᜉ | 4)) : ((int)this.ᜉ & -5));
		return;
		IL_86:
		throw new NotSupportedException();
	}

	// Token: 0x06003B4B RID: 15179 RVA: 0x0021366C File Offset: 0x0021266C
	public bool ᜆ()
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
		return (this.ᜈ & 1) == 1;
	}

	// Token: 0x06003B4C RID: 15180 RVA: 0x002136B4 File Offset: 0x002126B4
	public new void ᜀ(bool A_0)
	{
		if (true)
		{
		}
		if (A_0)
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
				this.ᜈ |= 1;
				return;
			}
		}
		this.ᜈ &= 254;
	}

	// Token: 0x06003B4D RID: 15181 RVA: 0x00213718 File Offset: 0x00212718
	public bool ᜏ()
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
		return (this.ᜈ & 2) == 2;
	}

	// Token: 0x06003B4E RID: 15182 RVA: 0x00213760 File Offset: 0x00212760
	public void ᜃ(bool A_0)
	{
		if (A_0)
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
				this.ᜈ |= 2;
				return;
			}
		}
		this.ᜈ &= 253;
	}

	// Token: 0x06003B4F RID: 15183 RVA: 0x002137C4 File Offset: 0x002127C4
	public bool ᜎ()
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
		return (this.ᜈ & 4) == 4;
	}

	// Token: 0x06003B50 RID: 15184 RVA: 0x0021380C File Offset: 0x0021280C
	public void ᜅ(bool A_0)
	{
		if (A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_43;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜈ |= 4;
			return;
		}
		IL_43:
		this.ᜈ &= 251;
	}

	// Token: 0x06003B51 RID: 15185 RVA: 0x00213870 File Offset: 0x00212870
	public bool ᜅ()
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
		return (this.ᜈ & 8) == 8;
	}

	// Token: 0x06003B52 RID: 15186 RVA: 0x002138B8 File Offset: 0x002128B8
	public void ᜆ(bool A_0)
	{
		if (A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_43;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜈ |= 8;
			return;
		}
		IL_43:
		this.ᜈ &= 247;
	}

	// Token: 0x06003B53 RID: 15187 RVA: 0x0021391C File Offset: 0x0021291C
	public bool ᜄ()
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
		return (this.ᜈ & 16) == 16;
	}

	// Token: 0x06003B54 RID: 15188 RVA: 0x00213964 File Offset: 0x00212964
	public void ᜇ(bool A_0)
	{
		if (A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3C;
			}
			if (false)
			{
			}
			this.ᜈ |= 16;
			return;
		}
		IL_3C:
		if (true)
		{
		}
		this.ᜈ &= 239;
	}

	// Token: 0x06003B55 RID: 15189 RVA: 0x002139C8 File Offset: 0x002129C8
	public bool \u170D()
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
		return (this.ᜈ & 32) == 32;
	}

	// Token: 0x06003B56 RID: 15190 RVA: 0x00213A10 File Offset: 0x00212A10
	public void ᜄ(bool A_0)
	{
		if (A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_44;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜈ |= 32;
			return;
		}
		IL_44:
		this.ᜈ &= 223;
	}

	// Token: 0x06003B57 RID: 15191 RVA: 0x00213A74 File Offset: 0x00212A74
	public bool ᜉ()
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
		return (this.ᜈ & 64) == 64;
	}

	// Token: 0x06003B58 RID: 15192 RVA: 0x00213ABC File Offset: 0x00212ABC
	public void ᜂ(bool A_0)
	{
		if (true)
		{
		}
		if (A_0)
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
				this.ᜈ |= 64;
				return;
			}
		}
		this.ᜈ &= 191;
	}

	// Token: 0x06003B59 RID: 15193 RVA: 0x00213B20 File Offset: 0x00212B20
	public override int ᜁ(ExcelVersion A_0)
	{
		if (this.ᜎ())
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return 4;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return 4 + this.ᜊ.Length * 2;
		}
		return 4;
	}

	// Token: 0x06003B5A RID: 15194 RVA: 0x00213B74 File Offset: 0x00212B74
	public override void ᜀ(FormulaUtil A_0, Stack<object> A_1, bool A_2)
	{
		int a_ = 13;
		int num = 7;
		object obj;
		for (;;)
		{
			string text;
			string str;
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				goto IL_9D;
			case 1:
				num = 6;
				continue;
			case 2:
				if (this.ᜇ())
				{
					num = 11;
					continue;
				}
				A_1.Push(this);
				num = 14;
				continue;
			case 3:
				if (this.ᜈ == 0)
				{
					num = 5;
					continue;
				}
				goto IL_1CB;
			case 4:
				if (obj != null)
				{
					num = 12;
					continue;
				}
				return;
			case 5:
				return;
			case 6:
				if (text[0] == ' ')
				{
					num = 8;
					continue;
				}
				goto IL_16A;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_138;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 8:
				text = A_1.Pop().ToString();
				num = 0;
				continue;
			case 9:
				if (text.EndsWith(RecordTableEnumerator.b("捂", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_16A;
			case 10:
				str = new string(' ', this.ᜊ());
				goto IL_138;
			case 11:
				obj = A_1.Pop();
				text = obj.ToString();
				num = 9;
				continue;
			case 12:
				goto IL_C6;
			case 13:
				goto IL_9D;
			case 14:
				goto IL_98;
			}
			if (this.ᜉ())
			{
				num = 10;
				continue;
			}
			num = 3;
			continue;
			IL_9D:
			A_1.Push(text + str);
			num = 4;
			continue;
			IL_138:
			num = 2;
			continue;
			IL_16A:
			obj = null;
			num = 13;
		}
		IL_98:
		return;
		IL_C6:
		A_1.Push(obj);
		return;
		IL_1CB:
		base.ᜀ(A_0, A_1, A_2);
	}

	// Token: 0x06003B5B RID: 15195 RVA: 0x00213D58 File Offset: 0x00212D58
	public override string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 19;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜎ())
				{
					num = 8;
					continue;
				}
				num = 5;
				continue;
			case 1:
				goto IL_B9;
			case 2:
				if (this.ᜈ == 0)
				{
					num = 11;
					continue;
				}
				goto IL_181;
			case 3:
				if (this.ᜅ())
				{
					num = 6;
					continue;
				}
				num = 0;
				continue;
			case 4:
				goto IL_76;
			case 5:
				if (this.ᜉ())
				{
					num = 4;
					continue;
				}
				num = 2;
				continue;
			case 6:
				goto IL_17F;
			case 8:
				goto IL_93;
			case 9:
				if (this.ᜏ())
				{
					num = 1;
					continue;
				}
				if (true)
				{
				}
				num = 3;
				continue;
			case 10:
				goto IL_59;
			case 11:
				goto IL_DC;
			}
			if (this.ᜄ())
			{
				num = 10;
			}
			else
			{
				num = 9;
			}
		}
		IL_59:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_129:
			return RecordTableEnumerator.b("่ъ᥌N", a_);
		default:
			if (false)
			{
			}
			return RecordTableEnumerator.b("ᩈṊL", a_);
		}
		IL_76:
		int count = this.ᜊ();
		return new string(' ', count);
		IL_93:
		return RecordTableEnumerator.b("ੈ͊ɌNɐᙒ", a_);
		IL_B9:
		return RecordTableEnumerator.b("Hൊ", a_);
		IL_DC:
		return string.Empty;
		IL_17F:
		goto IL_129;
		IL_181:
		return RecordTableEnumerator.b("慈歊㥌๎═❒❔睖㝘㑚⥜罞ࡠ๢ᕤ୦౨٪࡬Ůհᙲᅴ坶偸", a_);
	}

	// Token: 0x06003B5C RID: 15196 RVA: 0x00213EF4 File Offset: 0x00212EF4
	public override byte[] ᜀ(ExcelVersion A_0)
	{
		byte[] array;
		for (;;)
		{
			array = new byte[this.GetSize(A_0)];
			int num = 0;
			array[num++] = 25;
			array[num++] = this.ᜈ;
			Buffer.BlockCopy(BitConverter.GetBytes(this.ᜉ), 0, array, num, 2);
			num += 2;
			if (true)
			{
			}
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_A1;
				case 1:
					IL_6B:
					if (this.ᜎ())
					{
						num2 = 2;
						continue;
					}
					goto IL_A1;
				case 2:
					Buffer.BlockCopy(this.ᜊ, 0, array, num, this.GetSize(A_0) - num);
					num2 = 0;
					continue;
				}
				break;
				IL_A1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6B;
				default:
					goto IL_B7;
				}
			}
		}
		IL_B7:
		if (false)
		{
		}
		return array;
	}

	// Token: 0x06003B5D RID: 15197 RVA: 0x00213FC0 File Offset: 0x00212FC0
	public override void ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
	{
		switch (0)
		{
		default:
		{
			int num2;
			for (;;)
			{
				IL_3B:
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_161:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					num2 = A_1;
					this.TokenCode = FormulaToken.tAttr;
					this.ᜈ = A_0.ReadByte(A_1++);
					this.ᜉ = A_0.ReadUInt16(A_1);
					A_1 += 2;
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜎ())
						{
							num = 7;
							continue;
						}
						num = 4;
						continue;
					case 1:
						goto IL_16D;
					case 2:
						if (true)
						{
						}
						num = 3;
						continue;
					case 3:
						goto IL_154;
					case 4:
						goto IL_B9;
					case 5:
						goto IL_122;
					case 6:
						goto IL_122;
					case 7:
					{
						int num3 = (int)(this.ᜉ + 1);
						this.ᜊ = new ushort[num3];
						int num4 = 0;
						num = 6;
						continue;
					}
					case 8:
					{
						int num3;
						int num4;
						if (num4 >= num3)
						{
							num = 2;
							continue;
						}
						this.ᜊ[num4] = A_0.ReadUInt16(A_1);
						A_1 += 2;
						num4++;
						num = 5;
						continue;
					}
					}
					goto IL_3B;
					IL_122:
					num = 8;
				}
				IL_B9:
				base.ᜀ((this.ᜈ == 1) ? 0 : 1);
				goto IL_161;
			}
			IL_154:
			IL_16D:
			A_1 = num2 + this.GetSize(A_2) - 1;
			return;
		}
		}
	}

	// Token: 0x040019B8 RID: 6584
	public new const int ᜀ = 4;

	// Token: 0x040019B9 RID: 6585
	private new const int ᜁ = 2;

	// Token: 0x040019BA RID: 6586
	private const string ᜂ = "SUM";

	// Token: 0x040019BB RID: 6587
	private const string ᜃ = "IF";

	// Token: 0x040019BC RID: 6588
	private const string ᜄ = "GOTO";

	// Token: 0x040019BD RID: 6589
	private const string ᜅ = "CHOOSE";

	// Token: 0x040019BE RID: 6590
	private const string ᜆ = "( tAttr not implemented )";

	// Token: 0x040019BF RID: 6591
	private const ushort ᜇ = 4;

	// Token: 0x040019C0 RID: 6592
	private byte ᜈ;

	// Token: 0x040019C1 RID: 6593
	private ushort ᜉ;

	// Token: 0x040019C2 RID: 6594
	private ushort[] ᜊ;
}
