using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Threading;
using Spire.CompoundFile.XLS.Native;
using Spire.Compression;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x020003B4 RID: 948
internal class spr\u17FF : spr\u1DF5
{
	// Token: 0x0600398C RID: 14732 RVA: 0x00206360 File Offset: 0x00205360
	public static BooleanSwitch ᜃ()
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
		return spr\u17FF.ᜏ;
	}

	// Token: 0x0600398D RID: 14733 RVA: 0x002063A0 File Offset: 0x002053A0
	public static bool ᜂ()
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
		return spr\u17FF.\u1712 == DataProviderType.Unsafe;
	}

	// Token: 0x0600398E RID: 14734 RVA: 0x002063E4 File Offset: 0x002053E4
	public static void ᜀ(bool A_0)
	{
		while (A_0)
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
				spr\u17FF.\u1712 = DataProviderType.Unsafe;
				return;
			}
		}
		spr\u17FF.\u1712 = DataProviderType.Native;
	}

	// Token: 0x0600398F RID: 14735 RVA: 0x00206434 File Offset: 0x00205434
	public static DataProviderType ᜁ()
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
		return spr\u17FF.\u1712;
	}

	// Token: 0x06003990 RID: 14736 RVA: 0x00206474 File Offset: 0x00205474
	public static void ᜀ(DataProviderType A_0)
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
		spr\u17FF.\u1712 = A_0;
	}

	// Token: 0x06003991 RID: 14737 RVA: 0x002064B8 File Offset: 0x002054B8
	public bool ᜇ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IWorkbooks workbooks = this.ᜥ();
				int num = 0;
				int count = workbooks.Count;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return false;
					case 1:
						goto IL_A6;
					case 2:
					{
						if (num >= count)
						{
							num2 = 4;
							continue;
						}
						IWorkbook workbook = workbooks[num];
						num2 = 3;
						continue;
					}
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A6;
						default:
						{
							if (false)
							{
							}
							IWorkbook workbook;
							if (!workbook.Saved)
							{
								num2 = 0;
								continue;
							}
							if (true)
							{
							}
							num++;
							num2 = 1;
							continue;
						}
						}
						break;
					case 4:
						return true;
					case 5:
						goto IL_A6;
					}
					break;
					IL_A6:
					num2 = 2;
				}
			}
			return false;
		}
	}

	// Token: 0x06003992 RID: 14738 RVA: 0x00206594 File Offset: 0x00205594
	public int ᜅ()
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
		return this.ᜢ;
	}

	// Token: 0x06003993 RID: 14739 RVA: 0x002065D8 File Offset: 0x002055D8
	public IXLSRange ᜢ()
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
		return this.\u1713;
	}

	// Token: 0x06003994 RID: 14740 RVA: 0x0020661C File Offset: 0x0020561C
	public IWorksheet ᜤ()
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
		return this.\u1714 as IWorksheet;
	}

	// Token: 0x06003995 RID: 14741 RVA: 0x00206664 File Offset: 0x00205664
	public IWorkbook ᜆ()
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
		return this.\u1715;
	}

	// Token: 0x06003996 RID: 14742 RVA: 0x002066A8 File Offset: 0x002056A8
	protected internal spr\u1DF5 ᜧ()
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
		return this;
	}

	// Token: 0x06003997 RID: 14743 RVA: 0x002066E4 File Offset: 0x002056E4
	[DebuggerStepThrough]
	public IWorkbooks ᜥ()
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
		return this.\u1716;
	}

	// Token: 0x06003998 RID: 14744 RVA: 0x00206728 File Offset: 0x00205728
	public IWorksheets ᜡ()
	{
		while (this.ᜆ() != null)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (false)
			{
			}
			return this.ᜆ().Worksheets;
		}
		return null;
	}

	// Token: 0x06003999 RID: 14745 RVA: 0x0020677C File Offset: 0x0020577C
	[DebuggerStepThrough]
	public object \u1716()
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
		return null;
	}

	// Token: 0x0600399A RID: 14746 RVA: 0x002067B8 File Offset: 0x002057B8
	public IXLSRange \u171D()
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
		return this.ᜂ(this);
	}

	// Token: 0x0600399B RID: 14747 RVA: 0x002067FC File Offset: 0x002057FC
	public bool \u1719()
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
		return this.\u1717;
	}

	// Token: 0x0600399C RID: 14748 RVA: 0x00206840 File Offset: 0x00205840
	public void ᜇ(bool A_0)
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
		this.\u1717 = A_0;
	}

	// Token: 0x0600399D RID: 14749 RVA: 0x00206884 File Offset: 0x00205884
	public bool ᜐ()
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
		return this.\u1718;
	}

	// Token: 0x0600399E RID: 14750 RVA: 0x002068C8 File Offset: 0x002058C8
	public void ᜁ(bool A_0)
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
		this.\u1718 = A_0;
	}

	// Token: 0x0600399F RID: 14751 RVA: 0x0020690C File Offset: 0x0020590C
	public double \u171A()
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
		return this.\u1719;
	}

	// Token: 0x060039A0 RID: 14752 RVA: 0x00206950 File Offset: 0x00205950
	public void ᜄ(double A_0)
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
		this.\u1719 = A_0;
	}

	// Token: 0x060039A1 RID: 14753 RVA: 0x00206994 File Offset: 0x00205994
	[DebuggerStepThrough]
	public int \u170D()
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
		return 0;
	}

	// Token: 0x060039A2 RID: 14754 RVA: 0x002069D0 File Offset: 0x002059D0
	public int ᜌ()
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
		return this.\u171A;
	}

	// Token: 0x060039A3 RID: 14755 RVA: 0x00206A14 File Offset: 0x00205A14
	public void ᜂ(int A_0)
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
		this.\u171A = A_0;
	}

	// Token: 0x060039A4 RID: 14756 RVA: 0x00206A58 File Offset: 0x00205A58
	public int \u1712()
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
		return this.\u171B;
	}

	// Token: 0x060039A5 RID: 14757 RVA: 0x00206A9C File Offset: 0x00205A9C
	public void ᜁ(int A_0)
	{
		int a_ = 11;
		while (A_0 < 1)
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
				throw new ArgumentException(RecordTableEnumerator.b("@㝂敄⭆ⱈ⩊㹌㭎煐㱒㭔㉖祘⡚㕜㩞Ѡᝢ䕤੦ᱨᡪᥬ佮ᡰᵲ啴vᙸॺᙼᵾꦆ", a_));
			}
		}
		if (true)
		{
		}
		this.\u171B = A_0;
	}

	// Token: 0x060039A6 RID: 14758 RVA: 0x00206B04 File Offset: 0x00205B04
	public string ᜊ()
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
		return this.\u171C;
	}

	// Token: 0x060039A7 RID: 14759 RVA: 0x00206B48 File Offset: 0x00205B48
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
		this.\u171C = A_0;
	}

	// Token: 0x060039A8 RID: 14760 RVA: 0x00206B8C File Offset: 0x00205B8C
	public string ᜮ()
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
		return Environment.CurrentDirectory;
	}

	// Token: 0x060039A9 RID: 14761 RVA: 0x00206BCC File Offset: 0x00205BCC
	public void ᜂ(string A_0)
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
		Environment.CurrentDirectory = A_0;
	}

	// Token: 0x060039AA RID: 14762 RVA: 0x00206C10 File Offset: 0x00205C10
	public string ᜏ()
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

	// Token: 0x060039AB RID: 14763 RVA: 0x00206C54 File Offset: 0x00205C54
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
		return this.\u171D;
	}

	// Token: 0x060039AC RID: 14764 RVA: 0x00206C98 File Offset: 0x00205C98
	public void ᜀ(string A_0)
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
		this.\u171D = A_0;
	}

	// Token: 0x060039AD RID: 14765 RVA: 0x00206CDC File Offset: 0x00205CDC
	public string ᜉ()
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
		return this.\u171E;
	}

	// Token: 0x060039AE RID: 14766 RVA: 0x00206D20 File Offset: 0x00205D20
	public void ᜆ(string A_0)
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
		this.\u171E = A_0;
	}

	// Token: 0x060039AF RID: 14767 RVA: 0x00206D64 File Offset: 0x00205D64
	public string ᜑ()
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
		return this.\u171F;
	}

	// Token: 0x060039B0 RID: 14768 RVA: 0x00206DA8 File Offset: 0x00205DA8
	public void ᜅ(string A_0)
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
		this.\u171F = A_0;
	}

	// Token: 0x060039B1 RID: 14769 RVA: 0x00206DEC File Offset: 0x00205DEC
	[DebuggerStepThrough]
	public string ᜭ()
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
		return RecordTableEnumerator.b("瀼嘾≀ㅂ⩄㑆♈ⵊ㥌潎ᑐ⭒㙔㉖㕘", a_);
	}

	// Token: 0x060039B2 RID: 14770 RVA: 0x00206E40 File Offset: 0x00205E40
	public bool ᜦ()
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
		return this.ᜠ;
	}

	// Token: 0x060039B3 RID: 14771 RVA: 0x00206E84 File Offset: 0x00205E84
	public void ᜈ(bool A_0)
	{
		int a_ = 15;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 2:
				goto IL_6E;
			case 3:
				if (this.\u1716.Count > 0)
				{
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B2;
				default:
					if (false)
					{
					}
					this.ᜠ = A_0;
					num = 2;
					continue;
				}
				break;
			case 4:
				goto IL_B2;
			}
			if (A_0 == this.ᜠ)
			{
				break;
			}
			num = 0;
		}
		IL_6E:
		return;
		IL_B2:
		if (true)
		{
		}
		throw new ArgumentException(RecordTableEnumerator.b("ቄ⡆㭈⁊⽌⁎㹐㡒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨൪ɬᩮὰᝲ孴坶", a_));
	}

	// Token: 0x060039B4 RID: 14772 RVA: 0x00206F50 File Offset: 0x00205F50
	public SkipExtRecordsType \u171B()
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
		return this.ᜡ;
	}

	// Token: 0x060039B5 RID: 14773 RVA: 0x00206F94 File Offset: 0x00205F94
	public void ᜀ(SkipExtRecordsType A_0)
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
		this.ᜡ = A_0;
	}

	// Token: 0x060039B6 RID: 14774 RVA: 0x00206FD8 File Offset: 0x00205FD8
	public double \u1718()
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
		return (double)this.ᜢ / 20.0;
	}

	// Token: 0x060039B7 RID: 14775 RVA: 0x00207024 File Offset: 0x00206024
	public void ᜁ(double A_0)
	{
		int a_ = 10;
		if (A_0 < 0.0)
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
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ጿ㙁╃⡅ⱇ⭉㹋⩍ᡏ㝑㵓ㅕし⹙", a_));
			}
		}
		this.ᜢ = (int)(A_0 * 20.0);
		this.ᜣ = true;
	}

	// Token: 0x060039B8 RID: 14776 RVA: 0x002070A4 File Offset: 0x002060A4
	public bool ᜰ()
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
		return this.ᜣ;
	}

	// Token: 0x060039B9 RID: 14777 RVA: 0x002070E8 File Offset: 0x002060E8
	public void ᜉ(bool A_0)
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
		this.ᜣ = A_0;
	}

	// Token: 0x060039BA RID: 14778 RVA: 0x0020712C File Offset: 0x0020612C
	public double \u1713()
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
		return this.ᜤ;
	}

	// Token: 0x060039BB RID: 14779 RVA: 0x00207170 File Offset: 0x00206170
	public void ᜀ(double A_0)
	{
		int num = 2;
		for (;;)
		{
			if (true)
			{
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
				switch (num)
				{
				case 0:
					this.ᜤ = A_0;
					num = 1;
					continue;
				case 1:
					return;
				}
				if (this.ᜤ == A_0)
				{
					return;
				}
				num = 0;
				break;
			}
		}
	}

	// Token: 0x060039BC RID: 14780 RVA: 0x002071EC File Offset: 0x002061EC
	public bool \u1717()
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
		return this.ᜥ;
	}

	// Token: 0x060039BD RID: 14781 RVA: 0x00207230 File Offset: 0x00206230
	public void ᜃ(bool A_0)
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
		this.ᜥ = A_0;
	}

	// Token: 0x060039BE RID: 14782 RVA: 0x00207274 File Offset: 0x00206274
	public bool ᜣ()
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
		return this.ᜦ;
	}

	// Token: 0x060039BF RID: 14783 RVA: 0x002072B8 File Offset: 0x002062B8
	public void ᜊ(bool A_0)
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
		this.ᜦ = A_0;
	}

	// Token: 0x060039C0 RID: 14784 RVA: 0x002072FC File Offset: 0x002062FC
	public char \u171E()
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
		return this.ᜧ;
	}

	// Token: 0x060039C1 RID: 14785 RVA: 0x00207340 File Offset: 0x00206340
	public void ᜀ(char A_0)
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
		this.ᜧ = A_0;
	}

	// Token: 0x060039C2 RID: 14786 RVA: 0x00207384 File Offset: 0x00206384
	public char ᜬ()
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
		return this.ᜨ;
	}

	// Token: 0x060039C3 RID: 14787 RVA: 0x002073C8 File Offset: 0x002063C8
	public void ᜁ(char A_0)
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
		this.ᜨ = A_0;
	}

	// Token: 0x060039C4 RID: 14788 RVA: 0x0020740C File Offset: 0x0020640C
	public string \u1714()
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
		return this.ᜩ;
	}

	// Token: 0x060039C5 RID: 14789 RVA: 0x00207450 File Offset: 0x00206450
	public void ᜄ(string A_0)
	{
		int a_ = 3;
		if (true)
		{
		}
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_74;
			case 1:
				if (A_0.Length == 0)
				{
					num = 0;
					continue;
				}
				goto IL_A6;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_34;
				default:
					goto IL_8C;
				}
				break;
			}
			goto IL_31;
			IL_34:
			num = 2;
			continue;
			IL_31:
			if (A_0 == null)
			{
				goto IL_34;
			}
			num = 1;
		}
		IL_74:
		throw new ArgumentException(RecordTableEnumerator.b("伸娺儼䨾⑀捂桄杆㩈㽊㽌♎㽐㑒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨๪lὮհੲ", a_));
		IL_8C:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("伸娺儼䨾⑀", a_));
		IL_A6:
		this.ᜩ = A_0;
	}

	// Token: 0x060039C6 RID: 14790 RVA: 0x0020750C File Offset: 0x0020650C
	public bool ᜎ()
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
		return spr\u17FF.ᜂ();
	}

	// Token: 0x060039C7 RID: 14791 RVA: 0x0020754C File Offset: 0x0020654C
	public void ᜅ(bool A_0)
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
		spr\u17FF.ᜀ(A_0);
	}

	// Token: 0x060039C8 RID: 14792 RVA: 0x00207590 File Offset: 0x00206590
	public bool ᜈ()
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
		return this.ᜪ;
	}

	// Token: 0x060039C9 RID: 14793 RVA: 0x002075D4 File Offset: 0x002065D4
	public void ᜄ(bool A_0)
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
		this.ᜪ = A_0;
	}

	// Token: 0x060039CA RID: 14794 RVA: 0x00207618 File Offset: 0x00206618
	public int ᜨ()
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
		return this.ᜫ;
	}

	// Token: 0x060039CB RID: 14795 RVA: 0x0020765C File Offset: 0x0020665C
	public void ᜀ(int A_0)
	{
		int a_ = 15;
		if (true)
		{
		}
		if (A_0 <= 0)
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
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᝄ⡆㹈ᡊ㥌⁎⍐㉒㉔㉖ᡘ㝚ㅜぞɠɢᅤ๦٨ժ⽬ͮṰၲṴ", a_), RecordTableEnumerator.b("ᕄ㕆♈㭊⡌㵎═⩒畔㩖ⱘ⡚⥜罞͠٢䕤୦ࡨᥪ੬੮Ͱ卲Ŵὶᡸᕺ嵼վꦆ", a_));
			}
		}
		this.ᜫ = A_0;
	}

	// Token: 0x060039CC RID: 14796 RVA: 0x002076D0 File Offset: 0x002066D0
	public bool \u171C()
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
		return this.ᜬ;
	}

	// Token: 0x060039CD RID: 14797 RVA: 0x00207714 File Offset: 0x00206714
	public void ᜂ(bool A_0)
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
		this.ᜬ = A_0;
	}

	// Token: 0x060039CE RID: 14798 RVA: 0x00207758 File Offset: 0x00206758
	public ExcelVersion ᜋ()
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
		return this.ᜭ;
	}

	// Token: 0x060039CF RID: 14799 RVA: 0x0020779C File Offset: 0x0020679C
	public void ᜀ(ExcelVersion A_0)
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
		this.ᜭ = A_0;
	}

	// Token: 0x060039D0 RID: 14800 RVA: 0x002077E0 File Offset: 0x002067E0
	public bool ᜠ()
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
		return !this.ᜮ;
	}

	// Token: 0x060039D1 RID: 14801 RVA: 0x00207824 File Offset: 0x00206824
	public void ᜆ(bool A_0)
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
		this.ᜮ = !A_0;
	}

	// Token: 0x060039D2 RID: 14802 RVA: 0x0020786C File Offset: 0x0020686C
	public DataProviderType ᜯ()
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
		return spr\u17FF.\u1712;
	}

	// Token: 0x060039D3 RID: 14803 RVA: 0x002078AC File Offset: 0x002068AC
	public void ᜁ(DataProviderType A_0)
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
		spr\u17FF.\u1712 = A_0;
	}

	// Token: 0x060039D4 RID: 14804 RVA: 0x002078F0 File Offset: 0x002068F0
	public CompressionLevel? ᜫ()
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
		return this.ᜯ;
	}

	// Token: 0x060039D5 RID: 14805 RVA: 0x00207934 File Offset: 0x00206934
	public void ᜀ(CompressionLevel? A_0)
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
		this.ᜯ = A_0;
	}

	// Token: 0x060039D6 RID: 14806 RVA: 0x00207978 File Offset: 0x00206978
	static spr\u17FF()
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
		spr\u17FF.\u170D = new SizeF(8.43f, 12.75f);
		spr\u17FF.ᜏ = new BooleanSwitch(RecordTableEnumerator.b("ᝃ㙅ⅇ㡉⥋恍ࡏ㹑❓硕᱗㽙㹛⭝ݟ⭡੣eݧ", a_), RecordTableEnumerator.b("ൃ⡅ⱇ⍉⽋⽍⑏㝑❓癕⽗㽙⡛㙝՟ၡ䑣ብݧ䩩Ὣ٭Ὧձ味᩵ᅷ᡹๻ώﮁꒃ曆낏ﾑﮙﮛﮝ펟財", a_));
		spr\u17FF.ᜐ = spr\u17FF.ᜏ.Enabled;
		spr\u17FF.ᜑ = Assembly.GetExecutingAssembly().GetTypes();
		spr\u17FF.\u1712 = DataProviderType.Native;
		Bitmap bitmap = new Bitmap(1, 1);
		spr\u17FF.ᜎ = Graphics.FromImage(bitmap);
		PointF[] array = new PointF[]
		{
			new PointF(1f, 1f)
		};
		GraphicsContainer container = spr\u17FF.ᜎ.BeginContainer(new Rectangle(0, 0, 1, 1), new Rectangle(0, 0, 1, 1), GraphicsUnit.Pixel);
		spr\u17FF.ᜎ.PageUnit = GraphicsUnit.Inch;
		spr\u17FF.ᜎ.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, array);
		spr\u17FF.ᜎ.EndContainer(container);
		float x = array[0].X;
		spr\u17FF.ᜌ = new double[]
		{
			(double)x / 75.0,
			(double)x / 300.0,
			(double)x,
			(double)x / 25.4,
			(double)x / 2.54,
			1.0,
			(double)x / 72.0,
			(double)x / 72.0 / 12700.0
		};
		bitmap.Dispose();
		spr\u17FF.\u170D.Height = (float)spr\u17FF.ᜁ((double)spr\u17FF.\u170D.Height, MeasureUnits.Point);
		spr\u17FF.ᜎ.PageUnit = GraphicsUnit.Pixel;
	}

	// Token: 0x060039D7 RID: 14807 RVA: 0x00207B5C File Offset: 0x00206B5C
	public spr\u17FF()
	{
		int a_ = 4;
		this.ᜋ = string.Concat(Path.PathSeparator);
		this.ᜢ = 255;
		this.ᜤ = 8.43;
		this.ᜧ = ';';
		this.ᜨ = ',';
		this.ᜩ = RecordTableEnumerator.b("ᘹ", a_);
		this.ᜫ = 128;
		this.ᜬ = true;
		base..ctor();
		this.\u171F = Environment.UserName;
		this.\u1719 = 10.0;
		this.\u171A = 4;
		this.\u171B = 3;
		this.\u171C = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
		this.\u171D = RecordTableEnumerator.b("渹崻嘽⼿⽁╃", a_);
		this.\u171E = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
		this.ᜩ();
	}

	// Token: 0x060039D8 RID: 14808 RVA: 0x00207C4C File Offset: 0x00206C4C
	protected void ᜩ()
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
		this.\u1716 = new sprᬭ(this.ᜧ(), this);
	}

	// Token: 0x060039D9 RID: 14809 RVA: 0x00207C9C File Offset: 0x00206C9C
	public double ᜃ(double A_0)
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
		return this.ᜁ((double)((float)A_0), MeasureUnits.Centimeter, MeasureUnits.Point);
	}

	// Token: 0x060039DA RID: 14810 RVA: 0x00207CE4 File Offset: 0x00206CE4
	public double ᜂ(double A_0)
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
		return this.ᜁ((double)((float)A_0), MeasureUnits.Inch, MeasureUnits.Point);
	}

	// Token: 0x060039DB RID: 14811 RVA: 0x00207D2C File Offset: 0x00206D2C
	public void ᜃ(string A_0)
	{
		int num = 2;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
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
					return;
				case 1:
					this.ᜆ().SaveAs(A_0);
					num = 0;
					continue;
				}
				if (this.ᜆ() == null)
				{
					return;
				}
				num = 1;
				break;
			}
		}
	}

	// Token: 0x060039DC RID: 14812 RVA: 0x00207DAC File Offset: 0x00206DAC
	public virtual XlsWorkbook ᜀ(object A_0, ExcelVersion A_1)
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
		return new XlsWorkbook(this, A_0, A_1);
	}

	// Token: 0x060039DD RID: 14813 RVA: 0x00207DF0 File Offset: 0x00206DF0
	public virtual XlsWorkbook ᜀ(object A_0, Stream A_1, string A_2, int A_3, int A_4, ExcelVersion A_5, string A_6, Encoding A_7)
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
		return new XlsWorkbook(this, A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7);
	}

	// Token: 0x060039DE RID: 14814 RVA: 0x00207E40 File Offset: 0x00206E40
	public virtual XlsWorkbook ᜀ(object A_0, Stream A_1, ExcelVersion A_2, ExcelParseOptions A_3)
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
		return new XlsWorkbook(this, A_0, A_1, A_3, A_2);
	}

	// Token: 0x060039DF RID: 14815 RVA: 0x00207E88 File Offset: 0x00206E88
	public virtual XlsWorkbook ᜀ(object A_0, Stream A_1, ExcelParseOptions A_2, ExcelVersion A_3)
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
		return new XlsWorkbook(this, A_0, A_1, A_2, A_3);
	}

	// Token: 0x060039E0 RID: 14816 RVA: 0x00207ED0 File Offset: 0x00206ED0
	public virtual XlsWorkbook ᜀ(object A_0, string A_1, ExcelParseOptions A_2, bool A_3, string A_4, ExcelVersion A_5)
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
		return new XlsWorkbook(this, A_0, A_1, A_2, A_3, A_4, A_5);
	}

	// Token: 0x060039E1 RID: 14817 RVA: 0x00207F1C File Offset: 0x00206F1C
	public virtual XlsWorkbook ᜀ(object A_0, int A_1, ExcelVersion A_2)
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
		return new XlsWorkbook(this, A_0, A_1, A_2);
	}

	// Token: 0x060039E2 RID: 14818 RVA: 0x00207F60 File Offset: 0x00206F60
	public virtual XlsWorkbook ᜀ(object A_0, string A_1, ExcelVersion A_2)
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
		return new XlsWorkbook(this, A_0, A_1, A_2);
	}

	// Token: 0x060039E3 RID: 14819 RVA: 0x00207FA4 File Offset: 0x00206FA4
	public virtual XlsWorkbook ᜀ(object A_0, string A_1, ExcelParseOptions A_2, ExcelVersion A_3)
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
		return new XlsWorkbook(this, A_0, A_1, A_2, A_3);
	}

	// Token: 0x060039E4 RID: 14820 RVA: 0x00207FEC File Offset: 0x00206FEC
	public virtual XlsWorkbook ᜀ(object A_0, Stream A_1, ExcelParseOptions A_2, bool A_3, string A_4, ExcelVersion A_5)
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
		return new XlsWorkbook(this, A_0, A_1, A_2, A_3, A_4, A_5);
	}

	// Token: 0x060039E5 RID: 14821 RVA: 0x00208038 File Offset: 0x00207038
	internal virtual XlsWorksheet ᜁ(object A_0)
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
		return new XlsWorksheet(this, A_0);
	}

	// Token: 0x060039E6 RID: 14822 RVA: 0x0020807C File Offset: 0x0020707C
	internal virtual XlsWorksheet ᜀ(object A_0, sprἛ A_1, ExcelParseOptions A_2, bool A_3, Dictionary<int, int> A_4, IDecryptor A_5)
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
		return new XlsWorksheet(this, A_0, A_1, A_2, A_3, A_4, A_5);
	}

	// Token: 0x060039E7 RID: 14823 RVA: 0x002080C8 File Offset: 0x002070C8
	public virtual XlsRange ᜂ(object A_0)
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
		return new XlsRange(this, A_0);
	}

	// Token: 0x060039E8 RID: 14824 RVA: 0x0020810C File Offset: 0x0020710C
	public virtual XlsRange ᜀ(object A_0, int A_1, int A_2)
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
		return new XlsRange(this, A_0, A_1, A_2);
	}

	// Token: 0x060039E9 RID: 14825 RVA: 0x00208150 File Offset: 0x00207150
	internal virtual XlsRange ᜀ(object A_0, BiffRecordRaw[] A_1, ref int A_2)
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
		return new XlsRange(this, A_0, A_1, A_2);
	}

	// Token: 0x060039EA RID: 14826 RVA: 0x00208198 File Offset: 0x00207198
	internal virtual XlsRange ᜀ(object A_0, BiffRecordRaw[] A_1, ref int A_2, bool A_3)
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
		return new XlsRange(this, A_0, A_1, ref A_2, A_3);
	}

	// Token: 0x060039EB RID: 14827 RVA: 0x002081E0 File Offset: 0x002071E0
	public virtual XlsRange ᜀ(object A_0, List<BiffRecordRaw> A_1, ref int A_2, bool A_3)
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
		return new XlsRange(this, A_0, A_1, ref A_2, A_3);
	}

	// Token: 0x060039EC RID: 14828 RVA: 0x00208228 File Offset: 0x00207228
	public virtual XlsRange ᜀ(object A_0, int A_1, int A_2, int A_3, int A_4)
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
		return new XlsRange(this, A_0, A_1, A_2, A_3, A_4);
	}

	// Token: 0x060039ED RID: 14829 RVA: 0x00208270 File Offset: 0x00207270
	internal virtual XlsRange ᜀ(object A_0, BiffRecordRaw A_1, bool A_2)
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
		return new XlsRange(this, A_0, A_1, A_2);
	}

	// Token: 0x060039EE RID: 14830 RVA: 0x002082B4 File Offset: 0x002072B4
	public virtual XlsStyle ᜀ(XlsWorkbook A_0, string A_1)
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
		return new XlsStyle(A_0, A_1);
	}

	// Token: 0x060039EF RID: 14831 RVA: 0x002082F8 File Offset: 0x002072F8
	public virtual XlsStyle ᜀ(XlsWorkbook A_0, string A_1, XlsStyle A_2)
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
		return new XlsStyle(A_0, A_1, A_2);
	}

	// Token: 0x060039F0 RID: 14832 RVA: 0x0020833C File Offset: 0x0020733C
	internal virtual XlsStyle ᜀ(XlsWorkbook A_0, sprᬐ A_1)
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
		return new XlsStyle(A_0, A_1);
	}

	// Token: 0x060039F1 RID: 14833 RVA: 0x00208380 File Offset: 0x00207380
	public virtual XlsStyle ᜀ(XlsWorkbook A_0, string A_1, bool A_2)
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
		return new XlsStyle(A_0, A_1, null, A_2);
	}

	// Token: 0x060039F2 RID: 14834 RVA: 0x002083C4 File Offset: 0x002073C4
	public virtual XlsFont ᜀ(object A_0)
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
		return new XlsFont(this, A_0);
	}

	// Token: 0x060039F3 RID: 14835 RVA: 0x00208408 File Offset: 0x00207408
	public virtual XlsFont ᜀ(object A_0, Font A_1)
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
		return new XlsFont(this, A_0, A_1);
	}

	// Token: 0x060039F4 RID: 14836 RVA: 0x0020844C File Offset: 0x0020744C
	public virtual XlsFont ᜀ(IFont A_0)
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
		return new XlsFont(A_0);
	}

	// Token: 0x060039F5 RID: 14837 RVA: 0x00208490 File Offset: 0x00207490
	internal virtual XlsFont ᜀ(object A_0, spr\u2267 A_1)
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
		return new XlsFont(this, A_0, A_1);
	}

	// Token: 0x060039F6 RID: 14838 RVA: 0x002084D4 File Offset: 0x002074D4
	public virtual spr\u214D ᜪ()
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
		return new spr\u2322(new spr\u1C2C());
	}

	// Token: 0x060039F7 RID: 14839 RVA: 0x0020851C File Offset: 0x0020751C
	public virtual spr\u214D ᜀ(IWorksheet A_0)
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
		return new spr\u2322(A_0, new spr\u1C2C(A_0));
	}

	// Token: 0x060039F8 RID: 14840 RVA: 0x00208564 File Offset: 0x00207564
	public virtual XlsChart ᜄ(object A_0)
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
		return new XlsChart(this, A_0);
	}

	// Token: 0x060039F9 RID: 14841 RVA: 0x002085A8 File Offset: 0x002075A8
	public virtual XlsChartSerie ᜃ(object A_0)
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
		return new XlsChartSerie(this, A_0);
	}

	// Token: 0x060039FA RID: 14842 RVA: 0x002085EC File Offset: 0x002075EC
	public XlsRangesCollection ᜈ(object A_0)
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
		return new XlsRangesCollection(this, A_0);
	}

	// Token: 0x060039FB RID: 14843 RVA: 0x00208630 File Offset: 0x00207630
	public virtual XlsHyperLink ᜅ(object A_0)
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
		return new HyperLink((spr\u2158)this, A_0);
	}

	// Token: 0x060039FC RID: 14844 RVA: 0x00208678 File Offset: 0x00207678
	public virtual XlsHyperLink ᜀ(object A_0, IXLSRange A_1)
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
		return new HyperLink((spr\u2158)this, A_0, A_1);
	}

	// Token: 0x060039FD RID: 14845 RVA: 0x002086C0 File Offset: 0x002076C0
	public virtual CommentsRange ᜁ(IXLSRange A_0)
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
		return new CommentsRange(this, A_0);
	}

	// Token: 0x060039FE RID: 14846 RVA: 0x00208704 File Offset: 0x00207704
	public virtual XlsComment ᜆ(object A_0)
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
		return this.ᜀ(A_0, true);
	}

	// Token: 0x060039FF RID: 14847 RVA: 0x00208748 File Offset: 0x00207748
	public virtual XlsComment ᜀ(object A_0, bool A_1)
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
		return new XlsComment(this, A_0, A_1);
	}

	// Token: 0x06003A00 RID: 14848 RVA: 0x0020878C File Offset: 0x0020778C
	internal virtual XlsComment ᜀ(object A_0, sprὙ A_1)
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
		return new XlsComment(this, A_0, A_1);
	}

	// Token: 0x06003A01 RID: 14849 RVA: 0x002087D0 File Offset: 0x002077D0
	internal virtual XlsComment ᜀ(object A_0, sprὙ A_1, ExcelParseOptions A_2)
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
		return new XlsComment(this, A_0, A_1, A_2);
	}

	// Token: 0x06003A02 RID: 14850 RVA: 0x00208814 File Offset: 0x00207814
	public virtual spr\u173D ᜀ(IXLSRange A_0)
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
		return new spr\u173D(A_0);
	}

	// Token: 0x06003A03 RID: 14851 RVA: 0x00208858 File Offset: 0x00207858
	public virtual XlsValidationWrapper ᜀ(XlsRange A_0, XlsValidation A_1)
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
		return new XlsValidationWrapper(A_0, A_1);
	}

	// Token: 0x06003A04 RID: 14852 RVA: 0x0020889C File Offset: 0x0020789C
	public virtual XlsValidation ᜀ(XlsDataValidationCollection A_0)
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
		return new XlsValidation(A_0);
	}

	// Token: 0x06003A05 RID: 14853 RVA: 0x002088E0 File Offset: 0x002078E0
	internal virtual XlsValidation ᜀ(XlsDataValidationCollection A_0, sprᡣ A_1)
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
		return new XlsValidation(A_0, A_1);
	}

	// Token: 0x06003A06 RID: 14854 RVA: 0x00208924 File Offset: 0x00207924
	public virtual ConditionalFormats ᜀ(ICombinedRange A_0)
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
		return new ConditionalFormats(A_0);
	}

	// Token: 0x06003A07 RID: 14855 RVA: 0x00208968 File Offset: 0x00207968
	internal virtual XlsConditionalFormats ᜀ(object A_0, spr\u21C4 A_1, IList A_2)
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
		return new XlsConditionalFormats(this, A_0, A_1, A_2);
	}

	// Token: 0x06003A08 RID: 14856 RVA: 0x002089AC File Offset: 0x002079AC
	internal spr\u25CA ᜇ(object A_0)
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
		return new spr\u25CA(this, A_0);
	}

	// Token: 0x06003A09 RID: 14857 RVA: 0x002089F0 File Offset: 0x002079F0
	public static DataProvider ᜀ(IntPtr A_0)
	{
		DataProvider result;
		for (;;)
		{
			result = null;
			DataProviderType u = spr\u17FF.\u1712;
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
					num = 0;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return result;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						switch (u)
						{
						case DataProviderType.Native:
							result = new sprᰟ(A_0);
							num = 1;
							continue;
						case DataProviderType.Unsafe:
							result = new sprᨕ(A_0);
							num = 4;
							continue;
						case DataProviderType.ByteArray:
							result = new spr\u24E5();
							num = 5;
							continue;
						default:
							num = 2;
							continue;
						}
						break;
					}
					break;
				case 4:
					return result;
				case 5:
					return result;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x06003A0A RID: 14858 RVA: 0x00208AC0 File Offset: 0x00207AC0
	internal static DataProvider ᜀ()
	{
		DataProvider result;
		for (;;)
		{
			if (true)
			{
			}
			result = null;
			DataProviderType u = spr\u17FF.\u1712;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return result;
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
						switch (u)
						{
						case DataProviderType.Native:
							result = new sprᰟ(IntPtr.Zero);
							num = 5;
							continue;
						case DataProviderType.Unsafe:
							result = new sprᨕ(IntPtr.Zero);
							num = 3;
							continue;
						case DataProviderType.ByteArray:
							result = new spr\u24E5();
							num = 0;
							continue;
						default:
							num = 4;
							continue;
						}
						break;
					}
					break;
				case 3:
					return result;
				case 4:
					num = 1;
					continue;
				case 5:
					return result;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x06003A0B RID: 14859 RVA: 0x00208B98 File Offset: 0x00207B98
	internal spr\u2496 ᜁ(Stream A_0)
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
			if (!this.ᜮ)
			{
				return new sprᦿ(A_0);
			}
			if (true)
			{
			}
			break;
		}
		return new spr\u2604(A_0);
	}

	// Token: 0x06003A0C RID: 14860 RVA: 0x00208BEC File Offset: 0x00207BEC
	internal spr\u2496 ᜀ(string A_0, STGM A_1)
	{
		spr\u2496 result;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			for (;;)
			{
				bool a_ = (A_1 & STGM.STGM_CREATE) != STGM.STGM_READ;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						spr\u2604 spr_u = new spr\u2604(A_0, a_);
						spr_u.ᜀ(true);
						result = spr_u;
						num = 2;
						continue;
					}
					case 1:
						return result;
					case 2:
						goto IL_99;
					case 3:
						if (true)
						{
						}
						if (this.ᜮ)
						{
							num = 0;
							continue;
						}
						result = new sprᦿ(A_0, A_1);
						num = 1;
						continue;
					}
					break;
				}
			}
			IL_99:
			break;
		}
		return result;
	}

	// Token: 0x06003A0D RID: 14861 RVA: 0x00208C98 File Offset: 0x00207C98
	internal spr\u2496 ᜄ()
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
			if (!this.ᜮ)
			{
				return new sprᦿ();
			}
			break;
		}
		return new spr\u2604();
	}

	// Token: 0x06003A0E RID: 14862 RVA: 0x00208CEC File Offset: 0x00207CEC
	public static Image ᜀ(Stream A_0)
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
		return Image.FromStream(A_0, true, false);
	}

	// Token: 0x06003A0F RID: 14863 RVA: 0x00208D30 File Offset: 0x00207D30
	internal XlsTextBoxShape ᜀ(spr\u1D9B A_0)
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
		return new XlsTextBoxShape(this, A_0);
	}

	// Token: 0x06003A10 RID: 14864 RVA: 0x00208D74 File Offset: 0x00207D74
	public sprថ ᜉ(object A_0)
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
		return new sprថ(this, A_0);
	}

	// Token: 0x06003A11 RID: 14865 RVA: 0x00208DB8 File Offset: 0x00207DB8
	public XlsComboBoxShape ᜊ(object A_0)
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
		return new XlsComboBoxShape(this, A_0);
	}

	// Token: 0x06003A12 RID: 14866 RVA: 0x00208DFC File Offset: 0x00207DFC
	public RadioButton ᜋ(object A_0)
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
		return new RadioButton(this, A_0);
	}

	// Token: 0x06003A13 RID: 14867 RVA: 0x00208E40 File Offset: 0x00207E40
	public virtual Stream ᜂ(Stream A_0)
	{
		Stream result;
		for (;;)
		{
			IL_44:
			if (true)
			{
			}
			result = null;
			int num = 7;
			for (;;)
			{
				CompressionLevel compressionLevel;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_75;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						return result;
					case 1:
						result = new DeflateStream(A_0, CompressionMode.Compress, true);
						num = 4;
						continue;
					case 2:
						if (this.ᜯ == null)
						{
							num = 5;
							continue;
						}
						num = 6;
						continue;
					case 3:
						compressionLevel = CompressionLevel.Normal;
						goto IL_A6;
					case 4:
						return result;
					case 5:
						num = 3;
						continue;
					case 6:
						goto IL_75;
					case 7:
						if (this.ᜯ == null)
						{
							num = 1;
							continue;
						}
						num = 2;
						continue;
					}
					goto IL_44;
				}
				IL_A6:
				CompressionLevel a_ = compressionLevel;
				result = new spr\u1A9F(a_, A_0);
				num = 0;
				continue;
				IL_75:
				compressionLevel = this.ᜯ.Value;
				goto IL_A6;
			}
		}
		return result;
	}

	// Token: 0x06003A14 RID: 14868 RVA: 0x00208F3C File Offset: 0x00207F3C
	internal CultureInfo \u171F()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_E5:
			num = 3;
			break;
		default:
			if (false)
			{
			}
			goto IL_3E;
		}
		CultureInfo cultureInfo;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_11B;
			case 1:
				goto IL_7B;
			case 2:
				if (!(this.ᜊ() != cultureInfo.NumberFormat.NumberDecimalSeparator))
				{
					num = 1;
					continue;
				}
				goto IL_7D;
			case 3:
				if (this.ᜉ() != cultureInfo.NumberFormat.NumberGroupSeparator)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				goto IL_120;
			}
			goto IL_3E;
		}
		IL_7B:
		goto IL_E5;
		IL_7D:
		cultureInfo.NumberFormat.NumberDecimalSeparator = this.ᜊ();
		cultureInfo.NumberFormat.NumberGroupSeparator = this.ᜉ();
		cultureInfo.NumberFormat.PercentDecimalSeparator = this.ᜊ();
		cultureInfo.NumberFormat.PercentGroupSeparator = this.ᜉ();
		cultureInfo.NumberFormat.CurrencyGroupSeparator = this.ᜉ();
		cultureInfo.NumberFormat.CurrencyDecimalSeparator = this.ᜊ();
		return cultureInfo;
		IL_11B:
		goto IL_7D;
		IL_120:
		return null;
		IL_3E:
		cultureInfo = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);
		num = 2;
		goto IL_28;
	}

	// Token: 0x06003A15 RID: 14869 RVA: 0x0020906C File Offset: 0x0020806C
	public void ᜀ(IWorkbook A_0)
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
		this.\u1715 = A_0;
	}

	// Token: 0x06003A16 RID: 14870 RVA: 0x002090B0 File Offset: 0x002080B0
	public void ᜀ(XlsWorksheetBase A_0)
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
		this.\u1714 = A_0;
	}

	// Token: 0x06003A17 RID: 14871 RVA: 0x002090F4 File Offset: 0x002080F4
	public void ᜂ(IXLSRange A_0)
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
		this.\u1713 = A_0;
	}

	// Token: 0x06003A18 RID: 14872 RVA: 0x00209138 File Offset: 0x00208138
	internal static double ᜁ(double A_0, MeasureUnits A_1)
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
		return A_0 * spr\u17FF.ᜌ[(int)A_1];
	}

	// Token: 0x06003A19 RID: 14873 RVA: 0x0020917C File Offset: 0x0020817C
	internal static double ᜀ(double A_0, MeasureUnits A_1)
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
		return A_0 / spr\u17FF.ᜌ[(int)A_1];
	}

	// Token: 0x06003A1A RID: 14874 RVA: 0x002091C0 File Offset: 0x002081C0
	public static double ᜀ(double A_0, MeasureUnits A_1, MeasureUnits A_2)
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
		return A_0 * spr\u17FF.ᜌ[(int)A_1] / spr\u17FF.ᜌ[(int)A_2];
	}

	// Token: 0x06003A1B RID: 14875 RVA: 0x0020920C File Offset: 0x0020820C
	public double ᜁ(double A_0, MeasureUnits A_1, MeasureUnits A_2)
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
			if (A_1 == A_2)
			{
				return A_0;
			}
			break;
		}
		return A_0 * spr\u17FF.ᜌ[(int)A_1] / spr\u17FF.ᜌ[(int)A_2];
	}

	// Token: 0x06003A1C RID: 14876 RVA: 0x00209260 File Offset: 0x00208260
	public void ᜀ(long A_0, long A_1)
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
					break;
				}
				this.ᜰ(this, new sprᱢ(A_0, A_1));
				num = 2;
				continue;
			case 2:
				return;
			}
			if (true)
			{
			}
			if (this.ᜰ == null)
			{
				break;
			}
			num = 0;
		}
	}

	// Token: 0x06003A1D RID: 14877 RVA: 0x002092E8 File Offset: 0x002082E8
	public SizeF ᜀ(string A_0, XlsFont A_1, SizeF A_2)
	{
		SizeF result;
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
			StringFormat stringFormat = new StringFormat(StringFormatFlags.NoWrap);
			lock (spr\u17FF.ᜎ)
			{
				result = spr\u17FF.ᜎ.MeasureString(A_0, A_1.GenerateNativeFont(), A_2, stringFormat);
			}
			break;
		}
		}
		if (true)
		{
		}
		return result;
	}

	// Token: 0x06003A1E RID: 14878 RVA: 0x0020936C File Offset: 0x0020836C
	internal bool ᜁ(object A_0, PasswordRequiredEventArgs A_1)
	{
		bool result;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_5C:
			this.ᜱ(A_0, A_1);
			result = true;
			num = 2;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			goto IL_42;
		}
		for (;;)
		{
			IL_30:
			switch (num)
			{
			case 0:
				goto IL_5C;
			case 1:
				if (this.ᜱ != null)
				{
					num = 0;
					continue;
				}
				return result;
			case 2:
				return result;
			}
			goto IL_42;
		}
		return result;
		IL_42:
		result = false;
		num = 1;
		goto IL_30;
	}

	// Token: 0x06003A1F RID: 14879 RVA: 0x002093F4 File Offset: 0x002083F4
	internal bool ᜀ(object A_0, PasswordRequiredEventArgs A_1)
	{
		bool result;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_54:
			if (true)
			{
			}
			this.\u1732(A_0, A_1);
			result = true;
			num = 2;
			break;
		default:
			if (false)
			{
			}
			goto IL_3A;
		}
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_54;
			case 1:
				if (this.\u1732 != null)
				{
					num = 0;
					continue;
				}
				return result;
			case 2:
				return result;
			}
			goto IL_3A;
		}
		return result;
		IL_3A:
		result = false;
		num = 1;
		goto IL_28;
	}

	// Token: 0x06003A20 RID: 14880 RVA: 0x0020947C File Offset: 0x0020847C
	public void ᜀ(spr\u21D1 A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_68:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			goto IL_3A;
		}
		spr\u21D1 spr_u21D;
		spr\u21D1 spr_u21D2;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				if (spr_u21D == spr_u21D2)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				goto IL_4B;
			case 1:
				goto IL_49;
			case 2:
				return;
			}
			goto IL_3A;
		}
		IL_49:
		IL_4B:
		spr_u21D2 = spr_u21D;
		spr\u21D1 value = (spr\u21D1)Delegate.Combine(spr_u21D2, A_0);
		spr_u21D = Interlocked.CompareExchange<spr\u21D1>(ref this.ᜰ, value, spr_u21D2);
		goto IL_68;
		IL_3A:
		spr_u21D = this.ᜰ;
		num = 1;
		goto IL_28;
	}

	// Token: 0x06003A21 RID: 14881 RVA: 0x00209510 File Offset: 0x00208510
	public void ᜁ(spr\u21D1 A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_70:
			num = 1;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			goto IL_42;
		}
		spr\u21D1 spr_u21D;
		spr\u21D1 spr_u21D2;
		for (;;)
		{
			IL_30:
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (spr_u21D == spr_u21D2)
				{
					num = 0;
					continue;
				}
				goto IL_53;
			case 2:
				goto IL_51;
			}
			goto IL_42;
		}
		IL_51:
		IL_53:
		spr_u21D2 = spr_u21D;
		spr\u21D1 value = (spr\u21D1)Delegate.Remove(spr_u21D2, A_0);
		spr_u21D = Interlocked.CompareExchange<spr\u21D1>(ref this.ᜰ, value, spr_u21D2);
		goto IL_70;
		IL_42:
		spr_u21D = this.ᜰ;
		num = 2;
		goto IL_30;
	}

	// Token: 0x06003A22 RID: 14882 RVA: 0x002095A4 File Offset: 0x002085A4
	public void ᜁ(PasswordRequiredEventHandler A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_68:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_3A;
		}
		PasswordRequiredEventHandler passwordRequiredEventHandler;
		PasswordRequiredEventHandler passwordRequiredEventHandler2;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_49;
			case 1:
				if (passwordRequiredEventHandler == passwordRequiredEventHandler2)
				{
					num = 2;
					continue;
				}
				goto IL_4B;
			case 2:
				goto IL_7C;
			}
			goto IL_3A;
		}
		IL_49:
		IL_4B:
		passwordRequiredEventHandler2 = passwordRequiredEventHandler;
		PasswordRequiredEventHandler value = (PasswordRequiredEventHandler)Delegate.Combine(passwordRequiredEventHandler2, A_0);
		passwordRequiredEventHandler = Interlocked.CompareExchange<PasswordRequiredEventHandler>(ref this.ᜱ, value, passwordRequiredEventHandler2);
		goto IL_68;
		IL_7C:
		if (true)
		{
		}
		return;
		IL_3A:
		passwordRequiredEventHandler = this.ᜱ;
		num = 0;
		goto IL_28;
	}

	// Token: 0x06003A23 RID: 14883 RVA: 0x00209638 File Offset: 0x00208638
	public void ᜀ(PasswordRequiredEventHandler A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_70:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_3A;
		}
		PasswordRequiredEventHandler passwordRequiredEventHandler;
		PasswordRequiredEventHandler passwordRequiredEventHandler2;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_51;
			case 1:
				if (passwordRequiredEventHandler == passwordRequiredEventHandler2)
				{
					num = 2;
					continue;
				}
				goto IL_53;
			case 2:
				return;
			}
			goto IL_3A;
		}
		IL_51:
		IL_53:
		passwordRequiredEventHandler2 = passwordRequiredEventHandler;
		PasswordRequiredEventHandler value = (PasswordRequiredEventHandler)Delegate.Remove(passwordRequiredEventHandler2, A_0);
		passwordRequiredEventHandler = Interlocked.CompareExchange<PasswordRequiredEventHandler>(ref this.ᜱ, value, passwordRequiredEventHandler2);
		goto IL_70;
		IL_3A:
		if (true)
		{
		}
		passwordRequiredEventHandler = this.ᜱ;
		num = 0;
		goto IL_28;
	}

	// Token: 0x06003A24 RID: 14884 RVA: 0x002096CC File Offset: 0x002086CC
	public void ᜂ(PasswordRequiredEventHandler A_0)
	{
		if (true)
		{
		}
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_70:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			goto IL_42;
		}
		PasswordRequiredEventHandler passwordRequiredEventHandler;
		PasswordRequiredEventHandler passwordRequiredEventHandler2;
		for (;;)
		{
			IL_30:
			switch (num)
			{
			case 0:
				goto IL_51;
			case 1:
				return;
			case 2:
				if (passwordRequiredEventHandler == passwordRequiredEventHandler2)
				{
					num = 1;
					continue;
				}
				goto IL_53;
			}
			goto IL_42;
		}
		IL_51:
		IL_53:
		passwordRequiredEventHandler2 = passwordRequiredEventHandler;
		PasswordRequiredEventHandler value = (PasswordRequiredEventHandler)Delegate.Combine(passwordRequiredEventHandler2, A_0);
		passwordRequiredEventHandler = Interlocked.CompareExchange<PasswordRequiredEventHandler>(ref this.\u1732, value, passwordRequiredEventHandler2);
		goto IL_70;
		IL_42:
		passwordRequiredEventHandler = this.\u1732;
		num = 0;
		goto IL_30;
	}

	// Token: 0x06003A25 RID: 14885 RVA: 0x00209760 File Offset: 0x00208760
	public void ᜃ(PasswordRequiredEventHandler A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_68:
			if (true)
			{
			}
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_3A;
		}
		PasswordRequiredEventHandler passwordRequiredEventHandler;
		PasswordRequiredEventHandler passwordRequiredEventHandler2;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_49;
			case 1:
				if (passwordRequiredEventHandler == passwordRequiredEventHandler2)
				{
					num = 2;
					continue;
				}
				goto IL_4B;
			case 2:
				return;
			}
			goto IL_3A;
		}
		IL_49:
		IL_4B:
		passwordRequiredEventHandler2 = passwordRequiredEventHandler;
		PasswordRequiredEventHandler value = (PasswordRequiredEventHandler)Delegate.Remove(passwordRequiredEventHandler2, A_0);
		passwordRequiredEventHandler = Interlocked.CompareExchange<PasswordRequiredEventHandler>(ref this.\u1732, value, passwordRequiredEventHandler2);
		goto IL_68;
		IL_3A:
		passwordRequiredEventHandler = this.\u1732;
		num = 0;
		goto IL_28;
	}

	// Token: 0x0400192C RID: 6444
	private const double ᜀ = 8.0;

	// Token: 0x0400192D RID: 6445
	private const int ᜁ = 0;

	// Token: 0x0400192E RID: 6446
	private const double ᜂ = 10.0;

	// Token: 0x0400192F RID: 6447
	private const int ᜃ = 4;

	// Token: 0x04001930 RID: 6448
	private const int ᜄ = 3;

	// Token: 0x04001931 RID: 6449
	private const string ᜅ = "Arial";

	// Token: 0x04001932 RID: 6450
	private const string ᜆ = "Microsoft Excel";

	// Token: 0x04001933 RID: 6451
	private const string ᜇ = "Spire.Xls.DebugInfo";

	// Token: 0x04001934 RID: 6452
	private const string ᜈ = "Indicates wether to show library debug messages.";

	// Token: 0x04001935 RID: 6453
	internal const char ᜉ = ',';

	// Token: 0x04001936 RID: 6454
	internal const char ᜊ = ';';

	// Token: 0x04001937 RID: 6455
	private string ᜋ;

	// Token: 0x04001938 RID: 6456
	private static readonly double[] ᜌ;

	// Token: 0x04001939 RID: 6457
	internal static readonly SizeF \u170D;

	// Token: 0x0400193A RID: 6458
	private static readonly Graphics ᜎ;

	// Token: 0x0400193B RID: 6459
	private static readonly BooleanSwitch ᜏ;

	// Token: 0x0400193C RID: 6460
	private static readonly bool ᜐ;

	// Token: 0x0400193D RID: 6461
	internal static readonly Type[] ᜑ;

	// Token: 0x0400193E RID: 6462
	private static DataProviderType \u1712;

	// Token: 0x0400193F RID: 6463
	private IXLSRange \u1713;

	// Token: 0x04001940 RID: 6464
	private XlsWorksheetBase \u1714;

	// Token: 0x04001941 RID: 6465
	private IWorkbook \u1715;

	// Token: 0x04001942 RID: 6466
	private sprᬭ \u1716;

	// Token: 0x04001943 RID: 6467
	private bool \u1717;

	// Token: 0x04001944 RID: 6468
	private bool \u1718;

	// Token: 0x04001945 RID: 6469
	private double \u1719;

	// Token: 0x04001946 RID: 6470
	private int \u171A;

	// Token: 0x04001947 RID: 6471
	private int \u171B;

	// Token: 0x04001948 RID: 6472
	private string \u171C;

	// Token: 0x04001949 RID: 6473
	private string \u171D;

	// Token: 0x0400194A RID: 6474
	private string \u171E;

	// Token: 0x0400194B RID: 6475
	private string \u171F;

	// Token: 0x0400194C RID: 6476
	private bool ᜠ;

	// Token: 0x0400194D RID: 6477
	private SkipExtRecordsType ᜡ;

	// Token: 0x0400194E RID: 6478
	private int ᜢ;

	// Token: 0x0400194F RID: 6479
	private bool ᜣ;

	// Token: 0x04001950 RID: 6480
	private double ᜤ;

	// Token: 0x04001951 RID: 6481
	private bool ᜥ;

	// Token: 0x04001952 RID: 6482
	private bool ᜦ;

	// Token: 0x04001953 RID: 6483
	private char ᜧ;

	// Token: 0x04001954 RID: 6484
	private char ᜨ;

	// Token: 0x04001955 RID: 6485
	private string ᜩ;

	// Token: 0x04001956 RID: 6486
	private bool ᜪ;

	// Token: 0x04001957 RID: 6487
	private int ᜫ;

	// Token: 0x04001958 RID: 6488
	private bool ᜬ;

	// Token: 0x04001959 RID: 6489
	private ExcelVersion ᜭ;

	// Token: 0x0400195A RID: 6490
	private bool ᜮ;

	// Token: 0x0400195B RID: 6491
	private CompressionLevel? ᜯ;

	// Token: 0x0400195C RID: 6492
	private spr\u21D1 ᜰ;

	// Token: 0x0400195D RID: 6493
	private PasswordRequiredEventHandler ᜱ;

	// Token: 0x0400195E RID: 6494
	private PasswordRequiredEventHandler \u1732;
}
