using System;
using Spire.Doc.Interface;

namespace Spire.Doc.Reporting
{
	// Token: 0x0200010B RID: 267
	public class EntityEntry
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x0005674C File Offset: 0x0005574C
		public EntityEntry(DocumentObject ent)
		{
			this.Current = ent;
			this.Index = 0;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00056770 File Offset: 0x00055770
		public bool Fetch()
		{
			int num = 5;
			ICompositeObject compositeObject;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					compositeObject = (this.Current.Owner as ICompositeObject);
					num = 2;
					continue;
				case 1:
					num = 7;
					continue;
				case 2:
					if (compositeObject.ChildObjects.Count > this.Index + 1)
					{
						num = 6;
						continue;
					}
					goto IL_12A;
				case 3:
					num = 4;
					continue;
				case 4:
					if (this.Current.Owner != null)
					{
						num = 1;
						continue;
					}
					goto IL_12A;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B2;
					default:
						goto IL_9F;
					}
					break;
				case 7:
					goto IL_B2;
				}
				if (this.Current != null)
				{
					num = 3;
					continue;
				}
				goto IL_12A;
				IL_B2:
				if (!this.Current.Owner.IsComposite)
				{
					goto IL_12A;
				}
				num = 0;
			}
			IL_9F:
			if (false)
			{
			}
			this.Index++;
			this.Current = compositeObject.ChildObjects[this.Index];
			return true;
			IL_12A:
			this.Current = null;
			this.Index = -1;
			return false;
		}

		// Token: 0x04000E2B RID: 3627
		private int \u25D8\u007F\u008E\u00AC;

		// Token: 0x04000E2C RID: 3628
		private float \u25D9\u009F\u009E\u0084;

		// Token: 0x04000E2D RID: 3629
		public DocumentObject Current;

		// Token: 0x04000E2E RID: 3630
		private float[] \u2609\u0099\u00AD\u0080;

		// Token: 0x04000E2F RID: 3631
		private int \u25D9\u0080\u00A8\u009A;

		// Token: 0x04000E30 RID: 3632
		public int Index;
	}
}
