using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x0200082A RID: 2090
	internal class ScopeTree
	{
		// Token: 0x06004A6C RID: 19052 RVA: 0x00102656 File Offset: 0x00101656
		internal ScopeTree()
		{
			this.m_iOpenScopeCount = 0;
			this.m_iCount = 0;
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x0010266C File Offset: 0x0010166C
		internal int GetCurrentActiveScopeIndex()
		{
			int num = 0;
			int num2 = this.m_iCount - 1;
			if (this.m_iCount == 0)
			{
				return -1;
			}
			while (num > 0 || this.m_ScopeActions[num2] == ScopeAction.Close)
			{
				if (this.m_ScopeActions[num2] == ScopeAction.Open)
				{
					num--;
				}
				else
				{
					num++;
				}
				num2--;
			}
			return num2;
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x001026B8 File Offset: 0x001016B8
		internal void AddLocalSymInfoToCurrentScope(string strName, byte[] signature, int slot, int startOffset, int endOffset)
		{
			int currentActiveScopeIndex = this.GetCurrentActiveScopeIndex();
			if (this.m_localSymInfos[currentActiveScopeIndex] == null)
			{
				this.m_localSymInfos[currentActiveScopeIndex] = new LocalSymInfo();
			}
			this.m_localSymInfos[currentActiveScopeIndex].AddLocalSymInfo(strName, signature, slot, startOffset, endOffset);
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x001026F8 File Offset: 0x001016F8
		internal void AddUsingNamespaceToCurrentScope(string strNamespace)
		{
			int currentActiveScopeIndex = this.GetCurrentActiveScopeIndex();
			if (this.m_localSymInfos[currentActiveScopeIndex] == null)
			{
				this.m_localSymInfos[currentActiveScopeIndex] = new LocalSymInfo();
			}
			this.m_localSymInfos[currentActiveScopeIndex].AddUsingNamespace(strNamespace);
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x00102734 File Offset: 0x00101734
		internal void AddScopeInfo(ScopeAction sa, int iOffset)
		{
			if (sa == ScopeAction.Close && this.m_iOpenScopeCount <= 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_UnmatchingSymScope"));
			}
			this.EnsureCapacity();
			this.m_ScopeActions[this.m_iCount] = sa;
			this.m_iOffsets[this.m_iCount] = iOffset;
			this.m_localSymInfos[this.m_iCount] = null;
			this.m_iCount++;
			if (sa == ScopeAction.Open)
			{
				this.m_iOpenScopeCount++;
				return;
			}
			this.m_iOpenScopeCount--;
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x001027BC File Offset: 0x001017BC
		internal void EnsureCapacity()
		{
			if (this.m_iCount == 0)
			{
				this.m_iOffsets = new int[16];
				this.m_ScopeActions = new ScopeAction[16];
				this.m_localSymInfos = new LocalSymInfo[16];
				return;
			}
			if (this.m_iCount == this.m_iOffsets.Length)
			{
				int[] array = new int[this.m_iCount * 2];
				Array.Copy(this.m_iOffsets, array, this.m_iCount);
				this.m_iOffsets = array;
				ScopeAction[] array2 = new ScopeAction[this.m_iCount * 2];
				Array.Copy(this.m_ScopeActions, array2, this.m_iCount);
				this.m_ScopeActions = array2;
				LocalSymInfo[] array3 = new LocalSymInfo[this.m_iCount * 2];
				Array.Copy(this.m_localSymInfos, array3, this.m_iCount);
				this.m_localSymInfos = array3;
			}
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x00102880 File Offset: 0x00101880
		internal void EmitScopeTree(ISymbolWriter symWriter)
		{
			for (int i = 0; i < this.m_iCount; i++)
			{
				if (this.m_ScopeActions[i] == ScopeAction.Open)
				{
					symWriter.OpenScope(this.m_iOffsets[i]);
				}
				else
				{
					symWriter.CloseScope(this.m_iOffsets[i]);
				}
				if (this.m_localSymInfos[i] != null)
				{
					this.m_localSymInfos[i].EmitLocalSymInfo(symWriter);
				}
			}
		}

		// Token: 0x0400260E RID: 9742
		internal const int InitialSize = 16;

		// Token: 0x0400260F RID: 9743
		internal int[] m_iOffsets;

		// Token: 0x04002610 RID: 9744
		internal ScopeAction[] m_ScopeActions;

		// Token: 0x04002611 RID: 9745
		internal int m_iCount;

		// Token: 0x04002612 RID: 9746
		internal int m_iOpenScopeCount;

		// Token: 0x04002613 RID: 9747
		internal LocalSymInfo[] m_localSymInfos;
	}
}
