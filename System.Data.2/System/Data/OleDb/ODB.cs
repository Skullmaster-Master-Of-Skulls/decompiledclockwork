using System;
using System.Data.Common;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.OleDb
{
	// Token: 0x0200027F RID: 639
	internal static class ODB
	{
		// Token: 0x060026AA RID: 9898 RVA: 0x001063FC File Offset: 0x001057FC
		internal static void CommandParameterStatus(StringBuilder builder, int index, DBStatus status)
		{
			switch (status)
			{
			case DBStatus.S_OK:
			case DBStatus.S_ISNULL:
			case DBStatus.S_IGNORE:
				return;
			case DBStatus.E_BADACCESSOR:
				builder.Append(Res.GetString("OleDb_CommandParameterBadAccessor", new object[]
				{
					index.ToString(CultureInfo.InvariantCulture),
					""
				}));
				builder.Append(Environment.NewLine);
				return;
			case DBStatus.E_CANTCONVERTVALUE:
				builder.Append(Res.GetString("OleDb_CommandParameterCantConvertValue", new object[]
				{
					index.ToString(CultureInfo.InvariantCulture),
					""
				}));
				builder.Append(Environment.NewLine);
				return;
			case DBStatus.E_SIGNMISMATCH:
				builder.Append(Res.GetString("OleDb_CommandParameterSignMismatch", new object[]
				{
					index.ToString(CultureInfo.InvariantCulture),
					""
				}));
				builder.Append(Environment.NewLine);
				return;
			case DBStatus.E_DATAOVERFLOW:
				builder.Append(Res.GetString("OleDb_CommandParameterDataOverflow", new object[]
				{
					index.ToString(CultureInfo.InvariantCulture),
					""
				}));
				builder.Append(Environment.NewLine);
				return;
			case DBStatus.E_UNAVAILABLE:
				builder.Append(Res.GetString("OleDb_CommandParameterUnavailable", new object[]
				{
					index.ToString(CultureInfo.InvariantCulture),
					""
				}));
				builder.Append(Environment.NewLine);
				return;
			case DBStatus.S_DEFAULT:
				builder.Append(Res.GetString("OleDb_CommandParameterDefault", new object[]
				{
					index.ToString(CultureInfo.InvariantCulture),
					""
				}));
				builder.Append(Environment.NewLine);
				return;
			}
			builder.Append(Res.GetString("OleDb_CommandParameterError", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture),
				status.ToString()
			}));
			builder.Append(Environment.NewLine);
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x001065F8 File Offset: 0x001059F8
		internal static Exception CommandParameterStatus(string value, Exception inner)
		{
			if (ADP.IsEmpty(value))
			{
				return inner;
			}
			return ADP.InvalidOperation(value, inner);
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x00106618 File Offset: 0x00105A18
		internal static Exception UninitializedParameters(int index, OleDbType dbtype)
		{
			return ADP.InvalidOperation(Res.GetString("OleDb_UninitializedParameters", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture),
				dbtype.ToString()
			}));
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x0010665C File Offset: 0x00105A5C
		internal static Exception BadStatus_ParamAcc(int index, DBBindStatus status)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_BadStatus_ParamAcc", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture),
				status.ToString()
			}));
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x001066A0 File Offset: 0x00105AA0
		internal static Exception NoProviderSupportForParameters(string provider, Exception inner)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_NoProviderSupportForParameters", new object[]
			{
				provider
			}), inner);
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x001066C8 File Offset: 0x00105AC8
		internal static Exception NoProviderSupportForSProcResetParameters(string provider)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_NoProviderSupportForSProcResetParameters", new object[]
			{
				provider
			}));
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x001066F0 File Offset: 0x00105AF0
		internal static void PropsetSetFailure(StringBuilder builder, string description, OleDbPropertyStatus status)
		{
			if (status == OleDbPropertyStatus.Ok)
			{
				return;
			}
			switch (status)
			{
			case OleDbPropertyStatus.NotSupported:
				if (0 < builder.Length)
				{
					builder.Append(Environment.NewLine);
				}
				builder.Append(Res.GetString("OleDb_PropertyNotSupported", new object[]
				{
					description
				}));
				return;
			case OleDbPropertyStatus.BadValue:
				if (0 < builder.Length)
				{
					builder.Append(Environment.NewLine);
				}
				builder.Append(Res.GetString("OleDb_PropertyBadValue", new object[]
				{
					description
				}));
				return;
			case OleDbPropertyStatus.BadOption:
				if (0 < builder.Length)
				{
					builder.Append(Environment.NewLine);
				}
				builder.Append(Res.GetString("OleDb_PropertyBadOption", new object[]
				{
					description
				}));
				return;
			case OleDbPropertyStatus.BadColumn:
				if (0 < builder.Length)
				{
					builder.Append(Environment.NewLine);
				}
				builder.Append(Res.GetString("OleDb_PropertyBadColumn", new object[]
				{
					description
				}));
				return;
			case OleDbPropertyStatus.NotAllSettable:
				if (0 < builder.Length)
				{
					builder.Append(Environment.NewLine);
				}
				builder.Append(Res.GetString("OleDb_PropertyNotAllSettable", new object[]
				{
					description
				}));
				return;
			case OleDbPropertyStatus.NotSettable:
				if (0 < builder.Length)
				{
					builder.Append(Environment.NewLine);
				}
				builder.Append(Res.GetString("OleDb_PropertyNotSettable", new object[]
				{
					description
				}));
				return;
			case OleDbPropertyStatus.NotSet:
				if (0 < builder.Length)
				{
					builder.Append(Environment.NewLine);
				}
				builder.Append(Res.GetString("OleDb_PropertyNotSet", new object[]
				{
					description
				}));
				return;
			case OleDbPropertyStatus.Conflicting:
				if (0 < builder.Length)
				{
					builder.Append(Environment.NewLine);
				}
				builder.Append(Res.GetString("OleDb_PropertyConflicting", new object[]
				{
					description
				}));
				return;
			case OleDbPropertyStatus.NotAvailable:
				if (0 < builder.Length)
				{
					builder.Append(Environment.NewLine);
				}
				builder.Append(Res.GetString("OleDb_PropertyNotAvailable", new object[]
				{
					description
				}));
				return;
			default:
			{
				if (0 < builder.Length)
				{
					builder.Append(Environment.NewLine);
				}
				string name = "OleDb_PropertyStatusUnknown";
				object[] array = new object[1];
				int num = 0;
				int num2 = (int)status;
				array[num] = num2.ToString(CultureInfo.InvariantCulture);
				builder.Append(Res.GetString(name, array));
				return;
			}
			}
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x00106928 File Offset: 0x00105D28
		internal static Exception PropsetSetFailure(string value, Exception inner)
		{
			if (ADP.IsEmpty(value))
			{
				return inner;
			}
			return ADP.InvalidOperation(value, inner);
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x00106948 File Offset: 0x00105D48
		internal static ArgumentException SchemaRowsetsNotSupported(string provider)
		{
			return ADP.Argument(Res.GetString("OleDb_SchemaRowsetsNotSupported", new object[]
			{
				"IDBSchemaRowset",
				provider
			}));
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x00106978 File Offset: 0x00105D78
		internal static OleDbException NoErrorInformation(string provider, OleDbHResult hr, Exception inner)
		{
			OleDbException ex;
			if (!ADP.IsEmpty(provider))
			{
				ex = new OleDbException(Res.GetString("OleDb_NoErrorInformation2", new object[]
				{
					provider,
					ODB.ELookup(hr)
				}), hr, inner);
			}
			else
			{
				ex = new OleDbException(Res.GetString("OleDb_NoErrorInformation", new object[]
				{
					ODB.ELookup(hr)
				}), hr, inner);
			}
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x001069DC File Offset: 0x00105DDC
		internal static InvalidOperationException MDACNotAvailable(Exception inner)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_MDACNotAvailable"), inner);
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x001069FC File Offset: 0x00105DFC
		internal static ArgumentException MSDASQLNotSupported()
		{
			return ADP.Argument(Res.GetString("OleDb_MSDASQLNotSupported"));
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x00106A18 File Offset: 0x00105E18
		internal static InvalidOperationException CommandTextNotSupported(string provider, Exception inner)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_CommandTextNotSupported", new object[]
			{
				provider
			}), inner);
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x00106A40 File Offset: 0x00105E40
		internal static InvalidOperationException PossiblePromptNotUserInteractive()
		{
			return ADP.DataAdapter(Res.GetString("OleDb_PossiblePromptNotUserInteractive"));
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x00106A5C File Offset: 0x00105E5C
		internal static InvalidOperationException ProviderUnavailable(string provider, Exception inner)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_ProviderUnavailable", new object[]
			{
				provider
			}), inner);
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x00106A84 File Offset: 0x00105E84
		internal static InvalidOperationException TransactionsNotSupported(string provider, Exception inner)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_TransactionsNotSupported", new object[]
			{
				provider
			}), inner);
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x00106AAC File Offset: 0x00105EAC
		internal static ArgumentException AsynchronousNotSupported()
		{
			return ADP.Argument(Res.GetString("OleDb_AsynchronousNotSupported"));
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x00106AC8 File Offset: 0x00105EC8
		internal static ArgumentException NoProviderSpecified()
		{
			return ADP.Argument(Res.GetString("OleDb_NoProviderSpecified"));
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x00106AE4 File Offset: 0x00105EE4
		internal static ArgumentException InvalidProviderSpecified()
		{
			return ADP.Argument(Res.GetString("OleDb_InvalidProviderSpecified"));
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x00106B00 File Offset: 0x00105F00
		internal static ArgumentException InvalidRestrictionsDbInfoKeywords(string parameter)
		{
			return ADP.Argument(Res.GetString("OleDb_InvalidRestrictionsDbInfoKeywords"), parameter);
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x00106B20 File Offset: 0x00105F20
		internal static ArgumentException InvalidRestrictionsDbInfoLiteral(string parameter)
		{
			return ADP.Argument(Res.GetString("OleDb_InvalidRestrictionsDbInfoLiteral"), parameter);
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x00106B40 File Offset: 0x00105F40
		internal static ArgumentException InvalidRestrictionsSchemaGuids(string parameter)
		{
			return ADP.Argument(Res.GetString("OleDb_InvalidRestrictionsSchemaGuids"), parameter);
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x00106B60 File Offset: 0x00105F60
		internal static ArgumentException NotSupportedSchemaTable(Guid schema, OleDbConnection connection)
		{
			return ADP.Argument(Res.GetString("OleDb_NotSupportedSchemaTable", new object[]
			{
				OleDbSchemaGuid.GetTextFromValue(schema),
				connection.Provider
			}));
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x00106B94 File Offset: 0x00105F94
		internal static Exception InvalidOleDbType(OleDbType value)
		{
			return ADP.InvalidEnumerationValue(typeof(OleDbType), (int)value);
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x00106BB4 File Offset: 0x00105FB4
		internal static InvalidOperationException BadAccessor()
		{
			return ADP.DataAdapter(Res.GetString("OleDb_BadAccessor"));
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x00106BD0 File Offset: 0x00105FD0
		internal static InvalidCastException ConversionRequired()
		{
			return ADP.InvalidCast();
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x00106BE4 File Offset: 0x00105FE4
		internal static InvalidCastException CantConvertValue()
		{
			return ADP.InvalidCast(Res.GetString("OleDb_CantConvertValue"));
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x00106C00 File Offset: 0x00106000
		internal static InvalidOperationException SignMismatch(Type type)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_SignMismatch", new object[]
			{
				type.Name
			}));
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x00106C2C File Offset: 0x0010602C
		internal static InvalidOperationException DataOverflow(Type type)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_DataOverflow", new object[]
			{
				type.Name
			}));
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x00106C58 File Offset: 0x00106058
		internal static InvalidOperationException CantCreate(Type type)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_CantCreate", new object[]
			{
				type.Name
			}));
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x00106C84 File Offset: 0x00106084
		internal static InvalidOperationException Unavailable(Type type)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_Unavailable", new object[]
			{
				type.Name
			}));
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x00106CB0 File Offset: 0x001060B0
		internal static InvalidOperationException UnexpectedStatusValue(DBStatus status)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_UnexpectedStatusValue", new object[]
			{
				status.ToString()
			}));
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x00106CE4 File Offset: 0x001060E4
		internal static InvalidOperationException GVtUnknown(int wType)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_GVtUnknown", new object[]
			{
				wType.ToString("X4", CultureInfo.InvariantCulture),
				wType.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x00106D2C File Offset: 0x0010612C
		internal static InvalidOperationException SVtUnknown(int wType)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_SVtUnknown", new object[]
			{
				wType.ToString("X4", CultureInfo.InvariantCulture),
				wType.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x00106D74 File Offset: 0x00106174
		internal static InvalidOperationException BadStatusRowAccessor(int i, DBBindStatus rowStatus)
		{
			return ADP.DataAdapter(Res.GetString("OleDb_BadStatusRowAccessor", new object[]
			{
				i.ToString(CultureInfo.InvariantCulture),
				rowStatus.ToString()
			}));
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x00106DB8 File Offset: 0x001061B8
		internal static InvalidOperationException ThreadApartmentState(Exception innerException)
		{
			return ADP.InvalidOperation(Res.GetString("OleDb_ThreadApartmentState"), innerException);
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x00106DD8 File Offset: 0x001061D8
		internal static ArgumentException Fill_NotADODB(string parameter)
		{
			return ADP.Argument(Res.GetString("OleDb_Fill_NotADODB"), parameter);
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x00106DF8 File Offset: 0x001061F8
		internal static ArgumentException Fill_EmptyRecordSet(string parameter, Exception innerException)
		{
			return ADP.Argument(Res.GetString("OleDb_Fill_EmptyRecordSet", new object[]
			{
				"IRowset"
			}), parameter, innerException);
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x00106E24 File Offset: 0x00106224
		internal static ArgumentException Fill_EmptyRecord(string parameter, Exception innerException)
		{
			return ADP.Argument(Res.GetString("OleDb_Fill_EmptyRecord"), parameter, innerException);
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x00106E44 File Offset: 0x00106244
		internal static string NoErrorMessage(OleDbHResult errorcode)
		{
			return Res.GetString("OleDb_NoErrorMessage", new object[]
			{
				ODB.ELookup(errorcode)
			});
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x00106E6C File Offset: 0x0010626C
		internal static string FailedGetDescription(OleDbHResult errorcode)
		{
			return Res.GetString("OleDb_FailedGetDescription", new object[]
			{
				ODB.ELookup(errorcode)
			});
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x00106E94 File Offset: 0x00106294
		internal static string FailedGetSource(OleDbHResult errorcode)
		{
			return Res.GetString("OleDb_FailedGetSource", new object[]
			{
				ODB.ELookup(errorcode)
			});
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x00106EBC File Offset: 0x001062BC
		internal static InvalidOperationException DBBindingGetVector()
		{
			return ADP.InvalidOperation(Res.GetString("OleDb_DBBindingGetVector"));
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x00106ED8 File Offset: 0x001062D8
		internal static OleDbHResult GetErrorDescription(UnsafeNativeMethods.IErrorInfo errorInfo, OleDbHResult hresult, out string message)
		{
			Bid.Trace("<oledb.IErrorInfo.GetDescription|API|OS>\n");
			OleDbHResult description = errorInfo.GetDescription(out message);
			Bid.Trace("<oledb.IErrorInfo.GetDescription|API|OS|RET> %08X{HRESULT}, Message='%ls'\n", description, message);
			if (description < OleDbHResult.S_OK && ADP.IsEmpty(message))
			{
				message = ODB.FailedGetDescription(description) + Environment.NewLine + ODB.ELookup(hresult);
			}
			if (ADP.IsEmpty(message))
			{
				message = ODB.ELookup(hresult);
			}
			return description;
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x00106F3C File Offset: 0x0010633C
		internal static ArgumentException ISourcesRowsetNotSupported()
		{
			throw ADP.Argument("OleDb_ISourcesRowsetNotSupported");
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x00106F54 File Offset: 0x00106354
		internal static InvalidOperationException IDBInfoNotSupported()
		{
			return ADP.InvalidOperation(Res.GetString("OleDb_IDBInfoNotSupported"));
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x00106F70 File Offset: 0x00106370
		internal static string ELookup(OleDbHResult hr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(hr.ToString());
			if (0 < stringBuilder.Length && char.IsDigit(stringBuilder[0]))
			{
				stringBuilder.Length = 0;
			}
			stringBuilder.Append("(0x");
			StringBuilder stringBuilder2 = stringBuilder;
			int num = (int)hr;
			stringBuilder2.Append(num.ToString("X8", CultureInfo.InvariantCulture));
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x00106FF0 File Offset: 0x001063F0
		// Note: this type is marked as 'beforefieldinit'.
		static ODB()
		{
			char[] array = new char[3];
			array[0] = '\r';
			array[1] = '\n';
			ODB.ErrorTrimCharacters = array;
		}

		// Token: 0x040018EB RID: 6379
		internal const int ADODB_AlreadyClosedError = -2146824584;

		// Token: 0x040018EC RID: 6380
		internal const int ADODB_NextResultError = -2146825037;

		// Token: 0x040018ED RID: 6381
		internal const int InternalStateExecuting = 5;

		// Token: 0x040018EE RID: 6382
		internal const int InternalStateFetching = 9;

		// Token: 0x040018EF RID: 6383
		internal const int InternalStateClosed = 0;

		// Token: 0x040018F0 RID: 6384
		internal const int ExecutedIMultipleResults = 0;

		// Token: 0x040018F1 RID: 6385
		internal const int ExecutedIRowset = 1;

		// Token: 0x040018F2 RID: 6386
		internal const int ExecutedIRow = 2;

		// Token: 0x040018F3 RID: 6387
		internal const int PrepareICommandText = 3;

		// Token: 0x040018F4 RID: 6388
		internal const int InternalStateExecutingNot = -5;

		// Token: 0x040018F5 RID: 6389
		internal const int InternalStateFetchingNot = -9;

		// Token: 0x040018F6 RID: 6390
		internal const int InternalStateConnecting = 2;

		// Token: 0x040018F7 RID: 6391
		internal const int InternalStateOpen = 1;

		// Token: 0x040018F8 RID: 6392
		internal const int LargeDataSize = 8192;

		// Token: 0x040018F9 RID: 6393
		internal const int CacheIncrement = 10;

		// Token: 0x040018FA RID: 6394
		internal static readonly IntPtr DBRESULTFLAG_DEFAULT = IntPtr.Zero;

		// Token: 0x040018FB RID: 6395
		internal const short VARIANT_TRUE = -1;

		// Token: 0x040018FC RID: 6396
		internal const short VARIANT_FALSE = 0;

		// Token: 0x040018FD RID: 6397
		internal const int CLSCTX_ALL = 23;

		// Token: 0x040018FE RID: 6398
		internal const int MaxProgIdLength = 255;

		// Token: 0x040018FF RID: 6399
		internal const int DBLITERAL_CATALOG_SEPARATOR = 3;

		// Token: 0x04001900 RID: 6400
		internal const int DBLITERAL_QUOTE_PREFIX = 15;

		// Token: 0x04001901 RID: 6401
		internal const int DBLITERAL_QUOTE_SUFFIX = 28;

		// Token: 0x04001902 RID: 6402
		internal const int DBLITERAL_SCHEMA_SEPARATOR = 27;

		// Token: 0x04001903 RID: 6403
		internal const int DBLITERAL_TABLE_NAME = 17;

		// Token: 0x04001904 RID: 6404
		internal const int DBPROP_ACCESSORDER = 231;

		// Token: 0x04001905 RID: 6405
		internal const int DBPROP_AUTH_CACHE_AUTHINFO = 5;

		// Token: 0x04001906 RID: 6406
		internal const int DBPROP_AUTH_ENCRYPT_PASSWORD = 6;

		// Token: 0x04001907 RID: 6407
		internal const int DBPROP_AUTH_INTEGRATED = 7;

		// Token: 0x04001908 RID: 6408
		internal const int DBPROP_AUTH_MASK_PASSWORD = 8;

		// Token: 0x04001909 RID: 6409
		internal const int DBPROP_AUTH_PASSWORD = 9;

		// Token: 0x0400190A RID: 6410
		internal const int DBPROP_AUTH_PERSIST_ENCRYPTED = 10;

		// Token: 0x0400190B RID: 6411
		internal const int DBPROP_AUTH_PERSIST_SENSITIVE_AUTHINFO = 11;

		// Token: 0x0400190C RID: 6412
		internal const int DBPROP_AUTH_USERID = 12;

		// Token: 0x0400190D RID: 6413
		internal const int DBPROP_CATALOGLOCATION = 22;

		// Token: 0x0400190E RID: 6414
		internal const int DBPROP_COMMANDTIMEOUT = 34;

		// Token: 0x0400190F RID: 6415
		internal const int DBPROP_CONNECTIONSTATUS = 244;

		// Token: 0x04001910 RID: 6416
		internal const int DBPROP_CURRENTCATALOG = 37;

		// Token: 0x04001911 RID: 6417
		internal const int DBPROP_DATASOURCENAME = 38;

		// Token: 0x04001912 RID: 6418
		internal const int DBPROP_DBMSNAME = 40;

		// Token: 0x04001913 RID: 6419
		internal const int DBPROP_DBMSVER = 41;

		// Token: 0x04001914 RID: 6420
		internal const int DBPROP_GROUPBY = 44;

		// Token: 0x04001915 RID: 6421
		internal const int DBPROP_HIDDENCOLUMNS = 258;

		// Token: 0x04001916 RID: 6422
		internal const int DBPROP_IColumnsRowset = 123;

		// Token: 0x04001917 RID: 6423
		internal const int DBPROP_IDENTIFIERCASE = 46;

		// Token: 0x04001918 RID: 6424
		internal const int DBPROP_INIT_ASYNCH = 200;

		// Token: 0x04001919 RID: 6425
		internal const int DBPROP_INIT_BINDFLAGS = 270;

		// Token: 0x0400191A RID: 6426
		internal const int DBPROP_INIT_CATALOG = 233;

		// Token: 0x0400191B RID: 6427
		internal const int DBPROP_INIT_DATASOURCE = 59;

		// Token: 0x0400191C RID: 6428
		internal const int DBPROP_INIT_GENERALTIMEOUT = 284;

		// Token: 0x0400191D RID: 6429
		internal const int DBPROP_INIT_HWND = 60;

		// Token: 0x0400191E RID: 6430
		internal const int DBPROP_INIT_IMPERSONATION_LEVEL = 61;

		// Token: 0x0400191F RID: 6431
		internal const int DBPROP_INIT_LCID = 186;

		// Token: 0x04001920 RID: 6432
		internal const int DBPROP_INIT_LOCATION = 62;

		// Token: 0x04001921 RID: 6433
		internal const int DBPROP_INIT_LOCKOWNER = 271;

		// Token: 0x04001922 RID: 6434
		internal const int DBPROP_INIT_MODE = 63;

		// Token: 0x04001923 RID: 6435
		internal const int DBPROP_INIT_OLEDBSERVICES = 248;

		// Token: 0x04001924 RID: 6436
		internal const int DBPROP_INIT_PROMPT = 64;

		// Token: 0x04001925 RID: 6437
		internal const int DBPROP_INIT_PROTECTION_LEVEL = 65;

		// Token: 0x04001926 RID: 6438
		internal const int DBPROP_INIT_PROVIDERSTRING = 160;

		// Token: 0x04001927 RID: 6439
		internal const int DBPROP_INIT_TIMEOUT = 66;

		// Token: 0x04001928 RID: 6440
		internal const int DBPROP_IRow = 263;

		// Token: 0x04001929 RID: 6441
		internal const int DBPROP_MAXROWS = 73;

		// Token: 0x0400192A RID: 6442
		internal const int DBPROP_MULTIPLERESULTS = 196;

		// Token: 0x0400192B RID: 6443
		internal const int DBPROP_ORDERBYCOLUNSINSELECT = 85;

		// Token: 0x0400192C RID: 6444
		internal const int DBPROP_PROVIDERFILENAME = 96;

		// Token: 0x0400192D RID: 6445
		internal const int DBPROP_QUOTEDIDENTIFIERCASE = 100;

		// Token: 0x0400192E RID: 6446
		internal const int DBPROP_RESETDATASOURCE = 247;

		// Token: 0x0400192F RID: 6447
		internal const int DBPROP_SQLSUPPORT = 109;

		// Token: 0x04001930 RID: 6448
		internal const int DBPROP_UNIQUEROWS = 238;

		// Token: 0x04001931 RID: 6449
		internal const int DBPROPSTATUS_OK = 0;

		// Token: 0x04001932 RID: 6450
		internal const int DBPROPSTATUS_NOTSUPPORTED = 1;

		// Token: 0x04001933 RID: 6451
		internal const int DBPROPSTATUS_BADVALUE = 2;

		// Token: 0x04001934 RID: 6452
		internal const int DBPROPSTATUS_BADOPTION = 3;

		// Token: 0x04001935 RID: 6453
		internal const int DBPROPSTATUS_BADCOLUMN = 4;

		// Token: 0x04001936 RID: 6454
		internal const int DBPROPSTATUS_NOTALLSETTABLE = 5;

		// Token: 0x04001937 RID: 6455
		internal const int DBPROPSTATUS_NOTSETTABLE = 6;

		// Token: 0x04001938 RID: 6456
		internal const int DBPROPSTATUS_NOTSET = 7;

		// Token: 0x04001939 RID: 6457
		internal const int DBPROPSTATUS_CONFLICTING = 8;

		// Token: 0x0400193A RID: 6458
		internal const int DBPROPSTATUS_NOTAVAILABLE = 9;

		// Token: 0x0400193B RID: 6459
		internal const int DBPROPOPTIONS_REQUIRED = 0;

		// Token: 0x0400193C RID: 6460
		internal const int DBPROPOPTIONS_OPTIONAL = 1;

		// Token: 0x0400193D RID: 6461
		internal const int DBPROPFLAGS_WRITE = 1024;

		// Token: 0x0400193E RID: 6462
		internal const int DBPROPFLAGS_SESSION = 4096;

		// Token: 0x0400193F RID: 6463
		internal const int DBPROPVAL_AO_RANDOM = 2;

		// Token: 0x04001940 RID: 6464
		internal const int DBPROPVAL_CL_END = 2;

		// Token: 0x04001941 RID: 6465
		internal const int DBPROPVAL_CL_START = 1;

		// Token: 0x04001942 RID: 6466
		internal const int DBPROPVAL_CS_COMMUNICATIONFAILURE = 2;

		// Token: 0x04001943 RID: 6467
		internal const int DBPROPVAL_CS_INITIALIZED = 1;

		// Token: 0x04001944 RID: 6468
		internal const int DBPROPVAL_CS_UNINITIALIZED = 0;

		// Token: 0x04001945 RID: 6469
		internal const int DBPROPVAL_GB_COLLATE = 16;

		// Token: 0x04001946 RID: 6470
		internal const int DBPROPVAL_GB_CONTAINS_SELECT = 4;

		// Token: 0x04001947 RID: 6471
		internal const int DBPROPVAL_GB_EQUALS_SELECT = 2;

		// Token: 0x04001948 RID: 6472
		internal const int DBPROPVAL_GB_NO_RELATION = 8;

		// Token: 0x04001949 RID: 6473
		internal const int DBPROPVAL_GB_NOT_SUPPORTED = 1;

		// Token: 0x0400194A RID: 6474
		internal const int DBPROPVAL_IC_LOWER = 2;

		// Token: 0x0400194B RID: 6475
		internal const int DBPROPVAL_IC_MIXED = 8;

		// Token: 0x0400194C RID: 6476
		internal const int DBPROPVAL_IC_SENSITIVE = 4;

		// Token: 0x0400194D RID: 6477
		internal const int DBPROPVAL_IC_UPPER = 1;

		// Token: 0x0400194E RID: 6478
		internal const int DBPROPVAL_IN_ALLOWNULL = 0;

		// Token: 0x0400194F RID: 6479
		internal const int DBPROPVAL_MR_NOTSUPPORTED = 0;

		// Token: 0x04001950 RID: 6480
		internal const int DBPROPVAL_RD_RESETALL = -1;

		// Token: 0x04001951 RID: 6481
		internal const int DBPROPVAL_OS_RESOURCEPOOLING = 1;

		// Token: 0x04001952 RID: 6482
		internal const int DBPROPVAL_OS_TXNENLISTMENT = 2;

		// Token: 0x04001953 RID: 6483
		internal const int DBPROPVAL_OS_CLIENTCURSOR = 4;

		// Token: 0x04001954 RID: 6484
		internal const int DBPROPVAL_OS_AGR_AFTERSESSION = 8;

		// Token: 0x04001955 RID: 6485
		internal const int DBPROPVAL_SQL_ODBC_MINIMUM = 1;

		// Token: 0x04001956 RID: 6486
		internal const int DBPROPVAL_SQL_ESCAPECLAUSES = 256;

		// Token: 0x04001957 RID: 6487
		internal const int DBKIND_GUID_NAME = 0;

		// Token: 0x04001958 RID: 6488
		internal const int DBKIND_GUID_PROPID = 1;

		// Token: 0x04001959 RID: 6489
		internal const int DBKIND_NAME = 2;

		// Token: 0x0400195A RID: 6490
		internal const int DBKIND_PGUID_NAME = 3;

		// Token: 0x0400195B RID: 6491
		internal const int DBKIND_PGUID_PROPID = 4;

		// Token: 0x0400195C RID: 6492
		internal const int DBKIND_PROPID = 5;

		// Token: 0x0400195D RID: 6493
		internal const int DBKIND_GUID = 6;

		// Token: 0x0400195E RID: 6494
		internal const int DBCOLUMNFLAGS_ISBOOKMARK = 1;

		// Token: 0x0400195F RID: 6495
		internal const int DBCOLUMNFLAGS_ISLONG = 128;

		// Token: 0x04001960 RID: 6496
		internal const int DBCOLUMNFLAGS_ISFIXEDLENGTH = 16;

		// Token: 0x04001961 RID: 6497
		internal const int DBCOLUMNFLAGS_ISNULLABLE = 32;

		// Token: 0x04001962 RID: 6498
		internal const int DBCOLUMNFLAGS_ISROWSET = 1048576;

		// Token: 0x04001963 RID: 6499
		internal const int DBCOLUMNFLAGS_ISROW = 2097152;

		// Token: 0x04001964 RID: 6500
		internal const int DBCOLUMNFLAGS_ISROWSET_DBCOLUMNFLAGS_ISROW = 3145728;

		// Token: 0x04001965 RID: 6501
		internal const int DBCOLUMNFLAGS_ISLONG_DBCOLUMNFLAGS_ISSTREAM = 524416;

		// Token: 0x04001966 RID: 6502
		internal const int DBCOLUMNFLAGS_ISROWID_DBCOLUMNFLAGS_ISROWVER = 768;

		// Token: 0x04001967 RID: 6503
		internal const int DBCOLUMNFLAGS_WRITE_DBCOLUMNFLAGS_WRITEUNKNOWN = 12;

		// Token: 0x04001968 RID: 6504
		internal const int DBCOLUMNFLAGS_ISNULLABLE_DBCOLUMNFLAGS_MAYBENULL = 96;

		// Token: 0x04001969 RID: 6505
		internal const int DBACCESSOR_ROWDATA = 2;

		// Token: 0x0400196A RID: 6506
		internal const int DBACCESSOR_PARAMETERDATA = 4;

		// Token: 0x0400196B RID: 6507
		internal const int DBPARAMTYPE_INPUT = 1;

		// Token: 0x0400196C RID: 6508
		internal const int DBPARAMTYPE_INPUTOUTPUT = 2;

		// Token: 0x0400196D RID: 6509
		internal const int DBPARAMTYPE_OUTPUT = 3;

		// Token: 0x0400196E RID: 6510
		internal const int DBPARAMTYPE_RETURNVALUE = 4;

		// Token: 0x0400196F RID: 6511
		internal const int ParameterDirectionFlag = 3;

		// Token: 0x04001970 RID: 6512
		internal const uint DB_UNSEARCHABLE = 1U;

		// Token: 0x04001971 RID: 6513
		internal const uint DB_LIKE_ONLY = 2U;

		// Token: 0x04001972 RID: 6514
		internal const uint DB_ALL_EXCEPT_LIKE = 3U;

		// Token: 0x04001973 RID: 6515
		internal const uint DB_SEARCHABLE = 4U;

		// Token: 0x04001974 RID: 6516
		internal static readonly IntPtr DB_INVALID_HACCESSOR = ADP.PtrZero;

		// Token: 0x04001975 RID: 6517
		internal static readonly IntPtr DB_NULL_HCHAPTER = ADP.PtrZero;

		// Token: 0x04001976 RID: 6518
		internal static readonly IntPtr DB_NULL_HROW = ADP.PtrZero;

		// Token: 0x04001977 RID: 6519
		internal static readonly int SizeOf_tagDBBINDING = Marshal.SizeOf(typeof(tagDBBINDING));

		// Token: 0x04001978 RID: 6520
		internal static readonly int SizeOf_tagDBCOLUMNINFO = Marshal.SizeOf(typeof(tagDBCOLUMNINFO));

		// Token: 0x04001979 RID: 6521
		internal static readonly int SizeOf_tagDBLITERALINFO = Marshal.SizeOf(typeof(tagDBLITERALINFO));

		// Token: 0x0400197A RID: 6522
		internal static readonly int SizeOf_tagDBPROPSET = Marshal.SizeOf(typeof(tagDBPROPSET));

		// Token: 0x0400197B RID: 6523
		internal static readonly int SizeOf_tagDBPROP = Marshal.SizeOf(typeof(tagDBPROP));

		// Token: 0x0400197C RID: 6524
		internal static readonly int SizeOf_tagDBPROPINFOSET = Marshal.SizeOf(typeof(tagDBPROPINFOSET));

		// Token: 0x0400197D RID: 6525
		internal static readonly int SizeOf_tagDBPROPINFO = Marshal.SizeOf(typeof(tagDBPROPINFO));

		// Token: 0x0400197E RID: 6526
		internal static readonly int SizeOf_tagDBPROPIDSET = Marshal.SizeOf(typeof(tagDBPROPIDSET));

		// Token: 0x0400197F RID: 6527
		internal static readonly int SizeOf_Guid = Marshal.SizeOf(typeof(Guid));

		// Token: 0x04001980 RID: 6528
		internal static readonly int SizeOf_Variant = 8 + 2 * ADP.PtrSize;

		// Token: 0x04001981 RID: 6529
		internal static readonly int OffsetOf_tagDBPROP_Status = Marshal.OffsetOf(typeof(tagDBPROP), "dwStatus").ToInt32();

		// Token: 0x04001982 RID: 6530
		internal static readonly int OffsetOf_tagDBPROP_Value = Marshal.OffsetOf(typeof(tagDBPROP), "vValue").ToInt32();

		// Token: 0x04001983 RID: 6531
		internal static readonly int OffsetOf_tagDBPROPSET_Properties = Marshal.OffsetOf(typeof(tagDBPROPSET), "rgProperties").ToInt32();

		// Token: 0x04001984 RID: 6532
		internal static readonly int OffsetOf_tagDBPROPINFO_Value = Marshal.OffsetOf(typeof(tagDBPROPINFO), "vValue").ToInt32();

		// Token: 0x04001985 RID: 6533
		internal static readonly int OffsetOf_tagDBPROPIDSET_PropertySet = Marshal.OffsetOf(typeof(tagDBPROPIDSET), "guidPropertySet").ToInt32();

		// Token: 0x04001986 RID: 6534
		internal static readonly int OffsetOf_tagDBLITERALINFO_it = Marshal.OffsetOf(typeof(tagDBLITERALINFO), "it").ToInt32();

		// Token: 0x04001987 RID: 6535
		internal static readonly int OffsetOf_tagDBBINDING_obValue = Marshal.OffsetOf(typeof(tagDBBINDING), "obValue").ToInt32();

		// Token: 0x04001988 RID: 6536
		internal static readonly int OffsetOf_tagDBBINDING_wType = Marshal.OffsetOf(typeof(tagDBBINDING), "wType").ToInt32();

		// Token: 0x04001989 RID: 6537
		internal static Guid IID_NULL = Guid.Empty;

		// Token: 0x0400198A RID: 6538
		internal static Guid IID_IUnknown = new Guid(0, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);

		// Token: 0x0400198B RID: 6539
		internal static Guid IID_IDBInitialize = new Guid(208878219, 10780, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x0400198C RID: 6540
		internal static Guid IID_IDBCreateSession = new Guid(208878173, 10780, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x0400198D RID: 6541
		internal static Guid IID_IDBCreateCommand = new Guid(208878109, 10780, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x0400198E RID: 6542
		internal static Guid IID_ICommandText = new Guid(208878119, 10780, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x0400198F RID: 6543
		internal static Guid IID_IMultipleResults = new Guid(208878224, 10780, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x04001990 RID: 6544
		internal static Guid IID_IRow = new Guid(208878260, 10780, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x04001991 RID: 6545
		internal static Guid IID_IRowset = new Guid(208878204, 10780, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x04001992 RID: 6546
		internal static Guid IID_ISQLErrorInfo = new Guid(208878196, 10780, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x04001993 RID: 6547
		internal static Guid CLSID_DataLinks = new Guid(570871218, 6593, 4561, 137, 224, 0, 192, 79, 215, 168, 41);

		// Token: 0x04001994 RID: 6548
		internal static Guid DBGUID_DEFAULT = new Guid(3367313915U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x04001995 RID: 6549
		internal static Guid DBGUID_ROWSET = new Guid(3367314166U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x04001996 RID: 6550
		internal static Guid DBGUID_ROW = new Guid(3367314167U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x04001997 RID: 6551
		internal static Guid DBGUID_ROWDEFAULTSTREAM = new Guid(208878263, 10780, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x04001998 RID: 6552
		internal static readonly Guid CLSID_MSDASQL = new Guid(3367314123U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x04001999 RID: 6553
		internal static readonly object DBCOL_SPECIALCOL = new Guid(3367313970U, 23795, 4558, 173, 229, 0, 170, 0, 68, 119, 61);

		// Token: 0x0400199A RID: 6554
		internal static readonly char[] ErrorTrimCharacters;

		// Token: 0x0400199B RID: 6555
		internal const string Asynchronous_Processing = "asynchronous processing";

		// Token: 0x0400199C RID: 6556
		internal const string AttachDBFileName = "attachdbfilename";

		// Token: 0x0400199D RID: 6557
		internal const string Connect_Timeout = "connect timeout";

		// Token: 0x0400199E RID: 6558
		internal const string Data_Source = "data source";

		// Token: 0x0400199F RID: 6559
		internal const string File_Name = "file name";

		// Token: 0x040019A0 RID: 6560
		internal const string Initial_Catalog = "initial catalog";

		// Token: 0x040019A1 RID: 6561
		internal const string Password = "password";

		// Token: 0x040019A2 RID: 6562
		internal const string Persist_Security_Info = "persist security info";

		// Token: 0x040019A3 RID: 6563
		internal const string Provider = "provider";

		// Token: 0x040019A4 RID: 6564
		internal const string Pwd = "pwd";

		// Token: 0x040019A5 RID: 6565
		internal const string User_ID = "user id";

		// Token: 0x040019A6 RID: 6566
		internal const string Current_Catalog = "current catalog";

		// Token: 0x040019A7 RID: 6567
		internal const string DBMS_Version = "dbms version";

		// Token: 0x040019A8 RID: 6568
		internal const string Properties = "Properties";

		// Token: 0x040019A9 RID: 6569
		internal const string DataLinks_CLSID = "CLSID\\{2206CDB2-19C1-11D1-89E0-00C04FD7A829}\\InprocServer32";

		// Token: 0x040019AA RID: 6570
		internal const string OLEDB_SERVICES = "OLEDB_SERVICES";

		// Token: 0x040019AB RID: 6571
		internal const string DefaultDescription_MSDASQL = "microsoft ole db provider for odbc drivers";

		// Token: 0x040019AC RID: 6572
		internal const string MSDASQL = "msdasql";

		// Token: 0x040019AD RID: 6573
		internal const string MSDASQLdot = "msdasql.";

		// Token: 0x040019AE RID: 6574
		internal const string _Add = "add";

		// Token: 0x040019AF RID: 6575
		internal const string _Keyword = "keyword";

		// Token: 0x040019B0 RID: 6576
		internal const string _Name = "name";

		// Token: 0x040019B1 RID: 6577
		internal const string _Value = "value";

		// Token: 0x040019B2 RID: 6578
		internal const string DBCOLUMN_BASECATALOGNAME = "DBCOLUMN_BASECATALOGNAME";

		// Token: 0x040019B3 RID: 6579
		internal const string DBCOLUMN_BASECOLUMNNAME = "DBCOLUMN_BASECOLUMNNAME";

		// Token: 0x040019B4 RID: 6580
		internal const string DBCOLUMN_BASESCHEMANAME = "DBCOLUMN_BASESCHEMANAME";

		// Token: 0x040019B5 RID: 6581
		internal const string DBCOLUMN_BASETABLENAME = "DBCOLUMN_BASETABLENAME";

		// Token: 0x040019B6 RID: 6582
		internal const string DBCOLUMN_COLUMNSIZE = "DBCOLUMN_COLUMNSIZE";

		// Token: 0x040019B7 RID: 6583
		internal const string DBCOLUMN_FLAGS = "DBCOLUMN_FLAGS";

		// Token: 0x040019B8 RID: 6584
		internal const string DBCOLUMN_GUID = "DBCOLUMN_GUID";

		// Token: 0x040019B9 RID: 6585
		internal const string DBCOLUMN_IDNAME = "DBCOLUMN_IDNAME";

		// Token: 0x040019BA RID: 6586
		internal const string DBCOLUMN_ISAUTOINCREMENT = "DBCOLUMN_ISAUTOINCREMENT";

		// Token: 0x040019BB RID: 6587
		internal const string DBCOLUMN_ISUNIQUE = "DBCOLUMN_ISUNIQUE";

		// Token: 0x040019BC RID: 6588
		internal const string DBCOLUMN_KEYCOLUMN = "DBCOLUMN_KEYCOLUMN";

		// Token: 0x040019BD RID: 6589
		internal const string DBCOLUMN_NAME = "DBCOLUMN_NAME";

		// Token: 0x040019BE RID: 6590
		internal const string DBCOLUMN_NUMBER = "DBCOLUMN_NUMBER";

		// Token: 0x040019BF RID: 6591
		internal const string DBCOLUMN_PRECISION = "DBCOLUMN_PRECISION";

		// Token: 0x040019C0 RID: 6592
		internal const string DBCOLUMN_PROPID = "DBCOLUMN_PROPID";

		// Token: 0x040019C1 RID: 6593
		internal const string DBCOLUMN_SCALE = "DBCOLUMN_SCALE";

		// Token: 0x040019C2 RID: 6594
		internal const string DBCOLUMN_TYPE = "DBCOLUMN_TYPE";

		// Token: 0x040019C3 RID: 6595
		internal const string DBCOLUMN_TYPEINFO = "DBCOLUMN_TYPEINFO";

		// Token: 0x040019C4 RID: 6596
		internal const string PRIMARY_KEY = "PRIMARY_KEY";

		// Token: 0x040019C5 RID: 6597
		internal const string UNIQUE = "UNIQUE";

		// Token: 0x040019C6 RID: 6598
		internal const string COLUMN_NAME = "COLUMN_NAME";

		// Token: 0x040019C7 RID: 6599
		internal const string NULLS = "NULLS";

		// Token: 0x040019C8 RID: 6600
		internal const string INDEX_NAME = "INDEX_NAME";

		// Token: 0x040019C9 RID: 6601
		internal const string PARAMETER_NAME = "PARAMETER_NAME";

		// Token: 0x040019CA RID: 6602
		internal const string ORDINAL_POSITION = "ORDINAL_POSITION";

		// Token: 0x040019CB RID: 6603
		internal const string PARAMETER_TYPE = "PARAMETER_TYPE";

		// Token: 0x040019CC RID: 6604
		internal const string IS_NULLABLE = "IS_NULLABLE";

		// Token: 0x040019CD RID: 6605
		internal const string DATA_TYPE = "DATA_TYPE";

		// Token: 0x040019CE RID: 6606
		internal const string CHARACTER_MAXIMUM_LENGTH = "CHARACTER_MAXIMUM_LENGTH";

		// Token: 0x040019CF RID: 6607
		internal const string NUMERIC_PRECISION = "NUMERIC_PRECISION";

		// Token: 0x040019D0 RID: 6608
		internal const string NUMERIC_SCALE = "NUMERIC_SCALE";

		// Token: 0x040019D1 RID: 6609
		internal const string TYPE_NAME = "TYPE_NAME";

		// Token: 0x040019D2 RID: 6610
		internal const string ORDINAL_POSITION_ASC = "ORDINAL_POSITION ASC";

		// Token: 0x040019D3 RID: 6611
		internal const string SchemaGuids = "SchemaGuids";

		// Token: 0x040019D4 RID: 6612
		internal const string Schema = "Schema";

		// Token: 0x040019D5 RID: 6613
		internal const string RestrictionSupport = "RestrictionSupport";

		// Token: 0x040019D6 RID: 6614
		internal const string DbInfoKeywords = "DbInfoKeywords";

		// Token: 0x040019D7 RID: 6615
		internal const string Keyword = "Keyword";
	}
}
