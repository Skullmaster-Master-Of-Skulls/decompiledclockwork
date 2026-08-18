using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls.Core.Spreadsheet.PivotTables
{
	// Token: 0x0200022B RID: 555
	public class PivotCacheCollection : ICloneParent, IPivotCaches, IEnumerable<XlsPivotCache>
	{
		// Token: 0x17000C47 RID: 3143
		IPivotCache IPivotCaches.this[int id]
		{
			get
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
				return this.ᜁ[id];
			}
		}

		// Token: 0x17000C48 RID: 3144
		internal XlsPivotCache this[int A_0]
		{
			get
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
				return this.ᜁ[A_0];
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x00130818 File Offset: 0x0012F818
		public int Count
		{
			get
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
				return this.ᜁ.Count;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x00130860 File Offset: 0x0012F860
		internal spr\u1DF5 Application
		{
			get
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
				return this.ᜀ.AppImplementation;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x001308A8 File Offset: 0x0012F8A8
		public object Parent
		{
			get
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
				return this.ᜀ;
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x001308EC File Offset: 0x0012F8EC
		public List<int> Order
		{
			get
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
				return this.ᜂ;
			}
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x00130930 File Offset: 0x0012F930
		internal PivotCacheCollection(spr\u1DF5 A_0, object A_1)
		{
			this.ᜀ = this.ᜀ(A_1);
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00130968 File Offset: 0x0012F968
		internal PivotCacheCollection(spr\u1DF5 A_0, object A_1, spr\u20C3 A_2, IDecryptor A_3) : this(A_0, A_1)
		{
			this.ᜀ(A_2, A_3);
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00130988 File Offset: 0x0012F988
		internal void ᜀ(spr\u20C3 A_0, IDecryptor A_1)
		{
			int a_ = 14;
			for (;;)
			{
				IL_09:
				switch (0)
				{
				default:
				{
					int num = 4;
					for (;;)
					{
						if (true)
						{
						}
						spr\u20C3 spr_u20C;
						spr\u20C3 spr_u20C2;
						switch (num)
						{
						case 0:
							goto IL_70;
						case 1:
							goto IL_1FB;
						case 2:
							try
							{
								for (;;)
								{
									string[] array = spr_u20C.ᜁ();
									int num2 = 0;
									int num3 = array.Length;
									num = 3;
									for (;;)
									{
										spr\u1FDC spr_u1FDC;
										string text;
										switch (num)
										{
										case 0:
											goto IL_1B6;
										case 1:
											num = 0;
											continue;
										case 2:
											if (num2 >= num3)
											{
												num = 1;
												continue;
											}
											goto IL_18A;
										case 3:
											goto IL_BD;
										case 4:
											try
											{
												sprἛ sprἛ = new sprἛ(spr_u1FDC);
												try
												{
													XlsPivotCache a_2 = new XlsPivotCache(this.Application, this, sprἛ, A_1, text);
													this.ᜀ(text, a_2);
												}
												finally
												{
													num = 0;
													for (;;)
													{
														switch (num)
														{
														case 1:
															((IDisposable)sprἛ).Dispose();
															num = 2;
															continue;
														case 2:
															goto IL_142;
														}
														if (sprἛ == null)
														{
															break;
														}
														num = 1;
													}
													IL_142:;
												}
												goto IL_AE;
											}
											finally
											{
												num = 2;
												for (;;)
												{
													switch (num)
													{
													case 0:
														((IDisposable)spr_u1FDC).Dispose();
														num = 1;
														continue;
													case 1:
														goto IL_187;
													}
													if (spr_u1FDC == null)
													{
														break;
													}
													num = 0;
												}
												IL_187:;
											}
											goto IL_18A;
											IL_AE:
											num2++;
											num = 5;
											continue;
										case 5:
											goto IL_BD;
										}
										break;
										IL_BD:
										num = 2;
										continue;
										IL_18A:
										text = array[num2];
										spr_u1FDC = spr_u20C.ᜁ(text);
										num = 4;
									}
								}
								IL_1B6:
								return;
							}
							finally
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										spr_u20C2.Dispose();
										num = 1;
										continue;
									case 1:
										goto IL_1F8;
									}
									if (spr_u20C2 == null)
									{
										break;
									}
									num = 0;
								}
								IL_1F8:;
							}
							goto IL_1FB;
						case 3:
							if (A_0.ᜇ(RecordTableEnumerator.b("ᭃᕅ၇ᕉࡋ్ཏᅑœѕ", a_)))
							{
								num = 1;
								continue;
							}
							return;
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
						}
						if (A_0 == null)
						{
							num = 0;
							continue;
						}
						this.Clear();
						spr_u20C = null;
						num = 3;
						continue;
						IL_1FB:
						spr_u20C = (spr_u20C2 = A_0.ᜅ(RecordTableEnumerator.b("ᭃᕅ၇ᕉࡋ్ཏᅑœѕ", a_)));
						num = 2;
					}
					break;
				}
				}
			}
			IL_70:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝃㉅❇㡉ⵋ⥍㕏", a_));
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x00130C64 File Offset: 0x0012FC64
		public void Clear()
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
			this.ᜁ.Clear();
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00130CAC File Offset: 0x0012FCAC
		internal void ᜀ(spr\u20C3 A_0, IEncryptor A_1)
		{
			int a_ = 5;
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
						goto IL_5E;
					case 2:
						goto IL_257;
					case 3:
						goto IL_27A;
					case 4:
					{
						int count;
						if (count == 0)
						{
							num = 2;
							continue;
						}
						spr\u20C3 spr_u20C = A_0.ᜄ(RecordTableEnumerator.b("携渼朾Ṁ݂݄ᡆੈṊὌ", a_));
						num = 3;
						continue;
					}
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 1;
					}
					else
					{
						int count = this.Count;
						num = 4;
					}
				}
				IL_5E:
				throw new ArgumentNullException(RecordTableEnumerator.b("䠺䤼倾㍀≂≄≆", a_));
				IL_257:
				return;
				IL_27A:
				try
				{
					using (Dictionary<int, XlsPivotCache>.ValueCollection.Enumerator enumerator = this.ᜁ.Values.GetEnumerator())
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_1C1;
							case 2:
							{
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								XlsPivotCache xlsPivotCache = enumerator.Current;
								string a_2 = xlsPivotCache.StreamId.ToString(RecordTableEnumerator.b("挺़", a_));
								spr\u20C3 spr_u20C;
								spr\u1FDC spr_u1FDC = spr_u20C.ᜀ(a_2);
								num = 4;
								continue;
							}
							case 3:
								goto IL_1CD;
							case 4:
								try
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
										RecordArrayList recordArrayList = new RecordArrayList();
										XlsPivotCache xlsPivotCache;
										xlsPivotCache.SerializeDataToList(recordArrayList);
										spr\u1FDC spr_u1FDC;
										sprᡄ sprᡄ = new sprᡄ(spr_u1FDC);
										try
										{
											sprᡄ.ᜀ(recordArrayList, A_1);
										}
										finally
										{
											num = 0;
											for (;;)
											{
												switch (num)
												{
												case 1:
													goto IL_179;
												case 2:
													((IDisposable)sprᡄ).Dispose();
													num = 1;
													continue;
												}
												if (sprᡄ == null)
												{
													break;
												}
												num = 2;
											}
											IL_179:;
										}
										break;
									}
									}
									break;
								}
								finally
								{
									num = 0;
									for (;;)
									{
										spr\u1FDC spr_u1FDC;
										switch (num)
										{
										case 1:
											goto IL_1BE;
										case 2:
											((IDisposable)spr_u1FDC).Dispose();
											num = 1;
											continue;
										}
										if (spr_u1FDC == null)
										{
											break;
										}
										num = 2;
									}
									IL_1BE:;
								}
								goto IL_1C1;
							}
							IL_DA:
							num = 2;
							continue;
							goto IL_DA;
							IL_1C1:
							num = 3;
						}
						IL_1CD:;
					}
					return;
				}
				finally
				{
					num = 1;
					for (;;)
					{
						spr\u20C3 spr_u20C;
						switch (num)
						{
						case 0:
							spr_u20C.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_21D;
						}
						if (spr_u20C == null)
						{
							break;
						}
						num = 0;
					}
					IL_21D:;
				}
				return;
			}
			}
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x00130F9C File Offset: 0x0012FF9C
		internal void ᜁ(XlsPivotCache A_0)
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
			int key = A_0.Index = this.ᜀ(A_0);
			this.ᜁ.Add(key, A_0);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x00130FF8 File Offset: 0x0012FFF8
		private int ᜀ(XlsPivotCache A_0)
		{
			switch (0)
			{
			default:
			{
				int num;
				for (;;)
				{
					num = (int)A_0.StreamId;
					sprវ sprវ = this.ᜀ.DataHolder;
					Dictionary<string, string> dictionary = null;
					int num2 = 2;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							num2 = 9;
							continue;
						case 1:
							dictionary = sprវ.ᜢ();
							num2 = 4;
							continue;
						case 2:
							if (sprវ == null)
							{
								goto IL_B9;
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
								num2 = 1;
								continue;
							}
							break;
						case 3:
							if (!dictionary.ContainsKey((num + 1).ToString()))
							{
								num2 = 5;
								continue;
							}
							goto IL_123;
						case 4:
							goto IL_B9;
						case 5:
							goto IL_121;
						case 6:
							goto IL_B9;
						case 7:
							num2 = 3;
							continue;
						case 8:
							if (!this.ᜁ.ContainsKey(num))
							{
								num2 = 0;
								continue;
							}
							goto IL_123;
						case 9:
							if (dictionary != null)
							{
								num2 = 7;
								continue;
							}
							goto IL_135;
						}
						break;
						IL_B9:
						num2 = 8;
						continue;
						IL_123:
						num++;
						num2 = 6;
					}
				}
				IL_121:
				IL_135:
				A_0.StreamId = (ushort)num;
				return num;
			}
			}
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x00131144 File Offset: 0x00130144
		public PivotCache Add(CellRange range)
		{
			int a_ = 3;
			if (range == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("䬸娺匼堾⑀", a_));
			}
			PivotCache pivotCache = new PivotCache((spr\u2158)this.Application, this, range);
			this.ᜁ(pivotCache);
			this.ᜂ.Add(pivotCache.Index);
			return pivotCache;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x001311D0 File Offset: 0x001301D0
		private void ᜀ(string A_0, XlsPivotCache A_1)
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
			this.ᜁ(A_1);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x00131214 File Offset: 0x00130214
		internal int ᜀ(XlsPivotCache A_0, Dictionary<string, string> A_1)
		{
			switch (0)
			{
			default:
			{
				XlsPivotCache xlsPivotCache;
				for (;;)
				{
					IXLSRange sourceRange = A_0.SourceRange;
					xlsPivotCache = null;
					int num = 22;
					for (;;)
					{
						string text;
						IXLSRange ixlsrange;
						XlsPivotCache xlsPivotCache3;
						int num3;
						int count2;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_23C;
							default:
								if (false)
								{
								}
								if (A_1.ContainsKey(text))
								{
									num = 21;
									continue;
								}
								goto IL_101;
							}
							break;
						case 1:
							goto IL_23C;
						case 2:
							goto IL_2F4;
						case 3:
							goto IL_13A;
						case 4:
							goto IL_1DB;
						case 5:
							goto IL_275;
						case 6:
							goto IL_2F4;
						case 7:
							xlsPivotCache = this.Add((CellRange)ixlsrange);
							num = 23;
							continue;
						case 8:
						{
							XlsPivotCache xlsPivotCache2;
							if (xlsPivotCache2.ComparePreservedData(A_0))
							{
								num = 14;
								continue;
							}
							int num2;
							num2++;
							num = 3;
							continue;
						}
						case 9:
							if (A_1 != null)
							{
								num = 11;
								continue;
							}
							goto IL_101;
						case 10:
							goto IL_1DB;
						case 11:
							if (true)
							{
							}
							num = 0;
							continue;
						case 12:
							goto IL_13A;
						case 13:
							xlsPivotCache = (XlsPivotCache)A_0.Clone(this);
							this.ᜁ(xlsPivotCache);
							num = 5;
							continue;
						case 14:
						{
							XlsPivotCache xlsPivotCache2;
							xlsPivotCache = xlsPivotCache2;
							num = 16;
							continue;
						}
						case 15:
							goto IL_101;
						case 16:
							goto IL_1BB;
						case 17:
							if (xlsPivotCache3.SourceRange.RangeAddressLocal == ixlsrange.RangeAddressLocal)
							{
								num = 1;
								continue;
							}
							num3++;
							num = 10;
							continue;
						case 18:
							text = sourceRange.Worksheet.Name;
							num = 9;
							continue;
						case 19:
							if (xlsPivotCache == null)
							{
								num = 13;
								continue;
							}
							goto IL_330;
						case 20:
						{
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 24;
								continue;
							}
							XlsPivotCache xlsPivotCache2 = this[num2];
							num = 8;
							continue;
						}
						case 21:
							text = A_1[text];
							num = 15;
							continue;
						case 22:
						{
							if (sourceRange != null)
							{
								num = 18;
								continue;
							}
							int num2 = 0;
							int count = this.Count;
							num = 12;
							continue;
						}
						case 23:
							goto IL_FC;
						case 24:
							goto IL_1BB;
						case 25:
							if (num3 >= count2)
							{
								num = 6;
								continue;
							}
							xlsPivotCache3 = this[num3];
							num = 17;
							continue;
						case 26:
							if (xlsPivotCache == null)
							{
								num = 7;
								continue;
							}
							goto IL_330;
						}
						break;
						IL_101:
						ixlsrange = this.ᜀ.Worksheets[text][sourceRange.RangeAddressLocal];
						num3 = 0;
						count2 = this.Count;
						num = 4;
						continue;
						IL_13A:
						num = 20;
						continue;
						IL_1BB:
						num = 19;
						continue;
						IL_1DB:
						num = 25;
						continue;
						IL_23C:
						xlsPivotCache = xlsPivotCache3;
						num = 2;
						continue;
						IL_2F4:
						num = 26;
					}
				}
				IL_FC:
				IL_275:
				IL_330:
				return xlsPivotCache.Index;
			}
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00131558 File Offset: 0x00130558
		public object Clone(object parent)
		{
			switch (0)
			{
			default:
			{
				PivotCacheCollection pivotCacheCollection = (PivotCacheCollection)base.MemberwiseClone();
				pivotCacheCollection.ᜀ = this.ᜀ(parent);
				using (Dictionary<int, XlsPivotCache>.ValueCollection.Enumerator enumerator = this.ᜁ.Values.GetEnumerator())
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_E0;
						case 2:
							if (true)
							{
							}
							break;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A4;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 4:
							goto IL_A4;
						}
						goto IL_72;
						IL_A4:
						if (!enumerator.MoveNext())
						{
							num = 3;
							continue;
						}
						XlsPivotCache xlsPivotCache = enumerator.Current;
						XlsPivotCache a_ = (XlsPivotCache)xlsPivotCache.Clone(pivotCacheCollection);
						pivotCacheCollection.ᜁ(a_);
						num = 1;
						continue;
						IL_9B:
						num = 4;
						continue;
						IL_72:
						goto IL_9B;
					}
					IL_E0:;
				}
				return pivotCacheCollection;
			}
			}
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x00131668 File Offset: 0x00130668
		private XlsWorkbook ᜀ(object A_0)
		{
			int a_ = 3;
			XlsWorkbook xlsWorkbook = (XlsWorkbook)XlsObject.FindParent(A_0, typeof(XlsWorkbook));
			if (xlsWorkbook == null)
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
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("稸娺匼儾⹀㝂敄ⅆ⁈╊⥌潎⅐㉒❔㉖㝘⽚絜⡞๠ᅢ๤զ٨Ѫ٬", a_));
			}
			return xlsWorkbook;
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x001316DC File Offset: 0x001306DC
		public IEnumerator<XlsPivotCache> GetEnumerator()
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
			PivotCacheCollection.ᜁ ᜁ = new PivotCacheCollection.ᜁ(0);
			ᜁ.ᜂ = this;
			return ᜁ;
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x00131728 File Offset: 0x00130728
		IEnumerator IEnumerable.GetEnumerator()
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
			PivotCacheCollection.ᜀ ᜀ = new PivotCacheCollection.ᜀ(0);
			ᜀ.ᜂ = this;
			return ᜀ;
		}

		// Token: 0x040011D2 RID: 4562
		public const string DEF_PIVOT_CACHE_STORAGE = "_SX_DB_CUR";

		// Token: 0x040011D3 RID: 4563
		internal XlsWorkbook ᜀ;

		// Token: 0x040011D4 RID: 4564
		private Dictionary<int, XlsPivotCache> ᜁ = new Dictionary<int, XlsPivotCache>();

		// Token: 0x040011D5 RID: 4565
		private List<int> ᜂ = new List<int>();

		// Token: 0x0200022C RID: 556
		[CompilerGenerated]
		private sealed class ᜁ : IEnumerator<XlsPivotCache>
		{
			// Token: 0x06002202 RID: 8706 RVA: 0x00131774 File Offset: 0x00130774
			bool IEnumerator.ᜁ()
			{
				bool result;
				try
				{
					for (;;)
					{
						int num = this.ᜁ;
						int num2 = 6;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_F2;
							case 1:
								goto IL_5B;
							case 2:
								if (!this.ᜄ.MoveNext())
								{
									if (true)
									{
									}
									num2 = 5;
									continue;
								}
								this.ᜃ = this.ᜄ.Current;
								this.ᜀ = this.ᜃ;
								this.ᜁ = 2;
								result = true;
								num2 = 4;
								continue;
							case 3:
								goto IL_5B;
							case 4:
								goto IL_B3;
							case 5:
								this.ᜀ();
								num2 = 0;
								continue;
							case 6:
								switch (num)
								{
								case 0:
									this.ᜁ = -1;
									this.ᜄ = this.ᜂ.ᜁ.Values.GetEnumerator();
									this.ᜁ = 1;
									num2 = 3;
									continue;
								case 1:
									goto IL_144;
								case 2:
									this.ᜁ = 1;
									num2 = 1;
									continue;
								default:
									num2 = 7;
									continue;
								}
								break;
							case 7:
								num2 = 8;
								continue;
							case 8:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_F2;
								default:
									if (false)
									{
									}
									goto IL_144;
								}
								break;
							case 9:
								goto IL_151;
							}
							break;
							IL_5B:
							num2 = 2;
							continue;
							IL_144:
							result = false;
							num2 = 9;
							continue;
							IL_F2:
							goto IL_144;
						}
					}
					IL_B3:
					IL_151:;
				}
				catch
				{
					this.ᜃ();
					throw;
				}
				return result;
			}

			// Token: 0x06002203 RID: 8707 RVA: 0x00131904 File Offset: 0x00130904
			[DebuggerHidden]
			XlsPivotCache IEnumerator<XlsPivotCache>.ᜂ()
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

			// Token: 0x06002204 RID: 8708 RVA: 0x00131948 File Offset: 0x00130948
			[DebuggerHidden]
			void IEnumerator.ᜄ()
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

			// Token: 0x06002205 RID: 8709 RVA: 0x00131988 File Offset: 0x00130988
			void IDisposable.ᜃ()
			{
				switch (this.ᜁ)
				{
				case 1:
				case 2:
					try
					{
						return;
					}
					finally
					{
						this.ᜀ();
					}
					break;
				default:
					IL_17:
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_17;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					return;
				}
			}

			// Token: 0x06002206 RID: 8710 RVA: 0x001319F8 File Offset: 0x001309F8
			[DebuggerHidden]
			object IEnumerator.ᜅ()
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
				return this.ᜀ;
			}

			// Token: 0x06002207 RID: 8711 RVA: 0x00131A3C File Offset: 0x00130A3C
			[DebuggerHidden]
			public ᜁ(int A_0)
			{
				this.ᜁ = A_0;
			}

			// Token: 0x06002208 RID: 8712 RVA: 0x00131A58 File Offset: 0x00130A58
			private void ᜀ()
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
				this.ᜁ = -1;
				((IDisposable)this.ᜄ).Dispose();
			}

			// Token: 0x040011D6 RID: 4566
			private XlsPivotCache ᜀ;

			// Token: 0x040011D7 RID: 4567
			private int ᜁ;

			// Token: 0x040011D8 RID: 4568
			public PivotCacheCollection ᜂ;

			// Token: 0x040011D9 RID: 4569
			public XlsPivotCache ᜃ;

			// Token: 0x040011DA RID: 4570
			public Dictionary<int, XlsPivotCache>.ValueCollection.Enumerator ᜄ;
		}

		// Token: 0x0200022D RID: 557
		[CompilerGenerated]
		private sealed class ᜀ : IEnumerator<object>
		{
			// Token: 0x06002209 RID: 8713 RVA: 0x00131AAC File Offset: 0x00130AAC
			bool IEnumerator.ᜂ()
			{
				bool result;
				try
				{
					for (;;)
					{
						int num = this.ᜁ;
						int num2 = 9;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_5B;
							case 1:
								goto IL_B3;
							case 2:
								goto IL_5B;
							case 3:
								goto IL_144;
							case 4:
								goto IL_151;
							case 5:
								this.ᜀ();
								num2 = 3;
								continue;
							case 6:
								if (!this.ᜄ.MoveNext())
								{
									if (true)
									{
									}
									num2 = 5;
									continue;
								}
								this.ᜃ = this.ᜄ.Current;
								this.ᜀ = this.ᜃ;
								this.ᜁ = 2;
								result = true;
								goto IL_A8;
							case 7:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_A8;
								default:
									if (false)
									{
									}
									goto IL_144;
								}
								break;
							case 8:
								num2 = 7;
								continue;
							case 9:
								switch (num)
								{
								case 0:
									this.ᜁ = -1;
									this.ᜄ = this.ᜂ.ᜁ.Values.GetEnumerator();
									this.ᜁ = 1;
									num2 = 0;
									continue;
								case 1:
									goto IL_144;
								case 2:
									this.ᜁ = 1;
									num2 = 2;
									continue;
								default:
									num2 = 8;
									continue;
								}
								break;
							}
							break;
							IL_5B:
							num2 = 6;
							continue;
							IL_A8:
							num2 = 1;
							continue;
							IL_144:
							result = false;
							num2 = 4;
						}
					}
					IL_B3:
					IL_151:;
				}
				catch
				{
					this.ᜁ();
					throw;
				}
				return result;
			}

			// Token: 0x0600220A RID: 8714 RVA: 0x00131C3C File Offset: 0x00130C3C
			[DebuggerHidden]
			object IEnumerator<object>.ᜃ()
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

			// Token: 0x0600220B RID: 8715 RVA: 0x00131C80 File Offset: 0x00130C80
			[DebuggerHidden]
			void IEnumerator.ᜄ()
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

			// Token: 0x0600220C RID: 8716 RVA: 0x00131CC0 File Offset: 0x00130CC0
			void IDisposable.ᜁ()
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
					switch (this.ᜁ)
					{
					case 1:
					case 2:
						try
						{
							return;
						}
						finally
						{
							if (true)
							{
							}
							this.ᜀ();
						}
						break;
					}
					break;
				}
			}

			// Token: 0x0600220D RID: 8717 RVA: 0x00131D30 File Offset: 0x00130D30
			[DebuggerHidden]
			object IEnumerator.ᜅ()
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
				return this.ᜀ;
			}

			// Token: 0x0600220E RID: 8718 RVA: 0x00131D74 File Offset: 0x00130D74
			[DebuggerHidden]
			public ᜀ(int A_0)
			{
				this.ᜁ = A_0;
			}

			// Token: 0x0600220F RID: 8719 RVA: 0x00131D90 File Offset: 0x00130D90
			private void ᜀ()
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
				this.ᜁ = -1;
				((IDisposable)this.ᜄ).Dispose();
			}

			// Token: 0x040011DB RID: 4571
			private object ᜀ;

			// Token: 0x040011DC RID: 4572
			private int ᜁ;

			// Token: 0x040011DD RID: 4573
			public PivotCacheCollection ᜂ;

			// Token: 0x040011DE RID: 4574
			public XlsPivotCache ᜃ;

			// Token: 0x040011DF RID: 4575
			public Dictionary<int, XlsPivotCache>.ValueCollection.Enumerator ᜄ;
		}
	}
}
