using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Spire.CompoundFile.Doc;
using Spire.Doc;

// Token: 0x02000201 RID: 513
internal struct sprᦪ : IFormattable
{
	// Token: 0x06001670 RID: 5744 RVA: 0x0016AD2C File Offset: 0x00169D2C
	public sprᦪ(double A_0)
	{
		this.ᜋ = A_0;
		this.ᜌ = UnitGraphics.Point;
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x0016AD48 File Offset: 0x00169D48
	public sprᦪ(double A_0, UnitGraphics A_1)
	{
		int a_ = 5;
		if (!Enum.IsDefined(typeof(UnitGraphics), A_1))
		{
			throw new InvalidEnumArgumentException(ClipboardData.b("ὪᑬὮᑰ", a_));
		}
		this.ᜋ = A_0;
		this.ᜌ = A_1;
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x0016AD9C File Offset: 0x00169D9C
	public double ᜇ()
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
		return this.ᜋ;
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x0016ADE0 File Offset: 0x00169DE0
	public UnitGraphics ᜈ()
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
		return this.ᜌ;
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x0016AE24 File Offset: 0x00169E24
	public double ᜂ()
	{
		for (;;)
		{
			UnitGraphics unitGraphics = this.ᜌ;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (unitGraphics)
					{
					case UnitGraphics.Point:
						goto IL_C9;
					case UnitGraphics.Inch:
						goto IL_88;
					case UnitGraphics.Millimeter:
						goto IL_D0;
					case UnitGraphics.Centimeter:
						goto IL_6D;
					case UnitGraphics.Presentation:
						goto IL_A6;
					default:
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_A4;
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
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_6D:
		return this.ᜋ * 72.0 / 2.54;
		IL_88:
		return this.ᜋ * 72.0;
		IL_A4:
		throw new InvalidCastException();
		IL_A6:
		if (true)
		{
		}
		return this.ᜋ * 72.0 / 96.0;
		IL_C9:
		return this.ᜋ;
		IL_D0:
		return this.ᜋ * 72.0 / 25.4;
	}

	// Token: 0x06001675 RID: 5749 RVA: 0x0016AF24 File Offset: 0x00169F24
	public void ᜉ(double A_0)
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
		this.ᜋ = A_0;
		this.ᜌ = UnitGraphics.Point;
	}

	// Token: 0x06001676 RID: 5750 RVA: 0x0016AF70 File Offset: 0x00169F70
	public double ᜁ()
	{
		for (;;)
		{
			UnitGraphics unitGraphics = this.ᜌ;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_98;
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
						break;
					}
					if (true)
					{
					}
					num = 0;
					continue;
				case 2:
					switch (unitGraphics)
					{
					case UnitGraphics.Point:
						goto IL_AB;
					case UnitGraphics.Inch:
						goto IL_7E;
					case UnitGraphics.Millimeter:
						goto IL_BC;
					case UnitGraphics.Centimeter:
						goto IL_6D;
					case UnitGraphics.Presentation:
						goto IL_9A;
					default:
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_6D:
		return this.ᜋ / 2.54;
		IL_7E:
		return this.ᜋ;
		IL_98:
		throw new InvalidCastException();
		IL_9A:
		return this.ᜋ / 96.0;
		IL_AB:
		return this.ᜋ / 72.0;
		IL_BC:
		return this.ᜋ / 25.4;
	}

	// Token: 0x06001677 RID: 5751 RVA: 0x0016B050 File Offset: 0x0016A050
	public void ᜊ(double A_0)
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
		this.ᜋ = A_0;
		this.ᜌ = UnitGraphics.Inch;
	}

	// Token: 0x06001678 RID: 5752 RVA: 0x0016B09C File Offset: 0x0016A09C
	public double ᜄ()
	{
		for (;;)
		{
			UnitGraphics unitGraphics = this.ᜌ;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A2;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BF;
					default:
						if (false)
						{
						}
						switch (unitGraphics)
						{
						case UnitGraphics.Point:
							goto IL_BF;
						case UnitGraphics.Inch:
							goto IL_86;
						case UnitGraphics.Millimeter:
							goto IL_DA;
						case UnitGraphics.Centimeter:
							goto IL_75;
						case UnitGraphics.Presentation:
							goto IL_A4;
						default:
							num = 2;
							continue;
						}
						break;
					}
					break;
				case 2:
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_75:
		return this.ᜋ * 10.0;
		IL_86:
		return this.ᜋ * 25.4;
		IL_A2:
		throw new InvalidCastException();
		IL_A4:
		return this.ᜋ * 25.4 / 96.0;
		IL_BF:
		return this.ᜋ * 25.4 / 72.0;
		IL_DA:
		return this.ᜋ;
	}

	// Token: 0x06001679 RID: 5753 RVA: 0x0016B190 File Offset: 0x0016A190
	public void ᜇ(double A_0)
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
		this.ᜋ = A_0;
		this.ᜌ = UnitGraphics.Millimeter;
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x0016B1DC File Offset: 0x0016A1DC
	public double ᜃ()
	{
		for (;;)
		{
			UnitGraphics unitGraphics = this.ᜌ;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B5;
					default:
						if (false)
						{
						}
						switch (unitGraphics)
						{
						case UnitGraphics.Point:
							goto IL_B5;
						case UnitGraphics.Inch:
							goto IL_7C;
						case UnitGraphics.Millimeter:
							goto IL_D0;
						case UnitGraphics.Centimeter:
							goto IL_75;
						case UnitGraphics.Presentation:
							goto IL_9A;
						default:
							num = 0;
							continue;
						}
						break;
					}
					break;
				case 2:
					goto IL_98;
				}
				break;
			}
		}
		IL_75:
		return this.ᜋ;
		IL_7C:
		return this.ᜋ * 2.54;
		IL_98:
		throw new InvalidCastException();
		IL_9A:
		return this.ᜋ * 2.54 / 96.0;
		IL_B5:
		return this.ᜋ * 2.54 / 72.0;
		IL_D0:
		return this.ᜋ / 10.0;
	}

	// Token: 0x0600167B RID: 5755 RVA: 0x0016B2D0 File Offset: 0x0016A2D0
	public void ᜈ(double A_0)
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
		this.ᜋ = A_0;
		this.ᜌ = UnitGraphics.Centimeter;
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x0016B31C File Offset: 0x0016A31C
	public double ᜅ()
	{
		for (;;)
		{
			UnitGraphics unitGraphics = this.ᜌ;
			int num = 1;
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
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B5;
					default:
						if (false)
						{
						}
						switch (unitGraphics)
						{
						case UnitGraphics.Point:
							goto IL_B5;
						case UnitGraphics.Inch:
							goto IL_90;
						case UnitGraphics.Millimeter:
							goto IL_D0;
						case UnitGraphics.Centimeter:
							goto IL_75;
						case UnitGraphics.Presentation:
							goto IL_AE;
						default:
							num = 0;
							continue;
						}
						break;
					}
					break;
				case 2:
					goto IL_AC;
				}
				break;
			}
		}
		IL_75:
		return this.ᜋ * 96.0 / 2.54;
		IL_90:
		return this.ᜋ * 96.0;
		IL_AC:
		throw new InvalidCastException();
		IL_AE:
		return this.ᜋ;
		IL_B5:
		return this.ᜋ * 96.0 / 72.0;
		IL_D0:
		return this.ᜋ * 96.0 / 25.4;
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x0016B41C File Offset: 0x0016A41C
	public void ᜆ(double A_0)
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
		this.ᜋ = A_0;
		this.ᜌ = UnitGraphics.Point;
	}

	// Token: 0x0600167E RID: 5758 RVA: 0x0016B468 File Offset: 0x0016A468
	public string ᜀ(IFormatProvider A_0)
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
		return this.ᜋ.ToString(A_0) + this.ᜀ();
	}

	// Token: 0x0600167F RID: 5759 RVA: 0x0016B4BC File Offset: 0x0016A4BC
	string IFormattable.ᜀ(string A_0, IFormatProvider A_1)
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
		return this.ᜋ.ToString(A_0, A_1) + this.ᜀ();
	}

	// Token: 0x06001680 RID: 5760 RVA: 0x0016B514 File Offset: 0x0016A514
	public string ᜆ()
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
		return this.ᜋ.ToString(CultureInfo.InvariantCulture) + this.ᜀ();
	}

	// Token: 0x06001681 RID: 5761 RVA: 0x0016B56C File Offset: 0x0016A56C
	private string ᜀ()
	{
		int a_ = 19;
		for (;;)
		{
			UnitGraphics unitGraphics = this.ᜌ;
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B8;
					default:
						if (false)
						{
						}
						switch (unitGraphics)
						{
						case UnitGraphics.Point:
							goto IL_B8;
						case UnitGraphics.Inch:
							goto IL_8D;
						case UnitGraphics.Millimeter:
							goto IL_C7;
						case UnitGraphics.Centimeter:
							goto IL_7E;
						case UnitGraphics.Presentation:
							goto IL_A9;
						default:
							num = 1;
							continue;
						}
						break;
					}
					break;
				case 1:
					num = 2;
					continue;
				case 2:
					goto IL_A7;
				}
				break;
			}
		}
		IL_7E:
		return ClipboardData.b("᩸ᙺ", a_);
		IL_8D:
		return ClipboardData.b("ၸᕺ", a_);
		IL_A7:
		throw new InvalidCastException();
		IL_A9:
		return ClipboardData.b("ॸ๺", a_);
		IL_B8:
		return ClipboardData.b("ॸེ", a_);
		IL_C7:
		return ClipboardData.b("ᑸᙺ", a_);
	}

	// Token: 0x06001682 RID: 5762 RVA: 0x0016B654 File Offset: 0x0016A654
	public static sprᦪ ᜅ(double A_0)
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
		sprᦪ result;
		result.ᜋ = A_0;
		result.ᜌ = UnitGraphics.Point;
		return result;
	}

	// Token: 0x06001683 RID: 5763 RVA: 0x0016B6A0 File Offset: 0x0016A6A0
	public static sprᦪ ᜄ(double A_0)
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
		sprᦪ result;
		result.ᜋ = A_0;
		result.ᜌ = UnitGraphics.Inch;
		return result;
	}

	// Token: 0x06001684 RID: 5764 RVA: 0x0016B6EC File Offset: 0x0016A6EC
	public static sprᦪ ᜃ(double A_0)
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
		sprᦪ result;
		result.ᜋ = A_0;
		result.ᜌ = UnitGraphics.Millimeter;
		return result;
	}

	// Token: 0x06001685 RID: 5765 RVA: 0x0016B738 File Offset: 0x0016A738
	public static sprᦪ ᜂ(double A_0)
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
		sprᦪ result;
		result.ᜋ = A_0;
		result.ᜌ = UnitGraphics.Centimeter;
		return result;
	}

	// Token: 0x06001686 RID: 5766 RVA: 0x0016B784 File Offset: 0x0016A784
	public static sprᦪ ᜁ(double A_0)
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
		sprᦪ result;
		result.ᜋ = A_0;
		result.ᜌ = UnitGraphics.Presentation;
		return result;
	}

	// Token: 0x06001687 RID: 5767 RVA: 0x0016B7D0 File Offset: 0x0016A7D0
	public static sprᦪ ᜁ(string A_0)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			string text;
			sprᦪ result;
			for (;;)
			{
				A_0 = A_0.Trim();
				A_0 = A_0.Replace(',', '.');
				int length = A_0.Length;
				int num = 0;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_1A2;
					case 1:
					{
						char c;
						if (c != '+')
						{
							num2 = 26;
							continue;
						}
						goto IL_219;
					}
					case 2:
					{
						char c;
						if (char.IsNumber(c))
						{
							num2 = 24;
							continue;
						}
						goto IL_3A5;
					}
					case 3:
						goto IL_B5;
					case 4:
						num2 = 1;
						continue;
					case 5:
						if (spr᧓.ᜬ == null)
						{
							num2 = 7;
							continue;
						}
						goto IL_3FA;
					case 6:
					{
						string key;
						int num3;
						if (spr᧓.ᜬ.TryGetValue(key, out num3))
						{
							num2 = 16;
							continue;
						}
						goto IL_263;
					}
					case 7:
						spr᧓.ᜬ = new Dictionary<string, int>(6)
						{
							{
								ClipboardData.b("ᕵᕷ", a_),
								0
							},
							{
								ClipboardData.b("ήᙷ", a_),
								1
							},
							{
								ClipboardData.b("᭵ᕷ", a_),
								2
							},
							{
								"",
								3
							},
							{
								ClipboardData.b("ٵ౷", a_),
								4
							},
							{
								ClipboardData.b("ٵ൷", a_),
								5
							}
						};
						num2 = 14;
						continue;
					case 8:
						goto IL_CE;
					case 9:
						num2 = 10;
						continue;
					case 10:
						goto IL_1D2;
					case 11:
					{
						char c;
						if (c != '.')
						{
							num2 = 19;
							continue;
						}
						goto IL_219;
					}
					case 12:
					{
						if (num >= length)
						{
							num2 = 15;
							continue;
						}
						char c = A_0[num];
						num2 = 11;
						continue;
					}
					case 13:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B5;
						default:
							goto IL_258;
						}
						break;
					case 14:
						goto IL_3FA;
					case 15:
						goto IL_3A5;
					case 16:
						num2 = 23;
						continue;
					case 17:
					{
						char c;
						if (c != '-')
						{
							num2 = 4;
							continue;
						}
						goto IL_219;
					}
					case 18:
						goto IL_1EB;
					case 19:
						num2 = 17;
						continue;
					case 20:
						goto IL_2C0;
					case 21:
					{
						string key;
						if ((key = text) != null)
						{
							num2 = 22;
							continue;
						}
						goto IL_263;
					}
					case 22:
						num2 = 5;
						continue;
					case 23:
					{
						int num3;
						switch (num3)
						{
						case 0:
							result.ᜌ = UnitGraphics.Centimeter;
							num2 = 8;
							continue;
						case 1:
							result.ᜌ = UnitGraphics.Inch;
							if (true)
							{
							}
							num2 = 25;
							continue;
						case 2:
							result.ᜌ = UnitGraphics.Millimeter;
							num2 = 0;
							continue;
						case 3:
						case 4:
							result.ᜌ = UnitGraphics.Point;
							num2 = 13;
							continue;
						case 5:
							result.ᜌ = UnitGraphics.Presentation;
							num2 = 18;
							continue;
						default:
							num2 = 9;
							continue;
						}
						break;
					}
					case 24:
						goto IL_219;
					case 25:
						return result;
					case 26:
						num2 = 2;
						continue;
					}
					break;
					IL_219:
					num++;
					num2 = 20;
					continue;
					IL_2C0:
					num2 = 12;
					continue;
					IL_B5:
					goto IL_2C0;
					try
					{
						IL_3A5:
						result.ᜋ = double.Parse(A_0.Substring(0, num).Trim(), CultureInfo.InvariantCulture);
						goto IL_306;
					}
					catch (Exception innerException)
					{
						result.ᜋ = 1.0;
						string message = string.Format(ClipboardData.b("╵౷ࡹᕻၽꊁꎃﶅ뢇ꮋ꺍憐뒓벛ﾝ肟풡얣쪥솧캩貫\ud8ad톯\udeb1솳펵颷\udcb9펻첽뇁냃듅뷇꧉룋믍ꋏ럑胗这닛럝铟엡쫣", a_), A_0);
						throw new ArgumentException(message, innerException);
					}
					goto IL_3FA;
					IL_306:
					text = A_0.Substring(num).Trim().ToLower();
					result.ᜌ = UnitGraphics.Point;
					num2 = 21;
					continue;
					IL_3FA:
					num2 = 6;
				}
			}
			IL_CE:
			IL_1A2:
			return result;
			IL_1D2:
			goto IL_263;
			IL_1EB:
			return result;
			IL_258:
			if (false)
			{
			}
			return result;
			IL_263:
			throw new ArgumentException(ClipboardData.b("⍵ᙷᅹቻᅽꒃ꺍ꊗ몙뮛", a_) + text + ClipboardData.b("兵", a_));
		}
		}
	}

	// Token: 0x06001688 RID: 5768 RVA: 0x0016BC1C File Offset: 0x0016AC1C
	public static sprᦪ ᜀ(int A_0)
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
		sprᦪ result;
		result.ᜋ = (double)A_0;
		result.ᜌ = UnitGraphics.Point;
		return result;
	}

	// Token: 0x06001689 RID: 5769 RVA: 0x0016BC6C File Offset: 0x0016AC6C
	public static sprᦪ ᜀ(double A_0)
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
		sprᦪ result;
		result.ᜋ = A_0;
		result.ᜌ = UnitGraphics.Point;
		return result;
	}

	// Token: 0x0600168A RID: 5770 RVA: 0x0016BCB8 File Offset: 0x0016ACB8
	public static double ᜀ(sprᦪ A_0)
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
		return A_0.ᜂ();
	}

	// Token: 0x0600168B RID: 5771 RVA: 0x0016BCFC File Offset: 0x0016ACFC
	public static bool ᜁ(sprᦪ A_0, sprᦪ A_1)
	{
		while (A_0.ᜌ == A_1.ᜌ)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (false)
			{
			}
			return A_0.ᜋ == A_1.ᜋ;
		}
		if (true)
		{
		}
		return false;
	}

	// Token: 0x0600168C RID: 5772 RVA: 0x0016BD5C File Offset: 0x0016AD5C
	public static bool ᜀ(sprᦪ A_0, sprᦪ A_1)
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
		return !sprᦪ.ᜁ(A_0, A_1);
	}

	// Token: 0x0600168D RID: 5773 RVA: 0x0016BDA4 File Offset: 0x0016ADA4
	public bool ᜀ(object A_0)
	{
		while (A_0 is sprᦪ)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return sprᦪ.ᜁ(this, (sprᦪ)A_0);
		}
		return false;
	}

	// Token: 0x0600168E RID: 5774 RVA: 0x0016BDFC File Offset: 0x0016ADFC
	public int ᜉ()
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
		return this.ᜋ.GetHashCode() ^ this.ᜌ.GetHashCode();
	}

	// Token: 0x0600168F RID: 5775 RVA: 0x0016BE54 File Offset: 0x0016AE54
	public static sprᦪ ᜀ(string A_0)
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
		return sprᦪ.ᜁ(A_0);
	}

	// Token: 0x06001690 RID: 5776 RVA: 0x0016BE98 File Offset: 0x0016AE98
	public void ᜀ(UnitGraphics A_0)
	{
		int a_ = 13;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_112;
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				switch (A_0)
				{
				case UnitGraphics.Point:
					goto IL_F3;
				case UnitGraphics.Inch:
					goto IL_5F;
				case UnitGraphics.Millimeter:
					goto IL_DF;
				case UnitGraphics.Centimeter:
					goto IL_4B;
				case UnitGraphics.Presentation:
					goto IL_CA;
				default:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				break;
			case 4:
				num = 1;
				continue;
			}
			if (this.ᜌ == A_0)
			{
				num = 0;
			}
			else
			{
				num = 3;
			}
		}
		return;
		IL_4B:
		this.ᜋ = this.ᜃ();
		this.ᜌ = UnitGraphics.Centimeter;
		return;
		IL_5F:
		this.ᜋ = this.ᜁ();
		this.ᜌ = UnitGraphics.Inch;
		return;
		IL_CA:
		this.ᜋ = this.ᜅ();
		this.ᜌ = UnitGraphics.Presentation;
		return;
		IL_DF:
		this.ᜋ = this.ᜄ();
		this.ᜌ = UnitGraphics.Millimeter;
		return;
		IL_F3:
		this.ᜋ = this.ᜂ();
		this.ᜌ = UnitGraphics.Point;
		return;
		IL_112:
		throw new ArgumentException(ClipboardData.b("♲᭴ᱶ᝸ᑺ੼ᅾꆀﶈꮊ歷꾔랖뺘", a_) + A_0 + ClipboardData.b("呲", a_));
	}

	// Token: 0x04001A2E RID: 6702
	internal const double ᜀ = 1.0;

	// Token: 0x04001A2F RID: 6703
	internal const double ᜁ = 72.0;

	// Token: 0x04001A30 RID: 6704
	internal const double ᜂ = 2.834645669291339;

	// Token: 0x04001A31 RID: 6705
	internal const double ᜃ = 28.346456692913385;

	// Token: 0x04001A32 RID: 6706
	internal const double ᜄ = 0.75;

	// Token: 0x04001A33 RID: 6707
	internal const double ᜅ = 1.3333333333333333;

	// Token: 0x04001A34 RID: 6708
	internal const double ᜆ = 96.0;

	// Token: 0x04001A35 RID: 6709
	internal const double ᜇ = 3.7795275590551185;

	// Token: 0x04001A36 RID: 6710
	internal const double ᜈ = 37.79527559055118;

	// Token: 0x04001A37 RID: 6711
	internal const double ᜉ = 1.0;

	// Token: 0x04001A38 RID: 6712
	public static readonly sprᦪ ᜊ;

	// Token: 0x04001A39 RID: 6713
	private double ᜋ;

	// Token: 0x04001A3A RID: 6714
	private UnitGraphics ᜌ;
}
