using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003B5 RID: 949
	internal class FunctionImportReturnTypeStructuralTypeColumnRenameMapping
	{
		// Token: 0x0600227D RID: 8829 RVA: 0x000A0F34 File Offset: 0x0009F134
		internal FunctionImportReturnTypeStructuralTypeColumnRenameMapping(string defaultMemberName)
		{
			this._defaultMemberName = defaultMemberName;
			this._columnListForType = new Collection<FunctionImportReturnTypeStructuralTypeColumn>();
			this._columnListForIsTypeOfType = new Collection<FunctionImportReturnTypeStructuralTypeColumn>();
			this._renameCache = new Memoizer<StructuralType, FunctionImportReturnTypeStructuralTypeColumn>(new Func<StructuralType, FunctionImportReturnTypeStructuralTypeColumn>(this.GetRename), EqualityComparer<StructuralType>.Default);
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x000A0F80 File Offset: 0x0009F180
		internal string GetRename(EdmType type)
		{
			IXmlLineInfo xmlLineInfo;
			return this.GetRename(type, out xmlLineInfo);
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x000A0F98 File Offset: 0x0009F198
		internal string GetRename(EdmType type, out IXmlLineInfo lineInfo)
		{
			FunctionImportReturnTypeStructuralTypeColumn functionImportReturnTypeStructuralTypeColumn = this._renameCache.Evaluate(type as StructuralType);
			lineInfo = functionImportReturnTypeStructuralTypeColumn.LineInfo;
			return functionImportReturnTypeStructuralTypeColumn.ColumnName;
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x000A1000 File Offset: 0x0009F200
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
			return FunctionImportReturnTypeStructuralTypeColumnRenameMapping.GetLowestParentInHierarchy(enumerable);
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000A10A0 File Offset: 0x0009F2A0
		private static FunctionImportReturnTypeStructuralTypeColumn GetLowestParentInHierarchy(IEnumerable<FunctionImportReturnTypeStructuralTypeColumn> nodesInHierarchy)
		{
			FunctionImportReturnTypeStructuralTypeColumn functionImportReturnTypeStructuralTypeColumn = null;
			foreach (FunctionImportReturnTypeStructuralTypeColumn functionImportReturnTypeStructuralTypeColumn2 in nodesInHierarchy)
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

		// Token: 0x06002282 RID: 8834 RVA: 0x000A1100 File Offset: 0x0009F300
		internal void AddRename(FunctionImportReturnTypeStructuralTypeColumn renamedColumn)
		{
			if (!renamedColumn.IsTypeOf)
			{
				this._columnListForType.Add(renamedColumn);
				return;
			}
			this._columnListForIsTypeOfType.Add(renamedColumn);
		}

		// Token: 0x04000C2B RID: 3115
		private readonly Collection<FunctionImportReturnTypeStructuralTypeColumn> _columnListForType;

		// Token: 0x04000C2C RID: 3116
		private readonly Collection<FunctionImportReturnTypeStructuralTypeColumn> _columnListForIsTypeOfType;

		// Token: 0x04000C2D RID: 3117
		private readonly string _defaultMemberName;

		// Token: 0x04000C2E RID: 3118
		private readonly Memoizer<StructuralType, FunctionImportReturnTypeStructuralTypeColumn> _renameCache;
	}
}
