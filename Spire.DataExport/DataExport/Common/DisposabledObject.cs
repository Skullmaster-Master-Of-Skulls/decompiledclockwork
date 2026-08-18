using System;

namespace Spire.DataExport.Common
{
	// Token: 0x02000015 RID: 21
	public abstract class DisposabledObject : IDisposable
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00008DE8 File Offset: 0x00007DE8
		public DisposabledObject()
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00008DFC File Offset: 0x00007DFC
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

		// Token: 0x060000CA RID: 202 RVA: 0x00008E58 File Offset: 0x00007E58
		protected virtual void Dispose(bool Disposing)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_66;
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
						num = 1;
						continue;
					}
					break;
				}
				IL_26:
				if (!this.ᜀ)
				{
					num = 2;
					continue;
				}
				break;
				goto IL_26;
			}
			IL_66:
			this.ᜀ = true;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00008ED4 File Offset: 0x00007ED4
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
					break;
				default:
					goto IL_36;
				}
			}
			IL_36:
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00008F40 File Offset: 0x00007F40
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

		// Token: 0x04000026 RID: 38
		private long \u2593\u0083\u009E\u009C;

		// Token: 0x04000027 RID: 39
		private float \u2460\u0097\u00A7\u0095;

		// Token: 0x04000028 RID: 40
		private string \u2460\u008E\u008F\u00A9;

		// Token: 0x04000029 RID: 41
		private int \u2460\u00A8\u0093\u0096;

		// Token: 0x0400002A RID: 42
		private bool ᜀ;
	}
}
