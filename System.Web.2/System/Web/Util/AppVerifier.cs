using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x020001C1 RID: 449
	internal static class AppVerifier
	{
		// Token: 0x06001717 RID: 5911 RVA: 0x000487FC File Offset: 0x000469FC
		private static Action<AppVerifierException> GetAppVerifierBehaviorFromRegistry()
		{
			int valueOrDefault = (Misc.GetAspNetRegValue(null, "RuntimeVerificationBehavior", null) as int?).GetValueOrDefault();
			AppVerifier.AppVerifierErrorCodeEnableAssertMask = ((Misc.GetAspNetRegValue(null, "AppVerifierErrorCodeEnableAssertMask", -1L) as long?) ?? -1L);
			AppVerifier.AppVerifierErrorCodeCollectCallStackMask = ((Misc.GetAspNetRegValue(null, "AppVerifierErrorCodeCollectCallstackMask", -1L) as long?) ?? -1L);
			AppVerifier.AppVerifierCollectCallStackMask = (AppVerifier.CallStackCollectionBitMasks)((Misc.GetAspNetRegValue(null, "AppVerifierCollectCallStackMask", -1) as int?) ?? -1);
			switch (valueOrDefault)
			{
			case 1:
				return new Action<AppVerifierException>(AppVerifier.WriteToEventLog);
			case 2:
				return new Action<AppVerifierException>(AppVerifier.WriteToEventLogAndSoftBreak);
			case 3:
				return new Action<AppVerifierException>(AppVerifier.WriteToEventLogAndHardBreak);
			default:
				return null;
			}
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00048906 File Offset: 0x00046B06
		private static void WriteToEventLog(AppVerifierException ex)
		{
			Misc.WriteUnhandledExceptionToEventLog(AppDomain.CurrentDomain, ex);
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00048913 File Offset: 0x00046B13
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private static void WriteToEventLogAndSoftBreak(AppVerifierException ex)
		{
			AppVerifier.WriteToEventLog(ex);
			if (Debugger.Launch())
			{
				Debugger.Break();
			}
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00048927 File Offset: 0x00046B27
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private static void WriteToEventLogAndHardBreak(AppVerifierException ex)
		{
			AppVerifier.WriteToEventLog(ex);
			if (Debugger.IsAttached)
			{
				Debugger.Break();
				return;
			}
			AppVerifier.NativeMethods.DebugBreak();
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00048944 File Offset: 0x00046B44
		public static Func<T, AsyncCallback, object, IAsyncResult> WrapBeginMethod<T>(HttpApplication httpApplication, Func<T, AsyncCallback, object, IAsyncResult> originalDelegate)
		{
			if (!AppVerifier.IsAppVerifierEnabled)
			{
				return originalDelegate;
			}
			return (T arg, AsyncCallback callback, object state) => AppVerifier.WrapBeginMethodImpl(httpApplication, (AsyncCallback innerCallback, object innerState) => originalDelegate(arg, innerCallback, innerState), originalDelegate, new Action<AppVerifierException>(AppVerifier.HandleAppVerifierException), AppVerifier.CallStackCollectionBitMasks.AllHandlerMask)(callback, state);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00048980 File Offset: 0x00046B80
		public static BeginEventHandler WrapBeginMethod(HttpApplication httpApplication, BeginEventHandler originalDelegate)
		{
			if (!AppVerifier.IsAppVerifierEnabled)
			{
				return originalDelegate;
			}
			return (object sender, EventArgs e, AsyncCallback cb, object extraData) => AppVerifier.WrapBeginMethodImpl(httpApplication, (AsyncCallback innerCallback, object innerState) => originalDelegate(sender, e, innerCallback, innerState), originalDelegate, new Action<AppVerifierException>(AppVerifier.HandleAppVerifierException), AppVerifier.CallStackCollectionBitMasks.AllStepMask)(cb, extraData);
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x000489BC File Offset: 0x00046BBC
		internal static Func<AsyncCallback, object, IAsyncResult> WrapBeginMethodImpl(HttpApplication httpApplication, Func<AsyncCallback, object, IAsyncResult> beginMethod, Delegate originalDelegate, Action<AppVerifierException> errorHandler, AppVerifier.CallStackCollectionBitMasks callStackMask)
		{
			return delegate(AsyncCallback callback, object state)
			{
				AppVerifier.AsyncCallbackInvocationHelper asyncCallbackInvocationHelper = new AppVerifier.AsyncCallbackInvocationHelper();
				AppVerifier.CallStackCollectionBitMasks callStackCollectionBitMasks = callStackMask & AppVerifier.CallStackCollectionBitMasks.AllBeginMask;
				bool captureStack = (callStackCollectionBitMasks & AppVerifier.AppVerifierCollectCallStackMask) == callStackCollectionBitMasks;
				AppVerifier.InvocationInfo beginHandlerInvocationInfo = AppVerifier.InvocationInfo.Capture(captureStack);
				string requestUrl = null;
				RequestNotification? currentNotification = null;
				bool isPostNotification = false;
				Type httpHandlerType = null;
				if (httpApplication != null)
				{
					HttpContext context = httpApplication.Context;
					if (context != null)
					{
						if (!context.HideRequestResponse && context.Request != null)
						{
							requestUrl = AppVerifier.TryGetRequestUrl(context);
						}
						if (context.NotificationContext != null)
						{
							currentNotification = new RequestNotification?(context.NotificationContext.CurrentNotification);
							isPostNotification = context.NotificationContext.IsPostNotification;
						}
						if (context.Handler != null)
						{
							httpHandlerType = context.Handler.GetType();
						}
					}
				}
				AppVerifier.AssertDelegate assert = delegate(bool condition, AppVerifierErrorCode errorCode)
				{
					long num = 1L << (int)errorCode;
					bool flag5 = (AppVerifier.AppVerifierErrorCodeEnableAssertMask & num) == num;
					if (!condition && flag5)
					{
						bool captureStack2 = (AppVerifier.AppVerifierErrorCodeCollectCallStackMask & num) == num;
						AppVerifier.InvocationInfo invocationInfo = AppVerifier.InvocationInfo.Capture(captureStack2);
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_Title", new object[0]));
						stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_Subtitle", new object[0]));
						stringBuilder.AppendLine();
						stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_BasicInfo_URL", new object[]
						{
							requestUrl
						}));
						stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_BasicInfo_ErrorCode", new object[]
						{
							(int)errorCode
						}));
						stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_BasicInfo_Description", new object[]
						{
							AppVerifier.GetLocalizedDescriptionStringForError(errorCode)
						}));
						stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_BasicInfo_ThreadInfo", new object[]
						{
							invocationInfo.ThreadId,
							invocationInfo.Timestamp.ToLocalTime()
						}));
						stringBuilder.AppendLine(invocationInfo.StackTrace.ToString());
						stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_BeginMethodInfo_EntryMethod", new object[]
						{
							AppVerifier.PrettyPrintDelegate(originalDelegate)
						}));
						if (currentNotification != null)
						{
							stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_BeginMethodInfo_RequestNotification_Integrated", new object[]
							{
								currentNotification,
								isPostNotification
							}));
						}
						else
						{
							stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_BeginMethodInfo_RequestNotification_NotIntegrated", new object[0]));
						}
						stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_BeginMethodInfo_CurrentHandler", new object[]
						{
							httpHandlerType
						}));
						stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_BeginMethodInfo_ThreadInfo", new object[]
						{
							beginHandlerInvocationInfo.ThreadId,
							beginHandlerInvocationInfo.Timestamp.ToLocalTime()
						}));
						stringBuilder.AppendLine(beginHandlerInvocationInfo.StackTrace.ToString());
						int num2;
						AppVerifier.InvocationInfo firstInvocationInfo = asyncCallbackInvocationHelper.GetFirstInvocationInfo(out num2);
						stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_AsyncCallbackInfo_InvocationCount", new object[]
						{
							num2
						}));
						if (firstInvocationInfo != null)
						{
							stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_AsyncCallbackInfo_FirstInvocation_ThreadInfo", new object[]
							{
								firstInvocationInfo.ThreadId,
								firstInvocationInfo.Timestamp.ToLocalTime()
							}));
							stringBuilder.AppendLine(firstInvocationInfo.StackTrace.ToString());
						}
						AppVerifierException ex = new AppVerifierException(errorCode, stringBuilder.ToString());
						errorHandler(ex);
						throw ex;
					}
				};
				assert(httpApplication != null, AppVerifierErrorCode.HttpApplicationInstanceWasNull);
				assert(originalDelegate != null, AppVerifierErrorCode.BeginHandlerDelegateWasNull);
				object lockObj = new object();
				IAsyncResult asyncResult2 = null;
				IAsyncResult asyncResultPassedToCallback = null;
				object beginHandlerReturnValueHolder = null;
				Thread threadWhichCalledBeginHandler = Thread.CurrentThread;
				bool callbackRanToCompletion = false;
				HttpContext assignedContextUponCallingBeginHandler = httpApplication.Context;
				IAsyncResult result;
				try
				{
					asyncResult2 = beginMethod(delegate(IAsyncResult asyncResult)
					{
						try
						{
							AppVerifier.CallStackCollectionBitMasks callStackCollectionBitMasks2 = callStackMask & AppVerifier.CallStackCollectionBitMasks.AllCallbackMask;
							bool captureCallStack = (callStackCollectionBitMasks2 & AppVerifier.AppVerifierCollectCallStackMask) == callStackCollectionBitMasks2;
							int num = asyncCallbackInvocationHelper.RecordInvocation(captureCallStack);
							assert(num == 1, AppVerifierErrorCode.AsyncCallbackInvokedMultipleTimes);
							assert(asyncResult != null, AppVerifierErrorCode.AsyncCallbackInvokedWithNullParameter);
							object lockObj5 = lockObj;
							object beginHandlerReturnValueHolder;
							Thread threadWhichCalledBeginHandler;
							lock (lockObj5)
							{
								asyncResultPassedToCallback = asyncResult;
								beginHandlerReturnValueHolder = beginHandlerReturnValueHolder;
								threadWhichCalledBeginHandler = threadWhichCalledBeginHandler;
							}
							assert(asyncResult.IsCompleted, AppVerifierErrorCode.AsyncCallbackGivenAsyncResultWhichWasNotCompleted);
							if (beginHandlerReturnValueHolder == null)
							{
								if (!asyncResult.CompletedSynchronously)
								{
									assert(threadWhichCalledBeginHandler != Thread.CurrentThread, AppVerifierErrorCode.AsyncCallbackInvokedSynchronouslyButAsyncResultWasNotMarkedCompletedSynchronously);
								}
							}
							else
							{
								AppVerifier.Holder<IAsyncResult> holder = beginHandlerReturnValueHolder as AppVerifier.Holder<IAsyncResult>;
								if (holder != null)
								{
									assert(asyncResult == holder.Value, AppVerifierErrorCode.AsyncCallbackInvokedWithUnexpectedAsyncResultInstance);
									assert(!asyncResult.CompletedSynchronously, AppVerifierErrorCode.AsyncCallbackInvokedAsynchronouslyButAsyncResultWasMarkedCompletedSynchronously);
								}
								else
								{
									assert(false, AppVerifierErrorCode.BeginHandlerThrewThenAsyncCallbackInvokedAsynchronously);
								}
							}
							assert(asyncResult.AsyncState == state, AppVerifierErrorCode.AsyncCallbackInvokedWithUnexpectedAsyncResultAsyncState);
							assert(assignedContextUponCallingBeginHandler == httpApplication.Context, AppVerifierErrorCode.AsyncCallbackCalledAfterHttpApplicationReassigned);
						}
						catch (AppVerifierException)
						{
						}
						if (callback != null)
						{
							callback(asyncResult);
						}
						callbackRanToCompletion = true;
					}, state);
					assert(asyncResult2 != null, AppVerifierErrorCode.BeginHandlerReturnedNull);
					object lockObj6 = lockObj;
					lock (lockObj6)
					{
						beginHandlerReturnValueHolder = new AppVerifier.Holder<IAsyncResult>(asyncResult2);
					}
					if (asyncResult2.CompletedSynchronously)
					{
						assert(asyncResult2.IsCompleted, AppVerifierErrorCode.BeginHandlerReturnedAsyncResultMarkedCompletedSynchronouslyButWhichWasNotCompleted);
						assert(asyncCallbackInvocationHelper.TotalInvocations != 0, AppVerifierErrorCode.BeginHandlerReturnedAsyncResultMarkedCompletedSynchronouslyButAsyncCallbackNeverCalled);
					}
					object lockObj2 = lockObj;
					IAsyncResult asyncResultPassedToCallback3;
					lock (lockObj2)
					{
						asyncResultPassedToCallback3 = asyncResultPassedToCallback;
					}
					if (asyncResultPassedToCallback3 != null)
					{
						assert(asyncResultPassedToCallback3 == asyncResult2, AppVerifierErrorCode.BeginHandlerReturnedUnexpectedAsyncResultInstance);
					}
					assert(asyncResult2.AsyncState == state, AppVerifierErrorCode.BeginHandlerReturnedUnexpectedAsyncResultAsyncState);
					result = asyncResult2;
				}
				catch (AppVerifierException)
				{
					result = asyncResult2;
				}
				catch (Exception value)
				{
					if (asyncResult2 == null)
					{
						object lockObj3 = lockObj;
						IAsyncResult asyncResultPassedToCallback2;
						lock (lockObj3)
						{
							beginHandlerReturnValueHolder = new AppVerifier.Holder<Exception>(value);
							asyncResultPassedToCallback2 = asyncResultPassedToCallback;
						}
						try
						{
							if (asyncResultPassedToCallback2 != null)
							{
								assert(asyncResultPassedToCallback2.CompletedSynchronously, AppVerifierErrorCode.AsyncCallbackInvokedAsynchronouslyThenBeginHandlerThrew);
								assert(!callbackRanToCompletion, AppVerifierErrorCode.AsyncCallbackInvokedSynchronouslyThenBeginHandlerThrew);
							}
						}
						catch (AppVerifierException)
						{
						}
						throw;
					}
					result = asyncResult2;
				}
				finally
				{
					object lockObj4 = lockObj;
					lock (lockObj4)
					{
						threadWhichCalledBeginHandler = null;
					}
				}
				return result;
			};
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x000489FF File Offset: 0x00046BFF
		public static Action<bool> GetSyncContextCheckDelegate(ISyncContext syncContext)
		{
			if (!AppVerifier.IsAppVerifierEnabled)
			{
				return null;
			}
			return AppVerifier.GetSyncContextCheckDelegateImpl(syncContext, new Action<AppVerifierException>(AppVerifier.HandleAppVerifierException));
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00048A1C File Offset: 0x00046C1C
		internal static Action<bool> GetSyncContextCheckDelegateImpl(ISyncContext syncContext, Action<AppVerifierException> errorHandler)
		{
			string requestUrl = null;
			object originalThreadContextId = null;
			HttpContext httpContext = (syncContext != null) ? syncContext.HttpContext : null;
			if (httpContext != null)
			{
				if (!httpContext.HideRequestResponse && httpContext.Request != null)
				{
					requestUrl = AppVerifier.TryGetRequestUrl(httpContext);
				}
				originalThreadContextId = httpContext.ThreadContextId;
			}
			AppVerifier.AssertDelegate assert = AppVerifier.GetAssertDelegateImpl(requestUrl, errorHandler, null);
			return delegate(bool checkForReEntry)
			{
				try
				{
					HttpContext httpContext2 = (syncContext != null) ? syncContext.HttpContext : null;
					object obj = (httpContext2 != null) ? httpContext2.ThreadContextId : null;
					assert(obj != null && originalThreadContextId == obj, AppVerifierErrorCode.SyncContextSendOrPostCalledAfterRequestCompleted);
					if (HttpRuntime.UsingIntegratedPipeline && !httpContext2.HasWebSocketRequestTransitionCompleted)
					{
						NotificationContext notificationContext = (httpContext2 != null) ? httpContext2.NotificationContext : null;
						assert(notificationContext != null, AppVerifierErrorCode.SyncContextSendOrPostCalledBetweenNotifications);
						if (checkForReEntry && notificationContext != null)
						{
							assert(!notificationContext.IsReEntry, AppVerifierErrorCode.SyncContextPostCalledInNestedNotification);
						}
					}
				}
				catch (AppVerifierException)
				{
				}
			};
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x00048A98 File Offset: 0x00046C98
		internal static void InvokeVerifierCheck<T>(Action<T> verifierCheckDelegate, T result)
		{
			if (verifierCheckDelegate != null)
			{
				try
				{
					verifierCheckDelegate(result);
				}
				catch (AppVerifierException)
				{
				}
			}
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00048AC4 File Offset: 0x00046CC4
		internal static Action<RequestNotificationStatus> GetRequestNotificationStatusCheckDelegate(HttpContext context, RequestNotification currentNotification, bool isPostNotification)
		{
			if (!AppVerifier.IsAppVerifierEnabled)
			{
				return null;
			}
			return AppVerifier.GetRequestNotificationStatusCheckDelegateImpl(context, currentNotification, isPostNotification, new Action<AppVerifierException>(AppVerifier.HandleAppVerifierException));
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00048AE4 File Offset: 0x00046CE4
		internal static Action<RequestNotificationStatus> GetRequestNotificationStatusCheckDelegateImpl(HttpContext context, RequestNotification currentNotification, bool isPostNotification, Action<AppVerifierException> errorHandler)
		{
			NotificationContext originalNotificationContext = context.NotificationContext;
			bool isReentry = originalNotificationContext.IsReEntry;
			string requestUrl = null;
			if (!context.HideRequestResponse && context.Request != null)
			{
				requestUrl = AppVerifier.TryGetRequestUrl(context);
			}
			AppVerifier.AppendAdditionalInfoDelegate appendAdditionalInfoDelegate = delegate(StringBuilder errorString)
			{
				errorString.AppendLine(AppVerifier.FormatErrorString("AppVerifier_BasicInfo_NotificationInfo", new object[]
				{
					currentNotification,
					isPostNotification,
					isReentry
				}));
			};
			AppVerifier.AssertDelegate assert = AppVerifier.GetAssertDelegateImpl(requestUrl, errorHandler, appendAdditionalInfoDelegate);
			return delegate(RequestNotificationStatus status)
			{
				if (status == RequestNotificationStatus.Pending)
				{
					assert(!isReentry, AppVerifierErrorCode.PendingProcessRequestNotificationStatusAfterCompletingNestedNotification);
					return;
				}
				assert(context.NotificationContext != null && !context.NotificationContext.PendingAsyncCompletion, AppVerifierErrorCode.RequestNotificationCompletedSynchronouslyWithNotificationContextPending);
				assert(context.NotificationContext == originalNotificationContext, AppVerifierErrorCode.NotificationContextHasChangedAfterSynchronouslyProcessingNotification);
			};
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00048B80 File Offset: 0x00046D80
		private static AppVerifier.AssertDelegate GetAssertDelegateImpl(string requestUrl, Action<AppVerifierException> errorHandler, AppVerifier.AppendAdditionalInfoDelegate appendAdditionalInfoDelegate)
		{
			return delegate(bool condition, AppVerifierErrorCode errorCode)
			{
				long num = 1L << (int)errorCode;
				bool flag = (AppVerifier.AppVerifierErrorCodeEnableAssertMask & num) == num;
				if (!condition && flag)
				{
					bool captureStack = (AppVerifier.AppVerifierErrorCodeCollectCallStackMask & num) == num;
					AppVerifier.InvocationInfo invocationInfo = AppVerifier.InvocationInfo.Capture(captureStack);
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_Title", new object[0]));
					stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_Subtitle", new object[0]));
					stringBuilder.AppendLine();
					stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_BasicInfo_URL", new object[]
					{
						requestUrl
					}));
					stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_BasicInfo_ErrorCode", new object[]
					{
						(int)errorCode
					}));
					stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_BasicInfo_Description", new object[]
					{
						AppVerifier.GetLocalizedDescriptionStringForError(errorCode)
					}));
					stringBuilder.AppendLine(AppVerifier.FormatErrorString("AppVerifier_BasicInfo_ThreadInfo", new object[]
					{
						invocationInfo.ThreadId,
						invocationInfo.Timestamp.ToLocalTime()
					}));
					if (appendAdditionalInfoDelegate != null)
					{
						appendAdditionalInfoDelegate(stringBuilder);
					}
					stringBuilder.AppendLine(invocationInfo.StackTrace.ToString());
					AppVerifierException ex = new AppVerifierException(errorCode, stringBuilder.ToString());
					errorHandler(ex);
					throw ex;
				}
			};
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00048BB4 File Offset: 0x00046DB4
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private static void HandleAppVerifierException(AppVerifierException ex)
		{
			AppVerifierErrorCode errorCode = ex.ErrorCode;
			string message = ex.Message;
			AppVerifier.DefaultAppVerifierBehavior(ex);
			GC.KeepAlive(errorCode);
			GC.KeepAlive(message);
			GC.KeepAlive(ex);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00048BF4 File Offset: 0x00046DF4
		private static string TryGetRequestUrl(HttpContext context)
		{
			string result;
			try
			{
				result = context.Request.EnsureRawUrl();
			}
			catch (HttpException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00048C28 File Offset: 0x00046E28
		internal static string PrettyPrintDelegate(Delegate del)
		{
			return AppVerifier.PrettyPrintMemberInfo((del != null) ? del.Method : null);
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00048C3C File Offset: 0x00046E3C
		internal static string PrettyPrintMemberInfo(MethodInfo method)
		{
			if (method == null)
			{
				return null;
			}
			string text = method.ToString();
			Type reflectedType = method.ReflectedType;
			if (reflectedType != null)
			{
				text += " [";
				if (reflectedType.Module != null)
				{
					text = text + reflectedType.Module.Name + "!";
				}
				text = text + reflectedType.FullName + "]";
			}
			return text;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00048CAF File Offset: 0x00046EAF
		internal static string GetLocalizedDescriptionStringForError(AppVerifierErrorCode errorCode)
		{
			return AppVerifier.FormatErrorString(AppVerifier._errorStringMappings[errorCode], new object[0]);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00048CC7 File Offset: 0x00046EC7
		internal static string FormatErrorString(string name, params object[] args)
		{
			return string.Format(CultureInfo.InstalledUICulture, SR.Resources.GetString(name, CultureInfo.InstalledUICulture), args);
		}

		// Token: 0x040016C8 RID: 5832
		private static readonly Dictionary<AppVerifierErrorCode, string> _errorStringMappings = new Dictionary<AppVerifierErrorCode, string>
		{
			{
				AppVerifierErrorCode.HttpApplicationInstanceWasNull,
				"AppVerifier_Errors_HttpApplicationInstanceWasNull"
			},
			{
				AppVerifierErrorCode.BeginHandlerDelegateWasNull,
				"AppVerifier_Errors_BeginHandlerDelegateWasNull"
			},
			{
				AppVerifierErrorCode.AsyncCallbackInvokedMultipleTimes,
				"AppVerifier_Errors_AsyncCallbackInvokedMultipleTimes"
			},
			{
				AppVerifierErrorCode.AsyncCallbackInvokedWithNullParameter,
				"AppVerifier_Errors_AsyncCallbackInvokedWithNullParameter"
			},
			{
				AppVerifierErrorCode.AsyncCallbackGivenAsyncResultWhichWasNotCompleted,
				"AppVerifier_Errors_AsyncCallbackGivenAsyncResultWhichWasNotCompleted"
			},
			{
				AppVerifierErrorCode.AsyncCallbackInvokedSynchronouslyButAsyncResultWasNotMarkedCompletedSynchronously,
				"AppVerifier_Errors_AsyncCallbackInvokedSynchronouslyButAsyncResultWasNotMarkedCompletedSynchronously"
			},
			{
				AppVerifierErrorCode.AsyncCallbackInvokedAsynchronouslyButAsyncResultWasMarkedCompletedSynchronously,
				"AppVerifier_Errors_AsyncCallbackInvokedAsynchronouslyButAsyncResultWasMarkedCompletedSynchronously"
			},
			{
				AppVerifierErrorCode.AsyncCallbackInvokedWithUnexpectedAsyncResultInstance,
				"AppVerifier_Errors_AsyncCallbackInvokedWithUnexpectedAsyncResultInstance"
			},
			{
				AppVerifierErrorCode.AsyncCallbackInvokedAsynchronouslyThenBeginHandlerThrew,
				"AppVerifier_Errors_AsyncCallbackInvokedEvenThoughBeginHandlerThrew"
			},
			{
				AppVerifierErrorCode.BeginHandlerThrewThenAsyncCallbackInvokedAsynchronously,
				"AppVerifier_Errors_AsyncCallbackInvokedEvenThoughBeginHandlerThrew"
			},
			{
				AppVerifierErrorCode.AsyncCallbackInvokedSynchronouslyThenBeginHandlerThrew,
				"AppVerifier_Errors_AsyncCallbackInvokedEvenThoughBeginHandlerThrew"
			},
			{
				AppVerifierErrorCode.AsyncCallbackInvokedWithUnexpectedAsyncResultAsyncState,
				"AppVerifier_Errors_AsyncCallbackInvokedWithUnexpectedAsyncResultAsyncState"
			},
			{
				AppVerifierErrorCode.AsyncCallbackCalledAfterHttpApplicationReassigned,
				"AppVerifier_Errors_AsyncCallbackCalledAfterHttpApplicationReassigned"
			},
			{
				AppVerifierErrorCode.BeginHandlerReturnedNull,
				"AppVerifier_Errors_BeginHandlerReturnedNull"
			},
			{
				AppVerifierErrorCode.BeginHandlerReturnedAsyncResultMarkedCompletedSynchronouslyButWhichWasNotCompleted,
				"AppVerifier_Errors_BeginHandlerReturnedAsyncResultMarkedCompletedSynchronouslyButWhichWasNotCompleted"
			},
			{
				AppVerifierErrorCode.BeginHandlerReturnedAsyncResultMarkedCompletedSynchronouslyButAsyncCallbackNeverCalled,
				"AppVerifier_Errors_BeginHandlerReturnedAsyncResultMarkedCompletedSynchronouslyButAsyncCallbackNeverCalled"
			},
			{
				AppVerifierErrorCode.BeginHandlerReturnedUnexpectedAsyncResultInstance,
				"AppVerifier_Errors_AsyncCallbackInvokedWithUnexpectedAsyncResultInstance"
			},
			{
				AppVerifierErrorCode.BeginHandlerReturnedUnexpectedAsyncResultAsyncState,
				"AppVerifier_Errors_BeginHandlerReturnedUnexpectedAsyncResultAsyncState"
			},
			{
				AppVerifierErrorCode.SyncContextSendOrPostCalledAfterRequestCompleted,
				"AppVerifier_Errors_SyncContextSendOrPostCalledAfterRequestCompleted"
			},
			{
				AppVerifierErrorCode.SyncContextSendOrPostCalledBetweenNotifications,
				"AppVerifier_Errors_SyncContextSendOrPostCalledBetweenNotifications"
			},
			{
				AppVerifierErrorCode.SyncContextPostCalledInNestedNotification,
				"AppVerifier_Errors_SyncContextPostCalledInNestedNotification"
			},
			{
				AppVerifierErrorCode.RequestNotificationCompletedSynchronouslyWithNotificationContextPending,
				"AppVerifier_Errors_RequestNotificationCompletedSynchronouslyWithNotificationContextPending"
			},
			{
				AppVerifierErrorCode.NotificationContextHasChangedAfterSynchronouslyProcessingNotification,
				"AppVerifier_Errors_NotificationContextHasChangedAfterSynchronouslyProcessingNotification"
			},
			{
				AppVerifierErrorCode.PendingProcessRequestNotificationStatusAfterCompletingNestedNotification,
				"AppVerifier_Errors_PendingProcessRequestNotificationStatusAfterCompletingNestedNotification"
			}
		};

		// Token: 0x040016C9 RID: 5833
		private static Action<AppVerifierException> DefaultAppVerifierBehavior = AppVerifier.GetAppVerifierBehaviorFromRegistry();

		// Token: 0x040016CA RID: 5834
		internal static readonly bool IsAppVerifierEnabled = AppVerifier.DefaultAppVerifierBehavior != null;

		// Token: 0x040016CB RID: 5835
		private static long AppVerifierErrorCodeCollectCallStackMask;

		// Token: 0x040016CC RID: 5836
		private static long AppVerifierErrorCodeEnableAssertMask;

		// Token: 0x040016CD RID: 5837
		private static AppVerifier.CallStackCollectionBitMasks AppVerifierCollectCallStackMask;

		// Token: 0x02000924 RID: 2340
		[Flags]
		internal enum CallStackCollectionBitMasks
		{
			// Token: 0x0400375E RID: 14174
			AllMask = -1,
			// Token: 0x0400375F RID: 14175
			BeginCallHandlerMask = 1,
			// Token: 0x04003760 RID: 14176
			CallHandlerCallbackMask = 2,
			// Token: 0x04003761 RID: 14177
			BeginExecutionStepMask = 4,
			// Token: 0x04003762 RID: 14178
			ExecutionStepCallbackMask = 8,
			// Token: 0x04003763 RID: 14179
			AllHandlerMask = 3,
			// Token: 0x04003764 RID: 14180
			AllStepMask = 12,
			// Token: 0x04003765 RID: 14181
			AllBeginMask = 5,
			// Token: 0x04003766 RID: 14182
			AllCallbackMask = 10
		}

		// Token: 0x02000925 RID: 2341
		// (Invoke) Token: 0x0600692B RID: 26923
		private delegate void AssertDelegate(bool condition, AppVerifierErrorCode errorCode);

		// Token: 0x02000926 RID: 2342
		// (Invoke) Token: 0x0600692F RID: 26927
		private delegate void AppendAdditionalInfoDelegate(StringBuilder errorString);

		// Token: 0x02000927 RID: 2343
		private sealed class AsyncCallbackInvocationHelper
		{
			// Token: 0x17001D22 RID: 7458
			// (get) Token: 0x06006932 RID: 26930 RVA: 0x001764E0 File Offset: 0x001746E0
			public int TotalInvocations
			{
				[MethodImpl(MethodImplOptions.Synchronized)]
				get
				{
					return this._totalInvocationCount;
				}
			}

			// Token: 0x06006933 RID: 26931 RVA: 0x001764E8 File Offset: 0x001746E8
			[MethodImpl(MethodImplOptions.Synchronized)]
			public AppVerifier.InvocationInfo GetFirstInvocationInfo(out int totalInvocationCount)
			{
				totalInvocationCount = this._totalInvocationCount;
				return this._firstInvocationInfo;
			}

			// Token: 0x06006934 RID: 26932 RVA: 0x001764F8 File Offset: 0x001746F8
			[MethodImpl(MethodImplOptions.Synchronized)]
			public int RecordInvocation(bool captureCallStack)
			{
				this._totalInvocationCount++;
				if (this._firstInvocationInfo == null)
				{
					this._firstInvocationInfo = AppVerifier.InvocationInfo.Capture(captureCallStack);
				}
				return this._totalInvocationCount;
			}

			// Token: 0x04003767 RID: 14183
			private AppVerifier.InvocationInfo _firstInvocationInfo;

			// Token: 0x04003768 RID: 14184
			private int _totalInvocationCount;
		}

		// Token: 0x02000928 RID: 2344
		private sealed class Holder<T>
		{
			// Token: 0x06006936 RID: 26934 RVA: 0x00176522 File Offset: 0x00174722
			public Holder(T value)
			{
				this.Value = value;
			}

			// Token: 0x04003769 RID: 14185
			public readonly T Value;
		}

		// Token: 0x02000929 RID: 2345
		private sealed class InvocationInfo
		{
			// Token: 0x06006937 RID: 26935 RVA: 0x00176531 File Offset: 0x00174731
			private InvocationInfo(bool captureStack)
			{
				this.ThreadId = Thread.CurrentThread.ManagedThreadId;
				this.Timestamp = DateTimeOffset.UtcNow;
				this.StackTrace = (captureStack ? AppVerifier.InvocationInfo.CaptureStackTrace() : "n/a");
			}

			// Token: 0x06006938 RID: 26936 RVA: 0x00176569 File Offset: 0x00174769
			public static AppVerifier.InvocationInfo Capture(bool captureStack)
			{
				return new AppVerifier.InvocationInfo(captureStack);
			}

			// Token: 0x06006939 RID: 26937 RVA: 0x00176574 File Offset: 0x00174774
			private static string CaptureStackTrace()
			{
				StackTrace stackTrace = new StackTrace(true);
				string[] array = stackTrace.ToString().Split(new string[]
				{
					Environment.NewLine
				}, StringSplitOptions.None);
				int num = 0;
				while (num < stackTrace.FrameCount && num < array.Length)
				{
					StackFrame frame = stackTrace.GetFrame(num);
					if (!(frame.GetMethod().Module == typeof(AppVerifier).Module) || !frame.GetMethod().DeclaringType.FullName.StartsWith("System.Web.Util.AppVerifier", StringComparison.Ordinal))
					{
						return string.Join(Environment.NewLine, array.Skip(num));
					}
					num++;
				}
				return stackTrace.ToString();
			}

			// Token: 0x0400376A RID: 14186
			public readonly int ThreadId;

			// Token: 0x0400376B RID: 14187
			public readonly DateTimeOffset Timestamp;

			// Token: 0x0400376C RID: 14188
			public readonly string StackTrace;
		}

		// Token: 0x0200092A RID: 2346
		[SuppressUnmanagedCodeSecurity]
		private static class NativeMethods
		{
			// Token: 0x0600693A RID: 26938
			[DllImport("kernel32.dll")]
			internal static extern void DebugBreak();
		}
	}
}
