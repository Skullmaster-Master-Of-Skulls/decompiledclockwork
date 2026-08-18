using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.TemplateMarkers;

// Token: 0x020002D3 RID: 723
internal class spr\u25CA : XlsObject, IMarkersDesigner
{
	// Token: 0x06002C4A RID: 11338 RVA: 0x0018C02C File Offset: 0x0018B02C
	static spr\u25CA()
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
		spr\u25CA.ᜇ = new List<spr\u22EA>();
		spr\u25CA.ᜈ = new Type[]
		{
			typeof(XlsWorksheet),
			typeof(XlsWorkbook)
		};
		spr\u25CA.ᜇ.Add(new spr\u227C());
		spr\u25CA.ᜇ.Add(new sprᵼ());
		spr\u25CA.ᜇ.Add(new spr\u25AF());
		spr\u25CA.ᜇ.Add(new spr\u25C1());
	}

	// Token: 0x06002C4B RID: 11339 RVA: 0x0018C0D4 File Offset: 0x0018B0D4
	public spr\u25CA(spr\u1DF5 A_0, object A_1)
	{
		int a_ = 9;
		this.ᜉ = new Dictionary<string, object>();
		this.ᜊ = RecordTableEnumerator.b("᤾籀", a_);
		this.ᜋ = ',';
		this.ᜌ = new Dictionary<string, VariableTypeAction>();
		base..ctor(A_0, A_1);
		this.ᜆ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
		if (this.ᜆ == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("簾⁀ⵂ⭄⡆㵈歊⭌♎㽐㝒畔❖㡘⥚㡜ㅞᕠ䍢ቤࡦ᭨jཬnṰᡲ孴", a_));
		}
	}

	// Token: 0x06002C4C RID: 11340 RVA: 0x0018C160 File Offset: 0x0018B160
	public void ᜁ()
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
		this.ᜀ(UnknownVariableAction.Exception);
	}

	// Token: 0x06002C4D RID: 11341 RVA: 0x0018C1A4 File Offset: 0x0018B1A4
	public void ᜀ(UnknownVariableAction A_0)
	{
		int a_ = 19;
		object obj;
		IWorksheet worksheet;
		for (;;)
		{
			for (;;)
			{
				obj = base.FindParent(spr\u25CA.ᜈ);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (obj == null)
						{
							num = 1;
							continue;
						}
						worksheet = (obj as IWorksheet);
						this.ᜆ.InnerFormats.ᜇ();
						num = 2;
						continue;
					case 1:
						goto IL_4A;
					case 2:
						if (worksheet == null)
						{
							goto IL_BC;
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
							if (true)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 3:
						goto IL_A6;
					}
					break;
				}
			}
		}
		IL_4A:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("᥈⩊㽌⩎㽐❒畔㡖㭘ㅚ㡜㱞ᕠ䍢٤٦ݨժɬ᭮兰ᅲၴ坶ὸᑺࡼᅾ궂", a_));
		IL_A6:
		this.ᜀ(worksheet, A_0);
		return;
		IL_BC:
		this.ᜀ((IWorkbook)obj, A_0);
	}

	// Token: 0x06002C4E RID: 11342 RVA: 0x0018C27C File Offset: 0x0018B27C
	public void ᜀ(string A_0, object A_1)
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
		this.ᜀ(A_0, A_1, VariableTypeAction.None);
	}

	// Token: 0x06002C4F RID: 11343 RVA: 0x0018C2C0 File Offset: 0x0018B2C0
	internal void ᜀ(string A_0, object A_1, VariableTypeAction A_2)
	{
		int a_ = 16;
		for (;;)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_E4;
				case 1:
					if (A_0.Length == 0)
					{
						num = 0;
						continue;
					}
					num = 4;
					continue;
				case 2:
					goto IL_3F;
				case 3:
					goto IL_61;
				case 4:
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					goto IL_E9;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					num = 1;
				}
			}
			IL_E4:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_79;
			}
		}
		IL_3F:
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("⡅⥇❉⥋", a_));
		IL_61:
		throw new ArgumentNullException(RecordTableEnumerator.b("ぅ⥇㡉╋⽍㉏㹑ㅓ", a_));
		IL_79:
		if (false)
		{
		}
		throw new ArgumentException(RecordTableEnumerator.b("ࡅ⥇❉⥋湍㍏㍑㩓㡕㝗⹙籛㱝՟䉡ţ୥ᡧṩᕫ䁭", a_));
		IL_E9:
		this.ᜌ.Add(A_0, A_2);
		this.ᜉ.Add(A_0, A_1);
	}

	// Token: 0x06002C50 RID: 11344 RVA: 0x0018C3D0 File Offset: 0x0018B3D0
	public void ᜂ(string A_0)
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
		this.ᜌ.Remove(A_0);
		this.ᜉ.Remove(A_0);
	}

	// Token: 0x06002C51 RID: 11345 RVA: 0x0018C428 File Offset: 0x0018B428
	public void ᜀ(IWorksheet A_0)
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
		this.ᜀ(A_0, UnknownVariableAction.Exception);
	}

	// Token: 0x06002C52 RID: 11346 RVA: 0x0018C46C File Offset: 0x0018B46C
	public void ᜀ(IWorksheet A_0, UnknownVariableAction A_1)
	{
		int a_ = 9;
		if (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("䰾⥀♂⁄㍆", a_));
			}
		}
		XlsWorkbook xlsWorkbook = (XlsWorkbook)A_0.Workbook;
		SSTDictionary sstdictionary = xlsWorkbook.InnerSST;
		List<int> a_2 = sstdictionary.StartWith(this.ᜊ);
		this.ᜀ(A_0, a_2, A_1);
	}

	// Token: 0x06002C53 RID: 11347 RVA: 0x0018C4F4 File Offset: 0x0018B4F4
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
		this.ᜀ(A_0, UnknownVariableAction.Exception);
	}

	// Token: 0x06002C54 RID: 11348 RVA: 0x0018C538 File Offset: 0x0018B538
	public void ᜀ(IWorkbook A_0, UnknownVariableAction A_1)
	{
		int a_ = 15;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_5D;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 4;
				break;
			}
			break;
		}
		int num2;
		int count;
		IWorksheets worksheets;
		List<int> a_3;
		for (;;)
		{
			IL_3E:
			switch (num)
			{
			case 0:
				goto IL_E9;
			case 1:
				goto IL_69;
			case 2:
				return;
			case 3:
				goto IL_E9;
			case 5:
			{
				if (num2 >= count)
				{
					num = 2;
					continue;
				}
				IWorksheet a_2 = worksheets[num2];
				this.ᜀ(a_2, a_3, A_1);
				num2++;
				num = 3;
				continue;
			}
			}
			goto IL_5D;
			IL_E9:
			if (true)
			{
			}
			num = 5;
		}
		IL_69:
		throw new ArgumentNullException(RecordTableEnumerator.b("❄⡆♈⁊", a_));
		IL_5D:
		if (A_0 == null)
		{
			num = 1;
			goto IL_3E;
		}
		XlsWorkbook xlsWorkbook = (XlsWorkbook)A_0;
		SSTDictionary sstdictionary = xlsWorkbook.InnerSST;
		a_3 = sstdictionary.StartWith(this.ᜊ);
		worksheets = A_0.Worksheets;
		num2 = 0;
		count = worksheets.Count;
		num = 0;
		goto IL_3E;
	}

	// Token: 0x06002C55 RID: 11349 RVA: 0x0018C65C File Offset: 0x0018B65C
	public bool ᜄ(string A_0)
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
		return this.ᜉ.ContainsKey(A_0);
	}

	// Token: 0x06002C56 RID: 11350 RVA: 0x0018C6A4 File Offset: 0x0018B6A4
	internal IConditionalFormats ᜀ(IXLSRange A_0)
	{
		CondFormatCollectionWrapper condFormatCollectionWrapper;
		for (;;)
		{
			for (;;)
			{
				condFormatCollectionWrapper = null;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.\u170D.ContainsKey(A_0.RangeGlobalAddress))
						{
							num = 4;
							continue;
						}
						condFormatCollectionWrapper = ((XlsRange)A_0).ConditionalFormats;
						this.\u170D.Add(A_0.RangeGlobalAddress, condFormatCollectionWrapper);
						num = 1;
						continue;
					case 1:
						return condFormatCollectionWrapper;
					case 2:
						return condFormatCollectionWrapper;
					case 3:
						goto IL_92;
					case 4:
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
							condFormatCollectionWrapper = this.\u170D[((XlsRange)A_0).RangeGlobalAddress];
							num = 2;
							continue;
						}
						break;
					case 5:
						if (this.\u170D == null)
						{
							num = 6;
							continue;
						}
						goto IL_92;
					case 6:
						this.\u170D = new Dictionary<string, CondFormatCollectionWrapper>();
						num = 3;
						continue;
					}
					break;
					IL_92:
					num = 0;
				}
			}
		}
		return condFormatCollectionWrapper;
	}

	// Token: 0x06002C57 RID: 11351 RVA: 0x0018C7B4 File Offset: 0x0018B7B4
	private void ᜀ(IWorksheet A_0, List<int> A_1, UnknownVariableAction A_2)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				IList<long> list;
				List<sprᮐ> list2;
				int count;
				int num2;
				IMigrantRange a_2;
				switch (num)
				{
				case 0:
					this.ᜀ(A_0.Workbook, A_0, list, list2);
					num = 1;
					continue;
				case 1:
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_14D;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 3:
					goto IL_94;
				case 4:
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					goto IL_14D;
				case 5:
					goto IL_EE;
				case 6:
					goto IL_94;
				case 7:
					if (count > 0)
					{
						num = 0;
						continue;
					}
					return;
				case 8:
					if (num2 >= count)
					{
						num = 9;
						continue;
					}
					list2.Add(this.ᜀ(A_0, list, num2, a_2, A_2));
					num2++;
					num = 6;
					continue;
				case 9:
					this.ᜀ(list2, A_0);
					num = 7;
					continue;
				case 10:
					goto IL_87;
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				num = 4;
				continue;
				IL_94:
				num = 8;
				continue;
				IL_14D:
				list = this.ᜀ(A_0, A_1);
				count = list.Count;
				list2 = new List<sprᮐ>(count);
				a_2 = new spr\u24F1((spr\u2158)base.ReservedHandle, A_0);
				num2 = 0;
				num = 3;
			}
			IL_87:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("あⵄ≆ⱈ㽊", a_));
			IL_EE:
			throw new ArgumentNullException(RecordTableEnumerator.b("≂㝄㕆Ո⩊⽌⩎㵐⁒", a_));
		}
		}
	}

	// Token: 0x06002C58 RID: 11352 RVA: 0x0018C960 File Offset: 0x0018B960
	private void ᜀ(List<sprᮐ> A_0, IWorksheet A_1)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
				{
					if (A_1 == null)
					{
						num = 6;
						continue;
					}
					if (true)
					{
					}
					bool flag = false;
					int num2 = 0;
					bool flag2 = false;
					Dictionary<string, CondFormatCollectionWrapper>.Enumerator enumerator = this.\u170D.GetEnumerator();
					num = 5;
					continue;
				}
				case 3:
					if (A_0.Count == 0)
					{
						num = 0;
						continue;
					}
					goto IL_450;
				case 4:
					return;
				case 5:
					goto IL_C2;
					try
					{
						for (;;)
						{
							IL_C2:
							num = 4;
							for (;;)
							{
								bool flag2;
								KeyValuePair<string, CondFormatCollectionWrapper> keyValuePair;
								Rectangle a_2;
								switch (num)
								{
								case 0:
									goto IL_284;
								case 2:
									if (!flag2)
									{
										num = 13;
										continue;
									}
									a_2 = keyValuePair.Value.ConditionalFormats.CellRectangles[0];
									num = 9;
									continue;
								case 3:
									goto IL_440;
								case 5:
									try
									{
										num = 8;
										for (;;)
										{
											switch (num)
											{
											case 0:
												num = 6;
												continue;
											case 1:
												flag2 = true;
												num = 13;
												continue;
											case 2:
												goto IL_274;
											case 3:
											{
												List<sprᮐ>.Enumerator enumerator2;
												if (!enumerator2.MoveNext())
												{
													num = 5;
													continue;
												}
												sprᮐ sprᮐ = enumerator2.Current;
												num = 12;
												continue;
											}
											case 4:
												goto IL_258;
											case 5:
												goto IL_268;
											case 6:
											{
												bool flag;
												if (flag)
												{
													num = 9;
													continue;
												}
												int width;
												int num2 = width;
												num = 7;
												continue;
											}
											case 7:
												goto IL_258;
											case 9:
											{
												int height;
												int num2 = height;
												num = 4;
												continue;
											}
											case 10:
												goto IL_268;
											case 11:
											{
												spr\u2530 spr_u;
												Rectangle a_3;
												if (spr_u.ᜃ(a_3))
												{
													num = 0;
													continue;
												}
												break;
											}
											case 12:
											{
												sprᮐ sprᮐ;
												if (sprᮐ.ᜁ() == 0)
												{
													num = 1;
													continue;
												}
												Rectangle a_3 = sprᮐ.ᜀ(0);
												int width = a_3.Width;
												int height = a_3.Height;
												bool flag = width == 0;
												a_3.Width = 0;
												a_3.Height = 0;
												num = 11;
												continue;
											}
											}
											IL_1F2:
											num = 3;
											continue;
											goto IL_1F2;
											IL_258:
											flag2 = true;
											num = 10;
											continue;
											IL_268:
											num = 2;
										}
										IL_274:
										goto IL_2D0;
									}
									finally
									{
										List<sprᮐ>.Enumerator enumerator2;
										((IDisposable)enumerator2).Dispose();
									}
									goto IL_284;
									IL_2D0:
									num = 2;
									continue;
								case 6:
								{
									Dictionary<string, CondFormatCollectionWrapper>.Enumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 12;
										continue;
									}
									keyValuePair = enumerator.Current;
									spr\u2530 spr_u = new spr\u2530();
									IXLSRange ixlsrange = keyValuePair.Value.Range;
									Rectangle rectangle = sprṔ.ᜀ(ixlsrange, true);
									spr_u.ᜄ(new Rectangle[]
									{
										rectangle
									});
									num = 10;
									continue;
								}
								case 7:
									goto IL_284;
								case 8:
								{
									List<sprᮐ>.Enumerator enumerator2 = A_0.GetEnumerator();
									num = 5;
									continue;
								}
								case 9:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_C2;
									default:
									{
										if (false)
										{
										}
										bool flag;
										if (flag)
										{
											num = 11;
											continue;
										}
										int num2;
										a_2.Width += num2;
										num = 0;
										continue;
									}
									}
									break;
								case 10:
								{
									IXLSRange ixlsrange;
									if (ixlsrange.Worksheet.Name.Equals(A_1.Name))
									{
										num = 8;
										continue;
									}
									break;
								}
								case 11:
								{
									int num2;
									a_2.Height += num2;
									num = 7;
									continue;
								}
								case 12:
									num = 3;
									continue;
								case 13:
									goto IL_2EE;
								}
								goto IL_10C;
								IL_284:
								keyValuePair.Value.ConditionalFormats.ᜀ(a_2);
								flag2 = false;
								num = 1;
								continue;
								IL_2A7:
								num = 6;
								continue;
								IL_10C:
								goto IL_2A7;
							}
						}
						IL_2EE:
						throw new ArgumentException(RecordTableEnumerator.b("渹吻嬽怿ㅁ㑃⍅⭇⍉⩋❍㕏㙑瑓㕕㝗㑙㡛㝝ᑟୡୣࡥ१٩䱫࡭Ὧqᥳ᝵౷婹๻ώꚅ黎겋ﺏ늑ﶓﮙ쒟芡쎥얧\udaa9삫쾭쒯ힱ钳\udbb5\ud9b7좹ힻ\udbbd늿뛃Ʂꛇ귉꧋", a_));
						IL_440:
						return;
					}
					finally
					{
						Dictionary<string, CondFormatCollectionWrapper>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					goto IL_450;
				case 6:
					goto IL_46E;
				}
				if (this.\u170D == null)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
				IL_450:
				num = 2;
			}
			return;
			IL_46E:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤹吻嬽┿㙁", a_));
		}
		}
	}

	// Token: 0x06002C59 RID: 11353 RVA: 0x0018CE18 File Offset: 0x0018BE18
	private void ᜀ(IWorkbook A_0, IWorksheet A_1, IList<long> A_2, IList<sprᮐ> A_3)
	{
		int a_ = 5;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_C9:
			num = 8;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 0;
				break;
			}
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_129;
			case 2:
				if (A_3 == null)
				{
					num = 6;
					continue;
				}
				num = 5;
				continue;
			case 3:
				goto IL_79;
			case 4:
				goto IL_8D;
			case 5:
			{
				if (A_1 == null)
				{
					num = 9;
					continue;
				}
				int num2 = 0;
				int count = A_2.Count;
				num = 4;
				continue;
			}
			case 6:
				goto IL_C4;
			case 7:
				return;
			case 8:
			{
				int num2;
				int count;
				if (num2 >= count)
				{
					num = 7;
					continue;
				}
				long a_2 = A_2[num2];
				sprᮐ a_3 = A_3[num2];
				this.ᜀ(A_0, A_1, a_2, a_3);
				num2++;
				num = 1;
				continue;
			}
			case 9:
				goto IL_153;
			}
			if (A_2 == null)
			{
				num = 3;
			}
			else
			{
				if (true)
				{
				}
				num = 2;
			}
		}
		IL_79:
		throw new ArgumentNullException(RecordTableEnumerator.b("娺似䴾ɀ♂⥄⭆㩈", a_));
		IL_8D:
		goto IL_C9;
		IL_C4:
		throw new ArgumentNullException(RecordTableEnumerator.b("娺似䴾ፀ♂㙄㉆╈㽊Ὄ⹎㽐㑒ご⑖", a_));
		IL_129:
		goto IL_C9;
		IL_153:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
	}

	// Token: 0x06002C5A RID: 11354 RVA: 0x0018CF94 File Offset: 0x0018BF94
	private void ᜀ(IWorkbook A_0, IWorksheet A_1, long A_2, sprᮐ A_3)
	{
		switch (0)
		{
		default:
			if (true)
			{
			}
			for (;;)
			{
				IWorksheets worksheets = A_0.Worksheets;
				int num = 0;
				int count = worksheets.Count;
				int num2 = 0;
				for (;;)
				{
					int num3;
					int count2;
					ICharts charts;
					switch (num2)
					{
					case 0:
						goto IL_110;
					case 1:
						return;
					case 2:
						if (num >= count)
						{
							num2 = 5;
							continue;
						}
						this.ᜀ(worksheets[num], A_1, A_2, A_3);
						num++;
						num2 = 3;
						continue;
					case 3:
						goto IL_110;
					case 4:
						goto IL_D1;
					case 5:
						goto IL_8E;
					case 6:
					{
						if (num3 >= count2)
						{
							num2 = 1;
							continue;
						}
						IChart a_ = charts[num3];
						this.ᜀ(a_, A_1, A_2, A_3);
						num3++;
						num2 = 4;
						continue;
					}
					case 7:
						goto IL_D1;
					}
					break;
					IL_8E:
					charts = A_0.Charts;
					num3 = 0;
					count2 = charts.Count;
					num2 = 7;
					continue;
					IL_D1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8E;
					default:
						if (false)
						{
						}
						num2 = 6;
						continue;
					}
					IL_110:
					num2 = 2;
				}
			}
			return;
		}
	}

	// Token: 0x06002C5B RID: 11355 RVA: 0x0018D0D4 File Offset: 0x0018C0D4
	private void ᜀ(IWorksheet A_0, IWorksheet A_1, long A_2, sprᮐ A_3)
	{
		int a_ = 2;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_CD:
			num = 6;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 4;
				break;
			}
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_79;
			case 2:
				goto IL_C8;
			case 3:
				goto IL_157;
			case 5:
				goto IL_99;
			case 6:
			{
				int num2;
				int count;
				if (num2 >= count)
				{
					num = 0;
					continue;
				}
				IChartShapes charts;
				IChartShape a_2 = charts[num2];
				this.ᜀ(a_2, A_1, A_2, A_3);
				num2++;
				num = 9;
				continue;
			}
			case 7:
			{
				if (A_1 == null)
				{
					num = 3;
					continue;
				}
				IChartShapes charts = ((XlsWorksheet)A_0).Charts;
				int num2 = 0;
				int count = charts.Count;
				num = 5;
				continue;
			}
			case 8:
				if (A_3 == null)
				{
					num = 2;
					continue;
				}
				if (true)
				{
				}
				num = 7;
				continue;
			case 9:
				goto IL_125;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 8;
			}
		}
		IL_79:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬷刹夻嬽㐿", a_));
		IL_99:
		goto IL_CD;
		IL_C8:
		throw new ArgumentNullException(RecordTableEnumerator.b("娷伹唻刽␿❁㙃", a_));
		IL_125:
		goto IL_CD;
		IL_157:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬷刹夻嬽㐿Ł⅃⩅⑇", a_));
	}

	// Token: 0x06002C5C RID: 11356 RVA: 0x0018D254 File Offset: 0x0018C254
	private void ᜀ(IChart A_0, IWorksheet A_1, long A_2, sprᮐ A_3)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 16;
			for (;;)
			{
				XlsChartSeries xlsChartSeries;
				IXLSRange a_2;
				IChartSerie chartSerie;
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_163;
				case 1:
					goto IL_187;
				case 2:
					xlsChartSeries = ((XlsChart)A_0).Series;
					num = 17;
					continue;
				case 3:
					goto IL_1C5;
				case 4:
					goto IL_1D7;
				case 5:
					goto IL_11D;
				case 6:
					goto IL_89;
				case 7:
					if (this.ᜀ(a_2, A_1, A_2))
					{
						num = 18;
						continue;
					}
					goto IL_1D7;
				case 8:
					if (!this.ᜀ(a_2, A_1, A_2))
					{
						goto IL_11D;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1D7;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 9:
					chartSerie.Bubbles = A_3.ᜀ(A_1);
					num = 3;
					continue;
				case 10:
					if (A_0 is XlsChart)
					{
						num = 2;
						continue;
					}
					xlsChartSeries = ((XlsChartShape)A_0).Series;
					num = 1;
					continue;
				case 11:
					chartSerie.Values = A_3.ᜀ(A_1);
					num = 5;
					continue;
				case 12:
					if (num2 >= count)
					{
						num = 19;
						continue;
					}
					chartSerie = xlsChartSeries[num2];
					a_2 = chartSerie.Values;
					num = 8;
					continue;
				case 13:
					if (A_3 == null)
					{
						num = 20;
						continue;
					}
					xlsChartSeries = null;
					num = 10;
					continue;
				case 14:
					goto IL_163;
				case 15:
					if (this.ᜀ(a_2, A_1, A_2))
					{
						num = 9;
						continue;
					}
					goto IL_1C5;
				case 17:
					goto IL_187;
				case 18:
					chartSerie.CategoryLabels = A_3.ᜀ(A_1);
					num = 4;
					continue;
				case 19:
					return;
				case 20:
					goto IL_11B;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 13;
				continue;
				IL_11D:
				if (true)
				{
				}
				a_2 = chartSerie.CategoryLabels;
				num = 7;
				continue;
				IL_163:
				num = 12;
				continue;
				IL_187:
				num2 = 0;
				count = xlsChartSeries.Count;
				num = 0;
				continue;
				IL_1C5:
				num2++;
				num = 14;
				continue;
				IL_1D7:
				a_2 = chartSerie.Bubbles;
				num = 15;
			}
			IL_89:
			throw new ArgumentNullException(RecordTableEnumerator.b("帼圾⁀ㅂㅄ", a_));
			IL_11B:
			throw new ArgumentNullException(RecordTableEnumerator.b("弼䨾⡀⽂⅄≆㭈", a_));
		}
		}
	}

	// Token: 0x06002C5D RID: 11357 RVA: 0x0018D510 File Offset: 0x0018C510
	private bool ᜀ(IXLSRange A_0, IWorksheet A_1, long A_2)
	{
		int a_ = 18;
		int num = 0;
		int num3;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return false;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BA;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 3:
			{
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				int num2 = sprṔ.ᜁ(A_2);
				num3 = sprṔ.ᜀ(A_2);
				num = 9;
				continue;
			}
			case 4:
				goto IL_DD;
			case 5:
				if (A_0.Column == num3)
				{
					num = 6;
					continue;
				}
				goto IL_120;
			case 6:
				goto IL_10A;
			case 7:
			{
				int num2;
				if (A_0.LastRow == num2)
				{
					num = 8;
					continue;
				}
				goto IL_120;
			}
			case 8:
				num = 5;
				continue;
			case 9:
			{
				int num2;
				if (A_0.Row == num2)
				{
					num = 2;
					continue;
				}
				goto IL_120;
			}
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			IL_BA:
			num = 3;
		}
		return false;
		IL_DD:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉⥋⭍⑏", a_));
		IL_10A:
		return A_0.LastColumn == num3;
		IL_120:
		if (true)
		{
		}
		return false;
	}

	// Token: 0x06002C5E RID: 11358 RVA: 0x0018D648 File Offset: 0x0018C648
	private IList<long> ᜀ(IWorksheet A_0, List<int> A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				List<long> list;
				IDictionaryEnumerator enumerator;
				int count;
				switch (num)
				{
				case 0:
					try
					{
						num = 8;
						for (;;)
						{
							BiffRecordRaw biffRecordRaw;
							string text;
							switch (num)
							{
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_28A;
								default:
								{
									if (false)
									{
									}
									long item;
									list.Add(item);
									num = 9;
									continue;
								}
								}
								break;
							case 2:
							{
								if (!enumerator.MoveNext())
								{
									num = 11;
									continue;
								}
								DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
								biffRecordRaw = (BiffRecordRaw)dictionaryEntry.Value;
								long item = (long)dictionaryEntry.Key;
								TBIFFRecord typeCode = biffRecordRaw.TypeCode;
								num = 14;
								continue;
							}
							case 3:
							{
								int num2;
								if (num2 >= 0)
								{
									num = 17;
									continue;
								}
								break;
							}
							case 4:
								if (text.StartsWith(this.ᜊ))
								{
									num = 1;
									continue;
								}
								break;
							case 5:
							{
								TBIFFRecord typeCode;
								if (typeCode != TBIFFRecord.LabelSST)
								{
									num = 16;
									continue;
								}
								num = 10;
								continue;
							}
							case 7:
								goto IL_28A;
							case 10:
								if (count > 0)
								{
									num = 15;
									continue;
								}
								break;
							case 11:
								num = 7;
								continue;
							case 12:
								num = 0;
								continue;
							case 13:
							{
								TBIFFRecord typeCode;
								if (typeCode != TBIFFRecord.Label)
								{
									num = 12;
									continue;
								}
								goto IL_D7;
							}
							case 14:
							{
								TBIFFRecord typeCode;
								if (typeCode != TBIFFRecord.RString)
								{
									num = 18;
									continue;
								}
								goto IL_D7;
							}
							case 15:
							{
								spr\u1C7C spr_u1C7C = (spr\u1C7C)biffRecordRaw;
								int item2 = spr_u1C7C.ᜁ();
								int num2 = A_1.BinarySearch(item2);
								num = 3;
								continue;
							}
							case 16:
								num = 13;
								continue;
							case 17:
							{
								long item;
								list.Add(item);
								num = 6;
								continue;
							}
							case 18:
								num = 5;
								continue;
							}
							goto IL_AF;
							IL_D7:
							spr\u22BB spr_u22BB = (spr\u22BB)biffRecordRaw;
							text = spr_u22BB.ᜀ();
							num = 4;
							continue;
							IL_115:
							num = 2;
							continue;
							IL_AF:
							goto IL_115;
						}
						IL_28A:
						return list;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable = enumerator as IDisposable;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable != null)
									{
										num = 2;
										continue;
									}
									goto IL_2D7;
								case 1:
									goto IL_2D5;
								case 2:
									disposable.Dispose();
									num = 1;
									continue;
								}
								break;
							}
						}
						IL_2D5:
						IL_2D7:;
					}
					goto IL_2D8;
				case 1:
					goto IL_4C;
				case 2:
					if (true)
					{
					}
					break;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				IL_2D8:
				XlsWorksheet xlsWorksheet = (XlsWorksheet)A_0;
				XlsCellRecordCollection cellRecords = xlsWorksheet.CellRecords;
				list = new List<long>();
				count = A_1.Count;
				enumerator = cellRecords.GetEnumerator();
				num = 0;
			}
			IL_4C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨸区堼娾㕀", a_));
		}
		}
	}

	// Token: 0x06002C5F RID: 11359 RVA: 0x0018D99C File Offset: 0x0018C99C
	private sprᮐ ᜀ(IWorksheet A_0, IList<long> A_1, int A_2, IMigrantRange A_3, UnknownVariableAction A_4)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (true)
					{
					}
					int count;
					if (A_2 > count - 1)
					{
						num = 7;
						continue;
					}
					goto IL_16E;
				}
				case 2:
					goto IL_B7;
				case 3:
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					num = 5;
					continue;
				case 4:
					if (A_2 >= 0)
					{
						num = 9;
						continue;
					}
					goto IL_BC;
				case 5:
				{
					if (A_3 == null)
					{
						num = 6;
						continue;
					}
					int count = A_1.Count;
					num = 4;
					continue;
				}
				case 6:
					goto IL_139;
				case 7:
					goto IL_118;
				case 8:
					goto IL_67;
				case 9:
					num = 0;
					continue;
				}
				if (A_0 == null)
				{
					num = 8;
				}
				else
				{
					num = 3;
				}
			}
			IL_67:
			goto IL_DE;
			IL_B7:
			throw new ArgumentNullException(RecordTableEnumerator.b("╃㑅㩇ॉ⥋≍㱏⅑", a_));
			IL_BC:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⵃ", a_), RecordTableEnumerator.b("ቃ❅⑇㽉⥋湍㍏㍑㩓㡕㝗⹙籛㱝՟䉡ࡣͥ᭧ᥩ䱫ᩭᡯ፱ᩳ噵䡷婹ᵻၽꊁ늑ﺕ聯벛춡톣좥\udca7誩膫躭膯鲱", a_));
			IL_DE:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝃⹅ⵇ⽉㡋", a_));
			IL_118:
			goto IL_BC;
			IL_139:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_DE;
			default:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("⥃⽅⽇㡉ⵋ⁍⑏Q㕓㡕㽗㽙", a_));
			}
			IL_16E:
			XlsWorksheet xlsWorksheet = (XlsWorksheet)A_0;
			long num2 = A_1[A_2];
			string stringValue = xlsWorksheet.GetStringValue(num2);
			string a_2 = stringValue;
			string a_4;
			string a_3 = this.ᜀ(ref stringValue, out a_4);
			spr\u2064 spr_u;
			IList a_5 = this.ᜀ(a_4, out spr_u);
			spr_u.ᜀ(A_2);
			sprᮐ sprᮐ = new sprᮐ();
			spr_u.ᜀ(a_2);
			int iRow = sprṔ.ᜁ(num2);
			int iColumn = sprṔ.ᜀ(num2);
			A_3.ResetRowColumn(iRow, iColumn);
			this.ᜀ(a_3, stringValue, A_0, A_3, A_1, a_5, spr_u, sprᮐ, A_4);
			return sprᮐ;
		}
		}
	}

	// Token: 0x06002C60 RID: 11360 RVA: 0x0018DB94 File Offset: 0x0018CB94
	private string ᜀ(ref string A_0, out string A_1)
	{
		int a_ = 18;
		int num2;
		string result;
		for (;;)
		{
			A_1 = null;
			int num = 13;
			for (;;)
			{
				string text;
				switch (num)
				{
				case 0:
					A_1 = A_0.Substring(num2 + 1, A_0.Length - num2 - 2);
					A_0 = A_0.Substring(0, num2);
					num = 15;
					continue;
				case 1:
					if (A_0.EndsWith(RecordTableEnumerator.b("慇", a_)))
					{
						num = 0;
						continue;
					}
					goto IL_139;
				case 2:
					text = A_0;
					goto IL_B6;
				case 3:
					if (true)
					{
					}
					num = 1;
					continue;
				case 4:
					goto IL_6C;
				case 5:
					goto IL_CC;
				case 6:
					goto IL_193;
				case 7:
					if (A_0.Length != 0)
					{
						num2 = A_0.LastIndexOf('(');
						num = 11;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_17C;
					default:
						if (false)
						{
						}
						num = 14;
						continue;
					}
					break;
				case 8:
					A_0 = A_0.Remove(0, this.ᜊ.Length);
					num = 6;
					continue;
				case 9:
					num = 2;
					continue;
				case 10:
					if (A_0.StartsWith(this.ᜊ))
					{
						num = 8;
						continue;
					}
					goto IL_193;
				case 11:
					if (num2 > 0)
					{
						goto IL_17C;
					}
					goto IL_139;
				case 12:
					if (num2 < 0)
					{
						num = 9;
						continue;
					}
					num = 16;
					continue;
				case 13:
					if (A_0 == null)
					{
						num = 4;
						continue;
					}
					num = 7;
					continue;
				case 14:
					goto IL_111;
				case 15:
					goto IL_139;
				case 16:
					text = A_0.Substring(0, num2);
					goto IL_B6;
				}
				break;
				IL_B6:
				result = text;
				num = 5;
				continue;
				IL_139:
				num = 10;
				continue;
				IL_17C:
				num = 3;
				continue;
				IL_193:
				num2 = A_0.IndexOf(RecordTableEnumerator.b("晇", a_));
				num = 12;
			}
		}
		IL_6C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭇㹉㹋ᩍ㕏⩑⁓", a_));
		IL_CC:
		A_0 = ((num2 >= 0) ? A_0.Remove(0, num2 + 1) : string.Empty);
		return result;
		IL_111:
		throw new ArgumentException(RecordTableEnumerator.b("㭇㹉㹋ᩍ㕏⩑⁓癕畗穙⽛⩝቟ୡ੣ť䡧३൫mṯᵱs噵᩷ό屻᭽ﾅꚇ", a_));
	}

	// Token: 0x06002C61 RID: 11361 RVA: 0x0018DDF0 File Offset: 0x0018CDF0
	private void ᜀ(string A_0, string A_1, IWorksheet A_2, IMigrantRange A_3, IList<long> A_4, IList A_5, spr\u2064 A_6, sprᮐ A_7, UnknownVariableAction A_8)
	{
		int a_ = 9;
		int num = 0;
		object empty;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 1:
				goto IL_F9;
			case 2:
				goto IL_CD;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_78;
				default:
					goto IL_6A;
				}
				break;
			case 4:
				if (A_0.Length == 0)
				{
					num = 2;
					continue;
				}
				this.ᜉ.TryGetValue(A_0, out empty);
				num = 8;
				continue;
			case 5:
				goto IL_80;
			case 6:
				num = 9;
				continue;
			case 7:
				num = 1;
				continue;
			case 8:
				if (empty == null)
				{
					num = 6;
					continue;
				}
				goto IL_151;
			case 9:
				switch (A_8)
				{
				case UnknownVariableAction.Exception:
					goto IL_FB;
				case UnknownVariableAction.Skip:
					return;
				case UnknownVariableAction.ReplaceBlank:
					empty = string.Empty;
					goto IL_78;
				default:
					num = 7;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			num = 4;
			continue;
			IL_78:
			num = 5;
		}
		IL_6A:
		if (false)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("䰾㕀ㅂፄ♆㭈≊ⱌⵎ㵐㙒", a_));
		IL_80:
		goto IL_151;
		IL_CD:
		throw new ArgumentException(RecordTableEnumerator.b("䰾㕀ㅂፄ♆㭈≊ⱌⵎ㵐㙒畔穖祘⡚⥜ⵞࡠൢɤ䝦੨੪ͬŮṰݲ啴ᕶᱸ孺᡼ቾﲄꦆ", a_));
		IL_F9:
		goto IL_151;
		IL_FB:
		throw new ArgumentOutOfRangeException(A_0, RecordTableEnumerator.b("樾⽀⡂⩄う❈歊ᭌ⹎⍐㩒㑔㕖㕘㹚絜繞", a_));
		IL_151:
		VariableTypeAction a_2 = VariableTypeAction.None;
		this.ᜌ.TryGetValue(A_0, out a_2);
		A_3.Text = string.Empty;
		this.ᜀ(empty, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, a_2);
	}

	// Token: 0x06002C62 RID: 11362 RVA: 0x0018DF84 File Offset: 0x0018CF84
	private void ᜀ(object A_0, string A_1, IWorksheet A_2, IMigrantRange A_3, IList<long> A_4, IList A_5, spr\u2064 A_6, sprᮐ A_7, UnknownVariableAction A_8, VariableTypeAction A_9)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1E2;
			case 1:
				if (this.ᜄ(A_0))
				{
					num = 0;
					continue;
				}
				num = 4;
				continue;
			case 2:
				goto IL_BC;
			case 4:
				if (this.ᜃ(A_0))
				{
					num = 2;
					continue;
				}
				num = 5;
				continue;
			case 5:
				if (this.ᜂ(A_0))
				{
					num = 7;
					continue;
				}
				num = 11;
				continue;
			case 6:
				goto IL_E3;
			case 7:
				goto IL_98;
			case 8:
				goto IL_51;
			case 9:
				if (this.ᜅ(A_0))
				{
					num = 6;
					continue;
				}
				num = 1;
				continue;
			case 10:
				goto IL_10A;
			case 11:
				if (this.ᜀ(A_0))
				{
					num = 10;
					continue;
				}
				goto IL_1E4;
			}
			if (this.ᜁ(A_0))
			{
				num = 8;
			}
			else
			{
				num = 9;
			}
		}
		IL_51:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_10C;
		}
		if (false)
		{
		}
		IL_10C:
		this.ᜀ((DataView)A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9);
		return;
		IL_98:
		this.ᜀ((DataTable)A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9);
		return;
		IL_BC:
		this.ᜀ((DataSet)A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9);
		return;
		IL_E3:
		this.ᜀ((IList)A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9);
		return;
		IL_10A:
		if (true)
		{
		}
		this.ᜀ((DataColumn)A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9);
		return;
		IL_1E2:
		this.ᜀ((ICollection)A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8);
		return;
		IL_1E4:
		this.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, null, null);
	}

	// Token: 0x06002C63 RID: 11363 RVA: 0x0018E18C File Offset: 0x0018D18C
	private void ᜀ(object A_0, string A_1, IWorksheet A_2, IMigrantRange A_3, IList<long> A_4, IList A_5, spr\u2064 A_6, sprᮐ A_7, UnknownVariableAction A_8, string A_9, Type A_10)
	{
		int a_ = 3;
		int num = 9;
		VariableTypeAction a_2;
		for (;;)
		{
			object obj;
			switch (num)
			{
			case 0:
				goto IL_F7;
			case 1:
				if (A_1.Length == 0)
				{
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_92;
				default:
					goto IL_22E;
				}
				break;
			case 2:
				obj = this.ᜀ(A_0 as string, A_10);
				goto IL_16B;
			case 3:
				obj = A_0;
				goto IL_16B;
			case 4:
				goto IL_200;
			case 5:
				A_3.Value2 = ((A_0 != null) ? A_0 : RecordTableEnumerator.b("眸渺焼猾", a_));
				num = 14;
				continue;
			case 6:
				if (A_10 != null)
				{
					num = 19;
					continue;
				}
				num = 10;
				continue;
			case 7:
				if (!(A_0 is string))
				{
					num = 8;
					continue;
				}
				num = 2;
				continue;
			case 8:
				num = 3;
				continue;
			case 10:
				A_3.Value2 = ((A_0 != null) ? A_0 : RecordTableEnumerator.b("眸渺焼猾", a_));
				num = 4;
				continue;
			case 11:
				goto IL_74;
			case 12:
				a_2 = VariableTypeAction.DetectNumberFormat;
				A_3.NumberFormat = A_9;
				num = 16;
				continue;
			case 13:
				goto IL_92;
			case 14:
				if (A_9 != null)
				{
					num = 12;
					continue;
				}
				goto IL_202;
			case 15:
				if (A_1 != null)
				{
					num = 17;
					continue;
				}
				goto IL_F7;
			case 16:
				goto IL_1C9;
			case 17:
				num = 1;
				continue;
			case 18:
				goto IL_A1;
			case 19:
				a_2 = VariableTypeAction.DetectDataType;
				num = 7;
				continue;
			}
			if (A_2 == null)
			{
				num = 11;
				continue;
			}
			num = 13;
			continue;
			IL_92:
			if (A_7 == null)
			{
				num = 18;
				continue;
			}
			a_2 = VariableTypeAction.None;
			num = 15;
			continue;
			IL_F7:
			num = 6;
			continue;
			IL_16B:
			A_0 = obj;
			num = 5;
		}
		IL_74:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨸区堼娾㕀", a_));
		IL_A1:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬸为吼匾╀♂㝄", a_));
		IL_1C9:
		IL_200:
		IL_202:
		A_7.ᜁ(A_3.Row, A_3.Column);
		return;
		IL_22E:
		if (true)
		{
		}
		if (false)
		{
		}
		A_0 = this.ᜀ(A_0, ref A_1);
		this.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, a_2);
	}

	// Token: 0x06002C64 RID: 11364 RVA: 0x0018E3F8 File Offset: 0x0018D3F8
	internal void ᜀ(string[] A_0, out List<string> A_1, out List<Type> A_2, VariableTypeAction A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				A_1 = new List<string>();
				A_2 = new List<Type>();
				int num = 5;
				for (;;)
				{
					string item;
					Type item2;
					int num2;
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						goto IL_11D;
					case 2:
						goto IL_B9;
					case 3:
						if (true)
						{
						}
						goto IL_B9;
					case 4:
					{
						bool flag;
						if (flag)
						{
							num = 1;
							continue;
						}
						goto IL_6F;
					}
					case 5:
					{
						if (A_3 == VariableTypeAction.None)
						{
							num = 6;
							continue;
						}
						item = null;
						item2 = null;
						bool flag = A_3 == VariableTypeAction.DetectNumberFormat;
						num2 = 0;
						num = 3;
						continue;
					}
					case 6:
						return;
					case 7:
						return;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11D;
						default:
						{
							if (false)
							{
							}
							if (num2 >= A_0.Length)
							{
								num = 7;
								continue;
							}
							string a_ = A_0[num2];
							bool flag;
							this.ᜀ(a_, ref item, ref item2, flag);
							num = 4;
							continue;
						}
						}
						break;
					}
					break;
					IL_6F:
					A_2.Add(item2);
					num2++;
					num = 2;
					continue;
					IL_B9:
					num = 8;
					continue;
					IL_11D:
					A_1.Add(item);
					num = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06002C65 RID: 11365 RVA: 0x0018E53C File Offset: 0x0018D53C
	internal object ᜀ(string A_0, Type A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			bool flag;
			int num3;
			double num6;
			DateTime now;
			for (;;)
			{
				string s = A_0;
				int num = A_0.IndexOf(RecordTableEnumerator.b("浇", a_));
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_210;
					case 1:
						if (bool.TryParse(A_0, out flag))
						{
							num2 = 0;
							continue;
						}
						return A_0;
					case 2:
						goto IL_B7;
					case 3:
						if (int.TryParse(s, NumberStyles.Any, null, out num3))
						{
							num2 = 21;
							continue;
						}
						return A_0;
					case 4:
						goto IL_2AC;
					case 5:
					{
						double num4 = 0.0;
						double num5 = 0.0;
						string[] array;
						bool flag2 = double.TryParse(array[0], out num4);
						bool flag3 = double.TryParse(array[1], out num5);
						num2 = 27;
						continue;
					}
					case 6:
					{
						string[] array;
						if (array.Length == 2)
						{
							num2 = 5;
							continue;
						}
						goto IL_2E2;
					}
					case 7:
						goto IL_2E2;
					case 8:
						if (A_1 == typeof(int))
						{
							num2 = 19;
							continue;
						}
						num2 = 20;
						continue;
					case 9:
						goto IL_392;
					case 10:
						if (double.TryParse(s, NumberStyles.Any, null, out num6))
						{
							num2 = 9;
							continue;
						}
						return A_0;
					case 11:
						now = DateTime.Now;
						num2 = 12;
						continue;
					case 12:
						if (DateTime.TryParse(A_0, null, DateTimeStyles.AdjustToUniversal, out now))
						{
							num2 = 24;
							continue;
						}
						return A_0;
					case 13:
						if (A_1 == typeof(DateTime))
						{
							num2 = 11;
							continue;
						}
						num2 = 23;
						continue;
					case 14:
						num2 = 15;
						continue;
					case 15:
					{
						bool flag3;
						if (flag3)
						{
							num2 = 22;
							continue;
						}
						goto IL_26A;
					}
					case 16:
					{
						string[] array = A_0.Split(new char[]
						{
							RecordTableEnumerator.b("杇", a_)[0]
						});
						num2 = 6;
						continue;
					}
					case 17:
						if (num != -1)
						{
							num2 = 16;
							continue;
						}
						goto IL_2E2;
					case 18:
						s = A_0.Remove(num);
						num2 = 4;
						continue;
					case 19:
						num3 = 0;
						num2 = 3;
						continue;
					case 20:
						if (A_1 == typeof(double))
						{
							num2 = 25;
							continue;
						}
						num2 = 13;
						continue;
					case 21:
						goto IL_3C5;
					case 22:
					{
						double num4;
						double num5;
						s = (num4 / num5).ToString();
						num2 = 7;
						continue;
					}
					case 23:
						if (A_1 == typeof(bool))
						{
							num2 = 28;
							continue;
						}
						return A_0;
					case 24:
						goto IL_33E;
					case 25:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B7;
						default:
							if (false)
							{
							}
							num6 = 0.0;
							num2 = 10;
							continue;
						}
						break;
					case 26:
						goto IL_2E2;
					case 27:
					{
						bool flag2;
						if (flag2)
						{
							num2 = 14;
							continue;
						}
						goto IL_26A;
					}
					case 28:
						flag = false;
						num2 = 1;
						continue;
					}
					break;
					IL_B7:
					if (num != -1)
					{
						num2 = 18;
						continue;
					}
					goto IL_2AC;
					IL_26A:
					s = A_0;
					num2 = 26;
					continue;
					IL_2AC:
					num = A_0.IndexOf(RecordTableEnumerator.b("杇", a_));
					num2 = 17;
					continue;
					IL_2E2:
					num2 = 8;
				}
			}
			IL_210:
			return flag;
			IL_33E:
			if (true)
			{
			}
			return now;
			IL_392:
			return num6;
			IL_3C5:
			return num3;
		}
		}
	}

	// Token: 0x06002C66 RID: 11366 RVA: 0x0018E914 File Offset: 0x0018D914
	internal object ᜀ(string A_0, ref string A_1, ref Type A_2, bool A_3)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			DateTime now;
			double num;
			bool flag;
			int num2;
			for (;;)
			{
				string s = A_0;
				now = DateTime.Now;
				num = 0.0;
				flag = false;
				num2 = 0;
				int num3 = 18;
				for (;;)
				{
					string[] array;
					int num6;
					switch (num3)
					{
					case 0:
					{
						double num4 = 0.0;
						double num5 = 0.0;
						bool flag2 = double.TryParse(array[0], out num4);
						bool flag3 = double.TryParse(array[1], out num5);
						num3 = 10;
						continue;
					}
					case 1:
						goto IL_1DC;
					case 2:
						goto IL_29E;
					case 3:
						if (num6 != -1)
						{
							num3 = 8;
							continue;
						}
						goto IL_29E;
					case 4:
					{
						bool flag3;
						if (flag3)
						{
							num3 = 29;
							continue;
						}
						goto IL_27C;
					}
					case 5:
						goto IL_38A;
					case 6:
						goto IL_D8;
					case 7:
						if (double.TryParse(s, NumberStyles.Any, null, out num))
						{
							num3 = 27;
							continue;
						}
						num3 = 11;
						continue;
					case 8:
						s = A_0.Remove(num6);
						num3 = 2;
						continue;
					case 9:
						goto IL_38A;
					case 10:
					{
						bool flag2;
						if (flag2)
						{
							num3 = 25;
							continue;
						}
						goto IL_27C;
					}
					case 11:
						if (int.TryParse(s, NumberStyles.Currency, null, out num2))
						{
							num3 = 20;
							continue;
						}
						num3 = 13;
						continue;
					case 12:
						num3 = 22;
						continue;
					case 13:
						if (DateTime.TryParse(A_0, null, DateTimeStyles.AdjustToUniversal, out now))
						{
							num3 = 12;
							continue;
						}
						num3 = 15;
						continue;
					case 14:
						if (num6 != -1)
						{
							num3 = 21;
							continue;
						}
						goto IL_38A;
					case 15:
						if (bool.TryParse(A_0, out flag))
						{
							num3 = 1;
							continue;
						}
						return A_0;
					case 16:
						goto IL_174;
					case 17:
						if (A_3)
						{
							if (true)
							{
							}
							num3 = 24;
							continue;
						}
						goto IL_DD;
					case 18:
						if (A_0 == null)
						{
							num3 = 6;
							continue;
						}
						num6 = A_0.IndexOf(RecordTableEnumerator.b("ጵ", a_));
						num3 = 3;
						continue;
					case 19:
						if (A_3)
						{
							num3 = 30;
							continue;
						}
						goto IL_F1;
					case 20:
						num3 = 17;
						continue;
					case 21:
						goto IL_104;
					case 22:
						if (A_3)
						{
							num3 = 26;
							continue;
						}
						goto IL_256;
					case 23:
						goto IL_200;
					case 24:
						A_1 = this.ᜆ.InnerFormats.ᜅ(A_0);
						num3 = 31;
						continue;
					case 25:
						num3 = 4;
						continue;
					case 26:
						A_1 = this.ᜆ.InnerFormats.ᜇ(A_0);
						num3 = 16;
						continue;
					case 27:
						num3 = 19;
						continue;
					case 28:
						if (array.Length == 2)
						{
							num3 = 0;
							continue;
						}
						goto IL_38A;
					case 29:
					{
						double num4;
						double num5;
						s = (num4 / num5).ToString();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_104;
						default:
							if (false)
							{
							}
							num3 = 9;
							continue;
						}
						break;
					}
					case 30:
						A_1 = this.ᜆ.InnerFormats.ᜅ(A_0);
						num3 = 23;
						continue;
					case 31:
						goto IL_1B2;
					}
					break;
					IL_104:
					array = A_0.Split(new char[]
					{
						RecordTableEnumerator.b("ᤵ", a_)[0]
					});
					num3 = 28;
					continue;
					IL_27C:
					s = A_0;
					num3 = 5;
					continue;
					IL_29E:
					num6 = A_0.IndexOf(RecordTableEnumerator.b("ᤵ", a_));
					num3 = 14;
					continue;
					IL_38A:
					num3 = 7;
				}
			}
			IL_D8:
			return RecordTableEnumerator.b("砵洷瘹瀻", a_);
			IL_DD:
			A_2 = typeof(int);
			return num2;
			IL_F1:
			A_2 = typeof(double);
			return num;
			IL_174:
			goto IL_256;
			IL_1B2:
			goto IL_DD;
			IL_1DC:
			A_2 = typeof(bool);
			return flag;
			IL_200:
			goto IL_F1;
			IL_256:
			A_2 = typeof(DateTime);
			return now;
		}
		}
	}

	// Token: 0x06002C67 RID: 11367 RVA: 0x0018ED90 File Offset: 0x0018DD90
	private object ᜀ(object A_0, ref string A_1)
	{
		int a_ = 3;
		PropertyInfo property;
		for (;;)
		{
			IL_09:
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_60;
				case 1:
				{
					if (A_1.Length == 0)
					{
						num = 3;
						continue;
					}
					string name = this.ᜀ(ref A_1);
					Type type = A_0.GetType();
					property = type.GetProperty(name);
					num = 2;
					continue;
				}
				case 2:
					if (true)
					{
					}
					if (property == null)
					{
						num = 5;
						continue;
					}
					goto IL_135;
				case 3:
					goto IL_8F;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 5:
					goto IL_11F;
				case 6:
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					num = 1;
					continue;
				case 7:
					goto IL_A5;
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					num = 6;
				}
			}
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("伸娺儼䨾⑀", a_));
		IL_8F:
		throw new ArgumentException(RecordTableEnumerator.b("䨸伺似款⑀㭂ㅄ杆摈歊㹌㭎⍐㩒㭔ざ祘㡚㱜ㅞའౢᅤ䝦୨๪䵬੮ᱰͲŴ๶坸", a_));
		IL_A5:
		throw new ArgumentNullException(RecordTableEnumerator.b("䨸伺似款⑀㭂ㅄ", a_));
		IL_11F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䨸伺似款⑀㭂ㅄ", a_), RecordTableEnumerator.b("椸䤺刼伾⑀ㅂㅄ㹆楈⡊ⱌⅎ癐❒畔㕖㱘筚㭜ぞᑠൢŤ䥦䥨", a_));
		IL_135:
		A_0 = property.GetValue(A_0, null);
		return A_0;
	}

	// Token: 0x06002C68 RID: 11368 RVA: 0x0018EEE0 File Offset: 0x0018DEE0
	private void ᜀ(IList A_0, string A_1, IWorksheet A_2, IMigrantRange A_3, IList<long> A_4, IList A_5, spr\u2064 A_6, sprᮐ A_7, UnknownVariableAction A_8, VariableTypeAction A_9)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 22;
			for (;;)
			{
				string a_2;
				int num2;
				Type a_3;
				switch (num)
				{
				case 0:
				{
					List<Type> list;
					if (list.Count > 0)
					{
						num = 4;
						continue;
					}
					goto IL_9E;
				}
				case 1:
				{
					List<string> list2;
					a_2 = list2[0];
					num = 7;
					continue;
				}
				case 2:
				{
					int count;
					if (num2 >= count)
					{
						num = 10;
						continue;
					}
					num = 21;
					continue;
				}
				case 3:
				{
					int count;
					if (count == 0)
					{
						num = 6;
						continue;
					}
					List<string> list2 = new List<string>();
					List<Type> list = new List<Type>();
					num = 14;
					continue;
				}
				case 4:
				{
					List<Type> list;
					a_3 = list[0];
					num = 18;
					continue;
				}
				case 5:
					goto IL_149;
				case 6:
					return;
				case 7:
					goto IL_9E;
				case 8:
				{
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					int count = A_0.Count;
					a_2 = null;
					a_3 = null;
					num = 3;
					continue;
				}
				case 9:
					return;
				case 10:
					return;
				case 11:
					goto IL_175;
				case 12:
				{
					List<Type> list;
					if (list.Count > 0)
					{
						num = 23;
						continue;
					}
					goto IL_175;
				}
				case 13:
					if (true)
					{
					}
					goto IL_22D;
				case 14:
					if (A_0[0] is string)
					{
						num = 17;
						continue;
					}
					goto IL_9E;
				case 15:
					goto IL_98;
				case 16:
				{
					List<string> list2;
					a_2 = list2[num2];
					num = 11;
					continue;
				}
				case 17:
				{
					int count;
					string[] array = new string[count];
					A_0.CopyTo(array, 0);
					List<Type> list;
					List<string> list2;
					this.ᜀ(array, out list2, out list, A_9);
					num = 0;
					continue;
				}
				case 18:
				{
					List<string> list2;
					if (list2.Count > 0)
					{
						num = 1;
						continue;
					}
					goto IL_9E;
				}
				case 19:
				{
					List<string> list2;
					if (list2.Count > 0)
					{
						num = 16;
						continue;
					}
					goto IL_175;
				}
				case 20:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_124;
					default:
						if (false)
						{
						}
						goto IL_22D;
					}
					break;
				case 21:
					if (!this.ᜀ(A_2, A_3, A_4, A_5, A_6))
					{
						num = 9;
						continue;
					}
					num = 12;
					continue;
				case 23:
				{
					List<Type> list;
					a_3 = list[num2];
					num = 19;
					continue;
				}
				}
				if (A_2 == null)
				{
					num = 15;
					continue;
				}
				goto IL_124;
				IL_9E:
				object a_4 = A_0[0];
				this.ᜀ(a_4, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, a_2, a_3);
				a_2 = null;
				a_3 = null;
				num2 = 1;
				num = 13;
				continue;
				IL_124:
				num = 8;
				continue;
				IL_175:
				a_4 = A_0[num2];
				this.ᜀ(a_4, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, a_2, a_3);
				num2++;
				num = 20;
				continue;
				IL_22D:
				num = 2;
			}
			IL_98:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩈⍊⡌⩎═", a_));
			IL_149:
			throw new ArgumentNullException(RecordTableEnumerator.b("㽈⩊⅌㩎㑐", a_));
		}
		}
	}

	// Token: 0x06002C69 RID: 11369 RVA: 0x0018F234 File Offset: 0x0018E234
	private void ᜀ(ICollection A_0, string A_1, IWorksheet A_2, IMigrantRange A_3, IList<long> A_4, IList A_5, spr\u2064 A_6, sprᮐ A_7, UnknownVariableAction A_8)
	{
		int a_ = 18;
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
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (A_7 == null)
						{
							num = 6;
							continue;
						}
						bool flag = true;
						IEnumerator enumerator = A_0.GetEnumerator();
						num = 3;
						continue;
					}
					case 1:
						goto IL_75;
					case 2:
						goto IL_B7;
					case 3:
						try
						{
							num = 3;
							for (;;)
							{
								bool flag;
								object a_2;
								switch (num)
								{
								case 0:
									if (!flag)
									{
										num = 8;
										continue;
									}
									goto IL_142;
								case 1:
									goto IL_194;
								case 2:
								{
									IEnumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 1;
										continue;
									}
									a_2 = enumerator.Current;
									num = 0;
									continue;
								}
								case 5:
									goto IL_1A0;
								case 6:
									goto IL_142;
								case 7:
									if (this.ᜀ(A_2, A_3, A_4, A_5, A_6))
									{
										num = 6;
										continue;
									}
									goto IL_194;
								case 8:
									num = 7;
									continue;
								}
								IL_126:
								num = 2;
								continue;
								goto IL_126;
								IL_142:
								this.ᜀ(a_2, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, null, null);
								flag = false;
								num = 4;
								continue;
								IL_194:
								num = 5;
							}
							IL_1A0:
							return;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator;
								IDisposable disposable = enumerator as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_1E7;
									case 1:
										if (disposable != null)
										{
											num = 2;
											continue;
										}
										goto IL_1E9;
									case 2:
										disposable.Dispose();
										num = 0;
										continue;
									}
									break;
								}
							}
							IL_1E7:
							IL_1E9:;
						}
						goto IL_1EA;
					case 4:
						if (A_0 == null)
						{
							num = 2;
							continue;
						}
						goto IL_1EA;
					case 6:
						goto IL_209;
					}
					if (A_2 == null)
					{
						num = 1;
						continue;
					}
					num = 4;
					continue;
					IL_1EA:
					num = 0;
				}
				IL_75:
				throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉⥋⭍⑏", a_));
				IL_B7:
				throw new ArgumentNullException(RecordTableEnumerator.b("㹇⭉⁋㭍㕏", a_));
				IL_209:
				break;
			}
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("⩇㽉╋≍㑏㝑♓", a_));
	}

	// Token: 0x06002C6A RID: 11370 RVA: 0x0018F480 File Offset: 0x0018E480
	private void ᜀ(DataSet A_0, string A_1, IWorksheet A_2, IMigrantRange A_3, IList<long> A_4, IList A_5, spr\u2064 A_6, sprᮐ A_7, UnknownVariableAction A_8, VariableTypeAction A_9)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 13;
			string text;
			int length;
			object value;
			DataTable a_2;
			for (;;)
			{
				PropertyInfo propertyInfo;
				switch (num)
				{
				case 0:
					goto IL_13F;
				case 1:
				{
					if (A_1.Length == 0)
					{
						num = 0;
						continue;
					}
					text = this.ᜁ(A_1);
					length = text.Length;
					int length2 = A_1.Length;
					num = 19;
					continue;
				}
				case 2:
					value = propertyInfo.GetValue(A_0, null);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2B8;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 3:
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					if (true)
					{
					}
					num = 11;
					continue;
				case 4:
					goto IL_90;
				case 5:
					goto IL_13D;
				case 6:
					num = 1;
					continue;
				case 7:
					goto IL_176;
				case 8:
					if (A_0.Tables.Count == 1)
					{
						num = 12;
						continue;
					}
					goto IL_281;
				case 9:
				{
					int length2;
					if (length2 <= length)
					{
						num = 14;
						continue;
					}
					num = 17;
					continue;
				}
				case 10:
				{
					int length2;
					if (length2 <= length)
					{
						num = 21;
						continue;
					}
					num = 7;
					continue;
				}
				case 11:
					if (A_1 != null)
					{
						num = 6;
						continue;
					}
					goto IL_13F;
				case 12:
					goto IL_168;
				case 14:
					num = 20;
					continue;
				case 15:
					goto IL_2F8;
				case 16:
					num = 9;
					continue;
				case 17:
					goto IL_9E;
				case 18:
					if ((a_2 = A_0.Tables[text]) != null)
					{
						num = 16;
						continue;
					}
					goto IL_302;
				case 19:
					goto IL_2B8;
				case 20:
					goto IL_1B0;
				case 21:
					num = 15;
					continue;
				}
				if (A_2 == null)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
				IL_2B8:
				if (this.ᜀ(A_0, text, out propertyInfo))
				{
					num = 2;
					continue;
				}
				num = 18;
				continue;
				IL_13F:
				num = 8;
			}
			IL_90:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴽⠿❁⅃㉅", a_));
			IL_9E:
			string text2 = A_1.Substring(length + 1);
			goto IL_1F4;
			IL_13D:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠽ℿ⹁ㅃ⍅", a_));
			IL_168:
			this.ᜀ(A_0.Tables[0], A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9);
			return;
			IL_176:
			string text3 = A_1.Substring(length + 1);
			goto IL_1B7;
			IL_1B0:
			text2 = string.Empty;
			goto IL_1F4;
			IL_1B7:
			A_1 = text3;
			this.ᜀ(value, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9);
			return;
			IL_1F4:
			A_1 = text2;
			this.ᜀ(a_2, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9);
			return;
			IL_281:
			throw new ArgumentException(RecordTableEnumerator.b("紽ℿⱁ捃㉅桇⍉⅋㹍㽏⁑⁓癕᱗㭙⡛㽝㍟ݡၣ", a_));
			IL_2F8:
			text3 = string.Empty;
			goto IL_1B7;
			IL_302:
			this.ᜀ(text, A_8, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_9);
			return;
		}
		}
	}

	// Token: 0x06002C6B RID: 11371 RVA: 0x0018F7A8 File Offset: 0x0018E7A8
	private void ᜀ(DataView A_0, string A_1, IWorksheet A_2, IMigrantRange A_3, IList<long> A_4, IList A_5, spr\u2064 A_6, sprᮐ A_7, UnknownVariableAction A_8, VariableTypeAction A_9)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 14;
			string text;
			object value;
			int length;
			DataColumn a_2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					PropertyInfo propertyInfo;
					if (this.ᜀ(A_0, text, out propertyInfo))
					{
						num = 2;
						continue;
					}
					num = 10;
					continue;
				}
				case 1:
					if (true)
					{
					}
					if (A_1 == null)
					{
						goto IL_2B3;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C6;
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
					PropertyInfo propertyInfo;
					value = propertyInfo.GetValue(A_0, null);
					num = 15;
					continue;
				}
				case 3:
					if (A_0 == null)
					{
						num = 13;
						continue;
					}
					goto IL_C6;
				case 4:
					goto IL_1DE;
				case 5:
					goto IL_19A;
				case 6:
					if (A_1.Length > 0)
					{
						num = 9;
						continue;
					}
					goto IL_2B3;
				case 7:
					goto IL_B8;
				case 8:
					num = 11;
					continue;
				case 9:
				{
					text = this.ᜁ(A_1);
					length = text.Length;
					int length2 = A_1.Length;
					num = 0;
					continue;
				}
				case 10:
					if ((a_2 = A_0.ToTable().Columns[text]) != null)
					{
						num = 8;
						continue;
					}
					goto IL_176;
				case 11:
				{
					int length2;
					if (length2 <= length)
					{
						num = 12;
						continue;
					}
					num = 18;
					continue;
				}
				case 12:
					num = 4;
					continue;
				case 13:
					goto IL_142;
				case 15:
				{
					int length2;
					if (length2 <= length)
					{
						num = 16;
						continue;
					}
					num = 7;
					continue;
				}
				case 16:
					num = 5;
					continue;
				case 17:
					num = 6;
					continue;
				case 18:
					goto IL_119;
				case 19:
					goto IL_88;
				}
				if (A_2 == null)
				{
					num = 19;
					continue;
				}
				num = 3;
				continue;
				IL_C6:
				num = 1;
			}
			IL_88:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾⥀♂⁄㍆", a_));
			IL_B8:
			string text2 = A_1.Substring(length + 1);
			goto IL_298;
			IL_119:
			string text3 = A_1.Substring(length + 1);
			goto IL_1B8;
			IL_142:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤾⁀⽂い≆", a_));
			IL_176:
			this.ᜀ(text, A_8, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_9);
			return;
			IL_19A:
			text2 = string.Empty;
			goto IL_298;
			IL_1B8:
			A_1 = text3;
			this.ᜀ(a_2, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9);
			return;
			IL_1DE:
			text3 = string.Empty;
			goto IL_1B8;
			IL_298:
			A_1 = text2;
			this.ᜀ(value, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9);
			return;
			IL_2B3:
			throw new NotImplementedException();
		}
		}
	}

	// Token: 0x06002C6C RID: 11372 RVA: 0x0018FA70 File Offset: 0x0018EA70
	private void ᜀ(DataTable A_0, string A_1, IWorksheet A_2, IMigrantRange A_3, IList<long> A_4, IList A_5, spr\u2064 A_6, sprᮐ A_7, UnknownVariableAction A_8, VariableTypeAction A_9)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 10;
			int length2;
			string text;
			DataColumn a_2;
			object value;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					goto IL_B8;
				case 2:
				{
					int length;
					if (length <= length2)
					{
						num = 0;
						continue;
					}
					num = 1;
					continue;
				}
				case 3:
					goto IL_195;
				case 4:
					goto IL_88;
				case 5:
					goto IL_111;
				case 6:
					if (A_0 == null)
					{
						num = 12;
						continue;
					}
					goto IL_C6;
				case 7:
				{
					int length;
					if (length <= length2)
					{
						num = 18;
						continue;
					}
					num = 5;
					continue;
				}
				case 8:
					if (A_1.Length > 0)
					{
						num = 13;
						continue;
					}
					goto IL_2AE;
				case 9:
					if ((a_2 = A_0.Columns[text]) != null)
					{
						num = 15;
						continue;
					}
					goto IL_171;
				case 11:
					num = 8;
					continue;
				case 12:
					goto IL_13A;
				case 13:
				{
					text = this.ᜁ(A_1);
					length2 = text.Length;
					int length = A_1.Length;
					num = 14;
					continue;
				}
				case 14:
				{
					PropertyInfo propertyInfo;
					if (this.ᜀ(A_0, text, out propertyInfo))
					{
						num = 19;
						continue;
					}
					if (true)
					{
					}
					num = 9;
					continue;
				}
				case 15:
					num = 7;
					continue;
				case 16:
					goto IL_1D9;
				case 17:
					if (A_1 == null)
					{
						goto IL_2AE;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C6;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 18:
					num = 16;
					continue;
				case 19:
				{
					PropertyInfo propertyInfo;
					value = propertyInfo.GetValue(A_0, null);
					num = 2;
					continue;
				}
				}
				if (A_2 == null)
				{
					num = 4;
					continue;
				}
				num = 6;
				continue;
				IL_C6:
				num = 17;
			}
			IL_88:
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
			IL_B8:
			string text2 = A_1.Substring(length2 + 1);
			goto IL_293;
			IL_111:
			string text3 = A_1.Substring(length2 + 1);
			goto IL_1B3;
			IL_13A:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴺尼匾㑀♂", a_));
			IL_171:
			this.ᜀ(text, A_8, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_9);
			return;
			IL_195:
			text2 = string.Empty;
			goto IL_293;
			IL_1B3:
			A_1 = text3;
			this.ᜀ(a_2, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9);
			return;
			IL_1D9:
			text3 = string.Empty;
			goto IL_1B3;
			IL_293:
			A_1 = text2;
			this.ᜀ(value, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_9);
			return;
			IL_2AE:
			throw new NotImplementedException();
		}
		}
	}

	// Token: 0x06002C6D RID: 11373 RVA: 0x0018FD30 File Offset: 0x0018ED30
	private void ᜀ(string A_0, UnknownVariableAction A_1, string A_2, IWorksheet A_3, IMigrantRange A_4, IList<long> A_5, IList A_6, spr\u2064 A_7, sprᮐ A_8, VariableTypeAction A_9)
	{
		int a_ = 12;
		for (;;)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D1;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_C0;
				case 2:
					switch (A_1)
					{
					case UnknownVariableAction.Exception:
						goto IL_8D;
					case UnknownVariableAction.Skip:
						goto IL_C2;
					case UnknownVariableAction.ReplaceBlank:
						goto IL_71;
					default:
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_71:
		this.ᜀ(string.Empty, A_2, A_3, A_4, A_5, A_6, A_7, A_8, A_1, null, null);
		return;
		IL_8D:
		throw new ApplicationException(RecordTableEnumerator.b("ᑁ╃㑅ⅇ⭉⹋≍㕏牑", a_) + A_0 + RecordTableEnumerator.b("扁⩃⥅㱇橉⩋⅍╏㱑こ", a_));
		IL_C0:
		goto IL_D1;
		IL_C2:
		A_4.Text = A_7.ᜂ();
		return;
		IL_D1:
		throw new ApplicationException();
	}

	// Token: 0x06002C6E RID: 11374 RVA: 0x0018FE14 File Offset: 0x0018EE14
	private void ᜀ(DataColumn A_0, string A_1, IWorksheet A_2, IMigrantRange A_3, IList<long> A_4, IList A_5, spr\u2064 A_6, sprᮐ A_7, UnknownVariableAction A_8, VariableTypeAction A_9)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				DataTable table = A_0.Table;
				DataRowCollection rows = table.Rows;
				string a_ = null;
				Type a_2 = null;
				int num = 9;
				for (;;)
				{
					int num2;
					object obj;
					int count;
					bool a_3;
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (num2 > 0)
						{
							num = 7;
							continue;
						}
						goto IL_EB;
					case 2:
						goto IL_1BF;
					case 3:
						if (A_9 == VariableTypeAction.DetectDataType)
						{
							num = 8;
							continue;
						}
						goto IL_1BF;
					case 4:
						goto IL_19C;
					case 5:
						if (obj is string)
						{
							num = 12;
							continue;
						}
						goto IL_1BF;
					case 6:
						goto IL_19C;
					case 7:
						num = 13;
						continue;
					case 8:
						goto IL_AC;
					case 9:
						if (A_9 != VariableTypeAction.DetectNumberFormat)
						{
							num = 14;
							continue;
						}
						goto IL_AC;
					case 10:
						return;
					case 11:
						if (num2 >= count)
						{
							num = 10;
							continue;
						}
						num = 1;
						continue;
					case 12:
						this.ᜀ(obj as string, ref a_, ref a_2, a_3);
						num = 2;
						continue;
					case 13:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AC;
						default:
							if (false)
							{
							}
							if (!this.ᜀ(A_2, A_3, A_4, A_5, A_6))
							{
								num = 0;
								continue;
							}
							goto IL_EB;
						}
						break;
					case 14:
						if (true)
						{
						}
						num = 3;
						continue;
					}
					break;
					IL_AC:
					a_3 = (A_9 == VariableTypeAction.DetectNumberFormat);
					obj = rows[0][A_0];
					num = 5;
					continue;
					IL_EB:
					DataRow dataRow = rows[num2];
					object a_4 = dataRow[A_0];
					this.ᜀ(a_4, A_1, A_2, A_3, A_4, A_5, A_6, A_7, A_8, a_, a_2);
					num2++;
					num = 6;
					continue;
					IL_19C:
					num = 11;
					continue;
					IL_1BF:
					num2 = 0;
					count = rows.Count;
					num = 4;
				}
			}
			return;
		}
	}

	// Token: 0x06002C6F RID: 11375 RVA: 0x00190028 File Offset: 0x0018F028
	private bool ᜀ(IWorksheet A_0, IMigrantRange A_1, IList<long> A_2, IList A_3, spr\u2064 A_4)
	{
		bool flag;
		for (;;)
		{
			int row;
			int column;
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
				row = A_1.Row;
				column = A_1.Column;
				this.ᜀ(A_0, ref row, ref column, A_2, A_3, A_4);
				num = 4;
				break;
			}
			for (;;)
			{
				bool flag2;
				switch (num)
				{
				case 0:
					if (flag)
					{
						num = 3;
						continue;
					}
					return flag;
				case 1:
					if (true)
					{
					}
					num = 6;
					continue;
				case 2:
					flag2 = false;
					goto IL_A4;
				case 3:
					A_1.ResetRowColumn(row, column);
					num = 5;
					continue;
				case 4:
					if (row != 0)
					{
						num = 1;
						continue;
					}
					num = 2;
					continue;
				case 5:
					return flag;
				case 6:
					flag2 = (column != 0);
					goto IL_A4;
				}
				break;
				IL_A4:
				flag = flag2;
				num = 0;
			}
		}
		return flag;
	}

	// Token: 0x06002C70 RID: 11376 RVA: 0x00190110 File Offset: 0x0018F110
	private bool ᜅ(object A_0)
	{
		if (true)
		{
		}
		if (A_0 != null)
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
				return A_0 is IList;
			}
		}
		return false;
	}

	// Token: 0x06002C71 RID: 11377 RVA: 0x0019015C File Offset: 0x0018F15C
	private bool ᜄ(object A_0)
	{
		if (A_0 != null)
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
				return A_0 is ICollection;
			}
		}
		if (true)
		{
		}
		return false;
	}

	// Token: 0x06002C72 RID: 11378 RVA: 0x001901A8 File Offset: 0x0018F1A8
	private bool ᜃ(object A_0)
	{
		if (A_0 != null)
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
				return A_0 is DataSet;
			}
		}
		return false;
	}

	// Token: 0x06002C73 RID: 11379 RVA: 0x001901F4 File Offset: 0x0018F1F4
	private bool ᜂ(object A_0)
	{
		if (A_0 != null)
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
				return A_0 is DataTable;
			}
		}
		if (true)
		{
		}
		return false;
	}

	// Token: 0x06002C74 RID: 11380 RVA: 0x00190240 File Offset: 0x0018F240
	private bool ᜁ(object A_0)
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
		return A_0 is DataView;
	}

	// Token: 0x06002C75 RID: 11381 RVA: 0x00190288 File Offset: 0x0018F288
	private bool ᜀ(object A_0)
	{
		if (A_0 != null)
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
				return A_0 is DataColumn;
			}
		}
		return false;
	}

	// Token: 0x06002C76 RID: 11382 RVA: 0x001902D4 File Offset: 0x0018F2D4
	private bool ᜀ(object A_0, string A_1, out PropertyInfo A_2)
	{
		int a_ = 16;
		for (;;)
		{
			IL_45:
			A_2 = null;
			int num = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_45;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						return false;
					case 1:
						if (A_0 == null)
						{
							num = 0;
							continue;
						}
						num = 5;
						continue;
					case 2:
						goto IL_87;
					case 3:
						num = 4;
						continue;
					case 4:
						if (A_1.Length == 0)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						goto IL_BA;
					case 5:
						if (A_1 != null)
						{
							num = 3;
							continue;
						}
						goto IL_A6;
					}
					goto IL_45;
				}
			}
		}
		return false;
		IL_87:
		IL_A6:
		throw new ArgumentException(RecordTableEnumerator.b("㕅㱇㡉᱋㱍㽏≑ᩓ㝕㕗㽙籛獝䁟ᅡၣᑥŧѩ୫乭፯፱ᩳᡵ᝷๹屻ᱽꊁﺉꂍ", a_));
		IL_BA:
		Type type = A_0.GetType();
		A_2 = type.GetProperty(A_1);
		return A_2 != null;
	}

	// Token: 0x06002C77 RID: 11383 RVA: 0x001903B4 File Offset: 0x0018F3B4
	private string ᜀ(ref string A_0)
	{
		int a_ = 7;
		int num = 3;
		string result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_48;
			case 1:
				goto IL_4A;
			case 2:
			{
				if (A_0.Length == 0)
				{
					num = 4;
					continue;
				}
				int num2 = A_0.IndexOf(RecordTableEnumerator.b("ጼ", a_));
				num = 5;
				continue;
			}
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4A;
				default:
					goto IL_F3;
				}
				break;
			case 5:
			{
				int num2;
				if (num2 < 0)
				{
					num = 1;
					continue;
				}
				result = A_0.Substring(0, num2);
				A_0 = A_0.Substring(num2 + 1);
				if (true)
				{
				}
				num = 7;
				continue;
			}
			case 6:
				goto IL_66;
			case 7:
				goto IL_BC;
			}
			if (A_0 == null)
			{
				num = 0;
				continue;
			}
			num = 2;
			continue;
			IL_4A:
			result = A_0;
			A_0 = string.Empty;
			num = 6;
		}
		IL_48:
		throw new ArgumentNullException(RecordTableEnumerator.b("丼䬾㍀ᝂ⁄㽆㵈", a_));
		IL_66:
		IL_BC:
		return result;
		IL_F3:
		if (false)
		{
		}
		throw new ArgumentException(RecordTableEnumerator.b("丼䬾㍀ᝂ⁄㽆㵈歊恌潎≐❒❔㹖㝘㱚絜㱞`ൢ୤ࡦᵨ䭪ཬ੮兰ᙲᡴݶ൸ɺ卼", a_));
	}

	// Token: 0x06002C78 RID: 11384 RVA: 0x001904F8 File Offset: 0x0018F4F8
	private string ᜁ(string A_0)
	{
		int a_ = 2;
		int num = 6;
		int num2;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_D3;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					if (num2 >= 0)
					{
						num = 5;
						continue;
					}
					num = 3;
					continue;
				case 1:
					if (A_0.Length == 0)
					{
						num = 7;
						continue;
					}
					num2 = A_0.IndexOf(RecordTableEnumerator.b("ᘷ", a_));
					num = 0;
					continue;
				case 2:
					goto IL_72;
				case 3:
					goto IL_B2;
				case 4:
					goto IL_60;
				case 5:
					if (true)
					{
					}
					num = 2;
					continue;
				case 7:
					goto IL_D3;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 1;
				}
				break;
			}
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("䰷弹䐻䨽", a_));
		IL_72:
		return A_0.Substring(0, num2);
		IL_B2:
		return A_0;
		IL_D3:
		throw new ArgumentException(RecordTableEnumerator.b("男嬹主唽┿ぁ摃㉅ⵇ㉉㡋湍㍏㍑㩓㡕㝗⹙籛㱝՟䉡ţ୥ᡧṩᕫ䁭", a_));
	}

	// Token: 0x06002C79 RID: 11385 RVA: 0x0019061C File Offset: 0x0018F61C
	private IList ᜀ(string A_0, out spr\u2064 A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			SortedList<int, List<spr\u22EA>> sortedList;
			for (;;)
			{
				A_1 = new spr\u2064(this.ᜆ);
				sortedList = null;
				int num = 10;
				for (;;)
				{
					spr\u22EA spr_u22EA;
					List<spr\u22EA> list;
					int num3;
					switch (num)
					{
					case 0:
						goto IL_135;
					case 1:
						if (!sortedList.TryGetValue(spr_u22EA.ᜀ(), out list))
						{
							num = 12;
							continue;
						}
						goto IL_238;
					case 2:
						spr_u22EA.ᜀ(A_1);
						num = 6;
						continue;
					case 3:
						goto IL_13A;
					case 4:
					{
						string[] array = A_0.Split(new char[]
						{
							this.ᜋ
						});
						int num2 = array.Length;
						sortedList = new SortedList<int, List<spr\u22EA>>(num2);
						num3 = 0;
						num = 14;
						continue;
					}
					case 5:
						goto IL_238;
					case 6:
						goto IL_D9;
					case 7:
						if (spr_u22EA.ᜂ())
						{
							num = 2;
							continue;
						}
						goto IL_D9;
					case 8:
						if (A_0.Length != 0)
						{
							num = 4;
							continue;
						}
						goto IL_28E;
					case 9:
						if (spr_u22EA.ᜁ())
						{
							num = 18;
							continue;
						}
						goto IL_13A;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_10E;
						default:
							if (false)
							{
							}
							if (A_0 != null)
							{
								num = 17;
								continue;
							}
							goto IL_28E;
						}
						break;
					case 11:
						if (true)
						{
						}
						if (spr_u22EA == null)
						{
							num = 0;
							continue;
						}
						num = 7;
						continue;
					case 12:
					{
						list = new List<spr\u22EA>();
						int key;
						sortedList.Add(key, list);
						num = 5;
						continue;
					}
					case 13:
					{
						int num2;
						if (num3 >= num2)
						{
							num = 16;
							continue;
						}
						string[] array;
						string a_2 = array[num3];
						spr_u22EA = this.ᜀ(a_2);
						goto IL_10E;
					}
					case 14:
						goto IL_1C6;
					case 15:
						goto IL_1C6;
					case 16:
						goto IL_1E5;
					case 17:
						num = 8;
						continue;
					case 18:
					{
						int key = spr_u22EA.ᜀ();
						num = 1;
						continue;
					}
					}
					break;
					IL_D9:
					num = 9;
					continue;
					IL_10E:
					num = 11;
					continue;
					IL_13A:
					num3++;
					num = 15;
					continue;
					IL_1C6:
					num = 13;
					continue;
					IL_238:
					list.Add(spr_u22EA);
					num = 3;
				}
			}
			IL_135:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("眵䨷崹䤻匽┿ⱁぃ", a_), RecordTableEnumerator.b("挵嘷儹刻儽㜿ⱁ摃⭅⥇㡉❋⭍≏牑㕓⑕㽗⽙ㅛ㭝๟ᙡ䩣", a_));
			IL_1E5:
			IL_28E:
			return this.ᜁ(sortedList, A_1);
		}
		}
	}

	// Token: 0x06002C7A RID: 11386 RVA: 0x001908C0 File Offset: 0x0018F8C0
	private IList ᜁ(SortedList<int, List<spr\u22EA>> A_0, spr\u2064 A_1)
	{
		switch (0)
		{
		default:
		{
			List<spr\u22EA> list;
			for (;;)
			{
				IL_57:
				list = new List<spr\u22EA>();
				int num = 6;
				for (;;)
				{
					int num2;
					int count;
					IList<List<spr\u22EA>> values;
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
							goto IL_DD;
						case 1:
							return list;
						case 2:
							A_0 = new SortedList<int, List<spr\u22EA>>(1);
							num = 5;
							continue;
						case 3:
						{
							if (num2 >= count)
							{
								num = 1;
								continue;
							}
							List<spr\u22EA> collection = values[num2];
							list.AddRange(collection);
							num2++;
							num = 4;
							continue;
						}
						case 4:
							goto IL_DD;
						case 5:
							goto IL_97;
						case 6:
							if (A_0 == null)
							{
								num = 2;
								continue;
							}
							goto IL_97;
						}
						goto IL_57;
						IL_DD:
						num = 3;
						continue;
					}
					IL_97:
					this.ᜀ(A_0, A_1);
					values = A_0.Values;
					num2 = 0;
					count = A_0.Count;
					num = 0;
				}
			}
			return list;
		}
		}
	}

	// Token: 0x06002C7B RID: 11387 RVA: 0x001909CC File Offset: 0x0018F9CC
	private void ᜀ(SortedList<int, List<spr\u22EA>> A_0, spr\u2064 A_1)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				int a_2;
				int a_3;
				spr\u25AF spr_u25AF;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					a_2 = 0;
					a_3 = 1;
					num = 10;
					continue;
				case 1:
				{
					List<spr\u22EA> list = new List<spr\u22EA>(1);
					list.Add(spr_u25AF);
					A_0.Add(spr_u25AF.ᜀ(), list);
					num = 8;
					continue;
				}
				case 2:
					goto IL_61;
				case 4:
					if (!A_0.ContainsKey(spr_u25AF.ᜀ()))
					{
						num = 1;
						continue;
					}
					return;
				case 5:
					if (A_1 == null)
					{
						num = 9;
						continue;
					}
					num = 6;
					continue;
				case 6:
					if (A_1.ᜁ() == DataMarkerDirection.Horizontal)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10E;
					default:
						if (false)
						{
						}
						a_2 = 1;
						a_3 = 0;
						num = 7;
						continue;
					}
					break;
				case 7:
					goto IL_10E;
				case 8:
					return;
				case 9:
					goto IL_C9;
				case 10:
					goto IL_10E;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 5;
				continue;
				IL_10E:
				spr_u25AF = new spr\u25AF(a_2, a_3, true, true);
				num = 4;
			}
			IL_61:
			throw new ArgumentNullException(RecordTableEnumerator.b("儼䰾㕀โ⑄㕆≈⹊㽌㱎", a_));
			IL_C9:
			throw new ArgumentNullException(RecordTableEnumerator.b("刼伾㕀⩂⩄⥆㩈", a_));
		}
		}
	}

	// Token: 0x06002C7C RID: 11388 RVA: 0x00190B60 File Offset: 0x0018FB60
	private spr\u22EA ᜀ(string A_0)
	{
		int a_ = 15;
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
					goto IL_CB;
				case 1:
					goto IL_5D;
				case 2:
					goto IL_CD;
				case 3:
				{
					spr\u22EA spr_u22EA;
					return spr_u22EA;
				}
				case 4:
				{
					spr\u22EA spr_u22EA;
					if (spr_u22EA != null)
					{
						num = 3;
						continue;
					}
					if (true)
					{
					}
					int num2;
					num2++;
					num = 2;
					continue;
				}
				case 5:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 9;
						continue;
					}
					spr\u22EA spr_u22EA2 = spr\u25CA.ᜇ[num2];
					spr\u22EA spr_u22EA = spr_u22EA2.ᜀ(A_0);
					num = 4;
					continue;
				}
				case 7:
					if (A_0.Length != 0)
					{
						int num2 = 0;
						int count = spr\u25CA.ᜇ.Count;
						num = 8;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_152;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 8:
					goto IL_CD;
				case 9:
					goto IL_E9;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num = 7;
				continue;
				IL_CD:
				num = 5;
			}
			IL_5D:
			throw new ArgumentNullException(RecordTableEnumerator.b("⑄㕆⹈㹊⁌⩎㽐❒", a_));
			IL_CB:
			throw new ArgumentException(RecordTableEnumerator.b("ࡄ♆㭈⁊⡌㵎煐㉒❔ざⱘ㙚㡜ㅞᕠ䍢䡤䝦ᩨὪὬٮὰᑲ啴ᑶᡸᕺ፼ၾꎂꦈﾎ뮔", a_));
			IL_E9:
			IL_152:
			return null;
		}
		}
	}

	// Token: 0x06002C7D RID: 11389 RVA: 0x00190CC0 File Offset: 0x0018FCC0
	private void ᜀ(IWorksheet A_0, ref int A_1, ref int A_2, IList<long> A_3, IList A_4, spr\u2064 A_5)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				int count;
				Point a_2;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_C3;
				case 1:
					goto IL_B7;
				case 2:
					if (count == 0)
					{
						num = 6;
						continue;
					}
					if (true)
					{
					}
					a_2 = new Point(A_1, A_2);
					num2 = 0;
					num = 1;
					continue;
				case 4:
					goto IL_B7;
				case 5:
					goto IL_59;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C3;
					default:
						goto IL_9C;
					}
					break;
				case 7:
					return;
				}
				if (A_4 == null)
				{
					num = 5;
					continue;
				}
				count = A_4.Count;
				num = 2;
				continue;
				IL_C3:
				if (num2 >= count)
				{
					num = 7;
					continue;
				}
				spr\u22EA spr_u22EA = (spr\u22EA)A_4[num2];
				spr_u22EA.ᜀ(A_0, a_2, ref A_1, ref A_2, A_3, A_5);
				num2++;
				num = 4;
				continue;
				IL_B7:
				num = 0;
			}
			IL_59:
			throw new ArgumentNullException(RecordTableEnumerator.b("⡃㕅㱇୉㹋⥍╏㽑ㅓ㡕ⱗ⥙", a_));
			IL_9C:
			if (false)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06002C7E RID: 11390 RVA: 0x00190DF4 File Offset: 0x0018FDF4
	public string ᜂ()
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
		return this.ᜊ;
	}

	// Token: 0x06002C7F RID: 11391 RVA: 0x00190E38 File Offset: 0x0018FE38
	public void ᜃ(string A_0)
	{
		int a_ = 1;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_90;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A6;
				default:
					if (false)
					{
					}
					if (A_0.Length == 0)
					{
						num = 1;
						continue;
					}
					goto IL_A6;
				}
				break;
			case 3:
				goto IL_3C;
			}
			if (true)
			{
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
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䄶堸场䠼娾", a_));
		IL_90:
		throw new ArgumentException(RecordTableEnumerator.b("䄶堸场䠼娾慀湂敄㑆㵈㥊⑌ⅎ㙐獒㙔㙖㝘㕚㉜⭞䅠Ţd䝦౨٪ᵬ᭮ࡰ嵲", a_));
		IL_A6:
		this.ᜊ = A_0;
	}

	// Token: 0x06002C80 RID: 11392 RVA: 0x00190EF4 File Offset: 0x0018FEF4
	public char ᜀ()
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
		return this.ᜋ;
	}

	// Token: 0x06002C81 RID: 11393 RVA: 0x00190F38 File Offset: 0x0018FF38
	public void ᜀ(char A_0)
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
	}

	// Token: 0x04001484 RID: 5252
	private const string ᜀ = "&=";

	// Token: 0x04001485 RID: 5253
	private const string ᜁ = ".";

	// Token: 0x04001486 RID: 5254
	private const string ᜂ = "NULL";

	// Token: 0x04001487 RID: 5255
	private const char ᜃ = ',';

	// Token: 0x04001488 RID: 5256
	private const char ᜄ = '(';

	// Token: 0x04001489 RID: 5257
	private const string ᜅ = ")";

	// Token: 0x0400148A RID: 5258
	private XlsWorkbook ᜆ;

	// Token: 0x0400148B RID: 5259
	private static readonly List<spr\u22EA> ᜇ;

	// Token: 0x0400148C RID: 5260
	private static readonly Type[] ᜈ;

	// Token: 0x0400148D RID: 5261
	private Dictionary<string, object> ᜉ;

	// Token: 0x0400148E RID: 5262
	private string ᜊ;

	// Token: 0x0400148F RID: 5263
	private char ᜋ;

	// Token: 0x04001490 RID: 5264
	private Dictionary<string, VariableTypeAction> ᜌ;

	// Token: 0x04001491 RID: 5265
	private Dictionary<string, CondFormatCollectionWrapper> \u170D;
}
