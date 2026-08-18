using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000068 RID: 104
	internal static class MetadataTokens
	{
		// Token: 0x060002EE RID: 750 RVA: 0x0000788A File Offset: 0x00005A8A
		public static int GetHeapOffset(Handle handle)
		{
			if (!handle.IsHeapHandle)
			{
				Throw.HeapHandleRequired();
			}
			if (handle.IsVirtual)
			{
				return -1;
			}
			return handle.Offset;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000078AC File Offset: 0x00005AAC
		public static int GetToken(Handle handle)
		{
			if (!handle.IsEntityOrUserStringHandle)
			{
				Throw.EntityOrUserStringHandleRequired();
			}
			if (handle.IsVirtual)
			{
				return 0;
			}
			return handle.Token;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000078CE File Offset: 0x00005ACE
		public static bool TryGetTableIndex(HandleKind type, out TableIndex index)
		{
			if ((int)type < MetadataTokens.TableCount && (1L << (int)type & 71811071505072127L) != 0L)
			{
				index = (TableIndex)type;
				return true;
			}
			index = TableIndex.Module;
			return false;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000078F4 File Offset: 0x00005AF4
		public static Handle Handle(int token)
		{
			if (!TokenTypeIds.IsEntityOrUserStringToken((uint)token))
			{
				Throw.InvalidToken();
			}
			return System.Reflection.Metadata.Handle.FromVToken((uint)token);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00007909 File Offset: 0x00005B09
		private static int ToRowId(int rowNumber)
		{
			return rowNumber & 16777215;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00007912 File Offset: 0x00005B12
		public static MethodDefinitionHandle MethodDefinitionHandle(int rowNumber)
		{
			return System.Reflection.Metadata.MethodDefinitionHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000791F File Offset: 0x00005B1F
		public static MethodDebugInformationHandle MethodDebugInformationHandle(int rowNumber)
		{
			return System.Reflection.Metadata.MethodDebugInformationHandle.FromRowId(MetadataTokens.ToRowId(rowNumber));
		}

		// Token: 0x04000368 RID: 872
		public static readonly int TableCount = 64;
	}
}
