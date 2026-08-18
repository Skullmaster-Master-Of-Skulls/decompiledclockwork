using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001C1 RID: 449
	public sealed class AssociationSet : RelationshipSet
	{
		// Token: 0x06001F2C RID: 7980 RVA: 0x0006E033 File Offset: 0x0006C233
		internal AssociationSet(string name, AssociationType associationType) : base(name, null, null, null, associationType)
		{
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x0006E050 File Offset: 0x0006C250
		public new AssociationType ElementType
		{
			get
			{
				return (AssociationType)base.ElementType;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001F2E RID: 7982 RVA: 0x0006E05D File Offset: 0x0006C25D
		[MetadataProperty(BuiltInTypeKind.AssociationSetEnd, true)]
		public ReadOnlyMetadataCollection<AssociationSetEnd> AssociationSetEnds
		{
			get
			{
				return this._associationSetEnds;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x00033532 File Offset: 0x00031732
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationSet;
			}
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x0006E065 File Offset: 0x0006C265
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.AssociationSetEnds.Source.SetReadOnly();
			}
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0006E086 File Offset: 0x0006C286
		internal void AddAssociationSetEnd(AssociationSetEnd associationSetEnd)
		{
			this.AssociationSetEnds.Source.Add(associationSetEnd);
		}

		// Token: 0x04000D15 RID: 3349
		private readonly ReadOnlyMetadataCollection<AssociationSetEnd> _associationSetEnds = new ReadOnlyMetadataCollection<AssociationSetEnd>(new MetadataCollection<AssociationSetEnd>());
	}
}
