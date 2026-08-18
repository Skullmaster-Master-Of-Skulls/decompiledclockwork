using System;
using System.Collections;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x020001EC RID: 492
	public class MapEnumerator : IEnumerator
	{
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x000F4554 File Offset: 0x000F3554
		object IEnumerator.Current
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

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x000F4598 File Offset: 0x000F3598
		public RBTreeNode Current
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

		// Token: 0x06001C29 RID: 7209 RVA: 0x000F45DC File Offset: 0x000F35DC
		public MapEnumerator(RBTreeNode parent)
		{
			int a_ = 1;
			base..ctor();
			if (parent == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("䜶堸䤺堼儾㕀", a_));
			}
			this.ᜁ = parent;
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x000F4618 File Offset: 0x000F3618
		public void Reset()
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
			this.ᜀ = null;
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x000F465C File Offset: 0x000F365C
		public bool MoveNext()
		{
			int num = 2;
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
						if (this.ᜀ != null)
						{
							num = 4;
							continue;
						}
						return false;
					case 1:
						this.ᜀ = this.ᜁ;
						if (true)
						{
						}
						num = 5;
						continue;
					case 3:
						goto IL_56;
					case 4:
						goto IL_6E;
					case 5:
						goto IL_56;
					}
					if (this.ᜀ == null)
					{
						num = 1;
						continue;
					}
					this.ᜀ = MapCollection.Inc(this.ᜀ);
					break;
					IL_56:
					num = 0;
					continue;
				}
				num = 3;
			}
			IL_6E:
			return !this.ᜀ.IsNil;
		}

		// Token: 0x04001071 RID: 4209
		private bool \u2609\u008E\u008Dª;

		// Token: 0x04001072 RID: 4210
		private RBTreeNode ᜀ;

		// Token: 0x04001073 RID: 4211
		private RBTreeNode ᜁ;
	}
}
