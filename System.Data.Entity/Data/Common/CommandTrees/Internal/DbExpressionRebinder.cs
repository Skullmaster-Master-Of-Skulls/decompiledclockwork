using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.EntitySql;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Common.CommandTrees.Internal
{
	// Token: 0x02000434 RID: 1076
	internal class DbExpressionRebinder : DefaultExpressionVisitor
	{
		// Token: 0x060039AD RID: 14765 RVA: 0x000DBE27 File Offset: 0x000DA027
		protected DbExpressionRebinder(MetadataWorkspace targetWorkspace)
		{
			this._metadata = targetWorkspace;
			this._perspective = new ModelPerspective(targetWorkspace);
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x000DBE44 File Offset: 0x000DA044
		internal static DbExpression BindToWorkspace(DbExpression expression, MetadataWorkspace targetWorkspace)
		{
			DbExpressionRebinder dbExpressionRebinder = new DbExpressionRebinder(targetWorkspace);
			return dbExpressionRebinder.VisitExpression(expression);
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x000DBE60 File Offset: 0x000DA060
		protected override EntitySetBase VisitEntitySet(EntitySetBase entitySet)
		{
			EntityContainer entityContainer;
			if (!this._metadata.TryGetEntityContainer(entitySet.EntityContainer.Name, entitySet.EntityContainer.DataSpace, out entityContainer))
			{
				throw EntityUtil.Argument(Strings.Cqt_Copier_EntityContainerNotFound(entitySet.EntityContainer.Name));
			}
			EntitySetBase entitySetBase = null;
			if (entityContainer.BaseEntitySets.TryGetValue(entitySet.Name, false, out entitySetBase) && entitySetBase != null && entitySet.BuiltInTypeKind == entitySetBase.BuiltInTypeKind)
			{
				return entitySetBase;
			}
			throw EntityUtil.Argument(Strings.Cqt_Copier_EntitySetNotFound(entitySet.EntityContainer.Name, entitySet.Name));
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x000DBEF0 File Offset: 0x000DA0F0
		protected override EdmFunction VisitFunction(EdmFunction function)
		{
			List<TypeUsage> list = new List<TypeUsage>(function.Parameters.Count);
			foreach (FunctionParameter functionParameter in function.Parameters)
			{
				TypeUsage item = this.VisitTypeUsage(functionParameter.TypeUsage);
				list.Add(item);
			}
			IList<EdmFunction> functionsMetadata;
			if (DataSpace.SSpace == function.DataSpace)
			{
				EdmFunction edmFunction = null;
				if (this._metadata.TryGetFunction(function.Name, function.NamespaceName, list.ToArray(), false, function.DataSpace, out edmFunction) && edmFunction != null)
				{
					return edmFunction;
				}
			}
			else if (this._perspective.TryGetFunctionByName(function.NamespaceName, function.Name, false, out functionsMetadata))
			{
				bool flag;
				EdmFunction edmFunction2 = FunctionOverloadResolver.ResolveFunctionOverloads(functionsMetadata, list, false, out flag);
				if (!flag && edmFunction2 != null)
				{
					return edmFunction2;
				}
			}
			throw EntityUtil.Argument(Strings.Cqt_Copier_FunctionNotFound(TypeHelpers.GetFullName(function)));
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x000DBFE0 File Offset: 0x000DA1E0
		protected override EdmType VisitType(EdmType type)
		{
			EdmType edmType = type;
			if (BuiltInTypeKind.RefType == type.BuiltInTypeKind)
			{
				RefType refType = (RefType)type;
				EntityType entityType = (EntityType)this.VisitType(refType.ElementType);
				if (refType.ElementType != entityType)
				{
					edmType = new RefType(entityType);
				}
			}
			else if (BuiltInTypeKind.CollectionType == type.BuiltInTypeKind)
			{
				CollectionType collectionType = (CollectionType)type;
				TypeUsage typeUsage = this.VisitTypeUsage(collectionType.TypeUsage);
				if (collectionType.TypeUsage != typeUsage)
				{
					edmType = new CollectionType(typeUsage);
				}
			}
			else if (BuiltInTypeKind.RowType == type.BuiltInTypeKind)
			{
				RowType rowType = (RowType)type;
				List<KeyValuePair<string, TypeUsage>> list = null;
				for (int i = 0; i < rowType.Properties.Count; i++)
				{
					EdmProperty edmProperty = rowType.Properties[i];
					TypeUsage typeUsage2 = this.VisitTypeUsage(edmProperty.TypeUsage);
					if (edmProperty.TypeUsage != typeUsage2)
					{
						if (list == null)
						{
							list = new List<KeyValuePair<string, TypeUsage>>(from prop in rowType.Properties
							select new KeyValuePair<string, TypeUsage>(prop.Name, prop.TypeUsage));
						}
						list[i] = new KeyValuePair<string, TypeUsage>(edmProperty.Name, typeUsage2);
					}
				}
				if (list != null)
				{
					IEnumerable<EdmProperty> properties = from propInfo in list
					select new EdmProperty(propInfo.Key, propInfo.Value);
					edmType = new RowType(properties, rowType.InitializerMetadata);
				}
			}
			else if (!this._metadata.TryGetType(type.Name, type.NamespaceName, type.DataSpace, out edmType) || edmType == null)
			{
				throw EntityUtil.Argument(Strings.Cqt_Copier_TypeNotFound(TypeHelpers.GetFullName(type)));
			}
			return edmType;
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x000DC184 File Offset: 0x000DA384
		protected override TypeUsage VisitTypeUsage(TypeUsage type)
		{
			EdmType edmType = this.VisitType(type.EdmType);
			if (edmType == type.EdmType)
			{
				return type;
			}
			Facet[] array = new Facet[type.Facets.Count];
			int num = 0;
			foreach (Facet facet in type.Facets)
			{
				array[num] = facet;
				num++;
			}
			return TypeUsage.Create(edmType, array);
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x000DC20C File Offset: 0x000DA40C
		private bool TryGetMember<TMember>(DbExpression instance, string memberName, out TMember member) where TMember : EdmMember
		{
			member = default(TMember);
			StructuralType structuralType = instance.ResultType.EdmType as StructuralType;
			if (structuralType != null)
			{
				EdmMember edmMember = null;
				if (structuralType.Members.TryGetValue(memberName, false, out edmMember))
				{
					member = (edmMember as TMember);
				}
			}
			return member != null;
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x000DC268 File Offset: 0x000DA468
		public override DbExpression Visit(DbPropertyExpression expression)
		{
			EntityUtil.CheckArgumentNull<DbPropertyExpression>(expression, "expression");
			DbExpression result = expression;
			DbExpression dbExpression = this.VisitExpression(expression.Instance);
			if (expression.Instance != dbExpression)
			{
				if (Helper.IsRelationshipEndMember(expression.Property))
				{
					RelationshipEndMember relationshipEnd;
					if (!this.TryGetMember<RelationshipEndMember>(dbExpression, expression.Property.Name, out relationshipEnd))
					{
						throw EntityUtil.Argument(Strings.Cqt_Copier_EndNotFound(expression.Property.Name, TypeHelpers.GetFullName(dbExpression.ResultType.EdmType)));
					}
					result = dbExpression.Property(relationshipEnd);
				}
				else if (Helper.IsNavigationProperty(expression.Property))
				{
					NavigationProperty navigationProperty;
					if (!this.TryGetMember<NavigationProperty>(dbExpression, expression.Property.Name, out navigationProperty))
					{
						throw EntityUtil.Argument(Strings.Cqt_Copier_NavPropertyNotFound(expression.Property.Name, TypeHelpers.GetFullName(dbExpression.ResultType.EdmType)));
					}
					result = dbExpression.Property(navigationProperty);
				}
				else
				{
					EdmProperty propertyMetadata;
					if (!this.TryGetMember<EdmProperty>(dbExpression, expression.Property.Name, out propertyMetadata))
					{
						throw EntityUtil.Argument(Strings.Cqt_Copier_PropertyNotFound(expression.Property.Name, TypeHelpers.GetFullName(dbExpression.ResultType.EdmType)));
					}
					result = dbExpression.Property(propertyMetadata);
				}
			}
			return result;
		}

		// Token: 0x04001867 RID: 6247
		private readonly MetadataWorkspace _metadata;

		// Token: 0x04001868 RID: 6248
		private readonly Perspective _perspective;
	}
}
