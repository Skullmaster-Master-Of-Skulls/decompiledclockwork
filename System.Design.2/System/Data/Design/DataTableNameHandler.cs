using System;
using System.CodeDom.Compiler;
using System.Collections;

namespace System.Data.Design
{
	// Token: 0x0200022E RID: 558
	internal sealed class DataTableNameHandler
	{
		// Token: 0x060014A7 RID: 5287 RVA: 0x00076A3E File Offset: 0x00074C3E
		internal void GenerateMemberNames(DesignTable designTable, CodeDomProvider codeProvider, bool languageCaseInsensitive, ArrayList problemList)
		{
			this.languageCaseInsensitive = languageCaseInsensitive;
			this.validator = new MemberNameValidator(null, codeProvider, this.languageCaseInsensitive);
			this.AddReservedNames();
			this.ProcessMemberNames(designTable);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00076A68 File Offset: 0x00074C68
		private void AddReservedNames()
		{
			this.validator.GetNewMemberName("OnRowChanging");
			this.validator.GetNewMemberName("OnRowChanged");
			this.validator.GetNewMemberName("OnRowDeleting");
			this.validator.GetNewMemberName("OnRowDeleted");
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00076ABC File Offset: 0x00074CBC
		private void ProcessMemberNames(DesignTable designTable)
		{
			if (designTable.DesignColumns != null)
			{
				foreach (object obj in designTable.DesignColumns)
				{
					DesignColumn column = (DesignColumn)obj;
					this.ProcessColumnRelatedNames(column);
				}
			}
			DataRelationCollection childRelations = designTable.DataTable.ChildRelations;
			if (childRelations != null)
			{
				foreach (object obj2 in childRelations)
				{
					DataRelation relation = (DataRelation)obj2;
					DesignRelation relation2 = this.FindCorrespondingDesignRelation(designTable, relation);
					this.ProcessChildRelationName(relation2);
				}
			}
			DataRelationCollection parentRelations = designTable.DataTable.ParentRelations;
			if (parentRelations != null)
			{
				foreach (object obj3 in parentRelations)
				{
					DataRelation relation3 = (DataRelation)obj3;
					DesignRelation relation4 = this.FindCorrespondingDesignRelation(designTable, relation3);
					this.ProcessParentRelationName(relation4);
				}
			}
			this.ProcessEventNames(designTable);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00076BF4 File Offset: 0x00074DF4
		private DesignRelation FindCorrespondingDesignRelation(DesignTable designTable, DataRelation relation)
		{
			DesignDataSource owner = designTable.Owner;
			if (owner == null)
			{
				throw new InternalException("Unable to find DataSource for table.");
			}
			foreach (object obj in owner.DesignRelations)
			{
				DesignRelation designRelation = (DesignRelation)obj;
				if (designRelation.DataRelation != null && StringUtil.EqualValue(designRelation.DataRelation.ChildTable.TableName, relation.ChildTable.TableName) && StringUtil.EqualValue(designRelation.DataRelation.ParentTable.TableName, relation.ParentTable.TableName) && StringUtil.EqualValue(designRelation.Name, relation.RelationName))
				{
					return designRelation;
				}
			}
			return null;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00076CC4 File Offset: 0x00074EC4
		private void ProcessColumnRelatedNames(DesignColumn column)
		{
			bool flag = !StringUtil.EqualValue(column.Name, column.UserColumnName, this.languageCaseInsensitive);
			bool flag2 = false;
			bool flag3 = false;
			string name = this.TableColumnPropertyName(column.DataColumn, out flag2);
			string generatorColumnPropNameInTable = this.PlainTableColumnPropertyName(column.DataColumn, out flag2);
			if (flag2)
			{
				column.GeneratorColumnPropNameInTable = generatorColumnPropNameInTable;
			}
			else
			{
				if (flag || StringUtil.Empty(column.GeneratorColumnPropNameInTable))
				{
					column.GeneratorColumnPropNameInTable = this.validator.GenerateIdName(name);
				}
				else
				{
					column.GeneratorColumnPropNameInTable = this.validator.GenerateIdName(column.GeneratorColumnPropNameInTable);
				}
				if (!StringUtil.EqualValue(this.validator.GenerateIdName(name), column.GeneratorColumnPropNameInTable))
				{
					column.NamingPropertyNames.Add(DesignColumn.EXTPROPNAME_GENERATOR_COLUMNPROPNAMEINTABLE);
					flag3 = true;
				}
			}
			string name2 = this.TableColumnVariableName(column.DataColumn, out flag2);
			string generatorColumnVarNameInTable = this.PlainTableColumnVariableName(column.DataColumn, out flag2);
			if (flag2)
			{
				column.GeneratorColumnVarNameInTable = generatorColumnVarNameInTable;
			}
			else
			{
				if (flag || StringUtil.Empty(column.GeneratorColumnVarNameInTable))
				{
					column.GeneratorColumnVarNameInTable = this.validator.GenerateIdName(name2);
				}
				else
				{
					column.GeneratorColumnVarNameInTable = this.validator.GenerateIdName(column.GeneratorColumnVarNameInTable);
				}
				if (!StringUtil.EqualValue(this.validator.GenerateIdName(name2), column.GeneratorColumnVarNameInTable))
				{
					column.NamingPropertyNames.Add(DesignColumn.EXTPROPNAME_GENERATOR_COLUMNVARNAMEINTABLE);
					flag3 = true;
				}
			}
			string name3 = this.RowColumnPropertyName(column.DataColumn, out flag2);
			string generatorColumnPropNameInRow = this.PlainRowColumnPropertyName(column.DataColumn, out flag2);
			if (flag2)
			{
				column.GeneratorColumnPropNameInRow = generatorColumnPropNameInRow;
			}
			else
			{
				if (flag || StringUtil.Empty(column.GeneratorColumnPropNameInRow))
				{
					column.GeneratorColumnPropNameInRow = this.validator.GenerateIdName(name3);
				}
				else
				{
					column.GeneratorColumnPropNameInRow = this.validator.GenerateIdName(column.GeneratorColumnPropNameInRow);
				}
				if (!StringUtil.EqualValue(this.validator.GenerateIdName(name3), column.GeneratorColumnPropNameInRow))
				{
					column.NamingPropertyNames.Add(DesignColumn.EXTPROPNAME_GENERATOR_COLUMNPROPNAMEINROW);
					flag3 = true;
				}
			}
			column.UserColumnName = column.Name;
			if (flag3)
			{
				column.NamingPropertyNames.Add(DesignColumn.EXTPROPNAME_USER_COLUMNNAME);
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00076ED0 File Offset: 0x000750D0
		internal void ProcessChildRelationName(DesignRelation relation)
		{
			bool flag = !StringUtil.EqualValue(relation.Name, relation.UserRelationName, this.languageCaseInsensitive) || !StringUtil.EqualValue(relation.ChildDesignTable.Name, relation.UserChildTable, this.languageCaseInsensitive) || !StringUtil.EqualValue(relation.ParentDesignTable.Name, relation.UserParentTable, this.languageCaseInsensitive);
			bool flag2 = false;
			string text = this.ChildPropertyName(relation.DataRelation, out flag2);
			if (flag2)
			{
				relation.GeneratorChildPropName = text;
				return;
			}
			if (flag || StringUtil.Empty(relation.GeneratorChildPropName))
			{
				relation.GeneratorChildPropName = this.validator.GenerateIdName(text);
				return;
			}
			relation.GeneratorChildPropName = this.validator.GenerateIdName(relation.GeneratorChildPropName);
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00076F90 File Offset: 0x00075190
		internal void ProcessParentRelationName(DesignRelation relation)
		{
			bool flag = !StringUtil.EqualValue(relation.Name, relation.UserRelationName, this.languageCaseInsensitive) || !StringUtil.EqualValue(relation.ChildDesignTable.Name, relation.UserChildTable, this.languageCaseInsensitive) || !StringUtil.EqualValue(relation.ParentDesignTable.Name, relation.UserParentTable, this.languageCaseInsensitive);
			bool flag2 = false;
			string text = this.ParentPropertyName(relation.DataRelation, out flag2);
			if (flag2)
			{
				relation.GeneratorParentPropName = text;
				return;
			}
			if (flag || StringUtil.Empty(relation.GeneratorParentPropName))
			{
				relation.GeneratorParentPropName = this.validator.GenerateIdName(text);
				return;
			}
			relation.GeneratorParentPropName = this.validator.GenerateIdName(relation.GeneratorParentPropName);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00077050 File Offset: 0x00075250
		internal void ProcessEventNames(DesignTable designTable)
		{
			bool flag = false;
			bool flag2 = !StringUtil.EqualValue(designTable.Name, designTable.UserTableName, this.languageCaseInsensitive);
			string name = designTable.GeneratorRowClassName + "Changing";
			if (flag2 || StringUtil.Empty(designTable.GeneratorRowChangingName))
			{
				designTable.GeneratorRowChangingName = this.validator.GenerateIdName(name);
			}
			else
			{
				designTable.GeneratorRowChangingName = this.validator.GenerateIdName(designTable.GeneratorRowChangingName);
			}
			if (!StringUtil.EqualValue(this.validator.GenerateIdName(name), designTable.GeneratorRowChangingName))
			{
				designTable.NamingPropertyNames.Add(DesignTable.EXTPROPNAME_GENERATOR_ROWCHANGINGNAME);
				flag = true;
			}
			string name2 = designTable.GeneratorRowClassName + "Changed";
			if (flag2 || StringUtil.Empty(designTable.GeneratorRowChangedName))
			{
				designTable.GeneratorRowChangedName = this.validator.GenerateIdName(name2);
			}
			else
			{
				designTable.GeneratorRowChangedName = this.validator.GenerateIdName(designTable.GeneratorRowChangedName);
			}
			if (!StringUtil.EqualValue(this.validator.GenerateIdName(name2), designTable.GeneratorRowChangedName))
			{
				designTable.NamingPropertyNames.Add(DesignTable.EXTPROPNAME_GENERATOR_ROWCHANGEDNAME);
				flag = true;
			}
			string name3 = designTable.GeneratorRowClassName + "Deleting";
			if (flag2 || StringUtil.Empty(designTable.GeneratorRowDeletingName))
			{
				designTable.GeneratorRowDeletingName = this.validator.GenerateIdName(name3);
			}
			else
			{
				designTable.GeneratorRowDeletingName = this.validator.GenerateIdName(designTable.GeneratorRowDeletingName);
			}
			if (!StringUtil.EqualValue(this.validator.GenerateIdName(name3), designTable.GeneratorRowDeletingName))
			{
				designTable.NamingPropertyNames.Add(DesignTable.EXTPROPNAME_GENERATOR_ROWDELETINGNAME);
				flag = true;
			}
			string name4 = designTable.GeneratorRowClassName + "Deleted";
			if (flag2 || StringUtil.Empty(designTable.GeneratorRowDeletedName))
			{
				designTable.GeneratorRowDeletedName = this.validator.GenerateIdName(name4);
			}
			else
			{
				designTable.GeneratorRowDeletedName = this.validator.GenerateIdName(designTable.GeneratorRowDeletedName);
			}
			if (!StringUtil.EqualValue(this.validator.GenerateIdName(name4), designTable.GeneratorRowDeletedName))
			{
				designTable.NamingPropertyNames.Add(DesignTable.EXTPROPNAME_GENERATOR_ROWDELETEDNAME);
				flag = true;
			}
			if (flag && !designTable.NamingPropertyNames.Contains(DesignTable.EXTPROPNAME_USER_TABLENAME))
			{
				designTable.NamingPropertyNames.Add(DesignTable.EXTPROPNAME_USER_TABLENAME);
			}
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00077288 File Offset: 0x00075488
		private string RowColumnPropertyName(DataColumn column, out bool usesAnnotations)
		{
			usesAnnotations = true;
			string text = (string)column.ExtendedProperties["typedName"];
			if (StringUtil.Empty(text))
			{
				usesAnnotations = false;
				text = NameHandler.FixIdName(column.ColumnName);
			}
			return text;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x000772C8 File Offset: 0x000754C8
		private string PlainRowColumnPropertyName(DataColumn column, out bool usesAnnotations)
		{
			usesAnnotations = true;
			string text = (string)column.ExtendedProperties["typedName"];
			if (StringUtil.Empty(text))
			{
				usesAnnotations = false;
				text = column.ColumnName;
			}
			return text;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00077304 File Offset: 0x00075504
		private string TableColumnVariableName(DataColumn column, out bool usesAnnotations)
		{
			string text = this.RowColumnPropertyName(column, out usesAnnotations);
			string text2;
			if (StringUtil.EqualValue("column", text, true))
			{
				text2 = "columnField" + text;
			}
			else
			{
				text2 = "column" + text;
			}
			if (!StringUtil.EqualValue(text2, "Columns", this.languageCaseInsensitive))
			{
				return text2;
			}
			return "_" + text2;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00077364 File Offset: 0x00075564
		private string PlainTableColumnVariableName(DataColumn column, out bool usesAnnotations)
		{
			return "column" + this.PlainRowColumnPropertyName(column, out usesAnnotations);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00077378 File Offset: 0x00075578
		private string TableColumnPropertyName(DataColumn column, out bool usesAnnotations)
		{
			return this.RowColumnPropertyName(column, out usesAnnotations) + "Column";
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0007738C File Offset: 0x0007558C
		private string PlainTableColumnPropertyName(DataColumn column, out bool usesAnnotations)
		{
			return this.PlainRowColumnPropertyName(column, out usesAnnotations) + "Column";
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x000773A0 File Offset: 0x000755A0
		private string ChildPropertyName(DataRelation relation, out bool usesAnnotations)
		{
			usesAnnotations = true;
			string text = (string)relation.ExtendedProperties["typedChildren"];
			if (StringUtil.Empty(text))
			{
				string text2 = (string)relation.ChildTable.ExtendedProperties["typedPlural"];
				if (StringUtil.Empty(text2))
				{
					text2 = (string)relation.ChildTable.ExtendedProperties["typedName"];
					if (StringUtil.Empty(text2))
					{
						usesAnnotations = false;
						text = "Get" + relation.ChildTable.TableName + "Rows";
						if (1 < DataTableNameHandler.TablesConnectedness(relation.ParentTable, relation.ChildTable))
						{
							text = text + "By" + relation.RelationName;
						}
						return NameHandler.FixIdName(text);
					}
					text2 += "Rows";
				}
				text = "Get" + text2;
			}
			return text;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0007747C File Offset: 0x0007567C
		private string ParentPropertyName(DataRelation relation, out bool usesAnnotations)
		{
			usesAnnotations = true;
			string text = (string)relation.ExtendedProperties["typedParent"];
			if (StringUtil.Empty(text))
			{
				text = this.RowClassName(relation.ParentTable, out usesAnnotations);
				if (relation.ChildTable == relation.ParentTable || relation.ChildColumns.Length != 1)
				{
					text += "Parent";
				}
				if (1 < DataTableNameHandler.TablesConnectedness(relation.ParentTable, relation.ChildTable))
				{
					text = text + "By" + NameHandler.FixIdName(relation.RelationName);
				}
			}
			return text;
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0007750C File Offset: 0x0007570C
		private static int TablesConnectedness(DataTable parentTable, DataTable childTable)
		{
			int num = 0;
			DataRelationCollection parentRelations = childTable.ParentRelations;
			for (int i = 0; i < parentRelations.Count; i++)
			{
				if (parentRelations[i].ParentTable == parentTable)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00077548 File Offset: 0x00075748
		private string RowClassName(DataTable table, out bool usesAnnotations)
		{
			usesAnnotations = true;
			string text = (string)table.ExtendedProperties["typedName"];
			if (StringUtil.Empty(text))
			{
				usesAnnotations = false;
				text = table.TableName + "Row";
			}
			return text;
		}

		// Token: 0x04000AE8 RID: 2792
		private MemberNameValidator validator;

		// Token: 0x04000AE9 RID: 2793
		private bool languageCaseInsensitive;

		// Token: 0x04000AEA RID: 2794
		private const string onRowChangingMethodName = "OnRowChanging";

		// Token: 0x04000AEB RID: 2795
		private const string onRowChangedMethodName = "OnRowChanged";

		// Token: 0x04000AEC RID: 2796
		private const string onRowDeletingMethodName = "OnRowDeleting";

		// Token: 0x04000AED RID: 2797
		private const string onRowDeletedMethodName = "OnRowDeleted";
	}
}
