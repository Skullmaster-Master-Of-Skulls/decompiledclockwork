using System;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000042 RID: 66
	public class CommonWrapper : IOptimizedUpdate, ICloneParent
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0002919C File Offset: 0x0002819C
		protected int BeginCallsCount
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
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000291E0 File Offset: 0x000281E0
		public virtual void BeginUpdate()
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
			this.ᜀ++;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0002922C File Offset: 0x0002822C
		public virtual void EndUpdate()
		{
			for (;;)
			{
				IL_00:
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
							goto IL_00;
						default:
							if (false)
							{
							}
							this.ᜀ--;
							if (true)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					if (this.ᜀ <= 0)
					{
						return;
					}
					num = 1;
				}
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000292B0 File Offset: 0x000282B0
		public virtual object Clone(object parent)
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
			return base.MemberwiseClone();
		}

		// Token: 0x040000D7 RID: 215
		private long \u2609\u008B\u009B\u008E;

		// Token: 0x040000D8 RID: 216
		private int ᜀ;
	}
}
