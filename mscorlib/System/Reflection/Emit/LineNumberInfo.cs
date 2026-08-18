using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x0200082B RID: 2091
	internal class LineNumberInfo
	{
		// Token: 0x06004A73 RID: 19059 RVA: 0x001028DF File Offset: 0x001018DF
		internal LineNumberInfo()
		{
			this.m_DocumentCount = 0;
			this.m_iLastFound = 0;
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x001028F8 File Offset: 0x001018F8
		internal void AddLineNumberInfo(ISymbolDocumentWriter document, int iOffset, int iStartLine, int iStartColumn, int iEndLine, int iEndColumn)
		{
			int num = this.FindDocument(document);
			this.m_Documents[num].AddLineNumberInfo(document, iOffset, iStartLine, iStartColumn, iEndLine, iEndColumn);
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x00102924 File Offset: 0x00101924
		internal int FindDocument(ISymbolDocumentWriter document)
		{
			if (this.m_iLastFound < this.m_DocumentCount && this.m_Documents[this.m_iLastFound] == document)
			{
				return this.m_iLastFound;
			}
			for (int i = 0; i < this.m_DocumentCount; i++)
			{
				if (this.m_Documents[i].m_document == document)
				{
					this.m_iLastFound = i;
					return this.m_iLastFound;
				}
			}
			this.EnsureCapacity();
			this.m_iLastFound = this.m_DocumentCount;
			this.m_Documents[this.m_DocumentCount++] = new REDocument(document);
			return this.m_iLastFound;
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x001029BC File Offset: 0x001019BC
		internal void EnsureCapacity()
		{
			if (this.m_DocumentCount == 0)
			{
				this.m_Documents = new REDocument[16];
				return;
			}
			if (this.m_DocumentCount == this.m_Documents.Length)
			{
				REDocument[] array = new REDocument[this.m_DocumentCount * 2];
				Array.Copy(this.m_Documents, array, this.m_DocumentCount);
				this.m_Documents = array;
			}
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x00102A18 File Offset: 0x00101A18
		internal void EmitLineNumberInfo(ISymbolWriter symWriter)
		{
			for (int i = 0; i < this.m_DocumentCount; i++)
			{
				this.m_Documents[i].EmitLineNumberInfo(symWriter);
			}
		}

		// Token: 0x04002614 RID: 9748
		internal const int InitialSize = 16;

		// Token: 0x04002615 RID: 9749
		internal int m_DocumentCount;

		// Token: 0x04002616 RID: 9750
		internal REDocument[] m_Documents;

		// Token: 0x04002617 RID: 9751
		private int m_iLastFound;
	}
}
