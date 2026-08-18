using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.TypeConverters;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001AB RID: 427
	[TypeConverter(typeof(CollectionTypeConverter))]
	public class CellHyperlink : CustomItem, ICloneable
	{
		// Token: 0x06000BC7 RID: 3015 RVA: 0x0007BEF0 File Offset: 0x0007AEF0
		public CellHyperlink()
		{
			this.ᜅ = new CellFormat();
			this.ᜆ = string.Empty;
			this.ᜇ = string.Empty;
			this.ᜈ = string.Empty;
			base..ctor();
			this.ᜅ.Font.Color = CellColor.Blue;
			this.ᜅ.Font.Underline = XlsFontUnderline.Single;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0007BF54 File Offset: 0x0007AF54
		public CellHyperlink(CellHyperlinks Collection)
		{
			int a_ = 15;
			this.ᜅ = new CellFormat();
			this.ᜆ = string.Empty;
			this.ᜇ = string.Empty;
			this.ᜈ = string.Empty;
			base..ctor();
			if (Collection != null)
			{
				this.ᜂ = Collection.Holder;
			}
			if (this.ᜂ == null)
			{
				return;
			}
			PropertyInfo property = this.ᜂ.GetType().GetProperty(HyperlinksCollectionEditor.b("搪崬嬮堰尲嬴䐶", a_));
			if (property != null)
			{
				SheetOptions sheetOptions = (SheetOptions)property.GetValue(this.ᜂ, null);
				if (sheetOptions != null)
				{
					CellFormat cellFormat = sheetOptions.HyperlinkFormat.Clone() as CellFormat;
					cellFormat.FieldName = this.ᜅ.FieldName;
					this.ᜅ = cellFormat;
					return;
				}
			}
			else
			{
				property = this.ᜂ.GetType().GetProperty(HyperlinksCollectionEditor.b("挪听弮吰䄲头帶圸债笼倾㍀⹂⑄㍆", a_));
				if (property != null)
				{
					CellFormat cellFormat2 = (CellFormat)property.GetValue(this.ᜂ, null);
					if (cellFormat2 != null)
					{
						CellFormat cellFormat3 = this.ᜅ = (cellFormat2.Clone() as CellFormat);
						cellFormat3.FieldName = this.ᜅ.FieldName;
						this.ᜅ = cellFormat3;
					}
				}
			}
		}

		// Token: 0x06000BC9 RID: 3017
		[DllImport("kernel32")]
		private static extern int GetShortPathName(string A_0, byte[] A_1, int A_2);

		// Token: 0x06000BCA RID: 3018 RVA: 0x0007C0AC File Offset: 0x0007B0AC
		protected override void Dispose(bool Disposing)
		{
			if (!this.ᜁ)
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
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_5E;
							default:
								goto IL_7C;
							}
							break;
						case 1:
							goto IL_57;
						case 3:
							this.ᜅ.Dispose();
							num = 1;
							continue;
						}
						if (Disposing)
						{
							num = 3;
							continue;
						}
						goto IL_57;
						IL_5E:
						num = 0;
						continue;
						IL_57:
						this.ᜁ = true;
						goto IL_5E;
					}
					IL_7C:
					if (false)
					{
					}
				}
				finally
				{
					base.Dispose(Disposing);
				}
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0007C160 File Offset: 0x0007B160
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
			return new CellHyperlink
			{
				Row = this.Row,
				Col = this.Col,
				Style = this.Style,
				Title = this.Title,
				Target = this.Target,
				Format = this.Format,
				Tip = this.Tip
			};
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0007C1F8 File Offset: 0x0007B1F8
		internal override void InitCollectionItem()
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
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0007C234 File Offset: 0x0007B234
		public bool IsValid()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜄ > 0)
					{
						num = 5;
						continue;
					}
					goto IL_AF;
				case 1:
					goto IL_61;
				case 3:
					if (this.ᜃ <= 256)
					{
						num = 7;
						continue;
					}
					goto IL_AF;
				case 4:
					IL_4B:
					if (this.ᜇ.Length > 0)
					{
						num = 1;
						continue;
					}
					goto IL_AF;
				case 5:
					num = 4;
					continue;
				case 6:
					num = 3;
					continue;
				case 7:
					num = 0;
					continue;
				}
				if (this.ᜃ > 0)
				{
					num = 6;
					continue;
				}
				IL_AF:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4B;
				default:
					goto IL_D7;
				}
			}
			IL_61:
			return this.ᜆ.Length > 0;
			IL_D7:
			if (false)
			{
			}
			return false;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0007C320 File Offset: 0x0007B320
		public void SaveToXmlFile(XMLFile File, string Section)
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
			File.WriteValue(Section, HyperlinksCollectionEditor.b("昤䠦䔨帪䀬䄮", a_), this.ᜃ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("眤䠦帨", a_), this.ᜄ.ToString());
			string key = HyperlinksCollectionEditor.b("瘤匦倨䜪䠬", a_);
			int num = (int)this.ᜉ;
			File.WriteValue(Section, key, num.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("焤並崨䜪䠬", a_), this.ᜇ);
			File.WriteValue(Section, HyperlinksCollectionEditor.b("焤䘦嬨䰪䠬嬮", a_), this.ᜆ);
			File.WriteValue(Section, HyperlinksCollectionEditor.b("焤䠦䘨䜪帬笮堰䌲", a_), this.ᜈ);
			this.ᜅ.SaveToXmlFile(File, Section);
			File.SaveToFile();
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0007C42C File Offset: 0x0007B42C
		public void LoadFromXmlFile(XMLFile File, string Section)
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
			this.ᜃ = (int)Convert.ToUInt16(File.ReadValue(Section, HyperlinksCollectionEditor.b("氮帰弲䀴娶圸", a_), 0.ToString()));
			this.ᜄ = (int)Convert.ToUInt16(File.ReadValue(Section, HyperlinksCollectionEditor.b("紮帰䐲", a_), 0.ToString()));
			this.ᜉ = (XlsHyperlinkStyle)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("簮䔰䨲头制", a_), 0.ToString()));
			this.ᜇ = File.ReadValue(Section, HyperlinksCollectionEditor.b("笮堰䜲头制", a_), string.Empty);
			this.ᜆ = File.ReadValue(Section, HyperlinksCollectionEditor.b("笮倰䄲刴制䴸", a_), string.Empty);
			this.ᜈ = File.ReadValue(Section, HyperlinksCollectionEditor.b("笮帰尲头䐶洸刺䴼", a_), string.Empty);
			this.ᜅ.LoadFromXmlFile(File, Section);
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0007C558 File Offset: 0x0007B558
		[Browsable(false)]
		public override ItemType ItemType
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
				return ItemType.Hyperlink;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0007C594 File Offset: 0x0007B594
		[Browsable(false)]
		public int Size
		{
			get
			{
				int num;
				for (;;)
				{
					num = 36 + this.ᜇ.Length * 2 + 2;
					XlsHyperlinkStyle xlsHyperlinkStyle = this.ᜉ;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return num;
					default:
					{
						if (false)
						{
						}
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								return num;
							case 1:
								return num;
							case 2:
								switch (xlsHyperlinkStyle)
								{
								case XlsHyperlinkStyle.URL:
									num += 20 + this.ᜆ.Length * 2 + 2;
									num2 = 3;
									continue;
								case XlsHyperlinkStyle.LocalFile:
									num += 22 + this.ShortTarget.Length + 24 + 4 + 4 + 2 + this.ᜆ.Length;
									if (true)
									{
									}
									num2 = 0;
									continue;
								default:
									num2 = 4;
									continue;
								}
								break;
							case 3:
								return num;
							case 4:
								num2 = 1;
								continue;
							}
							break;
						}
						break;
					}
					}
				}
				return num;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0007C688 File Offset: 0x0007B688
		[Browsable(false)]
		public string ShortTarget
		{
			get
			{
				byte[] array = new byte[260];
				if (CellHyperlink.GetShortPathName(this.ᜆ, array, 260) > 0)
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
						return string.Empty;
					}
				}
				return Encoding.ASCII.GetString(array);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0007C6F8 File Offset: 0x0007B6F8
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x0007C73C File Offset: 0x0007B73C
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Defines the horizontal position of the link.")]
		public int Col
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
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_5D;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5D:
						this.ᜃ = value;
						num = 0;
						break;
					default:
						if (false)
						{
						}
						if (value == this.ᜃ)
						{
							return;
						}
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0007C7B8 File Offset: 0x0007B7B8
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x0007C7FC File Offset: 0x0007B7FC
		[DefaultValue(0)]
		[Description("Defines the vertical position of the link.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int Row
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜄ = value;
						num = 1;
						continue;
					case 1:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						}
						if (true)
						{
						}
						if (false)
						{
						}
						break;
					}
					if (value == this.ᜄ)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0007C878 File Offset: 0x0007B878
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x0007C8BC File Offset: 0x0007B8BC
		[Description("Defines parameters of displaying the hyperlink in the result document.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public CellFormat Format
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
				int num = 1;
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
							goto IL_5D;
						case 2:
							return;
						case 3:
							this.ᜅ = value;
							num = 2;
							continue;
						case 4:
							if (value != this.ᜅ)
							{
								num = 3;
								continue;
							}
							return;
						}
						if (value != null)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						return;
					}
					IL_5D:
					num = 4;
				}
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0007C954 File Offset: 0x0007B954
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0007C998 File Offset: 0x0007B998
		[DefaultValue(XlsHyperlinkStyle.URL)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Defines the type of the hyperlink target.")]
		public XlsHyperlinkStyle Style
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
				return this.ᜉ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						}
						if (false)
						{
						}
						break;
					case 2:
						this.ᜉ = value;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					if (value == this.ᜉ)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0007CA14 File Offset: 0x0007BA14
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0007CA58 File Offset: 0x0007BA58
		[DefaultValue("")]
		[Description("Defines the hyperlink target.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string Target
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_69;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_69;
						}
						if (false)
						{
						}
						break;
					case 2:
						this.ᜆ = value;
						num = 0;
						continue;
					}
					if (!(value != this.ᜆ))
					{
						break;
					}
					num = 2;
				}
				IL_69:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0007CAD8 File Offset: 0x0007BAD8
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x0007CB1C File Offset: 0x0007BB1C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		[Description("Defines the hyperlink text.")]
		public string Title
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
							return;
						}
						if (false)
						{
						}
						break;
					case 1:
						this.ᜇ = value;
						this.SetName(value);
						num = 2;
						continue;
					case 2:
						return;
					}
					if (!(value != this.ᜇ))
					{
						break;
					}
					if (true)
					{
					}
					num = 1;
				}
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0007CBA4 File Offset: 0x0007BBA4
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x0007CBE8 File Offset: 0x0007BBE8
		[DefaultValue("")]
		[Description("Defines the text of the hint to display in Excel for the link.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string Tip
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
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.ᜈ = value;
						num = 0;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (!(value != this.ᜈ))
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x04000905 RID: 2309
		private const int ᜀ = 260;

		// Token: 0x04000906 RID: 2310
		private bool ᜁ;

		// Token: 0x04000907 RID: 2311
		private object ᜂ;

		// Token: 0x04000908 RID: 2312
		private long[] \u2593\u00A0\u0085\u0086;

		// Token: 0x04000909 RID: 2313
		private int ᜃ;

		// Token: 0x0400090A RID: 2314
		private int \u2593ª\u0093\u0092;

		// Token: 0x0400090B RID: 2315
		private int ᜄ;

		// Token: 0x0400090C RID: 2316
		private CellFormat ᜅ;

		// Token: 0x0400090D RID: 2317
		private string ᜆ;

		// Token: 0x0400090E RID: 2318
		private string ᜇ;

		// Token: 0x0400090F RID: 2319
		private string ᜈ;

		// Token: 0x04000910 RID: 2320
		private XlsHyperlinkStyle ᜉ;
	}
}
