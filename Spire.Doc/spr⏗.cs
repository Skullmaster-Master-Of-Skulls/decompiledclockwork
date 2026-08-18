using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;

// Token: 0x02000301 RID: 769
internal static class spr\u23D7
{
	// Token: 0x060029D7 RID: 10711 RVA: 0x0029B74C File Offset: 0x0029A74C
	public static double ᜀ(DateTime A_0)
	{
		double num;
		for (;;)
		{
			num = A_0.ToOADate();
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return num;
			default:
			{
				if (false)
				{
				}
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return num;
					case 1:
						num -= 1.0;
						num2 = 0;
						continue;
					case 2:
						if (num < 61.0)
						{
							num2 = 1;
							continue;
						}
						return num;
					}
					break;
				}
				break;
			}
			}
		}
		return num;
	}

	// Token: 0x060029D8 RID: 10712 RVA: 0x0029B7D8 File Offset: 0x0029A7D8
	public static DateTime ᜀ(double A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_4F;
			case 2:
				A_0 += 1.0;
				num = 1;
				continue;
			}
			if (true)
			{
			}
			if (A_0 >= 61.0)
			{
				break;
			}
			num = 2;
		}
		IL_4F:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_4F;
		default:
			if (false)
			{
			}
			return DateTime.FromOADate(A_0);
		}
	}

	// Token: 0x060029D9 RID: 10713 RVA: 0x0029B864 File Offset: 0x0029A864
	public static void ᜀ(Stream A_0, Stream A_1)
	{
		int a_ = 18;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_47;
			case 1:
				return;
			case 2:
			{
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				byte[] buffer = new byte[32768];
				goto IL_F8;
			}
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_F8;
				default:
					if (false)
					{
					}
					goto IL_A0;
				}
				break;
			case 5:
			{
				byte[] buffer;
				int count;
				if ((count = A_0.Read(buffer, 0, 32768)) <= 0)
				{
					num = 1;
					continue;
				}
				A_1.Write(buffer, 0, count);
				num = 4;
				continue;
			}
			case 6:
				goto IL_A0;
			case 7:
				goto IL_E3;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 2;
			continue;
			IL_A0:
			num = 5;
			continue;
			IL_F8:
			num = 6;
		}
		IL_47:
		throw new ArgumentNullException(ClipboardData.b("୷ᕹॻ౽", a_));
		IL_E3:
		if (true)
		{
		}
		throw new ArgumentNullException(ClipboardData.b("ᱷόཻ੽", a_));
	}

	// Token: 0x060029DA RID: 10714 RVA: 0x0029B980 File Offset: 0x0029A980
	public static Stream ᜁ(Stream A_0)
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
		Stream stream = new MemoryStream((int)A_0.Length);
		long position = A_0.Position;
		A_0.Position = 0L;
		spr\u23D7.ᜀ(A_0, stream);
		stream.Position = (A_0.Position = position);
		return stream;
	}

	// Token: 0x060029DB RID: 10715 RVA: 0x0029B9F0 File Offset: 0x0029A9F0
	public static XmlReader ᜀ(Stream A_0, bool A_1)
	{
		XmlReader xmlReader;
		for (;;)
		{
			A_0.Position = 0L;
			xmlReader = XmlReader.Create(A_0);
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_44;
				case 1:
					num = 0;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_44;
					default:
						goto IL_7D;
					}
					break;
				case 3:
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						num = 2;
						continue;
					}
					xmlReader.Read();
					num = 4;
					continue;
				case 4:
					goto IL_44;
				case 5:
					if (A_1)
					{
						num = 1;
						continue;
					}
					return xmlReader;
				}
				break;
				IL_44:
				num = 3;
			}
		}
		IL_7D:
		if (false)
		{
		}
		if (true)
		{
		}
		return xmlReader;
	}

	// Token: 0x060029DC RID: 10716 RVA: 0x0029BAAC File Offset: 0x0029AAAC
	public static XmlReader ᜀ(Stream A_0)
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
		return spr\u23D7.ᜀ(A_0, true);
	}

	// Token: 0x060029DD RID: 10717 RVA: 0x0029BAF0 File Offset: 0x0029AAF0
	public static XmlWriter ᜀ(Stream A_0, Encoding A_1)
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
		return XmlWriter.Create(A_0, new XmlWriterSettings
		{
			Encoding = A_1
		});
	}

	// Token: 0x060029DE RID: 10718 RVA: 0x0029BB40 File Offset: 0x0029AB40
	public static XmlWriter ᜀ(TextWriter A_0)
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
		XmlWriterSettings settings = new XmlWriterSettings();
		return XmlWriter.Create(A_0, settings);
	}

	// Token: 0x060029DF RID: 10719 RVA: 0x0029BB88 File Offset: 0x0029AB88
	public static XmlWriter ᜀ(TextWriter A_0, bool A_1)
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
		return XmlWriter.Create(A_0, new XmlWriterSettings
		{
			Indent = A_1
		});
	}

	// Token: 0x060029E0 RID: 10720 RVA: 0x0029BBD8 File Offset: 0x0029ABD8
	public static MemoryStream ᜀ(XmlReader A_0)
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
		MemoryStream memoryStream = new MemoryStream();
		XmlWriter xmlWriter = spr\u23D7.ᜀ(memoryStream, Encoding.UTF8);
		xmlWriter.WriteNode(A_0, false);
		xmlWriter.Flush();
		return memoryStream;
	}

	// Token: 0x060029E1 RID: 10721 RVA: 0x0029BC34 File Offset: 0x0029AC34
	internal static string ᜀ(LineDashing A_0)
	{
		int a_ = 4;
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					switch (A_0)
					{
					case LineDashing.Dot:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_99;
						}
						break;
					case LineDashing.DashDot:
					case LineDashing.DashDotDot:
					case LineDashing.DotGEL:
						goto IL_F2;
					case LineDashing.DashGEL:
						goto IL_D9;
					case LineDashing.LongDashGEL:
						goto IL_65;
					case LineDashing.DashDotGEL:
						goto IL_74;
					case LineDashing.LongDashDotGEL:
						goto IL_CA;
					case LineDashing.LongDashDotDotGEL:
						goto IL_AE;
					}
					num = 1;
					continue;
				case 1:
					num = 2;
					continue;
				case 2:
					goto IL_C8;
				}
				break;
			}
		}
		IL_65:
		return ClipboardData.b("٩ͫmᝯ㙱ᕳյၷ", a_);
		IL_74:
		return ClipboardData.b("๩൫ᵭᡯ㙱᭳ɵ", a_);
		IL_99:
		if (false)
		{
		}
		return ClipboardData.b("孩䱫彭", a_);
		IL_AE:
		return ClipboardData.b("٩ͫmᝯ㙱ᕳյၷ㹹፻੽쑿", a_);
		IL_C8:
		goto IL_F2;
		IL_CA:
		return ClipboardData.b("٩ͫmᝯ㙱ᕳյၷ㹹፻੽", a_);
		IL_D9:
		return ClipboardData.b("๩൫ᵭᡯ", a_);
		IL_F2:
		return null;
	}

	// Token: 0x060029E2 RID: 10722 RVA: 0x0029BD34 File Offset: 0x0029AD34
	internal static LineDashing ᜁ(string A_0)
	{
		int a_ = 1;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
			{
				int num2;
				if (!spr᧓.ឥ.TryGetValue(A_0, out num2))
				{
					return LineDashing.Solid;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_146;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			}
			case 2:
				num = 8;
				continue;
			case 3:
				num = 9;
				continue;
			case 4:
				goto IL_146;
			case 5:
				if (true)
				{
				}
				goto IL_14B;
			case 6:
				spr᧓.ឥ = new Dictionary<string, int>(7)
				{
					{
						ClipboardData.b("噦䥨婪", a_),
						0
					},
					{
						ClipboardData.b("ͦ٨Ὢ", a_),
						1
					},
					{
						ClipboardData.b("ͦࡨᡪլ", a_),
						2
					},
					{
						ClipboardData.b("ͦࡨᡪլ⭮Ṱݲ", a_),
						3
					},
					{
						ClipboardData.b("୦٨ժ੬⭮ၰrᵴ", a_),
						4
					},
					{
						ClipboardData.b("୦٨ժ੬⭮ၰrᵴ㍶ᙸེ", a_),
						5
					},
					{
						ClipboardData.b("୦٨ժ੬⭮ၰrᵴ㍶ᙸེ㥼ၾ", a_),
						6
					}
				};
				num = 5;
				continue;
			case 7:
				num = 4;
				continue;
			case 8:
				if (spr᧓.ឥ == null)
				{
					num = 6;
					continue;
				}
				goto IL_14B;
			case 9:
			{
				int num2;
				switch (num2)
				{
				case 0:
				case 1:
					return LineDashing.Dot;
				case 2:
					return LineDashing.DashGEL;
				case 3:
					return LineDashing.DashDot;
				case 4:
					return LineDashing.LongDashGEL;
				case 5:
					return LineDashing.LongDashDotGEL;
				case 6:
					return LineDashing.LongDashDotDotGEL;
				default:
					num = 7;
					continue;
				}
				break;
			}
			}
			if (A_0 != null)
			{
				num = 2;
				continue;
			}
			return LineDashing.Solid;
			IL_14B:
			num = 1;
		}
		return LineDashing.DashDot;
		IL_146:
		return LineDashing.Solid;
	}

	// Token: 0x060029E3 RID: 10723 RVA: 0x0029BF1C File Offset: 0x0029AF1C
	internal static string ᜁ(TextBoxLineStyle A_0)
	{
		int a_ = 4;
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_77;
				case 1:
					goto IL_4F;
				case 2:
					if (true)
					{
					}
					switch (A_0)
					{
					case TextBoxLineStyle.Double:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4F;
						default:
							goto IL_A8;
						}
						break;
					case TextBoxLineStyle.ThickThin:
						goto IL_51;
					case TextBoxLineStyle.ThinThick:
						goto IL_60;
					case TextBoxLineStyle.Triple:
						goto IL_83;
					default:
						num = 1;
						continue;
					}
					break;
				}
				break;
				IL_4F:
				num = 0;
			}
		}
		IL_51:
		return ClipboardData.b("ṩѫݭ፯ᥱ⁳ṵᅷᑹ", a_);
		IL_60:
		return ClipboardData.b("ṩѫݭṯ♱ᱳή᭷ᅹ", a_);
		IL_77:
		return null;
		IL_83:
		return ClipboardData.b("ṩѫݭ፯ᥱ㙳፵౷൹᥻᭽횁", a_);
		IL_A8:
		if (false)
		{
		}
		return ClipboardData.b("ṩѫݭṯ♱ᱳήᙷ", a_);
	}

	// Token: 0x060029E4 RID: 10724 RVA: 0x0029BFE8 File Offset: 0x0029AFE8
	internal static TextBoxLineStyle ᜀ(string A_0)
	{
		int a_ = 14;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_0 == ClipboardData.b("sṵᅷ᥹᝻⩽", a_))
				{
					return TextBoxLineStyle.ThickThin;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BC;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 2:
				num = 8;
				continue;
			case 3:
				num = 1;
				continue;
			case 4:
				num = 10;
				continue;
			case 5:
				if (!(A_0 == ClipboardData.b("sṵᅷ᥹᝻㱽\ud88b憐ﲑ", a_)))
				{
					num = 4;
					continue;
				}
				return TextBoxLineStyle.Triple;
			case 6:
				if (!(A_0 == ClipboardData.b("sṵᅷᑹ⡻ᙽ", a_)))
				{
					num = 3;
					continue;
				}
				return TextBoxLineStyle.ThinThick;
			case 7:
				num = 5;
				continue;
			case 8:
				if (!(A_0 == ClipboardData.b("sṵᅷᑹ⡻ᙽ", a_)))
				{
					goto IL_BC;
				}
				return TextBoxLineStyle.Double;
			case 9:
				if (true)
				{
				}
				num = 6;
				continue;
			case 10:
				goto IL_61;
			}
			if (A_0 != null)
			{
				num = 2;
				continue;
			}
			return TextBoxLineStyle.Simple;
			IL_BC:
			num = 9;
		}
		return TextBoxLineStyle.ThinThick;
		IL_61:
		return TextBoxLineStyle.Simple;
	}

	// Token: 0x060029E5 RID: 10725 RVA: 0x0029C150 File Offset: 0x0029B150
	internal static BorderStyle ᜀ(TextBoxLineStyle A_0)
	{
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return BorderStyle.None;
				case 1:
					num = 0;
					continue;
				case 2:
					for (;;)
					{
						switch (A_0)
						{
						case TextBoxLineStyle.Simple:
							return BorderStyle.Single;
						case TextBoxLineStyle.Double:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							}
							goto Block_2;
						case TextBoxLineStyle.ThickThin:
							return BorderStyle.ThickThinMediumGap;
						case TextBoxLineStyle.ThinThick:
							goto IL_42;
						case TextBoxLineStyle.Triple:
							return BorderStyle.Triple;
						}
						break;
					}
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_42:
		if (true)
		{
		}
		return BorderStyle.ThinThickMediumGap;
		Block_2:
		if (false)
		{
		}
		return BorderStyle.Double;
	}
}
