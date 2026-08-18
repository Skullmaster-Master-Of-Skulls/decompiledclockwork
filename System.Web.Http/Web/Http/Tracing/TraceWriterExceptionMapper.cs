using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace System.Web.Http.Tracing
{
	// Token: 0x0200001B RID: 27
	internal static class TraceWriterExceptionMapper
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00004258 File Offset: 0x00002458
		public static void TranslateHttpResponseException(TraceRecord traceRecord)
		{
			HttpResponseException ex = TraceWriterExceptionMapper.ExtractHttpResponseException(traceRecord.Exception);
			if (ex == null)
			{
				return;
			}
			HttpResponseMessage response = ex.Response;
			if (traceRecord.Status == (HttpStatusCode)0)
			{
				traceRecord.Status = response.StatusCode;
			}
			traceRecord.Level = (TraceWriterExceptionMapper.GetMappedTraceLevel(ex) ?? traceRecord.Level);
			ObjectContent objectContent = response.Content as ObjectContent;
			if (objectContent == null)
			{
				return;
			}
			HttpError httpError = objectContent.Value as HttpError;
			if (httpError == null)
			{
				return;
			}
			object obj = null;
			object obj2 = null;
			List<string> list = new List<string>();
			if (httpError.TryGetValue(HttpErrorKeys.MessageKey, out obj))
			{
				list.Add(Error.Format("UserMessage='{0}'", new object[]
				{
					obj
				}));
			}
			if (httpError.TryGetValue(HttpErrorKeys.MessageDetailKey, out obj2))
			{
				list.Add(Error.Format("MessageDetail='{0}'", new object[]
				{
					obj2
				}));
			}
			TraceWriterExceptionMapper.AddExceptions(httpError, list);
			object obj3 = null;
			if (httpError.TryGetValue(HttpErrorKeys.ModelStateKey, out obj3))
			{
				HttpError httpError2 = obj3 as HttpError;
				if (httpError2 != null)
				{
					list.Add(TraceWriterExceptionMapper.FormatModelStateErrors(httpError2));
				}
			}
			traceRecord.Message = string.Join(", ", list);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000438C File Offset: 0x0000258C
		public static TraceLevel? GetMappedTraceLevel(Exception exception)
		{
			HttpResponseException ex = TraceWriterExceptionMapper.ExtractHttpResponseException(exception);
			if (ex == null)
			{
				return null;
			}
			return TraceWriterExceptionMapper.GetMappedTraceLevel(ex);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000043B4 File Offset: 0x000025B4
		public static TraceLevel? GetMappedTraceLevel(HttpResponseException httpResponseException)
		{
			HttpResponseMessage response = httpResponseException.Response;
			TraceLevel? result = null;
			if (response.StatusCode < HttpStatusCode.InternalServerError)
			{
				result = new TraceLevel?(TraceLevel.Warn);
			}
			if (response.StatusCode < HttpStatusCode.BadRequest)
			{
				result = new TraceLevel?(TraceLevel.Info);
			}
			return result;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000441C File Offset: 0x0000261C
		private static HttpResponseException ExtractHttpResponseException(Exception exception)
		{
			if (exception == null)
			{
				return null;
			}
			HttpResponseException ex3 = exception as HttpResponseException;
			if (ex3 != null)
			{
				return ex3;
			}
			AggregateException ex2 = exception as AggregateException;
			if (ex2 != null)
			{
				return (from ex in ex2.Flatten().InnerExceptions.Select(new Func<Exception, HttpResponseException>(TraceWriterExceptionMapper.ExtractHttpResponseException))
				where ex != null && ex.Response != null
				orderby ex.Response.StatusCode descending
				select ex).FirstOrDefault<HttpResponseException>();
			}
			return TraceWriterExceptionMapper.ExtractHttpResponseException(exception.InnerException);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000044B8 File Offset: 0x000026B8
		private static void AddExceptions(HttpError httpError, List<string> messages)
		{
			object obj = null;
			object obj2 = null;
			object obj3 = null;
			object obj4 = null;
			int num = 0;
			while (httpError != null)
			{
				string text = (num == 0) ? string.Empty : Error.Format("[{0}]", new object[]
				{
					num
				});
				if (httpError.TryGetValue(HttpErrorKeys.ExceptionTypeKey, out obj2))
				{
					messages.Add(Error.Format("ExceptionType{0}='{1}'", new object[]
					{
						text,
						obj2
					}));
				}
				if (httpError.TryGetValue(HttpErrorKeys.ExceptionMessageKey, out obj))
				{
					messages.Add(Error.Format("ExceptionMessage{0}='{1}'", new object[]
					{
						text,
						obj
					}));
				}
				if (httpError.TryGetValue(HttpErrorKeys.StackTraceKey, out obj3))
				{
					messages.Add(Error.Format("StackTrace{0}={1}", new object[]
					{
						text,
						obj3
					}));
				}
				if (!httpError.TryGetValue(HttpErrorKeys.InnerExceptionKey, out obj4))
				{
					return;
				}
				httpError = (obj4 as HttpError);
				num++;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000045C4 File Offset: 0x000027C4
		private static string FormatModelStateErrors(HttpError modelStateError)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, object> keyValuePair in modelStateError)
			{
				IEnumerable<string> enumerable = keyValuePair.Value as IEnumerable<string>;
				if (enumerable != null)
				{
					list.Add(Error.Format("{0}=[{1}]", new object[]
					{
						keyValuePair.Key,
						string.Join(", ", enumerable)
					}));
				}
			}
			return Error.Format("ModelStateError=[{0}]", new object[]
			{
				string.Join(", ", list)
			});
		}

		// Token: 0x04000028 RID: 40
		private const string HttpErrorExceptionMessageFormat = "ExceptionMessage{0}='{1}'";

		// Token: 0x04000029 RID: 41
		private const string HttpErrorExceptionTypeFormat = "ExceptionType{0}='{1}'";

		// Token: 0x0400002A RID: 42
		private const string HttpErrorMessageDetailFormat = "MessageDetail='{0}'";

		// Token: 0x0400002B RID: 43
		private const string HttpErrorModelStateErrorFormat = "ModelStateError=[{0}]";

		// Token: 0x0400002C RID: 44
		private const string HttpErrorModelStatePairFormat = "{0}=[{1}]";

		// Token: 0x0400002D RID: 45
		private const string HttpErrorStackTraceFormat = "StackTrace{0}={1}";

		// Token: 0x0400002E RID: 46
		private const string HttpErrorUserMessageFormat = "UserMessage='{0}'";
	}
}
