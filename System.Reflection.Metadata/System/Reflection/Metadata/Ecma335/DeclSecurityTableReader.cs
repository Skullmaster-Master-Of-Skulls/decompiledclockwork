using System;
using System.Reflection.Internal;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000EF RID: 239
	internal struct DeclSecurityTableReader
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x00018F8C File Offset: 0x0001718C
		internal DeclSecurityTableReader(int numberOfRows, bool declaredSorted, int hasDeclSecurityRefSize, int blobHeapRefSize, MemoryBlock containingBlock, int containingBlockOffset)
		{
			this.NumberOfRows = numberOfRows;
			this._IsHasDeclSecurityRefSizeSmall = (hasDeclSecurityRefSize == 2);
			this._IsBlobHeapRefSizeSmall = (blobHeapRefSize == 2);
			this._ActionOffset = 0;
			this._ParentOffset = this._ActionOffset + 2;
			this._PermissionSetOffset = this._ParentOffset + hasDeclSecurityRefSize;
			this.RowSize = this._PermissionSetOffset + blobHeapRefSize;
			this.Block = containingBlock.GetMemoryBlockAt(containingBlockOffset, this.RowSize * numberOfRows);
			if (!declaredSorted && !this.CheckSorted())
			{
				Throw.TableNotSorted(TableIndex.DeclSecurity);
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00019010 File Offset: 0x00017210
		internal DeclarativeSecurityAction GetAction(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return (DeclarativeSecurityAction)this.Block.PeekUInt16(num + this._ActionOffset);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00019040 File Offset: 0x00017240
		internal EntityHandle GetParent(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return HasDeclSecurityTag.ConvertToHandle(this.Block.PeekTaggedReference(num + this._ParentOffset, this._IsHasDeclSecurityRefSizeSmall));
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001907C File Offset: 0x0001727C
		internal BlobHandle GetPermissionSet(int rowId)
		{
			int num = (rowId - 1) * this.RowSize;
			return BlobHandle.FromOffset(this.Block.PeekHeapReference(num + this._PermissionSetOffset, this._IsBlobHeapRefSizeSmall));
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x000190B8 File Offset: 0x000172B8
		internal void GetAttributeRange(EntityHandle parentToken, out int firstImplRowId, out int lastImplRowId)
		{
			int num;
			int num2;
			this.Block.BinarySearchReferenceRange(this.NumberOfRows, this.RowSize, this._ParentOffset, HasDeclSecurityTag.ConvertToTag(parentToken), this._IsHasDeclSecurityRefSizeSmall, out num, out num2);
			if (num == -1)
			{
				firstImplRowId = 1;
				lastImplRowId = 0;
				return;
			}
			firstImplRowId = num + 1;
			lastImplRowId = num2 + 1;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001910C File Offset: 0x0001730C
		private bool CheckSorted()
		{
			return this.Block.IsOrderedByReferenceAscending(this.RowSize, this._ParentOffset, this._IsHasDeclSecurityRefSizeSmall);
		}

		// Token: 0x0400070D RID: 1805
		internal readonly int NumberOfRows;

		// Token: 0x0400070E RID: 1806
		private readonly bool _IsHasDeclSecurityRefSizeSmall;

		// Token: 0x0400070F RID: 1807
		private readonly bool _IsBlobHeapRefSizeSmall;

		// Token: 0x04000710 RID: 1808
		private readonly int _ActionOffset;

		// Token: 0x04000711 RID: 1809
		private readonly int _ParentOffset;

		// Token: 0x04000712 RID: 1810
		private readonly int _PermissionSetOffset;

		// Token: 0x04000713 RID: 1811
		internal readonly int RowSize;

		// Token: 0x04000714 RID: 1812
		internal readonly MemoryBlock Block;
	}
}
