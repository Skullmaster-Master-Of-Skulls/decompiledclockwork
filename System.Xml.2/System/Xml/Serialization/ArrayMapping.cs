using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000150 RID: 336
	internal class ArrayMapping : TypeMapping
	{
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x000675ED File Offset: 0x000657ED
		// (set) Token: 0x06001771 RID: 6001 RVA: 0x000675F5 File Offset: 0x000657F5
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

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x00067608 File Offset: 0x00065808
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

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x0006766D File Offset: 0x0006586D
		// (set) Token: 0x06001774 RID: 6004 RVA: 0x00067675 File Offset: 0x00065875
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

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x0006767E File Offset: 0x0006587E
		// (set) Token: 0x06001776 RID: 6006 RVA: 0x00067686 File Offset: 0x00065886
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

		// Token: 0x04000ADE RID: 2782
		private ElementAccessor[] elements;

		// Token: 0x04000ADF RID: 2783
		private ElementAccessor[] sortedElements;

		// Token: 0x04000AE0 RID: 2784
		private ArrayMapping next;

		// Token: 0x04000AE1 RID: 2785
		private StructMapping topLevelMapping;
	}
}
