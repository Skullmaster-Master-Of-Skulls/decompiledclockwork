using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.PropEditors;
using Spire.DataExport.TypeConverters;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001DD RID: 477
	[TypeConverter(typeof(CollectionTypeConverter))]
	public class Cell : CustomItem, ICloneable
	{
		// Token: 0x06000E63 RID: 3683 RVA: 0x0009FED0 File Offset: 0x0009EED0
		public Cell()
		{
			int a_ = 8;
			this.ᜁ = CellType.String;
			this.ᜄ = string.Format(HyperlinksCollectionEditor.b("弣ᘥ唧਩圫Ἥ䴯", a_), spr\u1C2B.ᡙ, spr\u1C2B.ᡚ);
			this.ᜅ = HyperlinksCollectionEditor.b("ܣਥଧऩ༫ȭጯᄱгᠵ࠷ਹ", a_);
			this.ᜆ = new CellFormat();
			this.ᜈ = DateTime.Today;
			this.ᜊ = string.Empty;
			this.ᜋ = spr\u1C2B.ᡝ;
			base..ctor();
			this.SetName(this.ᜀ(this.ᜂ, this.ᜃ));
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0009FF70 File Offset: 0x0009EF70
		protected override void Dispose(bool Disposing)
		{
			while (!this.ᜀ)
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
					try
					{
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_8C;
							case 1:
								this.ᜆ.Dispose();
								num = 3;
								continue;
							case 3:
								goto IL_7D;
							}
							if (Disposing)
							{
								num = 1;
								continue;
							}
							IL_7D:
							this.ᜀ = true;
							num = 0;
						}
						IL_8C:;
					}
					finally
					{
						base.Dispose(Disposing);
					}
					return;
				}
			}
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x000A0024 File Offset: 0x0009F024
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
			return new Cell
			{
				CellType = this.CellType,
				Column = this.Column,
				DateTimeFormat = this.DateTimeFormat,
				Format = this.Format,
				NumericFormat = this.NumericFormat,
				Row = this.Row,
				Value = this.Value
			};
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x000A00BC File Offset: 0x0009F0BC
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

		// Token: 0x06000E67 RID: 3687 RVA: 0x000A00F8 File Offset: 0x0009F0F8
		private string ᜀ(int A_0, int A_1)
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
			return string.Format(HyperlinksCollectionEditor.b("株䈬䌮ରጲ临ܶ䐸ᬺ漼倾㙀祂敄㱆硈㙊浌", a_), A_0, A_1);
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x000A015C File Offset: 0x0009F15C
		public bool IsCorrect()
		{
			while (this.ᜂ <= 0)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return false;
			}
			return this.ᜃ > 0;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x000A01B0 File Offset: 0x0009F1B0
		private bool ᜀ(object A_0)
		{
			int a_ = 15;
			try
			{
				for (;;)
				{
					CellType cellType = this.ᜁ;
					int num = 8;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_112;
						case 1:
							num = 4;
							continue;
						case 2:
							goto IL_112;
						case 3:
							goto IL_11D;
						case 4:
							goto IL_112;
						case 5:
							goto IL_B3;
						case 6:
							goto IL_112;
						case 7:
							goto IL_112;
						case 8:
							switch (cellType)
							{
							case CellType.Boolean:
								Convert.ToBoolean(A_0);
								num = 0;
								continue;
							case CellType.DateTime:
								Convert.ToDateTime(A_0);
								num = 7;
								continue;
							case CellType.Numeric:
								Convert.ToDouble(A_0);
								num = 6;
								continue;
							case CellType.String:
								Convert.ToString(A_0);
								num = 2;
								continue;
							case CellType.Formula:
							{
								string text = Convert.ToString(A_0);
								num = 9;
								continue;
							}
							default:
								num = 1;
								continue;
							}
							break;
						case 9:
						{
							string text;
							if (!text.StartsWith(HyperlinksCollectionEditor.b("ᘪ", a_)))
							{
								num = 5;
								continue;
							}
							goto IL_112;
						}
						}
						break;
						IL_112:
						num = 3;
					}
				}
				IL_B3:
				throw new Exception(HyperlinksCollectionEditor.b("洪䈬崮尰䘲头嘶ᤸ帺䔼伾㍀♂㙄㑆楈♊㡌㱎═獒♔⍖㡘⥚⥜罞ᙠ੢ᅤས䥨䱪偬䡮彰", a_));
				IL_11D:;
			}
			catch
			{
				return false;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				bool result;
				return result;
			}
			default:
				if (false)
				{
				}
				return true;
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000A0330 File Offset: 0x0009F330
		private void ᜀ()
		{
			for (;;)
			{
				CellType cellType = this.ᜁ;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9E;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch (cellType)
							{
							case CellType.Boolean:
								goto IL_79;
							case CellType.DateTime:
								goto IL_6D;
							case CellType.Numeric:
								goto IL_8E;
							default:
								num = 1;
								continue;
							}
							break;
						case 1:
							num = 2;
							continue;
						case 2:
							goto IL_8C;
						}
						break;
					}
					break;
				}
				}
			}
			IL_6D:
			this.ᜈ = DateTime.Now;
			return;
			IL_79:
			this.ᜇ = false;
			return;
			IL_8C:
			goto IL_9E;
			IL_8E:
			this.ᜉ = 0.0;
			return;
			IL_9E:
			this.ᜊ = string.Empty;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x000A03E8 File Offset: 0x0009F3E8
		public void SaveToXmlFile(XMLFile File, string Section)
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
			string key = HyperlinksCollectionEditor.b("氮吰弲头挶䀸䬺堼", a_);
			int num = (int)this.ᜁ;
			File.WriteValue(Section, key, num.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("氮帰弲䀴娶圸", a_), this.ᜂ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("紮帰䐲", a_), this.ᜃ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("欮倰䜲倴挶倸嘺堼社⹀ㅂ⡄♆㵈", a_), this.ᜄ);
			File.WriteValue(Section, HyperlinksCollectionEditor.b("愮䐰帲倴䔶倸堺笼倾㍀⹂⑄㍆", a_), this.ᜅ);
			File.WriteValue(Section, HyperlinksCollectionEditor.b("洮帰尲头制堸唺欼帾ⵀ㙂⁄", a_), this.ᜇ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("瘮吰刲䜴", a_), this.ᜈ.Year.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("戮帰崲䄴弶", a_), this.ᜈ.Month.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("欮倰䨲", a_), this.ᜈ.Day.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("朮帰䘲䜴", a_), this.ᜈ.Hour.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("戮堰崲", a_), this.ᜈ.Minute.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("簮吰倲", a_), this.ᜈ.Second.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("戮戰嘲嘴", a_), this.ᜈ.Millisecond.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("愮䐰帲倴䔶倸堺欼帾ⵀ㙂⁄", a_), this.ᜉ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("簮䔰䄲尴夶常洺尼匾㑀♂", a_), this.ᜊ);
			this.ᜆ.SaveToXmlFile(File, Section);
			File.SaveToFile();
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x000A0650 File Offset: 0x0009F650
		public void LoadFromXmlFile(XMLFile File, string Section)
		{
			int a_ = 12;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			string key = HyperlinksCollectionEditor.b("欧伩䀫䈭搯䬱䐳匵", a_);
			int num = (int)this.ᜁ;
			this.ᜁ = (CellType)Convert.ToInt32(File.ReadValue(Section, key, num.ToString()));
			this.ᜂ = (int)Convert.ToUInt16(File.ReadValue(Section, HyperlinksCollectionEditor.b("欧䔩䀫嬭崯就", a_), this.ᜂ.ToString()));
			this.ᜃ = (int)Convert.ToUInt16(File.ReadValue(Section, HyperlinksCollectionEditor.b("稧䔩嬫", a_), this.ᜃ.ToString()));
			this.ᜄ = File.ReadValue(Section, HyperlinksCollectionEditor.b("氧䬩堫䬭搯嬱夳匵縷唹主匽ℿ㙁", a_), this.ᜄ);
			this.ᜅ = File.ReadValue(Section, HyperlinksCollectionEditor.b("昧弩䄫䬭䈯嬱圳瀵圷䠹儻弽㐿", a_), this.ᜅ);
			this.ᜇ = Convert.ToBoolean(File.ReadValue(Section, HyperlinksCollectionEditor.b("樧䔩䌫䈭唯匱娳怵夷嘹䤻嬽", a_), this.ᜇ.ToString()));
			int year = Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("焧伩䴫尭", a_), this.ᜈ.Year.ToString()));
			int month = Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("攧䔩䈫娭堯", a_), this.ᜈ.Month.ToString()));
			int day = Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("氧䬩唫", a_), this.ᜈ.Day.ToString()));
			int hour = Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("性䔩夫尭", a_), this.ᜈ.Hour.ToString()));
			int minute = Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("攧䌩䈫", a_), this.ᜈ.Minute.ToString()));
			int second = Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("笧伩伫", a_), this.ᜈ.Second.ToString()));
			int millisecond = Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("攧礩䤫䴭", a_), this.ᜈ.Millisecond.ToString()));
			this.ᜈ = new DateTime(year, month, day, hour, minute, second, millisecond);
			this.ᜉ = double.Parse(File.ReadValue(Section, HyperlinksCollectionEditor.b("昧弩䄫䬭䈯嬱圳怵夷嘹䤻嬽", a_), this.ᜉ.ToString()), NumberStyles.Any);
			this.ᜊ = File.ReadValue(Section, HyperlinksCollectionEditor.b("笧帩師䜭帯唱戳圵吷伹夻", a_), this.ᜊ);
			this.ᜆ.LoadFromXmlFile(File, Section);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x000A0948 File Offset: 0x0009F948
		public bool ShouldSerializeDateTimeFormat()
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
			return string.Compare(this.ᜄ, spr\u1C2B.ᡛ) != 0;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x000A099C File Offset: 0x0009F99C
		public void ResetDateTimeFormat()
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
			this.ᜄ = spr\u1C2B.ᡛ;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x000A09E4 File Offset: 0x0009F9E4
		public bool ShouldSerializeCultureName()
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
			return string.Compare(this.ᜋ, spr\u1C2B.ᡝ) != 0;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x000A0A38 File Offset: 0x0009FA38
		public void ResetCultureName()
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
			this.ᜋ = spr\u1C2B.ᡝ;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x000A0A80 File Offset: 0x0009FA80
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
				return ItemType.Cell;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x000A0AC0 File Offset: 0x0009FAC0
		[Browsable(false)]
		public bool IsBoolean
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
				return this.ᜁ == CellType.Boolean;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x000A0B04 File Offset: 0x0009FB04
		[Browsable(false)]
		public bool IsDateTime
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
				return this.ᜁ == CellType.DateTime;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x000A0B48 File Offset: 0x0009FB48
		[Browsable(false)]
		public bool IsNumeric
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
				return this.ᜁ == CellType.Numeric;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x000A0B8C File Offset: 0x0009FB8C
		[Browsable(false)]
		public bool IsString
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
				return this.ᜁ == CellType.String;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x000A0BD0 File Offset: 0x0009FBD0
		[Browsable(false)]
		public bool IsFormula
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
				return this.ᜁ == CellType.Formula;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x000A0C14 File Offset: 0x0009FC14
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x000A0C58 File Offset: 0x0009FC58
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(CellType.String)]
		[Description("Gets the type of the cell value.")]
		[RefreshProperties(RefreshProperties.All)]
		public CellType CellType
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
				return this.ᜁ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_52;
					case 1:
						this.ᜁ = value;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					if (value != this.ᜁ)
					{
						num = 1;
						continue;
					}
					IL_52:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_68;
					}
				}
				IL_68:
				if (false)
				{
				}
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x000A0CD4 File Offset: 0x0009FCD4
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x000A0D18 File Offset: 0x0009FD18
		[DefaultValue(0)]
		[Description("Gets the horizontal position of the cell in the result Excel document.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int Column
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
				return this.ᜂ;
			}
			set
			{
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜂ = value;
						num = 2;
						continue;
					case 2:
						goto IL_52;
					}
					if (value != this.ᜂ)
					{
						num = 1;
						continue;
					}
					IL_52:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_68;
					}
				}
				IL_68:
				if (false)
				{
				}
				this.SetName(this.ᜀ(this.ᜂ, this.ᜃ));
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x000A0DAC File Offset: 0x0009FDAC
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x000A0DF0 File Offset: 0x0009FDF0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(0)]
		[Description("Gets or sets the vertical position of the cell in the result Excel document.")]
		public int Row
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜃ = value;
						num = 2;
						continue;
					case 2:
						goto IL_4A;
					}
					if (value != this.ᜃ)
					{
						num = 1;
						continue;
					}
					IL_4A:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_60;
					}
				}
				IL_60:
				if (false)
				{
				}
				if (true)
				{
				}
				this.SetName(this.ᜀ(this.ᜂ, this.ᜃ));
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x000A0E84 File Offset: 0x0009FE84
		// (set) Token: 0x06000E7E RID: 3710 RVA: 0x000A0EC8 File Offset: 0x0009FEC8
		[Editor(typeof(CellDateTimeFormatEditor), typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.All)]
		[Description("Gets the formatting string of the date/time values.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string DateTimeFormat
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
						return;
					case 1:
						this.ᜄ = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1C;
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					IL_1C:
					if (true)
					{
					}
					if (!(value != this.ᜄ))
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x000A0F48 File Offset: 0x0009FF48
		// (set) Token: 0x06000E80 RID: 3712 RVA: 0x000A0F8C File Offset: 0x0009FF8C
		[DefaultValue("#,###,##0.00")]
		[Description("Gets or sets the formatting string for the numeric values.")]
		[RefreshProperties(RefreshProperties.All)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string NumericFormat
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
					switch (num)
					{
					case 0:
						return;
					case 2:
						this.ᜅ = value;
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
							num = 0;
							continue;
						}
						break;
					}
					if (!(value != this.ᜅ))
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x000A100C File Offset: 0x000A000C
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x000A1050 File Offset: 0x000A0050
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets or sets the formatting options for the cell in the Excel document.")]
		public CellFormat Format
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
				return this.ᜆ;
			}
			set
			{
				for (;;)
				{
					IL_00:
					if (true)
					{
					}
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
								goto IL_00;
							default:
								if (false)
								{
								}
								num = 3;
								continue;
							}
							break;
						case 2:
							this.ᜆ = value;
							num = 4;
							continue;
						case 3:
							if (value != this.ᜆ)
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
							return;
						}
						num = 1;
					}
				}
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x000A10E8 File Offset: 0x000A00E8
		// (set) Token: 0x06000E84 RID: 3716 RVA: 0x000A1198 File Offset: 0x000A0198
		[TypeConverter(typeof(CellValueTypeConverter))]
		[Description("Gets or sets the value of the cell.")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public object Value
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5B:
					num = 0;
					break;
				default:
					if (false)
					{
					}
					goto IL_3A;
				}
				for (;;)
				{
					IL_28:
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						goto IL_8D;
					case 2:
						goto IL_49;
					}
					goto IL_3A;
				}
				IL_49:
				CellType cellType;
				switch (cellType)
				{
				case CellType.Boolean:
					return this.ᜇ;
				case CellType.DateTime:
					if (true)
					{
					}
					return this.ᜈ;
				case CellType.Numeric:
					return this.ᜉ;
				}
				goto IL_5B;
				IL_8D:
				return this.ᜊ;
				IL_3A:
				cellType = this.ᜁ;
				num = 2;
				goto IL_28;
			}
			set
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
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							CellType cellType;
							switch (cellType)
							{
							case CellType.Boolean:
								goto IL_BE;
							case CellType.DateTime:
								goto IL_65;
							case CellType.Numeric:
								goto IL_83;
							default:
								num = 3;
								continue;
							}
							break;
						}
						case 1:
							goto IL_59;
						case 3:
							num = 4;
							continue;
						case 4:
							goto IL_7A;
						}
						if (true)
						{
						}
						if (!this.ᜀ(value))
						{
							num = 1;
						}
						else
						{
							CellType cellType = this.ᜁ;
							num = 0;
						}
					}
					IL_65:
					this.ᜈ = Convert.ToDateTime(value);
					return;
					IL_7A:
					this.ᜊ = Convert.ToString(value);
					return;
					IL_83:
					this.ᜉ = Convert.ToDouble(value);
					return;
					IL_BE:
					this.ᜇ = Convert.ToBoolean(value);
					return;
				}
				}
				IL_59:
				this.ᜀ();
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x000A127C File Offset: 0x000A027C
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x000A12C0 File Offset: 0x000A02C0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Editor(typeof(CultureEditor), typeof(UITypeEditor))]
		[Description("Gets or sets the culture name.")]
		public string CultureName
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
				return this.ᜋ;
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
					case 2:
						if (true)
						{
						}
						this.ᜋ = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1C;
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					IL_1C:
					if (!(value != this.ᜋ))
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x000A1340 File Offset: 0x000A0340
		[Browsable(false)]
		public string DisplayName
		{
			get
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
				return string.Format(HyperlinksCollectionEditor.b("測䨮崰弲ᔴἶ稸吺儼Ծ慀㡂畄㩆楈᥊≌㡎歐獒⹔杖⑘牚", a_), this.ᜂ, this.ᜃ);
			}
		}

		// Token: 0x04000AF1 RID: 2801
		private long \u2609\u00A7\u008F\u009A;

		// Token: 0x04000AF2 RID: 2802
		private bool ᜀ;

		// Token: 0x04000AF3 RID: 2803
		private CellType ᜁ;

		// Token: 0x04000AF4 RID: 2804
		private int ᜂ;

		// Token: 0x04000AF5 RID: 2805
		private int \u25D8\u0093\u0087\u00AB;

		// Token: 0x04000AF6 RID: 2806
		private int ᜃ;

		// Token: 0x04000AF7 RID: 2807
		private string[] \u2609\u0096\u00A7\u0096;

		// Token: 0x04000AF8 RID: 2808
		private bool \u25D9\u0088\u0087\u008E;

		// Token: 0x04000AF9 RID: 2809
		private string ᜄ;

		// Token: 0x04000AFA RID: 2810
		private string ᜅ;

		// Token: 0x04000AFB RID: 2811
		private CellFormat ᜆ;

		// Token: 0x04000AFC RID: 2812
		private int \u2460\u00A2\u00A3\u0084;

		// Token: 0x04000AFD RID: 2813
		private bool ᜇ;

		// Token: 0x04000AFE RID: 2814
		private DateTime ᜈ;

		// Token: 0x04000AFF RID: 2815
		private double ᜉ;

		// Token: 0x04000B00 RID: 2816
		private string ᜊ;

		// Token: 0x04000B01 RID: 2817
		private string ᜋ;
	}
}
