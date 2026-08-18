using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000122 RID: 290
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Rebinder")]
	public class DbExpressionRebinder : DefaultExpressionVisitor
	{
		// Token: 0x06000923 RID: 2339 RVA: 0x0002EB4F File Offset: 0x0002CD4F
		internal DbExpressionRebinder()
		{
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002EB57 File Offset: 0x0002CD57
		protected DbExpressionRebinder(MetadataWorkspace targetWorkspace)
		{
			this._metadata = targetWorkspace;
			this._perspective = new ModelPerspective(targetWorkspace);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0002EB74 File Offset: 0x0002CD74
		protected override EntitySetBase VisitEntitySet(EntitySetBase entitySet)
		{
			EntityContainer entityContainer;
			if (!this._metadata.TryGetEntityContainer(entitySet.EntityContainer.Name, entitySet.EntityContainer.DataSpace, out entityContainer))
			{
				throw new ArgumentException(Strings.Cqt_Copier_EntityContainerNotFound(entitySet.EntityContainer.Name));
			}
			EntitySetBase entitySetBase = null;
			if (entityContainer.BaseEntitySets.TryGetValue(entitySet.Name, false, out entitySetBase) && entitySetBase != null && entitySet.BuiltInTypeKind == entitySetBase.BuiltInTypeKind)
			{
				return entitySetBase;
			}
			throw new ArgumentException(Strings.Cqt_Copier_EntitySetNotFound(entitySet.EntityContainer.Name, entitySet.Name));
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0002EC04 File Offset: 0x0002CE04
		protected override EdmFunction VisitFunction(EdmFunction functionMetadata)
		{
			List<TypeUsage> list = new List<TypeUsage>(functionMetadata.Parameters.Count);
			foreach (FunctionParameter functionParameter in functionMetadata.Parameters)
			{
				TypeUsage item = this.VisitTypeUsage(functionParameter.TypeUsage);
				list.Add(item);
			}
			IList<EdmFunction> functionsMetadata;
			if (DataSpace.SSpace == functionMetadata.DataSpace)
			{
				EdmFunction edmFunction = null;
				if (this._metadata.TryGetFunction(functionMetadata.Name, functionMetadata.NamespaceName, list.ToArray(), false, functionMetadata.DataSpace, out edmFunction) && edmFunction != null)
				{
					return edmFunction;
				}
			}
			else if (this._perspective.TryGetFunctionByName(functionMetadata.NamespaceName, functionMetadata.Name, false, out functionsMetadata))
			{
				bool flag;
				EdmFunction edmFunction2 = FunctionOverloadResolver.ResolveFunctionOverloads(functionsMetadata, list, false, out flag);
				if (!flag && edmFunction2 != null)
				{
					return edmFunction2;
				}
			}
			throw new ArgumentException(Strings.Cqt_Copier_FunctionNotFound(TypeHelpers.GetFullName(functionMetadata.NamespaceName, functionMetadata.Name)));
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0002ED28 File Offset: 0x0002CF28
		protected override EdmType VisitType(EdmType type)
		{
			EdmType edmType = type;
			if (BuiltInTypeKind.RefType == type.BuiltInTypeKind)
			{
				RefType refType = (RefType)type;
				EntityType entityType = (EntityType)this.VisitType(refType.ElementType);
				if (!object.ReferenceEquals(refType.ElementType, entityType))
				{
					edmType = new RefType(entityType);
				}
			}
			else if (BuiltInTypeKind.CollectionType == type.BuiltInTypeKind)
			{
				CollectionType collectionType = (CollectionType)type;
				TypeUsage typeUsage = this.VisitTypeUsage(collectionType.TypeUsage);
				if (!object.ReferenceEquals(collectionType.TypeUsage, typeUsage))
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
					if (!object.ReferenceEquals(edmProperty.TypeUsage, typeUsage2))
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
				throw new ArgumentException(Strings.Cqt_Copier_TypeNotFound(TypeHelpers.GetFullName(type.NamespaceName, type.Name)));
			}
			return edmType;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0002EEE4 File Offset: 0x0002D0E4
		protected override TypeUsage VisitTypeUsage(TypeUsage type)
		{
			EdmType edmType = this.VisitType(type.EdmType);
			if (object.ReferenceEquals(edmType, type.EdmType))
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

		// Token: 0x06000929 RID: 2345 RVA: 0x0002EF70 File Offset: 0x0002D170
		private static bool TryGetMember<TMember>(DbExpression instance, string memberName, out TMember member) where TMember : EdmMember
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

		// Token: 0x0600092A RID: 2346 RVA: 0x0002EFD0 File Offset: 0x0002D1D0
		public override DbExpression Visit(DbPropertyExpression expression)
		{
			Check.NotNull<DbPropertyExpression>(expression, "expression");
			DbExpression result = expression;
			DbExpression dbExpression = this.VisitExpression(expression.Instance);
			if (!object.ReferenceEquals(expression.Instance, dbExpression))
			{
				if (Helper.IsRelationshipEndMember(expression.Property))
				{
					RelationshipEndMember relationshipEnd;
					if (!DbExpressionRebinder.TryGetMember<RelationshipEndMember>(dbExpression, expression.Property.Name, out relationshipEnd))
					{
						EdmType edmType = dbExpression.ResultType.EdmType;
						throw new ArgumentException(Strings.Cqt_Copier_EndNotFound(expression.Property.Name, TypeHelpers.GetFullName(edmType.NamespaceName, edmType.Name)));
					}
					result = dbExpression.Property(relationshipEnd);
				}
				else if (Helper.IsNavigationProperty(expression.Property))
				{
					NavigationProperty navigationProperty;
					if (!DbExpressionRebinder.TryGetMember<NavigationProperty>(dbExpression, expression.Property.Name, out navigationProperty))
					{
						EdmType edmType2 = dbExpression.ResultType.EdmType;
						throw new ArgumentException(Strings.Cqt_Copier_NavPropertyNotFound(expression.Property.Name, TypeHelpers.GetFullName(edmType2.NamespaceName, edmType2.Name)));
					}
					result = dbExpression.Property(navigationProperty);
				}
				else
				{
					EdmProperty propertyMetadata;
					if (!DbExpressionRebinder.TryGetMember<EdmProperty>(dbExpression, expression.Property.Name, out propertyMetadata))
					{
						EdmType edmType3 = dbExpression.ResultType.EdmType;
						throw new ArgumentException(Strings.Cqt_Copier_PropertyNotFound(expression.Property.Name, TypeHelpers.GetFullName(edmType3.NamespaceName, edmType3.Name)));
					}
					result = dbExpression.Property(propertyMetadata);
				}
			}
			return result;
		}

		// Token: 0x0400028B RID: 651
		private readonly MetadataWorkspace _metadata;

		// Token: 0x0400028C RID: 652
		private readonly Perspective _perspective;
	}
}
