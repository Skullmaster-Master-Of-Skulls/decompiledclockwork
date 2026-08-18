using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;

namespace System.Data.Common
{
	// Token: 0x020002D1 RID: 721
	internal static class ADP
	{
		// Token: 0x06002B5F RID: 11103 RVA: 0x0011D990 File Offset: 0x0011CD90
		internal static Task<T> CreatedTaskWithException<T>(Exception ex)
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			taskCompletionSource.SetException(ex);
			return taskCompletionSource.Task;
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x0011D9B0 File Offset: 0x0011CDB0
		internal static Task<T> CreatedTaskWithCancellation<T>()
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			taskCompletionSource.SetCanceled();
			return taskCompletionSource.Task;
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x0011D9D0 File Offset: 0x0011CDD0
		internal static Exception ExceptionWithStackTrace(Exception e)
		{
			Exception result;
			try
			{
				throw e;
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x0011DA04 File Offset: 0x0011CE04
		internal static Task<bool> TrueTask
		{
			get
			{
				if (ADP._trueTask == null)
				{
					ADP._trueTask = Task.FromResult<bool>(true);
				}
				return ADP._trueTask;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x0011DA28 File Offset: 0x0011CE28
		internal static Task<bool> FalseTask
		{
			get
			{
				if (ADP._falseTask == null)
				{
					ADP._falseTask = Task.FromResult<bool>(false);
				}
				return ADP._falseTask;
			}
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x0011DA4C File Offset: 0x0011CE4C
		private static void TraceException(string trace, Exception e)
		{
			if (e != null)
			{
				Bid.Trace(trace, e.ToString());
			}
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x0011DA68 File Offset: 0x0011CE68
		internal static void TraceExceptionAsReturnValue(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|THROW> '%ls'\n", e);
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x0011DA80 File Offset: 0x0011CE80
		internal static void TraceExceptionForCapture(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|CATCH> '%ls'\n", e);
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x0011DA98 File Offset: 0x0011CE98
		internal static void TraceExceptionWithoutRethrow(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|CATCH> '%ls'\n", e);
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x0011DAB0 File Offset: 0x0011CEB0
		internal static ArgumentException Argument(string error)
		{
			ArgumentException ex = new ArgumentException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x0011DACC File Offset: 0x0011CECC
		internal static ArgumentException Argument(string error, Exception inner)
		{
			ArgumentException ex = new ArgumentException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x0011DAE8 File Offset: 0x0011CEE8
		internal static ArgumentException Argument(string error, string parameter)
		{
			ArgumentException ex = new ArgumentException(error, parameter);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x0011DB04 File Offset: 0x0011CF04
		internal static ArgumentException Argument(string error, string parameter, Exception inner)
		{
			ArgumentException ex = new ArgumentException(error, parameter, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x0011DB24 File Offset: 0x0011CF24
		internal static ArgumentNullException ArgumentNull(string parameter)
		{
			ArgumentNullException ex = new ArgumentNullException(parameter);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x0011DB40 File Offset: 0x0011CF40
		internal static ArgumentNullException ArgumentNull(string parameter, string error)
		{
			ArgumentNullException ex = new ArgumentNullException(parameter, error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x0011DB5C File Offset: 0x0011CF5C
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x0011DB78 File Offset: 0x0011CF78
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName, message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x0011DB94 File Offset: 0x0011CF94
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName, object value)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName, value, message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x0011DBB4 File Offset: 0x0011CFB4
		internal static ConfigurationException Configuration(string message)
		{
			ConfigurationException ex = new ConfigurationErrorsException(message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x0011DBD0 File Offset: 0x0011CFD0
		internal static ConfigurationException Configuration(string message, XmlNode node)
		{
			ConfigurationException ex = new ConfigurationErrorsException(message, node);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x0011DBEC File Offset: 0x0011CFEC
		internal static DataException Data(string message)
		{
			DataException ex = new DataException(message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0011DC08 File Offset: 0x0011D008
		internal static IndexOutOfRangeException IndexOutOfRange(int value)
		{
			IndexOutOfRangeException ex = new IndexOutOfRangeException(value.ToString(CultureInfo.InvariantCulture));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x0011DC30 File Offset: 0x0011D030
		internal static IndexOutOfRangeException IndexOutOfRange(string error)
		{
			IndexOutOfRangeException ex = new IndexOutOfRangeException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x0011DC4C File Offset: 0x0011D04C
		internal static IndexOutOfRangeException IndexOutOfRange()
		{
			IndexOutOfRangeException ex = new IndexOutOfRangeException();
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x0011DC68 File Offset: 0x0011D068
		internal static InvalidCastException InvalidCast(string error)
		{
			return ADP.InvalidCast(error, null);
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x0011DC7C File Offset: 0x0011D07C
		internal static InvalidCastException InvalidCast(string error, Exception inner)
		{
			InvalidCastException ex = new InvalidCastException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x0011DC98 File Offset: 0x0011D098
		internal static InvalidOperationException InvalidOperation(string error)
		{
			InvalidOperationException ex = new InvalidOperationException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x0011DCB4 File Offset: 0x0011D0B4
		internal static TimeoutException TimeoutException(string error)
		{
			TimeoutException ex = new TimeoutException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x0011DCD0 File Offset: 0x0011D0D0
		internal static InvalidOperationException InvalidOperation(string error, Exception inner)
		{
			InvalidOperationException ex = new InvalidOperationException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x0011DCEC File Offset: 0x0011D0EC
		internal static NotImplementedException NotImplemented(string error)
		{
			NotImplementedException ex = new NotImplementedException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x0011DD08 File Offset: 0x0011D108
		internal static NotSupportedException NotSupported()
		{
			NotSupportedException ex = new NotSupportedException();
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x0011DD24 File Offset: 0x0011D124
		internal static NotSupportedException NotSupported(string error)
		{
			NotSupportedException ex = new NotSupportedException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x0011DD40 File Offset: 0x0011D140
		internal static OverflowException Overflow(string error)
		{
			return ADP.Overflow(error, null);
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x0011DD54 File Offset: 0x0011D154
		internal static OverflowException Overflow(string error, Exception inner)
		{
			OverflowException ex = new OverflowException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x0011DD70 File Offset: 0x0011D170
		internal static PlatformNotSupportedException PropertyNotSupported(string property)
		{
			PlatformNotSupportedException ex = new PlatformNotSupportedException(Res.GetString("ADP_PropertyNotSupported", new object[]
			{
				property
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x0011DDA0 File Offset: 0x0011D1A0
		internal static TypeLoadException TypeLoad(string error)
		{
			TypeLoadException ex = new TypeLoadException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x0011DDBC File Offset: 0x0011D1BC
		internal static InvalidCastException InvalidCast()
		{
			InvalidCastException ex = new InvalidCastException();
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x0011DDD8 File Offset: 0x0011D1D8
		internal static IOException IO(string error)
		{
			IOException ex = new IOException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x0011DDF4 File Offset: 0x0011D1F4
		internal static IOException IO(string error, Exception inner)
		{
			IOException ex = new IOException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x0011DE10 File Offset: 0x0011D210
		internal static InvalidOperationException DataAdapter(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x0011DE24 File Offset: 0x0011D224
		internal static InvalidOperationException DataAdapter(string error, Exception inner)
		{
			return ADP.InvalidOperation(error, inner);
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x0011DE38 File Offset: 0x0011D238
		private static InvalidOperationException Provider(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x0011DE4C File Offset: 0x0011D24C
		internal static ObjectDisposedException ObjectDisposed(object instance)
		{
			ObjectDisposedException ex = new ObjectDisposedException(instance.GetType().Name);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x0011DE74 File Offset: 0x0011D274
		internal static InvalidOperationException MethodCalledTwice(string method)
		{
			InvalidOperationException ex = new InvalidOperationException(Res.GetString("ADP_CalledTwice", new object[]
			{
				method
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x0011DEA4 File Offset: 0x0011D2A4
		internal static ArgumentException IncorrectAsyncResult()
		{
			ArgumentException ex = new ArgumentException(Res.GetString("ADP_IncorrectAsyncResult"), "AsyncResult");
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x0011DED0 File Offset: 0x0011D2D0
		internal static ArgumentException SingleValuedProperty(string propertyName, string value)
		{
			ArgumentException ex = new ArgumentException(Res.GetString("ADP_SingleValuedProperty", new object[]
			{
				propertyName,
				value
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x0011DF04 File Offset: 0x0011D304
		internal static ArgumentException DoubleValuedProperty(string propertyName, string value1, string value2)
		{
			ArgumentException ex = new ArgumentException(Res.GetString("ADP_DoubleValuedProperty", new object[]
			{
				propertyName,
				value1,
				value2
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x0011DF3C File Offset: 0x0011D33C
		internal static ArgumentException InvalidPrefixSuffix()
		{
			ArgumentException ex = new ArgumentException(Res.GetString("ADP_InvalidPrefixSuffix"));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x0011DF60 File Offset: 0x0011D360
		internal static ArgumentException InvalidMultipartName(string property, string value)
		{
			ArgumentException ex = new ArgumentException(Res.GetString("ADP_InvalidMultipartName", new object[]
			{
				Res.GetString(property),
				value
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x0011DF98 File Offset: 0x0011D398
		internal static ArgumentException InvalidMultipartNameIncorrectUsageOfQuotes(string property, string value)
		{
			ArgumentException ex = new ArgumentException(Res.GetString("ADP_InvalidMultipartNameQuoteUsage", new object[]
			{
				Res.GetString(property),
				value
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x0011DFD0 File Offset: 0x0011D3D0
		internal static ArgumentException InvalidMultipartNameToManyParts(string property, string value, int limit)
		{
			ArgumentException ex = new ArgumentException(Res.GetString("ADP_InvalidMultipartNameToManyParts", new object[]
			{
				Res.GetString(property),
				value,
				limit
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x0011E010 File Offset: 0x0011D410
		internal static ArgumentException BadParameterName(string parameterName)
		{
			ArgumentException ex = new ArgumentException(Res.GetString("ADP_BadParameterName", new object[]
			{
				parameterName
			}));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x0011E040 File Offset: 0x0011D440
		internal static ArgumentException MultipleReturnValue()
		{
			ArgumentException ex = new ArgumentException(Res.GetString("ADP_MultipleReturnValue"));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x0011E064 File Offset: 0x0011D464
		internal static void CheckArgumentLength(string value, string parameterName)
		{
			ADP.CheckArgumentNull(value, parameterName);
			if (value.Length == 0)
			{
				throw ADP.Argument(Res.GetString("ADP_EmptyString", new object[]
				{
					parameterName
				}));
			}
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x0011E09C File Offset: 0x0011D49C
		internal static void CheckArgumentLength(Array value, string parameterName)
		{
			ADP.CheckArgumentNull(value, parameterName);
			if (value.Length == 0)
			{
				throw ADP.Argument(Res.GetString("ADP_EmptyArray", new object[]
				{
					parameterName
				}));
			}
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x0011E0D4 File Offset: 0x0011D4D4
		internal static void CheckArgumentNull(object value, string parameterName)
		{
			if (value == null)
			{
				throw ADP.ArgumentNull(parameterName);
			}
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x0011E0EC File Offset: 0x0011D4EC
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ADP.StackOverflowType && type != ADP.OutOfMemoryType && type != ADP.ThreadAbortType && type != ADP.NullReferenceType && type != ADP.AccessViolationType && !ADP.SecurityType.IsAssignableFrom(type);
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x0011E154 File Offset: 0x0011D554
		internal static bool IsCatchableOrSecurityExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ADP.StackOverflowType && type != ADP.OutOfMemoryType && type != ADP.ThreadAbortType && type != ADP.NullReferenceType && type != ADP.AccessViolationType;
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x0011E1AC File Offset: 0x0011D5AC
		internal static ArgumentOutOfRangeException InvalidEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(Res.GetString("ADP_InvalidEnumerationValue", new object[]
			{
				type.Name,
				value.ToString(CultureInfo.InvariantCulture)
			}), type.Name);
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x0011E1EC File Offset: 0x0011D5EC
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, string value, string method)
		{
			return ADP.ArgumentOutOfRange(Res.GetString("ADP_NotSupportedEnumerationValue", new object[]
			{
				type.Name,
				value,
				method
			}), type.Name);
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x0011E228 File Offset: 0x0011D628
		internal static ArgumentOutOfRangeException InvalidAcceptRejectRule(AcceptRejectRule value)
		{
			return ADP.InvalidEnumerationValue(typeof(AcceptRejectRule), (int)value);
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x0011E248 File Offset: 0x0011D648
		internal static ArgumentOutOfRangeException InvalidCatalogLocation(CatalogLocation value)
		{
			return ADP.InvalidEnumerationValue(typeof(CatalogLocation), (int)value);
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x0011E268 File Offset: 0x0011D668
		internal static ArgumentOutOfRangeException InvalidCommandBehavior(CommandBehavior value)
		{
			return ADP.InvalidEnumerationValue(typeof(CommandBehavior), (int)value);
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x0011E288 File Offset: 0x0011D688
		internal static void ValidateCommandBehavior(CommandBehavior value)
		{
			if (value < CommandBehavior.Default || (CommandBehavior.SingleResult | CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo | CommandBehavior.SingleRow | CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection) < value)
			{
				throw ADP.InvalidCommandBehavior(value);
			}
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x0011E2A8 File Offset: 0x0011D6A8
		internal static ArgumentException InvalidArgumentLength(string argumentName, int limit)
		{
			return ADP.Argument(Res.GetString("ADP_InvalidArgumentLength", new object[]
			{
				argumentName,
				limit
			}));
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x0011E2D8 File Offset: 0x0011D6D8
		internal static ArgumentException MustBeReadOnly(string argumentName)
		{
			return ADP.Argument(Res.GetString("ADP_MustBeReadOnly", new object[]
			{
				argumentName
			}));
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x0011E300 File Offset: 0x0011D700
		internal static ArgumentOutOfRangeException InvalidCommandType(CommandType value)
		{
			return ADP.InvalidEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x0011E320 File Offset: 0x0011D720
		internal static ArgumentOutOfRangeException InvalidConflictOptions(ConflictOption value)
		{
			return ADP.InvalidEnumerationValue(typeof(ConflictOption), (int)value);
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x0011E340 File Offset: 0x0011D740
		internal static ArgumentOutOfRangeException InvalidDataRowState(DataRowState value)
		{
			return ADP.InvalidEnumerationValue(typeof(DataRowState), (int)value);
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x0011E360 File Offset: 0x0011D760
		internal static ArgumentOutOfRangeException InvalidDataRowVersion(DataRowVersion value)
		{
			return ADP.InvalidEnumerationValue(typeof(DataRowVersion), (int)value);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x0011E380 File Offset: 0x0011D780
		internal static ArgumentOutOfRangeException InvalidIsolationLevel(IsolationLevel value)
		{
			return ADP.InvalidEnumerationValue(typeof(IsolationLevel), (int)value);
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x0011E3A0 File Offset: 0x0011D7A0
		internal static ArgumentOutOfRangeException InvalidKeyRestrictionBehavior(KeyRestrictionBehavior value)
		{
			return ADP.InvalidEnumerationValue(typeof(KeyRestrictionBehavior), (int)value);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x0011E3C0 File Offset: 0x0011D7C0
		internal static ArgumentOutOfRangeException InvalidLoadOption(LoadOption value)
		{
			return ADP.InvalidEnumerationValue(typeof(LoadOption), (int)value);
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x0011E3E0 File Offset: 0x0011D7E0
		internal static ArgumentOutOfRangeException InvalidMissingMappingAction(MissingMappingAction value)
		{
			return ADP.InvalidEnumerationValue(typeof(MissingMappingAction), (int)value);
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x0011E400 File Offset: 0x0011D800
		internal static ArgumentOutOfRangeException InvalidMissingSchemaAction(MissingSchemaAction value)
		{
			return ADP.InvalidEnumerationValue(typeof(MissingSchemaAction), (int)value);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x0011E420 File Offset: 0x0011D820
		internal static ArgumentOutOfRangeException InvalidParameterDirection(ParameterDirection value)
		{
			return ADP.InvalidEnumerationValue(typeof(ParameterDirection), (int)value);
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x0011E440 File Offset: 0x0011D840
		internal static ArgumentOutOfRangeException InvalidPermissionState(PermissionState value)
		{
			return ADP.InvalidEnumerationValue(typeof(PermissionState), (int)value);
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x0011E460 File Offset: 0x0011D860
		internal static ArgumentOutOfRangeException InvalidRule(Rule value)
		{
			return ADP.InvalidEnumerationValue(typeof(Rule), (int)value);
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x0011E480 File Offset: 0x0011D880
		internal static ArgumentOutOfRangeException InvalidSchemaType(SchemaType value)
		{
			return ADP.InvalidEnumerationValue(typeof(SchemaType), (int)value);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x0011E4A0 File Offset: 0x0011D8A0
		internal static ArgumentOutOfRangeException InvalidStatementType(StatementType value)
		{
			return ADP.InvalidEnumerationValue(typeof(StatementType), (int)value);
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x0011E4C0 File Offset: 0x0011D8C0
		internal static ArgumentOutOfRangeException InvalidUpdateRowSource(UpdateRowSource value)
		{
			return ADP.InvalidEnumerationValue(typeof(UpdateRowSource), (int)value);
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x0011E4E0 File Offset: 0x0011D8E0
		internal static ArgumentOutOfRangeException InvalidUpdateStatus(UpdateStatus value)
		{
			return ADP.InvalidEnumerationValue(typeof(UpdateStatus), (int)value);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x0011E500 File Offset: 0x0011D900
		internal static ArgumentOutOfRangeException NotSupportedCommandBehavior(CommandBehavior value, string method)
		{
			return ADP.NotSupportedEnumerationValue(typeof(CommandBehavior), value.ToString(), method);
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x0011E52C File Offset: 0x0011D92C
		internal static ArgumentOutOfRangeException NotSupportedStatementType(StatementType value, string method)
		{
			return ADP.NotSupportedEnumerationValue(typeof(StatementType), value.ToString(), method);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x0011E558 File Offset: 0x0011D958
		internal static ArgumentOutOfRangeException InvalidUserDefinedTypeSerializationFormat(Format value)
		{
			return ADP.InvalidEnumerationValue(typeof(Format), (int)value);
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x0011E578 File Offset: 0x0011D978
		internal static ArgumentOutOfRangeException NotSupportedUserDefinedTypeSerializationFormat(Format value, string method)
		{
			return ADP.NotSupportedEnumerationValue(typeof(Format), value.ToString(), method);
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x0011E5A4 File Offset: 0x0011D9A4
		internal static ArgumentException ConfigProviderNotFound()
		{
			return ADP.Argument(Res.GetString("ConfigProviderNotFound"));
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x0011E5C0 File Offset: 0x0011D9C0
		internal static InvalidOperationException ConfigProviderInvalid()
		{
			return ADP.InvalidOperation(Res.GetString("ConfigProviderInvalid"));
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x0011E5DC File Offset: 0x0011D9DC
		internal static ConfigurationException ConfigProviderNotInstalled()
		{
			return ADP.Configuration(Res.GetString("ConfigProviderNotInstalled"));
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x0011E5F8 File Offset: 0x0011D9F8
		internal static ConfigurationException ConfigProviderMissing()
		{
			return ADP.Configuration(Res.GetString("ConfigProviderMissing"));
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x0011E614 File Offset: 0x0011DA14
		internal static ConfigurationException ConfigBaseNoChildNodes(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("ConfigBaseNoChildNodes"), node);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x0011E634 File Offset: 0x0011DA34
		internal static ConfigurationException ConfigBaseElementsOnly(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("ConfigBaseElementsOnly"), node);
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x0011E654 File Offset: 0x0011DA54
		internal static ConfigurationException ConfigUnrecognizedAttributes(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("ConfigUnrecognizedAttributes", new object[]
			{
				node.Attributes[0].Name
			}), node);
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x0011E68C File Offset: 0x0011DA8C
		internal static ConfigurationException ConfigUnrecognizedElement(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("ConfigUnrecognizedElement"), node);
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x0011E6AC File Offset: 0x0011DAAC
		internal static ConfigurationException ConfigSectionsUnique(string sectionName)
		{
			return ADP.Configuration(Res.GetString("ConfigSectionsUnique", new object[]
			{
				sectionName
			}));
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x0011E6D4 File Offset: 0x0011DAD4
		internal static ConfigurationException ConfigRequiredAttributeMissing(string name, XmlNode node)
		{
			return ADP.Configuration(Res.GetString("ConfigRequiredAttributeMissing", new object[]
			{
				name
			}), node);
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x0011E6FC File Offset: 0x0011DAFC
		internal static ConfigurationException ConfigRequiredAttributeEmpty(string name, XmlNode node)
		{
			return ADP.Configuration(Res.GetString("ConfigRequiredAttributeEmpty", new object[]
			{
				name
			}), node);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x0011E724 File Offset: 0x0011DB24
		internal static ArgumentException ConnectionStringSyntax(int index)
		{
			return ADP.Argument(Res.GetString("ADP_ConnectionStringSyntax", new object[]
			{
				index
			}));
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x0011E750 File Offset: 0x0011DB50
		internal static ArgumentException KeywordNotSupported(string keyword)
		{
			return ADP.Argument(Res.GetString("ADP_KeywordNotSupported", new object[]
			{
				keyword
			}));
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x0011E778 File Offset: 0x0011DB78
		internal static ArgumentException UdlFileError(Exception inner)
		{
			return ADP.Argument(Res.GetString("ADP_UdlFileError"), inner);
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x0011E798 File Offset: 0x0011DB98
		internal static ArgumentException InvalidUDL()
		{
			return ADP.Argument(Res.GetString("ADP_InvalidUDL"));
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x0011E7B4 File Offset: 0x0011DBB4
		internal static InvalidOperationException InvalidDataDirectory()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InvalidDataDirectory"));
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x0011E7D0 File Offset: 0x0011DBD0
		internal static ArgumentException InvalidKeyname(string parameterName)
		{
			return ADP.Argument(Res.GetString("ADP_InvalidKey"), parameterName);
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x0011E7F0 File Offset: 0x0011DBF0
		internal static ArgumentException InvalidValue(string parameterName)
		{
			return ADP.Argument(Res.GetString("ADP_InvalidValue"), parameterName);
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x0011E810 File Offset: 0x0011DC10
		internal static ArgumentException InvalidMinMaxPoolSizeValues()
		{
			return ADP.Argument(Res.GetString("ADP_InvalidMinMaxPoolSizeValues"));
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x0011E82C File Offset: 0x0011DC2C
		internal static ArgumentException ConvertFailed(Type fromType, Type toType, Exception innerException)
		{
			return ADP.Argument(Res.GetString("SqlConvert_ConvertFailed", new object[]
			{
				fromType.FullName,
				toType.FullName
			}), innerException);
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x0011E864 File Offset: 0x0011DC64
		internal static InvalidOperationException InvalidMixedUsageOfSecureAndClearCredential()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InvalidMixedUsageOfSecureAndClearCredential"));
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x0011E880 File Offset: 0x0011DC80
		internal static ArgumentException InvalidMixedArgumentOfSecureAndClearCredential()
		{
			return ADP.Argument(Res.GetString("ADP_InvalidMixedUsageOfSecureAndClearCredential"));
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x0011E89C File Offset: 0x0011DC9C
		internal static InvalidOperationException InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity"));
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x0011E8B8 File Offset: 0x0011DCB8
		internal static ArgumentException InvalidMixedArgumentOfSecureCredentialAndIntegratedSecurity()
		{
			return ADP.Argument(Res.GetString("ADP_InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity"));
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x0011E8D4 File Offset: 0x0011DCD4
		internal static InvalidOperationException InvalidMixedUsageOfSecureCredentialAndContextConnection()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InvalidMixedUsageOfSecureCredentialAndContextConnection"));
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x0011E8F0 File Offset: 0x0011DCF0
		internal static ArgumentException InvalidMixedArgumentOfSecureCredentialAndContextConnection()
		{
			return ADP.Argument(Res.GetString("ADP_InvalidMixedUsageOfSecureCredentialAndContextConnection"));
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x0011E90C File Offset: 0x0011DD0C
		internal static InvalidOperationException InvalidMixedUsageOfAccessTokenAndContextConnection()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InvalidMixedUsageOfAccessTokenAndContextConnection"));
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x0011E928 File Offset: 0x0011DD28
		internal static InvalidOperationException InvalidMixedUsageOfAccessTokenAndIntegratedSecurity()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InvalidMixedUsageOfAccessTokenAndIntegratedSecurity"));
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x0011E944 File Offset: 0x0011DD44
		internal static InvalidOperationException InvalidMixedUsageOfAccessTokenAndUserIDPassword()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InvalidMixedUsageOfAccessTokenAndUserIDPassword"));
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x0011E960 File Offset: 0x0011DD60
		internal static Exception InvalidMixedUsageOfAccessTokenAndCredential()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InvalidMixedUsageOfAccessTokenAndCredential"));
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x0011E97C File Offset: 0x0011DD7C
		internal static Exception InvalidMixedUsageOfAccessTokenAndAuthentication()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InvalidMixedUsageOfAccessTokenAndAuthentication"));
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x0011E998 File Offset: 0x0011DD98
		internal static Exception InvalidMixedUsageOfCredentialAndAccessToken()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InvalidMixedUsageOfCredentialAndAccessToken"));
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x0011E9B4 File Offset: 0x0011DDB4
		internal static InvalidOperationException NoConnectionString()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_NoConnectionString"));
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x0011E9D0 File Offset: 0x0011DDD0
		internal static NotImplementedException MethodNotImplemented(string methodName)
		{
			NotImplementedException ex = new NotImplementedException(methodName);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x0011E9EC File Offset: 0x0011DDEC
		private static string ConnectionStateMsg(ConnectionState state)
		{
			switch (state)
			{
			case ConnectionState.Closed:
				break;
			case ConnectionState.Open:
				return Res.GetString("ADP_ConnectionStateMsg_Open");
			case ConnectionState.Connecting:
				return Res.GetString("ADP_ConnectionStateMsg_Connecting");
			case ConnectionState.Open | ConnectionState.Connecting:
			case ConnectionState.Executing:
				goto IL_5F;
			case ConnectionState.Open | ConnectionState.Executing:
				return Res.GetString("ADP_ConnectionStateMsg_OpenExecuting");
			default:
				if (state == (ConnectionState.Open | ConnectionState.Fetching))
				{
					return Res.GetString("ADP_ConnectionStateMsg_OpenFetching");
				}
				if (state != (ConnectionState.Connecting | ConnectionState.Broken))
				{
					goto IL_5F;
				}
				break;
			}
			return Res.GetString("ADP_ConnectionStateMsg_Closed");
			IL_5F:
			return Res.GetString("ADP_ConnectionStateMsg", new object[]
			{
				state.ToString()
			});
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x0011EA78 File Offset: 0x0011DE78
		internal static ConfigurationException ConfigUnableToLoadXmlMetaDataFile(string settingName)
		{
			return ADP.Configuration(Res.GetString("OleDb_ConfigUnableToLoadXmlMetaDataFile", new object[]
			{
				settingName
			}));
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x0011EAA0 File Offset: 0x0011DEA0
		internal static ConfigurationException ConfigWrongNumberOfValues(string settingName)
		{
			return ADP.Configuration(Res.GetString("OleDb_ConfigWrongNumberOfValues", new object[]
			{
				settingName
			}));
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x0011EAC8 File Offset: 0x0011DEC8
		internal static Exception InvalidConnectionOptionValue(string key)
		{
			return ADP.InvalidConnectionOptionValue(key, null);
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x0011EADC File Offset: 0x0011DEDC
		internal static Exception InvalidConnectionOptionValueLength(string key, int limit)
		{
			return ADP.Argument(Res.GetString("ADP_InvalidConnectionOptionValueLength", new object[]
			{
				key,
				limit
			}));
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x0011EB0C File Offset: 0x0011DF0C
		internal static Exception InvalidConnectionOptionValue(string key, Exception inner)
		{
			return ADP.Argument(Res.GetString("ADP_InvalidConnectionOptionValue", new object[]
			{
				key
			}), inner);
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x0011EB34 File Offset: 0x0011DF34
		internal static Exception MissingConnectionOptionValue(string key, string requiredAdditionalKey)
		{
			return ADP.Argument(Res.GetString("ADP_MissingConnectionOptionValue", new object[]
			{
				key,
				requiredAdditionalKey
			}));
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x0011EB60 File Offset: 0x0011DF60
		internal static Exception InvalidXMLBadVersion()
		{
			return ADP.Argument(Res.GetString("ADP_InvalidXMLBadVersion"));
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x0011EB7C File Offset: 0x0011DF7C
		internal static Exception NotAPermissionElement()
		{
			return ADP.Argument(Res.GetString("ADP_NotAPermissionElement"));
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x0011EB98 File Offset: 0x0011DF98
		internal static Exception PermissionTypeMismatch()
		{
			return ADP.Argument(Res.GetString("ADP_PermissionTypeMismatch"));
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x0011EBB4 File Offset: 0x0011DFB4
		internal static Exception WrongType(Type got, Type expected)
		{
			return ADP.Argument(Res.GetString("SQL_WrongType", new object[]
			{
				got.ToString(),
				expected.ToString()
			}));
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x0011EBE8 File Offset: 0x0011DFE8
		internal static Exception OdbcNoTypesFromProvider()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_OdbcNoTypesFromProvider"));
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x0011EC04 File Offset: 0x0011E004
		internal static Exception PooledOpenTimeout()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_PooledOpenTimeout"));
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x0011EC20 File Offset: 0x0011E020
		internal static Exception NonPooledOpenTimeout()
		{
			return ADP.TimeoutException(Res.GetString("ADP_NonPooledOpenTimeout"));
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x0011EC3C File Offset: 0x0011E03C
		internal static ArgumentException CollectionRemoveInvalidObject(Type itemType, ICollection collection)
		{
			return ADP.Argument(Res.GetString("ADP_CollectionRemoveInvalidObject", new object[]
			{
				itemType.Name,
				collection.GetType().Name
			}));
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x0011EC78 File Offset: 0x0011E078
		internal static ArgumentNullException CollectionNullValue(string parameter, Type collection, Type itemType)
		{
			return ADP.ArgumentNull(parameter, Res.GetString("ADP_CollectionNullValue", new object[]
			{
				collection.Name,
				itemType.Name
			}));
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x0011ECB0 File Offset: 0x0011E0B0
		internal static IndexOutOfRangeException CollectionIndexInt32(int index, Type collection, int count)
		{
			return ADP.IndexOutOfRange(Res.GetString("ADP_CollectionIndexInt32", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture),
				collection.Name,
				count.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x0011ECFC File Offset: 0x0011E0FC
		internal static IndexOutOfRangeException CollectionIndexString(Type itemType, string propertyName, string propertyValue, Type collection)
		{
			return ADP.IndexOutOfRange(Res.GetString("ADP_CollectionIndexString", new object[]
			{
				itemType.Name,
				propertyName,
				propertyValue,
				collection.Name
			}));
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x0011ED38 File Offset: 0x0011E138
		internal static InvalidCastException CollectionInvalidType(Type collection, Type itemType, object invalidValue)
		{
			return ADP.InvalidCast(Res.GetString("ADP_CollectionInvalidType", new object[]
			{
				collection.Name,
				itemType.Name,
				invalidValue.GetType().Name
			}));
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x0011ED7C File Offset: 0x0011E17C
		internal static Exception CollectionUniqueValue(Type itemType, string propertyName, string propertyValue)
		{
			return ADP.Argument(Res.GetString("ADP_CollectionUniqueValue", new object[]
			{
				itemType.Name,
				propertyName,
				propertyValue
			}));
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x0011EDB0 File Offset: 0x0011E1B0
		internal static ArgumentException ParametersIsNotParent(Type parameterType, ICollection collection)
		{
			return ADP.Argument(Res.GetString("ADP_CollectionIsNotParent", new object[]
			{
				parameterType.Name,
				collection.GetType().Name
			}));
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x0011EDEC File Offset: 0x0011E1EC
		internal static ArgumentException ParametersIsParent(Type parameterType, ICollection collection)
		{
			return ADP.Argument(Res.GetString("ADP_CollectionIsNotParent", new object[]
			{
				parameterType.Name,
				collection.GetType().Name
			}));
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x0011EE28 File Offset: 0x0011E228
		internal static InvalidOperationException TransactionConnectionMismatch()
		{
			return ADP.Provider(Res.GetString("ADP_TransactionConnectionMismatch"));
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x0011EE44 File Offset: 0x0011E244
		internal static InvalidOperationException TransactionCompletedButNotDisposed()
		{
			return ADP.Provider(Res.GetString("ADP_TransactionCompletedButNotDisposed"));
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x0011EE60 File Offset: 0x0011E260
		internal static InvalidOperationException TransactionRequired(string method)
		{
			return ADP.Provider(Res.GetString("ADP_TransactionRequired", new object[]
			{
				method
			}));
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x0011EE88 File Offset: 0x0011E288
		internal static InvalidOperationException MissingSelectCommand(string method)
		{
			return ADP.Provider(Res.GetString("ADP_MissingSelectCommand", new object[]
			{
				method
			}));
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x0011EEB0 File Offset: 0x0011E2B0
		private static InvalidOperationException DataMapping(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x0011EEC4 File Offset: 0x0011E2C4
		internal static InvalidOperationException ColumnSchemaExpression(string srcColumn, string cacheColumn)
		{
			return ADP.DataMapping(Res.GetString("ADP_ColumnSchemaExpression", new object[]
			{
				srcColumn,
				cacheColumn
			}));
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x0011EEF0 File Offset: 0x0011E2F0
		internal static InvalidOperationException ColumnSchemaMismatch(string srcColumn, Type srcType, DataColumn column)
		{
			return ADP.DataMapping(Res.GetString("ADP_ColumnSchemaMismatch", new object[]
			{
				srcColumn,
				srcType.Name,
				column.ColumnName,
				column.DataType.Name
			}));
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x0011EF38 File Offset: 0x0011E338
		internal static InvalidOperationException ColumnSchemaMissing(string cacheColumn, string tableName, string srcColumn)
		{
			if (ADP.IsEmpty(tableName))
			{
				return ADP.InvalidOperation(Res.GetString("ADP_ColumnSchemaMissing1", new object[]
				{
					cacheColumn,
					tableName,
					srcColumn
				}));
			}
			return ADP.DataMapping(Res.GetString("ADP_ColumnSchemaMissing2", new object[]
			{
				cacheColumn,
				tableName,
				srcColumn
			}));
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x0011EF90 File Offset: 0x0011E390
		internal static InvalidOperationException MissingColumnMapping(string srcColumn)
		{
			return ADP.DataMapping(Res.GetString("ADP_MissingColumnMapping", new object[]
			{
				srcColumn
			}));
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x0011EFB8 File Offset: 0x0011E3B8
		internal static InvalidOperationException MissingTableSchema(string cacheTable, string srcTable)
		{
			return ADP.DataMapping(Res.GetString("ADP_MissingTableSchema", new object[]
			{
				cacheTable,
				srcTable
			}));
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x0011EFE4 File Offset: 0x0011E3E4
		internal static InvalidOperationException MissingTableMapping(string srcTable)
		{
			return ADP.DataMapping(Res.GetString("ADP_MissingTableMapping", new object[]
			{
				srcTable
			}));
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x0011F00C File Offset: 0x0011E40C
		internal static InvalidOperationException MissingTableMappingDestination(string dstTable)
		{
			return ADP.DataMapping(Res.GetString("ADP_MissingTableMappingDestination", new object[]
			{
				dstTable
			}));
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x0011F034 File Offset: 0x0011E434
		internal static Exception InvalidSourceColumn(string parameter)
		{
			return ADP.Argument(Res.GetString("ADP_InvalidSourceColumn"), parameter);
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x0011F054 File Offset: 0x0011E454
		internal static Exception ColumnsAddNullAttempt(string parameter)
		{
			return ADP.CollectionNullValue(parameter, typeof(DataColumnMappingCollection), typeof(DataColumnMapping));
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x0011F07C File Offset: 0x0011E47C
		internal static Exception ColumnsDataSetColumn(string cacheColumn)
		{
			return ADP.CollectionIndexString(typeof(DataColumnMapping), "DataSetColumn", cacheColumn, typeof(DataColumnMappingCollection));
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x0011F0A8 File Offset: 0x0011E4A8
		internal static Exception ColumnsIndexInt32(int index, IColumnMappingCollection collection)
		{
			return ADP.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x0011F0C8 File Offset: 0x0011E4C8
		internal static Exception ColumnsIndexSource(string srcColumn)
		{
			return ADP.CollectionIndexString(typeof(DataColumnMapping), "SourceColumn", srcColumn, typeof(DataColumnMappingCollection));
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x0011F0F4 File Offset: 0x0011E4F4
		internal static Exception ColumnsIsNotParent(ICollection collection)
		{
			return ADP.ParametersIsNotParent(typeof(DataColumnMapping), collection);
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x0011F114 File Offset: 0x0011E514
		internal static Exception ColumnsIsParent(ICollection collection)
		{
			return ADP.ParametersIsParent(typeof(DataColumnMapping), collection);
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x0011F134 File Offset: 0x0011E534
		internal static Exception ColumnsUniqueSourceColumn(string srcColumn)
		{
			return ADP.CollectionUniqueValue(typeof(DataColumnMapping), "SourceColumn", srcColumn);
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x0011F158 File Offset: 0x0011E558
		internal static Exception NotADataColumnMapping(object value)
		{
			return ADP.CollectionInvalidType(typeof(DataColumnMappingCollection), typeof(DataColumnMapping), value);
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x0011F180 File Offset: 0x0011E580
		internal static Exception InvalidSourceTable(string parameter)
		{
			return ADP.Argument(Res.GetString("ADP_InvalidSourceTable"), parameter);
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x0011F1A0 File Offset: 0x0011E5A0
		internal static Exception TablesAddNullAttempt(string parameter)
		{
			return ADP.CollectionNullValue(parameter, typeof(DataTableMappingCollection), typeof(DataTableMapping));
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x0011F1C8 File Offset: 0x0011E5C8
		internal static Exception TablesDataSetTable(string cacheTable)
		{
			return ADP.CollectionIndexString(typeof(DataTableMapping), "DataSetTable", cacheTable, typeof(DataTableMappingCollection));
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x0011F1F4 File Offset: 0x0011E5F4
		internal static Exception TablesIndexInt32(int index, ITableMappingCollection collection)
		{
			return ADP.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x0011F214 File Offset: 0x0011E614
		internal static Exception TablesIsNotParent(ICollection collection)
		{
			return ADP.ParametersIsNotParent(typeof(DataTableMapping), collection);
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x0011F234 File Offset: 0x0011E634
		internal static Exception TablesIsParent(ICollection collection)
		{
			return ADP.ParametersIsParent(typeof(DataTableMapping), collection);
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x0011F254 File Offset: 0x0011E654
		internal static Exception TablesSourceIndex(string srcTable)
		{
			return ADP.CollectionIndexString(typeof(DataTableMapping), "SourceTable", srcTable, typeof(DataTableMappingCollection));
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x0011F280 File Offset: 0x0011E680
		internal static Exception TablesUniqueSourceTable(string srcTable)
		{
			return ADP.CollectionUniqueValue(typeof(DataTableMapping), "SourceTable", srcTable);
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x0011F2A4 File Offset: 0x0011E6A4
		internal static Exception NotADataTableMapping(object value)
		{
			return ADP.CollectionInvalidType(typeof(DataTableMappingCollection), typeof(DataTableMapping), value);
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x0011F2CC File Offset: 0x0011E6CC
		internal static InvalidOperationException CommandAsyncOperationCompleted()
		{
			return ADP.InvalidOperation(Res.GetString("SQL_AsyncOperationCompleted"));
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x0011F2E8 File Offset: 0x0011E6E8
		internal static Exception CommandTextRequired(string method)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_CommandTextRequired", new object[]
			{
				method
			}));
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x0011F310 File Offset: 0x0011E710
		internal static InvalidOperationException ConnectionRequired(string method)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_ConnectionRequired", new object[]
			{
				method
			}));
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x0011F338 File Offset: 0x0011E738
		internal static InvalidOperationException OpenConnectionRequired(string method, ConnectionState state)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_OpenConnectionRequired", new object[]
			{
				method,
				ADP.ConnectionStateMsg(state)
			}));
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x0011F368 File Offset: 0x0011E768
		internal static InvalidOperationException UpdateConnectionRequired(StatementType statementType, bool isRowUpdatingCommand)
		{
			string name;
			if (!isRowUpdatingCommand)
			{
				switch (statementType)
				{
				case StatementType.Insert:
					name = "ADP_ConnectionRequired_Insert";
					goto IL_4A;
				case StatementType.Update:
					name = "ADP_ConnectionRequired_Update";
					goto IL_4A;
				case StatementType.Delete:
					name = "ADP_ConnectionRequired_Delete";
					goto IL_4A;
				}
				throw ADP.InvalidStatementType(statementType);
			}
			name = "ADP_ConnectionRequired_Clone";
			IL_4A:
			return ADP.InvalidOperation(Res.GetString(name));
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x0011F3CC File Offset: 0x0011E7CC
		internal static InvalidOperationException ConnectionRequired_Res(string method)
		{
			string name = "ADP_ConnectionRequired_" + method;
			return ADP.InvalidOperation(Res.GetString(name));
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x0011F3F0 File Offset: 0x0011E7F0
		internal static InvalidOperationException UpdateOpenConnectionRequired(StatementType statementType, bool isRowUpdatingCommand, ConnectionState state)
		{
			string name;
			if (isRowUpdatingCommand)
			{
				name = "ADP_OpenConnectionRequired_Clone";
			}
			else
			{
				switch (statementType)
				{
				case StatementType.Insert:
					name = "ADP_OpenConnectionRequired_Insert";
					break;
				case StatementType.Update:
					name = "ADP_OpenConnectionRequired_Update";
					break;
				case StatementType.Delete:
					name = "ADP_OpenConnectionRequired_Delete";
					break;
				default:
					throw ADP.InvalidStatementType(statementType);
				}
			}
			return ADP.InvalidOperation(Res.GetString(name, new object[]
			{
				ADP.ConnectionStateMsg(state)
			}));
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x0011F458 File Offset: 0x0011E858
		internal static Exception NoStoredProcedureExists(string sproc)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_NoStoredProcedureExists", new object[]
			{
				sproc
			}));
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x0011F480 File Offset: 0x0011E880
		internal static Exception OpenReaderExists()
		{
			return ADP.OpenReaderExists(null);
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x0011F494 File Offset: 0x0011E894
		internal static Exception OpenReaderExists(Exception e)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_OpenReaderExists"), e);
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x0011F4B4 File Offset: 0x0011E8B4
		internal static Exception TransactionCompleted()
		{
			return ADP.DataAdapter(Res.GetString("ADP_TransactionCompleted"));
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x0011F4D0 File Offset: 0x0011E8D0
		internal static Exception NonSeqByteAccess(long badIndex, long currIndex, string method)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_NonSeqByteAccess", new object[]
			{
				badIndex.ToString(CultureInfo.InvariantCulture),
				currIndex.ToString(CultureInfo.InvariantCulture),
				method
			}));
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x0011F514 File Offset: 0x0011E914
		internal static Exception NegativeParameter(string parameterName)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_NegativeParameter", new object[]
			{
				parameterName
			}));
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x0011F53C File Offset: 0x0011E93C
		internal static Exception NumericToDecimalOverflow()
		{
			return ADP.InvalidCast(Res.GetString("ADP_NumericToDecimalOverflow"));
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x0011F558 File Offset: 0x0011E958
		internal static Exception ExceedsMaxDataLength(long specifiedLength, long maxLength)
		{
			return ADP.IndexOutOfRange(Res.GetString("SQL_ExceedsMaxDataLength", new object[]
			{
				specifiedLength.ToString(CultureInfo.InvariantCulture),
				maxLength.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x0011F598 File Offset: 0x0011E998
		internal static Exception InvalidSeekOrigin(string parameterName)
		{
			return ADP.ArgumentOutOfRange(Res.GetString("ADP_InvalidSeekOrigin"), parameterName);
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x0011F5B8 File Offset: 0x0011E9B8
		internal static Exception InvalidImplicitConversion(Type fromtype, string totype)
		{
			return ADP.InvalidCast(Res.GetString("ADP_InvalidImplicitConversion", new object[]
			{
				fromtype.Name,
				totype
			}));
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x0011F5E8 File Offset: 0x0011E9E8
		internal static Exception InvalidMetaDataValue()
		{
			return ADP.Argument(Res.GetString("ADP_InvalidMetaDataValue"));
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x0011F604 File Offset: 0x0011EA04
		internal static Exception NotRowType()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_NotRowType"));
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x0011F620 File Offset: 0x0011EA20
		internal static ArgumentException UnwantedStatementType(StatementType statementType)
		{
			return ADP.Argument(Res.GetString("ADP_UnwantedStatementType", new object[]
			{
				statementType.ToString()
			}));
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x0011F654 File Offset: 0x0011EA54
		internal static InvalidOperationException NonSequentialColumnAccess(int badCol, int currCol)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_NonSequentialColumnAccess", new object[]
			{
				badCol.ToString(CultureInfo.InvariantCulture),
				currCol.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x0011F694 File Offset: 0x0011EA94
		internal static Exception FillSchemaRequiresSourceTableName(string parameter)
		{
			return ADP.Argument(Res.GetString("ADP_FillSchemaRequiresSourceTableName"), parameter);
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x0011F6B4 File Offset: 0x0011EAB4
		internal static Exception InvalidMaxRecords(string parameter, int max)
		{
			return ADP.Argument(Res.GetString("ADP_InvalidMaxRecords", new object[]
			{
				max.ToString(CultureInfo.InvariantCulture)
			}), parameter);
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x0011F6E8 File Offset: 0x0011EAE8
		internal static Exception InvalidStartRecord(string parameter, int start)
		{
			return ADP.Argument(Res.GetString("ADP_InvalidStartRecord", new object[]
			{
				start.ToString(CultureInfo.InvariantCulture)
			}), parameter);
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x0011F71C File Offset: 0x0011EB1C
		internal static Exception FillRequires(string parameter)
		{
			return ADP.ArgumentNull(parameter);
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x0011F730 File Offset: 0x0011EB30
		internal static Exception FillRequiresSourceTableName(string parameter)
		{
			return ADP.Argument(Res.GetString("ADP_FillRequiresSourceTableName"), parameter);
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x0011F750 File Offset: 0x0011EB50
		internal static Exception FillChapterAutoIncrement()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_FillChapterAutoIncrement"));
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x0011F76C File Offset: 0x0011EB6C
		internal static InvalidOperationException MissingDataReaderFieldType(int index)
		{
			return ADP.DataAdapter(Res.GetString("ADP_MissingDataReaderFieldType", new object[]
			{
				index
			}));
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x0011F798 File Offset: 0x0011EB98
		internal static InvalidOperationException OnlyOneTableForStartRecordOrMaxRecords()
		{
			return ADP.DataAdapter(Res.GetString("ADP_OnlyOneTableForStartRecordOrMaxRecords"));
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x0011F7B4 File Offset: 0x0011EBB4
		internal static ArgumentNullException UpdateRequiresNonNullDataSet(string parameter)
		{
			return ADP.ArgumentNull(parameter);
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x0011F7C8 File Offset: 0x0011EBC8
		internal static InvalidOperationException UpdateRequiresSourceTable(string defaultSrcTableName)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_UpdateRequiresSourceTable", new object[]
			{
				defaultSrcTableName
			}));
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x0011F7F0 File Offset: 0x0011EBF0
		internal static InvalidOperationException UpdateRequiresSourceTableName(string srcTable)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_UpdateRequiresSourceTableName", new object[]
			{
				srcTable
			}));
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x0011F818 File Offset: 0x0011EC18
		internal static ArgumentNullException UpdateRequiresDataTable(string parameter)
		{
			return ADP.ArgumentNull(parameter);
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x0011F82C File Offset: 0x0011EC2C
		internal static Exception UpdateConcurrencyViolation(StatementType statementType, int affected, int expected, DataRow[] dataRows)
		{
			string name;
			switch (statementType)
			{
			case StatementType.Update:
				name = "ADP_UpdateConcurrencyViolation_Update";
				break;
			case StatementType.Delete:
				name = "ADP_UpdateConcurrencyViolation_Delete";
				break;
			case StatementType.Batch:
				name = "ADP_UpdateConcurrencyViolation_Batch";
				break;
			default:
				throw ADP.InvalidStatementType(statementType);
			}
			DBConcurrencyException ex = new DBConcurrencyException(Res.GetString(name, new object[]
			{
				affected.ToString(CultureInfo.InvariantCulture),
				expected.ToString(CultureInfo.InvariantCulture)
			}), null, dataRows);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x0011F8A8 File Offset: 0x0011ECA8
		internal static InvalidOperationException UpdateRequiresCommand(StatementType statementType, bool isRowUpdatingCommand)
		{
			string name;
			if (isRowUpdatingCommand)
			{
				name = "ADP_UpdateRequiresCommandClone";
			}
			else
			{
				switch (statementType)
				{
				case StatementType.Select:
					name = "ADP_UpdateRequiresCommandSelect";
					break;
				case StatementType.Insert:
					name = "ADP_UpdateRequiresCommandInsert";
					break;
				case StatementType.Update:
					name = "ADP_UpdateRequiresCommandUpdate";
					break;
				case StatementType.Delete:
					name = "ADP_UpdateRequiresCommandDelete";
					break;
				default:
					throw ADP.InvalidStatementType(statementType);
				}
			}
			return ADP.InvalidOperation(Res.GetString(name));
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x0011F90C File Offset: 0x0011ED0C
		internal static ArgumentException UpdateMismatchRowTable(int i)
		{
			return ADP.Argument(Res.GetString("ADP_UpdateMismatchRowTable", new object[]
			{
				i.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x0011F940 File Offset: 0x0011ED40
		internal static DataException RowUpdatedErrors()
		{
			return ADP.Data(Res.GetString("ADP_RowUpdatedErrors"));
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x0011F95C File Offset: 0x0011ED5C
		internal static DataException RowUpdatingErrors()
		{
			return ADP.Data(Res.GetString("ADP_RowUpdatingErrors"));
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x0011F978 File Offset: 0x0011ED78
		internal static InvalidOperationException ResultsNotAllowedDuringBatch()
		{
			return ADP.DataAdapter(Res.GetString("ADP_ResultsNotAllowedDuringBatch"));
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x0011F994 File Offset: 0x0011ED94
		internal static Exception InvalidCommandTimeout(int value)
		{
			return ADP.Argument(Res.GetString("ADP_InvalidCommandTimeout", new object[]
			{
				value.ToString(CultureInfo.InvariantCulture)
			}), "CommandTimeout");
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x0011F9CC File Offset: 0x0011EDCC
		internal static Exception DeriveParametersNotSupported(IDbCommand value)
		{
			return ADP.DataAdapter(Res.GetString("ADP_DeriveParametersNotSupported", new object[]
			{
				value.GetType().Name,
				value.CommandType.ToString()
			}));
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x0011FA14 File Offset: 0x0011EE14
		internal static Exception UninitializedParameterSize(int index, Type dataType)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_UninitializedParameterSize", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture),
				dataType.Name
			}));
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x0011FA50 File Offset: 0x0011EE50
		internal static Exception PrepareParameterType(IDbCommand cmd)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_PrepareParameterType", new object[]
			{
				cmd.GetType().Name
			}));
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x0011FA80 File Offset: 0x0011EE80
		internal static Exception PrepareParameterSize(IDbCommand cmd)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_PrepareParameterSize", new object[]
			{
				cmd.GetType().Name
			}));
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x0011FAB0 File Offset: 0x0011EEB0
		internal static Exception PrepareParameterScale(IDbCommand cmd, string type)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_PrepareParameterScale", new object[]
			{
				cmd.GetType().Name,
				type
			}));
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x0011FAE4 File Offset: 0x0011EEE4
		internal static Exception MismatchedAsyncResult(string expectedMethod, string gotMethod)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_MismatchedAsyncResult", new object[]
			{
				expectedMethod,
				gotMethod
			}));
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x0011FB10 File Offset: 0x0011EF10
		internal static Exception ConnectionIsDisabled(Exception InnerException)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_ConnectionIsDisabled"), InnerException);
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x0011FB30 File Offset: 0x0011EF30
		internal static Exception ClosedConnectionError()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_ClosedConnectionError"));
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x0011FB4C File Offset: 0x0011EF4C
		internal static Exception ConnectionAlreadyOpen(ConnectionState state)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_ConnectionAlreadyOpen", new object[]
			{
				ADP.ConnectionStateMsg(state)
			}));
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x0011FB78 File Offset: 0x0011EF78
		internal static Exception DelegatedTransactionPresent()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_DelegatedTransactionPresent"));
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x0011FB94 File Offset: 0x0011EF94
		internal static Exception TransactionPresent()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_TransactionPresent"));
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x0011FBB0 File Offset: 0x0011EFB0
		internal static Exception LocalTransactionPresent()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_LocalTransactionPresent"));
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x0011FBCC File Offset: 0x0011EFCC
		internal static Exception OpenConnectionPropertySet(string property, ConnectionState state)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_OpenConnectionPropertySet", new object[]
			{
				property,
				ADP.ConnectionStateMsg(state)
			}));
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x0011FBFC File Offset: 0x0011EFFC
		internal static Exception EmptyDatabaseName()
		{
			return ADP.Argument(Res.GetString("ADP_EmptyDatabaseName"));
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x0011FC18 File Offset: 0x0011F018
		internal static Exception DatabaseNameTooLong()
		{
			return ADP.Argument(Res.GetString("ADP_DatabaseNameTooLong"));
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x0011FC34 File Offset: 0x0011F034
		internal static Exception InternalConnectionError(ADP.ConnectionError internalError)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InternalConnectionError", new object[]
			{
				(int)internalError
			}));
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x0011FC60 File Offset: 0x0011F060
		internal static Exception InternalError(ADP.InternalErrorCode internalError)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InternalProviderError", new object[]
			{
				(int)internalError
			}));
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x0011FC8C File Offset: 0x0011F08C
		internal static Exception InternalError(ADP.InternalErrorCode internalError, Exception innerException)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InternalProviderError", new object[]
			{
				(int)internalError
			}), innerException);
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x0011FCB8 File Offset: 0x0011F0B8
		internal static Exception InvalidConnectTimeoutValue()
		{
			return ADP.Argument(Res.GetString("ADP_InvalidConnectTimeoutValue"));
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x0011FCD4 File Offset: 0x0011F0D4
		internal static Exception InvalidConnectRetryCountValue()
		{
			return ADP.Argument(Res.GetString("SQLCR_InvalidConnectRetryCountValue"));
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x0011FCF0 File Offset: 0x0011F0F0
		internal static Exception InvalidConnectRetryIntervalValue()
		{
			return ADP.Argument(Res.GetString("SQLCR_InvalidConnectRetryIntervalValue"));
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x0011FD0C File Offset: 0x0011F10C
		internal static Exception DataReaderNoData()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_DataReaderNoData"));
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x0011FD28 File Offset: 0x0011F128
		internal static Exception DataReaderClosed(string method)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_DataReaderClosed", new object[]
			{
				method
			}));
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x0011FD50 File Offset: 0x0011F150
		internal static ArgumentOutOfRangeException InvalidSourceBufferIndex(int maxLen, long srcOffset, string parameterName)
		{
			return ADP.ArgumentOutOfRange(Res.GetString("ADP_InvalidSourceBufferIndex", new object[]
			{
				maxLen.ToString(CultureInfo.InvariantCulture),
				srcOffset.ToString(CultureInfo.InvariantCulture)
			}), parameterName);
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x0011FD94 File Offset: 0x0011F194
		internal static ArgumentOutOfRangeException InvalidDestinationBufferIndex(int maxLen, int dstOffset, string parameterName)
		{
			return ADP.ArgumentOutOfRange(Res.GetString("ADP_InvalidDestinationBufferIndex", new object[]
			{
				maxLen.ToString(CultureInfo.InvariantCulture),
				dstOffset.ToString(CultureInfo.InvariantCulture)
			}), parameterName);
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x0011FDD8 File Offset: 0x0011F1D8
		internal static IndexOutOfRangeException InvalidBufferSizeOrIndex(int numBytes, int bufferIndex)
		{
			return ADP.IndexOutOfRange(Res.GetString("SQL_InvalidBufferSizeOrIndex", new object[]
			{
				numBytes.ToString(CultureInfo.InvariantCulture),
				bufferIndex.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x0011FE18 File Offset: 0x0011F218
		internal static Exception InvalidDataLength(long length)
		{
			return ADP.IndexOutOfRange(Res.GetString("SQL_InvalidDataLength", new object[]
			{
				length.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x0011FE4C File Offset: 0x0011F24C
		internal static InvalidOperationException AsyncOperationPending()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_PendingAsyncOperation"));
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x0011FE68 File Offset: 0x0011F268
		internal static Exception StreamClosed(string method)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_StreamClosed", new object[]
			{
				method
			}));
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x0011FE90 File Offset: 0x0011F290
		internal static IOException ErrorReadingFromStream(Exception internalException)
		{
			return ADP.IO(Res.GetString("SqlMisc_StreamErrorMessage"), internalException);
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x0011FEB0 File Offset: 0x0011F2B0
		internal static InvalidOperationException DynamicSQLJoinUnsupported()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_DynamicSQLJoinUnsupported"));
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x0011FECC File Offset: 0x0011F2CC
		internal static InvalidOperationException DynamicSQLNoTableInfo()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_DynamicSQLNoTableInfo"));
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x0011FEE8 File Offset: 0x0011F2E8
		internal static InvalidOperationException DynamicSQLNoKeyInfoDelete()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_DynamicSQLNoKeyInfoDelete"));
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x0011FF04 File Offset: 0x0011F304
		internal static InvalidOperationException DynamicSQLNoKeyInfoUpdate()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_DynamicSQLNoKeyInfoUpdate"));
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x0011FF20 File Offset: 0x0011F320
		internal static InvalidOperationException DynamicSQLNoKeyInfoRowVersionDelete()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_DynamicSQLNoKeyInfoRowVersionDelete"));
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x0011FF3C File Offset: 0x0011F33C
		internal static InvalidOperationException DynamicSQLNoKeyInfoRowVersionUpdate()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_DynamicSQLNoKeyInfoRowVersionUpdate"));
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x0011FF58 File Offset: 0x0011F358
		internal static InvalidOperationException DynamicSQLNestedQuote(string name, string quote)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_DynamicSQLNestedQuote", new object[]
			{
				name,
				quote
			}));
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x0011FF84 File Offset: 0x0011F384
		internal static InvalidOperationException NoQuoteChange()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_NoQuoteChange"));
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x0011FFA0 File Offset: 0x0011F3A0
		internal static InvalidOperationException ComputerNameEx(int lastError)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_ComputerNameEx", new object[]
			{
				lastError
			}));
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x0011FFCC File Offset: 0x0011F3CC
		internal static InvalidOperationException MissingSourceCommand()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_MissingSourceCommand"));
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x0011FFE8 File Offset: 0x0011F3E8
		internal static InvalidOperationException MissingSourceCommandConnection()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_MissingSourceCommandConnection"));
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x00120004 File Offset: 0x0011F404
		internal static ArgumentException InvalidDataType(TypeCode typecode)
		{
			return ADP.Argument(Res.GetString("ADP_InvalidDataType", new object[]
			{
				typecode.ToString()
			}));
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x00120038 File Offset: 0x0011F438
		internal static ArgumentException UnknownDataType(Type dataType)
		{
			return ADP.Argument(Res.GetString("ADP_UnknownDataType", new object[]
			{
				dataType.FullName
			}));
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x00120064 File Offset: 0x0011F464
		internal static ArgumentException DbTypeNotSupported(DbType type, Type enumtype)
		{
			return ADP.Argument(Res.GetString("ADP_DbTypeNotSupported", new object[]
			{
				type.ToString(),
				enumtype.Name
			}));
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x001200A0 File Offset: 0x0011F4A0
		internal static ArgumentException UnknownDataTypeCode(Type dataType, TypeCode typeCode)
		{
			string name = "ADP_UnknownDataTypeCode";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)typeCode;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = dataType.FullName;
			return ADP.Argument(Res.GetString(name, array));
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x001200DC File Offset: 0x0011F4DC
		internal static ArgumentException InvalidOffsetValue(int value)
		{
			return ADP.Argument(Res.GetString("ADP_InvalidOffsetValue", new object[]
			{
				value.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x00120110 File Offset: 0x0011F510
		internal static ArgumentException InvalidSizeValue(int value)
		{
			return ADP.Argument(Res.GetString("ADP_InvalidSizeValue", new object[]
			{
				value.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x00120144 File Offset: 0x0011F544
		internal static ArgumentException ParameterValueOutOfRange(decimal value)
		{
			return ADP.Argument(Res.GetString("ADP_ParameterValueOutOfRange", new object[]
			{
				value.ToString(null)
			}));
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x00120174 File Offset: 0x0011F574
		internal static ArgumentException ParameterValueOutOfRange(SqlDecimal value)
		{
			return ADP.Argument(Res.GetString("ADP_ParameterValueOutOfRange", new object[]
			{
				value.ToString()
			}));
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x001201A8 File Offset: 0x0011F5A8
		internal static ArgumentException ParameterValueOutOfRange(string value)
		{
			return ADP.Argument(Res.GetString("ADP_ParameterValueOutOfRange", new object[]
			{
				value
			}));
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x001201D0 File Offset: 0x0011F5D0
		internal static ArgumentException VersionDoesNotSupportDataType(string typeName)
		{
			return ADP.Argument(Res.GetString("ADP_VersionDoesNotSupportDataType", new object[]
			{
				typeName
			}));
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x001201F8 File Offset: 0x0011F5F8
		internal static Exception ParameterConversionFailed(object value, Type destType, Exception inner)
		{
			string @string = Res.GetString("ADP_ParameterConversionFailed", new object[]
			{
				value.GetType().Name,
				destType.Name
			});
			Exception ex;
			if (inner is ArgumentException)
			{
				ex = new ArgumentException(@string, inner);
			}
			else if (inner is FormatException)
			{
				ex = new FormatException(@string, inner);
			}
			else if (inner is InvalidCastException)
			{
				ex = new InvalidCastException(@string, inner);
			}
			else if (inner is OverflowException)
			{
				ex = new OverflowException(@string, inner);
			}
			else
			{
				ex = inner;
			}
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x00120280 File Offset: 0x0011F680
		internal static Exception ParametersMappingIndex(int index, IDataParameterCollection collection)
		{
			return ADP.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x001202A0 File Offset: 0x0011F6A0
		internal static Exception ParametersSourceIndex(string parameterName, IDataParameterCollection collection, Type parameterType)
		{
			return ADP.CollectionIndexString(parameterType, "ParameterName", parameterName, collection.GetType());
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x001202C0 File Offset: 0x0011F6C0
		internal static Exception ParameterNull(string parameter, IDataParameterCollection collection, Type parameterType)
		{
			return ADP.CollectionNullValue(parameter, collection.GetType(), parameterType);
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x001202DC File Offset: 0x0011F6DC
		internal static Exception InvalidParameterType(IDataParameterCollection collection, Type parameterType, object invalidValue)
		{
			return ADP.CollectionInvalidType(collection.GetType(), parameterType, invalidValue);
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x001202F8 File Offset: 0x0011F6F8
		internal static Exception ParallelTransactionsNotSupported(IDbConnection obj)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_ParallelTransactionsNotSupported", new object[]
			{
				obj.GetType().Name
			}));
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x00120328 File Offset: 0x0011F728
		internal static Exception TransactionZombied(IDbTransaction obj)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_TransactionZombied", new object[]
			{
				obj.GetType().Name
			}));
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x00120358 File Offset: 0x0011F758
		internal static Exception DbRecordReadOnly(string methodname)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_DbRecordReadOnly", new object[]
			{
				methodname
			}));
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x00120380 File Offset: 0x0011F780
		internal static Exception OffsetOutOfRangeException()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_OffsetOutOfRangeException"));
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x0012039C File Offset: 0x0011F79C
		internal static Exception AmbigousCollectionName(string collectionName)
		{
			return ADP.Argument(Res.GetString("MDF_AmbigousCollectionName", new object[]
			{
				collectionName
			}));
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x001203C4 File Offset: 0x0011F7C4
		internal static Exception CollectionNameIsNotUnique(string collectionName)
		{
			return ADP.Argument(Res.GetString("MDF_CollectionNameISNotUnique", new object[]
			{
				collectionName
			}));
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x001203EC File Offset: 0x0011F7EC
		internal static Exception DataTableDoesNotExist(string collectionName)
		{
			return ADP.Argument(Res.GetString("MDF_DataTableDoesNotExist", new object[]
			{
				collectionName
			}));
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x00120414 File Offset: 0x0011F814
		internal static Exception IncorrectNumberOfDataSourceInformationRows()
		{
			return ADP.Argument(Res.GetString("MDF_IncorrectNumberOfDataSourceInformationRows"));
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x00120430 File Offset: 0x0011F830
		internal static ArgumentException InvalidRestrictionValue(string collectionName, string restrictionName, string restrictionValue)
		{
			return ADP.Argument(Res.GetString("MDF_InvalidRestrictionValue", new object[]
			{
				collectionName,
				restrictionName,
				restrictionValue
			}));
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x00120460 File Offset: 0x0011F860
		internal static Exception InvalidXml()
		{
			return ADP.Argument(Res.GetString("MDF_InvalidXml"));
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x0012047C File Offset: 0x0011F87C
		internal static Exception InvalidXmlMissingColumn(string collectionName, string columnName)
		{
			return ADP.Argument(Res.GetString("MDF_InvalidXmlMissingColumn", new object[]
			{
				collectionName,
				columnName
			}));
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x001204A8 File Offset: 0x0011F8A8
		internal static Exception InvalidXmlInvalidValue(string collectionName, string columnName)
		{
			return ADP.Argument(Res.GetString("MDF_InvalidXmlInvalidValue", new object[]
			{
				collectionName,
				columnName
			}));
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x001204D4 File Offset: 0x0011F8D4
		internal static Exception MissingDataSourceInformationColumn()
		{
			return ADP.Argument(Res.GetString("MDF_MissingDataSourceInformationColumn"));
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x001204F0 File Offset: 0x0011F8F0
		internal static Exception MissingRestrictionColumn()
		{
			return ADP.Argument(Res.GetString("MDF_MissingRestrictionColumn"));
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x0012050C File Offset: 0x0011F90C
		internal static Exception MissingRestrictionRow()
		{
			return ADP.Argument(Res.GetString("MDF_MissingRestrictionRow"));
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x00120528 File Offset: 0x0011F928
		internal static Exception NoColumns()
		{
			return ADP.Argument(Res.GetString("MDF_NoColumns"));
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x00120544 File Offset: 0x0011F944
		internal static Exception QueryFailed(string collectionName, Exception e)
		{
			return ADP.InvalidOperation(Res.GetString("MDF_QueryFailed", new object[]
			{
				collectionName
			}), e);
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x0012056C File Offset: 0x0011F96C
		internal static Exception TooManyRestrictions(string collectionName)
		{
			return ADP.Argument(Res.GetString("MDF_TooManyRestrictions", new object[]
			{
				collectionName
			}));
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x00120594 File Offset: 0x0011F994
		internal static Exception UnableToBuildCollection(string collectionName)
		{
			return ADP.Argument(Res.GetString("MDF_UnableToBuildCollection", new object[]
			{
				collectionName
			}));
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x001205BC File Offset: 0x0011F9BC
		internal static Exception UndefinedCollection(string collectionName)
		{
			return ADP.Argument(Res.GetString("MDF_UndefinedCollection", new object[]
			{
				collectionName
			}));
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x001205E4 File Offset: 0x0011F9E4
		internal static Exception UndefinedPopulationMechanism(string populationMechanism)
		{
			return ADP.Argument(Res.GetString("MDF_UndefinedPopulationMechanism", new object[]
			{
				populationMechanism
			}));
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x0012060C File Offset: 0x0011FA0C
		internal static Exception UnsupportedVersion(string collectionName)
		{
			return ADP.Argument(Res.GetString("MDF_UnsupportedVersion", new object[]
			{
				collectionName
			}));
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x00120634 File Offset: 0x0011FA34
		internal static InvalidOperationException InvalidDateTimeDigits(string dataTypeName)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InvalidDateTimeDigits", new object[]
			{
				dataTypeName
			}));
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x0012065C File Offset: 0x0011FA5C
		internal static Exception InvalidFormatValue()
		{
			return ADP.Argument(Res.GetString("ADP_InvalidFormatValue"));
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x00120678 File Offset: 0x0011FA78
		internal static InvalidOperationException InvalidMaximumScale(string dataTypeName)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_InvalidMaximumScale", new object[]
			{
				dataTypeName
			}));
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x001206A0 File Offset: 0x0011FAA0
		internal static Exception LiteralValueIsInvalid(string dataTypeName)
		{
			return ADP.Argument(Res.GetString("ADP_LiteralValueIsInvalid", new object[]
			{
				dataTypeName
			}));
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x001206C8 File Offset: 0x0011FAC8
		internal static Exception EvenLengthLiteralValue(string argumentName)
		{
			return ADP.Argument(Res.GetString("ADP_EvenLengthLiteralValue"), argumentName);
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x001206E8 File Offset: 0x0011FAE8
		internal static Exception HexDigitLiteralValue(string argumentName)
		{
			return ADP.Argument(Res.GetString("ADP_HexDigitLiteralValue"), argumentName);
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x00120708 File Offset: 0x0011FB08
		internal static InvalidOperationException QuotePrefixNotSet(string method)
		{
			return ADP.InvalidOperation(Res.GetString("ADP_QuotePrefixNotSet", new object[]
			{
				method
			}));
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x00120730 File Offset: 0x0011FB30
		internal static InvalidOperationException UnableToCreateBooleanLiteral()
		{
			return ADP.InvalidOperation(Res.GetString("ADP_UnableToCreateBooleanLiteral"));
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x0012074C File Offset: 0x0011FB4C
		internal static Exception UnsupportedNativeDataTypeOleDb(string dataTypeName)
		{
			return ADP.Argument(Res.GetString("ADP_UnsupportedNativeDataTypeOleDb", new object[]
			{
				dataTypeName
			}));
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x00120774 File Offset: 0x0011FB74
		internal static Exception InvalidArgumentValue(string methodName)
		{
			return ADP.Argument(Res.GetString("ADP_InvalidArgumentValue", new object[]
			{
				methodName
			}));
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x0012079C File Offset: 0x0011FB9C
		internal static bool CompareInsensitiveInvariant(string strvalue, string strconst)
		{
			return CultureInfo.InvariantCulture.CompareInfo.Compare(strvalue, strconst, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x001207C0 File Offset: 0x0011FBC0
		internal static Delegate FindBuilder(MulticastDelegate mcd)
		{
			if (mcd != null)
			{
				Delegate[] invocationList = mcd.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					if (invocationList[i].Target is DbCommandBuilder)
					{
						return invocationList[i];
					}
				}
			}
			return null;
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x001207FC File Offset: 0x0011FBFC
		internal static Transaction GetCurrentTransaction()
		{
			return Transaction.Current;
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x00120810 File Offset: 0x0011FC10
		internal static void SetCurrentTransaction(Transaction transaction)
		{
			Transaction.Current = transaction;
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x00120824 File Offset: 0x0011FC24
		internal static IDtcTransaction GetOletxTransaction(Transaction transaction)
		{
			IDtcTransaction result = null;
			if (null != transaction)
			{
				result = TransactionInterop.GetDtcTransaction(transaction);
			}
			return result;
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x00120844 File Offset: 0x0011FC44
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static bool IsSysTxEqualSysEsTransaction()
		{
			return (!ContextUtil.IsInTransaction && null == Transaction.Current) || (ContextUtil.IsInTransaction && Transaction.Current == ContextUtil.SystemTransaction);
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x00120884 File Offset: 0x0011FC84
		internal static bool NeedManualEnlistment()
		{
			if (ADP.IsWindowsNT)
			{
				bool flag = !InOutOfProcHelper.InProc;
				if ((flag && !ADP.IsSysTxEqualSysEsTransaction()) || (!flag && null != Transaction.Current))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x001208C0 File Offset: 0x0011FCC0
		internal static void TimerCurrent(out long ticks)
		{
			ticks = DateTime.UtcNow.ToFileTimeUtc();
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x001208DC File Offset: 0x0011FCDC
		internal static long TimerCurrent()
		{
			return DateTime.UtcNow.ToFileTimeUtc();
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x001208F8 File Offset: 0x0011FCF8
		internal static long TimerFromSeconds(int seconds)
		{
			return checked(unchecked((long)seconds) * 10000000L);
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x00120910 File Offset: 0x0011FD10
		internal static long TimerFromMilliseconds(long milliseconds)
		{
			return checked(milliseconds * 10000L);
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x00120928 File Offset: 0x0011FD28
		internal static bool TimerHasExpired(long timerExpire)
		{
			return ADP.TimerCurrent() > timerExpire;
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x00120940 File Offset: 0x0011FD40
		internal static long TimerRemaining(long timerExpire)
		{
			long num = ADP.TimerCurrent();
			return checked(timerExpire - num);
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x00120958 File Offset: 0x0011FD58
		internal static long TimerRemainingMilliseconds(long timerExpire)
		{
			return ADP.TimerToMilliseconds(ADP.TimerRemaining(timerExpire));
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x00120974 File Offset: 0x0011FD74
		internal static long TimerRemainingSeconds(long timerExpire)
		{
			return ADP.TimerToSeconds(ADP.TimerRemaining(timerExpire));
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x00120990 File Offset: 0x0011FD90
		internal static long TimerToMilliseconds(long timerValue)
		{
			return timerValue / 10000L;
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x001209A8 File Offset: 0x0011FDA8
		private static long TimerToSeconds(long timerValue)
		{
			return timerValue / 10000000L;
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x001209C0 File Offset: 0x0011FDC0
		[EnvironmentPermission(SecurityAction.Assert, Read = "COMPUTERNAME")]
		internal static string MachineName()
		{
			return Environment.MachineName;
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x001209D4 File Offset: 0x0011FDD4
		internal static string BuildQuotedString(string quotePrefix, string quoteSuffix, string unQuotedString)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!ADP.IsEmpty(quotePrefix))
			{
				stringBuilder.Append(quotePrefix);
			}
			if (!ADP.IsEmpty(quoteSuffix))
			{
				stringBuilder.Append(unQuotedString.Replace(quoteSuffix, quoteSuffix + quoteSuffix));
				stringBuilder.Append(quoteSuffix);
			}
			else
			{
				stringBuilder.Append(unQuotedString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x00120A2C File Offset: 0x0011FE2C
		internal static byte[] ByteArrayFromString(string hexString, string dataTypeName)
		{
			if ((hexString.Length & 1) != 0)
			{
				throw ADP.LiteralValueIsInvalid(dataTypeName);
			}
			char[] array = hexString.ToCharArray();
			byte[] array2 = new byte[hexString.Length / 2];
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			for (int i = 0; i < hexString.Length; i += 2)
			{
				int num = "0123456789abcdef".IndexOf(char.ToLower(array[i], invariantCulture));
				int num2 = "0123456789abcdef".IndexOf(char.ToLower(array[i + 1], invariantCulture));
				if (num < 0 || num2 < 0)
				{
					throw ADP.LiteralValueIsInvalid(dataTypeName);
				}
				array2[i / 2] = (byte)(num << 4 | num2);
			}
			return array2;
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x00120AC4 File Offset: 0x0011FEC4
		internal static void EscapeSpecialCharacters(string unescapedString, StringBuilder escapedString)
		{
			foreach (char value in unescapedString)
			{
				if (".$^{[(|)*+?\\]".IndexOf(value) >= 0)
				{
					escapedString.Append("\\");
				}
				escapedString.Append(value);
			}
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x00120B10 File Offset: 0x0011FF10
		internal static string FixUpDecimalSeparator(string numericString, bool formatLiteral, string decimalSeparator, char[] exponentSymbols)
		{
			string result;
			if (numericString.IndexOfAny(exponentSymbols) == -1)
			{
				if (ADP.IsEmpty(decimalSeparator))
				{
					decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
				}
				if (formatLiteral)
				{
					result = numericString.Replace(".", decimalSeparator);
				}
				else
				{
					result = numericString.Replace(decimalSeparator, ".");
				}
			}
			else
			{
				result = numericString;
			}
			return result;
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x00120B64 File Offset: 0x0011FF64
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		internal static string GetFullPath(string filename)
		{
			return Path.GetFullPath(filename);
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x00120B78 File Offset: 0x0011FF78
		internal static string GetComputerNameDnsFullyQualified()
		{
			string result;
			if (ADP.IsPlatformNT5)
			{
				int num = 0;
				int num2 = 0;
				if (SafeNativeMethods.GetComputerNameEx(3, null, ref num) == 0)
				{
					num2 = Marshal.GetLastWin32Error();
				}
				if ((num2 != 0 && num2 != 234) || num <= 0)
				{
					throw ADP.ComputerNameEx(num2);
				}
				StringBuilder stringBuilder = new StringBuilder(num);
				num = stringBuilder.Capacity;
				if (SafeNativeMethods.GetComputerNameEx(3, stringBuilder, ref num) == 0)
				{
					throw ADP.ComputerNameEx(Marshal.GetLastWin32Error());
				}
				result = stringBuilder.ToString();
			}
			else
			{
				result = ADP.MachineName();
			}
			return result;
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x00120BEC File Offset: 0x0011FFEC
		internal static Stream GetFileStream(string filename)
		{
			new FileIOPermission(FileIOPermissionAccess.Read, filename).Assert();
			Stream result;
			try
			{
				result = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x00120C34 File Offset: 0x00120034
		internal static FileVersionInfo GetVersionInfo(string filename)
		{
			new FileIOPermission(FileIOPermissionAccess.Read, filename).Assert();
			FileVersionInfo versionInfo;
			try
			{
				versionInfo = FileVersionInfo.GetVersionInfo(filename);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return versionInfo;
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x00120C7C File Offset: 0x0012007C
		internal static Stream GetXmlStreamFromValues(string[] values, string errorString)
		{
			if (values.Length != 1)
			{
				throw ADP.ConfigWrongNumberOfValues(errorString);
			}
			return ADP.GetXmlStream(values[0], errorString);
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x00120CA0 File Offset: 0x001200A0
		internal static Stream GetXmlStream(string value, string errorString)
		{
			string runtimeDirectory = RuntimeEnvironment.GetRuntimeDirectory();
			if (runtimeDirectory == null)
			{
				throw ADP.ConfigUnableToLoadXmlMetaDataFile(errorString);
			}
			StringBuilder stringBuilder = new StringBuilder(runtimeDirectory.Length + "config\\".Length + value.Length);
			stringBuilder.Append(runtimeDirectory);
			stringBuilder.Append("config\\");
			stringBuilder.Append(value);
			string text = stringBuilder.ToString();
			if (ADP.GetFullPath(text) != text)
			{
				throw ADP.ConfigUnableToLoadXmlMetaDataFile(errorString);
			}
			Stream fileStream;
			try
			{
				fileStream = ADP.GetFileStream(text);
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				throw ADP.ConfigUnableToLoadXmlMetaDataFile(errorString);
			}
			return fileStream;
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x00120D50 File Offset: 0x00120150
		internal static object ClassesRootRegistryValue(string subkey, string queryvalue)
		{
			new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_CLASSES_ROOT\\" + subkey).Assert();
			object result;
			try
			{
				using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(subkey, false))
				{
					result = ((registryKey != null) ? registryKey.GetValue(queryvalue) : null);
				}
			}
			catch (SecurityException e)
			{
				ADP.TraceExceptionWithoutRethrow(e);
				result = null;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x00120DFC File Offset: 0x001201FC
		internal static object LocalMachineRegistryValue(string subkey, string queryvalue)
		{
			new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\" + subkey).Assert();
			object result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(subkey, false))
				{
					result = ((registryKey != null) ? registryKey.GetValue(queryvalue) : null);
				}
			}
			catch (SecurityException e)
			{
				ADP.TraceExceptionWithoutRethrow(e);
				result = null;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x00120EA8 File Offset: 0x001202A8
		internal static void CheckVersionMDAC(bool ifodbcelseoledb)
		{
			string text;
			int num;
			int num2;
			int num3;
			try
			{
				text = (string)ADP.LocalMachineRegistryValue("Software\\Microsoft\\DataAccess", "FullInstallVer");
				if (ADP.IsEmpty(text))
				{
					string filename = (string)ADP.ClassesRootRegistryValue("CLSID\\{2206CDB2-19C1-11D1-89E0-00C04FD7A829}\\InprocServer32", ADP.StrEmpty);
					FileVersionInfo versionInfo = ADP.GetVersionInfo(filename);
					num = versionInfo.FileMajorPart;
					num2 = versionInfo.FileMinorPart;
					num3 = versionInfo.FileBuildPart;
					text = versionInfo.FileVersion;
				}
				else
				{
					string[] array = text.Split(new char[]
					{
						'.'
					});
					num = int.Parse(array[0], NumberStyles.None, CultureInfo.InvariantCulture);
					num2 = int.Parse(array[1], NumberStyles.None, CultureInfo.InvariantCulture);
					num3 = int.Parse(array[2], NumberStyles.None, CultureInfo.InvariantCulture);
					int.Parse(array[3], NumberStyles.None, CultureInfo.InvariantCulture);
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ODB.MDACNotAvailable(ex);
			}
			if (num >= 2 && (num != 2 || (num2 >= 60 && (num2 != 60 || num3 >= 6526))))
			{
				return;
			}
			if (ifodbcelseoledb)
			{
				throw ADP.DataAdapter(Res.GetString("Odbc_MDACWrongVersion", new object[]
				{
					text
				}));
			}
			throw ADP.DataAdapter(Res.GetString("OleDb_MDACWrongVersion", new object[]
			{
				text
			}));
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x00120FEC File Offset: 0x001203EC
		internal static bool RemoveStringQuotes(string quotePrefix, string quoteSuffix, string quotedString, out string unquotedString)
		{
			int num;
			if (quotePrefix == null)
			{
				num = 0;
			}
			else
			{
				num = quotePrefix.Length;
			}
			int num2;
			if (quoteSuffix == null)
			{
				num2 = 0;
			}
			else
			{
				num2 = quoteSuffix.Length;
			}
			if (num2 + num == 0)
			{
				unquotedString = quotedString;
				return true;
			}
			if (quotedString == null)
			{
				unquotedString = quotedString;
				return false;
			}
			int length = quotedString.Length;
			if (length < num + num2)
			{
				unquotedString = quotedString;
				return false;
			}
			if (num > 0 && !quotedString.StartsWith(quotePrefix, StringComparison.Ordinal))
			{
				unquotedString = quotedString;
				return false;
			}
			if (num2 > 0)
			{
				if (!quotedString.EndsWith(quoteSuffix, StringComparison.Ordinal))
				{
					unquotedString = quotedString;
					return false;
				}
				unquotedString = quotedString.Substring(num, length - (num + num2)).Replace(quoteSuffix + quoteSuffix, quoteSuffix);
			}
			else
			{
				unquotedString = quotedString.Substring(num, length - num);
			}
			return true;
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x0012108C File Offset: 0x0012048C
		internal static DataRow[] SelectAdapterRows(DataTable dataTable, bool sorted)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			DataRowCollection rows = dataTable.Rows;
			foreach (object obj in rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataRowState rowState = dataRow.RowState;
				if (rowState != DataRowState.Added)
				{
					if (rowState != DataRowState.Deleted)
					{
						if (rowState == DataRowState.Modified)
						{
							num3++;
						}
					}
					else
					{
						num2++;
					}
				}
				else
				{
					num++;
				}
			}
			DataRow[] array = new DataRow[num + num2 + num3];
			if (sorted)
			{
				num3 = num + num2;
				num2 = num;
				num = 0;
				using (IEnumerator enumerator2 = rows.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						DataRow dataRow2 = (DataRow)obj2;
						DataRowState rowState2 = dataRow2.RowState;
						if (rowState2 != DataRowState.Added)
						{
							if (rowState2 != DataRowState.Deleted)
							{
								if (rowState2 == DataRowState.Modified)
								{
									array[num3++] = dataRow2;
								}
							}
							else
							{
								array[num2++] = dataRow2;
							}
						}
						else
						{
							array[num++] = dataRow2;
						}
					}
					return array;
				}
			}
			int num4 = 0;
			foreach (object obj3 in rows)
			{
				DataRow dataRow3 = (DataRow)obj3;
				if ((dataRow3.RowState & (DataRowState.Added | DataRowState.Deleted | DataRowState.Modified)) != (DataRowState)0)
				{
					array[num4++] = dataRow3;
					if (num4 == array.Length)
					{
						break;
					}
				}
			}
			return array;
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x00121240 File Offset: 0x00120640
		internal static int StringLength(string inputString)
		{
			if (inputString == null)
			{
				return 0;
			}
			return inputString.Length;
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x00121258 File Offset: 0x00120658
		internal static void BuildSchemaTableInfoTableNames(string[] columnNameArray)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(columnNameArray.Length);
			int num = columnNameArray.Length;
			int num2 = columnNameArray.Length - 1;
			while (0 <= num2)
			{
				string text = columnNameArray[num2];
				if (text != null && 0 < text.Length)
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					int val;
					if (dictionary.TryGetValue(text, out val))
					{
						num = Math.Min(num, val);
					}
					dictionary[text] = num2;
				}
				else
				{
					columnNameArray[num2] = ADP.StrEmpty;
					num = num2;
				}
				num2--;
			}
			int uniqueIndex = 1;
			for (int i = num; i < columnNameArray.Length; i++)
			{
				string text2 = columnNameArray[i];
				if (text2.Length == 0)
				{
					columnNameArray[i] = "Column";
					uniqueIndex = ADP.GenerateUniqueName(dictionary, ref columnNameArray[i], i, uniqueIndex);
				}
				else
				{
					text2 = text2.ToLower(CultureInfo.InvariantCulture);
					if (i != dictionary[text2])
					{
						ADP.GenerateUniqueName(dictionary, ref columnNameArray[i], i, 1);
					}
				}
			}
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x00121330 File Offset: 0x00120730
		private static int GenerateUniqueName(Dictionary<string, int> hash, ref string columnName, int index, int uniqueIndex)
		{
			string text;
			string key;
			for (;;)
			{
				text = columnName + uniqueIndex.ToString(CultureInfo.InvariantCulture);
				key = text.ToLower(CultureInfo.InvariantCulture);
				if (!hash.ContainsKey(key))
				{
					break;
				}
				uniqueIndex++;
			}
			columnName = text;
			hash.Add(key, index);
			return uniqueIndex;
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x0012137C File Offset: 0x0012077C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal static IntPtr IntPtrOffset(IntPtr pbase, int offset)
		{
			checked
			{
				if (4 == ADP.PtrSize)
				{
					return (IntPtr)(pbase.ToInt32() + offset);
				}
				return (IntPtr)(pbase.ToInt64() + unchecked((long)offset));
			}
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x001213B0 File Offset: 0x001207B0
		internal static int IntPtrToInt32(IntPtr value)
		{
			if (4 == ADP.PtrSize)
			{
				return (int)value;
			}
			long num = (long)value;
			num = Math.Min(2147483647L, num);
			num = Math.Max(-2147483648L, num);
			return (int)num;
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x001213F0 File Offset: 0x001207F0
		internal static int SrcCompare(string strA, string strB)
		{
			if (!(strA == strB))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x0012140C File Offset: 0x0012080C
		internal static int DstCompare(string strA, string strB)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x0012142C File Offset: 0x0012082C
		internal static bool IsDirection(IDataParameter value, ParameterDirection condition)
		{
			return condition == (condition & value.Direction);
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x00121444 File Offset: 0x00120844
		internal static bool IsEmpty(string str)
		{
			return str == null || str.Length == 0;
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x00121460 File Offset: 0x00120860
		internal static bool IsEmptyArray(string[] array)
		{
			return array == null || array.Length == 0;
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x00121478 File Offset: 0x00120878
		internal static bool IsNull(object value)
		{
			if (value == null || DBNull.Value == value)
			{
				return true;
			}
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x001214A4 File Offset: 0x001208A4
		internal static void IsNullOrSqlType(object value, out bool isNull, out bool isSqlType)
		{
			if (value == null || value == DBNull.Value)
			{
				isNull = true;
				isSqlType = false;
				return;
			}
			INullable nullable = value as INullable;
			if (nullable != null)
			{
				isNull = nullable.IsNull;
				isSqlType = DataStorage.IsSqlType(value.GetType());
				return;
			}
			isNull = false;
			isSqlType = false;
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x001214EC File Offset: 0x001208EC
		internal static Version GetAssemblyVersion()
		{
			if (ADP._systemDataVersion == null)
			{
				ADP._systemDataVersion = new Version("4.8.9221.0");
			}
			return ADP._systemDataVersion;
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x0012151C File Offset: 0x0012091C
		internal static bool IsAzureSqlServerEndpoint(string dataSource)
		{
			int i = dataSource.LastIndexOf(',');
			if (i >= 0)
			{
				dataSource = dataSource.Substring(0, i);
			}
			i = dataSource.LastIndexOf('\\');
			if (i >= 0)
			{
				dataSource = dataSource.Substring(0, i);
			}
			dataSource = dataSource.Trim();
			for (i = 0; i < ADP.AzureSqlServerEndpoints.Length; i++)
			{
				if (dataSource.EndsWith(ADP.AzureSqlServerEndpoints[i], StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001BCE RID: 7118
		private static Task<bool> _trueTask = null;

		// Token: 0x04001BCF RID: 7119
		private static Task<bool> _falseTask = null;

		// Token: 0x04001BD0 RID: 7120
		private static readonly Type StackOverflowType = typeof(StackOverflowException);

		// Token: 0x04001BD1 RID: 7121
		private static readonly Type OutOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x04001BD2 RID: 7122
		private static readonly Type ThreadAbortType = typeof(ThreadAbortException);

		// Token: 0x04001BD3 RID: 7123
		private static readonly Type NullReferenceType = typeof(NullReferenceException);

		// Token: 0x04001BD4 RID: 7124
		private static readonly Type AccessViolationType = typeof(AccessViolationException);

		// Token: 0x04001BD5 RID: 7125
		private static readonly Type SecurityType = typeof(SecurityException);

		// Token: 0x04001BD6 RID: 7126
		internal const string Append = "Append";

		// Token: 0x04001BD7 RID: 7127
		internal const string BeginExecuteNonQuery = "BeginExecuteNonQuery";

		// Token: 0x04001BD8 RID: 7128
		internal const string BeginExecuteReader = "BeginExecuteReader";

		// Token: 0x04001BD9 RID: 7129
		internal const string BeginTransaction = "BeginTransaction";

		// Token: 0x04001BDA RID: 7130
		internal const string BeginExecuteXmlReader = "BeginExecuteXmlReader";

		// Token: 0x04001BDB RID: 7131
		internal const string ChangeDatabase = "ChangeDatabase";

		// Token: 0x04001BDC RID: 7132
		internal const string Cancel = "Cancel";

		// Token: 0x04001BDD RID: 7133
		internal const string Clone = "Clone";

		// Token: 0x04001BDE RID: 7134
		internal const string ColumnEncryptionSystemProviderNamePrefix = "MSSQL_";

		// Token: 0x04001BDF RID: 7135
		internal const string CommitTransaction = "CommitTransaction";

		// Token: 0x04001BE0 RID: 7136
		internal const string CommandTimeout = "CommandTimeout";

		// Token: 0x04001BE1 RID: 7137
		internal const string ConnectionString = "ConnectionString";

		// Token: 0x04001BE2 RID: 7138
		internal const string DataSetColumn = "DataSetColumn";

		// Token: 0x04001BE3 RID: 7139
		internal const string DataSetTable = "DataSetTable";

		// Token: 0x04001BE4 RID: 7140
		internal const string Delete = "Delete";

		// Token: 0x04001BE5 RID: 7141
		internal const string DeleteCommand = "DeleteCommand";

		// Token: 0x04001BE6 RID: 7142
		internal const string DeriveParameters = "DeriveParameters";

		// Token: 0x04001BE7 RID: 7143
		internal const string EndExecuteNonQuery = "EndExecuteNonQuery";

		// Token: 0x04001BE8 RID: 7144
		internal const string EndExecuteReader = "EndExecuteReader";

		// Token: 0x04001BE9 RID: 7145
		internal const string EndExecuteXmlReader = "EndExecuteXmlReader";

		// Token: 0x04001BEA RID: 7146
		internal const string ExecuteReader = "ExecuteReader";

		// Token: 0x04001BEB RID: 7147
		internal const string ExecuteRow = "ExecuteRow";

		// Token: 0x04001BEC RID: 7148
		internal const string ExecuteNonQuery = "ExecuteNonQuery";

		// Token: 0x04001BED RID: 7149
		internal const string ExecuteScalar = "ExecuteScalar";

		// Token: 0x04001BEE RID: 7150
		internal const string ExecuteSqlScalar = "ExecuteSqlScalar";

		// Token: 0x04001BEF RID: 7151
		internal const string ExecuteXmlReader = "ExecuteXmlReader";

		// Token: 0x04001BF0 RID: 7152
		internal const string Fill = "Fill";

		// Token: 0x04001BF1 RID: 7153
		internal const string FillPage = "FillPage";

		// Token: 0x04001BF2 RID: 7154
		internal const string FillSchema = "FillSchema";

		// Token: 0x04001BF3 RID: 7155
		internal const string GetBytes = "GetBytes";

		// Token: 0x04001BF4 RID: 7156
		internal const string GetChars = "GetChars";

		// Token: 0x04001BF5 RID: 7157
		internal const string GetOleDbSchemaTable = "GetOleDbSchemaTable";

		// Token: 0x04001BF6 RID: 7158
		internal const string GetProperties = "GetProperties";

		// Token: 0x04001BF7 RID: 7159
		internal const string GetSchema = "GetSchema";

		// Token: 0x04001BF8 RID: 7160
		internal const string GetSchemaTable = "GetSchemaTable";

		// Token: 0x04001BF9 RID: 7161
		internal const string GetServerTransactionLevel = "GetServerTransactionLevel";

		// Token: 0x04001BFA RID: 7162
		internal const string Insert = "Insert";

		// Token: 0x04001BFB RID: 7163
		internal const string Open = "Open";

		// Token: 0x04001BFC RID: 7164
		internal const string Parameter = "Parameter";

		// Token: 0x04001BFD RID: 7165
		internal const string ParameterBuffer = "buffer";

		// Token: 0x04001BFE RID: 7166
		internal const string ParameterCount = "count";

		// Token: 0x04001BFF RID: 7167
		internal const string ParameterDestinationType = "destinationType";

		// Token: 0x04001C00 RID: 7168
		internal const string ParameterIndex = "index";

		// Token: 0x04001C01 RID: 7169
		internal const string ParameterName = "ParameterName";

		// Token: 0x04001C02 RID: 7170
		internal const string ParameterOffset = "offset";

		// Token: 0x04001C03 RID: 7171
		internal const string ParameterSetPosition = "set_Position";

		// Token: 0x04001C04 RID: 7172
		internal const string ParameterService = "Service";

		// Token: 0x04001C05 RID: 7173
		internal const string ParameterTimeout = "Timeout";

		// Token: 0x04001C06 RID: 7174
		internal const string ParameterUserData = "UserData";

		// Token: 0x04001C07 RID: 7175
		internal const string Prepare = "Prepare";

		// Token: 0x04001C08 RID: 7176
		internal const string QuoteIdentifier = "QuoteIdentifier";

		// Token: 0x04001C09 RID: 7177
		internal const string Read = "Read";

		// Token: 0x04001C0A RID: 7178
		internal const string ReadAsync = "ReadAsync";

		// Token: 0x04001C0B RID: 7179
		internal const string Remove = "Remove";

		// Token: 0x04001C0C RID: 7180
		internal const string RollbackTransaction = "RollbackTransaction";

		// Token: 0x04001C0D RID: 7181
		internal const string SaveTransaction = "SaveTransaction";

		// Token: 0x04001C0E RID: 7182
		internal const string SetProperties = "SetProperties";

		// Token: 0x04001C0F RID: 7183
		internal const string SourceColumn = "SourceColumn";

		// Token: 0x04001C10 RID: 7184
		internal const string SourceVersion = "SourceVersion";

		// Token: 0x04001C11 RID: 7185
		internal const string SourceTable = "SourceTable";

		// Token: 0x04001C12 RID: 7186
		internal const string UnquoteIdentifier = "UnquoteIdentifier";

		// Token: 0x04001C13 RID: 7187
		internal const string Update = "Update";

		// Token: 0x04001C14 RID: 7188
		internal const string UpdateCommand = "UpdateCommand";

		// Token: 0x04001C15 RID: 7189
		internal const string UpdateRows = "UpdateRows";

		// Token: 0x04001C16 RID: 7190
		internal const CompareOptions compareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x04001C17 RID: 7191
		internal const int DecimalMaxPrecision = 29;

		// Token: 0x04001C18 RID: 7192
		internal const int DecimalMaxPrecision28 = 28;

		// Token: 0x04001C19 RID: 7193
		internal const int DefaultCommandTimeout = 30;

		// Token: 0x04001C1A RID: 7194
		internal const int DefaultConnectionTimeout = 15;

		// Token: 0x04001C1B RID: 7195
		internal const float FailoverTimeoutStep = 0.08f;

		// Token: 0x04001C1C RID: 7196
		internal const float FailoverTimeoutStepForTnir = 0.125f;

		// Token: 0x04001C1D RID: 7197
		internal const int MinimumTimeoutForTnirMs = 500;

		// Token: 0x04001C1E RID: 7198
		internal static readonly string StrEmpty = "";

		// Token: 0x04001C1F RID: 7199
		internal static readonly IntPtr PtrZero = new IntPtr(0);

		// Token: 0x04001C20 RID: 7200
		internal static readonly int PtrSize = IntPtr.Size;

		// Token: 0x04001C21 RID: 7201
		internal static readonly IntPtr InvalidPtr = new IntPtr(-1);

		// Token: 0x04001C22 RID: 7202
		internal static readonly IntPtr RecordsUnaffected = new IntPtr(-1);

		// Token: 0x04001C23 RID: 7203
		internal static readonly HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

		// Token: 0x04001C24 RID: 7204
		internal const int CharSize = 2;

		// Token: 0x04001C25 RID: 7205
		internal static readonly bool IsWindowsNT = PlatformID.Win32NT == Environment.OSVersion.Platform;

		// Token: 0x04001C26 RID: 7206
		internal static readonly bool IsPlatformNT5 = ADP.IsWindowsNT && Environment.OSVersion.Version.Major >= 5;

		// Token: 0x04001C27 RID: 7207
		private const string hexDigits = "0123456789abcdef";

		// Token: 0x04001C28 RID: 7208
		private static Version _systemDataVersion;

		// Token: 0x04001C29 RID: 7209
		internal static readonly string[] AzureSqlServerEndpoints = new string[]
		{
			Res.GetString("AZURESQL_GenericEndpoint"),
			Res.GetString("AZURESQL_GermanEndpoint"),
			Res.GetString("AZURESQL_UsGovEndpoint"),
			Res.GetString("AZURESQL_ChinaEndpoint")
		};

		// Token: 0x02000431 RID: 1073
		internal enum ConnectionError
		{
			// Token: 0x0400230A RID: 8970
			BeginGetConnectionReturnsNull,
			// Token: 0x0400230B RID: 8971
			GetConnectionReturnsNull,
			// Token: 0x0400230C RID: 8972
			ConnectionOptionsMissing,
			// Token: 0x0400230D RID: 8973
			CouldNotSwitchToClosedPreviouslyOpenedState
		}

		// Token: 0x02000432 RID: 1074
		internal enum InternalErrorCode
		{
			// Token: 0x0400230F RID: 8975
			UnpooledObjectHasOwner,
			// Token: 0x04002310 RID: 8976
			UnpooledObjectHasWrongOwner,
			// Token: 0x04002311 RID: 8977
			PushingObjectSecondTime,
			// Token: 0x04002312 RID: 8978
			PooledObjectHasOwner,
			// Token: 0x04002313 RID: 8979
			PooledObjectInPoolMoreThanOnce,
			// Token: 0x04002314 RID: 8980
			CreateObjectReturnedNull,
			// Token: 0x04002315 RID: 8981
			NewObjectCannotBePooled,
			// Token: 0x04002316 RID: 8982
			NonPooledObjectUsedMoreThanOnce,
			// Token: 0x04002317 RID: 8983
			AttemptingToPoolOnRestrictedToken,
			// Token: 0x04002318 RID: 8984
			ConvertSidToStringSidWReturnedNull = 10,
			// Token: 0x04002319 RID: 8985
			AttemptingToConstructReferenceCollectionOnStaticObject = 12,
			// Token: 0x0400231A RID: 8986
			AttemptingToEnlistTwice,
			// Token: 0x0400231B RID: 8987
			CreateReferenceCollectionReturnedNull,
			// Token: 0x0400231C RID: 8988
			PooledObjectWithoutPool,
			// Token: 0x0400231D RID: 8989
			UnexpectedWaitAnyResult,
			// Token: 0x0400231E RID: 8990
			SynchronousConnectReturnedPending,
			// Token: 0x0400231F RID: 8991
			CompletedConnectReturnedPending,
			// Token: 0x04002320 RID: 8992
			NameValuePairNext = 20,
			// Token: 0x04002321 RID: 8993
			InvalidParserState1,
			// Token: 0x04002322 RID: 8994
			InvalidParserState2,
			// Token: 0x04002323 RID: 8995
			InvalidParserState3,
			// Token: 0x04002324 RID: 8996
			InvalidBuffer = 30,
			// Token: 0x04002325 RID: 8997
			UnimplementedSMIMethod = 40,
			// Token: 0x04002326 RID: 8998
			InvalidSmiCall,
			// Token: 0x04002327 RID: 8999
			SqlDependencyObtainProcessDispatcherFailureObjectHandle = 50,
			// Token: 0x04002328 RID: 9000
			SqlDependencyProcessDispatcherFailureCreateInstance,
			// Token: 0x04002329 RID: 9001
			SqlDependencyProcessDispatcherFailureAppDomain,
			// Token: 0x0400232A RID: 9002
			SqlDependencyCommandHashIsNotAssociatedWithNotification,
			// Token: 0x0400232B RID: 9003
			UnknownTransactionFailure = 60
		}
	}
}
