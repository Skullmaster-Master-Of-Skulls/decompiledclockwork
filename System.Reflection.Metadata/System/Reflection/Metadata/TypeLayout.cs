using System;

namespace System.Reflection.Metadata
{
	// Token: 0x020000B0 RID: 176
	public struct TypeLayout
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x00010567 File Offset: 0x0000E767
		public TypeLayout(int size, int packingSize)
		{
			this._size = size;
			this._packingSize = packingSize;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00010577 File Offset: 0x0000E777
		public int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001057F File Offset: 0x0000E77F
		public int PackingSize
		{
			get
			{
				return this._packingSize;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00010587 File Offset: 0x0000E787
		public bool IsDefault
		{
			get
			{
				return this._size == 0 && this._packingSize == 0;
			}
		}

		// Token: 0x04000470 RID: 1136
		private readonly int _size;

		// Token: 0x04000471 RID: 1137
		private readonly int _packingSize;
	}
}
