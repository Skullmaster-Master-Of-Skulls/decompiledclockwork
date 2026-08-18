using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x020002D3 RID: 723
	[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
	public struct FieldMetadata
	{
		// Token: 0x0600195F RID: 6495 RVA: 0x0007E990 File Offset: 0x0007CB90
		public FieldMetadata(int ordinal, EdmMember fieldType)
		{
			if (ordinal < 0)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
			Check.NotNull<EdmMember>(fieldType, "fieldType");
			this._fieldType = fieldType;
			this._ordinal = ordinal;
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x0007E9BB File Offset: 0x0007CBBB
		public EdmMember FieldType
		{
			get
			{
				return this._fieldType;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x0007E9C3 File Offset: 0x0007CBC3
		public int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x040008B0 RID: 2224
		private readonly EdmMember _fieldType;

		// Token: 0x040008B1 RID: 2225
		private readonly int _ordinal;
	}
}
