using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Properties;

namespace System.Web.Http.Tracing
{
	// Token: 0x02000154 RID: 340
	public static class ITraceWriterExtensions
	{
		// Token: 0x06000873 RID: 2163 RVA: 0x0001B113 File Offset: 0x00019313
		public static void Debug(this ITraceWriter traceWriter, HttpRequestMessage request, string category, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Debug, messageFormat, messageArguments);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001B121 File Offset: 0x00019321
		public static void Debug(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception)
		{
			traceWriter.Trace(request, category, TraceLevel.Debug, exception);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001B12D File Offset: 0x0001932D
		public static void Debug(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Debug, exception, messageFormat, messageArguments);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001B13D File Offset: 0x0001933D
		public static void Error(this ITraceWriter traceWriter, HttpRequestMessage request, string category, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Error, messageFormat, messageArguments);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001B14B File Offset: 0x0001934B
		public static void Error(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception)
		{
			traceWriter.Trace(request, category, TraceLevel.Error, exception);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001B157 File Offset: 0x00019357
		public static void Error(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Error, exception, messageFormat, messageArguments);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001B167 File Offset: 0x00019367
		public static void Fatal(this ITraceWriter traceWriter, HttpRequestMessage request, string category, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Fatal, messageFormat, messageArguments);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001B175 File Offset: 0x00019375
		public static void Fatal(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception)
		{
			traceWriter.Trace(request, category, TraceLevel.Fatal, exception);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001B181 File Offset: 0x00019381
		public static void Fatal(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Fatal, exception, messageFormat, messageArguments);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001B191 File Offset: 0x00019391
		public static void Info(this ITraceWriter traceWriter, HttpRequestMessage request, string category, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Info, messageFormat, messageArguments);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001B19F File Offset: 0x0001939F
		public static void Info(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception)
		{
			traceWriter.Trace(request, category, TraceLevel.Info, exception);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001B1AB File Offset: 0x000193AB
		public static void Info(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Info, exception, messageFormat, messageArguments);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001B1D4 File Offset: 0x000193D4
		public static void Trace(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, Exception exception)
		{
			if (traceWriter == null)
			{
				throw System.Web.Http.Error.ArgumentNull("traceWriter");
			}
			if (exception == null)
			{
				throw System.Web.Http.Error.ArgumentNull("exception");
			}
			traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
			{
				traceRecord.Exception = exception;
			});
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001B254 File Offset: 0x00019454
		public static void Trace(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, Exception exception, string messageFormat, params object[] messageArguments)
		{
			if (traceWriter == null)
			{
				throw System.Web.Http.Error.ArgumentNull("traceWriter");
			}
			if (exception == null)
			{
				throw System.Web.Http.Error.ArgumentNull("exception");
			}
			if (messageFormat == null)
			{
				throw System.Web.Http.Error.ArgumentNull("messageFormat");
			}
			traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
			{
				traceRecord.Exception = exception;
				traceRecord.Message = System.Web.Http.Error.Format(messageFormat, messageArguments);
			});
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001B2EC File Offset: 0x000194EC
		public static void Trace(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, string messageFormat, params object[] messageArguments)
		{
			if (traceWriter == null)
			{
				throw System.Web.Http.Error.ArgumentNull("traceWriter");
			}
			if (messageFormat == null)
			{
				throw System.Web.Http.Error.ArgumentNull("messageFormat");
			}
			traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
			{
				traceRecord.Message = System.Web.Http.Error.Format(messageFormat, messageArguments);
			});
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001B3B8 File Offset: 0x000195B8
		public static void TraceBeginEnd(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, string operatorName, string operationName, Action<TraceRecord> beginTrace, Action execute, Action<TraceRecord> endTrace, Action<TraceRecord> errorTrace)
		{
			if (traceWriter == null)
			{
				throw System.Web.Http.Error.ArgumentNull("traceWriter");
			}
			if (execute == null)
			{
				throw System.Web.Http.Error.ArgumentNull("execute");
			}
			traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
			{
				traceRecord.Kind = TraceKind.Begin;
				traceRecord.Operator = operatorName;
				traceRecord.Operation = operationName;
				if (beginTrace != null)
				{
					beginTrace(traceRecord);
				}
			});
			try
			{
				execute();
				traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
				{
					traceRecord.Kind = TraceKind.End;
					traceRecord.Operator = operatorName;
					traceRecord.Operation = operationName;
					if (endTrace != null)
					{
						endTrace(traceRecord);
					}
				});
			}
			catch (Exception exception)
			{
				traceWriter.TraceError(exception, request, category, operatorName, operationName, errorTrace);
				throw;
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001B4AC File Offset: 0x000196AC
		public static Task<TResult> TraceBeginEndAsync<TResult>(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, string operatorName, string operationName, Action<TraceRecord> beginTrace, Func<Task<TResult>> execute, Action<TraceRecord, TResult> endTrace, Action<TraceRecord> errorTrace)
		{
			if (traceWriter == null)
			{
				throw System.Web.Http.Error.ArgumentNull("traceWriter");
			}
			if (execute == null)
			{
				throw System.Web.Http.Error.ArgumentNull("execute");
			}
			traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
			{
				traceRecord.Kind = TraceKind.Begin;
				traceRecord.Operator = operatorName;
				traceRecord.Operation = operationName;
				if (beginTrace != null)
				{
					beginTrace(traceRecord);
				}
			});
			Task<TResult> result;
			try
			{
				Task<TResult> task = execute();
				if (task == null)
				{
					result = task;
				}
				else
				{
					result = traceWriter.TraceBeginEndAsyncCore(request, category, level, operatorName, operationName, endTrace, errorTrace, task);
				}
			}
			catch (Exception exception)
			{
				traceWriter.TraceError(exception, request, category, operatorName, operationName, errorTrace);
				throw;
			}
			return result;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001B87C File Offset: 0x00019A7C
		private static async Task<TResult> TraceBeginEndAsyncCore<TResult>(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, string operatorName, string operationName, Action<TraceRecord, TResult> endTrace, Action<TraceRecord> errorTrace, Task<TResult> task)
		{
			TResult result2;
			try
			{
				TResult result = await task;
				traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
				{
					traceRecord.Kind = TraceKind.End;
					traceRecord.Operator = operatorName;
					traceRecord.Operation = operationName;
					if (endTrace != null)
					{
						endTrace(traceRecord, result);
					}
				});
				result2 = result;
			}
			catch (OperationCanceledException)
			{
				traceWriter.Trace(request, category, TraceLevel.Warn, delegate(TraceRecord traceRecord)
				{
					traceRecord.Kind = TraceKind.End;
					traceRecord.Operator = operatorName;
					traceRecord.Operation = operationName;
					traceRecord.Message = SRResources.TraceCancelledMessage;
					if (errorTrace != null)
					{
						errorTrace(traceRecord);
					}
				});
				throw;
			}
			catch (Exception exception)
			{
				traceWriter.TraceError(exception, request, category, operatorName, operationName, errorTrace);
				throw;
			}
			return result2;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001B944 File Offset: 0x00019B44
		public static Task TraceBeginEndAsync(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, string operatorName, string operationName, Action<TraceRecord> beginTrace, Func<Task> execute, Action<TraceRecord> endTrace, Action<TraceRecord> errorTrace)
		{
			if (traceWriter == null)
			{
				throw System.Web.Http.Error.ArgumentNull("traceWriter");
			}
			if (execute == null)
			{
				throw System.Web.Http.Error.ArgumentNull("execute");
			}
			traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
			{
				traceRecord.Kind = TraceKind.Begin;
				traceRecord.Operator = operatorName;
				traceRecord.Operation = operationName;
				if (beginTrace != null)
				{
					beginTrace(traceRecord);
				}
			});
			Task result;
			try
			{
				Task task = execute();
				if (task == null)
				{
					result = task;
				}
				else
				{
					result = traceWriter.TraceBeginEndAsyncCore(request, category, level, operatorName, operationName, endTrace, errorTrace, task);
				}
			}
			catch (Exception exception)
			{
				traceWriter.TraceError(exception, request, category, operatorName, operationName, errorTrace);
				throw;
			}
			return result;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001BCAC File Offset: 0x00019EAC
		private static async Task TraceBeginEndAsyncCore(this ITraceWriter traceWriter, HttpRequestMessage request, string category, TraceLevel level, string operatorName, string operationName, Action<TraceRecord> endTrace, Action<TraceRecord> errorTrace, Task task)
		{
			try
			{
				await task;
				traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
				{
					traceRecord.Kind = TraceKind.End;
					traceRecord.Operator = operatorName;
					traceRecord.Operation = operationName;
					if (endTrace != null)
					{
						endTrace(traceRecord);
					}
				});
			}
			catch (OperationCanceledException)
			{
				traceWriter.Trace(request, category, TraceLevel.Warn, delegate(TraceRecord traceRecord)
				{
					traceRecord.Kind = TraceKind.End;
					traceRecord.Operator = operatorName;
					traceRecord.Operation = operationName;
					traceRecord.Message = SRResources.TraceCancelledMessage;
					if (errorTrace != null)
					{
						errorTrace(traceRecord);
					}
				});
				throw;
			}
			catch (Exception exception)
			{
				traceWriter.TraceError(exception, request, category, operatorName, operationName, errorTrace);
				throw;
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001BD37 File Offset: 0x00019F37
		public static void Warn(this ITraceWriter traceWriter, HttpRequestMessage request, string category, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Warn, messageFormat, messageArguments);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001BD45 File Offset: 0x00019F45
		public static void Warn(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception)
		{
			traceWriter.Trace(request, category, TraceLevel.Warn, exception);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001BD51 File Offset: 0x00019F51
		public static void Warn(this ITraceWriter traceWriter, HttpRequestMessage request, string category, Exception exception, string messageFormat, params object[] messageArguments)
		{
			traceWriter.Trace(request, category, TraceLevel.Warn, exception, messageFormat, messageArguments);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001BDC0 File Offset: 0x00019FC0
		private static void TraceError(this ITraceWriter traceWriter, Exception exception, HttpRequestMessage request, string category, string operatorName, string operationName, Action<TraceRecord> errorTrace)
		{
			TraceLevel level = TraceWriterExceptionMapper.GetMappedTraceLevel(exception) ?? TraceLevel.Error;
			traceWriter.Trace(request, category, level, delegate(TraceRecord traceRecord)
			{
				traceRecord.Kind = TraceKind.End;
				traceRecord.Operator = operatorName;
				traceRecord.Operation = operationName;
				traceRecord.Exception = exception;
				TraceWriterExceptionMapper.TranslateHttpResponseException(traceRecord);
				if (errorTrace != null)
				{
					errorTrace(traceRecord);
				}
			});
		}
	}
}
