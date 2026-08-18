using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.EntitySql;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Data.Spatial;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Data.Objects.ELinq
{
	// Token: 0x0200019B RID: 411
	internal sealed class ExpressionConverter
	{
		// Token: 0x06001DE8 RID: 7656 RVA: 0x000667F4 File Offset: 0x000649F4
		internal ExpressionConverter(Funcletizer funcletizer, Expression expression)
		{
			EntityUtil.CheckArgumentNull<Funcletizer>(funcletizer, "funcletizer");
			EntityUtil.CheckArgumentNull<Expression>(expression, "expression");
			this._funcletizer = funcletizer;
			expression = funcletizer.Funcletize(expression, out this._recompileRequired);
			LinqExpressionNormalizer linqExpressionNormalizer = new LinqExpressionNormalizer();
			this._expression = linqExpressionNormalizer.Visit(expression);
			this._perspective = funcletizer.RootContext.Perspective;
			this._bindingContext = new BindingContext();
			this._ignoreInclude = 0;
			this._orderByLifter = new ExpressionConverter.OrderByLifter(this._aliasGenerator);
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x00066890 File Offset: 0x00064A90
		private static Dictionary<ExpressionType, ExpressionConverter.Translator> InitializeTranslators()
		{
			Dictionary<ExpressionType, ExpressionConverter.Translator> dictionary = new Dictionary<ExpressionType, ExpressionConverter.Translator>();
			foreach (ExpressionConverter.Translator translator in ExpressionConverter.GetTranslators())
			{
				foreach (ExpressionType key in translator.NodeTypes)
				{
					dictionary.Add(key, translator);
				}
			}
			return dictionary;
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0006691C File Offset: 0x00064B1C
		private static IEnumerable<ExpressionConverter.Translator> GetTranslators()
		{
			yield return new ExpressionConverter.AndAlsoTranslator();
			yield return new ExpressionConverter.OrElseTranslator();
			yield return new ExpressionConverter.LessThanTranslator();
			yield return new ExpressionConverter.LessThanOrEqualsTranslator();
			yield return new ExpressionConverter.GreaterThanTranslator();
			yield return new ExpressionConverter.GreaterThanOrEqualsTranslator();
			yield return new ExpressionConverter.EqualsTranslator();
			yield return new ExpressionConverter.NotEqualsTranslator();
			yield return new ExpressionConverter.ConvertTranslator();
			yield return new ExpressionConverter.ConstantTranslator();
			yield return new ExpressionConverter.NotTranslator();
			yield return new ExpressionConverter.MemberAccessTranslator();
			yield return new ExpressionConverter.ParameterTranslator();
			yield return new ExpressionConverter.MemberInitTranslator();
			yield return new ExpressionConverter.NewTranslator();
			yield return new ExpressionConverter.AddTranslator();
			yield return new ExpressionConverter.ConditionalTranslator();
			yield return new ExpressionConverter.DivideTranslator();
			yield return new ExpressionConverter.ModuloTranslator();
			yield return new ExpressionConverter.SubtractTranslator();
			yield return new ExpressionConverter.MultiplyTranslator();
			yield return new ExpressionConverter.NegateTranslator();
			yield return new ExpressionConverter.UnaryPlusTranslator();
			yield return new ExpressionConverter.MethodCallTranslator();
			yield return new ExpressionConverter.CoalesceTranslator();
			yield return new ExpressionConverter.AsTranslator();
			yield return new ExpressionConverter.IsTranslator();
			yield return new ExpressionConverter.QuoteTranslator();
			yield return new ExpressionConverter.AndTranslator();
			yield return new ExpressionConverter.OrTranslator();
			yield return new ExpressionConverter.ExclusiveOrTranslator();
			yield return new ExpressionConverter.ExtensionTranslator();
			yield return new ExpressionConverter.NewArrayInitTranslator();
			yield return new ExpressionConverter.ListInitTranslator();
			ExpressionType[] array = new ExpressionType[8];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.31D32B096344F000A05D8A588CBD5E85BF51E8A61C6E6B050656883B66CD54B7).FieldHandle);
			yield return new ExpressionConverter.NotSupportedTranslator(array);
			yield break;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x00066925 File Offset: 0x00064B25
		private EdmItemCollection EdmItemCollection
		{
			get
			{
				return (EdmItemCollection)this._funcletizer.RootContext.MetadataWorkspace.GetItemCollection(DataSpace.CSpace, true);
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x00066943 File Offset: 0x00064B43
		internal DbProviderManifest ProviderManifest
		{
			get
			{
				return ((StoreItemCollection)this._funcletizer.RootContext.MetadataWorkspace.GetItemCollection(DataSpace.SSpace)).StoreProviderManifest;
			}
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00066965 File Offset: 0x00064B65
		internal ReadOnlyCollection<KeyValuePair<ObjectParameter, QueryParameterExpression>> GetParameters()
		{
			if (this._parameters != null)
			{
				return this._parameters.AsReadOnly();
			}
			return null;
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x0006697C File Offset: 0x00064B7C
		internal MergeOption? PropagatedMergeOption
		{
			get
			{
				return this._mergeOption;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x00066984 File Offset: 0x00064B84
		internal Span PropagatedSpan
		{
			get
			{
				return this._span;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x0006698C File Offset: 0x00064B8C
		internal Func<bool> RecompileRequired
		{
			get
			{
				return this._recompileRequired;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x00066994 File Offset: 0x00064B94
		// (set) Token: 0x06001DF2 RID: 7666 RVA: 0x0006699C File Offset: 0x00064B9C
		internal int IgnoreInclude
		{
			get
			{
				return this._ignoreInclude;
			}
			set
			{
				this._ignoreInclude = value;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x000669A5 File Offset: 0x00064BA5
		internal AliasGenerator AliasGenerator
		{
			get
			{
				return this._aliasGenerator;
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x000669B0 File Offset: 0x00064BB0
		internal DbExpression Convert()
		{
			DbExpression dbExpression = this.TranslateExpression(this._expression);
			if (!this.TryGetSpan(dbExpression, out this._span))
			{
				this._span = null;
			}
			return dbExpression;
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x000669E1 File Offset: 0x00064BE1
		internal static bool CanFuncletizePropertyInfo(PropertyInfo propertyInfo)
		{
			return ExpressionConverter.MemberAccessTranslator.CanFuncletizePropertyInfo(propertyInfo);
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x000669E9 File Offset: 0x00064BE9
		internal bool CanIncludeSpanInfo()
		{
			return this._ignoreInclude == 0;
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x000669F4 File Offset: 0x00064BF4
		private void NotifyMergeOption(MergeOption mergeOption)
		{
			if (this._mergeOption == null)
			{
				this._mergeOption = new MergeOption?(mergeOption);
			}
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00066A10 File Offset: 0x00064C10
		internal void ValidateInitializerMetadata(InitializerMetadata metadata)
		{
			InitializerMetadata other;
			if (this._initializers != null && this._initializers.TryGetValue(metadata.ClrType, out other))
			{
				if (!metadata.Equals(other))
				{
					throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedHeterogeneousInitializers(ExpressionConverter.DescribeClrType(metadata.ClrType)));
				}
			}
			else
			{
				if (this._initializers == null)
				{
					this._initializers = new Dictionary<Type, InitializerMetadata>();
				}
				this._initializers.Add(metadata.ClrType, metadata);
			}
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x00066A80 File Offset: 0x00064C80
		private void AddParameter(QueryParameterExpression queryParameter)
		{
			if (this._parameters == null)
			{
				this._parameters = new List<KeyValuePair<ObjectParameter, QueryParameterExpression>>();
			}
			if (!(from p in this._parameters
			select p.Value).Contains(queryParameter))
			{
				ObjectParameter key = new ObjectParameter(queryParameter.ParameterReference.ParameterName, queryParameter.Type);
				this._parameters.Add(new KeyValuePair<ObjectParameter, QueryParameterExpression>(key, queryParameter));
			}
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00066AFB File Offset: 0x00064CFB
		private bool IsQueryRoot(Expression Expression)
		{
			return this._expression == Expression;
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00066B08 File Offset: 0x00064D08
		private DbExpression AddSpanMapping(DbExpression expression, Span span)
		{
			if (span != null && this.CanIncludeSpanInfo())
			{
				if (this._spanMappings == null)
				{
					this._spanMappings = new Dictionary<DbExpression, Span>();
				}
				Span span2 = null;
				if (this._spanMappings.TryGetValue(expression, out span2))
				{
					foreach (Span.SpanPath spanPath in span.SpanList)
					{
						span2.AddSpanPath(spanPath);
					}
					this._spanMappings[expression] = span2;
				}
				else
				{
					this._spanMappings[expression] = span;
				}
			}
			return expression;
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x00066BA8 File Offset: 0x00064DA8
		private bool TryGetSpan(DbExpression expression, out Span span)
		{
			if (this._spanMappings != null)
			{
				return this._spanMappings.TryGetValue(expression, out span);
			}
			span = null;
			return false;
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x00066BC4 File Offset: 0x00064DC4
		private void ApplySpanMapping(DbExpression from, DbExpression to)
		{
			Span span;
			if (this.TryGetSpan(from, out span))
			{
				this.AddSpanMapping(to, span);
			}
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x00066BE8 File Offset: 0x00064DE8
		private void UnifySpanMappings(DbExpression left, DbExpression right, DbExpression to)
		{
			Span span = null;
			Span span2 = null;
			bool flag = this.TryGetSpan(left, out span);
			bool flag2 = this.TryGetSpan(right, out span2);
			if (!flag && !flag2)
			{
				return;
			}
			this.AddSpanMapping(to, Span.CopyUnion(span, span2));
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x00066C24 File Offset: 0x00064E24
		private DbDistinctExpression Distinct(DbExpression argument)
		{
			DbDistinctExpression dbDistinctExpression = argument.Distinct();
			this.ApplySpanMapping(argument, dbDistinctExpression);
			return dbDistinctExpression;
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x00066C44 File Offset: 0x00064E44
		private DbExceptExpression Except(DbExpression left, DbExpression right)
		{
			DbExceptExpression dbExceptExpression = left.Except(right);
			this.ApplySpanMapping(left, dbExceptExpression);
			return dbExceptExpression;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x00066C64 File Offset: 0x00064E64
		private DbExpression Filter(DbExpressionBinding input, DbExpression predicate)
		{
			DbExpression dbExpression = this._orderByLifter.Filter(input, predicate);
			this.ApplySpanMapping(input.Expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x00066C90 File Offset: 0x00064E90
		private DbIntersectExpression Intersect(DbExpression left, DbExpression right)
		{
			DbIntersectExpression dbIntersectExpression = left.Intersect(right);
			this.UnifySpanMappings(left, right, dbIntersectExpression);
			return dbIntersectExpression;
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x00066CB0 File Offset: 0x00064EB0
		private DbExpression Limit(DbExpression argument, DbExpression limit)
		{
			DbExpression dbExpression = this._orderByLifter.Limit(argument, limit);
			this.ApplySpanMapping(argument, dbExpression);
			return dbExpression;
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x00066CD4 File Offset: 0x00064ED4
		private DbExpression OfType(DbExpression argument, TypeUsage ofType)
		{
			DbExpression dbExpression = this._orderByLifter.OfType(argument, ofType);
			this.ApplySpanMapping(argument, dbExpression);
			return dbExpression;
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x00066CF8 File Offset: 0x00064EF8
		private DbExpression Project(DbExpressionBinding input, DbExpression projection)
		{
			DbExpression dbExpression = this._orderByLifter.Project(input, projection);
			if (projection.ExpressionKind == DbExpressionKind.VariableReference && ((DbVariableReferenceExpression)projection).VariableName.Equals(input.VariableName, StringComparison.Ordinal))
			{
				this.ApplySpanMapping(input.Expression, dbExpression);
			}
			return dbExpression;
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00066D44 File Offset: 0x00064F44
		private DbSortExpression Sort(DbExpressionBinding input, IList<DbSortClause> keys)
		{
			DbSortExpression dbSortExpression = input.Sort(keys);
			this.ApplySpanMapping(input.Expression, dbSortExpression);
			return dbSortExpression;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x00066D68 File Offset: 0x00064F68
		private DbExpression Skip(DbExpressionBinding input, DbExpression skipCount)
		{
			DbExpression dbExpression = this._orderByLifter.Skip(input, skipCount);
			this.ApplySpanMapping(input.Expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00066D94 File Offset: 0x00064F94
		private DbUnionAllExpression UnionAll(DbExpression left, DbExpression right)
		{
			DbUnionAllExpression dbUnionAllExpression = left.UnionAll(right);
			this.UnifySpanMappings(left, right, dbUnionAllExpression);
			return dbUnionAllExpression;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x00066DB4 File Offset: 0x00064FB4
		private TypeUsage GetCastTargetType(TypeUsage fromType, Type toClrType, Type fromClrType, bool preserveCastForDateTime)
		{
			if (fromClrType != null && fromClrType.IsGenericType && toClrType.IsGenericType && (fromClrType.GetGenericTypeDefinition() == typeof(ObjectQuery<>) || fromClrType.GetGenericTypeDefinition() == typeof(IOrderedQueryable<>)) && (toClrType.GetGenericTypeDefinition() == typeof(IQueryable<>) || toClrType.GetGenericTypeDefinition() == typeof(IOrderedQueryable<>)) && fromClrType.GetGenericArguments()[0] == toClrType.GetGenericArguments()[0])
			{
				return null;
			}
			TypeUsage typeUsage;
			if (this.TryGetValueLayerType(toClrType, out typeUsage) && ExpressionConverter.CanOmitCast(fromType, typeUsage, preserveCastForDateTime))
			{
				return null;
			}
			typeUsage = ExpressionConverter.ValidateAndAdjustCastTypes(typeUsage, fromType, toClrType, fromClrType);
			return typeUsage;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00066E78 File Offset: 0x00065078
		private static TypeUsage ValidateAndAdjustCastTypes(TypeUsage toType, TypeUsage fromType, Type toClrType, Type fromClrType)
		{
			if (toType == null || !TypeSemantics.IsScalarType(toType) || !TypeSemantics.IsScalarType(fromType))
			{
				throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedCast(ExpressionConverter.DescribeClrType(fromClrType), ExpressionConverter.DescribeClrType(toClrType)));
			}
			PrimitiveTypeKind primitiveTypeKind = Helper.AsPrimitive(fromType.EdmType).PrimitiveTypeKind;
			PrimitiveTypeKind primitiveTypeKind2 = Helper.AsPrimitive(toType.EdmType).PrimitiveTypeKind;
			if (primitiveTypeKind2 == PrimitiveTypeKind.Decimal)
			{
				if (primitiveTypeKind != PrimitiveTypeKind.Byte && primitiveTypeKind - PrimitiveTypeKind.SByte > 3)
				{
					throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedCastToDecimal);
				}
				toType = TypeUsage.CreateDecimalTypeUsage((PrimitiveType)toType.EdmType, 19, 0);
			}
			return toType;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x00066F04 File Offset: 0x00065104
		private static bool CanOmitCast(TypeUsage fromType, TypeUsage toType, bool preserveCastForDateTime)
		{
			bool flag = TypeSemantics.IsPrimitiveType(fromType);
			if (flag && preserveCastForDateTime && ((PrimitiveType)fromType.EdmType).PrimitiveTypeKind == PrimitiveTypeKind.DateTime)
			{
				return false;
			}
			if (ExpressionConverter.TypeUsageEquals(fromType, toType))
			{
				return true;
			}
			if (flag)
			{
				return fromType.EdmType.EdmEquals(toType.EdmType);
			}
			return TypeSemantics.IsSubTypeOf(fromType, toType);
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x00066F5C File Offset: 0x0006515C
		private TypeUsage GetIsOrAsTargetType(TypeUsage fromType, ExpressionType operationType, Type toClrType, Type fromClrType)
		{
			TypeUsage typeUsage;
			if (!this.TryGetValueLayerType(toClrType, out typeUsage) || (!TypeSemantics.IsEntityType(typeUsage) && !TypeSemantics.IsComplexType(typeUsage)))
			{
				throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedIsOrAs(operationType, ExpressionConverter.DescribeClrType(fromClrType), ExpressionConverter.DescribeClrType(toClrType)));
			}
			return typeUsage;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00066FA4 File Offset: 0x000651A4
		private DbExpression TranslateInlineQueryOfT(ObjectQuery inlineQuery)
		{
			if (this._funcletizer.RootContext != inlineQuery.QueryState.ObjectContext)
			{
				throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedDifferentContexts);
			}
			if (this._inlineEntitySqlQueries == null)
			{
				this._inlineEntitySqlQueries = new HashSet<ObjectQuery>();
			}
			bool flag = this._inlineEntitySqlQueries.Add(inlineQuery);
			EntitySqlQueryState entitySqlQueryState = (EntitySqlQueryState)inlineQuery.QueryState;
			ObjectParameterCollection parameters = inlineQuery.QueryState.Parameters;
			DbExpression dbExpression;
			if (!this._funcletizer.IsCompiledQuery || parameters == null || parameters.Count == 0)
			{
				if (flag && parameters != null)
				{
					if (this._parameters == null)
					{
						this._parameters = new List<KeyValuePair<ObjectParameter, QueryParameterExpression>>();
					}
					foreach (ObjectParameter objectParameter in ((IEnumerable<ObjectParameter>)inlineQuery.QueryState.Parameters))
					{
						this._parameters.Add(new KeyValuePair<ObjectParameter, QueryParameterExpression>(objectParameter.ShallowCopy(), null));
					}
				}
				dbExpression = entitySqlQueryState.Parse();
			}
			else
			{
				dbExpression = entitySqlQueryState.Parse();
				dbExpression = ExpressionConverter.ParameterReferenceRemover.RemoveParameterReferences(dbExpression, parameters);
			}
			return dbExpression;
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x000670B4 File Offset: 0x000652B4
		private DbExpression CreateCastExpression(DbExpression source, Type toClrType, Type fromClrType)
		{
			DbExpression dbExpression = this.NormalizeSetSource(source);
			if (source != dbExpression && this.GetCastTargetType(dbExpression.ResultType, toClrType, fromClrType, true) == null)
			{
				return source;
			}
			TypeUsage castTargetType = this.GetCastTargetType(source.ResultType, toClrType, fromClrType, true);
			if (castTargetType == null)
			{
				return source;
			}
			return source.CastTo(castTargetType);
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x000670FC File Offset: 0x000652FC
		private DbExpression TranslateLambda(LambdaExpression lambda, DbExpression input, out DbExpressionBinding binding)
		{
			input = this.NormalizeSetSource(input);
			binding = input.BindAs(this._aliasGenerator.Next());
			return this.TranslateLambda(lambda, binding.Variable);
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x00067128 File Offset: 0x00065328
		private DbExpression TranslateLambda(LambdaExpression lambda, DbExpression input, string bindingName, out DbExpressionBinding binding)
		{
			input = this.NormalizeSetSource(input);
			binding = input.BindAs(bindingName);
			return this.TranslateLambda(lambda, binding.Variable);
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x0006714C File Offset: 0x0006534C
		private DbExpression TranslateLambda(LambdaExpression lambda, DbExpression input, out DbGroupExpressionBinding binding)
		{
			input = this.NormalizeSetSource(input);
			string text = this._aliasGenerator.Next();
			binding = input.GroupBindAs(text, string.Format(CultureInfo.InvariantCulture, "Group{0}", new object[]
			{
				text
			}));
			return this.TranslateLambda(lambda, binding.Variable);
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x000671A0 File Offset: 0x000653A0
		private DbExpression TranslateLambda(LambdaExpression lambda, DbExpression input)
		{
			Binding binding = new Binding(lambda.Parameters[0], input);
			this._bindingContext.PushBindingScope(binding);
			this._ignoreInclude++;
			DbExpression result = this.TranslateExpression(lambda.Body);
			this._ignoreInclude--;
			this._bindingContext.PopBindingScope();
			return result;
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x00067204 File Offset: 0x00065404
		private DbExpression NormalizeSetSource(DbExpression input)
		{
			Span span;
			if (input.ExpressionKind == DbExpressionKind.Project && !this.TryGetSpan(input, out span))
			{
				DbProjectExpression dbProjectExpression = (DbProjectExpression)input;
				if (dbProjectExpression.Projection == dbProjectExpression.Input.Variable)
				{
					input = dbProjectExpression.Input.Expression;
				}
			}
			InitializerMetadata initializerMetadata;
			if (InitializerMetadata.TryGetInitializerMetadata(input.ResultType, out initializerMetadata))
			{
				if (initializerMetadata.Kind == InitializerMetadataKind.Grouping)
				{
					input = input.Property("Group");
				}
				else if (initializerMetadata.Kind == InitializerMetadataKind.EntityCollection)
				{
					input = input.Property("Elements");
				}
			}
			return input;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x0006728C File Offset: 0x0006548C
		private LambdaExpression GetLambdaExpression(MethodCallExpression callExpression, int argumentOrdinal)
		{
			Expression argument = callExpression.Arguments[argumentOrdinal];
			return (LambdaExpression)this.GetLambdaExpression(argument);
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x000672B2 File Offset: 0x000654B2
		private Expression GetLambdaExpression(Expression argument)
		{
			if (ExpressionType.Lambda == argument.NodeType)
			{
				return argument;
			}
			if (ExpressionType.Quote == argument.NodeType)
			{
				return this.GetLambdaExpression(((UnaryExpression)argument).Operand);
			}
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UnexpectedLinqLambdaExpressionFormat);
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000672E6 File Offset: 0x000654E6
		private DbExpression TranslateSet(Expression linq)
		{
			return this.NormalizeSetSource(this.TranslateExpression(linq));
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x000672F8 File Offset: 0x000654F8
		private DbExpression TranslateExpression(Expression linq)
		{
			DbExpression result;
			if (!this._bindingContext.TryGetBoundExpression(linq, out result))
			{
				ExpressionConverter.Translator translator;
				if (!ExpressionConverter.s_translators.TryGetValue(linq.NodeType, out translator))
				{
					throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UnknownLinqNodeType, -1, linq.NodeType.ToString());
				}
				result = translator.Translate(this, linq);
			}
			return result;
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00067358 File Offset: 0x00065558
		private DbExpression AlignTypes(DbExpression cqt, Type toClrType)
		{
			Type fromClrType = null;
			TypeUsage castTargetType = this.GetCastTargetType(cqt.ResultType, toClrType, fromClrType, false);
			if (castTargetType != null)
			{
				return cqt.CastTo(castTargetType);
			}
			return cqt;
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00067384 File Offset: 0x00065584
		private void CheckInitializerType(Type type)
		{
			TypeUsage typeUsage;
			if (this._funcletizer.RootContext.Perspective.TryGetType(type, out typeUsage))
			{
				BuiltInTypeKind builtInTypeKind = typeUsage.EdmType.BuiltInTypeKind;
				if (BuiltInTypeKind.EntityType == builtInTypeKind || BuiltInTypeKind.ComplexType == builtInTypeKind)
				{
					throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedNominalType(typeUsage.EdmType.FullName));
				}
			}
			if (TypeSystem.IsSequenceType(type))
			{
				throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedEnumerableType(ExpressionConverter.DescribeClrType(type)));
			}
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x000673F0 File Offset: 0x000655F0
		private static bool TypeUsageEquals(TypeUsage left, TypeUsage right)
		{
			if (left.EdmType.EdmEquals(right.EdmType))
			{
				return true;
			}
			if (BuiltInTypeKind.CollectionType == left.EdmType.BuiltInTypeKind && BuiltInTypeKind.CollectionType == right.EdmType.BuiltInTypeKind)
			{
				return ExpressionConverter.TypeUsageEquals(((CollectionType)left.EdmType).TypeUsage, ((CollectionType)right.EdmType).TypeUsage);
			}
			return BuiltInTypeKind.PrimitiveType == left.EdmType.BuiltInTypeKind && BuiltInTypeKind.PrimitiveType == right.EdmType.BuiltInTypeKind && ((PrimitiveType)left.EdmType).ClrEquivalentType.Equals(((PrimitiveType)right.EdmType).ClrEquivalentType);
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0006749C File Offset: 0x0006569C
		private TypeUsage GetValueLayerType(Type linqType)
		{
			TypeUsage result;
			if (!this.TryGetValueLayerType(linqType, out result))
			{
				throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedType(linqType));
			}
			return result;
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x000674C4 File Offset: 0x000656C4
		private bool TryGetValueLayerType(Type linqType, out TypeUsage type)
		{
			Type type2 = TypeSystem.GetNonNullableType(linqType);
			if (type2.IsEnum && this.EdmItemCollection.EdmVersion < 3.0)
			{
				type2 = type2.GetEnumUnderlyingType();
			}
			PrimitiveTypeKind primitiveTypeKind;
			if (ClrProviderManifest.Instance.TryGetPrimitiveTypeKind(type2, out primitiveTypeKind))
			{
				type = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(primitiveTypeKind);
				return true;
			}
			Type elementType = TypeSystem.GetElementType(type2);
			TypeUsage elementType2;
			if (elementType != type2 && this.TryGetValueLayerType(elementType, out elementType2))
			{
				type = TypeHelpers.CreateCollectionTypeUsage(elementType2);
				return true;
			}
			this._perspective.MetadataWorkspace.ImplicitLoadAssemblyForType(linqType, null);
			if (!this._perspective.TryGetTypeByName(type2.FullName, false, out type) && type2.IsEnum && ClrProviderManifest.Instance.TryGetPrimitiveTypeKind(type2.GetEnumUnderlyingType(), out primitiveTypeKind))
			{
				type = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(primitiveTypeKind);
			}
			return type != null;
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00067598 File Offset: 0x00065798
		private static void VerifyTypeSupportedForComparison(Type clrType, TypeUsage edmType, Stack<EdmMember> memberPath)
		{
			BuiltInTypeKind builtInTypeKind = edmType.EdmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.PrimitiveType)
			{
				if (builtInTypeKind - BuiltInTypeKind.EntityType > 1 && builtInTypeKind != BuiltInTypeKind.PrimitiveType)
				{
					goto IL_5B;
				}
			}
			else if (builtInTypeKind != BuiltInTypeKind.RefType)
			{
				if (builtInTypeKind != BuiltInTypeKind.RowType)
				{
					goto IL_5B;
				}
				InitializerMetadata initializerMetadata;
				if (!InitializerMetadata.TryGetInitializerMetadata(edmType, out initializerMetadata) || initializerMetadata.Kind == InitializerMetadataKind.ProjectionInitializer || initializerMetadata.Kind == InitializerMetadataKind.ProjectionNew)
				{
					ExpressionConverter.VerifyRowTypeSupportedForComparison(clrType, (RowType)edmType.EdmType, memberPath);
					return;
				}
				goto IL_5B;
			}
			return;
			IL_5B:
			if (memberPath == null)
			{
				throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedComparison(ExpressionConverter.DescribeClrType(clrType)));
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (EdmMember edmMember in memberPath)
			{
				stringBuilder.Append(Strings.ELinq_UnsupportedRowMemberComparison(edmMember.Name));
			}
			stringBuilder.Append(Strings.ELinq_UnsupportedRowTypeComparison(ExpressionConverter.DescribeClrType(clrType)));
			throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedRowComparison(stringBuilder.ToString()));
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x0006768C File Offset: 0x0006588C
		private static void VerifyRowTypeSupportedForComparison(Type clrType, RowType rowType, Stack<EdmMember> memberPath)
		{
			foreach (EdmMember edmMember in rowType.Properties)
			{
				if (memberPath == null)
				{
					memberPath = new Stack<EdmMember>();
				}
				memberPath.Push(edmMember);
				ExpressionConverter.VerifyTypeSupportedForComparison(clrType, edmMember.TypeUsage, memberPath);
				memberPath.Pop();
			}
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x00067700 File Offset: 0x00065900
		internal static string DescribeClrType(Type clrType)
		{
			string name = clrType.Name;
			if (ExpressionConverter.IsCSharpGeneratedClass(name, "DisplayClass") || ExpressionConverter.IsVBGeneratedClass(name, "Closure"))
			{
				return Strings.ELinq_ClosureType;
			}
			if (ExpressionConverter.IsCSharpGeneratedClass(name, "AnonymousType") || ExpressionConverter.IsVBGeneratedClass(name, "AnonymousType"))
			{
				return Strings.ELinq_AnonymousType;
			}
			string str = string.Empty;
			if (!string.IsNullOrEmpty(clrType.Namespace))
			{
				str = str + clrType.Namespace + ".";
			}
			return str + clrType.Name;
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x00067787 File Offset: 0x00065987
		private static bool IsCSharpGeneratedClass(string typeName, string pattern)
		{
			return typeName.Contains("<>") && typeName.Contains("__") && typeName.Contains(pattern);
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x000677AC File Offset: 0x000659AC
		private static bool IsVBGeneratedClass(string typeName, string pattern)
		{
			return typeName.Contains("_") && typeName.Contains("$") && typeName.Contains(pattern);
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000677D1 File Offset: 0x000659D1
		private DbExpression CreateIsNullExpression(DbExpression operand, Type operandClrType)
		{
			ExpressionConverter.VerifyTypeSupportedForComparison(operandClrType, operand.ResultType, null);
			return operand.IsNull();
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x000677E8 File Offset: 0x000659E8
		private DbExpression CreateEqualsExpression(DbExpression left, DbExpression right, ExpressionConverter.EqualsPattern pattern, Type leftClrType, Type rightClrType)
		{
			ExpressionConverter.VerifyTypeSupportedForComparison(leftClrType, left.ResultType, null);
			ExpressionConverter.VerifyTypeSupportedForComparison(rightClrType, right.ResultType, null);
			TypeUsage resultType = left.ResultType;
			TypeUsage resultType2 = right.ResultType;
			TypeUsage typeUsage;
			if (resultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.RefType && resultType2.EdmType.BuiltInTypeKind == BuiltInTypeKind.RefType && !TypeSemantics.TryGetCommonType(resultType, resultType2, out typeUsage))
			{
				RefType refType = left.ResultType.EdmType as RefType;
				RefType refType2 = right.ResultType.EdmType as RefType;
				throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedRefComparison(refType.ElementType.FullName, refType2.ElementType.FullName));
			}
			return this.RecursivelyRewriteEqualsExpression(left, right, pattern);
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x00067898 File Offset: 0x00065A98
		private DbExpression RecursivelyRewriteEqualsExpression(DbExpression left, DbExpression right, ExpressionConverter.EqualsPattern pattern)
		{
			RowType rowType = left.ResultType.EdmType as RowType;
			RowType rowType2 = left.ResultType.EdmType as RowType;
			if (rowType == null && rowType2 == null)
			{
				return this.ImplementEquality(left, right, pattern);
			}
			if (rowType != null && rowType2 != null && rowType.EdmEquals(rowType2))
			{
				DbExpression dbExpression = null;
				foreach (EdmProperty propertyMetadata in rowType.Properties)
				{
					DbPropertyExpression left2 = left.Property(propertyMetadata);
					DbPropertyExpression right2 = right.Property(propertyMetadata);
					DbExpression dbExpression2 = this.RecursivelyRewriteEqualsExpression(left2, right2, pattern);
					if (dbExpression == null)
					{
						dbExpression = dbExpression2;
					}
					else
					{
						dbExpression = dbExpression.And(dbExpression2);
					}
				}
				return dbExpression;
			}
			return DbExpressionBuilder.False;
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x00067964 File Offset: 0x00065B64
		private DbExpression ImplementEquality(DbExpression left, DbExpression right, ExpressionConverter.EqualsPattern pattern)
		{
			DbExpressionKind expressionKind = left.ExpressionKind;
			if (expressionKind != DbExpressionKind.Constant)
			{
				if (expressionKind != DbExpressionKind.Null)
				{
					DbExpressionKind expressionKind2 = right.ExpressionKind;
					if (expressionKind2 == DbExpressionKind.Constant)
					{
						return this.ImplementEqualityConstantAndUnknown((DbConstantExpression)right, left, pattern);
					}
					if (expressionKind2 != DbExpressionKind.Null)
					{
						return this.ImplementEqualityUnknownArguments(left, right, pattern);
					}
					return left.IsNull();
				}
				else
				{
					DbExpressionKind expressionKind3 = right.ExpressionKind;
					if (expressionKind3 == DbExpressionKind.Constant)
					{
						return DbExpressionBuilder.False;
					}
					if (expressionKind3 != DbExpressionKind.Null)
					{
						return right.IsNull();
					}
					return DbExpressionBuilder.True;
				}
			}
			else
			{
				DbExpressionKind expressionKind4 = right.ExpressionKind;
				if (expressionKind4 == DbExpressionKind.Constant)
				{
					return left.Equal(right);
				}
				if (expressionKind4 != DbExpressionKind.Null)
				{
					return this.ImplementEqualityConstantAndUnknown((DbConstantExpression)left, right, pattern);
				}
				return DbExpressionBuilder.False;
			}
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00067A08 File Offset: 0x00065C08
		private DbExpression ImplementEqualityConstantAndUnknown(DbConstantExpression constant, DbExpression unknown, ExpressionConverter.EqualsPattern pattern)
		{
			if (pattern <= ExpressionConverter.EqualsPattern.PositiveNullEqualityNonComposable)
			{
				return constant.Equal(unknown);
			}
			if (pattern != ExpressionConverter.EqualsPattern.PositiveNullEqualityComposable)
			{
				return null;
			}
			if (!this._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior)
			{
				return constant.Equal(unknown);
			}
			return constant.Equal(unknown).And(unknown.IsNull().Not());
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00067A60 File Offset: 0x00065C60
		private DbExpression ImplementEqualityUnknownArguments(DbExpression left, DbExpression right, ExpressionConverter.EqualsPattern pattern)
		{
			switch (pattern)
			{
			case ExpressionConverter.EqualsPattern.Store:
				return left.Equal(right);
			case ExpressionConverter.EqualsPattern.PositiveNullEqualityNonComposable:
				return left.Equal(right).Or(left.IsNull().And(right.IsNull()));
			case ExpressionConverter.EqualsPattern.PositiveNullEqualityComposable:
			{
				DbComparisonExpression left2 = left.Equal(right);
				DbAndExpression right2 = left.IsNull().And(right.IsNull());
				if (!this._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior)
				{
					return left2.Or(right2);
				}
				DbOrExpression argument = left.IsNull().Or(right.IsNull());
				return left2.And(argument.Not()).Or(right2);
			}
			default:
				return null;
			}
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x00067B0C File Offset: 0x00065D0C
		private DbExpression TranslateFunctionIntoLike(MethodCallExpression call, bool insertPercentAtStart, bool insertPercentAtEnd, Func<ExpressionConverter, MethodCallExpression, DbExpression, DbExpression, DbExpression> defaultTranslator)
		{
			char c;
			bool flag = this.ProviderManifest.SupportsEscapingLikeArgument(out c);
			bool flag2 = false;
			bool flag3 = true;
			Expression expression = call.Arguments[0];
			Expression @object = call.Object;
			QueryParameterExpression queryParameterExpression = expression as QueryParameterExpression;
			if (flag && queryParameterExpression != null)
			{
				flag2 = true;
				bool specifyEscapeDummy;
				expression = queryParameterExpression.EscapeParameterForLike((string input) => this.PreparePattern(input, insertPercentAtStart, insertPercentAtEnd, out specifyEscapeDummy));
			}
			DbExpression dbExpression = this.TranslateExpression(expression);
			DbExpression dbExpression2 = this.TranslateExpression(@object);
			if (flag && dbExpression.ExpressionKind == DbExpressionKind.Constant)
			{
				flag2 = true;
				DbConstantExpression dbConstantExpression = (DbConstantExpression)dbExpression;
				string value = this.PreparePattern((string)dbConstantExpression.Value, insertPercentAtStart, insertPercentAtEnd, out flag3);
				dbExpression = dbConstantExpression.ResultType.Constant(value);
			}
			DbExpression result;
			if (flag2)
			{
				if (flag3)
				{
					DbConstantExpression escape = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.String).Constant(new string(new char[]
					{
						c
					}));
					result = dbExpression2.Like(dbExpression, escape);
				}
				else
				{
					result = dbExpression2.Like(dbExpression);
				}
			}
			else
			{
				result = defaultTranslator(this, call, dbExpression, dbExpression2);
			}
			return result;
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00067C38 File Offset: 0x00065E38
		private string PreparePattern(string patternValue, bool insertPercentAtStart, bool insertPercentAtEnd, out bool specifyEscape)
		{
			if (patternValue == null)
			{
				specifyEscape = false;
				return null;
			}
			string text = this.ProviderManifest.EscapeLikeArgument(patternValue);
			if (text == null)
			{
				throw EntityUtil.ProviderIncompatible(Strings.ProviderEscapeLikeArgumentReturnedNull);
			}
			specifyEscape = (patternValue != text);
			StringBuilder stringBuilder = new StringBuilder();
			if (insertPercentAtStart)
			{
				stringBuilder.Append("%");
			}
			stringBuilder.Append(text);
			if (insertPercentAtEnd)
			{
				stringBuilder.Append("%");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00067CA8 File Offset: 0x00065EA8
		private DbFunctionExpression TranslateIntoCanonicalFunction(string functionName, Expression Expression, params Expression[] linqArguments)
		{
			DbExpression[] array = new DbExpression[linqArguments.Length];
			for (int i = 0; i < linqArguments.Length; i++)
			{
				array[i] = this.TranslateExpression(linqArguments[i]);
			}
			return this.CreateCanonicalFunction(functionName, Expression, array);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00067CE4 File Offset: 0x00065EE4
		private DbFunctionExpression CreateCanonicalFunction(string functionName, Expression Expression, params DbExpression[] translatedArguments)
		{
			List<TypeUsage> list = new List<TypeUsage>(translatedArguments.Length);
			foreach (DbExpression dbExpression in translatedArguments)
			{
				list.Add(dbExpression.ResultType);
			}
			EdmFunction function = this.FindCanonicalFunction(functionName, list, false, Expression);
			return function.Invoke(translatedArguments);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x00067D2E File Offset: 0x00065F2E
		private EdmFunction FindCanonicalFunction(string functionName, IList<TypeUsage> argumentTypes, bool isGroupAggregateFunction, Expression Expression)
		{
			return this.FindFunction("Edm", functionName, argumentTypes, isGroupAggregateFunction, Expression);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00067D40 File Offset: 0x00065F40
		private EdmFunction FindFunction(string namespaceName, string functionName, IList<TypeUsage> argumentTypes, bool isGroupAggregateFunction, Expression Expression)
		{
			IList<EdmFunction> functionsMetadata;
			if (!this._perspective.TryGetFunctionByName(namespaceName, functionName, false, out functionsMetadata))
			{
				ExpressionConverter.ThrowUnresolvableFunction(Expression);
			}
			bool flag;
			EdmFunction edmFunction = FunctionOverloadResolver.ResolveFunctionOverloads(functionsMetadata, argumentTypes, isGroupAggregateFunction, out flag);
			if (flag || edmFunction == null)
			{
				ExpressionConverter.ThrowUnresolvableFunctionOverload(Expression, flag);
			}
			return edmFunction;
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x00067D84 File Offset: 0x00065F84
		private static void ThrowUnresolvableFunction(Expression Expression)
		{
			if (Expression.NodeType == ExpressionType.Call)
			{
				MethodInfo method = ((MethodCallExpression)Expression).Method;
				throw EntityUtil.NotSupported(Strings.ELinq_UnresolvableFunctionForMethod(method, method.DeclaringType));
			}
			if (Expression.NodeType == ExpressionType.MemberAccess)
			{
				string text;
				Type type;
				MemberInfo memberInfo = TypeSystem.PropertyOrField(((MemberExpression)Expression).Member, out text, out type);
				throw EntityUtil.NotSupported(Strings.ELinq_UnresolvableFunctionForMember(memberInfo, memberInfo.DeclaringType));
			}
			throw EntityUtil.NotSupported(Strings.ELinq_UnresolvableFunctionForExpression(Expression.NodeType));
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x00067E00 File Offset: 0x00066000
		private static void ThrowUnresolvableFunctionOverload(Expression Expression, bool isAmbiguous)
		{
			if (Expression.NodeType == ExpressionType.Call)
			{
				MethodInfo method = ((MethodCallExpression)Expression).Method;
				if (isAmbiguous)
				{
					throw EntityUtil.NotSupported(Strings.ELinq_UnresolvableFunctionForMethodAmbiguousMatch(method, method.DeclaringType));
				}
				throw EntityUtil.NotSupported(Strings.ELinq_UnresolvableFunctionForMethodNotFound(method, method.DeclaringType));
			}
			else
			{
				if (Expression.NodeType == ExpressionType.MemberAccess)
				{
					string text;
					Type type;
					MemberInfo memberInfo = TypeSystem.PropertyOrField(((MemberExpression)Expression).Member, out text, out type);
					throw EntityUtil.NotSupported(Strings.ELinq_UnresolvableStoreFunctionForMember(memberInfo, memberInfo.DeclaringType));
				}
				throw EntityUtil.NotSupported(Strings.ELinq_UnresolvableStoreFunctionForExpression(Expression.NodeType));
			}
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x00067E90 File Offset: 0x00066090
		private DbNewInstanceExpression CreateNewRowExpression(List<KeyValuePair<string, DbExpression>> columns, InitializerMetadata initializerMetadata)
		{
			List<DbExpression> list = new List<DbExpression>(columns.Count);
			List<EdmProperty> list2 = new List<EdmProperty>(columns.Count);
			for (int i = 0; i < columns.Count; i++)
			{
				KeyValuePair<string, DbExpression> keyValuePair = columns[i];
				list.Add(keyValuePair.Value);
				list2.Add(new EdmProperty(keyValuePair.Key, keyValuePair.Value.ResultType));
			}
			RowType edmType = new RowType(list2, initializerMetadata);
			TypeUsage instanceType = TypeUsage.Create(edmType);
			return instanceType.New(list);
		}

		// Token: 0x04000BD9 RID: 3033
		private readonly Funcletizer _funcletizer;

		// Token: 0x04000BDA RID: 3034
		private readonly Perspective _perspective;

		// Token: 0x04000BDB RID: 3035
		private readonly Expression _expression;

		// Token: 0x04000BDC RID: 3036
		private readonly BindingContext _bindingContext;

		// Token: 0x04000BDD RID: 3037
		private Func<bool> _recompileRequired;

		// Token: 0x04000BDE RID: 3038
		private List<KeyValuePair<ObjectParameter, QueryParameterExpression>> _parameters;

		// Token: 0x04000BDF RID: 3039
		private Dictionary<DbExpression, Span> _spanMappings;

		// Token: 0x04000BE0 RID: 3040
		private MergeOption? _mergeOption;

		// Token: 0x04000BE1 RID: 3041
		private Dictionary<Type, InitializerMetadata> _initializers;

		// Token: 0x04000BE2 RID: 3042
		private Span _span;

		// Token: 0x04000BE3 RID: 3043
		private HashSet<ObjectQuery> _inlineEntitySqlQueries;

		// Token: 0x04000BE4 RID: 3044
		private int _ignoreInclude;

		// Token: 0x04000BE5 RID: 3045
		private readonly AliasGenerator _aliasGenerator = new AliasGenerator("LQ", 0);

		// Token: 0x04000BE6 RID: 3046
		private readonly ExpressionConverter.OrderByLifter _orderByLifter;

		// Token: 0x04000BE7 RID: 3047
		private const string s_visualBasicAssemblyFullName = "Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000BE8 RID: 3048
		private static readonly Dictionary<ExpressionType, ExpressionConverter.Translator> s_translators = ExpressionConverter.InitializeTranslators();

		// Token: 0x04000BE9 RID: 3049
		internal const string s_entityCollectionCountPropertyName = "Count";

		// Token: 0x04000BEA RID: 3050
		internal const string s_nullableHasValuePropertyName = "HasValue";

		// Token: 0x04000BEB RID: 3051
		internal const string s_nullableValuePropertyName = "Value";

		// Token: 0x04000BEC RID: 3052
		internal const string KeyColumnName = "Key";

		// Token: 0x04000BED RID: 3053
		internal const string GroupColumnName = "Group";

		// Token: 0x04000BEE RID: 3054
		internal const string EntityCollectionOwnerColumnName = "Owner";

		// Token: 0x04000BEF RID: 3055
		internal const string EntityCollectionElementsColumnName = "Elements";

		// Token: 0x04000BF0 RID: 3056
		internal const string EdmNamespaceName = "Edm";

		// Token: 0x04000BF1 RID: 3057
		private const string Concat = "Concat";

		// Token: 0x04000BF2 RID: 3058
		private const string IndexOf = "IndexOf";

		// Token: 0x04000BF3 RID: 3059
		private const string Length = "Length";

		// Token: 0x04000BF4 RID: 3060
		private const string Right = "Right";

		// Token: 0x04000BF5 RID: 3061
		private const string Substring = "Substring";

		// Token: 0x04000BF6 RID: 3062
		private const string ToUpper = "ToUpper";

		// Token: 0x04000BF7 RID: 3063
		private const string ToLower = "ToLower";

		// Token: 0x04000BF8 RID: 3064
		private const string Trim = "Trim";

		// Token: 0x04000BF9 RID: 3065
		private const string LTrim = "LTrim";

		// Token: 0x04000BFA RID: 3066
		private const string RTrim = "RTrim";

		// Token: 0x04000BFB RID: 3067
		private const string Reverse = "Reverse";

		// Token: 0x04000BFC RID: 3068
		private const string BitwiseAnd = "BitwiseAnd";

		// Token: 0x04000BFD RID: 3069
		private const string BitwiseOr = "BitwiseOr";

		// Token: 0x04000BFE RID: 3070
		private const string BitwiseNot = "BitwiseNot";

		// Token: 0x04000BFF RID: 3071
		private const string BitwiseXor = "BitwiseXor";

		// Token: 0x04000C00 RID: 3072
		private const string CurrentUtcDateTime = "CurrentUtcDateTime";

		// Token: 0x04000C01 RID: 3073
		private const string CurrentDateTimeOffset = "CurrentDateTimeOffset";

		// Token: 0x04000C02 RID: 3074
		private const string CurrentDateTime = "CurrentDateTime";

		// Token: 0x04000C03 RID: 3075
		private const string Year = "Year";

		// Token: 0x04000C04 RID: 3076
		private const string Month = "Month";

		// Token: 0x04000C05 RID: 3077
		private const string Day = "Day";

		// Token: 0x04000C06 RID: 3078
		private const string Hour = "Hour";

		// Token: 0x04000C07 RID: 3079
		private const string Minute = "Minute";

		// Token: 0x04000C08 RID: 3080
		private const string Second = "Second";

		// Token: 0x04000C09 RID: 3081
		private const string Millisecond = "Millisecond";

		// Token: 0x04000C0A RID: 3082
		private const string AsUnicode = "AsUnicode";

		// Token: 0x04000C0B RID: 3083
		private const string AsNonUnicode = "AsNonUnicode";

		// Token: 0x020004D1 RID: 1233
		private sealed class MethodCallTranslator : ExpressionConverter.TypedTranslator<MethodCallExpression>
		{
			// Token: 0x06003CFD RID: 15613 RVA: 0x000E47AB File Offset: 0x000E29AB
			internal MethodCallTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Call
			})
			{
			}

			// Token: 0x06003CFE RID: 15614 RVA: 0x000E47C0 File Offset: 0x000E29C0
			protected override DbExpression TypedTranslate(ExpressionConverter parent, MethodCallExpression linq)
			{
				SequenceMethod sequenceMethod;
				ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator sequenceMethodTranslator;
				if (ReflectionUtil.TryIdentifySequenceMethod(linq.Method, out sequenceMethod) && ExpressionConverter.MethodCallTranslator.s_sequenceTranslators.TryGetValue(sequenceMethod, out sequenceMethodTranslator))
				{
					return sequenceMethodTranslator.Translate(parent, linq, sequenceMethod);
				}
				ExpressionConverter.MethodCallTranslator.CallTranslator callTranslator;
				if (ExpressionConverter.MethodCallTranslator.TryGetCallTranslator(linq.Method, out callTranslator))
				{
					return callTranslator.Translate(parent, linq);
				}
				ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator objectQueryCallTranslator;
				if (ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator.IsCandidateMethod(linq.Method) && ExpressionConverter.MethodCallTranslator.s_objectQueryTranslators.TryGetValue(linq.Method.Name, out objectQueryCallTranslator))
				{
					return objectQueryCallTranslator.Translate(parent, linq);
				}
				EdmFunctionAttribute edmFunctionAttribute = linq.Method.GetCustomAttributes(typeof(EdmFunctionAttribute), false).Cast<EdmFunctionAttribute>().FirstOrDefault<EdmFunctionAttribute>();
				if (edmFunctionAttribute != null)
				{
					return ExpressionConverter.MethodCallTranslator.s_functionCallTranslator.TranslateFunctionCall(parent, linq, edmFunctionAttribute);
				}
				string name = linq.Method.Name;
				Type[] array;
				if (name == "Contains" && linq.Method.GetParameters().Count<ParameterInfo>() == 1 && linq.Method.ReturnType.Equals(typeof(bool)) && linq.Method.IsImplementationOfGenericInterfaceMethod(typeof(ICollection<>), out array))
				{
					return ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContains(parent, linq.Object, linq.Arguments[0]);
				}
				return ExpressionConverter.MethodCallTranslator.s_defaultTranslator.Translate(parent, linq);
			}

			// Token: 0x06003CFF RID: 15615 RVA: 0x000E48F8 File Offset: 0x000E2AF8
			private static Dictionary<MethodInfo, ExpressionConverter.MethodCallTranslator.CallTranslator> InitializeMethodTranslators()
			{
				Dictionary<MethodInfo, ExpressionConverter.MethodCallTranslator.CallTranslator> dictionary = new Dictionary<MethodInfo, ExpressionConverter.MethodCallTranslator.CallTranslator>();
				foreach (ExpressionConverter.MethodCallTranslator.CallTranslator callTranslator in ExpressionConverter.MethodCallTranslator.GetCallTranslators())
				{
					foreach (MethodInfo key in callTranslator.Methods)
					{
						dictionary.Add(key, callTranslator);
					}
				}
				return dictionary;
			}

			// Token: 0x06003D00 RID: 15616 RVA: 0x000E4984 File Offset: 0x000E2B84
			private static Dictionary<SequenceMethod, ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator> InitializeSequenceMethodTranslators()
			{
				Dictionary<SequenceMethod, ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator> dictionary = new Dictionary<SequenceMethod, ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator>();
				foreach (ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator sequenceMethodTranslator in ExpressionConverter.MethodCallTranslator.GetSequenceMethodTranslators())
				{
					foreach (SequenceMethod key in sequenceMethodTranslator.Methods)
					{
						dictionary.Add(key, sequenceMethodTranslator);
					}
				}
				return dictionary;
			}

			// Token: 0x06003D01 RID: 15617 RVA: 0x000E4A10 File Offset: 0x000E2C10
			private static Dictionary<string, ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator> InitializeObjectQueryTranslators()
			{
				Dictionary<string, ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator> dictionary = new Dictionary<string, ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator>(StringComparer.Ordinal);
				foreach (ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator objectQueryCallTranslator in ExpressionConverter.MethodCallTranslator.GetObjectQueryCallTranslators())
				{
					dictionary[objectQueryCallTranslator.MethodName] = objectQueryCallTranslator;
				}
				return dictionary;
			}

			// Token: 0x06003D02 RID: 15618 RVA: 0x000E4A70 File Offset: 0x000E2C70
			private static bool TryGetCallTranslator(MethodInfo methodInfo, out ExpressionConverter.MethodCallTranslator.CallTranslator callTranslator)
			{
				if (ExpressionConverter.MethodCallTranslator.s_methodTranslators.TryGetValue(methodInfo, out callTranslator))
				{
					return true;
				}
				if ("Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" == methodInfo.DeclaringType.Assembly.FullName)
				{
					object obj = ExpressionConverter.MethodCallTranslator.s_vbInitializerLock;
					lock (obj)
					{
						if (!ExpressionConverter.MethodCallTranslator.s_vbMethodsInitialized)
						{
							ExpressionConverter.MethodCallTranslator.InitializeVBMethods(methodInfo.DeclaringType.Assembly);
							ExpressionConverter.MethodCallTranslator.s_vbMethodsInitialized = true;
						}
						return ExpressionConverter.MethodCallTranslator.s_methodTranslators.TryGetValue(methodInfo, out callTranslator);
					}
				}
				callTranslator = null;
				return false;
			}

			// Token: 0x06003D03 RID: 15619 RVA: 0x000E4B08 File Offset: 0x000E2D08
			private static void InitializeVBMethods(Assembly vbAssembly)
			{
				foreach (ExpressionConverter.MethodCallTranslator.CallTranslator callTranslator in ExpressionConverter.MethodCallTranslator.GetVisualBasicCallTranslators(vbAssembly))
				{
					foreach (MethodInfo key in callTranslator.Methods)
					{
						ExpressionConverter.MethodCallTranslator.s_methodTranslators.Add(key, callTranslator);
					}
				}
			}

			// Token: 0x06003D04 RID: 15620 RVA: 0x000E4B90 File Offset: 0x000E2D90
			private static IEnumerable<ExpressionConverter.MethodCallTranslator.CallTranslator> GetVisualBasicCallTranslators(Assembly vbAssembly)
			{
				yield return new ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionDefaultTranslator(vbAssembly);
				yield return new ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator(vbAssembly);
				yield return new ExpressionConverter.MethodCallTranslator.VBDatePartTranslator(vbAssembly);
				yield break;
			}

			// Token: 0x06003D05 RID: 15621 RVA: 0x000E4BA0 File Offset: 0x000E2DA0
			private static IEnumerable<ExpressionConverter.MethodCallTranslator.CallTranslator> GetCallTranslators()
			{
				return new ExpressionConverter.MethodCallTranslator.CallTranslator[]
				{
					new ExpressionConverter.MethodCallTranslator.CanonicalFunctionDefaultTranslator(),
					new ExpressionConverter.MethodCallTranslator.AsUnicodeFunctionTranslator(),
					new ExpressionConverter.MethodCallTranslator.AsNonUnicodeFunctionTranslator(),
					new ExpressionConverter.MethodCallTranslator.MathPowerTranslator(),
					new ExpressionConverter.MethodCallTranslator.GuidNewGuidTranslator(),
					new ExpressionConverter.MethodCallTranslator.StringContainsTranslator(),
					new ExpressionConverter.MethodCallTranslator.StartsWithTranslator(),
					new ExpressionConverter.MethodCallTranslator.EndsWithTranslator(),
					new ExpressionConverter.MethodCallTranslator.IndexOfTranslator(),
					new ExpressionConverter.MethodCallTranslator.SubstringTranslator(),
					new ExpressionConverter.MethodCallTranslator.RemoveTranslator(),
					new ExpressionConverter.MethodCallTranslator.InsertTranslator(),
					new ExpressionConverter.MethodCallTranslator.IsNullOrEmptyTranslator(),
					new ExpressionConverter.MethodCallTranslator.StringConcatTranslator(),
					new ExpressionConverter.MethodCallTranslator.TrimTranslator(),
					new ExpressionConverter.MethodCallTranslator.TrimStartTranslator(),
					new ExpressionConverter.MethodCallTranslator.TrimEndTranslator(),
					new ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator()
				};
			}

			// Token: 0x06003D06 RID: 15622 RVA: 0x000E4C4D File Offset: 0x000E2E4D
			private static IEnumerable<ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator> GetSequenceMethodTranslators()
			{
				yield return new ExpressionConverter.MethodCallTranslator.ConcatTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.UnionTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.IntersectTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ExceptTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.DistinctTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.WhereTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SelectTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.OrderByTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.OrderByDescendingTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ThenByTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ThenByDescendingTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SelectManyTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.AnyTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.AnyPredicateTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.AllTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.JoinTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.GroupByTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.MaxTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.MinTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.AverageTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SumTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.CountTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.LongCountTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.CastMethodTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.GroupJoinTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.OfTypeTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.PassthroughTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.DefaultIfEmptyTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.FirstTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.FirstPredicateTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.FirstOrDefaultTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.FirstOrDefaultPredicateTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.TakeTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SkipTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SingleTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SinglePredicateTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SingleOrDefaultTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.SingleOrDefaultPredicateTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ContainsTranslator();
				yield break;
			}

			// Token: 0x06003D07 RID: 15623 RVA: 0x000E4C56 File Offset: 0x000E2E56
			private static IEnumerable<ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator> GetObjectQueryCallTranslators()
			{
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderDistinctTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderExceptTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderFirstTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryIncludeTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderIntersectTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderOfTypeTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderUnionTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryMergeAsTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryIncludeSpanTranslator();
				yield break;
			}

			// Token: 0x06003D08 RID: 15624 RVA: 0x000E4C60 File Offset: 0x000E2E60
			private static bool IsTrivialRename(LambdaExpression selectorLambda, ExpressionConverter converter, out string leftName, out string rightName, out InitializerMetadata initializerMetadata)
			{
				leftName = null;
				rightName = null;
				initializerMetadata = null;
				if (selectorLambda.Parameters.Count != 2 || selectorLambda.Body.NodeType != ExpressionType.New)
				{
					return false;
				}
				NewExpression newExpression = (NewExpression)selectorLambda.Body;
				if (newExpression.Arguments.Count != 2)
				{
					return false;
				}
				if (newExpression.Arguments[0] != selectorLambda.Parameters[0] || newExpression.Arguments[1] != selectorLambda.Parameters[1])
				{
					return false;
				}
				leftName = newExpression.Members[0].Name;
				rightName = newExpression.Members[1].Name;
				initializerMetadata = InitializerMetadata.CreateProjectionInitializer(converter.EdmItemCollection, newExpression);
				converter.ValidateInitializerMetadata(initializerMetadata);
				return true;
			}

			// Token: 0x04001ACE RID: 6862
			private const string s_stringsTypeFullName = "Microsoft.VisualBasic.Strings";

			// Token: 0x04001ACF RID: 6863
			private static readonly ExpressionConverter.MethodCallTranslator.CallTranslator s_defaultTranslator = new ExpressionConverter.MethodCallTranslator.DefaultTranslator();

			// Token: 0x04001AD0 RID: 6864
			private static readonly ExpressionConverter.MethodCallTranslator.FunctionCallTranslator s_functionCallTranslator = new ExpressionConverter.MethodCallTranslator.FunctionCallTranslator();

			// Token: 0x04001AD1 RID: 6865
			private static readonly Dictionary<MethodInfo, ExpressionConverter.MethodCallTranslator.CallTranslator> s_methodTranslators = ExpressionConverter.MethodCallTranslator.InitializeMethodTranslators();

			// Token: 0x04001AD2 RID: 6866
			private static readonly Dictionary<SequenceMethod, ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator> s_sequenceTranslators = ExpressionConverter.MethodCallTranslator.InitializeSequenceMethodTranslators();

			// Token: 0x04001AD3 RID: 6867
			private static readonly Dictionary<string, ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator> s_objectQueryTranslators = ExpressionConverter.MethodCallTranslator.InitializeObjectQueryTranslators();

			// Token: 0x04001AD4 RID: 6868
			private static bool s_vbMethodsInitialized;

			// Token: 0x04001AD5 RID: 6869
			private static readonly object s_vbInitializerLock = new object();

			// Token: 0x020006E3 RID: 1763
			private sealed class SpatialMethodCallTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x0600466D RID: 18029 RVA: 0x000FC57B File Offset: 0x000FA77B
				internal SpatialMethodCallTranslator() : base(ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetSupportedMethods())
				{
				}

				// Token: 0x0600466E RID: 18030 RVA: 0x000FC588 File Offset: 0x000FA788
				private static MethodInfo GetStaticMethod<TResult>(Expression<Func<TResult>> lambda)
				{
					return ((MethodCallExpression)lambda.Body).Method;
				}

				// Token: 0x0600466F RID: 18031 RVA: 0x000FC5A8 File Offset: 0x000FA7A8
				private static MethodInfo GetInstanceMethod<T, TResult>(Expression<Func<T, TResult>> lambda)
				{
					return ((MethodCallExpression)lambda.Body).Method;
				}

				// Token: 0x06004670 RID: 18032 RVA: 0x000FC5C7 File Offset: 0x000FA7C7
				private static IEnumerable<MethodInfo> GetSupportedMethods()
				{
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromText(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PointFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.LineFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PolygonFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPointFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiLineFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPolygonFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.GeographyCollectionFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromBinary(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PointFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.LineFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PolygonFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPointFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiLineFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPolygonFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.GeographyCollectionFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromGml(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromGml(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, byte[]>((DbGeography geo) => geo.AsBinary());
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, string>((DbGeography geo) => geo.AsGml());
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, string>((DbGeography geo) => geo.AsText());
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, bool>((DbGeography geo) => geo.SpatialEquals(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, bool>((DbGeography geo) => geo.Disjoint(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, bool>((DbGeography geo) => geo.Intersects(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Buffer((double?)0.0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, double?>((DbGeography geo) => geo.Distance(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Intersection(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Union(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Difference(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.SymmetricDifference(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.ElementAt(0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.PointAt(0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromText(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PointFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.LineFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PolygonFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPointFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiLineFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPolygonFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.GeometryCollectionFromText(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromBinary(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PointFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.LineFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PolygonFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPointFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiLineFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPolygonFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.GeometryCollectionFromBinary(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromGml(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromGml(null, 0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, byte[]>((DbGeometry geo) => geo.AsBinary());
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, string>((DbGeometry geo) => geo.AsGml());
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, string>((DbGeometry geo) => geo.AsText());
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.SpatialEquals(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Disjoint(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Intersects(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Touches(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Crosses(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Within(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Contains(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Overlaps(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Relate(null, null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Buffer((double?)0.0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, double?>((DbGeometry geo) => geo.Distance(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Intersection(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Union(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Difference(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.SymmetricDifference(null));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.ElementAt(0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.PointAt(0));
					yield return ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.InteriorRingAt(0));
					yield break;
				}

				// Token: 0x06004671 RID: 18033 RVA: 0x000FC5D0 File Offset: 0x000FA7D0
				private static Dictionary<MethodInfo, string> GetRenamedMethodFunctions()
				{
					return new Dictionary<MethodInfo, string>
					{
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromText(null)),
							"GeographyFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromText(null, 0)),
							"GeographyFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PointFromText(null, 0)),
							"GeographyPointFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.LineFromText(null, 0)),
							"GeographyLineFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PolygonFromText(null, 0)),
							"GeographyPolygonFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPointFromText(null, 0)),
							"GeographyMultiPointFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiLineFromText(null, 0)),
							"GeographyMultiLineFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPolygonFromText(null, 0)),
							"GeographyMultiPolygonFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.GeographyCollectionFromText(null, 0)),
							"GeographyCollectionFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromBinary(null, 0)),
							"GeographyFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromBinary(null)),
							"GeographyFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PointFromBinary(null, 0)),
							"GeographyPointFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.LineFromBinary(null, 0)),
							"GeographyLineFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.PolygonFromBinary(null, 0)),
							"GeographyPolygonFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPointFromBinary(null, 0)),
							"GeographyMultiPointFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiLineFromBinary(null, 0)),
							"GeographyMultiLineFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.MultiPolygonFromBinary(null, 0)),
							"GeographyMultiPolygonFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.GeographyCollectionFromBinary(null, 0)),
							"GeographyCollectionFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromGml(null)),
							"GeographyFromGml"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeography>(() => DbGeography.FromGml(null, 0)),
							"GeographyFromGml"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, byte[]>((DbGeography geo) => geo.AsBinary()),
							"AsBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, string>((DbGeography geo) => geo.AsGml()),
							"AsGml"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, string>((DbGeography geo) => geo.AsText()),
							"AsText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, bool>((DbGeography geo) => geo.SpatialEquals(null)),
							"SpatialEquals"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, bool>((DbGeography geo) => geo.Disjoint(null)),
							"SpatialDisjoint"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, bool>((DbGeography geo) => geo.Intersects(null)),
							"SpatialIntersects"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Buffer((double?)0.0)),
							"SpatialBuffer"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, double?>((DbGeography geo) => geo.Distance(null)),
							"Distance"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Intersection(null)),
							"SpatialIntersection"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Union(null)),
							"SpatialUnion"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.Difference(null)),
							"SpatialDifference"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.SymmetricDifference(null)),
							"SpatialSymmetricDifference"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.ElementAt(0)),
							"SpatialElementAt"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeography, DbGeography>((DbGeography geo) => geo.PointAt(0)),
							"PointAt"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromText(null)),
							"GeometryFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromText(null, 0)),
							"GeometryFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PointFromText(null, 0)),
							"GeometryPointFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.LineFromText(null, 0)),
							"GeometryLineFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PolygonFromText(null, 0)),
							"GeometryPolygonFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPointFromText(null, 0)),
							"GeometryMultiPointFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiLineFromText(null, 0)),
							"GeometryMultiLineFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPolygonFromText(null, 0)),
							"GeometryMultiPolygonFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.GeometryCollectionFromText(null, 0)),
							"GeometryCollectionFromText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromBinary(null)),
							"GeometryFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromBinary(null, 0)),
							"GeometryFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PointFromBinary(null, 0)),
							"GeometryPointFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.LineFromBinary(null, 0)),
							"GeometryLineFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.PolygonFromBinary(null, 0)),
							"GeometryPolygonFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPointFromBinary(null, 0)),
							"GeometryMultiPointFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiLineFromBinary(null, 0)),
							"GeometryMultiLineFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.MultiPolygonFromBinary(null, 0)),
							"GeometryMultiPolygonFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.GeometryCollectionFromBinary(null, 0)),
							"GeometryCollectionFromBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromGml(null)),
							"GeometryFromGml"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetStaticMethod<DbGeometry>(() => DbGeometry.FromGml(null, 0)),
							"GeometryFromGml"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, byte[]>((DbGeometry geo) => geo.AsBinary()),
							"AsBinary"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, string>((DbGeometry geo) => geo.AsGml()),
							"AsGml"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, string>((DbGeometry geo) => geo.AsText()),
							"AsText"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.SpatialEquals(null)),
							"SpatialEquals"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Disjoint(null)),
							"SpatialDisjoint"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Intersects(null)),
							"SpatialIntersects"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Touches(null)),
							"SpatialTouches"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Crosses(null)),
							"SpatialCrosses"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Within(null)),
							"SpatialWithin"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Contains(null)),
							"SpatialContains"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Overlaps(null)),
							"SpatialOverlaps"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, bool>((DbGeometry geo) => geo.Relate(null, null)),
							"SpatialRelate"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Buffer((double?)0.0)),
							"SpatialBuffer"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, double?>((DbGeometry geo) => geo.Distance(null)),
							"Distance"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Intersection(null)),
							"SpatialIntersection"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Union(null)),
							"SpatialUnion"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Difference(null)),
							"SpatialDifference"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.SymmetricDifference(null)),
							"SpatialSymmetricDifference"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.ElementAt(0)),
							"SpatialElementAt"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.PointAt(0)),
							"PointAt"
						},
						{
							ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetInstanceMethod<DbGeometry, DbGeometry>((DbGeometry geo) => geo.InteriorRingAt(0)),
							"InteriorRingAt"
						}
					};
				}

				// Token: 0x06004672 RID: 18034 RVA: 0x000FE1D4 File Offset: 0x000FC3D4
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					MethodInfo method = call.Method;
					string functionName;
					if (!ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.methodFunctionRenames.TryGetValue(method, out functionName))
					{
						functionName = "ST" + method.Name;
					}
					Expression[] linqArguments;
					if (method.IsStatic)
					{
						linqArguments = call.Arguments.ToArray<Expression>();
					}
					else
					{
						linqArguments = new Expression[]
						{
							call.Object
						}.Concat(call.Arguments).ToArray<Expression>();
					}
					return parent.TranslateIntoCanonicalFunction(functionName, call, linqArguments);
				}

				// Token: 0x0400209A RID: 8346
				private static readonly Dictionary<MethodInfo, string> methodFunctionRenames = ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetRenamedMethodFunctions();
			}

			// Token: 0x020006E4 RID: 1764
			private abstract class CallTranslator
			{
				// Token: 0x06004674 RID: 18036 RVA: 0x000FE255 File Offset: 0x000FC455
				protected CallTranslator(params MethodInfo[] methods)
				{
					this._methods = methods;
				}

				// Token: 0x06004675 RID: 18037 RVA: 0x000FE255 File Offset: 0x000FC455
				protected CallTranslator(IEnumerable<MethodInfo> methods)
				{
					this._methods = methods;
				}

				// Token: 0x17000BD2 RID: 3026
				// (get) Token: 0x06004676 RID: 18038 RVA: 0x000FE264 File Offset: 0x000FC464
				internal IEnumerable<MethodInfo> Methods
				{
					get
					{
						return this._methods;
					}
				}

				// Token: 0x06004677 RID: 18039
				internal abstract DbExpression Translate(ExpressionConverter parent, MethodCallExpression call);

				// Token: 0x06004678 RID: 18040 RVA: 0x000E5329 File Offset: 0x000E3529
				public override string ToString()
				{
					return base.GetType().Name;
				}

				// Token: 0x0400209B RID: 8347
				private readonly IEnumerable<MethodInfo> _methods;
			}

			// Token: 0x020006E5 RID: 1765
			private abstract class ObjectQueryCallTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06004679 RID: 18041 RVA: 0x000FE26C File Offset: 0x000FC46C
				internal static bool IsCandidateMethod(MethodInfo method)
				{
					Type declaringType = method.DeclaringType;
					return (method.IsPublic || (method.IsAssembly && (method.Name == "MergeAs" || method.Name == "IncludeSpan"))) && null != declaringType && declaringType.IsGenericType && typeof(ObjectQuery<>) == declaringType.GetGenericTypeDefinition();
				}

				// Token: 0x0600467A RID: 18042 RVA: 0x000FE2DC File Offset: 0x000FC4DC
				internal static Expression RemoveConvertToObjectQuery(Expression queryExpression)
				{
					if (queryExpression.NodeType == ExpressionType.Convert)
					{
						UnaryExpression unaryExpression = (UnaryExpression)queryExpression;
						Type type = unaryExpression.Operand.Type;
						if (type.IsGenericType && (typeof(IQueryable<>) == type.GetGenericTypeDefinition() || typeof(IOrderedQueryable<>) == type.GetGenericTypeDefinition()))
						{
							queryExpression = unaryExpression.Operand;
						}
					}
					return queryExpression;
				}

				// Token: 0x0600467B RID: 18043 RVA: 0x000FE345 File Offset: 0x000FC545
				protected ObjectQueryCallTranslator(string methodName) : base(new MethodInfo[0])
				{
					this._methodName = methodName;
				}

				// Token: 0x17000BD3 RID: 3027
				// (get) Token: 0x0600467C RID: 18044 RVA: 0x000FE35A File Offset: 0x000FC55A
				internal string MethodName
				{
					get
					{
						return this._methodName;
					}
				}

				// Token: 0x0400209C RID: 8348
				private readonly string _methodName;
			}

			// Token: 0x020006E6 RID: 1766
			private abstract class ObjectQueryBuilderCallTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator
			{
				// Token: 0x0600467D RID: 18045 RVA: 0x000FE364 File Offset: 0x000FC564
				protected ObjectQueryBuilderCallTranslator(string methodName, SequenceMethod sequenceEquivalent) : base(methodName)
				{
					bool flag = ExpressionConverter.MethodCallTranslator.s_sequenceTranslators.TryGetValue(sequenceEquivalent, out this._translator);
				}

				// Token: 0x0600467E RID: 18046 RVA: 0x000FE38A File Offset: 0x000FC58A
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return this._translator.Translate(parent, call);
				}

				// Token: 0x0400209D RID: 8349
				private readonly ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator _translator;
			}

			// Token: 0x020006E7 RID: 1767
			private sealed class ObjectQueryBuilderUnionTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x0600467F RID: 18047 RVA: 0x000FE399 File Offset: 0x000FC599
				internal ObjectQueryBuilderUnionTranslator() : base("Union", SequenceMethod.Union)
				{
				}
			}

			// Token: 0x020006E8 RID: 1768
			private sealed class ObjectQueryBuilderIntersectTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x06004680 RID: 18048 RVA: 0x000FE3A8 File Offset: 0x000FC5A8
				internal ObjectQueryBuilderIntersectTranslator() : base("Intersect", SequenceMethod.Intersect)
				{
				}
			}

			// Token: 0x020006E9 RID: 1769
			private sealed class ObjectQueryBuilderExceptTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x06004681 RID: 18049 RVA: 0x000FE3B7 File Offset: 0x000FC5B7
				internal ObjectQueryBuilderExceptTranslator() : base("Except", SequenceMethod.Except)
				{
				}
			}

			// Token: 0x020006EA RID: 1770
			private sealed class ObjectQueryBuilderDistinctTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x06004682 RID: 18050 RVA: 0x000FE3C6 File Offset: 0x000FC5C6
				internal ObjectQueryBuilderDistinctTranslator() : base("Distinct", SequenceMethod.Distinct)
				{
				}
			}

			// Token: 0x020006EB RID: 1771
			private sealed class ObjectQueryBuilderOfTypeTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x06004683 RID: 18051 RVA: 0x000FE3D5 File Offset: 0x000FC5D5
				internal ObjectQueryBuilderOfTypeTranslator() : base("OfType", SequenceMethod.OfType)
				{
				}
			}

			// Token: 0x020006EC RID: 1772
			private sealed class ObjectQueryBuilderFirstTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x06004684 RID: 18052 RVA: 0x000FE3E3 File Offset: 0x000FC5E3
				internal ObjectQueryBuilderFirstTranslator() : base("First", SequenceMethod.First)
				{
				}
			}

			// Token: 0x020006ED RID: 1773
			private sealed class ObjectQueryIncludeTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator
			{
				// Token: 0x06004685 RID: 18053 RVA: 0x000FE3F2 File Offset: 0x000FC5F2
				internal ObjectQueryIncludeTranslator() : base("Include")
				{
				}

				// Token: 0x06004686 RID: 18054 RVA: 0x000FE400 File Offset: 0x000FC600
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression expression = parent.TranslateExpression(call.Object);
					Span span;
					if (!parent.TryGetSpan(expression, out span))
					{
						span = null;
					}
					DbExpression dbExpression = parent.TranslateExpression(call.Arguments[0]);
					if (dbExpression.ExpressionKind == DbExpressionKind.Constant)
					{
						string pathToInclude = (string)((DbConstantExpression)dbExpression).Value;
						if (parent.CanIncludeSpanInfo())
						{
							span = Span.IncludeIn(span, pathToInclude);
						}
						return parent.AddSpanMapping(expression, span);
					}
					throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedInclude);
				}
			}

			// Token: 0x020006EE RID: 1774
			private sealed class ObjectQueryMergeAsTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator
			{
				// Token: 0x06004687 RID: 18055 RVA: 0x000FE47B File Offset: 0x000FC67B
				internal ObjectQueryMergeAsTranslator() : base("MergeAs")
				{
				}

				// Token: 0x06004688 RID: 18056 RVA: 0x000FE488 File Offset: 0x000FC688
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (call.Arguments[0].NodeType != ExpressionType.Constant)
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedMergeAs);
					}
					MergeOption mergeOption = (MergeOption)((ConstantExpression)call.Arguments[0]).Value;
					EntityUtil.CheckArgumentMergeOption(mergeOption);
					parent.NotifyMergeOption(mergeOption);
					Expression linq = ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator.RemoveConvertToObjectQuery(call.Object);
					DbExpression expression = parent.TranslateExpression(linq);
					Span span;
					if (!parent.TryGetSpan(expression, out span))
					{
						span = null;
					}
					return parent.AddSpanMapping(expression, span);
				}
			}

			// Token: 0x020006EF RID: 1775
			private sealed class ObjectQueryIncludeSpanTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator
			{
				// Token: 0x06004689 RID: 18057 RVA: 0x000FE507 File Offset: 0x000FC707
				internal ObjectQueryIncludeSpanTranslator() : base("IncludeSpan")
				{
				}

				// Token: 0x0600468A RID: 18058 RVA: 0x000FE514 File Offset: 0x000FC714
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					Span span = (Span)((ConstantExpression)call.Arguments[0]).Value;
					Expression linq = ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator.RemoveConvertToObjectQuery(call.Object);
					DbExpression expression = parent.TranslateExpression(linq);
					if (!parent.CanIncludeSpanInfo())
					{
						span = null;
					}
					return parent.AddSpanMapping(expression, span);
				}
			}

			// Token: 0x020006F0 RID: 1776
			private sealed class DefaultTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x0600468B RID: 18059 RVA: 0x000FE563 File Offset: 0x000FC763
				internal DefaultTranslator() : base(new MethodInfo[0])
				{
				}

				// Token: 0x0600468C RID: 18060 RVA: 0x000FE574 File Offset: 0x000FC774
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					MethodInfo p;
					if (ExpressionConverter.MethodCallTranslator.DefaultTranslator.TryGetAlternativeMethod(call.Method, out p))
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedMethodSuggestedAlternative(call.Method, p));
					}
					throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedMethod(call.Method));
				}

				// Token: 0x0600468D RID: 18061 RVA: 0x000FE5B4 File Offset: 0x000FC7B4
				private static bool TryGetAlternativeMethod(MethodInfo originalMethodInfo, out MethodInfo suggestedMethodInfo)
				{
					if (ExpressionConverter.MethodCallTranslator.DefaultTranslator.s_alternativeMethods.TryGetValue(originalMethodInfo, out suggestedMethodInfo))
					{
						return true;
					}
					if ("Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" == originalMethodInfo.DeclaringType.Assembly.FullName)
					{
						object obj = ExpressionConverter.MethodCallTranslator.DefaultTranslator.s_vbInitializerLock;
						lock (obj)
						{
							if (!ExpressionConverter.MethodCallTranslator.DefaultTranslator.s_vbMethodsInitialized)
							{
								ExpressionConverter.MethodCallTranslator.DefaultTranslator.InitializeVBMethods(originalMethodInfo.DeclaringType.Assembly);
								ExpressionConverter.MethodCallTranslator.DefaultTranslator.s_vbMethodsInitialized = true;
							}
							return ExpressionConverter.MethodCallTranslator.DefaultTranslator.s_alternativeMethods.TryGetValue(originalMethodInfo, out suggestedMethodInfo);
						}
					}
					suggestedMethodInfo = null;
					return false;
				}

				// Token: 0x0600468E RID: 18062 RVA: 0x000FE64C File Offset: 0x000FC84C
				private static Dictionary<MethodInfo, MethodInfo> InitializeAlternateMethodInfos()
				{
					return new Dictionary<MethodInfo, MethodInfo>(1);
				}

				// Token: 0x0600468F RID: 18063 RVA: 0x000FE654 File Offset: 0x000FC854
				private static void InitializeVBMethods(Assembly vbAssembly)
				{
					Type type = vbAssembly.GetType("Microsoft.VisualBasic.Strings");
					ExpressionConverter.MethodCallTranslator.DefaultTranslator.s_alternativeMethods.Add(type.GetMethod("Mid", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(string),
						typeof(int)
					}, null), type.GetMethod("Mid", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(string),
						typeof(int),
						typeof(int)
					}, null));
				}

				// Token: 0x0400209E RID: 8350
				private static readonly Dictionary<MethodInfo, MethodInfo> s_alternativeMethods = ExpressionConverter.MethodCallTranslator.DefaultTranslator.InitializeAlternateMethodInfos();

				// Token: 0x0400209F RID: 8351
				private static bool s_vbMethodsInitialized;

				// Token: 0x040020A0 RID: 8352
				private static readonly object s_vbInitializerLock = new object();
			}

			// Token: 0x020006F1 RID: 1777
			private sealed class FunctionCallTranslator
			{
				// Token: 0x06004691 RID: 18065 RVA: 0x00002050 File Offset: 0x00000250
				internal FunctionCallTranslator()
				{
				}

				// Token: 0x06004692 RID: 18066 RVA: 0x000FE6F8 File Offset: 0x000FC8F8
				internal DbExpression TranslateFunctionCall(ExpressionConverter parent, MethodCallExpression call, EdmFunctionAttribute functionAttribute)
				{
					ExpressionConverter.MethodCallTranslator.FunctionCallTranslator.ValidateFunctionAttributeParameter(call, functionAttribute.NamespaceName, "namespaceName");
					ExpressionConverter.MethodCallTranslator.FunctionCallTranslator.ValidateFunctionAttributeParameter(call, functionAttribute.FunctionName, "functionName");
					List<DbExpression> list = (from a in call.Arguments
					select this.UnwrapNoOpConverts(a) into b
					select this.NormalizeAllSetSources(parent, parent.TranslateExpression(b))).ToList<DbExpression>();
					List<TypeUsage> argumentTypes = (from a in list
					select a.ResultType).ToList<TypeUsage>();
					EdmFunction edmFunction = parent.FindFunction(functionAttribute.NamespaceName, functionAttribute.FunctionName, argumentTypes, false, call);
					if (!edmFunction.IsComposableAttribute)
					{
						throw EntityUtil.NotSupported(Strings.CannotCallNoncomposableFunction(edmFunction.FullName));
					}
					DbExpression dbExpression = edmFunction.Invoke(list);
					return this.ValidateReturnType(dbExpression, dbExpression.ResultType, parent, call, call.Type, false);
				}

				// Token: 0x06004693 RID: 18067 RVA: 0x000FE7F0 File Offset: 0x000FC9F0
				private DbExpression NormalizeAllSetSources(ExpressionConverter parent, DbExpression argumentExpr)
				{
					DbExpression dbExpression = null;
					BuiltInTypeKind builtInTypeKind = argumentExpr.ResultType.EdmType.BuiltInTypeKind;
					if (builtInTypeKind != BuiltInTypeKind.CollectionType)
					{
						if (builtInTypeKind == BuiltInTypeKind.RowType)
						{
							List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>();
							RowType rowType = argumentExpr.ResultType.EdmType as RowType;
							bool flag = false;
							foreach (EdmProperty propertyMetadata in rowType.Properties)
							{
								DbPropertyExpression dbPropertyExpression = argumentExpr.Property(propertyMetadata);
								dbExpression = this.NormalizeAllSetSources(parent, dbPropertyExpression);
								if (dbExpression != dbPropertyExpression)
								{
									flag = true;
									list.Add(new KeyValuePair<string, DbExpression>(dbPropertyExpression.Property.Name, dbExpression));
								}
								else
								{
									list.Add(new KeyValuePair<string, DbExpression>(dbPropertyExpression.Property.Name, dbPropertyExpression));
								}
							}
							if (flag)
							{
								dbExpression = DbExpressionBuilder.NewRow(list);
							}
							else
							{
								dbExpression = argumentExpr;
							}
						}
					}
					else
					{
						DbExpressionBinding dbExpressionBinding = argumentExpr.BindAs(parent.AliasGenerator.Next());
						DbExpression dbExpression2 = this.NormalizeAllSetSources(parent, dbExpressionBinding.Variable);
						if (dbExpression2 != dbExpressionBinding.Variable)
						{
							dbExpression = dbExpressionBinding.Project(dbExpression2);
						}
					}
					if (dbExpression != null && dbExpression != argumentExpr)
					{
						return parent.NormalizeSetSource(dbExpression);
					}
					return parent.NormalizeSetSource(argumentExpr);
				}

				// Token: 0x06004694 RID: 18068 RVA: 0x000FE92C File Offset: 0x000FCB2C
				private Expression UnwrapNoOpConverts(Expression expression)
				{
					if (expression.NodeType == ExpressionType.Convert)
					{
						UnaryExpression unaryExpression = (UnaryExpression)expression;
						Expression expression2 = this.UnwrapNoOpConverts(unaryExpression.Operand);
						if (expression.Type.IsAssignableFrom(expression2.Type))
						{
							return expression2;
						}
					}
					return expression;
				}

				// Token: 0x06004695 RID: 18069 RVA: 0x000FE970 File Offset: 0x000FCB70
				private DbExpression ValidateReturnType(DbExpression result, TypeUsage actualReturnType, ExpressionConverter parent, MethodCallExpression call, Type clrReturnType, bool isElementOfCollection)
				{
					BuiltInTypeKind builtInTypeKind = actualReturnType.EdmType.BuiltInTypeKind;
					if (builtInTypeKind != BuiltInTypeKind.CollectionType)
					{
						if (builtInTypeKind != BuiltInTypeKind.RefType)
						{
							if (builtInTypeKind != BuiltInTypeKind.RowType)
							{
								if (isElementOfCollection)
								{
									TypeUsage castTargetType = parent.GetCastTargetType(actualReturnType, clrReturnType, null, false);
									if (castTargetType != null)
									{
										throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
									}
								}
								TypeUsage valueLayerType = parent.GetValueLayerType(clrReturnType);
								if (!TypeSemantics.IsPromotableTo(actualReturnType, valueLayerType))
								{
									throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
								}
								if (!isElementOfCollection)
								{
									result = parent.AlignTypes(result, clrReturnType);
								}
							}
							else if (clrReturnType != typeof(DbDataRecord))
							{
								throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
							}
						}
						else if (clrReturnType != typeof(EntityKey))
						{
							throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
						}
					}
					else
					{
						if (!clrReturnType.IsGenericType)
						{
							throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
						}
						Type genericTypeDefinition = clrReturnType.GetGenericTypeDefinition();
						if (genericTypeDefinition != typeof(IEnumerable<>) && genericTypeDefinition != typeof(IQueryable<>))
						{
							throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
						}
						Type clrReturnType2 = clrReturnType.GetGenericArguments()[0];
						result = this.ValidateReturnType(result, TypeHelpers.GetElementTypeUsage(actualReturnType), parent, call, clrReturnType2, true);
					}
					return result;
				}

				// Token: 0x06004696 RID: 18070 RVA: 0x000FEB11 File Offset: 0x000FCD11
				internal static void ValidateFunctionAttributeParameter(MethodCallExpression call, string parameterValue, string parameterName)
				{
					if (string.IsNullOrEmpty(parameterValue))
					{
						throw EntityUtil.NotSupported(Strings.ELinq_EdmFunctionAttributeParameterNameNotValid(call.Method, call.Method.DeclaringType, parameterName));
					}
				}
			}

			// Token: 0x020006F2 RID: 1778
			private sealed class CanonicalFunctionDefaultTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06004697 RID: 18071 RVA: 0x000FEB38 File Offset: 0x000FCD38
				internal CanonicalFunctionDefaultTranslator() : base(ExpressionConverter.MethodCallTranslator.CanonicalFunctionDefaultTranslator.GetMethods())
				{
				}

				// Token: 0x06004698 RID: 18072 RVA: 0x000FEB48 File Offset: 0x000FCD48
				private static IEnumerable<MethodInfo> GetMethods()
				{
					List<MethodInfo> list = new List<MethodInfo>
					{
						typeof(Math).GetMethod("Ceiling", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							typeof(decimal)
						}, null),
						typeof(Math).GetMethod("Ceiling", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							typeof(double)
						}, null),
						typeof(Math).GetMethod("Floor", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							typeof(decimal)
						}, null),
						typeof(Math).GetMethod("Floor", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							typeof(double)
						}, null),
						typeof(Math).GetMethod("Round", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							typeof(decimal)
						}, null),
						typeof(Math).GetMethod("Round", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							typeof(double)
						}, null),
						typeof(Math).GetMethod("Round", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							typeof(decimal),
							typeof(int)
						}, null),
						typeof(Math).GetMethod("Round", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							typeof(double),
							typeof(int)
						}, null),
						typeof(decimal).GetMethod("Floor", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							typeof(decimal)
						}, null),
						typeof(decimal).GetMethod("Ceiling", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							typeof(decimal)
						}, null),
						typeof(decimal).GetMethod("Round", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							typeof(decimal)
						}, null),
						typeof(decimal).GetMethod("Round", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							typeof(decimal),
							typeof(int)
						}, null),
						typeof(string).GetMethod("Replace", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
						{
							typeof(string),
							typeof(string)
						}, null),
						typeof(string).GetMethod("ToLower", BindingFlags.Instance | BindingFlags.Public, null, new Type[0], null),
						typeof(string).GetMethod("ToUpper", BindingFlags.Instance | BindingFlags.Public, null, new Type[0], null),
						typeof(string).GetMethod("Trim", BindingFlags.Instance | BindingFlags.Public, null, new Type[0], null)
					};
					foreach (Type type in new Type[]
					{
						typeof(decimal),
						typeof(double),
						typeof(float),
						typeof(int),
						typeof(long),
						typeof(sbyte),
						typeof(short)
					})
					{
						list.Add(typeof(Math).GetMethod("Abs", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							type
						}, null));
					}
					return list;
				}

				// Token: 0x06004699 RID: 18073 RVA: 0x000FEF1C File Offset: 0x000FD11C
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					Expression[] linqArguments;
					if (!call.Method.IsStatic)
					{
						List<Expression> list = new List<Expression>(call.Arguments.Count + 1);
						list.Add(call.Object);
						list.AddRange(call.Arguments);
						linqArguments = list.ToArray();
					}
					else
					{
						linqArguments = call.Arguments.ToArray<Expression>();
					}
					return parent.TranslateIntoCanonicalFunction(call.Method.Name, call, linqArguments);
				}
			}

			// Token: 0x020006F3 RID: 1779
			private abstract class AsUnicodeNonUnicodeBaseFunctionTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x0600469A RID: 18074 RVA: 0x000FEF89 File Offset: 0x000FD189
				protected AsUnicodeNonUnicodeBaseFunctionTranslator(IEnumerable<MethodInfo> methods, bool isUnicode) : base(methods)
				{
					this._isUnicode = isUnicode;
				}

				// Token: 0x0600469B RID: 18075 RVA: 0x000FEF9C File Offset: 0x000FD19C
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateExpression(call.Arguments[0]);
					TypeUsage typeUsage = dbExpression.ResultType.ShallowCopy(new FacetValues
					{
						Unicode = new bool?(this._isUnicode)
					});
					DbExpressionKind expressionKind = dbExpression.ExpressionKind;
					DbExpression result;
					if (expressionKind != DbExpressionKind.Constant)
					{
						if (expressionKind != DbExpressionKind.Null)
						{
							if (expressionKind != DbExpressionKind.ParameterReference)
							{
								throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedAsUnicodeAndAsNonUnicode(call.Method));
							}
							result = typeUsage.Parameter(((DbParameterReferenceExpression)dbExpression).ParameterName);
						}
						else
						{
							result = typeUsage.Null();
						}
					}
					else
					{
						result = typeUsage.Constant(((DbConstantExpression)dbExpression).Value);
					}
					return result;
				}

				// Token: 0x040020A1 RID: 8353
				private bool _isUnicode;
			}

			// Token: 0x020006F4 RID: 1780
			private sealed class AsUnicodeFunctionTranslator : ExpressionConverter.MethodCallTranslator.AsUnicodeNonUnicodeBaseFunctionTranslator
			{
				// Token: 0x0600469C RID: 18076 RVA: 0x000FF03D File Offset: 0x000FD23D
				internal AsUnicodeFunctionTranslator() : base(ExpressionConverter.MethodCallTranslator.AsUnicodeFunctionTranslator.GetMethods(), true)
				{
				}

				// Token: 0x0600469D RID: 18077 RVA: 0x000FF04B File Offset: 0x000FD24B
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(EntityFunctions).GetMethod("AsUnicode", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(string)
					}, null);
					yield break;
				}
			}

			// Token: 0x020006F5 RID: 1781
			private sealed class AsNonUnicodeFunctionTranslator : ExpressionConverter.MethodCallTranslator.AsUnicodeNonUnicodeBaseFunctionTranslator
			{
				// Token: 0x0600469E RID: 18078 RVA: 0x000FF054 File Offset: 0x000FD254
				internal AsNonUnicodeFunctionTranslator() : base(ExpressionConverter.MethodCallTranslator.AsNonUnicodeFunctionTranslator.GetMethods(), false)
				{
				}

				// Token: 0x0600469F RID: 18079 RVA: 0x000FF062 File Offset: 0x000FD262
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(EntityFunctions).GetMethod("AsNonUnicode", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(string)
					}, null);
					yield break;
				}
			}

			// Token: 0x020006F6 RID: 1782
			private sealed class MathPowerTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046A0 RID: 18080 RVA: 0x000FF06C File Offset: 0x000FD26C
				internal MathPowerTranslator() : base(new MethodInfo[]
				{
					typeof(Math).GetMethod("Pow", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(double),
						typeof(double)
					}, null)
				})
				{
				}

				// Token: 0x060046A1 RID: 18081 RVA: 0x000FF0C0 File Offset: 0x000FD2C0
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression baseArgument = parent.TranslateExpression(call.Arguments[0]);
					DbExpression exponent = parent.TranslateExpression(call.Arguments[1]);
					return baseArgument.Power(exponent);
				}
			}

			// Token: 0x020006F7 RID: 1783
			private sealed class GuidNewGuidTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046A2 RID: 18082 RVA: 0x000FF0FC File Offset: 0x000FD2FC
				internal GuidNewGuidTranslator() : base(new MethodInfo[]
				{
					typeof(Guid).GetMethod("NewGuid", BindingFlags.Static | BindingFlags.Public, null, Type.EmptyTypes, null)
				})
				{
				}

				// Token: 0x060046A3 RID: 18083 RVA: 0x000FF135 File Offset: 0x000FD335
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return EdmFunctions.NewGuid();
				}
			}

			// Token: 0x020006F8 RID: 1784
			private sealed class StringContainsTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046A4 RID: 18084 RVA: 0x000FF13C File Offset: 0x000FD33C
				internal StringContainsTranslator() : base(ExpressionConverter.MethodCallTranslator.StringContainsTranslator.GetMethods())
				{
				}

				// Token: 0x060046A5 RID: 18085 RVA: 0x000FF149 File Offset: 0x000FD349
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetMethod("Contains", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
					{
						typeof(string)
					}, null);
					yield break;
				}

				// Token: 0x060046A6 RID: 18086 RVA: 0x000FF152 File Offset: 0x000FD352
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateFunctionIntoLike(call, true, true, new Func<ExpressionConverter, MethodCallExpression, DbExpression, DbExpression, DbExpression>(ExpressionConverter.MethodCallTranslator.StringContainsTranslator.CreateDefaultTranslation));
				}

				// Token: 0x060046A7 RID: 18087 RVA: 0x000FF16C File Offset: 0x000FD36C
				private static DbExpression CreateDefaultTranslation(ExpressionConverter parent, MethodCallExpression call, DbExpression patternExpression, DbExpression inputExpression)
				{
					DbFunctionExpression left = parent.CreateCanonicalFunction("IndexOf", call, new DbExpression[]
					{
						patternExpression,
						inputExpression
					});
					return left.GreaterThan(DbExpressionBuilder.Constant(0));
				}
			}

			// Token: 0x020006F9 RID: 1785
			private sealed class IndexOfTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046A8 RID: 18088 RVA: 0x000FF1A7 File Offset: 0x000FD3A7
				internal IndexOfTranslator() : base(ExpressionConverter.MethodCallTranslator.IndexOfTranslator.GetMethods())
				{
				}

				// Token: 0x060046A9 RID: 18089 RVA: 0x000FF1B4 File Offset: 0x000FD3B4
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetMethod("IndexOf", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
					{
						typeof(string)
					}, null);
					yield break;
				}

				// Token: 0x060046AA RID: 18090 RVA: 0x000FF1C0 File Offset: 0x000FD3C0
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbFunctionExpression left = parent.TranslateIntoCanonicalFunction("IndexOf", call, new Expression[]
					{
						call.Arguments[0],
						call.Object
					});
					return left.Minus(DbExpressionBuilder.Constant(1));
				}
			}

			// Token: 0x020006FA RID: 1786
			private sealed class StartsWithTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046AB RID: 18091 RVA: 0x000FF20B File Offset: 0x000FD40B
				internal StartsWithTranslator() : base(ExpressionConverter.MethodCallTranslator.StartsWithTranslator.GetMethods())
				{
				}

				// Token: 0x060046AC RID: 18092 RVA: 0x000FF218 File Offset: 0x000FD418
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetMethod("StartsWith", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
					{
						typeof(string)
					}, null);
					yield break;
				}

				// Token: 0x060046AD RID: 18093 RVA: 0x000FF221 File Offset: 0x000FD421
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateFunctionIntoLike(call, false, true, new Func<ExpressionConverter, MethodCallExpression, DbExpression, DbExpression, DbExpression>(ExpressionConverter.MethodCallTranslator.StartsWithTranslator.CreateDefaultTranslation));
				}

				// Token: 0x060046AE RID: 18094 RVA: 0x000FF238 File Offset: 0x000FD438
				private static DbExpression CreateDefaultTranslation(ExpressionConverter parent, MethodCallExpression call, DbExpression patternExpression, DbExpression inputExpression)
				{
					return parent.CreateCanonicalFunction("IndexOf", call, new DbExpression[]
					{
						patternExpression,
						inputExpression
					}).Equal(DbExpressionBuilder.Constant(1));
				}
			}

			// Token: 0x020006FB RID: 1787
			private sealed class EndsWithTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046AF RID: 18095 RVA: 0x000FF271 File Offset: 0x000FD471
				internal EndsWithTranslator() : base(ExpressionConverter.MethodCallTranslator.EndsWithTranslator.GetMethods())
				{
				}

				// Token: 0x060046B0 RID: 18096 RVA: 0x000FF27E File Offset: 0x000FD47E
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetMethod("EndsWith", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
					{
						typeof(string)
					}, null);
					yield break;
				}

				// Token: 0x060046B1 RID: 18097 RVA: 0x000FF287 File Offset: 0x000FD487
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateFunctionIntoLike(call, true, false, new Func<ExpressionConverter, MethodCallExpression, DbExpression, DbExpression, DbExpression>(ExpressionConverter.MethodCallTranslator.EndsWithTranslator.CreateDefaultTranslation));
				}

				// Token: 0x060046B2 RID: 18098 RVA: 0x000FF2A0 File Offset: 0x000FD4A0
				private static DbExpression CreateDefaultTranslation(ExpressionConverter parent, MethodCallExpression call, DbExpression patternExpression, DbExpression inputExpression)
				{
					DbFunctionExpression dbFunctionExpression = parent.CreateCanonicalFunction("Reverse", call, new DbExpression[]
					{
						patternExpression
					});
					DbFunctionExpression dbFunctionExpression2 = parent.CreateCanonicalFunction("Reverse", call, new DbExpression[]
					{
						inputExpression
					});
					return parent.CreateCanonicalFunction("IndexOf", call, new DbExpression[]
					{
						dbFunctionExpression,
						dbFunctionExpression2
					}).Equal(DbExpressionBuilder.Constant(1));
				}
			}

			// Token: 0x020006FC RID: 1788
			private sealed class SubstringTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046B3 RID: 18099 RVA: 0x000FF307 File Offset: 0x000FD507
				internal SubstringTranslator() : base(ExpressionConverter.MethodCallTranslator.SubstringTranslator.GetMethods())
				{
				}

				// Token: 0x060046B4 RID: 18100 RVA: 0x000FF314 File Offset: 0x000FD514
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetMethod("Substring", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
					{
						typeof(int)
					}, null);
					yield return typeof(string).GetMethod("Substring", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
					{
						typeof(int),
						typeof(int)
					}, null);
					yield break;
				}

				// Token: 0x060046B5 RID: 18101 RVA: 0x000FF320 File Offset: 0x000FD520
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateExpression(call.Arguments[0]);
					DbExpression dbExpression2 = parent.TranslateExpression(call.Object);
					DbExpression dbExpression3 = dbExpression.Plus(DbExpressionBuilder.Constant(1));
					DbExpression dbExpression4;
					if (call.Arguments.Count == 1)
					{
						dbExpression4 = parent.CreateCanonicalFunction("Length", call, new DbExpression[]
						{
							dbExpression2
						}).Minus(dbExpression);
					}
					else
					{
						dbExpression4 = parent.TranslateExpression(call.Arguments[1]);
					}
					return parent.CreateCanonicalFunction("Substring", call, new DbExpression[]
					{
						dbExpression2,
						dbExpression3,
						dbExpression4
					});
				}
			}

			// Token: 0x020006FD RID: 1789
			private sealed class RemoveTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046B6 RID: 18102 RVA: 0x000FF3C1 File Offset: 0x000FD5C1
				internal RemoveTranslator() : base(ExpressionConverter.MethodCallTranslator.RemoveTranslator.GetMethods())
				{
				}

				// Token: 0x060046B7 RID: 18103 RVA: 0x000FF3CE File Offset: 0x000FD5CE
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetMethod("Remove", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
					{
						typeof(int)
					}, null);
					yield return typeof(string).GetMethod("Remove", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
					{
						typeof(int),
						typeof(int)
					}, null);
					yield break;
				}

				// Token: 0x060046B8 RID: 18104 RVA: 0x000FF3D8 File Offset: 0x000FD5D8
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateExpression(call.Object);
					DbExpression dbExpression2 = parent.TranslateExpression(call.Arguments[0]);
					DbExpression dbExpression3 = parent.CreateCanonicalFunction("Substring", call, new DbExpression[]
					{
						dbExpression,
						DbExpressionBuilder.Constant(1),
						dbExpression2
					});
					if (call.Arguments.Count == 2)
					{
						DbExpression dbExpression4 = parent.TranslateExpression(call.Arguments[1]);
						if (!ExpressionConverter.MethodCallTranslator.RemoveTranslator.IsNonNegativeIntegerConstant(dbExpression4))
						{
							throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedStringRemoveCase(call.Method, call.Method.GetParameters()[1].Name));
						}
						DbExpression dbExpression5 = dbExpression2.Plus(dbExpression4).Plus(DbExpressionBuilder.Constant(1));
						DbExpression dbExpression6 = parent.CreateCanonicalFunction("Length", call, new DbExpression[]
						{
							dbExpression
						}).Minus(dbExpression2.Plus(dbExpression4));
						DbExpression dbExpression7 = parent.CreateCanonicalFunction("Substring", call, new DbExpression[]
						{
							dbExpression,
							dbExpression5,
							dbExpression6
						});
						dbExpression3 = parent.CreateCanonicalFunction("Concat", call, new DbExpression[]
						{
							dbExpression3,
							dbExpression7
						});
					}
					return dbExpression3;
				}

				// Token: 0x060046B9 RID: 18105 RVA: 0x000FF4FC File Offset: 0x000FD6FC
				private static bool IsNonNegativeIntegerConstant(DbExpression argument)
				{
					if (argument.ExpressionKind != DbExpressionKind.Constant || !TypeSemantics.IsPrimitiveType(argument.ResultType, PrimitiveTypeKind.Int32))
					{
						return false;
					}
					DbConstantExpression dbConstantExpression = (DbConstantExpression)argument;
					int num = (int)dbConstantExpression.Value;
					return num >= 0;
				}
			}

			// Token: 0x020006FE RID: 1790
			private sealed class InsertTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046BA RID: 18106 RVA: 0x000FF53D File Offset: 0x000FD73D
				internal InsertTranslator() : base(ExpressionConverter.MethodCallTranslator.InsertTranslator.GetMethods())
				{
				}

				// Token: 0x060046BB RID: 18107 RVA: 0x000FF54A File Offset: 0x000FD74A
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetMethod("Insert", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
					{
						typeof(int),
						typeof(string)
					}, null);
					yield break;
				}

				// Token: 0x060046BC RID: 18108 RVA: 0x000FF554 File Offset: 0x000FD754
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateExpression(call.Object);
					DbExpression dbExpression2 = parent.TranslateExpression(call.Arguments[0]);
					DbExpression dbExpression3 = parent.CreateCanonicalFunction("Substring", call, new DbExpression[]
					{
						dbExpression,
						DbExpressionBuilder.Constant(1),
						dbExpression2
					});
					DbExpression dbExpression4 = parent.CreateCanonicalFunction("Substring", call, new DbExpression[]
					{
						dbExpression,
						dbExpression2.Plus(DbExpressionBuilder.Constant(1)),
						parent.CreateCanonicalFunction("Length", call, new DbExpression[]
						{
							dbExpression
						}).Minus(dbExpression2)
					});
					DbExpression dbExpression5 = parent.TranslateExpression(call.Arguments[1]);
					return parent.CreateCanonicalFunction("Concat", call, new DbExpression[]
					{
						parent.CreateCanonicalFunction("Concat", call, new DbExpression[]
						{
							dbExpression3,
							dbExpression5
						}),
						dbExpression4
					});
				}
			}

			// Token: 0x020006FF RID: 1791
			private sealed class IsNullOrEmptyTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046BD RID: 18109 RVA: 0x000FF640 File Offset: 0x000FD840
				internal IsNullOrEmptyTranslator() : base(ExpressionConverter.MethodCallTranslator.IsNullOrEmptyTranslator.GetMethods())
				{
				}

				// Token: 0x060046BE RID: 18110 RVA: 0x000FF64D File Offset: 0x000FD84D
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetMethod("IsNullOrEmpty", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(string)
					}, null);
					yield break;
				}

				// Token: 0x060046BF RID: 18111 RVA: 0x000FF658 File Offset: 0x000FD858
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateExpression(call.Arguments[0]);
					DbExpression left = dbExpression.IsNull();
					DbExpression right = parent.CreateCanonicalFunction("Length", call, new DbExpression[]
					{
						dbExpression
					}).Equal(DbExpressionBuilder.Constant(0));
					return left.Or(right);
				}
			}

			// Token: 0x02000700 RID: 1792
			private sealed class StringConcatTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046C0 RID: 18112 RVA: 0x000FF6AF File Offset: 0x000FD8AF
				internal StringConcatTranslator() : base(ExpressionConverter.MethodCallTranslator.StringConcatTranslator.GetMethods())
				{
				}

				// Token: 0x060046C1 RID: 18113 RVA: 0x000FF6BC File Offset: 0x000FD8BC
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetMethod("Concat", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(string),
						typeof(string)
					}, null);
					yield return typeof(string).GetMethod("Concat", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string)
					}, null);
					yield return typeof(string).GetMethod("Concat", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string)
					}, null);
					yield break;
				}

				// Token: 0x060046C2 RID: 18114 RVA: 0x000FF6C8 File Offset: 0x000FD8C8
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateExpression(call.Arguments[0]);
					for (int i = 1; i < call.Arguments.Count; i++)
					{
						dbExpression = parent.CreateCanonicalFunction("Concat", call, new DbExpression[]
						{
							dbExpression,
							parent.TranslateExpression(call.Arguments[i])
						});
					}
					return dbExpression;
				}
			}

			// Token: 0x02000701 RID: 1793
			private abstract class TrimBaseTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046C3 RID: 18115 RVA: 0x000FF72B File Offset: 0x000FD92B
				protected TrimBaseTranslator(IEnumerable<MethodInfo> methods, string canonicalFunctionName) : base(methods)
				{
					this._canonicalFunctionName = canonicalFunctionName;
				}

				// Token: 0x060046C4 RID: 18116 RVA: 0x000FF73C File Offset: 0x000FD93C
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (!ExpressionConverter.MethodCallTranslator.TrimBaseTranslator.IsEmptyArray(call.Arguments[0]))
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedTrimStartTrimEndCase(call.Method));
					}
					return parent.TranslateIntoCanonicalFunction(this._canonicalFunctionName, call, new Expression[]
					{
						call.Object
					});
				}

				// Token: 0x060046C5 RID: 18117 RVA: 0x000FF78C File Offset: 0x000FD98C
				internal static bool IsEmptyArray(Expression expression)
				{
					if (expression.NodeType == ExpressionType.NewArrayInit)
					{
						NewArrayExpression newArrayExpression = (NewArrayExpression)expression;
						if (newArrayExpression.Expressions.Count == 0)
						{
							return true;
						}
					}
					else if (expression.NodeType == ExpressionType.NewArrayBounds)
					{
						NewArrayExpression newArrayExpression2 = (NewArrayExpression)expression;
						if (newArrayExpression2.Expressions.Count == 1 && newArrayExpression2.Expressions[0].NodeType == ExpressionType.Constant)
						{
							return object.Equals(((ConstantExpression)newArrayExpression2.Expressions[0]).Value, 0);
						}
					}
					return false;
				}

				// Token: 0x040020A2 RID: 8354
				private string _canonicalFunctionName;
			}

			// Token: 0x02000702 RID: 1794
			private sealed class TrimTranslator : ExpressionConverter.MethodCallTranslator.TrimBaseTranslator
			{
				// Token: 0x060046C6 RID: 18118 RVA: 0x000FF810 File Offset: 0x000FDA10
				internal TrimTranslator() : base(ExpressionConverter.MethodCallTranslator.TrimTranslator.GetMethods(), "Trim")
				{
				}

				// Token: 0x060046C7 RID: 18119 RVA: 0x000FF822 File Offset: 0x000FDA22
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetMethod("Trim", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
					{
						typeof(char[])
					}, null);
					yield break;
				}
			}

			// Token: 0x02000703 RID: 1795
			private sealed class TrimStartTranslator : ExpressionConverter.MethodCallTranslator.TrimBaseTranslator
			{
				// Token: 0x060046C8 RID: 18120 RVA: 0x000FF82B File Offset: 0x000FDA2B
				internal TrimStartTranslator() : base(ExpressionConverter.MethodCallTranslator.TrimStartTranslator.GetMethods(), "LTrim")
				{
				}

				// Token: 0x060046C9 RID: 18121 RVA: 0x000FF83D File Offset: 0x000FDA3D
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetMethod("TrimStart", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
					{
						typeof(char[])
					}, null);
					yield break;
				}
			}

			// Token: 0x02000704 RID: 1796
			private sealed class TrimEndTranslator : ExpressionConverter.MethodCallTranslator.TrimBaseTranslator
			{
				// Token: 0x060046CA RID: 18122 RVA: 0x000FF846 File Offset: 0x000FDA46
				internal TrimEndTranslator() : base(ExpressionConverter.MethodCallTranslator.TrimEndTranslator.GetMethods(), "RTrim")
				{
				}

				// Token: 0x060046CB RID: 18123 RVA: 0x000FF858 File Offset: 0x000FDA58
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetMethod("TrimEnd", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
					{
						typeof(char[])
					}, null);
					yield break;
				}
			}

			// Token: 0x02000705 RID: 1797
			private sealed class VBCanonicalFunctionDefaultTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046CC RID: 18124 RVA: 0x000FF861 File Offset: 0x000FDA61
				internal VBCanonicalFunctionDefaultTranslator(Assembly vbAssembly) : base(ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionDefaultTranslator.GetMethods(vbAssembly))
				{
				}

				// Token: 0x060046CD RID: 18125 RVA: 0x000FF86F File Offset: 0x000FDA6F
				private static IEnumerable<MethodInfo> GetMethods(Assembly vbAssembly)
				{
					Type stringsType = vbAssembly.GetType("Microsoft.VisualBasic.Strings");
					yield return stringsType.GetMethod("Trim", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(string)
					}, null);
					yield return stringsType.GetMethod("LTrim", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(string)
					}, null);
					yield return stringsType.GetMethod("RTrim", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(string)
					}, null);
					yield return stringsType.GetMethod("Left", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(string),
						typeof(int)
					}, null);
					yield return stringsType.GetMethod("Right", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(string),
						typeof(int)
					}, null);
					Type dateTimeType = vbAssembly.GetType("Microsoft.VisualBasic.DateAndTime");
					yield return dateTimeType.GetMethod("Year", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(DateTime)
					}, null);
					yield return dateTimeType.GetMethod("Month", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(DateTime)
					}, null);
					yield return dateTimeType.GetMethod("Day", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(DateTime)
					}, null);
					yield return dateTimeType.GetMethod("Hour", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(DateTime)
					}, null);
					yield return dateTimeType.GetMethod("Minute", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(DateTime)
					}, null);
					yield return dateTimeType.GetMethod("Second", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(DateTime)
					}, null);
					yield break;
				}

				// Token: 0x060046CE RID: 18126 RVA: 0x000FF87F File Offset: 0x000FDA7F
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateIntoCanonicalFunction(call.Method.Name, call, call.Arguments.ToArray<Expression>());
				}

				// Token: 0x040020A3 RID: 8355
				private const string s_stringsTypeFullName = "Microsoft.VisualBasic.Strings";

				// Token: 0x040020A4 RID: 8356
				private const string s_dateAndTimeTypeFullName = "Microsoft.VisualBasic.DateAndTime";
			}

			// Token: 0x02000706 RID: 1798
			private sealed class VBCanonicalFunctionRenameTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046CF RID: 18127 RVA: 0x000FF89E File Offset: 0x000FDA9E
				internal VBCanonicalFunctionRenameTranslator(Assembly vbAssembly) : base(ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethods(vbAssembly))
				{
				}

				// Token: 0x060046D0 RID: 18128 RVA: 0x000FF8AC File Offset: 0x000FDAAC
				private static IEnumerable<MethodInfo> GetMethods(Assembly vbAssembly)
				{
					Type stringsType = vbAssembly.GetType("Microsoft.VisualBasic.Strings");
					yield return ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethod(stringsType, "Len", "Length", new Type[]
					{
						typeof(string)
					});
					yield return ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethod(stringsType, "Mid", "Substring", new Type[]
					{
						typeof(string),
						typeof(int),
						typeof(int)
					});
					yield return ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethod(stringsType, "UCase", "ToUpper", new Type[]
					{
						typeof(string)
					});
					yield return ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethod(stringsType, "LCase", "ToLower", new Type[]
					{
						typeof(string)
					});
					yield break;
				}

				// Token: 0x060046D1 RID: 18129 RVA: 0x000FF8BC File Offset: 0x000FDABC
				private static MethodInfo GetMethod(Type declaringType, string methodName, string canonicalFunctionName, Type[] argumentTypes)
				{
					MethodInfo method = declaringType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public, null, argumentTypes, null);
					ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.s_methodNameMap.Add(method, canonicalFunctionName);
					return method;
				}

				// Token: 0x060046D2 RID: 18130 RVA: 0x000FF8E3 File Offset: 0x000FDAE3
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateIntoCanonicalFunction(ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.s_methodNameMap[call.Method], call, call.Arguments.ToArray<Expression>());
				}

				// Token: 0x040020A5 RID: 8357
				private const string s_stringsTypeFullName = "Microsoft.VisualBasic.Strings";

				// Token: 0x040020A6 RID: 8358
				private static readonly Dictionary<MethodInfo, string> s_methodNameMap = new Dictionary<MethodInfo, string>(4);
			}

			// Token: 0x02000707 RID: 1799
			private sealed class VBDatePartTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060046D4 RID: 18132 RVA: 0x000FF914 File Offset: 0x000FDB14
				internal VBDatePartTranslator(Assembly vbAssembly) : base(ExpressionConverter.MethodCallTranslator.VBDatePartTranslator.GetMethods(vbAssembly))
				{
				}

				// Token: 0x060046D5 RID: 18133 RVA: 0x000FF924 File Offset: 0x000FDB24
				static VBDatePartTranslator()
				{
					ExpressionConverter.MethodCallTranslator.VBDatePartTranslator.s_supportedIntervals.Add("Year");
					ExpressionConverter.MethodCallTranslator.VBDatePartTranslator.s_supportedIntervals.Add("Month");
					ExpressionConverter.MethodCallTranslator.VBDatePartTranslator.s_supportedIntervals.Add("Day");
					ExpressionConverter.MethodCallTranslator.VBDatePartTranslator.s_supportedIntervals.Add("Hour");
					ExpressionConverter.MethodCallTranslator.VBDatePartTranslator.s_supportedIntervals.Add("Minute");
					ExpressionConverter.MethodCallTranslator.VBDatePartTranslator.s_supportedIntervals.Add("Second");
				}

				// Token: 0x060046D6 RID: 18134 RVA: 0x000FF99B File Offset: 0x000FDB9B
				private static IEnumerable<MethodInfo> GetMethods(Assembly vbAssembly)
				{
					Type type = vbAssembly.GetType("Microsoft.VisualBasic.DateAndTime");
					Type type2 = vbAssembly.GetType("Microsoft.VisualBasic.DateInterval");
					Type type3 = vbAssembly.GetType("Microsoft.VisualBasic.FirstDayOfWeek");
					Type type4 = vbAssembly.GetType("Microsoft.VisualBasic.FirstWeekOfYear");
					yield return type.GetMethod("DatePart", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						type2,
						typeof(DateTime),
						type3,
						type4
					}, null);
					yield break;
				}

				// Token: 0x060046D7 RID: 18135 RVA: 0x000FF9AC File Offset: 0x000FDBAC
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					ConstantExpression constantExpression = call.Arguments[0] as ConstantExpression;
					if (constantExpression == null)
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedVBDatePartNonConstantInterval(call.Method, call.Method.GetParameters()[0].Name));
					}
					string text = constantExpression.Value.ToString();
					if (!ExpressionConverter.MethodCallTranslator.VBDatePartTranslator.s_supportedIntervals.Contains(text))
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedVBDatePartInvalidInterval(call.Method, call.Method.GetParameters()[0].Name, text));
					}
					return parent.TranslateIntoCanonicalFunction(text, call, new Expression[]
					{
						call.Arguments[1]
					});
				}

				// Token: 0x040020A7 RID: 8359
				private const string s_dateAndTimeTypeFullName = "Microsoft.VisualBasic.DateAndTime";

				// Token: 0x040020A8 RID: 8360
				private const string s_DateIntervalFullName = "Microsoft.VisualBasic.DateInterval";

				// Token: 0x040020A9 RID: 8361
				private const string s_FirstDayOfWeekFullName = "Microsoft.VisualBasic.FirstDayOfWeek";

				// Token: 0x040020AA RID: 8362
				private const string s_FirstWeekOfYearFullName = "Microsoft.VisualBasic.FirstWeekOfYear";

				// Token: 0x040020AB RID: 8363
				private static HashSet<string> s_supportedIntervals = new HashSet<string>();
			}

			// Token: 0x02000708 RID: 1800
			private abstract class SequenceMethodTranslator
			{
				// Token: 0x060046D8 RID: 18136 RVA: 0x000FFA4D File Offset: 0x000FDC4D
				protected SequenceMethodTranslator(params SequenceMethod[] methods)
				{
					this._methods = methods;
				}

				// Token: 0x17000BD4 RID: 3028
				// (get) Token: 0x060046D9 RID: 18137 RVA: 0x000FFA5C File Offset: 0x000FDC5C
				internal IEnumerable<SequenceMethod> Methods
				{
					get
					{
						return this._methods;
					}
				}

				// Token: 0x060046DA RID: 18138 RVA: 0x000FFA64 File Offset: 0x000FDC64
				internal virtual DbExpression Translate(ExpressionConverter parent, MethodCallExpression call, SequenceMethod sequenceMethod)
				{
					return this.Translate(parent, call);
				}

				// Token: 0x060046DB RID: 18139
				internal abstract DbExpression Translate(ExpressionConverter parent, MethodCallExpression call);

				// Token: 0x060046DC RID: 18140 RVA: 0x000E5329 File Offset: 0x000E3529
				public override string ToString()
				{
					return base.GetType().Name;
				}

				// Token: 0x040020AC RID: 8364
				private readonly IEnumerable<SequenceMethod> _methods;
			}

			// Token: 0x02000709 RID: 1801
			private abstract class PagingTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x060046DD RID: 18141 RVA: 0x000FFA6E File Offset: 0x000FDC6E
				protected PagingTranslator(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x060046DE RID: 18142 RVA: 0x000FFA78 File Offset: 0x000FDC78
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					Expression linq = call.Arguments[1];
					DbExpression count = parent.TranslateExpression(linq);
					return this.TranslatePagingOperator(parent, operand, count);
				}

				// Token: 0x060046DF RID: 18143
				protected abstract DbExpression TranslatePagingOperator(ExpressionConverter parent, DbExpression operand, DbExpression count);
			}

			// Token: 0x0200070A RID: 1802
			private sealed class TakeTranslator : ExpressionConverter.MethodCallTranslator.PagingTranslator
			{
				// Token: 0x060046E0 RID: 18144 RVA: 0x000FFAA5 File Offset: 0x000FDCA5
				internal TakeTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Take
				})
				{
				}

				// Token: 0x060046E1 RID: 18145 RVA: 0x000FFAB8 File Offset: 0x000FDCB8
				protected override DbExpression TranslatePagingOperator(ExpressionConverter parent, DbExpression operand, DbExpression count)
				{
					return parent.Limit(operand, count);
				}
			}

			// Token: 0x0200070B RID: 1803
			private sealed class SkipTranslator : ExpressionConverter.MethodCallTranslator.PagingTranslator
			{
				// Token: 0x060046E2 RID: 18146 RVA: 0x000FFAC2 File Offset: 0x000FDCC2
				internal SkipTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Skip
				})
				{
				}

				// Token: 0x060046E3 RID: 18147 RVA: 0x000FFAD5 File Offset: 0x000FDCD5
				protected override DbExpression TranslatePagingOperator(ExpressionConverter parent, DbExpression operand, DbExpression count)
				{
					return parent.Skip(operand.BindAs(parent.AliasGenerator.Next()), count);
				}
			}

			// Token: 0x0200070C RID: 1804
			private sealed class JoinTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x060046E4 RID: 18148 RVA: 0x000FFAEF File Offset: 0x000FDCEF
				internal JoinTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Join
				})
				{
				}

				// Token: 0x060046E5 RID: 18149 RVA: 0x000FFB04 File Offset: 0x000FDD04
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression input = parent.TranslateSet(call.Arguments[0]);
					DbExpression input2 = parent.TranslateSet(call.Arguments[1]);
					LambdaExpression lambdaExpression = parent.GetLambdaExpression(call, 2);
					LambdaExpression lambdaExpression2 = parent.GetLambdaExpression(call, 3);
					LambdaExpression lambdaExpression3 = parent.GetLambdaExpression(call, 4);
					string bindingName;
					string bindingName2;
					InitializerMetadata initializerMetadata;
					bool flag = ExpressionConverter.MethodCallTranslator.IsTrivialRename(lambdaExpression3, parent, out bindingName, out bindingName2, out initializerMetadata);
					DbExpressionBinding dbExpressionBinding;
					DbExpression dbExpression = flag ? parent.TranslateLambda(lambdaExpression, input, bindingName, out dbExpressionBinding) : parent.TranslateLambda(lambdaExpression, input, out dbExpressionBinding);
					DbExpressionBinding dbExpressionBinding2;
					DbExpression dbExpression2 = flag ? parent.TranslateLambda(lambdaExpression2, input2, bindingName2, out dbExpressionBinding2) : parent.TranslateLambda(lambdaExpression2, input2, out dbExpressionBinding2);
					if (!TypeSemantics.IsEqualComparable(dbExpression.ResultType) || !TypeSemantics.IsEqualComparable(dbExpression2.ResultType))
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedKeySelector(call.Method.Name));
					}
					DbExpression dbExpression3 = parent.CreateEqualsExpression(dbExpression, dbExpression2, ExpressionConverter.EqualsPattern.PositiveNullEqualityNonComposable, lambdaExpression.Body.Type, lambdaExpression2.Body.Type);
					if (flag)
					{
						TypeUsage elementType = TypeUsage.Create(TypeHelpers.CreateRowType(new List<KeyValuePair<string, TypeUsage>>
						{
							new KeyValuePair<string, TypeUsage>(dbExpressionBinding.VariableName, dbExpressionBinding.VariableType),
							new KeyValuePair<string, TypeUsage>(dbExpressionBinding2.VariableName, dbExpressionBinding2.VariableType)
						}, initializerMetadata));
						return new DbJoinExpression(DbExpressionKind.InnerJoin, TypeUsage.Create(TypeHelpers.CreateCollectionType(elementType)), dbExpressionBinding, dbExpressionBinding2, dbExpression3);
					}
					DbJoinExpression input3 = dbExpressionBinding.InnerJoin(dbExpressionBinding2, dbExpression3);
					DbExpressionBinding dbExpressionBinding3 = input3.BindAs(parent.AliasGenerator.Next());
					DbPropertyExpression cqtExpression = dbExpressionBinding3.Variable.Property(dbExpressionBinding.VariableName);
					DbPropertyExpression cqtExpression2 = dbExpressionBinding3.Variable.Property(dbExpressionBinding2.VariableName);
					parent._bindingContext.PushBindingScope(new Binding(lambdaExpression3.Parameters[0], cqtExpression));
					parent._bindingContext.PushBindingScope(new Binding(lambdaExpression3.Parameters[1], cqtExpression2));
					DbExpression projection = parent.TranslateExpression(lambdaExpression3.Body);
					parent._bindingContext.PopBindingScope();
					parent._bindingContext.PopBindingScope();
					return dbExpressionBinding3.Project(projection);
				}
			}

			// Token: 0x0200070D RID: 1805
			private abstract class BinarySequenceMethodTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x060046E6 RID: 18150 RVA: 0x000FFD0E File Offset: 0x000FDF0E
				protected BinarySequenceMethodTranslator(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x060046E7 RID: 18151 RVA: 0x000FFD17 File Offset: 0x000FDF17
				protected DbExpression TranslateLeft(ExpressionConverter parent, Expression expr)
				{
					return parent.TranslateSet(expr);
				}

				// Token: 0x060046E8 RID: 18152 RVA: 0x000FFD17 File Offset: 0x000FDF17
				protected virtual DbExpression TranslateRight(ExpressionConverter parent, Expression expr)
				{
					return parent.TranslateSet(expr);
				}

				// Token: 0x060046E9 RID: 18153 RVA: 0x000FFD20 File Offset: 0x000FDF20
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (call.Object != null)
					{
						DbExpression left = this.TranslateLeft(parent, call.Object);
						DbExpression right = this.TranslateRight(parent, call.Arguments[0]);
						return this.TranslateBinary(parent, left, right);
					}
					DbExpression left2 = this.TranslateLeft(parent, call.Arguments[0]);
					DbExpression right2 = this.TranslateRight(parent, call.Arguments[1]);
					return this.TranslateBinary(parent, left2, right2);
				}

				// Token: 0x060046EA RID: 18154
				protected abstract DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right);
			}

			// Token: 0x0200070E RID: 1806
			private class ConcatTranslator : ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator
			{
				// Token: 0x060046EB RID: 18155 RVA: 0x000FFD92 File Offset: 0x000FDF92
				internal ConcatTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Concat
				})
				{
				}

				// Token: 0x060046EC RID: 18156 RVA: 0x000FFDA5 File Offset: 0x000FDFA5
				protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right)
				{
					return parent.UnionAll(left, right);
				}
			}

			// Token: 0x0200070F RID: 1807
			private sealed class UnionTranslator : ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator
			{
				// Token: 0x060046ED RID: 18157 RVA: 0x000FFDAF File Offset: 0x000FDFAF
				internal UnionTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Union
				})
				{
				}

				// Token: 0x060046EE RID: 18158 RVA: 0x000FFDC2 File Offset: 0x000FDFC2
				protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right)
				{
					return parent.Distinct(parent.UnionAll(left, right));
				}
			}

			// Token: 0x02000710 RID: 1808
			private sealed class IntersectTranslator : ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator
			{
				// Token: 0x060046EF RID: 18159 RVA: 0x000FFDD2 File Offset: 0x000FDFD2
				internal IntersectTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Intersect
				})
				{
				}

				// Token: 0x060046F0 RID: 18160 RVA: 0x000FFDE5 File Offset: 0x000FDFE5
				protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right)
				{
					return parent.Intersect(left, right);
				}
			}

			// Token: 0x02000711 RID: 1809
			private sealed class ExceptTranslator : ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator
			{
				// Token: 0x060046F1 RID: 18161 RVA: 0x000FFDEF File Offset: 0x000FDFEF
				internal ExceptTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Except
				})
				{
				}

				// Token: 0x060046F2 RID: 18162 RVA: 0x000FFE02 File Offset: 0x000FE002
				protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right)
				{
					return parent.Except(left, right);
				}

				// Token: 0x060046F3 RID: 18163 RVA: 0x000FFE0C File Offset: 0x000FE00C
				protected override DbExpression TranslateRight(ExpressionConverter parent, Expression expr)
				{
					int ignoreInclude = parent.IgnoreInclude;
					parent.IgnoreInclude = ignoreInclude + 1;
					DbExpression result = base.TranslateRight(parent, expr);
					ignoreInclude = parent.IgnoreInclude;
					parent.IgnoreInclude = ignoreInclude - 1;
					return result;
				}
			}

			// Token: 0x02000712 RID: 1810
			private abstract class AggregateTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x060046F4 RID: 18164 RVA: 0x000FFE43 File Offset: 0x000FE043
				protected AggregateTranslator(string functionName, bool takesPredicate, params SequenceMethod[] methods) : base(methods)
				{
					this._takesPredicate = takesPredicate;
					this._functionName = functionName;
				}

				// Token: 0x060046F5 RID: 18165 RVA: 0x000FFE5C File Offset: 0x000FE05C
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					bool flag = 1 == call.Arguments.Count;
					DbExpression dbExpression = parent.TranslateSet(call.Arguments[0]);
					if (!flag)
					{
						LambdaExpression lambdaExpression = parent.GetLambdaExpression(call, 1);
						DbExpressionBinding input;
						DbExpression dbExpression2 = parent.TranslateLambda(lambdaExpression, dbExpression, out input);
						if (this._takesPredicate)
						{
							dbExpression = parent.Filter(input, dbExpression2);
						}
						else
						{
							dbExpression = input.Project(dbExpression2);
						}
					}
					TypeUsage returnType = this.GetReturnType(parent, call);
					EdmFunction function = this.FindFunction(parent, call, returnType);
					dbExpression = this.WrapCollectionOperand(parent, dbExpression, returnType);
					DbExpression cqt = function.Invoke(new List<DbExpression>(1)
					{
						dbExpression
					});
					return parent.AlignTypes(cqt, call.Type);
				}

				// Token: 0x060046F6 RID: 18166 RVA: 0x000FFF10 File Offset: 0x000FE110
				protected virtual TypeUsage GetReturnType(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.GetValueLayerType(call.Type);
				}

				// Token: 0x060046F7 RID: 18167 RVA: 0x000FFF20 File Offset: 0x000FE120
				protected virtual DbExpression WrapCollectionOperand(ExpressionConverter parent, DbExpression operand, TypeUsage returnType)
				{
					if (!ExpressionConverter.TypeUsageEquals(returnType, ((CollectionType)operand.ResultType.EdmType).TypeUsage))
					{
						DbExpressionBinding dbExpressionBinding = operand.BindAs(parent.AliasGenerator.Next());
						DbProjectExpression dbProjectExpression = dbExpressionBinding.Project(dbExpressionBinding.Variable.CastTo(returnType));
						operand = dbProjectExpression;
					}
					return operand;
				}

				// Token: 0x060046F8 RID: 18168 RVA: 0x000FFF73 File Offset: 0x000FE173
				protected virtual DbExpression WrapNonCollectionOperand(ExpressionConverter parent, DbExpression operand, TypeUsage returnType)
				{
					if (!ExpressionConverter.TypeUsageEquals(returnType, operand.ResultType))
					{
						operand = operand.CastTo(returnType);
					}
					return operand;
				}

				// Token: 0x060046F9 RID: 18169 RVA: 0x000FFF90 File Offset: 0x000FE190
				protected virtual EdmFunction FindFunction(ExpressionConverter parent, MethodCallExpression call, TypeUsage argumentType)
				{
					List<TypeUsage> list = new List<TypeUsage>(1);
					list.Add(argumentType);
					return parent.FindCanonicalFunction(this._functionName, list, true, call);
				}

				// Token: 0x040020AD RID: 8365
				private readonly string _functionName;

				// Token: 0x040020AE RID: 8366
				private readonly bool _takesPredicate;
			}

			// Token: 0x02000713 RID: 1811
			private sealed class MaxTranslator : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x060046FA RID: 18170 RVA: 0x000FFFBA File Offset: 0x000FE1BA
				internal MaxTranslator() : base("Max", false, new SequenceMethod[]
				{
					SequenceMethod.Max,
					SequenceMethod.MaxSelector,
					SequenceMethod.MaxInt,
					SequenceMethod.MaxIntSelector,
					SequenceMethod.MaxDecimal,
					SequenceMethod.MaxDecimalSelector,
					SequenceMethod.MaxDouble,
					SequenceMethod.MaxDoubleSelector,
					SequenceMethod.MaxLong,
					SequenceMethod.MaxLongSelector,
					SequenceMethod.MaxSingle,
					SequenceMethod.MaxSingleSelector,
					SequenceMethod.MaxNullableDecimal,
					SequenceMethod.MaxNullableDecimalSelector,
					SequenceMethod.MaxNullableDouble,
					SequenceMethod.MaxNullableDoubleSelector,
					SequenceMethod.MaxNullableInt,
					SequenceMethod.MaxNullableIntSelector,
					SequenceMethod.MaxNullableLong,
					SequenceMethod.MaxNullableLongSelector,
					SequenceMethod.MaxNullableSingle,
					SequenceMethod.MaxNullableSingleSelector
				})
				{
				}

				// Token: 0x060046FB RID: 18171 RVA: 0x000FFFDC File Offset: 0x000FE1DC
				protected override TypeUsage GetReturnType(ExpressionConverter parent, MethodCallExpression call)
				{
					TypeUsage returnType = base.GetReturnType(parent, call);
					if (!TypeSemantics.IsEnumerationType(returnType))
					{
						return returnType;
					}
					return TypeUsage.Create(Helper.GetUnderlyingEdmTypeForEnumType(returnType.EdmType), returnType.Facets);
				}
			}

			// Token: 0x02000714 RID: 1812
			private sealed class MinTranslator : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x060046FC RID: 18172 RVA: 0x00100012 File Offset: 0x000FE212
				internal MinTranslator() : base("Min", false, new SequenceMethod[]
				{
					SequenceMethod.Min,
					SequenceMethod.MinSelector,
					SequenceMethod.MinDecimal,
					SequenceMethod.MinDecimalSelector,
					SequenceMethod.MinDouble,
					SequenceMethod.MinDoubleSelector,
					SequenceMethod.MinInt,
					SequenceMethod.MinIntSelector,
					SequenceMethod.MinLong,
					SequenceMethod.MinLongSelector,
					SequenceMethod.MinNullableDecimal,
					SequenceMethod.MinSingle,
					SequenceMethod.MinSingleSelector,
					SequenceMethod.MinNullableDecimalSelector,
					SequenceMethod.MinNullableDouble,
					SequenceMethod.MinNullableDoubleSelector,
					SequenceMethod.MinNullableInt,
					SequenceMethod.MinNullableIntSelector,
					SequenceMethod.MinNullableLong,
					SequenceMethod.MinNullableLongSelector,
					SequenceMethod.MinNullableSingle,
					SequenceMethod.MinNullableSingleSelector
				})
				{
				}

				// Token: 0x060046FD RID: 18173 RVA: 0x00100034 File Offset: 0x000FE234
				protected override TypeUsage GetReturnType(ExpressionConverter parent, MethodCallExpression call)
				{
					TypeUsage returnType = base.GetReturnType(parent, call);
					if (!TypeSemantics.IsEnumerationType(returnType))
					{
						return returnType;
					}
					return TypeUsage.Create(Helper.GetUnderlyingEdmTypeForEnumType(returnType.EdmType), returnType.Facets);
				}
			}

			// Token: 0x02000715 RID: 1813
			private sealed class AverageTranslator : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x060046FE RID: 18174 RVA: 0x0010006A File Offset: 0x000FE26A
				internal AverageTranslator() : base("Avg", false, new SequenceMethod[]
				{
					SequenceMethod.AverageDecimal,
					SequenceMethod.AverageDecimalSelector,
					SequenceMethod.AverageDouble,
					SequenceMethod.AverageDoubleSelector,
					SequenceMethod.AverageInt,
					SequenceMethod.AverageIntSelector,
					SequenceMethod.AverageLong,
					SequenceMethod.AverageLongSelector,
					SequenceMethod.AverageSingle,
					SequenceMethod.AverageSingleSelector,
					SequenceMethod.AverageNullableDecimal,
					SequenceMethod.AverageNullableDecimalSelector,
					SequenceMethod.AverageNullableDouble,
					SequenceMethod.AverageNullableDoubleSelector,
					SequenceMethod.AverageNullableInt,
					SequenceMethod.AverageNullableIntSelector,
					SequenceMethod.AverageNullableLong,
					SequenceMethod.AverageNullableLongSelector,
					SequenceMethod.AverageNullableSingle,
					SequenceMethod.AverageNullableSingleSelector
				})
				{
				}
			}

			// Token: 0x02000716 RID: 1814
			private sealed class SumTranslator : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x060046FF RID: 18175 RVA: 0x0010008A File Offset: 0x000FE28A
				internal SumTranslator() : base("Sum", false, new SequenceMethod[]
				{
					SequenceMethod.SumDecimal,
					SequenceMethod.SumDecimalSelector,
					SequenceMethod.SumDouble,
					SequenceMethod.SumDoubleSelector,
					SequenceMethod.SumInt,
					SequenceMethod.SumIntSelector,
					SequenceMethod.SumLong,
					SequenceMethod.SumLongSelector,
					SequenceMethod.SumSingle,
					SequenceMethod.SumSingleSelector,
					SequenceMethod.SumNullableDecimal,
					SequenceMethod.SumNullableDecimalSelector,
					SequenceMethod.SumNullableDouble,
					SequenceMethod.SumNullableDoubleSelector,
					SequenceMethod.SumNullableInt,
					SequenceMethod.SumNullableIntSelector,
					SequenceMethod.SumNullableLong,
					SequenceMethod.SumNullableLongSelector,
					SequenceMethod.SumNullableSingle,
					SequenceMethod.SumNullableSingleSelector
				})
				{
				}
			}

			// Token: 0x02000717 RID: 1815
			private abstract class CountTranslatorBase : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x06004700 RID: 18176 RVA: 0x001000AA File Offset: 0x000FE2AA
				protected CountTranslatorBase(string functionName, params SequenceMethod[] methods) : base(functionName, true, methods)
				{
				}

				// Token: 0x06004701 RID: 18177 RVA: 0x001000B8 File Offset: 0x000FE2B8
				protected override DbExpression WrapCollectionOperand(ExpressionConverter parent, DbExpression operand, TypeUsage returnType)
				{
					return operand.BindAs(parent.AliasGenerator.Next()).Project(DbExpressionBuilder.Constant(1));
				}

				// Token: 0x06004702 RID: 18178 RVA: 0x001000E8 File Offset: 0x000FE2E8
				protected override DbExpression WrapNonCollectionOperand(ExpressionConverter parent, DbExpression operand, TypeUsage returnType)
				{
					DbExpression dbExpression = DbExpressionBuilder.Constant(1);
					if (!ExpressionConverter.TypeUsageEquals(dbExpression.ResultType, returnType))
					{
						dbExpression = dbExpression.CastTo(returnType);
					}
					return dbExpression;
				}

				// Token: 0x06004703 RID: 18179 RVA: 0x00100118 File Offset: 0x000FE318
				protected override EdmFunction FindFunction(ExpressionConverter parent, MethodCallExpression call, TypeUsage argumentType)
				{
					TypeUsage argumentType2 = TypeUsage.CreateDefaultTypeUsage(EdmProviderManifest.Instance.GetPrimitiveType(PrimitiveTypeKind.Int32));
					return base.FindFunction(parent, call, argumentType2);
				}
			}

			// Token: 0x02000718 RID: 1816
			private sealed class CountTranslator : ExpressionConverter.MethodCallTranslator.CountTranslatorBase
			{
				// Token: 0x06004704 RID: 18180 RVA: 0x00100140 File Offset: 0x000FE340
				internal CountTranslator() : base("Count", new SequenceMethod[]
				{
					SequenceMethod.Count,
					SequenceMethod.CountPredicate
				})
				{
				}
			}

			// Token: 0x02000719 RID: 1817
			private sealed class LongCountTranslator : ExpressionConverter.MethodCallTranslator.CountTranslatorBase
			{
				// Token: 0x06004705 RID: 18181 RVA: 0x0010015D File Offset: 0x000FE35D
				internal LongCountTranslator() : base("BigCount", new SequenceMethod[]
				{
					SequenceMethod.LongCount,
					SequenceMethod.LongCountPredicate
				})
				{
				}
			}

			// Token: 0x0200071A RID: 1818
			private abstract class UnarySequenceMethodTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06004706 RID: 18182 RVA: 0x000FFD0E File Offset: 0x000FDF0E
				protected UnarySequenceMethodTranslator(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x06004707 RID: 18183 RVA: 0x0010017C File Offset: 0x000FE37C
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (call.Object != null)
					{
						DbExpression operand = parent.TranslateSet(call.Object);
						return this.TranslateUnary(parent, operand, call);
					}
					DbExpression operand2 = parent.TranslateSet(call.Arguments[0]);
					return this.TranslateUnary(parent, operand2, call);
				}

				// Token: 0x06004708 RID: 18184
				protected abstract DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call);
			}

			// Token: 0x0200071B RID: 1819
			private sealed class PassthroughTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x06004709 RID: 18185 RVA: 0x001001C4 File Offset: 0x000FE3C4
				internal PassthroughTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.AsQueryableGeneric,
					SequenceMethod.AsQueryable,
					SequenceMethod.AsEnumerable
				})
				{
				}

				// Token: 0x0600470A RID: 18186 RVA: 0x001001DD File Offset: 0x000FE3DD
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					if (TypeSemantics.IsCollectionType(operand.ResultType))
					{
						return operand;
					}
					throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedPassthrough(call.Method.Name, operand.ResultType.EdmType.Name));
				}
			}

			// Token: 0x0200071C RID: 1820
			private sealed class OfTypeTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x0600470B RID: 18187 RVA: 0x00100213 File Offset: 0x000FE413
				internal OfTypeTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.OfType
				})
				{
				}

				// Token: 0x0600470C RID: 18188 RVA: 0x00100228 File Offset: 0x000FE428
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					Type type = call.Method.GetGenericArguments()[0];
					TypeUsage typeUsage;
					if (!parent.TryGetValueLayerType(type, out typeUsage) || (!TypeSemantics.IsEntityType(typeUsage) && !TypeSemantics.IsComplexType(typeUsage)))
					{
						throw EntityUtil.NotSupported(Strings.ELinq_InvalidOfTypeResult(ExpressionConverter.DescribeClrType(type)));
					}
					return parent.OfType(operand, typeUsage);
				}
			}

			// Token: 0x0200071D RID: 1821
			private sealed class DistinctTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x0600470D RID: 18189 RVA: 0x00100279 File Offset: 0x000FE479
				internal DistinctTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Distinct
				})
				{
				}

				// Token: 0x0600470E RID: 18190 RVA: 0x0010028C File Offset: 0x000FE48C
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					return parent.Distinct(operand);
				}
			}

			// Token: 0x0200071E RID: 1822
			private sealed class AnyTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x0600470F RID: 18191 RVA: 0x00100295 File Offset: 0x000FE495
				internal AnyTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Any
				})
				{
				}

				// Token: 0x06004710 RID: 18192 RVA: 0x001002A8 File Offset: 0x000FE4A8
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					return operand.IsEmpty().Not();
				}
			}

			// Token: 0x0200071F RID: 1823
			private abstract class OneLambdaTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06004711 RID: 18193 RVA: 0x000FFD0E File Offset: 0x000FDF0E
				internal OneLambdaTranslator(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x06004712 RID: 18194 RVA: 0x001002B8 File Offset: 0x000FE4B8
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression;
					DbExpressionBinding dbExpressionBinding;
					DbExpression dbExpression2;
					return this.Translate(parent, call, out dbExpression, out dbExpressionBinding, out dbExpression2);
				}

				// Token: 0x06004713 RID: 18195 RVA: 0x001002D4 File Offset: 0x000FE4D4
				protected DbExpression Translate(ExpressionConverter parent, MethodCallExpression call, out DbExpression source, out DbExpressionBinding sourceBinding, out DbExpression lambda)
				{
					source = parent.TranslateExpression(call.Arguments[0]);
					LambdaExpression lambdaExpression = parent.GetLambdaExpression(call, 1);
					lambda = parent.TranslateLambda(lambdaExpression, source, out sourceBinding);
					return this.TranslateOneLambda(parent, sourceBinding, lambda);
				}

				// Token: 0x06004714 RID: 18196
				protected abstract DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda);
			}

			// Token: 0x02000720 RID: 1824
			private sealed class AnyPredicateTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x06004715 RID: 18197 RVA: 0x00100319 File Offset: 0x000FE519
				internal AnyPredicateTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.AnyPredicate
				})
				{
				}

				// Token: 0x06004716 RID: 18198 RVA: 0x0010032C File Offset: 0x000FE52C
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return sourceBinding.Any(lambda);
				}
			}

			// Token: 0x02000721 RID: 1825
			private sealed class AllTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x06004717 RID: 18199 RVA: 0x00100335 File Offset: 0x000FE535
				internal AllTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.All
				})
				{
				}

				// Token: 0x06004718 RID: 18200 RVA: 0x00100348 File Offset: 0x000FE548
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return sourceBinding.All(lambda);
				}
			}

			// Token: 0x02000722 RID: 1826
			private sealed class WhereTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x06004719 RID: 18201 RVA: 0x00100351 File Offset: 0x000FE551
				internal WhereTranslator() : base(new SequenceMethod[1])
				{
				}

				// Token: 0x0600471A RID: 18202 RVA: 0x0010035F File Offset: 0x000FE55F
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return parent.Filter(sourceBinding, lambda);
				}
			}

			// Token: 0x02000723 RID: 1827
			private sealed class SelectTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x0600471B RID: 18203 RVA: 0x00100369 File Offset: 0x000FE569
				internal SelectTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Select
				})
				{
				}

				// Token: 0x0600471C RID: 18204 RVA: 0x0010037C File Offset: 0x000FE57C
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression;
					DbExpressionBinding dbExpressionBinding;
					DbExpression dbExpression2;
					return base.Translate(parent, call, out dbExpression, out dbExpressionBinding, out dbExpression2);
				}

				// Token: 0x0600471D RID: 18205 RVA: 0x00100399 File Offset: 0x000FE599
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return parent.Project(sourceBinding, lambda);
				}
			}

			// Token: 0x02000724 RID: 1828
			private sealed class DefaultIfEmptyTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x0600471E RID: 18206 RVA: 0x001003A3 File Offset: 0x000FE5A3
				internal DefaultIfEmptyTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.DefaultIfEmpty,
					SequenceMethod.DefaultIfEmptyValue
				})
				{
				}

				// Token: 0x0600471F RID: 18207 RVA: 0x001003BC File Offset: 0x000FE5BC
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateSet(call.Arguments[0]);
					DbExpression dbExpression2 = (call.Arguments.Count == 2) ? parent.TranslateExpression(call.Arguments[1]) : ExpressionConverter.MethodCallTranslator.DefaultIfEmptyTranslator.GetDefaultValue(parent, call.Type);
					DbExpression input = DbExpressionBuilder.NewCollection(new DbExpression[]
					{
						new byte?((byte)1)
					});
					DbExpressionBinding left = input.BindAs(parent.AliasGenerator.Next());
					bool flag = dbExpression2 != null && dbExpression2.ExpressionKind != DbExpressionKind.Null;
					if (flag)
					{
						DbExpressionBinding dbExpressionBinding = dbExpression.BindAs(parent.AliasGenerator.Next());
						dbExpression = dbExpressionBinding.Project(new Row(new byte?((byte)1).As("sentinel"), new KeyValuePair<string, DbExpression>[]
						{
							dbExpressionBinding.Variable.As("value")
						}));
					}
					DbExpressionBinding dbExpressionBinding2 = dbExpression.BindAs(parent.AliasGenerator.Next());
					DbExpression input2 = left.LeftOuterJoin(dbExpressionBinding2, new bool?(true));
					DbExpressionBinding dbExpressionBinding3 = input2.BindAs(parent.AliasGenerator.Next());
					DbExpression dbExpression3 = dbExpressionBinding3.Variable.Property(dbExpressionBinding2.VariableName);
					if (flag)
					{
						dbExpression3 = DbExpressionBuilder.Case(new DbIsNullExpression[]
						{
							dbExpression3.Property("sentinel").IsNull()
						}, new DbExpression[]
						{
							dbExpression2
						}, dbExpression3.Property("value"));
					}
					DbExpression dbExpression4 = dbExpressionBinding3.Project(dbExpression3);
					parent.ApplySpanMapping(dbExpression, dbExpression4);
					return dbExpression4;
				}

				// Token: 0x06004720 RID: 18208 RVA: 0x0010054C File Offset: 0x000FE74C
				private static DbExpression GetDefaultValue(ExpressionConverter parent, Type resultType)
				{
					Type elementType = TypeSystem.GetElementType(resultType);
					object defaultValue = TypeSystem.GetDefaultValue(elementType);
					return (defaultValue == null) ? null : parent.TranslateExpression(Expression.Constant(defaultValue, elementType));
				}
			}

			// Token: 0x02000725 RID: 1829
			private sealed class ContainsTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06004721 RID: 18209 RVA: 0x0010057C File Offset: 0x000FE77C
				internal ContainsTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Contains
				})
				{
				}

				// Token: 0x06004722 RID: 18210 RVA: 0x0010058F File Offset: 0x000FE78F
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContains(parent, call.Arguments[0], call.Arguments[1]);
				}

				// Token: 0x06004723 RID: 18211 RVA: 0x001005B0 File Offset: 0x000FE7B0
				private static DbExpression TranslateContainsHelper(ExpressionConverter parent, DbExpression left, IEnumerable<DbExpression> rightList, ExpressionConverter.EqualsPattern pattern, Type leftType, Type rightType)
				{
					IEnumerable<DbExpression> collection = from argument in rightList
					select parent.CreateEqualsExpression(left, argument, pattern, leftType, rightType);
					List<DbExpression> nodes = new List<DbExpression>(collection);
					return Helpers.BuildBalancedTreeInPlace<DbExpression>(nodes, (DbExpression prev, DbExpression next) => prev.Or(next));
				}

				// Token: 0x06004724 RID: 18212 RVA: 0x0010062C File Offset: 0x000FE82C
				internal static DbExpression TranslateContains(ExpressionConverter parent, Expression sourceExpression, Expression valueExpression)
				{
					DbExpression dbExpression = parent.NormalizeSetSource(parent.TranslateExpression(sourceExpression));
					DbExpression dbExpression2 = parent.TranslateExpression(valueExpression);
					Type elementType = TypeSystem.GetElementType(sourceExpression.Type);
					if (dbExpression.ExpressionKind != DbExpressionKind.NewInstance)
					{
						DbExpressionBinding dbExpressionBinding = dbExpression.BindAs(parent.AliasGenerator.Next());
						ExpressionConverter.EqualsPattern pattern = ExpressionConverter.EqualsPattern.Store;
						if (parent._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior)
						{
							pattern = ExpressionConverter.EqualsPattern.PositiveNullEqualityComposable;
						}
						return dbExpressionBinding.Filter(parent.CreateEqualsExpression(dbExpressionBinding.Variable, dbExpression2, pattern, elementType, valueExpression.Type)).Exists();
					}
					IList<DbExpression> arguments = ((DbNewInstanceExpression)dbExpression).Arguments;
					if (arguments.Count <= 0)
					{
						return new bool?(false);
					}
					if (!parent._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior)
					{
						return ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContainsHelper(parent, dbExpression2, arguments, ExpressionConverter.EqualsPattern.Store, elementType, valueExpression.Type);
					}
					IEnumerable<DbExpression> enumerable = from argument in arguments
					where argument.ExpressionKind == DbExpressionKind.Constant
					select argument;
					DbExpression dbExpression3 = null;
					if (enumerable.Count<DbExpression>() > 0)
					{
						dbExpression3 = ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContainsHelper(parent, dbExpression2, enumerable, ExpressionConverter.EqualsPattern.PositiveNullEqualityNonComposable, elementType, valueExpression.Type);
						dbExpression3 = dbExpression3.And(dbExpression2.IsNull().Not());
					}
					IEnumerable<DbExpression> enumerable2 = from argument in arguments
					where argument.ExpressionKind != DbExpressionKind.Constant
					select argument;
					DbExpression dbExpression4 = null;
					if (enumerable2.Count<DbExpression>() > 0)
					{
						dbExpression4 = ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContainsHelper(parent, dbExpression2, enumerable2, ExpressionConverter.EqualsPattern.PositiveNullEqualityComposable, elementType, valueExpression.Type);
					}
					if (dbExpression3 == null)
					{
						return dbExpression4;
					}
					if (dbExpression4 == null)
					{
						return dbExpression3;
					}
					return dbExpression3.Or(dbExpression4);
				}
			}

			// Token: 0x02000726 RID: 1830
			private abstract class FirstTranslatorBase : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x06004725 RID: 18213 RVA: 0x000FFA6E File Offset: 0x000FDC6E
				protected FirstTranslatorBase(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x06004726 RID: 18214 RVA: 0x001007C3 File Offset: 0x000FE9C3
				protected virtual DbExpression LimitResult(ExpressionConverter parent, DbExpression expression)
				{
					return parent.Limit(expression, DbExpressionBuilder.Constant(1));
				}

				// Token: 0x06004727 RID: 18215 RVA: 0x001007D8 File Offset: 0x000FE9D8
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					DbExpression dbExpression = this.LimitResult(parent, operand);
					if (!parent.IsQueryRoot(call))
					{
						dbExpression = dbExpression.Element();
						dbExpression = ExpressionConverter.MethodCallTranslator.FirstTranslatorBase.AddDefaultCase(parent, dbExpression, call.Type);
					}
					Span span = null;
					if (parent.TryGetSpan(operand, out span))
					{
						parent.AddSpanMapping(dbExpression, span);
					}
					return dbExpression;
				}

				// Token: 0x06004728 RID: 18216 RVA: 0x00100824 File Offset: 0x000FEA24
				internal static DbExpression AddDefaultCase(ExpressionConverter parent, DbExpression element, Type elementType)
				{
					object defaultValue = TypeSystem.GetDefaultValue(elementType);
					if (defaultValue == null)
					{
						return element;
					}
					return DbExpressionBuilder.Case(new List<DbExpression>(1)
					{
						parent.CreateIsNullExpression(element, elementType)
					}, new List<DbExpression>(1)
					{
						element.ResultType.Constant(defaultValue)
					}, element);
				}
			}

			// Token: 0x02000727 RID: 1831
			private sealed class FirstTranslator : ExpressionConverter.MethodCallTranslator.FirstTranslatorBase
			{
				// Token: 0x06004729 RID: 18217 RVA: 0x00100875 File Offset: 0x000FEA75
				internal FirstTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.First
				})
				{
				}

				// Token: 0x0600472A RID: 18218 RVA: 0x00100888 File Offset: 0x000FEA88
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					if (!parent.IsQueryRoot(call))
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedNestedFirst);
					}
					return base.TranslateUnary(parent, operand, call);
				}
			}

			// Token: 0x02000728 RID: 1832
			private sealed class FirstOrDefaultTranslator : ExpressionConverter.MethodCallTranslator.FirstTranslatorBase
			{
				// Token: 0x0600472B RID: 18219 RVA: 0x001008A7 File Offset: 0x000FEAA7
				internal FirstOrDefaultTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.FirstOrDefault
				})
				{
				}
			}

			// Token: 0x02000729 RID: 1833
			private abstract class SingleTranslatorBase : ExpressionConverter.MethodCallTranslator.FirstTranslatorBase
			{
				// Token: 0x0600472C RID: 18220 RVA: 0x001008BA File Offset: 0x000FEABA
				protected SingleTranslatorBase(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x0600472D RID: 18221 RVA: 0x001008C3 File Offset: 0x000FEAC3
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					if (!parent.IsQueryRoot(call))
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedNestedSingle);
					}
					return base.TranslateUnary(parent, operand, call);
				}

				// Token: 0x0600472E RID: 18222 RVA: 0x001008E2 File Offset: 0x000FEAE2
				protected override DbExpression LimitResult(ExpressionConverter parent, DbExpression expression)
				{
					return parent.Limit(expression, DbExpressionBuilder.Constant(2));
				}
			}

			// Token: 0x0200072A RID: 1834
			private sealed class SingleTranslator : ExpressionConverter.MethodCallTranslator.SingleTranslatorBase
			{
				// Token: 0x0600472F RID: 18223 RVA: 0x001008F6 File Offset: 0x000FEAF6
				internal SingleTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Single
				})
				{
				}
			}

			// Token: 0x0200072B RID: 1835
			private sealed class SingleOrDefaultTranslator : ExpressionConverter.MethodCallTranslator.SingleTranslatorBase
			{
				// Token: 0x06004730 RID: 18224 RVA: 0x00100909 File Offset: 0x000FEB09
				internal SingleOrDefaultTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.SingleOrDefault
				})
				{
				}
			}

			// Token: 0x0200072C RID: 1836
			private abstract class FirstPredicateTranslatorBase : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x06004731 RID: 18225 RVA: 0x0010091C File Offset: 0x000FEB1C
				protected FirstPredicateTranslatorBase(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x06004732 RID: 18226 RVA: 0x001007C3 File Offset: 0x000FE9C3
				protected virtual DbExpression RestrictResult(ExpressionConverter parent, DbExpression expression)
				{
					return parent.Limit(expression, DbExpressionBuilder.Constant(1));
				}

				// Token: 0x06004733 RID: 18227 RVA: 0x00100928 File Offset: 0x000FEB28
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = base.Translate(parent, call);
					if (parent.IsQueryRoot(call))
					{
						return this.RestrictResult(parent, dbExpression);
					}
					DbExpression dbExpression2 = dbExpression.Element();
					dbExpression2 = ExpressionConverter.MethodCallTranslator.FirstTranslatorBase.AddDefaultCase(parent, dbExpression2, call.Type);
					Span span = null;
					if (parent.TryGetSpan(dbExpression, out span))
					{
						parent.AddSpanMapping(dbExpression2, span);
					}
					return dbExpression2;
				}

				// Token: 0x06004734 RID: 18228 RVA: 0x0010035F File Offset: 0x000FE55F
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return parent.Filter(sourceBinding, lambda);
				}
			}

			// Token: 0x0200072D RID: 1837
			private sealed class FirstPredicateTranslator : ExpressionConverter.MethodCallTranslator.FirstPredicateTranslatorBase
			{
				// Token: 0x06004735 RID: 18229 RVA: 0x0010097C File Offset: 0x000FEB7C
				internal FirstPredicateTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.FirstPredicate
				})
				{
				}

				// Token: 0x06004736 RID: 18230 RVA: 0x0010098F File Offset: 0x000FEB8F
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (!parent.IsQueryRoot(call))
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedNestedFirst);
					}
					return base.Translate(parent, call);
				}
			}

			// Token: 0x0200072E RID: 1838
			private sealed class FirstOrDefaultPredicateTranslator : ExpressionConverter.MethodCallTranslator.FirstPredicateTranslatorBase
			{
				// Token: 0x06004737 RID: 18231 RVA: 0x001009AD File Offset: 0x000FEBAD
				internal FirstOrDefaultPredicateTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.FirstOrDefaultPredicate
				})
				{
				}
			}

			// Token: 0x0200072F RID: 1839
			private abstract class SinglePredicateTranslatorBase : ExpressionConverter.MethodCallTranslator.FirstPredicateTranslatorBase
			{
				// Token: 0x06004738 RID: 18232 RVA: 0x001009C0 File Offset: 0x000FEBC0
				protected SinglePredicateTranslatorBase(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x06004739 RID: 18233 RVA: 0x001009C9 File Offset: 0x000FEBC9
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (!parent.IsQueryRoot(call))
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedNestedSingle);
					}
					return base.Translate(parent, call);
				}

				// Token: 0x0600473A RID: 18234 RVA: 0x001008E2 File Offset: 0x000FEAE2
				protected override DbExpression RestrictResult(ExpressionConverter parent, DbExpression expression)
				{
					return parent.Limit(expression, DbExpressionBuilder.Constant(2));
				}
			}

			// Token: 0x02000730 RID: 1840
			private sealed class SinglePredicateTranslator : ExpressionConverter.MethodCallTranslator.SinglePredicateTranslatorBase
			{
				// Token: 0x0600473B RID: 18235 RVA: 0x001009E7 File Offset: 0x000FEBE7
				internal SinglePredicateTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.SinglePredicate
				})
				{
				}
			}

			// Token: 0x02000731 RID: 1841
			private sealed class SingleOrDefaultPredicateTranslator : ExpressionConverter.MethodCallTranslator.SinglePredicateTranslatorBase
			{
				// Token: 0x0600473C RID: 18236 RVA: 0x001009FA File Offset: 0x000FEBFA
				internal SingleOrDefaultPredicateTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.SingleOrDefaultPredicate
				})
				{
				}
			}

			// Token: 0x02000732 RID: 1842
			private sealed class SelectManyTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x0600473D RID: 18237 RVA: 0x00100A0D File Offset: 0x000FEC0D
				internal SelectManyTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.SelectMany,
					SequenceMethod.SelectManyResultSelector
				})
				{
				}

				// Token: 0x0600473E RID: 18238 RVA: 0x00100A24 File Offset: 0x000FEC24
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					LambdaExpression lambdaExpression = (call.Arguments.Count == 3) ? parent.GetLambdaExpression(call, 2) : null;
					DbExpression dbExpression = base.Translate(parent, call);
					DbExpressionBinding dbExpressionBinding;
					EdmProperty edmProperty;
					bool flag = ExpressionConverter.MethodCallTranslator.SelectManyTranslator.IsLeftOuterJoin(dbExpression, out dbExpressionBinding, out edmProperty);
					if (flag)
					{
						string varName;
						string varName2;
						InitializerMetadata initializerMetadata;
						if (lambdaExpression != null && ExpressionConverter.MethodCallTranslator.IsTrivialRename(lambdaExpression, parent, out varName, out varName2, out initializerMetadata))
						{
							DbExpressionBinding dbExpressionBinding2 = dbExpressionBinding.Expression.BindAs(varName);
							DbExpressionBinding dbExpressionBinding3 = dbExpressionBinding2.Variable.Property(edmProperty.Name).BindAs(varName2);
							TypeUsage elementType = TypeUsage.Create(TypeHelpers.CreateRowType(new List<KeyValuePair<string, TypeUsage>>
							{
								new KeyValuePair<string, TypeUsage>(dbExpressionBinding2.VariableName, dbExpressionBinding2.VariableType),
								new KeyValuePair<string, TypeUsage>(dbExpressionBinding3.VariableName, dbExpressionBinding3.VariableType)
							}, initializerMetadata));
							return new DbApplyExpression(DbExpressionKind.OuterApply, TypeUsage.Create(TypeHelpers.CreateCollectionType(elementType)), dbExpressionBinding2, dbExpressionBinding3);
						}
						dbExpression = dbExpressionBinding.OuterApply(dbExpressionBinding.Variable.Property(edmProperty).BindAs(parent.AliasGenerator.Next()));
					}
					DbExpressionBinding dbExpressionBinding4 = dbExpression.BindAs(parent.AliasGenerator.Next());
					RowType rowType = (RowType)dbExpressionBinding4.Variable.ResultType.EdmType;
					DbExpression dbExpression2 = dbExpressionBinding4.Variable.Property(rowType.Properties[1]);
					DbExpression projection;
					if (lambdaExpression != null)
					{
						DbExpression cqtExpression = dbExpressionBinding4.Variable.Property(rowType.Properties[0]);
						parent._bindingContext.PushBindingScope(new Binding(lambdaExpression.Parameters[0], cqtExpression));
						parent._bindingContext.PushBindingScope(new Binding(lambdaExpression.Parameters[1], dbExpression2));
						projection = parent.TranslateSet(lambdaExpression.Body);
						parent._bindingContext.PopBindingScope();
						parent._bindingContext.PopBindingScope();
					}
					else
					{
						projection = dbExpression2;
					}
					return dbExpressionBinding4.Project(projection);
				}

				// Token: 0x0600473F RID: 18239 RVA: 0x00100BFC File Offset: 0x000FEDFC
				private static bool IsLeftOuterJoin(DbExpression cqtExpression, out DbExpressionBinding crossApplyInput, out EdmProperty lojRightInput)
				{
					crossApplyInput = null;
					lojRightInput = null;
					if (cqtExpression.ExpressionKind != DbExpressionKind.CrossApply)
					{
						return false;
					}
					DbApplyExpression dbApplyExpression = (DbApplyExpression)cqtExpression;
					if (dbApplyExpression.Input.VariableType.EdmType.BuiltInTypeKind != BuiltInTypeKind.RowType)
					{
						return false;
					}
					RowType rowType = (RowType)dbApplyExpression.Input.VariableType.EdmType;
					if (dbApplyExpression.Apply.Expression.ExpressionKind != DbExpressionKind.Project)
					{
						return false;
					}
					DbProjectExpression dbProjectExpression = (DbProjectExpression)dbApplyExpression.Apply.Expression;
					if (dbProjectExpression.Input.Expression.ExpressionKind != DbExpressionKind.LeftOuterJoin)
					{
						return false;
					}
					DbJoinExpression dbJoinExpression = (DbJoinExpression)dbProjectExpression.Input.Expression;
					if (dbProjectExpression.Projection.ExpressionKind != DbExpressionKind.Property)
					{
						return false;
					}
					DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)dbProjectExpression.Projection;
					if (dbPropertyExpression.Instance != dbProjectExpression.Input.Variable || dbPropertyExpression.Property.Name != dbJoinExpression.Right.VariableName || dbJoinExpression.JoinCondition.ExpressionKind != DbExpressionKind.Constant)
					{
						return false;
					}
					DbConstantExpression dbConstantExpression = (DbConstantExpression)dbJoinExpression.JoinCondition;
					if (!(dbConstantExpression.Value is bool) || !(bool)dbConstantExpression.Value)
					{
						return false;
					}
					if (dbJoinExpression.Left.Expression.ExpressionKind != DbExpressionKind.NewInstance)
					{
						return false;
					}
					DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)dbJoinExpression.Left.Expression;
					if (dbNewInstanceExpression.Arguments.Count != 1 || dbNewInstanceExpression.Arguments[0].ExpressionKind != DbExpressionKind.Constant)
					{
						return false;
					}
					if (dbJoinExpression.Right.Expression.ExpressionKind != DbExpressionKind.Property)
					{
						return false;
					}
					DbPropertyExpression lojRight = (DbPropertyExpression)dbJoinExpression.Right.Expression;
					if (lojRight.Instance != dbApplyExpression.Input.Variable)
					{
						return false;
					}
					EdmProperty edmProperty = rowType.Properties.SingleOrDefault((EdmProperty p) => p.Name == lojRight.Property.Name);
					if (edmProperty == null)
					{
						return false;
					}
					crossApplyInput = dbApplyExpression.Input;
					lojRightInput = edmProperty;
					return true;
				}

				// Token: 0x06004740 RID: 18240 RVA: 0x00100DFC File Offset: 0x000FEFFC
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					lambda = parent.NormalizeSetSource(lambda);
					DbExpressionBinding apply = lambda.BindAs(parent.AliasGenerator.Next());
					return sourceBinding.CrossApply(apply);
				}
			}

			// Token: 0x02000733 RID: 1843
			private sealed class CastMethodTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06004741 RID: 18241 RVA: 0x00100E2D File Offset: 0x000FF02D
				internal CastMethodTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Cast
				})
				{
				}

				// Token: 0x06004742 RID: 18242 RVA: 0x00100E40 File Offset: 0x000FF040
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression input = parent.TranslateSet(call.Arguments[0]);
					Type elementType = TypeSystem.GetElementType(call.Type);
					Type elementType2 = TypeSystem.GetElementType(call.Arguments[0].Type);
					DbExpressionBinding dbExpressionBinding = input.BindAs(parent.AliasGenerator.Next());
					DbExpression projection = parent.CreateCastExpression(dbExpressionBinding.Variable, elementType, elementType2);
					return parent.Project(dbExpressionBinding, projection);
				}
			}

			// Token: 0x02000734 RID: 1844
			private sealed class GroupByTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06004743 RID: 18243 RVA: 0x00100EAE File Offset: 0x000FF0AE
				internal GroupByTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.GroupBy,
					SequenceMethod.GroupByElementSelector,
					SequenceMethod.GroupByElementSelectorResultSelector,
					SequenceMethod.GroupByResultSelector
				})
				{
				}

				// Token: 0x06004744 RID: 18244 RVA: 0x00100EC8 File Offset: 0x000FF0C8
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call, SequenceMethod sequenceMethod)
				{
					DbExpression input = parent.TranslateSet(call.Arguments[0]);
					LambdaExpression lambdaExpression = parent.GetLambdaExpression(call, 1);
					DbGroupExpressionBinding dbGroupExpressionBinding;
					DbExpression dbExpression = parent.TranslateLambda(lambdaExpression, input, out dbGroupExpressionBinding);
					if (!TypeSemantics.IsEqualComparable(dbExpression.ResultType))
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedKeySelector(call.Method.Name));
					}
					List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>();
					List<KeyValuePair<string, DbAggregate>> list2 = new List<KeyValuePair<string, DbAggregate>>();
					list.Add(new KeyValuePair<string, DbExpression>("Key", dbExpression));
					list2.Add(new KeyValuePair<string, DbAggregate>("Group", dbGroupExpressionBinding.GroupAggregate));
					DbExpression input2 = dbGroupExpressionBinding.GroupBy(list, list2);
					DbExpressionBinding dbExpressionBinding = input2.BindAs(parent.AliasGenerator.Next());
					DbExpression dbExpression2 = dbExpressionBinding.Variable.Property("Group");
					bool flag = sequenceMethod == SequenceMethod.GroupByElementSelector || sequenceMethod == SequenceMethod.GroupByElementSelectorResultSelector;
					if (flag)
					{
						LambdaExpression lambdaExpression2 = parent.GetLambdaExpression(call, 2);
						DbExpressionBinding input3;
						DbExpression projection = parent.TranslateLambda(lambdaExpression2, dbExpression2, out input3);
						dbExpression2 = input3.Project(projection);
					}
					DbExpression[] array = new DbExpression[]
					{
						dbExpressionBinding.Variable.Property("Key"),
						dbExpression2
					};
					List<EdmProperty> list3 = new List<EdmProperty>(2);
					list3.Add(new EdmProperty("Key", array[0].ResultType));
					list3.Add(new EdmProperty("Group", array[1].ResultType));
					InitializerMetadata initializerMetadata = InitializerMetadata.CreateGroupingInitializer(parent.EdmItemCollection, TypeSystem.GetElementType(call.Type));
					RowType edmType = new RowType(list3, initializerMetadata);
					TypeUsage instanceType = TypeUsage.Create(edmType);
					DbExpression dbExpression3 = dbExpressionBinding.Project(instanceType.New(array));
					DbExpression result = dbExpression3;
					return ExpressionConverter.MethodCallTranslator.GroupByTranslator.ProcessResultSelector(parent, call, sequenceMethod, dbExpression3, result);
				}

				// Token: 0x06004745 RID: 18245 RVA: 0x00101070 File Offset: 0x000FF270
				private static DbExpression ProcessResultSelector(ExpressionConverter parent, MethodCallExpression call, SequenceMethod sequenceMethod, DbExpression topLevelProject, DbExpression result)
				{
					LambdaExpression lambdaExpression = null;
					if (sequenceMethod == SequenceMethod.GroupByResultSelector)
					{
						lambdaExpression = parent.GetLambdaExpression(call, 2);
					}
					else if (sequenceMethod == SequenceMethod.GroupByElementSelectorResultSelector)
					{
						lambdaExpression = parent.GetLambdaExpression(call, 3);
					}
					if (lambdaExpression != null)
					{
						DbExpressionBinding dbExpressionBinding = topLevelProject.BindAs(parent.AliasGenerator.Next());
						DbPropertyExpression cqtExpression = dbExpressionBinding.Variable.Property("Key");
						DbPropertyExpression cqtExpression2 = dbExpressionBinding.Variable.Property("Group");
						parent._bindingContext.PushBindingScope(new Binding(lambdaExpression.Parameters[0], cqtExpression));
						parent._bindingContext.PushBindingScope(new Binding(lambdaExpression.Parameters[1], cqtExpression2));
						DbExpression projection = parent.TranslateExpression(lambdaExpression.Body);
						result = dbExpressionBinding.Project(projection);
						parent._bindingContext.PopBindingScope();
						parent._bindingContext.PopBindingScope();
					}
					return result;
				}

				// Token: 0x06004746 RID: 18246 RVA: 0x00006174 File Offset: 0x00004374
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return null;
				}
			}

			// Token: 0x02000735 RID: 1845
			private sealed class GroupJoinTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06004747 RID: 18247 RVA: 0x00101141 File Offset: 0x000FF341
				internal GroupJoinTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.GroupJoin
				})
				{
				}

				// Token: 0x06004748 RID: 18248 RVA: 0x00101154 File Offset: 0x000FF354
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression input = parent.TranslateSet(call.Arguments[0]);
					DbExpression input2 = parent.TranslateSet(call.Arguments[1]);
					LambdaExpression lambdaExpression = parent.GetLambdaExpression(call, 2);
					LambdaExpression lambdaExpression2 = parent.GetLambdaExpression(call, 3);
					DbExpressionBinding dbExpressionBinding;
					DbExpression dbExpression = parent.TranslateLambda(lambdaExpression, input, out dbExpressionBinding);
					DbExpressionBinding input3;
					DbExpression dbExpression2 = parent.TranslateLambda(lambdaExpression2, input2, out input3);
					if (!TypeSemantics.IsEqualComparable(dbExpression.ResultType) || !TypeSemantics.IsEqualComparable(dbExpression2.ResultType))
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedKeySelector(call.Method.Name));
					}
					DbExpression value = parent.Filter(input3, parent.CreateEqualsExpression(dbExpression, dbExpression2, ExpressionConverter.EqualsPattern.PositiveNullEqualityNonComposable, lambdaExpression.Body.Type, lambdaExpression2.Body.Type));
					DbExpression projection = DbExpressionBuilder.NewRow(new List<KeyValuePair<string, DbExpression>>(2)
					{
						new KeyValuePair<string, DbExpression>("o", dbExpressionBinding.Variable),
						new KeyValuePair<string, DbExpression>("i", value)
					});
					DbExpression input4 = dbExpressionBinding.Project(projection);
					DbExpressionBinding dbExpressionBinding2 = input4.BindAs(parent.AliasGenerator.Next());
					DbExpression cqtExpression = dbExpressionBinding2.Variable.Property("o");
					DbExpression cqtExpression2 = dbExpressionBinding2.Variable.Property("i");
					LambdaExpression lambdaExpression3 = parent.GetLambdaExpression(call, 4);
					parent._bindingContext.PushBindingScope(new Binding(lambdaExpression3.Parameters[0], cqtExpression));
					parent._bindingContext.PushBindingScope(new Binding(lambdaExpression3.Parameters[1], cqtExpression2));
					DbExpression projection2 = parent.TranslateExpression(lambdaExpression3.Body);
					parent._bindingContext.PopBindingScope();
					parent._bindingContext.PopBindingScope();
					DbExpression cqtExpression3 = dbExpressionBinding2.Project(projection2);
					return this.CollapseTrivialRenamingProjection(cqtExpression3);
				}

				// Token: 0x06004749 RID: 18249 RVA: 0x00101318 File Offset: 0x000FF518
				private DbExpression CollapseTrivialRenamingProjection(DbExpression cqtExpression)
				{
					if (cqtExpression.ExpressionKind != DbExpressionKind.Project)
					{
						return cqtExpression;
					}
					DbProjectExpression dbProjectExpression = (DbProjectExpression)cqtExpression;
					if (dbProjectExpression.Projection.ExpressionKind != DbExpressionKind.NewInstance || dbProjectExpression.Projection.ResultType.EdmType.BuiltInTypeKind != BuiltInTypeKind.RowType)
					{
						return cqtExpression;
					}
					DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)dbProjectExpression.Projection;
					RowType rowType = (RowType)dbNewInstanceExpression.ResultType.EdmType;
					List<Tuple<EdmProperty, string>> list = new List<Tuple<EdmProperty, string>>();
					for (int i = 0; i < dbNewInstanceExpression.Arguments.Count; i++)
					{
						if (dbNewInstanceExpression.Arguments[i].ExpressionKind != DbExpressionKind.Property)
						{
							return cqtExpression;
						}
						DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)dbNewInstanceExpression.Arguments[i];
						if (dbPropertyExpression.Instance != dbProjectExpression.Input.Variable)
						{
							return cqtExpression;
						}
						list.Add(Tuple.Create<EdmProperty, string>((EdmProperty)dbPropertyExpression.Property, rowType.Properties[i].Name));
					}
					if (dbProjectExpression.Input.Expression.ExpressionKind != DbExpressionKind.Project)
					{
						return cqtExpression;
					}
					DbProjectExpression dbProjectExpression2 = (DbProjectExpression)dbProjectExpression.Input.Expression;
					if (dbProjectExpression2.Projection.ExpressionKind != DbExpressionKind.NewInstance || dbProjectExpression2.Projection.ResultType.EdmType.BuiltInTypeKind != BuiltInTypeKind.RowType)
					{
						return cqtExpression;
					}
					DbNewInstanceExpression dbNewInstanceExpression2 = (DbNewInstanceExpression)dbProjectExpression2.Projection;
					RowType rowType2 = (RowType)dbNewInstanceExpression2.ResultType.EdmType;
					List<DbExpression> list2 = new List<DbExpression>();
					foreach (Tuple<EdmProperty, string> tuple in list)
					{
						int index = rowType2.Properties.IndexOf(tuple.Item1);
						list2.Add(dbNewInstanceExpression2.Arguments[index]);
					}
					DbNewInstanceExpression projection = dbNewInstanceExpression.ResultType.New(list2);
					return dbProjectExpression2.Input.Project(projection);
				}
			}

			// Token: 0x02000736 RID: 1846
			private abstract class OrderByTranslatorBase : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x0600474A RID: 18250 RVA: 0x0010150C File Offset: 0x000FF70C
				protected OrderByTranslatorBase(bool ascending, params SequenceMethod[] methods) : base(methods)
				{
					this._ascending = ascending;
				}

				// Token: 0x0600474B RID: 18251 RVA: 0x0010151C File Offset: 0x000FF71C
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					List<DbSortClause> list = new List<DbSortClause>(1);
					DbSortClause item = this._ascending ? lambda.ToSortClause() : lambda.ToSortClauseDescending();
					list.Add(item);
					return parent.Sort(sourceBinding, list);
				}

				// Token: 0x040020AF RID: 8367
				private readonly bool _ascending;
			}

			// Token: 0x02000737 RID: 1847
			private sealed class OrderByTranslator : ExpressionConverter.MethodCallTranslator.OrderByTranslatorBase
			{
				// Token: 0x0600474C RID: 18252 RVA: 0x00101558 File Offset: 0x000FF758
				internal OrderByTranslator() : base(true, new SequenceMethod[]
				{
					SequenceMethod.OrderBy
				})
				{
				}
			}

			// Token: 0x02000738 RID: 1848
			private sealed class OrderByDescendingTranslator : ExpressionConverter.MethodCallTranslator.OrderByTranslatorBase
			{
				// Token: 0x0600474D RID: 18253 RVA: 0x0010156C File Offset: 0x000FF76C
				internal OrderByDescendingTranslator() : base(false, new SequenceMethod[]
				{
					SequenceMethod.OrderByDescending
				})
				{
				}
			}

			// Token: 0x02000739 RID: 1849
			private abstract class ThenByTranslatorBase : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x0600474E RID: 18254 RVA: 0x00101580 File Offset: 0x000FF780
				protected ThenByTranslatorBase(bool ascending, params SequenceMethod[] methods) : base(methods)
				{
					this._ascending = ascending;
				}

				// Token: 0x0600474F RID: 18255 RVA: 0x00101590 File Offset: 0x000FF790
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateSet(call.Arguments[0]);
					if (DbExpressionKind.Sort != dbExpression.ExpressionKind)
					{
						throw EntityUtil.InvalidOperation(Strings.ELinq_ThenByDoesNotFollowOrderBy);
					}
					DbSortExpression dbSortExpression = (DbSortExpression)dbExpression;
					DbExpressionBinding input = dbSortExpression.Input;
					LambdaExpression lambdaExpression = parent.GetLambdaExpression(call, 1);
					ParameterExpression linqExpression = lambdaExpression.Parameters[0];
					parent._bindingContext.PushBindingScope(new Binding(linqExpression, input.Variable));
					DbExpression key = parent.TranslateExpression(lambdaExpression.Body);
					parent._bindingContext.PopBindingScope();
					return parent.Sort(input, new List<DbSortClause>(dbSortExpression.SortOrder)
					{
						new DbSortClause(key, this._ascending, null)
					});
				}

				// Token: 0x040020B0 RID: 8368
				private readonly bool _ascending;
			}

			// Token: 0x0200073A RID: 1850
			private sealed class ThenByTranslator : ExpressionConverter.MethodCallTranslator.ThenByTranslatorBase
			{
				// Token: 0x06004750 RID: 18256 RVA: 0x00101648 File Offset: 0x000FF848
				internal ThenByTranslator() : base(true, new SequenceMethod[]
				{
					SequenceMethod.ThenBy
				})
				{
				}
			}

			// Token: 0x0200073B RID: 1851
			private sealed class ThenByDescendingTranslator : ExpressionConverter.MethodCallTranslator.ThenByTranslatorBase
			{
				// Token: 0x06004751 RID: 18257 RVA: 0x0010165C File Offset: 0x000FF85C
				internal ThenByDescendingTranslator() : base(false, new SequenceMethod[]
				{
					SequenceMethod.ThenByDescending
				})
				{
				}
			}
		}

		// Token: 0x020004D2 RID: 1234
		private sealed class MemberAccessTranslator : ExpressionConverter.TypedTranslator<MemberExpression>
		{
			// Token: 0x06003D0A RID: 15626 RVA: 0x000E4D65 File Offset: 0x000E2F65
			internal MemberAccessTranslator() : base(new ExpressionType[]
			{
				ExpressionType.MemberAccess
			})
			{
			}

			// Token: 0x06003D0B RID: 15627 RVA: 0x000E4D78 File Offset: 0x000E2F78
			protected override DbExpression TypedTranslate(ExpressionConverter parent, MemberExpression linq)
			{
				string text;
				Type type;
				MemberInfo memberInfo = TypeSystem.PropertyOrField(linq.Member, out text, out type);
				if (linq.Expression != null)
				{
					DbExpression dbExpression = parent.TranslateExpression(linq.Expression);
					DbExpression result;
					if (ExpressionConverter.MemberAccessTranslator.TryResolveAsProperty(parent, memberInfo, dbExpression.ResultType, dbExpression, out result))
					{
						return result;
					}
				}
				ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator;
				if (memberInfo.MemberType == MemberTypes.Property && ExpressionConverter.MemberAccessTranslator.TryGetTranslator((PropertyInfo)memberInfo, out propertyTranslator))
				{
					return propertyTranslator.Translate(parent, linq);
				}
				throw EntityUtil.NotSupported(Strings.ELinq_UnrecognizedMember(linq.Member.Name));
			}

			// Token: 0x06003D0C RID: 15628 RVA: 0x000E4DFC File Offset: 0x000E2FFC
			static MemberAccessTranslator()
			{
				ExpressionConverter.MemberAccessTranslator.s_propertyTranslators = new Dictionary<PropertyInfo, ExpressionConverter.MemberAccessTranslator.PropertyTranslator>();
				foreach (ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator in ExpressionConverter.MemberAccessTranslator.GetPropertyTranslators())
				{
					foreach (PropertyInfo key in propertyTranslator.Properties)
					{
						ExpressionConverter.MemberAccessTranslator.s_propertyTranslators.Add(key, propertyTranslator);
					}
				}
			}

			// Token: 0x06003D0D RID: 15629 RVA: 0x000E4E98 File Offset: 0x000E3098
			private static bool TryGetTranslator(PropertyInfo propertyInfo, out ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator)
			{
				PropertyInfo propertyInfo2 = propertyInfo;
				if (propertyInfo.DeclaringType.IsGenericType)
				{
					try
					{
						propertyInfo = propertyInfo.DeclaringType.GetGenericTypeDefinition().GetProperty(propertyInfo.Name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
					}
					catch (AmbiguousMatchException)
					{
						propertyTranslator = null;
						return false;
					}
					if (propertyInfo == null)
					{
						propertyTranslator = null;
						return false;
					}
				}
				ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator2;
				if (ExpressionConverter.MemberAccessTranslator.s_propertyTranslators.TryGetValue(propertyInfo, out propertyTranslator2))
				{
					propertyTranslator = propertyTranslator2;
					return true;
				}
				if ("Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" == propertyInfo.DeclaringType.Assembly.FullName)
				{
					object obj = ExpressionConverter.MemberAccessTranslator.s_vbInitializerLock;
					lock (obj)
					{
						if (!ExpressionConverter.MemberAccessTranslator.s_vbPropertiesInitialized)
						{
							ExpressionConverter.MemberAccessTranslator.InitializeVBProperties(propertyInfo.DeclaringType.Assembly);
							ExpressionConverter.MemberAccessTranslator.s_vbPropertiesInitialized = true;
						}
						if (ExpressionConverter.MemberAccessTranslator.s_propertyTranslators.TryGetValue(propertyInfo, out propertyTranslator2))
						{
							propertyTranslator = propertyTranslator2;
							return true;
						}
						propertyTranslator = null;
						return false;
					}
				}
				if (ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator.TryGetPropertyTranslator(propertyInfo2, out propertyTranslator))
				{
					return true;
				}
				propertyTranslator = null;
				return false;
			}

			// Token: 0x06003D0E RID: 15630 RVA: 0x000E4F9C File Offset: 0x000E319C
			private static bool TryResolveAsProperty(ExpressionConverter parent, MemberInfo clrMember, TypeUsage definingType, DbExpression instance, out DbExpression propertyExpression)
			{
				RowType rowType = definingType.EdmType as RowType;
				string name = clrMember.Name;
				if (rowType == null)
				{
					StructuralType structuralType = definingType.EdmType as StructuralType;
					if (structuralType != null)
					{
						EdmMember edmMember = null;
						if (parent._perspective.TryGetMember(structuralType, name, false, out edmMember) && edmMember != null)
						{
							if (edmMember.BuiltInTypeKind == BuiltInTypeKind.NavigationProperty)
							{
								NavigationProperty navProp = (NavigationProperty)edmMember;
								propertyExpression = ExpressionConverter.MemberAccessTranslator.TranslateNavigationProperty(parent, clrMember, instance, navProp);
								return true;
							}
							propertyExpression = instance.Property(name);
							return true;
						}
					}
					if (name == "Key" && DbExpressionKind.Property == instance.ExpressionKind)
					{
						DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)instance;
						InitializerMetadata initializerMetadata;
						if (dbPropertyExpression.Property.Name == "Group" && InitializerMetadata.TryGetInitializerMetadata(dbPropertyExpression.Instance.ResultType, out initializerMetadata) && initializerMetadata.Kind == InitializerMetadataKind.Grouping)
						{
							propertyExpression = dbPropertyExpression.Instance.Property("Key");
							return true;
						}
					}
					propertyExpression = null;
					return false;
				}
				EdmMember edmMember2;
				if (rowType.Members.TryGetValue(name, false, out edmMember2))
				{
					propertyExpression = instance.Property(name);
					return true;
				}
				propertyExpression = null;
				return false;
			}

			// Token: 0x06003D0F RID: 15631 RVA: 0x000E50AC File Offset: 0x000E32AC
			private static DbExpression TranslateNavigationProperty(ExpressionConverter parent, MemberInfo clrMember, DbExpression instance, NavigationProperty navProp)
			{
				DbExpression dbExpression = instance.Property(navProp);
				if (BuiltInTypeKind.CollectionType == dbExpression.ResultType.EdmType.BuiltInTypeKind)
				{
					Type propertyType = ((PropertyInfo)clrMember).PropertyType;
					if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(EntityCollection<>))
					{
						dbExpression = parent.CreateNewRowExpression(new List<KeyValuePair<string, DbExpression>>(2)
						{
							new KeyValuePair<string, DbExpression>("Owner", instance),
							new KeyValuePair<string, DbExpression>("Elements", dbExpression)
						}, InitializerMetadata.CreateEntityCollectionInitializer(parent.EdmItemCollection, ((PropertyInfo)clrMember).PropertyType, navProp));
					}
				}
				return dbExpression;
			}

			// Token: 0x06003D10 RID: 15632 RVA: 0x000E5148 File Offset: 0x000E3348
			private static DbExpression TranslateCount(ExpressionConverter parent, Type sequenceElementType, Expression sequence)
			{
				MethodInfo methodInfo;
				ReflectionUtil.TryLookupMethod(SequenceMethod.Count, out methodInfo);
				methodInfo = methodInfo.MakeGenericMethod(new Type[]
				{
					sequenceElementType
				});
				Expression linq = Expression.Call(methodInfo, sequence);
				return parent.TranslateExpression(linq);
			}

			// Token: 0x06003D11 RID: 15633 RVA: 0x000E5180 File Offset: 0x000E3380
			private static void InitializeVBProperties(Assembly vbAssembly)
			{
				foreach (ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator in ExpressionConverter.MemberAccessTranslator.GetVisualBasicPropertyTranslators(vbAssembly))
				{
					foreach (PropertyInfo key in propertyTranslator.Properties)
					{
						ExpressionConverter.MemberAccessTranslator.s_propertyTranslators.Add(key, propertyTranslator);
					}
				}
			}

			// Token: 0x06003D12 RID: 15634 RVA: 0x000E5208 File Offset: 0x000E3408
			private static IEnumerable<ExpressionConverter.MemberAccessTranslator.PropertyTranslator> GetVisualBasicPropertyTranslators(Assembly vbAssembly)
			{
				yield return new ExpressionConverter.MemberAccessTranslator.VBDateAndTimeNowTranslator(vbAssembly);
				yield break;
			}

			// Token: 0x06003D13 RID: 15635 RVA: 0x000E5218 File Offset: 0x000E3418
			private static IEnumerable<ExpressionConverter.MemberAccessTranslator.PropertyTranslator> GetPropertyTranslators()
			{
				yield return new ExpressionConverter.MemberAccessTranslator.DefaultCanonicalFunctionPropertyTranslator();
				yield return new ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator();
				yield return new ExpressionConverter.MemberAccessTranslator.EntityCollectionCountTranslator();
				yield return new ExpressionConverter.MemberAccessTranslator.NullableHasValueTranslator();
				yield return new ExpressionConverter.MemberAccessTranslator.NullableValueTranslator();
				yield return new ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator();
				yield break;
			}

			// Token: 0x06003D14 RID: 15636 RVA: 0x000E5224 File Offset: 0x000E3424
			internal static bool CanFuncletizePropertyInfo(PropertyInfo propertyInfo)
			{
				ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator;
				return ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator.TryGetPropertyTranslator(propertyInfo, out propertyTranslator) || !ExpressionConverter.MemberAccessTranslator.TryGetTranslator(propertyInfo, out propertyTranslator);
			}

			// Token: 0x04001AD6 RID: 6870
			private static readonly Dictionary<PropertyInfo, ExpressionConverter.MemberAccessTranslator.PropertyTranslator> s_propertyTranslators;

			// Token: 0x04001AD7 RID: 6871
			private static bool s_vbPropertiesInitialized;

			// Token: 0x04001AD8 RID: 6872
			private static readonly object s_vbInitializerLock = new object();

			// Token: 0x0200073F RID: 1855
			private sealed class SpatialPropertyTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x0600476A RID: 18282 RVA: 0x00101E7F File Offset: 0x0010007F
				internal SpatialPropertyTranslator() : base(ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetSupportedProperties())
				{
				}

				// Token: 0x0600476B RID: 18283 RVA: 0x00101E98 File Offset: 0x00100098
				private static PropertyInfo GetProperty<T, TResult>(Expression<Func<T, TResult>> lambda)
				{
					MemberExpression memberExpression = (MemberExpression)lambda.Body;
					return (PropertyInfo)memberExpression.Member;
				}

				// Token: 0x0600476C RID: 18284 RVA: 0x00101EBE File Offset: 0x001000BE
				private static IEnumerable<PropertyInfo> GetSupportedProperties()
				{
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int>((DbGeography geo) => geo.CoordinateSystemId);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, string>((DbGeography geo) => geo.SpatialTypeName);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int>((DbGeography geo) => geo.Dimension);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, bool>((DbGeography geo) => geo.IsEmpty);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int?>((DbGeography geo) => geo.ElementCount);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Latitude);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Longitude);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Elevation);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Measure);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Length);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, DbGeography>((DbGeography geo) => geo.StartPoint);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, DbGeography>((DbGeography geo) => geo.EndPoint);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, bool?>((DbGeography geo) => geo.IsClosed);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int?>((DbGeography geo) => geo.PointCount);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Area);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int>((DbGeometry geo) => geo.CoordinateSystemId);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, string>((DbGeometry geo) => geo.SpatialTypeName);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int>((DbGeometry geo) => geo.Dimension);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Envelope);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool>((DbGeometry geo) => geo.IsEmpty);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool>((DbGeometry geo) => geo.IsSimple);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Boundary);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool>((DbGeometry geo) => geo.IsValid);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.ConvexHull);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int?>((DbGeometry geo) => geo.ElementCount);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.XCoordinate);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.YCoordinate);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Elevation);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Measure);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Length);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.StartPoint);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.EndPoint);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool?>((DbGeometry geo) => geo.IsClosed);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool?>((DbGeometry geo) => geo.IsRing);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int?>((DbGeometry geo) => geo.PointCount);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Area);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Centroid);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.PointOnSurface);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.ExteriorRing);
					yield return ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int?>((DbGeometry geo) => geo.InteriorRingCount);
					yield break;
				}

				// Token: 0x0600476D RID: 18285 RVA: 0x00101EC8 File Offset: 0x001000C8
				private static Dictionary<PropertyInfo, string> GetRenamedPropertyFunctions()
				{
					return new Dictionary<PropertyInfo, string>
					{
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int>((DbGeography geo) => geo.CoordinateSystemId),
							"CoordinateSystemId"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, string>((DbGeography geo) => geo.SpatialTypeName),
							"SpatialTypeName"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int>((DbGeography geo) => geo.Dimension),
							"SpatialDimension"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, bool>((DbGeography geo) => geo.IsEmpty),
							"IsEmptySpatial"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int?>((DbGeography geo) => geo.ElementCount),
							"SpatialElementCount"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Latitude),
							"Latitude"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Longitude),
							"Longitude"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Elevation),
							"Elevation"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Measure),
							"Measure"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Length),
							"SpatialLength"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, DbGeography>((DbGeography geo) => geo.StartPoint),
							"StartPoint"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, DbGeography>((DbGeography geo) => geo.EndPoint),
							"EndPoint"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, bool?>((DbGeography geo) => geo.IsClosed),
							"IsClosedSpatial"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, int?>((DbGeography geo) => geo.PointCount),
							"PointCount"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeography, double?>((DbGeography geo) => geo.Area),
							"Area"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int>((DbGeometry geo) => geo.CoordinateSystemId),
							"CoordinateSystemId"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, string>((DbGeometry geo) => geo.SpatialTypeName),
							"SpatialTypeName"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int>((DbGeometry geo) => geo.Dimension),
							"SpatialDimension"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Envelope),
							"SpatialEnvelope"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool>((DbGeometry geo) => geo.IsEmpty),
							"IsEmptySpatial"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool>((DbGeometry geo) => geo.IsSimple),
							"IsSimpleGeometry"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Boundary),
							"SpatialBoundary"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool>((DbGeometry geo) => geo.IsValid),
							"IsValidGeometry"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.ConvexHull),
							"SpatialConvexHull"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int?>((DbGeometry geo) => geo.ElementCount),
							"SpatialElementCount"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.XCoordinate),
							"XCoordinate"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.YCoordinate),
							"YCoordinate"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Elevation),
							"Elevation"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Measure),
							"Measure"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Length),
							"SpatialLength"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.StartPoint),
							"StartPoint"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.EndPoint),
							"EndPoint"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool?>((DbGeometry geo) => geo.IsClosed),
							"IsClosedSpatial"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, bool?>((DbGeometry geo) => geo.IsRing),
							"IsRing"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int?>((DbGeometry geo) => geo.PointCount),
							"PointCount"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, double?>((DbGeometry geo) => geo.Area),
							"Area"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.Centroid),
							"Centroid"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.PointOnSurface),
							"PointOnSurface"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, DbGeometry>((DbGeometry geo) => geo.ExteriorRing),
							"ExteriorRing"
						},
						{
							ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetProperty<DbGeometry, int?>((DbGeometry geo) => geo.InteriorRingCount),
							"InteriorRingCount"
						}
					};
				}

				// Token: 0x0600476E RID: 18286 RVA: 0x00102A44 File Offset: 0x00100C44
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					PropertyInfo propertyInfo = (PropertyInfo)call.Member;
					string functionName;
					if (!this.propertyFunctionRenames.TryGetValue(propertyInfo, out functionName))
					{
						functionName = "ST" + propertyInfo.Name;
					}
					return parent.TranslateIntoCanonicalFunction(functionName, call, new Expression[]
					{
						call.Expression
					});
				}

				// Token: 0x040020BC RID: 8380
				private readonly Dictionary<PropertyInfo, string> propertyFunctionRenames = ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetRenamedPropertyFunctions();
			}

			// Token: 0x02000740 RID: 1856
			private sealed class GenericICollectionTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x0600476F RID: 18287 RVA: 0x00102A97 File Offset: 0x00100C97
				private GenericICollectionTranslator(Type elementType) : base(Enumerable.Empty<PropertyInfo>())
				{
					this._elementType = elementType;
				}

				// Token: 0x06004770 RID: 18288 RVA: 0x00102AAB File Offset: 0x00100CAB
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return ExpressionConverter.MemberAccessTranslator.TranslateCount(parent, this._elementType, call.Expression);
				}

				// Token: 0x06004771 RID: 18289 RVA: 0x00102AC0 File Offset: 0x00100CC0
				internal static bool TryGetPropertyTranslator(PropertyInfo propertyInfo, out ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator)
				{
					if (propertyInfo.Name == "Count" && propertyInfo.PropertyType.Equals(typeof(int)))
					{
						foreach (KeyValuePair<Type, Type> keyValuePair in ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator.GetImplementedICollections(propertyInfo.DeclaringType))
						{
							Type key = keyValuePair.Key;
							Type value = keyValuePair.Value;
							if (propertyInfo.IsImplementationOf(key))
							{
								propertyTranslator = new ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator(value);
								return true;
							}
						}
					}
					propertyTranslator = null;
					return false;
				}

				// Token: 0x06004772 RID: 18290 RVA: 0x00102B64 File Offset: 0x00100D64
				private static bool IsICollection(Type candidateType, out Type elementType)
				{
					if (candidateType.IsGenericType && candidateType.GetGenericTypeDefinition().Equals(typeof(ICollection<>)))
					{
						elementType = candidateType.GetGenericArguments()[0];
						return true;
					}
					elementType = null;
					return false;
				}

				// Token: 0x06004773 RID: 18291 RVA: 0x00102B95 File Offset: 0x00100D95
				private static IEnumerable<KeyValuePair<Type, Type>> GetImplementedICollections(Type type)
				{
					Type value;
					if (ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator.IsICollection(type, out value))
					{
						yield return new KeyValuePair<Type, Type>(type, value);
					}
					else
					{
						foreach (Type type2 in type.GetInterfaces())
						{
							if (ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator.IsICollection(type2, out value))
							{
								yield return new KeyValuePair<Type, Type>(type2, value);
							}
						}
						Type[] array = null;
					}
					yield break;
				}

				// Token: 0x040020BD RID: 8381
				private readonly Type _elementType;
			}

			// Token: 0x02000741 RID: 1857
			private abstract class PropertyTranslator
			{
				// Token: 0x06004774 RID: 18292 RVA: 0x00102BA5 File Offset: 0x00100DA5
				protected PropertyTranslator(params PropertyInfo[] properties)
				{
					this._properties = properties;
				}

				// Token: 0x06004775 RID: 18293 RVA: 0x00102BA5 File Offset: 0x00100DA5
				protected PropertyTranslator(IEnumerable<PropertyInfo> properties)
				{
					this._properties = properties;
				}

				// Token: 0x17000BDB RID: 3035
				// (get) Token: 0x06004776 RID: 18294 RVA: 0x00102BB4 File Offset: 0x00100DB4
				internal IEnumerable<PropertyInfo> Properties
				{
					get
					{
						return this._properties;
					}
				}

				// Token: 0x06004777 RID: 18295
				internal abstract DbExpression Translate(ExpressionConverter parent, MemberExpression call);

				// Token: 0x06004778 RID: 18296 RVA: 0x000E5329 File Offset: 0x000E3529
				public override string ToString()
				{
					return base.GetType().Name;
				}

				// Token: 0x040020BE RID: 8382
				private readonly IEnumerable<PropertyInfo> _properties;
			}

			// Token: 0x02000742 RID: 1858
			private sealed class DefaultCanonicalFunctionPropertyTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06004779 RID: 18297 RVA: 0x00102BBC File Offset: 0x00100DBC
				internal DefaultCanonicalFunctionPropertyTranslator() : base(ExpressionConverter.MemberAccessTranslator.DefaultCanonicalFunctionPropertyTranslator.GetProperties())
				{
				}

				// Token: 0x0600477A RID: 18298 RVA: 0x00102BC9 File Offset: 0x00100DC9
				private static IEnumerable<PropertyInfo> GetProperties()
				{
					yield return typeof(string).GetProperty("Length", BindingFlags.Instance | BindingFlags.Public);
					yield return typeof(DateTime).GetProperty("Year", BindingFlags.Instance | BindingFlags.Public);
					yield return typeof(DateTime).GetProperty("Month", BindingFlags.Instance | BindingFlags.Public);
					yield return typeof(DateTime).GetProperty("Day", BindingFlags.Instance | BindingFlags.Public);
					yield return typeof(DateTime).GetProperty("Hour", BindingFlags.Instance | BindingFlags.Public);
					yield return typeof(DateTime).GetProperty("Minute", BindingFlags.Instance | BindingFlags.Public);
					yield return typeof(DateTime).GetProperty("Second", BindingFlags.Instance | BindingFlags.Public);
					yield return typeof(DateTime).GetProperty("Millisecond", BindingFlags.Instance | BindingFlags.Public);
					yield return typeof(DateTimeOffset).GetProperty("Year", BindingFlags.Instance | BindingFlags.Public);
					yield return typeof(DateTimeOffset).GetProperty("Month", BindingFlags.Instance | BindingFlags.Public);
					yield return typeof(DateTimeOffset).GetProperty("Day", BindingFlags.Instance | BindingFlags.Public);
					yield return typeof(DateTimeOffset).GetProperty("Hour", BindingFlags.Instance | BindingFlags.Public);
					yield return typeof(DateTimeOffset).GetProperty("Minute", BindingFlags.Instance | BindingFlags.Public);
					yield return typeof(DateTimeOffset).GetProperty("Second", BindingFlags.Instance | BindingFlags.Public);
					yield return typeof(DateTimeOffset).GetProperty("Millisecond", BindingFlags.Instance | BindingFlags.Public);
					yield break;
				}

				// Token: 0x0600477B RID: 18299 RVA: 0x00102BD2 File Offset: 0x00100DD2
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return parent.TranslateIntoCanonicalFunction(call.Member.Name, call, new Expression[]
					{
						call.Expression
					});
				}
			}

			// Token: 0x02000743 RID: 1859
			private sealed class RenameCanonicalFunctionPropertyTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x0600477C RID: 18300 RVA: 0x00102BF5 File Offset: 0x00100DF5
				internal RenameCanonicalFunctionPropertyTranslator() : base(ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperties())
				{
				}

				// Token: 0x0600477D RID: 18301 RVA: 0x00102C02 File Offset: 0x00100E02
				private static IEnumerable<PropertyInfo> GetProperties()
				{
					yield return ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(DateTime), "Now", BindingFlags.Static | BindingFlags.Public, "CurrentDateTime");
					yield return ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(DateTime), "UtcNow", BindingFlags.Static | BindingFlags.Public, "CurrentUtcDateTime");
					yield return ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(DateTimeOffset), "Now", BindingFlags.Static | BindingFlags.Public, "CurrentDateTimeOffset");
					yield return ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(TimeSpan), "Hours", BindingFlags.Instance | BindingFlags.Public, "Hour");
					yield return ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(TimeSpan), "Minutes", BindingFlags.Instance | BindingFlags.Public, "Minute");
					yield return ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(TimeSpan), "Seconds", BindingFlags.Instance | BindingFlags.Public, "Second");
					yield return ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(TimeSpan), "Milliseconds", BindingFlags.Instance | BindingFlags.Public, "Millisecond");
					yield break;
				}

				// Token: 0x0600477E RID: 18302 RVA: 0x00102C0C File Offset: 0x00100E0C
				private static PropertyInfo GetProperty(Type declaringType, string propertyName, BindingFlags bindingFlages, string canonicalFunctionName)
				{
					PropertyInfo property = declaringType.GetProperty(propertyName, bindingFlages);
					ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.s_propertyRenameMap.Add(property, canonicalFunctionName);
					return property;
				}

				// Token: 0x0600477F RID: 18303 RVA: 0x00102C30 File Offset: 0x00100E30
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					PropertyInfo key = (PropertyInfo)call.Member;
					string functionName = ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.s_propertyRenameMap[key];
					DbExpression result;
					if (call.Expression == null)
					{
						result = parent.TranslateIntoCanonicalFunction(functionName, call, new Expression[0]);
					}
					else
					{
						result = parent.TranslateIntoCanonicalFunction(functionName, call, new Expression[]
						{
							call.Expression
						});
					}
					return result;
				}

				// Token: 0x040020BF RID: 8383
				private static readonly Dictionary<PropertyInfo, string> s_propertyRenameMap = new Dictionary<PropertyInfo, string>(2);
			}

			// Token: 0x02000744 RID: 1860
			private sealed class VBDateAndTimeNowTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06004781 RID: 18305 RVA: 0x00102C94 File Offset: 0x00100E94
				internal VBDateAndTimeNowTranslator(Assembly vbAssembly) : base(new PropertyInfo[]
				{
					ExpressionConverter.MemberAccessTranslator.VBDateAndTimeNowTranslator.GetProperty(vbAssembly)
				})
				{
				}

				// Token: 0x06004782 RID: 18306 RVA: 0x00102CAB File Offset: 0x00100EAB
				private static PropertyInfo GetProperty(Assembly vbAssembly)
				{
					return vbAssembly.GetType("Microsoft.VisualBasic.DateAndTime").GetProperty("Now", BindingFlags.Static | BindingFlags.Public);
				}

				// Token: 0x06004783 RID: 18307 RVA: 0x00102CC4 File Offset: 0x00100EC4
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return parent.TranslateIntoCanonicalFunction("CurrentDateTime", call, new Expression[0]);
				}

				// Token: 0x040020C0 RID: 8384
				private const string s_dateAndTimeTypeFullName = "Microsoft.VisualBasic.DateAndTime";
			}

			// Token: 0x02000745 RID: 1861
			private sealed class EntityCollectionCountTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06004784 RID: 18308 RVA: 0x00102CD8 File Offset: 0x00100ED8
				internal EntityCollectionCountTranslator() : base(new PropertyInfo[]
				{
					ExpressionConverter.MemberAccessTranslator.EntityCollectionCountTranslator.GetProperty()
				})
				{
				}

				// Token: 0x06004785 RID: 18309 RVA: 0x00102CEE File Offset: 0x00100EEE
				private static PropertyInfo GetProperty()
				{
					return typeof(EntityCollection<>).GetProperty("Count", BindingFlags.Instance | BindingFlags.Public);
				}

				// Token: 0x06004786 RID: 18310 RVA: 0x00102D06 File Offset: 0x00100F06
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return ExpressionConverter.MemberAccessTranslator.TranslateCount(parent, call.Member.DeclaringType.GetGenericArguments()[0], call.Expression);
				}
			}

			// Token: 0x02000746 RID: 1862
			private sealed class NullableHasValueTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06004787 RID: 18311 RVA: 0x00102D26 File Offset: 0x00100F26
				internal NullableHasValueTranslator() : base(new PropertyInfo[]
				{
					ExpressionConverter.MemberAccessTranslator.NullableHasValueTranslator.GetProperty()
				})
				{
				}

				// Token: 0x06004788 RID: 18312 RVA: 0x00102D3C File Offset: 0x00100F3C
				private static PropertyInfo GetProperty()
				{
					return typeof(Nullable<>).GetProperty("HasValue", BindingFlags.Instance | BindingFlags.Public);
				}

				// Token: 0x06004789 RID: 18313 RVA: 0x00102D54 File Offset: 0x00100F54
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					DbExpression operand = parent.TranslateExpression(call.Expression);
					return parent.CreateIsNullExpression(operand, call.Expression.Type).Not();
				}
			}

			// Token: 0x02000747 RID: 1863
			private sealed class NullableValueTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x0600478A RID: 18314 RVA: 0x00102D85 File Offset: 0x00100F85
				internal NullableValueTranslator() : base(new PropertyInfo[]
				{
					ExpressionConverter.MemberAccessTranslator.NullableValueTranslator.GetProperty()
				})
				{
				}

				// Token: 0x0600478B RID: 18315 RVA: 0x00102D9B File Offset: 0x00100F9B
				private static PropertyInfo GetProperty()
				{
					return typeof(Nullable<>).GetProperty("Value", BindingFlags.Instance | BindingFlags.Public);
				}

				// Token: 0x0600478C RID: 18316 RVA: 0x00102DB4 File Offset: 0x00100FB4
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return parent.TranslateExpression(call.Expression);
				}
			}
		}

		// Token: 0x020004D3 RID: 1235
		private sealed class OrderByLifter
		{
			// Token: 0x06003D15 RID: 15637 RVA: 0x000E5248 File Offset: 0x000E3448
			internal OrderByLifter(AliasGenerator aliasGenerator)
			{
				this._aliasGenerator = aliasGenerator;
			}

			// Token: 0x06003D16 RID: 15638 RVA: 0x000E5258 File Offset: 0x000E3458
			internal DbExpression Project(DbExpressionBinding input, DbExpression projection)
			{
				ExpressionConverter.OrderByLifter.OrderByLifterBase lifter = this.GetLifter(input.Expression);
				return lifter.Project(input.Project(projection));
			}

			// Token: 0x06003D17 RID: 15639 RVA: 0x000E5280 File Offset: 0x000E3480
			internal DbExpression Filter(DbExpressionBinding input, DbExpression predicate)
			{
				ExpressionConverter.OrderByLifter.OrderByLifterBase lifter = this.GetLifter(input.Expression);
				return lifter.Filter(input.Filter(predicate));
			}

			// Token: 0x06003D18 RID: 15640 RVA: 0x000E52A8 File Offset: 0x000E34A8
			internal DbExpression OfType(DbExpression argument, TypeUsage type)
			{
				ExpressionConverter.OrderByLifter.OrderByLifterBase lifter = this.GetLifter(argument);
				return lifter.OfType(type);
			}

			// Token: 0x06003D19 RID: 15641 RVA: 0x000E52C4 File Offset: 0x000E34C4
			internal DbExpression Skip(DbExpressionBinding input, DbExpression skipCount)
			{
				ExpressionConverter.OrderByLifter.OrderByLifterBase lifter = this.GetLifter(input.Expression);
				return lifter.Skip(skipCount);
			}

			// Token: 0x06003D1A RID: 15642 RVA: 0x000E52E8 File Offset: 0x000E34E8
			internal DbExpression Limit(DbExpression argument, DbExpression limit)
			{
				ExpressionConverter.OrderByLifter.OrderByLifterBase lifter = this.GetLifter(argument);
				return lifter.Limit(limit);
			}

			// Token: 0x06003D1B RID: 15643 RVA: 0x000E5304 File Offset: 0x000E3504
			private ExpressionConverter.OrderByLifter.OrderByLifterBase GetLifter(DbExpression root)
			{
				return ExpressionConverter.OrderByLifter.OrderByLifterBase.GetLifter(root, this._aliasGenerator);
			}

			// Token: 0x04001AD9 RID: 6873
			private readonly AliasGenerator _aliasGenerator;

			// Token: 0x0200074A RID: 1866
			private abstract class OrderByLifterBase
			{
				// Token: 0x0600479D RID: 18333 RVA: 0x00102FCF File Offset: 0x001011CF
				protected OrderByLifterBase(DbExpression root, AliasGenerator aliasGenerator)
				{
					this._root = root;
					this._aliasGenerator = aliasGenerator;
				}

				// Token: 0x0600479E RID: 18334 RVA: 0x00102FE8 File Offset: 0x001011E8
				internal static ExpressionConverter.OrderByLifter.OrderByLifterBase GetLifter(DbExpression source, AliasGenerator aliasGenerator)
				{
					if (source.ExpressionKind == DbExpressionKind.Sort)
					{
						return new ExpressionConverter.OrderByLifter.SortLifter((DbSortExpression)source, aliasGenerator);
					}
					if (source.ExpressionKind == DbExpressionKind.Project)
					{
						DbProjectExpression dbProjectExpression = (DbProjectExpression)source;
						DbExpression expression = dbProjectExpression.Input.Expression;
						if (expression.ExpressionKind == DbExpressionKind.Sort)
						{
							return new ExpressionConverter.OrderByLifter.ProjectSortLifter(dbProjectExpression, (DbSortExpression)expression, aliasGenerator);
						}
						if (expression.ExpressionKind == DbExpressionKind.Skip)
						{
							return new ExpressionConverter.OrderByLifter.ProjectSkipLifter(dbProjectExpression, (DbSkipExpression)expression, aliasGenerator);
						}
						if (expression.ExpressionKind == DbExpressionKind.Limit)
						{
							DbLimitExpression dbLimitExpression = (DbLimitExpression)expression;
							DbExpression argument = dbLimitExpression.Argument;
							if (argument.ExpressionKind == DbExpressionKind.Sort)
							{
								return new ExpressionConverter.OrderByLifter.ProjectLimitSortLifter(dbProjectExpression, dbLimitExpression, (DbSortExpression)argument, aliasGenerator);
							}
							if (argument.ExpressionKind == DbExpressionKind.Skip)
							{
								return new ExpressionConverter.OrderByLifter.ProjectLimitSkipLifter(dbProjectExpression, dbLimitExpression, (DbSkipExpression)argument, aliasGenerator);
							}
						}
					}
					if (source.ExpressionKind == DbExpressionKind.Skip)
					{
						return new ExpressionConverter.OrderByLifter.SkipLifter((DbSkipExpression)source, aliasGenerator);
					}
					if (source.ExpressionKind == DbExpressionKind.Limit)
					{
						DbLimitExpression dbLimitExpression2 = (DbLimitExpression)source;
						DbExpression argument2 = dbLimitExpression2.Argument;
						if (argument2.ExpressionKind == DbExpressionKind.Sort)
						{
							return new ExpressionConverter.OrderByLifter.LimitSortLifter(dbLimitExpression2, (DbSortExpression)argument2, aliasGenerator);
						}
						if (argument2.ExpressionKind == DbExpressionKind.Skip)
						{
							return new ExpressionConverter.OrderByLifter.LimitSkipLifter(dbLimitExpression2, (DbSkipExpression)argument2, aliasGenerator);
						}
						if (argument2.ExpressionKind == DbExpressionKind.Project)
						{
							DbProjectExpression dbProjectExpression2 = (DbProjectExpression)argument2;
							DbExpression expression2 = dbProjectExpression2.Input.Expression;
							if (expression2.ExpressionKind == DbExpressionKind.Sort)
							{
								return new ExpressionConverter.OrderByLifter.ProjectLimitSortLifter(dbProjectExpression2, dbLimitExpression2, (DbSortExpression)expression2, aliasGenerator);
							}
							if (expression2.ExpressionKind == DbExpressionKind.Skip)
							{
								return new ExpressionConverter.OrderByLifter.ProjectLimitSkipLifter(dbProjectExpression2, dbLimitExpression2, (DbSkipExpression)expression2, aliasGenerator);
							}
						}
					}
					return new ExpressionConverter.OrderByLifter.PassthroughOrderByLifter(source, aliasGenerator);
				}

				// Token: 0x0600479F RID: 18335
				internal abstract DbExpression Project(DbProjectExpression project);

				// Token: 0x060047A0 RID: 18336
				internal abstract DbExpression Filter(DbFilterExpression filter);

				// Token: 0x060047A1 RID: 18337 RVA: 0x00103174 File Offset: 0x00101374
				internal virtual DbExpression OfType(TypeUsage type)
				{
					DbExpressionBinding dbExpressionBinding = this._root.BindAs(this._aliasGenerator.Next());
					DbExpression dbExpression = this.Filter(dbExpressionBinding.Filter(dbExpressionBinding.Variable.IsOf(type)));
					ExpressionConverter.OrderByLifter.OrderByLifterBase lifter = ExpressionConverter.OrderByLifter.OrderByLifterBase.GetLifter(dbExpression, this._aliasGenerator);
					DbExpressionBinding dbExpressionBinding2 = dbExpression.BindAs(this._aliasGenerator.Next());
					return lifter.Project(dbExpressionBinding2.Project(dbExpressionBinding2.Variable.TreatAs(type)));
				}

				// Token: 0x060047A2 RID: 18338
				internal abstract DbExpression Limit(DbExpression k);

				// Token: 0x060047A3 RID: 18339
				internal abstract DbExpression Skip(DbExpression k);

				// Token: 0x060047A4 RID: 18340 RVA: 0x001031EC File Offset: 0x001013EC
				protected DbProjectExpression ComposeProject(DbExpression input, DbProjectExpression first, DbProjectExpression second)
				{
					DbLambda lambda = DbExpressionBuilder.Lambda(second.Projection, new DbVariableReferenceExpression[]
					{
						second.Input.Variable
					});
					DbProjectExpression project = first.Input.Project(lambda.Invoke(new DbExpression[]
					{
						first.Projection
					}));
					return this.RebindProject(input, project);
				}

				// Token: 0x060047A5 RID: 18341 RVA: 0x00103244 File Offset: 0x00101444
				protected DbFilterExpression ComposeFilter(DbExpression input, DbProjectExpression first, DbFilterExpression second)
				{
					DbLambda lambda = DbExpressionBuilder.Lambda(second.Predicate, new DbVariableReferenceExpression[]
					{
						second.Input.Variable
					});
					DbFilterExpression filter = first.Input.Filter(lambda.Invoke(new DbExpression[]
					{
						first.Projection
					}));
					return this.RebindFilter(input, filter);
				}

				// Token: 0x060047A6 RID: 18342 RVA: 0x0010329C File Offset: 0x0010149C
				protected DbSkipExpression AddToSkip(DbExpression input, DbSkipExpression skip, DbExpression plusK)
				{
					DbExpression k = this.CombineIntegers(skip.Count, plusK, (int l, int r) => l + r);
					return this.RebindSkip(input, skip, k);
				}

				// Token: 0x060047A7 RID: 18343 RVA: 0x001032E0 File Offset: 0x001014E0
				protected DbLimitExpression SubtractFromLimit(DbExpression input, DbLimitExpression limit, DbExpression minusK)
				{
					DbExpression count = this.CombineIntegers(limit.Limit, minusK, delegate(int l, int r)
					{
						if (r <= l)
						{
							return l - r;
						}
						return 0;
					});
					return input.Limit(count);
				}

				// Token: 0x060047A8 RID: 18344 RVA: 0x00103324 File Offset: 0x00101524
				protected DbLimitExpression MinimumLimit(DbExpression input, DbLimitExpression limit, DbExpression k)
				{
					DbExpression count = this.CombineIntegers(limit.Limit, k, new Func<int, int, int>(Math.Min));
					return input.Limit(count);
				}

				// Token: 0x060047A9 RID: 18345 RVA: 0x00103354 File Offset: 0x00101554
				protected DbExpression CombineIntegers(DbExpression left, DbExpression right, Func<int, int, int> combineConstants)
				{
					if (left.ExpressionKind == DbExpressionKind.Constant && right.ExpressionKind == DbExpressionKind.Constant)
					{
						object value = ((DbConstantExpression)left).Value;
						object value2 = ((DbConstantExpression)right).Value;
						if (value is int && value2 is int)
						{
							return left.ResultType.Constant(combineConstants((int)value, (int)value2));
						}
					}
					throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UnexpectedLinqLambdaExpressionFormat);
				}

				// Token: 0x060047AA RID: 18346 RVA: 0x001033C8 File Offset: 0x001015C8
				protected DbProjectExpression RebindProject(DbExpression input, DbProjectExpression project)
				{
					DbExpressionBinding input2 = input.BindAs(project.Input.VariableName);
					return input2.Project(project.Projection);
				}

				// Token: 0x060047AB RID: 18347 RVA: 0x001033F4 File Offset: 0x001015F4
				protected DbFilterExpression RebindFilter(DbExpression input, DbFilterExpression filter)
				{
					DbExpressionBinding input2 = input.BindAs(filter.Input.VariableName);
					return input2.Filter(filter.Predicate);
				}

				// Token: 0x060047AC RID: 18348 RVA: 0x00103420 File Offset: 0x00101620
				protected DbSortExpression RebindSort(DbExpression input, DbSortExpression sort)
				{
					DbExpressionBinding input2 = input.BindAs(sort.Input.VariableName);
					return input2.Sort(sort.SortOrder);
				}

				// Token: 0x060047AD RID: 18349 RVA: 0x0010344C File Offset: 0x0010164C
				protected DbSortExpression ApplySkipOrderToSort(DbExpression input, DbSkipExpression sortSpec)
				{
					DbExpressionBinding input2 = input.BindAs(sortSpec.Input.VariableName);
					return input2.Sort(sortSpec.SortOrder);
				}

				// Token: 0x060047AE RID: 18350 RVA: 0x00103478 File Offset: 0x00101678
				protected DbSkipExpression ApplySortOrderToSkip(DbExpression input, DbSortExpression sort, DbExpression k)
				{
					DbExpressionBinding input2 = input.BindAs(sort.Input.VariableName);
					return input2.Skip(sort.SortOrder, k);
				}

				// Token: 0x060047AF RID: 18351 RVA: 0x001034A4 File Offset: 0x001016A4
				protected DbSkipExpression RebindSkip(DbExpression input, DbSkipExpression skip, DbExpression k)
				{
					DbExpressionBinding input2 = input.BindAs(skip.Input.VariableName);
					return input2.Skip(skip.SortOrder, k);
				}

				// Token: 0x040020C9 RID: 8393
				protected readonly DbExpression _root;

				// Token: 0x040020CA RID: 8394
				protected readonly AliasGenerator _aliasGenerator;
			}

			// Token: 0x0200074B RID: 1867
			private class LimitSkipLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x060047B0 RID: 18352 RVA: 0x001034D0 File Offset: 0x001016D0
				internal LimitSkipLifter(DbLimitExpression limit, DbSkipExpression skip, AliasGenerator aliasGenerator) : base(limit, aliasGenerator)
				{
					this._limit = limit;
					this._skip = skip;
					this._source = skip.Input.Expression;
				}

				// Token: 0x060047B1 RID: 18353 RVA: 0x001034F9 File Offset: 0x001016F9
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return base.ApplySkipOrderToSort(filter, this._skip);
				}

				// Token: 0x060047B2 RID: 18354 RVA: 0x00002391 File Offset: 0x00000591
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x060047B3 RID: 18355 RVA: 0x00103508 File Offset: 0x00101708
				internal override DbExpression Limit(DbExpression k)
				{
					if (this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return base.MinimumLimit(this._skip, this._limit, k);
					}
					return base.ApplySkipOrderToSort(this._limit, this._skip).Limit(k);
				}

				// Token: 0x060047B4 RID: 18356 RVA: 0x0010355D File Offset: 0x0010175D
				internal override DbExpression Skip(DbExpression k)
				{
					return base.RebindSkip(this._limit, this._skip, k);
				}

				// Token: 0x040020CB RID: 8395
				private readonly DbLimitExpression _limit;

				// Token: 0x040020CC RID: 8396
				private readonly DbSkipExpression _skip;

				// Token: 0x040020CD RID: 8397
				private readonly DbExpression _source;
			}

			// Token: 0x0200074C RID: 1868
			private class LimitSortLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x060047B5 RID: 18357 RVA: 0x00103572 File Offset: 0x00101772
				internal LimitSortLifter(DbLimitExpression limit, DbSortExpression sort, AliasGenerator aliasGenerator) : base(limit, aliasGenerator)
				{
					this._limit = limit;
					this._sort = sort;
					this._source = sort.Input.Expression;
				}

				// Token: 0x060047B6 RID: 18358 RVA: 0x0010359B File Offset: 0x0010179B
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return base.RebindSort(filter, this._sort);
				}

				// Token: 0x060047B7 RID: 18359 RVA: 0x00002391 File Offset: 0x00000591
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x060047B8 RID: 18360 RVA: 0x001035AC File Offset: 0x001017AC
				internal override DbExpression Limit(DbExpression k)
				{
					if (this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return base.MinimumLimit(this._sort, this._limit, k);
					}
					return base.RebindSort(this._limit, this._sort).Limit(k);
				}

				// Token: 0x060047B9 RID: 18361 RVA: 0x00103601 File Offset: 0x00101801
				internal override DbExpression Skip(DbExpression k)
				{
					return base.ApplySortOrderToSkip(this._limit, this._sort, k);
				}

				// Token: 0x040020CE RID: 8398
				private readonly DbLimitExpression _limit;

				// Token: 0x040020CF RID: 8399
				private readonly DbSortExpression _sort;

				// Token: 0x040020D0 RID: 8400
				private readonly DbExpression _source;
			}

			// Token: 0x0200074D RID: 1869
			private class ProjectLimitSkipLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x060047BA RID: 18362 RVA: 0x00103616 File Offset: 0x00101816
				internal ProjectLimitSkipLifter(DbProjectExpression project, DbLimitExpression limit, DbSkipExpression skip, AliasGenerator aliasGenerator) : base(project, aliasGenerator)
				{
					this._project = project;
					this._limit = limit;
					this._skip = skip;
					this._source = skip.Input.Expression;
				}

				// Token: 0x060047BB RID: 18363 RVA: 0x00103647 File Offset: 0x00101847
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return base.RebindProject(base.ApplySkipOrderToSort(base.ComposeFilter(this._skip.Limit(this._limit.Limit), this._project, filter), this._skip), this._project);
				}

				// Token: 0x060047BC RID: 18364 RVA: 0x00103684 File Offset: 0x00101884
				internal override DbExpression Project(DbProjectExpression project)
				{
					return base.ComposeProject(this._skip.Limit(this._limit.Limit), this._project, project);
				}

				// Token: 0x060047BD RID: 18365 RVA: 0x001036AC File Offset: 0x001018AC
				internal override DbExpression Limit(DbExpression k)
				{
					if (this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return base.RebindProject(base.MinimumLimit(this._skip, this._limit, k), this._project);
					}
					return base.RebindProject(base.ApplySkipOrderToSort(this._skip.Limit(this._limit.Limit), this._skip).Limit(k), this._project);
				}

				// Token: 0x060047BE RID: 18366 RVA: 0x0010372C File Offset: 0x0010192C
				internal override DbExpression Skip(DbExpression k)
				{
					if (this._skip.Count.ExpressionKind == DbExpressionKind.Constant && this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return base.RebindProject(base.SubtractFromLimit(base.AddToSkip(this._source, this._skip, k), this._limit, k), this._project);
					}
					return base.RebindProject(base.RebindSkip(this._skip.Limit(this._limit.Limit), this._skip, k), this._project);
				}

				// Token: 0x040020D1 RID: 8401
				private readonly DbProjectExpression _project;

				// Token: 0x040020D2 RID: 8402
				private readonly DbLimitExpression _limit;

				// Token: 0x040020D3 RID: 8403
				private readonly DbSkipExpression _skip;

				// Token: 0x040020D4 RID: 8404
				private readonly DbExpression _source;
			}

			// Token: 0x0200074E RID: 1870
			private class ProjectLimitSortLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x060047BF RID: 18367 RVA: 0x001037C4 File Offset: 0x001019C4
				internal ProjectLimitSortLifter(DbProjectExpression project, DbLimitExpression limit, DbSortExpression sort, AliasGenerator aliasGenerator) : base(project, aliasGenerator)
				{
					this._project = project;
					this._limit = limit;
					this._sort = sort;
					this._source = sort.Input.Expression;
				}

				// Token: 0x060047C0 RID: 18368 RVA: 0x001037F5 File Offset: 0x001019F5
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return base.RebindProject(base.RebindSort(base.ComposeFilter(this._sort.Limit(this._limit.Limit), this._project, filter), this._sort), this._project);
				}

				// Token: 0x060047C1 RID: 18369 RVA: 0x00103832 File Offset: 0x00101A32
				internal override DbExpression Project(DbProjectExpression project)
				{
					return base.ComposeProject(this._sort.Limit(this._limit.Limit), this._project, project);
				}

				// Token: 0x060047C2 RID: 18370 RVA: 0x00103858 File Offset: 0x00101A58
				internal override DbExpression Limit(DbExpression k)
				{
					if (this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return base.RebindProject(base.MinimumLimit(this._sort, this._limit, k), this._project);
					}
					return base.RebindProject(base.RebindSort(this._sort.Limit(this._limit.Limit), this._sort).Limit(k), this._project);
				}

				// Token: 0x060047C3 RID: 18371 RVA: 0x001038D5 File Offset: 0x00101AD5
				internal override DbExpression Skip(DbExpression k)
				{
					return base.RebindProject(base.ApplySortOrderToSkip(this._sort.Limit(this._limit.Limit), this._sort, k), this._project);
				}

				// Token: 0x040020D5 RID: 8405
				private readonly DbProjectExpression _project;

				// Token: 0x040020D6 RID: 8406
				private readonly DbLimitExpression _limit;

				// Token: 0x040020D7 RID: 8407
				private readonly DbSortExpression _sort;

				// Token: 0x040020D8 RID: 8408
				private readonly DbExpression _source;
			}

			// Token: 0x0200074F RID: 1871
			private class ProjectSkipLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x060047C4 RID: 18372 RVA: 0x00103906 File Offset: 0x00101B06
				internal ProjectSkipLifter(DbProjectExpression project, DbSkipExpression skip, AliasGenerator aliasGenerator) : base(project, aliasGenerator)
				{
					this._project = project;
					this._skip = skip;
					this._source = this._skip.Input.Expression;
				}

				// Token: 0x060047C5 RID: 18373 RVA: 0x00103934 File Offset: 0x00101B34
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return base.RebindProject(base.ApplySkipOrderToSort(base.ComposeFilter(this._skip, this._project, filter), this._skip), this._project);
				}

				// Token: 0x060047C6 RID: 18374 RVA: 0x00103961 File Offset: 0x00101B61
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x060047C7 RID: 18375 RVA: 0x0010396F File Offset: 0x00101B6F
				internal override DbExpression Project(DbProjectExpression project)
				{
					return base.ComposeProject(this._skip, this._project, project);
				}

				// Token: 0x060047C8 RID: 18376 RVA: 0x00103984 File Offset: 0x00101B84
				internal override DbExpression Skip(DbExpression k)
				{
					if (this._skip.Count.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return base.RebindProject(base.AddToSkip(this._source, this._skip, k), this._project);
					}
					return base.RebindProject(base.RebindSkip(this._skip, this._skip, k), this._project);
				}

				// Token: 0x040020D9 RID: 8409
				private readonly DbProjectExpression _project;

				// Token: 0x040020DA RID: 8410
				private readonly DbSkipExpression _skip;

				// Token: 0x040020DB RID: 8411
				private readonly DbExpression _source;
			}

			// Token: 0x02000750 RID: 1872
			private class SkipLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x060047C9 RID: 18377 RVA: 0x001039EC File Offset: 0x00101BEC
				internal SkipLifter(DbSkipExpression skip, AliasGenerator aliasGenerator) : base(skip, aliasGenerator)
				{
					this._skip = skip;
					this._source = skip.Input.Expression;
				}

				// Token: 0x060047CA RID: 18378 RVA: 0x00103A0E File Offset: 0x00101C0E
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return base.ApplySkipOrderToSort(filter, this._skip);
				}

				// Token: 0x060047CB RID: 18379 RVA: 0x00002391 File Offset: 0x00000591
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x060047CC RID: 18380 RVA: 0x00103961 File Offset: 0x00101B61
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x060047CD RID: 18381 RVA: 0x00103A20 File Offset: 0x00101C20
				internal override DbExpression Skip(DbExpression k)
				{
					if (this._skip.Count.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return base.AddToSkip(this._source, this._skip, k);
					}
					return base.RebindSkip(this._skip, this._skip, k);
				}

				// Token: 0x040020DC RID: 8412
				private readonly DbSkipExpression _skip;

				// Token: 0x040020DD RID: 8413
				private readonly DbExpression _source;
			}

			// Token: 0x02000751 RID: 1873
			private class ProjectSortLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x060047CE RID: 18382 RVA: 0x00103A70 File Offset: 0x00101C70
				internal ProjectSortLifter(DbProjectExpression project, DbSortExpression sort, AliasGenerator aliasGenerator) : base(project, aliasGenerator)
				{
					this._project = project;
					this._sort = sort;
					this._source = sort.Input.Expression;
				}

				// Token: 0x060047CF RID: 18383 RVA: 0x00103A99 File Offset: 0x00101C99
				internal override DbExpression Project(DbProjectExpression project)
				{
					return base.ComposeProject(this._sort, this._project, project);
				}

				// Token: 0x060047D0 RID: 18384 RVA: 0x00103AAE File Offset: 0x00101CAE
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return base.RebindProject(base.RebindSort(base.ComposeFilter(this._source, this._project, filter), this._sort), this._project);
				}

				// Token: 0x060047D1 RID: 18385 RVA: 0x00103961 File Offset: 0x00101B61
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x060047D2 RID: 18386 RVA: 0x00103ADB File Offset: 0x00101CDB
				internal override DbExpression Skip(DbExpression k)
				{
					return base.RebindProject(base.ApplySortOrderToSkip(this._source, this._sort, k), this._project);
				}

				// Token: 0x040020DE RID: 8414
				private readonly DbProjectExpression _project;

				// Token: 0x040020DF RID: 8415
				private readonly DbSortExpression _sort;

				// Token: 0x040020E0 RID: 8416
				private readonly DbExpression _source;
			}

			// Token: 0x02000752 RID: 1874
			private class SortLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x060047D3 RID: 18387 RVA: 0x00103AFC File Offset: 0x00101CFC
				internal SortLifter(DbSortExpression sort, AliasGenerator aliasGenerator) : base(sort, aliasGenerator)
				{
					this._sort = sort;
					this._source = sort.Input.Expression;
				}

				// Token: 0x060047D4 RID: 18388 RVA: 0x00002391 File Offset: 0x00000591
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x060047D5 RID: 18389 RVA: 0x00103B1E File Offset: 0x00101D1E
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return base.RebindSort(base.RebindFilter(this._source, filter), this._sort);
				}

				// Token: 0x060047D6 RID: 18390 RVA: 0x00103961 File Offset: 0x00101B61
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x060047D7 RID: 18391 RVA: 0x00103B39 File Offset: 0x00101D39
				internal override DbExpression Skip(DbExpression k)
				{
					return base.ApplySortOrderToSkip(this._source, this._sort, k);
				}

				// Token: 0x040020E1 RID: 8417
				private readonly DbSortExpression _sort;

				// Token: 0x040020E2 RID: 8418
				private readonly DbExpression _source;
			}

			// Token: 0x02000753 RID: 1875
			private class PassthroughOrderByLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x060047D8 RID: 18392 RVA: 0x00103B4E File Offset: 0x00101D4E
				internal PassthroughOrderByLifter(DbExpression source, AliasGenerator aliasGenerator) : base(source, aliasGenerator)
				{
				}

				// Token: 0x060047D9 RID: 18393 RVA: 0x00002391 File Offset: 0x00000591
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x060047DA RID: 18394 RVA: 0x00002391 File Offset: 0x00000591
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return filter;
				}

				// Token: 0x060047DB RID: 18395 RVA: 0x00103B58 File Offset: 0x00101D58
				internal override DbExpression OfType(TypeUsage type)
				{
					return this._root.OfType(type);
				}

				// Token: 0x060047DC RID: 18396 RVA: 0x00103961 File Offset: 0x00101B61
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x060047DD RID: 18397 RVA: 0x00103B66 File Offset: 0x00101D66
				internal override DbExpression Skip(DbExpression k)
				{
					throw EntityUtil.NotSupported(Strings.ELinq_SkipWithoutOrder);
				}
			}
		}

		// Token: 0x020004D4 RID: 1236
		private abstract class Translator
		{
			// Token: 0x06003D1C RID: 15644 RVA: 0x000E5312 File Offset: 0x000E3512
			protected Translator(params ExpressionType[] nodeTypes)
			{
				this._nodeTypes = nodeTypes;
			}

			// Token: 0x17000B05 RID: 2821
			// (get) Token: 0x06003D1D RID: 15645 RVA: 0x000E5321 File Offset: 0x000E3521
			internal IEnumerable<ExpressionType> NodeTypes
			{
				get
				{
					return this._nodeTypes;
				}
			}

			// Token: 0x06003D1E RID: 15646
			internal abstract DbExpression Translate(ExpressionConverter parent, Expression linq);

			// Token: 0x06003D1F RID: 15647 RVA: 0x000E5329 File Offset: 0x000E3529
			public override string ToString()
			{
				return base.GetType().Name;
			}

			// Token: 0x04001ADA RID: 6874
			private readonly ExpressionType[] _nodeTypes;
		}

		// Token: 0x020004D5 RID: 1237
		private abstract class TypedTranslator<T_Linq> : ExpressionConverter.Translator where T_Linq : Expression
		{
			// Token: 0x06003D20 RID: 15648 RVA: 0x000E5336 File Offset: 0x000E3536
			protected TypedTranslator(params ExpressionType[] nodeTypes) : base(nodeTypes)
			{
			}

			// Token: 0x06003D21 RID: 15649 RVA: 0x000E533F File Offset: 0x000E353F
			internal override DbExpression Translate(ExpressionConverter parent, Expression linq)
			{
				return this.TypedTranslate(parent, (T_Linq)((object)linq));
			}

			// Token: 0x06003D22 RID: 15650
			protected abstract DbExpression TypedTranslate(ExpressionConverter parent, T_Linq linq);
		}

		// Token: 0x020004D6 RID: 1238
		private sealed class ConstantTranslator : ExpressionConverter.TypedTranslator<ConstantExpression>
		{
			// Token: 0x06003D23 RID: 15651 RVA: 0x000E534E File Offset: 0x000E354E
			internal ConstantTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Constant
			})
			{
			}

			// Token: 0x06003D24 RID: 15652 RVA: 0x000E5364 File Offset: 0x000E3564
			protected override DbExpression TypedTranslate(ExpressionConverter parent, ConstantExpression linq)
			{
				if (linq == parent._funcletizer.RootContextExpression)
				{
					throw EntityUtil.InvalidOperation(Strings.ELinq_UnsupportedUseOfContextParameter(parent._funcletizer.RootContextParameter.Name));
				}
				ObjectQuery objectQuery = linq.Value as ObjectQuery;
				if (objectQuery != null)
				{
					return parent.TranslateInlineQueryOfT(objectQuery);
				}
				IEnumerable enumerable = linq.Value as IEnumerable;
				if (enumerable != null)
				{
					Type elementType = TypeSystem.GetElementType(linq.Type);
					if (elementType != null && elementType != linq.Type)
					{
						List<Expression> list = new List<Expression>();
						foreach (object value in enumerable)
						{
							list.Add(Expression.Constant(value, elementType));
						}
						parent._recompileRequired = (() => true);
						return parent.TranslateExpression(Expression.NewArrayInit(elementType, list));
					}
				}
				bool flag = linq.Value == null;
				bool flag2 = false;
				TypeUsage typeUsage;
				if (parent.TryGetValueLayerType(linq.Type, out typeUsage) && (Helper.IsScalarType(typeUsage.EdmType) || (flag && Helper.IsEntityType(typeUsage.EdmType))))
				{
					flag2 = true;
				}
				if (!flag2)
				{
					if (flag)
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedNullConstant(ExpressionConverter.DescribeClrType(linq.Type)));
					}
					throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedConstant(ExpressionConverter.DescribeClrType(linq.Type)));
				}
				else
				{
					if (flag)
					{
						return typeUsage.Null();
					}
					object value2 = linq.Value;
					if (Helper.IsPrimitiveType(typeUsage.EdmType))
					{
						Type nonNullableType = TypeSystem.GetNonNullableType(linq.Type);
						if (nonNullableType.IsEnum)
						{
							value2 = System.Convert.ChangeType(linq.Value, nonNullableType.GetEnumUnderlyingType(), CultureInfo.InvariantCulture);
						}
					}
					return typeUsage.Constant(value2);
				}
			}
		}

		// Token: 0x020004D7 RID: 1239
		private sealed class ParameterTranslator : ExpressionConverter.TypedTranslator<ParameterExpression>
		{
			// Token: 0x06003D25 RID: 15653 RVA: 0x000E5544 File Offset: 0x000E3744
			internal ParameterTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Parameter
			})
			{
			}

			// Token: 0x06003D26 RID: 15654 RVA: 0x000E5557 File Offset: 0x000E3757
			protected override DbExpression TypedTranslate(ExpressionConverter parent, ParameterExpression linq)
			{
				throw EntityUtil.InvalidOperation(Strings.ELinq_UnboundParameterExpression(linq.Name));
			}
		}

		// Token: 0x020004D8 RID: 1240
		private sealed class NewTranslator : ExpressionConverter.TypedTranslator<NewExpression>
		{
			// Token: 0x06003D27 RID: 15655 RVA: 0x000E5569 File Offset: 0x000E3769
			internal NewTranslator() : base(new ExpressionType[]
			{
				ExpressionType.New
			})
			{
			}

			// Token: 0x06003D28 RID: 15656 RVA: 0x000E557C File Offset: 0x000E377C
			protected override DbExpression TypedTranslate(ExpressionConverter parent, NewExpression linq)
			{
				int num = (linq.Members == null) ? 0 : linq.Members.Count;
				if (null == linq.Constructor || linq.Arguments.Count != num)
				{
					throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedConstructor);
				}
				parent.CheckInitializerType(linq.Type);
				List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>(num + 1);
				HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
				for (int i = 0; i < num; i++)
				{
					string text;
					Type type;
					MemberInfo memberInfo = TypeSystem.PropertyOrField(linq.Members[i], out text, out type);
					DbExpression value = parent.TranslateExpression(linq.Arguments[i]);
					hashSet.Add(text);
					list.Add(new KeyValuePair<string, DbExpression>(text, value));
				}
				InitializerMetadata initializerMetadata;
				if (num == 0)
				{
					list.Add(DbExpressionBuilder.True.As("Key"));
					initializerMetadata = InitializerMetadata.CreateEmptyProjectionInitializer(parent.EdmItemCollection, linq);
				}
				else
				{
					initializerMetadata = InitializerMetadata.CreateProjectionInitializer(parent.EdmItemCollection, linq);
				}
				parent.ValidateInitializerMetadata(initializerMetadata);
				return parent.CreateNewRowExpression(list, initializerMetadata);
			}
		}

		// Token: 0x020004D9 RID: 1241
		private sealed class NewArrayInitTranslator : ExpressionConverter.TypedTranslator<NewArrayExpression>
		{
			// Token: 0x06003D29 RID: 15657 RVA: 0x000E5683 File Offset: 0x000E3883
			internal NewArrayInitTranslator() : base(new ExpressionType[]
			{
				ExpressionType.NewArrayInit
			})
			{
			}

			// Token: 0x06003D2A RID: 15658 RVA: 0x000E5698 File Offset: 0x000E3898
			protected override DbExpression TypedTranslate(ExpressionConverter parent, NewArrayExpression linq)
			{
				if (linq.Expressions.Count > 0)
				{
					return DbExpressionBuilder.NewCollection(from e in linq.Expressions
					select parent.TranslateExpression(e));
				}
				TypeUsage collectionType;
				if (typeof(byte[]) == linq.Type)
				{
					TypeUsage elementType;
					if (parent.TryGetValueLayerType(typeof(byte), out elementType))
					{
						collectionType = TypeHelpers.CreateCollectionTypeUsage(elementType);
						return collectionType.NewEmptyCollection();
					}
				}
				else if (parent.TryGetValueLayerType(linq.Type, out collectionType))
				{
					return collectionType.NewEmptyCollection();
				}
				throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedType(ExpressionConverter.DescribeClrType(linq.Type)));
			}
		}

		// Token: 0x020004DA RID: 1242
		private sealed class ListInitTranslator : ExpressionConverter.TypedTranslator<ListInitExpression>
		{
			// Token: 0x06003D2B RID: 15659 RVA: 0x000E574C File Offset: 0x000E394C
			internal ListInitTranslator() : base(new ExpressionType[]
			{
				ExpressionType.ListInit
			})
			{
			}

			// Token: 0x06003D2C RID: 15660 RVA: 0x000E5760 File Offset: 0x000E3960
			protected override DbExpression TypedTranslate(ExpressionConverter parent, ListInitExpression linq)
			{
				if (linq.NewExpression.Constructor != null && linq.NewExpression.Constructor.GetParameters().Length != 0)
				{
					throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedConstructor);
				}
				if (linq.Initializers.Any((ElementInit i) => i.Arguments.Count != 1))
				{
					throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedInitializers);
				}
				return DbExpressionBuilder.NewCollection(from i in linq.Initializers
				select parent.TranslateExpression(i.Arguments[0]));
			}
		}

		// Token: 0x020004DB RID: 1243
		private sealed class MemberInitTranslator : ExpressionConverter.TypedTranslator<MemberInitExpression>
		{
			// Token: 0x06003D2D RID: 15661 RVA: 0x000E57FE File Offset: 0x000E39FE
			internal MemberInitTranslator() : base(new ExpressionType[]
			{
				ExpressionType.MemberInit
			})
			{
			}

			// Token: 0x06003D2E RID: 15662 RVA: 0x000E5814 File Offset: 0x000E3A14
			protected override DbExpression TypedTranslate(ExpressionConverter parent, MemberInitExpression linq)
			{
				if (null == linq.NewExpression.Constructor || linq.NewExpression.Constructor.GetParameters().Length != 0)
				{
					throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedConstructor);
				}
				parent.CheckInitializerType(linq.Type);
				List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>(linq.Bindings.Count + 1);
				MemberInfo[] array = new MemberInfo[linq.Bindings.Count];
				HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
				for (int i = 0; i < linq.Bindings.Count; i++)
				{
					MemberAssignment memberAssignment = linq.Bindings[i] as MemberAssignment;
					if (memberAssignment == null)
					{
						throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedBinding);
					}
					string text;
					Type type;
					MemberInfo memberInfo = TypeSystem.PropertyOrField(memberAssignment.Member, out text, out type);
					DbExpression value = parent.TranslateExpression(memberAssignment.Expression);
					hashSet.Add(text);
					array[i] = memberInfo;
					list.Add(new KeyValuePair<string, DbExpression>(text, value));
				}
				InitializerMetadata initializerMetadata;
				if (list.Count == 0)
				{
					list.Add(DbExpressionBuilder.Constant(true).As("Key"));
					initializerMetadata = InitializerMetadata.CreateEmptyProjectionInitializer(parent.EdmItemCollection, linq.NewExpression);
				}
				else
				{
					initializerMetadata = InitializerMetadata.CreateProjectionInitializer(parent.EdmItemCollection, linq, array);
				}
				parent.ValidateInitializerMetadata(initializerMetadata);
				return parent.CreateNewRowExpression(list, initializerMetadata);
			}
		}

		// Token: 0x020004DC RID: 1244
		private sealed class ConditionalTranslator : ExpressionConverter.TypedTranslator<ConditionalExpression>
		{
			// Token: 0x06003D2F RID: 15663 RVA: 0x000E5961 File Offset: 0x000E3B61
			internal ConditionalTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Conditional
			})
			{
			}

			// Token: 0x06003D30 RID: 15664 RVA: 0x000E5974 File Offset: 0x000E3B74
			protected override DbExpression TypedTranslate(ExpressionConverter parent, ConditionalExpression linq)
			{
				List<DbExpression> list = new List<DbExpression>(1);
				list.Add(parent.TranslateExpression(linq.Test));
				List<DbExpression> list2 = new List<DbExpression>(1);
				list2.Add(parent.TranslateExpression(linq.IfTrue));
				DbExpression elseExpression = parent.TranslateExpression(linq.IfFalse);
				return DbExpressionBuilder.Case(list, list2, elseExpression);
			}
		}

		// Token: 0x020004DD RID: 1245
		private sealed class NotSupportedTranslator : ExpressionConverter.Translator
		{
			// Token: 0x06003D31 RID: 15665 RVA: 0x000E5336 File Offset: 0x000E3536
			internal NotSupportedTranslator(params ExpressionType[] nodeTypes) : base(nodeTypes)
			{
			}

			// Token: 0x06003D32 RID: 15666 RVA: 0x000E59C8 File Offset: 0x000E3BC8
			internal override DbExpression Translate(ExpressionConverter parent, Expression linq)
			{
				throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedExpressionType(linq.NodeType));
			}
		}

		// Token: 0x020004DE RID: 1246
		private sealed class ExtensionTranslator : ExpressionConverter.Translator
		{
			// Token: 0x06003D33 RID: 15667 RVA: 0x000E59DF File Offset: 0x000E3BDF
			internal ExtensionTranslator() : base(new ExpressionType[]
			{
				(ExpressionType)(-1)
			})
			{
			}

			// Token: 0x06003D34 RID: 15668 RVA: 0x000E59F4 File Offset: 0x000E3BF4
			internal override DbExpression Translate(ExpressionConverter parent, Expression linq)
			{
				QueryParameterExpression queryParameterExpression = linq as QueryParameterExpression;
				if (queryParameterExpression == null)
				{
					throw EntityUtil.NotSupported(Strings.ELinq_UnsupportedExpressionType(linq.NodeType));
				}
				parent.AddParameter(queryParameterExpression);
				return queryParameterExpression.ParameterReference;
			}
		}

		// Token: 0x020004DF RID: 1247
		private abstract class BinaryTranslator : ExpressionConverter.TypedTranslator<BinaryExpression>
		{
			// Token: 0x06003D35 RID: 15669 RVA: 0x000E5A2E File Offset: 0x000E3C2E
			protected BinaryTranslator(params ExpressionType[] nodeTypes) : base(nodeTypes)
			{
			}

			// Token: 0x06003D36 RID: 15670 RVA: 0x000E5A37 File Offset: 0x000E3C37
			protected override DbExpression TypedTranslate(ExpressionConverter parent, BinaryExpression linq)
			{
				return this.TranslateBinary(parent, parent.TranslateExpression(linq.Left), parent.TranslateExpression(linq.Right), linq);
			}

			// Token: 0x06003D37 RID: 15671
			protected abstract DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq);
		}

		// Token: 0x020004E0 RID: 1248
		private sealed class CoalesceTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06003D38 RID: 15672 RVA: 0x000E5A59 File Offset: 0x000E3C59
			internal CoalesceTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Coalesce
			})
			{
			}

			// Token: 0x06003D39 RID: 15673 RVA: 0x000E5A6C File Offset: 0x000E3C6C
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				DbExpression item = parent.CreateIsNullExpression(left, linq.Left.Type);
				return DbExpressionBuilder.Case(new List<DbExpression>(1)
				{
					item
				}, new List<DbExpression>(1)
				{
					right
				}, left);
			}
		}

		// Token: 0x020004E1 RID: 1249
		private sealed class AndAlsoTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06003D3A RID: 15674 RVA: 0x000E5AB3 File Offset: 0x000E3CB3
			internal AndAlsoTranslator() : base(new ExpressionType[]
			{
				ExpressionType.AndAlso
			})
			{
			}

			// Token: 0x06003D3B RID: 15675 RVA: 0x000E5AC5 File Offset: 0x000E3CC5
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.And(right);
			}
		}

		// Token: 0x020004E2 RID: 1250
		private sealed class OrElseTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06003D3C RID: 15676 RVA: 0x000E5ACE File Offset: 0x000E3CCE
			internal OrElseTranslator() : base(new ExpressionType[]
			{
				ExpressionType.OrElse
			})
			{
			}

			// Token: 0x06003D3D RID: 15677 RVA: 0x000E5AE1 File Offset: 0x000E3CE1
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Or(right);
			}
		}

		// Token: 0x020004E3 RID: 1251
		private sealed class LessThanTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06003D3E RID: 15678 RVA: 0x000E5AEA File Offset: 0x000E3CEA
			internal LessThanTranslator() : base(new ExpressionType[]
			{
				ExpressionType.LessThan
			})
			{
			}

			// Token: 0x06003D3F RID: 15679 RVA: 0x000E5AFD File Offset: 0x000E3CFD
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.LessThan(right);
			}
		}

		// Token: 0x020004E4 RID: 1252
		private sealed class LessThanOrEqualsTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06003D40 RID: 15680 RVA: 0x000E5B06 File Offset: 0x000E3D06
			internal LessThanOrEqualsTranslator() : base(new ExpressionType[]
			{
				ExpressionType.LessThanOrEqual
			})
			{
			}

			// Token: 0x06003D41 RID: 15681 RVA: 0x000E5B19 File Offset: 0x000E3D19
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.LessThanOrEqual(right);
			}
		}

		// Token: 0x020004E5 RID: 1253
		private sealed class GreaterThanTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06003D42 RID: 15682 RVA: 0x000E5B22 File Offset: 0x000E3D22
			internal GreaterThanTranslator() : base(new ExpressionType[]
			{
				ExpressionType.GreaterThan
			})
			{
			}

			// Token: 0x06003D43 RID: 15683 RVA: 0x000E5B35 File Offset: 0x000E3D35
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.GreaterThan(right);
			}
		}

		// Token: 0x020004E6 RID: 1254
		private sealed class GreaterThanOrEqualsTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06003D44 RID: 15684 RVA: 0x000E5B3E File Offset: 0x000E3D3E
			internal GreaterThanOrEqualsTranslator() : base(new ExpressionType[]
			{
				ExpressionType.GreaterThanOrEqual
			})
			{
			}

			// Token: 0x06003D45 RID: 15685 RVA: 0x000E5B51 File Offset: 0x000E3D51
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.GreaterThanOrEqual(right);
			}
		}

		// Token: 0x020004E7 RID: 1255
		private sealed class EqualsTranslator : ExpressionConverter.TypedTranslator<BinaryExpression>
		{
			// Token: 0x06003D46 RID: 15686 RVA: 0x000E5B5A File Offset: 0x000E3D5A
			internal EqualsTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Equal
			})
			{
			}

			// Token: 0x06003D47 RID: 15687 RVA: 0x000E5B70 File Offset: 0x000E3D70
			protected override DbExpression TypedTranslate(ExpressionConverter parent, BinaryExpression linq)
			{
				Expression left = linq.Left;
				Expression right = linq.Right;
				bool flag = ExpressionConverter.EqualsTranslator.ExpressionIsNullConstant(left);
				bool flag2 = ExpressionConverter.EqualsTranslator.ExpressionIsNullConstant(right);
				if (flag && flag2)
				{
					return DbExpressionBuilder.True;
				}
				if (flag)
				{
					return ExpressionConverter.EqualsTranslator.CreateIsNullExpression(parent, right);
				}
				if (flag2)
				{
					return ExpressionConverter.EqualsTranslator.CreateIsNullExpression(parent, left);
				}
				DbExpression left2 = parent.TranslateExpression(left);
				DbExpression right2 = parent.TranslateExpression(right);
				ExpressionConverter.EqualsPattern pattern = ExpressionConverter.EqualsPattern.Store;
				if (parent._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior)
				{
					pattern = ExpressionConverter.EqualsPattern.PositiveNullEqualityComposable;
				}
				return parent.CreateEqualsExpression(left2, right2, pattern, left.Type, right.Type);
			}

			// Token: 0x06003D48 RID: 15688 RVA: 0x000E5C04 File Offset: 0x000E3E04
			private static DbExpression CreateIsNullExpression(ExpressionConverter parent, Expression input)
			{
				input = ExpressionConverter.EqualsTranslator.UnwrapConvert(input);
				DbExpression operand = parent.TranslateExpression(input);
				return parent.CreateIsNullExpression(operand, input.Type);
			}

			// Token: 0x06003D49 RID: 15689 RVA: 0x000E5C30 File Offset: 0x000E3E30
			private static bool ExpressionIsNullConstant(Expression expression)
			{
				expression = ExpressionConverter.EqualsTranslator.UnwrapConvert(expression);
				if (ExpressionType.Constant != expression.NodeType)
				{
					return false;
				}
				ConstantExpression constantExpression = (ConstantExpression)expression;
				return constantExpression.Value == null;
			}

			// Token: 0x06003D4A RID: 15690 RVA: 0x000E5C61 File Offset: 0x000E3E61
			private static Expression UnwrapConvert(Expression input)
			{
				while (ExpressionType.Convert == input.NodeType)
				{
					input = ((UnaryExpression)input).Operand;
				}
				return input;
			}
		}

		// Token: 0x020004E8 RID: 1256
		private sealed class NotEqualsTranslator : ExpressionConverter.TypedTranslator<BinaryExpression>
		{
			// Token: 0x06003D4B RID: 15691 RVA: 0x000E5C7D File Offset: 0x000E3E7D
			internal NotEqualsTranslator() : base(new ExpressionType[]
			{
				ExpressionType.NotEqual
			})
			{
			}

			// Token: 0x06003D4C RID: 15692 RVA: 0x000E5C90 File Offset: 0x000E3E90
			protected override DbExpression TypedTranslate(ExpressionConverter parent, BinaryExpression linq)
			{
				Expression linq2 = Expression.Not(Expression.Equal(linq.Left, linq.Right));
				return parent.TranslateExpression(linq2);
			}
		}

		// Token: 0x020004E9 RID: 1257
		private sealed class IsTranslator : ExpressionConverter.TypedTranslator<TypeBinaryExpression>
		{
			// Token: 0x06003D4D RID: 15693 RVA: 0x000E5CBB File Offset: 0x000E3EBB
			internal IsTranslator() : base(new ExpressionType[]
			{
				ExpressionType.TypeIs
			})
			{
			}

			// Token: 0x06003D4E RID: 15694 RVA: 0x000E5CD0 File Offset: 0x000E3ED0
			protected override DbExpression TypedTranslate(ExpressionConverter parent, TypeBinaryExpression linq)
			{
				DbExpression dbExpression = parent.TranslateExpression(linq.Expression);
				TypeUsage resultType = dbExpression.ResultType;
				TypeUsage isOrAsTargetType = parent.GetIsOrAsTargetType(resultType, ExpressionType.TypeIs, linq.TypeOperand, linq.Expression.Type);
				return dbExpression.IsOf(isOrAsTargetType);
			}
		}

		// Token: 0x020004EA RID: 1258
		private sealed class AddTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06003D4F RID: 15695 RVA: 0x000E5D13 File Offset: 0x000E3F13
			internal AddTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Add,
				ExpressionType.AddChecked
			})
			{
			}

			// Token: 0x06003D50 RID: 15696 RVA: 0x000E5D28 File Offset: 0x000E3F28
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				if (TypeSemantics.IsPrimitiveType(left.ResultType, PrimitiveTypeKind.String) && TypeSemantics.IsPrimitiveType(right.ResultType, PrimitiveTypeKind.String))
				{
					return parent.CreateCanonicalFunction("Concat", linq, new DbExpression[]
					{
						left,
						right
					});
				}
				return left.Plus(right);
			}
		}

		// Token: 0x020004EB RID: 1259
		private sealed class DivideTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06003D51 RID: 15697 RVA: 0x000E5D76 File Offset: 0x000E3F76
			internal DivideTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Divide
			})
			{
			}

			// Token: 0x06003D52 RID: 15698 RVA: 0x000E5D89 File Offset: 0x000E3F89
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Divide(right);
			}
		}

		// Token: 0x020004EC RID: 1260
		private sealed class ModuloTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06003D53 RID: 15699 RVA: 0x000E5D92 File Offset: 0x000E3F92
			internal ModuloTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Modulo
			})
			{
			}

			// Token: 0x06003D54 RID: 15700 RVA: 0x000E5DA5 File Offset: 0x000E3FA5
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Modulo(right);
			}
		}

		// Token: 0x020004ED RID: 1261
		private sealed class MultiplyTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06003D55 RID: 15701 RVA: 0x000E5DAE File Offset: 0x000E3FAE
			internal MultiplyTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Multiply,
				ExpressionType.MultiplyChecked
			})
			{
			}

			// Token: 0x06003D56 RID: 15702 RVA: 0x000E5DC6 File Offset: 0x000E3FC6
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Multiply(right);
			}
		}

		// Token: 0x020004EE RID: 1262
		private sealed class SubtractTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06003D57 RID: 15703 RVA: 0x000E5DCF File Offset: 0x000E3FCF
			internal SubtractTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Subtract,
				ExpressionType.SubtractChecked
			})
			{
			}

			// Token: 0x06003D58 RID: 15704 RVA: 0x000E5DE7 File Offset: 0x000E3FE7
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Minus(right);
			}
		}

		// Token: 0x020004EF RID: 1263
		private sealed class NegateTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x06003D59 RID: 15705 RVA: 0x000E5DF0 File Offset: 0x000E3FF0
			internal NegateTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Negate,
				ExpressionType.NegateChecked
			})
			{
			}

			// Token: 0x06003D5A RID: 15706 RVA: 0x000E5E08 File Offset: 0x000E4008
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				return operand.UnaryMinus();
			}
		}

		// Token: 0x020004F0 RID: 1264
		private sealed class UnaryPlusTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x06003D5B RID: 15707 RVA: 0x000E5E10 File Offset: 0x000E4010
			internal UnaryPlusTranslator() : base(new ExpressionType[]
			{
				ExpressionType.UnaryPlus
			})
			{
			}

			// Token: 0x06003D5C RID: 15708 RVA: 0x000E5E23 File Offset: 0x000E4023
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				return operand;
			}
		}

		// Token: 0x020004F1 RID: 1265
		private abstract class BitwiseBinaryTranslator : ExpressionConverter.TypedTranslator<BinaryExpression>
		{
			// Token: 0x06003D5D RID: 15709 RVA: 0x000E5E26 File Offset: 0x000E4026
			protected BitwiseBinaryTranslator(ExpressionType nodeType, string canonicalFunctionName) : base(new ExpressionType[]
			{
				nodeType
			})
			{
				this._canonicalFunctionName = canonicalFunctionName;
			}

			// Token: 0x06003D5E RID: 15710 RVA: 0x000E5E40 File Offset: 0x000E4040
			protected override DbExpression TypedTranslate(ExpressionConverter parent, BinaryExpression linq)
			{
				DbExpression dbExpression = parent.TranslateExpression(linq.Left);
				DbExpression dbExpression2 = parent.TranslateExpression(linq.Right);
				if (TypeSemantics.IsBooleanType(dbExpression.ResultType))
				{
					return this.TranslateIntoLogicExpression(parent, linq, dbExpression, dbExpression2);
				}
				return parent.CreateCanonicalFunction(this._canonicalFunctionName, linq, new DbExpression[]
				{
					dbExpression,
					dbExpression2
				});
			}

			// Token: 0x06003D5F RID: 15711
			protected abstract DbExpression TranslateIntoLogicExpression(ExpressionConverter parent, BinaryExpression linq, DbExpression left, DbExpression right);

			// Token: 0x04001ADB RID: 6875
			private readonly string _canonicalFunctionName;
		}

		// Token: 0x020004F2 RID: 1266
		private sealed class AndTranslator : ExpressionConverter.BitwiseBinaryTranslator
		{
			// Token: 0x06003D60 RID: 15712 RVA: 0x000E5E9A File Offset: 0x000E409A
			internal AndTranslator() : base(ExpressionType.And, "BitwiseAnd")
			{
			}

			// Token: 0x06003D61 RID: 15713 RVA: 0x000E5EA8 File Offset: 0x000E40A8
			protected override DbExpression TranslateIntoLogicExpression(ExpressionConverter parent, BinaryExpression linq, DbExpression left, DbExpression right)
			{
				return left.And(right);
			}
		}

		// Token: 0x020004F3 RID: 1267
		private sealed class OrTranslator : ExpressionConverter.BitwiseBinaryTranslator
		{
			// Token: 0x06003D62 RID: 15714 RVA: 0x000E5EB2 File Offset: 0x000E40B2
			internal OrTranslator() : base(ExpressionType.Or, "BitwiseOr")
			{
			}

			// Token: 0x06003D63 RID: 15715 RVA: 0x000E5EC1 File Offset: 0x000E40C1
			protected override DbExpression TranslateIntoLogicExpression(ExpressionConverter parent, BinaryExpression linq, DbExpression left, DbExpression right)
			{
				return left.Or(right);
			}
		}

		// Token: 0x020004F4 RID: 1268
		private sealed class ExclusiveOrTranslator : ExpressionConverter.BitwiseBinaryTranslator
		{
			// Token: 0x06003D64 RID: 15716 RVA: 0x000E5ECB File Offset: 0x000E40CB
			internal ExclusiveOrTranslator() : base(ExpressionType.ExclusiveOr, "BitwiseXor")
			{
			}

			// Token: 0x06003D65 RID: 15717 RVA: 0x000E5EDC File Offset: 0x000E40DC
			protected override DbExpression TranslateIntoLogicExpression(ExpressionConverter parent, BinaryExpression linq, DbExpression left, DbExpression right)
			{
				DbExpression left2 = left.And(right.Not());
				DbExpression right2 = left.Not().And(right);
				return left2.Or(right2);
			}
		}

		// Token: 0x020004F5 RID: 1269
		private sealed class NotTranslator : ExpressionConverter.TypedTranslator<UnaryExpression>
		{
			// Token: 0x06003D66 RID: 15718 RVA: 0x000E5F0E File Offset: 0x000E410E
			internal NotTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Not
			})
			{
			}

			// Token: 0x06003D67 RID: 15719 RVA: 0x000E5F24 File Offset: 0x000E4124
			protected override DbExpression TypedTranslate(ExpressionConverter parent, UnaryExpression linq)
			{
				DbExpression dbExpression = parent.TranslateExpression(linq.Operand);
				if (TypeSemantics.IsBooleanType(dbExpression.ResultType))
				{
					return dbExpression.Not();
				}
				return parent.CreateCanonicalFunction("BitwiseNot", linq, new DbExpression[]
				{
					dbExpression
				});
			}
		}

		// Token: 0x020004F6 RID: 1270
		private abstract class UnaryTranslator : ExpressionConverter.TypedTranslator<UnaryExpression>
		{
			// Token: 0x06003D68 RID: 15720 RVA: 0x000E5F68 File Offset: 0x000E4168
			protected UnaryTranslator(params ExpressionType[] nodeTypes) : base(nodeTypes)
			{
			}

			// Token: 0x06003D69 RID: 15721 RVA: 0x000E5F71 File Offset: 0x000E4171
			protected override DbExpression TypedTranslate(ExpressionConverter parent, UnaryExpression linq)
			{
				return this.TranslateUnary(parent, linq, parent.TranslateExpression(linq.Operand));
			}

			// Token: 0x06003D6A RID: 15722
			protected abstract DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand);
		}

		// Token: 0x020004F7 RID: 1271
		private sealed class QuoteTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x06003D6B RID: 15723 RVA: 0x000E5F87 File Offset: 0x000E4187
			internal QuoteTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Quote
			})
			{
			}

			// Token: 0x06003D6C RID: 15724 RVA: 0x000E5E23 File Offset: 0x000E4023
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				return operand;
			}
		}

		// Token: 0x020004F8 RID: 1272
		private sealed class ConvertTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x06003D6D RID: 15725 RVA: 0x000E5F9A File Offset: 0x000E419A
			internal ConvertTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Convert,
				ExpressionType.ConvertChecked
			})
			{
			}

			// Token: 0x06003D6E RID: 15726 RVA: 0x000E5FB4 File Offset: 0x000E41B4
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				Type type = unary.Type;
				Type type2 = unary.Operand.Type;
				return parent.CreateCastExpression(operand, type, type2);
			}
		}

		// Token: 0x020004F9 RID: 1273
		private sealed class AsTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x06003D6F RID: 15727 RVA: 0x000E5FDF File Offset: 0x000E41DF
			internal AsTranslator() : base(new ExpressionType[]
			{
				ExpressionType.TypeAs
			})
			{
			}

			// Token: 0x06003D70 RID: 15728 RVA: 0x000E5FF4 File Offset: 0x000E41F4
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				TypeUsage resultType = operand.ResultType;
				TypeUsage isOrAsTargetType = parent.GetIsOrAsTargetType(resultType, ExpressionType.TypeAs, unary.Type, unary.Operand.Type);
				return operand.TreatAs(isOrAsTargetType);
			}
		}

		// Token: 0x020004FA RID: 1274
		private class ParameterReferenceRemover : DefaultExpressionVisitor
		{
			// Token: 0x06003D71 RID: 15729 RVA: 0x000E602C File Offset: 0x000E422C
			internal static DbExpression RemoveParameterReferences(DbExpression expression, ObjectParameterCollection availableParameters)
			{
				ExpressionConverter.ParameterReferenceRemover parameterReferenceRemover = new ExpressionConverter.ParameterReferenceRemover(availableParameters);
				return parameterReferenceRemover.VisitExpression(expression);
			}

			// Token: 0x06003D72 RID: 15730 RVA: 0x000E6047 File Offset: 0x000E4247
			private ParameterReferenceRemover(ObjectParameterCollection availableParams)
			{
				this.objectParameters = availableParams;
			}

			// Token: 0x06003D73 RID: 15731 RVA: 0x000E6058 File Offset: 0x000E4258
			public override DbExpression Visit(DbParameterReferenceExpression expression)
			{
				if (!this.objectParameters.Contains(expression.ParameterName))
				{
					return expression;
				}
				ObjectParameter objectParameter = this.objectParameters[expression.ParameterName];
				if (objectParameter.Value == null)
				{
					return expression.ResultType.Null();
				}
				return expression.ResultType.Constant(objectParameter.Value);
			}

			// Token: 0x04001ADC RID: 6876
			private readonly ObjectParameterCollection objectParameters;
		}

		// Token: 0x020004FB RID: 1275
		private enum EqualsPattern
		{
			// Token: 0x04001ADE RID: 6878
			Store,
			// Token: 0x04001ADF RID: 6879
			PositiveNullEqualityNonComposable,
			// Token: 0x04001AE0 RID: 6880
			PositiveNullEqualityComposable
		}
	}
}
