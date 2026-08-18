using System;
using System.Collections;
using System.Drawing;
using System.Reflection;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002A1 RID: 673
[DefaultMember("Item")]
internal class spr\u1CCF : XlsObject, IXLSRange
{
	// Token: 0x06002823 RID: 10275 RVA: 0x0016B418 File Offset: 0x0016A418
	internal spr\u1CCF(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜀ();
	}

	// Token: 0x06002824 RID: 10276 RVA: 0x0016B434 File Offset: 0x0016A434
	internal spr\u1CCF(spr\u1DF5 A_0, object A_1, int A_2, int A_3) : this(A_0, A_1, A_2, A_3, A_2, A_3)
	{
	}

	// Token: 0x06002825 RID: 10277 RVA: 0x0016B450 File Offset: 0x0016A450
	internal spr\u1CCF(spr\u1DF5 A_0, object A_1, int A_2, int A_3, int A_4, int A_5) : this(A_0, A_1)
	{
		this.ᜀ = A_2;
		this.ᜁ = A_3;
		this.ᜂ = A_4;
		this.ᜃ = A_5;
	}

	// Token: 0x06002826 RID: 10278 RVA: 0x0016B484 File Offset: 0x0016A484
	internal spr\u1CCF(spr\u1DF5 A_0, object A_1, string A_2) : this(A_0, A_1, A_2, false)
	{
	}

	// Token: 0x06002827 RID: 10279 RVA: 0x0016B49C File Offset: 0x0016A49C
	public spr\u1CCF(spr\u1DF5 A_0, object A_1, string A_2, bool A_3)
	{
		int a_ = 8;
		this..ctor(A_0, A_1);
		spr\u233D spr_u233D = (spr\u233D)A_1;
		if (spr_u233D.ᝀ())
		{
			throw new NotSupportedException(RecordTableEnumerator.b("椽⼿ぁ⽃㕅⁇⽉⥋㩍灏ㅑ㭓㩕㑗㽙㽛⩝य़ൡ੣䙥୧୩ɫmὯٱ味ᑵᵷ婹᥻፽ﶃꢅ", a_));
		}
		IXLSRange ixlsrange = spr_u233D[0].AllocatedRange[A_2, A_3];
		this.ᜀ = ixlsrange.Row;
		this.ᜁ = ixlsrange.Column;
		this.ᜂ = ixlsrange.LastRow;
		this.ᜃ = ixlsrange.LastColumn;
	}

	// Token: 0x06002828 RID: 10280 RVA: 0x0016B524 File Offset: 0x0016A524
	private void ᜀ()
	{
		int a_ = 10;
		this.ᜄ = (base.FindParent(typeof(spr\u233D)) as spr\u233D);
		if (this.ᜄ == null)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("〿⍁㙃⍅♇㹉", a_), RecordTableEnumerator.b("ဿ⍁㙃⍅♇㹉汋⅍㉏㡑ㅓ㕕ⱗ穙㽛㽝๟ౡୣብ䡧ࡩ५乭ᙯᵱųᡵᱷ呹", a_));
		}
	}

	// Token: 0x06002829 RID: 10281 RVA: 0x0016B5B0 File Offset: 0x0016A5B0
	private IXLSRange ᜀ(int A_0)
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
		IWorksheet worksheet = this.ᜄ[A_0];
		return worksheet.AllocatedRange[this.ᜀ, this.ᜁ, this.ᜂ, this.ᜃ];
	}

	// Token: 0x0600282A RID: 10282 RVA: 0x0016B61C File Offset: 0x0016A61C
	public int ᜯ()
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
		return this.ᜄ.Count;
	}

	// Token: 0x0600282B RID: 10283 RVA: 0x0016B664 File Offset: 0x0016A664
	public IXLSRange ᜂ(int A_0)
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
		return this.ᜀ(A_0);
	}

	// Token: 0x0600282C RID: 10284 RVA: 0x0016B6A8 File Offset: 0x0016A6A8
	public XlsWorkbook ᜢ()
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
		return this.ᜄ.\u1733();
	}

	// Token: 0x0600282D RID: 10285 RVA: 0x0016B6F0 File Offset: 0x0016A6F0
	public string ᝈ()
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
		throw new NotSupportedException();
	}

	// Token: 0x0600282E RID: 10286 RVA: 0x0016B730 File Offset: 0x0016A730
	public void ᜀ(GroupByType A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6A;
				}
				if (false)
				{
				}
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						goto IL_6A;
					}
					(this.ᜀ(num) as XlsRange).CollapseGroup(A_0);
					num++;
					num2 = 1;
					continue;
				case 1:
					goto IL_5E;
				case 2:
					goto IL_5E;
				case 3:
					return;
				}
				break;
				IL_5E:
				num2 = 0;
				continue;
				IL_6A:
				num2 = 3;
			}
		}
	}

	// Token: 0x0600282F RID: 10287 RVA: 0x0016B7D4 File Offset: 0x0016A7D4
	public void ᜁ(GroupByType A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6A;
				}
				if (false)
				{
				}
				switch (num2)
				{
				case 0:
					goto IL_56;
				case 1:
					if (true)
					{
					}
					if (num >= count)
					{
						goto IL_6A;
					}
					(this.ᜀ(num) as XlsRange).ExpandGroup(A_0);
					num++;
					num2 = 0;
					continue;
				case 2:
					return;
				case 3:
					goto IL_56;
				}
				break;
				IL_56:
				num2 = 1;
				continue;
				IL_6A:
				num2 = 2;
			}
		}
	}

	// Token: 0x06002830 RID: 10288 RVA: 0x0016B878 File Offset: 0x0016A878
	public void ᜀ(GroupByType A_0, ExpandCollapseFlags A_1)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6A;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						goto IL_6A;
					}
					(this.ᜀ(num) as XlsRange).ExpandGroup(A_0, A_1);
					num++;
					num2 = 3;
					continue;
				case 1:
					goto IL_5E;
				case 2:
					return;
				case 3:
					goto IL_5E;
				}
				break;
				IL_5E:
				num2 = 0;
				continue;
				IL_6A:
				num2 = 2;
			}
		}
	}

	// Token: 0x06002831 RID: 10289 RVA: 0x0016B920 File Offset: 0x0016A920
	public string \u171A()
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
		return sprṔ.ᜀ(this.ᜀ, this.ᜁ, this.ᜂ, this.ᜃ);
	}

	// Token: 0x06002832 RID: 10290 RVA: 0x0016B978 File Offset: 0x0016A978
	public string ᜮ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002833 RID: 10291 RVA: 0x0016B9B8 File Offset: 0x0016A9B8
	public string \u173F()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002834 RID: 10292 RVA: 0x0016B9F8 File Offset: 0x0016A9F8
	public string ᜦ()
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
		return sprṔ.ᜀ(this.ᜀ, this.ᜁ, this.ᜂ, this.ᜃ, true);
	}

	// Token: 0x06002835 RID: 10293 RVA: 0x0016BA54 File Offset: 0x0016AA54
	public bool \u173B()
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					bool booleanValue;
					int num2;
					int count;
					switch (num)
					{
					case 0:
						return false;
					case 2:
						return false;
					case 3:
						return booleanValue;
					case 4:
						goto IL_99;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
						{
							if (false)
							{
							}
							if (num2 >= count)
							{
								num = 3;
								continue;
							}
							bool booleanValue2 = this.ᜀ(num2).BooleanValue;
							num = 6;
							continue;
						}
						}
						break;
					case 6:
					{
						bool booleanValue2;
						if (booleanValue != booleanValue2)
						{
							num = 0;
							continue;
						}
						num2++;
						if (true)
						{
						}
						num = 4;
						continue;
					}
					case 7:
						goto IL_99;
					}
					if (this.ᜄ.ᝀ())
					{
						num = 2;
						continue;
					}
					booleanValue = this.ᜀ(0).BooleanValue;
					num2 = 1;
					count = this.ᜄ.Count;
					num = 7;
					continue;
					IL_99:
					num = 5;
				}
				break;
			}
			}
		}
		return false;
	}

	// Token: 0x06002836 RID: 10294 RVA: 0x0016BB6C File Offset: 0x0016AB6C
	public void ᜆ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			if (true)
			{
			}
			int num2 = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6A;
				}
				if (false)
				{
				}
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_5E;
				case 2:
					if (num >= count)
					{
						goto IL_6A;
					}
					this.ᜀ(num).BooleanValue = A_0;
					num++;
					num2 = 3;
					continue;
				case 3:
					goto IL_5E;
				}
				break;
				IL_5E:
				num2 = 2;
				continue;
				IL_6A:
				num2 = 0;
			}
		}
	}

	// Token: 0x06002837 RID: 10295 RVA: 0x0016BC0C File Offset: 0x0016AC0C
	public IBorders ᜤ()
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
		return this.\u1757().Borders;
	}

	// Token: 0x06002838 RID: 10296 RVA: 0x0016BC54 File Offset: 0x0016AC54
	public CellRange[] \u1719()
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

	// Token: 0x06002839 RID: 10297 RVA: 0x0016BC94 File Offset: 0x0016AC94
	public int ᝄ()
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
		return this.ᜁ;
	}

	// Token: 0x0600283A RID: 10298 RVA: 0x0016BCD8 File Offset: 0x0016ACD8
	public int ᝐ()
	{
		int columnGroupLevel;
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					int num2;
					int count;
					switch (num)
					{
					case 0:
						return int.MinValue;
					case 1:
					{
						int columnGroupLevel2;
						if (columnGroupLevel != columnGroupLevel2)
						{
							num = 5;
							continue;
						}
						num2++;
						num = 2;
						continue;
					}
					case 2:
						goto IL_95;
					case 3:
						goto IL_CD;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
						{
							if (false)
							{
							}
							if (num2 >= count)
							{
								num = 3;
								continue;
							}
							int columnGroupLevel2 = this.ᜀ(num2).ColumnGroupLevel;
							num = 1;
							continue;
						}
						}
						break;
					case 5:
						return int.MinValue;
					case 7:
						goto IL_95;
					}
					if (this.ᜄ.ᝀ())
					{
						num = 0;
						continue;
					}
					columnGroupLevel = this.ᜀ(0).ColumnGroupLevel;
					num2 = 1;
					count = this.ᜄ.Count;
					num = 7;
					continue;
					IL_95:
					num = 4;
				}
				break;
			}
			}
		}
		return int.MinValue;
		IL_CD:
		if (true)
		{
		}
		return columnGroupLevel;
	}

	// Token: 0x0600283B RID: 10299 RVA: 0x0016BDF8 File Offset: 0x0016ADF8
	public double \u175E()
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					double columnWidth;
					int num2;
					int count;
					switch (num)
					{
					case 0:
						goto IL_61;
					case 1:
						return columnWidth;
					case 2:
					{
						double columnWidth2;
						if (columnWidth != columnWidth2)
						{
							num = 5;
							continue;
						}
						num2++;
						num = 4;
						continue;
					}
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
						{
							if (false)
							{
							}
							if (num2 >= count)
							{
								num = 1;
								continue;
							}
							double columnWidth2 = this.ᜀ(num2).ColumnWidth;
							num = 2;
							continue;
						}
						}
						break;
					case 4:
						goto IL_A1;
					case 5:
						goto IL_95;
					case 7:
						goto IL_A1;
					}
					if (true)
					{
					}
					if (this.ᜄ.ᝀ())
					{
						num = 0;
						continue;
					}
					columnWidth = this.ᜀ(0).ColumnWidth;
					num2 = 1;
					count = this.ᜄ.Count;
					num = 7;
					continue;
					IL_A1:
					num = 3;
				}
				break;
			}
			}
		}
		IL_61:
		return double.MinValue;
		IL_95:
		return double.MinValue;
	}

	// Token: 0x0600283C RID: 10300 RVA: 0x0016BF20 File Offset: 0x0016AF20
	public void ᜁ(double A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6A;
				}
				if (false)
				{
				}
				switch (num2)
				{
				case 0:
					goto IL_56;
				case 1:
					return;
				case 2:
					if (true)
					{
					}
					if (num >= count)
					{
						goto IL_6A;
					}
					this.ᜀ(num).ColumnWidth = A_0;
					num++;
					num2 = 0;
					continue;
				case 3:
					goto IL_56;
				}
				break;
				IL_56:
				num2 = 2;
				continue;
				IL_6A:
				num2 = 1;
			}
		}
	}

	// Token: 0x0600283D RID: 10301 RVA: 0x0016BFC0 File Offset: 0x0016AFC0
	int IXLSRange.\u1733()
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

	// Token: 0x0600283E RID: 10302 RVA: 0x0016C000 File Offset: 0x0016B000
	public DateTime ᜆ()
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 6;
				for (;;)
				{
					DateTime dateTimeValue;
					int num2;
					int count;
					switch (num)
					{
					case 0:
						goto IL_A2;
					case 1:
						return dateTimeValue;
					case 2:
						goto IL_9A;
					case 3:
						goto IL_A2;
					case 4:
						goto IL_61;
					case 5:
					{
						DateTime dateTimeValue2;
						if (dateTimeValue != dateTimeValue2)
						{
							num = 2;
							continue;
						}
						num2++;
						num = 0;
						continue;
					}
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
						{
							if (false)
							{
							}
							if (num2 >= count)
							{
								num = 1;
								continue;
							}
							DateTime dateTimeValue2 = this.ᜀ(num2).DateTimeValue;
							num = 5;
							continue;
						}
						}
						break;
					}
					if (this.ᜄ.ᝀ())
					{
						num = 4;
						continue;
					}
					dateTimeValue = this.ᜀ(0).DateTimeValue;
					num2 = 1;
					count = this.ᜄ.Count;
					num = 3;
					continue;
					IL_A2:
					num = 7;
				}
				break;
			}
			}
		}
		IL_61:
		return DateTime.MinValue;
		IL_9A:
		return DateTime.MinValue;
	}

	// Token: 0x0600283F RID: 10303 RVA: 0x0016C124 File Offset: 0x0016B124
	public void ᜂ(DateTime A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_62;
				}
				if (false)
				{
				}
				switch (num2)
				{
				case 0:
					return;
				case 1:
					if (num >= count)
					{
						goto IL_62;
					}
					if (true)
					{
					}
					this.ᜀ(num).DateTimeValue = A_0;
					num++;
					num2 = 2;
					continue;
				case 2:
					goto IL_56;
				case 3:
					goto IL_56;
				}
				break;
				IL_56:
				num2 = 1;
				continue;
				IL_62:
				num2 = 0;
			}
		}
	}

	// Token: 0x06002840 RID: 10304 RVA: 0x0016C1C4 File Offset: 0x0016B1C4
	public string ᝌ()
	{
		string numberText2;
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					int num2;
					int count;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
						{
							if (false)
							{
							}
							if (num2 >= count)
							{
								num = 5;
								continue;
							}
							string numberText = this.ᜀ(num2).NumberText;
							num = 4;
							continue;
						}
						}
						break;
					case 1:
						goto IL_96;
					case 3:
						goto IL_96;
					case 4:
					{
						string numberText;
						if (numberText2 != numberText)
						{
							num = 7;
							continue;
						}
						num2++;
						num = 1;
						continue;
					}
					case 5:
						goto IL_CE;
					case 6:
						goto IL_59;
					case 7:
						goto IL_92;
					}
					if (this.ᜄ.ᝀ())
					{
						num = 6;
						continue;
					}
					numberText2 = this.ᜀ(0).NumberText;
					num2 = 1;
					count = this.ᜄ.Count;
					num = 3;
					continue;
					IL_96:
					num = 0;
				}
				break;
			}
			}
		}
		IL_59:
		return null;
		IL_92:
		return null;
		IL_CE:
		if (true)
		{
		}
		return numberText2;
	}

	// Token: 0x06002841 RID: 10305 RVA: 0x0016C2E0 File Offset: 0x0016B2E0
	public IXLSRange \u175A()
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
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.ᜆ = new spr\u1CCF(base.ReservedHandle, this, this.ᜂ, this.ᜃ);
					num = 2;
					continue;
				case 2:
					goto IL_70;
				}
				if (this.ᜆ != null)
				{
					goto IL_84;
				}
				num = 1;
			}
			IL_70:
			if (true)
			{
			}
			break;
		}
		}
		IL_84:
		return this.ᜆ;
	}

	// Token: 0x06002842 RID: 10306 RVA: 0x0016C378 File Offset: 0x0016B378
	public IXLSRange \u171F()
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
		throw new NotImplementedException();
	}

	// Token: 0x06002843 RID: 10307 RVA: 0x0016C3B8 File Offset: 0x0016B3B8
	public IXLSRange \u175B()
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
		throw new NotImplementedException();
	}

	// Token: 0x06002844 RID: 10308 RVA: 0x0016C3F8 File Offset: 0x0016B3F8
	public string \u175D()
	{
		string errorValue2;
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					int num2;
					int count;
					switch (num)
					{
					case 0:
						goto IL_CE;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
						{
							if (false)
							{
							}
							if (num2 >= count)
							{
								num = 0;
								continue;
							}
							string errorValue = this.ᜀ(num2).ErrorValue;
							num = 6;
							continue;
						}
						}
						break;
					case 3:
						goto IL_96;
					case 4:
						goto IL_92;
					case 5:
						goto IL_96;
					case 6:
					{
						string errorValue;
						if (errorValue2 != errorValue)
						{
							num = 4;
							continue;
						}
						num2++;
						num = 5;
						continue;
					}
					case 7:
						goto IL_59;
					}
					if (this.ᜄ.ᝀ())
					{
						num = 7;
						continue;
					}
					errorValue2 = this.ᜀ(0).ErrorValue;
					num2 = 1;
					count = this.ᜄ.Count;
					num = 3;
					continue;
					IL_96:
					num = 2;
				}
				break;
			}
			}
		}
		IL_59:
		return null;
		IL_92:
		return null;
		IL_CE:
		if (true)
		{
		}
		return errorValue2;
	}

	// Token: 0x06002845 RID: 10309 RVA: 0x0016C514 File Offset: 0x0016B514
	public void ᜂ(string A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6A;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						goto IL_6A;
					}
					this.ᜀ(num).ErrorValue = A_0;
					num++;
					num2 = 3;
					continue;
				case 1:
					return;
				case 2:
					goto IL_5E;
				case 3:
					goto IL_5E;
				}
				break;
				IL_5E:
				num2 = 0;
				continue;
				IL_6A:
				num2 = 1;
			}
		}
	}

	// Token: 0x06002846 RID: 10310 RVA: 0x0016C5B4 File Offset: 0x0016B5B4
	public string \u1735()
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 7;
				for (;;)
				{
					string formula;
					int num2;
					int count;
					switch (num)
					{
					case 0:
						goto IL_9A;
					case 1:
						return formula;
					case 2:
						goto IL_61;
					case 3:
					{
						string formula2;
						if (formula != formula2)
						{
							num = 0;
							continue;
						}
						num2++;
						num = 6;
						continue;
					}
					case 4:
						goto IL_9E;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
						{
							if (false)
							{
							}
							if (num2 >= count)
							{
								num = 1;
								continue;
							}
							string formula2 = this.ᜀ(num2).Formula;
							num = 3;
							continue;
						}
						}
						break;
					case 6:
						goto IL_9E;
					}
					if (this.ᜄ.ᝀ())
					{
						num = 2;
						continue;
					}
					formula = this.ᜀ(0).Formula;
					num2 = 1;
					count = this.ᜄ.Count;
					num = 4;
					continue;
					IL_9E:
					num = 5;
				}
				break;
			}
			}
		}
		IL_61:
		return null;
		IL_9A:
		return null;
	}

	// Token: 0x06002847 RID: 10311 RVA: 0x0016C6D0 File Offset: 0x0016B6D0
	public void ᜉ(string A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_62;
				}
				if (false)
				{
				}
				switch (num2)
				{
				case 0:
					goto IL_56;
				case 1:
					return;
				case 2:
					goto IL_56;
				case 3:
					if (num >= count)
					{
						goto IL_62;
					}
					this.ᜀ(num).Formula = A_0;
					num++;
					num2 = 0;
					continue;
				}
				break;
				IL_56:
				num2 = 3;
				continue;
				IL_62:
				if (true)
				{
				}
				num2 = 1;
			}
		}
	}

	// Token: 0x06002848 RID: 10312 RVA: 0x0016C770 File Offset: 0x0016B770
	public string ᜱ()
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					int num2;
					int count;
					string formulaR1C2;
					switch (num)
					{
					case 0:
						goto IL_9D;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
						{
							if (false)
							{
							}
							if (num2 >= count)
							{
								num = 3;
								continue;
							}
							string formulaR1C = this.ᜀ(num2).FormulaR1C1;
							num = 2;
							continue;
						}
						}
						break;
					case 2:
					{
						string formulaR1C;
						if (formulaR1C2 != formulaR1C)
						{
							num = 0;
							continue;
						}
						if (true)
						{
						}
						num2++;
						num = 6;
						continue;
					}
					case 3:
						return formulaR1C2;
					case 5:
						goto IL_A1;
					case 6:
						goto IL_A1;
					case 7:
						goto IL_59;
					}
					if (this.ᜄ.ᝀ())
					{
						num = 7;
						continue;
					}
					formulaR1C2 = this.ᜀ(0).FormulaR1C1;
					num2 = 1;
					count = this.ᜄ.Count;
					num = 5;
					continue;
					IL_A1:
					num = 1;
				}
				break;
			}
			}
		}
		IL_59:
		return null;
		IL_9D:
		return null;
	}

	// Token: 0x06002849 RID: 10313 RVA: 0x0016C890 File Offset: 0x0016B890
	public void ᜃ(string A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6A;
				}
				if (false)
				{
				}
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						goto IL_6A;
					}
					this.ᜀ(num).FormulaR1C1 = A_0;
					num++;
					num2 = 1;
					continue;
				case 1:
					goto IL_5E;
				case 2:
					return;
				case 3:
					if (true)
					{
					}
					goto IL_5E;
				}
				break;
				IL_5E:
				num2 = 0;
				continue;
				IL_6A:
				num2 = 2;
			}
		}
	}

	// Token: 0x0600284A RID: 10314 RVA: 0x0016C930 File Offset: 0x0016B930
	public string \u1738()
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				string formulaArray;
				int num2;
				int count;
				switch (num)
				{
				case 0:
				{
					if (true)
					{
					}
					string formulaArray2;
					if (formulaArray != formulaArray2)
					{
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DE;
					default:
						if (false)
						{
						}
						num2++;
						num = 6;
						continue;
					}
					break;
				}
				case 1:
					goto IL_C0;
				case 2:
					goto IL_BC;
				case 4:
					return formulaArray;
				case 5:
					goto IL_59;
				case 6:
					goto IL_C0;
				case 7:
				{
					if (num2 >= count)
					{
						num = 4;
						continue;
					}
					string formulaArray2 = this.ᜀ(num2).FormulaArray;
					num = 0;
					continue;
				}
				}
				if (this.ᜄ.ᝀ())
				{
					num = 5;
					continue;
				}
				goto IL_DE;
				IL_C0:
				num = 7;
				continue;
				IL_DE:
				formulaArray = this.ᜀ(0).FormulaArray;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 1;
			}
			IL_59:
			return null;
			IL_BC:
			return null;
		}
		}
	}

	// Token: 0x0600284B RID: 10315 RVA: 0x0016CA54 File Offset: 0x0016BA54
	public void ᜀ(string A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_66:
				int num;
				int count;
				if (num >= count)
				{
					num2 = 2;
				}
				else
				{
					this.ᜀ(num).FormulaArray = A_0;
					num++;
					num2 = 3;
				}
				break;
			}
			default:
			{
				if (false)
				{
				}
				int num = 0;
				int count = this.ᜄ.Count;
				num2 = 1;
				break;
			}
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_66;
				case 1:
					goto IL_54;
				case 2:
					return;
				case 3:
					goto IL_54;
				}
				break;
				IL_54:
				num2 = 0;
			}
		}
	}

	// Token: 0x0600284C RID: 10316 RVA: 0x0016CAF4 File Offset: 0x0016BAF4
	public string ᝉ()
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				int num2;
				int count;
				string formulaArrayR1C2;
				switch (num)
				{
				case 1:
					goto IL_C0;
				case 2:
					goto IL_59;
				case 3:
					goto IL_C0;
				case 4:
				{
					if (num2 >= count)
					{
						num = 7;
						continue;
					}
					if (true)
					{
					}
					string formulaArrayR1C = this.ᜀ(num2).FormulaArrayR1C1;
					num = 5;
					continue;
				}
				case 5:
				{
					string formulaArrayR1C;
					if (formulaArrayR1C2 != formulaArrayR1C)
					{
						num = 6;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DE;
					default:
						if (false)
						{
						}
						num2++;
						num = 3;
						continue;
					}
					break;
				}
				case 6:
					goto IL_BC;
				case 7:
					return formulaArrayR1C2;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 2;
					continue;
				}
				goto IL_DE;
				IL_C0:
				num = 4;
				continue;
				IL_DE:
				formulaArrayR1C2 = this.ᜀ(0).FormulaArrayR1C1;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 1;
			}
			IL_59:
			return null;
			IL_BC:
			return null;
		}
		}
	}

	// Token: 0x0600284D RID: 10317 RVA: 0x0016CC18 File Offset: 0x0016BC18
	public void ᜁ(string A_0)
	{
		if (true)
		{
		}
		for (;;)
		{
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_66:
				int num;
				int count;
				if (num >= count)
				{
					num2 = 1;
				}
				else
				{
					this.ᜀ(num).FormulaArrayR1C1 = A_0;
					num++;
					num2 = 3;
				}
				break;
			}
			default:
			{
				if (false)
				{
				}
				int num = 0;
				int count = this.ᜄ.Count;
				num2 = 0;
				break;
			}
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_54;
				case 1:
					return;
				case 2:
					goto IL_66;
				case 3:
					goto IL_54;
				}
				break;
				IL_54:
				num2 = 2;
			}
		}
	}

	// Token: 0x0600284E RID: 10318 RVA: 0x0016CCB8 File Offset: 0x0016BCB8
	public bool \u1754()
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				int num2;
				int count;
				bool isFormulaHidden2;
				switch (num)
				{
				case 0:
				{
					if (num2 >= count)
					{
						num = 7;
						continue;
					}
					bool isFormulaHidden = this.ᜀ(num2).IsFormulaHidden;
					num = 6;
					continue;
				}
				case 1:
					goto IL_BB;
				case 2:
					return false;
				case 4:
					goto IL_BB;
				case 5:
					return false;
				case 6:
				{
					bool isFormulaHidden;
					if (isFormulaHidden2 != isFormulaHidden)
					{
						num = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D9;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num2++;
						num = 1;
						continue;
					}
					break;
				}
				case 7:
					return isFormulaHidden2;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 2;
					continue;
				}
				goto IL_D9;
				IL_BB:
				num = 0;
				continue;
				IL_D9:
				isFormulaHidden2 = this.ᜀ(0).IsFormulaHidden;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 4;
			}
			return false;
		}
		}
	}

	// Token: 0x0600284F RID: 10319 RVA: 0x0016CDD4 File Offset: 0x0016BDD4
	public void ᜅ(bool A_0)
	{
		for (;;)
		{
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_54:
				int num;
				int count;
				if (num >= count)
				{
					num2 = 0;
				}
				else
				{
					this.ᜀ(num).IsFormulaHidden = A_0;
					num++;
					num2 = 2;
				}
				break;
			}
			default:
			{
				if (false)
				{
				}
				int num = 0;
				int count = this.ᜄ.Count;
				num2 = 1;
				break;
			}
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_4C;
				case 2:
					if (true)
					{
					}
					goto IL_4C;
				case 3:
					goto IL_54;
				}
				break;
				IL_4C:
				num2 = 3;
			}
		}
	}

	// Token: 0x06002850 RID: 10320 RVA: 0x0016CE74 File Offset: 0x0016BE74
	public DateTime \u1753()
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				DateTime formulaDateTime;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					return formulaDateTime;
				case 1:
					goto IL_61;
				case 3:
					goto IL_C4;
				case 4:
					goto IL_BC;
				case 5:
					goto IL_C4;
				case 6:
				{
					DateTime formulaDateTime2;
					if (formulaDateTime != formulaDateTime2)
					{
						num = 4;
						continue;
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
						num2++;
						num = 3;
						continue;
					}
					break;
				}
				case 7:
				{
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					DateTime formulaDateTime2 = this.ᜀ(num2).FormulaDateTime;
					num = 6;
					continue;
				}
				}
				if (true)
				{
				}
				if (this.ᜄ.ᝀ())
				{
					num = 1;
					continue;
				}
				goto IL_E2;
				IL_C4:
				num = 7;
				continue;
				IL_E2:
				formulaDateTime = this.ᜀ(0).FormulaDateTime;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 5;
			}
			IL_61:
			return DateTime.MinValue;
			IL_BC:
			return DateTime.MinValue;
		}
		}
	}

	// Token: 0x06002851 RID: 10321 RVA: 0x0016CFA0 File Offset: 0x0016BFA0
	public void ᜃ(DateTime A_0)
	{
		for (;;)
		{
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_66:
				int num;
				int count;
				if (num >= count)
				{
					num2 = 1;
				}
				else
				{
					this.ᜀ(num).FormulaDateTime = A_0;
					num++;
					num2 = 0;
				}
				break;
			}
			default:
			{
				if (false)
				{
				}
				int num = 0;
				int count = this.ᜄ.Count;
				num2 = 3;
				break;
			}
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_4C;
				case 1:
					return;
				case 2:
					goto IL_66;
				case 3:
					goto IL_4C;
				}
				break;
				IL_4C:
				if (true)
				{
				}
				num2 = 2;
			}
		}
	}

	// Token: 0x06002852 RID: 10322 RVA: 0x0016D040 File Offset: 0x0016C040
	public bool \u1716()
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				int num2;
				int count;
				bool flag;
				switch (num)
				{
				case 0:
				{
					if (num2 >= count)
					{
						num = 4;
						continue;
					}
					bool hasDataValidation = this.ᜀ(num2).HasDataValidation;
					num = 2;
					continue;
				}
				case 2:
				{
					bool hasDataValidation;
					if (flag != hasDataValidation)
					{
						num = 8;
						continue;
					}
					num2++;
					num = 5;
					continue;
				}
				case 3:
					return false;
				case 4:
					return flag;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10E;
					default:
						if (false)
						{
						}
						goto IL_C9;
					}
					break;
				case 6:
					goto IL_10E;
				case 7:
					return flag;
				case 8:
					if (true)
					{
					}
					flag = false;
					num = 7;
					continue;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 3;
					continue;
				}
				flag = this.ᜀ(0).HasDataValidation;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 6;
				continue;
				IL_C9:
				num = 0;
				continue;
				IL_10E:
				goto IL_C9;
			}
			return false;
		}
		}
	}

	// Token: 0x06002853 RID: 10323 RVA: 0x0016D170 File Offset: 0x0016C170
	public bool \u1736()
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				bool hasBoolean;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					return hasBoolean;
				case 1:
					goto IL_BB;
				case 2:
					return false;
				case 3:
					goto IL_BB;
				case 5:
					return false;
				case 6:
				{
					bool hasBoolean2;
					if (hasBoolean != hasBoolean2)
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
						goto IL_D9;
					default:
						if (false)
						{
						}
						num2++;
						num = 3;
						continue;
					}
					break;
				}
				case 7:
				{
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					bool hasBoolean2 = this.ᜀ(num2).HasBoolean;
					num = 6;
					continue;
				}
				}
				if (this.ᜄ.ᝀ())
				{
					num = 2;
					continue;
				}
				goto IL_D9;
				IL_BB:
				num = 7;
				continue;
				IL_D9:
				hasBoolean = this.ᜀ(0).HasBoolean;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 1;
			}
			return false;
		}
		}
	}

	// Token: 0x06002854 RID: 10324 RVA: 0x0016D28C File Offset: 0x0016C28C
	public bool \u1717()
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				bool hasDateTime;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					return hasDateTime;
				case 1:
					return false;
				case 2:
				{
					bool hasDateTime2;
					if (hasDateTime != hasDateTime2)
					{
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D9;
					default:
						if (false)
						{
						}
						num2++;
						num = 4;
						continue;
					}
					break;
				}
				case 4:
					goto IL_BB;
				case 5:
					return false;
				case 6:
				{
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					if (true)
					{
					}
					bool hasDateTime2 = this.ᜀ(num2).HasDateTime;
					num = 2;
					continue;
				}
				case 7:
					goto IL_BB;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 5;
					continue;
				}
				goto IL_D9;
				IL_BB:
				num = 6;
				continue;
				IL_D9:
				hasDateTime = this.ᜀ(0).HasDateTime;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 7;
			}
			return false;
		}
		}
	}

	// Token: 0x06002855 RID: 10325 RVA: 0x0016D3A8 File Offset: 0x0016C3A8
	public bool ᜥ()
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				bool hasFormulaBoolValue;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_BB;
				case 1:
				{
					bool hasFormulaBoolValue2;
					if (hasFormulaBoolValue != hasFormulaBoolValue2)
					{
						num = 7;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D9;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num2++;
						num = 0;
						continue;
					}
					break;
				}
				case 2:
					goto IL_BB;
				case 3:
				{
					if (num2 >= count)
					{
						num = 5;
						continue;
					}
					bool hasFormulaBoolValue2 = this.ᜀ(num2).HasFormulaBoolValue;
					num = 1;
					continue;
				}
				case 5:
					return hasFormulaBoolValue;
				case 6:
					return false;
				case 7:
					return false;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 6;
					continue;
				}
				goto IL_D9;
				IL_BB:
				num = 3;
				continue;
				IL_D9:
				hasFormulaBoolValue = this.ᜀ(0).HasFormulaBoolValue;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 2;
			}
			return false;
		}
		}
	}

	// Token: 0x06002856 RID: 10326 RVA: 0x0016D4C4 File Offset: 0x0016C4C4
	public bool ᜨ()
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				bool hasFormulaErrorValue;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					return hasFormulaErrorValue;
				case 1:
					goto IL_BB;
				case 3:
					return false;
				case 4:
				{
					bool hasFormulaErrorValue2;
					if (hasFormulaErrorValue != hasFormulaErrorValue2)
					{
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D9;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num2++;
						num = 6;
						continue;
					}
					break;
				}
				case 5:
					return false;
				case 6:
					goto IL_BB;
				case 7:
				{
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					bool hasFormulaErrorValue2 = this.ᜀ(num2).HasFormulaErrorValue;
					num = 4;
					continue;
				}
				}
				if (this.ᜄ.ᝀ())
				{
					num = 5;
					continue;
				}
				goto IL_D9;
				IL_BB:
				num = 7;
				continue;
				IL_D9:
				hasFormulaErrorValue = this.ᜀ(0).HasFormulaErrorValue;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 1;
			}
			return false;
		}
		}
	}

	// Token: 0x06002857 RID: 10327 RVA: 0x0016D5E0 File Offset: 0x0016C5E0
	public bool ᜏ()
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				bool flag;
				int num2;
				int count;
				switch (num)
				{
				case 1:
					goto IL_10E;
				case 2:
				{
					bool hasFormulaDateTime;
					if (flag != hasFormulaDateTime)
					{
						num = 5;
						continue;
					}
					num2++;
					num = 4;
					continue;
				}
				case 3:
					return flag;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10E;
					default:
						if (false)
						{
						}
						goto IL_C9;
					}
					break;
				case 5:
					flag = false;
					num = 3;
					continue;
				case 6:
				{
					if (num2 >= count)
					{
						num = 8;
						continue;
					}
					bool hasFormulaDateTime = this.ᜀ(num2).HasFormulaDateTime;
					num = 2;
					continue;
				}
				case 7:
					return false;
				case 8:
					return flag;
				}
				if (true)
				{
				}
				if (this.ᜄ.ᝀ())
				{
					num = 7;
					continue;
				}
				flag = this.ᜀ(0).HasFormulaDateTime;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 1;
				continue;
				IL_C9:
				num = 6;
				continue;
				IL_10E:
				goto IL_C9;
			}
			return false;
		}
		}
	}

	// Token: 0x06002858 RID: 10328 RVA: 0x0016D710 File Offset: 0x0016C710
	public bool \u171B()
	{
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				bool flag;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_10E;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10E;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_C9;
					}
					break;
				case 2:
					return flag;
				case 3:
					return flag;
				case 4:
					flag = false;
					num = 3;
					continue;
				case 5:
				{
					bool hasFormulaNumberValue;
					if (flag != hasFormulaNumberValue)
					{
						num = 4;
						continue;
					}
					num2++;
					num = 1;
					continue;
				}
				case 6:
				{
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					bool hasFormulaNumberValue = this.ᜀ(num2).HasFormulaNumberValue;
					num = 5;
					continue;
				}
				case 7:
					return false;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 7;
					continue;
				}
				flag = this.ᜀ(0).HasFormulaNumberValue;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 0;
				continue;
				IL_C9:
				num = 6;
				continue;
				IL_10E:
				goto IL_C9;
			}
			return false;
		}
		}
	}

	// Token: 0x06002859 RID: 10329 RVA: 0x0016D840 File Offset: 0x0016C840
	public bool ᝅ()
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				bool flag;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_10E;
				case 1:
					return flag;
				case 2:
					return flag;
				case 3:
				{
					bool hasFormulaStringValue;
					if (flag != hasFormulaStringValue)
					{
						num = 8;
						continue;
					}
					num2++;
					num = 5;
					continue;
				}
				case 4:
					return false;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10E;
					default:
						if (false)
						{
						}
						goto IL_C1;
					}
					break;
				case 7:
				{
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					bool hasFormulaStringValue = this.ᜀ(num2).HasFormulaStringValue;
					num = 3;
					continue;
				}
				case 8:
					flag = false;
					num = 1;
					continue;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 4;
					continue;
				}
				if (true)
				{
				}
				flag = this.ᜀ(0).HasFormulaStringValue;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 0;
				continue;
				IL_C1:
				num = 7;
				continue;
				IL_10E:
				goto IL_C1;
			}
			return false;
		}
		}
	}

	// Token: 0x0600285A RID: 10330 RVA: 0x0016D970 File Offset: 0x0016C970
	public bool ᝁ()
	{
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				bool hasFormula;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_B3;
				case 1:
				{
					bool hasFormula2;
					if (hasFormula != hasFormula2)
					{
						num = 7;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D1;
					default:
						if (false)
						{
						}
						num2++;
						num = 2;
						continue;
					}
					break;
				}
				case 2:
					goto IL_B3;
				case 3:
					return false;
				case 4:
					return hasFormula;
				case 6:
				{
					if (num2 >= count)
					{
						num = 4;
						continue;
					}
					bool hasFormula2 = this.ᜀ(num2).HasFormula;
					num = 1;
					continue;
				}
				case 7:
					return false;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 3;
					continue;
				}
				goto IL_D1;
				IL_B3:
				num = 6;
				continue;
				IL_D1:
				hasFormula = this.ᜀ(0).HasFormula;
				num2 = 1;
				count = this.ᜄ.Count;
				if (true)
				{
				}
				num = 0;
			}
			return false;
		}
		}
	}

	// Token: 0x0600285B RID: 10331 RVA: 0x0016DA8C File Offset: 0x0016CA8C
	public bool ᜃ()
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				bool hasFormulaArray;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					return hasFormulaArray;
				case 1:
				{
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					if (true)
					{
					}
					bool hasFormulaArray2 = this.ᜀ(num2).HasFormulaArray;
					num = 6;
					continue;
				}
				case 2:
					return false;
				case 4:
					goto IL_C5;
				case 5:
					return false;
				case 6:
				{
					bool hasFormulaArray2;
					if (hasFormulaArray != hasFormulaArray2)
					{
						num = 5;
						continue;
					}
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FE;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				}
				case 7:
					goto IL_C5;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 2;
					continue;
				}
				hasFormulaArray = this.ᜀ(0).HasFormulaArray;
				num2 = 1;
				count = this.ᜄ.Count;
				goto IL_FE;
				IL_C5:
				num = 1;
				continue;
				IL_FE:
				num = 4;
			}
			return false;
		}
		}
	}

	// Token: 0x0600285C RID: 10332 RVA: 0x0016DBA8 File Offset: 0x0016CBA8
	public bool \u1756()
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				int num2;
				int count;
				bool hasNumber2;
				switch (num)
				{
				case 0:
					return false;
				case 1:
				{
					if (num2 >= count)
					{
						num = 4;
						continue;
					}
					bool hasNumber = this.ᜀ(num2).HasNumber;
					num = 3;
					continue;
				}
				case 2:
					goto IL_C5;
				case 3:
				{
					bool hasNumber;
					if (hasNumber2 != hasNumber)
					{
						num = 6;
						continue;
					}
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FE;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				}
				case 4:
					return hasNumber2;
				case 5:
					goto IL_C5;
				case 6:
					return false;
				}
				if (true)
				{
				}
				if (this.ᜄ.ᝀ())
				{
					num = 0;
					continue;
				}
				hasNumber2 = this.ᜀ(0).HasNumber;
				num2 = 1;
				count = this.ᜄ.Count;
				goto IL_FE;
				IL_C5:
				num = 1;
				continue;
				IL_FE:
				num = 2;
			}
			return false;
		}
		}
	}

	// Token: 0x0600285D RID: 10333 RVA: 0x0016DCC4 File Offset: 0x0016CCC4
	public bool ᜅ()
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			bool hasRichText2;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
				{
					if (num2 >= count)
					{
						num = 5;
						continue;
					}
					bool hasRichText = this.ᜀ(num2).HasRichText;
					num = 1;
					continue;
				}
				case 1:
				{
					bool hasRichText;
					if (hasRichText2 != hasRichText)
					{
						num = 2;
						continue;
					}
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F6;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				}
				case 2:
					return false;
				case 3:
					goto IL_BD;
				case 4:
					return false;
				case 5:
					goto IL_D9;
				case 7:
					goto IL_BD;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 4;
					continue;
				}
				hasRichText2 = this.ᜀ(0).HasRichText;
				num2 = 1;
				count = this.ᜄ.Count;
				goto IL_F6;
				IL_BD:
				num = 0;
				continue;
				IL_F6:
				num = 3;
			}
			return false;
			IL_D9:
			if (true)
			{
			}
			return hasRichText2;
		}
		}
	}

	// Token: 0x0600285E RID: 10334 RVA: 0x0016DDE0 File Offset: 0x0016CDE0
	public bool ᜬ()
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				bool hasString;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					return hasString;
				case 1:
					return false;
				case 2:
				{
					bool hasString2;
					if (hasString != hasString2)
					{
						num = 6;
						continue;
					}
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FE;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				}
				case 3:
					goto IL_C5;
				case 5:
				{
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					bool hasString2 = this.ᜀ(num2).HasString;
					num = 2;
					continue;
				}
				case 6:
					goto IL_B9;
				case 7:
					goto IL_C5;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 1;
					continue;
				}
				hasString = this.ᜀ(0).HasString;
				num2 = 1;
				count = this.ᜄ.Count;
				goto IL_FE;
				IL_C5:
				num = 5;
				continue;
				IL_FE:
				num = 3;
			}
			return false;
			IL_B9:
			if (true)
			{
			}
			return false;
		}
		}
	}

	// Token: 0x0600285F RID: 10335 RVA: 0x0016DEFC File Offset: 0x0016CEFC
	public bool ᜈ()
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				bool hasStyle;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					goto IL_C5;
				case 2:
					return hasStyle;
				case 3:
					return false;
				case 4:
					goto IL_C5;
				case 5:
				{
					bool hasStyle2;
					if (hasStyle != hasStyle2)
					{
						num = 7;
						continue;
					}
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FE;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				case 6:
				{
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					bool hasStyle2 = this.ᜀ(num2).HasStyle;
					num = 5;
					continue;
				}
				case 7:
					return false;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 3;
					continue;
				}
				hasStyle = this.ᜀ(0).HasStyle;
				num2 = 1;
				count = this.ᜄ.Count;
				goto IL_FE;
				IL_C5:
				num = 6;
				continue;
				IL_FE:
				num = 4;
			}
			return false;
		}
		}
	}

	// Token: 0x06002860 RID: 10336 RVA: 0x0016E018 File Offset: 0x0016D018
	public IHyperLinks \u1734()
	{
		int a_ = 7;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new NotImplementedException(RecordTableEnumerator.b("甼䘾ㅀ♂㝄⭆⁈╊♌㱎煐⍒❔㡖⥘㹚⽜⭞ᡠ", a_));
	}

	// Token: 0x06002861 RID: 10337 RVA: 0x0016E070 File Offset: 0x0016D070
	public HorizontalAlignType ᜂ()
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 3;
			for (;;)
			{
				HorizontalAlignType horizontalAlignment;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_C5;
				case 1:
					return horizontalAlignment;
				case 2:
					return HorizontalAlignType.General;
				case 4:
					return HorizontalAlignType.General;
				case 5:
					goto IL_C5;
				case 6:
				{
					HorizontalAlignType horizontalAlignment2;
					if (horizontalAlignment != horizontalAlignment2)
					{
						num = 2;
						continue;
					}
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FE;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				}
				case 7:
				{
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					HorizontalAlignType horizontalAlignment2 = this.ᜀ(num2).HorizontalAlignment;
					num = 6;
					continue;
				}
				}
				if (this.ᜄ.ᝀ())
				{
					num = 4;
					continue;
				}
				horizontalAlignment = this.ᜀ(0).HorizontalAlignment;
				num2 = 1;
				count = this.ᜄ.Count;
				goto IL_FE;
				IL_C5:
				num = 7;
				continue;
				IL_FE:
				num = 0;
			}
			return HorizontalAlignType.General;
		}
		}
	}

	// Token: 0x06002862 RID: 10338 RVA: 0x0016E18C File Offset: 0x0016D18C
	public void ᜀ(HorizontalAlignType A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						this.ᜀ(num).HorizontalAlignment = A_0;
						num++;
						if (true)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				case 1:
					goto IL_30;
				case 2:
					goto IL_30;
				case 3:
					return;
				}
				break;
				IL_30:
				num2 = 0;
			}
		}
	}

	// Token: 0x06002863 RID: 10339 RVA: 0x0016E22C File Offset: 0x0016D22C
	public int \u170D()
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			int indentLevel;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
					return int.MinValue;
				case 1:
					goto IL_DD;
				case 2:
					goto IL_C1;
				case 3:
				{
					int indentLevel2;
					if (indentLevel != indentLevel2)
					{
						num = 0;
						continue;
					}
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_102;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				}
				case 4:
					goto IL_C1;
				case 5:
					return int.MinValue;
				case 7:
				{
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					int indentLevel2 = this.ᜀ(num2).IndentLevel;
					num = 3;
					continue;
				}
				}
				if (this.ᜄ.ᝀ())
				{
					num = 5;
					continue;
				}
				indentLevel = this.ᜀ(0).IndentLevel;
				num2 = 1;
				count = this.ᜄ.Count;
				goto IL_102;
				IL_C1:
				num = 7;
				continue;
				IL_102:
				num = 4;
			}
			return int.MinValue;
			IL_DD:
			if (true)
			{
			}
			return indentLevel;
		}
		}
	}

	// Token: 0x06002864 RID: 10340 RVA: 0x0016E350 File Offset: 0x0016D350
	public void ᜃ(int A_0)
	{
		if (true)
		{
		}
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_42;
				case 1:
					return;
				case 2:
					goto IL_42;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						this.ᜀ(num).IndentLevel = A_0;
						num++;
						num2 = 0;
						continue;
					}
					break;
				}
				break;
				IL_42:
				num2 = 3;
			}
		}
	}

	// Token: 0x06002865 RID: 10341 RVA: 0x0016E3F0 File Offset: 0x0016D3F0
	public bool ᝆ()
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				bool isBlank;
				int num2;
				int count;
				switch (num)
				{
				case 0:
				{
					if (true)
					{
					}
					bool isBlank2;
					if (isBlank != isBlank2)
					{
						num = 6;
						continue;
					}
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FE;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				case 1:
				{
					if (num2 >= count)
					{
						num = 4;
						continue;
					}
					bool isBlank2 = this.ᜀ(num2).IsBlank;
					num = 0;
					continue;
				}
				case 2:
					return false;
				case 3:
					goto IL_C5;
				case 4:
					return isBlank;
				case 5:
					goto IL_C5;
				case 6:
					return false;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 2;
					continue;
				}
				isBlank = this.ᜀ(0).IsBlank;
				num2 = 1;
				count = this.ᜄ.Count;
				goto IL_FE;
				IL_C5:
				num = 1;
				continue;
				IL_FE:
				num = 5;
			}
			return false;
		}
		}
	}

	// Token: 0x06002866 RID: 10342 RVA: 0x0016E50C File Offset: 0x0016D50C
	public bool ᜐ()
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			bool hasError;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_BD;
				case 1:
					goto IL_D9;
				case 2:
					return false;
				case 3:
				{
					bool hasError2;
					if (hasError != hasError2)
					{
						num = 2;
						continue;
					}
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FE;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				}
				case 4:
					return false;
				case 5:
				{
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					bool hasError2 = this.ᜀ(num2).HasError;
					num = 3;
					continue;
				}
				case 6:
					goto IL_BD;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 4;
					continue;
				}
				hasError = this.ᜀ(0).HasError;
				num2 = 1;
				count = this.ᜄ.Count;
				goto IL_FE;
				IL_BD:
				num = 5;
				continue;
				IL_FE:
				num = 0;
			}
			return false;
			IL_D9:
			if (true)
			{
			}
			return hasError;
		}
		}
	}

	// Token: 0x06002867 RID: 10343 RVA: 0x0016E628 File Offset: 0x0016D628
	public bool \u1755()
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				bool isGroupedByColumn;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_C5;
				case 1:
					return false;
				case 2:
					return isGroupedByColumn;
				case 3:
				{
					bool isGroupedByColumn2;
					if (isGroupedByColumn != isGroupedByColumn2)
					{
						num = 1;
						continue;
					}
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FE;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				}
				case 4:
					return false;
				case 5:
					goto IL_C5;
				case 6:
					if (true)
					{
					}
					break;
				case 7:
				{
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					bool isGroupedByColumn2 = this.ᜀ(num2).IsGroupedByColumn;
					num = 3;
					continue;
				}
				}
				if (this.ᜄ.ᝀ())
				{
					num = 4;
					continue;
				}
				isGroupedByColumn = this.ᜀ(0).IsGroupedByColumn;
				num2 = 1;
				count = this.ᜄ.Count;
				goto IL_FE;
				IL_C5:
				num = 7;
				continue;
				IL_FE:
				num = 0;
			}
			return false;
		}
		}
	}

	// Token: 0x06002868 RID: 10344 RVA: 0x0016E744 File Offset: 0x0016D744
	public bool ᝃ()
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				bool isGroupedByRow;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_BD;
				case 1:
				{
					bool isGroupedByRow2;
					if (isGroupedByRow != isGroupedByRow2)
					{
						num = 2;
						continue;
					}
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FE;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				}
				case 2:
					return false;
				case 4:
					return false;
				case 5:
					goto IL_BD;
				case 6:
				{
					if (true)
					{
					}
					if (num2 >= count)
					{
						num = 7;
						continue;
					}
					bool isGroupedByRow2 = this.ᜀ(num2).IsGroupedByRow;
					num = 1;
					continue;
				}
				case 7:
					return isGroupedByRow;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 4;
					continue;
				}
				isGroupedByRow = this.ᜀ(0).IsGroupedByRow;
				num2 = 1;
				count = this.ᜄ.Count;
				goto IL_FE;
				IL_BD:
				num = 6;
				continue;
				IL_FE:
				num = 0;
			}
			return false;
		}
		}
	}

	// Token: 0x06002869 RID: 10345 RVA: 0x0016E860 File Offset: 0x0016D860
	public bool \u1739()
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				bool isInitialized;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					return false;
				case 1:
				{
					bool isInitialized2;
					if (isInitialized != isInitialized2)
					{
						num = 0;
						continue;
					}
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FE;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				case 2:
				{
					if (num2 >= count)
					{
						num = 4;
						continue;
					}
					bool isInitialized2 = this.ᜀ(num2).IsInitialized;
					num = 1;
					continue;
				}
				case 3:
					goto IL_C5;
				case 4:
					return isInitialized;
				case 5:
					goto IL_C5;
				case 6:
					return false;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 6;
					continue;
				}
				isInitialized = this.ᜀ(0).IsInitialized;
				num2 = 1;
				count = this.ᜄ.Count;
				goto IL_FE;
				IL_C5:
				num = 2;
				continue;
				IL_FE:
				num = 5;
			}
			return false;
		}
		}
	}

	// Token: 0x0600286A RID: 10346 RVA: 0x0016E97C File Offset: 0x0016D97C
	public int \u1718()
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
		return this.ᜃ;
	}

	// Token: 0x0600286B RID: 10347 RVA: 0x0016E9C0 File Offset: 0x0016D9C0
	public void ᜄ(int A_0)
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
	}

	// Token: 0x0600286C RID: 10348 RVA: 0x0016E9FC File Offset: 0x0016D9FC
	public int \u173E()
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
		return this.ᜂ;
	}

	// Token: 0x0600286D RID: 10349 RVA: 0x0016EA40 File Offset: 0x0016DA40
	public void ᜁ(int A_0)
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

	// Token: 0x0600286E RID: 10350 RVA: 0x0016EA7C File Offset: 0x0016DA7C
	public double \u175C()
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				double numberValue;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					return numberValue;
				case 1:
					goto IL_59;
				case 3:
				{
					if (true)
					{
					}
					double numberValue2;
					if (numberValue != numberValue2)
					{
						num = 4;
						continue;
					}
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_106;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				}
				case 4:
					goto IL_C1;
				case 5:
				{
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					double numberValue2 = this.ᜀ(num2).NumberValue;
					num = 3;
					continue;
				}
				case 6:
					goto IL_CD;
				case 7:
					goto IL_CD;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 1;
					continue;
				}
				numberValue = this.ᜀ(0).NumberValue;
				num2 = 1;
				count = this.ᜄ.Count;
				goto IL_106;
				IL_CD:
				num = 5;
				continue;
				IL_106:
				num = 6;
			}
			IL_59:
			return double.MinValue;
			IL_C1:
			return double.MinValue;
		}
		}
	}

	// Token: 0x0600286F RID: 10351 RVA: 0x0016EBA8 File Offset: 0x0016DBA8
	public void ᜃ(double A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_30;
				case 1:
					goto IL_30;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						if (true)
						{
						}
						this.ᜀ(num).NumberValue = A_0;
						num++;
						num2 = 0;
						continue;
					}
					break;
				case 3:
					return;
				}
				break;
				IL_30:
				num2 = 2;
			}
		}
	}

	// Token: 0x06002870 RID: 10352 RVA: 0x0016EC48 File Offset: 0x0016DC48
	public string ᜊ()
	{
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				string numberFormat;
				int num2;
				int count;
				switch (num)
				{
				case 1:
					goto IL_CA;
				case 2:
					goto IL_61;
				case 3:
				{
					string numberFormat2;
					if (numberFormat != numberFormat2)
					{
						num = 7;
						continue;
					}
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_103;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				case 4:
					goto IL_CA;
				case 5:
					return numberFormat;
				case 6:
				{
					if (num2 >= count)
					{
						num = 5;
						continue;
					}
					string numberFormat2 = this.ᜀ(num2).NumberFormat;
					num = 3;
					continue;
				}
				case 7:
					goto IL_C6;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 2;
					continue;
				}
				numberFormat = this.ᜀ(0).NumberFormat;
				num2 = 1;
				count = this.ᜄ.Count;
				goto IL_103;
				IL_CA:
				num = 6;
				continue;
				IL_103:
				num = 1;
			}
			IL_61:
			return null;
			IL_C6:
			return null;
		}
		}
	}

	// Token: 0x06002871 RID: 10353 RVA: 0x0016ED6C File Offset: 0x0016DD6C
	public void ᜋ(string A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					goto IL_38;
				case 1:
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_38;
					}
					if (false)
					{
					}
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					this.ᜀ(num).NumberFormat = A_0;
					num++;
					num2 = 0;
					continue;
				case 3:
					goto IL_38;
				}
				break;
				IL_38:
				num2 = 2;
			}
		}
	}

	// Token: 0x06002872 RID: 10354 RVA: 0x0016EE0C File Offset: 0x0016DE0C
	public int ᝊ()
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
		return this.ᜀ;
	}

	// Token: 0x06002873 RID: 10355 RVA: 0x0016EE50 File Offset: 0x0016DE50
	public int \u1732()
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				int rowGroupLevel;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					return rowGroupLevel;
				case 1:
					goto IL_92;
				case 2:
					if (true)
					{
					}
					break;
				case 3:
				{
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					int rowGroupLevel2 = this.ᜀ(num2).RowGroupLevel;
					num = 7;
					continue;
				}
				case 4:
					return int.MinValue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return int.MinValue;
					default:
						if (false)
						{
						}
						goto IL_A4;
					}
					break;
				case 6:
					goto IL_A4;
				case 7:
				{
					int rowGroupLevel2;
					if (rowGroupLevel != rowGroupLevel2)
					{
						num = 1;
						continue;
					}
					num2++;
					num = 6;
					continue;
				}
				}
				if (this.ᜄ.ᝀ())
				{
					num = 4;
					continue;
				}
				rowGroupLevel = this.ᜀ(0).RowGroupLevel;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 5;
				continue;
				IL_A4:
				num = 3;
			}
			return int.MinValue;
			IL_92:
			return int.MinValue;
		}
		}
	}

	// Token: 0x06002874 RID: 10356 RVA: 0x0016EF6C File Offset: 0x0016DF6C
	public double \u173C()
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				double rowHeight;
				int num2;
				int count;
				switch (num)
				{
				case 1:
					return rowHeight;
				case 2:
					goto IL_A8;
				case 3:
				{
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					double rowHeight2 = this.ᜀ(num2).RowHeight;
					num = 5;
					continue;
				}
				case 4:
					goto IL_56;
				case 5:
				{
					double rowHeight2;
					if (rowHeight != rowHeight2)
					{
						num = 6;
						continue;
					}
					num2++;
					num = 2;
					continue;
				}
				case 6:
					goto IL_92;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10B;
					default:
						if (false)
						{
						}
						goto IL_A8;
					}
					break;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 4;
					continue;
				}
				rowHeight = this.ᜀ(0).RowHeight;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 7;
				continue;
				IL_A8:
				num = 3;
			}
			IL_56:
			if (true)
			{
			}
			return double.MinValue;
			IL_92:
			IL_10B:
			return double.MinValue;
		}
		}
	}

	// Token: 0x06002875 RID: 10357 RVA: 0x0016F090 File Offset: 0x0016E090
	public void ᜀ(double A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_30;
				case 2:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_30;
					}
					if (false)
					{
					}
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					this.ᜀ(num).RowHeight = A_0;
					num++;
					num2 = 3;
					continue;
				case 3:
					goto IL_30;
				}
				break;
				IL_30:
				num2 = 2;
			}
		}
	}

	// Token: 0x06002876 RID: 10358 RVA: 0x0016F130 File Offset: 0x0016E130
	public IXLSRange[] ᜠ()
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
		throw new NotImplementedException();
	}

	// Token: 0x06002877 RID: 10359 RVA: 0x0016F170 File Offset: 0x0016E170
	public IXLSRange[] \u1712()
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
		throw new NotImplementedException();
	}

	// Token: 0x06002878 RID: 10360 RVA: 0x0016F1B0 File Offset: 0x0016E1B0
	public IStyle \u1757()
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
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					this.ᜇ = new sprᴖ(base.ReservedHandle, this);
					goto IL_64;
				case 2:
					goto IL_76;
				}
				if (true)
				{
				}
				if (this.ᜇ == null)
				{
					num = 0;
					continue;
				}
				goto IL_78;
			}
			IL_64:
			num = 2;
		}
		IL_76:
		IL_78:
		return this.ᜇ;
	}

	// Token: 0x06002879 RID: 10361 RVA: 0x0016F23C File Offset: 0x0016E23C
	public void ᜀ(IStyle A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x0600287A RID: 10362 RVA: 0x0016F27C File Offset: 0x0016E27C
	public string \u171C()
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				int num2;
				int count;
				string cellStyleName2;
				switch (num)
				{
				case 1:
				{
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					string cellStyleName = this.ᜀ(num2).CellStyleName;
					num = 6;
					continue;
				}
				case 2:
					return cellStyleName2;
				case 3:
					goto IL_A5;
				case 4:
					goto IL_56;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_108;
					default:
						if (false)
						{
						}
						goto IL_A5;
					}
					break;
				case 6:
				{
					string cellStyleName;
					if (cellStyleName2 != cellStyleName)
					{
						num = 7;
						continue;
					}
					num2++;
					if (true)
					{
					}
					num = 3;
					continue;
				}
				case 7:
					goto IL_97;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 4;
					continue;
				}
				cellStyleName2 = this.ᜀ(0).CellStyleName;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 5;
				continue;
				IL_A5:
				num = 1;
			}
			IL_56:
			return null;
			IL_97:
			IL_108:
			return null;
		}
		}
	}

	// Token: 0x0600287B RID: 10363 RVA: 0x0016F394 File Offset: 0x0016E394
	public void ᜇ(string A_0)
	{
		if (true)
		{
		}
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_38;
					}
					if (false)
					{
					}
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					this.ᜀ(num).CellStyleName = A_0;
					num++;
					num2 = 2;
					continue;
				case 1:
					return;
				case 2:
					goto IL_38;
				case 3:
					goto IL_38;
				}
				break;
				IL_38:
				num2 = 0;
			}
		}
	}

	// Token: 0x0600287C RID: 10364 RVA: 0x0016F434 File Offset: 0x0016E434
	public string ᜩ()
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				string text;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_56;
				case 1:
					return text;
				case 2:
					goto IL_97;
				case 4:
				{
					string text2;
					if (text != text2)
					{
						num = 2;
						continue;
					}
					if (true)
					{
					}
					num2++;
					num = 6;
					continue;
				}
				case 5:
				{
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					string text2 = this.ᜀ(num2).Text;
					num = 4;
					continue;
				}
				case 6:
					goto IL_A5;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_108;
					default:
						if (false)
						{
						}
						goto IL_A5;
					}
					break;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 0;
					continue;
				}
				text = this.ᜀ(0).Text;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 7;
				continue;
				IL_A5:
				num = 5;
			}
			IL_56:
			return null;
			IL_97:
			IL_108:
			return null;
		}
		}
	}

	// Token: 0x0600287D RID: 10365 RVA: 0x0016F54C File Offset: 0x0016E54C
	public void ᜆ(string A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_30;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_30;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						this.ᜀ(num).Text = A_0;
						num++;
						num2 = 3;
						continue;
					}
					break;
				case 3:
					goto IL_30;
				}
				break;
				IL_30:
				num2 = 2;
			}
		}
	}

	// Token: 0x0600287E RID: 10366 RVA: 0x0016F5EC File Offset: 0x0016E5EC
	public TimeSpan \u1752()
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				TimeSpan timeSpanValue;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_A9;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10C;
					default:
						if (false)
						{
						}
						goto IL_A9;
					}
					break;
				case 3:
				{
					if (true)
					{
					}
					TimeSpan timeSpanValue2;
					if (timeSpanValue != timeSpanValue2)
					{
						num = 7;
						continue;
					}
					num2++;
					num = 0;
					continue;
				}
				case 4:
				{
					if (num2 >= count)
					{
						num = 6;
						continue;
					}
					TimeSpan timeSpanValue2 = this.ᜀ(num2).TimeSpanValue;
					num = 3;
					continue;
				}
				case 5:
					goto IL_56;
				case 6:
					return timeSpanValue;
				case 7:
					goto IL_97;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 5;
					continue;
				}
				timeSpanValue = this.ᜀ(0).TimeSpanValue;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 1;
				continue;
				IL_A9:
				num = 4;
			}
			IL_56:
			return TimeSpan.MinValue;
			IL_97:
			IL_10C:
			return TimeSpan.MinValue;
		}
		}
	}

	// Token: 0x0600287F RID: 10367 RVA: 0x0016F70C File Offset: 0x0016E70C
	public void ᜀ(TimeSpan A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			if (true)
			{
			}
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_38;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_38;
					}
					if (false)
					{
					}
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					this.ᜀ(num).TimeSpanValue = A_0;
					num++;
					num2 = 0;
					continue;
				case 2:
					goto IL_38;
				case 3:
					return;
				}
				break;
				IL_38:
				num2 = 1;
			}
		}
	}

	// Token: 0x06002880 RID: 10368 RVA: 0x0016F7AC File Offset: 0x0016E7AC
	public string \u171D()
	{
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				string value;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_108;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_9D;
					}
					break;
				case 1:
					return value;
				case 2:
					goto IL_56;
				case 3:
					goto IL_8F;
				case 4:
				{
					string value2;
					if (value != value2)
					{
						num = 3;
						continue;
					}
					num2++;
					num = 7;
					continue;
				}
				case 6:
				{
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					string value2 = this.ᜀ(num2).Value;
					num = 4;
					continue;
				}
				case 7:
					goto IL_9D;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 2;
					continue;
				}
				value = this.ᜀ(0).Value;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 0;
				continue;
				IL_9D:
				num = 6;
			}
			IL_56:
			return null;
			IL_8F:
			IL_108:
			return null;
		}
		}
	}

	// Token: 0x06002881 RID: 10369 RVA: 0x0016F8C4 File Offset: 0x0016E8C4
	public void ᜄ(string A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_38;
					}
					if (false)
					{
					}
					if (num >= count)
					{
						num2 = 2;
						continue;
					}
					this.ᜀ(num).Value = A_0;
					num++;
					num2 = 3;
					continue;
				case 1:
					goto IL_38;
				case 2:
					return;
				case 3:
					goto IL_38;
				}
				break;
				IL_38:
				num2 = 0;
			}
		}
	}

	// Token: 0x06002882 RID: 10370 RVA: 0x0016F964 File Offset: 0x0016E964
	public string ᝋ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (((IWorksheet)base.Parent).FormulaEngine == null)
				{
					goto IL_CD;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3A;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 2:
				num = 0;
				continue;
			case 3:
				goto IL_C0;
			}
			if (!(base.Parent is IWorksheet))
			{
				goto IL_CD;
			}
			num = 2;
		}
		IL_3A:
		string a_ = sprḅ.ᜀ(this.ᝄ()) + this.ᝊ().ToString();
		return ((IWorksheet)base.Parent).FormulaEngine.ᜀ.\u17C4(a_);
		IL_C0:
		if (true)
		{
		}
		goto IL_3A;
		IL_CD:
		return null;
	}

	// Token: 0x06002883 RID: 10371 RVA: 0x0016FA40 File Offset: 0x0016EA40
	public object ᜭ()
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				int num2;
				int count;
				object value2;
				switch (num)
				{
				case 0:
					goto IL_56;
				case 1:
				{
					if (num2 >= count)
					{
						num = 3;
						continue;
					}
					object value = this.ᜀ(num2).Value2;
					num = 6;
					continue;
				}
				case 3:
					return value2;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_103;
					default:
						if (false)
						{
						}
						goto IL_A0;
					}
					break;
				case 5:
					goto IL_A0;
				case 6:
				{
					if (true)
					{
					}
					object value;
					if (value2 != value)
					{
						num = 7;
						continue;
					}
					num2++;
					num = 5;
					continue;
				}
				case 7:
					goto IL_92;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 0;
					continue;
				}
				value2 = this.ᜀ(0).Value2;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 4;
				continue;
				IL_A0:
				num = 1;
			}
			IL_56:
			return null;
			IL_92:
			IL_103:
			return null;
		}
		}
	}

	// Token: 0x06002884 RID: 10372 RVA: 0x0016FB54 File Offset: 0x0016EB54
	public void ᜀ(object A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_38;
					}
					if (false)
					{
					}
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					this.ᜀ(num).Value2 = A_0;
					num++;
					num2 = 1;
					continue;
				case 1:
					goto IL_38;
				case 2:
					goto IL_38;
				case 3:
					return;
				}
				break;
				IL_38:
				num2 = 0;
			}
		}
	}

	// Token: 0x06002885 RID: 10373 RVA: 0x0016FBF4 File Offset: 0x0016EBF4
	public VerticalAlignType ᜑ()
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				VerticalAlignType verticalAlignment;
				int num2;
				int count;
				switch (num)
				{
				case 0:
				{
					VerticalAlignType verticalAlignment2;
					if (verticalAlignment != verticalAlignment2)
					{
						num = 4;
						continue;
					}
					num2++;
					num = 2;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return VerticalAlignType.Top;
					default:
						if (false)
						{
						}
						goto IL_A0;
					}
					break;
				case 2:
					goto IL_A0;
				case 4:
					goto IL_8A;
				case 5:
					return verticalAlignment;
				case 6:
					return VerticalAlignType.Top;
				case 7:
				{
					if (num2 >= count)
					{
						num = 5;
						continue;
					}
					VerticalAlignType verticalAlignment2 = this.ᜀ(num2).VerticalAlignment;
					num = 0;
					continue;
				}
				}
				if (this.ᜄ.ᝀ())
				{
					num = 6;
					continue;
				}
				verticalAlignment = this.ᜀ(0).VerticalAlignment;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 1;
				continue;
				IL_A0:
				num = 7;
			}
			return VerticalAlignType.Top;
			IL_8A:
			if (true)
			{
			}
			return VerticalAlignType.Top;
		}
		}
	}

	// Token: 0x06002886 RID: 10374 RVA: 0x0016FD08 File Offset: 0x0016ED08
	public void ᜀ(VerticalAlignType A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_30;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_30;
					default:
						if (false)
						{
						}
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						this.ᜀ(num).VerticalAlignment = A_0;
						num++;
						if (true)
						{
						}
						num2 = 3;
						continue;
					}
					break;
				case 2:
					return;
				case 3:
					goto IL_30;
				}
				break;
				IL_30:
				num2 = 1;
			}
		}
	}

	// Token: 0x06002887 RID: 10375 RVA: 0x0016FDA8 File Offset: 0x0016EDA8
	public IWorksheet \u173D()
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
		return this.ᜄ;
	}

	// Token: 0x06002888 RID: 10376 RVA: 0x0016FDEC File Offset: 0x0016EDEC
	public IXLSRange ᜁ(int A_0, int A_1)
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
		return new spr\u1CCF(base.ReservedHandle, this.ᜄ, A_0, A_1, A_0, A_1);
	}

	// Token: 0x06002889 RID: 10377 RVA: 0x0016FE3C File Offset: 0x0016EE3C
	public void ᜀ(int A_0, int A_1, IXLSRange A_2)
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
		throw new NotSupportedException();
	}

	// Token: 0x0600288A RID: 10378 RVA: 0x0016FE7C File Offset: 0x0016EE7C
	public IXLSRange ᜀ(int A_0, int A_1, int A_2, int A_3)
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
		return new spr\u1CCF(base.ReservedHandle, this.ᜄ, A_0, A_1, A_2, A_3);
	}

	// Token: 0x0600288B RID: 10379 RVA: 0x0016FED0 File Offset: 0x0016EED0
	public IXLSRange ᜈ(string A_0)
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
		return this.ᜀ(A_0, false);
	}

	// Token: 0x0600288C RID: 10380 RVA: 0x0016FF14 File Offset: 0x0016EF14
	public IXLSRange ᜀ(string A_0, bool A_1)
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
		return new spr\u1CCF((spr\u2158)base.ReservedHandle, this.ᜄ, A_0, A_1);
	}

	// Token: 0x0600288D RID: 10381 RVA: 0x0016FF68 File Offset: 0x0016EF68
	public ConditionalFormats \u1715()
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
		throw new NotImplementedException();
	}

	// Token: 0x0600288E RID: 10382 RVA: 0x0016FFA8 File Offset: 0x0016EFA8
	public Validation ᝀ()
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

	// Token: 0x0600288F RID: 10383 RVA: 0x0016FFE4 File Offset: 0x0016EFE4
	public string \u1713()
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_97;
				case 1:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					string formulaStringValue = this.ᜀ(num2).FormulaStringValue;
					num = 4;
					continue;
				}
				case 2:
				{
					string formulaStringValue2;
					return formulaStringValue2;
				}
				case 3:
					goto IL_5E;
				case 4:
				{
					string formulaStringValue;
					string formulaStringValue2;
					if (formulaStringValue2 != formulaStringValue)
					{
						num = 0;
						continue;
					}
					int num2;
					num2++;
					num = 5;
					continue;
				}
				case 5:
					goto IL_9B;
				case 7:
					goto IL_9B;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 3;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_99;
				default:
				{
					if (false)
					{
					}
					string formulaStringValue2 = this.ᜀ(0).FormulaStringValue;
					int num2 = 1;
					int count = this.ᜄ.Count;
					num = 7;
					continue;
				}
				}
				IL_9B:
				num = 1;
			}
			IL_5E:
			goto IL_99;
			IL_97:
			return null;
			IL_99:
			return null;
		}
		}
	}

	// Token: 0x06002890 RID: 10384 RVA: 0x001700FC File Offset: 0x0016F0FC
	public void ᜊ(string A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_38;
				case 2:
					goto IL_38;
				case 3:
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					this.ᜀ(num).FormulaStringValue = A_0;
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num2 = 2;
						continue;
					}
					break;
				}
				break;
				IL_38:
				num2 = 3;
			}
		}
	}

	// Token: 0x06002891 RID: 10385 RVA: 0x0017019C File Offset: 0x0016F19C
	public double ᜰ()
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9E;
				case 1:
					goto IL_5E;
				case 2:
					goto IL_9E;
				case 3:
				{
					double formulaNumberValue;
					double formulaNumberValue2;
					if (formulaNumberValue != formulaNumberValue2)
					{
						num = 5;
						continue;
					}
					int num2;
					num2++;
					num = 2;
					continue;
				}
				case 4:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 7;
						continue;
					}
					double formulaNumberValue2 = this.ᜀ(num2).FormulaNumberValue;
					num = 3;
					continue;
				}
				case 5:
					goto IL_92;
				case 6:
					if (true)
					{
					}
					break;
				case 7:
				{
					double formulaNumberValue;
					return formulaNumberValue;
				}
				}
				if (this.ᜄ.ᝀ())
				{
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_94;
				default:
				{
					if (false)
					{
					}
					double formulaNumberValue = this.ᜀ(0).FormulaNumberValue;
					int num2 = 1;
					int count = this.ᜄ.Count;
					num = 0;
					continue;
				}
				}
				IL_9E:
				num = 4;
			}
			IL_5E:
			goto IL_94;
			IL_92:
			return double.MinValue;
			IL_94:
			return double.MinValue;
		}
		}
	}

	// Token: 0x06002892 RID: 10386 RVA: 0x001702C0 File Offset: 0x0016F2C0
	public void ᜂ(double A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						if (true)
						{
						}
						num2 = 2;
						continue;
					}
					this.ᜀ(num).FormulaNumberValue = A_0;
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				case 1:
					goto IL_30;
				case 2:
					return;
				case 3:
					goto IL_30;
				}
				break;
				IL_30:
				num2 = 0;
			}
		}
	}

	// Token: 0x06002893 RID: 10387 RVA: 0x00170360 File Offset: 0x0016F360
	public bool ᜁ()
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					bool formulaBoolValue;
					bool formulaBoolValue2;
					if (formulaBoolValue != formulaBoolValue2)
					{
						num = 5;
						continue;
					}
					int num2;
					num2++;
					num = 6;
					continue;
				}
				case 1:
					goto IL_5E;
				case 2:
					goto IL_96;
				case 3:
					if (true)
					{
					}
					break;
				case 4:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 7;
						continue;
					}
					bool formulaBoolValue2 = this.ᜀ(num2).FormulaBoolValue;
					num = 0;
					continue;
				}
				case 5:
					return false;
				case 6:
					goto IL_96;
				case 7:
				{
					bool formulaBoolValue;
					return formulaBoolValue;
				}
				}
				if (this.ᜄ.ᝀ())
				{
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return false;
				default:
				{
					if (false)
					{
					}
					bool formulaBoolValue = this.ᜀ(0).FormulaBoolValue;
					int num2 = 1;
					int count = this.ᜄ.Count;
					num = 2;
					continue;
				}
				}
				IL_96:
				num = 4;
			}
			IL_5E:
			return false;
		}
		}
	}

	// Token: 0x06002894 RID: 10388 RVA: 0x00170470 File Offset: 0x0016F470
	public void ᜂ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_30;
				case 1:
					goto IL_4E;
				case 2:
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					this.ᜀ(num).FormulaBoolValue = A_0;
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4E;
					default:
						if (false)
						{
						}
						num2 = 0;
						continue;
					}
					break;
				case 3:
					goto IL_30;
				}
				break;
				IL_30:
				num2 = 2;
			}
		}
		IL_4E:
		if (true)
		{
		}
	}

	// Token: 0x06002895 RID: 10389 RVA: 0x00170510 File Offset: 0x0016F510
	public string ᜣ()
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 7;
						continue;
					}
					string formulaStringValue = this.ᜀ(num2).FormulaStringValue;
					num = 1;
					continue;
				}
				case 1:
				{
					string formulaStringValue;
					string formulaErrorValue;
					if (formulaErrorValue != formulaStringValue)
					{
						num = 2;
						continue;
					}
					int num2;
					num2++;
					num = 4;
					continue;
				}
				case 2:
					goto IL_8F;
				case 3:
					goto IL_9B;
				case 4:
					goto IL_9B;
				case 5:
					goto IL_56;
				case 7:
				{
					string formulaErrorValue;
					return formulaErrorValue;
				}
				}
				if (this.ᜄ.ᝀ())
				{
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_99;
				default:
				{
					if (false)
					{
					}
					string formulaErrorValue = this.ᜀ(0).FormulaErrorValue;
					int num2 = 1;
					int count = this.ᜄ.Count;
					num = 3;
					continue;
				}
				}
				IL_9B:
				num = 0;
			}
			IL_56:
			goto IL_99;
			IL_8F:
			if (true)
			{
			}
			return null;
			IL_99:
			return null;
		}
		}
	}

	// Token: 0x06002896 RID: 10390 RVA: 0x00170628 File Offset: 0x0016F628
	public void ᜅ(string A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_38;
				case 2:
					goto IL_38;
				case 3:
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					this.ᜀ(num).FormulaErrorValue = A_0;
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				}
				break;
				IL_38:
				num2 = 3;
			}
		}
	}

	// Token: 0x06002897 RID: 10391 RVA: 0x001706C8 File Offset: 0x0016F6C8
	public ICommentShape ᜌ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002898 RID: 10392 RVA: 0x00170708 File Offset: 0x0016F708
	public IRichTextString ᝇ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				for (;;)
				{
					this.ᜅ = new spr\u1C7E(base.ReservedHandle, this);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_60;
					}
				}
				IL_60:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 1;
				continue;
			case 1:
				goto IL_76;
			}
			if (this.ᜅ != null)
			{
				break;
			}
			num = 0;
		}
		IL_76:
		return this.ᜅ;
	}

	// Token: 0x06002899 RID: 10393 RVA: 0x00170794 File Offset: 0x0016F794
	public bool \u1714()
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_96;
				case 2:
					return false;
				case 3:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 5;
						continue;
					}
					bool hasMerged = this.ᜀ(num2).HasMerged;
					num = 7;
					continue;
				}
				case 4:
					goto IL_56;
				case 5:
				{
					bool hasMerged2;
					return hasMerged2;
				}
				case 6:
					goto IL_96;
				case 7:
				{
					bool hasMerged;
					bool hasMerged2;
					if (hasMerged2 != hasMerged)
					{
						num = 2;
						continue;
					}
					int num2;
					num2++;
					if (true)
					{
					}
					num = 6;
					continue;
				}
				}
				if (this.ᜄ.ᝀ())
				{
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return false;
				default:
				{
					if (false)
					{
					}
					bool hasMerged2 = this.ᜀ(0).HasMerged;
					int num2 = 1;
					int count = this.ᜄ.Count;
					num = 1;
					continue;
				}
				}
				IL_96:
				num = 3;
			}
			IL_56:
			return false;
		}
		}
	}

	// Token: 0x0600289A RID: 10394 RVA: 0x001708A8 File Offset: 0x0016F8A8
	public IXLSRange ᜄ()
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
		throw new NotImplementedException();
	}

	// Token: 0x0600289B RID: 10395 RVA: 0x001708E8 File Offset: 0x0016F8E8
	public bool ᝑ()
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				bool isWrapText;
				int num2;
				int count;
				switch (num)
				{
				case 0:
				{
					bool isWrapText2;
					if (isWrapText != isWrapText2)
					{
						num = 1;
						continue;
					}
					num2++;
					num = 2;
					continue;
				}
				case 1:
					goto IL_8A;
				case 2:
					goto IL_8E;
				case 3:
					goto IL_8E;
				case 4:
					return isWrapText;
				case 5:
					goto IL_56;
				case 6:
				{
					if (num2 >= count)
					{
						num = 4;
						continue;
					}
					bool isWrapText2 = this.ᜀ(num2).IsWrapText;
					num = 0;
					continue;
				}
				}
				if (this.ᜄ.ᝀ())
				{
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return false;
				}
				if (false)
				{
				}
				isWrapText = this.ᜀ(0).IsWrapText;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 3;
				continue;
				IL_8E:
				num = 6;
			}
			IL_56:
			return false;
			IL_8A:
			if (true)
			{
			}
			return false;
		}
		}
	}

	// Token: 0x0600289C RID: 10396 RVA: 0x001709F8 File Offset: 0x0016F9F8
	public void ᜃ(bool A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_38;
				case 1:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					this.ᜀ(num).IsWrapText = A_0;
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num2 = 2;
						continue;
					}
					break;
				case 2:
					goto IL_38;
				case 3:
					return;
				}
				break;
				IL_38:
				num2 = 1;
			}
		}
	}

	// Token: 0x0600289D RID: 10397 RVA: 0x00170A98 File Offset: 0x0016FA98
	public bool \u173A()
	{
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 1:
					goto IL_96;
				case 2:
					if (num2 >= count)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					num = 3;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						if (!this.ᜀ(num2).HasExternalFormula)
						{
							num = 6;
							continue;
						}
						num2++;
						num = 7;
						continue;
					}
					break;
				case 4:
					return true;
				case 5:
					return false;
				case 6:
					return false;
				case 7:
					goto IL_96;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 5;
					continue;
				}
				num2 = 0;
				count = this.ᜄ.Count;
				num = 1;
				continue;
				IL_96:
				num = 2;
			}
		}
		return false;
	}

	// Token: 0x0600289E RID: 10398 RVA: 0x00170B88 File Offset: 0x0016FB88
	public IgnoreErrorType ᜋ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_D6:
			num = 7;
			break;
		default:
			if (false)
			{
			}
			num = 3;
			break;
		}
		int num2;
		int count;
		IgnoreErrorType ignoreErrorType;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_BB;
			case 1:
				if (num2 < count)
				{
					num = 4;
					continue;
				}
				return ignoreErrorType;
			case 2:
				if (ignoreErrorType == IgnoreErrorType.None)
				{
					num = 0;
					continue;
				}
				ignoreErrorType &= this.ᜀ(num2).IgnoreErrorOptions;
				num2++;
				num = 6;
				continue;
			case 4:
				num = 2;
				continue;
			case 5:
				return IgnoreErrorType.None;
			case 6:
				goto IL_63;
			case 7:
				goto IL_63;
			}
			if (this.ᜄ.ᝀ())
			{
				num = 5;
				continue;
			}
			goto IL_C5;
			IL_63:
			num = 1;
		}
		return IgnoreErrorType.None;
		IL_BB:
		if (true)
		{
		}
		return ignoreErrorType;
		IL_C5:
		ignoreErrorType = IgnoreErrorType.All;
		num2 = 0;
		count = this.ᜄ.Count;
		goto IL_D6;
	}

	// Token: 0x0600289F RID: 10399 RVA: 0x00170C7C File Offset: 0x0016FC7C
	public void ᜀ(IgnoreErrorType A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					this.ᜀ(num).IgnoreErrorOptions = A_0;
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num2 = 2;
						continue;
					}
					break;
				case 2:
					goto IL_38;
				case 3:
					goto IL_38;
				}
				break;
				IL_38:
				num2 = 1;
			}
		}
	}

	// Token: 0x060028A0 RID: 10400 RVA: 0x00170D1C File Offset: 0x0016FD1C
	public bool? \u1758()
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
		return this.ᜄ.ᜀ(this);
	}

	// Token: 0x060028A1 RID: 10401 RVA: 0x00170D64 File Offset: 0x0016FD64
	public void ᜀ(bool? A_0)
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
		this.ᜄ.ᜀ(this, A_0);
	}

	// Token: 0x060028A2 RID: 10402 RVA: 0x00170DAC File Offset: 0x0016FDAC
	public BuiltInStyles? \u171E()
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				BuiltInStyles? builtInStyles;
				int num2;
				bool flag;
				int count;
				switch (num)
				{
				case 0:
				{
					if (builtInStyles == null)
					{
						num = 1;
						continue;
					}
					BuiltInStyles? builtInStyle = this.ᜀ(num2).BuiltInStyle;
					BuiltInStyles? builtInStyles2 = builtInStyle;
					BuiltInStyles? builtInStyles3 = builtInStyles;
					goto IL_CD;
				}
				case 1:
					return builtInStyles;
				case 2:
					builtInStyles = null;
					num = 5;
					continue;
				case 4:
					goto IL_14C;
				case 5:
					return builtInStyles;
				case 6:
				{
					if (true)
					{
					}
					BuiltInStyles? builtInStyles2;
					BuiltInStyles? builtInStyles3;
					flag = (builtInStyles2 != null != (builtInStyles3 != null));
					goto IL_13C;
				}
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CD;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 8:
				{
					BuiltInStyles? builtInStyles2;
					BuiltInStyles? builtInStyles3;
					if (builtInStyles2.GetValueOrDefault() == builtInStyles3.GetValueOrDefault())
					{
						num = 7;
						continue;
					}
					num = 13;
					continue;
				}
				case 9:
					goto IL_14C;
				case 10:
					goto IL_7B;
				case 11:
					if (num2 < count)
					{
						num = 12;
						continue;
					}
					return builtInStyles;
				case 12:
					num = 0;
					continue;
				case 13:
					flag = true;
					goto IL_13C;
				}
				if (this.ᜄ.ᝀ())
				{
					num = 10;
					continue;
				}
				builtInStyles = this.ᜀ(0).BuiltInStyle;
				num2 = 1;
				count = this.ᜄ.Count;
				num = 4;
				continue;
				IL_CD:
				num = 8;
				continue;
				IL_13C:
				if (flag)
				{
					num = 2;
					continue;
				}
				num2++;
				num = 9;
				continue;
				IL_14C:
				num = 11;
			}
			IL_7B:
			return null;
		}
		}
	}

	// Token: 0x060028A3 RID: 10403 RVA: 0x00170F78 File Offset: 0x0016FF78
	public void ᜀ(BuiltInStyles? A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜄ.Count;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					this.ᜀ(num).BuiltInStyle = A_0;
					num++;
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
						num2 = 2;
						continue;
					}
					break;
				case 2:
					goto IL_30;
				case 3:
					goto IL_30;
				}
				break;
				IL_30:
				num2 = 1;
			}
		}
	}

	// Token: 0x060028A4 RID: 10404 RVA: 0x00171018 File Offset: 0x00170018
	public IXLSRange ᜉ()
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
		throw new NotSupportedException();
	}

	// Token: 0x060028A5 RID: 10405 RVA: 0x00171058 File Offset: 0x00170058
	public IXLSRange ᜄ(bool A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x060028A6 RID: 10406 RVA: 0x00171098 File Offset: 0x00170098
	public IXLSRange ᜃ(GroupByType A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x060028A7 RID: 10407 RVA: 0x001710D8 File Offset: 0x001700D8
	public IXLSRange ᜀ(GroupByType A_0, bool A_1)
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
		throw new NotSupportedException();
	}

	// Token: 0x060028A8 RID: 10408 RVA: 0x00171118 File Offset: 0x00170118
	public void ᜎ()
	{
		for (;;)
		{
			IL_18:
			int num = 0;
			int count = this.ᜄ.Count;
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8F:
				goto IL_5E;
			case 1:
				goto IL_4E;
			default:
				goto IL_4E;
			}
			int num2;
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					goto IL_5C;
				case 1:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					this.ᜀ(num).Merge();
					num++;
					num2 = 2;
					continue;
				case 2:
					goto IL_8F;
				case 3:
					return;
				}
				goto IL_18;
			}
			IL_5C:
			goto IL_5E;
			IL_4E:
			if (false)
			{
			}
			num2 = 0;
			goto IL_02;
			IL_5E:
			num2 = 1;
			goto IL_02;
		}
	}

	// Token: 0x060028A9 RID: 10409 RVA: 0x001711B8 File Offset: 0x001701B8
	public void ᜁ(bool A_0)
	{
		for (;;)
		{
			IL_18:
			int num = 0;
			int count = this.ᜄ.Count;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_90:
				goto IL_5E;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num2 = 3;
				break;
			}
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					return;
				case 1:
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					this.ᜀ(num).Merge(A_0);
					num++;
					num2 = 2;
					continue;
				case 2:
					goto IL_90;
				case 3:
					goto IL_5C;
				}
				goto IL_18;
			}
			IL_5C:
			IL_5E:
			num2 = 1;
			goto IL_02;
		}
	}

	// Token: 0x060028AA RID: 10410 RVA: 0x00171258 File Offset: 0x00170258
	public IXLSRange ᜂ(GroupByType A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x060028AB RID: 10411 RVA: 0x00171298 File Offset: 0x00170298
	public void ᝂ()
	{
		for (;;)
		{
			IL_18:
			int num = 0;
			int count = this.ᜄ.Count;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8F:
				goto IL_56;
			default:
				if (false)
				{
				}
				num2 = 1;
				break;
			}
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						num2 = 2;
						continue;
					}
					this.ᜀ(num).UnMerge();
					num++;
					num2 = 3;
					continue;
				case 1:
					goto IL_54;
				case 2:
					goto IL_6A;
				case 3:
					goto IL_8F;
				}
				goto IL_18;
			}
			IL_54:
			IL_56:
			num2 = 0;
			goto IL_02;
		}
		IL_6A:
		if (true)
		{
		}
	}

	// Token: 0x060028AC RID: 10412 RVA: 0x00171338 File Offset: 0x00170338
	public void ᜪ()
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
		throw new NotSupportedException();
	}

	// Token: 0x060028AD RID: 10413 RVA: 0x00171378 File Offset: 0x00170378
	public void ᝏ()
	{
		for (;;)
		{
			IL_18:
			if (true)
			{
			}
			int num = 0;
			int count = this.ᜄ.Count;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8F:
				goto IL_5E;
			case 1:
				goto IL_4E;
			default:
				goto IL_4E;
			}
			int num2;
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						num2 = 2;
						continue;
					}
					this.ᜀ(num).ClearContents();
					num++;
					num2 = 1;
					continue;
				case 1:
					goto IL_8F;
				case 2:
					return;
				case 3:
					goto IL_5C;
				}
				goto IL_18;
			}
			IL_5C:
			goto IL_5E;
			IL_4E:
			if (false)
			{
			}
			num2 = 3;
			goto IL_02;
			IL_5E:
			num2 = 0;
			goto IL_02;
		}
	}

	// Token: 0x060028AE RID: 10414 RVA: 0x00171418 File Offset: 0x00170418
	public void ᜇ(bool A_0)
	{
		for (;;)
		{
			IL_18:
			if (true)
			{
			}
			int num = 0;
			int count = this.ᜄ.Count;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_95:
				goto IL_5E;
			case 1:
				goto IL_4E;
			default:
				goto IL_4E;
			}
			int num2;
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						num2 = 2;
						continue;
					}
					((XlsRange)this.ᜀ(num)).Clear(A_0);
					num++;
					num2 = 3;
					continue;
				case 1:
					goto IL_5C;
				case 2:
					return;
				case 3:
					goto IL_95;
				}
				goto IL_18;
			}
			IL_5C:
			goto IL_5E;
			IL_4E:
			if (false)
			{
			}
			num2 = 1;
			goto IL_02;
			IL_5E:
			num2 = 0;
			goto IL_02;
		}
	}

	// Token: 0x060028AF RID: 10415 RVA: 0x001714BC File Offset: 0x001704BC
	public void ᜀ(MoveDirectionType A_0)
	{
		for (;;)
		{
			IL_18:
			int num = 0;
			int count = this.ᜄ.Count;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_95:
				goto IL_56;
			default:
				if (false)
				{
				}
				num2 = 1;
				break;
			}
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					((XlsRange)this.ᜀ(num)).Clear(A_0);
					num++;
					if (true)
					{
					}
					num2 = 2;
					continue;
				case 1:
					goto IL_54;
				case 2:
					goto IL_95;
				case 3:
					return;
				}
				goto IL_18;
			}
			IL_54:
			IL_56:
			num2 = 0;
			goto IL_02;
		}
	}

	// Token: 0x060028B0 RID: 10416 RVA: 0x00171560 File Offset: 0x00170560
	public void ᜀ(ExcelClearOptions A_0)
	{
		for (;;)
		{
			IL_18:
			int num = 0;
			int count = this.ᜄ.Count;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_90:
				goto IL_56;
			default:
				if (false)
				{
				}
				num2 = 2;
				break;
			}
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					goto IL_90;
				case 1:
					if (num >= count)
					{
						if (true)
						{
						}
						num2 = 3;
						continue;
					}
					this.ᜀ(num).Clear(A_0);
					num++;
					num2 = 0;
					continue;
				case 2:
					goto IL_54;
				case 3:
					return;
				}
				goto IL_18;
			}
			IL_54:
			IL_56:
			num2 = 1;
			goto IL_02;
		}
	}

	// Token: 0x060028B1 RID: 10417 RVA: 0x00171600 File Offset: 0x00170600
	public void ᜀ(MoveDirectionType A_0, CopyRangeOptions A_1)
	{
		for (;;)
		{
			IL_18:
			if (true)
			{
			}
			int num = 0;
			int count = this.ᜄ.Count;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_96:
				goto IL_5E;
			case 1:
				goto IL_4E;
			default:
				goto IL_4E;
			}
			int num2;
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					goto IL_5C;
				case 1:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					((XlsRange)this.ᜀ(num)).ᜀ(A_0, A_1);
					num++;
					num2 = 2;
					continue;
				case 2:
					goto IL_96;
				case 3:
					return;
				}
				goto IL_18;
			}
			IL_5C:
			goto IL_5E;
			IL_4E:
			if (false)
			{
			}
			num2 = 0;
			goto IL_02;
			IL_5E:
			num2 = 1;
			goto IL_02;
		}
	}

	// Token: 0x060028B2 RID: 10418 RVA: 0x001716A8 File Offset: 0x001706A8
	public void ᜀ(IXLSRange A_0)
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

	// Token: 0x060028B3 RID: 10419 RVA: 0x001716E8 File Offset: 0x001706E8
	public void ᜀ(IXLSRange A_0, bool A_1)
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
		throw new NotImplementedException();
	}

	// Token: 0x060028B4 RID: 10420 RVA: 0x00171728 File Offset: 0x00170728
	public IXLSRange ᜂ(IXLSRange A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x060028B5 RID: 10421 RVA: 0x00171768 File Offset: 0x00170768
	public IXLSRange ᜁ(IXLSRange A_0, bool A_1)
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
		throw new NotImplementedException();
	}

	// Token: 0x060028B6 RID: 10422 RVA: 0x001717A8 File Offset: 0x001707A8
	public IXLSRange ᜀ(IXLSRange A_0, CopyRangeOptions A_1)
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
		throw new NotImplementedException();
	}

	// Token: 0x060028B7 RID: 10423 RVA: 0x001717E8 File Offset: 0x001707E8
	public IXLSRange ᜁ(IXLSRange A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x060028B8 RID: 10424 RVA: 0x00171828 File Offset: 0x00170828
	public IXLSRange ᜃ(IXLSRange A_0)
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

	// Token: 0x060028B9 RID: 10425 RVA: 0x00171868 File Offset: 0x00170868
	public void \u1737()
	{
		for (;;)
		{
			IL_18:
			if (true)
			{
			}
			int num = 0;
			int count = this.ᜄ.Count;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8F:
				goto IL_5E;
			case 1:
				goto IL_4E;
			default:
				goto IL_4E;
			}
			int num2;
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					return;
				case 1:
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					this.ᜀ(num).AutoFitRows();
					num++;
					num2 = 3;
					continue;
				case 2:
					goto IL_5C;
				case 3:
					goto IL_8F;
				}
				goto IL_18;
			}
			IL_5C:
			goto IL_5E;
			IL_4E:
			if (false)
			{
			}
			num2 = 2;
			goto IL_02;
			IL_5E:
			num2 = 1;
			goto IL_02;
		}
	}

	// Token: 0x060028BA RID: 10426 RVA: 0x00171908 File Offset: 0x00170908
	public void ᜡ()
	{
		for (;;)
		{
			IL_18:
			int num = 0;
			int count = this.ᜄ.Count;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8F:
				goto IL_56;
			default:
				if (false)
				{
				}
				num2 = 3;
				break;
			}
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					goto IL_8F;
				case 1:
					if (num >= count)
					{
						if (true)
						{
						}
						num2 = 2;
						continue;
					}
					this.ᜀ(num).AutoFitColumns();
					num++;
					num2 = 0;
					continue;
				case 2:
					return;
				case 3:
					goto IL_54;
				}
				goto IL_18;
			}
			IL_54:
			IL_56:
			num2 = 1;
			goto IL_02;
		}
	}

	// Token: 0x060028BB RID: 10427 RVA: 0x001719A8 File Offset: 0x001709A8
	public ICommentShape ᜫ()
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
		throw new NotSupportedException();
	}

	// Token: 0x060028BC RID: 10428 RVA: 0x001719E8 File Offset: 0x001709E8
	public IXLSRange ᜀ(string A_0, FindType A_1)
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
		throw new NotImplementedException();
	}

	// Token: 0x060028BD RID: 10429 RVA: 0x00171A28 File Offset: 0x00170A28
	private IXLSRange ᜀ(double A_0, FindType A_1)
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

	// Token: 0x060028BE RID: 10430 RVA: 0x00171A68 File Offset: 0x00170A68
	private IXLSRange ᜀ(bool A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x060028BF RID: 10431 RVA: 0x00171AA8 File Offset: 0x00170AA8
	private IXLSRange ᜀ(DateTime A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x060028C0 RID: 10432 RVA: 0x00171AE8 File Offset: 0x00170AE8
	public IXLSRange ᜁ(TimeSpan A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x060028C1 RID: 10433 RVA: 0x00171B28 File Offset: 0x00170B28
	public IXLSRange[] ᜁ(string A_0, FindType A_1)
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
		throw new NotImplementedException();
	}

	// Token: 0x060028C2 RID: 10434 RVA: 0x00171B68 File Offset: 0x00170B68
	public IXLSRange[] ᜁ(double A_0, FindType A_1)
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
		throw new NotImplementedException();
	}

	// Token: 0x060028C3 RID: 10435 RVA: 0x00171BA8 File Offset: 0x00170BA8
	public IXLSRange[] ᜈ(bool A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x060028C4 RID: 10436 RVA: 0x00171BE8 File Offset: 0x00170BE8
	public IXLSRange[] ᜁ(DateTime A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x060028C5 RID: 10437 RVA: 0x00171C28 File Offset: 0x00170C28
	public IXLSRange[] ᜂ(TimeSpan A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x060028C6 RID: 10438 RVA: 0x00171C68 File Offset: 0x00170C68
	public void ᝎ()
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
		throw new NotSupportedException();
	}

	// Token: 0x060028C7 RID: 10439 RVA: 0x00171CA8 File Offset: 0x00170CA8
	public void ᝍ()
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
		this.ᜁ(LineStyleType.Thin);
	}

	// Token: 0x060028C8 RID: 10440 RVA: 0x00171CEC File Offset: 0x00170CEC
	public void ᜁ(LineStyleType A_0)
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
		this.ᜁ(A_0, ExcelColors.Black);
	}

	// Token: 0x060028C9 RID: 10441 RVA: 0x00171D30 File Offset: 0x00170D30
	public void ᜁ(LineStyleType A_0, Color A_1)
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
		ExcelColors nearestColor = this.ᜢ().GetNearestColor(A_1);
		this.ᜁ(A_0, nearestColor);
	}

	// Token: 0x060028CA RID: 10442 RVA: 0x00171D80 File Offset: 0x00170D80
	public void ᜁ(LineStyleType A_0, ExcelColors A_1)
	{
		for (;;)
		{
			IL_18:
			int num = 0;
			int count = this.ᜄ.Count;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_91:
				goto IL_56;
			default:
				if (false)
				{
				}
				num2 = 1;
				break;
			}
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_54;
				case 2:
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					this.ᜀ(num).BorderAround(A_0, A_1);
					num++;
					if (true)
					{
					}
					num2 = 3;
					continue;
				case 3:
					goto IL_91;
				}
				goto IL_18;
			}
			IL_54:
			IL_56:
			num2 = 2;
			goto IL_02;
		}
	}

	// Token: 0x060028CB RID: 10443 RVA: 0x00171E20 File Offset: 0x00170E20
	public void ᜧ()
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
		this.ᜀ(LineStyleType.Thin);
	}

	// Token: 0x060028CC RID: 10444 RVA: 0x00171E64 File Offset: 0x00170E64
	public void ᜀ(LineStyleType A_0)
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
		this.ᜀ(A_0, ExcelColors.Black);
	}

	// Token: 0x060028CD RID: 10445 RVA: 0x00171EA8 File Offset: 0x00170EA8
	public void ᜀ(LineStyleType A_0, Color A_1)
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
		ExcelColors nearestColor = this.ᜢ().GetNearestColor(A_1);
		this.ᜀ(A_0, nearestColor);
	}

	// Token: 0x060028CE RID: 10446 RVA: 0x00171EF8 File Offset: 0x00170EF8
	public void ᜀ(LineStyleType A_0, ExcelColors A_1)
	{
		for (;;)
		{
			IL_18:
			if (true)
			{
			}
			int num = 0;
			int count = this.ᜄ.Count;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_91:
				goto IL_5E;
			case 1:
				goto IL_4E;
			default:
				goto IL_4E;
			}
			int num2;
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					goto IL_91;
				case 1:
					goto IL_5C;
				case 2:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					this.ᜀ(num).BorderInside(A_0, A_1);
					num++;
					num2 = 0;
					continue;
				case 3:
					return;
				}
				goto IL_18;
			}
			IL_5C:
			goto IL_5E;
			IL_4E:
			if (false)
			{
			}
			num2 = 1;
			goto IL_02;
			IL_5E:
			num2 = 2;
			goto IL_02;
		}
	}

	// Token: 0x060028CF RID: 10447 RVA: 0x00171F98 File Offset: 0x00170F98
	public void ᜇ()
	{
		for (;;)
		{
			IL_18:
			int num = 0;
			int count = this.ᜄ.Count;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8F:
				goto IL_56;
			default:
				if (false)
				{
				}
				num2 = 2;
				break;
			}
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					goto IL_8F;
				case 1:
					return;
				case 2:
					goto IL_54;
				case 3:
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					this.ᜀ(num).BorderNone();
					num++;
					num2 = 0;
					continue;
				}
				goto IL_18;
			}
			IL_54:
			IL_56:
			if (true)
			{
			}
			num2 = 3;
			goto IL_02;
		}
	}

	// Token: 0x060028D0 RID: 10448 RVA: 0x00172038 File Offset: 0x00171038
	public IEnumerator GetEnumerator()
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
		throw new NotImplementedException();
	}

	// Token: 0x0400137B RID: 4987
	protected int ᜀ;

	// Token: 0x0400137C RID: 4988
	protected int ᜁ;

	// Token: 0x0400137D RID: 4989
	protected int ᜂ;

	// Token: 0x0400137E RID: 4990
	protected int ᜃ;

	// Token: 0x0400137F RID: 4991
	private spr\u233D ᜄ;

	// Token: 0x04001380 RID: 4992
	private spr\u1C7E ᜅ;

	// Token: 0x04001381 RID: 4993
	private spr\u1CCF ᜆ;

	// Token: 0x04001382 RID: 4994
	protected sprᴖ ᜇ;
}
