using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000CF RID: 207
	internal struct UserStringStreamReader
	{
		// Token: 0x06000874 RID: 2164 RVA: 0x000171C2 File Offset: 0x000153C2
		public UserStringStreamReader(MemoryBlock block)
		{
			this.Block = block;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000171CC File Offset: 0x000153CC
		internal string GetString(UserStringHandle handle)
		{
			int offset;
			int num;
			if (!this.Block.PeekHeapValueOffsetAndSize(handle.GetHeapOffset(), out offset, out num))
			{
				return string.Empty;
			}
			return this.Block.PeekUtf16(offset, num & -2);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00017210 File Offset: 0x00015410
		internal UserStringHandle GetNextHandle(UserStringHandle handle)
		{
			int num;
			int num2;
			if (!this.Block.PeekHeapValueOffsetAndSize(handle.GetHeapOffset(), out num, out num2))
			{
				return default(UserStringHandle);
			}
			int num3 = num + num2;
			if (num3 >= this.Block.Length)
			{
				return default(UserStringHandle);
			}
			return UserStringHandle.FromOffset(num3);
		}

		// Token: 0x040005B4 RID: 1460
		internal readonly MemoryBlock Block;
	}
}
