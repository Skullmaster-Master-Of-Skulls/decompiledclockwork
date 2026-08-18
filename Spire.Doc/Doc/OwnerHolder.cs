using System;

namespace Spire.Doc
{
	// Token: 0x0200008D RID: 141
	public abstract class OwnerHolder
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00009660 File Offset: 0x00008660
		public Document Document
		{
			get
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
					if (this.ᜀ != null)
					{
						return this.ᜀ.Document;
					}
					break;
				}
				return this.m_doc;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000096B8 File Offset: 0x000086B8
		internal OwnerHolder OwnerBase
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

		// Token: 0x0600007A RID: 122 RVA: 0x000096FC File Offset: 0x000086FC
		public OwnerHolder()
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00009710 File Offset: 0x00008710
		public OwnerHolder(Document doc) : this(doc, null)
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00009728 File Offset: 0x00008728
		public OwnerHolder(Document doc, OwnerHolder owner)
		{
			this.m_doc = doc;
			this.ᜀ = owner;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000974C File Offset: 0x0000874C
		internal void ᜀ(OwnerHolder A_0)
		{
			for (;;)
			{
				IL_00:
				for (;;)
				{
					IL_3A:
					this.ᜀ = A_0;
					int num = 2;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								this.m_doc = A_0.Document;
								if (true)
								{
								}
								num = 1;
								continue;
							case 1:
								return;
							case 2:
								if (A_0 != null)
								{
									num = 0;
									continue;
								}
								return;
							}
							goto IL_3A;
						}
					}
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000097D0 File Offset: 0x000087D0
		internal virtual void OnStateChange(object sender)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ.OnStateChange(sender);
					if (true)
					{
					}
					num = 2;
					continue;
				case 2:
					return;
				}
				if (this.ᜀ == null)
				{
					break;
				}
				num = 0;
			}
		}

		// Token: 0x0400092B RID: 2347
		private int \u2460\u0084\u00A0\u008F;

		// Token: 0x0400092C RID: 2348
		protected Document m_doc;

		// Token: 0x0400092D RID: 2349
		private byte[] \u2609\u00A1\u009A\u0097;

		// Token: 0x0400092E RID: 2350
		private string[] \u25D9\u0096\u009C\u008D;

		// Token: 0x0400092F RID: 2351
		private int[] \u25D8\u00A5\u00AF\u00A0;

		// Token: 0x04000930 RID: 2352
		private bool \u2460\u0091\u0082\u0087;

		// Token: 0x04000931 RID: 2353
		private OwnerHolder ᜀ;
	}
}
