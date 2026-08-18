using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200057E RID: 1406
	internal static class EntitySqlQueryBuilder
	{
		// Token: 0x060036EF RID: 14063 RVA: 0x00104E58 File Offset: 0x00103058
		private static string GetCommandText(ObjectQueryState query)
		{
			string result = null;
			if (!query.TryGetCommandText(out result))
			{
				throw new NotSupportedException(Strings.ObjectQuery_QueryBuilder_NotSupportedLinqSource);
			}
			return result;
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x00104E80 File Offset: 0x00103080
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
			foreach (ObjectParameter item in builderMethodParams)
			{
				objectParameterCollection.Add(item);
			}
			return objectParameterCollection;
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x00104ECC File Offset: 0x001030CC
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
				foreach (ObjectParameter objectParameter in objectParameterCollection2)
				{
					objectParameterCollection.Add(objectParameter.ShallowCopy());
				}
			}
			return objectParameterCollection;
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x00104F3C File Offset: 0x0010313C
		private static ObjectQueryState NewBuilderQuery(ObjectQueryState sourceQuery, Type elementType, StringBuilder queryText, Span newSpan, IEnumerable<ObjectParameter> enumerableParams)
		{
			return EntitySqlQueryBuilder.NewBuilderQuery(sourceQuery, elementType, queryText, false, newSpan, enumerableParams);
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x00104F4C File Offset: 0x0010314C
		private static ObjectQueryState NewBuilderQuery(ObjectQueryState sourceQuery, Type elementType, StringBuilder queryText, bool allowsLimit, Span newSpan, IEnumerable<ObjectParameter> enumerableParams)
		{
			ObjectParameterCollection objectParameterCollection = enumerableParams as ObjectParameterCollection;
			if (objectParameterCollection == null && enumerableParams != null)
			{
				objectParameterCollection = new ObjectParameterCollection(sourceQuery.ObjectContext.Perspective);
				foreach (ObjectParameter item in enumerableParams)
				{
					objectParameterCollection.Add(item);
				}
			}
			EntitySqlQueryState entitySqlQueryState = new EntitySqlQueryState(elementType, queryText.ToString(), allowsLimit, sourceQuery.ObjectContext, objectParameterCollection, newSpan);
			sourceQuery.ApplySettingsTo(entitySqlQueryState);
			return entitySqlQueryState;
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x00104FD4 File Offset: 0x001031D4
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		private static ObjectQueryState BuildSetOp(ObjectQueryState leftQuery, ObjectQueryState rightQuery, Span newSpan, string setOp)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(leftQuery);
			string commandText2 = EntitySqlQueryBuilder.GetCommandText(rightQuery);
			if (!object.ReferenceEquals(leftQuery.ObjectContext, rightQuery.ObjectContext))
			{
				throw new ArgumentException(Strings.ObjectQuery_QueryBuilder_InvalidQueryArgument, "query");
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

		// Token: 0x060036F5 RID: 14069 RVA: 0x00105094 File Offset: 0x00103294
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

		// Token: 0x060036F6 RID: 14070 RVA: 0x00105138 File Offset: 0x00103338
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

		// Token: 0x060036F7 RID: 14071 RVA: 0x00105240 File Offset: 0x00103440
		internal static ObjectQueryState Distinct(ObjectQueryState query)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(query);
			StringBuilder stringBuilder = new StringBuilder("SET(\r\n".Length + commandText.Length + "\r\n)".Length);
			stringBuilder.Append("SET(\r\n");
			stringBuilder.Append(commandText);
			stringBuilder.Append("\r\n)");
			return EntitySqlQueryBuilder.NewBuilderQuery(query, query.ElementType, stringBuilder, query.Span, ObjectParameterCollection.DeepCopy(query.Parameters));
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x001052B4 File Offset: 0x001034B4
		internal static ObjectQueryState Except(ObjectQueryState leftQuery, ObjectQueryState rightQuery)
		{
			return EntitySqlQueryBuilder.BuildSetOp(leftQuery, rightQuery, leftQuery.Span, "\r\n) EXCEPT (\r\n");
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x001052C8 File Offset: 0x001034C8
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

		// Token: 0x060036FA RID: 14074 RVA: 0x001053A0 File Offset: 0x001035A0
		internal static ObjectQueryState Intersect(ObjectQueryState leftQuery, ObjectQueryState rightQuery)
		{
			Span newSpan = Span.CopyUnion(leftQuery.Span, rightQuery.Span);
			return EntitySqlQueryBuilder.BuildSetOp(leftQuery, rightQuery, newSpan, "\r\n) INTERSECT (\r\n");
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x001053CC File Offset: 0x001035CC
		internal static ObjectQueryState OfType(ObjectQueryState query, EdmType newType, Type clrOfType)
		{
			string commandText = EntitySqlQueryBuilder.GetCommandText(query);
			int capacity = "OFTYPE(\r\n(\r\n".Length + commandText.Length + "\r\n),\r\n[".Length + newType.NamespaceName.Length + ((!string.IsNullOrEmpty(newType.NamespaceName)) ? "].[".Length : 0) + newType.Name.Length + "]\r\n)".Length;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			stringBuilder.Append("OFTYPE(\r\n(\r\n");
			stringBuilder.Append(commandText);
			stringBuilder.Append("\r\n),\r\n[");
			if (!string.IsNullOrEmpty(newType.NamespaceName))
			{
				stringBuilder.Append(newType.NamespaceName);
				stringBuilder.Append("].[");
			}
			stringBuilder.Append(newType.Name);
			stringBuilder.Append("]\r\n)");
			return EntitySqlQueryBuilder.NewBuilderQuery(query, clrOfType, stringBuilder, query.Span, ObjectParameterCollection.DeepCopy(query.Parameters));
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x001054BA File Offset: 0x001036BA
		internal static ObjectQueryState OrderBy(ObjectQueryState query, string alias, string keys, ObjectParameter[] parameters)
		{
			return EntitySqlQueryBuilder.BuildOrderByOrWhere(query, alias, keys, parameters, "\r\nORDER BY\r\n", null, true);
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x001054CC File Offset: 0x001036CC
		internal static ObjectQueryState Select(ObjectQueryState query, string alias, string projection, ObjectParameter[] parameters)
		{
			return EntitySqlQueryBuilder.BuildSelectOrSelectValue(query, alias, projection, parameters, "SELECT ", typeof(DbDataRecord));
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x001054E6 File Offset: 0x001036E6
		internal static ObjectQueryState SelectValue(ObjectQueryState query, string alias, string projection, ObjectParameter[] parameters, Type projectedType)
		{
			return EntitySqlQueryBuilder.BuildSelectOrSelectValue(query, alias, projection, parameters, "SELECT VALUE ", projectedType);
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x001054F8 File Offset: 0x001036F8
		internal static ObjectQueryState Skip(ObjectQueryState query, string alias, string keys, string count, ObjectParameter[] parameters)
		{
			return EntitySqlQueryBuilder.BuildOrderByOrWhere(query, alias, keys, parameters, "\r\nORDER BY\r\n", count, true);
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x0010550C File Offset: 0x0010370C
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

		// Token: 0x06003701 RID: 14081 RVA: 0x0010562C File Offset: 0x0010382C
		internal static ObjectQueryState Union(ObjectQueryState leftQuery, ObjectQueryState rightQuery)
		{
			Span newSpan = Span.CopyUnion(leftQuery.Span, rightQuery.Span);
			return EntitySqlQueryBuilder.BuildSetOp(leftQuery, rightQuery, newSpan, "\r\n) UNION (\r\n");
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x00105658 File Offset: 0x00103858
		internal static ObjectQueryState UnionAll(ObjectQueryState leftQuery, ObjectQueryState rightQuery)
		{
			Span newSpan = Span.CopyUnion(leftQuery.Span, rightQuery.Span);
			return EntitySqlQueryBuilder.BuildSetOp(leftQuery, rightQuery, newSpan, "\r\n) UNION ALL (\r\n");
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x00105684 File Offset: 0x00103884
		internal static ObjectQueryState Where(ObjectQueryState query, string alias, string predicate, ObjectParameter[] parameters)
		{
			return EntitySqlQueryBuilder.BuildOrderByOrWhere(query, alias, predicate, parameters, "\r\nWHERE\r\n", null, false);
		}

		// Token: 0x04001514 RID: 5396
		private const string _setOpEpilog = "\r\n)";

		// Token: 0x04001515 RID: 5397
		private const string _setOpProlog = "(\r\n";

		// Token: 0x04001516 RID: 5398
		private const string _fromOp = "\r\nFROM (\r\n";

		// Token: 0x04001517 RID: 5399
		private const string _asOp = "\r\n) AS ";

		// Token: 0x04001518 RID: 5400
		private const string _distinctProlog = "SET(\r\n";

		// Token: 0x04001519 RID: 5401
		private const string _distinctEpilog = "\r\n)";

		// Token: 0x0400151A RID: 5402
		private const string _exceptOp = "\r\n) EXCEPT (\r\n";

		// Token: 0x0400151B RID: 5403
		private const string _groupByOp = "\r\nGROUP BY\r\n";

		// Token: 0x0400151C RID: 5404
		private const string _intersectOp = "\r\n) INTERSECT (\r\n";

		// Token: 0x0400151D RID: 5405
		private const string _ofTypeProlog = "OFTYPE(\r\n(\r\n";

		// Token: 0x0400151E RID: 5406
		private const string _ofTypeInfix = "\r\n),\r\n[";

		// Token: 0x0400151F RID: 5407
		private const string _ofTypeInfix2 = "].[";

		// Token: 0x04001520 RID: 5408
		private const string _ofTypeEpilog = "]\r\n)";

		// Token: 0x04001521 RID: 5409
		private const string _orderByOp = "\r\nORDER BY\r\n";

		// Token: 0x04001522 RID: 5410
		private const string _selectOp = "SELECT ";

		// Token: 0x04001523 RID: 5411
		private const string _selectValueOp = "SELECT VALUE ";

		// Token: 0x04001524 RID: 5412
		private const string _skipOp = "\r\nSKIP\r\n";

		// Token: 0x04001525 RID: 5413
		private const string _limitOp = "\r\nLIMIT\r\n";

		// Token: 0x04001526 RID: 5414
		private const string _topOp = "SELECT VALUE TOP(\r\n";

		// Token: 0x04001527 RID: 5415
		private const string _topInfix = "\r\n) ";

		// Token: 0x04001528 RID: 5416
		private const string _unionOp = "\r\n) UNION (\r\n";

		// Token: 0x04001529 RID: 5417
		private const string _unionAllOp = "\r\n) UNION ALL (\r\n";

		// Token: 0x0400152A RID: 5418
		private const string _whereOp = "\r\nWHERE\r\n";
	}
}
