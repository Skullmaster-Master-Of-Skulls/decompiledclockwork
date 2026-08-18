using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Core.Parser.Biff_Records;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x020001EF RID: 495
	public class XlsConditionalFormats : CollectionExtended<IConditionalFormat>, IConditionalFormats
	{
		// Token: 0x06001C36 RID: 7222 RVA: 0x000F4730 File Offset: 0x000F3730
		internal XlsConditionalFormats(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ = (spr\u21C4)spr\u175E.ᜀ(TBIFFRecord.CondFMT);
			this.ᜁ();
			this.ᜂ = new spr\u2530(this.ᜀ.ᜄ());
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x000F4778 File Offset: 0x000F3778
		internal XlsConditionalFormats(spr\u1DF5 A_0, object A_1, XlsConditionalFormats A_2) : base(A_0, A_1)
		{
			if (A_2 == null)
			{
				return;
			}
			if (A_2.ᜀ != null)
			{
				this.ᜀ = (spr\u21C4)A_2.ᜀ.Clone();
			}
			int i = 0;
			int count = A_2.Count;
			while (i < count)
			{
				XlsConditionalFormat xlsConditionalFormat = (XlsConditionalFormat)A_2.List[i];
				if (xlsConditionalFormat != null)
				{
					object obj = xlsConditionalFormat.Clone(this);
					base.Add(obj as IConditionalFormat);
				}
				i++;
			}
			this.ᜁ();
			this.ᜂ = new spr\u2530(this.ᜀ.ᜄ());
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000F4818 File Offset: 0x000F3818
		internal XlsConditionalFormats(spr\u1DF5 A_0, object A_1, spr\u21C4 A_2, IList A_3)
		{
			int a_ = 17;
			base..ctor(A_0, A_1);
			if (A_2 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("ⅆ♈㥊⁌⹎═", a_));
			}
			if (A_3 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("ⅆ♈㥊⁌⹎═⁒", a_));
			}
			this.ᜀ = A_2;
			int i = 0;
			int count = A_3.Count;
			while (i < count)
			{
				spr\u206F a_2 = A_3[i] as spr\u206F;
				this.ᜀ(a_2);
				i++;
			}
			this.ᜁ();
			this.ᜂ = new spr\u2530(this.ᜀ.ᜄ());
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x000F48BC File Offset: 0x000F38BC
		private new void ᜁ()
		{
			int a_ = 17;
			this.ᜁ = (base.FindParent(typeof(XlsWorksheet)) as XlsWorksheet);
			if (this.ᜁ != null)
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
					return;
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ᝆ⡈㥊⡌ⅎ═獒≔㡖⭘ず⹜㝞Ѡ٢ᅤ䥦", a_));
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x000F493C File Offset: 0x000F393C
		private int MaxCFNumber
		{
			get
			{
				if (this.ᜁ.Version == ExcelVersion.Version97to2003)
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
						return 3;
					}
				}
				if (true)
				{
				}
				return int.MaxValue;
			}
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x000F4990 File Offset: 0x000F3990
		public IConditionalFormat AddCondition()
		{
			int a_ = 9;
			if (base.Count < this.MaxCFNumber)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_17;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				ConditionalFormat conditionalFormat = new ConditionalFormat((spr\u2158)base.ReservedHandle, this);
				base.Add(conditionalFormat);
				return conditionalFormat;
			}
			IL_17:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("款⹀ⱂ敄⩆⡈╊㑌潎㉐㱒㭔㍖じ⽚㑜ぞའɢ।䝦ཨѪὬɮၰݲٴ奶", a_));
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000F4A14 File Offset: 0x000F3A14
		public void Remove()
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

		// Token: 0x06001C3D RID: 7229 RVA: 0x000F4A54 File Offset: 0x000F3A54
		public void RemoveAt()
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

		// Token: 0x06001C3E RID: 7230 RVA: 0x000F4A94 File Offset: 0x000F3A94
		public void SerializeDataToList(RecordArrayList records)
		{
			for (;;)
			{
				int count = base.Count;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2;
						if (num2 >= count)
						{
							num = 3;
							continue;
						}
						if (true)
						{
						}
						XlsConditionalFormat xlsConditionalFormat = (XlsConditionalFormat)base.InnerList[num2];
						xlsConditionalFormat.SerializeDataToList(records);
						num2++;
						num = 1;
						continue;
					}
					case 1:
						goto IL_E6;
					case 2:
						goto IL_E4;
					case 3:
						goto IL_103;
					case 4:
						goto IL_E6;
					case 5:
						if (count > 0)
						{
							num = 7;
							continue;
						}
						return;
					case 6:
						if (this.ᜂ.ᜂ().Count != 0)
						{
							this.ᜀ.ᜁ((ushort)count);
							records.ᜀ(this.ᜀ);
							int num2 = 0;
							num = 4;
							continue;
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
							num = 2;
							continue;
						}
						break;
					case 7:
						num = 6;
						continue;
					}
					break;
					IL_E6:
					num = 0;
				}
			}
			IL_E4:
			return;
			IL_103:;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x000F4BB4 File Offset: 0x000F3BB4
		internal new void ᜀ(spr\u206F A_0)
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
			ConditionalFormat item = new ConditionalFormat((spr\u2158)base.ReservedHandle, this, A_0);
			base.Add(item);
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x000F4C0C File Offset: 0x000F3C0C
		public bool CompareTo(XlsConditionalFormats formats)
		{
			int num = 7;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					if (!this.CompareFormats(base[num2], formats[num2]))
					{
						num = 3;
						continue;
					}
					num2++;
					num = 2;
					continue;
				case 1:
					goto IL_83;
				case 2:
					goto IL_83;
				case 3:
					return false;
				case 4:
					return false;
				case 5:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return true;
					default:
						if (false)
						{
						}
						if (num2 >= base.Count)
						{
							num = 6;
							continue;
						}
						num = 0;
						continue;
					}
					break;
				case 6:
					return true;
				}
				if (formats.Count != base.Count)
				{
					num = 4;
					continue;
				}
				num2 = 0;
				num = 1;
				continue;
				IL_83:
				num = 5;
			}
			return false;
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x000F4D00 File Offset: 0x000F3D00
		public bool CompareFormats(IConditionalFormat firstFormat, IConditionalFormat secondFormat)
		{
			XlsConditionalFormat xlsConditionalFormat = (XlsConditionalFormat)firstFormat;
			XlsConditionalFormat xlsConditionalFormat2 = (XlsConditionalFormat)secondFormat;
			spr\u206F spr_u206F = xlsConditionalFormat.Record;
			spr\u206F spr_u206F2 = xlsConditionalFormat2.Record;
			if (spr_u206F.Data.Length != spr_u206F2.Data.Length)
			{
				if (true)
				{
				}
			}
			else
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
					return BiffRecordRaw.CompareArrays(spr_u206F.Data, 0, spr_u206F2.Data, 0, spr_u206F.Length);
				}
			}
			return false;
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x000F4D88 File Offset: 0x000F3D88
		public void AddCells(XlsConditionalFormats formats)
		{
			if (formats != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					spr\u21C4 spr_u21C = formats.ᜀ;
					List<Rectangle> arrCells = spr_u21C.ᜄ();
					this.AddCells(arrCells);
					return;
				}
				}
			}
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x000F4DE0 File Offset: 0x000F3DE0
		public bool Contains(Rectangle[] arrRanges)
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
			return this.ᜂ.ᜁ(arrRanges);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x000F4E28 File Offset: 0x000F3E28
		public int ContainsCount(Rectangle range)
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
			return this.ᜂ.ᜅ(range);
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x000F4E70 File Offset: 0x000F3E70
		public void AddCells(IList arrCells)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_9B:
				num = 5;
				break;
			default:
				if (false)
				{
				}
				num = 4;
				break;
			}
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_8F;
				case 2:
				{
					if (num2 >= count)
					{
						goto IL_9B;
					}
					Rectangle a_ = (Rectangle)arrCells[num2];
					this.ᜀ(a_);
					num2++;
					num = 3;
					continue;
				}
				case 3:
					goto IL_8F;
				case 5:
					return;
				}
				if (true)
				{
				}
				if (arrCells == null)
				{
					num = 0;
					continue;
				}
				num2 = 0;
				count = arrCells.Count;
				num = 1;
				continue;
				IL_8F:
				num = 2;
			}
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x000F4F2C File Offset: 0x000F3F2C
		public void AddRange(IXLSRange range)
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
			this.ᜂ.ᜀ(range);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x000F4F74 File Offset: 0x000F3F74
		internal new void ᜀ(Rectangle A_0)
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
			this.ᜂ.ᜄ(A_0);
			TAddr a_ = this.ᜀ.ᜀ();
			a_.FirstCol = Math.Min(A_0.Left, a_.FirstCol);
			a_.FirstRow = Math.Min(A_0.Top, a_.FirstRow);
			a_.LastCol = Math.Max(A_0.Right, a_.LastCol);
			a_.LastRow = Math.Max(A_0.Bottom, a_.LastRow);
			this.ᜀ.ᜀ(a_);
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000F503C File Offset: 0x000F403C
		public void Remove(Rectangle[] arrRanges)
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
			this.ᜂ.ᜀ(arrRanges);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000F5084 File Offset: 0x000F4084
		public void ClearCells()
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
			this.ᜀ.ᜄ().Clear();
			this.ᜀ.ᜀ(0);
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x000F50DC File Offset: 0x000F40DC
		internal new void ᜂ()
		{
			for (;;)
			{
				IL_14:
				int count = base.Count;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_75:
					num = 1;
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5D;
					case 1:
						return;
					case 2:
						if (count > 3)
						{
							num = 0;
							continue;
						}
						return;
					}
					goto IL_14;
				}
				IL_5D:
				int count2 = base.Count - 3;
				base.InnerList.RemoveRange(3, count2);
				goto IL_75;
			}
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000F5168 File Offset: 0x000F4168
		internal new XlsConditionalFormats ᜀ(int A_0, int A_1, int A_2, int A_3, bool A_4, int A_5, int A_6, object A_7)
		{
			int num = 1;
			XlsConditionalFormats xlsConditionalFormats;
			for (;;)
			{
				spr\u2530 spr_u;
				switch (num)
				{
				case 0:
					A_2--;
					num = 2;
					continue;
				case 2:
					goto IL_47;
				case 3:
					if (spr_u != null)
					{
						num = 5;
						continue;
					}
					goto IL_125;
				case 4:
					goto IL_125;
				case 5:
					xlsConditionalFormats = (XlsConditionalFormats)this.Clone(A_7);
					xlsConditionalFormats.ᜁ();
					xlsConditionalFormats.ᜂ = spr_u;
					xlsConditionalFormats.ᜀ = (spr\u21C4)spr\u1CD3.ᜀ(this.ᜀ);
					xlsConditionalFormats.ᜀ.ᜀ(spr_u.ᜂ());
					goto IL_F5;
				case 6:
					if (A_2 - 1 != 0)
					{
						num = 0;
						continue;
					}
					goto IL_47;
				case 7:
					A_3--;
					num = 8;
					continue;
				case 8:
					goto IL_102;
				}
				if (A_3 - 1 != 0)
				{
					num = 7;
					continue;
				}
				goto IL_102;
				IL_47:
				Rectangle a_ = new Rectangle(A_1 - 1, A_0 - 1, A_3, A_2);
				spr_u = this.ᜂ.ᜀ(a_, A_4, A_5, A_6);
				xlsConditionalFormats = null;
				num = 3;
				continue;
				IL_F5:
				num = 4;
				continue;
				IL_125:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_F5;
				default:
					goto IL_13B;
				}
				IL_102:
				num = 6;
			}
			IL_13B:
			if (false)
			{
			}
			if (true)
			{
			}
			return xlsConditionalFormats;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x000F52C0 File Offset: 0x000F42C0
		internal new void ᜀ(bool[] A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					List<IConditionalFormat> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 0;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_54;
						case 1:
						{
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							XlsConditionalFormat xlsConditionalFormat = (XlsConditionalFormat)innerList[num];
							xlsConditionalFormat.ᜀ(A_0);
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
								num2 = 3;
								continue;
							}
							break;
						}
						case 2:
							return;
						case 3:
							goto IL_54;
						}
						break;
						IL_54:
						num2 = 1;
					}
				}
				return;
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000F537C File Offset: 0x000F437C
		internal new void ᜀ(int[] A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					List<IConditionalFormat> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							if (true)
							{
							}
							XlsConditionalFormat xlsConditionalFormat = (XlsConditionalFormat)innerList[num];
							xlsConditionalFormat.ᜀ(A_0);
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
						case 1:
							goto IL_4C;
						case 2:
							return;
						case 3:
							goto IL_4C;
						}
						break;
						IL_4C:
						num2 = 0;
					}
				}
				return;
			}
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000F5438 File Offset: 0x000F4438
		internal new void ᜀ(int A_0, int A_1, Rectangle A_2, int A_3, Rectangle A_4)
		{
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_104;
					case 1:
						num = 6;
						continue;
					case 3:
						goto IL_104;
					case 4:
						goto IL_12C;
					case 5:
					{
						if (true)
						{
						}
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 4;
							continue;
						}
						int top;
						int left;
						(base[num2] as XlsConditionalFormat).UpdateFormula(A_0, A_1, A_2, A_3, A_4, top, left);
						num2++;
						num = 3;
						continue;
					}
					case 6:
						if (this.ᜂ.ᜂ().Count != 0)
						{
							Rectangle rectangle = this.ᜂ.ᜂ()[0];
							int top = rectangle.Top;
							int left = rectangle.Left;
							int num2 = 0;
							int count = base.Count;
							num = 0;
							continue;
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
							num = 7;
							continue;
						}
						break;
					case 7:
						goto IL_102;
					}
					if (this.ᜂ != null)
					{
						num = 1;
						continue;
					}
					break;
					IL_104:
					num = 5;
				}
				IL_102:
				return;
				IL_12C:
				return;
			}
			}
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x000F5580 File Offset: 0x000F4580
		public void BeginUpdate()
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

		// Token: 0x06001C50 RID: 7248 RVA: 0x000F55C0 File Offset: 0x000F45C0
		public void EndUpdate()
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

		// Token: 0x06001C51 RID: 7249 RVA: 0x000F5600 File Offset: 0x000F4600
		public override int GetHashCode()
		{
			int num;
			for (;;)
			{
				int count = base.Count;
				num = count;
				int num2 = 0;
				int num3 = 0;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7C;
						default:
							if (false)
							{
							}
							goto IL_53;
						}
						break;
					case 1:
						goto IL_53;
					case 2:
						if (num2 >= count)
						{
							num3 = 3;
							continue;
						}
						num |= base[num2].GetHashCode();
						num2++;
						goto IL_7C;
					case 3:
						goto IL_67;
					}
					break;
					IL_53:
					num3 = 2;
					continue;
					IL_7C:
					num3 = 1;
				}
			}
			IL_67:
			if (true)
			{
			}
			return num;
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x000F569C File Offset: 0x000F469C
		public override bool Equals(object obj)
		{
			for (;;)
			{
				IL_30:
				XlsConditionalFormats xlsConditionalFormats = obj as XlsConditionalFormats;
				for (;;)
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return false;
						case 1:
						{
							if (xlsConditionalFormats == null)
							{
								num = 0;
								continue;
							}
							int count = base.Count;
							num = 4;
							continue;
						}
						case 2:
							goto IL_9D;
						case 3:
							goto IL_9B;
						case 4:
						{
							int count;
							if (count != xlsConditionalFormats.Count)
							{
								num = 3;
								continue;
							}
							int num2 = 0;
							num = 5;
							continue;
						}
						case 5:
							goto IL_9D;
						case 6:
						{
							int count;
							int num2;
							if (num2 >= count)
							{
								num = 7;
								continue;
							}
							num = 9;
							continue;
						}
						case 7:
							goto IL_B7;
						case 8:
							return false;
						case 9:
						{
							int num2;
							if (!base[num2].Equals(xlsConditionalFormats[num2]))
							{
								num = 8;
								continue;
							}
							num2++;
							num = 2;
							continue;
						}
						}
						goto IL_30;
						IL_9D:
						num = 6;
					}
					IL_B7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_105;
					}
				}
			}
			return false;
			IL_9B:
			if (true)
			{
			}
			return false;
			IL_105:
			if (false)
			{
			}
			return true;
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x000F57B8 File Offset: 0x000F47B8
		public override object Clone(object parent)
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
			XlsConditionalFormats xlsConditionalFormats = (XlsConditionalFormats)base.Clone(parent);
			xlsConditionalFormats.ᜀ = (spr\u21C4)spr\u1CD3.ᜀ(this.ᜀ);
			List<Rectangle> a_ = xlsConditionalFormats.ᜀ.ᜄ();
			xlsConditionalFormats.ᜂ = new spr\u2530(a_);
			return xlsConditionalFormats;
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06001C54 RID: 7252 RVA: 0x000F5830 File Offset: 0x000F4830
		public bool IsEmpty
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
				return this.ᜀ.ᜄ().Count == 0;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x000F5880 File Offset: 0x000F4880
		public string Address
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
				TAddr taddr = this.ᜀ.ᜀ();
				return sprṔ.ᜀ(taddr.FirstRow + 1, taddr.FirstCol + 1, taddr.LastRow + 1, taddr.LastCol + 1);
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x000F58F0 File Offset: 0x000F48F0
		public string AddressR1C1
		{
			get
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
				TAddr taddr = this.ᜀ.ᜀ();
				return string.Format(RecordTableEnumerator.b("ᭈお経㉎ቐ⡒摔⩖捘ग़♜浞ᱠ⁢Ṥ呦ᑨ", a_), new object[]
				{
					taddr.FirstRow + 1,
					taddr.FirstCol + 1,
					taddr.LastRow + 1,
					taddr.LastCol + 1
				});
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x000F59A0 File Offset: 0x000F49A0
		// (set) Token: 0x06001C58 RID: 7256 RVA: 0x000F59E8 File Offset: 0x000F49E8
		[CLSCompliant(false)]
		internal TAddr EnclosedRange
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
				return this.ᜀ.ᜀ();
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
				this.ᜀ.ᜀ(value);
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x000F5A30 File Offset: 0x000F4A30
		internal string[] CellsList
		{
			get
			{
				switch (0)
				{
				default:
				{
					string[] array;
					for (;;)
					{
						if (true)
						{
						}
						List<Rectangle> list = this.ᜀ.ᜄ();
						int count = list.Count;
						array = new string[count];
						int num = 0;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								Rectangle rectangle = list[num];
								array[num] = sprṔ.ᜀ(rectangle.Y + 1, rectangle.X + 1, rectangle.Bottom + 1, rectangle.Right + 1);
								num++;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return array;
								default:
									if (false)
									{
									}
									num2 = 1;
									continue;
								}
								break;
							}
							case 1:
								goto IL_60;
							case 2:
								goto IL_60;
							case 3:
								return array;
							}
							break;
							IL_60:
							num2 = 0;
						}
					}
					return array;
				}
				}
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x000F5B1C File Offset: 0x000F4B1C
		public List<Rectangle> CellRectangles
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
				return this.ᜂ.ᜂ();
			}
		}

		// Token: 0x04001074 RID: 4212
		private long \u25D9\u0093\u0080\u008B;

		// Token: 0x04001075 RID: 4213
		public const int MAXIMUM_CF_NUMBER = 3;

		// Token: 0x04001076 RID: 4214
		private new spr\u21C4 ᜀ;

		// Token: 0x04001077 RID: 4215
		private float[] \u2609\u00A8\u0083\u009C;

		// Token: 0x04001078 RID: 4216
		private new XlsWorksheet ᜁ;

		// Token: 0x04001079 RID: 4217
		private long[] \u25D8\u0085\u00AE\u00A0;

		// Token: 0x0400107A RID: 4218
		private string \u25D9\u0098\u0093\u00A4;

		// Token: 0x0400107B RID: 4219
		private string[] \u2460\u009F\u00AE\u00A3;

		// Token: 0x0400107C RID: 4220
		private long[] \u25D8\u00A5\u00A6\u0083;

		// Token: 0x0400107D RID: 4221
		private new spr\u2530 ᜂ;
	}
}
