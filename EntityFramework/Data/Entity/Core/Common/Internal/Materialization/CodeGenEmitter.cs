using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020001D9 RID: 473
	internal static class CodeGenEmitter
	{
		// Token: 0x060010A6 RID: 4262 RVA: 0x00046A18 File Offset: 0x00044C18
		internal static bool BinaryEquals(byte[] left, byte[] right)
		{
			if (left == null)
			{
				return null == right;
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

		// Token: 0x060010A7 RID: 4263 RVA: 0x00046A55 File Offset: 0x00044C55
		internal static Func<Shaper, TResult> Compile<TResult>(Expression body)
		{
			return CodeGenEmitter.BuildShaperLambda<TResult>(body).Compile();
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00046A64 File Offset: 0x00044C64
		internal static Expression<Func<Shaper, TResult>> BuildShaperLambda<TResult>(Expression body)
		{
			if (body != null)
			{
				return Expression.Lambda<Func<Shaper, TResult>>(body, new ParameterExpression[]
				{
					CodeGenEmitter.Shaper_Parameter
				});
			}
			return null;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00046A8C File Offset: 0x00044C8C
		internal static object Compile(Type resultType, Expression body)
		{
			MethodInfo methodInfo = CodeGenEmitter.CodeGenEmitter_Compile.MakeGenericMethod(new Type[]
			{
				resultType
			});
			return methodInfo.Invoke(null, new object[]
			{
				body
			});
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00046AC4 File Offset: 0x00044CC4
		internal static Expression Emit_AndAlso(IEnumerable<Expression> operands)
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

		// Token: 0x060010AB RID: 4267 RVA: 0x00046B18 File Offset: 0x00044D18
		internal static Expression Emit_BitwiseOr(IEnumerable<Expression> operands)
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

		// Token: 0x060010AC RID: 4268 RVA: 0x00046B6C File Offset: 0x00044D6C
		internal static Expression Emit_NullConstant(Type type)
		{
			Expression result;
			if (type.IsNullable())
			{
				result = Expression.Constant(null, type);
			}
			else
			{
				result = CodeGenEmitter.Emit_EnsureType(Expression.Constant(null, typeof(object)), type);
			}
			return result;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00046BA3 File Offset: 0x00044DA3
		internal static Expression Emit_WrappedNullConstant()
		{
			return Expression.Property(null, CodeGenEmitter.EntityWrapperFactory_NullWrapper);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00046BB0 File Offset: 0x00044DB0
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
					MethodInfo method = CodeGenEmitter.CodeGenEmitter_CheckedConvert.MakeGenericMethod(new Type[]
					{
						input.Type,
						type
					});
					result = Expression.Call(method, input);
				}
			}
			return result;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00046C28 File Offset: 0x00044E28
		internal static Expression Emit_EnsureTypeAndWrap(Expression input, Expression keyReader, Expression entitySetReader, Type requestedType, Type identityType, Type actualType, MergeOption mergeOption, bool isProxy)
		{
			Expression input2 = CodeGenEmitter.Emit_EnsureType(input, requestedType);
			if (!requestedType.IsClass())
			{
				input2 = CodeGenEmitter.Emit_EnsureType(input, typeof(object));
			}
			input2 = CodeGenEmitter.Emit_EnsureType(input2, actualType);
			return CodeGenEmitter.CreateEntityWrapper(input2, keyReader, entitySetReader, actualType, identityType, mergeOption, isProxy);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00046C70 File Offset: 0x00044E70
		internal static Expression CreateEntityWrapper(Expression input, Expression keyReader, Expression entitySetReader, Type actualType, Type identityType, MergeOption mergeOption, bool isProxy)
		{
			bool flag = actualType.OverridesEqualsOrGetHashCode();
			bool flag2 = typeof(IEntityWithKey).IsAssignableFrom(actualType);
			bool flag3 = typeof(IEntityWithRelationships).IsAssignableFrom(actualType);
			bool flag4 = typeof(IEntityWithChangeTracker).IsAssignableFrom(actualType);
			Expression expression;
			if (flag3 && flag4 && flag2 && !isProxy)
			{
				Type type = typeof(LightweightEntityWrapper<>).MakeGenericType(new Type[]
				{
					actualType
				});
				ConstructorInfo declaredConstructor = type.GetDeclaredConstructor(new Type[]
				{
					actualType,
					typeof(EntityKey),
					typeof(EntitySet),
					typeof(ObjectContext),
					typeof(MergeOption),
					typeof(Type),
					typeof(bool)
				});
				expression = Expression.New(declaredConstructor, new Expression[]
				{
					input,
					keyReader,
					entitySetReader,
					CodeGenEmitter.Shaper_Context,
					Expression.Constant(mergeOption, typeof(MergeOption)),
					Expression.Constant(identityType, typeof(Type)),
					Expression.Constant(flag, typeof(bool))
				});
			}
			else
			{
				Expression expression2 = (!flag3 || isProxy) ? Expression.Call(CodeGenEmitter.EntityWrapperFactory_GetPocoPropertyAccessorStrategyFunc, new Expression[0]) : Expression.Call(CodeGenEmitter.EntityWrapperFactory_GetNullPropertyAccessorStrategyFunc, new Expression[0]);
				Expression expression3 = flag2 ? Expression.Call(CodeGenEmitter.EntityWrapperFactory_GetEntityWithKeyStrategyStrategyFunc, new Expression[0]) : Expression.Call(CodeGenEmitter.EntityWrapperFactory_GetPocoEntityKeyStrategyFunc, new Expression[0]);
				Expression expression4 = flag4 ? Expression.Call(CodeGenEmitter.EntityWrapperFactory_GetEntityWithChangeTrackerStrategyFunc, new Expression[0]) : Expression.Call(CodeGenEmitter.EntityWrapperFactory_GetSnapshotChangeTrackingStrategyFunc, new Expression[0]);
				Type type2 = flag3 ? typeof(EntityWrapperWithRelationships<>).MakeGenericType(new Type[]
				{
					actualType
				}) : typeof(EntityWrapperWithoutRelationships<>).MakeGenericType(new Type[]
				{
					actualType
				});
				ConstructorInfo declaredConstructor2 = type2.GetDeclaredConstructor(new Type[]
				{
					actualType,
					typeof(EntityKey),
					typeof(EntitySet),
					typeof(ObjectContext),
					typeof(MergeOption),
					typeof(Type),
					typeof(Func<object, IPropertyAccessorStrategy>),
					typeof(Func<object, IChangeTrackingStrategy>),
					typeof(Func<object, IEntityKeyStrategy>),
					typeof(bool)
				});
				expression = Expression.New(declaredConstructor2, new Expression[]
				{
					input,
					keyReader,
					entitySetReader,
					CodeGenEmitter.Shaper_Context,
					Expression.Constant(mergeOption, typeof(MergeOption)),
					Expression.Constant(identityType, typeof(Type)),
					expression2,
					expression4,
					expression3,
					Expression.Constant(flag, typeof(bool))
				});
			}
			return Expression.Convert(expression, typeof(IEntityWrapper));
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00046FC2 File Offset: 0x000451C2
		internal static Expression Emit_UnwrapAndEnsureType(Expression input, Type type)
		{
			return CodeGenEmitter.Emit_EnsureType(Expression.Property(input, CodeGenEmitter.IEntityWrapper_Entity), type);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00046FD8 File Offset: 0x000451D8
		internal static TTarget CheckedConvert<TSource, TTarget>(TSource value)
		{
			TTarget result;
			try
			{
				result = (TTarget)((object)value);
			}
			catch (InvalidCastException)
			{
				Type type = value.GetType();
				if (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(CompensatingCollection<>))
				{
					type = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments());
				}
				throw EntityUtil.ValueInvalidCast(type, typeof(TTarget));
			}
			catch (NullReferenceException)
			{
				throw new InvalidOperationException(Strings.Materializer_NullReferenceCast(typeof(TTarget).Name));
			}
			return result;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00047080 File Offset: 0x00045280
		internal static Expression Emit_Equal(Expression left, Expression right)
		{
			Expression result;
			if (typeof(byte[]) == left.Type)
			{
				result = Expression.Call(CodeGenEmitter.CodeGenEmitter_BinaryEquals, left, right);
			}
			else
			{
				result = Expression.Equal(left, right);
			}
			return result;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000470BC File Offset: 0x000452BC
		internal static Expression Emit_EntityKey_HasValue(SimpleColumnMap[] keyColumns)
		{
			Expression expression = CodeGenEmitter.Emit_Reader_IsDBNull(keyColumns[0]);
			return Expression.Not(expression);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000470DC File Offset: 0x000452DC
		internal static Expression Emit_Reader_GetValue(int ordinal, Type type)
		{
			return CodeGenEmitter.Emit_EnsureType(Expression.Call(CodeGenEmitter.Shaper_Reader, CodeGenEmitter.DbDataReader_GetValue, new Expression[]
			{
				Expression.Constant(ordinal)
			}), type);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00047118 File Offset: 0x00045318
		internal static Expression Emit_Reader_IsDBNull(int ordinal)
		{
			return Expression.Call(CodeGenEmitter.Shaper_Reader, CodeGenEmitter.DbDataReader_IsDBNull, new Expression[]
			{
				Expression.Constant(ordinal)
			});
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0004714C File Offset: 0x0004534C
		internal static Expression Emit_Reader_IsDBNull(ColumnMap columnMap)
		{
			return CodeGenEmitter.Emit_Reader_IsDBNull(((ScalarColumnMap)columnMap).ColumnPos);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0004716B File Offset: 0x0004536B
		internal static Expression Emit_Conditional_NotDBNull(Expression result, int ordinal, Type columnType)
		{
			result = Expression.Condition(CodeGenEmitter.Emit_Reader_IsDBNull(ordinal), Expression.Constant(TypeSystem.GetDefaultValue(columnType), columnType), result);
			return result;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00047188 File Offset: 0x00045388
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
				return CodeGenEmitter.DbDataReader_GetBoolean;
			case TypeCode.Byte:
				return CodeGenEmitter.DbDataReader_GetByte;
			case TypeCode.Int16:
				return CodeGenEmitter.DbDataReader_GetInt16;
			case TypeCode.Int32:
				return CodeGenEmitter.DbDataReader_GetInt32;
			case TypeCode.Int64:
				return CodeGenEmitter.DbDataReader_GetInt64;
			case TypeCode.Single:
				return CodeGenEmitter.DbDataReader_GetFloat;
			case TypeCode.Double:
				return CodeGenEmitter.DbDataReader_GetDouble;
			case TypeCode.Decimal:
				return CodeGenEmitter.DbDataReader_GetDecimal;
			case TypeCode.DateTime:
				return CodeGenEmitter.DbDataReader_GetDateTime;
			case TypeCode.String:
				result = CodeGenEmitter.DbDataReader_GetString;
				isNullable = true;
				return result;
			}
			if (typeof(Guid) == type)
			{
				result = CodeGenEmitter.DbDataReader_GetGuid;
			}
			else if (typeof(TimeSpan) == type || typeof(DateTimeOffset) == type)
			{
				result = CodeGenEmitter.DbDataReader_GetValue;
			}
			else if (typeof(object) == type)
			{
				result = CodeGenEmitter.DbDataReader_GetValue;
			}
			else
			{
				result = CodeGenEmitter.DbDataReader_GetValue;
				isNullable = true;
			}
			return result;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x000472D4 File Offset: 0x000454D4
		internal static Expression Emit_Shaper_GetPropertyValueWithErrorHandling(Type propertyType, int ordinal, string propertyName, string typeName, TypeUsage columnType)
		{
			PrimitiveTypeKind primitiveTypeKind;
			Expression result;
			if (Helper.IsSpatialType(columnType, out primitiveTypeKind))
			{
				result = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_GetSpatialPropertyValueWithErrorHandling.MakeGenericMethod(new Type[]
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
				result = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_GetPropertyValueWithErrorHandling.MakeGenericMethod(new Type[]
				{
					propertyType
				}), Expression.Constant(ordinal), Expression.Constant(propertyName), Expression.Constant(typeName));
			}
			return result;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0004738C File Offset: 0x0004558C
		internal static Expression Emit_Shaper_GetColumnValueWithErrorHandling(Type resultType, int ordinal, TypeUsage columnType)
		{
			PrimitiveTypeKind primitiveTypeKind;
			Expression result;
			if (Helper.IsSpatialType(columnType, out primitiveTypeKind))
			{
				primitiveTypeKind = (Helper.IsGeographicType((PrimitiveType)columnType.EdmType) ? PrimitiveTypeKind.Geography : PrimitiveTypeKind.Geometry);
				result = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_GetSpatialColumnValueWithErrorHandling.MakeGenericMethod(new Type[]
				{
					resultType
				}), Expression.Constant(ordinal), Expression.Constant(primitiveTypeKind, typeof(PrimitiveTypeKind)));
			}
			else
			{
				result = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_GetColumnValueWithErrorHandling.MakeGenericMethod(new Type[]
				{
					resultType
				}), new Expression[]
				{
					Expression.Constant(ordinal)
				});
			}
			return result;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0004743C File Offset: 0x0004563C
		internal static Expression Emit_Shaper_GetGeographyColumnValue(int ordinal)
		{
			return Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_GetGeographyColumnValue, new Expression[]
			{
				Expression.Constant(ordinal)
			});
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00047470 File Offset: 0x00045670
		internal static Expression Emit_Shaper_GetGeometryColumnValue(int ordinal)
		{
			return Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_GetGeometryColumnValue, new Expression[]
			{
				Expression.Constant(ordinal)
			});
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000474A4 File Offset: 0x000456A4
		internal static Expression Emit_Shaper_GetState(int stateSlotNumber, Type type)
		{
			return CodeGenEmitter.Emit_EnsureType(Expression.ArrayIndex(CodeGenEmitter.Shaper_State, Expression.Constant(stateSlotNumber)), type);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x000474D0 File Offset: 0x000456D0
		internal static Expression Emit_Shaper_SetState(int stateSlotNumber, Expression value)
		{
			return Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_SetState.MakeGenericMethod(new Type[]
			{
				value.Type
			}), Expression.Constant(stateSlotNumber), value);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00047510 File Offset: 0x00045710
		internal static Expression Emit_Shaper_SetStatePassthrough(int stateSlotNumber, Expression value)
		{
			return Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_SetStatePassthrough.MakeGenericMethod(new Type[]
			{
				value.Type
			}), Expression.Constant(stateSlotNumber), value);
		}

		// Token: 0x040004BA RID: 1210
		internal static readonly MethodInfo CodeGenEmitter_BinaryEquals = typeof(CodeGenEmitter).GetOnlyDeclaredMethod("BinaryEquals");

		// Token: 0x040004BB RID: 1211
		internal static readonly MethodInfo CodeGenEmitter_CheckedConvert = typeof(CodeGenEmitter).GetOnlyDeclaredMethod("CheckedConvert");

		// Token: 0x040004BC RID: 1212
		internal static readonly MethodInfo CodeGenEmitter_Compile = typeof(CodeGenEmitter).GetDeclaredMethod("Compile", new Type[]
		{
			typeof(Expression)
		});

		// Token: 0x040004BD RID: 1213
		internal static readonly MethodInfo DbDataReader_GetValue = typeof(DbDataReader).GetOnlyDeclaredMethod("GetValue");

		// Token: 0x040004BE RID: 1214
		internal static readonly MethodInfo DbDataReader_GetString = typeof(DbDataReader).GetOnlyDeclaredMethod("GetString");

		// Token: 0x040004BF RID: 1215
		internal static readonly MethodInfo DbDataReader_GetInt16 = typeof(DbDataReader).GetOnlyDeclaredMethod("GetInt16");

		// Token: 0x040004C0 RID: 1216
		internal static readonly MethodInfo DbDataReader_GetInt32 = typeof(DbDataReader).GetOnlyDeclaredMethod("GetInt32");

		// Token: 0x040004C1 RID: 1217
		internal static readonly MethodInfo DbDataReader_GetInt64 = typeof(DbDataReader).GetOnlyDeclaredMethod("GetInt64");

		// Token: 0x040004C2 RID: 1218
		internal static readonly MethodInfo DbDataReader_GetBoolean = typeof(DbDataReader).GetOnlyDeclaredMethod("GetBoolean");

		// Token: 0x040004C3 RID: 1219
		internal static readonly MethodInfo DbDataReader_GetDecimal = typeof(DbDataReader).GetOnlyDeclaredMethod("GetDecimal");

		// Token: 0x040004C4 RID: 1220
		internal static readonly MethodInfo DbDataReader_GetFloat = typeof(DbDataReader).GetOnlyDeclaredMethod("GetFloat");

		// Token: 0x040004C5 RID: 1221
		internal static readonly MethodInfo DbDataReader_GetDouble = typeof(DbDataReader).GetOnlyDeclaredMethod("GetDouble");

		// Token: 0x040004C6 RID: 1222
		internal static readonly MethodInfo DbDataReader_GetDateTime = typeof(DbDataReader).GetOnlyDeclaredMethod("GetDateTime");

		// Token: 0x040004C7 RID: 1223
		internal static readonly MethodInfo DbDataReader_GetGuid = typeof(DbDataReader).GetOnlyDeclaredMethod("GetGuid");

		// Token: 0x040004C8 RID: 1224
		internal static readonly MethodInfo DbDataReader_GetByte = typeof(DbDataReader).GetOnlyDeclaredMethod("GetByte");

		// Token: 0x040004C9 RID: 1225
		internal static readonly MethodInfo DbDataReader_IsDBNull = typeof(DbDataReader).GetOnlyDeclaredMethod("IsDBNull");

		// Token: 0x040004CA RID: 1226
		internal static readonly ConstructorInfo EntityKey_ctor_SingleKey = typeof(EntityKey).GetDeclaredConstructor(new Type[]
		{
			typeof(EntitySetBase),
			typeof(object)
		});

		// Token: 0x040004CB RID: 1227
		internal static readonly ConstructorInfo EntityKey_ctor_CompositeKey = typeof(EntityKey).GetDeclaredConstructor(new Type[]
		{
			typeof(EntitySetBase),
			typeof(object[])
		});

		// Token: 0x040004CC RID: 1228
		internal static readonly MethodInfo EntityWrapperFactory_GetEntityWithChangeTrackerStrategyFunc = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("GetEntityWithChangeTrackerStrategyFunc");

		// Token: 0x040004CD RID: 1229
		internal static readonly MethodInfo EntityWrapperFactory_GetEntityWithKeyStrategyStrategyFunc = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("GetEntityWithKeyStrategyStrategyFunc");

		// Token: 0x040004CE RID: 1230
		internal static readonly MethodInfo EntityProxyTypeInfo_SetEntityWrapper = typeof(EntityProxyTypeInfo).GetOnlyDeclaredMethod("SetEntityWrapper");

		// Token: 0x040004CF RID: 1231
		internal static readonly MethodInfo EntityWrapperFactory_GetNullPropertyAccessorStrategyFunc = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("GetNullPropertyAccessorStrategyFunc");

		// Token: 0x040004D0 RID: 1232
		internal static readonly MethodInfo EntityWrapperFactory_GetPocoEntityKeyStrategyFunc = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("GetPocoEntityKeyStrategyFunc");

		// Token: 0x040004D1 RID: 1233
		internal static readonly MethodInfo EntityWrapperFactory_GetPocoPropertyAccessorStrategyFunc = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("GetPocoPropertyAccessorStrategyFunc");

		// Token: 0x040004D2 RID: 1234
		internal static readonly MethodInfo EntityWrapperFactory_GetSnapshotChangeTrackingStrategyFunc = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("GetSnapshotChangeTrackingStrategyFunc");

		// Token: 0x040004D3 RID: 1235
		internal static readonly PropertyInfo EntityWrapperFactory_NullWrapper = typeof(NullEntityWrapper).GetDeclaredProperty("NullWrapper");

		// Token: 0x040004D4 RID: 1236
		internal static readonly PropertyInfo IEntityWrapper_Entity = typeof(IEntityWrapper).GetDeclaredProperty("Entity");

		// Token: 0x040004D5 RID: 1237
		internal static readonly MethodInfo IEqualityComparerOfString_Equals = typeof(IEqualityComparer<string>).GetDeclaredMethod("Equals", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x040004D6 RID: 1238
		internal static readonly ConstructorInfo MaterializedDataRecord_ctor = typeof(MaterializedDataRecord).GetDeclaredConstructor(new Type[]
		{
			typeof(MetadataWorkspace),
			typeof(TypeUsage),
			typeof(object[])
		});

		// Token: 0x040004D7 RID: 1239
		internal static readonly MethodInfo RecordState_GatherData = typeof(RecordState).GetOnlyDeclaredMethod("GatherData");

		// Token: 0x040004D8 RID: 1240
		internal static readonly MethodInfo RecordState_SetNullRecord = typeof(RecordState).GetOnlyDeclaredMethod("SetNullRecord");

		// Token: 0x040004D9 RID: 1241
		internal static readonly MethodInfo Shaper_Discriminate = typeof(Shaper).GetOnlyDeclaredMethod("Discriminate");

		// Token: 0x040004DA RID: 1242
		internal static readonly MethodInfo Shaper_GetPropertyValueWithErrorHandling = typeof(Shaper).GetOnlyDeclaredMethod("GetPropertyValueWithErrorHandling");

		// Token: 0x040004DB RID: 1243
		internal static readonly MethodInfo Shaper_GetColumnValueWithErrorHandling = typeof(Shaper).GetOnlyDeclaredMethod("GetColumnValueWithErrorHandling");

		// Token: 0x040004DC RID: 1244
		internal static readonly MethodInfo Shaper_GetGeographyColumnValue = typeof(Shaper).GetOnlyDeclaredMethod("GetGeographyColumnValue");

		// Token: 0x040004DD RID: 1245
		internal static readonly MethodInfo Shaper_GetGeometryColumnValue = typeof(Shaper).GetOnlyDeclaredMethod("GetGeometryColumnValue");

		// Token: 0x040004DE RID: 1246
		internal static readonly MethodInfo Shaper_GetSpatialColumnValueWithErrorHandling = typeof(Shaper).GetOnlyDeclaredMethod("GetSpatialColumnValueWithErrorHandling");

		// Token: 0x040004DF RID: 1247
		internal static readonly MethodInfo Shaper_GetSpatialPropertyValueWithErrorHandling = typeof(Shaper).GetOnlyDeclaredMethod("GetSpatialPropertyValueWithErrorHandling");

		// Token: 0x040004E0 RID: 1248
		internal static readonly MethodInfo Shaper_HandleEntity = typeof(Shaper).GetOnlyDeclaredMethod("HandleEntity");

		// Token: 0x040004E1 RID: 1249
		internal static readonly MethodInfo Shaper_HandleEntityAppendOnly = typeof(Shaper).GetOnlyDeclaredMethod("HandleEntityAppendOnly");

		// Token: 0x040004E2 RID: 1250
		internal static readonly MethodInfo Shaper_HandleEntityNoTracking = typeof(Shaper).GetOnlyDeclaredMethod("HandleEntityNoTracking");

		// Token: 0x040004E3 RID: 1251
		internal static readonly MethodInfo Shaper_HandleFullSpanCollection = typeof(Shaper).GetOnlyDeclaredMethod("HandleFullSpanCollection");

		// Token: 0x040004E4 RID: 1252
		internal static readonly MethodInfo Shaper_HandleFullSpanElement = typeof(Shaper).GetOnlyDeclaredMethod("HandleFullSpanElement");

		// Token: 0x040004E5 RID: 1253
		internal static readonly MethodInfo Shaper_HandleIEntityWithKey = typeof(Shaper).GetOnlyDeclaredMethod("HandleIEntityWithKey");

		// Token: 0x040004E6 RID: 1254
		internal static readonly MethodInfo Shaper_HandleRelationshipSpan = typeof(Shaper).GetOnlyDeclaredMethod("HandleRelationshipSpan");

		// Token: 0x040004E7 RID: 1255
		internal static readonly MethodInfo Shaper_SetColumnValue = typeof(Shaper).GetOnlyDeclaredMethod("SetColumnValue");

		// Token: 0x040004E8 RID: 1256
		internal static readonly MethodInfo Shaper_SetEntityRecordInfo = typeof(Shaper).GetOnlyDeclaredMethod("SetEntityRecordInfo");

		// Token: 0x040004E9 RID: 1257
		internal static readonly MethodInfo Shaper_SetState = typeof(Shaper).GetOnlyDeclaredMethod("SetState");

		// Token: 0x040004EA RID: 1258
		internal static readonly MethodInfo Shaper_SetStatePassthrough = typeof(Shaper).GetOnlyDeclaredMethod("SetStatePassthrough");

		// Token: 0x040004EB RID: 1259
		internal static readonly Expression DBNull_Value = Expression.Constant(DBNull.Value, typeof(object));

		// Token: 0x040004EC RID: 1260
		internal static readonly ParameterExpression Shaper_Parameter = Expression.Parameter(typeof(Shaper), "shaper");

		// Token: 0x040004ED RID: 1261
		internal static readonly Expression Shaper_Reader = Expression.Field(CodeGenEmitter.Shaper_Parameter, typeof(Shaper).GetField("Reader"));

		// Token: 0x040004EE RID: 1262
		internal static readonly Expression Shaper_Workspace = Expression.Field(CodeGenEmitter.Shaper_Parameter, typeof(Shaper).GetField("Workspace"));

		// Token: 0x040004EF RID: 1263
		internal static readonly Expression Shaper_State = Expression.Field(CodeGenEmitter.Shaper_Parameter, typeof(Shaper).GetField("State"));

		// Token: 0x040004F0 RID: 1264
		internal static readonly Expression Shaper_Context = Expression.Field(CodeGenEmitter.Shaper_Parameter, typeof(Shaper).GetField("Context"));

		// Token: 0x040004F1 RID: 1265
		internal static readonly Expression Shaper_Context_Options = Expression.Property(CodeGenEmitter.Shaper_Context, typeof(ObjectContext).GetDeclaredProperty("ContextOptions"));

		// Token: 0x040004F2 RID: 1266
		internal static readonly Expression Shaper_ProxyCreationEnabled = Expression.Property(CodeGenEmitter.Shaper_Context_Options, typeof(ObjectContextOptions).GetDeclaredProperty("ProxyCreationEnabled"));
	}
}
