using System;
using System.ComponentModel;
using System.Drawing.Design;
using Spire.DataExport.PropEditors;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001E1 RID: 481
	public class CellBorder : ICloneable
	{
		// Token: 0x06000E93 RID: 3731 RVA: 0x000A1808 File Offset: 0x000A0808
		public CellBorder()
		{
			this.SetDefault();
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x000A1824 File Offset: 0x000A0824
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
			return new CellBorder
			{
				Style = this.Style,
				Color = this.Color
			};
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x000A1880 File Offset: 0x000A0880
		public bool IsEqual(CellBorder Border)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return false;
				case 1:
					if (this.ᜀ == Border.Style)
					{
						num = 3;
						continue;
					}
					return false;
				case 3:
					goto IL_88;
				}
				if (Border == null)
				{
					if (true)
					{
					}
					num = 0;
				}
				else
				{
					num = 1;
				}
			}
			return false;
			IL_88:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return false;
			default:
				if (false)
				{
				}
				return this.ᜁ == Border.Color;
			}
			return false;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x000A191C File Offset: 0x000A091C
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
			this.ᜀ = CellBorderStyle.None;
			this.ᜁ = CellColor.Black;
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x000A1968 File Offset: 0x000A0968
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x000A19AC File Offset: 0x000A09AC
		[DefaultValue(CellBorderStyle.None)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public CellBorderStyle Style
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
						this.ᜀ = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_25;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 1:
						return;
					}
					goto IL_1C;
					IL_25:
					num = 0;
					continue;
					IL_1C:
					if (value != this.ᜀ)
					{
						goto IL_25;
					}
					break;
				}
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x000A1A28 File Offset: 0x000A0A28
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x000A1A6C File Offset: 0x000A0A6C
		[Editor(typeof(CellColorEditor), typeof(UITypeEditor))]
		[DefaultValue(CellColor.Black)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
						this.ᜁ = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2D;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					goto IL_1C;
					IL_2D:
					num = 1;
					continue;
					IL_1C:
					if (true)
					{
					}
					if (value != this.ᜁ)
					{
						goto IL_2D;
					}
					break;
				}
			}
		}

		// Token: 0x04000B1B RID: 2843
		private float \u25D8\u0089\u00AC\u0080;

		// Token: 0x04000B1C RID: 2844
		private long[] \u25D9\u00AB\u0086\u00A5;

		// Token: 0x04000B1D RID: 2845
		private CellBorderStyle ᜀ;

		// Token: 0x04000B1E RID: 2846
		private long \u25D9\u00AC\u00A2\u00A3;

		// Token: 0x04000B1F RID: 2847
		private int[] \u2609\u0084\u0096\u00A1;

		// Token: 0x04000B20 RID: 2848
		private byte[] \u2593\u00A6\u009C\u0098;

		// Token: 0x04000B21 RID: 2849
		private CellColor ᜁ;
	}
}
