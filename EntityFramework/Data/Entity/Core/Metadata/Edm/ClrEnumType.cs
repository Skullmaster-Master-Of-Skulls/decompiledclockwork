using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D0 RID: 1232
	[SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
	internal sealed class ClrEnumType : EnumType
	{
		// Token: 0x06002D83 RID: 11651 RVA: 0x000DC348 File Offset: 0x000DA548
		internal ClrEnumType(Type clrType, string cspaceNamespaceName, string cspaceTypeName) : base(clrType)
		{
			this._type = clrType;
			this._cspaceTypeName = cspaceNamespaceName + "." + cspaceTypeName;
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x000DC36A File Offset: 0x000DA56A
		internal override Type ClrType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x000DC372 File Offset: 0x000DA572
		internal string CSpaceTypeName
		{
			get
			{
				return this._cspaceTypeName;
			}
		}

		// Token: 0x040010AF RID: 4271
		private readonly Type _type;

		// Token: 0x040010B0 RID: 4272
		private readonly string _cspaceTypeName;
	}
}
