using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.PivotTables;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000025 RID: 37
	public class XlsPivotCachesCollection : ICloneParent, IPivotCaches, IEnumerable<XlsPivotCache>
	{
		// Token: 0x17000107 RID: 263
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
				return this.ᜂ[id];
			}
		}

		// Token: 0x17000108 RID: 264
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
				return this.ᜂ[A_0];
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0001725C File Offset: 0x0001625C
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
				return this.ᜂ.Count;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000294 RID: 660 RVA: 0x000172A4 File Offset: 0x000162A4
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
				return this.ᜁ.AppImplementation;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000172EC File Offset: 0x000162EC
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
				return this.ᜁ;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00017330 File Offset: 0x00016330
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
				return this.ᜃ;
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00017374 File Offset: 0x00016374
		internal XlsPivotCachesCollection(spr\u1DF5 A_0, object A_1)
		{
			this.ᜁ = this.ᜀ(A_1);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000173AC File Offset: 0x000163AC
		internal XlsPivotCachesCollection(spr\u1DF5 A_0, object A_1, spr\u20C3 A_2, IDecryptor A_3) : this(A_0, A_1)
		{
			this.ᜀ(A_2, A_3);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000173CC File Offset: 0x000163CC
		internal void ᜀ(spr\u20C3 A_0, IDecryptor A_1)
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					spr\u20C3 spr_u20C;
					spr\u20C3 spr_u20C2;
					switch (num)
					{
					case 0:
						if (A_0.ᜇ(RecordTableEnumerator.b("改漻昽Ἷفك᥅େὉṋ", a_)))
						{
							num = 3;
							continue;
						}
						return;
					case 1:
						goto IL_54;
					case 3:
						goto IL_1FB;
					case 4:
						try
						{
							for (;;)
							{
								string[] array = spr_u20C.ᜁ();
								int num2 = 0;
								int num3 = array.Length;
								num = 2;
								for (;;)
								{
									spr\u1FDC spr_u1FDC;
									string text;
									switch (num)
									{
									case 0:
										goto IL_BD;
									case 1:
										goto IL_C6;
									case 2:
										goto IL_BD;
									case 3:
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
											goto IL_92;
										}
										finally
										{
											num = 1;
											for (;;)
											{
												switch (num)
												{
												case 0:
													goto IL_187;
												case 2:
													((IDisposable)spr_u1FDC).Dispose();
													num = 0;
													continue;
												}
												if (spr_u1FDC == null)
												{
													break;
												}
												num = 2;
											}
											IL_187:;
										}
										goto IL_18A;
										IL_92:
										num2++;
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_C6;
										default:
											if (false)
											{
											}
											num = 0;
											continue;
										}
										break;
									case 4:
										num = 5;
										continue;
									case 5:
										goto IL_1B6;
									}
									break;
									IL_BD:
									num = 1;
									continue;
									IL_C6:
									if (num2 >= num3)
									{
										num = 4;
										continue;
									}
									IL_18A:
									text = array[num2];
									spr_u1FDC = spr_u20C.ᜁ(text);
									num = 3;
								}
							}
							IL_1B6:
							return;
						}
						finally
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_1F8;
								case 2:
									spr_u20C2.Dispose();
									num = 1;
									continue;
								}
								if (spr_u20C2 == null)
								{
									break;
								}
								num = 2;
							}
							IL_1F8:;
						}
						goto IL_1FB;
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 1;
						continue;
					}
					this.Clear();
					spr_u20C = null;
					num = 0;
					continue;
					IL_1FB:
					spr_u20C = (spr_u20C2 = A_0.ᜅ(RecordTableEnumerator.b("改漻昽Ἷفك᥅େὉṋ", a_)));
					num = 4;
				}
				IL_54:
				throw new ArgumentNullException(RecordTableEnumerator.b("䤹䠻䰽┿⍁⥃", a_));
			}
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000176A8 File Offset: 0x000166A8
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
			this.ᜂ.Clear();
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000176F0 File Offset: 0x000166F0
		internal void ᜀ(spr\u20C3 A_0, IEncryptor A_1)
		{
			int a_ = 8;
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
						goto IL_27A;
					case 2:
						goto IL_54;
					case 3:
					{
						int count;
						if (count == 0)
						{
							num = 4;
							continue;
						}
						spr\u20C3 spr_u20C = A_0.ᜄ(RecordTableEnumerator.b("愽ጿᩁᭃɅੇᕉཋ᭍ɏ", a_));
						num = 0;
						continue;
					}
					case 4:
						goto IL_257;
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 2;
					}
					else
					{
						int count = this.Count;
						num = 3;
					}
				}
				IL_54:
				throw new ArgumentNullException(RecordTableEnumerator.b("䴽㐿ぁ⅃❅╇", a_));
				IL_257:
				return;
				IL_27A:
				try
				{
					using (Dictionary<int, XlsPivotCache>.ValueCollection.Enumerator enumerator = this.ᜂ.Values.GetEnumerator())
					{
						num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (!enumerator.MoveNext())
								{
									num = 2;
									continue;
								}
								XlsPivotCache xlsPivotCache = enumerator.Current;
								string a_2 = xlsPivotCache.StreamId.ToString(RecordTableEnumerator.b("昽琿", a_));
								spr\u20C3 spr_u20C;
								spr\u1FDC spr_u1FDC = spr_u20C.ᜀ(a_2);
								num = 1;
								continue;
							}
							case 1:
								try
								{
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
										num = 1;
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
													goto IL_15F;
												case 2:
													goto IL_16F;
												}
												if (sprᡄ != null)
												{
													num = 0;
													continue;
												}
												goto IL_171;
											}
											IL_15F:
											((IDisposable)sprᡄ).Dispose();
											num = 2;
										}
										IL_16F:
										IL_171:;
									}
									break;
								}
								finally
								{
									num = 2;
									for (;;)
									{
										spr\u1FDC spr_u1FDC;
										switch (num)
										{
										case 0:
											goto IL_1B4;
										case 1:
											((IDisposable)spr_u1FDC).Dispose();
											num = 0;
											continue;
										}
										if (spr_u1FDC == null)
										{
											break;
										}
										num = 1;
									}
									IL_1B4:;
								}
								goto IL_1B7;
							case 2:
								goto IL_1B7;
							case 3:
								goto IL_1C3;
							}
							IL_D0:
							num = 0;
							continue;
							goto IL_D0;
							IL_1B7:
							num = 3;
						}
						IL_1C3:;
					}
					return;
				}
				finally
				{
					num = 2;
					for (;;)
					{
						spr\u20C3 spr_u20C;
						switch (num)
						{
						case 0:
							spr_u20C.Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_213;
						}
						if (spr_u20C == null)
						{
							break;
						}
						num = 0;
					}
					IL_213:;
				}
				return;
			}
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000179E0 File Offset: 0x000169E0
		public void Add(XlsPivotCache cache)
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
			int key = cache.Index = this.ᜀ(cache);
			this.ᜂ.Add(key, cache);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00017A3C File Offset: 0x00016A3C
		internal void ᜀ(int A_0, XlsPivotCache A_1)
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
			A_1.Index = A_0;
			this.ᜂ.Add(A_0, A_1);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00017A90 File Offset: 0x00016A90
		public PivotCache Add(CellRange range)
		{
			int a_ = 15;
			if (range != null)
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
					PivotCache pivotCache = new PivotCache((spr\u2158)this.Application, this, range);
					this.Add(pivotCache);
					return pivotCache;
				}
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㝄♆❈ⱊ⡌", a_));
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00017B08 File Offset: 0x00016B08
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
			this.Add(A_1);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00017B4C File Offset: 0x00016B4C
		private int ᜀ(XlsPivotCache A_0)
		{
			int num;
			for (;;)
			{
				IL_18:
				num = (int)A_0.StreamId;
				for (;;)
				{
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (!this.ᜂ.ContainsKey(num))
							{
								num2 = 1;
								continue;
							}
							num++;
							num2 = 3;
							continue;
						case 1:
							goto IL_59;
						case 2:
							if (true)
							{
							}
							goto IL_3B;
						case 3:
							goto IL_3B;
						}
						goto IL_18;
						IL_3B:
						num2 = 0;
					}
					IL_59:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_6F;
					}
				}
			}
			IL_6F:
			if (false)
			{
			}
			A_0.StreamId = (ushort)num;
			return num;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00017BEC File Offset: 0x00016BEC
		public void RemoveAt(int index)
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
			this.ᜂ.Remove(index);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00017C34 File Offset: 0x00016C34
		public int[] GetIndexes()
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
			int[] array = new int[this.ᜂ.Count];
			this.ᜂ.Keys.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00017C94 File Offset: 0x00016C94
		public object Clone(object parent)
		{
			for (;;)
			{
				switch (0)
				{
				default:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_24;
					}
					break;
				}
			}
			IL_24:
			if (false)
			{
			}
			PivotCacheCollection pivotCacheCollection = (PivotCacheCollection)base.MemberwiseClone();
			pivotCacheCollection.ᜀ = this.ᜀ(parent);
			Dictionary<int, XlsPivotCache>.ValueCollection.Enumerator enumerator = this.ᜂ.Values.GetEnumerator();
			try
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_CB;
					case 3:
					{
						if (!enumerator.MoveNext())
						{
							num = 4;
							continue;
						}
						XlsPivotCache xlsPivotCache = enumerator.Current;
						XlsPivotCache a_ = (XlsPivotCache)xlsPivotCache.Clone(pivotCacheCollection);
						pivotCacheCollection.ᜁ(a_);
						num = 2;
						continue;
					}
					case 4:
						num = 0;
						continue;
					}
					IL_A5:
					num = 3;
					continue;
					goto IL_A5;
				}
				IL_CB:;
			}
			finally
			{
				if (true)
				{
				}
				((IDisposable)enumerator).Dispose();
			}
			return pivotCacheCollection;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00017DA0 File Offset: 0x00016DA0
		private XlsWorkbook ᜀ(object A_0)
		{
			int a_ = 10;
			XlsWorkbook xlsWorkbook = (XlsWorkbook)XlsObject.FindParent(A_0, typeof(XlsWorkbook));
			if (xlsWorkbook != null)
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
					return xlsWorkbook;
				}
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("̿⍁⩃⡅❇㹉汋⡍㥏㱑こ癕⡗㭙⹛㭝๟ᙡ䑣ᅥݧᡩݫ౭Ὧᵱέ", a_));
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00017E14 File Offset: 0x00016E14
		public IEnumerator<XlsPivotCache> GetEnumerator()
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
			XlsPivotCachesCollection.ᜁ ᜁ = new XlsPivotCachesCollection.ᜁ(0);
			ᜁ.ᜂ = this;
			return ᜁ;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00017E60 File Offset: 0x00016E60
		IEnumerator IEnumerable.GetEnumerator()
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
			XlsPivotCachesCollection.ᜀ ᜀ = new XlsPivotCachesCollection.ᜀ(0);
			ᜀ.ᜂ = this;
			return ᜀ;
		}

		// Token: 0x0400007C RID: 124
		internal const string ᜀ = "_SX_DB_CUR";

		// Token: 0x0400007D RID: 125
		private XlsWorkbook ᜁ;

		// Token: 0x0400007E RID: 126
		private Dictionary<int, XlsPivotCache> ᜂ = new Dictionary<int, XlsPivotCache>();

		// Token: 0x0400007F RID: 127
		private List<int> ᜃ = new List<int>();

		// Token: 0x020001FB RID: 507
		[CompilerGenerated]
		private sealed class ᜁ : IEnumerator<XlsPivotCache>
		{
			// Token: 0x06001C97 RID: 7319 RVA: 0x000F7100 File Offset: 0x000F6100
			bool IEnumerator.ᜁ()
			{
				bool result;
				try
				{
					for (;;)
					{
						int num = this.ᜁ;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_3F;
							case 1:
								num2 = 5;
								continue;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_3F;
								default:
									if (false)
									{
									}
									goto IL_5B;
								}
								break;
							case 3:
								goto IL_B3;
							case 4:
								this.ᜀ();
								num2 = 6;
								continue;
							case 5:
								goto IL_147;
							case 6:
								goto IL_147;
							case 7:
								goto IL_154;
							case 8:
								goto IL_5B;
							case 9:
								if (!this.ᜄ.MoveNext())
								{
									if (true)
									{
									}
									num2 = 4;
									continue;
								}
								this.ᜃ = this.ᜄ.Current;
								this.ᜀ = this.ᜃ;
								this.ᜁ = 2;
								result = true;
								num2 = 3;
								continue;
							}
							break;
							IL_3F:
							switch (num)
							{
							case 0:
								this.ᜁ = -1;
								this.ᜄ = this.ᜂ.ᜂ.Values.GetEnumerator();
								this.ᜁ = 1;
								num2 = 2;
								continue;
							case 1:
								IL_147:
								result = false;
								num2 = 7;
								continue;
							case 2:
								this.ᜁ = 1;
								num2 = 8;
								continue;
							default:
								num2 = 1;
								continue;
							}
							IL_5B:
							num2 = 9;
						}
					}
					IL_B3:
					IL_154:;
				}
				catch
				{
					this.ᜃ();
					throw;
				}
				return result;
			}

			// Token: 0x06001C98 RID: 7320 RVA: 0x000F7294 File Offset: 0x000F6294
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

			// Token: 0x06001C99 RID: 7321 RVA: 0x000F72D8 File Offset: 0x000F62D8
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

			// Token: 0x06001C9A RID: 7322 RVA: 0x000F7318 File Offset: 0x000F6318
			void IDisposable.ᜃ()
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

			// Token: 0x06001C9B RID: 7323 RVA: 0x000F7388 File Offset: 0x000F6388
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

			// Token: 0x06001C9C RID: 7324 RVA: 0x000F73CC File Offset: 0x000F63CC
			[DebuggerHidden]
			public ᜁ(int A_0)
			{
				this.ᜁ = A_0;
			}

			// Token: 0x06001C9D RID: 7325 RVA: 0x000F73E8 File Offset: 0x000F63E8
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

			// Token: 0x04001088 RID: 4232
			private XlsPivotCache ᜀ;

			// Token: 0x04001089 RID: 4233
			private int ᜁ;

			// Token: 0x0400108A RID: 4234
			public XlsPivotCachesCollection ᜂ;

			// Token: 0x0400108B RID: 4235
			public XlsPivotCache ᜃ;

			// Token: 0x0400108C RID: 4236
			public Dictionary<int, XlsPivotCache>.ValueCollection.Enumerator ᜄ;
		}

		// Token: 0x020001FC RID: 508
		[CompilerGenerated]
		private sealed class ᜀ : IEnumerator<object>
		{
			// Token: 0x06001C9E RID: 7326 RVA: 0x000F743C File Offset: 0x000F643C
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
								goto IL_151;
							case 1:
								goto IL_5B;
							case 2:
								goto IL_B3;
							case 3:
								num2 = 8;
								continue;
							case 4:
								goto IL_5B;
							case 5:
								this.ᜀ();
								num2 = 6;
								continue;
							case 6:
								goto IL_F2;
							case 7:
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
								num2 = 2;
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
								switch (num)
								{
								case 0:
									this.ᜁ = -1;
									this.ᜄ = this.ᜂ.ᜂ.Values.GetEnumerator();
									this.ᜁ = 1;
									num2 = 1;
									continue;
								case 1:
									goto IL_144;
								case 2:
									this.ᜁ = 1;
									num2 = 4;
									continue;
								default:
									num2 = 3;
									continue;
								}
								break;
							}
							break;
							IL_5B:
							num2 = 7;
							continue;
							IL_144:
							result = false;
							num2 = 0;
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
					this.ᜁ();
					throw;
				}
				return result;
			}

			// Token: 0x06001C9F RID: 7327 RVA: 0x000F75CC File Offset: 0x000F65CC
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

			// Token: 0x06001CA0 RID: 7328 RVA: 0x000F7610 File Offset: 0x000F6610
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

			// Token: 0x06001CA1 RID: 7329 RVA: 0x000F7650 File Offset: 0x000F6650
			void IDisposable.ᜁ()
			{
				switch (this.ᜁ)
				{
				case 1:
				case 2:
					try
					{
						goto IL_23;
					}
					finally
					{
						this.ᜀ();
					}
					break;
					IL_23:
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
						return;
					}
					break;
				}
			}

			// Token: 0x06001CA2 RID: 7330 RVA: 0x000F76C0 File Offset: 0x000F66C0
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

			// Token: 0x06001CA3 RID: 7331 RVA: 0x000F7704 File Offset: 0x000F6704
			[DebuggerHidden]
			public ᜀ(int A_0)
			{
				this.ᜁ = A_0;
			}

			// Token: 0x06001CA4 RID: 7332 RVA: 0x000F7720 File Offset: 0x000F6720
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

			// Token: 0x0400108D RID: 4237
			private object ᜀ;

			// Token: 0x0400108E RID: 4238
			private int ᜁ;

			// Token: 0x0400108F RID: 4239
			public XlsPivotCachesCollection ᜂ;

			// Token: 0x04001090 RID: 4240
			public XlsPivotCache ᜃ;

			// Token: 0x04001091 RID: 4241
			public Dictionary<int, XlsPivotCache>.ValueCollection.Enumerator ᜄ;
		}
	}
}
