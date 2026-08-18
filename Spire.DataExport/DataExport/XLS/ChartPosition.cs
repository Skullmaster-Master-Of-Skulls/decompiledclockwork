using System;
using System.ComponentModel;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001B1 RID: 433
	public class ChartPosition : ICloneable
	{
		// Token: 0x06000C34 RID: 3124 RVA: 0x0008060C File Offset: 0x0007F60C
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
			return new ChartPosition
			{
				AutoPosition = this.AutoPosition,
				CustomPosition = this.CustomPosition,
				PositionType = this.PositionType
			};
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00080674 File Offset: 0x0007F674
		public void SaveToXmlFile(XMLFile File, string Section)
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ.SaveToXmlFile(File, Section);
			this.ᜁ.SaveToXmlFile(File, Section);
			string key = HyperlinksCollectionEditor.b("䰛焝匟䬡倣伥䜧䐩砫圭䀯圱", a_);
			int num = (int)this.ᜂ;
			File.WriteValue(Section, key, num.ToString());
			File.SaveToFile();
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000806FC File Offset: 0x0007F6FC
		public void LoadFromXmlFile(XMLFile File, string Section)
		{
			int a_ = 4;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜀ.LoadFromXmlFile(File, Section);
			this.ᜁ.LoadFromXmlFile(File, Section);
			this.ᜂ = (ChartPositionType)Convert.ToInt32(File.ReadValue(Section, HyperlinksCollectionEditor.b("瀟䴡圣伥尧䌩䌫䀭搯䬱䐳匵", a_), 0.ToString()));
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x00080784 File Offset: 0x0007F784
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x000807C8 File Offset: 0x0007F7C8
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ChartAutoPosition AutoPosition
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
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_31;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_31;
						}
						goto Block_2;
					case 3:
						if (value != this.ᜀ)
						{
							num = 0;
							continue;
						}
						return;
					case 4:
						num = 3;
						continue;
					}
					if (value != null)
					{
						num = 4;
						continue;
					}
					return;
					IL_31:
					this.ᜀ = value;
					num = 1;
				}
				Block_2:
				if (false)
				{
				}
				if (true)
				{
				}
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00080860 File Offset: 0x0007F860
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x000808A4 File Offset: 0x0007F8A4
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ChartCustomPosition CustomPosition
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != this.ᜁ)
						{
							num = 3;
							continue;
						}
						return;
					case 1:
						num = 0;
						continue;
					case 3:
						goto IL_39;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_39;
						default:
							goto IL_68;
						}
						break;
					}
					if (value != null)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					return;
					IL_39:
					this.ᜁ = value;
					num = 4;
				}
				IL_68:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0008093C File Offset: 0x0007F93C
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x00080980 File Offset: 0x0007F980
		[DefaultValue(ChartPositionType.Auto)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public ChartPositionType PositionType
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5D;
					case 2:
						return;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_5D:
						this.ᜂ = value;
						num = 2;
						break;
					default:
						if (false)
						{
						}
						if (value == this.ᜂ)
						{
							return;
						}
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x04000943 RID: 2371
		private ChartAutoPosition ᜀ = new ChartAutoPosition();

		// Token: 0x04000944 RID: 2372
		private float[] \u2609ª\u00A9\u0091;

		// Token: 0x04000945 RID: 2373
		private bool \u2460\u0090\u00A6\u00A7;

		// Token: 0x04000946 RID: 2374
		private long[] \u2593\u0088\u0094\u0086;

		// Token: 0x04000947 RID: 2375
		private int \u25D8\u0095\u0089\u009E;

		// Token: 0x04000948 RID: 2376
		private ChartCustomPosition ᜁ = new ChartCustomPosition();

		// Token: 0x04000949 RID: 2377
		private float \u25D9\u0093\u00A1ª;

		// Token: 0x0400094A RID: 2378
		private ChartPositionType ᜂ;
	}
}
