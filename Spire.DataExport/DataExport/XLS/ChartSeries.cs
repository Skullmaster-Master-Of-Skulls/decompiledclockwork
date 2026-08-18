using System;
using System.ComponentModel;
using System.Drawing.Design;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.PropEditors;
using Spire.DataExport.TypeConverters;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001D5 RID: 469
	[TypeConverter(typeof(CollectionTypeConverter))]
	public class ChartSeries : CustomItem, ICloneable
	{
		// Token: 0x06000E2B RID: 3627 RVA: 0x0009E094 File Offset: 0x0009D094
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
			return new ChartSeries
			{
				DataColumn = this.DataColumn,
				Color = this.Color,
				DataRange = this.DataRange,
				DataRangeType = this.DataRangeType,
				DataRangeSheet = this.DataRangeSheet,
				Title = this.Title
			};
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0009E120 File Offset: 0x0009D120
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

		// Token: 0x06000E2D RID: 3629 RVA: 0x0009E15C File Offset: 0x0009D15C
		public void SaveToXmlFile(XMLFile File, string Section)
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			string key = HyperlinksCollectionEditor.b("弛焝䰟䴡嘣", a_);
			int num = (int)this.ᜀ;
			File.WriteValue(Section, key, num.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("䠛眝吟両䄣", a_), this.ᜄ);
			File.WriteValue(Section, HyperlinksCollectionEditor.b("堛缝吟䌡瘣䜥䘧䴩䤫紭堯圱儳䈵", a_), this.ᜅ);
			File.WriteValue(Section, HyperlinksCollectionEditor.b("弛焝䰟圡䤣䠥", a_), this.ᜁ);
			string key2 = HyperlinksCollectionEditor.b("堛缝吟䌡瘣䜥䘧䴩䤫稭䤯䈱儳", a_);
			int num2 = (int)this.ᜃ;
			File.WriteValue(Section, key2, num2.ToString());
			this.ᜂ.SaveToXmlFile(File, Section);
			File.SaveToFile();
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0009E24C File Offset: 0x0009D24C
		public void LoadFromXmlFile(XMLFile File, string Section)
		{
			int a_ = 5;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜀ = (CellColor)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("戠䰢䤤䠦嬨", a_), 20.ToString()));
			this.ᜄ = File.ReadValue(Section, HyperlinksCollectionEditor.b("甠䨢儤䬦䰨", a_), string.Empty);
			this.ᜅ = File.ReadValue(Section, HyperlinksCollectionEditor.b("攠䈢儤䘦笨䨪䌬䠮吰怲崴制尸伺", a_), string.Empty);
			this.ᜁ = File.ReadValue(Section, HyperlinksCollectionEditor.b("戠䰢䤤刦䐨䔪", a_), string.Empty);
			this.ᜃ = (RangeType)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("攠䈢儤䘦笨䨪䌬䠮吰朲䰴䜶尸", a_), 0.ToString()));
			this.ᜂ.LoadFromXmlFile(File, Section);
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0009E350 File Offset: 0x0009D350
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
				return ItemType.Series;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0009E38C File Offset: 0x0009D38C
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x0009E3D0 File Offset: 0x0009D3D0
		[DefaultValue(CellColor.Aqua)]
		[Editor(typeof(CellColorEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the chart series color.")]
		public CellColor Color
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
				for (;;)
				{
					IL_00:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜀ = value;
							num = 1;
							continue;
						case 1:
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
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
						}
						if (value == this.ᜀ)
						{
							return;
						}
						num = 0;
					}
				}
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0009E44C File Offset: 0x0009D44C
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x0009E490 File Offset: 0x0009D490
		[DefaultValue("")]
		[Editor(typeof(ColumnNameEditor), typeof(UITypeEditor))]
		[Description("Gets or sets column name for the result chart series.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string DataColumn
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
				for (;;)
				{
					IL_00:
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
								goto IL_00;
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
						case 2:
							this.ᜁ = value;
							num = 0;
							continue;
						}
						if (!(value != this.ᜁ))
						{
							return;
						}
						num = 2;
					}
				}
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x0009E510 File Offset: 0x0009D510
		// (set) Token: 0x06000E35 RID: 3637 RVA: 0x0009E554 File Offset: 0x0009D554
		[Description("Gets or sets data range for the chart series.")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataRange DataRange
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
				for (;;)
				{
					IL_00:
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (value != this.ᜂ)
							{
								num = 4;
								continue;
							}
							return;
						case 2:
							num = 1;
							continue;
						case 3:
							if (true)
							{
							}
							break;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.ᜂ = value;
								num = 0;
								continue;
							}
							break;
						}
						if (value == null)
						{
							return;
						}
						num = 2;
					}
				}
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x0009E5EC File Offset: 0x0009D5EC
		// (set) Token: 0x06000E37 RID: 3639 RVA: 0x0009E630 File Offset: 0x0009D630
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the data range type for the chart series.")]
		[DefaultValue(RangeType.Column)]
		public RangeType DataRangeType
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
				for (;;)
				{
					IL_00:
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
								goto IL_00;
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
							return;
						case 2:
							this.ᜃ = value;
							num = 1;
							continue;
						}
						if (value == this.ᜃ)
						{
							return;
						}
						num = 2;
					}
				}
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x0009E6AC File Offset: 0x0009D6AC
		// (set) Token: 0x06000E39 RID: 3641 RVA: 0x0009E6F0 File Offset: 0x0009D6F0
		[Description("Gets or sets the worksheet of the data range.")]
		[Editor(typeof(WorkSheetNameEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string DataRangeSheet
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
				for (;;)
				{
					IL_00:
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
								goto IL_00;
							default:
								if (false)
								{
								}
								break;
							}
							break;
						case 1:
							return;
						case 2:
							this.ᜅ = value;
							num = 1;
							continue;
						}
						if (true)
						{
						}
						if (!(value != this.ᜅ))
						{
							return;
						}
						num = 2;
					}
				}
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x0009E770 File Offset: 0x0009D770
		// (set) Token: 0x06000E3B RID: 3643 RVA: 0x0009E7B4 File Offset: 0x0009D7B4
		[Description("Gets or sets the title of the result chart series.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
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
				return this.ᜄ;
			}
			set
			{
				for (;;)
				{
					IL_00:
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
								goto IL_00;
							default:
								if (false)
								{
								}
								break;
							}
							break;
						case 1:
							this.ᜄ = value;
							this.SetName(value);
							if (true)
							{
							}
							num = 2;
							continue;
						case 2:
							return;
						}
						if (!(value != this.ᜄ))
						{
							return;
						}
						num = 1;
					}
				}
			}
		}

		// Token: 0x04000AC9 RID: 2761
		private CellColor ᜀ = CellColor.Aqua;

		// Token: 0x04000ACA RID: 2762
		private string ᜁ = string.Empty;

		// Token: 0x04000ACB RID: 2763
		private DataRange ᜂ = new DataRange();

		// Token: 0x04000ACC RID: 2764
		private string[] \u2460\u00A7\u0099\u0087;

		// Token: 0x04000ACD RID: 2765
		private RangeType ᜃ;

		// Token: 0x04000ACE RID: 2766
		private string ᜄ = string.Empty;

		// Token: 0x04000ACF RID: 2767
		private byte \u2460\u007F\u0093\u00AE;

		// Token: 0x04000AD0 RID: 2768
		private string ᜅ = string.Empty;
	}
}
