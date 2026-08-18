using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000306 RID: 774
[DefaultMember("Item")]
internal class spr\u1FBC : XlsObject, ICloneParent
{
	// Token: 0x06002F9E RID: 12190 RVA: 0x001B0A5C File Offset: 0x001AFA5C
	public spr\u1FBC(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜀ();
	}

	// Token: 0x06002F9F RID: 12191 RVA: 0x001B0A94 File Offset: 0x001AFA94
	public void ᜀ(RecordArrayList A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				int num2;
				spr\u25A6.ᜀ[] array;
				int num4;
				int num5;
				switch (num)
				{
				case 0:
					goto IL_AA;
				case 1:
					num2 = this.ᜅ();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AA;
					}
					if (false)
					{
					}
					num = 3;
					continue;
				case 2:
					goto IL_14D;
				case 3:
				{
					if (num2 < 0)
					{
						num = 8;
						continue;
					}
					array = new spr\u25A6.ᜀ[num2];
					int num3 = 0;
					num = 2;
					continue;
				}
				case 4:
					goto IL_10E;
				case 5:
					num4 = 1027;
					num = 4;
					continue;
				case 6:
					num5 = 0;
					num = 10;
					continue;
				case 8:
					return;
				case 9:
					goto IL_14D;
				case 10:
					if (true)
					{
					}
					goto IL_9E;
				case 11:
					if (num4 > 1027)
					{
						num = 5;
						continue;
					}
					goto IL_10E;
				case 12:
				{
					int num3;
					if (num3 >= num2)
					{
						num = 6;
						continue;
					}
					Rectangle a_ = this.ᜃ[num3];
					spr\u25A6.ᜀ ᜀ = this.ᜄ(a_);
					array[num3] = ᜀ;
					num3++;
					num = 9;
					continue;
				}
				case 13:
					goto IL_9E;
				case 14:
					return;
				}
				if (this.ᜂ)
				{
					num = 1;
					continue;
				}
				goto IL_1E4;
				IL_9E:
				num = 0;
				continue;
				IL_AA:
				if (num5 == num2)
				{
					num = 14;
					continue;
				}
				num4 = num2 - num5;
				num = 11;
				continue;
				IL_10E:
				spr\u25A6 spr_u25A = (spr\u25A6)spr\u175E.ᜀ(TBIFFRecord.MergeCells);
				spr_u25A.ᜀ(num5, num4, array);
				A_0.ᜀ(spr_u25A);
				num5 += num4;
				num = 13;
				continue;
				IL_14D:
				num = 12;
			}
			return;
			IL_1E4:
			A_0.AddRange(this.ᜁ);
			return;
		}
		}
	}

	// Token: 0x06002FA0 RID: 12192 RVA: 0x001B0C94 File Offset: 0x001AFC94
	internal int ᜅ()
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
		this.ᜂ();
		return this.ᜃ.Count;
	}

	// Token: 0x06002FA1 RID: 12193 RVA: 0x001B0CE0 File Offset: 0x001AFCE0
	internal List<Rectangle> ᜄ()
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
		this.ᜂ();
		return this.ᜃ;
	}

	// Token: 0x06002FA2 RID: 12194 RVA: 0x001B0D28 File Offset: 0x001AFD28
	private void ᜀ()
	{
		int a_ = 17;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_43;
		}
		if (false)
		{
		}
		object obj = base.FindParent(typeof(XlsWorksheet));
		if (obj != null)
		{
			this.ᜀ = (XlsWorksheet)obj;
			return;
		}
		IL_43:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("ᝆ⡈㥊⡌ⅎ═獒㩔㕖㍘㹚㹜⭞䅠bѤ०ݨѪᥬ佮፰ᙲ啴ᅶᙸ๺፼᭾꾀", a_));
	}

	// Token: 0x06002FA3 RID: 12195 RVA: 0x001B0DA4 File Offset: 0x001AFDA4
	internal spr\u25A6.ᜀ ᜂ(Rectangle A_0)
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
		return this.ᜃ(A_0);
	}

	// Token: 0x06002FA4 RID: 12196 RVA: 0x001B0DE8 File Offset: 0x001AFDE8
	internal Rectangle ᜂ(int A_0)
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
		this.ᜂ();
		return this.ᜃ[A_0];
	}

	// Token: 0x06002FA5 RID: 12197 RVA: 0x001B0E38 File Offset: 0x001AFE38
	public IList<spr\u192F> ᜁ()
	{
		switch (0)
		{
		default:
		{
			IList<spr\u192F> list;
			for (;;)
			{
				this.ᜂ();
				list = new List<spr\u192F>();
				XlsCellRecordCollection cellRecords = this.ᜀ.CellRecords;
				int num = 0;
				int num2 = this.ᜅ();
				if (true)
				{
				}
				int num3;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_99:
					Rectangle a_ = this.ᜃ[num];
					spr\u25A6.ᜀ a_2 = this.ᜄ(a_);
					spr\u192F item = this.ᜀ(a_2);
					list.Add(item);
					num++;
					num3 = 3;
					break;
				}
				default:
					if (false)
					{
					}
					num3 = 1;
					break;
				}
				for (;;)
				{
					switch (num3)
					{
					case 0:
						return list;
					case 1:
						goto IL_77;
					case 2:
						if (num >= num2)
						{
							num3 = 0;
							continue;
						}
						goto IL_99;
					case 3:
						goto IL_77;
					}
					break;
					IL_77:
					num3 = 2;
				}
			}
			return list;
		}
		}
	}

	// Token: 0x06002FA6 RID: 12198 RVA: 0x001B0F1C File Offset: 0x001AFF1C
	public spr\u192F ᜀ(spr\u25A6.ᜀ A_0)
	{
		switch (0)
		{
		default:
		{
			XlsWorkbook xlsWorkbook;
			int num;
			int num2;
			for (;;)
			{
				xlsWorkbook = (XlsWorkbook)this.ᜀ.Workbook;
				XlsCellRecordCollection cellRecords = this.ᜀ.CellRecords;
				num = cellRecords.GetExtendedFormatIndex(A_0.ᜂ() + 1, A_0.ᜅ() + 1);
				num2 = cellRecords.GetExtendedFormatIndex(A_0.ᜇ() + 1, A_0.ᜃ() + 1);
				int num3 = 1;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						num2 = this.ᜀ.ParentWorkbook.DefaultXFIndex;
						num3 = 2;
						continue;
					case 1:
						if (num < 0)
						{
							goto IL_87;
						}
						goto IL_D5;
					case 2:
						goto IL_CB;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_87;
						default:
							if (false)
							{
							}
							num = xlsWorkbook.DefaultXFIndex;
							num3 = 4;
							continue;
						}
						break;
					case 4:
						goto IL_D5;
					case 5:
						if (num2 < 0)
						{
							num3 = 0;
							continue;
						}
						goto IL_112;
					}
					break;
					IL_87:
					num3 = 3;
					continue;
					IL_D5:
					num3 = 5;
				}
			}
			IL_CB:
			if (true)
			{
			}
			IL_112:
			sprᢖ sprᢖ = xlsWorkbook.InnerExtFormats;
			return sprᢖ.ᜀ(num, num2);
		}
		}
	}

	// Token: 0x06002FA7 RID: 12199 RVA: 0x001B1050 File Offset: 0x001B0050
	public void ᜀ(XlsRange A_0, MergeOperationType A_1)
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
		int a_ = A_0.FirstRow - 1;
		int a_2 = A_0.FirstColumn - 1;
		int a_3 = A_0.LastRow - 1;
		int a_4 = A_0.LastColumn - 1;
		this.ᜂ();
		this.ᜀ(a_, a_3, a_2, a_4, A_1);
	}

	// Token: 0x06002FA8 RID: 12200 RVA: 0x001B10C0 File Offset: 0x001B00C0
	private void ᜀ(spr\u25A6.ᜀ A_0, MergeOperationType A_1)
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
		int a_ = A_0.ᜂ();
		int a_2 = A_0.ᜅ();
		int a_3 = A_0.ᜇ();
		int a_4 = A_0.ᜃ();
		this.ᜀ(a_, a_3, a_2, a_4, A_1);
	}

	// Token: 0x06002FA9 RID: 12201 RVA: 0x001B1124 File Offset: 0x001B0124
	public void ᜀ(int A_0, int A_1, int A_2, int A_3, MergeOperationType A_4)
	{
		Rectangle rectangle;
		for (;;)
		{
			for (;;)
			{
				rectangle = new Rectangle(A_2, A_0, A_3 - A_2, A_1 - A_0);
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
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							this.ᜀ(rectangle);
							num = 1;
							continue;
						case 1:
							goto IL_6E;
						case 2:
							if (A_4 == MergeOperationType.Delete)
							{
								num = 0;
								continue;
							}
							goto IL_7A;
						}
						break;
					}
					break;
				}
				}
			}
		}
		IL_6E:
		IL_7A:
		this.ᜃ.Add(rectangle);
	}

	// Token: 0x06002FAA RID: 12202 RVA: 0x001B11B8 File Offset: 0x001B01B8
	internal void ᜀ(Rectangle A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜂ();
				List<Rectangle> list = new List<Rectangle>();
				int num = 0;
				int num2 = this.ᜅ();
				int num3 = 1;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						if (num >= num2)
						{
							goto IL_B7;
						}
						Rectangle rectangle = this.ᜃ[num];
						num3 = 2;
						continue;
					}
					case 1:
						goto IL_A7;
					case 2:
					{
						Rectangle rectangle;
						if (UtilityMethods.ᜀ(rectangle, A_0))
						{
							num3 = 10;
							continue;
						}
						goto IL_152;
					}
					case 3:
						return;
					case 4:
					{
						int num4 = 0;
						int count = list.Count;
						num3 = 6;
						continue;
					}
					case 5:
						goto IL_DA;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B7;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							goto IL_DA;
						}
						break;
					case 7:
					{
						int num4;
						int count;
						if (num4 >= count)
						{
							num3 = 3;
							continue;
						}
						this.ᜃ.Remove(list[num4]);
						num4++;
						num3 = 5;
						continue;
					}
					case 8:
						goto IL_152;
					case 9:
						goto IL_A7;
					case 10:
					{
						Rectangle rectangle;
						list.Add(rectangle);
						num3 = 8;
						continue;
					}
					}
					break;
					IL_A7:
					num3 = 0;
					continue;
					IL_B7:
					num3 = 4;
					continue;
					IL_DA:
					num3 = 7;
					continue;
					IL_152:
					num++;
					num3 = 9;
				}
			}
			return;
		}
	}

	// Token: 0x06002FAB RID: 12203 RVA: 0x001B132C File Offset: 0x001B032C
	public void ᜃ()
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
		this.ᜃ.Clear();
		this.ᜁ = null;
		this.ᜂ = true;
	}

	// Token: 0x06002FAC RID: 12204 RVA: 0x001B1380 File Offset: 0x001B0380
	public void ᜀ(spr\u25A6 A_0)
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
		this.ᜁ.Add(A_0);
		this.ᜂ = false;
	}

	// Token: 0x06002FAD RID: 12205 RVA: 0x001B13D0 File Offset: 0x001B03D0
	public void ᜂ()
	{
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
					goto IL_5E;
				case 1:
					goto IL_C2;
				case 2:
					goto IL_DE;
				case 3:
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AA;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 4:
					goto IL_C2;
				case 5:
				{
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					spr\u25A6 spr_u25A = this.ᜁ[num2];
					spr\u25A6.ᜀ[] array = spr_u25A.ᜃ();
					int num3 = 0;
					int num4 = array.Length;
					num = 0;
					continue;
				}
				case 7:
				{
					int num3;
					int num4;
					if (num3 >= num4)
					{
						num = 3;
						continue;
					}
					spr\u25A6.ᜀ[] array;
					this.ᜀ(array[num3], MergeOperationType.Leave);
					num3++;
					num = 9;
					continue;
				}
				case 8:
					return;
				case 9:
					goto IL_5E;
				}
				if (this.ᜂ)
				{
					num = 8;
					continue;
				}
				if (true)
				{
				}
				num2 = 0;
				count = this.ᜁ.Count;
				goto IL_AA;
				IL_5E:
				num = 7;
				continue;
				IL_AA:
				num = 4;
				continue;
				IL_C2:
				num = 5;
			}
			return;
			IL_DE:
			this.ᜂ = true;
			this.ᜁ = null;
			return;
		}
		}
	}

	// Token: 0x06002FAE RID: 12206 RVA: 0x001B1528 File Offset: 0x001B0528
	public void ᜁ(int A_0)
	{
		int a_ = 17;
		if (true)
		{
		}
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_9F;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_35;
				default:
					if (false)
					{
					}
					if (A_0 > this.ᜀ.Workbook.MaxRowCount)
					{
						num = 1;
						continue;
					}
					goto IL_A1;
				}
				break;
			case 3:
				num = 2;
				continue;
			}
			goto IL_31;
			IL_35:
			num = 3;
			continue;
			IL_31:
			if (A_0 >= 1)
			{
				goto IL_35;
			}
			break;
		}
		IL_3F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㕆♈㱊ьⅎ㕐㙒ⵔ", a_));
		IL_9F:
		goto IL_3F;
		IL_A1:
		this.ᜁ(A_0, true, 1);
	}

	// Token: 0x06002FAF RID: 12207 RVA: 0x001B15E0 File Offset: 0x001B05E0
	public void ᜃ(int A_0, int A_1)
	{
		int a_ = 12;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2D;
				default:
					if (false)
					{
					}
					if (A_0 > this.ᜀ.Workbook.MaxRowCount)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					goto IL_A1;
				}
				break;
			case 2:
				goto IL_9F;
			}
			goto IL_29;
			IL_2D:
			num = 0;
			continue;
			IL_29:
			if (A_0 >= 1)
			{
				goto IL_2D;
			}
			break;
		}
		IL_37:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ぁ⭃ㅅŇ⑉⡋⭍⡏", a_));
		IL_9F:
		goto IL_37;
		IL_A1:
		this.ᜁ(A_0, true, A_1);
	}

	// Token: 0x06002FB0 RID: 12208 RVA: 0x001B1698 File Offset: 0x001B0698
	public void ᜁ(int A_0, int A_1)
	{
		int a_ = 4;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2D;
				default:
					if (false)
					{
					}
					if (A_0 > this.ᜀ.Workbook.MaxRowCount)
					{
						num = 3;
						continue;
					}
					goto IL_A1;
				}
				break;
			case 3:
				goto IL_9F;
			}
			goto IL_29;
			IL_2D:
			if (true)
			{
			}
			num = 0;
			continue;
			IL_29:
			if (A_0 >= 1)
			{
				goto IL_2D;
			}
			break;
		}
		IL_3F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䠹医䤽िⱁ⁃⍅ぇ", a_));
		IL_9F:
		goto IL_3F;
		IL_A1:
		this.ᜁ(A_0, false, A_1);
	}

	// Token: 0x06002FB1 RID: 12209 RVA: 0x001B1750 File Offset: 0x001B0750
	public void ᜃ(int A_0)
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
		this.ᜀ(A_0, true, 1);
	}

	// Token: 0x06002FB2 RID: 12210 RVA: 0x001B1794 File Offset: 0x001B0794
	public void ᜄ(int A_0, int A_1)
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
		this.ᜀ(A_0, true, A_1);
	}

	// Token: 0x06002FB3 RID: 12211 RVA: 0x001B17D8 File Offset: 0x001B07D8
	public void ᜀ(int A_0)
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
		this.ᜀ(A_0, false, 1);
	}

	// Token: 0x06002FB4 RID: 12212 RVA: 0x001B181C File Offset: 0x001B081C
	public void ᜀ(int A_0, int A_1)
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
		this.ᜀ(A_0, false, A_1);
	}

	// Token: 0x06002FB5 RID: 12213 RVA: 0x001B1860 File Offset: 0x001B0860
	protected void ᜁ(int A_0, bool A_1, int A_2)
	{
		int a_ = 17;
		switch (0)
		{
		default:
			for (;;)
			{
				A_0--;
				int num = 5;
				for (;;)
				{
					if (true)
					{
					}
					List<Rectangle> list;
					int num2;
					spr\u25A6.ᜀ ᜀ;
					switch (num)
					{
					case 0:
						goto IL_15C;
					case 1:
						goto IL_111;
					case 2:
					{
						if (A_0 > this.ᜀ.Workbook.MaxRowCount)
						{
							num = 6;
							continue;
						}
						this.ᜂ();
						list = this.ᜃ;
						this.ᜃ = new List<Rectangle>();
						num2 = 0;
						int count = list.Count;
						num = 3;
						continue;
					}
					case 3:
						goto IL_111;
					case 4:
					{
						int count;
						if (num2 >= count)
						{
							num = 9;
							continue;
						}
						goto IL_B7;
					}
					case 5:
						if (A_0 >= 0)
						{
							num = 10;
							continue;
						}
						goto IL_FD;
					case 6:
						goto IL_15A;
					case 7:
						if (ᜀ != null)
						{
							num = 8;
							continue;
						}
						goto IL_15C;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B7;
						default:
							if (false)
							{
							}
							this.ᜀ(ᜀ, MergeOperationType.Delete);
							num = 0;
							continue;
						}
						break;
					case 9:
						return;
					case 10:
						num = 2;
						continue;
					}
					break;
					IL_B7:
					Rectangle a_2 = list[num2];
					ᜀ = this.ᜄ(a_2);
					ᜀ = spr\u1FBC.ᜁ(ᜀ, A_0, A_1, A_2, this.ᜀ.Workbook);
					num = 7;
					continue;
					IL_111:
					num = 4;
					continue;
					IL_15C:
					num2++;
					num = 1;
				}
			}
			IL_FD:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㕆♈㱊ьⅎ㕐㙒ⵔ", a_));
			IL_15A:
			goto IL_FD;
		}
	}

	// Token: 0x06002FB6 RID: 12214 RVA: 0x001B1A10 File Offset: 0x001B0A10
	protected void ᜀ(int A_0, bool A_1, int A_2)
	{
		int a_ = 12;
		switch (0)
		{
		default:
			for (;;)
			{
				A_0--;
				int num = 0;
				for (;;)
				{
					List<Rectangle> list;
					int num2;
					int count;
					spr\u25A6.ᜀ ᜀ;
					switch (num)
					{
					case 0:
						if (A_0 >= 0)
						{
							num = 6;
							continue;
						}
						goto IL_C9;
					case 1:
						if (A_2 >= 0)
						{
							num = 5;
							continue;
						}
						goto IL_1FB;
					case 2:
						if (A_2 >= this.ᜀ.Workbook.MaxColumnCount)
						{
							num = 9;
							continue;
						}
						this.ᜂ();
						list = this.ᜃ;
						this.ᜃ = new List<Rectangle>();
						num2 = 0;
						count = list.Count;
						num = 13;
						continue;
					case 3:
						goto IL_193;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_193;
						default:
							if (false)
							{
							}
							this.ᜀ(ᜀ, MergeOperationType.Delete);
							num = 7;
							continue;
						}
						break;
					case 5:
						if (true)
						{
						}
						num = 2;
						continue;
					case 6:
						num = 12;
						continue;
					case 7:
						goto IL_DD;
					case 8:
						return;
					case 9:
						goto IL_1DB;
					case 10:
						if (ᜀ != null)
						{
							num = 4;
							continue;
						}
						goto IL_DD;
					case 11:
						goto IL_187;
					case 12:
						if (A_0 >= this.ᜀ.Workbook.MaxColumnCount)
						{
							num = 14;
							continue;
						}
						num = 1;
						continue;
					case 13:
						goto IL_187;
					case 14:
						goto IL_120;
					}
					break;
					IL_DD:
					num2++;
					num = 11;
					continue;
					IL_193:
					if (num2 >= count)
					{
						num = 8;
						continue;
					}
					Rectangle a_2 = list[num2];
					ᜀ = this.ᜄ(a_2);
					ᜀ = spr\u1FBC.ᜀ(ᜀ, A_0, A_1, 1, this.ᜀ.Workbook);
					num = 10;
					continue;
					IL_187:
					num = 3;
				}
			}
			IL_C9:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⅁⭃⩅㵇❉≋ݍ㹏㙑ㅓ⹕", a_));
			IL_120:
			goto IL_C9;
			IL_1DB:
			IL_1FB:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⅁⭃㍅♇㹉", a_));
		}
	}

	// Token: 0x06002FB7 RID: 12215 RVA: 0x001B1C2C File Offset: 0x001B0C2C
	public void ᜀ(IXLSRange A_0, IXLSRange A_1, bool A_2)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 6;
			int a_2;
			int a_3;
			List<spr\u25A6.ᜀ> list;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					this.ᜂ();
					a_2 = A_0.Row - A_1.Row;
					a_3 = A_0.Column - A_1.Column;
					list = new List<spr\u25A6.ᜀ>();
					this.ᜀ(A_1, list);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D3;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					if (A_2)
					{
						num = 4;
						continue;
					}
					goto IL_126;
				case 2:
					goto IL_FA;
				case 3:
					goto IL_54;
				case 4:
					this.ᜀ(list);
					num = 2;
					continue;
				case 5:
					goto IL_124;
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					num = 0;
				}
			}
			IL_54:
			IL_D3:
			throw new ArgumentNullException(RecordTableEnumerator.b("≅ⵇ㥉㡋❍㹏㍑⁓㽕㝗㑙", a_));
			IL_FA:
			goto IL_126;
			IL_124:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕅❇㽉㹋ⵍ㕏", a_));
			IL_126:
			if (true)
			{
			}
			XlsWorksheet xlsWorksheet = (XlsWorksheet)A_1.Worksheet;
			xlsWorksheet.MergeCells.ᜀ(list, a_2, a_3);
			return;
		}
		}
	}

	// Token: 0x06002FB8 RID: 12216 RVA: 0x001B1D84 File Offset: 0x001B0D84
	[CLSCompliant(false)]
	public List<spr\u25A6.ᜀ> ᜀ(IXLSRange A_0, bool A_1)
	{
		List<spr\u25A6.ᜀ> list;
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_50:
				if (!A_1)
				{
					return list;
				}
				num = 0;
				break;
			default:
				if (false)
				{
				}
				list = new List<spr\u25A6.ᜀ>();
				this.ᜀ(A_0, list);
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ(list);
					num = 1;
					continue;
				case 1:
					goto IL_6C;
				case 2:
					goto IL_50;
				}
				break;
			}
		}
		IL_6C:
		if (true)
		{
		}
		return list;
	}

	// Token: 0x06002FB9 RID: 12217 RVA: 0x001B1E08 File Offset: 0x001B0E08
	internal void ᜀ(IXLSRange A_0, List<spr\u25A6.ᜀ> A_1)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			spr\u25A6.ᜀ ᜀ;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_D8:
				A_1.Add(ᜀ);
				num = 3;
				break;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
			for (;;)
			{
				int num4;
				switch (num)
				{
				case 1:
					goto IL_95;
				case 2:
					goto IL_13B;
				case 3:
					goto IL_9A;
				case 4:
					goto IL_D8;
				case 5:
					return;
				case 6:
					if (true)
					{
					}
					num = 9;
					continue;
				case 7:
					goto IL_13B;
				case 8:
				{
					int num2;
					int num3;
					if (num2 > num3)
					{
						num = 5;
						continue;
					}
					int num5;
					num4 = num5;
					num = 15;
					continue;
				}
				case 9:
					if (!A_1.Contains(ᜀ))
					{
						num = 4;
						continue;
					}
					goto IL_9A;
				case 10:
					if (ᜀ != null)
					{
						num = 6;
						continue;
					}
					goto IL_9A;
				case 11:
					goto IL_18C;
				case 12:
				{
					int num2;
					num2++;
					num = 7;
					continue;
				}
				case 13:
					goto IL_120;
				case 14:
				{
					int num6;
					if (num4 > num6)
					{
						num = 12;
						continue;
					}
					int num2;
					Rectangle a_2 = new Rectangle(num4, num2, 0, 0);
					ᜀ = this.ᜃ(a_2);
					num = 10;
					continue;
				}
				case 15:
					goto IL_18C;
				case 16:
				{
					if (A_1 == null)
					{
						num = 13;
						continue;
					}
					this.ᜂ();
					int num7 = A_0.Row - 1;
					int num5 = A_0.Column - 1;
					int num3 = A_0.LastRow - 1;
					int num6 = A_0.LastColumn - 1;
					int num2 = num7;
					num = 2;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num = 16;
				continue;
				IL_9A:
				num4++;
				num = 11;
				continue;
				IL_13B:
				num = 8;
				continue;
				IL_18C:
				num = 14;
			}
			IL_95:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿⍁⩃ⅅⵇ", a_));
			IL_120:
			throw new ArgumentNullException(RecordTableEnumerator.b("⠿⍁㝃⹅ᩇ⽉⭋❍㽏㱑❓", a_));
		}
		}
	}

	// Token: 0x06002FBA RID: 12218 RVA: 0x001B203C File Offset: 0x001B103C
	private static void ᜀ(spr\u25A6.ᜀ A_0, IXLSRange A_1)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 9;
			for (;;)
			{
				IL_22:
				switch (num)
				{
				case 0:
					num = 7;
					continue;
				case 1:
					num = 5;
					continue;
				case 2:
				{
					if (A_1 == null)
					{
						num = 10;
						continue;
					}
					int row = A_1.Row;
					int column = A_1.Column;
					int lastRow = A_1.LastRow;
					int lastColumn = A_1.LastColumn;
					num = 4;
					continue;
				}
				case 3:
					goto IL_65;
				case 4:
				{
					int column;
					if (A_0.ᜅ() >= column)
					{
						num = 1;
						continue;
					}
					goto IL_F0;
				}
				case 5:
				{
					int lastColumn;
					if (A_0.ᜃ() <= lastColumn)
					{
						num = 0;
						continue;
					}
					goto IL_F0;
				}
				case 6:
					num = 8;
					continue;
				case 7:
				{
					int row;
					while (A_0.ᜂ() >= row)
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
							num = 6;
							goto IL_22;
						}
					}
					goto IL_F0;
				}
				case 8:
				{
					if (true)
					{
					}
					int lastRow;
					if (A_0.ᜇ() > lastRow)
					{
						num = 11;
						continue;
					}
					return;
				}
				case 10:
					goto IL_EB;
				case 11:
					goto IL_141;
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					num = 2;
				}
			}
			IL_65:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿❁⍃⽅❇⑉", a_));
			IL_EB:
			throw new ArgumentNullException(RecordTableEnumerator.b("㈿⍁⩃ⅅⵇ", a_));
			IL_F0:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㈿❁⍃⽅❇⑉", a_));
			IL_141:
			goto IL_F0;
		}
		}
	}

	// Token: 0x06002FBB RID: 12219 RVA: 0x001B21E8 File Offset: 0x001B11E8
	private void ᜀ(List<spr\u25A6.ᜀ> A_0)
	{
		int a_ = 19;
		int num = 2;
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
				List<spr\u25A6.ᜀ>.Enumerator enumerator;
				switch (num)
				{
				case 0:
					goto IL_59;
				case 1:
					if (true)
					{
					}
					try
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								spr\u25A6.ᜀ ᜀ = enumerator.Current;
								Rectangle item = Rectangle.FromLTRB(ᜀ.ᜅ(), ᜀ.ᜂ(), ᜀ.ᜃ(), ᜀ.ᜇ());
								this.ᜃ.Remove(item);
								num = 4;
								continue;
							}
							case 1:
								goto IL_E4;
							case 3:
								num = 1;
								continue;
							}
							IL_C1:
							num = 0;
							continue;
							goto IL_C1;
						}
						IL_E4:
						return;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_F4;
				}
				goto IL_41;
				IL_F4:
				this.ᜂ();
				enumerator = A_0.GetEnumerator();
				num = 1;
				continue;
				IL_41:
				if (A_0 != null)
				{
					goto IL_F4;
				}
				break;
			}
			}
			num = 0;
		}
		IL_59:
		throw new ArgumentNullException(RecordTableEnumerator.b("╈㡊㥌ᵎ㑐㑒㱔㡖㝘⡚", a_));
	}

	// Token: 0x06002FBC RID: 12220 RVA: 0x001B2334 File Offset: 0x001B1334
	internal void ᜀ(List<spr\u25A6.ᜀ> A_0, int A_1, int A_2)
	{
		int a_ = 11;
		int num = 1;
		for (;;)
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
			{
				if (false)
				{
				}
				List<spr\u25A6.ᜀ>.Enumerator enumerator;
				switch (num)
				{
				case 0:
					goto IL_61;
				case 2:
					try
					{
						num = 4;
						for (;;)
						{
							switch (num)
							{
							case 1:
							{
								if (!enumerator.MoveNext())
								{
									num = 2;
									continue;
								}
								spr\u25A6.ᜀ ᜀ = enumerator.Current;
								ᜀ.ᜀ(A_1, A_2);
								this.ᜀ(ᜀ, MergeOperationType.Delete);
								num = 0;
								continue;
							}
							case 2:
								num = 3;
								continue;
							case 3:
								goto IL_D1;
							}
							IL_AE:
							num = 1;
							continue;
							goto IL_AE;
						}
						IL_D1:
						return;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_E1;
				}
				goto IL_53;
				IL_E1:
				this.ᜂ();
				enumerator = A_0.GetEnumerator();
				num = 2;
				continue;
				IL_53:
				if (A_0 != null)
				{
					goto IL_E1;
				}
				break;
			}
			}
			num = 0;
		}
		IL_61:
		throw new ArgumentNullException(RecordTableEnumerator.b("ⵀあㅄᕆⱈⱊ⑌⁎㽐⁒", a_));
	}

	// Token: 0x06002FBD RID: 12221 RVA: 0x001B2464 File Offset: 0x001B1464
	public void ᜀ(IDictionary A_0, int A_1, int A_2)
	{
		int a_ = 1;
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
				IDictionaryEnumerator enumerator;
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
						break;
					}
					break;
				case 1:
					try
					{
						num = 4;
						for (;;)
						{
							switch (num)
							{
							case 1:
							{
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
								long a_2 = (long)dictionaryEntry.Key;
								long a_3 = (long)dictionaryEntry.Value;
								int num2 = sprṔ.ᜁ(a_2);
								int num3 = sprṔ.ᜀ(a_2);
								int num4 = sprṔ.ᜁ(a_3);
								int num5 = sprṔ.ᜀ(a_3);
								this.ᜀ(num2 + A_1 - 1, num4 + A_1 - 1, num3 + A_2 - 1, num5 + A_2 - 1, MergeOperationType.Delete);
								num = 0;
								continue;
							}
							case 2:
								goto IL_139;
							case 3:
								num = 2;
								continue;
							}
							IL_107:
							num = 1;
							continue;
							goto IL_107;
						}
						IL_139:
						return;
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
									goto IL_184;
								case 1:
									if (disposable != null)
									{
										num = 2;
										continue;
									}
									goto IL_186;
								case 2:
									disposable.Dispose();
									num = 0;
									continue;
								}
								break;
							}
						}
						IL_184:
						IL_186:;
					}
					goto IL_187;
				case 2:
					goto IL_68;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				IL_187:
				this.ᜂ();
				enumerator = A_0.GetEnumerator();
				num = 1;
			}
			IL_68:
			throw new ArgumentNullException(RecordTableEnumerator.b("匶倸堺䤼爾⑀ㅂ≄≆㩈", a_));
		}
		}
	}

	// Token: 0x06002FBE RID: 12222 RVA: 0x001B2648 File Offset: 0x001B1648
	public Rectangle ᜁ(Rectangle A_0)
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
			spr\u25A6.ᜀ ᜀ = this.ᜃ(A_0);
			if (ᜀ != null)
			{
				return new Rectangle(ᜀ.ᜅ(), ᜀ.ᜂ(), 0, 0);
			}
			if (true)
			{
			}
			break;
		}
		}
		return Rectangle.FromLTRB(-1, -1, -1, -1);
	}

	// Token: 0x06002FBF RID: 12223 RVA: 0x001B26B0 File Offset: 0x001B16B0
	public object ᜀ(object A_0)
	{
		int a_ = 4;
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
			if (A_0 != null)
			{
				spr\u1FBC spr_u1FBC = (spr\u1FBC)base.MemberwiseClone();
				spr_u1FBC.SetParent(A_0);
				spr_u1FBC.ᜀ();
				spr_u1FBC.ᜃ = this.ᜀ(this.ᜃ);
				spr_u1FBC.ᜁ = spr\u1CD3.ᜀ<spr\u25A6>(this.ᜁ);
				return spr_u1FBC;
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䨹崻䰽┿ⱁぃ", a_));
	}

	// Token: 0x06002FC0 RID: 12224 RVA: 0x001B274C File Offset: 0x001B174C
	private List<Rectangle> ᜀ(List<Rectangle> A_0)
	{
		List<Rectangle> list;
		for (;;)
		{
			IL_18:
			int count = A_0.Count;
			list = new List<Rectangle>(count);
			int num = 0;
			for (;;)
			{
				IL_28:
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
							goto IL_28;
						default:
							if (false)
							{
							}
							goto IL_32;
						}
						break;
					case 1:
						if (num >= count)
						{
							if (true)
							{
							}
							num2 = 3;
							continue;
						}
						list.Add(A_0[num]);
						num++;
						num2 = 0;
						continue;
					case 2:
						goto IL_32;
					case 3:
						return list;
					}
					goto IL_18;
					IL_32:
					num2 = 1;
				}
			}
		}
		return list;
	}

	// Token: 0x06002FC1 RID: 12225 RVA: 0x001B27EC File Offset: 0x001B17EC
	public void ᜂ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				A_0--;
				A_1--;
				this.ᜂ();
				List<Rectangle> list = this.ᜃ;
				this.ᜃ = new List<Rectangle>();
				int num = 0;
				int count = list.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return;
					case 1:
					{
						spr\u25A6.ᜀ ᜀ;
						this.ᜀ(ᜀ, MergeOperationType.Delete);
						if (true)
						{
						}
						num2 = 2;
						continue;
					}
					case 2:
						goto IL_6C;
					case 3:
						goto IL_126;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F2;
						default:
							if (false)
							{
							}
							goto IL_126;
						}
						break;
					case 5:
					{
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						Rectangle a_ = list[num];
						spr\u25A6.ᜀ ᜀ = this.ᜄ(a_);
						ᜀ.ᜁ(Math.Min(ᜀ.ᜇ(), A_0));
						ᜀ.ᜀ(Math.Min(ᜀ.ᜃ(), A_1));
						goto IL_F2;
					}
					case 6:
					{
						spr\u25A6.ᜀ ᜀ;
						if (ᜀ.ᜆ() > 1)
						{
							num2 = 1;
							continue;
						}
						goto IL_6C;
					}
					}
					break;
					IL_6C:
					num++;
					num2 = 4;
					continue;
					IL_F2:
					num2 = 6;
					continue;
					IL_126:
					num2 = 5;
				}
			}
			return;
		}
	}

	// Token: 0x06002FC2 RID: 12226 RVA: 0x001B2940 File Offset: 0x001B1940
	[CLSCompliant(false)]
	internal spr\u25A6.ᜀ ᜃ(Rectangle A_0)
	{
		switch (0)
		{
		default:
		{
			spr\u25A6.ᜀ result;
			for (;;)
			{
				if (true)
				{
				}
				result = null;
				int num = 0;
				int num2 = this.ᜅ();
				int num3 = 6;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						if (num >= num2)
						{
							num3 = 3;
							continue;
						}
						Rectangle a_ = this.ᜃ[num];
						goto IL_9C;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9C;
						default:
							if (false)
							{
							}
							goto IL_C9;
						}
						break;
					case 2:
					{
						Rectangle a_;
						result = this.ᜄ(a_);
						num3 = 5;
						continue;
					}
					case 3:
						return result;
					case 4:
					{
						Rectangle a_;
						if (UtilityMethods.ᜀ(a_, A_0))
						{
							num3 = 2;
							continue;
						}
						num++;
						num3 = 1;
						continue;
					}
					case 5:
						return result;
					case 6:
						goto IL_C9;
					}
					break;
					IL_9C:
					num3 = 4;
					continue;
					IL_C9:
					num3 = 0;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06002FC3 RID: 12227 RVA: 0x001B2A38 File Offset: 0x001B1A38
	[CLSCompliant(false)]
	internal spr\u25A6.ᜀ ᜄ(Rectangle A_0)
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
		int x = A_0.X;
		int y = A_0.Y;
		int right = A_0.Right;
		int bottom = A_0.Bottom;
		return new spr\u25A6.ᜀ(y, bottom, x, right);
	}

	// Token: 0x06002FC4 RID: 12228 RVA: 0x001B2A9C File Offset: 0x001B1A9C
	public static spr\u25A6.ᜀ ᜃ(spr\u25A6.ᜀ A_0, bool A_1, int A_2, int A_3, IWorkbook A_4)
	{
		int a_ = 19;
		if (true)
		{
		}
		int num = 2;
		int num3;
		int num4;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				if (A_3 > A_4.MaxRowCount)
				{
					num = 11;
					continue;
				}
				num = 13;
				continue;
			case 1:
				goto IL_175;
			case 3:
				num = 5;
				continue;
			case 4:
				goto IL_115;
			case 5:
				num2 = A_3;
				goto IL_148;
			case 6:
				goto IL_83;
			case 7:
				num2 = -A_3;
				goto IL_148;
			case 8:
				if (num3 >= A_2)
				{
					goto IL_83;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_115;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 9:
				num = 0;
				continue;
			case 10:
				goto IL_A6;
			case 11:
				goto IL_144;
			case 12:
				if (num4 < A_2)
				{
					num = 10;
					continue;
				}
				goto IL_18B;
			case 13:
				if (!A_1)
				{
					num = 3;
					continue;
				}
				num = 7;
				continue;
			case 14:
				if (num3 >= A_4.MaxRowCount)
				{
					num = 1;
					continue;
				}
				num = 8;
				continue;
			}
			if (A_3 > 0)
			{
				num = 9;
				continue;
			}
			goto IL_CB;
			IL_83:
			int num5;
			num4 = A_0.ᜇ() + num5;
			num = 12;
			continue;
			IL_115:
			num3 = A_2;
			num = 6;
			continue;
			IL_148:
			num5 = num2;
			num3 = A_0.ᜂ() + num5;
			num = 14;
		}
		IL_A6:
		return null;
		IL_CB:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㭈⑊㩌౎㹐♒㭔⍖", a_));
		IL_144:
		goto IL_CB;
		IL_175:
		return null;
		IL_18B:
		num3 = spr\u1FBC.ᜁ(num3, A_4);
		num4 = spr\u1FBC.ᜁ(num4, A_4);
		return new spr\u25A6.ᜀ(num3, num4, A_0.ᜅ(), A_0.ᜃ());
	}

	// Token: 0x06002FC5 RID: 12229 RVA: 0x001B2C5C File Offset: 0x001B1C5C
	public static spr\u25A6.ᜀ ᜁ(spr\u25A6.ᜀ A_0, bool A_1, int A_2, IWorkbook A_3)
	{
		int a_ = 8;
		int num = 11;
		int num4;
		int num6;
		for (;;)
		{
			int num3;
			int num2;
			int num5;
			switch (num)
			{
			case 0:
				num = 4;
				continue;
			case 1:
				if (!A_1)
				{
					num = 6;
					continue;
				}
				num = 15;
				continue;
			case 2:
				num2 = A_0.ᜂ() + num3;
				goto IL_10D;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12F;
				default:
					if (false)
					{
					}
					if (num4 >= A_3.MaxRowCount)
					{
						num = 7;
						continue;
					}
					goto IL_196;
				}
				break;
			case 4:
				num5 = A_2;
				goto IL_E8;
			case 5:
				num5 = -A_2;
				goto IL_E8;
			case 6:
				num = 2;
				continue;
			case 7:
				goto IL_191;
			case 8:
				goto IL_83;
			case 9:
				if (A_2 > A_3.MaxRowCount)
				{
					num = 8;
					continue;
				}
				num = 14;
				continue;
			case 10:
				if (num6 < A_0.ᜂ())
				{
					num = 12;
					continue;
				}
				num = 3;
				continue;
			case 12:
				goto IL_12D;
			case 13:
				num = 9;
				continue;
			case 14:
				if (!A_1)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				num = 5;
				continue;
			case 15:
				num2 = A_0.ᜂ();
				goto IL_10D;
			}
			if (A_2 > 0)
			{
				num = 13;
				continue;
			}
			break;
			IL_E8:
			num3 = num5;
			num6 = A_0.ᜇ() + num3;
			num = 1;
			continue;
			IL_10D:
			num4 = num2;
			num = 10;
		}
		IL_83:
		goto IL_12F;
		IL_12D:
		return null;
		IL_12F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䰽⼿㕁݃⥅㵇⑉㡋", a_));
		IL_191:
		return null;
		IL_196:
		num4 = spr\u1FBC.ᜁ(num4, A_3);
		num6 = spr\u1FBC.ᜁ(num6, A_3);
		return new spr\u25A6.ᜀ(num4, num6, A_0.ᜅ(), A_0.ᜃ());
	}

	// Token: 0x06002FC6 RID: 12230 RVA: 0x001B2E24 File Offset: 0x001B1E24
	public static spr\u25A6.ᜀ ᜂ(spr\u25A6.ᜀ A_0, bool A_1, int A_2, int A_3, IWorkbook A_4)
	{
		int a_ = 0;
		int num = 0;
		int num2;
		for (;;)
		{
			int num3;
			switch (num)
			{
			case 1:
				num2 = A_2 - 1;
				num = 8;
				continue;
			case 2:
				if (num2 < A_2)
				{
					num = 1;
					continue;
				}
				goto IL_129;
			case 3:
				goto IL_124;
			case 4:
				num3 = A_3;
				goto IL_DA;
			case 5:
				num3 = -A_3;
				goto IL_DA;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_124;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 7:
				if (true)
				{
				}
				if (!A_1)
				{
					num = 3;
					continue;
				}
				num = 5;
				continue;
			case 8:
				goto IL_97;
			case 9:
				goto IL_D8;
			case 10:
				if (A_3 > A_4.MaxRowCount)
				{
					num = 9;
					continue;
				}
				num = 7;
				continue;
			}
			if (A_3 > 0)
			{
				num = 6;
				continue;
			}
			break;
			IL_DA:
			int num4 = num3;
			num2 = A_0.ᜇ() + num4;
			num = 2;
			continue;
			IL_124:
			num = 4;
		}
		IL_74:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䐵圷䴹缻儽㔿ⱁぃ", a_));
		IL_97:
		goto IL_129;
		IL_D8:
		goto IL_74;
		IL_129:
		num2 = spr\u1FBC.ᜁ(num2, A_4);
		return new spr\u25A6.ᜀ(A_0.ᜂ(), num2, A_0.ᜅ(), A_0.ᜃ());
	}

	// Token: 0x06002FC7 RID: 12231 RVA: 0x001B2F7C File Offset: 0x001B1F7C
	public static spr\u25A6.ᜀ ᜁ(spr\u25A6.ᜀ A_0, bool A_1, int A_2)
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
		return new spr\u25A6.ᜀ(A_0.ᜂ(), A_0.ᜇ(), A_0.ᜅ(), A_0.ᜃ());
	}

	// Token: 0x06002FC8 RID: 12232 RVA: 0x001B2FD4 File Offset: 0x001B1FD4
	public static spr\u25A6.ᜀ ᜁ(spr\u25A6.ᜀ A_0, int A_1, bool A_2, int A_3, IWorkbook A_4)
	{
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
					goto IL_13E;
				default:
					if (false)
					{
					}
					if (A_0.ᜇ() < A_1)
					{
						if (true)
						{
						}
						num = 10;
						continue;
					}
					goto IL_1F0;
				}
				break;
			case 1:
				if (A_0.ᜇ() == A_1)
				{
					num = 14;
					continue;
				}
				num = 0;
				continue;
			case 2:
				if (A_0.ᜇ() > A_1)
				{
					num = 4;
					continue;
				}
				goto IL_65;
			case 3:
				if (A_0.ᜂ() > A_1)
				{
					num = 7;
					continue;
				}
				num = 13;
				continue;
			case 4:
				goto IL_198;
			case 5:
				num = 6;
				continue;
			case 6:
				if (A_0.ᜇ() == A_4.MaxRowCount - 1)
				{
					num = 11;
					continue;
				}
				goto IL_A7;
			case 7:
				goto IL_D3;
			case 8:
				goto IL_1EB;
			case 10:
				goto IL_11E;
			case 11:
				goto IL_1C4;
			case 12:
				if (A_0.ᜂ() < A_1)
				{
					num = 15;
					continue;
				}
				goto IL_65;
			case 13:
				if (A_0.ᜂ() == A_1)
				{
					num = 8;
					continue;
				}
				num = 12;
				continue;
			case 14:
				goto IL_7E;
			case 15:
				num = 2;
				continue;
			}
			if (A_0.ᜂ() == 0)
			{
				num = 5;
				continue;
			}
			goto IL_A7;
			IL_65:
			num = 1;
			continue;
			IL_A7:
			num = 3;
		}
		IL_7E:
		return spr\u1FBC.ᜂ(A_0, A_2, A_1, A_3, A_4);
		IL_D3:
		return spr\u1FBC.ᜃ(A_0, A_2, A_1, A_3, A_4);
		IL_11E:
		return spr\u1FBC.ᜁ(A_0, A_2, A_3);
		IL_13E:
		return spr\u1FBC.ᜂ(A_0, A_2, A_1, A_3, A_4);
		IL_198:
		goto IL_13E;
		IL_1C4:
		return new spr\u25A6.ᜀ(A_0.ᜂ(), A_0.ᜇ(), A_0.ᜅ(), A_0.ᜃ());
		IL_1EB:
		return spr\u1FBC.ᜁ(A_0, A_2, A_3, A_4);
		IL_1F0:
		return null;
	}

	// Token: 0x06002FC9 RID: 12233 RVA: 0x001B31D4 File Offset: 0x001B21D4
	public static spr\u25A6.ᜀ ᜁ(spr\u25A6.ᜀ A_0, bool A_1, int A_2, int A_3, IWorkbook A_4)
	{
		int a_ = 7;
		int num = 3;
		int num3;
		int num4;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				num2 = A_3;
				goto IL_129;
			case 1:
				goto IL_13E;
			case 2:
				if (num3 < A_2)
				{
					num = 14;
					continue;
				}
				goto IL_185;
			case 4:
				num2 = -A_3;
				goto IL_129;
			case 5:
				if (true)
				{
				}
				num = 11;
				continue;
			case 6:
				goto IL_125;
			case 7:
				if (num4 < A_2)
				{
					num = 12;
					continue;
				}
				goto IL_7B;
			case 8:
				goto IL_153;
			case 9:
				if (!A_1)
				{
					num = 10;
					continue;
				}
				num = 4;
				continue;
			case 10:
				num = 0;
				continue;
			case 11:
				if (A_3 > A_4.MaxColumnCount)
				{
					num = 6;
					continue;
				}
				num = 9;
				continue;
			case 12:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_13E;
				default:
					if (false)
					{
					}
					num4 = A_2;
					num = 13;
					continue;
				}
				break;
			case 13:
				goto IL_7B;
			case 14:
				goto IL_9E;
			}
			if (A_3 > 0)
			{
				num = 5;
				continue;
			}
			goto IL_CA;
			IL_7B:
			int num5;
			num3 = A_0.ᜃ() + num5;
			num = 2;
			continue;
			IL_13E:
			if (num4 >= A_4.MaxColumnCount)
			{
				num = 8;
				continue;
			}
			num = 7;
			continue;
			IL_129:
			num5 = num2;
			num4 = A_0.ᜅ() + num5;
			num = 1;
		}
		IL_9E:
		return null;
		IL_CA:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帼倾㑀ⵂㅄ", a_));
		IL_125:
		goto IL_CA;
		IL_153:
		return null;
		IL_185:
		num4 = spr\u1FBC.ᜀ(num4, A_4);
		num3 = spr\u1FBC.ᜀ(num3, A_4);
		return new spr\u25A6.ᜀ(A_0.ᜂ(), A_0.ᜇ(), num4, num3);
	}

	// Token: 0x06002FCA RID: 12234 RVA: 0x001B338C File Offset: 0x001B238C
	public static spr\u25A6.ᜀ ᜀ(spr\u25A6.ᜀ A_0, bool A_1, int A_2, IWorkbook A_3)
	{
		int a_ = 19;
		int num = 4;
		int num5;
		int num6;
		for (;;)
		{
			int num2;
			int num3;
			int num4;
			switch (num)
			{
			case 0:
				num2 = A_0.ᜅ();
				goto IL_10D;
			case 1:
				num3 = A_2;
				goto IL_E0;
			case 2:
				if (A_2 > A_3.MaxColumnCount)
				{
					num = 14;
					continue;
				}
				num = 6;
				continue;
			case 3:
				num2 = A_0.ᜅ() + num4;
				goto IL_10D;
			case 5:
				goto IL_191;
			case 6:
				if (!A_1)
				{
					num = 9;
					continue;
				}
				num = 7;
				continue;
			case 7:
				num3 = -A_2;
				goto IL_E0;
			case 8:
				if (!A_1)
				{
					if (true)
					{
					}
					num = 10;
					continue;
				}
				num = 0;
				continue;
			case 9:
				num = 1;
				continue;
			case 10:
				num = 3;
				continue;
			case 11:
				if (num5 < A_0.ᜅ())
				{
					num = 15;
					continue;
				}
				num = 13;
				continue;
			case 12:
				num = 2;
				continue;
			case 13:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12F;
				default:
					if (false)
					{
					}
					if (num6 >= A_3.MaxColumnCount)
					{
						num = 5;
						continue;
					}
					goto IL_196;
				}
				break;
			case 14:
				goto IL_83;
			case 15:
				goto IL_12D;
			}
			if (A_2 > 0)
			{
				num = 12;
				continue;
			}
			break;
			IL_E0:
			num4 = num3;
			num5 = A_0.ᜃ() + num4;
			num = 8;
			continue;
			IL_10D:
			num6 = num2;
			num = 11;
		}
		IL_83:
		goto IL_12F;
		IL_12D:
		return null;
		IL_12F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⩈⑊㡌ⅎ═", a_));
		IL_191:
		return null;
		IL_196:
		num6 = spr\u1FBC.ᜁ(num6, A_3);
		num5 = spr\u1FBC.ᜁ(num5, A_3);
		return new spr\u25A6.ᜀ(A_0.ᜂ(), A_0.ᜇ(), num6, num5);
	}

	// Token: 0x06002FCB RID: 12235 RVA: 0x001B3554 File Offset: 0x001B2554
	public static spr\u25A6.ᜀ ᜀ(spr\u25A6.ᜀ A_0, bool A_1, int A_2, int A_3, IWorkbook A_4)
	{
		int a_ = 5;
		int num = 3;
		int num2;
		for (;;)
		{
			int num3;
			switch (num)
			{
			case 0:
				num2 = A_2 - 1;
				num = 5;
				continue;
			case 1:
				goto IL_124;
			case 2:
				if (!A_1)
				{
					num = 1;
					continue;
				}
				num = 10;
				continue;
			case 4:
				if (num2 < A_2)
				{
					num = 0;
					continue;
				}
				goto IL_129;
			case 5:
				goto IL_97;
			case 6:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_124;
				default:
					if (false)
					{
					}
					num = 9;
					continue;
				}
				break;
			case 7:
				num3 = A_3;
				goto IL_E2;
			case 8:
				goto IL_E0;
			case 9:
				if (A_3 > A_4.MaxColumnCount)
				{
					num = 8;
					continue;
				}
				num = 2;
				continue;
			case 10:
				num3 = -A_3;
				goto IL_E2;
			}
			if (A_3 > 0)
			{
				num = 6;
				continue;
			}
			break;
			IL_E2:
			int num4 = num3;
			num2 = A_0.ᜃ() + num4;
			num = 4;
			continue;
			IL_124:
			num = 7;
		}
		IL_74:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("堺刼䨾⽀㝂", a_));
		IL_97:
		goto IL_129;
		IL_E0:
		goto IL_74;
		IL_129:
		num2 = spr\u1FBC.ᜀ(num2, A_4);
		return new spr\u25A6.ᜀ(A_0.ᜂ(), A_0.ᜇ(), A_0.ᜅ(), num2);
	}

	// Token: 0x06002FCC RID: 12236 RVA: 0x001B36AC File Offset: 0x001B26AC
	public static spr\u25A6.ᜀ ᜀ(spr\u25A6.ᜀ A_0, bool A_1, int A_2)
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
		return new spr\u25A6.ᜀ(A_0.ᜂ(), A_0.ᜇ(), A_0.ᜅ(), A_0.ᜃ());
	}

	// Token: 0x06002FCD RID: 12237 RVA: 0x001B3704 File Offset: 0x001B2704
	public static spr\u25A6.ᜀ ᜀ(spr\u25A6.ᜀ A_0, int A_1, bool A_2, int A_3, IWorkbook A_4)
	{
		int num = 14;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.ᜃ() == A_1)
				{
					num = 1;
					continue;
				}
				num = 12;
				continue;
			case 1:
				goto IL_7E;
			case 2:
				goto IL_17E;
			case 3:
				goto IL_1AA;
			case 4:
				goto IL_DB;
			case 5:
				if (A_0.ᜅ() == A_1)
				{
					num = 11;
					continue;
				}
				num = 8;
				continue;
			case 6:
				goto IL_11B;
			case 7:
				if (A_0.ᜃ() > A_1)
				{
					num = 2;
					continue;
				}
				goto IL_65;
			case 8:
				if (A_0.ᜅ() < A_1)
				{
					num = 10;
					continue;
				}
				goto IL_65;
			case 9:
				num = 13;
				continue;
			case 10:
				num = 7;
				continue;
			case 11:
				goto IL_1D1;
			case 12:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_124;
				default:
					if (false)
					{
					}
					if (A_0.ᜃ() < A_1)
					{
						num = 6;
						continue;
					}
					goto IL_1D6;
				}
				break;
			case 13:
				if (A_0.ᜃ() == A_4.MaxColumnCount - 1)
				{
					num = 3;
					continue;
				}
				goto IL_A7;
			case 15:
				if (A_0.ᜅ() > A_1)
				{
					if (true)
					{
					}
					num = 4;
					continue;
				}
				num = 5;
				continue;
			}
			if (A_0.ᜅ() == 0)
			{
				num = 9;
				continue;
			}
			goto IL_A7;
			IL_65:
			num = 0;
			continue;
			IL_A7:
			num = 15;
		}
		IL_7E:
		return spr\u1FBC.ᜀ(A_0, A_2, A_1, A_3, A_4);
		IL_DB:
		return spr\u1FBC.ᜁ(A_0, A_2, A_1, A_3, A_4);
		IL_11B:
		return spr\u1FBC.ᜀ(A_0, A_2, A_3);
		IL_124:
		return spr\u1FBC.ᜀ(A_0, A_2, A_1, A_3, A_4);
		IL_17E:
		goto IL_124;
		IL_1AA:
		return new spr\u25A6.ᜀ(A_0);
		IL_1D1:
		return spr\u1FBC.ᜀ(A_0, A_2, A_3, A_4);
		IL_1D6:
		return null;
	}

	// Token: 0x06002FCE RID: 12238 RVA: 0x001B38E8 File Offset: 0x001B28E8
	public static int ᜁ(int A_0, IWorkbook A_1)
	{
		if (A_0 < 0)
		{
			for (;;)
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
				break;
			}
			if (false)
			{
			}
			return 0;
		}
		return Math.Min(A_0, A_1.MaxRowCount - 1);
	}

	// Token: 0x06002FCF RID: 12239 RVA: 0x001B393C File Offset: 0x001B293C
	public static int ᜀ(int A_0, IWorkbook A_1)
	{
		if (A_0 < 0)
		{
			for (;;)
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
				break;
			}
			if (false)
			{
			}
			return 0;
		}
		return Math.Min(A_0, A_1.MaxColumnCount - 1);
	}

	// Token: 0x04001554 RID: 5460
	private XlsWorksheet ᜀ;

	// Token: 0x04001555 RID: 5461
	private List<spr\u25A6> ᜁ = new List<spr\u25A6>();

	// Token: 0x04001556 RID: 5462
	private bool ᜂ = true;

	// Token: 0x04001557 RID: 5463
	private List<Rectangle> ᜃ = new List<Rectangle>();
}
