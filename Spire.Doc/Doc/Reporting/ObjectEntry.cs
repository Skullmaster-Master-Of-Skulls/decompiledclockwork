using System;
using Spire.Doc.Interface;

namespace Spire.Doc.Reporting
{
	// Token: 0x0200010A RID: 266
	public class ObjectEntry
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x000565E4 File Offset: 0x000555E4
		public ObjectEntry(DocumentObject ent)
		{
			this.Current = ent;
			this.Index = 0;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00056608 File Offset: 0x00055608
		public bool Fetch()
		{
			int num = 4;
			ICompositeObject compositeObject;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 7;
					continue;
				case 1:
					compositeObject = (this.Current.Owner as ICompositeObject);
					num = 3;
					continue;
				case 2:
					if (this.Current.Owner.IsComposite)
					{
						num = 1;
						continue;
					}
					goto IL_127;
				case 3:
					if (compositeObject.ChildObjects.Count > this.Index + 1)
					{
						num = 6;
						continue;
					}
					goto IL_127;
				case 5:
					goto IL_A7;
				case 6:
					goto IL_A5;
				case 7:
					if (this.Current.Owner != null)
					{
						num = 5;
						continue;
					}
					goto IL_127;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_A7:
					num = 2;
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					if (this.Current == null)
					{
						goto IL_127;
					}
					num = 0;
					break;
				}
			}
			IL_A5:
			this.Index++;
			this.Current = compositeObject.ChildObjects[this.Index];
			return true;
			IL_127:
			this.Current = null;
			this.Index = -1;
			return false;
		}

		// Token: 0x04000E25 RID: 3621
		private bool \u2609\u0097\u00A8\u0089;

		// Token: 0x04000E26 RID: 3622
		public DocumentObject Current;

		// Token: 0x04000E27 RID: 3623
		private float \u25D9ª\u00A1\u008F;

		// Token: 0x04000E28 RID: 3624
		private byte[] \u2593ª\u00AC\u00AF;

		// Token: 0x04000E29 RID: 3625
		private long[] \u2609\u00A3\u008A\u008A;

		// Token: 0x04000E2A RID: 3626
		public int Index;
	}
}
