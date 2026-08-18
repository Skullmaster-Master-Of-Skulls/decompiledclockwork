using System;

namespace System.Web
{
	// Token: 0x020000FF RID: 255
	internal class SafeStringResource
	{
		// Token: 0x06000F48 RID: 3912 RVA: 0x0002BFD4 File Offset: 0x0002A1D4
		internal SafeStringResource(IntPtr stringResourcePointer, int resourceSize)
		{
			this._stringResourcePointer = stringResourcePointer;
			this._resourceSize = resourceSize;
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0002BFEA File Offset: 0x0002A1EA
		internal IntPtr StringResourcePointer
		{
			get
			{
				return this._stringResourcePointer;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x0002BFF2 File Offset: 0x0002A1F2
		internal int ResourceSize
		{
			get
			{
				return this._resourceSize;
			}
		}

		// Token: 0x040005DA RID: 1498
		private IntPtr _stringResourcePointer;

		// Token: 0x040005DB RID: 1499
		private int _resourceSize;
	}
}
