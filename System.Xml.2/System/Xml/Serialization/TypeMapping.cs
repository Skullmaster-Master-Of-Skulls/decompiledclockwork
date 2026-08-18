using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200014D RID: 333
	internal abstract class TypeMapping : Mapping
	{
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x000674DE File Offset: 0x000656DE
		// (set) Token: 0x06001757 RID: 5975 RVA: 0x000674E6 File Offset: 0x000656E6
		internal bool ReferencedByTopLevelElement
		{
			get
			{
				return this.referencedByTopLevelElement;
			}
			set
			{
				this.referencedByTopLevelElement = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x000674EF File Offset: 0x000656EF
		// (set) Token: 0x06001759 RID: 5977 RVA: 0x00067501 File Offset: 0x00065701
		internal bool ReferencedByElement
		{
			get
			{
				return this.referencedByElement || this.referencedByTopLevelElement;
			}
			set
			{
				this.referencedByElement = value;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x0006750A File Offset: 0x0006570A
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x00067512 File Offset: 0x00065712
		internal string Namespace
		{
			get
			{
				return this.typeNs;
			}
			set
			{
				this.typeNs = value;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x0006751B File Offset: 0x0006571B
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x00067523 File Offset: 0x00065723
		internal string TypeName
		{
			get
			{
				return this.typeName;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x0006752C File Offset: 0x0006572C
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x00067534 File Offset: 0x00065734
		internal TypeDesc TypeDesc
		{
			get
			{
				return this.typeDesc;
			}
			set
			{
				this.typeDesc = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x0006753D File Offset: 0x0006573D
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x00067545 File Offset: 0x00065745
		internal bool IncludeInSchema
		{
			get
			{
				return this.includeInSchema;
			}
			set
			{
				this.includeInSchema = value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x0006754E File Offset: 0x0006574E
		// (set) Token: 0x06001763 RID: 5987 RVA: 0x00067551 File Offset: 0x00065751
		internal virtual bool IsList
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x00067553 File Offset: 0x00065753
		// (set) Token: 0x06001765 RID: 5989 RVA: 0x0006755B File Offset: 0x0006575B
		internal bool IsReference
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x00067564 File Offset: 0x00065764
		internal bool IsAnonymousType
		{
			get
			{
				return this.typeName == null || this.typeName.Length == 0;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x0006757E File Offset: 0x0006577E
		internal virtual string DefaultElementName
		{
			get
			{
				if (!this.IsAnonymousType)
				{
					return this.typeName;
				}
				return XmlConvert.EncodeLocalName(this.typeDesc.Name);
			}
		}

		// Token: 0x04000AD5 RID: 2773
		private TypeDesc typeDesc;

		// Token: 0x04000AD6 RID: 2774
		private string typeNs;

		// Token: 0x04000AD7 RID: 2775
		private string typeName;

		// Token: 0x04000AD8 RID: 2776
		private bool referencedByElement;

		// Token: 0x04000AD9 RID: 2777
		private bool referencedByTopLevelElement;

		// Token: 0x04000ADA RID: 2778
		private bool includeInSchema = true;

		// Token: 0x04000ADB RID: 2779
		private bool reference;
	}
}
