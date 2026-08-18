using System;
using System.ComponentModel;
using System.Drawing.Design;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.PropEditors;
using Spire.DataExport.XLS;

namespace Spire.DataExport.Common
{
	// Token: 0x02000162 RID: 354
	public class FormatsExport : ICloneable
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x0005A54C File Offset: 0x0005954C
		public FormatsExport(object Owner)
		{
			int a_ = 8;
			this.ᜁ = HyperlinksCollectionEditor.b("䌣", a_);
			this.ᜂ = HyperlinksCollectionEditor.b("䌣", a_);
			this.ᜃ = spr\u1C2B.ᡚ;
			this.ᜄ = spr\u1C2B.ᡛ;
			this.ᜅ = HyperlinksCollectionEditor.b("䜣", a_);
			this.ᜆ = HyperlinksCollectionEditor.b("倣吥崧伩", a_);
			this.ᜇ = HyperlinksCollectionEditor.b("䈣䜥䐧天䤫", a_);
			this.ᜈ = HyperlinksCollectionEditor.b("䨣匥䐧䘩", a_);
			this.ᜉ = spr\u1C2B.ᡝ;
			base..ctor();
			this.ᜀ = Owner;
			this.ResetFormats(Owner);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0005A610 File Offset: 0x00059610
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
			return new FormatsExport(this.ᜀ)
			{
				Integer = this.Integer,
				Float = this.Float,
				Time = this.Time,
				DateTime = this.DateTime,
				Currency = this.Currency,
				BooleanTrue = this.BooleanTrue,
				BooleanFalse = this.BooleanFalse,
				NullString = this.NullString
			};
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0005A6B8 File Offset: 0x000596B8
		public void ResetFormats(object Owner)
		{
			int a_ = 18;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_149;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 2:
					goto IL_147;
				case 3:
					goto IL_4C;
				case 4:
					num = 6;
					continue;
				case 5:
					if (Owner is WorkSheet)
					{
						num = 3;
						continue;
					}
					goto IL_100;
				case 6:
					if (!(Owner is CellExport))
					{
						num = 0;
						continue;
					}
					goto IL_4C;
				case 7:
					goto IL_8F;
				}
				if (Owner != null)
				{
					num = 4;
					continue;
				}
				goto IL_100;
				IL_4C:
				this.ᜁ = HyperlinksCollectionEditor.b("ഭᰯᄱᜳᔵᐷ᤹Ἳ฽", a_);
				this.ᜂ = HyperlinksCollectionEditor.b("ഭᰯᄱᜳᔵᐷ᤹Ἳ฽渿牁瑃", a_);
				this.ᜅ = spr\u1C2B.ᜀ();
				if (true)
				{
				}
				num = 7;
				continue;
				IL_100:
				this.ᜁ = HyperlinksCollectionEditor.b("䤭", a_);
				this.ᜂ = HyperlinksCollectionEditor.b("䤭", a_);
				this.ᜅ = HyperlinksCollectionEditor.b("䴭", a_);
				num = 2;
			}
			IL_8F:
			IL_147:
			IL_149:
			this.ᜃ = spr\u1C2B.ᡚ;
			this.ᜄ = spr\u1C2B.ᡛ;
			this.ᜆ = HyperlinksCollectionEditor.b("娭䈯䜱儳", a_);
			this.ᜇ = HyperlinksCollectionEditor.b("䠭儯帱䜳匵", a_);
			this.ᜈ = HyperlinksCollectionEditor.b("䀭䔯帱堳", a_);
			this.ᜉ = spr\u1C2B.ᡝ;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0005A86C File Offset: 0x0005986C
		private bool ᜋ()
		{
			int a_ = 6;
			int num = 4;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_DE;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						if (this.ᜀ is WorkSheet)
						{
							num = 3;
							continue;
						}
						goto IL_DE;
					case 1:
						num = 0;
						continue;
					case 2:
						num = 5;
						continue;
					case 3:
						goto IL_AF;
					case 5:
						if (true)
						{
						}
						if (!(this.ᜀ is CellExport))
						{
							num = 1;
							continue;
						}
						goto IL_6C;
					}
					if (this.ᜀ == null)
					{
						goto IL_DE;
					}
					num = 2;
					break;
				}
			}
			IL_6C:
			return string.Compare(this.ᜁ, HyperlinksCollectionEditor.b("ġࠣԥଧऩ+ഭጯȱ", a_)) != 0;
			IL_AF:
			goto IL_6C;
			IL_DE:
			return string.Compare(this.ᜁ, HyperlinksCollectionEditor.b("䔡", a_)) != 0;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0005A978 File Offset: 0x00059978
		private void ᜊ()
		{
			int a_ = 0;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 2:
					goto IL_89;
				case 3:
					goto IL_A1;
				case 4:
					if (true)
					{
					}
					if (!(this.ᜀ is CellExport))
					{
						num = 0;
						continue;
					}
					goto IL_6C;
				case 5:
					num = 4;
					continue;
				}
				if (this.ᜀ != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_89;
					}
					if (false)
					{
					}
					num = 5;
					continue;
				}
				goto IL_D0;
				IL_89:
				if (!(this.ᜀ is WorkSheet))
				{
					goto IL_D0;
				}
				num = 3;
			}
			IL_6C:
			this.ᜁ = HyperlinksCollectionEditor.b("㼛㈝̟ġܣਥଧऩᰫ", a_);
			return;
			IL_A1:
			goto IL_6C;
			IL_D0:
			this.ᜁ = HyperlinksCollectionEditor.b("笛", a_);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0005AA6C File Offset: 0x00059A6C
		private bool ᜉ()
		{
			int a_ = 9;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!(this.ᜀ is CellExport))
					{
						num = 3;
						continue;
					}
					goto IL_6C;
				case 1:
					goto IL_B7;
				case 3:
					num = 5;
					continue;
				case 4:
					num = 0;
					continue;
				case 5:
					goto IL_97;
				}
				if (this.ᜀ != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_97;
					}
					if (false)
					{
					}
					num = 4;
					continue;
				}
				goto IL_DE;
				IL_97:
				if (!(this.ᜀ is WorkSheet))
				{
					goto IL_DE;
				}
				if (true)
				{
				}
				num = 1;
			}
			IL_6C:
			return string.Compare(this.ᜂ, HyperlinksCollectionEditor.b("ؤଦਨࠪฬ̮ሰဲԴᤶस଺", a_)) != 0;
			IL_B7:
			goto IL_6C;
			IL_DE:
			return string.Compare(this.ᜂ, HyperlinksCollectionEditor.b("䈤", a_)) != 0;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0005AB78 File Offset: 0x00059B78
		private void ᜈ()
		{
			int a_ = 2;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!(this.ᜀ is CellExport))
					{
						num = 3;
						continue;
					}
					goto IL_74;
				case 1:
					goto IL_A9;
				case 3:
					num = 4;
					continue;
				case 4:
					goto IL_91;
				case 5:
					num = 0;
					continue;
				}
				if (this.ᜀ != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_91;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					num = 5;
					continue;
				}
				goto IL_D0;
				IL_91:
				if (!(this.ᜀ is WorkSheet))
				{
					goto IL_D0;
				}
				num = 1;
			}
			IL_74:
			this.ᜂ = HyperlinksCollectionEditor.b("㴝టġܣԥЧऩ༫ḭḯȱг", a_);
			return;
			IL_A9:
			goto IL_74;
			IL_D0:
			this.ᜂ = HyperlinksCollectionEditor.b("礝", a_);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0005AC6C File Offset: 0x00059C6C
		private bool ᜇ()
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
			return string.Compare(this.ᜃ, spr\u1C2B.ᡚ) != 0;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0005ACC0 File Offset: 0x00059CC0
		private void ᜆ()
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
			this.ᜃ = spr\u1C2B.ᡚ;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0005AD08 File Offset: 0x00059D08
		private bool ᜅ()
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
			return string.Compare(this.ᜄ, spr\u1C2B.ᡛ) != 0;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0005AD5C File Offset: 0x00059D5C
		private void ᜄ()
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
			this.ᜄ = spr\u1C2B.ᡛ;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0005ADA4 File Offset: 0x00059DA4
		private bool ᜃ()
		{
			int a_ = 19;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_AB;
				case 1:
					num = 3;
					continue;
				case 2:
					num = 4;
					continue;
				case 3:
					goto IL_93;
				case 4:
					if (!(this.ᜀ is CellExport))
					{
						num = 1;
						continue;
					}
					goto IL_74;
				case 5:
					if (true)
					{
					}
					break;
				}
				if (this.ᜀ == null)
				{
					goto IL_D2;
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
				IL_93:
				if (!(this.ᜀ is WorkSheet))
				{
					goto IL_D2;
				}
				num = 0;
			}
			IL_74:
			return string.Compare(this.ᜅ, spr\u1C2B.ᜀ()) != 0;
			IL_AB:
			goto IL_74;
			IL_D2:
			return string.Compare(this.ᜅ, HyperlinksCollectionEditor.b("䰮", a_)) != 0;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0005AEA4 File Offset: 0x00059EA4
		private void ᜂ()
		{
			int a_ = 16;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (!(this.ᜀ is CellExport))
					{
						num = 4;
						continue;
					}
					goto IL_74;
				case 2:
					num = 1;
					continue;
				case 3:
					goto IL_88;
				case 4:
					num = 3;
					continue;
				case 5:
					goto IL_A0;
				}
				if (this.ᜀ != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_88;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					num = 2;
					continue;
				}
				goto IL_C7;
				IL_88:
				if (!(this.ᜀ is WorkSheet))
				{
					goto IL_C7;
				}
				num = 5;
			}
			IL_74:
			this.ᜅ = spr\u1C2B.ᜀ();
			return;
			IL_A0:
			goto IL_74;
			IL_C7:
			this.ᜅ = HyperlinksCollectionEditor.b("伫", a_);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0005AF8C File Offset: 0x00059F8C
		private bool ᜁ()
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
			return string.Compare(this.ᜉ, spr\u1C2B.ᡝ) != 0;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0005AFE0 File Offset: 0x00059FE0
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
			this.ᜉ = spr\u1C2B.ᡝ;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0005B028 File Offset: 0x0005A028
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x0005B06C File Offset: 0x0005A06C
		[Description("Determines the representation of integer fields in the result file.")]
		[Editor(typeof(IntegerFormatEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string Integer
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
			set
			{
				int a_ = 12;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜀ != null)
						{
							num = 1;
							continue;
						}
						goto IL_D3;
					case 1:
						goto IL_D1;
					case 2:
						if (true)
						{
						}
						break;
					case 3:
						num = 4;
						continue;
					case 4:
						if (this.ᜀ is WorkSheet)
						{
							num = 6;
							continue;
						}
						goto IL_D3;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D1;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 6:
						goto IL_B1;
					case 7:
						if (!(this.ᜀ is CellExport))
						{
							num = 3;
							continue;
						}
						goto IL_7C;
					}
					if (value.Length == 0)
					{
						num = 5;
						continue;
					}
					goto IL_110;
					IL_D1:
					num = 7;
				}
				IL_7C:
				this.ᜁ = HyperlinksCollectionEditor.b("ଧة༫ഭጯḱᜳᔵ࠷", a_);
				return;
				IL_B1:
				goto IL_7C;
				IL_D3:
				this.ᜁ = HyperlinksCollectionEditor.b("伧", a_);
				return;
				IL_110:
				this.ᜁ = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0005B190 File Offset: 0x0005A190
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x0005B1D4 File Offset: 0x0005A1D4
		[Editor(typeof(CurrencyFormatEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the float columns in result file.")]
		public string Float
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
				int a_ = 4;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D4;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 1:
						if (!(this.ᜀ is CellExport))
						{
							num = 4;
							continue;
						}
						goto IL_7C;
					case 2:
						goto IL_B4;
					case 3:
						if (this.ᜀ != null)
						{
							num = 7;
							continue;
						}
						goto IL_D6;
					case 4:
						num = 6;
						continue;
					case 6:
						if (this.ᜀ is WorkSheet)
						{
							num = 2;
							continue;
						}
						goto IL_D6;
					case 7:
						goto IL_D4;
					}
					if (value.Length == 0)
					{
						num = 0;
						continue;
					}
					goto IL_116;
					IL_D4:
					num = 1;
				}
				IL_7C:
				this.ᜂ = HyperlinksCollectionEditor.b("̟มܣԥଧة༫ഭ/ᰱгص", a_);
				return;
				IL_B4:
				goto IL_7C;
				IL_D6:
				this.ᜂ = HyperlinksCollectionEditor.b("䜟", a_);
				return;
				IL_116:
				this.ᜂ = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0005B300 File Offset: 0x0005A300
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0005B344 File Offset: 0x0005A344
		[Description("Gets or sets the time columns in result file.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Editor(typeof(TimeFormatEditor), typeof(UITypeEditor))]
		public string Time
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
					if (value.Length == 0)
					{
						this.ᜃ = spr\u1C2B.ᡚ;
						return;
					}
					break;
				}
				this.ᜃ = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0005B39C File Offset: 0x0005A39C
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0005B3E0 File Offset: 0x0005A3E0
		[Description("Gets or sets the datetime columns in result file.")]
		[Editor(typeof(DateTimeFormatEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string DateTime
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
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (value.Length == 0)
					{
						if (true)
						{
						}
						this.ᜄ = spr\u1C2B.ᡛ;
						return;
					}
					break;
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0005B438 File Offset: 0x0005A438
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x0005B47C File Offset: 0x0005A47C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Editor(typeof(CurrencyFormatEditor), typeof(UITypeEditor))]
		[Description("Gets or sets the currency columns in result file.")]
		public string Currency
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
				int a_ = 13;
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
							goto IL_C0;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					case 1:
						goto IL_A0;
					case 3:
						goto IL_C0;
					case 4:
						if (!(this.ᜀ is CellExport))
						{
							num = 7;
							continue;
						}
						goto IL_74;
					case 5:
						if (this.ᜀ is WorkSheet)
						{
							num = 1;
							continue;
						}
						goto IL_C2;
					case 6:
						if (this.ᜀ != null)
						{
							num = 3;
							continue;
						}
						goto IL_C2;
					case 7:
						num = 5;
						continue;
					}
					if (value.Length == 0)
					{
						num = 0;
						continue;
					}
					goto IL_10A;
					IL_C0:
					num = 4;
				}
				IL_74:
				this.ᜅ = spr\u1C2B.ᜀ();
				return;
				IL_A0:
				goto IL_74;
				IL_C2:
				if (true)
				{
				}
				this.ᜅ = HyperlinksCollectionEditor.b("䨨", a_);
				return;
				IL_10A:
				this.ᜅ = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0005B59C File Offset: 0x0005A59C
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x0005B5E0 File Offset: 0x0005A5E0
		[DefaultValue("true")]
		[Description("Gets or sets the boolean columns in result file.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string BooleanTrue
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
				return this.ᜆ;
			}
			set
			{
				int a_ = 1;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (value.Length == 0)
					{
						this.ᜆ = HyperlinksCollectionEditor.b("検洞吠䘢", a_);
						return;
					}
					break;
				}
				if (true)
				{
				}
				this.ᜆ = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x0005B64C File Offset: 0x0005A64C
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x0005B690 File Offset: 0x0005A690
		[Description("Gets or sets the True value of the source boolean columns in result file.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("false")]
		public string BooleanFalse
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
				int a_ = 18;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (value.Length == 0)
					{
						if (true)
						{
						}
						this.ᜇ = HyperlinksCollectionEditor.b("䠭儯帱䜳匵", a_);
						return;
					}
					break;
				}
				this.ᜇ = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x0005B6FC File Offset: 0x0005A6FC
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x0005B740 File Offset: 0x0005A740
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the Null value in result file.")]
		[DefaultValue("null")]
		public string NullString
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
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜈ = value.Trim();
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x0005B788 File Offset: 0x0005A788
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x0005B7CC File Offset: 0x0005A7CC
		[Editor(typeof(CultureEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string CultureName
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (true)
						{
						}
						this.ᜉ = value;
						num = 2;
						continue;
					case 2:
						return;
					}
					if (!(value != this.ᜉ))
					{
						break;
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
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x040006EA RID: 1770
		private int \u2460\u00AE\u0085\u0089;

		// Token: 0x040006EB RID: 1771
		private object ᜀ;

		// Token: 0x040006EC RID: 1772
		private string ᜁ;

		// Token: 0x040006ED RID: 1773
		private string ᜂ;

		// Token: 0x040006EE RID: 1774
		private string ᜃ;

		// Token: 0x040006EF RID: 1775
		private string ᜄ;

		// Token: 0x040006F0 RID: 1776
		private string ᜅ;

		// Token: 0x040006F1 RID: 1777
		private string ᜆ;

		// Token: 0x040006F2 RID: 1778
		private string ᜇ;

		// Token: 0x040006F3 RID: 1779
		private float \u2593\u009C\u009F\u008F;

		// Token: 0x040006F4 RID: 1780
		private string ᜈ;

		// Token: 0x040006F5 RID: 1781
		private string ᜉ;
	}
}
