using System;
using System.Collections.Generic;
using System.Data.Common.QueryCache;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Mapping;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.Objects.ELinq;
using System.Data.Objects.Internal;
using System.Data.Query.InternalTrees;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003D7 RID: 983
	internal class Translator : ColumnMapVisitorWithResults<TranslatorResult, TranslatorArg>
	{
		// Token: 0x060034D9 RID: 13529 RVA: 0x000CBFFC File Offset: 0x000CA1FC
		private Translator(MetadataWorkspace workspace, SpanIndex spanIndex, MergeOption mergeOption, bool valueLayer)
		{
			this._workspace = workspace;
			this._spanIndex = spanIndex;
			this._mergeOption = mergeOption;
			this.IsValueLayer = valueLayer;
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000CC02C File Offset: 0x000CA22C
		internal static ShaperFactory<TRequestedType> TranslateColumnMap<TRequestedType>(QueryCacheManager queryCacheManager, ColumnMap columnMap, MetadataWorkspace workspace, SpanIndex spanIndex, MergeOption mergeOption, bool valueLayer)
		{
			string columnMapKey = ColumnMapKeyBuilder.GetColumnMapKey(columnMap, spanIndex);
			ShaperFactoryQueryCacheKey<TRequestedType> shaperFactoryQueryCacheKey = new ShaperFactoryQueryCacheKey<TRequestedType>(columnMapKey, mergeOption, valueLayer);
			ShaperFactory<TRequestedType> shaperFactory;
			if (queryCacheManager.TryCacheLookup<ShaperFactoryQueryCacheKey<TRequestedType>, ShaperFactory<TRequestedType>>(shaperFactoryQueryCacheKey, out shaperFactory))
			{
				return shaperFactory;
			}
			Translator translator = new Translator(workspace, spanIndex, mergeOption, valueLayer);
			columnMap.Accept<TranslatorResult, TranslatorArg>(translator, new TranslatorArg(typeof(IEnumerable<>).MakeGenericType(new Type[]
			{
				typeof(TRequestedType)
			})));
			CoordinatorFactory<TRequestedType> rootCoordinatorFactory = (CoordinatorFactory<TRequestedType>)translator._rootCoordinatorScratchpad.Compile();
			Action checkPermissionsDelegate = translator.GetCheckPermissionsDelegate();
			shaperFactory = new ShaperFactory<TRequestedType>(translator._stateSlotCount, rootCoordinatorFactory, checkPermissionsDelegate, mergeOption);
			QueryCacheEntry queryCacheEntry = new QueryCacheEntry(shaperFactoryQueryCacheKey, shaperFactory);
			if (queryCacheManager.TryLookupAndAdd(queryCacheEntry, out queryCacheEntry))
			{
				shaperFactory = (ShaperFactory<TRequestedType>)queryCacheEntry.GetTarget();
			}
			return shaperFactory;
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000CC0E4 File Offset: 0x000CA2E4
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		internal static Func<Shaper, TResult> Compile<TResult>(Expression body)
		{
			Expression<Func<Shaper, TResult>> expression = Expression.Lambda<Func<Shaper, TResult>>(body, new ParameterExpression[]
			{
				Translator.Shaper_Parameter
			});
			return expression.Compile();
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000CC10C File Offset: 0x000CA30C
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal static object Compile(Type resultType, Expression body)
		{
			MethodInfo methodInfo = Translator.Translator_Compile.MakeGenericMethod(new Type[]
			{
				resultType
			});
			return methodInfo.Invoke(null, new object[]
			{
				body
			});
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000CC140 File Offset: 0x000CA340
		private int AllocateStateSlot()
		{
			int stateSlotCount = this._stateSlotCount;
			this._stateSlotCount = stateSlotCount + 1;
			return stateSlotCount;
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x000CC15E File Offset: 0x000CA35E
		private Action GetCheckPermissionsDelegate()
		{
			if (!this._hasNonPublicMembers)
			{
				return null;
			}
			return new Action(Translator.DemandMemberAccess);
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x000CC176 File Offset: 0x000CA376
		private static void DemandMemberAccess()
		{
			LightweightCodeGenerator.MemberAccessReflectionPermission.Demand();
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x000CC184 File Offset: 0x000CA384
		private static void VerifyUserExpressions(IEnumerable<Expression<Func<object>>> userExpressions)
		{
			if (!LightweightCodeGenerator.HasMemberAccessReflectionPermission())
			{
				foreach (Expression<Func<object>> expression in userExpressions)
				{
					expression.Compile();
				}
			}
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000CC1D4 File Offset: 0x000CA3D4
		private Type DetermineClrType(TypeUsage typeUsage)
		{
			return this.DetermineClrType(typeUsage.EdmType);
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000CC1E4 File Offset: 0x000CA3E4
		private Type DetermineClrType(EdmType edmType)
		{
			Type type = null;
			edmType = this.ResolveSpanType(edmType);
			BuiltInTypeKind builtInTypeKind = edmType.BuiltInTypeKind;
			if (builtInTypeKind <= BuiltInTypeKind.EntityType)
			{
				if (builtInTypeKind != BuiltInTypeKind.CollectionType)
				{
					if (builtInTypeKind == BuiltInTypeKind.ComplexType || builtInTypeKind == BuiltInTypeKind.EntityType)
					{
						if (this.IsValueLayer)
						{
							type = typeof(RecordState);
						}
						else
						{
							type = this.LookupObjectMapping(edmType).ClrType.ClrType;
						}
					}
				}
				else if (this.IsValueLayer)
				{
					type = typeof(Coordinator<RecordState>);
				}
				else
				{
					EdmType edmType2 = ((CollectionType)edmType).TypeUsage.EdmType;
					type = this.DetermineClrType(edmType2);
					type = typeof(IEnumerable<>).MakeGenericType(new Type[]
					{
						type
					});
				}
			}
			else if (builtInTypeKind <= BuiltInTypeKind.PrimitiveType)
			{
				if (builtInTypeKind != BuiltInTypeKind.EnumType)
				{
					if (builtInTypeKind == BuiltInTypeKind.PrimitiveType)
					{
						type = ((PrimitiveType)edmType).ClrEquivalentType;
						if (type.IsValueType)
						{
							type = typeof(Nullable<>).MakeGenericType(new Type[]
							{
								type
							});
						}
					}
				}
				else if (this.IsValueLayer)
				{
					type = this.DetermineClrType(((EnumType)edmType).UnderlyingType);
				}
				else
				{
					type = this.LookupObjectMapping(edmType).ClrType.ClrType;
					type = typeof(Nullable<>).MakeGenericType(new Type[]
					{
						type
					});
				}
			}
			else if (builtInTypeKind != BuiltInTypeKind.RefType)
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
			return type;
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000CC38C File Offset: 0x000CA58C
		private ConstructorInfo GetConstructor(Type type)
		{
			ConstructorInfo constructorInfo = null;
			if (!type.IsAbstract)
			{
				constructorInfo = LightweightCodeGenerator.GetConstructorForType(type);
				if (!LightweightCodeGenerator.IsPublic(constructorInfo))
				{
					this._hasNonPublicMembers = true;
				}
			}
			return constructorInfo;
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000CC3BC File Offset: 0x000CA5BC
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

		// Token: 0x060034E5 RID: 13541 RVA: 0x000CC404 File Offset: 0x000CA604
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

		// Token: 0x060034E6 RID: 13542 RVA: 0x000CC480 File Offset: 0x000CA680
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private LambdaExpression CreateInlineDelegate(Expression body)
		{
			Type type = body.Type;
			MethodInfo methodInfo = Translator.Translator_TypedCreateInlineDelegate.MakeGenericMethod(new Type[]
			{
				type
			});
			return (LambdaExpression)methodInfo.Invoke(this, new object[]
			{
				body
			});
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x000CC4C4 File Offset: 0x000CA6C4
		private Expression<Func<Shaper, T>> TypedCreateInlineDelegate<T>(Expression body)
		{
			Expression<Func<Shaper, T>> expression = Expression.Lambda<Func<Shaper, T>>(body, new ParameterExpression[]
			{
				Translator.Shaper_Parameter
			});
			this._currentCoordinatorScratchpad.AddInlineDelegate(expression);
			return expression;
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000CC4F4 File Offset: 0x000CA6F4
		private static Expression Emit_AndAlso(IEnumerable<Expression> operands)
		{
			Expression expression = null;
			foreach (Expression expression2 in operands)
			{
				if (expression == null)
				{
					expression = expression2;
				}
				else
				{
					expression = Expression.AndAlso(expression, expression2);
				}
			}
			return expression;
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000CC548 File Offset: 0x000CA748
		private static Expression Emit_BitwiseOr(IEnumerable<Expression> operands)
		{
			Expression expression = null;
			foreach (Expression expression2 in operands)
			{
				if (expression == null)
				{
					expression = expression2;
				}
				else
				{
					expression = Expression.Or(expression, expression2);
				}
			}
			return expression;
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000CC59C File Offset: 0x000CA79C
		internal static Expression Emit_NullConstant(Type type)
		{
			EntityUtil.CheckArgumentNull<Type>(type, "type");
			Expression result;
			if (type.IsClass || TypeSystem.IsNullableType(type))
			{
				result = Expression.Constant(null, type);
			}
			else
			{
				result = Translator.Emit_EnsureType(Expression.Constant(null, typeof(object)), type);
			}
			return result;
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000CC5E7 File Offset: 0x000CA7E7
		internal static Expression Emit_WrappedNullConstant(Type type)
		{
			return Expression.Property(null, Translator.EntityWrapperFactory_NullWrapper);
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000CC5F4 File Offset: 0x000CA7F4
		internal static Expression Emit_EnsureType(Expression input, Type type)
		{
			Expression result = input;
			if (input.Type != type && !typeof(IEntityWrapper).IsAssignableFrom(input.Type))
			{
				if (type.IsAssignableFrom(input.Type))
				{
					result = Expression.Convert(input, type);
				}
				else
				{
					MethodInfo method = Translator.Translator_CheckedConvert.MakeGenericMethod(new Type[]
					{
						input.Type,
						type
					});
					result = Expression.Call(method, input);
				}
			}
			return result;
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000CC668 File Offset: 0x000CA868
		internal static Expression Emit_EnsureTypeAndWrap(Expression input, Expression keyReader, Expression entitySetReader, Type requestedType, Type identityType, Type actualType, MergeOption mergeOption, bool isProxy)
		{
			Expression input2 = Translator.Emit_EnsureType(input, requestedType);
			if (!requestedType.IsClass)
			{
				input2 = Translator.Emit_EnsureType(input, typeof(object));
			}
			input2 = Translator.Emit_EnsureType(input2, actualType);
			return Translator.CreateEntityWrapper(input2, keyReader, entitySetReader, actualType, identityType, mergeOption, isProxy);
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000CC6B0 File Offset: 0x000CA8B0
		private static Expression CreateEntityWrapper(Expression input, Expression keyReader, Expression entitySetReader, Type actualType, Type identityType, MergeOption mergeOption, bool isProxy)
		{
			bool flag = typeof(IEntityWithKey).IsAssignableFrom(actualType);
			bool flag2 = typeof(IEntityWithRelationships).IsAssignableFrom(actualType);
			bool flag3 = typeof(IEntityWithChangeTracker).IsAssignableFrom(actualType);
			Expression expression;
			if (flag2 && flag3 && flag && !isProxy)
			{
				Type type = typeof(LightweightEntityWrapper<>).MakeGenericType(new Type[]
				{
					actualType
				});
				ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new Type[]
				{
					actualType,
					typeof(EntityKey),
					typeof(EntitySet),
					typeof(ObjectContext),
					typeof(MergeOption),
					typeof(Type)
				}, null);
				expression = Expression.New(constructor, new Expression[]
				{
					input,
					keyReader,
					entitySetReader,
					Translator.Shaper_Context,
					Expression.Constant(mergeOption, typeof(MergeOption)),
					Expression.Constant(identityType, typeof(Type))
				});
			}
			else
			{
				Expression expression2 = (!flag2 || isProxy) ? Expression.Call(Translator.EntityWrapperFactory_GetPocoPropertyAccessorStrategyFunc, new Expression[0]) : Expression.Call(Translator.EntityWrapperFactory_GetNullPropertyAccessorStrategyFunc, new Expression[0]);
				Expression expression3 = flag ? Expression.Call(Translator.EntityWrapperFactory_GetEntityWithKeyStrategyStrategyFunc, new Expression[0]) : Expression.Call(Translator.EntityWrapperFactory_GetPocoEntityKeyStrategyFunc, new Expression[0]);
				Expression expression4 = flag3 ? Expression.Call(Translator.EntityWrapperFactory_GetEntityWithChangeTrackerStrategyFunc, new Expression[0]) : Expression.Call(Translator.EntityWrapperFactory_GetSnapshotChangeTrackingStrategyFunc, new Expression[0]);
				Type type2 = flag2 ? typeof(EntityWrapperWithRelationships<>).MakeGenericType(new Type[]
				{
					actualType
				}) : typeof(EntityWrapperWithoutRelationships<>).MakeGenericType(new Type[]
				{
					actualType
				});
				ConstructorInfo constructor2 = type2.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new Type[]
				{
					actualType,
					typeof(EntityKey),
					typeof(EntitySet),
					typeof(ObjectContext),
					typeof(MergeOption),
					typeof(Type),
					typeof(Func<object, IPropertyAccessorStrategy>),
					typeof(Func<object, IChangeTrackingStrategy>),
					typeof(Func<object, IEntityKeyStrategy>)
				}, null);
				expression = Expression.New(constructor2, new Expression[]
				{
					input,
					keyReader,
					entitySetReader,
					Translator.Shaper_Context,
					Expression.Constant(mergeOption, typeof(MergeOption)),
					Expression.Constant(identityType, typeof(Type)),
					expression2,
					expression4,
					expression3
				});
			}
			return Expression.Convert(expression, typeof(IEntityWrapper));
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000CC973 File Offset: 0x000CAB73
		internal static Expression Emit_UnwrapAndEnsureType(Expression input, Type type)
		{
			return Translator.Emit_EnsureType(Expression.Property(input, Translator.IEntityWrapper_Entity), type);
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000CC988 File Offset: 0x000CAB88
		private static TTarget CheckedConvert<TSource, TTarget>(TSource value)
		{
			TTarget result;
			try
			{
				result = (TTarget)((object)value);
			}
			catch (InvalidCastException)
			{
				Type type = value.GetType();
				if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(CompensatingCollection<>))
				{
					type = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments());
				}
				throw EntityUtil.ValueInvalidCast(type, typeof(TTarget));
			}
			catch (NullReferenceException)
			{
				throw EntityUtil.ValueNullReferenceCast(typeof(TTarget));
			}
			return result;
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000CCA28 File Offset: 0x000CAC28
		private static Expression Emit_Equal(Expression left, Expression right)
		{
			Expression result;
			if (typeof(byte[]) == left.Type)
			{
				result = Expression.Call(Translator.Translator_BinaryEquals, left, right);
			}
			else
			{
				result = Expression.Equal(left, right);
			}
			return result;
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000CCA64 File Offset: 0x000CAC64
		private static bool BinaryEquals(byte[] left, byte[] right)
		{
			if (left == null)
			{
				return right == null;
			}
			if (right == null)
			{
				return false;
			}
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x000CCAA4 File Offset: 0x000CACA4
		private static Expression Emit_EntityKey_ctor(Translator translator, EntityIdentity entityIdentity, bool isForColumnValue, out Expression entitySetReader)
		{
			Expression expression = null;
			List<Expression> list = new List<Expression>(entityIdentity.Keys.Length);
			for (int i = 0; i < entityIdentity.Keys.Length; i++)
			{
				Expression expression2 = entityIdentity.Keys[i].Accept<TranslatorResult, TranslatorArg>(translator, new TranslatorArg(typeof(object))).Expression;
				list.Add(expression2);
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
				Expression expression3 = discriminatedEntityIdentity.EntitySetColumnMap.Accept<TranslatorResult, TranslatorArg>(translator, new TranslatorArg(typeof(int?))).Expression;
				EntitySet[] entitySetMap = discriminatedEntityIdentity.EntitySetMap;
				entitySetReader = Expression.Constant(null, typeof(EntitySet));
				for (int j = 0; j < entitySetMap.Length; j++)
				{
					entitySetReader = Expression.Condition(Expression.Equal(expression3, Expression.Constant(j, typeof(int?))), Expression.Constant(entitySetMap[j], typeof(EntitySet)), entitySetReader);
				}
				int stateSlotNumber = translator.AllocateStateSlot();
				expression = Translator.Emit_Shaper_SetStatePassthrough(stateSlotNumber, entitySetReader);
				entitySetReader = Translator.Emit_Shaper_GetState(stateSlotNumber, typeof(EntitySet));
			}
			Expression expression4;
			if (1 == entityIdentity.Keys.Length)
			{
				expression4 = Expression.New(Translator.EntityKey_ctor_SingleKey, new Expression[]
				{
					entitySetReader,
					list[0]
				});
			}
			else
			{
				expression4 = Expression.New(Translator.EntityKey_ctor_CompositeKey, new Expression[]
				{
					entitySetReader,
					Expression.NewArrayInit(typeof(object), list)
				});
			}
			if (expression != null)
			{
				Expression ifTrue;
				if (translator.IsValueLayer && !isForColumnValue)
				{
					ifTrue = Expression.Constant(EntityKey.NoEntitySetKey, typeof(EntityKey));
				}
				else
				{
					ifTrue = Expression.Constant(null, typeof(EntityKey));
				}
				expression4 = Expression.Condition(Expression.Equal(expression, Expression.Constant(null, typeof(EntitySet))), ifTrue, expression4);
			}
			return expression4;
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000CCCBC File Offset: 0x000CAEBC
		private static Expression Emit_EntityKey_HasValue(SimpleColumnMap[] keyColumns)
		{
			Expression expression = Translator.Emit_Reader_IsDBNull(keyColumns[0]);
			return Expression.Not(expression);
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000CCCDC File Offset: 0x000CAEDC
		private static Expression Emit_Reader_GetValue(int ordinal, Type type)
		{
			return Translator.Emit_EnsureType(Expression.Call(Translator.Shaper_Reader, Translator.DbDataReader_GetValue, new Expression[]
			{
				Expression.Constant(ordinal)
			}), type);
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000CCD14 File Offset: 0x000CAF14
		private static Expression Emit_Reader_IsDBNull(int ordinal)
		{
			return Expression.Call(Translator.Shaper_Reader, Translator.DbDataReader_IsDBNull, new Expression[]
			{
				Expression.Constant(ordinal)
			});
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000CCD48 File Offset: 0x000CAF48
		private static Expression Emit_Reader_IsDBNull(ColumnMap columnMap)
		{
			return Translator.Emit_Reader_IsDBNull(((ScalarColumnMap)columnMap).ColumnPos);
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000CCD68 File Offset: 0x000CAF68
		private static Expression Emit_Shaper_GetPropertyValueWithErrorHandling(Type propertyType, int ordinal, string propertyName, string typeName, TypeUsage columnType)
		{
			PrimitiveTypeKind primitiveTypeKind;
			Expression result;
			if (Helper.IsSpatialType(columnType, out primitiveTypeKind))
			{
				result = Expression.Call(Translator.Shaper_Parameter, Translator.Shaper_GetSpatialPropertyValueWithErrorHandling.MakeGenericMethod(new Type[]
				{
					propertyType
				}), new Expression[]
				{
					Expression.Constant(ordinal),
					Expression.Constant(propertyName),
					Expression.Constant(typeName),
					Expression.Constant(primitiveTypeKind, typeof(PrimitiveTypeKind))
				});
			}
			else
			{
				result = Expression.Call(Translator.Shaper_Parameter, Translator.Shaper_GetPropertyValueWithErrorHandling.MakeGenericMethod(new Type[]
				{
					propertyType
				}), Expression.Constant(ordinal), Expression.Constant(propertyName), Expression.Constant(typeName));
			}
			return result;
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000CCE18 File Offset: 0x000CB018
		private static Expression Emit_Shaper_GetColumnValueWithErrorHandling(Type resultType, int ordinal, TypeUsage columnType)
		{
			PrimitiveTypeKind primitiveTypeKind;
			Expression result;
			if (Helper.IsSpatialType(columnType, out primitiveTypeKind))
			{
				primitiveTypeKind = (Helper.IsGeographicType((PrimitiveType)columnType.EdmType) ? PrimitiveTypeKind.Geography : PrimitiveTypeKind.Geometry);
				result = Expression.Call(Translator.Shaper_Parameter, Translator.Shaper_GetSpatialColumnValueWithErrorHandling.MakeGenericMethod(new Type[]
				{
					resultType
				}), Expression.Constant(ordinal), Expression.Constant(primitiveTypeKind, typeof(PrimitiveTypeKind)));
			}
			else
			{
				result = Expression.Call(Translator.Shaper_Parameter, Translator.Shaper_GetColumnValueWithErrorHandling.MakeGenericMethod(new Type[]
				{
					resultType
				}), new Expression[]
				{
					Expression.Constant(ordinal)
				});
			}
			return result;
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000CCEC0 File Offset: 0x000CB0C0
		private static Expression Emit_Shaper_GetGeographyColumnValue(int ordinal)
		{
			return Expression.Call(Translator.Shaper_Parameter, Translator.Shaper_GetGeographyColumnValue, new Expression[]
			{
				Expression.Constant(ordinal)
			});
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x000CCEF4 File Offset: 0x000CB0F4
		private static Expression Emit_Shaper_GetGeometryColumnValue(int ordinal)
		{
			return Expression.Call(Translator.Shaper_Parameter, Translator.Shaper_GetGeometryColumnValue, new Expression[]
			{
				Expression.Constant(ordinal)
			});
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000CCF28 File Offset: 0x000CB128
		private static Expression Emit_Shaper_GetState(int stateSlotNumber, Type type)
		{
			return Translator.Emit_EnsureType(Expression.ArrayIndex(Translator.Shaper_State, Expression.Constant(stateSlotNumber)), type);
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000CCF54 File Offset: 0x000CB154
		private static Expression Emit_Shaper_SetState(int stateSlotNumber, Expression value)
		{
			return Expression.Call(Translator.Shaper_Parameter, Translator.Shaper_SetState.MakeGenericMethod(new Type[]
			{
				value.Type
			}), Expression.Constant(stateSlotNumber), value);
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000CCF94 File Offset: 0x000CB194
		private static Expression Emit_Shaper_SetStatePassthrough(int stateSlotNumber, Expression value)
		{
			return Expression.Call(Translator.Shaper_Parameter, Translator.Shaper_SetStatePassthrough.MakeGenericMethod(new Type[]
			{
				value.Type
			}), Expression.Constant(stateSlotNumber), value);
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000CCFD4 File Offset: 0x000CB1D4
		private static TranslatorResult AcceptWithMappedType(Translator translator, ColumnMap columnMap, ColumnMap parent)
		{
			Type requestedType = translator.DetermineClrType(columnMap.Type);
			return columnMap.Accept<TranslatorResult, TranslatorArg>(translator, new TranslatorArg(requestedType));
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000CD000 File Offset: 0x000CB200
		internal override TranslatorResult Visit(ComplexTypeColumnMap columnMap, TranslatorArg arg)
		{
			Expression expression = null;
			if (columnMap.NullSentinel != null)
			{
				expression = Translator.Emit_Reader_IsDBNull(columnMap.NullSentinel);
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
				ConstructorInfo constructor = this.GetConstructor(type);
				List<MemberBinding> bindings = this.CreatePropertyBindings(columnMap, type, complexType.Properties);
				expression2 = Expression.MemberInit(Expression.New(constructor), bindings);
				if (expression != null)
				{
					expression2 = Expression.Condition(expression, Translator.Emit_NullConstant(expression2.Type), expression2);
				}
			}
			return new TranslatorResult(expression2, arg.RequestedType);
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000CD0A0 File Offset: 0x000CB2A0
		internal override TranslatorResult Visit(EntityColumnMap columnMap, TranslatorArg arg)
		{
			EntityIdentity entityIdentity = columnMap.EntityIdentity;
			Expression expression = null;
			Expression expression2 = Translator.Emit_EntityKey_ctor(this, entityIdentity, false, out expression);
			Expression returnedExpression;
			if (this.IsValueLayer)
			{
				Expression nullCheckExpression = Expression.Not(Translator.Emit_EntityKey_HasValue(entityIdentity.Keys));
				returnedExpression = this.BuildExpressionToGetRecordState(columnMap, expression2, expression, nullCheckExpression);
			}
			else
			{
				EntityType entityType = (EntityType)columnMap.Type.EdmType;
				ClrEntityType clrEntityType = (ClrEntityType)this.LookupObjectMapping(entityType).ClrType;
				Type clrType = clrEntityType.ClrType;
				List<MemberBinding> propertyBindings = this.CreatePropertyBindings(columnMap, clrType, entityType.Properties);
				EntityProxyTypeInfo proxyType = EntityProxyFactory.GetProxyType(clrEntityType);
				Expression expression3 = this.Emit_ConstructEntity(clrEntityType, propertyBindings, expression2, expression, arg, null);
				Expression expression4;
				if (proxyType == null)
				{
					expression4 = expression3;
				}
				else
				{
					Expression ifTrue = this.Emit_ConstructEntity(clrEntityType, propertyBindings, expression2, expression, arg, proxyType);
					expression4 = Expression.Condition(Translator.Shaper_ProxyCreationEnabled, ifTrue, expression3);
				}
				if (MergeOption.NoTracking != this._mergeOption)
				{
					Type c = (proxyType == null) ? clrType : proxyType.ProxyType;
					if (typeof(IEntityWithKey).IsAssignableFrom(c) && this._mergeOption != MergeOption.AppendOnly)
					{
						expression4 = Expression.Call(Translator.Shaper_Parameter, Translator.Shaper_HandleIEntityWithKey.MakeGenericMethod(new Type[]
						{
							clrType
						}), expression4, expression);
					}
					else if (this._mergeOption == MergeOption.AppendOnly)
					{
						LambdaExpression arg2 = this.CreateInlineDelegate(expression4);
						expression4 = Expression.Call(Translator.Shaper_Parameter, Translator.Shaper_HandleEntityAppendOnly.MakeGenericMethod(new Type[]
						{
							clrType
						}), arg2, expression2, expression);
					}
					else
					{
						expression4 = Expression.Call(Translator.Shaper_Parameter, Translator.Shaper_HandleEntity.MakeGenericMethod(new Type[]
						{
							clrType
						}), expression4, expression2, expression);
					}
				}
				else
				{
					expression4 = Expression.Call(Translator.Shaper_Parameter, Translator.Shaper_HandleEntityNoTracking.MakeGenericMethod(new Type[]
					{
						clrType
					}), new Expression[]
					{
						expression4
					});
				}
				returnedExpression = Expression.Condition(Translator.Emit_EntityKey_HasValue(entityIdentity.Keys), expression4, Translator.Emit_WrappedNullConstant(arg.RequestedType));
			}
			return new TranslatorResult(returnedExpression, arg.RequestedType);
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000CD28C File Offset: 0x000CB48C
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
				ConstructorInfo constructor = this.GetConstructor(clrType);
				expression = Expression.MemberInit(Expression.New(constructor), propertyBindings);
				actualType = clrType;
			}
			expression = Translator.Emit_EnsureTypeAndWrap(expression, entityKeyReader, entitySetReader, arg.RequestedType, clrType, actualType, (this._mergeOption == MergeOption.NoTracking) ? MergeOption.NoTracking : MergeOption.AppendOnly, flag);
			if (flag)
			{
				expression = Expression.Call(Expression.Constant(proxyTypeInfo), Translator.EntityProxyTypeInfo_SetEntityWrapper, new Expression[]
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

		// Token: 0x06003503 RID: 13571 RVA: 0x000CD340 File Offset: 0x000CB540
		private List<MemberBinding> CreatePropertyBindings(StructuredColumnMap columnMap, Type clrType, ReadOnlyMetadataCollection<EdmProperty> properties)
		{
			List<MemberBinding> list = new List<MemberBinding>(columnMap.Properties.Length);
			ObjectTypeMapping objectTypeMapping = this.LookupObjectMapping(columnMap.Type.EdmType);
			for (int i = 0; i < columnMap.Properties.Length; i++)
			{
				EdmProperty clrProperty = objectTypeMapping.GetPropertyMap(properties[i].Name).ClrProperty;
				MethodInfo methodInfo;
				Type type;
				LightweightCodeGenerator.ValidateSetterProperty(clrProperty.EntityDeclaringType, clrProperty.PropertySetterHandle, out methodInfo, out type);
				if (!LightweightCodeGenerator.IsPublic(methodInfo))
				{
					this._hasNonPublicMembers = true;
				}
				Expression expression = columnMap.Properties[i].Accept<TranslatorResult, TranslatorArg>(this, new TranslatorArg(type)).Expression;
				ScalarColumnMap scalarColumnMap = columnMap.Properties[i] as ScalarColumnMap;
				if (scalarColumnMap != null)
				{
					string propertyName = methodInfo.Name.Substring(4);
					Expression expressionWithErrorHandling = Translator.Emit_Shaper_GetPropertyValueWithErrorHandling(type, scalarColumnMap.ColumnPos, propertyName, methodInfo.DeclaringType.Name, scalarColumnMap.Type);
					this._currentCoordinatorScratchpad.AddExpressionWithErrorHandling(expression, expressionWithErrorHandling);
				}
				Type typeFromHandle = Type.GetTypeFromHandle(clrProperty.EntityDeclaringType);
				MemberBinding item = Expression.Bind(Translator.GetProperty(methodInfo, typeFromHandle), expression);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000CD460 File Offset: 0x000CB660
		private static PropertyInfo GetProperty(MethodInfo setterMethod, Type declaringType)
		{
			if (declaringType == null)
			{
				declaringType = setterMethod.DeclaringType;
			}
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			foreach (PropertyInfo propertyInfo in declaringType.GetProperties(bindingAttr))
			{
				if (propertyInfo.GetSetMethod(true) == setterMethod)
				{
					return propertyInfo;
				}
			}
			return null;
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000CD4B0 File Offset: 0x000CB6B0
		internal override TranslatorResult Visit(SimplePolymorphicColumnMap columnMap, TranslatorArg arg)
		{
			Expression expression = Translator.AcceptWithMappedType(this, columnMap.TypeDiscriminator, columnMap).Expression;
			Expression expression2;
			if (this.IsValueLayer)
			{
				expression2 = Translator.Emit_EnsureType(this.BuildExpressionToGetRecordState(columnMap, null, null, Expression.Constant(true)), arg.RequestedType);
			}
			else
			{
				expression2 = Translator.Emit_WrappedNullConstant(arg.RequestedType);
			}
			foreach (KeyValuePair<object, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
			{
				Type type = this.DetermineClrType(keyValuePair.Value.Type);
				if (!type.IsAbstract)
				{
					Expression expression3 = Expression.Constant(keyValuePair.Key, expression.Type);
					Expression test;
					if (expression.Type == typeof(string))
					{
						test = Expression.Call(Expression.Constant(TrailingSpaceStringComparer.Instance), Translator.IEqualityComparerOfString_Equals, expression3, expression);
					}
					else
					{
						test = Translator.Emit_Equal(expression3, expression);
					}
					expression2 = Expression.Condition(test, keyValuePair.Value.Accept<TranslatorResult, TranslatorArg>(this, arg).Expression, expression2);
				}
			}
			return new TranslatorResult(expression2, arg.RequestedType);
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000CD5E0 File Offset: 0x000CB7E0
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal override TranslatorResult Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, TranslatorArg arg)
		{
			MethodInfo methodInfo = Translator.Translator_MultipleDiscriminatorPolymorphicColumnMapHelper.MakeGenericMethod(new Type[]
			{
				arg.RequestedType
			});
			Expression returnedExpression = (Expression)methodInfo.Invoke(this, new object[]
			{
				columnMap,
				arg
			});
			return new TranslatorResult(returnedExpression, arg.RequestedType);
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000CD634 File Offset: 0x000CB834
		private Expression MultipleDiscriminatorPolymorphicColumnMapHelper<TElement>(MultipleDiscriminatorPolymorphicColumnMap columnMap, TranslatorArg arg)
		{
			Expression[] array = new Expression[columnMap.TypeDiscriminators.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = columnMap.TypeDiscriminators[i].Accept<TranslatorResult, TranslatorArg>(this, new TranslatorArg(typeof(object))).Expression;
			}
			Expression arg2 = Expression.NewArrayInit(typeof(object), array);
			List<Expression> list = new List<Expression>();
			Type typeFromHandle = typeof(KeyValuePair<EntityType, Func<Shaper, TElement>>);
			ConstructorInfo constructor = typeFromHandle.GetConstructor(new Type[]
			{
				typeof(EntityType),
				typeof(Func<Shaper, TElement>)
			});
			foreach (KeyValuePair<EntityType, TypedColumnMap> keyValuePair in columnMap.TypeChoices)
			{
				Expression body = Translator.Emit_EnsureType(Translator.AcceptWithMappedType(this, keyValuePair.Value, columnMap).UnwrappedExpression, typeof(TElement));
				LambdaExpression lambdaExpression = this.CreateInlineDelegate(body);
				Expression item = Expression.New(constructor, new Expression[]
				{
					Expression.Constant(keyValuePair.Key),
					lambdaExpression
				});
				list.Add(item);
			}
			MethodInfo method = Translator.Shaper_Discriminate.MakeGenericMethod(new Type[]
			{
				typeof(TElement)
			});
			return Expression.Call(Translator.Shaper_Parameter, method, arg2, Expression.Constant(columnMap.Discriminate), Expression.NewArrayInit(typeFromHandle, list));
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000CD7B0 File Offset: 0x000CB9B0
		internal override TranslatorResult Visit(RecordColumnMap columnMap, TranslatorArg arg)
		{
			Expression expression = null;
			if (columnMap.NullSentinel != null)
			{
				expression = Translator.Emit_Reader_IsDBNull(columnMap.NullSentinel);
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
					ifTrue = Translator.Emit_NullConstant(expression2.Type);
				}
				else
				{
					RowType spanRowType = (RowType)columnMap.Type.EdmType;
					if (this._spanIndex != null && this._spanIndex.HasSpanMap(spanRowType))
					{
						expression2 = this.HandleSpandexRecord(columnMap, arg, spanRowType);
						ifTrue = Translator.Emit_WrappedNullConstant(expression2.Type);
					}
					else
					{
						expression2 = this.HandleRegularRecord(columnMap, arg, spanRowType);
						ifTrue = Translator.Emit_NullConstant(expression2.Type);
					}
				}
				if (expression != null)
				{
					expression2 = Expression.Condition(expression, ifTrue, expression2);
				}
			}
			return new TranslatorResult(expression2, arg.RequestedType);
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000CD884 File Offset: 0x000CBA84
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
				array[i] = Expression.Call(Translator.Shaper_Parameter, Translator.Shaper_SetColumnValue, Expression.Constant(num), Expression.Constant(i), Expression.Coalesce(expression, Translator.DBNull_Value));
				array2[i] = columnMap.Properties[i].Name;
				array3[i] = columnMap.Properties[i].Type;
			}
			if (entityKeyReader != null)
			{
				array[num3 - 1] = Expression.Call(Translator.Shaper_Parameter, Translator.Shaper_SetEntityRecordInfo, Expression.Constant(num), entityKeyReader, entitySetReader);
			}
			recordStateScratchpad.GatherData = Translator.Emit_BitwiseOr(array);
			recordStateScratchpad.PropertyNames = array2;
			recordStateScratchpad.TypeUsages = array3;
			Expression expression2 = Expression.Call(Translator.Emit_Shaper_GetState(num, typeof(RecordState)), Translator.RecordState_GatherData, new Expression[]
			{
				Translator.Shaper_Parameter
			});
			if (nullCheckExpression != null)
			{
				Expression ifTrue = Expression.Call(Translator.Emit_Shaper_GetState(num, typeof(RecordState)), Translator.RecordState_SetNullRecord, new Expression[]
				{
					Translator.Shaper_Parameter
				});
				expression2 = Expression.Condition(nullCheckExpression, ifTrue, expression2);
			}
			return expression2;
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000CDA6C File Offset: 0x000CBC6C
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
			return initializerMetadata.Emit(this, list);
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000CDB1C File Offset: 0x000CBD1C
		private Expression HandleRegularRecord(RecordColumnMap columnMap, TranslatorArg arg, RowType spanRowType)
		{
			Expression[] array = new Expression[columnMap.Properties.Length];
			for (int i = 0; i < array.Length; i++)
			{
				Expression unwrappedExpression = Translator.AcceptWithMappedType(this, columnMap.Properties[i], columnMap).UnwrappedExpression;
				array[i] = Expression.Coalesce(Translator.Emit_EnsureType(unwrappedExpression, typeof(object)), Translator.DBNull_Value);
			}
			Expression expression = Expression.NewArrayInit(typeof(object), array);
			TypeUsage typeUsage = columnMap.Type;
			if (this._spanIndex != null)
			{
				typeUsage = (this._spanIndex.GetSpannedRowType(spanRowType) ?? typeUsage);
			}
			Expression expression2 = Expression.Constant(typeUsage, typeof(TypeUsage));
			return Translator.Emit_EnsureType(Expression.New(Translator.MaterializedDataRecord_ctor, new Expression[]
			{
				Translator.Shaper_Workspace,
				expression2,
				expression
			}), arg.RequestedType);
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x000CDBF4 File Offset: 0x000CBDF4
		private Expression HandleSpandexRecord(RecordColumnMap columnMap, TranslatorArg arg, RowType spanRowType)
		{
			Dictionary<int, AssociationEndMember> spanMap = this._spanIndex.GetSpanMap(spanRowType);
			Expression expression = columnMap.Properties[0].Accept<TranslatorResult, TranslatorArg>(this, arg).Expression;
			for (int i = 1; i < columnMap.Properties.Length; i++)
			{
				AssociationEndMember value = spanMap[i];
				TranslatorResult translatorResult = Translator.AcceptWithMappedType(this, columnMap.Properties[i], columnMap);
				Expression expression2 = translatorResult.Expression;
				CollectionTranslatorResult collectionTranslatorResult = translatorResult as CollectionTranslatorResult;
				if (collectionTranslatorResult != null)
				{
					Expression expressionToGetCoordinator = collectionTranslatorResult.ExpressionToGetCoordinator;
					Type type = expression2.Type.GetGenericArguments()[0];
					MethodInfo method = Translator.Shaper_HandleFullSpanCollection.MakeGenericMethod(new Type[]
					{
						arg.RequestedType,
						type
					});
					expression = Expression.Call(Translator.Shaper_Parameter, method, expression, expressionToGetCoordinator, Expression.Constant(value));
				}
				else if (typeof(EntityKey) == expression2.Type)
				{
					MethodInfo method2 = Translator.Shaper_HandleRelationshipSpan.MakeGenericMethod(new Type[]
					{
						arg.RequestedType
					});
					expression = Expression.Call(Translator.Shaper_Parameter, method2, expression, expression2, Expression.Constant(value));
				}
				else
				{
					MethodInfo method3 = Translator.Shaper_HandleFullSpanElement.MakeGenericMethod(new Type[]
					{
						arg.RequestedType,
						expression2.Type
					});
					expression = Expression.Call(Translator.Shaper_Parameter, method3, expression, expression2, Expression.Constant(value));
				}
			}
			return expression;
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x000CDD46 File Offset: 0x000CBF46
		internal override TranslatorResult Visit(SimpleCollectionColumnMap columnMap, TranslatorArg arg)
		{
			return this.ProcessCollectionColumnMap(columnMap, arg);
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x000CDD50 File Offset: 0x000CBF50
		internal override TranslatorResult Visit(DiscriminatedCollectionColumnMap columnMap, TranslatorArg arg)
		{
			return this.ProcessCollectionColumnMap(columnMap, arg, columnMap.Discriminator, columnMap.DiscriminatorValue);
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000CDD66 File Offset: 0x000CBF66
		private TranslatorResult ProcessCollectionColumnMap(CollectionColumnMap columnMap, TranslatorArg arg)
		{
			return this.ProcessCollectionColumnMap(columnMap, arg, null, null);
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x000CDD74 File Offset: 0x000CBF74
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
			Expression unconvertedExpression = columnMap2.Accept<TranslatorResult, TranslatorArg>(this, new TranslatorArg(type)).UnconvertedExpression;
			Expression[] array;
			if (columnMap.Keys != null)
			{
				array = new Expression[columnMap.Keys.Length];
				for (int i = 0; i < array.Length; i++)
				{
					Expression expression = Translator.AcceptWithMappedType(this, columnMap.Keys[i], columnMap).Expression;
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
				discriminator = Translator.AcceptWithMappedType(this, discriminatorColumnMap, columnMap).Expression;
			}
			Expression expression2 = this.BuildExpressionToGetCoordinator(type, unconvertedExpression, array, discriminator, discriminatorValue, coordinatorScratchpad);
			MethodInfo method = typeof(Coordinator<>).MakeGenericType(new Type[]
			{
				type
			}).GetMethod("GetElements", BindingFlags.Instance | BindingFlags.NonPublic);
			Expression expression3;
			if (this.IsValueLayer)
			{
				expression3 = expression2;
			}
			else
			{
				expression3 = Expression.Call(expression2, method);
				coordinatorScratchpad.Element = Translator.Emit_EnsureType(coordinatorScratchpad.Element, type);
				Type type2;
				if (EntityUtil.TryGetICollectionElementType(arg.RequestedType, out type2))
				{
					Type type3 = EntityUtil.DetermineCollectionType(arg.RequestedType);
					if (type3 == null)
					{
						throw EntityUtil.InvalidOperation(Strings.ObjectQuery_UnableToMaterializeArbitaryProjectionType(arg.RequestedType));
					}
					Type right = typeof(List<>).MakeGenericType(new Type[]
					{
						type2
					});
					if (type3 != right)
					{
						coordinatorScratchpad.InitializeCollection = Translator.Emit_EnsureType(Expression.New(this.GetConstructor(type3)), typeof(ICollection<>).MakeGenericType(new Type[]
						{
							type2
						}));
					}
					expression3 = Translator.Emit_EnsureType(expression3, arg.RequestedType);
				}
				else if (!arg.RequestedType.IsAssignableFrom(expression3.Type))
				{
					Type type4 = typeof(CompensatingCollection<>).MakeGenericType(new Type[]
					{
						type
					});
					ConstructorInfo constructor = type4.GetConstructors()[0];
					expression3 = Translator.Emit_EnsureType(Expression.New(constructor, new Expression[]
					{
						expression3
					}), type4);
				}
			}
			this.ExitCoordinatorTranslateScope();
			return new CollectionTranslatorResult(expression3, columnMap, arg.RequestedType, expression2);
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x000CDFD8 File Offset: 0x000CC1D8
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

		// Token: 0x06003512 RID: 13586 RVA: 0x000CE02C File Offset: 0x000CC22C
		private void EnterCoordinatorTranslateScope(CoordinatorScratchpad coordinatorScratchpad)
		{
			if (this._rootCoordinatorScratchpad == null)
			{
				coordinatorScratchpad.Depth = 0;
				this._rootCoordinatorScratchpad = coordinatorScratchpad;
				this._currentCoordinatorScratchpad = coordinatorScratchpad;
				return;
			}
			coordinatorScratchpad.Depth = this._currentCoordinatorScratchpad.Depth + 1;
			this._currentCoordinatorScratchpad.AddNestedCoordinator(coordinatorScratchpad);
			this._currentCoordinatorScratchpad = coordinatorScratchpad;
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x000CE07D File Offset: 0x000CC27D
		private void ExitCoordinatorTranslateScope()
		{
			this._currentCoordinatorScratchpad = this._currentCoordinatorScratchpad.Parent;
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x000CE090 File Offset: 0x000CC290
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
				list.Add(Translator.Emit_Shaper_SetState(stateSlotNumber2, expression));
				list2.Add(Translator.Emit_Equal(Translator.Emit_Shaper_GetState(stateSlotNumber2, expression.Type), expression));
			}
			coordinatorScratchpad.SetKeys = Translator.Emit_BitwiseOr(list);
			coordinatorScratchpad.CheckKeys = Translator.Emit_AndAlso(list2);
			if (discriminator != null)
			{
				coordinatorScratchpad.HasData = Translator.Emit_Equal(Expression.Constant(discriminatorValue, discriminator.Type), discriminator);
			}
			return Translator.Emit_Shaper_GetState(stateSlotNumber, typeof(Coordinator<>).MakeGenericType(new Type[]
			{
				elementType
			}));
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x000CE170 File Offset: 0x000CC370
		internal override TranslatorResult Visit(RefColumnMap columnMap, TranslatorArg arg)
		{
			EntityIdentity entityIdentity = columnMap.EntityIdentity;
			Expression expression;
			Expression returnedExpression = Expression.Condition(Translator.Emit_EntityKey_HasValue(entityIdentity.Keys), Translator.Emit_EntityKey_ctor(this, entityIdentity, true, out expression), Expression.Constant(null, typeof(EntityKey)));
			return new TranslatorResult(returnedExpression, arg.RequestedType);
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x000CE1BC File Offset: 0x000CC3BC
		internal override TranslatorResult Visit(ScalarColumnMap columnMap, TranslatorArg arg)
		{
			Type requestedType = arg.RequestedType;
			TypeUsage type = columnMap.Type;
			int columnPos = columnMap.ColumnPos;
			PrimitiveTypeKind primitiveTypeKind;
			Expression expression;
			if (Helper.IsSpatialType(type, out primitiveTypeKind))
			{
				expression = Translator.Emit_Conditional_NotDBNull(Helper.IsGeographicType((PrimitiveType)type.EdmType) ? Translator.Emit_EnsureType(Translator.Emit_Shaper_GetGeographyColumnValue(columnPos), requestedType) : Translator.Emit_EnsureType(Translator.Emit_Shaper_GetGeometryColumnValue(columnPos), requestedType), columnPos, requestedType);
			}
			else
			{
				bool flag;
				MethodInfo readerMethod = Translator.GetReaderMethod(requestedType, out flag);
				expression = Expression.Call(Translator.Shaper_Reader, readerMethod, new Expression[]
				{
					Expression.Constant(columnPos)
				});
				Type nonNullableType = TypeSystem.GetNonNullableType(requestedType);
				if (nonNullableType.IsEnum && nonNullableType != requestedType)
				{
					expression = Expression.Convert(expression, nonNullableType);
				}
				else if (requestedType == typeof(object) && !this.IsValueLayer && TypeSemantics.IsEnumerationType(type))
				{
					expression = Expression.Condition(Translator.Emit_Reader_IsDBNull(columnPos), expression, Expression.Convert(Expression.Convert(expression, TypeSystem.GetNonNullableType(this.DetermineClrType(type.EdmType))), typeof(object)));
				}
				expression = Translator.Emit_EnsureType(expression, requestedType);
				if (flag)
				{
					expression = Translator.Emit_Conditional_NotDBNull(expression, columnPos, requestedType);
				}
			}
			Expression expressionWithErrorHandling = Translator.Emit_Shaper_GetColumnValueWithErrorHandling(arg.RequestedType, columnPos, type);
			this._currentCoordinatorScratchpad.AddExpressionWithErrorHandling(expression, expressionWithErrorHandling);
			return new TranslatorResult(expression, requestedType);
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x000CE300 File Offset: 0x000CC500
		private static Expression Emit_Conditional_NotDBNull(Expression result, int ordinal, Type columnType)
		{
			result = Expression.Condition(Translator.Emit_Reader_IsDBNull(ordinal), Expression.Constant(TypeSystem.GetDefaultValue(columnType), columnType), result);
			return result;
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000CE320 File Offset: 0x000CC520
		internal static MethodInfo GetReaderMethod(Type type, out bool isNullable)
		{
			isNullable = false;
			Type underlyingType = Nullable.GetUnderlyingType(type);
			if (null != underlyingType)
			{
				isNullable = true;
				type = underlyingType;
			}
			MethodInfo result;
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return Translator.DbDataReader_GetBoolean;
			case TypeCode.Byte:
				return Translator.DbDataReader_GetByte;
			case TypeCode.Int16:
				return Translator.DbDataReader_GetInt16;
			case TypeCode.Int32:
				return Translator.DbDataReader_GetInt32;
			case TypeCode.Int64:
				return Translator.DbDataReader_GetInt64;
			case TypeCode.Single:
				return Translator.DbDataReader_GetFloat;
			case TypeCode.Double:
				return Translator.DbDataReader_GetDouble;
			case TypeCode.Decimal:
				return Translator.DbDataReader_GetDecimal;
			case TypeCode.DateTime:
				return Translator.DbDataReader_GetDateTime;
			case TypeCode.String:
				result = Translator.DbDataReader_GetString;
				isNullable = true;
				return result;
			}
			if (typeof(Guid) == type)
			{
				result = Translator.DbDataReader_GetGuid;
			}
			else if (typeof(TimeSpan) == type || typeof(DateTimeOffset) == type)
			{
				result = Translator.DbDataReader_GetValue;
			}
			else if (typeof(object) == type)
			{
				result = Translator.DbDataReader_GetValue;
			}
			else
			{
				result = Translator.DbDataReader_GetValue;
				isNullable = true;
			}
			return result;
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x00013B41 File Offset: 0x00011D41
		internal override TranslatorResult Visit(VarRefColumnMap columnMap, TranslatorArg arg)
		{
			throw EntityUtil.InvalidOperation(string.Empty);
		}

		// Token: 0x04001728 RID: 5928
		private readonly MetadataWorkspace _workspace;

		// Token: 0x04001729 RID: 5929
		private readonly SpanIndex _spanIndex;

		// Token: 0x0400172A RID: 5930
		private readonly MergeOption _mergeOption;

		// Token: 0x0400172B RID: 5931
		private readonly bool IsValueLayer;

		// Token: 0x0400172C RID: 5932
		private CoordinatorScratchpad _rootCoordinatorScratchpad;

		// Token: 0x0400172D RID: 5933
		private CoordinatorScratchpad _currentCoordinatorScratchpad;

		// Token: 0x0400172E RID: 5934
		private int _stateSlotCount;

		// Token: 0x0400172F RID: 5935
		private bool _hasNonPublicMembers;

		// Token: 0x04001730 RID: 5936
		private readonly Dictionary<EdmType, ObjectTypeMapping> _objectTypeMappings = new Dictionary<EdmType, ObjectTypeMapping>();

		// Token: 0x04001731 RID: 5937
		private static readonly MethodInfo DbDataReader_GetValue = typeof(DbDataReader).GetMethod("GetValue");

		// Token: 0x04001732 RID: 5938
		private static readonly MethodInfo DbDataReader_GetString = typeof(DbDataReader).GetMethod("GetString");

		// Token: 0x04001733 RID: 5939
		private static readonly MethodInfo DbDataReader_GetInt16 = typeof(DbDataReader).GetMethod("GetInt16");

		// Token: 0x04001734 RID: 5940
		private static readonly MethodInfo DbDataReader_GetInt32 = typeof(DbDataReader).GetMethod("GetInt32");

		// Token: 0x04001735 RID: 5941
		private static readonly MethodInfo DbDataReader_GetInt64 = typeof(DbDataReader).GetMethod("GetInt64");

		// Token: 0x04001736 RID: 5942
		private static readonly MethodInfo DbDataReader_GetBoolean = typeof(DbDataReader).GetMethod("GetBoolean");

		// Token: 0x04001737 RID: 5943
		private static readonly MethodInfo DbDataReader_GetDecimal = typeof(DbDataReader).GetMethod("GetDecimal");

		// Token: 0x04001738 RID: 5944
		private static readonly MethodInfo DbDataReader_GetFloat = typeof(DbDataReader).GetMethod("GetFloat");

		// Token: 0x04001739 RID: 5945
		private static readonly MethodInfo DbDataReader_GetDouble = typeof(DbDataReader).GetMethod("GetDouble");

		// Token: 0x0400173A RID: 5946
		private static readonly MethodInfo DbDataReader_GetDateTime = typeof(DbDataReader).GetMethod("GetDateTime");

		// Token: 0x0400173B RID: 5947
		private static readonly MethodInfo DbDataReader_GetGuid = typeof(DbDataReader).GetMethod("GetGuid");

		// Token: 0x0400173C RID: 5948
		private static readonly MethodInfo DbDataReader_GetByte = typeof(DbDataReader).GetMethod("GetByte");

		// Token: 0x0400173D RID: 5949
		private static readonly MethodInfo DbDataReader_IsDBNull = typeof(DbDataReader).GetMethod("IsDBNull");

		// Token: 0x0400173E RID: 5950
		private static readonly ConstructorInfo EntityKey_ctor_SingleKey = typeof(EntityKey).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
		{
			typeof(EntitySet),
			typeof(object)
		}, null);

		// Token: 0x0400173F RID: 5951
		private static readonly ConstructorInfo EntityKey_ctor_CompositeKey = typeof(EntityKey).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
		{
			typeof(EntitySet),
			typeof(object[])
		}, null);

		// Token: 0x04001740 RID: 5952
		private static readonly MethodInfo IEntityKeyWithKey_EntityKey = typeof(IEntityWithKey).GetProperty("EntityKey").GetSetMethod();

		// Token: 0x04001741 RID: 5953
		private static readonly MethodInfo IEqualityComparerOfString_Equals = typeof(IEqualityComparer<string>).GetMethod("Equals", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04001742 RID: 5954
		private static readonly ConstructorInfo MaterializedDataRecord_ctor = typeof(MaterializedDataRecord).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
		{
			typeof(MetadataWorkspace),
			typeof(TypeUsage),
			typeof(object[])
		}, null);

		// Token: 0x04001743 RID: 5955
		private static readonly MethodInfo RecordState_GatherData = typeof(RecordState).GetMethod("GatherData", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04001744 RID: 5956
		private static readonly MethodInfo RecordState_SetNullRecord = typeof(RecordState).GetMethod("SetNullRecord", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04001745 RID: 5957
		private static readonly MethodInfo Shaper_Discriminate = typeof(Shaper).GetMethod("Discriminate");

		// Token: 0x04001746 RID: 5958
		private static readonly MethodInfo Shaper_GetPropertyValueWithErrorHandling = typeof(Shaper).GetMethod("GetPropertyValueWithErrorHandling");

		// Token: 0x04001747 RID: 5959
		private static readonly MethodInfo Shaper_GetColumnValueWithErrorHandling = typeof(Shaper).GetMethod("GetColumnValueWithErrorHandling");

		// Token: 0x04001748 RID: 5960
		private static readonly MethodInfo Shaper_GetGeographyColumnValue = typeof(Shaper).GetMethod("GetGeographyColumnValue");

		// Token: 0x04001749 RID: 5961
		private static readonly MethodInfo Shaper_GetGeometryColumnValue = typeof(Shaper).GetMethod("GetGeometryColumnValue");

		// Token: 0x0400174A RID: 5962
		private static readonly MethodInfo Shaper_GetSpatialColumnValueWithErrorHandling = typeof(Shaper).GetMethod("GetSpatialColumnValueWithErrorHandling");

		// Token: 0x0400174B RID: 5963
		private static readonly MethodInfo Shaper_GetSpatialPropertyValueWithErrorHandling = typeof(Shaper).GetMethod("GetSpatialPropertyValueWithErrorHandling");

		// Token: 0x0400174C RID: 5964
		private static readonly MethodInfo Shaper_HandleEntity = typeof(Shaper).GetMethod("HandleEntity");

		// Token: 0x0400174D RID: 5965
		private static readonly MethodInfo Shaper_HandleEntityAppendOnly = typeof(Shaper).GetMethod("HandleEntityAppendOnly");

		// Token: 0x0400174E RID: 5966
		private static readonly MethodInfo Shaper_HandleEntityNoTracking = typeof(Shaper).GetMethod("HandleEntityNoTracking");

		// Token: 0x0400174F RID: 5967
		private static readonly MethodInfo Shaper_HandleFullSpanCollection = typeof(Shaper).GetMethod("HandleFullSpanCollection");

		// Token: 0x04001750 RID: 5968
		private static readonly MethodInfo Shaper_HandleFullSpanElement = typeof(Shaper).GetMethod("HandleFullSpanElement");

		// Token: 0x04001751 RID: 5969
		private static readonly MethodInfo Shaper_HandleIEntityWithKey = typeof(Shaper).GetMethod("HandleIEntityWithKey");

		// Token: 0x04001752 RID: 5970
		private static readonly MethodInfo Shaper_HandleRelationshipSpan = typeof(Shaper).GetMethod("HandleRelationshipSpan");

		// Token: 0x04001753 RID: 5971
		private static readonly MethodInfo Shaper_SetColumnValue = typeof(Shaper).GetMethod("SetColumnValue");

		// Token: 0x04001754 RID: 5972
		private static readonly MethodInfo Shaper_SetEntityRecordInfo = typeof(Shaper).GetMethod("SetEntityRecordInfo");

		// Token: 0x04001755 RID: 5973
		private static readonly MethodInfo Shaper_SetState = typeof(Shaper).GetMethod("SetState");

		// Token: 0x04001756 RID: 5974
		private static readonly MethodInfo Shaper_SetStatePassthrough = typeof(Shaper).GetMethod("SetStatePassthrough");

		// Token: 0x04001757 RID: 5975
		private static readonly MethodInfo Translator_BinaryEquals = typeof(Translator).GetMethod("BinaryEquals", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04001758 RID: 5976
		private static readonly MethodInfo Translator_CheckedConvert = typeof(Translator).GetMethod("CheckedConvert", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04001759 RID: 5977
		private static readonly MethodInfo Translator_Compile = typeof(Translator).GetMethod("Compile", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[]
		{
			typeof(Expression)
		}, null);

		// Token: 0x0400175A RID: 5978
		private static readonly MethodInfo Translator_MultipleDiscriminatorPolymorphicColumnMapHelper = typeof(Translator).GetMethod("MultipleDiscriminatorPolymorphicColumnMapHelper", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400175B RID: 5979
		private static readonly MethodInfo Translator_TypedCreateInlineDelegate = typeof(Translator).GetMethod("TypedCreateInlineDelegate", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400175C RID: 5980
		private static readonly PropertyInfo EntityWrapperFactory_NullWrapper = typeof(EntityWrapperFactory).GetProperty("NullWrapper", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x0400175D RID: 5981
		private static readonly PropertyInfo IEntityWrapper_Entity = typeof(IEntityWrapper).GetProperty("Entity");

		// Token: 0x0400175E RID: 5982
		private static readonly MethodInfo EntityProxyTypeInfo_SetEntityWrapper = typeof(EntityProxyTypeInfo).GetMethod("SetEntityWrapper", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400175F RID: 5983
		private static readonly ConstructorInfo PocoPropertyAccessorStrategy_ctor = typeof(PocoPropertyAccessorStrategy).GetConstructor(new Type[]
		{
			typeof(object)
		});

		// Token: 0x04001760 RID: 5984
		private static readonly ConstructorInfo EntityWithChangeTrackerStrategy_ctor = typeof(EntityWithChangeTrackerStrategy).GetConstructor(new Type[]
		{
			typeof(IEntityWithChangeTracker)
		});

		// Token: 0x04001761 RID: 5985
		private static readonly ConstructorInfo EntityWithKeyStrategy_ctor = typeof(EntityWithKeyStrategy).GetConstructor(new Type[]
		{
			typeof(IEntityWithKey)
		});

		// Token: 0x04001762 RID: 5986
		private static readonly ConstructorInfo PocoEntityKeyStrategy_ctor = typeof(PocoEntityKeyStrategy).GetConstructor(new Type[0]);

		// Token: 0x04001763 RID: 5987
		private static readonly PropertyInfo SnapshotChangeTrackingStrategy_Instance = typeof(SnapshotChangeTrackingStrategy).GetProperty("Instance", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x04001764 RID: 5988
		private static readonly MethodInfo EntityWrapperFactory_GetPocoPropertyAccessorStrategyFunc = typeof(EntityWrapperFactory).GetMethod("GetPocoPropertyAccessorStrategyFunc", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04001765 RID: 5989
		private static readonly MethodInfo EntityWrapperFactory_GetNullPropertyAccessorStrategyFunc = typeof(EntityWrapperFactory).GetMethod("GetNullPropertyAccessorStrategyFunc", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04001766 RID: 5990
		private static readonly MethodInfo EntityWrapperFactory_GetEntityWithChangeTrackerStrategyFunc = typeof(EntityWrapperFactory).GetMethod("GetEntityWithChangeTrackerStrategyFunc", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04001767 RID: 5991
		private static readonly MethodInfo EntityWrapperFactory_GetSnapshotChangeTrackingStrategyFunc = typeof(EntityWrapperFactory).GetMethod("GetSnapshotChangeTrackingStrategyFunc", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04001768 RID: 5992
		private static readonly MethodInfo EntityWrapperFactory_GetEntityWithKeyStrategyStrategyFunc = typeof(EntityWrapperFactory).GetMethod("GetEntityWithKeyStrategyStrategyFunc", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04001769 RID: 5993
		private static readonly MethodInfo EntityWrapperFactory_GetPocoEntityKeyStrategyFunc = typeof(EntityWrapperFactory).GetMethod("GetPocoEntityKeyStrategyFunc", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x0400176A RID: 5994
		private static readonly Expression DBNull_Value = Expression.Constant(DBNull.Value, typeof(object));

		// Token: 0x0400176B RID: 5995
		internal static readonly ParameterExpression Shaper_Parameter = Expression.Parameter(typeof(Shaper), "shaper");

		// Token: 0x0400176C RID: 5996
		private static readonly ParameterExpression EntityParameter = Expression.Parameter(typeof(object), "entity");

		// Token: 0x0400176D RID: 5997
		internal static readonly Expression Shaper_Reader = Expression.Field(Translator.Shaper_Parameter, typeof(Shaper).GetField("Reader"));

		// Token: 0x0400176E RID: 5998
		private static readonly Expression Shaper_Workspace = Expression.Field(Translator.Shaper_Parameter, typeof(Shaper).GetField("Workspace"));

		// Token: 0x0400176F RID: 5999
		private static readonly Expression Shaper_State = Expression.Field(Translator.Shaper_Parameter, typeof(Shaper).GetField("State"));

		// Token: 0x04001770 RID: 6000
		private static readonly Expression Shaper_Context = Expression.Field(Translator.Shaper_Parameter, typeof(Shaper).GetField("Context"));

		// Token: 0x04001771 RID: 6001
		private static readonly Expression Shaper_Context_Options = Expression.Property(Translator.Shaper_Context, typeof(ObjectContext).GetProperty("ContextOptions"));

		// Token: 0x04001772 RID: 6002
		private static readonly Expression Shaper_ProxyCreationEnabled = Expression.Property(Translator.Shaper_Context_Options, typeof(ObjectContextOptions).GetProperty("ProxyCreationEnabled"));
	}
}
