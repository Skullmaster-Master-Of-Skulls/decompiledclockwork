using System;
using System.ComponentModel;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.TypeConverters;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001B0 RID: 432
	[TypeConverter(typeof(CollectionTypeConverter))]
	public class MergedCells : CustomItem, ICloneable
	{
		// Token: 0x06000C22 RID: 3106 RVA: 0x0007FD98 File Offset: 0x0007ED98
		public MergedCells()
		{
			this.SetName(this.ᜀ(this.ᜂ, this.ᜁ, this.ᜂ, this.ᜃ));
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0007FDD0 File Offset: 0x0007EDD0
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
			return new MergedCells
			{
				StartCol = this.StartCol,
				StartRow = this.StartRow,
				EndCol = this.EndCol,
				EndRow = this.EndRow
			};
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0007FE44 File Offset: 0x0007EE44
		internal override void InitCollectionItem()
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

		// Token: 0x06000C25 RID: 3109 RVA: 0x0007FE80 File Offset: 0x0007EE80
		private string ᜀ(int A_0, int A_1, int A_2, int A_3)
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
			return string.Format(HyperlinksCollectionEditor.b("昪䠬崮嘰嘲儴᜶稸帺儼匾㉀捂浄㱆祈㙊慌潎⩐扒⡔策祘⁚潜≞䵠䍢Ṥ呦ᑨ䉪", a_), new object[]
			{
				A_0,
				A_1,
				A_2,
				A_3
			});
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0007FF04 File Offset: 0x0007EF04
		public bool IsCorrect()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_31;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 1:
					num = 5;
					continue;
				case 2:
					if (this.ᜂ > 0)
					{
						num = 4;
						continue;
					}
					return false;
				case 4:
					goto IL_68;
				case 5:
					if (this.ᜁ > 0)
					{
						num = 0;
						continue;
					}
					return false;
				}
				goto IL_28;
				IL_31:
				num = 1;
				continue;
				IL_28:
				if (this.ᜀ > 0)
				{
					goto IL_31;
				}
				return false;
			}
			IL_68:
			return this.ᜃ > 0;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0007FFBC File Offset: 0x0007EFBC
		public void SaveToXmlFile(XMLFile File, string Section)
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
			File.WriteValue(Section, HyperlinksCollectionEditor.b("樫䜭䈯䄱䀳电圷嘹", a_), this.ᜀ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("樫䜭䈯䄱䀳搵圷䴹", a_), this.ᜁ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("怫伭䌯䘱眳夵吷", a_), this.ᜂ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("怫伭䌯䘱昳夵伷", a_), this.ᜃ.ToString());
			File.SaveToFile();
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00080088 File Offset: 0x0007F088
		public void LoadFromXmlFile(XMLFile File, string Section)
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
			this.ᜀ = (int)Convert.ToUInt16(File.ReadValue(Section, HyperlinksCollectionEditor.b("搡䴣吥嬧帩漫䄭尯", a_), this.ᜀ.ToString()));
			this.ᜁ = (int)Convert.ToUInt16(File.ReadValue(Section, HyperlinksCollectionEditor.b("搡䴣吥嬧帩縫䄭䜯", a_), this.ᜁ.ToString()));
			this.ᜂ = (int)Convert.ToUInt16(File.ReadValue(Section, HyperlinksCollectionEditor.b("渡䔣唥尧椩䌫䈭", a_), this.ᜂ.ToString()));
			this.ᜃ = (int)Convert.ToUInt16(File.ReadValue(Section, HyperlinksCollectionEditor.b("渡䔣唥尧砩䌫夭", a_), this.ᜃ.ToString()));
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00080178 File Offset: 0x0007F178
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
				return ItemType.MergedCells;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x000801B8 File Offset: 0x0007F1B8
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x000801FC File Offset: 0x0007F1FC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the first column of the cell range to merge.")]
		[DefaultValue(0)]
		public int StartCol
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
				return this.ᜀ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6C;
					case 1:
						this.ᜀ = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6C;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					if (true)
					{
					}
					if (value == this.ᜀ)
					{
						break;
					}
					num = 1;
				}
				IL_6C:
				this.SetName(this.ᜀ(this.ᜀ, this.ᜁ, this.ᜂ, this.ᜃ));
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0008029C File Offset: 0x0007F29C
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x000802E0 File Offset: 0x0007F2E0
		[Description("Gets or sets the first row of the cell range to merge.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(0)]
		public int StartRow
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
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6C;
					case 1:
						this.ᜁ = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6C;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					if (value == this.ᜁ)
					{
						break;
					}
					num = 1;
				}
				IL_6C:
				this.SetName(this.ᜀ(this.ᜀ, this.ᜁ, this.ᜂ, this.ᜃ));
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x00080380 File Offset: 0x0007F380
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x000803C4 File Offset: 0x0007F3C4
		[Description("Gets or sets the last column of the cell range to merge.")]
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int EndCol
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜂ = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6C;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 1:
						goto IL_6C;
					}
					if (true)
					{
					}
					if (value == this.ᜂ)
					{
						break;
					}
					num = 0;
				}
				IL_6C:
				this.SetName(this.ᜀ(this.ᜀ, this.ᜁ, this.ᜂ, this.ᜃ));
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00080464 File Offset: 0x0007F464
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x000804A8 File Offset: 0x0007F4A8
		[Description("Gets or sets the last row of the cell range to merge.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(0)]
		public int EndRow
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_62;
					case 2:
						goto IL_51;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_51:
						this.ᜃ = value;
						num = 0;
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (value == this.ᜃ)
						{
							goto IL_6E;
						}
						num = 2;
						break;
					}
				}
				IL_62:
				IL_6E:
				this.SetName(this.ᜀ(this.ᜀ, this.ᜁ, this.ᜂ, this.ᜃ));
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00080548 File Offset: 0x0007F548
		[Browsable(false)]
		public string DisplayName
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
				return string.Format(HyperlinksCollectionEditor.b("刞䐠儢䈤䈦䴨ପ測䨮崰弲䘴᜶ᄸ䀺഼䈾浀捂㹄癆㑈杊浌㑎捐⹒祔睖≘桚⁜癞", a_), new object[]
				{
					this.ᜀ,
					this.ᜁ,
					this.ᜂ,
					this.ᜃ
				});
			}
		}

		// Token: 0x0400093A RID: 2362
		private long \u25D9\u00A7\u0099\u0083;

		// Token: 0x0400093B RID: 2363
		private long \u25D8\u0090\u00A2\u00A2;

		// Token: 0x0400093C RID: 2364
		private int ᜀ;

		// Token: 0x0400093D RID: 2365
		private float \u25D9\u00A9\u00A4\u008F;

		// Token: 0x0400093E RID: 2366
		private byte \u2593\u00A4\u0089\u009F;

		// Token: 0x0400093F RID: 2367
		private int ᜁ;

		// Token: 0x04000940 RID: 2368
		private long \u25D8\u009D\u0096\u0082;

		// Token: 0x04000941 RID: 2369
		private int ᜂ;

		// Token: 0x04000942 RID: 2370
		private int ᜃ;
	}
}
