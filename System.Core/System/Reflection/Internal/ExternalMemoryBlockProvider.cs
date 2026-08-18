using System;
using System.IO;
using System.Security;

namespace System.Reflection.Internal
{
	// Token: 0x0200007D RID: 125
	internal sealed class ExternalMemoryBlockProvider : MemoryBlockProvider
	{
		// Token: 0x0600031A RID: 794 RVA: 0x00007DBB File Offset: 0x00005FBB
		[SecurityCritical]
		public unsafe ExternalMemoryBlockProvider(byte* memory, int size)
		{
			this._memory = memory;
			this._size = size;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00007DD1 File Offset: 0x00005FD1
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00007DD9 File Offset: 0x00005FD9
		[SecuritySafeCritical]
		protected override AbstractMemoryBlock GetMemoryBlockImpl(int start, int size)
		{
			return new ExternalMemoryBlock(this, this._memory + start, size);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00007DEA File Offset: 0x00005FEA
		[SecuritySafeCritical]
		public override Stream GetStream(out StreamConstraints constraints)
		{
			constraints = new StreamConstraints(null, 0L, this._size);
			return new ReadOnlyUnmanagedMemoryStream(this._memory, this._size);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00007E11 File Offset: 0x00006011
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			this._memory = null;
			this._size = 0;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00007E22 File Offset: 0x00006022
		public unsafe byte* Pointer
		{
			[SecurityCritical]
			get
			{
				return this._memory;
			}
		}

		// Token: 0x0400047F RID: 1151
		[SecurityCritical]
		private unsafe byte* _memory;

		// Token: 0x04000480 RID: 1152
		private int _size;
	}
}
