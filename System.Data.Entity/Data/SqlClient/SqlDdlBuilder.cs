using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x02000024 RID: 36
	internal sealed class SqlDdlBuilder
	{
		// Token: 0x06000235 RID: 565 RVA: 0x00007770 File Offset: 0x00005970
		internal static string CreateObjectsScript(StoreItemCollection itemCollection, bool createSchemas)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			foreach (EntityContainer entityContainer in itemCollection.GetItems<EntityContainer>())
			{
				IOrderedEnumerable<EntitySet> source = from s in entityContainer.BaseEntitySets.OfType<EntitySet>()
				orderby s.Name
				select s;
				if (createSchemas)
				{
					HashSet<string> source2 = new HashSet<string>(from s in source
					select SqlDdlBuilder.GetSchemaName(s));
					foreach (string text in from s in source2
					orderby s
					select s)
					{
						if (text != "dbo")
						{
							sqlDdlBuilder.AppendCreateSchema(text);
						}
					}
				}
				foreach (EntitySet entitySet in from s in entityContainer.BaseEntitySets.OfType<EntitySet>()
				orderby s.Name
				select s)
				{
					sqlDdlBuilder.AppendCreateTable(entitySet);
				}
				foreach (AssociationSet associationSet in from s in entityContainer.BaseEntitySets.OfType<AssociationSet>()
				orderby s.Name
				select s)
				{
					sqlDdlBuilder.AppendCreateForeignKeys(associationSet);
				}
			}
			return sqlDdlBuilder.GetCommandText();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000079A4 File Offset: 0x00005BA4
		internal static string CreateDatabaseScript(string databaseName, string dataFileName, string logFileName)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("create database ");
			sqlDdlBuilder.AppendIdentifier(databaseName);
			if (dataFileName != null)
			{
				sqlDdlBuilder.AppendSql(" on primary ");
				sqlDdlBuilder.AppendFileName(dataFileName);
				sqlDdlBuilder.AppendSql(" log on ");
				sqlDdlBuilder.AppendFileName(logFileName);
			}
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000079FC File Offset: 0x00005BFC
		internal static string CreateDatabaseExistsScript(string databaseName, bool useDeprecatedSystemTable)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("SELECT Count(*) FROM ");
			SqlDdlBuilder.AppendSysDatabases(sqlDdlBuilder, useDeprecatedSystemTable);
			sqlDdlBuilder.AppendSql(" WHERE [name]=");
			sqlDdlBuilder.AppendStringLiteral(databaseName);
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007A3E File Offset: 0x00005C3E
		private static void AppendSysDatabases(SqlDdlBuilder builder, bool useDeprecatedSystemTable)
		{
			if (useDeprecatedSystemTable)
			{
				builder.AppendSql("sysdatabases");
				return;
			}
			builder.AppendSql("sys.databases");
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007A5C File Offset: 0x00005C5C
		internal static string CreateGetDatabaseNamesBasedOnFileNameScript(string databaseFileName, bool useDeprecatedSystemTable)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("SELECT [d].[name] FROM ");
			SqlDdlBuilder.AppendSysDatabases(sqlDdlBuilder, useDeprecatedSystemTable);
			sqlDdlBuilder.AppendSql(" AS [d] ");
			if (!useDeprecatedSystemTable)
			{
				sqlDdlBuilder.AppendSql("INNER JOIN sys.master_files AS [f] ON [f].[database_id] = [d].[database_id]");
			}
			sqlDdlBuilder.AppendSql(" WHERE [");
			if (useDeprecatedSystemTable)
			{
				sqlDdlBuilder.AppendSql("filename");
			}
			else
			{
				sqlDdlBuilder.AppendSql("f].[physical_name");
			}
			sqlDdlBuilder.AppendSql("]=");
			sqlDdlBuilder.AppendStringLiteral(databaseFileName);
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007AE0 File Offset: 0x00005CE0
		internal static string CreateCountDatabasesBasedOnFileNameScript(string databaseFileName, bool useDeprecatedSystemTable)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("SELECT Count(*) FROM ");
			if (useDeprecatedSystemTable)
			{
				sqlDdlBuilder.AppendSql("sysdatabases");
			}
			if (!useDeprecatedSystemTable)
			{
				sqlDdlBuilder.AppendSql("sys.master_files");
			}
			sqlDdlBuilder.AppendSql(" WHERE [");
			if (useDeprecatedSystemTable)
			{
				sqlDdlBuilder.AppendSql("filename");
			}
			else
			{
				sqlDdlBuilder.AppendSql("physical_name");
			}
			sqlDdlBuilder.AppendSql("]=");
			sqlDdlBuilder.AppendStringLiteral(databaseFileName);
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00007B60 File Offset: 0x00005D60
		internal static string DropDatabaseScript(string databaseName)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("drop database ");
			sqlDdlBuilder.AppendIdentifier(databaseName);
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00007B90 File Offset: 0x00005D90
		internal string GetCommandText()
		{
			return this.unencodedStringBuilder.ToString();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00007B9D File Offset: 0x00005D9D
		private static string GetSchemaName(EntitySet entitySet)
		{
			return entitySet.Schema ?? entitySet.EntityContainer.Name;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00007BB4 File Offset: 0x00005DB4
		private static string GetTableName(EntitySet entitySet)
		{
			return entitySet.Table ?? entitySet.Name;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00007BC8 File Offset: 0x00005DC8
		private void AppendCreateForeignKeys(AssociationSet associationSet)
		{
			ReferentialConstraint referentialConstraint = associationSet.ElementType.ReferentialConstraints.Single<ReferentialConstraint>();
			AssociationSetEnd associationSetEnd = associationSet.AssociationSetEnds[referentialConstraint.FromRole.Name];
			AssociationSetEnd associationSetEnd2 = associationSet.AssociationSetEnds[referentialConstraint.ToRole.Name];
			if (this.ignoredEntitySets.Contains(associationSetEnd.EntitySet) || this.ignoredEntitySets.Contains(associationSetEnd2.EntitySet))
			{
				this.AppendSql("-- Ignoring association set with participating entity set with defining query: ");
				this.AppendIdentifierEscapeNewLine(associationSet.Name);
			}
			else
			{
				this.AppendSql("alter table ");
				this.AppendIdentifier(associationSetEnd2.EntitySet);
				this.AppendSql(" add constraint ");
				this.AppendIdentifier(associationSet.Name);
				this.AppendSql(" foreign key (");
				this.AppendIdentifiers(referentialConstraint.ToProperties);
				this.AppendSql(") references ");
				this.AppendIdentifier(associationSetEnd.EntitySet);
				this.AppendSql("(");
				this.AppendIdentifiers(referentialConstraint.FromProperties);
				this.AppendSql(")");
				if (associationSetEnd.CorrespondingAssociationEndMember.DeleteBehavior == OperationAction.Cascade)
				{
					this.AppendSql(" on delete cascade");
				}
				this.AppendSql(";");
			}
			this.AppendNewLine();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00007D00 File Offset: 0x00005F00
		private void AppendCreateTable(EntitySet entitySet)
		{
			if (entitySet.DefiningQuery != null)
			{
				this.AppendSql("-- Ignoring entity set with defining query: ");
				this.AppendIdentifier(entitySet, new Action<string>(this.AppendIdentifierEscapeNewLine));
				this.ignoredEntitySets.Add(entitySet);
			}
			else
			{
				this.AppendSql("create table ");
				this.AppendIdentifier(entitySet);
				this.AppendSql(" (");
				this.AppendNewLine();
				foreach (EdmProperty edmProperty in entitySet.ElementType.Properties)
				{
					this.AppendSql("    ");
					this.AppendIdentifier(edmProperty.Name);
					this.AppendSql(" ");
					this.AppendType(edmProperty);
					this.AppendSql(",");
					this.AppendNewLine();
				}
				this.AppendSql("    primary key (");
				this.AppendJoin<EdmMember>(entitySet.ElementType.KeyMembers, delegate(EdmMember k)
				{
					this.AppendIdentifier(k.Name);
				}, ", ");
				this.AppendSql(")");
				this.AppendNewLine();
				this.AppendSql(");");
			}
			this.AppendNewLine();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00007E38 File Offset: 0x00006038
		private void AppendCreateSchema(string schema)
		{
			this.AppendSql("if (schema_id(");
			this.AppendStringLiteral(schema);
			this.AppendSql(") is null) exec(");
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("create schema ");
			sqlDdlBuilder.AppendIdentifier(schema);
			this.AppendStringLiteral(sqlDdlBuilder.unencodedStringBuilder.ToString());
			this.AppendSql(");");
			this.AppendNewLine();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00007E9C File Offset: 0x0000609C
		private void AppendIdentifier(EntitySet table)
		{
			this.AppendIdentifier(table, new Action<string>(this.AppendIdentifier));
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00007EB4 File Offset: 0x000060B4
		private void AppendIdentifier(EntitySet table, Action<string> AppendIdentifierEscape)
		{
			string schemaName = SqlDdlBuilder.GetSchemaName(table);
			string tableName = SqlDdlBuilder.GetTableName(table);
			if (schemaName != null)
			{
				AppendIdentifierEscape(schemaName);
				this.AppendSql(".");
			}
			AppendIdentifierEscape(tableName);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00007EEB File Offset: 0x000060EB
		private void AppendStringLiteral(string literalValue)
		{
			this.AppendSql("N'" + literalValue.Replace("'", "''") + "'");
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00007F12 File Offset: 0x00006112
		private void AppendIdentifiers(IEnumerable<EdmProperty> properties)
		{
			this.AppendJoin<EdmProperty>(properties, delegate(EdmProperty p)
			{
				this.AppendIdentifier(p.Name);
			}, ", ");
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007F2C File Offset: 0x0000612C
		private void AppendIdentifier(string identifier)
		{
			this.AppendSql("[" + identifier.Replace("]", "]]") + "]");
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007F53 File Offset: 0x00006153
		private void AppendIdentifierEscapeNewLine(string identifier)
		{
			this.AppendIdentifier(identifier.Replace("\r", "\r--").Replace("\n", "\n--"));
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007F7A File Offset: 0x0000617A
		private void AppendFileName(string path)
		{
			this.AppendSql("(name=");
			this.AppendStringLiteral(Path.GetFileName(path));
			this.AppendSql(", filename=");
			this.AppendStringLiteral(path);
			this.AppendSql(")");
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007FB0 File Offset: 0x000061B0
		private void AppendJoin<T>(IEnumerable<T> elements, Action<T> appendElement, string unencodedSeparator)
		{
			bool flag = true;
			foreach (T obj in elements)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.AppendSql(unencodedSeparator);
				}
				appendElement(obj);
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00008008 File Offset: 0x00006208
		private void AppendType(EdmProperty column)
		{
			TypeUsage typeUsage = column.TypeUsage;
			bool flag = false;
			Facet facet;
			if (typeUsage.EdmType.Name == "binary" && 8 == typeUsage.GetMaxLength() && column.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out facet) && facet.Value != null && StoreGeneratedPattern.Computed == (StoreGeneratedPattern)facet.Value)
			{
				flag = true;
				this.AppendIdentifier("rowversion");
			}
			else
			{
				string name = typeUsage.EdmType.Name;
				if (typeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType && name.EndsWith("(max)", StringComparison.Ordinal))
				{
					this.AppendIdentifier(name.Substring(0, name.Length - "(max)".Length));
					this.AppendSql("(max)");
				}
				else
				{
					this.AppendIdentifier(name);
				}
				string name2 = typeUsage.EdmType.Name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name2);
				if (num <= 1761125480U)
				{
					if (num <= 923440646U)
					{
						if (num != 520654156U)
						{
							if (num != 923440646U)
							{
								goto IL_2DE;
							}
							if (!(name2 == "datetime2"))
							{
								goto IL_2DE;
							}
							goto IL_29E;
						}
						else if (!(name2 == "decimal"))
						{
							goto IL_2DE;
						}
					}
					else if (num != 1539863742U)
					{
						if (num != 1564253156U)
						{
							if (num != 1761125480U)
							{
								goto IL_2DE;
							}
							if (!(name2 == "numeric"))
							{
								goto IL_2DE;
							}
						}
						else
						{
							if (!(name2 == "time"))
							{
								goto IL_2DE;
							}
							goto IL_29E;
						}
					}
					else
					{
						if (!(name2 == "nvarchar"))
						{
							goto IL_2DE;
						}
						goto IL_2BF;
					}
					this.AppendSqlInvariantFormat("({0}, {1})", new object[]
					{
						typeUsage.GetPrecision(),
						typeUsage.GetScale()
					});
					goto IL_2DE;
				}
				if (num <= 3347933383U)
				{
					if (num != 2336348659U)
					{
						if (num != 2823553821U)
						{
							if (num != 3347933383U)
							{
								goto IL_2DE;
							}
							if (!(name2 == "varbinary"))
							{
								goto IL_2DE;
							}
							goto IL_2BF;
						}
						else
						{
							if (!(name2 == "char"))
							{
								goto IL_2DE;
							}
							goto IL_2BF;
						}
					}
					else if (!(name2 == "datetimeoffset"))
					{
						goto IL_2DE;
					}
				}
				else if (num != 3716508924U)
				{
					if (num != 3761451113U)
					{
						if (num != 4163743794U)
						{
							goto IL_2DE;
						}
						if (!(name2 == "varchar"))
						{
							goto IL_2DE;
						}
						goto IL_2BF;
					}
					else
					{
						if (!(name2 == "nchar"))
						{
							goto IL_2DE;
						}
						goto IL_2BF;
					}
				}
				else
				{
					if (!(name2 == "binary"))
					{
						goto IL_2DE;
					}
					goto IL_2BF;
				}
				IL_29E:
				this.AppendSqlInvariantFormat("({0})", new object[]
				{
					typeUsage.GetPrecision()
				});
				goto IL_2DE;
				IL_2BF:
				this.AppendSqlInvariantFormat("({0})", new object[]
				{
					typeUsage.GetMaxLength()
				});
			}
			IL_2DE:
			this.AppendSql(column.Nullable ? " null" : " not null");
			if (!flag && column.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out facet) && facet.Value != null)
			{
				StoreGeneratedPattern storeGeneratedPattern = (StoreGeneratedPattern)facet.Value;
				if (storeGeneratedPattern == StoreGeneratedPattern.Identity)
				{
					if (typeUsage.EdmType.Name == "uniqueidentifier")
					{
						this.AppendSql(" default newid()");
						return;
					}
					this.AppendSql(" identity");
				}
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008372 File Offset: 0x00006572
		private void AppendSql(string text)
		{
			this.unencodedStringBuilder.Append(text);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00008381 File Offset: 0x00006581
		private void AppendNewLine()
		{
			this.unencodedStringBuilder.Append("\r\n");
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00008394 File Offset: 0x00006594
		private void AppendSqlInvariantFormat(string format, params object[] args)
		{
			this.unencodedStringBuilder.AppendFormat(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x04000652 RID: 1618
		private readonly StringBuilder unencodedStringBuilder = new StringBuilder();

		// Token: 0x04000653 RID: 1619
		private readonly HashSet<EntitySet> ignoredEntitySets = new HashSet<EntitySet>();
	}
}
