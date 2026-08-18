using System;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001C9 RID: 457
	internal sealed class ClrComplexType : ComplexType
	{
		// Token: 0x06001F57 RID: 8023 RVA: 0x0006E388 File Offset: 0x0006C588
		internal ClrComplexType(Type clrType, string cspaceNamespaceName, string cspaceTypeName) : base(EntityUtil.GenericCheckArgumentNull<Type>(clrType, "clrType").Name, clrType.Namespace ?? string.Empty, DataSpace.OSpace)
		{
			this._type = clrType.TypeHandle;
			this._cspaceTypeName = cspaceNamespaceName + "." + cspaceTypeName;
			base.Abstract = clrType.IsAbstract;
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x0006E3E8 File Offset: 0x0006C5E8
		internal static ClrComplexType CreateReadonlyClrComplexType(Type clrType, string cspaceNamespaceName, string cspaceTypeName)
		{
			ClrComplexType clrComplexType = new ClrComplexType(clrType, cspaceNamespaceName, cspaceTypeName);
			clrComplexType.SetReadOnly();
			return clrComplexType;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x0006E405 File Offset: 0x0006C605
		// (set) Token: 0x06001F5A RID: 8026 RVA: 0x0006E40D File Offset: 0x0006C60D
		internal Delegate Constructor
		{
			get
			{
				return this._constructor;
			}
			set
			{
				Interlocked.CompareExchange<Delegate>(ref this._constructor, value, null);
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x0006E41D File Offset: 0x0006C61D
		internal override Type ClrType
		{
			get
			{
				return Type.GetTypeFromHandle(this._type);
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001F5C RID: 8028 RVA: 0x0006E42A File Offset: 0x0006C62A
		internal string CSpaceTypeName
		{
			get
			{
				return this._cspaceTypeName;
			}
		}

		// Token: 0x04000D4B RID: 3403
		private readonly RuntimeTypeHandle _type;

		// Token: 0x04000D4C RID: 3404
		private Delegate _constructor;

		// Token: 0x04000D4D RID: 3405
		private readonly string _cspaceTypeName;
	}
}
