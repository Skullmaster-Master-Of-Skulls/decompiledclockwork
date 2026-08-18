using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf
{
	// Token: 0x020003B8 RID: 952
	public class CFFFontSubset : CFFFont
	{
		// Token: 0x060020D1 RID: 8401 RVA: 0x000C45C8 File Offset: 0x000C35C8
		public CFFFontSubset(RandomAccessFileOrArray rf, Dictionary<int, int[]> GlyphsUsed) : base(rf)
		{
			this.GlyphsUsed = GlyphsUsed;
			this.glyphsInList = new List<int>(GlyphsUsed.Keys);
			for (int i = 0; i < this.fonts.Length; i++)
			{
				base.Seek(this.fonts[i].charstringsOffset);
				this.fonts[i].nglyphs = (int)base.GetCard16();
				base.Seek(this.stringIndexOffset);
				this.fonts[i].nstrings = (int)base.GetCard16() + CFFFont.standardStrings.Length;
				this.fonts[i].charstringsOffsets = base.GetIndex(this.fonts[i].charstringsOffset);
				if (this.fonts[i].fdselectOffset >= 0)
				{
					this.ReadFDSelect(i);
					this.BuildFDArrayUsed(i);
				}
				if (this.fonts[i].isCID)
				{
					this.ReadFDArray(i);
				}
				this.fonts[i].CharsetLength = this.CountCharset(this.fonts[i].charsetOffset, this.fonts[i].nglyphs);
			}
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000C4714 File Offset: 0x000C3714
		internal int CountCharset(int Offset, int NumofGlyphs)
		{
			int result = 0;
			base.Seek(Offset);
			switch (base.GetCard8())
			{
			case '\0':
				result = 1 + 2 * NumofGlyphs;
				break;
			case '\u0001':
				result = 1 + 3 * this.CountRange(NumofGlyphs, 1);
				break;
			case '\u0002':
				result = 1 + 4 * this.CountRange(NumofGlyphs, 2);
				break;
			}
			return result;
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x000C476C File Offset: 0x000C376C
		private int CountRange(int NumofGlyphs, int Type)
		{
			int num = 0;
			int num2;
			for (int i = 1; i < NumofGlyphs; i += num2 + 1)
			{
				num++;
				base.GetCard16();
				if (Type == 1)
				{
					num2 = (int)base.GetCard8();
				}
				else
				{
					num2 = (int)base.GetCard16();
				}
			}
			return num;
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000C47AC File Offset: 0x000C37AC
		protected void ReadFDSelect(int Font)
		{
			int nglyphs = this.fonts[Font].nglyphs;
			int[] array = new int[nglyphs];
			base.Seek(this.fonts[Font].fdselectOffset);
			this.fonts[Font].FDSelectFormat = (int)base.GetCard8();
			int fdselectFormat = this.fonts[Font].FDSelectFormat;
			if (fdselectFormat != 0)
			{
				if (fdselectFormat == 3)
				{
					int card = (int)base.GetCard16();
					int num = 0;
					int num2 = (int)base.GetCard16();
					for (int i = 0; i < card; i++)
					{
						int card2 = (int)base.GetCard8();
						int card3 = (int)base.GetCard16();
						int num3 = card3 - num2;
						for (int j = 0; j < num3; j++)
						{
							array[num] = card2;
							num++;
						}
						num2 = card3;
					}
					this.fonts[Font].FDSelectLength = 3 + card * 3 + 2;
				}
			}
			else
			{
				for (int k = 0; k < nglyphs; k++)
				{
					array[k] = (int)base.GetCard8();
				}
				this.fonts[Font].FDSelectLength = this.fonts[Font].nglyphs + 1;
			}
			this.fonts[Font].FDSelect = array;
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000C48C4 File Offset: 0x000C38C4
		protected void BuildFDArrayUsed(int Font)
		{
			int[] fdselect = this.fonts[Font].FDSelect;
			for (int i = 0; i < this.glyphsInList.Count; i++)
			{
				int num = this.glyphsInList[i];
				int key = fdselect[num];
				this.FDArrayUsed[key] = null;
			}
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000C4914 File Offset: 0x000C3914
		protected void ReadFDArray(int Font)
		{
			base.Seek(this.fonts[Font].fdarrayOffset);
			this.fonts[Font].FDArrayCount = (int)base.GetCard16();
			this.fonts[Font].FDArrayOffsize = (int)base.GetCard8();
			if (this.fonts[Font].FDArrayOffsize < 4)
			{
				this.fonts[Font].FDArrayOffsize++;
			}
			this.fonts[Font].FDArrayOffsets = base.GetIndex(this.fonts[Font].fdarrayOffset);
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000C49A0 File Offset: 0x000C39A0
		public byte[] Process(string fontName)
		{
			byte[] result;
			try
			{
				this.buf.ReOpen();
				int num = 0;
				while (num < this.fonts.Length && !fontName.Equals(this.fonts[num].name))
				{
					num++;
				}
				if (num == this.fonts.Length)
				{
					result = null;
				}
				else
				{
					if (this.gsubrIndexOffset >= 0)
					{
						this.GBias = this.CalcBias(this.gsubrIndexOffset, num);
					}
					this.BuildNewCharString(num);
					this.BuildNewLGSubrs(num);
					byte[] array = this.BuildNewFile(num);
					result = array;
				}
			}
			finally
			{
				try
				{
					this.buf.Close();
				}
				catch
				{
				}
			}
			return result;
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000C4A54 File Offset: 0x000C3A54
		protected int CalcBias(int Offset, int Font)
		{
			base.Seek(Offset);
			int card = (int)base.GetCard16();
			if (this.fonts[Font].CharstringType == 1)
			{
				return 0;
			}
			if (card < 1240)
			{
				return 107;
			}
			if (card < 33900)
			{
				return 1131;
			}
			return 32768;
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000C4A9F File Offset: 0x000C3A9F
		protected void BuildNewCharString(int FontIndex)
		{
			this.NewCharStringsIndex = this.BuildNewIndex(this.fonts[FontIndex].charstringsOffsets, this.GlyphsUsed, 14);
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000C4AC4 File Offset: 0x000C3AC4
		protected void BuildNewLGSubrs(int Font)
		{
			if (this.fonts[Font].isCID)
			{
				this.hSubrsUsed = new Dictionary<int, int[]>[this.fonts[Font].fdprivateOffsets.Length];
				this.lSubrsUsed = new List<int>[this.fonts[Font].fdprivateOffsets.Length];
				this.NewLSubrsIndex = new byte[this.fonts[Font].fdprivateOffsets.Length][];
				this.fonts[Font].PrivateSubrsOffset = new int[this.fonts[Font].fdprivateOffsets.Length];
				this.fonts[Font].PrivateSubrsOffsetsArray = new int[this.fonts[Font].fdprivateOffsets.Length][];
				List<int> list = new List<int>(this.FDArrayUsed.Keys);
				for (int i = 0; i < list.Count; i++)
				{
					int num = list[i];
					this.hSubrsUsed[num] = new Dictionary<int, int[]>();
					this.lSubrsUsed[num] = new List<int>();
					this.BuildFDSubrsOffsets(Font, num);
					if (this.fonts[Font].PrivateSubrsOffset[num] >= 0)
					{
						this.BuildSubrUsed(Font, num, this.fonts[Font].PrivateSubrsOffset[num], this.fonts[Font].PrivateSubrsOffsetsArray[num], this.hSubrsUsed[num], this.lSubrsUsed[num]);
						this.NewLSubrsIndex[num] = this.BuildNewIndex(this.fonts[Font].PrivateSubrsOffsetsArray[num], this.hSubrsUsed[num], 11);
					}
				}
			}
			else if (this.fonts[Font].privateSubrs >= 0)
			{
				this.fonts[Font].SubrsOffsets = base.GetIndex(this.fonts[Font].privateSubrs);
				this.BuildSubrUsed(Font, -1, this.fonts[Font].privateSubrs, this.fonts[Font].SubrsOffsets, this.hSubrsUsedNonCID, this.lSubrsUsedNonCID);
			}
			this.BuildGSubrsUsed(Font);
			if (this.fonts[Font].privateSubrs >= 0)
			{
				this.NewSubrsIndexNonCID = this.BuildNewIndex(this.fonts[Font].SubrsOffsets, this.hSubrsUsedNonCID, 11);
			}
			this.NewGSubrsIndex = this.BuildNewIndex(this.gsubrOffsets, this.hGSubrsUsed, 11);
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000C4CE8 File Offset: 0x000C3CE8
		protected void BuildFDSubrsOffsets(int Font, int FD)
		{
			this.fonts[Font].PrivateSubrsOffset[FD] = -1;
			base.Seek(this.fonts[Font].fdprivateOffsets[FD]);
			while (base.GetPosition() < this.fonts[Font].fdprivateOffsets[FD] + this.fonts[Font].fdprivateLengths[FD])
			{
				base.GetDictItem();
				if (this.key == "Subrs")
				{
					this.fonts[Font].PrivateSubrsOffset[FD] = (int)this.args[0] + this.fonts[Font].fdprivateOffsets[FD];
				}
			}
			if (this.fonts[Font].PrivateSubrsOffset[FD] >= 0)
			{
				this.fonts[Font].PrivateSubrsOffsetsArray[FD] = base.GetIndex(this.fonts[Font].PrivateSubrsOffset[FD]);
			}
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000C4DC0 File Offset: 0x000C3DC0
		protected void BuildSubrUsed(int Font, int FD, int SubrOffset, int[] SubrsOffsets, Dictionary<int, int[]> hSubr, List<int> lSubr)
		{
			int lbias = this.CalcBias(SubrOffset, Font);
			for (int i = 0; i < this.glyphsInList.Count; i++)
			{
				int num = this.glyphsInList[i];
				int begin = this.fonts[Font].charstringsOffsets[num];
				int end = this.fonts[Font].charstringsOffsets[num + 1];
				if (FD >= 0)
				{
					this.EmptyStack();
					this.NumOfHints = 0;
					int num2 = this.fonts[Font].FDSelect[num];
					if (num2 == FD)
					{
						this.ReadASubr(begin, end, this.GBias, lbias, hSubr, lSubr, SubrsOffsets);
					}
				}
				else
				{
					this.ReadASubr(begin, end, this.GBias, lbias, hSubr, lSubr, SubrsOffsets);
				}
			}
			for (int j = 0; j < lSubr.Count; j++)
			{
				int num3 = lSubr[j];
				if (num3 < SubrsOffsets.Length - 1 && num3 >= 0)
				{
					int begin2 = SubrsOffsets[num3];
					int end2 = SubrsOffsets[num3 + 1];
					this.ReadASubr(begin2, end2, this.GBias, lbias, hSubr, lSubr, SubrsOffsets);
				}
			}
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000C4ED0 File Offset: 0x000C3ED0
		protected void BuildGSubrsUsed(int Font)
		{
			int lbias = 0;
			int num = 0;
			if (this.fonts[Font].privateSubrs >= 0)
			{
				lbias = this.CalcBias(this.fonts[Font].privateSubrs, Font);
				num = this.lSubrsUsedNonCID.Count;
			}
			for (int i = 0; i < this.lGSubrsUsed.Count; i++)
			{
				int num2 = this.lGSubrsUsed[i];
				if (num2 < this.gsubrOffsets.Length - 1 && num2 >= 0)
				{
					int begin = this.gsubrOffsets[num2];
					int end = this.gsubrOffsets[num2 + 1];
					if (this.fonts[Font].isCID)
					{
						this.ReadASubr(begin, end, this.GBias, 0, this.hGSubrsUsed, this.lGSubrsUsed, null);
					}
					else
					{
						this.ReadASubr(begin, end, this.GBias, lbias, this.hSubrsUsedNonCID, this.lSubrsUsedNonCID, this.fonts[Font].SubrsOffsets);
						if (num < this.lSubrsUsedNonCID.Count)
						{
							for (int j = num; j < this.lSubrsUsedNonCID.Count; j++)
							{
								int num3 = this.lSubrsUsedNonCID[j];
								if (num3 < this.fonts[Font].SubrsOffsets.Length - 1 && num3 >= 0)
								{
									int begin2 = this.fonts[Font].SubrsOffsets[num3];
									int end2 = this.fonts[Font].SubrsOffsets[num3 + 1];
									this.ReadASubr(begin2, end2, this.GBias, lbias, this.hSubrsUsedNonCID, this.lSubrsUsedNonCID, this.fonts[Font].SubrsOffsets);
								}
							}
							num = this.lSubrsUsedNonCID.Count;
						}
					}
				}
			}
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000C5078 File Offset: 0x000C4078
		protected void ReadASubr(int begin, int end, int GBias, int LBias, Dictionary<int, int[]> hSubr, List<int> lSubr, int[] LSubrsOffsets)
		{
			this.EmptyStack();
			this.NumOfHints = 0;
			base.Seek(begin);
			while (base.GetPosition() < end)
			{
				this.ReadCommand();
				int position = base.GetPosition();
				object obj = null;
				if (this.arg_count > 0)
				{
					obj = this.args[this.arg_count - 1];
				}
				int arg_count = this.arg_count;
				this.HandelStack();
				if (this.key == "callsubr")
				{
					if (arg_count > 0)
					{
						int num = (int)obj + LBias;
						if (!hSubr.ContainsKey(num))
						{
							hSubr[num] = null;
							lSubr.Add(num);
						}
						this.CalcHints(LSubrsOffsets[num], LSubrsOffsets[num + 1], LBias, GBias, LSubrsOffsets);
						base.Seek(position);
					}
				}
				else if (this.key == "callgsubr")
				{
					if (arg_count > 0)
					{
						int num2 = (int)obj + GBias;
						if (!this.hGSubrsUsed.ContainsKey(num2))
						{
							this.hGSubrsUsed[num2] = null;
							this.lGSubrsUsed.Add(num2);
						}
						this.CalcHints(this.gsubrOffsets[num2], this.gsubrOffsets[num2 + 1], LBias, GBias, LSubrsOffsets);
						base.Seek(position);
					}
				}
				else if (this.key == "hstem" || this.key == "vstem" || this.key == "hstemhm" || this.key == "vstemhm")
				{
					this.NumOfHints += arg_count / 2;
				}
				else if (this.key == "hintmask" || this.key == "cntrmask")
				{
					int num3 = this.NumOfHints / 8;
					if (this.NumOfHints % 8 != 0 || num3 == 0)
					{
						num3++;
					}
					for (int i = 0; i < num3; i++)
					{
						base.GetCard8();
					}
				}
			}
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x000C526C File Offset: 0x000C426C
		protected void HandelStack()
		{
			int num = this.StackOpp();
			if (num >= 2)
			{
				this.EmptyStack();
				return;
			}
			if (num == 1)
			{
				this.PushStack();
				return;
			}
			num *= -1;
			for (int i = 0; i < num; i++)
			{
				this.PopStack();
			}
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000C52AC File Offset: 0x000C42AC
		protected int StackOpp()
		{
			if (this.key == "ifelse")
			{
				return -3;
			}
			if (this.key == "roll" || this.key == "put")
			{
				return -2;
			}
			if (this.key == "callsubr" || this.key == "callgsubr" || this.key == "add" || this.key == "sub" || this.key == "div" || this.key == "mul" || this.key == "drop" || this.key == "and" || this.key == "or" || this.key == "eq")
			{
				return -1;
			}
			if (this.key == "abs" || this.key == "neg" || this.key == "sqrt" || this.key == "exch" || this.key == "index" || this.key == "get" || this.key == "not" || this.key == "return")
			{
				return 0;
			}
			if (this.key == "random" || this.key == "dup")
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000C546C File Offset: 0x000C446C
		protected void EmptyStack()
		{
			for (int i = 0; i < this.arg_count; i++)
			{
				this.args[i] = null;
			}
			this.arg_count = 0;
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000C549A File Offset: 0x000C449A
		protected void PopStack()
		{
			if (this.arg_count > 0)
			{
				this.args[this.arg_count - 1] = null;
				this.arg_count--;
			}
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000C54C3 File Offset: 0x000C44C3
		protected void PushStack()
		{
			this.arg_count++;
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000C54D4 File Offset: 0x000C44D4
		protected void ReadCommand()
		{
			this.key = null;
			bool flag = false;
			while (!flag)
			{
				char card = base.GetCard8();
				if (card == '\u001c')
				{
					int card2 = (int)base.GetCard8();
					int card3 = (int)base.GetCard8();
					this.args[this.arg_count] = (card2 << 8 | card3);
					this.arg_count++;
				}
				else if (card >= ' ' && card <= 'ö')
				{
					this.args[this.arg_count] = (int)(card - '\u008b');
					this.arg_count++;
				}
				else if (card >= '÷' && card <= 'ú')
				{
					int card4 = (int)base.GetCard8();
					this.args[this.arg_count] = (int)((card - '÷') * 'Ā') + card4 + 108;
					this.arg_count++;
				}
				else if (card >= 'û' && card <= 'þ')
				{
					int card5 = (int)base.GetCard8();
					this.args[this.arg_count] = (int)(-(card - 'û') * 'Ā') - card5 - 108;
					this.arg_count++;
				}
				else if (card == 'ÿ')
				{
					int card6 = (int)base.GetCard8();
					int card7 = (int)base.GetCard8();
					int card8 = (int)base.GetCard8();
					int card9 = (int)base.GetCard8();
					this.args[this.arg_count] = (card6 << 24 | card7 << 16 | card8 << 8 | card9);
					this.arg_count++;
				}
				else if (card <= '\u001f' && card != '\u001c')
				{
					flag = true;
					if (card == '\f')
					{
						int num = (int)base.GetCard8();
						if (num > CFFFontSubset.SubrsEscapeFuncs.Length - 1)
						{
							num = CFFFontSubset.SubrsEscapeFuncs.Length - 1;
						}
						this.key = CFFFontSubset.SubrsEscapeFuncs[num];
					}
					else
					{
						this.key = CFFFontSubset.SubrsFunctions[(int)card];
					}
				}
			}
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000C56C0 File Offset: 0x000C46C0
		protected int CalcHints(int begin, int end, int LBias, int GBias, int[] LSubrsOffsets)
		{
			base.Seek(begin);
			while (base.GetPosition() < end)
			{
				this.ReadCommand();
				int position = base.GetPosition();
				object obj = null;
				if (this.arg_count > 0)
				{
					obj = this.args[this.arg_count - 1];
				}
				int arg_count = this.arg_count;
				this.HandelStack();
				if (this.key == "callsubr")
				{
					if (arg_count > 0)
					{
						int num = (int)obj + LBias;
						this.CalcHints(LSubrsOffsets[num], LSubrsOffsets[num + 1], LBias, GBias, LSubrsOffsets);
						base.Seek(position);
					}
				}
				else if (this.key == "callgsubr")
				{
					if (arg_count > 0)
					{
						int num2 = (int)obj + GBias;
						this.CalcHints(this.gsubrOffsets[num2], this.gsubrOffsets[num2 + 1], LBias, GBias, LSubrsOffsets);
						base.Seek(position);
					}
				}
				else if (this.key == "hstem" || this.key == "vstem" || this.key == "hstemhm" || this.key == "vstemhm")
				{
					this.NumOfHints += arg_count / 2;
				}
				else if (this.key == "hintmask" || this.key == "cntrmask")
				{
					int num3 = this.NumOfHints / 8;
					if (this.NumOfHints % 8 != 0 || num3 == 0)
					{
						num3++;
					}
					for (int i = 0; i < num3; i++)
					{
						base.GetCard8();
					}
				}
			}
			return this.NumOfHints;
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000C5868 File Offset: 0x000C4868
		protected byte[] BuildNewIndex(int[] Offsets, Dictionary<int, int[]> Used, byte OperatorForUnusedEntries)
		{
			int num = 0;
			int num2 = 0;
			int[] array = new int[Offsets.Length];
			for (int i = 0; i < Offsets.Length; i++)
			{
				array[i] = num2;
				if (Used.ContainsKey(i))
				{
					num2 += Offsets[i + 1] - Offsets[i];
				}
				else
				{
					num++;
				}
			}
			byte[] array2 = new byte[num2 + num];
			int num3 = 0;
			for (int j = 0; j < Offsets.Length - 1; j++)
			{
				int num4 = array[j];
				int num5 = array[j + 1];
				array[j] = num4 + num3;
				if (num4 != num5)
				{
					this.buf.Seek(Offsets[j]);
					this.buf.ReadFully(array2, num4 + num3, num5 - num4);
				}
				else
				{
					array2[num4 + num3] = OperatorForUnusedEntries;
					num3++;
				}
			}
			array[Offsets.Length - 1] += num3;
			return this.AssembleIndex(array, array2);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000C5948 File Offset: 0x000C4948
		protected byte[] AssembleIndex(int[] NewOffsets, byte[] NewObjects)
		{
			char c = (char)(NewOffsets.Length - 1);
			int num = NewOffsets[NewOffsets.Length - 1];
			byte b;
			if (num <= 255)
			{
				b = 1;
			}
			else if (num <= 65535)
			{
				b = 2;
			}
			else if (num <= 16777215)
			{
				b = 3;
			}
			else
			{
				b = 4;
			}
			byte[] array = new byte[(int)('\u0003' + (char)b * (c + '\u0001')) + NewObjects.Length];
			int num2 = 0;
			array[num2++] = (byte)(c >> 8 & 'ÿ');
			array[num2++] = (byte)(c & 'ÿ');
			array[num2++] = b;
			int i = 0;
			while (i < NewOffsets.Length)
			{
				int num3 = NewOffsets[i];
				int num4 = num3 - NewOffsets[0] + 1;
				switch (b)
				{
				case 1:
					goto IL_EF;
				case 2:
					goto IL_DB;
				case 3:
					goto IL_C6;
				case 4:
					array[num2++] = (byte)(num4 >> 24 & 255);
					goto IL_C6;
				}
				IL_101:
				i++;
				continue;
				IL_EF:
				array[num2++] = (byte)(num4 & 255);
				goto IL_101;
				IL_DB:
				array[num2++] = (byte)(num4 >> 8 & 255);
				goto IL_EF;
				IL_C6:
				array[num2++] = (byte)(num4 >> 16 & 255);
				goto IL_DB;
			}
			foreach (byte b2 in NewObjects)
			{
				array[num2++] = b2;
			}
			return array;
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000C5A90 File Offset: 0x000C4A90
		protected byte[] BuildNewFile(int Font)
		{
			this.OutputList = new List<CFFFont.Item>();
			this.CopyHeader();
			this.BuildIndexHeader(1, 1, 1);
			this.OutputList.Add(new CFFFont.UInt8Item((char)(1 + this.fonts[Font].name.Length)));
			this.OutputList.Add(new CFFFont.StringItem(this.fonts[Font].name));
			this.BuildIndexHeader(1, 2, 1);
			CFFFont.OffsetItem offsetItem = new CFFFont.IndexOffsetItem(2);
			this.OutputList.Add(offsetItem);
			CFFFont.IndexBaseItem indexBaseItem = new CFFFont.IndexBaseItem();
			this.OutputList.Add(indexBaseItem);
			CFFFont.OffsetItem offsetItem2 = new CFFFont.DictOffsetItem();
			CFFFont.OffsetItem offsetItem3 = new CFFFont.DictOffsetItem();
			CFFFont.OffsetItem offsetItem4 = new CFFFont.DictOffsetItem();
			CFFFont.OffsetItem offsetItem5 = new CFFFont.DictOffsetItem();
			CFFFont.OffsetItem offsetItem6 = new CFFFont.DictOffsetItem();
			if (!this.fonts[Font].isCID)
			{
				this.OutputList.Add(new CFFFont.DictNumberItem(this.fonts[Font].nstrings));
				this.OutputList.Add(new CFFFont.DictNumberItem(this.fonts[Font].nstrings + 1));
				this.OutputList.Add(new CFFFont.DictNumberItem(0));
				this.OutputList.Add(new CFFFont.UInt8Item('\f'));
				this.OutputList.Add(new CFFFont.UInt8Item('\u001e'));
				this.OutputList.Add(new CFFFont.DictNumberItem(this.fonts[Font].nglyphs));
				this.OutputList.Add(new CFFFont.UInt8Item('\f'));
				this.OutputList.Add(new CFFFont.UInt8Item('"'));
			}
			base.Seek(this.topdictOffsets[Font]);
			while (base.GetPosition() < this.topdictOffsets[Font + 1])
			{
				int position = base.GetPosition();
				base.GetDictItem();
				int position2 = base.GetPosition();
				if (!(this.key == "Encoding") && !(this.key == "Private") && !(this.key == "FDSelect") && !(this.key == "FDArray") && !(this.key == "charset") && !(this.key == "CharStrings"))
				{
					this.OutputList.Add(new CFFFont.RangeItem(this.buf, position, position2 - position));
				}
			}
			this.CreateKeys(offsetItem4, offsetItem5, offsetItem2, offsetItem3);
			this.OutputList.Add(new CFFFont.IndexMarkerItem(offsetItem, indexBaseItem));
			if (this.fonts[Font].isCID)
			{
				this.OutputList.Add(this.GetEntireIndexRange(this.stringIndexOffset));
			}
			else
			{
				this.CreateNewStringIndex(Font);
			}
			this.OutputList.Add(new CFFFont.RangeItem(new RandomAccessFileOrArray(this.NewGSubrsIndex), 0, this.NewGSubrsIndex.Length));
			if (this.fonts[Font].isCID)
			{
				this.OutputList.Add(new CFFFont.MarkerItem(offsetItem5));
				if (this.fonts[Font].fdselectOffset >= 0)
				{
					this.OutputList.Add(new CFFFont.RangeItem(this.buf, this.fonts[Font].fdselectOffset, this.fonts[Font].FDSelectLength));
				}
				else
				{
					this.CreateFDSelect(offsetItem5, this.fonts[Font].nglyphs);
				}
				this.OutputList.Add(new CFFFont.MarkerItem(offsetItem2));
				this.OutputList.Add(new CFFFont.RangeItem(this.buf, this.fonts[Font].charsetOffset, this.fonts[Font].CharsetLength));
				if (this.fonts[Font].fdarrayOffset >= 0)
				{
					this.OutputList.Add(new CFFFont.MarkerItem(offsetItem4));
					this.Reconstruct(Font);
				}
				else
				{
					this.CreateFDArray(offsetItem4, offsetItem6, Font);
				}
			}
			else
			{
				this.CreateFDSelect(offsetItem5, this.fonts[Font].nglyphs);
				this.CreateCharset(offsetItem2, this.fonts[Font].nglyphs);
				this.CreateFDArray(offsetItem4, offsetItem6, Font);
			}
			if (this.fonts[Font].privateOffset >= 0)
			{
				CFFFont.IndexBaseItem indexBaseItem2 = new CFFFont.IndexBaseItem();
				this.OutputList.Add(indexBaseItem2);
				this.OutputList.Add(new CFFFont.MarkerItem(offsetItem6));
				CFFFont.OffsetItem offsetItem7 = new CFFFont.DictOffsetItem();
				this.CreateNonCIDPrivate(Font, offsetItem7);
				this.CreateNonCIDSubrs(Font, indexBaseItem2, offsetItem7);
			}
			this.OutputList.Add(new CFFFont.MarkerItem(offsetItem3));
			this.OutputList.Add(new CFFFont.RangeItem(new RandomAccessFileOrArray(this.NewCharStringsIndex), 0, this.NewCharStringsIndex.Length));
			int[] array = new int[]
			{
				0
			};
			foreach (CFFFont.Item item in this.OutputList)
			{
				item.Increment(array);
			}
			foreach (CFFFont.Item item2 in this.OutputList)
			{
				item2.Xref();
			}
			int num = array[0];
			byte[] array2 = new byte[num];
			foreach (CFFFont.Item item3 in this.OutputList)
			{
				item3.Emit(array2);
			}
			return array2;
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000C5FE4 File Offset: 0x000C4FE4
		protected void CopyHeader()
		{
			base.Seek(0);
			base.GetCard8();
			base.GetCard8();
			int card = (int)base.GetCard8();
			base.GetCard8();
			this.nextIndexOffset = card;
			this.OutputList.Add(new CFFFont.RangeItem(this.buf, 0, card));
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000C6034 File Offset: 0x000C5034
		protected void BuildIndexHeader(int Count, int Offsize, int First)
		{
			this.OutputList.Add(new CFFFont.UInt16Item((char)Count));
			this.OutputList.Add(new CFFFont.UInt8Item((char)Offsize));
			switch (Offsize)
			{
			case 1:
				this.OutputList.Add(new CFFFont.UInt8Item((char)First));
				return;
			case 2:
				this.OutputList.Add(new CFFFont.UInt16Item((char)First));
				return;
			case 3:
				this.OutputList.Add(new CFFFont.UInt24Item((int)((ushort)First)));
				return;
			case 4:
				this.OutputList.Add(new CFFFont.UInt32Item((int)((ushort)First)));
				return;
			default:
				return;
			}
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000C60CC File Offset: 0x000C50CC
		protected void CreateKeys(CFFFont.OffsetItem fdarrayRef, CFFFont.OffsetItem fdselectRef, CFFFont.OffsetItem charsetRef, CFFFont.OffsetItem charstringsRef)
		{
			this.OutputList.Add(fdarrayRef);
			this.OutputList.Add(new CFFFont.UInt8Item('\f'));
			this.OutputList.Add(new CFFFont.UInt8Item('$'));
			this.OutputList.Add(fdselectRef);
			this.OutputList.Add(new CFFFont.UInt8Item('\f'));
			this.OutputList.Add(new CFFFont.UInt8Item('%'));
			this.OutputList.Add(charsetRef);
			this.OutputList.Add(new CFFFont.UInt8Item('\u000f'));
			this.OutputList.Add(charstringsRef);
			this.OutputList.Add(new CFFFont.UInt8Item('\u0011'));
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000C6178 File Offset: 0x000C5178
		protected void CreateNewStringIndex(int Font)
		{
			string text = this.fonts[Font].name + "-OneRange";
			if (text.Length > 127)
			{
				text = text.Substring(0, 127);
			}
			string text2 = "AdobeIdentity" + text;
			int num = this.stringOffsets[this.stringOffsets.Length - 1] - this.stringOffsets[0];
			int num2 = this.stringOffsets[0] - 1;
			byte b;
			if (num + text2.Length <= 255)
			{
				b = 1;
			}
			else if (num + text2.Length <= 65535)
			{
				b = 2;
			}
			else if (num + text2.Length <= 16777215)
			{
				b = 3;
			}
			else
			{
				b = 4;
			}
			this.OutputList.Add(new CFFFont.UInt16Item((char)(this.stringOffsets.Length - 1 + 3)));
			this.OutputList.Add(new CFFFont.UInt8Item((char)b));
			foreach (int num3 in this.stringOffsets)
			{
				this.OutputList.Add(new CFFFont.IndexOffsetItem((int)b, num3 - num2));
			}
			int num4 = this.stringOffsets[this.stringOffsets.Length - 1] - num2;
			num4 += "Adobe".Length;
			this.OutputList.Add(new CFFFont.IndexOffsetItem((int)b, num4));
			num4 += "Identity".Length;
			this.OutputList.Add(new CFFFont.IndexOffsetItem((int)b, num4));
			num4 += text.Length;
			this.OutputList.Add(new CFFFont.IndexOffsetItem((int)b, num4));
			this.OutputList.Add(new CFFFont.RangeItem(this.buf, this.stringOffsets[0], num));
			this.OutputList.Add(new CFFFont.StringItem(text2));
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000C6334 File Offset: 0x000C5334
		protected void CreateFDSelect(CFFFont.OffsetItem fdselectRef, int nglyphs)
		{
			this.OutputList.Add(new CFFFont.MarkerItem(fdselectRef));
			this.OutputList.Add(new CFFFont.UInt8Item('\u0003'));
			this.OutputList.Add(new CFFFont.UInt16Item('\u0001'));
			this.OutputList.Add(new CFFFont.UInt16Item('\0'));
			this.OutputList.Add(new CFFFont.UInt8Item('\0'));
			this.OutputList.Add(new CFFFont.UInt16Item((char)nglyphs));
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x000C63A8 File Offset: 0x000C53A8
		protected void CreateCharset(CFFFont.OffsetItem charsetRef, int nglyphs)
		{
			this.OutputList.Add(new CFFFont.MarkerItem(charsetRef));
			this.OutputList.Add(new CFFFont.UInt8Item('\u0002'));
			this.OutputList.Add(new CFFFont.UInt16Item('\u0001'));
			this.OutputList.Add(new CFFFont.UInt16Item((char)(nglyphs - 1)));
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x000C63FC File Offset: 0x000C53FC
		protected void CreateFDArray(CFFFont.OffsetItem fdarrayRef, CFFFont.OffsetItem privateRef, int Font)
		{
			this.OutputList.Add(new CFFFont.MarkerItem(fdarrayRef));
			this.BuildIndexHeader(1, 1, 1);
			CFFFont.OffsetItem offsetItem = new CFFFont.IndexOffsetItem(1);
			this.OutputList.Add(offsetItem);
			CFFFont.IndexBaseItem indexBaseItem = new CFFFont.IndexBaseItem();
			this.OutputList.Add(indexBaseItem);
			int num = this.fonts[Font].privateLength;
			int num2 = this.CalcSubrOffsetSize(this.fonts[Font].privateOffset, this.fonts[Font].privateLength);
			if (num2 != 0)
			{
				num += 5 - num2;
			}
			this.OutputList.Add(new CFFFont.DictNumberItem(num));
			this.OutputList.Add(privateRef);
			this.OutputList.Add(new CFFFont.UInt8Item('\u0012'));
			this.OutputList.Add(new CFFFont.IndexMarkerItem(offsetItem, indexBaseItem));
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x000C64C4 File Offset: 0x000C54C4
		private void Reconstruct(int Font)
		{
			CFFFont.OffsetItem[] fdPrivate = new CFFFont.DictOffsetItem[this.fonts[Font].FDArrayOffsets.Length - 1];
			CFFFont.IndexBaseItem[] fdPrivateBase = new CFFFont.IndexBaseItem[this.fonts[Font].fdprivateOffsets.Length];
			CFFFont.OffsetItem[] fdSubrs = new CFFFont.DictOffsetItem[this.fonts[Font].fdprivateOffsets.Length];
			this.ReconstructFDArray(Font, fdPrivate);
			this.ReconstructPrivateDict(Font, fdPrivate, fdPrivateBase, fdSubrs);
			this.ReconstructPrivateSubrs(Font, fdPrivateBase, fdSubrs);
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x000C6530 File Offset: 0x000C5530
		private void ReconstructFDArray(int Font, CFFFont.OffsetItem[] fdPrivate)
		{
			this.BuildIndexHeader(this.fonts[Font].FDArrayCount, this.fonts[Font].FDArrayOffsize, 1);
			CFFFont.OffsetItem[] array = new CFFFont.IndexOffsetItem[this.fonts[Font].FDArrayOffsets.Length - 1];
			for (int i = 0; i < this.fonts[Font].FDArrayOffsets.Length - 1; i++)
			{
				array[i] = new CFFFont.IndexOffsetItem(this.fonts[Font].FDArrayOffsize);
				this.OutputList.Add(array[i]);
			}
			CFFFont.IndexBaseItem indexBaseItem = new CFFFont.IndexBaseItem();
			this.OutputList.Add(indexBaseItem);
			for (int j = 0; j < this.fonts[Font].FDArrayOffsets.Length - 1; j++)
			{
				if (this.FDArrayUsed.ContainsKey(j))
				{
					base.Seek(this.fonts[Font].FDArrayOffsets[j]);
					while (base.GetPosition() < this.fonts[Font].FDArrayOffsets[j + 1])
					{
						int position = base.GetPosition();
						base.GetDictItem();
						int position2 = base.GetPosition();
						if (this.key == "Private")
						{
							int num = (int)this.args[0];
							int num2 = this.CalcSubrOffsetSize(this.fonts[Font].fdprivateOffsets[j], this.fonts[Font].fdprivateLengths[j]);
							if (num2 != 0)
							{
								num += 5 - num2;
							}
							this.OutputList.Add(new CFFFont.DictNumberItem(num));
							fdPrivate[j] = new CFFFont.DictOffsetItem();
							this.OutputList.Add(fdPrivate[j]);
							this.OutputList.Add(new CFFFont.UInt8Item('\u0012'));
							base.Seek(position2);
						}
						else
						{
							this.OutputList.Add(new CFFFont.RangeItem(this.buf, position, position2 - position));
						}
					}
				}
				this.OutputList.Add(new CFFFont.IndexMarkerItem(array[j], indexBaseItem));
			}
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x000C6714 File Offset: 0x000C5714
		internal void ReconstructPrivateDict(int Font, CFFFont.OffsetItem[] fdPrivate, CFFFont.IndexBaseItem[] fdPrivateBase, CFFFont.OffsetItem[] fdSubrs)
		{
			for (int i = 0; i < this.fonts[Font].fdprivateOffsets.Length; i++)
			{
				if (this.FDArrayUsed.ContainsKey(i))
				{
					this.OutputList.Add(new CFFFont.MarkerItem(fdPrivate[i]));
					fdPrivateBase[i] = new CFFFont.IndexBaseItem();
					this.OutputList.Add(fdPrivateBase[i]);
					base.Seek(this.fonts[Font].fdprivateOffsets[i]);
					while (base.GetPosition() < this.fonts[Font].fdprivateOffsets[i] + this.fonts[Font].fdprivateLengths[i])
					{
						int position = base.GetPosition();
						base.GetDictItem();
						int position2 = base.GetPosition();
						if (this.key == "Subrs")
						{
							fdSubrs[i] = new CFFFont.DictOffsetItem();
							this.OutputList.Add(fdSubrs[i]);
							this.OutputList.Add(new CFFFont.UInt8Item('\u0013'));
						}
						else
						{
							this.OutputList.Add(new CFFFont.RangeItem(this.buf, position, position2 - position));
						}
					}
				}
			}
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x000C6828 File Offset: 0x000C5828
		internal void ReconstructPrivateSubrs(int Font, CFFFont.IndexBaseItem[] fdPrivateBase, CFFFont.OffsetItem[] fdSubrs)
		{
			for (int i = 0; i < this.fonts[Font].fdprivateLengths.Length; i++)
			{
				if (fdSubrs[i] != null && this.fonts[Font].PrivateSubrsOffset[i] >= 0)
				{
					this.OutputList.Add(new CFFFont.SubrMarkerItem(fdSubrs[i], fdPrivateBase[i]));
					this.OutputList.Add(new CFFFont.RangeItem(new RandomAccessFileOrArray(this.NewLSubrsIndex[i]), 0, this.NewLSubrsIndex[i].Length));
				}
			}
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x000C68A4 File Offset: 0x000C58A4
		internal int CalcSubrOffsetSize(int Offset, int Size)
		{
			int result = 0;
			base.Seek(Offset);
			while (base.GetPosition() < Offset + Size)
			{
				int position = base.GetPosition();
				base.GetDictItem();
				int position2 = base.GetPosition();
				if (this.key == "Subrs")
				{
					result = position2 - position - 1;
				}
			}
			return result;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000C68F4 File Offset: 0x000C58F4
		protected int CountEntireIndexRange(int indexOffset)
		{
			base.Seek(indexOffset);
			int card = (int)base.GetCard16();
			if (card == 0)
			{
				return 2;
			}
			int card2 = (int)base.GetCard8();
			base.Seek(indexOffset + 2 + 1 + card * card2);
			int num = base.GetOffset(card2) - 1;
			return 3 + (card + 1) * card2 + num;
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x000C6940 File Offset: 0x000C5940
		internal void CreateNonCIDPrivate(int Font, CFFFont.OffsetItem Subr)
		{
			base.Seek(this.fonts[Font].privateOffset);
			while (base.GetPosition() < this.fonts[Font].privateOffset + this.fonts[Font].privateLength)
			{
				int position = base.GetPosition();
				base.GetDictItem();
				int position2 = base.GetPosition();
				if (this.key == "Subrs")
				{
					this.OutputList.Add(Subr);
					this.OutputList.Add(new CFFFont.UInt8Item('\u0013'));
				}
				else
				{
					this.OutputList.Add(new CFFFont.RangeItem(this.buf, position, position2 - position));
				}
			}
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000C69E8 File Offset: 0x000C59E8
		internal void CreateNonCIDSubrs(int Font, CFFFont.IndexBaseItem PrivateBase, CFFFont.OffsetItem Subrs)
		{
			this.OutputList.Add(new CFFFont.SubrMarkerItem(Subrs, PrivateBase));
			if (this.NewSubrsIndexNonCID != null)
			{
				this.OutputList.Add(new CFFFont.RangeItem(new RandomAccessFileOrArray(this.NewSubrsIndexNonCID), 0, this.NewSubrsIndexNonCID.Length));
			}
		}

		// Token: 0x040016A5 RID: 5797
		internal const byte ENDCHAR_OP = 14;

		// Token: 0x040016A6 RID: 5798
		internal const byte RETURN_OP = 11;

		// Token: 0x040016A7 RID: 5799
		internal static string[] SubrsFunctions = new string[]
		{
			"RESERVED_0",
			"hstem",
			"RESERVED_2",
			"vstem",
			"vmoveto",
			"rlineto",
			"hlineto",
			"vlineto",
			"rrcurveto",
			"RESERVED_9",
			"callsubr",
			"return",
			"escape",
			"RESERVED_13",
			"endchar",
			"RESERVED_15",
			"RESERVED_16",
			"RESERVED_17",
			"hstemhm",
			"hintmask",
			"cntrmask",
			"rmoveto",
			"hmoveto",
			"vstemhm",
			"rcurveline",
			"rlinecurve",
			"vvcurveto",
			"hhcurveto",
			"shortint",
			"callgsubr",
			"vhcurveto",
			"hvcurveto"
		};

		// Token: 0x040016A8 RID: 5800
		internal static string[] SubrsEscapeFuncs = new string[]
		{
			"RESERVED_0",
			"RESERVED_1",
			"RESERVED_2",
			"and",
			"or",
			"not",
			"RESERVED_6",
			"RESERVED_7",
			"RESERVED_8",
			"abs",
			"add",
			"sub",
			"div",
			"RESERVED_13",
			"neg",
			"eq",
			"RESERVED_16",
			"RESERVED_17",
			"drop",
			"RESERVED_19",
			"put",
			"get",
			"ifelse",
			"random",
			"mul",
			"RESERVED_25",
			"sqrt",
			"dup",
			"exch",
			"index",
			"roll",
			"RESERVED_31",
			"RESERVED_32",
			"RESERVED_33",
			"hflex",
			"flex",
			"hflex1",
			"flex1",
			"RESERVED_REST"
		};

		// Token: 0x040016A9 RID: 5801
		internal Dictionary<int, int[]> GlyphsUsed;

		// Token: 0x040016AA RID: 5802
		internal List<int> glyphsInList;

		// Token: 0x040016AB RID: 5803
		internal Dictionary<int, object> FDArrayUsed = new Dictionary<int, object>();

		// Token: 0x040016AC RID: 5804
		internal Dictionary<int, int[]>[] hSubrsUsed;

		// Token: 0x040016AD RID: 5805
		internal List<int>[] lSubrsUsed;

		// Token: 0x040016AE RID: 5806
		internal Dictionary<int, int[]> hGSubrsUsed = new Dictionary<int, int[]>();

		// Token: 0x040016AF RID: 5807
		internal List<int> lGSubrsUsed = new List<int>();

		// Token: 0x040016B0 RID: 5808
		internal Dictionary<int, int[]> hSubrsUsedNonCID = new Dictionary<int, int[]>();

		// Token: 0x040016B1 RID: 5809
		internal List<int> lSubrsUsedNonCID = new List<int>();

		// Token: 0x040016B2 RID: 5810
		internal byte[][] NewLSubrsIndex;

		// Token: 0x040016B3 RID: 5811
		internal byte[] NewSubrsIndexNonCID;

		// Token: 0x040016B4 RID: 5812
		internal byte[] NewGSubrsIndex;

		// Token: 0x040016B5 RID: 5813
		internal byte[] NewCharStringsIndex;

		// Token: 0x040016B6 RID: 5814
		internal int GBias;

		// Token: 0x040016B7 RID: 5815
		internal List<CFFFont.Item> OutputList;

		// Token: 0x040016B8 RID: 5816
		internal int NumOfHints;
	}
}
