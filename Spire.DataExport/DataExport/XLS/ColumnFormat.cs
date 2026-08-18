using System;
using System.ComponentModel;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.TypeConverters;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001DF RID: 479
	[TypeConverter(typeof(CollectionTypeConverter))]
	public class ColumnFormat : CellFormat
	{
		// Token: 0x06000E89 RID: 3721 RVA: 0x000A13C4 File Offset: 0x000A03C4
		public new object Clone()
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
			return new ColumnFormat
			{
				FieldName = base.FieldName,
				Aggregate = this.Aggregate,
				Width = this.Width
			};
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000A142C File Offset: 0x000A042C
		public void Assign(object srcFormat)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					base.Font = (srcFormat as CellFormat).Font;
					base.Borders = (srcFormat as CellFormat).Borders;
					base.FillStyle = (srcFormat as CellFormat).FillStyle;
					base.Alignment = (srcFormat as CellFormat).Alignment;
					base.WordWrap = (srcFormat as CellFormat).WordWrap;
					base.Rotation = (srcFormat as CellFormat).Rotation;
					num = 1;
					continue;
				case 1:
					goto IL_B6;
				case 2:
					IL_08:
					break;
				}
				if (true)
				{
				}
				if (srcFormat is CellFormat)
				{
					num = 0;
					continue;
				}
				IL_B6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_08;
				default:
					goto IL_CC;
				}
			}
			IL_CC:
			if (false)
			{
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x000A150C File Offset: 0x000A050C
		public override void SetDefault()
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
			base.SetDefault();
			this.ᜁ = Aggregate.None;
			this.ᜀ = 0;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000A155C File Offset: 0x000A055C
		public new void SaveToXmlFile(XMLFile File, string Section)
		{
			int a_ = 16;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.SaveToXmlFile(File, Section);
			string key = HyperlinksCollectionEditor.b("洫䤭圯䀱儳儵夷丹夻", a_);
			int num = (int)this.ᜁ;
			File.WriteValue(Section, key, num.ToString());
			File.SaveToFile();
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x000A15D4 File Offset: 0x000A05D4
		public new void LoadFromXmlFile(XMLFile File, string Section)
		{
			int a_ = 5;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.LoadFromXmlFile(File, Section);
			this.ᜁ = (Aggregate)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("怠䐢䈤唦䰨䰪䰬嬮吰", a_), 0.ToString()));
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x000A164C File Offset: 0x000A064C
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
				return ItemType.FieldFormat;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x000A1688 File Offset: 0x000A0688
		// (set) Token: 0x06000E90 RID: 3728 RVA: 0x000A16CC File Offset: 0x000A06CC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(0)]
		public int Width
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_52;
					case 1:
						IL_08:
						break;
					case 2:
						this.ᜀ = value;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					if (value != this.ᜀ)
					{
						num = 2;
						continue;
					}
					IL_52:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_08;
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

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x000A1748 File Offset: 0x000A0748
		// (set) Token: 0x06000E92 RID: 3730 RVA: 0x000A178C File Offset: 0x000A078C
		[DefaultValue(Aggregate.None)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Aggregate Aggregate
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						IL_08:
						break;
					case 1:
						this.ᜁ = value;
						num = 2;
						continue;
					case 2:
						goto IL_40;
					}
					if (value != this.ᜁ)
					{
						num = 1;
						continue;
					}
					IL_40:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_08;
					default:
						goto IL_60;
					}
				}
				IL_60:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x04000B08 RID: 2824
		private int \u2609\u00A4\u0088\u008C;

		// Token: 0x04000B09 RID: 2825
		private long \u2460\u0089\u0090\u0099;

		// Token: 0x04000B0A RID: 2826
		private new int ᜀ;

		// Token: 0x04000B0B RID: 2827
		private Aggregate ᜁ;
	}
}
