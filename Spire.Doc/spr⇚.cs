using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x020001EC RID: 492
internal class spr\u21DA
{
	// Token: 0x06001587 RID: 5511 RVA: 0x0015DBD4 File Offset: 0x0015CBD4
	public virtual bool ᜀ(object A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return false;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A1;
				default:
					if (false)
					{
					}
					if (A_0.GetType() != typeof(spr\u21DA))
					{
						num = 0;
						continue;
					}
					goto IL_B7;
				}
				break;
			case 3:
				return true;
			case 4:
				goto IL_39;
			case 5:
				goto IL_A1;
			}
			if (object.ReferenceEquals(null, A_0))
			{
				num = 4;
				continue;
			}
			num = 5;
			continue;
			IL_A1:
			if (object.ReferenceEquals(this, A_0))
			{
				num = 3;
			}
			else
			{
				num = 1;
			}
		}
		IL_39:
		if (true)
		{
		}
		return false;
		IL_B7:
		return this.Equals((spr\u21DA)A_0);
	}

	// Token: 0x06001588 RID: 5512 RVA: 0x0015DCA4 File Offset: 0x0015CCA4
	public virtual int ᜃ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_7D;
			case 2:
				goto IL_4C;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (this.ᜉ == null)
			{
				num = 3;
			}
			else
			{
				num = 2;
			}
		}
		IL_4C:
		int num2 = this.ᜉ.GetHashCode();
		goto IL_80;
		IL_7D:
		num2 = 0;
		IL_80:
		int num3 = num2;
		num3 = (num3 * 397 ^ (int)this.ᜊ);
		return num3 * 397 ^ (int)this.ᜋ;
	}

	// Token: 0x06001589 RID: 5513 RVA: 0x0015DD54 File Offset: 0x0015CD54
	internal static spr\u21DA ᜈ(string A_0)
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
		return new spr\u21DA(A_0, CssValueType.Identifier, CssUnitType.None);
	}

	// Token: 0x0600158A RID: 5514 RVA: 0x0015DD98 File Offset: 0x0015CD98
	internal static spr\u21DA ᜇ(string A_0)
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
		return new spr\u21DA(A_0, CssValueType.String, CssUnitType.None);
	}

	// Token: 0x0600158B RID: 5515 RVA: 0x0015DDDC File Offset: 0x0015CDDC
	internal static spr\u21DA ᜁ(double A_0)
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
		return new spr\u21DA(A_0, CssValueType.Number, CssUnitType.None);
	}

	// Token: 0x0600158C RID: 5516 RVA: 0x0015DE24 File Offset: 0x0015CE24
	internal static spr\u21DA ᜀ(double A_0)
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
		return new spr\u21DA(A_0, CssValueType.Percentage, CssUnitType.None);
	}

	// Token: 0x0600158D RID: 5517 RVA: 0x0015DE6C File Offset: 0x0015CE6C
	internal static spr\u21DA ᜂ(double A_0, CssUnitType A_1)
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
		return new spr\u21DA(A_0, CssValueType.Length, A_1);
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x0015DEB4 File Offset: 0x0015CEB4
	internal static spr\u21DA ᜁ(sprᨢ A_0)
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
		return new spr\u21DA(A_0, CssValueType.Complex, CssUnitType.None);
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x0015DEF8 File Offset: 0x0015CEF8
	internal static spr\u21DA ᜀ(double A_0, double A_1, double A_2, double A_3, CssUnitType A_4)
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
		return spr\u21DA.ᜀ(new sprᨢ(4)
		{
			spr\u21DA.ᜂ(A_0, A_4),
			spr\u21DA.ᜂ(A_1, A_4),
			spr\u21DA.ᜂ(A_2, A_4),
			spr\u21DA.ᜂ(A_3, A_4)
		});
	}

	// Token: 0x06001590 RID: 5520 RVA: 0x0015DF7C File Offset: 0x0015CF7C
	internal static spr\u21DA ᜀ(sprᨢ A_0)
	{
		sprᨢ sprᨢ;
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_191:
				num = 12;
				break;
			default:
				if (false)
				{
				}
				sprᨢ = new sprᨢ(A_0);
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_108;
				case 1:
					num = 8;
					continue;
				case 2:
					if (sprᨢ.Count == 4)
					{
						num = 16;
						continue;
					}
					goto IL_1A6;
				case 3:
					goto IL_E6;
				case 4:
					sprᨢ.RemoveAt(3);
					num = 6;
					continue;
				case 5:
					num = 15;
					continue;
				case 6:
					goto IL_1A6;
				case 7:
					if (sprᨢ.Count == 3)
					{
						num = 1;
						continue;
					}
					goto IL_A4;
				case 8:
					if (sprᨢ.ᜀ(0).Equals(sprᨢ.ᜀ(2)))
					{
						goto IL_191;
					}
					goto IL_A4;
				case 9:
					sprᨢ.RemoveAt(1);
					num = 3;
					continue;
				case 10:
					if (sprᨢ.Count != 1)
					{
						num = 0;
						continue;
					}
					goto IL_1E1;
				case 11:
					goto IL_A4;
				case 12:
					if (true)
					{
					}
					sprᨢ.RemoveAt(2);
					num = 11;
					continue;
				case 13:
					if (sprᨢ.ᜀ(1).Equals(sprᨢ.ᜀ(3)))
					{
						num = 4;
						continue;
					}
					goto IL_1A6;
				case 14:
					if (sprᨢ.Count == 2)
					{
						num = 5;
						continue;
					}
					goto IL_E6;
				case 15:
					if (sprᨢ.ᜀ(0).Equals(sprᨢ.ᜀ(1)))
					{
						num = 9;
						continue;
					}
					goto IL_E6;
				case 16:
					num = 13;
					continue;
				}
				break;
				IL_A4:
				num = 14;
				continue;
				IL_E6:
				num = 10;
				continue;
				IL_1A6:
				num = 7;
			}
		}
		IL_108:
		return spr\u21DA.ᜁ(sprᨢ);
		IL_1E1:
		return new spr\u21DA(sprᨢ.ᜀ(0).ᜈ(), sprᨢ.ᜀ(0).ᜆ(), sprᨢ.ᜀ(0).ᜅ());
	}

	// Token: 0x06001591 RID: 5521 RVA: 0x0015E194 File Offset: 0x0015D194
	internal static spr\u21DA ᜀ(string A_0, string A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 1;
			sprᨢ sprᨢ;
			for (;;)
			{
				IEnumerator enumerator;
				Regex u170D;
				switch (num)
				{
				case 0:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 3;
								continue;
							case 2:
							{
								if (!enumerator.MoveNext())
								{
									num = 0;
									continue;
								}
								Match match = (Match)enumerator.Current;
								sprᨢ.Add(new spr\u21DA(match.Value));
								num = 4;
								continue;
							}
							case 3:
								goto IL_E8;
							}
							IL_C2:
							num = 2;
							continue;
							goto IL_C2;
						}
						IL_E8:
						goto IL_146;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable = enumerator as IDisposable;
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									disposable.Dispose();
									num = 2;
									continue;
								case 1:
									if (disposable != null)
									{
										num = 0;
										continue;
									}
									goto IL_132;
								case 2:
									goto IL_130;
								}
								break;
							}
						}
						IL_130:
						IL_132:;
					}
					goto IL_133;
				case 2:
					if (!spr\u1CC6.ᜀ(A_0, ClipboardData.b("ཨѪͬ᭮屰ᕲᑴ᩶ၸ᝺Ѽ", a_)))
					{
						num = 6;
						continue;
					}
					num = 4;
					continue;
				case 3:
					IL_13F:
					u170D = spr\u21DA.\u170D;
					goto IL_190;
				case 4:
					u170D = spr\u21DA.ᜎ;
					goto IL_190;
				case 5:
					num = 2;
					continue;
				case 6:
					goto IL_133;
				}
				if (spr\u21DA.ᜀ(A_0))
				{
					num = 5;
					continue;
				}
				goto IL_1DA;
				IL_133:
				num = 3;
				continue;
				IL_190:
				Regex regex = u170D;
				MatchCollection matchCollection = regex.Matches(A_1);
				sprᨢ = new sprᨢ(matchCollection.Count);
				enumerator = matchCollection.GetEnumerator();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_13F;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
			IL_146:
			return spr\u21DA.ᜁ(sprᨢ);
			IL_1DA:
			return new spr\u21DA(A_1);
		}
		}
	}

	// Token: 0x06001592 RID: 5522 RVA: 0x0015E394 File Offset: 0x0015D394
	private spr\u21DA(object A_0, CssValueType A_1, CssUnitType A_2)
	{
		this.ᜉ = A_0;
		this.ᜊ = A_1;
		this.ᜋ = A_2;
	}

	// Token: 0x06001593 RID: 5523 RVA: 0x0015E3BC File Offset: 0x0015D3BC
	private spr\u21DA(string A_0)
	{
		if (this.ᜆ(A_0))
		{
			return;
		}
		string a_ = A_0.ToLower();
		if (this.ᜅ(a_))
		{
			return;
		}
		if (this.ᜃ(a_))
		{
			return;
		}
		if (this.ᜂ(a_))
		{
			return;
		}
		if (this.ᜁ(a_))
		{
			return;
		}
		this.ᜉ = A_0;
		this.ᜊ = CssValueType.Identifier;
	}

	// Token: 0x06001594 RID: 5524 RVA: 0x0015E420 File Offset: 0x0015D420
	private bool ᜆ(string A_0)
	{
		int a_ = 7;
		int num = 5;
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
				goto IL_B9;
			case 2:
				if (A_0.StartsWith(ClipboardData.b("佬", a_)))
				{
					goto IL_DC;
				}
				return false;
			case 3:
				goto IL_81;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_DC;
				default:
					if (false)
					{
					}
					if (A_0.Length > 1)
					{
						num = 1;
						continue;
					}
					return false;
				}
				break;
			}
			if (!A_0.StartsWith(ClipboardData.b("䩬", a_)))
			{
				num = 0;
				continue;
			}
			IL_81:
			num = 4;
			continue;
			IL_DC:
			num = 3;
		}
		IL_B9:
		this.ᜉ = A_0.Substring(1, A_0.Length - 2);
		this.ᜊ = CssValueType.String;
		return true;
	}

	// Token: 0x06001595 RID: 5525 RVA: 0x0015E518 File Offset: 0x0015D518
	private bool ᜅ(string A_0)
	{
		Color color;
		for (;;)
		{
			color = spr\u21DA.ᜄ(A_0);
			if (!color.IsEmpty)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_4F;
			}
		}
		if (true)
		{
		}
		this.ᜉ = color;
		this.ᜊ = CssValueType.Color;
		return true;
		IL_4F:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x0015E57C File Offset: 0x0015D57C
	internal static Color ᜄ(string A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			Match match;
			string value;
			for (;;)
			{
				A_0 = A_0.ToLower();
				match = spr\u21DA.ᜇ.Match(A_0);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_245;
						}
						if (true)
						{
						}
						if (false)
						{
						}
						Group group = match.Groups[1];
						num = 3;
						continue;
					}
					case 1:
						if (match.Success)
						{
							num = 0;
							continue;
						}
						num = 4;
						continue;
					case 2:
						goto IL_1DB;
					case 3:
					{
						Group group;
						if (group.Success)
						{
							num = 7;
							continue;
						}
						goto IL_A8;
					}
					case 4:
						if (A_0.EndsWith(ClipboardData.b("ᕱٳ፵ŷ", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_257;
					case 5:
						A_0 = A_0.Substring(0, A_0.Length - ClipboardData.b("ᕱٳ፵ŷ", a_).Length) + ClipboardData.b("ᕱٳ᝵ŷ", a_);
						num = 2;
						continue;
					case 6:
						if (value.Length == 6)
						{
							num = 8;
							continue;
						}
						goto IL_12F;
					case 7:
					{
						Group group;
						value = group.Value;
						num = 6;
						continue;
					}
					case 8:
						goto IL_194;
					}
					break;
				}
			}
			IL_A8:
			int red = spr\u21DA.ᜀ(match.Groups[2].Value, match.Groups[3].Success);
			int green = spr\u21DA.ᜀ(match.Groups[4].Value, match.Groups[5].Success);
			int blue = spr\u21DA.ᜀ(match.Groups[6].Value, match.Groups[7].Success);
			return Color.FromArgb(red, green, blue);
			IL_12F:
			int num2 = spr\u1CC6.ᜂ(value[0]);
			int num3 = spr\u1CC6.ᜂ(value[1]);
			int num4 = spr\u1CC6.ᜂ(value[2]);
			return Color.FromArgb(num2 * 17, num3 * 17, num4 * 17);
			IL_194:
			goto IL_245;
			IL_1DB:
			goto IL_257;
			IL_245:
			return Color.FromArgb(-16777216 + sprᜌ.ᜄ(value));
			IL_257:
			return Color.FromName(A_0);
		}
		}
	}

	// Token: 0x06001597 RID: 5527 RVA: 0x0015E7E8 File Offset: 0x0015D7E8
	private static int ᜀ(string A_0, bool A_1)
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
		double a_ = sprᜌ.ᜏ(A_0) * (A_1 ? 2.55 : 1.0);
		return spr\u2109.ᜂ(spr\u2109.ᜁ(a_, 0.0, 255.0));
	}

	// Token: 0x06001598 RID: 5528 RVA: 0x0015E864 File Offset: 0x0015D864
	private bool ᜃ(string A_0)
	{
		int a_ = 12;
		int num = 7;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				return true;
			case 1:
				if (this.ᜀ(A_0, ClipboardData.b("ɱs", a_), CssUnitType.Pt))
				{
					num = 5;
					continue;
				}
				num = 3;
				continue;
			case 2:
				if (this.ᜀ(A_0, ClipboardData.b("ᅱᥳ", a_), CssUnitType.Cm))
				{
					num = 11;
					continue;
				}
				goto IL_16E;
			case 3:
				if (!this.ᜀ(A_0, ClipboardData.b("ɱᝳ", a_), CssUnitType.Pc))
				{
					num = 10;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_16E;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 4:
				return true;
			case 5:
				return true;
			case 6:
				if (this.ᜀ(A_0, ClipboardData.b("άᥳ", a_), CssUnitType.Mm))
				{
					num = 0;
					continue;
				}
				num = 1;
				continue;
			case 8:
				return true;
			case 9:
				return true;
			case 10:
				if (this.ᜀ(A_0, ClipboardData.b("ɱ౳", a_), CssUnitType.Px))
				{
					num = 8;
					continue;
				}
				return false;
			case 11:
				return true;
			}
			if (this.ᜀ(A_0, ClipboardData.b("᭱ᩳ", a_), CssUnitType.In))
			{
				num = 9;
				continue;
			}
			num = 2;
			continue;
			IL_16E:
			num = 6;
		}
		return true;
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x0015EA14 File Offset: 0x0015DA14
	private bool ᜀ(string A_0, string A_1, CssUnitType A_2)
	{
		int num = 3;
		double num2;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (!double.IsNaN(num2))
				{
					num = 1;
					continue;
				}
				return false;
			case 1:
				goto IL_BA;
			case 2:
			{
				string a_ = A_0.Substring(0, A_0.Length - A_1.Length);
				num2 = sprᜌ.\u170D(a_);
				num = 0;
				continue;
			}
			}
			while (A_0.EndsWith(A_1))
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
					num = 2;
					goto IL_0A;
				}
			}
			return false;
		}
		IL_BA:
		this.ᜉ = num2;
		this.ᜊ = CssValueType.Length;
		this.ᜋ = A_2;
		return true;
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x0015EAE0 File Offset: 0x0015DAE0
	private bool ᜂ(string A_0)
	{
		int a_ = 1;
		int num = 1;
		double num2;
		for (;;)
		{
			IL_13:
			switch (num)
			{
			case 0:
				if (!double.IsNaN(num2))
				{
					num = 2;
					continue;
				}
				return false;
			case 2:
				goto IL_C4;
			case 3:
			{
				string a_2 = A_0.Substring(0, A_0.Length - 1);
				num2 = sprᜌ.\u170D(a_2);
				num = 0;
				continue;
			}
			}
			while (A_0.EndsWith(ClipboardData.b("䉦", a_)))
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
					num = 3;
					goto IL_13;
				}
			}
			return false;
		}
		IL_C4:
		this.ᜉ = num2;
		this.ᜊ = CssValueType.Percentage;
		return true;
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x0015EBB4 File Offset: 0x0015DBB4
	private bool ᜁ(string A_0)
	{
		double num;
		for (;;)
		{
			num = sprᜌ.\u170D(A_0);
			if (!double.IsNaN(num))
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_46;
			}
		}
		this.ᜉ = num;
		this.ᜊ = CssValueType.Number;
		return true;
		IL_46:
		if (false)
		{
		}
		if (true)
		{
		}
		return false;
	}

	// Token: 0x0600159C RID: 5532 RVA: 0x0015EC18 File Offset: 0x0015DC18
	internal void ᜀ(StringBuilder A_0)
	{
		int a_ = 0;
		string text;
		for (;;)
		{
			if (true)
			{
			}
			CssValueType cssValueType = this.ᜊ;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_194;
				case 1:
					if (text.IndexOf(' ') >= 0)
					{
						num = 0;
						continue;
					}
					goto IL_113;
				case 2:
					return;
				case 3:
					switch (cssValueType)
					{
					case CssValueType.Identifier:
						goto IL_148;
					case CssValueType.Complex:
						goto IL_E9;
					case CssValueType.ComplexCommaSeparated:
						goto IL_156;
					case CssValueType.String:
						text = this.ᜂ();
						num = 1;
						continue;
					case CssValueType.Color:
						goto IL_6B;
					case CssValueType.Number:
						goto IL_FF;
					case CssValueType.Length:
						goto IL_11C;
					case CssValueType.Percentage:
						goto IL_199;
					default:
						num = 2;
						continue;
					}
					break;
				}
				break;
			}
		}
		return;
		IL_6B:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_FF:
			A_0.Append(sprᜌ.ᜁ(this.ᜉ()));
			return;
		default:
		{
			if (false)
			{
			}
			Color color = this.ᜄ();
			A_0.AppendFormat(ClipboardData.b("䕥፧婩噫᙭䉯ཱཱི䝵䉷ɹ乻ͽﭿ낁뺃ﺅ몇", a_), color.R, color.G, color.B);
			return;
		}
		}
		IL_E9:
		this.ᜀ(A_0, ClipboardData.b("䙥", a_));
		return;
		IL_113:
		A_0.Append(text);
		return;
		IL_11C:
		A_0.AppendFormat(ClipboardData.b("ᵥ塧ᝩᝫ彭൯", a_), sprᜌ.ᜁ(this.ᜉ()), spr\u21DA.ᜀ(this.ᜋ));
		return;
		IL_148:
		A_0.Append(this.ᜉ);
		return;
		IL_156:
		this.ᜀ(A_0, ClipboardData.b("䩥䡧", a_));
		return;
		IL_194:
		A_0.AppendFormat(ClipboardData.b("䅥፧婩ᅫ䥭", a_), text);
		return;
		IL_199:
		A_0.AppendFormat(ClipboardData.b("ᵥ塧ᝩ䥫", a_), sprᜌ.ᜁ(this.ᜉ()));
	}

	// Token: 0x0600159D RID: 5533 RVA: 0x0015EDE0 File Offset: 0x0015DDE0
	private void ᜀ(StringBuilder A_0, string A_1)
	{
		for (;;)
		{
			sprᨢ sprᨢ = this.ᜀ();
			int num = 5;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_61;
				case 1:
					sprᨢ.ᜀ(0).ᜀ(A_0);
					num2 = 1;
					num = 0;
					continue;
				case 2:
					goto IL_69;
				case 3:
					goto IL_61;
				case 4:
					goto IL_7A;
				case 5:
					if (sprᨢ.Count <= 0)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_69;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				break;
				IL_61:
				num = 2;
				continue;
				IL_69:
				if (num2 >= sprᨢ.Count)
				{
					num = 4;
				}
				else
				{
					A_0.Append(A_1);
					sprᨢ.ᜀ(num2).ᜀ(A_0);
					num2++;
					num = 3;
				}
			}
		}
		IL_7A:
		if (true)
		{
		}
	}

	// Token: 0x0600159E RID: 5534 RVA: 0x0015EEC0 File Offset: 0x0015DEC0
	private static string ᜀ(CssUnitType A_0)
	{
		int a_ = 6;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_BC;
				case 1:
					switch (A_0)
					{
					case CssUnitType.In:
						goto IL_93;
					case CssUnitType.Cm:
						goto IL_FD;
					case CssUnitType.Mm:
						goto IL_EE;
					case CssUnitType.Pt:
						goto IL_84;
					case CssUnitType.Pc:
						goto IL_BE;
					case CssUnitType.Px:
						goto IL_75;
					case CssUnitType.Em:
						goto IL_A2;
					case CssUnitType.Ex:
						goto IL_CD;
					default:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_84;
						default:
							if (false)
							{
							}
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
		return ClipboardData.b("ᱫ᙭", a_);
		IL_84:
		return ClipboardData.b("ᱫᩭ", a_);
		IL_93:
		return ClipboardData.b("իm", a_);
		IL_A2:
		return ClipboardData.b("५ͭ", a_);
		IL_BC:
		return "";
		IL_BE:
		return ClipboardData.b("ᱫ൭", a_);
		IL_CD:
		if (true)
		{
		}
		return ClipboardData.b("५᙭", a_);
		IL_EE:
		return ClipboardData.b("ūͭ", a_);
		IL_FD:
		return ClipboardData.b("ཫͭ", a_);
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x0015EFE0 File Offset: 0x0015DFE0
	internal string ᜂ()
	{
		int num = 2;
		for (;;)
		{
			IL_12:
			switch (num)
			{
			case 0:
				while (this.ᜊ == CssValueType.String)
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
				goto IL_87;
			case 1:
				goto IL_85;
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				num = 0;
				continue;
			}
			if (this.ᜊ == CssValueType.Identifier)
			{
				break;
			}
			num = 3;
		}
		IL_3A:
		return (string)this.ᜉ;
		IL_85:
		goto IL_3A;
		IL_87:
		return string.Empty;
	}

	// Token: 0x060015A0 RID: 5536 RVA: 0x0015F07C File Offset: 0x0015E07C
	internal sprᨢ ᜀ()
	{
		int num = 2;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 0:
				while (this.ᜊ == CssValueType.ComplexCommaSeparated)
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
						num = 1;
						goto IL_0A;
					}
				}
				goto IL_88;
			case 1:
				goto IL_86;
			case 3:
				num = 0;
				continue;
			}
			if (this.ᜊ == CssValueType.Complex)
			{
				break;
			}
			num = 3;
		}
		IL_33:
		return (sprᨢ)this.ᜉ;
		IL_86:
		goto IL_33;
		IL_88:
		return null;
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x0015F114 File Offset: 0x0015E114
	internal double ᜉ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜊ == CssValueType.Percentage)
				{
					goto IL_3B;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_31;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 2:
				goto IL_6A;
			case 3:
				num = 4;
				continue;
			case 4:
				if (this.ᜊ == CssValueType.Length)
				{
					num = 2;
					continue;
				}
				goto IL_A6;
			case 5:
				num = 0;
				continue;
			}
			goto IL_28;
			IL_31:
			num = 5;
			continue;
			IL_28:
			if (this.ᜊ != CssValueType.Number)
			{
				goto IL_31;
			}
			break;
		}
		IL_3B:
		return (double)this.ᜉ;
		IL_6A:
		goto IL_3B;
		IL_A6:
		if (true)
		{
		}
		return 0.0;
	}

	// Token: 0x060015A2 RID: 5538 RVA: 0x0015F1D8 File Offset: 0x0015E1D8
	internal double ᜁ(CssUnitType A_0)
	{
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_31;
				case 2:
					if (this.ᜊ == CssValueType.Number)
					{
						num = 3;
						continue;
					}
					goto IL_B8;
				case 3:
					goto IL_8F;
				}
				if (this.ᜊ == CssValueType.Length)
				{
					num = 0;
				}
				else
				{
					num = 2;
				}
			}
			IL_31:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_51;
			}
		}
		IL_51:
		if (false)
		{
		}
		double a_ = spr\u21DA.ᜁ((double)this.ᜉ, this.ᜋ);
		return spr\u21DA.ᜀ(a_, A_0);
		IL_8F:
		if (true)
		{
		}
		double a_2 = spr\u21DA.ᜁ((double)this.ᜉ, CssUnitType.Px);
		return spr\u21DA.ᜀ(a_2, A_0);
		IL_B8:
		return 0.0;
	}

	// Token: 0x060015A3 RID: 5539 RVA: 0x0015F2A8 File Offset: 0x0015E2A8
	internal double ᜀ(CssUnitType A_0, double A_1)
	{
		if (this.ᜊ != CssValueType.Percentage)
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
				return this.ᜁ(A_0);
			}
		}
		return (double)this.ᜉ * A_1 / 100.0;
	}

	// Token: 0x060015A4 RID: 5540 RVA: 0x0015F310 File Offset: 0x0015E310
	internal Color ᜄ()
	{
		if (this.ᜊ == CssValueType.Color)
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
				return (Color)this.ᜉ;
			}
		}
		return Color.Black;
	}

	// Token: 0x060015A5 RID: 5541 RVA: 0x0015F368 File Offset: 0x0015E368
	internal object ᜈ()
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
		return this.ᜉ;
	}

	// Token: 0x060015A6 RID: 5542 RVA: 0x0015F3AC File Offset: 0x0015E3AC
	internal CssValueType ᜆ()
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

	// Token: 0x060015A7 RID: 5543 RVA: 0x0015F3F0 File Offset: 0x0015E3F0
	internal CssUnitType ᜅ()
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
		return this.ᜋ;
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x0015F434 File Offset: 0x0015E434
	internal bool ᜇ()
	{
		if (this.ᜊ != CssValueType.Length)
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
				return this.ᜊ == CssValueType.Number;
			}
		}
		return true;
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x0015F488 File Offset: 0x0015E488
	internal bool ᜁ()
	{
		int num = 1;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 0:
				while (this.ᜊ != CssValueType.Number)
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
				return true;
			case 2:
				num = 0;
				continue;
			case 3:
				goto IL_84;
			}
			if (true)
			{
			}
			if (this.ᜊ == CssValueType.Length)
			{
				return true;
			}
			num = 2;
		}
		IL_84:
		return this.ᜊ == CssValueType.Percentage;
	}

	// Token: 0x060015AA RID: 5546 RVA: 0x0015F51C File Offset: 0x0015E51C
	private static double ᜁ(double A_0, CssUnitType A_1)
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
		return A_0 / spr\u21DA.ᜈ[(int)A_1];
	}

	// Token: 0x060015AB RID: 5547 RVA: 0x0015F560 File Offset: 0x0015E560
	private static double ᜀ(double A_0, CssUnitType A_1)
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
		return A_0 * spr\u21DA.ᜈ[(int)A_1];
	}

	// Token: 0x060015AC RID: 5548 RVA: 0x0015F5A4 File Offset: 0x0015E5A4
	private static bool ᜀ(string A_0)
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
		return spr\u21DA.ᜌ.Contains(A_0);
	}

	// Token: 0x060015AD RID: 5549 RVA: 0x0015F5EC File Offset: 0x0015E5EC
	static spr\u21DA()
	{
		int a_ = 0;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u21DA.ᜇ = new Regex(ClipboardData.b("㡥䁧啩噫䙭佯䡱坳幵⍷䩹养䝽꾁\udb85릉ꚍ꾏ꢑ쾓ꚕ떗ꎙﶛ뎝욟ﾡ\udfa3閥햧莩鎫螭馯캱鲳覵芷좹\udbbb\udcbd鲿飃뗅韋﯏近諕볗\udadf뻡쫣뫥賧샩엫퇭\ud9ef\udaf1퇳\udff5쟷꛹迻퓽⳿币眃Ⰵ\u2007儉ℋ┍䴏ⴑ䠓爕㌗㈙⌛␝簟డ砣䈥ȧ̩ጫܭᠯᜱᴳव搷䤹ᘻሽ᰿ㅁ湃湅ፇ杉杋ፍ潏๑こ絕灗教晛ɝ也㹡c䱥䅧啩䕫䙭啯孱䭳⩵୷偹⁻坽꥿ꮁꂃ", a_), RegexOptions.Compiled);
		spr\u21DA.ᜈ = new double[]
		{
			1.0,
			1.0,
			2.54,
			25.4,
			72.0,
			6.0,
			96.0
		};
		spr\u21DA.\u170D = new Regex(ClipboardData.b("ᑥཧࡩに䙭⭯ⱱ⡳彵╷偹⁻坽ﱿ풇ꊉ힋킍첏뮑즓법쒗뎙벝ﮟﲡ蚣ﮥ芧袩킫覭鎳銷鶹삻麿黁럃韉", a_), RegexOptions.IgnoreCase);
		spr\u21DA.ᜎ = new Regex(ClipboardData.b("䑥㍧㑩乫㍭婯偱ࡳ兵⍷⑹孻⍽ꩿꖁ\udd85횇ꚉ톋ꖍ", a_));
		spr\u21DA.ᜌ = sprᡕ.ᜀ();
		spr\u21DA.ᜌ.Add(ClipboardData.b("ѥ१३ݫ७ɯᵱųᡵᱷ坹౻ᅽ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ѥ१३ݫ७ɯᵱųᡵᱷ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ѥݧᡩ࡫୭ɯ影ᝳ᥵ᑷᕹ๻", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ѥݧᡩ࡫୭ɯ影ݳٵ᥷᥹ᕻၽ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ѥݧᡩ࡫୭ɯ影ݳɵŷᙹ᥻", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ѥݧᡩ࡫୭ɯ影s᥵ࡷ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ѥݧᡩ࡫୭ɯ影ٳήίቹࡻ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ѥݧᡩ࡫୭ɯ影ᙳ᥵౷๹፻፽", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ѥݧᡩ࡫୭ɯ影ᡳ፵ṷ๹", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ѥݧᡩ࡫୭ɯ影ͳήᱷ๹ᑻ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ѥݧᡩ࡫୭ɯ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("եݧѩᡫ୭ṯٱ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("եݧὩɫᩭᕯq女ήᙷ᥹๻᭽", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("եݧὩɫᩭᕯq女ѵᵷॹ᥻੽", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("եᵧཀྵ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("եᵧᡩὫŭɯ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("eݧѩᡫ䍭ᙯ፱ᥳήᑷ͹", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("eݧѩᡫ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("੥ŧᥩᡫ䍭ͯٱ൳᩵ᵷ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("୥१ᡩ୫ݭṯ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("॥ᵧṩkݭṯ᝱", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ᙥ१๩࡫ݭṯᕱ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ᙥ१ὩὫ୭", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ᙥѧ୩ᕫ䍭ᑯݱٳήᙷᵹ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ᝥᵧթᡫ୭ͯ", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ᕥŧၩ५", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ብ൧ቩᡫ䍭ᑯ᝱ᝳ᥵੷᭹ࡻ᝽", a_), null);
		spr\u21DA.ᜌ.Add(ClipboardData.b("ၥݧͩཫ୭嵯ᑱᕳ᭵ᅷᙹջ", a_), null);
	}

	// Token: 0x040019CC RID: 6604
	private const int ᜀ = 1;

	// Token: 0x040019CD RID: 6605
	private const int ᜁ = 2;

	// Token: 0x040019CE RID: 6606
	private const int ᜂ = 3;

	// Token: 0x040019CF RID: 6607
	private const int ᜃ = 4;

	// Token: 0x040019D0 RID: 6608
	private const int ᜄ = 5;

	// Token: 0x040019D1 RID: 6609
	private const int ᜅ = 6;

	// Token: 0x040019D2 RID: 6610
	private const int ᜆ = 7;

	// Token: 0x040019D3 RID: 6611
	private static readonly Regex ᜇ;

	// Token: 0x040019D4 RID: 6612
	private static readonly double[] ᜈ;

	// Token: 0x040019D5 RID: 6613
	private object ᜉ;

	// Token: 0x040019D6 RID: 6614
	private CssValueType ᜊ;

	// Token: 0x040019D7 RID: 6615
	private CssUnitType ᜋ;

	// Token: 0x040019D8 RID: 6616
	private static readonly IDictionary ᜌ;

	// Token: 0x040019D9 RID: 6617
	private static readonly Regex \u170D;

	// Token: 0x040019DA RID: 6618
	private static readonly Regex ᜎ;
}
