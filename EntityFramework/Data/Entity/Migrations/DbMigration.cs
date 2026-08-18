using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Builders;
using System.Data.Entity.Migrations.Edm;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Migrations
{
	// Token: 0x020006CC RID: 1740
	public abstract class DbMigration : IDbMigration
	{
		// Token: 0x06004517 RID: 17687
		public abstract void Up();

		// Token: 0x06004518 RID: 17688 RVA: 0x00145BEB File Offset: 0x00143DEB
		public virtual void Down()
		{
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x00145C53 File Offset: 0x00143E53
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public void CreateStoredProcedure(string name, string body, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.CreateStoredProcedure<object>(name, (ParameterBuilder _) => new
			{

			}, body, anonymousArguments);
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x00145CE4 File Offset: 0x00143EE4
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public void CreateStoredProcedure<TParameters>(string name, Func<ParameterBuilder, TParameters> parametersAction, string body, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<Func<ParameterBuilder, TParameters>>(parametersAction, "parametersAction");
			CreateProcedureOperation createProcedureOperation = new CreateProcedureOperation(name, body, anonymousArguments);
			this.AddOperation(createProcedureOperation);
			TParameters parameters = parametersAction(new ParameterBuilder());
			parameters.GetType().GetNonIndexerProperties().Each(delegate(PropertyInfo p, int i)
			{
				ParameterModel parameterModel = p.GetValue(parameters, null) as ParameterModel;
				if (parameterModel != null)
				{
					if (string.IsNullOrWhiteSpace(parameterModel.Name))
					{
						parameterModel.Name = p.Name;
					}
					createProcedureOperation.Parameters.Add(parameterModel);
				}
			});
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x00145D69 File Offset: 0x00143F69
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public void AlterStoredProcedure(string name, string body, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.AlterStoredProcedure<object>(name, (ParameterBuilder _) => new
			{

			}, body, anonymousArguments);
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x00145DFC File Offset: 0x00143FFC
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public void AlterStoredProcedure<TParameters>(string name, Func<ParameterBuilder, TParameters> parametersAction, string body, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<Func<ParameterBuilder, TParameters>>(parametersAction, "parametersAction");
			AlterProcedureOperation alterProcedureOperation = new AlterProcedureOperation(name, body, anonymousArguments);
			this.AddOperation(alterProcedureOperation);
			TParameters parameters = parametersAction(new ParameterBuilder());
			parameters.GetType().GetNonIndexerProperties().Each(delegate(PropertyInfo p, int i)
			{
				ParameterModel parameterModel = p.GetValue(parameters, null) as ParameterModel;
				if (parameterModel != null)
				{
					if (string.IsNullOrWhiteSpace(parameterModel.Name))
					{
						parameterModel.Name = p.Name;
					}
					alterProcedureOperation.Parameters.Add(parameterModel);
				}
			});
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x00145E7A File Offset: 0x0014407A
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public void DropStoredProcedure(string name, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.AddOperation(new DropProcedureOperation(name, anonymousArguments));
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x00145E95 File Offset: 0x00144095
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal TableBuilder<TColumns> CreateTable<TColumns>(string name, Func<ColumnBuilder, TColumns> columnsAction, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<Func<ColumnBuilder, TColumns>>(columnsAction, "columnsAction");
			return this.CreateTable<TColumns>(name, columnsAction, null, anonymousArguments);
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x00145EBC File Offset: 0x001440BC
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal TableBuilder<TColumns> CreateTable<TColumns>(string name, Func<ColumnBuilder, TColumns> columnsAction, IDictionary<string, object> annotations, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<Func<ColumnBuilder, TColumns>>(columnsAction, "columnsAction");
			CreateTableOperation createTableOperation = new CreateTableOperation(name, annotations, anonymousArguments);
			this.AddOperation(createTableOperation);
			DbMigration.AddColumns<TColumns>(columnsAction(new ColumnBuilder()), createTableOperation.Columns);
			return new TableBuilder<TColumns>(createTableOperation, this);
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x00145F10 File Offset: 0x00144110
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void AlterTableAnnotations<TColumns>(string name, Func<ColumnBuilder, TColumns> columnsAction, IDictionary<string, AnnotationValues> annotations, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<Func<ColumnBuilder, TColumns>>(columnsAction, "columnsAction");
			AlterTableOperation alterTableOperation = new AlterTableOperation(name, annotations, anonymousArguments);
			DbMigration.AddColumns<TColumns>(columnsAction(new ColumnBuilder()), alterTableOperation.Columns);
			this.AddOperation(alterTableOperation);
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x00145FB8 File Offset: 0x001441B8
		private static void AddColumns<TColumns>(TColumns columns, ICollection<ColumnModel> columnModels)
		{
			columns.GetType().GetNonIndexerProperties().Each(delegate(PropertyInfo p, int i)
			{
				ColumnModel columnModel = p.GetValue(columns, null) as ColumnModel;
				if (columnModel != null)
				{
					columnModel.ApiPropertyInfo = p;
					if (string.IsNullOrWhiteSpace(columnModel.Name))
					{
						columnModel.Name = p.Name;
					}
					columnModels.Add(columnModel);
				}
			});
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x00146000 File Offset: 0x00144200
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void AddForeignKey(string dependentTable, string dependentColumn, string principalTable, string principalColumn = null, bool cascadeDelete = false, string name = null, object anonymousArguments = null)
		{
			Check.NotEmpty(dependentTable, "dependentTable");
			Check.NotEmpty(dependentColumn, "dependentColumn");
			Check.NotEmpty(principalTable, "principalTable");
			this.AddForeignKey(dependentTable, new string[]
			{
				dependentColumn
			}, principalTable, (principalColumn != null) ? new string[]
			{
				principalColumn
			} : null, cascadeDelete, name, anonymousArguments);
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x00146090 File Offset: 0x00144290
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void AddForeignKey(string dependentTable, string[] dependentColumns, string principalTable, string[] principalColumns = null, bool cascadeDelete = false, string name = null, object anonymousArguments = null)
		{
			Check.NotEmpty(dependentTable, "dependentTable");
			Check.NotNull<string[]>(dependentColumns, "dependentColumns");
			Check.NotEmpty(principalTable, "principalTable");
			if (!dependentColumns.Any<string>())
			{
				throw new ArgumentException(Strings.CollectionEmpty("dependentColumns", "AddForeignKey"));
			}
			AddForeignKeyOperation addForeignKeyOperation = new AddForeignKeyOperation(anonymousArguments)
			{
				DependentTable = dependentTable,
				PrincipalTable = principalTable,
				CascadeDelete = cascadeDelete,
				Name = name
			};
			dependentColumns.Each(delegate(string c)
			{
				addForeignKeyOperation.DependentColumns.Add(c);
			});
			if (principalColumns != null)
			{
				principalColumns.Each(delegate(string c)
				{
					addForeignKeyOperation.PrincipalColumns.Add(c);
				});
			}
			this.AddOperation(addForeignKeyOperation);
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x00146150 File Offset: 0x00144350
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void DropForeignKey(string dependentTable, string name, object anonymousArguments = null)
		{
			Check.NotEmpty(dependentTable, "dependentTable");
			Check.NotEmpty(name, "name");
			DropForeignKeyOperation migrationOperation = new DropForeignKeyOperation(anonymousArguments)
			{
				DependentTable = dependentTable,
				Name = name
			};
			this.AddOperation(migrationOperation);
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x00146194 File Offset: 0x00144394
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void DropForeignKey(string dependentTable, string dependentColumn, string principalTable, object anonymousArguments = null)
		{
			Check.NotEmpty(dependentTable, "dependentTable");
			Check.NotEmpty(dependentColumn, "dependentColumn");
			Check.NotEmpty(principalTable, "principalTable");
			this.DropForeignKey(dependentTable, new string[]
			{
				dependentColumn
			}, principalTable, anonymousArguments);
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x001461DC File Offset: 0x001443DC
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "principalColumn")]
		[Obsolete("The principalColumn parameter is no longer required and can be removed.")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void DropForeignKey(string dependentTable, string dependentColumn, string principalTable, string principalColumn, object anonymousArguments = null)
		{
			Check.NotEmpty(dependentTable, "dependentTable");
			Check.NotEmpty(dependentColumn, "dependentColumn");
			Check.NotEmpty(principalTable, "principalTable");
			this.DropForeignKey(dependentTable, new string[]
			{
				dependentColumn
			}, principalTable, anonymousArguments);
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x00146240 File Offset: 0x00144440
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void DropForeignKey(string dependentTable, string[] dependentColumns, string principalTable, object anonymousArguments = null)
		{
			Check.NotEmpty(dependentTable, "dependentTable");
			Check.NotNull<string[]>(dependentColumns, "dependentColumns");
			Check.NotEmpty(principalTable, "principalTable");
			if (!dependentColumns.Any<string>())
			{
				throw new ArgumentException(Strings.CollectionEmpty("dependentColumns", "DropForeignKey"));
			}
			DropForeignKeyOperation dropForeignKeyOperation = new DropForeignKeyOperation(anonymousArguments)
			{
				DependentTable = dependentTable,
				PrincipalTable = principalTable
			};
			dependentColumns.Each(delegate(string c)
			{
				dropForeignKeyOperation.DependentColumns.Add(c);
			});
			this.AddOperation(dropForeignKeyOperation);
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x001462CF File Offset: 0x001444CF
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void DropTable(string name, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.DropTable(name, null, null, anonymousArguments);
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x001462E7 File Offset: 0x001444E7
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void DropTable(string name, IDictionary<string, IDictionary<string, object>> removedColumnAnnotations, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.DropTable(name, null, removedColumnAnnotations, anonymousArguments);
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x001462FF File Offset: 0x001444FF
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void DropTable(string name, IDictionary<string, object> removedAnnotations, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.DropTable(name, removedAnnotations, null, anonymousArguments);
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x00146317 File Offset: 0x00144517
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		protected internal void DropTable(string name, IDictionary<string, object> removedAnnotations, IDictionary<string, IDictionary<string, object>> removedColumnAnnotations, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.AddOperation(new DropTableOperation(name, removedAnnotations, removedColumnAnnotations, anonymousArguments));
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x00146335 File Offset: 0x00144535
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void MoveTable(string name, string newSchema, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.AddOperation(new MoveTableOperation(name, newSchema, anonymousArguments));
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x00146351 File Offset: 0x00144551
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void MoveStoredProcedure(string name, string newSchema, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			this.AddOperation(new MoveProcedureOperation(name, newSchema, anonymousArguments));
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x0014636D File Offset: 0x0014456D
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void RenameTable(string name, string newName, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this.AddOperation(new RenameTableOperation(name, newName, anonymousArguments));
		}

		// Token: 0x0600452F RID: 17711 RVA: 0x00146395 File Offset: 0x00144595
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void RenameStoredProcedure(string name, string newName, object anonymousArguments = null)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this.AddOperation(new RenameProcedureOperation(name, newName, anonymousArguments));
		}

		// Token: 0x06004530 RID: 17712 RVA: 0x001463BD File Offset: 0x001445BD
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void RenameColumn(string table, string name, string newName, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this.AddOperation(new RenameColumnOperation(table, name, newName, anonymousArguments));
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x001463F4 File Offset: 0x001445F4
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void AddColumn(string table, string name, Func<ColumnBuilder, ColumnModel> columnAction, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			Check.NotNull<Func<ColumnBuilder, ColumnModel>>(columnAction, "columnAction");
			ColumnModel columnModel = columnAction(new ColumnBuilder());
			columnModel.Name = name;
			this.AddOperation(new AddColumnOperation(table, columnModel, anonymousArguments));
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x00146447 File Offset: 0x00144647
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void DropColumn(string table, string name, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			this.DropColumn(table, name, null, anonymousArguments);
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x0014646B File Offset: 0x0014466B
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void DropColumn(string table, string name, IDictionary<string, object> removedAnnotations, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			this.AddOperation(new DropColumnOperation(table, name, removedAnnotations, anonymousArguments));
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x00146498 File Offset: 0x00144698
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void AlterColumn(string table, string name, Func<ColumnBuilder, ColumnModel> columnAction, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			Check.NotNull<Func<ColumnBuilder, ColumnModel>>(columnAction, "columnAction");
			ColumnModel columnModel = columnAction(new ColumnBuilder());
			columnModel.Name = name;
			this.AddOperation(new AlterColumnOperation(table, columnModel, false, anonymousArguments));
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x001464EC File Offset: 0x001446EC
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void AddPrimaryKey(string table, string column, string name = null, bool clustered = true, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(column, "column");
			this.AddPrimaryKey(table, new string[]
			{
				column
			}, name, clustered, anonymousArguments);
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x00146544 File Offset: 0x00144744
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void AddPrimaryKey(string table, string[] columns, string name = null, bool clustered = true, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotNull<string[]>(columns, "columns");
			if (!columns.Any<string>())
			{
				throw new ArgumentException(Strings.CollectionEmpty("columns", "AddPrimaryKey"));
			}
			AddPrimaryKeyOperation addPrimaryKeyOperation = new AddPrimaryKeyOperation(anonymousArguments)
			{
				Table = table,
				Name = name,
				IsClustered = clustered
			};
			columns.Each(delegate(string c)
			{
				addPrimaryKeyOperation.Columns.Add(c);
			});
			this.AddOperation(addPrimaryKeyOperation);
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x001465D0 File Offset: 0x001447D0
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void DropPrimaryKey(string table, string name, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			DropPrimaryKeyOperation migrationOperation = new DropPrimaryKeyOperation(anonymousArguments)
			{
				Table = table,
				Name = name
			};
			this.AddOperation(migrationOperation);
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x00146614 File Offset: 0x00144814
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void DropPrimaryKey(string table, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			DropPrimaryKeyOperation migrationOperation = new DropPrimaryKeyOperation(anonymousArguments)
			{
				Table = table
			};
			this.AddOperation(migrationOperation);
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x00146644 File Offset: 0x00144844
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void CreateIndex(string table, string column, bool unique = false, string name = null, bool clustered = false, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(column, "column");
			this.CreateIndex(table, new string[]
			{
				column
			}, unique, name, clustered, anonymousArguments);
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x001466A0 File Offset: 0x001448A0
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void CreateIndex(string table, string[] columns, bool unique = false, string name = null, bool clustered = false, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotNull<string[]>(columns, "columns");
			if (!columns.Any<string>())
			{
				throw new ArgumentException(Strings.CollectionEmpty("columns", "CreateIndex"));
			}
			CreateIndexOperation createIndexOperation = new CreateIndexOperation(anonymousArguments)
			{
				Table = table,
				IsUnique = unique,
				Name = name,
				IsClustered = clustered
			};
			columns.Each(delegate(string c)
			{
				createIndexOperation.Columns.Add(c);
			});
			this.AddOperation(createIndexOperation);
		}

		// Token: 0x0600453B RID: 17723 RVA: 0x00146734 File Offset: 0x00144934
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void DropIndex(string table, string name, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			DropIndexOperation migrationOperation = new DropIndexOperation(anonymousArguments)
			{
				Table = table,
				Name = name
			};
			this.AddOperation(migrationOperation);
		}

		// Token: 0x0600453C RID: 17724 RVA: 0x00146794 File Offset: 0x00144994
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void DropIndex(string table, string[] columns, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotNull<string[]>(columns, "columns");
			if (!columns.Any<string>())
			{
				throw new ArgumentException(Strings.CollectionEmpty("columns", "DropIndex"));
			}
			DropIndexOperation dropIndexOperation = new DropIndexOperation(anonymousArguments)
			{
				Table = table
			};
			columns.Each(delegate(string c)
			{
				dropIndexOperation.Columns.Add(c);
			});
			this.AddOperation(dropIndexOperation);
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x0014680F File Offset: 0x00144A0F
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void RenameIndex(string table, string name, string newName, object anonymousArguments = null)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			Check.NotEmpty(newName, "newName");
			this.AddOperation(new RenameIndexOperation(table, name, newName, anonymousArguments));
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x00146848 File Offset: 0x00144A48
		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void Sql(string sql, bool suppressTransaction = false, object anonymousArguments = null)
		{
			Check.NotEmpty(sql, "sql");
			this.AddOperation(new SqlOperation(sql, anonymousArguments)
			{
				SuppressTransaction = suppressTransaction
			});
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x00146878 File Offset: 0x00144A78
		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void SqlFile(string sqlFile, bool suppressTransaction = false, object anonymousArguments = null)
		{
			Check.NotEmpty(sqlFile, "sqlFile");
			if (!Path.IsPathRooted(sqlFile))
			{
				sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sqlFile);
			}
			this.AddOperation(new SqlOperation(File.ReadAllText(sqlFile), anonymousArguments)
			{
				SuppressTransaction = suppressTransaction
			});
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x001468C8 File Offset: 0x00144AC8
		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected internal void SqlResource(string sqlResource, Assembly resourceAssembly = null, bool suppressTransaction = false, object anonymousArguments = null)
		{
			Check.NotEmpty(sqlResource, "sqlResource");
			resourceAssembly = (resourceAssembly ?? Assembly.GetCallingAssembly());
			if (!resourceAssembly.GetManifestResourceNames().Contains(sqlResource))
			{
				throw new ArgumentException(Strings.UnableToLoadEmbeddedResource(resourceAssembly.FullName, sqlResource));
			}
			using (StreamReader streamReader = new StreamReader(resourceAssembly.GetManifestResourceStream(sqlResource)))
			{
				this.AddOperation(new SqlOperation(streamReader.ReadToEnd(), anonymousArguments)
				{
					SuppressTransaction = suppressTransaction
				});
			}
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x00146954 File Offset: 0x00144B54
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		void IDbMigration.AddOperation(MigrationOperation migrationOperation)
		{
			this.AddOperation(migrationOperation);
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x0014695D File Offset: 0x00144B5D
		internal void AddOperation(MigrationOperation migrationOperation)
		{
			Check.NotNull<MigrationOperation>(migrationOperation, "migrationOperation");
			this._operations.Add(migrationOperation);
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06004543 RID: 17731 RVA: 0x00146977 File Offset: 0x00144B77
		internal IEnumerable<MigrationOperation> Operations
		{
			get
			{
				return this._operations;
			}
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x0014697F File Offset: 0x00144B7F
		internal void Reset()
		{
			this._operations.Clear();
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x00146994 File Offset: 0x00144B94
		internal VersionedModel GetSourceModel()
		{
			return this.GetModel((IMigrationMetadata mm) => mm.Source);
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x001469C1 File Offset: 0x00144BC1
		internal VersionedModel GetTargetModel()
		{
			return this.GetModel((IMigrationMetadata mm) => mm.Target);
		}

		// Token: 0x06004547 RID: 17735 RVA: 0x001469E8 File Offset: 0x00144BE8
		private VersionedModel GetModel(Func<IMigrationMetadata, string> modelAccessor)
		{
			IMigrationMetadata arg = (IMigrationMetadata)this;
			string text = modelAccessor(arg);
			if (string.IsNullOrWhiteSpace(text))
			{
				return null;
			}
			GeneratedCodeAttribute generatedCodeAttribute = this.GetType().GetCustomAttributes(false).SingleOrDefault<GeneratedCodeAttribute>();
			string version = (generatedCodeAttribute != null && !string.IsNullOrWhiteSpace(generatedCodeAttribute.Version)) ? generatedCodeAttribute.Version : typeof(DbMigration).Assembly().GetInformationalVersion();
			return new VersionedModel(new ModelCompressor().Decompress(Convert.FromBase64String(text)), version);
		}

		// Token: 0x06004548 RID: 17736 RVA: 0x00146A63 File Offset: 0x00144C63
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x00146A6B File Offset: 0x00144C6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x00146A74 File Offset: 0x00144C74
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x00146A7C File Offset: 0x00144C7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0600454C RID: 17740 RVA: 0x00146A84 File Offset: 0x00144C84
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected new object MemberwiseClone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x04001967 RID: 6503
		private readonly List<MigrationOperation> _operations = new List<MigrationOperation>();
	}
}
