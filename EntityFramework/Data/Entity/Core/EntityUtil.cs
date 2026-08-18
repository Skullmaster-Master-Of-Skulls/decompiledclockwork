using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core
{
	// Token: 0x02000278 RID: 632
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal static class EntityUtil
	{
		// Token: 0x06001630 RID: 5680 RVA: 0x0006BA84 File Offset: 0x00069C84
		internal static IEnumerable<KeyValuePair<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second)
		{
			if (first != null && second != null)
			{
				using (IEnumerator<T1> firstEnumerator = first.GetEnumerator())
				{
					using (IEnumerator<T2> secondEnumerator = second.GetEnumerator())
					{
						while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
						{
							yield return new KeyValuePair<T1, T2>(firstEnumerator.Current, secondEnumerator.Current);
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0006BAA8 File Offset: 0x00069CA8
		internal static bool IsAnICollection(Type type)
		{
			return typeof(ICollection<>).IsAssignableFrom(type.GetGenericTypeDefinition()) || type.GetInterface(typeof(ICollection<>).FullName) != null;
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0006BAE0 File Offset: 0x00069CE0
		internal static Type GetCollectionElementType(Type propertyType)
		{
			Type type = propertyType.TryGetElementType(typeof(ICollection<>));
			if (type == null)
			{
				throw new InvalidOperationException(Strings.PocoEntityWrapper_UnexpectedTypeForNavigationProperty(propertyType.FullName, typeof(ICollection<>)));
			}
			return type;
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0006BB24 File Offset: 0x00069D24
		internal static Type DetermineCollectionType(Type requestedType)
		{
			Type collectionElementType = EntityUtil.GetCollectionElementType(requestedType);
			if (requestedType.IsArray)
			{
				throw new InvalidOperationException(Strings.ObjectQuery_UnableToMaterializeArray(requestedType, typeof(List<>).MakeGenericType(new Type[]
				{
					collectionElementType
				})));
			}
			if (!requestedType.IsAbstract() && requestedType.GetPublicConstructor(new Type[0]) != null)
			{
				return requestedType;
			}
			Type type = typeof(HashSet<>).MakeGenericType(new Type[]
			{
				collectionElementType
			});
			if (requestedType.IsAssignableFrom(type))
			{
				return type;
			}
			Type type2 = typeof(List<>).MakeGenericType(new Type[]
			{
				collectionElementType
			});
			if (requestedType.IsAssignableFrom(type2))
			{
				return type2;
			}
			return null;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0006BBDA File Offset: 0x00069DDA
		internal static Type GetEntityIdentityType(Type entityType)
		{
			if (!EntityProxyFactory.IsProxyType(entityType))
			{
				return entityType;
			}
			return entityType.BaseType();
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0006BBEC File Offset: 0x00069DEC
		internal static string QuoteIdentifier(string identifier)
		{
			return "[" + identifier.Replace("]", "]]") + "]";
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0006BC10 File Offset: 0x00069E10
		internal static MetadataException InvalidSchemaEncountered(string errors)
		{
			return new MetadataException(string.Format(CultureInfo.CurrentCulture, EntityRes.GetString("InvalidSchemaEncountered"), new object[]
			{
				errors
			}));
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0006BC44 File Offset: 0x00069E44
		internal static Exception InternalError(EntityUtil.InternalErrorCode internalError, int location, object additionalInfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}, {1}", (int)internalError, location);
			if (additionalInfo != null)
			{
				stringBuilder.AppendFormat(", {0}", additionalInfo);
			}
			return new InvalidOperationException(Strings.ADP_InternalProviderError(stringBuilder.ToString()));
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0006BC90 File Offset: 0x00069E90
		internal static void CheckValidStateForChangeEntityState(EntityState state)
		{
			switch (state)
			{
			case EntityState.Detached:
			case EntityState.Unchanged:
			case EntityState.Added:
				return;
			case EntityState.Detached | EntityState.Unchanged:
				break;
			default:
				if (state == EntityState.Deleted || state == EntityState.Modified)
				{
					return;
				}
				break;
			}
			throw new ArgumentException(Strings.ObjectContext_InvalidEntityState, "state");
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0006BCD0 File Offset: 0x00069ED0
		internal static void CheckValidStateForChangeRelationshipState(EntityState state, string paramName)
		{
			switch (state)
			{
			case EntityState.Detached:
			case EntityState.Unchanged:
			case EntityState.Added:
				return;
			case EntityState.Detached | EntityState.Unchanged:
				break;
			default:
				if (state == EntityState.Deleted)
				{
					return;
				}
				break;
			}
			throw new ArgumentException(Strings.ObjectContext_InvalidRelationshipState, paramName);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0006BD07 File Offset: 0x00069F07
		internal static void ThrowPropertyIsNotNullable(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ConstraintException(Strings.Materializer_PropertyIsNotNullable);
			}
			throw new PropertyConstraintException(Strings.Materializer_PropertyIsNotNullableWithName(propertyName), propertyName);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0006BD28 File Offset: 0x00069F28
		internal static void ThrowSetInvalidValue(object value, Type destinationType, string className, string propertyName)
		{
			if (value == null)
			{
				throw new ConstraintException(Strings.Materializer_SetInvalidValue((Nullable.GetUnderlyingType(destinationType) ?? destinationType).Name, className, propertyName, "null"));
			}
			throw new InvalidOperationException(Strings.Materializer_SetInvalidValue((Nullable.GetUnderlyingType(destinationType) ?? destinationType).Name, className, propertyName, value.GetType().Name));
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0006BD84 File Offset: 0x00069F84
		internal static InvalidOperationException ValueInvalidCast(Type valueType, Type destinationType)
		{
			if (destinationType.IsValueType() && destinationType.IsGenericType() && typeof(Nullable<>) == destinationType.GetGenericTypeDefinition())
			{
				return new InvalidOperationException(Strings.Materializer_InvalidCastNullable(valueType, destinationType.GetGenericArguments()[0]));
			}
			return new InvalidOperationException(Strings.Materializer_InvalidCastReference(valueType, destinationType));
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x0006BDD8 File Offset: 0x00069FD8
		internal static void CheckArgumentMergeOption(MergeOption mergeOption)
		{
			switch (mergeOption)
			{
			case MergeOption.AppendOnly:
			case MergeOption.OverwriteChanges:
			case MergeOption.PreserveChanges:
			case MergeOption.NoTracking:
				return;
			default:
			{
				string name = typeof(MergeOption).Name;
				object name2 = typeof(MergeOption).Name;
				int num = (int)mergeOption;
				throw new ArgumentOutOfRangeException(name, Strings.ADP_InvalidEnumerationValue(name2, num.ToString(CultureInfo.InvariantCulture)));
			}
			}
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0006BE34 File Offset: 0x0006A034
		internal static void CheckArgumentRefreshMode(RefreshMode refreshMode)
		{
			if (refreshMode != RefreshMode.ClientWins && refreshMode != RefreshMode.StoreWins)
			{
				string name = typeof(RefreshMode).Name;
				object name2 = typeof(RefreshMode).Name;
				int num = (int)refreshMode;
				throw new ArgumentOutOfRangeException(name, Strings.ADP_InvalidEnumerationValue(name2, num.ToString(CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0006BE80 File Offset: 0x0006A080
		internal static InvalidOperationException ExecuteFunctionCalledWithNonReaderFunction(EdmFunction functionImport)
		{
			string message;
			if (functionImport.ReturnParameter == null)
			{
				message = Strings.ObjectContext_ExecuteFunctionCalledWithNonQueryFunction(functionImport.Name);
			}
			else
			{
				message = Strings.ObjectContext_ExecuteFunctionCalledWithScalarFunction(functionImport.ReturnParameter.TypeUsage.EdmType.FullName, functionImport.Name);
			}
			return new InvalidOperationException(message);
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0006BECA File Offset: 0x0006A0CA
		internal static void ValidateEntitySetInKey(EntityKey key, EntitySet entitySet)
		{
			EntityUtil.ValidateEntitySetInKey(key, entitySet, null);
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0006BED4 File Offset: 0x0006A0D4
		internal static void ValidateEntitySetInKey(EntityKey key, EntitySet entitySet, string argument)
		{
			string entityContainerName = key.EntityContainerName;
			string entitySetName = key.EntitySetName;
			string name = entitySet.EntityContainer.Name;
			string name2 = entitySet.Name;
			if (StringComparer.Ordinal.Equals(entityContainerName, name) && StringComparer.Ordinal.Equals(entitySetName, name2))
			{
				return;
			}
			if (string.IsNullOrEmpty(argument))
			{
				throw new InvalidOperationException(Strings.ObjectContext_InvalidEntitySetInKey(entityContainerName, entitySetName, name, name2));
			}
			throw new InvalidOperationException(Strings.ObjectContext_InvalidEntitySetInKeyFromName(entityContainerName, entitySetName, name, name2, argument));
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0006BF48 File Offset: 0x0006A148
		internal static void ValidateNecessaryModificationFunctionMapping(ModificationFunctionMapping mapping, string currentState, IEntityStateEntry stateEntry, string type, string typeName)
		{
			if (mapping == null)
			{
				throw new UpdateException(Strings.Update_MissingFunctionMapping(currentState, type, typeName), null, new List<IEntityStateEntry>
				{
					stateEntry
				}.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
			}
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0006BF80 File Offset: 0x0006A180
		internal static UpdateException Update(string message, Exception innerException, params IEntityStateEntry[] stateEntries)
		{
			return new UpdateException(message, innerException, stateEntries.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x0006BF94 File Offset: 0x0006A194
		internal static UpdateException UpdateRelationshipCardinalityConstraintViolation(string relationshipSetName, int minimumCount, int? maximumCount, string entitySetName, int actualCount, string otherEndPluralName, IEntityStateEntry stateEntry)
		{
			string text = EntityUtil.ConvertCardinalityToString(new int?(minimumCount));
			string text2 = EntityUtil.ConvertCardinalityToString(maximumCount);
			string p = EntityUtil.ConvertCardinalityToString(new int?(actualCount));
			if (minimumCount == 1 && text == text2)
			{
				return EntityUtil.Update(Strings.Update_RelationshipCardinalityConstraintViolationSingleValue(entitySetName, relationshipSetName, p, otherEndPluralName, text), null, new IEntityStateEntry[]
				{
					stateEntry
				});
			}
			return EntityUtil.Update(Strings.Update_RelationshipCardinalityConstraintViolation(entitySetName, relationshipSetName, p, otherEndPluralName, text, text2), null, new IEntityStateEntry[]
			{
				stateEntry
			});
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x0006C010 File Offset: 0x0006A210
		private static string ConvertCardinalityToString(int? cardinality)
		{
			if (cardinality != null)
			{
				return cardinality.Value.ToString(CultureInfo.CurrentCulture);
			}
			return "*";
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0006C040 File Offset: 0x0006A240
		internal static T CheckArgumentOutOfRange<T>(T[] values, int index, string parameterName)
		{
			if (values.Length <= index)
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
			return values[index];
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0006C058 File Offset: 0x0006A258
		internal static IEnumerable<T> CheckArgumentContainsNull<T>(ref IEnumerable<T> enumerableArgument, string argumentName) where T : class
		{
			EntityUtil.GetCheapestSafeEnumerableAsCollection<T>(ref enumerableArgument);
			foreach (T t in enumerableArgument)
			{
				if (t == null)
				{
					throw new ArgumentException(Strings.CheckArgumentContainsNullFailed(argumentName));
				}
			}
			return enumerableArgument;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0006C0B8 File Offset: 0x0006A2B8
		internal static IEnumerable<T> CheckArgumentEmpty<T>(ref IEnumerable<T> enumerableArgument, Func<string, string> errorMessage, string argumentName)
		{
			int num;
			EntityUtil.GetCheapestSafeCountOfEnumerable<T>(ref enumerableArgument, out num);
			if (num <= 0)
			{
				throw new ArgumentException(errorMessage(argumentName));
			}
			return enumerableArgument;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x0006C0E0 File Offset: 0x0006A2E0
		private static void GetCheapestSafeCountOfEnumerable<T>(ref IEnumerable<T> enumerable, out int count)
		{
			ICollection<T> cheapestSafeEnumerableAsCollection = EntityUtil.GetCheapestSafeEnumerableAsCollection<T>(ref enumerable);
			count = cheapestSafeEnumerableAsCollection.Count;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x0006C0FC File Offset: 0x0006A2FC
		private static ICollection<T> GetCheapestSafeEnumerableAsCollection<T>(ref IEnumerable<T> enumerable)
		{
			ICollection<T> collection = enumerable as ICollection<T>;
			if (collection != null)
			{
				return collection;
			}
			enumerable = new List<T>(enumerable);
			return enumerable as ICollection<T>;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x0006C128 File Offset: 0x0006A328
		internal static bool IsNull(object value)
		{
			if (value == null || DBNull.Value == value)
			{
				return true;
			}
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x0006C154 File Offset: 0x0006A354
		internal static int SrcCompare(string strA, string strB)
		{
			if (!(strA == strB))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0006C162 File Offset: 0x0006A362
		internal static int DstCompare(string strA, string strB)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
		}

		// Token: 0x040007C7 RID: 1991
		internal const int AssemblyQualifiedNameIndex = 3;

		// Token: 0x040007C8 RID: 1992
		internal const int InvariantNameIndex = 2;

		// Token: 0x040007C9 RID: 1993
		internal const string Parameter = "Parameter";

		// Token: 0x040007CA RID: 1994
		internal const CompareOptions StringCompareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x040007CB RID: 1995
		internal static Dictionary<string, string> COMPILER_VERSION = new Dictionary<string, string>
		{
			{
				"CompilerVersion",
				"V3.5"
			}
		};

		// Token: 0x02000279 RID: 633
		internal enum InternalErrorCode
		{
			// Token: 0x040007CD RID: 1997
			WrongNumberOfKeys = 1000,
			// Token: 0x040007CE RID: 1998
			UnknownColumnMapKind,
			// Token: 0x040007CF RID: 1999
			NestOverNest,
			// Token: 0x040007D0 RID: 2000
			ColumnCountMismatch,
			// Token: 0x040007D1 RID: 2001
			AssertionFailed,
			// Token: 0x040007D2 RID: 2002
			UnknownVar,
			// Token: 0x040007D3 RID: 2003
			WrongVarType,
			// Token: 0x040007D4 RID: 2004
			ExtentWithoutEntity,
			// Token: 0x040007D5 RID: 2005
			UnnestWithoutInput,
			// Token: 0x040007D6 RID: 2006
			UnnestMultipleCollections,
			// Token: 0x040007D7 RID: 2007
			CodeGen_NoSuchProperty = 1011,
			// Token: 0x040007D8 RID: 2008
			JoinOverSingleStreamNest,
			// Token: 0x040007D9 RID: 2009
			InvalidInternalTree,
			// Token: 0x040007DA RID: 2010
			NameValuePairNext,
			// Token: 0x040007DB RID: 2011
			InvalidParserState1,
			// Token: 0x040007DC RID: 2012
			InvalidParserState2,
			// Token: 0x040007DD RID: 2013
			SqlGenParametersNotPermitted,
			// Token: 0x040007DE RID: 2014
			EntityKeyMissingKeyValue,
			// Token: 0x040007DF RID: 2015
			UpdatePipelineResultRequestInvalid,
			// Token: 0x040007E0 RID: 2016
			InvalidStateEntry,
			// Token: 0x040007E1 RID: 2017
			InvalidPrimitiveTypeKind,
			// Token: 0x040007E2 RID: 2018
			UnknownLinqNodeType = 1023,
			// Token: 0x040007E3 RID: 2019
			CollectionWithNoColumns,
			// Token: 0x040007E4 RID: 2020
			UnexpectedLinqLambdaExpressionFormat,
			// Token: 0x040007E5 RID: 2021
			CommandTreeOnStoredProcedureEntityCommand,
			// Token: 0x040007E6 RID: 2022
			BoolExprAssert,
			// Token: 0x040007E7 RID: 2023
			FailedToGeneratePromotionRank = 1029
		}
	}
}
