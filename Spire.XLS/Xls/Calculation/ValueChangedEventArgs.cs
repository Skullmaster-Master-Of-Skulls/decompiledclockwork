using System;

namespace Spire.Xls.Calculation
{
	// Token: 0x020001B3 RID: 435
	public class ValueChangedEventArgs : EventArgs
	{
		// Token: 0x06001774 RID: 6004 RVA: 0x000E1FB8 File Offset: 0x000E0FB8
		public ValueChangedEventArgs(int row, int col, string value)
		{
			this.ᜁ = row;
			this.ᜀ = col;
			this.ᜂ = value;
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x000E1FE0 File Offset: 0x000E0FE0
		// (set) Token: 0x06001776 RID: 6006 RVA: 0x000E2024 File Offset: 0x000E1024
		public int Column
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

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x000E2068 File Offset: 0x000E1068
		// (set) Token: 0x06001778 RID: 6008 RVA: 0x000E20AC File Offset: 0x000E10AC
		public int Row
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

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x000E20F0 File Offset: 0x000E10F0
		// (set) Token: 0x0600177A RID: 6010 RVA: 0x000E2134 File Offset: 0x000E1134
		public string Value
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜂ = value;
			}
		}

		// Token: 0x04000FA0 RID: 4000
		private byte \u25D9\u0096\u00A5\u0088;

		// Token: 0x04000FA1 RID: 4001
		private float[] \u25D9\u00AC\u0088\u009D;

		// Token: 0x04000FA2 RID: 4002
		private int ᜀ;

		// Token: 0x04000FA3 RID: 4003
		private string[] \u25D8\u00A1\u0086\u0090;

		// Token: 0x04000FA4 RID: 4004
		private byte[] \u2609\u00AD\u00B0\u0082;

		// Token: 0x04000FA5 RID: 4005
		private int ᜁ;

		// Token: 0x04000FA6 RID: 4006
		private string ᜂ;
	}
}
