using System;
using System.ComponentModel;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Migrations.Builders
{
	// Token: 0x020006CB RID: 1739
	public class TableBuilder<TColumns>
	{
		// Token: 0x0600450B RID: 17675 RVA: 0x0014587D File Offset: 0x00143A7D
		public TableBuilder(CreateTableOperation createTableOperation, DbMigration migration)
		{
			Check.NotNull<CreateTableOperation>(createTableOperation, "createTableOperation");
			this._createTableOperation = createTableOperation;
			this._migration = migration;
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x00145918 File Offset: 0x00143B18
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public TableBuilder<TColumns> PrimaryKey(Expression<Func<TColumns, object>> keyExpression, string name = null, bool clustered = true, object anonymousArguments = null)
		{
			Check.NotNull<Expression<Func<TColumns, object>>>(keyExpression, "keyExpression");
			AddPrimaryKeyOperation addPrimaryKeyOperation = new AddPrimaryKeyOperation(anonymousArguments)
			{
				Name = name,
				IsClustered = clustered
			};
			(from p in keyExpression.GetSimplePropertyAccessList()
			select this._createTableOperation.Columns.Single((ColumnModel c) => c.ApiPropertyInfo == p.Single<PropertyInfo>())).Each(delegate(ColumnModel c)
			{
				addPrimaryKeyOperation.Columns.Add(c.Name);
			});
			this._createTableOperation.PrimaryKey = addPrimaryKeyOperation;
			return this;
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x00145A10 File Offset: 0x00143C10
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public TableBuilder<TColumns> Index(Expression<Func<TColumns, object>> indexExpression, string name = null, bool unique = false, bool clustered = false, object anonymousArguments = null)
		{
			Check.NotNull<Expression<Func<TColumns, object>>>(indexExpression, "indexExpression");
			CreateIndexOperation createIndexOperation = new CreateIndexOperation(anonymousArguments)
			{
				Name = name,
				Table = this._createTableOperation.Name,
				IsUnique = unique,
				IsClustered = clustered
			};
			(from p in indexExpression.GetSimplePropertyAccessList()
			select this._createTableOperation.Columns.Single((ColumnModel c) => c.ApiPropertyInfo == p.Single<PropertyInfo>())).Each(delegate(ColumnModel c)
			{
				createIndexOperation.Columns.Add(c.Name);
			});
			this._migration.AddOperation(createIndexOperation);
			return this;
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x00145B20 File Offset: 0x00143D20
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public TableBuilder<TColumns> ForeignKey(string principalTable, Expression<Func<TColumns, object>> dependentKeyExpression, bool cascadeDelete = false, string name = null, object anonymousArguments = null)
		{
			Check.NotEmpty(principalTable, "principalTable");
			Check.NotNull<Expression<Func<TColumns, object>>>(dependentKeyExpression, "dependentKeyExpression");
			AddForeignKeyOperation addForeignKeyOperation = new AddForeignKeyOperation(anonymousArguments)
			{
				Name = name,
				PrincipalTable = principalTable,
				DependentTable = this._createTableOperation.Name,
				CascadeDelete = cascadeDelete
			};
			(from p in dependentKeyExpression.GetSimplePropertyAccessList()
			select this._createTableOperation.Columns.Single((ColumnModel c) => c.ApiPropertyInfo == p.Single<PropertyInfo>())).Each(delegate(ColumnModel c)
			{
				addForeignKeyOperation.DependentColumns.Add(c.Name);
			});
			this._migration.AddOperation(addForeignKeyOperation);
			return this;
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x00145BC2 File Offset: 0x00143DC2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x00145BCA File Offset: 0x00143DCA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x00145BD3 File Offset: 0x00143DD3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x00145BDB File Offset: 0x00143DDB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x00145BE3 File Offset: 0x00143DE3
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected new object MemberwiseClone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x04001965 RID: 6501
		private readonly CreateTableOperation _createTableOperation;

		// Token: 0x04001966 RID: 6502
		private readonly DbMigration _migration;
	}
}
