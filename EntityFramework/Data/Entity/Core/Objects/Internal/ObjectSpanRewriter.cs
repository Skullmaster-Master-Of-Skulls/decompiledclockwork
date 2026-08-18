using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200058F RID: 1423
	internal class ObjectSpanRewriter
	{
		// Token: 0x060037AB RID: 14251 RVA: 0x00107C03 File Offset: 0x00105E03
		internal static bool EntityTypeEquals(EntityTypeBase entityType1, EntityTypeBase entityType2)
		{
			return object.ReferenceEquals(entityType1, entityType2);
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x00107C0C File Offset: 0x00105E0C
		internal static bool TryRewrite(DbQueryCommandTree tree, Span span, MergeOption mergeOption, AliasGenerator aliasGenerator, out DbExpression newQuery, out SpanIndex spanInfo)
		{
			newQuery = null;
			spanInfo = null;
			ObjectSpanRewriter objectSpanRewriter = null;
			bool flag = Span.RequiresRelationshipSpan(mergeOption);
			if (span != null && span.SpanList.Count > 0)
			{
				objectSpanRewriter = new ObjectFullSpanRewriter(tree, tree.Query, span, aliasGenerator);
			}
			else if (flag)
			{
				objectSpanRewriter = new ObjectSpanRewriter(tree, tree.Query, aliasGenerator);
			}
			if (objectSpanRewriter != null)
			{
				objectSpanRewriter.RelationshipSpan = flag;
				newQuery = objectSpanRewriter.RewriteQuery();
				if (newQuery != null)
				{
					spanInfo = objectSpanRewriter.SpanIndex;
				}
			}
			return spanInfo != null;
		}

		// Token: 0x060037AD RID: 14253 RVA: 0x00107C87 File Offset: 0x00105E87
		internal ObjectSpanRewriter(DbCommandTree tree, DbExpression toRewrite, AliasGenerator aliasGenerator)
		{
			this._toRewrite = toRewrite;
			this._tree = tree;
			this._aliasGenerator = aliasGenerator;
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x00107CAF File Offset: 0x00105EAF
		internal MetadataWorkspace Metadata
		{
			get
			{
				return this._tree.MetadataWorkspace;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060037AF RID: 14255 RVA: 0x00107CBC File Offset: 0x00105EBC
		internal DbExpression Query
		{
			get
			{
				return this._toRewrite;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060037B0 RID: 14256 RVA: 0x00107CC4 File Offset: 0x00105EC4
		// (set) Token: 0x060037B1 RID: 14257 RVA: 0x00107CCC File Offset: 0x00105ECC
		internal bool RelationshipSpan
		{
			get
			{
				return this._relationshipSpan;
			}
			set
			{
				this._relationshipSpan = value;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060037B2 RID: 14258 RVA: 0x00107CD5 File Offset: 0x00105ED5
		internal SpanIndex SpanIndex
		{
			get
			{
				return this._spanIndex;
			}
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x00107CE0 File Offset: 0x00105EE0
		internal DbExpression RewriteQuery()
		{
			DbExpression dbExpression = this.Rewrite(this._toRewrite);
			if (object.ReferenceEquals(this._toRewrite, dbExpression))
			{
				return null;
			}
			return dbExpression;
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x00107D0C File Offset: 0x00105F0C
		internal ObjectSpanRewriter.SpanTrackingInfo InitializeTrackingInfo(bool createAssociationEndTrackingInfo)
		{
			ObjectSpanRewriter.SpanTrackingInfo result = default(ObjectSpanRewriter.SpanTrackingInfo);
			result.ColumnDefinitions = new List<KeyValuePair<string, DbExpression>>();
			result.ColumnNames = new AliasGenerator(string.Format(CultureInfo.InvariantCulture, "Span{0}_Column", new object[]
			{
				this._spanCount
			}));
			result.SpannedColumns = new Dictionary<int, AssociationEndMember>();
			if (createAssociationEndTrackingInfo)
			{
				result.FullSpannedEnds = new Dictionary<AssociationEndMember, bool>();
			}
			return result;
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x00107D7C File Offset: 0x00105F7C
		internal virtual ObjectSpanRewriter.SpanTrackingInfo CreateEntitySpanTrackingInfo(DbExpression expression, EntityType entityType)
		{
			return default(ObjectSpanRewriter.SpanTrackingInfo);
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x00107D94 File Offset: 0x00105F94
		protected DbExpression Rewrite(DbExpression expression)
		{
			DbExpressionKind expressionKind = expression.ExpressionKind;
			if (expressionKind == DbExpressionKind.Element)
			{
				return this.RewriteElementExpression((DbElementExpression)expression);
			}
			if (expressionKind == DbExpressionKind.Limit)
			{
				return this.RewriteLimitExpression((DbLimitExpression)expression);
			}
			BuiltInTypeKind builtInTypeKind = expression.ResultType.EdmType.BuiltInTypeKind;
			if (builtInTypeKind == BuiltInTypeKind.CollectionType)
			{
				return this.RewriteCollection(expression);
			}
			if (builtInTypeKind == BuiltInTypeKind.EntityType)
			{
				return this.RewriteEntity(expression, (EntityType)expression.ResultType.EdmType);
			}
			if (builtInTypeKind != BuiltInTypeKind.RowType)
			{
				return expression;
			}
			return this.RewriteRow(expression, (RowType)expression.ResultType.EdmType);
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x00107E28 File Offset: 0x00106028
		private void AddSpannedRowType(RowType spannedType, TypeUsage originalType)
		{
			if (this._spanIndex == null)
			{
				this._spanIndex = new SpanIndex();
			}
			this._spanIndex.AddSpannedRowType(spannedType, originalType);
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x00107E4A File Offset: 0x0010604A
		private void AddSpanMap(RowType rowType, Dictionary<int, AssociationEndMember> columnMap)
		{
			if (this._spanIndex == null)
			{
				this._spanIndex = new SpanIndex();
			}
			this._spanIndex.AddSpanMap(rowType, columnMap);
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x00107E6C File Offset: 0x0010606C
		private DbExpression RewriteEntity(DbExpression expression, EntityType entityType)
		{
			if (DbExpressionKind.NewInstance == expression.ExpressionKind)
			{
				return expression;
			}
			this._spanCount++;
			int spanCount = this._spanCount;
			ObjectSpanRewriter.SpanTrackingInfo spanTrackingInfo = this.CreateEntitySpanTrackingInfo(expression, entityType);
			List<KeyValuePair<AssociationEndMember, AssociationEndMember>> relationshipSpanEnds = this.GetRelationshipSpanEnds(entityType);
			if (relationshipSpanEnds != null)
			{
				if (spanTrackingInfo.ColumnDefinitions == null)
				{
					spanTrackingInfo = this.InitializeTrackingInfo(false);
				}
				int num = spanTrackingInfo.ColumnDefinitions.Count + 1;
				foreach (KeyValuePair<AssociationEndMember, AssociationEndMember> keyValuePair in relationshipSpanEnds)
				{
					if (spanTrackingInfo.FullSpannedEnds == null || !spanTrackingInfo.FullSpannedEnds.ContainsKey(keyValuePair.Value))
					{
						DbExpression value = null;
						if (!this.TryGetNavigationSource(keyValuePair.Value, out value))
						{
							DbExpression entityRef = expression.GetEntityRef();
							value = entityRef.NavigateAllowingAllRelationshipsInSameTypeHierarchy(keyValuePair.Key, keyValuePair.Value);
						}
						spanTrackingInfo.ColumnDefinitions.Add(new KeyValuePair<string, DbExpression>(spanTrackingInfo.ColumnNames.Next(), value));
						spanTrackingInfo.SpannedColumns[num] = keyValuePair.Value;
						num++;
					}
				}
			}
			if (spanTrackingInfo.ColumnDefinitions == null)
			{
				this._spanCount--;
				return expression;
			}
			spanTrackingInfo.ColumnDefinitions.Insert(0, new KeyValuePair<string, DbExpression>(string.Format(CultureInfo.InvariantCulture, "Span{0}_SpanRoot", new object[]
			{
				spanCount
			}), expression));
			DbExpression dbExpression = DbExpressionBuilder.NewRow(spanTrackingInfo.ColumnDefinitions);
			RowType rowType = (RowType)dbExpression.ResultType.EdmType;
			this.AddSpanMap(rowType, spanTrackingInfo.SpannedColumns);
			return dbExpression;
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x00108020 File Offset: 0x00106220
		private DbExpression RewriteElementExpression(DbElementExpression expression)
		{
			DbExpression dbExpression = this.Rewrite(expression.Argument);
			if (!object.ReferenceEquals(expression.Argument, dbExpression))
			{
				expression = dbExpression.Element();
			}
			return expression;
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x00108054 File Offset: 0x00106254
		private DbExpression RewriteLimitExpression(DbLimitExpression expression)
		{
			DbExpression dbExpression = this.Rewrite(expression.Argument);
			if (!object.ReferenceEquals(expression.Argument, dbExpression))
			{
				expression = dbExpression.Limit(expression.Limit);
			}
			return expression;
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x0010808C File Offset: 0x0010628C
		private DbExpression RewriteRow(DbExpression expression, RowType rowType)
		{
			DbLambdaExpression dbLambdaExpression = expression as DbLambdaExpression;
			DbNewInstanceExpression dbNewInstanceExpression;
			if (dbLambdaExpression != null)
			{
				dbNewInstanceExpression = (dbLambdaExpression.Lambda.Body as DbNewInstanceExpression);
			}
			else
			{
				dbNewInstanceExpression = (expression as DbNewInstanceExpression);
			}
			Dictionary<int, DbExpression> dictionary = null;
			Dictionary<int, DbExpression> dictionary2 = null;
			for (int i = 0; i < rowType.Properties.Count; i++)
			{
				EdmProperty edmProperty = rowType.Properties[i];
				DbExpression dbExpression;
				if (dbNewInstanceExpression != null)
				{
					dbExpression = dbNewInstanceExpression.Arguments[i];
				}
				else
				{
					dbExpression = expression.Property(edmProperty.Name);
				}
				DbExpression dbExpression2 = this.Rewrite(dbExpression);
				if (!object.ReferenceEquals(dbExpression2, dbExpression))
				{
					if (dictionary2 == null)
					{
						dictionary2 = new Dictionary<int, DbExpression>();
					}
					dictionary2[i] = dbExpression2;
				}
				else
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<int, DbExpression>();
					}
					dictionary[i] = dbExpression;
				}
			}
			if (dictionary2 == null)
			{
				return expression;
			}
			List<DbExpression> list = new List<DbExpression>(rowType.Properties.Count);
			List<EdmProperty> list2 = new List<EdmProperty>(rowType.Properties.Count);
			for (int j = 0; j < rowType.Properties.Count; j++)
			{
				EdmProperty edmProperty2 = rowType.Properties[j];
				DbExpression dbExpression3 = null;
				if (!dictionary2.TryGetValue(j, out dbExpression3))
				{
					dbExpression3 = dictionary[j];
				}
				list.Add(dbExpression3);
				list2.Add(new EdmProperty(edmProperty2.Name, dbExpression3.ResultType));
			}
			RowType rowType2 = new RowType(list2, rowType.InitializerMetadata);
			TypeUsage typeUsage = TypeUsage.Create(rowType2);
			DbExpression dbExpression4 = typeUsage.New(list);
			if (dbNewInstanceExpression == null)
			{
				DbExpression dbExpression5 = expression.IsNull();
				DbExpression dbExpression6 = typeUsage.Null();
				dbExpression4 = DbExpressionBuilder.Case(new List<DbExpression>(new DbExpression[]
				{
					dbExpression5
				}), new List<DbExpression>(new DbExpression[]
				{
					dbExpression6
				}), dbExpression4);
			}
			this.AddSpannedRowType(rowType2, expression.ResultType);
			if (dbLambdaExpression != null && dbNewInstanceExpression != null)
			{
				dbExpression4 = DbLambda.Create(dbExpression4, dbLambdaExpression.Lambda.Variables).Invoke(dbLambdaExpression.Arguments);
			}
			return dbExpression4;
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x00108280 File Offset: 0x00106480
		private DbExpression RewriteCollection(DbExpression expression)
		{
			DbExpression dbExpression = expression;
			DbProjectExpression dbProjectExpression = null;
			if (DbExpressionKind.Project == expression.ExpressionKind)
			{
				dbProjectExpression = (DbProjectExpression)expression;
				dbExpression = dbProjectExpression.Input.Expression;
			}
			ObjectSpanRewriter.NavigationInfo navigationInfo = null;
			if (this.RelationshipSpan)
			{
				dbExpression = ObjectSpanRewriter.RelationshipNavigationVisitor.FindNavigationExpression(dbExpression, this._aliasGenerator, out navigationInfo);
			}
			if (navigationInfo != null)
			{
				this.EnterNavigationCollection(navigationInfo);
			}
			else
			{
				this.EnterCollection();
			}
			DbExpression dbExpression2 = expression;
			if (dbProjectExpression != null)
			{
				DbExpression dbExpression3 = this.Rewrite(dbProjectExpression.Projection);
				if (!object.ReferenceEquals(dbProjectExpression.Projection, dbExpression3))
				{
					dbExpression2 = dbExpression.BindAs(dbProjectExpression.Input.VariableName).Project(dbExpression3);
				}
			}
			else
			{
				DbExpressionBinding dbExpressionBinding = dbExpression.BindAs(this._aliasGenerator.Next());
				DbExpression variable = dbExpressionBinding.Variable;
				DbExpression dbExpression4 = this.Rewrite(variable);
				if (!object.ReferenceEquals(variable, dbExpression4))
				{
					dbExpression2 = dbExpressionBinding.Project(dbExpression4);
				}
			}
			this.ExitCollection();
			if (navigationInfo != null && navigationInfo.InUse)
			{
				List<DbVariableReferenceExpression> list = new List<DbVariableReferenceExpression>(1);
				list.Add(navigationInfo.SourceVariable);
				List<DbExpression> list2 = new List<DbExpression>(1);
				list2.Add(navigationInfo.Source);
				dbExpression2 = DbExpressionBuilder.Lambda(dbExpression2, list).Invoke(list2);
			}
			return dbExpression2;
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x0010839D File Offset: 0x0010659D
		private void EnterCollection()
		{
			this._navSources.Push(null);
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x001083AB File Offset: 0x001065AB
		private void EnterNavigationCollection(ObjectSpanRewriter.NavigationInfo info)
		{
			this._navSources.Push(info);
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x001083B9 File Offset: 0x001065B9
		private void ExitCollection()
		{
			this._navSources.Pop();
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x001083C8 File Offset: 0x001065C8
		private bool TryGetNavigationSource(AssociationEndMember wasSourceNowTargetEnd, out DbExpression source)
		{
			source = null;
			ObjectSpanRewriter.NavigationInfo navigationInfo = null;
			if (this._navSources.Count > 0)
			{
				navigationInfo = this._navSources.Peek();
				if (navigationInfo != null && !object.ReferenceEquals(wasSourceNowTargetEnd, navigationInfo.SourceEnd))
				{
					navigationInfo = null;
				}
			}
			if (navigationInfo != null)
			{
				source = navigationInfo.SourceVariable;
				navigationInfo.InUse = true;
				return true;
			}
			return false;
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x0010841C File Offset: 0x0010661C
		private List<KeyValuePair<AssociationEndMember, AssociationEndMember>> GetRelationshipSpanEnds(EntityType entityType)
		{
			List<KeyValuePair<AssociationEndMember, AssociationEndMember>> list = null;
			if (this._relationshipSpan)
			{
				foreach (AssociationType associationType in this._tree.MetadataWorkspace.GetItems<AssociationType>(DataSpace.CSpace))
				{
					if (2 == associationType.AssociationEndMembers.Count)
					{
						AssociationEndMember associationEndMember = associationType.AssociationEndMembers[0];
						AssociationEndMember associationEndMember2 = associationType.AssociationEndMembers[1];
						if (ObjectSpanRewriter.IsValidRelationshipSpan(entityType, associationType, associationEndMember, associationEndMember2))
						{
							if (list == null)
							{
								list = new List<KeyValuePair<AssociationEndMember, AssociationEndMember>>();
							}
							list.Add(new KeyValuePair<AssociationEndMember, AssociationEndMember>(associationEndMember, associationEndMember2));
						}
						if (ObjectSpanRewriter.IsValidRelationshipSpan(entityType, associationType, associationEndMember2, associationEndMember))
						{
							if (list == null)
							{
								list = new List<KeyValuePair<AssociationEndMember, AssociationEndMember>>();
							}
							list.Add(new KeyValuePair<AssociationEndMember, AssociationEndMember>(associationEndMember2, associationEndMember));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x001084EC File Offset: 0x001066EC
		private static bool IsValidRelationshipSpan(EntityType compareType, AssociationType associationType, AssociationEndMember fromEnd, AssociationEndMember toEnd)
		{
			if (!associationType.IsForeignKey && (RelationshipMultiplicity.One == toEnd.RelationshipMultiplicity || toEnd.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne))
			{
				EntityType entityType = (EntityType)((RefType)fromEnd.TypeUsage.EdmType).ElementType;
				return ObjectSpanRewriter.EntityTypeEquals(compareType, entityType) || TypeSemantics.IsSubTypeOf(compareType, entityType) || TypeSemantics.IsSubTypeOf(entityType, compareType);
			}
			return false;
		}

		// Token: 0x0400155F RID: 5471
		private int _spanCount;

		// Token: 0x04001560 RID: 5472
		private SpanIndex _spanIndex;

		// Token: 0x04001561 RID: 5473
		private readonly DbExpression _toRewrite;

		// Token: 0x04001562 RID: 5474
		private bool _relationshipSpan;

		// Token: 0x04001563 RID: 5475
		private readonly DbCommandTree _tree;

		// Token: 0x04001564 RID: 5476
		private readonly Stack<ObjectSpanRewriter.NavigationInfo> _navSources = new Stack<ObjectSpanRewriter.NavigationInfo>();

		// Token: 0x04001565 RID: 5477
		private readonly AliasGenerator _aliasGenerator;

		// Token: 0x02000590 RID: 1424
		internal struct SpanTrackingInfo
		{
			// Token: 0x04001566 RID: 5478
			public List<KeyValuePair<string, DbExpression>> ColumnDefinitions;

			// Token: 0x04001567 RID: 5479
			public AliasGenerator ColumnNames;

			// Token: 0x04001568 RID: 5480
			public Dictionary<int, AssociationEndMember> SpannedColumns;

			// Token: 0x04001569 RID: 5481
			public Dictionary<AssociationEndMember, bool> FullSpannedEnds;
		}

		// Token: 0x02000591 RID: 1425
		private class NavigationInfo
		{
			// Token: 0x060037C4 RID: 14276 RVA: 0x0010854A File Offset: 0x0010674A
			public NavigationInfo(DbRelationshipNavigationExpression originalNavigation, DbRelationshipNavigationExpression rewrittenNavigation)
			{
				this._sourceEnd = (AssociationEndMember)originalNavigation.NavigateFrom;
				this._sourceRef = (DbVariableReferenceExpression)rewrittenNavigation.NavigationSource;
				this._source = originalNavigation.NavigationSource;
			}

			// Token: 0x17000864 RID: 2148
			// (get) Token: 0x060037C5 RID: 14277 RVA: 0x00108580 File Offset: 0x00106780
			public AssociationEndMember SourceEnd
			{
				get
				{
					return this._sourceEnd;
				}
			}

			// Token: 0x17000865 RID: 2149
			// (get) Token: 0x060037C6 RID: 14278 RVA: 0x00108588 File Offset: 0x00106788
			public DbExpression Source
			{
				get
				{
					return this._source;
				}
			}

			// Token: 0x17000866 RID: 2150
			// (get) Token: 0x060037C7 RID: 14279 RVA: 0x00108590 File Offset: 0x00106790
			public DbVariableReferenceExpression SourceVariable
			{
				get
				{
					return this._sourceRef;
				}
			}

			// Token: 0x0400156A RID: 5482
			private readonly DbVariableReferenceExpression _sourceRef;

			// Token: 0x0400156B RID: 5483
			private readonly AssociationEndMember _sourceEnd;

			// Token: 0x0400156C RID: 5484
			private readonly DbExpression _source;

			// Token: 0x0400156D RID: 5485
			public bool InUse;
		}

		// Token: 0x02000592 RID: 1426
		private class RelationshipNavigationVisitor : DefaultExpressionVisitor
		{
			// Token: 0x060037C8 RID: 14280 RVA: 0x00108598 File Offset: 0x00106798
			internal static DbExpression FindNavigationExpression(DbExpression expression, AliasGenerator aliasGenerator, out ObjectSpanRewriter.NavigationInfo navInfo)
			{
				navInfo = null;
				TypeUsage typeUsage = ((CollectionType)expression.ResultType.EdmType).TypeUsage;
				if (!TypeSemantics.IsEntityType(typeUsage) && !TypeSemantics.IsReferenceType(typeUsage))
				{
					return expression;
				}
				ObjectSpanRewriter.RelationshipNavigationVisitor relationshipNavigationVisitor = new ObjectSpanRewriter.RelationshipNavigationVisitor(aliasGenerator);
				DbExpression dbExpression = relationshipNavigationVisitor.Find(expression);
				if (!object.ReferenceEquals(expression, dbExpression))
				{
					navInfo = new ObjectSpanRewriter.NavigationInfo(relationshipNavigationVisitor._original, relationshipNavigationVisitor._rewritten);
					return dbExpression;
				}
				return expression;
			}

			// Token: 0x060037C9 RID: 14281 RVA: 0x001085FE File Offset: 0x001067FE
			private RelationshipNavigationVisitor(AliasGenerator aliasGenerator)
			{
				this._aliasGenerator = aliasGenerator;
			}

			// Token: 0x060037CA RID: 14282 RVA: 0x0010860D File Offset: 0x0010680D
			private DbExpression Find(DbExpression expression)
			{
				return this.VisitExpression(expression);
			}

			// Token: 0x060037CB RID: 14283 RVA: 0x00108618 File Offset: 0x00106818
			protected override DbExpression VisitExpression(DbExpression expression)
			{
				DbExpressionKind expressionKind = expression.ExpressionKind;
				if (expressionKind <= DbExpressionKind.Limit)
				{
					if (expressionKind != DbExpressionKind.Distinct && expressionKind != DbExpressionKind.Filter && expressionKind != DbExpressionKind.Limit)
					{
						return expression;
					}
				}
				else if (expressionKind != DbExpressionKind.OfType && expressionKind != DbExpressionKind.Project)
				{
					switch (expressionKind)
					{
					case DbExpressionKind.RelationshipNavigation:
					case DbExpressionKind.Skip:
					case DbExpressionKind.Sort:
						break;
					case DbExpressionKind.Scan:
						return expression;
					default:
						return expression;
					}
				}
				return base.VisitExpression(expression);
			}

			// Token: 0x060037CC RID: 14284 RVA: 0x00108670 File Offset: 0x00106870
			public override DbExpression Visit(DbRelationshipNavigationExpression expression)
			{
				Check.NotNull<DbRelationshipNavigationExpression>(expression, "expression");
				this._original = expression;
				string name = this._aliasGenerator.Next();
				DbVariableReferenceExpression navigateFrom = new DbVariableReferenceExpression(expression.NavigationSource.ResultType, name);
				this._rewritten = navigateFrom.Navigate(expression.NavigateFrom, expression.NavigateTo);
				return this._rewritten;
			}

			// Token: 0x060037CD RID: 14285 RVA: 0x001086CC File Offset: 0x001068CC
			public override DbExpression Visit(DbFilterExpression expression)
			{
				Check.NotNull<DbFilterExpression>(expression, "expression");
				DbExpression dbExpression = this.Find(expression.Input.Expression);
				if (!object.ReferenceEquals(dbExpression, expression.Input.Expression))
				{
					return dbExpression.BindAs(expression.Input.VariableName).Filter(expression.Predicate);
				}
				return expression;
			}

			// Token: 0x060037CE RID: 14286 RVA: 0x00108728 File Offset: 0x00106928
			public override DbExpression Visit(DbProjectExpression expression)
			{
				Check.NotNull<DbProjectExpression>(expression, "expression");
				DbExpression dbExpression = expression.Projection;
				if (DbExpressionKind.Deref == dbExpression.ExpressionKind)
				{
					dbExpression = ((DbDerefExpression)dbExpression).Argument;
				}
				if (DbExpressionKind.VariableReference == dbExpression.ExpressionKind)
				{
					DbVariableReferenceExpression dbVariableReferenceExpression = (DbVariableReferenceExpression)dbExpression;
					if (dbVariableReferenceExpression.VariableName.Equals(expression.Input.VariableName, StringComparison.Ordinal))
					{
						DbExpression dbExpression2 = this.Find(expression.Input.Expression);
						if (!object.ReferenceEquals(dbExpression2, expression.Input.Expression))
						{
							return dbExpression2.BindAs(expression.Input.VariableName).Project(expression.Projection);
						}
					}
				}
				return expression;
			}

			// Token: 0x060037CF RID: 14287 RVA: 0x001087CC File Offset: 0x001069CC
			public override DbExpression Visit(DbSortExpression expression)
			{
				Check.NotNull<DbSortExpression>(expression, "expression");
				DbExpression dbExpression = this.Find(expression.Input.Expression);
				if (!object.ReferenceEquals(dbExpression, expression.Input.Expression))
				{
					return dbExpression.BindAs(expression.Input.VariableName).Sort(expression.SortOrder);
				}
				return expression;
			}

			// Token: 0x060037D0 RID: 14288 RVA: 0x00108828 File Offset: 0x00106A28
			public override DbExpression Visit(DbSkipExpression expression)
			{
				Check.NotNull<DbSkipExpression>(expression, "expression");
				DbExpression dbExpression = this.Find(expression.Input.Expression);
				if (!object.ReferenceEquals(dbExpression, expression.Input.Expression))
				{
					return dbExpression.BindAs(expression.Input.VariableName).Skip(expression.SortOrder, expression.Count);
				}
				return expression;
			}

			// Token: 0x0400156E RID: 5486
			private readonly AliasGenerator _aliasGenerator;

			// Token: 0x0400156F RID: 5487
			private DbRelationshipNavigationExpression _original;

			// Token: 0x04001570 RID: 5488
			private DbRelationshipNavigationExpression _rewritten;
		}
	}
}
