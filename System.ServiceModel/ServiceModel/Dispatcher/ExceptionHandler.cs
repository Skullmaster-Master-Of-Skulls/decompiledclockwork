using System;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200045F RID: 1119
	public abstract class ExceptionHandler
	{
		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x000A978C File Offset: 0x000A798C
		public static ExceptionHandler AlwaysHandle
		{
			get
			{
				return ExceptionHandler.alwaysHandle;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000A9794 File Offset: 0x000A7994
		// (set) Token: 0x06002B3C RID: 11068 RVA: 0x000A97B7 File Offset: 0x000A79B7
		public static ExceptionHandler AsynchronousThreadExceptionHandler
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				ExceptionHandler.HandlerWrapper handlerWrapper = (ExceptionHandler.HandlerWrapper)Fx.AsynchronousThreadExceptionHandler;
				if (handlerWrapper != null)
				{
					return handlerWrapper.Handler;
				}
				return null;
			}
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
			set
			{
				Fx.AsynchronousThreadExceptionHandler = ((value == null) ? null : new ExceptionHandler.HandlerWrapper(value));
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x000A97CA File Offset: 0x000A79CA
		// (set) Token: 0x06002B3E RID: 11070 RVA: 0x000A97D1 File Offset: 0x000A79D1
		public static ExceptionHandler TransportExceptionHandler
		{
			get
			{
				return ExceptionHandler.transportExceptionHandler;
			}
			set
			{
				ExceptionHandler.transportExceptionHandler = value;
			}
		}

		// Token: 0x06002B3F RID: 11071
		public abstract bool HandleException(Exception exception);

		// Token: 0x06002B40 RID: 11072 RVA: 0x000A97DC File Offset: 0x000A79DC
		internal static bool HandleTransportExceptionHelper(Exception exception)
		{
			if (exception == null)
			{
				throw Fx.AssertAndThrow("Null exception passed to HandleTransportExceptionHelper.");
			}
			ExceptionHandler exceptionHandler = ExceptionHandler.TransportExceptionHandler;
			if (exceptionHandler == null)
			{
				return false;
			}
			try
			{
				if (!exceptionHandler.HandleException(exception))
				{
					return false;
				}
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Error);
				return false;
			}
			DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
			return true;
		}

		// Token: 0x0400240F RID: 9231
		private static readonly ExceptionHandler alwaysHandle = new ExceptionHandler.AlwaysHandleExceptionHandler();

		// Token: 0x04002410 RID: 9232
		private static ExceptionHandler transportExceptionHandler = ExceptionHandler.alwaysHandle;

		// Token: 0x02000C32 RID: 3122
		private class AlwaysHandleExceptionHandler : ExceptionHandler
		{
			// Token: 0x0600773A RID: 30522 RVA: 0x001BDB0E File Offset: 0x001BBD0E
			public override bool HandleException(Exception exception)
			{
				return true;
			}
		}

		// Token: 0x02000C33 RID: 3123
		private class HandlerWrapper : Fx.ExceptionHandler
		{
			// Token: 0x0600773C RID: 30524 RVA: 0x001BDB19 File Offset: 0x001BBD19
			[SecurityCritical]
			public HandlerWrapper(ExceptionHandler handler)
			{
				this.handler = handler;
			}

			// Token: 0x17001B49 RID: 6985
			// (get) Token: 0x0600773D RID: 30525 RVA: 0x001BDB28 File Offset: 0x001BBD28
			public ExceptionHandler Handler
			{
				[SecuritySafeCritical]
				[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
				get
				{
					return this.handler;
				}
			}

			// Token: 0x0600773E RID: 30526 RVA: 0x001BDB30 File Offset: 0x001BBD30
			[SecuritySafeCritical]
			public override bool HandleException(Exception exception)
			{
				return this.handler.HandleException(exception);
			}

			// Token: 0x04004432 RID: 17458
			[SecurityCritical]
			private readonly ExceptionHandler handler;
		}
	}
}
