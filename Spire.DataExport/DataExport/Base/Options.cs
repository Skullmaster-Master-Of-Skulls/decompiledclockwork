using System;
using System.ComponentModel;
using Spire.DataExport.Common;

namespace Spire.DataExport.Base
{
	// Token: 0x02000186 RID: 390
	public class Options : DisposabledObject, ICloneable
	{
		// Token: 0x06000AD4 RID: 2772 RVA: 0x00071DF4 File Offset: 0x00070DF4
		public Options(object Holder)
		{
			this.ᜀ = Holder;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00071E1C File Offset: 0x00070E1C
		protected override void Dispose(bool Disposing)
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
			bool flag = this.ᜁ;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00071E60 File Offset: 0x00070E60
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
			return new Options(this)
			{
				WaitCursor = this.WaitCursor,
				InsertRowAfterTitle = this.InsertRowAfterTitle,
				DisableControls = this.DisableControls
			};
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00071EC8 File Offset: 0x00070EC8
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x00071F0C File Offset: 0x00070F0C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		public bool DisableControls
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
				return this.ᜄ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							return;
						case 2:
							this.ᜄ = value;
							if (true)
							{
							}
							num = 0;
							continue;
						}
						if (value == this.ᜄ)
						{
							return;
						}
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00071F88 File Offset: 0x00070F88
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x00071FCC File Offset: 0x00070FCC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		public bool WaitCursor
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
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						switch (num)
						{
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
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00072048 File Offset: 0x00071048
		// (set) Token: 0x06000ADC RID: 2780 RVA: 0x0007208C File Offset: 0x0007108C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		public bool InsertRowAfterTitle
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
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							this.ᜂ = value;
							num = 2;
							continue;
						case 2:
							return;
						}
						if (value == this.ᜂ)
						{
							return;
						}
						break;
					}
					if (true)
					{
					}
					num = 0;
				}
			}
		}

		// Token: 0x0400082E RID: 2094
		private bool \u25D8\u008F\u0092\u00AF;

		// Token: 0x0400082F RID: 2095
		private object ᜀ;

		// Token: 0x04000830 RID: 2096
		private bool \u2460\u00A7\u009A\u009E;

		// Token: 0x04000831 RID: 2097
		private int \u25D8\u00A7\u0090\u008F;

		// Token: 0x04000832 RID: 2098
		private float[] \u2593\u008E\u00A6\u0096;

		// Token: 0x04000833 RID: 2099
		private bool \u2593\u007Fª\u0099;

		// Token: 0x04000834 RID: 2100
		private bool ᜁ;

		// Token: 0x04000835 RID: 2101
		private bool ᜂ;

		// Token: 0x04000836 RID: 2102
		private bool ᜃ = true;

		// Token: 0x04000837 RID: 2103
		private bool ᜄ = true;
	}
}
