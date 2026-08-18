using System;
using System.ComponentModel;
using System.Drawing;
using Spire.DataExport.Common;

namespace Spire.DataExport.PDF
{
	// Token: 0x02000227 RID: 551
	public class PdfExportPageOptions : ICloneable
	{
		// Token: 0x06001033 RID: 4147 RVA: 0x000AEE00 File Offset: 0x000ADE00
		public PdfExportPageOptions()
		{
			this.MarginLeft = 1.18;
			this.MarginBottom = 0.79;
			this.MarginRight = 0.59;
			this.MarginTop = 0.79;
			this.Format = PageFormat.A4;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x000AEE6C File Offset: 0x000ADE6C
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
			return new PdfExportPageOptions
			{
				Units = this.Units,
				Format = this.Format,
				Width = this.Width,
				Height = this.Height,
				Orientation = this.Orientation,
				MarginLeft = this.MarginLeft,
				MarginRight = this.MarginRight,
				MarginTop = this.MarginTop,
				MarginBottom = this.MarginBottom
			};
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x000AEF1C File Offset: 0x000ADF1C
		internal sprᶆ MediaBox
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
				this.ᜀ.ᜂ(0);
				this.ᜀ.ᜀ(0);
				this.ᜀ.ᜃ(this.ᜄ - 1);
				this.ᜀ.ᜁ(this.ᜅ - 1);
				return this.ᜀ;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x000AEF9C File Offset: 0x000ADF9C
		internal sprᶆ TrimBox
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
				this.ᜁ.ᜂ(this.ᜆ);
				this.ᜁ.ᜀ(this.ᜉ);
				this.ᜁ.ᜃ(this.ᜄ - this.ᜈ - 1);
				this.ᜁ.ᜁ(this.ᜅ - this.ᜇ - 1);
				return this.ᜁ;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x000AF034 File Offset: 0x000AE034
		// (set) Token: 0x06001038 RID: 4152 RVA: 0x000AF078 File Offset: 0x000AE078
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(PageUnits.Inch)]
		[RefreshProperties(RefreshProperties.All)]
		public PageUnits Units
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (true)
						{
						}
						this.ᜂ = value;
						num = 0;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (value == this.ᜂ)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x000AF0F4 File Offset: 0x000AE0F4
		// (set) Token: 0x0600103A RID: 4154 RVA: 0x000AF140 File Offset: 0x000AE140
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[RefreshProperties(RefreshProperties.All)]
		public double Width
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
				return sprᤓ.ᜀ(this.ᜂ, this.ᜄ);
			}
			set
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						int num = this.ᜄ;
						this.ᜄ = sprᤓ.ᜀ(this.ᜂ, value);
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								return;
							case 1:
								this.ᜊ = PageFormat.User;
								num2 = 0;
								continue;
							case 2:
								if (num != this.ᜄ)
								{
									num2 = 1;
									continue;
								}
								return;
							}
							break;
						}
						break;
					}
					}
				}
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x000AF1D8 File Offset: 0x000AE1D8
		// (set) Token: 0x0600103C RID: 4156 RVA: 0x000AF224 File Offset: 0x000AE224
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[RefreshProperties(RefreshProperties.All)]
		public double Height
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
				return sprᤓ.ᜀ(this.ᜂ, this.ᜅ);
			}
			set
			{
				for (;;)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					}
					if (false)
					{
					}
					int num = this.ᜅ;
					this.ᜅ = sprᤓ.ᜀ(this.ᜂ, value);
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num != this.ᜅ)
							{
								num2 = 1;
								continue;
							}
							return;
						case 1:
							this.ᜊ = PageFormat.User;
							num2 = 2;
							continue;
						case 2:
							return;
						}
						break;
					}
				}
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x000AF2BC File Offset: 0x000AE2BC
		// (set) Token: 0x0600103E RID: 4158 RVA: 0x000AF300 File Offset: 0x000AE300
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(PageFormat.A4)]
		public PageFormat Format
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
				return this.ᜊ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜊ != PageFormat.User)
						{
							num = 3;
							continue;
						}
						return;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						goto IL_94;
					case 3:
						this.ᜄ = sprᤓ.ᜁ(sprᤓ.ᜁ(this.ᜊ));
						this.ᜅ = sprᤓ.ᜁ(sprᤓ.ᜀ(this.ᜊ));
						num = 4;
						continue;
					case 4:
						return;
					}
					if (this.ᜊ == value)
					{
						break;
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
					IL_94:
					this.ᜊ = value;
					num = 0;
				}
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x000AF3D4 File Offset: 0x000AE3D4
		// (set) Token: 0x06001040 RID: 4160 RVA: 0x000AF418 File Offset: 0x000AE418
		[DefaultValue(PageOrientation.Portrait)]
		[RefreshProperties(RefreshProperties.All)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public PageOrientation Orientation
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
				int num = 0;
				Rectangle rectangle;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_8E;
					case 2:
						goto IL_167;
					case 3:
					{
						this.ᜃ = value;
						Size size = new Size(this.ᜄ, this.ᜅ);
						this.ᜄ = size.Height;
						this.ᜅ = size.Width;
						rectangle = new Rectangle(this.ᜆ, this.ᜇ, this.ᜈ - this.ᜆ, this.ᜉ - this.ᜇ);
						num = 4;
						continue;
					}
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_30;
						default:
							if (false)
							{
							}
							if (this.ᜃ == PageOrientation.Landscape)
							{
								num = 2;
								continue;
							}
							this.ᜆ = rectangle.Top;
							this.ᜇ = rectangle.Right;
							this.ᜈ = rectangle.Bottom;
							this.ᜉ = rectangle.Left;
							num = 1;
							continue;
						}
						break;
					}
					goto IL_24;
					IL_30:
					if (true)
					{
					}
					num = 3;
					continue;
					IL_24:
					if (this.ᜃ != value)
					{
						goto IL_30;
					}
					break;
				}
				IL_8E:
				return;
				IL_167:
				this.ᜆ = rectangle.Bottom;
				this.ᜇ = rectangle.Left;
				this.ᜈ = rectangle.Top;
				this.ᜉ = rectangle.Right;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x000AF594 File Offset: 0x000AE594
		// (set) Token: 0x06001042 RID: 4162 RVA: 0x000AF5E0 File Offset: 0x000AE5E0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public double MarginLeft
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
				return sprᤓ.ᜀ(this.ᜂ, this.ᜆ);
			}
			set
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
				this.ᜆ = sprᤓ.ᜀ(this.ᜂ, value);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x000AF630 File Offset: 0x000AE630
		// (set) Token: 0x06001044 RID: 4164 RVA: 0x000AF67C File Offset: 0x000AE67C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public double MarginTop
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
				return sprᤓ.ᜀ(this.ᜂ, this.ᜇ);
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
				this.ᜇ = sprᤓ.ᜀ(this.ᜂ, value);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x000AF6CC File Offset: 0x000AE6CC
		// (set) Token: 0x06001046 RID: 4166 RVA: 0x000AF718 File Offset: 0x000AE718
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public double MarginRight
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
				return sprᤓ.ᜀ(this.ᜂ, this.ᜈ);
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
				this.ᜈ = sprᤓ.ᜀ(this.ᜂ, value);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x000AF768 File Offset: 0x000AE768
		// (set) Token: 0x06001048 RID: 4168 RVA: 0x000AF7B4 File Offset: 0x000AE7B4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public double MarginBottom
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
				return sprᤓ.ᜀ(this.ᜂ, this.ᜉ);
			}
			set
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
				this.ᜉ = sprᤓ.ᜀ(this.ᜂ, value);
			}
		}

		// Token: 0x04000BBE RID: 3006
		private sprᶆ ᜀ = new sprᶆ();

		// Token: 0x04000BBF RID: 3007
		private sprᶆ ᜁ = new sprᶆ();

		// Token: 0x04000BC0 RID: 3008
		private PageUnits ᜂ;

		// Token: 0x04000BC1 RID: 3009
		private PageOrientation ᜃ;

		// Token: 0x04000BC2 RID: 3010
		private int ᜄ;

		// Token: 0x04000BC3 RID: 3011
		private int ᜅ;

		// Token: 0x04000BC4 RID: 3012
		private int ᜆ;

		// Token: 0x04000BC5 RID: 3013
		private int ᜇ;

		// Token: 0x04000BC6 RID: 3014
		private int ᜈ;

		// Token: 0x04000BC7 RID: 3015
		private float[] \u25D8\u0097\u0099\u00A6;

		// Token: 0x04000BC8 RID: 3016
		private int ᜉ;

		// Token: 0x04000BC9 RID: 3017
		private long \u25D9\u0084\u008D\u008E;

		// Token: 0x04000BCA RID: 3018
		private long[] \u2593\u008F\u0092\u008E;

		// Token: 0x04000BCB RID: 3019
		private PageFormat ᜊ;
	}
}
