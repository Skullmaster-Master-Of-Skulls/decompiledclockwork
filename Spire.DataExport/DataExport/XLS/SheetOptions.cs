using System;
using System.ComponentModel;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001D1 RID: 465
	public class SheetOptions : DisposabledObject, ICloneable
	{
		// Token: 0x06000DFD RID: 3581 RVA: 0x0009CC78 File Offset: 0x0009BC78
		public SheetOptions()
		{
			int a_ = 7;
			this.ᜁ = string.Empty;
			this.ᜂ = HyperlinksCollectionEditor.b("猢䐤䀦䰨ପବ缮ᄰ尲匴᜶Ἰ町", a_);
			this.ᜃ = HyperlinksCollectionEditor.b("瀢䴤䈦䰨弪ബḮ", a_);
			this.ᜄ = new CellFormat();
			this.ᜅ = new CellFormat();
			this.ᜆ = new CellFormat();
			this.ᜇ = new CellFormat();
			this.ᜈ = new CellFormat();
			this.ᜉ = new CellFormat();
			this.ᜊ = new CellNoteFormat();
			this.ᜋ = new CellFont();
			base..ctor();
			this.ᜅ.Font.Bold = true;
			this.ᜉ.Font.Color = CellColor.Blue;
			this.ᜉ.Font.Underline = XlsFontUnderline.Single;
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0009CD54 File Offset: 0x0009BD54
		protected override void Dispose(bool Disposing)
		{
			if (!this.ᜀ)
			{
				if (true)
				{
				}
				try
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜄ.Dispose();
							this.ᜅ.Dispose();
							this.ᜆ.Dispose();
							this.ᜇ.Dispose();
							this.ᜈ.Dispose();
							this.ᜉ.Dispose();
							this.ᜊ.Dispose();
							this.ᜋ = null;
							num = 3;
							continue;
						case 1:
							goto IL_CE;
						case 3:
							goto IL_A0;
						}
						goto IL_35;
						IL_38:
						num = 0;
						continue;
						IL_35:
						if (Disposing)
						{
							goto IL_38;
						}
						IL_A0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_38;
						default:
							if (false)
							{
							}
							this.ᜀ = true;
							num = 1;
							break;
						}
					}
					IL_CE:;
				}
				finally
				{
					base.Dispose(Disposing);
				}
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0009CE54 File Offset: 0x0009BE54
		public object Clone()
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
			return new SheetOptions
			{
				HeaderFormat = this.HeaderFormat,
				TitlesFormat = this.TitlesFormat,
				CustomDataFormat = this.CustomDataFormat,
				AggregateFormat = this.AggregateFormat,
				FooterFormat = this.FooterFormat,
				HyperlinkFormat = this.HyperlinkFormat,
				NoteFormat = this.NoteFormat
			};
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0009CEEC File Offset: 0x0009BEEC
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x0009CF30 File Offset: 0x0009BF30
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string PageHeader
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
				return this.ᜁ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜁ = value;
						goto IL_57;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_57;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 2:
						return;
					}
					if (value != this.ᜁ)
					{
						num = 0;
						continue;
					}
					break;
					IL_57:
					if (true)
					{
					}
					num = 2;
				}
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0009CFB0 File Offset: 0x0009BFB0
		// (set) Token: 0x06000E03 RID: 3587 RVA: 0x0009CFF4 File Offset: 0x0009BFF4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("Page &P of &N")]
		public string PageFooter
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
			set
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
							goto IL_5F;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							break;
						}
						break;
					case 1:
						this.ᜂ = value;
						goto IL_5F;
					case 2:
						return;
					}
					if (value != this.ᜂ)
					{
						num = 1;
						continue;
					}
					break;
					IL_5F:
					num = 2;
				}
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0009D074 File Offset: 0x0009C074
		// (set) Token: 0x06000E05 RID: 3589 RVA: 0x0009D0B8 File Offset: 0x0009C0B8
		[DefaultValue("Sheet 1")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string SheetTitle
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
			set
			{
				int a_ = 12;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8E;
						default:
							goto IL_AF;
						}
						break;
					case 2:
						goto IL_6B;
					case 3:
						if (value.Length > 0)
						{
							goto IL_8E;
						}
						this.ᜃ = HyperlinksCollectionEditor.b("笧䈩䤫䬭䐯̱", a_);
						num = 2;
						continue;
					case 4:
						if (true)
						{
						}
						num = 3;
						continue;
					}
					if (this.ᜃ != value)
					{
						num = 4;
						continue;
					}
					break;
					IL_8E:
					num = 1;
				}
				IL_6B:
				return;
				IL_AF:
				if (false)
				{
				}
				this.ᜃ = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0009D17C File Offset: 0x0009C17C
		// (set) Token: 0x06000E07 RID: 3591 RVA: 0x0009D1C0 File Offset: 0x0009C1C0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public CellFormat HeaderFormat
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
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
						this.ᜄ = value;
						num = 3;
						continue;
					case 2:
						if (value != this.ᜄ)
						{
							num = 1;
							continue;
						}
						return;
					case 3:
						return;
					case 4:
						num = 2;
						continue;
					}
					if (value == null)
					{
						break;
					}
					num = 4;
				}
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0009D258 File Offset: 0x0009C258
		// (set) Token: 0x06000E09 RID: 3593 RVA: 0x0009D29C File Offset: 0x0009C29C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public CellFormat TitlesFormat
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
				return this.ᜅ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						num = 3;
						continue;
					case 2:
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
						this.ᜅ = value;
						num = 4;
						continue;
					case 3:
						if (value != this.ᜅ)
						{
							num = 2;
							continue;
						}
						return;
					case 4:
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0009D334 File Offset: 0x0009C334
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x0009D378 File Offset: 0x0009C378
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public CellFormat CustomDataFormat
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
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						return;
					case 2:
						if (value != this.ᜆ)
						{
							num = 3;
							continue;
						}
						return;
					case 3:
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
						this.ᜆ = value;
						num = 1;
						continue;
					}
					if (value == null)
					{
						break;
					}
					if (true)
					{
					}
					num = 0;
				}
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0009D410 File Offset: 0x0009C410
		// (set) Token: 0x06000E0D RID: 3597 RVA: 0x0009D454 File Offset: 0x0009C454
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public CellFormat AggregateFormat
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
				return this.ᜇ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						break;
					case 1:
						num = 3;
						continue;
					case 2:
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
						this.ᜇ = value;
						num = 4;
						continue;
					case 3:
						if (value != this.ᜇ)
						{
							num = 2;
							continue;
						}
						return;
					case 4:
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0009D4EC File Offset: 0x0009C4EC
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x0009D530 File Offset: 0x0009C530
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CellFormat FooterFormat
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
				return this.ᜈ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						num = 3;
						continue;
					case 2:
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
						this.ᜈ = value;
						num = 4;
						continue;
					case 3:
						if (value != this.ᜈ)
						{
							num = 2;
							continue;
						}
						return;
					case 4:
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0009D5C8 File Offset: 0x0009C5C8
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x0009D60C File Offset: 0x0009C60C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public CellFormat HyperlinkFormat
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
				return this.ᜉ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (value != this.ᜉ)
						{
							num = 3;
							continue;
						}
						return;
					case 1:
						num = 0;
						continue;
					case 3:
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
						this.ᜉ = value;
						num = 4;
						continue;
					case 4:
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0009D6A4 File Offset: 0x0009C6A4
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x0009D6E8 File Offset: 0x0009C6E8
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CellNoteFormat NoteFormat
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
				return this.ᜊ;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						num = 4;
						continue;
					case 2:
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
						this.ᜊ = value;
						num = 0;
						continue;
					case 4:
						if (value != this.ᜊ)
						{
							num = 2;
							continue;
						}
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0009D780 File Offset: 0x0009C780
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x0009D7C4 File Offset: 0x0009C7C4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public CellFont DefaultFont
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜋ = value;
			}
		}

		// Token: 0x04000AA8 RID: 2728
		private bool ᜀ;

		// Token: 0x04000AA9 RID: 2729
		private string ᜁ;

		// Token: 0x04000AAA RID: 2730
		private int \u2593\u0095\u009A\u0085;

		// Token: 0x04000AAB RID: 2731
		private bool \u2609\u009C\u0088\u00A3;

		// Token: 0x04000AAC RID: 2732
		private string ᜂ;

		// Token: 0x04000AAD RID: 2733
		private string ᜃ;

		// Token: 0x04000AAE RID: 2734
		private CellFormat ᜄ;

		// Token: 0x04000AAF RID: 2735
		private CellFormat ᜅ;

		// Token: 0x04000AB0 RID: 2736
		private CellFormat ᜆ;

		// Token: 0x04000AB1 RID: 2737
		private CellFormat ᜇ;

		// Token: 0x04000AB2 RID: 2738
		private string \u25D8\u0094\u009Fª;

		// Token: 0x04000AB3 RID: 2739
		private CellFormat ᜈ;

		// Token: 0x04000AB4 RID: 2740
		private CellFormat ᜉ;

		// Token: 0x04000AB5 RID: 2741
		private CellNoteFormat ᜊ;

		// Token: 0x04000AB6 RID: 2742
		private CellFont ᜋ;
	}
}
