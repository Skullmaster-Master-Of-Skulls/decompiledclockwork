using System;
using System.ComponentModel;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Utils;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001D3 RID: 467
	public class ChartCustomPosition : ICloneable
	{
		// Token: 0x06000E1F RID: 3615 RVA: 0x0009DB24 File Offset: 0x0009CB24
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
			return new ChartCustomPosition
			{
				X1 = this.X1,
				X2 = this.X2,
				Y1 = this.Y1,
				Y2 = this.Y2
			};
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0009DB98 File Offset: 0x0009CB98
		public void SaveToXmlFile(XMLFile File, string Section)
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			File.WriteValue(Section, HyperlinksCollectionEditor.b("簣ᜥ", a_), this.ᜀ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("紣ᜥ", a_), this.ᜂ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("簣ᐥ", a_), this.ᜁ.ToString());
			File.WriteValue(Section, HyperlinksCollectionEditor.b("紣ᐥ", a_), this.ᜃ.ToString());
			File.SaveToFile();
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0009DC64 File Offset: 0x0009CC64
		public void LoadFromXmlFile(XMLFile File, string Section)
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
			this.ᜀ = Convert.ToByte(File.ReadValue(Section, HyperlinksCollectionEditor.b("䐛⼝", a_), 0.ToString()));
			this.ᜂ = (int)Convert.ToUInt16(File.ReadValue(Section, HyperlinksCollectionEditor.b("䔛⼝", a_), 0.ToString()));
			this.ᜁ = Convert.ToByte(File.ReadValue(Section, HyperlinksCollectionEditor.b("䐛Ⱍ", a_), 0.ToString()));
			this.ᜃ = (int)Convert.ToUInt16(File.ReadValue(Section, HyperlinksCollectionEditor.b("䔛Ⱍ", a_), 0.ToString()));
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0009DD4C File Offset: 0x0009CD4C
		// (set) Token: 0x06000E23 RID: 3619 RVA: 0x0009DD90 File Offset: 0x0009CD90
		[DefaultValue(0)]
		[Description("Gets or sets the horizontal position of the top left corner of the chart.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public byte X1
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						return;
					case 2:
						for (;;)
						{
							this.ᜀ = value;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_4C;
							}
						}
						IL_4C:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 1;
						continue;
					}
					if (value == this.ᜀ)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0009DE0C File Offset: 0x0009CE0C
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x0009DE50 File Offset: 0x0009CE50
		[Description("Gets or sets the horisontal position of the bottom right corner of the chart.")]
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public byte X2
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
						return;
					case 1:
						for (;;)
						{
							this.ᜁ = value;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_54;
							}
						}
						IL_54:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					if (true)
					{
					}
					if (value == this.ᜁ)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0009DECC File Offset: 0x0009CECC
		// (set) Token: 0x06000E27 RID: 3623 RVA: 0x0009DF10 File Offset: 0x0009CF10
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the vertical position of the top left corner of the chart.")]
		public int Y1
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
					case 1:
						for (;;)
						{
							this.ᜂ = value;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_54;
							}
						}
						IL_54:
						if (false)
						{
						}
						num = 2;
						continue;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (value == this.ᜂ)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0009DF8C File Offset: 0x0009CF8C
		// (set) Token: 0x06000E29 RID: 3625 RVA: 0x0009DFD0 File Offset: 0x0009CFD0
		[Description("Gets or sets the vertical position of the bottom right corner of the chart.")]
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int Y2
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						return;
					case 2:
						for (;;)
						{
							this.ᜃ = value;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_54;
							}
						}
						IL_54:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					if (value == this.ᜃ)
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x04000ABE RID: 2750
		private float \u25D8\u0089\u00AC\u00A0;

		// Token: 0x04000ABF RID: 2751
		private byte ᜀ;

		// Token: 0x04000AC0 RID: 2752
		private byte ᜁ;

		// Token: 0x04000AC1 RID: 2753
		private long[] \u2460\u0091\u008B\u00A8;

		// Token: 0x04000AC2 RID: 2754
		private string \u2460\u0086\u007F\u00A7;

		// Token: 0x04000AC3 RID: 2755
		private int ᜂ;

		// Token: 0x04000AC4 RID: 2756
		private byte \u2460\u009D\u0098\u0097;

		// Token: 0x04000AC5 RID: 2757
		private int ᜃ;
	}
}
