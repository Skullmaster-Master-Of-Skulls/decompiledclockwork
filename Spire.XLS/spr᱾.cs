using System;
using System.Reflection;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000441 RID: 1089
[DefaultMember("Item")]
internal class spr\u1C7E : RichTextString
{
	// Token: 0x06004170 RID: 16752 RVA: 0x0024BEDC File Offset: 0x0024AEDC
	internal spr\u1C7E(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜀ(A_1);
	}

	// Token: 0x06004171 RID: 16753 RVA: 0x0024BEF8 File Offset: 0x0024AEF8
	private void ᜀ(object A_0)
	{
		int a_ = 3;
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
			this.ᜀ = (XlsObject.FindParent(A_0, typeof(spr\u1CCF)) as spr\u1CCF);
			if (this.ᜀ == null)
			{
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤸娺似娾⽀㝂", a_), RecordTableEnumerator.b("椸娺似娾⽀㝂敄⡆⭈⅊⡌ⱎ═獒㙔㙖㝘㕚㉜⭞䅠Ţd䝦ཨѪᡬŮᕰ嵲", a_));
			}
			break;
		}
	}

	// Token: 0x06004172 RID: 16754 RVA: 0x0024BF84 File Offset: 0x0024AF84
	public IRichTextString ᜀ(int A_0)
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
		return this.ᜀ.ᜂ(A_0).RichText;
	}

	// Token: 0x06004173 RID: 16755 RVA: 0x0024BFD0 File Offset: 0x0024AFD0
	public new int ᜁ()
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
		return this.ᜀ.ᜯ();
	}

	// Token: 0x06004174 RID: 16756 RVA: 0x0024C018 File Offset: 0x0024B018
	public new IFont ᜁ(int A_0)
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

	// Token: 0x06004175 RID: 16757 RVA: 0x0024C058 File Offset: 0x0024B058
	public void ᜀ(int A_0, int A_1, IFont A_2)
	{
		for (;;)
		{
			IL_18:
			int num = 0;
			int num2 = this.ᜁ();
			for (;;)
			{
				IL_21:
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						return;
					case 1:
						goto IL_47;
					case 2:
						if (num >= num2)
						{
							if (true)
							{
							}
							num3 = 0;
							continue;
						}
						((RichTextString)this.ᜀ(num)).SetRichTextFont(A_0, A_1, A_2);
						num++;
						num3 = 1;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_21;
						default:
							if (false)
							{
							}
							goto IL_47;
						}
						break;
					}
					goto IL_18;
					IL_47:
					num3 = 2;
				}
			}
		}
	}

	// Token: 0x06004176 RID: 16758 RVA: 0x0024C0FC File Offset: 0x0024B0FC
	public new void ᜂ()
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜁ();
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return;
				case 1:
					goto IL_59;
				case 2:
					goto IL_61;
				case 3:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_61;
					default:
						if (false)
						{
						}
						goto IL_59;
					}
					break;
				}
				break;
				IL_59:
				num3 = 2;
				continue;
				IL_61:
				if (num >= num2)
				{
					num3 = 0;
				}
				else
				{
					this.ᜀ(num).ClearFormatting();
					num++;
					num3 = 1;
				}
			}
		}
	}

	// Token: 0x06004177 RID: 16759 RVA: 0x0024C198 File Offset: 0x0024B198
	public void ᜀ(string A_0, IFont A_1)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜁ();
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_61;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_61;
					default:
						if (false)
						{
						}
						goto IL_59;
					}
					break;
				case 2:
					goto IL_59;
				case 3:
					return;
				}
				break;
				IL_59:
				num3 = 0;
				continue;
				IL_61:
				if (num >= num2)
				{
					num3 = 3;
				}
				else
				{
					this.ᜀ(num).Append(A_0, A_1);
					num++;
					num3 = 2;
				}
			}
		}
	}

	// Token: 0x06004178 RID: 16760 RVA: 0x0024C234 File Offset: 0x0024B234
	public void ᜀ()
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜁ();
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return;
				case 1:
					goto IL_47;
				case 2:
					goto IL_59;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_59;
					default:
						if (false)
						{
						}
						goto IL_47;
					}
					break;
				}
				break;
				IL_47:
				num3 = 2;
				continue;
				IL_59:
				if (true)
				{
				}
				if (num >= num2)
				{
					num3 = 0;
				}
				else
				{
					this.ᜀ(num).Clear();
					num++;
					num3 = 1;
				}
			}
		}
	}

	// Token: 0x06004179 RID: 16761 RVA: 0x0024C2D0 File Offset: 0x0024B2D0
	public string ᜆ()
	{
		for (;;)
		{
			int num = this.ᜁ();
			int num2 = 6;
			for (;;)
			{
				string text;
				int num3;
				switch (num2)
				{
				case 0:
					goto IL_45;
				case 1:
					goto IL_AE;
				case 2:
					return text;
				case 3:
					goto IL_AA;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_37;
					default:
						if (false)
						{
						}
						if (text != this.ᜀ(num3).Text)
						{
							num2 = 3;
							continue;
						}
						num3++;
						num2 = 5;
						continue;
					}
					break;
				case 5:
					goto IL_AE;
				case 6:
					goto IL_37;
				case 7:
					if (num3 >= num)
					{
						num2 = 2;
						continue;
					}
					num2 = 4;
					continue;
				}
				break;
				IL_37:
				if (num == 0)
				{
					num2 = 0;
					continue;
				}
				text = this.ᜀ(0).Text;
				num3 = 1;
				num2 = 1;
				continue;
				IL_AE:
				num2 = 7;
			}
		}
		IL_45:
		if (true)
		{
		}
		return null;
		IL_AA:
		return null;
	}

	// Token: 0x0600417A RID: 16762 RVA: 0x0024C3C8 File Offset: 0x0024B3C8
	public void ᜀ(string A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜁ();
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return;
				case 1:
					goto IL_59;
				case 2:
					goto IL_47;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_59;
					default:
						if (false)
						{
						}
						goto IL_47;
					}
					break;
				}
				break;
				IL_47:
				num3 = 1;
				continue;
				IL_59:
				if (num >= num2)
				{
					num3 = 0;
				}
				else
				{
					if (true)
					{
					}
					this.ᜀ(num).Text = A_0;
					num++;
					num3 = 2;
				}
			}
		}
	}

	// Token: 0x0600417B RID: 16763 RVA: 0x0024C464 File Offset: 0x0024B464
	public string ᜃ()
	{
		for (;;)
		{
			int num = this.ᜁ();
			int num2 = 5;
			for (;;)
			{
				if (true)
				{
				}
				string rtfText;
				int num3;
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3F;
					default:
						if (false)
						{
						}
						if (rtfText != this.ᜀ(num3).RtfText)
						{
							num2 = 2;
							continue;
						}
						num3++;
						num2 = 1;
						continue;
					}
					break;
				case 1:
					goto IL_AE;
				case 2:
					goto IL_AA;
				case 3:
					if (num3 >= num)
					{
						num2 = 6;
						continue;
					}
					num2 = 0;
					continue;
				case 4:
					goto IL_AE;
				case 5:
					goto IL_3F;
				case 6:
					return rtfText;
				case 7:
					goto IL_4D;
				}
				break;
				IL_3F:
				if (num == 0)
				{
					num2 = 7;
					continue;
				}
				rtfText = this.ᜀ(0).RtfText;
				num3 = 1;
				num2 = 4;
				continue;
				IL_AE:
				num2 = 3;
			}
		}
		IL_4D:
		return null;
		IL_AA:
		return null;
	}

	// Token: 0x0600417C RID: 16764 RVA: 0x0024C55C File Offset: 0x0024B55C
	public bool ᜄ()
	{
		for (;;)
		{
			int num = this.ᜁ();
			int num2 = 7;
			for (;;)
			{
				bool isFormatted;
				int num3;
				switch (num2)
				{
				case 0:
					goto IL_A9;
				case 1:
					goto IL_A9;
				case 2:
					return false;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_37;
					default:
						if (false)
						{
						}
						if (isFormatted != this.ᜀ(num3).IsFormatted)
						{
							num2 = 4;
							continue;
						}
						num3++;
						if (true)
						{
						}
						num2 = 0;
						continue;
					}
					break;
				case 4:
					return false;
				case 5:
					if (num3 >= num)
					{
						num2 = 6;
						continue;
					}
					num2 = 3;
					continue;
				case 6:
					return isFormatted;
				case 7:
					goto IL_37;
				}
				break;
				IL_37:
				if (num == 0)
				{
					num2 = 2;
					continue;
				}
				isFormatted = this.ᜀ(0).IsFormatted;
				num3 = 1;
				num2 = 1;
				continue;
				IL_A9:
				num2 = 5;
			}
		}
		return false;
	}

	// Token: 0x0600417D RID: 16765 RVA: 0x0024C650 File Offset: 0x0024B650
	public void ᜇ()
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

	// Token: 0x0600417E RID: 16766 RVA: 0x0024C68C File Offset: 0x0024B68C
	public void ᜅ()
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

	// Token: 0x04001D1C RID: 7452
	private new spr\u1CCF ᜀ;
}
