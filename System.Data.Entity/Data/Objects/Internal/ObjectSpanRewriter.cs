using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Globalization;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000163 RID: 355
	internal class ObjectSpanRewriter
	{
		// Token: 0x06001A80 RID: 6784 RVA: 0x0005AF88 File Offset: 0x00059188
		internal static bool EntityTypeEquals(EntityTypeBase entityType1, EntityTypeBase entityType2)
		{
			return entityType1 == entityType2;
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0005AF90 File Offset: 0x00059190
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

		// Token: 0x06001A82 RID: 6786 RVA: 0x0005B008 File Offset: 0x00059208
		internal ObjectSpanRewriter(DbCommandTree tree, DbExpression toRewrite, AliasGenerator aliasGenerator)
		{
			this._toRewrite = toRewrite;
			this._tree = tree;
			this._aliasGenerator = aliasGenerator;
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x0005B030 File Offset: 0x00059230
		internal MetadataWorkspace Metadata
		{
			get
			{
				return this._tree.MetadataWorkspace;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001A84 RID: 6788 RVA: 0x0005B03D File Offset: 0x0005923D
		internal DbExpression Query
		{
			get
			{
				return this._toRewrite;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x0005B045 File Offset: 0x00059245
		// (set) Token: 0x06001A86 RID: 6790 RVA: 0x0005B04D File Offset: 0x0005924D
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

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0005B056 File Offset: 0x00059256
		internal SpanIndex SpanIndex
		{
			get
			{
				return this._spanIndex;
			}
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x0005B060 File Offset: 0x00059260
		internal DbExpression RewriteQuery()
		{
			DbExpression dbExpression = this.Rewrite(this._toRewrite);
			if (this._toRewrite == dbExpression)
			{
				return null;
			}
			return dbExpression;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0005B088 File Offset: 0x00059288
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

		// Token: 0x06001A8A RID: 6794 RVA: 0x0005B0F4 File Offset: 0x000592F4
		internal virtual ObjectSpanRewriter.SpanTrackingInfo CreateEntitySpanTrackingInfo(DbExpression expression, EntityType entityType)
		{
			return default(ObjectSpanRewriter.SpanTrackingInfo);
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0005B10C File Offset: 0x0005930C
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
				return this.RewriteCollection(expression, (CollectionType)expression.ResultType.EdmType);
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

		// Token: 0x06001A8C RID: 6796 RVA: 0x0005B1B0 File Offset: 0x000593B0
		private void AddSpannedRowType(RowType spannedType, TypeUsage originalType)
		{
			if (this._spanIndex == null)
			{
				this._spanIndex = new SpanIndex();
			}
			this._spanIndex.AddSpannedRowType(spannedType, originalType);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x0005B1D2 File Offset: 0x000593D2
		private void AddSpanMap(RowType rowType, Dictionary<int, AssociationEndMember> columnMap)
		{
			if (this._spanIndex == null)
			{
				this._spanIndex = new SpanIndex();
			}
			this._spanIndex.AddSpanMap(rowType, columnMap);
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x0005B1F4 File Offset: 0x000593F4
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

		// Token: 0x06001A8F RID: 6799 RVA: 0x0005B398 File Offset: 0x00059598
		private DbExpression RewriteElementExpression(DbElementExpression expression)
		{
			DbExpression dbExpression = this.Rewrite(expression.Argument);
			if (expression.Argument != dbExpression)
			{
				expression = dbExpression.Element();
			}
			return expression;
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0005B3C4 File Offset: 0x000595C4
		private DbExpression RewriteLimitExpression(DbLimitExpression expression)
		{
			DbExpression dbExpression = this.Rewrite(expression.Argument);
			if (expression.Argument != dbExpression)
			{
				expression = dbExpression.Limit(expression.Limit);
			}
			return expression;
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0005B3F8 File Offset: 0x000595F8
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
				if (dbExpression2 != dbExpression)
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
				DbExpression dbExpression5 = DbExpressionBuilder.CreateIsNullExpressionAllowingRowTypeArgument(expression);
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

		// Token: 0x06001A92 RID: 6802 RVA: 0x0005B5DC File Offset: 0x000597DC
		private DbExpression RewriteCollection(DbExpression expression, CollectionType collectionType)
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
				if (dbProjectExpression.Projection != dbExpression3)
				{
					dbExpression2 = dbExpression.BindAs(dbProjectExpression.Input.VariableName).Project(dbExpression3);
				}
			}
			else
			{
				DbExpressionBinding dbExpressionBinding = dbExpression.BindAs(this._aliasGenerator.Next());
				DbExpression variable = dbExpressionBinding.Variable;
				DbExpression dbExpression4 = this.Rewrite(variable);
				if (variable != dbExpression4)
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

		// Token: 0x06001A93 RID: 6803 RVA: 0x0005B6EF File Offset: 0x000598EF
		private void EnterCollection()
		{
			this._navSources.Push(null);
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0005B6FD File Offset: 0x000598FD
		private void EnterNavigationCollection(ObjectSpanRewriter.NavigationInfo info)
		{
			this._navSources.Push(info);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0005B70B File Offset: 0x0005990B
		private void ExitCollection()
		{
			this._navSources.Pop();
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0005B71C File Offset: 0x0005991C
		private bool TryGetNavigationSource(AssociationEndMember wasSourceNowTargetEnd, out DbExpression source)
		{
			source = null;
			ObjectSpanRewriter.NavigationInfo navigationInfo = null;
			if (this._navSources.Count > 0)
			{
				navigationInfo = this._navSources.Peek();
				if (navigationInfo != null && wasSourceNowTargetEnd != navigationInfo.SourceEnd)
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

		// Token: 0x06001A97 RID: 6807 RVA: 0x0005B76C File Offset: 0x0005996C
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

		// Token: 0x06001A98 RID: 6808 RVA: 0x0005B83C File Offset: 0x00059A3C
		private static bool IsValidRelationshipSpan(EntityType compareType, AssociationType associationType, AssociationEndMember fromEnd, AssociationEndMember toEnd)
		{
			if (!associationType.IsForeignKey && (RelationshipMultiplicity.One == toEnd.RelationshipMultiplicity || toEnd.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne))
			{
				EntityType entityType = (EntityType)((RefType)fromEnd.TypeUsage.EdmType).ElementType;
				return ObjectSpanRewriter.EntityTypeEquals(compareType, entityType) || TypeSemantics.IsSubTypeOf(compareType, entityType) || TypeSemantics.IsSubTypeOf(entityType, compareType);
			}
			return false;
		}

		// Token: 0x04000B20 RID: 2848
		private int _spanCount;

		// Token: 0x04000B21 RID: 2849
		private SpanIndex _spanIndex;

		// Token: 0x04000B22 RID: 2850
		private DbExpression _toRewrite;

		// Token: 0x04000B23 RID: 2851
		private bool _relationshipSpan;

		// Token: 0x04000B24 RID: 2852
		private DbCommandTree _tree;

		// Token: 0x04000B25 RID: 2853
		private Stack<ObjectSpanRewriter.NavigationInfo> _navSources = new Stack<ObjectSpanRewriter.NavigationInfo>();

		// Token: 0x04000B26 RID: 2854
		private readonly AliasGenerator _aliasGenerator;

		// Token: 0x020004B4 RID: 1204
		internal struct SpanTrackingInfo
		{
			// Token: 0x04001A6A RID: 6762
			public List<KeyValuePair<string, DbExpression>> ColumnDefinitions;

			// Token: 0x04001A6B RID: 6763
			public AliasGenerator ColumnNames;

			// Token: 0x04001A6C RID: 6764
			public Dictionary<int, AssociationEndMember> SpannedColumns;

			// Token: 0x04001A6D RID: 6765
			public Dictionary<AssociationEndMember, bool> FullSpannedEnds;
		}

		// Token: 0x020004B5 RID: 1205
		private class NavigationInfo
		{
			// Token: 0x06003C7B RID: 15483 RVA: 0x000E3298 File Offset: 0x000E1498
			public NavigationInfo(DbRelationshipNavigationExpression originalNavigation, DbRelationshipNavigationExpression rewrittenNavigation)
			{
				this._original = originalNavigation;
				this._rewritten = rewrittenNavigation;
				this._sourceEnd = (AssociationEndMember)originalNavigation.NavigateFrom;
				this._sourceRef = (DbVariableReferenceExpression)rewrittenNavigation.NavigationSource;
				this._source = originalNavigation.NavigationSource;
			}

			// Token: 0x17000AF2 RID: 2802
			// (get) Token: 0x06003C7C RID: 15484 RVA: 0x000E32E7 File Offset: 0x000E14E7
			public AssociationEndMember SourceEnd
			{
				get
				{
					return this._sourceEnd;
				}
			}

			// Token: 0x17000AF3 RID: 2803
			// (get) Token: 0x06003C7D RID: 15485 RVA: 0x000E32EF File Offset: 0x000E14EF
			public DbExpression Source
			{
				get
				{
					return this._source;
				}
			}

			// Token: 0x17000AF4 RID: 2804
			// (get) Token: 0x06003C7E RID: 15486 RVA: 0x000E32F7 File Offset: 0x000E14F7
			public DbVariableReferenceExpression SourceVariable
			{
				get
				{
					return this._sourceRef;
				}
			}

			// Token: 0x04001A6E RID: 6766
			private readonly DbRelationshipNavigationExpression _original;

			// Token: 0x04001A6F RID: 6767
			private readonly DbRelationshipNavigationExpression _rewritten;

			// Token: 0x04001A70 RID: 6768
			private DbVariableReferenceExpression _sourceRef;

			// Token: 0x04001A71 RID: 6769
			private AssociationEndMember _sourceEnd;

			// Token: 0x04001A72 RID: 6770
			private DbExpression _source;

			// Token: 0x04001A73 RID: 6771
			public bool InUse;
		}

		// Token: 0x020004B6 RID: 1206
		private class RelationshipNavigationVisitor : DefaultExpressionVisitor
		{
			// Token: 0x06003C7F RID: 15487 RVA: 0x000E3300 File Offset: 0x000E1500
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
				if (expression != dbExpression)
				{
					navInfo = new ObjectSpanRewriter.NavigationInfo(relationshipNavigationVisitor._original, relationshipNavigationVisitor._rewritten);
					return dbExpression;
				}
				return expression;
			}

			// Token: 0x06003C80 RID: 15488 RVA: 0x000E3361 File Offset: 0x000E1561
			private RelationshipNavigationVisitor(AliasGenerator aliasGenerator)
			{
				this._aliasGenerator = aliasGenerator;
			}

			// Token: 0x06003C81 RID: 15489 RVA: 0x000E3370 File Offset: 0x000E1570
			private DbExpression Find(DbExpression expression)
			{
				return this.VisitExpression(expression);
			}

			// Token: 0x06003C82 RID: 15490 RVA: 0x000E337C File Offset: 0x000E157C
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
				else if (expressionKind <= DbExpressionKind.Project)
				{
					if (expressionKind != DbExpressionKind.OfType && expressionKind != DbExpressionKind.Project)
					{
						return expression;
					}
				}
				else if (expressionKind != DbExpressionKind.RelationshipNavigation && expressionKind - DbExpressionKind.Skip > 1)
				{
					return expression;
				}
				return base.VisitExpression(expression);
			}

			// Token: 0x06003C83 RID: 15491 RVA: 0x000E33CC File Offset: 0x000E15CC
			public override DbExpression Visit(DbRelationshipNavigationExpression expression)
			{
				this._original = expression;
				string name = this._aliasGenerator.Next();
				DbVariableReferenceExpression navigateFrom = new DbVariableReferenceExpression(expression.NavigationSource.ResultType, name);
				this._rewritten = navigateFrom.Navigate(expression.NavigateFrom, expression.NavigateTo);
				return this._rewritten;
			}

			// Token: 0x06003C84 RID: 15492 RVA: 0x000E341C File Offset: 0x000E161C
			public override DbExpression Visit(DbFilterExpression expression)
			{
				DbExpression dbExpression = this.Find(expression.Input.Expression);
				if (dbExpression != expression.Input.Expression)
				{
					return dbExpression.BindAs(expression.Input.VariableName).Filter(expression.Predicate);
				}
				return expression;
			}

			// Token: 0x06003C85 RID: 15493 RVA: 0x000E3468 File Offset: 0x000E1668
			public override DbExpression Visit(DbProjectExpression expression)
			{
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
						if (dbExpression2 != expression.Input.Expression)
						{
							return dbExpression2.BindAs(expression.Input.VariableName).Project(expression.Projection);
						}
					}
				}
				return expression;
			}

			// Token: 0x06003C86 RID: 15494 RVA: 0x000E34FC File Offset: 0x000E16FC
			public override DbExpression Visit(DbSortExpression expression)
			{
				DbExpression dbExpression = this.Find(expression.Input.Expression);
				if (dbExpression != expression.Input.Expression)
				{
					return dbExpression.BindAs(expression.Input.VariableName).Sort(expression.SortOrder);
				}
				return expression;
			}

			// Token: 0x06003C87 RID: 15495 RVA: 0x000E3548 File Offset: 0x000E1748
			public override DbExpression Visit(DbSkipExpression expression)
			{
				DbExpression dbExpression = this.Find(expression.Input.Expression);
				if (dbExpression != expression.Input.Expression)
				{
					return dbExpression.BindAs(expression.Input.VariableName).Skip(expression.SortOrder, expression.Count);
				}
				return expression;
			}

			// Token: 0x04001A74 RID: 6772
			private readonly AliasGenerator _aliasGenerator;

			// Token: 0x04001A75 RID: 6773
			private DbRelationshipNavigationExpression _original;

			// Token: 0x04001A76 RID: 6774
			private DbRelationshipNavigationExpression _rewritten;
		}
	}
}
