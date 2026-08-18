using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Objects.Internal
{
	// Token: 0x0200015F RID: 351
	internal static class EntitySqlQueryBuilder
	{
		// Token: 0x06001A48 RID: 6728 RVA: 0x00059E54 File Offset: 0x00058054
		private static string GetCommandText(ObjectQueryState query)
		{
			string result = null;
			if (!query.TryGetCommandText(out result))
			{
				throw EntityUtil.NotSupported(Strings.ObjectQuery_QueryBuilder_NotSupportedLinqSource);
			}
			return result;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00059E7C File Offset: 0x0005807C
		private static ObjectParameterCollection MergeParameters(ObjectContext context, ObjectParameterCollection sourceQueryParams, ObjectParameter[] builderMethodParams)
		{
			if (sourceQueryParams == null && builderMethodParams.Length == 0)
			{
				return null;
			}
			ObjectParameterCollection objectParameterCollection = ObjectParameterCollection.DeepCopy(sourceQueryParams);
			if (objectParameterCollection == null)
			{
				objectParameterCollection = new ObjectParameterCollection(context.Perspective);
			}
			foreach (ObjectParameter parameter in builderMethodParams)
			{
				objectParameterCollection.Add(parameter);
			}
			return objectParameterCollection;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00059EC4 File Offset: 0x000580C4
		private static ObjectParameterCollection MergeParameters(ObjectParameterCollection query1Params, ObjectParameterCollection query2Params)
		{
			if (query1Params == null && query2Params == null)
			{
				return null;
			}
			ObjectParameterCollection objectParameterCollection;
			ObjectParameterCollection objectParameterCollection2;
			if (query1Params != null)
			{
				objectParameterCollection = ObjectParameterCollection.DeepCopy(query1Params);
				objectParameterCollection2 = query2Params;
			}
			else
			{
				objectParameterCollection = ObjectParameterCollection.DeepCopy(query2Params);
				objectParameterCollection2 = query1Params;
			}
			if (objectParameterCollection2 != null)
			{
				foreach (ObjectParameter objectParameter in ((IEnumerable<ObjectParameter>)objectParameterCollection2))
				{
					objectParameterCollection.Add(objectParameter.ShallowCopy());
				}
			}
			return objectParameterCollection;
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00059F34 File Offset: 0x00058134
		private static ObjectQueryState NewBuilderQuery(ObjectQueryState sourceQuery, Type elementType, StringBuilder queryText, Span newSpan, IEnumerable<ObjectParameter> enumerableParams)
		{
			return EntitySqlQueryBuilder.NewBuilderQuery(sourceQuery, elementType, queryText, false, newSpan, enumerableParams);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00059F44 File Offset: 0x00058144
		private static ObjectQueryState NewBuilderQuery(ObjectQueryState sourceQuery, Type elementType, StringBuilder queryText, bool allowsLimit, Span newSpan, IEnumerable<ObjectParameter> enumerableParams)
		{
			ObjectParameterCollection objectParameterCollection = enumerableParams as ObjectParameterCollection;
			if (objectParameterCollection == null && enumerableParams != null)
			{
				objectParameterCollection = new ObjectParameterCollection(sourceQuery.ObjectContext.Perspective);
				foreach (ObjectParameter parameter in enumerableParams)
				{
					objectParameterCollection.Add(parameter);
				}
			}
			EntitySqlQueryState entitySqlQueryState = new EntitySqlQueryState(elementType, queryText.ToString(), allowsLimit, sourceQuery.ObjectContext, objectParameterCollection, newSpan);
			sourceQuery.ApplySettingsTo(entitySqlQueryState);
			return entitySqlQueryState;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00059FCC File Offset: 0x000581CC
		private static ObjectQueryState BuildSetOp(ObjectQueryState leftQuery, ObjectQueryState rightQuery, Span newSpan, string setOp)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(leftQuery);
			string commandText2 = EntitySqlQueryBuilder.GetCommandText(rightQuery);
			if (leftQuery.ObjectContext != rightQuery.ObjectContext)
			{
				throw EntityUtil.Argument(Strings.ObjectQuery_QueryBuilder_InvalidQueryArgument, "query");
			}
			int capacity = "(\r\n".Length + commandText.Length + setOp.Length + commandText2.Length + "\r\n)".Length;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			stringBuilder.Append("(\r\n");
			stringBuilder.Append(commandText);
			stringBuilder.Append(setOp);
			stringBuilder.Append(commandText2);
			stringBuilder.Append("\r\n)");
			return EntitySqlQueryBuilder.NewBuilderQuery(leftQuery, leftQuery.ElementType, stringBuilder, newSpan, EntitySqlQueryBuilder.MergeParameters(leftQuery.Parameters, rightQuery.Parameters));
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0005A088 File Offset: 0x00058288
		private static ObjectQueryState BuildSelectOrSelectValue(ObjectQueryState query, string alias, string projection, ObjectParameter[] parameters, string projectOp, Type elementType)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(query);
			int capacity = projectOp.Length + projection.Length + "\r\nFROM (\r\n".Length + commandText.Length + "\r\n) AS ".Length + alias.Length;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			stringBuilder.Append(projectOp);
			stringBuilder.Append(projection);
			stringBuilder.Append("\r\nFROM (\r\n");
			stringBuilder.Append(commandText);
			stringBuilder.Append("\r\n) AS ");
			stringBuilder.Append(alias);
			return EntitySqlQueryBuilder.NewBuilderQuery(query, elementType, stringBuilder, null, EntitySqlQueryBuilder.MergeParameters(query.ObjectContext, query.Parameters, parameters));
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0005A12C File Offset: 0x0005832C
		private static ObjectQueryState BuildOrderByOrWhere(ObjectQueryState query, string alias, string predicateOrKeys, ObjectParameter[] parameters, string op, string skipCount, bool allowsLimit)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(query);
			int num = "SELECT VALUE ".Length + alias.Length + "\r\nFROM (\r\n".Length + commandText.Length + "\r\n) AS ".Length + alias.Length + op.Length + predicateOrKeys.Length;
			if (skipCount != null)
			{
				num += "\r\nSKIP\r\n".Length + skipCount.Length;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			stringBuilder.Append("SELECT VALUE ");
			stringBuilder.Append(alias);
			stringBuilder.Append("\r\nFROM (\r\n");
			stringBuilder.Append(commandText);
			stringBuilder.Append("\r\n) AS ");
			stringBuilder.Append(alias);
			stringBuilder.Append(op);
			stringBuilder.Append(predicateOrKeys);
			if (skipCount != null)
			{
				stringBuilder.Append("\r\nSKIP\r\n");
				stringBuilder.Append(skipCount);
			}
			return EntitySqlQueryBuilder.NewBuilderQuery(query, query.ElementType, stringBuilder, allowsLimit, query.Span, EntitySqlQueryBuilder.MergeParameters(query.ObjectContext, query.Parameters, parameters));
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0005A234 File Offset: 0x00058434
		internal static ObjectQueryState Distinct(ObjectQueryState query)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(query);
			StringBuilder stringBuilder = new StringBuilder("SET(\r\n".Length + commandText.Length + "\r\n)".Length);
			stringBuilder.Append("SET(\r\n");
			stringBuilder.Append(commandText);
			stringBuilder.Append("\r\n)");
			return EntitySqlQueryBuilder.NewBuilderQuery(query, query.ElementType, stringBuilder, query.Span, ObjectParameterCollection.DeepCopy(query.Parameters));
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0005A2A8 File Offset: 0x000584A8
		internal static ObjectQueryState Except(ObjectQueryState leftQuery, ObjectQueryState rightQuery)
		{
			return EntitySqlQueryBuilder.BuildSetOp(leftQuery, rightQuery, leftQuery.Span, "\r\n) EXCEPT (\r\n");
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x0005A2BC File Offset: 0x000584BC
		internal static ObjectQueryState GroupBy(ObjectQueryState query, string alias, string keys, string projection, ObjectParameter[] parameters)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(query);
			int capacity = "SELECT ".Length + projection.Length + "\r\nFROM (\r\n".Length + commandText.Length + "\r\n) AS ".Length + alias.Length + "\r\nGROUP BY\r\n".Length + keys.Length;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			stringBuilder.Append("SELECT ");
			stringBuilder.Append(projection);
			stringBuilder.Append("\r\nFROM (\r\n");
			stringBuilder.Append(commandText);
			stringBuilder.Append("\r\n) AS ");
			stringBuilder.Append(alias);
			stringBuilder.Append("\r\nGROUP BY\r\n");
			stringBuilder.Append(keys);
			return EntitySqlQueryBuilder.NewBuilderQuery(query, typeof(DbDataRecord), stringBuilder, null, EntitySqlQueryBuilder.MergeParameters(query.ObjectContext, query.Parameters, parameters));
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x0005A394 File Offset: 0x00058594
		internal static ObjectQueryState Intersect(ObjectQueryState leftQuery, ObjectQueryState rightQuery)
		{
			Span newSpan = Span.CopyUnion(leftQuery.Span, rightQuery.Span);
			return EntitySqlQueryBuilder.BuildSetOp(leftQuery, rightQuery, newSpan, "\r\n) INTERSECT (\r\n");
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x0005A3C0 File Offset: 0x000585C0
		internal static ObjectQueryState OfType(ObjectQueryState query, EdmType newType, Type clrOfType)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(query);
			int capacity = "OFTYPE(\r\n(\r\n".Length + commandText.Length + "\r\n),\r\n[".Length + newType.NamespaceName.Length + ((newType.NamespaceName != string.Empty) ? "].[".Length : 0) + newType.Name.Length + "]\r\n)".Length;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			stringBuilder.Append("OFTYPE(\r\n(\r\n");
			stringBuilder.Append(commandText);
			stringBuilder.Append("\r\n),\r\n[");
			if (newType.NamespaceName != string.Empty)
			{
				stringBuilder.Append(newType.NamespaceName);
				stringBuilder.Append("].[");
			}
			stringBuilder.Append(newType.Name);
			stringBuilder.Append("]\r\n)");
			return EntitySqlQueryBuilder.NewBuilderQuery(query, clrOfType, stringBuilder, query.Span, ObjectParameterCollection.DeepCopy(query.Parameters));
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x0005A4B8 File Offset: 0x000586B8
		internal static ObjectQueryState OrderBy(ObjectQueryState query, string alias, string keys, ObjectParameter[] parameters)
		{
			return EntitySqlQueryBuilder.BuildOrderByOrWhere(query, alias, keys, parameters, "\r\nORDER BY\r\n", null, true);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x0005A4CA File Offset: 0x000586CA
		internal static ObjectQueryState Select(ObjectQueryState query, string alias, string projection, ObjectParameter[] parameters)
		{
			return EntitySqlQueryBuilder.BuildSelectOrSelectValue(query, alias, projection, parameters, "SELECT ", typeof(DbDataRecord));
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x0005A4E4 File Offset: 0x000586E4
		internal static ObjectQueryState SelectValue(ObjectQueryState query, string alias, string projection, ObjectParameter[] parameters, Type projectedType)
		{
			return EntitySqlQueryBuilder.BuildSelectOrSelectValue(query, alias, projection, parameters, "SELECT VALUE ", projectedType);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x0005A4F6 File Offset: 0x000586F6
		internal static ObjectQueryState Skip(ObjectQueryState query, string alias, string keys, string count, ObjectParameter[] parameters)
		{
			return EntitySqlQueryBuilder.BuildOrderByOrWhere(query, alias, keys, parameters, "\r\nORDER BY\r\n", count, true);
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0005A50C File Offset: 0x0005870C
		internal static ObjectQueryState Top(ObjectQueryState query, string alias, string count, ObjectParameter[] parameters)
		{
			int num = count.Length;
			string commandText = EntitySqlQueryBuilder.GetCommandText(query);
			bool allowsLimitSubclause = ((EntitySqlQueryState)query).AllowsLimitSubclause;
			if (allowsLimitSubclause)
			{
				num += commandText.Length + "\r\nLIMIT\r\n".Length;
			}
			else
			{
				num += "SELECT VALUE TOP(\r\n".Length + "\r\n) ".Length + alias.Length + "\r\nFROM (\r\n".Length + commandText.Length + "\r\n) AS ".Length + alias.Length;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			if (allowsLimitSubclause)
			{
				stringBuilder.Append(commandText);
				stringBuilder.Append("\r\nLIMIT\r\n");
				stringBuilder.Append(count);
			}
			else
			{
				stringBuilder.Append("SELECT VALUE TOP(\r\n");
				stringBuilder.Append(count);
				stringBuilder.Append("\r\n) ");
				stringBuilder.Append(alias);
				stringBuilder.Append("\r\nFROM (\r\n");
				stringBuilder.Append(commandText);
				stringBuilder.Append("\r\n) AS ");
				stringBuilder.Append(alias);
			}
			return EntitySqlQueryBuilder.NewBuilderQuery(query, query.ElementType, stringBuilder, query.Span, EntitySqlQueryBuilder.MergeParameters(query.ObjectContext, query.Parameters, parameters));
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0005A62C File Offset: 0x0005882C
		internal static ObjectQueryState Union(ObjectQueryState leftQuery, ObjectQueryState rightQuery)
		{
			Span newSpan = Span.CopyUnion(leftQuery.Span, rightQuery.Span);
			return EntitySqlQueryBuilder.BuildSetOp(leftQuery, rightQuery, newSpan, "\r\n) UNION (\r\n");
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0005A658 File Offset: 0x00058858
		internal static ObjectQueryState UnionAll(ObjectQueryState leftQuery, ObjectQueryState rightQuery)
		{
			Span newSpan = Span.CopyUnion(leftQuery.Span, rightQuery.Span);
			return EntitySqlQueryBuilder.BuildSetOp(leftQuery, rightQuery, newSpan, "\r\n) UNION ALL (\r\n");
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0005A684 File Offset: 0x00058884
		internal static ObjectQueryState Where(ObjectQueryState query, string alias, string predicate, ObjectParameter[] parameters)
		{
			return EntitySqlQueryBuilder.BuildOrderByOrWhere(query, alias, predicate, parameters, "\r\nWHERE\r\n", null, false);
		}

		// Token: 0x04000AFA RID: 2810
		private const string _setOpEpilog = "\r\n)";

		// Token: 0x04000AFB RID: 2811
		private const string _setOpProlog = "(\r\n";

		// Token: 0x04000AFC RID: 2812
		private const string _fromOp = "\r\nFROM (\r\n";

		// Token: 0x04000AFD RID: 2813
		private const string _asOp = "\r\n) AS ";

		// Token: 0x04000AFE RID: 2814
		private const string _distinctProlog = "SET(\r\n";

		// Token: 0x04000AFF RID: 2815
		private const string _distinctEpilog = "\r\n)";

		// Token: 0x04000B00 RID: 2816
		private const string _exceptOp = "\r\n) EXCEPT (\r\n";

		// Token: 0x04000B01 RID: 2817
		private const string _groupByOp = "\r\nGROUP BY\r\n";

		// Token: 0x04000B02 RID: 2818
		private const string _intersectOp = "\r\n) INTERSECT (\r\n";

		// Token: 0x04000B03 RID: 2819
		private const string _ofTypeProlog = "OFTYPE(\r\n(\r\n";

		// Token: 0x04000B04 RID: 2820
		private const string _ofTypeInfix = "\r\n),\r\n[";

		// Token: 0x04000B05 RID: 2821
		private const string _ofTypeInfix2 = "].[";

		// Token: 0x04000B06 RID: 2822
		private const string _ofTypeEpilog = "]\r\n)";

		// Token: 0x04000B07 RID: 2823
		private const string _orderByOp = "\r\nORDER BY\r\n";

		// Token: 0x04000B08 RID: 2824
		private const string _selectOp = "SELECT ";

		// Token: 0x04000B09 RID: 2825
		private const string _selectValueOp = "SELECT VALUE ";

		// Token: 0x04000B0A RID: 2826
		private const string _skipOp = "\r\nSKIP\r\n";

		// Token: 0x04000B0B RID: 2827
		private const string _limitOp = "\r\nLIMIT\r\n";

		// Token: 0x04000B0C RID: 2828
		private const string _topOp = "SELECT VALUE TOP(\r\n";

		// Token: 0x04000B0D RID: 2829
		private const string _topInfix = "\r\n) ";

		// Token: 0x04000B0E RID: 2830
		private const string _unionOp = "\r\n) UNION (\r\n";

		// Token: 0x04000B0F RID: 2831
		private const string _unionAllOp = "\r\n) UNION ALL (\r\n";

		// Token: 0x04000B10 RID: 2832
		private const string _whereOp = "\r\nWHERE\r\n";
	}
}
