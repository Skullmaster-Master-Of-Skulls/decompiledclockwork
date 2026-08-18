using System;
using System.ComponentModel;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001B7 RID: 439
	public class TextAlignment : ICloneable
	{
		// Token: 0x06000C4F RID: 3151 RVA: 0x00081080 File Offset: 0x00080080
		public TextAlignment()
		{
			this.SetDefault();
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x000810A0 File Offset: 0x000800A0
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
			return new TextAlignment
			{
				Horizontal = this.Horizontal,
				Vertical = this.Vertical
			};
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000810FC File Offset: 0x000800FC
		public bool IsEqual(TextAlignment Alignment)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_6A:
				num = 0;
				break;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (this.ᜀ == Alignment.Horizontal)
					{
						num = 1;
						continue;
					}
					return false;
				case 1:
					goto IL_88;
				case 3:
					return false;
				}
				if (Alignment != null)
				{
					goto IL_6A;
				}
				num = 3;
			}
			return false;
			IL_88:
			return this.ᜁ == Alignment.Vertical;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00081198 File Offset: 0x00080198
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
			this.ᜀ = HorizontalAlignment.General;
			this.ᜁ = VerticalAlignment.Bottom;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x000811E4 File Offset: 0x000801E4
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x00081228 File Offset: 0x00080228
		[DefaultValue(HorizontalAlignment.General)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public HorizontalAlignment Horizontal
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
						if (true)
						{
						}
						this.ᜀ = value;
						num = 2;
						continue;
					case 2:
						return;
					}
					if (value == this.ᜀ)
					{
						break;
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
					num = 1;
				}
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x000812A4 File Offset: 0x000802A4
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x000812E8 File Offset: 0x000802E8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(VerticalAlignment.Bottom)]
		public VerticalAlignment Vertical
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜁ = value;
						num = 2;
						continue;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (value == this.ᜁ)
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
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x0400095E RID: 2398
		private int \u25D9\u0099\u009C\u0086;

		// Token: 0x0400095F RID: 2399
		private HorizontalAlignment ᜀ;

		// Token: 0x04000960 RID: 2400
		private float \u2593\u0083\u009D\u009C;

		// Token: 0x04000961 RID: 2401
		private byte[] \u25D8\u008D\u0088\u0095;

		// Token: 0x04000962 RID: 2402
		private int[] \u2609\u0083\u0087\u0085;

		// Token: 0x04000963 RID: 2403
		private float \u25D8\u009D\u008A\u009A;

		// Token: 0x04000964 RID: 2404
		private byte[] \u25D8\u0094\u00A6\u008D;

		// Token: 0x04000965 RID: 2405
		private VerticalAlignment ᜁ = VerticalAlignment.Bottom;
	}
}
