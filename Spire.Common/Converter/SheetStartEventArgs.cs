using System;

namespace Spire.Xls.Converter
{
	// Token: 0x02000023 RID: 35
	public class SheetStartEventArgs : EventArgs
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x0001499C File Offset: 0x00012B9C
		public SheetStartEventArgs(int currentSheet, object source)
		{
			if (!this.ᜁ)
			{
				this.ᜀ = currentSheet + 1;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000149CC File Offset: 0x00012BCC
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00014A10 File Offset: 0x00012C10
		public int CurrentSheet
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00014A54 File Offset: 0x00012C54
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00014A98 File Offset: 0x00012C98
		public bool Skip
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
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜁ = value;
			}
		}

		// Token: 0x04000077 RID: 119
		private byte \u25D8\u00A2\u008F\u0087;

		// Token: 0x04000078 RID: 120
		private int ᜀ = -1;

		// Token: 0x04000079 RID: 121
		private float[] \u2609\u007F\u009F\u00AB;

		// Token: 0x0400007A RID: 122
		private bool ᜁ;
	}
}
