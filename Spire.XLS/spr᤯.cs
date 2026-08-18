using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x020002CB RID: 715
internal class spr\u192F : XlsObject, IInternalAddtionalFormat, IComparable, ICloneParent, IExtendIndex, IDisposable
{
	// Token: 0x06002B76 RID: 11126 RVA: 0x001838EC File Offset: 0x001828EC
	internal void ᜏ(bool A_0)
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
		this.ᜏ = A_0;
	}

	// Token: 0x06002B77 RID: 11127 RVA: 0x00183930 File Offset: 0x00182930
	public int \u173B()
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
			if (this.ᝀ())
			{
				return (int)this.ᜄ.\u171D();
			}
			break;
		}
		return (int)this.ᜊ().\u171D();
	}

	// Token: 0x06002B78 RID: 11128 RVA: 0x0018398C File Offset: 0x0018298C
	public void ᜂ(int A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_4F;
					}
				}
				IL_4F:
				if (false)
				{
				}
				this.ᜉ(true);
				this.ᜄ.ᜉ((ushort)A_0);
				this.ᜏ();
				if (true)
				{
				}
				num = 2;
				continue;
			case 2:
				return;
			}
			if (this.\u173B() == A_0)
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x06002B79 RID: 11129 RVA: 0x00183A1C File Offset: 0x00182A1C
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
		return this.ᜠ();
	}

	// Token: 0x06002B7A RID: 11130 RVA: 0x00183A60 File Offset: 0x00182A60
	public int ᝊ()
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
			if (this.\u173D())
			{
				return (int)this.ᜄ.ᜂ();
			}
			break;
		}
		if (true)
		{
		}
		return (int)this.ᜊ().ᜂ();
	}

	// Token: 0x06002B7B RID: 11131 RVA: 0x00183ABC File Offset: 0x00182ABC
	public void ᜀ(int A_0)
	{
		int a_ = 2;
		int num = 4;
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
				switch (num)
				{
				case 0:
					this.ᜃ(true);
					this.ᜄ.ᜈ((ushort)A_0);
					num = 1;
					continue;
				case 1:
					goto IL_8A;
				case 2:
					goto IL_6C;
				case 3:
					if (this.ᝊ() != A_0)
					{
						goto IL_AA;
					}
					goto IL_CB;
				}
				if (true)
				{
				}
				if (!this.ᜅ.InnerFormats.ᜀ(A_0))
				{
					num = 2;
					continue;
				}
				num = 3;
				continue;
			}
			IL_AA:
			num = 0;
		}
		IL_6C:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("洷吹圻倽⼿㕁⩃晅♇㽉⅋ⱍ㕏⁑瑓さ㝗⡙ㅛ㽝ᑟ䱡", a_));
		IL_8A:
		IL_CB:
		this.ᜏ();
	}

	// Token: 0x06002B7C RID: 11132 RVA: 0x00183B9C File Offset: 0x00182B9C
	public ExcelPatternType ᜤ()
	{
		if (!this.\u1753())
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_38;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			IL_38:
			return (ExcelPatternType)this.ᜊ().ᜧ();
		}
		return (ExcelPatternType)this.ᜄ.ᜧ();
	}

	// Token: 0x06002B7D RID: 11133 RVA: 0x00183BF8 File Offset: 0x00182BF8
	public void ᜀ(ExcelPatternType A_0)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_B5;
			case 1:
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B5;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					goto IL_3E;
				}
				break;
			case 3:
				this.\u170D(true);
				num = 0;
				continue;
			case 4:
				this.ᜂ((ExcelColors)65);
				this.ᜁ(ExcelColors.BlackCustom);
				num = 2;
				continue;
			}
			if (this.ᜤ() != A_0)
			{
				num = 3;
				continue;
			}
			break;
			IL_3E:
			this.ᜄ.ᜌ((ushort)A_0);
			this.ᜏ();
			num = 1;
			continue;
			IL_B5:
			if (A_0 != ExcelPatternType.None)
			{
				goto IL_3E;
			}
			num = 4;
		}
	}

	// Token: 0x06002B7E RID: 11134 RVA: 0x00183CCC File Offset: 0x00182CCC
	public ExcelColors \u1739()
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
		return this.ᜩ();
	}

	// Token: 0x06002B7F RID: 11135 RVA: 0x00183D10 File Offset: 0x00182D10
	public void ᜀ(ExcelColors A_0)
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
		this.ᜂ(A_0);
	}

	// Token: 0x06002B80 RID: 11136 RVA: 0x00183D54 File Offset: 0x00182D54
	public Color ᜨ()
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
		return this.ᜰ();
	}

	// Token: 0x06002B81 RID: 11137 RVA: 0x00183D98 File Offset: 0x00182D98
	public void ᜀ(Color A_0)
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
		this.ᜃ(A_0);
	}

	// Token: 0x06002B82 RID: 11138 RVA: 0x00183DDC File Offset: 0x00182DDC
	public ExcelColors \u1734()
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
		return this.ᝆ();
	}

	// Token: 0x06002B83 RID: 11139 RVA: 0x00183E20 File Offset: 0x00182E20
	public void ᜃ(ExcelColors A_0)
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
		this.ᜁ(A_0);
	}

	// Token: 0x06002B84 RID: 11140 RVA: 0x00183E64 File Offset: 0x00182E64
	public Color ᝍ()
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
		return this.\u1732();
	}

	// Token: 0x06002B85 RID: 11141 RVA: 0x00183EA8 File Offset: 0x00182EA8
	public void ᜁ(Color A_0)
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
		this.ᜂ(A_0);
	}

	// Token: 0x06002B86 RID: 11142 RVA: 0x00183EEC File Offset: 0x00182EEC
	public HorizontalAlignType ᜋ()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				num = 2;
				continue;
			case 1:
				goto IL_56;
			case 2:
				goto IL_6E;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			}
			if (!this.ᜦ())
			{
				num = 0;
			}
			else
			{
				num = 1;
			}
		}
		IL_56:
		sprỶ sprỶ = this.ᜄ;
		goto IL_80;
		IL_6E:
		sprỶ = this.ᜊ();
		IL_80:
		sprỶ sprỶ2 = sprỶ;
		return sprỶ2.ᜊ();
	}

	// Token: 0x06002B87 RID: 11143 RVA: 0x00183F80 File Offset: 0x00182F80
	public void ᜀ(HorizontalAlignType A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
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
					break;
				}
				break;
			case 1:
				return;
			case 2:
				this.ᜈ(true);
				this.ᜄ.ᜀ(A_0);
				this.ᜏ();
				num = 1;
				continue;
			}
			if (this.ᜋ() == A_0)
			{
				break;
			}
			num = 2;
		}
	}

	// Token: 0x06002B88 RID: 11144 RVA: 0x00184010 File Offset: 0x00183010
	public int \u171A()
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
		return (int)this.ᜄ.ᜏ();
	}

	// Token: 0x06002B89 RID: 11145 RVA: 0x00184058 File Offset: 0x00183058
	public void ᜁ(int A_0)
	{
		int a_ = 13;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				if (A_0 > this.ᜅ.MaxIndent)
				{
					num = 2;
					continue;
				}
				this.ᜈ(true);
				this.ᜄ.ᜀ((byte)A_0);
				num = 3;
				continue;
			case 2:
				goto IL_E6;
			case 3:
				if (A_0 != 0)
				{
					num = 4;
					continue;
				}
				goto IL_E8;
			case 4:
				this.ᜄ.ᜁ(0);
				num = 7;
				continue;
			case 6:
				return;
			case 7:
				goto IL_E8;
			}
			if (true)
			{
			}
			if (this.\u171A() == A_0)
			{
				return;
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
			IL_EE:
			num = 6;
			continue;
			IL_E8:
			this.ᜏ();
			goto IL_EE;
		}
		IL_E6:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ੂ⭄⍆ⱈ╊㥌͎㑐╒ご㭖", a_));
	}

	// Token: 0x06002B8A RID: 11146 RVA: 0x00184174 File Offset: 0x00183174
	public bool \u1755()
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
		return this.ᜄ.ᜄ();
	}

	// Token: 0x06002B8B RID: 11147 RVA: 0x001841BC File Offset: 0x001831BC
	public void ᜆ(bool A_0)
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
		this.ᜄ.ᜂ(A_0);
		this.ᜏ();
	}

	// Token: 0x06002B8C RID: 11148 RVA: 0x0018420C File Offset: 0x0018320C
	public bool ᝎ()
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
		return this.ᜄ.\u171F();
	}

	// Token: 0x06002B8D RID: 11149 RVA: 0x00184254 File Offset: 0x00183254
	public void ᜁ(bool A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				if (true)
				{
				}
				this.ᜋ(true);
				this.ᜄ.ᜎ(A_0);
				this.ᜏ();
				num = 2;
				continue;
			case 2:
				return;
			}
			if (this.ᝎ() == A_0)
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x06002B8E RID: 11150 RVA: 0x001842E4 File Offset: 0x001832E4
	public bool ᜱ()
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
		return this.ᜄ.ᜡ();
	}

	// Token: 0x06002B8F RID: 11151 RVA: 0x0018432C File Offset: 0x0018332C
	public void ᜂ(bool A_0)
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
		this.ᜄ.ᜄ(A_0);
		this.ᜏ();
	}

	// Token: 0x06002B90 RID: 11152 RVA: 0x0018437C File Offset: 0x0018337C
	public string \u1715()
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
		return this.ᜅ.InnerFormats.ᜁ(this.ᝊ()).ᜂ();
	}

	// Token: 0x06002B91 RID: 11153 RVA: 0x001843D4 File Offset: 0x001833D4
	public void ᜁ(string A_0)
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
		this.ᜀ((int)((ushort)this.ᜅ.InnerFormats.ᜉ(A_0)));
		this.ᜏ();
	}

	// Token: 0x06002B92 RID: 11154 RVA: 0x00184430 File Offset: 0x00183430
	public string \u1737()
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
		return this.\u1715();
	}

	// Token: 0x06002B93 RID: 11155 RVA: 0x00184474 File Offset: 0x00183474
	public void ᜀ(string A_0)
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
		this.ᜁ(A_0);
	}

	// Token: 0x06002B94 RID: 11156 RVA: 0x001844B8 File Offset: 0x001834B8
	public INumberFormat ᝁ()
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
		return this.ᜅ.InnerFormats.ᜁ(this.ᝊ());
	}

	// Token: 0x06002B95 RID: 11157 RVA: 0x0018450C File Offset: 0x0018350C
	public bool ᝏ()
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
		return this.ᜄ.ᜉ();
	}

	// Token: 0x06002B96 RID: 11158 RVA: 0x00184554 File Offset: 0x00183554
	public void ᜇ(bool A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
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
					break;
				}
				break;
			case 1:
				this.ᜈ(true);
				this.ᜄ.ᜌ(A_0);
				this.ᜏ();
				num = 2;
				continue;
			case 2:
				return;
			}
			if (A_0 == this.ᝏ())
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x06002B97 RID: 11159 RVA: 0x001845E4 File Offset: 0x001835E4
	public bool \u1733()
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
		return this.ᜄ.ᜦ();
	}

	// Token: 0x06002B98 RID: 11160 RVA: 0x0018462C File Offset: 0x0018362C
	public void ᜅ(bool A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				return;
			case 2:
				this.ᜈ(true);
				this.ᜄ.ᜈ(A_0);
				this.ᜏ();
				num = 1;
				continue;
			}
			if (true)
			{
			}
			if (this.\u1733() == A_0)
			{
				break;
			}
			num = 2;
		}
	}

	// Token: 0x06002B99 RID: 11161 RVA: 0x001846BC File Offset: 0x001836BC
	public VerticalAlignType \u171D()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6E;
			case 1:
				goto IL_5E;
			case 2:
				num = 0;
				continue;
			case 3:
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
					break;
				}
				break;
			}
			if (!this.ᜦ())
			{
				num = 2;
			}
			else
			{
				num = 1;
			}
		}
		IL_5E:
		sprỶ sprỶ = this.ᜄ;
		goto IL_80;
		IL_6E:
		sprỶ = this.ᜊ();
		IL_80:
		sprỶ sprỶ2 = sprỶ;
		return sprỶ2.\u171A();
	}

	// Token: 0x06002B9A RID: 11162 RVA: 0x00184750 File Offset: 0x00183750
	public void ᜀ(VerticalAlignType A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜈ(true);
				this.ᜄ.ᜀ(A_0);
				this.ᜏ();
				num = 1;
				continue;
			case 1:
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			}
			if (true)
			{
			}
			if (this.\u171D() == A_0)
			{
				break;
			}
			num = 0;
		}
	}

	// Token: 0x06002B9B RID: 11163 RVA: 0x001847E0 File Offset: 0x001837E0
	public bool ᜦ()
	{
		if (true)
		{
		}
		bool flag = this.ᜄ.ᜆ();
		if (!this.ᝇ())
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
				break;
			}
			return !flag;
		}
		return flag;
	}

	// Token: 0x06002B9C RID: 11164 RVA: 0x00184838 File Offset: 0x00183838
	public void ᜈ(bool A_0)
	{
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 5;
				continue;
			case 1:
				goto IL_EA;
			case 2:
			{
				sprỶ a_ = this.ᜊ();
				this.ᜄ.ᜄ(a_);
				num = 3;
				continue;
			}
			case 3:
				goto IL_12B;
			case 4:
				num = 7;
				continue;
			case 5:
				if (A_0)
				{
					num = 4;
					continue;
				}
				goto IL_69;
			case 6:
				num = 9;
				continue;
			case 7:
				if (!this.ᜅ.Loading)
				{
					num = 2;
					continue;
				}
				goto IL_69;
			case 9:
				if (this.ᜦ() != A_0)
				{
					num = 0;
					continue;
				}
				return;
			}
			if (this.ᝇ())
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12B;
				default:
					if (false)
					{
					}
					num = 6;
					break;
				}
			}
			else
			{
				this.ᜄ.\u170D(!A_0);
				this.ᜏ();
				num = 1;
			}
		}
		IL_69:
		if (true)
		{
		}
		this.ᜄ.\u170D(A_0);
		this.ᜏ();
		return;
		IL_EA:
		return;
		IL_12B:
		goto IL_69;
	}

	// Token: 0x06002B9D RID: 11165 RVA: 0x00184980 File Offset: 0x00183980
	public bool \u1719()
	{
		bool flag = this.ᜄ.ᜥ();
		if (!this.ᝇ())
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
			IL_3C:
			return !flag;
		}
		if (true)
		{
		}
		return flag;
	}

	// Token: 0x06002B9E RID: 11166 RVA: 0x001849D8 File Offset: 0x001839D8
	public void ᜊ(bool A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_120;
			case 1:
				goto IL_E4;
			case 3:
				if (this.\u1719() != A_0)
				{
					num = 4;
					continue;
				}
				return;
			case 4:
				num = 7;
				continue;
			case 5:
			{
				spr\u192F a_ = this.ᜉ();
				this.ᜁ(a_);
				num = 0;
				continue;
			}
			case 6:
				num = 9;
				continue;
			case 7:
				if (A_0)
				{
					num = 6;
					continue;
				}
				goto IL_6E;
			case 8:
				num = 3;
				continue;
			case 9:
				if (!this.ᜅ.Loading)
				{
					num = 5;
					continue;
				}
				goto IL_6E;
			}
			if (true)
			{
			}
			if (this.ᝇ())
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_120;
				default:
					if (false)
					{
					}
					num = 8;
					break;
				}
			}
			else
			{
				this.ᜄ.ᜆ(!A_0);
				this.ᜏ();
				num = 1;
			}
		}
		IL_6E:
		this.ᜄ.ᜆ(A_0);
		this.ᜏ();
		return;
		IL_E4:
		return;
		IL_120:
		goto IL_6E;
	}

	// Token: 0x06002B9F RID: 11167 RVA: 0x00184B14 File Offset: 0x00183B14
	public bool ᝀ()
	{
		bool flag = this.ᜄ.ᜃ();
		if (!this.ᝇ())
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_44;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			IL_44:
			return !flag;
		}
		return flag;
	}

	// Token: 0x06002BA0 RID: 11168 RVA: 0x00184B6C File Offset: 0x00183B6C
	public void ᜉ(bool A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᝀ() != A_0)
				{
					if (true)
					{
					}
					num = 5;
					continue;
				}
				return;
			case 1:
				num = 3;
				continue;
			case 3:
				if (!this.ᜅ.Loading)
				{
					num = 8;
					continue;
				}
				goto IL_69;
			case 4:
				if (A_0)
				{
					num = 1;
					continue;
				}
				goto IL_69;
			case 5:
				num = 4;
				continue;
			case 6:
				goto IL_EA;
			case 7:
				goto IL_12E;
			case 8:
				this.ᜄ.ᜉ(this.ᜊ().\u171D());
				num = 7;
				continue;
			case 9:
				num = 0;
				continue;
			}
			if (this.ᝇ())
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12E;
				default:
					if (false)
					{
					}
					num = 9;
					break;
				}
			}
			else
			{
				this.ᜄ.ᜃ(!A_0);
				this.ᜏ();
				num = 6;
			}
		}
		IL_69:
		this.ᜄ.ᜃ(A_0);
		this.ᜏ();
		return;
		IL_EA:
		return;
		IL_12E:
		goto IL_69;
	}

	// Token: 0x06002BA1 RID: 11169 RVA: 0x00184CB8 File Offset: 0x00183CB8
	public bool \u173D()
	{
		bool flag = this.ᜄ.\u1715();
		if (!this.ᝇ())
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
			IL_44:
			return !flag;
		}
		return flag;
	}

	// Token: 0x06002BA2 RID: 11170 RVA: 0x00184D10 File Offset: 0x00183D10
	public void ᜃ(bool A_0)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_12E;
			case 1:
				if (this.\u173D() != A_0)
				{
					num = 3;
					continue;
				}
				return;
			case 2:
				num = 1;
				continue;
			case 3:
				num = 9;
				continue;
			case 4:
				if (true)
				{
				}
				this.ᜄ.ᜈ(this.ᜊ().ᜂ());
				num = 0;
				continue;
			case 6:
				num = 8;
				continue;
			case 7:
				goto IL_EA;
			case 8:
				if (!this.ᜅ.Loading)
				{
					num = 4;
					continue;
				}
				goto IL_69;
			case 9:
				if (A_0)
				{
					num = 6;
					continue;
				}
				goto IL_69;
			}
			if (this.ᝇ())
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12E;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
			else
			{
				this.ᜄ.ᜁ(!A_0);
				this.ᜏ();
				num = 7;
			}
		}
		IL_69:
		this.ᜄ.ᜁ(A_0);
		this.ᜏ();
		return;
		IL_EA:
		return;
		IL_12E:
		goto IL_69;
	}

	// Token: 0x06002BA3 RID: 11171 RVA: 0x00184E5C File Offset: 0x00183E5C
	public bool \u1753()
	{
		bool flag = this.ᜄ.ᜇ();
		if (!this.ᝇ())
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_44;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			IL_44:
			return !flag;
		}
		return flag;
	}

	// Token: 0x06002BA4 RID: 11172 RVA: 0x00184EB4 File Offset: 0x00183EB4
	public void \u170D(bool A_0)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				num = 3;
				continue;
			case 1:
				num = 4;
				continue;
			case 2:
				goto IL_EA;
			case 3:
				if (!this.ᜅ.Loading)
				{
					num = 8;
					continue;
				}
				goto IL_69;
			case 4:
				if (A_0)
				{
					num = 0;
					continue;
				}
				goto IL_69;
			case 6:
				num = 9;
				continue;
			case 7:
				goto IL_126;
			case 8:
			{
				spr\u192F a_ = this.ᜉ();
				this.ᜀ(a_);
				num = 7;
				continue;
			}
			case 9:
				if (this.\u1753() != A_0)
				{
					num = 1;
					continue;
				}
				return;
			}
			if (this.ᝇ())
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_126;
				default:
					if (false)
					{
					}
					num = 6;
					break;
				}
			}
			else
			{
				this.ᜄ.ᜋ(!A_0);
				this.ᜏ();
				num = 2;
			}
		}
		IL_69:
		this.ᜄ.ᜋ(A_0);
		this.ᜏ();
		return;
		IL_EA:
		return;
		IL_126:
		goto IL_69;
	}

	// Token: 0x06002BA5 RID: 11173 RVA: 0x00184FF8 File Offset: 0x00183FF8
	public bool \u1717()
	{
		bool flag;
		for (;;)
		{
			flag = this.ᜄ.\u1716();
			if (this.ᝇ())
			{
				return flag;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_34;
			}
		}
		IL_34:
		if (false)
		{
		}
		return !flag;
	}

	// Token: 0x06002BA6 RID: 11174 RVA: 0x00185050 File Offset: 0x00184050
	public void ᜋ(bool A_0)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 1:
				if (this.\u1717() != A_0)
				{
					num = 0;
					continue;
				}
				return;
			case 2:
			{
				if (true)
				{
				}
				sprỶ a_ = this.ᜊ();
				this.ᜄ.ᜅ(a_);
				num = 6;
				continue;
			}
			case 3:
				goto IL_48;
			case 4:
				if (A_0)
				{
					num = 8;
					continue;
				}
				goto IL_4A;
			case 6:
				goto IL_132;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_48;
				default:
					goto IL_EB;
				}
				break;
			case 8:
				num = 9;
				continue;
			case 9:
				if (!this.ᜅ.Loading)
				{
					num = 2;
					continue;
				}
				goto IL_4A;
			}
			if (this.ᝇ())
			{
				num = 3;
				continue;
			}
			this.ᜄ.ᜅ(!A_0);
			this.ᜏ();
			num = 7;
			continue;
			IL_48:
			num = 1;
		}
		IL_4A:
		this.ᜄ.ᜅ(A_0);
		this.ᜏ();
		return;
		IL_EB:
		if (false)
		{
		}
		return;
		IL_132:
		goto IL_4A;
	}

	// Token: 0x06002BA7 RID: 11175 RVA: 0x00185194 File Offset: 0x00184194
	public virtual IFont ᜀ()
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
		return this.ᜅ.InnerFonts[this.\u173B()];
	}

	// Token: 0x06002BA8 RID: 11176 RVA: 0x001851E8 File Offset: 0x001841E8
	public IBorders ᜪ()
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
		return new XlsBordersCollection(base.ReservedHandle, this.ᜅ, this);
	}

	// Token: 0x06002BA9 RID: 11177 RVA: 0x00185238 File Offset: 0x00184238
	public bool \u1713()
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
		return this.ᜄ.ᜫ();
	}

	// Token: 0x06002BAA RID: 11178 RVA: 0x00185280 File Offset: 0x00184280
	public void ᜌ(bool A_0)
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
		this.ᜄ.ᜇ(A_0);
		this.ᜏ();
	}

	// Token: 0x06002BAB RID: 11179 RVA: 0x001852D0 File Offset: 0x001842D0
	public ExcelColors ᝆ()
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
		return this.\u1754().ᜂ(this.ᜅ);
	}

	// Token: 0x06002BAC RID: 11180 RVA: 0x0018531C File Offset: 0x0018431C
	public void ᜁ(ExcelColors A_0)
	{
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
					break;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					this.\u170D(true);
					this.\u1754().SetKnownColor(A_0);
					sprỶ sprỶ = this.ᜄ;
					sprỶ.ᜌ(sprỶ.ᜧ() | 1);
					this.ᜏ();
					num = 2;
					continue;
				}
				}
				break;
			case 2:
				return;
			}
			if (A_0 == this.ᝆ())
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x06002BAD RID: 11181 RVA: 0x001853C0 File Offset: 0x001843C0
	public Color \u1732()
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
		return this.\u1754().ᜁ(this.ᜅ);
	}

	// Token: 0x06002BAE RID: 11182 RVA: 0x0018540C File Offset: 0x0018440C
	public void ᜂ(Color A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_63:
				if (this.ᜄ.ᜧ() != 0)
				{
					goto IL_9B;
				}
				num = 2;
				break;
			default:
				if (false)
				{
				}
				this.\u170D(true);
				this.\u1754().ᜀ(A_0, this.ᜅ);
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_99;
				case 1:
					goto IL_63;
				case 2:
				{
					sprỶ sprỶ = this.ᜄ;
					sprỶ.ᜌ(sprỶ.ᜧ() | 1);
					num = 0;
					continue;
				}
				}
				break;
			}
		}
		IL_99:
		IL_9B:
		this.ᜏ();
	}

	// Token: 0x06002BAF RID: 11183 RVA: 0x001854BC File Offset: 0x001844BC
	public OColor \u1754()
	{
		if (true)
		{
		}
		int num = 2;
		for (;;)
		{
			IL_12:
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				goto IL_73;
			case 3:
				goto IL_5E;
			}
			while (this.\u1753())
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
					num = 3;
					goto IL_12;
				}
			}
			num = 0;
		}
		IL_5E:
		spr\u192F spr_u192F = this;
		goto IL_7B;
		IL_73:
		spr_u192F = this.ᜉ();
		IL_7B:
		spr\u192F spr_u192F2 = spr_u192F;
		return spr_u192F2.ᜐ();
	}

	// Token: 0x06002BB0 RID: 11184 RVA: 0x0018554C File Offset: 0x0018454C
	public ExcelColors ᜩ()
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
		return this.ᝄ().ᜂ(this.ᜅ);
	}

	// Token: 0x06002BB1 RID: 11185 RVA: 0x00185598 File Offset: 0x00184598
	public void ᜂ(ExcelColors A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A5;
				default:
					if (false)
					{
					}
					this.\u170D(true);
					this.ᝄ().ᜀ(A_0, true, this.ᜅ);
					num = 5;
					continue;
				}
				break;
			case 2:
				goto IL_5A;
			case 3:
				this.ᜄ.ᜌ(1);
				num = 2;
				continue;
			case 4:
				return;
			case 5:
				goto IL_A5;
			}
			if (A_0 != this.ᜩ())
			{
				num = 0;
				continue;
			}
			break;
			IL_5A:
			this.ᜏ();
			num = 4;
			continue;
			IL_A5:
			if (true)
			{
			}
			if (this.ᜄ.ᜧ() != 0)
			{
				goto IL_5A;
			}
			num = 3;
		}
	}

	// Token: 0x06002BB2 RID: 11186 RVA: 0x00185678 File Offset: 0x00184678
	public Color ᜰ()
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
		return this.ᝄ().ᜁ(this.ᜅ);
	}

	// Token: 0x06002BB3 RID: 11187 RVA: 0x001856C4 File Offset: 0x001846C4
	public void ᜃ(Color A_0)
	{
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5B:
				if (true)
				{
				}
				if (this.ᜄ.ᜧ() != 0)
				{
					goto IL_93;
				}
				num = 0;
				break;
			default:
				if (false)
				{
				}
				this.\u170D(true);
				this.ᝄ().ᜀ(A_0, this.ᜅ);
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜄ.ᜌ(1);
					num = 2;
					continue;
				case 1:
					goto IL_5B;
				case 2:
					goto IL_91;
				}
				break;
			}
		}
		IL_91:
		IL_93:
		this.ᜏ();
	}

	// Token: 0x06002BB4 RID: 11188 RVA: 0x0018576C File Offset: 0x0018476C
	public OColor ᝄ()
	{
		int num = 2;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 0:
				goto IL_73;
			case 1:
				goto IL_56;
			case 3:
				num = 0;
				continue;
			}
			while (this.\u1753())
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
					num = 1;
					goto IL_0A;
				}
			}
			num = 3;
		}
		IL_56:
		if (true)
		{
		}
		spr\u192F spr_u192F = this;
		goto IL_7B;
		IL_73:
		spr_u192F = this.ᜉ();
		IL_7B:
		spr\u192F spr_u192F2 = spr_u192F;
		return spr_u192F2.ᝋ();
	}

	// Token: 0x06002BB5 RID: 11189 RVA: 0x001857FC File Offset: 0x001847FC
	public bool \u173A()
	{
		bool result;
		for (;;)
		{
			if (true)
			{
			}
			result = false;
			int num = 11;
			for (;;)
			{
				bool flag;
				switch (num)
				{
				case 0:
					if (!this.\u1719())
					{
						num = 2;
						continue;
					}
					goto IL_112;
				case 1:
					flag = this.\u1717();
					goto IL_E4;
				case 2:
					goto IL_143;
				case 3:
					if (!this.ᜦ())
					{
						num = 10;
						continue;
					}
					goto IL_112;
				case 4:
					flag = true;
					goto IL_E4;
				case 5:
					num = 3;
					continue;
				case 6:
					num = 14;
					continue;
				case 7:
					num = 9;
					continue;
				case 8:
					if (!this.ᝀ())
					{
						num = 6;
						continue;
					}
					goto IL_112;
				case 9:
					if (this.\u1753())
					{
						goto IL_112;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_143;
					default:
						if (false)
						{
						}
						num = 13;
						continue;
					}
					break;
				case 10:
					num = 0;
					continue;
				case 11:
					if (this.ᝇ())
					{
						num = 5;
						continue;
					}
					return result;
				case 12:
					return result;
				case 13:
					num = 1;
					continue;
				case 14:
					if (!this.\u173D())
					{
						num = 7;
						continue;
					}
					goto IL_112;
				}
				break;
				IL_E4:
				result = flag;
				num = 12;
				continue;
				IL_112:
				num = 4;
				continue;
				IL_143:
				num = 8;
			}
		}
		return result;
	}

	// Token: 0x06002BB6 RID: 11190 RVA: 0x00185970 File Offset: 0x00184970
	private bool ᜂ(spr\u192F A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x06002BB7 RID: 11191 RVA: 0x001859B0 File Offset: 0x001849B0
	public ReadingOrderType \u171C()
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
		return (ReadingOrderType)this.ᜄ.\u171E();
	}

	// Token: 0x06002BB8 RID: 11192 RVA: 0x001859F8 File Offset: 0x001849F8
	public void ᜀ(ReadingOrderType A_0)
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
		this.ᜄ.ᜋ((ushort)A_0);
		this.ᜏ();
	}

	// Token: 0x06002BB9 RID: 11193 RVA: 0x00185A48 File Offset: 0x00184A48
	public int \u171B()
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
		return (int)this.ᜄ.ᜣ();
	}

	// Token: 0x06002BBA RID: 11194 RVA: 0x00185A90 File Offset: 0x00184A90
	public void ᜅ(int A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A7;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					this.ᜈ(true);
					this.ᜄ.ᜁ((ushort)A_0);
					num = 1;
					continue;
				}
				break;
			case 1:
				goto IL_A7;
			case 2:
				goto IL_5A;
			case 4:
				this.ᜄ.ᜀ(0);
				num = 2;
				continue;
			case 5:
				return;
			}
			if (A_0 != this.\u171B())
			{
				num = 0;
				continue;
			}
			break;
			IL_5A:
			this.ᜏ();
			num = 5;
			continue;
			IL_A7:
			if (A_0 == 0)
			{
				goto IL_5A;
			}
			num = 4;
		}
	}

	// Token: 0x06002BBB RID: 11195 RVA: 0x00185B60 File Offset: 0x00184B60
	internal sprỶ.TXFType \u171E()
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
		return this.ᜄ.ᜎ();
	}

	// Token: 0x06002BBC RID: 11196 RVA: 0x00185BA8 File Offset: 0x00184BA8
	internal void ᜀ(sprỶ.TXFType A_0)
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
		this.ᜄ.ᜀ(A_0);
		this.ᜏ();
	}

	// Token: 0x06002BBD RID: 11197 RVA: 0x00185BF8 File Offset: 0x00184BF8
	internal IGradient ᝐ()
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
		return this.ᜇ;
	}

	// Token: 0x06002BBE RID: 11198 RVA: 0x00185C3C File Offset: 0x00184C3C
	internal void ᜀ(IGradient A_0)
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
		this.ᜇ = (XlsShapeFill)A_0;
	}

	// Token: 0x06002BBF RID: 11199 RVA: 0x00185C84 File Offset: 0x00184C84
	internal int ᜠ()
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
		return this.ᜆ;
	}

	// Token: 0x06002BC0 RID: 11200 RVA: 0x00185CC8 File Offset: 0x00184CC8
	internal void ᜃ(int A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x06002BC1 RID: 11201 RVA: 0x00185D0C File Offset: 0x00184D0C
	internal sprỶ ᜑ()
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
		return this.ᜄ;
	}

	// Token: 0x06002BC2 RID: 11202 RVA: 0x00185D50 File Offset: 0x00184D50
	internal void ᜀ(sprỶ A_0)
	{
		while (A_0 == null)
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
				throw new ArgumentNullException();
			}
		}
		this.ᜄ = A_0;
	}

	// Token: 0x06002BC3 RID: 11203 RVA: 0x00185DA0 File Offset: 0x00184DA0
	internal int ᜯ()
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
		return (int)this.ᜄ.\u1713();
	}

	// Token: 0x06002BC4 RID: 11204 RVA: 0x00185DE8 File Offset: 0x00184DE8
	internal void ᜄ(int A_0)
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
		this.ᜄ.ᜇ((ushort)A_0);
	}

	// Token: 0x06002BC5 RID: 11205 RVA: 0x00185E30 File Offset: 0x00184E30
	public XlsWorkbook ᜎ()
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
		return this.ᜅ;
	}

	// Token: 0x06002BC6 RID: 11206 RVA: 0x00185E74 File Offset: 0x00184E74
	protected internal sprᢖ \u1714()
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
		return this.ᜅ.InnerExtFormats;
	}

	// Token: 0x06002BC7 RID: 11207 RVA: 0x00185EBC File Offset: 0x00184EBC
	public OColor ᜡ()
	{
		int num = 1;
		for (;;)
		{
			IL_12:
			switch (num)
			{
			case 0:
				goto IL_73;
			case 1:
				if (true)
				{
				}
				break;
			case 2:
				num = 0;
				continue;
			case 3:
				goto IL_5E;
			}
			while (this.\u1719())
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
					num = 3;
					goto IL_12;
				}
			}
			num = 2;
		}
		IL_5E:
		spr\u192F spr_u192F = this;
		goto IL_7B;
		IL_73:
		spr_u192F = this.ᜉ();
		IL_7B:
		spr\u192F spr_u192F2 = spr_u192F;
		return spr_u192F2.\u1752();
	}

	// Token: 0x06002BC8 RID: 11208 RVA: 0x00185F4C File Offset: 0x00184F4C
	public OColor \u173F()
	{
		int num = 2;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 1:
				goto IL_73;
			case 3:
				num = 1;
				continue;
			}
			while (this.\u1719())
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
					num = 0;
					goto IL_0A;
				}
			}
			if (true)
			{
			}
			num = 3;
		}
		IL_5E:
		spr\u192F spr_u192F = this;
		goto IL_7B;
		IL_73:
		spr_u192F = this.ᜉ();
		IL_7B:
		spr\u192F spr_u192F2 = spr_u192F;
		return spr_u192F2.ᝈ();
	}

	// Token: 0x06002BC9 RID: 11209 RVA: 0x00185FDC File Offset: 0x00184FDC
	public OColor ᝅ()
	{
		if (true)
		{
		}
		int num = 2;
		for (;;)
		{
			IL_12:
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				goto IL_5E;
			case 3:
				goto IL_73;
			}
			while (this.\u1719())
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
					num = 1;
					goto IL_12;
				}
			}
			num = 0;
		}
		IL_5E:
		spr\u192F spr_u192F = this;
		goto IL_7B;
		IL_73:
		spr_u192F = this.ᜉ();
		IL_7B:
		spr\u192F spr_u192F2 = spr_u192F;
		return spr_u192F2.ᝌ();
	}

	// Token: 0x06002BCA RID: 11210 RVA: 0x0018606C File Offset: 0x0018506C
	public OColor \u1756()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_5E;
			case 2:
				goto IL_73;
			case 3:
				num = 2;
				continue;
			}
			for (;;)
			{
				if (true)
				{
				}
				if (!this.\u1719())
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_50;
				}
			}
			num = 3;
			continue;
			IL_50:
			if (false)
			{
			}
			num = 1;
		}
		IL_5E:
		spr\u192F spr_u192F = this;
		goto IL_7B;
		IL_73:
		spr_u192F = this.ᜉ();
		IL_7B:
		spr\u192F spr_u192F2 = spr_u192F;
		return spr_u192F2.\u1712();
	}

	// Token: 0x06002BCB RID: 11211 RVA: 0x001860FC File Offset: 0x001850FC
	public OColor \u171F()
	{
		int num = 1;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 2:
				goto IL_73;
			case 3:
				num = 2;
				continue;
			}
			while (this.\u1719())
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
					num = 0;
					goto IL_0A;
				}
			}
			num = 3;
		}
		IL_5E:
		spr\u192F spr_u192F = this;
		goto IL_7B;
		IL_73:
		spr_u192F = this.ᜉ();
		IL_7B:
		spr\u192F spr_u192F2 = spr_u192F;
		return spr_u192F2.\u1757();
	}

	// Token: 0x06002BCC RID: 11212 RVA: 0x0018618C File Offset: 0x0018518C
	public LineStyleType ᝉ()
	{
		int num = 1;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 0:
				goto IL_78;
			case 2:
				if (true)
				{
				}
				num = 0;
				continue;
			case 3:
				goto IL_5E;
			}
			while (this.\u1719())
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
					num = 3;
					goto IL_0A;
				}
			}
			num = 2;
		}
		IL_5E:
		sprỶ sprỶ = this.ᜄ;
		goto IL_80;
		IL_78:
		sprỶ = this.ᜊ();
		IL_80:
		sprỶ sprỶ2 = sprỶ;
		return sprỶ2.ᜈ();
	}

	// Token: 0x06002BCD RID: 11213 RVA: 0x00186220 File Offset: 0x00185220
	public void ᜀ(LineStyleType A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					this.ᜊ(true);
					this.ᜄ.ᜀ(this.ᜅ);
					this.ᜄ.ᜃ(A_0);
					this.ᝅ().ᜁ();
					this.ᜏ();
					num = 2;
					continue;
				}
				break;
			case 2:
				return;
			}
			if (true)
			{
			}
			if (this.ᝉ() == A_0)
			{
				break;
			}
			num = 0;
		}
	}

	// Token: 0x06002BCE RID: 11214 RVA: 0x001862CC File Offset: 0x001852CC
	public LineStyleType ᜫ()
	{
		int num = 2;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 0:
				goto IL_56;
			case 1:
				num = 3;
				continue;
			case 3:
				goto IL_66;
			}
			while (this.\u1719())
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
					num = 0;
					goto IL_0A;
				}
			}
			num = 1;
		}
		IL_56:
		sprỶ sprỶ = this.ᜄ;
		goto IL_80;
		IL_66:
		if (true)
		{
		}
		sprỶ = this.ᜊ();
		IL_80:
		sprỶ sprỶ2 = sprỶ;
		return sprỶ2.ᜭ();
	}

	// Token: 0x06002BCF RID: 11215 RVA: 0x00186360 File Offset: 0x00185360
	public void ᜂ(LineStyleType A_0)
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
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					this.ᜊ(true);
					this.ᜄ.ᜀ(this.ᜅ);
					this.ᜄ.ᜂ(A_0);
					this.\u1756().ᜁ();
					this.ᜏ();
					num = 1;
					continue;
				}
				break;
			}
			if (this.ᜫ() == A_0)
			{
				break;
			}
			num = 2;
		}
	}

	// Token: 0x06002BD0 RID: 11216 RVA: 0x0018640C File Offset: 0x0018540C
	public LineStyleType \u1738()
	{
		if (true)
		{
		}
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_72;
				}
				break;
			case 1:
				num = 0;
				continue;
			case 3:
				goto IL_4C;
			}
			IL_32:
			if (!this.\u1719())
			{
				num = 1;
				continue;
			}
			num = 3;
			continue;
			goto IL_32;
		}
		IL_4C:
		sprỶ sprỶ = this.ᜄ;
		goto IL_80;
		IL_72:
		if (false)
		{
		}
		sprỶ = this.ᜊ();
		IL_80:
		sprỶ sprỶ2 = sprỶ;
		return sprỶ2.ᜐ();
	}

	// Token: 0x06002BD1 RID: 11217 RVA: 0x001864A0 File Offset: 0x001854A0
	public void ᜄ(LineStyleType A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				this.ᜊ(true);
				this.ᜄ.ᜀ(this.ᜅ);
				this.ᜄ.ᜁ(A_0);
				this.\u173F().ᜁ();
				this.ᜏ();
				num = 2;
				continue;
			case 1:
				IL_08:
				break;
			case 2:
				goto IL_80;
			}
			if (this.\u1738() != A_0)
			{
				num = 0;
				continue;
			}
			IL_80:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_08;
			default:
				goto IL_96;
			}
		}
		IL_96:
		if (false)
		{
		}
	}

	// Token: 0x06002BD2 RID: 11218 RVA: 0x0018654C File Offset: 0x0018554C
	public LineStyleType \u170D()
	{
		int num = 3;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 1:
				goto IL_4C;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_72;
				}
				break;
			}
			IL_32:
			if (!this.\u1719())
			{
				num = 0;
				continue;
			}
			num = 1;
			continue;
			goto IL_32;
		}
		IL_4C:
		sprỶ sprỶ = this.ᜄ;
		goto IL_80;
		IL_72:
		if (false)
		{
		}
		sprỶ = this.ᜊ();
		IL_80:
		sprỶ sprỶ2 = sprỶ;
		return sprỶ2.ᜋ();
	}

	// Token: 0x06002BD3 RID: 11219 RVA: 0x001865E0 File Offset: 0x001855E0
	public void ᜅ(LineStyleType A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				IL_08:
				break;
			case 1:
				if (true)
				{
				}
				this.ᜊ(true);
				this.ᜄ.ᜀ(this.ᜅ);
				this.ᜄ.ᜀ(A_0);
				this.ᜡ().ᜁ();
				this.ᜏ();
				num = 2;
				continue;
			case 2:
				goto IL_80;
			}
			if (this.\u170D() != A_0)
			{
				num = 1;
				continue;
			}
			IL_80:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_08;
			default:
				goto IL_96;
			}
		}
		IL_96:
		if (false)
		{
		}
	}

	// Token: 0x06002BD4 RID: 11220 RVA: 0x0018668C File Offset: 0x0018568C
	public LineStyleType \u1736()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_6A;
				}
				break;
			case 2:
				goto IL_44;
			case 3:
				num = 0;
				continue;
			}
			IL_2A:
			if (!this.\u1719())
			{
				num = 3;
				continue;
			}
			num = 2;
			continue;
			goto IL_2A;
		}
		IL_44:
		sprỶ sprỶ = this.ᜄ;
		goto IL_80;
		IL_6A:
		if (true)
		{
		}
		if (false)
		{
		}
		sprỶ = this.ᜊ();
		IL_80:
		sprỶ sprỶ2 = sprỶ;
		return (LineStyleType)sprỶ2.ᜤ();
	}

	// Token: 0x06002BD5 RID: 11221 RVA: 0x00186720 File Offset: 0x00185720
	public void ᜃ(LineStyleType A_0)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜊ(true);
				this.ᜄ.ᜆ((ushort)A_0);
				this.\u171F().ᜁ();
				this.ᜏ();
				num = 5;
				continue;
			case 1:
				goto IL_B7;
			case 2:
				this.ᜊ(true);
				this.ᜄ.ᜉ(true);
				this.ᜏ();
				num = 1;
				continue;
			case 3:
				if (!this.ᜄ.\u1714())
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				goto IL_B7;
			case 5:
				goto IL_5E;
			}
			IL_28:
			if (this.\u1736() != A_0)
			{
				num = 0;
				continue;
			}
			IL_5E:
			num = 3;
			continue;
			IL_B7:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_28;
			default:
				goto IL_CD;
			}
		}
		IL_CD:
		if (false)
		{
		}
	}

	// Token: 0x06002BD6 RID: 11222 RVA: 0x0018680C File Offset: 0x0018580C
	public LineStyleType \u173C()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				IL_08:
				break;
			case 1:
				this.ᜊ();
				num = 2;
				continue;
			case 2:
				goto IL_49;
			}
			if (!this.\u1719())
			{
				num = 1;
				continue;
			}
			IL_49:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_08;
			default:
				goto IL_67;
			}
		}
		IL_67:
		if (false)
		{
		}
		return (LineStyleType)this.ᜄ.ᜤ();
	}

	// Token: 0x06002BD7 RID: 11223 RVA: 0x00186894 File Offset: 0x00185894
	public void ᜁ(LineStyleType A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				this.ᜊ(true);
				this.ᜄ.ᜊ(true);
				this.ᜏ();
				num = 3;
				continue;
			case 2:
				if (!this.ᜄ.\u1719())
				{
					num = 1;
					continue;
				}
				goto IL_B7;
			case 3:
				goto IL_B7;
			case 4:
				this.ᜊ(true);
				this.ᜄ.ᜆ((ushort)A_0);
				this.\u171F().ᜁ();
				this.ᜏ();
				num = 5;
				continue;
			case 5:
				goto IL_5E;
			}
			IL_28:
			if (this.\u173C() != A_0)
			{
				num = 4;
				continue;
			}
			IL_5E:
			if (true)
			{
			}
			num = 2;
			continue;
			IL_B7:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_28;
			default:
				goto IL_CD;
			}
		}
		IL_CD:
		if (false)
		{
		}
	}

	// Token: 0x06002BD8 RID: 11224 RVA: 0x00186980 File Offset: 0x00185980
	public bool ᜢ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_44;
			case 2:
				num = 3;
				continue;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_6A;
				}
				break;
			}
			IL_2A:
			if (!this.\u1719())
			{
				num = 2;
				continue;
			}
			num = 0;
			continue;
			goto IL_2A;
		}
		IL_44:
		sprỶ sprỶ = this.ᜄ;
		goto IL_80;
		IL_6A:
		if (true)
		{
		}
		if (false)
		{
		}
		sprỶ = this.ᜊ();
		IL_80:
		sprỶ sprỶ2 = sprỶ;
		return sprỶ2.\u1714();
	}

	// Token: 0x06002BD9 RID: 11225 RVA: 0x00186A14 File Offset: 0x00185A14
	public void ᜀ(bool A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_64;
			case 1:
				IL_08:
				if (true)
				{
				}
				break;
			case 2:
				this.ᜊ(true);
				this.ᜄ.ᜉ(A_0);
				this.ᜏ();
				num = 0;
				continue;
			}
			if (this.ᜢ() != A_0)
			{
				num = 2;
				continue;
			}
			IL_64:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_08;
			default:
				goto IL_7A;
			}
		}
		IL_7A:
		if (false)
		{
		}
	}

	// Token: 0x06002BDA RID: 11226 RVA: 0x00186AA4 File Offset: 0x00185AA4
	public bool ᜮ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_4C;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_72;
				}
				break;
			case 3:
				num = 1;
				continue;
			}
			IL_2A:
			if (!this.\u1719())
			{
				if (true)
				{
				}
				num = 3;
				continue;
			}
			num = 0;
			continue;
			goto IL_2A;
		}
		IL_4C:
		sprỶ sprỶ = this.ᜄ;
		goto IL_80;
		IL_72:
		if (false)
		{
		}
		sprỶ = this.ᜊ();
		IL_80:
		sprỶ sprỶ2 = sprỶ;
		return sprỶ2.\u1719();
	}

	// Token: 0x06002BDB RID: 11227 RVA: 0x00186B38 File Offset: 0x00185B38
	public void ᜄ(bool A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_64;
			case 1:
				this.ᜊ(true);
				this.ᜄ.ᜊ(A_0);
				this.ᜏ();
				if (true)
				{
				}
				num = 0;
				continue;
			case 2:
				IL_08:
				break;
			}
			if (this.ᜮ() != A_0)
			{
				num = 1;
				continue;
			}
			IL_64:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_08;
			default:
				goto IL_7A;
			}
		}
		IL_7A:
		if (false)
		{
		}
	}

	// Token: 0x06002BDC RID: 11228 RVA: 0x00186BC8 File Offset: 0x00185BC8
	public bool ᝇ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				if (this.ᜄ.ᜢ())
				{
					num = 3;
					continue;
				}
				return false;
			case 3:
				goto IL_8C;
			}
			if (true)
			{
			}
			if (!this.ᜏ)
			{
				goto IL_8E;
			}
			num = 0;
		}
		return false;
		IL_8C:
		return this.ᜯ() != this.ᜅ.MaxXFCount;
		IL_8E:
		return this.ᜯ() != this.ᜅ.MaxXFCount;
	}

	// Token: 0x06002BDD RID: 11229 RVA: 0x00186C84 File Offset: 0x00185C84
	public bool ᝑ()
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
		return this.ᜩ() == (ExcelColors)65;
	}

	// Token: 0x06002BDE RID: 11230 RVA: 0x00186CCC File Offset: 0x00185CCC
	public bool \u173E()
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
		return this.ᝆ() == ExcelColors.BlackCustom;
	}

	// Token: 0x06002BDF RID: 11231 RVA: 0x00186D14 File Offset: 0x00185D14
	private sprỶ ᜊ()
	{
		if (true)
		{
		}
		if (!this.ᝇ())
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
				return this.ᜄ;
			}
		}
		return ((spr\u192F)this.ᜅ.GetExtFormat(this.ᜯ())).ᜑ();
	}

	// Token: 0x06002BE0 RID: 11232 RVA: 0x00186D7C File Offset: 0x00185D7C
	private spr\u192F ᜉ()
	{
		if (!this.ᝇ())
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
				return this;
			}
		}
		return (spr\u192F)this.ᜅ.GetExtFormat(this.ᜯ());
	}

	// Token: 0x06002BE1 RID: 11233 RVA: 0x00186DDC File Offset: 0x00185DDC
	public sprᤅ \u1718()
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
		int a_ = this.ᝊ();
		return this.ᜅ.InnerFormats.ᜁ(a_);
	}

	// Token: 0x06002BE2 RID: 11234 RVA: 0x00186E30 File Offset: 0x00185E30
	protected OColor ᝋ()
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
		return this.ᜈ;
	}

	// Token: 0x06002BE3 RID: 11235 RVA: 0x00186E74 File Offset: 0x00185E74
	protected OColor ᜐ()
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

	// Token: 0x06002BE4 RID: 11236 RVA: 0x00186EB8 File Offset: 0x00185EB8
	protected OColor ᝈ()
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
		return this.ᜊ;
	}

	// Token: 0x06002BE5 RID: 11237 RVA: 0x00186EFC File Offset: 0x00185EFC
	protected OColor \u1752()
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
		return this.ᜋ;
	}

	// Token: 0x06002BE6 RID: 11238 RVA: 0x00186F40 File Offset: 0x00185F40
	protected OColor ᝌ()
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
		return this.ᜌ;
	}

	// Token: 0x06002BE7 RID: 11239 RVA: 0x00186F84 File Offset: 0x00185F84
	protected OColor \u1712()
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
		return this.\u170D;
	}

	// Token: 0x06002BE8 RID: 11240 RVA: 0x00186FC8 File Offset: 0x00185FC8
	protected OColor \u1757()
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
		return this.ᜎ;
	}

	// Token: 0x06002BE9 RID: 11241 RVA: 0x0018700C File Offset: 0x0018600C
	internal void ᜏ()
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
		this.ᜅ.Saved = false;
	}

	// Token: 0x06002BEA RID: 11242 RVA: 0x00187054 File Offset: 0x00186054
	public void ᜇ(spr\u192F A_0)
	{
		int a_ = 9;
		if (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("䬾㙀⩂⭄", a_));
			}
		}
		A_0.ᜅ = this.ᜅ;
		this.ᜄ.ᜃ(A_0.ᜄ);
	}

	// Token: 0x06002BEB RID: 11243 RVA: 0x001870D0 File Offset: 0x001860D0
	public spr\u192F ᜭ()
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
		return this.ᜎ(true);
	}

	// Token: 0x06002BEC RID: 11244 RVA: 0x00187114 File Offset: 0x00186114
	internal spr\u192F ᜎ(bool A_0)
	{
		spr\u192F spr_u192F;
		for (;;)
		{
			IL_1C:
			spr_u192F = this;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_AB:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				num = 3;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A0;
				case 1:
					return spr_u192F;
				case 2:
					spr_u192F = this.ᜅ.ᜀ(spr_u192F);
					num = 1;
					continue;
				case 3:
					if (spr_u192F.ᜑ().ᜎ() == sprỶ.TXFType.XF_CELL)
					{
						num = 4;
						continue;
					}
					return spr_u192F;
				case 4:
					spr_u192F = this.ᜅ.ᜀ(spr_u192F);
					spr_u192F.ᜑ().ᜀ(sprỶ.TXFType.XF_STYLE);
					spr_u192F.ᜄ(this.ᜠ());
					num = 0;
					continue;
				}
				goto IL_1C;
			}
			IL_A0:
			if (true)
			{
			}
			if (A_0)
			{
				goto IL_AB;
			}
			break;
		}
		return spr_u192F;
	}

	// Token: 0x06002BED RID: 11245 RVA: 0x001871E4 File Offset: 0x001861E4
	internal spr\u192F ᜆ(spr\u192F A_0)
	{
		switch (0)
		{
		default:
		{
			spr\u192F spr_u192F;
			for (;;)
			{
				sprỶ.TXFType txftype = this.ᜑ().ᜎ();
				spr_u192F = this.ᜭ();
				int num = 16;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						spr\u192F spr_u192F2;
						spr\u192F.ᜅ(A_0, spr_u192F2, false);
						spr_u192F2.ᜈ(true);
						bool flag = true;
						num = 18;
						continue;
					}
					case 1:
						if (!this.\u1753())
						{
							num = 20;
							continue;
						}
						goto IL_15B;
					case 2:
						if (!this.\u1717())
						{
							num = 3;
							continue;
						}
						goto IL_20D;
					case 3:
					{
						spr\u192F spr_u192F2;
						spr\u192F.ᜀ(A_0, spr_u192F2, false);
						spr_u192F2.ᜋ(true);
						bool flag = true;
						num = 6;
						continue;
					}
					case 4:
						if (!this.ᝀ())
						{
							num = 13;
							continue;
						}
						goto IL_1AD;
					case 5:
					{
						spr\u192F spr_u192F2;
						spr_u192F = this.ᜅ.InnerExtFormats.ᜁ(spr_u192F2);
						num = 17;
						continue;
					}
					case 6:
						goto IL_137;
					case 7:
						goto IL_15B;
					case 8:
						if (!this.\u1719())
						{
							num = 22;
							continue;
						}
						goto IL_258;
					case 9:
					{
						bool flag;
						if (flag)
						{
							num = 5;
							continue;
						}
						return spr_u192F;
					}
					case 10:
					{
						spr\u192F spr_u192F2;
						spr\u192F.ᜂ(A_0, spr_u192F2, false);
						spr_u192F2.ᜃ(true);
						bool flag = true;
						if (true)
						{
						}
						num = 21;
						continue;
					}
					case 11:
					{
						spr\u192F spr_u192F2 = (spr\u192F)spr_u192F.\u1758();
						bool flag = false;
						num = 12;
						continue;
					}
					case 12:
						if (!this.ᜦ())
						{
							num = 0;
							continue;
						}
						goto IL_F2;
					case 13:
					{
						spr\u192F spr_u192F2;
						spr\u192F.ᜃ(A_0, spr_u192F2, false);
						spr_u192F2.ᜉ(true);
						bool flag = true;
						num = 19;
						continue;
					}
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_137;
						default:
							if (false)
							{
							}
							goto IL_258;
						}
						break;
					case 15:
						if (!this.\u173D())
						{
							num = 10;
							continue;
						}
						goto IL_230;
					case 16:
						if (txftype == sprỶ.TXFType.XF_CELL)
						{
							num = 11;
							continue;
						}
						return spr_u192F;
					case 17:
						return spr_u192F;
					case 18:
						goto IL_F2;
					case 19:
						goto IL_1AD;
					case 20:
					{
						spr\u192F spr_u192F2;
						spr\u192F.ᜁ(A_0, spr_u192F2, false);
						spr_u192F2.\u170D(true);
						bool flag = true;
						num = 7;
						continue;
					}
					case 21:
						goto IL_230;
					case 22:
					{
						spr\u192F spr_u192F2;
						spr\u192F.ᜄ(A_0, spr_u192F2, false);
						spr_u192F2.ᜊ(true);
						bool flag = true;
						num = 14;
						continue;
					}
					}
					break;
					IL_F2:
					num = 8;
					continue;
					IL_15B:
					num = 2;
					continue;
					IL_1AD:
					num = 15;
					continue;
					IL_20D:
					num = 9;
					continue;
					IL_137:
					goto IL_20D;
					IL_230:
					num = 1;
					continue;
					IL_258:
					num = 4;
				}
			}
			return spr_u192F;
		}
		}
	}

	// Token: 0x06002BEE RID: 11246 RVA: 0x001874C8 File Offset: 0x001864C8
	internal void ᝂ()
	{
		for (;;)
		{
			sprỶ sprỶ = this.ᜊ();
			int num = 12;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!this.\u1753())
					{
						goto IL_91;
					}
					goto IL_EA;
				case 1:
					this.ᜁ(this.ᜉ());
					num = 15;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_91;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						this.ᜄ.ᜉ(sprỶ.\u171D());
						num = 4;
						continue;
					}
					break;
				case 3:
					if (!this.\u1717())
					{
						num = 16;
						continue;
					}
					return;
				case 4:
					goto IL_A1;
				case 5:
					return;
				case 6:
					goto IL_7E;
				case 7:
					if (!this.\u173D())
					{
						num = 9;
						continue;
					}
					goto IL_7E;
				case 8:
					if (!this.\u1719())
					{
						num = 1;
						continue;
					}
					goto IL_1B6;
				case 9:
					this.ᜄ.ᜈ(sprỶ.ᜂ());
					num = 6;
					continue;
				case 10:
					if (!this.ᝀ())
					{
						num = 2;
						continue;
					}
					goto IL_A1;
				case 11:
					this.ᜄ.ᜄ(sprỶ);
					num = 14;
					continue;
				case 12:
					if (!this.ᜦ())
					{
						num = 11;
						continue;
					}
					goto IL_C4;
				case 13:
					this.ᜀ(this.ᜉ());
					num = 17;
					continue;
				case 14:
					goto IL_C4;
				case 15:
					goto IL_1B6;
				case 16:
					this.ᜄ.ᜅ(sprỶ);
					num = 5;
					continue;
				case 17:
					goto IL_EA;
				}
				break;
				IL_7E:
				num = 0;
				continue;
				IL_91:
				num = 13;
				continue;
				IL_A1:
				num = 7;
				continue;
				IL_C4:
				num = 8;
				continue;
				IL_EA:
				num = 3;
				continue;
				IL_1B6:
				num = 10;
			}
		}
	}

	// Token: 0x06002BEF RID: 11247 RVA: 0x001876D8 File Offset: 0x001866D8
	private void ᜁ(spr\u192F A_0)
	{
		int a_ = 7;
		if (A_0 == null)
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
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("丼倾㑀ㅂ♄≆", a_));
			}
		}
		this.ᜄ.ᜁ(A_0.ᜄ);
		this.ᜊ.ᜀ(A_0.ᜊ, false);
		this.ᜋ.ᜀ(A_0.ᜋ, false);
		this.ᜌ.ᜀ(A_0.ᜌ, false);
		this.\u170D.ᜀ(A_0.\u170D, false);
		this.ᜎ.ᜀ(A_0.ᜎ, false);
	}

	// Token: 0x06002BF0 RID: 11248 RVA: 0x001877A0 File Offset: 0x001867A0
	private void ᜀ(spr\u192F A_0)
	{
		int a_ = 17;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜈ == null)
				{
					num = 3;
					continue;
				}
				goto IL_AD;
			case 1:
				goto IL_46;
			case 2:
				return;
			case 3:
				num = 4;
				continue;
			case 4:
				if (this.ᜅ.Loading)
				{
					num = 2;
					continue;
				}
				goto IL_AD;
			}
			goto IL_3B;
			IL_3E:
			num = 1;
			continue;
			IL_3B:
			if (A_0 == null)
			{
				goto IL_3E;
			}
			this.ᜄ.ᜂ(this.ᜄ);
			num = 0;
			continue;
			IL_AD:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3E;
			default:
				goto IL_C3;
			}
		}
		IL_46:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑆♈㹊㽌ⱎ㑐", a_));
		IL_C3:
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜈ.ᜀ(A_0.ᜈ, false);
		this.ᜉ.ᜀ(A_0.ᜉ, false);
	}

	// Token: 0x06002BF1 RID: 11249 RVA: 0x001878A4 File Offset: 0x001868A4
	public void ᝃ()
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

	// Token: 0x06002BF2 RID: 11250 RVA: 0x001878E0 File Offset: 0x001868E0
	public void \u1716()
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

	// Token: 0x06002BF3 RID: 11251 RVA: 0x0018791C File Offset: 0x0018691C
	internal spr\u192F(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜈ();
		this.ᜄ = (sprỶ)spr\u175E.ᜀ(TBIFFRecord.ExtendedFormat);
		this.ᜁ(this.ᜄ);
	}

	// Token: 0x06002BF4 RID: 11252 RVA: 0x00187970 File Offset: 0x00186970
	private spr\u192F(spr\u1DF5 A_0, object A_1, sprἛ A_2) : this(A_0, A_1)
	{
		this.ᜀ(A_2);
	}

	// Token: 0x06002BF5 RID: 11253 RVA: 0x0018798C File Offset: 0x0018698C
	internal spr\u192F(spr\u1DF5 A_0, object A_1, BiffRecordRaw[] A_2, int A_3) : this(A_0, A_1)
	{
		this.ᜀ(A_2, A_3);
	}

	// Token: 0x06002BF6 RID: 11254 RVA: 0x001879AC File Offset: 0x001869AC
	internal spr\u192F(spr\u1DF5 A_0, object A_1, List<BiffRecordRaw> A_2, int A_3) : this(A_0, A_1)
	{
		this.ᜀ(A_2, A_3);
	}

	// Token: 0x06002BF7 RID: 11255 RVA: 0x001879CC File Offset: 0x001869CC
	internal spr\u192F(spr\u1DF5 A_0, object A_1, sprỶ A_2) : this(A_0, A_1, A_2, true)
	{
	}

	// Token: 0x06002BF8 RID: 11256 RVA: 0x001879E4 File Offset: 0x001869E4
	internal spr\u192F(spr\u1DF5 A_0, object A_1, sprỶ A_2, bool A_3) : base(A_0, A_1)
	{
		this.ᜈ();
		this.ᜀ(A_2, A_3);
	}

	// Token: 0x06002BF9 RID: 11257 RVA: 0x00187A20 File Offset: 0x00186A20
	private void ᜈ()
	{
		int a_ = 4;
		if (true)
		{
		}
		this.ᜅ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
		if (this.ᜅ == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_5C;
			}
			if (false)
			{
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("樹崻䰽┿ⱁぃ晅❇⡉♋⭍㍏♑瑓㕕㥗㑙㉛ㅝᑟ䉡٣ͥ䡧౩ͫ᭭ṯᙱ婳", a_));
		}
	}

	// Token: 0x06002BFA RID: 11258 RVA: 0x00187AA0 File Offset: 0x00186AA0
	internal void ᜀ(sprἛ A_0)
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

	// Token: 0x06002BFB RID: 11259 RVA: 0x00187ADC File Offset: 0x00186ADC
	internal void ᜀ(IList<BiffRecordRaw> A_0, int A_1)
	{
		int a_ = 2;
		int num = 5;
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
				switch (num)
				{
				case 0:
					goto IL_87;
				case 1:
					if (A_1 >= 0)
					{
						num = 2;
						continue;
					}
					goto IL_B6;
				case 2:
					num = 3;
					continue;
				case 3:
					goto IL_74;
				case 4:
					goto IL_58;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				num = 1;
				continue;
			}
			IL_74:
			if (A_1 <= A_0.Count - 1)
			{
				goto IL_D8;
			}
			num = 0;
		}
		IL_58:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("尷嬹䠻弽", a_));
		IL_87:
		IL_B6:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䠷唹伻圽㐿⭁⭃⡅", a_), RecordTableEnumerator.b("渷嬹倻䬽┿扁❃❅♇⑉⍋㩍灏けㅓ癕㑗㽙⽛ⵝ䁟ᙡౣݥ٧䩩屫乭ᅯᱱၳ噵ίࡹ᥻ώꚅﲇ낏聯몙ﮝ캟얡킣캥蚧", a_));
		IL_D8:
		this.ᜁ((sprỶ)A_0[A_1]);
	}

	// Token: 0x06002BFC RID: 11260 RVA: 0x00187BD4 File Offset: 0x00186BD4
	internal void ᜁ(sprỶ A_0)
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
		this.ᜀ(A_0, true);
	}

	// Token: 0x06002BFD RID: 11261 RVA: 0x00187C18 File Offset: 0x00186C18
	[CLSCompliant(false)]
	protected void ᜀ(sprỶ A_0, bool A_1)
	{
		int a_ = 18;
		for (;;)
		{
			this.ᜄ = A_0;
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 1:
					if (this.ᜅ.Loading)
					{
						num = 0;
						continue;
					}
					goto IL_107;
				case 2:
					if (!A_1)
					{
						num = 3;
						continue;
					}
					goto IL_107;
				case 3:
					goto IL_105;
				case 4:
					if (this.ᜅ.Version == ExcelVersion.Version97to2003)
					{
						num = 5;
						continue;
					}
					goto IL_107;
				case 5:
					if (true)
					{
					}
					num = 6;
					continue;
				case 6:
					if (!this.\u1753())
					{
						num = 12;
						continue;
					}
					goto IL_107;
				case 7:
					if (!this.ᝇ())
					{
						num = 10;
						continue;
					}
					return;
				case 8:
					return;
				case 9:
					if ((int)this.ᜄ.\u171D() > this.ᜅ.InnerFonts.Count)
					{
						num = 11;
						continue;
					}
					this.ᜃ((int)((ushort)this.ᜅ.InnerExtFormats.Count));
					num = 2;
					continue;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_105;
					default:
						if (false)
						{
						}
						goto IL_107;
					}
					break;
				case 11:
					goto IL_83;
				case 12:
					num = 7;
					continue;
				}
				break;
				IL_105:
				num = 1;
				continue;
				IL_107:
				this.ᜬ();
				num = 8;
			}
		}
		IL_83:
		throw new ApplicationException(RecordTableEnumerator.b("േ㉉㡋⭍㹏㙑ㅓ㉕硗᱙㍛ⱝൟ͡ၣ䙥ᩧཀྵཫŭɯᙱ味ふ᝷ᑹࡻ㝽ﺅꢇﲏ뒓ﺕ聯벛튟춡쪣솥袧\udca9춫슭얯ힱ", a_));
	}

	// Token: 0x06002BFE RID: 11262 RVA: 0x00187DB8 File Offset: 0x00186DB8
	internal void \u1735()
	{
		for (;;)
		{
			spr\u192F spr_u192F = this.ᜉ();
			int num = 6;
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
					if ((ushort)spr_u192F.\u1739() == this.ᜄ.\u1712())
					{
						num = 8;
						continue;
					}
					goto IL_64;
				case 3:
					if (this.ᝇ())
					{
						num = 9;
						continue;
					}
					return;
				case 4:
					num = 10;
					continue;
				case 5:
					goto IL_109;
				case 6:
					if (this.ᜅ.Version == ExcelVersion.Version97to2003)
					{
						num = 4;
						continue;
					}
					return;
				case 7:
					goto IL_64;
				case 8:
					num = 11;
					continue;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FC;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 10:
					if (!this.\u1753())
					{
						goto IL_FC;
					}
					return;
				case 11:
					if ((ushort)spr_u192F.\u1734() != this.ᜄ.ᜬ())
					{
						num = 7;
						continue;
					}
					goto IL_109;
				}
				break;
				IL_64:
				ushort a_ = this.ᜄ.ᜬ();
				ushort a_2 = this.ᜄ.\u1712();
				this.\u170D(true);
				this.ᜄ.ᜊ(a_);
				this.ᜄ.ᜎ(a_2);
				num = 5;
				continue;
				IL_FC:
				num = 0;
				continue;
				IL_109:
				this.ᜬ();
				num = 1;
			}
		}
	}

	// Token: 0x06002BFF RID: 11263 RVA: 0x00187F50 File Offset: 0x00186F50
	protected void ᜬ()
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
		this.ᜈ = new OColor((ExcelColors)this.ᜄ.\u1712());
		this.ᜈ.AfterChange += this.ᜇ;
		this.ᜉ = new OColor((ExcelColors)this.ᜄ.ᜬ());
		this.ᜉ.AfterChange += this.ᜆ;
		this.ᜊ = new OColor((ExcelColors)this.ᜄ.ᜅ());
		this.ᜊ.AfterChange += this.ᜅ;
		this.ᜋ = new OColor((ExcelColors)this.ᜄ.ᜨ());
		this.ᜋ.AfterChange += this.ᜄ;
		this.ᜌ = new OColor((ExcelColors)this.ᜄ.\u1717());
		this.ᜌ.AfterChange += this.ᜃ;
		this.\u170D = new OColor((ExcelColors)this.ᜄ.ᜩ());
		this.\u170D.AfterChange += this.ᜂ;
		this.ᜎ = new OColor((ExcelColors)this.ᜄ.ᜌ());
		this.ᜎ.AfterChange += this.ᜁ;
	}

	// Token: 0x06002C00 RID: 11264 RVA: 0x001880C8 File Offset: 0x001870C8
	private void ᜇ()
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
		this.ᜄ.ᜎ((ushort)this.ᜈ.ᜂ(this.ᜅ));
	}

	// Token: 0x06002C01 RID: 11265 RVA: 0x00188120 File Offset: 0x00187120
	private void ᜆ()
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
		this.ᜄ.ᜊ((ushort)this.ᜉ.ᜂ(this.ᜅ));
	}

	// Token: 0x06002C02 RID: 11266 RVA: 0x00188178 File Offset: 0x00187178
	private void ᜅ()
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
		this.ᜄ.ᜅ((ushort)this.ᜊ.ᜂ(this.ᜅ));
	}

	// Token: 0x06002C03 RID: 11267 RVA: 0x001881D0 File Offset: 0x001871D0
	private void ᜄ()
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
		this.ᜄ.ᜃ((ushort)this.ᜋ.ᜂ(this.ᜅ));
	}

	// Token: 0x06002C04 RID: 11268 RVA: 0x00188228 File Offset: 0x00187228
	private void ᜃ()
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
		this.ᜄ.ᜄ((ushort)this.ᜌ.ᜂ(this.ᜅ));
	}

	// Token: 0x06002C05 RID: 11269 RVA: 0x00188280 File Offset: 0x00187280
	private void ᜂ()
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
		this.ᜄ.\u170D((ushort)this.\u170D.ᜂ(this.ᜅ));
	}

	// Token: 0x06002C06 RID: 11270 RVA: 0x001882D8 File Offset: 0x001872D8
	private void ᜁ()
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
		this.ᜄ.ᜂ((ushort)this.ᜎ.ᜂ(this.ᜅ));
	}

	// Token: 0x06002C07 RID: 11271 RVA: 0x00188330 File Offset: 0x00187330
	public void ᜀ(RecordArrayList A_0)
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
		sprỶ sprỶ = (sprỶ)this.ᜄ.Clone();
		sprỶ.ᜎ((ushort)this.ᜩ());
		sprỶ.ᜊ((ushort)this.ᝆ());
		this.ᜂ(sprỶ);
		A_0.ᜀ(sprỶ);
	}

	// Token: 0x06002C08 RID: 11272 RVA: 0x001883A8 File Offset: 0x001873A8
	protected void ᜂ(sprỶ A_0)
	{
		sprỶ sprỶ;
		for (;;)
		{
			if (true)
			{
			}
			spr\u192F spr_u192F = (spr\u192F)this.ᜅ.GetExtFormat(0);
			sprỶ = spr_u192F.ᜄ;
			int num = 18;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_30C;
				case 1:
					A_0.ᜅ(sprỶ);
					num = 26;
					continue;
				case 2:
					A_0.ᜈ(sprỶ.ᜂ());
					num = 29;
					continue;
				case 3:
					A_0.ᜉ(sprỶ.\u171D());
					num = 17;
					continue;
				case 4:
					A_0.ᜁ(sprỶ);
					num = 22;
					continue;
				case 5:
					if (!A_0.\u1715())
					{
						num = 35;
						continue;
					}
					goto IL_1B2;
				case 6:
					num = 19;
					continue;
				case 7:
					if (!A_0.\u1716())
					{
						num = 1;
						continue;
					}
					goto IL_200;
				case 8:
					A_0.ᜉ(sprỶ.\u171D());
					num = 38;
					continue;
				case 9:
					num = 39;
					continue;
				case 10:
					if (!A_0.ᜇ())
					{
						num = 15;
						continue;
					}
					return;
				case 11:
					if (this.ᜆ > 20)
					{
						num = 9;
						continue;
					}
					return;
				case 12:
					A_0.ᜄ(sprỶ);
					num = 28;
					continue;
				case 13:
					if (!A_0.ᜥ())
					{
						num = 36;
						continue;
					}
					goto IL_2AF;
				case 14:
					if (!A_0.ᜃ())
					{
						num = 3;
						continue;
					}
					goto IL_226;
				case 15:
					goto IL_36F;
				case 16:
					if (!this.ᝇ())
					{
						num = 25;
						continue;
					}
					return;
				case 17:
					goto IL_226;
				case 18:
					if (this.ᜯ() == 0)
					{
						num = 6;
						continue;
					}
					num = 16;
					continue;
				case 19:
					if (!A_0.ᜆ())
					{
						num = 12;
						continue;
					}
					goto IL_175;
				case 20:
					A_0.ᜅ(sprỶ);
					num = 0;
					continue;
				case 21:
					A_0.ᜂ(sprỶ);
					num = 33;
					continue;
				case 22:
					goto IL_F6;
				case 23:
					if (!A_0.\u1716())
					{
						num = 20;
						continue;
					}
					goto IL_30C;
				case 24:
					goto IL_416;
				case 25:
					num = 11;
					continue;
				case 26:
					goto IL_200;
				case 27:
					if (!A_0.ᜥ())
					{
						num = 4;
						continue;
					}
					goto IL_F6;
				case 28:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_20B;
					default:
						if (false)
						{
						}
						goto IL_175;
					}
					break;
				case 29:
					goto IL_34E;
				case 30:
					if (!A_0.\u1715())
					{
						num = 2;
						continue;
					}
					goto IL_34E;
				case 31:
					goto IL_20B;
				case 32:
					goto IL_1B2;
				case 33:
					goto IL_268;
				case 34:
					goto IL_2AF;
				case 35:
					A_0.ᜈ(sprỶ.ᜂ());
					num = 32;
					continue;
				case 36:
					A_0.ᜁ(sprỶ);
					num = 34;
					continue;
				case 37:
					if (!A_0.ᜇ())
					{
						num = 21;
						continue;
					}
					return;
				case 38:
					goto IL_2E9;
				case 39:
					if (!A_0.ᜆ())
					{
						num = 40;
						continue;
					}
					goto IL_416;
				case 40:
					A_0.ᜄ(sprỶ);
					num = 24;
					continue;
				}
				break;
				IL_F6:
				num = 23;
				continue;
				IL_175:
				num = 13;
				continue;
				IL_1B2:
				num = 37;
				continue;
				IL_200:
				num = 31;
				continue;
				IL_20B:
				if (!A_0.ᜃ())
				{
					num = 8;
					continue;
				}
				goto IL_2E9;
				IL_226:
				num = 5;
				continue;
				IL_2AF:
				num = 7;
				continue;
				IL_2E9:
				num = 30;
				continue;
				IL_30C:
				num = 14;
				continue;
				IL_34E:
				num = 10;
				continue;
				IL_416:
				num = 27;
			}
		}
		IL_268:
		return;
		IL_36F:
		A_0.ᜂ(sprỶ);
	}

	// Token: 0x06002C09 RID: 11273 RVA: 0x001887F4 File Offset: 0x001877F4
	public int ᜁ(object A_0)
	{
		int a_ = 17;
		if (!(A_0 is spr\u192F))
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
				break;
			}
			throw new ArgumentException(RecordTableEnumerator.b("ፆえ㭊⡌潎㡐⁒畔㥖㙘⽚絜ⱞ`๢d䥦", a_), RecordTableEnumerator.b("⡆⭈⅊", a_));
		}
		spr\u192F a_2 = (spr\u192F)A_0;
		return this.ᜅ(a_2);
	}

	// Token: 0x06002C0A RID: 11274 RVA: 0x00188874 File Offset: 0x00187874
	public int ᜅ(spr\u192F A_0)
	{
		switch (0)
		{
		default:
		{
			byte[] data;
			byte[] data2;
			int result;
			for (;;)
			{
				this.ᜂ(this.ᜄ);
				A_0.ᜂ(A_0.ᜄ);
				data = this.ᜄ.Data;
				data2 = A_0.ᜄ.Data;
				int num = 0;
				int num2 = Math.Min(data.Length, data2.Length);
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if ((result = data[num].CompareTo(data2[num])) != 0)
						{
							num3 = 5;
							continue;
						}
						num++;
						num3 = 1;
						continue;
					case 1:
						goto IL_DB;
					case 2:
						if (true)
						{
						}
						goto IL_DB;
					case 3:
						if (num >= num2)
						{
							num3 = 4;
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
							num3 = 0;
							continue;
						}
						break;
					case 4:
						goto IL_102;
					case 5:
						return result;
					}
					break;
					IL_DB:
					num3 = 3;
				}
			}
			return result;
			IL_102:
			return data.Length - data2.Length;
		}
		}
	}

	// Token: 0x06002C0B RID: 11275 RVA: 0x0018898C File Offset: 0x0018798C
	public int ᜄ(spr\u192F A_0)
	{
		for (;;)
		{
			int num = 1;
			int num2 = 2;
			for (;;)
			{
				int num3;
				int num4;
				switch (num2)
				{
				case 0:
					goto IL_1EF;
				case 1:
					if (num == 0)
					{
						num2 = 9;
						continue;
					}
					goto IL_CE;
				case 2:
					if (this.ᜇ != null)
					{
						num2 = 18;
						continue;
					}
					num2 = 13;
					continue;
				case 3:
					goto IL_1EF;
				case 4:
					if (num == 0)
					{
						if (true)
						{
						}
						num2 = 5;
						continue;
					}
					return 1;
				case 5:
					num2 = 11;
					continue;
				case 6:
					num2 = 12;
					continue;
				case 7:
					if (this.ᜈ == A_0.ᜈ)
					{
						num2 = 6;
						continue;
					}
					goto IL_1A7;
				case 8:
					num3 = 1;
					goto IL_114;
				case 9:
					num2 = 7;
					continue;
				case 10:
					num3 = 0;
					goto IL_114;
				case 11:
					if (this.ᜄ.ᜀ(A_0.ᜄ) != 0)
					{
						num2 = 14;
						continue;
					}
					return 0;
				case 12:
					if (!(this.ᜉ == A_0.ᜉ))
					{
						num2 = 15;
						continue;
					}
					num2 = 10;
					continue;
				case 13:
					if (A_0.ᝐ() != null)
					{
						num2 = 20;
						continue;
					}
					num2 = 17;
					continue;
				case 14:
					goto IL_1A5;
				case 15:
					goto IL_1A7;
				case 16:
					goto IL_CE;
				case 17:
					num4 = 0;
					goto IL_1B8;
				case 18:
					num = this.ᜇ.CompareTo(A_0.ᝐ());
					num2 = 0;
					continue;
				case 19:
					num4 = 1;
					goto IL_1B8;
				case 20:
					num2 = 19;
					continue;
				}
				break;
				IL_CE:
				num2 = 4;
				continue;
				IL_114:
				num = num3;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_1EF:
					num2 = 1;
					continue;
				default:
					if (false)
					{
					}
					num2 = 16;
					continue;
				}
				IL_1A7:
				num2 = 8;
				continue;
				IL_1B8:
				num = num4;
				num2 = 3;
			}
		}
		return 1;
		IL_1A5:
		return 1;
	}

	// Token: 0x06002C0C RID: 11276 RVA: 0x00188BAC File Offset: 0x00187BAC
	public virtual int ᜣ()
	{
		if (this.ᜇ != null)
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
				if (true)
				{
				}
				return this.ᜄ.GetHashCode() ^ this.ᜇ.GetHashCode();
			}
		}
		return this.ᜄ.GetHashCode();
	}

	// Token: 0x06002C0D RID: 11277 RVA: 0x00188C14 File Offset: 0x00187C14
	public virtual bool ᜃ(object A_0)
	{
		spr\u192F spr_u192F = A_0 as spr\u192F;
		if (spr_u192F == null)
		{
			if (true)
			{
			}
		}
		else
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
				return this.ᜄ(spr_u192F) == 0;
			}
		}
		return false;
	}

	// Token: 0x06002C0E RID: 11278 RVA: 0x00188C68 File Offset: 0x00187C68
	public static void ᜆ(spr\u192F A_0, spr\u192F A_1, bool A_2)
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
		spr\u192F.ᜅ(A_0, A_1, A_2);
		spr\u192F.ᜄ(A_0, A_1, A_2);
		spr\u192F.ᜃ(A_0, A_1, A_2);
		spr\u192F.ᜂ(A_0, A_1, A_2);
		spr\u192F.ᜁ(A_0, A_1, A_2);
		spr\u192F.ᜀ(A_0, A_1, A_2);
	}

	// Token: 0x06002C0F RID: 11279 RVA: 0x00188CD4 File Offset: 0x00187CD4
	private static void ᜅ(spr\u192F A_0, spr\u192F A_1, bool A_2)
	{
		for (;;)
		{
			A_1.ᜀ(A_0.\u171D());
			A_1.ᜀ(A_0.ᜋ());
			A_1.ᜅ(A_0.\u1733());
			A_1.ᜁ(A_0.\u171A());
			A_1.ᜅ(A_0.\u171B());
			A_1.ᜀ(A_0.\u171C());
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_2)
					{
						num = 2;
						continue;
					}
					return;
				case 1:
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						A_0.ᜈ(false);
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
		}
	}

	// Token: 0x06002C10 RID: 11280 RVA: 0x00188D94 File Offset: 0x00187D94
	private static void ᜄ(spr\u192F A_0, spr\u192F A_1, bool A_2)
	{
		for (;;)
		{
			if (true)
			{
			}
			A_1.ᜑ().ᜀ(A_0.ᜑ().ᜋ());
			A_1.ᜑ().ᜃ(A_0.ᜑ().ᜈ());
			A_1.ᜑ().ᜂ(A_0.ᜑ().ᜭ());
			A_1.ᜑ().ᜁ(A_0.ᜑ().ᜐ());
			A_1.ᜑ().ᜉ(A_0.ᜑ().\u1714());
			A_1.ᜑ().ᜊ(A_0.ᜑ().\u1719());
			A_1.ᜑ().ᜆ(A_0.ᜑ().ᜤ());
			A_1.\u173F().ᜀ(A_0.\u173F(), true);
			A_1.ᜡ().ᜀ(A_0.ᜡ(), true);
			A_1.ᝅ().ᜀ(A_0.ᝅ(), true);
			A_1.\u1756().ᜀ(A_0.\u1756(), true);
			A_1.\u171F().ᜀ(A_0.\u171F(), true);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						A_0.ᜊ(false);
						num = 0;
						continue;
					}
					break;
				case 2:
					if (A_2)
					{
						num = 1;
						continue;
					}
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06002C11 RID: 11281 RVA: 0x00188F08 File Offset: 0x00187F08
	private static void ᜃ(spr\u192F A_0, spr\u192F A_1, bool A_2)
	{
		for (;;)
		{
			A_1.ᜂ(A_0.\u173B());
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						A_0.ᜉ(false);
						num = 2;
						continue;
					}
					break;
				case 1:
					if (A_2)
					{
						num = 0;
						continue;
					}
					return;
				case 2:
					goto IL_6A;
				}
				break;
			}
		}
		IL_6A:
		if (true)
		{
		}
	}

	// Token: 0x06002C12 RID: 11282 RVA: 0x00188F8C File Offset: 0x00187F8C
	private static void ᜂ(spr\u192F A_0, spr\u192F A_1, bool A_2)
	{
		for (;;)
		{
			A_1.ᜁ(A_0.\u1715());
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						A_0.ᜃ(false);
						num = 0;
						continue;
					}
					break;
				case 2:
					if (true)
					{
					}
					if (A_2)
					{
						num = 1;
						continue;
					}
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06002C13 RID: 11283 RVA: 0x00189010 File Offset: 0x00188010
	private static void ᜁ(spr\u192F A_0, spr\u192F A_1, bool A_2)
	{
		if (true)
		{
		}
		for (;;)
		{
			A_1.ᝄ().ᜀ(A_0.ᝄ(), true);
			A_1.\u1754().ᜀ(A_0.\u1754(), true);
			A_1.ᜀ(A_0.ᜤ());
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_2)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						A_0.\u170D(false);
						num = 2;
						continue;
					}
					break;
				case 2:
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06002C14 RID: 11284 RVA: 0x001890B8 File Offset: 0x001880B8
	private static void ᜀ(spr\u192F A_0, spr\u192F A_1, bool A_2)
	{
		for (;;)
		{
			A_1.ᜆ(A_0.\u1755());
			A_1.ᜁ(A_0.ᝎ());
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
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
						A_0.ᜋ(false);
						num = 2;
						continue;
					}
					break;
				case 1:
					if (A_2)
					{
						num = 0;
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

	// Token: 0x06002C15 RID: 11285 RVA: 0x00189148 File Offset: 0x00188148
	protected void ᜃ(spr\u192F A_0)
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
		this.ᜈ.ᜀ(A_0.ᜈ, false);
		this.ᜉ.ᜀ(A_0.ᜉ, false);
		this.ᜊ.ᜀ(A_0.ᜊ, false);
		this.ᜋ.ᜀ(A_0.ᜋ, false);
		this.ᜌ.ᜀ(A_0.ᜌ, false);
		this.\u170D.ᜀ(A_0.\u170D, false);
		this.ᜎ.ᜀ(A_0.ᜎ, false);
	}

	// Token: 0x06002C16 RID: 11286 RVA: 0x00189204 File Offset: 0x00188204
	public object \u1758()
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
		return this.ᜀ(this);
	}

	// Token: 0x06002C17 RID: 11287 RVA: 0x00189248 File Offset: 0x00188248
	public spr\u192F ᜀ(object A_0)
	{
		spr\u192F spr_u192F;
		for (;;)
		{
			spr_u192F = (base.MemberwiseClone() as spr\u192F);
			spr_u192F.ᜄ = (this.ᜄ.Clone() as sprỶ);
			spr_u192F.ᜃ(65535);
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					spr_u192F.ᜇ = this.ᜇ.Clone(spr_u192F);
					if (true)
					{
					}
					num = 4;
					continue;
				case 1:
					spr_u192F.SetParent(A_0);
					spr_u192F.ᜈ();
					num = 7;
					continue;
				case 2:
					spr_u192F.ᜄ(spr_u192F.ᜅ.MaxXFCount);
					num = 5;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_107;
					default:
						if (false)
						{
						}
						if (this.ᜯ() == this.ᜅ.MaxXFCount)
						{
							num = 2;
							continue;
						}
						goto IL_14D;
					}
					break;
				case 4:
					goto IL_77;
				case 5:
					goto IL_125;
				case 6:
					if (spr_u192F.ᜇ != null)
					{
						num = 0;
						continue;
					}
					goto IL_77;
				case 7:
					goto IL_107;
				case 8:
					if (A_0 != spr_u192F.Parent)
					{
						num = 1;
						continue;
					}
					goto IL_127;
				}
				break;
				IL_77:
				num = 3;
				continue;
				IL_127:
				num = 6;
				continue;
				IL_107:
				goto IL_127;
			}
		}
		IL_125:
		IL_14D:
		spr_u192F.ᜬ();
		spr_u192F.ᜈ.ᜀ(this.ᜈ, false);
		spr_u192F.ᜉ.ᜀ(this.ᜉ, false);
		spr_u192F.ᜊ.ᜀ(this.ᜊ, false);
		spr_u192F.ᜋ.ᜀ(this.ᜋ, false);
		spr_u192F.ᜌ.ᜀ(this.ᜌ, false);
		spr_u192F.\u170D.ᜀ(this.\u170D, false);
		spr_u192F.ᜎ.ᜀ(this.ᜎ, false);
		return spr_u192F;
	}

	// Token: 0x06002C18 RID: 11288 RVA: 0x00189428 File Offset: 0x00188428
	object ICloneParent.ᜂ(object A_0)
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
		return this.ᜀ(A_0);
	}

	// Token: 0x06002C19 RID: 11289 RVA: 0x0018946C File Offset: 0x0018846C
	public void ᜥ()
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
		this.ᜄ = null;
		this.ᜈ = null;
		this.ᜉ = null;
		this.ᜊ = null;
		this.ᜋ = null;
		this.ᜌ = null;
		this.\u170D = null;
		this.ᜎ = null;
		this.Dispose();
	}

	// Token: 0x06002C1A RID: 11290 RVA: 0x001894E8 File Offset: 0x001884E8
	void IDisposable.ᜧ()
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
		GC.SuppressFinalize(this);
	}

	// Token: 0x04001454 RID: 5204
	public const int ᜀ = 4095;

	// Token: 0x04001455 RID: 5205
	public const int ᜁ = 255;

	// Token: 0x04001456 RID: 5206
	internal ushort ᜂ = 700;

	// Token: 0x04001457 RID: 5207
	internal ushort ᜃ = 400;

	// Token: 0x04001458 RID: 5208
	private sprỶ ᜄ;

	// Token: 0x04001459 RID: 5209
	private XlsWorkbook ᜅ;

	// Token: 0x0400145A RID: 5210
	private int ᜆ;

	// Token: 0x0400145B RID: 5211
	private XlsShapeFill ᜇ;

	// Token: 0x0400145C RID: 5212
	private OColor ᜈ;

	// Token: 0x0400145D RID: 5213
	private OColor ᜉ;

	// Token: 0x0400145E RID: 5214
	private OColor ᜊ;

	// Token: 0x0400145F RID: 5215
	private OColor ᜋ;

	// Token: 0x04001460 RID: 5216
	private OColor ᜌ;

	// Token: 0x04001461 RID: 5217
	private OColor \u170D;

	// Token: 0x04001462 RID: 5218
	private OColor ᜎ;

	// Token: 0x04001463 RID: 5219
	private bool ᜏ;
}
