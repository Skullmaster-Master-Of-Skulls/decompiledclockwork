using System;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000608 RID: 1544
	public class RangeRichTextString : RichTextString, IRTFWrapper
	{
		// Token: 0x06005B5C RID: 23388 RVA: 0x0038F57C File Offset: 0x0038E57C
		internal RangeRichTextString(spr\u1DF5 A_0, object A_1, int A_2, int A_3) : this(A_0, A_1, sprṔ.ᜀ(A_3, A_2))
		{
		}

		// Token: 0x06005B5D RID: 23389 RVA: 0x0038F59C File Offset: 0x0038E59C
		internal RangeRichTextString(spr\u1DF5 A_0, object A_1, long A_2) : base(A_0, ((XlsWorksheet)A_1).ParentWorkbook)
		{
			this.ᜀ = (XlsWorksheet)A_1;
			this.ᜁ = A_2;
			this.ᜁ = this.ᜀ.ᜂ(this.ᜁ);
			if (this.ᜁ != null)
			{
				this.ᜁ = this.ᜁ.\u170D();
			}
		}

		// Token: 0x06005B5E RID: 23390 RVA: 0x0038F604 File Offset: 0x0038E604
		internal RangeRichTextString(spr\u1DF5 A_0, object A_1, long A_2, spr\u223A A_3) : base(A_0, ((XlsWorksheet)A_1).ParentWorkbook, true)
		{
			this.ᜀ = (XlsWorksheet)A_1;
			this.ᜁ = A_2;
			this.ᜁ = A_3;
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06005B5F RID: 23391 RVA: 0x0038F640 File Offset: 0x0038E640
		// (set) Token: 0x06005B60 RID: 23392 RVA: 0x0038F6AC File Offset: 0x0038E6AC
		public override XlsFont DefaultFont
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
				spr\u192F spr_u192F = this.ᜀ.ᜃ(this.ᜁ);
				int index = spr_u192F.\u173B();
				return this.ᜂ.InnerFonts[index] as XlsFont;
			}
			internal set
			{
				int row;
				int column;
				IInternalFont internalFont;
				for (;;)
				{
					row = sprṔ.ᜁ(this.ᜁ);
					column = sprṔ.ᜀ(this.ᜁ);
					internalFont = (this.ᜂ.AddFont(value) as IInternalFont);
					if (!(this.ᜀ[row, column].Style is CellStyle))
					{
						goto IL_A0;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_63;
					}
				}
				IL_63:
				if (false)
				{
				}
				((this.ᜀ[row, column].Style as CellStyle).Wrapped as AddtionalFormatWrapper).FontIndex = internalFont.Index;
				return;
				IL_A0:
				(this.ᜀ[row, column].Style as AddtionalFormatWrapper).FontIndex = internalFont.Index;
			}
		}

		// Token: 0x06005B61 RID: 23393 RVA: 0x0038F77C File Offset: 0x0038E77C
		public override void BeginUpdate()
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (this.ᜁ != null)
					{
						num = 3;
						continue;
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
						this.ᜁ = new spr\u223A();
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_D7;
				case 3:
				{
					if (true)
					{
					}
					SSTDictionary sstdictionary = this.ᜂ.InnerSST;
					sstdictionary.Parse();
					int stringIndex = this.ᜀ.GetStringIndex(this.ᜁ);
					int stringCount = sstdictionary.GetStringCount(stringIndex);
					num = 4;
					continue;
				}
				case 4:
				{
					int stringCount;
					if (stringCount != 1)
					{
						num = 8;
						continue;
					}
					SSTDictionary sstdictionary;
					int stringIndex;
					sstdictionary.RemoveDecrease(stringIndex);
					num = 5;
					continue;
				}
				case 5:
					goto IL_62;
				case 6:
					goto IL_80;
				case 8:
					this.ᜁ = this.ᜁ.\u170D();
					num = 6;
					continue;
				}
				IL_34:
				if (base.BeginCallsCount == 0)
				{
					num = 0;
					continue;
				}
				break;
				goto IL_34;
			}
			IL_62:
			IL_80:
			IL_D7:
			base.BeginUpdate();
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x0038F8C0 File Offset: 0x0038E8C0
		public override void EndUpdate()
		{
			for (;;)
			{
				IL_24:
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_73:
					num = 5;
					break;
				default:
					if (false)
					{
					}
					base.EndUpdate();
					num = 6;
					break;
				}
				for (;;)
				{
					object obj;
					SSTDictionary sstdictionary;
					switch (num)
					{
					case 0:
						obj = this.ᜁ;
						goto IL_C4;
					case 1:
						return;
					case 2:
						goto IL_B8;
					case 3:
						sstdictionary = this.ᜂ.InnerSST;
						num = 4;
						continue;
					case 4:
						if (this.ᜁ.ᜆ() <= 0)
						{
							num = 2;
							continue;
						}
						num = 0;
						continue;
					case 5:
						obj = this.ᜁ.ᜏ();
						goto IL_C4;
					case 6:
						if (base.BeginCallsCount == 0)
						{
							num = 3;
							continue;
						}
						return;
					}
					goto IL_24;
					IL_C4:
					object key = obj;
					int iSSTIndex = sstdictionary.AddIncrease(key);
					this.ᜀ.SetLabelSSTIndex(this.ᜁ, iSSTIndex);
					if (true)
					{
					}
					num = 1;
				}
				IL_B8:
				goto IL_73;
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06005B63 RID: 23395 RVA: 0x0038F9C4 File Offset: 0x0038E9C4
		public int Index
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
				return this.ᜀ.GetStringIndex(this.ᜁ);
			}
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x0038FA10 File Offset: 0x0038EA10
		public void Dispose()
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
			GC.SuppressFinalize(this);
		}

		// Token: 0x04002C96 RID: 11414
		private new XlsWorksheet ᜀ;

		// Token: 0x04002C97 RID: 11415
		private bool[] \u2593\u008D\u00A7\u0094;

		// Token: 0x04002C98 RID: 11416
		private int \u2593\u00AC\u0084\u0087;

		// Token: 0x04002C99 RID: 11417
		private string[] \u25D8\u0084\u0098\u0099;

		// Token: 0x04002C9A RID: 11418
		private float \u2609\u0087\u007F\u00A3;

		// Token: 0x04002C9B RID: 11419
		private float[] \u25D8\u00AE\u00AF\u0085;

		// Token: 0x04002C9C RID: 11420
		private new long ᜁ;
	}
}
