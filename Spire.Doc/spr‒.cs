using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;

// Token: 0x0200043D RID: 1085
internal class spr\u2012
{
	// Token: 0x06003C8E RID: 15502 RVA: 0x0038814C File Offset: 0x0038714C
	internal static TableAlignment ᜃ(string A_0)
	{
		int a_ = 12;
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_0 == ClipboardData.b("ᅱᅳᡵ౷ό๻", a_)))
				{
					num = 5;
					continue;
				}
				return TableAlignment.Center;
			case 1:
				num = 3;
				continue;
			case 2:
				num = 0;
				continue;
			case 3:
				goto IL_73;
			case 4:
				num = 9;
				continue;
			case 5:
				num = 7;
				continue;
			case 6:
				if (true)
				{
				}
				if (!(A_0 == ClipboardData.b("űs᝵੷๹", a_)))
				{
					num = 2;
					continue;
				}
				return TableAlignment.Left;
			case 7:
				if (!(A_0 == ClipboardData.b("qᵳᅵၷ๹", a_)))
				{
					num = 8;
					continue;
				}
				return TableAlignment.Right;
			case 8:
				num = 11;
				continue;
			case 9:
				if (!(A_0 == ClipboardData.b("ṱᅳၵ౷", a_)))
				{
					num = 12;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_100;
				default:
					goto IL_154;
				}
				break;
			case 11:
				if (!(A_0 == ClipboardData.b("᝱ᩳት", a_)))
				{
					goto IL_100;
				}
				return TableAlignment.Right;
			case 12:
				num = 6;
				continue;
			}
			if (A_0 != null)
			{
				num = 4;
				continue;
			}
			return TableAlignment.Left;
			IL_100:
			num = 1;
		}
		return TableAlignment.Left;
		IL_73:
		return TableAlignment.Left;
		IL_154:
		if (false)
		{
		}
		return TableAlignment.Left;
	}

	// Token: 0x06003C8F RID: 15503 RVA: 0x003882EC File Offset: 0x003872EC
	internal static string ᜀ(TableAlignment A_0)
	{
		int a_ = 8;
		for (;;)
		{
			IL_39:
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_67;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						switch (A_0)
						{
						case TableAlignment.Left:
							goto IL_76;
						case TableAlignment.Center:
							goto IL_67;
						case TableAlignment.Right:
							goto IL_92;
						default:
							num = 2;
							continue;
						}
						break;
					case 1:
						goto IL_90;
					case 2:
						num = 1;
						continue;
					}
					goto IL_39;
				}
			}
		}
		IL_67:
		return ClipboardData.b("൭ᕯᱱs፵੷", a_);
		IL_76:
		return ClipboardData.b("ɭᕯᑱs", a_);
		IL_90:
		return "";
		IL_92:
		return ClipboardData.b("ᱭ᥯ᕱᱳɵ", a_);
	}

	// Token: 0x06003C90 RID: 15504 RVA: 0x003883AC File Offset: 0x003873AC
	internal static HeightRule ᜂ(string A_0)
	{
		int a_ = 2;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_0 == ClipboardData.b("१ṩ䅫ɭᕯ፱ݳɵ", a_)))
				{
					num = 8;
					continue;
				}
				return HeightRule.AtLeast;
			case 1:
				if (!(A_0 == ClipboardData.b("१ṩ⁫୭ᅯűs", a_)))
				{
					num = 7;
					continue;
				}
				return HeightRule.AtLeast;
			case 2:
				num = 1;
				continue;
			case 4:
				if (!(A_0 == ClipboardData.b("१Ὡᡫŭ", a_)))
				{
					num = 6;
					continue;
				}
				return HeightRule.Auto;
			case 5:
				if (!(A_0 == ClipboardData.b("൧ቩ൫൭ѯ", a_)))
				{
					num = 2;
					continue;
				}
				return HeightRule.Exactly;
			case 6:
				num = 5;
				continue;
			case 7:
				num = 0;
				continue;
			case 8:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return HeightRule.Auto;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 9:
				num = 4;
				continue;
			case 10:
				goto IL_7D;
			}
			if (A_0 == null)
			{
				return HeightRule.Auto;
			}
			num = 9;
		}
		return HeightRule.Auto;
		IL_7D:
		return HeightRule.Auto;
	}

	// Token: 0x06003C91 RID: 15505 RVA: 0x00388510 File Offset: 0x00387510
	internal static string ᜀ(HeightRule A_0, bool A_1)
	{
		int a_ = 17;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_DB;
				case 1:
					switch (A_0)
					{
					case HeightRule.AtLeast:
						num = 4;
						continue;
					case HeightRule.Exactly:
						goto IL_76;
					case HeightRule.Auto:
						goto IL_A1;
					default:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_92;
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
					num = 3;
					continue;
				case 3:
					goto IL_90;
				case 4:
					if (true)
					{
					}
					if (!A_1)
					{
						num = 0;
						continue;
					}
					goto IL_92;
				}
				break;
			}
		}
		IL_76:
		return ClipboardData.b("ቶŸ᩺Ṽ୾", a_);
		IL_90:
		return "";
		IL_92:
		return ClipboardData.b("ᙶ൸㝺᡼Ṿ", a_);
		IL_A1:
		return ClipboardData.b("ᙶ౸ེቼ", a_);
		IL_DB:
		return ClipboardData.b("ᙶ൸噺ᅼ᩾", a_);
	}

	// Token: 0x06003C92 RID: 15506 RVA: 0x00388600 File Offset: 0x00387600
	internal static CellMerge ᜁ(string A_0)
	{
		int a_ = 15;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 1:
				if (!(A_0 == ClipboardData.b("ᙴᡶ᝸ེᑼᅾ", a_)))
				{
					if (true)
					{
					}
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_AA;
				default:
					goto IL_5D;
				}
				break;
			case 3:
				num = 1;
				continue;
			case 4:
				goto IL_77;
			case 5:
				goto IL_AA;
			case 6:
				if (!(A_0 == ClipboardData.b("ݴቶ੸ེᱼൾ", a_)))
				{
					num = 0;
					continue;
				}
				return CellMerge.Start;
			}
			if (A_0 != null)
			{
				num = 3;
				continue;
			}
			return CellMerge.None;
			IL_AA:
			num = 6;
		}
		IL_5D:
		if (false)
		{
		}
		return CellMerge.Continue;
		IL_77:
		return CellMerge.None;
	}

	// Token: 0x06003C93 RID: 15507 RVA: 0x003886EC File Offset: 0x003876EC
	internal static string ᜀ(CellMerge A_0)
	{
		int a_ = 18;
		for (;;)
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
					switch (A_0)
					{
					case CellMerge.Start:
						goto IL_53;
					case CellMerge.Continue:
						goto IL_62;
					}
					goto IL_49;
				case 1:
					goto IL_98;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_49;
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
				IL_49:
				num = 2;
			}
		}
		IL_53:
		return ClipboardData.b("੷όཻ੽", a_);
		IL_62:
		return ClipboardData.b("᭷ᕹቻ੽", a_);
		IL_98:
		return "";
	}

	// Token: 0x06003C94 RID: 15508 RVA: 0x00388798 File Offset: 0x00387798
	internal static CellVerticalAlignment ᜀ(string A_0)
	{
		int a_ = 19;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_110;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 1:
				if (!(A_0 == ClipboardData.b("൸ᑺർ", a_)))
				{
					num = 2;
					continue;
				}
				return CellVerticalAlignment.Top;
			case 2:
				num = 8;
				continue;
			case 3:
				num = 1;
				continue;
			case 5:
				goto IL_87;
			case 6:
				num = 7;
				continue;
			case 7:
				if (!(A_0 == ClipboardData.b("᭸ᑺॼ୾", a_)))
				{
					goto IL_110;
				}
				return CellVerticalAlignment.Bottom;
			case 8:
				if (!(A_0 == ClipboardData.b("᩸Ṻ፼୾", a_)))
				{
					num = 6;
					continue;
				}
				return CellVerticalAlignment.Center;
			}
			if (A_0 != null)
			{
				num = 3;
				continue;
			}
			return CellVerticalAlignment.Top;
			IL_110:
			num = 0;
		}
		return CellVerticalAlignment.Top;
		IL_87:
		return CellVerticalAlignment.Top;
	}

	// Token: 0x06003C95 RID: 15509 RVA: 0x003888C8 File Offset: 0x003878C8
	internal static string ᜀ(CellVerticalAlignment A_0)
	{
		int a_ = 18;
		for (;;)
		{
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						num = 1;
						continue;
					case 1:
						goto IL_9A;
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
							switch (A_0)
							{
							case CellVerticalAlignment.Top:
								goto IL_80;
							case CellVerticalAlignment.Center:
								goto IL_71;
							case CellVerticalAlignment.Bottom:
								goto IL_9C;
							default:
								num = 0;
								continue;
							}
							break;
						}
						break;
					}
					break;
				}
			}
		}
		IL_71:
		return ClipboardData.b("᭷όቻ੽", a_);
		IL_80:
		return ClipboardData.b("౷ᕹ౻", a_);
		IL_9A:
		return "";
		IL_9C:
		return ClipboardData.b("᩷ᕹࡻ੽", a_);
	}
}
