using System;
using System.Collections.Generic;
using System.Threading;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls
{
	// Token: 0x02000051 RID: 81
	public class RadioButton : TextBoxShapeBase, IRadioButton
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060007DB RID: 2011 RVA: 0x00053048 File Offset: 0x00052048
		// (remove) Token: 0x060007DC RID: 2012 RVA: 0x000530DC File Offset: 0x000520DC
		internal event XlsEventHandler CheckStateChanged
		{
			add
			{
				for (;;)
				{
					XlsEventHandler xlsEventHandler = this.ᜉ;
					if (true)
					{
					}
					int num = 0;
					for (;;)
					{
						XlsEventHandler xlsEventHandler2;
						switch (num)
						{
						case 0:
							goto IL_2D;
						case 1:
							if (xlsEventHandler == xlsEventHandler2)
							{
								num = 2;
								continue;
							}
							goto IL_2D;
						case 2:
							goto IL_5E;
						}
						break;
						IL_2D:
						xlsEventHandler2 = xlsEventHandler;
						XlsEventHandler value2 = (XlsEventHandler)Delegate.Combine(xlsEventHandler2, value);
						xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.ᜉ, value2, xlsEventHandler2);
						num = 1;
					}
				}
				IL_5E:
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
			}
			remove
			{
				for (;;)
				{
					XlsEventHandler xlsEventHandler = this.ᜉ;
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						XlsEventHandler xlsEventHandler2;
						switch (num)
						{
						case 0:
							if (xlsEventHandler == xlsEventHandler2)
							{
								num = 1;
								continue;
							}
							goto IL_2D;
						case 1:
							goto IL_5E;
						case 2:
							goto IL_2D;
						}
						break;
						IL_2D:
						xlsEventHandler2 = xlsEventHandler;
						XlsEventHandler value2 = (XlsEventHandler)Delegate.Remove(xlsEventHandler2, value);
						xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.ᜉ, value2, xlsEventHandler2);
						num = 0;
					}
				}
				IL_5E:
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
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00053170 File Offset: 0x00052170
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x000531B4 File Offset: 0x000521B4
		public bool InvokeEvent
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

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x000531F8 File Offset: 0x000521F8
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x0005323C File Offset: 0x0005223C
		internal int Index
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜈ = value;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00053280 File Offset: 0x00052280
		internal int NextButtonId
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
				return (int)this.ᜆ;
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000532C4 File Offset: 0x000522C4
		internal RadioButton(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			base.ShapeType = ExcelShapeType.TextBox;
			base.Fill.ForeColor = spr\u1D39.ᜁ;
			base.Line.ForeColor = spr\u1D39.ᜅ;
			base.Line.BackColor = spr\u1D39.ᜅ;
			base.FillColor = spr\u1D39.ᜂ;
			base.Line.HasPattern = false;
			base.HasFill = false;
			base.AlternativeText = null;
			base.VmlShape = true;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00053340 File Offset: 0x00052340
		internal RadioButton(spr\u1DF5 A_0, object A_1, sprὙ A_2, ExcelParseOptions A_3) : base(A_0, A_1, A_2, A_3)
		{
			base.ShapeType = ExcelShapeType.TextBox;
			base.VmlShape = true;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00053368 File Offset: 0x00052368
		internal RadioButton(spr\u1DF5 A_0, object A_1, sprὙ A_2, ExcelParseOptions A_3, int A_4) : base(A_0, A_1, A_2, A_3)
		{
			base.ShapeType = ExcelShapeType.TextBox;
			base.VmlShape = true;
			this.ᜈ = A_4;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00053398 File Offset: 0x00052398
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
			base.ShapeType = ExcelShapeType.TextBox;
			this.m_bUpdateLineFill = true;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000533E4 File Offset: 0x000523E4
		protected override void OnPrepareForSerialization()
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
			this.ᜏ = (sprἼ)spr\u231F.ᜀ(MsoRecords.msofbtSp);
			this.ᜏ.ᜉ(2);
			this.ᜏ.ᜈ(201);
			this.ᜏ.ᜆ(true);
			this.ᜏ.ᜇ(true);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00053468 File Offset: 0x00052468
		internal override void SerializeShape(spr\u21EB spgrContainer)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				int num = 0;
				sprὙ sprὙ;
				spr᪙ spr᪙;
				for (;;)
				{
					spr\u2003 spr_u;
					spr᧗ spr᧗;
					spr\u2223 spr_u2;
					sprទ a_2;
					sprἨ sprἨ;
					sprᯋ sprᯋ;
					switch (num)
					{
					case 1:
						num = 19;
						continue;
					case 2:
						goto IL_17E;
					case 3:
						spr᧗ = (spr᧗)this.ᜀ(spr_u.ᜃ(), TObjSubRecordType.ftCblsFmla);
						num = 2;
						continue;
					case 4:
						goto IL_AD;
					case 5:
						spr_u.ᜀ(spr᧗);
						num = 14;
						continue;
					case 6:
						sprὙ.ᜀ(base.ChildAnchor);
						num = 27;
						continue;
					case 7:
					{
						spr_u = (spr\u2003)spr\u175E.ᜀ(TBIFFRecord.OBJ);
						spr_u2 = new spr\u2223();
						spr_u2.ᜀ(TObjType.otOptionBtn);
						spr_u2.ᜀ(true);
						spr_u2.ᜂ(base.IsTextLocked);
						spr_u2.ᜃ(false);
						a_2 = new sprទ();
						sprᯄ a_3 = new sprᯄ();
						sprἨ = new sprἨ();
						spr᧗ = new spr᧗();
						sprᾑ a_4 = new sprᾑ();
						sprᯋ = new sprᯋ();
						spr_u.ᜀ(spr_u2);
						spr_u.ᜀ(a_3);
						spr_u.ᜀ(a_4);
						num = 21;
						continue;
					}
					case 8:
						if (this.LinkedCell != null)
						{
							num = 3;
							continue;
						}
						goto IL_17E;
					case 9:
						goto IL_17E;
					case 10:
						num = 24;
						continue;
					case 11:
					{
						spr\u23E7 spr_u23E;
						sprὙ.ᜀ(spr_u23E);
						num = 15;
						continue;
					}
					case 12:
						if (base.ChildAnchor != null)
						{
							num = 6;
							continue;
						}
						sprὙ.ᜀ(base.ClientAnchor);
						num = 22;
						continue;
					case 13:
					{
						spr\u23E7 spr_u23E;
						if (spr_u23E.ᜀ().Length > 0)
						{
							num = 11;
							continue;
						}
						goto IL_45A;
					}
					case 14:
						goto IL_106;
					case 15:
						goto IL_45A;
					case 16:
					{
						spr_u2.ᜁ((base.OldObjId > 0U) ? ((ushort)base.OldObjId) : ((ushort)base.ParentWorkbook.CurrentObjectId));
						sprὙ.ᜀ(this.ᜏ);
						spr\u23E7 spr_u23E = this.SerializeOptions(sprὙ);
						num = 13;
						continue;
					}
					case 17:
					{
						if (spr_u == null)
						{
							num = 7;
							continue;
						}
						spr_u2 = (spr_u.ᜃ()[0] as spr\u2223);
						sprᯄ a_3 = (sprᯄ)this.ᜀ(spr_u.ᜃ(), TObjSubRecordType.ftCbls);
						sprἨ = (sprἨ)this.ᜀ(spr_u.ᜃ(), TObjSubRecordType.ftCblsData);
						sprᾑ a_4 = (sprᾑ)this.ᜀ(spr_u.ᜃ(), TObjSubRecordType.ftRbo);
						sprᯋ = (sprᯋ)this.ᜀ(spr_u.ᜃ(), TObjSubRecordType.ftRboData);
						num = 8;
						continue;
					}
					case 18:
						if (spr᧗ != null)
						{
							num = 1;
							continue;
						}
						goto IL_AD;
					case 19:
						if (spr᧗.ᜀ() == null)
						{
							num = 10;
							continue;
						}
						goto IL_AD;
					case 20:
						if (this.LinkedCell != null)
						{
							goto IL_419;
						}
						goto IL_106;
					case 21:
						if (this.IsFirstButton)
						{
							num = 25;
							continue;
						}
						goto IL_106;
					case 22:
						goto IL_349;
					case 23:
						spr᧗.ᜀ((this.LinkedCell as spr\u1A8B).ᜀ());
						num = 4;
						continue;
					case 24:
						if (this.LinkedCell != null)
						{
							num = 23;
							continue;
						}
						goto IL_AD;
					case 25:
						num = 20;
						continue;
					case 26:
						goto IL_AB;
					case 27:
						goto IL_179;
					}
					if (spgrContainer == null)
					{
						num = 26;
						continue;
					}
					sprὙ = (sprὙ)spr\u231F.ᜀ(MsoRecords.msofbtSpContainer);
					spr᪙ = (spr᪙)spr\u231F.ᜀ(MsoRecords.msofbtClientData);
					spr_u2 = null;
					spr_u = base.Obj;
					spr᧗ = null;
					num = 17;
					continue;
					IL_AD:
					num = 16;
					continue;
					IL_106:
					spr_u.ᜀ(sprἨ);
					spr_u.ᜀ(sprᯋ);
					spr_u.ᜀ(a_2);
					num = 9;
					continue;
					IL_17E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_419:
						if (true)
						{
						}
						num = 5;
						continue;
					default:
						if (false)
						{
						}
						spr᪙.ᜀ(spr_u);
						sprἨ.ᜀ(this.ᜂ);
						sprἨ.ᜀ(this.Display3DShading);
						sprᯋ.ᜀ(this.IsFirstButton);
						sprᯋ.ᜀ(this.ᜆ);
						num = 18;
						continue;
					}
					IL_45A:
					num = 12;
				}
				IL_AB:
				throw new ArgumentNullException(RecordTableEnumerator.b("㩈㭊⩌㵎ቐ㱒㭔⍖㡘㉚㍜㩞፠", a_));
				IL_179:
				IL_349:
				sprὙ.ᜀ(spr᪙);
				base.IsTextLocked = base.IsTextLocked;
				spr\u2016 a_5 = base.ᜁ(sprὙ);
				sprὙ.ᜀ(a_5);
				spgrContainer.ᜀ(sprὙ);
				return;
			}
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00053968 File Offset: 0x00052968
		private spr\u25AD ᜀ(List<spr\u25AD> A_0, TObjSubRecordType A_1)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				spr\u25AD result;
				for (;;)
				{
					IL_57:
					result = null;
					int num = 0;
					int count = A_0.Count;
					int num2 = 1;
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
							switch (num2)
							{
							case 0:
							{
								spr\u25AD spr_u25AD;
								if (spr_u25AD.ᜏ() == A_1)
								{
									num2 = 5;
									continue;
								}
								num++;
								num2 = 4;
								continue;
							}
							case 1:
								goto IL_6B;
							case 2:
							{
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								spr\u25AD spr_u25AD = A_0[num];
								num2 = 0;
								continue;
							}
							case 3:
								return result;
							case 4:
								goto IL_BE;
							case 5:
							{
								spr\u25AD spr_u25AD;
								result = spr_u25AD;
								num2 = 6;
								continue;
							}
							case 6:
								return result;
							}
							goto IL_57;
						}
						IL_BE:
						num2 = 2;
						continue;
						IL_6B:
						goto IL_BE;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00053A54 File Offset: 0x00052A54
		[CLSCompliant(false)]
		internal override spr\u23E7 SerializeOptions(spr\u1D3B parent)
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
			spr\u23E7 spr_u23E = base.SerializeOptions(parent);
			spr\u23E7.ᜀ ᜀ = new spr\u23E7.ᜀ();
			ᜀ = new spr\u23E7.ᜀ();
			ᜀ.ᜀ(MsoOptions.SizeTextToFitShape);
			ᜀ.ᜀ(1703944);
			spr_u23E.ᜁ(ᜀ);
			return spr_u23E;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00053AC4 File Offset: 0x00052AC4
		[CLSCompliant(false)]
		internal override spr\u23E7 CreateDefaultOptions()
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
			spr\u23E7 spr_u23E = base.CreateDefaultOptions();
			spr_u23E.ᜉ(3);
			spr_u23E.ᜈ(2);
			return spr_u23E;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00053B18 File Offset: 0x00052B18
		internal override void ParseClientData(spr᪙ clientData, ExcelParseOptions options)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					base.ParseClientData(clientData, options);
					spr\u2003 spr_u = clientData.ᜁ();
					List<spr\u25AD> list = spr_u.ᜃ();
					int num = 0;
					int count = list.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num2 = 6;
							continue;
						case 1:
						{
							if (num >= count)
							{
								num2 = 4;
								continue;
							}
							spr\u25AD spr_u25AD = list[num];
							TObjSubRecordType tobjSubRecordType = spr_u25AD.ᜏ();
							num2 = 5;
							continue;
						}
						case 2:
							goto IL_101;
						case 3:
							goto IL_16E;
						case 4:
							return;
						case 5:
						{
							TObjSubRecordType tobjSubRecordType;
							switch (tobjSubRecordType)
							{
							case TObjSubRecordType.ftRboData:
							{
								spr\u25AD spr_u25AD;
								this.ᜆ = ((sprᯋ)spr_u25AD).ᜂ();
								this.ᜄ = ((sprᯋ)spr_u25AD).ᜀ();
								if (true)
								{
								}
								num2 = 3;
								continue;
							}
							case TObjSubRecordType.ftCblsData:
							{
								spr\u25AD spr_u25AD;
								this.ᜂ = ((sprἨ)spr_u25AD).ᜀ();
								this.ᜅ = ((sprἨ)spr_u25AD).ᜂ();
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_172;
								default:
									if (false)
									{
									}
									num2 = 7;
									continue;
								}
								break;
							}
							case TObjSubRecordType.ftLbsData:
								goto IL_16E;
							case TObjSubRecordType.ftCblsFmla:
							{
								spr\u25AD spr_u25AD;
								this.ᜃ = (((spr᧗)spr_u25AD).ᜀ()[0] as sprỜ).ᜀ(base.ParentWorkbook, base.Worksheet as IWorksheet);
								num2 = 8;
								continue;
							}
							default:
								num2 = 0;
								continue;
							}
							break;
						}
						case 6:
							goto IL_16E;
						case 7:
							goto IL_16E;
						case 8:
							goto IL_16E;
						case 9:
							goto IL_101;
						}
						break;
						IL_101:
						num2 = 1;
						continue;
						IL_172:
						num2 = 9;
						continue;
						IL_16E:
						num++;
						goto IL_172;
					}
				}
				return;
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00053CF4 File Offset: 0x00052CF4
		public override IShape Clone(object parent, Dictionary<string, string> hashNewNames, Dictionary<int, int> dicFontIndexes, bool addToCollections)
		{
			RadioButton radioButton;
			for (;;)
			{
				radioButton = (RadioButton)base.Clone(parent, hashNewNames, dicFontIndexes, addToCollections);
				XlsWorksheetBase worksheet = radioButton.Worksheet;
				XlsWorkbook parentWorkbook = worksheet.ParentWorkbook;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (addToCollections)
						{
							goto IL_B9;
						}
						return radioButton;
					case 1:
						if (this.ᜃ != null)
						{
							num = 3;
							continue;
						}
						goto IL_AA;
					case 2:
						return radioButton;
					case 3:
						radioButton.ᜃ = (this.ᜃ as ICombinedRange).Clone(worksheet, hashNewNames, parentWorkbook);
						num = 5;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B9;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							(radioButton.Worksheet.RadioButtons as RadioButtonCollection).AddRadioButton(radioButton);
							num = 2;
							continue;
						}
						break;
					case 5:
						goto IL_AA;
					}
					break;
					IL_AA:
					num = 0;
					continue;
					IL_B9:
					num = 4;
				}
			}
			return radioButton;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x00053DF0 File Offset: 0x00052DF0
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x00053E48 File Offset: 0x00052E48
		public new CommentHAlignType HAlignment
		{
			get
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
				throw new NotSupportedException(RecordTableEnumerator.b("缽ⰿ⭁⍃⡅╇⽉≋㩍", a_));
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
				throw new NotSupportedException(RecordTableEnumerator.b("े♉╋⥍㹏㽑ㅓ㡕ⱗ", a_));
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x00053EA0 File Offset: 0x00052EA0
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x00053EF8 File Offset: 0x00052EF8
		public new CommentVAlignType VAlignment
		{
			get
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
				throw new NotSupportedException(RecordTableEnumerator.b("瘶唸刺娼儾ⱀ♂⭄㍆", a_));
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
				throw new NotSupportedException(RecordTableEnumerator.b("Ճ⩅ⅇⵉ≋⍍㕏㱑⁓", a_));
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00053F50 File Offset: 0x00052F50
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x00053FA8 File Offset: 0x00052FA8
		public new TextRotationType TextRotation
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
				throw new NotSupportedException(RecordTableEnumerator.b("ᅂ⩄㍆⡈㽊⑌⁎㽐", a_));
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
				throw new NotSupportedException(RecordTableEnumerator.b("ᑅ❇㹉ⵋ㩍㥏㵑㩓", a_));
			}
		}

		// Token: 0x1700025C RID: 604
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x00054000 File Offset: 0x00053000
		public new RichTextString RichText
		{
			set
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
				throw new NotSupportedException(RecordTableEnumerator.b("ሿ⭁❃⹅桇ṉ⥋㙍⑏", a_));
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00054058 File Offset: 0x00053058
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x0005409C File Offset: 0x0005309C
		public CheckState CheckState
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
				return this.ᜂ;
			}
			set
			{
				int a_ = 5;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
					{
						CheckState checkState;
						this.ᜉ(this, new XlsEventArgs(checkState, value, this.Name));
						num = 6;
						continue;
					}
					case 2:
						if (this.ᜇ)
						{
							num = 1;
							continue;
						}
						return;
					case 3:
						if (this.ᜉ != null)
						{
							num = 5;
							continue;
						}
						return;
					case 4:
						goto IL_4C;
					case 5:
						num = 2;
						continue;
					case 6:
						return;
					case 7:
					{
						if (true)
						{
						}
						CheckState checkState;
						if (checkState == value)
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
							num = 8;
							continue;
						}
						break;
					}
					case 8:
						num = 3;
						continue;
					}
					if (value == CheckState.Mixed)
					{
						num = 4;
					}
					else
					{
						CheckState checkState = this.ᜂ;
						this.ᜂ = value;
						num = 7;
					}
				}
				IL_4C:
				throw new NotSupportedException(RecordTableEnumerator.b("瘺吼䜾⑀❂敄㑆㵈⩊㥌⩎煐㵒㩔⍖祘࡚⡜⽞ᅠౢᝤ፦౨ཪ", a_));
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x000541D0 File Offset: 0x000531D0
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x00054214 File Offset: 0x00053214
		public IXLSRange LinkedCell
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
				return this.ᜃ;
			}
			set
			{
				int a_ = 14;
				int num = 9;
				for (;;)
				{
					string text;
					IXLSRange ixlsrange;
					string a;
					string b;
					string text2;
					switch (num)
					{
					case 0:
						goto IL_BB;
					case 1:
						num = 6;
						continue;
					case 2:
						text = value.RangeGlobalAddress;
						goto IL_20F;
					case 3:
						goto IL_1F7;
					case 4:
						num = 11;
						continue;
					case 5:
						num = 10;
						continue;
					case 6:
						if (value.Row == value.LastRow)
						{
							num = 18;
							continue;
						}
						goto IL_80;
					case 7:
						if (ixlsrange == null)
						{
							num = 4;
							continue;
						}
						num = 13;
						continue;
					case 8:
						if (a != b)
						{
							num = 5;
							continue;
						}
						goto IL_23B;
					case 10:
						if (this.ᜇ)
						{
							num = 19;
							continue;
						}
						goto IL_23B;
					case 11:
						text2 = null;
						goto IL_114;
					case 12:
						if (this.ᜊ != null)
						{
							num = 14;
							continue;
						}
						goto IL_23B;
					case 13:
						text2 = ixlsrange.RangeGlobalAddress;
						goto IL_114;
					case 14:
						this.ᜊ(this, new XlsEventArgs(ixlsrange, value, this.Name));
						num = 3;
						continue;
					case 15:
						num = 16;
						continue;
					case 16:
						if (true)
						{
						}
						text = null;
						goto IL_20F;
					case 17:
						if (value.Column != value.LastColumn)
						{
							num = 0;
							continue;
						}
						goto IL_166;
					case 18:
						num = 17;
						continue;
					case 19:
						num = 12;
						continue;
					case 20:
						if (value == null)
						{
							num = 15;
							continue;
						}
						num = 2;
						continue;
					}
					if (value != null)
					{
						num = 1;
						continue;
					}
					goto IL_166;
					IL_115:
					num = 20;
					continue;
					IL_166:
					ixlsrange = this.ᜃ;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_115;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					IL_114:
					a = text2;
					goto IL_115;
					IL_20F:
					b = text;
					this.ᜃ = value;
					num = 8;
				}
				IL_80:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ࡃ⽅♇ⅉ⥋⩍ፏ㝑㡓㩕硗㝙⥛ⵝᑟ䉡٣ͥ䡧ᥩիmᝯṱᅳ噵᭷όၻች깿", a_));
				IL_BB:
				goto IL_80;
				IL_1F7:
				IL_23B:
				(this.ᜃ as spr\u1A8B).ᜀ();
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00054470 File Offset: 0x00053470
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x000544B4 File Offset: 0x000534B4
		public bool IsFirstButton
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
				return this.ᜄ;
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
				this.ᜄ = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x000544F8 File Offset: 0x000534F8
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0005453C File Offset: 0x0005353C
		public bool Display3DShading
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
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060007FC RID: 2044 RVA: 0x00054580 File Offset: 0x00053580
		// (remove) Token: 0x060007FD RID: 2045 RVA: 0x00054614 File Offset: 0x00053614
		internal event XlsEventHandler LinkedCellValueChanged
		{
			add
			{
				if (true)
				{
				}
				for (;;)
				{
					XlsEventHandler xlsEventHandler = this.ᜊ;
					int num = 1;
					for (;;)
					{
						XlsEventHandler xlsEventHandler2;
						switch (num)
						{
						case 0:
							return;
						case 1:
							goto IL_2D;
						case 2:
							if (xlsEventHandler == xlsEventHandler2)
							{
								goto IL_7C;
							}
							goto IL_2D;
						}
						break;
						IL_2D:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_7C:
							num = 0;
							continue;
						}
						if (false)
						{
						}
						xlsEventHandler2 = xlsEventHandler;
						XlsEventHandler value2 = (XlsEventHandler)Delegate.Combine(xlsEventHandler2, value);
						xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.ᜊ, value2, xlsEventHandler2);
						num = 2;
					}
				}
			}
			remove
			{
				for (;;)
				{
					XlsEventHandler xlsEventHandler = this.ᜊ;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							XlsEventHandler xlsEventHandler2;
							if (xlsEventHandler == xlsEventHandler2)
							{
								goto IL_7C;
							}
							goto IL_25;
						}
						case 1:
							goto IL_25;
						case 2:
							return;
						}
						break;
						IL_25:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_7C:
							num = 2;
							break;
						default:
						{
							if (false)
							{
							}
							if (true)
							{
							}
							XlsEventHandler xlsEventHandler2 = xlsEventHandler;
							XlsEventHandler value2 = (XlsEventHandler)Delegate.Remove(xlsEventHandler2, value);
							xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.ᜊ, value2, xlsEventHandler2);
							num = 0;
							break;
						}
						}
					}
				}
			}
		}

		// Token: 0x04000150 RID: 336
		internal new const int ᜀ = 201;

		// Token: 0x04000151 RID: 337
		private long[] \u2609\u00B0\u00A3\u00AF;

		// Token: 0x04000152 RID: 338
		private new const int ᜁ = 2;

		// Token: 0x04000153 RID: 339
		private new CheckState ᜂ;

		// Token: 0x04000154 RID: 340
		private float \u2593\u0090\u008D\u009E;

		// Token: 0x04000155 RID: 341
		private new IXLSRange ᜃ;

		// Token: 0x04000156 RID: 342
		private new bool ᜄ;

		// Token: 0x04000157 RID: 343
		private new bool ᜅ;

		// Token: 0x04000158 RID: 344
		private bool \u25D8\u0091\u0080\u0083;

		// Token: 0x04000159 RID: 345
		private new byte ᜆ;

		// Token: 0x0400015A RID: 346
		private new bool ᜇ;

		// Token: 0x0400015B RID: 347
		private int ᜈ;

		// Token: 0x0400015C RID: 348
		private XlsEventHandler ᜉ;

		// Token: 0x0400015D RID: 349
		private XlsEventHandler ᜊ;
	}
}
