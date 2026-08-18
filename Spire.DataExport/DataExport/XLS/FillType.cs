using System;
using System.ComponentModel;
using System.Drawing.Design;
using Spire.DataExport.PropEditors;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001C8 RID: 456
	public class FillType : ICloneable
	{
		// Token: 0x06000D71 RID: 3441 RVA: 0x00094C64 File Offset: 0x00093C64
		public FillType()
		{
			this.SetDefault();
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00094C88 File Offset: 0x00093C88
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
			return new FillType
			{
				Background = this.Background,
				Foreground = this.Foreground,
				Pattern = this.Pattern
			};
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00094CF0 File Offset: 0x00093CF0
		public bool IsEqual(FillType Fill)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜀ == Fill.Background)
					{
						num = 3;
						continue;
					}
					return false;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6F;
					default:
						goto IL_51;
					}
					break;
				case 2:
					if (this.ᜁ == Fill.Pattern)
					{
						goto IL_6F;
					}
					return false;
				case 3:
					num = 2;
					continue;
				case 4:
					goto IL_77;
				}
				if (Fill == null)
				{
					num = 1;
					continue;
				}
				num = 0;
				continue;
				IL_6F:
				num = 4;
			}
			IL_51:
			if (false)
			{
			}
			return false;
			IL_77:
			return this.ᜂ == Fill.Foreground;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00094DB8 File Offset: 0x00093DB8
		public void SetDefault()
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
			this.ᜀ = CellColor.White;
			this.ᜁ = Pattern.None;
			this.ᜂ = CellColor.Black;
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00094E0C File Offset: 0x00093E0C
		// (set) Token: 0x06000D76 RID: 3446 RVA: 0x00094E50 File Offset: 0x00093E50
		[Editor(typeof(CellColorEditor), typeof(UITypeEditor))]
		[DefaultValue(CellColor.White)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public CellColor Background
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
							this.ᜀ = value;
							break;
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

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00094ECC File Offset: 0x00093ECC
		// (set) Token: 0x06000D78 RID: 3448 RVA: 0x00094F10 File Offset: 0x00093F10
		[DefaultValue(Pattern.None)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Pattern Pattern
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
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.ᜁ = value;
							break;
						}
						num = 1;
						continue;
					case 1:
						return;
					}
					if (true)
					{
					}
					if (value == this.ᜁ)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00094F8C File Offset: 0x00093F8C
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x00094FD0 File Offset: 0x00093FD0
		[DefaultValue(CellColor.Black)]
		[Editor(typeof(CellColorEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public CellColor Foreground
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
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.ᜂ = value;
							break;
						}
						num = 1;
						continue;
					case 1:
						return;
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
			}
		}

		// Token: 0x04000A1B RID: 2587
		private CellColor ᜀ = CellColor.White;

		// Token: 0x04000A1C RID: 2588
		private string[] \u2593\u008D\u0085\u0099;

		// Token: 0x04000A1D RID: 2589
		private long \u2593\u0082\u0088\u0081;

		// Token: 0x04000A1E RID: 2590
		private int[] \u25D9\u00A3\u00AD\u00AF;

		// Token: 0x04000A1F RID: 2591
		private bool[] \u2593\u00A8\u008A\u009C;

		// Token: 0x04000A20 RID: 2592
		private string[] \u2609\u0085\u0085\u00A0;

		// Token: 0x04000A21 RID: 2593
		private int[] \u25D9\u008A\u0091\u0088;

		// Token: 0x04000A22 RID: 2594
		private Pattern ᜁ;

		// Token: 0x04000A23 RID: 2595
		private CellColor ᜂ;
	}
}
