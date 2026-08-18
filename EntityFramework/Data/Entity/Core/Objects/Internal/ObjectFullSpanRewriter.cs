using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000593 RID: 1427
	internal class ObjectFullSpanRewriter : ObjectSpanRewriter
	{
		// Token: 0x060037D1 RID: 14289 RVA: 0x0010888C File Offset: 0x00106A8C
		internal ObjectFullSpanRewriter(DbCommandTree tree, DbExpression toRewrite, Span span, AliasGenerator aliasGenerator) : base(tree, toRewrite, aliasGenerator)
		{
			EntityType declaringType = null;
			if (!ObjectFullSpanRewriter.TryGetEntityType(base.Query.ResultType, out declaringType))
			{
				throw new InvalidOperationException(Strings.ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection);
			}
			ObjectFullSpanRewriter.SpanPathInfo spanPathInfo = new ObjectFullSpanRewriter.SpanPathInfo(declaringType);
			foreach (Span.SpanPath spanPath in span.SpanList)
			{
				this.AddSpanPath(spanPathInfo, spanPath.Navigations);
			}
			this._currentSpanPath.Push(spanPathInfo);
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x00108930 File Offset: 0x00106B30
		private void AddSpanPath(ObjectFullSpanRewriter.SpanPathInfo parentInfo, List<string> navPropNames)
		{
			this.ConvertSpanPath(parentInfo, navPropNames, 0);
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x0010893C File Offset: 0x00106B3C
		private void ConvertSpanPath(ObjectFullSpanRewriter.SpanPathInfo parentInfo, List<string> navPropNames, int pos)
		{
			NavigationProperty navigationProperty = null;
			if (!parentInfo.DeclaringType.NavigationProperties.TryGetValue(navPropNames[pos], true, out navigationProperty))
			{
				throw new InvalidOperationException(Strings.ObjectQuery_Span_NoNavProp(parentInfo.DeclaringType.FullName, navPropNames[pos]));
			}
			if (parentInfo.Children == null)
			{
				parentInfo.Children = new Dictionary<NavigationProperty, ObjectFullSpanRewriter.SpanPathInfo>();
			}
			ObjectFullSpanRewriter.SpanPathInfo spanPathInfo = null;
			if (!parentInfo.Children.TryGetValue(navigationProperty, out spanPathInfo))
			{
				spanPathInfo = new ObjectFullSpanRewriter.SpanPathInfo(ObjectFullSpanRewriter.EntityTypeFromResultType(navigationProperty));
				parentInfo.Children[navigationProperty] = spanPathInfo;
			}
			if (pos < navPropNames.Count - 1)
			{
				this.ConvertSpanPath(spanPathInfo, navPropNames, pos + 1);
			}
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x001089D8 File Offset: 0x00106BD8
		private static EntityType EntityTypeFromResultType(NavigationProperty navProp)
		{
			EntityType result = null;
			ObjectFullSpanRewriter.TryGetEntityType(navProp.TypeUsage, out result);
			return result;
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x001089F8 File Offset: 0x00106BF8
		private static bool TryGetEntityType(TypeUsage resultType, out EntityType entityType)
		{
			if (BuiltInTypeKind.EntityType == resultType.EdmType.BuiltInTypeKind)
			{
				entityType = (EntityType)resultType.EdmType;
				return true;
			}
			if (BuiltInTypeKind.CollectionType == resultType.EdmType.BuiltInTypeKind)
			{
				EdmType edmType = ((CollectionType)resultType.EdmType).TypeUsage.EdmType;
				if (BuiltInTypeKind.EntityType == edmType.BuiltInTypeKind)
				{
					entityType = (EntityType)edmType;
					return true;
				}
			}
			entityType = null;
			return false;
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x00108A60 File Offset: 0x00106C60
		private AssociationEndMember GetNavigationPropertyTargetEnd(NavigationProperty property)
		{
			AssociationType item = base.Metadata.GetItem<AssociationType>(property.RelationshipType.FullName, DataSpace.CSpace);
			return item.AssociationEndMembers[property.ToEndMember.Name];
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x00108A9C File Offset: 0x00106C9C
		internal override ObjectSpanRewriter.SpanTrackingInfo CreateEntitySpanTrackingInfo(DbExpression expression, EntityType entityType)
		{
			ObjectSpanRewriter.SpanTrackingInfo result = default(ObjectSpanRewriter.SpanTrackingInfo);
			ObjectFullSpanRewriter.SpanPathInfo spanPathInfo = this._currentSpanPath.Peek();
			if (spanPathInfo.Children != null)
			{
				int num = 1;
				foreach (KeyValuePair<NavigationProperty, ObjectFullSpanRewriter.SpanPathInfo> keyValuePair in spanPathInfo.Children)
				{
					if (result.ColumnDefinitions == null)
					{
						result = base.InitializeTrackingInfo(base.RelationshipSpan);
					}
					DbExpression dbExpression = expression.Property(keyValuePair.Key);
					this._currentSpanPath.Push(keyValuePair.Value);
					dbExpression = base.Rewrite(dbExpression);
					this._currentSpanPath.Pop();
					result.ColumnDefinitions.Add(new KeyValuePair<string, DbExpression>(result.ColumnNames.Next(), dbExpression));
					AssociationEndMember navigationPropertyTargetEnd = this.GetNavigationPropertyTargetEnd(keyValuePair.Key);
					result.SpannedColumns[num] = navigationPropertyTargetEnd;
					if (base.RelationshipSpan)
					{
						result.FullSpannedEnds[navigationPropertyTargetEnd] = true;
					}
					num++;
				}
			}
			return result;
		}

		// Token: 0x04001571 RID: 5489
		private readonly Stack<ObjectFullSpanRewriter.SpanPathInfo> _currentSpanPath = new Stack<ObjectFullSpanRewriter.SpanPathInfo>();

		// Token: 0x02000594 RID: 1428
		private class SpanPathInfo
		{
			// Token: 0x060037D8 RID: 14296 RVA: 0x00108BB8 File Offset: 0x00106DB8
			internal SpanPathInfo(EntityType declaringType)
			{
				this.DeclaringType = declaringType;
			}

			// Token: 0x04001572 RID: 5490
			internal readonly EntityType DeclaringType;

			// Token: 0x04001573 RID: 5491
			internal Dictionary<NavigationProperty, ObjectFullSpanRewriter.SpanPathInfo> Children;
		}
	}
}
