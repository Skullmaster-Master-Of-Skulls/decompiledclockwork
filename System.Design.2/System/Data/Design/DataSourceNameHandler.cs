using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Design;

namespace System.Data.Design
{
	// Token: 0x02000225 RID: 549
	internal sealed class DataSourceNameHandler
	{
		// Token: 0x06001474 RID: 5236 RVA: 0x00075795 File Offset: 0x00073995
		internal void GenerateMemberNames(DesignDataSource dataSource, CodeDomProvider codeProvider, bool languageCaseInsensitive, ArrayList problemList)
		{
			this.languageCaseInsensitive = languageCaseInsensitive;
			this.validator = new MemberNameValidator(new string[]
			{
				DataSourceNameHandler.tablesPropertyName,
				DataSourceNameHandler.relationsPropertyName
			}, codeProvider, this.languageCaseInsensitive);
			this.ProcessMemberNames(dataSource);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x000757D0 File Offset: 0x000739D0
		internal void ProcessMemberNames(DesignDataSource dataSource)
		{
			this.ProcessDataSourceName(dataSource);
			if (dataSource.DesignTables != null)
			{
				foreach (object obj in dataSource.DesignTables)
				{
					DesignTable table = (DesignTable)obj;
					this.ProcessTableRelatedNames(table);
				}
			}
			if (dataSource.DesignRelations != null)
			{
				foreach (object obj2 in dataSource.DesignRelations)
				{
					DesignRelation relation = (DesignRelation)obj2;
					this.ProcessRelationRelatedNames(relation);
				}
			}
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0007588C File Offset: 0x00073A8C
		internal void ProcessDataSourceName(DesignDataSource dataSource)
		{
			if (StringUtil.Empty(dataSource.Name))
			{
				throw new DataSourceGeneratorException(SR.GetString("CG_EmptyDSName"));
			}
			if (!StringUtil.EqualValue(dataSource.Name, dataSource.UserDataSetName, this.languageCaseInsensitive) || StringUtil.Empty(dataSource.GeneratorDataSetName))
			{
				dataSource.GeneratorDataSetName = NameHandler.FixIdName(dataSource.Name);
			}
			else
			{
				dataSource.GeneratorDataSetName = this.validator.GenerateIdName(dataSource.GeneratorDataSetName);
			}
			dataSource.UserDataSetName = dataSource.Name;
			if (!StringUtil.EqualValue(NameHandler.FixIdName(dataSource.Name), dataSource.GeneratorDataSetName))
			{
				dataSource.NamingPropertyNames.Add(DesignDataSource.EXTPROPNAME_USER_DATASETNAME);
				dataSource.NamingPropertyNames.Add(DesignDataSource.EXTPROPNAME_GENERATOR_DATASETNAME);
			}
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00075954 File Offset: 0x00073B54
		internal void ProcessTableRelatedNames(DesignTable table)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = !StringUtil.EqualValue(table.Name, table.UserTableName, this.languageCaseInsensitive);
			string name = this.TablePropertyName(table.DataTable, out flag);
			string generatorTablePropName = this.PlainTablePropertyName(table.DataTable, out flag);
			if (flag)
			{
				table.GeneratorTablePropName = generatorTablePropName;
			}
			else
			{
				if (flag3 || StringUtil.Empty(table.GeneratorTablePropName))
				{
					table.GeneratorTablePropName = this.validator.GenerateIdName(name);
				}
				else
				{
					table.GeneratorTablePropName = this.validator.GenerateIdName(table.GeneratorTablePropName);
				}
				if (!StringUtil.EqualValue(this.validator.GenerateIdName(name), table.GeneratorTablePropName))
				{
					table.NamingPropertyNames.Add(DesignTable.EXTPROPNAME_GENERATOR_TABLEPROPNAME);
					flag2 = true;
				}
			}
			string name2 = this.TableVariableName(table.DataTable, out flag);
			string generatorTableVarName = this.PlainTableVariableName(table.DataTable, out flag);
			if (flag)
			{
				table.GeneratorTableVarName = generatorTableVarName;
			}
			else
			{
				if (flag3 || StringUtil.Empty(table.GeneratorTableVarName))
				{
					table.GeneratorTableVarName = this.validator.GenerateIdName(name2);
				}
				else
				{
					table.GeneratorTableVarName = this.validator.GenerateIdName(table.GeneratorTableVarName);
				}
				if (!StringUtil.EqualValue(this.validator.GenerateIdName(name2), table.GeneratorTableVarName))
				{
					table.NamingPropertyNames.Add(DesignTable.EXTPROPNAME_GENERATOR_TABLEVARNAME);
					flag2 = true;
				}
			}
			string name3 = this.TableClassName(table.DataTable, out flag);
			string generatorTableClassName = this.PlainTableClassName(table.DataTable, out flag);
			if (flag)
			{
				table.GeneratorTableClassName = generatorTableClassName;
			}
			else
			{
				if (flag3 || StringUtil.Empty(table.GeneratorTableClassName))
				{
					table.GeneratorTableClassName = this.validator.GenerateIdName(name3);
				}
				else
				{
					table.GeneratorTableClassName = this.validator.GenerateIdName(table.GeneratorTableClassName);
				}
				if (!StringUtil.EqualValue(this.validator.GenerateIdName(name3), table.GeneratorTableClassName))
				{
					table.NamingPropertyNames.Add(DesignTable.EXTPROPNAME_GENERATOR_TABLECLASSNAME);
					flag2 = true;
				}
			}
			string name4 = this.RowClassName(table.DataTable, out flag);
			string generatorRowClassName = this.PlainRowClassName(table.DataTable, out flag);
			if (flag)
			{
				table.GeneratorRowClassName = generatorRowClassName;
			}
			else
			{
				if (flag3 || StringUtil.Empty(table.GeneratorRowClassName))
				{
					table.GeneratorRowClassName = this.validator.GenerateIdName(name4);
				}
				else
				{
					table.GeneratorRowClassName = this.validator.GenerateIdName(table.GeneratorRowClassName);
				}
				if (!StringUtil.EqualValue(this.validator.GenerateIdName(name4), table.GeneratorRowClassName))
				{
					table.NamingPropertyNames.Add(DesignTable.EXTPROPNAME_GENERATOR_ROWCLASSNAME);
					flag2 = true;
				}
			}
			string name5 = this.RowEventHandlerName(table.DataTable, out flag);
			string generatorRowEvHandlerName = this.PlainRowEventHandlerName(table.DataTable, out flag);
			if (flag)
			{
				table.GeneratorRowEvHandlerName = generatorRowEvHandlerName;
			}
			else
			{
				if (flag3 || StringUtil.Empty(table.GeneratorRowEvHandlerName))
				{
					table.GeneratorRowEvHandlerName = this.validator.GenerateIdName(name5);
				}
				else
				{
					table.GeneratorRowEvHandlerName = this.validator.GenerateIdName(table.GeneratorRowEvHandlerName);
				}
				if (!StringUtil.EqualValue(this.validator.GenerateIdName(name5), table.GeneratorRowEvHandlerName))
				{
					table.NamingPropertyNames.Add(DesignTable.EXTPROPNAME_GENERATOR_ROWEVHANDLERNAME);
					flag2 = true;
				}
			}
			string name6 = this.RowEventArgClassName(table.DataTable, out flag);
			string generatorRowEvArgName = this.PlainRowEventArgClassName(table.DataTable, out flag);
			if (flag)
			{
				table.GeneratorRowEvArgName = generatorRowEvArgName;
			}
			else
			{
				if (flag3 || StringUtil.Empty(table.GeneratorRowEvArgName))
				{
					table.GeneratorRowEvArgName = this.validator.GenerateIdName(name6);
				}
				else
				{
					table.GeneratorRowEvArgName = this.validator.GenerateIdName(table.GeneratorRowEvArgName);
				}
				if (!StringUtil.EqualValue(this.validator.GenerateIdName(name6), table.GeneratorRowEvArgName))
				{
					table.NamingPropertyNames.Add(DesignTable.EXTPROPNAME_GENERATOR_ROWEVARGNAME);
					flag2 = true;
				}
			}
			if (flag2)
			{
				table.NamingPropertyNames.Add(DesignTable.EXTPROPNAME_USER_TABLENAME);
			}
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00075D18 File Offset: 0x00073F18
		internal void ProcessRelationRelatedNames(DesignRelation relation)
		{
			if (relation.DataRelation == null)
			{
				return;
			}
			if (!StringUtil.EqualValue(relation.Name, relation.UserRelationName, this.languageCaseInsensitive) || StringUtil.Empty(relation.GeneratorRelationVarName))
			{
				relation.GeneratorRelationVarName = this.validator.GenerateIdName(this.RelationVariableName(relation.DataRelation));
				return;
			}
			relation.GeneratorRelationVarName = this.validator.GenerateIdName(relation.GeneratorRelationVarName);
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x00075D8E File Offset: 0x00073F8E
		internal static string TablesPropertyName
		{
			get
			{
				return DataSourceNameHandler.tablesPropertyName;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00075D95 File Offset: 0x00073F95
		internal static string RelationsPropertyName
		{
			get
			{
				return DataSourceNameHandler.relationsPropertyName;
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00075D9C File Offset: 0x00073F9C
		private string TableClassName(DataTable table, out bool usesAnnotations)
		{
			usesAnnotations = true;
			string text = (string)table.ExtendedProperties["typedPlural"];
			if (StringUtil.Empty(text))
			{
				text = (string)table.ExtendedProperties["typedName"];
				if (StringUtil.Empty(text))
				{
					usesAnnotations = false;
					text = NameHandler.FixIdName(table.TableName);
				}
			}
			return text + "DataTable";
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00075E04 File Offset: 0x00074004
		private string PlainTableClassName(DataTable table, out bool usesAnnotations)
		{
			usesAnnotations = true;
			string text = (string)table.ExtendedProperties["typedPlural"];
			if (StringUtil.Empty(text))
			{
				text = (string)table.ExtendedProperties["typedName"];
				if (StringUtil.Empty(text))
				{
					usesAnnotations = false;
					text = table.TableName;
				}
			}
			return text + "DataTable";
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00075E68 File Offset: 0x00074068
		private string TablePropertyName(DataTable table, out bool usesAnnotations)
		{
			usesAnnotations = true;
			string text = (string)table.ExtendedProperties["typedPlural"];
			if (StringUtil.Empty(text))
			{
				text = (string)table.ExtendedProperties["typedName"];
				if (StringUtil.Empty(text))
				{
					usesAnnotations = false;
					text = NameHandler.FixIdName(table.TableName);
				}
				else
				{
					text += "Table";
				}
			}
			return text;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00075ED4 File Offset: 0x000740D4
		private string PlainTablePropertyName(DataTable table, out bool usesAnnotations)
		{
			usesAnnotations = true;
			string text = (string)table.ExtendedProperties["typedPlural"];
			if (StringUtil.Empty(text))
			{
				text = (string)table.ExtendedProperties["typedName"];
				if (StringUtil.Empty(text))
				{
					usesAnnotations = false;
					text = table.TableName;
				}
				else
				{
					text += "Table";
				}
			}
			return text;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00075F39 File Offset: 0x00074139
		private string TableVariableName(DataTable table, out bool usesAnnotations)
		{
			return "table" + this.TablePropertyName(table, out usesAnnotations);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00075F4D File Offset: 0x0007414D
		private string PlainTableVariableName(DataTable table, out bool usesAnnotations)
		{
			return "table" + this.PlainTablePropertyName(table, out usesAnnotations);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00075F64 File Offset: 0x00074164
		private string RowClassName(DataTable table, out bool usesAnnotations)
		{
			usesAnnotations = true;
			string text = (string)table.ExtendedProperties["typedName"];
			if (StringUtil.Empty(text))
			{
				usesAnnotations = false;
				text = NameHandler.FixIdName(table.TableName) + "Row";
			}
			return text;
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00075FAC File Offset: 0x000741AC
		private string PlainRowClassName(DataTable table, out bool usesAnnotations)
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

		// Token: 0x06001483 RID: 5251 RVA: 0x00075FEF File Offset: 0x000741EF
		private string RowEventArgClassName(DataTable table, out bool usesAnnotations)
		{
			return this.RowClassName(table, out usesAnnotations) + "ChangeEvent";
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00076003 File Offset: 0x00074203
		private string PlainRowEventArgClassName(DataTable table, out bool usesAnnotations)
		{
			return this.PlainRowClassName(table, out usesAnnotations) + "ChangeEvent";
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00076017 File Offset: 0x00074217
		private string RowEventHandlerName(DataTable table, out bool usesAnnotations)
		{
			return this.RowClassName(table, out usesAnnotations) + "ChangeEventHandler";
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0007602B File Offset: 0x0007422B
		private string PlainRowEventHandlerName(DataTable table, out bool usesAnnotations)
		{
			return this.PlainRowClassName(table, out usesAnnotations) + "ChangeEventHandler";
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0007603F File Offset: 0x0007423F
		private string RelationVariableName(DataRelation relation)
		{
			return NameHandler.FixIdName("relation" + relation.RelationName);
		}

		// Token: 0x04000ADA RID: 2778
		private MemberNameValidator validator;

		// Token: 0x04000ADB RID: 2779
		private bool languageCaseInsensitive;

		// Token: 0x04000ADC RID: 2780
		private static string tablesPropertyName = "Tables";

		// Token: 0x04000ADD RID: 2781
		private static string relationsPropertyName = "Relations";
	}
}
