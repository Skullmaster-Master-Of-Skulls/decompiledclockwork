using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x0200027F RID: 639
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal class DynamicToFunctionModificationCommandConverter : DefaultExpressionVisitor
	{
		// Token: 0x0600166C RID: 5740 RVA: 0x0006C61F File Offset: 0x0006A81F
		public DynamicToFunctionModificationCommandConverter(EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping, EntityContainerMapping entityContainerMapping)
		{
			this._entityTypeModificationFunctionMapping = entityTypeModificationFunctionMapping;
			this._entityContainerMapping = entityContainerMapping;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0006C635 File Offset: 0x0006A835
		public DynamicToFunctionModificationCommandConverter(AssociationSetModificationFunctionMapping associationSetModificationFunctionMapping, EntityContainerMapping entityContainerMapping)
		{
			this._associationSetModificationFunctionMapping = associationSetModificationFunctionMapping;
			this._entityContainerMapping = entityContainerMapping;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0006C6B7 File Offset: 0x0006A8B7
		public IEnumerable<TCommandTree> Convert<TCommandTree>(IEnumerable<TCommandTree> modificationCommandTrees) where TCommandTree : DbModificationCommandTree
		{
			this._currentFunctionMapping = null;
			this._currentProperty = null;
			this._storeGeneratedKeys = null;
			this._nextStoreGeneratedKey = 0;
			return modificationCommandTrees.Select(delegate(TCommandTree modificationCommandTree)
			{
				if (DynamicToFunctionModificationCommandConverter.<Convert>o__SiteContainer0<TCommandTree>.<>p__Site1 == null)
				{
					DynamicToFunctionModificationCommandConverter.<Convert>o__SiteContainer0<TCommandTree>.<>p__Site1 = CallSite<Func<CallSite, DynamicToFunctionModificationCommandConverter, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "ConvertInternal", null, typeof(DynamicToFunctionModificationCommandConverter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				return DynamicToFunctionModificationCommandConverter.<Convert>o__SiteContainer0<TCommandTree>.<>p__Site1.Target(DynamicToFunctionModificationCommandConverter.<Convert>o__SiteContainer0<TCommandTree>.<>p__Site1, this, modificationCommandTree);
			}).Cast<TCommandTree>();
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x0006C6F4 File Offset: 0x0006A8F4
		private DbModificationCommandTree ConvertInternal(DbInsertCommandTree commandTree)
		{
			if (this._currentFunctionMapping == null)
			{
				this._currentFunctionMapping = ((this._entityTypeModificationFunctionMapping != null) ? this._entityTypeModificationFunctionMapping.InsertFunctionMapping : this._associationSetModificationFunctionMapping.InsertFunctionMapping);
				EntityTypeBase elementType = ((DbScanExpression)commandTree.Target.Expression).Target.ElementType;
				this._storeGeneratedKeys = (from p in elementType.KeyProperties
				where p.IsStoreGeneratedIdentity
				select p).ToList<EdmProperty>();
			}
			this._nextStoreGeneratedKey = 0;
			return new DbInsertCommandTree(commandTree.MetadataWorkspace, commandTree.DataSpace, commandTree.Target, this.VisitSetClauses(commandTree.SetClauses), (commandTree.Returning != null) ? commandTree.Returning.Accept<DbExpression>(this) : null);
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0006C7C0 File Offset: 0x0006A9C0
		private DbModificationCommandTree ConvertInternal(DbUpdateCommandTree commandTree)
		{
			this._currentFunctionMapping = this._entityTypeModificationFunctionMapping.UpdateFunctionMapping;
			this._useOriginalValues = true;
			DbExpression predicate = commandTree.Predicate.Accept<DbExpression>(this);
			this._useOriginalValues = false;
			return new DbUpdateCommandTree(commandTree.MetadataWorkspace, commandTree.DataSpace, commandTree.Target, predicate, this.VisitSetClauses(commandTree.SetClauses), (commandTree.Returning != null) ? commandTree.Returning.Accept<DbExpression>(this) : null);
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0006C834 File Offset: 0x0006AA34
		private DbModificationCommandTree ConvertInternal(DbDeleteCommandTree commandTree)
		{
			this._currentFunctionMapping = ((this._entityTypeModificationFunctionMapping != null) ? this._entityTypeModificationFunctionMapping.DeleteFunctionMapping : this._associationSetModificationFunctionMapping.DeleteFunctionMapping);
			return new DbDeleteCommandTree(commandTree.MetadataWorkspace, commandTree.DataSpace, commandTree.Target, commandTree.Predicate.Accept<DbExpression>(this));
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0006C8A9 File Offset: 0x0006AAA9
		private ReadOnlyCollection<DbModificationClause> VisitSetClauses(IList<DbModificationClause> setClauses)
		{
			return new ReadOnlyCollection<DbModificationClause>((from DbSetClause s in setClauses
			select new DbSetClause(s.Property.Accept<DbExpression>(this), s.Value.Accept<DbExpression>(this))).Cast<DbModificationClause>().ToList<DbModificationClause>());
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0006C8D4 File Offset: 0x0006AAD4
		public override DbExpression Visit(DbComparisonExpression expression)
		{
			DbComparisonExpression dbComparisonExpression = (DbComparisonExpression)base.Visit(expression);
			DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)dbComparisonExpression.Left;
			EdmProperty edmProperty = (EdmProperty)dbPropertyExpression.Property;
			if (edmProperty.Nullable)
			{
				DbAndExpression right = dbPropertyExpression.IsNull().And(dbComparisonExpression.Right.IsNull());
				return dbComparisonExpression.Or(right);
			}
			return dbComparisonExpression;
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0006C92E File Offset: 0x0006AB2E
		public override DbExpression Visit(DbPropertyExpression expression)
		{
			this._currentProperty = (EdmProperty)expression.Property;
			return base.Visit(expression);
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0006C948 File Offset: 0x0006AB48
		public override DbExpression Visit(DbConstantExpression expression)
		{
			if (this._currentProperty != null)
			{
				Tuple<FunctionParameter, bool> parameter = this.GetParameter(this._currentProperty, this._useOriginalValues);
				if (parameter != null)
				{
					return new DbParameterReferenceExpression(parameter.Item1.TypeUsage, parameter.Item1.Name);
				}
			}
			return base.Visit(expression);
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0006C998 File Offset: 0x0006AB98
		public override DbExpression Visit(DbAndExpression expression)
		{
			DbExpression dbExpression = this.VisitExpression(expression.Left);
			DbExpression dbExpression2 = this.VisitExpression(expression.Right);
			if (dbExpression != null && dbExpression2 != null)
			{
				return dbExpression.And(dbExpression2);
			}
			return dbExpression ?? dbExpression2;
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0006C9D4 File Offset: 0x0006ABD4
		public override DbExpression Visit(DbIsNullExpression expression)
		{
			DbPropertyExpression dbPropertyExpression = expression.Argument as DbPropertyExpression;
			if (dbPropertyExpression != null)
			{
				Tuple<FunctionParameter, bool> parameter = this.GetParameter((EdmProperty)dbPropertyExpression.Property, true);
				if (parameter != null)
				{
					if (parameter.Item2)
					{
						return null;
					}
					DbParameterReferenceExpression dbParameterReferenceExpression = new DbParameterReferenceExpression(parameter.Item1.TypeUsage, parameter.Item1.Name);
					DbComparisonExpression left = dbPropertyExpression.Equal(dbParameterReferenceExpression);
					DbAndExpression right = dbPropertyExpression.IsNull().And(dbParameterReferenceExpression.IsNull());
					return left.Or(right);
				}
			}
			return base.Visit(expression);
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0006CA58 File Offset: 0x0006AC58
		public override DbExpression Visit(DbNullExpression expression)
		{
			if (this._currentProperty != null)
			{
				Tuple<FunctionParameter, bool> parameter = this.GetParameter(this._currentProperty, false);
				if (parameter != null)
				{
					return new DbParameterReferenceExpression(parameter.Item1.TypeUsage, parameter.Item1.Name);
				}
			}
			return base.Visit(expression);
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0006D124 File Offset: 0x0006B324
		public override DbExpression Visit(DbNewInstanceExpression expression)
		{
			List<KeyValuePair<string, DbExpression>> columnValues = (from DbPropertyExpression propertyExpression in expression.Arguments
			let resultBinding = this._currentFunctionMapping.ResultBindings.Single((ModificationFunctionResultBinding rb) => (from esm in this._entityContainerMapping.EntitySetMappings
			from etm in esm.EntityTypeMappings
			from mf in etm.MappingFragments
			from pm in mf.PropertyMappings.OfType<ScalarPropertyMapping>()
			where pm.Column.EdmEquals(propertyExpression.Property) && pm.Column.DeclaringType.EdmEquals(propertyExpression.Property.DeclaringType)
			select pm.Property).Contains(rb.Property))
			select new KeyValuePair<string, DbExpression>(resultBinding.ColumnName, propertyExpression)).ToList<KeyValuePair<string, DbExpression>>();
			return DbExpressionBuilder.NewRow(columnValues);
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0006DA48 File Offset: 0x0006BC48
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		private Tuple<FunctionParameter, bool> GetParameter(EdmProperty column, bool originalValue = false)
		{
			List<ColumnMappingBuilder> columnMappings = (from esm in this._entityContainerMapping.EntitySetMappings
			from etm in esm.EntityTypeMappings
			from mf in etm.MappingFragments
			from cm in mf.FlattenedProperties
			where cm.ColumnProperty.EdmEquals(column) && cm.ColumnProperty.DeclaringType.EdmEquals(column.DeclaringType)
			select cm).ToList<ColumnMappingBuilder>();
			List<ModificationFunctionParameterBinding> list = (from pb in this._currentFunctionMapping.ParameterBindings
			where columnMappings.Any((ColumnMappingBuilder cm) => pb.MemberPath.Members.Reverse<EdmMember>().SequenceEqual(cm.PropertyPath))
			select pb).ToList<ModificationFunctionParameterBinding>();
			if (!list.Any<ModificationFunctionParameterBinding>())
			{
				List<EdmMember[]> iaColumnMappings = (from asm in this._entityContainerMapping.AssociationSetMappings
				from tm in asm.TypeMappings
				from mf in tm.MappingFragments
				from epm in mf.PropertyMappings.OfType<EndPropertyMapping>()
				from pm in epm.PropertyMappings
				where pm.Column.EdmEquals(column) && pm.Column.DeclaringType.EdmEquals(column.DeclaringType)
				select new EdmMember[]
				{
					pm.Property,
					epm.AssociationEnd
				}).ToList<EdmMember[]>();
				list = (from pb in this._currentFunctionMapping.ParameterBindings
				where iaColumnMappings.Any((EdmMember[] epm) => pb.MemberPath.Members.SequenceEqual(epm))
				select pb).ToList<ModificationFunctionParameterBinding>();
			}
			if (list.Count == 0 && column.IsPrimaryKeyColumn)
			{
				return Tuple.Create<FunctionParameter, bool>(new FunctionParameter(this._storeGeneratedKeys[this._nextStoreGeneratedKey++].Name, column.TypeUsage, ParameterMode.In), true);
			}
			if (list.Count == 1)
			{
				return Tuple.Create<FunctionParameter, bool>(list[0].Parameter, list[0].IsCurrent);
			}
			if (list.Count == 0)
			{
				return null;
			}
			ModificationFunctionParameterBinding modificationFunctionParameterBinding;
			if (!originalValue)
			{
				modificationFunctionParameterBinding = list.Single((ModificationFunctionParameterBinding pb) => pb.IsCurrent);
			}
			else
			{
				modificationFunctionParameterBinding = list.Single((ModificationFunctionParameterBinding pb) => !pb.IsCurrent);
			}
			ModificationFunctionParameterBinding modificationFunctionParameterBinding2 = modificationFunctionParameterBinding;
			return Tuple.Create<FunctionParameter, bool>(modificationFunctionParameterBinding2.Parameter, modificationFunctionParameterBinding2.IsCurrent);
		}

		// Token: 0x040007F0 RID: 2032
		private readonly EntityTypeModificationFunctionMapping _entityTypeModificationFunctionMapping;

		// Token: 0x040007F1 RID: 2033
		private readonly AssociationSetModificationFunctionMapping _associationSetModificationFunctionMapping;

		// Token: 0x040007F2 RID: 2034
		private readonly EntityContainerMapping _entityContainerMapping;

		// Token: 0x040007F3 RID: 2035
		private ModificationFunctionMapping _currentFunctionMapping;

		// Token: 0x040007F4 RID: 2036
		private EdmProperty _currentProperty;

		// Token: 0x040007F5 RID: 2037
		private List<EdmProperty> _storeGeneratedKeys;

		// Token: 0x040007F6 RID: 2038
		private int _nextStoreGeneratedKey;

		// Token: 0x040007F7 RID: 2039
		private bool _useOriginalValues;

		// Token: 0x0200090E RID: 2318
		[CompilerGenerated]
		private static class <Convert>o__SiteContainer0<TCommandTree>
		{
			// Token: 0x0400278D RID: 10125
			public static CallSite<Func<CallSite, DynamicToFunctionModificationCommandConverter, object, object>> <>p__Site1;
		}
	}
}
