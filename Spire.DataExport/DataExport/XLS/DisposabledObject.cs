using System;

namespace Spire.DataExport.XLS
{
	// Token: 0x0200000C RID: 12
	public abstract class DisposabledObject : IDisposable
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00005848 File Offset: 0x00004848
		public DisposabledObject()
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000585C File Offset: 0x0000485C
		~DisposabledObject()
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
			this.Dispose(false);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000058B8 File Offset: 0x000048B8
		protected virtual void Dispose(bool Disposing)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
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
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_66;
				}
				if (this.ᜀ)
				{
					break;
				}
				num = 1;
			}
			IL_66:
			this.ᜀ = true;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00005934 File Offset: 0x00004934
		public void Dispose()
		{
			for (;;)
			{
				lock (this)
				{
					this.Dispose(true);
					GC.SuppressFinalize(this);
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000059A0 File Offset: 0x000049A0
		public void Close()
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
			this.Dispose();
		}

		// Token: 0x0400001A RID: 26
		private byte \u25D8\u009A\u0089\u009F;

		// Token: 0x0400001B RID: 27
		private int \u25D8\u008C\u00A5\u009F;

		// Token: 0x0400001C RID: 28
		private int[] \u2593\u00AF\u00A6\u008D;

		// Token: 0x0400001D RID: 29
		private long \u2593\u0096\u008C\u009C;

		// Token: 0x0400001E RID: 30
		private long \u2609\u00A0\u0085\u0081;

		// Token: 0x0400001F RID: 31
		private float[] \u25D8\u0085\u009B\u00AC;

		// Token: 0x04000020 RID: 32
		private bool ᜀ;
	}
}
