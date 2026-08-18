using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004FD RID: 1277
	internal struct StackFrame
	{
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x000B8D1D File Offset: 0x000B6F1D
		internal int Count
		{
			get
			{
				return this.endPtr - this.basePtr + 1;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (set) Token: 0x0600304B RID: 12363 RVA: 0x000B8D2E File Offset: 0x000B6F2E
		internal int EndPtr
		{
			set
			{
				this.endPtr = value;
			}
		}

		// Token: 0x17000B77 RID: 2935
		internal int this[int offset]
		{
			get
			{
				return this.basePtr + offset;
			}
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000B8D41 File Offset: 0x000B6F41
		internal bool IsValidPtr(int ptr)
		{
			return ptr >= this.basePtr && ptr <= this.endPtr;
		}

		// Token: 0x040025F7 RID: 9719
		internal int basePtr;

		// Token: 0x040025F8 RID: 9720
		internal int endPtr;
	}
}
