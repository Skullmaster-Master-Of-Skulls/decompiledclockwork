using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Spire.Xls.Calculation;
using Spire.Xls.Collections;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000600 RID: 1536
	public class XlsExternWorksheet : XlsObject, IInternalWorksheet, ICloneParent
	{
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06005A21 RID: 23073 RVA: 0x00386B88 File Offset: 0x00385B88
		// (remove) Token: 0x06005A22 RID: 23074 RVA: 0x00386C20 File Offset: 0x00385C20
		public event XlsRange.CellValueChangedEventHandler CellValueChanged
		{
			add
			{
				for (;;)
				{
					IL_14:
					XlsRange.CellValueChangedEventHandler cellValueChangedEventHandler = this.ᜌ;
					if (true)
					{
					}
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_47:
						goto IL_53;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
					XlsRange.CellValueChangedEventHandler cellValueChangedEventHandler2;
					for (;;)
					{
						IL_02:
						switch (num)
						{
						case 0:
							return;
						case 1:
							goto IL_47;
						case 2:
							if (cellValueChangedEventHandler == cellValueChangedEventHandler2)
							{
								num = 0;
								continue;
							}
							goto IL_53;
						}
						goto IL_14;
					}
					IL_53:
					cellValueChangedEventHandler2 = cellValueChangedEventHandler;
					XlsRange.CellValueChangedEventHandler value2 = (XlsRange.CellValueChangedEventHandler)Delegate.Combine(cellValueChangedEventHandler2, value);
					cellValueChangedEventHandler = Interlocked.CompareExchange<XlsRange.CellValueChangedEventHandler>(ref this.ᜌ, value2, cellValueChangedEventHandler2);
					num = 2;
					goto IL_02;
				}
			}
			remove
			{
				for (;;)
				{
					IL_14:
					XlsRange.CellValueChangedEventHandler cellValueChangedEventHandler = this.ᜌ;
					if (true)
					{
					}
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_47:
						goto IL_53;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
					XlsRange.CellValueChangedEventHandler cellValueChangedEventHandler2;
					for (;;)
					{
						IL_02:
						switch (num)
						{
						case 0:
							if (cellValueChangedEventHandler == cellValueChangedEventHandler2)
							{
								num = 1;
								continue;
							}
							goto IL_53;
						case 1:
							return;
						case 2:
							goto IL_47;
						}
						goto IL_14;
					}
					IL_53:
					cellValueChangedEventHandler2 = cellValueChangedEventHandler;
					XlsRange.CellValueChangedEventHandler value2 = (XlsRange.CellValueChangedEventHandler)Delegate.Remove(cellValueChangedEventHandler2, value);
					cellValueChangedEventHandler = Interlocked.CompareExchange<XlsRange.CellValueChangedEventHandler>(ref this.ᜌ, value2, cellValueChangedEventHandler2);
					num = 0;
					goto IL_02;
				}
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06005A23 RID: 23075 RVA: 0x00386CB8 File Offset: 0x00385CB8
		// (remove) Token: 0x06005A24 RID: 23076 RVA: 0x00386D50 File Offset: 0x00385D50
		public event XlsWorksheet.ErrorFunctionEventHandler MissingFunction
		{
			add
			{
				for (;;)
				{
					IL_14:
					XlsWorksheet.ErrorFunctionEventHandler errorFunctionEventHandler = this.\u170D;
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_3F:
						goto IL_41;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
					XlsWorksheet.ErrorFunctionEventHandler errorFunctionEventHandler2;
					for (;;)
					{
						IL_02:
						switch (num)
						{
						case 0:
							return;
						case 1:
							goto IL_3F;
						case 2:
							if (errorFunctionEventHandler == errorFunctionEventHandler2)
							{
								if (true)
								{
								}
								num = 0;
								continue;
							}
							goto IL_41;
						}
						goto IL_14;
					}
					IL_41:
					errorFunctionEventHandler2 = errorFunctionEventHandler;
					XlsWorksheet.ErrorFunctionEventHandler value2 = (XlsWorksheet.ErrorFunctionEventHandler)Delegate.Combine(errorFunctionEventHandler2, value);
					errorFunctionEventHandler = Interlocked.CompareExchange<XlsWorksheet.ErrorFunctionEventHandler>(ref this.\u170D, value2, errorFunctionEventHandler2);
					num = 2;
					goto IL_02;
				}
			}
			remove
			{
				for (;;)
				{
					IL_14:
					XlsWorksheet.ErrorFunctionEventHandler errorFunctionEventHandler = this.\u170D;
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_3F:
						goto IL_41;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
					XlsWorksheet.ErrorFunctionEventHandler errorFunctionEventHandler2;
					for (;;)
					{
						IL_02:
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (errorFunctionEventHandler == errorFunctionEventHandler2)
							{
								if (true)
								{
								}
								num = 0;
								continue;
							}
							goto IL_41;
						case 2:
							goto IL_3F;
						}
						goto IL_14;
					}
					IL_41:
					errorFunctionEventHandler2 = errorFunctionEventHandler;
					XlsWorksheet.ErrorFunctionEventHandler value2 = (XlsWorksheet.ErrorFunctionEventHandler)Delegate.Remove(errorFunctionEventHandler2, value);
					errorFunctionEventHandler = Interlocked.CompareExchange<XlsWorksheet.ErrorFunctionEventHandler>(ref this.\u170D, value2, errorFunctionEventHandler2);
					num = 1;
					goto IL_02;
				}
			}
		}

		// Token: 0x06005A25 RID: 23077 RVA: 0x00386DE8 File Offset: 0x00385DE8
		internal XlsExternWorksheet(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 2;
			this.ᜀ = (spr᥊)spr\u175E.ᜀ(TBIFFRecord.XCT);
			this.ᜁ = new List<BiffRecordRaw>();
			this.ᜅ = -1;
			this.ᜆ = int.MaxValue;
			this.ᜇ = -1;
			this.ᜈ = int.MaxValue;
			this.ᜊ = 9;
			base..ctor(A_0, A_1);
			this.ᜂ = (XlsExternWorkbook)A_1;
			this.ᜄ = new XlsCellRecordCollection(base.AppImplementation, this);
			this.ᜉ = new Dictionary<string, string>();
			this.ᜉ[RecordTableEnumerator.b("䨷弹娻䰽┿ㅁⱃͅ㩇㡉⍋㱍", a_)] = RecordTableEnumerator.b("ष", a_);
		}

		// Token: 0x06005A26 RID: 23078 RVA: 0x00386EA0 File Offset: 0x00385EA0
		internal int ᜀ(BiffRecordRaw[] A_0, int A_1)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					BiffRecordRaw biffRecordRaw;
					int num2;
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
						case 1:
							return A_1;
						case 2:
							if (A_1 > A_0.Length - 1)
							{
								num = 5;
								continue;
							}
							biffRecordRaw = A_0[A_1];
							num = 3;
							continue;
						case 3:
						{
							if (biffRecordRaw.TypeCode != TBIFFRecord.XCT)
							{
								num = 11;
								continue;
							}
							spr᥊ spr᥊ = (spr᥊)biffRecordRaw;
							A_1++;
							this.ᜁ.Clear();
							this.ᜁ.Add(spr᥊);
							num2 = 0;
							int num3 = (int)spr᥊.ᜁ();
							num = 6;
							continue;
						}
						case 4:
							goto IL_81;
						case 5:
							goto IL_1D5;
						case 6:
							goto IL_172;
						case 7:
							num = 2;
							continue;
						case 8:
						{
							int num3;
							if (num2 >= num3)
							{
								num = 1;
								continue;
							}
							goto IL_119;
						}
						case 9:
							if (A_1 >= 0)
							{
								num = 7;
								continue;
							}
							goto IL_190;
						case 10:
							goto IL_172;
						case 11:
							return A_1;
						}
						if (A_0 == null)
						{
							num = 4;
							continue;
						}
						num = 9;
						continue;
						IL_172:
						num = 8;
						continue;
					}
					IL_119:
					biffRecordRaw = A_0[A_1];
					biffRecordRaw.CheckTypeCode(TBIFFRecord.CRN);
					this.ᜀ((spr\u1BAA)biffRecordRaw);
					this.ᜁ.Add(biffRecordRaw);
					num2++;
					A_1++;
					num = 10;
				}
				IL_81:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("ℿぁ㙃Ʌ⥇㹉ⵋ", a_));
				IL_190:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⼿⑁≃㕅ⵇ㹉", a_), RecordTableEnumerator.b("ᘿ⍁⡃㍅ⵇ橉⽋⽍㹏㱑㭓≕硗㡙㥛繝౟ݡᝣᕥ䡧ṩѫ཭ṯ剱䑳噵᥷ᑹ᡻幽ﲇﺋ꺍晴뢗ﺙﶛ솟芡좣쎥욧충\ud8ab욭麯", a_));
				IL_1D5:
				goto IL_190;
			}
			}
		}

		// Token: 0x06005A27 RID: 23079 RVA: 0x00387088 File Offset: 0x00386088
		private void ᜀ(spr\u1BAA A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num = (int)(A_0.ᜀ() + 1);
					int num2 = (int)(A_0.ᜅ() + 1);
					int num3 = 0;
					int num4 = 10;
					for (;;)
					{
						switch (num4)
						{
						case 0:
						{
							object obj;
							this.ᜄ.SetNumberValue(num, num2, (double)obj, 0);
							num4 = 14;
							continue;
						}
						case 1:
						{
							if (num2 > (int)(A_0.ᜁ() + 1))
							{
								num4 = 4;
								continue;
							}
							object obj = A_0.ᜄ()[num3];
							string text = obj as string;
							goto IL_10E;
						}
						case 2:
							goto IL_15E;
						case 3:
							goto IL_E3;
						case 4:
							return;
						case 5:
							if (true)
							{
							}
							goto IL_E3;
						case 6:
						{
							object obj;
							this.ᜄ.SetErrorValue(num, num2, (byte)obj, 0);
							num4 = 3;
							continue;
						}
						case 7:
							goto IL_E3;
						case 8:
						{
							string text;
							if (text != null)
							{
								num4 = 11;
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
								num4 = 9;
								continue;
							}
							break;
						}
						case 9:
						{
							object obj;
							if (obj is double)
							{
								num4 = 0;
								continue;
							}
							num4 = 13;
							continue;
						}
						case 10:
							goto IL_15E;
						case 11:
						{
							string text;
							this.ᜄ.ᜀ(num, num2, 0, text);
							num4 = 5;
							continue;
						}
						case 12:
						{
							object obj;
							if (obj is byte)
							{
								num4 = 6;
								continue;
							}
							goto IL_E3;
						}
						case 13:
						{
							object obj;
							if (obj is bool)
							{
								num4 = 15;
								continue;
							}
							num4 = 12;
							continue;
						}
						case 14:
							goto IL_E3;
						case 15:
						{
							object obj;
							this.ᜄ.SetBooleanValue(num, num2, (bool)obj, 0);
							num4 = 7;
							continue;
						}
						}
						break;
						IL_E3:
						num2++;
						num3++;
						num4 = 2;
						continue;
						IL_10E:
						num4 = 8;
						continue;
						IL_15E:
						num4 = 1;
					}
				}
				return;
			}
		}

		// Token: 0x06005A28 RID: 23080 RVA: 0x003872AC File Offset: 0x003862AC
		internal void ᜀ(sprἛ A_0, IDecryptor A_1)
		{
			int a_ = 17;
			int num = 7;
			for (;;)
			{
				TBIFFRecord tbiffrecord;
				switch (num)
				{
				case 0:
				{
					if (tbiffrecord != TBIFFRecord.XCT)
					{
						num = 5;
						continue;
					}
					BiffRecordRaw biffRecordRaw = A_0.ᜀ(A_1);
					this.ᜀ = (spr᥊)biffRecordRaw;
					this.ᜁ.Clear();
					tbiffrecord = A_0.ᜉ();
					num = 6;
					continue;
				}
				case 1:
					goto IL_51;
				case 2:
					IL_92:
					goto IL_A9;
				case 3:
					return;
				case 4:
				{
					if (tbiffrecord != TBIFFRecord.CRN)
					{
						num = 3;
						continue;
					}
					BiffRecordRaw biffRecordRaw = A_0.ᜀ(A_1);
					biffRecordRaw.CheckTypeCode(TBIFFRecord.CRN);
					this.ᜀ((spr\u1BAA)biffRecordRaw);
					this.ᜁ.Add(biffRecordRaw);
					tbiffrecord = A_0.ᜉ();
					if (true)
					{
					}
					num = 2;
					continue;
				}
				case 5:
					return;
				case 6:
					goto IL_A9;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				tbiffrecord = A_0.ᜉ();
				num = 0;
				continue;
				IL_A9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_92;
				default:
					if (false)
					{
					}
					num = 4;
					break;
				}
			}
			IL_51:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⩊⥌⩎⍐", a_));
		}

		// Token: 0x06005A29 RID: 23081 RVA: 0x003873F8 File Offset: 0x003863F8
		internal void ᜁ(RecordArrayList A_0)
		{
			int a_ = 2;
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
					throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹弻儽㈿♁㝃", a_));
				}
			}
			A_0.ᜀ(this.ᜀ);
			this.ᜀ(A_0);
		}

		// Token: 0x06005A2A RID: 23082 RVA: 0x0038746C File Offset: 0x0038646C
		private void ᜀ(RecordArrayList A_0)
		{
			int a_ = 6;
			int num = 6;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_8C;
				case 1:
					num2 = this.ᜅ;
					num = 0;
					continue;
				case 2:
					goto IL_47;
				case 3:
					if (this.ᜅ >= 0)
					{
						num = 1;
						continue;
					}
					return;
				case 4:
					goto IL_8C;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_51;
					default:
						goto IL_C1;
					}
					break;
				case 7:
					if (num2 > this.ᜇ)
					{
						num = 5;
						continue;
					}
					goto IL_51;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 3;
				continue;
				IL_51:
				this.ᜀ(num2, A_0);
				num2++;
				num = 4;
				continue;
				IL_8C:
				num = 7;
			}
			IL_47:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("主嬽⌿ⵁ㙃≅㭇", a_));
			IL_C1:
			if (false)
			{
			}
		}

		// Token: 0x06005A2B RID: 23083 RVA: 0x00387570 File Offset: 0x00386570
		private void ᜀ(int A_0, RecordArrayList A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					sprᱧ sprᱧ = sprᜑ.ᜀ(this, A_0 - 1, false);
					int num = 13;
					for (;;)
					{
						List<object> list;
						object item;
						int num2;
						int num3;
						spr\u1BAA spr_u1BAA;
						switch (num)
						{
						case 0:
							if (list.Count > 0)
							{
								num = 14;
								continue;
							}
							return;
						case 1:
						{
							IEnumerator enumerator;
							if (!enumerator.MoveNext())
							{
								num = 2;
								continue;
							}
							BiffRecordRaw biffRecordRaw = (BiffRecordRaw)enumerator.Current;
							spr\u1929 spr_u = (spr\u1929)biffRecordRaw;
							item = spr_u.ᜀ();
							num2 = (biffRecordRaw as spr\u23A5).ᜅ();
							num = 3;
							continue;
						}
						case 2:
							num = 0;
							continue;
						case 3:
							if (num3 < 0)
							{
								num = 8;
								continue;
							}
							num = 7;
							continue;
						case 4:
						{
							IEnumerator enumerator = sprᱧ.ᜀ(this.ᜄ.RecordExtractor);
							spr_u1BAA = (spr\u1BAA)spr\u175E.ᜀ(TBIFFRecord.CRN);
							spr_u1BAA.ᜀ((ushort)(A_0 - 1));
							num3 = -1;
							list = spr_u1BAA.ᜄ();
							num = 11;
							continue;
						}
						case 5:
							goto IL_1F9;
						case 6:
							A_1.ᜀ(spr_u1BAA);
							spr_u1BAA = (spr\u1BAA)spr\u175E.ᜀ(TBIFFRecord.CRN);
							spr_u1BAA.ᜀ((ushort)(A_0 - 1));
							spr_u1BAA.ᜁ((byte)num2);
							list = spr_u1BAA.ᜄ();
							num = 9;
							continue;
						case 7:
							if (num3 + 1 != num2)
							{
								num = 6;
								continue;
							}
							goto IL_1BF;
						case 8:
							spr_u1BAA.ᜁ((byte)num2);
							num = 10;
							continue;
						case 9:
							goto IL_1BF;
						case 10:
							goto IL_1BF;
						case 11:
							goto IL_9A;
						case 12:
							goto IL_9A;
						case 13:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								if (sprᱧ != null)
								{
									num = 4;
									continue;
								}
								return;
							}
							break;
						case 14:
							A_1.ᜀ(spr_u1BAA);
							num = 5;
							continue;
						}
						break;
						IL_9A:
						num = 1;
						continue;
						IL_1BF:
						list.Add(item);
						spr_u1BAA.ᜀ((byte)num2);
						num3 = num2;
						num = 12;
					}
				}
				IL_1F9:
				if (true)
				{
				}
				return;
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06005A2C RID: 23084 RVA: 0x003877C8 File Offset: 0x003867C8
		// (set) Token: 0x06005A2D RID: 23085 RVA: 0x00387810 File Offset: 0x00386810
		public int Index
		{
			get
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
				return (int)this.ᜀ.ᜀ();
			}
			set
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
				this.ᜀ.ᜀ((ushort)value);
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06005A2E RID: 23086 RVA: 0x00387858 File Offset: 0x00386858
		internal XlsExternWorkbook Workbook
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

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06005A2F RID: 23087 RVA: 0x0038789C File Offset: 0x0038689C
		internal int ReferenceIndex
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
				XlsWorkbook workbook = this.ᜂ.Workbook;
				return workbook.AddSheetReference(this.ᜂ.Index, this.Index, this.Index);
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06005A30 RID: 23088 RVA: 0x00387900 File Offset: 0x00386900
		// (set) Token: 0x06005A31 RID: 23089 RVA: 0x00387944 File Offset: 0x00386944
		internal Dictionary<string, string> AdditionalAttributes
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
				return this.ᜉ;
			}
			set
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
				this.ᜉ = value;
			}
		}

		// Token: 0x06005A32 RID: 23090 RVA: 0x00387988 File Offset: 0x00386988
		public object Clone(object parent)
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
			XlsExternWorksheet xlsExternWorksheet = (XlsExternWorksheet)base.MemberwiseClone();
			this.ᜀ = (spr᥊)spr\u1CD3.ᜀ(this.ᜀ);
			xlsExternWorksheet.SetParent(parent);
			xlsExternWorksheet.ᜂ = (XlsExternWorkbook)xlsExternWorksheet.FindParent(typeof(XlsExternWorkbook));
			xlsExternWorksheet.ᜄ = this.ᜄ.Clone(xlsExternWorksheet);
			this.ᜁ = spr\u1CD3.ᜀ(this.ᜁ);
			return xlsExternWorksheet;
		}

		// Token: 0x06005A33 RID: 23091 RVA: 0x00387A2C File Offset: 0x00386A2C
		protected override void OnDispose()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜄ != null)
					{
						num = 2;
						continue;
					}
					goto IL_3D;
				case 1:
					goto IL_3D;
				case 2:
					this.ᜄ.Dispose();
					this.ᜄ = null;
					num = 1;
					continue;
				case 4:
					num = 0;
					continue;
				case 5:
					return;
				}
				if (!this.m_bIsDisposed)
				{
					num = 4;
					continue;
				}
				break;
				IL_3D:
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
					base.OnDispose();
					num = 5;
					break;
				}
			}
		}

		// Token: 0x06005A34 RID: 23092 RVA: 0x00387AF4 File Offset: 0x00386AF4
		internal void ᜀ(IXLSRange A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num = A_0.Row;
					int lastRow = A_0.LastRow;
					int num2 = 17;
					for (;;)
					{
						int num3;
						IXLSRange ixlsrange;
						switch (num2)
						{
						case 0:
							goto IL_1F8;
						case 1:
						{
							if (num > lastRow)
							{
								num2 = 18;
								continue;
							}
							num3 = A_0.Column;
							int lastColumn = A_0.LastColumn;
							num2 = 14;
							continue;
						}
						case 2:
							if (ixlsrange.HasString)
							{
								num2 = 8;
								continue;
							}
							num2 = 6;
							continue;
						case 3:
							goto IL_27B;
						case 4:
							goto IL_173;
						case 5:
							goto IL_173;
						case 6:
							if (ixlsrange.HasError)
							{
								num2 = 10;
								continue;
							}
							goto IL_173;
						case 7:
							if (ixlsrange.HasBoolean)
							{
								num2 = 15;
								continue;
							}
							num2 = 19;
							continue;
						case 8:
							this.ᜄ.ᜀ(num, num3, 0, ixlsrange.Text);
							num2 = 5;
							continue;
						case 9:
						{
							int lastColumn;
							if (num3 <= lastColumn)
							{
								ixlsrange = A_0[num, num3];
								num2 = 7;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1F8;
							default:
								if (false)
								{
								}
								num2 = 0;
								continue;
							}
							break;
						}
						case 10:
							this.ᜄ.SetErrorValue(num, num3, ixlsrange.ErrorValue);
							num2 = 4;
							continue;
						case 11:
							goto IL_F9;
						case 12:
							if (ixlsrange.HasNumber)
							{
								num2 = 3;
								continue;
							}
							num2 = 2;
							continue;
						case 13:
							goto IL_188;
						case 14:
							goto IL_F9;
						case 15:
							this.ᜄ.SetBooleanValue(num, num3, ixlsrange.BooleanValue, 0);
							num2 = 21;
							continue;
						case 16:
							num2 = 12;
							continue;
						case 17:
							goto IL_188;
						case 18:
							goto IL_1A7;
						case 19:
							if (!ixlsrange.HasDateTime)
							{
								num2 = 16;
								continue;
							}
							goto IL_27B;
						case 20:
							goto IL_173;
						case 21:
							goto IL_173;
						}
						break;
						IL_F9:
						num2 = 9;
						continue;
						IL_173:
						num3++;
						num2 = 11;
						continue;
						IL_188:
						num2 = 1;
						continue;
						IL_1F8:
						num++;
						num2 = 13;
						continue;
						IL_27B:
						this.ᜄ.SetNumberValue(num, num3, ixlsrange.NumberValue, 0);
						num2 = 20;
					}
				}
				IL_1A7:
				if (true)
				{
				}
				return;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06005A35 RID: 23093 RVA: 0x00387DAC File Offset: 0x00386DAC
		// (set) Token: 0x06005A36 RID: 23094 RVA: 0x00387DF0 File Offset: 0x00386DF0
		public FormulaEngine FormulaEngine
		{
			get
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
			set
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
				this.ᜋ = value;
			}
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x00387E34 File Offset: 0x00386E34
		internal void ᜀ()
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					Hashtable hashtable;
					IEnumerator enumerator3;
					IEnumerator enumerator4;
					switch (num)
					{
					case 0:
						if (hashtable != null)
						{
							num = 2;
							continue;
						}
						goto IL_3BB;
					case 1:
					{
						if (true)
						{
						}
						this.FormulaEngine = new FormulaEngine(this);
						this.FormulaEngine.ᜀ.\u170D(true);
						int a_2 = FormulaEngine.ᜁ();
						string text = RecordTableEnumerator.b("ᤷ", a_);
						IEnumerator enumerator = this.ParentWorkbook.Worksheets.GetEnumerator();
						num = 4;
						continue;
					}
					case 2:
					{
						IEnumerator enumerator2 = hashtable.Keys.GetEnumerator();
						num = 7;
						continue;
					}
					case 4:
						goto IL_14E;
					case 5:
					{
						try
						{
							num = 8;
							for (;;)
							{
								INamedRange namedRange;
								switch (num)
								{
								case 0:
								{
									string text;
									if (text.IndexOf(RecordTableEnumerator.b("ᤷ", a_) + namedRange.Scope + RecordTableEnumerator.b("ᤷ", a_)) > -1)
									{
										num = 7;
										continue;
									}
									goto IL_5A2;
								}
								case 2:
									num = 0;
									continue;
								case 3:
									if (namedRange.Scope.Length > 0)
									{
										num = 2;
										continue;
									}
									goto IL_5A2;
								case 4:
									if (!enumerator3.MoveNext())
									{
										num = 6;
										continue;
									}
									namedRange = (INamedRange)enumerator3.Current;
									num = 3;
									continue;
								case 5:
									goto IL_5F0;
								case 6:
									num = 5;
									continue;
								case 7:
									hashtable.Add((namedRange.Scope + RecordTableEnumerator.b("ᤷ", a_) + namedRange.Name).ToUpper(), namedRange.Value.Replace(RecordTableEnumerator.b("ἷ", a_), ""));
									num = 1;
									continue;
								}
								IL_51D:
								num = 4;
								continue;
								goto IL_51D;
								IL_5A2:
								hashtable.Add(namedRange.Name.ToUpper(), namedRange.Value.Replace(RecordTableEnumerator.b("ἷ", a_), ""));
								num = 9;
							}
							IL_5F0:
							goto IL_62;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable = enumerator3 as IDisposable;
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (disposable != null)
										{
											num = 1;
											continue;
										}
										goto IL_63D;
									case 1:
										disposable.Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_63B;
									}
									break;
								}
							}
							IL_63B:
							IL_63D:;
						}
						return;
						IL_62:
						Hashtable hashtable2 = new Hashtable();
						num = 0;
						continue;
					}
					case 6:
						try
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 2:
									goto IL_100;
								case 3:
									num = 2;
									continue;
								case 4:
								{
									if (!enumerator4.MoveNext())
									{
										num = 3;
										continue;
									}
									IWorksheet worksheet = (IWorksheet)enumerator4.Current;
									Hashtable hashtable2;
									worksheet.FormulaEngine.ᜀ.ᜀ(hashtable2);
									num = 0;
									continue;
								}
								}
								IL_DA:
								num = 4;
								continue;
								goto IL_DA;
							}
							IL_100:
							return;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable2 = enumerator4 as IDisposable;
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
										goto IL_14D;
									case 1:
										disposable2.Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_14B;
									}
									break;
								}
							}
							IL_14B:
							IL_14D:;
						}
						goto Block_4;
					case 7:
						goto IL_2BB;
					}
					if (this.FormulaEngine == null)
					{
						num = 1;
						continue;
					}
					break;
					IL_388:
					hashtable = new Hashtable();
					enumerator3 = this.ParentWorkbook.Names.GetEnumerator();
					num = 5;
					continue;
					Block_5:
					try
					{
						IL_2BB:
						num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_33D;
							case 2:
								num = 0;
								continue;
							case 4:
							{
								IEnumerator enumerator2;
								if (!enumerator2.MoveNext())
								{
									num = 2;
									continue;
								}
								string text2 = (string)enumerator2.Current;
								Hashtable hashtable2;
								hashtable2.Add(text2.ToUpper(CultureInfo.InvariantCulture), hashtable[text2]);
								num = 1;
								continue;
							}
							}
							IL_317:
							num = 4;
							continue;
							goto IL_317;
						}
						IL_33D:
						goto IL_3BB;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator2;
							IDisposable disposable3 = enumerator2 as IDisposable;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable3 != null)
									{
										num = 2;
										continue;
									}
									goto IL_387;
								case 1:
									goto IL_385;
								case 2:
									disposable3.Dispose();
									num = 1;
									continue;
								}
								break;
							}
						}
						IL_385:
						IL_387:;
					}
					goto IL_388;
					Block_4:
					try
					{
						IL_14E:
						num = 0;
						for (;;)
						{
							IWorksheet worksheet2;
							switch (num)
							{
							case 1:
								goto IL_26D;
							case 2:
							{
								IEnumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								worksheet2 = (IWorksheet)enumerator.Current;
								goto IL_18F;
							}
							case 3:
								num = 1;
								continue;
							case 4:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_18F;
								default:
									if (false)
									{
									}
									worksheet2.FormulaEngine = new FormulaEngine(worksheet2);
									num = 6;
									continue;
								}
								break;
							case 5:
								if (worksheet2.FormulaEngine == null)
								{
									num = 4;
									continue;
								}
								goto IL_1CB;
							case 6:
								goto IL_1CB;
							}
							goto IL_180;
							IL_18F:
							num = 5;
							continue;
							IL_1AB:
							num = 2;
							continue;
							IL_180:
							goto IL_1AB;
							IL_1CB:
							int a_2;
							this.FormulaEngine.ᜀ.ᜀ(worksheet2.Name, worksheet2, a_2);
							worksheet2.FormulaEngine.ᜀ.ᜁ(new spr\u21C1(this.ᜀ));
							string text = text + worksheet2.Name + RecordTableEnumerator.b("ᤷ", a_);
							num = 7;
						}
						IL_26D:
						goto IL_388;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator;
							IDisposable disposable4 = enumerator as IDisposable;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable4 != null)
									{
										num = 2;
										continue;
									}
									goto IL_2BA;
								case 1:
									goto IL_2B8;
								case 2:
									disposable4.Dispose();
									num = 1;
									continue;
								}
								break;
							}
						}
						IL_2B8:
						IL_2BA:;
					}
					goto Block_5;
					IL_3BB:
					enumerator4 = this.ParentWorkbook.Worksheets.GetEnumerator();
					num = 6;
				}
				return;
			}
			}
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x003884E4 File Offset: 0x003874E4
		internal void ᜁ()
		{
			int num = 2;
			for (;;)
			{
				IEnumerator enumerator;
				switch (num)
				{
				case 0:
					goto IL_186;
				case 1:
					try
					{
						num = 7;
						for (;;)
						{
							IWorksheet worksheet;
							switch (num)
							{
							case 0:
								goto IL_88;
							case 1:
								num = 6;
								continue;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_8F;
								default:
									if (false)
									{
									}
									if (worksheet.FormulaEngine != null)
									{
										num = 4;
										continue;
									}
									goto IL_88;
								}
								break;
							case 4:
								worksheet.FormulaEngine.ᜀ.ᜀ(new spr\u21C1(this.ᜀ));
								worksheet.FormulaEngine.Dispose();
								num = 0;
								continue;
							case 5:
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								worksheet = (IWorksheet)enumerator.Current;
								num = 2;
								continue;
							case 6:
								goto IL_140;
							}
							goto IL_86;
							IL_8F:
							num = 3;
							continue;
							IL_88:
							worksheet.FormulaEngine = null;
							goto IL_8F;
							IL_99:
							num = 5;
							continue;
							IL_86:
							goto IL_99;
						}
						IL_140:
						return;
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
										num = 1;
										continue;
									}
									goto IL_185;
								case 1:
									disposable.Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_183;
								}
								break;
							}
						}
						IL_183:
						IL_185:;
					}
					goto IL_186;
				case 2:
					if (true)
					{
					}
					break;
				case 3:
					num = 4;
					continue;
				case 4:
					if (this.ParentWorkbook.Worksheets != null)
					{
						num = 0;
						continue;
					}
					return;
				case 5:
					num = 6;
					continue;
				case 6:
					if (this.ParentWorkbook != null)
					{
						num = 3;
						continue;
					}
					return;
				}
				if (this.FormulaEngine != null)
				{
					num = 5;
					continue;
				}
				break;
				IL_186:
				enumerator = this.ParentWorkbook.Worksheets.GetEnumerator();
				num = 1;
			}
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x003886F0 File Offset: 0x003876F0
		private void ᜀ(object A_0, spr\u2623 A_1)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					XlsWorksheet.ErrorFunctionEventArgs errorFunctionEventArgs = new XlsWorksheet.ErrorFunctionEventArgs();
					errorFunctionEventArgs.FunctionName = A_1.ᜁ();
					errorFunctionEventArgs.CellRange = A_1.ᜀ();
					this.\u170D(this, errorFunctionEventArgs);
					num = 4;
					continue;
				}
				case 2:
					goto IL_75;
				case 3:
					if (this.FormulaEngine != null)
					{
						num = 0;
						continue;
					}
					goto IL_92;
				case 4:
					goto IL_92;
				}
				if (this.\u170D != null)
				{
					num = 2;
					continue;
				}
				goto IL_92;
				IL_75:
				num = 3;
				continue;
				IL_92:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_75;
				default:
					goto IL_B0;
				}
			}
			IL_B0:
			if (false)
			{
			}
		}

		// Token: 0x06005A3A RID: 23098 RVA: 0x003887B4 File Offset: 0x003877B4
		public object GetCaculateValue(int row, int col)
		{
			IXLSRange ixlsrange = this[row, col];
			if (ixlsrange.HasFormula)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_39;
				}
				if (false)
				{
				}
				IL_39:
				return ixlsrange.Formula;
			}
			if (true)
			{
			}
			return ixlsrange.Value;
		}

		// Token: 0x06005A3B RID: 23099 RVA: 0x00388810 File Offset: 0x00387810
		public void SetCaculateValue(object value, int row, int col)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_5B;
				case 2:
					goto IL_45;
				}
				if (value == null)
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
					num = 2;
					continue;
				}
				IL_45:
				this.SetValue(row, col, value.ToString());
				num = 1;
			}
			IL_5B:
			if (true)
			{
			}
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x0038888C File Offset: 0x0038788C
		internal void ᜃ()
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

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06005A3D RID: 23101 RVA: 0x003888C8 File Offset: 0x003878C8
		// (remove) Token: 0x06005A3E RID: 23102 RVA: 0x00388960 File Offset: 0x00387960
		public event ValueChangedEventHandler CaculateValueChanged
		{
			add
			{
				for (;;)
				{
					IL_14:
					ValueChangedEventHandler valueChangedEventHandler;
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_3F:
						goto IL_4B;
					default:
						if (false)
						{
						}
						valueChangedEventHandler = this.ᜎ;
						num = 0;
						break;
					}
					ValueChangedEventHandler valueChangedEventHandler2;
					for (;;)
					{
						IL_02:
						switch (num)
						{
						case 0:
							goto IL_3F;
						case 1:
							if (true)
							{
							}
							if (valueChangedEventHandler == valueChangedEventHandler2)
							{
								num = 2;
								continue;
							}
							goto IL_4B;
						case 2:
							return;
						}
						goto IL_14;
					}
					IL_4B:
					valueChangedEventHandler2 = valueChangedEventHandler;
					ValueChangedEventHandler value2 = (ValueChangedEventHandler)Delegate.Combine(valueChangedEventHandler2, value);
					valueChangedEventHandler = Interlocked.CompareExchange<ValueChangedEventHandler>(ref this.ᜎ, value2, valueChangedEventHandler2);
					num = 1;
					goto IL_02;
				}
			}
			remove
			{
				for (;;)
				{
					IL_14:
					ValueChangedEventHandler valueChangedEventHandler;
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_3F:
						goto IL_4B;
					default:
						if (false)
						{
						}
						valueChangedEventHandler = this.ᜎ;
						num = 2;
						break;
					}
					ValueChangedEventHandler valueChangedEventHandler2;
					for (;;)
					{
						IL_02:
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (valueChangedEventHandler == valueChangedEventHandler2)
							{
								if (true)
								{
								}
								num = 0;
								continue;
							}
							goto IL_4B;
						case 2:
							goto IL_3F;
						}
						goto IL_14;
					}
					IL_4B:
					valueChangedEventHandler2 = valueChangedEventHandler;
					ValueChangedEventHandler value2 = (ValueChangedEventHandler)Delegate.Remove(valueChangedEventHandler2, value);
					valueChangedEventHandler = Interlocked.CompareExchange<ValueChangedEventHandler>(ref this.ᜎ, value2, valueChangedEventHandler2);
					num = 1;
					goto IL_02;
				}
			}
		}

		// Token: 0x06005A3F RID: 23103 RVA: 0x003889F8 File Offset: 0x003879F8
		public void OnValueChanged(int row, int col, string value)
		{
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 1:
				{
					ValueChangedEventArgs e = new ValueChangedEventArgs(row, col, value);
					this.ᜎ(this, e);
					num = 2;
					continue;
				}
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_74;
					}
					break;
				}
				IL_24:
				if (this.ᜎ != null)
				{
					num = 1;
					continue;
				}
				return;
				goto IL_24;
			}
			IL_74:
			if (false)
			{
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06005A40 RID: 23104 RVA: 0x00388A84 File Offset: 0x00387A84
		public IAutoFilters AutoFilters
		{
			get
			{
				int a_ = 9;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("款⥀♂敄⩆ⱈ㽊╌⁎㕐獒㩔╖祘㑚ⵜ㩞፠ɢᅤ๦٨ժ䵬ٮɰ卲᭴ᡶ൸孺ᑼቾ歷붒", a_));
			}
		}

		// Token: 0x06005A41 RID: 23105 RVA: 0x00388ADC File Offset: 0x00387ADC
		public void SaveToHtml(string filename)
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

		// Token: 0x06005A42 RID: 23106 RVA: 0x00388B18 File Offset: 0x00387B18
		public void SaveToHtml(Stream stream)
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

		// Token: 0x06005A43 RID: 23107 RVA: 0x00388B54 File Offset: 0x00387B54
		public void SaveToHtml(string filename, HTMLOptions saveOptions)
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

		// Token: 0x06005A44 RID: 23108 RVA: 0x00388B90 File Offset: 0x00387B90
		public void SaveToHtml(Stream stream, HTMLOptions saveOptions)
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

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06005A45 RID: 23109 RVA: 0x00388BCC File Offset: 0x00387BCC
		public IXLSRange[] Cells
		{
			get
			{
				int a_ = 3;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("洸区堼Ἶⱀ♂ㅄ⽆♈⽊浌⁎⍐獒㩔❖㱘⥚㱜⭞ࡠౢ୤䝦hᡪ䵬ŮṰݲ啴Ṷᑸ୺ᅼ᩾ꎌ", a_));
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06005A46 RID: 23110 RVA: 0x00388C24 File Offset: 0x00387C24
		// (set) Token: 0x06005A47 RID: 23111 RVA: 0x00388C7C File Offset: 0x00387C7C
		public bool DisplayPageBreaks
		{
			get
			{
				int a_ = 16;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ቅ⁇⽉汋⍍㕏♑㱓㥕㱗穙㍛ⱝ䁟ൡᑣͥᩧ୩ᡫݭὯᱱ味ή୷婹ቻᅽꊁﲑﲗ뒙", a_));
			}
			set
			{
				int a_ = 3;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("洸区堼Ἶⱀ♂ㅄ⽆♈⽊浌⁎⍐獒㩔❖㱘⥚㱜⭞ࡠౢ୤䝦hᡪ䵬ŮṰݲ啴Ṷᑸ୺ᅼ᩾ꎌ", a_));
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06005A48 RID: 23112 RVA: 0x00388CD4 File Offset: 0x00387CD4
		public SheetProtectionType Protection
		{
			get
			{
				int a_ = 15;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06005A49 RID: 23113 RVA: 0x00388D2C File Offset: 0x00387D2C
		public bool ProtectContents
		{
			get
			{
				int a_ = 7;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("椼圾⑀捂⡄≆㵈⍊≌⭎煐㱒❔睖㙘⭚㡜ⵞ`ᝢ౤ࡦݨ䭪Ѭᱮ兰ᵲᩴͶ奸ቺၼཾﾊ뾐", a_));
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06005A4A RID: 23114 RVA: 0x00388D84 File Offset: 0x00387D84
		public bool ProtectDrawingObjects
		{
			get
			{
				int a_ = 1;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("挶儸帺ᴼ刾⑀㝂ⵄ⡆ⵈ歊≌㵎煐㱒╔㉖⭘㩚⥜㙞๠ൢ䕤๦ᩨ䭪ͬnհ卲ᱴ᩶ॸ᝺᡼ቾꖊ", a_));
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06005A4B RID: 23115 RVA: 0x00388DDC File Offset: 0x00387DDC
		public bool ProtectScenarios
		{
			get
			{
				int a_ = 15;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06005A4D RID: 23117 RVA: 0x00388E74 File Offset: 0x00387E74
		// (set) Token: 0x06005A4C RID: 23116 RVA: 0x00388E34 File Offset: 0x00387E34
		public bool HasOleObjects
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
				throw new NotSupportedException();
			}
			set
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
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06005A4E RID: 23118 RVA: 0x00388EB4 File Offset: 0x00387EB4
		public IXLSRange[] MergedCells
		{
			get
			{
				int a_ = 9;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("款⥀♂敄⩆ⱈ㽊╌⁎㕐獒㩔╖祘㑚ⵜ㩞፠ɢᅤ๦٨ժ䵬ٮɰ卲᭴ᡶ൸孺ᑼቾ歷붒", a_));
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06005A4F RID: 23119 RVA: 0x00388F0C File Offset: 0x00387F0C
		public INameRanges Names
		{
			get
			{
				int a_ = 0;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("戵倷弹᰻匽┿㙁ⱃ⥅ⱇ橉⍋㱍灏㵑⑓㍕⩗㭙⡛㝝ཟౡ䑣ཥ᭧䩩ɫŭѯ剱ᵳ᭵ࡷᙹ᥻፽ꒉ", a_));
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06005A50 RID: 23120 RVA: 0x00388F64 File Offset: 0x00387F64
		public string CodeName
		{
			get
			{
				int a_ = 10;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᐿ⩁⅃晅╇⽉㡋♍㽏㙑瑓㥕⩗穙㍛⹝՟ၡգብŧթɫ乭᥯ű味ᡵ᝷๹屻᝽揄몓", a_));
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06005A51 RID: 23121 RVA: 0x00388FBC File Offset: 0x00387FBC
		public IPageSetup PageSetup
		{
			get
			{
				int a_ = 10;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᐿ⩁⅃晅╇⽉㡋♍㽏㙑瑓㥕⩗穙㍛⹝՟ၡգብŧթɫ乭᥯ű味ᡵ᝷๹屻᝽揄몓", a_));
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06005A52 RID: 23122 RVA: 0x00389014 File Offset: 0x00388014
		public IXLSRange AllocatedRange
		{
			get
			{
				int a_ = 0;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("戵倷弹᰻匽┿㙁ⱃ⥅ⱇ橉⍋㱍灏㵑⑓㍕⩗㭙⡛㝝ཟౡ䑣ཥ᭧䩩ɫŭѯ剱ᵳ᭵ࡷᙹ᥻፽ꒉ", a_));
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06005A53 RID: 23123 RVA: 0x0038906C File Offset: 0x0038806C
		public IXLSRange[] Rows
		{
			get
			{
				int a_ = 8;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("樽⠿❁摃⭅ⵇ㹉⑋⅍㑏牑㭓⑕硗㕙ⱛ㭝቟͡ၣཥݧѩ䱫ݭͯ剱ᩳ᥵౷婹ᕻ፽벑", a_));
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06005A54 RID: 23124 RVA: 0x003890C4 File Offset: 0x003880C4
		public IXLSRange[] Columns
		{
			get
			{
				int a_ = 0;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("戵倷弹᰻匽┿㙁ⱃ⥅ⱇ橉⍋㱍灏㵑⑓㍕⩗㭙⡛㝝ཟౡ䑣ཥ᭧䩩ɫŭѯ剱ᵳ᭵ࡷᙹ᥻፽ꒉ", a_));
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06005A55 RID: 23125 RVA: 0x0038911C File Offset: 0x0038811C
		// (set) Token: 0x06005A56 RID: 23126 RVA: 0x00389174 File Offset: 0x00388174
		public double StandardHeight
		{
			get
			{
				int a_ = 15;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
			}
			set
			{
				int a_ = 3;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("洸区堼Ἶⱀ♂ㅄ⽆♈⽊浌⁎⍐獒㩔❖㱘⥚㱜⭞ࡠౢ୤䝦hᡪ䵬ŮṰݲ啴Ṷᑸ୺ᅼ᩾ꎌ", a_));
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06005A57 RID: 23127 RVA: 0x003891CC File Offset: 0x003881CC
		// (set) Token: 0x06005A58 RID: 23128 RVA: 0x00389224 File Offset: 0x00388224
		public bool StandardHeightFlag
		{
			get
			{
				int a_ = 15;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
			}
			set
			{
				int a_ = 6;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("栻嘽┿扁⥃⍅㱇≉⍋⩍灏㵑♓癕㝗⩙㥛ⱝşᙡൣ॥٧䩩իᵭ偯ᱱ᭳ɵ塷፹ᅻ๽ﺉ뺏", a_));
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06005A59 RID: 23129 RVA: 0x0038927C File Offset: 0x0038827C
		// (set) Token: 0x06005A5A RID: 23130 RVA: 0x003892D4 File Offset: 0x003882D4
		public double StandardWidth
		{
			get
			{
				int a_ = 9;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("款⥀♂敄⩆ⱈ㽊╌⁎㕐獒㩔╖祘㑚ⵜ㩞፠ɢᅤ๦٨ժ䵬ٮɰ卲᭴ᡶ൸孺ᑼቾ歷붒", a_));
			}
			set
			{
				int a_ = 16;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ቅ⁇⽉汋⍍㕏♑㱓㥕㱗穙㍛ⱝ䁟ൡᑣͥᩧ୩ᡫݭὯᱱ味ή୷婹ቻᅽꊁﲑﲗ뒙", a_));
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06005A5B RID: 23131 RVA: 0x0038932C File Offset: 0x0038832C
		public ExcelSheetType Type
		{
			get
			{
				int a_ = 1;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("挶儸帺ᴼ刾⑀㝂ⵄ⡆ⵈ歊≌㵎煐㱒╔㉖⭘㩚⥜㙞๠ൢ䕤๦ᩨ䭪ͬnհ卲ᱴ᩶ॸ᝺᡼ቾꖊ", a_));
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06005A5C RID: 23132 RVA: 0x00389384 File Offset: 0x00388384
		public IXLSRange UsedRange
		{
			get
			{
				int a_ = 17;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ፆⅈ⹊浌≎㑐❒㵔㡖㵘筚㉜ⵞ䅠ౢᕤɦ᭨੪ᥬٮṰᵲ啴Ṷ੸孺፼ၾꎂ麗ﶒﶘ떚", a_));
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06005A5D RID: 23133 RVA: 0x003893DC File Offset: 0x003883DC
		// (set) Token: 0x06005A5E RID: 23134 RVA: 0x00389434 File Offset: 0x00388434
		public int Zoom
		{
			get
			{
				int a_ = 15;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
			}
			set
			{
				int a_ = 14;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("၃⹅ⵇ橉⅋⭍⑏㩑㭓㉕硗㕙⹛繝ཟቡţᑥ१ṩիŭṯ剱ᵳյ塷ᑹ፻੽ꁿﺏ뚗", a_));
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06005A5F RID: 23135 RVA: 0x0038948C File Offset: 0x0038848C
		// (set) Token: 0x06005A60 RID: 23136 RVA: 0x003894E4 File Offset: 0x003884E4
		public int VerticalSplit
		{
			get
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
				throw new Exception(RecordTableEnumerator.b("᱇≉⥋湍㵏㝑⁓㹕㝗㹙籛ㅝ቟䉡ୣᙥ൧ᡩ൫ᩭ᥯ᵱᩳ噵ᅷॹ屻ၽꒃ憎ﶏ望ﶗﺙ늛", a_));
			}
			set
			{
				int a_ = 18;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("᱇≉⥋湍㵏㝑⁓㹕㝗㹙籛ㅝ቟䉡ୣᙥ൧ᡩ൫ᩭ᥯ᵱᩳ噵ᅷॹ屻ၽꒃ憎ﶏ望ﶗﺙ늛", a_));
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06005A61 RID: 23137 RVA: 0x0038953C File Offset: 0x0038853C
		// (set) Token: 0x06005A62 RID: 23138 RVA: 0x00389594 File Offset: 0x00388594
		public int HorizontalSplit
		{
			get
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
				throw new Exception(RecordTableEnumerator.b("椼圾⑀捂⡄≆㵈⍊≌⭎煐㱒❔睖㙘⭚㡜ⵞ`ᝢ౤ࡦݨ䭪Ѭᱮ兰ᵲᩴͶ奸ቺၼཾﾊ뾐", a_));
			}
			set
			{
				int a_ = 19;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᵈ⍊⡌潎㱐㙒⅔㽖㙘㽚絜ぞ፠䍢੤ᝦ౨ᥪ౬᭮ᡰᱲ᭴坶ၸࡺ嵼ᅾꖄﮊﲐﮔﲘﾚ뎜", a_));
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06005A63 RID: 23139 RVA: 0x003895EC File Offset: 0x003885EC
		// (set) Token: 0x06005A64 RID: 23140 RVA: 0x00389644 File Offset: 0x00388644
		public int FirstVisibleRow
		{
			get
			{
				int a_ = 2;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("氷刹夻ḽⴿ❁ぃ⹅❇⹉汋⅍≏牑㭓♕㵗⡙㵛⩝य़ൡ੣䙥ŧᥩ䱫mὯٱ味ήᕷ੹ၻ᭽ꊋ", a_));
			}
			set
			{
				int a_ = 13;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᝂⵄ≆楈♊⡌㭎㥐㱒ㅔ睖㙘⥚絜ぞᅠ٢ᝤ٦ᵨɪɬŮ兰ᩲٴ坶᝸ᑺॼ彾릖", a_));
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06005A65 RID: 23141 RVA: 0x0038969C File Offset: 0x0038869C
		public IOleObjects OleObjects
		{
			get
			{
				int a_ = 10;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new NotImplementedException(RecordTableEnumerator.b("ᐿ⩁⅃晅╇⽉㡋♍㽏㙑瑓㥕⩗穙㍛⹝՟ၡգብŧթɫ乭᥯ű味ᡵ᝷๹屻᝽揄몓", a_));
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06005A66 RID: 23142 RVA: 0x003896F4 File Offset: 0x003886F4
		// (set) Token: 0x06005A67 RID: 23143 RVA: 0x0038974C File Offset: 0x0038874C
		public int FirstVisibleColumn
		{
			get
			{
				int a_ = 3;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("洸区堼Ἶⱀ♂ㅄ⽆♈⽊浌⁎⍐獒㩔❖㱘⥚㱜⭞ࡠౢ୤䝦hᡪ䵬ŮṰݲ啴Ṷᑸ୺ᅼ᩾ꎌ", a_));
			}
			set
			{
				int a_ = 3;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("洸区堼Ἶⱀ♂ㅄ⽆♈⽊浌⁎⍐獒㩔❖㱘⥚㱜⭞ࡠౢ୤䝦hᡪ䵬ŮṰݲ啴Ṷᑸ୺ᅼ᩾ꎌ", a_));
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06005A68 RID: 23144 RVA: 0x003897A4 File Offset: 0x003887A4
		// (set) Token: 0x06005A69 RID: 23145 RVA: 0x003897FC File Offset: 0x003887FC
		public int ActivePane
		{
			get
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
				throw new Exception(RecordTableEnumerator.b("᱇≉⥋湍㵏㝑⁓㹕㝗㹙籛ㅝ቟䉡ୣᙥ൧ᡩ൫ᩭ᥯ᵱᩳ噵ᅷॹ屻ၽꒃ憎ﶏ望ﶗﺙ늛", a_));
			}
			set
			{
				int a_ = 15;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06005A6A RID: 23146 RVA: 0x00389854 File Offset: 0x00388854
		// (set) Token: 0x06005A6B RID: 23147 RVA: 0x003898AC File Offset: 0x003888AC
		public bool IsDisplayZeros
		{
			get
			{
				int a_ = 4;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("渹吻嬽怿⽁⅃㉅⁇╉⡋湍㽏⁑瑓㥕⡗㽙⹛㽝ᑟୡୣࡥ䡧ͩὫ乭ṯᵱs噵ᅷ᝹౻ችﲇꂍ", a_));
			}
			set
			{
				int a_ = 11;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᕀ⭂⁄杆⑈⹊㥌❎㹐㝒畔㡖⭘筚㉜⽞ѠᅢѤ፦hѪͬ佮ᡰr啴᥶ᙸེ嵼ᙾﮎ뮔", a_));
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06005A6C RID: 23148 RVA: 0x00389904 File Offset: 0x00388904
		// (set) Token: 0x06005A6D RID: 23149 RVA: 0x0038995C File Offset: 0x0038895C
		public bool GridLinesVisible
		{
			get
			{
				int a_ = 5;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
			}
			set
			{
				int a_ = 9;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("款⥀♂敄⩆ⱈ㽊╌⁎㕐獒㩔╖祘㑚ⵜ㩞፠ɢᅤ๦٨ժ䵬ٮɰ卲᭴ᡶ൸孺ᑼቾ歷붒", a_));
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06005A6E RID: 23150 RVA: 0x003899B4 File Offset: 0x003889B4
		// (set) Token: 0x06005A6F RID: 23151 RVA: 0x00389A0C File Offset: 0x00388A0C
		public ExcelColors GridLineColor
		{
			get
			{
				int a_ = 2;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("氷刹夻ḽⴿ❁ぃ⹅❇⹉汋⅍≏牑㭓♕㵗⡙㵛⩝य़ൡ੣䙥ŧᥩ䱫mὯٱ味ήᕷ੹ၻ᭽ꊋ", a_));
			}
			set
			{
				int a_ = 11;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᕀ⭂⁄杆⑈⹊㥌❎㹐㝒畔㡖⭘筚㉜⽞ѠᅢѤ፦hѪͬ佮ᡰr啴᥶ᙸེ嵼ᙾﮎ뮔", a_));
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06005A70 RID: 23152 RVA: 0x00389A64 File Offset: 0x00388A64
		// (set) Token: 0x06005A71 RID: 23153 RVA: 0x00389ABC File Offset: 0x00388ABC
		public bool RowColumnHeadersVisible
		{
			get
			{
				int a_ = 6;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("栻嘽┿扁⥃⍅㱇≉⍋⩍灏㵑♓癕㝗⩙㥛ⱝşᙡൣ॥٧䩩իᵭ偯ᱱ᭳ɵ塷፹ᅻ๽ﺉ뺏", a_));
			}
			set
			{
				int a_ = 4;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("渹吻嬽怿⽁⅃㉅⁇╉⡋湍㽏⁑瑓㥕⡗㽙⹛㽝ᑟୡୣࡥ䡧ͩὫ乭ṯᵱs噵ᅷ᝹౻ችﲇꂍ", a_));
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06005A72 RID: 23154 RVA: 0x00389B14 File Offset: 0x00388B14
		public IVPageBreaks VPageBreaks
		{
			get
			{
				int a_ = 4;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("渹吻嬽怿⽁⅃㉅⁇╉⡋湍㽏⁑瑓㥕⡗㽙⹛㽝ᑟୡୣࡥ䡧ͩὫ乭ṯᵱs噵ᅷ᝹౻ችﲇꂍ", a_));
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06005A73 RID: 23155 RVA: 0x00389B6C File Offset: 0x00388B6C
		// (set) Token: 0x06005A74 RID: 23156 RVA: 0x00389BA8 File Offset: 0x00388BA8
		public int DefaultPrintRowHeight
		{
			get
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
				return 0;
			}
			set
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
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06005A75 RID: 23157 RVA: 0x00389BE4 File Offset: 0x00388BE4
		// (set) Token: 0x06005A76 RID: 23158 RVA: 0x00389C3C File Offset: 0x00388C3C
		public double DefaultRowHeight
		{
			get
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
				throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
			}
			set
			{
				int a_ = 17;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ፆⅈ⹊浌≎㑐❒㵔㡖㵘筚㉜ⵞ䅠ౢᕤɦ᭨੪ᥬٮṰᵲ啴Ṷ੸孺፼ၾꎂ麗ﶒﶘ떚", a_));
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06005A77 RID: 23159 RVA: 0x00389C94 File Offset: 0x00388C94
		// (set) Token: 0x06005A78 RID: 23160 RVA: 0x00389CD8 File Offset: 0x00388CD8
		public int FirstRow
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
				return this.ᜅ;
			}
			set
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
				this.ᜅ = value;
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06005A79 RID: 23161 RVA: 0x00389D1C File Offset: 0x00388D1C
		// (set) Token: 0x06005A7A RID: 23162 RVA: 0x00389D60 File Offset: 0x00388D60
		public int FirstColumn
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
				return this.ᜆ;
			}
			set
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
				this.ᜆ = value;
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06005A7B RID: 23163 RVA: 0x00389DA4 File Offset: 0x00388DA4
		// (set) Token: 0x06005A7C RID: 23164 RVA: 0x00389DE8 File Offset: 0x00388DE8
		public int LastRow
		{
			get
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
				return this.ᜇ;
			}
			set
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
				this.ᜇ = value;
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06005A7D RID: 23165 RVA: 0x00389E2C File Offset: 0x00388E2C
		// (set) Token: 0x06005A7E RID: 23166 RVA: 0x00389E70 File Offset: 0x00388E70
		public int LastColumn
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
				return this.ᜈ;
			}
			set
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
				this.ᜈ = value;
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06005A7F RID: 23167 RVA: 0x00389EB4 File Offset: 0x00388EB4
		public XlsCellRecordCollection CellRecords
		{
			[DebuggerStepThrough]
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
				return this.ᜄ;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06005A80 RID: 23168 RVA: 0x00389EF8 File Offset: 0x00388EF8
		public XlsWorkbook ParentWorkbook
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
				return this.ᜂ.Workbook;
			}
		}

		// Token: 0x06005A81 RID: 23169 RVA: 0x00389F40 File Offset: 0x00388F40
		public bool IsArrayFormula(long index)
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
			return false;
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06005A82 RID: 23170 RVA: 0x00389F7C File Offset: 0x00388F7C
		public ExcelVersion Version
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
				return ExcelVersion.Version2007;
			}
		}

		// Token: 0x06005A83 RID: 23171 RVA: 0x00389FB8 File Offset: 0x00388FB8
		public IInternalWorksheet GetClonedObject(Dictionary<string, string> hashNewNames, XlsWorkbook book)
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
			int index = this.ᜂ.Index;
			int index2 = this.Index;
			return book.ExternWorkbooks[index].Worksheets[index2];
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06005A84 RID: 23172 RVA: 0x0038A020 File Offset: 0x00389020
		// (set) Token: 0x06005A85 RID: 23173 RVA: 0x0038A078 File Offset: 0x00389078
		public double DefaultColumnWidth
		{
			get
			{
				int a_ = 0;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("戵倷弹᰻匽┿㙁ⱃ⥅ⱇ橉⍋㱍灏㵑⑓㍕⩗㭙⡛㝝ཟౡ䑣ཥ᭧䩩ɫŭѯ剱ᵳ᭵ࡷᙹ᥻፽ꒉ", a_));
			}
			set
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
				throw new Exception(RecordTableEnumerator.b("椼圾⑀捂⡄≆㵈⍊≌⭎煐㱒❔睖㙘⭚㡜ⵞ`ᝢ౤ࡦݨ䭪Ѭᱮ兰ᵲᩴͶ奸ቺၼཾﾊ뾐", a_));
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06005A86 RID: 23174 RVA: 0x0038A0D0 File Offset: 0x003890D0
		public XlsRange Range
		{
			get
			{
				int a_ = 6;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("栻嘽┿扁⥃⍅㱇≉⍋⩍灏㵑♓癕㝗⩙㥛ⱝşᙡൣ॥٧䩩իᵭ偯ᱱ᭳ɵ塷፹ᅻ๽ﺉ뺏", a_));
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06005A87 RID: 23175 RVA: 0x0038A128 File Offset: 0x00389128
		public IHPageBreaks HPageBreaks
		{
			get
			{
				int a_ = 19;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᵈ⍊⡌潎㱐㙒⅔㽖㙘㽚絜ぞ፠䍢੤ᝦ౨ᥪ౬᭮ᡰᱲ᭴坶ၸࡺ嵼ᅾꖄﮊﲐﮔﲘﾚ뎜", a_));
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06005A88 RID: 23176 RVA: 0x0038A180 File Offset: 0x00389180
		// (set) Token: 0x06005A89 RID: 23177 RVA: 0x0038A1D8 File Offset: 0x003891D8
		public bool IsStringsPreserved
		{
			get
			{
				int a_ = 6;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("栻嘽┿扁⥃⍅㱇≉⍋⩍灏㵑♓癕㝗⩙㥛ⱝşᙡൣ॥٧䩩իᵭ偯ᱱ᭳ɵ塷፹ᅻ๽ﺉ뺏", a_));
			}
			set
			{
				int a_ = 16;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ቅ⁇⽉汋⍍㕏♑㱓㥕㱗穙㍛ⱝ䁟ൡᑣͥᩧ୩ᡫݭὯᱱ味ή୷婹ቻᅽꊁﲑﲗ뒙", a_));
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06005A8A RID: 23178 RVA: 0x0038A230 File Offset: 0x00389230
		public IComments Comments
		{
			get
			{
				int a_ = 10;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᐿ⩁⅃晅╇⽉㡋♍㽏㙑瑓㥕⩗穙㍛⹝՟ၡգብŧթɫ乭᥯ű味ᡵ᝷๹屻᝽揄몓", a_));
			}
		}

		// Token: 0x17000E38 RID: 3640
		public IXLSRange this[int row, int column]
		{
			get
			{
				int a_ = 5;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
			}
		}

		// Token: 0x17000E39 RID: 3641
		public IXLSRange this[int row, int column, int lastRow, int lastColumn]
		{
			get
			{
				int a_ = 10;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᐿ⩁⅃晅╇⽉㡋♍㽏㙑瑓㥕⩗穙㍛⹝՟ၡգብŧթɫ乭᥯ű味ᡵ᝷๹屻᝽揄몓", a_));
			}
		}

		// Token: 0x17000E3A RID: 3642
		public IXLSRange this[string name]
		{
			get
			{
				int a_ = 11;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᕀ⭂⁄杆⑈⹊㥌❎㹐㝒畔㡖⭘筚㉜⽞ѠᅢѤ፦hѪͬ佮ᡰr啴᥶ᙸེ嵼ᙾﮎ뮔", a_));
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06005A8E RID: 23182 RVA: 0x0038A390 File Offset: 0x00389390
		public IHyperLinks HyperLinks
		{
			get
			{
				int a_ = 10;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᐿ⩁⅃晅╇⽉㡋♍㽏㙑瑓㥕⩗穙㍛⹝՟ၡգብŧթɫ乭᥯ű味ᡵ᝷๹屻᝽揄몓", a_));
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06005A8F RID: 23183 RVA: 0x0038A3E8 File Offset: 0x003893E8
		// (set) Token: 0x06005A90 RID: 23184 RVA: 0x0038A440 File Offset: 0x00389440
		public bool UseRangesCache
		{
			get
			{
				int a_ = 17;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ፆⅈ⹊浌≎㑐❒㵔㡖㵘筚㉜ⵞ䅠ౢᕤɦ᭨੪ᥬٮṰᵲ啴Ṷ੸孺፼ၾꎂ麗ﶒﶘ떚", a_));
			}
			set
			{
				int a_ = 6;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("栻嘽┿扁⥃⍅㱇≉⍋⩍灏㵑♓癕㝗⩙㥛ⱝşᙡൣ॥٧䩩իᵭ偯ᱱ᭳ɵ塷፹ᅻ๽ﺉ뺏", a_));
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06005A91 RID: 23185 RVA: 0x0038A498 File Offset: 0x00389498
		// (set) Token: 0x06005A92 RID: 23186 RVA: 0x0038A4F0 File Offset: 0x003894F0
		public int TopVisibleRow
		{
			get
			{
				int a_ = 5;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
			}
			set
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
				throw new Exception(RecordTableEnumerator.b("၃⹅ⵇ橉⅋⭍⑏㩑㭓㉕硗㕙⹛繝ཟቡţᑥ१ṩիŭṯ剱ᵳյ塷ᑹ፻੽ꁿﺏ뚗", a_));
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06005A93 RID: 23187 RVA: 0x0038A548 File Offset: 0x00389548
		// (set) Token: 0x06005A94 RID: 23188 RVA: 0x0038A5A0 File Offset: 0x003895A0
		public int LeftVisibleColumn
		{
			get
			{
				int a_ = 13;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᝂⵄ≆楈♊⡌㭎㥐㱒ㅔ睖㙘⥚絜ぞᅠ٢ᝤ٦ᵨɪɬŮ兰ᩲٴ坶᝸ᑺॼ彾릖", a_));
			}
			set
			{
				int a_ = 10;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᐿ⩁⅃晅╇⽉㡋♍㽏㙑瑓㥕⩗穙㍛⹝՟ၡգብŧթɫ乭᥯ű味ᡵ᝷๹屻᝽揄몓", a_));
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06005A95 RID: 23189 RVA: 0x0038A5F8 File Offset: 0x003895F8
		// (set) Token: 0x06005A96 RID: 23190 RVA: 0x0038A650 File Offset: 0x00389650
		public bool AllocatedRangeIncludesFormatting
		{
			get
			{
				int a_ = 5;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
			}
			set
			{
				int a_ = 0;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("戵倷弹᰻匽┿㙁ⱃ⥅ⱇ橉⍋㱍灏㵑⑓㍕⩗㭙⡛㝝ཟౡ䑣ཥ᭧䩩ɫŭѯ剱ᵳ᭵ࡷᙹ᥻፽ꒉ", a_));
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06005A97 RID: 23191 RVA: 0x0038A6A8 File Offset: 0x003896A8
		public PivotTablesCollection PivotTables
		{
			get
			{
				int a_ = 3;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("洸区堼Ἶⱀ♂ㅄ⽆♈⽊浌⁎⍐獒㩔❖㱘⥚㱜⭞ࡠౢ୤䝦hᡪ䵬ŮṰݲ啴Ṷᑸ୺ᅼ᩾ꎌ", a_));
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06005A98 RID: 23192 RVA: 0x0038A700 File Offset: 0x00389700
		public IListObjects ListObjects
		{
			get
			{
				int a_ = 10;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᐿ⩁⅃晅╇⽉㡋♍㽏㙑瑓㥕⩗穙㍛⹝՟ၡգብŧթɫ乭᥯ű味ᡵ᝷๹屻᝽揄몓", a_));
			}
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x0038A758 File Offset: 0x00389758
		public void CopyToClipboard()
		{
			int a_ = 5;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x0038A7B0 File Offset: 0x003897B0
		public void Clear()
		{
			int a_ = 3;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("洸区堼Ἶⱀ♂ㅄ⽆♈⽊浌⁎⍐獒㩔❖㱘⥚㱜⭞ࡠౢ୤䝦hᡪ䵬ŮṰݲ啴Ṷᑸ୺ᅼ᩾ꎌ", a_));
		}

		// Token: 0x06005A9B RID: 23195 RVA: 0x0038A808 File Offset: 0x00389808
		public void ClearData()
		{
			int a_ = 5;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
		}

		// Token: 0x06005A9C RID: 23196 RVA: 0x0038A860 File Offset: 0x00389860
		public bool CheckExistence(int iRow, int iColumn)
		{
			int a_ = 10;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᐿ⩁⅃晅╇⽉㡋♍㽏㙑瑓㥕⩗穙㍛⹝՟ၡգብŧթɫ乭᥯ű味ᡵ᝷๹屻᝽揄몓", a_));
		}

		// Token: 0x06005A9D RID: 23197 RVA: 0x0038A8B8 File Offset: 0x003898B8
		public void CreateNamedRanges(string namedRange, string referRange, bool vertical)
		{
			int a_ = 2;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("氷刹夻ḽⴿ❁ぃ⹅❇⹉汋⅍≏牑㭓♕㵗⡙㵛⩝य़ൡ੣䙥ŧᥩ䱫mὯٱ味ήᕷ੹ၻ᭽ꊋ", a_));
		}

		// Token: 0x06005A9E RID: 23198 RVA: 0x0038A910 File Offset: 0x00389910
		public bool IsColumnVisible(int columnIndex)
		{
			int a_ = 16;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ቅ⁇⽉汋⍍㕏♑㱓㥕㱗穙㍛ⱝ䁟ൡᑣͥᩧ୩ᡫݭὯᱱ味ή୷婹ቻᅽꊁﲑﲗ뒙", a_));
		}

		// Token: 0x06005A9F RID: 23199 RVA: 0x0038A968 File Offset: 0x00389968
		public bool IsRowVisible(int rowIndex)
		{
			int a_ = 2;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("氷刹夻ḽⴿ❁ぃ⹅❇⹉汋⅍≏牑㭓♕㵗⡙㵛⩝य़ൡ੣䙥ŧᥩ䱫mὯٱ味ήᕷ੹ၻ᭽ꊋ", a_));
		}

		// Token: 0x06005AA0 RID: 23200 RVA: 0x0038A9C0 File Offset: 0x003899C0
		internal void ᜀ(int A_0, bool A_1)
		{
			int a_ = 9;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("款⥀♂敄⩆ⱈ㽊╌⁎㕐獒㩔╖祘㑚ⵜ㩞፠ɢᅤ๦٨ժ䵬ٮɰ卲᭴ᡶ൸孺ᑼቾ歷붒", a_));
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x0038AA18 File Offset: 0x00389A18
		internal void ᜀ(IXLSRange A_0, bool A_1)
		{
			int a_ = 4;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("渹吻嬽怿⽁⅃㉅⁇╉⡋湍㽏⁑瑓㥕⡗㽙⹛㽝ᑟୡୣࡥ䡧ͩὫ乭ṯᵱs噵ᅷ᝹౻ችﲇꂍ", a_));
		}

		// Token: 0x06005AA2 RID: 23202 RVA: 0x0038AA70 File Offset: 0x00389A70
		internal void ᜀ(RangesCollection A_0, bool A_1)
		{
			int a_ = 18;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("᱇≉⥋湍㵏㝑⁓㹕㝗㹙籛ㅝ቟䉡ୣᙥ൧ᡩ൫ᩭ᥯ᵱᩳ噵ᅷॹ屻ၽꒃ憎ﶏ望ﶗﺙ늛", a_));
		}

		// Token: 0x06005AA3 RID: 23203 RVA: 0x0038AAC8 File Offset: 0x00389AC8
		internal void ᜀ(IXLSRange[] A_0, bool A_1)
		{
			int a_ = 1;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("挶儸帺ᴼ刾⑀㝂ⵄ⡆ⵈ歊≌㵎煐㱒╔㉖⭘㩚⥜㙞๠ൢ䕤๦ᩨ䭪ͬnհ卲ᱴ᩶ॸ᝺᡼ቾꖊ", a_));
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x0038AB20 File Offset: 0x00389B20
		public void DeleteRow(int index)
		{
			int a_ = 13;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᝂⵄ≆楈♊⡌㭎㥐㱒ㅔ睖㙘⥚絜ぞᅠ٢ᝤ٦ᵨɪɬŮ兰ᩲٴ坶᝸ᑺॼ彾릖", a_));
		}

		// Token: 0x06005AA5 RID: 23205 RVA: 0x0038AB78 File Offset: 0x00389B78
		public void DeleteColumn(int index)
		{
			int a_ = 10;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᐿ⩁⅃晅╇⽉㡋♍㽏㙑瑓㥕⩗穙㍛⹝՟ၡգብŧթɫ乭᥯ű味ᡵ᝷๹屻᝽揄몓", a_));
		}

		// Token: 0x06005AA6 RID: 23206 RVA: 0x0038ABD0 File Offset: 0x00389BD0
		public int InsertArray(object[] arrObject, int firstRow, int firstColumn, bool isVertical)
		{
			int a_ = 10;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᐿ⩁⅃晅╇⽉㡋♍㽏㙑瑓㥕⩗穙㍛⹝՟ၡգብŧթɫ乭᥯ű味ᡵ᝷๹屻᝽揄몓", a_));
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x0038AC28 File Offset: 0x00389C28
		public int InsertArray(string[] arrString, int firstRow, int firstColumn, bool isVertical)
		{
			int a_ = 19;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᵈ⍊⡌潎㱐㙒⅔㽖㙘㽚絜ぞ፠䍢੤ᝦ౨ᥪ౬᭮ᡰᱲ᭴坶ၸࡺ嵼ᅾꖄﮊﲐﮔﲘﾚ뎜", a_));
		}

		// Token: 0x06005AA8 RID: 23208 RVA: 0x0038AC80 File Offset: 0x00389C80
		public int InsertArray(int[] arrInt, int firstRow, int firstColumn, bool isVertical)
		{
			int a_ = 17;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ፆⅈ⹊浌≎㑐❒㵔㡖㵘筚㉜ⵞ䅠ౢᕤɦ᭨੪ᥬٮṰᵲ啴Ṷ੸孺፼ၾꎂ麗ﶒﶘ떚", a_));
		}

		// Token: 0x06005AA9 RID: 23209 RVA: 0x0038ACD8 File Offset: 0x00389CD8
		public int InsertArray(double[] arrDouble, int firstRow, int firstColumn, bool isVertical)
		{
			int a_ = 5;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
		}

		// Token: 0x06005AAA RID: 23210 RVA: 0x0038AD30 File Offset: 0x00389D30
		public int InsertArray(DateTime[] arrDateTime, int firstRow, int firstColumn, bool isVertical)
		{
			int a_ = 2;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("氷刹夻ḽⴿ❁ぃ⹅❇⹉汋⅍≏牑㭓♕㵗⡙㵛⩝य़ൡ੣䙥ŧᥩ䱫mὯٱ味ήᕷ੹ၻ᭽ꊋ", a_));
		}

		// Token: 0x06005AAB RID: 23211 RVA: 0x0038AD88 File Offset: 0x00389D88
		public int InsertArray(object[,] arrObject, int firstRow, int firstColumn)
		{
			int a_ = 1;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("挶儸帺ᴼ刾⑀㝂ⵄ⡆ⵈ歊≌㵎煐㱒╔㉖⭘㩚⥜㙞๠ൢ䕤๦ᩨ䭪ͬnհ卲ᱴ᩶ॸ᝺᡼ቾꖊ", a_));
		}

		// Token: 0x06005AAC RID: 23212 RVA: 0x0038ADE0 File Offset: 0x00389DE0
		public int InsertDataColumn(DataColumn dataColumn, bool isFieldNameShown, int firstRow, int firstColumn)
		{
			int a_ = 4;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("渹吻嬽怿⽁⅃㉅⁇╉⡋湍㽏⁑瑓㥕⡗㽙⹛㽝ᑟୡୣࡥ䡧ͩὫ乭ṯᵱs噵ᅷ᝹౻ችﲇꂍ", a_));
		}

		// Token: 0x06005AAD RID: 23213 RVA: 0x0038AE38 File Offset: 0x00389E38
		public int InsertDataTable(DataTable dataTable, bool isFieldNameShown, int firstRow, int firstColumn)
		{
			int a_ = 11;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᕀ⭂⁄杆⑈⹊㥌❎㹐㝒畔㡖⭘筚㉜⽞ѠᅢѤ፦hѪͬ佮ᡰr啴᥶ᙸེ嵼ᙾﮎ뮔", a_));
		}

		// Token: 0x06005AAE RID: 23214 RVA: 0x0038AE90 File Offset: 0x00389E90
		public int InsertDataTable(DataTable dataTable, bool isFieldNameShown, int firstRow, int firstColumn, bool preserveTypes)
		{
			int a_ = 3;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("洸区堼Ἶⱀ♂ㅄ⽆♈⽊浌⁎⍐獒㩔❖㱘⥚㱜⭞ࡠౢ୤䝦hᡪ䵬ŮṰݲ啴Ṷᑸ୺ᅼ᩾ꎌ", a_));
		}

		// Token: 0x06005AAF RID: 23215 RVA: 0x0038AEE8 File Offset: 0x00389EE8
		public int InsertDataTable(DataTable dataTable, bool isFieldNameShown, int firstRow, int firstColumn, int maxRows, int maxColumns)
		{
			int a_ = 6;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("栻嘽┿扁⥃⍅㱇≉⍋⩍灏㵑♓癕㝗⩙㥛ⱝşᙡൣ॥٧䩩իᵭ偯ᱱ᭳ɵ塷፹ᅻ๽ﺉ뺏", a_));
		}

		// Token: 0x06005AB0 RID: 23216 RVA: 0x0038AF40 File Offset: 0x00389F40
		public int InsertDataTable(DataTable dataTable, bool isFieldNameShown, int firstRow, int firstColumn, int maxRows, int maxColumns, bool preserveTypes)
		{
			int a_ = 17;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ፆⅈ⹊浌≎㑐❒㵔㡖㵘筚㉜ⵞ䅠ౢᕤɦ᭨੪ᥬٮṰᵲ啴Ṷ੸孺፼ၾꎂ麗ﶒﶘ떚", a_));
		}

		// Token: 0x06005AB1 RID: 23217 RVA: 0x0038AF98 File Offset: 0x00389F98
		public int InsertDataView(DataView dataView, bool isFieldNameShown, int firstRow, int firstColumn)
		{
			int a_ = 2;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("氷刹夻ḽⴿ❁ぃ⹅❇⹉汋⅍≏牑㭓♕㵗⡙㵛⩝य़ൡ੣䙥ŧᥩ䱫mὯٱ味ήᕷ੹ၻ᭽ꊋ", a_));
		}

		// Token: 0x06005AB2 RID: 23218 RVA: 0x0038AFF0 File Offset: 0x00389FF0
		public int InsertDataView(DataView dataView, bool isFieldNameShown, int firstRow, int firstColumn, bool bPreserveTypes)
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
			throw new Exception(RecordTableEnumerator.b("၃⹅ⵇ橉⅋⭍⑏㩑㭓㉕硗㕙⹛繝ཟቡţᑥ१ṩիŭṯ剱ᵳյ塷ᑹ፻੽ꁿﺏ뚗", a_));
		}

		// Token: 0x06005AB3 RID: 23219 RVA: 0x0038B048 File Offset: 0x0038A048
		public int InsertDataView(DataView dataView, bool isFieldNameShown, int firstRow, int firstColumn, int maxRows, int maxColumns)
		{
			int a_ = 1;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("挶儸帺ᴼ刾⑀㝂ⵄ⡆ⵈ歊≌㵎煐㱒╔㉖⭘㩚⥜㙞๠ൢ䕤๦ᩨ䭪ͬnհ卲ᱴ᩶ॸ᝺᡼ቾꖊ", a_));
		}

		// Token: 0x06005AB4 RID: 23220 RVA: 0x0038B0A0 File Offset: 0x0038A0A0
		public int InsertDataView(DataView dataView, bool isFieldNameShown, int firstRow, int firstColumn, int maxRows, int maxColumns, bool bPreserveTypes)
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("戵倷弹᰻匽┿㙁ⱃ⥅ⱇ橉⍋㱍灏㵑⑓㍕⩗㭙⡛㝝ཟౡ䑣ཥ᭧䩩ɫŭѯ剱ᵳ᭵ࡷᙹ᥻፽ꒉ", a_));
		}

		// Token: 0x06005AB5 RID: 23221 RVA: 0x0038B0F8 File Offset: 0x0038A0F8
		public void SetColumnWidthInPixels(int iColumnIndex, int value)
		{
			int a_ = 12;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᙁⱃ⍅桇❉⥋㩍㡏㵑こ癕㝗⡙籛ㅝၟݡᙣݥᱧͩͫm偯᭱ݳ噵ᙷᕹࡻ幽뢕", a_));
		}

		// Token: 0x06005AB6 RID: 23222 RVA: 0x0038B150 File Offset: 0x0038A150
		public void SetRowHeightPixels(int iRowIndex, double value)
		{
			int a_ = 9;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("款⥀♂敄⩆ⱈ㽊╌⁎㕐獒㩔╖祘㑚ⵜ㩞፠ɢᅤ๦٨ժ䵬ٮɰ卲᭴ᡶ൸孺ᑼቾ歷붒", a_));
		}

		// Token: 0x06005AB7 RID: 23223 RVA: 0x0038B1A8 File Offset: 0x0038A1A8
		public int GetColumnWidthPixels(int iColumnIndex)
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("樽⠿❁摃⭅ⵇ㹉⑋⅍㑏牑㭓⑕硗㕙ⱛ㭝቟͡ၣཥݧѩ䱫ݭͯ剱ᩳ᥵౷婹ᕻ፽벑", a_));
		}

		// Token: 0x06005AB8 RID: 23224 RVA: 0x0038B200 File Offset: 0x0038A200
		public int GetRowHeightPixels(int iRowIndex)
		{
			int a_ = 3;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("洸区堼Ἶⱀ♂ㅄ⽆♈⽊浌⁎⍐獒㩔❖㱘⥚㱜⭞ࡠౢ୤䝦hᡪ䵬ŮṰݲ啴Ṷᑸ୺ᅼ᩾ꎌ", a_));
		}

		// Token: 0x06005AB9 RID: 23225 RVA: 0x0038B258 File Offset: 0x0038A258
		public void RemovePanes()
		{
			int a_ = 12;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᙁⱃ⍅桇❉⥋㩍㡏㵑こ癕㝗⡙籛ㅝၟݡᙣݥᱧͩͫm偯᭱ݳ噵ᙷᕹࡻ幽뢕", a_));
		}

		// Token: 0x06005ABA RID: 23226 RVA: 0x0038B2B0 File Offset: 0x0038A2B0
		public void Protect(string password)
		{
			int a_ = 1;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("挶儸帺ᴼ刾⑀㝂ⵄ⡆ⵈ歊≌㵎煐㱒╔㉖⭘㩚⥜㙞๠ൢ䕤๦ᩨ䭪ͬnհ卲ᱴ᩶ॸ᝺᡼ቾꖊ", a_));
		}

		// Token: 0x06005ABB RID: 23227 RVA: 0x0038B308 File Offset: 0x0038A308
		public void Unprotect(string password)
		{
			int a_ = 15;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
		}

		// Token: 0x06005ABC RID: 23228 RVA: 0x0038B360 File Offset: 0x0038A360
		public void AutoFitRow(int rowIndex)
		{
			int a_ = 3;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("洸区堼Ἶⱀ♂ㅄ⽆♈⽊浌⁎⍐獒㩔❖㱘⥚㱜⭞ࡠౢ୤䝦hᡪ䵬ŮṰݲ啴Ṷᑸ୺ᅼ᩾ꎌ", a_));
		}

		// Token: 0x06005ABD RID: 23229 RVA: 0x0038B3B8 File Offset: 0x0038A3B8
		public void AutoFitColumn(int columnIndex)
		{
			int a_ = 1;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("挶儸帺ᴼ刾⑀㝂ⵄ⡆ⵈ歊≌㵎煐㱒╔㉖⭘㩚⥜㙞๠ൢ䕤๦ᩨ䭪ͬnհ卲ᱴ᩶ॸ᝺᡼ቾꖊ", a_));
		}

		// Token: 0x06005ABE RID: 23230 RVA: 0x0038B410 File Offset: 0x0038A410
		public void Replace(string oldValue, string newValue)
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("樽⠿❁摃⭅ⵇ㹉⑋⅍㑏牑㭓⑕硗㕙ⱛ㭝቟͡ၣཥݧѩ䱫ݭͯ剱ᩳ᥵౷婹ᕻ፽벑", a_));
		}

		// Token: 0x06005ABF RID: 23231 RVA: 0x0038B468 File Offset: 0x0038A468
		public void Replace(string oldValue, double newValue)
		{
			int a_ = 3;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("洸区堼Ἶⱀ♂ㅄ⽆♈⽊浌⁎⍐獒㩔❖㱘⥚㱜⭞ࡠౢ୤䝦hᡪ䵬ŮṰݲ啴Ṷᑸ୺ᅼ᩾ꎌ", a_));
		}

		// Token: 0x06005AC0 RID: 23232 RVA: 0x0038B4C0 File Offset: 0x0038A4C0
		public void Replace(string oldValue, DateTime newValue)
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("戵倷弹᰻匽┿㙁ⱃ⥅ⱇ橉⍋㱍灏㵑⑓㍕⩗㭙⡛㝝ཟౡ䑣ཥ᭧䩩ɫŭѯ剱ᵳ᭵ࡷᙹ᥻፽ꒉ", a_));
		}

		// Token: 0x06005AC1 RID: 23233 RVA: 0x0038B518 File Offset: 0x0038A518
		public void Replace(string oldValue, string[] newValues, bool isVertical)
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
			throw new Exception(RecordTableEnumerator.b("椼圾⑀捂⡄≆㵈⍊≌⭎煐㱒❔睖㙘⭚㡜ⵞ`ᝢ౤ࡦݨ䭪Ѭᱮ兰ᵲᩴͶ奸ቺၼཾﾊ뾐", a_));
		}

		// Token: 0x06005AC2 RID: 23234 RVA: 0x0038B570 File Offset: 0x0038A570
		public void Replace(string oldValue, int[] newValues, bool isVertical)
		{
			int a_ = 16;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ቅ⁇⽉汋⍍㕏♑㱓㥕㱗穙㍛ⱝ䁟ൡᑣͥᩧ୩ᡫݭὯᱱ味ή୷婹ቻᅽꊁﲑﲗ뒙", a_));
		}

		// Token: 0x06005AC3 RID: 23235 RVA: 0x0038B5C8 File Offset: 0x0038A5C8
		public void Replace(string oldValue, double[] newValues, bool isVertical)
		{
			int a_ = 6;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("栻嘽┿扁⥃⍅㱇≉⍋⩍灏㵑♓癕㝗⩙㥛ⱝşᙡൣ॥٧䩩իᵭ偯ᱱ᭳ɵ塷፹ᅻ๽ﺉ뺏", a_));
		}

		// Token: 0x06005AC4 RID: 23236 RVA: 0x0038B620 File Offset: 0x0038A620
		public void Replace(string oldValue, DataTable newValues, bool isFieldNamesShown)
		{
			int a_ = 13;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᝂⵄ≆楈♊⡌㭎㥐㱒ㅔ睖㙘⥚絜ぞᅠ٢ᝤ٦ᵨɪɬŮ兰ᩲٴ坶᝸ᑺॼ彾릖", a_));
		}

		// Token: 0x06005AC5 RID: 23237 RVA: 0x0038B678 File Offset: 0x0038A678
		public void Replace(string oldValue, DataColumn newValues, bool isFieldNamesShown)
		{
			int a_ = 9;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("款⥀♂敄⩆ⱈ㽊╌⁎㕐獒㩔╖祘㑚ⵜ㩞፠ɢᅤ๦٨ժ䵬ٮɰ卲᭴ᡶ൸孺ᑼቾ歷붒", a_));
		}

		// Token: 0x06005AC6 RID: 23238 RVA: 0x0038B6D0 File Offset: 0x0038A6D0
		public void Remove()
		{
			int a_ = 4;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("渹吻嬽怿⽁⅃㉅⁇╉⡋湍㽏⁑瑓㥕⡗㽙⹛㽝ᑟୡୣࡥ䡧ͩὫ乭ṯᵱs噵ᅷ᝹౻ችﲇꂍ", a_));
		}

		// Token: 0x06005AC7 RID: 23239 RVA: 0x0038B728 File Offset: 0x0038A728
		public void MoveWorksheet(int iNewIndex)
		{
			int a_ = 16;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ቅ⁇⽉汋⍍㕏♑㱓㥕㱗穙㍛ⱝ䁟ൡᑣͥᩧ୩ᡫݭὯᱱ味ή୷婹ቻᅽꊁﲑﲗ뒙", a_));
		}

		// Token: 0x06005AC8 RID: 23240 RVA: 0x0038B780 File Offset: 0x0038A780
		public int ColumnWidthToPixels(double widthInChars)
		{
			int a_ = 14;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("၃⹅ⵇ橉⅋⭍⑏㩑㭓㉕硗㕙⹛繝ཟቡţᑥ१ṩիŭṯ剱ᵳյ塷ᑹ፻੽ꁿﺏ뚗", a_));
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x0038B7D8 File Offset: 0x0038A7D8
		public double PixelsToColumnWidth(double pixels)
		{
			int a_ = 15;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
		}

		// Token: 0x06005ACA RID: 23242 RVA: 0x0038B830 File Offset: 0x0038A830
		public void SaveToFile(string fileName, string separator)
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
			throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
		}

		// Token: 0x06005ACB RID: 23243 RVA: 0x0038B888 File Offset: 0x0038A888
		public void SaveToStream(Stream stream, string separator)
		{
			int a_ = 5;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
		}

		// Token: 0x06005ACC RID: 23244 RVA: 0x0038B8E0 File Offset: 0x0038A8E0
		public void SetDefaultColumnStyle(int iColumnIndex, IStyle defaultStyle)
		{
			int a_ = 2;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("氷刹夻ḽⴿ❁ぃ⹅❇⹉汋⅍≏牑㭓♕㵗⡙㵛⩝य़ൡ੣䙥ŧᥩ䱫mὯٱ味ήᕷ੹ၻ᭽ꊋ", a_));
		}

		// Token: 0x06005ACD RID: 23245 RVA: 0x0038B938 File Offset: 0x0038A938
		public void SetDefaultColumnStyle(int iStartColumnIndex, int iEndColumnIndex, IStyle defaultStyle)
		{
			int a_ = 18;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("᱇≉⥋湍㵏㝑⁓㹕㝗㹙籛ㅝ቟䉡ୣᙥ൧ᡩ൫ᩭ᥯ᵱᩳ噵ᅷॹ屻ၽꒃ憎ﶏ望ﶗﺙ늛", a_));
		}

		// Token: 0x06005ACE RID: 23246 RVA: 0x0038B990 File Offset: 0x0038A990
		public void SetDefaultRowStyle(int rowIndex, IStyle defaultStyle)
		{
			int a_ = 19;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᵈ⍊⡌潎㱐㙒⅔㽖㙘㽚絜ぞ፠䍢੤ᝦ౨ᥪ౬᭮ᡰᱲ᭴坶ၸࡺ嵼ᅾꖄﮊﲐﮔﲘﾚ뎜", a_));
		}

		// Token: 0x06005ACF RID: 23247 RVA: 0x0038B9E8 File Offset: 0x0038A9E8
		public void SetDefaultRowStyle(int iStartRowIndex, int iEndRowIndex, IStyle defaultStyle)
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
			throw new Exception(RecordTableEnumerator.b("᱇≉⥋湍㵏㝑⁓㹕㝗㹙籛ㅝ቟䉡ୣᙥ൧ᡩ൫ᩭ᥯ᵱᩳ噵ᅷॹ屻ၽꒃ憎ﶏ望ﶗﺙ늛", a_));
		}

		// Token: 0x06005AD0 RID: 23248 RVA: 0x0038BA40 File Offset: 0x0038AA40
		public IStyle GetDefaultColumnStyle(int iColumnIndex)
		{
			int a_ = 2;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("氷刹夻ḽⴿ❁ぃ⹅❇⹉汋⅍≏牑㭓♕㵗⡙㵛⩝य़ൡ੣䙥ŧᥩ䱫mὯٱ味ήᕷ੹ၻ᭽ꊋ", a_));
		}

		// Token: 0x06005AD1 RID: 23249 RVA: 0x0038BA98 File Offset: 0x0038AA98
		public IStyle GetDefaultRowStyle(int rowIndex)
		{
			int a_ = 16;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ቅ⁇⽉汋⍍㕏♑㱓㥕㱗穙㍛ⱝ䁟ൡᑣͥᩧ୩ᡫݭὯᱱ味ή୷婹ቻᅽꊁﲑﲗ뒙", a_));
		}

		// Token: 0x06005AD2 RID: 23250 RVA: 0x0038BAF0 File Offset: 0x0038AAF0
		public void SetValue(int iRow, int iColumn, string value)
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("樽⠿❁摃⭅ⵇ㹉⑋⅍㑏牑㭓⑕硗㕙ⱛ㭝቟͡ၣཥݧѩ䱫ݭͯ剱ᩳ᥵౷婹ᕻ፽벑", a_));
		}

		// Token: 0x06005AD3 RID: 23251 RVA: 0x0038BB48 File Offset: 0x0038AB48
		public void SetNumber(int iRow, int iColumn, double value)
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
			throw new Exception(RecordTableEnumerator.b("椼圾⑀捂⡄≆㵈⍊≌⭎煐㱒❔睖㙘⭚㡜ⵞ`ᝢ౤ࡦݨ䭪Ѭᱮ兰ᵲᩴͶ奸ቺၼཾﾊ뾐", a_));
		}

		// Token: 0x06005AD4 RID: 23252 RVA: 0x0038BBA0 File Offset: 0x0038ABA0
		public void SetBoolean(int iRow, int iColumn, bool value)
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("樽⠿❁摃⭅ⵇ㹉⑋⅍㑏牑㭓⑕硗㕙ⱛ㭝቟͡ၣཥݧѩ䱫ݭͯ剱ᩳ᥵౷婹ᕻ፽벑", a_));
		}

		// Token: 0x06005AD5 RID: 23253 RVA: 0x0038BBF8 File Offset: 0x0038ABF8
		public void SetText(int iRow, int iColumn, string value)
		{
			int a_ = 14;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("၃⹅ⵇ橉⅋⭍⑏㩑㭓㉕硗㕙⹛繝ཟቡţᑥ१ṩիŭṯ剱ᵳյ塷ᑹ፻੽ꁿﺏ뚗", a_));
		}

		// Token: 0x06005AD6 RID: 23254 RVA: 0x0038BC50 File Offset: 0x0038AC50
		public void SetFormula(int iRow, int iColumn, string value)
		{
			int a_ = 1;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("挶儸帺ᴼ刾⑀㝂ⵄ⡆ⵈ歊≌㵎煐㱒╔㉖⭘㩚⥜㙞๠ൢ䕤๦ᩨ䭪ͬnհ卲ᱴ᩶ॸ᝺᡼ቾꖊ", a_));
		}

		// Token: 0x06005AD7 RID: 23255 RVA: 0x0038BCA8 File Offset: 0x0038ACA8
		public void SetError(int iRow, int iColumn, string value)
		{
			int a_ = 16;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ቅ⁇⽉汋⍍㕏♑㱓㥕㱗穙㍛ⱝ䁟ൡᑣͥᩧ୩ᡫݭὯᱱ味ή୷婹ቻᅽꊁﲑﲗ뒙", a_));
		}

		// Token: 0x06005AD8 RID: 23256 RVA: 0x0038BD00 File Offset: 0x0038AD00
		public void SetBlank(int iRow, int iColumn)
		{
			int a_ = 15;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x0038BD58 File Offset: 0x0038AD58
		public void SetFormulaNumberValue(int iRow, int iColumn, double value)
		{
			int a_ = 10;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᐿ⩁⅃晅╇⽉㡋♍㽏㙑瑓㥕⩗穙㍛⹝՟ၡգብŧթɫ乭᥯ű味ᡵ᝷๹屻᝽揄몓", a_));
		}

		// Token: 0x06005ADA RID: 23258 RVA: 0x0038BDB0 File Offset: 0x0038ADB0
		public void SetFormulaErrorValue(int iRow, int iColumn, string value)
		{
			int a_ = 18;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("᱇≉⥋湍㵏㝑⁓㹕㝗㹙籛ㅝ቟䉡ୣᙥ൧ᡩ൫ᩭ᥯ᵱᩳ噵ᅷॹ屻ၽꒃ憎ﶏ望ﶗﺙ늛", a_));
		}

		// Token: 0x06005ADB RID: 23259 RVA: 0x0038BE08 File Offset: 0x0038AE08
		public void SetFormulaBoolValue(int iRow, int iColumn, bool value)
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("戵倷弹᰻匽┿㙁ⱃ⥅ⱇ橉⍋㱍灏㵑⑓㍕⩗㭙⡛㝝ཟౡ䑣ཥ᭧䩩ɫŭѯ剱ᵳ᭵ࡷᙹ᥻፽ꒉ", a_));
		}

		// Token: 0x06005ADC RID: 23260 RVA: 0x0038BE60 File Offset: 0x0038AE60
		public void SetFormulaStringValue(int iRow, int iColumn, string value)
		{
			int a_ = 8;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("樽⠿❁摃⭅ⵇ㹉⑋⅍㑏牑㭓⑕硗㕙ⱛ㭝቟͡ၣཥݧѩ䱫ݭͯ剱ᩳ᥵౷婹ᕻ፽벑", a_));
		}

		// Token: 0x06005ADD RID: 23261 RVA: 0x0038BEB8 File Offset: 0x0038AEB8
		public string GetText(int row, int column)
		{
			int a_ = 15;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
		}

		// Token: 0x06005ADE RID: 23262 RVA: 0x0038BF10 File Offset: 0x0038AF10
		public double GetNumber(int row, int column)
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
			throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
		}

		// Token: 0x06005ADF RID: 23263 RVA: 0x0038BF68 File Offset: 0x0038AF68
		public string GetFormula(int row, int column, bool bR1C1)
		{
			int a_ = 16;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ቅ⁇⽉汋⍍㕏♑㱓㥕㱗穙㍛ⱝ䁟ൡᑣͥᩧ୩ᡫݭὯᱱ味ή୷婹ቻᅽꊁﲑﲗ뒙", a_));
		}

		// Token: 0x06005AE0 RID: 23264 RVA: 0x0038BFC0 File Offset: 0x0038AFC0
		public string GetError(int row, int column)
		{
			int a_ = 17;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ፆⅈ⹊浌≎㑐❒㵔㡖㵘筚㉜ⵞ䅠ౢᕤɦ᭨੪ᥬٮṰᵲ啴Ṷ੸孺፼ၾꎂ麗ﶒﶘ떚", a_));
		}

		// Token: 0x06005AE1 RID: 23265 RVA: 0x0038C018 File Offset: 0x0038B018
		public bool GetBoolean(int row, int column)
		{
			int a_ = 19;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᵈ⍊⡌潎㱐㙒⅔㽖㙘㽚絜ぞ፠䍢੤ᝦ౨ᥪ౬᭮ᡰᱲ᭴坶ၸࡺ嵼ᅾꖄﮊﲐﮔﲘﾚ뎜", a_));
		}

		// Token: 0x06005AE2 RID: 23266 RVA: 0x0038C070 File Offset: 0x0038B070
		public bool GetFormulaBoolValue(int row, int column)
		{
			int a_ = 1;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("挶儸帺ᴼ刾⑀㝂ⵄ⡆ⵈ歊≌㵎煐㱒╔㉖⭘㩚⥜㙞๠ൢ䕤๦ᩨ䭪ͬnհ卲ᱴ᩶ॸ᝺᡼ቾꖊ", a_));
		}

		// Token: 0x06005AE3 RID: 23267 RVA: 0x0038C0C8 File Offset: 0x0038B0C8
		public string GetFormulaErrorValue(int row, int column)
		{
			int a_ = 16;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ቅ⁇⽉汋⍍㕏♑㱓㥕㱗穙㍛ⱝ䁟ൡᑣͥᩧ୩ᡫݭὯᱱ味ή୷婹ቻᅽꊁﲑﲗ뒙", a_));
		}

		// Token: 0x06005AE4 RID: 23268 RVA: 0x0038C120 File Offset: 0x0038B120
		public double GetFormulaNumberValue(int row, int column)
		{
			int a_ = 15;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x0038C178 File Offset: 0x0038B178
		public string GetFormulaStringValue(int row, int column)
		{
			int a_ = 17;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ፆⅈ⹊浌≎㑐❒㵔㡖㵘筚㉜ⵞ䅠ౢᕤɦ᭨੪ᥬٮṰᵲ啴Ṷ੸孺፼ၾꎂ麗ﶒﶘ떚", a_));
		}

		// Token: 0x06005AE6 RID: 23270 RVA: 0x0038C1D0 File Offset: 0x0038B1D0
		public Image SaveToImage(int firstRow, int firstColumn, int lastRow, int lastColumn)
		{
			int a_ = 13;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᝂⵄ≆楈♊⡌㭎㥐㱒ㅔ睖㙘⥚絜ぞᅠ٢ᝤ٦ᵨɪɬŮ兰ᩲٴ坶᝸ᑺॼ彾릖", a_));
		}

		// Token: 0x06005AE7 RID: 23271 RVA: 0x0038C228 File Offset: 0x0038B228
		public Image SaveToImage(Stream stream, int firstRow, int firstColumn, int lastRow, int lastColumn, ImageType imageType)
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("樽⠿❁摃⭅ⵇ㹉⑋⅍㑏牑㭓⑕硗㕙ⱛ㭝቟͡ၣཥݧѩ䱫ݭͯ剱ᩳ᥵౷婹ᕻ፽벑", a_));
		}

		// Token: 0x06005AE8 RID: 23272 RVA: 0x0038C280 File Offset: 0x0038B280
		public Image SaveToImage(Stream outputStream, int firstRow, int firstColumn, int lastRow, int lastColumn, EmfType emfType)
		{
			int a_ = 18;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("᱇≉⥋湍㵏㝑⁓㹕㝗㹙籛ㅝ቟䉡ୣᙥ൧ᡩ൫ᩭ᥯ᵱᩳ噵ᅷॹ屻ၽꒃ憎ﶏ望ﶗﺙ늛", a_));
		}

		// Token: 0x06005AE9 RID: 23273 RVA: 0x0038C2D8 File Offset: 0x0038B2D8
		public Image SaveToImage(Stream outputStream, int firstRow, int firstColumn, int lastRow, int lastColumn, ImageType imageType, EmfType emfType)
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
			throw new Exception(RecordTableEnumerator.b("᱇≉⥋湍㵏㝑⁓㹕㝗㹙籛ㅝ቟䉡ୣᙥ൧ᡩ൫ᩭ᥯ᵱᩳ噵ᅷॹ屻ၽꒃ憎ﶏ望ﶗﺙ늛", a_));
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06005AEA RID: 23274 RVA: 0x0038C330 File Offset: 0x0038B330
		// (set) Token: 0x06005AEB RID: 23275 RVA: 0x0038C388 File Offset: 0x0038B388
		public ExcelColors TabKnownColor
		{
			get
			{
				int a_ = 7;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("椼圾⑀捂⡄≆㵈⍊≌⭎煐㱒❔睖㙘⭚㡜ⵞ`ᝢ౤ࡦݨ䭪Ѭᱮ兰ᵲᩴͶ奸ቺၼཾﾊ뾐", a_));
			}
			set
			{
				int a_ = 5;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06005AEC RID: 23276 RVA: 0x0038C3E0 File Offset: 0x0038B3E0
		// (set) Token: 0x06005AED RID: 23277 RVA: 0x0038C438 File Offset: 0x0038B438
		public Color TabColor
		{
			get
			{
				int a_ = 16;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ቅ⁇⽉汋⍍㕏♑㱓㥕㱗穙㍛ⱝ䁟ൡᑣͥᩧ୩ᡫݭὯᱱ味ή୷婹ቻᅽꊁﲑﲗ뒙", a_));
			}
			set
			{
				int a_ = 4;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("渹吻嬽怿⽁⅃㉅⁇╉⡋湍㽏⁑瑓㥕⡗㽙⹛㽝ᑟୡୣࡥ䡧ͩὫ乭ṯᵱs噵ᅷ᝹౻ችﲇꂍ", a_));
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06005AEE RID: 23278 RVA: 0x0038C490 File Offset: 0x0038B490
		public IPictures Pictures
		{
			get
			{
				int a_ = 5;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06005AEF RID: 23279 RVA: 0x0038C4E8 File Offset: 0x0038B4E8
		IWorkbook ITabSheet.Workbook
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
				return this.Workbook.Workbook;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06005AF0 RID: 23280 RVA: 0x0038C530 File Offset: 0x0038B530
		// (set) Token: 0x06005AF1 RID: 23281 RVA: 0x0038C588 File Offset: 0x0038B588
		public bool IsRightToLeft
		{
			get
			{
				int a_ = 13;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᝂⵄ≆楈♊⡌㭎㥐㱒ㅔ睖㙘⥚絜ぞᅠ٢ᝤ٦ᵨɪɬŮ兰ᩲٴ坶᝸ᑺॼ彾릖", a_));
			}
			set
			{
				int a_ = 2;
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("氷刹夻ḽⴿ❁ぃ⹅❇⹉汋⅍≏牑㭓♕㵗⡙㵛⩝य़ൡ੣䙥ŧᥩ䱫mὯٱ味ήᕷ੹ၻ᭽ꊋ", a_));
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06005AF2 RID: 23282 RVA: 0x0038C5E0 File Offset: 0x0038B5E0
		public bool IsSelected
		{
			get
			{
				int a_ = 13;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᝂⵄ≆楈♊⡌㭎㥐㱒ㅔ睖㙘⥚絜ぞᅠ٢ᝤ٦ᵨɪɬŮ兰ᩲٴ坶᝸ᑺॼ彾릖", a_));
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06005AF3 RID: 23283 RVA: 0x0038C638 File Offset: 0x0038B638
		public int TabIndex
		{
			get
			{
				int a_ = 6;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("栻嘽┿扁⥃⍅㱇≉⍋⩍灏㵑♓癕㝗⩙㥛ⱝşᙡൣ॥٧䩩իᵭ偯ᱱ᭳ɵ塷፹ᅻ๽ﺉ뺏", a_));
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06005AF4 RID: 23284 RVA: 0x0038C690 File Offset: 0x0038B690
		// (set) Token: 0x06005AF5 RID: 23285 RVA: 0x0038C6D4 File Offset: 0x0038B6D4
		public string Name
		{
			[CompilerGenerated]
			get
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
				return this.ᜏ;
			}
			[CompilerGenerated]
			set
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
				this.ᜏ = value;
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06005AF6 RID: 23286 RVA: 0x0038C718 File Offset: 0x0038B718
		// (set) Token: 0x06005AF7 RID: 23287 RVA: 0x0038C770 File Offset: 0x0038B770
		public WorksheetVisibility Visibility
		{
			get
			{
				int a_ = 15;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᅄ⽆ⱈ歊⁌⩎═㭒㩔㍖祘㑚⽜罞๠።dᕦࡨὪѬnὰ卲ᱴѶ奸ᕺቼ୾ꆀﾐ래", a_));
			}
			set
			{
				int a_ = 5;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06005AF8 RID: 23288 RVA: 0x0038C7C8 File Offset: 0x0038B7C8
		public ITextBoxes TextBoxes
		{
			get
			{
				int a_ = 5;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("漺唼娾慀⹂⁄㍆ⅈ⑊⥌潎㹐⅒畔㡖⥘㹚⽜㹞ᕠ੢੤०䥨ɪṬ佮ὰᱲŴ坶ၸᙺർ፾ﶈꆎ", a_));
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06005AF9 RID: 23289 RVA: 0x0038C820 File Offset: 0x0038B820
		public ICheckBoxes CheckBoxes
		{
			get
			{
				int a_ = 19;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᵈ⍊⡌潎㱐㙒⅔㽖㙘㽚絜ぞ፠䍢੤ᝦ౨ᥪ౬᭮ᡰᱲ᭴坶ၸࡺ嵼ᅾꖄﮊﲐﮔﲘﾚ뎜", a_));
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06005AFA RID: 23290 RVA: 0x0038C878 File Offset: 0x0038B878
		public IRadioButtons RadioButtons
		{
			get
			{
				int a_ = 11;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᕀ⭂⁄杆⑈⹊㥌❎㹐㝒畔㡖⭘筚㉜⽞ѠᅢѤ፦hѪͬ佮ᡰr啴᥶ᙸེ嵼ᙾﮎ뮔", a_));
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06005AFB RID: 23291 RVA: 0x0038C8D0 File Offset: 0x0038B8D0
		public IComboBoxes ComboBoxes
		{
			get
			{
				int a_ = 2;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("氷刹夻ḽⴿ❁ぃ⹅❇⹉汋⅍≏牑㭓♕㵗⡙㵛⩝य़ൡ੣䙥ŧᥩ䱫mὯٱ味ήᕷ੹ၻ᭽ꊋ", a_));
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06005AFC RID: 23292 RVA: 0x0038C928 File Offset: 0x0038B928
		public bool IsPasswordProtected
		{
			get
			{
				int a_ = 10;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				throw new Exception(RecordTableEnumerator.b("ᐿ⩁⅃晅╇⽉㡋♍㽏㙑瑓㥕⩗穙㍛⹝՟ၡգብŧթɫ乭᥯ű味ᡵ᝷๹屻᝽揄몓", a_));
			}
		}

		// Token: 0x06005AFD RID: 23293 RVA: 0x0038C980 File Offset: 0x0038B980
		public void Activate()
		{
			int a_ = 3;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new Exception(RecordTableEnumerator.b("洸区堼Ἶⱀ♂ㅄ⽆♈⽊浌⁎⍐獒㩔❖㱘⥚㱜⭞ࡠౢ୤䝦hᡪ䵬ŮṰݲ啴Ṷᑸ୺ᅼ᩾ꎌ", a_));
		}

		// Token: 0x06005AFE RID: 23294 RVA: 0x0038C9D8 File Offset: 0x0038B9D8
		public void Select()
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
			throw new Exception(RecordTableEnumerator.b("椼圾⑀捂⡄≆㵈⍊≌⭎煐㱒❔睖㙘⭚㡜ⵞ`ᝢ౤ࡦݨ䭪Ѭᱮ兰ᵲᩴͶ奸ቺၼཾﾊ뾐", a_));
		}

		// Token: 0x06005AFF RID: 23295 RVA: 0x0038CA30 File Offset: 0x0038BA30
		public void Unselect()
		{
			int a_ = 18;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("᱇≉⥋湍㵏㝑⁓㹕㝗㹙籛ㅝ቟䉡ୣᙥ൧ᡩ൫ᩭ᥯ᵱᩳ噵ᅷॹ屻ၽꒃ憎ﶏ望ﶗﺙ늛", a_));
		}

		// Token: 0x06005B00 RID: 23296 RVA: 0x0038CA88 File Offset: 0x0038BA88
		public void Protect(string password, SheetProtectionType options)
		{
			int a_ = 19;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᵈ⍊⡌潎㱐㙒⅔㽖㙘㽚絜ぞ፠䍢੤ᝦ౨ᥪ౬᭮ᡰᱲ᭴坶ၸࡺ嵼ᅾꖄﮊﲐﮔﲘﾚ뎜", a_));
		}

		// Token: 0x04002C60 RID: 11360
		private spr᥊ ᜀ;

		// Token: 0x04002C61 RID: 11361
		private List<BiffRecordRaw> ᜁ;

		// Token: 0x04002C62 RID: 11362
		private XlsExternWorkbook ᜂ;

		// Token: 0x04002C63 RID: 11363
		private string ᜃ;

		// Token: 0x04002C64 RID: 11364
		private XlsCellRecordCollection ᜄ;

		// Token: 0x04002C65 RID: 11365
		private int ᜅ;

		// Token: 0x04002C66 RID: 11366
		private float \u2460\u00A6\u0089\u0098;

		// Token: 0x04002C67 RID: 11367
		private int ᜆ;

		// Token: 0x04002C68 RID: 11368
		private int ᜇ;

		// Token: 0x04002C69 RID: 11369
		private int ᜈ;

		// Token: 0x04002C6A RID: 11370
		private string[] \u2609\u009F\u00A0\u0096;

		// Token: 0x04002C6B RID: 11371
		private Dictionary<string, string> ᜉ;

		// Token: 0x04002C6C RID: 11372
		internal int ᜊ;

		// Token: 0x04002C6D RID: 11373
		private float[] \u2609\u009A\u00AE\u008A;

		// Token: 0x04002C6E RID: 11374
		private FormulaEngine ᜋ;

		// Token: 0x04002C6F RID: 11375
		private XlsRange.CellValueChangedEventHandler ᜌ;

		// Token: 0x04002C70 RID: 11376
		private long \u25D9\u007F\u009F\u00AF;

		// Token: 0x04002C71 RID: 11377
		private XlsWorksheet.ErrorFunctionEventHandler \u170D;

		// Token: 0x04002C72 RID: 11378
		private ValueChangedEventHandler ᜎ;

		// Token: 0x04002C73 RID: 11379
		[CompilerGenerated]
		private string ᜏ;
	}
}
