using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D5 RID: 1237
	[SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
	internal sealed class ClrComplexType : ComplexType
	{
		// Token: 0x06002D94 RID: 11668 RVA: 0x000DC528 File Offset: 0x000DA728
		internal ClrComplexType(Type clrType, string cspaceNamespaceName, string cspaceTypeName) : base(Check.NotNull<Type>(clrType, "clrType").Name, clrType.NestingNamespace() ?? string.Empty, DataSpace.OSpace)
		{
			this._type = clrType;
			this._cspaceTypeName = cspaceNamespaceName + "." + cspaceTypeName;
			base.Abstract = clrType.IsAbstract();
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000DC580 File Offset: 0x000DA780
		internal static ClrComplexType CreateReadonlyClrComplexType(Type clrType, string cspaceNamespaceName, string cspaceTypeName)
		{
			ClrComplexType clrComplexType = new ClrComplexType(clrType, cspaceNamespaceName, cspaceTypeName);
			clrComplexType.SetReadOnly();
			return clrComplexType;
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x000DC59D File Offset: 0x000DA79D
		// (set) Token: 0x06002D97 RID: 11671 RVA: 0x000DC5A5 File Offset: 0x000DA7A5
		internal Func<object> Constructor
		{
			get
			{
				return this._constructor;
			}
			set
			{
				Interlocked.CompareExchange<Func<object>>(ref this._constructor, value, null);
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06002D98 RID: 11672 RVA: 0x000DC5B5 File Offset: 0x000DA7B5
		internal override Type ClrType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06002D99 RID: 11673 RVA: 0x000DC5BD File Offset: 0x000DA7BD
		internal string CSpaceTypeName
		{
			get
			{
				return this._cspaceTypeName;
			}
		}

		// Token: 0x040010DE RID: 4318
		private readonly Type _type;

		// Token: 0x040010DF RID: 4319
		private Func<object> _constructor;

		// Token: 0x040010E0 RID: 4320
		private readonly string _cspaceTypeName;
	}
}
