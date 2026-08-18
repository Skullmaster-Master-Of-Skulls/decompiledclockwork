using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.ServiceModel.Activation;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000863 RID: 2147
	internal static class HttpChannelUtilities
	{
		// Token: 0x060050BE RID: 20670 RVA: 0x00128B14 File Offset: 0x00126D14
		public static Exception CreateCommunicationException(HttpListenerException listenerException)
		{
			int nativeErrorCode = listenerException.NativeErrorCode;
			if (nativeErrorCode <= 14)
			{
				if (nativeErrorCode == 6)
				{
					return new CommunicationObjectAbortedException(SR.GetString("HttpResponseAborted"), listenerException);
				}
				if (nativeErrorCode != 8 && nativeErrorCode != 14)
				{
					goto IL_94;
				}
			}
			else
			{
				if (nativeErrorCode == 64)
				{
					return new CommunicationException(SR.GetString("HttpNetnameDeleted", new object[]
					{
						listenerException.Message
					}), listenerException);
				}
				if (nativeErrorCode == 1172)
				{
					return new CommunicationException(SR.GetString("HttpNoTrackingService", new object[]
					{
						listenerException.Message
					}), listenerException);
				}
				if (nativeErrorCode != 1450)
				{
					goto IL_94;
				}
			}
			return new InsufficientMemoryException(SR.GetString("InsufficentMemory"), listenerException);
			IL_94:
			return new CommunicationException(listenerException.Message, listenerException);
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x00128BC1 File Offset: 0x00126DC1
		public static void EnsureHttpRequestMessageContentNotNull(HttpRequestMessage httpRequestMessage)
		{
			if (httpRequestMessage.Content == null)
			{
				httpRequestMessage.Content = new ByteArrayContent(EmptyArray<byte>.Instance);
			}
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x00128BDB File Offset: 0x00126DDB
		public static void EnsureHttpResponseMessageContentNotNull(HttpResponseMessage httpResponseMessage)
		{
			if (httpResponseMessage.Content == null)
			{
				httpResponseMessage.Content = new ByteArrayContent(EmptyArray<byte>.Instance);
			}
		}

		// Token: 0x060050C1 RID: 20673 RVA: 0x00128BF8 File Offset: 0x00126DF8
		public static bool IsEmpty(HttpResponseMessage httpResponseMessage)
		{
			return httpResponseMessage.Content == null || (httpResponseMessage.Content.Headers.ContentLength != null && httpResponseMessage.Content.Headers.ContentLength.Value == 0L);
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x00128C47 File Offset: 0x00126E47
		internal static void HandleContinueWithTask(Task task)
		{
			HttpChannelUtilities.HandleContinueWithTask(task, null);
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x00128C50 File Offset: 0x00126E50
		internal static void HandleContinueWithTask(Task task, Action<Exception> exceptionHandler)
		{
			if (task.IsFaulted)
			{
				if (exceptionHandler == null)
				{
					throw FxTrace.Exception.AsError<FaultException>(task.Exception);
				}
				exceptionHandler(task.Exception);
				return;
			}
			else
			{
				if (task.IsCanceled)
				{
					throw FxTrace.Exception.AsError(new TimeoutException(SR.GetString("TaskCancelledError")));
				}
				return;
			}
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x00128CA8 File Offset: 0x00126EA8
		public static void AbortRequest(HttpWebRequest request)
		{
			request.Abort();
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x00128CB0 File Offset: 0x00126EB0
		public static void SetRequestTimeout(HttpWebRequest request, TimeSpan timeout)
		{
			int num = TimeoutHelper.ToMilliseconds(timeout);
			if (num == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("HttpRequestTimedOut", new object[]
				{
					request.RequestUri,
					timeout
				})));
			}
			request.Timeout = num;
			request.ReadWriteTimeout = num;
		}

		// Token: 0x060050C6 RID: 20678 RVA: 0x00128D08 File Offset: 0x00126F08
		public static void AddReplySecurityProperty(HttpChannelFactory<IRequestChannel> factory, HttpWebRequest webRequest, HttpWebResponse webResponse, Message replyMessage)
		{
			SecurityMessageProperty securityMessageProperty = factory.CreateReplySecurityProperty(webRequest, webResponse);
			if (securityMessageProperty != null)
			{
				replyMessage.Properties.Security = securityMessageProperty;
			}
		}

		// Token: 0x060050C7 RID: 20679 RVA: 0x00128D2D File Offset: 0x00126F2D
		public static void CopyHeaders(HttpRequestMessage request, AddHeaderDelegate addHeader)
		{
			HttpChannelUtilities.CopyHeaders(request.Headers, addHeader);
			if (request.Content != null)
			{
				HttpChannelUtilities.CopyHeaders(request.Content.Headers, addHeader);
			}
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x00128D54 File Offset: 0x00126F54
		public static void CopyHeaders(HttpResponseMessage response, AddHeaderDelegate addHeader)
		{
			HttpChannelUtilities.CopyHeaders(response.Headers, addHeader);
			if (response.Content != null)
			{
				HttpChannelUtilities.CopyHeaders(response.Content.Headers, addHeader);
			}
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x00128D7C File Offset: 0x00126F7C
		private static void CopyHeaders(HttpHeaders headers, AddHeaderDelegate addHeader)
		{
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in headers)
			{
				foreach (string value in keyValuePair.Value)
				{
					HttpChannelUtilities.TryAddToCollection(addHeader, keyValuePair.Key, value);
				}
			}
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x00128E04 File Offset: 0x00127004
		public static void CopyHeaders(NameValueCollection headers, AddHeaderDelegate addHeader)
		{
			int count = headers.Count;
			for (int i = 0; i < count; i++)
			{
				string key = headers.GetKey(i);
				string[] values = headers.GetValues(i);
				if (values != null)
				{
					for (int j = 0; j < values.Length; j++)
					{
						HttpChannelUtilities.TryAddToCollection(addHeader, key, values[j]);
					}
				}
				else
				{
					addHeader(key, null);
				}
			}
		}

		// Token: 0x060050CB RID: 20683 RVA: 0x00128E5E File Offset: 0x0012705E
		public static void CopyHeadersToNameValueCollection(NameValueCollection headers, NameValueCollection destination)
		{
			HttpChannelUtilities.CopyHeaders(headers, new AddHeaderDelegate(destination.Add));
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x00128E74 File Offset: 0x00127074
		private static void TryAddToCollection(AddHeaderDelegate addHeader, string headerName, string value)
		{
			try
			{
				addHeader(headerName, value);
			}
			catch (ArgumentException exception)
			{
				string headerValue = null;
				if (HttpChannelUtilities.TryEncodeHeaderValueAsUri(headerName, value, out headerValue))
				{
					addHeader(headerName, headerValue);
				}
				else
				{
					FxTrace.Exception.AsInformation(exception);
				}
			}
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x00128EC0 File Offset: 0x001270C0
		private static bool TryEncodeHeaderValueAsUri(string headerName, string value, out string result)
		{
			result = null;
			Uri uri;
			if (string.Compare(headerName, "Referer", StringComparison.OrdinalIgnoreCase) == 0 && Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out uri))
			{
				if (uri.IsAbsoluteUri)
				{
					result = uri.AbsoluteUri;
				}
				else
				{
					result = uri.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x00128F0C File Offset: 0x0012710C
		internal static Type GetTypeFromAssembliesInCurrentDomain(string typeString)
		{
			Type type = Type.GetType(typeString, false);
			if (null == type)
			{
				if (!HttpChannelUtilities.allReferencedAssembliesLoaded)
				{
					HttpChannelUtilities.allReferencedAssembliesLoaded = true;
					AspNetEnvironment.Current.EnsureAllReferencedAssemblyLoaded();
				}
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				for (int i = 0; i < assemblies.Length; i++)
				{
					type = assemblies[i].GetType(typeString, false);
					if (null != type)
					{
						break;
					}
				}
			}
			return type;
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x00128F70 File Offset: 0x00127170
		public static NetworkCredential GetCredential(AuthenticationSchemes authenticationScheme, SecurityTokenProviderContainer credentialProvider, TimeSpan timeout, out TokenImpersonationLevel impersonationLevel, out AuthenticationLevel authenticationLevel)
		{
			impersonationLevel = TokenImpersonationLevel.None;
			authenticationLevel = AuthenticationLevel.None;
			NetworkCredential result = null;
			if (authenticationScheme != AuthenticationSchemes.Anonymous)
			{
				result = HttpChannelUtilities.GetCredentialCore(authenticationScheme, credentialProvider, timeout, out impersonationLevel, out authenticationLevel);
			}
			return result;
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x00128F9C File Offset: 0x0012719C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static NetworkCredential GetCredentialCore(AuthenticationSchemes authenticationScheme, SecurityTokenProviderContainer credentialProvider, TimeSpan timeout, out TokenImpersonationLevel impersonationLevel, out AuthenticationLevel authenticationLevel)
		{
			impersonationLevel = TokenImpersonationLevel.None;
			authenticationLevel = AuthenticationLevel.None;
			NetworkCredential result = null;
			switch (authenticationScheme)
			{
			case AuthenticationSchemes.Digest:
				result = TransportSecurityHelpers.GetSspiCredential(credentialProvider, timeout, out impersonationLevel, out authenticationLevel);
				HttpChannelUtilities.ValidateDigestCredential(ref result, impersonationLevel);
				return result;
			case AuthenticationSchemes.Negotiate:
				return TransportSecurityHelpers.GetSspiCredential(credentialProvider, timeout, out impersonationLevel, out authenticationLevel);
			case AuthenticationSchemes.Digest | AuthenticationSchemes.Negotiate:
				break;
			case AuthenticationSchemes.Ntlm:
				result = TransportSecurityHelpers.GetSspiCredential(credentialProvider, timeout, out impersonationLevel, out authenticationLevel);
				if (authenticationLevel == AuthenticationLevel.MutualAuthRequired)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CredentialDisallowsNtlm")));
				}
				return result;
			default:
				if (authenticationScheme == AuthenticationSchemes.Basic)
				{
					result = TransportSecurityHelpers.GetUserNameCredential(credentialProvider, timeout);
					impersonationLevel = TokenImpersonationLevel.Delegation;
					return result;
				}
				break;
			}
			throw Fx.AssertAndThrow("GetCredential: Invalid authentication scheme");
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x00129038 File Offset: 0x00127238
		public static HttpWebResponse ProcessGetResponseWebException(WebException webException, HttpWebRequest request, HttpAbortReason abortReason)
		{
			HttpWebResponse httpWebResponse = null;
			if (webException.Status == WebExceptionStatus.Success || webException.Status == WebExceptionStatus.ProtocolError)
			{
				httpWebResponse = (HttpWebResponse)webException.Response;
			}
			if (httpWebResponse == null)
			{
				Exception ex = HttpChannelUtilities.ConvertWebException(webException, request, abortReason);
				if (ex != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(webException.Message, webException));
			}
			else
			{
				if (httpWebResponse.StatusCode == HttpStatusCode.NotFound)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("EndpointNotFound", new object[]
					{
						request.RequestUri.AbsoluteUri
					}), webException));
				}
				if (httpWebResponse.StatusCode == HttpStatusCode.ServiceUnavailable)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ServerTooBusyException(SR.GetString("HttpServerTooBusy", new object[]
					{
						request.RequestUri.AbsoluteUri
					}), webException));
				}
				if (httpWebResponse.StatusCode == HttpStatusCode.UnsupportedMediaType)
				{
					string statusDescription = httpWebResponse.StatusDescription;
					if (!string.IsNullOrEmpty(statusDescription) && string.Compare(statusDescription, "Missing Content Type", StringComparison.OrdinalIgnoreCase) == 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("MissingContentType", new object[]
						{
							request.RequestUri
						}), webException));
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("FramingContentTypeMismatch", new object[]
					{
						request.ContentType,
						request.RequestUri
					}), webException));
				}
				else
				{
					if (httpWebResponse.StatusCode == HttpStatusCode.GatewayTimeout)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(webException.Message, webException));
					}
					if (httpWebResponse.StatusCode == HttpStatusCode.BadRequest)
					{
						string text = null;
						if (httpWebResponse.ContentLength == (long)"<h1>Bad Request (Invalid Hostname)</h1>".Length)
						{
							text = "<h1>Bad Request (Invalid Hostname)</h1>";
						}
						else if (httpWebResponse.ContentLength == (long)"<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\"\"http://www.w3.org/TR/html4/strict.dtd\">\r\n<HTML><HEAD><TITLE>Bad Request</TITLE>\r\n<META HTTP-EQUIV=\"Content-Type\" Content=\"text/html; charset=us-ascii\"></HEAD>\r\n<BODY><h2>Bad Request - Invalid Hostname</h2>\r\n<hr><p>HTTP Error 400. The request hostname is invalid.</p>\r\n</BODY></HTML>\r\n".Length)
						{
							text = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\"\"http://www.w3.org/TR/html4/strict.dtd\">\r\n<HTML><HEAD><TITLE>Bad Request</TITLE>\r\n<META HTTP-EQUIV=\"Content-Type\" Content=\"text/html; charset=us-ascii\"></HEAD>\r\n<BODY><h2>Bad Request - Invalid Hostname</h2>\r\n<hr><p>HTTP Error 400. The request hostname is invalid.</p>\r\n</BODY></HTML>\r\n";
						}
						if (text != null)
						{
							Stream responseStream = httpWebResponse.GetResponseStream();
							byte[] array = new byte[text.Length];
							int num = responseStream.Read(array, 0, array.Length);
							if (num == text.Length && text == Encoding.ASCII.GetString(array))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("EndpointNotFound", new object[]
								{
									request.RequestUri.AbsoluteUri
								}), webException));
							}
						}
					}
					return httpWebResponse;
				}
			}
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x00129278 File Offset: 0x00127478
		public static Exception ConvertWebException(WebException webException, HttpWebRequest request, HttpAbortReason abortReason)
		{
			switch (webException.Status)
			{
			case WebExceptionStatus.NameResolutionFailure:
			case WebExceptionStatus.ConnectFailure:
			case WebExceptionStatus.ProxyNameResolutionFailure:
				return new EndpointNotFoundException(SR.GetString("EndpointNotFound", new object[]
				{
					request.RequestUri.AbsoluteUri
				}), webException);
			case WebExceptionStatus.ReceiveFailure:
				return new CommunicationException(SR.GetString("HttpReceiveFailure", new object[]
				{
					request.RequestUri
				}), webException);
			case WebExceptionStatus.SendFailure:
				return new CommunicationException(SR.GetString("HttpSendFailure", new object[]
				{
					request.RequestUri
				}), webException);
			case WebExceptionStatus.RequestCanceled:
				return HttpChannelUtilities.CreateRequestCanceledException(webException, request, abortReason);
			case WebExceptionStatus.ProtocolError:
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)webException.Response;
				if (httpWebResponse.StatusCode == HttpStatusCode.InternalServerError && string.Compare(httpWebResponse.StatusDescription, "System.ServiceModel.ServiceActivationException", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return new ServiceActivationException(SR.GetString("Hosting_ServiceActivationFailed", new object[]
					{
						request.RequestUri
					}));
				}
				return null;
			}
			case WebExceptionStatus.TrustFailure:
				return new SecurityNegotiationException(SR.GetString("TrustFailure", new object[]
				{
					request.RequestUri.Authority
				}), webException);
			case WebExceptionStatus.SecureChannelFailure:
				return new SecurityNegotiationException(SR.GetString("SecureChannelFailure", new object[]
				{
					request.RequestUri.Authority
				}), webException);
			case WebExceptionStatus.Timeout:
				return new TimeoutException(HttpChannelUtilities.CreateRequestTimedOutMessage(request), webException);
			}
			return null;
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x001293E8 File Offset: 0x001275E8
		public static Exception CreateResponseIOException(IOException ioException, TimeSpan receiveTimeout)
		{
			if (ioException.InnerException is SocketException)
			{
				return SocketConnection.ConvertTransferException((SocketException)ioException.InnerException, receiveTimeout, ioException);
			}
			return new CommunicationException(SR.GetString("HttpTransferError", new object[]
			{
				ioException.Message
			}), ioException);
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x00129434 File Offset: 0x00127634
		public static Exception CreateResponseWebException(WebException webException, HttpWebResponse response, HttpAbortReason abortReason)
		{
			WebExceptionStatus status = webException.Status;
			if (status == WebExceptionStatus.RequestCanceled)
			{
				return HttpChannelUtilities.CreateResponseCanceledException(webException, response, abortReason);
			}
			if (status == WebExceptionStatus.ConnectionClosed)
			{
				return HttpChannelUtilities.TraceResponseException(new CommunicationException(webException.Message, webException));
			}
			if (status != WebExceptionStatus.Timeout)
			{
				return HttpChannelUtilities.CreateUnexpectedResponseException(webException, response);
			}
			return HttpChannelUtilities.TraceResponseException(new TimeoutException(SR.GetString("HttpResponseTimedOut", new object[]
			{
				response.ResponseUri,
				TimeSpan.FromMilliseconds((double)response.GetResponseStream().ReadTimeout)
			}), webException));
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x001294B8 File Offset: 0x001276B8
		public static Exception CreateResponseCanceledException(Exception webException, HttpWebResponse response, HttpAbortReason abortReason)
		{
			if (abortReason == HttpAbortReason.Aborted)
			{
				return HttpChannelUtilities.TraceResponseException(new CommunicationObjectAbortedException(SR.GetString("HttpResponseAborted"), webException));
			}
			if (abortReason != HttpAbortReason.TimedOut)
			{
				return HttpChannelUtilities.TraceResponseException(new CommunicationObjectAbortedException(SR.GetString("HttpResponseAborted"), webException));
			}
			return HttpChannelUtilities.TraceResponseException(new TimeoutException(SR.GetString("HttpResponseTimedOut", new object[]
			{
				response.ResponseUri,
				TimeSpan.FromMilliseconds((double)response.GetResponseStream().ReadTimeout)
			}), webException));
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x00129538 File Offset: 0x00127738
		public static Exception CreateRequestCanceledException(Exception webException, HttpWebRequest request, HttpAbortReason abortReason)
		{
			if (abortReason == HttpAbortReason.Aborted)
			{
				return new CommunicationObjectAbortedException(SR.GetString("HttpRequestAborted", new object[]
				{
					request.RequestUri
				}), webException);
			}
			if (abortReason != HttpAbortReason.TimedOut)
			{
				return new CommunicationException(SR.GetString("HttpTransferError", new object[]
				{
					webException.Message
				}), webException);
			}
			return new TimeoutException(HttpChannelUtilities.CreateRequestTimedOutMessage(request), webException);
		}

		// Token: 0x060050D7 RID: 20695 RVA: 0x0012959B File Offset: 0x0012779B
		public static Exception CreateRequestIOException(IOException ioException, HttpWebRequest request)
		{
			return HttpChannelUtilities.CreateRequestIOException(ioException, request, null);
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x001295A8 File Offset: 0x001277A8
		public static Exception CreateRequestIOException(IOException ioException, HttpWebRequest request, Exception originalException)
		{
			Exception ex = (originalException == null) ? ioException : originalException;
			if (ioException.InnerException is SocketException)
			{
				return SocketConnection.ConvertTransferException((SocketException)ioException.InnerException, TimeSpan.FromMilliseconds((double)request.Timeout), ex);
			}
			return new CommunicationException(SR.GetString("HttpTransferError", new object[]
			{
				ex.Message
			}), ex);
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x00129607 File Offset: 0x00127807
		private static string CreateRequestTimedOutMessage(HttpWebRequest request)
		{
			return SR.GetString("HttpRequestTimedOut", new object[]
			{
				request.RequestUri,
				TimeSpan.FromMilliseconds((double)request.Timeout)
			});
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x00129638 File Offset: 0x00127838
		public static Exception CreateRequestWebException(WebException webException, HttpWebRequest request, HttpAbortReason abortReason)
		{
			Exception ex = HttpChannelUtilities.ConvertWebException(webException, request, abortReason);
			if (webException.Response != null)
			{
				webException.Response.Close();
			}
			if (ex != null)
			{
				return ex;
			}
			if (webException.InnerException is IOException)
			{
				return HttpChannelUtilities.CreateRequestIOException((IOException)webException.InnerException, request, webException);
			}
			if (webException.InnerException is SocketException)
			{
				return SocketConnectionInitiator.ConvertConnectException((SocketException)webException.InnerException, request.RequestUri, TimeSpan.MaxValue, webException);
			}
			return new EndpointNotFoundException(SR.GetString("EndpointNotFound", new object[]
			{
				request.RequestUri.AbsoluteUri
			}), webException);
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x001296D4 File Offset: 0x001278D4
		private static Exception CreateUnexpectedResponseException(WebException responseException, HttpWebResponse response)
		{
			string text = response.StatusDescription;
			if (string.IsNullOrEmpty(text))
			{
				text = response.StatusCode.ToString();
			}
			return HttpChannelUtilities.TraceResponseException(new ProtocolException(SR.GetString("UnexpectedHttpResponseCode", new object[]
			{
				(int)response.StatusCode,
				text
			}), responseException));
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x00129732 File Offset: 0x00127932
		public static Exception CreateNullReferenceResponseException(NullReferenceException nullReferenceException)
		{
			return HttpChannelUtilities.TraceResponseException(new ProtocolException(SR.GetString("NullReferenceOnHttpResponse"), nullReferenceException));
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x0012974C File Offset: 0x0012794C
		private static string GetResponseStreamString(HttpWebResponse webResponse, out int bytesRead)
		{
			Stream responseStream = webResponse.GetResponseStream();
			long num = webResponse.ContentLength;
			if (num < 0L || num > 1024L)
			{
				num = 1024L;
			}
			byte[] array = DiagnosticUtility.Utility.AllocateByteArray(checked((int)num));
			bytesRead = responseStream.Read(array, 0, (int)num);
			responseStream.Close();
			return Encoding.UTF8.GetString(array, 0, bytesRead);
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x001297A9 File Offset: 0x001279A9
		private static Exception TraceResponseException(Exception exception)
		{
			if (DiagnosticUtility.ShouldTraceError)
			{
				TraceUtility.TraceEvent(TraceEventType.Error, 262156, SR.GetString("TraceCodeHttpChannelUnexpectedResponse"), null, exception);
			}
			return exception;
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x001297CC File Offset: 0x001279CC
		private static bool ValidateEmptyContent(HttpWebResponse response)
		{
			bool result = true;
			if (response.ContentLength > 0L)
			{
				result = false;
			}
			else if (response.ContentLength == -1L)
			{
				Stream responseStream = response.GetResponseStream();
				byte[] buffer = new byte[1];
				result = (responseStream.Read(buffer, 0, 1) != 1);
			}
			return result;
		}

		// Token: 0x060050E0 RID: 20704 RVA: 0x00129814 File Offset: 0x00127A14
		private static void ValidateAuthentication(HttpWebRequest request, HttpWebResponse response, WebException responseException, HttpChannelFactory<IRequestChannel> factory)
		{
			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				string @string = SR.GetString("HttpAuthorizationFailed", new object[]
				{
					factory.AuthenticationScheme,
					response.Headers[HttpResponseHeader.WwwAuthenticate]
				});
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.TraceResponseException(new MessageSecurityException(@string, responseException)));
			}
			if (response.StatusCode == HttpStatusCode.Forbidden)
			{
				string string2 = SR.GetString("HttpAuthorizationForbidden", new object[]
				{
					factory.AuthenticationScheme
				});
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.TraceResponseException(new MessageSecurityException(string2, responseException)));
			}
			if (request.AuthenticationLevel == AuthenticationLevel.MutualAuthRequired && !response.IsMutuallyAuthenticated)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.TraceResponseException(new SecurityNegotiationException(SR.GetString("HttpMutualAuthNotSatisfied"), responseException)));
			}
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x001298E8 File Offset: 0x00127AE8
		public static void ValidateDigestCredential(ref NetworkCredential credential, TokenImpersonationLevel impersonationLevel)
		{
			if (!SecurityUtils.IsDefaultNetworkCredential(credential) && !TokenImpersonationLevelHelper.IsGreaterOrEqual(impersonationLevel, TokenImpersonationLevel.Impersonation))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("DigestExplicitCredsImpersonationLevel", new object[]
				{
					impersonationLevel
				})));
			}
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x00129928 File Offset: 0x00127B28
		public static HttpInput ValidateRequestReplyResponse(HttpWebRequest request, HttpWebResponse response, HttpChannelFactory<IRequestChannel> factory, WebException responseException, ChannelBinding channelBinding)
		{
			HttpChannelUtilities.ValidateAuthentication(request, response, responseException, factory);
			HttpInput httpInput = null;
			if ((HttpStatusCode.OK > response.StatusCode || response.StatusCode >= HttpStatusCode.MultipleChoices) && response.StatusCode != HttpStatusCode.InternalServerError)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.CreateUnexpectedResponseException(responseException, response));
			}
			if (response.StatusCode == HttpStatusCode.InternalServerError && string.Compare(response.StatusDescription, "System.ServiceModel.ServiceActivationException", StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ServiceActivationException(SR.GetString("Hosting_ServiceActivationFailed", new object[]
				{
					request.RequestUri
				})));
			}
			bool flag = true;
			try
			{
				if (string.IsNullOrEmpty(response.ContentType))
				{
					if (!HttpChannelUtilities.ValidateEmptyContent(response))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.TraceResponseException(new ProtocolException(SR.GetString("HttpContentTypeHeaderRequired"), responseException)));
					}
				}
				else if (response.ContentLength != 0L)
				{
					MessageEncoder encoder = factory.MessageEncoderFactory.Encoder;
					if (!encoder.IsContentTypeSupported(response.ContentType))
					{
						int num;
						string responseStreamString = HttpChannelUtilities.GetResponseStreamString(response, out num);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(HttpChannelUtilities.TraceResponseException(new ProtocolException(SR.GetString("ResponseContentTypeMismatch", new object[]
						{
							response.ContentType,
							encoder.ContentType,
							num,
							responseStreamString
						}), responseException)));
					}
					httpInput = HttpInput.CreateHttpInput(request, response, factory, channelBinding);
					httpInput.WebException = responseException;
				}
				flag = false;
			}
			finally
			{
				if (flag)
				{
					response.Close();
				}
			}
			if (httpInput == null)
			{
				if (factory.MessageEncoderFactory.MessageVersion == MessageVersion.None)
				{
					httpInput = HttpInput.CreateHttpInput(request, response, factory, channelBinding);
					httpInput.WebException = responseException;
				}
				else
				{
					response.Close();
				}
			}
			return httpInput;
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x00129AD4 File Offset: 0x00127CD4
		public static bool GetHttpResponseTypeAndEncodingForCompression(ref string contentType, out string contentEncoding)
		{
			contentEncoding = null;
			bool flag = false;
			bool flag2 = false;
			if (string.Equals(BinaryVersion.GZipVersion1.ContentType, contentType, StringComparison.OrdinalIgnoreCase) || (flag = string.Equals(BinaryVersion.GZipVersion1.SessionContentType, contentType, StringComparison.OrdinalIgnoreCase)) || (flag2 = (string.Equals(BinaryVersion.DeflateVersion1.ContentType, contentType, StringComparison.OrdinalIgnoreCase) || (flag = string.Equals(BinaryVersion.DeflateVersion1.SessionContentType, contentType, StringComparison.OrdinalIgnoreCase)))))
			{
				contentType = (flag ? BinaryVersion.Version1.SessionContentType : BinaryVersion.Version1.ContentType);
				contentEncoding = (flag2 ? "deflate" : "gzip");
				return true;
			}
			return false;
		}

		// Token: 0x040031DF RID: 12767
		internal const string HttpStatusCodeKey = "HttpStatusCode";

		// Token: 0x040031E0 RID: 12768
		internal const string HttpStatusCodeExceptionKey = "System.ServiceModel.Channels.HttpInput.HttpStatusCode";

		// Token: 0x040031E1 RID: 12769
		internal const string HttpStatusDescriptionExceptionKey = "System.ServiceModel.Channels.HttpInput.HttpStatusDescription";

		// Token: 0x040031E2 RID: 12770
		internal const int ResponseStreamExcerptSize = 1024;

		// Token: 0x040031E3 RID: 12771
		internal const string MIMEVersionHeader = "MIME-Version";

		// Token: 0x040031E4 RID: 12772
		internal const string ContentEncodingHeader = "Content-Encoding";

		// Token: 0x040031E5 RID: 12773
		internal const string AcceptEncodingHeader = "Accept-Encoding";

		// Token: 0x040031E6 RID: 12774
		private const string ContentLengthHeader = "Content-Length";

		// Token: 0x040031E7 RID: 12775
		private static readonly HashSet<string> httpContentHeaders = new HashSet<string>
		{
			"Allow",
			"Content-Encoding",
			"Content-Language",
			"Content-Location",
			"Content-MD5",
			"Content-Range",
			"Expires",
			"Last-Modified",
			"Content-Type",
			"Content-Length"
		};

		// Token: 0x040031E8 RID: 12776
		private static bool allReferencedAssembliesLoaded = false;

		// Token: 0x02000D47 RID: 3399
		internal static class StatusDescriptionStrings
		{
			// Token: 0x040047A6 RID: 18342
			internal const string HttpContentTypeMissing = "Missing Content Type";

			// Token: 0x040047A7 RID: 18343
			internal const string HttpContentTypeMismatch = "Cannot process the message because the content type '{0}' was not the expected type '{1}'.";

			// Token: 0x040047A8 RID: 18344
			internal const string HttpStatusServiceActivationException = "System.ServiceModel.ServiceActivationException";
		}

		// Token: 0x02000D48 RID: 3400
		internal static class ObsoleteDescriptionStrings
		{
			// Token: 0x040047A9 RID: 18345
			internal const string PropertyObsoleteUseAllowCookies = "This property is obsolete. To enable Http CookieContainer, use the AllowCookies property instead.";

			// Token: 0x040047AA RID: 18346
			internal const string TypeObsoleteUseAllowCookies = "This type is obsolete. To enable the Http CookieContainer, use the AllowCookies property on the http binding or on the HttpTransportBindingElement.";
		}
	}
}
