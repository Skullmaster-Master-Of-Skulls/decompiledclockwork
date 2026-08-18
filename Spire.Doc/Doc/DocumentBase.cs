using System;

namespace Spire.Doc
{
	// Token: 0x02000091 RID: 145
	public abstract class DocumentBase : DocumentObject, spr\u1AB8
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000A33C File Offset: 0x0000933C
		spr\u1D30 spr\u1AB8.LayoutInfo
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_6A;
					case 2:
						this.CreateLayoutInfo();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					if (true)
					{
					}
					if (this.ᜀ != null)
					{
						break;
					}
					num = 2;
				}
				IL_6A:
				return this.ᜀ;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000A3BC File Offset: 0x000093BC
		public DocumentBase(Document doc, DocumentObject owner) : base(doc, owner)
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000A3D4 File Offset: 0x000093D4
		void spr\u1AB8.Draw(spr\u19E0 dc, sprᦰ ltWidget)
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
			this.DrawImpl(dc, ltWidget);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000A418 File Offset: 0x00009418
		internal virtual void DrawImpl(spr\u19E0 dc, sprᦰ ltWidget)
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
		}

		// Token: 0x060000A8 RID: 168
		protected abstract void CreateLayoutInfo();

		// Token: 0x04000938 RID: 2360
		private bool \u2460\u0098\u007F\u009B;

		// Token: 0x04000939 RID: 2361
		private long \u2593\u00A3\u00A8\u00A1;

		// Token: 0x0400093A RID: 2362
		internal new spr\u1D30 ᜀ;
	}
}
