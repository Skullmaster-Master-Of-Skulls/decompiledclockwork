using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004FC RID: 1276
	public class RefType : EdmType
	{
		// Token: 0x06002F7E RID: 12158 RVA: 0x000E4527 File Offset: 0x000E2727
		internal RefType()
		{
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000E452F File Offset: 0x000E272F
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal RefType(EntityType entityType) : base(RefType.GetIdentity(Check.NotNull<EntityType>(entityType, "entityType")), "Transient", entityType.DataSpace)
		{
			this._elementType = entityType;
			this.SetReadOnly();
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002F80 RID: 12160 RVA: 0x000E455F File Offset: 0x000E275F
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.RefType;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002F81 RID: 12161 RVA: 0x000E4563 File Offset: 0x000E2763
		[MetadataProperty(BuiltInTypeKind.EntityTypeBase, false)]
		public virtual EntityTypeBase ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000E456C File Offset: 0x000E276C
		private static string GetIdentity(EntityTypeBase entityTypeBase)
		{
			StringBuilder stringBuilder = new StringBuilder(50);
			stringBuilder.Append("reference[");
			entityTypeBase.BuildIdentity(stringBuilder);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000E45A6 File Offset: 0x000E27A6
		public override int GetHashCode()
		{
			return this._elementType.GetHashCode() * 397 ^ typeof(RefType).GetHashCode();
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000E45CC File Offset: 0x000E27CC
		public override bool Equals(object obj)
		{
			RefType refType = obj as RefType;
			return refType != null && object.ReferenceEquals(refType._elementType, this._elementType);
		}

		// Token: 0x04001225 RID: 4645
		private readonly EntityTypeBase _elementType;
	}
}
