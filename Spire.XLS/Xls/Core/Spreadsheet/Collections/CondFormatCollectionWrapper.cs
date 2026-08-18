using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000171 RID: 369
	public class CondFormatCollectionWrapper : CommonWrapper, IConditionalFormats
	{
		// Token: 0x060011A5 RID: 4517 RVA: 0x000AD868 File Offset: 0x000AC868
		private CondFormatCollectionWrapper()
		{
			this.ᜂ = new List<IConditionalFormat>();
			base..ctor();
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000AD888 File Offset: 0x000AC888
		public CondFormatCollectionWrapper(ICombinedRange range)
		{
			int a_ = 0;
			this.ᜂ = new List<IConditionalFormat>();
			base..ctor();
			if (range == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("䐵夷吹嬻嬽", a_));
			}
			this.ᜀ = range;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000AD8D0 File Offset: 0x000AC8D0
		public override void BeginUpdate()
		{
			for (;;)
			{
				int num;
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
					base.BeginUpdate();
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜃ();
						this.ᜀ.ClearConditionalFormats();
						this.ᜂ();
						num = 2;
						continue;
					case 1:
						if (base.BeginCallsCount == 1)
						{
							num = 0;
							continue;
						}
						return;
					case 2:
						return;
					}
					break;
				}
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x000AD964 File Offset: 0x000AC964
		public override void EndUpdate()
		{
			for (;;)
			{
				int num;
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
					base.EndUpdate();
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (base.BeginCallsCount == 0)
						{
							num = 2;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						this.ᜁ = this.SheetFormats.Add(this.ᜁ);
						num = 1;
						continue;
					}
					break;
				}
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x000AD9F4 File Offset: 0x000AC9F4
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
				this.ᜃ();
				return this.ᜁ.Count;
			}
		}

		// Token: 0x17000639 RID: 1593
		public IConditionalFormat this[int index]
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
				this.ᜃ();
				return this.ᜂ[index];
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000ADA90 File Offset: 0x000ACA90
		public IConditionalFormat AddCondition()
		{
			IConditionalFormat result;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				this.BeginUpdate();
				try
				{
					if (true)
					{
					}
					IConditionalFormat conditionalFormat = this.ᜁ.AddCondition();
					conditionalFormat = new ConditionalFormatWrapper(this, this.Count - 1);
					((ConditionalFormatWrapper)conditionalFormat).Range = this.ᜀ;
					this.ᜂ.Add(conditionalFormat);
					result = conditionalFormat;
				}
				finally
				{
					this.EndUpdate();
				}
				break;
			}
			return result;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x000ADB28 File Offset: 0x000ACB28
		public void Remove()
		{
			int a_ = 1;
			if (this.ᜀ.ConditionalFormats == null)
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
						break;
					default:
						goto IL_40;
					}
				}
				IL_40:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("琶嘸唺夼嘾㕀⩂⩄⥆⡈❊浌ॎ㹐⅒㡔㙖ⵘ", a_));
			}
			((XlsWorksheet)this.ᜀ.Worksheet).ConditionalFormats.Remove(this.ᜀ.GetRectangles());
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x000ADBB4 File Offset: 0x000ACBB4
		public void RemoveAt(int index)
		{
			int a_ = 14;
			if (this.ᜀ.ConditionalFormats == null)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_38;
					}
				}
				IL_38:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("݃⥅♇⹉╋㩍㥏㵑㩓㝕㑗穙ᩛㅝ቟ཡգብ", a_));
			}
			((XlsWorksheet)this.ᜀ.Worksheet).ConditionalFormats.RemoveAt(index);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x000ADC38 File Offset: 0x000ACC38
		public IEnumerator GetEnumerator()
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
			return this.ᜂ.GetEnumerator();
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x000ADC84 File Offset: 0x000ACC84
		internal spr\u1DF5 ReservedHandle
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
				return ((XlsRange)this.ᜀ).Application;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x000ADCD0 File Offset: 0x000ACCD0
		public object Parent
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
				return this.ᜀ;
			}
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x000ADD14 File Offset: 0x000ACD14
		private void ᜃ()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_3D;
				case 2:
				{
					XlsWorksheetConditionalFormats xlsWorksheetConditionalFormats;
					this.ᜁ = new XlsConditionalFormats(this.ReservedHandle, xlsWorksheetConditionalFormats);
					num = 1;
					continue;
				}
				case 3:
					if (this.ᜁ == null)
					{
						num = 2;
						continue;
					}
					goto IL_3D;
				case 4:
				{
					XlsWorksheetConditionalFormats xlsWorksheetConditionalFormats = this.SheetFormats;
					this.ᜁ = xlsWorksheetConditionalFormats.Find(this.ᜀ.GetRectangles());
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4B;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				case 5:
					return;
				}
				if (this.ᜁ == null)
				{
					num = 4;
					continue;
				}
				break;
				IL_4B:
				num = 5;
				continue;
				IL_3D:
				if (true)
				{
				}
				this.ᜁ();
				goto IL_4B;
			}
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x000ADDFC File Offset: 0x000ACDFC
		private void ᜂ()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6A;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						break;
					}
					break;
				case 1:
					this.ᜃ();
					num = 2;
					continue;
				case 2:
					goto IL_6A;
				}
				if (this.ᜁ != null)
				{
					break;
				}
				num = 1;
			}
			IL_6A:
			this.ᜁ = new XlsConditionalFormats(this.ReservedHandle, this.ᜀ, this.ᜁ);
			this.ᜁ.ClearCells();
			this.ᜁ.AddRange(this.ᜀ);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x000ADEB0 File Offset: 0x000ACEB0
		private void ᜁ()
		{
			for (;;)
			{
				int num = 0;
				int count = this.ᜁ.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40;
						default:
							if (false)
							{
							}
							goto IL_38;
						}
						break;
					case 2:
						if (true)
						{
						}
						goto IL_38;
					case 3:
						goto IL_40;
					}
					break;
					IL_38:
					num2 = 3;
					continue;
					IL_40:
					if (num >= count)
					{
						num2 = 0;
					}
					else
					{
						this.ᜂ.Add(new ConditionalFormatWrapper(this, num));
						num++;
						num2 = 1;
					}
				}
			}
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000ADF54 File Offset: 0x000ACF54
		public XlsConditionalFormat GetCondition(int iCondition)
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
			return this.ᜁ[iCondition] as XlsConditionalFormat;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000ADFA0 File Offset: 0x000ACFA0
		public void AddRange(IXLSRange range)
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
			this.ᜁ.AddRange(range);
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x000ADFE8 File Offset: 0x000ACFE8
		private XlsWorksheetConditionalFormats SheetFormats
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
				XlsWorksheet xlsWorksheet = (XlsWorksheet)this.ᜀ.Worksheet;
				return xlsWorksheet.ConditionalFormats;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x000AE03C File Offset: 0x000AD03C
		internal XlsConditionalFormats ConditionalFormats
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

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x000AE080 File Offset: 0x000AD080
		internal IXLSRange Range
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
				return this.ᜀ;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x000AE0C4 File Offset: 0x000AD0C4
		internal List<Rectangle> CellRectangles
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
				return this.ᜁ.CellRectangles;
			}
		}

		// Token: 0x04000E2B RID: 3627
		private ICombinedRange ᜀ;

		// Token: 0x04000E2C RID: 3628
		private bool[] \u2593\u0096\u0088\u0087;

		// Token: 0x04000E2D RID: 3629
		private XlsConditionalFormats ᜁ;

		// Token: 0x04000E2E RID: 3630
		private List<IConditionalFormat> ᜂ;
	}
}
