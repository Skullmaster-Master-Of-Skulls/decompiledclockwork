using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.SqlServer.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x0200001A RID: 26
	internal sealed class SqlDdlBuilder
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00004DDC File Offset: 0x00002FDC
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

		// Token: 0x060000FE RID: 254 RVA: 0x00005008 File Offset: 0x00003208
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
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

		// Token: 0x060000FF RID: 255 RVA: 0x00005060 File Offset: 0x00003260
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EngineEdition")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "serverproperty")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "spexecutesql")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
		internal static string SetDatabaseOptionsScript(SqlVersion sqlVersion, string databaseName)
		{
			if (sqlVersion < SqlVersion.Sql9)
			{
				return string.Empty;
			}
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("if serverproperty('EngineEdition') <> 5 execute sp_executesql ");
			sqlDdlBuilder.AppendStringLiteral(SqlDdlBuilder.SetReadCommittedSnapshotScript(databaseName));
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000050A0 File Offset: 0x000032A0
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "readcommittedsnapshot")]
		private static string SetReadCommittedSnapshotScript(string databaseName)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("alter database ");
			sqlDdlBuilder.AppendIdentifier(databaseName);
			sqlDdlBuilder.AppendSql(" set read_committed_snapshot on");
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000050DC File Offset: 0x000032DC
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "dbid")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
		internal static string CreateDatabaseExistsScript(string databaseName)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("IF db_id(");
			sqlDdlBuilder.AppendStringLiteral(databaseName);
			sqlDdlBuilder.AppendSql(") IS NOT NULL SELECT 1 ELSE SELECT Count(*) FROM sys.databases WHERE [name]=");
			sqlDdlBuilder.AppendStringLiteral(databaseName);
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000511E File Offset: 0x0000331E
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "sysdatabases")]
		private static void AppendSysDatabases(SqlDdlBuilder builder, bool useDeprecatedSystemTable)
		{
			if (useDeprecatedSystemTable)
			{
				builder.AppendSql("sysdatabases");
				return;
			}
			builder.AppendSql("sys.databases");
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000513C File Offset: 0x0000333C
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "databaseid")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "physicalname")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "masterfiles")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
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

		// Token: 0x06000104 RID: 260 RVA: 0x000051C0 File Offset: 0x000033C0
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "masterfiles")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "physicalname")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "sysdatabases")]
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

		// Token: 0x06000105 RID: 261 RVA: 0x00005240 File Offset: 0x00003440
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
		internal static string DropDatabaseScript(string databaseName)
		{
			SqlDdlBuilder sqlDdlBuilder = new SqlDdlBuilder();
			sqlDdlBuilder.AppendSql("drop database ");
			sqlDdlBuilder.AppendIdentifier(databaseName);
			return sqlDdlBuilder.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005270 File Offset: 0x00003470
		internal string GetCommandText()
		{
			return this.unencodedStringBuilder.ToString();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000527D File Offset: 0x0000347D
		internal static string GetSchemaName(EntitySet entitySet)
		{
			return entitySet.GetMetadataPropertyValue("Schema") ?? entitySet.EntityContainer.Name;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005299 File Offset: 0x00003499
		internal static string GetTableName(EntitySet entitySet)
		{
			return entitySet.GetMetadataPropertyValue("Table") ?? entitySet.Name;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000052B0 File Offset: 0x000034B0
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
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

		// Token: 0x0600010A RID: 266 RVA: 0x000053F4 File Offset: 0x000035F4
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
		private void AppendCreateTable(EntitySet entitySet)
		{
			if (entitySet.GetMetadataPropertyValue("DefiningQuery") != null)
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

		// Token: 0x0600010B RID: 267 RVA: 0x00005538 File Offset: 0x00003738
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "schemaid")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
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

		// Token: 0x0600010C RID: 268 RVA: 0x0000559C File Offset: 0x0000379C
		private void AppendIdentifier(EntitySet table)
		{
			this.AppendIdentifier(table, new Action<string>(this.AppendIdentifier));
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000055B4 File Offset: 0x000037B4
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
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

		// Token: 0x0600010E RID: 270 RVA: 0x000055EB File Offset: 0x000037EB
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
		private void AppendStringLiteral(string literalValue)
		{
			this.AppendSql("N'" + literalValue.Replace("'", "''") + "'");
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005620 File Offset: 0x00003820
		private void AppendIdentifiers(IEnumerable<EdmProperty> properties)
		{
			this.AppendJoin<EdmProperty>(properties, delegate(EdmProperty p)
			{
				this.AppendIdentifier(p.Name);
			}, ", ");
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000563A File Offset: 0x0000383A
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
		private void AppendIdentifier(string identifier)
		{
			this.AppendSql("[" + identifier.Replace("]", "]]") + "]");
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005661 File Offset: 0x00003861
		private void AppendIdentifierEscapeNewLine(string identifier)
		{
			this.AppendIdentifier(identifier.Replace("\r", "\r--").Replace("\n", "\n--"));
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005688 File Offset: 0x00003888
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
		private void AppendFileName(string path)
		{
			this.AppendSql("(name=");
			this.AppendStringLiteral(Path.GetFileName(path));
			this.AppendSql(", filename=");
			this.AppendStringLiteral(path);
			this.AppendSql(")");
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000056C0 File Offset: 0x000038C0
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

		// Token: 0x06000114 RID: 276 RVA: 0x00005718 File Offset: 0x00003918
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "newid")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.SqlServer.SqlDdlBuilder.AppendSql(System.String)")]
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
				string name2;
				switch (name2 = typeUsage.EdmType.Name)
				{
				case "decimal":
				case "numeric":
					this.AppendSqlInvariantFormat("({0}, {1})", new object[]
					{
						typeUsage.GetPrecision(),
						typeUsage.GetScale()
					});
					break;
				case "datetime2":
				case "datetimeoffset":
				case "time":
					this.AppendSqlInvariantFormat("({0})", new object[]
					{
						typeUsage.GetPrecision()
					});
					break;
				case "binary":
				case "varbinary":
				case "nvarchar":
				case "varchar":
				case "char":
				case "nchar":
					this.AppendSqlInvariantFormat("({0})", new object[]
					{
						typeUsage.GetMaxLength()
					});
					break;
				}
			}
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

		// Token: 0x06000115 RID: 277 RVA: 0x000059E6 File Offset: 0x00003BE6
		private void AppendSql(string text)
		{
			this.unencodedStringBuilder.Append(text);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000059F5 File Offset: 0x00003BF5
		private void AppendNewLine()
		{
			this.unencodedStringBuilder.Append("\r\n");
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005A08 File Offset: 0x00003C08
		private void AppendSqlInvariantFormat(string format, params object[] args)
		{
			this.unencodedStringBuilder.AppendFormat(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x0400004F RID: 79
		private readonly StringBuilder unencodedStringBuilder = new StringBuilder();

		// Token: 0x04000050 RID: 80
		private readonly HashSet<EntitySet> ignoredEntitySets = new HashSet<EntitySet>();
	}
}
