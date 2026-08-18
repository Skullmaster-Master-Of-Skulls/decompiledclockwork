using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.QueryCache;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020002EE RID: 750
	internal class Translator
	{
		// Token: 0x06001A6F RID: 6767 RVA: 0x000833F0 File Offset: 0x000815F0
		internal virtual ShaperFactory<T> TranslateColumnMap<T>(ColumnMap columnMap, MetadataWorkspace workspace, SpanIndex spanIndex, MergeOption mergeOption, bool streaming, bool valueLayer)
		{
			string columnMapKey = ColumnMapKeyBuilder.GetColumnMapKey(columnMap, spanIndex);
			ShaperFactoryQueryCacheKey<T> shaperFactoryQueryCacheKey = new ShaperFactoryQueryCacheKey<T>(columnMapKey, mergeOption, streaming, valueLayer);
			QueryCacheManager queryCacheManager = workspace.GetQueryCacheManager();
			ShaperFactory<T> shaperFactory;
			if (queryCacheManager.TryCacheLookup<ShaperFactoryQueryCacheKey<T>, ShaperFactory<T>>(shaperFactoryQueryCacheKey, out shaperFactory))
			{
				return shaperFactory;
			}
			Translator.TranslatorVisitor translatorVisitor = new Translator.TranslatorVisitor(workspace, spanIndex, mergeOption, streaming, valueLayer);
			columnMap.Accept<TranslatorResult, TranslatorArg>(translatorVisitor, new TranslatorArg(typeof(IEnumerable<>).MakeGenericType(new Type[]
			{
				typeof(T)
			})));
			CoordinatorFactory<T> rootCoordinatorFactory = (CoordinatorFactory<T>)translatorVisitor.RootCoordinatorScratchpad.Compile();
			Type[] array = null;
			bool[] array2 = null;
			if (!streaming)
			{
				int num = Math.Max(translatorVisitor.ColumnTypes.Any<KeyValuePair<int, Type>>() ? translatorVisitor.ColumnTypes.Keys.Max() : 0, translatorVisitor.NullableColumns.Any<int>() ? translatorVisitor.NullableColumns.Max() : 0);
				array = new Type[num + 1];
				foreach (KeyValuePair<int, Type> keyValuePair in translatorVisitor.ColumnTypes)
				{
					array[keyValuePair.Key] = keyValuePair.Value;
				}
				array2 = new bool[num + 1];
				foreach (int num2 in translatorVisitor.NullableColumns)
				{
					array2[num2] = true;
				}
			}
			shaperFactory = new ShaperFactory<T>(translatorVisitor.StateSlotCount, rootCoordinatorFactory, array, array2, mergeOption);
			QueryCacheEntry queryCacheEntry = new QueryCacheEntry(shaperFactoryQueryCacheKey, shaperFactory);
			if (queryCacheManager.TryLookupAndAdd(queryCacheEntry, out queryCacheEntry))
			{
				shaperFactory = (ShaperFactory<T>)queryCacheEntry.GetTarget();
			}
			return shaperFactory;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000835B8 File Offset: 0x000817B8
		internal static ShaperFactory TranslateColumnMap(Translator translator, Type elementType, ColumnMap columnMap, MetadataWorkspace workspace, SpanIndex spanIndex, MergeOption mergeOption, bool streaming, bool valueLayer)
		{
			MethodInfo methodInfo = Translator.GenericTranslateColumnMap.MakeGenericMethod(new Type[]
			{
				elementType
			});
			return (ShaperFactory)methodInfo.Invoke(translator, new object[]
			{
				columnMap,
				workspace,
				spanIndex,
				mergeOption,
				streaming,
				valueLayer
			});
		}

		// Token: 0x04000924 RID: 2340
		public static readonly MethodInfo GenericTranslateColumnMap = typeof(Translator).GetDeclaredMethod("TranslateColumnMap", new Type[]
		{
			typeof(ColumnMap),
			typeof(MetadataWorkspace),
			typeof(SpanIndex),
			typeof(MergeOption),
			typeof(bool),
			typeof(bool)
		});

		// Token: 0x020002F0 RID: 752
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		internal class TranslatorVisitor : ColumnMapVisitorWithResults<TranslatorResult, TranslatorArg>
		{
			// Token: 0x06001A81 RID: 6785 RVA: 0x000836DC File Offset: 0x000818DC
			public TranslatorVisitor(MetadataWorkspace workspace, SpanIndex spanIndex, MergeOption mergeOption, bool streaming, bool valueLayer)
			{
				this._workspace = workspace;
				this._spanIndex = spanIndex;
				this._mergeOption = mergeOption;
				this._streaming = streaming;
				this.ColumnTypes = new Dictionary<int, Type>();
				this.NullableColumns = new Set<int>();
				this.IsValueLayer = valueLayer;
			}

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x06001A82 RID: 6786 RVA: 0x00083735 File Offset: 0x00081935
			// (set) Token: 0x06001A83 RID: 6787 RVA: 0x0008373D File Offset: 0x0008193D
			public CoordinatorScratchpad RootCoordinatorScratchpad { get; private set; }

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06001A84 RID: 6788 RVA: 0x00083746 File Offset: 0x00081946
			// (set) Token: 0x06001A85 RID: 6789 RVA: 0x0008374E File Offset: 0x0008194E
			public int StateSlotCount { get; private set; }

			// Token: 0x170002F5 RID: 757
			// (get) Token: 0x06001A86 RID: 6790 RVA: 0x00083757 File Offset: 0x00081957
			// (set) Token: 0x06001A87 RID: 6791 RVA: 0x0008375F File Offset: 0x0008195F
			public Dictionary<int, Type> ColumnTypes { get; private set; }

			// Token: 0x170002F6 RID: 758
			// (get) Token: 0x06001A88 RID: 6792 RVA: 0x00083768 File Offset: 0x00081968
			// (set) Token: 0x06001A89 RID: 6793 RVA: 0x00083770 File Offset: 0x00081970
			public Set<int> NullableColumns { get; private set; }

			// Token: 0x06001A8A RID: 6794 RVA: 0x0008377C File Offset: 0x0008197C
			private static TranslatorResult AcceptWithMappedType(Translator.TranslatorVisitor translatorVisitor, ColumnMap columnMap)
			{
				Type requestedType = translatorVisitor.DetermineClrType(columnMap.Type);
				return columnMap.Accept<TranslatorResult, TranslatorArg>(translatorVisitor, new TranslatorArg(requestedType));
			}

			// Token: 0x06001A8B RID: 6795 RVA: 0x000837A8 File Offset: 0x000819A8
			internal override TranslatorResult Visit(ComplexTypeColumnMap columnMap, TranslatorArg arg)
			{
				Expression expression = null;
				bool inNullableType = this._inNullableType;
				if (columnMap.NullSentinel != null)
				{
					expression = CodeGenEmitter.Emit_Reader_IsDBNull(columnMap.NullSentinel);
					this._inNullableType = true;
					int columnPos = ((ScalarColumnMap)columnMap.NullSentinel).ColumnPos;
					if (!this._streaming && !this.NullableColumns.Contains(columnPos))
					{
						this.NullableColumns.Add(columnPos);
					}
				}
				Expression expression2;
				if (this.IsValueLayer)
				{
					expression2 = this.BuildExpressionToGetRecordState(columnMap, null, null, expression);
				}
				else
				{
					ComplexType complexType = (ComplexType)columnMap.Type.EdmType;
					Type type = this.DetermineClrType(complexType);
					ConstructorInfo constructorForType = DelegateFactory.GetConstructorForType(type);
					List<MemberBinding> bindings = this.CreatePropertyBindings(columnMap, complexType.Properties);
					expression2 = Expression.MemberInit(Expression.New(constructorForType), bindings);
					if (expression != null)
					{
						expression2 = Expression.Condition(expression, CodeGenEmitter.Emit_NullConstant(expression2.Type), expression2);
					}
				}
				this._inNullableType = inNullableType;
				return new TranslatorResult(expression2, arg.RequestedType);
			}

			// Token: 0x06001A8C RID: 6796 RVA: 0x00083894 File Offset: 0x00081A94
			internal override TranslatorResult Visit(EntityColumnMap columnMap, TranslatorArg arg)
			{
				EntityIdentity entityIdentity = columnMap.EntityIdentity;
				Expression expression = null;
				Expression expression2 = this.Emit_EntityKey_ctor(this, entityIdentity, columnMap.Type.EdmType, false, out expression);
				Expression returnedExpression;
				if (this.IsValueLayer)
				{
					Expression nullCheckExpression = Expression.Not(CodeGenEmitter.Emit_EntityKey_HasValue(entityIdentity.Keys));
					returnedExpression = this.BuildExpressionToGetRecordState(columnMap, expression2, expression, nullCheckExpression);
				}
				else
				{
					EntityType entityType = (EntityType)columnMap.Type.EdmType;
					ClrEntityType clrEntityType = (ClrEntityType)this.LookupObjectMapping(entityType).ClrType;
					Type clrType = clrEntityType.ClrType;
					List<MemberBinding> propertyBindings = this.CreatePropertyBindings(columnMap, entityType.Properties);
					EntityProxyTypeInfo proxyType = EntityProxyFactory.GetProxyType(clrEntityType, this._workspace);
					Expression expression3 = this.Emit_ConstructEntity(clrEntityType, propertyBindings, expression2, expression, arg, null);
					Expression expression4;
					if (proxyType == null)
					{
						expression4 = expression3;
					}
					else
					{
						Expression ifTrue = this.Emit_ConstructEntity(clrEntityType, propertyBindings, expression2, expression, arg, proxyType);
						expression4 = Expression.Condition(CodeGenEmitter.Shaper_ProxyCreationEnabled, ifTrue, expression3);
					}
					if (MergeOption.NoTracking != this._mergeOption)
					{
						Type c = (proxyType == null) ? clrType : proxyType.ProxyType;
						if (typeof(IEntityWithKey).IsAssignableFrom(c) && this._mergeOption != MergeOption.AppendOnly)
						{
							expression4 = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_HandleIEntityWithKey.MakeGenericMethod(new Type[]
							{
								clrType
							}), expression4, expression);
						}
						else if (this._mergeOption == MergeOption.AppendOnly)
						{
							LambdaExpression arg2 = this.CreateInlineDelegate(expression4);
							expression4 = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_HandleEntityAppendOnly.MakeGenericMethod(new Type[]
							{
								clrType
							}), arg2, expression2, expression);
						}
						else
						{
							expression4 = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_HandleEntity.MakeGenericMethod(new Type[]
							{
								clrType
							}), expression4, expression2, expression);
						}
					}
					else
					{
						expression4 = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_HandleEntityNoTracking.MakeGenericMethod(new Type[]
						{
							clrType
						}), new Expression[]
						{
							expression4
						});
					}
					returnedExpression = Expression.Condition(CodeGenEmitter.Emit_EntityKey_HasValue(entityIdentity.Keys), expression4, CodeGenEmitter.Emit_WrappedNullConstant());
				}
				int columnPos = ((ScalarColumnMap)entityIdentity.Keys[0]).ColumnPos;
				if (!this._streaming && !this.NullableColumns.Contains(columnPos))
				{
					this.NullableColumns.Add(columnPos);
				}
				return new TranslatorResult(returnedExpression, arg.RequestedType);
			}

			// Token: 0x06001A8D RID: 6797 RVA: 0x00083ADC File Offset: 0x00081CDC
			private Expression Emit_ConstructEntity(EntityType oSpaceType, IEnumerable<MemberBinding> propertyBindings, Expression entityKeyReader, Expression entitySetReader, TranslatorArg arg, EntityProxyTypeInfo proxyTypeInfo)
			{
				bool flag = proxyTypeInfo != null;
				Type clrType = oSpaceType.ClrType;
				Expression expression;
				Type actualType;
				if (flag)
				{
					expression = Expression.MemberInit(Expression.New(proxyTypeInfo.ProxyType), propertyBindings);
					actualType = proxyTypeInfo.ProxyType;
				}
				else
				{
					ConstructorInfo constructorForType = DelegateFactory.GetConstructorForType(clrType);
					expression = Expression.MemberInit(Expression.New(constructorForType), propertyBindings);
					actualType = clrType;
				}
				expression = CodeGenEmitter.Emit_EnsureTypeAndWrap(expression, entityKeyReader, entitySetReader, arg.RequestedType, clrType, actualType, (this._mergeOption == MergeOption.NoTracking) ? MergeOption.NoTracking : MergeOption.AppendOnly, flag);
				if (flag)
				{
					expression = Expression.Call(Expression.Constant(proxyTypeInfo), CodeGenEmitter.EntityProxyTypeInfo_SetEntityWrapper, new Expression[]
					{
						expression
					});
					if (proxyTypeInfo.InitializeEntityCollections != null)
					{
						expression = Expression.Call(proxyTypeInfo.InitializeEntityCollections, expression);
					}
				}
				return expression;
			}

			// Token: 0x06001A8E RID: 6798 RVA: 0x00083B94 File Offset: 0x00081D94
			private List<MemberBinding> CreatePropertyBindings(StructuredColumnMap columnMap, ReadOnlyMetadataCollection<EdmProperty> properties)
			{
				List<MemberBinding> list = new List<MemberBinding>(columnMap.Properties.Length);
				ObjectTypeMapping objectTypeMapping = this.LookupObjectMapping(columnMap.Type.EdmType);
				for (int i = 0; i < columnMap.Properties.Length; i++)
				{
					EdmProperty clrProperty = objectTypeMapping.GetPropertyMap(properties[i].Name).ClrProperty;
					PropertyInfo propertyInfo = DelegateFactory.ValidateSetterProperty(clrProperty.PropertyInfo);
					MethodInfo methodInfo = propertyInfo.Setter();
					Type propertyType = propertyInfo.PropertyType;
					Expression expression = columnMap.Properties[i].Accept<TranslatorResult, TranslatorArg>(this, new TranslatorArg(propertyType)).Expression;
					ScalarColumnMap scalarColumnMap = columnMap.Properties[i] as ScalarColumnMap;
					if (scalarColumnMap != null)
					{
						string propertyName = methodInfo.Name.Substring(4);
						Expression expressionWithErrorHandling = CodeGenEmitter.Emit_Shaper_GetPropertyValueWithErrorHandling(propertyType, scalarColumnMap.ColumnPos, propertyName, methodInfo.DeclaringType.Name, scalarColumnMap.Type);
						this._currentCoordinatorScratchpad.AddExpressionWithErrorHandling(expression, expressionWithErrorHandling);
					}
					list.Add(Expression.Bind(propertyInfo, expression));
				}
				return list;
			}

			// Token: 0x06001A8F RID: 6799 RVA: 0x00083C94 File Offset: 0x00081E94
			internal override TranslatorResult Visit(SimplePolymorphicColumnMap columnMap, TranslatorArg arg)
			{
				Expression expression = Translator.TranslatorVisitor.AcceptWithMappedType(this, columnMap.TypeDiscriminator).Expression;
				Expression expression2;
				if (this.IsValueLayer)
				{
					expression2 = CodeGenEmitter.Emit_EnsureType(this.BuildExpressionToGetRecordState(columnMap, null, null, Expression.Constant(true)), arg.RequestedType);
				}
				else
				{
					expression2 = CodeGenEmitter.Emit_WrappedNullConstant();
				}
				foreach (KeyValuePair<object, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
				{
					Type type = this.DetermineClrType(keyValuePair.Value.Type);
					if (!type.IsAbstract())
					{
						Expression expression3 = Expression.Constant(keyValuePair.Key, expression.Type);
						Expression test;
						if (expression.Type == typeof(string))
						{
							test = Expression.Call(Expression.Constant(TrailingSpaceStringComparer.Instance), CodeGenEmitter.IEqualityComparerOfString_Equals, expression3, expression);
						}
						else
						{
							test = CodeGenEmitter.Emit_Equal(expression3, expression);
						}
						bool inNullableType = this._inNullableType;
						this._inNullableType = true;
						expression2 = Expression.Condition(test, keyValuePair.Value.Accept<TranslatorResult, TranslatorArg>(this, arg).Expression, expression2);
						this._inNullableType = inNullableType;
					}
				}
				return new TranslatorResult(expression2, arg.RequestedType);
			}

			// Token: 0x06001A90 RID: 6800 RVA: 0x00083DD8 File Offset: 0x00081FD8
			internal override TranslatorResult Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, TranslatorArg arg)
			{
				MethodInfo methodInfo = Translator.TranslatorVisitor.Translator_MultipleDiscriminatorPolymorphicColumnMapHelper.MakeGenericMethod(new Type[]
				{
					arg.RequestedType
				});
				Expression returnedExpression = (Expression)methodInfo.Invoke(this, new object[]
				{
					columnMap
				});
				return new TranslatorResult(returnedExpression, arg.RequestedType);
			}

			// Token: 0x06001A91 RID: 6801 RVA: 0x00083E28 File Offset: 0x00082028
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Called via reflection by the Visit method")]
			private Expression MultipleDiscriminatorPolymorphicColumnMapHelper<TElement>(MultipleDiscriminatorPolymorphicColumnMap columnMap)
			{
				Expression[] array = new Expression[columnMap.TypeDiscriminators.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = columnMap.TypeDiscriminators[i].Accept<TranslatorResult, TranslatorArg>(this, new TranslatorArg(typeof(object))).Expression;
				}
				Expression arg = Expression.NewArrayInit(typeof(object), array);
				List<Expression> list = new List<Expression>();
				Type typeFromHandle = typeof(KeyValuePair<EntityType, Func<Shaper, TElement>>);
				ConstructorInfo declaredConstructor = typeFromHandle.GetDeclaredConstructor(new Type[]
				{
					typeof(EntityType),
					typeof(Func<Shaper, TElement>)
				});
				foreach (KeyValuePair<EntityType, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
				{
					Expression body = CodeGenEmitter.Emit_EnsureType(Translator.TranslatorVisitor.AcceptWithMappedType(this, keyValuePair.Value).UnwrappedExpression, typeof(TElement));
					LambdaExpression lambdaExpression = this.CreateInlineDelegate(body);
					Expression item = Expression.New(declaredConstructor, new Expression[]
					{
						Expression.Constant(keyValuePair.Key),
						lambdaExpression
					});
					list.Add(item);
				}
				MethodInfo method = CodeGenEmitter.Shaper_Discriminate.MakeGenericMethod(new Type[]
				{
					typeof(TElement)
				});
				return Expression.Call(CodeGenEmitter.Shaper_Parameter, method, arg, Expression.Constant(columnMap.Discriminate), Expression.NewArrayInit(typeFromHandle, list));
			}

			// Token: 0x06001A92 RID: 6802 RVA: 0x00083FB0 File Offset: 0x000821B0
			internal override TranslatorResult Visit(RecordColumnMap columnMap, TranslatorArg arg)
			{
				Expression expression = null;
				bool inNullableType = this._inNullableType;
				if (columnMap.NullSentinel != null)
				{
					expression = CodeGenEmitter.Emit_Reader_IsDBNull(columnMap.NullSentinel);
					this._inNullableType = true;
					int columnPos = ((ScalarColumnMap)columnMap.NullSentinel).ColumnPos;
					if (!this._streaming && !this.NullableColumns.Contains(columnPos))
					{
						this.NullableColumns.Add(columnPos);
					}
				}
				Expression expression2;
				if (this.IsValueLayer)
				{
					expression2 = this.BuildExpressionToGetRecordState(columnMap, null, null, expression);
				}
				else
				{
					InitializerMetadata initializerMetadata;
					Expression ifTrue;
					if (InitializerMetadata.TryGetInitializerMetadata(columnMap.Type, out initializerMetadata))
					{
						expression2 = this.HandleLinqRecord(columnMap, initializerMetadata);
						ifTrue = CodeGenEmitter.Emit_NullConstant(expression2.Type);
					}
					else
					{
						RowType spanRowType = (RowType)columnMap.Type.EdmType;
						if (this._spanIndex != null && this._spanIndex.HasSpanMap(spanRowType))
						{
							expression2 = this.HandleSpandexRecord(columnMap, arg, spanRowType);
							ifTrue = CodeGenEmitter.Emit_WrappedNullConstant();
						}
						else
						{
							expression2 = this.HandleRegularRecord(columnMap, arg, spanRowType);
							ifTrue = CodeGenEmitter.Emit_NullConstant(expression2.Type);
						}
					}
					if (expression != null)
					{
						expression2 = Expression.Condition(expression, ifTrue, expression2);
					}
				}
				this._inNullableType = inNullableType;
				return new TranslatorResult(expression2, arg.RequestedType);
			}

			// Token: 0x06001A93 RID: 6803 RVA: 0x000840CC File Offset: 0x000822CC
			private Expression BuildExpressionToGetRecordState(StructuredColumnMap columnMap, Expression entityKeyReader, Expression entitySetReader, Expression nullCheckExpression)
			{
				RecordStateScratchpad recordStateScratchpad = this._currentCoordinatorScratchpad.CreateRecordStateScratchpad();
				int num = this.AllocateStateSlot();
				recordStateScratchpad.StateSlotNumber = num;
				int num2 = columnMap.Properties.Length;
				int num3 = (entityKeyReader != null) ? (num2 + 1) : num2;
				recordStateScratchpad.ColumnCount = num2;
				EntityType metadata = null;
				if (TypeHelpers.TryGetEdmType<EntityType>(columnMap.Type, out metadata))
				{
					recordStateScratchpad.DataRecordInfo = new EntityRecordInfo(metadata, EntityKey.EntityNotValidKey, null);
				}
				else
				{
					TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(columnMap.Type);
					recordStateScratchpad.DataRecordInfo = new DataRecordInfo(modelTypeUsage);
				}
				Expression[] array = new Expression[num3];
				string[] array2 = new string[recordStateScratchpad.ColumnCount];
				TypeUsage[] array3 = new TypeUsage[recordStateScratchpad.ColumnCount];
				for (int i = 0; i < num2; i++)
				{
					Expression expression = columnMap.Properties[i].Accept<TranslatorResult, TranslatorArg>(this, new TranslatorArg(typeof(object))).Expression;
					array[i] = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_SetColumnValue, Expression.Constant(num), Expression.Constant(i), Expression.Coalesce(expression, CodeGenEmitter.DBNull_Value));
					array2[i] = columnMap.Properties[i].Name;
					array3[i] = columnMap.Properties[i].Type;
				}
				if (entityKeyReader != null)
				{
					array[num3 - 1] = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_SetEntityRecordInfo, Expression.Constant(num), entityKeyReader, entitySetReader);
				}
				recordStateScratchpad.GatherData = CodeGenEmitter.Emit_BitwiseOr(array);
				recordStateScratchpad.PropertyNames = array2;
				recordStateScratchpad.TypeUsages = array3;
				Expression expression2 = Expression.Call(CodeGenEmitter.Emit_Shaper_GetState(num, typeof(RecordState)), CodeGenEmitter.RecordState_GatherData, new Expression[]
				{
					CodeGenEmitter.Shaper_Parameter
				});
				if (nullCheckExpression != null)
				{
					Expression ifTrue = Expression.Call(CodeGenEmitter.Emit_Shaper_GetState(num, typeof(RecordState)), CodeGenEmitter.RecordState_SetNullRecord);
					expression2 = Expression.Condition(nullCheckExpression, ifTrue, expression2);
				}
				return expression2;
			}

			// Token: 0x06001A94 RID: 6804 RVA: 0x000842A8 File Offset: 0x000824A8
			private Expression HandleLinqRecord(RecordColumnMap columnMap, InitializerMetadata initializerMetadata)
			{
				List<TranslatorResult> list = new List<TranslatorResult>(columnMap.Properties.Length);
				foreach (KeyValuePair<ColumnMap, Type> keyValuePair in columnMap.Properties.Zip(initializerMetadata.GetChildTypes()))
				{
					ColumnMap key = keyValuePair.Key;
					Type type = keyValuePair.Value;
					if (null == type)
					{
						type = this.DetermineClrType(key.Type);
					}
					TranslatorResult item = key.Accept<TranslatorResult, TranslatorArg>(this, new TranslatorArg(type));
					list.Add(item);
				}
				return initializerMetadata.Emit(list);
			}

			// Token: 0x06001A95 RID: 6805 RVA: 0x00084354 File Offset: 0x00082554
			private Expression HandleRegularRecord(RecordColumnMap columnMap, TranslatorArg arg, RowType spanRowType)
			{
				Expression[] array = new Expression[columnMap.Properties.Length];
				for (int i = 0; i < array.Length; i++)
				{
					Expression unwrappedExpression = Translator.TranslatorVisitor.AcceptWithMappedType(this, columnMap.Properties[i]).UnwrappedExpression;
					array[i] = Expression.Coalesce(CodeGenEmitter.Emit_EnsureType(unwrappedExpression, typeof(object)), CodeGenEmitter.DBNull_Value);
				}
				Expression expression = Expression.NewArrayInit(typeof(object), array);
				TypeUsage typeUsage = columnMap.Type;
				if (this._spanIndex != null)
				{
					typeUsage = (this._spanIndex.GetSpannedRowType(spanRowType) ?? typeUsage);
				}
				Expression expression2 = Expression.Constant(typeUsage, typeof(TypeUsage));
				return CodeGenEmitter.Emit_EnsureType(Expression.New(CodeGenEmitter.MaterializedDataRecord_ctor, new Expression[]
				{
					CodeGenEmitter.Shaper_Workspace,
					expression2,
					expression
				}), arg.RequestedType);
			}

			// Token: 0x06001A96 RID: 6806 RVA: 0x00084430 File Offset: 0x00082630
			private Expression HandleSpandexRecord(RecordColumnMap columnMap, TranslatorArg arg, RowType spanRowType)
			{
				Dictionary<int, AssociationEndMember> spanMap = this._spanIndex.GetSpanMap(spanRowType);
				Expression expression = columnMap.Properties[0].Accept<TranslatorResult, TranslatorArg>(this, arg).Expression;
				for (int i = 1; i < columnMap.Properties.Length; i++)
				{
					AssociationEndMember value = spanMap[i];
					TranslatorResult translatorResult = Translator.TranslatorVisitor.AcceptWithMappedType(this, columnMap.Properties[i]);
					Expression expression2 = translatorResult.Expression;
					CollectionTranslatorResult collectionTranslatorResult = translatorResult as CollectionTranslatorResult;
					if (collectionTranslatorResult != null)
					{
						Expression expressionToGetCoordinator = collectionTranslatorResult.ExpressionToGetCoordinator;
						Type type = expression2.Type.GetGenericArguments()[0];
						MethodInfo method = CodeGenEmitter.Shaper_HandleFullSpanCollection.MakeGenericMethod(new Type[]
						{
							type
						});
						expression = Expression.Call(CodeGenEmitter.Shaper_Parameter, method, expression, expressionToGetCoordinator, Expression.Constant(value));
					}
					else if (typeof(EntityKey) == expression2.Type)
					{
						MethodInfo shaper_HandleRelationshipSpan = CodeGenEmitter.Shaper_HandleRelationshipSpan;
						expression = Expression.Call(CodeGenEmitter.Shaper_Parameter, shaper_HandleRelationshipSpan, expression, expression2, Expression.Constant(value));
					}
					else
					{
						MethodInfo shaper_HandleFullSpanElement = CodeGenEmitter.Shaper_HandleFullSpanElement;
						expression = Expression.Call(CodeGenEmitter.Shaper_Parameter, shaper_HandleFullSpanElement, expression, expression2, Expression.Constant(value));
					}
				}
				return expression;
			}

			// Token: 0x06001A97 RID: 6807 RVA: 0x00084548 File Offset: 0x00082748
			internal override TranslatorResult Visit(SimpleCollectionColumnMap columnMap, TranslatorArg arg)
			{
				return this.ProcessCollectionColumnMap(columnMap, arg);
			}

			// Token: 0x06001A98 RID: 6808 RVA: 0x00084552 File Offset: 0x00082752
			internal override TranslatorResult Visit(DiscriminatedCollectionColumnMap columnMap, TranslatorArg arg)
			{
				return this.ProcessCollectionColumnMap(columnMap, arg, columnMap.Discriminator, columnMap.DiscriminatorValue);
			}

			// Token: 0x06001A99 RID: 6809 RVA: 0x00084568 File Offset: 0x00082768
			private TranslatorResult ProcessCollectionColumnMap(CollectionColumnMap columnMap, TranslatorArg arg)
			{
				return this.ProcessCollectionColumnMap(columnMap, arg, null, null);
			}

			// Token: 0x06001A9A RID: 6810 RVA: 0x00084574 File Offset: 0x00082774
			private TranslatorResult ProcessCollectionColumnMap(CollectionColumnMap columnMap, TranslatorArg arg, ColumnMap discriminatorColumnMap, object discriminatorValue)
			{
				Type type = this.DetermineElementType(arg.RequestedType, columnMap);
				CoordinatorScratchpad coordinatorScratchpad = new CoordinatorScratchpad(type);
				this.EnterCoordinatorTranslateScope(coordinatorScratchpad);
				ColumnMap columnMap2 = columnMap.Element;
				if (this.IsValueLayer && !(columnMap2 is StructuredColumnMap))
				{
					ColumnMap[] properties = new ColumnMap[]
					{
						columnMap.Element
					};
					columnMap2 = new RecordColumnMap(columnMap.Element.Type, columnMap.Element.Name, properties, null);
				}
				bool inNullableType = this._inNullableType;
				if (discriminatorColumnMap != null)
				{
					this._inNullableType = true;
				}
				Expression unconvertedExpression = columnMap2.Accept<TranslatorResult, TranslatorArg>(this, new TranslatorArg(type)).UnconvertedExpression;
				Expression[] array;
				if (columnMap.Keys != null)
				{
					array = new Expression[columnMap.Keys.Length];
					for (int i = 0; i < array.Length; i++)
					{
						Expression expression = Translator.TranslatorVisitor.AcceptWithMappedType(this, columnMap.Keys[i]).Expression;
						array[i] = expression;
					}
				}
				else
				{
					array = new Expression[0];
				}
				Expression discriminator = null;
				if (discriminatorColumnMap != null)
				{
					discriminator = Translator.TranslatorVisitor.AcceptWithMappedType(this, discriminatorColumnMap).Expression;
					this._inNullableType = inNullableType;
				}
				Expression expression2 = this.BuildExpressionToGetCoordinator(type, unconvertedExpression, array, discriminator, discriminatorValue, coordinatorScratchpad);
				MethodInfo genericElementsMethod = Translator.TranslatorVisitor.GetGenericElementsMethod(type);
				Expression expression3;
				if (this.IsValueLayer)
				{
					expression3 = expression2;
				}
				else
				{
					expression3 = Expression.Call(expression2, genericElementsMethod);
					coordinatorScratchpad.Element = CodeGenEmitter.Emit_EnsureType(coordinatorScratchpad.Element, type);
					Type type2 = arg.RequestedType.TryGetElementType(typeof(ICollection<>));
					if (type2 != null)
					{
						Type type3 = EntityUtil.DetermineCollectionType(arg.RequestedType);
						if (type3 == null)
						{
							throw new InvalidOperationException(Strings.ObjectQuery_UnableToMaterializeArbitaryProjectionType(arg.RequestedType));
						}
						Type right = typeof(List<>).MakeGenericType(new Type[]
						{
							type2
						});
						if (type3 != right)
						{
							coordinatorScratchpad.InitializeCollection = CodeGenEmitter.Emit_EnsureType(DelegateFactory.GetNewExpressionForCollectionType(type3), typeof(ICollection<>).MakeGenericType(new Type[]
							{
								type2
							}));
						}
						expression3 = CodeGenEmitter.Emit_EnsureType(expression3, arg.RequestedType);
					}
					else if (!arg.RequestedType.IsAssignableFrom(expression3.Type))
					{
						Type type4 = typeof(CompensatingCollection<>).MakeGenericType(new Type[]
						{
							type
						});
						ConstructorInfo constructor = type4.GetConstructors()[0];
						expression3 = CodeGenEmitter.Emit_EnsureType(Expression.New(constructor, new Expression[]
						{
							expression3
						}), type4);
					}
				}
				this.ExitCoordinatorTranslateScope();
				return new CollectionTranslatorResult(expression3, arg.RequestedType, expression2);
			}

			// Token: 0x06001A9B RID: 6811 RVA: 0x000847FC File Offset: 0x000829FC
			public static MethodInfo GetGenericElementsMethod(Type elementType)
			{
				return typeof(Coordinator<>).MakeGenericType(new Type[]
				{
					elementType
				}).GetOnlyDeclaredMethod("GetElements");
			}

			// Token: 0x06001A9C RID: 6812 RVA: 0x00084830 File Offset: 0x00082A30
			private Type DetermineElementType(Type collectionType, CollectionColumnMap columnMap)
			{
				Type type;
				if (this.IsValueLayer)
				{
					type = typeof(RecordState);
				}
				else
				{
					type = TypeSystem.GetElementType(collectionType);
					if (type == collectionType)
					{
						TypeUsage typeUsage = ((CollectionType)columnMap.Type.EdmType).TypeUsage;
						type = this.DetermineClrType(typeUsage);
					}
				}
				return type;
			}

			// Token: 0x06001A9D RID: 6813 RVA: 0x00084884 File Offset: 0x00082A84
			private void EnterCoordinatorTranslateScope(CoordinatorScratchpad coordinatorScratchpad)
			{
				if (this.RootCoordinatorScratchpad == null)
				{
					coordinatorScratchpad.Depth = 0;
					this.RootCoordinatorScratchpad = coordinatorScratchpad;
					this._currentCoordinatorScratchpad = coordinatorScratchpad;
					return;
				}
				coordinatorScratchpad.Depth = this._currentCoordinatorScratchpad.Depth + 1;
				this._currentCoordinatorScratchpad.AddNestedCoordinator(coordinatorScratchpad);
				this._currentCoordinatorScratchpad = coordinatorScratchpad;
			}

			// Token: 0x06001A9E RID: 6814 RVA: 0x000848D5 File Offset: 0x00082AD5
			private void ExitCoordinatorTranslateScope()
			{
				this._currentCoordinatorScratchpad = this._currentCoordinatorScratchpad.Parent;
			}

			// Token: 0x06001A9F RID: 6815 RVA: 0x000848E8 File Offset: 0x00082AE8
			private Expression BuildExpressionToGetCoordinator(Type elementType, Expression element, Expression[] keyReaders, Expression discriminator, object discriminatorValue, CoordinatorScratchpad coordinatorScratchpad)
			{
				int stateSlotNumber = this.AllocateStateSlot();
				coordinatorScratchpad.StateSlotNumber = stateSlotNumber;
				coordinatorScratchpad.Element = element;
				List<Expression> list = new List<Expression>(keyReaders.Length);
				List<Expression> list2 = new List<Expression>(keyReaders.Length);
				foreach (Expression expression in keyReaders)
				{
					int stateSlotNumber2 = this.AllocateStateSlot();
					list.Add(CodeGenEmitter.Emit_Shaper_SetState(stateSlotNumber2, expression));
					list2.Add(CodeGenEmitter.Emit_Equal(CodeGenEmitter.Emit_Shaper_GetState(stateSlotNumber2, expression.Type), expression));
				}
				coordinatorScratchpad.SetKeys = CodeGenEmitter.Emit_BitwiseOr(list);
				coordinatorScratchpad.CheckKeys = CodeGenEmitter.Emit_AndAlso(list2);
				if (discriminator != null)
				{
					coordinatorScratchpad.HasData = CodeGenEmitter.Emit_Equal(Expression.Constant(discriminatorValue, discriminator.Type), discriminator);
				}
				return CodeGenEmitter.Emit_Shaper_GetState(stateSlotNumber, typeof(Coordinator<>).MakeGenericType(new Type[]
				{
					elementType
				}));
			}

			// Token: 0x06001AA0 RID: 6816 RVA: 0x000849CC File Offset: 0x00082BCC
			internal override TranslatorResult Visit(RefColumnMap columnMap, TranslatorArg arg)
			{
				EntityIdentity entityIdentity = columnMap.EntityIdentity;
				Expression expression;
				Expression returnedExpression = Expression.Condition(CodeGenEmitter.Emit_EntityKey_HasValue(entityIdentity.Keys), this.Emit_EntityKey_ctor(this, entityIdentity, ((RefType)columnMap.Type.EdmType).ElementType, true, out expression), Expression.Constant(null, typeof(EntityKey)));
				int columnPos = ((ScalarColumnMap)entityIdentity.Keys[0]).ColumnPos;
				if (!this._streaming && !this.NullableColumns.Contains(columnPos))
				{
					this.NullableColumns.Add(columnPos);
				}
				return new TranslatorResult(returnedExpression, arg.RequestedType);
			}

			// Token: 0x06001AA1 RID: 6817 RVA: 0x00084A64 File Offset: 0x00082C64
			[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
			internal override TranslatorResult Visit(ScalarColumnMap columnMap, TranslatorArg arg)
			{
				Type requestedType = arg.RequestedType;
				TypeUsage type = columnMap.Type;
				int columnPos = columnMap.ColumnPos;
				Type type2 = null;
				PrimitiveTypeKind primitiveTypeKind;
				Expression expression;
				if (Helper.IsSpatialType(type, out primitiveTypeKind))
				{
					expression = CodeGenEmitter.Emit_Conditional_NotDBNull(Helper.IsGeographicType((PrimitiveType)type.EdmType) ? CodeGenEmitter.Emit_EnsureType(CodeGenEmitter.Emit_Shaper_GetGeographyColumnValue(columnPos), requestedType) : CodeGenEmitter.Emit_EnsureType(CodeGenEmitter.Emit_Shaper_GetGeometryColumnValue(columnPos), requestedType), columnPos, requestedType);
					if (!this._streaming && !this.NullableColumns.Contains(columnPos))
					{
						this.NullableColumns.Add(columnPos);
					}
				}
				else
				{
					bool flag;
					MethodInfo readerMethod = CodeGenEmitter.GetReaderMethod(requestedType, out flag);
					expression = Expression.Call(CodeGenEmitter.Shaper_Reader, readerMethod, new Expression[]
					{
						Expression.Constant(columnPos)
					});
					type2 = TypeSystem.GetNonNullableType(requestedType);
					if (type2.IsEnum() && type2 != requestedType)
					{
						expression = Expression.Convert(expression, type2);
					}
					else if (requestedType == typeof(object) && !this.IsValueLayer && TypeSemantics.IsEnumerationType(type))
					{
						expression = Expression.Condition(CodeGenEmitter.Emit_Reader_IsDBNull(columnPos), expression, Expression.Convert(Expression.Convert(expression, TypeSystem.GetNonNullableType(this.DetermineClrType(type.EdmType))), typeof(object)));
						if (!this._streaming && !this.NullableColumns.Contains(columnPos))
						{
							this.NullableColumns.Add(columnPos);
						}
					}
					expression = CodeGenEmitter.Emit_EnsureType(expression, requestedType);
					if (flag)
					{
						expression = CodeGenEmitter.Emit_Conditional_NotDBNull(expression, columnPos, requestedType);
						if (!this._streaming && !this.NullableColumns.Contains(columnPos))
						{
							this.NullableColumns.Add(columnPos);
						}
					}
				}
				if (!this._streaming)
				{
					Type type3 = type2 ?? requestedType;
					type3 = (type3.IsEnum() ? type3.GetEnumUnderlyingType() : type3);
					Type left;
					if (this.ColumnTypes.TryGetValue(columnPos, out left))
					{
						if (left == typeof(object) && type3 != typeof(object))
						{
							this.ColumnTypes[columnPos] = type3;
						}
					}
					else
					{
						this.ColumnTypes.Add(columnPos, type3);
						if (this._inNullableType && !this.NullableColumns.Contains(columnPos))
						{
							this.NullableColumns.Add(columnPos);
						}
					}
				}
				Expression expressionWithErrorHandling = CodeGenEmitter.Emit_Shaper_GetColumnValueWithErrorHandling(arg.RequestedType, columnPos, type);
				this._currentCoordinatorScratchpad.AddExpressionWithErrorHandling(expression, expressionWithErrorHandling);
				return new TranslatorResult(expression, requestedType);
			}

			// Token: 0x06001AA2 RID: 6818 RVA: 0x00084CBE File Offset: 0x00082EBE
			internal override TranslatorResult Visit(VarRefColumnMap columnMap, TranslatorArg arg)
			{
				throw new InvalidOperationException(string.Empty);
			}

			// Token: 0x06001AA3 RID: 6819 RVA: 0x00084CCC File Offset: 0x00082ECC
			private int AllocateStateSlot()
			{
				return this.StateSlotCount++;
			}

			// Token: 0x06001AA4 RID: 6820 RVA: 0x00084CEA File Offset: 0x00082EEA
			private Type DetermineClrType(TypeUsage typeUsage)
			{
				return this.DetermineClrType(typeUsage.EdmType);
			}

			// Token: 0x06001AA5 RID: 6821 RVA: 0x00084CF8 File Offset: 0x00082EF8
			private Type DetermineClrType(EdmType edmType)
			{
				Type type = null;
				edmType = this.ResolveSpanType(edmType);
				BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
				if (builtInTypeKind <= BuiltInTypeKind.EnumType)
				{
					switch (builtInTypeKind)
					{
					case BuiltInTypeKind.CollectionType:
					{
						if (this.IsValueLayer)
						{
							return typeof(Coordinator<RecordState>);
						}
						EdmType edmType2 = ((CollectionType)edmType).TypeUsage.EdmType;
						type = this.DetermineClrType(edmType2);
						return typeof(IEnumerable<>).MakeGenericType(new Type[]
						{
							type
						});
					}
					case BuiltInTypeKind.CollectionKind:
						return type;
					case BuiltInTypeKind.ComplexType:
						break;
					default:
						switch (builtInTypeKind)
						{
						case BuiltInTypeKind.EntityType:
							break;
						case BuiltInTypeKind.EnumType:
							if (this.IsValueLayer)
							{
								return this.DetermineClrType(((EnumType)edmType).UnderlyingType);
							}
							type = this.LookupObjectMapping(edmType).ClrType.ClrType;
							return typeof(Nullable<>).MakeGenericType(new Type[]
							{
								type
							});
						default:
							return type;
						}
						break;
					}
					if (this.IsValueLayer)
					{
						type = typeof(RecordState);
					}
					else
					{
						type = this.LookupObjectMapping(edmType).ClrType.ClrType;
					}
				}
				else if (builtInTypeKind != BuiltInTypeKind.PrimitiveType)
				{
					if (builtInTypeKind != BuiltInTypeKind.RefType)
					{
						if (builtInTypeKind == BuiltInTypeKind.RowType)
						{
							if (this.IsValueLayer)
							{
								type = typeof(RecordState);
							}
							else
							{
								InitializerMetadata initializerMetadata = ((RowType)edmType).InitializerMetadata;
								if (initializerMetadata != null)
								{
									type = initializerMetadata.ClrType;
								}
								else
								{
									type = typeof(DbDataRecord);
								}
							}
						}
					}
					else
					{
						type = typeof(EntityKey);
					}
				}
				else
				{
					type = ((PrimitiveType)edmType).ClrEquivalentType;
					if (type.IsValueType())
					{
						type = typeof(Nullable<>).MakeGenericType(new Type[]
						{
							type
						});
					}
				}
				return type;
			}

			// Token: 0x06001AA6 RID: 6822 RVA: 0x00084EB4 File Offset: 0x000830B4
			private static ConstructorInfo GetConstructor(Type type)
			{
				if (!type.IsAbstract())
				{
					return DelegateFactory.GetConstructorForType(type);
				}
				return null;
			}

			// Token: 0x06001AA7 RID: 6823 RVA: 0x00084EC8 File Offset: 0x000830C8
			private ObjectTypeMapping LookupObjectMapping(EdmType edmType)
			{
				EdmType edmType2 = this.ResolveSpanType(edmType);
				if (edmType2 == null)
				{
					edmType2 = edmType;
				}
				ObjectTypeMapping objectMapping;
				if (!this._objectTypeMappings.TryGetValue(edmType2, out objectMapping))
				{
					objectMapping = Util.GetObjectMapping(edmType2, this._workspace);
					this._objectTypeMappings.Add(edmType2, objectMapping);
				}
				return objectMapping;
			}

			// Token: 0x06001AA8 RID: 6824 RVA: 0x00084F10 File Offset: 0x00083110
			private EdmType ResolveSpanType(EdmType edmType)
			{
				EdmType edmType2 = edmType;
				BuiltInTypeKind builtInTypeKind = edmType2.BuiltInTypeKind;
				if (builtInTypeKind != BuiltInTypeKind.CollectionType)
				{
					if (builtInTypeKind == BuiltInTypeKind.RowType)
					{
						RowType rowType = (RowType)edmType2;
						if (this._spanIndex != null && this._spanIndex.HasSpanMap(rowType))
						{
							edmType2 = rowType.Members[0].TypeUsage.EdmType;
						}
					}
				}
				else
				{
					edmType2 = this.ResolveSpanType(((CollectionType)edmType2).TypeUsage.EdmType);
					if (edmType2 != null)
					{
						edmType2 = new CollectionType(edmType2);
					}
				}
				return edmType2;
			}

			// Token: 0x06001AA9 RID: 6825 RVA: 0x00084F8C File Offset: 0x0008318C
			private LambdaExpression CreateInlineDelegate(Expression body)
			{
				Type type = body.Type;
				MethodInfo methodInfo = Translator.TranslatorVisitor.Translator_TypedCreateInlineDelegate.MakeGenericMethod(new Type[]
				{
					type
				});
				return (LambdaExpression)methodInfo.Invoke(this, new object[]
				{
					body
				});
			}

			// Token: 0x06001AAA RID: 6826 RVA: 0x00084FD4 File Offset: 0x000831D4
			[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Called via reflection by the non-generic overload")]
			private Expression<Func<Shaper, T>> TypedCreateInlineDelegate<T>(Expression body)
			{
				Expression<Func<Shaper, T>> expression = Expression.Lambda<Func<Shaper, T>>(body, new ParameterExpression[]
				{
					CodeGenEmitter.Shaper_Parameter
				});
				this._currentCoordinatorScratchpad.AddInlineDelegate(expression);
				return expression;
			}

			// Token: 0x06001AAB RID: 6827 RVA: 0x00085008 File Offset: 0x00083208
			private Expression Emit_EntityKey_ctor(Translator.TranslatorVisitor translatorVisitor, EntityIdentity entityIdentity, EdmType type, bool isForColumnValue, out Expression entitySetReader)
			{
				Expression expression = null;
				List<Expression> list = new List<Expression>(entityIdentity.Keys.Length);
				if (this.IsValueLayer)
				{
					for (int i = 0; i < entityIdentity.Keys.Length; i++)
					{
						Expression expression2 = entityIdentity.Keys[i].Accept<TranslatorResult, TranslatorArg>(translatorVisitor, new TranslatorArg(typeof(object))).Expression;
						list.Add(expression2);
					}
				}
				else
				{
					ObjectTypeMapping objectTypeMapping = this.LookupObjectMapping(type);
					for (int j = 0; j < entityIdentity.Keys.Length; j++)
					{
						EdmProperty clrProperty = objectTypeMapping.GetPropertyMap(entityIdentity.Keys[j].Name).ClrProperty;
						PropertyInfo propertyInfo = DelegateFactory.ValidateSetterProperty(clrProperty.PropertyInfo);
						Type propertyType = propertyInfo.PropertyType;
						Expression expression3 = entityIdentity.Keys[j].Accept<TranslatorResult, TranslatorArg>(translatorVisitor, new TranslatorArg(propertyType)).Expression;
						list.Add(CodeGenEmitter.Emit_EnsureType(expression3, typeof(object)));
					}
				}
				SimpleEntityIdentity simpleEntityIdentity = entityIdentity as SimpleEntityIdentity;
				if (simpleEntityIdentity != null)
				{
					if (simpleEntityIdentity.EntitySet == null)
					{
						entitySetReader = Expression.Constant(null, typeof(EntitySet));
						return Expression.Constant(null, typeof(EntityKey));
					}
					entitySetReader = Expression.Constant(simpleEntityIdentity.EntitySet, typeof(EntitySet));
				}
				else
				{
					DiscriminatedEntityIdentity discriminatedEntityIdentity = (DiscriminatedEntityIdentity)entityIdentity;
					Expression expression4 = discriminatedEntityIdentity.EntitySetColumnMap.Accept<TranslatorResult, TranslatorArg>(translatorVisitor, new TranslatorArg(typeof(int?))).Expression;
					EntitySet[] entitySetMap = discriminatedEntityIdentity.EntitySetMap;
					entitySetReader = Expression.Constant(null, typeof(EntitySet));
					for (int k = 0; k < entitySetMap.Length; k++)
					{
						entitySetReader = Expression.Condition(Expression.Equal(expression4, Expression.Constant(k, typeof(int?))), Expression.Constant(entitySetMap[k], typeof(EntitySet)), entitySetReader);
					}
					int stateSlotNumber = translatorVisitor.AllocateStateSlot();
					expression = CodeGenEmitter.Emit_Shaper_SetStatePassthrough(stateSlotNumber, entitySetReader);
					entitySetReader = CodeGenEmitter.Emit_Shaper_GetState(stateSlotNumber, typeof(EntitySet));
				}
				Expression expression5;
				if (1 == entityIdentity.Keys.Length)
				{
					expression5 = Expression.New(CodeGenEmitter.EntityKey_ctor_SingleKey, new Expression[]
					{
						entitySetReader,
						list[0]
					});
				}
				else
				{
					expression5 = Expression.New(CodeGenEmitter.EntityKey_ctor_CompositeKey, new Expression[]
					{
						entitySetReader,
						Expression.NewArrayInit(typeof(object), list)
					});
				}
				if (expression != null)
				{
					Expression ifTrue;
					if (translatorVisitor.IsValueLayer && !isForColumnValue)
					{
						ifTrue = Expression.Constant(EntityKey.NoEntitySetKey, typeof(EntityKey));
					}
					else
					{
						ifTrue = Expression.Constant(null, typeof(EntityKey));
					}
					expression5 = Expression.Condition(Expression.Equal(expression, Expression.Constant(null, typeof(EntitySet))), ifTrue, expression5);
				}
				return expression5;
			}

			// Token: 0x04000925 RID: 2341
			private readonly MetadataWorkspace _workspace;

			// Token: 0x04000926 RID: 2342
			private readonly SpanIndex _spanIndex;

			// Token: 0x04000927 RID: 2343
			private readonly MergeOption _mergeOption;

			// Token: 0x04000928 RID: 2344
			private readonly bool _streaming;

			// Token: 0x04000929 RID: 2345
			private readonly bool IsValueLayer;

			// Token: 0x0400092A RID: 2346
			private CoordinatorScratchpad _currentCoordinatorScratchpad;

			// Token: 0x0400092B RID: 2347
			private readonly Dictionary<EdmType, ObjectTypeMapping> _objectTypeMappings = new Dictionary<EdmType, ObjectTypeMapping>();

			// Token: 0x0400092C RID: 2348
			private bool _inNullableType;

			// Token: 0x0400092D RID: 2349
			public static readonly MethodInfo Translator_MultipleDiscriminatorPolymorphicColumnMapHelper = typeof(Translator.TranslatorVisitor).GetOnlyDeclaredMethod("MultipleDiscriminatorPolymorphicColumnMapHelper");

			// Token: 0x0400092E RID: 2350
			public static readonly MethodInfo Translator_TypedCreateInlineDelegate = typeof(Translator.TranslatorVisitor).GetOnlyDeclaredMethod("TypedCreateInlineDelegate");
		}
	}
}
