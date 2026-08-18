using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B0 RID: 1200
	internal abstract class Perspective
	{
		// Token: 0x06002C3A RID: 11322 RVA: 0x000D7301 File Offset: 0x000D5501
		internal Perspective(MetadataWorkspace metadataWorkspace, DataSpace targetDataspace)
		{
			this._metadataWorkspace = metadataWorkspace;
			this._targetDataspace = targetDataspace;
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000D7317 File Offset: 0x000D5517
		internal virtual bool TryGetMember(StructuralType type, string memberName, bool ignoreCase, out EdmMember outMember)
		{
			Check.NotEmpty(memberName, "memberName");
			outMember = null;
			return type.Members.TryGetValue(memberName, ignoreCase, out outMember);
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000D7338 File Offset: 0x000D5538
		internal virtual bool TryGetEnumMember(EnumType type, string memberName, bool ignoreCase, out EnumMember outMember)
		{
			Check.NotEmpty(memberName, "memberName");
			outMember = null;
			return type.Members.TryGetValue(memberName, ignoreCase, out outMember);
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000D7359 File Offset: 0x000D5559
		internal virtual bool TryGetExtent(EntityContainer entityContainer, string extentName, bool ignoreCase, out EntitySetBase outSet)
		{
			return entityContainer.BaseEntitySets.TryGetValue(extentName, ignoreCase, out outSet);
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000D739C File Offset: 0x000D559C
		internal virtual bool TryGetFunctionImport(EntityContainer entityContainer, string functionImportName, bool ignoreCase, out EdmFunction functionImport)
		{
			functionImport = null;
			if (ignoreCase)
			{
				functionImport = (from fi in entityContainer.FunctionImports
				where string.Equals(fi.Name, functionImportName, StringComparison.OrdinalIgnoreCase)
				select fi).SingleOrDefault<EdmFunction>();
			}
			else
			{
				functionImport = (from fi in entityContainer.FunctionImports
				where fi.Name == functionImportName
				select fi).SingleOrDefault<EdmFunction>();
			}
			return functionImport != null;
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000D7414 File Offset: 0x000D5614
		internal virtual EntityContainer GetDefaultContainer()
		{
			return null;
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000D7417 File Offset: 0x000D5617
		internal virtual bool TryGetEntityContainer(string name, bool ignoreCase, out EntityContainer entityContainer)
		{
			return this.MetadataWorkspace.TryGetEntityContainer(name, ignoreCase, this.TargetDataspace, out entityContainer);
		}

		// Token: 0x06002C41 RID: 11329
		internal abstract bool TryGetTypeByName(string fullName, bool ignoreCase, out TypeUsage typeUsage);

		// Token: 0x06002C42 RID: 11330 RVA: 0x000D7430 File Offset: 0x000D5630
		internal bool TryGetFunctionByName(string namespaceName, string functionName, bool ignoreCase, out IList<EdmFunction> functionOverloads)
		{
			Check.NotEmpty(namespaceName, "namespaceName");
			Check.NotEmpty(functionName, "functionName");
			string functionName2 = namespaceName + "." + functionName;
			ItemCollection itemCollection = this._metadataWorkspace.GetItemCollection(this._targetDataspace);
			IList<EdmFunction> list = (this._targetDataspace == DataSpace.SSpace) ? ((StoreItemCollection)itemCollection).GetCTypeFunctions(functionName2, ignoreCase) : itemCollection.GetFunctions(functionName2, ignoreCase);
			if (this._targetDataspace == DataSpace.CSpace)
			{
				EntityContainer entityContainer;
				EdmFunction edmFunction;
				if ((list == null || list.Count == 0) && this.TryGetEntityContainer(namespaceName, false, out entityContainer) && this.TryGetFunctionImport(entityContainer, functionName, false, out edmFunction))
				{
					list = new EdmFunction[]
					{
						edmFunction
					};
				}
				ItemCollection itemCollection2;
				if ((list == null || list.Count == 0) && this._metadataWorkspace.TryGetItemCollection(DataSpace.SSpace, out itemCollection2))
				{
					list = ((StoreItemCollection)itemCollection2).GetCTypeFunctions(functionName2, ignoreCase);
				}
			}
			functionOverloads = ((list != null && list.Count > 0) ? list : null);
			return functionOverloads != null;
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06002C43 RID: 11331 RVA: 0x000D7519 File Offset: 0x000D5719
		internal MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this._metadataWorkspace;
			}
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000D7521 File Offset: 0x000D5721
		internal virtual bool TryGetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind, out PrimitiveType primitiveType)
		{
			primitiveType = this._metadataWorkspace.GetMappedPrimitiveType(primitiveTypeKind, DataSpace.CSpace);
			return null != primitiveType;
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x000D753A File Offset: 0x000D573A
		internal DataSpace TargetDataspace
		{
			get
			{
				return this._targetDataspace;
			}
		}

		// Token: 0x04001053 RID: 4179
		private readonly MetadataWorkspace _metadataWorkspace;

		// Token: 0x04001054 RID: 4180
		private readonly DataSpace _targetDataspace;
	}
}
