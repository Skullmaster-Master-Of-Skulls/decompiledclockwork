using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003F5 RID: 1013
[spr\u2593(TBIFFRecord.RangeProtection)]
[CLSCompliant(false)]
internal class spr\u1A5D : BiffRecordRaw
{
	// Token: 0x06003CEF RID: 15599 RVA: 0x002207F4 File Offset: 0x0021F7F4
	public IgnoreErrorType ᜁ()
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
		return this.ᜇ;
	}

	// Token: 0x06003CF0 RID: 15600 RVA: 0x00220838 File Offset: 0x0021F838
	public void ᜀ(IgnoreErrorType A_0)
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
		this.ᜇ = A_0;
	}

	// Token: 0x06003CF1 RID: 15601 RVA: 0x0022087C File Offset: 0x0021F87C
	public spr\u1F7E ᜂ()
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
		return this.ᜈ;
	}

	// Token: 0x06003CF2 RID: 15602 RVA: 0x002208C0 File Offset: 0x0021F8C0
	public void ᜀ(spr\u1F7E A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x06003CF3 RID: 15603 RVA: 0x00220904 File Offset: 0x0021F904
	public virtual int ᜃ()
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
		return 39;
	}

	// Token: 0x06003CF4 RID: 15604 RVA: 0x00220944 File Offset: 0x0021F944
	public spr\u1A5D()
	{
		byte[] array = new byte[6];
		array[0] = 4;
		this.ᜆ = array;
		base..ctor();
	}

	// Token: 0x06003CF5 RID: 15605 RVA: 0x00220984 File Offset: 0x0021F984
	public spr\u1A5D(Stream A_0, out int A_1)
	{
		byte[] array = new byte[6];
		array[0] = 4;
		this.ᜆ = array;
		base..ctor(A_0, out A_1);
	}

	// Token: 0x06003CF6 RID: 15606 RVA: 0x002209C4 File Offset: 0x0021F9C4
	public spr\u1A5D(int A_0)
	{
		byte[] array = new byte[6];
		array[0] = 4;
		this.ᜆ = array;
		base..ctor(A_0);
	}

	// Token: 0x06003CF7 RID: 15607 RVA: 0x00220A04 File Offset: 0x0021FA04
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		int a_ = 13;
		switch (0)
		{
		default:
			for (;;)
			{
				int num = (int)A_0.ReadUInt16(A_1 + 19);
				int num2 = 27 + num * 8;
				int num3 = 0;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						if (num2 > A_2)
						{
							num3 = 1;
							continue;
						}
						this.ᜇ = (IgnoreErrorType)A_0.ReadUInt16(A_1 + num2);
						A_1 += 27;
						num3 = 6;
						continue;
					case 1:
						goto IL_6B;
					case 2:
						return;
					case 3:
						goto IL_10F;
					case 4:
					{
						int num4;
						if (num4 >= num)
						{
							num3 = 2;
							continue;
						}
						if (true)
						{
						}
						int num5 = (int)A_0.ReadUInt16(A_1);
						A_1 += 2;
						int num6 = (int)A_0.ReadUInt16(A_1);
						A_1 += 2;
						int num7 = (int)A_0.ReadUInt16(A_1);
						A_1 += 2;
						int num8 = (int)A_0.ReadUInt16(A_1);
						A_1 += 2;
						Rectangle a_2 = new Rectangle(num7, num5, num8 - num7, num6 - num5);
						this.ᜈ.ᜄ(a_2);
						num4++;
						num3 = 5;
						continue;
					}
					case 5:
						goto IL_10F;
					case 6:
					{
						if (this.ᜇ == IgnoreErrorType.None)
						{
							num3 = 7;
							continue;
						}
						this.ᜈ = new spr\u1F7E(this.ᜇ);
						int num4 = 0;
						num3 = 3;
						continue;
					}
					case 7:
						return;
					}
					break;
					IL_10F:
					num3 = 4;
				}
			}
			IL_6B:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				throw new ApplicationException(RecordTableEnumerator.b("B⑄⥆❈⑊㥌潎⅐㉒❔⑖㱘筚㹜⩞፠ᅢd०ᵨ䭪Ὤ੮ተᱲݴ፶坸", a_));
			}
			return;
		}
	}

	// Token: 0x06003CF8 RID: 15608 RVA: 0x00220BAC File Offset: 0x0021FBAC
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
			switch (0)
			{
			}
			break;
		}
		for (;;)
		{
			this.m_iLength = this.GetStoreSize(A_2);
			A_0.WriteBytes(A_1, this.ᜅ, 0, this.ᜅ.Length);
			A_1 += 19;
			List<Rectangle> list = this.ᜈ.ᜂ();
			int count = list.Count;
			A_0.WriteUInt16(A_1, (ushort)count);
			A_1 += 2;
			int num = this.ᜆ.Length;
			A_0.WriteBytes(A_1, this.ᜆ, 0, num);
			A_1 += num;
			int num2 = 0;
			int num3 = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num3)
				{
				case 0:
				{
					if (num2 >= count)
					{
						num3 = 1;
						continue;
					}
					Rectangle rectangle = list[num2];
					A_0.WriteUInt16(A_1, (ushort)rectangle.Y);
					A_1 += 2;
					A_0.WriteUInt16(A_1, (ushort)rectangle.Bottom);
					A_1 += 2;
					A_0.WriteUInt16(A_1, (ushort)rectangle.X);
					A_1 += 2;
					A_0.WriteUInt16(A_1, (ushort)rectangle.Right);
					A_1 += 2;
					num2++;
					num3 = 2;
					continue;
				}
				case 1:
					goto IL_DE;
				case 2:
					goto IL_C2;
				case 3:
					goto IL_C2;
				}
				break;
				IL_C2:
				num3 = 0;
			}
		}
		IL_DE:
		A_0.WriteInt32(A_1, (int)this.ᜇ);
	}

	// Token: 0x06003CF9 RID: 15609 RVA: 0x00220D20 File Offset: 0x0021FD20
	public virtual int ᜀ(ExcelVersion A_0)
	{
		for (;;)
		{
			this.ᜀ();
			int num = 1;
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
						goto IL_42;
					default:
						goto IL_68;
					}
					break;
				case 1:
					if (this.ᜈ != null)
					{
						num = 2;
						continue;
					}
					goto IL_42;
				case 2:
					num = 3;
					continue;
				case 3:
					goto IL_79;
				}
				break;
				IL_42:
				num = 0;
			}
		}
		IL_68:
		if (false)
		{
		}
		int num2 = 0;
		goto IL_8B;
		IL_79:
		num2 = this.ᜈ.ᜂ().Count;
		IL_8B:
		int num3 = num2;
		return 27 + num3 * 8 + 4;
	}

	// Token: 0x06003CFA RID: 15610 RVA: 0x00220DC4 File Offset: 0x0021FDC4
	private void ᜀ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				List<Rectangle> list = this.ᜈ.ᜂ();
				int num = 9;
				for (;;)
				{
					SortedDictionary<int, SortedList<int, Rectangle>> sortedDictionary;
					Rectangle value;
					SortedList<int, Rectangle> sortedList;
					int num2;
					int count;
					int num3;
					int count2;
					switch (num)
					{
					case 0:
						goto IL_171;
					case 1:
						goto IL_19C;
					case 2:
						if (!sortedDictionary.TryGetValue(value.Top, out sortedList))
						{
							num = 8;
							continue;
						}
						goto IL_2AF;
					case 3:
						if (num2 == count)
						{
							num = 11;
							continue;
						}
						goto IL_19C;
					case 4:
					{
						this.ᜈ.ᜃ();
						SortedDictionary<int, SortedList<int, Rectangle>>.KeyCollection.Enumerator enumerator = sortedDictionary.Keys.GetEnumerator();
						num = 12;
						continue;
					}
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_205;
						default:
							if (false)
							{
							}
							goto IL_2AF;
						}
						break;
					case 6:
						goto IL_171;
					case 7:
						count = list.Count;
						num = 1;
						continue;
					case 8:
						sortedList = new SortedList<int, Rectangle>();
						sortedDictionary.Add(value.Top, sortedList);
						num = 5;
						continue;
					case 9:
						if (list.Count > 1)
						{
							num = 7;
							continue;
						}
						return;
					case 10:
						if (num3 >= count2)
						{
							num = 4;
							continue;
						}
						value = list[num3];
						num = 2;
						continue;
					case 11:
						return;
					case 12:
						try
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_12F;
								case 2:
									goto IL_12F;
								case 3:
								{
									SortedDictionary<int, SortedList<int, Rectangle>>.KeyCollection.Enumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 7;
										continue;
									}
									int key = enumerator.Current;
									IList<Rectangle> list2 = sortedDictionary[key].Values;
									list2 = this.ᜀ(list2);
									int num4 = 0;
									int count3 = list2.Count;
									num = 2;
									continue;
								}
								case 4:
								{
									int num4;
									int count3;
									if (num4 >= count3)
									{
										num = 6;
										continue;
									}
									IList<Rectangle> list2;
									this.ᜈ.ᜄ(list2[num4]);
									num4++;
									num = 1;
									continue;
								}
								case 5:
									goto IL_15E;
								case 7:
									num = 5;
									continue;
								}
								IL_AE:
								num = 3;
								continue;
								goto IL_AE;
								IL_12F:
								num = 4;
							}
							IL_15E:
							goto IL_205;
						}
						finally
						{
							SortedDictionary<int, SortedList<int, Rectangle>>.KeyCollection.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						goto IL_171;
					}
					break;
					IL_171:
					if (true)
					{
					}
					num = 10;
					continue;
					IL_19C:
					num2 = count;
					sortedDictionary = new SortedDictionary<int, SortedList<int, Rectangle>>();
					num3 = 0;
					count2 = list.Count;
					num = 0;
					continue;
					IL_205:
					count = this.ᜈ.ᜂ().Count;
					num = 3;
					continue;
					IL_2AF:
					sortedList.Add(value.Left, value);
					num3++;
					num = 6;
				}
			}
			return;
		}
	}

	// Token: 0x06003CFB RID: 15611 RVA: 0x002210B8 File Offset: 0x002200B8
	private IList<Rectangle> ᜀ(IList<Rectangle> A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 15;
			for (;;)
			{
				Rectangle item;
				List<Rectangle> list;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_6C;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6C;
					default:
					{
						if (false)
						{
						}
						Rectangle value = Rectangle.FromLTRB(value.Left, value.Top, item.Right, value.Bottom);
						int index;
						list[index] = value;
						num = 11;
						continue;
					}
					}
					break;
				case 2:
					if (true)
					{
					}
					num = 3;
					continue;
				case 3:
				{
					Rectangle value;
					if (value.Bottom == item.Bottom)
					{
						num = 6;
						continue;
					}
					goto IL_6E;
				}
				case 4:
					goto IL_FD;
				case 5:
				{
					Rectangle value;
					if (value.Top == item.Top)
					{
						num = 2;
						continue;
					}
					goto IL_6E;
				}
				case 6:
					num = 10;
					continue;
				case 7:
				{
					if (A_0.Count == 0)
					{
						num = 4;
						continue;
					}
					list = new List<Rectangle>();
					list.Add(A_0[0]);
					num2 = 1;
					int count = A_0.Count;
					num = 8;
					continue;
				}
				case 8:
					goto IL_139;
				case 9:
				{
					int count;
					if (num2 >= count)
					{
						num = 14;
						continue;
					}
					int index = list.Count - 1;
					Rectangle value = list[index];
					item = A_0[num2];
					num = 5;
					continue;
				}
				case 10:
				{
					Rectangle value;
					if (value.Right + 1 == item.Left)
					{
						num = 1;
						continue;
					}
					goto IL_6E;
				}
				case 11:
					goto IL_C8;
				case 12:
					goto IL_C8;
				case 13:
					goto IL_139;
				case 14:
					return list;
				}
				if (A_0 != null)
				{
					num = 0;
					continue;
				}
				break;
				IL_6C:
				num = 7;
				continue;
				IL_6E:
				list.Add(item);
				num = 12;
				continue;
				IL_C8:
				num2++;
				num = 13;
				continue;
				IL_139:
				num = 9;
			}
			return A_0;
			IL_FD:
			return A_0;
		}
		}
	}

	// Token: 0x04001A46 RID: 6726
	private new const int ᜀ = 19;

	// Token: 0x04001A47 RID: 6727
	private const int ᜁ = 27;

	// Token: 0x04001A48 RID: 6728
	private const int ᜂ = 8;

	// Token: 0x04001A49 RID: 6729
	private new const int ᜃ = 4;

	// Token: 0x04001A4A RID: 6730
	public const int ᜄ = 1024;

	// Token: 0x04001A4B RID: 6731
	private readonly byte[] ᜅ = new byte[]
	{
		104,
		8,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		3,
		0,
		0,
		0,
		0,
		0,
		0
	};

	// Token: 0x04001A4C RID: 6732
	private readonly byte[] ᜆ;

	// Token: 0x04001A4D RID: 6733
	private IgnoreErrorType ᜇ;

	// Token: 0x04001A4E RID: 6734
	private spr\u1F7E ᜈ;
}
