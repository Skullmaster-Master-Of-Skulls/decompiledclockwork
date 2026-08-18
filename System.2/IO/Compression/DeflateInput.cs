using System;

namespace System.IO.Compression
{
	// Token: 0x0200041E RID: 1054
	internal class DeflateInput
	{
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002774 RID: 10100 RVA: 0x000B5DAB File Offset: 0x000B3FAB
		// (set) Token: 0x06002775 RID: 10101 RVA: 0x000B5DB3 File Offset: 0x000B3FB3
		internal byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
			set
			{
				this.buffer = value;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x000B5DBC File Offset: 0x000B3FBC
		// (set) Token: 0x06002777 RID: 10103 RVA: 0x000B5DC4 File Offset: 0x000B3FC4
		internal int Count
		{
			get
			{
				return this.count;
			}
			set
			{
				this.count = value;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06002778 RID: 10104 RVA: 0x000B5DCD File Offset: 0x000B3FCD
		// (set) Token: 0x06002779 RID: 10105 RVA: 0x000B5DD5 File Offset: 0x000B3FD5
		internal int StartIndex
		{
			get
			{
				return this.startIndex;
			}
			set
			{
				this.startIndex = value;
			}
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000B5DDE File Offset: 0x000B3FDE
		internal void ConsumeBytes(int n)
		{
			this.startIndex += n;
			this.count -= n;
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000B5DFC File Offset: 0x000B3FFC
		internal DeflateInput.InputState DumpState()
		{
			DeflateInput.InputState result;
			result.count = this.count;
			result.startIndex = this.startIndex;
			return result;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000B5E24 File Offset: 0x000B4024
		internal void RestoreState(DeflateInput.InputState state)
		{
			this.count = state.count;
			this.startIndex = state.startIndex;
		}

		// Token: 0x04002174 RID: 8564
		private byte[] buffer;

		// Token: 0x04002175 RID: 8565
		private int count;

		// Token: 0x04002176 RID: 8566
		private int startIndex;

		// Token: 0x02000816 RID: 2070
		internal struct InputState
		{
			// Token: 0x0400359A RID: 13722
			internal int count;

			// Token: 0x0400359B RID: 13723
			internal int startIndex;
		}
	}
}
