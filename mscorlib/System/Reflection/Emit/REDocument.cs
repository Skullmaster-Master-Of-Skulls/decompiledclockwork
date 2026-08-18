using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x0200082C RID: 2092
	internal class REDocument
	{
		// Token: 0x06004A78 RID: 19064 RVA: 0x00102A44 File Offset: 0x00101A44
		internal REDocument(ISymbolDocumentWriter document)
		{
			this.m_iLineNumberCount = 0;
			this.m_document = document;
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x00102A5C File Offset: 0x00101A5C
		internal void AddLineNumberInfo(ISymbolDocumentWriter document, int iOffset, int iStartLine, int iStartColumn, int iEndLine, int iEndColumn)
		{
			this.EnsureCapacity();
			this.m_iOffsets[this.m_iLineNumberCount] = iOffset;
			this.m_iLines[this.m_iLineNumberCount] = iStartLine;
			this.m_iColumns[this.m_iLineNumberCount] = iStartColumn;
			this.m_iEndLines[this.m_iLineNumberCount] = iEndLine;
			this.m_iEndColumns[this.m_iLineNumberCount] = iEndColumn;
			this.m_iLineNumberCount++;
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x00102AC8 File Offset: 0x00101AC8
		internal void EnsureCapacity()
		{
			if (this.m_iLineNumberCount == 0)
			{
				this.m_iOffsets = new int[16];
				this.m_iLines = new int[16];
				this.m_iColumns = new int[16];
				this.m_iEndLines = new int[16];
				this.m_iEndColumns = new int[16];
				return;
			}
			if (this.m_iLineNumberCount == this.m_iOffsets.Length)
			{
				int[] array = new int[this.m_iLineNumberCount * 2];
				Array.Copy(this.m_iOffsets, array, this.m_iLineNumberCount);
				this.m_iOffsets = array;
				array = new int[this.m_iLineNumberCount * 2];
				Array.Copy(this.m_iLines, array, this.m_iLineNumberCount);
				this.m_iLines = array;
				array = new int[this.m_iLineNumberCount * 2];
				Array.Copy(this.m_iColumns, array, this.m_iLineNumberCount);
				this.m_iColumns = array;
				array = new int[this.m_iLineNumberCount * 2];
				Array.Copy(this.m_iEndLines, array, this.m_iLineNumberCount);
				this.m_iEndLines = array;
				array = new int[this.m_iLineNumberCount * 2];
				Array.Copy(this.m_iEndColumns, array, this.m_iLineNumberCount);
				this.m_iEndColumns = array;
			}
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x00102BF8 File Offset: 0x00101BF8
		internal void EmitLineNumberInfo(ISymbolWriter symWriter)
		{
			if (this.m_iLineNumberCount == 0)
			{
				return;
			}
			int[] array = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iOffsets, array, this.m_iLineNumberCount);
			int[] array2 = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iLines, array2, this.m_iLineNumberCount);
			int[] array3 = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iColumns, array3, this.m_iLineNumberCount);
			int[] array4 = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iEndLines, array4, this.m_iLineNumberCount);
			int[] array5 = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iEndColumns, array5, this.m_iLineNumberCount);
			symWriter.DefineSequencePoints(this.m_document, array, array2, array3, array4, array5);
		}

		// Token: 0x04002618 RID: 9752
		internal const int InitialSize = 16;

		// Token: 0x04002619 RID: 9753
		internal int[] m_iOffsets;

		// Token: 0x0400261A RID: 9754
		internal int[] m_iLines;

		// Token: 0x0400261B RID: 9755
		internal int[] m_iColumns;

		// Token: 0x0400261C RID: 9756
		internal int[] m_iEndLines;

		// Token: 0x0400261D RID: 9757
		internal int[] m_iEndColumns;

		// Token: 0x0400261E RID: 9758
		internal ISymbolDocumentWriter m_document;

		// Token: 0x0400261F RID: 9759
		internal int m_iLineNumberCount;
	}
}
