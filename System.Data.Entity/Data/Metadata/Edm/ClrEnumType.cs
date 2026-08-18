using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001D5 RID: 469
	internal sealed class ClrEnumType : EnumType
	{
		// Token: 0x06001FD0 RID: 8144 RVA: 0x0006F4DC File Offset: 0x0006D6DC
		internal ClrEnumType(Type clrType, string cspaceNamespaceName, string cspaceTypeName) : base(clrType)
		{
			this._type = clrType.TypeHandle;
			this._cspaceTypeName = cspaceNamespaceName + "." + cspaceTypeName;
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x0006F503 File Offset: 0x0006D703
		internal override Type ClrType
		{
			get
			{
				return Type.GetTypeFromHandle(this._type);
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x0006F510 File Offset: 0x0006D710
		internal string CSpaceTypeName
		{
			get
			{
				return this._cspaceTypeName;
			}
		}

		// Token: 0x04000E0E RID: 3598
		private readonly RuntimeTypeHandle _type;

		// Token: 0x04000E0F RID: 3599
		private readonly string _cspaceTypeName;
	}
}
