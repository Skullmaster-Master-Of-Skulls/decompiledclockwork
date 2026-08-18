using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.SqlGen;
using System.Data.Entity.SqlServer.Utilities;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000024 RID: 36
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	public class SqlServerMigrationSqlGenerator : MigrationSqlGenerator
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00007BB0 File Offset: 0x00005DB0
		public override IEnumerable<MigrationStatement> Generate(IEnumerable<MigrationOperation> migrationOperations, string providerManifestToken)
		{
			Check.NotNull<IEnumerable<MigrationOperation>>(migrationOperations, "migrationOperations");
			Check.NotNull<string>(providerManifestToken, "providerManifestToken");
			this._statements = new List<MigrationStatement>();
			this._generatedSchemas = new HashSet<string>();
			this.InitializeProviderServices(providerManifestToken);
			this.GenerateStatements(migrationOperations);
			return this._statements;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007C6A File Offset: 0x00005E6A
		private void GenerateStatements(IEnumerable<MigrationOperation> migrationOperations)
		{
			Check.NotNull<IEnumerable<MigrationOperation>>(migrationOperations, "migrationOperations");
			SqlServerMigrationSqlGenerator.DetectHistoryRebuild(migrationOperations).Each(delegate(dynamic o)
			{
				if (SqlServerMigrationSqlGenerator.<GenerateStatements>o__SiteContainer0.<>p__Site1 == null)
				{
					SqlServerMigrationSqlGenerator.<GenerateStatements>o__SiteContainer0.<>p__Site1 = CallSite<Action<CallSite, SqlServerMigrationSqlGenerator, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "Generate", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				SqlServerMigrationSqlGenerator.<GenerateStatements>o__SiteContainer0.<>p__Site1.Target(SqlServerMigrationSqlGenerator.<GenerateStatements>o__SiteContainer0.<>p__Site1, this, o);
			});
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007C8F File Offset: 0x00005E8F
		public override string GenerateProcedureBody(ICollection<DbModificationCommandTree> commandTrees, string rowsAffectedParameter, string providerManifestToken)
		{
			Check.NotNull<ICollection<DbModificationCommandTree>>(commandTrees, "commandTrees");
			Check.NotEmpty(providerManifestToken, "providerManifestToken");
			if (!commandTrees.Any<DbModificationCommandTree>())
			{
				return "RETURN";
			}
			this.InitializeProviderServices(providerManifestToken);
			return this.GenerateFunctionSql(commandTrees, rowsAffectedParameter);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007CC8 File Offset: 0x00005EC8
		private void InitializeProviderServices(string providerManifestToken)
		{
			Check.NotEmpty(providerManifestToken, "providerManifestToken");
			this._providerManifestToken = providerManifestToken;
			using (DbConnection dbConnection = this.CreateConnection())
			{
				base.ProviderManifest = DbProviderServices.GetProviderServices(dbConnection).GetProviderManifest(providerManifestToken);
				this._sqlGenerator = new SqlGenerator(SqlVersionUtils.GetSqlVersion(providerManifestToken));
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00007D30 File Offset: 0x00005F30
		private string GenerateFunctionSql(ICollection<DbModificationCommandTree> commandTrees, string rowsAffectedParameter)
		{
			DmlFunctionSqlGenerator dmlFunctionSqlGenerator = new DmlFunctionSqlGenerator(this._sqlGenerator);
			switch (commandTrees.First<DbModificationCommandTree>().CommandTreeKind)
			{
			case DbCommandTreeKind.Update:
				return dmlFunctionSqlGenerator.GenerateUpdate(commandTrees.Cast<DbUpdateCommandTree>().ToList<DbUpdateCommandTree>(), rowsAffectedParameter);
			case DbCommandTreeKind.Insert:
				return dmlFunctionSqlGenerator.GenerateInsert(commandTrees.Cast<DbInsertCommandTree>().ToList<DbInsertCommandTree>());
			case DbCommandTreeKind.Delete:
				return dmlFunctionSqlGenerator.GenerateDelete(commandTrees.Cast<DbDeleteCommandTree>().ToList<DbDeleteCommandTree>(), rowsAffectedParameter);
			default:
				return null;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007DC0 File Offset: 0x00005FC0
		protected virtual void Generate(UpdateDatabaseOperation updateDatabaseOperation)
		{
			Check.NotNull<UpdateDatabaseOperation>(updateDatabaseOperation, "updateDatabaseOperation");
			if (!updateDatabaseOperation.Migrations.Any<UpdateDatabaseOperation.Migration>())
			{
				return;
			}
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.WriteLine("DECLARE @CurrentMigration [nvarchar](max)");
				indentedTextWriter.WriteLine();
				foreach (DbQueryCommandTree tree in updateDatabaseOperation.HistoryQueryTrees)
				{
					HashSet<string> hashSet;
					string s = this._sqlGenerator.GenerateSql(tree, out hashSet);
					indentedTextWriter.Write("IF object_id('");
					indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(this._sqlGenerator.Targets.Single<string>()));
					indentedTextWriter.WriteLine("') IS NOT NULL");
					indentedTextWriter.Indent++;
					indentedTextWriter.WriteLine("SELECT @CurrentMigration =");
					indentedTextWriter.Indent++;
					indentedTextWriter.Write("(");
					indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Indent(s, indentedTextWriter.CurrentIndentation()));
					indentedTextWriter.WriteLine(")");
					indentedTextWriter.Indent -= 2;
					indentedTextWriter.WriteLine();
				}
				indentedTextWriter.WriteLine("IF @CurrentMigration IS NULL");
				indentedTextWriter.Indent++;
				indentedTextWriter.WriteLine("SET @CurrentMigration = '0'");
				this.Statement(indentedTextWriter, null);
			}
			List<MigrationStatement> statements = this._statements;
			foreach (UpdateDatabaseOperation.Migration migration in updateDatabaseOperation.Migrations)
			{
				using (IndentedTextWriter indentedTextWriter2 = SqlServerMigrationSqlGenerator.Writer())
				{
					this._statements = new List<MigrationStatement>();
					this.GenerateStatements(migration.Operations);
					if (this._statements.Count > 0)
					{
						indentedTextWriter2.Write("IF @CurrentMigration < '");
						indentedTextWriter2.Write(SqlServerMigrationSqlGenerator.Escape(migration.MigrationId));
						indentedTextWriter2.WriteLine("'");
						indentedTextWriter2.Write("BEGIN");
						using (IndentedTextWriter blockWriter = SqlServerMigrationSqlGenerator.Writer())
						{
							blockWriter.WriteLine();
							blockWriter.Indent++;
							foreach (MigrationStatement migrationStatement in this._statements)
							{
								if (string.IsNullOrWhiteSpace(migrationStatement.BatchTerminator))
								{
									migrationStatement.Sql.EachLine(new Action<string>(blockWriter.WriteLine));
								}
								else
								{
									blockWriter.WriteLine("EXECUTE('");
									blockWriter.Indent++;
									migrationStatement.Sql.EachLine(delegate(string l)
									{
										blockWriter.WriteLine(SqlServerMigrationSqlGenerator.Escape(l));
									});
									blockWriter.Indent--;
									blockWriter.WriteLine("')");
								}
							}
							indentedTextWriter2.WriteLine(blockWriter.InnerWriter.ToString().TrimEnd(new char[0]));
						}
						indentedTextWriter2.WriteLine("END");
						statements.Add(new MigrationStatement
						{
							Sql = indentedTextWriter2.InnerWriter.ToString()
						});
					}
				}
			}
			this._statements = statements;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000081CC File Offset: 0x000063CC
		protected virtual void Generate(MigrationOperation migrationOperation)
		{
			Check.NotNull<MigrationOperation>(migrationOperation, "migrationOperation");
			throw Error.SqlServerMigrationSqlGenerator_UnknownOperation(base.GetType().Name, migrationOperation.GetType().FullName);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000081F5 File Offset: 0x000063F5
		protected virtual DbConnection CreateConnection()
		{
			return DbConfiguration.DependencyResolver.GetService("System.Data.SqlClient").CreateConnection();
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000820B File Offset: 0x0000640B
		protected virtual void Generate(CreateProcedureOperation createProcedureOperation)
		{
			Check.NotNull<CreateProcedureOperation>(createProcedureOperation, "createProcedureOperation");
			this.Generate(createProcedureOperation, "CREATE");
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008225 File Offset: 0x00006425
		protected virtual void Generate(AlterProcedureOperation alterProcedureOperation)
		{
			Check.NotNull<AlterProcedureOperation>(alterProcedureOperation, "alterProcedureOperation");
			this.Generate(alterProcedureOperation, "ALTER");
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000082A8 File Offset: 0x000064A8
		private void Generate(ProcedureOperation procedureOperation, string modifier)
		{
			SqlServerMigrationSqlGenerator.<>c__DisplayClass9 CS$<>8__locals1 = new SqlServerMigrationSqlGenerator.<>c__DisplayClass9();
			CS$<>8__locals1.procedureOperation = procedureOperation;
			CS$<>8__locals1.<>4__this = this;
			using (IndentedTextWriter writer = SqlServerMigrationSqlGenerator.Writer())
			{
				writer.Write(modifier);
				writer.WriteLine(" PROCEDURE " + this.Name(CS$<>8__locals1.procedureOperation.Name));
				writer.Indent++;
				CS$<>8__locals1.procedureOperation.Parameters.Each(delegate(ParameterModel p, int i)
				{
					CS$<>8__locals1.<>4__this.Generate(p, writer);
					writer.WriteLine((i < CS$<>8__locals1.procedureOperation.Parameters.Count - 1) ? "," : string.Empty);
				});
				writer.Indent--;
				writer.WriteLine("AS");
				writer.WriteLine("BEGIN");
				writer.Indent++;
				writer.WriteLine((!string.IsNullOrWhiteSpace(CS$<>8__locals1.procedureOperation.BodySql)) ? SqlServerMigrationSqlGenerator.Indent(CS$<>8__locals1.procedureOperation.BodySql, writer.CurrentIndentation()) : "RETURN");
				writer.Indent--;
				writer.Write("END");
				this.Statement(writer, "GO");
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008434 File Offset: 0x00006634
		private void Generate(ParameterModel parameterModel, IndentedTextWriter writer)
		{
			writer.Write("@");
			writer.Write(parameterModel.Name);
			writer.Write(" ");
			writer.Write(this.BuildPropertyType(parameterModel));
			if (parameterModel.IsOutParameter)
			{
				writer.Write(" OUT");
			}
			if (parameterModel.DefaultValue != null)
			{
				writer.Write(" = ");
				if (SqlServerMigrationSqlGenerator.<Generate>o__SiteContainere.<>p__Sitef == null)
				{
					SqlServerMigrationSqlGenerator.<Generate>o__SiteContainere.<>p__Sitef = CallSite<Action<CallSite, IndentedTextWriter, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Write", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Action<CallSite, IndentedTextWriter, object> target = SqlServerMigrationSqlGenerator.<Generate>o__SiteContainere.<>p__Sitef.Target;
				CallSite <>p__Sitef = SqlServerMigrationSqlGenerator.<Generate>o__SiteContainere.<>p__Sitef;
				if (SqlServerMigrationSqlGenerator.<Generate>o__SiteContainere.<>p__Site10 == null)
				{
					SqlServerMigrationSqlGenerator.<Generate>o__SiteContainere.<>p__Site10 = CallSite<Func<CallSite, SqlServerMigrationSqlGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				target(<>p__Sitef, writer, SqlServerMigrationSqlGenerator.<Generate>o__SiteContainere.<>p__Site10.Target(SqlServerMigrationSqlGenerator.<Generate>o__SiteContainere.<>p__Site10, this, parameterModel.DefaultValue));
				return;
			}
			if (!string.IsNullOrWhiteSpace(parameterModel.DefaultValueSql))
			{
				writer.Write(" = ");
				writer.Write(parameterModel.DefaultValueSql);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008578 File Offset: 0x00006778
		protected virtual void Generate(DropProcedureOperation dropProcedureOperation)
		{
			Check.NotNull<DropProcedureOperation>(dropProcedureOperation, "dropProcedureOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("DROP PROCEDURE ");
				indentedTextWriter.Write(this.Name(dropProcedureOperation.Name));
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000085D8 File Offset: 0x000067D8
		protected virtual void Generate(CreateTableOperation createTableOperation)
		{
			Check.NotNull<CreateTableOperation>(createTableOperation, "createTableOperation");
			DatabaseName databaseName = DatabaseName.Parse(createTableOperation.Name);
			if (!string.IsNullOrWhiteSpace(databaseName.Schema) && !databaseName.Schema.EqualsIgnoreCase("dbo") && !this._generatedSchemas.Contains(databaseName.Schema))
			{
				this.GenerateCreateSchema(databaseName.Schema);
				this._generatedSchemas.Add(databaseName.Schema);
			}
			this.WriteCreateTable(createTableOperation);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008654 File Offset: 0x00006854
		protected virtual void WriteCreateTable(CreateTableOperation createTableOperation)
		{
			Check.NotNull<CreateTableOperation>(createTableOperation, "createTableOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				this.WriteCreateTable(createTableOperation, indentedTextWriter);
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000086E4 File Offset: 0x000068E4
		protected virtual void WriteCreateTable(CreateTableOperation createTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateTableOperation>(createTableOperation, "createTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("CREATE TABLE " + this.Name(createTableOperation.Name) + " (");
			writer.Indent++;
			createTableOperation.Columns.Each(delegate(ColumnModel c, int i)
			{
				this.Generate(c, writer);
				if (i < createTableOperation.Columns.Count - 1)
				{
					writer.WriteLine(",");
				}
			});
			if (createTableOperation.PrimaryKey != null)
			{
				writer.WriteLine(",");
				writer.Write("CONSTRAINT ");
				writer.Write(this.Quote(createTableOperation.PrimaryKey.Name));
				writer.Write(" PRIMARY KEY ");
				if (!createTableOperation.PrimaryKey.IsClustered)
				{
					writer.Write("NONCLUSTERED ");
				}
				writer.Write("(");
				writer.Write(createTableOperation.PrimaryKey.Columns.Join(new Func<string, string>(this.Quote), ", "));
				writer.WriteLine(")");
			}
			else
			{
				writer.WriteLine();
			}
			writer.Indent--;
			writer.Write(")");
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000888D File Offset: 0x00006A8D
		protected internal virtual void Generate(AlterTableOperation alterTableOperation)
		{
			Check.NotNull<AlterTableOperation>(alterTableOperation, "alterTableOperation");
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000889C File Offset: 0x00006A9C
		protected virtual void GenerateMakeSystemTable(CreateTableOperation createTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateTableOperation>(createTableOperation, "createTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("BEGIN TRY");
			writer.Indent++;
			writer.WriteLine("EXECUTE sp_MS_marksystemobject '" + SqlServerMigrationSqlGenerator.Escape(createTableOperation.Name) + "'");
			writer.Indent--;
			writer.WriteLine("END TRY");
			writer.WriteLine("BEGIN CATCH");
			writer.Write("END CATCH");
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000892C File Offset: 0x00006B2C
		protected virtual void GenerateCreateSchema(string schema)
		{
			Check.NotEmpty(schema, "schema");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("IF schema_id('");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(schema));
				indentedTextWriter.WriteLine("') IS NULL");
				indentedTextWriter.Indent++;
				indentedTextWriter.Write("EXECUTE('CREATE SCHEMA ");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(this.Quote(schema)));
				indentedTextWriter.Write("')");
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000089C8 File Offset: 0x00006BC8
		protected virtual void Generate(AddForeignKeyOperation addForeignKeyOperation)
		{
			Check.NotNull<AddForeignKeyOperation>(addForeignKeyOperation, "addForeignKeyOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("ALTER TABLE ");
				indentedTextWriter.Write(this.Name(addForeignKeyOperation.DependentTable));
				indentedTextWriter.Write(" ADD CONSTRAINT ");
				indentedTextWriter.Write(this.Quote(addForeignKeyOperation.Name));
				indentedTextWriter.Write(" FOREIGN KEY (");
				indentedTextWriter.Write(addForeignKeyOperation.DependentColumns.Select(new Func<string, string>(this.Quote)).Join(null, ", "));
				indentedTextWriter.Write(") REFERENCES ");
				indentedTextWriter.Write(this.Name(addForeignKeyOperation.PrincipalTable));
				indentedTextWriter.Write(" (");
				indentedTextWriter.Write(addForeignKeyOperation.PrincipalColumns.Select(new Func<string, string>(this.Quote)).Join(null, ", "));
				indentedTextWriter.Write(")");
				if (addForeignKeyOperation.CascadeDelete)
				{
					indentedTextWriter.Write(" ON DELETE CASCADE");
				}
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008AE8 File Offset: 0x00006CE8
		protected virtual void Generate(DropForeignKeyOperation dropForeignKeyOperation)
		{
			Check.NotNull<DropForeignKeyOperation>(dropForeignKeyOperation, "dropForeignKeyOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("IF object_id(N'");
				string schema = DatabaseName.Parse(dropForeignKeyOperation.DependentTable).Schema;
				if (schema != null)
				{
					indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(this.Quote(schema)));
					indentedTextWriter.Write(".");
				}
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(this.Quote(dropForeignKeyOperation.Name)));
				indentedTextWriter.WriteLine("', N'F') IS NOT NULL");
				indentedTextWriter.Indent++;
				indentedTextWriter.Write("ALTER TABLE ");
				indentedTextWriter.Write(this.Name(dropForeignKeyOperation.DependentTable));
				indentedTextWriter.Write(" DROP CONSTRAINT ");
				indentedTextWriter.Write(this.Quote(dropForeignKeyOperation.Name));
				indentedTextWriter.Indent--;
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008BE0 File Offset: 0x00006DE0
		protected virtual void Generate(CreateIndexOperation createIndexOperation)
		{
			Check.NotNull<CreateIndexOperation>(createIndexOperation, "createIndexOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("CREATE ");
				if (createIndexOperation.IsUnique)
				{
					indentedTextWriter.Write("UNIQUE ");
				}
				if (createIndexOperation.IsClustered)
				{
					indentedTextWriter.Write("CLUSTERED ");
				}
				indentedTextWriter.Write("INDEX ");
				indentedTextWriter.Write(this.Quote(createIndexOperation.Name));
				indentedTextWriter.Write(" ON ");
				indentedTextWriter.Write(this.Name(createIndexOperation.Table));
				indentedTextWriter.Write("(");
				indentedTextWriter.Write(createIndexOperation.Columns.Join(new Func<string, string>(this.Quote), ", "));
				indentedTextWriter.Write(")");
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008CC8 File Offset: 0x00006EC8
		protected virtual void Generate(DropIndexOperation dropIndexOperation)
		{
			Check.NotNull<DropIndexOperation>(dropIndexOperation, "dropIndexOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(dropIndexOperation.Name));
				indentedTextWriter.Write("' AND object_id = object_id(N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(this.Name(dropIndexOperation.Table)));
				indentedTextWriter.WriteLine("', N'U'))");
				indentedTextWriter.Indent++;
				indentedTextWriter.Write("DROP INDEX ");
				indentedTextWriter.Write(this.Quote(dropIndexOperation.Name));
				indentedTextWriter.Write(" ON ");
				indentedTextWriter.Write(this.Name(dropIndexOperation.Table));
				indentedTextWriter.Indent--;
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008DAC File Offset: 0x00006FAC
		protected virtual void Generate(AddPrimaryKeyOperation addPrimaryKeyOperation)
		{
			Check.NotNull<AddPrimaryKeyOperation>(addPrimaryKeyOperation, "addPrimaryKeyOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("ALTER TABLE ");
				indentedTextWriter.Write(this.Name(addPrimaryKeyOperation.Table));
				indentedTextWriter.Write(" ADD CONSTRAINT ");
				indentedTextWriter.Write(this.Quote(addPrimaryKeyOperation.Name));
				indentedTextWriter.Write(" PRIMARY KEY ");
				if (!addPrimaryKeyOperation.IsClustered)
				{
					indentedTextWriter.Write("NONCLUSTERED ");
				}
				indentedTextWriter.Write("(");
				indentedTextWriter.Write(addPrimaryKeyOperation.Columns.Select(new Func<string, string>(this.Quote)).Join(null, ", "));
				indentedTextWriter.Write(")");
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008E88 File Offset: 0x00007088
		protected virtual void Generate(DropPrimaryKeyOperation dropPrimaryKeyOperation)
		{
			Check.NotNull<DropPrimaryKeyOperation>(dropPrimaryKeyOperation, "dropPrimaryKeyOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("ALTER TABLE ");
				indentedTextWriter.Write(this.Name(dropPrimaryKeyOperation.Table));
				indentedTextWriter.Write(" DROP CONSTRAINT ");
				indentedTextWriter.Write(this.Quote(dropPrimaryKeyOperation.Name));
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008F08 File Offset: 0x00007108
		protected virtual void Generate(AddColumnOperation addColumnOperation)
		{
			Check.NotNull<AddColumnOperation>(addColumnOperation, "addColumnOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("ALTER TABLE ");
				indentedTextWriter.Write(this.Name(addColumnOperation.Table));
				indentedTextWriter.Write(" ADD ");
				ColumnModel column = addColumnOperation.Column;
				this.Generate(column, indentedTextWriter);
				if (column.IsNullable != null && !column.IsNullable.Value && column.DefaultValue == null && string.IsNullOrWhiteSpace(column.DefaultValueSql) && !column.IsIdentity && !column.IsTimestamp && !column.StoreType.EqualsIgnoreCase("rowversion") && !column.StoreType.EqualsIgnoreCase("timestamp"))
				{
					indentedTextWriter.Write(" DEFAULT ");
					if (column.Type == PrimitiveTypeKind.DateTime)
					{
						indentedTextWriter.Write(this.Generate(DateTime.Parse("1900-01-01 00:00:00", CultureInfo.InvariantCulture)));
					}
					else
					{
						if (SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer14.<>p__Site15 == null)
						{
							SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer14.<>p__Site15 = CallSite<Action<CallSite, IndentedTextWriter, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Write", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						Action<CallSite, IndentedTextWriter, object> target = SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer14.<>p__Site15.Target;
						CallSite <>p__Site = SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer14.<>p__Site15;
						IndentedTextWriter arg = indentedTextWriter;
						if (SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer14.<>p__Site16 == null)
						{
							SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer14.<>p__Site16 = CallSite<Func<CallSite, SqlServerMigrationSqlGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						target(<>p__Site, arg, SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer14.<>p__Site16.Target(SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer14.<>p__Site16, this, column.ClrDefaultValue));
					}
				}
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00009108 File Offset: 0x00007308
		protected virtual void Generate(DropColumnOperation dropColumnOperation)
		{
			Check.NotNull<DropColumnOperation>(dropColumnOperation, "dropColumnOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				this.DropDefaultConstraint(dropColumnOperation.Table, dropColumnOperation.Name, indentedTextWriter);
				indentedTextWriter.Write("ALTER TABLE ");
				indentedTextWriter.Write(this.Name(dropColumnOperation.Table));
				indentedTextWriter.Write(" DROP COLUMN ");
				indentedTextWriter.Write(this.Quote(dropColumnOperation.Name));
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00009198 File Offset: 0x00007398
		protected virtual void Generate(AlterColumnOperation alterColumnOperation)
		{
			Check.NotNull<AlterColumnOperation>(alterColumnOperation, "alterColumnOperation");
			ColumnModel column = alterColumnOperation.Column;
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				if (column.DefaultValue != null || !string.IsNullOrWhiteSpace(column.DefaultValueSql))
				{
					this.DropDefaultConstraint(alterColumnOperation.Table, column.Name, indentedTextWriter);
				}
				indentedTextWriter.Write("ALTER TABLE ");
				indentedTextWriter.Write(this.Name(alterColumnOperation.Table));
				indentedTextWriter.Write(" ALTER COLUMN ");
				indentedTextWriter.Write(this.Quote(column.Name));
				indentedTextWriter.Write(" ");
				indentedTextWriter.Write(this.BuildColumnType(column));
				if (column.IsNullable != null && !column.IsNullable.Value)
				{
					indentedTextWriter.Write(" NOT");
				}
				indentedTextWriter.Write(" NULL");
				if (column.DefaultValue != null || !string.IsNullOrWhiteSpace(column.DefaultValueSql))
				{
					indentedTextWriter.WriteLine();
					indentedTextWriter.Write("ALTER TABLE ");
					indentedTextWriter.Write(this.Name(alterColumnOperation.Table));
					indentedTextWriter.Write(" ADD CONSTRAINT ");
					indentedTextWriter.Write(this.Quote("DF_" + alterColumnOperation.Table + "_" + column.Name));
					indentedTextWriter.Write(" DEFAULT ");
					if (SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer17.<>p__Site18 == null)
					{
						SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer17.<>p__Site18 = CallSite<Action<CallSite, IndentedTextWriter, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Write", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Action<CallSite, IndentedTextWriter, object> target = SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer17.<>p__Site18.Target;
					CallSite <>p__Site = SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer17.<>p__Site18;
					IndentedTextWriter arg = indentedTextWriter;
					object arg2;
					if (column.DefaultValue == null)
					{
						arg2 = column.DefaultValueSql;
					}
					else
					{
						if (SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer17.<>p__Site19 == null)
						{
							SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer17.<>p__Site19 = CallSite<Func<CallSite, SqlServerMigrationSqlGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						arg2 = SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer17.<>p__Site19.Target(SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer17.<>p__Site19, this, column.DefaultValue);
					}
					target(<>p__Site, arg, arg2);
					indentedTextWriter.Write(" FOR ");
					indentedTextWriter.Write(this.Quote(column.Name));
				}
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00009408 File Offset: 0x00007608
		protected internal virtual void DropDefaultConstraint(string table, string column, IndentedTextWriter writer)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(column, "column");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			string value = "@var" + this._variableCounter++;
			writer.Write("DECLARE ");
			writer.Write(value);
			writer.WriteLine(" nvarchar(128)");
			writer.Write("SELECT ");
			writer.Write(value);
			writer.WriteLine(" = name");
			writer.WriteLine("FROM sys.default_constraints");
			writer.Write("WHERE parent_object_id = object_id(N'");
			writer.Write(table);
			writer.WriteLine("')");
			writer.Write("AND col_name(parent_object_id, parent_column_id) = '");
			writer.Write(column);
			writer.WriteLine("';");
			writer.Write("IF ");
			writer.Write(value);
			writer.WriteLine(" IS NOT NULL");
			writer.Indent++;
			writer.Write("EXECUTE('ALTER TABLE ");
			writer.Write(SqlServerMigrationSqlGenerator.Escape(this.Name(table)));
			writer.Write(" DROP CONSTRAINT [' + ");
			writer.Write(value);
			writer.WriteLine(" + ']')");
			writer.Indent--;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000954C File Offset: 0x0000774C
		protected virtual void Generate(DropTableOperation dropTableOperation)
		{
			Check.NotNull<DropTableOperation>(dropTableOperation, "dropTableOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("DROP TABLE ");
				indentedTextWriter.Write(this.Name(dropTableOperation.Name));
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000095AC File Offset: 0x000077AC
		protected virtual void Generate(SqlOperation sqlOperation)
		{
			Check.NotNull<SqlOperation>(sqlOperation, "sqlOperation");
			this.StatementBatch(sqlOperation.Sql, sqlOperation.SuppressTransaction);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000095CC File Offset: 0x000077CC
		protected virtual void Generate(RenameColumnOperation renameColumnOperation)
		{
			Check.NotNull<RenameColumnOperation>(renameColumnOperation, "renameColumnOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("EXECUTE sp_rename @objname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameColumnOperation.Table));
				indentedTextWriter.Write(".");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameColumnOperation.Name));
				indentedTextWriter.Write("', @newname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameColumnOperation.NewName));
				indentedTextWriter.Write("', @objtype = N'COLUMN'");
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00009670 File Offset: 0x00007870
		protected virtual void Generate(RenameIndexOperation renameIndexOperation)
		{
			Check.NotNull<RenameIndexOperation>(renameIndexOperation, "renameIndexOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("EXECUTE sp_rename @objname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameIndexOperation.Table));
				indentedTextWriter.Write(".");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameIndexOperation.Name));
				indentedTextWriter.Write("', @newname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameIndexOperation.NewName));
				indentedTextWriter.Write("', @objtype = N'INDEX'");
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00009714 File Offset: 0x00007914
		protected virtual void Generate(RenameTableOperation renameTableOperation)
		{
			Check.NotNull<RenameTableOperation>(renameTableOperation, "renameTableOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				SqlServerMigrationSqlGenerator.WriteRenameTable(renameTableOperation, indentedTextWriter);
				string identifier = PrimaryKeyOperation.BuildDefaultName(renameTableOperation.Name);
				string s = PrimaryKeyOperation.BuildDefaultName(((RenameTableOperation)renameTableOperation.Inverse).Name);
				indentedTextWriter.WriteLine();
				indentedTextWriter.Write("IF object_id('");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(this.Quote(identifier)));
				indentedTextWriter.WriteLine("') IS NOT NULL BEGIN");
				indentedTextWriter.Indent++;
				indentedTextWriter.Write("EXECUTE sp_rename @objname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(this.Quote(identifier)));
				indentedTextWriter.Write("', @newname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(s));
				indentedTextWriter.WriteLine("', @objtype = N'OBJECT'");
				indentedTextWriter.Indent--;
				indentedTextWriter.Write("END");
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00009814 File Offset: 0x00007A14
		private static void WriteRenameTable(RenameTableOperation renameTableOperation, IndentedTextWriter writer)
		{
			writer.Write("EXECUTE sp_rename @objname = N'");
			writer.Write(SqlServerMigrationSqlGenerator.Escape(renameTableOperation.Name));
			writer.Write("', @newname = N'");
			writer.Write(SqlServerMigrationSqlGenerator.Escape(renameTableOperation.NewName));
			writer.Write("', @objtype = N'OBJECT'");
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00009864 File Offset: 0x00007A64
		protected virtual void Generate(RenameProcedureOperation renameProcedureOperation)
		{
			Check.NotNull<RenameProcedureOperation>(renameProcedureOperation, "renameProcedureOperation");
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("EXECUTE sp_rename @objname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameProcedureOperation.Name));
				indentedTextWriter.Write("', @newname = N'");
				indentedTextWriter.Write(SqlServerMigrationSqlGenerator.Escape(renameProcedureOperation.NewName));
				indentedTextWriter.Write("', @objtype = N'OBJECT'");
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000098EC File Offset: 0x00007AEC
		protected virtual void Generate(MoveProcedureOperation moveProcedureOperation)
		{
			Check.NotNull<MoveProcedureOperation>(moveProcedureOperation, "moveProcedureOperation");
			string text = moveProcedureOperation.NewSchema ?? "dbo";
			if (!text.EqualsIgnoreCase("dbo") && !this._generatedSchemas.Contains(text))
			{
				this.GenerateCreateSchema(text);
				this._generatedSchemas.Add(text);
			}
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter.Write("ALTER SCHEMA ");
				indentedTextWriter.Write(this.Quote(text));
				indentedTextWriter.Write(" TRANSFER ");
				indentedTextWriter.Write(this.Name(moveProcedureOperation.Name));
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000099A4 File Offset: 0x00007BA4
		protected virtual void Generate(MoveTableOperation moveTableOperation)
		{
			Check.NotNull<MoveTableOperation>(moveTableOperation, "moveTableOperation");
			string text = moveTableOperation.NewSchema ?? "dbo";
			if (!text.EqualsIgnoreCase("dbo") && !this._generatedSchemas.Contains(text))
			{
				this.GenerateCreateSchema(text);
				this._generatedSchemas.Add(text);
			}
			if (!moveTableOperation.IsSystem)
			{
				using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
				{
					indentedTextWriter.Write("ALTER SCHEMA ");
					indentedTextWriter.Write(this.Quote(text));
					indentedTextWriter.Write(" TRANSFER ");
					indentedTextWriter.Write(this.Name(moveTableOperation.Name));
					this.Statement(indentedTextWriter, null);
					return;
				}
			}
			using (IndentedTextWriter indentedTextWriter2 = SqlServerMigrationSqlGenerator.Writer())
			{
				indentedTextWriter2.Write("IF object_id('");
				indentedTextWriter2.Write(moveTableOperation.CreateTableOperation.Name);
				indentedTextWriter2.WriteLine("') IS NULL BEGIN");
				indentedTextWriter2.Indent++;
				this.WriteCreateTable(moveTableOperation.CreateTableOperation, indentedTextWriter2);
				indentedTextWriter2.WriteLine();
				indentedTextWriter2.Indent--;
				indentedTextWriter2.WriteLine("END");
				indentedTextWriter2.Write("INSERT INTO ");
				indentedTextWriter2.WriteLine(this.Name(moveTableOperation.CreateTableOperation.Name));
				indentedTextWriter2.Write("SELECT * FROM ");
				indentedTextWriter2.WriteLine(this.Name(moveTableOperation.Name));
				indentedTextWriter2.Write("WHERE [ContextKey] = ");
				indentedTextWriter2.WriteLine(this.Generate(moveTableOperation.ContextKey));
				indentedTextWriter2.Write("DELETE ");
				indentedTextWriter2.WriteLine(this.Name(moveTableOperation.Name));
				indentedTextWriter2.Write("WHERE [ContextKey] = ");
				indentedTextWriter2.WriteLine(this.Generate(moveTableOperation.ContextKey));
				indentedTextWriter2.Write("IF NOT EXISTS(SELECT * FROM ");
				indentedTextWriter2.Write(this.Name(moveTableOperation.Name));
				indentedTextWriter2.WriteLine(")");
				indentedTextWriter2.Indent++;
				indentedTextWriter2.Write("DROP TABLE ");
				indentedTextWriter2.Write(this.Name(moveTableOperation.Name));
				indentedTextWriter2.Indent--;
				this.Statement(indentedTextWriter2, null);
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00009BFC File Offset: 0x00007DFC
		protected internal virtual void Generate(ColumnModel column, IndentedTextWriter writer)
		{
			Check.NotNull<ColumnModel>(column, "column");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write(this.Quote(column.Name));
			writer.Write(" ");
			writer.Write(this.BuildColumnType(column));
			if (column.IsNullable != null && !column.IsNullable.Value)
			{
				writer.Write(" NOT NULL");
			}
			if (column.DefaultValue != null)
			{
				writer.Write(" DEFAULT ");
				if (SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer1a.<>p__Site1b == null)
				{
					SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer1a.<>p__Site1b = CallSite<Action<CallSite, IndentedTextWriter, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Write", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Action<CallSite, IndentedTextWriter, object> target = SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer1a.<>p__Site1b.Target;
				CallSite <>p__Site1b = SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer1a.<>p__Site1b;
				if (SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer1a.<>p__Site1c == null)
				{
					SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer1a.<>p__Site1c = CallSite<Func<CallSite, SqlServerMigrationSqlGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(SqlServerMigrationSqlGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				target(<>p__Site1b, writer, SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer1a.<>p__Site1c.Target(SqlServerMigrationSqlGenerator.<Generate>o__SiteContainer1a.<>p__Site1c, this, column.DefaultValue));
				return;
			}
			if (!string.IsNullOrWhiteSpace(column.DefaultValueSql))
			{
				writer.Write(" DEFAULT ");
				writer.Write(column.DefaultValueSql);
				return;
			}
			if (column.IsIdentity)
			{
				if (column.Type == PrimitiveTypeKind.Guid && column.DefaultValue == null)
				{
					writer.Write(" DEFAULT " + this.GuidColumnDefault);
					return;
				}
				writer.Write(" IDENTITY");
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00009DA7 File Offset: 0x00007FA7
		protected virtual string GuidColumnDefault
		{
			get
			{
				if (!(this._providerManifestToken != "2012.Azure") || !(this._providerManifestToken != "2000"))
				{
					return "newid()";
				}
				return "newsequentialid()";
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00009E54 File Offset: 0x00008054
		protected virtual void Generate(HistoryOperation historyOperation)
		{
			Check.NotNull<HistoryOperation>(historyOperation, "historyOperation");
			using (IndentedTextWriter writer = SqlServerMigrationSqlGenerator.Writer())
			{
				historyOperation.CommandTrees.Each(delegate(DbModificationCommandTree commandTree)
				{
					switch (commandTree.CommandTreeKind)
					{
					case DbCommandTreeKind.Insert:
					{
						List<SqlParameter> list;
						writer.Write(DmlSqlGenerator.GenerateInsertSql((DbInsertCommandTree)commandTree, this._sqlGenerator, out list, false, true, false));
						return;
					}
					case DbCommandTreeKind.Delete:
					{
						List<SqlParameter> list;
						writer.Write(DmlSqlGenerator.GenerateDeleteSql((DbDeleteCommandTree)commandTree, this._sqlGenerator, out list, true, false));
						return;
					}
					default:
						return;
					}
				});
				this.Statement(writer, null);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00009ED8 File Offset: 0x000080D8
		protected virtual string Generate(byte[] defaultValue)
		{
			Check.NotNull<byte[]>(defaultValue, "defaultValue");
			return "0x" + defaultValue.ToHexString();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00009EF6 File Offset: 0x000080F6
		protected virtual string Generate(bool defaultValue)
		{
			if (!defaultValue)
			{
				return "0";
			}
			return "1";
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00009F06 File Offset: 0x00008106
		protected virtual string Generate(DateTime defaultValue)
		{
			return "'" + defaultValue.ToString("yyyy-MM-ddTHH:mm:ss.fffK", CultureInfo.InvariantCulture) + "'";
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00009F28 File Offset: 0x00008128
		protected virtual string Generate(DateTimeOffset defaultValue)
		{
			return "'" + defaultValue.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", CultureInfo.InvariantCulture) + "'";
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009F4A File Offset: 0x0000814A
		protected virtual string Generate(Guid defaultValue)
		{
			return "'" + defaultValue + "'";
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009F61 File Offset: 0x00008161
		protected virtual string Generate(string defaultValue)
		{
			Check.NotNull<string>(defaultValue, "defaultValue");
			return "'" + defaultValue + "'";
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009F7F File Offset: 0x0000817F
		protected virtual string Generate(TimeSpan defaultValue)
		{
			return "'" + defaultValue + "'";
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009F96 File Offset: 0x00008196
		protected virtual string Generate(DbGeography defaultValue)
		{
			return "'" + defaultValue + "'";
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009FA8 File Offset: 0x000081A8
		protected virtual string Generate(DbGeometry defaultValue)
		{
			return "'" + defaultValue + "'";
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00009FBC File Offset: 0x000081BC
		protected virtual string Generate(object defaultValue)
		{
			Check.NotNull<object>(defaultValue, "defaultValue");
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				defaultValue
			});
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009FF0 File Offset: 0x000081F0
		protected virtual string BuildColumnType(ColumnModel columnModel)
		{
			Check.NotNull<ColumnModel>(columnModel, "columnModel");
			if (columnModel.IsTimestamp)
			{
				return "rowversion";
			}
			return this.BuildPropertyType(columnModel);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A014 File Offset: 0x00008214
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private string BuildPropertyType(PropertyModel propertyModel)
		{
			string text = propertyModel.StoreType;
			TypeUsage typeUsage = base.ProviderManifest.GetStoreType(propertyModel.TypeUsage);
			if (string.IsNullOrWhiteSpace(text))
			{
				text = typeUsage.EdmType.Name;
			}
			else
			{
				TypeUsage typeUsage2 = this.BuildStoreTypeUsage(text, propertyModel);
				typeUsage = (typeUsage2 ?? typeUsage);
			}
			string text2 = text;
			if (text2.EndsWith("(max)", StringComparison.Ordinal))
			{
				text2 = this.Quote(text2.Substring(0, text2.Length - "(max)".Length)) + "(max)";
			}
			else
			{
				text2 = this.Quote(text2);
			}
			string key;
			switch (key = text)
			{
			case "decimal":
			case "numeric":
			{
				object obj = text2;
				text2 = string.Concat(new object[]
				{
					obj,
					"(",
					propertyModel.Precision ?? typeUsage.GetPrecision(),
					", ",
					propertyModel.Scale ?? typeUsage.GetScale(),
					")"
				});
				break;
			}
			case "datetime2":
			case "datetimeoffset":
			case "time":
			{
				object obj2 = text2;
				text2 = string.Concat(new object[]
				{
					obj2,
					"(",
					propertyModel.Precision ?? typeUsage.GetPrecision(),
					")"
				});
				break;
			}
			case "binary":
			case "varbinary":
			case "nvarchar":
			case "varchar":
			case "char":
			case "nchar":
			{
				object obj3 = text2;
				text2 = string.Concat(new object[]
				{
					obj3,
					"(",
					propertyModel.MaxLength ?? typeUsage.GetMaxLength(),
					")"
				});
				break;
			}
			}
			return text2;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A2D0 File Offset: 0x000084D0
		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#")]
		protected virtual string Name(string name)
		{
			Check.NotEmpty(name, "name");
			DatabaseName databaseName = DatabaseName.Parse(name);
			return new string[]
			{
				databaseName.Schema,
				databaseName.Name
			}.Join(new Func<string, string>(this.Quote), ".");
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A321 File Offset: 0x00008521
		protected virtual string Quote(string identifier)
		{
			Check.NotEmpty(identifier, "identifier");
			return SqlGenerator.QuoteIdentifier(identifier);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A335 File Offset: 0x00008535
		private static string Escape(string s)
		{
			return s.Replace("'", "''");
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A347 File Offset: 0x00008547
		private static string Indent(string s, string indentation)
		{
			return new Regex("\\r?\\n *").Replace(s, Environment.NewLine + indentation);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A364 File Offset: 0x00008564
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected void Statement(string sql, bool suppressTransaction = false, string batchTerminator = null)
		{
			Check.NotEmpty(sql, "sql");
			this._statements.Add(new MigrationStatement
			{
				Sql = sql,
				SuppressTransaction = suppressTransaction,
				BatchTerminator = batchTerminator
			});
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000A3A4 File Offset: 0x000085A4
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		protected static IndentedTextWriter Writer()
		{
			return new IndentedTextWriter(new StringWriter(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000A3B5 File Offset: 0x000085B5
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected void Statement(IndentedTextWriter writer, string batchTerminator = null)
		{
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			this.Statement(writer.InnerWriter.ToString(), false, batchTerminator);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000A3D8 File Offset: 0x000085D8
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected void StatementBatch(string sqlBatch, bool suppressTransaction = false)
		{
			Check.NotNull<string>(sqlBatch, "sqlBatch");
			sqlBatch = Regex.Replace(sqlBatch, "\\\\(\\r\\n|\\r|\\n)", "");
			string[] array = Regex.Split(sqlBatch, string.Format(CultureInfo.InvariantCulture, "^\\s*({0}[ \\t]+[0-9]+|{0})(?:\\s+|$)", new object[]
			{
				"GO"
			}), RegexOptions.IgnoreCase | RegexOptions.Multiline);
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].StartsWith("GO", StringComparison.OrdinalIgnoreCase) && (i != array.Length - 1 || !string.IsNullOrWhiteSpace(array[i])))
				{
					if (array.Length > i + 1 && array[i + 1].StartsWith("GO", StringComparison.OrdinalIgnoreCase))
					{
						int num = 1;
						if (!array[i + 1].EqualsIgnoreCase("GO"))
						{
							num = int.Parse(Regex.Match(array[i + 1], "([0-9]+)").Value, CultureInfo.InvariantCulture);
						}
						for (int j = 0; j < num; j++)
						{
							this.Statement(array[i], suppressTransaction, "GO");
						}
					}
					else
					{
						this.Statement(array[i], suppressTransaction, null);
					}
				}
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000A5E4 File Offset: 0x000087E4
		private static IEnumerable<MigrationOperation> DetectHistoryRebuild(IEnumerable<MigrationOperation> operations)
		{
			IEnumerator<MigrationOperation> enumerator = operations.GetEnumerator();
			while (enumerator.MoveNext())
			{
				SqlServerMigrationSqlGenerator.HistoryRebuildOperationSequence sequence = SqlServerMigrationSqlGenerator.HistoryRebuildOperationSequence.Detect(enumerator);
				yield return sequence ?? enumerator.Current;
			}
			yield break;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000A604 File Offset: 0x00008804
		private void Generate(SqlServerMigrationSqlGenerator.HistoryRebuildOperationSequence sequence)
		{
			CreateTableOperation createTableOperation = sequence.DropPrimaryKeyOperation.CreateTableOperation;
			CreateTableOperation createTableOperation2 = SqlServerMigrationSqlGenerator.ResolveNameConflicts(createTableOperation);
			RenameTableOperation renameTableOperation = new RenameTableOperation(createTableOperation2.Name, "__MigrationHistory", null);
			using (IndentedTextWriter indentedTextWriter = SqlServerMigrationSqlGenerator.Writer())
			{
				this.WriteCreateTable(createTableOperation2, indentedTextWriter);
				indentedTextWriter.WriteLine();
				indentedTextWriter.Write("INSERT INTO ");
				indentedTextWriter.WriteLine(this.Name(createTableOperation2.Name));
				indentedTextWriter.Write("SELECT ");
				bool flag = true;
				foreach (ColumnModel columnModel in createTableOperation.Columns)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						indentedTextWriter.Write(", ");
					}
					indentedTextWriter.Write((columnModel.Name == sequence.AddColumnOperation.Column.Name) ? this.Generate((string)sequence.AddColumnOperation.Column.DefaultValue) : ((columnModel.Type == PrimitiveTypeKind.String) ? string.Concat(new object[]
					{
						"LEFT(",
						this.Name(columnModel.Name),
						", ",
						columnModel.MaxLength,
						")"
					}) : this.Name(columnModel.Name)));
				}
				indentedTextWriter.Write(" FROM ");
				indentedTextWriter.WriteLine(this.Name(createTableOperation.Name));
				indentedTextWriter.Write("DROP TABLE ");
				indentedTextWriter.WriteLine(this.Name(createTableOperation.Name));
				SqlServerMigrationSqlGenerator.WriteRenameTable(renameTableOperation, indentedTextWriter);
				this.Statement(indentedTextWriter, null);
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000A824 File Offset: 0x00008A24
		private static CreateTableOperation ResolveNameConflicts(CreateTableOperation source)
		{
			CreateTableOperation target = new CreateTableOperation(source.Name + "2", null)
			{
				PrimaryKey = new AddPrimaryKeyOperation(null)
			};
			source.Columns.Each(delegate(ColumnModel c)
			{
				target.Columns.Add(c);
			});
			source.PrimaryKey.Columns.Each(delegate(string c)
			{
				target.PrimaryKey.Columns.Add(c);
			});
			return target;
		}

		// Token: 0x04000071 RID: 113
		private const string BatchTerminator = "GO";

		// Token: 0x04000072 RID: 114
		internal const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffK";

		// Token: 0x04000073 RID: 115
		internal const string DateTimeOffsetFormat = "yyyy-MM-ddTHH:mm:ss.fffzzz";

		// Token: 0x04000074 RID: 116
		private SqlGenerator _sqlGenerator;

		// Token: 0x04000075 RID: 117
		private List<MigrationStatement> _statements;

		// Token: 0x04000076 RID: 118
		private HashSet<string> _generatedSchemas;

		// Token: 0x04000077 RID: 119
		private string _providerManifestToken;

		// Token: 0x04000078 RID: 120
		private int _variableCounter;

		// Token: 0x02000025 RID: 37
		private class HistoryRebuildOperationSequence : MigrationOperation
		{
			// Token: 0x06000231 RID: 561 RVA: 0x0000A8A2 File Offset: 0x00008AA2
			private HistoryRebuildOperationSequence(AddColumnOperation addColumnOperation, DropPrimaryKeyOperation dropPrimaryKeyOperation) : base(null)
			{
				this.AddColumnOperation = addColumnOperation;
				this.DropPrimaryKeyOperation = dropPrimaryKeyOperation;
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000232 RID: 562 RVA: 0x0000A8B9 File Offset: 0x00008AB9
			public override bool IsDestructiveChange
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000233 RID: 563 RVA: 0x0000A8BC File Offset: 0x00008ABC
			public static SqlServerMigrationSqlGenerator.HistoryRebuildOperationSequence Detect(IEnumerator<MigrationOperation> enumerator)
			{
				AddColumnOperation addColumnOperation = enumerator.Current as AddColumnOperation;
				if (addColumnOperation == null || addColumnOperation.Table != "dbo.__MigrationHistory" || addColumnOperation.Column.Name != "ContextKey")
				{
					return null;
				}
				enumerator.MoveNext();
				DropPrimaryKeyOperation dropPrimaryKeyOperation = (DropPrimaryKeyOperation)enumerator.Current;
				enumerator.MoveNext();
				AlterColumnOperation alterColumnOperation = (AlterColumnOperation)enumerator.Current;
				enumerator.MoveNext();
				AddPrimaryKeyOperation addPrimaryKeyOperation = (AddPrimaryKeyOperation)enumerator.Current;
				return new SqlServerMigrationSqlGenerator.HistoryRebuildOperationSequence(addColumnOperation, dropPrimaryKeyOperation);
			}

			// Token: 0x04000079 RID: 121
			public readonly AddColumnOperation AddColumnOperation;

			// Token: 0x0400007A RID: 122
			public readonly DropPrimaryKeyOperation DropPrimaryKeyOperation;
		}

		// Token: 0x02000066 RID: 102
		[CompilerGenerated]
		private static class <GenerateStatements>o__SiteContainer0
		{
			// Token: 0x0400020C RID: 524
			public static CallSite<Action<CallSite, SqlServerMigrationSqlGenerator, object>> <>p__Site1;
		}

		// Token: 0x0200006A RID: 106
		[CompilerGenerated]
		private static class <Generate>o__SiteContainere
		{
			// Token: 0x04000212 RID: 530
			public static CallSite<Action<CallSite, IndentedTextWriter, object>> <>p__Sitef;

			// Token: 0x04000213 RID: 531
			public static CallSite<Func<CallSite, SqlServerMigrationSqlGenerator, object, object>> <>p__Site10;
		}

		// Token: 0x0200006C RID: 108
		[CompilerGenerated]
		private static class <Generate>o__SiteContainer14
		{
			// Token: 0x04000217 RID: 535
			public static CallSite<Action<CallSite, IndentedTextWriter, object>> <>p__Site15;

			// Token: 0x04000218 RID: 536
			public static CallSite<Func<CallSite, SqlServerMigrationSqlGenerator, object, object>> <>p__Site16;
		}

		// Token: 0x0200006D RID: 109
		[CompilerGenerated]
		private static class <Generate>o__SiteContainer17
		{
			// Token: 0x04000219 RID: 537
			public static CallSite<Action<CallSite, IndentedTextWriter, object>> <>p__Site18;

			// Token: 0x0400021A RID: 538
			public static CallSite<Func<CallSite, SqlServerMigrationSqlGenerator, object, object>> <>p__Site19;
		}

		// Token: 0x0200006E RID: 110
		[CompilerGenerated]
		private static class <Generate>o__SiteContainer1a
		{
			// Token: 0x0400021B RID: 539
			public static CallSite<Action<CallSite, IndentedTextWriter, object>> <>p__Site1b;

			// Token: 0x0400021C RID: 540
			public static CallSite<Func<CallSite, SqlServerMigrationSqlGenerator, object, object>> <>p__Site1c;
		}
	}
}
