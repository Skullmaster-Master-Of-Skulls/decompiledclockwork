using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Xml;

namespace System.Data.Mapping
{
	// Token: 0x0200025E RID: 606
	internal class FunctionImportReturnTypeStructuralTypeColumnRenameMapping
	{
		// Token: 0x0600258A RID: 9610 RVA: 0x0008C040 File Offset: 0x0008A240
		internal FunctionImportReturnTypeStructuralTypeColumnRenameMapping(string defaultMemberName)
		{
			this._defaultMemberName = defaultMemberName;
			this._columnListForType = new Collection<FunctionImportReturnTypeStructuralTypeColumn>();
			this._columnListForIsTypeOfType = new Collection<FunctionImportReturnTypeStructuralTypeColumn>();
			this._renameCache = new Memoizer<StructuralType, FunctionImportReturnTypeStructuralTypeColumn>(new Func<StructuralType, FunctionImportReturnTypeStructuralTypeColumn>(this.GetRename), EqualityComparer<StructuralType>.Default);
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x0008C08C File Offset: 0x0008A28C
		internal string GetRename(EdmType type)
		{
			IXmlLineInfo xmlLineInfo;
			return this.GetRename(type, out xmlLineInfo);
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x0008C0A4 File Offset: 0x0008A2A4
		internal string GetRename(EdmType type, out IXmlLineInfo lineInfo)
		{
			EntityUtil.CheckArgumentNull<EdmType>(type, "type");
			FunctionImportReturnTypeStructuralTypeColumn functionImportReturnTypeStructuralTypeColumn = this._renameCache.Evaluate(type as StructuralType);
			lineInfo = functionImportReturnTypeStructuralTypeColumn.LineInfo;
			return functionImportReturnTypeStructuralTypeColumn.ColumnName;
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x0008C0E0 File Offset: 0x0008A2E0
		private FunctionImportReturnTypeStructuralTypeColumn GetRename(StructuralType typeForRename)
		{
			FunctionImportReturnTypeStructuralTypeColumn functionImportReturnTypeStructuralTypeColumn = this._columnListForType.FirstOrDefault((FunctionImportReturnTypeStructuralTypeColumn t) => t.Type == typeForRename);
			if (functionImportReturnTypeStructuralTypeColumn != null)
			{
				return functionImportReturnTypeStructuralTypeColumn;
			}
			FunctionImportReturnTypeStructuralTypeColumn functionImportReturnTypeStructuralTypeColumn2 = (from t in this._columnListForIsTypeOfType
			where t.Type == typeForRename
			select t).LastOrDefault<FunctionImportReturnTypeStructuralTypeColumn>();
			if (functionImportReturnTypeStructuralTypeColumn2 != null)
			{
				return functionImportReturnTypeStructuralTypeColumn2;
			}
			IEnumerable<FunctionImportReturnTypeStructuralTypeColumn> enumerable = from t in this._columnListForIsTypeOfType
			where t.Type.IsAssignableFrom(typeForRename)
			select t;
			if (enumerable.Count<FunctionImportReturnTypeStructuralTypeColumn>() == 0)
			{
				return new FunctionImportReturnTypeStructuralTypeColumn(this._defaultMemberName, typeForRename, false, null);
			}
			return this.GetLowestParentInHierachy(enumerable);
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x0008C174 File Offset: 0x0008A374
		private FunctionImportReturnTypeStructuralTypeColumn GetLowestParentInHierachy(IEnumerable<FunctionImportReturnTypeStructuralTypeColumn> nodesInHierachy)
		{
			FunctionImportReturnTypeStructuralTypeColumn functionImportReturnTypeStructuralTypeColumn = null;
			foreach (FunctionImportReturnTypeStructuralTypeColumn functionImportReturnTypeStructuralTypeColumn2 in nodesInHierachy)
			{
				if (functionImportReturnTypeStructuralTypeColumn == null)
				{
					functionImportReturnTypeStructuralTypeColumn = functionImportReturnTypeStructuralTypeColumn2;
				}
				else if (functionImportReturnTypeStructuralTypeColumn.Type.IsAssignableFrom(functionImportReturnTypeStructuralTypeColumn2.Type))
				{
					functionImportReturnTypeStructuralTypeColumn = functionImportReturnTypeStructuralTypeColumn2;
				}
			}
			return functionImportReturnTypeStructuralTypeColumn;
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x0008C1D4 File Offset: 0x0008A3D4
		internal void AddRename(FunctionImportReturnTypeStructuralTypeColumn renamedColumn)
		{
			EntityUtil.CheckArgumentNull<FunctionImportReturnTypeStructuralTypeColumn>(renamedColumn, "renamedColumn");
			if (!renamedColumn.IsTypeOf)
			{
				this._columnListForType.Add(renamedColumn);
				return;
			}
			this._columnListForIsTypeOfType.Add(renamedColumn);
		}

		// Token: 0x04001135 RID: 4405
		private Collection<FunctionImportReturnTypeStructuralTypeColumn> _columnListForType;

		// Token: 0x04001136 RID: 4406
		private Collection<FunctionImportReturnTypeStructuralTypeColumn> _columnListForIsTypeOfType;

		// Token: 0x04001137 RID: 4407
		private readonly string _defaultMemberName;

		// Token: 0x04001138 RID: 4408
		private Memoizer<StructuralType, FunctionImportReturnTypeStructuralTypeColumn> _renameCache;
	}
}
