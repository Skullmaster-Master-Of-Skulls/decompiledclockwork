using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003AF RID: 943
	public sealed class FunctionImportComplexTypeMapping : FunctionImportStructuralTypeMapping
	{
		// Token: 0x0600225D RID: 8797 RVA: 0x000A0B07 File Offset: 0x0009ED07
		public FunctionImportComplexTypeMapping(ComplexType returnType, Collection<FunctionImportReturnTypePropertyMapping> properties) : this(Check.NotNull<ComplexType>(returnType, "returnType"), Check.NotNull<Collection<FunctionImportReturnTypePropertyMapping>>(properties, "properties"), LineInfo.Empty)
		{
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000A0B2A File Offset: 0x0009ED2A
		internal FunctionImportComplexTypeMapping(ComplexType returnType, Collection<FunctionImportReturnTypePropertyMapping> properties, LineInfo lineInfo) : base(properties, lineInfo)
		{
			this._returnType = returnType;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x000A0B3B File Offset: 0x0009ED3B
		public ComplexType ReturnType
		{
			get
			{
				return this._returnType;
			}
		}

		// Token: 0x04000C1E RID: 3102
		private readonly ComplexType _returnType;
	}
}
