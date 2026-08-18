using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000162 RID: 354
	internal class ObjectFullSpanRewriter : ObjectSpanRewriter
	{
		// Token: 0x06001A79 RID: 6777 RVA: 0x0005AC60 File Offset: 0x00058E60
		internal ObjectFullSpanRewriter(DbCommandTree tree, DbExpression toRewrite, Span span, AliasGenerator aliasGenerator) : base(tree, toRewrite, aliasGenerator)
		{
			EntityType declaringType = null;
			if (!ObjectFullSpanRewriter.TryGetEntityType(base.Query.ResultType, out declaringType))
			{
				throw EntityUtil.InvalidOperation(Strings.ObjectQuery_Span_IncludeRequiresEntityOrEntityCollection);
			}
			ObjectFullSpanRewriter.SpanPathInfo spanPathInfo = new ObjectFullSpanRewriter.SpanPathInfo(declaringType);
			foreach (Span.SpanPath spanPath in span.SpanList)
			{
				this.AddSpanPath(spanPathInfo, spanPath.Navigations);
			}
			this._currentSpanPath.Push(spanPathInfo);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0005AD04 File Offset: 0x00058F04
		private void AddSpanPath(ObjectFullSpanRewriter.SpanPathInfo parentInfo, List<string> navPropNames)
		{
			this.ConvertSpanPath(parentInfo, navPropNames, 0);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0005AD10 File Offset: 0x00058F10
		private void ConvertSpanPath(ObjectFullSpanRewriter.SpanPathInfo parentInfo, List<string> navPropNames, int pos)
		{
			NavigationProperty navigationProperty = null;
			if (!parentInfo.DeclaringType.NavigationProperties.TryGetValue(navPropNames[pos], true, out navigationProperty))
			{
				throw EntityUtil.InvalidOperation(Strings.ObjectQuery_Span_NoNavProp(parentInfo.DeclaringType.FullName, navPropNames[pos]));
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

		// Token: 0x06001A7C RID: 6780 RVA: 0x0005ADAC File Offset: 0x00058FAC
		private static EntityType EntityTypeFromResultType(NavigationProperty navProp)
		{
			EntityType result = null;
			ObjectFullSpanRewriter.TryGetEntityType(navProp.TypeUsage, out result);
			return result;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0005ADCC File Offset: 0x00058FCC
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

		// Token: 0x06001A7E RID: 6782 RVA: 0x0005AE34 File Offset: 0x00059034
		private AssociationEndMember GetNavigationPropertyTargetEnd(NavigationProperty property)
		{
			AssociationType item = base.Metadata.GetItem<AssociationType>(property.RelationshipType.FullName, DataSpace.CSpace);
			return item.AssociationEndMembers[property.ToEndMember.Name];
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0005AE70 File Offset: 0x00059070
		internal override ObjectSpanRewriter.SpanTrackingInfo CreateEntitySpanTrackingInfo(DbExpression expression, EntityType entityType)
		{
			ObjectSpanRewriter.SpanTrackingInfo spanTrackingInfo = default(ObjectSpanRewriter.SpanTrackingInfo);
			ObjectFullSpanRewriter.SpanPathInfo spanPathInfo = this._currentSpanPath.Peek();
			if (spanPathInfo.Children != null)
			{
				int num = 1;
				foreach (KeyValuePair<NavigationProperty, ObjectFullSpanRewriter.SpanPathInfo> keyValuePair in spanPathInfo.Children)
				{
					if (spanTrackingInfo.ColumnDefinitions == null)
					{
						spanTrackingInfo = base.InitializeTrackingInfo(base.RelationshipSpan);
					}
					DbExpression dbExpression = expression.Property(keyValuePair.Key);
					this._currentSpanPath.Push(keyValuePair.Value);
					dbExpression = base.Rewrite(dbExpression);
					this._currentSpanPath.Pop();
					spanTrackingInfo.ColumnDefinitions.Add(new KeyValuePair<string, DbExpression>(spanTrackingInfo.ColumnNames.Next(), dbExpression));
					AssociationEndMember navigationPropertyTargetEnd = this.GetNavigationPropertyTargetEnd(keyValuePair.Key);
					spanTrackingInfo.SpannedColumns[num] = navigationPropertyTargetEnd;
					if (base.RelationshipSpan)
					{
						spanTrackingInfo.FullSpannedEnds[navigationPropertyTargetEnd] = true;
					}
					num++;
				}
			}
			return spanTrackingInfo;
		}

		// Token: 0x04000B1F RID: 2847
		private Stack<ObjectFullSpanRewriter.SpanPathInfo> _currentSpanPath = new Stack<ObjectFullSpanRewriter.SpanPathInfo>();

		// Token: 0x020004B3 RID: 1203
		private class SpanPathInfo
		{
			// Token: 0x06003C7A RID: 15482 RVA: 0x000E3289 File Offset: 0x000E1489
			internal SpanPathInfo(EntityType declaringType)
			{
				this.DeclaringType = declaringType;
			}

			// Token: 0x04001A68 RID: 6760
			internal EntityType DeclaringType;

			// Token: 0x04001A69 RID: 6761
			internal Dictionary<NavigationProperty, ObjectFullSpanRewriter.SpanPathInfo> Children;
		}
	}
}
