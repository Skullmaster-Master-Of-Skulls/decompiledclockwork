using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Spire.CompoundFile.Doc;
using Spire.CompoundFile.Doc.Native;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Documents.Converters;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Layouting;

// Token: 0x0200025D RID: 605
internal class sprᣑ
{
	// Token: 0x06001E58 RID: 7768 RVA: 0x001E23A8 File Offset: 0x001E13A8
	internal Dictionary<string, int> \u1717()
	{
		int num = 2;
		for (;;)
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
				switch (num)
				{
				case 0:
					this.ᜤ = new Dictionary<string, int>();
					num = 1;
					continue;
				case 1:
					goto IL_65;
				}
				break;
			}
			if (this.ᜤ != null)
			{
				break;
			}
			num = 0;
		}
		IL_65:
		return this.ᜤ;
	}

	// Token: 0x06001E59 RID: 7769 RVA: 0x001E242C File Offset: 0x001E142C
	internal Dictionary<string, Comment> \u1716()
	{
		int num = 0;
		for (;;)
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
				switch (num)
				{
				case 1:
					this.ᜆ();
					num = 2;
					continue;
				case 2:
					goto IL_60;
				}
				break;
			}
			if (this.ᜡ != null)
			{
				break;
			}
			num = 1;
		}
		IL_60:
		return this.ᜡ;
	}

	// Token: 0x06001E5A RID: 7770 RVA: 0x001E24AC File Offset: 0x001E14AC
	internal Stack<Comment> \u171A()
	{
		int num = 2;
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
					this.ᜢ = new Stack<Comment>();
					if (true)
					{
					}
					num = 1;
					continue;
				case 1:
					goto IL_65;
				}
				break;
			}
			if (this.ᜢ != null)
			{
				break;
			}
			num = 0;
		}
		IL_65:
		return this.ᜢ;
	}

	// Token: 0x06001E5B RID: 7771 RVA: 0x001E2530 File Offset: 0x001E1530
	private Stack<int> \u1715()
	{
		int num = 2;
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
					this.ᜥ = new Stack<int>();
					if (true)
					{
					}
					num = 1;
					continue;
				case 1:
					goto IL_65;
				}
				break;
			}
			if (this.ᜥ != null)
			{
				break;
			}
			num = 0;
		}
		IL_65:
		return this.ᜥ;
	}

	// Token: 0x06001E5C RID: 7772 RVA: 0x001E25B4 File Offset: 0x001E15B4
	private Comment \u1714()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜢ.Count > 0)
				{
					num = 1;
					continue;
				}
				goto IL_8C;
			case 1:
				goto IL_8A;
			case 2:
				if (true)
				{
				}
				num = 0;
				continue;
			}
			if (this.ᜢ == null)
			{
				goto IL_8C;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_4E;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
		}
		IL_4E:
		return this.ᜢ.Peek();
		IL_8A:
		goto IL_4E;
		IL_8C:
		return null;
	}

	// Token: 0x06001E5D RID: 7773 RVA: 0x001E2650 File Offset: 0x001E1650
	private Stack<Field> \u1713()
	{
		int num = 1;
		for (;;)
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
				switch (num)
				{
				case 0:
					goto IL_65;
				case 2:
					this.\u171D = new Stack<Field>();
					num = 0;
					continue;
				}
				break;
			}
			if (this.\u171D != null)
			{
				break;
			}
			num = 2;
		}
		IL_65:
		return this.\u171D;
	}

	// Token: 0x06001E5E RID: 7774 RVA: 0x001E26D4 File Offset: 0x001E16D4
	private Field \u1712()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (this.\u171D.Count > 0)
				{
					num = 3;
					continue;
				}
				goto IL_8C;
			case 2:
				num = 1;
				continue;
			case 3:
				goto IL_8A;
			}
			if (true)
			{
			}
			if (this.\u171D == null)
			{
				goto IL_8C;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_56;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
		}
		IL_56:
		return this.\u171D.Peek();
		IL_8A:
		goto IL_56;
		IL_8C:
		return null;
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x001E2770 File Offset: 0x001E1770
	private List<DictionaryEntry> ᜑ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.\u1718.Count == 0)
				{
					num = 2;
					continue;
				}
				goto IL_A2;
			case 2:
				goto IL_36;
			case 3:
				goto IL_80;
			case 4:
				goto IL_7E;
			}
			if (this.\u1718 != null)
			{
				num = 3;
				continue;
			}
			IL_36:
			this.\u1718 = new List<DictionaryEntry>();
			this.ᜀ(true);
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_80:
				num = 0;
				continue;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			num = 4;
		}
		IL_7E:
		IL_A2:
		return this.\u1718;
	}

	// Token: 0x06001E60 RID: 7776 RVA: 0x001E2828 File Offset: 0x001E1828
	private List<DictionaryEntry> ᜐ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				for (;;)
				{
					this.\u1719 = new List<DictionaryEntry>();
					this.ᜀ(false);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_68;
					}
				}
				IL_68:
				if (false)
				{
				}
				num = 2;
				continue;
			case 2:
				goto IL_76;
			}
			if (true)
			{
			}
			if (this.\u1719 != null)
			{
				break;
			}
			num = 0;
		}
		IL_76:
		return this.\u1719;
	}

	// Token: 0x06001E61 RID: 7777 RVA: 0x001E28B4 File Offset: 0x001E18B4
	private Dictionary<string, string> ᜏ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 2:
				for (;;)
				{
					this.\u1715 = new Dictionary<string, string>();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_59;
					}
				}
				IL_59:
				if (false)
				{
				}
				if (true)
				{
				}
				num = 0;
				continue;
			}
			if (this.\u1715 != null)
			{
				break;
			}
			num = 2;
		}
		IL_6F:
		return this.\u1715;
	}

	// Token: 0x06001E62 RID: 7778 RVA: 0x001E2938 File Offset: 0x001E1938
	private Dictionary<string, string> ᜎ()
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
				for (;;)
				{
					this.\u1714 = new Dictionary<string, string>();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_61;
					}
				}
				IL_61:
				if (false)
				{
				}
				num = 1;
				continue;
			case 1:
				goto IL_6F;
			}
			if (this.\u1714 != null)
			{
				break;
			}
			num = 0;
		}
		IL_6F:
		return this.\u1714;
	}

	// Token: 0x06001E63 RID: 7779 RVA: 0x001E29BC File Offset: 0x001E19BC
	private Dictionary<string, string> \u170D()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 1:
				for (;;)
				{
					this.\u1713 = new Dictionary<string, string>();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_61;
					}
				}
				IL_61:
				if (false)
				{
				}
				num = 0;
				continue;
			}
			if (true)
			{
			}
			if (this.\u1713 != null)
			{
				break;
			}
			num = 1;
		}
		IL_6F:
		return this.\u1713;
	}

	// Token: 0x06001E64 RID: 7780 RVA: 0x001E2A40 File Offset: 0x001E1A40
	internal Dictionary<string, DocPicture> \u1719()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				for (;;)
				{
					this.\u1712 = new Dictionary<string, DocPicture>();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_61;
					}
				}
				IL_61:
				if (false)
				{
				}
				num = 1;
				continue;
			case 1:
				goto IL_6F;
			}
			if (true)
			{
			}
			if (this.\u1712 != null)
			{
				break;
			}
			num = 0;
		}
		IL_6F:
		return this.\u1712;
	}

	// Token: 0x06001E65 RID: 7781 RVA: 0x001E2AC4 File Offset: 0x001E1AC4
	internal Dictionary<string, DictionaryEntry> \u1718()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				for (;;)
				{
					this.ᜎ = new Dictionary<string, DictionaryEntry>();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_61;
					}
				}
				IL_61:
				if (false)
				{
				}
				num = 2;
				continue;
			case 2:
				goto IL_6F;
			}
			if (true)
			{
			}
			if (this.ᜎ != null)
			{
				break;
			}
			num = 0;
		}
		IL_6F:
		return this.ᜎ;
	}

	// Token: 0x06001E66 RID: 7782 RVA: 0x001E2B48 File Offset: 0x001E1B48
	private Dictionary<string, bool> ᜌ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (true)
				{
				}
				for (;;)
				{
					this.ᜏ = new Dictionary<string, bool>();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_61;
					}
				}
				IL_61:
				if (false)
				{
				}
				num = 2;
				continue;
			case 2:
				goto IL_6F;
			}
			if (this.ᜏ != null)
			{
				break;
			}
			num = 1;
		}
		IL_6F:
		return this.ᜏ;
	}

	// Token: 0x06001E67 RID: 7783 RVA: 0x001E2BCC File Offset: 0x001E1BCC
	private Dictionary<string, string> ᜋ()
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
				goto IL_6F;
			case 1:
				for (;;)
				{
					this.ᜐ = new Dictionary<string, string>();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_61;
					}
				}
				IL_61:
				if (false)
				{
				}
				num = 0;
				continue;
			}
			if (this.ᜐ != null)
			{
				break;
			}
			num = 1;
		}
		IL_6F:
		return this.ᜐ;
	}

	// Token: 0x06001E68 RID: 7784 RVA: 0x001E2C50 File Offset: 0x001E1C50
	private Dictionary<string, Dictionary<string, DictionaryEntry>> ᜊ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				for (;;)
				{
					this.ᜑ = new Dictionary<string, Dictionary<string, DictionaryEntry>>();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_59;
					}
				}
				IL_59:
				if (false)
				{
				}
				if (true)
				{
				}
				num = 1;
				continue;
			case 1:
				goto IL_6F;
			}
			if (this.ᜑ != null)
			{
				break;
			}
			num = 0;
		}
		IL_6F:
		return this.ᜑ;
	}

	// Token: 0x06001E69 RID: 7785 RVA: 0x001E2CD4 File Offset: 0x001E1CD4
	private Dictionary<string, string> ᜉ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				for (;;)
				{
					this.\u170D = new Dictionary<string, string>();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_61;
					}
				}
				IL_61:
				if (false)
				{
				}
				num = 2;
				continue;
			case 2:
				goto IL_6F;
			}
			if (true)
			{
			}
			if (this.\u170D != null)
			{
				break;
			}
			num = 1;
		}
		IL_6F:
		return this.\u170D;
	}

	// Token: 0x06001E6A RID: 7786 RVA: 0x001E2D58 File Offset: 0x001E1D58
	private Dictionary<string, string> ᜈ()
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
		return this.ᜄ.StyleNameIds;
	}

	// Token: 0x06001E6B RID: 7787 RVA: 0x001E2DA0 File Offset: 0x001E1DA0
	private Dictionary<string, string> ᜇ()
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
				goto IL_6F;
			case 1:
				for (;;)
				{
					this.ᜌ = new Dictionary<string, string>();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_61;
					}
				}
				IL_61:
				if (false)
				{
				}
				num = 0;
				continue;
			}
			if (this.ᜌ != null)
			{
				break;
			}
			num = 1;
		}
		IL_6F:
		return this.ᜌ;
	}

	// Token: 0x06001E6C RID: 7788 RVA: 0x001E2E24 File Offset: 0x001E1E24
	private void ᜀ(ParagraphBase A_0, IParagraph A_1)
	{
		for (;;)
		{
			A_1.Items.Add(A_0);
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 10;
					continue;
				case 1:
					num = 6;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EE;
					default:
						if (false)
						{
						}
						if (this.\u1712() != A_0)
						{
							num = 5;
							continue;
						}
						return;
					}
					break;
				case 3:
					goto IL_EE;
				case 4:
					if (!this.\u1712().IsFieldRange)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					return;
				case 5:
					num = 14;
					continue;
				case 6:
					if (this.\u1712().Owner != A_0)
					{
						num = 11;
						continue;
					}
					return;
				case 7:
					if (this.\u1712() != null)
					{
						num = 12;
						continue;
					}
					return;
				case 8:
					return;
				case 9:
					num = 13;
					continue;
				case 10:
					if (this.\u1712().Type != FieldType.FieldHyperlink)
					{
						num = 3;
						continue;
					}
					return;
				case 11:
					num = 2;
					continue;
				case 12:
					num = 4;
					continue;
				case 13:
					if (this.\u1712().Type != FieldType.FieldLink)
					{
						num = 0;
						continue;
					}
					return;
				case 14:
					if (this.\u1712().Type != FieldType.FieldEmbed)
					{
						num = 9;
						continue;
					}
					return;
				}
				break;
				IL_EE:
				this.ᜀ(A_0);
				num = 8;
			}
		}
	}

	// Token: 0x06001E6D RID: 7789 RVA: 0x001E2FD4 File Offset: 0x001E1FD4
	internal Document ᜀ(string A_0, Document A_1)
	{
		int a_ = 13;
		for (;;)
		{
			this.ᜂ = new spr\u1FDD();
			int num = 3;
			for (;;)
			{
				FileStream fileStream;
				switch (num)
				{
				case 0:
					goto IL_168;
				case 1:
					if (A_0.Length == 0)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_11F;
				case 2:
					num = 1;
					continue;
				case 3:
					if (A_0 != null)
					{
						num = 2;
						continue;
					}
					goto IL_16A;
				case 4:
					try
					{
						num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_E0;
							case 1:
								goto IL_D5;
							case 2:
								goto IL_B5;
							case 3:
								goto IL_D5;
							}
							if (A_1.ᜉ(fileStream))
							{
								num = 2;
								continue;
							}
							this.ᜂ.ᜁ(fileStream, false);
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
							IL_B5:
							Stream a_2 = this.ᜀ(fileStream, A_1);
							this.ᜂ.ᜁ(a_2, false);
							num = 3;
							continue;
							IL_D5:
							num = 0;
						}
						IL_E0:
						goto IL_17E;
					}
					finally
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								((IDisposable)fileStream).Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_11C;
							}
							if (fileStream == null)
							{
								break;
							}
							num = 0;
						}
						IL_11C:;
					}
					goto IL_11F;
				}
				break;
				IL_11F:
				fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read);
				num = 4;
			}
		}
		IL_168:
		IL_16A:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᩲ᭴ݶ౸ེ㭼ᙾ쮄", a_));
		IL_17E:
		this.ᜄ = A_1;
		this.ᜁ(A_1);
		return A_1;
	}

	// Token: 0x06001E6E RID: 7790 RVA: 0x001E3180 File Offset: 0x001E2180
	internal Document ᜁ(Stream A_0, Document A_1)
	{
		for (;;)
		{
			this.ᜂ = new spr\u1FDD();
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (A_1.ᜉ(A_0))
					{
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
						if (false)
						{
						}
						this.ᜂ.ᜁ(A_0, false);
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_81;
				case 2:
				{
					Stream a_ = this.ᜀ(A_0, A_1);
					this.ᜂ.ᜁ(a_, false);
					num = 3;
					continue;
				}
				case 3:
					goto IL_A4;
				}
				break;
			}
		}
		IL_81:
		IL_A4:
		this.ᜄ = A_1;
		this.ᜁ(A_1);
		return A_1;
	}

	// Token: 0x06001E6F RID: 7791 RVA: 0x001E3244 File Offset: 0x001E2244
	private Stream ᜀ(Stream A_0, Document A_1)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_4C;
				case 1:
					goto IL_73;
				case 2:
				{
					bool flag;
					if (!flag)
					{
						num = 1;
						continue;
					}
					Stream result;
					return result;
				}
				case 3:
					try
					{
						for (;;)
						{
							spr\u21F4 spr_u21F;
							spr\u2547 a_2 = spr_u21F.ᜀ();
							spr\u1AED spr_u1AED = new spr\u1AED();
							spr\u1AED.EncrytionType encrytionType = spr_u1AED.ᜀ(a_2);
							spr\u1AED.EncrytionType encrytionType2 = encrytionType;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									switch (encrytionType2)
									{
									case spr\u1AED.EncrytionType.Standard:
									{
										bool flag = true;
										spr\u19E4 spr_u19E = new spr\u19E4();
										spr_u19E.ᜃ(a_2);
										num = 4;
										continue;
									}
									case spr\u1AED.EncrytionType.Agile:
									{
										bool flag = true;
										spr\u2532 spr_u = new spr\u2532();
										spr_u.ᜃ(a_2);
										num = 9;
										continue;
									}
									default:
										num = 3;
										continue;
									}
									break;
								case 1:
									goto IL_1FF;
								case 2:
									goto IL_20B;
								case 3:
									num = 5;
									continue;
								case 4:
								{
									spr\u19E4 spr_u19E;
									if (!spr_u19E.ᜀ(A_1.\u171A))
									{
										num = 6;
										continue;
									}
									Stream result = spr_u19E.ᜀ();
									num = 1;
									continue;
								}
								case 5:
									goto IL_1FF;
								case 6:
									goto IL_171;
								case 7:
									goto IL_11B;
								case 8:
									goto IL_1C2;
								case 9:
								{
									spr\u2532 spr_u;
									if (!spr_u.ᜀ(A_1.\u171A))
									{
										num = 8;
										continue;
									}
									Stream result = spr_u.ᜀ();
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_11B;
									default:
										if (false)
										{
										}
										num = 7;
										continue;
									}
									break;
								}
								}
								break;
								IL_1FF:
								num = 2;
								continue;
								IL_11B:
								goto IL_1FF;
							}
						}
						IL_171:
						throw new Exception(ClipboardData.b("㑦ᥨ๪๬ٮᝰᩲၴ፶奸୺ᱼ౾ꮊ꾌", a_) + A_1.\u171A + ClipboardData.b("䕦䥨ɪṬ佮ᡰᵲᙴᡶ୸ॺ᡼᱾ꊂ", a_));
						IL_1C2:
						throw new Exception(ClipboardData.b("⍦٨ࡪᡬɮᑰᵲŴ坶ၸࡺ嵼᩾ﺆ麗ﾊ붐뎒\uda94ﲘ붜즠욢薤힦좨\ud8aa\udeac\ud8ae\udeb0솲톴鞶ힸ\udeba\ud8bc\udbbe닀뗄ꛆ뫈룊뫌ꃎꏐ럒ﯔ", a_));
						IL_20B:
						goto IL_51;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							spr\u21F4 spr_u21F;
							switch (num)
							{
							case 0:
								spr_u21F.Dispose();
								num = 1;
								continue;
							case 1:
								goto IL_24B;
							}
							if (spr_u21F == null)
							{
								break;
							}
							num = 0;
						}
						IL_24B:;
					}
					goto IL_24E;
					IL_51:
					num = 2;
					continue;
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					Stream result = new MemoryStream();
					bool flag = false;
					spr\u21F4 spr_u21F = A_1.ᜊ(A_0);
					num = 3;
				}
			}
			IL_4C:
			goto IL_24E;
			IL_73:
			if (true)
			{
			}
			throw new ApplicationException(ClipboardData.b("て᭨Ѫͬ࡮兰⑲ᩴնᵸ孺୼᩾", a_));
			IL_24E:
			throw new ArgumentNullException(ClipboardData.b("ᑦᵨᥪ࡬๮ᱰ", a_));
		}
		}
	}

	// Token: 0x06001E70 RID: 7792 RVA: 0x001E3510 File Offset: 0x001E2510
	private void ᜁ(Document A_0)
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
		spr᪆ spr᪆ = new spr᪆();
		spr᪆.ᜀ(this.ᜂ);
		A_0.DocxPackage = spr᪆;
		this.ᜀ(A_0);
		this.ᜀ(spr᪆);
		this.ᜁ(spr᪆);
		this.ᜂ.ᜁ();
	}

	// Token: 0x06001E71 RID: 7793 RVA: 0x001E3584 File Offset: 0x001E2584
	private void ᜀ(Document A_0)
	{
		int a_ = 0;
		for (;;)
		{
			bool flag = false;
			A_0.DetectedFormatType = FileFormat.Docx;
			XmlReader xmlReader = XmlReader.Create(A_0.DocxPackage.ᜁ()[ClipboardData.b("㵥⭧թɫᩭᕯᱱs⥵ⱷ͹౻᭽\udf81ꪃﺅ", a_)].ᜁ());
			int num = 12;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (xmlReader.LocalName == ClipboardData.b("⥥ṧཀྵṫᱭ᥯ᙱᅳ", a_))
					{
						num = 13;
						continue;
					}
					goto IL_274;
				case 1:
					A_0.DetectedFormatType = FileFormat.Dotx2010;
					num = 23;
					continue;
				case 2:
					goto IL_1E9;
				case 3:
					if (xmlReader.LocalName != ClipboardData.b("㉥ᅧᩩ५ᵭ", a_))
					{
						num = 22;
						continue;
					}
					xmlReader.Read();
					num = 10;
					continue;
				case 4:
					A_0.DetectedFormatType = FileFormat.Docm2010;
					num = 9;
					continue;
				case 5:
					num = 3;
					continue;
				case 6:
					flag = true;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3B3;
					default:
						if (false)
						{
						}
						num = 25;
						continue;
					}
					break;
				case 7:
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						num = 5;
						continue;
					}
					xmlReader.Read();
					num = 11;
					continue;
				case 8:
					if (!(xmlReader.LocalName != ClipboardData.b("㉥ᅧᩩ५ᵭ", a_)))
					{
						num = 19;
						continue;
					}
					num = 0;
					continue;
				case 9:
					goto IL_1A3;
				case 10:
					goto IL_131;
				case 11:
					goto IL_167;
				case 12:
					goto IL_167;
				case 13:
					num = 24;
					continue;
				case 14:
					goto IL_1A3;
				case 15:
				{
					string attribute = xmlReader.GetAttribute(ClipboardData.b("㙥१ᡩᡫ⁭ᅯάᅳ", a_));
					num = 20;
					continue;
				}
				case 16:
					if (xmlReader.GetAttribute(ClipboardData.b("╥ݧѩᡫ୭ṯٱ⁳ཱུࡷό", a_)) == ClipboardData.b("ݥᡧᩩkݭ፯፱sή᝷ᑹ卻ࡽꪃﮇ꞉ﮋ몓蓮얟첡킣袥얧쮩쾫\udcad\udfaf\udab3ힵ\udab7횹\ud9bb\udabd꿁ꗃ꿅ꛇ듋ꏍ볏", a_))
					{
						num = 4;
						continue;
					}
					goto IL_274;
				case 17:
					goto IL_131;
				case 18:
					if (xmlReader.GetAttribute(ClipboardData.b("╥ݧѩᡫ୭ṯٱ⁳ཱུࡷό", a_)) == ClipboardData.b("ݥᡧᩩkݭ፯፱sή᝷ᑹ卻ࡽꪃﮇ꞉ﮋ몓ﶗ솟횡솣袥얧쮩쾫\udcad\udfaf\udab3ힵ\udab7횹\ud9bb\udabd钿ꟁ꧃뛅꓇ꯉ룋ꯍﻏ뿑뗓뿕뛗ꓛ돝賟", a_))
					{
						num = 26;
						continue;
					}
					num = 16;
					continue;
				case 19:
					goto IL_1A3;
				case 20:
				{
					string attribute;
					if (ClipboardData.b("䥥ὧթṫ੭彯ᙱ᭳ᕵ൷᝹᥻ၽ겁ﲃ", a_) == attribute)
					{
						if (true)
						{
						}
						num = 6;
						continue;
					}
					goto IL_274;
				}
				case 21:
					if (!flag)
					{
						num = 2;
						continue;
					}
					return;
				case 22:
					goto IL_30D;
				case 23:
					goto IL_1A3;
				case 24:
					if (xmlReader.HasAttributes)
					{
						num = 15;
						continue;
					}
					goto IL_274;
				case 25:
					if (xmlReader.GetAttribute(ClipboardData.b("╥ݧѩᡫ୭ṯٱ⁳ཱུࡷό", a_)) == ClipboardData.b("ݥᡧᩩkݭ፯፱sή᝷ᑹ卻ࡽꪃﶏﺑ秊ﶛ펟辡쮣삥캧쎩쾫쮭풯\uddb1ힳ쎵햷\udfb9튻쪽뗁ꯃ듅곇뫉뻋ꇍ돏럑ꟓꗕ뇗듙믛돝賟쳡郣菥藧髩胫迭蓯韱\udaf3鯵駷鏹鋻헽磿漁栃", a_))
					{
						num = 1;
						continue;
					}
					goto IL_3B3;
				case 26:
					A_0.DetectedFormatType = FileFormat.Dotm2010;
					num = 14;
					continue;
				}
				break;
				IL_131:
				num = 8;
				continue;
				IL_167:
				num = 7;
				continue;
				IL_1A3:
				A_0.DocxPackage.ᜁ()[ClipboardData.b("㵥⭧թɫᩭᕯᱱs⥵ⱷ͹౻᭽\udf81ꪃﺅ", a_)].ᜁ().Position = 0L;
				num = 21;
				continue;
				IL_274:
				xmlReader.Read();
				num = 17;
				continue;
				IL_3B3:
				num = 18;
			}
		}
		IL_1E9:
		throw new XmlException(ClipboardData.b("㉥gͩὫ乭᥯ű味ᡵ᝷๹屻ώꁿ욁ꪉ煉늛", a_));
		IL_30D:
		throw new XmlException(ClipboardData.b("⍥ၧᩩ५൭ѯ᝱ၳ噵w᝹ၻ幽ꚅꪇ\ude89ﺍ뚓", a_));
	}

	// Token: 0x06001E72 RID: 7794 RVA: 0x001E398C File Offset: 0x001E298C
	internal void ᜀ(spr\u1FDD A_0, spr᪆ A_1, Document A_2)
	{
		int a_ = 12;
		for (;;)
		{
			this.ᜂ = A_0;
			this.ᜄ = A_2;
			this.ᜀ(A_1);
			this.ᜁ(A_1);
			sprᭇ sprᭇ = A_1.ᜁ(ClipboardData.b("ᅱųյ౷ᕹᅻ⭽쥿궁", a_));
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (sprᭇ.ᜃ() == ClipboardData.b("ᅱųյ౷ᕹᅻ♽ꮃ", a_))
					{
						num = 5;
						continue;
					}
					goto IL_116;
				case 1:
					goto IL_B5;
				case 2:
					goto IL_116;
				case 3:
					this.ᜄ.CustomUIPartContainer = sprᭇ;
					num = 1;
					continue;
				case 4:
					if (sprᭇ.ᜁ().ContainsKey(ClipboardData.b("ᅱųյ౷ᕹᅻ⭽쥿겁ﲃ", a_)))
					{
						goto IL_7D;
					}
					goto IL_B5;
				case 5:
					this.ᜄ.CustomXMLContainer = sprᭇ;
					num = 2;
					continue;
				}
				break;
				IL_7D:
				if (true)
				{
				}
				num = 3;
				continue;
				IL_B5:
				sprᭇ = A_1.ᜁ(ClipboardData.b("ᅱųյ౷ᕹᅻ♽ꮃ", a_));
				num = 0;
				continue;
				IL_116:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7D;
				default:
					goto IL_12C;
				}
			}
		}
		IL_12C:
		if (false)
		{
		}
		this.ᜂ.ᜁ();
	}

	// Token: 0x06001E73 RID: 7795 RVA: 0x001E3AD8 File Offset: 0x001E2AD8
	private void ᜁ(spr᪆ A_0)
	{
		int a_ = 18;
		switch (0)
		{
		default:
			for (;;)
			{
				sprᭇ sprᭇ = A_0.ᜁ(ClipboardData.b("ཷᕹ๻᩽꽿ꎋ", a_));
				int num = 4;
				for (;;)
				{
					spr\u22A5 spr_u22A;
					Dictionary<string, spr\u22A5>.KeyCollection.Enumerator enumerator;
					switch (num)
					{
					case 0:
						goto IL_27C;
					case 1:
						num = 21;
						continue;
					case 2:
						if (sprᭇ.ᜁ().ContainsKey(ClipboardData.b("୷όࡻ੽ꚇ", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_2EE;
					case 3:
						num = 29;
						continue;
					case 4:
						if (sprᭇ.ᜁ().ContainsKey(ClipboardData.b("౷ቹ᥻፽뎁ꪃﺅ", a_)))
						{
							num = 26;
							continue;
						}
						goto IL_60B;
					case 5:
						this.ᜉ(sprᭇ.ᜁ()[ClipboardData.b("ṷᕹቻ੽푿ꒉﲏ", a_)].ᜁ());
						num = 0;
						continue;
					case 6:
						if (sprᭇ.ᜁ().ContainsKey(ClipboardData.b("୷๹ջችꪃﺅ", a_)))
						{
							num = 18;
							continue;
						}
						goto IL_36E;
					case 7:
						if (sprᭇ.ᜁ().ContainsKey(ClipboardData.b("ṷᕹቻ੽푿ꒉﲏ", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_27C;
					case 8:
						this.ᜃ = spr\u23D7.ᜀ(spr_u22A.ᜁ());
						this.\u1712(this.ᜃ);
						this.ᜃ.Close();
						num = 30;
						continue;
					case 9:
						goto IL_2EE;
					case 10:
						if (sprᭇ.ᜁ().ContainsKey(ClipboardData.b("๷᡹ᵻ⹽ﺉꊋ憐ﲑ", a_)))
						{
							num = 16;
							continue;
						}
						goto IL_329;
					case 11:
						this.ᜀ(sprᭇ.ᜁ()[ClipboardData.b("୷όࡻ੽ꚇ", a_)].ᜁ());
						num = 9;
						continue;
					case 12:
						goto IL_23F;
					case 13:
					{
						sprℏ sprℏ = sprᭇ.ᜂ()[ClipboardData.b("ཷᕹ๻᩽꽿\udd81黎ꎋﾏﮕﶗ낝\ud89f쾡좣袥\udaa7쾩삫\uddad", a_)];
						this.ᜁ(sprℏ.ᜁ());
						num = 14;
						continue;
					}
					case 14:
						goto IL_491;
					case 15:
						if (sprᭇ.ᜂ().ContainsKey(ClipboardData.b("ཷᕹ๻᩽꽿\udd81黎ꎋﾏﮕﶗ낝\ud89f쾡좣袥\udaa7쾩삫\uddad", a_)))
						{
							num = 13;
							continue;
						}
						goto IL_491;
					case 16:
						this.ᜋ(sprᭇ.ᜁ()[ClipboardData.b("๷᡹ᵻ⹽ﺉꊋ憐ﲑ", a_)].ᜁ());
						num = 23;
						continue;
					case 17:
						goto IL_36E;
					case 18:
						this.ᜃ = spr\u23D7.ᜀ(sprᭇ.ᜁ()[ClipboardData.b("୷๹ջችꪃﺅ", a_)].ᜁ());
						this.ᜋ(this.ᜃ);
						this.ᜃ.Close();
						num = 17;
						continue;
					case 19:
						goto IL_60B;
					case 20:
						this.ᜊ(sprᭇ.ᜁ()[ClipboardData.b("๷᡹ᵻ㩽ꢅ", a_)].ᜁ());
						num = 27;
						continue;
					case 21:
						if (spr_u22A.ᜁ() != null)
						{
							num = 3;
							continue;
						}
						goto IL_512;
					case 22:
						try
						{
							num = 6;
							for (;;)
							{
								IL_12F:
								switch (num)
								{
								case 0:
									num = 7;
									continue;
								case 1:
									num = 3;
									continue;
								case 2:
									while (enumerator.MoveNext())
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
											string text = enumerator.Current;
											num = 4;
											goto IL_12F;
										}
										}
									}
									num = 1;
									continue;
								case 3:
									goto IL_22C;
								case 4:
								{
									string text;
									string a;
									if ((a = text) != null)
									{
										num = 0;
										continue;
									}
									break;
								}
								case 7:
								{
									string a;
									if (a == ClipboardData.b("ᱷᕹύ୽ꚇ", a_))
									{
										num = 8;
										continue;
									}
									break;
								}
								case 8:
									this.ᜈ(sprᭇ.ᜁ()[ClipboardData.b("ᱷᕹύ୽ꚇ", a_)].ᜁ());
									num = 5;
									continue;
								}
								IL_1CB:
								num = 2;
								continue;
								goto IL_1CB;
							}
							IL_22C:
							goto IL_57F;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_23F;
						IL_57F:
						num = 2;
						continue;
					case 23:
						goto IL_329;
					case 24:
						if (sprᭇ.ᜁ().ContainsKey(ClipboardData.b("๷᡹ᵻ㩽ꢅ", a_)))
						{
							num = 20;
							continue;
						}
						return;
					case 25:
						if (spr_u22A != null)
						{
							num = 1;
							continue;
						}
						goto IL_512;
					case 26:
						this.\u170D(sprᭇ.ᜁ()[ClipboardData.b("౷ቹ᥻፽뎁ꪃﺅ", a_)].ᜁ());
						num = 19;
						continue;
					case 27:
						return;
					case 28:
						if (sprᭇ.ᜁ().ContainsKey(ClipboardData.b("ᙷཹᅻᱽꒉﲏ", a_)))
						{
							num = 12;
							continue;
						}
						goto IL_512;
					case 29:
						if (spr_u22A.ᜁ().Length > 0L)
						{
							if (true)
							{
							}
							num = 8;
							continue;
						}
						goto IL_512;
					case 30:
						goto IL_512;
					}
					break;
					IL_23F:
					spr_u22A = sprᭇ.ᜁ()[ClipboardData.b("ᙷཹᅻᱽꒉﲏ", a_)];
					num = 25;
					continue;
					IL_27C:
					num = 10;
					continue;
					IL_2EE:
					num = 7;
					continue;
					IL_329:
					num = 24;
					continue;
					IL_36E:
					enumerator = sprᭇ.ᜁ().Keys.GetEnumerator();
					num = 22;
					continue;
					IL_491:
					sprᭇ = A_0.ᜁ(ClipboardData.b("ཷᕹ๻᩽꽿", a_));
					num = 28;
					continue;
					IL_512:
					num = 6;
					continue;
					IL_60B:
					sprᭇ = A_0.ᜁ(ClipboardData.b("ཷᕹ๻᩽꽿", a_));
					num = 15;
				}
			}
			return;
		}
	}

	// Token: 0x06001E74 RID: 7796 RVA: 0x001E415C File Offset: 0x001E315C
	private void ᜁ(string A_0, string A_1)
	{
		for (;;)
		{
			IL_30:
			sprᭇ sprᭇ = this.ᜄ.DocxPackage.ᜁ(A_1);
			int num = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						if (sprᭇ.ᜁ().ContainsKey(A_0))
						{
							num = 2;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
					{
						spr\u22A5 spr_u22A = sprᭇ.ᜁ()[A_0];
						sprᭇ.ᜁ().Remove(A_0);
						spr_u22A.ᜁ().Close();
						if (true)
						{
						}
						num = 1;
						continue;
					}
					}
					goto IL_30;
				}
			}
		}
	}

	// Token: 0x06001E75 RID: 7797 RVA: 0x001E4214 File Offset: 0x001E3214
	private void \u170D(Stream A_0)
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
		this.ᜅ = this.ᜀ(A_0, true);
		this.ᜆ = this.ᜀ(A_0, false);
		this.ᜌ(A_0);
	}

	// Token: 0x06001E76 RID: 7798 RVA: 0x001E4274 File Offset: 0x001E3274
	private string ᜀ(Stream A_0, bool A_1)
	{
		int a_ = 5;
		int num = 8;
		string attribute;
		for (;;)
		{
			XmlReader xmlReader;
			string text;
			switch (num)
			{
			case 0:
				if (xmlReader.EOF)
				{
					num = 4;
					continue;
				}
				goto IL_FF;
			case 1:
				if (!xmlReader.Read())
				{
					num = 2;
					continue;
				}
				num = 10;
				continue;
			case 2:
				goto IL_11D;
			case 3:
				goto IL_E2;
			case 4:
				goto IL_18A;
			case 5:
				num = 9;
				continue;
			case 6:
				text = ClipboardData.b("٪౬ծṰŲ㍴ᡶ᝸ེ", a_);
				goto IL_146;
			case 7:
				goto IL_A5;
			case 8:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E2;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 9:
				text = ClipboardData.b("٪ѬŮṰŲ㍴ᡶ᝸ེ", a_);
				goto IL_146;
			case 10:
				if (xmlReader.LocalName == ClipboardData.b("ݪ౬᭮ᡰᵲ", a_))
				{
					num = 3;
					continue;
				}
				goto IL_FF;
			case 11:
				if (attribute != null)
				{
					num = 7;
					continue;
				}
				goto IL_FF;
			}
			if (!A_1)
			{
				num = 5;
				continue;
			}
			num = 6;
			continue;
			IL_E2:
			attribute = xmlReader.GetAttribute(ClipboardData.b("ὪᑬὮᑰᕲᑴᑶᱸ", a_));
			num = 11;
			continue;
			IL_FF:
			num = 1;
			continue;
			IL_146:
			string localName = text;
			A_0.Position = 0L;
			xmlReader = XmlReader.Create(A_0);
			xmlReader.ReadToFollowing(localName, ClipboardData.b("ͪᥬ᭮Ű䥲婴塶੸᡺ᕼ᩾ꦆﮊﺒ璉ﺞ햠킢认좦\udba8첪芬쮮쎰튲슴\udeb6ힸ\udcba킼펾￈ꃌ껎룐뷒", a_));
			num = 0;
		}
		IL_A5:
		A_0.Position = 0L;
		return attribute;
		IL_11D:
		return null;
		IL_18A:
		return null;
	}

	// Token: 0x06001E77 RID: 7799 RVA: 0x001E4410 File Offset: 0x001E3410
	private void ᜌ(Stream A_0)
	{
		int a_ = 2;
		switch (0)
		{
		default:
			for (;;)
			{
				string localName = ClipboardData.b("୧٩ṫ㵭፯ᩱᅳ᭵ᵷ", a_);
				A_0.Position = 0L;
				XmlReader xmlReader = XmlReader.Create(A_0);
				xmlReader.ReadToFollowing(localName, ClipboardData.b("gṩᡫṭ䩯嵱孳յ᭷ቹ᥻፽ꪃﶏﺑ秊ﶛ펟財쮣풥쾧薩좫\udcad톯얱\uddb3\ud8b5\udfb7ힹ킻醽Ꟊ귋ꟍ뻏", a_));
				int num = 22;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_E4;
					case 1:
					{
						string localName2;
						if (!(localName2 == ClipboardData.b("᭧፩Ὣ⵭ᱯq", a_)))
						{
							num = 21;
							continue;
						}
						string attribute = xmlReader.GetAttribute(ClipboardData.b("ѧ୩Ὣᩭ㍯ṱٳ", a_));
						string attribute2 = xmlReader.GetAttribute(ClipboardData.b("ṧ୩k", a_));
						num = 18;
						continue;
					}
					case 2:
						num = 8;
						continue;
					case 3:
						goto IL_171;
					case 4:
						num = 1;
						continue;
					case 5:
						goto IL_171;
					case 6:
						return;
					case 7:
						if (xmlReader.NodeType == XmlNodeType.Element)
						{
							num = 2;
							continue;
						}
						return;
					case 8:
					{
						if (!(xmlReader.LocalName != ClipboardData.b("୧٩ṫ㵭፯ᩱᅳ᭵ᵷ", a_)))
						{
							num = 6;
							continue;
						}
						string localName3 = xmlReader.LocalName;
						xmlReader.Read();
						num = 5;
						continue;
					}
					case 9:
						return;
					case 10:
						if (xmlReader.NodeType == XmlNodeType.Element)
						{
							num = 12;
							continue;
						}
						goto IL_144;
					case 11:
					{
						string attribute;
						string attribute2;
						this.ᜄ.ColorScheme.Add(attribute2, attribute);
						num = 0;
						continue;
					}
					case 12:
						num = 25;
						continue;
					case 13:
					{
						string localName2;
						if (!(localName2 == ClipboardData.b("᭧ᡩ୫౭㍯ṱٳ", a_)))
						{
							num = 24;
							continue;
						}
						string attribute3 = xmlReader.GetAttribute(ClipboardData.b("ṧ୩k", a_));
						string localName3;
						this.ᜄ.ColorScheme.Add(localName3, attribute3);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_171;
						default:
							if (false)
							{
							}
							num = 16;
							continue;
						}
						break;
					}
					case 14:
						if (true)
						{
						}
						goto IL_144;
					case 15:
					{
						string localName2;
						if ((localName2 = xmlReader.LocalName) != null)
						{
							num = 4;
							continue;
						}
						goto IL_E4;
					}
					case 16:
						goto IL_E4;
					case 17:
						goto IL_197;
					case 18:
					{
						string attribute2;
						if (attribute2 != null)
						{
							num = 11;
							continue;
						}
						string attribute;
						string localName3;
						this.ᜄ.ColorScheme.Add(localName3, attribute);
						num = 23;
						continue;
					}
					case 19:
						goto IL_197;
					case 20:
						goto IL_E4;
					case 21:
						num = 13;
						continue;
					case 22:
						if (xmlReader.EOF)
						{
							num = 9;
							continue;
						}
						xmlReader.Read();
						num = 19;
						continue;
					case 23:
						goto IL_E4;
					case 24:
						num = 20;
						continue;
					case 25:
					{
						string localName3;
						if (!(xmlReader.LocalName != localName3))
						{
							num = 14;
							continue;
						}
						num = 15;
						continue;
					}
					}
					break;
					IL_E4:
					xmlReader.Read();
					num = 3;
					continue;
					IL_144:
					xmlReader.Read();
					num = 17;
					continue;
					IL_171:
					num = 10;
					continue;
					IL_197:
					num = 7;
				}
			}
			return;
		}
	}

	// Token: 0x06001E78 RID: 7800 RVA: 0x001E47B4 File Offset: 0x001E37B4
	private void ᜀ(spr᪆ A_0)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			sprᭇ sprᭇ = A_0.ᜁ(ClipboardData.b("ᵸᑺṼ⽾Ꚉ", a_));
			using (Dictionary<string, spr\u22A5>.KeyCollection.Enumerator enumerator = sprᭇ.ᜁ().Keys.GetEnumerator())
			{
				int num = 5;
				for (;;)
				{
					string a;
					Stream a_2;
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
							num = 7;
							continue;
						case 1:
						{
							string text;
							if ((a = text) != null)
							{
								num = 15;
								continue;
							}
							break;
						}
						case 2:
							goto IL_27C;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num = 12;
								continue;
							}
							string text = enumerator.Current;
							a_2 = sprᭇ.ᜁ()[text].ᜁ();
							num = 1;
							continue;
						}
						case 4:
							if (!(a == ClipboardData.b("᩸๺๼୾ꮄﾆ", a_)))
							{
								num = 0;
								continue;
							}
							this.ᜃ = spr\u23D7.ᜀ(a_2);
							this.\u1715(this.ᜃ);
							this.ᜃ.Close();
							num = 11;
							continue;
						case 8:
							if (!(a == ClipboardData.b("ᡸ୺ർ兾呂", a_)))
							{
								num = 14;
								continue;
							}
							this.ᜃ = spr\u23D7.ᜀ(a_2);
							this.\u1713(this.ᜃ);
							this.ᜃ.Close();
							num = 10;
							continue;
						case 9:
							num = 4;
							continue;
						case 12:
							num = 2;
							continue;
						case 13:
							goto IL_1DD;
						case 14:
							num = 13;
							continue;
						case 15:
							num = 8;
							continue;
						}
						IL_19A:
						num = 3;
						continue;
						goto IL_19A;
					}
					IL_1DD:
					if (!(a == ClipboardData.b("᩸ᑺོ᩾꾀ﮂ", a_)))
					{
						num = 9;
					}
					else
					{
						this.ᜃ = spr\u23D7.ᜀ(a_2);
						this.\u1714(this.ᜃ);
						this.ᜃ.Close();
						num = 6;
					}
				}
				IL_27C:;
			}
			return;
		}
		}
	}

	// Token: 0x06001E79 RID: 7801 RVA: 0x001E4A6C File Offset: 0x001E3A6C
	private void ᜋ(Stream A_0)
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
		this.ᜄ.VbaProject = A_0;
	}

	// Token: 0x06001E7A RID: 7802 RVA: 0x001E4AB4 File Offset: 0x001E3AB4
	private void ᜊ(Stream A_0)
	{
		int a_ = 15;
		for (;;)
		{
			XmlReader xmlReader = spr\u23D7.ᜀ(A_0);
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
						goto IL_1A8;
					default:
						if (false)
						{
						}
						if (xmlReader == null)
						{
							num = 12;
							continue;
						}
						goto IL_267;
					}
					break;
				case 1:
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						num = 15;
						continue;
					}
					goto IL_18D;
				case 2:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᅴᡶ᩸㹺୼᩾", a_)))
					{
						num = 5;
						continue;
					}
					xmlReader.Read();
					this.ᜠ(xmlReader);
					num = 23;
					continue;
				}
				case 3:
					if (!(xmlReader.LocalName != ClipboardData.b("ʹᕶᡸ⡺ࡼཾ잂", a_)))
					{
						num = 16;
						continue;
					}
					num = 1;
					continue;
				case 4:
				{
					string localName;
					if ((localName = xmlReader.LocalName) != null)
					{
						num = 10;
						continue;
					}
					goto IL_18D;
				}
				case 5:
					num = 13;
					continue;
				case 6:
					goto IL_13F;
				case 7:
					goto IL_18D;
				case 8:
					num = 2;
					continue;
				case 9:
					if (xmlReader.IsEmptyElement)
					{
						num = 19;
						continue;
					}
					goto IL_1A8;
				case 10:
					num = 20;
					continue;
				case 11:
					goto IL_1FA;
				case 12:
					goto IL_AD;
				case 13:
					goto IL_18D;
				case 14:
					num = 17;
					continue;
				case 15:
					num = 4;
					continue;
				case 16:
					return;
				case 17:
					if (xmlReader.LocalName != ClipboardData.b("ʹᕶᡸ⡺ࡼཾ잂", a_))
					{
						num = 6;
						continue;
					}
					num = 9;
					continue;
				case 18:
					goto IL_1FA;
				case 19:
					return;
				case 20:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᡴᑶᵸࡺ", a_)))
					{
						num = 8;
						continue;
					}
					xmlReader.Read();
					this.ᜡ(xmlReader);
					num = 7;
					continue;
				}
				case 21:
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						num = 14;
						continue;
					}
					xmlReader.Read();
					num = 22;
					continue;
				case 22:
					goto IL_267;
				case 23:
					goto IL_18D;
				}
				break;
				IL_18D:
				xmlReader.Read();
				this.ᜀ(xmlReader);
				num = 18;
				continue;
				IL_1A8:
				xmlReader.Read();
				this.ᜀ(xmlReader);
				if (true)
				{
				}
				num = 11;
				continue;
				IL_1FA:
				num = 3;
				continue;
				IL_267:
				num = 21;
			}
		}
		IL_AD:
		throw new Exception(ClipboardData.b("ݴቶᡸὺ᡼ൾꆀꞆﺊ", a_));
		IL_13F:
		throw new XmlException(ClipboardData.b("ぴྲྀॸṺṼ୾ꖄﾆ권ﮎ떔떖連ﲜ첞풠펢햤좨\udfaa첬趮", a_));
	}

	// Token: 0x06001E7B RID: 7803 RVA: 0x001E4DAC File Offset: 0x001E3DAC
	private void ᜡ(XmlReader A_0)
	{
		int a_ = 17;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_2AE;
			case 1:
				goto IL_F6;
			case 2:
			{
				sprᴚ sprᴚ = new sprᴚ();
				sprᴚ.ᜂ(A_0.GetAttribute(ClipboardData.b("᥶ᡸᙺ᡼", a_), ClipboardData.b("ὶ൸ེർ䕾꺀겂붒ﺖ滛캠얢톤覦쪨쒪사肮\udeb0햲펴\udeb6\udab8\udeba銼좾껀뇂ꇄ﯈﯊﷌硫ﻐꓒ뫔ꗖ뷘뛚뇜", a_)));
				sprᴚ.ᜁ(A_0.GetAttribute(ClipboardData.b("ᕶ㱸ᕺṼൾ", a_), ClipboardData.b("ὶ൸ེർ䕾꺀겂붒ﺖ滛캠얢톤覦쪨쒪사肮\udeb0햲펴\udeb6\udab8\udeba銼좾껀뇂ꇄ﯈﯊﷌硫ﻐꓒ뫔ꗖ뷘뛚뇜", a_)));
				sprᴚ.ᜀ(A_0.GetAttribute(ClipboardData.b("ᑶᑸᱺ", a_), ClipboardData.b("ὶ൸ེർ䕾꺀겂붒ﺖ滛캠얢톤覦쪨쒪사肮\udeb0햲펴\udeb6\udab8\udeba銼좾껀뇂ꇄ﯈﯊﷌硫ﻐꓒ뫔ꗖ뷘뛚뇜", a_)));
				this.ᜄ.VbaData.Add(sprᴚ);
				num = 1;
				continue;
			}
			case 4:
				goto IL_14A;
			case 5:
				num = 8;
				continue;
			case 6:
				return;
			case 7:
				goto IL_76;
			case 8:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 15;
					continue;
				}
				goto IL_F6;
			}
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7B;
				default:
					if (false)
					{
					}
					if (!(A_0.LocalName != ClipboardData.b("᩶᩸ὺ๼", a_)))
					{
						num = 6;
						continue;
					}
					num = 13;
					continue;
				}
				break;
			case 10:
				if (A_0.LocalName != ClipboardData.b("᩶᩸ὺ", a_))
				{
					num = 0;
					continue;
				}
				this.ᜀ(A_0);
				num = 4;
				continue;
			case 11:
				goto IL_D2;
			case 12:
				goto IL_14A;
			case 13:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 5;
					continue;
				}
				goto IL_F6;
			case 14:
				num = 10;
				continue;
			case 15:
				goto IL_7B;
			case 16:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 14;
					continue;
				}
				A_0.Read();
				num = 11;
				continue;
			case 17:
			{
				string localName;
				if (localName == ClipboardData.b("᩶᩸ὺ", a_))
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				goto IL_F6;
			}
			}
			if (A_0 == null)
			{
				num = 7;
				continue;
			}
			goto IL_D2;
			IL_7B:
			num = 17;
			continue;
			IL_D2:
			num = 16;
			continue;
			IL_F6:
			A_0.Read();
			this.ᜀ(A_0);
			num = 12;
			continue;
			IL_14A:
			num = 9;
		}
		IL_76:
		throw new Exception(ClipboardData.b("նᱸ᩺᥼᩾ꎂꦈ﶐", a_));
		IL_2AE:
		throw new XmlException(ClipboardData.b("㉶Ÿ୺᡼᱾Ꞇ꾎랖뮘ﺜﮞ莠", a_));
	}

	// Token: 0x06001E7C RID: 7804 RVA: 0x001E506C File Offset: 0x001E406C
	private void ᜠ(XmlReader A_0)
	{
		int a_ = 13;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜀ(A_0);
				num = 1;
				continue;
			case 1:
				goto IL_DF;
			case 2:
				if (!(A_0.LocalName != ClipboardData.b("ᝲᩴᑶ㱸ൺ᡼ᅾ", a_)))
				{
					num = 6;
					continue;
				}
				num = 3;
				continue;
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
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 11;
						continue;
					}
					goto IL_126;
				}
				break;
			case 4:
				goto IL_61;
			case 5:
				goto IL_DF;
			case 6:
				return;
			case 8:
				goto IL_155;
			case 9:
				goto IL_126;
			case 10:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 0;
					continue;
				}
				if (true)
				{
				}
				A_0.Read();
				num = 8;
				continue;
			case 11:
				this.ᜄ.DocEvents.Add(A_0.LocalName);
				num = 9;
				continue;
			}
			IL_53:
			if (A_0 == null)
			{
				num = 4;
				continue;
			}
			goto IL_155;
			goto IL_53;
			IL_DF:
			num = 2;
			continue;
			IL_126:
			A_0.Read();
			this.ᜀ(A_0);
			num = 5;
			continue;
			IL_155:
			num = 10;
		}
		IL_61:
		throw new Exception(ClipboardData.b("ŲၴᙶᵸṺོ彾ꖄﲈ", a_));
	}

	// Token: 0x06001E7D RID: 7805 RVA: 0x001E51F4 File Offset: 0x001E41F4
	private void ᜉ(Stream A_0)
	{
		int a_ = 0;
		if (true)
		{
		}
		for (;;)
		{
			XmlReader xmlReader = spr\u23D7.ᜀ(A_0);
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 18;
					continue;
				case 1:
					return;
				case 2:
					return;
				case 3:
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						num = 16;
						continue;
					}
					goto IL_1E9;
				case 4:
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						num = 9;
						continue;
					}
					xmlReader.Read();
					num = 14;
					continue;
				case 5:
					goto IL_83;
				case 6:
					goto IL_1E9;
				case 7:
					if (xmlReader == null)
					{
						goto IL_7B;
					}
					goto IL_CB;
				case 8:
				{
					string localName;
					if ((localName = xmlReader.LocalName) != null)
					{
						num = 0;
						continue;
					}
					goto IL_1E9;
				}
				case 9:
					num = 15;
					continue;
				case 10:
					goto IL_17C;
				case 11:
					if (xmlReader.IsEmptyElement)
					{
						num = 2;
						continue;
					}
					xmlReader.Read();
					this.ᜀ(xmlReader);
					num = 10;
					continue;
				case 12:
					if (!(xmlReader.LocalName != ClipboardData.b("eݧѩᡫᵭ", a_)))
					{
						num = 1;
						continue;
					}
					num = 3;
					continue;
				case 13:
				{
					string attribute = xmlReader.GetAttribute(ClipboardData.b("ࡥ१ݩ५", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧\udda9쎫\udcad풯슱욳\ud9b5\udbb7\udfb9쾻춽ꦿ곁ꏃꯅ꓇ﻋﻍﯓ믕맗동닛", a_));
					this.ᜅ(xmlReader, attribute);
					num = 6;
					continue;
				}
				case 14:
					goto IL_CB;
				case 15:
					if (xmlReader.LocalName != ClipboardData.b("eݧѩᡫᵭ", a_))
					{
						num = 19;
						continue;
					}
					num = 11;
					continue;
				case 16:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7B;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 17:
					goto IL_17C;
				case 18:
				{
					string localName;
					if (localName == ClipboardData.b("eݧѩᡫ", a_))
					{
						num = 13;
						continue;
					}
					goto IL_1E9;
				}
				case 19:
					goto IL_1E7;
				}
				break;
				IL_7B:
				num = 5;
				continue;
				IL_CB:
				num = 4;
				continue;
				IL_17C:
				num = 12;
				continue;
				IL_1E9:
				xmlReader.Read();
				this.ᜀ(xmlReader);
				num = 17;
			}
		}
		IL_83:
		throw new Exception(ClipboardData.b("ᑥ൧୩࡫୭ɯ剱ᵳյ塷ᑹॻች", a_));
		IL_1E7:
		throw new XmlException(ClipboardData.b("⍥ၧᩩ५൭ѯ᝱ၳ噵w᝹ၻ幽ꚅꪇ뚓", a_));
	}

	// Token: 0x06001E7E RID: 7806 RVA: 0x001E5498 File Offset: 0x001E4498
	private void ᜅ(XmlReader A_0, string A_1)
	{
		int a_ = 12;
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_A2;
			case 1:
				num = 12;
				continue;
			case 2:
				if (A_0.IsEmptyElement)
				{
					num = 4;
					continue;
				}
				A_0.Read();
				this.ᜀ(A_0);
				num = 21;
				continue;
			case 3:
				if (!(A_0.LocalName != ClipboardData.b("ᑱ᭳ᡵ౷", a_)))
				{
					num = 20;
					continue;
				}
				num = 15;
				continue;
			case 4:
				return;
			case 5:
				if (A_0.LocalName != ClipboardData.b("ᑱ᭳ᡵ౷", a_))
				{
					if (true)
					{
					}
					num = 8;
					continue;
				}
				num = 2;
				continue;
			case 6:
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("ѱᕳ᩵", a_), ClipboardData.b("ᩱsɵࡷ䁹卻兽ﾋꂍﾏ쾟킡즣장\udca7\ud9a9芫솭슯햱鮳솵ힷ좹\ud8bb캽늿귁ꟃꏅ믇막ꗋꃍ럏뿑룓崙쿟迡藣迥蛧", a_));
				num = 9;
				continue;
			}
			case 7:
				num = 5;
				continue;
			case 8:
				goto IL_15B;
			case 9:
			{
				if (this.ᜄ.FontSubstitutionTable.ContainsKey(A_1))
				{
					num = 11;
					continue;
				}
				string attribute;
				this.ᜄ.FontSubstitutionTable.Add(A_1, attribute);
				num = 17;
				continue;
			}
			case 11:
			{
				string attribute;
				this.ᜄ.FontSubstitutionTable[A_1] = attribute;
				num = 16;
				continue;
			}
			case 12:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 22;
					continue;
				}
				goto IL_248;
			}
			case 13:
			{
				string localName;
				if (localName == ClipboardData.b("፱ᡳɵ㙷᭹ᅻ᭽", a_))
				{
					num = 6;
					continue;
				}
				goto IL_248;
			}
			case 14:
				goto IL_221;
			case 15:
				goto IL_103;
			case 16:
				goto IL_248;
			case 17:
				goto IL_248;
			case 18:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 7;
					continue;
				}
				A_0.Read();
				num = 14;
				continue;
			case 19:
				goto IL_1CD;
			case 20:
				return;
			case 21:
				goto IL_1CD;
			case 22:
				num = 13;
				continue;
			}
			if (A_0 != null)
			{
				goto IL_221;
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
			IL_103:
			if (A_0.NodeType == XmlNodeType.Element)
			{
				num = 1;
				continue;
			}
			goto IL_248;
			IL_1CD:
			num = 3;
			continue;
			IL_221:
			num = 18;
			continue;
			IL_248:
			A_0.Read();
			this.ᜀ(A_0);
			num = 19;
		}
		IL_A2:
		throw new Exception(ClipboardData.b("qᅳ᝵ᱷό๻幽ꒃﶇ", a_));
		IL_15B:
		throw new XmlException(ClipboardData.b("㝱౳ٵᵷ᥹ࡻ᭽ꊁﲃꪉ늑뚓벝", a_));
	}

	// Token: 0x06001E7F RID: 7807 RVA: 0x001E57A8 File Offset: 0x001E47A8
	private void ᜈ(Stream A_0)
	{
		int a_ = 7;
		XmlReader xmlReader;
		for (;;)
		{
			xmlReader = spr\u23D7.ᜀ(A_0);
			int num = 26;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_195;
				case 1:
					goto IL_158;
				case 2:
					return;
				case 3:
					if (!xmlReader.IsEmptyElement)
					{
						bool flag = false;
						xmlReader.Read();
						this.ᜀ(xmlReader);
						num = 10;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C1;
					default:
						if (false)
						{
						}
						num = 24;
						continue;
					}
					break;
				case 4:
					goto IL_A4;
				case 5:
				{
					string localName;
					if (!(localName == ClipboardData.b("ཬ๮ተᡲቴնᙸ๺፼᭾", a_)))
					{
						num = 12;
						continue;
					}
					this.\u1716(xmlReader);
					bool flag = true;
					num = 23;
					continue;
				}
				case 6:
					num = 5;
					continue;
				case 7:
					goto IL_2EA;
				case 8:
					if (xmlReader.LocalName != ClipboardData.b("६nተٲᡴቶ᝸ེ", a_))
					{
						num = 21;
						continue;
					}
					num = 3;
					continue;
				case 9:
					if (true)
					{
					}
					num = 16;
					continue;
				case 10:
					goto IL_204;
				case 11:
				{
					string localName;
					if (!(localName == ClipboardData.b("ཬnᕰੲ", a_)))
					{
						num = 9;
						continue;
					}
					this.ᜄ.AddSection();
					this.ᜏ(xmlReader, null);
					num = 7;
					continue;
				}
				case 12:
					num = 11;
					continue;
				case 13:
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						num = 22;
						continue;
					}
					xmlReader.Read();
					num = 1;
					continue;
				case 14:
				{
					string localName;
					if ((localName = xmlReader.LocalName) != null)
					{
						goto IL_1C1;
					}
					goto IL_2EA;
				}
				case 15:
					goto IL_195;
				case 16:
					goto IL_2EA;
				case 17:
				{
					bool flag;
					if (!flag)
					{
						num = 27;
						continue;
					}
					goto IL_195;
				}
				case 18:
					goto IL_204;
				case 19:
				{
					if (!(xmlReader.LocalName != ClipboardData.b("६nተٲᡴቶ᝸ེ", a_)))
					{
						num = 2;
						continue;
					}
					bool flag = false;
					num = 20;
					continue;
				}
				case 20:
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						num = 25;
						continue;
					}
					xmlReader.Read();
					num = 15;
					continue;
				case 21:
					goto IL_135;
				case 22:
					num = 8;
					continue;
				case 23:
					goto IL_2EA;
				case 24:
					return;
				case 25:
					num = 14;
					continue;
				case 26:
					if (xmlReader == null)
					{
						num = 4;
						continue;
					}
					goto IL_158;
				case 27:
					xmlReader.Read();
					num = 0;
					continue;
				}
				break;
				IL_158:
				num = 13;
				continue;
				IL_195:
				this.ᜀ(xmlReader);
				num = 18;
				continue;
				IL_1C1:
				num = 6;
				continue;
				IL_204:
				num = 19;
				continue;
				IL_2EA:
				num = 17;
			}
		}
		IL_A4:
		throw new Exception(ClipboardData.b("Ὤ੮ၰᝲၴն", a_));
		IL_135:
		throw new XmlException(ClipboardData.b("㡬Ůᑰ୲մቶེ᩸᡼᭾ꆀﮂꦈﾊ놐", a_) + xmlReader.LocalName);
	}

	// Token: 0x06001E80 RID: 7808 RVA: 0x001E5B08 File Offset: 0x001E4B08
	private void ᜏ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 58;
			for (;;)
			{
				IParagraph paragraph;
				ITable table;
				switch (num)
				{
				case 0:
					if (paragraph.Owner is Body)
					{
						num = 81;
						continue;
					}
					goto IL_701;
				case 1:
					goto IL_701;
				case 2:
					spr᧓.ᝊ = new Dictionary<string, int>(10)
					{
						{
							ClipboardData.b("Ͳ", a_),
							0
						},
						{
							ClipboardData.b("ݲ᝴᭶", a_),
							1
						},
						{
							ClipboardData.b("rၴᑶ൸⭺ོ", a_),
							2
						},
						{
							ClipboardData.b("ᅲᩴᡶቸᙺᱼൾ킂ﮈﾊ", a_),
							3
						},
						{
							ClipboardData.b("ᅲᩴᡶቸᙺᱼൾ욂", a_),
							4
						},
						{
							ClipboardData.b("ၲᩴ᩶ᑸṺ፼୾", a_),
							5
						},
						{
							ClipboardData.b("ၲᩴ᩶ᑸṺ፼୾펀\ud88a歷", a_),
							6
						},
						{
							ClipboardData.b("ၲᩴ᩶ᑸṺ፼୾펀캊", a_),
							7
						},
						{
							ClipboardData.b("rᅴͶ", a_),
							8
						},
						{
							ClipboardData.b("ቲᥴͶ㩸፺ࡼᅾ", a_),
							9
						}
					};
					num = 17;
					continue;
				case 3:
					if (paragraph.Items.Count > 0)
					{
						num = 12;
						continue;
					}
					goto IL_701;
				case 4:
				{
					bool flag = false;
					num = 21;
					continue;
				}
				case 5:
				{
					int num2;
					paragraph.Items.RemoveAt(num2);
					num = 7;
					continue;
				}
				case 6:
					return;
				case 7:
					if (paragraph.Items.Count == 0)
					{
						num = 48;
						continue;
					}
					goto IL_701;
				case 8:
					goto IL_A03;
				case 9:
					goto IL_701;
				case 10:
					if (paragraph.Owner.Owner is Section)
					{
						num = 32;
						continue;
					}
					goto IL_701;
				case 11:
					if (spr᧓.ᝊ == null)
					{
						num = 2;
						continue;
					}
					goto IL_398;
				case 12:
					num = 66;
					continue;
				case 13:
					goto IL_6D8;
				case 14:
					if (!string.IsNullOrEmpty(paragraph.StyleName))
					{
						num = 64;
						continue;
					}
					goto IL_439;
				case 15:
				{
					bool flag;
					if (flag)
					{
						num = 84;
						continue;
					}
					goto IL_786;
				}
				case 16:
				{
					Section section;
					if (section != this.ᜄ.LastSection)
					{
						num = 35;
						continue;
					}
					goto IL_701;
				}
				case 17:
					goto IL_398;
				case 18:
				{
					Field field;
					if (field.Range.ᜁ().Contains(table))
					{
						num = 73;
						continue;
					}
					goto IL_27E;
				}
				case 19:
					goto IL_701;
				case 20:
				{
					string localName = A_0.LocalName;
					num = 80;
					continue;
				}
				case 21:
				{
					string localName2;
					if ((localName2 = A_0.LocalName) != null)
					{
						num = 53;
						continue;
					}
					goto IL_701;
				}
				case 22:
					if (table.Rows.Count <= 0)
					{
						num = 38;
						continue;
					}
					goto IL_701;
				case 23:
					goto IL_554;
				case 24:
				{
					int num2;
					if (paragraph.Items[num2] is Break)
					{
						num = 5;
						continue;
					}
					num2++;
					num = 75;
					continue;
				}
				case 25:
					A_0.Read();
					num = 8;
					continue;
				case 26:
					goto IL_439;
				case 27:
					return;
				case 28:
					num = 18;
					continue;
				case 29:
					goto IL_701;
				case 30:
					if (paragraph.Items != null)
					{
						num = 78;
						continue;
					}
					goto IL_701;
				case 31:
					goto IL_701;
				case 32:
				{
					Section section = paragraph.Owner.Owner as Section;
					num = 16;
					continue;
				}
				case 33:
				{
					Field field;
					if (field != null)
					{
						num = 55;
						continue;
					}
					goto IL_554;
				}
				case 34:
					goto IL_9A7;
				case 35:
				{
					int num2 = 0;
					num = 34;
					continue;
				}
				case 36:
				{
					string localName2;
					int num3;
					if (spr᧓.ᝊ.TryGetValue(localName2, out num3))
					{
						num = 52;
						continue;
					}
					goto IL_701;
				}
				case 37:
				{
					Field field;
					if (field.IsFieldRange)
					{
						goto IL_4C8;
					}
					goto IL_786;
				}
				case 38:
					this.ᜀ(A_1, table);
					num = 1;
					continue;
				case 39:
					num = 82;
					continue;
				case 40:
				{
					Field field;
					if (field != null)
					{
						num = 44;
						continue;
					}
					goto IL_635;
				}
				case 41:
					num = 37;
					continue;
				case 42:
					goto IL_27E;
				case 43:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 4;
						continue;
					}
					A_0.Read();
					num = 49;
					continue;
				case 44:
					if (true)
					{
					}
					num = 85;
					continue;
				case 45:
				{
					bool flag = true;
					num = 23;
					continue;
				}
				case 46:
					goto IL_701;
				case 47:
					goto IL_701;
				case 48:
					(paragraph.Owner as Body).ChildObjects.Remove(paragraph);
					num = 46;
					continue;
				case 49:
					goto IL_A03;
				case 50:
				{
					bool flag2;
					if (!flag2)
					{
						num = 25;
						continue;
					}
					goto IL_A03;
				}
				case 51:
					goto IL_701;
				case 52:
					num = 62;
					continue;
				case 53:
					num = 11;
					continue;
				case 54:
					goto IL_701;
				case 55:
					num = 67;
					continue;
				case 56:
					goto IL_786;
				case 57:
					goto IL_701;
				case 59:
				{
					Field field;
					field.IsFieldRange = false;
					num = 79;
					continue;
				}
				case 60:
				{
					Field field;
					if (field != null)
					{
						num = 41;
						continue;
					}
					goto IL_786;
				}
				case 61:
					goto IL_A03;
				case 62:
				{
					int num3;
					switch (num3)
					{
					case 0:
					{
						paragraph = this.ᜄ(A_1);
						Field field = this.\u1712();
						num = 33;
						continue;
					}
					case 1:
					{
						table = this.ᜁ(A_1);
						Field field = this.\u1712();
						num = 69;
						continue;
					}
					case 2:
						this.ᜁ(A_0, this.ᜄ.LastSection);
						num = 57;
						continue;
					case 3:
						this.ᜉ(A_0, null);
						num = 31;
						continue;
					case 4:
						this.ᜌ(A_0, A_1);
						num = 51;
						continue;
					case 5:
						this.\u171F(A_0);
						num = 19;
						continue;
					case 6:
						this.ᜏ(A_0, null);
						num = 77;
						continue;
					case 7:
						this.ᜎ(A_0, null);
						num = 9;
						continue;
					case 8:
					{
						spr\u2215 spr_u = this.ᜀ(A_1);
						this.ᜁ(A_0, spr_u as spr\u1AE7);
						num = 54;
						continue;
					}
					case 9:
						this.ᜀ(A_0, null);
						num = 65;
						continue;
					default:
						num = 76;
						continue;
					}
					break;
				}
				case 63:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4C8;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 64:
					paragraph.ApplyStyle(paragraph.StyleName);
					num = 26;
					continue;
				case 65:
					goto IL_701;
				case 66:
					if (!(paragraph.Owner is HeaderFooter))
					{
						num = 63;
						continue;
					}
					goto IL_701;
				case 67:
				{
					Field field;
					if (field.Range.ᜁ().Contains(paragraph))
					{
						num = 45;
						continue;
					}
					goto IL_554;
				}
				case 68:
					num = 15;
					continue;
				case 69:
				{
					Field field;
					if (field != null)
					{
						num = 28;
						continue;
					}
					goto IL_27E;
				}
				case 70:
				{
					int num2;
					if (num2 >= paragraph.Items.Count)
					{
						num = 74;
						continue;
					}
					num = 24;
					continue;
				}
				case 71:
				{
					string localName;
					if (!(A_0.LocalName != localName))
					{
						num = 6;
						continue;
					}
					num = 43;
					continue;
				}
				case 72:
					goto IL_196;
				case 73:
				{
					bool flag = true;
					num = 42;
					continue;
				}
				case 74:
					num = 29;
					continue;
				case 75:
					goto IL_9A7;
				case 76:
					num = 47;
					continue;
				case 77:
					goto IL_701;
				case 78:
					num = 3;
					continue;
				case 79:
					goto IL_635;
				case 80:
				{
					if (A_0.IsEmptyElement)
					{
						num = 27;
						continue;
					}
					bool flag2 = false;
					A_0.Read();
					this.ᜀ(A_0);
					num = 61;
					continue;
				}
				case 81:
					num = 10;
					continue;
				case 82:
				{
					bool flag;
					if (flag)
					{
						num = 59;
						continue;
					}
					goto IL_635;
				}
				case 83:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 20;
						continue;
					}
					A_0.Read();
					num = 13;
					continue;
				case 84:
				{
					Field field;
					field.IsFieldRange = false;
					num = 56;
					continue;
				}
				case 85:
				{
					Field field;
					if (field.IsFieldRange)
					{
						num = 39;
						continue;
					}
					goto IL_635;
				}
				}
				if (A_0 == null)
				{
					num = 72;
					continue;
				}
				goto IL_6D8;
				IL_27E:
				this.ᜅ(A_0, table as Table);
				num = 40;
				continue;
				IL_398:
				num = 36;
				continue;
				IL_439:
				num = 30;
				continue;
				IL_4C8:
				num = 68;
				continue;
				IL_554:
				this.ᜀ(paragraph);
				this.ᜐ(A_0, paragraph.Items);
				num = 60;
				continue;
				IL_635:
				num = 22;
				continue;
				IL_6D8:
				num = 83;
				continue;
				IL_701:
				num = 50;
				continue;
				IL_786:
				num = 14;
				continue;
				IL_9A7:
				num = 70;
				continue;
				IL_A03:
				num = 71;
			}
			IL_196:
			throw new ArgumentNullException(ClipboardData.b("ŲၴᙶᵸṺོ", a_));
		}
		}
	}

	// Token: 0x06001E81 RID: 7809 RVA: 0x001E65B4 File Offset: 0x001E55B4
	private IParagraph ᜄ(IDocumentObject A_0)
	{
		IParagraph paragraph;
		for (;;)
		{
			paragraph = null;
			int num = 14;
			for (;;)
			{
				switch (num)
				{
				case 0:
					paragraph = (A_0 as HeaderFooter).AddParagraph();
					if (true)
					{
					}
					num = 13;
					continue;
				case 1:
					goto IL_148;
				case 2:
					if (A_0 is Footnote)
					{
						num = 16;
						continue;
					}
					num = 4;
					continue;
				case 3:
					goto IL_148;
				case 4:
					if (A_0 is Comment)
					{
						num = 12;
						continue;
					}
					num = 15;
					continue;
				case 5:
					goto IL_148;
				case 6:
					goto IL_148;
				case 7:
					return paragraph;
				case 8:
					if (this.\u1712() != null)
					{
						num = 11;
						continue;
					}
					return paragraph;
				case 9:
					paragraph = (A_0 as spr\u1AE7).ᜆ().ᜂ().AddParagraph();
					num = 3;
					continue;
				case 10:
					this.\u1712().IsFieldRange = true;
					this.ᜀ(paragraph as Paragraph);
					num = 7;
					continue;
				case 11:
					num = 17;
					continue;
				case 12:
					paragraph = (A_0 as Comment).Body.AddParagraph();
					num = 1;
					continue;
				case 13:
					goto IL_148;
				case 14:
					if (A_0 is HeaderFooter)
					{
						num = 0;
						continue;
					}
					num = 2;
					continue;
				case 15:
					if (A_0 is spr\u1AE7)
					{
						num = 9;
						continue;
					}
					paragraph = this.ᜄ.LastSection.AddParagraph();
					num = 5;
					continue;
				case 16:
					paragraph = (A_0 as Footnote).TextBody.AddParagraph();
					num = 6;
					continue;
				case 17:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return paragraph;
					default:
						if (false)
						{
						}
						if (!this.\u1712().IsFieldRange)
						{
							num = 10;
							continue;
						}
						return paragraph;
					}
					break;
				}
				break;
				IL_148:
				num = 8;
			}
		}
		return paragraph;
	}

	// Token: 0x06001E82 RID: 7810 RVA: 0x001E67E4 File Offset: 0x001E57E4
	private void ᜀ(IParagraph A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_67;
			case 1:
				A_0.Items.Add(this.\u171F);
				this.\u171F = null;
				num = 0;
				continue;
			case 3:
				return;
			case 4:
				if (this.ᜠ != null)
				{
					if (true)
					{
					}
					num = 5;
					continue;
				}
				return;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					A_0.Items.Add(this.ᜠ);
					this.ᜠ = null;
					num = 3;
					continue;
				}
				break;
			}
			if (this.\u171F != null)
			{
				num = 1;
				continue;
			}
			IL_67:
			num = 4;
		}
	}

	// Token: 0x06001E83 RID: 7811 RVA: 0x001E68BC File Offset: 0x001E58BC
	private void ᜐ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 16;
			for (;;)
			{
				sprἤ sprἤ;
				switch (num)
				{
				case 0:
					goto IL_574;
				case 1:
				{
					int num2;
					switch (num2)
					{
					case 0:
					{
						Paragraph paragraph = A_1.OwnerBase as Paragraph;
						num = 34;
						continue;
					}
					case 1:
						this.ᜈ(A_0, A_1);
						this.\u1716 = null;
						num = 43;
						continue;
					case 2:
						this.ᜉ(A_0, A_1);
						num = 47;
						continue;
					case 3:
						this.ᜊ(A_0, A_1);
						num = 12;
						continue;
					case 4:
						if (true)
						{
						}
						this.ᜏ(A_0, A_1);
						num = 39;
						continue;
					case 5:
						this.ᜎ(A_0, A_1);
						num = 45;
						continue;
					case 6:
						this.\u171E = TrackChangeType.IsDelete;
						this.ᜐ(A_0, A_1);
						this.\u171E = TrackChangeType.None;
						num = 4;
						continue;
					case 7:
						this.\u171E = TrackChangeType.IsInsert;
						this.ᜐ(A_0, A_1);
						this.\u171E = TrackChangeType.None;
						num = 3;
						continue;
					case 8:
						this.\u170D(A_0, A_1);
						num = 31;
						continue;
					case 9:
					{
						bool flag = this.ᜋ(A_0, A_1);
						num = 5;
						continue;
					}
					case 10:
					{
						MemoryStream memoryStream;
						ParagraphBase entity = this.ᜀ(A_0, A_1, ref memoryStream);
						A_1.Add(entity);
						this.\u1716 = null;
						bool flag = true;
						num = 18;
						continue;
					}
					case 11:
						sprἤ = new sprờ(this.ᜄ);
						this.ᜁ(sprἤ as ParagraphBase, A_1);
						num = 6;
						continue;
					default:
						num = 30;
						continue;
					}
					break;
				}
				case 2:
					goto IL_37B;
				case 3:
					goto IL_6D6;
				case 4:
					goto IL_6D6;
				case 5:
					goto IL_6D6;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_55E;
					default:
						if (false)
						{
						}
						if (A_1.OwnerBase is Paragraph)
						{
							num = 28;
							continue;
						}
						goto IL_37B;
					}
					break;
				case 7:
					spr᧓.ᝋ = new Dictionary<string, int>(12)
					{
						{
							ClipboardData.b("ٵ⡷ࡹ", a_),
							0
						},
						{
							ClipboardData.b("ѵ", a_),
							1
						},
						{
							ClipboardData.b("ᑵ᝷ᕹ᝻፽햅ﲇﺋ揄", a_),
							2
						},
						{
							ClipboardData.b("ᑵ᝷ᕹ᝻፽쎅", a_),
							3
						},
						{
							ClipboardData.b("ᕵ᝷᝹ᅻ᭽횃\udd8d", a_),
							4
						},
						{
							ClipboardData.b("ᕵ᝷᝹ᅻ᭽횃쮍ﺏ", a_),
							5
						},
						{
							ClipboardData.b("ትᵷᙹ", a_),
							6
						},
						{
							ClipboardData.b("ήᙷॹ", a_),
							7
						},
						{
							ClipboardData.b("ṵŷ੹᥻౽", a_),
							8
						},
						{
							ClipboardData.b("ၵᑷṹ⽻᝽", a_),
							9
						},
						{
							ClipboardData.b("ት੷᭹୻᝽", a_),
							10
						},
						{
							ClipboardData.b("յᱷ๹", a_),
							11
						}
					};
					num = 0;
					continue;
				case 8:
					goto IL_699;
				case 9:
					if (spr᧓.ᝋ == null)
					{
						num = 7;
						continue;
					}
					goto IL_574;
				case 10:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 29;
						continue;
					}
					A_0.Read();
					num = 42;
					continue;
				case 11:
					goto IL_39A;
				case 12:
					goto IL_6D6;
				case 13:
				{
					Paragraph paragraph;
					if (paragraph.StyleName == string.Empty)
					{
						num = 8;
						continue;
					}
					goto IL_6D6;
				}
				case 14:
					num = 37;
					continue;
				case 15:
					goto IL_6D6;
				case 17:
					num = 9;
					continue;
				case 18:
					goto IL_6D6;
				case 19:
					if (this.ᜄ.Styles.FindByName(ClipboardData.b("㡵᝷ࡹᅻώ", a_)) != null)
					{
						num = 36;
						continue;
					}
					goto IL_6D6;
				case 20:
					return;
				case 21:
				{
					Paragraph paragraph;
					if (paragraph != null)
					{
						num = 27;
						continue;
					}
					goto IL_6D6;
				}
				case 22:
					goto IL_39A;
				case 23:
					goto IL_39A;
				case 24:
				{
					int num2;
					string localName;
					if (spr᧓.ᝋ.TryGetValue(localName, out num2))
					{
						num = 33;
						continue;
					}
					goto IL_6D6;
				}
				case 25:
				{
					Paragraph paragraph;
					if (paragraph.StyleName != null)
					{
						num = 26;
						continue;
					}
					goto IL_699;
				}
				case 26:
					num = 13;
					continue;
				case 27:
				{
					Paragraph paragraph;
					this.ᜋ(A_0, paragraph.Format);
					num = 25;
					continue;
				}
				case 28:
					(A_1.OwnerBase as Paragraph).ᜋ = true;
					num = 2;
					continue;
				case 29:
					num = 49;
					continue;
				case 30:
					num = 46;
					continue;
				case 31:
					goto IL_6D6;
				case 32:
					A_0.Read();
					num = 23;
					continue;
				case 33:
					num = 1;
					continue;
				case 34:
					if (A_1.Owner is spr\u1AD2)
					{
						num = 40;
						continue;
					}
					goto IL_3C8;
				case 35:
					goto IL_6D6;
				case 36:
				{
					Paragraph paragraph;
					paragraph.ApplyStyle(ClipboardData.b("㡵᝷ࡹᅻώ", a_));
					num = 35;
					continue;
				}
				case 37:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 17;
						continue;
					}
					goto IL_6D6;
				}
				case 38:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 14;
						continue;
					}
					A_0.Read();
					num = 22;
					continue;
				case 39:
					goto IL_55E;
				case 40:
				{
					Paragraph paragraph = A_1.Owner.Owner.Owner as Paragraph;
					num = 50;
					continue;
				}
				case 41:
				{
					string localName2;
					if (!(A_0.LocalName != localName2))
					{
						num = 44;
						continue;
					}
					bool flag = false;
					num = 38;
					continue;
				}
				case 43:
					goto IL_6D6;
				case 44:
					return;
				case 45:
					goto IL_6D6;
				case 46:
					goto IL_6D6;
				case 47:
					goto IL_6D6;
				case 48:
				{
					bool flag;
					if (!flag)
					{
						num = 32;
						continue;
					}
					goto IL_39A;
				}
				case 49:
				{
					if (A_0.IsEmptyElement)
					{
						num = 20;
						continue;
					}
					bool flag = false;
					string localName2 = A_0.LocalName;
					A_0.Read();
					MemoryStream memoryStream = null;
					this.ᜀ(A_0);
					num = 11;
					continue;
				}
				case 50:
					goto IL_3C8;
				}
				goto IL_F8;
				IL_37B:
				this.ᜀ(A_0, sprἤ as sprờ);
				num = 15;
				continue;
				IL_39A:
				num = 41;
				continue;
				IL_3C8:
				num = 21;
				continue;
				IL_574:
				num = 24;
				continue;
				IL_657:
				num = 10;
				continue;
				IL_F8:
				goto IL_657;
				IL_699:
				num = 19;
				continue;
				IL_6D6:
				num = 48;
				continue;
				IL_55E:
				goto IL_6D6;
			}
			return;
		}
		}
	}

	// Token: 0x06001E84 RID: 7812 RVA: 0x001E7040 File Offset: 0x001E6040
	private void \u171F(XmlReader A_0)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 8;
			Comment comment;
			string attribute3;
			for (;;)
			{
				string attribute;
				string attribute2;
				switch (num)
				{
				case 0:
					goto IL_E3;
				case 1:
					comment.Format.Initial = attribute;
					num = 7;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E3;
					default:
						if (false)
						{
						}
						comment.Format.Author = attribute2;
						num = 4;
						continue;
					}
					break;
				case 3:
					if (attribute != null)
					{
						num = 1;
						continue;
					}
					goto IL_1C5;
				case 4:
					if (true)
					{
					}
					goto IL_63;
				case 5:
					this.ᜡ = new Dictionary<string, Comment>();
					num = 0;
					continue;
				case 6:
					if (attribute2 != null)
					{
						num = 2;
						continue;
					}
					goto IL_63;
				case 7:
					goto IL_FD;
				}
				if (this.ᜡ == null)
				{
					num = 5;
					continue;
				}
				goto IL_102;
				IL_63:
				attribute = A_0.GetAttribute(ClipboardData.b("ၸᕺᑼ୾", a_), ClipboardData.b("ᅸེॼཾ뮀겂ꪄ뮔ﺚ철쾢쎤좦\udba8욪첬\udbae슰鶲\udab4얶\udeb8钺쪼킾돀Ꟃ뗄뗆ꛈ꣊꣌볎ꋐ뫒믔냖듘럚퇠폢폤죦蓨諪蓬臮", a_));
				num = 3;
				continue;
				IL_102:
				comment = new Comment(this.ᜄ);
				attribute3 = A_0.GetAttribute(ClipboardData.b("ၸὺ", a_), ClipboardData.b("ᅸེॼཾ뮀겂ꪄ뮔ﺚ철쾢쎤좦\udba8욪첬\udbae슰鶲\udab4얶\udeb8钺쪼킾돀Ꟃ뗄뗆ꛈ꣊꣌볎ꋐ뫒믔냖듘럚퇠폢폤죦蓨諪蓬臮", a_));
				comment.Format.TagBkmk = int.Parse(A_0.GetAttribute(ClipboardData.b("ၸὺ", a_), ClipboardData.b("ᅸེॼཾ뮀겂ꪄ뮔ﺚ철쾢쎤좦\udba8욪첬\udbae슰鶲\udab4얶\udeb8钺쪼킾돀Ꟃ뗄뗆ꛈ꣊꣌볎ꋐ뫒믔냖듘럚퇠폢폤죦蓨諪蓬臮", a_)));
				attribute2 = A_0.GetAttribute(ClipboardData.b("ᡸ๺ॼ᝾", a_), ClipboardData.b("ᅸེॼཾ뮀겂ꪄ뮔ﺚ철쾢쎤좦\udba8욪첬\udbae슰鶲\udab4얶\udeb8钺쪼킾돀Ꟃ뗄뗆ꛈ꣊꣌볎ꋐ뫒믔냖듘럚퇠폢폤죦蓨諪蓬臮", a_));
				num = 6;
				continue;
				IL_E3:
				goto IL_102;
			}
			IL_FD:
			IL_1C5:
			this.ᜡ.Add(attribute3, comment);
			this.ᜏ(A_0, comment);
			return;
		}
		}
	}

	// Token: 0x06001E85 RID: 7813 RVA: 0x001E7228 File Offset: 0x001E6228
	private void ᜆ()
	{
		int a_ = 11;
		spr\u22A5 spr_u22A;
		for (;;)
		{
			spr_u22A = this.ᜀ(ClipboardData.b("ٰᱲݴ፶噸", a_), ClipboardData.b("ተᱲᡴ᩶ᱸᕺॼ౾꾀ﮂ", a_));
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (spr_u22A == null)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D7;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 1:
					if (spr_u22A.ᜁ() != null)
					{
						num = 3;
						continue;
					}
					return;
				case 2:
					num = 1;
					continue;
				case 3:
					num = 4;
					continue;
				case 4:
					if (spr_u22A.ᜁ().Length == 0L)
					{
						num = 5;
						continue;
					}
					goto IL_D7;
				case 5:
					goto IL_AB;
				}
				break;
			}
		}
		return;
		IL_AB:
		return;
		IL_D7:
		spr_u22A.ᜁ().Position = 0L;
		XmlReader a_2 = spr\u23D7.ᜀ(spr_u22A.ᜁ());
		this.ᜋ = ClipboardData.b("ተᱲᡴ᩶ᱸᕺॼ౾꾀ﮂ", a_);
		this.ᜏ(a_2, null);
		this.ᜋ = string.Empty;
	}

	// Token: 0x06001E86 RID: 7814 RVA: 0x001E734C File Offset: 0x001E634C
	private void ᜏ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 2;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ŧ๩", a_), ClipboardData.b("gṩᡫṭ䩯嵱孳յ᭷ቹ᥻፽ꪃﶏﺑ秊ﶛ펟財쮣풥쾧薩\udbab솭슯횱쒳쒵ힷ\ud9b9\ud9bb춽뎿ꯁ꫃ꇅꗇꛉﳍ崙뗗믙뗛냝", a_));
			CommentMark entity = new CommentMark(this.ᜄ, int.Parse(attribute));
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_1.Add(entity);
					num = 3;
					continue;
				case 1:
					this.\u171A().Push(this.\u1716()[attribute]);
					num = 6;
					continue;
				case 2:
					goto IL_7C;
				case 3:
					goto IL_7C;
				case 4:
					if (true)
					{
					}
					if (this.\u1716().ContainsKey(attribute))
					{
						goto IL_9D;
					}
					return;
				case 5:
					if (A_1.OwnerBase != null)
					{
						num = 0;
						continue;
					}
					this.ᜠ = entity;
					num = 2;
					continue;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9D;
					default:
						goto IL_115;
					}
					break;
				}
				break;
				IL_7C:
				num = 4;
				continue;
				IL_9D:
				num = 1;
			}
		}
		IL_115:
		if (false)
		{
		}
	}

	// Token: 0x06001E87 RID: 7815 RVA: 0x001E7478 File Offset: 0x001E6478
	private void ᜎ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 0;
		for (;;)
		{
			int a_2 = int.Parse(A_0.GetAttribute(ClipboardData.b("ཥ౧", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧\udda9쎫\udcad풯슱욳\ud9b5\udbb7\udfb9쾻춽ꦿ곁ꏃꯅ꓇ﻋﻍﯓ믕맗동닛", a_)));
			CommentMark entity = new CommentMark(this.ᜄ, a_2, CommentMarkType.CommentEnd);
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_BC;
				case 1:
					A_1.Add(entity);
					num = 0;
					continue;
				case 2:
					return;
				case 3:
					if (true)
					{
					}
					if (A_1.OwnerBase != null)
					{
						num = 1;
						continue;
					}
					this.ᜠ = entity;
					num = 7;
					continue;
				case 4:
					if (this.ᜢ.Count > 0)
					{
						num = 6;
						continue;
					}
					return;
				case 5:
					if (this.ᜢ != null)
					{
						num = 8;
						continue;
					}
					return;
				case 6:
					this.ᜢ.Pop();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BC;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 7:
					goto IL_BC;
				case 8:
					num = 4;
					continue;
				}
				break;
				IL_BC:
				num = 5;
			}
		}
	}

	// Token: 0x06001E88 RID: 7816 RVA: 0x001E75CC File Offset: 0x001E65CC
	private void ᜁ(ParagraphBase A_0)
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
					return;
				}
				if (false)
				{
				}
				this.\u1714().Items.Add(A_0);
				num = 2;
				continue;
			case 2:
				return;
			}
			if (this.\u1714() == null)
			{
				break;
			}
			if (true)
			{
			}
			num = 0;
		}
	}

	// Token: 0x06001E89 RID: 7817 RVA: 0x001E7654 File Offset: 0x001E6654
	private void ᜅ(XmlReader A_0, Table A_1)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 21;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_210;
				case 1:
					num = 22;
					continue;
				case 2:
				{
					string localName;
					if (!(localName == ClipboardData.b("౭Ὧᵱέ᭵᥷ࡹ᝻⵽", a_)))
					{
						num = 5;
						continue;
					}
					this.ᜉ(A_0, null);
					num = 25;
					continue;
				}
				case 3:
					goto IL_EE;
				case 4:
					goto IL_230;
				case 5:
					num = 34;
					continue;
				case 6:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 1;
						continue;
					}
					goto IL_210;
				}
				case 7:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 16;
						continue;
					}
					A_0.Read();
					num = 37;
					continue;
				case 8:
					goto IL_303;
				case 9:
					num = 2;
					continue;
				case 10:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᵭᑯٱ", a_)))
					{
						num = 9;
						continue;
					}
					spr\u204E a_2 = new spr\u204E(A_1.Document);
					this.ᜀ(A_0, a_2, A_1);
					num = 26;
					continue;
				}
				case 11:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᩭɯ", a_)))
					{
						num = 12;
						continue;
					}
					TableRow tableRow = A_1.AddRow(false, false);
					this.ᜀ(tableRow, A_1);
					this.ᜣ = -1;
					this.ᜂ(A_0, tableRow);
					num = 19;
					continue;
				}
				case 12:
					num = 10;
					continue;
				case 13:
					num = 0;
					continue;
				case 14:
					if (!A_0.IsEmptyElement)
					{
						num = 31;
						continue;
					}
					goto IL_210;
				case 15:
					if (A_1 != null)
					{
						bool flag = false;
						A_0.Read();
						this.ᜀ(A_0);
						num = 18;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_408;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 16:
					num = 6;
					continue;
				case 17:
					goto IL_210;
				case 18:
					goto IL_230;
				case 19:
					goto IL_210;
				case 20:
					return;
				case 22:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᩭቯṱ⑳ѵ", a_)))
					{
						num = 24;
						continue;
					}
					A_1.DocxTableFormat.Format.LayoutType = LayoutType.None;
					this.ᜎ(A_0, A_1);
					this.ᜀ(A_1.DocxTableFormat);
					goto IL_408;
				}
				case 23:
				{
					if (!(A_0.LocalName != ClipboardData.b("ᩭቯṱ", a_)))
					{
						num = 33;
						continue;
					}
					bool flag = false;
					num = 7;
					continue;
				}
				case 24:
					num = 35;
					continue;
				case 25:
					goto IL_210;
				case 26:
					goto IL_210;
				case 27:
					A_0.Read();
					num = 4;
					continue;
				case 28:
					if (A_0.IsEmptyElement)
					{
						num = 20;
						continue;
					}
					num = 15;
					continue;
				case 29:
				{
					bool flag;
					if (!flag)
					{
						num = 27;
						continue;
					}
					goto IL_230;
				}
				case 30:
					goto IL_210;
				case 31:
					A_1.TableGrid.Add(0f);
					this.ᜀ(A_0, A_1, false);
					num = 17;
					continue;
				case 32:
					if (true)
					{
					}
					goto IL_210;
				case 33:
					return;
				case 34:
				{
					string localName;
					if (!(localName == ClipboardData.b("౭Ὧᵱέ᭵᥷ࡹ᝻㭽", a_)))
					{
						num = 13;
						continue;
					}
					this.ᜌ(A_0, A_1);
					num = 30;
					continue;
				}
				case 35:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᩭቯṱ㍳ѵᅷṹ", a_)))
					{
						num = 36;
						continue;
					}
					num = 14;
					continue;
				}
				case 36:
					num = 11;
					continue;
				case 37:
					goto IL_230;
				}
				if (A_0.LocalName != ClipboardData.b("ᩭቯṱ", a_))
				{
					num = 3;
					continue;
				}
				num = 28;
				continue;
				IL_210:
				num = 29;
				continue;
				IL_230:
				num = 23;
				continue;
				IL_408:
				num = 32;
			}
			IL_EE:
			throw new XmlException(ClipboardData.b("ᩭᅯၱᡳ፵塷όၻ᭽", a_));
			IL_303:
			throw new ArgumentException(ClipboardData.b("ᩭᅯၱᡳ፵", a_));
		}
		}
	}

	// Token: 0x06001E8A RID: 7818 RVA: 0x001E7B54 File Offset: 0x001E6B54
	private void ᜂ(XmlReader A_0, TableRow A_1)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 21;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_D6;
				case 1:
					if (true)
					{
					}
					if (A_0.IsEmptyElement)
					{
						num = 8;
						continue;
					}
					num = 14;
					continue;
				case 2:
					goto IL_166;
				case 3:
					goto IL_1C2;
				case 4:
					num = 16;
					continue;
				case 5:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 4;
						continue;
					}
					A_0.Read();
					num = 31;
					continue;
				case 6:
				{
					int num2;
					switch (num2)
					{
					case 0:
						this.ᜁ(A_0, A_1);
						num = 25;
						continue;
					case 1:
						A_1.HasTblPrEx = true;
						this.ᜎ(A_0, A_1);
						num = 30;
						continue;
					case 2:
					{
						spr\u1AA4 a_2 = new spr\u1AA4(A_1.Document);
						this.ᜁ(A_0, a_2, A_1);
						num = 13;
						continue;
					}
					case 3:
					{
						TableCell a_3 = A_1.AddCell(false);
						this.ᜣ++;
						this.ᜃ(A_0, a_3);
						num = 23;
						continue;
					}
					case 4:
						this.ᜉ(A_0, null);
						num = 11;
						continue;
					case 5:
						this.ᜌ(A_0, A_1);
						num = 18;
						continue;
					default:
						num = 27;
						continue;
					}
					break;
				}
				case 7:
					if (!(A_0.LocalName != ClipboardData.b("᭮Ͱ", a_)))
					{
						num = 24;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_31B;
					default:
					{
						if (false)
						{
						}
						bool flag = false;
						num = 5;
						continue;
					}
					}
					break;
				case 8:
					return;
				case 9:
					goto IL_1C2;
				case 10:
					goto IL_31B;
				case 11:
					goto IL_2D5;
				case 12:
				{
					int num2;
					string localName;
					if (spr᧓.ᝌ.TryGetValue(localName, out num2))
					{
						num = 26;
						continue;
					}
					goto IL_2D5;
				}
				case 13:
					goto IL_2D5;
				case 14:
				{
					if (A_1 == null)
					{
						num = 29;
						continue;
					}
					bool flag = false;
					A_0.Read();
					this.ᜀ(A_0);
					num = 3;
					continue;
				}
				case 15:
				{
					bool flag;
					if (!flag)
					{
						num = 22;
						continue;
					}
					goto IL_166;
				}
				case 16:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 19;
						continue;
					}
					goto IL_2D5;
				}
				case 17:
					if (spr᧓.ᝌ == null)
					{
						num = 20;
						continue;
					}
					goto IL_31B;
				case 18:
					goto IL_2D5;
				case 19:
					num = 17;
					continue;
				case 20:
					spr᧓.ᝌ = new Dictionary<string, int>(6)
					{
						{
							ClipboardData.b("᭮Ͱ⍲ݴ", a_),
							0
						},
						{
							ClipboardData.b("᭮፰ὲ╴ն㱸ͺ", a_),
							1
						},
						{
							ClipboardData.b("ᱮᕰݲ", a_),
							2
						},
						{
							ClipboardData.b("᭮ተ", a_),
							3
						},
						{
							ClipboardData.b("൮ṰᱲṴ᩶ᡸॺᙼⱾ", a_),
							4
						},
						{
							ClipboardData.b("൮ṰᱲṴ᩶ᡸॺᙼ㩾", a_),
							5
						}
					};
					num = 10;
					continue;
				case 22:
					A_0.Read();
					num = 2;
					continue;
				case 23:
					goto IL_2D5;
				case 24:
					return;
				case 25:
					goto IL_2D5;
				case 26:
					num = 6;
					continue;
				case 27:
					num = 28;
					continue;
				case 28:
					goto IL_2D5;
				case 29:
					goto IL_2A6;
				case 30:
					goto IL_2D5;
				case 31:
					goto IL_166;
				}
				if (A_0.LocalName != ClipboardData.b("᭮Ͱ", a_))
				{
					num = 0;
					continue;
				}
				num = 1;
				continue;
				IL_166:
				this.ᜀ(A_0);
				num = 9;
				continue;
				IL_1C2:
				num = 7;
				continue;
				IL_2D5:
				num = 15;
				continue;
				IL_31B:
				num = 12;
			}
			IL_D6:
			throw new XmlException(ClipboardData.b("᭮ၰᅲᥴቶ奸ॺቼࡾꆀﮎ", a_));
			IL_2A6:
			throw new ArgumentException(ClipboardData.b("᭮ၰᅲᥴቶ奸ॺቼࡾ", a_));
		}
		}
	}

	// Token: 0x06001E8B RID: 7819 RVA: 0x001E7FF0 File Offset: 0x001E6FF0
	private void ᜁ(XmlReader A_0, spr\u1AA4 A_1, TableRow A_2)
	{
		int a_ = 11;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string localName;
				if (!(localName == ClipboardData.b("ɰᝲŴ㉶᝸ὺ⵼ൾ", a_)))
				{
					num = 23;
					continue;
				}
				this.ᜌ(A_0, A_1.ᜀ());
				num = 1;
				continue;
			}
			case 1:
				goto IL_19C;
			case 3:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 25;
					continue;
				}
				goto IL_12D;
			case 4:
				goto IL_19C;
			case 5:
				return;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12D;
				default:
					if (false)
					{
					}
					goto IL_19C;
				}
				break;
			case 7:
				goto IL_1C2;
			case 8:
			{
				if (A_0.IsEmptyElement)
				{
					num = 26;
					continue;
				}
				bool flag = false;
				string localName2 = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				num = 7;
				continue;
			}
			case 9:
				num = 21;
				continue;
			case 11:
			{
				string localName2;
				if (!(A_0.LocalName != localName2))
				{
					num = 5;
					continue;
				}
				bool flag = false;
				num = 3;
				continue;
			}
			case 12:
				num = 8;
				continue;
			case 13:
				num = 20;
				continue;
			case 14:
				goto IL_1C2;
			case 15:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 13;
					continue;
				}
				goto IL_19C;
			}
			case 16:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 12;
					continue;
				}
				A_0.Read();
				num = 10;
				continue;
			case 17:
				goto IL_19C;
			case 18:
				goto IL_1C2;
			case 19:
				num = 0;
				continue;
			case 20:
			{
				string localName;
				if (!(localName == ClipboardData.b("ɰᝲŴ❶୸", a_)))
				{
					num = 9;
					continue;
				}
				this.ᜀ(A_0, A_1.ᜂ());
				num = 17;
				continue;
			}
			case 21:
			{
				string localName;
				if (!(localName == ClipboardData.b("ɰᝲŴ㑶ᙸᕺॼ᩾", a_)))
				{
					num = 19;
					continue;
				}
				this.ᜀ(A_0, A_1, A_2);
				num = 4;
				continue;
			}
			case 22:
			{
				bool flag;
				if (!flag)
				{
					if (true)
					{
					}
					num = 24;
					continue;
				}
				goto IL_1C2;
			}
			case 23:
				num = 6;
				continue;
			case 24:
				A_0.Read();
				num = 18;
				continue;
			case 25:
				num = 15;
				continue;
			case 26:
				return;
			}
			IL_EC:
			num = 16;
			continue;
			goto IL_EC;
			IL_12D:
			A_0.Read();
			num = 14;
			continue;
			IL_19C:
			num = 22;
			continue;
			IL_1C2:
			num = 11;
		}
	}

	// Token: 0x06001E8C RID: 7820 RVA: 0x001E82FC File Offset: 0x001E72FC
	private void ᜀ(XmlReader A_0, spr\u1AA4 A_1, TableRow A_2)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 19;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 12;
						continue;
					}
					A_0.Read();
					num = 1;
					continue;
				case 1:
					goto IL_1F3;
				case 2:
				{
					if (true)
					{
					}
					string localName;
					if (!(localName == ClipboardData.b("ᵭᑯٱ", a_)))
					{
						num = 3;
						continue;
					}
					spr\u1AA4 a_2 = new spr\u1AA4(this.ᜄ);
					this.ᜁ(A_0, a_2, A_2);
					num = 15;
					continue;
				}
				case 3:
					num = 14;
					continue;
				case 4:
					goto IL_272;
				case 5:
				{
					string localName2 = A_0.LocalName;
					num = 9;
					continue;
				}
				case 6:
					goto IL_1F3;
				case 7:
					goto IL_9C;
				case 8:
					num = 2;
					continue;
				case 9:
				{
					if (A_0.IsEmptyElement)
					{
						num = 11;
						continue;
					}
					bool flag = false;
					A_0.Read();
					this.ᜀ(A_0);
					num = 6;
					continue;
				}
				case 10:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 5;
						continue;
					}
					A_0.Read();
					num = 4;
					continue;
				case 11:
					return;
				case 12:
					num = 17;
					continue;
				case 13:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A1;
					default:
						goto IL_232;
					}
					break;
				case 14:
					goto IL_1D3;
				case 15:
					goto IL_1D3;
				case 16:
				{
					string localName2;
					if (!(A_0.LocalName != localName2))
					{
						num = 13;
						continue;
					}
					num = 0;
					continue;
				}
				case 17:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 24;
						continue;
					}
					goto IL_1D3;
				}
				case 18:
					goto IL_1F3;
				case 20:
				{
					bool flag;
					if (!flag)
					{
						num = 22;
						continue;
					}
					goto IL_1F3;
				}
				case 21:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᩭ፯", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_A1;
				}
				case 22:
					A_0.Read();
					num = 18;
					continue;
				case 23:
					goto IL_1D3;
				case 24:
					num = 21;
					continue;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				goto IL_272;
				IL_A1:
				TableCell tableCell = A_2.AddCell(false);
				this.ᜣ++;
				this.ᜃ(A_0, tableCell);
				tableCell.SDTCell = A_1;
				num = 23;
				continue;
				IL_1D3:
				num = 20;
				continue;
				IL_1F3:
				num = 16;
				continue;
				IL_272:
				num = 10;
			}
			IL_9C:
			throw new ArgumentNullException(ClipboardData.b("ᱭᕯ፱ၳ፵੷", a_));
			IL_232:
			if (false)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06001E8D RID: 7821 RVA: 0x001E8620 File Offset: 0x001E7620
	private void ᜀ(XmlReader A_0, spr\u204E A_1, Table A_2)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 1:
				{
					string localName;
					if (!(localName == ClipboardData.b("Ὣ੭ѯ≱ٳ", a_)))
					{
						num = 19;
						continue;
					}
					this.ᜀ(A_0, A_1.ᜂ());
					num = 2;
					continue;
				}
				case 2:
					goto IL_14E;
				case 3:
					num = 22;
					continue;
				case 4:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 17;
						continue;
					}
					A_0.Read();
					num = 24;
					continue;
				case 5:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 21;
						continue;
					}
					goto IL_1C2;
				}
				case 6:
					goto IL_1E2;
				case 8:
					A_0.Read();
					num = 26;
					continue;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_14E;
					default:
						if (false)
						{
						}
						goto IL_1C2;
					}
					break;
				case 10:
					goto IL_1C2;
				case 11:
					num = 25;
					continue;
				case 12:
					goto IL_20B;
				case 13:
				{
					string localName;
					if (!(localName == ClipboardData.b("Ὣ੭ѯㅱ᭳ᡵ౷όቻ੽", a_)))
					{
						num = 11;
						continue;
					}
					bool flag;
					this.ᜀ(A_0, A_1, A_2, ref flag);
					num = 10;
					continue;
				}
				case 14:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 3;
						continue;
					}
					A_0.Read();
					num = 0;
					continue;
				case 15:
				{
					string localName2;
					if (!(A_0.LocalName != localName2))
					{
						num = 12;
						continue;
					}
					bool flag2 = false;
					num = 4;
					continue;
				}
				case 16:
				{
					bool flag2;
					if (!flag2)
					{
						num = 8;
						continue;
					}
					goto IL_1E2;
				}
				case 17:
					num = 5;
					continue;
				case 18:
					return;
				case 19:
					num = 13;
					continue;
				case 20:
					num = 9;
					continue;
				case 21:
					num = 1;
					continue;
				case 22:
				{
					if (A_0.IsEmptyElement)
					{
						num = 18;
						continue;
					}
					bool flag2 = false;
					string localName2 = A_0.LocalName;
					A_0.Read();
					this.ᜀ(A_0);
					bool flag = true;
					num = 6;
					continue;
				}
				case 23:
					goto IL_1C2;
				case 24:
					goto IL_1E2;
				case 25:
				{
					string localName;
					if (!(localName == ClipboardData.b("Ὣ੭ѯ㝱ᩳት⡷ࡹ", a_)))
					{
						num = 20;
						continue;
					}
					this.ᜌ(A_0, A_1.ᜀ());
					num = 23;
					continue;
				}
				case 26:
					goto IL_1E2;
				}
				IL_10C:
				num = 14;
				continue;
				goto IL_10C;
				IL_1C2:
				num = 16;
				continue;
				IL_14E:
				goto IL_1C2;
				IL_1E2:
				num = 15;
			}
			IL_20B:
			if (true)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06001E8E RID: 7822 RVA: 0x001E8958 File Offset: 0x001E7958
	private void ᜀ(XmlReader A_0, spr\u204E A_1, Table A_2, ref bool A_3)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				string localName;
				switch (num)
				{
				case 0:
					goto IL_1EA;
				case 1:
					return;
				case 2:
					goto IL_20A;
				case 4:
					num = 0;
					continue;
				case 5:
				{
					if (!(localName == ClipboardData.b("ᥬᵮ", a_)))
					{
						num = 13;
						continue;
					}
					TableRow tableRow = A_2.AddRow(false, false);
					this.ᜀ(tableRow, A_2);
					this.ᜣ = -1;
					this.ᜂ(A_0, tableRow);
					num = 25;
					continue;
				}
				case 6:
					A_0.Read();
					num = 2;
					continue;
				case 7:
					return;
				case 8:
					goto IL_A7;
				case 9:
					goto IL_20A;
				case 10:
					goto IL_1EA;
				case 11:
					num = 14;
					continue;
				case 12:
				{
					if (A_0.IsEmptyElement)
					{
						num = 7;
						continue;
					}
					bool flag = false;
					A_0.Read();
					this.ᜀ(A_0);
					num = 9;
					continue;
				}
				case 13:
					num = 16;
					continue;
				case 14:
					if ((localName = A_0.LocalName) != null)
					{
						num = 17;
						continue;
					}
					goto IL_1EA;
				case 15:
					goto IL_20A;
				case 16:
					goto IL_D4;
				case 17:
					num = 5;
					continue;
				case 18:
				{
					bool flag;
					if (!flag)
					{
						num = 6;
						continue;
					}
					goto IL_20A;
				}
				case 19:
				{
					string localName2;
					if (!(A_0.LocalName != localName2))
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					num = 24;
					continue;
				}
				case 20:
				{
					TableRow tableRow;
					tableRow.SDTRow = A_1;
					A_3 = false;
					num = 26;
					continue;
				}
				case 21:
					goto IL_284;
				case 22:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D4;
					default:
						if (false)
						{
						}
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 23;
							continue;
						}
						A_0.Read();
						num = 21;
						continue;
					}
					break;
				case 23:
				{
					string localName2 = A_0.LocalName;
					num = 12;
					continue;
				}
				case 24:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 11;
						continue;
					}
					A_0.Read();
					num = 15;
					continue;
				case 25:
					if (A_3)
					{
						num = 20;
						continue;
					}
					goto IL_1EA;
				case 26:
					goto IL_1EA;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				goto IL_284;
				IL_D4:
				if (!(localName == ClipboardData.b("Ṭ୮հ", a_)))
				{
					num = 4;
					continue;
				}
				spr\u204E a_2 = new spr\u204E(this.ᜄ);
				this.ᜀ(A_0, a_2, A_2);
				num = 10;
				continue;
				IL_1EA:
				num = 18;
				continue;
				IL_20A:
				num = 19;
				continue;
				IL_284:
				num = 22;
			}
			IL_A7:
			throw new ArgumentNullException(ClipboardData.b("Ὤ੮ၰᝲၴն", a_));
		}
		}
	}

	// Token: 0x06001E8F RID: 7823 RVA: 0x001E8CB4 File Offset: 0x001E7CB4
	private void ᜃ(XmlReader A_0, TableCell A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 21;
			for (;;)
			{
				Field field;
				Table table;
				IParagraph paragraph;
				switch (num)
				{
				case 0:
					goto IL_6D1;
				case 1:
					goto IL_6D1;
				case 2:
					if (!this.\u1712().IsFieldRange)
					{
						num = 67;
						continue;
					}
					goto IL_452;
				case 3:
					num = 28;
					continue;
				case 4:
					num = 29;
					continue;
				case 5:
					return;
				case 6:
					num = 8;
					continue;
				case 7:
					field.IsFieldRange = false;
					num = 1;
					continue;
				case 8:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 23;
						continue;
					}
					goto IL_6D1;
				}
				case 9:
					this.\u1715().Push(this.ᜣ);
					this.ᜣ = -1;
					num = 70;
					continue;
				case 10:
				{
					string localName;
					if (!(localName == ClipboardData.b("๫ŭὯᥱᥳ᝵੷ᅹ㥻ၽ", a_)))
					{
						num = 27;
						continue;
					}
					this.ᜌ(A_0, A_1);
					num = 33;
					continue;
				}
				case 11:
				{
					bool flag;
					if (flag)
					{
						num = 62;
						continue;
					}
					goto IL_74B;
				}
				case 12:
					if (table.Rows.Count <= 0)
					{
						num = 13;
						continue;
					}
					goto IL_6D1;
				case 13:
					num = 55;
					continue;
				case 14:
					if (field != null)
					{
						num = 39;
						continue;
					}
					goto IL_6D1;
				case 15:
					this.ᜣ = this.\u1715().Pop();
					num = 71;
					continue;
				case 16:
				{
					bool flag2;
					if (!flag2)
					{
						num = 48;
						continue;
					}
					goto IL_304;
				}
				case 17:
				{
					if (!(A_0.LocalName != ClipboardData.b("ᡫ൭", a_)))
					{
						num = 5;
						continue;
					}
					bool flag2 = false;
					bool flag = false;
					num = 24;
					continue;
				}
				case 18:
					if (A_0.IsEmptyElement)
					{
						num = 54;
						continue;
					}
					num = 59;
					continue;
				case 19:
					goto IL_6D1;
				case 20:
					goto IL_304;
				case 22:
					num = 11;
					continue;
				case 23:
					num = 69;
					continue;
				case 24:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 6;
						continue;
					}
					A_0.Read();
					num = 46;
					continue;
				case 25:
					if (field != null)
					{
						num = 53;
						continue;
					}
					goto IL_74B;
				case 26:
					if (this.\u1712() != null)
					{
						num = 4;
						continue;
					}
					goto IL_840;
				case 27:
					IL_412:
					num = 50;
					continue;
				case 28:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᡫ౭ᱯ", a_)))
					{
						num = 35;
						continue;
					}
					num = 66;
					continue;
				}
				case 29:
					if (!this.\u1712().IsFieldRange)
					{
						num = 68;
						continue;
					}
					goto IL_840;
				case 30:
					num = 10;
					continue;
				case 31:
					if (this.\u1715().Count > 0)
					{
						num = 15;
						continue;
					}
					goto IL_555;
				case 32:
					num = 51;
					continue;
				case 33:
					goto IL_6D1;
				case 34:
					A_1.CellFormat.Borders.IsDefault = false;
					this.ᜂ(A_0, A_1);
					num = 52;
					continue;
				case 35:
					num = 41;
					continue;
				case 36:
					if (this.\u1712() != null)
					{
						num = 49;
						continue;
					}
					goto IL_452;
				case 37:
					goto IL_840;
				case 38:
					if (!string.IsNullOrEmpty(paragraph.StyleName))
					{
						num = 57;
						continue;
					}
					goto IL_64C;
				case 39:
					num = 45;
					continue;
				case 40:
				{
					bool flag;
					if (flag)
					{
						num = 7;
						continue;
					}
					goto IL_6D1;
				}
				case 41:
				{
					string localName;
					if (!(localName == ClipboardData.b("๫ŭὯᥱᥳ᝵੷ᅹ⽻੽", a_)))
					{
						num = 30;
						continue;
					}
					this.ᜉ(A_0, null);
					num = 19;
					continue;
				}
				case 42:
					goto IL_797;
				case 43:
					num = 40;
					continue;
				case 44:
					goto IL_452;
				case 45:
					if (field.IsFieldRange)
					{
						num = 43;
						continue;
					}
					goto IL_6D1;
				case 46:
					goto IL_304;
				case 47:
					goto IL_808;
				case 48:
					A_0.Read();
					num = 20;
					continue;
				case 49:
					num = 2;
					continue;
				case 50:
					goto IL_6D1;
				case 51:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᱫ", a_)))
					{
						num = 3;
						continue;
					}
					paragraph = A_1.AddParagraph();
					num = 36;
					continue;
				}
				case 52:
					goto IL_6D1;
				case 53:
					num = 58;
					continue;
				case 54:
					return;
				case 55:
					if (A_1.Items.Contains(table))
					{
						num = 60;
						continue;
					}
					goto IL_6D1;
				case 56:
					if (!A_0.IsEmptyElement)
					{
						num = 34;
						continue;
					}
					goto IL_6D1;
				case 57:
					paragraph.ApplyStyle(paragraph.StyleName);
					num = 63;
					continue;
				case 58:
					if (field.IsFieldRange)
					{
						num = 22;
						continue;
					}
					goto IL_74B;
				case 59:
				{
					if (A_1 == null)
					{
						num = 42;
						continue;
					}
					bool flag2 = false;
					A_0.Read();
					this.ᜀ(A_0);
					num = 47;
					continue;
				}
				case 60:
					A_1.Items.Remove(table);
					num = 0;
					continue;
				case 61:
					goto IL_176;
				case 62:
					field.IsFieldRange = false;
					num = 64;
					continue;
				case 63:
					goto IL_64C;
				case 64:
					goto IL_74B;
				case 65:
					goto IL_808;
				case 66:
					if (this.ᜣ >= 0)
					{
						num = 9;
						continue;
					}
					goto IL_79C;
				case 67:
				{
					if (true)
					{
					}
					this.\u1712().IsFieldRange = true;
					bool flag = true;
					this.ᜀ(paragraph as Paragraph);
					num = 44;
					continue;
				}
				case 68:
				{
					this.\u1712().IsFieldRange = true;
					bool flag = true;
					this.ᜀ(table);
					num = 37;
					continue;
				}
				case 69:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᡫ൭⁯q", a_)))
					{
						num = 32;
						continue;
					}
					num = 56;
					continue;
				}
				case 70:
					goto IL_79C;
				case 71:
					goto IL_555;
				}
				if (A_0.LocalName != ClipboardData.b("ᡫ൭", a_))
				{
					num = 61;
					continue;
				}
				num = 18;
				continue;
				IL_304:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_412;
				default:
					if (false)
					{
					}
					this.ᜀ(A_0);
					num = 65;
					continue;
				}
				IL_452:
				field = this.\u1712();
				this.ᜀ(paragraph);
				this.ᜐ(A_0, paragraph.Items);
				num = 38;
				continue;
				IL_555:
				num = 12;
				continue;
				IL_64C:
				num = 14;
				continue;
				IL_6D1:
				num = 16;
				continue;
				IL_74B:
				num = 31;
				continue;
				IL_79C:
				table = A_1.AddTable();
				num = 26;
				continue;
				IL_808:
				num = 17;
				continue;
				IL_840:
				field = this.\u1712();
				this.ᜅ(A_0, table);
				num = 25;
			}
			IL_176:
			throw new XmlException(ClipboardData.b("ᡫ཭ቯṱᅳ噵᭷όၻችꁿ揄", a_));
			IL_797:
			throw new ArgumentException(ClipboardData.b("ᡫ཭ቯṱᅳ噵᭷όၻች", a_));
		}
		}
	}

	// Token: 0x06001E90 RID: 7824 RVA: 0x001E9534 File Offset: 0x001E8534
	private void ᜂ(XmlReader A_0, TableCell A_1)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 25;
			for (;;)
			{
				CellFormat cellFormat;
				bool flag;
				CellFormat cellFormat2;
				switch (num)
				{
				case 0:
					if (A_1.CellFormat.IsAutoResized)
					{
						num = 27;
						continue;
					}
					goto IL_486;
				case 1:
					goto IL_24E;
				case 2:
					goto IL_24E;
				case 3:
					goto IL_486;
				case 4:
					goto IL_24E;
				case 5:
					if (A_1.GridSpan == 1)
					{
						num = 38;
						continue;
					}
					goto IL_24E;
				case 6:
					goto IL_24E;
				case 7:
					goto IL_24E;
				case 8:
					goto IL_24E;
				case 9:
					goto IL_24E;
				case 10:
				{
					string localName;
					int num2;
					if (spr᧓.ᝍ.TryGetValue(localName, out num2))
					{
						num = 30;
						continue;
					}
					goto IL_398;
				}
				case 11:
					goto IL_24E;
				case 12:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 32;
						continue;
					}
					goto IL_398;
				}
				case 13:
					goto IL_5DC;
				case 14:
				{
					int num2;
					switch (num2)
					{
					case 0:
						cellFormat.TextWrap = false;
						num = 31;
						continue;
					case 1:
						cellFormat.FitText = true;
						num = 28;
						continue;
					case 2:
						this.ᜀ(A_0, A_1);
						num = 9;
						continue;
					case 3:
						this.ᜀ(A_0, cellFormat);
						num = 11;
						continue;
					case 4:
						cellFormat.VerticalAlignment = this.\u171E(A_0);
						num = 6;
						continue;
					case 5:
						this.ᜂ(A_0, cellFormat);
						num = 37;
						continue;
					case 6:
						this.ᜁ(A_0, cellFormat);
						num = 1;
						continue;
					case 7:
						cellFormat.SamePaddingsAsTable = false;
						this.\u170D(A_0, A_1);
						num = 2;
						continue;
					case 8:
						this.ᜂ(A_0, A_1);
						num = 7;
						continue;
					case 9:
						this.ᜁ(A_0, A_1);
						num = 8;
						continue;
					case 10:
					{
						short num3 = short.Parse(A_0.GetAttribute(ClipboardData.b("ѱᕳ᩵", a_), ClipboardData.b("ᩱsɵࡷ䁹卻兽ﾋꂍﾏ쾟킡즣장\udca7\ud9a9芫솭슯햱鮳솵ힷ좹\ud8bb캽늿귁ꟃꏅ믇막ꗋꃍ럏뿑룓崙쿟迡藣迥蛧", a_)));
						num = 5;
						continue;
					}
					case 11:
						this.ᜈ = true;
						A_0.Read();
						this.ᜀ(A_0);
						this.ᜂ(A_0, A_1);
						this.ᜈ = false;
						num = 4;
						continue;
					case 12:
						goto IL_24E;
					default:
						num = 41;
						continue;
					}
					break;
				}
				case 15:
					if (A_0.IsEmptyElement)
					{
						num = 17;
						continue;
					}
					num = 23;
					continue;
				case 16:
					if (!flag)
					{
						num = 34;
						continue;
					}
					goto IL_6E6;
				case 17:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3AC;
					default:
						goto IL_67F;
					}
					break;
				case 18:
					if (!this.ᜈ)
					{
						num = 19;
						continue;
					}
					num = 21;
					continue;
				case 19:
					num = 47;
					continue;
				case 20:
					goto IL_398;
				case 21:
					cellFormat2 = A_1.TrackCellFormat;
					goto IL_234;
				case 22:
					num = 12;
					continue;
				case 23:
					if (A_1 == null)
					{
						num = 46;
						continue;
					}
					flag = false;
					A_0.Read();
					this.ᜀ(A_0);
					num = 18;
					continue;
				case 24:
					goto IL_116;
				case 26:
					if (!(A_0.LocalName != ClipboardData.b("ٱᝳ♵੷", a_)))
					{
						num = 40;
						continue;
					}
					flag = false;
					num = 39;
					continue;
				case 27:
				{
					short num3;
					this.ᜀ(A_1, this.ᜣ, (int)num3);
					this.ᜣ += (int)(num3 - 1);
					num = 3;
					continue;
				}
				case 28:
					goto IL_24E;
				case 29:
					if (true)
					{
					}
					goto IL_271;
				case 30:
					num = 14;
					continue;
				case 31:
					goto IL_24E;
				case 32:
					num = 35;
					continue;
				case 33:
					goto IL_6E6;
				case 34:
					A_0.Read();
					num = 43;
					continue;
				case 35:
					if (spr᧓.ᝍ == null)
					{
						num = 44;
						continue;
					}
					goto IL_5DC;
				case 36:
					goto IL_271;
				case 37:
					goto IL_24E;
				case 38:
				{
					short num3;
					A_1.GridSpan = num3;
					A_0.MoveToContent();
					num = 0;
					continue;
				}
				case 39:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 22;
						continue;
					}
					A_0.Read();
					num = 33;
					continue;
				case 40:
					return;
				case 41:
					num = 20;
					continue;
				case 42:
					goto IL_24E;
				case 43:
					goto IL_6E6;
				case 44:
					spr᧓.ᝍ = new Dictionary<string, int>(13)
					{
						{
							ClipboardData.b("ᱱ᭳ⅵ੷᭹౻", a_),
							0
						},
						{
							ClipboardData.b("ٱᝳふᅷ๹⡻᭽", a_),
							1
						},
						{
							ClipboardData.b("ٱᝳⅵ", a_),
							2
						},
						{
							ClipboardData.b("ٱᅳ๵౷㹹ᕻ౽", a_),
							3
						},
						{
							ClipboardData.b("ѱ㕳᩵ᅷᵹቻ", a_),
							4
						},
						{
							ClipboardData.b("ѱ㥳፵੷ᵹ᥻", a_),
							5
						},
						{
							ClipboardData.b("ᩱ㥳፵੷ᵹ᥻", a_),
							6
						},
						{
							ClipboardData.b("ٱᝳ㭵᥷ࡹ", a_),
							7
						},
						{
							ClipboardData.b("ٱᝳ㑵᝷ࡹ᡻᭽", a_),
							8
						},
						{
							ClipboardData.b("űᱳት", a_),
							9
						},
						{
							ClipboardData.b("ᕱٳήᱷ⥹౻ώ", a_),
							10
						},
						{
							ClipboardData.b("ٱᝳ♵੷㥹ᑻώ", a_),
							11
						},
						{
							ClipboardData.b("ᅱᩳၵ⭷๹ջች", a_),
							12
						}
					};
					num = 13;
					continue;
				case 45:
					goto IL_24E;
				case 46:
					goto IL_62B;
				case 47:
					cellFormat2 = A_1.CellFormat;
					goto IL_234;
				}
				if (A_0.LocalName != ClipboardData.b("ٱᝳ♵੷", a_))
				{
					num = 24;
					continue;
				}
				num = 15;
				continue;
				IL_234:
				cellFormat = cellFormat2;
				num = 29;
				continue;
				IL_24E:
				num = 16;
				continue;
				IL_271:
				num = 26;
				continue;
				IL_3AC:
				num = 45;
				continue;
				IL_398:
				cellFormat.XmlProps2010.Add(this.ᜢ(A_0));
				flag = true;
				goto IL_3AC;
				IL_486:
				cellFormat.XmlProps2010.Add(this.ᜢ(A_0));
				flag = true;
				num = 42;
				continue;
				IL_5DC:
				num = 10;
				continue;
				IL_6E6:
				this.ᜀ(A_0);
				num = 36;
			}
			IL_116:
			throw new XmlException(ClipboardData.b("ٱᕳᑵᑷό屻ᵽꚅﺍﾕﶗ벛ﮝ첟잡즣쎥욧\udea9", a_));
			IL_62B:
			throw new ArgumentException(ClipboardData.b("ٱᕳᑵᑷό屻ᵽ", a_));
			IL_67F:
			if (false)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06001E91 RID: 7825 RVA: 0x001E9C94 File Offset: 0x001E8C94
	private void ᜁ(XmlReader A_0, TableCell A_1)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			Color a_2;
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("ݰቲᥴ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
				int num = 8;
				for (;;)
				{
					string attribute2;
					string attribute3;
					switch (num)
					{
					case 0:
						a_2 = Color.Empty;
						num = 11;
						continue;
					case 1:
						goto IL_1E1;
					case 2:
						goto IL_1E1;
					case 3:
						goto IL_13E;
					case 4:
						A_1.CellFormat.BackColor = Color.Empty;
						num = 6;
						continue;
					case 5:
						A_1.TrackCellFormat.TextureStyle = this.ᜉ(attribute);
						num = 2;
						continue;
					case 6:
						goto IL_C5;
					case 7:
						if (attribute2 == ClipboardData.b("ၰٲŴᡶ", a_))
						{
							num = 4;
							continue;
						}
						A_1.CellFormat.BackColor = this.ᜃ(attribute2);
						goto IL_246;
					case 8:
						if (this.ᜈ)
						{
							num = 5;
							continue;
						}
						A_1.CellFormat.TextureStyle = this.ᜉ(attribute);
						num = 1;
						continue;
					case 9:
						if (this.ᜈ)
						{
							num = 12;
							continue;
						}
						goto IL_257;
					case 10:
						if (attribute3 == ClipboardData.b("ၰٲŴᡶ", a_))
						{
							num = 0;
							continue;
						}
						a_2 = this.ᜃ(attribute3);
						num = 3;
						continue;
					case 11:
						goto IL_13E;
					case 12:
						goto IL_17D;
					case 13:
						goto IL_C5;
					}
					break;
					IL_C5:
					attribute3 = A_0.GetAttribute(ClipboardData.b("ተᱲᥴᡶ୸", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
					num = 10;
					continue;
					IL_13E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_246:
						num = 13;
						continue;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					IL_1E1:
					attribute2 = A_0.GetAttribute(ClipboardData.b("ᝰᩲᥴ᭶", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
					num = 7;
				}
			}
			IL_17D:
			A_1.TrackCellFormat.ForeColor = a_2;
			return;
			IL_257:
			A_1.CellFormat.ForeColor = a_2;
			return;
		}
		}
	}

	// Token: 0x06001E92 RID: 7826 RVA: 0x001E9F04 File Offset: 0x001E8F04
	private void ᜂ(XmlReader A_0, CellFormat A_1)
	{
		int a_ = 11;
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
			string attribute = A_0.GetAttribute(ClipboardData.b("ݰቲᥴ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
			if (!(attribute == ClipboardData.b("ͰᙲٴͶᡸॺॼ", a_)))
			{
				if (true)
				{
				}
				A_1.VerticalMerge = CellMerge.Continue;
				return;
			}
			break;
		}
		}
		A_1.VerticalMerge = CellMerge.Start;
	}

	// Token: 0x06001E93 RID: 7827 RVA: 0x001E9F94 File Offset: 0x001E8F94
	private void ᜁ(XmlReader A_0, CellFormat A_1)
	{
		int a_ = 1;
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
			string attribute = A_0.GetAttribute(ClipboardData.b("ᅦࡨݪ", a_), ClipboardData.b("སᵨὪᵬ啮幰屲ٴᑶᅸṺၼṾ궂﶐杖漢辠첢힤삦蚨\udcaa슬\uddae햰쎲잴\ud8b6\udab8\udeba캼첾ꣀ귂ꋄ꫆ꗈￌￎ䀹뫖룘닚돜", a_));
			if (!(attribute == ClipboardData.b("ᕦ౨ᡪᥬ๮Ͱݲ", a_)))
			{
				A_1.HorizontalMerge = CellMerge.Continue;
				return;
			}
			break;
		}
		}
		if (true)
		{
		}
		A_1.HorizontalMerge = CellMerge.Start;
	}

	// Token: 0x06001E94 RID: 7828 RVA: 0x001EA024 File Offset: 0x001E9024
	private VerticalAlignment \u171E(XmlReader A_0)
	{
		int a_ = 13;
		VerticalAlignment result;
		for (;;)
		{
			result = VerticalAlignment.Top;
			string attribute = A_0.GetAttribute(ClipboardData.b("ղᑴ᭶", a_), ClipboardData.b("᭲ŴͶॸ䅺剼偾ﺌꆎﺐ練咽캠톢좤욦\udda8\ud8aa莬삮쎰풲骴삶횸즺\ud9bc쾾돀곂ꛄꋆ뫈룊꓌ꇎ뛐뻒맔컠転蓤軦蟨", a_));
			int num = 9;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
				{
					string a;
					if (a == ClipboardData.b("ᅲᩴͶ൸ᑺၼ", a_))
					{
						result = VerticalAlignment.Bottom;
						num = 12;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7E;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				}
				case 1:
					return result;
				case 2:
				{
					string a;
					if (!(a == ClipboardData.b("ݲᩴݶ", a_)))
					{
						num = 4;
						continue;
					}
					result = VerticalAlignment.Top;
					num = 1;
					continue;
				}
				case 3:
					num = 10;
					continue;
				case 4:
					num = 0;
					continue;
				case 5:
					return result;
				case 6:
					num = 13;
					continue;
				case 7:
					return result;
				case 8:
					num = 2;
					continue;
				case 9:
					goto IL_7E;
				case 10:
				{
					string a;
					if ((a = attribute) != null)
					{
						num = 8;
						continue;
					}
					return result;
				}
				case 11:
					num = 7;
					continue;
				case 12:
					return result;
				case 13:
				{
					string a;
					if (!(a == ClipboardData.b("ၲၴ᥶൸Ṻོ", a_)))
					{
						num = 11;
						continue;
					}
					result = VerticalAlignment.Middle;
					num = 5;
					continue;
				}
				}
				break;
				IL_7E:
				if (attribute == null)
				{
					return result;
				}
				num = 3;
			}
		}
		return result;
	}

	// Token: 0x06001E95 RID: 7829 RVA: 0x001EA1DC File Offset: 0x001E91DC
	private void ᜀ(XmlReader A_0, CellFormat A_1)
	{
		int a_ = 10;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ٯ፱ᡳ", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱쎳\ud9b5쪷\udeb9첻첽꾿ꇁꇃ뗅믇ꏉꋋ꧍뷏뻑ﯓ跟菡跣裥", a_));
			int num = 11;
			for (;;)
			{
				string a;
				switch (num)
				{
				case 0:
					if (a == ClipboardData.b("ѯၱ♳᩵⹷", a_))
					{
						goto IL_D5;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_70;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 1:
					goto IL_9F;
				case 2:
					if (!(a == ClipboardData.b("ቯٱ㡳ѵ", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_185;
				case 3:
					num = 0;
					continue;
				case 4:
					num = 6;
					continue;
				case 5:
					num = 2;
					continue;
				case 6:
					if (!(a == ClipboardData.b("ᱯq⁳ᑵ⹷", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_116;
				case 7:
					num = 10;
					continue;
				case 8:
					num = 1;
					continue;
				case 9:
					if (!(a == ClipboardData.b("ѯၱ♳᩵", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_10E;
				case 10:
					if (!(a == ClipboardData.b("ᱯq⁳ᑵ", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_8C;
				case 11:
					goto IL_70;
				case 12:
					num = 9;
					continue;
				}
				break;
				IL_70:
				if ((a = attribute) == null)
				{
					goto IL_1DD;
				}
				num = 5;
			}
		}
		IL_8C:
		A_1.TextDirection = TextDirection.TopToBottom;
		return;
		IL_9F:
		goto IL_1DD;
		IL_D5:
		A_1.TextDirection = TextDirection.RightToLeftRotated;
		return;
		IL_10E:
		A_1.TextDirection = TextDirection.RightToLeft;
		return;
		IL_116:
		if (true)
		{
		}
		A_1.TextDirection = TextDirection.TopToBottomRotated;
		return;
		IL_185:
		A_1.TextDirection = TextDirection.LeftToRightRotated;
		return;
		IL_1DD:
		A_1.TextDirection = TextDirection.LeftToRight;
	}

	// Token: 0x06001E96 RID: 7830 RVA: 0x001EA3D0 File Offset: 0x001E93D0
	private void ᜀ(XmlReader A_0, TableCell A_1)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num2;
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("հੲմቶ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (attribute == ClipboardData.b("ŰၲŴ", a_))
						{
							num = 6;
							continue;
						}
						float a_2 = (float)num2 / 20f;
						num = 9;
						continue;
					}
					case 1:
					{
						string attribute2 = A_0.GetAttribute(ClipboardData.b("ٰ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
						num2 = int.Parse(attribute2, NumberStyles.Integer, CultureInfo.InvariantCulture);
						num = 0;
						continue;
					}
					case 2:
						goto IL_118;
					case 3:
						return;
					case 4:
					{
						string a;
						if ((a = attribute) != null)
						{
							goto IL_F2;
						}
						return;
					}
					case 5:
					{
						float a_2;
						A_1.TrackCellFormat.CellWidth = a_2;
						num = 2;
						continue;
					}
					case 6:
						goto IL_20E;
					case 7:
						if (attribute != null)
						{
							num = 4;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F2;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 8:
						num = 10;
						continue;
					case 9:
					{
						if (this.ᜈ)
						{
							num = 5;
							continue;
						}
						float a_2;
						A_1.CellFormat.CellWidth = a_2;
						num = 12;
						continue;
					}
					case 10:
					{
						string a;
						if (!(a == ClipboardData.b("ၰٲŴᡶ", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_C3;
					}
					case 11:
						if (true)
						{
						}
						num = 1;
						continue;
					case 12:
						goto IL_16B;
					}
					break;
					IL_F2:
					num = 8;
				}
			}
			return;
			IL_C3:
			A_1.CellFormat.IsAutoResized = true;
			this.ᜀ(A_1, this.ᜣ);
			return;
			IL_118:
			IL_16B:
			goto IL_221;
			IL_20E:
			A_1.Scaling = (float)num2 / 50f;
			A_1.WidthType = FtsWidth.Percentage;
			return;
			IL_221:
			A_1.WidthType = FtsWidth.Point;
			return;
		}
		}
	}

	// Token: 0x06001E97 RID: 7831 RVA: 0x001EA608 File Offset: 0x001E9608
	private void ᜁ(TableCell A_0)
	{
		int num = 3;
		float num2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
			{
				if (A_0.OwnerRow.OwnerTable == null)
				{
					num = 1;
					continue;
				}
				List<float> list = A_0.OwnerRow.OwnerTable.TableGrid;
				num2 = list[list.Count - 1];
				num2 = num2 * A_0.OwnerRow.OwnerTable.DocxTableFormat.Format.Scaling / 100f;
				num = 4;
				continue;
			}
			case 1:
				goto IL_F2;
			case 2:
				goto IL_CC;
			case 4:
				if (this.ᜈ)
				{
					num = 2;
					continue;
				}
				goto IL_10E;
			case 5:
				goto IL_CF;
			}
			if (A_0.OwnerRow == null)
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
				num = 5;
				continue;
			}
			IL_CF:
			num = 0;
		}
		IL_CC:
		A_0.TrackCellFormat.CellWidth = num2 * A_0.Scaling / 100f;
		return;
		IL_F2:
		return;
		IL_10E:
		A_0.CellFormat.CellWidth = num2 * A_0.Scaling / 100f;
	}

	// Token: 0x06001E98 RID: 7832 RVA: 0x001EA73C File Offset: 0x001E973C
	private void ᜀ(TableCell A_0, int A_1)
	{
		int num = 11;
		float num2;
		for (;;)
		{
			float width;
			switch (num)
			{
			case 0:
				num = 7;
				continue;
			case 1:
			{
				List<float> list;
				if (list.Count == 0)
				{
					goto IL_1CF;
				}
				num = 3;
				continue;
			}
			case 2:
				goto IL_122;
			case 3:
			{
				List<float> list;
				if (A_1 + 1 < list.Count)
				{
					num = 8;
					continue;
				}
				return;
			}
			case 4:
			{
				if (true)
				{
				}
				List<float> list;
				if (list != null)
				{
					num = 5;
					continue;
				}
				goto IL_122;
			}
			case 5:
				num = 1;
				continue;
			case 6:
				if (this.ᜈ)
				{
					num = 13;
					continue;
				}
				goto IL_1DF;
			case 7:
				if (A_0.OwnerRow.OwnerTable != null)
				{
					List<float> list = A_0.OwnerRow.OwnerTable.TableGrid;
					num2 = 0f;
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1CF;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 8:
			{
				List<float> list;
				num2 = list[A_1 + 1] - list[A_1];
				num = 12;
				continue;
			}
			case 9:
				num2 = width / (float)A_0.OwnerRow.Cells.Count;
				num = 15;
				continue;
			case 10:
				goto IL_10C;
			case 12:
				goto IL_AA;
			case 13:
				goto IL_CB;
			case 14:
				if (width != 0f)
				{
					num = 9;
					continue;
				}
				goto IL_AA;
			case 15:
				goto IL_AA;
			}
			if (A_0.OwnerRow != null)
			{
				num = 0;
				continue;
			}
			break;
			IL_AA:
			num = 6;
			continue;
			IL_122:
			width = A_0.OwnerRow.OwnerTable.Width;
			num = 14;
			continue;
			IL_1CF:
			num = 2;
		}
		return;
		IL_CB:
		A_0.TrackCellFormat.CellWidth = num2 / 20f;
		return;
		IL_10C:
		return;
		IL_1DF:
		A_0.CellFormat.CellWidth = num2 / 20f;
	}

	// Token: 0x06001E99 RID: 7833 RVA: 0x001EA93C File Offset: 0x001E993C
	private void ᜀ(TableCell A_0, int A_1, int A_2)
	{
		int num = 9;
		float num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜈ)
				{
					num = 12;
					continue;
				}
				goto IL_173;
			case 1:
			{
				List<float> list;
				if (A_1 + A_2 < list.Count)
				{
					num = 11;
					continue;
				}
				return;
			}
			case 2:
			{
				if (A_0.OwnerRow.OwnerTable == null)
				{
					goto IL_E4;
				}
				List<float> list = A_0.OwnerRow.OwnerTable.TableGrid;
				num = 5;
				continue;
			}
			case 3:
				goto IL_7A;
			case 4:
				num = 0;
				continue;
			case 5:
			{
				List<float> list;
				if (list != null)
				{
					num = 8;
					continue;
				}
				return;
			}
			case 6:
				num = 2;
				continue;
			case 7:
			{
				List<float> list;
				if (list.Count == 0)
				{
					num = 10;
					continue;
				}
				num2 = 0f;
				num = 1;
				continue;
			}
			case 8:
				num = 7;
				continue;
			case 10:
				return;
			case 11:
			{
				List<float> list;
				num2 = list[A_1 + A_2] - list[A_1];
				num = 4;
				continue;
			}
			case 12:
				goto IL_CA;
			}
			if (A_0.OwnerRow != null)
			{
				num = 6;
				continue;
			}
			goto IL_7A;
			IL_E4:
			num = 3;
			continue;
			IL_7A:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_E4;
			}
			goto Block_3;
		}
		return;
		Block_3:
		if (false)
		{
		}
		if (true)
		{
		}
		return;
		IL_CA:
		A_0.TrackCellFormat.CellWidth = num2 / 20f;
		return;
		IL_173:
		A_0.CellFormat.CellWidth = num2 / 20f;
	}

	// Token: 0x06001E9A RID: 7834 RVA: 0x001EAAD0 File Offset: 0x001E9AD0
	private void ᜁ(XmlReader A_0, TableRow A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 31;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.IsEmptyElement)
					{
						num = 7;
						continue;
					}
					num = 42;
					continue;
				case 1:
					goto IL_555;
				case 2:
				{
					float num2;
					if (num2 != 3.4028235E+38f)
					{
						num = 27;
						continue;
					}
					goto IL_555;
				}
				case 3:
					goto IL_555;
				case 4:
					goto IL_555;
				case 5:
					goto IL_555;
				case 6:
					goto IL_5E3;
				case 7:
					return;
				case 8:
					goto IL_555;
				case 9:
					num = 19;
					continue;
				case 10:
					goto IL_162;
				case 11:
					num = 22;
					continue;
				case 12:
					num = 52;
					continue;
				case 13:
				{
					string attribute;
					if (!(attribute == ClipboardData.b("ᥨࡪᥬ", a_)))
					{
						num = 53;
						continue;
					}
					RowFormat rowFormat;
					rowFormat.GridBeforeWidth.ᜀ(int.Parse(A_0.GetAttribute(ClipboardData.b("Ṩ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_))));
					rowFormat.GridBeforeWidth.ᜀ(FtsWidth.Percentage);
					num = 54;
					continue;
				}
				case 14:
					spr᧓.ᝎ = new Dictionary<string, int>(11)
					{
						{
							ClipboardData.b("ᵨᥪ╬੮ᡰᑲᵴͶ", a_),
							0
						},
						{
							ClipboardData.b("ᵨ४ŬⱮᑰὲᥴ⑶ॸ᩺Ṽᙾ", a_),
							1
						},
						{
							ClipboardData.b("ᵨ४Ŭ❮ᑰቲᅴቶ୸", a_),
							2
						},
						{
							ClipboardData.b("൨๪Ŭ", a_),
							3
						},
						{
							ClipboardData.b("hժṬ", a_),
							4
						},
						{
							ClipboardData.b("੨੪ͬ᭮≰ͲᥴṶ൸", a_),
							5
						},
						{
							ClipboardData.b("੨ժ୬㱮հੲᥴቶ", a_),
							6
						},
						{
							ClipboardData.b("๨ᥪѬ୮㍰ᙲ፴ᡶ୸Ṻ", a_),
							7
						},
						{
							ClipboardData.b("๨ᥪѬ୮ばᕲŴቶ୸", a_),
							8
						},
						{
							ClipboardData.b("Ṩ⥪࡬८ṰŲၴ", a_),
							9
						},
						{
							ClipboardData.b("Ṩ⩪୬᭮ᑰŲ", a_),
							10
						}
					};
					num = 6;
					continue;
				case 15:
				{
					string attribute;
					if ((attribute = A_0.GetAttribute(ClipboardData.b("ᵨቪᵬ੮", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_))) != null)
					{
						num = 49;
						continue;
					}
					goto IL_555;
				}
				case 16:
					goto IL_555;
				case 17:
				{
					string attribute2;
					if ((attribute2 = A_0.GetAttribute(ClipboardData.b("ᵨቪᵬ੮", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_))) != null)
					{
						num = 11;
						continue;
					}
					goto IL_555;
				}
				case 18:
				{
					string attribute;
					if (!(attribute == ClipboardData.b("൨፪౬", a_)))
					{
						num = 24;
						continue;
					}
					RowFormat rowFormat;
					rowFormat.GridBeforeWidth.ᜀ(FtsWidth.Point);
					rowFormat.GridBeforeWidth.ᜀ(int.Parse(A_0.GetAttribute(ClipboardData.b("Ṩ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_))));
					num = 36;
					continue;
				}
				case 19:
				{
					int num3;
					switch (num3)
					{
					case 0:
						this.ᜀ(A_0, A_1);
						num = 43;
						continue;
					case 1:
					{
						float num2 = this.ᜀ(A_0, ClipboardData.b("Ṩ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_));
						num = 2;
						continue;
					}
					case 2:
						A_1.IsHeader = true;
						num = 30;
						continue;
					case 3:
						goto IL_589;
					case 4:
						A_1.IsInsertRevision = true;
						num = 4;
						continue;
					case 5:
					{
						RowFormat rowFormat;
						rowFormat.IsBreakAcrossPages = false;
						num = 3;
						continue;
					}
					case 6:
						goto IL_555;
					case 7:
					{
						RowFormat rowFormat;
						rowFormat.GridBefore = Convert.ToInt32(this.ᜁ(A_0, ClipboardData.b("Ὠ੪Ŭ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_)));
						num = 1;
						continue;
					}
					case 8:
					{
						RowFormat rowFormat;
						rowFormat.GridAfter = Convert.ToInt32(this.ᜁ(A_0, ClipboardData.b("Ὠ੪Ŭ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_)));
						num = 8;
						continue;
					}
					case 9:
						num = 15;
						continue;
					case 10:
						num = 17;
						continue;
					default:
						num = 41;
						continue;
					}
					break;
				}
				case 20:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 12;
						continue;
					}
					goto IL_7A8;
				}
				case 21:
					goto IL_555;
				case 22:
				{
					string attribute2;
					if (!(attribute2 == ClipboardData.b("ᥨࡪᥬ", a_)))
					{
						num = 32;
						continue;
					}
					RowFormat rowFormat;
					rowFormat.GridAfterWidth.ᜀ(int.Parse(A_0.GetAttribute(ClipboardData.b("Ṩ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_))));
					rowFormat.GridAfterWidth.ᜀ(FtsWidth.Percentage);
					num = 25;
					continue;
				}
				case 23:
					num = 21;
					continue;
				case 24:
					num = 5;
					continue;
				case 25:
					goto IL_555;
				case 26:
					if (A_0.LocalName != string.Empty)
					{
						num = 47;
						continue;
					}
					goto IL_555;
				case 27:
				{
					float num2;
					RowFormat rowFormat;
					rowFormat.CellSpacing = num2;
					num = 44;
					continue;
				}
				case 28:
				{
					bool flag;
					if (!flag)
					{
						num = 37;
						continue;
					}
					goto IL_852;
				}
				case 29:
				{
					string attribute2;
					if (!(attribute2 == ClipboardData.b("൨፪౬", a_)))
					{
						num = 23;
						continue;
					}
					RowFormat rowFormat;
					rowFormat.GridAfterWidth.ᜀ(FtsWidth.Point);
					rowFormat.GridAfterWidth.ᜀ(int.Parse(A_0.GetAttribute(ClipboardData.b("Ṩ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_))));
					num = 16;
					continue;
				}
				case 30:
					goto IL_555;
				case 31:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_589;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 32:
					num = 29;
					continue;
				case 33:
					goto IL_852;
				case 34:
					goto IL_7A8;
				case 35:
					goto IL_555;
				case 36:
					goto IL_555;
				case 37:
					A_0.Read();
					num = 56;
					continue;
				case 38:
					goto IL_482;
				case 39:
					goto IL_852;
				case 40:
					goto IL_555;
				case 41:
					num = 34;
					continue;
				case 42:
				{
					if (A_1 == null)
					{
						num = 45;
						continue;
					}
					bool flag = false;
					A_0.Read();
					this.ᜀ(A_0);
					RowFormat rowFormat = A_1.RowFormat;
					num = 39;
					continue;
				}
				case 43:
					goto IL_555;
				case 44:
					goto IL_555;
				case 45:
					goto IL_84C;
				case 46:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 57;
						continue;
					}
					A_0.Read();
					num = 33;
					continue;
				case 47:
				{
					RowFormat rowFormat;
					rowFormat.XmlProps2010.Add(this.ᜢ(A_0));
					bool flag = true;
					num = 40;
					continue;
				}
				case 48:
				{
					if (!(A_0.LocalName != ClipboardData.b("ᵨᥪ㵬ᵮ", a_)))
					{
						num = 38;
						continue;
					}
					bool flag = false;
					num = 46;
					continue;
				}
				case 49:
					num = 13;
					continue;
				case 50:
				{
					int num3;
					string localName;
					if (spr᧓.ᝎ.TryGetValue(localName, out num3))
					{
						num = 9;
						continue;
					}
					goto IL_7A8;
				}
				case 51:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 55;
						continue;
					}
					return;
				case 52:
					if (spr᧓.ᝎ == null)
					{
						num = 14;
						continue;
					}
					goto IL_5E3;
				case 53:
					num = 18;
					continue;
				case 54:
					goto IL_555;
				case 55:
					num = 48;
					continue;
				case 56:
					goto IL_852;
				case 57:
					num = 20;
					continue;
				}
				if (true)
				{
				}
				if (A_0.LocalName != ClipboardData.b("ᵨᥪ㵬ᵮ", a_))
				{
					num = 10;
					continue;
				}
				num = 0;
				continue;
				IL_555:
				num = 28;
				continue;
				IL_589:
				A_1.IsDeleteRevision = true;
				num = 35;
				continue;
				IL_5E3:
				num = 50;
				continue;
				IL_7A8:
				num = 26;
				continue;
				IL_852:
				num = 51;
			}
			IL_162:
			throw new XmlException(ClipboardData.b("ᵨ੪ཬͮᑰ卲ݴᡶ๸孺᡼፾ﶈ", a_));
			IL_482:
			return;
			IL_84C:
			throw new ArgumentException(ClipboardData.b("ᵨ੪ཬͮᑰ卲ݴᡶ๸", a_));
		}
		}
	}

	// Token: 0x06001E9B RID: 7835 RVA: 0x001EB42C File Offset: 0x001EA42C
	private string ᜁ(XmlReader A_0, string A_1, string A_2)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_7B;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7D;
				default:
					if (false)
					{
					}
					if (A_2 == null)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_7F;
				}
				break;
			case 2:
				goto IL_30;
			}
			if (A_0.AttributeCount == 0)
			{
				num = 2;
			}
			else
			{
				num = 1;
			}
		}
		IL_30:
		goto IL_7D;
		IL_7B:
		return A_0.GetAttribute(A_1);
		IL_7D:
		return null;
		IL_7F:
		return A_0.GetAttribute(A_1, A_2);
	}

	// Token: 0x06001E9C RID: 7836 RVA: 0x001EB4C0 File Offset: 0x001EA4C0
	private void ᜀ(XmlReader A_0, TableRow A_1)
	{
		int a_ = 11;
		for (;;)
		{
			float num = this.ᜀ(A_0, ClipboardData.b("ݰቲᥴ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
			int num2 = 2;
			for (;;)
			{
				string attribute;
				switch (num2)
				{
				case 0:
					num2 = 7;
					continue;
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
						if (true)
						{
						}
						A_1.Height = num;
						num2 = 5;
						continue;
					}
					break;
				case 2:
					if (num != 3.4028235E+38f)
					{
						num2 = 1;
						continue;
					}
					goto IL_A7;
				case 3:
					if (attribute != null)
					{
						num2 = 0;
						continue;
					}
					return;
				case 4:
					A_1.HeightType = TableRowHeightType.Exactly;
					num2 = 6;
					continue;
				case 5:
					goto IL_A7;
				case 6:
					return;
				case 7:
					if (attribute == ClipboardData.b("ᑰ୲ᑴᑶ൸", a_))
					{
						num2 = 4;
						continue;
					}
					return;
				}
				break;
				IL_A7:
				attribute = A_0.GetAttribute(ClipboardData.b("ᥰⅲt᭶ᱸ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
				num2 = 3;
			}
		}
	}

	// Token: 0x06001E9D RID: 7837 RVA: 0x001EB600 File Offset: 0x001EA600
	private void ᜀ(TableRow A_0, Table A_1)
	{
		int num = 4;
		for (;;)
		{
			Borders borders;
			Borders borders2;
			switch (num)
			{
			case 0:
				if (borders.Bottom.BorderType == BorderStyle.None)
				{
					num = 15;
					continue;
				}
				goto IL_262;
			case 1:
				if (A_1.DocxTableFormat.StyleName.Length == 0)
				{
					num = 16;
					continue;
				}
				return;
			case 2:
				return;
			case 3:
				goto IL_262;
			case 5:
				if (borders.Top.BorderType == BorderStyle.None)
				{
					num = 10;
					continue;
				}
				goto IL_187;
			case 6:
				goto IL_237;
			case 7:
				goto IL_187;
			case 8:
				if (borders.Horizontal.BorderType == BorderStyle.None)
				{
					num = 11;
					continue;
				}
				goto IL_20C;
			case 9:
				num = 1;
				continue;
			case 10:
				borders2.Top.HasNoneStyle = true;
				num = 7;
				continue;
			case 11:
				borders2.Horizontal.HasNoneStyle = true;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1E3;
				default:
					if (false)
					{
					}
					num = 13;
					continue;
				}
				break;
			case 12:
				if (borders.Left.BorderType == BorderStyle.None)
				{
					num = 23;
					continue;
				}
				goto IL_1CB;
			case 13:
				goto IL_20C;
			case 14:
				if (borders.Vertical.BorderType == BorderStyle.None)
				{
					num = 19;
					continue;
				}
				return;
			case 15:
				if (true)
				{
				}
				borders2.Bottom.HasNoneStyle = true;
				num = 3;
				continue;
			case 16:
				goto IL_C5;
			case 17:
				A_0.RowFormat.ImportContainer(A_1.DocxTableFormat.Format);
				A_0.RowFormat.Scaling = A_1.DocxTableFormat.Format.Scaling;
				A_0.RowFormat.LayoutType = A_1.DocxTableFormat.Format.LayoutType;
				num = 20;
				continue;
			case 18:
				if (borders.Right.BorderType == BorderStyle.None)
				{
					goto IL_1E3;
				}
				goto IL_237;
			case 19:
				borders2.Vertical.HasNoneStyle = true;
				num = 2;
				continue;
			case 20:
				if (A_1.DocxTableFormat.StyleName != null)
				{
					num = 9;
					continue;
				}
				goto IL_C5;
			case 21:
				goto IL_1CB;
			case 22:
				borders2.Right.HasNoneStyle = true;
				num = 6;
				continue;
			case 23:
				borders2.Left.HasNoneStyle = true;
				num = 21;
				continue;
			}
			if (A_1.DocxTableFormat.HasFormat)
			{
				num = 17;
				continue;
			}
			break;
			IL_C5:
			borders = A_1.DocxTableFormat.Format.Borders;
			borders2 = A_0.RowFormat.Borders;
			num = 0;
			continue;
			IL_187:
			num = 8;
			continue;
			IL_1CB:
			num = 18;
			continue;
			IL_1E3:
			num = 22;
			continue;
			IL_20C:
			num = 14;
			continue;
			IL_237:
			num = 5;
			continue;
			IL_262:
			num = 12;
		}
	}

	// Token: 0x06001E9E RID: 7838 RVA: 0x001EB92C File Offset: 0x001EA92C
	private void ᜀ(XmlTableFormat A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.Format.Borders.IsDefault)
				{
					num = 2;
					continue;
				}
				return;
			case 1:
				goto IL_5E;
			case 2:
				A_0.Format.Borders.BorderType = BorderStyle.None;
				num = 1;
				continue;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_A2;
				}
				break;
			}
			if (!string.IsNullOrEmpty(A_0.StyleName))
			{
				num = 4;
			}
			else
			{
				if (true)
				{
				}
				num = 0;
			}
		}
		IL_5E:
		return;
		IL_A2:
		if (false)
		{
		}
	}

	// Token: 0x06001E9F RID: 7839 RVA: 0x001EB9E4 File Offset: 0x001EA9E4
	private void ᜎ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 45;
			for (;;)
			{
				string localName;
				bool flag;
				Table table;
				RowFormat rowFormat;
				switch (num)
				{
				case 0:
					if (A_0.LocalName != localName)
					{
						if (true)
						{
						}
						flag = false;
						num = 48;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_253;
					default:
						if (false)
						{
						}
						num = 13;
						continue;
					}
					break;
				case 1:
					if (A_1 is TableRow)
					{
						num = 8;
						continue;
					}
					goto IL_8FE;
				case 2:
					goto IL_511;
				case 3:
				{
					string localName2;
					if ((localName2 = A_0.LocalName) != null)
					{
						num = 37;
						continue;
					}
					goto IL_534;
				}
				case 4:
					goto IL_511;
				case 5:
					if (A_0.LocalName != string.Empty)
					{
						num = 43;
						continue;
					}
					goto IL_511;
				case 6:
					goto IL_534;
				case 7:
					if (!flag)
					{
						num = 47;
						continue;
					}
					goto IL_8FE;
				case 8:
					table = (A_1 as TableRow).OwnerTable;
					num = 52;
					continue;
				case 9:
					return;
				case 10:
					goto IL_511;
				case 11:
				{
					string attribute;
					if (attribute == ClipboardData.b("᝵൷๹፻", a_))
					{
						num = 17;
						continue;
					}
					num = 16;
					continue;
				}
				case 12:
					num = 0;
					continue;
				case 13:
					return;
				case 14:
					goto IL_2A2;
				case 15:
					goto IL_511;
				case 16:
				{
					string attribute;
					if (attribute == ClipboardData.b("ٵ᭷๹", a_))
					{
						num = 54;
						continue;
					}
					num = 57;
					continue;
				}
				case 17:
					rowFormat.IsAutoResized = true;
					table.PreferredTableWidth.ᜀ(FtsWidth.Auto);
					table.PreferredTableWidth.ᜀ(0);
					num = 50;
					continue;
				case 18:
					goto IL_511;
				case 19:
					goto IL_2A2;
				case 20:
					goto IL_511;
				case 21:
					goto IL_511;
				case 22:
					goto IL_8FE;
				case 23:
					num = 33;
					continue;
				case 24:
					goto IL_8FE;
				case 25:
					if (!(A_1 is TableRow))
					{
						num = 23;
						continue;
					}
					goto IL_511;
				case 26:
				{
					float num2;
					if (num2 != 3.4028235E+38f)
					{
						num = 38;
						continue;
					}
					goto IL_511;
				}
				case 27:
					num = 3;
					continue;
				case 28:
					goto IL_511;
				case 29:
					spr᧓.ᝏ = new Dictionary<string, int>(17)
					{
						{
							ClipboardData.b("ɵ᩷ᙹ⽻੽勵", a_),
							0
						},
						{
							ClipboardData.b("ɵ᩷ᙹ⭻", a_),
							1
						},
						{
							ClipboardData.b("ɵ᩷ᙹ㹻ᅽﮇ", a_),
							2
						},
						{
							ClipboardData.b("ɵ᩷ᙹ㽻᭽힃", a_),
							3
						},
						{
							ClipboardData.b("ᱵ᭷", a_),
							4
						},
						{
							ClipboardData.b("ɵ᩷ᙹ㽻᭽즃慎", a_),
							5
						},
						{
							ClipboardData.b("ɵ᩷ᙹ㕻ၽ", a_),
							6
						},
						{
							ClipboardData.b("ɵ᩷ᙹほώ勵", a_),
							7
						},
						{
							ClipboardData.b("ɵ᩷ᙹⱻ౽썿", a_),
							8
						},
						{
							ClipboardData.b("ɵ᩷ᙹⱻ౽앿嬨잃", a_),
							9
						},
						{
							ClipboardData.b("ትᵷᙹ", a_),
							10
						},
						{
							ClipboardData.b("ήᙷॹ", a_),
							11
						},
						{
							ClipboardData.b("ɵ᩷ᙹ౻⹽", a_),
							12
						},
						{
							ClipboardData.b("յၷṹ", a_),
							13
						},
						{
							ClipboardData.b("ɵ᩷ᙹほᅽ", a_),
							14
						},
						{
							ClipboardData.b("ɵ᩷ᙹ㽻ώ", a_),
							15
						},
						{
							ClipboardData.b("ɵ᩷ᙹ㡻᭽ﺉﺏ", a_),
							16
						}
					};
					num = 34;
					continue;
				case 30:
					goto IL_511;
				case 31:
				{
					int num3;
					switch (num3)
					{
					case 0:
						this.ᜀ(A_0, table);
						num = 55;
						continue;
					case 1:
					{
						string attribute = A_0.GetAttribute(ClipboardData.b("ɵŷ੹᥻", a_), ClipboardData.b("ṵ౷๹౻䑽꽿궁벑ﮓﶗ첟쒡쮣풥얧쮩\ud8ab\uddad麯\uddb1욳통鞷춹펻첽꒿닁뛃꧅ꯇ꿉뿋뷍맏병돓믕듗탟퓡쯣该觧菩苫", a_));
						num = 11;
						continue;
					}
					case 2:
						this.ᜂ(A_0, A_1);
						num = 59;
						continue;
					case 3:
					{
						float num2 = this.ᜀ(A_0, ClipboardData.b("ŵ", a_), ClipboardData.b("ṵ౷๹౻䑽꽿궁벑ﮓﶗ첟쒡쮣풥얧쮩\ud8ab\uddad麯\uddb1욳통鞷춹펻첽꒿닁뛃꧅ꯇ꿉뿋뷍맏병돓믕듗탟퓡쯣该觧菩苫", a_));
						num = 26;
						continue;
					}
					case 4:
						rowFormat.HorizontalAlignment = this.\u171D(A_0);
						num = 20;
						continue;
					case 5:
						this.\u170D(A_0, A_1);
						num = 30;
						continue;
					case 6:
					{
						string attribute2 = A_0.GetAttribute(ClipboardData.b("ŵ", a_), ClipboardData.b("ṵ౷๹౻䑽꽿궁벑ﮓﶗ첟쒡쮣풥얧쮩\ud8ab\uddad麯\uddb1욳통鞷춹펻첽꒿닁뛃꧅ꯇ꿉뿋뷍맏병돓믕듗탟퓡쯣该觧菩苫", a_));
						rowFormat.RowIndent = float.Parse(attribute2, NumberStyles.Float, CultureInfo.InvariantCulture) / 20f;
						num = 4;
						continue;
					}
					case 7:
						this.ᜀ(A_0, rowFormat);
						num = 10;
						continue;
					case 8:
					case 9:
						this.ᜉ = true;
						A_0.Read();
						this.ᜀ(A_0);
						this.ᜎ(A_0, table);
						this.ᜉ = false;
						num = 21;
						continue;
					case 10:
						table.SetDeleteRev(true);
						num = 40;
						continue;
					case 11:
						table.SetInsertRev(true);
						num = 2;
						continue;
					case 12:
						this.ᜁ(A_0, table);
						num = 18;
						continue;
					case 13:
						this.ᜁ(A_0, rowFormat);
						num = 15;
						continue;
					case 14:
						this.ᜂ(A_0, table);
						num = 49;
						continue;
					case 15:
						this.ᜄ(A_0, table);
						num = 58;
						continue;
					case 16:
						this.ᜃ(A_0, table);
						num = 28;
						continue;
					default:
						num = 46;
						continue;
					}
					break;
				}
				case 32:
					goto IL_511;
				case 33:
					if (this.ᜉ)
					{
						num = 42;
						continue;
					}
					table.DocxTableFormat.NodeArray2010.Add(this.ᜢ(A_0));
					num = 14;
					continue;
				case 34:
					goto IL_319;
				case 35:
					goto IL_511;
				case 36:
					goto IL_511;
				case 37:
					num = 53;
					continue;
				case 38:
				{
					float num2;
					rowFormat.CellSpacing = num2;
					num = 36;
					continue;
				}
				case 39:
					goto IL_511;
				case 40:
					goto IL_511;
				case 41:
				{
					int a_2 = Convert.ToInt32(Convert.ToDecimal(A_0.GetAttribute(ClipboardData.b("ŵ", a_), ClipboardData.b("ṵ౷๹౻䑽꽿궁벑ﮓﶗ첟쒡쮣풥얧쮩\ud8ab\uddad麯\uddb1욳통鞷춹펻첽꒿닁뛃꧅ꯇ꿉뿋뷍맏병돓믕듗탟퓡쯣该觧菩苫", a_))));
					rowFormat.IsAutoResized = false;
					table.PreferredTableWidth.ᜀ(a_2);
					table.PreferredTableWidth.ᜀ(FtsWidth.Point);
					num = 32;
					continue;
				}
				case 42:
					table.TrackTblFormat.NodeArray2010.Add(this.ᜢ(A_0));
					num = 19;
					continue;
				case 43:
					num = 25;
					continue;
				case 44:
					num = 31;
					continue;
				case 46:
					num = 6;
					continue;
				case 47:
					A_0.Read();
					num = 24;
					continue;
				case 48:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 27;
						continue;
					}
					A_0.Read();
					num = 22;
					continue;
				case 49:
					goto IL_253;
				case 50:
					goto IL_511;
				case 51:
				{
					string localName2;
					int num3;
					if (spr᧓.ᝏ.TryGetValue(localName2, out num3))
					{
						num = 44;
						continue;
					}
					goto IL_534;
				}
				case 52:
					goto IL_8FE;
				case 53:
					if (spr᧓.ᝏ == null)
					{
						num = 29;
						continue;
					}
					goto IL_319;
				case 54:
				{
					int num4 = int.Parse(A_0.GetAttribute(ClipboardData.b("ŵ", a_), ClipboardData.b("ṵ౷๹౻䑽꽿궁벑ﮓﶗ첟쒡쮣풥얧쮩\ud8ab\uddad麯\uddb1욳통鞷춹펻첽꒿닁뛃꧅ꯇ꿉뿋뷍맏병돓믕듗탟퓡쯣该觧菩苫", a_)));
					rowFormat.IsAutoResized = false;
					table.PreferredTableWidth.ᜀ(FtsWidth.Percentage);
					table.PreferredTableWidth.ᜀ(num4 / 50);
					num = 39;
					continue;
				}
				case 55:
					goto IL_511;
				case 56:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 12;
						continue;
					}
					return;
				case 57:
				{
					string attribute;
					if (attribute == ClipboardData.b("ትw᭹", a_))
					{
						num = 41;
						continue;
					}
					goto IL_511;
				}
				case 58:
					goto IL_511;
				case 59:
					goto IL_511;
				}
				if (A_0.IsEmptyElement)
				{
					num = 9;
					continue;
				}
				localName = A_0.LocalName;
				rowFormat = this.ᜂ(A_1);
				flag = false;
				A_0.Read();
				this.ᜀ(A_0);
				table = (A_1 as Table);
				num = 1;
				continue;
				IL_2A2:
				flag = true;
				num = 35;
				continue;
				IL_319:
				num = 51;
				continue;
				IL_511:
				num = 7;
				continue;
				IL_253:
				goto IL_511;
				IL_534:
				num = 5;
				continue;
				IL_8FE:
				num = 56;
			}
			return;
		}
		}
	}

	// Token: 0x06001EA0 RID: 7840 RVA: 0x001EC3D0 File Offset: 0x001EB3D0
	private void ᜄ(XmlReader A_0, Table A_1)
	{
		int a_ = 2;
		if (true)
		{
		}
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ṧ୩k", a_), ClipboardData.b("gṩᡫṭ䩯嵱孳յ᭷ቹ᥻፽ꪃﶏﺑ秊ﶛ펟財쮣풥쾧薩\udbab솭슯횱쒳쒵ힷ\ud9b9\ud9bb춽뎿ꯁ꫃ꇅꗇꛉﳍ崙뗗믙뗛냝", a_));
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
						goto IL_64;
					default:
						goto IL_82;
					}
					break;
				case 1:
					if (attribute != null)
					{
						num = 2;
						continue;
					}
					return;
				case 2:
					A_1.Title = attribute;
					goto IL_64;
				}
				break;
				IL_64:
				num = 0;
			}
		}
		IL_82:
		if (false)
		{
		}
	}

	// Token: 0x06001EA1 RID: 7841 RVA: 0x001EC474 File Offset: 0x001EB474
	private void ᜃ(XmlReader A_0, Table A_1)
	{
		int a_ = 12;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ѱᕳ᩵", a_), ClipboardData.b("ᩱsɵࡷ䁹卻兽ﾋꂍﾏ쾟킡즣장\udca7\ud9a9芫솭슯햱鮳솵ힷ좹\ud8bb캽늿귁ꟃꏅ믇막ꗋꃍ럏뿑룓崙쿟迡藣迥蛧", a_));
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (attribute != null)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					A_1.TableDescription = attribute;
					goto IL_64;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_64;
					default:
						goto IL_82;
					}
					break;
				}
				break;
				IL_64:
				num = 2;
			}
		}
		IL_82:
		if (false)
		{
		}
	}

	// Token: 0x06001EA2 RID: 7842 RVA: 0x001EC518 File Offset: 0x001EB518
	private void ᜂ(XmlReader A_0, Table A_1)
	{
		int a_ = 12;
		for (;;)
		{
			string text = this.ᜄ(A_0, ClipboardData.b("ᑱᵳѵ୷๹⹻ᅽ", a_));
			int num = 15;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_87;
				case 1:
					goto IL_82;
				case 2:
					A_1.ApplyStyleForBandedRows = !this.ᜭ(text);
					num = 12;
					continue;
				case 3:
					if (text != null)
					{
						num = 9;
						continue;
					}
					goto IL_BB;
				case 4:
					goto IL_BB;
				case 5:
					if (text != null)
					{
						num = 10;
						continue;
					}
					goto IL_25E;
				case 6:
					goto IL_EF;
				case 7:
					if (text != null)
					{
						num = 8;
						continue;
					}
					goto IL_87;
				case 8:
					A_1.ApplyStyleForLastColumn = this.ᜭ(text);
					num = 0;
					continue;
				case 9:
					A_1.ApplyStyleForFirstColumn = this.ᜭ(text);
					num = 4;
					continue;
				case 10:
					A_1.ApplyStyleForBandedColumns = !this.ᜭ(text);
					num = 14;
					continue;
				case 11:
					A_1.ApplyStyleForLastRow = this.ᜭ(text);
					num = 17;
					continue;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_82;
					default:
						if (false)
						{
						}
						goto IL_126;
					}
					break;
				case 13:
					if (text != null)
					{
						num = 2;
						continue;
					}
					goto IL_126;
				case 14:
					goto IL_1CE;
				case 15:
					if (text != null)
					{
						num = 1;
						continue;
					}
					goto IL_EF;
				case 16:
					if (text != null)
					{
						num = 11;
						continue;
					}
					goto IL_227;
				case 17:
					goto IL_227;
				}
				break;
				IL_82:
				A_1.ApplyStyleForHeaderRow = this.ᜭ(text);
				num = 6;
				continue;
				IL_87:
				text = this.ᜄ(A_0, ClipboardData.b("ᱱ᭳㹵㩷᭹ቻ᩽", a_));
				num = 13;
				continue;
				IL_BB:
				text = this.ᜄ(A_0, ClipboardData.b("ṱᕳյ౷㥹፻ች", a_));
				num = 7;
				continue;
				IL_EF:
				text = this.ᜄ(A_0, ClipboardData.b("ṱᕳյ౷⡹፻ॽ", a_));
				num = 16;
				continue;
				IL_126:
				text = this.ᜄ(A_0, ClipboardData.b("ᱱ᭳⁵㩷᭹ቻ᩽", a_));
				num = 5;
				continue;
				IL_227:
				text = this.ᜄ(A_0, ClipboardData.b("ᑱᵳѵ୷๹㽻ᅽ", a_));
				num = 3;
			}
		}
		IL_1CE:
		IL_25E:
		if (true)
		{
		}
	}

	// Token: 0x06001EA3 RID: 7843 RVA: 0x001EC798 File Offset: 0x001EB798
	private bool ᜭ(string A_0)
	{
		int a_ = 10;
		if (true)
		{
		}
		if (!(A_0 == ClipboardData.b("䅯", a_)))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return true;
			}
			if (false)
			{
			}
			return false;
		}
		return true;
	}

	// Token: 0x06001EA4 RID: 7844 RVA: 0x001EC7F8 File Offset: 0x001EB7F8
	private string ᜄ(XmlReader A_0, string A_1)
	{
		int a_ = 0;
		string attribute;
		string text;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_2DF:
			text = Convert.ToString(Convert.ToInt32(attribute, 16), 2).PadLeft(11, '0');
			num = 23;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_A0;
			}
			break;
		}
		string text2;
		for (;;)
		{
			IL_35:
			switch (num)
			{
			case 0:
				return text2;
			case 1:
				goto IL_2DF;
			case 2:
				return text2;
			case 3:
				num = 10;
				continue;
			case 4:
				goto IL_EE;
			case 5:
				if (!(A_1 == ClipboardData.b("੥१ᥩᡫ⵭Ὧṱų᭵ᙷ", a_)))
				{
					num = 21;
					continue;
				}
				text2 = text.Substring(2, 1);
				num = 2;
				continue;
			case 6:
				return text2;
			case 7:
				if (!(A_1 == ClipboardData.b("ࡥݧ≩⹫཭ṯᙱ", a_)))
				{
					num = 3;
					continue;
				}
				text2 = text.Substring(1, 1);
				num = 17;
				continue;
			case 8:
				num = 18;
				continue;
			case 9:
				return text2;
			case 10:
				if (!(A_1 == ClipboardData.b("ࡥݧ㱩⹫཭ṯᙱ", a_)))
				{
					num = 24;
					continue;
				}
				text2 = text.Substring(0, 1);
				num = 6;
				continue;
			case 11:
				if (!(A_1 == ClipboardData.b("੥१ᥩᡫ㱭Ὧձ", a_)))
				{
					num = 8;
					continue;
				}
				text2 = text.Substring(4, 1);
				num = 4;
				continue;
			case 12:
				goto IL_FF;
			case 13:
				num = 11;
				continue;
			case 14:
				if (attribute != null)
				{
					num = 1;
					continue;
				}
				return text2;
			case 15:
				attribute = A_0.GetAttribute(ClipboardData.b("ၥ१٩", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧\udda9쎫\udcad풯슱욳\ud9b5\udbb7\udfb9쾻춽ꦿ곁ꏃꯅ꓇ﻋﻍﯓ믕맗동닛", a_));
				num = 14;
				continue;
			case 16:
				num = 20;
				continue;
			case 17:
				return text2;
			case 18:
				if (!(A_1 == ClipboardData.b("eŧᡩὫᩭ㍯ᵱᡳ͵ᕷᑹ", a_)))
				{
					num = 22;
					continue;
				}
				text2 = text.Substring(3, 1);
				num = 0;
				continue;
			case 19:
				if (text2 == null)
				{
					num = 15;
					continue;
				}
				return text2;
			case 20:
				if (!(A_1 == ClipboardData.b("eŧᡩὫᩭ≯ᵱͳ", a_)))
				{
					num = 13;
					continue;
				}
				text2 = text.Substring(5, 1);
				num = 9;
				continue;
			case 21:
				num = 7;
				continue;
			case 22:
				num = 5;
				continue;
			case 23:
				if (A_1 != null)
				{
					num = 16;
					continue;
				}
				return text2;
			case 24:
				num = 12;
				continue;
			}
			goto IL_A0;
		}
		IL_EE:
		return text2;
		IL_FF:
		if (true)
		{
		}
		return text2;
		IL_A0:
		text2 = A_0.GetAttribute(A_1, ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧\udda9쎫\udcad풯슱욳\ud9b5\udbb7\udfb9쾻춽ꦿ곁ꏃꯅ꓇ﻋﻍﯓ믕맗동닛", a_));
		num = 19;
		goto IL_35;
	}

	// Token: 0x06001EA5 RID: 7845 RVA: 0x001ECB58 File Offset: 0x001EBB58
	private void ᜀ(XmlReader A_0, Paddings A_1)
	{
		int a_ = 9;
		for (;;)
		{
			float num = float.MaxValue;
			int num2 = 1;
			for (;;)
			{
				string localName2;
				switch (num2)
				{
				case 0:
					A_1.Top = num;
					num2 = 31;
					continue;
				case 1:
					if (A_0.LocalName != ClipboardData.b("᭮፰ὲ㙴ቶᕸ᝺ぼṾ", a_))
					{
						num2 = 16;
						continue;
					}
					goto IL_175;
				case 2:
					num2 = 30;
					continue;
				case 3:
					goto IL_1B3;
				case 4:
					if (A_0.IsEmptyElement)
					{
						num2 = 14;
						continue;
					}
					A_0.Read();
					this.ᜀ(A_0);
					num2 = 11;
					continue;
				case 5:
					A_1.Left = num;
					num2 = 27;
					continue;
				case 6:
					goto IL_2B8;
				case 7:
				{
					string localName;
					if (!(localName == ClipboardData.b("ͮᑰᕲŴ", a_)))
					{
						num2 = 2;
						continue;
					}
					num = this.ᜀ(A_0, ClipboardData.b("ᡮ", a_), ClipboardData.b("ݮհݲմ䵶噸呺๼᱾愈ꖊﾎﶒ殺ﶚ철슢톤풦螨쒪\udfac좮麰쒲\udab4얶\uddb8쮺쾼킾ꋀꛂ뛄듆ꃈꗊ꫌ꋎ뷐ﳒ닞胠諢诤", a_));
					num2 = 34;
					continue;
				}
				case 8:
					num2 = 26;
					continue;
				case 9:
					if (A_0.LocalName != ClipboardData.b("᭮ተ㹲ᑴն", a_))
					{
						num2 = 13;
						continue;
					}
					goto IL_175;
				case 10:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num2 = 17;
						continue;
					}
					return;
				case 11:
					goto IL_1B3;
				case 12:
					num2 = 18;
					continue;
				case 13:
					goto IL_325;
				case 14:
					return;
				case 15:
					goto IL_161;
				case 16:
					num2 = 9;
					continue;
				case 17:
					num2 = 35;
					continue;
				case 18:
				{
					string localName;
					if (!(localName == ClipboardData.b("᭮ṰͲ", a_)))
					{
						num2 = 24;
						continue;
					}
					num = this.ᜀ(A_0, ClipboardData.b("ᡮ", a_), ClipboardData.b("ݮհݲմ䵶噸呺๼᱾愈ꖊﾎﶒ殺ﶚ철슢톤풦螨쒪\udfac좮麰쒲\udab4얶\uddb8쮺쾼킾ꋀꛂ뛄듆ꃈꗊ꫌ꋎ뷐ﳒ닞胠諢诤", a_));
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_175;
					default:
						if (false)
						{
						}
						num2 = 20;
						continue;
					}
					break;
				}
				case 19:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num2 = 12;
						continue;
					}
					goto IL_161;
				}
				case 20:
					if (num != 3.4028235E+38f)
					{
						num2 = 0;
						continue;
					}
					goto IL_161;
				case 21:
					if (num != 3.4028235E+38f)
					{
						num2 = 37;
						continue;
					}
					goto IL_161;
				case 22:
					num2 = 29;
					continue;
				case 23:
					if (true)
					{
					}
					num2 = 19;
					continue;
				case 24:
					num2 = 7;
					continue;
				case 25:
					A_1.Bottom = num;
					num2 = 15;
					continue;
				case 26:
					goto IL_161;
				case 27:
					goto IL_161;
				case 28:
					goto IL_1B3;
				case 29:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᵮᡰᑲᵴͶ", a_)))
					{
						num2 = 8;
						continue;
					}
					num = this.ᜀ(A_0, ClipboardData.b("ᡮ", a_), ClipboardData.b("ݮհݲմ䵶噸呺๼᱾愈ꖊﾎﶒ殺ﶚ철슢톤풦螨쒪\udfac좮麰쒲\udab4얶\uddb8쮺쾼킾ꋀꛂ뛄듆ꃈꗊ꫌ꋎ뷐ﳒ닞胠諢诤", a_));
					num2 = 21;
					continue;
				}
				case 30:
				{
					string localName;
					if (!(localName == ClipboardData.b("൮ṰݲŴᡶᑸ", a_)))
					{
						num2 = 22;
						continue;
					}
					num = this.ᜀ(A_0, ClipboardData.b("ᡮ", a_), ClipboardData.b("ݮհݲմ䵶噸呺๼᱾愈ꖊﾎﶒ殺ﶚ철슢톤풦螨쒪\udfac좮麰쒲\udab4얶\uddb8쮺쾼킾ꋀꛂ뛄듆ꃈꗊ꫌ꋎ뷐ﳒ닞胠諢诤", a_));
					num2 = 36;
					continue;
				}
				case 31:
					goto IL_161;
				case 32:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num2 = 23;
						continue;
					}
					A_0.Read();
					num2 = 3;
					continue;
				case 33:
					goto IL_161;
				case 34:
					if (num != 3.4028235E+38f)
					{
						num2 = 5;
						continue;
					}
					goto IL_161;
				case 35:
					if (!(A_0.LocalName != localName2))
					{
						num2 = 6;
						continue;
					}
					num2 = 32;
					continue;
				case 36:
					if (num != 3.4028235E+38f)
					{
						num2 = 25;
						continue;
					}
					goto IL_161;
				case 37:
					A_1.Right = num;
					num2 = 33;
					continue;
				}
				break;
				IL_161:
				A_0.Read();
				num2 = 28;
				continue;
				IL_175:
				localName2 = A_0.LocalName;
				num2 = 4;
				continue;
				IL_1B3:
				num2 = 10;
			}
		}
		return;
		IL_2B8:
		return;
		IL_325:
		throw new XmlException(ClipboardData.b("㭮ၰᅲᥴቶ奸ᙺᱼൾ", a_));
	}

	// Token: 0x06001EA6 RID: 7846 RVA: 0x001ED034 File Offset: 0x001EC034
	private void \u170D(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 1;
		switch (0)
		{
		default:
			for (;;)
			{
				float num = float.MaxValue;
				Paddings paddings = this.ᜃ(A_1);
				int num2 = 27;
				for (;;)
				{
					string localName2;
					switch (num2)
					{
					case 0:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num2 = 1;
							continue;
						}
						goto IL_17E;
					}
					case 1:
						num2 = 14;
						continue;
					case 2:
						goto IL_1DC;
					case 3:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num2 = 7;
							continue;
						}
						A_0.Read();
						num2 = 9;
						continue;
					case 4:
						if (A_0.LocalName != ClipboardData.b("፦੨♪౬ᵮ", a_))
						{
							num2 = 35;
							continue;
						}
						goto IL_193;
					case 5:
						goto IL_3BF;
					case 6:
						num2 = 25;
						continue;
					case 7:
						num2 = 0;
						continue;
					case 8:
						if (!(A_0.LocalName != localName2))
						{
							num2 = 32;
							continue;
						}
						num2 = 3;
						continue;
					case 9:
						goto IL_1DC;
					case 10:
						num2 = 8;
						continue;
					case 11:
						paddings.Left = num;
						num2 = 17;
						continue;
					case 12:
						num2 = 24;
						continue;
					case 13:
						if (num != 3.4028235E+38f)
						{
							num2 = 22;
							continue;
						}
						goto IL_17E;
					case 14:
					{
						string localName;
						if (!(localName == ClipboardData.b("፦٨᭪", a_)))
						{
							num2 = 12;
							continue;
						}
						num = this.ᜀ(A_0, ClipboardData.b("ၦ", a_), ClipboardData.b("སᵨὪᵬ啮幰屲ٴᑶᅸṺၼṾ궂﶐杖漢辠첢힤삦蚨\udcaa슬\uddae햰쎲잴\ud8b6\udab8\udeba캼첾ꣀ귂ꋄ꫆ꗈￌￎ䀹뫖룘닚돜", a_));
						num2 = 33;
						continue;
					}
					case 15:
						num2 = 19;
						continue;
					case 16:
						goto IL_17E;
					case 17:
						goto IL_17E;
					case 18:
						paddings.Bottom = num;
						num2 = 16;
						continue;
					case 19:
					{
						string localName;
						if (!(localName == ClipboardData.b("ᕦh౪լ᭮", a_)))
						{
							num2 = 36;
							continue;
						}
						num = this.ᜀ(A_0, ClipboardData.b("ၦ", a_), ClipboardData.b("སᵨὪᵬ啮幰屲ٴᑶᅸṺၼṾ궂﶐杖漢辠첢힤삦蚨\udcaa슬\uddae햰쎲잴\ud8b6\udab8\udeba캼첾ꣀ귂ꋄ꫆ꗈￌￎ䀹뫖룘닚돜", a_));
						num2 = 13;
						continue;
					}
					case 20:
						if (num != 3.4028235E+38f)
						{
							num2 = 11;
							continue;
						}
						goto IL_17E;
					case 21:
						if (A_0.NodeType != XmlNodeType.EndElement)
						{
							num2 = 10;
							continue;
						}
						return;
					case 22:
						paddings.Right = num;
						num2 = 5;
						continue;
					case 23:
						if (A_0.IsEmptyElement)
						{
							num2 = 29;
							continue;
						}
						A_0.Read();
						this.ᜀ(A_0);
						num2 = 2;
						continue;
					case 24:
					{
						string localName;
						if (localName == ClipboardData.b("୦౨൪ᥬ", a_))
						{
							num = this.ᜀ(A_0, ClipboardData.b("ၦ", a_), ClipboardData.b("སᵨὪᵬ啮幰屲ٴᑶᅸṺၼṾ궂﶐杖漢辠첢힤삦蚨\udcaa슬\uddae햰쎲잴\ud8b6\udab8\udeba캼첾ꣀ귂ꋄ꫆ꗈￌￎ䀹뫖룘닚돜", a_));
							num2 = 20;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3BF;
						default:
							if (false)
							{
							}
							num2 = 6;
							continue;
						}
						break;
					}
					case 25:
					{
						string localName;
						if (!(localName == ClipboardData.b("զ٨Ὢᥬnᱰ", a_)))
						{
							num2 = 15;
							continue;
						}
						num = this.ᜀ(A_0, ClipboardData.b("ၦ", a_), ClipboardData.b("སᵨὪᵬ啮幰屲ٴᑶᅸṺၼṾ궂﶐杖漢辠첢힤삦蚨\udcaa슬\uddae햰쎲잴\ud8b6\udab8\udeba캼첾ꣀ귂ꋄ꫆ꗈￌￎ䀹뫖룘닚돜", a_));
						num2 = 26;
						continue;
					}
					case 26:
						if (num != 3.4028235E+38f)
						{
							num2 = 18;
							continue;
						}
						goto IL_17E;
					case 27:
						if (A_0.LocalName != ClipboardData.b("፦୨ݪ⹬੮ᵰὲ㡴ᙶ୸", a_))
						{
							num2 = 28;
							continue;
						}
						goto IL_193;
					case 28:
						num2 = 4;
						continue;
					case 29:
						goto IL_1BA;
					case 30:
						paddings.Top = num;
						num2 = 34;
						continue;
					case 31:
						goto IL_17E;
					case 32:
						goto IL_2BC;
					case 33:
						if (num != 3.4028235E+38f)
						{
							num2 = 30;
							continue;
						}
						goto IL_17E;
					case 34:
						goto IL_17E;
					case 35:
						goto IL_349;
					case 36:
						num2 = 31;
						continue;
					case 37:
						goto IL_1DC;
					}
					break;
					IL_17E:
					A_0.Read();
					num2 = 37;
					continue;
					IL_3BF:
					goto IL_17E;
					IL_193:
					localName2 = A_0.LocalName;
					num2 = 23;
					continue;
					IL_1DC:
					num2 = 21;
				}
			}
			IL_1BA:
			if (true)
			{
			}
			return;
			IL_2BC:
			return;
			IL_349:
			throw new XmlException(ClipboardData.b("㍦ࡨ४Ŭ੮兰ṲᑴնṸቺ፼౾", a_));
		}
	}

	// Token: 0x06001EA7 RID: 7847 RVA: 0x001ED550 File Offset: 0x001EC550
	private Paddings ᜃ(IDocumentObject A_0)
	{
		Paddings result;
		for (;;)
		{
			result = null;
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 1:
					result = (A_0 as TableRow).TrackRowFormat.Paddings;
					num = 16;
					continue;
				case 2:
					if (this.ᜈ)
					{
						num = 14;
						continue;
					}
					result = (A_0 as TableCell).CellFormat.Paddings;
					num = 9;
					continue;
				case 3:
					return result;
				case 4:
					if (this.ᜇ)
					{
						num = 1;
						continue;
					}
					result = (A_0 as TableRow).RowFormat.Paddings;
					num = 6;
					continue;
				case 5:
					if (true)
					{
					}
					result = (A_0 as Table).TrackTblFormat.Format.Paddings;
					num = 13;
					continue;
				case 6:
					return result;
				case 7:
					if (A_0 is TableCell)
					{
						num = 15;
						continue;
					}
					return result;
				case 8:
					if (this.ᜉ)
					{
						num = 5;
						continue;
					}
					result = (A_0 as Table).DocxTableFormat.Format.Paddings;
					goto IL_1C4;
				case 9:
					return result;
				case 10:
					if (A_0 is Table)
					{
						num = 17;
						continue;
					}
					num = 12;
					continue;
				case 11:
					return result;
				case 12:
					if (A_0 is TableRow)
					{
						num = 0;
						continue;
					}
					num = 7;
					continue;
				case 13:
					return result;
				case 14:
					result = (A_0 as TableCell).TrackCellFormat.Paddings;
					num = 11;
					continue;
				case 15:
					num = 2;
					continue;
				case 16:
					return result;
				case 17:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C4;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				}
				break;
				IL_1C4:
				num = 3;
			}
		}
		return result;
	}

	// Token: 0x06001EA8 RID: 7848 RVA: 0x001ED780 File Offset: 0x001EC780
	private RowAlignment \u171D(XmlReader A_0)
	{
		int a_ = 18;
		RowAlignment result;
		for (;;)
		{
			result = RowAlignment.Left;
			string attribute = A_0.GetAttribute(ClipboardData.b("๷᭹ၻ", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return result;
				case 1:
					if (attribute != null)
					{
						num = 2;
						continue;
					}
					return result;
				case 2:
					num = 7;
					continue;
				case 3:
				{
					string a;
					if (!(a == ClipboardData.b("ᑷό᩻੽", a_)))
					{
						num = 12;
						continue;
					}
					result = RowAlignment.Left;
					goto IL_88;
				}
				case 4:
					return result;
				case 5:
				{
					string a;
					if (!(a == ClipboardData.b("੷፹᭻ᙽ", a_)))
					{
						num = 8;
						continue;
					}
					result = RowAlignment.Right;
					if (true)
					{
					}
					num = 10;
					continue;
				}
				case 6:
					num = 5;
					continue;
				case 7:
				{
					string a;
					if ((a = attribute) != null)
					{
						num = 9;
						continue;
					}
					return result;
				}
				case 8:
					num = 11;
					continue;
				case 9:
					num = 3;
					continue;
				case 10:
					return result;
				case 11:
					return result;
				case 12:
					num = 13;
					continue;
				case 13:
				{
					string a;
					if (!(a == ClipboardData.b("᭷όቻ੽", a_)))
					{
						num = 6;
						continue;
					}
					result = RowAlignment.Center;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_88;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				}
				break;
				IL_88:
				num = 4;
			}
		}
		return result;
	}

	// Token: 0x06001EA9 RID: 7849 RVA: 0x001ED93C File Offset: 0x001EC93C
	private void ᜁ(XmlReader A_0, Table A_1)
	{
		int a_ = 11;
		for (;;)
		{
			RowFormat.TablePositioning positioning = A_1.DocxTableFormat.Format.Positioning;
			A_1.DocxTableFormat.Format.WrapTextAround = true;
			string attribute = A_0.GetAttribute(ClipboardData.b("ᵰᙲ፴Ͷ㽸ॺቼቾ햀ﶄ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
			int num = 13;
			for (;;)
			{
				switch (num)
				{
				case 0:
					positioning.DistanceFromTop = float.Parse(attribute, NumberStyles.Float, CultureInfo.InvariantCulture) / 20f;
					num = 1;
					continue;
				case 1:
					goto IL_3AC;
				case 2:
					goto IL_274;
				case 3:
					if (attribute != null)
					{
						num = 27;
						continue;
					}
					goto IL_28A;
				case 4:
					this.ᜁ(positioning, attribute);
					num = 18;
					continue;
				case 5:
					if (attribute != null)
					{
						num = 9;
						continue;
					}
					goto IL_23C;
				case 6:
					if (attribute != null)
					{
						num = 4;
						continue;
					}
					goto IL_F2;
				case 7:
					positioning.DistanceFromBottom = float.Parse(attribute, NumberStyles.Float, CultureInfo.InvariantCulture) / 20f;
					num = 23;
					continue;
				case 8:
					if (attribute != null)
					{
						num = 0;
						continue;
					}
					goto IL_3AC;
				case 9:
					positioning.HorizPosition = float.Parse(attribute, NumberStyles.Float, CultureInfo.InvariantCulture) / 20f;
					num = 29;
					continue;
				case 10:
					goto IL_172;
				case 11:
					if (attribute == null)
					{
						goto IL_1B6;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_274;
					default:
						if (false)
						{
						}
						num = 22;
						continue;
					}
					break;
				case 12:
					return;
				case 13:
					if (attribute != null)
					{
						num = 28;
						continue;
					}
					goto IL_3ED;
				case 14:
					if (attribute != null)
					{
						num = 21;
						continue;
					}
					goto IL_172;
				case 15:
					this.ᜂ(positioning, attribute);
					num = 17;
					continue;
				case 16:
					goto IL_28A;
				case 17:
					goto IL_48A;
				case 18:
					goto IL_F2;
				case 19:
					goto IL_3ED;
				case 20:
					if (attribute != null)
					{
						num = 15;
						continue;
					}
					goto IL_48A;
				case 21:
					positioning.DistanceFromRight = float.Parse(attribute, NumberStyles.Float, CultureInfo.InvariantCulture) / 20f;
					num = 10;
					continue;
				case 22:
					this.ᜀ(positioning, attribute);
					num = 24;
					continue;
				case 23:
					goto IL_431;
				case 24:
					goto IL_1B6;
				case 25:
					if (attribute != null)
					{
						num = 7;
						continue;
					}
					goto IL_431;
				case 26:
					positioning.VertPosition = float.Parse(attribute, NumberStyles.Float, CultureInfo.InvariantCulture) / 20f;
					num = 12;
					continue;
				case 27:
					this.ᜃ(positioning, attribute);
					num = 16;
					continue;
				case 28:
					positioning.DistanceFromLeft = float.Parse(attribute, NumberStyles.Float, CultureInfo.InvariantCulture) / 20f;
					num = 19;
					continue;
				case 29:
					goto IL_23C;
				}
				break;
				IL_F2:
				if (true)
				{
				}
				attribute = A_0.GetAttribute(ClipboardData.b("հᅲᥴݶ⁸⡺ർ᩾", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
				num = 11;
				continue;
				IL_172:
				attribute = A_0.GetAttribute(ClipboardData.b("հᱲմㅶ୸ᑺၼ⭾ﮂ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
				num = 8;
				continue;
				IL_1B6:
				attribute = A_0.GetAttribute(ClipboardData.b("հᅲᥴݶⅸ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
				num = 5;
				continue;
				IL_23C:
				attribute = A_0.GetAttribute(ClipboardData.b("հᅲᥴݶ⁸", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
				num = 2;
				continue;
				IL_274:
				if (attribute != null)
				{
					num = 26;
					continue;
				}
				return;
				IL_28A:
				attribute = A_0.GetAttribute(ClipboardData.b("ᥰᱲݴ൶㡸ᕺṼ᝾", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
				num = 20;
				continue;
				IL_3AC:
				attribute = A_0.GetAttribute(ClipboardData.b("፰ᱲŴͶᙸᙺ㭼ൾ톄ﾊ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
				num = 25;
				continue;
				IL_3ED:
				attribute = A_0.GetAttribute(ClipboardData.b("Ͱᩲቴὶ൸㵺ོၾ힂ﾆﶈ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
				num = 14;
				continue;
				IL_431:
				attribute = A_0.GetAttribute(ClipboardData.b("ݰᙲݴͶ㡸ᕺṼ᝾", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
				num = 3;
				continue;
				IL_48A:
				attribute = A_0.GetAttribute(ClipboardData.b("հᅲᥴݶⅸ⡺ർ᩾", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
				num = 6;
			}
		}
	}

	// Token: 0x06001EAA RID: 7850 RVA: 0x001EDE30 File Offset: 0x001ECE30
	private void ᜃ(RowFormat.TablePositioning A_0, string A_1)
	{
		int a_ = 16;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_1 == ClipboardData.b("ɵᵷɹࡻ", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_AF;
			case 1:
				num = 3;
				continue;
			case 2:
				num = 6;
				continue;
			case 3:
				goto IL_85;
			case 4:
				num = 0;
				continue;
			case 5:
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
			case 6:
				if (!(A_1 == ClipboardData.b("ٵ᥷ᵹ᥻", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_6B;
			}
			if (true)
			{
			}
			if (A_1 == null)
			{
				goto IL_E5;
			}
			num = 2;
		}
		IL_6B:
		A_0.VertRelationTo = VerticalRelation.Page;
		return;
		IL_85:
		goto IL_E5;
		IL_AF:
		A_0.VertRelationTo = VerticalRelation.Paragraph;
		return;
		IL_E5:
		A_0.VertRelationTo = VerticalRelation.Margin;
	}

	// Token: 0x06001EAB RID: 7851 RVA: 0x001EDF2C File Offset: 0x001ECF2C
	private void ᜂ(RowFormat.TablePositioning A_0, string A_1)
	{
		int a_ = 13;
		if (true)
		{
		}
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 4;
				continue;
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
				break;
			case 2:
				num = 6;
				continue;
			case 3:
				if (!(A_1 == ClipboardData.b("Ͳᑴၶᱸ", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_6B;
			case 4:
				goto IL_85;
			case 5:
				num = 3;
				continue;
			case 6:
				if (!(A_1 == ClipboardData.b("ṲᑴնṸቺ፼", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_AF;
			}
			if (A_1 == null)
			{
				goto IL_E5;
			}
			num = 5;
		}
		IL_6B:
		A_0.HorizRelationTo = HorizontalRelation.Page;
		return;
		IL_85:
		goto IL_E5;
		IL_AF:
		A_0.HorizRelationTo = HorizontalRelation.Margin;
		return;
		IL_E5:
		A_0.HorizRelationTo = HorizontalRelation.Column;
	}

	// Token: 0x06001EAC RID: 7852 RVA: 0x001EE028 File Offset: 0x001ED028
	private void ᜁ(RowFormat.TablePositioning A_0, string A_1)
	{
		int a_ = 7;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 1:
				num = 7;
				continue;
			case 2:
				if (!(A_1 == ClipboardData.b("๬੮ὰݲၴն", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_A2;
			case 4:
				num = 9;
				continue;
			case 5:
				num = 8;
				continue;
			case 6:
				if (!(A_1 == ClipboardData.b("ѬŮɰᩲᅴቶ", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_101;
			case 7:
				goto IL_67;
			case 8:
				if (!(A_1 == ClipboardData.b("ɬᩮհrᱴ፶ᱸ", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_57;
			case 9:
				if (!(A_1 == ClipboardData.b("Ὤٮᙰ᭲Ŵ", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_109;
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_67;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			}
			if (A_1 == null)
			{
				goto IL_173;
			}
			num = 10;
		}
		IL_57:
		A_0.HorizPositionAbs = HorizontalPosition.Outside;
		return;
		IL_67:
		if (true)
		{
		}
		goto IL_173;
		IL_A2:
		A_0.HorizPositionAbs = HorizontalPosition.Center;
		return;
		IL_101:
		A_0.HorizPositionAbs = HorizontalPosition.Inside;
		return;
		IL_109:
		A_0.HorizPositionAbs = HorizontalPosition.Right;
		return;
		IL_173:
		A_0.HorizPositionAbs = HorizontalPosition.Left;
	}

	// Token: 0x06001EAD RID: 7853 RVA: 0x001EE1B0 File Offset: 0x001ED1B0
	private void ᜀ(RowFormat.TablePositioning A_0, string A_1)
	{
		int a_ = 13;
		int num = 8;
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
					num = 7;
					continue;
				case 1:
					num = 2;
					continue;
				case 2:
					goto IL_8B;
				case 3:
					goto IL_179;
				case 4:
					if (!(A_1 == ClipboardData.b("ၲၴ᥶൸Ṻོ", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_102;
				case 5:
					if (!(A_1 == ClipboardData.b("ᩲ᭴Ѷၸὺ᡼", a_)))
					{
						if (true)
						{
						}
						num = 11;
						continue;
					}
					goto IL_17B;
				case 6:
					if (!(A_1 == ClipboardData.b("ᅲᩴͶ൸ᑺၼ", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_C1;
				case 7:
					if (!(A_1 == ClipboardData.b("ݲᩴݶ", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_10A;
				case 9:
					num = 4;
					continue;
				case 10:
					num = 5;
					continue;
				case 11:
					num = 12;
					continue;
				case 12:
					if (!(A_1 == ClipboardData.b("ᱲtͶ੸ቺ᥼᩾", a_)))
					{
						num = 9;
						continue;
					}
					goto IL_7B;
				}
				if (A_1 != null)
				{
					num = 10;
					continue;
				}
				goto IL_1B7;
			}
			IL_179:
			num = 6;
		}
		IL_7B:
		A_0.VertPositionAbs = VerticalPosition.Outside;
		return;
		IL_8B:
		goto IL_1B7;
		IL_C1:
		A_0.VertPositionAbs = VerticalPosition.Bottom;
		return;
		IL_102:
		A_0.VertPositionAbs = VerticalPosition.Center;
		return;
		IL_10A:
		A_0.VertPositionAbs = VerticalPosition.Top;
		return;
		IL_17B:
		A_0.VertPositionAbs = VerticalPosition.Inside;
		return;
		IL_1B7:
		A_0.VertPositionAbs = VerticalPosition.None;
	}

	// Token: 0x06001EAE RID: 7854 RVA: 0x001EE37C File Offset: 0x001ED37C
	private void ᜁ(XmlReader A_0, RowFormat A_1)
	{
		int a_ = 14;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ታήᑷᙹ", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
			string attribute2 = A_0.GetAttribute(ClipboardData.b("ɳ᝵ᑷ", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_12E;
				case 1:
					return;
				case 2:
					num = 6;
					continue;
				case 3:
					if (attribute2 != null)
					{
						num = 7;
						continue;
					}
					goto IL_12E;
				case 4:
					goto IL_F8;
				case 5:
					if (attribute != null)
					{
						goto IL_13C;
					}
					return;
				case 6:
					if (!(attribute == ClipboardData.b("ᕳ͵౷ᕹ", a_)))
					{
						A_1.BackColor = this.ᜃ(attribute);
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_13C;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 7:
					A_1.TextureStyle = this.ᜉ(attribute2);
					num = 0;
					continue;
				}
				break;
				IL_12E:
				num = 5;
				continue;
				IL_13C:
				num = 2;
			}
		}
		IL_F8:
		A_1.BackColor = Color.Empty;
	}

	// Token: 0x06001EAF RID: 7855 RVA: 0x001EE4D8 File Offset: 0x001ED4D8
	private void ᜀ(XmlReader A_0, RowFormat A_1)
	{
		int a_ = 12;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ٱ൳ٵᵷ", a_), ClipboardData.b("ᩱsɵࡷ䁹卻兽ﾋꂍﾏ쾟킡즣장\udca7\ud9a9芫솭슯햱鮳솵ힷ좹\ud8bb캽늿귁ꟃꏅ믇막ꗋꃍ럏뿑룓崙쿟迡藣迥蛧", a_));
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9B;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BE;
					default:
						goto IL_E1;
					}
					break;
				case 2:
					A_1.IsAutoResized = false;
					A_1.LayoutType = LayoutType.Fixed;
					num = 0;
					continue;
				case 3:
					if (true)
					{
					}
					if (attribute == ClipboardData.b("፱ųɵ᝷", a_))
					{
						num = 1;
						continue;
					}
					num = 4;
					continue;
				case 4:
					if (attribute == ClipboardData.b("ᑱᵳ๵ᵷṹ", a_))
					{
						goto IL_BE;
					}
					return;
				}
				break;
				IL_BE:
				num = 2;
			}
		}
		IL_9B:
		return;
		IL_E1:
		if (false)
		{
		}
		A_1.IsAutoResized = true;
		A_1.LayoutType = LayoutType.Auto;
	}

	// Token: 0x06001EB0 RID: 7856 RVA: 0x001EE5DC File Offset: 0x001ED5DC
	private void ᜀ(XmlReader A_0, Table A_1)
	{
		int a_ = 4;
		string attribute;
		for (;;)
		{
			attribute = A_0.GetAttribute(ClipboardData.b("ᱩ൫ɭ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫\ud9ad\udfaf삱킳욵쪷햹\udfbb\udbbd뎿뇁귃ꣅ꿇Ꟊꃋ럙뷛럝軟", a_));
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B9;
				case 1:
					if (this.ᜈ().ContainsKey(attribute))
					{
						num = 7;
						continue;
					}
					goto IL_98;
				case 2:
					goto IL_98;
				case 3:
					goto IL_BB;
				case 4:
					if (true)
					{
					}
					if (this.ᜄ.Styles.FindByName(this.ᜈ()[attribute], StyleType.TableStyle) != null)
					{
						num = 3;
						continue;
					}
					goto IL_98;
				case 5:
					if (!string.IsNullOrEmpty(attribute))
					{
						num = 6;
						continue;
					}
					goto IL_98;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BB;
					}
					if (false)
					{
					}
					num = 1;
					continue;
				case 7:
					num = 4;
					continue;
				case 8:
					if (this.ᜉ)
					{
						num = 0;
						continue;
					}
					goto IL_152;
				}
				break;
				IL_98:
				num = 8;
				continue;
				IL_BB:
				A_1.ᜀ(this.ᜈ()[attribute]);
				num = 2;
			}
		}
		IL_B9:
		A_1.TrackTblFormat.StyleName = attribute;
		return;
		IL_152:
		A_1.DocxTableFormat.StyleName = attribute;
	}

	// Token: 0x06001EB1 RID: 7857 RVA: 0x001EE748 File Offset: 0x001ED748
	private RowFormat ᜂ(IDocumentObject A_0)
	{
		RowFormat rowFormat;
		for (;;)
		{
			IL_40:
			rowFormat = null;
			Table table = null;
			for (;;)
			{
				IL_44:
				if (true)
				{
				}
				int num = 12;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return rowFormat;
					case 1:
						table = (A_0 as Table);
						num = 6;
						continue;
					case 2:
						if (!rowFormat.HasKey(107))
						{
							num = 9;
							continue;
						}
						return rowFormat;
					case 3:
						rowFormat = table.TrackTblFormat.Format;
						num = 0;
						continue;
					case 4:
						return rowFormat;
					case 5:
						if (A_0 is TableRow)
						{
							num = 11;
							continue;
						}
						return rowFormat;
					case 6:
						if (this.ᜉ)
						{
							num = 3;
							continue;
						}
						rowFormat = table.DocxTableFormat.Format;
						num = 2;
						continue;
					case 7:
						rowFormat = (A_0 as TableRow).TrackRowFormat;
						num = 8;
						continue;
					case 8:
						return rowFormat;
					case 9:
						rowFormat.RowIndent = float.MinValue;
						num = 4;
						continue;
					case 10:
						if (this.ᜇ)
						{
							num = 7;
							continue;
						}
						rowFormat = (A_0 as TableRow).RowFormat;
						num = 13;
						continue;
					case 11:
						table = (A_0 as TableRow).OwnerTable;
						num = 10;
						continue;
					case 12:
						if (A_0 is Table)
						{
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_44;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 13:
						return rowFormat;
					}
					goto IL_40;
				}
			}
		}
		return rowFormat;
	}

	// Token: 0x06001EB2 RID: 7858 RVA: 0x001EE8F4 File Offset: 0x001ED8F4
	private void ᜀ(XmlReader A_0, Table A_1, bool A_2)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				bool flag;
				List<float> list;
				List<float> list2;
				string localName2;
				switch (num)
				{
				case 0:
					goto IL_187;
				case 1:
					if (!A_2)
					{
						num = 23;
						continue;
					}
					num = 24;
					continue;
				case 2:
					num = 17;
					continue;
				case 3:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 28;
						continue;
					}
					return;
				case 4:
					goto IL_D4;
				case 5:
					num = 19;
					continue;
				case 7:
					goto IL_2C3;
				case 8:
					if (!flag)
					{
						num = 9;
						continue;
					}
					goto IL_203;
				case 9:
					A_0.Read();
					num = 10;
					continue;
				case 10:
					goto IL_203;
				case 11:
					goto IL_203;
				case 12:
					goto IL_260;
				case 13:
				{
					string attribute;
					if (!string.IsNullOrEmpty(attribute))
					{
						num = 20;
						continue;
					}
					goto IL_D4;
				}
				case 14:
					goto IL_182;
				case 15:
					list = A_1.TableGrid;
					goto IL_2C5;
				case 16:
					goto IL_187;
				case 17:
					goto IL_D4;
				case 18:
					if (!(A_0.LocalName == ClipboardData.b("ṩ๫ɭ㝯qᵳት㭷ቹᵻၽ", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_134;
				case 19:
				{
					string localName;
					if (!(localName == ClipboardData.b("൩ṫݭᑯㅱ᭳᩵", a_)))
					{
						num = 21;
						continue;
					}
					if (true)
					{
					}
					string attribute = A_0.GetAttribute(ClipboardData.b("ᵩ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫\ud9ad\udfaf삱킳욵쪷햹\udfbb\udbbd뎿뇁귃ꣅ꿇Ꟊꃋ럙뷛럝軟", a_));
					num = 13;
					continue;
				}
				case 20:
				{
					float num2 = list2[list2.Count - 1];
					string attribute;
					list2.Add(float.Parse(attribute) + num2);
					A_1._ColumnWidths.Add(float.Parse(attribute));
					num = 4;
					continue;
				}
				case 21:
					num = 29;
					continue;
				case 22:
					num = 18;
					continue;
				case 23:
					num = 15;
					continue;
				case 24:
					list = A_1.TrackTableGrid;
					goto IL_2C5;
				case 25:
					num = 27;
					continue;
				case 26:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 25;
						continue;
					}
					A_0.Read();
					num = 11;
					continue;
				case 27:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 5;
						continue;
					}
					goto IL_D4;
				}
				case 28:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_182;
					default:
						if (false)
						{
						}
						num = 30;
						continue;
					}
					break;
				case 29:
				{
					string localName;
					if (!(localName == ClipboardData.b("ṩ๫ɭ㝯qᵳት㭷ቹᵻၽ", a_)))
					{
						num = 2;
						continue;
					}
					A_1.TrackTableGrid.Add(0f);
					this.ᜀ(A_0, A_1, true);
					num = 14;
					continue;
				}
				case 30:
					if (!(A_0.LocalName != localName2))
					{
						num = 12;
						continue;
					}
					flag = false;
					num = 26;
					continue;
				}
				if (!(A_0.LocalName == ClipboardData.b("ṩ๫ɭ㝯qᵳት", a_)))
				{
					num = 22;
					continue;
				}
				goto IL_134;
				IL_D4:
				num = 8;
				continue;
				IL_182:
				goto IL_D4;
				IL_134:
				localName2 = A_0.LocalName;
				flag = false;
				num = 1;
				continue;
				IL_187:
				num = 3;
				continue;
				IL_203:
				this.ᜀ(A_0);
				num = 0;
				continue;
				IL_2C5:
				list2 = list;
				A_0.Read();
				this.ᜀ(A_0);
				num = 16;
			}
			IL_260:
			return;
			IL_2C3:
			throw new XmlException(ClipboardData.b("ṩ൫౭ᱯ᝱味ᅵ੷፹᡻", a_));
		}
		}
	}

	// Token: 0x06001EB3 RID: 7859 RVA: 0x001EED08 File Offset: 0x001EDD08
	private ITable ᜁ(IDocumentObject A_0)
	{
		ITable table;
		for (;;)
		{
			table = null;
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_13E;
				case 1:
					if (A_0 is spr\u1AE7)
					{
						num = 11;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CF;
					default:
						if (false)
						{
						}
						table = this.ᜄ.LastSection.AddTable();
						num = 4;
						continue;
					}
					break;
				case 2:
					goto IL_13E;
				case 3:
					if (this.\u1712() != null)
					{
						num = 5;
						continue;
					}
					return table;
				case 4:
					goto IL_13E;
				case 5:
					if (true)
					{
					}
					num = 17;
					continue;
				case 6:
					if (A_0 is HeaderFooter)
					{
						num = 13;
						continue;
					}
					goto IL_CF;
				case 7:
					return table;
				case 8:
					this.\u1712().IsFieldRange = true;
					this.ᜀ(table as Table);
					num = 7;
					continue;
				case 9:
					table = (A_0 as Comment).Body.AddTable();
					num = 2;
					continue;
				case 10:
					if (A_0 is Comment)
					{
						num = 9;
						continue;
					}
					num = 1;
					continue;
				case 11:
					table = (A_0 as spr\u1AE7).ᜆ().ᜂ().AddTable();
					num = 14;
					continue;
				case 12:
					table = (A_0 as Footnote).TextBody.AddTable();
					num = 0;
					continue;
				case 13:
					table = (A_0 as HeaderFooter).AddTable();
					num = 15;
					continue;
				case 14:
					goto IL_13E;
				case 15:
					goto IL_13E;
				case 16:
					if (A_0 is Footnote)
					{
						num = 12;
						continue;
					}
					num = 10;
					continue;
				case 17:
					if (!this.\u1712().IsFieldRange)
					{
						num = 8;
						continue;
					}
					return table;
				}
				break;
				IL_CF:
				num = 16;
				continue;
				IL_13E:
				num = 3;
			}
		}
		return table;
	}

	// Token: 0x06001EB4 RID: 7860 RVA: 0x001EEF34 File Offset: 0x001EDF34
	private void ᜀ(IDocumentObject A_0, IDocumentObject A_1)
	{
		int num = 26;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 20;
				continue;
			case 1:
				if (this.\u1712() != null)
				{
					num = 8;
					continue;
				}
				return;
			case 2:
				(A_0 as Comment).Body.Items.Remove(A_1);
				num = 29;
				continue;
			case 3:
				num = 25;
				continue;
			case 4:
				return;
			case 5:
				if (A_0 is spr\u1AE7)
				{
					num = 3;
					continue;
				}
				goto IL_A6;
			case 6:
				num = 10;
				continue;
			case 7:
				this.\u1712().Range.ᜁ().Remove(A_1);
				num = 4;
				continue;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1E4;
				default:
					if (false)
					{
					}
					num = 27;
					continue;
				}
				break;
			case 9:
				if (this.\u1712().Range.ᜁ().Contains(A_1))
				{
					num = 7;
					continue;
				}
				return;
			case 10:
				if ((A_0 as HeaderFooter).Items.Contains(A_1))
				{
					num = 17;
					continue;
				}
				goto IL_307;
			case 11:
				goto IL_260;
			case 12:
				goto IL_260;
			case 13:
				goto IL_260;
			case 14:
				(A_0 as spr\u1AE7).ᜆ().ᜂ().Items.Remove(A_1);
				num = 15;
				continue;
			case 15:
				goto IL_260;
			case 16:
				if (true)
				{
				}
				this.ᜄ.LastSection.Body.Items.Remove(A_1);
				num = 11;
				continue;
			case 17:
				(A_0 as HeaderFooter).Items.Remove(A_1);
				num = 13;
				continue;
			case 18:
				if ((A_0 as Comment).Body.Items.Contains(A_1))
				{
					num = 2;
					continue;
				}
				goto IL_35D;
			case 19:
				if (A_0 is Footnote)
				{
					num = 0;
					continue;
				}
				goto IL_2AC;
			case 20:
				if ((A_0 as Footnote).TextBody.Items.Contains(A_1))
				{
					num = 22;
					continue;
				}
				goto IL_2AC;
			case 21:
				num = 18;
				continue;
			case 22:
				(A_0 as Footnote).TextBody.Items.Remove(A_1);
				num = 12;
				continue;
			case 23:
				if (this.ᜄ.LastSection.Body.Items.Contains(A_1))
				{
					num = 16;
					continue;
				}
				goto IL_260;
			case 24:
				goto IL_1E4;
			case 25:
				if ((A_0 as spr\u1AE7).ᜆ().ᜂ().Items.Contains(A_1))
				{
					num = 14;
					continue;
				}
				goto IL_A6;
			case 27:
				if (this.\u1712().IsFieldRange)
				{
					num = 24;
					continue;
				}
				return;
			case 28:
				if (A_0 is Comment)
				{
					num = 21;
					continue;
				}
				goto IL_35D;
			case 29:
				goto IL_260;
			}
			if (A_0 is HeaderFooter)
			{
				num = 6;
				continue;
			}
			goto IL_307;
			IL_A6:
			num = 23;
			continue;
			IL_1E4:
			num = 9;
			continue;
			IL_260:
			num = 1;
			continue;
			IL_2AC:
			num = 28;
			continue;
			IL_307:
			num = 19;
			continue;
			IL_35D:
			num = 5;
		}
	}

	// Token: 0x06001EB5 RID: 7861 RVA: 0x001EF2F0 File Offset: 0x001EE2F0
	private void ᜀ(DocumentObject A_0)
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
		this.\u1712().Range.ᜁ().Add(A_0);
	}

	// Token: 0x06001EB6 RID: 7862 RVA: 0x001EF344 File Offset: 0x001EE344
	private void \u170D(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("ᵳት", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵ힷ\udcb9\udabbힽꎿꟁ胃꧅ꯇ뿉ꇋꯍ뻏ꛑﯓ鋟蟡裣蟥鳧菩菫胭華髱鷳蛵请", a_));
				string attribute2 = A_0.GetAttribute(ClipboardData.b("ᕳᡵ᭷ቹ፻౽", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
				int num = 0;
				for (;;)
				{
					Hyperlink hyperlink;
					Field field;
					switch (num)
					{
					case 0:
						if (attribute == null)
						{
							num = 11;
							continue;
						}
						goto IL_14C;
					case 1:
					{
						bool flag = this.ᜌ()[attribute];
						num = 5;
						continue;
					}
					case 2:
						num = 12;
						continue;
					case 3:
					{
						DictionaryEntry dictionaryEntry;
						if (dictionaryEntry.Key != null)
						{
							num = 2;
							continue;
						}
						return;
					}
					case 4:
						goto IL_27C;
					case 5:
					{
						bool flag;
						if (!flag)
						{
							num = 18;
							continue;
						}
						goto IL_27C;
					}
					case 6:
						hyperlink.Type = HyperlinkType.Bookmark;
						hyperlink.BookmarkName = attribute2;
						this.ᜀ(field, A_1);
						num = 10;
						continue;
					case 7:
						goto IL_373;
					case 8:
						if (this.ᜎ.ContainsKey(attribute))
						{
							num = 1;
							continue;
						}
						goto IL_27C;
					case 9:
					{
						string text;
						if (text.StartsWith(ClipboardData.b("坳", a_)))
						{
							goto IL_3FA;
						}
						field.m_fieldValue = ClipboardData.b("噳", a_) + text.Replace(ClipboardData.b("⡳", a_), ClipboardData.b("⡳⩵", a_)) + ClipboardData.b("噳", a_);
						num = 19;
						continue;
					}
					case 10:
						goto IL_27C;
					case 11:
						num = 16;
						continue;
					case 12:
					{
						DictionaryEntry dictionaryEntry;
						if (dictionaryEntry.Key.ToString() != ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵ힷ\udcb9\udabbힽꎿꟁ胃꧅ꯇ뿉ꇋꯍ뻏ꛑﯓ鋟蟡裣蟥鳧菩菫胭華髱鷳蛵请헹铻蟽烿朁瘃樅愇搉朋", a_))
						{
							num = 15;
							continue;
						}
						this.ᜀ(field, A_1);
						string text = (string)dictionaryEntry.Value;
						num = 9;
						continue;
					}
					case 13:
					{
						hyperlink.Type = HyperlinkType.Bookmark;
						string text;
						hyperlink.BookmarkName = text.Replace(ClipboardData.b("坳", a_), string.Empty);
						num = 23;
						continue;
					}
					case 14:
					{
						if (attribute == null)
						{
							num = 6;
							continue;
						}
						DictionaryEntry dictionaryEntry = this.ᜬ(attribute);
						num = 3;
						continue;
					}
					case 15:
						goto IL_3B9;
					case 16:
						if (attribute2 != null)
						{
							goto IL_14C;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3FA;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					case 17:
						if (A_0.LocalName.ToLower() == ClipboardData.b("ᱳཱུࡷό๻ች", a_))
						{
							num = 20;
							continue;
						}
						return;
					case 18:
						field.IsLocal = true;
						num = 4;
						continue;
					case 19:
						if (this.ᜏ != null)
						{
							num = 22;
							continue;
						}
						goto IL_27C;
					case 20:
					{
						this.ᜌ(A_0, A_1);
						FieldMark a_2 = new FieldMark(this.ᜄ, FieldMarkType.FieldEnd);
						this.ᜀ(a_2, A_1);
						field.End = a_2;
						num = 21;
						continue;
					}
					case 21:
						goto IL_24F;
					case 22:
						num = 8;
						continue;
					case 23:
						goto IL_27C;
					}
					break;
					IL_14C:
					field = new Field(this.ᜄ);
					field.Type = FieldType.FieldHyperlink;
					hyperlink = new Hyperlink(field);
					num = 14;
					continue;
					IL_27C:
					FieldMark a_3 = new FieldMark(this.ᜄ, FieldMarkType.FieldSeparator);
					this.ᜀ(a_3, A_1);
					field.Separator = a_3;
					num = 17;
					continue;
					IL_3FA:
					num = 13;
				}
			}
			IL_24F:
			return;
			IL_373:
			if (true)
			{
			}
			return;
			IL_3B9:
			return;
		}
	}

	// Token: 0x06001EB7 RID: 7863 RVA: 0x001EF75C File Offset: 0x001EE75C
	private DictionaryEntry ᜬ(string A_0)
	{
		int a_ = 3;
		int num = 2;
		DictionaryEntry result;
		for (;;)
		{
			Dictionary<string, DictionaryEntry> dictionary;
			string a_2;
			string text;
			switch (num)
			{
			case 0:
				goto IL_295;
			case 1:
				num = 27;
				continue;
			case 3:
				if (dictionary.ContainsKey(A_0))
				{
					num = 26;
					continue;
				}
				return result;
			case 4:
				num = 3;
				continue;
			case 5:
				goto IL_17F;
			case 6:
				if (true)
				{
				}
				if (this.ᜋ.StartsWith(ClipboardData.b("ཨѪɬ᭮ᑰŲ", a_)))
				{
					num = 5;
					continue;
				}
				num = 14;
				continue;
			case 7:
				if (this.ᜎ.ContainsKey(A_0))
				{
					goto IL_208;
				}
				return result;
			case 8:
				num = 6;
				continue;
			case 9:
				if (!this.ᜋ.StartsWith(ClipboardData.b("ཨѪɬ᭮ᑰŲ", a_)))
				{
					num = 23;
					continue;
				}
				goto IL_259;
			case 10:
				a_2 = ClipboardData.b("੨ѪlɮᑰᵲŴѶ坸ͺၼ፾꾀愈", a_);
				num = 21;
				continue;
			case 11:
				if (!this.ᜋ.StartsWith(ClipboardData.b("੨ѪlɮᑰᵲŴѶ", a_)))
				{
					num = 19;
					continue;
				}
				goto IL_259;
			case 12:
				if (!this.ᜋ.StartsWith(ClipboardData.b("Ũ๪౬୮ᑰŲ", a_)))
				{
					num = 8;
					continue;
				}
				goto IL_17F;
			case 13:
				if (this.\u1717)
				{
					num = 15;
					continue;
				}
				num = 7;
				continue;
			case 14:
				if (this.ᜋ.StartsWith(ClipboardData.b("੨ѪlɮᑰᵲŴѶ", a_)))
				{
					num = 10;
					continue;
				}
				num = 17;
				continue;
			case 15:
				goto IL_259;
			case 16:
				text = ClipboardData.b("ཨѪɬ᭮ὰᱲŴቶ੸啺ռቾ궂", a_);
				goto IL_1BF;
			case 17:
				if (!this.\u1717)
				{
					num = 1;
					continue;
				}
				num = 16;
				continue;
			case 18:
				return result;
			case 19:
				num = 13;
				continue;
			case 20:
				num = 9;
				continue;
			case 21:
				goto IL_295;
			case 22:
				if (dictionary != null)
				{
					num = 4;
					continue;
				}
				return result;
			case 23:
				num = 11;
				continue;
			case 24:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_208;
				default:
					if (false)
					{
					}
					goto IL_295;
				}
				break;
			case 25:
				return result;
			case 26:
				result = dictionary[A_0];
				num = 25;
				continue;
			case 27:
				text = ClipboardData.b("౨ժ६ŮṰݲၴѶ坸ͺၼ፾꾀愈", a_);
				goto IL_1BF;
			case 28:
				result = this.ᜎ[A_0];
				num = 18;
				continue;
			}
			if (!this.ᜋ.StartsWith(ClipboardData.b("Ũ๪౬୮ᑰŲ", a_)))
			{
				num = 20;
				continue;
			}
			goto IL_259;
			IL_17F:
			a_2 = this.ᜋ;
			num = 0;
			continue;
			IL_1BF:
			a_2 = text;
			num = 24;
			continue;
			IL_208:
			num = 28;
			continue;
			IL_259:
			a_2 = string.Empty;
			num = 12;
			continue;
			IL_295:
			dictionary = this.ᜅ(a_2);
			num = 22;
		}
		return result;
	}

	// Token: 0x06001EB8 RID: 7864 RVA: 0x001EFAE8 File Offset: 0x001EEAE8
	private void ᜌ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 12;
		for (;;)
		{
			A_0.MoveToElement();
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!(A_0.LocalName != ClipboardData.b("ᩱ൳ٵᵷࡹၻ᝽", a_)))
					{
						num = 1;
						continue;
					}
					this.ᜈ(A_0, A_1);
					A_0.Read();
					this.ᜀ(A_0);
					num = 6;
					continue;
				case 1:
					goto IL_FA;
				case 2:
					goto IL_66;
				case 3:
					goto IL_C6;
				case 4:
					if (A_0.LocalName != ClipboardData.b("ᩱ൳ٵᵷࡹၻ᝽", a_))
					{
						num = 2;
						continue;
					}
					num = 7;
					continue;
				case 5:
					return;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B2;
					default:
						if (false)
						{
						}
						goto IL_C6;
					}
					break;
				case 7:
					if (true)
					{
					}
					if (A_0.IsEmptyElement)
					{
						num = 5;
						continue;
					}
					A_0.Read();
					this.ᜀ(A_0);
					num = 3;
					continue;
				}
				break;
				IL_C6:
				num = 0;
			}
		}
		IL_66:
		IL_B2:
		throw new XmlException(ClipboardData.b("ᩱ൳ٵᵷࡹၻ᝽", a_));
		IL_FA:
		this.\u1716 = null;
	}

	// Token: 0x06001EB9 RID: 7865 RVA: 0x001EFC3C File Offset: 0x001EEC3C
	private bool ᜋ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			Field field;
			FieldMark a_2;
			Paragraph paragraph;
			for (;;)
			{
				string text = A_0.GetAttribute(ClipboardData.b("ŧѩὫᩭɯ", a_), ClipboardData.b("gṩᡫṭ䩯嵱孳յ᭷ቹ᥻፽ꪃﶏﺑ秊ﶛ펟財쮣풥쾧薩\udbab솭슯횱쒳쒵ힷ\ud9b9\ud9bb춽뎿ꯁ꫃ꇅꗇꛉﳍ崙뗗믙뗛냝", a_));
				int num = 15;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_D3;
					case 1:
						field.ApplyCharacterFormat(this.\u1716);
						num = 21;
						continue;
					case 2:
						if (this.\u1716 != null)
						{
							num = 1;
							continue;
						}
						goto IL_138;
					case 3:
						goto IL_109;
					case 4:
						goto IL_3EF;
					case 5:
						if (field.Type == FieldType.FieldNext)
						{
							num = 6;
							continue;
						}
						a_2 = new FieldMark(this.ᜄ, FieldMarkType.FieldSeparator);
						this.ᜁ(a_2, A_1);
						field.Separator = a_2;
						A_0.Read();
						num = 17;
						continue;
					case 6:
						paragraph = new Paragraph(this.ᜄ);
						this.\u1713().Push(field);
						this.\u171B = FieldCharType.SimpleField;
						A_0.Read();
						num = 13;
						continue;
					case 7:
						goto IL_3EF;
					case 8:
						goto IL_1DF;
					case 9:
						if (A_0.IsEmptyElement)
						{
							num = 18;
							continue;
						}
						num = 22;
						continue;
					case 10:
						this.\u1713().Push(field);
						this.\u171B = FieldCharType.SimpleField;
						A_0.Read();
						num = 4;
						continue;
					case 11:
						goto IL_29A;
					case 12:
						num = 24;
						continue;
					case 13:
						goto IL_D3;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_42A;
						default:
							if (false)
							{
							}
							text = this.\u1716(A_0.ReadInnerXml());
							num = 25;
							continue;
						}
						break;
					case 15:
						if (string.IsNullOrEmpty(text))
						{
							num = 14;
							continue;
						}
						goto IL_32B;
					case 16:
						if (!(A_0.LocalName != ClipboardData.b("๧٩࡫㵭᥯άѳ᩵ᵷ", a_)))
						{
							num = 3;
							continue;
						}
						this.ᜈ(A_0, paragraph.Items);
						A_0.Read();
						this.ᜀ(A_0);
						num = 0;
						continue;
					case 17:
						goto IL_1DF;
					case 18:
						goto IL_163;
					case 19:
						if (!(A_0.LocalName != ClipboardData.b("๧٩࡫㵭᥯άѳ᩵ᵷ", a_)))
						{
							num = 20;
							continue;
						}
						this.ᜈ(A_0, A_1);
						A_0.Read();
						this.ᜀ(A_0);
						num = 7;
						continue;
					case 20:
						goto IL_425;
					case 21:
						goto IL_138;
					case 22:
						if (field.Type == FieldType.FieldMergeField)
						{
							num = 10;
							continue;
						}
						num = 5;
						continue;
					case 23:
						if (A_0.LocalName != ClipboardData.b("๧٩࡫㵭᥯άѳ᩵ᵷ", a_))
						{
							num = 12;
							continue;
						}
						goto IL_42A;
					case 24:
						if (!(A_0.LocalName != ClipboardData.b("ᡧ", a_)))
						{
							num = 11;
							continue;
						}
						this.ᜈ(A_0, A_1);
						A_0.Read();
						this.ᜀ(A_0);
						num = 8;
						continue;
					case 25:
						goto IL_32B;
					}
					break;
					IL_D3:
					num = 16;
					continue;
					IL_138:
					this.ᜁ(field, A_1);
					num = 9;
					continue;
					IL_1DF:
					num = 23;
					continue;
					IL_32B:
					field = this.ᜫ(text);
					field.ParseFieldCode(field.Code);
					num = 2;
					continue;
					IL_3EF:
					num = 19;
				}
			}
			IL_109:
			field.m_formattingString = paragraph.Text;
			field.Text = paragraph.Text;
			this.\u1713().Pop();
			this.\u171B = FieldCharType.Unknown;
			return true;
			IL_163:
			if (true)
			{
			}
			return false;
			IL_29A:
			goto IL_42A;
			IL_425:
			this.\u1713().Pop();
			this.\u171B = FieldCharType.Unknown;
			return true;
			IL_42A:
			a_2 = new FieldMark(this.ᜄ, FieldMarkType.FieldEnd);
			this.ᜁ(a_2, A_1);
			field.End = a_2;
			return true;
		}
		}
	}

	// Token: 0x06001EBA RID: 7866 RVA: 0x001F0090 File Offset: 0x001EF090
	private void ᜊ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 13;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ᩲᅴ", a_), ClipboardData.b("᭲ŴͶॸ䅺剼偾ﺌꆎﺐ練咽캠톢좤욦\udda8\ud8aa莬삮쎰풲骴삶횸즺\ud9bc쾾돀곂ꛄꋆ뫈룊꓌ꇎ뛐뻒맔컠転蓤軦蟨", a_));
			int num = 1;
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
						return;
					default:
						if (false)
						{
						}
						if (this.ᜋ().ContainsKey(attribute))
						{
							num = 2;
							continue;
						}
						return;
					}
					break;
				case 2:
				{
					BookmarkEnd entity = new BookmarkEnd(this.ᜄ, this.ᜋ()[attribute]);
					A_1.Add(entity);
					if (true)
					{
					}
					num = 0;
					continue;
				}
				}
				break;
			}
		}
	}

	// Token: 0x06001EBB RID: 7867 RVA: 0x001F0158 File Offset: 0x001EF158
	private void ᜌ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 15;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ᱴ፶", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_));
			int num = 21;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 is Table)
					{
						num = 20;
						continue;
					}
					goto IL_17B;
				case 1:
					goto IL_20A;
				case 2:
				{
					Paragraph paragraph = A_1 as Paragraph;
					num = 22;
					continue;
				}
				case 3:
				{
					Paragraph paragraph = this.ᜀ(A_1 as TableCell);
					num = 23;
					continue;
				}
				case 4:
					if (A_1 == null)
					{
						num = 8;
						continue;
					}
					num = 0;
					continue;
				case 5:
					if (A_1 is TableRow)
					{
						num = 9;
						continue;
					}
					goto IL_20A;
				case 6:
					goto IL_20A;
				case 7:
				{
					TableRow lastRow = (A_1 as Table).LastRow;
					num = 15;
					continue;
				}
				case 8:
				{
					Paragraph paragraph = this.ᜄ.LastParagraph;
					num = 12;
					continue;
				}
				case 9:
				{
					Paragraph paragraph = this.ᜀ(A_1 as TableRow);
					num = 1;
					continue;
				}
				case 10:
					goto IL_20A;
				case 11:
				{
					Paragraph paragraph;
					paragraph.AppendBookmarkEnd(this.ᜋ()[attribute]);
					num = 18;
					continue;
				}
				case 12:
					goto IL_20A;
				case 13:
					if (A_1 is TableCell)
					{
						num = 3;
						continue;
					}
					num = 5;
					continue;
				case 14:
					if ((A_1 as Table).OwnerTextBody != null)
					{
						num = 7;
						continue;
					}
					goto IL_17B;
				case 15:
				{
					TableRow lastRow;
					if (lastRow != null)
					{
						num = 24;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A3;
					default:
					{
						if (false)
						{
						}
						Paragraph paragraph = (A_1 as Table).OwnerTextBody.AddParagraph();
						num = 6;
						continue;
					}
					}
					break;
				}
				case 16:
					if (A_1 is Paragraph)
					{
						num = 2;
						continue;
					}
					num = 4;
					continue;
				case 17:
				{
					Paragraph paragraph = null;
					num = 16;
					continue;
				}
				case 18:
					return;
				case 19:
				{
					Paragraph paragraph;
					if (paragraph != null)
					{
						num = 11;
						continue;
					}
					return;
				}
				case 20:
					num = 14;
					continue;
				case 21:
					goto IL_A3;
				case 22:
					goto IL_20A;
				case 23:
					goto IL_20A;
				case 24:
				{
					TableRow lastRow;
					Paragraph paragraph = this.ᜀ(lastRow);
					num = 10;
					continue;
				}
				}
				break;
				IL_A3:
				if (true)
				{
				}
				if (this.ᜋ().ContainsKey(attribute))
				{
					num = 17;
					continue;
				}
				return;
				IL_17B:
				num = 13;
				continue;
				IL_20A:
				num = 19;
			}
		}
	}

	// Token: 0x06001EBC RID: 7868 RVA: 0x001F044C File Offset: 0x001EF44C
	private Paragraph ᜀ(TableCell A_0)
	{
		Paragraph paragraph;
		for (;;)
		{
			paragraph = null;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_44;
				case 1:
					if (A_0.Items != null)
					{
						num = 2;
						continue;
					}
					goto IL_44;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_44;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 3:
					return paragraph;
				case 4:
					paragraph = (A_0.Items[A_0.Items.Count - 1] as Paragraph);
					num = 0;
					continue;
				case 5:
					paragraph = A_0.AddParagraph();
					num = 3;
					continue;
				case 6:
					if (true)
					{
					}
					if (paragraph == null)
					{
						num = 5;
						continue;
					}
					return paragraph;
				case 7:
					if (A_0.Items.Count > 0)
					{
						num = 4;
						continue;
					}
					goto IL_44;
				}
				break;
				IL_44:
				num = 6;
			}
		}
		return paragraph;
	}

	// Token: 0x06001EBD RID: 7869 RVA: 0x001F054C File Offset: 0x001EF54C
	private Paragraph ᜀ(TableRow A_0)
	{
		Paragraph result;
		for (;;)
		{
			result = null;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					result = this.ᜀ(A_0.Cells[A_0.Cells.Count - 1]);
					num = 1;
					continue;
				case 1:
					return result;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return result;
					default:
						if (false)
						{
						}
						if (A_0.Cells.Count > 0)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						return result;
					}
					break;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x06001EBE RID: 7870 RVA: 0x001F05EC File Offset: 0x001EF5EC
	private void ᜉ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			BookmarkStart bookmarkStart;
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("mᅯάᅳ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_));
				string attribute2 = A_0.GetAttribute(ClipboardData.b("ݭᑯ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_));
				bookmarkStart = new BookmarkStart(this.ᜄ, attribute);
				string attribute3 = A_0.GetAttribute(ClipboardData.b("൭Ὧṱ㉳ή੷ॹࡻ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_));
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_1F3:
					num = 3;
					break;
				default:
					if (false)
					{
					}
					num = 11;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_1 == null)
						{
							num = 2;
							continue;
						}
						goto IL_21E;
					case 1:
						goto IL_1AF;
					case 2:
						goto IL_1D4;
					case 3:
						this.ᜋ().Add(attribute2, attribute);
						if (true)
						{
						}
						num = 7;
						continue;
					case 4:
						goto IL_148;
					case 5:
						bookmarkStart.ColumnLast = int.Parse(attribute3);
						num = 6;
						continue;
					case 6:
						goto IL_1D6;
					case 7:
						goto IL_1AF;
					case 8:
						bookmarkStart.ColumnFirst = int.Parse(attribute3);
						num = 4;
						continue;
					case 9:
						if (!this.ᜋ().ContainsKey(attribute2))
						{
							goto IL_1F3;
						}
						this.ᜋ()[attribute2] = attribute;
						num = 1;
						continue;
					case 10:
						if (!string.IsNullOrEmpty(attribute3))
						{
							num = 5;
							continue;
						}
						goto IL_1D6;
					case 11:
						if (!string.IsNullOrEmpty(attribute3))
						{
							num = 8;
							continue;
						}
						goto IL_148;
					}
					break;
					IL_148:
					attribute3 = A_0.GetAttribute(ClipboardData.b("൭Ὧṱ㡳᝵୷๹", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_));
					num = 10;
					continue;
					IL_1AF:
					num = 0;
					continue;
					IL_1D6:
					num = 9;
				}
			}
			IL_1D4:
			this.\u171F = bookmarkStart;
			return;
			IL_21E:
			A_1.Add(bookmarkStart);
			return;
		}
		}
	}

	// Token: 0x06001EBF RID: 7871 RVA: 0x001F0820 File Offset: 0x001EF820
	private void ᜈ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 13;
			for (;;)
			{
				ParagraphBase paragraphBase;
				bool flag3;
				MemoryStream a_3;
				CharacterFormat format;
				XmlReader xmlReader;
				switch (num)
				{
				case 0:
				{
					Paragraph paragraph;
					this.\u1716.ApplyBase(paragraph.ParaStyle.CharacterFormat);
					num = 32;
					continue;
				}
				case 1:
				{
					Paragraph paragraph = A_1.Owner.Owner.Owner as Paragraph;
					num = 84;
					continue;
				}
				case 2:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 12;
						continue;
					}
					A_0.Read();
					num = 40;
					continue;
				case 3:
					goto IL_4CB;
				case 4:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 19;
						continue;
					}
					goto IL_72B;
				case 5:
					goto IL_4CB;
				case 6:
				{
					Paragraph paragraph;
					if (paragraph != null)
					{
						num = 71;
						continue;
					}
					goto IL_5C6;
				}
				case 7:
				{
					paragraphBase = new TextRange(this.ᜄ);
					string a_2 = '\t'.ToString() + '\t'.ToString();
					this.ᜀ(paragraphBase as TextRange, a_2, this.\u1716);
					num = 21;
					continue;
				}
				case 8:
					num = 30;
					continue;
				case 9:
					if (A_0.LocalName == ClipboardData.b("ɯ", a_))
					{
						num = 49;
						continue;
					}
					goto IL_ACA;
				case 10:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 53;
						continue;
					}
					goto IL_4CB;
				}
				case 11:
					goto IL_4CB;
				case 12:
					num = 20;
					continue;
				case 14:
					return;
				case 15:
					if (A_1.Owner is spr\u1AD2)
					{
						num = 1;
						continue;
					}
					goto IL_37A;
				case 16:
				{
					Paragraph paragraph;
					if (paragraph.ParaStyle != null)
					{
						num = 0;
						continue;
					}
					goto IL_5C6;
				}
				case 17:
					goto IL_4CB;
				case 18:
					goto IL_4CB;
				case 19:
				{
					bool flag = false;
					bool flag2 = false;
					A_0.Read();
					num = 28;
					continue;
				}
				case 20:
				{
					if (A_0.IsEmptyElement)
					{
						num = 14;
						continue;
					}
					flag3 = false;
					bool flag = false;
					bool flag2 = false;
					A_0.Read();
					a_3 = null;
					this.ᜀ(A_0);
					num = 22;
					continue;
				}
				case 21:
					goto IL_4CB;
				case 22:
					goto IL_DB7;
				case 23:
					goto IL_4CB;
				case 24:
					goto IL_3AF;
				case 25:
					if (A_0.IsStartElement())
					{
						num = 79;
						continue;
					}
					goto IL_ACA;
				case 26:
					goto IL_4CB;
				case 27:
					goto IL_4CB;
				case 28:
					goto IL_ACA;
				case 29:
					goto IL_4CB;
				case 30:
				{
					int num2;
					switch (num2)
					{
					case 0:
					{
						Paragraph paragraph = A_1.OwnerBase as Paragraph;
						num = 15;
						continue;
					}
					case 1:
					case 2:
						paragraphBase = this.ᜁ(A_0, A_1);
						num = 42;
						continue;
					case 3:
						num = 68;
						continue;
					case 4:
						paragraphBase = this.\u171A(A_0);
						flag3 = true;
						num = 46;
						continue;
					case 5:
						paragraphBase = this.ᜀ(A_0, A_1, a_3);
						flag3 = true;
						a_3 = null;
						num = 29;
						continue;
					case 6:
					case 7:
						this.ᜃ(A_0, A_1);
						num = 17;
						continue;
					case 8:
						this.ᜆ(A_0, A_1);
						flag3 = true;
						num = 85;
						continue;
					case 9:
					case 10:
						this.ᜇ(A_0, A_1);
						this.\u171A = false;
						num = 18;
						continue;
					case 11:
					case 12:
						format = null;
						num = 33;
						continue;
					case 13:
					case 14:
						paragraphBase = this.ᜃ();
						num = 36;
						continue;
					case 15:
					{
						A_0.GetAttribute(ClipboardData.b("ɯ᝱ᡳ᝵౷፹੻᭽푿", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱쎳\ud9b5쪷\udeb9첻첽꾿ꇁꇃ뗅믇ꏉꋋ꧍뷏뻑ﯓ跟菡跣裥", a_));
						string attribute = A_0.GetAttribute(ClipboardData.b("ᅯṱᵳᅵᙷ᝹᥻ၽ", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱쎳\ud9b5쪷\udeb9첻첽꾿ꇁꇃ뗅믇ꏉꋋ꧍뷏뻑ﯓ跟菡跣裥", a_));
						A_0.GetAttribute(ClipboardData.b("ᱯ᝱ᕳትᵷࡹ", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱쎳\ud9b5쪷\udeb9첻첽꾿ꇁꇃ뗅믇ꏉꋋ꧍뷏뻑ﯓ跟菡跣裥", a_));
						num = 45;
						continue;
					}
					case 16:
					{
						paragraphBase = new TextRange(this.ᜄ);
						string a_4 = '\t'.ToString();
						this.ᜀ(paragraphBase as TextRange, a_4, this.\u1716);
						num = 62;
						continue;
					}
					case 17:
						paragraphBase = this.ᜂ(A_0, A_1);
						num = 52;
						continue;
					case 18:
						paragraphBase = new TextRange(this.ᜄ);
						this.ᜀ(paragraphBase as TextRange, '\u001e'.ToString(), this.\u1716);
						num = 47;
						continue;
					case 19:
						paragraphBase = new TextRange(this.ᜄ);
						this.ᜀ(paragraphBase as TextRange, '\u001f'.ToString(), this.\u1716);
						num = 11;
						continue;
					case 20:
					{
						if (true)
						{
						}
						string attribute2 = A_0.GetAttribute(ClipboardData.b("᥯ᙱ", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱쎳\ud9b5쪷\udeb9첻첽꾿ꇁꇃ뗅믇ꏉꋋ꧍뷏뻑ﯓ跟菡跣裥", a_));
						num = 31;
						continue;
					}
					case 21:
					{
						bool flag = true;
						num = 3;
						continue;
					}
					case 22:
						num = 67;
						continue;
					case 23:
						this.ᜀ(A_0, A_1);
						num = 61;
						continue;
					default:
						num = 34;
						continue;
					}
					break;
				}
				case 31:
				{
					string attribute2;
					if (this.\u1716().ContainsKey(attribute2))
					{
						num = 73;
						continue;
					}
					goto IL_4CB;
				}
				case 32:
					goto IL_5C6;
				case 33:
					if (this.\u1716 != null)
					{
						num = 38;
						continue;
					}
					goto IL_D1B;
				case 34:
					num = 23;
					continue;
				case 35:
					if (!(A_0.LocalName != ClipboardData.b("ɯ", a_)))
					{
						num = 65;
						continue;
					}
					paragraphBase = null;
					flag3 = false;
					num = 69;
					continue;
				case 36:
					goto IL_4CB;
				case 37:
					this.ᜀ(paragraphBase, A_1);
					this.ᜀ(paragraphBase);
					num = 24;
					continue;
				case 38:
					format = this.\u1716;
					num = 66;
					continue;
				case 39:
					if (xmlReader.LocalName == ClipboardData.b("ѯ", a_))
					{
						num = 56;
						continue;
					}
					goto IL_ACA;
				case 41:
					goto IL_ACA;
				case 42:
					goto IL_4CB;
				case 43:
					goto IL_4CB;
				case 44:
					goto IL_50A;
				case 45:
				{
					string attribute;
					if (attribute.ToLower() == ClipboardData.b("፯᝱ᩳɵᵷࡹ", a_))
					{
						num = 74;
						continue;
					}
					num = 70;
					continue;
				}
				case 46:
					goto IL_4CB;
				case 47:
					goto IL_4CB;
				case 48:
				{
					bool flag2 = true;
					num = 5;
					continue;
				}
				case 49:
					num = 25;
					continue;
				case 50:
					this.ᜀ(paragraphBase, A_1);
					this.ᜀ(paragraphBase);
					num = 57;
					continue;
				case 51:
					num = 10;
					continue;
				case 52:
					goto IL_4CB;
				case 53:
					num = 81;
					continue;
				case 54:
					num = 76;
					continue;
				case 55:
					goto IL_4CB;
				case 56:
					num = 72;
					continue;
				case 57:
					goto IL_DB7;
				case 58:
					goto IL_ACA;
				case 59:
					goto IL_4CB;
				case 60:
					A_0.Read();
					num = 9;
					continue;
				case 61:
					goto IL_4CB;
				case 62:
					goto IL_4CB;
				case 63:
					if (A_0.LocalName == ClipboardData.b("ㅯṱs፵੷ᑹᵻ੽솁ﲇ揄", a_))
					{
						num = 83;
						continue;
					}
					goto IL_72B;
				case 64:
					if (paragraphBase != null)
					{
						num = 50;
						continue;
					}
					goto IL_DB7;
				case 65:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_660;
					default:
						goto IL_E03;
					}
					break;
				case 66:
					goto IL_660;
				case 67:
				{
					bool flag;
					if (flag)
					{
						num = 48;
						continue;
					}
					goto IL_4CB;
				}
				case 68:
				{
					bool flag;
					if (flag)
					{
						num = 54;
						continue;
					}
					goto IL_665;
				}
				case 69:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 51;
						continue;
					}
					num = 63;
					continue;
				case 70:
				{
					string attribute;
					if (attribute.ToLower() == ClipboardData.b("ɯ᭱፳ṵ౷", a_))
					{
						num = 7;
						continue;
					}
					goto IL_4CB;
				}
				case 71:
					num = 16;
					continue;
				case 72:
					if (paragraphBase != null)
					{
						num = 37;
						continue;
					}
					goto IL_3AF;
				case 73:
				{
					string attribute2;
					paragraphBase = this.\u1716()[attribute2];
					num = 27;
					continue;
				}
				case 74:
				{
					paragraphBase = new TextRange(this.ᜄ);
					string a_5 = '\t'.ToString();
					this.ᜀ(paragraphBase as TextRange, a_5, this.\u1716);
					num = 26;
					continue;
				}
				case 75:
					if (!flag3)
					{
						num = 60;
						continue;
					}
					goto IL_ACA;
				case 76:
				{
					bool flag2;
					if (flag2)
					{
						num = 77;
						continue;
					}
					goto IL_665;
				}
				case 77:
					paragraphBase = this.ᜀ(A_0, A_1, ref a_3);
					paragraphBase = null;
					num = 44;
					continue;
				case 78:
					goto IL_50A;
				case 79:
				{
					MemoryStream memoryStream = this.ᜢ(A_0);
					memoryStream.Position = 0L;
					xmlReader = spr\u23D7.ᜀ(memoryStream);
					xmlReader.Read();
					num = 39;
					continue;
				}
				case 80:
					spr᧓.ᝐ = new Dictionary<string, int>(24)
					{
						{
							ClipboardData.b("ɯ≱ٳ", a_),
							0
						},
						{
							ClipboardData.b("ᑯ᝱ᡳ≵ᵷɹࡻ", a_),
							1
						},
						{
							ClipboardData.b("ѯ", a_),
							2
						},
						{
							ClipboardData.b("ᑯqᕳŵᅷᑹ᭻", a_),
							3
						},
						{
							ClipboardData.b("Ὧၱṳ፵᭷๹", a_),
							4
						},
						{
							ClipboardData.b("o᭱ᝳɵ", a_),
							5
						},
						{
							ClipboardData.b("ቯq", a_),
							6
						},
						{
							ClipboardData.b("፯q", a_),
							7
						},
						{
							ClipboardData.b("ᙯṱၳ㕵ၷ᭹๻", a_),
							8
						},
						{
							ClipboardData.b("ᑯ᝱ᡳ㽵ᙷॹࡻ౽푿ﲃ", a_),
							9
						},
						{
							ClipboardData.b("᥯ᱱݳɵ੷⹹᥻ٽ", a_),
							10
						},
						{
							ClipboardData.b("ᙯᵱ᭳ɵᙷᕹࡻ᭽퉿慎", a_),
							11
						},
						{
							ClipboardData.b("ᕯᱱၳᡵ᝷๹᥻ⱽ", a_),
							12
						},
						{
							ClipboardData.b("ᙯᵱ᭳ɵᙷᕹࡻ᭽퉿", a_),
							13
						},
						{
							ClipboardData.b("ᕯᱱၳᡵ᝷๹᥻ⱽ", a_),
							14
						},
						{
							ClipboardData.b("oٱᕳᑵ", a_),
							15
						},
						{
							ClipboardData.b("ѯ፱ᙳ", a_),
							16
						},
						{
							ClipboardData.b("ͯୱᥳ", a_),
							17
						},
						{
							ClipboardData.b("ṯᵱ㙳ѵᵷ᭹᝻㙽勵", a_),
							18
						},
						{
							ClipboardData.b("ͯᵱታɵぷ͹౻ᙽ", a_),
							19
						},
						{
							ClipboardData.b("፯ᵱᥳ᭵ᵷᑹࡻⱽ", a_),
							20
						},
						{
							ClipboardData.b("ㅯṱs፵੷ᑹᵻ੽솁ﲇ揄", a_),
							21
						},
						{
							ClipboardData.b("㍯ᩱ᭳ή᭷ό", a_),
							22
						},
						{
							ClipboardData.b("ᅯṱs㕵ၷཹቻᕽ", a_),
							23
						}
					};
					num = 86;
					continue;
				case 81:
					if (spr᧓.ᝐ == null)
					{
						num = 80;
						continue;
					}
					goto IL_436;
				case 82:
				{
					string localName;
					int num2;
					if (spr᧓.ᝐ.TryGetValue(localName, out num2))
					{
						num = 8;
						continue;
					}
					goto IL_4CB;
				}
				case 83:
					num = 4;
					continue;
				case 84:
					goto IL_37A;
				case 85:
					goto IL_4CB;
				case 86:
					goto IL_436;
				}
				goto IL_188;
				IL_37A:
				this.\u1716 = new CharacterFormat(this.ᜄ);
				num = 6;
				continue;
				IL_3AF:
				paragraphBase = this.ᜁ(xmlReader, A_1);
				num = 58;
				continue;
				IL_436:
				num = 82;
				continue;
				IL_4CB:
				num = 75;
				continue;
				IL_50A:
				flag3 = true;
				num = 55;
				continue;
				IL_5C6:
				this.ᜋ(A_0, this.\u1716);
				num = 59;
				continue;
				IL_623:
				num = 2;
				continue;
				IL_188:
				goto IL_623;
				IL_665:
				paragraphBase = this.ᜀ(A_0, A_1, ref a_3);
				num = 78;
				continue;
				IL_72B:
				A_0.Read();
				num = 41;
				continue;
				IL_ACA:
				this.ᜀ(A_0);
				num = 64;
				continue;
				IL_D1B:
				paragraphBase = this.\u171C(A_0);
				(paragraphBase as Footnote).MarkerCharacterFormat.ImportContainer(format);
				num = 43;
				continue;
				IL_660:
				goto IL_D1B;
				IL_DB7:
				num = 35;
			}
			return;
			IL_E03:
			if (false)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06001EC0 RID: 7872 RVA: 0x001F163C File Offset: 0x001F063C
	private void ᜇ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		switch (0)
		{
		default:
		{
			Field field;
			Field field2;
			FormField formField;
			for (;;)
			{
				string text = A_0.ReadString();
				int num = 12;
				for (;;)
				{
					ParagraphBase paragraphBase;
					int num2;
					switch (num)
					{
					case 0:
						goto IL_140;
					case 1:
						if (this.\u1716 != null)
						{
							num = 7;
							continue;
						}
						return;
					case 2:
						if (!(field is FormField))
						{
							num = 15;
							continue;
						}
						return;
					case 3:
						field2 = this.\u1713().Pop();
						A_1.Remove(field2);
						field2 = this.ᜫ(field2.Code);
						num = 26;
						continue;
					case 4:
						if (this.\u1716 != null)
						{
							num = 9;
							continue;
						}
						goto IL_1D6;
					case 5:
						goto IL_213;
					case 6:
						goto IL_4F9;
					case 7:
						goto IL_53D;
					case 8:
						goto IL_39D;
					case 9:
						field.ApplyCharacterFormat(this.\u1716);
						num = 6;
						continue;
					case 10:
						goto IL_74B;
					case 11:
						if (this.\u171B != FieldCharType.Seperate)
						{
							num = 25;
							continue;
						}
						goto IL_774;
					case 12:
						if (text != null)
						{
							num = 43;
							continue;
						}
						return;
					case 13:
						if (A_1.LastItem is FormField)
						{
							num = 14;
							continue;
						}
						goto IL_231;
					case 14:
						formField = (A_1.LastItem as FormField);
						num = 31;
						continue;
					case 15:
						this.\u1713().Push(field);
						num = 52;
						continue;
					case 16:
						num = 59;
						continue;
					case 17:
						if (this.\u1716 != null)
						{
							num = 58;
							continue;
						}
						goto IL_714;
					case 18:
						if (this.\u1712() != field2)
						{
							num = 38;
							continue;
						}
						return;
					case 19:
						if (paragraphBase is MergeField)
						{
							num = 41;
							continue;
						}
						goto IL_542;
					case 20:
						if (this.ᜄ())
						{
							num = 10;
							continue;
						}
						goto IL_774;
					case 21:
						if (field.Type == FieldType.FieldMergeField)
						{
							num = 44;
							continue;
						}
						field.Code = text;
						num = 13;
						continue;
					case 22:
						if (this.\u171B != FieldCharType.Seperate)
						{
							num = 32;
							continue;
						}
						goto IL_3D0;
					case 23:
						goto IL_452;
					case 24:
						if (this.\u171B == FieldCharType.SimpleField)
						{
							num = 29;
							continue;
						}
						goto IL_542;
					case 25:
					{
						Field field3 = this.\u1712();
						field3.Code += text;
						num = 50;
						continue;
					}
					case 26:
						if (this.\u1716 != null)
						{
							num = 30;
							continue;
						}
						goto IL_162;
					case 27:
						if (this.ᜄ())
						{
							num = 61;
							continue;
						}
						return;
					case 28:
						goto IL_714;
					case 29:
						goto IL_3D0;
					case 30:
						field2.ApplyCharacterFormat(this.\u1716);
						num = 48;
						continue;
					case 31:
						if (formField.Code != field.Code)
						{
							num = 36;
							continue;
						}
						goto IL_231;
					case 32:
						num = 24;
						continue;
					case 33:
						goto IL_36B;
					case 34:
						num = 60;
						continue;
					case 35:
						goto IL_287;
					case 36:
						goto IL_2F6;
					case 37:
						if (this.\u1712().NextSibling == null)
						{
							num = 57;
							continue;
						}
						goto IL_287;
					case 38:
						goto IL_1AC;
					case 39:
						num = 40;
						continue;
					case 40:
						if (num2 != A_1.Count - 1)
						{
							num = 23;
							continue;
						}
						goto IL_74B;
					case 41:
					{
						Field field4 = this.\u1712();
						field4.Text += text;
						num = 1;
						continue;
					}
					case 42:
						if (!this.\u171A)
						{
							num = 62;
							continue;
						}
						goto IL_7A9;
					case 43:
						num = 53;
						continue;
					case 44:
						this.\u171C.Remove(0, this.\u171C.Length);
						num = 4;
						continue;
					case 45:
						if (this.ᜄ())
						{
							num = 34;
							continue;
						}
						goto IL_39D;
					case 46:
						if (this.\u1712() != null)
						{
							num = 51;
							continue;
						}
						goto IL_7A9;
					case 47:
						if (this.\u1712() != null)
						{
							num = 54;
							continue;
						}
						goto IL_287;
					case 48:
						goto IL_162;
					case 49:
						if (this.\u1712() != null)
						{
							num = 16;
							continue;
						}
						goto IL_774;
					case 50:
						if (this.\u1712().Type == FieldType.FieldUnknown)
						{
							num = 3;
							continue;
						}
						num = 27;
						continue;
					case 51:
						num = 45;
						continue;
					case 52:
						goto IL_313;
					case 53:
						if (text == string.Empty)
						{
							num = 33;
							continue;
						}
						field = this.ᜫ(text);
						num = 42;
						continue;
					case 54:
						num = 37;
						continue;
					case 55:
						goto IL_15D;
					case 56:
						if (this.\u1716 != null)
						{
							num = 5;
							continue;
						}
						goto IL_140;
					case 57:
						paragraphBase = this.\u1712();
						num2 = A_1.IndexOf(this.\u1712());
						num = 35;
						continue;
					case 58:
						if (true)
						{
						}
						field.ApplyCharacterFormat(this.\u1716);
						num = 28;
						continue;
					case 59:
						if (num2 >= 0)
						{
							num = 39;
							continue;
						}
						goto IL_452;
					case 60:
						if (field != this.\u1712())
						{
							num = 8;
							continue;
						}
						goto IL_7A9;
					case 61:
						goto IL_5B6;
					case 62:
						num = 46;
						continue;
					}
					break;
					IL_140:
					TextRange textRange;
					textRange.Text = text;
					this.ᜀ(textRange, A_1);
					num = 55;
					continue;
					IL_162:
					A_1.Add(field2);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_213:
						textRange.ApplyCharacterFormat(this.\u1716);
						num = 0;
						continue;
					default:
						if (false)
						{
						}
						num = 18;
						continue;
					}
					IL_231:
					this.\u171C.Remove(0, this.\u171C.Length);
					num = 17;
					continue;
					IL_287:
					num = 22;
					continue;
					IL_39D:
					paragraphBase = (A_1.LastItem as ParagraphBase);
					num2 = -1;
					num = 47;
					continue;
					IL_3D0:
					num = 19;
					continue;
					IL_452:
					num = 20;
					continue;
					IL_542:
					num = 49;
					continue;
					IL_714:
					this.ᜀ(field, A_1);
					this.ᜀ(field);
					num = 2;
					continue;
					IL_74B:
					num = 11;
					continue;
					IL_774:
					textRange = new TextRange(this.ᜄ);
					num = 56;
					continue;
					IL_7A9:
					num = 21;
				}
			}
			IL_15D:
			return;
			IL_1AC:
			this.\u1713().Push(field2);
			return;
			IL_1D6:
			this.ᜀ(field, A_1);
			this.\u1713().Push(field);
			return;
			IL_2F6:
			formField.Code += field.Code;
			return;
			IL_313:
			return;
			IL_36B:
			return;
			IL_4F9:
			goto IL_1D6;
			IL_53D:
			this.\u1712().ApplyCharacterFormat(this.\u1716);
			return;
			IL_5B6:
			A_1.Add(this.\u1712());
			return;
		}
		}
	}

	// Token: 0x06001EC1 RID: 7873 RVA: 0x001F1E20 File Offset: 0x001F0E20
	private void ᜆ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			Stream a_2;
			for (;;)
			{
				if (true)
				{
				}
				string attribute = A_0.GetAttribute(ClipboardData.b("ᕲᥴ፶㩸፺ᱼൾ햀廒", a_), ClipboardData.b("᭲ŴͶॸ䅺剼偾ﺌꆎﺐ練咽캠톢좤욦\udda8\ud8aa莬삮쎰풲骴삶횸즺\ud9bc쾾돀곂ꛄꋆ뫈룊꓌ꇎ뛐뻒맔컠転蓤軦蟨", a_));
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_11F;
					case 1:
						if (!this.ᜇ(a_2))
						{
							num = 8;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_149;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 2:
					{
						string a;
						if (!(a == ClipboardData.b("rၴݶᡸॺᱼ୾", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_121;
					}
					case 3:
						num = 10;
						continue;
					case 4:
						return;
					case 5:
						goto IL_149;
					case 6:
						num = 2;
						continue;
					case 7:
					{
						string a;
						if (!(a == ClipboardData.b("ᅲၴၶၸᕺ", a_)))
						{
							num = 6;
							continue;
						}
						goto IL_12A;
					}
					case 8:
					{
						string a;
						if ((a = attribute) != null)
						{
							num = 5;
							continue;
						}
						return;
					}
					case 9:
						if (attribute == null)
						{
							num = 4;
							continue;
						}
						a_2 = this.ᜢ(A_0);
						num = 1;
						continue;
					case 10:
					{
						string a;
						if (!(a == ClipboardData.b("ᙲ᭴፶", a_)))
						{
							num = 12;
							continue;
						}
						this.ᜀ(A_1);
						num = 11;
						continue;
					}
					case 11:
						goto IL_144;
					case 12:
						return;
					}
					break;
					IL_149:
					num = 7;
				}
			}
			return;
			IL_11F:
			XmlReader a_3 = spr\u23D7.ᜀ(a_2);
			this.ᜅ(a_3, A_1);
			return;
			IL_121:
			this.ᜁ(A_1);
			return;
			IL_12A:
			this.ᜅ();
			return;
			IL_144:
			return;
		}
		}
	}

	// Token: 0x06001EC2 RID: 7874 RVA: 0x001F2010 File Offset: 0x001F1010
	private bool ᜇ(Stream A_0)
	{
		int a_ = 9;
		for (;;)
		{
			XmlReader xmlReader = spr\u23D7.ᜀ(A_0);
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!xmlReader.Read())
					{
						num = 1;
						continue;
					}
					num = 4;
					continue;
				case 1:
					goto IL_4E;
				case 2:
					return true;
				case 3:
					goto IL_36;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					if (false)
					{
					}
					if (xmlReader.LocalName == ClipboardData.b("८ᝰ㝲ᑴͶᡸ", a_))
					{
						num = 2;
						continue;
					}
					goto IL_36;
				}
				break;
				IL_36:
				num = 0;
			}
		}
		IL_4E:
		if (true)
		{
		}
		return false;
	}

	// Token: 0x06001EC3 RID: 7875 RVA: 0x001F20D0 File Offset: 0x001F10D0
	private void ᜁ(ParagraphItemCollection A_0)
	{
		switch (0)
		{
		default:
		{
			ParagraphBase paragraphBase;
			FieldMark a_;
			for (;;)
			{
				this.\u171A = false;
				this.\u171B = FieldCharType.Seperate;
				paragraphBase = (A_0.LastItem as ParagraphBase);
				int num = 20;
				for (;;)
				{
					bool flag;
					bool flag2;
					bool flag3;
					string value;
					bool flag4;
					switch (num)
					{
					case 0:
						goto IL_2EE;
					case 1:
						if (this.\u171C.Length > 0)
						{
							num = 37;
							continue;
						}
						return;
					case 2:
						flag = false;
						goto IL_400;
					case 3:
						if (!(paragraphBase is Field))
						{
							num = 14;
							continue;
						}
						goto IL_32F;
					case 4:
						paragraphBase = this.\u1712();
						num = 23;
						continue;
					case 5:
						goto IL_3FB;
					case 6:
						if (paragraphBase is MergeField)
						{
							num = 7;
							continue;
						}
						return;
					case 7:
						num = 1;
						continue;
					case 8:
					{
						Field field;
						if (field != null)
						{
							num = 22;
							continue;
						}
						goto IL_3C1;
					}
					case 9:
						flag2 = false;
						goto IL_481;
					case 10:
						num = 2;
						continue;
					case 11:
					{
						Field field = paragraphBase as Field;
						num = 8;
						continue;
					}
					case 12:
						goto IL_32F;
					case 13:
						num = 9;
						continue;
					case 14:
						num = 36;
						continue;
					case 15:
						if (!(paragraphBase is TextRange))
						{
							num = 10;
							continue;
						}
						num = 24;
						continue;
					case 16:
						if (!(paragraphBase is Field))
						{
							num = 13;
							continue;
						}
						num = 31;
						continue;
					case 17:
						if (!flag3)
						{
							num = 11;
							continue;
						}
						goto IL_3C1;
					case 18:
					{
						if (paragraphBase.Owner == null)
						{
							num = 28;
							continue;
						}
						(paragraphBase as Field).ParseFieldCode((paragraphBase as Field).Code);
						TableOfContent tableOfContent = new TableOfContent(this.ᜄ, this.\u1712().FormattingString);
						this.ᜄ.TOC = tableOfContent;
						tableOfContent.FormattingString = this.\u1712().FormattingString;
						A_0.Remove(paragraphBase);
						this.ᜀ(tableOfContent, A_0);
						num = 25;
						continue;
					}
					case 19:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_219;
						default:
						{
							if (false)
							{
							}
							if (true)
							{
							}
							Field field;
							Field field2 = field;
							field2.m_formattingString += this.\u171C;
							num = 26;
							continue;
						}
						}
						break;
					case 20:
						if (this.\u1712() != null)
						{
							num = 4;
							continue;
						}
						goto IL_4AA;
					case 21:
						if (!(paragraphBase is MergeField))
						{
							num = 27;
							continue;
						}
						goto IL_1C9;
					case 22:
						goto IL_219;
					case 23:
						goto IL_4AA;
					case 24:
						flag = true;
						goto IL_400;
					case 25:
						goto IL_3C1;
					case 26:
						goto IL_3C1;
					case 27:
						num = 16;
						continue;
					case 28:
						return;
					case 29:
						if (paragraphBase is Field)
						{
							num = 5;
							continue;
						}
						return;
					case 30:
						num = 18;
						continue;
					case 31:
						flag2 = ((paragraphBase as Field).Type == FieldType.FieldTOC);
						goto IL_481;
					case 32:
					{
						string fieldName;
						if (!string.IsNullOrEmpty(fieldName))
						{
							num = 34;
							continue;
						}
						goto IL_2CA;
					}
					case 33:
					{
						Field field;
						if (field.FormattingString.Contains(value))
						{
							num = 19;
							continue;
						}
						goto IL_3C1;
					}
					case 34:
					{
						string fieldName;
						MergeField mergeField;
						mergeField.FieldName = fieldName;
						num = 35;
						continue;
					}
					case 35:
						goto IL_2CA;
					case 36:
						if (paragraphBase is TextRange)
						{
							num = 12;
							continue;
						}
						goto IL_1C9;
					case 37:
					{
						MergeField mergeField = paragraphBase as MergeField;
						string fieldName = mergeField.FieldName;
						mergeField.ParseFieldCode(mergeField.Value + this.\u171C.ToString());
						num = 32;
						continue;
					}
					case 38:
						if (flag4)
						{
							num = 30;
							continue;
						}
						num = 17;
						continue;
					}
					break;
					IL_1C9:
					num = 6;
					continue;
					IL_219:
					num = 33;
					continue;
					IL_2CA:
					this.\u171C.Remove(0, this.\u171C.Length);
					num = 0;
					continue;
					IL_32F:
					num = 21;
					continue;
					IL_3C1:
					a_ = new FieldMark(this.ᜄ, FieldMarkType.FieldSeparator);
					this.ᜁ(a_, A_0);
					num = 29;
					continue;
					IL_400:
					flag3 = flag;
					value = this.\u171C.ToString();
					num = 38;
					continue;
					IL_481:
					flag4 = flag2;
					num = 15;
					continue;
					IL_4AA:
					num = 3;
				}
			}
			IL_2EE:
			return;
			IL_3FB:
			(paragraphBase as Field).Separator = a_;
			return;
		}
		}
	}

	// Token: 0x06001EC4 RID: 7876 RVA: 0x001F25E0 File Offset: 0x001F15E0
	private void ᜀ(ParagraphItemCollection A_0)
	{
		for (;;)
		{
			this.\u171A = false;
			this.\u171B = FieldCharType.End;
			ParagraphBase paragraphBase = A_0.LastItem as ParagraphBase;
			int num = 17;
			for (;;)
			{
				Field field;
				switch (num)
				{
				case 0:
					goto IL_C4;
				case 1:
					if (!(paragraphBase is Field))
					{
						goto IL_1EF;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_82;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 2:
				{
					FieldMark a_ = new FieldMark(this.ᜄ, FieldMarkType.FieldEnd);
					this.ᜁ(a_, A_0);
					num = 1;
					continue;
				}
				case 3:
				{
					FieldMark a_;
					(paragraphBase as Field).End = a_;
					num = 9;
					continue;
				}
				case 4:
					if (this.\u171D.Count > 0)
					{
						num = 12;
						continue;
					}
					goto IL_210;
				case 5:
					if (!(paragraphBase is MergeField))
					{
						num = 2;
						continue;
					}
					goto IL_C4;
				case 6:
					num = 4;
					continue;
				case 7:
					if (field.Type != FieldType.FieldDate)
					{
						num = 13;
						continue;
					}
					goto IL_16F;
				case 8:
					goto IL_82;
				case 9:
					goto IL_1EF;
				case 10:
					goto IL_E7;
				case 11:
					if (field.Type == FieldType.FieldTime)
					{
						num = 15;
						continue;
					}
					goto IL_210;
				case 12:
					field = this.\u171D.Pop();
					field.ParseFieldCode(field.Code);
					num = 7;
					continue;
				case 13:
					num = 11;
					continue;
				case 14:
					goto IL_180;
				case 15:
					goto IL_16F;
				case 16:
					if (this.\u171D != null)
					{
						num = 6;
						continue;
					}
					goto IL_210;
				case 17:
					if (this.\u1712() != null)
					{
						num = 8;
						continue;
					}
					goto IL_E7;
				}
				break;
				IL_82:
				paragraphBase = this.\u1712();
				num = 10;
				continue;
				IL_C4:
				num = 16;
				continue;
				IL_E7:
				num = 5;
				continue;
				IL_16F:
				field.ᜎ();
				num = 14;
				continue;
				IL_1EF:
				this.\u1716 = null;
				num = 0;
			}
		}
		IL_180:
		IL_210:
		if (true)
		{
		}
		this.\u171B = FieldCharType.Unknown;
	}

	// Token: 0x06001EC5 RID: 7877 RVA: 0x001F280C File Offset: 0x001F180C
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
		this.\u171A = true;
		this.\u171B = FieldCharType.Begin;
	}

	// Token: 0x06001EC6 RID: 7878 RVA: 0x001F2858 File Offset: 0x001F1858
	private Field ᜫ(string A_0)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			Field field;
			for (;;)
			{
				field = null;
				string text = A_0.Trim();
				FieldType fieldType = spr\u1C8B.ᜀ(text);
				FieldType fieldType2 = fieldType;
				int num = 16;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (fieldType2 != FieldType.FieldFormDropDown)
						{
							num = 21;
							continue;
						}
						goto IL_412;
					case 1:
						num = 27;
						continue;
					case 2:
						goto IL_14E;
					case 3:
						goto IL_35F;
					case 4:
						num = 6;
						continue;
					case 5:
						num = 25;
						continue;
					case 6:
					{
						string a;
						if (!(a == ClipboardData.b("⵪≬㵮㱰㝲❴㡶⥸㽺㉼⡾쾀", a_)))
						{
							num = 13;
							continue;
						}
						goto IL_FA;
					}
					case 7:
						return field;
					case 8:
						goto IL_469;
					case 9:
					{
						string a;
						if (!(a == ClipboardData.b("⡪╬⩮㉰㡲㝴㡶ⅸ", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_37C;
					}
					case 10:
						goto IL_469;
					case 11:
						goto IL_1BE;
					case 12:
						goto IL_1BE;
					case 13:
						num = 9;
						continue;
					case 14:
						if (field is FormField)
						{
							return field;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_14E;
						default:
							if (false)
							{
							}
							num = 35;
							continue;
						}
						break;
					case 15:
						goto IL_1BE;
					case 16:
						if (fieldType2 <= FieldType.FieldMergeField)
						{
							num = 24;
							continue;
						}
						num = 36;
						continue;
					case 17:
						num = 26;
						continue;
					case 18:
						num = 33;
						continue;
					case 19:
						goto IL_35F;
					case 20:
						num = 31;
						continue;
					case 21:
						num = 3;
						continue;
					case 22:
					{
						string a;
						if (!(a == ClipboardData.b("⵪≬㵮㱰❲ぴ⽶⵸", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_1A1;
					}
					case 23:
						if (this.\u1712() == null)
						{
							num = 30;
							continue;
						}
						field = this.\u1712();
						if (true)
						{
						}
						num = 15;
						continue;
					case 24:
						num = 29;
						continue;
					case 25:
					{
						string a;
						if (!(a == ClipboardData.b("⵪≬㵮㱰ひ㵴㉶㩸ぺ㽼ま\ud980", a_)))
						{
							num = 17;
							continue;
						}
						goto IL_37C;
					}
					case 26:
						goto IL_469;
					case 27:
					{
						string a;
						if (!(a == ClipboardData.b("⽪⥬⍮㡰⁲ⅴ", a_)))
						{
							num = 4;
							continue;
						}
						goto IL_FA;
					}
					case 28:
						goto IL_469;
					case 29:
						if (fieldType2 != FieldType.FieldIf)
						{
							num = 18;
							continue;
						}
						field = new IfField(this.ᜄ);
						num = 32;
						continue;
					case 30:
						num = 39;
						continue;
					case 31:
					{
						string a;
						if (!(a == ClipboardData.b("㽪⡬㝮╰㩲㭴❶ⱸ⽺", a_)))
						{
							num = 37;
							continue;
						}
						goto IL_1A1;
					}
					case 32:
						goto IL_1BE;
					case 33:
						if (fieldType2 != FieldType.FieldMergeField)
						{
							num = 38;
							continue;
						}
						field = new MergeField(this.ᜄ);
						num = 12;
						continue;
					case 34:
						goto IL_1BE;
					case 35:
						field.Type = fieldType;
						num = 7;
						continue;
					case 36:
						switch (fieldType2)
						{
						case FieldType.FieldFormTextInput:
						case FieldType.FieldFormCheckBox:
							goto IL_412;
						default:
							num = 2;
							continue;
						}
						break;
					case 37:
						num = 22;
						continue;
					case 38:
						num = 19;
						continue;
					case 39:
					{
						string a;
						if ((a = text.ToUpper()) != null)
						{
							num = 20;
							continue;
						}
						goto IL_469;
					}
					}
					break;
					IL_FA:
					field = new DropDownFormField(this.ᜄ);
					num = 28;
					continue;
					IL_14E:
					num = 0;
					continue;
					IL_1A1:
					field = new TextFormField(this.ᜄ);
					num = 8;
					continue;
					IL_1BE:
					Field field2 = field;
					field2.Code += A_0;
					num = 14;
					continue;
					IL_35F:
					field = new Field(this.ᜄ);
					num = 11;
					continue;
					IL_37C:
					field = new CheckBoxFormField(this.ᜄ);
					num = 10;
					continue;
					IL_412:
					num = 23;
					continue;
					IL_469:
					(field as FormField).HasFFData = false;
					num = 34;
				}
			}
			return field;
		}
		}
	}

	// Token: 0x06001EC7 RID: 7879 RVA: 0x001F2D24 File Offset: 0x001F1D24
	private void ᜂ(ParagraphBase A_0, ParagraphItemCollection A_1)
	{
		for (;;)
		{
			int count = A_1.Count;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!(A_0.PreviousSibling is FieldMark))
					{
						return;
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
						continue;
					}
					break;
				case 1:
					return;
				case 2:
				{
					if (true)
					{
					}
					FieldMark a_ = new FieldMark(this.ᜄ, FieldMarkType.FieldEnd);
					this.ᜀ(a_, A_1);
					(A_1[count - 3] as Field).End = a_;
					num = 1;
					continue;
				}
				}
				break;
			}
		}
	}

	// Token: 0x06001EC8 RID: 7880 RVA: 0x001F2DD0 File Offset: 0x001F1DD0
	private void ᜅ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				string localName2;
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (A_1.OwnerBase is Paragraph)
					{
						num = 14;
						continue;
					}
					goto IL_249;
				case 2:
					if (true)
					{
					}
					num = 12;
					continue;
				case 3:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 2;
						continue;
					}
					goto IL_249;
				}
				case 5:
					this.ᜀ(A_0);
					num = 6;
					continue;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_249;
					default:
						if (false)
						{
						}
						goto IL_1C9;
					}
					break;
				case 7:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 18;
						continue;
					}
					goto IL_249;
				case 8:
					if (!A_0.IsEmptyElement)
					{
						num = 17;
						continue;
					}
					goto IL_249;
				case 9:
					goto IL_195;
				case 10:
					if (!(A_0.LocalName != localName2))
					{
						num = 11;
						continue;
					}
					num = 7;
					continue;
				case 11:
					return;
				case 12:
				{
					string localName;
					if (localName == ClipboardData.b("८ᝰ㝲ᑴͶᡸ", a_))
					{
						num = 21;
						continue;
					}
					goto IL_249;
				}
				case 13:
					goto IL_1C9;
				case 14:
				{
					FormField formField;
					this.ᜀ(formField, A_1);
					num = 20;
					continue;
				}
				case 15:
					if (this.\u1716 != null)
					{
						num = 19;
						continue;
					}
					goto IL_195;
				case 16:
					if (A_0.NodeType == XmlNodeType.Whitespace)
					{
						num = 5;
						continue;
					}
					goto IL_1C9;
				case 17:
				{
					Stream stream = this.ᜢ(A_0);
					FormField formField = this.ᜆ(stream);
					this.\u1713().Push(formField);
					stream.Position = 0L;
					XmlReader a_2 = spr\u23D7.ᜀ(stream);
					this.ᜀ(a_2, formField);
					num = 15;
					continue;
				}
				case 18:
					num = 3;
					continue;
				case 19:
				{
					FormField formField;
					formField.ApplyCharacterFormat(this.\u1716);
					num = 9;
					continue;
				}
				case 20:
					goto IL_249;
				case 21:
					num = 8;
					continue;
				}
				if (A_0.IsEmptyElement)
				{
					num = 0;
					continue;
				}
				A_0.MoveToContent();
				localName2 = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				num = 13;
				continue;
				IL_195:
				this.\u1716 = null;
				num = 1;
				continue;
				IL_1C9:
				num = 10;
				continue;
				IL_249:
				num = 16;
			}
			return;
		}
		}
	}

	// Token: 0x06001EC9 RID: 7881 RVA: 0x001F30AC File Offset: 0x001F20AC
	private void ᜀ(XmlReader A_0, FormField A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 24;
			for (;;)
			{
				string localName;
				string localName2;
				switch (num)
				{
				case 0:
					num = 20;
					continue;
				case 1:
					goto IL_28E;
				case 2:
					num = 21;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3A6;
					default:
					{
						if (false)
						{
						}
						string attribute;
						A_1.Help = attribute;
						num = 22;
						continue;
					}
					}
					break;
				case 4:
					return;
				case 5:
					goto IL_53F;
				case 6:
					goto IL_53F;
				case 7:
					return;
				case 8:
				{
					int num2;
					if (spr᧓.ᝑ.TryGetValue(localName, out num2))
					{
						num = 0;
						continue;
					}
					goto IL_200;
				}
				case 9:
					if (!(A_0.LocalName != localName2))
					{
						num = 4;
						continue;
					}
					num = 27;
					continue;
				case 10:
					goto IL_200;
				case 11:
					goto IL_200;
				case 12:
					num = 19;
					continue;
				case 13:
					spr᧓.ᝑ = new Dictionary<string, int>(10)
					{
						{
							ClipboardData.b("ᙴὶᱸ᡺ᙼ㵾ﮂ", a_),
							0
						},
						{
							ClipboardData.b("ŴቶŸེ㑼ᅾ", a_),
							1
						},
						{
							ClipboardData.b("ᅴ፶㕸ቺ๼୾", a_),
							2
						},
						{
							ClipboardData.b("᭴ᙶᑸṺ", a_),
							3
						},
						{
							ClipboardData.b("ၴ᥶ᡸ᥺ᅼ᩾", a_),
							4
						},
						{
							ClipboardData.b("ᙴᙶᕸ᡺㉼ᅾ쒀ﮂ", a_),
							5
						},
						{
							ClipboardData.b("ᵴቶᕸ୺⥼᩾呂", a_),
							6
						},
						{
							ClipboardData.b("ٴͶᡸེࡼ౾햀ﶄ", a_),
							7
						},
						{
							ClipboardData.b("ၴ᥶൸ॺѼ㉾", a_),
							8
						},
						{
							ClipboardData.b("ၴྲྀၸེぼṾ", a_),
							9
						}
					};
					num = 35;
					continue;
				case 14:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						if (true)
						{
						}
						num = 33;
						continue;
					}
					return;
				case 15:
					num = 18;
					continue;
				case 16:
					goto IL_200;
				case 17:
					goto IL_200;
				case 18:
					if (spr᧓.ᝑ == null)
					{
						num = 13;
						continue;
					}
					goto IL_2DB;
				case 19:
					goto IL_200;
				case 20:
				{
					int num2;
					switch (num2)
					{
					case 0:
						this.ᜀ(A_0, A_1 as CheckBoxFormField);
						num = 16;
						continue;
					case 1:
						this.ᜀ(A_0, A_1 as TextFormField);
						num = 26;
						continue;
					case 2:
						this.ᜀ(A_0, A_1 as DropDownFormField);
						num = 23;
						continue;
					case 3:
						A_1.Name = A_0.GetAttribute(ClipboardData.b("ʹᙶᕸ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_));
						num = 10;
						continue;
					case 4:
						A_1.Enabled = this.ᜂ(A_0);
						num = 30;
						continue;
					case 5:
						A_1.CalculateOnExit = this.ᜂ(A_0);
						num = 32;
						continue;
					case 6:
					{
						string attribute = A_0.GetAttribute(ClipboardData.b("ʹᙶᕸ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_));
						num = 29;
						continue;
					}
					case 7:
					{
						string attribute2 = A_0.GetAttribute(ClipboardData.b("ʹᙶᕸ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_));
						num = 34;
						continue;
					}
					case 8:
						A_1.MacroOnStart = A_0.GetAttribute(ClipboardData.b("ʹᙶᕸ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_));
						num = 25;
						continue;
					case 9:
						A_1.MacroOnEnd = A_0.GetAttribute(ClipboardData.b("ʹᙶᕸ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_));
						num = 11;
						continue;
					default:
						num = 12;
						continue;
					}
					break;
				}
				case 21:
					goto IL_3A6;
				case 22:
					goto IL_200;
				case 23:
					goto IL_200;
				case 25:
					goto IL_200;
				case 26:
					goto IL_200;
				case 27:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 2;
						continue;
					}
					A_0.Read();
					num = 6;
					continue;
				case 28:
				{
					string attribute2;
					A_1.StatusBarHelp = attribute2;
					num = 17;
					continue;
				}
				case 29:
				{
					string attribute;
					if (attribute != null)
					{
						num = 3;
						continue;
					}
					goto IL_200;
				}
				case 30:
					goto IL_200;
				case 31:
					goto IL_28E;
				case 32:
					goto IL_200;
				case 33:
					num = 9;
					continue;
				case 34:
				{
					string attribute2;
					if (attribute2 != null)
					{
						num = 28;
						continue;
					}
					goto IL_200;
				}
				case 35:
					goto IL_2DB;
				}
				if (A_0.IsEmptyElement)
				{
					num = 7;
					continue;
				}
				A_0.MoveToContent();
				localName2 = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				num = 1;
				continue;
				IL_200:
				A_0.Read();
				num = 5;
				continue;
				IL_28E:
				num = 14;
				continue;
				IL_2DB:
				num = 8;
				continue;
				IL_53F:
				this.ᜀ(A_0);
				num = 31;
				continue;
				IL_3A6:
				if ((localName = A_0.LocalName) == null)
				{
					goto IL_200;
				}
				num = 15;
			}
			return;
		}
		}
	}

	// Token: 0x06001ECA RID: 7882 RVA: 0x001F3680 File Offset: 0x001F2680
	private void ᜀ(XmlReader A_0, DropDownFormField A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				string localName2;
				switch (num)
				{
				case 0:
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_203;
					default:
						if (false)
						{
						}
						num = 17;
						continue;
					}
					break;
				case 2:
				{
					string localName;
					if (!(localName == ClipboardData.b("ѧͩὫᩭ㕯ᱱsѵŷ", a_)))
					{
						num = 6;
						continue;
					}
					string attribute = A_0.GetAttribute(ClipboardData.b("ṧ୩k", a_), ClipboardData.b("gṩᡫṭ䩯嵱孳յ᭷ቹ᥻፽ꪃﶏﺑ秊ﶛ펟財쮣풥쾧薩\udbab솭슯횱쒳쒵ힷ\ud9b9\ud9bb춽뎿ꯁ꫃ꇅꗇꛉﳍ崙뗗믙뗛냝", a_));
					A_1.DropDownItems.Add(attribute);
					num = 11;
					continue;
				}
				case 4:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 1;
						continue;
					}
					return;
				case 5:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 19;
						continue;
					}
					goto IL_A2;
				}
				case 6:
					num = 12;
					continue;
				case 7:
					goto IL_120;
				case 8:
					goto IL_231;
				case 9:
					goto IL_A2;
				case 10:
				{
					string localName;
					if (!(localName == ClipboardData.b("౧ཀྵ੫཭կṱs", a_)))
					{
						num = 16;
						continue;
					}
					int a_2 = int.Parse(A_0.GetAttribute(ClipboardData.b("ṧ୩k", a_), ClipboardData.b("gṩᡫṭ䩯嵱孳յ᭷ቹ᥻፽ꪃﶏﺑ秊ﶛ펟財쮣풥쾧薩\udbab솭슯횱쒳쒵ힷ\ud9b9\ud9bb춽뎿ꯁ꫃ꇅꗇꛉﳍ崙뗗믙뗛냝", a_)));
					A_1.DefaultDropDownValue = a_2;
					num = 9;
					continue;
				}
				case 11:
					goto IL_A2;
				case 12:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᩧཀྵὫ᭭ᱯٱ", a_)))
					{
						num = 13;
						continue;
					}
					int dropDownSelectedIndex = int.Parse(A_0.GetAttribute(ClipboardData.b("ṧ୩k", a_), ClipboardData.b("gṩᡫṭ䩯嵱孳յ᭷ቹ᥻፽ꪃﶏﺑ秊ﶛ펟財쮣풥쾧薩\udbab솭슯횱쒳쒵ힷ\ud9b9\ud9bb춽뎿ꯁ꫃ꇅꗇꛉﳍ崙뗗믙뗛냝", a_)));
					A_1.DropDownSelectedIndex = dropDownSelectedIndex;
					num = 18;
					continue;
				}
				case 13:
					num = 10;
					continue;
				case 14:
					goto IL_231;
				case 15:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 21;
						continue;
					}
					A_0.Read();
					num = 7;
					continue;
				case 16:
					num = 22;
					continue;
				case 17:
					if (!(A_0.LocalName != localName2))
					{
						num = 20;
						continue;
					}
					num = 15;
					continue;
				case 18:
					goto IL_A2;
				case 19:
					num = 2;
					continue;
				case 20:
					return;
				case 21:
					num = 5;
					continue;
				case 22:
					goto IL_203;
				case 23:
					goto IL_120;
				}
				if (A_0.IsEmptyElement)
				{
					num = 0;
					continue;
				}
				if (true)
				{
				}
				A_0.MoveToContent();
				localName2 = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				num = 14;
				continue;
				IL_A2:
				A_0.Read();
				num = 23;
				continue;
				IL_120:
				this.ᜀ(A_0);
				num = 8;
				continue;
				IL_231:
				num = 4;
				continue;
				IL_203:
				goto IL_A2;
			}
			return;
		}
		}
	}

	// Token: 0x06001ECB RID: 7883 RVA: 0x001F39E8 File Offset: 0x001F29E8
	private void ᜀ(XmlReader A_0, TextFormField A_1)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 21;
			for (;;)
			{
				string localName2;
				switch (num)
				{
				case 0:
					goto IL_346;
				case 1:
					goto IL_1BD;
				case 2:
					return;
				case 3:
				{
					string localName;
					if (!(localName == ClipboardData.b("੦ࡨ፪Ⅼ੮ὰᑲŴὶ", a_)))
					{
						num = 8;
						continue;
					}
					if (true)
					{
					}
					A_1.MaximumLength = int.Parse(A_0.GetAttribute(ClipboardData.b("ᅦࡨݪ", a_), ClipboardData.b("སᵨὪᵬ啮幰屲ٴᑶᅸṺၼṾ궂﶐杖漢辠첢힤삦蚨\udcaa슬\uddae햰쎲잴\ud8b6\udab8\udeba캼첾ꣀ귂ꋄ꫆ꗈￌￎ䀹뫖룘닚돜", a_)));
					num = 15;
					continue;
				}
				case 4:
					num = 12;
					continue;
				case 5:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 20;
						continue;
					}
					A_0.Read();
					num = 13;
					continue;
				case 6:
				{
					string localName;
					if (!(localName == ClipboardData.b("Ŧ٨ᥪl๮հ", a_)))
					{
						num = 17;
						continue;
					}
					string attribute = A_0.GetAttribute(ClipboardData.b("ᅦࡨݪ", a_), ClipboardData.b("སᵨὪᵬ啮幰屲ٴᑶᅸṺၼṾ궂﶐杖漢辠첢힤삦蚨\udcaa슬\uddae햰쎲잴\ud8b6\udab8\udeba캼첾ꣀ귂ꋄ꫆ꗈￌￎ䀹뫖룘닚돜", a_));
					num = 27;
					continue;
				}
				case 7:
				{
					string attribute;
					A_1.TextFormat = this.ᜪ(attribute);
					num = 16;
					continue;
				}
				case 8:
					num = 6;
					continue;
				case 9:
					num = 23;
					continue;
				case 10:
					goto IL_258;
				case 11:
				{
					string localName;
					if (!(localName == ClipboardData.b("፦ၨ᭪࡬", a_)))
					{
						num = 4;
						continue;
					}
					string attribute2 = A_0.GetAttribute(ClipboardData.b("ᅦࡨݪ", a_), ClipboardData.b("སᵨὪᵬ啮幰屲ٴᑶᅸṺၼṾ궂﶐杖漢辠첢힤삦蚨\udcaa슬\uddae햰쎲잴\ud8b6\udab8\udeba캼첾ꣀ귂ꋄ꫆ꗈￌￎ䀹뫖룘닚돜", a_));
					A_1.TextFieldType = this.ᜩ(attribute2);
					num = 19;
					continue;
				}
				case 12:
				{
					string localName;
					if (!(localName == ClipboardData.b("ͦ౨൪౬ᩮᵰݲ", a_)))
					{
						goto IL_2F7;
					}
					A_1.DefaultText = A_0.GetAttribute(ClipboardData.b("ᅦࡨݪ", a_), ClipboardData.b("སᵨὪᵬ啮幰屲ٴᑶᅸṺၼṾ궂﶐杖漢辠첢힤삦蚨\udcaa슬\uddae햰쎲잴\ud8b6\udab8\udeba캼첾ꣀ귂ꋄ꫆ꗈￌￎ䀹뫖룘닚돜", a_));
					num = 22;
					continue;
				}
				case 13:
					goto IL_258;
				case 14:
					num = 11;
					continue;
				case 15:
					goto IL_346;
				case 16:
					goto IL_346;
				case 17:
					num = 26;
					continue;
				case 18:
					goto IL_1BD;
				case 19:
					goto IL_346;
				case 20:
					num = 28;
					continue;
				case 22:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2F7;
					default:
						if (false)
						{
						}
						goto IL_346;
					}
					break;
				case 23:
					if (!(A_0.LocalName != localName2))
					{
						num = 25;
						continue;
					}
					num = 5;
					continue;
				case 24:
					num = 3;
					continue;
				case 25:
					return;
				case 26:
					goto IL_346;
				case 27:
				{
					if (A_1.TextFieldType == TextFormFieldType.RegularText)
					{
						num = 7;
						continue;
					}
					string attribute;
					A_1.StringFormat = attribute;
					num = 0;
					continue;
				}
				case 28:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 14;
						continue;
					}
					goto IL_346;
				}
				case 29:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 9;
						continue;
					}
					return;
				}
				if (A_0.IsEmptyElement)
				{
					num = 2;
					continue;
				}
				A_0.MoveToContent();
				localName2 = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				num = 1;
				continue;
				IL_1BD:
				num = 29;
				continue;
				IL_258:
				this.ᜀ(A_0);
				num = 18;
				continue;
				IL_2F7:
				num = 24;
				continue;
				IL_346:
				A_0.Read();
				num = 10;
			}
			return;
		}
		}
	}

	// Token: 0x06001ECC RID: 7884 RVA: 0x001F3E18 File Offset: 0x001F2E18
	private TextFormat ᜪ(string A_0)
	{
		int a_ = 13;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 8;
				continue;
			case 1:
				if (A_0 == ClipboardData.b("㽲ᩴvᱸॺṼṾ", a_))
				{
					goto IL_57;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_117;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 2:
				if (!(A_0 == ClipboardData.b("❲ᱴͶᕸṺ嵼᱾", a_)))
				{
					goto IL_117;
				}
				return TextFormat.Titlecase;
			case 3:
				num = 2;
				continue;
			case 4:
				num = 9;
				continue;
			case 5:
				if (!(A_0 == ClipboardData.b("㕲ᱴն੸ེ嵼᱾", a_)))
				{
					num = 3;
					continue;
				}
				return TextFormat.FirstCapital;
			case 6:
				num = 5;
				continue;
			case 8:
				goto IL_69;
			case 9:
				if (!(A_0 == ClipboardData.b("♲մݶᱸॺṼṾ", a_)))
				{
					num = 10;
					continue;
				}
				return TextFormat.Uppercase;
			case 10:
				num = 1;
				continue;
			}
			if (A_0 != null)
			{
				num = 4;
				continue;
			}
			return TextFormat.None;
			IL_117:
			num = 0;
		}
		IL_57:
		if (true)
		{
		}
		return TextFormat.Lowercase;
		IL_69:
		return TextFormat.None;
	}

	// Token: 0x06001ECD RID: 7885 RVA: 0x001F3F80 File Offset: 0x001F2F80
	private TextFormFieldType ᜩ(string A_0)
	{
		int a_ = 17;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_0 == ClipboardData.b("᥶౸ᙺὼ᩾", a_)))
				{
					if (true)
					{
					}
					num = 7;
					continue;
				}
				return TextFormFieldType.NumberText;
			case 1:
				goto IL_62;
			case 3:
				if (!(A_0 == ClipboardData.b("ᑶ౸ॺོ᩾톄", a_)))
				{
					num = 4;
					continue;
				}
				return TextFormFieldType.DateText;
			case 4:
				num = 8;
				continue;
			case 5:
				if (!(A_0 == ClipboardData.b("ᑶ౸ॺོ᩾솄ﶈ", a_)))
				{
					num = 9;
					continue;
				}
				return TextFormFieldType.DateText;
			case 6:
				num = 0;
				continue;
			case 7:
				num = 5;
				continue;
			case 8:
				if (A_0 == ClipboardData.b("፶ᡸེ᡼", a_))
				{
					return TextFormFieldType.DateText;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return TextFormFieldType.DateText;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 9:
				num = 3;
				continue;
			case 10:
				num = 1;
				continue;
			}
			if (A_0 == null)
			{
				break;
			}
			num = 6;
		}
		IL_62:
		return TextFormFieldType.RegularText;
	}

	// Token: 0x06001ECE RID: 7886 RVA: 0x001F40E0 File Offset: 0x001F30E0
	private void ᜀ(XmlReader A_0, CheckBoxFormField A_1)
	{
		int a_ = 3;
		int num = 26;
		for (;;)
		{
			string localName2;
			switch (num)
			{
			case 0:
				goto IL_17D;
			case 1:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 24;
					continue;
				}
				return;
			case 2:
				num = 21;
				continue;
			case 3:
				goto IL_338;
			case 4:
				goto IL_139;
			case 5:
			{
				string localName;
				if (localName == ClipboardData.b("൨๪୬๮ѰὲŴ", a_))
				{
					A_1.DefaultCheckBoxValue = this.ᜂ(A_0);
					num = 25;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_338;
				default:
					if (false)
					{
					}
					num = 18;
					continue;
				}
				break;
			}
			case 6:
			{
				if (true)
				{
				}
				string localName;
				if (!(localName == ClipboardData.b("੨ͪ࡬౮ᩰᙲᅴ", a_)))
				{
					num = 16;
					continue;
				}
				A_1.Checked = this.ᜂ(A_0);
				num = 4;
				continue;
			}
			case 7:
				if (!(A_0.LocalName != localName2))
				{
					num = 0;
					continue;
				}
				num = 13;
				continue;
			case 8:
			{
				string localName;
				if (!(localName == ClipboardData.b("ᩨɪᝬ੮", a_)))
				{
					num = 2;
					continue;
				}
				A_1.CheckBoxSize = int.Parse(A_0.GetAttribute(ClipboardData.b("Ὠ੪Ŭ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_))) / 2;
				A_1.SizeType = CheckBoxSizeType.Exactly;
				num = 14;
				continue;
			}
			case 9:
				num = 17;
				continue;
			case 10:
				goto IL_139;
			case 11:
				goto IL_283;
			case 12:
				num = 20;
				continue;
			case 13:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 9;
					continue;
				}
				A_0.Read();
				num = 23;
				continue;
			case 14:
				goto IL_139;
			case 15:
				return;
			case 16:
				num = 8;
				continue;
			case 17:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 12;
					continue;
				}
				goto IL_139;
			}
			case 18:
				num = 6;
				continue;
			case 19:
				num = 5;
				continue;
			case 20:
			{
				string localName;
				if (!(localName == ClipboardData.b("ᩨɪᝬ੮ばٲŴᡶ", a_)))
				{
					num = 19;
					continue;
				}
				bool flag = this.ᜂ(A_0);
				num = 27;
				continue;
			}
			case 21:
				goto IL_139;
			case 22:
				num = 15;
				continue;
			case 23:
				goto IL_283;
			case 24:
				num = 7;
				continue;
			case 25:
				goto IL_139;
			case 27:
			{
				bool flag;
				A_1.SizeType = (flag ? CheckBoxSizeType.Auto : CheckBoxSizeType.Exactly);
				num = 10;
				continue;
			}
			case 28:
				goto IL_338;
			}
			if (A_0.IsEmptyElement)
			{
				num = 22;
				continue;
			}
			A_0.MoveToContent();
			localName2 = A_0.LocalName;
			A_0.Read();
			this.ᜀ(A_0);
			num = 28;
			continue;
			IL_139:
			A_0.Read();
			num = 11;
			continue;
			IL_283:
			this.ᜀ(A_0);
			num = 3;
			continue;
			IL_338:
			num = 1;
		}
		IL_17D:;
	}

	// Token: 0x06001ECF RID: 7887 RVA: 0x001F4474 File Offset: 0x001F3474
	private FormField ᜆ(Stream A_0)
	{
		int a_ = 9;
		for (;;)
		{
			IL_67:
			A_0.Position = 0L;
			XmlReader xmlReader = spr\u23D7.ᜀ(A_0);
			if (true)
			{
			}
			int num = 8;
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
						if (!xmlReader.Read())
						{
							num = 5;
							continue;
						}
						num = 7;
						continue;
					case 1:
						num = 9;
						continue;
					case 2:
					{
						string localName;
						if (!(localName == ClipboardData.b("୮ᕰ㽲ᱴѶ൸", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_8B;
					}
					case 3:
						num = 6;
						continue;
					case 4:
						num = 10;
						continue;
					case 5:
						goto IL_12D;
					case 6:
					{
						string localName;
						if (!(localName == ClipboardData.b("᭮ᑰ୲Ŵ㹶᝸୺ࡼ୾", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_D5;
					}
					case 7:
					{
						string localName;
						if ((localName = xmlReader.LocalName) != null)
						{
							num = 4;
							continue;
						}
						goto IL_10F;
					}
					case 8:
						goto IL_10F;
					case 9:
						goto IL_10F;
					case 10:
					{
						string localName;
						if (!(localName == ClipboardData.b("౮ᥰᙲᙴᱶ㭸ᑺռ", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_103;
					}
					case 11:
						goto IL_D0;
					}
					goto IL_67;
					IL_10F:
					num = 0;
					continue;
				}
				IL_D0:
				num = 2;
			}
		}
		IL_8B:
		return new DropDownFormField(this.ᜄ);
		IL_D5:
		return new TextFormField(this.ᜄ);
		IL_103:
		return new CheckBoxFormField(this.ᜄ);
		IL_12D:
		return null;
	}

	// Token: 0x06001ED0 RID: 7888 RVA: 0x001F4614 File Offset: 0x001F3614
	private bool ᜄ()
	{
		for (;;)
		{
			FieldType type = this.\u1712().Type;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (type != FieldType.FieldFormCheckBox)
					{
						num = 1;
						continue;
					}
					goto IL_43;
				case 1:
					num = 3;
					continue;
				case 2:
					goto IL_6B;
				case 3:
					if (type != FieldType.FieldFormDropDown)
					{
						num = 2;
						continue;
					}
					goto IL_43;
				case 4:
					if (type == FieldType.FieldFormTextInput)
					{
						num = 5;
						continue;
					}
					goto IL_9F;
				case 5:
					goto IL_43;
				}
				break;
				IL_6B:
				num = 4;
				continue;
				IL_43:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6B;
				default:
					goto IL_59;
				}
			}
		}
		IL_59:
		if (false)
		{
		}
		return true;
		IL_9F:
		if (true)
		{
		}
		return false;
	}

	// Token: 0x06001ED1 RID: 7889 RVA: 0x001F46CC File Offset: 0x001F36CC
	private void ᜀ(bool A_0)
	{
		int a_ = 10;
		switch (0)
		{
		default:
			for (;;)
			{
				string a_2 = string.Empty;
				int num = 29;
				for (;;)
				{
					string text;
					string text2;
					XmlReader xmlReader;
					bool flag;
					string text3;
					spr\u22A5 spr_u22A;
					DictionaryEntry item;
					Footnote footnote;
					switch (num)
					{
					case 0:
						goto IL_299;
					case 1:
						goto IL_45E;
					case 2:
						if (text != null)
						{
							num = 7;
							continue;
						}
						goto IL_E0;
					case 3:
						if (text2 == ClipboardData.b("䅯", a_))
						{
							num = 32;
							continue;
						}
						goto IL_483;
					case 4:
						num = 28;
						continue;
					case 5:
						this.ᜄ(xmlReader, A_0);
						flag = true;
						num = 1;
						continue;
					case 6:
						if (text != ClipboardData.b("ṯᵱٳ᭵᥷ᙹ", a_))
						{
							num = 24;
							continue;
						}
						goto IL_483;
					case 7:
						num = 20;
						continue;
					case 8:
						text3 = ClipboardData.b("ᕯᱱၳᡵ᝷๹᥻ൽ깿嬨", a_);
						goto IL_3AC;
					case 9:
						if (!(text != string.Empty))
						{
							num = 36;
							continue;
						}
						goto IL_44D;
					case 10:
						num = 13;
						continue;
					case 11:
						num = 9;
						continue;
					case 12:
						if (spr_u22A == null)
						{
							if (true)
							{
							}
							num = 34;
							continue;
						}
						xmlReader = spr\u23D7.ᜀ(spr_u22A.ᜁ());
						xmlReader.MoveToContent();
						text2 = string.Empty;
						text = string.Empty;
						flag = false;
						num = 30;
						continue;
					case 13:
						if (!(text2 == ClipboardData.b("䁯", a_)))
						{
							num = 15;
							continue;
						}
						goto IL_200;
					case 14:
						xmlReader.Read();
						num = 17;
						continue;
					case 15:
						num = 3;
						continue;
					case 16:
						num = 8;
						continue;
					case 17:
						goto IL_326;
					case 18:
						if (text != null)
						{
							num = 11;
							continue;
						}
						goto IL_483;
					case 19:
						goto IL_299;
					case 20:
						if (text != string.Empty)
						{
							num = 5;
							continue;
						}
						goto IL_E0;
					case 21:
						return;
					case 22:
						if (text2 != null)
						{
							num = 4;
							continue;
						}
						goto IL_45E;
					case 23:
						if (xmlReader.EOF)
						{
							num = 21;
							continue;
						}
						goto IL_145;
					case 24:
						goto IL_44D;
					case 25:
						goto IL_45E;
					case 26:
						this.\u1718.Add(item);
						num = 19;
						continue;
					case 27:
						text3 = ClipboardData.b("ᙯᵱ᭳ɵᙷᕹࡻ᭽겁ﲃ", a_);
						goto IL_3AC;
					case 28:
						if (text2 != string.Empty)
						{
							num = 10;
							continue;
						}
						goto IL_45E;
					case 29:
						if (!A_0)
						{
							num = 16;
							continue;
						}
						num = 27;
						continue;
					case 30:
						goto IL_145;
					case 31:
						goto IL_45E;
					case 32:
						goto IL_200;
					case 33:
						if (flag)
						{
							goto IL_326;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_100;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					case 34:
						return;
					case 35:
						if (A_0)
						{
							num = 26;
							continue;
						}
						footnote.FootnoteType = FootnoteType.Endnote;
						this.\u1719.Add(item);
						num = 0;
						continue;
					case 36:
						num = 6;
						continue;
					}
					break;
					IL_100:
					num = 35;
					continue;
					IL_E0:
					footnote = new Footnote(this.ᜄ);
					this.ᜏ(xmlReader, footnote);
					item = new DictionaryEntry(text2, footnote);
					goto IL_100;
					IL_145:
					num = 33;
					continue;
					IL_200:
					num = 18;
					continue;
					IL_299:
					flag = false;
					num = 25;
					continue;
					IL_326:
					text2 = xmlReader.GetAttribute(ClipboardData.b("᥯ᙱ", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱쎳\ud9b5쪷\udeb9첻첽꾿ꇁꇃ뗅믇ꏉꋋ꧍뷏뻑ﯓ跟菡跣裥", a_));
					text = xmlReader.GetAttribute(ClipboardData.b("ѯୱѳ፵", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱쎳\ud9b5쪷\udeb9첻첽꾿ꇁꇃ뗅믇ꏉꋋ꧍뷏뻑ﯓ跟菡跣裥", a_));
					num = 22;
					continue;
					IL_3AC:
					a_2 = text3;
					spr_u22A = this.ᜀ(ClipboardData.b("ݯᵱٳት坷", a_), a_2);
					num = 12;
					continue;
					IL_44D:
					flag = false;
					num = 31;
					continue;
					IL_45E:
					num = 23;
					continue;
					IL_483:
					num = 2;
				}
			}
			return;
		}
	}

	// Token: 0x06001ED2 RID: 7890 RVA: 0x001F4B80 File Offset: 0x001F3B80
	private void ᜄ(XmlReader A_0, bool A_1)
	{
		Stream stream;
		for (;;)
		{
			stream = this.ᜢ(A_0);
			XmlReader xmlReader = spr\u23D7.ᜀ(stream);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!A_1)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_89;
						}
					}
					num = 1;
					continue;
				case 1:
					goto IL_66;
				case 2:
					if (xmlReader.IsEmptyElement)
					{
						num = 3;
						continue;
					}
					num = 0;
					continue;
				case 3:
					return;
				}
				break;
			}
		}
		return;
		IL_66:
		this.ᜄ.FootnoteNodes2010.Add(stream);
		return;
		IL_89:
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜄ.EndnoteNodes2010.Add(stream);
	}

	// Token: 0x06001ED3 RID: 7891 RVA: 0x001F4C38 File Offset: 0x001F3C38
	private Footnote \u171C(XmlReader A_0)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			Footnote footnote;
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("ŧ๩", a_), ClipboardData.b("gṩᡫṭ䩯嵱孳յ᭷ቹ᥻፽ꪃﶏﺑ秊ﶛ펟財쮣풥쾧薩\udbab솭슯횱쒳쒵ힷ\ud9b9\ud9bb춽뎿ꯁ꫃ꇅꗇꛉﳍ崙뗗믙뗛냝", a_));
				string attribute2 = A_0.GetAttribute(ClipboardData.b("୧ὩὫᩭὯά㥳᝵੷ᅹ㩻ᅽﮇ", a_), ClipboardData.b("gṩᡫṭ䩯嵱孳յ᭷ቹ᥻፽ꪃﶏﺑ秊ﶛ펟財쮣풥쾧薩\udbab솭슯횱쒳쒵ힷ\ud9b9\ud9bb춽뎿ꯁ꫃ꇅꗇꛉﳍ崙뗗믙뗛냝", a_));
				bool flag = true;
				string localName = A_0.LocalName;
				int num = 15;
				for (;;)
				{
					bool flag2;
					bool flag3;
					switch (num)
					{
					case 0:
						flag2 = true;
						goto IL_17D;
					case 1:
						num = 13;
						continue;
					case 2:
						num = 10;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F4;
						default:
							if (false)
							{
							}
							goto IL_143;
						}
						break;
					case 4:
						flag3 = true;
						goto IL_1EA;
					case 5:
						if (!(attribute2 == ClipboardData.b("塧", a_)))
						{
							num = 2;
							continue;
						}
						num = 0;
						continue;
					case 6:
						if (attribute2 != null)
						{
							num = 9;
							continue;
						}
						goto IL_143;
					case 7:
						goto IL_20F;
					case 8:
						this.ᜀ(A_0, footnote);
						if (true)
						{
						}
						num = 11;
						continue;
					case 9:
						num = 5;
						continue;
					case 10:
						flag2 = false;
						goto IL_17D;
					case 11:
						goto IL_1C5;
					case 12:
						if (attribute == null)
						{
							num = 7;
							continue;
						}
						num = 6;
						continue;
					case 13:
						flag3 = false;
						goto IL_1EA;
					case 14:
						if (!flag)
						{
							num = 8;
							continue;
						}
						return footnote;
					case 15:
						if (!localName.StartsWith(ClipboardData.b("๧թͫᩭṯᵱs፵", a_)))
						{
							num = 1;
							continue;
						}
						num = 4;
						continue;
					}
					break;
					IL_143:
					bool flag4;
					footnote = this.ᜀ(flag4, attribute);
					num = 14;
					continue;
					IL_17D:
					flag = flag2;
					num = 3;
					continue;
					IL_1F4:
					num = 12;
					continue;
					IL_1EA:
					flag4 = flag3;
					this.\u1717 = flag4;
					goto IL_1F4;
				}
			}
			IL_1C5:
			return footnote;
			IL_20F:
			return null;
		}
		}
	}

	// Token: 0x06001ED4 RID: 7892 RVA: 0x001F4E6C File Offset: 0x001F3E6C
	private Footnote ᜀ(bool A_0, string A_1)
	{
		switch (0)
		{
		default:
		{
			Footnote result;
			for (;;)
			{
				result = null;
				int num = 8;
				for (;;)
				{
					int num2;
					int count;
					DictionaryEntry dictionaryEntry2;
					switch (num)
					{
					case 0:
						goto IL_1B9;
					case 1:
						num = 2;
						continue;
					case 2:
						return result;
					case 3:
						return result;
					case 4:
						return result;
					case 5:
						num2 = 0;
						count = this.ᜑ().Count;
						num = 0;
						continue;
					case 6:
						goto IL_194;
					case 7:
						result = (Footnote)this.ᜑ()[num2].Value;
						num = 4;
						continue;
					case 8:
					{
						if (A_0)
						{
							num = 5;
							continue;
						}
						int num3 = 0;
						int count2 = this.ᜐ().Count;
						num = 6;
						continue;
					}
					case 9:
						goto IL_1C5;
					case 10:
						goto IL_1B9;
					case 11:
					{
						int num3;
						int count2;
						if (num3 >= count2)
						{
							num = 3;
							continue;
						}
						DictionaryEntry dictionaryEntry = this.ᜐ()[num3];
						num = 16;
						continue;
					}
					case 12:
						if (dictionaryEntry2.Key.ToString() == A_1)
						{
							num = 7;
							continue;
						}
						num2++;
						num = 10;
						continue;
					case 13:
						return result;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1C5;
						default:
							if (false)
							{
							}
							goto IL_194;
						}
						break;
					case 15:
					{
						int num3;
						result = (Footnote)this.ᜐ()[num3].Value;
						num = 13;
						continue;
					}
					case 16:
					{
						DictionaryEntry dictionaryEntry;
						if (dictionaryEntry.Key.ToString() == A_1)
						{
							num = 15;
							continue;
						}
						int num3;
						num3++;
						num = 14;
						continue;
					}
					}
					break;
					IL_1C5:
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					dictionaryEntry2 = this.ᜑ()[num2];
					if (true)
					{
					}
					num = 12;
					continue;
					IL_194:
					num = 11;
					continue;
					IL_1B9:
					num = 9;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06001ED5 RID: 7893 RVA: 0x001F50B4 File Offset: 0x001F40B4
	private void ᜀ(XmlReader A_0, Footnote A_1)
	{
		int a_ = 1;
		string attribute;
		string text;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
				for (;;)
				{
					A_1.IsAutoNumbered = false;
					this.ᜀ(A_0);
					this.\u171B(A_0);
					attribute = A_0.GetAttribute(ClipboardData.b("Ŧ٨ժᥬ", a_), ClipboardData.b("སᵨὪᵬ啮幰屲ٴᑶᅸṺၼṾ궂﶐杖漢辠첢힤삦蚨\udcaa슬\uddae햰쎲잴\ud8b6\udab8\udeba캼첾ꣀ귂ꋄ꫆ꗈￌￎ䀹뫖룘닚돜", a_));
					text = A_0.GetAttribute(ClipboardData.b("ѦŨ੪Ὤ", a_), ClipboardData.b("སᵨὪᵬ啮幰屲ٴᑶᅸṺၼṾ궂﶐杖漢辠첢힤삦蚨\udcaa슬\uddae햰쎲잴\ud8b6\udab8\udeba캼첾ꣀ귂ꋄ꫆ꗈￌￎ䀹뫖룘닚돜", a_));
					string text2 = A_0.ReadString();
					int num = 0;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							if (attribute != null)
							{
								num = 1;
								continue;
							}
							goto IL_C6;
						case 1:
							num = 4;
							continue;
						case 2:
							if (text2 != null)
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_09;
								}
								if (false)
								{
								}
								num = 3;
								continue;
							}
							return;
						case 3:
							A_1.CustomMarker = text2;
							num = 6;
							continue;
						case 4:
							if (text != null)
							{
								num = 5;
								continue;
							}
							goto IL_C6;
						case 5:
							goto IL_164;
						case 6:
							return;
						}
						break;
						IL_C6:
						num = 2;
					}
				}
				break;
			}
		}
		IL_164:
		A_1.SymbolFontName = attribute;
		text = text.Replace(ClipboardData.b("Ⅶ奨", a_), string.Empty);
		byte symbolCode = byte.Parse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
		A_1.SymbolCode = symbolCode;
	}

	// Token: 0x06001ED6 RID: 7894 RVA: 0x001F523C File Offset: 0x001F423C
	private void \u171B(XmlReader A_0)
	{
		for (;;)
		{
			for (;;)
			{
				A_0.Read();
				int num;
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
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (A_0.NodeType != XmlNodeType.Whitespace)
						{
							num = 0;
							continue;
						}
						break;
					case 2:
						if (true)
						{
						}
						num = 1;
						continue;
					case 3:
						if (!(A_0.LocalName == string.Empty))
						{
							num = 2;
							continue;
						}
						break;
					}
					break;
				}
			}
		}
	}

	// Token: 0x06001ED7 RID: 7895 RVA: 0x001F52DC File Offset: 0x001F42DC
	private TextRange ᜃ()
	{
		TextRange textRange;
		for (;;)
		{
			textRange = new TextRange(this.ᜄ);
			textRange.Text = '\u0002'.ToString();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u1716 == null)
					{
						return textRange;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_75;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 1:
					return textRange;
				case 2:
					goto IL_75;
				}
				break;
				IL_75:
				textRange.ApplyCharacterFormat(this.\u1716);
				num = 1;
			}
		}
		return textRange;
	}

	// Token: 0x06001ED8 RID: 7896 RVA: 0x001F537C File Offset: 0x001F437C
	private ParagraphBase \u171A(XmlReader A_0)
	{
		int a_ = 18;
		Stream stream;
		for (;;)
		{
			stream = this.ᜢ(A_0);
			bool flag = this.ᜀ(stream, ClipboardData.b("᭷ᕹቻ੽", a_));
			bool flag2 = this.ᜀ(stream, ClipboardData.b("㝷㙹㥻ㅽﲇ", a_));
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_70;
				case 1:
					goto IL_C1;
				case 2:
					if (flag2)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7C;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 3:
					if (flag)
					{
						num = 1;
						continue;
					}
					goto IL_CB;
				}
				break;
			}
		}
		IL_70:
		return this.ᜅ(stream);
		IL_7C:
		stream.Position = 0L;
		return this.ᜃ(stream);
		IL_C1:
		goto IL_7C;
		IL_CB:
		return null;
	}

	// Token: 0x06001ED9 RID: 7897 RVA: 0x001F5458 File Offset: 0x001F4458
	private ParagraphBase ᜅ(Stream A_0)
	{
		int a_ = 9;
		switch (0)
		{
		default:
			for (;;)
			{
				DocOleObject docOleObject = new DocOleObject(this.ᜄ);
				XmlReader xmlReader = spr\u23D7.ᜀ(A_0);
				int num = 15;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 11;
						continue;
					case 1:
						if (true)
						{
						}
						goto IL_173;
					case 2:
						goto IL_173;
					case 3:
						return docOleObject;
					case 4:
						num = 16;
						continue;
					case 5:
						num = 12;
						continue;
					case 6:
						num = 18;
						continue;
					case 7:
						num = 8;
						continue;
					case 8:
					{
						string localName;
						if (!(localName == ClipboardData.b("ᵮᑰၲŴ", a_)))
						{
							num = 4;
							continue;
						}
						goto IL_15A;
					}
					case 9:
						goto IL_173;
					case 10:
						goto IL_A3;
					case 11:
					{
						string localName;
						if (!(localName == ClipboardData.b("⁮㵰㙲㩴ᕶ፸ṺṼ୾", a_)))
						{
							num = 5;
							continue;
						}
						this.ᜀ(xmlReader, docOleObject);
						num = 13;
						continue;
					}
					case 12:
						goto IL_202;
					case 13:
						goto IL_202;
					case 14:
						if (xmlReader.NodeType == XmlNodeType.Element)
						{
							num = 6;
							continue;
						}
						xmlReader.Read();
						num = 1;
						continue;
					case 15:
					{
						if (xmlReader.IsEmptyElement)
						{
							num = 10;
							continue;
						}
						string localName2 = xmlReader.LocalName;
						xmlReader.Read();
						this.ᜀ(xmlReader);
						num = 2;
						continue;
					}
					case 16:
					{
						string localName;
						if (!(localName == ClipboardData.b("ᱮᥰቲմቶ", a_)))
						{
							num = 0;
							continue;
						}
						goto IL_15A;
					}
					case 17:
						goto IL_202;
					case 18:
					{
						string localName;
						if ((localName = xmlReader.LocalName) != null)
						{
							num = 7;
							continue;
						}
						goto IL_202;
					}
					case 19:
					{
						string localName2;
						if (!(xmlReader.LocalName != localName2))
						{
							num = 3;
							continue;
						}
						goto IL_F4;
					}
					}
					break;
					IL_F4:
					num = 14;
					continue;
					IL_202:
					xmlReader.Read();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F4;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					IL_15A:
					this.ᜁ(xmlReader, docOleObject);
					num = 17;
					continue;
					IL_173:
					num = 19;
				}
			}
			IL_A3:
			return null;
		}
	}

	// Token: 0x06001EDA RID: 7898 RVA: 0x001F56E4 File Offset: 0x001F46E4
	private void ᜁ(XmlReader A_0, DocOleObject A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				bool flag;
				string attribute2;
				bool a_2;
				switch (num)
				{
				case 0:
					goto IL_1B2;
				case 2:
					if (!this.ᜋ.StartsWith(ClipboardData.b("ṷᕹ፻੽", a_)))
					{
						num = 14;
						continue;
					}
					goto IL_C9;
				case 3:
					num = 2;
					continue;
				case 4:
					if (!this.ᜋ.StartsWith(ClipboardData.b("ၷόᵻ᩽", a_)))
					{
						num = 3;
						continue;
					}
					goto IL_C9;
				case 5:
					flag = true;
					goto IL_1E7;
				case 6:
				{
					string attribute;
					if (attribute != null)
					{
						num = 19;
						continue;
					}
					goto IL_1C7;
				}
				case 7:
					if (attribute2 != null)
					{
						num = 10;
						continue;
					}
					return;
				case 8:
					goto IL_29F;
				case 9:
					return;
				case 10:
				{
					DocPicture docPicture;
					this.ᜀ(docPicture, attribute2);
					docPicture.IsShape = true;
					num = 0;
					continue;
				}
				case 11:
					if (A_0.IsEmptyElement)
					{
						num = 15;
						continue;
					}
					num = 4;
					continue;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_29F;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 13:
					goto IL_2C9;
				case 14:
					num = 17;
					continue;
				case 15:
					return;
				case 16:
				{
					if (A_0.LocalName != ClipboardData.b("ᅷ᝹ᵻ᥽", a_))
					{
						num = 9;
						continue;
					}
					string attribute = A_0.GetAttribute(ClipboardData.b("ᅷṹ", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹펻\ud8bdꚿꯁꟃꏅ資ꗉ꿋믍뷏럑뫓ꋕ훟췡難菥蓧诩飫蟭鿯鳱蟳黵釷諹迻", a_));
					DocPicture docPicture = new DocPicture(this.ᜄ);
					num = 6;
					continue;
				}
				case 17:
					flag = false;
					goto IL_1E7;
				case 18:
					goto IL_1C7;
				case 19:
				{
					if (true)
					{
					}
					DocPicture docPicture;
					docPicture.ᜀ(A_1);
					string attribute;
					this.ᜀ(docPicture, attribute, a_2, false);
					A_1.ᜀ(docPicture);
					num = 18;
					continue;
				}
				}
				if (A_0.LocalName != ClipboardData.b("୷ቹᵻ๽", a_))
				{
					num = 12;
					continue;
				}
				goto IL_DB;
				IL_C9:
				num = 5;
				continue;
				IL_DB:
				num = 11;
				continue;
				IL_29F:
				if (A_0.LocalName != ClipboardData.b("੷όύ੽", a_))
				{
					num = 13;
					continue;
				}
				goto IL_DB;
				IL_1C7:
				num = 7;
				continue;
				IL_1E7:
				a_2 = flag;
				attribute2 = A_0.GetAttribute(ClipboardData.b("୷๹ջች", a_));
				A_0.ReadToFollowing(ClipboardData.b("ᅷ᝹ᵻ᥽", a_), ClipboardData.b("൷ࡹቻ䑽ﾋꎍﶏﮑ풟辡잣즥얧邩\udaab쎭\udcaf", a_));
				num = 16;
			}
			return;
			IL_1B2:
			return;
			IL_2C9:
			throw new XmlException(ClipboardData.b("㝷㙹㥻幽ꪉ", a_));
		}
		}
	}

	// Token: 0x06001EDB RID: 7899 RVA: 0x001F59FC File Offset: 0x001F49FC
	private void ᜀ(XmlReader A_0, DocOleObject A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 24;
			string attribute3;
			spr\u22A5 spr_u22A;
			byte[] array;
			for (;;)
			{
				string attribute;
				string attribute2;
				string attribute4;
				string attribute5;
				switch (num)
				{
				case 0:
					if (attribute == ClipboardData.b("㑷፹ቻᕽ", a_))
					{
						num = 28;
						continue;
					}
					A_1.ᜀ(OleLinkType.Embed);
					num = 11;
					continue;
				case 1:
					goto IL_2B1;
				case 2:
					if (A_1.LinkType == OleLinkType.Embed)
					{
						num = 6;
						continue;
					}
					goto IL_480;
				case 3:
					if (attribute2 != null)
					{
						num = 19;
						continue;
					}
					goto IL_407;
				case 4:
					if (true)
					{
					}
					num = 0;
					continue;
				case 5:
					goto IL_407;
				case 6:
					spr_u22A = this.ᜨ(attribute3);
					num = 15;
					continue;
				case 7:
					if (attribute2 == ClipboardData.b("㭷ᕹቻ੽", a_))
					{
						goto IL_F2;
					}
					goto IL_407;
				case 8:
					array = new byte[spr_u22A.ᜁ().Length];
					spr_u22A.ᜁ().Read(array, 0, array.Length);
					num = 26;
					continue;
				case 9:
					if (attribute4 != null)
					{
						num = 13;
						continue;
					}
					goto IL_1DD;
				case 10:
					goto IL_1D8;
				case 11:
					goto IL_2FD;
				case 12:
					if (A_1.OleObjectType == OleObjectType.Package)
					{
						num = 16;
						continue;
					}
					goto IL_28D;
				case 13:
					A_1.OleStorageName = attribute4.Replace(ClipboardData.b("❷", a_), string.Empty);
					num = 18;
					continue;
				case 14:
					A_1.DisplayAsIcon = false;
					num = 5;
					continue;
				case 15:
					if (spr_u22A == null)
					{
						num = 23;
						continue;
					}
					array = null;
					spr_u22A.ᜁ().Position = 0L;
					A_1.ᜑ = spr_u22A.ᜁ();
					num = 17;
					continue;
				case 16:
				{
					sprḴ sprḴ;
					A_1.ᜀ(sprḴ.ᜀ(), sprḴ.ᜁ());
					num = 10;
					continue;
				}
				case 17:
				{
					if (this.ᜀ(A_1.OleObjectType))
					{
						num = 8;
						continue;
					}
					spr\u20BF spr_u20BF = new spr\u20BF(spr_u22A.ᜁ());
					sprḴ sprḴ = new sprḴ();
					sprḴ.ᜂ(spr_u20BF.ᜇ());
					array = sprḴ.ᜃ();
					num = 12;
					continue;
				}
				case 18:
					goto IL_1DD;
				case 19:
					num = 7;
					continue;
				case 20:
					if (attribute5 != null)
					{
						num = 27;
						continue;
					}
					goto IL_2B1;
				case 21:
					if (attribute != null)
					{
						num = 4;
						continue;
					}
					goto IL_2FD;
				case 22:
					goto IL_C7;
				case 23:
					return;
				case 25:
					goto IL_2FD;
				case 26:
					goto IL_14F;
				case 27:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F2;
					default:
						if (false)
						{
						}
						A_1.ObjectType = attribute5;
						num = 1;
						continue;
					}
					break;
				case 28:
					A_1.ᜀ(OleLinkType.Link);
					num = 25;
					continue;
				}
				if (A_0.LocalName != ClipboardData.b("㝷㙹㥻ㅽﲇ", a_))
				{
					num = 22;
					continue;
				}
				attribute = A_0.GetAttribute(ClipboardData.b("ⱷ͹౻᭽", a_));
				num = 21;
				continue;
				IL_F2:
				num = 14;
				continue;
				IL_1DD:
				attribute2 = A_0.GetAttribute(ClipboardData.b("㱷ࡹᵻॽ셿ﺉ", a_));
				num = 3;
				continue;
				IL_2B1:
				attribute4 = A_0.GetAttribute(ClipboardData.b("㝷᡹ᙻ᭽춃슅", a_));
				num = 9;
				continue;
				IL_2FD:
				attribute5 = A_0.GetAttribute(ClipboardData.b("⡷ࡹ፻᥽쥿욁", a_));
				num = 20;
				continue;
				IL_407:
				attribute3 = A_0.GetAttribute(ClipboardData.b("ᅷṹ", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹펻\ud8bdꚿꯁꟃꏅ資ꗉ꿋믍뷏럑뫓ꋕ훟췡難菥蓧诩飫蟭鿯鳱蟳黵釷諹迻", a_));
				num = 2;
			}
			IL_C7:
			throw new XmlException(ClipboardData.b("㝷㙹㥻幽콿ﺉ겋", a_));
			IL_14F:
			IL_1D8:
			IL_28D:
			spr_u22A.ᜁ().Position = 0L;
			A_1.ᜀ(array);
			A_1.ᜀ(array, string.Empty);
			return;
			IL_480:
			A_1.LinkPath = this.ᜧ(attribute3);
			return;
		}
		}
	}

	// Token: 0x06001EDC RID: 7900 RVA: 0x001F5E98 File Offset: 0x001F4E98
	private spr\u22A5 ᜨ(string A_0)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 7;
			string text;
			for (;;)
			{
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
					if (flag)
					{
						num = 9;
						continue;
					}
					text = this.ᜎ[A_0].Value.ToString();
					num = 11;
					continue;
				case 1:
					flag2 = true;
					goto IL_172;
				case 2:
					num = 10;
					continue;
				case 3:
					if (text == null)
					{
						num = 8;
						continue;
					}
					goto IL_1A7;
				case 4:
					goto IL_155;
				case 5:
					flag2 = false;
					goto IL_172;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_135;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 8:
					goto IL_170;
				case 9:
				{
					Dictionary<string, DictionaryEntry> dictionary = this.ᜅ(this.ᜋ);
					text = (string)dictionary[A_0].Value;
					num = 4;
					continue;
				}
				case 10:
					if (!this.ᜋ.StartsWith(ClipboardData.b("ၵ᝷ᕹࡻ᭽", a_)))
					{
						goto IL_135;
					}
					goto IL_198;
				case 11:
					goto IL_155;
				}
				if (!this.ᜋ.StartsWith(ClipboardData.b("ṵᵷ᭹᡻᭽", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_198;
				IL_135:
				if (true)
				{
				}
				num = 6;
				continue;
				IL_155:
				num = 3;
				continue;
				IL_172:
				flag = flag2;
				text = null;
				num = 0;
				continue;
				IL_198:
				num = 1;
			}
			IL_170:
			return null;
			IL_1A7:
			text = text.Replace(ClipboardData.b("፵ᕷ᡹᥻᩽ﮇꖉ", a_), null);
			return this.ᜀ(ClipboardData.b("ŵ᝷ࡹ᡻兽뮓", a_), text);
		}
		}
	}

	// Token: 0x06001EDD RID: 7901 RVA: 0x001F6078 File Offset: 0x001F5078
	private string ᜧ(string A_0)
	{
		int a_ = 5;
		string text;
		for (;;)
		{
			bool flag = false;
			text = null;
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_48;
				case 1:
					if (!flag)
					{
						num = 2;
						continue;
					}
					goto IL_48;
				case 2:
					goto IL_44;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_44;
					default:
						if (false)
						{
						}
						if (text == null)
						{
							num = 4;
							continue;
						}
						goto IL_AA;
					}
					break;
				case 4:
					goto IL_77;
				}
				break;
				IL_44:
				text = this.ᜎ[A_0].Value.ToString();
				num = 0;
				continue;
				IL_48:
				num = 3;
			}
		}
		IL_77:
		return null;
		IL_AA:
		return text.Replace(ClipboardData.b("൪Ѭͮᑰ䥲婴塶", a_), string.Empty);
	}

	// Token: 0x06001EDE RID: 7902 RVA: 0x001F6148 File Offset: 0x001F5148
	private bool ᜀ(OleObjectType A_0)
	{
		bool result;
		for (;;)
		{
			result = false;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 != OleObjectType.ExcelWorksheet)
					{
						num = 9;
						continue;
					}
					goto IL_172;
				case 1:
					if (A_0 != OleObjectType.Excel_97_2003_Worksheet)
					{
						num = 28;
						continue;
					}
					goto IL_172;
				case 2:
					if (A_0 != OleObjectType.ExcelBinaryWorksheet)
					{
						num = 7;
						continue;
					}
					goto IL_172;
				case 3:
					if (A_0 != OleObjectType.PowerPointPresentation)
					{
						if (true)
						{
						}
						num = 16;
						continue;
					}
					goto IL_172;
				case 4:
					num = 19;
					continue;
				case 5:
					num = 22;
					continue;
				case 6:
					if (A_0 != OleObjectType.PowerPoint_97_2003_Presentation)
					{
						num = 29;
						continue;
					}
					goto IL_172;
				case 7:
					num = 17;
					continue;
				case 8:
					if (A_0 != OleObjectType.PowerPointSlide)
					{
						num = 14;
						continue;
					}
					goto IL_172;
				case 9:
					num = 6;
					continue;
				case 10:
					num = 3;
					continue;
				case 11:
					if (A_0 != OleObjectType.PowerPoint_97_2003_Slide)
					{
						num = 4;
						continue;
					}
					goto IL_172;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1AE;
					default:
						if (false)
						{
						}
						num = 20;
						continue;
					}
					break;
				case 13:
					num = 27;
					continue;
				case 14:
					num = 21;
					continue;
				case 15:
					goto IL_172;
				case 16:
					num = 8;
					continue;
				case 17:
					if (A_0 != OleObjectType.ExcelChart)
					{
						num = 30;
						continue;
					}
					goto IL_172;
				case 18:
					if (A_0 != OleObjectType.PowerPointMacroSlide)
					{
						num = 10;
						continue;
					}
					goto IL_172;
				case 19:
					if (A_0 != OleObjectType.PowerPointMacroPresentation)
					{
						num = 24;
						continue;
					}
					goto IL_172;
				case 20:
					if (A_0 != OleObjectType.Word_97_2003_Document)
					{
						num = 5;
						continue;
					}
					goto IL_172;
				case 21:
					if (A_0 != OleObjectType.VisioDrawing)
					{
						num = 12;
						continue;
					}
					goto IL_172;
				case 22:
					if (A_0 != OleObjectType.WordDocument)
					{
						num = 13;
						continue;
					}
					goto IL_172;
				case 23:
					if (A_0 != OleObjectType.ExcelMacroWorksheet)
					{
						num = 25;
						continue;
					}
					goto IL_172;
				case 24:
					num = 18;
					continue;
				case 25:
					goto IL_1AE;
				case 26:
					return result;
				case 27:
					if (A_0 == OleObjectType.WordMacroDocument)
					{
						num = 15;
						continue;
					}
					return result;
				case 28:
					num = 2;
					continue;
				case 29:
					num = 11;
					continue;
				case 30:
					num = 23;
					continue;
				}
				break;
				IL_172:
				result = true;
				num = 26;
				continue;
				IL_1AE:
				num = 0;
			}
		}
		return result;
	}

	// Token: 0x06001EDF RID: 7903 RVA: 0x001F6408 File Offset: 0x001F5408
	private bool ᜀ(Stream A_0, string A_1)
	{
		for (;;)
		{
			IL_1C:
			XmlReader xmlReader;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_4F:
				goto IL_5B;
			default:
				if (false)
				{
				}
				A_0.Position = 0L;
				xmlReader = spr\u23D7.ᜀ(A_0);
				num = 1;
				break;
			}
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					if (!xmlReader.Read())
					{
						num = 4;
						continue;
					}
					num = 3;
					continue;
				case 1:
					goto IL_4F;
				case 2:
					goto IL_AB;
				case 3:
					if (xmlReader.LocalName == A_1)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					goto IL_5B;
				case 4:
					goto IL_73;
				}
				goto IL_1C;
			}
			IL_5B:
			num = 0;
			goto IL_02;
		}
		IL_73:
		A_0.Position = 0L;
		return false;
		IL_AB:
		A_0.Position = 0L;
		return true;
	}

	// Token: 0x06001EE0 RID: 7904 RVA: 0x001F64CC File Offset: 0x001F54CC
	private ParagraphBase ᜀ(XmlReader A_0, ParagraphItemCollection A_1, MemoryStream A_2)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 4;
			MemoryStream memoryStream;
			XmlReader a_2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6B;
				case 1:
					goto IL_147;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B8;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 3:
					goto IL_B8;
				}
				if (A_0.LocalName != ClipboardData.b("ᡧͩཫᩭ", a_))
				{
					num = 0;
					continue;
				}
				memoryStream = this.ᜢ(A_0);
				memoryStream.Position = 0L;
				Spire.Doc.Documents.Converters.ShapeType shapeType = this.ᜀ(memoryStream);
				memoryStream.Position = 0L;
				a_2 = spr\u23D7.ᜀ(memoryStream);
				Spire.Doc.Documents.Converters.ShapeType shapeType2 = shapeType;
				num = 3;
				continue;
				IL_B8:
				switch (shapeType2)
				{
				case Spire.Doc.Documents.Converters.ShapeType.TextboxShape:
					goto IL_132;
				case Spire.Doc.Documents.Converters.ShapeType.GroupedShape:
					goto IL_12A;
				case Spire.Doc.Documents.Converters.ShapeType.PictureShape:
					goto IL_7A;
				case Spire.Doc.Documents.Converters.ShapeType.WatermarkShape:
					goto IL_70;
				case Spire.Doc.Documents.Converters.ShapeType.OleObject:
					goto IL_105;
				default:
					num = 2;
					break;
				}
			}
			IL_6B:
			throw new XmlException(ClipboardData.b("ᡧͩཫᩭկqᅳ噵୷ቹᵻ๽ꊁ", a_));
			IL_70:
			this.ᜀ(memoryStream, A_1);
			return null;
			IL_7A:
			return this.ᜁ(memoryStream);
			IL_105:
			memoryStream.Position = 0L;
			return this.ᜀ(a_2, memoryStream);
			IL_12A:
			return this.ᜃ(memoryStream);
			IL_132:
			return this.ᜀ(memoryStream, A_2);
			IL_147:
			sprᩍ a_3 = this.ᜄ(memoryStream);
			memoryStream.Position = 0L;
			spr\u24D5 spr_u24D = this.ᜃ(memoryStream);
			spr_u24D.ᜀ(a_3);
			return spr_u24D;
		}
		}
	}

	// Token: 0x06001EE1 RID: 7905 RVA: 0x001F6648 File Offset: 0x001F5648
	private sprᩍ ᜄ(MemoryStream A_0)
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
		A_0.Position = 0L;
		return spr\u1DE1.ᜂ(new sprᨉ(A_0, this.ᜄ, this));
	}

	// Token: 0x06001EE2 RID: 7906 RVA: 0x001F66A4 File Offset: 0x001F56A4
	private ParagraphBase ᜃ(MemoryStream A_0)
	{
		int a_ = 8;
		for (;;)
		{
			IL_25:
			A_0.Position = 0L;
			XmlReader xmlReader = spr\u23D7.ᜀ(A_0);
			for (;;)
			{
				IL_34:
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7B;
					case 1:
						if (xmlReader.EOF)
						{
							num = 0;
							continue;
						}
						goto IL_DB;
					case 2:
						xmlReader.ReadToFollowing(ClipboardData.b("७ɯᵱųٵ", a_), ClipboardData.b("᭭ɯᱱ乳յ᭷ቹ᥻፽ꦃﺋ﶑떗蓮骟풡즣쪥", a_));
						num = 3;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_34;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							goto IL_63;
						}
						break;
					case 4:
						if (xmlReader.LocalName != ClipboardData.b("७ɯᵱųٵ", a_))
						{
							num = 2;
							continue;
						}
						goto IL_63;
					}
					goto IL_25;
					IL_63:
					num = 1;
				}
			}
		}
		IL_7B:
		return null;
		IL_DB:
		sprᩍ a_2 = this.ᜄ(A_0);
		A_0.Position = 0L;
		spr\u24D5 spr_u24D = this.ᜃ(A_0);
		spr_u24D.ᜀ(a_2);
		return spr_u24D;
	}

	// Token: 0x06001EE3 RID: 7907 RVA: 0x001F67AC File Offset: 0x001F57AC
	private void ᜀ(MemoryStream A_0, ParagraphItemCollection A_1)
	{
		int a_ = 14;
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_ED;
			case 2:
			{
				string text;
				if (text.StartsWith(ClipboardData.b("⑳᥵ཷό๻⹽톅ﺉﲍ\udd8fﶕ힗ﮝ쎟횡", a_)))
				{
					num = 3;
					continue;
				}
				num = 12;
				continue;
			}
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_ED;
				default:
					if (false)
					{
					}
					this.ᜄ(A_0);
					num = 1;
					continue;
				}
				break;
			case 4:
				this.ᜂ(A_0);
				num = 9;
				continue;
			case 5:
			{
				HeaderFooter headerFooter = A_1.OwnerBase.OwnerBase as HeaderFooter;
				num = 8;
				continue;
			}
			case 6:
				if (true)
				{
				}
				if (A_1.OwnerBase.OwnerBase is HeaderFooter)
				{
					num = 5;
					continue;
				}
				return;
			case 7:
			{
				HeaderFooter headerFooter;
				headerFooter.WriteWatermark = true;
				num = 0;
				continue;
			}
			case 8:
			{
				HeaderFooter headerFooter;
				if (headerFooter != null)
				{
					num = 7;
					continue;
				}
				return;
			}
			case 9:
				goto IL_6C;
			case 11:
			{
				string text = this.ᜀ(A_0, ClipboardData.b("ݳṵ᥷੹᥻", a_), ClipboardData.b("ᵳት", a_), null);
				num = 2;
				continue;
			}
			case 12:
			{
				string text;
				if (text.StartsWith(ClipboardData.b("⍳᥵੷ṹⱻ᝽\udd89揄煉", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_6C;
			}
			}
			if (this.ᜄ.Watermark.Type == WatermarkType.NoWatermark)
			{
				num = 11;
				continue;
			}
			IL_6C:
			num = 6;
			continue;
			IL_ED:
			goto IL_6C;
		}
	}

	// Token: 0x06001EE4 RID: 7908 RVA: 0x001F697C File Offset: 0x001F597C
	private void ᜂ(MemoryStream A_0)
	{
		int a_ = 3;
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			PictureWatermark pictureWatermark;
			for (;;)
			{
				this.ᜄ.ᜀ(WatermarkType.PictureWatermark);
				pictureWatermark = (this.ᜄ.Watermark as PictureWatermark);
				string a_2 = this.ᜀ(A_0, ClipboardData.b("h٪౬࡮ᑰᝲᑴͶᡸ", a_), ClipboardData.b("hཪ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪슬즮ힰ\udab2횴튶ﶸ풺\udebc쪾곀ꛂꯄ돆流﷌ￎﳒ꟔닖뗘뫚꧜뛞軠跢雤迦胨鯪黬", a_));
				DocPicture a_3 = new DocPicture(this.ᜄ);
				this.ᜀ(a_3, a_2, true, false);
				pictureWatermark.WordPicture = a_3;
				string text = this.ᜀ(A_0, ClipboardData.b("h٪౬࡮ᑰᝲᑴͶᡸ", a_), ClipboardData.b("๨੪ѬŮ", a_), null);
				string text2 = this.ᜀ(A_0, ClipboardData.b("h٪౬࡮ᑰᝲᑴͶᡸ", a_), ClipboardData.b("୨ݪ౬౮ᩰὲၴŶᱸ᝺", a_), null);
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_123;
					case 1:
						if (text2 == null)
						{
							num = 2;
							continue;
						}
						goto IL_169;
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
							pictureWatermark.IsWashout = false;
							num = 0;
							continue;
						}
						break;
					case 3:
						num = 1;
						continue;
					case 4:
						if (text == null)
						{
							num = 3;
							continue;
						}
						goto IL_169;
					}
					break;
				}
			}
			IL_123:
			IL_169:
			A_0.Position = 0L;
			XmlReader a_4 = spr\u23D7.ᜀ(A_0);
			this.ᜀ(a_4, pictureWatermark);
			return;
		}
		}
	}

	// Token: 0x06001EE5 RID: 7909 RVA: 0x001F6B0C File Offset: 0x001F5B0C
	private void ᜀ(XmlReader A_0, PictureWatermark A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 14;
			for (;;)
			{
				string localName2;
				int num4;
				switch (num)
				{
				case 0:
					goto IL_C8;
				case 1:
				{
					float num2;
					A_1.WordPicture.Height = num2;
					float num3;
					A_1.WordPicture.Width = num3;
					num = 31;
					continue;
				}
				case 2:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 21;
						continue;
					}
					goto IL_157;
				}
				case 3:
					return;
				case 4:
					goto IL_38C;
				case 5:
				{
					float num3;
					if (num3 != 3.4028235E+38f)
					{
						goto IL_DC;
					}
					goto IL_157;
				}
				case 6:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 17;
						continue;
					}
					return;
				case 7:
					if (!(A_0.LocalName != localName2))
					{
						num = 15;
						continue;
					}
					num = 8;
					continue;
				case 8:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 12;
						continue;
					}
					A_0.Read();
					num = 16;
					continue;
				case 9:
				{
					string attribute = A_0.GetAttribute(ClipboardData.b("୷๹ջች", a_));
					num = 13;
					continue;
				}
				case 10:
					goto IL_38C;
				case 11:
				{
					string localName;
					if (localName == ClipboardData.b("୷ቹᵻ๽", a_))
					{
						num = 9;
						continue;
					}
					goto IL_157;
				}
				case 12:
					num = 2;
					continue;
				case 13:
				{
					string attribute;
					if (attribute == null)
					{
						num = 23;
						continue;
					}
					float num3 = float.MaxValue;
					float num2 = float.MaxValue;
					string[] array = attribute.Split(new char[]
					{
						';'
					});
					string text = null;
					num4 = 0;
					int num5 = array.Length;
					num = 4;
					continue;
				}
				case 15:
					return;
				case 16:
					goto IL_1BA;
				case 17:
					num = 7;
					continue;
				case 18:
				{
					string text = text.Replace(ClipboardData.b("ཷ፹᡻੽뢁", a_), string.Empty);
					float num3 = this.ᜎ(text);
					num = 26;
					continue;
				}
				case 19:
					goto IL_1BA;
				case 20:
				{
					string text = text.Replace(ClipboardData.b("ၷόᕻ᥽뺃", a_), string.Empty);
					float num2 = this.ᜎ(text);
					num = 29;
					continue;
				}
				case 21:
					num = 11;
					continue;
				case 22:
				{
					int num5;
					if (num4 >= num5)
					{
						num = 0;
						continue;
					}
					string[] array;
					string text = array[num4];
					num = 25;
					continue;
				}
				case 23:
					return;
				case 24:
				{
					float num2;
					if (num2 != 3.4028235E+38f)
					{
						num = 1;
						continue;
					}
					goto IL_157;
				}
				case 25:
				{
					string text;
					if (!text.StartsWith(ClipboardData.b("ཷ፹᡻੽뢁", a_)))
					{
						num = 30;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DC;
					default:
						if (false)
						{
						}
						num = 18;
						continue;
					}
					break;
				}
				case 26:
					goto IL_407;
				case 27:
					num = 24;
					continue;
				case 28:
					goto IL_1BA;
				case 29:
					goto IL_C8;
				case 30:
				{
					string text;
					if (text.StartsWith(ClipboardData.b("ၷόᕻ᥽뺃", a_)))
					{
						num = 20;
						continue;
					}
					goto IL_407;
				}
				case 31:
					goto IL_157;
				}
				if (A_0.IsEmptyElement)
				{
					num = 3;
					continue;
				}
				localName2 = A_0.LocalName;
				num = 19;
				continue;
				IL_C8:
				num = 5;
				continue;
				IL_DC:
				num = 27;
				continue;
				IL_157:
				A_0.Read();
				num = 28;
				continue;
				IL_1BA:
				if (true)
				{
				}
				num = 6;
				continue;
				IL_38C:
				num = 22;
				continue;
				IL_407:
				num4++;
				num = 10;
			}
			return;
		}
		}
	}

	// Token: 0x06001EE6 RID: 7910 RVA: 0x001F6F38 File Offset: 0x001F5F38
	private void ᜄ(Stream A_0)
	{
		int a_ = 18;
		TextWatermark textWatermark;
		for (;;)
		{
			this.ᜄ.ᜀ(WatermarkType.TextWatermark);
			textWatermark = (this.ᜄ.Watermark as TextWatermark);
			string text = this.ᜀ(A_0, ClipboardData.b("ṷ፹ၻች", a_), ClipboardData.b("᝷੹ᵻᵽﶃ", a_), null);
			int num = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				case 1:
					goto IL_2B;
				default:
					goto IL_2B;
				}
				IL_98:
				if (text == null)
				{
					num = 2;
					continue;
				}
				goto IL_BC;
				IL_2B:
				if (false)
				{
				}
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_98;
				case 1:
					goto IL_BA;
				case 2:
					textWatermark.Semitransparent = false;
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_BA:
		IL_BC:
		XmlReader a_2 = spr\u23D7.ᜀ(A_0);
		this.ᜁ(a_2, textWatermark);
	}

	// Token: 0x06001EE7 RID: 7911 RVA: 0x001F7010 File Offset: 0x001F6010
	private void ᜁ(XmlReader A_0, TextWatermark A_1)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 13;
			for (;;)
			{
				string localName2;
				switch (num)
				{
				case 0:
					goto IL_226;
				case 1:
					A_1.Color = Color.Empty;
					num = 24;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_38E;
					default:
					{
						if (false)
						{
						}
						string localName;
						if (!(localName == ClipboardData.b("ṬݮၰͲၴ", a_)))
						{
							num = 9;
							continue;
						}
						string attribute = A_0.GetAttribute(ClipboardData.b("୬ٮᵰὲᙴᡶᕸᑺོ", a_));
						num = 14;
						continue;
					}
					}
					break;
				case 3:
					goto IL_1C2;
				case 4:
					num = 2;
					continue;
				case 5:
					if (A_0.GetAttribute(ClipboardData.b("Ṭ᭮Ͱᩲ᭴ၶ", a_)) != null)
					{
						num = 17;
						continue;
					}
					goto IL_1C2;
				case 6:
					return;
				case 7:
					goto IL_126;
				case 8:
					if (A_0.GetAttribute(ClipboardData.b("Ṭ᭮ࡰὲၴ", a_)) != null)
					{
						num = 19;
						continue;
					}
					goto IL_226;
				case 9:
					num = 0;
					continue;
				case 10:
					goto IL_226;
				case 11:
					num = 21;
					continue;
				case 12:
					num = 23;
					continue;
				case 14:
				{
					string attribute;
					if (attribute == null)
					{
						goto IL_38E;
					}
					if (true)
					{
					}
					num = 20;
					continue;
				}
				case 15:
					goto IL_23B;
				case 16:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 11;
						continue;
					}
					A_0.Read();
					num = 26;
					continue;
				case 17:
					A_1.Text = A_0.GetAttribute(ClipboardData.b("Ṭ᭮Ͱᩲ᭴ၶ", a_));
					num = 3;
					continue;
				case 18:
					if (!(A_0.LocalName != localName2))
					{
						num = 6;
						continue;
					}
					num = 16;
					continue;
				case 19:
				{
					int a_2 = A_1.ShapeHeightInPixels;
					int a_3 = A_1.ShapeWidthInPixels;
					this.ᜀ(A_0, A_1, a_2, a_3);
					num = 25;
					continue;
				}
				case 20:
				{
					string attribute;
					if (attribute == ClipboardData.b("౬ᩮհᱲ", a_))
					{
						num = 1;
						continue;
					}
					A_1.Color = this.ᜃ(attribute);
					num = 7;
					continue;
				}
				case 21:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 12;
						continue;
					}
					goto IL_226;
				}
				case 22:
					goto IL_23B;
				case 23:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᥬ੮॰ݲմᙶ൸፺", a_)))
					{
						num = 4;
						continue;
					}
					num = 5;
					continue;
				}
				case 24:
					goto IL_126;
				case 25:
					goto IL_226;
				case 26:
					goto IL_23B;
				case 27:
					return;
				case 28:
					return;
				}
				if (A_0.IsEmptyElement)
				{
					num = 27;
					continue;
				}
				localName2 = A_0.LocalName;
				A_0.Read();
				num = 22;
				continue;
				IL_126:
				this.ᜀ(A_0, A_1);
				num = 10;
				continue;
				IL_1C2:
				num = 8;
				continue;
				IL_226:
				A_0.Read();
				num = 15;
				continue;
				IL_23B:
				num = 18;
				continue;
				IL_38E:
				num = 28;
			}
			return;
		}
		}
	}

	// Token: 0x06001EE8 RID: 7912 RVA: 0x001F73BC File Offset: 0x001F63BC
	private void ᜀ(XmlReader A_0, TextWatermark A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("ᩨὪᑬͮᑰ", a_));
				int num = 4;
				for (;;)
				{
					string text;
					int num2;
					switch (num)
					{
					case 0:
						goto IL_200;
					case 1:
						return;
					case 2:
						if (text.StartsWith(ClipboardData.b("Ũ๪Ѭ࡮ᥰݲ佴", a_)))
						{
							num = 6;
							continue;
						}
						num = 9;
						continue;
					case 3:
						if (!text.StartsWith(ClipboardData.b("Ṩɪ६᭮ᥰ䥲", a_)))
						{
							num = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_13E;
						default:
							if (false)
							{
							}
							num = 15;
							continue;
						}
						break;
					case 4:
					{
						if (attribute.Length == 0)
						{
							num = 17;
							continue;
						}
						string[] array = attribute.Split(new char[]
						{
							';'
						});
						text = null;
						bool flag = true;
						num2 = 0;
						int num3 = array.Length;
						num = 0;
						continue;
					}
					case 5:
						goto IL_A2;
					case 6:
						if (true)
						{
						}
						text = text.Replace(ClipboardData.b("Ũ๪Ѭ࡮ᥰݲ佴", a_), string.Empty);
						A_1.ShapeHeightInPixels = (int)(this.ᜎ(text) * 20f);
						num = 5;
						continue;
					case 7:
						goto IL_A2;
					case 8:
						A_1.Layout = WatermarkLayout.Horizontal;
						num = 1;
						continue;
					case 9:
						if (text.StartsWith(ClipboardData.b("᭨Ѫᥬ๮հᩲᩴ᥶", a_)))
						{
							num = 16;
							continue;
						}
						goto IL_A2;
					case 10:
						goto IL_200;
					case 11:
						goto IL_A2;
					case 12:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 14;
							continue;
						}
						string[] array;
						text = array[num2];
						num = 3;
						continue;
					}
					case 13:
					{
						bool flag;
						if (flag)
						{
							num = 8;
							continue;
						}
						return;
					}
					case 14:
						num = 13;
						continue;
					case 15:
						goto IL_13E;
					case 16:
					{
						bool flag = false;
						num = 7;
						continue;
					}
					case 17:
						return;
					}
					break;
					IL_A2:
					num2++;
					num = 10;
					continue;
					IL_13E:
					text = text.Replace(ClipboardData.b("Ṩɪ६᭮ᥰ䥲", a_), string.Empty);
					A_1.ShapeWidthInPixels = (int)(this.ᜎ(text) * 20f);
					num = 11;
					continue;
					IL_200:
					num = 12;
				}
			}
			return;
		}
	}

	// Token: 0x06001EE9 RID: 7913 RVA: 0x001F7678 File Offset: 0x001F6678
	private void ᜀ(XmlReader A_0, TextWatermark A_1, int A_2, int A_3)
	{
		int a_ = 2;
		switch (0)
		{
		default:
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("᭧ṩᕫɭᕯ", a_));
				int num = 4;
				for (;;)
				{
					string text;
					string text3;
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
					{
						string[] array;
						text = array[1];
						goto IL_1A0;
					}
					case 2:
						return;
					case 3:
						text = string.Empty;
						goto IL_1A0;
					case 4:
					{
						if (true)
						{
						}
						if (attribute.Length == 0)
						{
							num = 11;
							continue;
						}
						string[] array = attribute.Split(new char[]
						{
							';'
						});
						string text2 = array[0];
						text2 = text2.Replace(ClipboardData.b("䩧", a_), string.Empty);
						A_1.FontName = text2.Replace(ClipboardData.b("๧թɫᩭ嵯ᑱᕳ᭵ᅷᙹջ䑽", a_), string.Empty);
						num = 8;
						continue;
					}
					case 5:
						num = 12;
						continue;
					case 6:
						A_1.FontSize = 144f;
						num = 10;
						continue;
					case 7:
						goto IL_E4;
					case 8:
					{
						string[] array;
						if (array.Length != 2)
						{
							num = 0;
							continue;
						}
						num = 1;
						continue;
					}
					case 9:
						if (text3 != string.Empty)
						{
							num = 5;
							continue;
						}
						return;
					case 10:
						goto IL_E4;
					case 11:
						return;
					case 12:
						if (text3 == ClipboardData.b("๧թɫᩭ嵯űᵳ౵ᵷ䁹乻᝽", a_))
						{
							num = 6;
							continue;
						}
						text3 = text3.Replace(ClipboardData.b("๧թɫᩭ嵯űᵳ౵ᵷ䁹", a_), string.Empty);
						A_1.FontSize = this.ᜎ(text3);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					}
					break;
					IL_E4:
					A_1.ShapeHeightInPixels = A_2;
					A_1.ShapeWidthInPixels = A_3;
					num = 2;
					continue;
					IL_1A0:
					text3 = text;
					num = 9;
				}
			}
			return;
		}
	}

	// Token: 0x06001EEA RID: 7914 RVA: 0x001F78A8 File Offset: 0x001F68A8
	private string ᜀ(Stream A_0, string A_1, string A_2, string A_3)
	{
		string attribute;
		for (;;)
		{
			A_0.Position = 0L;
			XmlReader xmlReader = spr\u23D7.ᜀ(A_0);
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_64;
				case 1:
					attribute = xmlReader.GetAttribute(A_2, A_3);
					num = 2;
					continue;
				case 2:
					if (attribute != null)
					{
						num = 0;
						continue;
					}
					goto IL_89;
				case 3:
					goto IL_D5;
				case 4:
					goto IL_89;
				case 5:
					if (xmlReader.LocalName == A_1)
					{
						num = 1;
						continue;
					}
					goto IL_89;
				case 6:
					if (xmlReader.Read())
					{
						num = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_64;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				break;
				IL_89:
				if (true)
				{
				}
				num = 6;
			}
		}
		IL_64:
		A_0.Position = 0L;
		return attribute;
		IL_D5:
		A_0.Position = 0L;
		return null;
	}

	// Token: 0x06001EEB RID: 7915 RVA: 0x001F7998 File Offset: 0x001F6998
	private DocPicture ᜁ(MemoryStream A_0)
	{
		XmlReader a_;
		DocPicture docPicture;
		for (;;)
		{
			a_ = spr\u23D7.ᜀ(A_0);
			docPicture = new DocPicture(this.ᜄ);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8B;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2F;
					default:
						if (false)
						{
						}
						docPicture.PictureCharacterFormat.ImportContainer(this.\u1716);
						if (true)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_2F;
				}
				break;
				IL_2F:
				if (this.\u1716 == null)
				{
					goto IL_8D;
				}
				num = 1;
			}
		}
		IL_8B:
		IL_8D:
		this.\u1716 = null;
		this.ᜋ(a_, docPicture);
		return docPicture;
	}

	// Token: 0x06001EEC RID: 7916 RVA: 0x001F7A44 File Offset: 0x001F6A44
	private void ᜋ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 8;
		int num = 5;
		for (;;)
		{
			string localName2;
			switch (num)
			{
			case 0:
				goto IL_12A;
			case 1:
				return;
			case 2:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 9;
					continue;
				}
				goto IL_66;
			}
			case 3:
				goto IL_12A;
			case 4:
				goto IL_66;
			case 6:
				if (!(A_0.LocalName != localName2))
				{
					num = 1;
					continue;
				}
				num = 11;
				continue;
			case 7:
			{
				string localName;
				if (localName == ClipboardData.b("ᵭᡯ፱ѳ፵", a_))
				{
					num = 8;
					continue;
				}
				goto IL_66;
			}
			case 8:
			{
				this.ᜉ(A_0, A_1);
				DocPicture docPicture = A_1 as DocPicture;
				docPicture.IsShape = true;
				this.ᜊ(A_0, A_1);
				num = 4;
				continue;
			}
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CC;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 10:
				return;
			case 11:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 13;
					continue;
				}
				A_0.Read();
				num = 0;
				continue;
			case 12:
				goto IL_12A;
			case 13:
				num = 2;
				continue;
			}
			if (A_0.IsEmptyElement)
			{
				num = 10;
				continue;
			}
			goto IL_CC;
			IL_66:
			if (true)
			{
			}
			A_0.Read();
			num = 3;
			continue;
			IL_CC:
			localName2 = A_0.LocalName;
			A_0.Read();
			num = 12;
			continue;
			IL_12A:
			num = 6;
		}
	}

	// Token: 0x06001EED RID: 7917 RVA: 0x001F7BF8 File Offset: 0x001F6BF8
	private void \u1719(XmlReader A_0)
	{
		int a_ = 13;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 1:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 2;
					continue;
				}
				goto IL_5E;
			}
			case 2:
			{
				string localName;
				localName == ClipboardData.b("ᩲᡴᙶṸṺ᥼Ṿ", a_);
				num = 0;
				continue;
			}
			case 3:
				goto IL_D2;
			case 4:
			{
				string localName2;
				if (!(A_0.LocalName != localName2))
				{
					num = 7;
					continue;
				}
				num = 10;
				continue;
			}
			case 5:
				goto IL_D2;
			case 6:
				goto IL_15C;
			case 7:
				return;
			case 9:
				goto IL_D2;
			case 10:
				if (true)
				{
				}
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 6;
					continue;
				}
				A_0.Read();
				num = 3;
				continue;
			case 11:
				return;
			}
			if (A_0.IsEmptyElement)
			{
				num = 11;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_15C;
			default:
			{
				if (false)
				{
				}
				string localName2 = A_0.LocalName;
				A_0.Read();
				num = 5;
				continue;
			}
			}
			IL_5E:
			A_0.Read();
			num = 9;
			continue;
			IL_D2:
			num = 4;
			continue;
			IL_15C:
			num = 1;
		}
	}

	// Token: 0x06001EEE RID: 7918 RVA: 0x001F7D64 File Offset: 0x001F6D64
	private void ᜊ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 63;
			for (;;)
			{
				string attribute;
				string attribute2;
				DocPicture docPicture;
				string attribute3;
				string attribute4;
				bool flag;
				string localName;
				string attribute5;
				string attribute8;
				string attribute9;
				string attribute10;
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (attribute.EndsWith(ClipboardData.b("८", a_)))
					{
						num = 22;
						continue;
					}
					goto IL_2C5;
				case 2:
					num = 21;
					continue;
				case 3:
					goto IL_81D;
				case 4:
					goto IL_81D;
				case 5:
					if (attribute2 != null)
					{
						num = 6;
						continue;
					}
					goto IL_790;
				case 6:
					num = 26;
					continue;
				case 7:
					if (spr᧓.\u1752 == null)
					{
						num = 61;
						continue;
					}
					goto IL_463;
				case 8:
					docPicture.Chromakey = this.ᜃ(attribute3);
					num = 31;
					continue;
				case 9:
					goto IL_511;
				case 10:
					if (!this.ᜋ.StartsWith(ClipboardData.b("८ṰᱲŴቶ୸", a_)))
					{
						num = 53;
						continue;
					}
					goto IL_658;
				case 11:
					docPicture.Title = attribute4;
					num = 9;
					continue;
				case 12:
					goto IL_931;
				case 13:
					if (!docPicture.Borders.NoBorder)
					{
						goto IL_A0B;
					}
					goto IL_81D;
				case 14:
					flag = true;
					goto IL_5A2;
				case 15:
					num = 39;
					continue;
				case 16:
					if (!(A_0.LocalName != localName))
					{
						num = 59;
						continue;
					}
					num = 60;
					continue;
				case 17:
				{
					int num2;
					switch (num2)
					{
					case 0:
						attribute5 = A_0.GetAttribute(ClipboardData.b("ٮᕰ", a_), ClipboardData.b("ݮհݲմ䵶噸呺๼᱾愈ꖊﾎﶒ殺ﶚ철슢톤풦螨쒪\udfac좮麰\udcb2펴톶킸\ud8ba\ud8bc﮾껀ꃂ냄꫆곈ꗊ만꧚룜돞胠韢賤裦蟨飪藬蛮臰胲", a_));
						num = 40;
						continue;
					case 1:
					{
						string attribute6 = A_0.GetAttribute(ClipboardData.b("ͮᡰᵲၴѶ൸ɺᅼ᩾", a_));
						num = 13;
						continue;
					}
					case 2:
					{
						string attribute7 = A_0.GetAttribute(ClipboardData.b("᭮ࡰͲၴ", a_));
						num = 41;
						continue;
					}
					case 3:
						this.ᜀ(A_0, docPicture.PictureShape.ᜊ().ᜂ());
						num = 43;
						continue;
					case 4:
						this.ᜀ(A_0, docPicture.PictureShape.ᜊ().ᜃ());
						num = 38;
						continue;
					case 5:
						this.ᜀ(A_0, docPicture.PictureShape.ᜊ().ᜇ());
						num = 23;
						continue;
					case 6:
						this.ᜀ(A_0, docPicture.PictureShape.ᜊ().ᜈ());
						num = 4;
						continue;
					default:
						num = 28;
						continue;
					}
					break;
				}
				case 18:
					if (attribute4 != null)
					{
						num = 11;
						continue;
					}
					goto IL_511;
				case 19:
					goto IL_3EC;
				case 20:
					if (attribute8.EndsWith(ClipboardData.b("८", a_)))
					{
						num = 58;
						continue;
					}
					goto IL_931;
				case 21:
					if (attribute9.EndsWith(ClipboardData.b("८", a_)))
					{
						num = 34;
						continue;
					}
					goto IL_6EA;
				case 22:
					docPicture.CropFromRight = float.Parse(attribute.Replace(ClipboardData.b("८", a_), ""), NumberStyles.Float, CultureInfo.InvariantCulture) / 65536f;
					num = 42;
					continue;
				case 23:
					goto IL_81D;
				case 24:
					num = 17;
					continue;
				case 25:
					goto IL_6A2;
				case 26:
					if (attribute2.EndsWith(ClipboardData.b("८", a_)))
					{
						num = 29;
						continue;
					}
					goto IL_790;
				case 27:
					num = 7;
					continue;
				case 28:
					num = 33;
					continue;
				case 29:
					docPicture.CropFromTop = float.Parse(attribute2.Replace(ClipboardData.b("८", a_), ""), NumberStyles.Float, CultureInfo.InvariantCulture) / 65536f;
					num = 55;
					continue;
				case 30:
					num = 50;
					continue;
				case 31:
					goto IL_81D;
				case 32:
					num = 10;
					continue;
				case 33:
					goto IL_81D;
				case 34:
					docPicture.CropFromBottom = float.Parse(attribute9.Replace(ClipboardData.b("८", a_), ""), NumberStyles.Float, CultureInfo.InvariantCulture) / 65536f;
					num = 64;
					continue;
				case 35:
				{
					string attribute6;
					docPicture.Borders.BorderType = this.ᜊ(attribute6);
					num = 49;
					continue;
				}
				case 36:
					if (attribute8 != null)
					{
						num = 65;
						continue;
					}
					goto IL_931;
				case 37:
					docPicture.TextWrappingType = this.ᜣ(attribute10);
					num = 3;
					continue;
				case 38:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A0B;
					default:
						if (false)
						{
						}
						goto IL_81D;
					}
					break;
				case 39:
					if (!this.ᜋ.StartsWith(ClipboardData.b("ݮᑰቲᅴቶ୸", a_)))
					{
						num = 32;
						continue;
					}
					goto IL_658;
				case 40:
					if (!string.IsNullOrEmpty(attribute5))
					{
						num = 15;
						continue;
					}
					goto IL_6A2;
				case 41:
				{
					string attribute7;
					if (attribute7 != null)
					{
						num = 66;
						continue;
					}
					goto IL_61E;
				}
				case 42:
					goto IL_2C5;
				case 43:
					goto IL_81D;
				case 44:
					if (true)
					{
					}
					flag = false;
					goto IL_5A2;
				case 45:
					if (attribute != null)
					{
						num = 57;
						continue;
					}
					goto IL_2C5;
				case 46:
					if (attribute10 != null)
					{
						num = 37;
						continue;
					}
					goto IL_81D;
				case 47:
					if (attribute9 != null)
					{
						num = 2;
						continue;
					}
					goto IL_6EA;
				case 48:
					goto IL_3EC;
				case 49:
					goto IL_81D;
				case 50:
				{
					string localName2;
					if ((localName2 = A_0.LocalName) != null)
					{
						num = 27;
						continue;
					}
					goto IL_81D;
				}
				case 51:
				{
					int num2;
					string localName2;
					if (spr᧓.\u1752.TryGetValue(localName2, out num2))
					{
						num = 24;
						continue;
					}
					goto IL_81D;
				}
				case 52:
					goto IL_3EC;
				case 53:
					num = 44;
					continue;
				case 54:
					if (attribute3 != null)
					{
						num = 8;
						continue;
					}
					goto IL_81D;
				case 55:
					goto IL_790;
				case 56:
					goto IL_61E;
				case 57:
					num = 1;
					continue;
				case 58:
					docPicture.CropFromLeft = float.Parse(attribute8.Replace(ClipboardData.b("८", a_), ""), NumberStyles.Float, CultureInfo.InvariantCulture) / 65536f;
					num = 12;
					continue;
				case 59:
					return;
				case 60:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 30;
						continue;
					}
					A_0.Read();
					num = 52;
					continue;
				case 61:
					spr᧓.\u1752 = new Dictionary<string, int>(7)
					{
						{
							ClipboardData.b("ٮᱰቲቴቶᵸ᩺ॼṾ", a_),
							0
						},
						{
							ClipboardData.b("ᱮհŲᩴᱶᱸ", a_),
							1
						},
						{
							ClipboardData.b("ᡮͰቲմ", a_),
							2
						},
						{
							ClipboardData.b("൮ṰŲᅴቶ୸ེቼཾ", a_),
							3
						},
						{
							ClipboardData.b("൮ṰŲᅴቶ୸᝺᡼᥾", a_),
							4
						},
						{
							ClipboardData.b("൮ṰŲᅴቶ୸᥺ቼ୾", a_),
							5
						},
						{
							ClipboardData.b("൮ṰŲᅴቶ୸ॺᑼ᡾", a_),
							6
						}
					};
					num = 62;
					continue;
				case 62:
					goto IL_463;
				case 64:
					goto IL_6EA;
				case 65:
					num = 20;
					continue;
				case 66:
				{
					string attribute7;
					docPicture.TextWrappingStyle = this.ᜤ(attribute7);
					num = 56;
					continue;
				}
				}
				if (A_0.IsEmptyElement)
				{
					num = 0;
					continue;
				}
				localName = A_0.LocalName;
				A_0.Read();
				docPicture = (A_1 as DocPicture);
				num = 48;
				continue;
				IL_2C5:
				attribute3 = A_0.GetAttribute(ClipboardData.b("౮ᥰŲᩴ᩶ᡸၺ᡼پ", a_));
				num = 54;
				continue;
				IL_3EC:
				num = 16;
				continue;
				IL_463:
				num = 51;
				continue;
				IL_511:
				attribute2 = A_0.GetAttribute(ClipboardData.b("౮ͰᱲմͶᙸ୺", a_));
				num = 5;
				continue;
				IL_5A2:
				bool a_2 = flag;
				float width = docPicture.Width;
				float height = docPicture.Height;
				this.ᜀ(docPicture, attribute5, a_2, false);
				docPicture.Width = width;
				docPicture.Height = height;
				num = 25;
				continue;
				IL_61E:
				attribute10 = A_0.GetAttribute(ClipboardData.b("ᱮᡰᝲၴ", a_));
				num = 46;
				continue;
				IL_658:
				num = 14;
				continue;
				IL_6A2:
				attribute4 = A_0.GetAttribute(ClipboardData.b("᭮ᡰݲᥴቶ", a_), ClipboardData.b("ᩮͰᵲ佴Ѷ᩸፺᡼ቾꢄﾌﲒ뒘鮠첢쎤솦삨좪좬閮\udeb0햲펴\udeb6\udab8\udeba", a_));
				num = 18;
				continue;
				IL_6EA:
				attribute8 = A_0.GetAttribute(ClipboardData.b("౮Ͱᱲմ᭶ᱸᵺॼ", a_));
				num = 36;
				continue;
				IL_790:
				attribute9 = A_0.GetAttribute(ClipboardData.b("౮Ͱᱲմᕶᙸེॼၾ", a_));
				num = 47;
				continue;
				IL_81D:
				A_0.Read();
				num = 19;
				continue;
				IL_931:
				attribute = A_0.GetAttribute(ClipboardData.b("౮Ͱᱲմնၸᱺᕼ୾", a_));
				num = 45;
				continue;
				IL_A0B:
				num = 35;
			}
			return;
		}
		}
	}

	// Token: 0x06001EEF RID: 7919 RVA: 0x001F8790 File Offset: 0x001F7790
	private void ᜀ(XmlReader A_0, spr\u224E A_1)
	{
		int a_ = 15;
		if (true)
		{
		}
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("Ŵ๶ॸṺ", a_));
			int num = 11;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_1A5;
				case 1:
					goto IL_121;
				case 2:
					if (attribute == ClipboardData.b("Ŵն౸Ṻ", a_))
					{
						num = 1;
						continue;
					}
					return;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DE;
					default:
					{
						if (false)
						{
						}
						BorderStyle borderStyle = this.ᜦ(attribute);
						A_1.ᜃ((byte)borderStyle);
						num = 12;
						continue;
					}
					}
					break;
				case 4:
				{
					int num2 = int.Parse(attribute, NumberStyles.Number, CultureInfo.InvariantCulture) / 8;
					A_1.ᜀ((byte)num2);
					num = 0;
					continue;
				}
				case 5:
					return;
				case 6:
					if (!(attribute == ClipboardData.b("䑴", a_)))
					{
						num = 13;
						continue;
					}
					goto IL_121;
				case 7:
					if (attribute != null)
					{
						num = 4;
						continue;
					}
					goto IL_1A5;
				case 8:
					num = 10;
					continue;
				case 9:
					num = 6;
					continue;
				case 10:
					if (!(attribute == ClipboardData.b("ᩴ᥶", a_)))
					{
						goto IL_DE;
					}
					goto IL_121;
				case 11:
					if (attribute != null)
					{
						num = 3;
						continue;
					}
					goto IL_EB;
				case 12:
					goto IL_EB;
				case 13:
					num = 2;
					continue;
				case 14:
					if (attribute != null)
					{
						num = 8;
						continue;
					}
					return;
				}
				break;
				IL_DE:
				num = 9;
				continue;
				IL_EB:
				attribute = A_0.GetAttribute(ClipboardData.b("ɴṶᵸེᕼ", a_));
				num = 7;
				continue;
				IL_121:
				A_1.ᜀ(true);
				num = 5;
				continue;
				IL_1A5:
				attribute = A_0.GetAttribute(ClipboardData.b("ٴὶᡸὺቼࡾ", a_));
				num = 14;
			}
		}
	}

	// Token: 0x06001EF0 RID: 7920 RVA: 0x001F899C File Offset: 0x001F799C
	private BorderStyle ᜦ(string A_0)
	{
		int a_ = 19;
		BorderStyle result;
		for (;;)
		{
			result = BorderStyle.None;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					if (spr᧓.\u1753.TryGetValue(A_0, out num2))
					{
						num = 31;
						continue;
					}
					return result;
				}
				case 1:
					goto IL_209;
				case 2:
					return result;
				case 3:
					goto IL_1BE;
				case 4:
					return result;
				case 5:
					num = 30;
					continue;
				case 6:
					return result;
				case 7:
					if (A_0 != null)
					{
						num = 5;
						continue;
					}
					return result;
				case 8:
					return result;
				case 9:
					return result;
				case 10:
					goto IL_EA;
				case 11:
					return result;
				case 12:
					return result;
				case 13:
					goto IL_D7;
				case 14:
				{
					int num2;
					switch (num2)
					{
					case 0:
						result = BorderStyle.Single;
						num = 34;
						continue;
					case 1:
						result = BorderStyle.Thick;
						num = 24;
						continue;
					case 2:
						result = BorderStyle.Double;
						num = 20;
						continue;
					case 3:
						result = BorderStyle.Hairline;
						num = 25;
						continue;
					case 4:
						result = BorderStyle.Dot;
						num = 21;
						continue;
					case 5:
						result = BorderStyle.DashLargeGap;
						goto IL_300;
					case 6:
						result = BorderStyle.DotDash;
						num = 12;
						continue;
					case 7:
						result = BorderStyle.DotDotDash;
						num = 26;
						continue;
					case 8:
						result = BorderStyle.Triple;
						num = 19;
						continue;
					case 9:
						result = BorderStyle.ThinThickSmallGap;
						num = 33;
						continue;
					case 10:
						result = BorderStyle.ThinThinSmallGap;
						num = 28;
						continue;
					case 11:
						result = BorderStyle.ThinThickThinSmallGap;
						num = 32;
						continue;
					case 12:
						result = BorderStyle.ThinThickMediumGap;
						num = 10;
						continue;
					case 13:
						result = BorderStyle.ThickThinMediumGap;
						num = 8;
						continue;
					case 14:
						if (true)
						{
						}
						result = BorderStyle.ThickThickThinMediumGap;
						num = 2;
						continue;
					case 15:
						result = BorderStyle.ThinThickLargeGap;
						num = 29;
						continue;
					case 16:
						result = BorderStyle.ThickThinLargeGap;
						num = 13;
						continue;
					case 17:
						result = BorderStyle.ThinThickThinLargeGap;
						num = 9;
						continue;
					case 18:
						result = BorderStyle.Wave;
						num = 22;
						continue;
					case 19:
						result = BorderStyle.DoubleWave;
						num = 6;
						continue;
					case 20:
						result = BorderStyle.DashSmallGap;
						num = 16;
						continue;
					case 21:
						result = BorderStyle.DashDotStroker;
						num = 4;
						continue;
					case 22:
						result = BorderStyle.Emboss3D;
						num = 23;
						continue;
					case 23:
						result = BorderStyle.Engrave3D;
						num = 3;
						continue;
					case 24:
						result = BorderStyle.Outset;
						num = 15;
						continue;
					case 25:
						result = BorderStyle.Inset;
						num = 1;
						continue;
					default:
						num = 18;
						continue;
					}
					break;
				}
				case 15:
					return result;
				case 16:
					goto IL_10F;
				case 17:
					return result;
				case 18:
					num = 11;
					continue;
				case 19:
					return result;
				case 20:
					goto IL_1E3;
				case 21:
					return result;
				case 22:
					return result;
				case 23:
					goto IL_1F6;
				case 24:
					goto IL_FC;
				case 25:
					return result;
				case 26:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_300;
					default:
						goto IL_232;
					}
					break;
				case 27:
					spr᧓.\u1753 = new Dictionary<string, int>(26)
					{
						{
							ClipboardData.b("੸ቺ፼᡾", a_),
							0
						},
						{
							ClipboardData.b("൸፺ᑼ᱾", a_),
							1
						},
						{
							ClipboardData.b("ᵸᑺࡼᵾ", a_),
							2
						},
						{
							ClipboardData.b("ᅸ᩺ᑼൾ", a_),
							3
						},
						{
							ClipboardData.b("ᵸᑺॼ", a_),
							4
						},
						{
							ClipboardData.b("ᵸ᩺๼᝾", a_),
							5
						},
						{
							ClipboardData.b("ᵸᑺॼ㭾", a_),
							6
						},
						{
							ClipboardData.b("ᵸ᩺๼᝾얀쎆ﾊ", a_),
							7
						},
						{
							ClipboardData.b("൸ॺᑼཾ", a_),
							8
						},
						{
							ClipboardData.b("൸፺ᑼᅾ햀\ud88a﶐ﾒ", a_),
							9
						},
						{
							ClipboardData.b("൸፺ᑼ᱾힂\ud88a﶐ﾒ", a_),
							10
						},
						{
							ClipboardData.b("൸፺ᑼ᱾솂ﺈ얐ﮒﲔ練쪘ﲜ춠", a_),
							11
						},
						{
							ClipboardData.b("൸፺ᑼᅾ햀", a_),
							12
						},
						{
							ClipboardData.b("൸፺ᑼ᱾힂", a_),
							13
						},
						{
							ClipboardData.b("൸፺ᑼ᱾솂ﺈ얐ﮒﲔ練", a_),
							14
						},
						{
							ClipboardData.b("൸፺ᑼᅾ햀잊ﶎ", a_),
							15
						},
						{
							ClipboardData.b("൸፺ᑼ᱾힂잊ﶎ", a_),
							16
						},
						{
							ClipboardData.b("൸፺ᑼ᱾솂ﺈ얐ﮒﲔ練햘漢쒠", a_),
							17
						},
						{
							ClipboardData.b("๸᩺୼᩾", a_),
							18
						},
						{
							ClipboardData.b("ᵸᑺࡼᵾ튄ﾈ", a_),
							19
						},
						{
							ClipboardData.b("ᵸ᩺๼᝾횄", a_),
							20
						},
						{
							ClipboardData.b("ᵸ᩺๼᝾얀풆ﶈ力", a_),
							21
						},
						{
							ClipboardData.b("൸፺ོ᩾잂삄ﺌﲎ", a_),
							22
						},
						{
							ClipboardData.b("൸፺ོ᩾잂삄力年", a_),
							23
						},
						{
							ClipboardData.b("ㅸ⽺ぼ㍾캀ﾊ", a_),
							24
						},
						{
							ClipboardData.b("ㅸ⽺ぼ㍾좀ﶈ", a_),
							25
						}
					};
					num = 35;
					continue;
				case 28:
					return result;
				case 29:
					goto IL_1AB;
				case 30:
					if (spr᧓.\u1753 == null)
					{
						num = 27;
						continue;
					}
					goto IL_310;
				case 31:
					num = 14;
					continue;
				case 32:
					goto IL_1D1;
				case 33:
					return result;
				case 34:
					return result;
				case 35:
					goto IL_310;
				}
				break;
				IL_300:
				num = 17;
				continue;
				IL_310:
				num = 0;
			}
		}
		IL_D7:
		IL_EA:
		IL_FC:
		IL_10F:
		IL_1AB:
		IL_1BE:
		IL_1D1:
		IL_1E3:
		IL_1F6:
		IL_209:
		return result;
		IL_232:
		if (false)
		{
		}
		return result;
	}

	// Token: 0x06001EF1 RID: 7921 RVA: 0x001F8FC4 File Offset: 0x001F7FC4
	private void ᜉ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 4;
		switch (0)
		{
		default:
			for (;;)
			{
				DocPicture docPicture = A_1 as DocPicture;
				string text = A_0.GetAttribute(ClipboardData.b("ᥩᡫ᝭ᱯ᝱", a_));
				int num = 9;
				for (;;)
				{
					string attribute;
					string attribute2;
					int num2;
					switch (num)
					{
					case 0:
						goto IL_145;
					case 1:
						if (attribute == ClipboardData.b("ṩ", a_))
						{
							num = 19;
							continue;
						}
						return;
					case 2:
						return;
					case 3:
						num = 1;
						continue;
					case 4:
						goto IL_167;
					case 5:
					{
						string[] array;
						if (array != null)
						{
							num = 15;
							continue;
						}
						goto IL_341;
					}
					case 6:
						goto IL_25D;
					case 7:
						if (attribute != null)
						{
							num = 3;
							continue;
						}
						return;
					case 8:
					{
						string[] array2;
						docPicture.Borders.Color = ColorTranslator.FromHtml(array2[0]);
						num = 0;
						continue;
					}
					case 9:
						if (text != null)
						{
							num = 16;
							continue;
						}
						goto IL_2E3;
					case 10:
						goto IL_341;
					case 11:
					{
						string[] array2;
						if (array2.Length > 0)
						{
							num = 8;
							continue;
						}
						goto IL_145;
					}
					case 12:
						goto IL_2E3;
					case 13:
						docPicture.Borders.LineWidth = this.\u171F(attribute2);
						num = 2;
						continue;
					case 14:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 12;
							continue;
						}
						string[] array3;
						string[] array = this.\u171D(array3[num2]);
						num = 5;
						continue;
					}
					case 15:
					{
						string[] array;
						this.ᜀ(docPicture, array[0], array[1]);
						num = 10;
						continue;
					}
					case 16:
					{
						text = text.Trim();
						char[] separator = new char[]
						{
							';'
						};
						string[] array3 = text.Split(separator);
						string[] array = new string[2];
						num2 = 0;
						int num3 = array3.Length;
						num = 20;
						continue;
					}
					case 17:
					{
						if (true)
						{
						}
						string attribute3;
						string[] array2 = attribute3.Split(new char[]
						{
							' '
						}, StringSplitOptions.RemoveEmptyEntries);
						num = 11;
						continue;
					}
					case 18:
					{
						string attribute3;
						if (attribute3 == null)
						{
							goto IL_145;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_167;
						default:
							if (false)
							{
							}
							num = 17;
							continue;
						}
						break;
					}
					case 19:
					{
						docPicture.Borders.BorderType = BorderStyle.Single;
						docPicture.Borders.LineWidth = 0.5f;
						docPicture.Borders.Color = Color.Black;
						string attribute3 = A_0.GetAttribute(ClipboardData.b("ᥩᡫᱭὯᥱᅳᕵ᝷ᙹ፻౽", a_));
						num = 18;
						continue;
					}
					case 20:
						goto IL_25D;
					}
					break;
					IL_145:
					attribute2 = A_0.GetAttribute(ClipboardData.b("ᥩᡫᱭὯᥱᅳŵᵷ፹᭻ᙽ", a_));
					num = 4;
					continue;
					IL_167:
					if (attribute2 != null)
					{
						num = 13;
						continue;
					}
					return;
					IL_25D:
					num = 14;
					continue;
					IL_2E3:
					docPicture.AlternativeText = A_0.GetAttribute(ClipboardData.b("୩kᩭ", a_));
					attribute = A_0.GetAttribute(ClipboardData.b("ᥩᡫᱭὯᥱᅳት", a_));
					num = 7;
					continue;
					IL_341:
					num2++;
					num = 6;
				}
			}
			return;
		}
	}

	// Token: 0x06001EF2 RID: 7922 RVA: 0x001F932C File Offset: 0x001F832C
	private void ᜀ(DocPicture A_0, string A_1, string A_2)
	{
		int a_ = 5;
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
					goto IL_142;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 2:
				spr᧓.\u1754 = new Dictionary<string, int>(14)
				{
					{
						ClipboardData.b("٪౬ᵮᙰᩲ᭴婶ᕸṺ᭼୾", a_),
						0
					},
					{
						ClipboardData.b("٪౬ᵮᙰᩲ᭴婶൸ᑺർ", a_),
						1
					},
					{
						ClipboardData.b("ᱪѬ୮հ᭲", a_),
						2
					},
					{
						ClipboardData.b("ͪ࡬ٮᙰ᭲Ŵ", a_),
						3
					},
					{
						ClipboardData.b("ᅪ䁬ٮὰᝲၴྲྀ", a_),
						4
					},
					{
						ClipboardData.b("٪Ṭn屰ͲᩴѶၸེᑼၾ꺂ﮈﾐﮖ", a_),
						5
					},
					{
						ClipboardData.b("٪Ṭn屰ͲᩴѶၸེᑼၾ꺂ﮈﾊﾒ", a_),
						6
					},
					{
						ClipboardData.b("٪Ṭn屰ͲᩴѶၸེᑼၾ꺂ﮈﾊﾒ뢔ﲘﲜ좠햢삤", a_),
						7
					},
					{
						ClipboardData.b("٪Ṭn屰ͲᩴѶၸེᑼၾ꺂ﮈﾐﮖ뒘삠힢첤톦첨", a_),
						8
					},
					{
						ClipboardData.b("٪Ṭn屰Ѳݴᙶॸ噺᥼ᙾꂌ", a_),
						9
					},
					{
						ClipboardData.b("٪Ṭn屰Ѳݴᙶॸ噺᥼ᙾꂌﶎﶔ", a_),
						10
					},
					{
						ClipboardData.b("٪Ṭn屰Ѳݴᙶॸ噺᥼ᙾꂌﮎﺐ", a_),
						11
					},
					{
						ClipboardData.b("٪Ṭn屰Ѳݴᙶॸ噺᥼ᙾꂌﺐ", a_),
						12
					},
					{
						ClipboardData.b("᭪ɬᱮᡰݲᱴᡶ᝸", a_),
						13
					}
				};
				num = 9;
				continue;
			case 3:
				goto IL_41F;
			case 4:
			{
				int num2;
				switch (num2)
				{
				case 0:
					goto IL_94;
				case 1:
					goto IL_126;
				case 2:
					goto IL_275;
				case 3:
					goto IL_226;
				case 4:
				{
					int num3 = int.Parse(A_2, NumberStyles.Integer, CultureInfo.InvariantCulture);
					A_0.OrderIndex = num3;
					num = 15;
					continue;
				}
				case 5:
					goto IL_267;
				case 6:
					goto IL_424;
				case 7:
					goto IL_DF;
				case 8:
					goto IL_76;
				case 9:
					A_0.WrapDistanceLeft = this.ᜄ(A_2);
					num = 8;
					continue;
				case 10:
					goto IL_234;
				case 11:
					goto IL_A2;
				case 12:
					goto IL_134;
				case 13:
					num = 7;
					continue;
				default:
					num = 13;
					continue;
				}
				break;
			}
			case 5:
				num = 10;
				continue;
			case 6:
			{
				int num2;
				if (spr᧓.\u1754.TryGetValue(A_1, out num2))
				{
					num = 1;
					continue;
				}
				return;
			}
			case 7:
				if (!string.IsNullOrEmpty(A_2))
				{
					num = 17;
					continue;
				}
				return;
			case 8:
				return;
			case 9:
				goto IL_B0;
			case 10:
				if (spr᧓.\u1754 == null)
				{
					num = 2;
					continue;
				}
				goto IL_B0;
			case 11:
				goto IL_142;
			case 12:
				if (A_2 == ClipboardData.b("੪ཬᱮṰὲtͶᱸ", a_))
				{
					num = 11;
					continue;
				}
				return;
			case 13:
				return;
			case 14:
				if (A_0.IsUnderText)
				{
					num = 3;
					continue;
				}
				goto IL_8C;
			case 15:
			{
				int num3;
				A_0.IsUnderText = (num3 <= 0);
				num = 14;
				continue;
			}
			case 16:
				goto IL_15B;
			case 17:
				num = 12;
				continue;
			}
			if (A_1 != null)
			{
				num = 5;
				continue;
			}
			return;
			IL_B0:
			num = 6;
			continue;
			IL_142:
			A_0.HorizontalOrigin = HorizontalOrigin.Column;
			A_0.VerticalOrigin = VerticalOrigin.Paragraph;
			num = 16;
		}
		IL_76:
		if (true)
		{
		}
		A_0.HorizontalOrigin = this.\u171A(A_2);
		return;
		IL_8C:
		A_0.TextWrappingStyle = TextWrappingStyle.InFrontOfText;
		return;
		IL_94:
		A_0.HorizontalPosition = this.ᜄ(A_2);
		return;
		IL_A2:
		A_0.WrapDistanceTop = this.ᜄ(A_2);
		return;
		IL_DF:
		A_0.VerticalOrigin = this.\u1718(A_2);
		return;
		IL_126:
		A_0.VerticalPosition = this.ᜄ(A_2);
		return;
		IL_134:
		A_0.WrapDistanceBottom = this.ᜄ(A_2);
		return;
		IL_15B:
		return;
		IL_226:
		A_0.Height = this.ᜄ(A_2);
		return;
		IL_234:
		A_0.WrapDistanceRight = this.ᜄ(A_2);
		return;
		IL_267:
		A_0.HorizontalAlignment = this.\u171B(A_2);
		return;
		IL_275:
		A_0.Width = this.ᜄ(A_2);
		return;
		IL_41F:
		A_0.TextWrappingStyle = TextWrappingStyle.Behind;
		return;
		IL_424:
		A_0.VerticalAlignment = this.\u1719(A_2);
	}

	// Token: 0x06001EF3 RID: 7923 RVA: 0x001F976C File Offset: 0x001F876C
	private void ᜈ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 17;
		switch (0)
		{
		default:
			for (;;)
			{
				GroupedShapeObject groupedShapeObject = A_1 as GroupedShapeObject;
				string text = A_0.GetAttribute(ClipboardData.b("Ѷ൸ɺᅼ᩾", a_));
				int num = 29;
				for (;;)
				{
					string attribute;
					Borders borders;
					int num2;
					string attribute3;
					string attribute4;
					string attribute5;
					switch (num)
					{
					case 0:
						if (attribute != null)
						{
							num = 13;
							continue;
						}
						goto IL_45E;
					case 1:
					{
						string[] array;
						groupedShapeObject.CoordOrigin = new Point(int.Parse(array[0], NumberStyles.Integer), int.Parse(array[1], NumberStyles.Integer));
						num = 8;
						continue;
					}
					case 2:
					{
						borders = new Borders();
						borders.BorderType = BorderStyle.Single;
						borders.LineWidth = 0.5f;
						borders.Color = Color.Black;
						string attribute2 = A_0.GetAttribute(ClipboardData.b("Ѷ൸ॺቼᑾ力", a_));
						num = 23;
						continue;
					}
					case 3:
						goto IL_45E;
					case 4:
						goto IL_286;
					case 5:
					{
						string[] array2;
						borders.Color = ColorTranslator.FromHtml(array2[0]);
						num = 4;
						continue;
					}
					case 6:
						goto IL_260;
					case 7:
					{
						string[] array3;
						groupedShapeObject.CoordSize = new Size(int.Parse(array3[0], NumberStyles.Integer), int.Parse(array3[1], NumberStyles.Integer));
						num = 3;
						continue;
					}
					case 8:
						goto IL_498;
					case 9:
					{
						string attribute2;
						string[] array2 = attribute2.Split(new char[]
						{
							' '
						}, StringSplitOptions.RemoveEmptyEntries);
						num = 19;
						continue;
					}
					case 10:
						goto IL_260;
					case 11:
					{
						string[] array4;
						if (array4 != null)
						{
							num = 22;
							continue;
						}
						goto IL_173;
					}
					case 12:
					{
						text = text.Trim();
						char[] separator = new char[]
						{
							';'
						};
						string[] array5 = text.Split(separator);
						string[] array4 = new string[2];
						num2 = 0;
						int num3 = array5.Length;
						num = 10;
						continue;
					}
					case 13:
					{
						string[] array3 = attribute.Split(new char[]
						{
							','
						}, StringSplitOptions.RemoveEmptyEntries);
						num = 24;
						continue;
					}
					case 14:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 26;
							continue;
						}
						string[] array5;
						string[] array4 = this.\u171D(array5[num2]);
						num = 11;
						continue;
					}
					case 15:
						borders.LineWidth = this.\u171F(attribute3);
						num = 30;
						continue;
					case 16:
					{
						string[] array = attribute4.Split(new char[]
						{
							','
						}, StringSplitOptions.RemoveEmptyEntries);
						num = 25;
						continue;
					}
					case 17:
						return;
					case 18:
						goto IL_173;
					case 19:
					{
						string[] array2;
						if (array2.Length > 0)
						{
							num = 5;
							continue;
						}
						goto IL_286;
					}
					case 20:
						if (attribute5 == ClipboardData.b("Ͷ", a_))
						{
							num = 2;
							continue;
						}
						return;
					case 21:
						if (attribute3 != null)
						{
							num = 15;
							continue;
						}
						goto IL_2BD;
					case 22:
					{
						string[] array4;
						this.ᜀ(groupedShapeObject, array4[0], array4[1]);
						num = 18;
						continue;
					}
					case 23:
					{
						string attribute2;
						if (attribute2 != null)
						{
							num = 9;
							continue;
						}
						goto IL_286;
					}
					case 24:
					{
						string[] array3;
						if (array3.Length == 2)
						{
							num = 7;
							continue;
						}
						goto IL_45E;
					}
					case 25:
					{
						if (true)
						{
						}
						string[] array;
						if (array.Length != 2)
						{
							goto IL_498;
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
							num = 1;
							continue;
						}
						break;
					}
					case 26:
						goto IL_386;
					case 27:
						if (attribute5 != null)
						{
							num = 31;
							continue;
						}
						return;
					case 28:
						if (attribute4 != null)
						{
							num = 16;
							continue;
						}
						goto IL_498;
					case 29:
						if (text != null)
						{
							num = 12;
							continue;
						}
						goto IL_386;
					case 30:
						goto IL_2BD;
					case 31:
						num = 20;
						continue;
					}
					break;
					IL_173:
					num2++;
					num = 6;
					continue;
					IL_260:
					num = 14;
					continue;
					IL_286:
					attribute3 = A_0.GetAttribute(ClipboardData.b("Ѷ൸ॺቼᑾ歷", a_));
					num = 21;
					continue;
					IL_2BD:
					groupedShapeObject.Borders = borders;
					num = 17;
					continue;
					IL_386:
					attribute4 = A_0.GetAttribute(ClipboardData.b("ᑶᙸᑺོ᭾", a_));
					num = 28;
					continue;
					IL_45E:
					attribute5 = A_0.GetAttribute(ClipboardData.b("Ѷ൸ॺቼᑾ", a_));
					num = 27;
					continue;
					IL_498:
					attribute = A_0.GetAttribute(ClipboardData.b("ᑶᙸᑺོ᭾ﾄ", a_));
					num = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06001EF4 RID: 7924 RVA: 0x001F9C48 File Offset: 0x001F8C48
	private void ᜀ(GroupedShapeObject A_0, string A_1, string A_2)
	{
		int a_ = 15;
		int num = 13;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!string.IsNullOrEmpty(A_2))
				{
					num = 9;
					continue;
				}
				return;
			case 1:
			{
				int num2;
				switch (num2)
				{
				case 0:
				case 1:
					goto IL_96;
				case 2:
				case 3:
					goto IL_128;
				case 4:
					goto IL_26B;
				case 5:
					goto IL_214;
				case 6:
				{
					int num3 = int.Parse(A_2, NumberStyles.Integer, CultureInfo.InvariantCulture);
					A_0.OrderIndex = num3;
					num = 16;
					continue;
				}
				case 7:
					goto IL_255;
				case 8:
					goto IL_458;
				case 9:
					goto IL_E1;
				case 10:
					goto IL_80;
				case 11:
					A_0.WrapDistanceLeft = this.ᜄ(A_2);
					num = 2;
					continue;
				case 12:
					goto IL_222;
				case 13:
					goto IL_A4;
				case 14:
					goto IL_136;
				case 15:
					num = 0;
					continue;
				default:
					num = 12;
					continue;
				}
				break;
			}
			case 2:
				return;
			case 3:
			{
				int num2;
				if (spr᧓.\u1755.TryGetValue(A_1, out num2))
				{
					num = 8;
					continue;
				}
				return;
			}
			case 4:
				if (A_2 == ClipboardData.b("ᑴᕶ੸ᑺᅼ੾", a_))
				{
					num = 5;
					continue;
				}
				return;
			case 5:
				A_0.HorizontalOrigin = HorizontalOrigin.Column;
				A_0.VerticalOrigin = VerticalOrigin.Paragraph;
				num = 15;
				continue;
			case 6:
				if (spr᧓.\u1755 == null)
				{
					num = 17;
					continue;
				}
				goto IL_B2;
			case 7:
				num = 6;
				continue;
			case 8:
				num = 1;
				continue;
			case 9:
				num = 4;
				continue;
			case 10:
				goto IL_B2;
			case 11:
				goto IL_453;
			case 12:
				return;
			case 14:
				if (A_0.IsUnderText)
				{
					num = 11;
					continue;
				}
				goto IL_8E;
			case 15:
				goto IL_15D;
			case 16:
			{
				int num3;
				A_0.IsUnderText = (num3 <= 0);
				goto IL_432;
			}
			case 17:
				spr᧓.\u1755 = new Dictionary<string, int>(16)
				{
					{
						ClipboardData.b("ᥴቶὸེ", a_),
						0
					},
					{
						ClipboardData.b("ᡴᙶ୸ᱺᑼᅾ검ﶈ", a_),
						1
					},
					{
						ClipboardData.b("Ŵᡶॸ", a_),
						2
					},
					{
						ClipboardData.b("ᡴᙶ୸ᱺᑼᅾ검", a_),
						3
					},
					{
						ClipboardData.b("ɴṶᵸེᕼ", a_),
						4
					},
					{
						ClipboardData.b("ᵴቶၸᱺᕼ୾", a_),
						5
					},
					{
						ClipboardData.b("ུ婶ၸᕺ᥼᩾呂", a_),
						6
					},
					{
						ClipboardData.b("ᡴѶᙸ噺ർၾꂌﺐﲔﺞ춠", a_),
						7
					},
					{
						ClipboardData.b("ᡴѶᙸ噺ർၾꂌ年ﺖ滛漢", a_),
						8
					},
					{
						ClipboardData.b("ᡴѶᙸ噺ർၾꂌ年ﺖ滛漢늞펠욢즤욦\udda8슪\udbac쪮", a_),
						9
					},
					{
						ClipboardData.b("ᡴѶᙸ噺ർၾꂌﺐﲔﺞ춠躢힤슦얨쪪\ud9ac욮잰횲", a_),
						10
					},
					{
						ClipboardData.b("ᡴѶᙸ噺੼ൾꢄ歷ﾐ몖ﺚﮜ", a_),
						11
					},
					{
						ClipboardData.b("ᡴѶᙸ噺੼ൾꢄ歷ﾐ몖煮햠", a_),
						12
					},
					{
						ClipboardData.b("ᡴѶᙸ噺੼ൾꢄ歷ﾐ몖", a_),
						13
					},
					{
						ClipboardData.b("ᡴѶᙸ噺੼ൾꢄ歷ﾐ몖ﮘ캠캢", a_),
						14
					},
					{
						ClipboardData.b("մᡶ੸ቺॼᙾ", a_),
						15
					}
				};
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_432;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			}
			if (A_1 != null)
			{
				num = 7;
				continue;
			}
			return;
			IL_B2:
			num = 3;
			continue;
			IL_432:
			num = 14;
		}
		IL_80:
		A_0.HorizontalOrigin = this.\u171A(A_2);
		return;
		IL_8E:
		A_0.TextWrappingStyle = TextWrappingStyle.InFrontOfText;
		return;
		IL_96:
		A_0.HorizontalPosition = this.ᜄ(A_2);
		return;
		IL_A4:
		A_0.WrapDistanceTop = this.ᜄ(A_2);
		return;
		IL_E1:
		A_0.VerticalOrigin = this.\u1718(A_2);
		return;
		IL_128:
		A_0.VerticalPosition = this.ᜄ(A_2);
		return;
		IL_136:
		A_0.WrapDistanceBottom = this.ᜄ(A_2);
		return;
		IL_15D:
		return;
		IL_214:
		A_0.Height = this.ᜄ(A_2);
		return;
		IL_222:
		A_0.WrapDistanceRight = this.ᜄ(A_2);
		return;
		IL_255:
		if (true)
		{
		}
		A_0.HorizontalAlignment = this.\u171B(A_2);
		return;
		IL_26B:
		A_0.Width = this.ᜄ(A_2);
		return;
		IL_453:
		A_0.TextWrappingStyle = TextWrappingStyle.Behind;
		return;
		IL_458:
		A_0.VerticalAlignment = this.\u1719(A_2);
	}

	// Token: 0x06001EF5 RID: 7925 RVA: 0x001FA0BC File Offset: 0x001F90BC
	private void ᜇ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
			for (;;)
			{
				GroupedShapePicture groupedShapePicture = A_1 as GroupedShapePicture;
				string text = A_0.GetAttribute(ClipboardData.b("Ὣᩭ९ṱᅳ", a_));
				int num = 6;
				for (;;)
				{
					string attribute;
					Borders borders;
					string attribute3;
					int num2;
					switch (num)
					{
					case 0:
						if (attribute != null)
						{
							num = 2;
							continue;
						}
						goto IL_1E0;
					case 1:
						goto IL_1F9;
					case 2:
						borders.LineWidth = this.\u171F(attribute);
						num = 3;
						continue;
					case 3:
						goto IL_1E0;
					case 4:
						goto IL_24D;
					case 5:
					{
						string attribute2;
						string[] array = attribute2.Split(new char[]
						{
							' '
						}, StringSplitOptions.RemoveEmptyEntries);
						num = 12;
						continue;
					}
					case 6:
						if (text != null)
						{
							num = 13;
							continue;
						}
						goto IL_1F9;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_191;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 8:
						if (attribute3 == ClipboardData.b("ᡫ", a_))
						{
							num = 17;
							continue;
						}
						return;
					case 9:
					{
						string[] array2;
						if (array2 != null)
						{
							goto IL_191;
						}
						goto IL_24D;
					}
					case 10:
						if (attribute3 != null)
						{
							num = 7;
							continue;
						}
						return;
					case 11:
					{
						string[] array2;
						this.ᜀ(groupedShapePicture, array2[0], array2[1]);
						num = 4;
						continue;
					}
					case 12:
					{
						string[] array;
						if (array.Length > 0)
						{
							num = 18;
							continue;
						}
						goto IL_264;
					}
					case 13:
					{
						text = text.Trim();
						char[] separator = new char[]
						{
							';'
						};
						string[] array3 = text.Split(separator);
						string[] array2 = new string[2];
						num2 = 0;
						int num3 = array3.Length;
						num = 14;
						continue;
					}
					case 14:
						goto IL_345;
					case 15:
						return;
					case 16:
					{
						string attribute2;
						if (attribute2 != null)
						{
							num = 5;
							continue;
						}
						goto IL_264;
					}
					case 17:
					{
						borders = new Borders();
						borders.BorderType = BorderStyle.Single;
						borders.LineWidth = 0.5f;
						borders.Color = Color.Black;
						string attribute2 = A_0.GetAttribute(ClipboardData.b("Ὣᩭɯᵱέ፵᭷ᕹၻᅽ", a_));
						num = 16;
						continue;
					}
					case 18:
					{
						string[] array;
						borders.Color = ColorTranslator.FromHtml(array[0]);
						num = 20;
						continue;
					}
					case 19:
						goto IL_345;
					case 20:
						goto IL_264;
					case 21:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 1;
							continue;
						}
						string[] array3;
						string[] array2 = this.\u171D(array3[num2]);
						num = 9;
						continue;
					}
					}
					break;
					IL_191:
					num = 11;
					continue;
					IL_1E0:
					groupedShapePicture.Borders = borders;
					num = 15;
					continue;
					IL_1F9:
					groupedShapePicture.AlternativeText = A_0.GetAttribute(ClipboardData.b("൫ɭѯ", a_));
					attribute3 = A_0.GetAttribute(ClipboardData.b("Ὣᩭɯᵱέ፵ᱷ", a_));
					num = 10;
					continue;
					IL_24D:
					num2++;
					num = 19;
					continue;
					IL_264:
					attribute = A_0.GetAttribute(ClipboardData.b("Ὣᩭɯᵱέ፵ཷόᕻ᥽", a_));
					if (true)
					{
					}
					num = 0;
					continue;
					IL_345:
					num = 21;
				}
			}
			return;
		}
	}

	// Token: 0x06001EF6 RID: 7926 RVA: 0x001FA434 File Offset: 0x001F9434
	private void ᜀ(GroupedShapePicture A_0, string A_1, string A_2)
	{
		int a_ = 3;
		int num = 12;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 14;
				continue;
			case 1:
				spr᧓.\u1756 = new Dictionary<string, int>(6)
				{
					{
						ClipboardData.b("Ṩɪ६᭮ᥰ", a_),
						0
					},
					{
						ClipboardData.b("Ũ๪Ѭ࡮ᥰݲ", a_),
						1
					},
					{
						ClipboardData.b("ᵨѪᵬ", a_),
						2
					},
					{
						ClipboardData.b("ը๪୬᭮", a_),
						3
					},
					{
						ClipboardData.b("፨䙪ѬŮᕰᙲ൴", a_),
						4
					},
					{
						ClipboardData.b("ᥨѪṬٮհᩲᩴ᥶", a_),
						5
					}
				};
				num = 4;
				continue;
			case 2:
				return;
			case 3:
				goto IL_13B;
			case 4:
				goto IL_9B;
			case 5:
			{
				int num2;
				if (spr᧓.\u1756.TryGetValue(A_1, out num2))
				{
					num = 0;
					continue;
				}
				return;
			}
			case 6:
				return;
			case 7:
			{
				int num3;
				A_0.IsUnderText = (num3 <= 0);
				num = 16;
				continue;
			}
			case 8:
				return;
			case 9:
				return;
			case 10:
				num = 11;
				continue;
			case 11:
				if (true)
				{
				}
				if (spr᧓.\u1756 != null)
				{
					goto IL_9B;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_13B;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 13:
				goto IL_1B0;
			case 14:
			{
				int num2;
				switch (num2)
				{
				case 0:
					goto IL_FA;
				case 1:
					goto IL_E6;
				case 2:
					A_0.TopPosition = this.ᜄ(A_2) / 127f;
					num = 8;
					continue;
				case 3:
					goto IL_D2;
				case 4:
				{
					int num3 = int.Parse(A_2, NumberStyles.Integer, CultureInfo.InvariantCulture);
					A_0.OrderIndex = num3;
					num = 7;
					continue;
				}
				case 5:
					num = 3;
					continue;
				default:
					num = 6;
					continue;
				}
				break;
			}
			case 15:
				A_2 == ClipboardData.b("ࡨ४ṬnᵰٲŴቶ", a_);
				num = 9;
				continue;
			case 16:
				if (A_0.IsUnderText)
				{
					num = 13;
					continue;
				}
				A_0.TextWrappingStyle = TextWrappingStyle.InFrontOfText;
				num = 2;
				continue;
			}
			if (A_1 != null)
			{
				num = 10;
				continue;
			}
			return;
			IL_9B:
			num = 5;
			continue;
			IL_13B:
			if (string.IsNullOrEmpty(A_2))
			{
				return;
			}
			num = 15;
		}
		IL_D2:
		A_0.LeftPosition = this.ᜄ(A_2) / 127f;
		return;
		IL_E6:
		A_0.Height = this.ᜄ(A_2) / 127f;
		return;
		IL_FA:
		A_0.Width = this.ᜄ(A_2) / 127f;
		return;
		IL_1B0:
		A_0.TextWrappingStyle = TextWrappingStyle.Behind;
	}

	// Token: 0x06001EF7 RID: 7927 RVA: 0x001FA71C File Offset: 0x001F971C
	private TextBox ᜀ(MemoryStream A_0, MemoryStream A_1)
	{
		XmlReader a_;
		TextBox textBox;
		for (;;)
		{
			A_0.Position = 0L;
			a_ = spr\u23D7.ᜀ(A_0);
			textBox = new TextBox(this.ᜄ);
			int num = 2;
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
						goto IL_96;
					default:
					{
						if (false)
						{
						}
						A_1.Position = 0L;
						XmlReader a_2 = spr\u23D7.ᜀ(A_1);
						this.ᜅ(a_2, textBox);
						num = 1;
						continue;
					}
					}
					break;
				case 1:
					goto IL_94;
				case 2:
					if (A_1 != null)
					{
						num = 0;
						continue;
					}
					goto IL_96;
				}
				break;
			}
		}
		IL_94:
		IL_96:
		this.ᜆ(a_, textBox);
		return textBox;
	}

	// Token: 0x06001EF8 RID: 7928 RVA: 0x001FA7C8 File Offset: 0x001F97C8
	private void ᜅ(XmlReader A_0, TextBox A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_40C:
				A_1.Format.TextDirection = TextDirection.TopToBottomRotated;
				num = 9;
				break;
			default:
				if (false)
				{
				}
				num = 47;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 28;
						continue;
					}
					A_0.Read();
					num = 31;
					continue;
				case 1:
					goto IL_119;
				case 2:
				{
					string attribute;
					if (attribute == ClipboardData.b("ᵪ࡬ᵮհ", a_))
					{
						num = 27;
						continue;
					}
					num = 15;
					continue;
				}
				case 3:
				{
					int num2;
					switch (num2)
					{
					case 0:
						A_1.Format.TextWrappingStyle = TextWrappingStyle.Square;
						num = 18;
						continue;
					case 1:
						A_1.Format.TextWrappingStyle = TextWrappingStyle.Tight;
						num = 21;
						continue;
					case 2:
						A_1.Format.TextWrappingStyle = TextWrappingStyle.Through;
						num = 42;
						continue;
					case 3:
						A_1.Format.TextWrappingStyle = TextWrappingStyle.TopAndBottom;
						num = 22;
						continue;
					case 4:
						num = 48;
						continue;
					case 5:
						num = 17;
						continue;
					case 6:
						A_1.Format.TextWrappingStyle = TextWrappingStyle.Inline;
						num = 38;
						continue;
					case 7:
						num = 46;
						continue;
					default:
						num = 10;
						continue;
					}
					break;
				}
				case 4:
					goto IL_187;
				case 5:
				{
					string attribute;
					A_1.Format.IsBelowText = (attribute == ClipboardData.b("婪", a_) || attribute == ClipboardData.b("ὪὬᩮᑰ", a_));
					num = 24;
					continue;
				}
				case 6:
					if (A_0.LocalName != ClipboardData.b("ཪὬ๮ٰᩲ᭴ၶ", a_))
					{
						num = 29;
						continue;
					}
					num = 34;
					continue;
				case 7:
					goto IL_40C;
				case 8:
					num = 19;
					continue;
				case 9:
					goto IL_167;
				case 10:
					num = 44;
					continue;
				case 11:
					goto IL_167;
				case 12:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 37;
						continue;
					}
					goto IL_167;
				}
				case 13:
				{
					int num2;
					string localName;
					if (spr᧓.\u1757.TryGetValue(localName, out num2))
					{
						num = 40;
						continue;
					}
					goto IL_167;
				}
				case 14:
					goto IL_167;
				case 15:
				{
					string attribute;
					if (attribute == ClipboardData.b("ᵪ࡬ᵮհ䅲䉴䝶", a_))
					{
						num = 7;
						continue;
					}
					goto IL_167;
				}
				case 16:
					goto IL_167;
				case 17:
					if (A_0.AttributeCount != 0)
					{
						num = 33;
						continue;
					}
					goto IL_167;
				case 18:
					goto IL_167;
				case 19:
				{
					if (!(A_0.LocalName != ClipboardData.b("ཪὬ๮ٰᩲ᭴ၶ", a_)))
					{
						num = 32;
						continue;
					}
					bool flag = false;
					num = 0;
					continue;
				}
				case 20:
				{
					string attribute = A_0.GetAttribute(ClipboardData.b("ᵪ࡬ᵮհ", a_));
					num = 2;
					continue;
				}
				case 21:
					goto IL_167;
				case 22:
					if (true)
					{
					}
					goto IL_167;
				case 24:
					goto IL_167;
				case 25:
					num = 6;
					continue;
				case 26:
					A_0.Read();
					num = 4;
					continue;
				case 27:
					A_1.Format.TextDirection = TextDirection.TopToBottom;
					num = 11;
					continue;
				case 28:
					num = 12;
					continue;
				case 29:
					goto IL_1D5;
				case 30:
					if (spr᧓.\u1757 == null)
					{
						num = 43;
						continue;
					}
					goto IL_119;
				case 31:
					goto IL_187;
				case 32:
					goto IL_46A;
				case 33:
				{
					string attribute = A_0.GetAttribute(ClipboardData.b("४࡬ݮᡰᵲᅴ㍶ᙸ᡺", a_));
					num = 5;
					continue;
				}
				case 34:
				{
					if (A_0.IsEmptyElement)
					{
						num = 36;
						continue;
					}
					bool flag = false;
					A_0.Read();
					this.ᜀ(A_0);
					num = 45;
					continue;
				}
				case 35:
					A_1.Format.TextWrappingStyle = TextWrappingStyle.Behind;
					num = 16;
					continue;
				case 36:
					return;
				case 37:
					num = 30;
					continue;
				case 38:
					goto IL_167;
				case 39:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 8;
						continue;
					}
					return;
				case 40:
					num = 3;
					continue;
				case 41:
				{
					bool flag;
					if (!flag)
					{
						num = 26;
						continue;
					}
					goto IL_187;
				}
				case 42:
					goto IL_167;
				case 43:
					spr᧓.\u1757 = new Dictionary<string, int>(8)
					{
						{
							ClipboardData.b("ᱪὬ๮Ű⁲Ѵɶᡸॺ᡼", a_),
							0
						},
						{
							ClipboardData.b("ᱪὬ๮Ű❲ᱴၶᅸེ", a_),
							1
						},
						{
							ClipboardData.b("ᱪὬ๮Ű❲ᵴնᙸ๺᩼᝾", a_),
							2
						},
						{
							ClipboardData.b("ᱪὬ๮Ű❲ᩴݶ㡸ᕺ᥼㵾", a_),
							3
						},
						{
							ClipboardData.b("ᱪὬ๮Ű㵲ᩴ᥶ᱸ", a_),
							4
						},
						{
							ClipboardData.b("੪ͬ౮ᥰᱲݴ", a_),
							5
						},
						{
							ClipboardData.b("ɪͬͮᡰᵲၴ", a_),
							6
						},
						{
							ClipboardData.b("४ɬ୮ࡰͲݴ", a_),
							7
						}
					};
					num = 1;
					continue;
				case 44:
					goto IL_167;
				case 45:
					goto IL_67F;
				case 46:
					if (A_0.AttributeCount != 0)
					{
						num = 20;
						continue;
					}
					goto IL_167;
				case 48:
					if (A_1.Format.IsBelowText)
					{
						num = 35;
						continue;
					}
					A_1.Format.TextWrappingStyle = TextWrappingStyle.InFrontOfText;
					num = 14;
					continue;
				case 49:
					goto IL_67F;
				case 50:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 25;
						continue;
					}
					A_0.Read();
					num = 23;
					continue;
				}
				goto IL_114;
				IL_119:
				num = 13;
				continue;
				IL_167:
				num = 41;
				continue;
				IL_187:
				this.ᜀ(A_0);
				num = 49;
				continue;
				IL_25B:
				num = 50;
				continue;
				IL_114:
				goto IL_25B;
				IL_67F:
				num = 39;
			}
			IL_1D5:
			throw new XmlException(ClipboardData.b("㹪ͬ੮॰Ͳၴᑶ൸Ṻ᥼彾呂Ꞇﶈ꾎", a_) + A_0.LocalName);
			IL_46A:
			return;
		}
		}
	}

	// Token: 0x06001EF9 RID: 7929 RVA: 0x001FAF00 File Offset: 0x001F9F00
	private void ᜆ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 18;
		int num = 7;
		for (;;)
		{
			string localName2;
			switch (num)
			{
			case 0:
				num = 13;
				continue;
			case 1:
				goto IL_166;
			case 2:
				goto IL_105;
			case 3:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 1;
					continue;
				}
				goto IL_7E;
			}
			case 4:
				goto IL_7E;
			case 5:
			{
				string localName;
				if (!(localName == ClipboardData.b("୷ቹᵻ๽", a_)))
				{
					num = 15;
					continue;
				}
				goto IL_1B8;
			}
			case 6:
				return;
			case 7:
				if (true)
				{
				}
				break;
			case 8:
				goto IL_1B8;
			case 9:
				return;
			case 10:
				goto IL_105;
			case 11:
				num = 3;
				continue;
			case 12:
				goto IL_105;
			case 13:
				if (A_0.EOF)
				{
					num = 6;
					continue;
				}
				num = 17;
				continue;
			case 14:
			{
				string localName;
				if (!(localName == ClipboardData.b("੷όύ੽", a_)))
				{
					goto IL_7E;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_166;
				default:
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			}
			case 15:
				num = 14;
				continue;
			case 16:
				if (A_0.LocalName != localName2)
				{
					num = 0;
					continue;
				}
				return;
			case 17:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 11;
					continue;
				}
				A_0.Read();
				num = 2;
				continue;
			}
			if (A_0.IsEmptyElement)
			{
				num = 9;
				continue;
			}
			localName2 = A_0.LocalName;
			A_0.Read();
			this.ᜀ(A_0);
			num = 12;
			continue;
			IL_7E:
			A_0.Read();
			num = 10;
			continue;
			IL_105:
			num = 16;
			continue;
			IL_166:
			num = 5;
			continue;
			IL_1B8:
			this.ᜃ(A_0, A_1);
			this.ᜄ(A_0, A_1);
			this.ᜅ(A_0, A_1);
			num = 4;
		}
	}

	// Token: 0x06001EFA RID: 7930 RVA: 0x001FB118 File Offset: 0x001FA118
	private void ᜅ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 11;
			for (;;)
			{
				string localName;
				bool flag;
				switch (num)
				{
				case 0:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 16;
						continue;
					}
					return;
				case 1:
					goto IL_F4;
				case 2:
				{
					if (true)
					{
					}
					string attribute;
					if (attribute != null)
					{
						num = 12;
						continue;
					}
					goto IL_143;
				}
				case 3:
					if (!(A_0.LocalName != localName))
					{
						num = 9;
						continue;
					}
					flag = false;
					num = 14;
					continue;
				case 4:
					num = 18;
					continue;
				case 5:
					if (flag)
					{
						goto IL_F4;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D7;
					default:
						if (false)
						{
						}
						num = 17;
						continue;
					}
					break;
				case 6:
				{
					string localName2;
					if (localName2 == ClipboardData.b("v୸᩺ർ", a_))
					{
						num = 8;
						continue;
					}
					goto IL_143;
				}
				case 7:
					goto IL_F4;
				case 8:
				{
					string attribute = A_0.GetAttribute(ClipboardData.b("Ͷx୺᡼", a_));
					num = 2;
					continue;
				}
				case 9:
					return;
				case 10:
					num = 6;
					continue;
				case 12:
				{
					string attribute;
					(A_1 as TextBox).Format.TextWrappingStyle = this.ᜤ(attribute);
					num = 13;
					continue;
				}
				case 13:
					goto IL_143;
				case 14:
					goto IL_D7;
				case 15:
					goto IL_F4;
				case 16:
					num = 3;
					continue;
				case 17:
					A_0.Read();
					num = 1;
					continue;
				case 18:
				{
					string localName2;
					if ((localName2 = A_0.LocalName) != null)
					{
						num = 10;
						continue;
					}
					goto IL_143;
				}
				case 19:
					return;
				}
				if (A_0.IsEmptyElement)
				{
					num = 19;
					continue;
				}
				localName = A_0.LocalName;
				A_0.Read();
				flag = false;
				num = 7;
				continue;
				IL_D7:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 4;
					continue;
				}
				A_0.Read();
				num = 15;
				continue;
				IL_F4:
				num = 0;
				continue;
				IL_143:
				num = 5;
			}
			return;
		}
		}
	}

	// Token: 0x06001EFB RID: 7931 RVA: 0x001FB388 File Offset: 0x001FA388
	private void ᜄ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 4;
		int num = 10;
		for (;;)
		{
			string localName;
			bool flag;
			switch (num)
			{
			case 0:
				if (A_0.LocalName != localName)
				{
					flag = false;
					num = 8;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2AD;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 1:
				goto IL_1DD;
			case 2:
				num = 0;
				continue;
			case 3:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 2;
					continue;
				}
				return;
			case 4:
				A_0.Read();
				num = 1;
				continue;
			case 5:
				num = 14;
				continue;
			case 6:
				return;
			case 7:
			{
				string localName2;
				if (!(localName2 == ClipboardData.b("ṩ५᙭ѯၱ᭳๵", a_)))
				{
					num = 17;
					continue;
				}
				goto IL_2AD;
			}
			case 8:
				if (true)
				{
				}
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 15;
					continue;
				}
				A_0.Read();
				num = 12;
				continue;
			case 9:
				goto IL_95;
			case 11:
				num = 7;
				continue;
			case 12:
				goto IL_1DD;
			case 13:
			{
				string localName2;
				if ((localName2 = A_0.LocalName) != null)
				{
					num = 22;
					continue;
				}
				goto IL_95;
			}
			case 14:
				goto IL_95;
			case 15:
				num = 13;
				continue;
			case 16:
				goto IL_95;
			case 17:
				num = 23;
				continue;
			case 18:
				if (!flag)
				{
					num = 4;
					continue;
				}
				goto IL_1DD;
			case 19:
			{
				string localName2;
				if (!(localName2 == ClipboardData.b("ᥩᡫᱭὯᥱᅳ", a_)))
				{
					num = 11;
					continue;
				}
				this.ᜁ(A_0, A_1 as TextBox);
				num = 9;
				continue;
			}
			case 20:
				goto IL_95;
			case 21:
				return;
			case 22:
				num = 19;
				continue;
			case 23:
			{
				string localName2;
				if (!(localName2 == ClipboardData.b("౩իɭᱯ", a_)))
				{
					num = 5;
					continue;
				}
				flag = this.ᜄ(A_0, A_1 as TextBox);
				num = 16;
				continue;
			}
			case 24:
				goto IL_1DD;
			}
			if (A_0.IsEmptyElement)
			{
				num = 21;
				continue;
			}
			localName = A_0.LocalName;
			A_0.Read();
			flag = false;
			num = 24;
			continue;
			IL_95:
			num = 18;
			continue;
			IL_1DD:
			num = 3;
			continue;
			IL_2AD:
			this.ᜂ(A_0, A_1 as TextBox);
			this.ᜃ(A_0, A_1 as TextBox);
			this.\u1716 = null;
			this.ᜐ(A_0, A_1);
			num = 20;
		}
	}

	// Token: 0x06001EFC RID: 7932 RVA: 0x001FB67C File Offset: 0x001FA67C
	private bool ᜄ(XmlReader A_0, TextBox A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			bool result;
			for (;;)
			{
				IL_74:
				result = false;
				string attribute = A_0.GetAttribute(ClipboardData.b("౷͹౻᭽", a_));
				for (;;)
				{
					IL_8B:
					int num = 8;
					for (;;)
					{
						string attribute2;
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							goto IL_133;
						case 2:
							goto IL_133;
						case 3:
							num = 7;
							continue;
						case 4:
							num = 19;
							continue;
						case 5:
							num = 18;
							continue;
						case 6:
							goto IL_133;
						case 7:
						{
							string a;
							if (!(a == ClipboardData.b("ίࡹᵻ᩽\uda87ﺑ", a_)))
							{
								num = 4;
								continue;
							}
							goto IL_CF;
						}
						case 8:
							if (attribute != null)
							{
								num = 13;
								continue;
							}
							goto IL_133;
						case 9:
							goto IL_133;
						case 10:
							if (attribute2 == ClipboardData.b("䡷", a_))
							{
								num = 12;
								continue;
							}
							return result;
						case 11:
						{
							string a;
							if (!(a == ClipboardData.b("ṷࡹᵻ፽", a_)))
							{
								num = 17;
								continue;
							}
							goto IL_1B8;
						}
						case 12:
							A_1.Format.FillColor = Color.Empty;
							num = 14;
							continue;
						case 13:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_8B;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								num = 20;
								continue;
							}
							break;
						case 14:
							return result;
						case 15:
							num = 11;
							continue;
						case 16:
						{
							string a;
							if (!(a == ClipboardData.b("ίࡹᵻ᩽", a_)))
							{
								num = 3;
								continue;
							}
							goto IL_CF;
						}
						case 17:
							num = 16;
							continue;
						case 18:
						{
							string a;
							if (!(a == ClipboardData.b("౷፹ၻ᭽", a_)))
							{
								num = 15;
								continue;
							}
							goto IL_1B8;
						}
						case 19:
						{
							string a;
							if (!(a == ClipboardData.b("ࡷ᭹ࡻ੽", a_)))
							{
								num = 0;
								continue;
							}
							this.ᜁ(A_0, A_1.Format.FillEfects);
							result = true;
							num = 9;
							continue;
						}
						case 20:
						{
							string a;
							if ((a = attribute) != null)
							{
								num = 5;
								continue;
							}
							goto IL_133;
						}
						}
						goto IL_74;
						IL_CF:
						this.ᜃ(A_0, A_1.Format.FillEfects);
						num = 2;
						continue;
						IL_133:
						attribute2 = A_0.GetAttribute(ClipboardData.b("᝷੹ᵻᵽﶃ", a_));
						num = 10;
						continue;
						IL_1B8:
						this.ᜀ(A_0, A_1, attribute);
						num = 6;
					}
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06001EFD RID: 7933 RVA: 0x001FB964 File Offset: 0x001FA964
	private void ᜃ(XmlReader A_0, Background A_1)
	{
		int a_ = 7;
		string attribute = A_0.GetAttribute(ClipboardData.b("ᥬ᙮Űᙲ", a_));
		A_1.Type = BackgroundType.Gradient;
		this.ᜂ(A_0, A_1);
		if (attribute == ClipboardData.b("੬ᵮၰᝲᱴቶ᝸ེ⽼Ṿ", a_))
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ(A_0, A_1.Gradient);
			return;
		}
		this.ᜁ(A_0, A_1.Gradient);
	}

	// Token: 0x06001EFE RID: 7934 RVA: 0x001FBA00 File Offset: 0x001FAA00
	private void ᜂ(XmlReader A_0, Background A_1)
	{
		int a_ = 6;
		BackgroundGradient gradient;
		string attribute;
		for (;;)
		{
			IL_09:
			for (;;)
			{
				IL_57:
				gradient = A_1.Gradient;
				int num = 4;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							goto IL_82;
						case 1:
							goto IL_B0;
						case 2:
							gradient.Color1 = Color.White;
							num = 0;
							continue;
						case 3:
							goto IL_82;
						case 4:
							if (A_1.Color == Color.Empty)
							{
								num = 2;
								continue;
							}
							gradient.Color1 = A_1.Color;
							num = 3;
							continue;
						case 5:
							if (attribute == null)
							{
								num = 1;
								continue;
							}
							goto IL_EF;
						}
						goto IL_57;
						IL_82:
						attribute = A_0.GetAttribute(ClipboardData.b("ཫŭᱯᵱٳ䑵", a_));
						num = 5;
						break;
					}
				}
			}
		}
		IL_B0:
		gradient.Color2 = Color.Black;
		return;
		IL_EF:
		gradient.Color2 = this.ᜃ(attribute);
	}

	// Token: 0x06001EFF RID: 7935 RVA: 0x001FBB0C File Offset: 0x001FAB0C
	private void ᜁ(XmlReader A_0, BackgroundGradient A_1)
	{
		int a_ = 4;
		string attribute;
		for (;;)
		{
			attribute = A_0.GetAttribute(ClipboardData.b("౩ͫ൭կű", a_));
			string attribute2 = A_0.GetAttribute(ClipboardData.b("୩ɫ७ᱯ᝱", a_));
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_1.ShadingStyle = GradientShadingStyle.DiagonalUp;
					num = 5;
					continue;
				case 1:
					if (!(attribute2 == ClipboardData.b("䝩填孭", a_)))
					{
						goto IL_18E;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_18C;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 2:
					goto IL_18C;
				case 3:
					A_1.ShadingStyle = GradientShadingStyle.DiagonalDown;
					if (true)
					{
					}
					num = 6;
					continue;
				case 4:
					A_1.ShadingStyle = GradientShadingStyle.Vertical;
					num = 2;
					continue;
				case 5:
					goto IL_9F;
				case 6:
					goto IL_11A;
				case 7:
					if (attribute2 == ClipboardData.b("䝩啫幭", a_))
					{
						num = 4;
						continue;
					}
					num = 11;
					continue;
				case 8:
					A_1.ShadingStyle = GradientShadingStyle.Horizontal;
					num = 9;
					continue;
				case 9:
					goto IL_12E;
				case 10:
					if (attribute2 == null)
					{
						num = 8;
						continue;
					}
					num = 7;
					continue;
				case 11:
					if (attribute2 == ClipboardData.b("䝩嵫嵭䕯", a_))
					{
						num = 0;
						continue;
					}
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_9F:
		IL_11A:
		IL_12E:
		IL_18C:
		IL_18E:
		A_1.ShadingVariant = this.ᜥ(attribute);
	}

	// Token: 0x06001F00 RID: 7936 RVA: 0x001FBCB4 File Offset: 0x001FACB4
	private void ᜀ(XmlReader A_0, BackgroundGradient A_1)
	{
		int a_ = 7;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("୬nተٲٴݶᙸࡺᑼ୾", a_));
			string attribute2 = A_0.GetAttribute(ClipboardData.b("୬nተٲٴ", a_));
			string a = A_0.ReadInnerXml();
			if (true)
			{
			}
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (attribute == null)
					{
						num = 10;
						continue;
					}
					num = 6;
					continue;
				case 1:
					A_1.ShadingStyle = GradientShadingStyle.FromCorner;
					num = 0;
					continue;
				case 2:
					if (attribute == ClipboardData.b("䅬幮", a_))
					{
						num = 8;
						continue;
					}
					return;
				case 3:
					goto IL_1B2;
				case 4:
					if (a != string.Empty)
					{
						num = 1;
						continue;
					}
					A_1.ShadingStyle = GradientShadingStyle.FromCenter;
					A_1.ShadingVariant = this.ᜥ(attribute2);
					num = 9;
					continue;
				case 5:
					goto IL_102;
				case 6:
					if (attribute == ClipboardData.b("屬", a_))
					{
						num = 3;
						continue;
					}
					num = 7;
					continue;
				case 7:
					if (attribute == ClipboardData.b("屬䍮䁰", a_))
					{
						num = 5;
						continue;
					}
					num = 2;
					continue;
				case 8:
					goto IL_D4;
				case 9:
					return;
				case 10:
					goto IL_134;
				}
				break;
			}
		}
		for (;;)
		{
			IL_1B2:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_175;
			}
		}
		IL_175:
		if (false)
		{
		}
		A_1.ShadingVariant = GradientShadingVariant.ShadingDown;
		return;
		IL_D4:
		A_1.ShadingVariant = GradientShadingVariant.ShadingOut;
		return;
		IL_102:
		A_1.ShadingVariant = GradientShadingVariant.ShadingMiddle;
		return;
		IL_134:
		A_1.ShadingVariant = GradientShadingVariant.ShadingUp;
	}

	// Token: 0x06001F01 RID: 7937 RVA: 0x001FBE78 File Offset: 0x001FAE78
	private GradientShadingVariant ᜥ(string A_0)
	{
		int a_ = 11;
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
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return GradientShadingVariant.ShadingUp;
				case 1:
					if (A_0 == ClipboardData.b("䁰䍲䕴剶", a_))
					{
						num = 0;
						continue;
					}
					if (true)
					{
					}
					num = 5;
					continue;
				case 3:
					return GradientShadingVariant.ShadingDown;
				case 4:
					return GradientShadingVariant.ShadingMiddle;
				case 5:
					if (A_0 == ClipboardData.b("䑰䍲側", a_))
					{
						num = 4;
						continue;
					}
					return GradientShadingVariant.ShadingOut;
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					num = 1;
				}
			}
			return GradientShadingVariant.ShadingDown;
		}
		}
		return GradientShadingVariant.ShadingUp;
	}

	// Token: 0x06001F02 RID: 7938 RVA: 0x001FBF4C File Offset: 0x001FAF4C
	private void ᜁ(XmlReader A_0, Background A_1)
	{
		int a_ = 18;
		for (;;)
		{
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("ᅷṹ", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹펻\ud8bdꚿꯁꟃꏅ資ꗉ꿋믍뷏럑뫓ꋕ훟췡難菥蓧诩飫蟭鿯鳱蟳黵釷諹迻", a_));
				A_1.PatternFill2010 = this.ᜢ(A_0);
				int num = 8;
				for (;;)
				{
					bool flag;
					switch (num)
					{
					case 0:
						if (!this.ᜋ.StartsWith(ClipboardData.b("ṷᕹ፻੽", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_85;
					case 1:
						num = 0;
						continue;
					case 2:
						if (!this.ᜋ.StartsWith(ClipboardData.b("ၷόᵻ᩽", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_85;
					case 3:
						flag = true;
						goto IL_FA;
					case 4:
						if (true)
						{
						}
						num = 2;
						continue;
					case 5:
						num = 7;
						continue;
					case 6:
						return;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							flag = false;
							goto IL_FA;
						}
						break;
					case 8:
						if (attribute != null)
						{
							num = 4;
							continue;
						}
						return;
					}
					break;
					IL_85:
					num = 3;
					continue;
					IL_FA:
					bool a_2 = flag;
					string a_3 = this.ᜁ(attribute, a_2, false);
					A_1.PatternImageBytes = this.ᜮ(a_3);
					num = 6;
				}
			}
		}
	}

	// Token: 0x06001F03 RID: 7939 RVA: 0x001FC0B4 File Offset: 0x001FB0B4
	private void ᜀ(XmlReader A_0, TextBox A_1, string A_2)
	{
		int a_ = 7;
		string text;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("Ѭ୮", a_), ClipboardData.b("լ᭮հͲ佴塶噸ࡺṼ᝾ꞈﶌﾐﮖﾘ삠힢횤覦욨\ud9aa쪬肮\udeb0햲펴\udeb6\udab8\udeba寮킾ꋀ뛂꣄ꋆꟈ뿊﷎ꯘ뻚뇜뻞闠諢諤触髨菪蓬鿮苰", a_));
			int num = 12;
			for (;;)
			{
				bool flag;
				switch (num)
				{
				case 0:
					if (!this.ᜋ.StartsWith(ClipboardData.b("լ੮ၰᝲၴն", a_)))
					{
						num = 13;
						continue;
					}
					goto IL_BE;
				case 1:
					return;
				case 2:
					goto IL_168;
				case 3:
					flag = false;
					goto IL_136;
				case 4:
					goto IL_168;
				case 5:
					A_1.Format.FillEfects.Type = BackgroundType.Picture;
					num = 2;
					continue;
				case 6:
					if (A_2 == ClipboardData.b("୬ᵮၰṲၴ", a_))
					{
						num = 5;
						continue;
					}
					A_1.Format.FillEfects.Type = BackgroundType.Texture;
					num = 4;
					continue;
				case 7:
					goto IL_196;
				case 8:
					flag = true;
					goto IL_136;
				case 9:
					if (!this.ᜋ.StartsWith(ClipboardData.b("୬nṰݲၴն", a_)))
					{
						num = 11;
						continue;
					}
					goto IL_BE;
				case 10:
					if (this.\u1717().ContainsKey(text))
					{
						num = 7;
						continue;
					}
					goto IL_207;
				case 11:
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
						continue;
					}
					break;
				case 12:
					if (attribute == null)
					{
						num = 1;
						continue;
					}
					num = 0;
					continue;
				case 13:
					num = 9;
					continue;
				}
				break;
				IL_BE:
				num = 8;
				continue;
				IL_136:
				bool a_2 = flag;
				num = 6;
				continue;
				IL_168:
				text = this.ᜁ(attribute, a_2, false);
				num = 10;
			}
		}
		return;
		IL_196:
		A_1.Format.FillEfects.ImageRecord = this.ᜄ.Images.ᜀ(this.\u1717()[text]);
		return;
		IL_207:
		if (true)
		{
		}
		A_1.Format.FillEfects.ImageBytes = this.ᜮ(text);
		this.\u1717().Add(text, A_1.Format.FillEfects.ImageRecord.ᜀ());
	}

	// Token: 0x06001F04 RID: 7940 RVA: 0x001FC308 File Offset: 0x001FB308
	private TextWrappingStyle ᜤ(string A_0)
	{
		int a_ = 8;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (!(A_0 == ClipboardData.b("ᵭůݱᕳѵᵷ", a_)))
				{
					num = 11;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_142;
				}
				break;
			case 2:
				num = 4;
				continue;
			case 3:
				if (!(A_0 == ClipboardData.b("mὯᱱᅳ", a_)))
				{
					num = 2;
					continue;
				}
				return TextWrappingStyle.Inline;
			case 4:
				goto IL_69;
			case 5:
				num = 1;
				continue;
			case 6:
				if (!(A_0 == ClipboardData.b("ᩭὯɱ㕳ᡵᱷ㡹፻੽", a_)))
				{
					if (true)
					{
					}
					num = 12;
					continue;
				}
				return TextWrappingStyle.TopAndBottom;
			case 7:
				if (!(A_0 == ClipboardData.b("ᩭ᥯ᕱᱳɵ", a_)))
				{
					num = 8;
					continue;
				}
				return TextWrappingStyle.Tight;
			case 8:
				num = 9;
				continue;
			case 9:
				if (!(A_0 == ClipboardData.b("ᩭᡯq᭳͵ίቹ", a_)))
				{
					num = 10;
					continue;
				}
				return TextWrappingStyle.Through;
			case 10:
				num = 6;
				continue;
			case 11:
				num = 7;
				continue;
			case 12:
				num = 3;
				continue;
			}
			IL_4D:
			if (A_0 != null)
			{
				num = 5;
				continue;
			}
			return TextWrappingStyle.InFrontOfText;
			goto IL_4D;
		}
		return TextWrappingStyle.Tight;
		IL_69:
		return TextWrappingStyle.InFrontOfText;
		IL_142:
		if (false)
		{
		}
		return TextWrappingStyle.Square;
	}

	// Token: 0x06001F05 RID: 7941 RVA: 0x001FC4A8 File Offset: 0x001FB4A8
	private TextWrappingType ᜣ(string A_0)
	{
		int a_ = 8;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_9F:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			num = 4;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				if (!(A_0 == ClipboardData.b("ᱭ᥯ᕱᱳɵ", a_)))
				{
					num = 3;
					continue;
				}
				return TextWrappingType.Right;
			case 2:
				if (true)
				{
				}
				num = 6;
				continue;
			case 3:
				num = 5;
				continue;
			case 5:
				goto IL_77;
			case 6:
				goto IL_89;
			}
			if (A_0 == null)
			{
				break;
			}
			num = 2;
		}
		IL_77:
		return TextWrappingType.Both;
		IL_89:
		if (!(A_0 == ClipboardData.b("ɭᕯᑱs", a_)))
		{
			goto IL_9F;
		}
		return TextWrappingType.Left;
	}

	// Token: 0x06001F06 RID: 7942 RVA: 0x001FC590 File Offset: 0x001FB590
	internal void ᜐ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
			for (;;)
			{
				TextBox textBox = A_1 as TextBox;
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 22;
						continue;
					case 1:
					{
						ITable table;
						if (table.Rows.Count <= 0)
						{
							num = 18;
							continue;
						}
						goto IL_352;
					}
					case 2:
						goto IL_352;
					case 3:
						num = 15;
						continue;
					case 4:
						if (A_0.NodeType != XmlNodeType.EndElement)
						{
							num = 24;
							continue;
						}
						return;
					case 5:
					{
						ITable table;
						if (textBox.Body.Items.Contains(table))
						{
							num = 14;
							continue;
						}
						goto IL_352;
					}
					case 6:
					{
						if (A_0.IsEmptyElement)
						{
							num = 25;
							continue;
						}
						string localName = A_0.LocalName;
						A_0.Read();
						num = 16;
						continue;
					}
					case 7:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 20;
							continue;
						}
						A_0.Read();
						num = 17;
						continue;
					case 8:
						return;
					case 9:
						goto IL_1E1;
					case 10:
					{
						string localName2;
						if (!(localName2 == ClipboardData.b("᭪", a_)))
						{
							num = 23;
							continue;
						}
						IParagraph paragraph = textBox.Body.AddParagraph();
						this.ᜐ(A_0, paragraph.Items);
						num = 2;
						continue;
					}
					case 11:
					{
						string localName;
						if (!(A_0.LocalName != localName))
						{
							num = 8;
							continue;
						}
						goto IL_2E1;
					}
					case 12:
					{
						string localName2;
						if (!(localName2 == ClipboardData.b("Ὢཬͮ", a_)))
						{
							num = 3;
							continue;
						}
						ITable table = textBox.Body.AddTable();
						this.ᜅ(A_0, table as Table);
						if (true)
						{
						}
						num = 1;
						continue;
					}
					case 13:
						goto IL_352;
					case 14:
					{
						ITable table;
						textBox.Body.Items.Remove(table);
						num = 13;
						continue;
					}
					case 15:
					{
						string localName2;
						if (!(localName2 == ClipboardData.b("ᡪ६᭮", a_)))
						{
							num = 0;
							continue;
						}
						spr\u2215 spr_u = textBox.Body.ᜐ();
						this.ᜁ(A_0, spr_u as spr\u1AE7);
						num = 21;
						continue;
					}
					case 16:
						goto IL_1E1;
					case 17:
						goto IL_18B;
					case 18:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2E1;
						}
						if (false)
						{
						}
						num = 5;
						continue;
					case 19:
					{
						string localName2;
						if ((localName2 = A_0.LocalName) != null)
						{
							num = 26;
							continue;
						}
						goto IL_352;
					}
					case 20:
						num = 19;
						continue;
					case 21:
						goto IL_352;
					case 22:
						goto IL_352;
					case 23:
						num = 12;
						continue;
					case 24:
						num = 11;
						continue;
					case 25:
						return;
					case 26:
						num = 10;
						continue;
					case 27:
						goto IL_18B;
					}
					break;
					IL_18B:
					this.ᜀ(A_0);
					num = 9;
					continue;
					IL_1E1:
					num = 4;
					continue;
					IL_2E1:
					num = 7;
					continue;
					IL_352:
					A_0.Read();
					num = 27;
				}
			}
			return;
		}
	}

	// Token: 0x06001F07 RID: 7943 RVA: 0x001FC938 File Offset: 0x001FB938
	private void ᜃ(XmlReader A_0, TextBox A_1)
	{
		int a_ = 11;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ɰݲ౴᭶ᱸ", a_));
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (attribute.Contains(ClipboardData.b("ᵰቲ౴ᡶ౸ེ偼᥾붆ﾌﲒﮔ난얠욢쪤삦\udba8쪪\uddac잮\ud8b0킲", a_)))
					{
						num = 11;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1A8;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 1:
					A_1.Format.LayoutFlowAlt = TextDirection.RightToLeftRotated;
					num = 2;
					continue;
				case 2:
					goto IL_18C;
				case 3:
					goto IL_BD;
				case 4:
					return;
				case 5:
					if (attribute.Contains(ClipboardData.b("ᵰቲ౴ᡶ౸ེ偼᥾붆ﾈﾌﮎﮖꊘ負쾢쒤\udea6욨\udeaa\ud9ac芮ힰ\udfb2\udab4삶钸\udaba톼쮾﯀ꇂ꫄돆뷈꓊ꃌꗐ볒ꏖ뛘ꯚ", a_)))
					{
						num = 12;
						continue;
					}
					num = 0;
					continue;
				case 6:
					goto IL_122;
				case 7:
					if (attribute.Contains(ClipboardData.b("ᵰቲ౴ᡶ౸ེ偼᥾붆ﾈﾌﮎﮖ뒘列爵캠쒢힤욦\ud9a8쎪쒬첮", a_)))
					{
						num = 1;
						continue;
					}
					return;
				case 8:
					if (attribute == null)
					{
						num = 4;
						continue;
					}
					num = 9;
					continue;
				case 9:
					if (attribute.Contains(ClipboardData.b("ᱰrᩴ婶ὸቺॼ剾Ꚋ歷벐ꆚ", a_)))
					{
						num = 6;
						continue;
					}
					num = 5;
					continue;
				case 10:
					if (attribute.Contains(ClipboardData.b("ᵰቲ౴ᡶ౸ེ偼᥾붆ﾈﾌﮎﮖ", a_)))
					{
						num = 3;
						continue;
					}
					if (true)
					{
					}
					num = 7;
					continue;
				case 11:
					goto IL_EE;
				case 12:
					goto IL_1E4;
				}
				break;
			}
		}
		return;
		IL_BD:
		A_1.Format.LayoutFlowAlt = TextDirection.RightToLeft;
		return;
		IL_EE:
		goto IL_1A8;
		IL_122:
		A_1.Format.IsFitTextToShape = true;
		return;
		IL_18C:
		return;
		IL_1A8:
		A_1.Format.LayoutFlowAlt = TextDirection.TopToBottomRotated;
		return;
		IL_1E4:
		A_1.Format.LayoutFlowAlt = TextDirection.LeftToRightRotated;
	}

	// Token: 0x06001F08 RID: 7944 RVA: 0x001FCB2C File Offset: 0x001FBB2C
	private void ᜂ(XmlReader A_0, TextBox A_1)
	{
		int a_ = 11;
		switch (0)
		{
		default:
			for (;;)
			{
				string text = A_0.GetAttribute(ClipboardData.b("ᡰᵲٴቶ൸", a_));
				int num = 6;
				for (;;)
				{
					int num2;
					float a_2;
					switch (num)
					{
					case 0:
						goto IL_191;
					case 1:
						if (num2 == 2)
						{
							num = 10;
							continue;
						}
						num = 15;
						continue;
					case 2:
						if (num2 == 1)
						{
							num = 12;
							continue;
						}
						num = 1;
						continue;
					case 3:
						return;
					case 4:
						goto IL_112;
					case 5:
						goto IL_112;
					case 6:
					{
						if (text == null)
						{
							num = 18;
							continue;
						}
						text = text.Replace(ClipboardData.b("ᱰṲ", a_), string.Empty);
						string[] array = text.Split(new char[]
						{
							','
						});
						num2 = 0;
						int num3 = array.Length;
						num = 9;
						continue;
					}
					case 7:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 3;
							continue;
						}
						num = 16;
						continue;
					}
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_218;
						default:
						{
							if (false)
							{
							}
							string[] array;
							a_2 = this.ᜢ(array[num2]);
							num = 14;
							continue;
						}
						}
						break;
					case 9:
						goto IL_191;
					case 10:
						A_1.Format.InternalMargin.ᜃ(a_2);
						if (true)
						{
						}
						num = 4;
						continue;
					case 11:
						A_1.Format.InternalMargin.ᜀ(a_2);
						num = 13;
						continue;
					case 12:
						A_1.Format.InternalMargin.ᜁ(a_2);
						num = 19;
						continue;
					case 13:
						goto IL_112;
					case 14:
						if (num2 == 0)
						{
							num = 17;
							continue;
						}
						num = 2;
						continue;
					case 15:
						if (num2 == 3)
						{
							num = 11;
							continue;
						}
						goto IL_112;
					case 16:
					{
						string[] array;
						if (!(array[num2] == string.Empty))
						{
							num = 8;
							continue;
						}
						goto IL_112;
					}
					case 17:
						goto IL_218;
					case 18:
						return;
					case 19:
						goto IL_112;
					}
					break;
					IL_112:
					num2++;
					num = 0;
					continue;
					IL_191:
					num = 7;
					continue;
					IL_218:
					A_1.Format.InternalMargin.ᜂ(a_2);
					num = 5;
				}
			}
			return;
		}
	}

	// Token: 0x06001F09 RID: 7945 RVA: 0x001FCDC8 File Offset: 0x001FBDC8
	private float ᜢ(string A_0)
	{
		int a_ = 3;
		int num = 5;
		float result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return result;
			case 1:
				A_0 = A_0.Replace(ClipboardData.b("hժ", a_), string.Empty);
				result = (float)spr\u1C39.ᜁ().ᜀ(Convert.ToDouble(A_0), PrintUnits.Inch, PrintUnits.Point);
				num = 7;
				continue;
			case 2:
			{
				if (A_0.EndsWith(ClipboardData.b("hժ", a_)))
				{
					num = 1;
					continue;
				}
				if (true)
				{
				}
				float num2 = float.Parse(A_0, NumberStyles.Float, CultureInfo.InvariantCulture);
				result = (float)spr\u1C39.ᜁ().ᜀ((double)num2, PrintUnits.Millimeter, PrintUnits.Point);
				num = 6;
				continue;
			}
			case 3:
				if (A_0.EndsWith(ClipboardData.b("ᥨὪ", a_)))
				{
					goto IL_15B;
				}
				num = 2;
				continue;
			case 4:
				A_0 = A_0.Replace(ClipboardData.b("ᥨὪ", a_), string.Empty);
				result = float.Parse(A_0, NumberStyles.Float, CultureInfo.InvariantCulture);
				num = 0;
				continue;
			case 6:
				return result;
			case 7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_15B;
				default:
					goto IL_A7;
				}
				break;
			case 8:
				goto IL_55;
			}
			if (A_0 == string.Empty)
			{
				num = 8;
				continue;
			}
			result = float.MaxValue;
			num = 3;
			continue;
			IL_15B:
			num = 4;
		}
		IL_55:
		return 0f;
		IL_A7:
		if (false)
		{
		}
		return result;
	}

	// Token: 0x06001F0A RID: 7946 RVA: 0x001FCF78 File Offset: 0x001FBF78
	private void ᜁ(XmlReader A_0, TextBox A_1)
	{
		int a_ = 19;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_B8:
			num = 3;
			break;
		case 1:
			goto IL_29;
		default:
			goto IL_29;
		}
		string attribute;
		string attribute2;
		for (;;)
		{
			IL_31:
			switch (num)
			{
			case 0:
				goto IL_95;
			case 1:
				goto IL_B5;
			case 2:
				A_1.Format.LineDashing = this.ᜡ(attribute);
				num = 0;
				continue;
			case 3:
				if (true)
				{
				}
				A_1.Format.LineStyle = this.ᜠ(attribute2);
				num = 5;
				continue;
			case 4:
				if (attribute != null)
				{
					num = 2;
					continue;
				}
				goto IL_95;
			case 5:
				goto IL_93;
			}
			goto IL_4F;
			IL_95:
			attribute2 = A_0.GetAttribute(ClipboardData.b("ᕸቺ፼᩾ﲄ", a_));
			num = 1;
		}
		IL_93:
		return;
		IL_B5:
		if (attribute2 != null)
		{
			goto IL_B8;
		}
		return;
		IL_29:
		if (false)
		{
		}
		IL_4F:
		attribute = A_0.GetAttribute(ClipboardData.b("ᵸ᩺๼᝾ﲄ", a_));
		num = 4;
		goto IL_31;
	}

	// Token: 0x06001F0B RID: 7947 RVA: 0x001FD074 File Offset: 0x001FC074
	private LineDashing ᜡ(string A_0)
	{
		int a_ = 5;
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
					continue;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 1:
				num = 6;
				continue;
			case 2:
				spr᧓.\u1758 = new Dictionary<string, int>(7)
				{
					{
						ClipboardData.b("婪䵬幮", a_),
						0
					},
					{
						ClipboardData.b("ཪɬ᭮", a_),
						1
					},
					{
						ClipboardData.b("ཪ౬ᱮᥰ", a_),
						2
					},
					{
						ClipboardData.b("ཪ౬ᱮᥰ㝲ᩴͶ", a_),
						3
					},
					{
						ClipboardData.b("ݪɬŮᙰ㝲ᑴѶᅸ", a_),
						4
					},
					{
						ClipboardData.b("ݪɬŮᙰ㝲ᑴѶᅸ㽺ቼ୾", a_),
						5
					},
					{
						ClipboardData.b("ݪɬŮᙰ㝲ᑴѶᅸ㽺ቼ୾얀", a_),
						6
					}
				};
				num = 5;
				continue;
			case 4:
				if (spr᧓.\u1758 == null)
				{
					num = 2;
					continue;
				}
				goto IL_16E;
			case 5:
				goto IL_16E;
			case 6:
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
					num = 0;
					continue;
				}
				break;
			}
			case 7:
				goto IL_16C;
			case 8:
			{
				int num2;
				if (spr᧓.\u1758.TryGetValue(A_0, out num2))
				{
					num = 1;
					continue;
				}
				return LineDashing.Solid;
			}
			case 9:
				if (true)
				{
				}
				num = 4;
				continue;
			}
			if (A_0 != null)
			{
				num = 9;
				continue;
			}
			return LineDashing.Solid;
			IL_16E:
			num = 8;
		}
		return LineDashing.DashDot;
		IL_16C:
		return LineDashing.Solid;
	}

	// Token: 0x06001F0C RID: 7948 RVA: 0x001FD258 File Offset: 0x001FC258
	private TextBoxLineStyle ᜠ(string A_0)
	{
		int a_ = 9;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_0 == ClipboardData.b("᭮ᥰᩲ᭴⍶ᅸቺṼᑾ", a_)))
				{
					num = 4;
					continue;
				}
				return TextBoxLineStyle.ThinThick;
			case 1:
				if (A_0 == ClipboardData.b("᭮ᥰᩲᙴᱶ⵸፺ᑼᅾ", a_))
				{
					return TextBoxLineStyle.ThickThin;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return TextBoxLineStyle.Triple;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 2:
				num = 9;
				continue;
			case 3:
				if (!(A_0 == ClipboardData.b("᭮ᥰᩲ᭴⍶ᅸቺ፼", a_)))
				{
					num = 8;
					continue;
				}
				return TextBoxLineStyle.Double;
			case 4:
				if (true)
				{
				}
				num = 1;
				continue;
			case 5:
				num = 3;
				continue;
			case 6:
				num = 10;
				continue;
			case 8:
				num = 0;
				continue;
			case 9:
				goto IL_61;
			case 10:
				if (!(A_0 == ClipboardData.b("᭮ᥰᩲᙴᱶ㭸Ṻॼࡾ펆", a_)))
				{
					num = 2;
					continue;
				}
				return TextBoxLineStyle.Triple;
			}
			if (A_0 == null)
			{
				return TextBoxLineStyle.Simple;
			}
			num = 5;
		}
		return TextBoxLineStyle.ThinThick;
		IL_61:
		return TextBoxLineStyle.Simple;
	}

	// Token: 0x06001F0D RID: 7949 RVA: 0x001FD3BC File Offset: 0x001FC3BC
	private void ᜃ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			TextBox textBox;
			for (;;)
			{
				IL_60:
				textBox = (A_1 as TextBox);
				string text = A_0.GetAttribute(ClipboardData.b("rŴ๶ᕸṺ", a_));
				int num = 3;
				for (;;)
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
						int num2;
						switch (num)
						{
						case 0:
						{
							int num3;
							if (num2 >= num3)
							{
								num = 7;
								continue;
							}
							string[] array2;
							string[] array = this.\u171D(array2[num2]);
							num = 8;
							continue;
						}
						case 1:
						{
							string[] array;
							this.ᜀ(textBox, array[0], array[1]);
							goto IL_147;
						}
						case 2:
							goto IL_93;
						case 3:
						{
							if (text == null)
							{
								num = 5;
								continue;
							}
							text = text.Trim();
							char[] separator = new char[]
							{
								';'
							};
							string[] array2 = text.Split(separator);
							string[] array = new string[2];
							num2 = 0;
							int num3 = array2.Length;
							num = 4;
							continue;
						}
						case 4:
							if (true)
							{
							}
							goto IL_D2;
						case 5:
							return;
						case 6:
							goto IL_D2;
						case 7:
							goto IL_F0;
						case 8:
						{
							string[] array;
							if (array != null)
							{
								num = 1;
								continue;
							}
							goto IL_93;
						}
						}
						goto IL_60;
						IL_93:
						num2++;
						num = 6;
						continue;
						IL_D2:
						num = 0;
						continue;
					}
					}
					IL_147:
					num = 2;
				}
			}
			return;
			IL_F0:
			this.ᜀ(A_0, textBox);
			return;
		}
		}
	}

	// Token: 0x06001F0E RID: 7950 RVA: 0x001FD534 File Offset: 0x001FC534
	private void ᜀ(XmlReader A_0, TextBox A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("᭧ṩṫŭ᭯᝱ᝳ᥵ᑷᕹ๻", a_));
				int num = 10;
				for (;;)
				{
					string attribute2;
					string attribute3;
					string attribute4;
					string attribute5;
					string attribute6;
					switch (num)
					{
					case 0:
						return;
					case 1:
						A_1.Format.FillColor = Color.Empty;
						num = 11;
						continue;
					case 2:
						A_1.Format.LineWidth = this.\u171F(attribute2);
						num = 13;
						continue;
					case 3:
						num = 16;
						continue;
					case 4:
						goto IL_2A0;
					case 5:
						if (attribute3 != null)
						{
							num = 6;
							continue;
						}
						goto IL_2A0;
					case 6:
						num = 17;
						continue;
					case 7:
						if (attribute4 != null)
						{
							num = 19;
							continue;
						}
						goto IL_228;
					case 8:
						goto IL_C7;
					case 9:
						A_1.Format.LineColor = this.ᜃ(attribute);
						num = 8;
						continue;
					case 10:
						if (attribute != null)
						{
							num = 9;
							continue;
						}
						goto IL_C7;
					case 11:
						goto IL_228;
					case 12:
						if (attribute2 != null)
						{
							num = 2;
							continue;
						}
						goto IL_121;
					case 13:
						if (true)
						{
						}
						goto IL_121;
					case 14:
						goto IL_153;
					case 15:
						if (attribute5 != null)
						{
							num = 3;
							continue;
						}
						return;
					case 16:
						A_1.Format.IsAllowInCell = !(attribute5 == ClipboardData.b("๧", a_));
						num = 0;
						continue;
					case 17:
						A_1.Format.NoLine = (attribute3 == ClipboardData.b("๧", a_));
						num = 4;
						continue;
					case 18:
						if (attribute6 == ClipboardData.b("๧", a_))
						{
							num = 1;
							continue;
						}
						goto IL_1A4;
					case 19:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_153;
						default:
							if (false)
							{
							}
							A_1.Format.FillEfects.Color = this.ᜃ(attribute4);
							A_1.Format.FillEfects.Type = BackgroundType.Color;
							num = 21;
							continue;
						}
						break;
					case 20:
						if (attribute6 != null)
						{
							num = 14;
							continue;
						}
						goto IL_1A4;
					case 21:
						goto IL_228;
					}
					break;
					IL_C7:
					attribute2 = A_0.GetAttribute(ClipboardData.b("᭧ṩṫŭ᭯᝱ͳ፵ᅷᵹᑻ੽", a_));
					num = 12;
					continue;
					IL_121:
					attribute6 = A_0.GetAttribute(ClipboardData.b("๧ͩkɭᕯᙱ", a_));
					num = 20;
					continue;
					IL_153:
					num = 18;
					continue;
					IL_1A4:
					attribute4 = A_0.GetAttribute(ClipboardData.b("๧ͩkɭ፯ᵱᡳ᥵੷", a_));
					num = 7;
					continue;
					IL_228:
					attribute3 = A_0.GetAttribute(ClipboardData.b("᭧ṩṫŭ᭯᝱ၳ", a_));
					num = 5;
					continue;
					IL_2A0:
					attribute5 = A_0.GetAttribute(ClipboardData.b("१٩kŭݯ᭱ᩳᕵᵷᙹၻ", a_), ClipboardData.b("ᵧᡩɫ呭ͯᅱᱳ፵ᕷ᭹ཻ卽黎뾑秊ꂙ욟쮡잣쎥銧얩쪫좭\ud9af톱톳", a_));
					num = 15;
				}
			}
			return;
		}
	}

	// Token: 0x06001F0F RID: 7951 RVA: 0x001FD874 File Offset: 0x001FC874
	private float \u171F(string A_0)
	{
		int a_ = 5;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_14A;
			case 1:
				goto IL_23A;
			case 2:
				if (A_0.EndsWith(ClipboardData.b("ࡪl", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_27B;
			case 3:
				goto IL_62;
			case 5:
				goto IL_1A4;
			case 6:
				if (true)
				{
				}
				if (A_0.EndsWith(ClipboardData.b("٪l", a_)))
				{
					num = 7;
					continue;
				}
				num = 8;
				continue;
			case 7:
				goto IL_116;
			case 8:
				if (A_0.EndsWith(ClipboardData.b("᭪ᕬ", a_)))
				{
					num = 1;
					continue;
				}
				num = 9;
				continue;
			case 9:
				if (A_0.EndsWith(ClipboardData.b("ɪͬ", a_)))
				{
					num = 5;
					continue;
				}
				num = 2;
				continue;
			}
			if (A_0.EndsWith(ClipboardData.b("᭪ᥬ", a_)))
			{
				num = 3;
			}
			else
			{
				num = 6;
			}
		}
		IL_62:
		goto IL_14C;
		IL_116:
		A_0 = A_0.Replace(ClipboardData.b("٪l", a_), string.Empty);
		float num2 = float.Parse(A_0, NumberStyles.Float, CultureInfo.InvariantCulture);
		return (float)spr\u1C39.ᜁ().ᜀ((double)num2, PrintUnits.Millimeter, PrintUnits.Point);
		IL_14A:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_14C:
			A_0 = A_0.Replace(ClipboardData.b("᭪ᥬ", a_), string.Empty);
			return float.Parse(A_0, NumberStyles.Float, CultureInfo.InvariantCulture);
		default:
			if (false)
			{
			}
			A_0 = A_0.Replace(ClipboardData.b("ࡪl", a_), string.Empty);
			num2 = float.Parse(A_0, NumberStyles.Float, CultureInfo.InvariantCulture);
			return (float)spr\u1C39.ᜁ().ᜀ((double)num2, PrintUnits.Centimeter, PrintUnits.Point);
		}
		IL_1A4:
		A_0 = A_0.Replace(ClipboardData.b("ɪͬ", a_), string.Empty);
		num2 = float.Parse(A_0, NumberStyles.Float, CultureInfo.InvariantCulture);
		return (float)spr\u1C39.ᜁ().ᜀ((double)num2, PrintUnits.Inch, PrintUnits.Point);
		IL_23A:
		A_0 = A_0.Replace(ClipboardData.b("᭪ᕬ", a_), string.Empty);
		num2 = float.Parse(A_0, NumberStyles.Float, CultureInfo.InvariantCulture);
		return (float)spr\u1C39.ᜁ().ᜀ((double)num2, PrintUnits.Pixel, PrintUnits.Point);
		IL_27B:
		num2 = float.Parse(A_0, NumberStyles.Float, CultureInfo.InvariantCulture);
		return num2 / 12700f;
	}

	// Token: 0x06001F10 RID: 7952 RVA: 0x001FDB18 File Offset: 0x001FCB18
	private void ᜀ(TextBox A_0, string A_1, string A_2)
	{
		int a_ = 19;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				spr᧓.\u1759 = new Dictionary<string, int>(12)
				{
					{
						ClipboardData.b("ᑸོ᩺᡾ꢄ歷", a_),
						0
					},
					{
						ClipboardData.b("ᑸོ᩺᡾ꢄﮊ", a_),
						1
					},
					{
						ClipboardData.b("๸ቺ᥼୾", a_),
						2
					},
					{
						ClipboardData.b("ᅸṺᑼ᡾", a_),
						3
					},
					{
						ClipboardData.b("͸噺ᑼᅾﶄ", a_),
						4
					},
					{
						ClipboardData.b("ᑸࡺቼ剾ﶈ벐ﮒ杖햠슢즤", a_),
						5
					},
					{
						ClipboardData.b("ᑸࡺቼ剾ﶈ벐ﺜﺞ춠", a_),
						6
					},
					{
						ClipboardData.b("ᑸࡺቼ剾ﶈ벐ﺜﺞ춠躢힤슦얨쪪\ud9ac욮잰횲", a_),
						7
					},
					{
						ClipboardData.b("ᑸࡺቼ剾ﶈ벐ﮒ杖햠슢즤誦\udba8캪솬캮얰\udab2쎴튶", a_),
						8
					},
					{
						ClipboardData.b("ᑸࡺቼ剾ꒈﮊﶎﮔ", a_),
						9
					},
					{
						ClipboardData.b("ᑸࡺቼ剾ꪆ麗ﾌﶒ", a_),
						10
					},
					{
						ClipboardData.b("ླྀ噺ॼ᩾呂ꢄ", a_),
						11
					}
				};
				num = 12;
				continue;
			case 1:
				goto IL_224;
			case 2:
				num = 1;
				continue;
			case 3:
			{
				int num2;
				if (spr᧓.\u1759.TryGetValue(A_1, out num2))
				{
					num = 11;
					continue;
				}
				goto IL_3A6;
			}
			case 4:
				if (spr᧓.\u1759 == null)
				{
					num = 0;
					continue;
				}
				goto IL_19F;
			case 5:
				goto IL_2DD;
			case 6:
				num = 4;
				continue;
			case 7:
			{
				int num2;
				switch (num2)
				{
				case 0:
					goto IL_229;
				case 1:
					goto IL_23C;
				case 2:
					goto IL_24F;
				case 3:
					goto IL_262;
				case 4:
				{
					int num3 = int.Parse(A_2, NumberStyles.Integer, CultureInfo.InvariantCulture);
					A_0.Format.OrderIndex = num3;
					num = 8;
					continue;
				}
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_30F;
					}
					break;
				case 6:
					goto IL_328;
				case 7:
					goto IL_33B;
				case 8:
					goto IL_34E;
				case 9:
					goto IL_361;
				case 10:
					goto IL_37A;
				case 11:
					goto IL_393;
				}
				num = 2;
				continue;
			}
			case 8:
			{
				int num3;
				A_0.Format.IsBelowText = (num3 <= 0);
				num = 10;
				continue;
			}
			case 9:
				if (true)
				{
				}
				break;
			case 10:
				if (A_0.Format.IsBelowText)
				{
					num = 5;
					continue;
				}
				goto IL_2EC;
			case 11:
				num = 7;
				continue;
			case 12:
				goto IL_19F;
			}
			if (A_1 != null)
			{
				num = 6;
				continue;
			}
			break;
			IL_19F:
			num = 3;
		}
		IL_224:
		goto IL_3A6;
		IL_229:
		A_0.Format.HorizontalPosition = this.ᜄ(A_2);
		return;
		IL_23C:
		A_0.Format.VerticalPosition = this.ᜄ(A_2);
		return;
		IL_24F:
		A_0.Format.Width = this.ᜄ(A_2);
		return;
		IL_262:
		A_0.Format.Height = this.ᜄ(A_2);
		return;
		IL_2DD:
		A_0.Format.TextWrappingStyle = TextWrappingStyle.Behind;
		return;
		IL_2EC:
		A_0.Format.TextWrappingStyle = TextWrappingStyle.InFrontOfText;
		return;
		IL_30F:
		if (false)
		{
		}
		A_0.Format.HorizontalAlignment = this.\u171B(A_2);
		return;
		IL_328:
		A_0.Format.VerticalAlignment = this.\u1719(A_2);
		return;
		IL_33B:
		A_0.Format.VerticalOrigin = this.\u1718(A_2);
		return;
		IL_34E:
		A_0.Format.HorizontalOrigin = this.\u171A(A_2);
		return;
		IL_361:
		A_0.Format.HorizontalRelativePercent = this.ᜄ(A_2) / 10f;
		return;
		IL_37A:
		A_0.Format.VerticalRelativePercent = this.ᜄ(A_2) / 10f;
		return;
		IL_393:
		A_0.Format.TextAnchor = this.\u171E(A_2);
		return;
		IL_3A6:
		A_0.Format.DocxStyleProps.Add(A_1 + ClipboardData.b("䍸", a_) + A_2);
	}

	// Token: 0x06001F11 RID: 7953 RVA: 0x001FDEF0 File Offset: 0x001FCEF0
	private ShapeVerticalAlignment \u171E(string A_0)
	{
		int a_ = 2;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 1:
				if (!(A_0 == ClipboardData.b("էͩ࡫੭ᱯ᝱", a_)))
				{
					num = 0;
					continue;
				}
				return ShapeVerticalAlignment.Center;
			case 3:
				num = 1;
				continue;
			case 4:
				if (!(A_0 == ClipboardData.b("ᱧթᱫ", a_)))
				{
					num = 8;
					continue;
				}
				return ShapeVerticalAlignment.Top;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_45;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 6:
				goto IL_7D;
			case 7:
				if (!(A_0 == ClipboardData.b("੧թᡫᩭὯά", a_)))
				{
					num = 5;
					continue;
				}
				return ShapeVerticalAlignment.Bottom;
			case 8:
				num = 7;
				continue;
			}
			goto IL_3D;
			IL_45:
			num = 3;
			continue;
			IL_3D:
			if (A_0 != null)
			{
				goto IL_45;
			}
			return ShapeVerticalAlignment.None;
		}
		return ShapeVerticalAlignment.Center;
		IL_7D:
		return ShapeVerticalAlignment.None;
	}

	// Token: 0x06001F12 RID: 7954 RVA: 0x001FE01C File Offset: 0x001FD01C
	private string[] \u171D(string A_0)
	{
		int a_ = 18;
		if (true)
		{
		}
		A_0 = A_0.Replace(ClipboardData.b("ࡷ๹", a_), string.Empty);
		char[] separator = new char[]
		{
			':'
		};
		string[] array = A_0.Split(separator);
		if (array.Length == 2)
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
				return array;
			}
		}
		return null;
	}

	// Token: 0x06001F13 RID: 7955 RVA: 0x001FE09C File Offset: 0x001FD09C
	private Spire.Doc.Documents.Converters.ShapeType ᜀ(MemoryStream A_0)
	{
		int a_ = 11;
		switch (0)
		{
		default:
			for (;;)
			{
				XmlReader xmlReader = spr\u23D7.ᜀ(A_0);
				int num = 16;
				for (;;)
				{
					string text;
					bool flag;
					string text2;
					string text3;
					string text4;
					string text5;
					bool flag2;
					bool flag3;
					switch (num)
					{
					case 0:
						if (spr᧓.\u175A == null)
						{
							num = 62;
							continue;
						}
						goto IL_56D;
					case 1:
						if (xmlReader.NodeType == XmlNodeType.Element)
						{
							num = 36;
							continue;
						}
						goto IL_2D5;
					case 2:
						if (!(text == ClipboardData.b("剰Ⱳ൴䝶䥸䭺䵼⁾놂떄떆", a_)))
						{
							num = 21;
							continue;
						}
						return Spire.Doc.Documents.Converters.ShapeType.TextboxShape;
					case 3:
					{
						string localName;
						int num2;
						if (spr᧓.\u175A.TryGetValue(localName, out num2))
						{
							num = 50;
							continue;
						}
						goto IL_2D5;
					}
					case 4:
						if (xmlReader.LocalName != ClipboardData.b("ɰ᭲ᑴݶᱸ", a_))
						{
							num = 32;
							continue;
						}
						goto IL_6EE;
					case 5:
						num = 55;
						continue;
					case 6:
						if (flag)
						{
							num = 37;
							continue;
						}
						goto IL_2AB;
					case 7:
						goto IL_2D5;
					case 8:
						if (text2 != null)
						{
							num = 39;
							continue;
						}
						goto IL_6A7;
					case 9:
						num = 18;
						continue;
					case 10:
						num = 48;
						continue;
					case 11:
						if (text3 != null)
						{
							num = 47;
							continue;
						}
						goto IL_608;
					case 12:
						num = 20;
						continue;
					case 13:
						num = 40;
						continue;
					case 14:
						return Spire.Doc.Documents.Converters.ShapeType.PictureShape;
					case 15:
						if (flag)
						{
							num = 13;
							continue;
						}
						goto IL_7D6;
					case 16:
						if (xmlReader.LocalName != ClipboardData.b("ŰᩲᙴͶ", a_))
						{
							num = 61;
							continue;
						}
						goto IL_6EE;
					case 17:
						if (!flag)
						{
							num = 52;
							continue;
						}
						goto IL_4B1;
					case 18:
						if (!flag)
						{
							num = 14;
							continue;
						}
						goto IL_59F;
					case 19:
						goto IL_2D5;
					case 20:
						if (text4 == ClipboardData.b("⹰୲䕴䝶䥸䭺≼୾뚀뚂", a_))
						{
							num = 28;
							continue;
						}
						goto IL_59F;
					case 21:
						goto IL_2AB;
					case 22:
						goto IL_7D1;
					case 23:
						if (text == ClipboardData.b("剰Ⱳ൴䝶䥸䭺䵼⁾뒂낄", a_))
						{
							num = 5;
							continue;
						}
						goto IL_59F;
					case 24:
						if (text4 != null)
						{
							num = 10;
							continue;
						}
						goto IL_59F;
					case 25:
						num = 0;
						continue;
					case 26:
						goto IL_5BD;
					case 27:
						goto IL_2D5;
					case 28:
						num = 23;
						continue;
					case 29:
						goto IL_6A7;
					case 30:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5BD;
						default:
							if (false)
							{
							}
							if (text == ClipboardData.b("剰Ⱳ൴䝶䥸䭺䵼⁾뒂낄", a_))
							{
								num = 54;
								continue;
							}
							goto IL_4B1;
						}
						break;
					case 31:
						if (text5 != null)
						{
							num = 41;
							continue;
						}
						goto IL_853;
					case 32:
						goto IL_333;
					case 33:
					{
						string localName;
						if ((localName = xmlReader.LocalName) != null)
						{
							num = 25;
							continue;
						}
						goto IL_2D5;
					}
					case 34:
						goto IL_56D;
					case 35:
						num = 11;
						continue;
					case 36:
						num = 33;
						continue;
					case 37:
						num = 2;
						continue;
					case 38:
						if (this.ᜎ.ContainsKey(text5))
						{
							num = 56;
							continue;
						}
						goto IL_853;
					case 39:
						num = 53;
						continue;
					case 40:
						if (flag2)
						{
							num = 22;
							continue;
						}
						goto IL_7D6;
					case 41:
						num = 38;
						continue;
					case 42:
						goto IL_2D5;
					case 43:
						goto IL_2D5;
					case 44:
						if (text3.StartsWith(ClipboardData.b("♰ᱲݴ፶⥸ቺṼ୾킆ﾊﶎﲐﲖ", a_)))
						{
							num = 63;
							continue;
						}
						goto IL_608;
					case 45:
					{
						int num2;
						switch (num2)
						{
						case 0:
							return Spire.Doc.Documents.Converters.ShapeType.GroupedShape;
						case 1:
							return Spire.Doc.Documents.Converters.ShapeType.OleObject;
						case 2:
							text = xmlReader.GetAttribute(ClipboardData.b("հੲմቶ", a_));
							text3 = xmlReader.GetAttribute(ClipboardData.b("ᡰᝲ", a_));
							num = 43;
							continue;
						case 3:
						case 4:
							flag = true;
							num = 7;
							continue;
						case 5:
							flag2 = true;
							num = 27;
							continue;
						case 6:
							text4 = xmlReader.GetAttribute(ClipboardData.b("ᡰᝲ", a_));
							num = 19;
							continue;
						case 7:
							flag3 = true;
							text5 = xmlReader.GetAttribute(ClipboardData.b("ᡰᝲ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲\udab4톶\udfb8튺\udebc\udabe藀곂ꛄ닆꓈껊ꏌ믎ﻐ꿜뫞跠苢釤軦蛨藪黬蟮飰菲蛴", a_));
							text2 = xmlReader.GetAttribute(ClipboardData.b("ᥰŲၴᅶ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲\udab4톶\udfb8튺\udebc\udabe藀곂ꛄ닆꓈껊ꏌ믎ﻐ꿜뫞跠苢釤軦蛨藪黬蟮飰菲蛴", a_));
							num = 46;
							continue;
						case 8:
						case 9:
							return Spire.Doc.Documents.Converters.ShapeType.XmlParagraphItem;
						default:
							num = 49;
							continue;
						}
						break;
					}
					case 46:
						goto IL_2D5;
					case 47:
						num = 51;
						continue;
					case 48:
						if (text != null)
						{
							num = 12;
							continue;
						}
						goto IL_59F;
					case 49:
						num = 42;
						continue;
					case 50:
						num = 45;
						continue;
					case 51:
						if (!text3.StartsWith(ClipboardData.b("ⅰᱲɴቶ୸⭺ᅼ੾풂力삌\uda94ﺚﺜ", a_)))
						{
							num = 59;
							continue;
						}
						return Spire.Doc.Documents.Converters.ShapeType.WatermarkShape;
					case 52:
						return Spire.Doc.Documents.Converters.ShapeType.PictureShape;
					case 53:
						if (!(text2 != string.Empty))
						{
							num = 29;
							continue;
						}
						return Spire.Doc.Documents.Converters.ShapeType.PictureShape;
					case 54:
						num = 17;
						continue;
					case 55:
						if (flag3)
						{
							num = 9;
							continue;
						}
						goto IL_59F;
					case 56:
						goto IL_73E;
					case 57:
						goto IL_2D5;
					case 58:
						if (!xmlReader.Read())
						{
							num = 35;
							continue;
						}
						num = 1;
						continue;
					case 59:
						num = 44;
						continue;
					case 60:
						if (text4 == null)
						{
							num = 26;
							continue;
						}
						goto IL_4B1;
					case 61:
						num = 4;
						continue;
					case 62:
						spr᧓.\u175A = new Dictionary<string, int>(10)
						{
							{
								ClipboardData.b("ᙰŲᩴɶॸ", a_),
								0
							},
							{
								ClipboardData.b("㹰㽲ぴ㡶᭸ᅺ᡼᱾", a_),
								1
							},
							{
								ClipboardData.b("ɰ᭲ᑴݶᱸ", a_),
								2
							},
							{
								ClipboardData.b("հᙲ൴Ͷ᭸ᑺռ", a_),
								3
							},
							{
								ClipboardData.b("ͰᙲᙴͶ", a_),
								4
							},
							{
								ClipboardData.b("հ୲᝴ྲྀ㩸ᑺ፼୾", a_),
								5
							},
							{
								ClipboardData.b("ɰ᭲ᑴݶᱸེѼཾ", a_),
								6
							},
							{
								ClipboardData.b("ᡰṲᑴၶᱸὺᱼ୾", a_),
								7
							},
							{
								ClipboardData.b("Ͱᱲt᥶ᵸॺ᡼᱾", a_),
								8
							},
							{
								ClipboardData.b("Ṱղᑴ᭶", a_),
								9
							}
						};
						num = 34;
						continue;
					case 63:
						goto IL_6A5;
					}
					break;
					IL_2AB:
					num = 15;
					continue;
					IL_2D5:
					num = 58;
					continue;
					IL_4B1:
					num = 8;
					continue;
					IL_56D:
					num = 3;
					continue;
					IL_59F:
					num = 60;
					continue;
					IL_5BD:
					num = 30;
					continue;
					IL_608:
					num = 6;
					continue;
					IL_6A7:
					num = 31;
					continue;
					IL_6EE:
					text3 = null;
					text = null;
					text4 = null;
					text5 = null;
					text2 = null;
					flag3 = false;
					flag = false;
					flag2 = false;
					num = 57;
					continue;
					IL_7D6:
					num = 24;
				}
			}
			return Spire.Doc.Documents.Converters.ShapeType.GroupedShape;
			IL_333:
			throw new XmlException(ClipboardData.b("ŰᩲᙴͶ౸ॺ᡼彾ꮊﺒ練", a_));
			IL_6A5:
			return Spire.Doc.Documents.Converters.ShapeType.WatermarkShape;
			IL_73E:
			return Spire.Doc.Documents.Converters.ShapeType.PictureShape;
			IL_7D1:
			return Spire.Doc.Documents.Converters.ShapeType.TextboxShape;
			IL_853:
			if (true)
			{
			}
			return Spire.Doc.Documents.Converters.ShapeType.XmlParagraphItem;
		}
	}

	// Token: 0x06001F14 RID: 7956 RVA: 0x001FE908 File Offset: 0x001FD908
	private ParagraphBase ᜀ(XmlReader A_0, ParagraphItemCollection A_1, ref MemoryStream A_2)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 71;
			XmlReader xmlReader;
			DocPicture docPicture;
			for (;;)
			{
				string attribute4;
				string attribute7;
				bool flag3;
				string attribute10;
				bool flag5;
				bool flag6;
				bool flag7;
				bool flag8;
				switch (num)
				{
				case 0:
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						num = 64;
						continue;
					}
					xmlReader.Read();
					num = 85;
					continue;
				case 1:
					goto IL_8F8;
				case 2:
					goto IL_58F;
				case 3:
					goto IL_BE9;
				case 4:
					goto IL_9AB;
				case 5:
				{
					MemoryStream a_2 = this.ᜢ(xmlReader);
					this.ᜀ(a_2, docPicture);
					num = 1;
					continue;
				}
				case 6:
					goto IL_985;
				case 7:
					num = 29;
					continue;
				case 8:
				{
					string attribute = xmlReader.GetAttribute(ClipboardData.b("ᅥ", a_));
					string attribute2 = xmlReader.GetAttribute(ClipboardData.b("եէᩩ࡫", a_));
					bool flag = false;
					xmlReader.Read();
					num = 24;
					continue;
				}
				case 9:
				{
					string attribute2;
					if (attribute2 != null)
					{
						num = 54;
						continue;
					}
					goto IL_88F;
				}
				case 10:
					goto IL_985;
				case 11:
					goto IL_D2C;
				case 12:
				{
					string a_3;
					docPicture.Borders.Color = this.ᜃ(a_3);
					string attribute3;
					docPicture.Borders.ColorShemeName = attribute3;
					num = 52;
					continue;
				}
				case 13:
					if (!this.ᜋ.StartsWith(ClipboardData.b("๥൧୩࡫୭ɯ", a_)))
					{
						num = 62;
						continue;
					}
					goto IL_27F;
				case 14:
					goto IL_A0D;
				case 15:
					goto IL_65D;
				case 16:
					goto IL_1E2;
				case 17:
				{
					bool flag;
					if (flag)
					{
						num = 35;
						continue;
					}
					goto IL_88F;
				}
				case 18:
					num = 34;
					continue;
				case 19:
					if (!this.ᜋ.StartsWith(ClipboardData.b("eݧթᡫ୭ɯ", a_)))
					{
						num = 74;
						continue;
					}
					goto IL_27F;
				case 20:
					goto IL_985;
				case 21:
				{
					bool flag;
					if (flag)
					{
						num = 40;
						continue;
					}
					goto IL_CBB;
				}
				case 22:
					goto IL_985;
				case 23:
				{
					bool flag;
					if (flag)
					{
						num = 66;
						continue;
					}
					goto IL_985;
				}
				case 24:
					goto IL_58F;
				case 25:
					docPicture.CropFromRight = float.Parse(attribute4, NumberStyles.Float, CultureInfo.InvariantCulture) / 100000f;
					num = 3;
					continue;
				case 26:
					num = 59;
					continue;
				case 27:
				{
					string localName;
					if (!(localName == ClipboardData.b("ࡥݧⱩիɭᱯ", a_)))
					{
						num = 84;
						continue;
					}
					bool flag = false;
					num = 22;
					continue;
				}
				case 28:
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						num = 31;
						continue;
					}
					xmlReader.Read();
					num = 58;
					continue;
				case 29:
				{
					string attribute5;
					if (attribute5.Trim().Length > 0)
					{
						num = 57;
						continue;
					}
					goto IL_65D;
				}
				case 30:
				{
					string attribute6;
					if (attribute6 != null)
					{
						num = 42;
						continue;
					}
					goto IL_985;
				}
				case 31:
				{
					bool flag2 = this.ᜄ(xmlReader, A_1);
					xmlReader = spr\u23D7.ᜀ(A_2);
					num = 49;
					continue;
				}
				case 32:
				{
					string attribute;
					docPicture.Borders.LineWidth = Convert.ToSingle(attribute) / 12700f;
					num = 50;
					continue;
				}
				case 33:
					num = 27;
					continue;
				case 34:
					if (attribute4.Trim().Length > 0)
					{
						num = 25;
						continue;
					}
					goto IL_BE9;
				case 35:
					goto IL_CDA;
				case 36:
					num = 63;
					continue;
				case 37:
					if (attribute7.Trim().Length > 0)
					{
						num = 65;
						continue;
					}
					goto IL_D2C;
				case 38:
				{
					string attribute8;
					Image image = this.ᜀ(attribute8, flag3, false);
					num = 97;
					continue;
				}
				case 39:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᕥݧ٩ի੭㙯᭱ᡳ᩵", a_)))
					{
						num = 56;
						continue;
					}
					bool flag = true;
					docPicture.Borders.BorderType = BorderStyle.Single;
					docPicture.Borders.LineWidth = 0.5f;
					num = 6;
					continue;
				}
				case 40:
					xmlReader.Read();
					num = 2;
					continue;
				case 41:
				{
					string attribute3;
					if (attribute3 != null)
					{
						num = 53;
						continue;
					}
					goto IL_985;
				}
				case 42:
					num = 23;
					continue;
				case 43:
					if (attribute7 != null)
					{
						num = 68;
						continue;
					}
					goto IL_D2C;
				case 44:
					num = 79;
					continue;
				case 45:
					if (attribute4 != null)
					{
						num = 18;
						continue;
					}
					goto IL_BE9;
				case 46:
				{
					string attribute9;
					this.ᜀ(docPicture, attribute9, flag3, false);
					num = 88;
					continue;
				}
				case 47:
					if (attribute10 != null)
					{
						num = 44;
						continue;
					}
					goto IL_A0D;
				case 48:
					num = 67;
					continue;
				case 49:
					goto IL_D03;
				case 50:
					goto IL_7F9;
				case 51:
					num = 20;
					continue;
				case 52:
					goto IL_985;
				case 53:
					num = 70;
					continue;
				case 54:
				{
					string attribute2;
					docPicture.Borders.BorderType = this.ᜊ(attribute2);
					num = 87;
					continue;
				}
				case 55:
				{
					string attribute5 = xmlReader.GetAttribute(ClipboardData.b("ѥ", a_));
					num = 89;
					continue;
				}
				case 56:
					num = 77;
					continue;
				case 57:
				{
					string attribute5;
					docPicture.CropFromBottom = float.Parse(attribute5, NumberStyles.Float, CultureInfo.InvariantCulture) / 100000f;
					num = 15;
					continue;
				}
				case 58:
					goto IL_A47;
				case 59:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᕥ୧ɩ५ͭᕯㅱᡳѵ", a_)))
					{
						num = 36;
						continue;
					}
					string attribute3 = xmlReader.GetAttribute(ClipboardData.b("ၥ१٩", a_));
					num = 41;
					continue;
				}
				case 60:
					goto IL_A47;
				case 61:
				{
					bool flag;
					if (flag)
					{
						num = 12;
						continue;
					}
					goto IL_985;
				}
				case 62:
					num = 19;
					continue;
				case 63:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᕥᩧ൩๫⵭ᱯq", a_)))
					{
						num = 51;
						continue;
					}
					string attribute6 = xmlReader.GetAttribute(ClipboardData.b("ၥ१٩", a_));
					num = 30;
					continue;
				}
				case 64:
				{
					bool flag4 = this.\u1718(xmlReader);
					num = 69;
					continue;
				}
				case 65:
					docPicture.CropFromTop = float.Parse(attribute7, NumberStyles.Float, CultureInfo.InvariantCulture) / 100000f;
					num = 11;
					continue;
				case 66:
				{
					string attribute6;
					docPicture.Borders.Color = this.ᜃ(attribute6);
					num = 10;
					continue;
				}
				case 67:
					if (!(xmlReader.LocalName != ClipboardData.b("੥٧", a_)))
					{
						num = 96;
						continue;
					}
					num = 73;
					continue;
				case 68:
					num = 37;
					continue;
				case 69:
				{
					bool flag4;
					if (flag4)
					{
						num = 82;
						continue;
					}
					goto IL_D85;
				}
				case 70:
				{
					string attribute3;
					if (this.ᜄ.ColorScheme.ContainsKey(attribute3))
					{
						num = 75;
						continue;
					}
					goto IL_985;
				}
				case 72:
					if (true)
					{
					}
					flag5 = false;
					goto IL_4F0;
				case 73:
				{
					string localName;
					if ((localName = xmlReader.LocalName) != null)
					{
						num = 33;
						continue;
					}
					goto IL_985;
				}
				case 74:
					num = 72;
					continue;
				case 75:
				{
					string attribute3;
					string a_3 = this.ᜄ.ColorScheme[attribute3];
					num = 61;
					continue;
				}
				case 76:
					goto IL_985;
				case 77:
				{
					string localName;
					if (!(localName == ClipboardData.b("ťᩧ୩࡫⡭᥯ṱᡳ", a_)))
					{
						num = 26;
						continue;
					}
					bool flag = true;
					docPicture.Borders.BorderType = BorderStyle.Single;
					docPicture.Borders.LineWidth = 0.5f;
					num = 76;
					continue;
				}
				case 78:
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						num = 48;
						continue;
					}
					goto IL_CBB;
				case 79:
					if (attribute10.Trim().Length > 0)
					{
						num = 83;
						continue;
					}
					goto IL_A0D;
				case 80:
					goto IL_8C4;
				case 81:
					if (flag6)
					{
						num = 5;
						continue;
					}
					goto IL_8F8;
				case 82:
				{
					docPicture = new DocPicture(this.ᜄ);
					xmlReader.ReadToFollowing(ClipboardData.b("ѥѧͩᱫ", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧캩\udeab쾭잯\udbb1\udab3통햷횹鎻貽ꗇꯉꗋꃍ", a_));
					string attribute9 = xmlReader.GetAttribute(ClipboardData.b("ͥէࡩ५੭", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧얩쪫좭\ud9af톱톳ힷ\ud9b9즻펽ꖿ곁냃難韛ﳋￏꃑ뇓뫕맗껙뗛뇝軟釡賣迥飧駩", a_));
					string attribute8 = xmlReader.GetAttribute(ClipboardData.b("੥ŧѩݫ", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧얩쪫좭\ud9af톱톳ힷ\ud9b9즻펽ꖿ곁냃難韛ﳋￏꃑ뇓뫕맗껙뗛뇝軟釡賣迥飧駩", a_));
					num = 13;
					continue;
				}
				case 83:
					docPicture.CropFromLeft = float.Parse(attribute10, NumberStyles.Float, CultureInfo.InvariantCulture) / 100000f;
					num = 14;
					continue;
				case 84:
					num = 39;
					continue;
				case 85:
					goto IL_D03;
				case 86:
					flag5 = true;
					goto IL_4F0;
				case 87:
					goto IL_88F;
				case 88:
					goto IL_9AB;
				case 89:
				{
					string attribute5;
					if (attribute5 != null)
					{
						num = 7;
						continue;
					}
					goto IL_65D;
				}
				case 90:
				{
					string attribute;
					if (attribute != null)
					{
						num = 32;
						continue;
					}
					goto IL_7F9;
				}
				case 91:
					if (flag7)
					{
						num = 8;
						continue;
					}
					goto IL_88F;
				case 92:
					if (!flag8)
					{
						goto IL_D2C;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CDA;
					default:
						if (false)
						{
						}
						num = 55;
						continue;
					}
					break;
				case 93:
				{
					Image image;
					docPicture.LoadImage(image);
					num = 4;
					continue;
				}
				case 94:
				{
					string attribute9;
					if (attribute9 != null)
					{
						num = 46;
						continue;
					}
					num = 98;
					continue;
				}
				case 95:
				{
					bool flag2;
					if (flag2)
					{
						num = 80;
						continue;
					}
					goto IL_A04;
				}
				case 96:
					goto IL_CBB;
				case 97:
				{
					Image image;
					if (image != null)
					{
						num = 93;
						continue;
					}
					goto IL_9A9;
				}
				case 98:
				{
					string attribute8;
					if (attribute8 != null)
					{
						num = 38;
						continue;
					}
					goto IL_65B;
				}
				}
				if (A_0.LocalName != ClipboardData.b("ɥᩧ୩᭫ݭṯᕱ", a_))
				{
					num = 16;
					continue;
				}
				A_2 = this.ᜢ(A_0);
				xmlReader = spr\u23D7.ᜀ(A_2);
				num = 60;
				continue;
				IL_27F:
				num = 86;
				continue;
				IL_4F0:
				flag3 = flag5;
				num = 94;
				continue;
				IL_58F:
				num = 78;
				continue;
				IL_65D:
				attribute10 = xmlReader.GetAttribute(ClipboardData.b("੥", a_));
				num = 47;
				continue;
				IL_7F9:
				num = 9;
				continue;
				IL_88F:
				xmlReader.Close();
				A_2.Position = 0L;
				xmlReader = spr\u23D7.ᜀ(A_2);
				num = 95;
				continue;
				IL_8F8:
				A_2.Position = 0L;
				xmlReader = spr\u23D7.ᜀ(A_2);
				flag8 = xmlReader.ReadToFollowing(ClipboardData.b("ᕥᩧ३㹫୭፯ٱ", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧캩\udeab쾭잯\udbb1\udab3통햷횹鎻貽ꗇꯉꗋꃍ", a_));
				num = 92;
				continue;
				IL_985:
				num = 21;
				continue;
				IL_9AB:
				A_2.Position = 0L;
				xmlReader = spr\u23D7.ᜀ(A_2);
				flag6 = xmlReader.ReadToFollowing(ClipboardData.b("եѧᡩ⽫٭ᅯᱱ፳፵", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧캩\udeab쾭잯\udbb1\udab3통햷횹鎻貽ꗇꯉꗋꃍ", a_));
				num = 81;
				continue;
				IL_A0D:
				attribute4 = xmlReader.GetAttribute(ClipboardData.b("ᑥ", a_));
				num = 45;
				continue;
				IL_A47:
				num = 28;
				continue;
				IL_BE9:
				attribute7 = xmlReader.GetAttribute(ClipboardData.b("ብ", a_));
				num = 43;
				continue;
				IL_CBB:
				num = 17;
				continue;
				IL_CDA:
				num = 90;
				continue;
				IL_D03:
				num = 0;
				continue;
				IL_D2C:
				A_2.Position = 0L;
				xmlReader = spr\u23D7.ᜀ(A_2);
				flag7 = xmlReader.ReadToFollowing(ClipboardData.b("੥٧", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧캩\udeab쾭잯\udbb1\udab3통햷횹鎻貽ꗇꯉꗋꃍ", a_));
				num = 91;
			}
			IL_1E2:
			throw new XmlException(ClipboardData.b("≥ᩧ୩᭫ݭṯᕱ味፵ᑷόᅻ᭽", a_));
			IL_65B:
			return null;
			IL_8C4:
			this.ᜀ(this.ᜄ(xmlReader, docPicture), A_1);
			FieldMark a_4 = new FieldMark(this.ᜄ, FieldMarkType.FieldEnd);
			this.ᜀ(a_4, A_1);
			return null;
			IL_9A9:
			return null;
			IL_A04:
			return this.ᜄ(xmlReader, docPicture);
			IL_D85:
			A_2.Position = 0L;
			return this.ᜃ(A_2);
		}
		}
	}

	// Token: 0x06001F15 RID: 7957 RVA: 0x001FF6AC File Offset: 0x001FE6AC
	private void ᜀ(MemoryStream A_0, IDocumentObject A_1)
	{
		int a_ = 10;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_3C:
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_13D:
					num = 6;
					break;
				case 1:
					goto IL_5C;
				default:
					goto IL_5C;
				}
				XmlReader xmlReader;
				bool flag2;
				for (;;)
				{
					IL_19:
					switch (num)
					{
					case 0:
					{
						bool flag;
						if (flag)
						{
							num = 5;
							continue;
						}
						return;
					}
					case 1:
						return;
					case 2:
						goto IL_13A;
					case 3:
						if (A_1 is DocPicture)
						{
							num = 4;
							continue;
						}
						return;
					case 4:
						flag2 = xmlReader.ReadToFollowing(ClipboardData.b("፯ṱٳ≵᝷", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱킳쒵\ud9b7춹햻킽ꞿ꿁ꣃ難韛ﳋￏ뿑뗓뿕뛗", a_));
						num = 2;
						continue;
					case 5:
					{
						string attribute = xmlReader.GetAttribute(ClipboardData.b("ٯ፱ᡳ", a_));
						(A_1 as DocPicture).Chromakey = this.ᜃ(attribute);
						num = 1;
						continue;
					}
					case 6:
					{
						bool flag = xmlReader.ReadToFollowing(ClipboardData.b("ͯq፳ᑵ㭷ᙹ๻", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱킳쒵\ud9b7춹햻킽ꞿ꿁ꣃ難韛ﳋￏ뿑뗓뿕뛗", a_));
						if (true)
						{
						}
						num = 0;
						continue;
					}
					}
					goto IL_3C;
				}
				IL_13A:
				if (flag2)
				{
					goto IL_13D;
				}
				break;
				IL_5C:
				if (false)
				{
				}
				xmlReader = spr\u23D7.ᜀ(A_0);
				num = 3;
				goto IL_19;
			}
			return;
		}
	}

	// Token: 0x06001F16 RID: 7958 RVA: 0x001FF808 File Offset: 0x001FE808
	private bool ᜄ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 15;
		for (;;)
		{
			A_0.ReadToFollowing(ClipboardData.b("ᵴ᭶ၸᕺᙼ㱾", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶\uddb8즺\udcbc좾ꣀ귂ꋄ꫆ꗈￌￎ䀹뫖룘닚돜", a_));
			if (A_0.LocalName == ClipboardData.b("ᵴ᭶ၸᕺᙼ㱾", a_))
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_7B;
			}
		}
		if (true)
		{
		}
		this.\u170D(A_0, A_1);
		return true;
		IL_7B:
		if (false)
		{
		}
		return false;
	}

	// Token: 0x06001F17 RID: 7959 RVA: 0x001FF898 File Offset: 0x001FE898
	private Image \u171C(string A_0)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 1;
			Image result;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_53;
				case 2:
					try
					{
						num = 9;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_1A9;
							case 1:
								goto IL_1A9;
							case 2:
								goto IL_1B5;
							case 3:
								if (A_0.StartsWith(ClipboardData.b("ŵཷ൹", a_)))
								{
									num = 8;
									continue;
								}
								num = 6;
								continue;
							case 4:
								A_0 = A_0.Replace(ClipboardData.b("ၵᅷᙹ᥻䑽꽿궁", a_), string.Empty);
								num = 5;
								continue;
							case 5:
								goto IL_109;
							case 6:
								if (A_0.StartsWith(ClipboardData.b("ၵᅷᙹ᥻䑽꽿궁", a_)))
								{
									num = 4;
									continue;
								}
								goto IL_109;
							case 7:
								num = 3;
								continue;
							case 8:
								goto IL_151;
							}
							if (!A_0.StartsWith(ClipboardData.b("ṵ౷๹౻", a_)))
							{
								num = 7;
								continue;
							}
							goto IL_151;
							IL_109:
							result = Image.FromFile(A_0);
							num = 1;
							continue;
							IL_151:
							HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(A_0);
							httpWebRequest.AllowWriteStreamBuffering = true;
							WebResponse response = httpWebRequest.GetResponse();
							Stream responseStream = response.GetResponseStream();
							result = Image.FromStream(responseStream);
							response.Close();
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
							IL_1A9:
							num = 2;
						}
						IL_1B5:
						goto IL_1E9;
					}
					catch
					{
						new FileLoadException(ClipboardData.b("㕵᥷ᑹ孻੽ꁿꪉ몕뢗뺝풟쪡춣향袧\udfa9\udeab슭誯銱", a_) + A_0);
						goto IL_1E9;
					}
					goto IL_1D4;
				}
				if (string.IsNullOrEmpty(A_0))
				{
					num = 0;
					continue;
				}
				IL_1D4:
				result = null;
				num = 2;
			}
			IL_53:
			return null;
			IL_1E9:
			if (true)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x06001F18 RID: 7960 RVA: 0x001FFAB4 File Offset: 0x001FEAB4
	private spr\u24D5 ᜃ(Stream A_0)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			spr\u24D5 spr_u24D;
			for (;;)
			{
				spr_u24D = new spr\u24D5(A_0, this.ᜄ);
				spr_u24D.ᜀ(this.\u1716);
				List<string> list = this.ᜂ(A_0);
				int num = 6;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
					{
						string text;
						DictionaryEntry value;
						spr_u24D.ᜁ().Add(text, value);
						num = 2;
						continue;
					}
					case 1:
						goto IL_ED;
					case 2:
						goto IL_1E7;
					case 3:
					{
						DocPicture docPicture = new DocPicture(this.ᜄ);
						MemoryStream memoryStream;
						memoryStream.Position = 0L;
						XmlReader a_2 = spr\u23D7.ᜀ(memoryStream);
						this.ᜉ(a_2, docPicture);
						DocPicture docPicture2 = docPicture;
						docPicture2.IsShape = true;
						this.ᜊ(a_2, docPicture);
						spr_u24D.ᜀ(docPicture2);
						num = 28;
						continue;
					}
					case 4:
					{
						string text;
						DictionaryEntry value = this.ᜎ[text];
						num = 24;
						continue;
					}
					case 5:
					{
						string text;
						Dictionary<string, DictionaryEntry> dictionary;
						DictionaryEntry value = dictionary[text];
						num = 22;
						continue;
					}
					case 6:
						if (list.Count > 0)
						{
							num = 26;
							continue;
						}
						return spr_u24D;
					case 7:
						num = 13;
						continue;
					case 8:
					{
						Spire.Doc.Documents.Converters.ShapeType shapeType;
						if (shapeType == Spire.Doc.Documents.Converters.ShapeType.PictureShape)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						return spr_u24D;
					}
					case 9:
						if (spr_u24D.ᜇ().Count > 0)
						{
							num = 21;
							continue;
						}
						return spr_u24D;
					case 10:
					{
						string text;
						if (this.ᜎ.ContainsKey(text))
						{
							num = 4;
							continue;
						}
						goto IL_111;
					}
					case 11:
					{
						string text;
						if (!spr_u24D.ᜁ().ContainsKey(text))
						{
							num = 0;
							continue;
						}
						goto IL_1E7;
					}
					case 12:
					{
						this.ᜀ(spr_u24D, list);
						num2 = 0;
						int count = list.Count;
						num = 1;
						continue;
					}
					case 13:
					{
						XmlReader xmlReader;
						if (xmlReader.LocalName == ClipboardData.b("ᩨͪ౬Ὦᑰ", a_))
						{
							num = 17;
							continue;
						}
						return spr_u24D;
					}
					case 14:
						num = 18;
						continue;
					case 15:
					{
						XmlReader xmlReader;
						if (xmlReader.ReadToFollowing(ClipboardData.b("ᩨͪ౬Ὦᑰ", a_), ClipboardData.b("ᱨᥪͬ啮ɰၲᵴቶᑸ᩺๼剾뺒ꆚ춠", a_)))
						{
							num = 7;
							continue;
						}
						return spr_u24D;
					}
					case 16:
						goto IL_3B2;
					case 17:
					{
						XmlReader xmlReader;
						MemoryStream memoryStream = this.ᜢ(xmlReader);
						Spire.Doc.Documents.Converters.ShapeType shapeType = this.ᜀ(memoryStream);
						num = 8;
						continue;
					}
					case 18:
						if (this.ᜋ != string.Empty)
						{
							num = 16;
							continue;
						}
						goto IL_3E4;
					case 19:
					{
						XmlReader xmlReader = spr\u23D7.ᜀ(A_0);
						num = 9;
						continue;
					}
					case 20:
					{
						DictionaryEntry value = default(DictionaryEntry);
						num = 27;
						continue;
					}
					case 21:
						num = 15;
						continue;
					case 22:
						goto IL_111;
					case 23:
					{
						Dictionary<string, DictionaryEntry> dictionary;
						if (dictionary != null)
						{
							num = 5;
							continue;
						}
						goto IL_111;
					}
					case 24:
						goto IL_111;
					case 25:
					{
						bool flag;
						if (!flag)
						{
							num = 20;
							continue;
						}
						goto IL_1E7;
					}
					case 26:
						num = 29;
						continue;
					case 27:
						if (this.ᜋ != null)
						{
							num = 14;
							continue;
						}
						goto IL_3E4;
					case 28:
						return spr_u24D;
					case 29:
						if (this.ᜎ != null)
						{
							num = 12;
							continue;
						}
						return spr_u24D;
					case 30:
					{
						int count;
						if (num2 >= count)
						{
							num = 19;
							continue;
						}
						string text = list[num2];
						bool flag = this.ᜀ(spr_u24D, text);
						num = 25;
						continue;
					}
					case 31:
						goto IL_ED;
					}
					break;
					IL_ED:
					num = 30;
					continue;
					IL_111:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_3B2:
						Dictionary<string, DictionaryEntry> dictionary = this.ᜅ(this.ᜋ);
						num = 23;
						continue;
					}
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					IL_1E7:
					num2++;
					num = 31;
					continue;
					IL_3E4:
					num = 10;
				}
			}
			return spr_u24D;
		}
		}
	}

	// Token: 0x06001F19 RID: 7961 RVA: 0x001FFF2C File Offset: 0x001FEF2C
	private spr\u24D5 ᜀ(XmlReader A_0, Stream A_1)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			spr\u24D5 spr_u24D;
			for (;;)
			{
				IL_A3:
				spr_u24D = new spr\u24D5(A_1, this.ᜄ);
				spr_u24D.ᜀ(this.\u1716);
				List<string> list = this.ᜂ(A_1);
				for (;;)
				{
					IL_C4:
					int num = 5;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
						{
							int count;
							if (num2 >= count)
							{
								num = 21;
								continue;
							}
							if (true)
							{
							}
							string text = list[num2];
							bool flag = this.ᜀ(spr_u24D, text);
							num = 16;
							continue;
						}
						case 1:
						{
							MemoryStream memoryStream = this.ᜢ(A_0);
							Spire.Doc.Documents.Converters.ShapeType shapeType = this.ᜀ(memoryStream);
							num = 4;
							continue;
						}
						case 2:
						{
							this.ᜀ(spr_u24D, list);
							num2 = 0;
							int count = list.Count;
							num = 28;
							continue;
						}
						case 3:
							if (A_0.ReadToFollowing(ClipboardData.b("ᥩѫ཭o᝱", a_), ClipboardData.b("Ὡṫm䩯űᝳṵᵷ᝹ᵻൽ굿慎ﾋ릓ꚛ춟캡", a_)))
							{
								num = 22;
								continue;
							}
							return spr_u24D;
						case 4:
						{
							Spire.Doc.Documents.Converters.ShapeType shapeType;
							if (shapeType == Spire.Doc.Documents.Converters.ShapeType.PictureShape)
							{
								num = 8;
								continue;
							}
							return spr_u24D;
						}
						case 5:
							if (list.Count > 0)
							{
								num = 23;
								continue;
							}
							goto IL_410;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C4;
							default:
							{
								if (false)
								{
								}
								string text;
								if (this.ᜎ.ContainsKey(text))
								{
									num = 12;
									continue;
								}
								goto IL_111;
							}
							}
							break;
						case 7:
						{
							string text;
							Dictionary<string, DictionaryEntry> dictionary;
							DictionaryEntry value = dictionary[text];
							num = 9;
							continue;
						}
						case 8:
						{
							DocPicture docPicture = new DocPicture(this.ᜄ);
							MemoryStream memoryStream;
							memoryStream.Position = 0L;
							XmlReader a_2 = spr\u23D7.ᜀ(memoryStream);
							this.ᜉ(a_2, docPicture);
							DocPicture docPicture2 = docPicture;
							docPicture2.IsShape = true;
							this.ᜊ(a_2, docPicture);
							spr_u24D.ᜀ(docPicture2);
							num = 24;
							continue;
						}
						case 9:
							goto IL_111;
						case 10:
							num = 17;
							continue;
						case 11:
							if (this.ᜋ != null)
							{
								num = 10;
								continue;
							}
							goto IL_3C5;
						case 12:
						{
							string text;
							DictionaryEntry value = this.ᜎ[text];
							num = 26;
							continue;
						}
						case 13:
							if (this.ᜎ != null)
							{
								num = 2;
								continue;
							}
							goto IL_410;
						case 14:
							if (A_0.LocalName == ClipboardData.b("ᥩѫ཭o᝱", a_))
							{
								num = 1;
								continue;
							}
							return spr_u24D;
						case 15:
							if (spr_u24D.ᜇ().Count > 0)
							{
								num = 25;
								continue;
							}
							return spr_u24D;
						case 16:
						{
							bool flag;
							if (!flag)
							{
								num = 30;
								continue;
							}
							goto IL_1CA;
						}
						case 17:
							if (this.ᜋ != string.Empty)
							{
								num = 27;
								continue;
							}
							goto IL_3C5;
						case 18:
						{
							string text;
							if (!spr_u24D.ᜁ().ContainsKey(text))
							{
								num = 19;
								continue;
							}
							goto IL_1CA;
						}
						case 19:
						{
							string text;
							DictionaryEntry value;
							spr_u24D.ᜁ().Add(text, value);
							num = 20;
							continue;
						}
						case 20:
							goto IL_1CA;
						case 21:
							goto IL_410;
						case 22:
							num = 14;
							continue;
						case 23:
							num = 13;
							continue;
						case 24:
							return spr_u24D;
						case 25:
							num = 3;
							continue;
						case 26:
							goto IL_111;
						case 27:
						{
							Dictionary<string, DictionaryEntry> dictionary = this.ᜅ(this.ᜋ);
							num = 29;
							continue;
						}
						case 28:
							goto IL_ED;
						case 29:
						{
							Dictionary<string, DictionaryEntry> dictionary;
							if (dictionary != null)
							{
								num = 7;
								continue;
							}
							goto IL_111;
						}
						case 30:
						{
							DictionaryEntry value = default(DictionaryEntry);
							num = 11;
							continue;
						}
						case 31:
							goto IL_ED;
						}
						goto IL_A3;
						IL_ED:
						num = 0;
						continue;
						IL_111:
						num = 18;
						continue;
						IL_1CA:
						num2++;
						num = 31;
						continue;
						IL_3C5:
						num = 6;
						continue;
						IL_410:
						num = 15;
					}
				}
			}
			return spr_u24D;
		}
		}
	}

	// Token: 0x06001F1A RID: 7962 RVA: 0x00200398 File Offset: 0x001FF398
	private bool ᜀ(spr\u24D5 A_0, string A_1)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			sprᠾ sprᠾ;
			for (;;)
			{
				sprᠾ = null;
				int num = 5;
				for (;;)
				{
					bool flag;
					string text;
					switch (num)
					{
					case 0:
						flag = true;
						goto IL_1F1;
					case 1:
						goto IL_194;
					case 2:
					{
						if (this.\u1717().ContainsKey(text))
						{
							num = 12;
							continue;
						}
						byte[] array = this.ᜮ(text);
						num = 8;
						continue;
					}
					case 3:
					{
						byte[] array;
						if (array.Length > 0)
						{
							num = 16;
							continue;
						}
						goto IL_95;
					}
					case 4:
						if (!A_0.ᜇ().ContainsKey(A_1))
						{
							num = 15;
							continue;
						}
						goto IL_25A;
					case 5:
						if (!this.ᜋ.StartsWith(ClipboardData.b("٭ᕯ፱ၳ፵੷", a_)))
						{
							num = 6;
							continue;
						}
						num = 0;
						continue;
					case 6:
						num = 11;
						continue;
					case 7:
						goto IL_A1;
					case 8:
					{
						byte[] array;
						if (array != null)
						{
							num = 9;
							continue;
						}
						goto IL_95;
					}
					case 9:
						num = 3;
						continue;
					case 10:
						num = 4;
						continue;
					case 11:
						flag = this.ᜋ.StartsWith(ClipboardData.b("࡭Ὧᵱs፵੷", a_));
						goto IL_1F1;
					case 12:
					{
						sprᠾ = this.ᜄ.Images.ᜀ(this.\u1717()[text]);
						sprᠾ sprᠾ2 = sprᠾ;
						sprᠾ2.ᜂ(sprᠾ2.ᜅ() + 1);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A1;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					}
					case 13:
						goto IL_95;
					case 14:
						goto IL_95;
					case 15:
						A_0.ᜇ().Add(A_1, sprᠾ);
						num = 1;
						continue;
					case 16:
					{
						byte[] array;
						sprᠾ = this.ᜄ.Images.ᜃ(array);
						this.\u1717().Add(text, sprᠾ.ᜀ());
						num = 13;
						continue;
					}
					}
					break;
					IL_95:
					num = 7;
					continue;
					IL_A1:
					if (sprᠾ != null)
					{
						num = 10;
						continue;
					}
					goto IL_25A;
					IL_1F1:
					bool a_2 = flag;
					text = this.ᜁ(A_1, a_2, false);
					if (true)
					{
					}
					num = 2;
				}
			}
			IL_194:
			IL_25A:
			return sprᠾ != null;
		}
		}
	}

	// Token: 0x06001F1B RID: 7963 RVA: 0x00200608 File Offset: 0x001FF608
	private List<string> ᜂ(Stream A_0)
	{
		int a_ = 4;
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			List<string> list;
			for (;;)
			{
				A_0.Position = 0L;
				XmlReader xmlReader = spr\u23D7.ᜀ(A_0);
				list = new List<string>();
				bool flag = false;
				int num = 30;
				for (;;)
				{
					string text;
					string text2;
					switch (num)
					{
					case 0:
						list.Add(text);
						num = 23;
						continue;
					case 1:
						list.Add(text2);
						num = 12;
						continue;
					case 2:
						return list;
					case 3:
						if (text2 != null)
						{
							num = 35;
							continue;
						}
						goto IL_364;
					case 4:
						goto IL_F3;
					case 5:
						goto IL_F3;
					case 6:
						num = 21;
						continue;
					case 7:
						goto IL_F3;
					case 8:
						goto IL_5E2;
					case 9:
						goto IL_628;
					case 10:
						if (!string.IsNullOrEmpty(text))
						{
							num = 37;
							continue;
						}
						goto IL_4EA;
					case 11:
					{
						string localName;
						if ((localName = xmlReader.LocalName) != null)
						{
							num = 29;
							continue;
						}
						goto IL_F3;
					}
					case 12:
						goto IL_364;
					case 13:
						if (!string.IsNullOrEmpty(text))
						{
							num = 9;
							continue;
						}
						goto IL_26A;
					case 14:
						num = 33;
						continue;
					case 15:
						if (!flag)
						{
							num = 27;
							continue;
						}
						goto IL_343;
					case 16:
						if (!string.IsNullOrEmpty(text))
						{
							num = 0;
							continue;
						}
						goto IL_2A0;
					case 17:
						goto IL_343;
					case 18:
						num = 15;
						continue;
					case 19:
						goto IL_3F6;
					case 20:
						num = 5;
						continue;
					case 21:
					{
						int num2;
						switch (num2)
						{
						case 0:
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
						case 6:
							text = xmlReader.GetAttribute(ClipboardData.b("ͩ࡫", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫솭횯풱\uddb3햵\uddb7ﺹ펻\uddbd떿꿁ꇃꣅ볇ﻋﻍﯓꓕ뷗뛙뷛ꫝ觟跡諣闥胧菩鳫鷭", a_));
							text2 = xmlReader.GetAttribute(ClipboardData.b("ɩṫ୭ᙯ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫솭횯풱\uddb3햵\uddb7ﺹ펻\uddbd떿꿁ꇃꣅ볇ﻋﻍﯓꓕ뷗뛙뷛ꫝ觟跡諣闥胧菩鳫鷭", a_));
							num = 7;
							continue;
						case 7:
							text = xmlReader.GetAttribute(ClipboardData.b("ཀྵū౭ᕯᙱ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫솭횯풱\uddb3햵\uddb7ﺹ펻\uddbd떿꿁ꇃꣅ볇ﻋﻍﯓꓕ뷗뛙뷛ꫝ觟跡諣闥胧菩鳫鷭", a_));
							num = 4;
							continue;
						case 8:
							text = xmlReader.GetAttribute(ClipboardData.b("๩ū", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫솭횯풱\uddb3햵\uddb7ﺹ펻\uddbd떿꿁ꇃꣅ볇ﻋﻍﯓꓕ뷗뛙뷛ꫝ觟跡諣闥胧菩鳫鷭", a_));
							num = 10;
							continue;
						default:
							num = 20;
							continue;
						}
						break;
					}
					case 22:
						list.Add(text);
						num = 8;
						continue;
					case 23:
						goto IL_2A0;
					case 24:
						if (!string.IsNullOrEmpty(text))
						{
							num = 22;
							continue;
						}
						goto IL_5E2;
					case 25:
						if (xmlReader.EOF)
						{
							num = 2;
							continue;
						}
						goto IL_3A4;
					case 26:
						spr᧓.\u175B = new Dictionary<string, int>(9)
						{
							{
								ClipboardData.b("౩իɭᱯ", a_),
								0
							},
							{
								ClipboardData.b("३ѫ཭ɯٱ", a_),
								1
							},
							{
								ClipboardData.b("ͩū཭ᝯ᝱ၳ᝵౷᭹", a_),
								2
							},
							{
								ClipboardData.b("ᥩᡫᱭὯᥱᅳ", a_),
								3
							},
							{
								ClipboardData.b("३ͫmѯq᭳᩵", a_),
								4
							},
							{
								ClipboardData.b("╩⁫⭭㽯ၱṳ፵᭷๹", a_),
								5
							},
							{
								ClipboardData.b("ɩᕫṭᕯqᡳήᙷᅹ", a_),
								6
							},
							{
								ClipboardData.b("ࡩkݭo", a_),
								7
							},
							{
								ClipboardData.b("ᡩ५ɭ㥯ᙱݳ", a_),
								8
							}
						};
						num = 19;
						continue;
					case 27:
						list.Add(text);
						num = 17;
						continue;
					case 28:
						goto IL_4EA;
					case 29:
						num = 36;
						continue;
					case 30:
						goto IL_3A4;
					case 31:
						if (text != null)
						{
							num = 14;
							continue;
						}
						goto IL_343;
					case 32:
						goto IL_26A;
					case 33:
						if (text != string.Empty)
						{
							num = 18;
							continue;
						}
						goto IL_343;
					case 34:
					{
						string localName;
						int num2;
						if (spr᧓.\u175B.TryGetValue(localName, out num2))
						{
							num = 6;
							continue;
						}
						goto IL_F3;
					}
					case 35:
						num = 38;
						continue;
					case 36:
						if (spr᧓.\u175B == null)
						{
							num = 26;
							continue;
						}
						goto IL_3F6;
					case 37:
						list.Add(text);
						num = 28;
						continue;
					case 38:
						if (!(text2 != string.Empty))
						{
							goto IL_364;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_628;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 39:
						goto IL_F3;
					}
					break;
					IL_F3:
					num = 31;
					continue;
					IL_26A:
					flag = true;
					num = 39;
					continue;
					IL_2A0:
					text = xmlReader.GetAttribute(ClipboardData.b("᭩Ὣ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫솭횯풱\uddb3햵\uddb7ﺹ펻\uddbd떿꿁ꇃꣅ볇ﻋﻍﯓꓕ뷗뛙뷛ꫝ觟跡諣闥胧菩鳫鷭", a_));
					num = 24;
					continue;
					IL_343:
					num = 3;
					continue;
					IL_364:
					num = 25;
					continue;
					IL_3A4:
					flag = false;
					xmlReader.Read();
					text = string.Empty;
					text2 = string.Empty;
					num = 11;
					continue;
					IL_3F6:
					num = 34;
					continue;
					IL_4EA:
					text = xmlReader.GetAttribute(ClipboardData.b("٩ͫ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫솭횯풱\uddb3햵\uddb7ﺹ펻\uddbd떿꿁ꇃꣅ볇ﻋﻍﯓꓕ뷗뛙뷛ꫝ觟跡諣闥胧菩鳫鷭", a_));
					num = 16;
					continue;
					IL_5E2:
					text = xmlReader.GetAttribute(ClipboardData.b("३Ὣ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫솭횯풱\uddb3햵\uddb7ﺹ펻\uddbd떿꿁ꇃꣅ볇ﻋﻍﯓꓕ뷗뛙뷛ꫝ觟跡諣闥胧菩鳫鷭", a_));
					num = 13;
					continue;
					IL_628:
					list.Add(text);
					num = 32;
				}
			}
			return list;
		}
		}
	}

	// Token: 0x06001F1C RID: 7964 RVA: 0x00200C44 File Offset: 0x001FFC44
	private void ᜀ(spr\u24D5 A_0, List<string> A_1)
	{
		for (;;)
		{
			int num = 0;
			int count = A_1.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_93;
				case 1:
					return;
				case 2:
					goto IL_93;
				case 3:
				{
					string key;
					A_0.ᜄ = key;
					num2 = 6;
					continue;
				}
				case 4:
					if (num < count)
					{
						if (true)
						{
						}
						string key = A_1[num];
						num2 = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_86;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				case 5:
				{
					string key;
					if (this.ᜌ().ContainsKey(key))
					{
						goto IL_86;
					}
					goto IL_41;
				}
				case 6:
					goto IL_41;
				}
				break;
				IL_41:
				num++;
				num2 = 0;
				continue;
				IL_86:
				num2 = 3;
				continue;
				IL_93:
				num2 = 4;
			}
		}
	}

	// Token: 0x06001F1D RID: 7965 RVA: 0x00200D1C File Offset: 0x001FFD1C
	private ParagraphBase ᜄ(XmlReader A_0, DocPicture A_1)
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
				goto IL_5B;
			case 1:
				A_1.PictureCharacterFormat.ImportContainer(this.\u1716);
				num = 0;
				continue;
			}
			IL_26:
			if (this.\u1716 != null)
			{
				num = 1;
				continue;
			}
			IL_5B:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_26;
			default:
				goto IL_71;
			}
		}
		IL_71:
		if (false)
		{
		}
		this.\u1716 = null;
		this.ᜃ(A_0, A_1);
		return A_1;
	}

	// Token: 0x06001F1E RID: 7966 RVA: 0x00200DB0 File Offset: 0x001FFDB0
	private void ᜃ(XmlReader A_0, DocPicture A_1)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 44;
			for (;;)
			{
				string attribute;
				int num2;
				int num4;
				switch (num)
				{
				case 0:
					spr᧓.\u175C = new Dictionary<string, int>(11)
					{
						{
							ClipboardData.b("୭ᙯᑱᅳᕵ౷㽹ѻ੽", a_),
							0
						},
						{
							ClipboardData.b("୭࡯ٱᅳᡵ౷", a_),
							1
						},
						{
							ClipboardData.b("ṭὯűᵳɵᅷᕹቻ㙽", a_),
							2
						},
						{
							ClipboardData.b("ṭὯűᵳɵᅷᕹቻ⡽", a_),
							3
						},
						{
							ClipboardData.b("੭Ὧᅱ⑳ѵ", a_),
							4
						},
						{
							ClipboardData.b("ᥭɯ፱ѳ╵ॷཹᵻ౽", a_),
							5
						},
						{
							ClipboardData.b("ᥭɯ፱ѳ≵ᅷᵹᑻ੽", a_),
							6
						},
						{
							ClipboardData.b("ᥭɯ፱ѳ≵ၷࡹ፻୽", a_),
							7
						},
						{
							ClipboardData.b("ᥭɯ፱ѳ≵᝷੹㵻ၽ삁ﲇ", a_),
							8
						},
						{
							ClipboardData.b("ᥭɯ፱ѳ㡵᝷ᑹ᥻", a_),
							9
						},
						{
							ClipboardData.b("཭ṯᅱᱳ᥵੷", a_),
							10
						}
					};
					num = 13;
					continue;
				case 1:
					if (attribute != null)
					{
						num = 26;
						continue;
					}
					goto IL_53B;
				case 2:
					A_1.IsUnderText = (attribute == ClipboardData.b("彭", a_) || attribute == ClipboardData.b("ᩭɯݱᅳ", a_));
					attribute = A_0.GetAttribute(ClipboardData.b("ɭᅯୱ᭳͵౷㍹ቻ㵽", a_));
					num = 40;
					continue;
				case 3:
					goto IL_84E;
				case 4:
					return;
				case 5:
					goto IL_84E;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_89C;
					default:
						if (false)
						{
						}
						num = 33;
						continue;
					}
					break;
				case 7:
					num = 45;
					continue;
				case 8:
					goto IL_84E;
				case 9:
					goto IL_84E;
				case 10:
					num = 51;
					continue;
				case 11:
					goto IL_895;
				case 12:
					num = 56;
					continue;
				case 13:
					goto IL_3EA;
				case 14:
					goto IL_84E;
				case 15:
					attribute = A_0.GetAttribute(ClipboardData.b("౭ᕯᩱᵳᡵᱷ㹹፻ᵽ", a_));
					num = 32;
					continue;
				case 16:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 10;
						continue;
					}
					A_0.Read();
					num = 60;
					continue;
				case 17:
					num2 = 16910;
					goto IL_250;
				case 18:
					goto IL_1F6;
				case 19:
					num = 62;
					continue;
				case 20:
					if (!(A_1.Image is Metafile))
					{
						num = 57;
						continue;
					}
					num = 17;
					continue;
				case 21:
					num = 42;
					continue;
				case 22:
					if (A_0.AttributeCount != 0)
					{
						num = 15;
						continue;
					}
					goto IL_84E;
				case 24:
					num2 = 12689;
					goto IL_250;
				case 25:
					goto IL_84E;
				case 26:
					num = 43;
					continue;
				case 27:
				{
					string localName;
					int num3;
					if (spr᧓.\u175C.TryGetValue(localName, out num3))
					{
						num = 59;
						continue;
					}
					goto IL_84E;
				}
				case 28:
					goto IL_84E;
				case 29:
					goto IL_53B;
				case 30:
				{
					bool flag;
					if (!flag)
					{
						num = 54;
						continue;
					}
					goto IL_895;
				}
				case 31:
				{
					int num3;
					switch (num3)
					{
					case 0:
					{
						A_1.DocxProps.Add(this.ᜢ(A_0));
						bool flag = true;
						num = 8;
						continue;
					}
					case 1:
						attribute = A_0.GetAttribute(ClipboardData.b("൭࡯", a_));
						num = 1;
						continue;
					case 2:
						this.ᜁ(A_0, A_1);
						num = 5;
						continue;
					case 3:
						this.ᜀ(A_0, A_1);
						num = 9;
						continue;
					case 4:
						A_1.Title = A_0.GetAttribute(ClipboardData.b("ᩭ᥯ٱᡳ፵", a_));
						A_1.AlternativeText = A_0.GetAttribute(ClipboardData.b("੭ᕯűᝳѵ", a_));
						num = 28;
						continue;
					case 5:
						A_1.TextWrappingStyle = TextWrappingStyle.Square;
						this.ᜂ(A_0, A_1);
						num = 52;
						continue;
					case 6:
						A_1.TextWrappingStyle = TextWrappingStyle.Tight;
						this.ᜂ(A_0, A_1);
						num = 14;
						continue;
					case 7:
						A_1.TextWrappingStyle = TextWrappingStyle.Through;
						this.ᜂ(A_0, A_1);
						num = 35;
						continue;
					case 8:
						A_1.TextWrappingStyle = TextWrappingStyle.TopAndBottom;
						num = 58;
						continue;
					case 9:
						num = 36;
						continue;
					case 10:
						num = 22;
						continue;
					default:
						num = 19;
						continue;
					}
					break;
				}
				case 32:
					try
					{
						A_1.OrderIndex = int.Parse(A_0.GetAttribute(ClipboardData.b("ᱭᕯṱᕳɵᅷ౹᥻㙽ﲇ", a_)));
					}
					catch (Exception)
					{
					}
					num = 2;
					continue;
				case 33:
					if (A_0.LocalName != ClipboardData.b("੭ɯ፱ͳήᙷᵹ", a_))
					{
						num = 63;
						continue;
					}
					num = 53;
					continue;
				case 34:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 21;
						continue;
					}
					return;
				case 35:
					goto IL_84E;
				case 36:
					if (A_1.IsUnderText)
					{
						num = 61;
						continue;
					}
					A_1.TextWrappingStyle = TextWrappingStyle.InFrontOfText;
					num = 55;
					continue;
				case 37:
					A_1.Width = float.Parse(attribute) / (float)num4;
					num = 29;
					continue;
				case 38:
					goto IL_84E;
				case 39:
					A_1.Height = float.Parse(attribute) / (float)num4;
					num = 38;
					continue;
				case 40:
					A_1.LayoutInCell = (attribute == ClipboardData.b("彭", a_) || attribute == ClipboardData.b("ᩭɯݱᅳ", a_));
					num = 25;
					continue;
				case 41:
					goto IL_84E;
				case 42:
				{
					if (!(A_0.LocalName != ClipboardData.b("੭ɯ፱ͳήᙷᵹ", a_)))
					{
						num = 4;
						continue;
					}
					bool flag = false;
					num = 16;
					continue;
				}
				case 43:
					if (A_1.Width == -3.4028235E+38f)
					{
						num = 37;
						continue;
					}
					A_1.WidthScale = float.Parse(attribute) / (float)num4 * 100f / A_1.Width;
					num = 47;
					continue;
				case 45:
					if (spr᧓.\u175C == null)
					{
						num = 0;
						continue;
					}
					goto IL_3EA;
				case 46:
					goto IL_8AA;
				case 47:
					goto IL_53B;
				case 48:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 6;
						continue;
					}
					A_0.Read();
					num = 23;
					continue;
				case 49:
					goto IL_8AA;
				case 50:
					if (attribute != null)
					{
						num = 12;
						continue;
					}
					goto IL_84E;
				case 51:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 7;
						continue;
					}
					goto IL_84E;
				}
				case 52:
					goto IL_84E;
				case 53:
				{
					if (A_0.IsEmptyElement)
					{
						num = 18;
						continue;
					}
					bool flag = false;
					num = 20;
					continue;
				}
				case 54:
					A_0.Read();
					num = 11;
					continue;
				case 55:
					goto IL_84E;
				case 56:
					if (A_1.Height == -3.4028235E+38f)
					{
						num = 39;
						continue;
					}
					A_1.HeightScale = float.Parse(attribute) / (float)num4 * 100f / A_1.Height;
					num = 41;
					continue;
				case 57:
					num = 24;
					continue;
				case 58:
					goto IL_84E;
				case 59:
					num = 31;
					continue;
				case 60:
					goto IL_895;
				case 61:
					A_1.TextWrappingStyle = TextWrappingStyle.Behind;
					num = 3;
					continue;
				case 62:
					goto IL_84E;
				case 63:
					goto IL_1B5;
				}
				IL_143:
				num = 48;
				continue;
				goto IL_143;
				IL_250:
				num4 = num2;
				A_0.Read();
				this.ᜀ(A_0);
				num = 49;
				continue;
				IL_3EA:
				num = 27;
				continue;
				IL_53B:
				attribute = A_0.GetAttribute(ClipboardData.b("൭९", a_));
				num = 50;
				continue;
				IL_84E:
				num = 30;
				continue;
				IL_89C:
				num = 46;
				continue;
				IL_895:
				this.ᜀ(A_0);
				goto IL_89C;
				IL_8AA:
				num = 34;
			}
			IL_1B5:
			throw new XmlException(ClipboardData.b("㭭ṯ᝱౳ٵᵷ᥹ࡻ᭽ꊁﲃꪉ늑", a_) + A_0.LocalName);
			IL_1F6:
			if (true)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06001F1F RID: 7967 RVA: 0x002016D4 File Offset: 0x002006D4
	private void ᜂ(XmlReader A_0, DocPicture A_1)
	{
		int a_ = 15;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ɴնᡸ୺⥼᩾呂", a_));
			int num = 11;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					string a;
					if (!(a == ClipboardData.b("ݴṶṸ፺ॼ", a_)))
					{
						goto IL_138;
					}
					goto IL_EF;
				}
				case 2:
				{
					string a;
					if (!(a == ClipboardData.b("ᥴᙶ୸ᱺ᡼౾", a_)))
					{
						num = 0;
						continue;
					}
					A_1.TextWrappingType = TextWrappingType.Largest;
					num = 5;
					continue;
				}
				case 3:
				{
					string a;
					if (!(a == ClipboardData.b("ᥴቶὸེ", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_F8;
				}
				case 4:
					num = 7;
					continue;
				case 5:
					return;
				case 6:
					return;
				case 7:
				{
					string a;
					if (!(a == ClipboardData.b("᝴ᡶ൸፺⹼ᙾ", a_)))
					{
						num = 9;
						continue;
					}
					goto IL_72;
				}
				case 8:
					num = 1;
					continue;
				case 9:
					num = 3;
					continue;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_138;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 11:
					if (attribute == null)
					{
						num = 6;
						continue;
					}
					if (true)
					{
					}
					num = 12;
					continue;
				case 12:
				{
					string a;
					if ((a = attribute) != null)
					{
						num = 4;
						continue;
					}
					return;
				}
				}
				break;
				IL_138:
				num = 10;
			}
		}
		return;
		IL_72:
		A_1.TextWrappingType = TextWrappingType.Both;
		return;
		IL_EF:
		A_1.TextWrappingType = TextWrappingType.Right;
		return;
		IL_F8:
		A_1.TextWrappingType = TextWrappingType.Left;
	}

	// Token: 0x06001F20 RID: 7968 RVA: 0x0020189C File Offset: 0x0020089C
	private void ᜁ(XmlReader A_0, DocPicture A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_17:
				int num = 30;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						string localName;
						if (!(localName == ClipboardData.b("մᡶ੸㑺᭼᥾", a_)))
						{
							num = 22;
							continue;
						}
						float num2 = float.MaxValue;
						num2 = float.Parse(A_0.ReadString());
						num = 25;
						continue;
					}
					case 1:
					{
						string localName;
						if (!(localName == ClipboardData.b("ᑴ᭶ၸᱺ፼", a_)))
						{
							num = 14;
							continue;
						}
						string text = A_0.ReadString();
						num = 17;
						continue;
					}
					case 2:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 20;
							continue;
						}
						A_0.Read();
						num = 4;
						continue;
					case 3:
						goto IL_176;
					case 4:
						goto IL_1CA;
					case 5:
					{
						if (!(A_0.LocalName != ClipboardData.b("մᡶ੸ቺॼᙾ춄", a_)))
						{
							num = 23;
							continue;
						}
						bool flag = false;
						num = 2;
						continue;
					}
					case 6:
					{
						bool flag;
						if (!flag)
						{
							num = 16;
							continue;
						}
						goto IL_1CA;
					}
					case 7:
					{
						string attribute;
						if (attribute != null)
						{
							num = 18;
							continue;
						}
						goto IL_1AB;
					}
					case 8:
						goto IL_156;
					case 9:
					{
						string text;
						A_1.HorizontalAlignment = this.\u171B(text);
						num = 15;
						continue;
					}
					case 10:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 31;
							continue;
						}
						goto IL_156;
					}
					case 11:
						goto IL_1AB;
					case 12:
						if (A_0.NodeType != XmlNodeType.EndElement)
						{
							num = 29;
							continue;
						}
						return;
					case 13:
						return;
					case 14:
						num = 0;
						continue;
					case 15:
						if (true)
						{
						}
						goto IL_156;
					case 16:
						A_0.Read();
						num = 26;
						continue;
					case 17:
					{
						string text;
						if (text != null)
						{
							num = 9;
							continue;
						}
						goto IL_156;
					}
					case 18:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17;
						default:
						{
							if (false)
							{
							}
							string attribute;
							A_1.HorizontalOrigin = this.\u171A(attribute);
							num = 11;
							continue;
						}
						}
						break;
					case 19:
						goto IL_D6;
					case 20:
						num = 10;
						continue;
					case 21:
					{
						if (A_0.IsEmptyElement)
						{
							num = 13;
							continue;
						}
						bool flag = false;
						string attribute = A_0.GetAttribute(ClipboardData.b("ݴቶᕸ᩺ॼᙾ쎄", a_));
						num = 7;
						continue;
					}
					case 22:
						num = 24;
						continue;
					case 23:
						goto IL_215;
					case 24:
						goto IL_156;
					case 25:
					{
						float num2;
						if (num2 != 3.4028235E+38f)
						{
							num = 27;
							continue;
						}
						goto IL_156;
					}
					case 26:
						goto IL_1CA;
					case 27:
					{
						float num2;
						A_1.HorizontalPosition = (float)((int)Math.Round((double)(num2 / 12700f)));
						num = 8;
						continue;
					}
					case 28:
						goto IL_176;
					case 29:
						num = 5;
						continue;
					case 31:
						num = 1;
						continue;
					}
					if (A_0.LocalName != ClipboardData.b("մᡶ੸ቺॼᙾ춄", a_))
					{
						num = 19;
						continue;
					}
					num = 21;
					continue;
					IL_156:
					num = 6;
					continue;
					IL_176:
					num = 12;
					continue;
					IL_1AB:
					A_0.Read();
					this.ᜀ(A_0);
					num = 3;
					continue;
					IL_1CA:
					this.ᜀ(A_0);
					num = 28;
				}
			}
			IL_D6:
			throw new XmlException(ClipboardData.b("մᡶ੸ቺॼᙾ춄", a_));
			IL_215:
			return;
		}
	}

	// Token: 0x06001F21 RID: 7969 RVA: 0x00201C98 File Offset: 0x00200C98
	private ShapeHorizontalAlignment \u171B(string A_0)
	{
		int a_ = 7;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 10;
				continue;
			case 1:
				if (!(A_0 == ClipboardData.b("Ὤٮᙰ᭲Ŵ", a_)))
				{
					num = 2;
					continue;
				}
				return ShapeHorizontalAlignment.Right;
			case 2:
				num = 6;
				continue;
			case 4:
				num = 8;
				continue;
			case 5:
				num = 12;
				continue;
			case 6:
				IL_14B:
				if (!(A_0 == ClipboardData.b("ѬŮɰᩲᅴቶ", a_)))
				{
					num = 4;
					continue;
				}
				return ShapeHorizontalAlignment.Inside;
			case 7:
				num = 1;
				continue;
			case 8:
				if (!(A_0 == ClipboardData.b("ɬᩮհrᱴ፶ᱸ", a_)))
				{
					num = 5;
					continue;
				}
				return ShapeHorizontalAlignment.Outside;
			case 9:
				num = 11;
				continue;
			case 10:
				if (!(A_0 == ClipboardData.b("๬੮ὰݲၴն", a_)))
				{
					num = 9;
					continue;
				}
				return ShapeHorizontalAlignment.Center;
			case 11:
				if (!(A_0 == ClipboardData.b("Ŭ੮ᝰݲ", a_)))
				{
					num = 7;
					continue;
				}
				return ShapeHorizontalAlignment.Left;
			case 12:
				goto IL_174;
			}
			if (true)
			{
			}
			if (A_0 != null)
			{
				num = 0;
				continue;
			}
			IL_174:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_14B;
			default:
				goto IL_18A;
			}
		}
		return ShapeHorizontalAlignment.Left;
		IL_18A:
		if (false)
		{
		}
		return ShapeHorizontalAlignment.None;
	}

	// Token: 0x06001F22 RID: 7970 RVA: 0x00201E38 File Offset: 0x00200E38
	private HorizontalOrigin \u171A(string A_0)
	{
		int a_ = 6;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				if (spr᧓.\u175D.TryGetValue(A_0, out num2))
				{
					num = 3;
					continue;
				}
				return HorizontalOrigin.Margin;
			}
			case 1:
				if (spr᧓.\u175D == null)
				{
					num = 7;
					continue;
				}
				goto IL_76;
			case 2:
				goto IL_182;
			case 3:
				num = 8;
				continue;
			case 4:
				goto IL_76;
			case 5:
				if (true)
				{
				}
				num = 1;
				continue;
			case 7:
				goto IL_1AE;
			case 8:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return HorizontalOrigin.Page;
				case 1:
					return HorizontalOrigin.Column;
				case 2:
				case 3:
					return HorizontalOrigin.Character;
				case 4:
					return HorizontalOrigin.LeftMarginArea;
				case 5:
					return HorizontalOrigin.RightMarginArea;
				case 6:
					return HorizontalOrigin.InnerMarginArea;
				case 7:
					return HorizontalOrigin.OuterMarginArea;
				default:
					num = 9;
					continue;
				}
				break;
			}
			case 9:
				num = 2;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_1AE:
				spr᧓.\u175D = new Dictionary<string, int>(8)
				{
					{
						ClipboardData.b("ᱫ཭ᝯ᝱", a_),
						0
					},
					{
						ClipboardData.b("ཫŭᱯݱᥳᡵ", a_),
						1
					},
					{
						ClipboardData.b("ཫ٭ᅯq", a_),
						2
					},
					{
						ClipboardData.b("ཫ٭ᅯqᕳᕵ౷ό๻", a_),
						3
					},
					{
						ClipboardData.b("k୭ᙯٱ女᭵᥷ࡹ᭻᝽꾁", a_),
						4
					},
					{
						ClipboardData.b("ṫݭᝯᩱs孵ᕷ᭹๻᥽ꦃ慎", a_),
						5
					},
					{
						ClipboardData.b("իmṯ᝱ٳ孵ᕷ᭹๻᥽ꦃ慎", a_),
						6
					},
					{
						ClipboardData.b("ͫ᭭ѯ᝱ٳ孵ᕷ᭹๻᥽ꦃ慎", a_),
						7
					}
				};
				num = 4;
				continue;
			default:
				if (false)
				{
				}
				if (A_0 != null)
				{
					num = 5;
					continue;
				}
				return HorizontalOrigin.Margin;
			}
			IL_76:
			num = 0;
		}
		return HorizontalOrigin.LeftMarginArea;
		IL_182:
		return HorizontalOrigin.Margin;
	}

	// Token: 0x06001F23 RID: 7971 RVA: 0x0020203C File Offset: 0x0020103C
	private void ᜀ(XmlReader A_0, DocPicture A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 22;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					string text;
					if (text != null)
					{
						num = 13;
						continue;
					}
					goto IL_CC;
				}
				case 1:
				{
					bool flag;
					if (!flag)
					{
						num = 19;
						continue;
					}
					goto IL_208;
				}
				case 2:
					if (A_0.LocalName != ClipboardData.b("ٵ᝷ॹᕻ੽킅", a_))
					{
						bool flag = false;
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_13C;
					default:
						if (false)
						{
						}
						num = 23;
						continue;
					}
					break;
				case 3:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 5;
						continue;
					}
					A_0.Read();
					num = 20;
					continue;
				case 4:
					goto IL_18C;
				case 5:
					if (true)
					{
					}
					num = 9;
					continue;
				case 6:
					goto IL_CC;
				case 7:
				{
					string localName;
					if (!(localName == ClipboardData.b("ٵ᝷ॹ㍻᡽", a_)))
					{
						num = 8;
						continue;
					}
					float num2 = float.MaxValue;
					num2 = float.Parse(A_0.ReadString());
					num = 12;
					continue;
				}
				case 8:
					num = 24;
					continue;
				case 9:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 10;
						continue;
					}
					goto IL_CC;
				}
				case 10:
					num = 21;
					continue;
				case 11:
					goto IL_CC;
				case 12:
				{
					float num2;
					if (num2 != 3.4028235E+38f)
					{
						num = 28;
						continue;
					}
					goto IL_CC;
				}
				case 13:
				{
					string text;
					A_1.VerticalAlignment = this.\u1719(text);
					num = 11;
					continue;
				}
				case 14:
					num = 7;
					continue;
				case 15:
					return;
				case 16:
				{
					if (A_0.IsEmptyElement)
					{
						num = 15;
						continue;
					}
					bool flag = false;
					string attribute = A_0.GetAttribute(ClipboardData.b("ѵᵷᙹᵻ੽삅慎", a_));
					A_1.VerticalOrigin = this.\u1718(attribute);
					A_0.Read();
					this.ᜀ(A_0);
					goto IL_13C;
				}
				case 17:
					goto IL_208;
				case 18:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 26;
						continue;
					}
					return;
				case 19:
					A_0.Read();
					num = 17;
					continue;
				case 20:
					goto IL_208;
				case 21:
				{
					string localName;
					if (!(localName == ClipboardData.b("᝵ᑷ፹᭻ၽ", a_)))
					{
						num = 14;
						continue;
					}
					string text = A_0.ReadString();
					num = 0;
					continue;
				}
				case 23:
					goto IL_272;
				case 24:
					goto IL_CC;
				case 25:
					goto IL_18C;
				case 26:
					num = 2;
					continue;
				case 27:
					goto IL_C7;
				case 28:
				{
					float num2;
					A_1.VerticalPosition = (float)((int)Math.Round((double)(num2 / 12700f)));
					num = 6;
					continue;
				}
				}
				if (A_0.LocalName != ClipboardData.b("ٵ᝷ॹᕻ੽킅", a_))
				{
					num = 27;
					continue;
				}
				num = 16;
				continue;
				IL_CC:
				num = 1;
				continue;
				IL_13C:
				num = 25;
				continue;
				IL_18C:
				num = 18;
				continue;
				IL_208:
				this.ᜀ(A_0);
				num = 4;
			}
			IL_C7:
			throw new XmlException(ClipboardData.b("♵᝷ॹᕻ੽킅", a_));
			IL_272:
			return;
		}
		}
	}

	// Token: 0x06001F24 RID: 7972 RVA: 0x00202400 File Offset: 0x00201400
	private ShapeVerticalAlignment \u1719(string A_0)
	{
		int a_ = 14;
		int num = 9;
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
					goto IL_14B;
				case 1:
				{
					int num2;
					if (spr᧓.\u175E.TryGetValue(A_0, out num2))
					{
						num = 7;
						continue;
					}
					return ShapeVerticalAlignment.None;
				}
				case 2:
				{
					int num2;
					switch (num2)
					{
					case 0:
						return ShapeVerticalAlignment.Top;
					case 1:
						return ShapeVerticalAlignment.Bottom;
					case 2:
						return ShapeVerticalAlignment.Center;
					case 3:
						return ShapeVerticalAlignment.Inside;
					case 4:
						return ShapeVerticalAlignment.Inline;
					case 5:
						goto IL_136;
					default:
						num = 6;
						continue;
					}
					break;
				}
				case 3:
					goto IL_76;
				case 4:
					num = 8;
					continue;
				case 5:
					goto IL_14D;
				case 6:
					num = 0;
					continue;
				case 7:
					num = 2;
					continue;
				case 8:
					if (spr᧓.\u175E == null)
					{
						num = 3;
						continue;
					}
					goto IL_14D;
				}
				if (A_0 != null)
				{
					num = 4;
					continue;
				}
				return ShapeVerticalAlignment.None;
				IL_14D:
				num = 1;
				continue;
			}
			IL_76:
			spr᧓.\u175E = new Dictionary<string, int>(6)
			{
				{
					ClipboardData.b("s᥵ࡷ", a_),
					0
				},
				{
					ClipboardData.b("ᙳ᥵౷๹፻፽", a_),
					1
				},
				{
					ClipboardData.b("ᝳ፵ᙷ๹᥻౽", a_),
					2
				},
				{
					ClipboardData.b("ᵳᡵ୷፹᡻᭽", a_),
					3
				},
				{
					ClipboardData.b("ᵳᡵᑷ፹ቻ᭽", a_),
					4
				},
				{
					ClipboardData.b("᭳͵౷ॹᕻ᩽", a_),
					5
				}
			};
			num = 5;
		}
		return ShapeVerticalAlignment.Center;
		IL_136:
		if (true)
		{
		}
		return ShapeVerticalAlignment.Outside;
		IL_14B:
		return ShapeVerticalAlignment.None;
	}

	// Token: 0x06001F25 RID: 7973 RVA: 0x002025C8 File Offset: 0x002015C8
	private VerticalOrigin \u1718(string A_0)
	{
		int a_ = 2;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return VerticalOrigin.Page;
				case 1:
					return VerticalOrigin.Paragraph;
				case 2:
				case 3:
					return VerticalOrigin.Line;
				case 4:
					return VerticalOrigin.TopMarginArea;
				case 5:
					return VerticalOrigin.BottomMarginArea;
				case 6:
					return VerticalOrigin.InnerMarginArea;
				case 7:
					return VerticalOrigin.OuterMarginArea;
				default:
					num = 2;
					continue;
				}
				break;
			}
			case 1:
				num = 0;
				continue;
			case 2:
				num = 5;
				continue;
			case 3:
				if (spr᧓.\u175F == null)
				{
					num = 8;
					continue;
				}
				goto IL_76;
			case 4:
				num = 3;
				continue;
			case 5:
				goto IL_18A;
			case 6:
				goto IL_76;
			case 7:
			{
				if (true)
				{
				}
				int num2;
				if (spr᧓.\u175F.TryGetValue(A_0, out num2))
				{
					num = 1;
					continue;
				}
				return VerticalOrigin.Margin;
			}
			case 8:
				goto IL_1AE;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_1AE:
				spr᧓.\u175F = new Dictionary<string, int>(8)
				{
					{
						ClipboardData.b("ᡧ୩୫୭", a_),
						0
					},
					{
						ClipboardData.b("ᡧ୩ṫ཭ᝯqᕳٵၷ", a_),
						1
					},
					{
						ClipboardData.b("ᱧཀྵᑫᩭ", a_),
						2
					},
					{
						ClipboardData.b("ѧͩɫ୭", a_),
						3
					},
					{
						ClipboardData.b("ᱧթᱫ䍭ᵯ፱ٳᅵᅷᑹ养ώ", a_),
						4
					},
					{
						ClipboardData.b("੧թᡫᩭὯά女᭵᥷ࡹ᭻᝽꾁", a_),
						5
					},
					{
						ClipboardData.b("ŧѩɫ୭ɯ影ᥳ᝵੷ᵹᕻၽ굿", a_),
						6
					},
					{
						ClipboardData.b("ݧὩᡫ୭ɯ影ᥳ᝵੷ᵹᕻၽ굿", a_),
						7
					}
				};
				num = 6;
				continue;
			default:
				if (false)
				{
				}
				if (A_0 != null)
				{
					num = 4;
					continue;
				}
				return VerticalOrigin.Margin;
			}
			IL_76:
			num = 7;
		}
		return VerticalOrigin.TopMarginArea;
		IL_18A:
		return VerticalOrigin.Margin;
	}

	// Token: 0x06001F26 RID: 7974 RVA: 0x002027CC File Offset: 0x002017CC
	private bool \u1718(XmlReader A_0)
	{
		int a_ = 6;
		for (;;)
		{
			for (;;)
			{
				A_0.ReadToFollowing(ClipboardData.b("୫ᱭᅯɱᱳή᭷㹹ᵻ੽", a_), ClipboardData.b("ѫᩭѯɱ乳奵坷ॹύᙽꚇﲋﺏ煉歹ﺗ솟횡힣袥잧\ud8a9쮫膭풯삱햳솵톷풹\udbbb펽겿ﳉꏍ뇏믑뫓", a_));
				int num = 5;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
					{
						string attribute;
						if (attribute == null)
						{
							return false;
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
							num = 2;
							continue;
						}
						break;
					}
					case 1:
						return false;
					case 2:
						num = 4;
						continue;
					case 3:
						return true;
					case 4:
					{
						string attribute;
						if (attribute == ClipboardData.b("ѫᩭѯɱ乳奵坷ॹύᙽꚇﲋﺏ煉歹ﺗ솟횡힣袥잧\ud8a9쮫膭풯삱햳솵톷풹\udbbb펽겿ﳉ뻍맏뇑ꃓꏕ꫗뿙", a_))
						{
							num = 3;
							continue;
						}
						return false;
					}
					case 5:
					{
						if (A_0.LocalName != ClipboardData.b("୫ᱭᅯɱᱳή᭷㹹ᵻ੽", a_))
						{
							num = 1;
							continue;
						}
						string attribute = A_0.GetAttribute(ClipboardData.b("ᥫᱭ᥯", a_));
						num = 0;
						continue;
					}
					}
					break;
				}
			}
		}
		return false;
	}

	// Token: 0x06001F27 RID: 7975 RVA: 0x002028E4 File Offset: 0x002018E4
	private void ᜃ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 10;
		Break @break;
		for (;;)
		{
			if (true)
			{
			}
			@break = null;
			int num = 0;
			for (;;)
			{
				string attribute;
				switch (num)
				{
				case 0:
					goto IL_57;
				case 1:
					if (attribute == ClipboardData.b("፯ᵱᡳ͵ᕷᑹ", a_))
					{
						num = 11;
						continue;
					}
					num = 6;
					continue;
				case 2:
					goto IL_A4;
				case 3:
					if (this.\u1716 != null)
					{
						num = 8;
						continue;
					}
					goto IL_21B;
				case 4:
					goto IL_107;
				case 5:
					goto IL_140;
				case 6:
					if (attribute == ClipboardData.b("o፱፳፵", a_))
					{
						num = 10;
						continue;
					}
					@break = new Break(this.ᜄ, BreakType.LineBreak);
					@break.TextRange.Text = ClipboardData.b("筯", a_);
					num = 3;
					continue;
				case 7:
					num = 12;
					continue;
				case 8:
					@break.TextRange.ApplyCharacterFormat(this.\u1716);
					num = 4;
					continue;
				case 9:
					goto IL_1C2;
				case 10:
					@break = new Break(this.ᜄ, BreakType.PageBreak);
					num = 2;
					continue;
				case 11:
					@break = new Break(this.ᜄ, BreakType.ColumnBreak);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_57;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				case 12:
					if (A_0.LocalName != ClipboardData.b("፯q", a_))
					{
						num = 5;
						continue;
					}
					goto IL_1C4;
				}
				break;
				IL_57:
				if (A_0.LocalName != ClipboardData.b("ቯq", a_))
				{
					num = 7;
					continue;
				}
				IL_1C4:
				attribute = A_0.GetAttribute(ClipboardData.b("ѯୱѳ፵", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱쎳\ud9b5쪷\udeb9첻첽꾿ꇁꇃ뗅믇ꏉꋋ꧍뷏뻑ﯓ跟菡跣裥", a_));
				num = 1;
			}
		}
		IL_A4:
		IL_107:
		goto IL_21B;
		IL_140:
		throw new XmlException(ClipboardData.b("ቯqᅳ᝵፷婹ᕻ੽", a_));
		IL_1C2:
		IL_21B:
		this.ᜁ(@break, A_1);
	}

	// Token: 0x06001F28 RID: 7976 RVA: 0x00202B14 File Offset: 0x00201B14
	private ParagraphBase ᜂ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 3;
			Symbol symbol;
			int num2;
			string attribute2;
			TextRange textRange;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_126;
				case 1:
					if (this.\u1716 != null)
					{
						num = 14;
						continue;
					}
					return symbol;
				case 2:
				{
					string attribute;
					if (!attribute.StartsWith(ClipboardData.b("㹷䩹", a_)))
					{
						goto IL_26D;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_102;
					default:
						if (false)
						{
						}
						num = 15;
						continue;
					}
					break;
				}
				case 4:
					num = 7;
					continue;
				case 5:
					goto IL_16D;
				case 6:
					if (this.\u1716 != null)
					{
						num = 10;
						continue;
					}
					goto IL_2C8;
				case 7:
				{
					string attribute;
					if (attribute == null)
					{
						num = 9;
						continue;
					}
					num2 = int.Parse(attribute, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
					num = 2;
					continue;
				}
				case 8:
					if (attribute2 != null)
					{
						num = 4;
						continue;
					}
					goto IL_2C6;
				case 9:
					goto IL_2B0;
				case 10:
					if (true)
					{
					}
					textRange.CharacterFormat.ImportContainer(this.\u1716);
					num = 5;
					continue;
				case 11:
					goto IL_145;
				case 12:
					goto IL_26D;
				case 13:
					if (num2 <= 255)
					{
						num = 16;
						continue;
					}
					textRange = new TextRange(this.ᜄ);
					num = 6;
					continue;
				case 14:
					symbol.CharacterFormat.ImportContainer(this.\u1716);
					num = 11;
					continue;
				case 15:
					num2 -= 61440;
					num = 12;
					continue;
				case 16:
					symbol = new Symbol(this.ᜄ);
					symbol.CharacterCode = (byte)num2;
					symbol.FontName = attribute2;
					num = 1;
					continue;
				case 17:
				{
					if (A_0.AttributeCount != 2)
					{
						num = 0;
						continue;
					}
					attribute2 = A_0.GetAttribute(ClipboardData.b("ṷᕹቻ੽", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
					string attribute = A_0.GetAttribute(ClipboardData.b("᭷ቹᵻ౽", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
					num = 8;
					continue;
				}
				case 18:
					goto IL_99;
				}
				if (A_0.LocalName != ClipboardData.b("୷͹ᅻ", a_))
				{
					num = 18;
					continue;
				}
				IL_102:
				num = 17;
				continue;
				IL_26D:
				num = 13;
			}
			IL_99:
			throw new XmlException(ClipboardData.b("㵷ɹύ᭽ꪉ\udf8bﶏﮓ歹뢗ﾙﮝ춟잡쪣튥", a_));
			IL_126:
			return null;
			IL_145:
			return symbol;
			IL_16D:
			goto IL_2C8;
			IL_2B0:
			IL_2C6:
			return null;
			IL_2C8:
			textRange.CharacterFormat.FontName = attribute2;
			textRange.Text = Convert.ToString((char)num2);
			return textRange;
		}
		}
	}

	// Token: 0x06001F29 RID: 7977 RVA: 0x00202E08 File Offset: 0x00201E08
	private void ᜀ(TextRange A_0, string A_1, CharacterFormat A_2)
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
		A_0.ApplyCharacterFormat(A_2);
		A_0.Text = A_1;
	}

	// Token: 0x06001F2A RID: 7978 RVA: 0x00202E54 File Offset: 0x00201E54
	private TextRange ᜁ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 5;
		TextRange textRange;
		for (;;)
		{
			textRange = null;
			int num = 6;
			for (;;)
			{
				string text;
				switch (num)
				{
				case 0:
					if (this.\u1716 != null)
					{
						goto IL_353;
					}
					goto IL_207;
				case 1:
					if (this.\u1716 != null)
					{
						num = 18;
						continue;
					}
					goto IL_365;
				case 2:
					num = 24;
					continue;
				case 3:
					text = this.\u1717(text);
					this.\u1716.Bidi = false;
					num = 20;
					continue;
				case 4:
					goto IL_272;
				case 5:
					if (this.\u171A)
					{
						num = 22;
						continue;
					}
					textRange = new TextRange(this.ᜄ);
					num = 0;
					continue;
				case 6:
					if (!(this.\u1712() is MergeField))
					{
						num = 12;
						continue;
					}
					goto IL_2E5;
				case 7:
					goto IL_2E5;
				case 8:
					if (textRange.Text.Length > 0)
					{
						num = 14;
						continue;
					}
					return textRange;
				case 9:
					goto IL_1B3;
				case 10:
					goto IL_E8;
				case 11:
					textRange = (this.\u1712() as TextFormField).TextRange;
					num = 9;
					continue;
				case 12:
					if (true)
					{
					}
					num = 13;
					continue;
				case 13:
					if (this.\u1712() is TextFormField)
					{
						num = 7;
						continue;
					}
					num = 5;
					continue;
				case 14:
					textRange.Text = textRange.Text.Replace('\n', ' ').Replace(ClipboardData.b("晪", a_), "");
					num = 4;
					continue;
				case 15:
				{
					if (textRange.Text == string.Empty)
					{
						num = 26;
						continue;
					}
					TextRange textRange2 = textRange;
					textRange2.Text += text;
					num = 10;
					continue;
				}
				case 16:
					if (textRange.Text != null)
					{
						num = 17;
						continue;
					}
					return textRange;
				case 17:
					num = 8;
					continue;
				case 18:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_353;
					}
					if (false)
					{
					}
					num = 21;
					continue;
				case 19:
					goto IL_1B3;
				case 20:
					goto IL_365;
				case 21:
					if (this.\u1716.Bidi)
					{
						num = 2;
						continue;
					}
					goto IL_365;
				case 22:
					goto IL_15F;
				case 23:
					textRange.ApplyCharacterFormat(this.\u1716);
					num = 28;
					continue;
				case 24:
					if (text != null)
					{
						num = 3;
						continue;
					}
					goto IL_365;
				case 25:
					if (this.\u1712() is TextFormField)
					{
						num = 11;
						continue;
					}
					textRange = this.\u1712();
					num = 19;
					continue;
				case 26:
					textRange.Text = text;
					textRange.ApplyCharacterFormat(this.\u1716);
					num = 27;
					continue;
				case 27:
					goto IL_1AE;
				case 28:
					goto IL_207;
				}
				break;
				IL_1B3:
				text = this.\u1717(A_0);
				num = 1;
				continue;
				IL_207:
				textRange.Text = this.\u1717(A_0);
				num = 16;
				continue;
				IL_2E5:
				num = 25;
				continue;
				IL_353:
				num = 23;
				continue;
				IL_365:
				num = 15;
			}
		}
		IL_E8:
		goto IL_363;
		IL_15F:
		this.ᜇ(A_0, A_1);
		this.\u171A = false;
		return textRange;
		IL_1AE:
		goto IL_363;
		IL_272:
		return textRange;
		IL_363:
		return null;
	}

	// Token: 0x06001F2B RID: 7979 RVA: 0x002031F8 File Offset: 0x002021F8
	private string \u1717(XmlReader A_0)
	{
		int a_ = 11;
		string text;
		for (;;)
		{
			text = string.Empty;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					if (num2 != 0)
					{
						num = 20;
						continue;
					}
					return text;
				}
				case 1:
					if (true)
					{
					}
					num = 0;
					continue;
				case 2:
					if (!A_0.IsEmptyElement)
					{
						num = 8;
						continue;
					}
					goto IL_253;
				case 3:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 10;
						continue;
					}
					goto IL_253;
				case 4:
					goto IL_215;
				case 5:
				{
					if (A_0.IsEmptyElement)
					{
						num = 11;
						continue;
					}
					int num2 = 0;
					A_0.Read();
					this.ᜀ(A_0);
					num = 4;
					continue;
				}
				case 6:
					goto IL_27B;
				case 7:
					goto IL_253;
				case 8:
				{
					int num2;
					num2++;
					string text2 = this.\u1717(A_0);
					text2 = text2.Replace(spr\u20E8.ᜉ, ClipboardData.b("兰", a_));
					text2 = text2.Replace(spr\u20E8.\u171F, ClipboardData.b("兰", a_));
					text2 = text2.Replace(spr\u20E8.ᜏ, ' ');
					text += text2;
					num = 7;
					continue;
				}
				case 9:
					goto IL_253;
				case 10:
					num = 2;
					continue;
				case 11:
					return text;
				case 12:
					goto IL_156;
				case 13:
					return text;
				case 14:
					num = 17;
					continue;
				case 15:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 13;
						continue;
					}
					goto IL_12F;
				case 16:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 1;
						continue;
					}
					goto IL_156;
				case 17:
					if (A_0.NodeType == XmlNodeType.SignificantWhitespace)
					{
						num = 6;
						continue;
					}
					num = 3;
					continue;
				case 18:
					goto IL_215;
				case 19:
				{
					int num2;
					if (num2 > 0)
					{
						goto IL_12F;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_24E;
					default:
						if (false)
						{
						}
						num = 22;
						continue;
					}
					break;
				}
				case 20:
				{
					int num2;
					num2--;
					num = 12;
					continue;
				}
				case 21:
					if (A_0.NodeType != XmlNodeType.Text)
					{
						num = 14;
						continue;
					}
					goto IL_27B;
				case 22:
					goto IL_24E;
				}
				break;
				IL_12F:
				num = 21;
				continue;
				IL_156:
				A_0.Read();
				this.ᜀ(A_0);
				num = 18;
				continue;
				IL_215:
				num = 19;
				continue;
				IL_24E:
				num = 15;
				continue;
				IL_253:
				num = 16;
				continue;
				IL_27B:
				text += A_0.Value;
				num = 9;
			}
		}
		return text;
	}

	// Token: 0x06001F2C RID: 7980 RVA: 0x002034C4 File Offset: 0x002024C4
	private string \u1717(string A_0)
	{
		string text;
		for (;;)
		{
			char[] array = A_0.ToCharArray();
			text = string.Empty;
			int num = array.Length - 1;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return text;
				case 1:
					if (true)
					{
					}
					goto IL_51;
				case 2:
					if (num < 0)
					{
						num2 = 0;
						continue;
					}
					text += array[num].ToString();
					num--;
					num2 = 1;
					continue;
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
						goto IL_51;
					}
					break;
				}
				break;
				IL_51:
				num2 = 2;
			}
		}
		return text;
	}

	// Token: 0x06001F2D RID: 7981 RVA: 0x00203570 File Offset: 0x00202570
	private string \u1716(string A_0)
	{
		int a_ = 18;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0 = A_0.Replace(ClipboardData.b("幷᭹ᅻ๽뭿", a_), ClipboardData.b("幷", a_));
		A_0 = A_0.Replace(ClipboardData.b("幷ᙹࡻ䕽", a_), ClipboardData.b("䑷", a_));
		A_0 = A_0.Replace(ClipboardData.b("幷ᵹࡻ䕽", a_), ClipboardData.b("䙷", a_));
		return A_0;
	}

	// Token: 0x06001F2E RID: 7982 RVA: 0x00203624 File Offset: 0x00202624
	private void \u1716(XmlReader A_0)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				XmlReader xmlReader;
				Color color;
				switch (num)
				{
				case 0:
				{
					string attribute;
					if (!(attribute == ClipboardData.b("ታѵ᥷᝹᥻", a_)))
					{
						num = 9;
						continue;
					}
					goto IL_27C;
				}
				case 1:
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						num = 15;
						continue;
					}
					goto IL_21B;
				case 2:
					if (!xmlReader.Read())
					{
						num = 21;
						continue;
					}
					num = 1;
					continue;
				case 3:
					this.ᜃ(xmlReader, this.ᜄ.Background);
					num = 19;
					continue;
				case 4:
					goto IL_21B;
				case 6:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᙳ᝵᭷ᅹ᭻౽", a_)))
					{
						num = 22;
						continue;
					}
					color = this.ᜃ(xmlReader.GetAttribute(ClipboardData.b("ታήᑷᙹύᅽ", a_)));
					this.ᜄ.Background.Color = color;
					num = 4;
					continue;
				}
				case 7:
				{
					string attribute;
					if (attribute == ClipboardData.b("sήᑷό", a_))
					{
						num = 8;
						continue;
					}
					goto IL_21B;
				}
				case 8:
					goto IL_27C;
				case 9:
					num = 7;
					continue;
				case 10:
					goto IL_B9;
				case 11:
				{
					string attribute;
					if (attribute.StartsWith(ClipboardData.b("፳ѵ᥷ṹᕻ᭽", a_)))
					{
						num = 3;
						continue;
					}
					num = 0;
					continue;
				}
				case 12:
				{
					string localName;
					if (!(localName == ClipboardData.b("ታήᑷᙹ", a_)))
					{
						num = 13;
						continue;
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
						string attribute = xmlReader.GetAttribute(ClipboardData.b("sཱུࡷό", a_));
						break;
					}
					}
					num = 11;
					continue;
				}
				case 13:
					num = 18;
					continue;
				case 14:
					goto IL_21B;
				case 15:
					num = 20;
					continue;
				case 16:
					num = 6;
					continue;
				case 17:
					if (true)
					{
					}
					goto IL_21B;
				case 18:
					goto IL_21B;
				case 19:
					goto IL_21B;
				case 20:
				{
					string localName;
					if ((localName = xmlReader.LocalName) != null)
					{
						num = 16;
						continue;
					}
					goto IL_21B;
				}
				case 21:
					return;
				case 22:
					num = 12;
					continue;
				}
				if (A_0.LocalName != ClipboardData.b("ᙳ᝵᭷ᅹ᭻౽", a_))
				{
					num = 10;
					continue;
				}
				Stream a_2 = this.ᜢ(A_0);
				xmlReader = spr\u23D7.ᜀ(a_2);
				color = this.ᜃ(xmlReader.GetAttribute(ClipboardData.b("ᝳ᥵ᑷᕹ๻", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_)));
				this.ᜄ.Background.Color = color;
				this.ᜄ.Background.Type = BackgroundType.Color;
				num = 14;
				continue;
				IL_21B:
				num = 2;
				continue;
				IL_27C:
				this.ᜀ(xmlReader, this.ᜄ.Background);
				num = 17;
			}
			IL_B9:
			throw new XmlException(ClipboardData.b("ᙳ᝵᭷ᅹ᭻౽", a_));
		}
		}
	}

	// Token: 0x06001F2F RID: 7983 RVA: 0x002039C0 File Offset: 0x002029C0
	private void ᜀ(XmlReader A_0, Background A_1)
	{
		int a_ = 11;
		string text;
		for (;;)
		{
			IL_31:
			string attribute = A_0.GetAttribute(ClipboardData.b("հੲմቶ", a_));
			string attribute2 = A_0.GetAttribute(ClipboardData.b("ᡰᝲ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲\udab4톶\udfb8튺\udebc\udabe藀곂ꛄ닆꓈껊ꏌ믎ﻐ꿜뫞跠苢釤軦蛨藪黬蟮飰菲蛴", a_));
			for (;;)
			{
				IL_69:
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!this.\u1717().ContainsKey(text))
						{
							goto IL_155;
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
							num = 2;
							continue;
						}
						break;
					case 1:
						A_1.Type = ((attribute == ClipboardData.b("հᩲᥴቶ", a_)) ? BackgroundType.Texture : BackgroundType.Picture);
						text = this.ᜁ(attribute2, false, false);
						num = 0;
						continue;
					case 2:
						A_1.ImageRecord = this.ᜄ.Images.ᜀ(this.\u1717()[text]);
						num = 4;
						continue;
					case 3:
						goto IL_F2;
					case 4:
						return;
					case 5:
						if (true)
						{
						}
						num = 6;
						continue;
					case 6:
						if (attribute2 == null)
						{
							num = 3;
							continue;
						}
						num = 1;
						continue;
					case 7:
						if (attribute != null)
						{
							num = 5;
							continue;
						}
						return;
					}
					goto IL_31;
				}
			}
		}
		return;
		IL_F2:
		return;
		IL_155:
		A_1.ImageBytes = this.ᜮ(text);
		this.\u1717().Add(text, A_1.ImageRecord.ᜀ());
	}

	// Token: 0x06001F30 RID: 7984 RVA: 0x00203B48 File Offset: 0x00202B48
	private spr\u2215 ᜀ(IDocumentObject A_0)
	{
		spr\u2215 result;
		for (;;)
		{
			result = null;
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 is Footnote)
					{
						num = 5;
						continue;
					}
					num = 2;
					continue;
				case 1:
					result = (A_0 as spr\u1AE7).ᜆ().ᜂ().ᜐ();
					if (true)
					{
					}
					num = 10;
					continue;
				case 2:
					if (A_0 is Comment)
					{
						num = 8;
						continue;
					}
					num = 11;
					continue;
				case 3:
					return result;
				case 4:
					return result;
				case 5:
					result = (A_0 as Footnote).TextBody.ᜐ();
					num = 9;
					continue;
				case 6:
					if (A_0 is HeaderFooter)
					{
						num = 7;
						continue;
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
						num = 0;
						continue;
					}
					break;
				case 7:
					result = (A_0 as HeaderFooter).ᜐ();
					num = 4;
					continue;
				case 8:
					result = (A_0 as Comment).Body.ᜐ();
					num = 12;
					continue;
				case 9:
					return result;
				case 10:
					return result;
				case 11:
					if (A_0 is spr\u1AE7)
					{
						num = 1;
						continue;
					}
					result = this.ᜄ.LastSection.ᜀ();
					num = 3;
					continue;
				case 12:
					return result;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x06001F31 RID: 7985 RVA: 0x00203CE4 File Offset: 0x00202CE4
	private void ᜀ(XmlReader A_0, sprờ A_1)
	{
		int a_ = 12;
		int num = 25;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 7;
				continue;
			case 1:
			{
				bool flag;
				if (!flag)
				{
					num = 22;
					continue;
				}
				goto IL_1EF;
			}
			case 2:
				num = 8;
				continue;
			case 3:
				goto IL_1C9;
			case 4:
				goto IL_1C9;
			case 5:
				return;
			case 7:
			{
				string localName;
				if (!(localName == ClipboardData.b("űၳɵ㵷ᑹ᡻⹽", a_)))
				{
					num = 16;
					continue;
				}
				this.ᜌ(A_0, A_1.ᜅ());
				num = 3;
				continue;
			}
			case 8:
			{
				string localName;
				if (!(localName == ClipboardData.b("űၳɵ㭷ᕹቻ੽", a_)))
				{
					num = 0;
					continue;
				}
				this.ᜀ(A_0, A_1.ᜇ());
				num = 15;
				continue;
			}
			case 9:
				return;
			case 10:
				num = 12;
				continue;
			case 11:
				goto IL_1EF;
			case 12:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 13;
					continue;
				}
				goto IL_1C9;
			}
			case 13:
				num = 24;
				continue;
			case 14:
				goto IL_1EF;
			case 15:
				goto IL_1C9;
			case 16:
				num = 23;
				continue;
			case 17:
			{
				if (A_0.IsEmptyElement)
				{
					num = 9;
					continue;
				}
				bool flag = false;
				string localName2 = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 14;
					continue;
				}
				break;
			}
			case 18:
			{
				string localName2;
				if (!(A_0.LocalName != localName2))
				{
					num = 5;
					continue;
				}
				bool flag = false;
				num = 20;
				continue;
			}
			case 19:
				num = 17;
				continue;
			case 20:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 10;
					continue;
				}
				A_0.Read();
				num = 26;
				continue;
			case 21:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 19;
					continue;
				}
				A_0.Read();
				num = 6;
				continue;
			case 22:
				A_0.Read();
				num = 11;
				continue;
			case 23:
				goto IL_1C9;
			case 24:
			{
				string localName;
				if (!(localName == ClipboardData.b("űၳɵ⡷ࡹ", a_)))
				{
					num = 2;
					continue;
				}
				this.ᜀ(A_0, A_1.ᜀ());
				num = 4;
				continue;
			}
			case 26:
				goto IL_1EF;
			}
			IL_FA:
			num = 21;
			continue;
			goto IL_FA;
			IL_1C9:
			if (true)
			{
			}
			num = 1;
			continue;
			IL_1EF:
			num = 18;
		}
	}

	// Token: 0x06001F32 RID: 7986 RVA: 0x00203FF8 File Offset: 0x00202FF8
	private void ᜀ(XmlReader A_0, spr\u1AD2 A_1)
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
		this.ᜐ(A_0, A_1.ᜂ());
	}

	// Token: 0x06001F33 RID: 7987 RVA: 0x00204040 File Offset: 0x00203040
	private void ᜁ(XmlReader A_0, spr\u1AE7 A_1)
	{
		int a_ = 15;
		int num = 23;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string localName;
				if (!(localName == ClipboardData.b("ٴ፶൸⭺ོ", a_)))
				{
					num = 8;
					continue;
				}
				this.ᜀ(A_0, A_1.ᜈ());
				num = 15;
				continue;
			}
			case 1:
				num = 0;
				continue;
			case 2:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 4;
					continue;
				}
				A_0.Read();
				num = 13;
				continue;
			case 3:
				goto IL_1C0;
			case 4:
				num = 24;
				continue;
			case 5:
				num = 17;
				continue;
			case 6:
				return;
			case 7:
				goto IL_1C0;
			case 8:
				num = 21;
				continue;
			case 9:
				return;
			case 10:
				goto IL_1C0;
			case 11:
			{
				string localName;
				if (!(localName == ClipboardData.b("ٴ፶൸㹺፼᭾톀", a_)))
				{
					num = 5;
					continue;
				}
				this.ᜌ(A_0, A_1.ᜏ());
				num = 14;
				continue;
			}
			case 12:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 1;
					continue;
				}
				goto IL_1A2;
			}
			case 14:
				goto IL_1A2;
			case 15:
				goto IL_1A2;
			case 16:
			{
				bool flag;
				if (!flag)
				{
					num = 20;
					continue;
				}
				goto IL_1C0;
			}
			case 17:
				goto IL_2F4;
			case 18:
				goto IL_1A2;
			case 19:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2F4;
				default:
				{
					if (false)
					{
					}
					string localName2;
					if (!(A_0.LocalName != localName2))
					{
						num = 6;
						continue;
					}
					bool flag = false;
					num = 26;
					continue;
				}
				}
				break;
			case 20:
				A_0.Read();
				num = 3;
				continue;
			case 21:
			{
				string localName;
				if (!(localName == ClipboardData.b("ٴ፶൸㡺ቼᅾ", a_)))
				{
					num = 22;
					continue;
				}
				this.ᜀ(A_0, A_1);
				num = 18;
				continue;
			}
			case 22:
				num = 11;
				continue;
			case 24:
			{
				if (A_0.IsEmptyElement)
				{
					num = 9;
					continue;
				}
				bool flag = false;
				string localName2 = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				num = 10;
				continue;
			}
			case 25:
				if (true)
				{
				}
				num = 12;
				continue;
			case 26:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 25;
					continue;
				}
				A_0.Read();
				num = 7;
				continue;
			}
			IL_F5:
			num = 2;
			continue;
			goto IL_F5;
			IL_1A2:
			num = 16;
			continue;
			IL_2F4:
			goto IL_1A2;
			IL_1C0:
			num = 19;
		}
	}

	// Token: 0x06001F34 RID: 7988 RVA: 0x00204348 File Offset: 0x00203348
	private void ᜀ(XmlReader A_0, ParagraphItemCollection A_1)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 14;
			Stream stream;
			for (;;)
			{
				Document document;
				switch (num)
				{
				case 0:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 44;
						continue;
					}
					goto IL_56A;
				}
				case 1:
					goto IL_819;
				case 2:
					goto IL_5AD;
				case 3:
					num = 27;
					continue;
				case 4:
					num = 15;
					continue;
				case 5:
					goto IL_56A;
				case 6:
					goto IL_155;
				case 7:
				{
					string localName2;
					if (!(A_0.LocalName != localName2))
					{
						num = 43;
						continue;
					}
					bool flag = false;
					num = 23;
					continue;
				}
				case 8:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								IEnumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								Section section = (Section)enumerator.Current;
								IEnumerator enumerator2 = section.Body.Items.GetEnumerator();
								num = 2;
								continue;
							}
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
								break;
							case 2:
								try
								{
									num = 8;
									for (;;)
									{
										DocumentObject documentObject;
										switch (num)
										{
										case 0:
											if (documentObject is Paragraph)
											{
												num = 4;
												continue;
											}
											goto IL_350;
										case 1:
											goto IL_452;
										case 3:
											if (A_1 != null)
											{
												num = 6;
												continue;
											}
											goto IL_350;
										case 4:
										{
											IEnumerator enumerator3 = (documentObject as Paragraph).Items.GetEnumerator();
											num = 9;
											continue;
										}
										case 5:
											goto IL_446;
										case 6:
											num = 0;
											continue;
										case 7:
										{
											IEnumerator enumerator2;
											if (!enumerator2.MoveNext())
											{
												num = 5;
												continue;
											}
											documentObject = (DocumentObject)enumerator2.Current;
											num = 3;
											continue;
										}
										case 9:
											try
											{
												num = 3;
												for (;;)
												{
													switch (num)
													{
													case 0:
													{
														IEnumerator enumerator3;
														if (!enumerator3.MoveNext())
														{
															num = 2;
															continue;
														}
														DocumentObject documentObject2 = (DocumentObject)enumerator3.Current;
														A_1.Add(documentObject2.Clone());
														num = 4;
														continue;
													}
													case 1:
														goto IL_3F8;
													case 2:
														num = 1;
														continue;
													}
													IL_3AB:
													num = 0;
													continue;
													goto IL_3AB;
												}
												IL_3F8:
												break;
											}
											finally
											{
												for (;;)
												{
													IEnumerator enumerator3;
													IDisposable disposable = enumerator3 as IDisposable;
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
															goto IL_445;
														case 2:
															goto IL_443;
														}
														break;
													}
												}
												IL_443:
												IL_445:;
											}
											goto IL_446;
										}
										IL_2E9:
										num = 7;
										continue;
										goto IL_2E9;
										IL_350:
										this.ᜄ.LastSection.Body.Items.Add(documentObject.Clone());
										num = 2;
										continue;
										IL_446:
										num = 1;
									}
									IL_452:;
								}
								finally
								{
									for (;;)
									{
										IEnumerator enumerator2;
										IDisposable disposable2 = enumerator2 as IDisposable;
										num = 1;
										for (;;)
										{
											switch (num)
											{
											case 0:
												disposable2.Dispose();
												num = 2;
												continue;
											case 1:
												if (disposable2 != null)
												{
													num = 0;
													continue;
												}
												goto IL_49C;
											case 2:
												goto IL_49A;
											}
											break;
										}
									}
									IL_49A:
									IL_49C:;
								}
								break;
							case 3:
								num = 4;
								continue;
							case 4:
								goto IL_501;
							}
							IL_49D:
							num = 0;
							continue;
							goto IL_49D;
						}
						IL_501:
						goto IL_8BE;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator;
							IDisposable disposable3 = enumerator as IDisposable;
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									disposable3.Dispose();
									num = 2;
									continue;
								case 1:
									if (disposable3 != null)
									{
										num = 0;
										continue;
									}
									goto IL_54E;
								case 2:
									goto IL_54C;
								}
								break;
							}
						}
						IL_54C:
						IL_54E:;
					}
					goto IL_54F;
					IL_8BE:
					Document.IsCloneParagraphCheckFormat = false;
					num = 11;
					continue;
				case 9:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 29;
						continue;
					}
					A_0.Read();
					num = 34;
					continue;
				case 10:
				{
					string attribute;
					string text = this.\u1718()[attribute].Value.ToString();
					stream = this.\u1715(text);
					num = 32;
					continue;
				}
				case 11:
					goto IL_54F;
				case 12:
					A_0.Read();
					num = 35;
					continue;
				case 13:
				{
					string attribute2;
					attribute2 == ClipboardData.b("ᩭɯݱᅳ", a_);
					num = 5;
					continue;
				}
				case 15:
				{
					string a;
					if (!(a == ClipboardData.b("੭Ὧᅱ", a_)))
					{
						num = 33;
						continue;
					}
					document.LoadFromStream(stream, FileFormat.Doc);
					num = 25;
					continue;
				}
				case 16:
				{
					string attribute2;
					if (!(attribute2 == ClipboardData.b("ᩭ", a_)))
					{
						num = 13;
						continue;
					}
					goto IL_56A;
				}
				case 17:
					goto IL_565;
				case 18:
					if (stream != null)
					{
						num = 28;
						continue;
					}
					goto IL_8D5;
				case 19:
				{
					string a;
					if (!(a == ClipboardData.b("ᱭѯᑱ", a_)))
					{
						num = 21;
						continue;
					}
					document.LoadFromStream(stream, FileFormat.Rtf);
					num = 31;
					continue;
				}
				case 20:
					if (!A_0.IsEmptyElement)
					{
						num = 12;
						continue;
					}
					goto IL_1F0;
				case 21:
					num = 6;
					continue;
				case 22:
				{
					string localName;
					if (localName == ClipboardData.b("ͭᅯٱᝳṵ⭷ࡹύ", a_))
					{
						num = 30;
						continue;
					}
					goto IL_56A;
				}
				case 23:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 39;
						continue;
					}
					A_0.Read();
					num = 1;
					continue;
				case 24:
					goto IL_819;
				case 25:
					goto IL_155;
				case 26:
				{
					if (true)
					{
					}
					bool flag;
					if (!flag)
					{
						num = 40;
						continue;
					}
					goto IL_819;
				}
				case 27:
				{
					string a;
					if (!(a == ClipboardData.b("੭Ὧᅱ౳", a_)))
					{
						num = 4;
						continue;
					}
					document.LoadFromStream(stream, FileFormat.Docx);
					num = 37;
					continue;
				}
				case 28:
				{
					string text;
					string text2 = text.Substring(text.LastIndexOf('.') + 1).ToLower();
					stream.Position = 0L;
					document = new Document();
					num = 42;
					continue;
				}
				case 29:
				{
					stream = null;
					bool flag = false;
					string text = string.Empty;
					string localName2 = A_0.LocalName;
					A_0.LocalName == ClipboardData.b("཭ᱯٱ㝳ṵ൷ᑹ᝻", a_);
					string attribute = A_0.GetAttribute(ClipboardData.b("ݭᑯ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯\uddb1튳킵톷\ud9b9\ud9bb諾꾿ꇁ뇃ꯅ귇꓉룋꣙맛닝臟雡跣觥蛧駩蓫蟭胯臱", a_));
					num = 41;
					continue;
				}
				case 30:
				{
					string attribute2 = A_0.GetAttribute(ClipboardData.b("ᡭᅯṱ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_));
					num = 16;
					continue;
				}
				case 31:
					goto IL_155;
				case 32:
					goto IL_5AD;
				case 33:
					num = 19;
					continue;
				case 35:
					goto IL_819;
				case 36:
				{
					Document.IsCloneParagraphCheckFormat = true;
					IEnumerator enumerator = document.Sections.GetEnumerator();
					num = 8;
					continue;
				}
				case 37:
					goto IL_155;
				case 38:
					if (document.Sections.Count > 0)
					{
						num = 36;
						continue;
					}
					goto IL_54F;
				case 39:
					num = 0;
					continue;
				case 40:
					A_0.Read();
					num = 24;
					continue;
				case 41:
				{
					string attribute;
					if (this.\u1718().ContainsKey(attribute))
					{
						num = 10;
						continue;
					}
					string text = attribute;
					stream = this.\u1715(attribute);
					num = 2;
					continue;
				}
				case 42:
				{
					string a;
					string text2;
					if ((a = text2) != null)
					{
						num = 3;
						continue;
					}
					goto IL_155;
				}
				case 43:
					goto IL_1F0;
				case 44:
					num = 22;
					continue;
				}
				goto IL_E0;
				IL_155:
				num = 38;
				continue;
				IL_1F0:
				num = 18;
				continue;
				IL_54F:
				document.Close();
				document = null;
				num = 17;
				continue;
				IL_56A:
				num = 26;
				continue;
				IL_5AD:
				num = 20;
				continue;
				IL_819:
				num = 7;
				continue;
				IL_861:
				num = 9;
				continue;
				IL_E0:
				goto IL_861;
			}
			IL_565:
			IL_8D5:
			stream.Dispose();
			return;
		}
		}
	}

	// Token: 0x06001F35 RID: 7989 RVA: 0x00204C7C File Offset: 0x00203C7C
	private Stream \u1715(string A_0)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 3;
			spr\u22A5 spr_u22A;
			spr\u22A5 spr_u22A2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_FF;
				case 1:
					goto IL_68;
				case 2:
					if (spr_u22A != null)
					{
						num = 5;
						continue;
					}
					goto IL_10F;
				case 4:
					if (spr_u22A2 != null)
					{
						num = 0;
						continue;
					}
					goto IL_B6;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_68;
					default:
						goto IL_AE;
					}
					break;
				}
				if (true)
				{
				}
				if (A_0.Contains(ClipboardData.b("婴", a_)))
				{
					num = 1;
					continue;
				}
				spr_u22A = this.ᜀ(ClipboardData.b("ɴᡶ୸ὺ剼", a_), A_0);
				num = 2;
				continue;
				IL_68:
				string a_2 = A_0.Substring(0, A_0.LastIndexOf('/') + 1);
				string a_3 = A_0.Substring(A_0.LastIndexOf('/') + 1);
				spr_u22A2 = this.ᜀ(a_2, a_3);
				num = 4;
			}
			IL_AE:
			if (false)
			{
			}
			return spr_u22A.ᜁ();
			IL_B6:
			return null;
			IL_FF:
			return spr_u22A2.ᜁ();
			IL_10F:
			return null;
		}
		}
	}

	// Token: 0x06001F36 RID: 7990 RVA: 0x00204DA4 File Offset: 0x00203DA4
	private void ᜌ(XmlReader A_0, CharacterFormat A_1)
	{
		int a_ = 3;
		int num = 11;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				num = 4;
				continue;
			case 1:
				this.ᜋ(A_0, A_1);
				num = 2;
				continue;
			case 2:
				goto IL_6E;
			case 3:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 16;
					continue;
				}
				goto IL_6E;
			}
			case 4:
			{
				if (A_0.IsEmptyElement)
				{
					num = 10;
					continue;
				}
				bool flag = false;
				string localName2 = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				num = 17;
				continue;
			}
			case 5:
				return;
			case 6:
				num = 3;
				continue;
			case 7:
			{
				string localName;
				if (localName == ClipboardData.b("᭨㭪Ὤ", a_))
				{
					num = 1;
					continue;
				}
				goto IL_6E;
			}
			case 8:
				goto IL_14F;
			case 9:
				goto IL_14F;
			case 10:
				return;
			case 12:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 6;
					continue;
				}
				A_0.Read();
				num = 8;
				continue;
			case 13:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 0;
					continue;
				}
				A_0.Read();
				num = 15;
				continue;
			case 14:
				A_0.Read();
				num = 9;
				continue;
			case 16:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 17:
				goto IL_14F;
			case 18:
			{
				string localName2;
				if (!(A_0.LocalName != localName2))
				{
					num = 5;
					continue;
				}
				bool flag = false;
				num = 12;
				continue;
			}
			case 19:
			{
				bool flag;
				if (!flag)
				{
					num = 14;
					continue;
				}
				goto IL_14F;
			}
			}
			goto IL_69;
			IL_6E:
			num = 19;
			continue;
			IL_101:
			num = 13;
			continue;
			IL_69:
			goto IL_101;
			IL_14F:
			num = 18;
		}
	}

	// Token: 0x06001F37 RID: 7991 RVA: 0x00204FDC File Offset: 0x00203FDC
	private void ᜀ(XmlReader A_0, spr\u1AE7 A_1)
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
		this.ᜏ(A_0, A_1);
	}

	// Token: 0x06001F38 RID: 7992 RVA: 0x00205020 File Offset: 0x00204020
	private void ᜀ(XmlReader A_0, spr\u1803 A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 50;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_284;
				case 1:
					num = 11;
					continue;
				case 2:
				{
					string attribute;
					if (!(attribute == ClipboardData.b("ٴ፶൸㝺ቼ᱾", a_)))
					{
						num = 1;
						continue;
					}
					A_1.ᜀ(LockSettings.SDTLocked);
					num = 10;
					continue;
				}
				case 3:
					goto IL_284;
				case 4:
				{
					string attribute2;
					if (attribute2 != string.Empty)
					{
						num = 14;
						continue;
					}
					goto IL_750;
				}
				case 5:
					goto IL_284;
				case 6:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 12;
						continue;
					}
					A_0.Read();
					num = 57;
					continue;
				case 7:
					A_0.Read();
					num = 48;
					continue;
				case 8:
					goto IL_284;
				case 9:
					goto IL_284;
				case 10:
					goto IL_284;
				case 11:
				{
					string attribute;
					if (!(attribute == ClipboardData.b("ٴ፶൸㡺ቼᅾ얈", a_)))
					{
						num = 53;
						continue;
					}
					A_1.ᜀ(LockSettings.SDTContentLocked);
					num = 27;
					continue;
				}
				case 12:
					num = 17;
					continue;
				case 13:
					goto IL_750;
				case 14:
				{
					string attribute2;
					A_1.ᜏ().ᜀ(attribute2);
					num = 13;
					continue;
				}
				case 15:
				{
					string attribute;
					if ((attribute = A_0.GetAttribute(ClipboardData.b("ʹᙶᕸ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_))) != null)
					{
						num = 38;
						continue;
					}
					goto IL_284;
				}
				case 16:
					goto IL_284;
				case 17:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 54;
						continue;
					}
					goto IL_284;
				}
				case 18:
				{
					string attribute;
					if (!(attribute == ClipboardData.b("ᙴᡶ᝸ེ᡼ᅾ쾂", a_)))
					{
						num = 40;
						continue;
					}
					A_1.ᜀ(LockSettings.ContentLocked);
					num = 42;
					continue;
				}
				case 19:
					goto IL_284;
				case 20:
				{
					bool flag;
					if (!flag)
					{
						num = 7;
						continue;
					}
					goto IL_3A9;
				}
				case 21:
					num = 8;
					continue;
				case 22:
					goto IL_284;
				case 23:
					num = 31;
					continue;
				case 24:
					goto IL_284;
				case 25:
					return;
				case 26:
					num = 19;
					continue;
				case 27:
					goto IL_284;
				case 28:
					goto IL_284;
				case 29:
					if (spr᧓.ᝠ == null)
					{
						num = 43;
						continue;
					}
					goto IL_170;
				case 30:
					return;
				case 31:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5AC;
					default:
					{
						if (false)
						{
						}
						int num2;
						switch (num2)
						{
						case 0:
							A_1.ᜃ(A_0.GetAttribute(ClipboardData.b("ʹᙶᕸ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_)));
							num = 24;
							continue;
						case 1:
							A_1.ᜁ(true);
							num = 5;
							continue;
						case 2:
							A_1.ᜀ(A_0.GetAttribute(ClipboardData.b("ʹᙶᕸ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_)));
							num = 59;
							continue;
						case 3:
							A_1.ᜃ(true);
							num = 0;
							continue;
						case 4:
							A_1.ᜀ(true);
							num = 35;
							continue;
						case 5:
							A_1.ᜂ(true);
							num = 9;
							continue;
						case 6:
							A_1.ᜀ(StructureDocumentType.Equation);
							num = 36;
							continue;
						case 7:
							A_1.ᜀ(StructureDocumentType.Picture);
							num = 51;
							continue;
						case 8:
							A_1.ᜀ(StructureDocumentType.Text);
							num = 49;
							continue;
						case 9:
							A_1.ᜀ(StructureDocumentType.RichText);
							num = 44;
							continue;
						case 10:
							A_1.ᜀ(StructureDocumentType.ComboBox);
							A_1.ᜀ(new sprᢾ());
							A_1.ᜉ().ᜀ(A_0.GetAttribute(ClipboardData.b("ᥴᙶ੸ེ⭼Ṿ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_)));
							this.ᜀ(A_0, A_1.ᜉ());
							num = 16;
							continue;
						case 11:
							A_1.ᜀ(StructureDocumentType.DropDownList);
							A_1.ᜀ(new spr\u24AE());
							A_1.ᜆ().ᜀ(A_0.GetAttribute(ClipboardData.b("ᥴᙶ੸ེ⭼Ṿ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_)));
							this.ᜀ(A_0, A_1.ᜆ());
							num = 52;
							continue;
						case 12:
							num = 15;
							continue;
						case 13:
						{
							A_1.ᜀ(new spr\u2319());
							A_1.ᜀ(StructureDocumentType.DatePicker);
							string attribute2 = A_0.GetAttribute(ClipboardData.b("፴ɶᕸ᝺㥼Ṿ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_));
							num = 4;
							continue;
						}
						case 14:
							A_1.ᜀ(new spr\u1FAF());
							A_1.ᜄ().ᜀ(A_0.GetAttribute(ClipboardData.b("൴ݶᡸེᕼ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_)));
							A_1.ᜄ().ᜂ(A_0.GetAttribute(ClipboardData.b("ٴͶᙸॺ᡼㙾캆춈", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_)));
							A_1.ᜄ().ᜁ(A_0.GetAttribute(ClipboardData.b("մնᱸᵺᑼݾ첀ﲎ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_)));
							num = 45;
							continue;
						case 15:
							A_1.ᜀ(new spr\u1A70());
							A_1.ᜀ(StructureDocumentType.CheckBox);
							this.ᜀ(A_0, A_1.\u170D());
							num = 22;
							continue;
						case 16:
							A_1.ᜀ(new spr\u259E());
							this.ᜀ(A_0, A_1.ᜎ());
							num = 46;
							continue;
						case 17:
							A_1.ᜀ(new spr\u22CC());
							this.ᜀ(A_0, A_1.ᜋ());
							num = 28;
							continue;
						default:
							num = 21;
							continue;
						}
						break;
					}
					}
					break;
				case 32:
				{
					string localName2;
					if (!(A_0.LocalName != localName2))
					{
						num = 25;
						continue;
					}
					bool flag = false;
					num = 6;
					continue;
				}
				case 33:
				{
					string attribute;
					if (!(attribute == ClipboardData.b("t᥶ᕸᑺṼᑾ", a_)))
					{
						num = 26;
						continue;
					}
					A_1.ᜀ(LockSettings.UnLocked);
					num = 55;
					continue;
				}
				case 34:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 58;
						continue;
					}
					A_0.Read();
					num = 41;
					continue;
				case 35:
					goto IL_284;
				case 36:
					if (true)
					{
					}
					goto IL_284;
				case 37:
					goto IL_5AC;
				case 38:
					num = 2;
					continue;
				case 39:
					goto IL_3A9;
				case 40:
					num = 33;
					continue;
				case 42:
					goto IL_284;
				case 43:
					spr᧓.ᝠ = new Dictionary<string, int>(18)
					{
						{
							ClipboardData.b("ᱴ፶", a_),
							0
						},
						{
							ClipboardData.b("ٴὶᙸ౺ᑼᅾ펂솈ﾌ", a_),
							1
						},
						{
							ClipboardData.b("ᑴ᭶ၸ᩺๼", a_),
							2
						},
						{
							ClipboardData.b("᝴Ṷ᭸᝺ᑼၾ", a_),
							3
						},
						{
							ClipboardData.b("ᙴṶ൸᩺ॼᙾ", a_),
							4
						},
						{
							ClipboardData.b("Ŵቶᑸ୺ቼൾﲄ", a_),
							5
						},
						{
							ClipboardData.b("ၴٶ౸᩺ॼᙾ", a_),
							6
						},
						{
							ClipboardData.b("մṶེ᩸ࡼൾ", a_),
							7
						},
						{
							ClipboardData.b("ŴቶŸེ", a_),
							8
						},
						{
							ClipboardData.b("ݴṶ᩸፺⥼᩾呂", a_),
							9
						},
						{
							ClipboardData.b("ᙴᡶᑸ᥺ቼ㵾ﮂ", a_),
							10
						},
						{
							ClipboardData.b("ᅴնᙸ୺㥼ၾ즄愈ﾊ", a_),
							11
						},
						{
							ClipboardData.b("ᥴᡶ᩸ၺ", a_),
							12
						},
						{
							ClipboardData.b("ᅴᙶ൸Ṻ", a_),
							13
						},
						{
							ClipboardData.b("ᅴᙶ൸᩺㽼ᙾ", a_),
							14
						},
						{
							ClipboardData.b("ᙴὶᱸ᡺ᙼᵾﮂ", a_),
							15
						},
						{
							ClipboardData.b("ᅴᡶ᩸⭺ᱼൾ첂", a_),
							16
						},
						{
							ClipboardData.b("ᅴᡶ᩸⭺ᱼൾ쾂ﶈ", a_),
							17
						}
					};
					num = 37;
					continue;
				case 44:
					goto IL_284;
				case 45:
					goto IL_284;
				case 46:
					goto IL_284;
				case 47:
				{
					string localName;
					int num2;
					if (spr᧓.ᝠ.TryGetValue(localName, out num2))
					{
						num = 23;
						continue;
					}
					goto IL_284;
				}
				case 48:
					goto IL_3A9;
				case 49:
					goto IL_284;
				case 51:
					goto IL_284;
				case 52:
					goto IL_284;
				case 53:
					num = 18;
					continue;
				case 54:
					num = 29;
					continue;
				case 55:
					goto IL_284;
				case 56:
				{
					if (A_0.IsEmptyElement)
					{
						num = 30;
						continue;
					}
					bool flag = false;
					string localName2 = A_0.LocalName;
					A_0.Read();
					this.ᜀ(A_0);
					num = 39;
					continue;
				}
				case 57:
					goto IL_3A9;
				case 58:
					num = 56;
					continue;
				case 59:
					goto IL_284;
				}
				goto IL_11C;
				IL_170:
				num = 47;
				continue;
				IL_5AC:
				goto IL_170;
				IL_284:
				num = 20;
				continue;
				IL_3A9:
				num = 32;
				continue;
				IL_750:
				this.ᜀ(A_0, A_1.ᜏ());
				num = 3;
				continue;
				IL_7FE:
				num = 34;
				continue;
				IL_11C:
				goto IL_7FE;
			}
			return;
		}
		}
	}

	// Token: 0x06001F39 RID: 7993 RVA: 0x00205A98 File Offset: 0x00204A98
	private void ᜀ(XmlReader A_0, spr\u22CC A_1)
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
		this.ᜀ(A_0, A_1);
	}

	// Token: 0x06001F3A RID: 7994 RVA: 0x00205ADC File Offset: 0x00204ADC
	private void ᜀ(XmlReader A_0, spr\u1DC1 A_1)
	{
		int a_ = 4;
		int num = 23;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1C2;
			case 1:
				return;
			case 2:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 17;
					continue;
				}
				goto IL_1C2;
			}
			case 3:
				goto IL_1E0;
			case 4:
				goto IL_1E0;
			case 5:
				num = 18;
				continue;
			case 6:
			{
				bool flag;
				if (!flag)
				{
					num = 16;
					continue;
				}
				goto IL_1E0;
			}
			case 7:
			{
				string localName2;
				if (A_0.LocalName != localName2)
				{
					bool flag = false;
					num = 20;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_196;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			case 8:
			{
				if (A_0.IsEmptyElement)
				{
					num = 11;
					continue;
				}
				bool flag = false;
				string localName2 = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				num = 3;
				continue;
			}
			case 9:
				goto IL_1C2;
			case 10:
				goto IL_196;
			case 11:
				return;
			case 12:
				num = 8;
				continue;
			case 13:
				goto IL_1C2;
			case 14:
				goto IL_1C2;
			case 15:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 12;
					continue;
				}
				A_0.Read();
				num = 25;
				continue;
			case 16:
				A_0.Read();
				num = 4;
				continue;
			case 17:
				num = 22;
				continue;
			case 18:
			{
				string localName;
				if (!(localName == ClipboardData.b("๩ͫ൭⁯፱ٳɵ㽷᭹ၻችﶃ", a_)))
				{
					num = 10;
					continue;
				}
				A_1.ᜁ(A_0.GetAttribute(ClipboardData.b("ᱩ൫ɭ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫\ud9ad\udfaf삱킳욵쪷햹\udfbb\udbbd뎿뇁귃ꣅ꿇Ꟊꃋ럙뷛럝軟", a_)));
				num = 14;
				continue;
			}
			case 19:
				num = 2;
				continue;
			case 20:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 19;
					continue;
				}
				A_0.Read();
				num = 24;
				continue;
			case 21:
			{
				string localName;
				if (!(localName == ClipboardData.b("๩ͫ൭⁯፱ٳɵ㭷᭹ࡻ᭽ﾅ", a_)))
				{
					num = 26;
					continue;
				}
				A_1.ᜀ(A_0.GetAttribute(ClipboardData.b("ᱩ൫ɭ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫\ud9ad\udfaf삱킳욵쪷햹\udfbb\udbbd뎿뇁귃ꣅ꿇Ꟊꃋ럙뷛럝軟", a_)));
				num = 0;
				continue;
			}
			case 22:
			{
				string localName;
				if (!(localName == ClipboardData.b("๩ͫ൭⁯፱ٳɵ⵷ᑹᕻཽ", a_)))
				{
					num = 5;
					continue;
				}
				A_1.ᜀ(true);
				num = 9;
				continue;
			}
			case 24:
				goto IL_1E0;
			case 26:
				num = 13;
				continue;
			}
			IL_10E:
			num = 15;
			continue;
			goto IL_10E;
			IL_196:
			num = 21;
			continue;
			IL_1C2:
			num = 6;
			continue;
			IL_1E0:
			if (true)
			{
			}
			num = 7;
		}
	}

	// Token: 0x06001F3B RID: 7995 RVA: 0x00205E20 File Offset: 0x00204E20
	private void ᜀ(XmlReader A_0, spr\u259E A_1)
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
		this.ᜀ(A_0, A_1);
	}

	// Token: 0x06001F3C RID: 7996 RVA: 0x00205E64 File Offset: 0x00204E64
	private void ᜀ(XmlReader A_0, spr\u1A70 A_1)
	{
		int a_ = 0;
		int num = 19;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_208;
			case 1:
				num = 14;
				continue;
			case 2:
			{
				if (A_0.IsEmptyElement)
				{
					num = 10;
					continue;
				}
				bool flag = false;
				string localName = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				num = 3;
				continue;
			}
			case 3:
				goto IL_226;
			case 4:
				goto IL_226;
			case 5:
			{
				string localName;
				if (A_0.LocalName != localName)
				{
					bool flag = false;
					num = 7;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1DC;
				default:
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			}
			case 6:
				num = 26;
				continue;
			case 7:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 15;
					continue;
				}
				A_0.Read();
				num = 4;
				continue;
			case 8:
				return;
			case 9:
			{
				bool flag;
				if (!flag)
				{
					num = 21;
					continue;
				}
				goto IL_226;
			}
			case 10:
				goto IL_2CB;
			case 12:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 25;
					continue;
				}
				A_0.Read();
				num = 11;
				continue;
			case 13:
				goto IL_1DC;
			case 14:
				goto IL_208;
			case 15:
				num = 20;
				continue;
			case 16:
				goto IL_208;
			case 17:
			{
				string localName2;
				if (!(localName2 == ClipboardData.b("եgཀྵཫխᕯᙱ❳ɵ᥷๹᥻", a_)))
				{
					num = 13;
					continue;
				}
				A_1.ᜀ().ᜀ(A_0.GetAttribute(ClipboardData.b("ၥ१٩", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﶍﾏ뢕ﮗ놝쾟쒡슣쾥쮧쾩莫\ud9ad\udfaf삱킳馵誷誹趻躽뗁ꯃ듅곇Ꟊꃋ", a_)));
				A_1.ᜀ().ᜁ(A_0.GetAttribute(ClipboardData.b("eݧѩᡫ", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﶍﾏ뢕ﮗ놝쾟쒡슣쾥쮧쾩莫\ud9ad\udfaf삱킳馵誷誹趻躽뗁ꯃ듅곇Ꟊꃋ", a_)));
				num = 16;
				continue;
			}
			case 18:
				goto IL_208;
			case 20:
			{
				string localName2;
				if ((localName2 = A_0.LocalName) != null)
				{
					num = 6;
					continue;
				}
				goto IL_208;
			}
			case 21:
				A_0.Read();
				num = 22;
				continue;
			case 22:
				goto IL_226;
			case 23:
				num = 17;
				continue;
			case 24:
			{
				string localName2;
				if (!(localName2 == ClipboardData.b("፥٧३ѫ୭፯ᥱᅳት⭷๹ᵻ੽", a_)))
				{
					num = 1;
					continue;
				}
				A_1.ᜂ().ᜁ(A_0.GetAttribute(ClipboardData.b("eݧѩᡫ", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﶍﾏ뢕ﮗ놝쾟쒡슣쾥쮧쾩莫\ud9ad\udfaf삱킳馵誷誹趻躽뗁ꯃ듅곇Ꟊꃋ", a_)));
				A_1.ᜂ().ᜀ(A_0.GetAttribute(ClipboardData.b("ၥ१٩", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﶍﾏ뢕ﮗ놝쾟쒡슣쾥쮧쾩莫\ud9ad\udfaf삱킳馵誷誹趻躽뗁ꯃ듅곇Ꟊꃋ", a_)));
				num = 18;
				continue;
			}
			case 25:
				num = 2;
				continue;
			case 26:
			{
				string localName2;
				if (!(localName2 == ClipboardData.b("եgཀྵཫխᕯᙱ", a_)))
				{
					num = 23;
					continue;
				}
				A_1.ᜀ(this.ᜀ(A_0, ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﶍﾏ뢕ﮗ놝쾟쒡슣쾥쮧쾩莫\ud9ad\udfaf삱킳馵誷誹趻躽뗁ꯃ듅곇Ꟊꃋ", a_)));
				num = 0;
				continue;
			}
			}
			IL_140:
			num = 12;
			continue;
			goto IL_140;
			IL_1DC:
			num = 24;
			continue;
			IL_208:
			num = 9;
			continue;
			IL_226:
			num = 5;
		}
		return;
		IL_2CB:
		if (true)
		{
		}
	}

	// Token: 0x06001F3D RID: 7997 RVA: 0x00206220 File Offset: 0x00205220
	private void ᜀ(XmlReader A_0, spr\u24AE A_1)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					A_0.Read();
					num = 15;
					continue;
				case 1:
					num = 13;
					continue;
				case 2:
				{
					bool flag;
					if (!flag)
					{
						num = 0;
						continue;
					}
					goto IL_1C2;
				}
				case 3:
					num = 12;
					continue;
				case 4:
					goto IL_1C2;
				case 5:
				{
					if (A_0.IsEmptyElement)
					{
						num = 14;
						continue;
					}
					bool flag = false;
					string localName = A_0.LocalName;
					A_0.Read();
					this.ᜀ(A_0);
					num = 4;
					continue;
				}
				case 6:
					num = 5;
					continue;
				case 9:
					goto IL_1C2;
				case 10:
				{
					spr\u25C2 spr_u25C = new spr\u25C2();
					spr_u25C.ᜁ(A_0.GetAttribute(ClipboardData.b("ͦhᡪᵬͮၰੲⅴቶŸེ", a_), ClipboardData.b("སᵨὪᵬ啮幰屲ٴᑶᅸṺၼṾ궂﶐杖漢辠첢힤삦蚨\udcaa슬\uddae햰쎲잴\ud8b6\udab8\udeba캼첾ꣀ귂ꋄ꫆ꗈￌￎ䀹뫖룘닚돜", a_)));
					spr_u25C.ᜀ(A_0.GetAttribute(ClipboardData.b("ᅦࡨݪᡬ੮", a_), ClipboardData.b("སᵨὪᵬ啮幰屲ٴᑶᅸṺၼṾ궂﶐杖漢辠첢힤삦蚨\udcaa슬\uddae햰쎲잴\ud8b6\udab8\udeba캼첾ꣀ귂ꋄ꫆ꗈￌￎ䀹뫖룘닚돜", a_)));
					A_1.ᜀ().Add(spr_u25C);
					num = 11;
					continue;
				}
				case 11:
					goto IL_7E;
				case 12:
				{
					string localName2;
					if ((localName2 = A_0.LocalName) != null)
					{
						num = 1;
						continue;
					}
					goto IL_7E;
				}
				case 13:
				{
					string localName2;
					if (localName2 == ClipboardData.b("୦hᡪᥬ♮հᙲᡴ", a_))
					{
						num = 10;
						continue;
					}
					goto IL_7E;
				}
				case 14:
					return;
				case 15:
					goto IL_1C2;
				case 16:
				{
					string localName;
					if (!(A_0.LocalName != localName))
					{
						num = 17;
						continue;
					}
					bool flag = false;
					num = 18;
					continue;
				}
				case 17:
					return;
				case 18:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 3;
						continue;
					}
					A_0.Read();
					num = 9;
					continue;
				case 19:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 6;
							continue;
						}
						A_0.Read();
						num = 8;
						continue;
					}
					break;
				}
				goto IL_79;
				IL_7E:
				num = 2;
				continue;
				IL_10A:
				num = 19;
				continue;
				IL_79:
				goto IL_10A;
				goto IL_79;
				IL_1C2:
				num = 16;
			}
			return;
		}
		}
	}

	// Token: 0x06001F3E RID: 7998 RVA: 0x002064E0 File Offset: 0x002054E0
	private void ᜀ(XmlReader A_0, sprᢾ A_1)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 15;
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
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 7;
							continue;
						}
						A_0.Read();
						num = 12;
						continue;
					}
					break;
				case 1:
					A_0.Read();
					num = 14;
					continue;
				case 2:
				{
					string localName;
					if (localName == ClipboardData.b("ᵰᩲٴͶへེ᡼ቾ", a_))
					{
						num = 6;
						continue;
					}
					goto IL_86;
				}
				case 3:
					goto IL_1C2;
				case 4:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 18;
						continue;
					}
					goto IL_86;
				}
				case 5:
				{
					if (A_0.IsEmptyElement)
					{
						num = 16;
						continue;
					}
					bool flag = false;
					string localName2 = A_0.LocalName;
					A_0.Read();
					this.ᜀ(A_0);
					num = 11;
					continue;
				}
				case 6:
				{
					spr\u25C2 spr_u25C = new spr\u25C2();
					spr_u25C.ᜁ(A_0.GetAttribute(ClipboardData.b("ᕰᩲٴݶᕸ᩺Ѽ⭾ﮂ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_)));
					spr_u25C.ᜀ(A_0.GetAttribute(ClipboardData.b("ݰቲᥴɶᱸ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_)));
					A_1.ᜀ().Add(spr_u25C);
					num = 19;
					continue;
				}
				case 7:
					num = 5;
					continue;
				case 8:
				{
					bool flag;
					if (!flag)
					{
						num = 1;
						continue;
					}
					goto IL_1C2;
				}
				case 9:
				{
					string localName2;
					if (!(A_0.LocalName != localName2))
					{
						num = 13;
						continue;
					}
					bool flag = false;
					num = 17;
					continue;
				}
				case 10:
					num = 4;
					continue;
				case 11:
					goto IL_1C2;
				case 12:
					goto IL_10A;
				case 13:
					return;
				case 14:
					goto IL_1C2;
				case 16:
					return;
				case 17:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 10;
						continue;
					}
					A_0.Read();
					num = 3;
					continue;
				case 18:
					num = 2;
					continue;
				case 19:
					goto IL_86;
				}
				goto IL_79;
				IL_86:
				num = 8;
				continue;
				IL_10A:
				num = 0;
				continue;
				IL_79:
				if (true)
				{
				}
				goto IL_10A;
				goto IL_79;
				IL_1C2:
				num = 9;
			}
			return;
		}
		}
	}

	// Token: 0x06001F3F RID: 7999 RVA: 0x002067A0 File Offset: 0x002057A0
	private void ᜀ(XmlReader A_0, spr\u2319 A_1)
	{
		int a_ = 17;
		int num = 13;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 16;
				continue;
			case 1:
				goto IL_1E6;
			case 2:
				num = 25;
				continue;
			case 3:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 26;
					continue;
				}
				A_0.Read();
				num = 19;
				continue;
			case 4:
				goto IL_1E6;
			case 5:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 0;
					continue;
				}
				goto IL_1E6;
			}
			case 6:
				num = 15;
				continue;
			case 7:
				num = 5;
				continue;
			case 8:
			{
				if (A_0.IsEmptyElement)
				{
					num = 10;
					continue;
				}
				bool flag = false;
				string localName2 = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				num = 9;
				continue;
			}
			case 9:
				goto IL_204;
			case 10:
				return;
			case 11:
				if (true)
				{
				}
				if (A_0.NodeType != XmlNodeType.Element)
				{
					A_0.Read();
					num = 23;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_168;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 12:
			{
				string localName2;
				if (!(A_0.LocalName != localName2))
				{
					num = 18;
					continue;
				}
				bool flag = false;
				num = 11;
				continue;
			}
			case 14:
				goto IL_204;
			case 15:
			{
				string localName;
				if (!(localName == ClipboardData.b("᭶ၸὺ", a_)))
				{
					num = 2;
					continue;
				}
				A_1.ᜁ(A_0.GetAttribute(ClipboardData.b("Ŷᡸ᝺", a_), ClipboardData.b("ὶ൸ེർ䕾꺀겂붒杖ﲘ춠얢쪤햦쒨쪪\ud9ac\udcae龰\udcb2잴킶隸첺튼춾ꗀ돂럄꣆꫈껊뻌볎룐뷒닔뫖뗘퇠헢쫤諦裨苪菬", a_)));
				num = 21;
				continue;
			}
			case 16:
			{
				string localName;
				if (!(localName == ClipboardData.b("፶ᡸེ᡼㥾ﶈ", a_)))
				{
					num = 6;
					continue;
				}
				A_1.ᜂ(A_0.GetAttribute(ClipboardData.b("Ŷᡸ᝺", a_), ClipboardData.b("ὶ൸ེർ䕾꺀겂붒杖ﲘ춠얢쪤햦쒨쪪\ud9ac\udcae龰\udcb2잴킶隸첺튼춾ꗀ돂럄꣆꫈껊뻌볎룐뷒닔뫖뗘퇠헢쫤諦裨苪菬", a_)));
				num = 24;
				continue;
			}
			case 17:
				A_0.Read();
				num = 14;
				continue;
			case 18:
				return;
			case 20:
				num = 4;
				continue;
			case 21:
				goto IL_1E6;
			case 22:
			{
				bool flag;
				if (!flag)
				{
					num = 17;
					continue;
				}
				goto IL_204;
			}
			case 23:
				goto IL_204;
			case 24:
				goto IL_168;
			case 25:
			{
				string localName;
				if (!(localName == ClipboardData.b("ᑶᡸ᝺᡼ᅾ", a_)))
				{
					num = 20;
					continue;
				}
				A_1.ᜀ(this.\u1714(A_0.GetAttribute(ClipboardData.b("Ŷᡸ᝺", a_), ClipboardData.b("ὶ൸ེർ䕾꺀겂붒杖ﲘ춠얢쪤햦쒨쪪\ud9ac\udcae龰\udcb2잴킶隸첺튼춾ꗀ돂럄꣆꫈껊뻌볎룐뷒닔뫖뗘퇠헢쫤諦裨苪菬", a_))));
				num = 1;
				continue;
			}
			case 26:
				num = 8;
				continue;
			}
			IL_10E:
			num = 3;
			continue;
			goto IL_10E;
			IL_1E6:
			num = 22;
			continue;
			IL_168:
			goto IL_1E6;
			IL_204:
			num = 12;
		}
	}

	// Token: 0x06001F40 RID: 8000 RVA: 0x00206B10 File Offset: 0x00205B10
	private CalendarType \u1714(string A_0)
	{
		int a_ = 14;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				spr᧓.ᝡ = new Dictionary<string, int>(13)
				{
					{
						ClipboardData.b("፳ѵᵷᵹ፻౽", a_),
						0
					},
					{
						ClipboardData.b("፳ѵᵷᵹ፻౽입慎", a_),
						1
					},
					{
						ClipboardData.b("፳ѵᵷᵹ፻౽쮅첉ﺋﺏﲓ", a_),
						2
					},
					{
						ClipboardData.b("፳ѵᵷᵹ፻౽펅ﮇ", a_),
						3
					},
					{
						ClipboardData.b("፳ѵᵷᵹ፻౽\ude85쮍ﺏﾕ", a_),
						4
					},
					{
						ClipboardData.b("፳ѵᵷᵹ፻౽\ude85좍望", a_),
						5
					},
					{
						ClipboardData.b("ᱳ፵᩷ࡹ᥻ॽ", a_),
						6
					},
					{
						ClipboardData.b("ᱳήቷࡹᕻ", a_),
						7
					},
					{
						ClipboardData.b("ṳ᝵ࡷ᭹ቻ", a_),
						8
					},
					{
						ClipboardData.b("έ᥵੷όᵻ", a_),
						9
					},
					{
						ClipboardData.b("ݳ᝵፷᭹", a_),
						10
					},
					{
						ClipboardData.b("s᝵ᅷ൹ᵻၽ", a_),
						11
					},
					{
						ClipboardData.b("sṵ᥷፹", a_),
						12
					}
				};
				num = 4;
				continue;
			case 1:
				if (spr᧓.ᝡ == null)
				{
					num = 0;
					continue;
				}
				goto IL_250;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6F;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 3:
				goto IL_21F;
			case 4:
				goto IL_250;
			case 5:
			{
				int num2;
				if (spr᧓.ᝡ.TryGetValue(A_0, out num2))
				{
					num = 9;
					continue;
				}
				return CalendarType.None;
			}
			case 6:
				num = 1;
				continue;
			case 7:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return CalendarType.Gregorian;
				case 1:
					return CalendarType.GregorianArabic;
				case 2:
					return CalendarType.GregorianMiddleEastFrench;
				case 3:
					return CalendarType.GregorianEnglish;
				case 4:
					return CalendarType.GregorianTransliteratedEnglish;
				case 5:
					return CalendarType.GregorianTransliteratedFrench;
				case 6:
					return CalendarType.Hebrew;
				case 7:
					return CalendarType.Hijri;
				case 8:
					return CalendarType.Japan;
				case 9:
					return CalendarType.Korean;
				case 10:
					return CalendarType.Saka;
				case 11:
					return CalendarType.Taiwan;
				case 12:
					return CalendarType.Thai;
				default:
					num = 8;
					continue;
				}
				break;
			}
			case 8:
				num = 3;
				continue;
			case 9:
				num = 7;
				continue;
			}
			goto IL_67;
			IL_6F:
			num = 6;
			continue;
			IL_250:
			num = 5;
			continue;
			IL_67:
			if (A_0 != null)
			{
				goto IL_6F;
			}
			return CalendarType.None;
		}
		return CalendarType.Hijri;
		IL_21F:
		return CalendarType.None;
	}

	// Token: 0x06001F41 RID: 8001 RVA: 0x00206DA4 File Offset: 0x00205DA4
	private void ᜁ(Stream A_0)
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
		XmlReader a_ = spr\u23D7.ᜀ(A_0);
		this.ᜀ(a_, this.\u1718());
	}

	// Token: 0x06001F42 RID: 8002 RVA: 0x00206DF4 File Offset: 0x00205DF4
	private void ᜀ(XmlReader A_0, Dictionary<string, DictionaryEntry> A_1)
	{
		int a_ = 9;
		switch (0)
		{
		default:
			for (;;)
			{
				A_0.MoveToContent();
				int num = 4;
				for (;;)
				{
					bool flag;
					string text;
					string text2;
					string text3;
					string a;
					bool flag2;
					switch (num)
					{
					case 0:
						num = 17;
						continue;
					case 1:
						if (flag)
						{
							num = 22;
							continue;
						}
						goto IL_208;
					case 2:
						if (!(A_0.LocalName != ClipboardData.b("㵮ᑰὲᑴͶၸᑺ፼౾", a_)))
						{
							num = 6;
							continue;
						}
						goto IL_130;
					case 3:
						if (text != null)
						{
							num = 18;
							continue;
						}
						goto IL_208;
					case 4:
						if (A_0.LocalName != ClipboardData.b("㵮ᑰὲᑴͶၸᑺ፼౾", a_))
						{
							num = 8;
							continue;
						}
						text2 = null;
						text3 = null;
						text = null;
						a = null;
						num = 7;
						continue;
					case 5:
						goto IL_CD;
					case 6:
						return;
					case 7:
						goto IL_130;
					case 8:
						goto IL_B6;
					case 9:
						goto IL_208;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_288;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 11:
						num = 13;
						continue;
					case 12:
						this.ᜌ().Add(text2, flag);
						goto IL_288;
					case 13:
						flag2 = false;
						goto IL_1E8;
					case 14:
						text3 = text3.Remove(0, 1);
						num = 5;
						continue;
					case 15:
						if (!this.ᜌ().ContainsKey(text2))
						{
							num = 12;
							continue;
						}
						goto IL_208;
					case 16:
						flag2 = true;
						goto IL_1E8;
					case 17:
						if (text3 != null)
						{
							num = 10;
							continue;
						}
						goto IL_208;
					case 18:
						num = 20;
						continue;
					case 19:
						if (text2 != null)
						{
							num = 0;
							continue;
						}
						goto IL_208;
					case 20:
						if (text3.StartsWith(ClipboardData.b("䁮", a_)))
						{
							num = 14;
							continue;
						}
						goto IL_CD;
					case 21:
						if (true)
						{
						}
						if (!(a == ClipboardData.b("⩮॰ݲၴն᝸᩺ᅼ", a_)))
						{
							num = 11;
							continue;
						}
						num = 16;
						continue;
					case 22:
						num = 15;
						continue;
					}
					break;
					IL_CD:
					DictionaryEntry value = new DictionaryEntry(text, text3);
					A_1.Add(text2, value);
					a = A_0.GetAttribute(ClipboardData.b("㭮ၰŲቴቶ൸㙺ቼ᭾", a_));
					num = 21;
					continue;
					IL_130:
					A_0.Read();
					text2 = A_0.GetAttribute(ClipboardData.b("♮ᕰ", a_));
					text3 = A_0.GetAttribute(ClipboardData.b("㭮ၰŲቴቶ൸", a_));
					text = A_0.GetAttribute(ClipboardData.b("㭮ࡰͲၴ", a_));
					num = 19;
					continue;
					IL_1E8:
					flag = flag2;
					num = 1;
					continue;
					IL_208:
					num = 2;
					continue;
					IL_288:
					num = 9;
				}
			}
			IL_B6:
			A_0.ReadInnerXml();
			return;
		}
	}

	// Token: 0x06001F43 RID: 8003 RVA: 0x00207128 File Offset: 0x00206128
	private void \u1715(XmlReader A_0)
	{
		int a_ = 2;
		int num = 1;
		for (;;)
		{
			CustomDocumentProperties customDocumentProperties;
			switch (num)
			{
			case 0:
				if (A_0.LocalName == ClipboardData.b("ᡧᡩͫṭᕯqsཱུ", a_))
				{
					num = 17;
					continue;
				}
				goto IL_14C;
			case 2:
				return;
			case 3:
				if (A_0.IsEmptyElement)
				{
					num = 2;
					continue;
				}
				customDocumentProperties = this.ᜄ.CustomDocumentProperties;
				A_0.Read();
				num = 18;
				continue;
			case 4:
				goto IL_8C;
			case 5:
				num = 0;
				continue;
			case 6:
				goto IL_11E;
			case 7:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 5;
					continue;
				}
				A_0.Skip();
				num = 14;
				continue;
			case 8:
				goto IL_CB;
			case 9:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 13;
					continue;
				}
				A_0.Read();
				num = 8;
				continue;
			case 10:
				goto IL_14C;
			case 11:
				goto IL_1CA;
			case 12:
				if (A_0.LocalName != ClipboardData.b("㡧ᡩͫṭᕯqsήᵷॹ", a_))
				{
					num = 11;
					continue;
				}
				num = 3;
				continue;
			case 13:
				num = 12;
				continue;
			case 14:
				goto IL_14C;
			case 15:
				goto IL_171;
			case 16:
				if (A_0.EOF)
				{
					num = 15;
					continue;
				}
				goto IL_11E;
			case 17:
				goto IL_C5;
			case 18:
				if (!A_0.EOF)
				{
					num = 6;
					continue;
				}
				return;
			}
			if (A_0 != null)
			{
				goto IL_CB;
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
				num = 4;
				continue;
			}
			IL_C5:
			this.ᜀ(A_0, customDocumentProperties);
			num = 10;
			continue;
			IL_CB:
			if (true)
			{
			}
			num = 9;
			continue;
			IL_11E:
			num = 7;
			continue;
			IL_14C:
			A_0.Read();
			num = 16;
		}
		IL_8C:
		throw new ArgumentNullException(ClipboardData.b("ᩧཀྵ൫੭ᕯq", a_));
		IL_171:
		return;
		IL_1CA:
		throw new XmlException(ClipboardData.b("㵧ѩ५᙭o᝱ᝳɵᵷṹ屻ٽꒃ겋", a_) + A_0.LocalName);
	}

	// Token: 0x06001F44 RID: 8004 RVA: 0x00207384 File Offset: 0x00206384
	private void ᜀ(XmlReader A_0, CustomDocumentProperties A_1)
	{
		int a_ = 7;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string text = A_0.Value;
				num = 21;
				continue;
			}
			case 1:
				IL_1CC:
				int.Parse(A_0.Value, CultureInfo.InvariantCulture);
				num = 9;
				continue;
			case 3:
				if (A_0.NodeType == XmlNodeType.Text)
				{
					num = 22;
					continue;
				}
				goto IL_1D1;
			case 4:
				if (!(A_0.LocalName != ClipboardData.b("ᵬᵮṰͲၴն൸ɺ", a_)))
				{
					num = 11;
					continue;
				}
				goto IL_36F;
			case 5:
				if (true)
				{
				}
				num = 10;
				continue;
			case 6:
				num = 17;
				continue;
			case 7:
				num = 4;
				continue;
			case 8:
			{
				string text;
				DocumentProperty documentProperty = new DocumentProperty(text, this.\u1713(A_0.Value));
				this.ᜄ.CustomDocumentProperties.CustomHash.Add(documentProperty.Name, documentProperty);
				num = 27;
				continue;
			}
			case 9:
				goto IL_1D1;
			case 10:
				if (A_0.MoveToAttribute(ClipboardData.b("ͬ๮ᱰᙲ", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_1A0;
			case 11:
				return;
			case 12:
				if (A_0.LocalName == ClipboardData.b("ᵬᵮṰͲၴն൸ɺ", a_))
				{
					num = 15;
					continue;
				}
				return;
			case 13:
			{
				string text;
				if (text != null)
				{
					num = 8;
					continue;
				}
				goto IL_1D1;
			}
			case 14:
			{
				if (A_0.LocalName != ClipboardData.b("ᵬᵮṰͲၴն൸ɺ", a_))
				{
					num = 20;
					continue;
				}
				A_0.GetAttribute(ClipboardData.b("ͬ๮ᱰᙲ", a_));
				A_0.MoveToElement();
				string text = null;
				num = 16;
				continue;
			}
			case 15:
				goto IL_36F;
			case 16:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 19;
					continue;
				}
				return;
			case 17:
				if (A_0.LocalName == ClipboardData.b("ᵬᵮṰͲၴն൸ɺ", a_))
				{
					num = 5;
					continue;
				}
				goto IL_393;
			case 18:
				goto IL_9E;
			case 19:
				num = 12;
				continue;
			case 20:
				goto IL_134;
			case 21:
				goto IL_1A0;
			case 22:
				num = 13;
				continue;
			case 23:
				if (A_1 == null)
				{
					num = 28;
					continue;
				}
				num = 14;
				continue;
			case 24:
				if (!A_0.EOF)
				{
					num = 7;
					continue;
				}
				return;
			case 25:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 6;
					continue;
				}
				goto IL_393;
			case 26:
				if (A_0.MoveToAttribute(ClipboardData.b("ᵬٮᕰ", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_1D1;
			case 27:
				goto IL_1D1;
			case 28:
				goto IL_19B;
			}
			if (A_0 == null)
			{
				num = 18;
				continue;
			}
			num = 23;
			continue;
			IL_1A0:
			num = 26;
			continue;
			IL_1D1:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_1CC;
			default:
				if (false)
				{
				}
				A_0.Read();
				num = 24;
				continue;
			}
			IL_36F:
			num = 25;
			continue;
			IL_393:
			num = 3;
		}
		IL_9E:
		throw new ArgumentNullException(ClipboardData.b("Ὤ੮ၰᝲၴն", a_));
		IL_134:
		throw new XmlException(ClipboardData.b("㡬Ůᑰ୲մቶེ᩸᡼᭾ꆀﮂꦈﾊ놐", a_) + A_0.LocalName);
		IL_19B:
		throw new ArgumentNullException(ClipboardData.b("๬ᩮɰݲᩴ᩶⥸ॺቼཾ", a_));
	}

	// Token: 0x06001F45 RID: 8005 RVA: 0x00207764 File Offset: 0x00206764
	private string \u1713(string A_0)
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
		A_0 = XmlConvert.DecodeName(A_0);
		return A_0;
	}

	// Token: 0x06001F46 RID: 8006 RVA: 0x002077A8 File Offset: 0x002067A8
	private void \u1714(XmlReader A_0)
	{
		int a_ = 10;
		int num = 11;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 21;
				continue;
			case 1:
				if (spr᧓.ᝢ == null)
				{
					num = 22;
					continue;
				}
				goto IL_3F2;
			case 2:
				goto IL_22B;
			case 3:
				goto IL_22B;
			case 4:
				goto IL_494;
			case 5:
				goto IL_22B;
			case 6:
				goto IL_C1;
			case 7:
				return;
			case 8:
				if (true)
				{
				}
				goto IL_22B;
			case 9:
				if (A_0.NodeType == XmlNodeType.EndElement)
				{
					num = 7;
					continue;
				}
				num = 25;
				continue;
			case 10:
				goto IL_22B;
			case 12:
				num = 20;
				continue;
			case 13:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					goto IL_22B;
				}
				break;
			case 14:
				goto IL_22B;
			case 15:
				goto IL_3F2;
			case 16:
				goto IL_22B;
			case 17:
				goto IL_22B;
			case 18:
			{
				string localName;
				int num2;
				if (spr᧓.ᝢ.TryGetValue(localName, out num2))
				{
					num = 31;
					continue;
				}
				goto IL_1BE;
			}
			case 19:
				goto IL_22B;
			case 20:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 34;
					continue;
				}
				goto IL_1BE;
			}
			case 21:
				goto IL_1BE;
			case 22:
				spr᧓.ᝢ = new Dictionary<string, int>(11)
				{
					{
						ClipboardData.b("፯፱s፵ίᕹ๻ݽ", a_),
						0
					},
					{
						ClipboardData.b("፯qᅳ᝵౷ό᡻", a_),
						1
					},
					{
						ClipboardData.b("፯qᅳ᝵౷ᕹ๻", a_),
						2
					},
					{
						ClipboardData.b("ᑯ᝱ݳᕵ੷፹౻੽", a_),
						3
					},
					{
						ClipboardData.b("᭯᝱൳ŵ᝷ࡹ᡻ൽ", a_),
						4
					},
					{
						ClipboardData.b("ᱯ፱ݳɵ㕷ᕹ᡻᝽쪇", a_),
						5
					},
					{
						ClipboardData.b("ᱯ፱ݳɵ⡷ࡹᕻၽ", a_),
						6
					},
					{
						ClipboardData.b("ᵯᵱၳήṷ፹᥻᩽", a_),
						7
					},
					{
						ClipboardData.b("ͯݱᙳᱵᵷ᥹ࡻ", a_),
						8
					},
					{
						ClipboardData.b("ѯ᭱s᩵ᵷ", a_),
						9
					},
					{
						ClipboardData.b("ɯ᝱ɳή୷፹፻ၽ", a_),
						10
					}
				};
				num = 15;
				continue;
			case 23:
				goto IL_22B;
			case 24:
				if (A_0.LocalName != ClipboardData.b("፯ᵱٳ፵⡷ࡹ፻๽黎", a_))
				{
					num = 36;
					continue;
				}
				num = 33;
				continue;
			case 25:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 12;
					continue;
				}
				A_0.Skip();
				num = 19;
				continue;
			case 26:
				goto IL_142;
			case 27:
				num = 24;
				continue;
			case 28:
			{
				int num2;
				switch (num2)
				{
				case 0:
				{
					BuiltinDocumentProperties builtinDocumentProperties;
					builtinDocumentProperties.Category = this.ᜁ(A_0);
					num = 23;
					continue;
				}
				case 1:
				{
					BuiltinDocumentProperties builtinDocumentProperties;
					builtinDocumentProperties.CreateDate = DateTime.Parse(this.ᜁ(A_0));
					num = 13;
					continue;
				}
				case 2:
				{
					BuiltinDocumentProperties builtinDocumentProperties;
					builtinDocumentProperties.Author = this.ᜁ(A_0);
					num = 8;
					continue;
				}
				case 3:
				{
					BuiltinDocumentProperties builtinDocumentProperties;
					builtinDocumentProperties.Comments = this.ᜁ(A_0);
					num = 29;
					continue;
				}
				case 4:
				{
					BuiltinDocumentProperties builtinDocumentProperties;
					builtinDocumentProperties.Keywords = this.ᜁ(A_0);
					num = 16;
					continue;
				}
				case 5:
				{
					BuiltinDocumentProperties builtinDocumentProperties;
					builtinDocumentProperties.LastAuthor = this.ᜁ(A_0);
					num = 5;
					continue;
				}
				case 6:
				{
					BuiltinDocumentProperties builtinDocumentProperties;
					builtinDocumentProperties.LastPrinted = DateTime.Parse(this.ᜁ(A_0));
					num = 32;
					continue;
				}
				case 7:
				{
					BuiltinDocumentProperties builtinDocumentProperties;
					builtinDocumentProperties.LastSaveDate = DateTime.Parse(this.ᜁ(A_0));
					num = 35;
					continue;
				}
				case 8:
				{
					BuiltinDocumentProperties builtinDocumentProperties;
					builtinDocumentProperties.Subject = this.ᜁ(A_0);
					num = 14;
					continue;
				}
				case 9:
				{
					BuiltinDocumentProperties builtinDocumentProperties;
					builtinDocumentProperties.Title = this.ᜁ(A_0);
					num = 3;
					continue;
				}
				case 10:
				{
					BuiltinDocumentProperties builtinDocumentProperties;
					builtinDocumentProperties.RevisionNumber = this.ᜁ(A_0);
					num = 2;
					continue;
				}
				default:
					num = 0;
					continue;
				}
				break;
			}
			case 29:
				goto IL_22B;
			case 30:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 27;
					continue;
				}
				A_0.Read();
				num = 4;
				continue;
			case 31:
				num = 28;
				continue;
			case 32:
				goto IL_22B;
			case 33:
			{
				if (A_0.IsEmptyElement)
				{
					num = 26;
					continue;
				}
				BuiltinDocumentProperties builtinDocumentProperties = this.ᜄ.BuiltinDocumentProperties;
				A_0.Read();
				num = 17;
				continue;
			}
			case 34:
				num = 1;
				continue;
			case 35:
				goto IL_22B;
			case 36:
				goto IL_19F;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			goto IL_494;
			IL_1BE:
			A_0.Skip();
			num = 10;
			continue;
			IL_22B:
			num = 9;
			continue;
			IL_3F2:
			num = 18;
			continue;
			IL_494:
			num = 30;
		}
		IL_C1:
		throw new ArgumentNullException(ClipboardData.b("ɯ᝱ᕳትᵷࡹ", a_));
		IL_142:
		return;
		IL_19F:
		throw new XmlException(ClipboardData.b("╯ᱱᅳ๵ࡷόύ੽ꒃﺅ겋揄뒓", a_) + A_0.LocalName);
	}

	// Token: 0x06001F47 RID: 8007 RVA: 0x00207D38 File Offset: 0x00206D38
	private void \u1713(XmlReader A_0)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 29;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.LocalName != ClipboardData.b("♵੷ᕹ౻᭽ﮇ", a_))
					{
						num = 28;
						continue;
					}
					num = 4;
					continue;
				case 1:
					goto IL_27A;
				case 2:
					goto IL_27A;
				case 3:
					num = 33;
					continue;
				case 4:
				{
					if (A_0.IsEmptyElement)
					{
						num = 8;
						continue;
					}
					BuiltinDocumentProperties builtinDocumentProperties = this.ᜄ.BuiltinDocumentProperties;
					A_0.Read();
					num = 6;
					continue;
				}
				case 5:
					goto IL_27A;
				case 6:
					goto IL_27A;
				case 7:
					goto IL_D2;
				case 8:
					return;
				case 9:
					num = 32;
					continue;
				case 10:
					goto IL_27A;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4F7;
					default:
						if (false)
						{
						}
						goto IL_27A;
					}
					break;
				case 12:
					goto IL_4F7;
				case 13:
					return;
				case 14:
					goto IL_27A;
				case 15:
					goto IL_27A;
				case 16:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 21;
						continue;
					}
					A_0.Read();
					num = 26;
					continue;
				case 17:
					goto IL_27A;
				case 18:
					goto IL_27A;
				case 19:
				{
					int num2;
					switch (num2)
					{
					case 0:
					{
						BuiltinDocumentProperties builtinDocumentProperties;
						builtinDocumentProperties.Template = this.ᜁ(A_0);
						num = 11;
						continue;
					}
					case 1:
					{
						string s = this.ᜁ(A_0).Replace(ClipboardData.b("婵", a_), string.Empty);
						BuiltinDocumentProperties builtinDocumentProperties;
						builtinDocumentProperties.TotalEditingTime = TimeSpan.FromMinutes(Math.Round((double)XmlConvert.ToSingle(s)));
						num = 25;
						continue;
					}
					case 2:
					{
						string s = this.ᜁ(A_0);
						BuiltinDocumentProperties builtinDocumentProperties;
						builtinDocumentProperties.ᜀ(PIDSI.Pagecount, int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture));
						num = 12;
						continue;
					}
					case 3:
					{
						string s = this.ᜁ(A_0);
						BuiltinDocumentProperties builtinDocumentProperties;
						builtinDocumentProperties.ᜀ(PIDSI.Wordcount, int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture));
						num = 14;
						continue;
					}
					case 4:
					{
						string s = this.ᜁ(A_0);
						BuiltinDocumentProperties builtinDocumentProperties;
						builtinDocumentProperties.ᜀ(PIDSI.Charcount, int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture));
						num = 2;
						continue;
					}
					case 5:
					{
						BuiltinDocumentProperties builtinDocumentProperties;
						builtinDocumentProperties.ApplicationName = this.ᜁ(A_0);
						num = 18;
						continue;
					}
					case 6:
					{
						string s = this.ᜁ(A_0);
						BuiltinDocumentProperties builtinDocumentProperties;
						builtinDocumentProperties.DocSecurity = int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture);
						num = 15;
						continue;
					}
					case 7:
					{
						string s = this.ᜁ(A_0);
						BuiltinDocumentProperties builtinDocumentProperties;
						builtinDocumentProperties.ᜀ(PIDDSI.LineCount, int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture));
						num = 17;
						continue;
					}
					case 8:
					{
						string s = this.ᜁ(A_0);
						BuiltinDocumentProperties builtinDocumentProperties;
						builtinDocumentProperties.ᜀ(PIDDSI.ParCount, int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture));
						num = 22;
						continue;
					}
					case 9:
					{
						BuiltinDocumentProperties builtinDocumentProperties;
						builtinDocumentProperties.Company = this.ᜁ(A_0);
						num = 27;
						continue;
					}
					case 10:
					{
						BuiltinDocumentProperties builtinDocumentProperties;
						builtinDocumentProperties.Manager = this.ᜁ(A_0);
						num = 5;
						continue;
					}
					default:
						num = 23;
						continue;
					}
					break;
				}
				case 20:
				{
					int num2;
					string localName;
					if (spr᧓.ᝣ.TryGetValue(localName, out num2))
					{
						if (true)
						{
						}
						num = 35;
						continue;
					}
					goto IL_1ED;
				}
				case 21:
					num = 0;
					continue;
				case 22:
					goto IL_27A;
				case 23:
					num = 31;
					continue;
				case 24:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 3;
						continue;
					}
					A_0.Skip();
					num = 1;
					continue;
				case 25:
					goto IL_27A;
				case 26:
					goto IL_525;
				case 27:
					goto IL_27A;
				case 28:
					goto IL_1B6;
				case 30:
					spr᧓.ᝣ = new Dictionary<string, int>(11)
					{
						{
							ClipboardData.b("≵ᵷ᝹౻ች", a_),
							0
						},
						{
							ClipboardData.b("≵᝷๹ᵻች푿", a_),
							1
						},
						{
							ClipboardData.b("♵᥷ᵹ᥻ൽ", a_),
							2
						},
						{
							ClipboardData.b("ⅵ᝷ࡹ᡻ൽ", a_),
							3
						},
						{
							ClipboardData.b("㕵ၷ᭹๻ώﮇ", a_),
							4
						},
						{
							ClipboardData.b("㝵ࡷ੹ၻ᝽", a_),
							5
						},
						{
							ClipboardData.b("㉵᝷᥹⽻᭽ﲇ", a_),
							6
						},
						{
							ClipboardData.b("㩵ᅷᑹ᥻ൽ", a_),
							7
						},
						{
							ClipboardData.b("♵᥷ࡹᵻ᥽ﮇ", a_),
							8
						},
						{
							ClipboardData.b("㕵᝷᝹౻ώﮁ", a_),
							9
						},
						{
							ClipboardData.b("㭵᥷ᑹᵻ᥽", a_),
							10
						}
					};
					num = 34;
					continue;
				case 31:
					goto IL_1ED;
				case 32:
					if (spr᧓.ᝣ == null)
					{
						num = 30;
						continue;
					}
					goto IL_468;
				case 33:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 9;
						continue;
					}
					goto IL_1ED;
				}
				case 34:
					goto IL_468;
				case 35:
					num = 19;
					continue;
				case 36:
					if (A_0.NodeType == XmlNodeType.EndElement)
					{
						num = 13;
						continue;
					}
					num = 24;
					continue;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				goto IL_525;
				IL_1ED:
				A_0.Skip();
				num = 10;
				continue;
				IL_27A:
				num = 36;
				continue;
				IL_4F7:
				goto IL_27A;
				IL_468:
				num = 20;
				continue;
				IL_525:
				num = 16;
			}
			IL_D2:
			throw new ArgumentNullException(ClipboardData.b("ѵᵷ᭹᡻᭽", a_));
			IL_1B6:
			throw new XmlException(ClipboardData.b("⍵ᙷόѻ๽ꪉﲏ늑ﾗ몙", a_) + A_0.LocalName);
		}
		}
	}

	// Token: 0x06001F48 RID: 8008 RVA: 0x0020838C File Offset: 0x0020738C
	private void ᜀ(XmlReader A_0, FormatBase A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			ListFormat listFormat;
			string a_3;
			for (;;)
			{
				for (;;)
				{
					ParagraphFormat paragraphFormat = A_1 as ParagraphFormat;
					int num = 22;
					for (;;)
					{
						spr\u1CC1 spr_u1CC;
						switch (num)
						{
						case 0:
						{
							ListStyle listStyle = this.ᜀ(paragraphFormat.OwnerBase as ParagraphStyle);
							num = 1;
							continue;
						}
						case 1:
						{
							ListStyle listStyle;
							if (listStyle != null)
							{
								num = 29;
								continue;
							}
							return;
						}
						case 2:
						{
							sprḍ a_2 = new sprḍ();
							paragraphFormat.Sprms = a_2;
							num = 16;
							continue;
						}
						case 3:
						{
							ListStyle listStyle;
							if (listStyle.Levels.Count <= listFormat.ListLevelNumber)
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
								num = 21;
								continue;
							}
							break;
						}
						case 4:
							listFormat = (paragraphFormat.OwnerBase as Paragraph).ListFormat;
							num = 10;
							continue;
						case 5:
						{
							bool flag;
							if (flag)
							{
								num = 26;
								continue;
							}
							return;
						}
						case 6:
							num = 28;
							continue;
						case 7:
						{
							listFormat = (paragraphFormat.OwnerBase as ParagraphStyle).ListFormat;
							bool flag = true;
							num = 17;
							continue;
						}
						case 8:
							if (paragraphFormat.OwnerBase is ParagraphStyle)
							{
								num = 6;
								continue;
							}
							return;
						case 9:
							if (paragraphFormat.Sprms == null)
							{
								num = 2;
								continue;
							}
							goto IL_463;
						case 10:
							if (true)
							{
							}
							goto IL_3CA;
						case 11:
							return;
						case 12:
							if (listFormat.ListLevelNumber != -1)
							{
								num = 14;
								continue;
							}
							goto IL_43E;
						case 13:
						{
							Dictionary<string, string>.Enumerator enumerator = this.ᜈ().GetEnumerator();
							num = 19;
							continue;
						}
						case 14:
							spr_u1CC.ᜁ(listFormat.ListLevelNumber);
							num = 23;
							continue;
						case 15:
							goto IL_3CA;
						case 16:
							goto IL_463;
						case 17:
							goto IL_3CA;
						case 18:
							listFormat = (paragraphFormat.OwnerBase as spr\u173A).ᜉ();
							num = 15;
							continue;
						case 19:
							try
							{
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 1:
									{
										KeyValuePair<string, string> keyValuePair;
										a_3 = keyValuePair.Key;
										num = 5;
										continue;
									}
									case 2:
									{
										KeyValuePair<string, string> keyValuePair;
										if (keyValuePair.Value == (paragraphFormat.OwnerBase as ParagraphStyle).Name)
										{
											num = 1;
											continue;
										}
										break;
									}
									case 3:
										goto IL_20F;
									case 4:
										goto IL_21B;
									case 5:
										goto IL_20F;
									case 6:
									{
										Dictionary<string, string>.Enumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num = 3;
											continue;
										}
										KeyValuePair<string, string> keyValuePair = enumerator.Current;
										num = 2;
										continue;
									}
									}
									IL_1DB:
									num = 6;
									continue;
									goto IL_1DB;
									IL_20F:
									num = 4;
								}
								IL_21B:
								goto IL_430;
							}
							finally
							{
								Dictionary<string, string>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
							goto IL_22E;
						case 20:
							if (paragraphFormat.OwnerBase is ParagraphStyle)
							{
								num = 7;
								continue;
							}
							num = 24;
							continue;
						case 21:
						{
							a_3 = (paragraphFormat.OwnerBase as ParagraphStyle).Name.Replace(ClipboardData.b("噵", a_), string.Empty);
							ListStyle listStyle;
							listStyle.Levels[listFormat.ListLevelNumber].ParaStyleName = a_3;
							num = 30;
							continue;
						}
						case 22:
						{
							if (paragraphFormat.OwnerBase == null)
							{
								num = 11;
								continue;
							}
							bool flag = false;
							listFormat = null;
							num = 25;
							continue;
						}
						case 23:
							goto IL_43E;
						case 24:
							if (paragraphFormat.OwnerBase is spr\u173A)
							{
								num = 18;
								continue;
							}
							goto IL_3CA;
						case 25:
							if (paragraphFormat.OwnerBase is Paragraph)
							{
								num = 4;
								continue;
							}
							num = 20;
							continue;
						case 26:
							a_3 = null;
							num = 27;
							continue;
						case 27:
							if (listFormat.CurrentListLevel != null)
							{
								num = 13;
								continue;
							}
							num = 8;
							continue;
						case 28:
							if (listFormat.ListLevelNumber > 0)
							{
								num = 0;
								continue;
							}
							return;
						case 29:
							goto IL_22E;
						case 30:
							goto IL_35B;
						}
						break;
						IL_22E:
						num = 3;
						continue;
						IL_3CA:
						this.ᜀ(A_0, listFormat);
						spr_u1CC = new spr\u1CC1(9738);
						num = 12;
						continue;
						IL_43E:
						num = 9;
						continue;
						IL_463:
						paragraphFormat.Sprms.ᜆ(spr_u1CC);
						num = 5;
					}
				}
			}
			return;
			IL_35B:
			return;
			IL_430:
			listFormat.CurrentListLevel.ParaStyleName = a_3;
			return;
		}
		}
	}

	// Token: 0x06001F49 RID: 8009 RVA: 0x0020888C File Offset: 0x0020788C
	private ListStyle ᜀ(ParagraphStyle A_0)
	{
		ParagraphStyle baseStyle;
		for (;;)
		{
			baseStyle = A_0.BaseStyle;
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
						goto IL_7B;
					case 1:
						if (baseStyle == null)
						{
							num = 5;
							continue;
						}
						num = 6;
						continue;
					case 2:
						goto IL_C3;
					case 3:
						if (baseStyle == null)
						{
							num = 4;
							continue;
						}
						goto IL_7B;
					case 4:
						goto IL_5A;
					case 5:
						goto IL_9E;
					case 6:
						if (baseStyle.ListFormat.CurrentListStyle != null)
						{
							num = 2;
							continue;
						}
						baseStyle = baseStyle.BaseStyle;
						num = 0;
						continue;
					}
					break;
					IL_7B:
					num = 1;
				}
				break;
			}
			}
		}
		IL_5A:
		return null;
		IL_9E:
		if (true)
		{
		}
		return null;
		IL_C3:
		return baseStyle.ListFormat.CurrentListStyle;
	}

	// Token: 0x06001F4A RID: 8010 RVA: 0x00208968 File Offset: 0x00207968
	private void ᜀ(XmlReader A_0, ListFormat A_1)
	{
		int a_ = 12;
		switch (0)
		{
		default:
			for (;;)
			{
				string localName = A_0.LocalName;
				A_0.Read();
				int num = 28;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_242;
					case 1:
					{
						string attribute;
						if (this.ᜎ().ContainsKey(attribute))
						{
							num = 8;
							continue;
						}
						goto IL_242;
					}
					case 2:
						A_1.IsEmptyList = true;
						num = 24;
						continue;
					case 3:
						num2 = 8;
						num = 26;
						continue;
					case 4:
					{
						string localName2;
						if ((localName2 = A_0.LocalName) != null)
						{
							num = 30;
							continue;
						}
						goto IL_242;
					}
					case 5:
						num = 21;
						continue;
					case 6:
						if (!(A_0.LocalName != localName))
						{
							if (true)
							{
							}
							num = 20;
							continue;
						}
						num = 4;
						continue;
					case 7:
						num = 0;
						continue;
					case 8:
					{
						string attribute;
						string text = this.ᜎ()[attribute];
						ListStyle listStyle = this.ᜄ.ListStyles.FindByName(text);
						num = 19;
						continue;
					}
					case 9:
					{
						ListStyle listStyle;
						if (listStyle.BaseListStyleName != null)
						{
							num = 5;
							continue;
						}
						goto IL_3E8;
					}
					case 10:
					{
						string text;
						if (text != null)
						{
							num = 29;
							continue;
						}
						goto IL_190;
					}
					case 11:
						num = 9;
						continue;
					case 12:
						goto IL_3E8;
					case 13:
						goto IL_242;
					case 14:
					{
						string localName2;
						if (!(localName2 == ClipboardData.b("ᱱų᭵ㅷṹ", a_)))
						{
							num = 7;
							continue;
						}
						string attribute = A_0.GetAttribute(ClipboardData.b("ѱᕳ᩵", a_), ClipboardData.b("ᩱsɵࡷ䁹卻兽ﾋꂍﾏ쾟킡즣장\udca7\ud9a9芫솭슯햱鮳솵ힷ좹\ud8bb캽늿귁ꟃꏅ믇막ꗋꃍ럏뿑룓崙쿟迡藣迥蛧", a_));
						num = 36;
						continue;
					}
					case 15:
						goto IL_242;
					case 16:
					{
						string key;
						if (this.ᜎ().ContainsKey(key))
						{
							num = 23;
							continue;
						}
						goto IL_3E8;
					}
					case 17:
						num = 32;
						continue;
					case 18:
					{
						string localName2;
						if (!(localName2 == ClipboardData.b("᭱ᡳuᑷ", a_)))
						{
							num = 34;
							continue;
						}
						num2 = int.Parse(A_0.GetAttribute(ClipboardData.b("ѱᕳ᩵", a_), ClipboardData.b("ᩱsɵࡷ䁹卻兽ﾋꂍﾏ쾟킡즣장\udca7\ud9a9芫솭슯햱鮳솵ힷ좹\ud8bb캽늿귁ꟃꏅ믇막ꗋꃍ럏뿑룓崙쿟迡藣迥蛧", a_)));
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3E8;
						default:
							if (false)
							{
							}
							num = 31;
							continue;
						}
						break;
					}
					case 19:
						if (this.\u1714 != null)
						{
							num = 11;
							continue;
						}
						goto IL_3E8;
					case 20:
						return;
					case 21:
					{
						ListStyle listStyle;
						if (listStyle.BaseListStyleName != string.Empty)
						{
							num = 17;
							continue;
						}
						goto IL_3E8;
					}
					case 22:
					{
						ListStyle listStyle;
						string key = this.\u1714[listStyle.BaseListStyleName];
						num = 16;
						continue;
					}
					case 23:
					{
						string key;
						string text = this.ᜎ()[key];
						num = 12;
						continue;
					}
					case 24:
						goto IL_242;
					case 25:
						goto IL_190;
					case 26:
						goto IL_362;
					case 27:
						goto IL_2D8;
					case 28:
						goto IL_2D8;
					case 29:
					{
						string text;
						A_1.ApplyStyle(text);
						num = 25;
						continue;
					}
					case 30:
						num = 18;
						continue;
					case 31:
						if (num2 > 8)
						{
							num = 3;
							continue;
						}
						goto IL_362;
					case 32:
					{
						ListStyle listStyle;
						if (this.\u1714.ContainsKey(listStyle.BaseListStyleName))
						{
							num = 22;
							continue;
						}
						goto IL_3E8;
					}
					case 33:
					{
						string attribute;
						A_1.LFOStyleName = this.ᜏ()[attribute];
						num = 13;
						continue;
					}
					case 34:
						num = 14;
						continue;
					case 35:
					{
						string attribute;
						if (this.ᜏ().ContainsKey(attribute))
						{
							num = 33;
							continue;
						}
						goto IL_242;
					}
					case 36:
					{
						string attribute;
						if (attribute == ClipboardData.b("䉱", a_))
						{
							num = 2;
							continue;
						}
						num = 1;
						continue;
					}
					}
					break;
					IL_190:
					num = 35;
					continue;
					IL_242:
					A_0.Read();
					num = 27;
					continue;
					IL_2D8:
					num = 6;
					continue;
					IL_362:
					A_1.ListLevelNumber = num2;
					num = 15;
					continue;
					IL_3E8:
					num = 10;
				}
			}
			return;
		}
	}

	// Token: 0x06001F4B RID: 8011 RVA: 0x00208E3C File Offset: 0x00207E3C
	private void \u1712(XmlReader A_0)
	{
		int a_ = 5;
		int num = 21;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_11E;
			case 1:
				goto IL_11E;
			case 2:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 25;
					continue;
				}
				A_0.Read();
				num = 5;
				continue;
			case 3:
				if (A_0.IsEmptyElement)
				{
					num = 20;
					continue;
				}
				goto IL_DF;
			case 4:
			{
				if (A_0.EOF)
				{
					num = 30;
					continue;
				}
				bool flag = false;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_333;
				default:
					if (false)
					{
					}
					num = 22;
					continue;
				}
				break;
			}
			case 5:
				goto IL_DF;
			case 6:
				goto IL_1E2;
			case 7:
			{
				bool flag;
				if (!flag)
				{
					num = 12;
					continue;
				}
				goto IL_106;
			}
			case 8:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 26;
					continue;
				}
				goto IL_11E;
			}
			case 9:
				goto IL_B9;
			case 10:
			{
				string localName;
				if (!(localName == ClipboardData.b("ժᡬɮⅰᩲᙴ㕶౸᝺ᅼ᩾", a_)))
				{
					goto IL_333;
				}
				this.ᜎ(this.ᜑ(A_0));
				num = 28;
				continue;
			}
			case 11:
			{
				string localName;
				if (!(localName == ClipboardData.b("੪ཬᱮհŲᑴᑶ൸㕺ࡼቾ", a_)))
				{
					num = 18;
					continue;
				}
				this.ᜏ(A_0);
				num = 1;
				continue;
			}
			case 12:
				A_0.Read();
				num = 27;
				continue;
			case 13:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 34;
					continue;
				}
				return;
			case 14:
				goto IL_1DD;
			case 15:
				num = 8;
				continue;
			case 16:
				goto IL_106;
			case 17:
				num = 0;
				continue;
			case 18:
				num = 33;
				continue;
			case 19:
				num = 4;
				continue;
			case 20:
				return;
			case 22:
				if (true)
				{
				}
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 15;
					continue;
				}
				A_0.Read();
				num = 16;
				continue;
			case 23:
				goto IL_1E2;
			case 24:
				if (A_0.LocalName != ClipboardData.b("ժᡬɮ፰ᙲݴṶ᝸ᱺ", a_))
				{
					num = 19;
					continue;
				}
				return;
			case 25:
				num = 29;
				continue;
			case 26:
				num = 10;
				continue;
			case 27:
				goto IL_106;
			case 28:
				goto IL_11E;
			case 29:
			{
				if (A_0.LocalName != ClipboardData.b("ժᡬɮ፰ᙲݴṶ᝸ᱺ", a_))
				{
					num = 14;
					continue;
				}
				bool flag = false;
				A_0.Read();
				this.ᜀ(A_0);
				num = 23;
				continue;
			}
			case 30:
				goto IL_22B;
			case 31:
				goto IL_11E;
			case 32:
				num = 11;
				continue;
			case 33:
			{
				string localName;
				if (!(localName == ClipboardData.b("ժᡬɮ", a_)))
				{
					num = 17;
					continue;
				}
				this.ᜐ(A_0);
				num = 31;
				continue;
			}
			case 34:
				num = 24;
				continue;
			}
			if (A_0 == null)
			{
				num = 9;
				continue;
			}
			num = 3;
			continue;
			IL_DF:
			num = 2;
			continue;
			IL_106:
			this.ᜀ(A_0);
			num = 6;
			continue;
			IL_11E:
			num = 7;
			continue;
			IL_1E2:
			num = 13;
			continue;
			IL_333:
			num = 32;
		}
		IL_B9:
		throw new ArgumentNullException(ClipboardData.b("ᥪ࡬๮ᕰᙲݴ", a_));
		IL_1DD:
		throw new XmlException(ClipboardData.b("㹪ͬ੮॰Ͳၴᑶ൸Ṻ᥼彾呂Ꞇﶈ꾎", a_) + A_0.LocalName);
		IL_22B:;
	}

	// Token: 0x06001F4C RID: 8012 RVA: 0x00209248 File Offset: 0x00208248
	private XmlReader ᜑ(XmlReader A_0)
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
		MemoryStream memoryStream = this.ᜢ(A_0);
		memoryStream.Position = 0L;
		return spr\u23D7.ᜀ(memoryStream);
	}

	// Token: 0x06001F4D RID: 8013 RVA: 0x0020929C File Offset: 0x0020829C
	private void ᜐ(XmlReader A_0)
	{
		int a_ = 4;
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			int num = 23;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_18D;
				case 1:
					goto IL_31A;
				case 2:
					goto IL_18D;
				case 3:
					goto IL_236;
				case 4:
					num = 6;
					continue;
				case 5:
				{
					string attribute;
					if (this.\u170D().ContainsKey(attribute))
					{
						num = 24;
						continue;
					}
					goto IL_31A;
				}
				case 6:
				{
					if (!(A_0.LocalName != ClipboardData.b("୩๫ᵭѯqᕳᕵ౷㑹ॻ፽", a_)))
					{
						num = 3;
						continue;
					}
					bool flag = false;
					num = 17;
					continue;
				}
				case 7:
					return;
				case 8:
				{
					string localName;
					if (!(localName == ClipboardData.b("٩ᩫɭ㽯ѱᅳѵ੷፹᡻᭽", a_)))
					{
						num = 10;
						continue;
					}
					string attribute2;
					this.ᜃ(A_0, attribute2);
					num = 1;
					continue;
				}
				case 9:
					A_0.Read();
					num = 2;
					continue;
				case 10:
					num = 22;
					continue;
				case 11:
					num = 26;
					continue;
				case 12:
					goto IL_18D;
				case 13:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 11;
						continue;
					}
					goto IL_31A;
				}
				case 14:
				{
					bool flag;
					if (!flag)
					{
						num = 9;
						continue;
					}
					goto IL_18D;
				}
				case 15:
					goto IL_31A;
				case 16:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						goto IL_1A6;
					}
					return;
				case 17:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 19;
						continue;
					}
					A_0.Read();
					num = 0;
					continue;
				case 18:
				{
					if (A_0.IsEmptyElement)
					{
						num = 7;
						continue;
					}
					bool flag = false;
					string attribute2 = A_0.GetAttribute(ClipboardData.b("ѩᥫͭ㥯ᙱ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫\ud9ad\udfaf삱킳욵쪷햹\udfbb\udbbd뎿뇁귃ꣅ꿇Ꟊꃋ럙뷛럝軟", a_));
					A_0.Read();
					this.ᜀ(A_0);
					num = 12;
					continue;
				}
				case 19:
					num = 13;
					continue;
				case 20:
					goto IL_D9;
				case 21:
					if (A_0.LocalName != ClipboardData.b("ѩᥫͭ", a_))
					{
						num = 25;
						continue;
					}
					num = 18;
					continue;
				case 22:
					goto IL_31A;
				case 24:
				{
					string attribute;
					string attribute2;
					this.ᜎ().Add(attribute2, this.\u170D()[attribute]);
					num = 15;
					continue;
				}
				case 25:
					goto IL_2F1;
				case 26:
				{
					string localName;
					if (!(localName == ClipboardData.b("୩๫ᵭѯqᕳᕵ౷㑹ॻ፽쥿", a_)))
					{
						num = 27;
						continue;
					}
					string attribute = A_0.GetAttribute(ClipboardData.b("ᱩ൫ɭ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫\ud9ad\udfaf삱킳욵쪷햹\udfbb\udbbd뎿뇁귃ꣅ꿇Ꟊꃋ럙뷛럝軟", a_));
					num = 5;
					continue;
				}
				case 27:
					num = 8;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_1A6:
					num = 4;
					continue;
				default:
					if (false)
					{
					}
					if (A_0 == null)
					{
						num = 20;
						continue;
					}
					num = 21;
					continue;
				}
				IL_18D:
				num = 16;
				continue;
				IL_31A:
				num = 14;
			}
			IL_D9:
			throw new ArgumentException(ClipboardData.b("ᡩ५཭ᑯ᝱ٳ", a_));
			IL_236:
			return;
			IL_2F1:
			throw new XmlException(ClipboardData.b("㽩ɫ୭࡯ɱᅳᕵ౷ό᡻幽ꚅﲇ꺍", a_) + A_0.LocalName);
		}
		}
	}

	// Token: 0x06001F4E RID: 8014 RVA: 0x00209668 File Offset: 0x00208668
	private void ᜃ(XmlReader A_0, string A_1)
	{
		int a_ = 9;
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			int num = 13;
			for (;;)
			{
				OverrideLevelFormat a_2;
				string attribute;
				switch (num)
				{
				case 0:
					goto IL_172;
				case 1:
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9B;
					}
					if (false)
					{
					}
					spr\u177D spr_u177D = new spr\u177D(this.ᜄ);
					spr_u177D.Name = ClipboardData.b("⍮ᝰᱲ♴Ͷx᝺᡼⁾", a_) + Guid.NewGuid();
					this.ᜏ().Add(A_1, spr_u177D.Name);
					this.ᜄ.ListOverrides.ᜀ(spr_u177D);
					num = 7;
					continue;
				}
				case 2:
					return;
				case 3:
				{
					if (A_0.IsEmptyElement)
					{
						num = 2;
						continue;
					}
					spr\u177D spr_u177D = null;
					num = 9;
					continue;
				}
				case 4:
					this.ᜀ(A_0, a_2);
					num = 10;
					continue;
				case 5:
					if (attribute != null)
					{
						num = 14;
						continue;
					}
					return;
				case 6:
					goto IL_10C;
				case 7:
					goto IL_172;
				case 8:
					if (A_0.LocalName != ClipboardData.b("ͮݰὲ㩴Ŷᱸॺོᙾ", a_))
					{
						num = 6;
						continue;
					}
					num = 3;
					continue;
				case 9:
				{
					if (!this.ᜏ().ContainsKey(A_1))
					{
						num = 1;
						continue;
					}
					spr\u177D spr_u177D = this.ᜄ.ListOverrides.ᜀ(this.ᜏ()[A_1]);
					goto IL_9B;
				}
				case 10:
					goto IL_1D8;
				case 11:
					goto IL_79;
				case 12:
					if (A_0.LocalName == ClipboardData.b("ͮݰὲ㩴Ŷᱸॺོᙾ", a_))
					{
						num = 4;
						continue;
					}
					return;
				case 14:
				{
					int a_3 = int.Parse(attribute);
					spr\u177D spr_u177D;
					spr_u177D.ᜃ().ᜀ(a_3, a_2);
					num = 12;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				num = 8;
				continue;
				IL_9B:
				num = 0;
				continue;
				IL_172:
				attribute = A_0.GetAttribute(ClipboardData.b("ٮᵰղᥴ", a_), ClipboardData.b("ݮհݲմ䵶噸呺๼᱾愈ꖊﾎﶒ殺ﶚ철슢톤풦螨쒪\udfac좮麰쒲\udab4얶\uddb8쮺쾼킾ꋀꛂ뛄듆ꃈꗊ꫌ꋎ뷐ﳒ닞胠諢诤", a_));
				a_2 = new OverrideLevelFormat(this.ᜄ);
				num = 5;
			}
			IL_79:
			throw new ArgumentException(ClipboardData.b("ᵮᑰቲᅴቶ୸", a_));
			IL_10C:
			throw new XmlException(ClipboardData.b("㩮ὰᙲ൴ݶᱸ᡺ॼ᩾ꎂﶄꮊ歷뎒", a_) + A_0.LocalName);
			IL_1D8:
			return;
		}
		}
	}

	// Token: 0x06001F4F RID: 8015 RVA: 0x00209924 File Offset: 0x00208924
	private void ᜀ(XmlReader A_0, OverrideLevelFormat A_1)
	{
		int a_ = 5;
		for (;;)
		{
			bool flag = false;
			A_0.Read();
			this.ᜀ(A_0);
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 16;
					continue;
				case 1:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 7;
						continue;
					}
					return;
				case 2:
					if (!(A_0.LocalName != ClipboardData.b("ݪ᭬ͮ㹰ղၴն୸ቺ᥼᩾", a_)))
					{
						num = 3;
						continue;
					}
					flag = false;
					num = 6;
					continue;
				case 3:
					return;
				case 4:
					num = 10;
					continue;
				case 5:
				{
					string attribute;
					if (attribute != null)
					{
						num = 14;
						continue;
					}
					goto IL_2B8;
				}
				case 6:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 4;
						continue;
					}
					A_0.Read();
					num = 8;
					continue;
				case 7:
					num = 2;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_22F;
					default:
						if (false)
						{
						}
						goto IL_F7;
					}
					break;
				case 9:
					goto IL_1D4;
				case 10:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 0;
						continue;
					}
					goto IL_130;
				}
				case 11:
					if (!flag)
					{
						num = 23;
						continue;
					}
					goto IL_F7;
				case 12:
					goto IL_130;
				case 13:
					goto IL_130;
				case 14:
				{
					string attribute;
					A_1.StartAt = int.Parse(attribute);
					num = 17;
					continue;
				}
				case 15:
					goto IL_130;
				case 16:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᡪᥬ๮Ͱݲ㩴Ŷᱸॺོᙾ", a_)))
					{
						num = 20;
						continue;
					}
					string attribute = A_0.GetAttribute(ClipboardData.b("ᵪ౬ͮ", a_), ClipboardData.b("ͪᥬ᭮Ű䥲婴塶੸᡺ᕼ᩾ꦆﮊﺒ璉ﺞ햠킢认좦\udba8첪芬\ud8ae\udeb0솲톴잶쮸풺\udebc\udabe닀냂계꧆껈ꛊꇌ뛚볜뛞迠", a_));
					if (true)
					{
					}
					num = 5;
					continue;
				}
				case 17:
					goto IL_2B8;
				case 18:
					goto IL_1D4;
				case 19:
					goto IL_F7;
				case 20:
					num = 22;
					continue;
				case 21:
					goto IL_22F;
				case 22:
				{
					string localName;
					if (!(localName == ClipboardData.b("ݪ᭬ͮ", a_)))
					{
						num = 21;
						continue;
					}
					A_1.OverrideFormatting = true;
					this.ᜁ(A_0, A_1.OverrideListLevel);
					num = 15;
					continue;
				}
				case 23:
					A_0.Read();
					num = 19;
					continue;
				}
				break;
				IL_F7:
				this.ᜀ(A_0);
				num = 18;
				continue;
				IL_130:
				num = 11;
				continue;
				IL_1D4:
				num = 1;
				continue;
				IL_22F:
				num = 12;
				continue;
				IL_2B8:
				A_1.OverrideStartAtValue = true;
				num = 13;
			}
		}
	}

	// Token: 0x06001F50 RID: 8016 RVA: 0x00209C00 File Offset: 0x00208C00
	private void ᜏ(XmlReader A_0)
	{
		int a_ = 9;
		if (true)
		{
		}
		int num = 8;
		string attribute;
		ListStyle listStyle;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 7;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_58;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 2:
				goto IL_BA;
			case 3:
				goto IL_164;
			case 4:
				goto IL_169;
			case 5:
				if (A_0.LocalName != ClipboardData.b("๮፰rŴնᡸ᡺ॼㅾ", a_))
				{
					num = 3;
					continue;
				}
				goto IL_58;
			case 6:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 1;
					continue;
				}
				A_0.Read();
				num = 4;
				continue;
			case 7:
				goto IL_10F;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			goto IL_169;
			IL_58:
			attribute = A_0.GetAttribute(ClipboardData.b("๮፰rŴնᡸ᡺ॼㅾ첄", a_), ClipboardData.b("ݮհݲմ䵶噸呺๼᱾愈ꖊﾎﶒ殺ﶚ철슢톤풦螨쒪\udfac좮麰쒲\udab4얶\uddb8쮺쾼킾ꋀꛂ뛄듆ꃈꗊ꫌ꋎ뷐ﳒ닞胠諢诤", a_));
			listStyle = new ListStyle(this.ᜄ);
			this.ᜀ(A_0, listStyle);
			this.ᜄ.ListStyles.Add(listStyle);
			this.ᜁ(listStyle);
			this.ᜀ(listStyle);
			num = 2;
			continue;
			IL_169:
			num = 6;
		}
		IL_BA:
		listStyle.IsSimple = (listStyle.Levels.Count == 1);
		this.\u170D().Add(attribute, listStyle.Name);
		return;
		IL_10F:
		throw new ArgumentNullException(ClipboardData.b("ᵮᑰቲᅴቶ୸", a_));
		IL_164:
		throw new XmlException(ClipboardData.b("㩮ὰᙲ൴ݶᱸ᡺ॼ᩾ꎂﶄꮊ歷뎒", a_) + A_0.LocalName);
	}

	// Token: 0x06001F51 RID: 8017 RVA: 0x00209DD0 File Offset: 0x00208DD0
	private void ᜀ(XmlReader A_0, ListStyle A_1)
	{
		int a_ = 16;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 6;
				continue;
			case 1:
				num = 29;
				continue;
			case 2:
				goto IL_1AE;
			case 3:
				A_1.IsHybrid = true;
				num = 25;
				continue;
			case 4:
			{
				bool flag;
				if (!flag)
				{
					num = 9;
					continue;
				}
				goto IL_215;
			}
			case 5:
				goto IL_41A;
			case 6:
				goto IL_453;
			case 7:
			{
				string localName;
				if (!(localName == ClipboardData.b("᩵๷ᙹ", a_)))
				{
					num = 0;
					continue;
				}
				ListLevel listLevel = new ListLevel(A_1);
				A_1.Levels.ᜁ(listLevel);
				this.ᜁ(A_0, listLevel);
				num = 33;
				continue;
			}
			case 9:
				A_0.Read();
				num = 15;
				continue;
			case 10:
				num = 7;
				continue;
			case 11:
				goto IL_215;
			case 12:
			{
				if (!(A_0.LocalName != ClipboardData.b("᝵᩷ॹࡻ౽종ﶇ", a_)))
				{
					num = 30;
					continue;
				}
				bool flag = false;
				num = 13;
				continue;
			}
			case 13:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 26;
					continue;
				}
				A_0.Read();
				num = 11;
				continue;
			case 14:
				goto IL_453;
			case 15:
				goto IL_215;
			case 16:
				if (A_0.LocalName != ClipboardData.b("᝵᩷ॹࡻ౽종ﶇ", a_))
				{
					num = 5;
					continue;
				}
				num = 22;
				continue;
			case 17:
				num = 18;
				continue;
			case 18:
			{
				string localName;
				if (!(localName == ClipboardData.b("յ౷͹ၻ᭽챿", a_)))
				{
					num = 10;
					continue;
				}
				A_1.StyleLink = A_0.GetAttribute(ClipboardData.b("u᥷ᙹ", a_), ClipboardData.b("ṵ౷๹౻䑽꽿궁벑ﮓﶗ첟쒡쮣풥얧쮩\ud8ab\uddad麯\uddb1욳통鞷춹펻첽꒿닁뛃꧅ꯇ꿉뿋뷍맏병돓믕듗탟퓡쯣该觧菩苫", a_));
				num = 14;
				continue;
			}
			case 19:
				goto IL_1AE;
			case 20:
				IL_1D9:
				num = 12;
				continue;
			case 21:
			{
				string localName;
				if (!(localName == ClipboardData.b("᭵൷ᙹࡻ᝽챿\ude89ﺍ", a_)))
				{
					num = 1;
					continue;
				}
				num = 28;
				continue;
			}
			case 22:
			{
				if (A_0.IsEmptyElement)
				{
					num = 32;
					continue;
				}
				bool flag = false;
				A_0.Read();
				this.ᜀ(A_0);
				num = 2;
				continue;
			}
			case 23:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 20;
					continue;
				}
				return;
			case 24:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 34;
					continue;
				}
				goto IL_453;
			}
			case 25:
				goto IL_453;
			case 26:
				num = 24;
				continue;
			case 27:
				goto IL_453;
			case 28:
				if (A_0.GetAttribute(ClipboardData.b("u᥷ᙹ", a_), ClipboardData.b("ṵ౷๹౻䑽꽿궁벑ﮓﶗ첟쒡쮣풥얧쮩\ud8ab\uddad麯\uddb1욳통鞷춹펻첽꒿닁뛃꧅ꯇ꿉뿋뷍맏병돓믕듗탟퓡쯣该觧菩苫", a_)) == ClipboardData.b("ṵŷ᡹๻᝽쾁ﲇ", a_))
				{
					num = 3;
					continue;
				}
				goto IL_453;
			case 29:
			{
				string localName;
				if (!(localName == ClipboardData.b("ᡵ൷᝹⽻੽勵쪅", a_)))
				{
					num = 17;
					continue;
				}
				A_1.BaseListStyleName = A_0.GetAttribute(ClipboardData.b("u᥷ᙹ", a_), ClipboardData.b("ṵ౷๹౻䑽꽿궁벑ﮓﶗ첟쒡쮣풥얧쮩\ud8ab\uddad麯\uddb1욳통鞷춹펻첽꒿닁뛃꧅ꯇ꿉뿋뷍맏병돓믕듗탟퓡쯣该觧菩苫", a_));
				num = 27;
				continue;
			}
			case 30:
				goto IL_25D;
			case 31:
				goto IL_B9;
			case 32:
				return;
			case 33:
				goto IL_453;
			case 34:
				num = 21;
				continue;
			}
			if (A_1 == null)
			{
				num = 31;
				continue;
			}
			num = 16;
			continue;
			IL_1AE:
			if (true)
			{
			}
			num = 23;
			continue;
			IL_453:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_1D9;
			default:
				if (false)
				{
				}
				num = 4;
				continue;
			}
			IL_215:
			this.ᜀ(A_0);
			num = 19;
		}
		IL_B9:
		throw new ArgumentException(ClipboardData.b("᩵ᅷॹࡻ幽ﶃ", a_));
		IL_25D:
		return;
		IL_41A:
		throw new XmlException(ClipboardData.b("⍵ᙷόѻ๽ꪉﲏ늑ﾗ몙", a_) + A_0.LocalName);
	}

	// Token: 0x06001F52 RID: 8018 RVA: 0x0020A270 File Offset: 0x00209270
	private void ᜁ(XmlReader A_0, ListLevel A_1)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 16;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_17E;
				case 1:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 24;
						continue;
					}
					A_0.Read();
					num = 8;
					continue;
				case 2:
					goto IL_373;
				case 3:
					goto IL_17E;
				case 4:
				{
					if (!(A_0.LocalName != ClipboardData.b("ɭٯṱ", a_)))
					{
						num = 15;
						continue;
					}
					bool flag = false;
					num = 1;
					continue;
				}
				case 5:
					A_1.NoRestartByHigher = true;
					num = 0;
					continue;
				case 6:
				{
					string attribute;
					if (this.ᜈ().ContainsKey(attribute))
					{
						num = 34;
						continue;
					}
					goto IL_17E;
				}
				case 7:
					goto IL_F6;
				case 8:
					goto IL_1DE;
				case 9:
					goto IL_17E;
				case 10:
				{
					int num2;
					switch (num2)
					{
					case 0:
						A_1.StartAt = int.Parse(A_0.GetAttribute(ClipboardData.b("ᡭᅯṱ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_)));
						num = 38;
						continue;
					case 1:
						this.ᜋ(A_0, A_1.ParagraphFormat);
						num = 19;
						continue;
					case 2:
						this.ᜋ(A_0, A_1.CharacterFormat);
						num = 9;
						continue;
					case 3:
						A_1.IsLegalStyleNumbering = true;
						num = 43;
						continue;
					case 4:
					{
						string attribute2 = A_0.GetAttribute(ClipboardData.b("ᡭᅯṱ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_));
						num = 12;
						continue;
					}
					case 5:
					{
						string attribute = A_0.GetAttribute(ClipboardData.b("ᡭᅯṱ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_));
						num = 6;
						continue;
					}
					case 6:
					{
						string attribute3 = A_0.GetAttribute(ClipboardData.b("ᡭᅯṱ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_));
						num = 21;
						continue;
					}
					case 7:
					{
						string attribute4 = A_0.GetAttribute(ClipboardData.b("ᡭᅯṱ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_));
						A_1.PatternType = this.ᜏ(attribute4);
						num = 42;
						continue;
					}
					case 8:
					{
						string attribute5 = A_0.GetAttribute(ClipboardData.b("ᡭᅯṱ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_));
						this.ᜀ(A_1, attribute5);
						num = 28;
						continue;
					}
					case 9:
					{
						string attribute6 = A_0.GetAttribute(ClipboardData.b("ᡭᅯṱ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_));
						A_1.NumberAlignment = this.ᜑ(attribute6);
						num = 17;
						continue;
					}
					case 10:
					{
						string attribute7 = A_0.GetAttribute(ClipboardData.b("ᡭᅯṱ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_));
						A_1.FollowCharacter = this.\u1712(attribute7);
						num = 3;
						continue;
					}
					case 11:
						this.ᜀ(A_0, A_1);
						num = 45;
						continue;
					default:
						num = 11;
						continue;
					}
					break;
				}
				case 11:
					num = 33;
					continue;
				case 12:
				{
					string attribute2;
					if (attribute2 == ClipboardData.b("幭", a_))
					{
						num = 5;
						continue;
					}
					goto IL_17E;
				}
				case 13:
					goto IL_17E;
				case 14:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 27;
						continue;
					}
					return;
				case 15:
					goto IL_2C6;
				case 17:
					goto IL_17E;
				case 18:
					return;
				case 19:
					goto IL_17E;
				case 20:
					spr᧓.ᝤ = new Dictionary<string, int>(12)
					{
						{
							ClipboardData.b("ᵭѯ፱ٳɵ", a_),
							0
						},
						{
							ClipboardData.b("ṭ⁯q", a_),
							1
						},
						{
							ClipboardData.b("ᱭ⁯q", a_),
							2
						},
						{
							ClipboardData.b("ݭͯ㹱፳᩵", a_),
							3
						},
						{
							ClipboardData.b("ɭٯṱ♳፵୷๹ᵻ౽", a_),
							4
						},
						{
							ClipboardData.b("ṭ⍯ٱ൳᩵ᵷ", a_),
							5
						},
						{
							ClipboardData.b("ɭٯṱ⑳ή᭷㡹ॻች쾅", a_),
							6
						},
						{
							ClipboardData.b("mկά㉳᭵౷", a_),
							7
						},
						{
							ClipboardData.b("ɭٯṱ⁳፵w๹", a_),
							8
						},
						{
							ClipboardData.b("ɭٯṱ㹳ᕵ", a_),
							9
						},
						{
							ClipboardData.b("ᵭկᑱታ", a_),
							10
						},
						{
							ClipboardData.b("ɭᕯᕱᕳᕵŷ", a_),
							11
						}
					};
					num = 2;
					continue;
				case 21:
				{
					string attribute3;
					if (this.\u1719().ContainsKey(attribute3))
					{
						num = 35;
						continue;
					}
					goto IL_17E;
				}
				case 22:
					A_0.Read();
					num = 25;
					continue;
				case 23:
				{
					int num2;
					string localName;
					if (spr᧓.ᝤ.TryGetValue(localName, out num2))
					{
						num = 40;
						continue;
					}
					goto IL_17E;
				}
				case 24:
					num = 44;
					continue;
				case 25:
					goto IL_1DE;
				case 26:
					goto IL_762;
				case 27:
					num = 4;
					continue;
				case 28:
					goto IL_17E;
				case 29:
					goto IL_6C9;
				case 30:
					goto IL_17E;
				case 31:
				{
					bool flag;
					if (!flag)
					{
						num = 22;
						continue;
					}
					goto IL_1DE;
				}
				case 32:
					num = 36;
					continue;
				case 33:
					goto IL_17E;
				case 34:
				{
					string attribute;
					A_1.ParaStyleName = this.ᜈ()[attribute];
					num = 13;
					continue;
				}
				case 35:
				{
					string attribute3;
					A_1.PicBullet = this.\u1719()[attribute3];
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_267;
					default:
						if (false)
						{
						}
						num = 30;
						continue;
					}
					break;
				}
				case 36:
					if (spr᧓.ᝤ == null)
					{
						goto IL_267;
					}
					goto IL_373;
				case 37:
				{
					if (A_0.IsEmptyElement)
					{
						num = 18;
						continue;
					}
					bool flag = false;
					A_0.Read();
					this.ᜀ(A_0);
					num = 26;
					continue;
				}
				case 38:
					goto IL_17E;
				case 39:
					goto IL_762;
				case 40:
					num = 10;
					continue;
				case 41:
					if (A_0.LocalName != ClipboardData.b("ɭٯṱ", a_))
					{
						num = 29;
						continue;
					}
					num = 37;
					continue;
				case 42:
					goto IL_17E;
				case 43:
					goto IL_17E;
				case 44:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 32;
						continue;
					}
					goto IL_17E;
				}
				case 45:
					goto IL_17E;
				}
				if (A_1 == null)
				{
					num = 7;
					continue;
				}
				num = 41;
				continue;
				IL_17E:
				num = 31;
				continue;
				IL_1DE:
				if (true)
				{
				}
				this.ᜀ(A_0);
				num = 39;
				continue;
				IL_267:
				num = 20;
				continue;
				IL_373:
				num = 23;
				continue;
				IL_762:
				num = 14;
			}
			IL_F6:
			throw new ArgumentException(ClipboardData.b("ɭ᥯űs噵ᑷό੻᭽", a_));
			IL_2C6:
			return;
			IL_6C9:
			throw new XmlException(ClipboardData.b("㭭ṯ᝱౳ٵᵷ᥹ࡻ᭽ꊁﲃꪉ늑", a_) + A_0.LocalName);
		}
		}
	}

	// Token: 0x06001F53 RID: 8019 RVA: 0x0020AA64 File Offset: 0x00209A64
	private void ᜀ(XmlReader A_0, ListLevel A_1)
	{
		int a_ = 7;
		string attribute;
		for (;;)
		{
			attribute = A_0.GetAttribute(ClipboardData.b("Ŭ੮ᙰቲᙴ๶", a_), ClipboardData.b("լ᭮հͲ佴塶噸ࡺṼ᝾ꞈﶌﾐﮖﾘ삠힢횤覦욨\ud9aa쪬肮우\udcb2잴펶즸즺튼\udcbe꓀냂뛄껆ꟈ곊ꃌꏎﻐ냜뻞裠跢", a_));
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_72;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 2:
					goto IL_98;
				case 3:
					if (!(attribute == ClipboardData.b("୬๮ᵰrၴ", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_86;
				case 4:
					goto IL_86;
				case 5:
					if (!(attribute == ClipboardData.b("嵬", a_)))
					{
						goto IL_72;
					}
					goto IL_86;
				case 6:
					goto IL_127;
				case 7:
					if (attribute == ClipboardData.b("ɬ८ᝰ", a_))
					{
						num = 4;
						continue;
					}
					A_1.Word6Legacy = true;
					num = 6;
					continue;
				}
				break;
				IL_72:
				num = 0;
				continue;
				IL_86:
				A_1.Word6Legacy = false;
				num = 2;
			}
		}
		IL_98:
		IL_127:
		if (true)
		{
		}
		attribute = A_0.GetAttribute(ClipboardData.b("Ŭ੮ᙰቲᙴ๶⩸୺ᱼ᱾", a_), ClipboardData.b("լ᭮հͲ佴塶噸ࡺṼ᝾ꞈﶌﾐﮖﾘ삠힢횤覦욨\ud9aa쪬肮우\udcb2잴펶즸즺튼\udcbe꓀냂뛄껆ꟈ곊ꃌꏎﻐ냜뻞裠跢", a_));
		A_1.LegacySpace = (int)float.Parse(attribute, NumberStyles.Number);
		attribute = A_0.GetAttribute(ClipboardData.b("Ŭ੮ᙰቲᙴ๶へᕺ᥼᩾", a_), ClipboardData.b("լ᭮հͲ佴塶噸ࡺṼ᝾ꞈﶌﾐﮖﾘ삠힢횤覦욨\ud9aa쪬肮우\udcb2잴펶즸즺튼\udcbe꓀냂뛄껆ꟈ곊ꃌꏎﻐ냜뻞裠跢", a_));
		A_1.LegacyIndent = (int)float.Parse(attribute, NumberStyles.Number);
	}

	// Token: 0x06001F54 RID: 8020 RVA: 0x0020AC08 File Offset: 0x00209C08
	private FollowCharacterType \u1712(string A_0)
	{
		int a_ = 9;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!(A_0 == ClipboardData.b("ᱮŰቲᙴቶ", a_)))
				{
					num = 4;
					continue;
				}
				return FollowCharacterType.Space;
			case 2:
				if (!(A_0 == ClipboardData.b("᭮ၰᅲ", a_)))
				{
					num = 3;
					continue;
				}
				return FollowCharacterType.Tab;
			case 3:
				num = 0;
				continue;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_45;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 5:
				num = 2;
				continue;
			case 6:
				goto IL_75;
			}
			goto IL_35;
			IL_45:
			num = 5;
			continue;
			IL_35:
			if (true)
			{
			}
			if (A_0 != null)
			{
				goto IL_45;
			}
			return FollowCharacterType.Nothing;
		}
		return FollowCharacterType.Tab;
		IL_75:
		return FollowCharacterType.Nothing;
	}

	// Token: 0x06001F55 RID: 8021 RVA: 0x0020ACF8 File Offset: 0x00209CF8
	private ListNumberAlignment ᜑ(string A_0)
	{
		int a_ = 13;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_75;
			case 2:
				if (true)
				{
				}
				num = 6;
				continue;
			case 3:
				num = 4;
				continue;
			case 4:
				if (!(A_0 == ClipboardData.b("Ųᱴၶᅸེ", a_)))
				{
					num = 5;
					continue;
				}
				return ListNumberAlignment.Right;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3D;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 6:
				if (!(A_0 == ClipboardData.b("ၲၴ᥶൸Ṻོ", a_)))
				{
					num = 3;
					continue;
				}
				return ListNumberAlignment.Center;
			}
			goto IL_35;
			IL_3D:
			num = 2;
			continue;
			IL_35:
			if (A_0 != null)
			{
				goto IL_3D;
			}
			return ListNumberAlignment.Left;
		}
		return ListNumberAlignment.Center;
		IL_75:
		return ListNumberAlignment.Left;
	}

	// Token: 0x06001F56 RID: 8022 RVA: 0x0020ADE8 File Offset: 0x00209DE8
	private void ᜀ(ListLevel A_0, string A_1)
	{
		int a_ = 6;
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				IL_2A:
				int num2;
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (num2 != -1)
					{
						num = 2;
						continue;
					}
					return;
				case 2:
				{
					A_0.NumberPrefix = A_1.Substring(0, num2);
					A_0.NumberPrefix = this.ᜐ(A_0.NumberPrefix);
					int num3 = num2 + 2;
					A_0.NumberSufix = A_1.Substring(num3, A_1.Length - num3);
					num = 0;
					continue;
				}
				case 3:
					goto IL_74;
				}
				while (A_0.PatternType == ListPatternType.Bullet)
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
						goto IL_2A;
					}
				}
				int num4 = A_0.LevelNumber + 1;
				string value = ClipboardData.b("䥫", a_) + num4.ToString();
				num2 = A_1.IndexOf(value);
				num = 1;
			}
			IL_74:
			A_0.BulletCharacter = A_1;
			return;
		}
		}
	}

	// Token: 0x06001F57 RID: 8023 RVA: 0x0020AF14 File Offset: 0x00209F14
	private string ᜐ(string A_0)
	{
		int a_ = 0;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 1:
				goto IL_79;
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
					if (A_0 == string.Empty)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					goto IL_85;
				}
				break;
			}
			if (A_0 == null)
			{
				break;
			}
			num = 0;
		}
		return A_0;
		IL_79:
		return A_0;
		IL_85:
		string text = A_0.Replace(ClipboardData.b("䍥奧", a_), ClipboardData.b("晥", a_));
		text = text.Replace(ClipboardData.b("䍥婧", a_), ClipboardData.b("来", a_));
		text = text.Replace(ClipboardData.b("䍥孧", a_), ClipboardData.b("摥", a_));
		text = text.Replace(ClipboardData.b("䍥屧", a_), ClipboardData.b("敥", a_));
		text = text.Replace(ClipboardData.b("䍥嵧", a_), ClipboardData.b("扥", a_));
		text = text.Replace(ClipboardData.b("䍥幧", a_), ClipboardData.b("捥", a_));
		text = text.Replace(ClipboardData.b("䍥彧", a_), ClipboardData.b("恥", a_));
		text = text.Replace(ClipboardData.b("䍥偧", a_), ClipboardData.b("慥", a_));
		return text.Replace(ClipboardData.b("䍥內", a_), ClipboardData.b("湥", a_));
	}

	// Token: 0x06001F58 RID: 8024 RVA: 0x0020B0E4 File Offset: 0x0020A0E4
	private ListPatternType ᜏ(string A_0)
	{
		int a_ = 3;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				if (spr᧓.ᝥ.TryGetValue(A_0, out num2))
				{
					num = 3;
					continue;
				}
				return ListPatternType.Bullet;
			}
			case 1:
				num = 2;
				continue;
			case 2:
				if (spr᧓.ᝥ == null)
				{
					num = 4;
					continue;
				}
				goto IL_239;
			case 3:
				num = 5;
				continue;
			case 4:
				spr᧓.ᝥ = new Dictionary<string, int>(12)
				{
					{
						ClipboardData.b("ݨѪͬ੮", a_),
						0
					},
					{
						ClipboardData.b("൨๪๬ٮᱰቲᥴ", a_),
						1
					},
					{
						ClipboardData.b("ᱨ᭪ᵬ੮Ͱⅲᩴ᩶ᡸᕺ", a_),
						2
					},
					{
						ClipboardData.b("ըѪᩬ੮Ͱⅲᩴ᩶ᡸᕺ", a_),
						3
					},
					{
						ClipboardData.b("ᱨ᭪ᵬ੮Ͱ㽲ၴͶ൸Ṻོ", a_),
						4
					},
					{
						ClipboardData.b("ըѪᩬ੮Ͱ㽲ၴͶ൸Ṻོ", a_),
						5
					},
					{
						ClipboardData.b("٨ᥪ६ٮὰቲᥴ", a_),
						6
					},
					{
						ClipboardData.b("٨ᥪ६ٮὰቲᥴ⍶ᱸͺॼ", a_),
						7
					},
					{
						ClipboardData.b("൨๪๬ٮᱰቲᥴ⵶ᱸॺቼ", a_),
						8
					},
					{
						ClipboardData.b("੨੪Ὤ୮ᡰᵲᑴ᭶⵸Ṻռ୾", a_),
						9
					},
					{
						ClipboardData.b("ࡨɪᡬ੮Ṱ㕲t᭶ᕸⱺᑼ᭾", a_),
						10
					},
					{
						ClipboardData.b("᭨ṪṬᱮᡰቲ᭴㭶ᙸ౺᡼ൾ", a_),
						11
					}
				};
				num = 6;
				continue;
			case 5:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return ListPatternType.None;
				case 1:
					return ListPatternType.Arabic;
				case 2:
					return ListPatternType.UpRoman;
				case 3:
					return ListPatternType.LowRoman;
				case 4:
					return ListPatternType.UpLetter;
				case 5:
					return ListPatternType.LowLetter;
				case 6:
					return ListPatternType.Ordinal;
				case 7:
					return ListPatternType.OrdinalText;
				case 8:
					goto IL_1FE;
				case 9:
					return ListPatternType.Number;
				case 10:
					return ListPatternType.FarEast;
				case 11:
					return ListPatternType.Special;
				default:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1FE;
					}
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			}
			case 6:
				goto IL_239;
			case 7:
				goto IL_1FC;
			case 8:
				num = 7;
				continue;
			}
			if (A_0 != null)
			{
				num = 1;
				continue;
			}
			return ListPatternType.Bullet;
			IL_239:
			num = 0;
		}
		return ListPatternType.Ordinal;
		IL_1FC:
		return ListPatternType.Bullet;
		IL_1FE:
		if (true)
		{
		}
		return ListPatternType.LeadingZero;
	}

	// Token: 0x06001F59 RID: 8025 RVA: 0x0020B360 File Offset: 0x0020A360
	private void ᜁ(ListStyle A_0)
	{
		if (true)
		{
		}
		A_0.ListType = ListType.Bulleted;
		IEnumerator enumerator = A_0.Levels.GetEnumerator();
		try
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_D2;
				case 2:
					goto IL_C7;
				case 3:
					if (!enumerator.MoveNext())
					{
						num = 6;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
					{
						if (false)
						{
						}
						ListLevel listLevel = (ListLevel)enumerator.Current;
						num = 4;
						continue;
					}
					}
					break;
				case 4:
				{
					ListLevel listLevel;
					if (listLevel.PatternType != ListPatternType.Bullet)
					{
						num = 5;
						continue;
					}
					break;
				}
				case 5:
					A_0.ListType = ListType.Numbered;
					num = 2;
					continue;
				case 6:
					goto IL_C7;
				}
				IL_99:
				num = 3;
				continue;
				goto IL_99;
				IL_C7:
				num = 0;
			}
			IL_D2:;
		}
		finally
		{
			for (;;)
			{
				IDisposable disposable = enumerator as IDisposable;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_112;
					case 1:
						disposable.Dispose();
						num = 0;
						continue;
					case 2:
						if (disposable != null)
						{
							num = 1;
							continue;
						}
						goto IL_114;
					}
					break;
				}
			}
			IL_112:
			IL_114:;
		}
	}

	// Token: 0x06001F5A RID: 8026 RVA: 0x0020B494 File Offset: 0x0020A494
	private void ᜀ(ListStyle A_0)
	{
		int a_ = 1;
		if (A_0.ListType == ListType.Numbered)
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
			A_0.Name = ClipboardData.b("⥦ᱨ٪ཬ੮Ͱᙲᅴ⡶", a_) + Guid.NewGuid().ToString();
			return;
		}
		A_0.Name = ClipboardData.b("╦ᱨݪŬ੮հᙲᅴ⡶", a_) + Guid.NewGuid().ToString();
	}

	// Token: 0x06001F5B RID: 8027 RVA: 0x0020B53C File Offset: 0x0020A53C
	private void ᜎ(XmlReader A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("ᱱų᭵⡷፹ύ㱽ﲇ쎉", a_), ClipboardData.b("ᩱsɵࡷ䁹卻兽ﾋꂍﾏ쾟킡즣장\udca7\ud9a9芫솭슯햱鮳솵ힷ좹\ud8bb캽늿귁ꟃꏅ믇막ꗋꃍ럏뿑룓崙쿟迡藣迥蛧", a_));
				bool flag = A_0.ReadToFollowing(ClipboardData.b("űᱳ᝵ࡷό", a_), ClipboardData.b("ݱٳᡵ䉷ॹύᙽꖇ﶑秊ﺗ놛ﶝ쾟쾡麣킥얧용", a_));
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						string a_2 = this.ᜌ(A_0);
						num = 8;
						continue;
					}
					case 1:
					{
						DocPicture docPicture;
						if (docPicture.Image == null)
						{
							num = 9;
							continue;
						}
						this.\u1719().Add(attribute, docPicture);
						num = 4;
						continue;
					}
					case 2:
						if (flag)
						{
							num = 0;
							continue;
						}
						goto IL_15A;
					case 3:
						if (A_0.LocalName != ClipboardData.b("ᱱų᭵⡷፹ύ㱽ﲇ", a_))
						{
							num = 12;
							continue;
						}
						return;
					case 4:
						goto IL_15A;
					case 5:
					{
						if (true)
						{
						}
						string text = this.\u170D(A_0);
						num = 13;
						continue;
					}
					case 6:
						goto IL_204;
					case 7:
					{
						string text;
						if (text.Length == 0)
						{
							num = 16;
							continue;
						}
						DocPicture docPicture = new DocPicture(this.ᜄ);
						this.ᜀ(docPicture, text, false, true);
						string a_2;
						this.ᜀ(docPicture, a_2);
						num = 10;
						continue;
					}
					case 8:
						if (!A_0.IsEmptyElement)
						{
							num = 18;
							continue;
						}
						goto IL_15A;
					case 9:
						return;
					case 10:
						if (!(attribute == string.Empty))
						{
							num = 14;
							continue;
						}
						return;
					case 11:
						if (A_0.EOF)
						{
							num = 6;
							continue;
						}
						A_0.Read();
						num = 19;
						continue;
					case 12:
						num = 11;
						continue;
					case 13:
					{
						string text;
						if (text != null)
						{
							num = 15;
							continue;
						}
						return;
					}
					case 14:
						num = 1;
						continue;
					case 15:
						num = 7;
						continue;
					case 16:
						goto IL_2C2;
					case 17:
					{
						bool flag2;
						if (!flag2)
						{
							goto IL_15A;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24D;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					}
					case 18:
					{
						bool flag2 = A_0.ReadToFollowing(ClipboardData.b("᭱ᥳ᝵ίό᡻ώ", a_));
						num = 17;
						continue;
					}
					case 19:
						goto IL_24D;
					}
					break;
					IL_15A:
					num = 3;
					continue;
					IL_24D:
					goto IL_15A;
				}
			}
			return;
			IL_204:
			return;
			IL_2C2:
			return;
		}
	}

	// Token: 0x06001F5C RID: 8028 RVA: 0x0020B810 File Offset: 0x0020A810
	private void ᜀ(DocPicture A_0, string A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
			for (;;)
			{
				A_1 = A_1.Replace(ClipboardData.b("ࡷ๹", a_), string.Empty);
				string[] array = A_1.Split(new char[]
				{
					';'
				});
				int num = 0;
				int num2 = array.Length;
				int num3 = 9;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						string text;
						if (text.StartsWith(ClipboardData.b("ཷ፹᡻੽뢁", a_)))
						{
							num3 = 3;
							continue;
						}
						num3 = 5;
						continue;
					}
					case 1:
						goto IL_8B;
					case 2:
						goto IL_101;
					case 3:
					{
						string text = text.Replace(ClipboardData.b("ཷ፹᡻੽뢁", a_), string.Empty);
						A_0.Width = this.ᜎ(text);
						goto IL_165;
					}
					case 4:
						if (num < num2)
						{
							string text = array[num];
							num3 = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_165;
						default:
							if (false)
							{
							}
							num3 = 8;
							continue;
						}
						break;
					case 5:
					{
						string text;
						if (text.StartsWith(ClipboardData.b("ၷόᕻ᥽뺃", a_)))
						{
							num3 = 7;
							continue;
						}
						goto IL_8B;
					}
					case 6:
						goto IL_8B;
					case 7:
					{
						string text = text.Replace(ClipboardData.b("ၷόᕻ᥽뺃", a_), string.Empty);
						A_0.Height = this.ᜎ(text);
						num3 = 1;
						continue;
					}
					case 8:
						return;
					case 9:
						goto IL_101;
					}
					break;
					IL_8B:
					num++;
					num3 = 2;
					continue;
					IL_101:
					num3 = 4;
					continue;
					IL_165:
					if (true)
					{
					}
					num3 = 6;
				}
			}
			return;
		}
	}

	// Token: 0x06001F5D RID: 8029 RVA: 0x0020B9E0 File Offset: 0x0020A9E0
	private float ᜎ(string A_0)
	{
		int a_ = 2;
		if (A_0.EndsWith(ClipboardData.b("ŧѩ", a_)))
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
			if (true)
			{
			}
			int num = int.Parse(A_0.Replace(ClipboardData.b("ŧѩ", a_), string.Empty));
			return (float)spr\u1C39.ᜁ().ᜀ((double)num, PrintUnits.Inch, PrintUnits.Point);
		}
		A_0 = A_0.Replace(ClipboardData.b("ᡧṩ", a_), string.Empty);
		return this.ᜄ(A_0);
	}

	// Token: 0x06001F5E RID: 8030 RVA: 0x0020BA90 File Offset: 0x0020AA90
	private string \u170D(XmlReader A_0)
	{
		int a_ = 10;
		if (A_0.LocalName != ClipboardData.b("᥯άᕳᅵᵷṹᵻ੽", a_))
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
			throw new XmlException(ClipboardData.b("᥯άᕳᅵᵷṹᵻ੽ꊁꦃꚅ慎ﮑﮓ肟쮡삣蚥잧첩貫\uddad\ud8af펱쒳펵", a_));
		}
		return A_0.GetAttribute(ClipboardData.b("᥯ᙱ", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱\udbb3킵\udeb7펹\udfbb\udbbd蒿귁ꟃ독ꗇ꿉ꋋ뫍ￏ껛믝賟菡郣迥蟧蓩鿫蛭駯英蟳", a_));
	}

	// Token: 0x06001F5F RID: 8031 RVA: 0x0020BB2C File Offset: 0x0020AB2C
	private string ᜌ(XmlReader A_0)
	{
		int a_ = 17;
		if (A_0.LocalName != ClipboardData.b("Ѷᅸ᩺ർ᩾", a_))
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
				break;
			}
			throw new XmlException(ClipboardData.b("Ѷᅸ᩺ർ᩾ꆀ꺂ꖄ힆歷搜떔爵햠", a_));
		}
		return A_0.GetAttribute(ClipboardData.b("Ѷ൸ɺᅼ᩾", a_));
	}

	// Token: 0x06001F60 RID: 8032 RVA: 0x0020BBB8 File Offset: 0x0020ABB8
	private void ᜋ(XmlReader A_0)
	{
		int a_ = 2;
		int num = 22;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string localName;
				if (!(localName == ClipboardData.b("᭧ṩᕫɭᕯ", a_)))
				{
					num = 26;
					continue;
				}
				bool flag = this.ᜊ(A_0);
				num = 3;
				continue;
			}
			case 1:
				goto IL_1F8;
			case 2:
				goto IL_1EB;
			case 3:
				goto IL_C9;
			case 4:
				goto IL_2D5;
			case 5:
			{
				string localName;
				if (!(localName == ClipboardData.b("ѧ୩ᡫ୭ṯٱ❳ɵŷᙹ᥻ൽ", a_)))
				{
					num = 27;
					continue;
				}
				this.ᜉ(A_0);
				bool flag = true;
				num = 30;
				continue;
			}
			case 6:
			{
				string localName;
				if (!(localName == ClipboardData.b("౧թཫ⩭ᕯᑱᕳ͵ᑷ๹ཻ", a_)))
				{
					num = 25;
					continue;
				}
				this.ᜈ(A_0);
				num = 13;
				continue;
			}
			case 7:
				A_0.Read();
				num = 31;
				continue;
			case 8:
				if (A_0.NodeType != XmlNodeType.Element)
				{
					A_0.Skip();
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C9;
				default:
					if (false)
					{
					}
					num = 19;
					continue;
				}
				break;
			case 9:
			{
				if (A_0.IsEmptyElement)
				{
					num = 2;
					continue;
				}
				bool flag = false;
				A_0.Read();
				this.ᜀ(A_0);
				num = 16;
				continue;
			}
			case 10:
				num = 28;
				continue;
			case 11:
				goto IL_243;
			case 12:
				num = 6;
				continue;
			case 13:
				goto IL_39D;
			case 14:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 32;
					continue;
				}
				A_0.Read();
				num = 18;
				continue;
			case 15:
			{
				bool flag;
				if (!flag)
				{
					num = 7;
					continue;
				}
				goto IL_1F8;
			}
			case 16:
				goto IL_174;
			case 17:
				goto IL_B1;
			case 18:
				goto IL_134;
			case 19:
				num = 20;
				continue;
			case 20:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 12;
					continue;
				}
				goto IL_39D;
			}
			case 21:
				goto IL_39D;
			case 23:
				goto IL_174;
			case 24:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 10;
					continue;
				}
				goto IL_3C8;
			case 25:
				num = 5;
				continue;
			case 26:
				num = 21;
				continue;
			case 27:
				num = 0;
				continue;
			case 28:
			{
				if (!(A_0.LocalName != ClipboardData.b("᭧ṩᕫɭᕯű", a_)))
				{
					num = 11;
					continue;
				}
				bool flag = false;
				num = 8;
				continue;
			}
			case 29:
				if (A_0.LocalName != ClipboardData.b("᭧ṩᕫɭᕯű", a_))
				{
					num = 4;
					continue;
				}
				num = 9;
				continue;
			case 30:
				goto IL_39D;
			case 31:
				goto IL_1F8;
			case 32:
				num = 29;
				continue;
			}
			if (A_0 == null)
			{
				num = 17;
				continue;
			}
			IL_134:
			num = 14;
			continue;
			IL_174:
			num = 24;
			continue;
			IL_1F8:
			this.ᜀ(A_0);
			num = 23;
			continue;
			IL_39D:
			num = 15;
			continue;
			IL_C9:
			goto IL_39D;
		}
		IL_B1:
		throw new ArgumentNullException(ClipboardData.b("ᩧཀྵ൫੭ᕯq", a_));
		IL_1EB:
		if (true)
		{
		}
		return;
		IL_243:
		goto IL_3C8;
		IL_2D5:
		throw new XmlException(ClipboardData.b("㵧ѩ५᙭o᝱ᝳɵᵷṹ屻ٽꒃ겋", a_) + A_0.LocalName);
		IL_3C8:
		this.ᜂ();
		this.ᜀ();
		this.ᜁ();
	}

	// Token: 0x06001F61 RID: 8033 RVA: 0x0020BFA0 File Offset: 0x0020AFA0
	private void ᜂ()
	{
		int num = 0;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			Dictionary<string, string>.KeyCollection.Enumerator enumerator;
			switch (num)
			{
			case 1:
				try
				{
					num = 7;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							Style style;
							if (style != null)
							{
								num = 4;
								continue;
							}
							break;
						}
						case 1:
						{
							Style style;
							string text;
							style.ApplyBaseStyle(this.ᜈ()[this.\u170D[text]]);
							num = 6;
							continue;
						}
						case 2:
						{
							string text;
							if (this.ᜈ().ContainsKey(this.\u170D[text]))
							{
								num = 1;
								continue;
							}
							break;
						}
						case 3:
							num = 8;
							continue;
						case 4:
							num = 2;
							continue;
						case 5:
						{
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							string text = enumerator.Current;
							Style style = this.ᜄ.Styles.FindByName(text);
							num = 0;
							continue;
						}
						case 8:
							goto IL_148;
						}
						IL_C7:
						num = 5;
						continue;
						goto IL_C7;
					}
					IL_148:
					return;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				goto IL_158;
			case 2:
				return;
			}
			if (this.\u170D == null)
			{
				num = 2;
				continue;
			}
			IL_158:
			enumerator = this.\u170D.Keys.GetEnumerator();
			num = 1;
		}
	}

	// Token: 0x06001F62 RID: 8034 RVA: 0x0020C138 File Offset: 0x0020B138
	private void ᜁ()
	{
		switch (0)
		{
		default:
		{
			IEnumerator enumerator = this.ᜄ.Styles.GetEnumerator();
			try
			{
				int num = 7;
				for (;;)
				{
					IStyle style;
					string key;
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
							num = 5;
							continue;
						case 1:
						{
							ListStyle currentListStyle;
							if (currentListStyle != null)
							{
								num = 12;
								continue;
							}
							break;
						}
						case 2:
						{
							ListStyle currentListStyle;
							if (currentListStyle.BaseListStyleName != null)
							{
								num = 11;
								continue;
							}
							break;
						}
						case 3:
						{
							ListStyle currentListStyle;
							if (this.ᜎ().ContainsKey(currentListStyle.BaseListStyleName))
							{
								num = 8;
								continue;
							}
							break;
						}
						case 4:
						{
							ListStyle currentListStyle = (style as ParagraphStyle).ListFormat.CurrentListStyle;
							num = 1;
							continue;
						}
						case 5:
							goto IL_1EB;
						case 6:
							if (style is ParagraphStyle)
							{
								num = 4;
								continue;
							}
							break;
						case 8:
						{
							ListStyle currentListStyle;
							key = this.ᜎ()[currentListStyle.BaseListStyleName];
							num = 10;
							continue;
						}
						case 10:
							if (this.ᜎ().ContainsKey(key))
							{
								num = 13;
								continue;
							}
							break;
						case 11:
							num = 3;
							continue;
						case 12:
							num = 2;
							continue;
						case 13:
							goto IL_CC;
						case 14:
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							style = (IStyle)enumerator.Current;
							num = 6;
							continue;
						}
						IL_167:
						num = 14;
						continue;
						goto IL_167;
					}
					IL_CC:
					string styleName = this.ᜎ()[key];
					(style as ParagraphStyle).ListFormat.ApplyStyle(styleName);
					num = 9;
				}
				IL_1EB:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_23B;
						case 1:
							if (disposable != null)
							{
								if (true)
								{
								}
								num = 2;
								continue;
							}
							goto IL_23D;
						case 2:
							disposable.Dispose();
							num = 0;
							continue;
						}
						break;
					}
				}
				IL_23B:
				IL_23D:;
			}
			return;
		}
		}
	}

	// Token: 0x06001F63 RID: 8035 RVA: 0x0020C3A0 File Offset: 0x0020B3A0
	private void ᜀ()
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				Dictionary<string, string>.KeyCollection.Enumerator enumerator;
				switch (num)
				{
				case 0:
					return;
				case 1:
					IL_15B:
					try
					{
						num = 5;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								Style style;
								string key;
								style.LinkStyle = this.ᜈ()[key];
								num = 3;
								continue;
							}
							case 1:
								num = 6;
								continue;
							case 2:
							{
								string key;
								if (this.ᜈ().ContainsKey(key))
								{
									num = 0;
									continue;
								}
								break;
							}
							case 4:
							{
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								string text = enumerator.Current;
								Style style = this.ᜄ.Styles.FindByName(text);
								string key = this.ᜌ[text];
								num = 2;
								continue;
							}
							case 6:
								goto IL_112;
							}
							IL_C6:
							num = 4;
							continue;
							goto IL_C6;
						}
						IL_112:
						return;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_122;
				}
				if (this.ᜌ == null)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				IL_122:
				enumerator = this.ᜌ.Keys.GetEnumerator();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_15B;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
			}
			return;
		}
		}
	}

	// Token: 0x06001F64 RID: 8036 RVA: 0x0020C528 File Offset: 0x0020B528
	private bool ᜊ(XmlReader A_0)
	{
		int a_ = 8;
		int num = 2;
		Style style;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1C1;
			case 1:
				goto IL_64;
			case 3:
				if (A_0.LocalName != ClipboardData.b("ᵭѯୱᡳ፵", a_))
				{
					num = 7;
					continue;
				}
				num = 12;
				continue;
			case 4:
			{
				string attribute;
				style.IsCustom = XmlConvert.ToBoolean(attribute);
				num = 0;
				continue;
			}
			case 5:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 15;
					continue;
				}
				A_0.Read();
				num = 13;
				continue;
			case 6:
				goto IL_BD;
			case 7:
				goto IL_21A;
			case 8:
			{
				string attribute;
				if (attribute != null)
				{
					num = 4;
					continue;
				}
				goto IL_27F;
			}
			case 9:
				return false;
			case 10:
			{
				if (!A_0.HasAttributes)
				{
					num = 11;
					continue;
				}
				string text = A_0.GetAttribute(ClipboardData.b("ᩭ९ɱᅳ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_)).ToLower();
				goto IL_91;
			}
			case 11:
				goto IL_247;
			case 12:
				if (A_0.IsEmptyElement)
				{
					num = 9;
					continue;
				}
				num = 10;
				continue;
			case 13:
				goto IL_CE;
			case 14:
			{
				string text;
				if (text == ClipboardData.b("mկάᙳ፵੷፹ቻ᥽", a_))
				{
					num = 6;
					continue;
				}
				style = this.\u170D(text);
				style.Name = A_0.GetAttribute(ClipboardData.b("ᵭѯୱᡳ፵ㅷṹ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_));
				string attribute = A_0.GetAttribute(ClipboardData.b("൭կűs᥵ᕷ⥹ࡻݽ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﺍﲑﮕﲙ춟쎡킣향蚧얩\udeab즭龯얱\udbb3쒵\udcb7쪹캻톽ꎿꟁ럃뗅ꇇ꓉ꯋꏍ볏﷑돝臟诡諣", a_));
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
					num = 8;
					continue;
				}
				break;
			}
			case 15:
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			goto IL_CE;
			IL_91:
			num = 14;
			continue;
			IL_CE:
			num = 5;
		}
		IL_64:
		throw new ArgumentNullException(ClipboardData.b("ᱭᕯ፱ၳ፵੷", a_));
		IL_BD:
		this.ᜢ(A_0);
		return true;
		IL_1C1:
		goto IL_27F;
		IL_21A:
		throw new XmlException(ClipboardData.b("㭭ṯ᝱౳ٵᵷ᥹ࡻ᭽ꊁﲃꪉ늑", a_) + A_0.LocalName);
		IL_247:
		A_0.Read();
		return false;
		IL_27F:
		A_0.MoveToElement();
		this.ᜁ(A_0, style);
		this.ᜄ.Styles.Add(style);
		return false;
	}

	// Token: 0x06001F65 RID: 8037 RVA: 0x0020C7D8 File Offset: 0x0020B7D8
	private void ᜁ(XmlReader A_0, Style A_1)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 46;
			for (;;)
			{
				string text;
				switch (num)
				{
				case 0:
				{
					bool flag;
					if (!flag)
					{
						num = 2;
						continue;
					}
					goto IL_2D2;
				}
				case 1:
					goto IL_6A5;
				case 2:
					A_0.Read();
					num = 20;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A00;
					default:
						if (false)
						{
						}
						num = 63;
						continue;
					}
					break;
				case 4:
					goto IL_1E4;
				case 5:
					spr᧓.ᝦ = new Dictionary<string, int>(13)
					{
						{
							ClipboardData.b("Ͱ⍲ݴ", a_),
							0
						},
						{
							ClipboardData.b("Ű⍲ݴ", a_),
							1
						},
						{
							ClipboardData.b("հᅲᥴ❶୸", a_),
							2
						},
						{
							ClipboardData.b("հŲ╴ն", a_),
							3
						},
						{
							ClipboardData.b("հၲ╴ն", a_),
							4
						},
						{
							ClipboardData.b("հᅲᥴ⑶൸ɺᅼ᩾톀", a_),
							5
						},
						{
							ClipboardData.b("ɰᙲᡴṶㅸቺ᥼᭾", a_),
							6
						},
						{
							ClipboardData.b("ѰᵲᵴṶᵸṺ⩼᝾킄", a_),
							7
						},
						{
							ClipboardData.b("p㕲ᩴնᑸ᩺ॼ", a_),
							8
						},
						{
							ClipboardData.b("ᵰᩲ᭴ᱶ", a_),
							9
						},
						{
							ClipboardData.b("ὰᙲ൴Ͷ", a_),
							10
						},
						{
							ClipboardData.b("ὰቲᡴቶ", a_),
							11
						},
						{
							ClipboardData.b("፰ቲٴቶᵸ㑺፼", a_),
							12
						}
					};
					num = 26;
					continue;
				case 6:
					num = 11;
					continue;
				case 7:
				{
					string localName;
					int num2;
					if (spr᧓.ᝦ.TryGetValue(localName, out num2))
					{
						num = 62;
						continue;
					}
					goto IL_1E4;
				}
				case 8:
					goto IL_1E4;
				case 9:
					goto IL_A00;
				case 10:
					num = 27;
					continue;
				case 11:
				{
					if (true)
					{
					}
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 17;
						continue;
					}
					goto IL_1E4;
				}
				case 12:
					num = 56;
					continue;
				case 13:
					if (!A_1.IsCustom)
					{
						num = 40;
						continue;
					}
					goto IL_A6B;
				case 14:
					goto IL_39F;
				case 15:
					return;
				case 16:
					goto IL_1E4;
				case 17:
					num = 57;
					continue;
				case 18:
					goto IL_1E4;
				case 19:
					goto IL_1E4;
				case 20:
					goto IL_2D2;
				case 21:
					goto IL_1E4;
				case 22:
					goto IL_39F;
				case 23:
					goto IL_1E4;
				case 24:
					if (A_0.LocalName != ClipboardData.b("ɰݲ౴᭶ᱸ", a_))
					{
						num = 50;
						continue;
					}
					num = 64;
					continue;
				case 25:
					A_1.\u170D();
					num = 33;
					continue;
				case 26:
					goto IL_36D;
				case 27:
					if (A_1 is ParagraphStyle)
					{
						num = 25;
						continue;
					}
					return;
				case 28:
					goto IL_A6B;
				case 29:
					goto IL_1E4;
				case 30:
				{
					bool flag2;
					if (!flag2)
					{
						num = 3;
						continue;
					}
					return;
				}
				case 31:
					goto IL_1E4;
				case 32:
					goto IL_1E4;
				case 33:
					return;
				case 34:
					goto IL_9CB;
				case 35:
					goto IL_163;
				case 36:
					num = 24;
					continue;
				case 37:
				{
					int num2;
					switch (num2)
					{
					case 0:
					{
						CharacterFormat a_2 = this.ᜁ(A_1);
						this.ᜋ(A_0, a_2);
						num = 70;
						continue;
					}
					case 1:
					{
						ParagraphFormat a_3 = this.ᜀ(A_1);
						this.ᜋ(A_0, a_3);
						num = 29;
						continue;
					}
					case 2:
						this.ᜁ(A_0, (A_1 as spr\u173A).ᜃ());
						num = 44;
						continue;
					case 3:
						this.ᜀ(A_0, (A_1 as spr\u173A).ᜈ());
						num = 41;
						continue;
					case 4:
						this.ᜁ(A_0, (A_1 as spr\u173A).ᜊ());
						num = 69;
						continue;
					case 5:
						this.ᜀ(A_0, A_1);
						num = 59;
						continue;
					case 6:
						A_1.IsSemiHidden = this.ᜂ(A_0);
						num = 8;
						continue;
					case 7:
						A_1.UnhideWhenUsed = this.ᜂ(A_0);
						num = 23;
						continue;
					case 8:
						A_1.IsPrimaryStyle = this.ᜂ(A_0);
						num = 18;
						continue;
					case 9:
					{
						string attribute = A_0.GetAttribute(ClipboardData.b("ݰቲᥴ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
						num = 54;
						continue;
					}
					case 10:
					{
						string attribute2 = A_0.GetAttribute(ClipboardData.b("ݰቲᥴ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
						A_1.NextStyle = attribute2;
						num = 31;
						continue;
					}
					case 11:
						text = A_0.GetAttribute(ClipboardData.b("ݰቲᥴ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
						num = 13;
						continue;
					case 12:
					{
						string attribute3 = A_0.GetAttribute(ClipboardData.b("ݰቲᥴ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
						bool flag2 = true;
						num = 42;
						continue;
					}
					default:
						num = 38;
						continue;
					}
					break;
				}
				case 38:
					num = 49;
					continue;
				case 39:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 12;
						continue;
					}
					goto IL_789;
				case 40:
					num = 9;
					continue;
				case 41:
					goto IL_1E4;
				case 42:
				{
					string attribute3;
					if (!this.ᜈ().ContainsKey(attribute3))
					{
						num = 68;
						continue;
					}
					IStyle style = this.ᜄ.Styles.FindByName(this.ᜈ()[attribute3]);
					num = 71;
					continue;
				}
				case 43:
					this.ᜈ().Add(A_1.Name, text);
					num = 35;
					continue;
				case 44:
					goto IL_1E4;
				case 45:
					goto IL_1E4;
				case 47:
				{
					string attribute;
					A_1.LinkStyle = this.ᜈ()[attribute];
					num = 45;
					continue;
				}
				case 48:
					goto IL_2D2;
				case 49:
					goto IL_1E4;
				case 50:
					goto IL_7DF;
				case 51:
					text = A_1.BuiltinStyles[text.ToLower()];
					num = 28;
					continue;
				case 52:
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					goto IL_9CB;
				case 53:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 36;
						continue;
					}
					A_0.Read();
					num = 34;
					continue;
				case 54:
				{
					string attribute;
					if (this.ᜈ().ContainsKey(attribute))
					{
						num = 47;
						continue;
					}
					this.ᜇ().Add(A_1.Name, attribute);
					num = 21;
					continue;
				}
				case 55:
					if (!this.ᜈ().ContainsKey(A_1.Name))
					{
						num = 43;
						continue;
					}
					goto IL_163;
				case 56:
				{
					if (!(A_0.LocalName != ClipboardData.b("ɰݲ౴᭶ᱸ", a_)))
					{
						num = 61;
						continue;
					}
					bool flag = false;
					num = 67;
					continue;
				}
				case 57:
					if (spr᧓.ᝦ == null)
					{
						num = 5;
						continue;
					}
					goto IL_36D;
				case 58:
					goto IL_15E;
				case 59:
					goto IL_1E4;
				case 60:
				{
					string attribute3;
					this.ᜉ().Add(A_1.Name, attribute3);
					num = 16;
					continue;
				}
				case 61:
					goto IL_789;
				case 62:
					num = 37;
					continue;
				case 63:
					if (A_1.BaseStyle != null)
					{
						num = 10;
						continue;
					}
					return;
				case 64:
				{
					if (A_0.IsEmptyElement)
					{
						num = 15;
						continue;
					}
					bool flag = false;
					bool flag2 = false;
					A_0.Read();
					this.ᜀ(A_0);
					num = 14;
					continue;
				}
				case 65:
					if (!string.IsNullOrEmpty(A_1.Name))
					{
						num = 66;
						continue;
					}
					goto IL_163;
				case 66:
					num = 55;
					continue;
				case 67:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 6;
						continue;
					}
					A_0.Read();
					num = 48;
					continue;
				case 68:
				{
					string attribute3;
					this.ᜉ().Add(A_1.Name, attribute3);
					num = 19;
					continue;
				}
				case 69:
					goto IL_1E4;
				case 70:
					goto IL_1E4;
				case 71:
				{
					IStyle style;
					if (style == null)
					{
						num = 60;
						continue;
					}
					string attribute3;
					A_1.ApplyBaseStyle(this.ᜈ()[attribute3]);
					num = 32;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 58;
					continue;
				}
				num = 52;
				continue;
				IL_163:
				A_1.Name = text;
				num = 4;
				continue;
				IL_1E4:
				num = 0;
				continue;
				IL_2D2:
				this.ᜀ(A_0);
				num = 22;
				continue;
				IL_36D:
				num = 7;
				continue;
				IL_39F:
				num = 39;
				continue;
				IL_789:
				num = 30;
				continue;
				IL_9CB:
				num = 53;
				continue;
				IL_A00:
				if (A_1.BuiltinStyles.ContainsKey(text.ToLower()))
				{
					num = 51;
					continue;
				}
				IL_A6B:
				num = 65;
			}
			IL_15E:
			throw new ArgumentNullException(ClipboardData.b("Ͱᙲᑴ፶ᱸॺ", a_));
			IL_6A5:
			throw new ArgumentException(ClipboardData.b("ɰݲ౴᭶ᱸ", a_));
			IL_7DF:
			throw new XmlException(ClipboardData.b("⑰ᵲၴྲྀॸṺṼ୾ꖄﾆ권ﮎ떔", a_) + A_0.LocalName);
		}
		}
	}

	// Token: 0x06001F66 RID: 8038 RVA: 0x0020D280 File Offset: 0x0020C280
	private Style \u170D(string A_0)
	{
		int a_ = 4;
		Style result;
		for (;;)
		{
			result = null;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return result;
				case 1:
					return result;
				case 2:
					return result;
				case 3:
					if (A_0 != null)
					{
						if (true)
						{
						}
						num = 9;
						continue;
					}
					return result;
				case 4:
					if (!(A_0 == ClipboardData.b("ᩩ൫ᱭᅯᕱٳ᝵ࡷቹ", a_)))
					{
						num = 7;
						continue;
					}
					result = new ParagraphStyle(this.ᜄ);
					num = 2;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return result;
					default:
						if (false)
						{
						}
						if (!(A_0 == ClipboardData.b("ṩ൫౭ᱯ᝱", a_)))
						{
							num = 6;
							continue;
						}
						result = new spr\u173A(this.ᜄ);
						num = 10;
						continue;
					}
					break;
				case 6:
					num = 1;
					continue;
				case 7:
					num = 5;
					continue;
				case 8:
					num = 4;
					continue;
				case 9:
					num = 11;
					continue;
				case 10:
					return result;
				case 11:
					if (!(A_0 == ClipboardData.b("३ѫ཭ɯ፱ᝳɵᵷࡹ", a_)))
					{
						num = 8;
						continue;
					}
					result = new sprᯉ(this.ᜄ);
					num = 0;
					continue;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x06001F67 RID: 8039 RVA: 0x0020D40C File Offset: 0x0020C40C
	private void ᜉ(XmlReader A_0)
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
		this.ᜄ.LatentStyles2010 = this.ᜢ(A_0);
	}

	// Token: 0x06001F68 RID: 8040 RVA: 0x0020D45C File Offset: 0x0020C45C
	private void ᜈ(XmlReader A_0)
	{
		int a_ = 2;
		int num = 22;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜋ(A_0, this.ᜄ.ᜬ);
				A_0.Read();
				num = 12;
				continue;
			case 1:
			{
				string localName;
				if (localName == ClipboardData.b("ᡧ㩩ṫ⩭ᕯᑱᕳ͵ᑷ๹", a_))
				{
					if (true)
					{
					}
					this.ᜄ.ᜬ = new ParagraphFormat(this.ᜄ);
					num = 6;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2E7;
				default:
					if (false)
					{
					}
					num = 28;
					continue;
				}
				break;
			}
			case 2:
				A_0.Read();
				this.ᜀ(A_0);
				this.ᜄ.DefCharFormat = new CharacterFormat(this.ᜄ);
				this.ᜋ(A_0, this.ᜄ.DefCharFormat);
				A_0.Read();
				num = 18;
				continue;
			case 3:
				return;
			case 4:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 5;
					continue;
				}
				A_0.Read();
				num = 13;
				continue;
			case 5:
				num = 21;
				continue;
			case 6:
				if (!A_0.IsEmptyElement)
				{
					num = 7;
					continue;
				}
				goto IL_146;
			case 7:
				A_0.Read();
				this.ᜀ(A_0);
				num = 19;
				continue;
			case 8:
				if (!(A_0.LocalName != ClipboardData.b("౧թཫ⩭ᕯᑱᕳ͵ᑷ๹ཻ", a_)))
				{
					num = 24;
					continue;
				}
				num = 4;
				continue;
			case 9:
			{
				string localName;
				if (!(localName == ClipboardData.b("ᩧ㩩ṫ⩭ᕯᑱᕳ͵ᑷ๹", a_)))
				{
					num = 17;
					continue;
				}
				num = 14;
				continue;
			}
			case 10:
				num = 16;
				continue;
			case 11:
				num = 9;
				continue;
			case 12:
				goto IL_146;
			case 13:
				goto IL_19F;
			case 14:
				if (!A_0.IsEmptyElement)
				{
					num = 2;
					continue;
				}
				goto IL_146;
			case 15:
				goto IL_214;
			case 16:
				if (A_0.IsStartElement())
				{
					num = 0;
					continue;
				}
				goto IL_146;
			case 17:
				goto IL_2E7;
			case 18:
				goto IL_146;
			case 19:
				if (A_0.LocalName == ClipboardData.b("ᡧ㩩ṫ", a_))
				{
					num = 10;
					continue;
				}
				goto IL_146;
			case 20:
				goto IL_B6;
			case 21:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 11;
					continue;
				}
				goto IL_146;
			}
			case 23:
				goto IL_19F;
			case 24:
				return;
			case 25:
				goto IL_214;
			case 26:
				if (A_0.IsEmptyElement)
				{
					num = 3;
					continue;
				}
				A_0.Read();
				this.ᜀ(A_0);
				num = 15;
				continue;
			case 27:
				goto IL_146;
			case 28:
				num = 27;
				continue;
			}
			if (A_0.LocalName != ClipboardData.b("౧թཫ⩭ᕯᑱᕳ͵ᑷ๹ཻ", a_))
			{
				num = 20;
				continue;
			}
			num = 26;
			continue;
			IL_146:
			A_0.Read();
			num = 23;
			continue;
			IL_19F:
			this.ᜀ(A_0);
			num = 25;
			continue;
			IL_214:
			num = 8;
			continue;
			IL_2E7:
			num = 1;
		}
		IL_B6:
		throw new XmlException(ClipboardData.b("౧թཫ⩭ᕯᑱᕳ͵ᑷ๹ཻ", a_));
	}

	// Token: 0x06001F69 RID: 8041 RVA: 0x0020D818 File Offset: 0x0020C818
	private CharacterFormat ᜁ(Style A_0)
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
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_29;
				default:
					goto IL_8C;
				}
				break;
			case 2:
				if (A_0.StyleType != StyleType.CharacterStyle)
				{
					num = 1;
					continue;
				}
				goto IL_33;
			}
			goto IL_20;
			IL_29:
			num = 0;
			continue;
			IL_20:
			if (A_0.StyleType != StyleType.TableStyle)
			{
				goto IL_29;
			}
			goto IL_94;
		}
		IL_33:
		return (A_0 as sprᯉ).CharacterFormat;
		IL_8C:
		if (false)
		{
		}
		return (A_0 as ParagraphStyle).CharacterFormat;
		IL_94:
		return (A_0 as spr\u173A).CharacterFormat;
	}

	// Token: 0x06001F6A RID: 8042 RVA: 0x0020D8C4 File Offset: 0x0020C8C4
	private ParagraphFormat ᜀ(Style A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_37;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		if (A_0.StyleType == StyleType.TableStyle)
		{
			return (A_0 as spr\u173A).ᜅ();
		}
		IL_37:
		return (A_0 as ParagraphStyle).ParagraphFormat;
	}

	// Token: 0x06001F6B RID: 8043 RVA: 0x0020D924 File Offset: 0x0020C924
	private void ᜀ(XmlReader A_0, Style A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 36;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᙥ㡧ᡩ", a_)))
					{
						num = 13;
						continue;
					}
					sprῊ sprῊ;
					this.ᜋ(A_0, sprῊ.ᜀ());
					num = 11;
					continue;
				}
				case 1:
				{
					string localName;
					if (!(localName == ClipboardData.b("ብ੧٩㱫ᱭ", a_)))
					{
						num = 16;
						continue;
					}
					sprῊ sprῊ;
					this.ᜁ(A_0, sprῊ.ᜂ());
					num = 14;
					continue;
				}
				case 2:
				{
					string localName;
					if (!(localName == ClipboardData.b("ብᩧ㩩ṫ", a_)))
					{
						num = 7;
						continue;
					}
					sprῊ sprῊ;
					this.ᜀ(A_0, sprῊ.ᜅ());
					num = 3;
					continue;
				}
				case 3:
					goto IL_150;
				case 4:
					goto IL_243;
				case 5:
					if (!(A_0.LocalName != ClipboardData.b("ብ੧٩㽫ᩭ९ṱᅳ♵੷", a_)))
					{
						num = 4;
						continue;
					}
					num = 30;
					continue;
				case 6:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 33;
						continue;
					}
					goto IL_150;
				}
				case 7:
					num = 25;
					continue;
				case 8:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 21;
						continue;
					}
					return;
				case 9:
					goto IL_446;
				case 10:
					goto IL_150;
				case 11:
					goto IL_150;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_286;
					}
					if (false)
					{
					}
					num = 0;
					continue;
				case 13:
					num = 1;
					continue;
				case 14:
					goto IL_150;
				case 15:
					if (A_1 == null)
					{
						num = 9;
						continue;
					}
					goto IL_286;
				case 16:
					num = 2;
					continue;
				case 17:
					goto IL_150;
				case 18:
					num = 27;
					continue;
				case 19:
					goto IL_197;
				case 20:
					goto IL_44B;
				case 21:
					num = 5;
					continue;
				case 22:
				{
					if (A_0.IsEmptyElement)
					{
						num = 32;
						continue;
					}
					string attribute = A_0.GetAttribute(ClipboardData.b("ብᅧᩩ५", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧\udda9쎫\udcad풯슱욳\ud9b5\udbb7\udfb9쾻춽ꦿ곁ꏃꯅ꓇ﻋﻍﯓ믕맗동닛", a_));
					ConditionalFormattingCode a_2 = this.ᜌ(attribute);
					sprῊ sprῊ = (A_1 as spr\u173A).ᜀ(a_2);
					A_0.Read();
					this.ᜀ(A_0);
					num = 19;
					continue;
				}
				case 23:
					goto IL_44B;
				case 24:
					goto IL_286;
				case 25:
				{
					string localName;
					if (!(localName == ClipboardData.b("ብ୧㩩ṫ", a_)))
					{
						num = 18;
						continue;
					}
					sprῊ sprῊ;
					this.ᜁ(A_0, sprῊ.ᜈ());
					if (true)
					{
					}
					num = 10;
					continue;
				}
				case 26:
					goto IL_197;
				case 27:
					goto IL_150;
				case 28:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 34;
						continue;
					}
					A_0.Read();
					num = 24;
					continue;
				case 29:
					goto IL_D2;
				case 30:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 31;
						continue;
					}
					A_0.Read();
					num = 23;
					continue;
				case 31:
					num = 6;
					continue;
				case 32:
					return;
				case 33:
					num = 35;
					continue;
				case 34:
					num = 22;
					continue;
				case 35:
				{
					string localName;
					if (!(localName == ClipboardData.b("ᑥ㡧ᡩ", a_)))
					{
						num = 12;
						continue;
					}
					sprῊ sprῊ;
					this.ᜋ(A_0, sprῊ.CharacterFormat);
					num = 17;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 29;
					continue;
				}
				num = 15;
				continue;
				IL_150:
				A_0.Read();
				num = 20;
				continue;
				IL_197:
				num = 8;
				continue;
				IL_286:
				num = 28;
				continue;
				IL_44B:
				this.ᜀ(A_0);
				num = 26;
			}
			IL_D2:
			throw new ArgumentNullException(ClipboardData.b("ᑥ൧୩࡫୭ɯ", a_));
			IL_243:
			return;
			IL_446:
			throw new ArgumentException(ClipboardData.b("ᕥᱧ፩k୭", a_));
		}
		}
	}

	// Token: 0x06001F6C RID: 8044 RVA: 0x0020DDE8 File Offset: 0x0020CDE8
	private ConditionalFormattingCode ᜌ(string A_0)
	{
		int a_ = 2;
		ConditionalFormattingCode result;
		for (;;)
		{
			for (;;)
			{
				result = ConditionalFormattingCode.FirstRow;
				int num = 15;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2;
						switch (num2)
						{
						case 0:
							result = ConditionalFormattingCode.FirstRow;
							num = 12;
							continue;
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
								result = ConditionalFormattingCode.LastRow;
								num = 16;
								continue;
							}
							break;
						case 2:
							result = ConditionalFormattingCode.OddRowBanding;
							num = 9;
							continue;
						case 3:
							result = ConditionalFormattingCode.EvenRowBanding;
							num = 14;
							continue;
						case 4:
							result = ConditionalFormattingCode.FirstColumn;
							num = 17;
							continue;
						case 5:
							result = ConditionalFormattingCode.LastColumn;
							num = 4;
							continue;
						case 6:
							result = ConditionalFormattingCode.OddColumnBanding;
							num = 8;
							continue;
						case 7:
							result = ConditionalFormattingCode.EvenColumnBanding;
							num = 1;
							continue;
						case 8:
							result = ConditionalFormattingCode.FirstRowLastCell;
							num = 19;
							continue;
						case 9:
							result = ConditionalFormattingCode.FirstRowFirstCell;
							num = 7;
							continue;
						case 10:
							result = ConditionalFormattingCode.LastRowLastCell;
							num = 11;
							continue;
						case 11:
							result = ConditionalFormattingCode.LastRowFirstCell;
							num = 18;
							continue;
						default:
							num = 5;
							continue;
						}
						break;
					}
					case 1:
						goto IL_244;
					case 2:
						goto IL_2DC;
					case 3:
						if (spr᧓.ᝧ == null)
						{
							num = 20;
							continue;
						}
						goto IL_2DC;
					case 4:
						return result;
					case 5:
						num = 6;
						continue;
					case 6:
						goto IL_279;
					case 7:
						goto IL_115;
					case 8:
						goto IL_9F;
					case 9:
						goto IL_269;
					case 10:
						num = 0;
						continue;
					case 11:
						goto IL_257;
					case 12:
						goto IL_2B0;
					case 13:
						num = 3;
						continue;
					case 14:
						goto IL_340;
					case 15:
						if (A_0 != null)
						{
							num = 13;
							continue;
						}
						return result;
					case 16:
						goto IL_331;
					case 17:
						goto IL_B1;
					case 18:
						goto IL_29E;
					case 19:
						goto IL_28B;
					case 20:
						spr᧓.ᝧ = new Dictionary<string, int>(12)
						{
							{
								ClipboardData.b("๧ͩṫᵭѯⁱ᭳ŵ", a_),
								0
							},
							{
								ClipboardData.b("ѧ୩Ὣᩭ≯ᵱͳ", a_),
								1
							},
							{
								ClipboardData.b("੧୩ɫ੭䅯㩱᭳ѵɷ", a_),
								2
							},
							{
								ClipboardData.b("੧୩ɫ੭䉯㩱᭳ѵɷ", a_),
								3
							},
							{
								ClipboardData.b("๧ͩṫᵭѯㅱ᭳᩵", a_),
								4
							},
							{
								ClipboardData.b("ѧ୩Ὣᩭ㍯ᵱᡳ", a_),
								5
							},
							{
								ClipboardData.b("੧୩ɫ੭䅯⑱ᅳѵ౷", a_),
								6
							},
							{
								ClipboardData.b("੧୩ɫ੭䉯⑱ᅳѵ౷", a_),
								7
							},
							{
								ClipboardData.b("٧ཀྵ⽫୭ᱯṱ", a_),
								8
							},
							{
								ClipboardData.b("٧ᵩ⽫୭ᱯṱ", a_),
								9
							},
							{
								ClipboardData.b("᭧ཀྵ⽫୭ᱯṱ", a_),
								10
							},
							{
								ClipboardData.b("᭧ᵩ⽫୭ᱯṱ", a_),
								11
							}
						};
						num = 2;
						continue;
					case 21:
					{
						int num2;
						if (spr᧓.ᝧ.TryGetValue(A_0, out num2))
						{
							num = 10;
							continue;
						}
						return result;
					}
					}
					break;
					IL_2DC:
					num = 21;
				}
			}
		}
		IL_9F:
		IL_B1:
		IL_115:
		IL_244:
		IL_257:
		IL_269:
		IL_279:
		IL_28B:
		IL_29E:
		IL_2B0:
		IL_331:
		return result;
		IL_340:
		if (true)
		{
		}
		return result;
	}

	// Token: 0x06001F6D RID: 8045 RVA: 0x0020E150 File Offset: 0x0020D150
	private void ᜁ(XmlReader A_0, sprᦣ A_1)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 16;
			for (;;)
			{
				string localName2;
				switch (num)
				{
				case 0:
					goto IL_34F;
				case 1:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 2;
						continue;
					}
					goto IL_3C2;
				}
				case 2:
					num = 13;
					continue;
				case 3:
					goto IL_3C2;
				case 4:
					goto IL_3C2;
				case 5:
					return;
				case 6:
					goto IL_2CC;
				case 7:
					goto IL_3C2;
				case 8:
					goto IL_2CC;
				case 9:
					num = 25;
					continue;
				case 10:
					return;
				case 11:
					goto IL_3C2;
				case 12:
					goto IL_3C2;
				case 13:
					if (spr᧓.ᝨ == null)
					{
						num = 24;
						continue;
					}
					goto IL_34F;
				case 14:
					num = 1;
					continue;
				case 15:
					goto IL_3BD;
				case 17:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 27;
						continue;
					}
					return;
				case 18:
				{
					float num2;
					A_1.ᜁ(num2);
					num = 7;
					continue;
				}
				case 19:
					goto IL_3C2;
				case 20:
				{
					string localName;
					int num3;
					if (spr᧓.ᝨ.TryGetValue(localName, out num3))
					{
						num = 9;
						continue;
					}
					goto IL_3C2;
				}
				case 21:
					goto IL_3C2;
				case 22:
					if (!(A_0.LocalName != localName2))
					{
						num = 5;
						continue;
					}
					num = 23;
					continue;
				case 23:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 14;
						continue;
					}
					A_0.Read();
					num = 8;
					continue;
				case 24:
					spr᧓.ᝨ = new Dictionary<string, int>(8)
					{
						{
							ClipboardData.b("Ͷ᭸᝺⹼୾햆ﲊ쾌ﾐ요ﺖﺚ", a_),
							0
						},
						{
							ClipboardData.b("Ͷ᭸᝺⹼୾쒆쾌ﾐ요ﺖﺚ", a_),
							1
						},
						{
							ClipboardData.b("Ͷ᭸᝺㹼᩾횄", a_),
							2
						},
						{
							ClipboardData.b("Ͷ᭸᝺㑼ᅾ", a_),
							3
						},
						{
							ClipboardData.b("ᵶ᩸", a_),
							4
						},
						{
							ClipboardData.b("Ͷ᭸᝺㹼᩾좄ﮈ", a_),
							5
						},
						{
							ClipboardData.b("Ͷ᭸᝺㽼ၾ愈", a_),
							6
						},
						{
							ClipboardData.b("Ѷᅸὺ", a_),
							7
						}
					};
					num = 0;
					continue;
				case 25:
				{
					int num3;
					switch (num3)
					{
					case 0:
					{
						string attribute = A_0.GetAttribute(ClipboardData.b("Ŷᡸ᝺", a_), ClipboardData.b("ὶ൸ེർ䕾꺀겂붒杖ﲘ춠얢쪤햦쒨쪪\ud9ac\udcae龰\udcb2잴킶隸첺튼춾ꗀ돂럄꣆꫈껊뻌볎룐뷒닔뫖뗘퇠헢쫤諦裨苪菬", a_));
						A_1.ᜁ(long.Parse(attribute, NumberStyles.Integer, CultureInfo.InvariantCulture));
						num = 3;
						continue;
					}
					case 1:
					{
						string attribute2 = A_0.GetAttribute(ClipboardData.b("Ŷᡸ᝺", a_), ClipboardData.b("ὶ൸ེർ䕾꺀겂붒杖ﲘ춠얢쪤햦쒨쪪\ud9ac\udcae龰\udcb2잴킶隸첺튼춾ꗀ돂럄꣆꫈껊뻌볎룐뷒닔뫖뗘퇠헢쫤諦裨苪菬", a_));
						A_1.ᜀ(long.Parse(attribute2, NumberStyles.Integer, CultureInfo.InvariantCulture));
						num = 4;
						continue;
					}
					case 2:
					{
						float num2 = this.ᜀ(A_0, ClipboardData.b("v", a_), ClipboardData.b("ὶ൸ེർ䕾꺀겂붒杖ﲘ춠얢쪤햦쒨쪪\ud9ac\udcae龰\udcb2잴킶隸첺튼춾ꗀ돂럄꣆꫈껊뻌볎룐뷒닔뫖뗘퇠헢쫤諦裨苪菬", a_));
						num = 29;
						continue;
					}
					case 3:
					{
						string attribute3 = A_0.GetAttribute(ClipboardData.b("v", a_), ClipboardData.b("ὶ൸ེർ䕾꺀겂붒杖ﲘ춠얢쪤햦쒨쪪\ud9ac\udcae龰\udcb2잴킶隸첺튼춾ꗀ돂럄꣆꫈껊뻌볎룐뷒닔뫖뗘퇠헢쫤諦裨苪菬", a_));
						A_1.ᜀ(float.Parse(attribute3, NumberStyles.Float, CultureInfo.InvariantCulture) / 20f);
						num = 21;
						continue;
					}
					case 4:
						A_1.ᜀ(this.\u171D(A_0));
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3BD;
						default:
							if (false)
							{
							}
							num = 26;
							continue;
						}
						break;
					case 5:
						this.ᜀ(A_0, A_1.ᜈ());
						num = 11;
						continue;
					case 6:
						this.ᜀ(A_0, A_1.ᜁ());
						num = 12;
						continue;
					case 7:
						if (true)
						{
						}
						this.ᜀ(A_0, A_1);
						num = 19;
						continue;
					default:
						num = 15;
						continue;
					}
					break;
				}
				case 26:
					goto IL_3C2;
				case 27:
					num = 22;
					continue;
				case 28:
					goto IL_3C2;
				case 29:
				{
					float num2;
					if (num2 != 3.4028235E+38f)
					{
						num = 18;
						continue;
					}
					goto IL_3C2;
				}
				case 30:
					goto IL_186;
				case 31:
					goto IL_186;
				}
				if (A_0.IsEmptyElement)
				{
					num = 10;
					continue;
				}
				localName2 = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				num = 30;
				continue;
				IL_186:
				num = 17;
				continue;
				IL_2CC:
				this.ᜀ(A_0);
				num = 31;
				continue;
				IL_34F:
				num = 20;
				continue;
				IL_3BD:
				num = 28;
				continue;
				IL_3C2:
				A_0.Read();
				num = 6;
			}
			return;
		}
		}
	}

	// Token: 0x06001F6E RID: 8046 RVA: 0x0020E694 File Offset: 0x0020D694
	private void ᜀ(XmlReader A_0, spr\u20C7 A_1)
	{
		int a_ = 11;
		int num = 28;
		for (;;)
		{
			string localName2;
			switch (num)
			{
			case 0:
				num = 21;
				continue;
			case 1:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 4;
					continue;
				}
				A_0.Read();
				num = 15;
				continue;
			case 2:
			{
				string localName;
				if (!(localName == ClipboardData.b("հᅲᥴ㑶ᱸ᝺ᅼⱾ", a_)))
				{
					num = 0;
					continue;
				}
				float num2 = this.ᜀ(A_0, ClipboardData.b("ٰ", a_), ClipboardData.b("ᥰݲŴݶ䍸呺剼౾ꎌﮔﮜ펠캢쒤펦\udaa8薪슬\uddae횰鲲슴\ud8b6쮸\udfba춼춾껀ꃂꃄ듆뫈ꋊꏌ꣎볐뿒䀹賠苢賤触", a_));
				num = 29;
				continue;
			}
			case 3:
				return;
			case 4:
				num = 27;
				continue;
			case 5:
			{
				float num2;
				A_1.ᜀ(num2);
				num = 30;
				continue;
			}
			case 6:
				goto IL_320;
			case 7:
				return;
			case 8:
				num = 17;
				continue;
			case 9:
			{
				string localName;
				if (!(localName == ClipboardData.b("ᥰᩲᅴ፶ᱸᕺ", a_)))
				{
					num = 22;
					continue;
				}
				A_1.ᜂ(this.ᜂ(A_0));
				num = 31;
				continue;
			}
			case 10:
				if (!(A_0.LocalName != localName2))
				{
					num = 3;
					continue;
				}
				num = 1;
				continue;
			case 11:
				num = 2;
				continue;
			case 12:
				goto IL_202;
			case 13:
				goto IL_320;
			case 14:
				goto IL_320;
			case 15:
				goto IL_202;
			case 16:
				goto IL_31E;
			case 17:
			{
				string localName;
				if (localName == ClipboardData.b("ተቲ᭴Ͷ⩸୺ᅼᙾ", a_))
				{
					A_1.ᜁ(this.ᜂ(A_0));
					num = 14;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_31E;
				default:
					if (false)
					{
					}
					num = 11;
					continue;
				}
				break;
			}
			case 18:
				goto IL_320;
			case 19:
				num = 10;
				continue;
			case 20:
				goto IL_17A;
			case 21:
			{
				string localName;
				if (!(localName == ClipboardData.b("᭰ၲ", a_)))
				{
					num = 16;
					continue;
				}
				A_1.ᜀ(this.\u171D(A_0));
				num = 18;
				continue;
			}
			case 22:
				num = 25;
				continue;
			case 23:
				num = 9;
				continue;
			case 24:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 19;
					continue;
				}
				return;
			case 25:
			{
				string localName;
				if (!(localName == ClipboardData.b("հᅲᥴ㽶ᱸ᩺᥼᩾", a_)))
				{
					num = 8;
					continue;
				}
				A_1.ᜀ(this.ᜂ(A_0));
				num = 6;
				continue;
			}
			case 26:
				goto IL_17A;
			case 27:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 23;
					continue;
				}
				goto IL_320;
			}
			case 29:
			{
				float num2;
				if (num2 != 3.4028235E+38f)
				{
					num = 5;
					continue;
				}
				goto IL_320;
			}
			case 30:
				goto IL_320;
			case 31:
				goto IL_320;
			}
			if (A_0.IsEmptyElement)
			{
				num = 7;
				continue;
			}
			localName2 = A_0.LocalName;
			A_0.Read();
			this.ᜀ(A_0);
			num = 20;
			continue;
			IL_17A:
			if (true)
			{
			}
			num = 24;
			continue;
			IL_202:
			this.ᜀ(A_0);
			num = 26;
			continue;
			IL_31E:
			num = 13;
			continue;
			IL_320:
			A_0.Read();
			num = 12;
		}
	}

	// Token: 0x06001F6F RID: 8047 RVA: 0x0020EA70 File Offset: 0x0020DA70
	private void ᜁ(XmlReader A_0, spr\u2021 A_1)
	{
		int a_ = 11;
		int num = 6;
		for (;;)
		{
			string localName2;
			switch (num)
			{
			case 0:
				num = 27;
				continue;
			case 1:
				goto IL_2FA;
			case 2:
				num = 21;
				continue;
			case 3:
				goto IL_19F;
			case 4:
			{
				string localName;
				if (!(localName == ClipboardData.b("հၲ㝴ᡶ୸ὺ᡼ൾ", a_)))
				{
					num = 17;
					continue;
				}
				this.ᜀ(A_0, A_1.ᜁ());
				if (true)
				{
				}
				num = 16;
				continue;
			}
			case 5:
				num = 11;
				continue;
			case 7:
				num = 18;
				continue;
			case 8:
			{
				string localName;
				if (!(localName == ClipboardData.b("ὰᱲ≴նᡸ୺", a_)))
				{
					num = 0;
					continue;
				}
				A_1.ᜀ(!this.ᜂ(A_0));
				num = 10;
				continue;
			}
			case 9:
				goto IL_1D5;
			case 10:
				goto IL_2FA;
			case 11:
			{
				string localName;
				if (!(localName == ClipboardData.b("հၲ㡴ᙶ୸", a_)))
				{
					num = 12;
					continue;
				}
				this.ᜀ(A_0, A_1.ᜅ());
				num = 20;
				continue;
			}
			case 12:
				num = 4;
				continue;
			case 13:
				goto IL_16F;
			case 14:
				return;
			case 15:
				goto IL_2FA;
			case 16:
				goto IL_2FA;
			case 17:
				num = 24;
				continue;
			case 18:
				if (!(A_0.LocalName != localName2))
				{
					num = 14;
					continue;
				}
				num = 23;
				continue;
			case 19:
				goto IL_1D5;
			case 20:
				goto IL_2FA;
			case 21:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 25;
					continue;
				}
				goto IL_2FA;
			}
			case 22:
				num = 3;
				continue;
			case 23:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 2;
					continue;
				}
				A_0.Read();
				num = 9;
				continue;
			case 24:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_19F;
				default:
				{
					if (false)
					{
					}
					string localName;
					if (!(localName == ClipboardData.b("ɰ᭲ᅴ", a_)))
					{
						num = 22;
						continue;
					}
					this.ᜀ(A_0, A_1);
					num = 1;
					continue;
				}
				}
				break;
			case 25:
				num = 8;
				continue;
			case 26:
				goto IL_16F;
			case 27:
			{
				string localName;
				if (!(localName == ClipboardData.b("ݰ㉲ᥴṶṸᕺ", a_)))
				{
					num = 5;
					continue;
				}
				A_1.ᜀ(this.\u171E(A_0));
				num = 15;
				continue;
			}
			case 28:
				return;
			case 29:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 7;
					continue;
				}
				return;
			}
			if (A_0.IsEmptyElement)
			{
				num = 28;
				continue;
			}
			localName2 = A_0.LocalName;
			A_0.Read();
			this.ᜀ(A_0);
			num = 13;
			continue;
			IL_16F:
			num = 29;
			continue;
			IL_1D5:
			this.ᜀ(A_0);
			num = 26;
			continue;
			IL_2FA:
			A_0.Read();
			num = 19;
			continue;
			IL_19F:
			goto IL_2FA;
		}
	}

	// Token: 0x06001F70 RID: 8048 RVA: 0x0020EDF4 File Offset: 0x0020DDF4
	private void ᜀ(XmlReader A_0, sprᦣ A_1)
	{
		int a_ = 12;
		for (;;)
		{
			IL_4D:
			string attribute = A_0.GetAttribute(ClipboardData.b("ᑱᵳ᩵ᑷ", a_), ClipboardData.b("ᩱsɵࡷ䁹卻兽ﾋꂍﾏ쾟킡즣장\udca7\ud9a9芫솭슯햱鮳솵ힷ좹\ud8bb캽늿귁ꟃꏅ믇막ꗋꃍ럏뿑룓崙쿟迡藣迥蛧", a_));
			for (;;)
			{
				IL_70:
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
						A_1.ᜀ(Color.Empty);
						num = 12;
						continue;
					case 1:
						if (attribute == ClipboardData.b("፱ųɵ᝷", a_))
						{
							num = 0;
							continue;
						}
						A_1.ᜀ(this.ᜃ(attribute));
						num = 6;
						continue;
					case 2:
						return;
					case 3:
						if (true)
						{
						}
						A_1.ᜁ(Color.Empty);
						num = 14;
						continue;
					case 4:
						if (attribute != null)
						{
							num = 10;
							continue;
						}
						return;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_70;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					case 6:
						goto IL_117;
					case 7:
						if (attribute == ClipboardData.b("፱ųɵ᝷", a_))
						{
							num = 3;
							continue;
						}
						A_1.ᜁ(this.ᜃ(attribute));
						num = 8;
						continue;
					case 8:
						goto IL_1C9;
					case 9:
						num = 1;
						continue;
					case 10:
						A_1.ᜀ(this.ᜉ(attribute));
						num = 2;
						continue;
					case 11:
						if (attribute != null)
						{
							num = 5;
							continue;
						}
						goto IL_1C9;
					case 12:
						goto IL_117;
					case 13:
						if (attribute != null)
						{
							num = 9;
							continue;
						}
						goto IL_117;
					case 14:
						goto IL_1C9;
					}
					goto IL_4D;
					IL_117:
					attribute = A_0.GetAttribute(ClipboardData.b("ѱᕳ᩵", a_), ClipboardData.b("ᩱsɵࡷ䁹卻兽ﾋꂍﾏ쾟킡즣장\udca7\ud9a9芫솭슯햱鮳솵ힷ좹\ud8bb캽늿귁ꟃꏅ믇막ꗋꃍ럏뿑룓崙쿟迡藣迥蛧", a_));
					num = 4;
					continue;
					IL_1C9:
					attribute = A_0.GetAttribute(ClipboardData.b("ᅱ᭳᩵᝷ࡹ", a_), ClipboardData.b("ᩱsɵࡷ䁹卻兽ﾋꂍﾏ쾟킡즣장\udca7\ud9a9芫솭슯햱鮳솵ힷ좹\ud8bb캽늿귁ꟃꏅ믇막ꗋꃍ럏뿑룓崙쿟迡藣迥蛧", a_));
					num = 13;
				}
			}
		}
	}

	// Token: 0x06001F71 RID: 8049 RVA: 0x0020F028 File Offset: 0x0020E028
	private void ᜀ(XmlReader A_0, spr\u2021 A_1)
	{
		int a_ = 18;
		for (;;)
		{
			IL_4D:
			string attribute = A_0.GetAttribute(ClipboardData.b("ṷ፹ၻች", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
			for (;;)
			{
				IL_70:
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
							goto IL_70;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 1:
						if (attribute != null)
						{
							num = 0;
							continue;
						}
						goto IL_1C9;
					case 2:
						goto IL_1C9;
					case 3:
						A_1.ᜁ(Color.Empty);
						num = 2;
						continue;
					case 4:
						goto IL_1C9;
					case 5:
						if (attribute != null)
						{
							num = 12;
							continue;
						}
						goto IL_11F;
					case 6:
						if (attribute == ClipboardData.b("᥷ཹࡻᅽ", a_))
						{
							num = 14;
							continue;
						}
						A_1.ᜀ(this.ᜃ(attribute));
						num = 11;
						continue;
					case 7:
						A_1.ᜀ(this.ᜉ(attribute));
						num = 13;
						continue;
					case 8:
						if (attribute == ClipboardData.b("᥷ཹࡻᅽ", a_))
						{
							num = 3;
							continue;
						}
						A_1.ᜁ(this.ᜃ(attribute));
						num = 4;
						continue;
					case 9:
						if (attribute != null)
						{
							num = 7;
							continue;
						}
						return;
					case 10:
						goto IL_11F;
					case 11:
						goto IL_11F;
					case 12:
						num = 6;
						continue;
					case 13:
						return;
					case 14:
						A_1.ᜀ(Color.Empty);
						num = 10;
						continue;
					}
					goto IL_4D;
					IL_11F:
					attribute = A_0.GetAttribute(ClipboardData.b("๷᭹ၻ", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
					num = 9;
					continue;
					IL_1C9:
					attribute = A_0.GetAttribute(ClipboardData.b("᭷ᕹၻᅽ", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
					num = 5;
				}
			}
		}
	}

	// Token: 0x06001F72 RID: 8050 RVA: 0x0020F25C File Offset: 0x0020E25C
	private void ᜋ(XmlReader A_0, CharacterFormat A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = false;
				int num = 34;
				for (;;)
				{
					switch (num)
					{
					case 0:
						A_1.XmlProps2010.Add(this.ᜢ(A_0));
						flag = true;
						num = 47;
						continue;
					case 1:
						goto IL_FD0;
					case 2:
						goto IL_748;
					case 3:
						goto IL_748;
					case 4:
						goto IL_748;
					case 5:
						goto IL_748;
					case 6:
					{
						string attribute;
						A_1.CharStyleName = this.ᜈ()[attribute];
						num = 78;
						continue;
					}
					case 7:
						A_1.ComplexScript = this.ᜂ(A_0);
						this.ᜀ(A_1, 99, A_1.ComplexScript);
						num = 80;
						continue;
					case 8:
					{
						float num2;
						if (num2 != 3.4028235E+38f)
						{
							num = 18;
							continue;
						}
						goto IL_748;
					}
					case 9:
						num = 35;
						continue;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DF2;
						default:
						{
							if (false)
							{
							}
							string attribute2;
							if (attribute2 == ClipboardData.b("ᕳ͵౷ᕹ", a_))
							{
								num = 22;
								continue;
							}
							num = 44;
							continue;
						}
						}
						break;
					case 11:
					{
						string localName;
						int num3;
						if (spr᧓.ᝩ.TryGetValue(localName, out num3))
						{
							num = 20;
							continue;
						}
						goto IL_453;
					}
					case 12:
						if (!flag)
						{
							num = 13;
							continue;
						}
						goto IL_36C;
					case 13:
						A_0.Read();
						num = 33;
						continue;
					case 14:
						goto IL_748;
					case 15:
						if (!(A_0.LocalName != ClipboardData.b("ٳ♵੷", a_)))
						{
							num = 21;
							continue;
						}
						flag = false;
						num = 55;
						continue;
					case 16:
						if (A_0.LocalName != string.Empty)
						{
							num = 50;
							continue;
						}
						goto IL_748;
					case 17:
						goto IL_FD0;
					case 18:
					{
						float num2;
						A_1.Position = num2;
						num = 45;
						continue;
					}
					case 19:
						num = 38;
						continue;
					case 20:
						num = 75;
						continue;
					case 21:
						goto IL_1006;
					case 22:
						A_1.TextColor = Color.Empty;
						num = 40;
						continue;
					case 23:
					{
						string attribute3;
						A_1.FontSizeBidi = float.Parse(attribute3, NumberStyles.Number, CultureInfo.InvariantCulture) / 2f;
						num = 59;
						continue;
					}
					case 24:
						if (spr᧓.ᝩ == null)
						{
							num = 43;
							continue;
						}
						goto IL_3F7;
					case 25:
						goto IL_748;
					case 26:
						goto IL_73B;
					case 27:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 7;
							continue;
						}
						goto IL_748;
					case 28:
						goto IL_748;
					case 29:
						goto IL_748;
					case 30:
						goto IL_748;
					case 31:
						goto IL_748;
					case 32:
						goto IL_748;
					case 33:
						goto IL_36C;
					case 34:
						if (A_0.LocalName != ClipboardData.b("ٳ♵੷", a_))
						{
							num = 79;
							continue;
						}
						num = 83;
						continue;
					case 35:
						goto IL_453;
					case 36:
						goto IL_748;
					case 37:
						goto IL_748;
					case 38:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 48;
							continue;
						}
						goto IL_453;
					}
					case 39:
						goto IL_748;
					case 40:
						goto IL_748;
					case 41:
						goto IL_748;
					case 42:
					{
						string attribute;
						if (this.ᜈ().ContainsKey(attribute))
						{
							num = 6;
							continue;
						}
						goto IL_748;
					}
					case 43:
						spr᧓.ᝩ = new Dictionary<string, int>(38)
						{
							{
								ClipboardData.b("ٳふ᝷ᑹࡻൽ", a_),
								0
							},
							{
								ClipboardData.b("ᝳյ", a_),
								1
							},
							{
								ClipboardData.b("ݳ౵", a_),
								2
							},
							{
								ClipboardData.b("ݳ౵㭷ॹ", a_),
								3
							},
							{
								ClipboardData.b("ᡳ᝵ᙷᵹ", a_),
								4
							},
							{
								ClipboardData.b("ų", a_),
								5
							},
							{
								ClipboardData.b("ɳ፵੷๹㵻ች", a_),
								6
							},
							{
								ClipboardData.b("ᝳ᥵ᑷᕹ๻", a_),
								7
							},
							{
								ClipboardData.b("ᱳήίቹၻ᝽", a_),
								8
							},
							{
								ClipboardData.b("᭳͵౷ᙹᕻၽ", a_),
								9
							},
							{
								ClipboardData.b("ѳ᥵୷፹ࡻ᝽", a_),
								10
							},
							{
								ClipboardData.b("ݳٵ᥷᥹ᕻၽ", a_),
								11
							},
							{
								ClipboardData.b("ٳ╵౷͹ၻ᭽", a_),
								12
							},
							{
								ClipboardData.b("ݳṵᱷ", a_),
								13
							},
							{
								ClipboardData.b("ᙳት੷", a_),
								14
							},
							{
								ClipboardData.b("ٳ♵੷㥹ᑻώ", a_),
								15
							},
							{
								ClipboardData.b("ᙳ", a_),
								16
							},
							{
								ClipboardData.b("ᙳ㕵୷", a_),
								17
							},
							{
								ClipboardData.b("ᝳ᝵ࡷॹ", a_),
								18
							},
							{
								ClipboardData.b("ၳյ౷ࡹᕻᕽ", a_),
								19
							},
							{
								ClipboardData.b("ɳ᝵ᙷ፹ཻᙽ", a_),
								20
							},
							{
								ClipboardData.b("ݳ᭵᥷ᙹၻ㵽", a_),
								21
							},
							{
								ClipboardData.b("ᵳ᭵ࡷࡹᕻၽ", a_),
								22
							},
							{
								ClipboardData.b("ᅳ᭵᩷ᕹཻൽ", a_),
								23
							},
							{
								ClipboardData.b("ᵳ", a_),
								24
							},
							{
								ClipboardData.b("ᵳ㕵୷", a_),
								25
							},
							{
								ClipboardData.b("ݳɵ੷፹᝻᭽", a_),
								26
							},
							{
								ClipboardData.b("ݳṵ᥷ṹ፻ॽ", a_),
								27
							},
							{
								ClipboardData.b("ᩳ᥵⡷ࡹ፻ᅽ", a_),
								28
							},
							{
								ClipboardData.b("ͳ፵᩷㉹ᕻ᩽", a_),
								29
							},
							{
								ClipboardData.b("ٳɵᑷ", a_),
								30
							},
							{
								ClipboardData.b("ၳ፵ᑷ", a_),
								31
							},
							{
								ClipboardData.b("ᵳᡵ୷", a_),
								32
							},
							{
								ClipboardData.b("ᝳᡵ౷ɹࡻ㽽", a_),
								33
							},
							{
								ClipboardData.b("ᡳήί᭹ࡻ୽", a_),
								34
							},
							{
								ClipboardData.b("ᩳ͵ᕷ㱹፻౽", a_),
								35
							},
							{
								ClipboardData.b("ᩳ͵ᕷ⥹౻ώ", a_),
								36
							},
							{
								ClipboardData.b("ݳɵŷᙹᕻൽ햅ﺉﾋ", a_),
								37
							}
						};
						num = 53;
						continue;
					case 44:
					{
						string attribute2;
						if (attribute2 != null)
						{
							num = 81;
							continue;
						}
						goto IL_748;
					}
					case 45:
						goto IL_748;
					case 46:
						goto IL_748;
					case 47:
						goto IL_748;
					case 48:
						num = 24;
						continue;
					case 49:
						goto IL_36C;
					case 50:
						num = 68;
						continue;
					case 51:
						goto IL_748;
					case 52:
						goto IL_748;
					case 53:
						goto IL_3F7;
					case 54:
						goto IL_748;
					case 55:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 19;
							continue;
						}
						A_0.Read();
						num = 49;
						continue;
					case 56:
					{
						string attribute4;
						if (attribute4 != null)
						{
							num = 70;
							continue;
						}
						goto IL_748;
					}
					case 57:
						goto IL_748;
					case 58:
						goto IL_748;
					case 59:
						goto IL_748;
					case 60:
						goto IL_748;
					case 61:
						goto IL_748;
					case 62:
						goto IL_748;
					case 63:
						goto IL_748;
					case 64:
					{
						string attribute3;
						if (attribute3 != null)
						{
							num = 23;
							continue;
						}
						goto IL_748;
					}
					case 65:
						goto IL_748;
					case 66:
						return;
					case 67:
						if (A_0.IsEmptyElement)
						{
							num = 66;
							continue;
						}
						A_0.Read();
						this.ᜀ(A_0);
						num = 1;
						continue;
					case 68:
						if (A_0.LocalName != ClipboardData.b("ٳ♵੷", a_))
						{
							num = 0;
							continue;
						}
						goto IL_748;
					case 69:
						goto IL_748;
					case 70:
					{
						string attribute4;
						A_1.FontSize = float.Parse(attribute4, NumberStyles.Number, CultureInfo.InvariantCulture) / 2f;
						num = 65;
						continue;
					}
					case 71:
						goto IL_748;
					case 72:
						goto IL_748;
					case 73:
						goto IL_748;
					case 74:
						goto IL_748;
					case 75:
					{
						int num3;
						switch (num3)
						{
						case 0:
							this.ᜂ(A_0, A_1);
							num = 71;
							continue;
						case 1:
							num = 27;
							continue;
						case 2:
						{
							string attribute4 = A_0.GetAttribute(ClipboardData.b("ɳ᝵ᑷ", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
							num = 56;
							continue;
						}
						case 3:
						{
							string attribute3 = A_0.GetAttribute(ClipboardData.b("ɳ᝵ᑷ", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
							num = 64;
							continue;
						}
						case 4:
							this.ᜁ(A_0, A_1);
							num = 52;
							continue;
						case 5:
							this.ᜃ(A_0, A_1);
							num = 37;
							continue;
						case 6:
							this.ᜄ(A_0, A_1);
							num = 41;
							continue;
						case 7:
						{
							string attribute2 = A_0.GetAttribute(ClipboardData.b("ɳ᝵ᑷ", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
							num = 10;
							continue;
						}
						case 8:
							this.ᜀ(A_0, A_1);
							num = 14;
							continue;
						case 9:
							A_1.IsOutLine = this.ᜂ(A_0);
							this.ᜀ(A_1, 71, A_1.IsOutLine);
							num = 5;
							continue;
						case 10:
						{
							float num2 = this.ᜀ(A_0, ClipboardData.b("ɳ᝵ᑷ", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
							num = 8;
							continue;
						}
						case 11:
						{
							float num4 = this.ᜀ(A_0, ClipboardData.b("ɳ᝵ᑷ", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
							num = 85;
							continue;
						}
						case 12:
						{
							string attribute = A_0.GetAttribute(ClipboardData.b("ɳ᝵ᑷ", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
							num = 42;
							continue;
						}
						case 13:
							this.ᜅ(A_0, A_1);
							num = 63;
							continue;
						case 14:
							this.ᜀ(A_0, A_1.Border);
							num = 30;
							continue;
						case 15:
							goto IL_DF2;
						case 16:
							A_1.Bold = this.ᜂ(A_0);
							this.ᜀ(A_1, 4, A_1.Bold);
							num = 51;
							continue;
						case 17:
							A_1.BoldBidi = this.ᜂ(A_0);
							this.ᜀ(A_1, 59, A_1.BoldBidi);
							num = 36;
							continue;
						case 18:
							A_1.AllCaps = this.ᜂ(A_0);
							this.ᜀ(A_1, 54, A_1.AllCaps);
							num = 25;
							continue;
						case 19:
							A_1.DoubleStrike = this.ᜂ(A_0);
							this.ᜀ(A_1, 14, A_1.DoubleStrike);
							num = 39;
							continue;
						case 20:
							A_1.Hidden = this.ᜂ(A_0);
							this.ᜀ(A_1, 53, A_1.Hidden);
							num = 3;
							continue;
						case 21:
							A_1.IsSmallCaps = this.ᜂ(A_0);
							this.ᜀ(A_1, 55, A_1.IsSmallCaps);
							num = 32;
							continue;
						case 22:
							A_1.Engrave = this.ᜂ(A_0);
							this.ᜀ(A_1, 52, A_1.Engrave);
							num = 29;
							continue;
						case 23:
							A_1.Emboss = this.ᜂ(A_0);
							this.ᜀ(A_1, 51, A_1.Emboss);
							num = 28;
							continue;
						case 24:
							A_1.Italic = this.ᜂ(A_0);
							this.ᜀ(A_1, 5, A_1.Italic);
							num = 62;
							continue;
						case 25:
							A_1.ItalicBidi = this.ᜂ(A_0);
							this.ᜀ(A_1, 60, A_1.ItalicBidi);
							num = 73;
							continue;
						case 26:
							A_1.IsStrikeout = this.ᜂ(A_0);
							this.ᜀ(A_1, 6, A_1.IsStrikeout);
							num = 57;
							continue;
						case 27:
							A_1.IsShadow = this.ᜂ(A_0);
							this.ᜀ(A_1, 50, A_1.IsShadow);
							num = 54;
							continue;
						case 28:
							A_1.IsNoProof = this.ᜂ(A_0);
							this.ᜀ(A_1, 79, A_1.IsNoProof);
							num = 4;
							continue;
						case 29:
							A_1.IsWebHidden = this.ᜂ(A_0);
							this.ᜀ(A_1, 125, A_1.IsWebHidden);
							num = 31;
							continue;
						case 30:
							A_1.Bidi = this.ᜂ(A_0);
							this.ᜀ(A_1, 58, A_1.Bidi);
							num = 74;
							continue;
						case 31:
							A_1.IsDeleteRevision = true;
							num = 77;
							continue;
						case 32:
							A_1.IsInsertRevision = true;
							num = 58;
							continue;
						case 33:
							A_1.AllowContextualAlternates = this.ᜀ(A_0, ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏ﾑﶓ욟횡誣얥잧잩莫솭횯풱\uddb3햵\uddb7閹쮻톽늿ꛁ﯉ﳋ꟏뷑ꛓ닕뗗뛙", a_));
							num = 82;
							continue;
						case 34:
							this.ᜊ(A_0, A_1);
							num = 2;
							continue;
						case 35:
							this.ᜉ(A_0, A_1);
							num = 46;
							continue;
						case 36:
							this.ᜈ(A_0, A_1);
							num = 76;
							continue;
						case 37:
							this.ᜇ(A_0, A_1);
							num = 72;
							continue;
						default:
							num = 9;
							continue;
						}
						break;
					}
					case 76:
						goto IL_748;
					case 77:
						goto IL_748;
					case 78:
						goto IL_748;
					case 79:
						goto IL_1B3;
					case 80:
						goto IL_748;
					case 81:
					{
						string attribute2;
						A_1.TextColor = this.ᜃ(attribute2);
						num = 69;
						continue;
					}
					case 82:
						goto IL_748;
					case 83:
						if (A_1 == null)
						{
							num = 26;
							continue;
						}
						num = 67;
						continue;
					case 84:
					{
						float num4;
						A_1.CharacterSpacing = num4;
						num = 60;
						continue;
					}
					case 85:
					{
						float num4;
						if (num4 != 3.4028235E+38f)
						{
							num = 84;
							continue;
						}
						goto IL_748;
					}
					}
					break;
					IL_36C:
					this.ᜀ(A_0);
					num = 17;
					continue;
					IL_3F7:
					num = 11;
					continue;
					IL_453:
					num = 16;
					continue;
					IL_748:
					num = 12;
					continue;
					IL_DF2:
					this.ᜆ(A_0, A_1);
					num = 61;
					continue;
					IL_FD0:
					num = 15;
				}
			}
			IL_1B3:
			throw new XmlException(ClipboardData.b("♳͵ᙷ婹౻౽ﲇﶍ", a_));
			IL_73B:
			if (true)
			{
			}
			throw new ArgumentException(ClipboardData.b("㝳ṵ᥷ࡹᵻᵽꚅ캇ﺋ", a_));
			IL_1006:
			this.ᜀ(A_1);
			return;
		}
	}

	// Token: 0x06001F73 RID: 8051 RVA: 0x00210278 File Offset: 0x0020F278
	private void ᜊ(XmlReader A_0, CharacterFormat A_1)
	{
		int a_ = 3;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("Ὠ੪Ŭ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄﾌﲒ래躠첢쎤솦삨좪좬肮우\udcb2잴펶隸覺趼躾닄꣆믈꿊ꃌꏎ", a_));
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 6;
					continue;
				case 1:
					goto IL_127;
				case 2:
					return;
				case 3:
				{
					string key;
					int num2;
					if (spr᧓.ᝪ.TryGetValue(key, out num2))
					{
						num = 0;
						continue;
					}
					return;
				}
				case 4:
				{
					string key;
					if ((key = attribute) != null)
					{
						goto IL_6C;
					}
					return;
				}
				case 5:
					num = 7;
					continue;
				case 6:
				{
					int num2;
					switch (num2)
					{
					case 0:
						goto IL_DD;
					case 1:
						goto IL_1BC;
					case 2:
						goto IL_12C;
					case 3:
						goto IL_1C4;
					case 4:
						goto IL_1DD;
					case 5:
						goto IL_D5;
					case 6:
						goto IL_82;
					case 7:
						goto IL_362;
					case 8:
						goto IL_1D5;
					case 9:
						goto IL_79;
					case 10:
						goto IL_C3;
					case 11:
						goto IL_1CC;
					case 12:
						goto IL_8B;
					case 13:
						goto IL_EF;
					case 14:
						goto IL_CC;
					case 15:
						A_1.LigaturesType = LigatureType.All;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6C;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					default:
						num = 2;
						continue;
					}
					break;
				}
				case 7:
					if (spr᧓.ᝪ == null)
					{
						num = 8;
						continue;
					}
					goto IL_94;
				case 8:
					spr᧓.ᝪ = new Dictionary<string, int>(16)
					{
						{
							ClipboardData.b("ݨѪͬ੮", a_),
							0
						},
						{
							ClipboardData.b("ᩨὪ౬Ůᕰቲݴ፶", a_),
							1
						},
						{
							ClipboardData.b("੨Ѫͬ᭮ᑰ୲Ŵɶᡸ᝺", a_),
							2
						},
						{
							ClipboardData.b("ᩨὪ౬Ůᕰቲݴ፶㩸ᑺ፼୾ﮂ", a_),
							3
						},
						{
							ClipboardData.b("ŨɪṬ᭮ṰŲᱴᑶᡸ᝺", a_),
							4
						},
						{
							ClipboardData.b("ᩨὪ౬Ůᕰቲݴ፶ㅸቺ๼୾", a_),
							5
						},
						{
							ClipboardData.b("੨Ѫͬ᭮ᑰ୲Ŵɶᡸ᝺㕼ᙾ", a_),
							6
						},
						{
							ClipboardData.b("ᩨὪ౬Ůᕰቲݴ፶㩸ᑺ፼୾ﮂ얌杖ﲜ", a_),
							7
						},
						{
							ClipboardData.b("൨ɪṬ౮ͰᙲŴṶᙸᕺᱼ፾", a_),
							8
						},
						{
							ClipboardData.b("ᩨὪ౬Ůᕰቲݴ፶㵸ቺ๼᱾", a_),
							9
						},
						{
							ClipboardData.b("੨Ѫͬ᭮ᑰ୲Ŵɶᡸ᝺㥼ᙾﶈﾒ", a_),
							10
						},
						{
							ClipboardData.b("ᩨὪ౬Ůᕰቲݴ፶㩸ᑺ፼୾ﮂ즌삠쾢", a_),
							11
						},
						{
							ClipboardData.b("ŨɪṬ᭮ṰŲᱴᑶᡸ᝺㥼ᙾﶈﾒ", a_),
							12
						},
						{
							ClipboardData.b("ᩨὪ౬Ůᕰቲݴ፶ㅸቺ๼୾즌삠쾢", a_),
							13
						},
						{
							ClipboardData.b("੨Ѫͬ᭮ᑰ୲Ŵɶᡸ᝺㕼ᙾ햐朗ﺚ캠춢쒤쮦", a_),
							14
						},
						{
							ClipboardData.b("ࡨݪŬ", a_),
							15
						}
					};
					if (true)
					{
					}
					num = 9;
					continue;
				case 9:
					goto IL_94;
				}
				break;
				IL_6C:
				num = 5;
				continue;
				IL_94:
				num = 3;
			}
		}
		IL_79:
		A_1.LigaturesType = LigatureType.DefaultDiscretional;
		return;
		IL_82:
		A_1.LigaturesType = LigatureType.ContextualHistorical;
		return;
		IL_8B:
		A_1.LigaturesType = LigatureType.HistoricalDiscretional;
		return;
		IL_C3:
		A_1.LigaturesType = LigatureType.ContextualDiscretional;
		return;
		IL_CC:
		A_1.LigaturesType = LigatureType.ContextualHistoricalDiscretional;
		return;
		IL_D5:
		A_1.LigaturesType = LigatureType.DefaultHistorical;
		return;
		IL_DD:
		A_1.LigaturesType = LigatureType.None;
		return;
		IL_EF:
		A_1.LigaturesType = LigatureType.DefaultHistoricalDiscretional;
		return;
		IL_127:
		return;
		IL_12C:
		A_1.LigaturesType = LigatureType.Contextual;
		return;
		IL_1BC:
		A_1.LigaturesType = LigatureType.Standard;
		return;
		IL_1C4:
		A_1.LigaturesType = LigatureType.DefaultContextual;
		return;
		IL_1CC:
		A_1.LigaturesType = LigatureType.DefaultContextualDiscretional;
		return;
		IL_1D5:
		A_1.LigaturesType = LigatureType.Discretional;
		return;
		IL_1DD:
		A_1.LigaturesType = LigatureType.Historical;
		return;
		IL_362:
		A_1.LigaturesType = LigatureType.DefaultContextualHistorical;
	}

	// Token: 0x06001F74 RID: 8052 RVA: 0x002105F0 File Offset: 0x0020F5F0
	private void ᜉ(XmlReader A_0, CharacterFormat A_1)
	{
		int a_ = 13;
		for (;;)
		{
			if (true)
			{
			}
			string attribute = A_0.GetAttribute(ClipboardData.b("ղᑴ᭶", a_), ClipboardData.b("᭲ŴͶॸ䅺剼偾ﺌꆎﲐ朗咽햠趢욤좦쒨蒪슬즮ힰ\udab2횴튶隸첺튼춾ꗀ﯊룎뻐ꇒ뇔뫖뗘", a_));
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					string a;
					if ((a = attribute) != null)
					{
						num = 7;
						continue;
					}
					return;
				}
				case 1:
					goto IL_DF;
				case 2:
					return;
				case 3:
				{
					string a;
					if (!(a == ClipboardData.b("ὲᱴ᥶ၸᕺ᩼", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_115;
				}
				case 4:
				{
					string a;
					if (!(a == ClipboardData.b("ᝲၴᅶᡸ๺ᅼ୾", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_7A;
				}
				case 5:
					goto IL_11D;
				case 6:
				{
					string a;
					if (!(a == ClipboardData.b("ᱲᥴ፶⩸ེѼ፾", a_)))
					{
						num = 2;
						continue;
					}
					A_1.NumberFormType = NumberFormType.Old;
					num = 1;
					continue;
				}
				case 7:
					num = 4;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11D;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				break;
				IL_11D:
				num = 6;
			}
		}
		IL_7A:
		A_1.NumberFormType = NumberFormType.Default;
		return;
		IL_DF:
		return;
		IL_115:
		A_1.NumberFormType = NumberFormType.Lining;
	}

	// Token: 0x06001F75 RID: 8053 RVA: 0x00210758 File Offset: 0x0020F758
	private void ᜈ(XmlReader A_0, CharacterFormat A_1)
	{
		int a_ = 3;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("Ὠ੪Ŭ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄﾌﲒ래躠첢쎤솦삨좪좬肮우\udcb2잴펶隸覺趼躾닄꣆믈꿊ꃌꏎ", a_));
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11D;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 1:
				{
					string a;
					if (!(a == ClipboardData.b("൨๪୬๮ѰὲŴ", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_72;
				}
				case 2:
					goto IL_DF;
				case 3:
					return;
				case 4:
				{
					string a;
					if (!(a == ClipboardData.b("ᵨ੪ཬᩮᵰቲݴ", a_)))
					{
						num = 3;
						continue;
					}
					A_1.NumberSpaceType = NumberSpaceType.Tabular;
					if (true)
					{
					}
					num = 2;
					continue;
				}
				case 5:
				{
					string a;
					if (!(a == ClipboardData.b("ᥨᥪɬὮṰŲŴṶᙸᕺᱼ፾", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_115;
				}
				case 6:
				{
					string a;
					if ((a = attribute) != null)
					{
						num = 7;
						continue;
					}
					return;
				}
				case 7:
					num = 1;
					continue;
				case 8:
					goto IL_11D;
				}
				break;
				IL_11D:
				num = 4;
			}
		}
		IL_72:
		A_1.NumberSpaceType = NumberSpaceType.Default;
		return;
		IL_DF:
		return;
		IL_115:
		A_1.NumberSpaceType = NumberSpaceType.Proportional;
	}

	// Token: 0x06001F76 RID: 8054 RVA: 0x002108C0 File Offset: 0x0020F8C0
	private void ᜇ(XmlReader A_0, CharacterFormat A_1)
	{
		int a_ = 8;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 13;
					continue;
				}
				goto IL_200;
			}
			case 1:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 19;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_196;
				}
				if (false)
				{
				}
				A_0.Read();
				num = 8;
				continue;
			case 2:
				if (true)
				{
				}
				num = 0;
				continue;
			case 3:
				return;
			case 4:
				goto IL_196;
			case 5:
				if (A_0.IsEmptyElement)
				{
					num = 12;
					continue;
				}
				A_0.Read();
				this.ᜀ(A_0);
				num = 18;
				continue;
			case 6:
				goto IL_200;
			case 7:
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("ݭᑯ", a_), ClipboardData.b("٭ѯٱѳ䱵坷啹ཻᵽﮇꒉﮓﲙ낝쎟춡즣覥잧첩쪫잭펯ힱ鮳솵ힷ좹\ud8bb醽뷉ꏋ볍듏뿑룓", a_));
				A_1.StylisticSetType = this.ᜋ(attribute);
				num = 6;
				continue;
			}
			case 8:
				goto IL_C4;
			case 10:
			{
				string localName;
				if (localName == ClipboardData.b("ᵭѯୱᡳ፵⭷όࡻ", a_))
				{
					num = 7;
					continue;
				}
				goto IL_200;
			}
			case 11:
				goto IL_1FE;
			case 12:
				return;
			case 13:
				num = 10;
				continue;
			case 14:
				goto IL_74;
			case 15:
				if (A_0.LocalName != ClipboardData.b("ᵭѯୱᡳή୷๹ᕻᵽ퍿", a_))
				{
					num = 11;
					continue;
				}
				num = 5;
				continue;
			case 16:
				if (!(A_0.LocalName != ClipboardData.b("ᵭѯୱᡳή୷๹ᕻᵽ퍿", a_)))
				{
					num = 3;
					continue;
				}
				num = 17;
				continue;
			case 17:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 2;
					continue;
				}
				goto IL_200;
			case 18:
				goto IL_196;
			case 19:
				num = 15;
				continue;
			}
			if (A_0 == null)
			{
				num = 14;
				continue;
			}
			IL_C4:
			num = 1;
			continue;
			IL_196:
			num = 16;
			continue;
			IL_200:
			A_0.Read();
			this.ᜀ(A_0);
			num = 4;
		}
		IL_74:
		throw new Exception(ClipboardData.b("ᱭᕯ፱ၳ፵੷婹ᕻൽꁿ", a_));
		IL_1FE:
		throw new XmlException(ClipboardData.b("⭭࡯ɱᅳᕵ౷ό᡻幽ꚅﲇ꺍늏즟송쎥\udca7\ud9a9身", a_));
	}

	// Token: 0x06001F77 RID: 8055 RVA: 0x00210B60 File Offset: 0x0020FB60
	private StylisticSetType ᜋ(string A_0)
	{
		int a_ = 1;
		StylisticSetType result;
		for (;;)
		{
			IL_90:
			result = StylisticSetType.Default;
			for (;;)
			{
				IL_92:
				int num = 18;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						return result;
					case 2:
						goto IL_100;
					case 3:
						return result;
					case 4:
						return result;
					case 5:
						return result;
					case 6:
						return result;
					case 7:
						return result;
					case 8:
						spr᧓.ᝫ = new Dictionary<string, int>(21)
						{
							{
								ClipboardData.b("坦", a_),
								0
							},
							{
								ClipboardData.b("噦", a_),
								1
							},
							{
								ClipboardData.b("啦", a_),
								2
							},
							{
								ClipboardData.b("呦", a_),
								3
							},
							{
								ClipboardData.b("卦", a_),
								4
							},
							{
								ClipboardData.b("剦", a_),
								5
							},
							{
								ClipboardData.b("兦", a_),
								6
							},
							{
								ClipboardData.b("偦", a_),
								7
							},
							{
								ClipboardData.b("彦", a_),
								8
							},
							{
								ClipboardData.b("幦", a_),
								9
							},
							{
								ClipboardData.b("噦奨", a_),
								10
							},
							{
								ClipboardData.b("噦塨", a_),
								11
							},
							{
								ClipboardData.b("噦孨", a_),
								12
							},
							{
								ClipboardData.b("噦婨", a_),
								13
							},
							{
								ClipboardData.b("噦嵨", a_),
								14
							},
							{
								ClipboardData.b("噦屨", a_),
								15
							},
							{
								ClipboardData.b("噦彨", a_),
								16
							},
							{
								ClipboardData.b("噦幨", a_),
								17
							},
							{
								ClipboardData.b("噦全", a_),
								18
							},
							{
								ClipboardData.b("噦偨", a_),
								19
							},
							{
								ClipboardData.b("啦奨", a_),
								20
							}
						};
						num = 2;
						continue;
					case 9:
						return result;
					case 10:
					{
						int num2;
						if (spr᧓.ᝫ.TryGetValue(A_0, out num2))
						{
							num = 21;
							continue;
						}
						return result;
					}
					case 11:
						return result;
					case 12:
						return result;
					case 13:
						return result;
					case 14:
						return result;
					case 15:
						return result;
					case 16:
						return result;
					case 17:
						return result;
					case 18:
						if (A_0 != null)
						{
							num = 27;
							continue;
						}
						return result;
					case 19:
						return result;
					case 20:
						return result;
					case 21:
						num = 24;
						continue;
					case 22:
						num = 9;
						continue;
					case 23:
						return result;
					case 24:
					{
						int num2;
						switch (num2)
						{
						case 0:
							result = StylisticSetType.Default;
							num = 1;
							continue;
						case 1:
							result = StylisticSetType.StylisticSet01;
							num = 7;
							continue;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_92;
							default:
								if (false)
								{
								}
								result = StylisticSetType.StylisticSet02;
								num = 19;
								continue;
							}
							break;
						case 3:
							result = StylisticSetType.StylisticSet03;
							num = 20;
							continue;
						case 4:
							result = StylisticSetType.StylisticSet04;
							num = 14;
							continue;
						case 5:
							result = StylisticSetType.StylisticSet05;
							num = 25;
							continue;
						case 6:
							result = StylisticSetType.StylisticSet06;
							num = 26;
							continue;
						case 7:
							result = StylisticSetType.StylisticSet07;
							num = 17;
							continue;
						case 8:
							if (true)
							{
							}
							result = StylisticSetType.StylisticSet08;
							num = 30;
							continue;
						case 9:
							result = StylisticSetType.StylisticSet09;
							num = 15;
							continue;
						case 10:
							result = StylisticSetType.StylisticSet10;
							num = 16;
							continue;
						case 11:
							result = StylisticSetType.StylisticSet11;
							num = 13;
							continue;
						case 12:
							result = StylisticSetType.StylisticSet12;
							num = 4;
							continue;
						case 13:
							result = StylisticSetType.StylisticSet13;
							num = 29;
							continue;
						case 14:
							result = StylisticSetType.StylisticSet14;
							num = 12;
							continue;
						case 15:
							result = StylisticSetType.StylisticSet15;
							num = 23;
							continue;
						case 16:
							result = StylisticSetType.StylisticSet16;
							num = 0;
							continue;
						case 17:
							result = StylisticSetType.StylisticSet17;
							num = 11;
							continue;
						case 18:
							result = StylisticSetType.StylisticSet18;
							num = 5;
							continue;
						case 19:
							result = StylisticSetType.StylisticSet19;
							num = 3;
							continue;
						case 20:
							result = StylisticSetType.StylisticSet20;
							num = 6;
							continue;
						default:
							num = 22;
							continue;
						}
						break;
					}
					case 25:
						return result;
					case 26:
						return result;
					case 27:
						num = 28;
						continue;
					case 28:
						if (spr᧓.ᝫ == null)
						{
							num = 8;
							continue;
						}
						goto IL_100;
					case 29:
						return result;
					case 30:
						return result;
					}
					goto IL_90;
					IL_100:
					num = 10;
				}
			}
		}
		return result;
	}

	// Token: 0x06001F78 RID: 8056 RVA: 0x0021108C File Offset: 0x0021008C
	private void ᜀ(CharacterFormat A_0)
	{
		int a_ = 17;
		int num = 20;
		for (;;)
		{
			FontStyle fontStyle;
			string text;
			string text2;
			Font item;
			switch (num)
			{
			case 0:
				num = 5;
				continue;
			case 1:
				fontStyle |= FontStyle.Italic;
				num = 7;
				continue;
			case 2:
				if (A_0.HasValue(4))
				{
					num = 0;
					continue;
				}
				goto IL_1E5;
			case 3:
				fontStyle |= FontStyle.Strikeout;
				goto IL_20D;
			case 4:
				if (A_0.IsStrikeout)
				{
					num = 3;
					continue;
				}
				goto IL_22B;
			case 5:
				if (A_0.Bold)
				{
					num = 30;
					continue;
				}
				goto IL_1E5;
			case 6:
				return;
			case 7:
				goto IL_1BE;
			case 8:
				if (A_0.HasValue(7))
				{
					num = 11;
					continue;
				}
				goto IL_31D;
			case 9:
				if (A_0.HasValue(6))
				{
					num = 13;
					continue;
				}
				goto IL_22B;
			case 10:
				return;
			case 11:
				num = 19;
				continue;
			case 12:
				text = null;
				goto IL_266;
			case 13:
				num = 4;
				continue;
			case 14:
				goto IL_11A;
			case 15:
				if (A_0.Italic)
				{
					num = 1;
					continue;
				}
				goto IL_1BE;
			case 16:
				A_0.Italic = true;
				num = 14;
				continue;
			case 17:
				text = A_0.ᜇ(68);
				goto IL_266;
			case 18:
				fontStyle |= FontStyle.Underline;
				num = 26;
				continue;
			case 19:
				if (A_0.UnderlineStyle != UnderlineStyle.None)
				{
					num = 18;
					continue;
				}
				goto IL_31D;
			case 21:
				goto IL_1E5;
			case 22:
				if (text2 == ClipboardData.b("㩶ᙸᕺቼ୾Ꞇ쪈ﾌﲎ", a_))
				{
					num = 16;
					continue;
				}
				goto IL_11A;
			case 23:
				if (!this.ᜄ.UsedFontNames.Contains(item))
				{
					num = 28;
					continue;
				}
				return;
			case 24:
				goto IL_22B;
			case 25:
				num = 12;
				continue;
			case 26:
				goto IL_31D;
			case 27:
				if (A_0.HasValue(5))
				{
					num = 31;
					continue;
				}
				goto IL_1BE;
			case 28:
				this.ᜄ.UsedFontNames.Add(item);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_20D;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 29:
				if (string.IsNullOrEmpty(text2))
				{
					num = 6;
					continue;
				}
				num = 22;
				continue;
			case 30:
				if (true)
				{
				}
				fontStyle |= FontStyle.Bold;
				num = 21;
				continue;
			case 31:
				num = 15;
				continue;
			}
			if (!A_0.HasValue(68))
			{
				num = 25;
				continue;
			}
			num = 17;
			continue;
			IL_11A:
			fontStyle = FontStyle.Regular;
			num = 2;
			continue;
			IL_1BE:
			num = 8;
			continue;
			IL_1E5:
			num = 27;
			continue;
			IL_20D:
			num = 24;
			continue;
			IL_22B:
			item = spr\u215C.ᜀ(text2, 11f, fontStyle);
			num = 23;
			continue;
			IL_266:
			text2 = text;
			num = 29;
			continue;
			IL_31D:
			num = 9;
		}
	}

	// Token: 0x06001F79 RID: 8057 RVA: 0x002113E0 File Offset: 0x002103E0
	private void ᜀ(FormatBase A_0, short A_1, bool A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				byte b = 0;
				int num = 11;
				for (;;)
				{
					byte b2;
					byte b3;
					byte b4;
					switch (num)
					{
					case 0:
					{
						CharacterFormat characterFormat;
						if (characterFormat != null)
						{
							num = 7;
							continue;
						}
						goto IL_104;
					}
					case 1:
					{
						Style style;
						if (style.CharacterFormat.HasValue((int)A_1))
						{
							num = 26;
							continue;
						}
						goto IL_127;
					}
					case 2:
						b2 = 128;
						goto IL_275;
					case 3:
						b3 = 129;
						goto IL_2CA;
					case 4:
					{
						bool flag;
						if (A_2 != flag)
						{
							num = 23;
							continue;
						}
						num = 2;
						continue;
					}
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F1;
						default:
							if (false)
							{
							}
							num = 22;
							continue;
						}
						break;
					case 6:
						goto IL_297;
					case 7:
						num = 16;
						continue;
					case 8:
					{
						CharacterFormat characterFormat;
						bool flag = (bool)characterFormat.ᜃ((int)A_1);
						num = 4;
						continue;
					}
					case 9:
						goto IL_297;
					case 10:
					{
						Style style;
						if (style != null)
						{
							num = 21;
							continue;
						}
						goto IL_127;
					}
					case 11:
						if (!(A_0.OwnerBase is ListLevel))
						{
							num = 20;
							continue;
						}
						goto IL_297;
					case 12:
						if (true)
						{
						}
						b3 = 128;
						goto IL_2CA;
					case 13:
						num = 3;
						continue;
					case 14:
						b2 = 129;
						goto IL_275;
					case 15:
						if (!A_2)
						{
							num = 5;
							continue;
						}
						num = 19;
						continue;
					case 16:
					{
						CharacterFormat characterFormat;
						if (characterFormat.HasValue((int)A_1))
						{
							num = 8;
							continue;
						}
						goto IL_104;
					}
					case 17:
						goto IL_297;
					case 18:
						A_0.ᜀ(A_1, b);
						num = 27;
						continue;
					case 19:
						b4 = 129;
						goto IL_266;
					case 20:
					{
						CharacterFormat characterFormat2 = A_0 as CharacterFormat;
						Style style = this.ᜄ.Styles.FindByName(characterFormat2.CharStyleName);
						CharacterFormat characterFormat = A_0.BaseFormat as CharacterFormat;
						bool flag = false;
						num = 10;
						continue;
					}
					case 21:
						goto IL_1F1;
					case 22:
						b4 = 128;
						goto IL_266;
					case 23:
						num = 14;
						continue;
					case 24:
					{
						bool flag;
						if (A_2 != flag)
						{
							num = 13;
							continue;
						}
						num = 12;
						continue;
					}
					case 25:
						if (b != 0)
						{
							num = 18;
							continue;
						}
						return;
					case 26:
					{
						Style style;
						bool flag = (bool)style.CharacterFormat.ᜃ((int)A_1);
						num = 24;
						continue;
					}
					case 27:
						return;
					}
					break;
					IL_104:
					num = 15;
					continue;
					IL_127:
					num = 0;
					continue;
					IL_1F1:
					num = 1;
					continue;
					IL_266:
					b = b4;
					num = 6;
					continue;
					IL_275:
					b = b2;
					num = 9;
					continue;
					IL_297:
					num = 25;
					continue;
					IL_2CA:
					b = b3;
					num = 17;
				}
			}
			return;
		}
	}

	// Token: 0x06001F7A RID: 8058 RVA: 0x002116F8 File Offset: 0x002106F8
	private void ᜆ(XmlReader A_0, CharacterFormat A_1)
	{
		A_1.IsChangedFormat = true;
		CharacterFormat a_ = new CharacterFormat(this.ᜄ);
		if (A_0.IsEmptyElement)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			return;
		}
		if (true)
		{
		}
		A_0.Read();
		this.ᜀ(A_0);
		this.ᜋ(A_0, a_);
		spr\u1CC1 spr_u1CC = new spr\u1CC1(10883);
		spr_u1CC.ᜁ(true);
		A_1.Sprms.ᜆ(spr_u1CC);
		A_1.Sprms.ᜂ().Reverse();
	}

	// Token: 0x06001F7B RID: 8059 RVA: 0x00211798 File Offset: 0x00210798
	private void ᜀ(XmlReader A_0, Border A_1)
	{
		int a_ = 18;
		for (;;)
		{
			A_1.IsRead = true;
			string attribute = A_0.GetAttribute(ClipboardData.b("୷y", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
			int num = 9;
			for (;;)
			{
				string attribute2;
				switch (num)
				{
				case 0:
					if (A_1.LineWidth == 0f)
					{
						num = 10;
						continue;
					}
					goto IL_42C;
				case 1:
					goto IL_1F6;
				case 2:
					if (attribute == null)
					{
						goto IL_11F;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_21C;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 3:
					goto IL_D7;
				case 4:
					if (attribute != null)
					{
						num = 29;
						continue;
					}
					goto IL_1F6;
				case 5:
					num = 20;
					continue;
				case 6:
					num = 23;
					continue;
				case 7:
					goto IL_301;
				case 8:
					A_1.BorderType = this.ᜀ(attribute, A_1);
					A_1.IsChanged = true;
					num = 12;
					continue;
				case 9:
					if (attribute != null)
					{
						num = 15;
						continue;
					}
					goto IL_196;
				case 10:
					goto IL_21C;
				case 11:
					num = 0;
					continue;
				case 12:
					goto IL_11F;
				case 13:
					if (attribute != null)
					{
						num = 24;
						continue;
					}
					goto IL_306;
				case 14:
					if (attribute2 != null)
					{
						num = 28;
						continue;
					}
					goto IL_256;
				case 15:
					A_1.LineWidth = float.Parse(attribute, NumberStyles.Float, CultureInfo.InvariantCulture) / 8f;
					A_1.IsChanged = true;
					num = 17;
					continue;
				case 16:
					goto IL_256;
				case 17:
					goto IL_196;
				case 18:
					num = 27;
					continue;
				case 19:
					if (A_1.BorderType == BorderStyle.None)
					{
						num = 18;
						continue;
					}
					goto IL_42C;
				case 20:
					if (!(attribute == ClipboardData.b("䥷", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_D7;
				case 21:
					goto IL_306;
				case 22:
					if (!(attribute == ClipboardData.b("᝷ᑹ", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_D7;
				case 23:
					if (attribute == ClipboardData.b("౷ࡹॻ᭽", a_))
					{
						num = 3;
						continue;
					}
					goto IL_1F6;
				case 24:
					A_1.Space = float.Parse(attribute, NumberStyles.Float, CultureInfo.InvariantCulture);
					num = 21;
					continue;
				case 25:
					if (A_1.Color == Color.Black)
					{
						num = 26;
						continue;
					}
					goto IL_42C;
				case 26:
					A_1.BorderType = BorderStyle.Single;
					A_1.LineWidth = 0.5f;
					num = 7;
					continue;
				case 27:
					if (!A_1.HasNoneStyle)
					{
						num = 11;
						continue;
					}
					goto IL_42C;
				case 28:
					A_1.Color = this.ᜃ(attribute2);
					A_1.IsChanged = true;
					num = 16;
					continue;
				case 29:
					num = 22;
					continue;
				}
				break;
				IL_D7:
				A_1.Shadow = true;
				num = 1;
				continue;
				IL_11F:
				if (true)
				{
				}
				attribute = A_0.GetAttribute(ClipboardData.b("୷੹ᵻᵽ", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
				num = 13;
				continue;
				IL_196:
				attribute = A_0.GetAttribute(ClipboardData.b("๷᭹ၻ", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
				num = 2;
				continue;
				IL_1F6:
				num = 19;
				continue;
				IL_21C:
				num = 25;
				continue;
				IL_256:
				attribute = A_0.GetAttribute(ClipboardData.b("୷ቹᵻ᩽", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
				num = 4;
				continue;
				IL_306:
				attribute2 = A_0.GetAttribute(ClipboardData.b("᭷ᕹၻᅽ", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
				num = 14;
			}
		}
		IL_301:
		IL_42C:
		A_1.IsRead = false;
	}

	// Token: 0x06001F7C RID: 8060 RVA: 0x00211BD8 File Offset: 0x00210BD8
	private void ᜀ(XmlReader A_0, Border A_1, IDocumentObject A_2)
	{
		int a_ = 4;
		for (;;)
		{
			A_1.IsRead = true;
			string attribute = A_0.GetAttribute(ClipboardData.b("ᥩᙫ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫\ud9ad\udfaf삱킳욵쪷햹\udfbb\udbbd뎿뇁귃ꣅ꿇Ꟊꃋ럙뷛럝軟", a_));
			int num = 26;
			for (;;)
			{
				string attribute2;
				switch (num)
				{
				case 0:
					if (attribute == ClipboardData.b("ṩṫ᭭ᕯ", a_))
					{
						num = 19;
						continue;
					}
					goto IL_1EE;
				case 1:
					if (!(attribute == ClipboardData.b("孩", a_)))
					{
						num = 21;
						continue;
					}
					goto IL_D7;
				case 2:
					if (!(attribute == ClipboardData.b("թɫ", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_D7;
				case 3:
					if (attribute2 != null)
					{
						num = 4;
						continue;
					}
					goto IL_256;
				case 4:
					A_1.Color = this.ᜃ(attribute2);
					A_1.IsChanged = true;
					num = 16;
					continue;
				case 5:
					num = 1;
					continue;
				case 6:
					if (!A_1.HasNoneStyle)
					{
						num = 17;
						continue;
					}
					goto IL_432;
				case 7:
					A_1.BorderType = BorderStyle.Single;
					A_1.LineWidth = 0.5f;
					num = 18;
					continue;
				case 8:
					goto IL_21C;
				case 9:
					if (attribute == null)
					{
						goto IL_11F;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_21C;
					default:
						if (false)
						{
						}
						num = 13;
						continue;
					}
					break;
				case 10:
					num = 6;
					continue;
				case 11:
					if (A_1.Color == Color.Black)
					{
						num = 7;
						continue;
					}
					goto IL_432;
				case 12:
					if (attribute != null)
					{
						num = 28;
						continue;
					}
					goto IL_1EE;
				case 13:
					A_1.BorderType = this.ᜀ(attribute, A_1);
					A_1.IsChanged = true;
					num = 29;
					continue;
				case 14:
					A_1.LineWidth = float.Parse(attribute, NumberStyles.Float, CultureInfo.InvariantCulture) / 8f;
					A_1.IsChanged = true;
					num = 15;
					continue;
				case 15:
					goto IL_18E;
				case 16:
					goto IL_256;
				case 17:
					num = 20;
					continue;
				case 18:
					goto IL_307;
				case 19:
					goto IL_D7;
				case 20:
					if (A_1.LineWidth == 0f)
					{
						num = 8;
						continue;
					}
					goto IL_432;
				case 21:
					num = 0;
					continue;
				case 22:
					if (attribute != null)
					{
						num = 24;
						continue;
					}
					goto IL_30C;
				case 23:
					goto IL_30C;
				case 24:
					A_1.Space = float.Parse(attribute, NumberStyles.Float, CultureInfo.InvariantCulture) / 20f;
					num = 23;
					continue;
				case 25:
					goto IL_1EE;
				case 26:
					if (attribute != null)
					{
						num = 14;
						continue;
					}
					goto IL_18E;
				case 27:
					if (true)
					{
					}
					if (A_1.BorderType == BorderStyle.None)
					{
						num = 10;
						continue;
					}
					goto IL_432;
				case 28:
					num = 2;
					continue;
				case 29:
					goto IL_11F;
				}
				break;
				IL_D7:
				A_1.Shadow = true;
				num = 25;
				continue;
				IL_11F:
				attribute = A_0.GetAttribute(ClipboardData.b("ᥩᱫ཭፯᝱", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫\ud9ad\udfaf삱킳욵쪷햹\udfbb\udbbd뎿뇁귃ꣅ꿇Ꟊꃋ럙뷛럝軟", a_));
				num = 22;
				continue;
				IL_18E:
				attribute = A_0.GetAttribute(ClipboardData.b("ᱩ൫ɭ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫\ud9ad\udfaf삱킳욵쪷햹\udfbb\udbbd뎿뇁귃ꣅ꿇Ꟊꃋ럙뷛럝軟", a_));
				num = 9;
				continue;
				IL_1EE:
				num = 27;
				continue;
				IL_21C:
				num = 11;
				continue;
				IL_256:
				attribute = A_0.GetAttribute(ClipboardData.b("ᥩѫ཭ᑯᵱͳ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫\ud9ad\udfaf삱킳욵쪷햹\udfbb\udbbd뎿뇁귃ꣅ꿇Ꟊꃋ럙뷛럝軟", a_));
				num = 12;
				continue;
				IL_30C:
				attribute2 = A_0.GetAttribute(ClipboardData.b("३ͫɭὯq", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫\ud9ad\udfaf삱킳욵쪷햹\udfbb\udbbd뎿뇁귃ꣅ꿇Ꟊꃋ럙뷛럝軟", a_));
				num = 3;
			}
		}
		IL_307:
		IL_432:
		A_1.IsRead = false;
	}

	// Token: 0x06001F7D RID: 8061 RVA: 0x00212020 File Offset: 0x00211020
	private BorderStyle ᜀ(string A_0, Border A_1)
	{
		BorderStyle borderStyle;
		for (;;)
		{
			borderStyle = this.ᜊ(A_0);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return borderStyle;
				case 1:
					A_1.HasNoneStyle = true;
					goto IL_5E;
				case 2:
					if (borderStyle != BorderStyle.None)
					{
						return borderStyle;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5E;
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
				IL_5E:
				if (true)
				{
				}
				num = 0;
			}
		}
		return borderStyle;
	}

	// Token: 0x06001F7E RID: 8062 RVA: 0x002120A0 File Offset: 0x002110A0
	private BorderStyle ᜊ(string A_0)
	{
		int a_ = 17;
		BorderStyle result;
		for (;;)
		{
			result = BorderStyle.None;
			int num = 30;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_27F;
				case 1:
					goto IL_1F1;
				case 2:
					goto IL_23A;
				case 3:
					goto IL_362;
				case 4:
					goto IL_216;
				case 5:
					goto IL_2EF;
				case 6:
					num = 15;
					continue;
				case 7:
					goto IL_314;
				case 8:
					goto IL_1CB;
				case 9:
					if (spr᧓.ᝬ == null)
					{
						if (true)
						{
						}
						num = 31;
						continue;
					}
					goto IL_362;
				case 10:
					goto IL_337;
				case 11:
					num = 9;
					continue;
				case 12:
					goto IL_104;
				case 13:
					goto IL_1DE;
				case 14:
					goto IL_117;
				case 15:
					goto IL_324;
				case 16:
					goto IL_2CA;
				case 17:
					goto IL_292;
				case 18:
					goto IL_2DC;
				case 19:
					goto IL_2B7;
				case 20:
				{
					int num2;
					if (spr᧓.ᝬ.TryGetValue(A_0, out num2))
					{
						num = 23;
						continue;
					}
					return result;
				}
				case 21:
					return result;
				case 22:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E3;
					default:
						goto IL_6AD;
					}
					break;
				case 23:
					num = 37;
					continue;
				case 24:
					goto IL_39F;
				case 25:
					goto IL_2A4;
				case 26:
					goto IL_DE;
				case 27:
					goto IL_F1;
				case 28:
					goto IL_35D;
				case 29:
					goto IL_687;
				case 30:
					if (A_0 != null)
					{
						num = 11;
						continue;
					}
					return result;
				case 31:
					spr᧓.ᝬ = new Dictionary<string, int>(32)
					{
						{
							ClipboardData.b("Ͷ๸ቺ๼୾즄ﺌ뺎", a_),
							0
						},
						{
							ClipboardData.b("Ͷ୸ቺർ፾", a_),
							1
						},
						{
							ClipboardData.b("፶ᡸࡺᕼⱾ캈ﶌ", a_),
							2
						},
						{
							ClipboardData.b("Ѷၸᕺ᩼፾", a_),
							3
						},
						{
							ClipboardData.b("፶ᙸེॼ᩾", a_),
							4
						},
						{
							ClipboardData.b("፶ᙸེ㥼Ṿ", a_),
							5
						},
						{
							ClipboardData.b("፶ᙸེ㥼ၾ잂", a_),
							6
						},
						{
							ClipboardData.b("፶ᡸࡺᕼ᩾", a_),
							7
						},
						{
							ClipboardData.b("፶ᙸ๺ὼ፾", a_),
							8
						},
						{
							ClipboardData.b("Ͷᅸቺ፼⭾", a_),
							9
						},
						{
							ClipboardData.b("ͶᅸቺṼᑾ햀\uda88﶐풒", a_),
							10
						},
						{
							ClipboardData.b("Ͷᅸቺ፼⭾\uda88﶐풒", a_),
							11
						},
						{
							ClipboardData.b("Ͷᅸቺ፼⭾\udd88슐ﺒﮖ\udc9aﲜ", a_),
							12
						},
						{
							ClipboardData.b("ͶᅸቺṼᑾ쎀\udb8e戀朗ﮔ", a_),
							13
						},
						{
							ClipboardData.b("Ͷᅸቺ፼⭾\udd88\udc90ﺖ\uda9cﺞ토", a_),
							14
						},
						{
							ClipboardData.b("ͶᅸቺṼᑾ햀", a_),
							15
						},
						{
							ClipboardData.b("ͶᅸቺṼᑾ햀쒈ﺒ튔", a_),
							16
						},
						{
							ClipboardData.b("Ͷᅸቺ፼⭾", a_),
							17
						},
						{
							ClipboardData.b("Ͷᅸቺ፼⭾쒈ﺒ튔", a_),
							18
						},
						{
							ClipboardData.b("ͶᅸቺṼᑾ햀얈ﾌ풒", a_),
							19
						},
						{
							ClipboardData.b("Ͷᅸቺ፼⭾얈ﾌ풒", a_),
							20
						},
						{
							ClipboardData.b("Ͷᅸቺ፼⭾\udd88\udd90ﲘ\udc9aﲜ", a_),
							21
						},
						{
							ClipboardData.b("ͶᅸቺṼᑾ", a_),
							22
						},
						{
							ClipboardData.b("vᡸൺ᡼", a_),
							23
						},
						{
							ClipboardData.b("፶ᙸ๺ὼ፾풂", a_),
							24
						},
						{
							ClipboardData.b("፶ᡸࡺᕼ㭾횄ﮈ", a_),
							25
						},
						{
							ClipboardData.b("Ͷᅸॺ᡼᩾얀욂ﮈﮌ", a_),
							26
						},
						{
							ClipboardData.b("Ͷᅸॺ᡼᩾얀욂ﺌ", a_),
							27
						},
						{
							ClipboardData.b("ᡶ౸ེ๼᩾", a_),
							28
						},
						{
							ClipboardData.b("Ṷ᝸ࡺ᡼୾", a_),
							29
						},
						{
							ClipboardData.b("᥶ၸ᝺", a_),
							30
						},
						{
							ClipboardData.b("᥶ᙸᕺ᡼", a_),
							31
						}
					};
					num = 3;
					continue;
				case 32:
					goto IL_34A;
				case 33:
					goto IL_203;
				case 34:
					goto IL_302;
				case 35:
					goto IL_228;
				case 36:
					return result;
				case 37:
				{
					int num2;
					switch (num2)
					{
					case 0:
						result = BorderStyle.TwistedLines1;
						num = 0;
						continue;
					case 1:
						result = BorderStyle.Triple;
						num = 12;
						continue;
					case 2:
						result = BorderStyle.DashSmallGap;
						num = 17;
						continue;
					case 3:
						result = BorderStyle.Single;
						num = 33;
						continue;
					case 4:
						result = BorderStyle.Dot;
						num = 25;
						continue;
					case 5:
						result = BorderStyle.DotDash;
						num = 7;
						continue;
					case 6:
						result = BorderStyle.DotDotDash;
						num = 32;
						continue;
					case 7:
						result = BorderStyle.DashLargeGap;
						num = 18;
						continue;
					case 8:
						result = BorderStyle.Double;
						num = 2;
						continue;
					case 9:
					case 10:
						result = BorderStyle.ThinThinSmallGap;
						num = 5;
						continue;
					case 11:
						result = BorderStyle.ThinThickSmallGap;
						num = 28;
						continue;
					case 12:
						result = BorderStyle.ThinThickThinSmallGap;
						num = 22;
						continue;
					case 13:
					case 14:
						result = BorderStyle.ThickThickThinMediumGap;
						num = 1;
						continue;
					case 15:
					case 16:
						goto IL_E3;
					case 17:
					case 18:
						result = BorderStyle.ThinThickMediumGap;
						num = 29;
						continue;
					case 19:
						result = BorderStyle.ThickThinLargeGap;
						num = 34;
						continue;
					case 20:
						result = BorderStyle.ThinThickLargeGap;
						num = 24;
						continue;
					case 21:
						result = BorderStyle.ThinThickThinLargeGap;
						num = 8;
						continue;
					case 22:
						result = BorderStyle.Thick;
						num = 26;
						continue;
					case 23:
						result = BorderStyle.Wave;
						num = 16;
						continue;
					case 24:
						result = BorderStyle.DoubleWave;
						num = 36;
						continue;
					case 25:
						result = BorderStyle.DashDotStroker;
						num = 10;
						continue;
					case 26:
						result = BorderStyle.Engrave3D;
						num = 14;
						continue;
					case 27:
						result = BorderStyle.Emboss3D;
						num = 4;
						continue;
					case 28:
						result = BorderStyle.Outset;
						num = 19;
						continue;
					case 29:
						result = BorderStyle.Inset;
						num = 13;
						continue;
					case 30:
						result = BorderStyle.Cleared;
						num = 21;
						continue;
					case 31:
						result = BorderStyle.None;
						num = 35;
						continue;
					default:
						num = 6;
						continue;
					}
					break;
				}
				}
				break;
				IL_E3:
				result = BorderStyle.ThickThinMediumGap;
				num = 27;
				continue;
				IL_362:
				num = 20;
			}
		}
		IL_DE:
		IL_F1:
		IL_104:
		IL_117:
		IL_1CB:
		IL_1DE:
		IL_1F1:
		IL_203:
		IL_216:
		IL_228:
		IL_23A:
		IL_27F:
		IL_292:
		IL_2A4:
		IL_2B7:
		IL_2CA:
		IL_2DC:
		IL_2EF:
		IL_302:
		IL_314:
		IL_324:
		IL_337:
		IL_34A:
		IL_35D:
		IL_39F:
		IL_687:
		return result;
		IL_6AD:
		if (false)
		{
		}
		return result;
	}

	// Token: 0x06001F7F RID: 8063 RVA: 0x00212790 File Offset: 0x00211790
	private int ᜀ(Border A_0)
	{
		for (;;)
		{
			FormatBase formatBase = A_0.ParentFormat;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (formatBase is CharacterFormat)
					{
						num = 8;
						continue;
					}
					num = 6;
					continue;
				case 1:
					goto IL_B6;
				case 2:
					return 20;
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
						if (formatBase != null)
						{
							num = 4;
							continue;
						}
						return 20;
					}
					break;
				case 4:
					num = 7;
					continue;
				case 5:
					formatBase = formatBase.ParentFormat;
					num = 1;
					continue;
				case 6:
					if (formatBase is Borders)
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					goto IL_B6;
				case 7:
					if (formatBase is ParagraphFormat)
					{
						num = 2;
						continue;
					}
					return 8;
				case 8:
					return 20;
				}
				break;
				IL_B6:
				num = 3;
			}
		}
		return 20;
	}

	// Token: 0x06001F80 RID: 8064 RVA: 0x00212890 File Offset: 0x00211890
	private void ᜅ(XmlReader A_0, CharacterFormat A_1)
	{
		int a_ = 18;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ṷ፹ၻች", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
			string attribute2 = A_0.GetAttribute(ClipboardData.b("๷᭹ၻ", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_D2;
				case 1:
					goto IL_12E;
				case 2:
					if (attribute == ClipboardData.b("᥷ཹࡻᅽ", a_))
					{
						num = 0;
						continue;
					}
					A_1.TextBackgroundColor = this.ᜃ(attribute);
					num = 5;
					continue;
				case 3:
					if (attribute2 != null)
					{
						num = 6;
						continue;
					}
					goto IL_12E;
				case 4:
					goto IL_139;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_139;
					default:
						goto IL_126;
					}
					break;
				case 6:
					if (true)
					{
					}
					A_1.TextureStyle = this.ᜉ(attribute2);
					num = 1;
					continue;
				case 7:
					num = 2;
					continue;
				}
				break;
				IL_12E:
				num = 4;
				continue;
				IL_139:
				if (attribute == null)
				{
					return;
				}
				num = 7;
			}
		}
		IL_D2:
		A_1.TextBackgroundColor = Color.Empty;
		return;
		IL_126:
		if (false)
		{
		}
	}

	// Token: 0x06001F81 RID: 8065 RVA: 0x002129EC File Offset: 0x002119EC
	private TextureStyle ᜉ(string A_0)
	{
		int a_ = 15;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_42B;
			case 1:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return TextureStyle.Texture5Percent;
				case 1:
					return TextureStyle.Texture10Percent;
				case 2:
					return TextureStyle.Texture12Pt5Percent;
				case 3:
					return TextureStyle.Texture15Percent;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_42B;
					default:
						goto IL_93;
					}
					break;
				case 5:
					return TextureStyle.Texture25Percent;
				case 6:
					return TextureStyle.Texture30Percent;
				case 7:
					return TextureStyle.Texture35Percent;
				case 8:
					return TextureStyle.Texture37Pt5Percent;
				case 9:
					return TextureStyle.Texture40Percent;
				case 10:
					return TextureStyle.Texture45Percent;
				case 11:
					return TextureStyle.Texture50Percent;
				case 12:
					return TextureStyle.Texture55Percent;
				case 13:
					return TextureStyle.Texture60Percent;
				case 14:
					return TextureStyle.Texture62Pt5Percent;
				case 15:
					return TextureStyle.Texture65Percent;
				case 16:
					return TextureStyle.Texture70Percent;
				case 17:
					goto IL_56;
				case 18:
					return TextureStyle.Texture80Percent;
				case 19:
					return TextureStyle.Texture85Percent;
				case 20:
					return TextureStyle.Texture87Pt5Percent;
				case 21:
					return TextureStyle.Texture90Percent;
				case 22:
					return TextureStyle.Texture95Percent;
				case 23:
					return TextureStyle.TextureCross;
				case 24:
					return TextureStyle.TextureDarkCross;
				case 25:
					return TextureStyle.TextureDarkDiagonalCross;
				case 26:
					return TextureStyle.TextureDarkDiagonalDown;
				case 27:
					return TextureStyle.TextureDarkDiagonalUp;
				case 28:
					return TextureStyle.TextureDarkHorizontal;
				case 29:
					return TextureStyle.TextureDarkVertical;
				case 30:
					return TextureStyle.TextureDiagonalCross;
				case 31:
					return TextureStyle.TextureDiagonalDown;
				case 32:
					return TextureStyle.TextureDiagonalUp;
				case 33:
					return TextureStyle.TextureHorizontal;
				case 34:
					return TextureStyle.TextureSolid;
				case 35:
					return TextureStyle.TextureVertical;
				default:
					num = 7;
					continue;
				}
				break;
			}
			case 2:
				spr᧓.\u176D = new Dictionary<string, int>(36)
				{
					{
						ClipboardData.b("մᑶ൸乺", a_),
						0
					},
					{
						ClipboardData.b("մᑶ൸䩺䵼", a_),
						1
					},
					{
						ClipboardData.b("մᑶ൸䩺佼", a_),
						2
					},
					{
						ClipboardData.b("մᑶ൸䩺䡼", a_),
						3
					},
					{
						ClipboardData.b("մᑶ൸䥺䵼", a_),
						4
					},
					{
						ClipboardData.b("մᑶ൸䥺䡼", a_),
						5
					},
					{
						ClipboardData.b("մᑶ൸䡺䵼", a_),
						6
					},
					{
						ClipboardData.b("մᑶ൸䡺䡼", a_),
						7
					},
					{
						ClipboardData.b("մᑶ൸䡺䩼", a_),
						8
					},
					{
						ClipboardData.b("մᑶ൸佺䵼", a_),
						9
					},
					{
						ClipboardData.b("մᑶ൸佺䡼", a_),
						10
					},
					{
						ClipboardData.b("մᑶ൸乺䵼", a_),
						11
					},
					{
						ClipboardData.b("մᑶ൸乺䡼", a_),
						12
					},
					{
						ClipboardData.b("մᑶ൸䵺䵼", a_),
						13
					},
					{
						ClipboardData.b("մᑶ൸䵺佼", a_),
						14
					},
					{
						ClipboardData.b("մᑶ൸䵺䡼", a_),
						15
					},
					{
						ClipboardData.b("մᑶ൸䱺䵼", a_),
						16
					},
					{
						ClipboardData.b("մᑶ൸䱺䡼", a_),
						17
					},
					{
						ClipboardData.b("մᑶ൸䍺䵼", a_),
						18
					},
					{
						ClipboardData.b("մᑶ൸䍺䡼", a_),
						19
					},
					{
						ClipboardData.b("մᑶ൸䍺䩼", a_),
						20
					},
					{
						ClipboardData.b("մᑶ൸䉺䵼", a_),
						21
					},
					{
						ClipboardData.b("մᑶ൸䉺䡼", a_),
						22
					},
					{
						ClipboardData.b("Ŵὶၸᕺ㕼ၾ廬욄ﺌ", a_),
						23
					},
					{
						ClipboardData.b("ᵴᡶ୸ź㹼ൾ", a_),
						24
					},
					{
						ClipboardData.b("ᅴṶᡸᱺ㹼ൾ", a_),
						25
					},
					{
						ClipboardData.b("ݴቶླྀṺོ౾잂\ud88a歷ﶎ", a_),
						26
					},
					{
						ClipboardData.b("ᅴṶᡸᱺ⹼୾", a_),
						27
					},
					{
						ClipboardData.b("ᵴᡶ୸ź⹼୾", a_),
						28
					},
					{
						ClipboardData.b("ʹቶ୸ེ⹼୾", a_),
						29
					},
					{
						ClipboardData.b("Ŵὶၸᕺ㥼ᙾ욄ﺌ", a_),
						30
					},
					{
						ClipboardData.b("Ŵὶၸᕺ⽼᩾쾊삒", a_),
						31
					},
					{
						ClipboardData.b("Ŵὶၸᕺ㥼ᙾ횄ﮈﶌ", a_),
						32
					},
					{
						ClipboardData.b("Ŵὶၸᕺ㕼ၾ廬횄ﮈﶌ", a_),
						33
					},
					{
						ClipboardData.b("ٴᡶᕸቺ᥼", a_),
						34
					},
					{
						ClipboardData.b("Ŵὶၸᕺ⭼᩾횄ﮈﶌ", a_),
						35
					}
				};
				num = 3;
				continue;
			case 3:
				goto IL_4DC;
			case 5:
				if (spr᧓.\u176D == null)
				{
					num = 2;
					continue;
				}
				goto IL_4DC;
			case 6:
				goto IL_6F;
			case 7:
				num = 6;
				continue;
			case 8:
				num = 5;
				continue;
			case 9:
			{
				int num2;
				if (spr᧓.\u176D.TryGetValue(A_0, out num2))
				{
					num = 0;
					continue;
				}
				return TextureStyle.TextureNone;
			}
			}
			if (A_0 != null)
			{
				num = 8;
				continue;
			}
			return TextureStyle.TextureNone;
			IL_42B:
			num = 1;
			continue;
			IL_4DC:
			num = 9;
		}
		return TextureStyle.TextureCross;
		IL_56:
		if (true)
		{
		}
		return TextureStyle.Texture75Percent;
		IL_6F:
		return TextureStyle.TextureNone;
		IL_93:
		if (false)
		{
		}
		return TextureStyle.Texture20Percent;
	}

	// Token: 0x06001F82 RID: 8066 RVA: 0x00212F20 File Offset: 0x00211F20
	private void ᜄ(XmlReader A_0, CharacterFormat A_1)
	{
		int a_ = 3;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("Ὠ੪Ŭ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_));
			int num = 7;
			for (;;)
			{
				IL_0B:
				switch (num)
				{
				case 0:
					while (attribute == ClipboardData.b("ᩨṪཬᱮተŲᱴݶ൸", a_))
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
							goto IL_0B;
						}
					}
					num = 8;
					continue;
				case 1:
					A_1.SubSuperScript = SubSuperScript.None;
					num = 5;
					continue;
				case 2:
					if (attribute == ClipboardData.b("୨੪Ṭ੮ᵰᩲ᭴ቶ", a_))
					{
						num = 1;
						continue;
					}
					return;
				case 3:
					goto IL_F3;
				case 4:
					goto IL_A9;
				case 5:
					return;
				case 6:
					return;
				case 7:
					if (true)
					{
					}
					if (attribute == null)
					{
						num = 6;
						continue;
					}
					num = 0;
					continue;
				case 8:
					if (attribute == ClipboardData.b("ᩨṪᵬ੮Ͱrᙴնၸ୺ॼ", a_))
					{
						num = 4;
						continue;
					}
					num = 2;
					continue;
				}
				break;
			}
		}
		return;
		IL_A9:
		A_1.SubSuperScript = SubSuperScript.SuperScript;
		return;
		IL_F3:
		A_1.SubSuperScript = SubSuperScript.SubScript;
	}

	// Token: 0x06001F83 RID: 8067 RVA: 0x00213078 File Offset: 0x00212078
	private void ᜃ(XmlReader A_0, CharacterFormat A_1)
	{
		int a_ = 17;
		for (;;)
		{
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("Ŷᡸ᝺", a_), ClipboardData.b("ὶ൸ེർ䕾꺀겂붒杖ﲘ춠얢쪤햦쒨쪪\ud9ac\udcae龰\udcb2잴킶隸첺튼춾ꗀ돂럄꣆꫈껊뻌볎룐뷒닔뫖뗘퇠헢쫤諦裨苪菬", a_));
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (spr᧓.ᝮ == null)
						{
							num = 1;
							continue;
						}
						goto IL_9B;
					case 1:
						spr᧓.ᝮ = new Dictionary<string, int>(17)
						{
							{
								ClipboardData.b("Ѷၸᕺ᩼፾", a_),
								0
							},
							{
								ClipboardData.b("vᙸॺ᥼౾", a_),
								1
							},
							{
								ClipboardData.b("፶ᙸ๺ὼ፾", a_),
								2
							},
							{
								ClipboardData.b("፶ᙸེॼ᩾", a_),
								3
							},
							{
								ClipboardData.b("ͶᅸቺṼᑾ", a_),
								4
							},
							{
								ClipboardData.b("፶ᡸࡺᕼ", a_),
								5
							},
							{
								ClipboardData.b("፶ᙸེ㥼Ṿ", a_),
								6
							},
							{
								ClipboardData.b("፶ᙸེ㥼ၾ잂", a_),
								7
							},
							{
								ClipboardData.b("vᡸൺ᡼", a_),
								8
							},
							{
								ClipboardData.b("፶ᡸࡺᕼ㍾", a_),
								9
							},
							{
								ClipboardData.b("፶ᙸེॼ᩾쮂ﾈ", a_),
								10
							},
							{
								ClipboardData.b("፶ᡸࡺᕼ᩾쮂ﾈ", a_),
								11
							},
							{
								ClipboardData.b("፶ᡸࡺᕼ㍾쾆ﮌ", a_),
								12
							},
							{
								ClipboardData.b("፶ᡸࡺᕼ㭾춄ﶊ", a_),
								13
							},
							{
								ClipboardData.b("፶ᡸࡺᕼ㭾솄ﶈ쎊", a_),
								14
							},
							{
								ClipboardData.b("vᡸൺѼ㝾ﺆ", a_),
								15
							},
							{
								ClipboardData.b("vᡸൺѼ㭾", a_),
								16
							}
						};
						num = 8;
						continue;
					case 2:
						if (attribute == null)
						{
							num = 3;
							continue;
						}
						num = 6;
						continue;
					case 3:
						return;
					case 4:
						num = 5;
						continue;
					case 5:
						goto IL_1BD;
					case 6:
					{
						string key;
						if ((key = attribute) != null)
						{
							num = 11;
							continue;
						}
						goto IL_3B4;
					}
					case 7:
					{
						string key;
						int num2;
						if (spr᧓.ᝮ.TryGetValue(key, out num2))
						{
							num = 10;
							continue;
						}
						goto IL_3B4;
					}
					case 8:
						goto IL_9B;
					case 9:
					{
						int num2;
						switch (num2)
						{
						case 0:
							goto IL_34D;
						case 1:
							goto IL_1A9;
						case 2:
							goto IL_36F;
						case 3:
							goto IL_367;
						case 4:
							goto IL_3A3;
						case 5:
							goto IL_12D;
						case 6:
							goto IL_92;
						case 7:
							goto IL_377;
						case 8:
							goto IL_35E;
						case 9:
							goto IL_89;
						case 10:
							goto IL_FF;
						case 11:
							goto IL_3AB;
						case 12:
							goto IL_355;
						case 13:
							goto IL_C7;
						case 14:
							goto IL_197;
						case 15:
							goto IL_124;
						case 16:
							goto IL_1A0;
						default:
							num = 4;
							continue;
						}
						break;
					}
					case 10:
						num = 9;
						continue;
					case 11:
						num = 0;
						continue;
					}
					break;
					IL_9B:
					num = 7;
				}
			}
			IL_FF:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_115;
			}
		}
		return;
		IL_89:
		A_1.UnderlineStyle = UnderlineStyle.DashLong;
		return;
		IL_92:
		A_1.UnderlineStyle = UnderlineStyle.DotDash;
		return;
		IL_C7:
		if (true)
		{
		}
		A_1.UnderlineStyle = UnderlineStyle.DotDashHeavy;
		return;
		IL_115:
		if (false)
		{
		}
		A_1.UnderlineStyle = UnderlineStyle.DottedHeavy;
		return;
		IL_124:
		A_1.UnderlineStyle = UnderlineStyle.WavyHeavy;
		return;
		IL_12D:
		A_1.UnderlineStyle = UnderlineStyle.Dash;
		return;
		IL_197:
		A_1.UnderlineStyle = UnderlineStyle.DotDotDashHeavy;
		return;
		IL_1A0:
		A_1.UnderlineStyle = UnderlineStyle.WavyDouble;
		return;
		IL_1A9:
		A_1.UnderlineStyle = UnderlineStyle.Words;
		return;
		IL_1BD:
		goto IL_3B4;
		IL_34D:
		A_1.UnderlineStyle = UnderlineStyle.Single;
		return;
		IL_355:
		A_1.UnderlineStyle = UnderlineStyle.DashLongHeavy;
		return;
		IL_35E:
		A_1.UnderlineStyle = UnderlineStyle.Wavy;
		return;
		IL_367:
		A_1.UnderlineStyle = UnderlineStyle.Dotted;
		return;
		IL_36F:
		A_1.UnderlineStyle = UnderlineStyle.Double;
		return;
		IL_377:
		A_1.UnderlineStyle = UnderlineStyle.DotDotDash;
		return;
		IL_3A3:
		A_1.UnderlineStyle = UnderlineStyle.Thick;
		return;
		IL_3AB:
		A_1.UnderlineStyle = UnderlineStyle.DashHeavy;
		return;
		IL_3B4:
		A_1.UnderlineStyle = UnderlineStyle.None;
	}

	// Token: 0x06001F84 RID: 8068 RVA: 0x00213440 File Offset: 0x00212440
	private void ᜂ(XmlReader A_0, CharacterFormat A_1)
	{
		int a_ = 7;
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int attributeCount = A_0.AttributeCount;
				int num2 = 8;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_55E;
					case 1:
						if (this.ᜆ == null)
						{
							num2 = 3;
							continue;
						}
						A_1.FontName = this.ᜆ;
						num2 = 5;
						continue;
					case 2:
						if (true)
						{
						}
						goto IL_55E;
					case 3:
						A_1.FontName = ClipboardData.b("⹬๮ᵰᩲ᝴նၸ", a_);
						num2 = 22;
						continue;
					case 4:
						num2 = 9;
						continue;
					case 5:
						goto IL_55E;
					case 6:
						goto IL_274;
					case 7:
						goto IL_55E;
					case 8:
						goto IL_274;
					case 9:
					{
						string localName;
						if (!(localName == ClipboardData.b("࡬๮ɰݲ㑴Ѷၸ᩺", a_)))
						{
							num2 = 43;
							continue;
						}
						A_1.FontNameFarEast = A_0.GetAttribute(num);
						num2 = 19;
						continue;
					}
					case 10:
						goto IL_216;
					case 11:
					{
						string a;
						if (!(a == ClipboardData.b("࡬๮ɰݲ㑴Ѷၸ᩺", a_)))
						{
							num2 = 29;
							continue;
						}
						A_1.IdctHintValue = IdctHint.EastAsia;
						num2 = 30;
						continue;
					}
					case 12:
						num2 = 20;
						continue;
					case 13:
						goto IL_55E;
					case 14:
						goto IL_55E;
					case 15:
						if (this.ᜅ == null)
						{
							num2 = 31;
							continue;
						}
						A_1.FontName = this.ᜅ;
						num2 = 36;
						continue;
					case 16:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_FA;
						default:
							if (false)
							{
							}
							num2 = 38;
							continue;
						}
						break;
					case 17:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num2 = 23;
							continue;
						}
						goto IL_55E;
					}
					case 18:
					{
						string attribute;
						if (attribute == ClipboardData.b("lٮὰᱲݴ㽶㡸ᕺ๼ᙾ", a_))
						{
							num2 = 25;
							continue;
						}
						num2 = 33;
						continue;
					}
					case 19:
						goto IL_55E;
					case 20:
					{
						string localName;
						if (!(localName == ClipboardData.b("լٮὰݲ", a_)))
						{
							num2 = 34;
							continue;
						}
						string attribute2 = A_0.GetAttribute(num);
						goto IL_FA;
					}
					case 21:
						if (num >= attributeCount)
						{
							num2 = 39;
							continue;
						}
						A_0.MoveToAttribute(num);
						num2 = 17;
						continue;
					case 22:
						goto IL_55E;
					case 23:
						num2 = 26;
						continue;
					case 24:
						num2 = 10;
						continue;
					case 25:
						num2 = 1;
						continue;
					case 26:
					{
						string localName;
						if (!(localName == ClipboardData.b("౬ᱮተᩲᱴ", a_)))
						{
							num2 = 41;
							continue;
						}
						string attribute3 = A_0.GetAttribute(num);
						A_1.FontNameAscii = attribute3;
						A_1.FontName = attribute3;
						num2 = 14;
						continue;
					}
					case 27:
					{
						string a;
						if (!(a == ClipboardData.b("๬ᱮ", a_)))
						{
							num2 = 24;
							continue;
						}
						A_1.IdctHintValue = IdctHint.ComplexScript;
						num2 = 0;
						continue;
					}
					case 28:
					{
						string localName;
						if (!(localName == ClipboardData.b("լ⹮ὰrᱴ", a_)))
						{
							num2 = 4;
							continue;
						}
						A_1.FontNameNonFarEast = A_0.GetAttribute(num);
						num2 = 13;
						continue;
					}
					case 29:
						num2 = 27;
						continue;
					case 30:
						goto IL_55E;
					case 31:
						A_1.FontName = ClipboardData.b("⹬๮ᱰᅲݴṶᡸ", a_);
						num2 = 7;
						continue;
					case 32:
						goto IL_55E;
					case 33:
					{
						string attribute;
						if (attribute == ClipboardData.b("l๮᭰ᱲݴ㽶㡸ᕺ๼ᙾ", a_))
						{
							num2 = 37;
							continue;
						}
						goto IL_55E;
					}
					case 34:
						num2 = 44;
						continue;
					case 35:
					{
						string a;
						string attribute2;
						if ((a = attribute2) != null)
						{
							num2 = 42;
							continue;
						}
						goto IL_216;
					}
					case 36:
						goto IL_55E;
					case 37:
						num2 = 15;
						continue;
					case 38:
					{
						string localName;
						if (!(localName == ClipboardData.b("౬ᱮተᩲᱴ⍶ᅸṺၼ᩾", a_)))
						{
							num2 = 12;
							continue;
						}
						string attribute = A_0.GetAttribute(num);
						num2 = 18;
						continue;
					}
					case 39:
						return;
					case 40:
					{
						string localName;
						if (!(localName == ClipboardData.b("๬ᱮ", a_)))
						{
							num2 = 16;
							continue;
						}
						A_1.FontNameBidi = A_0.GetAttribute(num);
						num2 = 2;
						continue;
					}
					case 41:
						num2 = 28;
						continue;
					case 42:
						num2 = 11;
						continue;
					case 43:
						num2 = 40;
						continue;
					case 44:
						goto IL_55E;
					}
					break;
					IL_FA:
					num2 = 35;
					continue;
					IL_216:
					A_1.IdctHintValue = IdctHint.Default;
					num2 = 32;
					continue;
					IL_274:
					num2 = 21;
					continue;
					IL_55E:
					num++;
					num2 = 6;
				}
			}
			return;
		}
	}

	// Token: 0x06001F85 RID: 8069 RVA: 0x002139EC File Offset: 0x002129EC
	private void ᜁ(XmlReader A_0, CharacterFormat A_1)
	{
		int a_ = 5;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ᵪ౬ͮ", a_), ClipboardData.b("ͪᥬ᭮Ű䥲婴塶੸᡺ᕼ᩾ꦆﮊﺒ璉ﺞ햠킢认좦\udba8첪芬\ud8ae\udeb0솲톴잶쮸풺\udebc\udabe닀냂계꧆껈ꛊꇌ뛚볜뛞迠", a_));
			int num = 6;
			for (;;)
			{
				string attribute2;
				switch (num)
				{
				case 0:
					if (Enum.IsDefined(typeof(LocaleIDs), attribute2.Replace('-', '_')))
					{
						num = 9;
						continue;
					}
					return;
				case 1:
					num = 0;
					continue;
				case 2:
					if (Enum.IsDefined(typeof(LocaleIDs), attribute.Replace('-', '_')))
					{
						num = 10;
						continue;
					}
					goto IL_AC;
				case 3:
					num = 2;
					continue;
				case 4:
					if (!string.IsNullOrEmpty(attribute2))
					{
						num = 1;
						continue;
					}
					return;
				case 5:
					if (Enum.IsDefined(typeof(LocaleIDs), attribute.Replace('-', '_')))
					{
						num = 14;
						continue;
					}
					goto IL_24E;
				case 6:
					if (!string.IsNullOrEmpty(attribute))
					{
						num = 8;
						continue;
					}
					goto IL_24E;
				case 7:
					goto IL_24E;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_189;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 9:
					if (true)
					{
					}
					A_1.LidBi = (short)((LocaleIDs)Enum.Parse(typeof(LocaleIDs), attribute2.Replace('-', '_')));
					goto IL_189;
				case 10:
					A_1.LocaleIdFarEast = (short)((LocaleIDs)Enum.Parse(typeof(LocaleIDs), attribute.Replace('-', '_')));
					num = 12;
					continue;
				case 11:
					if (!string.IsNullOrEmpty(attribute))
					{
						num = 3;
						continue;
					}
					goto IL_AC;
				case 12:
					goto IL_AC;
				case 13:
					return;
				case 14:
					A_1.LocaleIdASCII = (short)((LocaleIDs)Enum.Parse(typeof(LocaleIDs), attribute.Replace('-', '_')));
					num = 7;
					continue;
				}
				break;
				IL_AC:
				attribute2 = A_0.GetAttribute(ClipboardData.b("४Ѭ୮ᡰ", a_), ClipboardData.b("ͪᥬ᭮Ű䥲婴塶੸᡺ᕼ᩾ꦆﮊﺒ璉ﺞ햠킢认좦\udba8첪芬\ud8ae\udeb0솲톴잶쮸풺\udebc\udabe닀냂계꧆껈ꛊꇌ뛚볜뛞迠", a_));
				num = 4;
				continue;
				IL_189:
				num = 13;
				continue;
				IL_24E:
				attribute = A_0.GetAttribute(ClipboardData.b("๪౬ᱮհ㉲ٴṶᡸ", a_), ClipboardData.b("ͪᥬ᭮Ű䥲婴塶੸᡺ᕼ᩾ꦆﮊﺒ璉ﺞ햠킢认좦\udba8첪芬\ud8ae\udeb0솲톴잶쮸풺\udebc\udabe닀냂계꧆껈ꛊꇌ뛚볜뛞迠", a_));
				num = 11;
			}
		}
	}

	// Token: 0x06001F86 RID: 8070 RVA: 0x00213C90 File Offset: 0x00212C90
	private void ᜀ(XmlReader A_0, CharacterFormat A_1)
	{
		int a_ = 16;
		string attribute;
		for (;;)
		{
			attribute = A_0.GetAttribute(ClipboardData.b("u᥷ᙹ", a_), ClipboardData.b("ṵ౷๹౻䑽꽿궁벑ﮓﶗ첟쒡쮣풥얧쮩\ud8ab\uddad麯\uddb1욳통鞷춹펻첽꒿닁뛃꧅ꯇ꿉뿋뷍맏병돓믕듗탟퓡쯣该觧菩苫", a_));
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_73;
				case 1:
					goto IL_C4;
				case 2:
					if (attribute.ToLower() == ClipboardData.b("ት᥷ࡹ᝻ݽﾇ", a_))
					{
						num = 1;
						continue;
					}
					goto IL_C7;
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
						if (attribute == null)
						{
							num = 0;
							continue;
						}
						num = 2;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_73:
		if (true)
		{
		}
		return;
		IL_C4:
		A_1.HighlightColor = Color.Gold;
		return;
		IL_C7:
		A_1.HighlightColor = this.ᜃ(attribute);
	}

	// Token: 0x06001F87 RID: 8071 RVA: 0x00213D74 File Offset: 0x00212D74
	private void ᜋ(XmlReader A_0, ParagraphFormat A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 71;
			for (;;)
			{
				string text;
				switch (num)
				{
				case 0:
					goto IL_90F;
				case 1:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 40;
						continue;
					}
					goto IL_9BB;
				}
				case 2:
					goto IL_90F;
				case 3:
					if (!string.IsNullOrEmpty(text))
					{
						num = 70;
						continue;
					}
					goto IL_90F;
				case 4:
					spr᧓.ᝯ = new Dictionary<string, int>(25)
					{
						{
							ClipboardData.b("୷όύ੽큿", a_),
							0
						},
						{
							ClipboardData.b("ࡷ⥹ࡻݽ", a_),
							1
						},
						{
							ClipboardData.b("ṷࡹᵻ፽튁", a_),
							2
						},
						{
							ClipboardData.b("੷⩹๻", a_),
							3
						},
						{
							ClipboardData.b("౷᭹ṻൽ", a_),
							4
						},
						{
							ClipboardData.b("ࡷ᭹᭻᭽쉿좉ﾏ", a_),
							5
						},
						{
							ClipboardData.b("፷ό᥻๽챿ﮇ", a_),
							6
						},
						{
							ClipboardData.b("᝷ཹࡻች쪅ﺇ", a_),
							7
						},
						{
							ClipboardData.b("፷ό᥻๽칿ﲃ", a_),
							8
						},
						{
							ClipboardData.b("ቷ᥹", a_),
							9
						},
						{
							ClipboardData.b("ᅷᑹ᡻", a_),
							10
						},
						{
							ClipboardData.b("୷੹ᵻᵽ", a_),
							11
						},
						{
							ClipboardData.b("୷ቹ᡻", a_),
							12
						},
						{
							ClipboardData.b("᩷፹᡻᝽", a_),
							13
						},
						{
							ClipboardData.b("ཷ፹᡻ᅽ솁ﲇ", a_),
							14
						},
						{
							ClipboardData.b("᥷ཹࡻᅽ퍿캉즋", a_),
							15
						},
						{
							ClipboardData.b("᥷ཹࡻᅽ퍿캉슋", a_),
							16
						},
						{
							ClipboardData.b("᥷ṹᙻ୽횃잍ﺏ", a_),
							17
						},
						{
							ClipboardData.b("ࡷ㡹᡻౽", a_),
							18
						},
						{
							ClipboardData.b("ᙷཹᅻ⹽", a_),
							19
						},
						{
							ClipboardData.b("᭷ᕹቻ੽嬨\udf8bﺍﶓﾗ", a_),
							20
						},
						{
							ClipboardData.b("ࡷ⩹๻㵽", a_),
							21
						},
						{
							ClipboardData.b("᭷ᑹ᩻⵽ﮁ", a_),
							22
						},
						{
							ClipboardData.b("ᕷ፹๻౽춃揄", a_),
							23
						},
						{
							ClipboardData.b("୷ཹ౻๽즇ﾉ\ud88fﺕﶗ", a_),
							24
						}
					};
					num = 75;
					continue;
				case 5:
					goto IL_889;
				case 6:
					goto IL_90F;
				case 7:
				{
					CharacterFormat breakCharacterFormat = (A_1.OwnerBase as Paragraph).BreakCharacterFormat;
					this.ᜋ(A_0, breakCharacterFormat);
					num = 22;
					continue;
				}
				case 8:
					num = 9;
					continue;
				case 9:
					if (A_0.LocalName != ClipboardData.b("ࡷ⩹๻", a_))
					{
						num = 23;
						continue;
					}
					goto IL_90F;
				case 10:
					goto IL_889;
				case 11:
					goto IL_9BB;
				case 12:
					goto IL_90F;
				case 13:
					if (A_1.OwnerBase is Paragraph)
					{
						num = 7;
						continue;
					}
					goto IL_90F;
				case 14:
					if (A_0.LocalName != string.Empty)
					{
						num = 8;
						continue;
					}
					goto IL_90F;
				case 15:
				{
					int num2;
					if (num2 < 0)
					{
						goto IL_658;
					}
					if (num2 > 9)
					{
						goto IL_658;
					}
					OutlineLevel outlineLevel = (OutlineLevel)Enum.ToObject(typeof(OutlineLevel), num2);
					IL_635:
					A_1.OutlineLevel = outlineLevel;
					num = 12;
					continue;
					IL_658:
					outlineLevel = OutlineLevel.Body;
					goto IL_635;
				}
				case 16:
				{
					ParagraphStyle paragraphStyle = A_1.OwnerBase as ParagraphStyle;
					num = 57;
					continue;
				}
				case 17:
				{
					Borders borders;
					if (borders != null)
					{
						num = 73;
						continue;
					}
					goto IL_90F;
				}
				case 18:
					goto IL_90F;
				case 19:
					goto IL_90F;
				case 20:
				{
					string attribute;
					int num2 = int.Parse(attribute, NumberStyles.Integer, CultureInfo.InvariantCulture);
					num = 15;
					continue;
				}
				case 21:
					goto IL_90F;
				case 22:
					goto IL_90F;
				case 23:
				{
					A_1.XmlProps2010.Add(this.ᜢ(A_0));
					bool flag = true;
					num = 6;
					continue;
				}
				case 24:
					if (this.ᜈ().ContainsKey(text))
					{
						num = 53;
						continue;
					}
					goto IL_90F;
				case 25:
					if (!A_0.IsEmptyElement)
					{
						num = 16;
						continue;
					}
					goto IL_4AB;
				case 26:
				{
					if (!(A_0.LocalName != ClipboardData.b("ࡷ⩹๻", a_)))
					{
						num = 62;
						continue;
					}
					bool flag = false;
					num = 27;
					continue;
				}
				case 27:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 68;
						continue;
					}
					A_0.Read();
					num = 74;
					continue;
				case 28:
					goto IL_90F;
				case 29:
				{
					int num3;
					switch (num3)
					{
					case 0:
						this.ᜁ(A_0, this.ᜄ.LastSection);
						this.ᜄ.AddSection();
						num = 63;
						continue;
					case 1:
						text = A_0.GetAttribute(ClipboardData.b("๷᭹ၻ", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
						num = 3;
						continue;
					case 2:
						this.ᜊ(A_0, A_1);
						num = 55;
						continue;
					case 3:
						num = 13;
						continue;
					case 4:
						this.ᜆ(A_0, A_1);
						num = 64;
						continue;
					case 5:
						A_1.PageBreakBefore = this.ᜂ(A_0);
						num = 45;
						continue;
					case 6:
						A_1.Keep = this.ᜂ(A_0);
						num = 21;
						continue;
					case 7:
					{
						string attribute = A_0.GetAttribute(ClipboardData.b("๷᭹ၻ", a_), ClipboardData.b("ၷ๹ࡻ๽멿궁ꮃ몓秊ﾙ춟캡슣즥\udaa7잩춫\udaad쎯鲱\udbb3쒵\udfb7閹쮻톽늿ꛁ듃듅Ꟈ꧉꧋뷍ꏏ믑뫓뇕뗗뛙탟틡틣짥藧诩藫胭", a_));
						num = 66;
						continue;
					}
					case 8:
						A_1.KeepFollow = this.ᜂ(A_0);
						num = 37;
						continue;
					case 9:
						this.ᜁ(A_0, A_1);
						num = 18;
						continue;
					case 10:
						this.ᜂ(A_0, A_1);
						num = 65;
						continue;
					case 11:
						this.ᜄ(A_0, A_1);
						num = 2;
						continue;
					case 12:
						this.ᜀ(A_0, A_1);
						num = 52;
						continue;
					case 13:
						A_1.IsBidi = this.ᜂ(A_0);
						num = 58;
						continue;
					case 14:
						A_1.IsWidowControl = this.ᜂ(A_0);
						num = 42;
						continue;
					case 15:
						A_1.AutoSpaceDE = this.ᜂ(A_0);
						num = 38;
						continue;
					case 16:
						A_1.AutoSpaceDN = this.ᜂ(A_0);
						num = 54;
						continue;
					case 17:
						A_1.AdjustRightIndent = this.ᜂ(A_0);
						num = 43;
						continue;
					case 18:
						num = 34;
						continue;
					case 19:
						this.ᜀ(A_0, A_1);
						num = 49;
						continue;
					case 20:
						A_1.IsContextualSpacing = true;
						num = 30;
						continue;
					case 21:
						this.ᜅ(A_0, A_1);
						num = 28;
						continue;
					case 22:
						goto IL_90F;
					case 23:
						A_1.MirrorIndents = this.ᜂ(A_0);
						num = 60;
						continue;
					case 24:
						A_1.SuppressAutoHyphens = this.ᜂ(A_0);
						num = 41;
						continue;
					default:
						num = 72;
						continue;
					}
					break;
				}
				case 30:
					goto IL_90F;
				case 31:
					if (A_1 == null)
					{
						num = 79;
						continue;
					}
					num = 44;
					continue;
				case 32:
					num = 56;
					continue;
				case 33:
					goto IL_C8D;
				case 34:
					if (A_1.OwnerBase is IStyle)
					{
						num = 59;
						continue;
					}
					goto IL_4AB;
				case 35:
					return;
				case 36:
					num = 77;
					continue;
				case 37:
					goto IL_90F;
				case 38:
					goto IL_90F;
				case 39:
					if (spr᧓.ᝯ == null)
					{
						num = 4;
						continue;
					}
					goto IL_35D;
				case 40:
					num = 39;
					continue;
				case 41:
					goto IL_90F;
				case 42:
					goto IL_90F;
				case 43:
					goto IL_90F;
				case 44:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52D;
					default:
					{
						if (false)
						{
						}
						if (A_0.IsEmptyElement)
						{
							num = 35;
							continue;
						}
						bool flag = false;
						A_0.Read();
						this.ᜀ(A_0);
						num = 5;
						continue;
					}
					}
					break;
				case 45:
					goto IL_90F;
				case 46:
					goto IL_22E;
				case 47:
					num = 29;
					continue;
				case 48:
					num = 24;
					continue;
				case 49:
					goto IL_90F;
				case 50:
				{
					string localName;
					int num3;
					if (spr᧓.ᝯ.TryGetValue(localName, out num3))
					{
						num = 47;
						continue;
					}
					goto IL_9BB;
				}
				case 51:
				{
					bool flag;
					if (!flag)
					{
						num = 69;
						continue;
					}
					goto IL_C8D;
				}
				case 52:
					goto IL_90F;
				case 53:
				{
					IParagraphStyle paragraphStyle2 = this.ᜄ.Styles.FindByName(this.ᜈ()[text], StyleType.ParagraphStyle) as IParagraphStyle;
					num = 61;
					continue;
				}
				case 54:
					goto IL_90F;
				case 55:
					goto IL_90F;
				case 56:
					return;
				case 57:
				{
					ParagraphStyle paragraphStyle;
					if (paragraphStyle == null)
					{
						num = 32;
						continue;
					}
					Borders borders = paragraphStyle.ParagraphFormat.Borders;
					num = 17;
					continue;
				}
				case 58:
					goto IL_90F;
				case 59:
					num = 25;
					continue;
				case 60:
					goto IL_90F;
				case 61:
				{
					IParagraphStyle paragraphStyle2;
					if (paragraphStyle2 != null)
					{
						num = 76;
						continue;
					}
					goto IL_90F;
				}
				case 62:
					return;
				case 63:
					goto IL_90F;
				case 64:
					goto IL_90F;
				case 65:
					goto IL_90F;
				case 66:
				{
					string attribute;
					if (attribute != null)
					{
						num = 20;
						continue;
					}
					goto IL_90F;
				}
				case 67:
					if (A_1.OwnerBase is Paragraph)
					{
						num = 48;
						continue;
					}
					goto IL_90F;
				case 68:
					num = 1;
					continue;
				case 69:
					if (true)
					{
					}
					A_0.Read();
					num = 33;
					continue;
				case 70:
					goto IL_52D;
				case 72:
					num = 11;
					continue;
				case 73:
				{
					Borders borders;
					this.ᜀ(A_0, borders);
					num = 78;
					continue;
				}
				case 74:
					goto IL_C8D;
				case 75:
					goto IL_35D;
				case 76:
				{
					IParagraphStyle paragraphStyle2;
					(A_1.OwnerBase as Paragraph).ᜀ(paragraphStyle2);
					num = 0;
					continue;
				}
				case 77:
					if (A_0.LocalName != ClipboardData.b("ࡷ⩹๻㵽", a_))
					{
						num = 46;
						continue;
					}
					goto IL_C6A;
				case 78:
					goto IL_90F;
				case 79:
					goto IL_C88;
				}
				if (A_0.LocalName != ClipboardData.b("ࡷ⩹๻", a_))
				{
					num = 36;
					continue;
				}
				goto IL_C6A;
				IL_35D:
				num = 50;
				continue;
				IL_4AB:
				this.ᜂ(A_0, A_1.OwnerBase as IDocumentObject);
				num = 19;
				continue;
				IL_52D:
				text = text.Trim();
				num = 67;
				continue;
				IL_889:
				num = 26;
				continue;
				IL_90F:
				num = 51;
				continue;
				IL_9BB:
				num = 14;
				continue;
				IL_C6A:
				num = 31;
				continue;
				IL_C8D:
				this.ᜀ(A_0);
				num = 10;
			}
			IL_22E:
			throw new XmlException(ClipboardData.b("⡷᭹๻ώꪉﲋﲍﾏ鍊", a_));
			IL_C88:
			throw new ArgumentException(ClipboardData.b("⡷᭹๻ώꪉﾑ뢗햟캡삣蚥욧얩\ud8ab躭튯ힱ钳\ud8b5춷횹킻", a_));
		}
		}
	}

	// Token: 0x06001F88 RID: 8072 RVA: 0x00214A28 File Offset: 0x00213A28
	private void ᜊ(XmlReader A_0, ParagraphFormat A_1)
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
		A_1.IsFrame = true;
		this.ᜇ(A_0, A_1);
		this.ᜈ(A_0, A_1);
		this.ᜉ(A_0, A_1);
	}

	// Token: 0x06001F89 RID: 8073 RVA: 0x00214A84 File Offset: 0x00213A84
	private void ᜉ(XmlReader A_0, ParagraphFormat A_1)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			short num2;
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("v", a_), ClipboardData.b("ὶ൸ེർ䕾꺀겂붒杖ﲘ춠얢쪤햦쒨쪪\ud9ac\udcae龰\udcb2잴킶隸첺튼춾ꗀ돂럄꣆꫈껊뻌볎룐뷒닔뫖뗘퇠헢쫤諦裨苪菬", a_));
				int num = 1;
				for (;;)
				{
					string attribute2;
					string attribute3;
					switch (num)
					{
					case 0:
						if (num2 != 0)
						{
							num = 6;
							continue;
						}
						return;
					case 1:
						if (attribute != null)
						{
							num = 2;
							continue;
						}
						goto IL_1E5;
					case 2:
						if (true)
						{
						}
						num = 13;
						continue;
					case 3:
						goto IL_197;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_20A;
						default:
							if (false)
							{
							}
							num2 = (short)int.Parse(attribute2);
							num = 8;
							continue;
						}
						break;
					case 5:
						num = 16;
						continue;
					case 6:
						A_1.FrameHeight = (short)((int)num2 | 32768);
						num = 10;
						continue;
					case 7:
						num = 9;
						continue;
					case 8:
						goto IL_B7;
					case 9:
						if (attribute3 == ClipboardData.b("ቶŸ᩺Ṽ୾", a_))
						{
							num = 3;
							continue;
						}
						goto IL_11F;
					case 10:
						return;
					case 11:
						A_1.FrameWidth = (short)int.Parse(attribute);
						num = 15;
						continue;
					case 12:
						if (attribute2 != null)
						{
							num = 5;
							continue;
						}
						goto IL_B7;
					case 13:
						if (attribute != string.Empty)
						{
							num = 11;
							continue;
						}
						goto IL_1E5;
					case 14:
						if (attribute3 != null)
						{
							num = 7;
							continue;
						}
						goto IL_11F;
					case 15:
						goto IL_1E5;
					case 16:
						if (attribute2 != string.Empty)
						{
							num = 4;
							continue;
						}
						goto IL_B7;
					}
					break;
					IL_B7:
					attribute3 = A_0.GetAttribute(ClipboardData.b("ὶ⭸๺ᅼ᩾", a_), ClipboardData.b("ὶ൸ེർ䕾꺀겂붒杖ﲘ춠얢쪤햦쒨쪪\ud9ac\udcae龰\udcb2잴킶隸첺튼춾ꗀ돂럄꣆꫈껊뻌볎룐뷒닔뫖뗘퇠헢쫤諦裨苪菬", a_));
					num = 14;
					continue;
					IL_11F:
					num = 0;
					continue;
					IL_20A:
					num = 12;
					continue;
					IL_1E5:
					attribute2 = A_0.GetAttribute(ClipboardData.b("ὶ", a_), ClipboardData.b("ὶ൸ེർ䕾꺀겂붒杖ﲘ춠얢쪤햦쒨쪪\ud9ac\udcae龰\udcb2잴킶隸첺튼춾ꗀ돂럄꣆꫈껊뻌볎룐뷒닔뫖뗘퇠헢쫤諦裨苪菬", a_));
					num2 = 0;
					goto IL_20A;
				}
			}
			IL_197:
			A_1.FrameHeight = num2;
			return;
		}
		}
	}

	// Token: 0x06001F8A RID: 8074 RVA: 0x00214CF0 File Offset: 0x00213CF0
	private void ᜈ(XmlReader A_0, ParagraphFormat A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
				{
					if (false)
					{
					}
					string attribute = A_0.GetAttribute(ClipboardData.b("ͪⱬŮተ᭲ᩴն", a_), ClipboardData.b("ͪᥬ᭮Ű䥲婴塶੸᡺ᕼ᩾ꦆﮊﺒ璉ﺞ햠킢认좦\udba8첪芬\ud8ae\udeb0솲톴잶쮸풺\udebc\udabe닀냂계꧆껈ꛊꇌ뛚볜뛞迠", a_));
					int num = 5;
					for (;;)
					{
						string attribute2;
						switch (num)
						{
						case 0:
							num = 7;
							continue;
						case 1:
							num = 28;
							continue;
						case 2:
						{
							string a;
							if (!(a == ClipboardData.b("᭪౬࡮ᑰ", a_)))
							{
								num = 11;
								continue;
							}
							goto IL_20E;
						}
						case 3:
						{
							string a;
							if (!(a == ClipboardData.b("٪౬ᵮᙰᩲ᭴", a_)))
							{
								num = 4;
								continue;
							}
							goto IL_28F;
						}
						case 4:
							num = 2;
							continue;
						case 5:
							if (attribute != null)
							{
								num = 16;
								continue;
							}
							goto IL_216;
						case 6:
							num = 13;
							continue;
						case 7:
						{
							string a;
							if ((a = attribute2) != null)
							{
								num = 27;
								continue;
							}
							return;
						}
						case 8:
							num = 15;
							continue;
						case 9:
							goto IL_370;
						case 10:
							num = 19;
							continue;
						case 11:
							num = 26;
							continue;
						case 12:
							num = 21;
							continue;
						case 13:
							goto IL_216;
						case 14:
						{
							string a2;
							if ((a2 = attribute) != null)
							{
								num = 12;
								continue;
							}
							goto IL_216;
						}
						case 15:
							if (attribute2 != string.Empty)
							{
								if (true)
								{
								}
								num = 0;
								continue;
							}
							return;
						case 16:
							num = 18;
							continue;
						case 17:
							num = 14;
							continue;
						case 18:
							if (attribute != string.Empty)
							{
								num = 17;
								continue;
							}
							goto IL_216;
						case 19:
						{
							string a2;
							if (!(a2 == ClipboardData.b("ࡪɬͮѰṲ᭴", a_)))
							{
								num = 6;
								continue;
							}
							A_1.FrameHorizontalPos = 0;
							num = 25;
							continue;
						}
						case 20:
							goto IL_216;
						case 21:
						{
							string a2;
							if (!(a2 == ClipboardData.b("٪౬ᵮᙰᩲ᭴", a_)))
							{
								num = 1;
								continue;
							}
							A_1.FrameHorizontalPos = 1;
							num = 20;
							continue;
						}
						case 22:
							goto IL_2AA;
						case 23:
							goto IL_216;
						case 24:
							if (attribute2 != null)
							{
								num = 8;
								continue;
							}
							return;
						case 25:
							goto IL_216;
						case 26:
						{
							string a;
							if (!(a == ClipboardData.b("Ὢ࡬ᝮհ", a_)))
							{
								num = 9;
								continue;
							}
							A_1.FrameVerticalPos = 2;
							num = 22;
							continue;
						}
						case 27:
							num = 3;
							continue;
						case 28:
						{
							string a2;
							if (!(a2 == ClipboardData.b("᭪౬࡮ᑰ", a_)))
							{
								num = 10;
								continue;
							}
							A_1.FrameHorizontalPos = 2;
							num = 23;
							continue;
						}
						}
						break;
						IL_216:
						attribute2 = A_0.GetAttribute(ClipboardData.b("ᵪⱬŮተ᭲ᩴն", a_), ClipboardData.b("ͪᥬ᭮Ű䥲婴塶੸᡺ᕼ᩾ꦆﮊﺒ璉ﺞ햠킢认좦\udba8첪芬\ud8ae\udeb0솲톴잶쮸풺\udebc\udabe닀냂계꧆껈ꛊꇌ뛚볜뛞迠", a_));
						num = 24;
					}
					break;
				}
				}
			}
			return;
			IL_20E:
			A_1.FrameVerticalPos = 1;
			return;
			IL_28F:
			A_1.FrameVerticalPos = 0;
			return;
			IL_2AA:
			return;
			IL_370:
			return;
		}
	}

	// Token: 0x06001F8B RID: 8075 RVA: 0x002150AC File Offset: 0x002140AC
	private void ᜇ(XmlReader A_0, ParagraphFormat A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("ṥ⥧٩ի७ṯ", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧\udda9쎫\udcad풯슱욳\ud9b5\udbb7\udfb9쾻춽ꦿ곁ꏃꯅ꓇ﻋﻍﯓ믕맗동닛", a_));
				int num = 0;
				for (;;)
				{
					string attribute2;
					string attribute3;
					string attribute4;
					string attribute5;
					string attribute6;
					string attribute7;
					switch (num)
					{
					case 0:
						if (attribute != null)
						{
							num = 56;
							continue;
						}
						goto IL_38F;
					case 1:
						if (attribute2 != string.Empty)
						{
							num = 2;
							continue;
						}
						goto IL_45F;
					case 2:
						A_1.FrameX = (short)int.Parse(attribute2);
						num = 40;
						continue;
					case 3:
					{
						string a;
						if (!(a == ClipboardData.b("॥ᵧṩὫݭᑯ᝱", a_)))
						{
							num = 61;
							continue;
						}
						A_1.FrameY = -20;
						num = 35;
						continue;
					}
					case 4:
						num = 49;
						continue;
					case 5:
						goto IL_45F;
					case 6:
						num = 19;
						continue;
					case 7:
						A_1.FrameHorizontalDistanceFromText = (short)int.Parse(attribute3);
						num = 22;
						continue;
					case 8:
						goto IL_516;
					case 9:
					{
						string a2;
						if ((a2 = attribute) != null)
						{
							num = 31;
							continue;
						}
						goto IL_45F;
					}
					case 10:
						A_1.FrameY = (short)int.Parse(attribute4);
						num = 29;
						continue;
					case 11:
						if (attribute5 != null)
						{
							num = 24;
							continue;
						}
						goto IL_256;
					case 12:
					{
						string a;
						if (!(a == ClipboardData.b("ե൧ѩᡫ୭ɯ", a_)))
						{
							num = 57;
							continue;
						}
						A_1.FrameY = -8;
						num = 16;
						continue;
					}
					case 13:
						num = 12;
						continue;
					case 14:
						if (!string.IsNullOrEmpty(attribute3))
						{
							num = 7;
							continue;
						}
						goto IL_6EC;
					case 15:
						num = 44;
						continue;
					case 16:
						goto IL_516;
					case 17:
						if (attribute != string.Empty)
						{
							num = 38;
							continue;
						}
						goto IL_38F;
					case 18:
						num = 30;
						continue;
					case 19:
					{
						string a2;
						if (!(a2 == ClipboardData.b("ཥ٧ᥩի੭ᕯ", a_)))
						{
							num = 32;
							continue;
						}
						A_1.FrameX = -12;
						num = 5;
						continue;
					}
					case 20:
						goto IL_71C;
					case 21:
						return;
					case 22:
						goto IL_6EC;
					case 23:
						A_1.FrameVerticalDistanceFromText = (short)int.Parse(attribute6);
						num = 21;
						continue;
					case 24:
						num = 25;
						continue;
					case 25:
						if (!(attribute5 == ClipboardData.b("ݥᩧթᥫmᑯ", a_)))
						{
							num = 15;
							continue;
						}
						goto IL_2CD;
					case 26:
						goto IL_45F;
					case 27:
						goto IL_516;
					case 28:
						goto IL_2CD;
					case 29:
						goto IL_516;
					case 30:
					{
						string a;
						if (!(a == ClipboardData.b("ѥݧṩᡫŭᵯ", a_)))
						{
							num = 13;
							continue;
						}
						A_1.FrameY = -12;
						num = 8;
						continue;
					}
					case 31:
						num = 43;
						continue;
					case 32:
						num = 51;
						continue;
					case 33:
						num = 1;
						continue;
					case 34:
						num = 54;
						continue;
					case 35:
						goto IL_516;
					case 36:
						if (attribute4 != null)
						{
							num = 55;
							continue;
						}
						goto IL_516;
					case 37:
						goto IL_516;
					case 38:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_71C;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					case 39:
					{
						string a;
						if (!(a == ClipboardData.b("ཥ٧ᥩի੭ᕯ", a_)))
						{
							num = 48;
							continue;
						}
						A_1.FrameY = -16;
						num = 27;
						continue;
					}
					case 40:
						goto IL_45F;
					case 41:
						if (attribute2 != null)
						{
							num = 33;
							continue;
						}
						goto IL_45F;
					case 42:
					{
						string a2;
						if (!(a2 == ClipboardData.b("ե൧ѩᡫ୭ɯ", a_)))
						{
							num = 6;
							continue;
						}
						A_1.FrameX = -4;
						num = 62;
						continue;
					}
					case 43:
					{
						string a2;
						if (!(a2 == ClipboardData.b("ᑥŧ൩ѫᩭ", a_)))
						{
							num = 50;
							continue;
						}
						A_1.FrameX = -8;
						num = 26;
						continue;
					}
					case 44:
						if (attribute5 == ClipboardData.b("ݥᵧṩͫ", a_))
						{
							num = 28;
							continue;
						}
						goto IL_256;
					case 45:
						if (attribute7 != string.Empty)
						{
							num = 46;
							continue;
						}
						goto IL_7DC;
					case 46:
						num = 63;
						continue;
					case 47:
						num = 45;
						continue;
					case 48:
						num = 3;
						continue;
					case 49:
					{
						string a;
						if (!(a == ClipboardData.b("ብݧᩩ", a_)))
						{
							num = 18;
							continue;
						}
						A_1.FrameY = -4;
						num = 37;
						continue;
					}
					case 50:
						num = 42;
						continue;
					case 51:
					{
						string a2;
						if (!(a2 == ClipboardData.b("॥ᵧṩὫݭᑯ᝱", a_)))
						{
							num = 34;
							continue;
						}
						A_1.FrameX = -16;
						num = 52;
						continue;
					}
					case 52:
						goto IL_45F;
					case 53:
						if (attribute4 != string.Empty)
						{
							num = 10;
							continue;
						}
						goto IL_516;
					case 54:
						goto IL_45F;
					case 55:
						num = 53;
						continue;
					case 56:
						num = 17;
						continue;
					case 57:
						num = 39;
						continue;
					case 58:
						goto IL_516;
					case 59:
						goto IL_256;
					case 60:
						if (attribute7 != null)
						{
							num = 47;
							continue;
						}
						goto IL_7DC;
					case 61:
						num = 58;
						continue;
					case 62:
						goto IL_45F;
					case 63:
					{
						string a;
						if ((a = attribute7) != null)
						{
							num = 4;
							continue;
						}
						goto IL_516;
					}
					}
					break;
					IL_256:
					attribute3 = A_0.GetAttribute(ClipboardData.b("๥㭧ᩩ൫൭ᕯ", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧\udda9쎫\udcad풯슱욳\ud9b5\udbb7\udfb9쾻춽ꦿ곁ꏃꯅ꓇ﻋﻍﯓ믕맗동닛", a_));
					num = 14;
					continue;
					IL_2CD:
					A_1.WrapFrameAround = true;
					num = 59;
					continue;
					IL_38F:
					attribute2 = A_0.GetAttribute(ClipboardData.b("ṥ", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧\udda9쎫\udcad풯슱욳\ud9b5\udbb7\udfb9쾻춽ꦿ곁ꏃꯅ꓇ﻋﻍﯓ믕맗동닛", a_));
					num = 41;
					continue;
					IL_45F:
					attribute7 = A_0.GetAttribute(ClipboardData.b("ὥ⥧٩ի७ṯ", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧\udda9쎫\udcad풯슱욳\ud9b5\udbb7\udfb9쾻춽ꦿ곁ꏃꯅ꓇ﻋﻍﯓ믕맗동닛", a_));
					num = 60;
					continue;
					IL_516:
					attribute5 = A_0.GetAttribute(ClipboardData.b("ᅥᩧ୩ᱫ", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧\udda9쎫\udcad풯슱욳\ud9b5\udbb7\udfb9쾻춽ꦿ곁ꏃꯅ꓇ﻋﻍﯓ믕맗동닛", a_));
					num = 11;
					continue;
					IL_6EC:
					attribute6 = A_0.GetAttribute(ClipboardData.b("ၥ㭧ᩩ൫൭ᕯ", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧\udda9쎫\udcad풯슱욳\ud9b5\udbb7\udfb9쾻춽ꦿ곁ꏃꯅ꓇ﻋﻍﯓ믕맗동닛", a_));
					num = 20;
					continue;
					IL_71C:
					if (!string.IsNullOrEmpty(attribute6))
					{
						num = 23;
						continue;
					}
					return;
					IL_7DC:
					attribute4 = A_0.GetAttribute(ClipboardData.b("ὥ", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧\udda9쎫\udcad풯슱욳\ud9b5\udbb7\udfb9쾻춽ꦿ곁ꏃꯅ꓇ﻋﻍﯓ믕맗동닛", a_));
					num = 36;
				}
			}
			return;
		}
	}

	// Token: 0x06001F8C RID: 8076 RVA: 0x002158DC File Offset: 0x002148DC
	private void ᜆ(XmlReader A_0, ParagraphFormat A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 15;
			for (;;)
			{
				float num2;
				string attribute2;
				switch (num)
				{
				case 0:
					num = 8;
					continue;
				case 1:
					A_0.Read();
					num = 17;
					continue;
				case 2:
					if (A_0.NodeType != XmlNodeType.EndElement)
					{
						num = 23;
						continue;
					}
					return;
				case 3:
					goto IL_DE;
				case 4:
					goto IL_266;
				case 5:
				{
					Tab tab;
					string attribute;
					tab.Justification = this.ᜈ(attribute);
					num = 25;
					continue;
				}
				case 6:
					goto IL_17D;
				case 7:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 20;
						continue;
					}
					goto IL_3D4;
				}
				case 8:
				{
					string attribute;
					if (attribute == ClipboardData.b("ᝳ᩵ᵷ᭹๻", a_))
					{
						num = 24;
						continue;
					}
					Tab tab;
					tab.Position = num2;
					num = 16;
					continue;
				}
				case 9:
				{
					string localName;
					if (localName == ClipboardData.b("s᝵᩷", a_))
					{
						num = 33;
						continue;
					}
					goto IL_3D4;
				}
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_17D;
					default:
						goto IL_176;
					}
					break;
				case 11:
					if (attribute2 != null)
					{
						num = 19;
						continue;
					}
					goto IL_3D4;
				case 12:
				{
					bool flag;
					if (!flag)
					{
						num = 1;
						continue;
					}
					goto IL_266;
				}
				case 13:
					goto IL_1C5;
				case 14:
					goto IL_1C5;
				case 16:
					goto IL_17D;
				case 17:
					goto IL_266;
				case 18:
				{
					if (A_0.IsEmptyElement)
					{
						num = 10;
						continue;
					}
					bool flag = false;
					A_0.Read();
					this.ᜀ(A_0);
					num = 13;
					continue;
				}
				case 19:
				{
					Tab tab;
					tab.TabLeader = this.ᜇ(attribute2);
					num = 30;
					continue;
				}
				case 20:
					if (true)
					{
					}
					num = 9;
					continue;
				case 21:
					if (num2 != 3.4028235E+38f)
					{
						num = 0;
						continue;
					}
					goto IL_17D;
				case 22:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 28;
						continue;
					}
					A_0.Read();
					num = 4;
					continue;
				case 23:
					num = 27;
					continue;
				case 24:
				{
					Tab tab;
					tab.DeletePosition = num2 * 20f;
					num = 6;
					continue;
				}
				case 25:
					goto IL_1EF;
				case 26:
					num = 32;
					continue;
				case 27:
				{
					if (!(A_0.LocalName != ClipboardData.b("s᝵᩷ॹ", a_)))
					{
						num = 29;
						continue;
					}
					bool flag = false;
					num = 22;
					continue;
				}
				case 28:
					num = 7;
					continue;
				case 29:
					return;
				case 30:
					goto IL_3D4;
				case 31:
				{
					string attribute;
					if (attribute != null)
					{
						num = 26;
						continue;
					}
					goto IL_1EF;
				}
				case 32:
				{
					string attribute;
					if (attribute != ClipboardData.b("ᝳ᩵ᵷ᭹๻", a_))
					{
						num = 5;
						continue;
					}
					goto IL_1EF;
				}
				case 33:
				{
					Tab tab = A_1.Tabs.AddTab();
					string attribute = A_0.GetAttribute(ClipboardData.b("ɳ᝵ᑷ", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
					num = 31;
					continue;
				}
				}
				if (A_0.LocalName != ClipboardData.b("s᝵᩷ॹ", a_))
				{
					num = 3;
					continue;
				}
				num = 18;
				continue;
				IL_17D:
				attribute2 = A_0.GetAttribute(ClipboardData.b("ᡳ፵᥷ṹ᥻౽", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
				num = 11;
				continue;
				IL_1C5:
				num = 2;
				continue;
				IL_1EF:
				num2 = this.ᜀ(A_0, ClipboardData.b("ѳ᥵୷", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
				num = 21;
				continue;
				IL_266:
				this.ᜀ(A_0);
				num = 14;
				continue;
				IL_3D4:
				num = 12;
			}
			IL_DE:
			throw new XmlException(ClipboardData.b("⁳᝵᩷婹ύᅽﲇ", a_));
			IL_176:
			if (false)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06001F8D RID: 8077 RVA: 0x00215D70 File Offset: 0x00214D70
	private Spire.Doc.Documents.TabJustification ᜈ(string A_0)
	{
		int a_ = 5;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_71;
			case 1:
				num = 8;
				continue;
			case 2:
				if (!(A_0 == ClipboardData.b("ժᡬɮ", a_)))
				{
					num = 10;
					continue;
				}
				return Spire.Doc.Documents.TabJustification.List;
			case 3:
				num = 7;
				continue;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 12;
					continue;
				}
				break;
			case 6:
				num = 2;
				continue;
			case 7:
				if (!(A_0 == ClipboardData.b("४౬ᵮ", a_)))
				{
					num = 6;
					continue;
				}
				return Spire.Doc.Documents.TabJustification.Bar;
			case 8:
				if (!(A_0 == ClipboardData.b("ᥪѬ࡮ᥰݲ", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_5F;
			case 9:
				num = 11;
				continue;
			case 10:
				num = 0;
				continue;
			case 11:
				if (!(A_0 == ClipboardData.b("ࡪ࡬Ůհᙲݴ", a_)))
				{
					num = 1;
					continue;
				}
				return Spire.Doc.Documents.TabJustification.Centered;
			case 12:
				if (!(A_0 == ClipboardData.b("ཪ࡬౮ᡰṲᑴ᭶", a_)))
				{
					num = 3;
					continue;
				}
				return Spire.Doc.Documents.TabJustification.Decimal;
			}
			if (A_0 == null)
			{
				return Spire.Doc.Documents.TabJustification.Left;
			}
			num = 9;
		}
		IL_5F:
		if (true)
		{
		}
		return Spire.Doc.Documents.TabJustification.Right;
		IL_71:
		return Spire.Doc.Documents.TabJustification.Left;
	}

	// Token: 0x06001F8E RID: 8078 RVA: 0x00215F10 File Offset: 0x00214F10
	private Spire.Doc.Documents.TabLeader ᜇ(string A_0)
	{
		int a_ = 17;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_11F;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_11F;
				default:
					if (false)
					{
					}
					if (!(A_0 == ClipboardData.b("ὶx୺ᕼ᩾", a_)))
					{
						num = 2;
						continue;
					}
					return Spire.Doc.Documents.TabLeader.Hyphenated;
				}
				break;
			case 2:
				num = 6;
				continue;
			case 3:
				num = 8;
				continue;
			case 4:
				num = 9;
				continue;
			case 5:
				goto IL_6B;
			case 6:
				if (!(A_0 == ClipboardData.b("ɶ᝸ὺ᡼ൾ", a_)))
				{
					num = 4;
					continue;
				}
				return Spire.Doc.Documents.TabLeader.Single;
			case 8:
				if (!(A_0 == ClipboardData.b("፶ᙸེ", a_)))
				{
					num = 10;
					continue;
				}
				return Spire.Doc.Documents.TabLeader.Dotted;
			case 9:
				if (!(A_0 == ClipboardData.b("ὶᱸ᩺୼پ", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_E9;
			case 10:
				num = 1;
				continue;
			}
			if (A_0 != null)
			{
				num = 3;
				continue;
			}
			return Spire.Doc.Documents.TabLeader.NoLeader;
			IL_11F:
			num = 5;
		}
		return Spire.Doc.Documents.TabLeader.Hyphenated;
		IL_6B:
		return Spire.Doc.Documents.TabLeader.NoLeader;
		IL_E9:
		if (true)
		{
		}
		return Spire.Doc.Documents.TabLeader.Heavy;
	}

	// Token: 0x06001F8F RID: 8079 RVA: 0x00216074 File Offset: 0x00215074
	private void ᜅ(XmlReader A_0, ParagraphFormat A_1)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_1.IsChangedFormat = true;
		ParagraphFormat a_ = new ParagraphFormat(this.ᜄ);
		if (!A_0.IsEmptyElement)
		{
			A_0.Read();
			this.ᜀ(A_0);
			this.ᜋ(A_0, a_);
			spr\u1CC1 spr_u1CC = new spr\u1CC1(9828);
			spr_u1CC.ᜁ(true);
			A_1.Sprms.ᜆ(spr_u1CC);
			A_1.Sprms.ᜂ().Reverse();
			return;
		}
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x00216114 File Offset: 0x00215114
	private void ᜀ(ParagraphBase A_0)
	{
		if (true)
		{
		}
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0.\u1713(true);
				num = 3;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_87;
				default:
					goto IL_53;
				}
				break;
			case 2:
				if (this.\u171E == TrackChangeType.IsInsert)
				{
					goto IL_87;
				}
				return;
			case 3:
				return;
			}
			if (this.\u171E == TrackChangeType.IsDelete)
			{
				num = 1;
				continue;
			}
			num = 2;
			continue;
			IL_87:
			num = 0;
		}
		IL_53:
		if (false)
		{
		}
		A_0.\u1712(true);
	}

	// Token: 0x06001F91 RID: 8081 RVA: 0x002161BC File Offset: 0x002151BC
	private void ᜂ(XmlReader A_0, IDocumentObject A_1)
	{
		for (;;)
		{
			Borders borders = null;
			int num = 23;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
					return;
				case 2:
					borders = (A_1 as TableRow).TrackRowFormat.Borders;
					num = 12;
					continue;
				case 3:
					if (A_1 is TableRow)
					{
						num = 0;
						continue;
					}
					num = 4;
					continue;
				case 4:
					if (A_1 is TableCell)
					{
						num = 24;
						continue;
					}
					num = 26;
					continue;
				case 5:
					if (this.ᜇ)
					{
						num = 2;
						continue;
					}
					borders = (A_1 as TableRow).RowFormat.Borders;
					num = 16;
					continue;
				case 6:
					this.ᜀ(A_0, borders, A_1);
					num = 1;
					continue;
				case 7:
					num = 8;
					continue;
				case 8:
					if (this.ᜉ)
					{
						num = 13;
						continue;
					}
					borders = (A_1 as Table).DocxTableFormat.Format.Borders;
					num = 9;
					continue;
				case 9:
					goto IL_16F;
				case 10:
					goto IL_16F;
				case 11:
					borders = (A_1 as Section).PageSetup.Borders;
					num = 22;
					continue;
				case 12:
					goto IL_16F;
				case 13:
					borders = (A_1 as Table).TrackTblFormat.Format.Borders;
					num = 25;
					continue;
				case 14:
					goto IL_16F;
				case 15:
					borders = (A_1 as Paragraph).Format.Borders;
					num = 21;
					continue;
				case 16:
					goto IL_16F;
				case 17:
					borders = (A_1 as TableCell).TrackCellFormat.Borders;
					num = 10;
					continue;
				case 18:
					if (borders != null)
					{
						num = 6;
						continue;
					}
					return;
				case 19:
					if (A_1 is Paragraph)
					{
						num = 15;
						continue;
					}
					goto IL_16F;
				case 20:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A1;
					default:
						if (false)
						{
						}
						if (this.ᜈ)
						{
							num = 17;
							continue;
						}
						borders = (A_1 as TableCell).CellFormat.Borders;
						if (true)
						{
						}
						num = 14;
						continue;
					}
					break;
				case 21:
					goto IL_16F;
				case 22:
					goto IL_16F;
				case 23:
					if (A_1 is Table)
					{
						num = 7;
						continue;
					}
					num = 3;
					continue;
				case 24:
					num = 20;
					continue;
				case 25:
					goto IL_16F;
				case 26:
					goto IL_A1;
				}
				break;
				IL_A1:
				if (A_1 is Section)
				{
					num = 11;
					continue;
				}
				num = 19;
				continue;
				IL_16F:
				num = 18;
			}
		}
	}

	// Token: 0x06001F92 RID: 8082 RVA: 0x002164D0 File Offset: 0x002154D0
	private void ᜀ(XmlReader A_0, Borders A_1)
	{
		int a_ = 19;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				int num2;
				switch (num2)
				{
				case 0:
					this.ᜀ(A_0, A_1.Top);
					num = 18;
					continue;
				case 1:
					this.ᜀ(A_0, A_1.Left);
					num = 13;
					continue;
				case 2:
					this.ᜀ(A_0, A_1.Bottom);
					num = 28;
					continue;
				case 3:
					this.ᜀ(A_0, A_1.Right);
					num = 15;
					continue;
				case 4:
				case 5:
					this.ᜀ(A_0, A_1.Horizontal);
					num = 24;
					continue;
				case 6:
				case 7:
					this.ᜀ(A_0, A_1.Vertical);
					num = 1;
					continue;
				case 8:
					this.ᜀ(A_0, A_1.DiagonalDown);
					num = 5;
					continue;
				case 9:
					this.ᜀ(A_0, A_1.DiagonalUp);
					num = 3;
					continue;
				default:
					num = 6;
					continue;
				}
				break;
			}
			case 1:
				goto IL_393;
			case 2:
			{
				string localName;
				if (!(A_0.LocalName != localName))
				{
					num = 22;
					continue;
				}
				num = 9;
				continue;
			}
			case 3:
				goto IL_393;
			case 5:
				goto IL_393;
			case 6:
				num = 21;
				continue;
			case 7:
				num = 0;
				continue;
			case 8:
				goto IL_14D;
			case 9:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					if (true)
					{
					}
					num = 20;
					continue;
				}
				A_0.Read();
				num = 27;
				continue;
			case 10:
				goto IL_1D3;
			case 11:
			{
				int num2;
				string localName2;
				if (spr᧓.ᝰ.TryGetValue(localName2, out num2))
				{
					num = 7;
					continue;
				}
				goto IL_393;
			}
			case 12:
				return;
			case 13:
				goto IL_393;
			case 14:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_38E;
				default:
					if (false)
					{
					}
					num = 30;
					continue;
				}
				break;
			case 15:
				goto IL_393;
			case 16:
			{
				string localName2;
				if ((localName2 = A_0.LocalName) != null)
				{
					num = 14;
					continue;
				}
				goto IL_393;
			}
			case 17:
			{
				if (A_0.IsEmptyElement)
				{
					num = 12;
					continue;
				}
				string localName = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				num = 8;
				continue;
			}
			case 18:
				goto IL_393;
			case 19:
				goto IL_38E;
			case 20:
				num = 16;
				continue;
			case 21:
				goto IL_393;
			case 22:
				goto IL_211;
			case 23:
				goto IL_AD;
			case 24:
				goto IL_393;
			case 25:
				spr᧓.ᝰ = new Dictionary<string, int>(10)
				{
					{
						ClipboardData.b("൸ᑺർ", a_),
						0
					},
					{
						ClipboardData.b("ᕸṺ᭼୾", a_),
						1
					},
					{
						ClipboardData.b("᭸ᑺॼ୾", a_),
						2
					},
					{
						ClipboardData.b("୸ቺ᩼᝾", a_),
						3
					},
					{
						ClipboardData.b("᭸Ṻॼࡾ", a_),
						4
					},
					{
						ClipboardData.b("ၸᕺ๼ᙾ춄", a_),
						5
					},
					{
						ClipboardData.b("᭸ོ᩺", a_),
						6
					},
					{
						ClipboardData.b("ၸᕺ๼ᙾ펄", a_),
						7
					},
					{
						ClipboardData.b("൸᝺佼ᵾ", a_),
						8
					},
					{
						ClipboardData.b("൸ॺ佼ᵾ", a_),
						9
					}
				};
				num = 19;
				continue;
			case 26:
				goto IL_14D;
			case 27:
				goto IL_1D3;
			case 28:
				goto IL_393;
			case 29:
				num = 2;
				continue;
			case 30:
				if (spr᧓.ᝰ == null)
				{
					num = 25;
					continue;
				}
				goto IL_216;
			case 31:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 29;
					continue;
				}
				return;
			}
			if (A_1 == null)
			{
				num = 23;
				continue;
			}
			num = 17;
			continue;
			IL_14D:
			num = 31;
			continue;
			IL_1D3:
			this.ᜀ(A_0);
			num = 26;
			continue;
			IL_216:
			num = 11;
			continue;
			IL_38E:
			goto IL_216;
			IL_393:
			A_0.Read();
			num = 10;
		}
		IL_AD:
		throw new ArgumentException(ClipboardData.b("㭸ᑺོ᭾Ꞇ愈搜﶐떔練붜ﶞ쒠莢쮤튦얨잪", a_));
		IL_211:;
	}

	// Token: 0x06001F93 RID: 8083 RVA: 0x0021697C File Offset: 0x0021597C
	private void ᜀ(XmlReader A_0, Borders A_1, IDocumentObject A_2)
	{
		int a_ = 19;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
			{
				string localName;
				if (!(A_0.LocalName != localName))
				{
					num = 12;
					continue;
				}
				num = 21;
				continue;
			}
			case 2:
				goto IL_395;
			case 3:
			{
				string localName2;
				if ((localName2 = A_0.LocalName) != null)
				{
					num = 6;
					continue;
				}
				goto IL_395;
			}
			case 4:
				goto IL_AD;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_390;
				default:
					if (false)
					{
					}
					num = 17;
					continue;
				}
				break;
			case 7:
				goto IL_395;
			case 8:
				goto IL_395;
			case 9:
				goto IL_395;
			case 10:
				goto IL_395;
			case 11:
				goto IL_395;
			case 12:
				goto IL_20B;
			case 13:
			{
				if (A_0.IsEmptyElement)
				{
					num = 0;
					continue;
				}
				string localName = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				if (true)
				{
				}
				num = 27;
				continue;
			}
			case 14:
				num = 26;
				continue;
			case 15:
				goto IL_395;
			case 16:
				goto IL_390;
			case 17:
				if (spr᧓.\u1771 == null)
				{
					num = 28;
					continue;
				}
				goto IL_210;
			case 18:
				num = 3;
				continue;
			case 19:
				goto IL_146;
			case 20:
				goto IL_1CD;
			case 21:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 18;
					continue;
				}
				A_0.Read();
				num = 20;
				continue;
			case 22:
				num = 25;
				continue;
			case 23:
			{
				string localName2;
				int num2;
				if (spr᧓.\u1771.TryGetValue(localName2, out num2))
				{
					num = 22;
					continue;
				}
				goto IL_395;
			}
			case 24:
				goto IL_395;
			case 25:
			{
				int num2;
				switch (num2)
				{
				case 0:
					this.ᜀ(A_0, A_1.Top, A_2);
					num = 24;
					continue;
				case 1:
					this.ᜀ(A_0, A_1.Left, A_2);
					num = 2;
					continue;
				case 2:
					this.ᜀ(A_0, A_1.Bottom, A_2);
					num = 7;
					continue;
				case 3:
					this.ᜀ(A_0, A_1.Right, A_2);
					num = 8;
					continue;
				case 4:
				case 5:
					this.ᜀ(A_0, A_1.Horizontal);
					num = 15;
					continue;
				case 6:
				case 7:
					this.ᜀ(A_0, A_1.Vertical);
					num = 11;
					continue;
				case 8:
					this.ᜀ(A_0, A_1.DiagonalDown);
					num = 9;
					continue;
				case 9:
					this.ᜀ(A_0, A_1.DiagonalUp);
					num = 10;
					continue;
				default:
					num = 14;
					continue;
				}
				break;
			}
			case 26:
				goto IL_395;
			case 27:
				goto IL_146;
			case 28:
				spr᧓.\u1771 = new Dictionary<string, int>(10)
				{
					{
						ClipboardData.b("൸ᑺർ", a_),
						0
					},
					{
						ClipboardData.b("ᕸṺ᭼୾", a_),
						1
					},
					{
						ClipboardData.b("᭸ᑺॼ୾", a_),
						2
					},
					{
						ClipboardData.b("୸ቺ᩼᝾", a_),
						3
					},
					{
						ClipboardData.b("᭸Ṻॼࡾ", a_),
						4
					},
					{
						ClipboardData.b("ၸᕺ๼ᙾ춄", a_),
						5
					},
					{
						ClipboardData.b("᭸ོ᩺", a_),
						6
					},
					{
						ClipboardData.b("ၸᕺ๼ᙾ펄", a_),
						7
					},
					{
						ClipboardData.b("൸᝺佼ᵾ", a_),
						8
					},
					{
						ClipboardData.b("൸ॺ佼ᵾ", a_),
						9
					}
				};
				num = 16;
				continue;
			case 29:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 30;
					continue;
				}
				return;
			case 30:
				num = 1;
				continue;
			case 31:
				goto IL_1CD;
			}
			if (A_1 == null)
			{
				num = 4;
				continue;
			}
			num = 13;
			continue;
			IL_146:
			num = 29;
			continue;
			IL_1CD:
			this.ᜀ(A_0);
			num = 19;
			continue;
			IL_210:
			num = 23;
			continue;
			IL_390:
			goto IL_210;
			IL_395:
			A_0.Read();
			num = 31;
		}
		IL_AD:
		throw new ArgumentException(ClipboardData.b("㭸ᑺོ᭾Ꞇ愈搜﶐떔練붜ﶞ쒠莢쮤튦얨잪", a_));
		IL_20B:;
	}

	// Token: 0x06001F94 RID: 8084 RVA: 0x00216E2C File Offset: 0x00215E2C
	private void ᜄ(XmlReader A_0, ParagraphFormat A_1)
	{
		int a_ = 7;
		int num = 6;
		for (;;)
		{
			float num2;
			string attribute;
			switch (num)
			{
			case 0:
				goto IL_1DE;
			case 1:
				A_1.AfterSpacing = num2;
				num = 15;
				continue;
			case 2:
				return;
			case 3:
				A_1.IsSpacingBeforeAuto = (attribute == ClipboardData.b("屬", a_));
				num = 13;
				continue;
			case 4:
				num = 3;
				continue;
			case 5:
				goto IL_170;
			case 7:
				if (num2 != 3.4028235E+38f)
				{
					num = 9;
					continue;
				}
				goto IL_170;
			case 8:
				num = 14;
				continue;
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_C9;
				default:
					if (false)
					{
					}
					A_1.BeforeSpacing = num2;
					num = 5;
					continue;
				}
				break;
			case 10:
				if (attribute != null)
				{
					num = 8;
					continue;
				}
				goto IL_1DE;
			case 11:
				if (true)
				{
				}
				if (attribute != null)
				{
					num = 4;
					continue;
				}
				goto IL_25C;
			case 12:
				if (num2 != 3.4028235E+38f)
				{
					num = 1;
					continue;
				}
				goto IL_A6;
			case 13:
				goto IL_16B;
			case 14:
				A_1.IsSpacingAfterAuto = (attribute == ClipboardData.b("屬", a_));
				num = 0;
				continue;
			case 15:
				goto IL_A6;
			}
			if (A_0.AttributeCount == 0)
			{
				num = 2;
				continue;
			}
			num2 = this.ᜀ(A_0, ClipboardData.b("ཬ੮ᝰᱲݴቶ", a_), ClipboardData.b("լ᭮հͲ佴塶噸ࡺṼ᝾ꞈﶌﾐﮖﾘ삠힢횤覦욨\ud9aa쪬肮우\udcb2잴펶즸즺튼\udcbe꓀냂뛄껆ꟈ곊ꃌꏎﻐ냜뻞裠跢", a_));
			num = 7;
			continue;
			IL_C9:
			num = 10;
			continue;
			IL_A6:
			attribute = A_0.GetAttribute(ClipboardData.b("ཬ੮ᝰᱲݴቶ㡸๺ॼၾ", a_), ClipboardData.b("լ᭮հͲ佴塶噸ࡺṼ᝾ꞈﶌﾐﮖﾘ삠힢횤覦욨\ud9aa쪬肮우\udcb2잴펶즸즺튼\udcbe꓀냂뛄껆ꟈ곊ꃌꏎﻐ냜뻞裠跢", a_));
			goto IL_C9;
			IL_170:
			num2 = this.ᜀ(A_0, ClipboardData.b("౬८հᙲݴ", a_), ClipboardData.b("լ᭮հͲ佴塶噸ࡺṼ᝾ꞈﶌﾐﮖﾘ삠힢횤覦욨\ud9aa쪬肮우\udcb2잴펶즸즺튼\udcbe꓀냂뛄껆ꟈ곊ꃌꏎﻐ냜뻞裠跢", a_));
			num = 12;
			continue;
			IL_1DE:
			attribute = A_0.GetAttribute(ClipboardData.b("౬८հᙲݴ㙶౸ེቼ౾", a_), ClipboardData.b("լ᭮հͲ佴塶噸ࡺṼ᝾ꞈﶌﾐﮖﾘ삠힢횤覦욨\ud9aa쪬肮우\udcb2잴펶즸즺튼\udcbe꓀냂뛄껆ꟈ곊ꃌꏎﻐ냜뻞裠跢", a_));
			num = 11;
		}
		return;
		IL_16B:
		IL_25C:
		this.ᜃ(A_0, A_1);
	}

	// Token: 0x06001F95 RID: 8085 RVA: 0x002170A0 File Offset: 0x002160A0
	private void ᜃ(XmlReader A_0, ParagraphFormat A_1)
	{
		int a_ = 6;
		for (;;)
		{
			float num = this.ᜀ(A_0, ClipboardData.b("kݭṯ᝱", a_), ClipboardData.b("ѫᩭѯɱ乳奵坷ॹύᙽꚇﲋﺏ煉歹ﺗ솟횡힣袥잧\ud8a9쮫膭잯\uddb1욳튵좷좹펻\uddbdꖿ뇁럃꿅ꛇ귉ꇋꋍￏ뇛뿝觟賡", a_));
			int num2 = 6;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_F7;
				case 1:
				{
					string a;
					if (!(a == ClipboardData.b("൫ᩭ㱯᝱ᕳյ౷", a_)))
					{
						num2 = 10;
						continue;
					}
					goto IL_8C;
				}
				case 2:
				{
					A_1.LineSpacing = 0f;
					A_1.LineSpacing = num;
					string attribute = A_0.GetAttribute(ClipboardData.b("kݭṯ᝱♳͵ᑷό", a_), ClipboardData.b("ѫᩭѯɱ乳奵坷ॹύᙽꚇﲋﺏ煉歹ﺗ솟횡힣袥잧\ud8a9쮫膭잯\uddb1욳튵좷좹펻\uddbdꖿ뇁럃꿅ꛇ귉ꇋꋍￏ뇛뿝觟賡", a_));
					num2 = 7;
					continue;
				}
				case 3:
				{
					string a;
					string attribute;
					if ((a = attribute) != null)
					{
						num2 = 9;
						continue;
					}
					goto IL_E5;
				}
				case 4:
					goto IL_E5;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6D;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num2 = 4;
						continue;
					}
					break;
				case 6:
					goto IL_6D;
				case 7:
				{
					string attribute;
					if (attribute != null)
					{
						num2 = 11;
						continue;
					}
					return;
				}
				case 8:
				{
					string a;
					if (!(a == ClipboardData.b("५᙭ᅯᅱs", a_)))
					{
						num2 = 5;
						continue;
					}
					goto IL_14F;
				}
				case 9:
					num2 = 1;
					continue;
				case 10:
					num2 = 8;
					continue;
				case 11:
					num2 = 3;
					continue;
				}
				break;
				IL_6D:
				if (num != 3.4028235E+38f)
				{
					num2 = 2;
					continue;
				}
				return;
				IL_E5:
				A_1.LineSpacingRule = LineSpacingRule.Multiple;
				num2 = 0;
			}
		}
		IL_8C:
		A_1.LineSpacingRule = LineSpacingRule.AtLeast;
		return;
		IL_F7:
		return;
		IL_14F:
		A_1.LineSpacingRule = LineSpacingRule.Exactly;
	}

	// Token: 0x06001F96 RID: 8086 RVA: 0x00217268 File Offset: 0x00216268
	private void ᜂ(XmlReader A_0, ParagraphFormat A_1)
	{
		int a_ = 3;
		int num = 3;
		for (;;)
		{
			float num2;
			switch (num)
			{
			case 0:
				if (num2 != 3.4028235E+38f)
				{
					num = 9;
					continue;
				}
				goto IL_14C;
			case 1:
				A_1.FirstLineIndentChars = num2 * 20f / 100f;
				num = 22;
				continue;
			case 2:
				A_1.FirstLineIndent = -num2;
				num = 11;
				continue;
			case 4:
				if (num2 != 3.4028235E+38f)
				{
					num = 15;
					continue;
				}
				goto IL_275;
			case 5:
				goto IL_275;
			case 6:
				if (num2 != 3.4028235E+38f)
				{
					num = 20;
					continue;
				}
				goto IL_9C;
			case 7:
				A_1.FirstLineIndentChars = -(num2 * 20f / 100f);
				num = 18;
				continue;
			case 8:
				A_1.RightIndent = num2;
				num = 10;
				continue;
			case 9:
				A_1.LeftIndent = num2;
				num = 17;
				continue;
			case 10:
				IL_2D1:
				goto IL_341;
			case 11:
				goto IL_E6;
			case 12:
				if (num2 != 3.4028235E+38f)
				{
					num = 1;
					continue;
				}
				goto IL_22B;
			case 13:
				if (num2 != 3.4028235E+38f)
				{
					num = 16;
					continue;
				}
				goto IL_2D3;
			case 14:
				if (num2 != 3.4028235E+38f)
				{
					num = 7;
					continue;
				}
				return;
			case 15:
				A_1.FirstLineIndent = num2;
				num = 5;
				continue;
			case 16:
				A_1.RightIndentChars = num2 * 20f / 100f;
				num = 24;
				continue;
			case 17:
				goto IL_14C;
			case 18:
				return;
			case 19:
				if (num2 != 3.4028235E+38f)
				{
					num = 2;
					continue;
				}
				goto IL_E6;
			case 20:
				A_1.LeftIndentChars = num2 * 20f / 100f;
				num = 21;
				continue;
			case 21:
				goto IL_9C;
			case 22:
				goto IL_22B;
			case 23:
				return;
			case 24:
				goto IL_2D3;
			case 25:
				if (true)
				{
				}
				if (num2 != 3.4028235E+38f)
				{
					num = 8;
					continue;
				}
				goto IL_341;
			}
			if (A_0.AttributeCount == 0)
			{
				num = 23;
				continue;
			}
			num2 = this.ᜀ(A_0, ClipboardData.b("ը๪୬᭮", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_));
			num = 0;
			continue;
			IL_9C:
			num2 = this.ᜀ(A_0, ClipboardData.b("᭨ɪ੬ݮհひᵴᙶ୸ࡺ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_));
			num = 13;
			continue;
			IL_E6:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_2D1;
			default:
				if (false)
				{
				}
				num2 = this.ᜀ(A_0, ClipboardData.b("ը๪୬᭮㉰᭲ᑴն੸", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_));
				num = 6;
				continue;
			}
			IL_14C:
			num2 = this.ᜀ(A_0, ClipboardData.b("᭨ɪ੬ݮհ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_));
			num = 25;
			continue;
			IL_22B:
			num2 = this.ᜀ(A_0, ClipboardData.b("Ũ੪ͬ࡮ᡰᵲቴ㑶ᅸོ᩺౾", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_));
			num = 14;
			continue;
			IL_275:
			num2 = this.ᜀ(A_0, ClipboardData.b("Ũ੪ͬ࡮ᡰᵲቴ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_));
			num = 19;
			continue;
			IL_2D3:
			num2 = this.ᜀ(A_0, ClipboardData.b("ཨɪὬᱮհ㽲ᱴ᥶ᱸ㡺ᕼṾ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_));
			num = 12;
			continue;
			IL_341:
			num2 = this.ᜀ(A_0, ClipboardData.b("ཨɪὬᱮհ㽲ᱴ᥶ᱸ", a_), ClipboardData.b("ŨὪᥬὮ䭰屲婴Ѷ᩸፺᡼ቾꮄ麗ﲐﾒﲜ튠趢쪤햦캨蒪\udaac삮쎰ힲ어얶횸\ud8ba\ud8bc첾닀ꫂꯄꃆ꓈ꟊ﷎듘뫚드뇞", a_));
			num = 4;
		}
	}

	// Token: 0x06001F97 RID: 8087 RVA: 0x00217674 File Offset: 0x00216674
	private void ᜁ(XmlReader A_0, ParagraphFormat A_1)
	{
		int a_ = 19;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ླྀ᩺ᅼ", a_), ClipboardData.b("ᅸེॼཾ뮀겂ꪄ뮔ﺚ철쾢쎤좦\udba8욪첬\udbae슰鶲\udab4얶\udeb8钺쪼킾돀Ꟃ뗄뗆ꛈ꣊꣌볎ꋐ뫒믔냖듘럚퇠폢폤죦蓨諪蓬臮", a_));
			int num = 0;
			for (;;)
			{
				string a;
				switch (num)
				{
				case 0:
					if (attribute != null)
					{
						num = 7;
						continue;
					}
					return;
				case 1:
					num = 13;
					continue;
				case 2:
					goto IL_11A;
				case 3:
					if ((a = attribute) != null)
					{
						num = 4;
						continue;
					}
					goto IL_11A;
				case 4:
					num = 9;
					continue;
				case 5:
					num = 6;
					continue;
				case 6:
					if (true)
					{
					}
					if (!(a == ClipboardData.b("ᕸᑺ੼㑾", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_A0;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1A6;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 8:
					num = 11;
					continue;
				case 9:
					if (!(a == ClipboardData.b("᩸Ṻ፼୾", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_112;
				case 10:
					return;
				case 11:
					if (!(a == ClipboardData.b("୸ቺ᩼᝾", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_A0;
				case 12:
					num = 2;
					continue;
				case 13:
					goto IL_1A6;
				}
				break;
				IL_11A:
				A_1.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
				num = 10;
				continue;
				IL_1A6:
				if (a == ClipboardData.b("᭸ᑺॼ᝾", a_))
				{
					goto IL_ED;
				}
				num = 12;
			}
		}
		IL_A0:
		A_1.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Right;
		return;
		IL_ED:
		A_1.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Justify;
		return;
		IL_112:
		A_1.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
	}

	// Token: 0x06001F98 RID: 8088 RVA: 0x00217850 File Offset: 0x00216850
	private void ᜀ(XmlReader A_0, ParagraphFormat A_1)
	{
		int a_ = 19;
		for (;;)
		{
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("᩸ᑺᅼၾ", a_), ClipboardData.b("ᅸེॼཾ뮀겂ꪄ뮔ﺚ철쾢쎤좦\udba8욪첬\udbae슰鶲\udab4얶\udeb8钺쪼킾돀Ꟃ뗄뗆ꛈ꣊꣌볎ꋐ뫒믔냖듘럚퇠폢폤죦蓨諪蓬臮", a_));
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						A_1.TextureStyle = this.ᜉ(attribute);
						num = 13;
						continue;
					case 1:
						if (attribute != null)
						{
							num = 0;
							continue;
						}
						return;
					case 2:
						if (attribute != null)
						{
							num = 7;
							continue;
						}
						goto IL_1C9;
					case 3:
						num = 10;
						continue;
					case 4:
						A_1.ForeColor = Color.Empty;
						num = 8;
						continue;
					case 5:
						goto IL_1C9;
					case 6:
						goto IL_10D;
					case 7:
						if (true)
						{
						}
						num = 14;
						continue;
					case 8:
						goto IL_10D;
					case 9:
						if (attribute != null)
						{
							num = 3;
							continue;
						}
						goto IL_10D;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (attribute == ClipboardData.b("ᡸ๺ॼၾ", a_))
							{
								num = 4;
								continue;
							}
							A_1.BackColor = this.ᜃ(attribute);
							num = 6;
							continue;
						}
						break;
					case 11:
						goto IL_1C9;
					case 12:
						A_1.BackColor = Color.Empty;
						num = 5;
						continue;
					case 13:
						return;
					case 14:
						if (attribute == ClipboardData.b("ᡸ๺ॼၾ", a_))
						{
							num = 12;
							continue;
						}
						A_1.ForeColor = this.ᜃ(attribute);
						num = 11;
						continue;
					}
					break;
					IL_10D:
					attribute = A_0.GetAttribute(ClipboardData.b("ླྀ᩺ᅼ", a_), ClipboardData.b("ᅸེॼཾ뮀겂ꪄ뮔ﺚ철쾢쎤좦\udba8욪첬\udbae슰鶲\udab4얶\udeb8钺쪼킾돀Ꟃ뗄뗆ꛈ꣊꣌볎ꋐ뫒믔냖듘럚퇠폢폤죦蓨諪蓬臮", a_));
					num = 1;
					continue;
					IL_1C9:
					attribute = A_0.GetAttribute(ClipboardData.b("ὸቺᅼ፾", a_), ClipboardData.b("ᅸེॼཾ뮀겂ꪄ뮔ﺚ철쾢쎤좦\udba8욪첬\udbae슰鶲\udab4얶\udeb8钺쪼킾돀Ꟃ뗄뗆ꛈ꣊꣌볎ꋐ뫒믔냖듘럚퇠폢폤죦蓨諪蓬臮", a_));
					num = 9;
				}
			}
		}
	}

	// Token: 0x06001F99 RID: 8089 RVA: 0x00217A84 File Offset: 0x00216A84
	private void ᜁ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 14;
			for (;;)
			{
				DictionaryEntry dictionaryEntry;
				bool flag;
				string attribute;
				switch (num)
				{
				case 0:
					goto IL_1B7;
				case 1:
					goto IL_1B7;
				case 2:
					goto IL_1B7;
				case 3:
					if (this.ᜂ(A_0))
					{
						num = 23;
						continue;
					}
					goto IL_1B7;
				case 4:
					goto IL_1B7;
				case 5:
					if (!(A_0.LocalName != ClipboardData.b("ٴቶེ᩸⵼ൾ", a_)))
					{
						num = 27;
						continue;
					}
					num = 13;
					continue;
				case 6:
					goto IL_1B7;
				case 7:
					goto IL_3BA;
				case 8:
					if (!((string)dictionaryEntry.Key == ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶횸\uddba\udbbc횾ꋀꛂ臄꣆꫈뻊ꃌ꫎뿐꟒䀹鏠蛢觤蛦鷨苪苬臮苰鯲鳴蟶諸퓺闼髾怀朂怄甆", a_)))
					{
						num = 20;
						continue;
					}
					num = 32;
					continue;
				case 9:
					goto IL_1B7;
				case 10:
					goto IL_112;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 41;
						continue;
					}
					break;
				case 12:
					goto IL_7DC;
				case 13:
					if (A_0.NodeType == XmlNodeType.Element)
					{
						num = 17;
						continue;
					}
					A_0.Read();
					num = 12;
					continue;
				case 15:
					goto IL_1B7;
				case 16:
					goto IL_3BA;
				case 17:
					num = 29;
					continue;
				case 18:
					flag = false;
					goto IL_524;
				case 19:
				{
					string localName;
					int num2;
					if (spr᧓.\u1772.TryGetValue(localName, out num2))
					{
						num = 11;
						continue;
					}
					goto IL_1B7;
				}
				case 20:
					num = 18;
					continue;
				case 21:
					goto IL_1B7;
				case 22:
					goto IL_1B7;
				case 23:
					(A_1 as Section).PageSetup.Margins.Right += this.ᜊ;
					num = 33;
					continue;
				case 24:
					goto IL_1B7;
				case 25:
					goto IL_1B7;
				case 26:
					num = 28;
					continue;
				case 27:
					return;
				case 28:
					goto IL_1B7;
				case 29:
				{
					string localName;
					if ((localName = A_0.LocalName) != null)
					{
						num = 39;
						continue;
					}
					goto IL_1B7;
				}
				case 30:
					goto IL_1B7;
				case 31:
					if (spr᧓.\u1772 == null)
					{
						num = 34;
						continue;
					}
					goto IL_62B;
				case 32:
					flag = true;
					goto IL_524;
				case 33:
					goto IL_1B7;
				case 34:
					spr᧓.\u1772 = new Dictionary<string, int>(18)
					{
						{
							ClipboardData.b("፴ᡶᙸེ᡼ൾ펀ﮈ", a_),
							0
						},
						{
							ClipboardData.b("ᵴቶᡸὺ᡼ൾ펀ﮈ", a_),
							1
						},
						{
							ClipboardData.b("Ŵ๶ॸṺ", a_),
							2
						},
						{
							ClipboardData.b("մၶ⩸ź", a_),
							3
						},
						{
							ClipboardData.b("մၶ㑸ོ᩺", a_),
							4
						},
						{
							ClipboardData.b("ᙴᡶᕸࡺ", a_),
							5
						},
						{
							ClipboardData.b("ŴṶ൸᝺᡼⽾", a_),
							6
						},
						{
							ClipboardData.b("մၶ㭸ᑺོ᭾", a_),
							7
						},
						{
							ClipboardData.b("ᅴᡶ᩸㱺ོᙾ", a_),
							8
						},
						{
							ClipboardData.b("ʹ㙶ᕸቺ᩼ᅾ", a_),
							9
						},
						{
							ClipboardData.b("ᥴ᥶㝸๺ၼ⭾", a_),
							10
						},
						{
							ClipboardData.b("፴ᡶᙸེ፼ၾ햄", a_),
							11
						},
						{
							ClipboardData.b("ၴ᥶ᵸᕺቼ୾펂", a_),
							12
						},
						{
							ClipboardData.b("ŴቶŸེ㥼ᙾ", a_),
							13
						},
						{
							ClipboardData.b("ݴͶᕸ㱺ࡼ୾", a_),
							14
						},
						{
							ClipboardData.b("մၶ㝸๺ၼ⭾", a_),
							15
						},
						{
							ClipboardData.b("᝴Ṷᵸቺ", a_),
							16
						},
						{
							ClipboardData.b("፴ᡶ୸ᙺ⵼ൾ", a_),
							17
						}
					};
					num = 43;
					continue;
				case 35:
					goto IL_75E;
				case 36:
					if (A_1 == null)
					{
						num = 35;
						continue;
					}
					num = 44;
					continue;
				case 37:
					goto IL_1B7;
				case 38:
					goto IL_1B7;
				case 39:
					num = 31;
					continue;
				case 40:
					return;
				case 41:
				{
					int num2;
					switch (num2)
					{
					case 0:
					case 1:
						dictionaryEntry = this.ᜎ[A_0.GetAttribute(ClipboardData.b("ݴ䵶ၸὺ", a_))];
						attribute = A_0.GetAttribute(ClipboardData.b("Ŵ๶ॸṺ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_));
						num = 8;
						continue;
					case 2:
					{
						string attribute2 = A_0.GetAttribute(ClipboardData.b("ʹᙶᕸ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_));
						this.ᜀ(A_1 as Section, attribute2);
						num = 38;
						continue;
					}
					case 3:
						this.ᜀ(A_0, A_1 as Section);
						num = 4;
						continue;
					case 4:
						this.ᜁ(A_0, A_1 as Section);
						num = 25;
						continue;
					case 5:
						this.ᜄ(A_0, A_1 as Section);
						if (true)
						{
						}
						num = 2;
						continue;
					case 6:
						(A_1 as Section).PageSetup.DifferentFirstPageHeaderFooter = true;
						num = 6;
						continue;
					case 7:
						this.ᜅ(A_0, A_1 as Section);
						num = 9;
						continue;
					case 8:
						this.ᜆ(A_0, A_1 as Section);
						num = 24;
						continue;
					case 9:
						this.ᜇ(A_0, A_1 as Section);
						num = 15;
						continue;
					case 10:
						this.ᜈ(A_0, A_1 as Section);
						num = 42;
						continue;
					case 11:
						this.ᜃ(A_0, true);
						num = 21;
						continue;
					case 12:
						this.ᜃ(A_0, false);
						num = 37;
						continue;
					case 13:
						this.ᜀ(A_0, A_1 as Section);
						num = 45;
						continue;
					case 14:
						num = 3;
						continue;
					case 15:
						this.ᜉ(A_0, A_1 as Section);
						num = 30;
						continue;
					case 16:
						(A_1 as Section).PageSetup.Bidi = this.ᜂ(A_0);
						num = 22;
						continue;
					case 17:
						(A_1 as Section).ProtectForm = this.ᜂ(A_0);
						num = 0;
						continue;
					default:
						num = 26;
						continue;
					}
					break;
				}
				case 42:
					goto IL_1B7;
				case 43:
					goto IL_62B;
				case 44:
					if (A_0.IsEmptyElement)
					{
						num = 40;
						continue;
					}
					A_0.Read();
					this.ᜀ(A_0);
					num = 16;
					continue;
				case 45:
					goto IL_1B7;
				case 46:
					goto IL_7DC;
				}
				if (A_0.LocalName != ClipboardData.b("ٴቶེ᩸⵼ൾ", a_))
				{
					num = 10;
					continue;
				}
				num = 36;
				continue;
				IL_1B7:
				A_0.Read();
				num = 46;
				continue;
				IL_3BA:
				num = 5;
				continue;
				IL_524:
				bool a_2 = flag;
				spr\u22A5 a_3 = this.ᜀ(ClipboardData.b("ɴᡶ୸ὺ剼", a_), dictionaryEntry.Value.ToString());
				this.ᜋ = dictionaryEntry.Value.ToString() + ClipboardData.b("孴նᱸ᝺๼", a_);
				this.ᜀ(this.ᜄ.LastSection.HeadersFooters, a_3, attribute, a_2);
				this.ᜋ = "";
				num = 1;
				continue;
				IL_62B:
				num = 19;
				continue;
				IL_7DC:
				this.ᜀ(A_0);
				num = 7;
			}
			IL_112:
			throw new XmlException(ClipboardData.b("♴ቶེ᩸ᑼၾꎂﮊﶎ朗", a_));
			IL_75E:
			throw new ArgumentException(ClipboardData.b("♴ቶེ᩸ᑼၾꎂﺊ놐ﶒ杖릘連뾞쾠횢즤쮦", a_));
		}
		}
	}

	// Token: 0x06001F9A RID: 8090 RVA: 0x00218320 File Offset: 0x00217320
	private void ᜀ(HeadersFooters A_0, spr\u22A5 A_1, string A_2, bool A_3)
	{
		int a_ = 6;
		for (;;)
		{
			IDocumentObject documentObject = null;
			XmlReader xmlReader = spr\u23D7.ᜀ(A_1.ᜁ());
			int num = 16;
			for (;;)
			{
				switch (num)
				{
				case 0:
					documentObject = A_0.OddHeader;
					num = 6;
					continue;
				case 1:
					if (!(A_2 == ClipboardData.b("੫ݭɯűs", a_)))
					{
						num = 18;
						continue;
					}
					num = 20;
					continue;
				case 2:
					if (documentObject != null)
					{
						num = 3;
						continue;
					}
					return;
				case 3:
					xmlReader.MoveToContent();
					this.ᜏ(xmlReader, documentObject);
					num = 10;
					continue;
				case 4:
					goto IL_1CB;
				case 5:
					if (!(A_2 == ClipboardData.b("५ᡭᕯᱱ", a_)))
					{
						num = 21;
						continue;
					}
					num = 12;
					continue;
				case 6:
					goto IL_1CB;
				case 7:
					goto IL_1CB;
				case 8:
					goto IL_1CB;
				case 9:
					num = 23;
					continue;
				case 10:
					return;
				case 11:
					goto IL_1CB;
				case 12:
					if (A_3)
					{
						num = 14;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1F0;
					default:
						if (false)
						{
						}
						documentObject = A_0.EvenFooter;
						num = 7;
						continue;
					}
					break;
				case 13:
					goto IL_1CB;
				case 14:
					documentObject = A_0.EvenHeader;
					goto IL_1F0;
				case 15:
					num = 1;
					continue;
				case 16:
					if (A_2 != null)
					{
						num = 9;
						continue;
					}
					goto IL_1CB;
				case 17:
					goto IL_1CB;
				case 18:
					num = 5;
					continue;
				case 19:
					if (A_3)
					{
						num = 0;
						continue;
					}
					documentObject = A_0.OddFooter;
					num = 11;
					continue;
				case 20:
					if (A_3)
					{
						if (true)
						{
						}
						num = 22;
						continue;
					}
					documentObject = A_0.FirstPageFooter;
					num = 8;
					continue;
				case 21:
					num = 4;
					continue;
				case 22:
					documentObject = A_0.FirstPageHeader;
					num = 13;
					continue;
				case 23:
					if (!(A_2 == ClipboardData.b("࡫୭ᙯ፱ų᩵౷", a_)))
					{
						num = 15;
						continue;
					}
					num = 19;
					continue;
				}
				break;
				IL_1CB:
				num = 2;
				continue;
				IL_1F0:
				num = 17;
			}
		}
	}

	// Token: 0x06001F9B RID: 8091 RVA: 0x002185B8 File Offset: 0x002175B8
	private void ᜃ(XmlReader A_0, bool A_1)
	{
		int a_ = 0;
		for (;;)
		{
			IL_8C:
			string localName = A_0.LocalName;
			A_0.Read();
			this.ᜀ(A_0);
			int num = 18;
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
					{
						string localName2;
						if (!(localName2 == ClipboardData.b("ᙥݧᥩ", a_)))
						{
							num = 2;
							continue;
						}
						string attribute = A_0.GetAttribute(ClipboardData.b("ၥ१٩", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧\udda9쎫\udcad풯슱욳\ud9b5\udbb7\udfb9쾻춽ꦿ곁ꏃꯅ꓇ﻋﻍﯓ믕맗동닛", a_));
						num = 10;
						continue;
					}
					case 1:
						if (!(A_0.LocalName != localName))
						{
							num = 16;
							continue;
						}
						num = 9;
						continue;
					case 2:
						num = 7;
						continue;
					case 3:
						goto IL_2DC;
					case 4:
						if (true)
						{
						}
						goto IL_196;
					case 5:
						this.ᜄ.FooternoteOptions.Position = FootnotePosition.PrintImmediatelyBeneathText;
						num = 15;
						continue;
					case 6:
					{
						string attribute;
						if (attribute == ClipboardData.b("ѥ൧ѩ५཭ѯᩱ⁳፵w๹", a_))
						{
							num = 5;
							continue;
						}
						goto IL_234;
					}
					case 7:
					{
						string localName2;
						if (!(localName2 == ClipboardData.b("ࡥᵧݩ⩫ͭѯ", a_)))
						{
							num = 19;
							continue;
						}
						this.ᜁ(A_0, A_1);
						num = 13;
						continue;
					}
					case 8:
						num = 0;
						continue;
					case 9:
					{
						string localName2;
						if ((localName2 = A_0.LocalName) != null)
						{
							num = 8;
							continue;
						}
						goto IL_234;
					}
					case 10:
					{
						string attribute;
						if (attribute != null)
						{
							num = 22;
							continue;
						}
						goto IL_234;
					}
					case 11:
						num = 3;
						continue;
					case 12:
						goto IL_234;
					case 13:
						goto IL_234;
					case 14:
					{
						string localName2;
						if (!(localName2 == ClipboardData.b("ࡥᵧݩ㽫ᩭᅯqs", a_)))
						{
							num = 11;
							continue;
						}
						this.ᜂ(A_0, A_1);
						num = 17;
						continue;
					}
					case 15:
						goto IL_234;
					case 16:
						return;
					case 17:
						goto IL_234;
					case 18:
						goto IL_196;
					case 19:
						num = 20;
						continue;
					case 20:
					{
						string localName2;
						if (!(localName2 == ClipboardData.b("ࡥᵧݩ㹫୭ͯٱᕳѵ౷", a_)))
						{
							num = 21;
							continue;
						}
						this.ᜀ(A_0, A_1);
						num = 12;
						continue;
					}
					case 21:
						num = 14;
						continue;
					case 22:
						num = 6;
						continue;
					}
					goto IL_8C;
					IL_196:
					num = 1;
					continue;
				}
				IL_234:
				A_0.Read();
				this.ᜀ(A_0);
				num = 4;
				continue;
				IL_2DC:
				goto IL_234;
			}
		}
	}

	// Token: 0x06001F9C RID: 8092 RVA: 0x002188A8 File Offset: 0x002178A8
	private void ᜂ(XmlReader A_0, bool A_1)
	{
		int a_ = 13;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_9E:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_47;
		}
		string attribute;
		for (;;)
		{
			IL_31:
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (A_1)
				{
					num = 3;
					continue;
				}
				goto IL_B4;
			case 2:
				if (true)
				{
				}
				if (attribute == null)
				{
					num = 0;
					continue;
				}
				goto IL_9E;
			case 3:
				goto IL_B1;
			}
			goto IL_47;
		}
		return;
		IL_B1:
		this.ᜄ.FooternoteOptions.StartNumber = int.Parse(attribute);
		return;
		IL_B4:
		this.ᜄ.EndnoteOptions.StartNumber = int.Parse(attribute);
		return;
		IL_47:
		attribute = A_0.GetAttribute(ClipboardData.b("ղᑴ᭶", a_), ClipboardData.b("᭲ŴͶॸ䅺剼偾ﺌꆎﺐ練咽캠톢좤욦\udda8\ud8aa莬삮쎰풲骴삶횸즺\ud9bc쾾돀곂ꛄꋆ뫈룊꓌ꇎ뛐뻒맔컠転蓤軦蟨", a_));
		num = 2;
		goto IL_31;
	}

	// Token: 0x06001F9D RID: 8093 RVA: 0x00218980 File Offset: 0x00217980
	private void ᜁ(XmlReader A_0, bool A_1)
	{
		int a_ = 5;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ᵪ౬ͮ", a_), ClipboardData.b("ͪᥬ᭮Ű䥲婴塶੸᡺ᕼ᩾ꦆﮊﺒ璉ﺞ햠킢认좦\udba8첪芬\ud8ae\udeb0솲톴잶쮸풺\udebc\udabe닀냂계꧆껈ꛊꇌ뛚볜뛞迠", a_));
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 13;
					continue;
				case 1:
					if (A_1)
					{
						num = 16;
						continue;
					}
					goto IL_2F8;
				case 2:
					if (attribute == null)
					{
						num = 4;
						continue;
					}
					num = 22;
					continue;
				case 3:
					num = 15;
					continue;
				case 4:
					return;
				case 5:
				{
					string a;
					if (!(a == ClipboardData.b("ṪᵬὮᑰŲ❴ᡶᑸ᩺፼", a_)))
					{
						num = 19;
						continue;
					}
					num = 9;
					continue;
				}
				case 6:
				{
					string a;
					if (!(a == ClipboardData.b("ݪɬᡮᑰŲ❴ᡶᑸ᩺፼", a_)))
					{
						num = 21;
						continue;
					}
					num = 11;
					continue;
				}
				case 7:
					goto IL_214;
				case 8:
					num = 18;
					continue;
				case 9:
					if (A_1)
					{
						num = 24;
						continue;
					}
					this.ᜄ.EndnoteOptions.NumberFormat = FootEndnoteNumberFormat.UpperCaseRoman;
					num = 7;
					continue;
				case 10:
					num = 6;
					continue;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2C4;
					default:
						if (false)
						{
						}
						if (A_1)
						{
							num = 17;
							continue;
						}
						goto IL_111;
					}
					break;
				case 12:
					goto IL_235;
				case 13:
				{
					string a;
					if (!(a == ClipboardData.b("ṪᵬὮᑰŲ㥴ቶ൸ེ᡼ൾ", a_)))
					{
						num = 10;
						continue;
					}
					num = 14;
					continue;
				}
				case 14:
					if (A_1)
					{
						goto IL_2C4;
					}
					goto IL_B9;
				case 15:
				{
					string a;
					if (!(a == ClipboardData.b("ݪɬᡮᑰŲ㥴ቶ൸ེ᡼ൾ", a_)))
					{
						num = 0;
						continue;
					}
					num = 1;
					continue;
				}
				case 16:
					goto IL_1A1;
				case 17:
					goto IL_365;
				case 18:
				{
					string a;
					if (!(a == ClipboardData.b("ཪ࡬౮ᡰṲᑴ᭶", a_)))
					{
						num = 3;
						continue;
					}
					num = 20;
					continue;
				}
				case 19:
					return;
				case 20:
					if (A_1)
					{
						num = 12;
						continue;
					}
					goto IL_2E6;
				case 21:
					num = 5;
					continue;
				case 22:
				{
					string a;
					if ((a = attribute) != null)
					{
						num = 8;
						continue;
					}
					goto IL_36A;
				}
				case 23:
					goto IL_2CF;
				case 24:
					goto IL_1F3;
				}
				break;
				IL_2C4:
				num = 23;
			}
		}
		return;
		IL_B9:
		this.ᜄ.EndnoteOptions.NumberFormat = FootEndnoteNumberFormat.UpperCaseLetter;
		return;
		IL_111:
		this.ᜄ.EndnoteOptions.NumberFormat = FootEndnoteNumberFormat.LowerCaseRoman;
		return;
		IL_1A1:
		this.ᜄ.FooternoteOptions.NumberFormat = FootEndnoteNumberFormat.LowerCaseLetter;
		return;
		IL_1F3:
		this.ᜄ.FooternoteOptions.NumberFormat = FootEndnoteNumberFormat.UpperCaseRoman;
		return;
		IL_214:
		goto IL_36A;
		IL_235:
		this.ᜄ.FooternoteOptions.NumberFormat = FootEndnoteNumberFormat.Arabic;
		return;
		IL_2CF:
		this.ᜄ.FooternoteOptions.NumberFormat = FootEndnoteNumberFormat.UpperCaseLetter;
		return;
		IL_2E6:
		this.ᜄ.EndnoteOptions.NumberFormat = FootEndnoteNumberFormat.Arabic;
		return;
		IL_2F8:
		this.ᜄ.EndnoteOptions.NumberFormat = FootEndnoteNumberFormat.LowerCaseLetter;
		return;
		IL_365:
		this.ᜄ.FooternoteOptions.NumberFormat = FootEndnoteNumberFormat.LowerCaseRoman;
		return;
		IL_36A:
		if (true)
		{
		}
	}

	// Token: 0x06001F9E RID: 8094 RVA: 0x00218D00 File Offset: 0x00217D00
	private void ᜀ(XmlReader A_0, bool A_1)
	{
		int a_ = 5;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ᵪ౬ͮ", a_), ClipboardData.b("ͪᥬ᭮Ű䥲婴塶੸᡺ᕼ᩾ꦆﮊﺒ璉ﺞ햠킢认좦\udba8첪芬\ud8ae\udeb0솲톴잶쮸풺\udebc\udabe닀냂계꧆껈ꛊꇌ뛚볜뛞迠", a_));
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 5;
					continue;
				case 1:
					goto IL_112;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_114;
					default:
						goto IL_84;
					}
					break;
				case 3:
					if (attribute == null)
					{
						num = 2;
						continue;
					}
					num = 8;
					continue;
				case 4:
					return;
				case 5:
					if (A_1)
					{
						num = 7;
						continue;
					}
					goto IL_114;
				case 6:
					if (attribute == ClipboardData.b("๪౬౮ᥰ⁲ၴᑶ൸", a_))
					{
						num = 0;
						continue;
					}
					return;
				case 7:
					goto IL_B0;
				case 8:
					if (attribute == ClipboardData.b("๪౬౮ᥰ⍲ᑴၶᱸ", a_))
					{
						num = 1;
						continue;
					}
					num = 6;
					continue;
				}
				break;
				IL_114:
				this.ᜄ.EndnoteOptions.RestartRule = EndnoteRestartRule.RestartSection;
				num = 4;
			}
		}
		IL_84:
		if (false)
		{
		}
		return;
		IL_B0:
		this.ᜄ.FooternoteOptions.RestartRule = FootnoteRestartRule.RestartSection;
		return;
		IL_112:
		this.ᜄ.FooternoteOptions.RestartRule = FootnoteRestartRule.RestartPage;
	}

	// Token: 0x06001F9F RID: 8095 RVA: 0x00218E70 File Offset: 0x00217E70
	private void ᜉ(XmlReader A_0, Section A_1)
	{
		int a_ = 4;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("౩ūᩭ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫\ud9ad\udfaf삱킳욵쪷햹\udfbb\udbbd뎿뇁귃ꣅ꿇Ꟊꃋ럙뷛럝軟", a_));
			int num = 14;
			for (;;)
			{
				string attribute2;
				switch (num)
				{
				case 0:
					A_1.PageSetup.RestartPageNumbering = true;
					A_1.PageSetup.PageStartingNumber = int.Parse(attribute2);
					num = 3;
					continue;
				case 1:
					goto IL_136;
				case 2:
					if (attribute2 == null)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_32A;
					default:
						if (false)
						{
						}
						num = 16;
						continue;
					}
					break;
				case 3:
					return;
				case 4:
				{
					string a;
					if (!(a == ClipboardData.b("Ὡᱫṭᕯq♳᥵ᕷ᭹ቻ", a_)))
					{
						num = 11;
						continue;
					}
					A_1.PageSetup.PageNumberStyle = PageNumberStyle.RomanUpper;
					num = 13;
					continue;
				}
				case 5:
				{
					string a;
					if (!(a == ClipboardData.b("๩५൭᥯άᕳ᩵", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_32A;
				}
				case 6:
				{
					string a;
					if (!(a == ClipboardData.b("٩ͫᥭᕯq㡳፵౷๹᥻౽", a_)))
					{
						num = 10;
						continue;
					}
					A_1.PageSetup.PageNumberStyle = PageNumberStyle.LetterLower;
					num = 17;
					continue;
				}
				case 7:
					num = 22;
					continue;
				case 8:
					goto IL_136;
				case 9:
					num = 4;
					continue;
				case 10:
					num = 20;
					continue;
				case 11:
					num = 6;
					continue;
				case 12:
					num = 18;
					continue;
				case 13:
					goto IL_136;
				case 14:
					if (true)
					{
					}
					if (attribute != null & attribute != string.Empty)
					{
						num = 12;
						continue;
					}
					goto IL_136;
				case 15:
					goto IL_136;
				case 16:
					num = 23;
					continue;
				case 17:
					goto IL_136;
				case 18:
				{
					string a;
					if ((a = attribute) != null)
					{
						num = 24;
						continue;
					}
					goto IL_136;
				}
				case 19:
					num = 21;
					continue;
				case 20:
				{
					string a;
					if (!(a == ClipboardData.b("Ὡᱫṭᕯq㡳፵౷๹᥻౽", a_)))
					{
						num = 19;
						continue;
					}
					A_1.PageSetup.PageNumberStyle = PageNumberStyle.LetterUpper;
					num = 15;
					continue;
				}
				case 21:
					goto IL_136;
				case 22:
				{
					string a;
					if (!(a == ClipboardData.b("٩ͫᥭᕯq♳᥵ᕷ᭹ቻ", a_)))
					{
						num = 9;
						continue;
					}
					A_1.PageSetup.PageNumberStyle = PageNumberStyle.RomanLower;
					num = 8;
					continue;
				}
				case 23:
					if (attribute2 != string.Empty)
					{
						num = 0;
						continue;
					}
					return;
				case 24:
					num = 5;
					continue;
				}
				break;
				IL_136:
				attribute2 = A_0.GetAttribute(ClipboardData.b("ᥩᡫ཭ɯٱ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫\ud9ad\udfaf삱킳욵쪷햹\udfbb\udbbd뎿뇁귃ꣅ꿇Ꟊꃋ럙뷛럝軟", a_));
				num = 2;
				continue;
				IL_32A:
				A_1.PageSetup.PageNumberStyle = PageNumberStyle.Arabic;
				num = 1;
			}
		}
	}

	// Token: 0x06001FA0 RID: 8096 RVA: 0x002191C4 File Offset: 0x002181C4
	private void ᜀ(XmlReader A_0, IDocumentObject A_1)
	{
		int a_ = 10;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ٯ፱ᡳ", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱쎳\ud9b5쪷\udeb9첻첽꾿ꇁꇃ뗅믇ꏉꋋ꧍뷏뻑ﯓ跟菡跣裥", a_));
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (attribute == null)
					{
						num = 11;
						continue;
					}
					num = 10;
					continue;
				case 1:
				{
					string a;
					if (!(a == ClipboardData.b("ѯၱ♳᩵⹷", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_15B;
				}
				case 2:
				{
					string a;
					if (!(a == ClipboardData.b("ᱯq⁳ᑵ⹷", a_)))
					{
						num = 4;
						continue;
					}
					goto IL_1CD;
				}
				case 3:
				{
					string a;
					if (!(a == ClipboardData.b("ᱯq⁳ᑵ", a_)))
					{
						num = 14;
						continue;
					}
					goto IL_93;
				}
				case 4:
					num = 12;
					continue;
				case 5:
					num = 3;
					continue;
				case 6:
					num = 1;
					continue;
				case 7:
					num = 8;
					continue;
				case 8:
					goto IL_1E5;
				case 9:
				{
					string a;
					if (!(a == ClipboardData.b("ቯٱ㡳ѵ", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_14E;
				}
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1CB;
					default:
					{
						if (false)
						{
						}
						string a;
						if ((a = attribute) != null)
						{
							num = 13;
							continue;
						}
						goto IL_223;
					}
					}
					break;
				case 11:
					goto IL_86;
				case 12:
				{
					string a;
					if (!(a == ClipboardData.b("ѯၱ♳᩵", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_105;
				}
				case 13:
					num = 9;
					continue;
				case 14:
					goto IL_1CB;
				}
				break;
				IL_1CB:
				num = 2;
			}
		}
		IL_86:
		if (true)
		{
		}
		return;
		IL_93:
		(A_1 as Section).TextDirection = TextDirection.TopToBottom;
		return;
		IL_105:
		(A_1 as Section).TextDirection = TextDirection.RightToLeft;
		return;
		IL_14E:
		(A_1 as Section).TextDirection = TextDirection.LeftToRightRotated;
		return;
		IL_15B:
		(A_1 as Section).TextDirection = TextDirection.RightToLeftRotated;
		return;
		IL_1CD:
		(A_1 as Section).TextDirection = TextDirection.TopToBottomRotated;
		return;
		IL_1E5:
		IL_223:
		(A_1 as Section).TextDirection = TextDirection.LeftToRight;
	}

	// Token: 0x06001FA1 RID: 8097 RVA: 0x00219400 File Offset: 0x00218400
	private void ᜈ(XmlReader A_0, Section A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			PageSetup pageSetup;
			for (;;)
			{
				pageSetup = A_1.PageSetup;
				pageSetup.HasLineNumbering = true;
				string attribute = A_0.GetAttribute(ClipboardData.b("ࡪɬᩮὰݲ㝴๶", a_), ClipboardData.b("ͪᥬ᭮Ű䥲婴塶੸᡺ᕼ᩾ꦆﮊﺒ璉ﺞ햠킢认좦\udba8첪芬\ud8ae\udeb0솲톴잶쮸풺\udebc\udabe닀냂계꧆껈ꛊꇌ뛚볜뛞迠", a_));
				int num = 8;
				for (;;)
				{
					float num2;
					switch (num)
					{
					case 0:
						goto IL_1CA;
					case 1:
						pageSetup.LineNumberingStep = int.Parse(attribute, NumberStyles.Integer, CultureInfo.InvariantCulture);
						num = 2;
						continue;
					case 2:
						goto IL_176;
					case 3:
						if (attribute != null)
						{
							num = 18;
							continue;
						}
						return;
					case 4:
						if (true)
						{
						}
						pageSetup.LineNumberingDistanceFromText = num2;
						num = 9;
						continue;
					case 5:
					{
						string a;
						if (!(a == ClipboardData.b("ժ࡬ᡮ≰ᙲᙴͶၸᑺ፼", a_)))
						{
							num = 17;
							continue;
						}
						goto IL_24F;
					}
					case 6:
						num = 12;
						continue;
					case 7:
						goto IL_1F4;
					case 8:
						if (attribute != null)
						{
							num = 1;
							continue;
						}
						goto IL_176;
					case 9:
						goto IL_2C2;
					case 10:
						if (attribute != null)
						{
							num = 11;
							continue;
						}
						goto IL_1F4;
					case 11:
						goto IL_1B4;
					case 12:
					{
						string a;
						if (a == ClipboardData.b("ժ࡬ᡮⅰቲቴቶ", a_))
						{
							goto IL_16E;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1B4;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					}
					case 13:
						if (num2 != 3.4028235E+38f)
						{
							num = 4;
							continue;
						}
						goto IL_2C2;
					case 14:
						num = 5;
						continue;
					case 15:
						return;
					case 16:
					{
						string a;
						if (!(a == ClipboardData.b("ࡪɬŮհᩲ᭴ɶᙸ๺๼", a_)))
						{
							num = 15;
							continue;
						}
						pageSetup.LineNumberingRestartMode = LineNumberingRestartMode.Continuous;
						num = 0;
						continue;
					}
					case 17:
						num = 16;
						continue;
					case 18:
						num = 19;
						continue;
					case 19:
					{
						string a;
						if ((a = attribute) != null)
						{
							num = 6;
							continue;
						}
						return;
					}
					}
					break;
					IL_176:
					attribute = A_0.GetAttribute(ClipboardData.b("ᡪᥬ๮Ͱݲ", a_), ClipboardData.b("ͪᥬ᭮Ű䥲婴塶੸᡺ᕼ᩾ꦆﮊﺒ璉ﺞ햠킢认좦\udba8첪芬\ud8ae\udeb0솲톴잶쮸풺\udebc\udabe닀냂계꧆껈ꛊꇌ뛚볜뛞迠", a_));
					num = 10;
					continue;
					IL_1B4:
					pageSetup.LineNumberingStartValue = int.Parse(attribute, NumberStyles.Integer, CultureInfo.InvariantCulture) + 1;
					num = 7;
					continue;
					IL_1F4:
					num2 = this.ᜀ(A_0, ClipboardData.b("ཪѬᱮհቲ᭴ᑶᱸ", a_), ClipboardData.b("ͪᥬ᭮Ű䥲婴塶੸᡺ᕼ᩾ꦆﮊﺒ璉ﺞ햠킢认좦\udba8첪芬\ud8ae\udeb0솲톴잶쮸풺\udebc\udabe닀냂계꧆껈ꛊꇌ뛚볜뛞迠", a_));
					num = 13;
					continue;
					IL_2C2:
					attribute = A_0.GetAttribute(ClipboardData.b("ᥪ࡬ᱮհቲݴͶ", a_), ClipboardData.b("ͪᥬ᭮Ű䥲婴塶੸᡺ᕼ᩾ꦆﮊﺒ璉ﺞ햠킢认좦\udba8첪芬\ud8ae\udeb0솲톴잶쮸풺\udebc\udabe닀냂계꧆껈ꛊꇌ뛚볜뛞迠", a_));
					num = 3;
				}
			}
			return;
			IL_16E:
			pageSetup.LineNumberingRestartMode = LineNumberingRestartMode.RestartPage;
			return;
			IL_1CA:
			return;
			IL_24F:
			pageSetup.LineNumberingRestartMode = LineNumberingRestartMode.RestartSection;
			return;
		}
		}
	}

	// Token: 0x06001FA2 RID: 8098 RVA: 0x00219714 File Offset: 0x00218714
	private void ᜇ(XmlReader A_0, Section A_1)
	{
		int a_ = 2;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ṧ୩k", a_), ClipboardData.b("gṩᡫṭ䩯嵱孳յ᭷ቹ᥻፽ꪃﶏﺑ秊ﶛ펟財쮣풥쾧薩\udbab솭슯횱쒳쒵ힷ\ud9b9\ud9bb춽뎿ꯁ꫃ꇅꗇꛉﳍ崙뗗믙뗛냝", a_));
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_70;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 1:
					goto IL_141;
				case 2:
					return;
				case 3:
				{
					string a;
					if (!(a == ClipboardData.b("ᱧթᱫ", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_11D;
				}
				case 4:
				{
					string a;
					if (!(a == ClipboardData.b("୧ཀྵɫᩭᕯq", a_)))
					{
						num = 10;
						continue;
					}
					goto IL_80;
				}
				case 5:
					if (true)
					{
					}
					num = 4;
					continue;
				case 6:
				{
					string a;
					if (!(a == ClipboardData.b("੧թᡫᩭὯά", a_)))
					{
						num = 2;
						continue;
					}
					A_1.PageSetup.VerticalAlignment = PageAlignment.Bottom;
					num = 1;
					continue;
				}
				case 7:
					num = 3;
					continue;
				case 8:
				{
					string a;
					if (!(a == ClipboardData.b("੧թᡫ٭", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_C7;
				}
				case 9:
					goto IL_70;
				case 10:
					num = 8;
					continue;
				case 11:
				{
					string a;
					if ((a = attribute) != null)
					{
						num = 7;
						continue;
					}
					return;
				}
				case 12:
					num = 6;
					continue;
				}
				break;
				IL_70:
				if (attribute == null)
				{
					return;
				}
				num = 0;
			}
		}
		IL_80:
		A_1.PageSetup.VerticalAlignment = PageAlignment.Middle;
		return;
		IL_C7:
		A_1.PageSetup.VerticalAlignment = PageAlignment.Justified;
		return;
		IL_11D:
		A_1.PageSetup.VerticalAlignment = PageAlignment.Top;
		return;
		IL_141:;
	}

	// Token: 0x06001FA3 RID: 8099 RVA: 0x00219900 File Offset: 0x00218900
	private void ᜆ(XmlReader A_0, Section A_1)
	{
		int a_ = 7;
		for (;;)
		{
			float num = this.ᜀ(A_0, ClipboardData.b("Ŭٮὰᙲ╴Ṷ൸᡺ᕼ", a_), ClipboardData.b("լ᭮հͲ佴塶噸ࡺṼ᝾ꞈﶌﾐﮖﾘ삠힢횤覦욨\ud9aa쪬肮우\udcb2잴펶즸즺튼\udcbe꓀냂뛄껆ꟈ곊ꃌꏎﻐ냜뻞裠跢", a_));
			int num2 = 10;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					num2 = 2;
					continue;
				case 1:
					return;
				case 2:
				{
					string a;
					if (!(a == ClipboardData.b("Ŭٮὰᙲٴ㙶᝸ὺ㹼᝾", a_)))
					{
						num2 = 12;
						continue;
					}
					goto IL_13A;
				}
				case 3:
				{
					string a;
					if (!(a == ClipboardData.b("ṬŮၰͲⅴᡶ㩸፺ᱼൾ", a_)))
					{
						num2 = 9;
						continue;
					}
					A_1.PageSetup.PitchType = GridPitchType.SnapToChars;
					num2 = 4;
					continue;
				}
				case 4:
					return;
				case 5:
				{
					string attribute;
					if (attribute == null)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_196;
					default:
						if (false)
						{
						}
						num2 = 7;
						continue;
					}
					break;
				}
				case 6:
				{
					string a;
					if (!(a == ClipboardData.b("Ŭٮὰᙲٴ", a_)))
					{
						num2 = 0;
						continue;
					}
					goto IL_86;
				}
				case 7:
					goto IL_196;
				case 8:
				{
					string a;
					string attribute;
					if ((a = attribute) != null)
					{
						num2 = 11;
						continue;
					}
					return;
				}
				case 9:
					return;
				case 10:
				{
					if (num == 3.4028235E+38f)
					{
						num2 = 1;
						continue;
					}
					A_1.PageSetup.LinePitch = num;
					string attribute = A_0.GetAttribute(ClipboardData.b("ᥬ᙮Űᙲ", a_), ClipboardData.b("լ᭮հͲ佴塶噸ࡺṼ᝾ꞈﶌﾐﮖﾘ삠힢횤覦욨\ud9aa쪬肮우\udcb2잴펶즸즺튼\udcbe꓀냂뛄껆ꟈ곊ꃌꏎﻐ냜뻞裠跢", a_));
					num2 = 5;
					continue;
				}
				case 11:
					num2 = 6;
					continue;
				case 12:
					num2 = 3;
					continue;
				}
				break;
				IL_196:
				if (true)
				{
				}
				num2 = 8;
			}
		}
		return;
		IL_86:
		A_1.PageSetup.PitchType = GridPitchType.LinesOnly;
		return;
		IL_13A:
		A_1.PageSetup.PitchType = GridPitchType.CharsAndLine;
	}

	// Token: 0x06001FA4 RID: 8100 RVA: 0x00219AFC File Offset: 0x00218AFC
	private void ᜅ(XmlReader A_0, Section A_1)
	{
		int a_ = 14;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("᭳ၵṷॹ᥻੽왿", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
			int num = 9;
			for (;;)
			{
				string attribute2;
				switch (num)
				{
				case 0:
					if (attribute2 == ClipboardData.b("ᩳ᥵౷㱹ᕻ౽풃", a_))
					{
						num = 1;
						continue;
					}
					goto IL_1C0;
				case 1:
					A_1.PageSetup.PageBordersApplyType = PageBordersApplyType.AllExceptFirstPage;
					num = 2;
					continue;
				case 2:
					goto IL_1A2;
				case 3:
					goto IL_79;
				case 4:
					if (attribute2 != null)
					{
						goto IL_AD;
					}
					goto IL_1C0;
				case 5:
					if (attribute2 == ClipboardData.b("ታή੷ॹࡻ⹽", a_))
					{
						num = 8;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AD;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 6:
					A_1.PageSetup.PageBorderOffsetFrom = ((attribute == ClipboardData.b("ѳ᝵ίό", a_)) ? PageBorderOffsetFrom.PageEdge : PageBorderOffsetFrom.Text);
					if (true)
					{
					}
					num = 3;
					continue;
				case 7:
					goto IL_1BE;
				case 8:
					A_1.PageSetup.PageBordersApplyType = PageBordersApplyType.FirstPage;
					num = 7;
					continue;
				case 9:
					if (attribute != null)
					{
						num = 11;
						continue;
					}
					goto IL_79;
				case 10:
					num = 5;
					continue;
				case 11:
					num = 6;
					continue;
				}
				break;
				IL_79:
				attribute2 = A_0.GetAttribute(ClipboardData.b("ၳή୷੹ၻώ勵", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
				num = 4;
				continue;
				IL_AD:
				num = 10;
			}
		}
		IL_1A2:
		IL_1BE:
		IL_1C0:
		this.ᜂ(A_0, A_1);
	}

	// Token: 0x06001FA5 RID: 8101 RVA: 0x00219CD4 File Offset: 0x00218CD4
	private void ᜄ(XmlReader A_0, Section A_1)
	{
		int a_ = 14;
		for (;;)
		{
			IL_5B:
			string attribute = A_0.GetAttribute(ClipboardData.b("ݳ፵ࡷ", a_), ClipboardData.b("ᱳɵ౷੹䙻兽꽿ﶍ뺏﶑욟춡횣쮥즧\udea9\udfab肭\udfaf삱펳馵쾷햹캻\udabd낿냁ꯃꗅ귇막뿋ꟍ뻏뗑맓뫕훟췡解蟥臧蓩", a_));
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_DB;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						if (attribute != null)
						{
							num = 1;
							continue;
						}
						goto IL_96;
					case 1:
						num = 2;
						continue;
					case 2:
						goto IL_DB;
					case 3:
						goto IL_96;
					case 4:
						if (!this.ᜇ(A_0))
						{
							num = 5;
							continue;
						}
						goto IL_11D;
					case 5:
						goto IL_B5;
					case 6:
						A_1.PageSetup.DrawLinesBetweenCols = true;
						num = 3;
						continue;
					}
					goto IL_5B;
				}
				IL_96:
				num = 4;
				continue;
				IL_DB:
				if (!(attribute == ClipboardData.b("䕳", a_)))
				{
					goto IL_96;
				}
				num = 6;
			}
		}
		IL_B5:
		A_1.Columns.OwnerSection.PageSetup.EqualColumnWidth = false;
		this.ᜂ(A_0, A_1);
		return;
		IL_11D:
		A_1.Columns.OwnerSection.PageSetup.EqualColumnWidth = true;
		this.ᜃ(A_0, A_1);
	}

	// Token: 0x06001FA6 RID: 8102 RVA: 0x00219E1C File Offset: 0x00218E1C
	private bool ᜇ(XmlReader A_0)
	{
		int a_ = 4;
		while (!(A_0.GetAttribute(ClipboardData.b("ཀྵᵫ᭭ᅯṱ⍳ήᱷ๹ᑻ", a_), ClipboardData.b("ɩᡫᩭo䡱孳奵୷᥹ᑻ᭽ꢅ憎ﾑﾝ풟톡誣즥\udaa7충莫\ud9ad\udfaf삱킳욵쪷햹\udfbb\udbbd뎿뇁귃ꣅ꿇Ꟊꃋ럙뷛럝軟", a_)) == ClipboardData.b("婩", a_)))
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
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001FA7 RID: 8103 RVA: 0x00219E9C File Offset: 0x00218E9C
	private void ᜃ(XmlReader A_0, Section A_1)
	{
		int a_ = 7;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_6C:
				float num = A_1.PageSetup.PageSize.Width * 20f;
				float num2 = A_1.PageSetup.Margins.Left * 20f;
				float num3 = A_1.PageSetup.Margins.Right * 20f;
				float num4 = 0f;
				float num5 = 0f;
				int num6 = 1;
				for (;;)
				{
					IL_C5:
					int num7 = 7;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C5;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							int num8;
							switch (num7)
							{
							case 0:
								goto IL_1CF;
							case 1:
								if (A_0.GetAttribute(ClipboardData.b("ṬὮၰၲၴ", a_), ClipboardData.b("լ᭮հͲ佴塶噸ࡺṼ᝾ꞈﶌﾐﮖﾘ삠힢횤覦욨\ud9aa쪬肮우\udcb2잴펶즸즺튼\udcbe꓀냂뛄껆ꟈ곊ꃌꏎﻐ냜뻞裠跢", a_)) != null)
								{
									num7 = 9;
									continue;
								}
								goto IL_233;
							case 2:
								goto IL_1CF;
							case 3:
								num6 = int.Parse(A_0.GetAttribute(ClipboardData.b("ͬᩮᱰ", a_), ClipboardData.b("լ᭮հͲ佴塶噸ࡺṼ᝾ꞈﶌﾐﮖﾘ삠힢횤覦욨\ud9aa쪬肮우\udcb2잴펶즸즺튼\udcbe꓀냂뛄껆ꟈ곊ꃌꏎﻐ냜뻞裠跢", a_)));
								num7 = 4;
								continue;
							case 4:
								goto IL_1F2;
							case 5:
							{
								if (num8 >= num6)
								{
									num7 = 6;
									continue;
								}
								Column column = new Column(this.ᜄ);
								column.Space = num4 / 20f;
								column.Width = num5 / 20f;
								A_1.Columns.Add(column);
								num8++;
								num7 = 2;
								continue;
							}
							case 6:
								return;
							case 7:
								if (A_0.GetAttribute(ClipboardData.b("ͬᩮᱰ", a_), ClipboardData.b("լ᭮հͲ佴塶噸ࡺṼ᝾ꞈﶌﾐﮖﾘ삠힢횤覦욨\ud9aa쪬肮우\udcb2잴펶즸즺튼\udcbe꓀냂뛄껆ꟈ곊ꃌꏎﻐ냜뻞裠跢", a_)) != null)
								{
									num7 = 3;
									continue;
								}
								goto IL_1F2;
							case 8:
								goto IL_233;
							case 9:
								num4 = float.Parse(A_0.GetAttribute(ClipboardData.b("ṬὮၰၲၴ", a_), ClipboardData.b("լ᭮հͲ佴塶噸ࡺṼ᝾ꞈﶌﾐﮖﾘ삠힢횤覦욨\ud9aa쪬肮우\udcb2잴펶즸즺튼\udcbe꓀냂뛄껆ꟈ곊ꃌꏎﻐ냜뻞裠跢", a_)));
								num7 = 8;
								continue;
							}
							goto IL_6C;
							IL_1CF:
							num7 = 5;
							break;
							IL_1F2:
							num7 = 1;
							break;
							IL_233:
							num5 = (num - num2 - num3 - num4 * (float)num6 - 1f) / (float)num6;
							num8 = 0;
							num7 = 0;
							break;
						}
						}
					}
				}
			}
			return;
		}
	}

	// Token: 0x06001FA8 RID: 8104 RVA: 0x0021A108 File Offset: 0x00219108
	private void ᜂ(XmlReader A_0, Section A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
			for (;;)
			{
				string localName = A_0.LocalName;
				A_0.Read();
				this.ᜀ(A_0);
				int num = 1;
				for (;;)
				{
					Column column;
					switch (num)
					{
					case 0:
						goto IL_281;
					case 1:
						goto IL_1D0;
					case 2:
					{
						string localName2;
						if (localName2 == ClipboardData.b("୧թk", a_))
						{
							goto IL_197;
						}
						goto IL_155;
					}
					case 3:
						goto IL_2E8;
					case 4:
					{
						float num2;
						if (num2 != 3.4028235E+38f)
						{
							num = 10;
							continue;
						}
						goto IL_281;
					}
					case 5:
						if (A_0.GetAttribute(ClipboardData.b("ὧ", a_), ClipboardData.b("gṩᡫṭ䩯嵱孳յ᭷ቹ᥻፽ꪃﶏﺑ秊ﶛ펟財쮣풥쾧薩\udbab솭슯횱쒳쒵ힷ\ud9b9\ud9bb춽뎿ꯁ꫃ꇅꗇꛉﳍ崙뗗믙뗛냝", a_)) != null)
						{
							num = 17;
							continue;
						}
						goto IL_B5;
					case 6:
						return;
					case 7:
						goto IL_1D0;
					case 8:
						if (true)
						{
						}
						if (!(A_0.LocalName != localName))
						{
							num = 6;
							continue;
						}
						num = 9;
						continue;
					case 9:
					{
						string localName2;
						if ((localName2 = A_0.LocalName) != null)
						{
							num = 12;
							continue;
						}
						goto IL_155;
					}
					case 10:
					{
						float num2;
						column.Width = num2 / 20f;
						num = 0;
						continue;
					}
					case 11:
					{
						float num3 = (float)int.Parse(A_0.GetAttribute(ClipboardData.b("᭧ᩩ൫൭ᕯ", a_), ClipboardData.b("gṩᡫṭ䩯嵱孳յ᭷ቹ᥻፽ꪃﶏﺑ秊ﶛ펟財쮣풥쾧薩\udbab솭슯횱쒳쒵ힷ\ud9b9\ud9bb춽뎿ꯁ꫃ꇅꗇꛉﳍ崙뗗믙뗛냝", a_)));
						num = 3;
						continue;
					}
					case 12:
						num = 2;
						continue;
					case 13:
						goto IL_1A8;
					case 14:
						if (A_0.GetAttribute(ClipboardData.b("᭧ᩩ൫൭ᕯ", a_), ClipboardData.b("gṩᡫṭ䩯嵱孳յ᭷ቹ᥻፽ꪃﶏﺑ秊ﶛ펟財쮣풥쾧薩\udbab솭슯횱쒳쒵ힷ\ud9b9\ud9bb춽뎿ꯁ꫃ꇅꗇꛉﳍ崙뗗믙뗛냝", a_)) != null)
						{
							num = 11;
							continue;
						}
						goto IL_2E8;
					case 15:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_197;
						default:
						{
							if (false)
							{
							}
							float num3 = float.MaxValue;
							float num2 = float.MaxValue;
							num = 14;
							continue;
						}
						}
						break;
					case 16:
					{
						float num3;
						column.Space = num3 / 20f;
						num = 13;
						continue;
					}
					case 17:
					{
						float num2 = (float)int.Parse(A_0.GetAttribute(ClipboardData.b("ὧ", a_), ClipboardData.b("gṩᡫṭ䩯嵱孳յ᭷ቹ᥻፽ꪃﶏﺑ秊ﶛ펟財쮣풥쾧薩\udbab솭슯횱쒳쒵ힷ\ud9b9\ud9bb춽뎿ꯁ꫃ꇅꗇꛉﳍ崙뗗믙뗛냝", a_)));
						num = 20;
						continue;
					}
					case 18:
					{
						float num3;
						if (num3 != 3.4028235E+38f)
						{
							num = 16;
							continue;
						}
						goto IL_1A8;
					}
					case 19:
						goto IL_155;
					case 20:
						goto IL_B5;
					}
					break;
					IL_B5:
					column = new Column(this.ᜄ);
					num = 18;
					continue;
					IL_155:
					A_0.Read();
					this.ᜀ(A_0);
					num = 7;
					continue;
					IL_197:
					num = 15;
					continue;
					IL_1A8:
					num = 4;
					continue;
					IL_1D0:
					num = 8;
					continue;
					IL_281:
					A_1.Columns.Add(column);
					num = 19;
					continue;
					IL_2E8:
					num = 5;
				}
			}
			return;
		}
	}

	// Token: 0x06001FA9 RID: 8105 RVA: 0x0021A444 File Offset: 0x00219444
	private void ᜁ(XmlReader A_0, Section A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			float a_2;
			for (;;)
			{
				float top = this.ᜂ(A_0, ClipboardData.b("ɵ᝷੹", a_));
				float right = this.ᜂ(A_0, ClipboardData.b("ѵᅷᵹᑻ੽", a_));
				float bottom = this.ᜂ(A_0, ClipboardData.b("ᑵ᝷๹ࡻᅽ", a_));
				float left = this.ᜂ(A_0, ClipboardData.b("᩵ᵷᱹࡻ", a_));
				float num = this.ᜂ(A_0, ClipboardData.b("ၵ᝷ᕹࡻ᭽", a_));
				float num2 = this.ᜂ(A_0, ClipboardData.b("ṵᵷ᭹᡻᭽", a_));
				a_2 = this.ᜂ(A_0, ClipboardData.b("ᅵ൷๹ࡻ᭽", a_));
				A_1.PageSetup.Margins = new MarginsF(left, top, right, bottom);
				A_1.PageSetup.Margins.Gutter = a_2;
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_14E;
						default:
							if (false)
							{
							}
							A_1.PageSetup.HeaderDistance = num2;
							num3 = 3;
							continue;
						}
						break;
					case 1:
						if (num2 != -1f)
						{
							goto IL_14E;
						}
						goto IL_1A5;
					case 2:
						if (num != -1f)
						{
							num3 = 4;
							continue;
						}
						goto IL_139;
					case 3:
						goto IL_137;
					case 4:
						A_1.PageSetup.FooterDistance = num;
						num3 = 5;
						continue;
					case 5:
						goto IL_139;
					}
					break;
					IL_139:
					num3 = 1;
					continue;
					IL_14E:
					num3 = 0;
				}
			}
			IL_137:
			IL_1A5:
			this.ᜊ = a_2;
			return;
		}
		}
	}

	// Token: 0x06001FAA RID: 8106 RVA: 0x0021A600 File Offset: 0x00219600
	private float ᜂ(XmlReader A_0, string A_1)
	{
		int a_ = 10;
		int num = 6;
		float result;
		for (;;)
		{
			float num2;
			string attribute;
			switch (num)
			{
			case 0:
				return result;
			case 1:
				num2 = (float)0;
				goto IL_C8;
			case 2:
				if (true)
				{
				}
				if (attribute != null)
				{
					num = 7;
					continue;
				}
				return result;
			case 3:
				num2 = (float)-1;
				goto IL_C8;
			case 4:
				num = 8;
				continue;
			case 5:
				num = 1;
				continue;
			case 7:
				result = float.Parse(attribute) / 20f;
				num = 0;
				continue;
			case 8:
				if (!(A_1 == ClipboardData.b("ᡯ᝱ᕳትᵷࡹ", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_BA;
			}
			if (!(A_1 == ClipboardData.b("ᙯᵱ᭳ɵᵷࡹ", a_)))
			{
				num = 4;
				continue;
			}
			IL_BA:
			num = 3;
			continue;
			IL_C8:
			result = num2;
			attribute = A_0.GetAttribute(A_1, ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱쎳\ud9b5쪷\udeb9첻첽꾿ꇁꇃ뗅믇ꏉꋋ꧍뷏뻑ﯓ跟菡跣裥", a_));
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
				break;
			}
		}
		return result;
	}

	// Token: 0x06001FAB RID: 8107 RVA: 0x0021A730 File Offset: 0x00219730
	private void ᜀ(XmlReader A_0, Section A_1)
	{
		int a_ = 15;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		float height = float.Parse(A_0.GetAttribute(ClipboardData.b("ᵴ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_))) / 20f;
		float width = float.Parse(A_0.GetAttribute(ClipboardData.b("ɴ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_))) / 20f;
		A_1.PageSetup.PageSize = new SizeF(width, height);
		string attribute = A_0.GetAttribute(ClipboardData.b("ᩴնၸṺ፼୾", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_));
		A_1.PageSetup.Orientation = ((attribute == ClipboardData.b("ᥴᙶ᝸ὺ๼᱾", a_)) ? PageOrientation.Landscape : PageOrientation.Portrait);
	}

	// Token: 0x06001FAC RID: 8108 RVA: 0x0021A830 File Offset: 0x00219830
	private void ᜀ(Section A_0, string A_1)
	{
		int a_ = 17;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_8D;
			case 1:
				num = 3;
				continue;
			case 2:
				if (!(A_1 == ClipboardData.b("᥶ᱸͺॼ⽾", a_)))
				{
					goto IL_BB;
				}
				goto IL_73;
			case 3:
				if (!(A_1 == ClipboardData.b("ᡶᵸὺ⵼Ṿ", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_106;
			case 4:
				num = 0;
				continue;
			case 5:
				num = 2;
				continue;
			case 7:
				num = 9;
				continue;
			case 8:
				num = 10;
				continue;
			case 9:
				if (!(A_1 == ClipboardData.b("ቶླྀṺ፼⽾", a_)))
				{
					num = 1;
					continue;
				}
				goto IL_C8;
			case 10:
				if (!(A_1 == ClipboardData.b("᥶ᱸͺॼ㱾", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_FE;
			}
			if (A_1 == null)
			{
				goto IL_170;
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
				num = 8;
				continue;
			}
			IL_BB:
			num = 7;
		}
		IL_73:
		A_0.BreakCode = SectionBreakType.NewPage;
		return;
		IL_8D:
		if (true)
		{
		}
		goto IL_170;
		IL_C8:
		A_0.BreakCode = SectionBreakType.EvenPage;
		return;
		IL_FE:
		A_0.BreakCode = SectionBreakType.NewColumn;
		return;
		IL_106:
		A_0.BreakCode = SectionBreakType.Oddpage;
		return;
		IL_170:
		A_0.BreakCode = SectionBreakType.NoBreak;
	}

	// Token: 0x06001FAD RID: 8109 RVA: 0x0021A9B4 File Offset: 0x002199B4
	private void ᜀ(Stream A_0)
	{
		int a_ = 8;
		XmlReader xmlReader;
		string localName;
		for (;;)
		{
			xmlReader = spr\u23D7.ᜀ(A_0);
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					localName = xmlReader.LocalName;
					num = 3;
					continue;
				case 2:
					if (xmlReader.LocalName != ClipboardData.b("੭Ὧᅱų᭵ᵷᑹࡻ", a_))
					{
						num = 9;
						continue;
					}
					goto IL_FB;
				case 3:
					if (xmlReader.LocalName != ClipboardData.b("ᵭᕯٱsήᙷᵹཻ", a_))
					{
						goto IL_148;
					}
					goto IL_FB;
				case 4:
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						num = 1;
						continue;
					}
					xmlReader.Read();
					num = 7;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_148;
					default:
						if (false)
						{
						}
						if (xmlReader == null)
						{
							num = 10;
							continue;
						}
						goto IL_158;
					}
					break;
				case 6:
					if (xmlReader.IsEmptyElement)
					{
						num = 8;
						continue;
					}
					goto IL_186;
				case 7:
					goto IL_158;
				case 8:
					return;
				case 9:
					goto IL_C8;
				case 10:
					goto IL_76;
				}
				break;
				IL_FB:
				num = 6;
				continue;
				IL_148:
				num = 0;
				continue;
				IL_158:
				num = 4;
			}
		}
		IL_76:
		throw new Exception(ClipboardData.b("ᱭᕯ፱ၳ፵੷", a_));
		IL_C8:
		if (true)
		{
		}
		throw new XmlException(ClipboardData.b("㭭ṯ᝱౳ٵᵷ᥹ࡻ᭽ꊁﲃꪉ늑", a_) + xmlReader.LocalName);
		IL_186:
		xmlReader.Read();
		this.ᜁ(xmlReader, localName);
	}

	// Token: 0x06001FAE RID: 8110 RVA: 0x0021AB58 File Offset: 0x00219B58
	private void ᜁ(XmlReader A_0, string A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = false;
				bool flag2 = false;
				int num = 13;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!flag)
						{
							num = 6;
							continue;
						}
						goto IL_8ED;
					case 1:
					{
						string localName;
						int num2;
						if (spr᧓.\u1773.TryGetValue(localName, out num2))
						{
							num = 44;
							continue;
						}
						goto IL_19F;
					}
					case 2:
					{
						int num2;
						switch (num2)
						{
						case 0:
						{
							string attribute = A_0.GetAttribute(ClipboardData.b("ᱫ୭ɯᅱᅳᡵ౷", a_), ClipboardData.b("ѫᩭѯɱ乳奵坷ॹύᙽꚇﲋﺏ煉歹ﺗ솟횡힣袥잧\ud8a9쮫膭잯\uddb1욳튵좷좹펻\uddbdꖿ뇁럃꿅ꛇ귉ꇋꋍￏ뇛뿝觟賡", a_));
							num = 39;
							continue;
						}
						case 1:
						{
							IEnumerator enumerator = this.ᜄ.Sections.GetEnumerator();
							num = 4;
							continue;
						}
						case 2:
						{
							string text = this.ᜁ(A_0, ClipboardData.b("ᩫ཭ᱯ", a_), ClipboardData.b("ѫᩭѯɱ乳奵坷ॹύᙽꚇﲋﺏ煉歹ﺗ솟횡힣袥잧\ud8a9쮫膭잯\uddb1욳튵좷좹펻\uddbdꖿ뇁럃꿅ꛇ귉ꇋꋍￏ뇛뿝觟賡", a_));
							IEnumerator enumerator2 = this.ᜄ.Sections.GetEnumerator();
							num = 41;
							continue;
						}
						case 3:
							this.ᜄ.DOP.ᜃ(true);
							num = 14;
							continue;
						case 4:
							flag2 = true;
							num = 48;
							continue;
						case 5:
							this.ᜄ.DOP.\u1717().ᜑ(true);
							num = 11;
							continue;
						case 6:
							this.ᜄ.TrackChanges = true;
							num = 33;
							continue;
						case 7:
							goto IL_35A;
						case 8:
						case 9:
						case 10:
						case 11:
						case 12:
						case 13:
						case 14:
						case 15:
							A_0.Skip();
							flag = true;
							num = 8;
							continue;
						case 16:
							num = 49;
							continue;
						case 17:
							num = 20;
							continue;
						case 18:
							this.ᜃ(A_0);
							num = 19;
							continue;
						case 19:
							this.ᜄ(A_0);
							num = 23;
							continue;
						case 20:
							this.ᜆ(A_0);
							num = 28;
							continue;
						case 21:
							this.ᜄ.DOP.ᜄ(this.ᜂ(A_0));
							num = 21;
							continue;
						case 22:
						{
							string attribute2 = A_0.GetAttribute(ClipboardData.b("ᩫ཭ᱯ", a_), ClipboardData.b("ѫᩭѯɱ乳奵坷ॹύᙽꚇﲋﺏ煉歹ﺗ솟횡힣袥잧\ud8a9쮫膭잯\uddb1욳튵좷좹펻\uddbdꖿ뇁럃꿅ꛇ귉ꇋꋍￏ뇛뿝觟賡", a_));
							num = 37;
							continue;
						}
						case 23:
						{
							string attribute3 = A_0.GetAttribute(ClipboardData.b("ᩫ཭ᱯ", a_), ClipboardData.b("ѫᩭѯɱ乳奵坷ॹύᙽꚇﲋﺏ煉歹ﺗ솟횡힣袥잧\ud8a9쮫膭잯\uddb1욳튵좷좹펻\uddbdꖿ뇁럃꿅ꛇ귉ꇋꋍￏ뇛뿝觟賡", a_));
							num = 43;
							continue;
						}
						case 24:
							this.ᜄ.DOP.ᜂ(!this.ᜂ(A_0));
							num = 36;
							continue;
						default:
							num = 12;
							continue;
						}
						break;
					}
					case 3:
						num = 24;
						continue;
					case 4:
						goto IL_726;
					case 5:
						goto IL_35A;
					case 6:
						A_0.Read();
						num = 7;
						continue;
					case 7:
						goto IL_8ED;
					case 8:
						goto IL_35A;
					case 9:
						goto IL_35A;
					case 10:
					{
						string localName;
						if ((localName = A_0.LocalName) != null)
						{
							num = 46;
							continue;
						}
						goto IL_19F;
					}
					case 11:
						goto IL_35A;
					case 12:
						num = 17;
						continue;
					case 13:
						goto IL_8ED;
					case 14:
						goto IL_35A;
					case 15:
						goto IL_35A;
					case 16:
						spr᧓.\u1773 = new Dictionary<string, int>(25)
						{
							{
								ClipboardData.b("ᙫŭὯά", a_),
								0
							},
							{
								ClipboardData.b("࡫୭ᙯ፱ų᩵౷⹹ᵻᱽ퍿", a_),
								1
							},
							{
								ClipboardData.b("ཫ٭ᅯqᕳᕵ౷ό๻⵽쾋ﺏ秊", a_),
								2
							},
							{
								ClipboardData.b("ūݭɯq᭳ѵ㕷᭹๻᥽", a_),
								3
							},
							{
								ClipboardData.b("࡫ݭͯɱᡳ᝵ŷ㡹ᵻᵽﶇ\udd8d", a_),
								4
							},
							{
								ClipboardData.b("࡫ŭ㹯ᵱs㉵ᅷॹ౻ችﮁ풃캋ﲑ鍊", a_),
								5
							},
							{
								ClipboardData.b("ᡫᱭᅯᅱέ⑵ᵷ౹ᕻൽ", a_),
								6
							},
							{
								ClipboardData.b("ᥫݭ㍯ᵱᥳٵ᥷๹䕻䥽푿뚃뚅뢇릉", a_),
								7
							},
							{
								ClipboardData.b("ѫ੭ɯⅱᱳ᝵ࡷό㡻᭽ﲇ黎", a_),
								8
							},
							{
								ClipboardData.b("੫ŭὯٱᩳ᥵౷όⱻ౽", a_),
								9
							},
							{
								ClipboardData.b("५mᑯᱱ᭳ɵᵷ⩹๻", a_),
								10
							},
							{
								ClipboardData.b("ṫᵭ᥯ᙱݳ", a_),
								11
							},
							{
								ClipboardData.b("ū཭ѯᩱ⑳ѵ", a_),
								12
							},
							{
								ClipboardData.b("ᡫ٭ᕯάᅳふ᝷ᑹࡻ㉽", a_),
								13
							},
							{
								ClipboardData.b("ཫɭɯⅱᝳṵᵷ᝹᥻㍽", a_),
								14
							},
							{
								ClipboardData.b("Ὣ٭ᅯɱᅳ㉵ᵷᱹᵻ୽", a_),
								15
							},
							{
								ClipboardData.b("५ᡭᕯᱱ㕳ᡵᱷ㕹᡻᩽졿ﾋ", a_),
								16
							},
							{
								ClipboardData.b("࡫ŭ፯⑱ᕳѵ୷", a_),
								17
							},
							{
								ClipboardData.b("ᩫݭᕯձ", a_),
								18
							},
							{
								ClipboardData.b("࡫ŭ፯ݱᥳ፵ᙷ๹ⱻ౽ﲇ", a_),
								19
							},
							{
								ClipboardData.b("ཫŭᵯɱᕳɵ", a_),
								20
							},
							{
								ClipboardData.b("൫᭭ѯᵱ㱳ཱུࡷቹ᥻ၽ", a_),
								21
							},
							{
								ClipboardData.b("ཫŭṯűᅳᕵ൷๹ᕻࡽ쪁ﶃ슍憐ﾑﶓ", a_),
								22
							},
							{
								ClipboardData.b("ѫ᝭oᩱᅳᡵ᥷๹ᕻᅽ\ud881", a_),
								23
							},
							{
								ClipboardData.b("࡫ŭ㹯ᵱs㹵ŷ੹ᑻ᭽쮇ﲋﶍ", a_),
								24
							}
						};
						num = 34;
						continue;
					case 17:
						goto IL_19F;
					case 18:
						return;
					case 19:
						goto IL_35A;
					case 20:
						if (!A_0.IsEmptyElement)
						{
							num = 47;
							continue;
						}
						goto IL_35A;
					case 21:
						goto IL_35A;
					case 22:
					{
						string attribute2;
						this.ᜄ.DOP.ᜂ(int.Parse(attribute2, NumberStyles.Integer, CultureInfo.InvariantCulture));
						num = 15;
						continue;
					}
					case 23:
						goto IL_35A;
					case 24:
						if (!flag2)
						{
							num = 32;
							continue;
						}
						return;
					case 25:
						if (spr᧓.\u1773 == null)
						{
							num = 16;
							continue;
						}
						goto IL_1C9;
					case 26:
						goto IL_8ED;
					case 27:
						goto IL_35A;
					case 28:
						goto IL_35A;
					case 29:
						goto IL_35A;
					case 30:
						num = 10;
						continue;
					case 31:
						if (A_0.NodeType == XmlNodeType.Element)
						{
							num = 30;
							continue;
						}
						A_0.Read();
						num = 26;
						continue;
					case 32:
						this.ᜄ.Background.Type = BackgroundType.NoBackground;
						num = 18;
						continue;
					case 33:
						goto IL_35A;
					case 34:
						goto IL_1C9;
					case 35:
						if (!(A_0.LocalName != A_1))
						{
							num = 3;
							continue;
						}
						flag = false;
						num = 31;
						continue;
					case 36:
						goto IL_35A;
					case 37:
					{
						string attribute2;
						if (attribute2 != null)
						{
							num = 22;
							continue;
						}
						goto IL_35A;
					}
					case 38:
						this.ᜄ.Sections[0].PageSetup.DifferentOddAndEvenPagesHeaderFooter = true;
						num = 9;
						continue;
					case 39:
					{
						string attribute;
						if (attribute != null)
						{
							num = 45;
							continue;
						}
						goto IL_35A;
					}
					case 40:
					{
						string attribute3;
						this.ᜄ.DOP.ᜃ(int.Parse(attribute3, NumberStyles.Integer, CultureInfo.InvariantCulture));
						num = 29;
						continue;
					}
					case 41:
						try
						{
							num = 12;
							for (;;)
							{
								switch (num)
								{
								case 0:
									num = 14;
									continue;
								case 2:
									num = 8;
									continue;
								case 3:
								{
									string a;
									if (!(a == ClipboardData.b("ཫŭᵯɱٳ፵୷ॹⱻ୽ﺉﺏ펑望튗ﮙﾝ캟잡힣쎥쮩슫쾭", a_)))
									{
										num = 0;
										continue;
									}
									Section section;
									section.PageSetup.CharacterSpacingControl = CharacterSpacing.compressPunctuationAndJapaneseKana;
									num = 4;
									continue;
								}
								case 5:
									num = 6;
									continue;
								case 6:
								{
									string a;
									if (!(a == ClipboardData.b("ཫŭᵯɱٳ፵୷ॹⱻ୽ﺉﺏ", a_)))
									{
										num = 9;
										continue;
									}
									Section section;
									section.PageSetup.CharacterSpacingControl = CharacterSpacing.compressPunctuation;
									num = 1;
									continue;
								}
								case 7:
									num = 11;
									continue;
								case 8:
								{
									string a;
									if (!(a == ClipboardData.b("࡫ŭ㹯ᵱs㕵᝷᝹౻౽", a_)))
									{
										num = 5;
										continue;
									}
									Section section;
									section.PageSetup.CharacterSpacingControl = CharacterSpacing.doNotCompress;
									num = 15;
									continue;
								}
								case 9:
									num = 3;
									continue;
								case 10:
								{
									IEnumerator enumerator2;
									if (!enumerator2.MoveNext())
									{
										num = 7;
										continue;
									}
									Section section = (Section)enumerator2.Current;
									num = 13;
									continue;
								}
								case 11:
									goto IL_6D8;
								case 13:
								{
									string text;
									string a;
									if ((a = text) != null)
									{
										num = 2;
										continue;
									}
									break;
								}
								}
								IL_62E:
								num = 10;
								continue;
								goto IL_62E;
							}
							IL_6D8:
							goto IL_35A;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator2;
								IDisposable disposable = enumerator2 as IDisposable;
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
										goto IL_725;
									case 2:
										goto IL_723;
									}
									break;
								}
							}
							IL_723:
							IL_725:;
						}
						goto Block_10;
					case 42:
						goto IL_35A;
					case 43:
					{
						string attribute3;
						if (attribute3 != null)
						{
							num = 40;
							continue;
						}
						goto IL_35A;
					}
					case 44:
						num = 2;
						continue;
					case 45:
					{
						string attribute;
						int a_2 = int.Parse(attribute, NumberStyles.Integer, CultureInfo.InvariantCulture);
						this.ᜄ.ViewSetup.ᜀ(a_2);
						num = 42;
						continue;
					}
					case 46:
						num = 25;
						continue;
					case 47:
						goto IL_829;
					case 48:
						goto IL_35A;
					case 49:
						if (this.ᜄ.Sections.Count != 0)
						{
							num = 38;
							continue;
						}
						goto IL_35A;
					}
					break;
					IL_19F:
					this.ᜄ.DocxProps2010.Add(this.ᜢ(A_0));
					flag = true;
					num = 27;
					continue;
					IL_1C9:
					num = 1;
					continue;
					IL_35A:
					num = 0;
					continue;
					Block_10:
					try
					{
						IL_726:
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_7C4;
								default:
									if (false)
									{
									}
									break;
								}
								break;
							case 2:
								num = 3;
								continue;
							case 3:
								goto IL_7DB;
							case 4:
							{
								IEnumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 2;
									continue;
								}
								Section section2 = (Section)enumerator.Current;
								section2.PageSetup.DefaultTabWidth = this.ᜀ(A_0, ClipboardData.b("ᩫ཭ᱯ", a_), ClipboardData.b("ѫᩭѯɱ乳奵坷ॹύᙽꚇﲋﺏ煉歹ﺗ솟횡힣袥잧\ud8a9쮫膭잯\uddb1욳튵좷좹펻\uddbdꖿ뇁럃꿅ꛇ귉ꇋꋍￏ뇛뿝觟賡", a_));
								goto IL_7C4;
							}
							}
							IL_76A:
							num = 4;
							continue;
							goto IL_76A;
							IL_7C4:
							num = 0;
						}
						IL_7DB:
						goto IL_35A;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator;
							IDisposable disposable2 = enumerator as IDisposable;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable2 != null)
									{
										num = 1;
										continue;
									}
									goto IL_828;
								case 1:
									disposable2.Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_826;
								}
								break;
							}
						}
						IL_826:
						IL_828:;
					}
					IL_829:
					if (true)
					{
					}
					this.ᜅ(A_0);
					num = 5;
					continue;
					IL_8ED:
					num = 35;
				}
			}
			return;
		}
	}

	// Token: 0x06001FAF RID: 8111 RVA: 0x0021B7B4 File Offset: 0x0021A7B4
	private void ᜆ(XmlReader A_0)
	{
		int a_ = 11;
		int num = 6;
		for (;;)
		{
			bool flag;
			switch (num)
			{
			case 0:
				goto IL_214;
			case 1:
				A_0.Read();
				num = 12;
				continue;
			case 2:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 10;
					continue;
				}
				A_0.Read();
				num = 5;
				continue;
			case 3:
				if (A_0.IsEmptyElement)
				{
					num = 22;
					continue;
				}
				flag = false;
				A_0.Read();
				num = 0;
				continue;
			case 4:
				if (!flag)
				{
					num = 1;
					continue;
				}
				goto IL_214;
			case 5:
				goto IL_15B;
			case 7:
				num = 17;
				continue;
			case 8:
				goto IL_135;
			case 9:
				if (A_0.LocalName != ClipboardData.b("ተᱲᡴݶᡸེ", a_))
				{
					num = 8;
					continue;
				}
				num = 3;
				continue;
			case 10:
				if (true)
				{
				}
				num = 9;
				continue;
			case 11:
				if (A_0.NodeType == XmlNodeType.Element)
				{
					num = 24;
					continue;
				}
				A_0.Read();
				num = 27;
				continue;
			case 12:
				goto IL_214;
			case 13:
				return;
			case 14:
			{
				string localName;
				if (!(localName == ClipboardData.b("ᕰᱲ㭴ᡶ൸⹺๼᩾즀힂좄쮆\ud988ﾌ\uda9a캠햤욦쪨슪쎬좮", a_)))
				{
					num = 25;
					continue;
				}
				this.ᜄ.DOP.\u1717().ᜆ().ᜅ(this.ᜂ(A_0));
				num = 19;
				continue;
			}
			case 15:
			{
				string localName;
				if ((localName = A_0.LocalName) != null)
				{
					num = 21;
					continue;
				}
				goto IL_300;
			}
			case 16:
				goto IL_9A;
			case 17:
				goto IL_300;
			case 18:
				goto IL_324;
			case 19:
				goto IL_13A;
			case 20:
				goto IL_13A;
			case 21:
				num = 14;
				continue;
			case 22:
				return;
			case 23:
			{
				string localName;
				if (!(localName == ClipboardData.b("ᕰᱲ㭴ᡶ൸⹺๼᩾좀ﾊ첌ﲎ\udf90ﲘ욠쒤얦直\udfaa슬\udfae", a_)))
				{
					num = 7;
					continue;
				}
				this.ᜄ.CompatibilitySettings.ᜀ(CompatibilityOptions.DontUseIndentAsListTabStop, this.ᜂ(A_0));
				num = 20;
				continue;
			}
			case 24:
				num = 15;
				continue;
			case 25:
				num = 23;
				continue;
			case 26:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_324;
				default:
					if (false)
					{
					}
					if (!(A_0.LocalName != ClipboardData.b("ተᱲᡴݶᡸེ", a_)))
					{
						num = 13;
						continue;
					}
					flag = false;
					num = 11;
					continue;
				}
				break;
			case 27:
				goto IL_214;
			}
			if (A_0 == null)
			{
				num = 16;
				continue;
			}
			goto IL_15B;
			IL_13A:
			num = 4;
			continue;
			IL_324:
			goto IL_13A;
			IL_15B:
			num = 2;
			continue;
			IL_214:
			num = 26;
			continue;
			IL_300:
			this.ᜄ.DocxProps2010.Add(this.ᜢ(A_0));
			flag = true;
			num = 18;
		}
		IL_9A:
		throw new Exception(ClipboardData.b("Ͱᙲᑴ፶ᱸॺ", a_));
		IL_135:
		throw new XmlException(ClipboardData.b("㑰୲մቶེ᩸᡼᭾ꆀﮂꦈﾊ놐높ﲜ莠", a_));
	}

	// Token: 0x06001FB0 RID: 8112 RVA: 0x0021BB2C File Offset: 0x0021AB2C
	private void ᜅ(XmlReader A_0)
	{
		int a_ = 10;
		for (;;)
		{
			A_0.Read();
			int num = 5;
			for (;;)
			{
				string attribute;
				string attribute2;
				switch (num)
				{
				case 0:
					goto IL_111;
				case 1:
					num = 7;
					continue;
				case 2:
					if (!string.IsNullOrEmpty(attribute))
					{
						num = 1;
						continue;
					}
					goto IL_111;
				case 3:
					goto IL_123;
				case 4:
					this.ᜄ.Variables.Items.Add(attribute, this.ᜆ(attribute2));
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_123;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 5:
					goto IL_A8;
				case 6:
					return;
				case 7:
					if (!string.IsNullOrEmpty(attribute2))
					{
						num = 4;
						continue;
					}
					goto IL_111;
				}
				break;
				IL_A8:
				attribute = A_0.GetAttribute(ClipboardData.b("ṯ፱ᥳ፵", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱쎳\ud9b5쪷\udeb9첻첽꾿ꇁꇃ뗅믇ꏉꋋ꧍뷏뻑ﯓ跟菡跣裥", a_));
				attribute2 = A_0.GetAttribute(ClipboardData.b("ٯ፱ᡳ", a_), ClipboardData.b("ᡯٱsٵ䉷啹卻ൽ黎ꊋ望瀞튟쾡얣튥\udba7蒩쎫\udcad힯鶱쎳\ud9b5쪷\udeb9첻첽꾿ꇁꇃ뗅믇ꏉꋋ꧍뷏뻑ﯓ跟菡跣裥", a_));
				num = 2;
				continue;
				IL_123:
				if (!(A_0.LocalName != ClipboardData.b("ᑯᵱᝳ⁵᥷ࡹཻ", a_)))
				{
					if (true)
					{
					}
					num = 6;
					continue;
				}
				goto IL_A8;
				IL_111:
				A_0.Read();
				num = 3;
			}
		}
	}

	// Token: 0x06001FB1 RID: 8113 RVA: 0x0021BC9C File Offset: 0x0021AC9C
	private string ᜆ(string A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				stringBuilder = new StringBuilder(A_0);
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					int num4;
					int num5;
					switch (num2)
					{
					case 0:
					{
						int num3 = num4 - num5;
						num2 = 9;
						continue;
					}
					case 1:
						num5 += 2;
						num4 = A_0.IndexOf(ClipboardData.b("⵱", a_), num5);
						num2 = 8;
						continue;
					case 2:
						goto IL_134;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_140;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							if (num5 != -1)
							{
								num2 = 1;
								continue;
							}
							goto IL_1EF;
						}
						break;
					case 4:
					{
						string text = A_0.Substring(num5, 4);
						num2 = 12;
						continue;
					}
					case 5:
						goto IL_140;
					case 6:
						goto IL_1B5;
					case 7:
						goto IL_155;
					case 8:
						if (num4 != -1)
						{
							num2 = 0;
							continue;
						}
						goto IL_1EF;
					case 9:
					{
						int num3;
						if (num3 == 4)
						{
							num2 = 4;
							continue;
						}
						goto IL_1B5;
					}
					case 10:
						goto IL_134;
					case 11:
					{
						string text;
						int utf;
						stringBuilder.Replace(ClipboardData.b("⵱౳", a_) + text + ClipboardData.b("⵱", a_), char.ConvertFromUtf32(utf));
						num2 = 6;
						continue;
					}
					case 12:
					{
						string text;
						int utf;
						if (int.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out utf))
						{
							num2 = 11;
							continue;
						}
						goto IL_1B5;
					}
					}
					break;
					IL_134:
					num2 = 5;
					continue;
					IL_140:
					if (num >= A_0.Length)
					{
						num2 = 7;
						continue;
					}
					num5 = A_0.IndexOf(ClipboardData.b("⵱౳", a_), num);
					num2 = 3;
					continue;
					IL_1B5:
					num = num4;
					num2 = 10;
				}
			}
			IL_155:
			IL_1EF:
			return stringBuilder.ToString();
		}
		}
	}

	// Token: 0x06001FB2 RID: 8114 RVA: 0x0021BEA0 File Offset: 0x0021AEA0
	private void ᜄ(XmlReader A_0)
	{
		int a_ = 0;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ͥ౧ͩᡫ", a_), ClipboardData.b("๥ᱧṩᱫ呭彯嵱ݳᕵၷόᅻώ겁ﲏﮓﮙ躟춡횣솥螧\udda9쎫\udcad풯슱욳\ud9b5\udbb7\udfb9쾻춽ꦿ곁ꏃꯅ꓇ﻋﻍﯓ믕맗동닛", a_));
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_17D;
				case 1:
				{
					string a;
					if ((a = attribute) != null)
					{
						num = 3;
						continue;
					}
					return;
				}
				case 2:
					num = 12;
					continue;
				case 3:
					num = 6;
					continue;
				case 4:
					return;
				case 5:
					if (attribute != null)
					{
						if (true)
						{
						}
						num = 8;
						continue;
					}
					return;
				case 6:
				{
					string a;
					if (!(a == ClipboardData.b("եݧݩū୭ṯٱݳ", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_96;
				}
				case 7:
					num = 11;
					continue;
				case 8:
					num = 14;
					continue;
				case 9:
				{
					string a;
					if (a == ClipboardData.b("ብᩧ୩ཫխᕯᙱ㝳ṵ᥷ᑹ᭻᭽", a_))
					{
						goto IL_FE;
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
						continue;
					}
					break;
				}
				case 10:
					num = 9;
					continue;
				case 11:
				{
					string a;
					if (!(a == ClipboardData.b("eݧᡩūᵭ", a_)))
					{
						num = 10;
						continue;
					}
					goto IL_17F;
				}
				case 12:
				{
					string a;
					if (!(a == ClipboardData.b("ᑥ൧୩࡫Ⅽṯṱ൳", a_)))
					{
						num = 4;
						continue;
					}
					this.ᜄ.ProtectionType = ProtectionType.AllowOnlyReading;
					num = 13;
					continue;
				}
				case 13:
					goto IL_124;
				case 14:
					if (attribute == string.Empty)
					{
						num = 0;
						continue;
					}
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_96:
		this.ᜄ.ProtectionType = ProtectionType.AllowOnlyComments;
		return;
		IL_FE:
		this.ᜄ.ProtectionType = ProtectionType.AllowOnlyRevisions;
		return;
		IL_124:
		return;
		IL_17D:
		return;
		IL_17F:
		this.ᜄ.ProtectionType = ProtectionType.AllowOnlyFormFields;
	}

	// Token: 0x06001FB3 RID: 8115 RVA: 0x0021C0C0 File Offset: 0x0021B0C0
	private void ᜃ(XmlReader A_0)
	{
		int a_ = 15;
		for (;;)
		{
			string attribute = A_0.GetAttribute(ClipboardData.b("ʹᙶᕸ", a_), ClipboardData.b("ᵴͶ൸୺䝼偾꺀ﲎ뾐ﲒ잠첢힤쪦좨\udfaa\udeac膮\udeb0솲튴颶캸풺쾼\udbbe뇀뇂꫄꓆곈룊뻌ꛎ뿐듒룔믖ퟠ쳢裤蛦胨藪", a_));
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 6;
					continue;
				case 1:
					num = 5;
					continue;
				case 2:
					return;
				case 3:
					if (attribute != null)
					{
						num = 7;
						continue;
					}
					return;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B0;
					default:
						goto IL_114;
					}
					break;
				case 5:
				{
					string a;
					if (!(a == ClipboardData.b("ᩴɶ൸᝺ᑼᅾ", a_)))
					{
						num = 2;
						continue;
					}
					this.ᜄ.ViewSetup.DocumentViewType = DocumentViewType.OutlineLayout;
					num = 4;
					continue;
				}
				case 6:
				{
					string a;
					if (!(a == ClipboardData.b("ɴቶ᭸", a_)))
					{
						num = 1;
						continue;
					}
					goto IL_70;
				}
				case 7:
					goto IL_B0;
				case 8:
				{
					string a;
					if ((a = attribute) != null)
					{
						num = 0;
						continue;
					}
					return;
				}
				}
				break;
				IL_B0:
				if (true)
				{
				}
				num = 8;
			}
		}
		IL_70:
		this.ᜄ.ViewSetup.DocumentViewType = DocumentViewType.WebLayout;
		return;
		IL_114:
		if (false)
		{
		}
	}

	// Token: 0x06001FB4 RID: 8116 RVA: 0x0021C220 File Offset: 0x0021B220
	private void ᜁ(ParagraphBase A_0, ParagraphItemCollection A_1)
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
		A_1.Add(A_0);
	}

	// Token: 0x06001FB5 RID: 8117 RVA: 0x0021C264 File Offset: 0x0021B264
	private void ᜀ(ParagraphBase A_0, ParagraphItemCollection A_1)
	{
		for (;;)
		{
			IL_14:
			this.ᜁ(A_0, A_1);
			this.ᜁ(A_0);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5C;
				case 1:
					this.ᜀ(A_0 as DocOleObject, A_1);
					num = 0;
					continue;
				case 2:
					if (A_0 is DocOleObject)
					{
						num = 1;
						continue;
					}
					goto IL_5E;
				}
				goto IL_14;
			}
			IL_5E:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				goto IL_7C;
			}
			IL_5C:
			goto IL_5E;
		}
		IL_7C:
		if (false)
		{
		}
	}

	// Token: 0x06001FB6 RID: 8118 RVA: 0x0021C2F4 File Offset: 0x0021B2F4
	private void ᜀ(DocOleObject A_0, ParagraphItemCollection A_1)
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
		this.ᜀ(new FieldMark(this.ᜄ)
		{
			Type = FieldMarkType.FieldSeparator
		}, A_1);
		DocPicture olePicture = A_0.OlePicture;
		this.ᜀ(olePicture, A_1);
		this.ᜀ(new FieldMark(this.ᜄ)
		{
			Type = FieldMarkType.FieldEnd
		}, A_1);
	}

	// Token: 0x06001FB7 RID: 8119 RVA: 0x0021C374 File Offset: 0x0021B374
	private void ᜀ(DocPicture A_0, string A_1, bool A_2, bool A_3)
	{
		string text;
		for (;;)
		{
			text = this.ᜁ(A_1, A_2, A_3);
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (this.\u1717().ContainsKey(text))
					{
						goto IL_45;
					}
					byte[] array = this.ᜮ(text);
					num = 3;
					continue;
				}
				case 1:
					num = 5;
					continue;
				case 2:
					return;
				case 3:
				{
					byte[] array;
					if (array != null)
					{
						num = 1;
						continue;
					}
					return;
				}
				case 4:
					goto IL_4D;
				case 5:
				{
					byte[] array;
					if (array.Length > 0)
					{
						num = 6;
						continue;
					}
					return;
				}
				case 6:
				{
					byte[] array;
					A_0.LoadImage(array);
					this.\u1717().Add(text, A_0.ImageRecord.ᜀ());
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_45;
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
				}
				}
				break;
				IL_45:
				num = 4;
			}
		}
		IL_4D:
		A_0.ᜀ(this.ᜄ.Images.ᜀ(this.\u1717()[text]));
		sprᠾ sprᠾ = A_0.ImageRecord;
		sprᠾ.ᜂ(sprᠾ.ᜅ() + 1);
	}

	// Token: 0x06001FB8 RID: 8120 RVA: 0x0021C4A0 File Offset: 0x0021B4A0
	internal string ᜁ(string A_0, bool A_1, bool A_2)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 1;
			string result;
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
					{
						if (false)
						{
						}
						Dictionary<string, DictionaryEntry> dictionary = this.ᜅ(ClipboardData.b("ተᱲᡴ᩶ᱸᕺॼ౾꾀ﮂꞈ力", a_));
						result = dictionary[A_0].Value.ToString();
						break;
					}
					}
					num = 10;
					continue;
				case 2:
					return result;
				case 3:
				{
					Dictionary<string, DictionaryEntry> dictionary2 = this.ᜅ(this.ᜋ);
					result = (string)dictionary2[A_0].Value;
					num = 2;
					continue;
				}
				case 4:
					if (true)
					{
					}
					if (this.ᜋ.StartsWith(ClipboardData.b("ተᱲᡴ᩶ᱸᕺॼ౾", a_)))
					{
						num = 0;
						continue;
					}
					num = 7;
					continue;
				case 5:
					return result;
				case 6:
				{
					Dictionary<string, DictionaryEntry> dictionary3 = this.ᜅ(this.ᜋ);
					result = dictionary3[A_0].Value.ToString();
					num = 5;
					continue;
				}
				case 7:
					if (!string.IsNullOrEmpty(this.ᜋ))
					{
						num = 6;
						continue;
					}
					result = this.ᜎ[A_0].Value.ToString();
					num = 12;
					continue;
				case 8:
					if (A_2)
					{
						num = 11;
						continue;
					}
					num = 4;
					continue;
				case 9:
					return result;
				case 10:
					return result;
				case 11:
				{
					Dictionary<string, DictionaryEntry> dictionary4 = this.ᜅ(ClipboardData.b("ὰٲᡴᕶᱸॺᑼᅾ궂ﶄꖊﾌ﶐", a_));
					result = dictionary4[A_0].Value.ToString();
					num = 9;
					continue;
				}
				case 12:
					return result;
				}
				if (A_1)
				{
					num = 3;
				}
				else
				{
					num = 8;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06001FB9 RID: 8121 RVA: 0x0021C6E4 File Offset: 0x0021B6E4
	internal byte[] ᜮ(string A_0)
	{
		int a_ = 3;
		byte[] result;
		for (;;)
		{
			A_0 = A_0.Replace(ClipboardData.b("Ѩ๪६ٮၰ屲", a_), null);
			spr\u22A5 spr_u22A = this.ᜀ(ClipboardData.b("ṨѪὬ୮幰Ṳၴ፶ၸ᩺剼", a_), A_0);
			result = null;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return result;
				case 1:
					if (true)
					{
					}
					result = this.ᜀ(spr_u22A);
					spr_u22A = null;
					this.ᜁ(A_0, ClipboardData.b("Ѩ๪६ٮၰ屲", a_));
					num = 0;
					continue;
				case 2:
					return result;
				case 3:
					spr_u22A = this.ᜀ(ClipboardData.b("Ѩ๪६ٮၰ屲", a_), A_0);
					num = 5;
					continue;
				case 4:
					if (spr_u22A == null)
					{
						num = 3;
						continue;
					}
					result = this.ᜀ(spr_u22A);
					spr_u22A = null;
					this.ᜁ(A_0, ClipboardData.b("ṨѪὬ୮幰Ṳၴ፶ၸ᩺剼", a_));
					num = 2;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return result;
					default:
						if (false)
						{
						}
						if (spr_u22A != null)
						{
							num = 1;
							continue;
						}
						return result;
					}
					break;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x06001FBA RID: 8122 RVA: 0x0021C81C File Offset: 0x0021B81C
	private Image ᜀ(string A_0, bool A_1, bool A_2)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			Image image;
			for (;;)
			{
				string a_2 = null;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return image;
					case 1:
						goto IL_60;
					case 2:
						if (image != null)
						{
							num = 0;
							continue;
						}
						goto IL_16D;
					case 3:
						goto IL_60;
					case 4:
						if (A_1)
						{
							num = 8;
							continue;
						}
						num = 5;
						continue;
					case 5:
						for (;;)
						{
							if (true)
							{
							}
							if (!A_2)
							{
								goto IL_141;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_12A;
							}
						}
						IL_12A:
						if (false)
						{
						}
						num = 7;
						continue;
						IL_141:
						a_2 = this.ᜎ[A_0].Value.ToString();
						num = 6;
						continue;
					case 6:
						goto IL_60;
					case 7:
					{
						Dictionary<string, DictionaryEntry> dictionary = this.ᜅ(ClipboardData.b("᥶౸ᙺὼ᩾ꞈ뾐ﮖ", a_));
						a_2 = dictionary[A_0].Value.ToString();
						num = 3;
						continue;
					}
					case 8:
					{
						Dictionary<string, DictionaryEntry> dictionary2 = this.ᜅ(this.ᜋ);
						a_2 = (string)dictionary2[A_0].Value;
						num = 1;
						continue;
					}
					}
					break;
					IL_60:
					image = this.\u171C(a_2);
					num = 2;
				}
			}
			return image;
			IL_16D:
			return null;
		}
		}
	}

	// Token: 0x06001FBB RID: 8123 RVA: 0x0021C998 File Offset: 0x0021B998
	private byte[] ᜀ(spr\u22A5 A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_31;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		if (A_0 == null)
		{
			return null;
		}
		IL_31:
		int num = (int)A_0.ᜁ().Length;
		byte[] array = new byte[num];
		A_0.ᜁ().Position = 0L;
		A_0.ᜁ().Read(array, 0, num);
		return array;
	}

	// Token: 0x06001FBC RID: 8124 RVA: 0x0021CA0C File Offset: 0x0021BA0C
	private spr\u22A5 ᜀ(string A_0, string A_1)
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
		if (true)
		{
		}
		A_1 = A_1.Replace(A_0, string.Empty);
		sprᭇ sprᭇ = this.ᜄ.DocxPackage.ᜁ(A_0);
		if (!sprᭇ.ᜁ().ContainsKey(A_1))
		{
			return null;
		}
		IL_5C:
		return sprᭇ.ᜁ()[A_1];
	}

	// Token: 0x06001FBD RID: 8125 RVA: 0x0021CA88 File Offset: 0x0021BA88
	private Dictionary<string, DictionaryEntry> ᜅ(string A_0)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 5;
			sprᭇ sprᭇ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (sprᭇ.ᜂ()[ClipboardData.b("ɴᡶ୸ὺ剼⁾Ꚉ", a_) + A_0].ᜁ() == null)
					{
						num = 2;
						continue;
					}
					goto IL_120;
				case 2:
					goto IL_B6;
				case 3:
					if (sprᭇ.ᜂ().ContainsKey(ClipboardData.b("ɴᡶ୸ὺ剼⁾Ꚉ", a_) + A_0))
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_79;
				case 4:
					sprᭇ = this.ᜄ.DocxPackage.ᜁ(ClipboardData.b("ɴᡶ୸ὺ剼", a_));
					num = 3;
					continue;
				}
				if (this.ᜊ().ContainsKey(A_0))
				{
					goto IL_181;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_79;
				default:
					if (false)
					{
					}
					num = 4;
					break;
				}
			}
			IL_79:
			return null;
			IL_B6:
			goto IL_79;
			IL_120:
			sprℏ sprℏ = sprᭇ.ᜂ()[ClipboardData.b("ɴᡶ୸ὺ剼⁾Ꚉ", a_) + A_0];
			sprℏ.ᜁ().Position = 0L;
			XmlReader a_2 = spr\u23D7.ᜀ(sprℏ.ᜁ());
			Dictionary<string, DictionaryEntry> dictionary = new Dictionary<string, DictionaryEntry>();
			this.ᜀ(a_2, dictionary);
			this.ᜊ().Add(A_0, dictionary);
			return this.ᜊ()[A_0];
			IL_181:
			return this.ᜊ()[A_0];
		}
		}
	}

	// Token: 0x06001FBE RID: 8126 RVA: 0x0021CC2C File Offset: 0x0021BC2C
	private float ᜄ(string A_0)
	{
		int a_ = 4;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.EndsWith(ClipboardData.b("ͩɫ", a_)))
				{
					num = 3;
					continue;
				}
				goto IL_108;
			case 1:
				A_0 = ClipboardData.b("婩", a_) + A_0;
				num = 4;
				continue;
			case 3:
				goto IL_103;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					goto IL_D7;
				}
				break;
			}
			IL_2D:
			if (A_0.StartsWith(ClipboardData.b("䑩", a_)))
			{
				num = 1;
				continue;
			}
			goto IL_D7;
			goto IL_2D;
			IL_D7:
			num = 0;
		}
		IL_103:
		if (true)
		{
		}
		float num2 = float.Parse(A_0.Replace(ClipboardData.b("ͩɫ", a_), string.Empty), CultureInfo.InvariantCulture);
		return (float)spr\u1C39.ᜁ().ᜀ((double)num2, PrintUnits.Inch, PrintUnits.Point);
		float result;
		try
		{
			IL_108:
			result = Convert.ToSingle(A_0, CultureInfo.InvariantCulture);
		}
		catch (Exception)
		{
			goto IL_CB;
		}
		return result;
		IL_CB:
		return float.Parse(A_0, CultureInfo.InvariantCulture);
	}

	// Token: 0x06001FBF RID: 8127 RVA: 0x0021CD70 File Offset: 0x0021BD70
	private bool ᜂ(XmlReader A_0)
	{
		int a_ = 6;
		bool result;
		for (;;)
		{
			if (true)
			{
			}
			result = true;
			int num = 0;
			for (;;)
			{
				IL_0B:
				switch (num)
				{
				case 0:
					if (A_0.AttributeCount > 0)
					{
						num = 4;
						continue;
					}
					return result;
				case 1:
				{
					string attribute;
					if (!(attribute == ClipboardData.b("੫཭ᱯűᅳ", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_136;
				}
				case 2:
					num = 1;
					continue;
				case 3:
					goto IL_136;
				case 4:
				{
					string attribute = A_0.GetAttribute(ClipboardData.b("ᩫ཭ᱯ", a_), ClipboardData.b("ѫᩭѯɱ乳奵坷ॹύᙽꚇﲋﺏ煉歹ﺗ솟횡힣袥잧\ud8a9쮫膭잯\uddb1욳튵좷좹펻\uddbdꖿ뇁럃꿅ꛇ귉ꇋꋍￏ뇛뿝觟賡", a_));
					num = 8;
					continue;
				}
				case 5:
					num = 6;
					continue;
				case 6:
				{
					string attribute;
					if (attribute == ClipboardData.b("ͫ࡭ᙯ", a_))
					{
						num = 3;
						continue;
					}
					return result;
				}
				case 7:
					return result;
				case 8:
				{
					string attribute;
					while (!(attribute == ClipboardData.b("屫", a_)))
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
							goto IL_0B;
						}
					}
					goto IL_136;
				}
				}
				break;
				IL_136:
				result = false;
				num = 7;
			}
		}
		return result;
	}

	// Token: 0x06001FC0 RID: 8128 RVA: 0x0021CEC4 File Offset: 0x0021BEC4
	private bool ᜀ(XmlReader A_0, string A_1)
	{
		int a_ = 12;
		bool result;
		for (;;)
		{
			result = true;
			int num = 6;
			for (;;)
			{
				IL_0B:
				switch (num)
				{
				case 0:
					return result;
				case 1:
				{
					string attribute;
					if (attribute == ClipboardData.b("ᵱታၵ", a_))
					{
						num = 4;
						continue;
					}
					return result;
				}
				case 2:
				{
					string attribute = A_0.GetAttribute(ClipboardData.b("ѱᕳ᩵", a_), A_1);
					num = 3;
					continue;
				}
				case 3:
				{
					string attribute;
					while (!(attribute == ClipboardData.b("䉱", a_)))
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
						num = 5;
						goto IL_0B;
					}
					goto IL_126;
				}
				case 4:
					goto IL_126;
				case 5:
					num = 7;
					continue;
				case 6:
					if (A_0.AttributeCount > 0)
					{
						num = 2;
						continue;
					}
					return result;
				case 7:
				{
					string attribute;
					if (!(attribute == ClipboardData.b("ᑱᕳ᩵୷ό", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_126;
				}
				case 8:
					num = 1;
					continue;
				}
				break;
				IL_126:
				result = false;
				num = 0;
			}
		}
		return result;
	}

	// Token: 0x06001FC1 RID: 8129 RVA: 0x0021D008 File Offset: 0x0021C008
	private string ᜁ(XmlReader A_0)
	{
		int num = 2;
		string result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.NodeType != XmlNodeType.EndElement)
				{
					num = 1;
					continue;
				}
				result = string.Empty;
				num = 3;
				continue;
			case 1:
				result = A_0.Value;
				A_0.Skip();
				goto IL_6B;
			case 3:
				goto IL_B9;
			case 4:
				goto IL_73;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6B;
				default:
					goto IL_56;
				}
				break;
			}
			if (true)
			{
			}
			if (A_0.IsEmptyElement)
			{
				num = 5;
				continue;
			}
			A_0.Read();
			num = 0;
			continue;
			IL_6B:
			num = 4;
		}
		IL_56:
		if (false)
		{
		}
		A_0.Read();
		return string.Empty;
		IL_73:
		IL_B9:
		A_0.Skip();
		return result;
	}

	// Token: 0x06001FC2 RID: 8130 RVA: 0x0021D0E4 File Offset: 0x0021C0E4
	private Color ᜃ(string A_0)
	{
		int a_ = 9;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (!A_0.StartsWith(ClipboardData.b("८ᡰὲᥴ坶ᵸོ᩺ᑾ", a_)))
				{
					num = 4;
					continue;
				}
				goto IL_55;
			case 1:
			{
				Color color;
				if (color == Color.Empty)
				{
					num = 9;
					continue;
				}
				return color;
			}
			case 2:
			{
				Color color;
				return color;
			}
			case 4:
				num = 5;
				continue;
			case 5:
			{
				if (A_0.StartsWith(ClipboardData.b("८ᡰὲᥴ坶ᕸቺ᩼᝾", a_)))
				{
					num = 10;
					continue;
				}
				Color color = this.ᜁ(A_0);
				num = 1;
				continue;
			}
			case 6:
				goto IL_50;
			case 7:
				goto IL_DA;
			case 8:
				if (A_0 == ClipboardData.b("๮Ѱݲᩴ", a_))
				{
					goto IL_CF;
				}
				num = 0;
				continue;
			case 9:
			{
				Color color = this.ᜀ(A_0);
				num = 2;
				continue;
			}
			case 10:
				goto IL_55;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 8;
			continue;
			IL_CF:
			num = 7;
			continue;
			IL_55:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_CF;
			default:
				goto IL_6B;
			}
		}
		IL_50:
		return Color.Empty;
		IL_6B:
		if (false)
		{
		}
		return this.ᜂ(A_0);
		IL_DA:
		if (true)
		{
		}
		return Color.Black;
	}

	// Token: 0x06001FC3 RID: 8131 RVA: 0x0021D258 File Offset: 0x0021C258
	private Color ᜂ(string A_0)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num4;
			for (;;)
			{
				IL_5C:
				int num = A_0.IndexOf(ClipboardData.b("䝮", a_)) + 1;
				int num2 = A_0.IndexOf(ClipboardData.b("䙮", a_));
				int num3 = 0;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1F2;
					default:
						if (false)
						{
						}
						switch (num3)
						{
						case 0:
							if (num != -1)
							{
								num3 = 1;
								continue;
							}
							goto IL_1F2;
						case 1:
							num3 = 5;
							continue;
						case 2:
							if (!A_0.StartsWith(ClipboardData.b("८ᡰὲᥴ坶ᵸོ᩺ᑾ", a_)))
							{
								num3 = 6;
								continue;
							}
							num3 = 7;
							continue;
						case 3:
							num4 = int.Parse(A_0.Substring(num, num2 - num));
							num3 = 2;
							continue;
						case 4:
							goto IL_D8;
						case 5:
							if (num2 != -1)
							{
								num3 = 3;
								continue;
							}
							goto IL_1F2;
						case 6:
							if (true)
							{
							}
							num3 = 4;
							continue;
						case 7:
							goto IL_BC;
						}
						goto IL_5C;
					}
				}
			}
			IL_BC:
			string text = ClipboardData.b("幮", a_);
			goto IL_10C;
			IL_D8:
			text = ClipboardData.b("嵮", a_);
			IL_10C:
			string str = text;
			int alpha = int.Parse(ClipboardData.b("੮ᝰ", a_), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			int red = int.Parse(ClipboardData.b("८䅰", a_), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			int green = int.Parse(ClipboardData.b("彮", a_) + str, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			int blue = int.Parse(num4.ToString(ClipboardData.b("㝮䍰", a_)), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			return Color.FromArgb(alpha, red, green, blue);
			IL_1F2:
			return Color.White;
		}
		}
	}

	// Token: 0x06001FC4 RID: 8132 RVA: 0x0021D45C File Offset: 0x0021C45C
	private Color ᜁ(string A_0)
	{
		int a_ = 3;
		switch (0)
		{
		default:
			for (;;)
			{
				A_0 = A_0.Replace(ClipboardData.b("䩨", a_), string.Empty);
				int num = 14;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_251;
					case 1:
					{
						int num2 = 6 - A_0.Length;
						int num3 = 0;
						num = 0;
						continue;
					}
					case 2:
						if (A_0.Length != 3)
						{
							num = 1;
							continue;
						}
						goto IL_176;
					case 3:
						if (A_0.Length < 6)
						{
							num = 10;
							continue;
						}
						goto IL_176;
					case 4:
						if (A_0.Length == 3)
						{
							num = 6;
							continue;
						}
						goto IL_1E9;
					case 5:
						num = 9;
						continue;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_174;
						default:
							if (false)
							{
							}
							A_0 = A_0.Insert(0, A_0[0].ToString());
							A_0 = A_0.Insert(2, A_0[2].ToString());
							A_0 = A_0.Insert(4, A_0[4].ToString());
							num = 12;
							continue;
						}
						break;
					case 7:
					{
						int num2;
						int num3;
						if (num3 >= num2)
						{
							num = 5;
							continue;
						}
						A_0 = A_0.Insert(0, ClipboardData.b("奨", a_));
						num3++;
						num = 8;
						continue;
					}
					case 8:
						goto IL_251;
					case 9:
						goto IL_1E9;
					case 10:
						goto IL_174;
					case 11:
						A_0 = A_0.Substring(0, 6);
						num = 13;
						continue;
					case 12:
						goto IL_1E9;
					case 13:
						goto IL_1E9;
					case 14:
						if (A_0.Length > 6)
						{
							num = 11;
							continue;
						}
						num = 3;
						continue;
					}
					break;
					IL_174:
					num = 2;
					continue;
					IL_176:
					num = 4;
					continue;
					IL_251:
					if (true)
					{
					}
					num = 7;
					continue;
					try
					{
						IL_1E9:
						string s = A_0.Substring(0, 2);
						string s2 = A_0.Substring(2, 2);
						string s3 = A_0.Substring(4, 2);
						int red = int.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
						int green = int.Parse(s2, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
						int blue = int.Parse(s3, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
						return Color.FromArgb(red, green, blue);
					}
					catch
					{
						goto IL_19C;
					}
					goto IL_251;
				}
			}
			IL_19C:
			return Color.Empty;
		}
	}

	// Token: 0x06001FC5 RID: 8133 RVA: 0x0021D714 File Offset: 0x0021C714
	private Color ᜀ(string A_0)
	{
		int a_ = 10;
		for (;;)
		{
			int num = A_0.IndexOf(ClipboardData.b("⭯", a_));
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num != -1)
					{
						num2 = 2;
						continue;
					}
					goto IL_48;
				case 1:
					goto IL_48;
				case 2:
					goto IL_54;
				}
				break;
				IL_54:
				A_0 = A_0.Remove(num, A_0.Length - num);
				A_0 = A_0.Trim();
				num2 = 1;
				continue;
				try
				{
					IL_48:
					Color result = ColorTranslator.FromHtml(A_0);
					goto IL_87;
				}
				catch
				{
					goto IL_81;
				}
				goto IL_54;
			}
		}
		IL_81:
		return Color.Empty;
		IL_87:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_81;
		default:
		{
			if (true)
			{
			}
			if (false)
			{
			}
			Color result;
			return result;
		}
		}
	}

	// Token: 0x06001FC6 RID: 8134 RVA: 0x0021D7E0 File Offset: 0x0021C7E0
	private float ᜀ(XmlReader A_0, string A_1, string A_2)
	{
		int a_ = 10;
		int num = 4;
		string attribute;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1.Length == 0)
				{
					num = 5;
					continue;
				}
				num = 6;
				continue;
			case 1:
				attribute = A_0.GetAttribute(A_1);
				num = 7;
				continue;
			case 2:
				goto IL_106;
			case 3:
				if (attribute != null)
				{
					num = 2;
					continue;
				}
				goto IL_129;
			case 5:
				goto IL_EB;
			case 6:
				if (A_2 == null)
				{
					num = 1;
					continue;
				}
				attribute = A_0.GetAttribute(A_1, A_2);
				num = 8;
				continue;
			case 7:
				if (true)
				{
				}
				goto IL_ED;
			case 8:
				goto IL_ED;
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_106;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			if (A_1 != null)
			{
				num = 9;
				continue;
			}
			break;
			IL_ED:
			num = 3;
		}
		IL_A1:
		throw new ArgumentException(ClipboardData.b("ㅯٱsѵᅷ᡹ॻ੽ꊁ겋뚕뺝슟잡蒣좥\udda7용삫躭\udfaf삱钳펵햷쪹좻잽", a_));
		IL_EB:
		goto IL_A1;
		IL_106:
		return float.Parse(attribute, NumberStyles.Float, CultureInfo.InvariantCulture) / 20f;
		IL_129:
		return float.MaxValue;
	}

	// Token: 0x06001FC7 RID: 8135 RVA: 0x0021D91C File Offset: 0x0021C91C
	internal MemoryStream ᜢ(XmlReader A_0)
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

	// Token: 0x06001FC8 RID: 8136 RVA: 0x0021D978 File Offset: 0x0021C978
	private void ᜀ(XmlReader A_0)
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
				goto IL_49;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_65;
				default:
					goto IL_8D;
				}
				break;
			case 3:
				return;
			case 4:
				if (A_0.NodeType != XmlNodeType.Whitespace)
				{
					num = 2;
					continue;
				}
				goto IL_65;
			}
			if (A_0.NodeType == XmlNodeType.Element)
			{
				num = 3;
				continue;
			}
			IL_49:
			num = 4;
			continue;
			IL_65:
			A_0.Read();
			num = 1;
		}
		return;
		IL_8D:
		if (false)
		{
		}
	}

	// Token: 0x04001FB1 RID: 8113
	private const char ᜀ = '\u001e';

	// Token: 0x04001FB2 RID: 8114
	private const char ᜁ = '\u001f';

	// Token: 0x04001FB3 RID: 8115
	private spr\u1FDD ᜂ;

	// Token: 0x04001FB4 RID: 8116
	private XmlReader ᜃ;

	// Token: 0x04001FB5 RID: 8117
	private Document ᜄ;

	// Token: 0x04001FB6 RID: 8118
	private string ᜅ;

	// Token: 0x04001FB7 RID: 8119
	private string ᜆ;

	// Token: 0x04001FB8 RID: 8120
	private bool ᜇ;

	// Token: 0x04001FB9 RID: 8121
	private bool ᜈ;

	// Token: 0x04001FBA RID: 8122
	private bool ᜉ;

	// Token: 0x04001FBB RID: 8123
	private float ᜊ;

	// Token: 0x04001FBC RID: 8124
	private string ᜋ = string.Empty;

	// Token: 0x04001FBD RID: 8125
	private Dictionary<string, string> ᜌ;

	// Token: 0x04001FBE RID: 8126
	private Dictionary<string, string> \u170D;

	// Token: 0x04001FBF RID: 8127
	private Dictionary<string, DictionaryEntry> ᜎ;

	// Token: 0x04001FC0 RID: 8128
	private Dictionary<string, bool> ᜏ;

	// Token: 0x04001FC1 RID: 8129
	private Dictionary<string, string> ᜐ;

	// Token: 0x04001FC2 RID: 8130
	private Dictionary<string, Dictionary<string, DictionaryEntry>> ᜑ;

	// Token: 0x04001FC3 RID: 8131
	private Dictionary<string, DocPicture> \u1712;

	// Token: 0x04001FC4 RID: 8132
	private Dictionary<string, string> \u1713;

	// Token: 0x04001FC5 RID: 8133
	private Dictionary<string, string> \u1714;

	// Token: 0x04001FC6 RID: 8134
	private Dictionary<string, string> \u1715;

	// Token: 0x04001FC7 RID: 8135
	private CharacterFormat \u1716;

	// Token: 0x04001FC8 RID: 8136
	private bool \u1717;

	// Token: 0x04001FC9 RID: 8137
	private List<DictionaryEntry> \u1718;

	// Token: 0x04001FCA RID: 8138
	private List<DictionaryEntry> \u1719;

	// Token: 0x04001FCB RID: 8139
	private bool \u171A;

	// Token: 0x04001FCC RID: 8140
	private FieldCharType \u171B;

	// Token: 0x04001FCD RID: 8141
	private StringBuilder \u171C = new StringBuilder();

	// Token: 0x04001FCE RID: 8142
	private Stack<Field> \u171D;

	// Token: 0x04001FCF RID: 8143
	private TrackChangeType \u171E;

	// Token: 0x04001FD0 RID: 8144
	private BookmarkStart \u171F;

	// Token: 0x04001FD1 RID: 8145
	private CommentMark ᜠ;

	// Token: 0x04001FD2 RID: 8146
	private Dictionary<string, Comment> ᜡ;

	// Token: 0x04001FD3 RID: 8147
	private Stack<Comment> ᜢ;

	// Token: 0x04001FD4 RID: 8148
	private int ᜣ = -1;

	// Token: 0x04001FD5 RID: 8149
	private Dictionary<string, int> ᜤ;

	// Token: 0x04001FD6 RID: 8150
	private Stack<int> ᜥ;
}
