using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002C9 RID: 713
	internal class ArrayMapping : TypeMapping
	{
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x0009F30B File Offset: 0x0009E30B
		// (set) Token: 0x060021C4 RID: 8644 RVA: 0x0009F313 File Offset: 0x0009E313
		internal ElementAccessor[] Elements
		{
			get
			{
				return this.elements;
			}
			set
			{
				this.elements = value;
				this.sortedElements = null;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x0009F324 File Offset: 0x0009E324
		internal ElementAccessor[] ElementsSortedByDerivation
		{
			get
			{
				if (this.sortedElements != null)
				{
					return this.sortedElements;
				}
				if (this.elements == null)
				{
					return null;
				}
				this.sortedElements = new ElementAccessor[this.elements.Length];
				Array.Copy(this.elements, 0, this.sortedElements, 0, this.elements.Length);
				AccessorMapping.SortMostToLeastDerived(this.sortedElements);
				return this.sortedElements;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x0009F389 File Offset: 0x0009E389
		// (set) Token: 0x060021C7 RID: 8647 RVA: 0x0009F391 File Offset: 0x0009E391
		internal ArrayMapping Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x0009F39A File Offset: 0x0009E39A
		// (set) Token: 0x060021C9 RID: 8649 RVA: 0x0009F3A2 File Offset: 0x0009E3A2
		internal StructMapping TopLevelMapping
		{
			get
			{
				return this.topLevelMapping;
			}
			set
			{
				this.topLevelMapping = value;
			}
		}

		// Token: 0x04001478 RID: 5240
		private ElementAccessor[] elements;

		// Token: 0x04001479 RID: 5241
		private ElementAccessor[] sortedElements;

		// Token: 0x0400147A RID: 5242
		private ArrayMapping next;

		// Token: 0x0400147B RID: 5243
		private StructMapping topLevelMapping;
	}
}
