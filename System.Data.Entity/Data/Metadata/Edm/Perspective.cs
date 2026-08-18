using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000207 RID: 519
	internal abstract class Perspective
	{
		// Token: 0x06002264 RID: 8804 RVA: 0x0007901A File Offset: 0x0007721A
		internal Perspective(MetadataWorkspace metadataWorkspace, DataSpace targetDataspace)
		{
			EntityUtil.CheckArgumentNull<MetadataWorkspace>(metadataWorkspace, "metadataWorkspace");
			this.m_metadataWorkspace = metadataWorkspace;
			this.m_targetDataspace = targetDataspace;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0007903C File Offset: 0x0007723C
		internal virtual bool TryGetMember(StructuralType type, string memberName, bool ignoreCase, out EdmMember outMember)
		{
			EntityUtil.CheckArgumentNull<StructuralType>(type, "type");
			EntityUtil.CheckStringArgument(memberName, "memberName");
			outMember = null;
			return type.Members.TryGetValue(memberName, ignoreCase, out outMember);
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x00079068 File Offset: 0x00077268
		internal bool TryGetEnumMember(EnumType type, string memberName, bool ignoreCase, out EnumMember outMember)
		{
			EntityUtil.CheckArgumentNull<EnumType>(type, "type");
			EntityUtil.CheckStringArgument(memberName, "memberName");
			outMember = null;
			return type.Members.TryGetValue(memberName, ignoreCase, out outMember);
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x00079094 File Offset: 0x00077294
		internal bool TryGetExtent(EntityContainer entityContainer, string extentName, bool ignoreCase, out EntitySetBase outSet)
		{
			return entityContainer.BaseEntitySets.TryGetValue(extentName, ignoreCase, out outSet);
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x000790A8 File Offset: 0x000772A8
		internal bool TryGetFunctionImport(EntityContainer entityContainer, string functionImportName, bool ignoreCase, out EdmFunction functionImport)
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

		// Token: 0x06002269 RID: 8809 RVA: 0x00006174 File Offset: 0x00004374
		internal virtual EntityContainer GetDefaultContainer()
		{
			return null;
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x0007910F File Offset: 0x0007730F
		internal virtual bool TryGetEntityContainer(string name, bool ignoreCase, out EntityContainer entityContainer)
		{
			return this.MetadataWorkspace.TryGetEntityContainer(name, ignoreCase, this.TargetDataspace, out entityContainer);
		}

		// Token: 0x0600226B RID: 8811
		internal abstract bool TryGetTypeByName(string fullName, bool ignoreCase, out TypeUsage typeUsage);

		// Token: 0x0600226C RID: 8812 RVA: 0x00079128 File Offset: 0x00077328
		internal bool TryGetFunctionByName(string namespaceName, string functionName, bool ignoreCase, out IList<EdmFunction> functionOverloads)
		{
			EntityUtil.CheckStringArgument(namespaceName, "namespaceName");
			EntityUtil.CheckStringArgument(functionName, "functionName");
			string functionName2 = namespaceName + "." + functionName;
			ItemCollection itemCollection = this.m_metadataWorkspace.GetItemCollection(this.m_targetDataspace);
			IList<EdmFunction> list = (this.m_targetDataspace == DataSpace.SSpace) ? ((StoreItemCollection)itemCollection).GetCTypeFunctions(functionName2, ignoreCase) : itemCollection.GetFunctions(functionName2, ignoreCase);
			if (this.m_targetDataspace == DataSpace.CSpace)
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
				if ((list == null || list.Count == 0) && this.m_metadataWorkspace.TryGetItemCollection(DataSpace.SSpace, out itemCollection2))
				{
					list = ((StoreItemCollection)itemCollection2).GetCTypeFunctions(functionName2, ignoreCase);
				}
			}
			functionOverloads = ((list != null && list.Count > 0) ? list : null);
			return functionOverloads != null;
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x00079207 File Offset: 0x00077407
		internal MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this.m_metadataWorkspace;
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0007920F File Offset: 0x0007740F
		internal virtual bool TryGetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind, out PrimitiveType primitiveType)
		{
			primitiveType = this.m_metadataWorkspace.GetMappedPrimitiveType(primitiveTypeKind, DataSpace.CSpace);
			return primitiveType != null;
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x00079225 File Offset: 0x00077425
		internal DataSpace TargetDataspace
		{
			get
			{
				return this.m_targetDataspace;
			}
		}

		// Token: 0x04000EF0 RID: 3824
		private MetadataWorkspace m_metadataWorkspace;

		// Token: 0x04000EF1 RID: 3825
		private DataSpace m_targetDataspace;
	}
}
