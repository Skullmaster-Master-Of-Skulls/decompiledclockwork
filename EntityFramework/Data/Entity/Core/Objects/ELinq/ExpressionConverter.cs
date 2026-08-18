using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x0200002F RID: 47
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal sealed class ExpressionConverter
	{
		// Token: 0x06000205 RID: 517 RVA: 0x0000BD74 File Offset: 0x00009F74
		internal ExpressionConverter(Funcletizer funcletizer, Expression expression)
		{
			this._funcletizer = funcletizer;
			expression = funcletizer.Funcletize(expression, out this._recompileRequired);
			LinqExpressionNormalizer linqExpressionNormalizer = new LinqExpressionNormalizer();
			this._expression = linqExpressionNormalizer.Visit(expression);
			this._perspective = funcletizer.RootContext.Perspective;
			this._bindingContext = new BindingContext();
			this._ignoreInclude = 0;
			this._orderByLifter = new ExpressionConverter.OrderByLifter(this._aliasGenerator);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000BDF8 File Offset: 0x00009FF8
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

		// Token: 0x06000207 RID: 519 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
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
			yield return new ExpressionConverter.PowerTranslator();
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
			yield return new ExpressionConverter.NotSupportedTranslator(new ExpressionType[]
			{
				ExpressionType.LeftShift,
				ExpressionType.RightShift,
				ExpressionType.ArrayLength,
				ExpressionType.ArrayIndex,
				ExpressionType.Invoke,
				ExpressionType.Lambda,
				ExpressionType.NewArrayBounds
			});
			yield break;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000C3DE File Offset: 0x0000A5DE
		private EdmItemCollection EdmItemCollection
		{
			get
			{
				return (EdmItemCollection)this._funcletizer.RootContext.MetadataWorkspace.GetItemCollection(DataSpace.CSpace, true);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		internal DbProviderManifest ProviderManifest
		{
			get
			{
				return ((StoreItemCollection)this._funcletizer.RootContext.MetadataWorkspace.GetItemCollection(DataSpace.SSpace)).ProviderManifest;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000C41E File Offset: 0x0000A61E
		internal IEnumerable<Tuple<ObjectParameter, QueryParameterExpression>> GetParameters()
		{
			if (this._parameters != null)
			{
				return this._parameters;
			}
			return null;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000C430 File Offset: 0x0000A630
		internal MergeOption? PropagatedMergeOption
		{
			get
			{
				return this._mergeOption;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000C438 File Offset: 0x0000A638
		internal Span PropagatedSpan
		{
			get
			{
				return this._span;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000C440 File Offset: 0x0000A640
		internal Func<bool> RecompileRequired
		{
			get
			{
				return this._recompileRequired;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000C448 File Offset: 0x0000A648
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000C450 File Offset: 0x0000A650
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000C459 File Offset: 0x0000A659
		internal AliasGenerator AliasGenerator
		{
			get
			{
				return this._aliasGenerator;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000C464 File Offset: 0x0000A664
		internal DbExpression Convert()
		{
			DbExpression dbExpression = this.TranslateExpression(this._expression);
			if (!this.TryGetSpan(dbExpression, out this._span))
			{
				this._span = null;
			}
			return dbExpression;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000C495 File Offset: 0x0000A695
		internal static bool CanFuncletizePropertyInfo(PropertyInfo propertyInfo)
		{
			return ExpressionConverter.MemberAccessTranslator.CanFuncletizePropertyInfo(propertyInfo);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000C49D File Offset: 0x0000A69D
		internal bool CanIncludeSpanInfo()
		{
			return this._ignoreInclude == 0;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		private void NotifyMergeOption(MergeOption mergeOption)
		{
			if (this._mergeOption == null)
			{
				this._mergeOption = new MergeOption?(mergeOption);
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		[SuppressMessage("Microsoft.Usage", "CA2301", Justification = "metadata.ClrType is not expected to be an Embedded Interop Type.")]
		internal void ValidateInitializerMetadata(InitializerMetadata metadata)
		{
			InitializerMetadata other;
			if (this._initializers != null && this._initializers.TryGetValue(metadata.ClrType, out other))
			{
				if (!metadata.Equals(other))
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedHeterogeneousInitializers(ExpressionConverter.DescribeClrType(metadata.ClrType)));
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

		// Token: 0x06000216 RID: 534 RVA: 0x0000C53C File Offset: 0x0000A73C
		private void AddParameter(QueryParameterExpression queryParameter)
		{
			if (this._parameters == null)
			{
				this._parameters = new List<Tuple<ObjectParameter, QueryParameterExpression>>();
			}
			if (!(from p in this._parameters
			select p.Item2).Contains(queryParameter))
			{
				ObjectParameter item = new ObjectParameter(queryParameter.ParameterReference.ParameterName, queryParameter.Type);
				this._parameters.Add(new Tuple<ObjectParameter, QueryParameterExpression>(item, queryParameter));
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000C5B5 File Offset: 0x0000A7B5
		private bool IsQueryRoot(Expression Expression)
		{
			return object.ReferenceEquals(this._expression, Expression);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
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

		// Token: 0x06000219 RID: 537 RVA: 0x0000C664 File Offset: 0x0000A864
		private bool TryGetSpan(DbExpression expression, out Span span)
		{
			if (this._spanMappings != null)
			{
				return this._spanMappings.TryGetValue(expression, out span);
			}
			span = null;
			return false;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C680 File Offset: 0x0000A880
		private void ApplySpanMapping(DbExpression from, DbExpression to)
		{
			Span span;
			if (this.TryGetSpan(from, out span))
			{
				this.AddSpanMapping(to, span);
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
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

		// Token: 0x0600021C RID: 540 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
		private DbDistinctExpression Distinct(DbExpression argument)
		{
			DbDistinctExpression dbDistinctExpression = argument.Distinct();
			this.ApplySpanMapping(argument, dbDistinctExpression);
			return dbDistinctExpression;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000C700 File Offset: 0x0000A900
		private DbExceptExpression Except(DbExpression left, DbExpression right)
		{
			DbExceptExpression dbExceptExpression = left.Except(right);
			this.ApplySpanMapping(left, dbExceptExpression);
			return dbExceptExpression;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000C720 File Offset: 0x0000A920
		private DbExpression Filter(DbExpressionBinding input, DbExpression predicate)
		{
			DbExpression dbExpression = this._orderByLifter.Filter(input, predicate);
			this.ApplySpanMapping(input.Expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000C74C File Offset: 0x0000A94C
		private DbIntersectExpression Intersect(DbExpression left, DbExpression right)
		{
			DbIntersectExpression dbIntersectExpression = left.Intersect(right);
			this.UnifySpanMappings(left, right, dbIntersectExpression);
			return dbIntersectExpression;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000C76C File Offset: 0x0000A96C
		private DbExpression Limit(DbExpression argument, DbExpression limit)
		{
			DbExpression dbExpression = this._orderByLifter.Limit(argument, limit);
			this.ApplySpanMapping(argument, dbExpression);
			return dbExpression;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000C790 File Offset: 0x0000A990
		private DbExpression OfType(DbExpression argument, TypeUsage ofType)
		{
			DbExpression dbExpression = this._orderByLifter.OfType(argument, ofType);
			this.ApplySpanMapping(argument, dbExpression);
			return dbExpression;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		private DbExpression Project(DbExpressionBinding input, DbExpression projection)
		{
			DbExpression dbExpression = this._orderByLifter.Project(input, projection);
			if (projection.ExpressionKind == DbExpressionKind.VariableReference && ((DbVariableReferenceExpression)projection).VariableName.Equals(input.VariableName, StringComparison.Ordinal))
			{
				this.ApplySpanMapping(input.Expression, dbExpression);
			}
			return dbExpression;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000C800 File Offset: 0x0000AA00
		private DbSortExpression Sort(DbExpressionBinding input, IList<DbSortClause> keys)
		{
			DbSortExpression dbSortExpression = input.Sort(keys);
			this.ApplySpanMapping(input.Expression, dbSortExpression);
			return dbSortExpression;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000C824 File Offset: 0x0000AA24
		private DbExpression Skip(DbExpressionBinding input, DbExpression skipCount)
		{
			DbExpression dbExpression = this._orderByLifter.Skip(input, skipCount);
			this.ApplySpanMapping(input.Expression, dbExpression);
			return dbExpression;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000C850 File Offset: 0x0000AA50
		private DbUnionAllExpression UnionAll(DbExpression left, DbExpression right)
		{
			DbUnionAllExpression dbUnionAllExpression = left.UnionAll(right);
			this.UnifySpanMappings(left, right, dbUnionAllExpression);
			return dbUnionAllExpression;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000C870 File Offset: 0x0000AA70
		private TypeUsage GetCastTargetType(TypeUsage fromType, Type toClrType, Type fromClrType, bool preserveCastForDateTime)
		{
			if (fromClrType != null && fromClrType.IsGenericType() && toClrType.IsGenericType() && (fromClrType.GetGenericTypeDefinition() == typeof(ObjectQuery<>) || fromClrType.GetGenericTypeDefinition() == typeof(IQueryable<>) || fromClrType.GetGenericTypeDefinition() == typeof(IOrderedQueryable<>)) && (toClrType.GetGenericTypeDefinition() == typeof(ObjectQuery<>) || toClrType.GetGenericTypeDefinition() == typeof(IQueryable<>) || toClrType.GetGenericTypeDefinition() == typeof(IOrderedQueryable<>)) && fromClrType.GetGenericArguments()[0] == toClrType.GetGenericArguments()[0])
			{
				return null;
			}
			if (fromClrType != null && TypeSystem.GetNonNullableType(fromClrType).IsEnum && toClrType == typeof(Enum))
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

		// Token: 0x06000227 RID: 551 RVA: 0x0000C990 File Offset: 0x0000AB90
		private static TypeUsage ValidateAndAdjustCastTypes(TypeUsage toType, TypeUsage fromType, Type toClrType, Type fromClrType)
		{
			if (toType == null || !TypeSemantics.IsScalarType(toType) || !TypeSemantics.IsScalarType(fromType))
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedCast(ExpressionConverter.DescribeClrType(fromClrType), ExpressionConverter.DescribeClrType(toClrType)));
			}
			PrimitiveTypeKind primitiveTypeKind = Helper.AsPrimitive(fromType.EdmType).PrimitiveTypeKind;
			PrimitiveTypeKind primitiveTypeKind2 = Helper.AsPrimitive(toType.EdmType).PrimitiveTypeKind;
			if (primitiveTypeKind2 == PrimitiveTypeKind.Decimal)
			{
				PrimitiveTypeKind primitiveTypeKind3 = primitiveTypeKind;
				if (primitiveTypeKind3 != PrimitiveTypeKind.Byte)
				{
					switch (primitiveTypeKind3)
					{
					case PrimitiveTypeKind.SByte:
					case PrimitiveTypeKind.Int16:
					case PrimitiveTypeKind.Int32:
					case PrimitiveTypeKind.Int64:
						break;
					default:
						throw new NotSupportedException(Strings.ELinq_UnsupportedCastToDecimal);
					}
				}
				toType = TypeUsage.CreateDecimalTypeUsage((PrimitiveType)toType.EdmType, 19, 0);
			}
			return toType;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000CA30 File Offset: 0x0000AC30
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

		// Token: 0x06000229 RID: 553 RVA: 0x0000CA88 File Offset: 0x0000AC88
		private TypeUsage GetIsOrAsTargetType(ExpressionType operationType, Type toClrType, Type fromClrType)
		{
			TypeUsage typeUsage;
			if (!this.TryGetValueLayerType(toClrType, out typeUsage) || (!TypeSemantics.IsEntityType(typeUsage) && !TypeSemantics.IsComplexType(typeUsage)))
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedIsOrAs(operationType, ExpressionConverter.DescribeClrType(fromClrType), ExpressionConverter.DescribeClrType(toClrType)));
			}
			return typeUsage;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
		private DbExpression TranslateInlineQueryOfT(ObjectQuery inlineQuery)
		{
			if (!object.ReferenceEquals(this._funcletizer.RootContext, inlineQuery.QueryState.ObjectContext))
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedDifferentContexts);
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
						this._parameters = new List<Tuple<ObjectParameter, QueryParameterExpression>>();
					}
					foreach (ObjectParameter objectParameter in inlineQuery.QueryState.Parameters)
					{
						this._parameters.Add(new Tuple<ObjectParameter, QueryParameterExpression>(objectParameter.ShallowCopy(), null));
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

		// Token: 0x0600022B RID: 555 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
		private DbExpression CreateCastExpression(DbExpression source, Type toClrType, Type fromClrType)
		{
			DbExpression dbExpression = this.NormalizeSetSource(source);
			if (!object.ReferenceEquals(source, dbExpression) && this.GetCastTargetType(dbExpression.ResultType, toClrType, fromClrType, true) == null)
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

		// Token: 0x0600022C RID: 556 RVA: 0x0000CC35 File Offset: 0x0000AE35
		private DbExpression TranslateLambda(LambdaExpression lambda, DbExpression input, out DbExpressionBinding binding)
		{
			input = this.NormalizeSetSource(input);
			binding = input.BindAs(this._aliasGenerator.Next());
			return this.TranslateLambda(lambda, binding.Variable);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000CC61 File Offset: 0x0000AE61
		private DbExpression TranslateLambda(LambdaExpression lambda, DbExpression input, string bindingName, out DbExpressionBinding binding)
		{
			input = this.NormalizeSetSource(input);
			binding = input.BindAs(bindingName);
			return this.TranslateLambda(lambda, binding.Variable);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000CC88 File Offset: 0x0000AE88
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

		// Token: 0x0600022F RID: 559 RVA: 0x0000CCDC File Offset: 0x0000AEDC
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

		// Token: 0x06000230 RID: 560 RVA: 0x0000CD40 File Offset: 0x0000AF40
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

		// Token: 0x06000231 RID: 561 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
		private LambdaExpression GetLambdaExpression(MethodCallExpression callExpression, int argumentOrdinal)
		{
			Expression argument = callExpression.Arguments[argumentOrdinal];
			return (LambdaExpression)this.GetLambdaExpression(argument);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000CDF0 File Offset: 0x0000AFF0
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
			if (ExpressionType.Call == argument.NodeType)
			{
				if (typeof(Expression).IsAssignableFrom(argument.Type))
				{
					Func<Expression> func = Expression.Lambda<Func<Expression>>(argument, new ParameterExpression[0]).Compile();
					return this.GetLambdaExpression(func());
				}
			}
			else if (ExpressionType.Invoke == argument.NodeType && typeof(Expression).IsAssignableFrom(argument.Type))
			{
				Func<Expression> func2 = Expression.Lambda<Func<Expression>>(argument, new ParameterExpression[0]).Compile();
				return this.GetLambdaExpression(func2());
			}
			throw new InvalidOperationException(Strings.ADP_InternalProviderError(1025));
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		private DbExpression TranslateSet(Expression linq)
		{
			return this.NormalizeSetSource(this.TranslateExpression(linq));
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000CEC8 File Offset: 0x0000B0C8
		private DbExpression TranslateExpression(Expression linq)
		{
			DbExpression result;
			if (!this._bindingContext.TryGetBoundExpression(linq, out result))
			{
				ExpressionConverter.Translator translator;
				if (!ExpressionConverter._translators.TryGetValue(linq.NodeType, out translator))
				{
					throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UnknownLinqNodeType, -1, linq.NodeType.ToString());
				}
				result = translator.Translate(this, linq);
			}
			return result;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000CF24 File Offset: 0x0000B124
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

		// Token: 0x06000236 RID: 566 RVA: 0x0000CF50 File Offset: 0x0000B150
		private void CheckInitializerType(Type type)
		{
			TypeUsage typeUsage;
			if (this._funcletizer.RootContext.Perspective.TryGetType(type, out typeUsage))
			{
				BuiltInTypeKind builtInTypeKind = typeUsage.EdmType.BuiltInTypeKind;
				if (BuiltInTypeKind.EntityType == builtInTypeKind || BuiltInTypeKind.ComplexType == builtInTypeKind)
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedNominalType(typeUsage.EdmType.FullName));
				}
			}
			if (TypeSystem.IsSequenceType(type))
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedEnumerableType(ExpressionConverter.DescribeClrType(type)));
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000CFBC File Offset: 0x0000B1BC
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

		// Token: 0x06000238 RID: 568 RVA: 0x0000D068 File Offset: 0x0000B268
		private TypeUsage GetValueLayerType(Type linqType)
		{
			TypeUsage result;
			if (!this.TryGetValueLayerType(linqType, out result))
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedType(linqType));
			}
			return result;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000D090 File Offset: 0x0000B290
		private bool TryGetValueLayerType(Type linqType, out TypeUsage type)
		{
			Type type2 = TypeSystem.GetNonNullableType(linqType);
			if (type2.IsEnum() && this.EdmItemCollection.EdmVersion < 3.0)
			{
				type2 = type2.GetEnumUnderlyingType();
			}
			PrimitiveTypeKind primitiveTypeKind;
			if (ClrProviderManifest.TryGetPrimitiveTypeKind(type2, out primitiveTypeKind))
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
			if (!this._perspective.TryGetTypeByName(type2.FullNameWithNesting(), false, out type) && type2.IsEnum() && ClrProviderManifest.TryGetPrimitiveTypeKind(type2.GetEnumUnderlyingType(), out primitiveTypeKind))
			{
				type = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(primitiveTypeKind);
			}
			return type != null;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000D15C File Offset: 0x0000B35C
		private static void VerifyTypeSupportedForComparison(Type clrType, TypeUsage edmType, Stack<EdmMember> memberPath)
		{
			BuiltInTypeKind builtInTypeKind = edmType.EdmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.PrimitiveType)
			{
				switch (builtInTypeKind)
				{
				case BuiltInTypeKind.EntityType:
				case BuiltInTypeKind.EnumType:
					break;
				default:
					if (builtInTypeKind != BuiltInTypeKind.PrimitiveType)
					{
						goto IL_65;
					}
					break;
				}
			}
			else if (builtInTypeKind != BuiltInTypeKind.RefType)
			{
				if (builtInTypeKind != BuiltInTypeKind.RowType)
				{
					goto IL_65;
				}
				InitializerMetadata initializerMetadata;
				if (!InitializerMetadata.TryGetInitializerMetadata(edmType, out initializerMetadata) || initializerMetadata.Kind == InitializerMetadataKind.ProjectionInitializer || initializerMetadata.Kind == InitializerMetadataKind.ProjectionNew)
				{
					ExpressionConverter.VerifyRowTypeSupportedForComparison(clrType, (RowType)edmType.EdmType, memberPath);
					return;
				}
				goto IL_65;
			}
			return;
			IL_65:
			if (memberPath == null)
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedComparison(ExpressionConverter.DescribeClrType(clrType)));
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (EdmMember edmMember in memberPath)
			{
				stringBuilder.Append(Strings.ELinq_UnsupportedRowMemberComparison(edmMember.Name));
			}
			stringBuilder.Append(Strings.ELinq_UnsupportedRowTypeComparison(ExpressionConverter.DescribeClrType(clrType)));
			throw new NotSupportedException(Strings.ELinq_UnsupportedRowComparison(stringBuilder.ToString()));
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000D258 File Offset: 0x0000B458
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

		// Token: 0x0600023C RID: 572 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		internal static string DescribeClrType(Type clrType)
		{
			if (ExpressionConverter.IsCSharpGeneratedClass(clrType.Name, "DisplayClass") || ExpressionConverter.IsVBGeneratedClass(clrType.Name, "Closure"))
			{
				return Strings.ELinq_ClosureType;
			}
			if (ExpressionConverter.IsCSharpGeneratedClass(clrType.Name, "AnonymousType") || ExpressionConverter.IsVBGeneratedClass(clrType.Name, "AnonymousType"))
			{
				return Strings.ELinq_AnonymousType;
			}
			return clrType.FullName;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000D333 File Offset: 0x0000B533
		private static bool IsCSharpGeneratedClass(string typeName, string pattern)
		{
			return typeName.Contains("<>") && typeName.Contains("__") && typeName.Contains(pattern);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000D358 File Offset: 0x0000B558
		private static bool IsVBGeneratedClass(string typeName, string pattern)
		{
			return typeName.Contains("_") && typeName.Contains("$") && typeName.Contains(pattern);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D37D File Offset: 0x0000B57D
		private static DbExpression CreateIsNullExpression(DbExpression operand, Type operandClrType)
		{
			ExpressionConverter.VerifyTypeSupportedForComparison(operandClrType, operand.ResultType, null);
			return operand.IsNull();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000D394 File Offset: 0x0000B594
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
				throw new NotSupportedException(Strings.ELinq_UnsupportedRefComparison(refType.ElementType.FullName, refType2.ElementType.FullName));
			}
			return this.RecursivelyRewriteEqualsExpression(left, right, pattern);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000D444 File Offset: 0x0000B644
		private DbExpression RecursivelyRewriteEqualsExpression(DbExpression left, DbExpression right, ExpressionConverter.EqualsPattern pattern)
		{
			RowType rowType = left.ResultType.EdmType as RowType;
			RowType rowType2 = right.ResultType.EdmType as RowType;
			if (rowType != null || rowType2 != null)
			{
				if (rowType != null && rowType2 != null)
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
			else
			{
				if (!this._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior)
				{
					return this.ImplementEquality(left, right, pattern);
				}
				return this.ImplementEquality(left, right, ExpressionConverter.EqualsPattern.Store);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000D524 File Offset: 0x0000B724
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

		// Token: 0x06000243 RID: 579 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		private DbExpression ImplementEqualityConstantAndUnknown(DbConstantExpression constant, DbExpression unknown, ExpressionConverter.EqualsPattern pattern)
		{
			switch (pattern)
			{
			case ExpressionConverter.EqualsPattern.Store:
			case ExpressionConverter.EqualsPattern.PositiveNullEqualityNonComposable:
				return constant.Equal(unknown);
			case ExpressionConverter.EqualsPattern.PositiveNullEqualityComposable:
				if (!this._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior)
				{
					return constant.Equal(unknown);
				}
				return constant.Equal(unknown).And(unknown.IsNull().Not());
			default:
				return null;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000D62C File Offset: 0x0000B82C
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

		// Token: 0x06000245 RID: 581 RVA: 0x0000D718 File Offset: 0x0000B918
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

		// Token: 0x06000246 RID: 582 RVA: 0x0000D860 File Offset: 0x0000BA60
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
				throw new ProviderIncompatibleException(Strings.ProviderEscapeLikeArgumentReturnedNull);
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

		// Token: 0x06000247 RID: 583 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
		private DbFunctionExpression TranslateIntoCanonicalFunction(string functionName, Expression Expression, params Expression[] linqArguments)
		{
			DbExpression[] array = new DbExpression[linqArguments.Length];
			for (int i = 0; i < linqArguments.Length; i++)
			{
				array[i] = this.TranslateExpression(linqArguments[i]);
			}
			return this.CreateCanonicalFunction(functionName, Expression, array);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000D90C File Offset: 0x0000BB0C
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

		// Token: 0x06000249 RID: 585 RVA: 0x0000D959 File Offset: 0x0000BB59
		private EdmFunction FindCanonicalFunction(string functionName, IList<TypeUsage> argumentTypes, bool isGroupAggregateFunction, Expression Expression)
		{
			return this.FindFunction("Edm", functionName, argumentTypes, isGroupAggregateFunction, Expression);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000D96C File Offset: 0x0000BB6C
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

		// Token: 0x0600024B RID: 587 RVA: 0x0000D9B0 File Offset: 0x0000BBB0
		private static void ThrowUnresolvableFunction(Expression Expression)
		{
			if (Expression.NodeType == ExpressionType.Call)
			{
				MethodInfo method = ((MethodCallExpression)Expression).Method;
				throw new NotSupportedException(Strings.ELinq_UnresolvableFunctionForMethod(method, method.DeclaringType));
			}
			if (Expression.NodeType == ExpressionType.MemberAccess)
			{
				string text;
				Type type;
				MemberInfo memberInfo = TypeSystem.PropertyOrField(((MemberExpression)Expression).Member, out text, out type);
				throw new NotSupportedException(Strings.ELinq_UnresolvableFunctionForMember(memberInfo, memberInfo.DeclaringType));
			}
			throw new NotSupportedException(Strings.ELinq_UnresolvableFunctionForExpression(Expression.NodeType));
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000DA2C File Offset: 0x0000BC2C
		private static void ThrowUnresolvableFunctionOverload(Expression Expression, bool isAmbiguous)
		{
			if (Expression.NodeType == ExpressionType.Call)
			{
				MethodInfo method = ((MethodCallExpression)Expression).Method;
				if (isAmbiguous)
				{
					throw new NotSupportedException(Strings.ELinq_UnresolvableFunctionForMethodAmbiguousMatch(method, method.DeclaringType));
				}
				throw new NotSupportedException(Strings.ELinq_UnresolvableFunctionForMethodNotFound(method, method.DeclaringType));
			}
			else
			{
				if (Expression.NodeType == ExpressionType.MemberAccess)
				{
					string text;
					Type type;
					MemberInfo memberInfo = TypeSystem.PropertyOrField(((MemberExpression)Expression).Member, out text, out type);
					throw new NotSupportedException(Strings.ELinq_UnresolvableStoreFunctionForMember(memberInfo, memberInfo.DeclaringType));
				}
				throw new NotSupportedException(Strings.ELinq_UnresolvableStoreFunctionForExpression(Expression.NodeType));
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000DABC File Offset: 0x0000BCBC
		private static DbNewInstanceExpression CreateNewRowExpression(List<KeyValuePair<string, DbExpression>> columns, InitializerMetadata initializerMetadata)
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

		// Token: 0x040000E9 RID: 233
		private const string s_visualBasicAssemblyFullName = "Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x040000EA RID: 234
		internal const string KeyColumnName = "Key";

		// Token: 0x040000EB RID: 235
		internal const string GroupColumnName = "Group";

		// Token: 0x040000EC RID: 236
		internal const string EntityCollectionOwnerColumnName = "Owner";

		// Token: 0x040000ED RID: 237
		internal const string EntityCollectionElementsColumnName = "Elements";

		// Token: 0x040000EE RID: 238
		internal const string EdmNamespaceName = "Edm";

		// Token: 0x040000EF RID: 239
		private const string Concat = "Concat";

		// Token: 0x040000F0 RID: 240
		private const string IndexOf = "IndexOf";

		// Token: 0x040000F1 RID: 241
		private const string Length = "Length";

		// Token: 0x040000F2 RID: 242
		private const string Right = "Right";

		// Token: 0x040000F3 RID: 243
		private const string Substring = "Substring";

		// Token: 0x040000F4 RID: 244
		private const string ToUpper = "ToUpper";

		// Token: 0x040000F5 RID: 245
		private const string ToLower = "ToLower";

		// Token: 0x040000F6 RID: 246
		private const string Trim = "Trim";

		// Token: 0x040000F7 RID: 247
		private const string LTrim = "LTrim";

		// Token: 0x040000F8 RID: 248
		private const string RTrim = "RTrim";

		// Token: 0x040000F9 RID: 249
		private const string Reverse = "Reverse";

		// Token: 0x040000FA RID: 250
		private const string BitwiseAnd = "BitwiseAnd";

		// Token: 0x040000FB RID: 251
		private const string BitwiseOr = "BitwiseOr";

		// Token: 0x040000FC RID: 252
		private const string BitwiseNot = "BitwiseNot";

		// Token: 0x040000FD RID: 253
		private const string BitwiseXor = "BitwiseXor";

		// Token: 0x040000FE RID: 254
		private const string CurrentUtcDateTime = "CurrentUtcDateTime";

		// Token: 0x040000FF RID: 255
		private const string CurrentDateTimeOffset = "CurrentDateTimeOffset";

		// Token: 0x04000100 RID: 256
		private const string CurrentDateTime = "CurrentDateTime";

		// Token: 0x04000101 RID: 257
		private const string Year = "Year";

		// Token: 0x04000102 RID: 258
		private const string Month = "Month";

		// Token: 0x04000103 RID: 259
		private const string Day = "Day";

		// Token: 0x04000104 RID: 260
		private const string Hour = "Hour";

		// Token: 0x04000105 RID: 261
		private const string Minute = "Minute";

		// Token: 0x04000106 RID: 262
		private const string Second = "Second";

		// Token: 0x04000107 RID: 263
		private const string Millisecond = "Millisecond";

		// Token: 0x04000108 RID: 264
		private const string AsUnicode = "AsUnicode";

		// Token: 0x04000109 RID: 265
		private const string AsNonUnicode = "AsNonUnicode";

		// Token: 0x0400010A RID: 266
		private readonly Funcletizer _funcletizer;

		// Token: 0x0400010B RID: 267
		private readonly Perspective _perspective;

		// Token: 0x0400010C RID: 268
		private readonly Expression _expression;

		// Token: 0x0400010D RID: 269
		private readonly BindingContext _bindingContext;

		// Token: 0x0400010E RID: 270
		private Func<bool> _recompileRequired;

		// Token: 0x0400010F RID: 271
		private List<Tuple<ObjectParameter, QueryParameterExpression>> _parameters;

		// Token: 0x04000110 RID: 272
		private Dictionary<DbExpression, Span> _spanMappings;

		// Token: 0x04000111 RID: 273
		private MergeOption? _mergeOption;

		// Token: 0x04000112 RID: 274
		private Dictionary<Type, InitializerMetadata> _initializers;

		// Token: 0x04000113 RID: 275
		private Span _span;

		// Token: 0x04000114 RID: 276
		private HashSet<ObjectQuery> _inlineEntitySqlQueries;

		// Token: 0x04000115 RID: 277
		private int _ignoreInclude;

		// Token: 0x04000116 RID: 278
		private readonly AliasGenerator _aliasGenerator = new AliasGenerator("LQ", 0);

		// Token: 0x04000117 RID: 279
		private readonly ExpressionConverter.OrderByLifter _orderByLifter;

		// Token: 0x04000118 RID: 280
		private static readonly Dictionary<ExpressionType, ExpressionConverter.Translator> _translators = ExpressionConverter.InitializeTranslators();

		// Token: 0x02000030 RID: 48
		internal static class StringTranslatorUtil
		{
			// Token: 0x06000250 RID: 592 RVA: 0x0000DD10 File Offset: 0x0000BF10
			internal static IEnumerable<Expression> GetConcatArgs(Expression linq)
			{
				if (linq.IsStringAddExpression())
				{
					foreach (Expression arg in ExpressionConverter.StringTranslatorUtil.GetConcatArgs((BinaryExpression)linq))
					{
						yield return arg;
					}
				}
				else
				{
					yield return linq;
				}
				yield break;
			}

			// Token: 0x06000251 RID: 593 RVA: 0x0000DF88 File Offset: 0x0000C188
			internal static IEnumerable<Expression> GetConcatArgs(BinaryExpression linq)
			{
				foreach (Expression arg in ExpressionConverter.StringTranslatorUtil.GetConcatArgs(linq.Left))
				{
					yield return arg;
				}
				foreach (Expression arg2 in ExpressionConverter.StringTranslatorUtil.GetConcatArgs(linq.Right))
				{
					yield return arg2;
				}
				yield break;
			}

			// Token: 0x06000252 RID: 594 RVA: 0x0000DFA5 File Offset: 0x0000C1A5
			internal static DbExpression ConcatArgs(ExpressionConverter parent, BinaryExpression linq)
			{
				return ExpressionConverter.StringTranslatorUtil.ConcatArgs(parent, linq, ExpressionConverter.StringTranslatorUtil.GetConcatArgs(linq).ToArray<Expression>());
			}

			// Token: 0x06000253 RID: 595 RVA: 0x0000DFDC File Offset: 0x0000C1DC
			internal static DbExpression ConcatArgs(ExpressionConverter parent, Expression linq, Expression[] linqArgs)
			{
				DbExpression[] array = (from arg in linqArgs
				where !arg.IsNullConstant()
				select ExpressionConverter.StringTranslatorUtil.ConvertToString(parent, arg)).ToArray<DbExpression>();
				if (array.Length == 0)
				{
					return DbExpressionBuilder.Constant(string.Empty);
				}
				DbExpression dbExpression = array.First<DbExpression>();
				foreach (DbExpression dbExpression2 in array.Skip(1))
				{
					dbExpression = parent.CreateCanonicalFunction("Concat", linq, new DbExpression[]
					{
						dbExpression,
						dbExpression2
					});
				}
				return dbExpression;
			}

			// Token: 0x06000254 RID: 596 RVA: 0x0000E0B0 File Offset: 0x0000C2B0
			internal static DbExpression StripNull(Expression sourceExpression, DbExpression inputExpression, DbExpression outputExpression, bool useDatabaseNullSemantics)
			{
				if (sourceExpression.IsNullConstant())
				{
					return DbExpressionBuilder.Constant(string.Empty);
				}
				if (sourceExpression.NodeType == ExpressionType.Constant)
				{
					return outputExpression;
				}
				if (useDatabaseNullSemantics)
				{
					return outputExpression;
				}
				return DbExpressionBuilder.Case(new DbIsNullExpression[]
				{
					inputExpression.IsNull()
				}, new DbConstantExpression[]
				{
					DbExpressionBuilder.Constant(string.Empty)
				}, outputExpression);
			}

			// Token: 0x06000255 RID: 597 RVA: 0x0000E160 File Offset: 0x0000C360
			[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "the same linqExpression value is never cast to ConstantExpression twice")]
			internal static DbExpression ConvertToString(ExpressionConverter parent, Expression linqExpression)
			{
				if (linqExpression.Type == typeof(object))
				{
					ConstantExpression constantExpression = linqExpression as ConstantExpression;
					linqExpression = ((constantExpression != null) ? Expression.Constant(constantExpression.Value) : linqExpression.RemoveConvert());
				}
				DbExpression expression = parent.TranslateExpression(linqExpression);
				Type nonNullableType = TypeSystem.GetNonNullableType(linqExpression.Type);
				bool useDatabaseNullSemantics = !parent._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior;
				if (nonNullableType.IsEnum)
				{
					if (Attribute.IsDefined(nonNullableType, typeof(FlagsAttribute)))
					{
						throw new NotSupportedException(Strings.Elinq_ToStringNotSupportedForEnumsWithFlags);
					}
					if (linqExpression.IsNullConstant())
					{
						return DbExpressionBuilder.Constant(string.Empty);
					}
					if (linqExpression.NodeType == ExpressionType.Constant)
					{
						object value = ((ConstantExpression)linqExpression).Value;
						string value2 = Enum.GetName(nonNullableType, value) ?? value.ToString();
						return DbExpressionBuilder.Constant(value2);
					}
					Type integralType = nonNullableType.GetEnumUnderlyingType();
					TypeUsage type = parent.GetValueLayerType(integralType);
					IEnumerable<DbExpression> whenExpressions = (from object v in nonNullableType.GetEnumValues()
					select System.Convert.ChangeType(v, integralType, CultureInfo.InvariantCulture) into v
					select DbExpressionBuilder.Constant(v) into c
					select expression.CastTo(type).Equal(c)).Concat(new DbIsNullExpression[]
					{
						expression.CastTo(type).IsNull()
					});
					IEnumerable<DbConstantExpression> thenExpressions = (from s in nonNullableType.GetEnumNames()
					select DbExpressionBuilder.Constant(s)).Concat(new DbConstantExpression[]
					{
						DbExpressionBuilder.Constant(string.Empty)
					});
					UnaryExpression linq = Expression.Convert(linqExpression, integralType);
					DbCastExpression elseExpression = parent.TranslateExpression(linq).CastTo(parent.GetValueLayerType(typeof(string)));
					return DbExpressionBuilder.Case(whenExpressions, thenExpressions, elseExpression);
				}
				else
				{
					if (TypeSemantics.IsPrimitiveType(expression.ResultType, PrimitiveTypeKind.String))
					{
						return ExpressionConverter.StringTranslatorUtil.StripNull(linqExpression, expression, expression, useDatabaseNullSemantics);
					}
					if (TypeSemantics.IsPrimitiveType(expression.ResultType, PrimitiveTypeKind.Guid))
					{
						return ExpressionConverter.StringTranslatorUtil.StripNull(linqExpression, expression, expression.CastTo(parent.GetValueLayerType(typeof(string))).ToLower(), useDatabaseNullSemantics);
					}
					if (TypeSemantics.IsPrimitiveType(expression.ResultType, PrimitiveTypeKind.Boolean))
					{
						if (linqExpression.IsNullConstant())
						{
							return DbExpressionBuilder.Constant(string.Empty);
						}
						if (linqExpression.NodeType == ExpressionType.Constant)
						{
							string value3 = ((ConstantExpression)linqExpression).Value.ToString();
							return DbExpressionBuilder.Constant(value3);
						}
						DbComparisonExpression dbComparisonExpression = expression.Equal(DbExpressionBuilder.True);
						DbComparisonExpression dbComparisonExpression2 = expression.Equal(DbExpressionBuilder.False);
						DbConstantExpression dbConstantExpression = DbExpressionBuilder.Constant(true.ToString());
						DbConstantExpression dbConstantExpression2 = DbExpressionBuilder.Constant(false.ToString());
						return DbExpressionBuilder.Case(new DbComparisonExpression[]
						{
							dbComparisonExpression,
							dbComparisonExpression2
						}, new DbConstantExpression[]
						{
							dbConstantExpression,
							dbConstantExpression2
						}, DbExpressionBuilder.Constant(string.Empty));
					}
					else
					{
						if (!ExpressionConverter.StringTranslatorUtil.SupportsCastToString(expression.ResultType))
						{
							throw new NotSupportedException(Strings.Elinq_ToStringNotSupportedForType(expression.ResultType.EdmType.Name));
						}
						return ExpressionConverter.StringTranslatorUtil.StripNull(linqExpression, expression, expression.CastTo(parent.GetValueLayerType(typeof(string))), useDatabaseNullSemantics);
					}
				}
			}

			// Token: 0x06000256 RID: 598 RVA: 0x0000E518 File Offset: 0x0000C718
			internal static bool SupportsCastToString(TypeUsage typeUsage)
			{
				return TypeSemantics.IsPrimitiveType(typeUsage, PrimitiveTypeKind.String) || TypeSemantics.IsNumericType(typeUsage) || TypeSemantics.IsBooleanType(typeUsage) || TypeSemantics.IsPrimitiveType(typeUsage, PrimitiveTypeKind.DateTime) || TypeSemantics.IsPrimitiveType(typeUsage, PrimitiveTypeKind.DateTimeOffset) || TypeSemantics.IsPrimitiveType(typeUsage, PrimitiveTypeKind.Time) || TypeSemantics.IsPrimitiveType(typeUsage, PrimitiveTypeKind.Guid);
			}
		}

		// Token: 0x02000033 RID: 51
		private class ParameterReferenceRemover : DefaultExpressionVisitor
		{
			// Token: 0x060002D6 RID: 726 RVA: 0x0000FBB8 File Offset: 0x0000DDB8
			internal static DbExpression RemoveParameterReferences(DbExpression expression, ObjectParameterCollection availableParameters)
			{
				ExpressionConverter.ParameterReferenceRemover parameterReferenceRemover = new ExpressionConverter.ParameterReferenceRemover(availableParameters);
				return parameterReferenceRemover.VisitExpression(expression);
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x0000FBD3 File Offset: 0x0000DDD3
			private ParameterReferenceRemover(ObjectParameterCollection availableParams)
			{
				this.objectParameters = availableParams;
			}

			// Token: 0x060002D8 RID: 728 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
			public override DbExpression Visit(DbParameterReferenceExpression expression)
			{
				Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
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

			// Token: 0x04000120 RID: 288
			private readonly ObjectParameterCollection objectParameters;
		}

		// Token: 0x02000034 RID: 52
		private enum EqualsPattern
		{
			// Token: 0x04000122 RID: 290
			Store,
			// Token: 0x04000123 RID: 291
			PositiveNullEqualityNonComposable,
			// Token: 0x04000124 RID: 292
			PositiveNullEqualityComposable
		}

		// Token: 0x02000035 RID: 53
		internal abstract class Translator
		{
			// Token: 0x060002D9 RID: 729 RVA: 0x0000FC49 File Offset: 0x0000DE49
			protected Translator(params ExpressionType[] nodeTypes)
			{
				this._nodeTypes = nodeTypes;
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060002DA RID: 730 RVA: 0x0000FC58 File Offset: 0x0000DE58
			internal IEnumerable<ExpressionType> NodeTypes
			{
				get
				{
					return this._nodeTypes;
				}
			}

			// Token: 0x060002DB RID: 731
			internal abstract DbExpression Translate(ExpressionConverter parent, Expression linq);

			// Token: 0x060002DC RID: 732 RVA: 0x0000FC60 File Offset: 0x0000DE60
			public override string ToString()
			{
				return base.GetType().Name;
			}

			// Token: 0x04000125 RID: 293
			private readonly ExpressionType[] _nodeTypes;
		}

		// Token: 0x02000036 RID: 54
		internal abstract class TypedTranslator<T_Linq> : ExpressionConverter.Translator where T_Linq : Expression
		{
			// Token: 0x060002DD RID: 733 RVA: 0x0000FC6D File Offset: 0x0000DE6D
			protected TypedTranslator(params ExpressionType[] nodeTypes) : base(nodeTypes)
			{
			}

			// Token: 0x060002DE RID: 734 RVA: 0x0000FC76 File Offset: 0x0000DE76
			internal override DbExpression Translate(ExpressionConverter parent, Expression linq)
			{
				return this.TypedTranslate(parent, (T_Linq)((object)linq));
			}

			// Token: 0x060002DF RID: 735
			protected abstract DbExpression TypedTranslate(ExpressionConverter parent, T_Linq linq);
		}

		// Token: 0x02000037 RID: 55
		internal sealed class MethodCallTranslator : ExpressionConverter.TypedTranslator<MethodCallExpression>
		{
			// Token: 0x060002E0 RID: 736 RVA: 0x0000FC88 File Offset: 0x0000DE88
			internal MethodCallTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Call
			})
			{
			}

			// Token: 0x060002E1 RID: 737 RVA: 0x0000FCA8 File Offset: 0x0000DEA8
			protected override DbExpression TypedTranslate(ExpressionConverter parent, MethodCallExpression linq)
			{
				SequenceMethod sequenceMethod;
				ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator sequenceMethodTranslator;
				if (ReflectionUtil.TryIdentifySequenceMethod(linq.Method, out sequenceMethod) && ExpressionConverter.MethodCallTranslator._sequenceTranslators.TryGetValue(sequenceMethod, out sequenceMethodTranslator))
				{
					return sequenceMethodTranslator.Translate(parent, linq, sequenceMethod);
				}
				ExpressionConverter.MethodCallTranslator.CallTranslator callTranslator;
				if (ExpressionConverter.MethodCallTranslator.TryGetCallTranslator(linq.Method, out callTranslator))
				{
					return callTranslator.Translate(parent, linq);
				}
				ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator objectQueryCallTranslator;
				if (ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator.IsCandidateMethod(linq.Method) && ExpressionConverter.MethodCallTranslator._objectQueryTranslators.TryGetValue(linq.Method.Name, out objectQueryCallTranslator))
				{
					return objectQueryCallTranslator.Translate(parent, linq);
				}
				DbFunctionAttribute dbFunctionAttribute = linq.Method.GetCustomAttributes(false).FirstOrDefault<DbFunctionAttribute>();
				if (dbFunctionAttribute != null)
				{
					return ExpressionConverter.MethodCallTranslator._functionCallTranslator.TranslateFunctionCall(parent, linq, dbFunctionAttribute);
				}
				string name;
				Type[] array;
				if ((name = linq.Method.Name) != null && name == "Contains" && linq.Method.GetParameters().Count<ParameterInfo>() == 1 && linq.Method.ReturnType.Equals(typeof(bool)) && linq.Method.IsImplementationOfGenericInterfaceMethod(typeof(ICollection<>), out array))
				{
					return ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContains(parent, linq.Object, linq.Arguments[0]);
				}
				return ExpressionConverter.MethodCallTranslator._defaultTranslator.Translate(parent, linq);
			}

			// Token: 0x060002E2 RID: 738 RVA: 0x0000FDD8 File Offset: 0x0000DFD8
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

			// Token: 0x060002E3 RID: 739 RVA: 0x0000FE68 File Offset: 0x0000E068
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

			// Token: 0x060002E4 RID: 740 RVA: 0x0000FEF8 File Offset: 0x0000E0F8
			private static Dictionary<string, ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator> InitializeObjectQueryTranslators()
			{
				Dictionary<string, ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator> dictionary = new Dictionary<string, ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator>(StringComparer.Ordinal);
				foreach (ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator objectQueryCallTranslator in ExpressionConverter.MethodCallTranslator.GetObjectQueryCallTranslators())
				{
					dictionary[objectQueryCallTranslator.MethodName] = objectQueryCallTranslator;
				}
				return dictionary;
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x0000FF58 File Offset: 0x0000E158
			private static bool TryGetCallTranslator(MethodInfo methodInfo, out ExpressionConverter.MethodCallTranslator.CallTranslator callTranslator)
			{
				if (ExpressionConverter.MethodCallTranslator._methodTranslators.TryGetValue(methodInfo, out callTranslator))
				{
					return true;
				}
				if ("Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" == methodInfo.DeclaringType.Assembly().FullName)
				{
					lock (ExpressionConverter.MethodCallTranslator._vbInitializerLock)
					{
						if (!ExpressionConverter.MethodCallTranslator.s_vbMethodsInitialized)
						{
							ExpressionConverter.MethodCallTranslator.InitializeVBMethods(methodInfo.DeclaringType.Assembly());
							ExpressionConverter.MethodCallTranslator.s_vbMethodsInitialized = true;
						}
						return ExpressionConverter.MethodCallTranslator._methodTranslators.TryGetValue(methodInfo, out callTranslator);
					}
				}
				callTranslator = null;
				return false;
			}

			// Token: 0x060002E6 RID: 742 RVA: 0x0000FFF0 File Offset: 0x0000E1F0
			private static void InitializeVBMethods(Assembly vbAssembly)
			{
				foreach (ExpressionConverter.MethodCallTranslator.CallTranslator callTranslator in ExpressionConverter.MethodCallTranslator.GetVisualBasicCallTranslators(vbAssembly))
				{
					foreach (MethodInfo key in callTranslator.Methods)
					{
						ExpressionConverter.MethodCallTranslator._methodTranslators.Add(key, callTranslator);
					}
				}
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x00010190 File Offset: 0x0000E390
			private static IEnumerable<ExpressionConverter.MethodCallTranslator.CallTranslator> GetVisualBasicCallTranslators(Assembly vbAssembly)
			{
				yield return new ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionDefaultTranslator(vbAssembly);
				yield return new ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator(vbAssembly);
				yield return new ExpressionConverter.MethodCallTranslator.VBDatePartTranslator(vbAssembly);
				yield break;
			}

			// Token: 0x060002E8 RID: 744 RVA: 0x000101B0 File Offset: 0x0000E3B0
			private static IEnumerable<ExpressionConverter.MethodCallTranslator.CallTranslator> GetCallTranslators()
			{
				return new ExpressionConverter.MethodCallTranslator.CallTranslator[]
				{
					new ExpressionConverter.MethodCallTranslator.CanonicalFunctionDefaultTranslator(),
					new ExpressionConverter.MethodCallTranslator.AsUnicodeFunctionTranslator(),
					new ExpressionConverter.MethodCallTranslator.AsNonUnicodeFunctionTranslator(),
					new ExpressionConverter.MethodCallTranslator.MathTruncateTranslator(),
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
					new ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator(),
					new ExpressionConverter.MethodCallTranslator.HasFlagTranslator(),
					new ExpressionConverter.MethodCallTranslator.ToStringTranslator()
				};
			}

			// Token: 0x060002E9 RID: 745 RVA: 0x000107F4 File Offset: 0x0000E9F4
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

			// Token: 0x060002EA RID: 746 RVA: 0x000109E4 File Offset: 0x0000EBE4
			private static IEnumerable<ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator> GetObjectQueryCallTranslators()
			{
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderDistinctTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderExceptTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderFirstTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderToListTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryIncludeTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderIntersectTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderOfTypeTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderUnionTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryMergeAsTranslator();
				yield return new ExpressionConverter.MethodCallTranslator.ObjectQueryIncludeSpanTranslator();
				yield break;
			}

			// Token: 0x060002EB RID: 747 RVA: 0x000109FC File Offset: 0x0000EBFC
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

			// Token: 0x04000126 RID: 294
			private const string s_stringsTypeFullName = "Microsoft.VisualBasic.Strings";

			// Token: 0x04000127 RID: 295
			private static readonly ExpressionConverter.MethodCallTranslator.CallTranslator _defaultTranslator = new ExpressionConverter.MethodCallTranslator.DefaultTranslator();

			// Token: 0x04000128 RID: 296
			private static readonly ExpressionConverter.MethodCallTranslator.FunctionCallTranslator _functionCallTranslator = new ExpressionConverter.MethodCallTranslator.FunctionCallTranslator();

			// Token: 0x04000129 RID: 297
			private static readonly Dictionary<MethodInfo, ExpressionConverter.MethodCallTranslator.CallTranslator> _methodTranslators = ExpressionConverter.MethodCallTranslator.InitializeMethodTranslators();

			// Token: 0x0400012A RID: 298
			private static readonly Dictionary<SequenceMethod, ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator> _sequenceTranslators = ExpressionConverter.MethodCallTranslator.InitializeSequenceMethodTranslators();

			// Token: 0x0400012B RID: 299
			private static readonly Dictionary<string, ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator> _objectQueryTranslators = ExpressionConverter.MethodCallTranslator.InitializeObjectQueryTranslators();

			// Token: 0x0400012C RID: 300
			private static bool s_vbMethodsInitialized;

			// Token: 0x0400012D RID: 301
			private static readonly object _vbInitializerLock = new object();

			// Token: 0x02000038 RID: 56
			internal abstract class CallTranslator
			{
				// Token: 0x060002ED RID: 749 RVA: 0x00010B01 File Offset: 0x0000ED01
				protected CallTranslator(params MethodInfo[] methods)
				{
					this._methods = methods;
				}

				// Token: 0x060002EE RID: 750 RVA: 0x00010B10 File Offset: 0x0000ED10
				protected CallTranslator(IEnumerable<MethodInfo> methods)
				{
					this._methods = methods;
				}

				// Token: 0x1700002D RID: 45
				// (get) Token: 0x060002EF RID: 751 RVA: 0x00010B1F File Offset: 0x0000ED1F
				internal IEnumerable<MethodInfo> Methods
				{
					get
					{
						return this._methods;
					}
				}

				// Token: 0x060002F0 RID: 752
				internal abstract DbExpression Translate(ExpressionConverter parent, MethodCallExpression call);

				// Token: 0x060002F1 RID: 753 RVA: 0x00010B27 File Offset: 0x0000ED27
				public override string ToString()
				{
					return base.GetType().Name;
				}

				// Token: 0x0400012E RID: 302
				private readonly IEnumerable<MethodInfo> _methods;
			}

			// Token: 0x02000039 RID: 57
			private abstract class ObjectQueryCallTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060002F2 RID: 754 RVA: 0x00010B34 File Offset: 0x0000ED34
				internal static bool IsCandidateMethod(MethodInfo method)
				{
					Type declaringType = method.DeclaringType;
					return (method.IsPublic || (method.IsAssembly && (method.Name == "MergeAs" || method.Name == "IncludeSpan"))) && null != declaringType && declaringType.IsGenericType() && typeof(ObjectQuery<>) == declaringType.GetGenericTypeDefinition();
				}

				// Token: 0x060002F3 RID: 755 RVA: 0x00010BA4 File Offset: 0x0000EDA4
				internal static Expression RemoveConvertToObjectQuery(Expression queryExpression)
				{
					if (queryExpression.NodeType == ExpressionType.Convert)
					{
						UnaryExpression unaryExpression = (UnaryExpression)queryExpression;
						Type type = unaryExpression.Operand.Type;
						if (type.IsGenericType() && (typeof(IQueryable<>) == type.GetGenericTypeDefinition() || typeof(IOrderedQueryable<>) == type.GetGenericTypeDefinition()))
						{
							queryExpression = unaryExpression.Operand;
						}
					}
					return queryExpression;
				}

				// Token: 0x060002F4 RID: 756 RVA: 0x00010C0D File Offset: 0x0000EE0D
				protected ObjectQueryCallTranslator(string methodName) : base(new MethodInfo[0])
				{
					this._methodName = methodName;
				}

				// Token: 0x1700002E RID: 46
				// (get) Token: 0x060002F5 RID: 757 RVA: 0x00010C22 File Offset: 0x0000EE22
				internal string MethodName
				{
					get
					{
						return this._methodName;
					}
				}

				// Token: 0x0400012F RID: 303
				private readonly string _methodName;
			}

			// Token: 0x0200003A RID: 58
			private abstract class ObjectQueryBuilderCallTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator
			{
				// Token: 0x060002F6 RID: 758 RVA: 0x00010C2A File Offset: 0x0000EE2A
				protected ObjectQueryBuilderCallTranslator(string methodName, SequenceMethod sequenceEquivalent) : base(methodName)
				{
					ExpressionConverter.MethodCallTranslator._sequenceTranslators.TryGetValue(sequenceEquivalent, out this._translator);
				}

				// Token: 0x060002F7 RID: 759 RVA: 0x00010C45 File Offset: 0x0000EE45
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return this._translator.Translate(parent, call);
				}

				// Token: 0x04000130 RID: 304
				private readonly ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator _translator;
			}

			// Token: 0x0200003B RID: 59
			private sealed class ObjectQueryBuilderUnionTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x060002F8 RID: 760 RVA: 0x00010C54 File Offset: 0x0000EE54
				internal ObjectQueryBuilderUnionTranslator() : base("Union", SequenceMethod.Union)
				{
				}
			}

			// Token: 0x0200003C RID: 60
			private sealed class ObjectQueryBuilderIntersectTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x060002F9 RID: 761 RVA: 0x00010C63 File Offset: 0x0000EE63
				internal ObjectQueryBuilderIntersectTranslator() : base("Intersect", SequenceMethod.Intersect)
				{
				}
			}

			// Token: 0x0200003D RID: 61
			private sealed class ObjectQueryBuilderExceptTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x060002FA RID: 762 RVA: 0x00010C72 File Offset: 0x0000EE72
				internal ObjectQueryBuilderExceptTranslator() : base("Except", SequenceMethod.Except)
				{
				}
			}

			// Token: 0x0200003E RID: 62
			private sealed class ObjectQueryBuilderDistinctTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x060002FB RID: 763 RVA: 0x00010C81 File Offset: 0x0000EE81
				internal ObjectQueryBuilderDistinctTranslator() : base("Distinct", SequenceMethod.Distinct)
				{
				}
			}

			// Token: 0x0200003F RID: 63
			private sealed class ObjectQueryBuilderOfTypeTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x060002FC RID: 764 RVA: 0x00010C90 File Offset: 0x0000EE90
				internal ObjectQueryBuilderOfTypeTranslator() : base("OfType", SequenceMethod.OfType)
				{
				}
			}

			// Token: 0x02000040 RID: 64
			private sealed class ObjectQueryBuilderFirstTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x060002FD RID: 765 RVA: 0x00010C9E File Offset: 0x0000EE9E
				internal ObjectQueryBuilderFirstTranslator() : base("First", SequenceMethod.First)
				{
				}
			}

			// Token: 0x02000041 RID: 65
			private sealed class ObjectQueryBuilderToListTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryBuilderCallTranslator
			{
				// Token: 0x060002FE RID: 766 RVA: 0x00010CAD File Offset: 0x0000EEAD
				internal ObjectQueryBuilderToListTranslator() : base("ToList", SequenceMethod.ToList)
				{
				}
			}

			// Token: 0x02000042 RID: 66
			private sealed class ObjectQueryIncludeTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator
			{
				// Token: 0x060002FF RID: 767 RVA: 0x00010CBF File Offset: 0x0000EEBF
				internal ObjectQueryIncludeTranslator() : base("Include")
				{
				}

				// Token: 0x06000300 RID: 768 RVA: 0x00010CCC File Offset: 0x0000EECC
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
					throw new NotSupportedException(Strings.ELinq_UnsupportedInclude);
				}
			}

			// Token: 0x02000043 RID: 67
			private sealed class ObjectQueryMergeAsTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator
			{
				// Token: 0x06000301 RID: 769 RVA: 0x00010D47 File Offset: 0x0000EF47
				internal ObjectQueryMergeAsTranslator() : base("MergeAs")
				{
				}

				// Token: 0x06000302 RID: 770 RVA: 0x00010D54 File Offset: 0x0000EF54
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (call.Arguments[0].NodeType != ExpressionType.Constant)
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedMergeAs);
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

			// Token: 0x02000044 RID: 68
			private sealed class ObjectQueryIncludeSpanTranslator : ExpressionConverter.MethodCallTranslator.ObjectQueryCallTranslator
			{
				// Token: 0x06000303 RID: 771 RVA: 0x00010DD3 File Offset: 0x0000EFD3
				internal ObjectQueryIncludeSpanTranslator() : base("IncludeSpan")
				{
				}

				// Token: 0x06000304 RID: 772 RVA: 0x00010DE0 File Offset: 0x0000EFE0
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

			// Token: 0x02000045 RID: 69
			internal sealed class DefaultTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06000305 RID: 773 RVA: 0x00010E38 File Offset: 0x0000F038
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					MethodInfo method = call.Method;
					if (method.DeclaringType.Assembly().FullName == "Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" && method.Name == "Mid")
					{
						if (new Type[]
						{
							typeof(string),
							typeof(int)
						}.SequenceEqual(from p in method.GetParameters()
						select p.ParameterType))
						{
							throw new NotSupportedException(Strings.ELinq_UnsupportedMethodSuggestedAlternative(method, "System.String Mid(System.String, Int32, Int32)"));
						}
					}
					throw new NotSupportedException(Strings.ELinq_UnsupportedMethod(method));
				}

				// Token: 0x06000306 RID: 774 RVA: 0x00010EE7 File Offset: 0x0000F0E7
				public DefaultTranslator() : base(new MethodInfo[0])
				{
				}
			}

			// Token: 0x02000046 RID: 70
			private sealed class FunctionCallTranslator
			{
				// Token: 0x06000308 RID: 776 RVA: 0x00010F30 File Offset: 0x0000F130
				internal DbExpression TranslateFunctionCall(ExpressionConverter parent, MethodCallExpression call, DbFunctionAttribute functionAttribute)
				{
					List<DbExpression> list = (from a in call.Arguments
					select this.UnwrapNoOpConverts(a) into b
					select this.NormalizeAllSetSources(parent, parent.TranslateExpression(b))).ToList<DbExpression>();
					List<TypeUsage> argumentTypes = (from a in list
					select a.ResultType).ToList<TypeUsage>();
					EdmFunction edmFunction = parent.FindFunction(functionAttribute.NamespaceName, functionAttribute.FunctionName, argumentTypes, false, call);
					if (!edmFunction.IsComposableAttribute)
					{
						throw new NotSupportedException(Strings.CannotCallNoncomposableFunction(edmFunction.FullName));
					}
					DbExpression dbExpression = edmFunction.Invoke(list);
					return this.ValidateReturnType(dbExpression, dbExpression.ResultType, parent, call, call.Type, false);
				}

				// Token: 0x06000309 RID: 777 RVA: 0x00011008 File Offset: 0x0000F208
				private DbExpression NormalizeAllSetSources(ExpressionConverter parent, DbExpression argumentExpr)
				{
					DbExpression dbExpression = null;
					BuiltInTypeKind builtInTypeKind = argumentExpr.ResultType.EdmType.BuiltInTypeKind;
					BuiltInTypeKind builtInTypeKind2 = builtInTypeKind;
					if (builtInTypeKind2 != BuiltInTypeKind.CollectionType)
					{
						if (builtInTypeKind2 == BuiltInTypeKind.RowType)
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

				// Token: 0x0600030A RID: 778 RVA: 0x0001114C File Offset: 0x0000F34C
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

				// Token: 0x0600030B RID: 779 RVA: 0x00011190 File Offset: 0x0000F390
				private DbExpression ValidateReturnType(DbExpression result, TypeUsage actualReturnType, ExpressionConverter parent, MethodCallExpression call, Type clrReturnType, bool isElementOfCollection)
				{
					BuiltInTypeKind builtInTypeKind = actualReturnType.EdmType.BuiltInTypeKind;
					BuiltInTypeKind builtInTypeKind2 = builtInTypeKind;
					if (builtInTypeKind2 != BuiltInTypeKind.CollectionType)
					{
						if (builtInTypeKind2 != BuiltInTypeKind.RefType)
						{
							if (builtInTypeKind2 != BuiltInTypeKind.RowType)
							{
								if (isElementOfCollection)
								{
									TypeUsage castTargetType = parent.GetCastTargetType(actualReturnType, clrReturnType, null, false);
									if (castTargetType != null)
									{
										throw new NotSupportedException(Strings.ELinq_DbFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
									}
								}
								TypeUsage valueLayerType = parent.GetValueLayerType(clrReturnType);
								if (!TypeSemantics.IsPromotableTo(actualReturnType, valueLayerType))
								{
									throw new NotSupportedException(Strings.ELinq_DbFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
								}
								if (!isElementOfCollection)
								{
									result = parent.AlignTypes(result, clrReturnType);
								}
							}
							else if (clrReturnType != typeof(DbDataRecord))
							{
								throw new NotSupportedException(Strings.ELinq_DbFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
							}
						}
						else if (clrReturnType != typeof(EntityKey))
						{
							throw new NotSupportedException(Strings.ELinq_DbFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
						}
					}
					else
					{
						if (!clrReturnType.IsGenericType())
						{
							throw new NotSupportedException(Strings.ELinq_DbFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
						}
						Type genericTypeDefinition = clrReturnType.GetGenericTypeDefinition();
						if (genericTypeDefinition != typeof(IEnumerable<>) && genericTypeDefinition != typeof(IQueryable<>))
						{
							throw new NotSupportedException(Strings.ELinq_DbFunctionAttributedFunctionWithWrongReturnType(call.Method, call.Method.DeclaringType));
						}
						Type clrReturnType2 = clrReturnType.GetGenericArguments()[0];
						result = this.ValidateReturnType(result, TypeHelpers.GetElementTypeUsage(actualReturnType), parent, call, clrReturnType2, true);
					}
					return result;
				}
			}

			// Token: 0x02000047 RID: 71
			internal sealed class CanonicalFunctionDefaultTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x0600030F RID: 783 RVA: 0x0001133F File Offset: 0x0000F53F
				internal CanonicalFunctionDefaultTranslator() : base(ExpressionConverter.MethodCallTranslator.CanonicalFunctionDefaultTranslator.GetMethods())
				{
				}

				// Token: 0x06000310 RID: 784 RVA: 0x0001137C File Offset: 0x0000F57C
				private static IEnumerable<MethodInfo> GetMethods()
				{
					List<MethodInfo> list = new List<MethodInfo>
					{
						typeof(Math).GetDeclaredMethod("Ceiling", new Type[]
						{
							typeof(decimal)
						}),
						typeof(Math).GetDeclaredMethod("Ceiling", new Type[]
						{
							typeof(double)
						}),
						typeof(Math).GetDeclaredMethod("Floor", new Type[]
						{
							typeof(decimal)
						}),
						typeof(Math).GetDeclaredMethod("Floor", new Type[]
						{
							typeof(double)
						}),
						typeof(Math).GetDeclaredMethod("Round", new Type[]
						{
							typeof(decimal)
						}),
						typeof(Math).GetDeclaredMethod("Round", new Type[]
						{
							typeof(double)
						}),
						typeof(Math).GetDeclaredMethod("Round", new Type[]
						{
							typeof(decimal),
							typeof(int)
						}),
						typeof(Math).GetDeclaredMethod("Round", new Type[]
						{
							typeof(double),
							typeof(int)
						}),
						typeof(decimal).GetDeclaredMethod("Floor", new Type[]
						{
							typeof(decimal)
						}),
						typeof(decimal).GetDeclaredMethod("Ceiling", new Type[]
						{
							typeof(decimal)
						}),
						typeof(decimal).GetDeclaredMethod("Round", new Type[]
						{
							typeof(decimal)
						}),
						typeof(decimal).GetDeclaredMethod("Round", new Type[]
						{
							typeof(decimal),
							typeof(int)
						}),
						typeof(string).GetDeclaredMethod("Replace", new Type[]
						{
							typeof(string),
							typeof(string)
						}),
						typeof(string).GetDeclaredMethod("ToLower", new Type[0]),
						typeof(string).GetDeclaredMethod("ToUpper", new Type[0]),
						typeof(string).GetDeclaredMethod("Trim", new Type[0])
					};
					list.AddRange(from a in new Type[]
					{
						typeof(decimal),
						typeof(double),
						typeof(float),
						typeof(int),
						typeof(long),
						typeof(sbyte),
						typeof(short)
					}
					select typeof(Math).GetDeclaredMethod("Abs", new Type[]
					{
						a
					}));
					return list;
				}

				// Token: 0x06000311 RID: 785 RVA: 0x00011744 File Offset: 0x0000F944
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

			// Token: 0x02000048 RID: 72
			internal abstract class AsUnicodeNonUnicodeBaseFunctionTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06000313 RID: 787 RVA: 0x000117B1 File Offset: 0x0000F9B1
				protected AsUnicodeNonUnicodeBaseFunctionTranslator(IEnumerable<MethodInfo> methods, bool isUnicode) : base(methods)
				{
					this._isUnicode = isUnicode;
				}

				// Token: 0x06000314 RID: 788 RVA: 0x000117C4 File Offset: 0x0000F9C4
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
								throw new NotSupportedException(Strings.ELinq_UnsupportedAsUnicodeAndAsNonUnicode(call.Method));
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

				// Token: 0x04000134 RID: 308
				private readonly bool _isUnicode;
			}

			// Token: 0x02000049 RID: 73
			internal sealed class AsUnicodeFunctionTranslator : ExpressionConverter.MethodCallTranslator.AsUnicodeNonUnicodeBaseFunctionTranslator
			{
				// Token: 0x06000315 RID: 789 RVA: 0x0001186B File Offset: 0x0000FA6B
				internal AsUnicodeFunctionTranslator() : base(ExpressionConverter.MethodCallTranslator.AsUnicodeFunctionTranslator.GetMethods(), true)
				{
				}

				// Token: 0x06000316 RID: 790 RVA: 0x000119A0 File Offset: 0x0000FBA0
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(DbFunctions).GetDeclaredMethod("AsUnicode", new Type[]
					{
						typeof(string)
					});
					yield return typeof(EntityFunctions).GetDeclaredMethod("AsUnicode", new Type[]
					{
						typeof(string)
					});
					yield break;
				}
			}

			// Token: 0x0200004A RID: 74
			internal sealed class AsNonUnicodeFunctionTranslator : ExpressionConverter.MethodCallTranslator.AsUnicodeNonUnicodeBaseFunctionTranslator
			{
				// Token: 0x06000317 RID: 791 RVA: 0x000119B6 File Offset: 0x0000FBB6
				internal AsNonUnicodeFunctionTranslator() : base(ExpressionConverter.MethodCallTranslator.AsNonUnicodeFunctionTranslator.GetMethods(), false)
				{
				}

				// Token: 0x06000318 RID: 792 RVA: 0x00011AE8 File Offset: 0x0000FCE8
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(DbFunctions).GetDeclaredMethod("AsNonUnicode", new Type[]
					{
						typeof(string)
					});
					yield return typeof(EntityFunctions).GetDeclaredMethod("AsNonUnicode", new Type[]
					{
						typeof(string)
					});
					yield break;
				}
			}

			// Token: 0x0200004B RID: 75
			internal sealed class HasFlagTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06000319 RID: 793 RVA: 0x00011B00 File Offset: 0x0000FD00
				internal HasFlagTranslator() : base(new MethodInfo[]
				{
					ExpressionConverter.MethodCallTranslator.HasFlagTranslator._hasFlagMethod
				})
				{
				}

				// Token: 0x0600031A RID: 794 RVA: 0x00011B24 File Offset: 0x0000FD24
				[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Scope = "member", Justification = "The argument name passed to ArgumentNullException matches the name of the argument of the HasFlag method being translated.")]
				private static DbExpression TranslateHasFlag(ExpressionConverter parent, Expression sourceExpression, Expression valueExpression)
				{
					if (valueExpression.NodeType == ExpressionType.Constant && ((ConstantExpression)valueExpression).Value == null)
					{
						throw new ArgumentNullException("flag");
					}
					DbExpression dbExpression = parent.TranslateExpression(valueExpression);
					DbExpression dbExpression2 = parent.TranslateExpression(sourceExpression);
					if (dbExpression2.ResultType.EdmType != dbExpression.ResultType.EdmType)
					{
						throw new NotSupportedException(Strings.ELinq_HasFlagArgumentAndSourceTypeMismatch(dbExpression.ResultType.EdmType.Name, dbExpression2.ResultType.EdmType.Name));
					}
					TypeUsage toType = TypeHelpers.CreateEnumUnderlyingTypeUsage(dbExpression2.ResultType);
					DbCastExpression dbCastExpression = dbExpression.CastTo(toType);
					return dbExpression2.CastTo(toType).BitwiseAnd(dbCastExpression).Equal(dbCastExpression);
				}

				// Token: 0x0600031B RID: 795 RVA: 0x00011BCD File Offset: 0x0000FDCD
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return ExpressionConverter.MethodCallTranslator.HasFlagTranslator.TranslateHasFlag(parent, call.Object, call.Arguments[0]);
				}

				// Token: 0x04000135 RID: 309
				private static readonly MethodInfo _hasFlagMethod = typeof(Enum).GetDeclaredMethod("HasFlag", new Type[]
				{
					typeof(Enum)
				});
			}

			// Token: 0x0200004C RID: 76
			internal sealed class MathTruncateTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x0600031D RID: 797 RVA: 0x00011C24 File Offset: 0x0000FE24
				internal MathTruncateTranslator() : base(new MethodInfo[]
				{
					typeof(Math).GetDeclaredMethod("Truncate", new Type[]
					{
						typeof(decimal)
					}),
					typeof(Math).GetDeclaredMethod("Truncate", new Type[]
					{
						typeof(double)
					})
				})
				{
				}

				// Token: 0x0600031E RID: 798 RVA: 0x00011C98 File Offset: 0x0000FE98
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression value = parent.TranslateExpression(call.Arguments[0]);
					DbConstantExpression digits = DbExpressionBuilder.Constant(0);
					return value.Truncate(digits);
				}
			}

			// Token: 0x0200004D RID: 77
			internal sealed class MathPowerTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x0600031F RID: 799 RVA: 0x00011CCC File Offset: 0x0000FECC
				internal MathPowerTranslator() : base(new MethodInfo[]
				{
					typeof(Math).GetDeclaredMethod("Pow", new Type[]
					{
						typeof(double),
						typeof(double)
					})
				})
				{
				}

				// Token: 0x06000320 RID: 800 RVA: 0x00011D20 File Offset: 0x0000FF20
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression baseArgument = parent.TranslateExpression(call.Arguments[0]);
					DbExpression exponent = parent.TranslateExpression(call.Arguments[1]);
					return baseArgument.Power(exponent);
				}
			}

			// Token: 0x0200004E RID: 78
			internal sealed class GuidNewGuidTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06000321 RID: 801 RVA: 0x00011D5C File Offset: 0x0000FF5C
				internal GuidNewGuidTranslator() : base(new MethodInfo[]
				{
					typeof(Guid).GetDeclaredMethod("NewGuid", new Type[0])
				})
				{
				}

				// Token: 0x06000322 RID: 802 RVA: 0x00011D94 File Offset: 0x0000FF94
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return EdmFunctions.NewGuid();
				}
			}

			// Token: 0x0200004F RID: 79
			internal sealed class StringContainsTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06000323 RID: 803 RVA: 0x00011D9B File Offset: 0x0000FF9B
				internal StringContainsTranslator() : base(ExpressionConverter.MethodCallTranslator.StringContainsTranslator.GetMethods())
				{
				}

				// Token: 0x06000324 RID: 804 RVA: 0x00011E88 File Offset: 0x00010088
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("Contains", new Type[]
					{
						typeof(string)
					});
					yield break;
				}

				// Token: 0x06000325 RID: 805 RVA: 0x00011E9E File Offset: 0x0001009E
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateFunctionIntoLike(call, true, true, new Func<ExpressionConverter, MethodCallExpression, DbExpression, DbExpression, DbExpression>(ExpressionConverter.MethodCallTranslator.StringContainsTranslator.CreateDefaultTranslation));
				}

				// Token: 0x06000326 RID: 806 RVA: 0x00011EB8 File Offset: 0x000100B8
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

			// Token: 0x02000050 RID: 80
			internal sealed class IndexOfTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06000327 RID: 807 RVA: 0x00011EF5 File Offset: 0x000100F5
				internal IndexOfTranslator() : base(ExpressionConverter.MethodCallTranslator.IndexOfTranslator.GetMethods())
				{
				}

				// Token: 0x06000328 RID: 808 RVA: 0x00011FE4 File Offset: 0x000101E4
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("IndexOf", new Type[]
					{
						typeof(string)
					});
					yield break;
				}

				// Token: 0x06000329 RID: 809 RVA: 0x00011FFC File Offset: 0x000101FC
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

			// Token: 0x02000051 RID: 81
			internal sealed class StartsWithTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x0600032A RID: 810 RVA: 0x00012049 File Offset: 0x00010249
				internal StartsWithTranslator() : base(ExpressionConverter.MethodCallTranslator.StartsWithTranslator.GetMethods())
				{
				}

				// Token: 0x0600032B RID: 811 RVA: 0x00012138 File Offset: 0x00010338
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("StartsWith", new Type[]
					{
						typeof(string)
					});
					yield break;
				}

				// Token: 0x0600032C RID: 812 RVA: 0x0001214E File Offset: 0x0001034E
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateFunctionIntoLike(call, false, true, new Func<ExpressionConverter, MethodCallExpression, DbExpression, DbExpression, DbExpression>(ExpressionConverter.MethodCallTranslator.StartsWithTranslator.CreateDefaultTranslation));
				}

				// Token: 0x0600032D RID: 813 RVA: 0x00012168 File Offset: 0x00010368
				private static DbExpression CreateDefaultTranslation(ExpressionConverter parent, MethodCallExpression call, DbExpression patternExpression, DbExpression inputExpression)
				{
					return parent.CreateCanonicalFunction("IndexOf", call, new DbExpression[]
					{
						patternExpression,
						inputExpression
					}).Equal(DbExpressionBuilder.Constant(1));
				}
			}

			// Token: 0x02000052 RID: 82
			internal sealed class EndsWithTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x0600032E RID: 814 RVA: 0x000121A3 File Offset: 0x000103A3
				internal EndsWithTranslator() : base(ExpressionConverter.MethodCallTranslator.EndsWithTranslator.GetMethods())
				{
				}

				// Token: 0x0600032F RID: 815 RVA: 0x00012290 File Offset: 0x00010490
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("EndsWith", new Type[]
					{
						typeof(string)
					});
					yield break;
				}

				// Token: 0x06000330 RID: 816 RVA: 0x000122A6 File Offset: 0x000104A6
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateFunctionIntoLike(call, true, false, new Func<ExpressionConverter, MethodCallExpression, DbExpression, DbExpression, DbExpression>(ExpressionConverter.MethodCallTranslator.EndsWithTranslator.CreateDefaultTranslation));
				}

				// Token: 0x06000331 RID: 817 RVA: 0x000122C0 File Offset: 0x000104C0
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

			// Token: 0x02000053 RID: 83
			internal sealed class SubstringTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06000332 RID: 818 RVA: 0x00012334 File Offset: 0x00010534
				internal SubstringTranslator() : base(ExpressionConverter.MethodCallTranslator.SubstringTranslator.GetMethods())
				{
				}

				// Token: 0x06000333 RID: 819 RVA: 0x00012478 File Offset: 0x00010678
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("Substring", new Type[]
					{
						typeof(int)
					});
					yield return typeof(string).GetDeclaredMethod("Substring", new Type[]
					{
						typeof(int),
						typeof(int)
					});
					yield break;
				}

				// Token: 0x06000334 RID: 820 RVA: 0x00012490 File Offset: 0x00010690
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

			// Token: 0x02000054 RID: 84
			internal sealed class RemoveTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06000335 RID: 821 RVA: 0x0001253D File Offset: 0x0001073D
				internal RemoveTranslator() : base(ExpressionConverter.MethodCallTranslator.RemoveTranslator.GetMethods())
				{
				}

				// Token: 0x06000336 RID: 822 RVA: 0x00012680 File Offset: 0x00010880
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("Remove", new Type[]
					{
						typeof(int)
					});
					yield return typeof(string).GetDeclaredMethod("Remove", new Type[]
					{
						typeof(int),
						typeof(int)
					});
					yield break;
				}

				// Token: 0x06000337 RID: 823 RVA: 0x00012698 File Offset: 0x00010898
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
							throw new NotSupportedException(Strings.ELinq_UnsupportedStringRemoveCase(call.Method, call.Method.GetParameters()[1].Name));
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

				// Token: 0x06000338 RID: 824 RVA: 0x000127D4 File Offset: 0x000109D4
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

			// Token: 0x02000055 RID: 85
			internal sealed class InsertTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06000339 RID: 825 RVA: 0x00012815 File Offset: 0x00010A15
				internal InsertTranslator() : base(ExpressionConverter.MethodCallTranslator.InsertTranslator.GetMethods())
				{
				}

				// Token: 0x0600033A RID: 826 RVA: 0x00012910 File Offset: 0x00010B10
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("Insert", new Type[]
					{
						typeof(int),
						typeof(string)
					});
					yield break;
				}

				// Token: 0x0600033B RID: 827 RVA: 0x00012928 File Offset: 0x00010B28
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

			// Token: 0x02000056 RID: 86
			internal sealed class IsNullOrEmptyTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x0600033C RID: 828 RVA: 0x00012A33 File Offset: 0x00010C33
				internal IsNullOrEmptyTranslator() : base(ExpressionConverter.MethodCallTranslator.IsNullOrEmptyTranslator.GetMethods())
				{
				}

				// Token: 0x0600033D RID: 829 RVA: 0x00012B20 File Offset: 0x00010D20
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("IsNullOrEmpty", new Type[]
					{
						typeof(string)
					});
					yield break;
				}

				// Token: 0x0600033E RID: 830 RVA: 0x00012B38 File Offset: 0x00010D38
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

			// Token: 0x02000057 RID: 87
			internal sealed class StringConcatTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x0600033F RID: 831 RVA: 0x00012B94 File Offset: 0x00010D94
				internal StringConcatTranslator() : base(ExpressionConverter.MethodCallTranslator.StringConcatTranslator.GetMethods())
				{
				}

				// Token: 0x06000340 RID: 832 RVA: 0x00012F0C File Offset: 0x0001110C
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[]
					{
						typeof(string),
						typeof(string)
					});
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string)
					});
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string)
					});
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[]
					{
						typeof(object),
						typeof(object)
					});
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[]
					{
						typeof(object),
						typeof(object),
						typeof(object)
					});
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[]
					{
						typeof(object),
						typeof(object),
						typeof(object),
						typeof(object)
					});
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[]
					{
						typeof(object[])
					});
					yield return typeof(string).GetDeclaredMethod("Concat", new Type[]
					{
						typeof(string[])
					});
					yield break;
				}

				// Token: 0x06000341 RID: 833 RVA: 0x00012F2C File Offset: 0x0001112C
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					Expression[] linqArgs;
					if (call.Arguments.Count == 1 && (call.Arguments.First<Expression>().Type == typeof(object[]) || call.Arguments.First<Expression>().Type == typeof(string[])))
					{
						NewArrayExpression newArrayExpression = call.Arguments[0] as NewArrayExpression;
						if (newArrayExpression != null)
						{
							linqArgs = ((NewArrayExpression)call.Arguments[0]).Expressions.ToArray<Expression>();
						}
						else
						{
							ConstantExpression constantExpression = (ConstantExpression)call.Arguments[0];
							if (constantExpression.Value == null)
							{
								throw new ArgumentNullException((constantExpression.Type == typeof(object[])) ? "args" : "values");
							}
							linqArgs = (from v in (object[])constantExpression.Value
							select Expression.Constant(v)).ToArray<ConstantExpression>();
						}
					}
					else
					{
						linqArgs = call.Arguments.ToArray<Expression>();
					}
					return ExpressionConverter.StringTranslatorUtil.ConcatArgs(parent, call, linqArgs);
				}
			}

			// Token: 0x02000058 RID: 88
			internal sealed class ToStringTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06000343 RID: 835 RVA: 0x00013051 File Offset: 0x00011251
				internal ToStringTranslator() : base(ExpressionConverter.MethodCallTranslator.ToStringTranslator._methods)
				{
				}

				// Token: 0x06000344 RID: 836 RVA: 0x0001305E File Offset: 0x0001125E
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return ExpressionConverter.StringTranslatorUtil.ConvertToString(parent, call.Object);
				}

				// Token: 0x04000137 RID: 311
				private static readonly MethodInfo[] _methods = new MethodInfo[]
				{
					typeof(string).GetDeclaredMethod("ToString", new Type[0]),
					typeof(byte).GetDeclaredMethod("ToString", new Type[0]),
					typeof(sbyte).GetDeclaredMethod("ToString", new Type[0]),
					typeof(short).GetDeclaredMethod("ToString", new Type[0]),
					typeof(int).GetDeclaredMethod("ToString", new Type[0]),
					typeof(long).GetDeclaredMethod("ToString", new Type[0]),
					typeof(double).GetDeclaredMethod("ToString", new Type[0]),
					typeof(float).GetDeclaredMethod("ToString", new Type[0]),
					typeof(Guid).GetDeclaredMethod("ToString", new Type[0]),
					typeof(DateTime).GetDeclaredMethod("ToString", new Type[0]),
					typeof(DateTimeOffset).GetDeclaredMethod("ToString", new Type[0]),
					typeof(TimeSpan).GetDeclaredMethod("ToString", new Type[0]),
					typeof(decimal).GetDeclaredMethod("ToString", new Type[0]),
					typeof(bool).GetDeclaredMethod("ToString", new Type[0]),
					typeof(object).GetDeclaredMethod("ToString", new Type[0])
				};
			}

			// Token: 0x02000059 RID: 89
			internal abstract class TrimBaseTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06000346 RID: 838 RVA: 0x00013240 File Offset: 0x00011440
				protected TrimBaseTranslator(IEnumerable<MethodInfo> methods, string canonicalFunctionName) : base(methods)
				{
					this._canonicalFunctionName = canonicalFunctionName;
				}

				// Token: 0x06000347 RID: 839 RVA: 0x00013250 File Offset: 0x00011450
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (!ExpressionConverter.MethodCallTranslator.TrimBaseTranslator.IsEmptyArray(call.Arguments[0]))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedTrimStartTrimEndCase(call.Method));
					}
					return parent.TranslateIntoCanonicalFunction(this._canonicalFunctionName, call, new Expression[]
					{
						call.Object
					});
				}

				// Token: 0x06000348 RID: 840 RVA: 0x000132A0 File Offset: 0x000114A0
				internal static bool IsEmptyArray(Expression expression)
				{
					NewArrayExpression newArrayExpression = (NewArrayExpression)expression;
					if (expression.NodeType == ExpressionType.NewArrayInit)
					{
						if (newArrayExpression.Expressions.Count == 0)
						{
							return true;
						}
					}
					else if (expression.NodeType == ExpressionType.NewArrayBounds && newArrayExpression.Expressions.Count == 1 && newArrayExpression.Expressions[0].NodeType == ExpressionType.Constant)
					{
						return object.Equals(((ConstantExpression)newArrayExpression.Expressions[0]).Value, 0);
					}
					return false;
				}

				// Token: 0x04000138 RID: 312
				private readonly string _canonicalFunctionName;
			}

			// Token: 0x0200005A RID: 90
			internal sealed class TrimTranslator : ExpressionConverter.MethodCallTranslator.TrimBaseTranslator
			{
				// Token: 0x06000349 RID: 841 RVA: 0x0001331D File Offset: 0x0001151D
				internal TrimTranslator() : base(ExpressionConverter.MethodCallTranslator.TrimTranslator.GetMethods(), "Trim")
				{
				}

				// Token: 0x0600034A RID: 842 RVA: 0x00013410 File Offset: 0x00011610
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("Trim", new Type[]
					{
						typeof(char[])
					});
					yield break;
				}
			}

			// Token: 0x0200005B RID: 91
			internal sealed class TrimStartTranslator : ExpressionConverter.MethodCallTranslator.TrimBaseTranslator
			{
				// Token: 0x0600034B RID: 843 RVA: 0x00013426 File Offset: 0x00011626
				internal TrimStartTranslator() : base(ExpressionConverter.MethodCallTranslator.TrimStartTranslator.GetMethods(), "LTrim")
				{
				}

				// Token: 0x0600034C RID: 844 RVA: 0x00013518 File Offset: 0x00011718
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("TrimStart", new Type[]
					{
						typeof(char[])
					});
					yield break;
				}
			}

			// Token: 0x0200005C RID: 92
			internal sealed class TrimEndTranslator : ExpressionConverter.MethodCallTranslator.TrimBaseTranslator
			{
				// Token: 0x0600034D RID: 845 RVA: 0x0001352E File Offset: 0x0001172E
				internal TrimEndTranslator() : base(ExpressionConverter.MethodCallTranslator.TrimEndTranslator.GetMethods(), "RTrim")
				{
				}

				// Token: 0x0600034E RID: 846 RVA: 0x00013620 File Offset: 0x00011820
				private static IEnumerable<MethodInfo> GetMethods()
				{
					yield return typeof(string).GetDeclaredMethod("TrimEnd", new Type[]
					{
						typeof(char[])
					});
					yield break;
				}
			}

			// Token: 0x0200005D RID: 93
			internal sealed class VBCanonicalFunctionDefaultTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x0600034F RID: 847 RVA: 0x00013636 File Offset: 0x00011836
				internal VBCanonicalFunctionDefaultTranslator(Assembly vbAssembly) : base(ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionDefaultTranslator.GetMethods(vbAssembly))
				{
				}

				// Token: 0x06000350 RID: 848 RVA: 0x00013A08 File Offset: 0x00011C08
				private static IEnumerable<MethodInfo> GetMethods(Assembly vbAssembly)
				{
					Type stringsType = vbAssembly.GetType("Microsoft.VisualBasic.Strings");
					yield return stringsType.GetDeclaredMethod("Trim", new Type[]
					{
						typeof(string)
					});
					yield return stringsType.GetDeclaredMethod("LTrim", new Type[]
					{
						typeof(string)
					});
					yield return stringsType.GetDeclaredMethod("RTrim", new Type[]
					{
						typeof(string)
					});
					yield return stringsType.GetDeclaredMethod("Left", new Type[]
					{
						typeof(string),
						typeof(int)
					});
					yield return stringsType.GetDeclaredMethod("Right", new Type[]
					{
						typeof(string),
						typeof(int)
					});
					Type dateTimeType = vbAssembly.GetType("Microsoft.VisualBasic.DateAndTime");
					yield return dateTimeType.GetDeclaredMethod("Year", new Type[]
					{
						typeof(DateTime)
					});
					yield return dateTimeType.GetDeclaredMethod("Month", new Type[]
					{
						typeof(DateTime)
					});
					yield return dateTimeType.GetDeclaredMethod("Day", new Type[]
					{
						typeof(DateTime)
					});
					yield return dateTimeType.GetDeclaredMethod("Hour", new Type[]
					{
						typeof(DateTime)
					});
					yield return dateTimeType.GetDeclaredMethod("Minute", new Type[]
					{
						typeof(DateTime)
					});
					yield return dateTimeType.GetDeclaredMethod("Second", new Type[]
					{
						typeof(DateTime)
					});
					yield break;
				}

				// Token: 0x06000351 RID: 849 RVA: 0x00013A25 File Offset: 0x00011C25
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateIntoCanonicalFunction(call.Method.Name, call, call.Arguments.ToArray<Expression>());
				}

				// Token: 0x04000139 RID: 313
				private const string s_stringsTypeFullName = "Microsoft.VisualBasic.Strings";

				// Token: 0x0400013A RID: 314
				private const string s_dateAndTimeTypeFullName = "Microsoft.VisualBasic.DateAndTime";
			}

			// Token: 0x0200005E RID: 94
			internal sealed class VBCanonicalFunctionRenameTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06000352 RID: 850 RVA: 0x00013A44 File Offset: 0x00011C44
				internal VBCanonicalFunctionRenameTranslator(Assembly vbAssembly) : base(ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethods(vbAssembly).ToArray<MethodInfo>())
				{
				}

				// Token: 0x06000353 RID: 851 RVA: 0x00013C48 File Offset: 0x00011E48
				private static IEnumerable<MethodInfo> GetMethods(Assembly vbAssembly)
				{
					Type stringsType = vbAssembly.GetType("Microsoft.VisualBasic.Strings");
					yield return ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethodInfo(stringsType, "Len", "Length", new Type[]
					{
						typeof(string)
					});
					yield return ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethodInfo(stringsType, "Mid", "Substring", new Type[]
					{
						typeof(string),
						typeof(int),
						typeof(int)
					});
					yield return ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethodInfo(stringsType, "UCase", "ToUpper", new Type[]
					{
						typeof(string)
					});
					yield return ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.GetMethodInfo(stringsType, "LCase", "ToLower", new Type[]
					{
						typeof(string)
					});
					yield break;
				}

				// Token: 0x06000354 RID: 852 RVA: 0x00013C68 File Offset: 0x00011E68
				private static MethodInfo GetMethodInfo(Type declaringType, string methodName, string canonicalFunctionName, Type[] argumentTypes)
				{
					MethodInfo declaredMethod = declaringType.GetDeclaredMethod(methodName, argumentTypes);
					ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.s_methodNameMap.Add(declaredMethod, canonicalFunctionName);
					return declaredMethod;
				}

				// Token: 0x06000355 RID: 853 RVA: 0x00013C8B File Offset: 0x00011E8B
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.TranslateIntoCanonicalFunction(ExpressionConverter.MethodCallTranslator.VBCanonicalFunctionRenameTranslator.s_methodNameMap[call.Method], call, call.Arguments.ToArray<Expression>());
				}

				// Token: 0x0400013B RID: 315
				private const string s_stringsTypeFullName = "Microsoft.VisualBasic.Strings";

				// Token: 0x0400013C RID: 316
				private static readonly Dictionary<MethodInfo, string> s_methodNameMap = new Dictionary<MethodInfo, string>(4);
			}

			// Token: 0x0200005F RID: 95
			internal sealed class VBDatePartTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x06000357 RID: 855 RVA: 0x00013CBC File Offset: 0x00011EBC
				internal VBDatePartTranslator(Assembly vbAssembly) : base(ExpressionConverter.MethodCallTranslator.VBDatePartTranslator.GetMethods(vbAssembly))
				{
				}

				// Token: 0x06000358 RID: 856 RVA: 0x00013E28 File Offset: 0x00012028
				private static IEnumerable<MethodInfo> GetMethods(Assembly vbAssembly)
				{
					Type dateAndTimeType = vbAssembly.GetType("Microsoft.VisualBasic.DateAndTime");
					Type dateIntervalEnum = vbAssembly.GetType("Microsoft.VisualBasic.DateInterval");
					Type firstDayOfWeekEnum = vbAssembly.GetType("Microsoft.VisualBasic.FirstDayOfWeek");
					Type firstWeekOfYearEnum = vbAssembly.GetType("Microsoft.VisualBasic.FirstWeekOfYear");
					yield return dateAndTimeType.GetDeclaredMethod("DatePart", new Type[]
					{
						dateIntervalEnum,
						typeof(DateTime),
						firstDayOfWeekEnum,
						firstWeekOfYearEnum
					});
					yield break;
				}

				// Token: 0x06000359 RID: 857 RVA: 0x00013E48 File Offset: 0x00012048
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					ConstantExpression constantExpression = call.Arguments[0] as ConstantExpression;
					if (constantExpression == null)
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedVBDatePartNonConstantInterval(call.Method, call.Method.GetParameters()[0].Name));
					}
					string text = constantExpression.Value.ToString();
					if (!ExpressionConverter.MethodCallTranslator.VBDatePartTranslator._supportedIntervals.Contains(text))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedVBDatePartInvalidInterval(call.Method, call.Method.GetParameters()[0].Name, text));
					}
					return parent.TranslateIntoCanonicalFunction(text, call, new Expression[]
					{
						call.Arguments[1]
					});
				}

				// Token: 0x0400013D RID: 317
				private const string s_dateAndTimeTypeFullName = "Microsoft.VisualBasic.DateAndTime";

				// Token: 0x0400013E RID: 318
				private const string s_DateIntervalFullName = "Microsoft.VisualBasic.DateInterval";

				// Token: 0x0400013F RID: 319
				private const string s_FirstDayOfWeekFullName = "Microsoft.VisualBasic.FirstDayOfWeek";

				// Token: 0x04000140 RID: 320
				private const string s_FirstWeekOfYearFullName = "Microsoft.VisualBasic.FirstWeekOfYear";

				// Token: 0x04000141 RID: 321
				private static readonly HashSet<string> _supportedIntervals = new HashSet<string>
				{
					"Year",
					"Month",
					"Day",
					"Hour",
					"Minute",
					"Second"
				};
			}

			// Token: 0x02000060 RID: 96
			private abstract class SequenceMethodTranslator
			{
				// Token: 0x0600035B RID: 859 RVA: 0x00013F4D File Offset: 0x0001214D
				protected SequenceMethodTranslator(params SequenceMethod[] methods)
				{
					this._methods = methods;
				}

				// Token: 0x1700002F RID: 47
				// (get) Token: 0x0600035C RID: 860 RVA: 0x00013F5C File Offset: 0x0001215C
				internal IEnumerable<SequenceMethod> Methods
				{
					get
					{
						return this._methods;
					}
				}

				// Token: 0x0600035D RID: 861 RVA: 0x00013F64 File Offset: 0x00012164
				internal virtual DbExpression Translate(ExpressionConverter parent, MethodCallExpression call, SequenceMethod sequenceMethod)
				{
					return this.Translate(parent, call);
				}

				// Token: 0x0600035E RID: 862
				internal abstract DbExpression Translate(ExpressionConverter parent, MethodCallExpression call);

				// Token: 0x0600035F RID: 863 RVA: 0x00013F6E File Offset: 0x0001216E
				public override string ToString()
				{
					return base.GetType().Name;
				}

				// Token: 0x04000142 RID: 322
				private readonly IEnumerable<SequenceMethod> _methods;
			}

			// Token: 0x02000061 RID: 97
			private abstract class UnarySequenceMethodTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06000360 RID: 864 RVA: 0x00013F7B File Offset: 0x0001217B
				protected UnarySequenceMethodTranslator(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x06000361 RID: 865 RVA: 0x00013F84 File Offset: 0x00012184
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

				// Token: 0x06000362 RID: 866
				protected abstract DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call);
			}

			// Token: 0x02000062 RID: 98
			private abstract class PagingTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x06000363 RID: 867 RVA: 0x00013FCC File Offset: 0x000121CC
				protected PagingTranslator(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x06000364 RID: 868 RVA: 0x00013FD8 File Offset: 0x000121D8
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					Expression linq = call.Arguments[1];
					DbExpression count = parent.TranslateExpression(linq);
					return this.TranslatePagingOperator(parent, operand, count);
				}

				// Token: 0x06000365 RID: 869
				protected abstract DbExpression TranslatePagingOperator(ExpressionConverter parent, DbExpression operand, DbExpression count);
			}

			// Token: 0x02000063 RID: 99
			private sealed class TakeTranslator : ExpressionConverter.MethodCallTranslator.PagingTranslator
			{
				// Token: 0x06000366 RID: 870 RVA: 0x00014008 File Offset: 0x00012208
				internal TakeTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Take
				})
				{
				}

				// Token: 0x06000367 RID: 871 RVA: 0x00014028 File Offset: 0x00012228
				protected override DbExpression TranslatePagingOperator(ExpressionConverter parent, DbExpression operand, DbExpression count)
				{
					DbConstantExpression dbConstantExpression = count as DbConstantExpression;
					if (dbConstantExpression != null && dbConstantExpression.Value.Equals(0))
					{
						return parent.Filter(operand.BindAs(parent.AliasGenerator.Next()), DbExpressionBuilder.False);
					}
					return parent.Limit(operand, count);
				}
			}

			// Token: 0x02000064 RID: 100
			private sealed class SkipTranslator : ExpressionConverter.MethodCallTranslator.PagingTranslator
			{
				// Token: 0x06000368 RID: 872 RVA: 0x00014078 File Offset: 0x00012278
				internal SkipTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Skip
				})
				{
				}

				// Token: 0x06000369 RID: 873 RVA: 0x00014098 File Offset: 0x00012298
				protected override DbExpression TranslatePagingOperator(ExpressionConverter parent, DbExpression operand, DbExpression count)
				{
					return parent.Skip(operand.BindAs(parent.AliasGenerator.Next()), count);
				}
			}

			// Token: 0x02000065 RID: 101
			private sealed class JoinTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x0600036A RID: 874 RVA: 0x000140B4 File Offset: 0x000122B4
				internal JoinTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Join
				})
				{
				}

				// Token: 0x0600036B RID: 875 RVA: 0x000140D4 File Offset: 0x000122D4
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
						throw new NotSupportedException(Strings.ELinq_UnsupportedKeySelector(call.Method.Name));
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

			// Token: 0x02000066 RID: 102
			private abstract class BinarySequenceMethodTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x0600036C RID: 876 RVA: 0x000142E4 File Offset: 0x000124E4
				protected BinarySequenceMethodTranslator(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x0600036D RID: 877 RVA: 0x000142ED File Offset: 0x000124ED
				private static DbExpression TranslateLeft(ExpressionConverter parent, Expression expr)
				{
					return parent.TranslateSet(expr);
				}

				// Token: 0x0600036E RID: 878 RVA: 0x000142F6 File Offset: 0x000124F6
				protected virtual DbExpression TranslateRight(ExpressionConverter parent, Expression expr)
				{
					return parent.TranslateSet(expr);
				}

				// Token: 0x0600036F RID: 879 RVA: 0x00014300 File Offset: 0x00012500
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (call.Object != null)
					{
						DbExpression left = ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator.TranslateLeft(parent, call.Object);
						DbExpression right = this.TranslateRight(parent, call.Arguments[0]);
						return this.TranslateBinary(parent, left, right);
					}
					DbExpression left2 = ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator.TranslateLeft(parent, call.Arguments[0]);
					DbExpression right2 = this.TranslateRight(parent, call.Arguments[1]);
					return this.TranslateBinary(parent, left2, right2);
				}

				// Token: 0x06000370 RID: 880
				protected abstract DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right);
			}

			// Token: 0x02000067 RID: 103
			private class ConcatTranslator : ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator
			{
				// Token: 0x06000371 RID: 881 RVA: 0x00014370 File Offset: 0x00012570
				internal ConcatTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Concat
				})
				{
				}

				// Token: 0x06000372 RID: 882 RVA: 0x00014390 File Offset: 0x00012590
				protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right)
				{
					return parent.UnionAll(left, right);
				}
			}

			// Token: 0x02000068 RID: 104
			private sealed class UnionTranslator : ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator
			{
				// Token: 0x06000373 RID: 883 RVA: 0x0001439C File Offset: 0x0001259C
				internal UnionTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Union
				})
				{
				}

				// Token: 0x06000374 RID: 884 RVA: 0x000143BC File Offset: 0x000125BC
				protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right)
				{
					return parent.Distinct(parent.UnionAll(left, right));
				}
			}

			// Token: 0x02000069 RID: 105
			private sealed class IntersectTranslator : ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator
			{
				// Token: 0x06000375 RID: 885 RVA: 0x000143CC File Offset: 0x000125CC
				internal IntersectTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Intersect
				})
				{
				}

				// Token: 0x06000376 RID: 886 RVA: 0x000143EC File Offset: 0x000125EC
				protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right)
				{
					return parent.Intersect(left, right);
				}
			}

			// Token: 0x0200006A RID: 106
			private sealed class ExceptTranslator : ExpressionConverter.MethodCallTranslator.BinarySequenceMethodTranslator
			{
				// Token: 0x06000377 RID: 887 RVA: 0x000143F8 File Offset: 0x000125F8
				internal ExceptTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Except
				})
				{
				}

				// Token: 0x06000378 RID: 888 RVA: 0x00014418 File Offset: 0x00012618
				protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right)
				{
					return parent.Except(left, right);
				}

				// Token: 0x06000379 RID: 889 RVA: 0x00014424 File Offset: 0x00012624
				protected override DbExpression TranslateRight(ExpressionConverter parent, Expression expr)
				{
					parent.IgnoreInclude++;
					DbExpression result = base.TranslateRight(parent, expr);
					parent.IgnoreInclude--;
					return result;
				}
			}

			// Token: 0x0200006B RID: 107
			private abstract class AggregateTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x0600037A RID: 890 RVA: 0x00014457 File Offset: 0x00012657
				protected AggregateTranslator(string functionName, bool takesPredicate, params SequenceMethod[] methods) : base(methods)
				{
					this._takesPredicate = takesPredicate;
					this._functionName = functionName;
				}

				// Token: 0x0600037B RID: 891 RVA: 0x00014470 File Offset: 0x00012670
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

				// Token: 0x0600037C RID: 892 RVA: 0x00014522 File Offset: 0x00012722
				protected virtual TypeUsage GetReturnType(ExpressionConverter parent, MethodCallExpression call)
				{
					return parent.GetValueLayerType(call.Type);
				}

				// Token: 0x0600037D RID: 893 RVA: 0x00014530 File Offset: 0x00012730
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

				// Token: 0x0600037E RID: 894 RVA: 0x00014583 File Offset: 0x00012783
				protected virtual DbExpression WrapNonCollectionOperand(ExpressionConverter parent, DbExpression operand, TypeUsage returnType)
				{
					if (!ExpressionConverter.TypeUsageEquals(returnType, operand.ResultType))
					{
						operand = operand.CastTo(returnType);
					}
					return operand;
				}

				// Token: 0x0600037F RID: 895 RVA: 0x000145A0 File Offset: 0x000127A0
				protected virtual EdmFunction FindFunction(ExpressionConverter parent, MethodCallExpression call, TypeUsage argumentType)
				{
					List<TypeUsage> list = new List<TypeUsage>(1);
					list.Add(argumentType);
					return parent.FindCanonicalFunction(this._functionName, list, true, call);
				}

				// Token: 0x04000143 RID: 323
				private readonly string _functionName;

				// Token: 0x04000144 RID: 324
				private readonly bool _takesPredicate;
			}

			// Token: 0x0200006C RID: 108
			private sealed class MaxTranslator : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x06000380 RID: 896 RVA: 0x000145CC File Offset: 0x000127CC
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

				// Token: 0x06000381 RID: 897 RVA: 0x0001466C File Offset: 0x0001286C
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

			// Token: 0x0200006D RID: 109
			private sealed class MinTranslator : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x06000382 RID: 898 RVA: 0x000146A4 File Offset: 0x000128A4
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

				// Token: 0x06000383 RID: 899 RVA: 0x00014744 File Offset: 0x00012944
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

			// Token: 0x0200006E RID: 110
			private sealed class AverageTranslator : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x06000384 RID: 900 RVA: 0x0001477C File Offset: 0x0001297C
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

			// Token: 0x0200006F RID: 111
			private sealed class SumTranslator : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x06000385 RID: 901 RVA: 0x0001484C File Offset: 0x00012A4C
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

			// Token: 0x02000070 RID: 112
			private abstract class CountTranslatorBase : ExpressionConverter.MethodCallTranslator.AggregateTranslator
			{
				// Token: 0x06000386 RID: 902 RVA: 0x000148FB File Offset: 0x00012AFB
				protected CountTranslatorBase(string functionName, params SequenceMethod[] methods) : base(functionName, true, methods)
				{
				}

				// Token: 0x06000387 RID: 903 RVA: 0x00014908 File Offset: 0x00012B08
				protected override DbExpression WrapCollectionOperand(ExpressionConverter parent, DbExpression operand, TypeUsage returnType)
				{
					return operand.BindAs(parent.AliasGenerator.Next()).Project(DbExpressionBuilder.Constant(1));
				}

				// Token: 0x06000388 RID: 904 RVA: 0x00014938 File Offset: 0x00012B38
				protected override DbExpression WrapNonCollectionOperand(ExpressionConverter parent, DbExpression operand, TypeUsage returnType)
				{
					DbExpression dbExpression = DbExpressionBuilder.Constant(1);
					if (!ExpressionConverter.TypeUsageEquals(dbExpression.ResultType, returnType))
					{
						dbExpression = dbExpression.CastTo(returnType);
					}
					return dbExpression;
				}

				// Token: 0x06000389 RID: 905 RVA: 0x00014968 File Offset: 0x00012B68
				protected override EdmFunction FindFunction(ExpressionConverter parent, MethodCallExpression call, TypeUsage argumentType)
				{
					TypeUsage argumentType2 = TypeUsage.CreateDefaultTypeUsage(EdmProviderManifest.Instance.GetPrimitiveType(PrimitiveTypeKind.Int32));
					return base.FindFunction(parent, call, argumentType2);
				}
			}

			// Token: 0x02000071 RID: 113
			private sealed class CountTranslator : ExpressionConverter.MethodCallTranslator.CountTranslatorBase
			{
				// Token: 0x0600038A RID: 906 RVA: 0x00014990 File Offset: 0x00012B90
				internal CountTranslator() : base("Count", new SequenceMethod[]
				{
					SequenceMethod.Count,
					SequenceMethod.CountPredicate
				})
				{
				}
			}

			// Token: 0x02000072 RID: 114
			private sealed class LongCountTranslator : ExpressionConverter.MethodCallTranslator.CountTranslatorBase
			{
				// Token: 0x0600038B RID: 907 RVA: 0x000149BC File Offset: 0x00012BBC
				internal LongCountTranslator() : base("BigCount", new SequenceMethod[]
				{
					SequenceMethod.LongCount,
					SequenceMethod.LongCountPredicate
				})
				{
				}
			}

			// Token: 0x02000073 RID: 115
			private sealed class PassthroughTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x0600038C RID: 908 RVA: 0x000149E8 File Offset: 0x00012BE8
				internal PassthroughTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.AsQueryableGeneric,
					SequenceMethod.AsQueryable,
					SequenceMethod.AsEnumerable,
					SequenceMethod.ToList
				})
				{
				}

				// Token: 0x0600038D RID: 909 RVA: 0x00014A23 File Offset: 0x00012C23
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					if (TypeSemantics.IsCollectionType(operand.ResultType))
					{
						return operand;
					}
					throw new NotSupportedException(Strings.ELinq_UnsupportedPassthrough(call.Method.Name, operand.ResultType.EdmType.Name));
				}
			}

			// Token: 0x02000074 RID: 116
			private sealed class OfTypeTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x0600038E RID: 910 RVA: 0x00014A5C File Offset: 0x00012C5C
				internal OfTypeTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.OfType
				})
				{
				}

				// Token: 0x0600038F RID: 911 RVA: 0x00014A7C File Offset: 0x00012C7C
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					Type type = call.Method.GetGenericArguments()[0];
					TypeUsage typeUsage;
					if (!parent.TryGetValueLayerType(type, out typeUsage) || (!TypeSemantics.IsEntityType(typeUsage) && !TypeSemantics.IsComplexType(typeUsage)))
					{
						throw new NotSupportedException(Strings.ELinq_InvalidOfTypeResult(ExpressionConverter.DescribeClrType(type)));
					}
					return parent.OfType(operand, typeUsage);
				}
			}

			// Token: 0x02000075 RID: 117
			private sealed class DistinctTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x06000390 RID: 912 RVA: 0x00014AD0 File Offset: 0x00012CD0
				internal DistinctTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Distinct
				})
				{
				}

				// Token: 0x06000391 RID: 913 RVA: 0x00014AF0 File Offset: 0x00012CF0
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					return parent.Distinct(operand);
				}
			}

			// Token: 0x02000076 RID: 118
			private sealed class AnyTranslator : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x06000392 RID: 914 RVA: 0x00014AFC File Offset: 0x00012CFC
				internal AnyTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Any
				})
				{
				}

				// Token: 0x06000393 RID: 915 RVA: 0x00014B1C File Offset: 0x00012D1C
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					return operand.IsEmpty().Not();
				}
			}

			// Token: 0x02000077 RID: 119
			private abstract class OneLambdaTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x06000394 RID: 916 RVA: 0x00014B29 File Offset: 0x00012D29
				internal OneLambdaTranslator(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x06000395 RID: 917 RVA: 0x00014B34 File Offset: 0x00012D34
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression;
					DbExpressionBinding dbExpressionBinding;
					DbExpression dbExpression2;
					return this.Translate(parent, call, out dbExpression, out dbExpressionBinding, out dbExpression2);
				}

				// Token: 0x06000396 RID: 918 RVA: 0x00014B50 File Offset: 0x00012D50
				protected DbExpression Translate(ExpressionConverter parent, MethodCallExpression call, out DbExpression source, out DbExpressionBinding sourceBinding, out DbExpression lambda)
				{
					source = parent.TranslateExpression(call.Arguments[0]);
					LambdaExpression lambdaExpression = parent.GetLambdaExpression(call, 1);
					lambda = parent.TranslateLambda(lambdaExpression, source, out sourceBinding);
					return this.TranslateOneLambda(parent, sourceBinding, lambda);
				}

				// Token: 0x06000397 RID: 919
				protected abstract DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda);
			}

			// Token: 0x02000078 RID: 120
			private sealed class AnyPredicateTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x06000398 RID: 920 RVA: 0x00014B98 File Offset: 0x00012D98
				internal AnyPredicateTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.AnyPredicate
				})
				{
				}

				// Token: 0x06000399 RID: 921 RVA: 0x00014BB8 File Offset: 0x00012DB8
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return sourceBinding.Any(lambda);
				}
			}

			// Token: 0x02000079 RID: 121
			private sealed class AllTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x0600039A RID: 922 RVA: 0x00014BC4 File Offset: 0x00012DC4
				internal AllTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.All
				})
				{
				}

				// Token: 0x0600039B RID: 923 RVA: 0x00014BE4 File Offset: 0x00012DE4
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return sourceBinding.All(lambda);
				}
			}

			// Token: 0x0200007A RID: 122
			private sealed class WhereTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x0600039C RID: 924 RVA: 0x00014BF0 File Offset: 0x00012DF0
				internal WhereTranslator()
				{
					SequenceMethod[] methods = new SequenceMethod[1];
					base..ctor(methods);
				}

				// Token: 0x0600039D RID: 925 RVA: 0x00014C0B File Offset: 0x00012E0B
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return parent.Filter(sourceBinding, lambda);
				}
			}

			// Token: 0x0200007B RID: 123
			private sealed class SelectTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x0600039E RID: 926 RVA: 0x00014C18 File Offset: 0x00012E18
				internal SelectTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Select
				})
				{
				}

				// Token: 0x0600039F RID: 927 RVA: 0x00014C38 File Offset: 0x00012E38
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression;
					DbExpressionBinding dbExpressionBinding;
					DbExpression dbExpression2;
					return base.Translate(parent, call, out dbExpression, out dbExpressionBinding, out dbExpression2);
				}

				// Token: 0x060003A0 RID: 928 RVA: 0x00014C55 File Offset: 0x00012E55
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return parent.Project(sourceBinding, lambda);
				}
			}

			// Token: 0x0200007C RID: 124
			private sealed class DefaultIfEmptyTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x060003A1 RID: 929 RVA: 0x00014C60 File Offset: 0x00012E60
				internal DefaultIfEmptyTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.DefaultIfEmpty,
					SequenceMethod.DefaultIfEmptyValue
				})
				{
				}

				// Token: 0x060003A2 RID: 930 RVA: 0x00014C88 File Offset: 0x00012E88
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateSet(call.Arguments[0]);
					DbExpression dbExpression2 = (call.Arguments.Count == 2) ? parent.TranslateExpression(call.Arguments[1]) : ExpressionConverter.MethodCallTranslator.DefaultIfEmptyTranslator.GetDefaultValue(parent, call.Type);
					DbExpression input = DbExpressionBuilder.NewCollection(new DbExpression[]
					{
						new byte?(1)
					});
					DbExpressionBinding left = input.BindAs(parent.AliasGenerator.Next());
					bool flag = dbExpression2 != null && dbExpression2.ExpressionKind != DbExpressionKind.Null;
					if (flag)
					{
						DbExpressionBinding dbExpressionBinding = dbExpression.BindAs(parent.AliasGenerator.Next());
						dbExpression = dbExpressionBinding.Project(new Row(new byte?(1).As("sentinel"), new KeyValuePair<string, DbExpression>[]
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

				// Token: 0x060003A3 RID: 931 RVA: 0x00014E30 File Offset: 0x00013030
				private static DbExpression GetDefaultValue(ExpressionConverter parent, Type resultType)
				{
					Type elementType = TypeSystem.GetElementType(resultType);
					object defaultValue = TypeSystem.GetDefaultValue(elementType);
					return (defaultValue == null) ? null : parent.TranslateExpression(Expression.Constant(defaultValue, elementType));
				}
			}

			// Token: 0x0200007D RID: 125
			private sealed class ContainsTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x060003A4 RID: 932 RVA: 0x00014E60 File Offset: 0x00013060
				internal ContainsTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Contains
				})
				{
				}

				// Token: 0x060003A5 RID: 933 RVA: 0x00014E80 File Offset: 0x00013080
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContains(parent, call.Arguments[0], call.Arguments[1]);
				}

				// Token: 0x060003A6 RID: 934 RVA: 0x00014ED8 File Offset: 0x000130D8
				private static DbExpression TranslateContainsHelper(ExpressionConverter parent, DbExpression left, IEnumerable<DbExpression> rightList, ExpressionConverter.EqualsPattern pattern, Type leftType, Type rightType)
				{
					IEnumerable<DbExpression> collection = from argument in rightList
					select parent.CreateEqualsExpression(left, argument, pattern, leftType, rightType);
					List<DbExpression> nodes = new List<DbExpression>(collection);
					return Helpers.BuildBalancedTreeInPlace<DbExpression>(nodes, (DbExpression prev, DbExpression next) => prev.Or(next));
				}

				// Token: 0x060003A7 RID: 935 RVA: 0x00014F50 File Offset: 0x00013150
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
					bool useCSharpNullComparisonBehavior = parent._funcletizer.RootContext.ContextOptions.UseCSharpNullComparisonBehavior;
					bool flag = parent.ProviderManifest.SupportsInExpression();
					if (!useCSharpNullComparisonBehavior && !flag)
					{
						return ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContainsHelper(parent, dbExpression2, arguments, ExpressionConverter.EqualsPattern.Store, elementType, valueExpression.Type);
					}
					List<DbExpression> list = new List<DbExpression>();
					List<DbExpression> list2 = new List<DbExpression>();
					foreach (DbExpression dbExpression3 in arguments)
					{
						List<DbExpression> list3 = (dbExpression3.ExpressionKind == DbExpressionKind.Constant) ? list : list2;
						list3.Add(dbExpression3);
					}
					DbExpression dbExpression4 = null;
					if (list.Count > 0)
					{
						ExpressionConverter.EqualsPattern pattern2 = useCSharpNullComparisonBehavior ? ExpressionConverter.EqualsPattern.PositiveNullEqualityNonComposable : ExpressionConverter.EqualsPattern.Store;
						dbExpression4 = (flag ? DbExpressionBuilder.CreateInExpression(dbExpression2, list) : ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContainsHelper(parent, dbExpression2, list, pattern2, elementType, valueExpression.Type));
						if (useCSharpNullComparisonBehavior)
						{
							dbExpression4 = dbExpression4.And(dbExpression2.IsNull().Not());
						}
					}
					DbExpression dbExpression5 = null;
					if (list2.Count > 0)
					{
						ExpressionConverter.EqualsPattern pattern3 = useCSharpNullComparisonBehavior ? ExpressionConverter.EqualsPattern.PositiveNullEqualityComposable : ExpressionConverter.EqualsPattern.Store;
						dbExpression5 = ExpressionConverter.MethodCallTranslator.ContainsTranslator.TranslateContainsHelper(parent, dbExpression2, list2, pattern3, elementType, valueExpression.Type);
					}
					if (dbExpression4 == null)
					{
						return dbExpression5;
					}
					if (dbExpression5 == null)
					{
						return dbExpression4;
					}
					return dbExpression4.Or(dbExpression5);
				}
			}

			// Token: 0x0200007E RID: 126
			private abstract class FirstTranslatorBase : ExpressionConverter.MethodCallTranslator.UnarySequenceMethodTranslator
			{
				// Token: 0x060003A9 RID: 937 RVA: 0x00015138 File Offset: 0x00013338
				protected FirstTranslatorBase(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x060003AA RID: 938 RVA: 0x00015141 File Offset: 0x00013341
				protected virtual DbExpression LimitResult(ExpressionConverter parent, DbExpression expression)
				{
					return parent.Limit(expression, DbExpressionBuilder.Constant(1));
				}

				// Token: 0x060003AB RID: 939 RVA: 0x00015158 File Offset: 0x00013358
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					DbExpression dbExpression = this.LimitResult(parent, operand);
					if (!parent.IsQueryRoot(call))
					{
						dbExpression = dbExpression.Element();
						dbExpression = ExpressionConverter.MethodCallTranslator.FirstTranslatorBase.AddDefaultCase(dbExpression, call.Type);
					}
					Span span = null;
					if (parent.TryGetSpan(operand, out span))
					{
						parent.AddSpanMapping(dbExpression, span);
					}
					return dbExpression;
				}

				// Token: 0x060003AC RID: 940 RVA: 0x000151A4 File Offset: 0x000133A4
				internal static DbExpression AddDefaultCase(DbExpression element, Type elementType)
				{
					object defaultValue = TypeSystem.GetDefaultValue(elementType);
					if (defaultValue == null)
					{
						return element;
					}
					return DbExpressionBuilder.Case(new List<DbExpression>(1)
					{
						ExpressionConverter.CreateIsNullExpression(element, elementType)
					}, new List<DbExpression>(1)
					{
						element.ResultType.Constant(defaultValue)
					}, element);
				}
			}

			// Token: 0x0200007F RID: 127
			private sealed class FirstTranslator : ExpressionConverter.MethodCallTranslator.FirstTranslatorBase
			{
				// Token: 0x060003AD RID: 941 RVA: 0x000151F4 File Offset: 0x000133F4
				internal FirstTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.First
				})
				{
				}

				// Token: 0x060003AE RID: 942 RVA: 0x00015214 File Offset: 0x00013414
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					if (!parent.IsQueryRoot(call))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedNestedFirst);
					}
					return base.TranslateUnary(parent, operand, call);
				}
			}

			// Token: 0x02000080 RID: 128
			private sealed class FirstOrDefaultTranslator : ExpressionConverter.MethodCallTranslator.FirstTranslatorBase
			{
				// Token: 0x060003AF RID: 943 RVA: 0x00015234 File Offset: 0x00013434
				internal FirstOrDefaultTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.FirstOrDefault
				})
				{
				}
			}

			// Token: 0x02000081 RID: 129
			private abstract class SingleTranslatorBase : ExpressionConverter.MethodCallTranslator.FirstTranslatorBase
			{
				// Token: 0x060003B0 RID: 944 RVA: 0x00015254 File Offset: 0x00013454
				protected SingleTranslatorBase(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x060003B1 RID: 945 RVA: 0x0001525D File Offset: 0x0001345D
				protected override DbExpression TranslateUnary(ExpressionConverter parent, DbExpression operand, MethodCallExpression call)
				{
					if (!parent.IsQueryRoot(call))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedNestedSingle);
					}
					return base.TranslateUnary(parent, operand, call);
				}

				// Token: 0x060003B2 RID: 946 RVA: 0x0001527C File Offset: 0x0001347C
				protected override DbExpression LimitResult(ExpressionConverter parent, DbExpression expression)
				{
					return parent.Limit(expression, DbExpressionBuilder.Constant(2));
				}
			}

			// Token: 0x02000082 RID: 130
			private sealed class SingleTranslator : ExpressionConverter.MethodCallTranslator.SingleTranslatorBase
			{
				// Token: 0x060003B3 RID: 947 RVA: 0x00015290 File Offset: 0x00013490
				internal SingleTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Single
				})
				{
				}
			}

			// Token: 0x02000083 RID: 131
			private sealed class SingleOrDefaultTranslator : ExpressionConverter.MethodCallTranslator.SingleTranslatorBase
			{
				// Token: 0x060003B4 RID: 948 RVA: 0x000152B0 File Offset: 0x000134B0
				internal SingleOrDefaultTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.SingleOrDefault
				})
				{
				}
			}

			// Token: 0x02000084 RID: 132
			private abstract class FirstPredicateTranslatorBase : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x060003B5 RID: 949 RVA: 0x000152D0 File Offset: 0x000134D0
				protected FirstPredicateTranslatorBase(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x060003B6 RID: 950 RVA: 0x000152D9 File Offset: 0x000134D9
				protected virtual DbExpression RestrictResult(ExpressionConverter parent, DbExpression expression)
				{
					return parent.Limit(expression, DbExpressionBuilder.Constant(1));
				}

				// Token: 0x060003B7 RID: 951 RVA: 0x000152F0 File Offset: 0x000134F0
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = base.Translate(parent, call);
					if (parent.IsQueryRoot(call))
					{
						return this.RestrictResult(parent, dbExpression);
					}
					dbExpression = this.RestrictResult(parent, dbExpression);
					DbExpression dbExpression2 = dbExpression.Element();
					dbExpression2 = ExpressionConverter.MethodCallTranslator.FirstTranslatorBase.AddDefaultCase(dbExpression2, call.Type);
					Span span = null;
					if (parent.TryGetSpan(dbExpression, out span))
					{
						parent.AddSpanMapping(dbExpression2, span);
					}
					return dbExpression2;
				}

				// Token: 0x060003B8 RID: 952 RVA: 0x0001534C File Offset: 0x0001354C
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					return parent.Filter(sourceBinding, lambda);
				}
			}

			// Token: 0x02000085 RID: 133
			private sealed class FirstPredicateTranslator : ExpressionConverter.MethodCallTranslator.FirstPredicateTranslatorBase
			{
				// Token: 0x060003B9 RID: 953 RVA: 0x00015358 File Offset: 0x00013558
				internal FirstPredicateTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.FirstPredicate
				})
				{
				}

				// Token: 0x060003BA RID: 954 RVA: 0x00015378 File Offset: 0x00013578
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (!parent.IsQueryRoot(call))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedNestedFirst);
					}
					return base.Translate(parent, call);
				}
			}

			// Token: 0x02000086 RID: 134
			private sealed class FirstOrDefaultPredicateTranslator : ExpressionConverter.MethodCallTranslator.FirstPredicateTranslatorBase
			{
				// Token: 0x060003BB RID: 955 RVA: 0x00015398 File Offset: 0x00013598
				internal FirstOrDefaultPredicateTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.FirstOrDefaultPredicate
				})
				{
				}
			}

			// Token: 0x02000087 RID: 135
			private abstract class SinglePredicateTranslatorBase : ExpressionConverter.MethodCallTranslator.FirstPredicateTranslatorBase
			{
				// Token: 0x060003BC RID: 956 RVA: 0x000153B8 File Offset: 0x000135B8
				protected SinglePredicateTranslatorBase(params SequenceMethod[] methods) : base(methods)
				{
				}

				// Token: 0x060003BD RID: 957 RVA: 0x000153C1 File Offset: 0x000135C1
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					if (!parent.IsQueryRoot(call))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedNestedSingle);
					}
					return base.Translate(parent, call);
				}

				// Token: 0x060003BE RID: 958 RVA: 0x000153DF File Offset: 0x000135DF
				protected override DbExpression RestrictResult(ExpressionConverter parent, DbExpression expression)
				{
					return parent.Limit(expression, DbExpressionBuilder.Constant(2));
				}
			}

			// Token: 0x02000088 RID: 136
			private sealed class SinglePredicateTranslator : ExpressionConverter.MethodCallTranslator.SinglePredicateTranslatorBase
			{
				// Token: 0x060003BF RID: 959 RVA: 0x000153F4 File Offset: 0x000135F4
				internal SinglePredicateTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.SinglePredicate
				})
				{
				}
			}

			// Token: 0x02000089 RID: 137
			private sealed class SingleOrDefaultPredicateTranslator : ExpressionConverter.MethodCallTranslator.SinglePredicateTranslatorBase
			{
				// Token: 0x060003C0 RID: 960 RVA: 0x00015414 File Offset: 0x00013614
				internal SingleOrDefaultPredicateTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.SingleOrDefaultPredicate
				})
				{
				}
			}

			// Token: 0x0200008A RID: 138
			private sealed class SelectManyTranslator : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x060003C1 RID: 961 RVA: 0x00015434 File Offset: 0x00013634
				internal SelectManyTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.SelectMany,
					SequenceMethod.SelectManyResultSelector
				})
				{
				}

				// Token: 0x060003C2 RID: 962 RVA: 0x00015458 File Offset: 0x00013658
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

				// Token: 0x060003C3 RID: 963 RVA: 0x0001565C File Offset: 0x0001385C
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

				// Token: 0x060003C4 RID: 964 RVA: 0x00015858 File Offset: 0x00013A58
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					lambda = parent.NormalizeSetSource(lambda);
					DbExpressionBinding apply = lambda.BindAs(parent.AliasGenerator.Next());
					return sourceBinding.CrossApply(apply);
				}
			}

			// Token: 0x0200008B RID: 139
			private sealed class CastMethodTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x060003C5 RID: 965 RVA: 0x0001588C File Offset: 0x00013A8C
				internal CastMethodTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.Cast
				})
				{
				}

				// Token: 0x060003C6 RID: 966 RVA: 0x000158AC File Offset: 0x00013AAC
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

			// Token: 0x0200008C RID: 140
			private sealed class GroupByTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x060003C7 RID: 967 RVA: 0x0001591C File Offset: 0x00013B1C
				internal GroupByTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.GroupBy,
					SequenceMethod.GroupByElementSelector,
					SequenceMethod.GroupByElementSelectorResultSelector,
					SequenceMethod.GroupByResultSelector
				})
				{
				}

				// Token: 0x060003C8 RID: 968 RVA: 0x0001594C File Offset: 0x00013B4C
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call, SequenceMethod sequenceMethod)
				{
					DbExpression input = parent.TranslateSet(call.Arguments[0]);
					LambdaExpression lambdaExpression = parent.GetLambdaExpression(call, 1);
					DbGroupExpressionBinding dbGroupExpressionBinding;
					DbExpression dbExpression = parent.TranslateLambda(lambdaExpression, input, out dbGroupExpressionBinding);
					if (!TypeSemantics.IsEqualComparable(dbExpression.ResultType))
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedKeySelector(call.Method.Name));
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

				// Token: 0x060003C9 RID: 969 RVA: 0x00015AF4 File Offset: 0x00013CF4
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

				// Token: 0x060003CA RID: 970 RVA: 0x00015BC5 File Offset: 0x00013DC5
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					return null;
				}
			}

			// Token: 0x0200008D RID: 141
			private sealed class GroupJoinTranslator : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x060003CB RID: 971 RVA: 0x00015BC8 File Offset: 0x00013DC8
				internal GroupJoinTranslator() : base(new SequenceMethod[]
				{
					SequenceMethod.GroupJoin
				})
				{
				}

				// Token: 0x060003CC RID: 972 RVA: 0x00015BE8 File Offset: 0x00013DE8
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
						throw new NotSupportedException(Strings.ELinq_UnsupportedKeySelector(call.Method.Name));
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
					return ExpressionConverter.MethodCallTranslator.GroupJoinTranslator.CollapseTrivialRenamingProjection(cqtExpression3);
				}

				// Token: 0x060003CD RID: 973 RVA: 0x00015DAC File Offset: 0x00013FAC
				private static DbExpression CollapseTrivialRenamingProjection(DbExpression cqtExpression)
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

			// Token: 0x0200008E RID: 142
			private abstract class OrderByTranslatorBase : ExpressionConverter.MethodCallTranslator.OneLambdaTranslator
			{
				// Token: 0x060003CE RID: 974 RVA: 0x00015FA0 File Offset: 0x000141A0
				protected OrderByTranslatorBase(bool ascending, params SequenceMethod[] methods) : base(methods)
				{
					this._ascending = ascending;
				}

				// Token: 0x060003CF RID: 975 RVA: 0x00015FB0 File Offset: 0x000141B0
				protected override DbExpression TranslateOneLambda(ExpressionConverter parent, DbExpressionBinding sourceBinding, DbExpression lambda)
				{
					List<DbSortClause> list = new List<DbSortClause>(1);
					DbSortClause item = this._ascending ? lambda.ToSortClause() : lambda.ToSortClauseDescending();
					list.Add(item);
					return parent.Sort(sourceBinding, list);
				}

				// Token: 0x04000146 RID: 326
				private readonly bool _ascending;
			}

			// Token: 0x0200008F RID: 143
			private sealed class OrderByTranslator : ExpressionConverter.MethodCallTranslator.OrderByTranslatorBase
			{
				// Token: 0x060003D0 RID: 976 RVA: 0x00015FEC File Offset: 0x000141EC
				internal OrderByTranslator() : base(true, new SequenceMethod[]
				{
					SequenceMethod.OrderBy
				})
				{
				}
			}

			// Token: 0x02000090 RID: 144
			private sealed class OrderByDescendingTranslator : ExpressionConverter.MethodCallTranslator.OrderByTranslatorBase
			{
				// Token: 0x060003D1 RID: 977 RVA: 0x00016010 File Offset: 0x00014210
				internal OrderByDescendingTranslator() : base(false, new SequenceMethod[]
				{
					SequenceMethod.OrderByDescending
				})
				{
				}
			}

			// Token: 0x02000091 RID: 145
			private abstract class ThenByTranslatorBase : ExpressionConverter.MethodCallTranslator.SequenceMethodTranslator
			{
				// Token: 0x060003D2 RID: 978 RVA: 0x00016031 File Offset: 0x00014231
				protected ThenByTranslatorBase(bool ascending, params SequenceMethod[] methods) : base(methods)
				{
					this._ascending = ascending;
				}

				// Token: 0x060003D3 RID: 979 RVA: 0x00016044 File Offset: 0x00014244
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					DbExpression dbExpression = parent.TranslateSet(call.Arguments[0]);
					if (DbExpressionKind.Sort != dbExpression.ExpressionKind)
					{
						throw new InvalidOperationException(Strings.ELinq_ThenByDoesNotFollowOrderBy);
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

				// Token: 0x04000147 RID: 327
				private readonly bool _ascending;
			}

			// Token: 0x02000092 RID: 146
			private sealed class ThenByTranslator : ExpressionConverter.MethodCallTranslator.ThenByTranslatorBase
			{
				// Token: 0x060003D4 RID: 980 RVA: 0x000160FC File Offset: 0x000142FC
				internal ThenByTranslator() : base(true, new SequenceMethod[]
				{
					SequenceMethod.ThenBy
				})
				{
				}
			}

			// Token: 0x02000093 RID: 147
			private sealed class ThenByDescendingTranslator : ExpressionConverter.MethodCallTranslator.ThenByTranslatorBase
			{
				// Token: 0x060003D5 RID: 981 RVA: 0x00016120 File Offset: 0x00014320
				internal ThenByDescendingTranslator() : base(false, new SequenceMethod[]
				{
					SequenceMethod.ThenByDescending
				})
				{
				}
			}

			// Token: 0x02000094 RID: 148
			private sealed class SpatialMethodCallTranslator : ExpressionConverter.MethodCallTranslator.CallTranslator
			{
				// Token: 0x060003D6 RID: 982 RVA: 0x00016141 File Offset: 0x00014341
				internal SpatialMethodCallTranslator() : base(ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetSupportedMethods())
				{
				}

				// Token: 0x060003D7 RID: 983 RVA: 0x00016150 File Offset: 0x00014350
				private static MethodInfo GetStaticMethod<TResult>(Expression<Func<TResult>> lambda)
				{
					return ((MethodCallExpression)lambda.Body).Method;
				}

				// Token: 0x060003D8 RID: 984 RVA: 0x00016170 File Offset: 0x00014370
				private static MethodInfo GetInstanceMethod<T, TResult>(Expression<Func<T, TResult>> lambda)
				{
					return ((MethodCallExpression)lambda.Body).Method;
				}

				// Token: 0x060003D9 RID: 985 RVA: 0x00018598 File Offset: 0x00016798
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

				// Token: 0x060003DA RID: 986 RVA: 0x000185B0 File Offset: 0x000167B0
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

				// Token: 0x060003DB RID: 987 RVA: 0x0001A484 File Offset: 0x00018684
				internal override DbExpression Translate(ExpressionConverter parent, MethodCallExpression call)
				{
					MethodInfo method = call.Method;
					string functionName;
					if (!ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator._methodFunctionRenames.TryGetValue(method, out functionName))
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

				// Token: 0x04000148 RID: 328
				private static readonly Dictionary<MethodInfo, string> _methodFunctionRenames = ExpressionConverter.MethodCallTranslator.SpatialMethodCallTranslator.GetRenamedMethodFunctions();
			}
		}

		// Token: 0x02000095 RID: 149
		private sealed class OrderByLifter
		{
			// Token: 0x060003DD RID: 989 RVA: 0x0001A50A File Offset: 0x0001870A
			internal OrderByLifter(AliasGenerator aliasGenerator)
			{
				this._aliasGenerator = aliasGenerator;
			}

			// Token: 0x060003DE RID: 990 RVA: 0x0001A51C File Offset: 0x0001871C
			internal DbExpression Project(DbExpressionBinding input, DbExpression projection)
			{
				ExpressionConverter.OrderByLifter.OrderByLifterBase lifter = this.GetLifter(input.Expression);
				return lifter.Project(input.Project(projection));
			}

			// Token: 0x060003DF RID: 991 RVA: 0x0001A544 File Offset: 0x00018744
			internal DbExpression Filter(DbExpressionBinding input, DbExpression predicate)
			{
				ExpressionConverter.OrderByLifter.OrderByLifterBase lifter = this.GetLifter(input.Expression);
				return lifter.Filter(input.Filter(predicate));
			}

			// Token: 0x060003E0 RID: 992 RVA: 0x0001A56C File Offset: 0x0001876C
			internal DbExpression OfType(DbExpression argument, TypeUsage type)
			{
				ExpressionConverter.OrderByLifter.OrderByLifterBase lifter = this.GetLifter(argument);
				return lifter.OfType(type);
			}

			// Token: 0x060003E1 RID: 993 RVA: 0x0001A588 File Offset: 0x00018788
			internal DbExpression Skip(DbExpressionBinding input, DbExpression skipCount)
			{
				ExpressionConverter.OrderByLifter.OrderByLifterBase lifter = this.GetLifter(input.Expression);
				return lifter.Skip(skipCount);
			}

			// Token: 0x060003E2 RID: 994 RVA: 0x0001A5AC File Offset: 0x000187AC
			internal DbExpression Limit(DbExpression argument, DbExpression limit)
			{
				ExpressionConverter.OrderByLifter.OrderByLifterBase lifter = this.GetLifter(argument);
				return lifter.Limit(limit);
			}

			// Token: 0x060003E3 RID: 995 RVA: 0x0001A5C8 File Offset: 0x000187C8
			private ExpressionConverter.OrderByLifter.OrderByLifterBase GetLifter(DbExpression root)
			{
				return ExpressionConverter.OrderByLifter.OrderByLifterBase.GetLifter(root, this._aliasGenerator);
			}

			// Token: 0x04000149 RID: 329
			private readonly AliasGenerator _aliasGenerator;

			// Token: 0x02000096 RID: 150
			private abstract class OrderByLifterBase
			{
				// Token: 0x060003E4 RID: 996 RVA: 0x0001A5D6 File Offset: 0x000187D6
				protected OrderByLifterBase(DbExpression root, AliasGenerator aliasGenerator)
				{
					this._root = root;
					this._aliasGenerator = aliasGenerator;
				}

				// Token: 0x060003E5 RID: 997 RVA: 0x0001A5EC File Offset: 0x000187EC
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

				// Token: 0x060003E6 RID: 998
				internal abstract DbExpression Project(DbProjectExpression project);

				// Token: 0x060003E7 RID: 999
				internal abstract DbExpression Filter(DbFilterExpression filter);

				// Token: 0x060003E8 RID: 1000 RVA: 0x0001A778 File Offset: 0x00018978
				internal virtual DbExpression OfType(TypeUsage type)
				{
					DbExpressionBinding dbExpressionBinding = this._root.BindAs(this._aliasGenerator.Next());
					DbExpression dbExpression = this.Filter(dbExpressionBinding.Filter(dbExpressionBinding.Variable.IsOf(type)));
					ExpressionConverter.OrderByLifter.OrderByLifterBase lifter = ExpressionConverter.OrderByLifter.OrderByLifterBase.GetLifter(dbExpression, this._aliasGenerator);
					DbExpressionBinding dbExpressionBinding2 = dbExpression.BindAs(this._aliasGenerator.Next());
					return lifter.Project(dbExpressionBinding2.Project(dbExpressionBinding2.Variable.TreatAs(type)));
				}

				// Token: 0x060003E9 RID: 1001
				internal abstract DbExpression Limit(DbExpression k);

				// Token: 0x060003EA RID: 1002
				internal abstract DbExpression Skip(DbExpression k);

				// Token: 0x060003EB RID: 1003 RVA: 0x0001A7F0 File Offset: 0x000189F0
				protected static DbProjectExpression ComposeProject(DbExpression input, DbProjectExpression first, DbProjectExpression second)
				{
					DbLambda lambda = DbExpressionBuilder.Lambda(second.Projection, new DbVariableReferenceExpression[]
					{
						second.Input.Variable
					});
					DbProjectExpression project = first.Input.Project(lambda.Invoke(new DbExpression[]
					{
						first.Projection
					}));
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(input, project);
				}

				// Token: 0x060003EC RID: 1004 RVA: 0x0001A84C File Offset: 0x00018A4C
				protected static DbFilterExpression ComposeFilter(DbExpression input, DbProjectExpression first, DbFilterExpression second)
				{
					DbLambda lambda = DbExpressionBuilder.Lambda(second.Predicate, new DbVariableReferenceExpression[]
					{
						second.Input.Variable
					});
					DbFilterExpression filter = first.Input.Filter(lambda.Invoke(new DbExpression[]
					{
						first.Projection
					}));
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindFilter(input, filter);
				}

				// Token: 0x060003ED RID: 1005 RVA: 0x0001A8AC File Offset: 0x00018AAC
				protected static DbSkipExpression AddToSkip(DbExpression input, DbSkipExpression skip, DbExpression plusK)
				{
					DbExpression k = ExpressionConverter.OrderByLifter.OrderByLifterBase.CombineIntegers(skip.Count, plusK, (int l, int r) => l + r);
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSkip(input, skip, k);
				}

				// Token: 0x060003EE RID: 1006 RVA: 0x0001A8F8 File Offset: 0x00018AF8
				protected static DbLimitExpression SubtractFromLimit(DbExpression input, DbLimitExpression limit, DbExpression minusK)
				{
					DbExpression count = ExpressionConverter.OrderByLifter.OrderByLifterBase.CombineIntegers(limit.Limit, minusK, delegate(int l, int r)
					{
						if (r <= l)
						{
							return l - r;
						}
						return 0;
					});
					return input.Limit(count);
				}

				// Token: 0x060003EF RID: 1007 RVA: 0x0001A938 File Offset: 0x00018B38
				protected static DbLimitExpression MinimumLimit(DbExpression input, DbLimitExpression limit, DbExpression k)
				{
					DbExpression count = ExpressionConverter.OrderByLifter.OrderByLifterBase.CombineIntegers(limit.Limit, k, new Func<int, int, int>(Math.Min));
					return input.Limit(count);
				}

				// Token: 0x060003F0 RID: 1008 RVA: 0x0001A968 File Offset: 0x00018B68
				private static DbExpression CombineIntegers(DbExpression left, DbExpression right, Func<int, int, int> combineConstants)
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
					throw new InvalidOperationException(Strings.ADP_InternalProviderError(1025));
				}

				// Token: 0x060003F1 RID: 1009 RVA: 0x0001A9E8 File Offset: 0x00018BE8
				protected static DbProjectExpression RebindProject(DbExpression input, DbProjectExpression project)
				{
					DbExpressionBinding input2 = input.BindAs(project.Input.VariableName);
					return input2.Project(project.Projection);
				}

				// Token: 0x060003F2 RID: 1010 RVA: 0x0001AA14 File Offset: 0x00018C14
				protected static DbFilterExpression RebindFilter(DbExpression input, DbFilterExpression filter)
				{
					DbExpressionBinding input2 = input.BindAs(filter.Input.VariableName);
					return input2.Filter(filter.Predicate);
				}

				// Token: 0x060003F3 RID: 1011 RVA: 0x0001AA40 File Offset: 0x00018C40
				protected static DbSortExpression RebindSort(DbExpression input, DbSortExpression sort)
				{
					DbExpressionBinding input2 = input.BindAs(sort.Input.VariableName);
					return input2.Sort(sort.SortOrder);
				}

				// Token: 0x060003F4 RID: 1012 RVA: 0x0001AA6C File Offset: 0x00018C6C
				protected static DbSortExpression ApplySkipOrderToSort(DbExpression input, DbSkipExpression sortSpec)
				{
					DbExpressionBinding input2 = input.BindAs(sortSpec.Input.VariableName);
					return input2.Sort(sortSpec.SortOrder);
				}

				// Token: 0x060003F5 RID: 1013 RVA: 0x0001AA98 File Offset: 0x00018C98
				protected static DbSkipExpression ApplySortOrderToSkip(DbExpression input, DbSortExpression sort, DbExpression k)
				{
					DbExpressionBinding input2 = input.BindAs(sort.Input.VariableName);
					return input2.Skip(sort.SortOrder, k);
				}

				// Token: 0x060003F6 RID: 1014 RVA: 0x0001AAC4 File Offset: 0x00018CC4
				protected static DbSkipExpression RebindSkip(DbExpression input, DbSkipExpression skip, DbExpression k)
				{
					DbExpressionBinding input2 = input.BindAs(skip.Input.VariableName);
					return input2.Skip(skip.SortOrder, k);
				}

				// Token: 0x0400014A RID: 330
				protected readonly DbExpression _root;

				// Token: 0x0400014B RID: 331
				protected readonly AliasGenerator _aliasGenerator;
			}

			// Token: 0x02000097 RID: 151
			private class LimitSkipLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x060003F9 RID: 1017 RVA: 0x0001AAF0 File Offset: 0x00018CF0
				internal LimitSkipLifter(DbLimitExpression limit, DbSkipExpression skip, AliasGenerator aliasGenerator) : base(limit, aliasGenerator)
				{
					this._limit = limit;
					this._skip = skip;
				}

				// Token: 0x060003FA RID: 1018 RVA: 0x0001AB08 File Offset: 0x00018D08
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySkipOrderToSort(filter, this._skip);
				}

				// Token: 0x060003FB RID: 1019 RVA: 0x0001AB16 File Offset: 0x00018D16
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x060003FC RID: 1020 RVA: 0x0001AB1C File Offset: 0x00018D1C
				internal override DbExpression Limit(DbExpression k)
				{
					if (this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return ExpressionConverter.OrderByLifter.OrderByLifterBase.MinimumLimit(this._skip, this._limit, k);
					}
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySkipOrderToSort(this._limit, this._skip).Limit(k);
				}

				// Token: 0x060003FD RID: 1021 RVA: 0x0001AB6F File Offset: 0x00018D6F
				internal override DbExpression Skip(DbExpression k)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSkip(this._limit, this._skip, k);
				}

				// Token: 0x0400014E RID: 334
				private readonly DbLimitExpression _limit;

				// Token: 0x0400014F RID: 335
				private readonly DbSkipExpression _skip;
			}

			// Token: 0x02000098 RID: 152
			private class LimitSortLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x060003FE RID: 1022 RVA: 0x0001AB83 File Offset: 0x00018D83
				internal LimitSortLifter(DbLimitExpression limit, DbSortExpression sort, AliasGenerator aliasGenerator) : base(limit, aliasGenerator)
				{
					this._limit = limit;
					this._sort = sort;
				}

				// Token: 0x060003FF RID: 1023 RVA: 0x0001AB9B File Offset: 0x00018D9B
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSort(filter, this._sort);
				}

				// Token: 0x06000400 RID: 1024 RVA: 0x0001ABA9 File Offset: 0x00018DA9
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x06000401 RID: 1025 RVA: 0x0001ABAC File Offset: 0x00018DAC
				internal override DbExpression Limit(DbExpression k)
				{
					if (this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return ExpressionConverter.OrderByLifter.OrderByLifterBase.MinimumLimit(this._sort, this._limit, k);
					}
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSort(this._limit, this._sort).Limit(k);
				}

				// Token: 0x06000402 RID: 1026 RVA: 0x0001ABFF File Offset: 0x00018DFF
				internal override DbExpression Skip(DbExpression k)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySortOrderToSkip(this._limit, this._sort, k);
				}

				// Token: 0x04000150 RID: 336
				private readonly DbLimitExpression _limit;

				// Token: 0x04000151 RID: 337
				private readonly DbSortExpression _sort;
			}

			// Token: 0x02000099 RID: 153
			private class ProjectLimitSkipLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x06000403 RID: 1027 RVA: 0x0001AC13 File Offset: 0x00018E13
				internal ProjectLimitSkipLifter(DbProjectExpression project, DbLimitExpression limit, DbSkipExpression skip, AliasGenerator aliasGenerator) : base(project, aliasGenerator)
				{
					this._project = project;
					this._limit = limit;
					this._skip = skip;
					this._source = skip.Input.Expression;
				}

				// Token: 0x06000404 RID: 1028 RVA: 0x0001AC44 File Offset: 0x00018E44
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySkipOrderToSort(ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeFilter(this._skip.Limit(this._limit.Limit), this._project, filter), this._skip), this._project);
				}

				// Token: 0x06000405 RID: 1029 RVA: 0x0001AC7E File Offset: 0x00018E7E
				internal override DbExpression Project(DbProjectExpression project)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeProject(this._skip.Limit(this._limit.Limit), this._project, project);
				}

				// Token: 0x06000406 RID: 1030 RVA: 0x0001ACA4 File Offset: 0x00018EA4
				internal override DbExpression Limit(DbExpression k)
				{
					if (this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.MinimumLimit(this._skip, this._limit, k), this._project);
					}
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySkipOrderToSort(this._skip.Limit(this._limit.Limit), this._skip).Limit(k), this._project);
				}

				// Token: 0x06000407 RID: 1031 RVA: 0x0001AD20 File Offset: 0x00018F20
				internal override DbExpression Skip(DbExpression k)
				{
					if (this._skip.Count.ExpressionKind == DbExpressionKind.Constant && this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.SubtractFromLimit(ExpressionConverter.OrderByLifter.OrderByLifterBase.AddToSkip(this._source, this._skip, k), this._limit, k), this._project);
					}
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSkip(this._skip.Limit(this._limit.Limit), this._skip, k), this._project);
				}

				// Token: 0x04000152 RID: 338
				private readonly DbProjectExpression _project;

				// Token: 0x04000153 RID: 339
				private readonly DbLimitExpression _limit;

				// Token: 0x04000154 RID: 340
				private readonly DbSkipExpression _skip;

				// Token: 0x04000155 RID: 341
				private readonly DbExpression _source;
			}

			// Token: 0x0200009A RID: 154
			private class ProjectLimitSortLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x06000408 RID: 1032 RVA: 0x0001ADB3 File Offset: 0x00018FB3
				internal ProjectLimitSortLifter(DbProjectExpression project, DbLimitExpression limit, DbSortExpression sort, AliasGenerator aliasGenerator) : base(project, aliasGenerator)
				{
					this._project = project;
					this._limit = limit;
					this._sort = sort;
				}

				// Token: 0x06000409 RID: 1033 RVA: 0x0001ADD3 File Offset: 0x00018FD3
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSort(ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeFilter(this._sort.Limit(this._limit.Limit), this._project, filter), this._sort), this._project);
				}

				// Token: 0x0600040A RID: 1034 RVA: 0x0001AE0D File Offset: 0x0001900D
				internal override DbExpression Project(DbProjectExpression project)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeProject(this._sort.Limit(this._limit.Limit), this._project, project);
				}

				// Token: 0x0600040B RID: 1035 RVA: 0x0001AE34 File Offset: 0x00019034
				internal override DbExpression Limit(DbExpression k)
				{
					if (this._limit.Limit.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.MinimumLimit(this._sort, this._limit, k), this._project);
					}
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSort(this._sort.Limit(this._limit.Limit), this._sort).Limit(k), this._project);
				}

				// Token: 0x0600040C RID: 1036 RVA: 0x0001AEAD File Offset: 0x000190AD
				internal override DbExpression Skip(DbExpression k)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySortOrderToSkip(this._sort.Limit(this._limit.Limit), this._sort, k), this._project);
				}

				// Token: 0x04000156 RID: 342
				private readonly DbProjectExpression _project;

				// Token: 0x04000157 RID: 343
				private readonly DbLimitExpression _limit;

				// Token: 0x04000158 RID: 344
				private readonly DbSortExpression _sort;
			}

			// Token: 0x0200009B RID: 155
			private class ProjectSkipLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x0600040D RID: 1037 RVA: 0x0001AEDC File Offset: 0x000190DC
				internal ProjectSkipLifter(DbProjectExpression project, DbSkipExpression skip, AliasGenerator aliasGenerator) : base(project, aliasGenerator)
				{
					this._project = project;
					this._skip = skip;
					this._source = this._skip.Input.Expression;
				}

				// Token: 0x0600040E RID: 1038 RVA: 0x0001AF0A File Offset: 0x0001910A
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySkipOrderToSort(ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeFilter(this._skip, this._project, filter), this._skip), this._project);
				}

				// Token: 0x0600040F RID: 1039 RVA: 0x0001AF34 File Offset: 0x00019134
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x06000410 RID: 1040 RVA: 0x0001AF42 File Offset: 0x00019142
				internal override DbExpression Project(DbProjectExpression project)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeProject(this._skip, this._project, project);
				}

				// Token: 0x06000411 RID: 1041 RVA: 0x0001AF58 File Offset: 0x00019158
				internal override DbExpression Skip(DbExpression k)
				{
					if (this._skip.Count.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.AddToSkip(this._source, this._skip, k), this._project);
					}
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSkip(this._skip, this._skip, k), this._project);
				}

				// Token: 0x04000159 RID: 345
				private readonly DbProjectExpression _project;

				// Token: 0x0400015A RID: 346
				private readonly DbSkipExpression _skip;

				// Token: 0x0400015B RID: 347
				private readonly DbExpression _source;
			}

			// Token: 0x0200009C RID: 156
			private class SkipLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x06000412 RID: 1042 RVA: 0x0001AFBC File Offset: 0x000191BC
				internal SkipLifter(DbSkipExpression skip, AliasGenerator aliasGenerator) : base(skip, aliasGenerator)
				{
					this._skip = skip;
					this._source = skip.Input.Expression;
				}

				// Token: 0x06000413 RID: 1043 RVA: 0x0001AFDE File Offset: 0x000191DE
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySkipOrderToSort(filter, this._skip);
				}

				// Token: 0x06000414 RID: 1044 RVA: 0x0001AFEC File Offset: 0x000191EC
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x06000415 RID: 1045 RVA: 0x0001AFEF File Offset: 0x000191EF
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x06000416 RID: 1046 RVA: 0x0001B000 File Offset: 0x00019200
				internal override DbExpression Skip(DbExpression k)
				{
					if (this._skip.Count.ExpressionKind == DbExpressionKind.Constant && k.ExpressionKind == DbExpressionKind.Constant)
					{
						return ExpressionConverter.OrderByLifter.OrderByLifterBase.AddToSkip(this._source, this._skip, k);
					}
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSkip(this._skip, this._skip, k);
				}

				// Token: 0x0400015C RID: 348
				private readonly DbSkipExpression _skip;

				// Token: 0x0400015D RID: 349
				private readonly DbExpression _source;
			}

			// Token: 0x0200009D RID: 157
			private class ProjectSortLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x06000417 RID: 1047 RVA: 0x0001B04E File Offset: 0x0001924E
				internal ProjectSortLifter(DbProjectExpression project, DbSortExpression sort, AliasGenerator aliasGenerator) : base(project, aliasGenerator)
				{
					this._project = project;
					this._sort = sort;
					this._source = sort.Input.Expression;
				}

				// Token: 0x06000418 RID: 1048 RVA: 0x0001B077 File Offset: 0x00019277
				internal override DbExpression Project(DbProjectExpression project)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeProject(this._sort, this._project, project);
				}

				// Token: 0x06000419 RID: 1049 RVA: 0x0001B08B File Offset: 0x0001928B
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSort(ExpressionConverter.OrderByLifter.OrderByLifterBase.ComposeFilter(this._source, this._project, filter), this._sort), this._project);
				}

				// Token: 0x0600041A RID: 1050 RVA: 0x0001B0B5 File Offset: 0x000192B5
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x0600041B RID: 1051 RVA: 0x0001B0C3 File Offset: 0x000192C3
				internal override DbExpression Skip(DbExpression k)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindProject(ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySortOrderToSkip(this._source, this._sort, k), this._project);
				}

				// Token: 0x0400015E RID: 350
				private readonly DbProjectExpression _project;

				// Token: 0x0400015F RID: 351
				private readonly DbSortExpression _sort;

				// Token: 0x04000160 RID: 352
				private readonly DbExpression _source;
			}

			// Token: 0x0200009E RID: 158
			private class SortLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x0600041C RID: 1052 RVA: 0x0001B0E2 File Offset: 0x000192E2
				internal SortLifter(DbSortExpression sort, AliasGenerator aliasGenerator) : base(sort, aliasGenerator)
				{
					this._sort = sort;
					this._source = sort.Input.Expression;
				}

				// Token: 0x0600041D RID: 1053 RVA: 0x0001B104 File Offset: 0x00019304
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x0600041E RID: 1054 RVA: 0x0001B107 File Offset: 0x00019307
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindSort(ExpressionConverter.OrderByLifter.OrderByLifterBase.RebindFilter(this._source, filter), this._sort);
				}

				// Token: 0x0600041F RID: 1055 RVA: 0x0001B120 File Offset: 0x00019320
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x06000420 RID: 1056 RVA: 0x0001B12E File Offset: 0x0001932E
				internal override DbExpression Skip(DbExpression k)
				{
					return ExpressionConverter.OrderByLifter.OrderByLifterBase.ApplySortOrderToSkip(this._source, this._sort, k);
				}

				// Token: 0x04000161 RID: 353
				private readonly DbSortExpression _sort;

				// Token: 0x04000162 RID: 354
				private readonly DbExpression _source;
			}

			// Token: 0x0200009F RID: 159
			private class PassthroughOrderByLifter : ExpressionConverter.OrderByLifter.OrderByLifterBase
			{
				// Token: 0x06000421 RID: 1057 RVA: 0x0001B142 File Offset: 0x00019342
				internal PassthroughOrderByLifter(DbExpression source, AliasGenerator aliasGenerator) : base(source, aliasGenerator)
				{
				}

				// Token: 0x06000422 RID: 1058 RVA: 0x0001B14C File Offset: 0x0001934C
				internal override DbExpression Project(DbProjectExpression project)
				{
					return project;
				}

				// Token: 0x06000423 RID: 1059 RVA: 0x0001B14F File Offset: 0x0001934F
				internal override DbExpression Filter(DbFilterExpression filter)
				{
					return filter;
				}

				// Token: 0x06000424 RID: 1060 RVA: 0x0001B152 File Offset: 0x00019352
				internal override DbExpression OfType(TypeUsage type)
				{
					return this._root.OfType(type);
				}

				// Token: 0x06000425 RID: 1061 RVA: 0x0001B160 File Offset: 0x00019360
				internal override DbExpression Limit(DbExpression k)
				{
					return this._root.Limit(k);
				}

				// Token: 0x06000426 RID: 1062 RVA: 0x0001B16E File Offset: 0x0001936E
				internal override DbExpression Skip(DbExpression k)
				{
					throw new NotSupportedException(Strings.ELinq_SkipWithoutOrder);
				}
			}
		}

		// Token: 0x020000A0 RID: 160
		internal sealed class MemberAccessTranslator : ExpressionConverter.TypedTranslator<MemberExpression>
		{
			// Token: 0x06000427 RID: 1063 RVA: 0x0001B17C File Offset: 0x0001937C
			internal MemberAccessTranslator() : base(new ExpressionType[]
			{
				ExpressionType.MemberAccess
			})
			{
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x0001B19C File Offset: 0x0001939C
			protected override DbExpression TypedTranslate(ExpressionConverter parent, MemberExpression linq)
			{
				string text;
				Type type;
				MemberInfo memberInfo = TypeSystem.PropertyOrField(linq.Member, out text, out type);
				if (linq.Expression != null)
				{
					if (ExpressionType.Constant == linq.Expression.NodeType)
					{
						ConstantExpression constantExpression = (ConstantExpression)linq.Expression;
						if (constantExpression.Type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).FirstOrDefault<object>() != null)
						{
							Delegate @delegate = Expression.Lambda(linq, new ParameterExpression[0]).Compile();
							return parent.TranslateExpression(Expression.Constant(@delegate.DynamicInvoke(new object[0])));
						}
					}
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
				throw new NotSupportedException(Strings.ELinq_UnrecognizedMember(linq.Member.Name));
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x0001B288 File Offset: 0x00019488
			[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "System.Data.Entity.Core.Objects.ELinq.ExpressionConverter+MemberAccessTranslator.#.cctor()")]
			static MemberAccessTranslator()
			{
				ExpressionConverter.MemberAccessTranslator._propertyTranslators = new Dictionary<PropertyInfo, ExpressionConverter.MemberAccessTranslator.PropertyTranslator>();
				foreach (ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator in ExpressionConverter.MemberAccessTranslator.GetPropertyTranslators())
				{
					foreach (PropertyInfo key in propertyTranslator.Properties)
					{
						ExpressionConverter.MemberAccessTranslator._propertyTranslators.Add(key, propertyTranslator);
					}
				}
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x0001B324 File Offset: 0x00019524
			private static bool TryGetTranslator(PropertyInfo propertyInfo, out ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator)
			{
				PropertyInfo propertyInfo2 = propertyInfo;
				if (propertyInfo.DeclaringType.IsGenericType())
				{
					try
					{
						propertyInfo = propertyInfo.DeclaringType.GetGenericTypeDefinition().GetDeclaredProperty(propertyInfo.Name);
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
				if (ExpressionConverter.MemberAccessTranslator._propertyTranslators.TryGetValue(propertyInfo, out propertyTranslator2))
				{
					propertyTranslator = propertyTranslator2;
					return true;
				}
				if ("Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" == propertyInfo.DeclaringType.Assembly().FullName)
				{
					lock (ExpressionConverter.MemberAccessTranslator._vbInitializerLock)
					{
						if (!ExpressionConverter.MemberAccessTranslator._vbPropertiesInitialized)
						{
							ExpressionConverter.MemberAccessTranslator.InitializeVBProperties(propertyInfo.DeclaringType.Assembly());
							ExpressionConverter.MemberAccessTranslator._vbPropertiesInitialized = true;
						}
						if (ExpressionConverter.MemberAccessTranslator._propertyTranslators.TryGetValue(propertyInfo, out propertyTranslator2))
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

			// Token: 0x0600042B RID: 1067 RVA: 0x0001B428 File Offset: 0x00019628
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

			// Token: 0x0600042C RID: 1068 RVA: 0x0001B538 File Offset: 0x00019738
			private static DbExpression TranslateNavigationProperty(ExpressionConverter parent, MemberInfo clrMember, DbExpression instance, NavigationProperty navProp)
			{
				DbExpression dbExpression = instance.Property(navProp);
				if (BuiltInTypeKind.CollectionType == dbExpression.ResultType.EdmType.BuiltInTypeKind)
				{
					Type propertyType = ((PropertyInfo)clrMember).PropertyType;
					if (propertyType.IsGenericType() && propertyType.GetGenericTypeDefinition() == typeof(EntityCollection<>))
					{
						dbExpression = ExpressionConverter.CreateNewRowExpression(new List<KeyValuePair<string, DbExpression>>(2)
						{
							new KeyValuePair<string, DbExpression>("Owner", instance),
							new KeyValuePair<string, DbExpression>("Elements", dbExpression)
						}, InitializerMetadata.CreateEntityCollectionInitializer(parent.EdmItemCollection, propertyType, navProp));
					}
				}
				return dbExpression;
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x0001B5CC File Offset: 0x000197CC
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

			// Token: 0x0600042E RID: 1070 RVA: 0x0001B608 File Offset: 0x00019808
			private static void InitializeVBProperties(Assembly vbAssembly)
			{
				foreach (ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator in ExpressionConverter.MemberAccessTranslator.GetVisualBasicPropertyTranslators(vbAssembly))
				{
					foreach (PropertyInfo key in propertyTranslator.Properties)
					{
						ExpressionConverter.MemberAccessTranslator._propertyTranslators.Add(key, propertyTranslator);
					}
				}
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x0001B690 File Offset: 0x00019890
			private static IEnumerable<ExpressionConverter.MemberAccessTranslator.PropertyTranslator> GetVisualBasicPropertyTranslators(Assembly vbAssembly)
			{
				return new ExpressionConverter.MemberAccessTranslator.PropertyTranslator[]
				{
					new ExpressionConverter.MemberAccessTranslator.VBDateAndTimeNowTranslator(vbAssembly)
				};
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x0001B6B0 File Offset: 0x000198B0
			private static IEnumerable<ExpressionConverter.MemberAccessTranslator.PropertyTranslator> GetPropertyTranslators()
			{
				return new ExpressionConverter.MemberAccessTranslator.PropertyTranslator[]
				{
					new ExpressionConverter.MemberAccessTranslator.DefaultCanonicalFunctionPropertyTranslator(),
					new ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator(),
					new ExpressionConverter.MemberAccessTranslator.EntityCollectionCountTranslator(),
					new ExpressionConverter.MemberAccessTranslator.NullableHasValueTranslator(),
					new ExpressionConverter.MemberAccessTranslator.NullableValueTranslator(),
					new ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator()
				};
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x0001B6F8 File Offset: 0x000198F8
			internal static bool CanFuncletizePropertyInfo(PropertyInfo propertyInfo)
			{
				ExpressionConverter.MemberAccessTranslator.PropertyTranslator propertyTranslator;
				return ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator.TryGetPropertyTranslator(propertyInfo, out propertyTranslator) || !ExpressionConverter.MemberAccessTranslator.TryGetTranslator(propertyInfo, out propertyTranslator);
			}

			// Token: 0x04000163 RID: 355
			private static readonly Dictionary<PropertyInfo, ExpressionConverter.MemberAccessTranslator.PropertyTranslator> _propertyTranslators;

			// Token: 0x04000164 RID: 356
			private static bool _vbPropertiesInitialized;

			// Token: 0x04000165 RID: 357
			private static readonly object _vbInitializerLock = new object();

			// Token: 0x020000A1 RID: 161
			internal abstract class PropertyTranslator
			{
				// Token: 0x06000432 RID: 1074 RVA: 0x0001B71C File Offset: 0x0001991C
				protected PropertyTranslator(params PropertyInfo[] properties)
				{
					this._properties = properties;
				}

				// Token: 0x06000433 RID: 1075 RVA: 0x0001B72B File Offset: 0x0001992B
				protected PropertyTranslator(IEnumerable<PropertyInfo> properties)
				{
					this._properties = properties;
				}

				// Token: 0x17000030 RID: 48
				// (get) Token: 0x06000434 RID: 1076 RVA: 0x0001B73A File Offset: 0x0001993A
				internal IEnumerable<PropertyInfo> Properties
				{
					get
					{
						return this._properties;
					}
				}

				// Token: 0x06000435 RID: 1077
				internal abstract DbExpression Translate(ExpressionConverter parent, MemberExpression call);

				// Token: 0x06000436 RID: 1078 RVA: 0x0001B742 File Offset: 0x00019942
				public override string ToString()
				{
					return base.GetType().Name;
				}

				// Token: 0x04000166 RID: 358
				private readonly IEnumerable<PropertyInfo> _properties;
			}

			// Token: 0x020000A2 RID: 162
			private sealed class SpatialPropertyTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06000437 RID: 1079 RVA: 0x0001B74F File Offset: 0x0001994F
				internal SpatialPropertyTranslator() : base(ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetSupportedProperties())
				{
				}

				// Token: 0x06000438 RID: 1080 RVA: 0x0001B768 File Offset: 0x00019968
				private static PropertyInfo GetProperty<T, TResult>(Expression<Func<T, TResult>> lambda)
				{
					MemberExpression memberExpression = (MemberExpression)lambda.Body;
					return (PropertyInfo)memberExpression.Member;
				}

				// Token: 0x06000439 RID: 1081 RVA: 0x0001C70C File Offset: 0x0001A90C
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

				// Token: 0x0600043A RID: 1082 RVA: 0x0001C724 File Offset: 0x0001A924
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

				// Token: 0x0600043B RID: 1083 RVA: 0x0001D39C File Offset: 0x0001B59C
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

				// Token: 0x04000167 RID: 359
				private readonly Dictionary<PropertyInfo, string> propertyFunctionRenames = ExpressionConverter.MemberAccessTranslator.SpatialPropertyTranslator.GetRenamedPropertyFunctions();
			}

			// Token: 0x020000A3 RID: 163
			private sealed class GenericICollectionTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x0600043C RID: 1084 RVA: 0x0001D3F1 File Offset: 0x0001B5F1
				private GenericICollectionTranslator(Type elementType) : base(Enumerable.Empty<PropertyInfo>())
				{
					this._elementType = elementType;
				}

				// Token: 0x0600043D RID: 1085 RVA: 0x0001D405 File Offset: 0x0001B605
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return ExpressionConverter.MemberAccessTranslator.TranslateCount(parent, this._elementType, call.Expression);
				}

				// Token: 0x0600043E RID: 1086 RVA: 0x0001D41C File Offset: 0x0001B61C
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

				// Token: 0x0600043F RID: 1087 RVA: 0x0001D4C0 File Offset: 0x0001B6C0
				private static bool IsICollection(Type candidateType, out Type elementType)
				{
					if (candidateType.IsGenericType() && candidateType.GetGenericTypeDefinition().Equals(typeof(ICollection<>)))
					{
						elementType = candidateType.GetGenericArguments()[0];
						return true;
					}
					elementType = null;
					return false;
				}

				// Token: 0x06000440 RID: 1088 RVA: 0x0001D6E4 File Offset: 0x0001B8E4
				private static IEnumerable<KeyValuePair<Type, Type>> GetImplementedICollections(Type type)
				{
					Type collectionElementType;
					if (ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator.IsICollection(type, out collectionElementType))
					{
						yield return new KeyValuePair<Type, Type>(type, collectionElementType);
					}
					else
					{
						foreach (Type interfaceType in type.GetInterfaces())
						{
							if (ExpressionConverter.MemberAccessTranslator.GenericICollectionTranslator.IsICollection(interfaceType, out collectionElementType))
							{
								yield return new KeyValuePair<Type, Type>(interfaceType, collectionElementType);
							}
						}
					}
					yield break;
				}

				// Token: 0x04000168 RID: 360
				private readonly Type _elementType;
			}

			// Token: 0x020000A4 RID: 164
			internal sealed class DefaultCanonicalFunctionPropertyTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06000441 RID: 1089 RVA: 0x0001D701 File Offset: 0x0001B901
				internal DefaultCanonicalFunctionPropertyTranslator() : base(ExpressionConverter.MemberAccessTranslator.DefaultCanonicalFunctionPropertyTranslator.GetProperties())
				{
				}

				// Token: 0x06000442 RID: 1090 RVA: 0x0001D710 File Offset: 0x0001B910
				private static IEnumerable<PropertyInfo> GetProperties()
				{
					return new PropertyInfo[]
					{
						typeof(string).GetDeclaredProperty("Length"),
						typeof(DateTime).GetDeclaredProperty("Year"),
						typeof(DateTime).GetDeclaredProperty("Month"),
						typeof(DateTime).GetDeclaredProperty("Day"),
						typeof(DateTime).GetDeclaredProperty("Hour"),
						typeof(DateTime).GetDeclaredProperty("Minute"),
						typeof(DateTime).GetDeclaredProperty("Second"),
						typeof(DateTime).GetDeclaredProperty("Millisecond"),
						typeof(DateTimeOffset).GetDeclaredProperty("Year"),
						typeof(DateTimeOffset).GetDeclaredProperty("Month"),
						typeof(DateTimeOffset).GetDeclaredProperty("Day"),
						typeof(DateTimeOffset).GetDeclaredProperty("Hour"),
						typeof(DateTimeOffset).GetDeclaredProperty("Minute"),
						typeof(DateTimeOffset).GetDeclaredProperty("Second"),
						typeof(DateTimeOffset).GetDeclaredProperty("Millisecond")
					};
				}

				// Token: 0x06000443 RID: 1091 RVA: 0x0001D888 File Offset: 0x0001BA88
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return parent.TranslateIntoCanonicalFunction(call.Member.Name, call, new Expression[]
					{
						call.Expression
					});
				}
			}

			// Token: 0x020000A5 RID: 165
			internal sealed class RenameCanonicalFunctionPropertyTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06000444 RID: 1092 RVA: 0x0001D8B8 File Offset: 0x0001BAB8
				internal RenameCanonicalFunctionPropertyTranslator() : base(ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperties())
				{
				}

				// Token: 0x06000445 RID: 1093 RVA: 0x0001D8C8 File Offset: 0x0001BAC8
				private static IEnumerable<PropertyInfo> GetProperties()
				{
					return new PropertyInfo[]
					{
						ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(DateTime), "Now", "CurrentDateTime"),
						ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(DateTime), "UtcNow", "CurrentUtcDateTime"),
						ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(DateTimeOffset), "Now", "CurrentDateTimeOffset"),
						ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(TimeSpan), "Hours", "Hour"),
						ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(TimeSpan), "Minutes", "Minute"),
						ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(TimeSpan), "Seconds", "Second"),
						ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator.GetProperty(typeof(TimeSpan), "Milliseconds", "Millisecond")
					};
				}

				// Token: 0x06000446 RID: 1094 RVA: 0x0001D9A4 File Offset: 0x0001BBA4
				private static PropertyInfo GetProperty(Type declaringType, string propertyName, string canonicalFunctionName)
				{
					PropertyInfo declaredProperty = declaringType.GetDeclaredProperty(propertyName);
					ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator._propertyRenameMap[declaredProperty] = canonicalFunctionName;
					return declaredProperty;
				}

				// Token: 0x06000447 RID: 1095 RVA: 0x0001D9C8 File Offset: 0x0001BBC8
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					PropertyInfo key = (PropertyInfo)call.Member;
					string functionName = ExpressionConverter.MemberAccessTranslator.RenameCanonicalFunctionPropertyTranslator._propertyRenameMap[key];
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

				// Token: 0x04000169 RID: 361
				private static readonly Dictionary<PropertyInfo, string> _propertyRenameMap = new Dictionary<PropertyInfo, string>(2);
			}

			// Token: 0x020000A6 RID: 166
			internal sealed class VBDateAndTimeNowTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06000449 RID: 1097 RVA: 0x0001DA30 File Offset: 0x0001BC30
				internal VBDateAndTimeNowTranslator(Assembly vbAssembly) : base(new PropertyInfo[]
				{
					ExpressionConverter.MemberAccessTranslator.VBDateAndTimeNowTranslator.GetProperty(vbAssembly)
				})
				{
				}

				// Token: 0x0600044A RID: 1098 RVA: 0x0001DA54 File Offset: 0x0001BC54
				private static PropertyInfo GetProperty(Assembly vbAssembly)
				{
					return vbAssembly.GetType("Microsoft.VisualBasic.DateAndTime").GetDeclaredProperty("Now");
				}

				// Token: 0x0600044B RID: 1099 RVA: 0x0001DA6B File Offset: 0x0001BC6B
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return parent.TranslateIntoCanonicalFunction("CurrentDateTime", call, new Expression[0]);
				}

				// Token: 0x0400016A RID: 362
				private const string s_dateAndTimeTypeFullName = "Microsoft.VisualBasic.DateAndTime";
			}

			// Token: 0x020000A7 RID: 167
			internal sealed class EntityCollectionCountTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x0600044C RID: 1100 RVA: 0x0001DA80 File Offset: 0x0001BC80
				internal EntityCollectionCountTranslator() : base(new PropertyInfo[]
				{
					ExpressionConverter.MemberAccessTranslator.EntityCollectionCountTranslator.GetProperty()
				})
				{
				}

				// Token: 0x0600044D RID: 1101 RVA: 0x0001DAA3 File Offset: 0x0001BCA3
				private static PropertyInfo GetProperty()
				{
					return typeof(EntityCollection<>).GetDeclaredProperty("Count");
				}

				// Token: 0x0600044E RID: 1102 RVA: 0x0001DAB9 File Offset: 0x0001BCB9
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return ExpressionConverter.MemberAccessTranslator.TranslateCount(parent, call.Member.DeclaringType.GetGenericArguments()[0], call.Expression);
				}
			}

			// Token: 0x020000A8 RID: 168
			internal sealed class NullableHasValueTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x0600044F RID: 1103 RVA: 0x0001DADC File Offset: 0x0001BCDC
				internal NullableHasValueTranslator() : base(new PropertyInfo[]
				{
					ExpressionConverter.MemberAccessTranslator.NullableHasValueTranslator.GetProperty()
				})
				{
				}

				// Token: 0x06000450 RID: 1104 RVA: 0x0001DAFF File Offset: 0x0001BCFF
				private static PropertyInfo GetProperty()
				{
					return typeof(Nullable<>).GetDeclaredProperty("HasValue");
				}

				// Token: 0x06000451 RID: 1105 RVA: 0x0001DB18 File Offset: 0x0001BD18
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					DbExpression operand = parent.TranslateExpression(call.Expression);
					return ExpressionConverter.CreateIsNullExpression(operand, call.Expression.Type).Not();
				}
			}

			// Token: 0x020000A9 RID: 169
			internal sealed class NullableValueTranslator : ExpressionConverter.MemberAccessTranslator.PropertyTranslator
			{
				// Token: 0x06000452 RID: 1106 RVA: 0x0001DB48 File Offset: 0x0001BD48
				internal NullableValueTranslator() : base(new PropertyInfo[]
				{
					ExpressionConverter.MemberAccessTranslator.NullableValueTranslator.GetProperty()
				})
				{
				}

				// Token: 0x06000453 RID: 1107 RVA: 0x0001DB6B File Offset: 0x0001BD6B
				private static PropertyInfo GetProperty()
				{
					return typeof(Nullable<>).GetDeclaredProperty("Value");
				}

				// Token: 0x06000454 RID: 1108 RVA: 0x0001DB84 File Offset: 0x0001BD84
				internal override DbExpression Translate(ExpressionConverter parent, MemberExpression call)
				{
					return parent.TranslateExpression(call.Expression);
				}
			}
		}

		// Token: 0x020000AA RID: 170
		private sealed class ConstantTranslator : ExpressionConverter.TypedTranslator<ConstantExpression>
		{
			// Token: 0x06000455 RID: 1109 RVA: 0x0001DBA0 File Offset: 0x0001BDA0
			internal ConstantTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Constant
			})
			{
			}

			// Token: 0x06000456 RID: 1110 RVA: 0x0001DBC4 File Offset: 0x0001BDC4
			protected override DbExpression TypedTranslate(ExpressionConverter parent, ConstantExpression linq)
			{
				if (linq == parent._funcletizer.RootContextExpression)
				{
					throw new InvalidOperationException(Strings.ELinq_UnsupportedUseOfContextParameter(parent._funcletizer.RootContextParameter.Name));
				}
				ObjectQuery objectQuery = (linq.Value as IQueryable).TryGetObjectQuery();
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
				bool flag = null == linq.Value;
				bool flag2 = false;
				Type type = linq.Type;
				if (type == typeof(Enum))
				{
					type = linq.Value.GetType();
				}
				TypeUsage typeUsage;
				if (parent.TryGetValueLayerType(type, out typeUsage) && (Helper.IsScalarType(typeUsage.EdmType) || (flag && Helper.IsEntityType(typeUsage.EdmType))))
				{
					flag2 = true;
				}
				if (!flag2)
				{
					if (flag)
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedNullConstant(ExpressionConverter.DescribeClrType(linq.Type)));
					}
					throw new NotSupportedException(Strings.ELinq_UnsupportedConstant(ExpressionConverter.DescribeClrType(linq.Type)));
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
						Type nonNullableType = TypeSystem.GetNonNullableType(type);
						if (nonNullableType.IsEnum())
						{
							value2 = System.Convert.ChangeType(linq.Value, nonNullableType.GetEnumUnderlyingType(), CultureInfo.InvariantCulture);
						}
					}
					return typeUsage.Constant(value2);
				}
			}
		}

		// Token: 0x020000AB RID: 171
		private sealed class ParameterTranslator : ExpressionConverter.TypedTranslator<ParameterExpression>
		{
			// Token: 0x06000458 RID: 1112 RVA: 0x0001DDC8 File Offset: 0x0001BFC8
			internal ParameterTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Parameter
			})
			{
			}

			// Token: 0x06000459 RID: 1113 RVA: 0x0001DDE8 File Offset: 0x0001BFE8
			protected override DbExpression TypedTranslate(ExpressionConverter parent, ParameterExpression linq)
			{
				throw new InvalidOperationException(Strings.ELinq_UnboundParameterExpression(linq.Name));
			}
		}

		// Token: 0x020000AC RID: 172
		private sealed class NewTranslator : ExpressionConverter.TypedTranslator<NewExpression>
		{
			// Token: 0x0600045A RID: 1114 RVA: 0x0001DDFC File Offset: 0x0001BFFC
			internal NewTranslator() : base(new ExpressionType[]
			{
				ExpressionType.New
			})
			{
			}

			// Token: 0x0600045B RID: 1115 RVA: 0x0001DE1C File Offset: 0x0001C01C
			protected override DbExpression TypedTranslate(ExpressionConverter parent, NewExpression linq)
			{
				int num = (linq.Members == null) ? 0 : linq.Members.Count;
				if (null == linq.Constructor || linq.Arguments.Count != num)
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedConstructor);
				}
				parent.CheckInitializerType(linq.Type);
				List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>(num + 1);
				HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
				for (int i = 0; i < num; i++)
				{
					string text;
					Type type;
					TypeSystem.PropertyOrField(linq.Members[i], out text, out type);
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
				return ExpressionConverter.CreateNewRowExpression(list, initializerMetadata);
			}
		}

		// Token: 0x020000AD RID: 173
		private sealed class NewArrayInitTranslator : ExpressionConverter.TypedTranslator<NewArrayExpression>
		{
			// Token: 0x0600045C RID: 1116 RVA: 0x0001DF20 File Offset: 0x0001C120
			internal NewArrayInitTranslator() : base(new ExpressionType[]
			{
				ExpressionType.NewArrayInit
			})
			{
			}

			// Token: 0x0600045D RID: 1117 RVA: 0x0001DF58 File Offset: 0x0001C158
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
				throw new NotSupportedException(Strings.ELinq_UnsupportedType(ExpressionConverter.DescribeClrType(linq.Type)));
			}
		}

		// Token: 0x020000AE RID: 174
		private sealed class ListInitTranslator : ExpressionConverter.TypedTranslator<ListInitExpression>
		{
			// Token: 0x0600045E RID: 1118 RVA: 0x0001E014 File Offset: 0x0001C214
			internal ListInitTranslator() : base(new ExpressionType[]
			{
				ExpressionType.ListInit
			})
			{
			}

			// Token: 0x0600045F RID: 1119 RVA: 0x0001E068 File Offset: 0x0001C268
			protected override DbExpression TypedTranslate(ExpressionConverter parent, ListInitExpression linq)
			{
				if (linq.NewExpression.Constructor != null && linq.NewExpression.Constructor.GetParameters().Length != 0)
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedConstructor);
				}
				if (linq.Initializers.Any((ElementInit i) => i.Arguments.Count != 1))
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedInitializers);
				}
				return DbExpressionBuilder.NewCollection(from i in linq.Initializers
				select parent.TranslateExpression(i.Arguments[0]));
			}
		}

		// Token: 0x020000AF RID: 175
		private sealed class MemberInitTranslator : ExpressionConverter.TypedTranslator<MemberInitExpression>
		{
			// Token: 0x06000461 RID: 1121 RVA: 0x0001E108 File Offset: 0x0001C308
			internal MemberInitTranslator() : base(new ExpressionType[]
			{
				ExpressionType.MemberInit
			})
			{
			}

			// Token: 0x06000462 RID: 1122 RVA: 0x0001E128 File Offset: 0x0001C328
			protected override DbExpression TypedTranslate(ExpressionConverter parent, MemberInitExpression linq)
			{
				if (null == linq.NewExpression.Constructor || linq.NewExpression.Constructor.GetParameters().Length != 0)
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedConstructor);
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
						throw new NotSupportedException(Strings.ELinq_UnsupportedBinding);
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
					initializerMetadata = InitializerMetadata.CreateProjectionInitializer(parent.EdmItemCollection, linq);
				}
				parent.ValidateInitializerMetadata(initializerMetadata);
				return ExpressionConverter.CreateNewRowExpression(list, initializerMetadata);
			}
		}

		// Token: 0x020000B0 RID: 176
		private sealed class ConditionalTranslator : ExpressionConverter.TypedTranslator<ConditionalExpression>
		{
			// Token: 0x06000463 RID: 1123 RVA: 0x0001E274 File Offset: 0x0001C474
			internal ConditionalTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Conditional
			})
			{
			}

			// Token: 0x06000464 RID: 1124 RVA: 0x0001E294 File Offset: 0x0001C494
			protected override DbExpression TypedTranslate(ExpressionConverter parent, ConditionalExpression linq)
			{
				DbExpression item = parent.TranslateExpression(linq.Test);
				DbExpression dbExpression;
				DbExpression dbExpression2;
				if (!linq.IfTrue.IsNullConstant())
				{
					dbExpression = parent.TranslateExpression(linq.IfTrue);
					dbExpression2 = ((!linq.IfFalse.IsNullConstant()) ? parent.TranslateExpression(linq.IfFalse) : dbExpression.ResultType.Null());
				}
				else
				{
					if (linq.IfFalse.IsNullConstant())
					{
						throw new NotSupportedException(Strings.ELinq_UnsupportedNullConstant(ExpressionConverter.DescribeClrType(linq.Type)));
					}
					dbExpression2 = parent.TranslateExpression(linq.IfFalse);
					dbExpression = dbExpression2.ResultType.Null();
				}
				return DbExpressionBuilder.Case(new List<DbExpression>
				{
					item
				}, new List<DbExpression>
				{
					dbExpression
				}, dbExpression2);
			}
		}

		// Token: 0x020000B1 RID: 177
		private sealed class NotSupportedTranslator : ExpressionConverter.Translator
		{
			// Token: 0x06000465 RID: 1125 RVA: 0x0001E354 File Offset: 0x0001C554
			internal NotSupportedTranslator(params ExpressionType[] nodeTypes) : base(nodeTypes)
			{
			}

			// Token: 0x06000466 RID: 1126 RVA: 0x0001E35D File Offset: 0x0001C55D
			internal override DbExpression Translate(ExpressionConverter parent, Expression linq)
			{
				throw new NotSupportedException(Strings.ELinq_UnsupportedExpressionType(linq.NodeType));
			}
		}

		// Token: 0x020000B2 RID: 178
		private sealed class ExtensionTranslator : ExpressionConverter.Translator
		{
			// Token: 0x06000467 RID: 1127 RVA: 0x0001E374 File Offset: 0x0001C574
			internal ExtensionTranslator() : base(new ExpressionType[]
			{
				(ExpressionType)(-1)
			})
			{
			}

			// Token: 0x06000468 RID: 1128 RVA: 0x0001E394 File Offset: 0x0001C594
			internal override DbExpression Translate(ExpressionConverter parent, Expression linq)
			{
				QueryParameterExpression queryParameterExpression = linq as QueryParameterExpression;
				if (queryParameterExpression == null)
				{
					throw new NotSupportedException(Strings.ELinq_UnsupportedExpressionType(linq.NodeType));
				}
				parent.AddParameter(queryParameterExpression);
				return queryParameterExpression.ParameterReference;
			}
		}

		// Token: 0x020000B3 RID: 179
		private abstract class BinaryTranslator : ExpressionConverter.TypedTranslator<BinaryExpression>
		{
			// Token: 0x06000469 RID: 1129 RVA: 0x0001E3CE File Offset: 0x0001C5CE
			protected BinaryTranslator(params ExpressionType[] nodeTypes) : base(nodeTypes)
			{
			}

			// Token: 0x0600046A RID: 1130 RVA: 0x0001E3D7 File Offset: 0x0001C5D7
			protected override DbExpression TypedTranslate(ExpressionConverter parent, BinaryExpression linq)
			{
				return this.TranslateBinary(parent, parent.TranslateExpression(linq.Left), parent.TranslateExpression(linq.Right), linq);
			}

			// Token: 0x0600046B RID: 1131
			protected abstract DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq);
		}

		// Token: 0x020000B4 RID: 180
		private sealed class CoalesceTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x0600046C RID: 1132 RVA: 0x0001E3FC File Offset: 0x0001C5FC
			internal CoalesceTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Coalesce
			})
			{
			}

			// Token: 0x0600046D RID: 1133 RVA: 0x0001E41C File Offset: 0x0001C61C
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				DbExpression item = ExpressionConverter.CreateIsNullExpression(left, linq.Left.Type);
				return DbExpressionBuilder.Case(new List<DbExpression>(1)
				{
					item
				}, new List<DbExpression>(1)
				{
					right
				}, left);
			}
		}

		// Token: 0x020000B5 RID: 181
		private sealed class AndAlsoTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x0600046E RID: 1134 RVA: 0x0001E464 File Offset: 0x0001C664
			internal AndAlsoTranslator() : base(new ExpressionType[]
			{
				ExpressionType.AndAlso
			})
			{
			}

			// Token: 0x0600046F RID: 1135 RVA: 0x0001E483 File Offset: 0x0001C683
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.And(right);
			}
		}

		// Token: 0x020000B6 RID: 182
		private sealed class OrElseTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06000470 RID: 1136 RVA: 0x0001E48C File Offset: 0x0001C68C
			internal OrElseTranslator() : base(new ExpressionType[]
			{
				ExpressionType.OrElse
			})
			{
			}

			// Token: 0x06000471 RID: 1137 RVA: 0x0001E4AC File Offset: 0x0001C6AC
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Or(right);
			}
		}

		// Token: 0x020000B7 RID: 183
		private sealed class LessThanTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06000472 RID: 1138 RVA: 0x0001E4B8 File Offset: 0x0001C6B8
			internal LessThanTranslator() : base(new ExpressionType[]
			{
				ExpressionType.LessThan
			})
			{
			}

			// Token: 0x06000473 RID: 1139 RVA: 0x0001E4D8 File Offset: 0x0001C6D8
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.LessThan(right);
			}
		}

		// Token: 0x020000B8 RID: 184
		private sealed class LessThanOrEqualsTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06000474 RID: 1140 RVA: 0x0001E4E4 File Offset: 0x0001C6E4
			internal LessThanOrEqualsTranslator() : base(new ExpressionType[]
			{
				ExpressionType.LessThanOrEqual
			})
			{
			}

			// Token: 0x06000475 RID: 1141 RVA: 0x0001E504 File Offset: 0x0001C704
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.LessThanOrEqual(right);
			}
		}

		// Token: 0x020000B9 RID: 185
		private sealed class GreaterThanTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06000476 RID: 1142 RVA: 0x0001E510 File Offset: 0x0001C710
			internal GreaterThanTranslator() : base(new ExpressionType[]
			{
				ExpressionType.GreaterThan
			})
			{
			}

			// Token: 0x06000477 RID: 1143 RVA: 0x0001E530 File Offset: 0x0001C730
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.GreaterThan(right);
			}
		}

		// Token: 0x020000BA RID: 186
		private sealed class GreaterThanOrEqualsTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06000478 RID: 1144 RVA: 0x0001E53C File Offset: 0x0001C73C
			internal GreaterThanOrEqualsTranslator() : base(new ExpressionType[]
			{
				ExpressionType.GreaterThanOrEqual
			})
			{
			}

			// Token: 0x06000479 RID: 1145 RVA: 0x0001E55C File Offset: 0x0001C75C
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.GreaterThanOrEqual(right);
			}
		}

		// Token: 0x020000BB RID: 187
		private sealed class EqualsTranslator : ExpressionConverter.TypedTranslator<BinaryExpression>
		{
			// Token: 0x0600047A RID: 1146 RVA: 0x0001E568 File Offset: 0x0001C768
			internal EqualsTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Equal
			})
			{
			}

			// Token: 0x0600047B RID: 1147 RVA: 0x0001E588 File Offset: 0x0001C788
			protected override DbExpression TypedTranslate(ExpressionConverter parent, BinaryExpression linq)
			{
				Expression left = linq.Left;
				Expression right = linq.Right;
				bool flag = left.IsNullConstant();
				bool flag2 = right.IsNullConstant();
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

			// Token: 0x0600047C RID: 1148 RVA: 0x0001E61C File Offset: 0x0001C81C
			private static DbExpression CreateIsNullExpression(ExpressionConverter parent, Expression input)
			{
				input = input.RemoveConvert();
				DbExpression operand = parent.TranslateExpression(input);
				return ExpressionConverter.CreateIsNullExpression(operand, input.Type);
			}
		}

		// Token: 0x020000BC RID: 188
		private sealed class NotEqualsTranslator : ExpressionConverter.TypedTranslator<BinaryExpression>
		{
			// Token: 0x0600047D RID: 1149 RVA: 0x0001E648 File Offset: 0x0001C848
			internal NotEqualsTranslator() : base(new ExpressionType[]
			{
				ExpressionType.NotEqual
			})
			{
			}

			// Token: 0x0600047E RID: 1150 RVA: 0x0001E668 File Offset: 0x0001C868
			protected override DbExpression TypedTranslate(ExpressionConverter parent, BinaryExpression linq)
			{
				Expression linq2 = Expression.Not(Expression.Equal(linq.Left, linq.Right));
				return parent.TranslateExpression(linq2);
			}
		}

		// Token: 0x020000BD RID: 189
		private sealed class IsTranslator : ExpressionConverter.TypedTranslator<TypeBinaryExpression>
		{
			// Token: 0x0600047F RID: 1151 RVA: 0x0001E694 File Offset: 0x0001C894
			internal IsTranslator() : base(new ExpressionType[]
			{
				ExpressionType.TypeIs
			})
			{
			}

			// Token: 0x06000480 RID: 1152 RVA: 0x0001E6B4 File Offset: 0x0001C8B4
			protected override DbExpression TypedTranslate(ExpressionConverter parent, TypeBinaryExpression linq)
			{
				DbExpression dbExpression = parent.TranslateExpression(linq.Expression);
				TypeUsage resultType = dbExpression.ResultType;
				TypeUsage isOrAsTargetType = parent.GetIsOrAsTargetType(ExpressionType.TypeIs, linq.TypeOperand, linq.Expression.Type);
				return dbExpression.IsOf(isOrAsTargetType);
			}
		}

		// Token: 0x020000BE RID: 190
		private sealed class AddTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06000481 RID: 1153 RVA: 0x0001E6F8 File Offset: 0x0001C8F8
			internal AddTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Add,
				ExpressionType.AddChecked
			})
			{
			}

			// Token: 0x06000482 RID: 1154 RVA: 0x0001E717 File Offset: 0x0001C917
			protected override DbExpression TypedTranslate(ExpressionConverter parent, BinaryExpression linq)
			{
				if (linq.IsStringAddExpression())
				{
					return ExpressionConverter.StringTranslatorUtil.ConcatArgs(parent, linq);
				}
				return this.TranslateBinary(parent, parent.TranslateExpression(linq.Left), parent.TranslateExpression(linq.Right), linq);
			}

			// Token: 0x06000483 RID: 1155 RVA: 0x0001E749 File Offset: 0x0001C949
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Plus(right);
			}
		}

		// Token: 0x020000BF RID: 191
		private sealed class DivideTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06000484 RID: 1156 RVA: 0x0001E754 File Offset: 0x0001C954
			internal DivideTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Divide
			})
			{
			}

			// Token: 0x06000485 RID: 1157 RVA: 0x0001E774 File Offset: 0x0001C974
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Divide(right);
			}
		}

		// Token: 0x020000C0 RID: 192
		private sealed class ModuloTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06000486 RID: 1158 RVA: 0x0001E780 File Offset: 0x0001C980
			internal ModuloTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Modulo
			})
			{
			}

			// Token: 0x06000487 RID: 1159 RVA: 0x0001E7A0 File Offset: 0x0001C9A0
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Modulo(right);
			}
		}

		// Token: 0x020000C1 RID: 193
		private sealed class MultiplyTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x06000488 RID: 1160 RVA: 0x0001E7AC File Offset: 0x0001C9AC
			internal MultiplyTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Multiply,
				ExpressionType.MultiplyChecked
			})
			{
			}

			// Token: 0x06000489 RID: 1161 RVA: 0x0001E7D1 File Offset: 0x0001C9D1
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Multiply(right);
			}
		}

		// Token: 0x020000C2 RID: 194
		private sealed class PowerTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x0600048A RID: 1162 RVA: 0x0001E7DC File Offset: 0x0001C9DC
			internal PowerTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Power
			})
			{
			}

			// Token: 0x0600048B RID: 1163 RVA: 0x0001E7FC File Offset: 0x0001C9FC
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Power(right);
			}
		}

		// Token: 0x020000C3 RID: 195
		private sealed class SubtractTranslator : ExpressionConverter.BinaryTranslator
		{
			// Token: 0x0600048C RID: 1164 RVA: 0x0001E808 File Offset: 0x0001CA08
			internal SubtractTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Subtract,
				ExpressionType.SubtractChecked
			})
			{
			}

			// Token: 0x0600048D RID: 1165 RVA: 0x0001E82D File Offset: 0x0001CA2D
			protected override DbExpression TranslateBinary(ExpressionConverter parent, DbExpression left, DbExpression right, BinaryExpression linq)
			{
				return left.Minus(right);
			}
		}

		// Token: 0x020000C4 RID: 196
		private abstract class UnaryTranslator : ExpressionConverter.TypedTranslator<UnaryExpression>
		{
			// Token: 0x0600048E RID: 1166 RVA: 0x0001E836 File Offset: 0x0001CA36
			protected UnaryTranslator(params ExpressionType[] nodeTypes) : base(nodeTypes)
			{
			}

			// Token: 0x0600048F RID: 1167 RVA: 0x0001E83F File Offset: 0x0001CA3F
			protected override DbExpression TypedTranslate(ExpressionConverter parent, UnaryExpression linq)
			{
				return this.TranslateUnary(parent, linq, parent.TranslateExpression(linq.Operand));
			}

			// Token: 0x06000490 RID: 1168
			protected abstract DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand);
		}

		// Token: 0x020000C5 RID: 197
		private sealed class NegateTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x06000491 RID: 1169 RVA: 0x0001E858 File Offset: 0x0001CA58
			internal NegateTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Negate,
				ExpressionType.NegateChecked
			})
			{
			}

			// Token: 0x06000492 RID: 1170 RVA: 0x0001E87D File Offset: 0x0001CA7D
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				return operand.UnaryMinus();
			}
		}

		// Token: 0x020000C6 RID: 198
		private sealed class UnaryPlusTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x06000493 RID: 1171 RVA: 0x0001E888 File Offset: 0x0001CA88
			internal UnaryPlusTranslator() : base(new ExpressionType[]
			{
				ExpressionType.UnaryPlus
			})
			{
			}

			// Token: 0x06000494 RID: 1172 RVA: 0x0001E8A8 File Offset: 0x0001CAA8
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				return operand;
			}
		}

		// Token: 0x020000C7 RID: 199
		private abstract class BitwiseBinaryTranslator : ExpressionConverter.TypedTranslator<BinaryExpression>
		{
			// Token: 0x06000495 RID: 1173 RVA: 0x0001E8AC File Offset: 0x0001CAAC
			protected BitwiseBinaryTranslator(ExpressionType nodeType, string canonicalFunctionName) : base(new ExpressionType[]
			{
				nodeType
			})
			{
				this._canonicalFunctionName = canonicalFunctionName;
			}

			// Token: 0x06000496 RID: 1174 RVA: 0x0001E8D4 File Offset: 0x0001CAD4
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

			// Token: 0x06000497 RID: 1175
			protected abstract DbExpression TranslateIntoLogicExpression(ExpressionConverter parent, BinaryExpression linq, DbExpression left, DbExpression right);

			// Token: 0x0400016D RID: 365
			private readonly string _canonicalFunctionName;
		}

		// Token: 0x020000C8 RID: 200
		private sealed class AndTranslator : ExpressionConverter.BitwiseBinaryTranslator
		{
			// Token: 0x06000498 RID: 1176 RVA: 0x0001E930 File Offset: 0x0001CB30
			internal AndTranslator() : base(ExpressionType.And, "BitwiseAnd")
			{
			}

			// Token: 0x06000499 RID: 1177 RVA: 0x0001E93E File Offset: 0x0001CB3E
			protected override DbExpression TranslateIntoLogicExpression(ExpressionConverter parent, BinaryExpression linq, DbExpression left, DbExpression right)
			{
				return left.And(right);
			}
		}

		// Token: 0x020000C9 RID: 201
		private sealed class OrTranslator : ExpressionConverter.BitwiseBinaryTranslator
		{
			// Token: 0x0600049A RID: 1178 RVA: 0x0001E948 File Offset: 0x0001CB48
			internal OrTranslator() : base(ExpressionType.Or, "BitwiseOr")
			{
			}

			// Token: 0x0600049B RID: 1179 RVA: 0x0001E957 File Offset: 0x0001CB57
			protected override DbExpression TranslateIntoLogicExpression(ExpressionConverter parent, BinaryExpression linq, DbExpression left, DbExpression right)
			{
				return left.Or(right);
			}
		}

		// Token: 0x020000CA RID: 202
		private sealed class ExclusiveOrTranslator : ExpressionConverter.BitwiseBinaryTranslator
		{
			// Token: 0x0600049C RID: 1180 RVA: 0x0001E961 File Offset: 0x0001CB61
			internal ExclusiveOrTranslator() : base(ExpressionType.ExclusiveOr, "BitwiseXor")
			{
			}

			// Token: 0x0600049D RID: 1181 RVA: 0x0001E970 File Offset: 0x0001CB70
			protected override DbExpression TranslateIntoLogicExpression(ExpressionConverter parent, BinaryExpression linq, DbExpression left, DbExpression right)
			{
				DbExpression left2 = left.And(right.Not());
				DbExpression right2 = left.Not().And(right);
				return left2.Or(right2);
			}
		}

		// Token: 0x020000CB RID: 203
		private sealed class NotTranslator : ExpressionConverter.TypedTranslator<UnaryExpression>
		{
			// Token: 0x0600049E RID: 1182 RVA: 0x0001E9A4 File Offset: 0x0001CBA4
			internal NotTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Not
			})
			{
			}

			// Token: 0x0600049F RID: 1183 RVA: 0x0001E9C4 File Offset: 0x0001CBC4
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

		// Token: 0x020000CC RID: 204
		private sealed class QuoteTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x060004A0 RID: 1184 RVA: 0x0001EA0C File Offset: 0x0001CC0C
			internal QuoteTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Quote
			})
			{
			}

			// Token: 0x060004A1 RID: 1185 RVA: 0x0001EA2C File Offset: 0x0001CC2C
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				return operand;
			}
		}

		// Token: 0x020000CD RID: 205
		private sealed class ConvertTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x060004A2 RID: 1186 RVA: 0x0001EA30 File Offset: 0x0001CC30
			internal ConvertTranslator() : base(new ExpressionType[]
			{
				ExpressionType.Convert,
				ExpressionType.ConvertChecked
			})
			{
			}

			// Token: 0x060004A3 RID: 1187 RVA: 0x0001EA58 File Offset: 0x0001CC58
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				Type type = unary.Type;
				Type type2 = unary.Operand.Type;
				return parent.CreateCastExpression(operand, type, type2);
			}
		}

		// Token: 0x020000CE RID: 206
		private sealed class AsTranslator : ExpressionConverter.UnaryTranslator
		{
			// Token: 0x060004A4 RID: 1188 RVA: 0x0001EA84 File Offset: 0x0001CC84
			internal AsTranslator() : base(new ExpressionType[]
			{
				ExpressionType.TypeAs
			})
			{
			}

			// Token: 0x060004A5 RID: 1189 RVA: 0x0001EAA4 File Offset: 0x0001CCA4
			protected override DbExpression TranslateUnary(ExpressionConverter parent, UnaryExpression unary, DbExpression operand)
			{
				TypeUsage resultType = operand.ResultType;
				TypeUsage isOrAsTargetType = parent.GetIsOrAsTargetType(ExpressionType.TypeAs, unary.Type, unary.Operand.Type);
				return operand.TreatAs(isOrAsTargetType);
			}
		}
	}
}
