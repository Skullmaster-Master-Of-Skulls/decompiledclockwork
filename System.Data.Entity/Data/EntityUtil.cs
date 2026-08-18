using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Common.EntitySql;
using System.Data.Entity;
using System.Data.Mapping;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.Internal;
using System.Data.Query.InternalTrees;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Data
{
	// Token: 0x02000017 RID: 23
	internal static class EntityUtil
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003094 File Offset: 0x00001294
		internal static bool? ThreeValuedNot(bool? operand)
		{
			if (operand == null)
			{
				return null;
			}
			return new bool?(!operand.Value);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000030C4 File Offset: 0x000012C4
		internal static bool? ThreeValuedAnd(bool? left, bool? right)
		{
			bool? result;
			if (left != null && right != null)
			{
				result = new bool?(left.Value && right.Value);
			}
			else if (left == null && right == null)
			{
				result = null;
			}
			else if (left != null)
			{
				result = (left.Value ? null : new bool?(false));
			}
			else
			{
				result = (right.Value ? null : new bool?(false));
			}
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003160 File Offset: 0x00001360
		internal static bool? ThreeValuedOr(bool? left, bool? right)
		{
			bool? result;
			if (left != null && right != null)
			{
				result = new bool?(left.Value || right.Value);
			}
			else if (left == null && right == null)
			{
				result = null;
			}
			else if (left != null)
			{
				result = (left.Value ? new bool?(true) : null);
			}
			else
			{
				result = (right.Value ? new bool?(true) : null);
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000031F9 File Offset: 0x000013F9
		internal static IEnumerable<KeyValuePair<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second)
		{
			if (first == null || second == null)
			{
				yield break;
			}
			using (IEnumerator<T1> firstEnumerator = first.GetEnumerator())
			{
				using (IEnumerator<T2> secondEnumerator = second.GetEnumerator())
				{
					while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
					{
						yield return new KeyValuePair<T1, T2>(firstEnumerator.Current, secondEnumerator.Current);
					}
				}
				IEnumerator<T2> secondEnumerator = null;
			}
			IEnumerator<T1> firstEnumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003210 File Offset: 0x00001410
		internal static bool IsAnICollection(Type type)
		{
			return typeof(ICollection<>).IsAssignableFrom(type.GetGenericTypeDefinition()) || type.GetInterface(typeof(ICollection<>).FullName) != null;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003248 File Offset: 0x00001448
		internal static bool TryGetICollectionElementType(Type collectionType, out Type elementType)
		{
			elementType = null;
			try
			{
				Type type = (collectionType.IsGenericType && typeof(ICollection<>).IsAssignableFrom(collectionType.GetGenericTypeDefinition())) ? collectionType : collectionType.GetInterface(typeof(ICollection<>).FullName);
				if (type != null && !type.ContainsGenericParameters)
				{
					elementType = type.GetGenericArguments()[0];
					return true;
				}
			}
			catch (AmbiguousMatchException)
			{
			}
			return false;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000032C8 File Offset: 0x000014C8
		internal static Type GetCollectionElementType(Type propertyType)
		{
			Type result;
			if (!EntityUtil.TryGetICollectionElementType(propertyType, out result))
			{
				throw EntityUtil.InvalidOperation(Strings.PocoEntityWrapper_UnexpectedTypeForNavigationProperty(propertyType.FullName, typeof(ICollection<>)));
			}
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000032FC File Offset: 0x000014FC
		internal static Type DetermineCollectionType(Type requestedType)
		{
			Type collectionElementType = EntityUtil.GetCollectionElementType(requestedType);
			if (requestedType.IsArray)
			{
				throw EntityUtil.InvalidOperation(Strings.ObjectQuery_UnableToMaterializeArray(requestedType, typeof(List<>).MakeGenericType(new Type[]
				{
					collectionElementType
				})));
			}
			if (!requestedType.IsAbstract && requestedType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, Type.EmptyTypes, null) != null)
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

		// Token: 0x06000075 RID: 117 RVA: 0x000033AC File Offset: 0x000015AC
		internal static Type GetEntityIdentityType(Type entityType)
		{
			if (!EntityProxyFactory.IsProxyType(entityType))
			{
				return entityType;
			}
			return entityType.BaseType;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000033BE File Offset: 0x000015BE
		internal static string QuoteIdentifier(string identifier)
		{
			return "[" + identifier.Replace("]", "]]") + "]";
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000033DF File Offset: 0x000015DF
		internal static ArgumentException Argument(string error)
		{
			return new ArgumentException(error);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000033E7 File Offset: 0x000015E7
		internal static ArgumentException Argument(string error, Exception inner)
		{
			return new ArgumentException(error, inner);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000033F0 File Offset: 0x000015F0
		internal static ArgumentException Argument(string error, string parameter)
		{
			return new ArgumentException(error, parameter);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000033F9 File Offset: 0x000015F9
		internal static ArgumentException Argument(string error, string parameter, Exception inner)
		{
			return new ArgumentException(error, parameter, inner);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003403 File Offset: 0x00001603
		internal static ArgumentNullException ArgumentNull(string parameter)
		{
			return new ArgumentNullException(parameter);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000340B File Offset: 0x0000160B
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName)
		{
			return new ArgumentOutOfRangeException(parameterName);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003413 File Offset: 0x00001613
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName)
		{
			return new ArgumentOutOfRangeException(parameterName, message);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000341C File Offset: 0x0000161C
		internal static EntityCommandExecutionException CommandExecution(string message)
		{
			return new EntityCommandExecutionException(message);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003424 File Offset: 0x00001624
		internal static EntityCommandExecutionException CommandExecution(string message, Exception innerException)
		{
			return new EntityCommandExecutionException(message, innerException);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000342D File Offset: 0x0000162D
		internal static EntityCommandCompilationException CommandCompilation(string message, Exception innerException)
		{
			return new EntityCommandCompilationException(message, innerException);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003436 File Offset: 0x00001636
		internal static PropertyConstraintException PropertyConstraint(string message, string propertyName)
		{
			return new PropertyConstraintException(message, propertyName);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000343F File Offset: 0x0000163F
		internal static ConstraintException Constraint(string message)
		{
			return new ConstraintException(message);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003447 File Offset: 0x00001647
		internal static IndexOutOfRangeException IndexOutOfRange(string error)
		{
			return new IndexOutOfRangeException(error);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000344F File Offset: 0x0000164F
		internal static InvalidOperationException InvalidOperation(string error)
		{
			return new InvalidOperationException(error);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003457 File Offset: 0x00001657
		internal static InvalidOperationException InvalidOperation(string error, Exception inner)
		{
			return new InvalidOperationException(error, inner);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003460 File Offset: 0x00001660
		internal static ArgumentException InvalidStringArgument(string parameterName)
		{
			return EntityUtil.Argument(Strings.InvalidStringArgument(parameterName), parameterName);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000346E File Offset: 0x0000166E
		internal static MappingException Mapping(string message)
		{
			return new MappingException(message);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003476 File Offset: 0x00001676
		internal static MetadataException Metadata(string message, Exception inner)
		{
			return new MetadataException(message, inner);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000347F File Offset: 0x0000167F
		internal static MetadataException Metadata(string message)
		{
			return new MetadataException(message);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003487 File Offset: 0x00001687
		internal static NotSupportedException NotSupported()
		{
			return new NotSupportedException();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000348E File Offset: 0x0000168E
		internal static NotSupportedException NotSupported(string error)
		{
			return new NotSupportedException(error);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003496 File Offset: 0x00001696
		internal static ObjectDisposedException ObjectDisposed(string error)
		{
			return new ObjectDisposedException(null, error);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000349F File Offset: 0x0000169F
		internal static ObjectNotFoundException ObjectNotFound(string error)
		{
			return new ObjectNotFoundException(error);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000034A7 File Offset: 0x000016A7
		internal static EntitySqlException EntitySqlError(string message)
		{
			return new EntitySqlException(message);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000034AF File Offset: 0x000016AF
		internal static EntitySqlException EntitySqlError(string message, Exception innerException)
		{
			return new EntitySqlException(message, innerException);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000034B8 File Offset: 0x000016B8
		internal static EntitySqlException EntitySqlError(ErrorContext errCtx, string message)
		{
			return EntitySqlException.Create(errCtx, message, null);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000034B8 File Offset: 0x000016B8
		internal static EntitySqlException EntitySqlError(ErrorContext errCtx, string message, Exception innerException)
		{
			return EntitySqlException.Create(errCtx, message, null);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000034C2 File Offset: 0x000016C2
		internal static EntitySqlException EntitySqlError(string queryText, string errorMessage, int errorPosition)
		{
			return EntitySqlException.Create(queryText, errorMessage, errorPosition, null, false, null);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000034CF File Offset: 0x000016CF
		internal static EntitySqlException EntitySqlError(string queryText, string errorMessage, int errorPosition, string additionalErrorInformation, bool loadContextInfoFromResource)
		{
			return EntitySqlException.Create(queryText, errorMessage, errorPosition, additionalErrorInformation, loadContextInfoFromResource, null);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000034DD File Offset: 0x000016DD
		internal static ProviderIncompatibleException CannotCloneStoreProvider()
		{
			return EntityUtil.ProviderIncompatible(Strings.EntityClient_CannotCloneStoreProvider);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000034E9 File Offset: 0x000016E9
		internal static InvalidOperationException ClosedDataReaderError()
		{
			return EntityUtil.InvalidOperation(Strings.ADP_ClosedDataReaderError);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000034F5 File Offset: 0x000016F5
		internal static InvalidOperationException DataReaderClosed(string method)
		{
			return EntityUtil.InvalidOperation(Strings.ADP_DataReaderClosed(method));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003502 File Offset: 0x00001702
		internal static InvalidOperationException ImplicitlyClosedDataReaderError()
		{
			return EntityUtil.InvalidOperation(Strings.ADP_ImplicitlyClosedDataReaderError);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000350E File Offset: 0x0000170E
		internal static IndexOutOfRangeException InvalidBufferSizeOrIndex(int numBytes, int bufferIndex)
		{
			return EntityUtil.IndexOutOfRange(Strings.ADP_InvalidBufferSizeOrIndex(numBytes.ToString(CultureInfo.InvariantCulture), bufferIndex.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003532 File Offset: 0x00001732
		internal static IndexOutOfRangeException InvalidDataLength(long length)
		{
			return EntityUtil.IndexOutOfRange(Strings.ADP_InvalidDataLength(length.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000354A File Offset: 0x0000174A
		internal static ArgumentOutOfRangeException InvalidDestinationBufferIndex(int maxLen, int dstOffset, string parameterName)
		{
			return EntityUtil.ArgumentOutOfRange(Strings.ADP_InvalidDestinationBufferIndex(maxLen.ToString(CultureInfo.InvariantCulture), dstOffset.ToString(CultureInfo.InvariantCulture)), parameterName);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000356F File Offset: 0x0000176F
		internal static ArgumentOutOfRangeException InvalidSourceBufferIndex(int maxLen, long srcOffset, string parameterName)
		{
			return EntityUtil.ArgumentOutOfRange(Strings.ADP_InvalidSourceBufferIndex(maxLen.ToString(CultureInfo.InvariantCulture), srcOffset.ToString(CultureInfo.InvariantCulture)), parameterName);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003594 File Offset: 0x00001794
		internal static InvalidOperationException MustUseSequentialAccess()
		{
			return EntityUtil.InvalidOperation(Strings.ADP_MustUseSequentialAccess);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000035A0 File Offset: 0x000017A0
		internal static InvalidOperationException NoData()
		{
			return EntityUtil.InvalidOperation(Strings.ADP_NoData);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000035AC File Offset: 0x000017AC
		internal static InvalidOperationException NonSequentialArrayOffsetAccess(long badIndex, long currIndex, string method)
		{
			return EntityUtil.InvalidOperation(Strings.ADP_NonSequentialChunkAccess(badIndex.ToString(CultureInfo.InvariantCulture), currIndex.ToString(CultureInfo.InvariantCulture), method));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000035D1 File Offset: 0x000017D1
		internal static InvalidOperationException NonSequentialColumnAccess(int badCol, int currCol)
		{
			return EntityUtil.InvalidOperation(Strings.ADP_NonSequentialColumnAccess(badCol.ToString(CultureInfo.InvariantCulture), currCol.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000035F8 File Offset: 0x000017F8
		internal static NotSupportedException KeysRequiredForJoinOverNest(Op op)
		{
			return EntityUtil.NotSupported(Strings.ADP_KeysRequiredForJoinOverNest(op.OpType.ToString()));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003623 File Offset: 0x00001823
		internal static NotSupportedException KeysRequiredForNesting()
		{
			return EntityUtil.NotSupported(Strings.ADP_KeysRequiredForNesting);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003630 File Offset: 0x00001830
		internal static NotSupportedException NestingNotSupported(Op parentOp, Op childOp)
		{
			return EntityUtil.NotSupported(Strings.ADP_NestingNotSupported(parentOp.OpType.ToString(), childOp.OpType.ToString()));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000366F File Offset: 0x0000186F
		internal static NotSupportedException ProviderDoesNotSupportCommandTrees()
		{
			return EntityUtil.NotSupported(Strings.ADP_ProviderDoesNotSupportCommandTrees);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000367B File Offset: 0x0000187B
		internal static EntityCommandExecutionException CommandExecutionDataReaderFieldCountForScalarType()
		{
			return EntityUtil.CommandExecution(Strings.ADP_InvalidDataReaderFieldCountForScalarType);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003687 File Offset: 0x00001887
		internal static EntityCommandExecutionException CommandExecutionDataReaderMissingColumnForType(EdmMember member, EdmType currentType)
		{
			return EntityUtil.CommandExecution(Strings.ADP_InvalidDataReaderMissingColumnForType(currentType.FullName, member.Name));
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000369F File Offset: 0x0000189F
		internal static EntityCommandExecutionException CommandExecutionDataReaderMissinDiscriminatorColumn(string columnName, EdmFunction functionImport)
		{
			return EntityUtil.CommandExecution(Strings.ADP_InvalidDataReaderMissingDiscriminatorColumn(columnName, functionImport.FullName));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000036B2 File Offset: 0x000018B2
		internal static ProviderIncompatibleException ProviderIncompatible(string error)
		{
			return new ProviderIncompatibleException(error);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000036BA File Offset: 0x000018BA
		internal static ProviderIncompatibleException ProviderIncompatible(string error, Exception innerException)
		{
			return new ProviderIncompatibleException(error, innerException);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000036C3 File Offset: 0x000018C3
		internal static EntityException Provider(string error)
		{
			return new EntityException(error);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000036CB File Offset: 0x000018CB
		internal static EntityException Provider(Exception inner)
		{
			return new EntityException(Strings.EntityClient_ProviderGeneralError, inner);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000036D8 File Offset: 0x000018D8
		internal static EntityException Provider(string parameter, Exception inner)
		{
			return new EntityException(Strings.EntityClient_ProviderSpecificError(parameter), inner);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000036E6 File Offset: 0x000018E6
		internal static EntityException ProviderExceptionWithMessage(string message, Exception inner)
		{
			return new EntityException(message, inner);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000036EF File Offset: 0x000018EF
		internal static InvalidOperationException SqlTypesAssemblyNotFound()
		{
			return EntityUtil.InvalidOperation(Strings.SqlProvider_SqlTypesAssemblyNotFound);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000036FB File Offset: 0x000018FB
		internal static ProviderIncompatibleException GeographyValueNotSqlCompatible()
		{
			return EntityUtil.ProviderIncompatible(Strings.SqlProvider_GeographyValueNotSqlCompatible);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003707 File Offset: 0x00001907
		internal static ProviderIncompatibleException GeometryValueNotSqlCompatible()
		{
			return EntityUtil.ProviderIncompatible(Strings.SqlProvider_GeometryValueNotSqlCompatible);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003713 File Offset: 0x00001913
		internal static MetadataException InvalidSchemaEncountered(string errors)
		{
			return EntityUtil.Metadata(string.Format(CultureInfo.CurrentCulture, EntityRes.GetString("InvalidSchemaEncountered"), new object[]
			{
				errors
			}));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003738 File Offset: 0x00001938
		internal static MetadataException InvalidCollectionForMapping(DataSpace space)
		{
			return EntityUtil.Metadata(Strings.InvalidCollectionForMapping(space.ToString()));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003751 File Offset: 0x00001951
		internal static ArgumentException MemberInvalidIdentity(string identity, string parameter)
		{
			return EntityUtil.Argument(Strings.MemberInvalidIdentity(identity), parameter);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000375F File Offset: 0x0000195F
		internal static ArgumentException ArrayTooSmall(string parameter)
		{
			return EntityUtil.Argument(Strings.ArrayTooSmall, parameter);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000376C File Offset: 0x0000196C
		internal static ArgumentException ItemDuplicateIdentity(string identity, string parameter, Exception inner)
		{
			return EntityUtil.Argument(Strings.ItemDuplicateIdentity(identity), parameter, inner);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000377B File Offset: 0x0000197B
		internal static ArgumentException ItemInvalidIdentity(string identity, string parameter)
		{
			return EntityUtil.Argument(Strings.ItemInvalidIdentity(identity), parameter);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003789 File Offset: 0x00001989
		internal static InvalidOperationException MoreThanOneItemMatchesIdentity(string identity)
		{
			return EntityUtil.InvalidOperation(Strings.MoreThanOneItemMatchesIdentity(identity));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003796 File Offset: 0x00001996
		internal static InvalidOperationException OperationOnReadOnlyCollection()
		{
			return EntityUtil.InvalidOperation(Strings.OperationOnReadOnlyCollection);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000037A2 File Offset: 0x000019A2
		internal static InvalidOperationException ItemCollectionAlreadyRegistered(DataSpace space)
		{
			return EntityUtil.InvalidOperation(Strings.ItemCollectionAlreadyRegistered(space.ToString()));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000037BB File Offset: 0x000019BB
		internal static InvalidOperationException NoCollectionForSpace(DataSpace space)
		{
			return EntityUtil.InvalidOperation(Strings.NoCollectionForSpace(space.ToString()));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000037D4 File Offset: 0x000019D4
		internal static InvalidOperationException InvalidCollectionSpecified(DataSpace space)
		{
			return EntityUtil.InvalidOperation(Strings.InvalidCollectionSpecified(space));
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000037E6 File Offset: 0x000019E6
		internal static MetadataException DifferentSchemaVersionInCollection(string itemCollectionType, double versionToRegister, double currentSchemaVersion)
		{
			return EntityUtil.Metadata(Strings.DifferentSchemaVersionInCollection(itemCollectionType, versionToRegister, currentSchemaVersion));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000037FF File Offset: 0x000019FF
		internal static ArgumentException NotBinaryTypeForTypeUsage()
		{
			return EntityUtil.Argument(Strings.NotBinaryTypeForTypeUsage);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000380B File Offset: 0x00001A0B
		internal static ArgumentException NotDateTimeTypeForTypeUsage()
		{
			return EntityUtil.Argument(Strings.NotDateTimeTypeForTypeUsage);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003817 File Offset: 0x00001A17
		internal static ArgumentException NotDateTimeOffsetTypeForTypeUsage()
		{
			return EntityUtil.Argument(Strings.NotDateTimeOffsetTypeForTypeUsage);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003823 File Offset: 0x00001A23
		internal static ArgumentException NotTimeTypeForTypeUsage()
		{
			return EntityUtil.Argument(Strings.NotTimeTypeForTypeUsage);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000382F File Offset: 0x00001A2F
		internal static ArgumentException NotDecimalTypeForTypeUsage()
		{
			return EntityUtil.Argument(Strings.NotDecimalTypeForTypeUsage);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000383B File Offset: 0x00001A3B
		internal static ArgumentException NotStringTypeForTypeUsage()
		{
			return EntityUtil.Argument(Strings.NotStringTypeForTypeUsage);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003847 File Offset: 0x00001A47
		internal static ArgumentException InvalidEntitySetName(string name)
		{
			return EntityUtil.Argument(Strings.InvalidEntitySetName(name));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003854 File Offset: 0x00001A54
		internal static ArgumentException InvalidRelationshipSetName(string name)
		{
			return EntityUtil.Argument(Strings.InvalidRelationshipSetName(name));
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003861 File Offset: 0x00001A61
		internal static ArgumentException InvalidEDMVersion(double edmVersion)
		{
			return EntityUtil.Argument(Strings.InvalidEDMVersion(edmVersion.ToString(CultureInfo.CurrentCulture)));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003879 File Offset: 0x00001A79
		internal static ArgumentException EntitySetInAnotherContainer(string parameter)
		{
			return EntityUtil.Argument(Strings.EntitySetInAnotherContainer, parameter);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003886 File Offset: 0x00001A86
		internal static InvalidOperationException OperationOnReadOnlyItem()
		{
			return EntityUtil.InvalidOperation(Strings.OperationOnReadOnlyItem);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003892 File Offset: 0x00001A92
		internal static ArgumentException MinAndMaxValueMustBeSameForConstantFacet(string facetName, string typeName)
		{
			return EntityUtil.Argument(Strings.MinAndMaxValueMustBeSameForConstantFacet(facetName, typeName));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000038A0 File Offset: 0x00001AA0
		internal static ArgumentException MissingDefaultValueForConstantFacet(string facetName, string typeName)
		{
			return EntityUtil.Argument(Strings.MissingDefaultValueForConstantFacet(facetName, typeName));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000038AE File Offset: 0x00001AAE
		internal static ArgumentException BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet(string facetName, string typeName)
		{
			return EntityUtil.Argument(Strings.BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet(facetName, typeName));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000038BC File Offset: 0x00001ABC
		internal static ArgumentException MinAndMaxValueMustBeDifferentForNonConstantFacet(string facetName, string typeName)
		{
			return EntityUtil.Argument(Strings.MinAndMaxValueMustBeDifferentForNonConstantFacet(facetName, typeName));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000038CA File Offset: 0x00001ACA
		internal static ArgumentException MinAndMaxMustBePositive(string facetName, string typeName)
		{
			return EntityUtil.Argument(Strings.MinAndMaxMustBePositive(facetName, typeName));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000038D8 File Offset: 0x00001AD8
		internal static ArgumentException MinMustBeLessThanMax(string minimumValue, string facetName, string typeName)
		{
			return EntityUtil.Argument(Strings.MinMustBeLessThanMax(minimumValue, facetName, typeName));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000038E7 File Offset: 0x00001AE7
		internal static ArgumentException EntitySetNotInCSpace(string name)
		{
			return EntityUtil.Argument(Strings.EntitySetNotInCSPace(name));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000038F4 File Offset: 0x00001AF4
		internal static ArgumentException TypeNotInEntitySet(string entitySetName, string rootEntityTypeName, string entityTypeName)
		{
			return EntityUtil.Argument(Strings.TypeNotInEntitySet(entityTypeName, rootEntityTypeName, entitySetName));
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000038E7 File Offset: 0x00001AE7
		internal static ArgumentException AssociationSetNotInCSpace(string name)
		{
			return EntityUtil.Argument(Strings.EntitySetNotInCSPace(name));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003903 File Offset: 0x00001B03
		internal static ArgumentException TypeNotInAssociationSet(string setName, string rootEntityTypeName, string typeName)
		{
			return EntityUtil.Argument(Strings.TypeNotInAssociationSet(typeName, rootEntityTypeName, setName));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003912 File Offset: 0x00001B12
		internal static Exception InternalError(EntityUtil.InternalErrorCode internalError)
		{
			return EntityUtil.InvalidOperation(Strings.ADP_InternalProviderError((int)internalError));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003924 File Offset: 0x00001B24
		internal static Exception InternalError(EntityUtil.InternalErrorCode internalError, int location, object additionalInfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}, {1}", (int)internalError, location);
			if (additionalInfo != null)
			{
				stringBuilder.AppendFormat(", {0}", additionalInfo);
			}
			return EntityUtil.InvalidOperation(Strings.ADP_InternalProviderError(stringBuilder.ToString()));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000396F File Offset: 0x00001B6F
		internal static Exception InternalError(EntityUtil.InternalErrorCode internalError, int location)
		{
			return EntityUtil.InternalError(internalError, location, null);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003979 File Offset: 0x00001B79
		internal static InvalidOperationException OriginalValuesDoesNotExist()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_OriginalValuesDoesNotExist);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003985 File Offset: 0x00001B85
		internal static InvalidOperationException CurrentValuesDoesNotExist()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_CurrentValuesDoesNotExist);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003991 File Offset: 0x00001B91
		internal static ArgumentException InvalidTypeForComplexTypeProperty(string argument)
		{
			return EntityUtil.Argument(Strings.ObjectStateEntry_InvalidTypeForComplexTypeProperty, argument);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000399E File Offset: 0x00001B9E
		internal static InvalidOperationException ObjectStateEntryinInvalidState()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_InvalidState);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000039AA File Offset: 0x00001BAA
		internal static InvalidOperationException CantModifyDetachedDeletedEntries()
		{
			throw EntityUtil.InvalidOperation(Strings.ObjectStateEntry_CantModifyDetachedDeletedEntries);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000039B6 File Offset: 0x00001BB6
		internal static InvalidOperationException SetModifiedStates(string methodName)
		{
			throw EntityUtil.InvalidOperation(Strings.ObjectStateEntry_SetModifiedStates(methodName));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000039C3 File Offset: 0x00001BC3
		internal static InvalidOperationException EntityCantHaveMultipleChangeTrackers()
		{
			return EntityUtil.InvalidOperation(Strings.Entity_EntityCantHaveMultipleChangeTrackers);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000039CF File Offset: 0x00001BCF
		internal static InvalidOperationException CantModifyRelationValues()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_CantModifyRelationValues);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000039DB File Offset: 0x00001BDB
		internal static InvalidOperationException CantModifyRelationState()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_CantModifyRelationState);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000039E7 File Offset: 0x00001BE7
		internal static InvalidOperationException CannotModifyKeyProperty(string fieldName)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_CannotModifyKeyProperty(fieldName));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000039F4 File Offset: 0x00001BF4
		internal static InvalidOperationException CantSetEntityKey()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_CantSetEntityKey);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003A00 File Offset: 0x00001C00
		internal static InvalidOperationException CannotAccessKeyEntryValues()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_CannotAccessKeyEntryValues);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003A0C File Offset: 0x00001C0C
		internal static InvalidOperationException CannotModifyKeyEntryState()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_CannotModifyKeyEntryState);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003A18 File Offset: 0x00001C18
		internal static InvalidOperationException CannotCallDeleteOnKeyEntry()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_CannotDeleteOnKeyEntry);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003A24 File Offset: 0x00001C24
		internal static ArgumentException InvalidModifiedPropertyName(string propertyName)
		{
			return EntityUtil.Argument(Strings.ObjectStateEntry_SetModifiedOnInvalidProperty(propertyName));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003A31 File Offset: 0x00001C31
		internal static InvalidOperationException NoEntryExistForEntityKey()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateManager_NoEntryExistForEntityKey);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003A3D File Offset: 0x00001C3D
		internal static ArgumentException DetachedObjectStateEntriesDoesNotExistInObjectStateManager()
		{
			return EntityUtil.Argument(Strings.ObjectStateManager_DetachedObjectStateEntriesDoesNotExistInObjectStateManager);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003A49 File Offset: 0x00001C49
		internal static InvalidOperationException ObjectStateManagerContainsThisEntityKey()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateManager_ObjectStateManagerContainsThisEntityKey);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003A55 File Offset: 0x00001C55
		internal static InvalidOperationException ObjectStateManagerDoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity(EntityState state)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateManager_DoesnotAllowToReAddUnchangedOrModifiedOrDeletedEntity(state));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003A67 File Offset: 0x00001C67
		internal static InvalidOperationException CannotFixUpKeyToExistingValues()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateManager_CannotFixUpKeyToExistingValues);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003A73 File Offset: 0x00001C73
		internal static InvalidOperationException KeyPropertyDoesntMatchValueInKey(bool forAttach)
		{
			if (forAttach)
			{
				return EntityUtil.InvalidOperation(Strings.ObjectStateManager_KeyPropertyDoesntMatchValueInKeyForAttach);
			}
			return EntityUtil.InvalidOperation(Strings.ObjectStateManager_KeyPropertyDoesntMatchValueInKey);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003A8D File Offset: 0x00001C8D
		internal static void CheckValidStateForChangeEntityState(EntityState state)
		{
			if (state <= EntityState.Added)
			{
				if (state - EntityState.Detached <= 1 || state == EntityState.Added)
				{
					return;
				}
			}
			else if (state == EntityState.Deleted || state == EntityState.Modified)
			{
				return;
			}
			throw EntityUtil.InvalidEntityStateArgument("state");
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003AB3 File Offset: 0x00001CB3
		internal static void CheckValidStateForChangeRelationshipState(EntityState state, string paramName)
		{
			if (state - EntityState.Detached > 1 && state != EntityState.Added && state != EntityState.Deleted)
			{
				throw EntityUtil.InvalidRelationshipStateArgument(paramName);
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003ACA File Offset: 0x00001CCA
		internal static InvalidOperationException InvalidKey()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateManager_InvalidKey);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003AD6 File Offset: 0x00001CD6
		internal static InvalidOperationException AcceptChangesEntityKeyIsNotValid()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateManager_AcceptChangesEntityKeyIsNotValid);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003AE2 File Offset: 0x00001CE2
		internal static InvalidOperationException EntityConflictsWithKeyEntry()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateManager_EntityConflictsWithKeyEntry);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003AEE File Offset: 0x00001CEE
		internal static InvalidOperationException ObjectDoesNotHaveAKey(object entity)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateManager_GetEntityKeyRequiresObjectToHaveAKey(entity.GetType().FullName));
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003B05 File Offset: 0x00001D05
		internal static InvalidOperationException EntityValueChangedWithoutEntityValueChanging()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_EntityMemberChangedWithoutEntityMemberChanging);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003B11 File Offset: 0x00001D11
		internal static InvalidOperationException ChangedInDifferentStateFromChanging(EntityState currentState, EntityState previousState)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_ChangedInDifferentStateFromChanging(previousState, currentState));
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003B29 File Offset: 0x00001D29
		internal static ArgumentException ChangeOnUnmappedProperty(string entityPropertyName)
		{
			return EntityUtil.Argument(Strings.ObjectStateEntry_ChangeOnUnmappedProperty(entityPropertyName));
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003B36 File Offset: 0x00001D36
		internal static ArgumentException ChangeOnUnmappedComplexProperty(string complexPropertyName)
		{
			return EntityUtil.Argument(Strings.ObjectStateEntry_ChangeOnUnmappedComplexProperty(complexPropertyName));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003B43 File Offset: 0x00001D43
		internal static ArgumentException EntityTypeDoesNotMatchEntitySet(string entityType, string entitysetName, string argument)
		{
			return EntityUtil.Argument(Strings.ObjectStateManager_EntityTypeDoesnotMatchtoEntitySetType(entityType, entitysetName), argument);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003B52 File Offset: 0x00001D52
		internal static InvalidOperationException NoEntryExistsForObject(object entity)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateManager_NoEntryExistsForObject(entity.GetType().FullName));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003B69 File Offset: 0x00001D69
		internal static InvalidOperationException EntityNotTracked()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateManager_EntityNotTracked);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003B75 File Offset: 0x00001D75
		internal static InvalidOperationException SetOriginalComplexProperties(string propertyName)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_SetOriginalComplexProperties(propertyName));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003B82 File Offset: 0x00001D82
		internal static InvalidOperationException NullOriginalValueForNonNullableProperty(string propertyName, string clrMemberName, string clrTypeName)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_NullOriginalValueForNonNullableProperty(propertyName, clrMemberName, clrTypeName));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003B91 File Offset: 0x00001D91
		internal static InvalidOperationException SetOriginalPrimaryKey(string propertyName)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectStateEntry_SetOriginalPrimaryKey(propertyName));
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003B9E File Offset: 0x00001D9E
		internal static void ThrowPropertyIsNotNullable(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw EntityUtil.Constraint(Strings.Materializer_PropertyIsNotNullable);
			}
			throw EntityUtil.PropertyConstraint(Strings.Materializer_PropertyIsNotNullableWithName(propertyName), propertyName);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003BC0 File Offset: 0x00001DC0
		internal static void ThrowSetInvalidValue(object value, Type destinationType, string className, string propertyName)
		{
			if (value == null)
			{
				throw EntityUtil.Constraint(Strings.Materializer_SetInvalidValue((Nullable.GetUnderlyingType(destinationType) ?? destinationType).Name, className, propertyName, "null"));
			}
			throw EntityUtil.InvalidOperation(Strings.Materializer_SetInvalidValue((Nullable.GetUnderlyingType(destinationType) ?? destinationType).Name, className, propertyName, value.GetType().Name));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003C1C File Offset: 0x00001E1C
		internal static InvalidOperationException ValueInvalidCast(Type valueType, Type destinationType)
		{
			if (destinationType.IsValueType && destinationType.IsGenericType && typeof(Nullable<>) == destinationType.GetGenericTypeDefinition())
			{
				return EntityUtil.InvalidOperation(Strings.Materializer_InvalidCastNullable(valueType, destinationType.GetGenericArguments()[0]));
			}
			return EntityUtil.InvalidOperation(Strings.Materializer_InvalidCastReference(valueType, destinationType));
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003C70 File Offset: 0x00001E70
		internal static InvalidOperationException ValueNullReferenceCast(Type destinationType)
		{
			return EntityUtil.InvalidOperation(Strings.Materializer_NullReferenceCast(destinationType.Name));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003C82 File Offset: 0x00001E82
		internal static NotSupportedException RecyclingEntity(EntityKey key, Type newEntityType, Type existingEntityType)
		{
			return EntityUtil.NotSupported(Strings.Materializer_RecyclingEntity(TypeHelpers.GetFullName(key.EntityContainerName, key.EntitySetName), newEntityType.FullName, existingEntityType.FullName, key.ConcatKeyValue()));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003CB1 File Offset: 0x00001EB1
		internal static InvalidOperationException AddedEntityAlreadyExists(EntityKey key)
		{
			return EntityUtil.InvalidOperation(Strings.Materializer_AddedEntityAlreadyExists(key.ConcatKeyValue()));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003CC3 File Offset: 0x00001EC3
		internal static InvalidOperationException CannotReEnumerateQueryResults()
		{
			return EntityUtil.InvalidOperation(Strings.Materializer_CannotReEnumerateQueryResults);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003CCF File Offset: 0x00001ECF
		internal static NotSupportedException MaterializerUnsupportedType()
		{
			return EntityUtil.NotSupported(Strings.Materializer_UnsupportedType);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003CDB File Offset: 0x00001EDB
		internal static InvalidOperationException CannotReplacetheEntityorRow()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectView_CannotReplacetheEntityorRow);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003CE7 File Offset: 0x00001EE7
		internal static NotSupportedException IndexBasedInsertIsNotSupported()
		{
			return EntityUtil.NotSupported(Strings.ObjectView_IndexBasedInsertIsNotSupported);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003CF3 File Offset: 0x00001EF3
		internal static InvalidOperationException WriteOperationNotAllowedOnReadOnlyBindingList()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectView_WriteOperationNotAllowedOnReadOnlyBindingList);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003CFF File Offset: 0x00001EFF
		internal static InvalidOperationException AddNewOperationNotAllowedOnAbstractBindingList()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectView_AddNewOperationNotAllowedOnAbstractBindingList);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003D0B File Offset: 0x00001F0B
		internal static ArgumentException IncompatibleArgument()
		{
			return EntityUtil.Argument(Strings.ObjectView_IncompatibleArgument);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003D17 File Offset: 0x00001F17
		internal static InvalidOperationException CannotResolveTheEntitySetforGivenEntity(Type type)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectView_CannotResolveTheEntitySet(type.FullName));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003D29 File Offset: 0x00001F29
		internal static InvalidOperationException NoRelationshipSetMatched(string relationshipName)
		{
			return EntityUtil.InvalidOperation(Strings.Collections_NoRelationshipSetMatched(relationshipName));
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003D36 File Offset: 0x00001F36
		internal static InvalidOperationException ExpectedCollectionGotReference(string typeName, string roleName, string relationshipName)
		{
			return EntityUtil.InvalidOperation(Strings.Collections_ExpectedCollectionGotReference(typeName, roleName, relationshipName));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003D45 File Offset: 0x00001F45
		internal static InvalidOperationException CannotFillTryDifferentMergeOption(string relationshipName, string roleName)
		{
			return EntityUtil.InvalidOperation(Strings.Collections_CannotFillTryDifferentMergeOption(relationshipName, roleName));
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003D53 File Offset: 0x00001F53
		internal static InvalidOperationException CannotRemergeCollections()
		{
			return EntityUtil.InvalidOperation(Strings.Collections_UnableToMergeCollections);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00003D5F File Offset: 0x00001F5F
		internal static InvalidOperationException ExpectedReferenceGotCollection(string typeName, string roleName, string relationshipName)
		{
			return EntityUtil.InvalidOperation(Strings.EntityReference_ExpectedReferenceGotCollection(typeName, roleName, relationshipName));
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003D6E File Offset: 0x00001F6E
		internal static InvalidOperationException CannotAddMoreThanOneEntityToEntityReference(string roleName, string relationshipName)
		{
			return EntityUtil.InvalidOperation(Strings.EntityReference_CannotAddMoreThanOneEntityToEntityReference(roleName, relationshipName));
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003D7C File Offset: 0x00001F7C
		internal static ArgumentException CannotSetSpecialKeys()
		{
			return EntityUtil.Argument(Strings.EntityReference_CannotSetSpecialKeys, "value");
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003D8D File Offset: 0x00001F8D
		internal static InvalidOperationException EntityKeyValueMismatch()
		{
			return EntityUtil.InvalidOperation(Strings.EntityReference_EntityKeyValueMismatch);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003D99 File Offset: 0x00001F99
		internal static InvalidOperationException RelatedEndNotAttachedToContext(string relatedEndType)
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_RelatedEndNotAttachedToContext(relatedEndType));
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003DA6 File Offset: 0x00001FA6
		internal static InvalidOperationException CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(string roleName)
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(roleName));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003DB3 File Offset: 0x00001FB3
		internal static InvalidOperationException CannotCreateRelationshipEntitiesInDifferentContexts()
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003DBF File Offset: 0x00001FBF
		internal static InvalidOperationException InvalidContainedTypeCollection(string entityType, string relatedEndType)
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_InvalidContainedType_Collection(entityType, relatedEndType));
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003DCD File Offset: 0x00001FCD
		internal static InvalidOperationException InvalidContainedTypeReference(string entityType, string relatedEndType)
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_InvalidContainedType_Reference(entityType, relatedEndType));
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003DDB File Offset: 0x00001FDB
		internal static InvalidOperationException CannotAddToFixedSizeArray(object collectionType)
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_CannotAddToFixedSizeArray(collectionType.GetType()));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003DED File Offset: 0x00001FED
		internal static InvalidOperationException CannotRemoveFromFixedSizeArray(object collectionType)
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_CannotRemoveFromFixedSizeArray(collectionType.GetType()));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003DFF File Offset: 0x00001FFF
		internal static InvalidOperationException OwnerIsNull()
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_OwnerIsNull);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003E0B File Offset: 0x0000200B
		internal static InvalidOperationException UnableToAddRelationshipWithDeletedEntity()
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_UnableToAddRelationshipWithDeletedEntity);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003E17 File Offset: 0x00002017
		internal static InvalidOperationException ConflictingChangeOfRelationshipDetected()
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_ConflictingChangeOfRelationshipDetected);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003E23 File Offset: 0x00002023
		internal static InvalidOperationException InvalidRelationshipFixupDetected(string propertyName, string entityType)
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_InvalidRelationshipFixupDetected(propertyName, entityType));
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003E31 File Offset: 0x00002031
		internal static InvalidOperationException LessThanExpectedRelatedEntitiesFound()
		{
			return EntityUtil.InvalidOperation(Strings.EntityReference_LessThanExpectedRelatedEntitiesFound);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003E3D File Offset: 0x0000203D
		internal static InvalidOperationException MoreThanExpectedRelatedEntitiesFound()
		{
			return EntityUtil.InvalidOperation(Strings.EntityReference_MoreThanExpectedRelatedEntitiesFound);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003E49 File Offset: 0x00002049
		internal static InvalidOperationException CannotChangeReferentialConstraintProperty()
		{
			return EntityUtil.InvalidOperation(Strings.EntityReference_CannotChangeReferentialConstraintProperty);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003E55 File Offset: 0x00002055
		internal static InvalidOperationException RelatedEndNotFound()
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_RelatedEndNotFound);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003E61 File Offset: 0x00002061
		internal static InvalidOperationException LoadCalledOnNonEmptyNoTrackedRelatedEnd()
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00003E6D File Offset: 0x0000206D
		internal static InvalidOperationException LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd()
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003E79 File Offset: 0x00002079
		internal static InvalidOperationException MismatchedMergeOptionOnLoad(MergeOption mergeOption)
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_MismatchedMergeOptionOnLoad(mergeOption));
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00003E8B File Offset: 0x0000208B
		internal static InvalidOperationException EntitySetIsNotValidForRelationship(string entitySetContainerName, string entitySetName, string roleName, string associationSetContainerName, string associationSetName)
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_EntitySetIsNotValidForRelationship(entitySetContainerName, entitySetName, roleName, associationSetContainerName, associationSetName));
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003E9D File Offset: 0x0000209D
		internal static InvalidOperationException UnableToRetrieveReferentialConstraintProperties()
		{
			return EntityUtil.InvalidOperation(Strings.RelationshipManager_UnableToRetrieveReferentialConstraintProperties);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003EA9 File Offset: 0x000020A9
		internal static InvalidOperationException InconsistentReferentialConstraintProperties()
		{
			return EntityUtil.InvalidOperation(Strings.RelationshipManager_InconsistentReferentialConstraintProperties);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00003EB5 File Offset: 0x000020B5
		internal static InvalidOperationException CircularRelationshipsWithReferentialConstraints()
		{
			return EntityUtil.InvalidOperation(Strings.RelationshipManager_CircularRelationshipsWithReferentialConstraints);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00003EC1 File Offset: 0x000020C1
		internal static ArgumentException UnableToFindRelationshipTypeInMetadata(string relationshipName, string parameterName)
		{
			return EntityUtil.Argument(Strings.RelationshipManager_UnableToFindRelationshipTypeInMetadata(relationshipName), parameterName);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003ECF File Offset: 0x000020CF
		internal static ArgumentException InvalidTargetRole(string relationshipName, string targetRoleName, string parameterName)
		{
			return EntityUtil.Argument(Strings.RelationshipManager_InvalidTargetRole(relationshipName, targetRoleName), parameterName);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003EDE File Offset: 0x000020DE
		internal static InvalidOperationException OwnerIsNotSourceType(string ownerType, string sourceRoleType, string sourceRoleName, string relationshipName)
		{
			return EntityUtil.InvalidOperation(Strings.RelationshipManager_OwnerIsNotSourceType(ownerType, sourceRoleType, sourceRoleName, relationshipName));
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00003EEE File Offset: 0x000020EE
		internal static InvalidOperationException UnexpectedNullContext()
		{
			return EntityUtil.InvalidOperation(Strings.RelationshipManager_UnexpectedNullContext);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003EFA File Offset: 0x000020FA
		internal static InvalidOperationException ReferenceAlreadyInitialized()
		{
			return EntityUtil.InvalidOperation(Strings.RelationshipManager_ReferenceAlreadyInitialized(Strings.RelationshipManager_InitializeIsForDeserialization));
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003F0B File Offset: 0x0000210B
		internal static InvalidOperationException RelationshipManagerAttached()
		{
			return EntityUtil.InvalidOperation(Strings.RelationshipManager_RelationshipManagerAttached(Strings.RelationshipManager_InitializeIsForDeserialization));
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003F1C File Offset: 0x0000211C
		internal static InvalidOperationException CollectionAlreadyInitialized()
		{
			return EntityUtil.InvalidOperation(Strings.RelationshipManager_CollectionAlreadyInitialized(Strings.RelationshipManager_CollectionInitializeIsForDeserialization));
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003F2D File Offset: 0x0000212D
		internal static InvalidOperationException CollectionRelationshipManagerAttached()
		{
			return EntityUtil.InvalidOperation(Strings.RelationshipManager_CollectionRelationshipManagerAttached(Strings.RelationshipManager_CollectionInitializeIsForDeserialization));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003F3E File Offset: 0x0000213E
		internal static void CheckContextNull(ObjectContext context)
		{
			if (context == null)
			{
				throw EntityUtil.UnexpectedNullContext();
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003F49 File Offset: 0x00002149
		internal static void CheckArgumentMergeOption(MergeOption mergeOption)
		{
			if (mergeOption > MergeOption.NoTracking)
			{
				throw EntityUtil.InvalidMergeOption(mergeOption);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00003F56 File Offset: 0x00002156
		internal static void CheckArgumentRefreshMode(RefreshMode refreshMode)
		{
			if (refreshMode - RefreshMode.StoreWins > 1)
			{
				throw EntityUtil.InvalidRefreshMode(refreshMode);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00003F65 File Offset: 0x00002165
		internal static InvalidOperationException InvalidEntityStateSource()
		{
			return EntityUtil.InvalidOperation(Strings.Collections_InvalidEntityStateSource);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00003F71 File Offset: 0x00002171
		internal static InvalidOperationException InvalidEntityStateLoad(string relatedEndType)
		{
			return EntityUtil.InvalidOperation(Strings.Collections_InvalidEntityStateLoad(relatedEndType));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00003F7E File Offset: 0x0000217E
		internal static InvalidOperationException InvalidOwnerStateForAttach()
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_InvalidOwnerStateForAttach);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003F8A File Offset: 0x0000218A
		internal static InvalidOperationException InvalidNthElementNullForAttach(int index)
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_InvalidNthElementNullForAttach(index));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00003F9C File Offset: 0x0000219C
		internal static InvalidOperationException InvalidNthElementContextForAttach(int index)
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_InvalidNthElementContextForAttach(index));
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003FAE File Offset: 0x000021AE
		internal static InvalidOperationException InvalidNthElementStateForAttach(int index)
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_InvalidNthElementStateForAttach(index));
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00003FC0 File Offset: 0x000021C0
		internal static InvalidOperationException InvalidEntityContextForAttach()
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_InvalidEntityContextForAttach);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00003FCC File Offset: 0x000021CC
		internal static InvalidOperationException InvalidEntityStateForAttach()
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_InvalidEntityStateForAttach);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00003FD8 File Offset: 0x000021D8
		internal static InvalidOperationException UnableToAddToDisconnectedRelatedEnd()
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_UnableToAddEntity);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00003FE4 File Offset: 0x000021E4
		internal static InvalidOperationException UnableToRemoveFromDisconnectedRelatedEnd()
		{
			return EntityUtil.InvalidOperation(Strings.RelatedEnd_UnableToRemoveEntity);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00003FF0 File Offset: 0x000021F0
		internal static InvalidOperationException ProxyMetadataIsUnavailable(Type type, Exception inner)
		{
			return EntityUtil.InvalidOperation(Strings.EntityProxyTypeInfo_ProxyMetadataIsUnavailable(type.FullName), inner);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004003 File Offset: 0x00002203
		internal static InvalidOperationException DuplicateTypeForProxyType(Type type)
		{
			return EntityUtil.InvalidOperation(Strings.EntityProxyTypeInfo_DuplicateOSpaceType(type.FullName));
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004015 File Offset: 0x00002215
		internal static InvalidOperationException ClientEntityRemovedFromStore(string entitiesKeys)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_ClientEntityRemovedFromStore(entitiesKeys));
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004022 File Offset: 0x00002222
		internal static InvalidOperationException StoreEntityNotPresentInClient()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_StoreEntityNotPresentInClient);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000402E File Offset: 0x0000222E
		internal static InvalidOperationException ContextMetadataHasChanged()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_MetadataHasChanged);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000403A File Offset: 0x0000223A
		internal static ArgumentException InvalidConnection(bool isConnectionConstructor, Exception innerException)
		{
			if (isConnectionConstructor)
			{
				return EntityUtil.InvalidConnection("connection", innerException);
			}
			return EntityUtil.InvalidConnectionString("connectionString", innerException);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004056 File Offset: 0x00002256
		internal static ArgumentException InvalidConnectionString(string parameter, Exception inner)
		{
			return EntityUtil.Argument(Strings.ObjectContext_InvalidConnectionString, parameter, inner);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004064 File Offset: 0x00002264
		internal static ArgumentException InvalidConnection(string parameter, Exception inner)
		{
			return EntityUtil.Argument(Strings.ObjectContext_InvalidConnection, parameter, inner);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004072 File Offset: 0x00002272
		internal static InvalidOperationException InvalidDataAdapter()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_InvalidDataAdapter);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000407E File Offset: 0x0000227E
		internal static ArgumentException InvalidDefaultContainerName(string parameter, string defaultContainerName)
		{
			return EntityUtil.Argument(Strings.ObjectContext_InvalidDefaultContainerName(defaultContainerName), parameter);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000408C File Offset: 0x0000228C
		internal static InvalidOperationException NthElementInAddedState(int i)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_NthElementInAddedState(i));
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000409E File Offset: 0x0000229E
		internal static InvalidOperationException NthElementIsDuplicate(int i)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_NthElementIsDuplicate(i));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000040B0 File Offset: 0x000022B0
		internal static InvalidOperationException NthElementIsNull(int i)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_NthElementIsNull(i));
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000040C2 File Offset: 0x000022C2
		internal static InvalidOperationException NthElementNotInObjectStateManager(int i)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_NthElementNotInObjectStateManager(i));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000040D4 File Offset: 0x000022D4
		internal static ObjectDisposedException ObjectContextDisposed()
		{
			return EntityUtil.ObjectDisposed(Strings.ObjectContext_ObjectDisposed);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000040E0 File Offset: 0x000022E0
		internal static ObjectNotFoundException ObjectNotFound()
		{
			return EntityUtil.ObjectNotFound(Strings.ObjectContext_ObjectNotFound);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000040EC File Offset: 0x000022EC
		internal static InvalidOperationException InvalidEntityType(Type type)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_NoMappingForEntityType(type.FullName));
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000040FE File Offset: 0x000022FE
		internal static InvalidOperationException CannotDeleteEntityNotInObjectStateManager()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_CannotDeleteEntityNotInObjectStateManager);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000410A File Offset: 0x0000230A
		internal static InvalidOperationException CannotDetachEntityNotInObjectStateManager()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_CannotDetachEntityNotInObjectStateManager);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004116 File Offset: 0x00002316
		internal static InvalidOperationException EntitySetNotFoundForName(string entitySetName)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_EntitySetNotFoundForName(entitySetName));
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004123 File Offset: 0x00002323
		internal static InvalidOperationException EntityContainterNotFoundForName(string entityContainerName)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_EntityContainerNotFoundForName(entityContainerName));
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004130 File Offset: 0x00002330
		internal static ArgumentException InvalidCommandTimeout(string argument)
		{
			return EntityUtil.Argument(Strings.ObjectContext_InvalidCommandTimeout, argument);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000413D File Offset: 0x0000233D
		internal static InvalidOperationException EntityAlreadyExistsInObjectStateManager()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_EntityAlreadyExistsInObjectStateManager);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004149 File Offset: 0x00002349
		internal static InvalidOperationException InvalidEntitySetInKey(string keyContainer, string keyEntitySet, string expectedContainer, string expectedEntitySet)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_InvalidEntitySetInKey(keyContainer, keyEntitySet, expectedContainer, expectedEntitySet));
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004159 File Offset: 0x00002359
		internal static InvalidOperationException InvalidEntitySetInKeyFromName(string keyContainer, string keyEntitySet, string expectedContainer, string expectedEntitySet, string argument)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_InvalidEntitySetInKeyFromName(keyContainer, keyEntitySet, expectedContainer, expectedEntitySet, argument));
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000416B File Offset: 0x0000236B
		internal static InvalidOperationException CannotAttachEntityWithoutKey()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_CannotAttachEntityWithoutKey);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004177 File Offset: 0x00002377
		internal static InvalidOperationException CannotAttachEntityWithTemporaryKey()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_CannotAttachEntityWithTemporaryKey);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004183 File Offset: 0x00002383
		internal static InvalidOperationException EntitySetNameOrEntityKeyRequired()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_EntitySetNameOrEntityKeyRequired);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000418F File Offset: 0x0000238F
		internal static InvalidOperationException ExecuteFunctionTypeMismatch(Type typeArgument, EdmType expectedElementType)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_ExecuteFunctionTypeMismatch(typeArgument.FullName, expectedElementType.FullName));
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000041A8 File Offset: 0x000023A8
		internal static InvalidOperationException ExecuteFunctionCalledWithNonReaderFunction(EdmFunction functionImport)
		{
			string error;
			if (functionImport.ReturnParameter == null)
			{
				error = Strings.ObjectContext_ExecuteFunctionCalledWithNonQueryFunction(functionImport.Name);
			}
			else
			{
				error = Strings.ObjectContext_ExecuteFunctionCalledWithScalarFunction(functionImport.ReturnParameter.TypeUsage.EdmType.FullName, functionImport.Name);
			}
			return EntityUtil.InvalidOperation(error);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000041F2 File Offset: 0x000023F2
		internal static ArgumentException QualfiedEntitySetName(string parameterName)
		{
			return EntityUtil.Argument(Strings.ObjectContext_QualfiedEntitySetName, parameterName);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000041FF File Offset: 0x000023FF
		internal static ArgumentException ContainerQualifiedEntitySetNameRequired(string argument)
		{
			return EntityUtil.Argument(Strings.ObjectContext_ContainerQualifiedEntitySetNameRequired, argument);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000420C File Offset: 0x0000240C
		internal static InvalidOperationException CannotSetDefaultContainerName()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_CannotSetDefaultContainerName);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004218 File Offset: 0x00002418
		internal static ArgumentException EntitiesHaveDifferentType(string originalEntityTypeName, string changedEntityTypeName)
		{
			return EntityUtil.Argument(Strings.ObjectContext_EntitiesHaveDifferentType(originalEntityTypeName, changedEntityTypeName));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004226 File Offset: 0x00002426
		internal static InvalidOperationException EntityMustBeUnchangedOrModified(EntityState state)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_EntityMustBeUnchangedOrModified(state.ToString()));
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000423F File Offset: 0x0000243F
		internal static InvalidOperationException EntityMustBeUnchangedOrModifiedOrDeleted(EntityState state)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_EntityMustBeUnchangedOrModifiedOrDeleted(state.ToString()));
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00004258 File Offset: 0x00002458
		internal static InvalidOperationException EntityNotTrackedOrHasTempKey()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_EntityNotTrackedOrHasTempKey);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004264 File Offset: 0x00002464
		internal static InvalidOperationException AcceptAllChangesFailure(Exception e)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_AcceptAllChangesFailure(e.Message));
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004276 File Offset: 0x00002476
		internal static ArgumentException InvalidEntitySetOnEntity(string entitySetName, Type entityType, string parameter)
		{
			return EntityUtil.Argument(Strings.ObjectContext_InvalidEntitySetOnEntity(entitySetName, entityType), parameter);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00004285 File Offset: 0x00002485
		internal static ArgumentException InvalidEntityTypeForObjectSet(string tEntityType, string entitySetType, string entitySetName, string parameter)
		{
			return EntityUtil.Argument(Strings.ObjectContext_InvalidObjectSetTypeForEntitySet(tEntityType, entitySetType, entitySetName), parameter);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004295 File Offset: 0x00002495
		internal static InvalidOperationException RequiredMetadataNotAvailable()
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_RequiredMetadataNotAvailble);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000042A1 File Offset: 0x000024A1
		internal static ArgumentException MultipleEntitySetsFoundInSingleContainer(string entityTypeName, string entityContainerName, string exceptionParameterName)
		{
			return EntityUtil.Argument(Strings.ObjectContext_MultipleEntitySetsFoundInSingleContainer(entityTypeName, entityContainerName), exceptionParameterName);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000042B0 File Offset: 0x000024B0
		internal static ArgumentException MultipleEntitySetsFoundInAllContainers(string entityTypeName, string exceptionParameterName)
		{
			return EntityUtil.Argument(Strings.ObjectContext_MultipleEntitySetsFoundInAllContainers(entityTypeName), exceptionParameterName);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000042BE File Offset: 0x000024BE
		internal static ArgumentException NoEntitySetFoundForType(string entityTypeName, string exceptionParameterName)
		{
			return EntityUtil.Argument(Strings.ObjectContext_NoEntitySetFoundForType(entityTypeName), exceptionParameterName);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000042CC File Offset: 0x000024CC
		internal static InvalidOperationException EntityNotInObjectSet_Delete(string actualContainerName, string actualEntitySetName, string expectedContainerName, string expectedEntitySetName)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_EntityNotInObjectSet_Delete(actualContainerName, actualEntitySetName, expectedContainerName, expectedEntitySetName));
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000042DC File Offset: 0x000024DC
		internal static InvalidOperationException EntityNotInObjectSet_Detach(string actualContainerName, string actualEntitySetName, string expectedContainerName, string expectedEntitySetName)
		{
			return EntityUtil.InvalidOperation(Strings.ObjectContext_EntityNotInObjectSet_Detach(actualContainerName, actualEntitySetName, expectedContainerName, expectedEntitySetName));
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000042EC File Offset: 0x000024EC
		internal static ArgumentException InvalidRelationshipStateArgument(string paramName)
		{
			return new ArgumentException(Strings.ObjectContext_InvalidRelationshipState, paramName);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000042F9 File Offset: 0x000024F9
		internal static ArgumentException InvalidEntityStateArgument(string paramName)
		{
			return new ArgumentException(Strings.ObjectContext_InvalidEntityState, paramName);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004306 File Offset: 0x00002506
		internal static InvalidOperationException NullableComplexTypesNotSupported(string propertyName)
		{
			return EntityUtil.InvalidOperation(Strings.ComplexObject_NullableComplexTypesNotSupported(propertyName));
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004313 File Offset: 0x00002513
		internal static InvalidOperationException ComplexObjectAlreadyAttachedToParent()
		{
			return EntityUtil.InvalidOperation(Strings.ComplexObject_ComplexObjectAlreadyAttachedToParent);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000431F File Offset: 0x0000251F
		internal static ArgumentException ComplexChangeRequestedOnScalarProperty(string propertyName)
		{
			return EntityUtil.Argument(Strings.ComplexObject_ComplexChangeRequestedOnScalarProperty(propertyName));
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000432C File Offset: 0x0000252C
		internal static ArgumentException SpanPathSyntaxError()
		{
			return EntityUtil.Argument(Strings.ObjectQuery_Span_SpanPathSyntaxError);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004338 File Offset: 0x00002538
		internal static ArgumentException ADP_InvalidMultipartNameDelimiterUsage()
		{
			return EntityUtil.Argument(Strings.ADP_InvalidMultipartNameDelimiterUsage, "path");
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004349 File Offset: 0x00002549
		internal static Exception InvalidConnectionOptionValue(string key)
		{
			return EntityUtil.Argument(Strings.ADP_InvalidConnectionOptionValue(key));
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004356 File Offset: 0x00002556
		internal static ArgumentException InvalidSizeValue(int value)
		{
			return EntityUtil.Argument(Strings.ADP_InvalidSizeValue(value.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000436E File Offset: 0x0000256E
		internal static ArgumentException ConnectionStringSyntax(int index)
		{
			return EntityUtil.Argument(Strings.ADP_ConnectionStringSyntax(index));
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004380 File Offset: 0x00002580
		internal static InvalidOperationException DataRecordMustBeEntity()
		{
			return EntityUtil.InvalidOperation(Strings.EntityKey_DataRecordMustBeEntity);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000438C File Offset: 0x0000258C
		internal static ArgumentException EntitySetDoesNotMatch(string argument, string entitySetName)
		{
			return EntityUtil.Argument(Strings.EntityKey_EntitySetDoesNotMatch(entitySetName), argument);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000439A File Offset: 0x0000259A
		internal static InvalidOperationException EntityTypesDoNotMatch(string recordType, string entitySetType)
		{
			return EntityUtil.InvalidOperation(Strings.EntityKey_EntityTypesDoNotMatch(recordType, entitySetType));
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000043A8 File Offset: 0x000025A8
		internal static ArgumentException IncorrectNumberOfKeyValuePairs(string argument, string typeName, int expectedNumFields, int actualNumFields)
		{
			return EntityUtil.Argument(Strings.EntityKey_IncorrectNumberOfKeyValuePairs(typeName, expectedNumFields, actualNumFields), argument);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000043C2 File Offset: 0x000025C2
		internal static InvalidOperationException IncorrectNumberOfKeyValuePairsInvalidOperation(string typeName, int expectedNumFields, int actualNumFields)
		{
			return EntityUtil.InvalidOperation(Strings.EntityKey_IncorrectNumberOfKeyValuePairs(typeName, expectedNumFields, actualNumFields));
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000043DB File Offset: 0x000025DB
		internal static ArgumentException IncorrectValueType(string argument, string keyField, string expectedTypeName, string actualTypeName)
		{
			return EntityUtil.Argument(Strings.EntityKey_IncorrectValueType(keyField, expectedTypeName, actualTypeName), argument);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000043EB File Offset: 0x000025EB
		internal static InvalidOperationException IncorrectValueTypeInvalidOperation(string keyField, string expectedTypeName, string actualTypeName)
		{
			return EntityUtil.InvalidOperation(Strings.EntityKey_IncorrectValueType(keyField, expectedTypeName, actualTypeName));
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000043FA File Offset: 0x000025FA
		internal static ArgumentException NoCorrespondingOSpaceTypeForEnumKeyField(string argument, string keyField, string cspaceTypeName)
		{
			return EntityUtil.Argument(Strings.EntityKey_NoCorrespondingOSpaceTypeForEnumKeyMember(keyField, cspaceTypeName), argument);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004409 File Offset: 0x00002609
		internal static InvalidOperationException NoCorrespondingOSpaceTypeForEnumKeyFieldInvalidOperation(string keyField, string cspaceTypeName)
		{
			return EntityUtil.InvalidOperation(Strings.EntityKey_NoCorrespondingOSpaceTypeForEnumKeyMember(keyField, cspaceTypeName));
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004417 File Offset: 0x00002617
		internal static ArgumentException MissingKeyValue(string argument, string keyField, string typeName)
		{
			return EntityUtil.MissingKeyValue(argument, keyField, typeName, null);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00004422 File Offset: 0x00002622
		internal static ArgumentException MissingKeyValue(string argument, string keyField, string typeName, Exception inner)
		{
			return EntityUtil.Argument(Strings.EntityKey_MissingKeyValue(keyField, typeName), argument);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004431 File Offset: 0x00002631
		internal static InvalidOperationException NullKeyValue(string keyField, string typeName)
		{
			return EntityUtil.InvalidOperation(Strings.EntityKey_NullKeyValue(keyField, typeName));
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000443F File Offset: 0x0000263F
		internal static InvalidOperationException MissingKeyValueInvalidOperation(string keyField, string typeName)
		{
			return EntityUtil.InvalidOperation(Strings.EntityKey_MissingKeyValue(keyField, typeName));
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000444D File Offset: 0x0000264D
		internal static ArgumentException NoNullsAllowedInKeyValuePairs(string argument)
		{
			return EntityUtil.Argument(Strings.EntityKey_NoNullsAllowedInKeyValuePairs, argument);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000445A File Offset: 0x0000265A
		internal static ArgumentException EntityKeyMustHaveValues(string argument)
		{
			return EntityUtil.Argument(Strings.EntityKey_EntityKeyMustHaveValues, argument);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00004467 File Offset: 0x00002667
		internal static ArgumentException InvalidQualifiedEntitySetName()
		{
			return EntityUtil.Argument(Strings.EntityKey_InvalidQualifiedEntitySetName, "qualifiedEntitySetName");
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00004478 File Offset: 0x00002678
		internal static ArgumentException EntityKeyInvalidName(string invalidName)
		{
			return EntityUtil.Argument(Strings.EntityKey_InvalidName(invalidName));
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004485 File Offset: 0x00002685
		internal static InvalidOperationException MissingQualifiedEntitySetName()
		{
			return EntityUtil.InvalidOperation(Strings.EntityKey_MissingEntitySetName);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00004491 File Offset: 0x00002691
		internal static InvalidOperationException CannotChangeEntityKey()
		{
			return EntityUtil.InvalidOperation(Strings.EntityKey_CannotChangeKey);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000449D File Offset: 0x0000269D
		internal static InvalidOperationException UnexpectedNullEntityKey()
		{
			return new InvalidOperationException(Strings.EntityKey_UnexpectedNull);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000044A9 File Offset: 0x000026A9
		internal static InvalidOperationException EntityKeyDoesntMatchKeySetOnEntity(object entity)
		{
			return new InvalidOperationException(Strings.EntityKey_DoesntMatchKeyOnEntity(entity.GetType().FullName));
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000044C0 File Offset: 0x000026C0
		internal static void CheckEntityKeyNull(EntityKey entityKey)
		{
			if (entityKey == null)
			{
				throw EntityUtil.UnexpectedNullEntityKey();
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000044CB File Offset: 0x000026CB
		internal static void CheckEntityKeysMatch(IEntityWrapper wrappedEntity, EntityKey key)
		{
			if (wrappedEntity.EntityKey != key)
			{
				throw EntityUtil.EntityKeyDoesntMatchKeySetOnEntity(wrappedEntity.Entity);
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000044E7 File Offset: 0x000026E7
		internal static InvalidOperationException UnexpectedNullRelationshipManager()
		{
			return new InvalidOperationException(Strings.RelationshipManager_UnexpectedNull);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000044F3 File Offset: 0x000026F3
		internal static InvalidOperationException InvalidRelationshipManagerOwner()
		{
			return EntityUtil.InvalidOperation(Strings.RelationshipManager_InvalidRelationshipManagerOwner);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000044FF File Offset: 0x000026FF
		internal static void ValidateEntitySetInKey(EntityKey key, EntitySet entitySet)
		{
			EntityUtil.ValidateEntitySetInKey(key, entitySet, null);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000450C File Offset: 0x0000270C
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
				throw EntityUtil.InvalidEntitySetInKey(entityContainerName, entitySetName, name, name2);
			}
			throw EntityUtil.InvalidEntitySetInKeyFromName(entityContainerName, entitySetName, name, name2, argument);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00004573 File Offset: 0x00002773
		internal static ArgumentOutOfRangeException InvalidMergeOption(MergeOption value)
		{
			return EntityUtil.InvalidEnumerationValue(typeof(MergeOption), (int)value);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00004585 File Offset: 0x00002785
		internal static ArgumentOutOfRangeException InvalidRefreshMode(RefreshMode value)
		{
			return EntityUtil.InvalidEnumerationValue(typeof(RefreshMode), (int)value);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00004597 File Offset: 0x00002797
		internal static ArgumentException InvalidDataType(TypeCode typecode)
		{
			return EntityUtil.Argument(Strings.ADP_InvalidDataType(typecode.ToString()));
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000045B0 File Offset: 0x000027B0
		internal static ArgumentException UnknownDataTypeCode(Type dataType, TypeCode typeCode)
		{
			int num = (int)typeCode;
			return EntityUtil.Argument(Strings.ADP_UnknownDataTypeCode(num.ToString(CultureInfo.InvariantCulture), dataType.FullName));
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000045DB File Offset: 0x000027DB
		internal static ArgumentOutOfRangeException InvalidParameterDirection(ParameterDirection value)
		{
			return EntityUtil.InvalidEnumerationValue(typeof(ParameterDirection), (int)value);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000045ED File Offset: 0x000027ED
		internal static ArgumentOutOfRangeException InvalidDataRowVersion(DataRowVersion value)
		{
			return EntityUtil.InvalidEnumerationValue(typeof(DataRowVersion), (int)value);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000045FF File Offset: 0x000027FF
		private static IEnumerable<ObjectStateEntry> ProcessStateEntries(IEnumerable<IEntityStateEntry> stateEntries)
		{
			return stateEntries.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000460C File Offset: 0x0000280C
		internal static void ValidateNecessaryModificationFunctionMapping(StorageModificationFunctionMapping mapping, string currentState, IEntityStateEntry stateEntry, string type, string typeName)
		{
			if (mapping == null)
			{
				throw EntityUtil.Update(Strings.Update_MissingFunctionMapping(currentState, type, typeName), null, new List<IEntityStateEntry>
				{
					stateEntry
				});
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000462D File Offset: 0x0000282D
		internal static UpdateException Update(string message, Exception innerException, params IEntityStateEntry[] stateEntries)
		{
			return EntityUtil.Update(message, innerException, stateEntries);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00004637 File Offset: 0x00002837
		internal static UpdateException Update(string message, Exception innerException, IEnumerable<IEntityStateEntry> stateEntries)
		{
			return new UpdateException(message, innerException, EntityUtil.ProcessStateEntries(stateEntries));
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00004648 File Offset: 0x00002848
		internal static OptimisticConcurrencyException UpdateConcurrency(long rowsAffected, Exception innerException, IEnumerable<IEntityStateEntry> stateEntries)
		{
			string message = Strings.Update_ConcurrencyError(rowsAffected);
			return new OptimisticConcurrencyException(message, innerException, EntityUtil.ProcessStateEntries(stateEntries));
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00004670 File Offset: 0x00002870
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

		// Token: 0x06000199 RID: 409 RVA: 0x000046E4 File Offset: 0x000028E4
		internal static UpdateException UpdateEntityMissingConstraintViolation(string relationshipSetName, string endName, IEntityStateEntry stateEntry)
		{
			string message = Strings.Update_MissingRequiredEntity(relationshipSetName, stateEntry.State, endName);
			return EntityUtil.Update(message, null, new IEntityStateEntry[]
			{
				stateEntry
			});
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004718 File Offset: 0x00002918
		private static string ConvertCardinalityToString(int? cardinality)
		{
			string result;
			if (cardinality == null)
			{
				result = "*";
			}
			else
			{
				result = cardinality.Value.ToString(CultureInfo.CurrentCulture);
			}
			return result;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000474C File Offset: 0x0000294C
		internal static UpdateException UpdateMissingEntity(string relationshipSetName, string entitySetName)
		{
			return EntityUtil.Update(Strings.Update_MissingEntity(relationshipSetName, entitySetName), null, new IEntityStateEntry[0]);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00004761 File Offset: 0x00002961
		internal static ArgumentException CollectionParameterElementIsNull(string parameterName)
		{
			return EntityUtil.Argument(Strings.ADP_CollectionParameterElementIsNull(parameterName));
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000476E File Offset: 0x0000296E
		internal static ArgumentException CollectionParameterElementIsNullOrEmpty(string parameterName)
		{
			return EntityUtil.Argument(Strings.ADP_CollectionParameterElementIsNullOrEmpty(parameterName));
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000477B File Offset: 0x0000297B
		internal static InvalidOperationException FunctionHasNoDefinition(EdmFunction function)
		{
			return EntityUtil.InvalidOperation(Strings.Cqt_UDF_FunctionHasNoDefinition(function.Identity));
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000478D File Offset: 0x0000298D
		internal static InvalidOperationException FunctionDefinitionResultTypeMismatch(EdmFunction function, TypeUsage generatedDefinitionResultType)
		{
			return EntityUtil.InvalidOperation(Strings.Cqt_UDF_FunctionDefinitionResultTypeMismatch(TypeHelpers.GetFullName(function.ReturnParameter.TypeUsage), function.FullName, TypeHelpers.GetFullName(generatedDefinitionResultType)));
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000047B5 File Offset: 0x000029B5
		internal static Exception EntityParameterCollectionInvalidIndex(int index, int count)
		{
			return new IndexOutOfRangeException(Strings.EntityParameterCollectionInvalidIndex(index.ToString(CultureInfo.InvariantCulture), count.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000047D9 File Offset: 0x000029D9
		internal static Exception EntityParameterCollectionInvalidParameterName(string parameterName)
		{
			return new IndexOutOfRangeException(Strings.EntityParameterCollectionInvalidParameterName(parameterName));
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000047E6 File Offset: 0x000029E6
		internal static Exception EntityParameterNull(string parameter)
		{
			return new ArgumentNullException(parameter, Strings.EntityParameterNull);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000047F3 File Offset: 0x000029F3
		internal static Exception InvalidEntityParameterType(object invalidValue)
		{
			return new InvalidCastException(Strings.InvalidEntityParameterType(invalidValue.GetType().Name));
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000480A File Offset: 0x00002A0A
		internal static ArgumentException EntityParameterCollectionRemoveInvalidObject()
		{
			return new ArgumentException(Strings.EntityParameterCollectionRemoveInvalidObject);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00004816 File Offset: 0x00002A16
		internal static ArgumentException EntityParameterContainedByAnotherCollection()
		{
			return new ArgumentException(Strings.EntityParameterContainedByAnotherCollection);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00004822 File Offset: 0x00002A22
		internal static void ThrowArgumentNullException(string parameterName)
		{
			throw EntityUtil.ArgumentNull(parameterName);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000482A File Offset: 0x00002A2A
		internal static void ThrowArgumentOutOfRangeException(string parameterName)
		{
			throw EntityUtil.ArgumentOutOfRange(parameterName);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00004832 File Offset: 0x00002A32
		internal static T CheckArgumentOutOfRange<T>(T[] values, int index, string parameterName)
		{
			if (values.Length <= index)
			{
				EntityUtil.ThrowArgumentOutOfRangeException(parameterName);
			}
			return values[index];
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00004847 File Offset: 0x00002A47
		internal static T CheckArgumentNull<T>(T value, string parameterName) where T : class
		{
			if (value == null)
			{
				EntityUtil.ThrowArgumentNullException(parameterName);
			}
			return value;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00004858 File Offset: 0x00002A58
		internal static IEnumerable<T> CheckArgumentContainsNull<T>(ref IEnumerable<T> enumerableArgument, string argumentName) where T : class
		{
			EntityUtil.GetCheapestSafeEnumerableAsCollection<T>(ref enumerableArgument);
			foreach (T t in enumerableArgument)
			{
				if (t == null)
				{
					throw EntityUtil.Argument(Strings.CheckArgumentContainsNullFailed(argumentName));
				}
			}
			return enumerableArgument;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000048B8 File Offset: 0x00002AB8
		internal static IEnumerable<T> CheckArgumentEmpty<T>(ref IEnumerable<T> enumerableArgument, Func<string, string> errorMessage, string argumentName)
		{
			int num;
			EntityUtil.GetCheapestSafeCountOfEnumerable<T>(ref enumerableArgument, out num);
			if (num <= 0)
			{
				throw EntityUtil.Argument(errorMessage(argumentName));
			}
			return enumerableArgument;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000048E0 File Offset: 0x00002AE0
		private static void GetCheapestSafeCountOfEnumerable<T>(ref IEnumerable<T> enumerable, out int count)
		{
			ICollection<T> cheapestSafeEnumerableAsCollection = EntityUtil.GetCheapestSafeEnumerableAsCollection<T>(ref enumerable);
			count = cheapestSafeEnumerableAsCollection.Count;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000048FC File Offset: 0x00002AFC
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

		// Token: 0x060001AE RID: 430 RVA: 0x00004926 File Offset: 0x00002B26
		internal static T GenericCheckArgumentNull<T>(T value, string parameterName) where T : class
		{
			return EntityUtil.CheckArgumentNull<T>(value, parameterName);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000492F File Offset: 0x00002B2F
		internal static ArgumentException KeywordNotSupported(string keyword)
		{
			return EntityUtil.Argument(Strings.EntityClient_KeywordNotSupported(keyword));
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000493C File Offset: 0x00002B3C
		internal static ArgumentException ADP_KeywordNotSupported(string keyword)
		{
			return EntityUtil.Argument(Strings.ADP_KeywordNotSupported(keyword));
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00004949 File Offset: 0x00002B49
		internal static ArgumentOutOfRangeException InvalidEnumerationValue(Type type, int value)
		{
			return EntityUtil.ArgumentOutOfRange(Strings.ADP_InvalidEnumerationValue(type.Name, value.ToString(CultureInfo.InvariantCulture)), type.Name);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00004970 File Offset: 0x00002B70
		internal static bool TryGetProviderInvariantName(DbProviderFactory providerFactory, out string invariantName)
		{
			Type type = providerFactory.GetType();
			AssemblyName assemblyName = new AssemblyName(type.Assembly.FullName);
			foreach (object obj in DbProviderFactories.GetFactoryClasses().Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string typeName = (string)dataRow[3];
				AssemblyName rowProviderFactoryAssemblyName = null;
				Type.GetType(typeName, delegate(AssemblyName a)
				{
					rowProviderFactoryAssemblyName = a;
					return null;
				}, (Assembly _, string __, bool ___) => null);
				if (rowProviderFactoryAssemblyName != null && string.Equals(assemblyName.Name, rowProviderFactoryAssemblyName.Name, StringComparison.OrdinalIgnoreCase))
				{
					try
					{
						DbProviderFactory factory = DbProviderFactories.GetFactory(dataRow);
						if (factory.GetType().Equals(type))
						{
							invariantName = (string)dataRow[2];
							return true;
						}
					}
					catch (Exception ex)
					{
					}
				}
			}
			invariantName = null;
			return false;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00004AA0 File Offset: 0x00002CA0
		internal static bool AssemblyNamesMatch(string infoRowProviderAssemblyName, AssemblyName targetAssemblyName)
		{
			if (string.IsNullOrWhiteSpace(infoRowProviderAssemblyName))
			{
				return false;
			}
			AssemblyName assemblyName = null;
			try
			{
				assemblyName = new AssemblyName(infoRowProviderAssemblyName);
			}
			catch (Exception e)
			{
				if (!EntityUtil.IsCatchableExceptionType(e))
				{
					throw;
				}
				return false;
			}
			if (!string.Equals(targetAssemblyName.Name, assemblyName.Name, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (targetAssemblyName.Version == null || assemblyName.Version == null)
			{
				return false;
			}
			if (targetAssemblyName.Version.Major != assemblyName.Version.Major || targetAssemblyName.Version.Minor != assemblyName.Version.Minor)
			{
				return false;
			}
			byte[] publicKeyToken = targetAssemblyName.GetPublicKeyToken();
			return publicKeyToken != null && publicKeyToken.SequenceEqual(assemblyName.GetPublicKeyToken());
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00004B64 File Offset: 0x00002D64
		internal static void CheckStringArgument(string value, string parameterName)
		{
			EntityUtil.CheckArgumentNull<string>(value, parameterName);
			if (value.Length == 0)
			{
				throw EntityUtil.InvalidStringArgument(parameterName);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00004B80 File Offset: 0x00002D80
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != EntityUtil.StackOverflowType && type != EntityUtil.OutOfMemoryType && type != EntityUtil.ThreadAbortType && type != EntityUtil.NullReferenceType && type != EntityUtil.AccessViolationType && !EntityUtil.SecurityType.IsAssignableFrom(type);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00004BE8 File Offset: 0x00002DE8
		internal static bool IsCatchableEntityExceptionType(Exception e)
		{
			Type type = e.GetType();
			return EntityUtil.IsCatchableExceptionType(e) && type != EntityUtil.CommandExecutionType && type != EntityUtil.CommandCompilationType && type != EntityUtil.QueryType;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00004C2C File Offset: 0x00002E2C
		internal static bool IsNull(object value)
		{
			if (value == null || DBNull.Value == value)
			{
				return true;
			}
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00004C58 File Offset: 0x00002E58
		internal static void BoolExprAssert(bool condition, string message)
		{
			if (!condition)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.BoolExprAssert, 0, message);
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00004C6A File Offset: 0x00002E6A
		internal static PropertyInfo GetTopProperty(Type t, string propertyName)
		{
			return EntityUtil.GetTopProperty(ref t, propertyName);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00004C74 File Offset: 0x00002E74
		internal static PropertyInfo GetTopProperty(ref Type t, string propertyName)
		{
			PropertyInfo propertyInfo = null;
			while (propertyInfo == null && t != null)
			{
				propertyInfo = t.GetProperty(propertyName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				t = t.BaseType;
			}
			t = propertyInfo.DeclaringType;
			return propertyInfo;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00004CB5 File Offset: 0x00002EB5
		internal static int SrcCompare(string strA, string strB)
		{
			if (!(strA == strB))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00004CC3 File Offset: 0x00002EC3
		internal static int DstCompare(string strA, string strB)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00004CD8 File Offset: 0x00002ED8
		[SecuritySafeCritical]
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		internal static string GetFullPath(string filename)
		{
			return Path.GetFullPath(filename);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00004CE0 File Offset: 0x00002EE0
		public static Type[] GetTypesSpecial(Assembly assembly)
		{
			if (assembly != typeof(ObjectContext).Assembly)
			{
				return assembly.GetTypes();
			}
			return new Type[0];
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00004D04 File Offset: 0x00002F04
		public static bool UseFx40CompatMode
		{
			get
			{
				if (EntityUtil.useFx40CompatMode == null)
				{
					string text = AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName;
					if (string.IsNullOrWhiteSpace(text))
					{
						Assembly entryAssembly = Assembly.GetEntryAssembly();
						if (entryAssembly != null)
						{
							TargetFrameworkAttribute customAttribute = entryAssembly.GetCustomAttribute<TargetFrameworkAttribute>();
							if (customAttribute != null)
							{
								text = customAttribute.FrameworkName;
							}
						}
					}
					if (!string.IsNullOrWhiteSpace(text))
					{
						try
						{
							FrameworkName frameworkName = new FrameworkName(text);
							Version v = new Version(4, 5);
							EntityUtil.useFx40CompatMode = new bool?(frameworkName.Version < v);
						}
						catch (ArgumentException)
						{
						}
					}
					if (EntityUtil.useFx40CompatMode == null)
					{
						EntityUtil.useFx40CompatMode = new bool?(true);
					}
				}
				return EntityUtil.useFx40CompatMode.Value;
			}
		}

		// Token: 0x04000085 RID: 133
		internal const int AssemblyQualifiedNameIndex = 3;

		// Token: 0x04000086 RID: 134
		internal const int InvariantNameIndex = 2;

		// Token: 0x04000087 RID: 135
		internal const string Parameter = "Parameter";

		// Token: 0x04000088 RID: 136
		internal const CompareOptions StringCompareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x04000089 RID: 137
		private static readonly Type StackOverflowType = typeof(StackOverflowException);

		// Token: 0x0400008A RID: 138
		private static readonly Type OutOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x0400008B RID: 139
		private static readonly Type ThreadAbortType = typeof(ThreadAbortException);

		// Token: 0x0400008C RID: 140
		private static readonly Type NullReferenceType = typeof(NullReferenceException);

		// Token: 0x0400008D RID: 141
		private static readonly Type AccessViolationType = typeof(AccessViolationException);

		// Token: 0x0400008E RID: 142
		private static readonly Type SecurityType = typeof(SecurityException);

		// Token: 0x0400008F RID: 143
		private static readonly Type CommandExecutionType = typeof(EntityCommandExecutionException);

		// Token: 0x04000090 RID: 144
		private static readonly Type CommandCompilationType = typeof(EntityCommandCompilationException);

		// Token: 0x04000091 RID: 145
		private static readonly Type QueryType = typeof(EntitySqlException);

		// Token: 0x04000092 RID: 146
		internal static Dictionary<string, string> COMPILER_VERSION = new Dictionary<string, string>
		{
			{
				"CompilerVersion",
				"V3.5"
			}
		};

		// Token: 0x04000093 RID: 147
		private static bool? useFx40CompatMode;

		// Token: 0x0200043E RID: 1086
		internal enum InternalErrorCode
		{
			// Token: 0x0400189B RID: 6299
			WrongNumberOfKeys = 1000,
			// Token: 0x0400189C RID: 6300
			UnknownColumnMapKind,
			// Token: 0x0400189D RID: 6301
			NestOverNest,
			// Token: 0x0400189E RID: 6302
			ColumnCountMismatch,
			// Token: 0x0400189F RID: 6303
			AssertionFailed,
			// Token: 0x040018A0 RID: 6304
			UnknownVar,
			// Token: 0x040018A1 RID: 6305
			WrongVarType,
			// Token: 0x040018A2 RID: 6306
			ExtentWithoutEntity,
			// Token: 0x040018A3 RID: 6307
			UnnestWithoutInput,
			// Token: 0x040018A4 RID: 6308
			UnnestMultipleCollections,
			// Token: 0x040018A5 RID: 6309
			CodeGen_NoSuchProperty = 1011,
			// Token: 0x040018A6 RID: 6310
			JoinOverSingleStreamNest,
			// Token: 0x040018A7 RID: 6311
			InvalidInternalTree,
			// Token: 0x040018A8 RID: 6312
			NameValuePairNext,
			// Token: 0x040018A9 RID: 6313
			InvalidParserState1,
			// Token: 0x040018AA RID: 6314
			InvalidParserState2,
			// Token: 0x040018AB RID: 6315
			SqlGenParametersNotPermitted,
			// Token: 0x040018AC RID: 6316
			EntityKeyMissingKeyValue,
			// Token: 0x040018AD RID: 6317
			UpdatePipelineResultRequestInvalid,
			// Token: 0x040018AE RID: 6318
			InvalidStateEntry,
			// Token: 0x040018AF RID: 6319
			InvalidPrimitiveTypeKind,
			// Token: 0x040018B0 RID: 6320
			UnknownLinqNodeType = 1023,
			// Token: 0x040018B1 RID: 6321
			CollectionWithNoColumns,
			// Token: 0x040018B2 RID: 6322
			UnexpectedLinqLambdaExpressionFormat,
			// Token: 0x040018B3 RID: 6323
			CommandTreeOnStoredProcedureEntityCommand,
			// Token: 0x040018B4 RID: 6324
			BoolExprAssert,
			// Token: 0x040018B5 RID: 6325
			FailedToGeneratePromotionRank = 1029
		}
	}
}
