using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x0200052A RID: 1322
internal class spr\u2436 : XlsShapeFill
{
	// Token: 0x060050D3 RID: 20691 RVA: 0x0032B008 File Offset: 0x0032A008
	internal spr\u2436(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜀ = (spr\u216D)spr\u175E.ᜀ(TBIFFRecord.ChartGelFrame);
		this.m_bIsShapeFill = false;
	}

	// Token: 0x060050D4 RID: 20692 RVA: 0x0032B03C File Offset: 0x0032A03C
	internal spr\u2436(spr\u1DF5 A_0, object A_1, spr\u216D A_2)
	{
		int a_ = 0;
		base..ctor(A_0, A_1);
		if (A_2 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("儵崷嘹", a_));
		}
		this.ᜀ = A_2;
		this.m_bIsShapeFill = false;
		this.ᜀ();
	}

	// Token: 0x060050D5 RID: 20693 RVA: 0x0032B088 File Offset: 0x0032A088
	private void ᜀ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList list = this.ᜀ.ᜅ();
				int num = 0;
				int count = list.Count;
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						spr\u23E7.ᜀ ᜀ;
						if (ᜀ != null)
						{
							num2 = 6;
							continue;
						}
						return;
					}
					case 1:
						if (num >= count)
						{
							num2 = 5;
							continue;
						}
						base.ᜀ(list[num] as spr\u23E7.ᜀ);
						num++;
						num2 = 2;
						continue;
					case 2:
						goto IL_7C;
					case 3:
						return;
					case 4:
						goto IL_7C;
					case 5:
					{
						if (true)
						{
						}
						spr\u23E7.ᜀ ᜀ = base.ParsePictureData;
						num2 = 0;
						continue;
					}
					case 6:
						base.ParsePictureOrUserDefinedTexture(this.m_fillType == ShapeFillType.Picture);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7C;
						default:
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					}
					break;
					IL_7C:
					num2 = 1;
				}
			}
			return;
		}
	}

	// Token: 0x060050D6 RID: 20694 RVA: 0x0032B194 File Offset: 0x0032A194
	[CLSCompliant(false)]
	public void ᜀ(IList<IRecordStorage> A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				int num2;
				IList<BiffRecordRaw> list;
				switch (num)
				{
				case 0:
					goto IL_10A;
				case 1:
				{
					int count;
					if (num2 >= count)
					{
						num = 3;
						continue;
					}
					goto IL_63;
				}
				case 2:
					goto IL_10A;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_63;
					default:
						goto IL_140;
					}
					break;
				case 5:
					goto IL_108;
				case 6:
					num = 7;
					continue;
				case 7:
				{
					if (!this.Visible)
					{
						num = 5;
						continue;
					}
					this.ᜀ.ᜈ();
					spr\u2028 a_ = new spr\u2028(this.ᜀ.ᜅ());
					base.\u170D(a_);
					spr\u216D spr_u216D = (spr\u216D)this.ᜀ.Clone();
					list = spr_u216D.ᜃ();
					num2 = 0;
					int count = list.Count;
					num = 2;
					continue;
				}
				}
				if (!(base.Parent as spr\u218E).ᜇ())
				{
					num = 6;
					continue;
				}
				break;
				IL_63:
				if (true)
				{
				}
				A_0.Add(list[num2]);
				num2++;
				num = 0;
				continue;
				IL_10A:
				num = 1;
			}
			IL_108:
			return;
			IL_140:
			if (false)
			{
			}
			return;
		}
		}
	}

	// Token: 0x060050D7 RID: 20695 RVA: 0x0032B2EC File Offset: 0x0032A2EC
	public virtual double ᜁ()
	{
		int a_ = 14;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("၃⹅ⅇ㥉汋㹍≏㵑⑓㍕⩗⹙╛繝џൡţᕥ٧ṩ䱫ᵭկɱѳ᥵੷๹屻᝽ꊁ꺍횏ﮑ歹뢗ﲙ춟쎡킣袥", a_));
	}

	// Token: 0x060050D8 RID: 20696 RVA: 0x0032B344 File Offset: 0x0032A344
	public virtual void ᜁ(double A_0)
	{
		int a_ = 17;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("ፆⅈ≊㹌潎⅐⅒㩔❖㱘⥚⥜♞䅠ݢ੤ɦᩨժᥬ佮ɰٲմݶᙸॺॼ彾ꖄﾌﮎ놐햒ﲔﮖ뮚ﮜ펠캢쒤펦螨", a_));
	}

	// Token: 0x060050D9 RID: 20697 RVA: 0x0032B39C File Offset: 0x0032A39C
	public virtual double ᜃ()
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("ᕀ⭂ⱄ㑆楈㭊㽌⁎⅐㙒❔⍖⁘筚㥜ぞѠၢ୤፦䥨ᡪᡬὮŰᱲݴͶ奸ቺ፼彾ﶈꮊ쮌﶐ﾒ떔ﺞ햠趢", a_));
	}

	// Token: 0x060050DA RID: 20698 RVA: 0x0032B3F4 File Offset: 0x0032A3F4
	public virtual void ᜀ(double A_0)
	{
		int a_ = 8;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("樽⠿⭁㝃晅㡇㡉⍋㹍㕏⁑⁓⽕硗㹙㍛㭝፟ౡၣ䙥᭧ὩᱫṭὯqs噵ᅷᑹ屻ᵽꢇ첉ﲏ늑秊ﶛ躟", a_));
	}

	// Token: 0x060050DB RID: 20699 RVA: 0x0032B44C File Offset: 0x0032A44C
	public virtual OColor ᜄ()
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
		return (base.Parent as spr\u218E).ᜀ();
	}

	// Token: 0x060050DC RID: 20700 RVA: 0x0032B498 File Offset: 0x0032A498
	public virtual OColor ᜅ()
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
		return (base.Parent as spr\u218E).ᜅ();
	}

	// Token: 0x060050DD RID: 20701 RVA: 0x0032B4E4 File Offset: 0x0032A4E4
	public virtual bool ᜆ()
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
		return (base.Parent as spr\u218E).ᜆ() != ExcelPatternType.None;
	}

	// Token: 0x060050DE RID: 20702 RVA: 0x0032B538 File Offset: 0x0032A538
	public virtual void ᜀ(bool A_0)
	{
		spr\u218E spr_u218E;
		for (;;)
		{
			spr_u218E = (base.Parent as spr\u218E);
			spr_u218E.ᜀ(false);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0)
					{
						num = 1;
						continue;
					}
					spr_u218E.ᜀ(ExcelPatternType.None);
					num = 4;
					continue;
				case 1:
					num = 3;
					continue;
				case 2:
					goto IL_7F;
				case 3:
					if (spr_u218E.ᜆ() == ExcelPatternType.None)
					{
						num = 2;
						continue;
					}
					return;
				case 4:
					goto IL_5D;
				}
				break;
			}
		}
		IL_5D:
		return;
		IL_7F:
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
		spr_u218E.ᜀ(ExcelPatternType.Solid);
	}

	// Token: 0x060050DF RID: 20703 RVA: 0x0032B5EC File Offset: 0x0032A5EC
	[CLSCompliant(false)]
	internal virtual sprᡍ ᜀ(sprᡍ A_0)
	{
		int a_ = 3;
		MemoryStream memoryStream;
		byte[] array;
		byte[] buffer;
		byte[] array2;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_E8:
			try
			{
				new MD5CryptoServiceProvider().ComputeHash(memoryStream).CopyTo(array, 8);
				goto IL_FE;
			}
			catch (InvalidOperationException)
			{
				new MACTripleDES().ComputeHash(memoryStream).CopyTo(array, 8);
				goto IL_FE;
			}
			goto IL_9A;
			IL_FE:
			array[24] = byte.MaxValue;
			buffer.CopyTo(array, 25);
			BitConverter.GetBytes(buffer.Length + 17).CopyTo(array, 4);
			array2.CopyTo(array, 0);
			XlsShape.ᜀ(A_0, MsoOptions.PatternTexture, 0, array, true);
			return A_0;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 1;
				break;
			}
			break;
		}
		for (;;)
		{
			IL_50:
			switch (num)
			{
			case 0:
				goto IL_E8;
			case 2:
				goto IL_6F;
			}
			if (A_0 != null)
			{
				goto IL_9A;
			}
			num = 2;
		}
		IL_6F:
		throw new ArgumentNullException(RecordTableEnumerator.b("嘸䬺䤼", a_));
		IL_9A:
		memoryStream = new MemoryStream();
		this.m_picture.Save(memoryStream, this.m_picture.RawFormat);
		buffer = memoryStream.GetBuffer();
		array = new byte[buffer.Length + 25];
		array2 = new byte[]
		{
			160,
			70,
			29,
			240
		};
		num = 0;
		goto IL_50;
	}

	// Token: 0x060050E0 RID: 20704 RVA: 0x0032B744 File Offset: 0x0032A744
	protected virtual int ᜀ(Image A_0, string A_1)
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
		return 0;
	}

	// Token: 0x060050E1 RID: 20705 RVA: 0x0032B780 File Offset: 0x0032A780
	[CLSCompliant(false)]
	internal virtual sprᡍ ᜁ(sprᡍ A_0)
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
		return A_0;
	}

	// Token: 0x060050E2 RID: 20706 RVA: 0x0032B7BC File Offset: 0x0032A7BC
	protected virtual void ᜂ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_94:
			num = 3;
			break;
		case 1:
			goto IL_20;
		default:
			goto IL_20;
		}
		for (;;)
		{
			IL_30:
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_7D;
			case 1:
				goto IL_87;
			case 3:
				goto IL_6E;
			case 4:
				num = 1;
				continue;
			}
			if (!(base.Parent as spr\u218E).ᜇ())
			{
				num = 4;
				continue;
			}
			IL_6E:
			this.Visible = true;
			num = 0;
		}
		IL_7D:
		return;
		IL_87:
		if (base.Parent is XlsChartFrameFormat)
		{
			goto IL_94;
		}
		return;
		IL_20:
		if (false)
		{
		}
		num = 2;
		goto IL_30;
	}

	// Token: 0x04002429 RID: 9257
	private new spr\u216D ᜀ;
}
