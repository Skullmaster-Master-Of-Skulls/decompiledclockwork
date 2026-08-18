using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020004A6 RID: 1190
	internal static class ViewValidator
	{
		// Token: 0x06002BD2 RID: 11218 RVA: 0x000D5C8C File Offset: 0x000D3E8C
		internal static IEnumerable<EdmSchemaError> ValidateQueryView(DbQueryCommandTree view, EntitySetBaseMapping setMapping, EntityTypeBase elementType, bool includeSubtypes)
		{
			ViewValidator.ViewExpressionValidator viewExpressionValidator = new ViewValidator.ViewExpressionValidator(setMapping, elementType, includeSubtypes);
			viewExpressionValidator.VisitExpression(view.Query);
			if (viewExpressionValidator.Errors.Count<EdmSchemaError>() == 0 && setMapping.Set.BuiltInTypeKind == BuiltInTypeKind.AssociationSet)
			{
				ViewValidator.AssociationSetViewValidator associationSetViewValidator = new ViewValidator.AssociationSetViewValidator(setMapping);
				associationSetViewValidator.VisitExpression(view.Query);
				return associationSetViewValidator.Errors;
			}
			return viewExpressionValidator.Errors;
		}

		// Token: 0x020004A7 RID: 1191
		private sealed class ViewExpressionValidator : BasicExpressionVisitor
		{
			// Token: 0x1700060A RID: 1546
			// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x000D5CEA File Offset: 0x000D3EEA
			private EdmItemCollection EdmItemCollection
			{
				get
				{
					return this._setMapping.EntityContainerMapping.StorageMappingItemCollection.EdmItemCollection;
				}
			}

			// Token: 0x1700060B RID: 1547
			// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x000D5D01 File Offset: 0x000D3F01
			private StoreItemCollection StoreItemCollection
			{
				get
				{
					return this._setMapping.EntityContainerMapping.StorageMappingItemCollection.StoreItemCollection;
				}
			}

			// Token: 0x06002BD5 RID: 11221 RVA: 0x000D5D18 File Offset: 0x000D3F18
			internal ViewExpressionValidator(EntitySetBaseMapping setMapping, EntityTypeBase elementType, bool includeSubtypes)
			{
				this._setMapping = setMapping;
				this._elementType = elementType;
				this._includeSubtypes = includeSubtypes;
				this._errors = new List<EdmSchemaError>();
			}

			// Token: 0x1700060C RID: 1548
			// (get) Token: 0x06002BD6 RID: 11222 RVA: 0x000D5D40 File Offset: 0x000D3F40
			internal IEnumerable<EdmSchemaError> Errors
			{
				get
				{
					return this._errors;
				}
			}

			// Token: 0x06002BD7 RID: 11223 RVA: 0x000D5D48 File Offset: 0x000D3F48
			public override void VisitExpression(DbExpression expression)
			{
				Check.NotNull<DbExpression>(expression, "expression");
				this.ValidateExpressionKind(expression.ExpressionKind);
				base.VisitExpression(expression);
			}

			// Token: 0x06002BD8 RID: 11224 RVA: 0x000D5D6C File Offset: 0x000D3F6C
			private void ValidateExpressionKind(DbExpressionKind expressionKind)
			{
				switch (expressionKind)
				{
				case DbExpressionKind.And:
				case DbExpressionKind.Case:
				case DbExpressionKind.Cast:
				case DbExpressionKind.Constant:
				case DbExpressionKind.EntityRef:
				case DbExpressionKind.Equals:
				case DbExpressionKind.Filter:
				case DbExpressionKind.FullOuterJoin:
				case DbExpressionKind.Function:
				case DbExpressionKind.GreaterThan:
				case DbExpressionKind.GreaterThanOrEquals:
				case DbExpressionKind.InnerJoin:
				case DbExpressionKind.IsNull:
				case DbExpressionKind.LeftOuterJoin:
				case DbExpressionKind.LessThan:
				case DbExpressionKind.LessThanOrEquals:
				case DbExpressionKind.NewInstance:
				case DbExpressionKind.Not:
				case DbExpressionKind.NotEquals:
				case DbExpressionKind.Null:
				case DbExpressionKind.Or:
				case DbExpressionKind.Project:
				case DbExpressionKind.Property:
				case DbExpressionKind.Ref:
					return;
				case DbExpressionKind.Any:
				case DbExpressionKind.CrossApply:
				case DbExpressionKind.CrossJoin:
				case DbExpressionKind.Deref:
				case DbExpressionKind.Distinct:
				case DbExpressionKind.Divide:
				case DbExpressionKind.Element:
				case DbExpressionKind.Except:
				case DbExpressionKind.GroupBy:
				case DbExpressionKind.Intersect:
				case DbExpressionKind.IsEmpty:
				case DbExpressionKind.IsOf:
				case DbExpressionKind.IsOfOnly:
				case DbExpressionKind.Like:
				case DbExpressionKind.Limit:
				case DbExpressionKind.Minus:
				case DbExpressionKind.Modulo:
				case DbExpressionKind.Multiply:
				case DbExpressionKind.OfType:
				case DbExpressionKind.OfTypeOnly:
				case DbExpressionKind.OuterApply:
				case DbExpressionKind.ParameterReference:
				case DbExpressionKind.Plus:
					break;
				default:
					if (expressionKind == DbExpressionKind.Scan)
					{
						return;
					}
					switch (expressionKind)
					{
					case DbExpressionKind.UnionAll:
					case DbExpressionKind.VariableReference:
						return;
					}
					break;
				}
				string p = this._includeSubtypes ? ("IsTypeOf(" + this._elementType + ")") : this._elementType.ToString();
				this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedExpressionKind_QueryView(this._setMapping.Set.Name, p, expressionKind), 2071, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
			}

			// Token: 0x06002BD9 RID: 11225 RVA: 0x000D5EDC File Offset: 0x000D40DC
			public override void Visit(DbPropertyExpression expression)
			{
				Check.NotNull<DbPropertyExpression>(expression, "expression");
				base.Visit(expression);
				if (expression.Property.BuiltInTypeKind != BuiltInTypeKind.EdmProperty)
				{
					this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedPropertyKind_QueryView(this._setMapping.Set.Name, expression.Property.Name, expression.Property.BuiltInTypeKind), 2073, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
				}
			}

			// Token: 0x06002BDA RID: 11226 RVA: 0x000D5F78 File Offset: 0x000D4178
			public override void Visit(DbNewInstanceExpression expression)
			{
				Check.NotNull<DbNewInstanceExpression>(expression, "expression");
				base.Visit(expression);
				EdmType edmType = expression.ResultType.EdmType;
				if (edmType.BuiltInTypeKind != BuiltInTypeKind.RowType && edmType != this._elementType && (!this._includeSubtypes || !this._elementType.IsAssignableFrom(edmType)) && (edmType.BuiltInTypeKind != BuiltInTypeKind.ComplexType || !this.GetComplexTypes().Contains((ComplexType)edmType)))
				{
					this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedInitialization_QueryView(this._setMapping.Set.Name, edmType.FullName), 2074, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
				}
			}

			// Token: 0x06002BDB RID: 11227 RVA: 0x000D6050 File Offset: 0x000D4250
			private IEnumerable<ComplexType> GetComplexTypes()
			{
				IEnumerable<EdmProperty> properties = this.GetEntityTypes().SelectMany((EntityType entityType) => entityType.Properties).Distinct<EdmProperty>();
				return this.GetComplexTypes(properties);
			}

			// Token: 0x06002BDC RID: 11228 RVA: 0x000D6334 File Offset: 0x000D4534
			private IEnumerable<ComplexType> GetComplexTypes(IEnumerable<EdmProperty> properties)
			{
				foreach (ComplexType complexType in (from p in properties
				select p.TypeUsage.EdmType).OfType<ComplexType>())
				{
					yield return complexType;
					foreach (ComplexType nestedComplexType in this.GetComplexTypes(complexType.Properties))
					{
						yield return nestedComplexType;
					}
				}
				yield break;
			}

			// Token: 0x06002BDD RID: 11229 RVA: 0x000D6358 File Offset: 0x000D4558
			private IEnumerable<EntityType> GetEntityTypes()
			{
				if (this._includeSubtypes)
				{
					return MetadataHelper.GetTypeAndSubtypesOf(this._elementType, this.EdmItemCollection, true).OfType<EntityType>();
				}
				if (this._elementType.BuiltInTypeKind == BuiltInTypeKind.EntityType)
				{
					return Enumerable.Repeat<EntityType>((EntityType)this._elementType, 1);
				}
				return Enumerable.Empty<EntityType>();
			}

			// Token: 0x06002BDE RID: 11230 RVA: 0x000D63AC File Offset: 0x000D45AC
			public override void Visit(DbFunctionExpression expression)
			{
				Check.NotNull<DbFunctionExpression>(expression, "expression");
				base.Visit(expression);
				if (!ViewValidator.ViewExpressionValidator.IsStoreSpaceOrCanonicalFunction(this.StoreItemCollection, expression.Function))
				{
					this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedFunctionCall_QueryView(this._setMapping.Set.Name, expression.Function.Identity), 2112, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
				}
			}

			// Token: 0x06002BDF RID: 11231 RVA: 0x000D643C File Offset: 0x000D463C
			internal static bool IsStoreSpaceOrCanonicalFunction(StoreItemCollection sSpace, EdmFunction function)
			{
				if (TypeHelpers.IsCanonicalFunction(function))
				{
					return true;
				}
				ReadOnlyCollection<EdmFunction> ctypeFunctions = sSpace.GetCTypeFunctions(function.FullName, false);
				return ctypeFunctions.Contains(function);
			}

			// Token: 0x06002BE0 RID: 11232 RVA: 0x000D6468 File Offset: 0x000D4668
			public override void Visit(DbScanExpression expression)
			{
				Check.NotNull<DbScanExpression>(expression, "expression");
				base.Visit(expression);
				EntitySetBase target = expression.Target;
				EntityContainer entityContainer = target.EntityContainer;
				if (entityContainer.DataSpace != DataSpace.SSpace)
				{
					this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedScanTarget_QueryView(this._setMapping.Set.Name, target.Name), 2072, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
				}
			}

			// Token: 0x0400103C RID: 4156
			private readonly EntitySetBaseMapping _setMapping;

			// Token: 0x0400103D RID: 4157
			private readonly List<EdmSchemaError> _errors;

			// Token: 0x0400103E RID: 4158
			private readonly EntityTypeBase _elementType;

			// Token: 0x0400103F RID: 4159
			private readonly bool _includeSubtypes;
		}

		// Token: 0x020004A8 RID: 1192
		private class AssociationSetViewValidator : DbExpressionVisitor<ViewValidator.DbExpressionEntitySetInfo>
		{
			// Token: 0x06002BE3 RID: 11235 RVA: 0x000D64F6 File Offset: 0x000D46F6
			internal AssociationSetViewValidator(EntitySetBaseMapping setMapping)
			{
				this._setMapping = setMapping;
			}

			// Token: 0x1700060D RID: 1549
			// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x000D651B File Offset: 0x000D471B
			internal List<EdmSchemaError> Errors
			{
				get
				{
					return this._errors;
				}
			}

			// Token: 0x06002BE5 RID: 11237 RVA: 0x000D6523 File Offset: 0x000D4723
			internal ViewValidator.DbExpressionEntitySetInfo VisitExpression(DbExpression expression)
			{
				return expression.Accept<ViewValidator.DbExpressionEntitySetInfo>(this);
			}

			// Token: 0x06002BE6 RID: 11238 RVA: 0x000D652C File Offset: 0x000D472C
			private ViewValidator.DbExpressionEntitySetInfo VisitExpressionBinding(DbExpressionBinding binding)
			{
				if (binding != null)
				{
					return this.VisitExpression(binding.Expression);
				}
				return null;
			}

			// Token: 0x06002BE7 RID: 11239 RVA: 0x000D6540 File Offset: 0x000D4740
			private void VisitExpressionBindingEnterScope(DbExpressionBinding binding)
			{
				ViewValidator.DbExpressionEntitySetInfo value = this.VisitExpressionBinding(binding);
				this.variableScopes.Push(new KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo>(binding.VariableName, value));
			}

			// Token: 0x06002BE8 RID: 11240 RVA: 0x000D656C File Offset: 0x000D476C
			private void VisitExpressionBindingExitScope()
			{
				this.variableScopes.Pop();
			}

			// Token: 0x06002BE9 RID: 11241 RVA: 0x000D65A0 File Offset: 0x000D47A0
			private void ValidateEntitySetsMappedForAssociationSetMapping(ViewValidator.DbExpressionStructuralTypeEntitySetInfo setInfos)
			{
				AssociationSet associationSet = this._setMapping.Set as AssociationSet;
				int num = 0;
				if (setInfos.SetInfos.All((KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo> it) => it.Value != null && it.Value is ViewValidator.DbExpressionSimpleTypeEntitySetInfo) && setInfos.SetInfos.Count<KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo>>() == 2)
				{
					foreach (ViewValidator.DbExpressionEntitySetInfo dbExpressionEntitySetInfo in from it in setInfos.SetInfos
					select it.Value)
					{
						ViewValidator.DbExpressionSimpleTypeEntitySetInfo dbExpressionSimpleTypeEntitySetInfo = (ViewValidator.DbExpressionSimpleTypeEntitySetInfo)dbExpressionEntitySetInfo;
						AssociationSetEnd associationSetEnd = associationSet.AssociationSetEnds[num];
						EntitySet entitySet = associationSetEnd.EntitySet;
						if (!entitySet.Equals(dbExpressionSimpleTypeEntitySetInfo.EntitySet))
						{
							this._errors.Add(new EdmSchemaError(Strings.Mapping_EntitySetMismatchOnAssociationSetEnd_QueryView(dbExpressionSimpleTypeEntitySetInfo.EntitySet.Name, entitySet.Name, associationSetEnd.Name, this._setMapping.Set.Name), 2074, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
						}
						num++;
					}
				}
			}

			// Token: 0x06002BEA RID: 11242 RVA: 0x000D66FC File Offset: 0x000D48FC
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbExpression expression)
			{
				Check.NotNull<DbExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BEB RID: 11243 RVA: 0x000D6738 File Offset: 0x000D4938
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbVariableReferenceExpression expression)
			{
				Check.NotNull<DbVariableReferenceExpression>(expression, "expression");
				return (from it in this.variableScopes
				where it.Key == expression.VariableName
				select it.Value).FirstOrDefault<ViewValidator.DbExpressionEntitySetInfo>();
			}

			// Token: 0x06002BEC RID: 11244 RVA: 0x000D67A4 File Offset: 0x000D49A4
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbPropertyExpression expression)
			{
				Check.NotNull<DbPropertyExpression>(expression, "expression");
				ViewValidator.DbExpressionStructuralTypeEntitySetInfo dbExpressionStructuralTypeEntitySetInfo = this.VisitExpression(expression.Instance) as ViewValidator.DbExpressionStructuralTypeEntitySetInfo;
				if (dbExpressionStructuralTypeEntitySetInfo != null)
				{
					return dbExpressionStructuralTypeEntitySetInfo.GetEntitySetInfoForMember(expression.Property.Name);
				}
				return null;
			}

			// Token: 0x06002BED RID: 11245 RVA: 0x000D67E8 File Offset: 0x000D49E8
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbProjectExpression expression)
			{
				Check.NotNull<DbProjectExpression>(expression, "expression");
				this.VisitExpressionBindingEnterScope(expression.Input);
				ViewValidator.DbExpressionEntitySetInfo result = this.VisitExpression(expression.Projection);
				this.VisitExpressionBindingExitScope();
				return result;
			}

			// Token: 0x06002BEE RID: 11246 RVA: 0x000D6824 File Offset: 0x000D4A24
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbNewInstanceExpression expression)
			{
				Check.NotNull<DbNewInstanceExpression>(expression, "expression");
				ViewValidator.DbExpressionMemberCollectionEntitySetInfo dbExpressionMemberCollectionEntitySetInfo = this.VisitExpressionList(expression.Arguments);
				StructuralType structuralType = expression.ResultType.EdmType as StructuralType;
				if (dbExpressionMemberCollectionEntitySetInfo != null && structuralType != null)
				{
					ViewValidator.DbExpressionStructuralTypeEntitySetInfo dbExpressionStructuralTypeEntitySetInfo = new ViewValidator.DbExpressionStructuralTypeEntitySetInfo();
					int num = 0;
					foreach (ViewValidator.DbExpressionEntitySetInfo value in dbExpressionMemberCollectionEntitySetInfo.entitySetInfos)
					{
						dbExpressionStructuralTypeEntitySetInfo.Add(structuralType.Members[num].Name, value);
						num++;
					}
					if (expression.ResultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.AssociationType)
					{
						this.ValidateEntitySetsMappedForAssociationSetMapping(dbExpressionStructuralTypeEntitySetInfo);
					}
					return dbExpressionStructuralTypeEntitySetInfo;
				}
				return null;
			}

			// Token: 0x06002BEF RID: 11247 RVA: 0x000D68ED File Offset: 0x000D4AED
			private ViewValidator.DbExpressionMemberCollectionEntitySetInfo VisitExpressionList(IList<DbExpression> list)
			{
				return new ViewValidator.DbExpressionMemberCollectionEntitySetInfo(from it in list
				select this.VisitExpression(it));
			}

			// Token: 0x06002BF0 RID: 11248 RVA: 0x000D6906 File Offset: 0x000D4B06
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbRefExpression expression)
			{
				Check.NotNull<DbRefExpression>(expression, "expression");
				return new ViewValidator.DbExpressionSimpleTypeEntitySetInfo(expression.EntitySet);
			}

			// Token: 0x06002BF1 RID: 11249 RVA: 0x000D691F File Offset: 0x000D4B1F
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbComparisonExpression expression)
			{
				Check.NotNull<DbComparisonExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BF2 RID: 11250 RVA: 0x000D692E File Offset: 0x000D4B2E
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbLikeExpression expression)
			{
				Check.NotNull<DbLikeExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BF3 RID: 11251 RVA: 0x000D693D File Offset: 0x000D4B3D
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbLimitExpression expression)
			{
				Check.NotNull<DbLimitExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BF4 RID: 11252 RVA: 0x000D694C File Offset: 0x000D4B4C
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbIsNullExpression expression)
			{
				Check.NotNull<DbIsNullExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BF5 RID: 11253 RVA: 0x000D695B File Offset: 0x000D4B5B
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbArithmeticExpression expression)
			{
				Check.NotNull<DbArithmeticExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BF6 RID: 11254 RVA: 0x000D696A File Offset: 0x000D4B6A
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbAndExpression expression)
			{
				Check.NotNull<DbAndExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BF7 RID: 11255 RVA: 0x000D6979 File Offset: 0x000D4B79
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbOrExpression expression)
			{
				Check.NotNull<DbOrExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BF8 RID: 11256 RVA: 0x000D6988 File Offset: 0x000D4B88
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbInExpression expression)
			{
				Check.NotNull<DbInExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BF9 RID: 11257 RVA: 0x000D6997 File Offset: 0x000D4B97
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbNotExpression expression)
			{
				Check.NotNull<DbNotExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BFA RID: 11258 RVA: 0x000D69A6 File Offset: 0x000D4BA6
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbDistinctExpression expression)
			{
				Check.NotNull<DbDistinctExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BFB RID: 11259 RVA: 0x000D69B5 File Offset: 0x000D4BB5
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbElementExpression expression)
			{
				Check.NotNull<DbElementExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BFC RID: 11260 RVA: 0x000D69C4 File Offset: 0x000D4BC4
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbIsEmptyExpression expression)
			{
				Check.NotNull<DbIsEmptyExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BFD RID: 11261 RVA: 0x000D69D3 File Offset: 0x000D4BD3
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbUnionAllExpression expression)
			{
				Check.NotNull<DbUnionAllExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BFE RID: 11262 RVA: 0x000D69E2 File Offset: 0x000D4BE2
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbIntersectExpression expression)
			{
				Check.NotNull<DbIntersectExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002BFF RID: 11263 RVA: 0x000D69F1 File Offset: 0x000D4BF1
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbExceptExpression expression)
			{
				Check.NotNull<DbExceptExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C00 RID: 11264 RVA: 0x000D6A00 File Offset: 0x000D4C00
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbTreatExpression expression)
			{
				Check.NotNull<DbTreatExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C01 RID: 11265 RVA: 0x000D6A0F File Offset: 0x000D4C0F
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbIsOfExpression expression)
			{
				Check.NotNull<DbIsOfExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C02 RID: 11266 RVA: 0x000D6A1E File Offset: 0x000D4C1E
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbCastExpression expression)
			{
				Check.NotNull<DbCastExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C03 RID: 11267 RVA: 0x000D6A2D File Offset: 0x000D4C2D
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbCaseExpression expression)
			{
				Check.NotNull<DbCaseExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C04 RID: 11268 RVA: 0x000D6A3C File Offset: 0x000D4C3C
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbOfTypeExpression expression)
			{
				Check.NotNull<DbOfTypeExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C05 RID: 11269 RVA: 0x000D6A4B File Offset: 0x000D4C4B
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbRelationshipNavigationExpression expression)
			{
				Check.NotNull<DbRelationshipNavigationExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C06 RID: 11270 RVA: 0x000D6A5A File Offset: 0x000D4C5A
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbDerefExpression expression)
			{
				Check.NotNull<DbDerefExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C07 RID: 11271 RVA: 0x000D6A69 File Offset: 0x000D4C69
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbRefKeyExpression expression)
			{
				Check.NotNull<DbRefKeyExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C08 RID: 11272 RVA: 0x000D6A78 File Offset: 0x000D4C78
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbEntityRefExpression expression)
			{
				Check.NotNull<DbEntityRefExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C09 RID: 11273 RVA: 0x000D6A87 File Offset: 0x000D4C87
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbScanExpression expression)
			{
				Check.NotNull<DbScanExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C0A RID: 11274 RVA: 0x000D6A96 File Offset: 0x000D4C96
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbFilterExpression expression)
			{
				Check.NotNull<DbFilterExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C0B RID: 11275 RVA: 0x000D6AA5 File Offset: 0x000D4CA5
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbConstantExpression expression)
			{
				Check.NotNull<DbConstantExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C0C RID: 11276 RVA: 0x000D6AB4 File Offset: 0x000D4CB4
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbNullExpression expression)
			{
				Check.NotNull<DbNullExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C0D RID: 11277 RVA: 0x000D6AC3 File Offset: 0x000D4CC3
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbCrossJoinExpression expression)
			{
				Check.NotNull<DbCrossJoinExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C0E RID: 11278 RVA: 0x000D6AD2 File Offset: 0x000D4CD2
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbJoinExpression expression)
			{
				Check.NotNull<DbJoinExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C0F RID: 11279 RVA: 0x000D6AE1 File Offset: 0x000D4CE1
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbParameterReferenceExpression expression)
			{
				Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C10 RID: 11280 RVA: 0x000D6AF0 File Offset: 0x000D4CF0
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbFunctionExpression expression)
			{
				Check.NotNull<DbFunctionExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C11 RID: 11281 RVA: 0x000D6AFF File Offset: 0x000D4CFF
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbLambdaExpression expression)
			{
				Check.NotNull<DbLambdaExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C12 RID: 11282 RVA: 0x000D6B0E File Offset: 0x000D4D0E
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbApplyExpression expression)
			{
				Check.NotNull<DbApplyExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C13 RID: 11283 RVA: 0x000D6B1D File Offset: 0x000D4D1D
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbGroupByExpression expression)
			{
				Check.NotNull<DbGroupByExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C14 RID: 11284 RVA: 0x000D6B2C File Offset: 0x000D4D2C
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbSkipExpression expression)
			{
				Check.NotNull<DbSkipExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C15 RID: 11285 RVA: 0x000D6B3B File Offset: 0x000D4D3B
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbSortExpression expression)
			{
				Check.NotNull<DbSortExpression>(expression, "expression");
				return null;
			}

			// Token: 0x06002C16 RID: 11286 RVA: 0x000D6B4A File Offset: 0x000D4D4A
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbQuantifierExpression expression)
			{
				Check.NotNull<DbQuantifierExpression>(expression, "expression");
				return null;
			}

			// Token: 0x04001042 RID: 4162
			private readonly Stack<KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo>> variableScopes = new Stack<KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo>>();

			// Token: 0x04001043 RID: 4163
			private readonly EntitySetBaseMapping _setMapping;

			// Token: 0x04001044 RID: 4164
			private readonly List<EdmSchemaError> _errors = new List<EdmSchemaError>();
		}

		// Token: 0x020004A9 RID: 1193
		internal abstract class DbExpressionEntitySetInfo
		{
		}

		// Token: 0x020004AA RID: 1194
		private class DbExpressionSimpleTypeEntitySetInfo : ViewValidator.DbExpressionEntitySetInfo
		{
			// Token: 0x1700060E RID: 1550
			// (get) Token: 0x06002C1C RID: 11292 RVA: 0x000D6B61 File Offset: 0x000D4D61
			internal EntitySet EntitySet
			{
				get
				{
					return this.m_entitySet;
				}
			}

			// Token: 0x06002C1D RID: 11293 RVA: 0x000D6B69 File Offset: 0x000D4D69
			internal DbExpressionSimpleTypeEntitySetInfo(EntitySet entitySet)
			{
				this.m_entitySet = entitySet;
			}

			// Token: 0x04001048 RID: 4168
			private readonly EntitySet m_entitySet;
		}

		// Token: 0x020004AB RID: 1195
		private class DbExpressionStructuralTypeEntitySetInfo : ViewValidator.DbExpressionEntitySetInfo
		{
			// Token: 0x06002C1E RID: 11294 RVA: 0x000D6B78 File Offset: 0x000D4D78
			internal DbExpressionStructuralTypeEntitySetInfo()
			{
				this.m_entitySetInfos = new Dictionary<string, ViewValidator.DbExpressionEntitySetInfo>();
			}

			// Token: 0x06002C1F RID: 11295 RVA: 0x000D6B8B File Offset: 0x000D4D8B
			internal void Add(string key, ViewValidator.DbExpressionEntitySetInfo value)
			{
				this.m_entitySetInfos.Add(key, value);
			}

			// Token: 0x1700060F RID: 1551
			// (get) Token: 0x06002C20 RID: 11296 RVA: 0x000D6B9A File Offset: 0x000D4D9A
			internal IEnumerable<KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo>> SetInfos
			{
				get
				{
					return this.m_entitySetInfos;
				}
			}

			// Token: 0x06002C21 RID: 11297 RVA: 0x000D6BA2 File Offset: 0x000D4DA2
			internal ViewValidator.DbExpressionEntitySetInfo GetEntitySetInfoForMember(string memberName)
			{
				return this.m_entitySetInfos[memberName];
			}

			// Token: 0x04001049 RID: 4169
			private readonly Dictionary<string, ViewValidator.DbExpressionEntitySetInfo> m_entitySetInfos;
		}

		// Token: 0x020004AC RID: 1196
		private class DbExpressionMemberCollectionEntitySetInfo : ViewValidator.DbExpressionEntitySetInfo
		{
			// Token: 0x06002C22 RID: 11298 RVA: 0x000D6BB0 File Offset: 0x000D4DB0
			internal DbExpressionMemberCollectionEntitySetInfo(IEnumerable<ViewValidator.DbExpressionEntitySetInfo> entitySetInfos)
			{
				this.m_entitySets = entitySetInfos;
			}

			// Token: 0x17000610 RID: 1552
			// (get) Token: 0x06002C23 RID: 11299 RVA: 0x000D6BBF File Offset: 0x000D4DBF
			internal IEnumerable<ViewValidator.DbExpressionEntitySetInfo> entitySetInfos
			{
				get
				{
					return this.m_entitySets;
				}
			}

			// Token: 0x0400104A RID: 4170
			private readonly IEnumerable<ViewValidator.DbExpressionEntitySetInfo> m_entitySets;
		}
	}
}
