using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Mapping
{
	// Token: 0x0200022F RID: 559
	internal static class ViewValidator
	{
		// Token: 0x060023D3 RID: 9171 RVA: 0x00081628 File Offset: 0x0007F828
		internal static IEnumerable<EdmSchemaError> ValidateQueryView(DbQueryCommandTree view, StorageSetMapping setMapping, EntityTypeBase elementType, bool includeSubtypes)
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

		// Token: 0x02000565 RID: 1381
		private sealed class ViewExpressionValidator : BasicExpressionVisitor
		{
			// Token: 0x17000B35 RID: 2869
			// (get) Token: 0x06003F36 RID: 16182 RVA: 0x000E9CE4 File Offset: 0x000E7EE4
			private EdmItemCollection EdmItemCollection
			{
				get
				{
					return this._setMapping.EntityContainerMapping.StorageMappingItemCollection.EdmItemCollection;
				}
			}

			// Token: 0x17000B36 RID: 2870
			// (get) Token: 0x06003F37 RID: 16183 RVA: 0x000E9CFB File Offset: 0x000E7EFB
			private StoreItemCollection StoreItemCollection
			{
				get
				{
					return this._setMapping.EntityContainerMapping.StorageMappingItemCollection.StoreItemCollection;
				}
			}

			// Token: 0x06003F38 RID: 16184 RVA: 0x000E9D12 File Offset: 0x000E7F12
			internal ViewExpressionValidator(StorageSetMapping setMapping, EntityTypeBase elementType, bool includeSubtypes)
			{
				this._setMapping = setMapping;
				this._elementType = elementType;
				this._includeSubtypes = includeSubtypes;
				this._errors = new List<EdmSchemaError>();
			}

			// Token: 0x17000B37 RID: 2871
			// (get) Token: 0x06003F39 RID: 16185 RVA: 0x000E9D3A File Offset: 0x000E7F3A
			internal IEnumerable<EdmSchemaError> Errors
			{
				get
				{
					return this._errors;
				}
			}

			// Token: 0x06003F3A RID: 16186 RVA: 0x000E9D42 File Offset: 0x000E7F42
			public override void VisitExpression(DbExpression expression)
			{
				if (expression != null)
				{
					this.ValidateExpressionKind(expression.ExpressionKind);
				}
				base.VisitExpression(expression);
			}

			// Token: 0x06003F3B RID: 16187 RVA: 0x000E9D5C File Offset: 0x000E7F5C
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
					if (expressionKind == DbExpressionKind.Scan || expressionKind - DbExpressionKind.UnionAll <= 1)
					{
						return;
					}
					break;
				}
				string p = this._includeSubtypes ? ("IsTypeOf(" + this._elementType.ToString() + ")") : this._elementType.ToString();
				this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedExpressionKind_QueryView(this._setMapping.Set.Name, p, expressionKind), 2071, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
			}

			// Token: 0x06003F3C RID: 16188 RVA: 0x000E9EC8 File Offset: 0x000E80C8
			public override void Visit(DbPropertyExpression expression)
			{
				base.Visit(expression);
				if (expression.Property.BuiltInTypeKind != BuiltInTypeKind.EdmProperty)
				{
					this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedPropertyKind_QueryView(this._setMapping.Set.Name, expression.Property.Name, expression.Property.BuiltInTypeKind), 2073, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
				}
			}

			// Token: 0x06003F3D RID: 16189 RVA: 0x000E9F58 File Offset: 0x000E8158
			public override void Visit(DbNewInstanceExpression expression)
			{
				base.Visit(expression);
				EdmType edmType = expression.ResultType.EdmType;
				if (edmType.BuiltInTypeKind != BuiltInTypeKind.RowType && edmType != this._elementType && (!this._includeSubtypes || !this._elementType.IsAssignableFrom(edmType)) && (edmType.BuiltInTypeKind != BuiltInTypeKind.ComplexType || !this.GetComplexTypes().Contains((ComplexType)edmType)))
				{
					this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedInitialization_QueryView(this._setMapping.Set.Name, edmType.FullName), 2074, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
				}
			}

			// Token: 0x06003F3E RID: 16190 RVA: 0x000EA01C File Offset: 0x000E821C
			private IEnumerable<ComplexType> GetComplexTypes()
			{
				IEnumerable<EdmProperty> properties = this.GetEntityTypes().SelectMany((EntityType entityType) => entityType.Properties).Distinct<EdmProperty>();
				return this.GetComplexTypes(properties);
			}

			// Token: 0x06003F3F RID: 16191 RVA: 0x000EA060 File Offset: 0x000E8260
			private IEnumerable<ComplexType> GetComplexTypes(IEnumerable<EdmProperty> properties)
			{
				foreach (ComplexType complexType in (from p in properties
				select p.TypeUsage.EdmType).OfType<ComplexType>())
				{
					yield return complexType;
					foreach (ComplexType complexType2 in this.GetComplexTypes(complexType.Properties))
					{
						yield return complexType2;
					}
					IEnumerator<ComplexType> enumerator2 = null;
					complexType = null;
				}
				IEnumerator<ComplexType> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06003F40 RID: 16192 RVA: 0x000EA078 File Offset: 0x000E8278
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

			// Token: 0x06003F41 RID: 16193 RVA: 0x000EA0CC File Offset: 0x000E82CC
			public override void Visit(DbFunctionExpression expression)
			{
				base.Visit(expression);
				if (!ViewValidator.ViewExpressionValidator.IsStoreSpaceOrCanonicalFunction(this.StoreItemCollection, expression.Function))
				{
					this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedFunctionCall_QueryView(this._setMapping.Set.Name, expression.Function.Identity), 2112, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
				}
			}

			// Token: 0x06003F42 RID: 16194 RVA: 0x000EA150 File Offset: 0x000E8350
			internal static bool IsStoreSpaceOrCanonicalFunction(StoreItemCollection sSpace, EdmFunction function)
			{
				if (TypeHelpers.IsCanonicalFunction(function))
				{
					return true;
				}
				ReadOnlyCollection<EdmFunction> ctypeFunctions = sSpace.GetCTypeFunctions(function.FullName, false);
				return ctypeFunctions.Contains(function);
			}

			// Token: 0x06003F43 RID: 16195 RVA: 0x000EA17C File Offset: 0x000E837C
			public override void Visit(DbScanExpression expression)
			{
				base.Visit(expression);
				EntitySetBase target = expression.Target;
				EntityContainer entityContainer = target.EntityContainer;
				if (entityContainer.DataSpace != DataSpace.SSpace)
				{
					this._errors.Add(new EdmSchemaError(Strings.Mapping_UnsupportedScanTarget_QueryView(this._setMapping.Set.Name, target.Name), 2072, EdmSchemaErrorSeverity.Error, this._setMapping.EntityContainerMapping.SourceLocation, this._setMapping.StartLineNumber, this._setMapping.StartLinePosition));
				}
			}

			// Token: 0x04001C35 RID: 7221
			private readonly StorageSetMapping _setMapping;

			// Token: 0x04001C36 RID: 7222
			private readonly List<EdmSchemaError> _errors;

			// Token: 0x04001C37 RID: 7223
			private readonly EntityTypeBase _elementType;

			// Token: 0x04001C38 RID: 7224
			private readonly bool _includeSubtypes;
		}

		// Token: 0x02000566 RID: 1382
		private class AssociationSetViewValidator : DbExpressionVisitor<ViewValidator.DbExpressionEntitySetInfo>
		{
			// Token: 0x06003F44 RID: 16196 RVA: 0x000EA1FE File Offset: 0x000E83FE
			internal AssociationSetViewValidator(StorageSetMapping setMapping)
			{
				this._setMapping = setMapping;
			}

			// Token: 0x17000B38 RID: 2872
			// (get) Token: 0x06003F45 RID: 16197 RVA: 0x000EA223 File Offset: 0x000E8423
			internal List<EdmSchemaError> Errors
			{
				get
				{
					return this._errors;
				}
			}

			// Token: 0x06003F46 RID: 16198 RVA: 0x000EA22B File Offset: 0x000E842B
			internal ViewValidator.DbExpressionEntitySetInfo VisitExpression(DbExpression expression)
			{
				return expression.Accept<ViewValidator.DbExpressionEntitySetInfo>(this);
			}

			// Token: 0x06003F47 RID: 16199 RVA: 0x000EA234 File Offset: 0x000E8434
			private ViewValidator.DbExpressionEntitySetInfo VisitExpressionBinding(DbExpressionBinding binding)
			{
				if (binding != null)
				{
					return this.VisitExpression(binding.Expression);
				}
				return null;
			}

			// Token: 0x06003F48 RID: 16200 RVA: 0x000EA254 File Offset: 0x000E8454
			private void VisitExpressionBindingEnterScope(DbExpressionBinding binding)
			{
				ViewValidator.DbExpressionEntitySetInfo value = this.VisitExpressionBinding(binding);
				this.variableScopes.Push(new KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo>(binding.VariableName, value));
			}

			// Token: 0x06003F49 RID: 16201 RVA: 0x000EA280 File Offset: 0x000E8480
			private void VisitExpressionBindingExitScope()
			{
				this.variableScopes.Pop();
			}

			// Token: 0x06003F4A RID: 16202 RVA: 0x000EA290 File Offset: 0x000E8490
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

			// Token: 0x06003F4B RID: 16203 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbExpression expression)
			{
				return null;
			}

			// Token: 0x06003F4C RID: 16204 RVA: 0x000EA3EC File Offset: 0x000E85EC
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbVariableReferenceExpression expression)
			{
				return (from it in this.variableScopes
				where it.Key == expression.VariableName
				select it.Value).FirstOrDefault<ViewValidator.DbExpressionEntitySetInfo>();
			}

			// Token: 0x06003F4D RID: 16205 RVA: 0x000EA448 File Offset: 0x000E8648
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbPropertyExpression expression)
			{
				ViewValidator.DbExpressionStructuralTypeEntitySetInfo dbExpressionStructuralTypeEntitySetInfo = this.VisitExpression(expression.Instance) as ViewValidator.DbExpressionStructuralTypeEntitySetInfo;
				if (dbExpressionStructuralTypeEntitySetInfo != null)
				{
					return dbExpressionStructuralTypeEntitySetInfo.GetEntitySetInfoForMember(expression.Property.Name);
				}
				return null;
			}

			// Token: 0x06003F4E RID: 16206 RVA: 0x000EA480 File Offset: 0x000E8680
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbProjectExpression expression)
			{
				this.VisitExpressionBindingEnterScope(expression.Input);
				ViewValidator.DbExpressionEntitySetInfo result = this.VisitExpression(expression.Projection);
				this.VisitExpressionBindingExitScope();
				return result;
			}

			// Token: 0x06003F4F RID: 16207 RVA: 0x000EA4B0 File Offset: 0x000E86B0
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbNewInstanceExpression expression)
			{
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

			// Token: 0x06003F50 RID: 16208 RVA: 0x000EA564 File Offset: 0x000E8764
			private ViewValidator.DbExpressionMemberCollectionEntitySetInfo VisitExpressionList(IList<DbExpression> list)
			{
				return new ViewValidator.DbExpressionMemberCollectionEntitySetInfo(from it in list
				select this.VisitExpression(it));
			}

			// Token: 0x06003F51 RID: 16209 RVA: 0x000EA57D File Offset: 0x000E877D
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbRefExpression expression)
			{
				return new ViewValidator.DbExpressionSimpleTypeEntitySetInfo(expression.EntitySet);
			}

			// Token: 0x06003F52 RID: 16210 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbComparisonExpression expression)
			{
				return null;
			}

			// Token: 0x06003F53 RID: 16211 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbLikeExpression expression)
			{
				return null;
			}

			// Token: 0x06003F54 RID: 16212 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbLimitExpression expression)
			{
				return null;
			}

			// Token: 0x06003F55 RID: 16213 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbIsNullExpression expression)
			{
				return null;
			}

			// Token: 0x06003F56 RID: 16214 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbArithmeticExpression expression)
			{
				return null;
			}

			// Token: 0x06003F57 RID: 16215 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbAndExpression expression)
			{
				return null;
			}

			// Token: 0x06003F58 RID: 16216 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbOrExpression expression)
			{
				return null;
			}

			// Token: 0x06003F59 RID: 16217 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbNotExpression expression)
			{
				return null;
			}

			// Token: 0x06003F5A RID: 16218 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbDistinctExpression expression)
			{
				return null;
			}

			// Token: 0x06003F5B RID: 16219 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbElementExpression expression)
			{
				return null;
			}

			// Token: 0x06003F5C RID: 16220 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbIsEmptyExpression expression)
			{
				return null;
			}

			// Token: 0x06003F5D RID: 16221 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbUnionAllExpression expression)
			{
				return null;
			}

			// Token: 0x06003F5E RID: 16222 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbIntersectExpression expression)
			{
				return null;
			}

			// Token: 0x06003F5F RID: 16223 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbExceptExpression expression)
			{
				return null;
			}

			// Token: 0x06003F60 RID: 16224 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbTreatExpression expression)
			{
				return null;
			}

			// Token: 0x06003F61 RID: 16225 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbIsOfExpression expression)
			{
				return null;
			}

			// Token: 0x06003F62 RID: 16226 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbCastExpression expression)
			{
				return null;
			}

			// Token: 0x06003F63 RID: 16227 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbCaseExpression expression)
			{
				return null;
			}

			// Token: 0x06003F64 RID: 16228 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbOfTypeExpression expression)
			{
				return null;
			}

			// Token: 0x06003F65 RID: 16229 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbRelationshipNavigationExpression expression)
			{
				return null;
			}

			// Token: 0x06003F66 RID: 16230 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbDerefExpression expression)
			{
				return null;
			}

			// Token: 0x06003F67 RID: 16231 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbRefKeyExpression expression)
			{
				return null;
			}

			// Token: 0x06003F68 RID: 16232 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbEntityRefExpression expression)
			{
				return null;
			}

			// Token: 0x06003F69 RID: 16233 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbScanExpression expression)
			{
				return null;
			}

			// Token: 0x06003F6A RID: 16234 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbFilterExpression expression)
			{
				return null;
			}

			// Token: 0x06003F6B RID: 16235 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbConstantExpression expression)
			{
				return null;
			}

			// Token: 0x06003F6C RID: 16236 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbNullExpression expression)
			{
				return null;
			}

			// Token: 0x06003F6D RID: 16237 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbCrossJoinExpression expression)
			{
				return null;
			}

			// Token: 0x06003F6E RID: 16238 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbJoinExpression expression)
			{
				return null;
			}

			// Token: 0x06003F6F RID: 16239 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbParameterReferenceExpression expression)
			{
				return null;
			}

			// Token: 0x06003F70 RID: 16240 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbFunctionExpression expression)
			{
				return null;
			}

			// Token: 0x06003F71 RID: 16241 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbLambdaExpression expression)
			{
				return null;
			}

			// Token: 0x06003F72 RID: 16242 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbApplyExpression expression)
			{
				return null;
			}

			// Token: 0x06003F73 RID: 16243 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbGroupByExpression expression)
			{
				return null;
			}

			// Token: 0x06003F74 RID: 16244 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbSkipExpression expression)
			{
				return null;
			}

			// Token: 0x06003F75 RID: 16245 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbSortExpression expression)
			{
				return null;
			}

			// Token: 0x06003F76 RID: 16246 RVA: 0x00006174 File Offset: 0x00004374
			public override ViewValidator.DbExpressionEntitySetInfo Visit(DbQuantifierExpression expression)
			{
				return null;
			}

			// Token: 0x04001C39 RID: 7225
			private readonly Stack<KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo>> variableScopes = new Stack<KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo>>();

			// Token: 0x04001C3A RID: 7226
			private StorageSetMapping _setMapping;

			// Token: 0x04001C3B RID: 7227
			private List<EdmSchemaError> _errors = new List<EdmSchemaError>();
		}

		// Token: 0x02000567 RID: 1383
		internal abstract class DbExpressionEntitySetInfo
		{
		}

		// Token: 0x02000568 RID: 1384
		private class DbExpressionSimpleTypeEntitySetInfo : ViewValidator.DbExpressionEntitySetInfo
		{
			// Token: 0x17000B39 RID: 2873
			// (get) Token: 0x06003F79 RID: 16249 RVA: 0x000EA593 File Offset: 0x000E8793
			internal EntitySet EntitySet
			{
				get
				{
					return this.m_entitySet;
				}
			}

			// Token: 0x06003F7A RID: 16250 RVA: 0x000EA59B File Offset: 0x000E879B
			internal DbExpressionSimpleTypeEntitySetInfo(EntitySet entitySet)
			{
				this.m_entitySet = entitySet;
			}

			// Token: 0x04001C3C RID: 7228
			private EntitySet m_entitySet;
		}

		// Token: 0x02000569 RID: 1385
		private class DbExpressionStructuralTypeEntitySetInfo : ViewValidator.DbExpressionEntitySetInfo
		{
			// Token: 0x06003F7B RID: 16251 RVA: 0x000EA5AA File Offset: 0x000E87AA
			internal DbExpressionStructuralTypeEntitySetInfo()
			{
				this.m_entitySetInfos = new Dictionary<string, ViewValidator.DbExpressionEntitySetInfo>();
			}

			// Token: 0x06003F7C RID: 16252 RVA: 0x000EA5BD File Offset: 0x000E87BD
			internal void Add(string key, ViewValidator.DbExpressionEntitySetInfo value)
			{
				this.m_entitySetInfos.Add(key, value);
			}

			// Token: 0x17000B3A RID: 2874
			// (get) Token: 0x06003F7D RID: 16253 RVA: 0x000EA5CC File Offset: 0x000E87CC
			internal IEnumerable<KeyValuePair<string, ViewValidator.DbExpressionEntitySetInfo>> SetInfos
			{
				get
				{
					return this.m_entitySetInfos;
				}
			}

			// Token: 0x06003F7E RID: 16254 RVA: 0x000EA5D4 File Offset: 0x000E87D4
			internal ViewValidator.DbExpressionEntitySetInfo GetEntitySetInfoForMember(string memberName)
			{
				return this.m_entitySetInfos[memberName];
			}

			// Token: 0x04001C3D RID: 7229
			private Dictionary<string, ViewValidator.DbExpressionEntitySetInfo> m_entitySetInfos;
		}

		// Token: 0x0200056A RID: 1386
		private class DbExpressionMemberCollectionEntitySetInfo : ViewValidator.DbExpressionEntitySetInfo
		{
			// Token: 0x06003F7F RID: 16255 RVA: 0x000EA5E2 File Offset: 0x000E87E2
			internal DbExpressionMemberCollectionEntitySetInfo(IEnumerable<ViewValidator.DbExpressionEntitySetInfo> entitySetInfos)
			{
				this.m_entitySets = entitySetInfos;
			}

			// Token: 0x17000B3B RID: 2875
			// (get) Token: 0x06003F80 RID: 16256 RVA: 0x000EA5F1 File Offset: 0x000E87F1
			internal IEnumerable<ViewValidator.DbExpressionEntitySetInfo> entitySetInfos
			{
				get
				{
					return this.m_entitySets;
				}
			}

			// Token: 0x04001C3E RID: 7230
			private IEnumerable<ViewValidator.DbExpressionEntitySetInfo> m_entitySets;
		}
	}
}
