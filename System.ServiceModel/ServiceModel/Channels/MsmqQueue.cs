using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Diagnostics;
using System.Threading;
using System.Transactions;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008FB RID: 2299
	internal class MsmqQueue : IDisposable
	{
		// Token: 0x060057A9 RID: 22441 RVA: 0x00141E3B File Offset: 0x0014003B
		public MsmqQueue(string formatName, int accessMode)
		{
			this.formatName = formatName;
			this.accessMode = accessMode;
			this.shareMode = 0;
		}

		// Token: 0x060057AA RID: 22442 RVA: 0x00141E58 File Offset: 0x00140058
		public MsmqQueue(string formatName, int accessMode, int shareMode)
		{
			this.formatName = formatName;
			this.accessMode = accessMode;
			this.shareMode = shareMode;
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x060057AB RID: 22443 RVA: 0x00141E75 File Offset: 0x00140075
		protected object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x060057AC RID: 22444 RVA: 0x00141E78 File Offset: 0x00140078
		public string FormatName
		{
			get
			{
				return this.formatName;
			}
		}

		// Token: 0x060057AD RID: 22445 RVA: 0x00141E80 File Offset: 0x00140080
		public override string ToString()
		{
			return this.formatName;
		}

		// Token: 0x060057AE RID: 22446 RVA: 0x00141E88 File Offset: 0x00140088
		public void Dispose()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.CloseQueue();
			}
		}

		// Token: 0x060057AF RID: 22447 RVA: 0x00141EC8 File Offset: 0x001400C8
		internal void EnsureOpen()
		{
			this.GetHandle();
		}

		// Token: 0x060057B0 RID: 22448 RVA: 0x00141ED4 File Offset: 0x001400D4
		private MsmqQueueHandle GetHandleForAsync(out bool useCompletionPort)
		{
			object thisLock = this.ThisLock;
			MsmqQueueHandle result;
			lock (thisLock)
			{
				if (this.handle == null)
				{
					this.handle = this.OpenQueue();
				}
				if (!this.isAsyncEnabled)
				{
					if (MsmqQueue.IsCompletionPortSupported(this.handle))
					{
						ThreadPool.BindHandle(this.handle);
						this.isBoundToCompletionPort = true;
					}
					this.isAsyncEnabled = true;
				}
				useCompletionPort = this.isBoundToCompletionPort;
				result = this.handle;
			}
			return result;
		}

		// Token: 0x060057B1 RID: 22449 RVA: 0x00141F64 File Offset: 0x00140164
		protected MsmqQueueHandle GetHandle()
		{
			object thisLock = this.ThisLock;
			MsmqQueueHandle result;
			lock (thisLock)
			{
				if (this.handle == null)
				{
					this.handle = this.OpenQueue();
				}
				result = this.handle;
			}
			return result;
		}

		// Token: 0x060057B2 RID: 22450 RVA: 0x00141FBC File Offset: 0x001401BC
		private static bool IsCompletionPortSupported(MsmqQueueHandle handle)
		{
			int num;
			return UnsafeNativeMethods.GetHandleInformation(handle, out num) != 0;
		}

		// Token: 0x060057B3 RID: 22451 RVA: 0x00141FD4 File Offset: 0x001401D4
		internal virtual MsmqQueueHandle OpenQueue()
		{
			MsmqQueueHandle result;
			int num = UnsafeNativeMethods.MQOpenQueue(this.formatName, this.accessMode, this.shareMode, out result);
			if (num != 0)
			{
				Utility.CloseInvalidOutSafeHandle(result);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqOpenError", new object[]
				{
					MsmqError.GetErrorString(num)
				}), num));
			}
			MsmqDiagnostics.QueueOpened(this.formatName);
			return result;
		}

		// Token: 0x060057B4 RID: 22452 RVA: 0x0014203A File Offset: 0x0014023A
		public virtual void CloseQueue()
		{
			if (this.handle != null)
			{
				this.CloseQueue(this.handle);
				this.handle = null;
				this.isBoundToCompletionPort = false;
				this.isAsyncEnabled = false;
				MsmqDiagnostics.QueueClosed(this.formatName);
			}
		}

		// Token: 0x060057B5 RID: 22453 RVA: 0x00142070 File Offset: 0x00140270
		private void CloseQueue(MsmqQueueHandle handle)
		{
			handle.Dispose();
		}

		// Token: 0x060057B6 RID: 22454 RVA: 0x00142078 File Offset: 0x00140278
		protected void HandleIsStale(MsmqQueueHandle handle)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.handle == handle)
				{
					this.CloseQueue();
				}
			}
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x001420C4 File Offset: 0x001402C4
		public static void GetMsmqInformation(ref Version version, ref bool activeDirectoryEnabled)
		{
			MsmqQueue.PrivateComputerProperties privateComputerProperties = new MsmqQueue.PrivateComputerProperties();
			using (privateComputerProperties)
			{
				IntPtr properties = privateComputerProperties.Pin();
				try
				{
					int num = UnsafeNativeMethods.MQGetPrivateComputerInformation(null, properties);
					if (num != 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqGetPrivateComputerInformationError", new object[]
						{
							MsmqError.GetErrorString(num)
						}), num));
					}
					int value = privateComputerProperties.Version.Value;
					version = new Version(value >> 24, (value & 16711680) >> 16, value & 65535);
					activeDirectoryEnabled = privateComputerProperties.ActiveDirectory.Value;
				}
				finally
				{
					privateComputerProperties.Unpin();
				}
			}
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x00142180 File Offset: 0x00140380
		public static bool IsReadable(string formatName, out MsmqException ex)
		{
			return MsmqQueue.SupportsAccessMode(formatName, 1, out ex);
		}

		// Token: 0x060057B9 RID: 22457 RVA: 0x0014218C File Offset: 0x0014038C
		public static bool IsWriteable(string formatName)
		{
			MsmqException ex;
			return MsmqQueue.SupportsAccessMode(formatName, 2, out ex);
		}

		// Token: 0x060057BA RID: 22458 RVA: 0x001421A4 File Offset: 0x001403A4
		public static bool IsMoveable(string formatName)
		{
			MsmqException ex;
			return MsmqQueue.SupportsAccessMode(formatName, 4, out ex);
		}

		// Token: 0x060057BB RID: 22459 RVA: 0x001421BC File Offset: 0x001403BC
		internal static bool IsQueueOpenable(string formatName, int accessMode, int shareMode, out int error)
		{
			MsmqQueueHandle msmqQueueHandle;
			error = UnsafeNativeMethods.MQOpenQueue(formatName, accessMode, shareMode, out msmqQueueHandle);
			if (error != 0)
			{
				Utility.CloseInvalidOutSafeHandle(msmqQueueHandle);
				return false;
			}
			msmqQueueHandle.Dispose();
			return true;
		}

		// Token: 0x060057BC RID: 22460 RVA: 0x001421E8 File Offset: 0x001403E8
		private static bool SupportsAccessMode(string formatName, int accessType, out MsmqException msmqException)
		{
			msmqException = null;
			try
			{
				using (MsmqQueue msmqQueue = new MsmqQueue(formatName, accessType))
				{
					msmqQueue.GetHandle();
				}
			}
			catch (Exception ex)
			{
				msmqException = (ex as MsmqException);
				if (msmqException != null)
				{
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x060057BD RID: 22461 RVA: 0x00142248 File Offset: 0x00140448
		public static bool TryGetIsTransactional(string formatName, out bool isTransactional)
		{
			bool result;
			using (MsmqQueue.QueueTransactionProperties queueTransactionProperties = new MsmqQueue.QueueTransactionProperties())
			{
				IntPtr properties = queueTransactionProperties.Pin();
				try
				{
					if (UnsafeNativeMethods.MQGetQueueProperties(formatName, properties) == 0)
					{
						isTransactional = (queueTransactionProperties.Transaction.Value > 0);
						result = true;
					}
					else
					{
						isTransactional = false;
						MsmqDiagnostics.QueueTransactionalStatusUnknown(formatName);
						result = false;
					}
				}
				finally
				{
					queueTransactionProperties.Unpin();
				}
			}
			return result;
		}

		// Token: 0x060057BE RID: 22462 RVA: 0x001422BC File Offset: 0x001404BC
		protected static bool IsErrorDueToStaleHandle(int error)
		{
			return error - -1072824314 <= 1 || error == -1072824234 || error == -1072824230;
		}

		// Token: 0x060057BF RID: 22463 RVA: 0x001422DC File Offset: 0x001404DC
		protected static bool IsReceiveErrorDueToInsufficientBuffer(int error)
		{
			if (error <= -1072824280)
			{
				if (error <= -1072824289)
				{
					if (error != -1072824294 && error != -1072824289)
					{
						return false;
					}
				}
				else if (error - -1072824286 > 1 && error != -1072824280)
				{
					return false;
				}
			}
			else if (error <= -1072824250)
			{
				if (error != -1072824277 && error != -1072824250)
				{
					return false;
				}
			}
			else if (error != -1072824226 && error - -1072824223 > 2 && error != 1074659337)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060057C0 RID: 22464 RVA: 0x00142358 File Offset: 0x00140558
		public void MarkMessageRejected(long lookupId)
		{
			MsmqQueueHandle msmqQueueHandle = this.GetHandle();
			int num = 0;
			try
			{
				num = UnsafeNativeMethods.MQMarkMessageRejected(msmqQueueHandle, lookupId);
			}
			catch (ObjectDisposedException ex)
			{
				MsmqDiagnostics.ExpectedException(ex);
			}
			if (num != 0)
			{
				if (MsmqQueue.IsErrorDueToStaleHandle(num))
				{
					this.HandleIsStale(msmqQueueHandle);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqSendError", new object[]
				{
					MsmqError.GetErrorString(num)
				}), num));
			}
		}

		// Token: 0x060057C1 RID: 22465 RVA: 0x001423D0 File Offset: 0x001405D0
		private int TryMoveMessageDtcTransacted(long lookupId, MsmqQueueHandle sourceQueueHandle, MsmqQueueHandle destinationQueueHandle, MsmqTransactionMode transactionMode)
		{
			IDtcTransaction nativeTransaction = this.GetNativeTransaction(transactionMode);
			if (nativeTransaction != null)
			{
				try
				{
					return UnsafeNativeMethods.MQMoveMessage(sourceQueueHandle, destinationQueueHandle, lookupId, nativeTransaction);
				}
				finally
				{
					Marshal.ReleaseComObject(nativeTransaction);
				}
			}
			return UnsafeNativeMethods.MQMoveMessage(sourceQueueHandle, destinationQueueHandle, lookupId, (IntPtr)this.GetTransactionConstant(transactionMode));
		}

		// Token: 0x060057C2 RID: 22466 RVA: 0x00142424 File Offset: 0x00140624
		public MsmqQueue.MoveReceiveResult TryMoveMessage(long lookupId, MsmqQueue destinationQueue, MsmqTransactionMode transactionMode)
		{
			MsmqQueueHandle sourceQueueHandle = this.GetHandle();
			MsmqQueueHandle destinationQueueHandle = destinationQueue.GetHandle();
			int num;
			try
			{
				if (this.RequiresDtcTransaction(transactionMode))
				{
					num = this.TryMoveMessageDtcTransacted(lookupId, sourceQueueHandle, destinationQueueHandle, transactionMode);
				}
				else
				{
					num = UnsafeNativeMethods.MQMoveMessage(sourceQueueHandle, destinationQueueHandle, lookupId, (IntPtr)this.GetTransactionConstant(transactionMode));
				}
			}
			catch (ObjectDisposedException ex)
			{
				MsmqDiagnostics.ExpectedException(ex);
				return MsmqQueue.MoveReceiveResult.Succeeded;
			}
			if (num == 0)
			{
				return MsmqQueue.MoveReceiveResult.Succeeded;
			}
			if (num == -1072824184)
			{
				return MsmqQueue.MoveReceiveResult.MessageNotFound;
			}
			if (num == -1072824164)
			{
				return MsmqQueue.MoveReceiveResult.MessageLockedUnderTransaction;
			}
			if (MsmqQueue.IsErrorDueToStaleHandle(num))
			{
				this.HandleIsStale(sourceQueueHandle);
				destinationQueue.HandleIsStale(destinationQueueHandle);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqSendError", new object[]
			{
				MsmqError.GetErrorString(num)
			}), num));
		}

		// Token: 0x060057C3 RID: 22467 RVA: 0x001424E4 File Offset: 0x001406E4
		public virtual MsmqQueue.ReceiveResult TryReceive(NativeMsmqMessage message, TimeSpan timeout, MsmqTransactionMode transactionMode)
		{
			return this.TryReceiveInternal(message, timeout, transactionMode, 0);
		}

		// Token: 0x060057C4 RID: 22468 RVA: 0x001424F0 File Offset: 0x001406F0
		private MsmqQueue.ReceiveResult TryReceiveInternal(NativeMsmqMessage message, TimeSpan timeout, MsmqTransactionMode transactionMode, int action)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			MsmqQueueHandle msmqQueueHandle = this.GetHandle();
			int num;
			for (;;)
			{
				num = this.ReceiveCore(msmqQueueHandle, message, timeoutHelper.RemainingTime(), transactionMode, action);
				if (num == 0)
				{
					break;
				}
				if (!MsmqQueue.IsReceiveErrorDueToInsufficientBuffer(num))
				{
					goto IL_37;
				}
				message.GrowBuffers();
			}
			return MsmqQueue.ReceiveResult.MessageReceived;
			IL_37:
			if (num == -1072824293)
			{
				return MsmqQueue.ReceiveResult.Timeout;
			}
			if (num == -1072824312)
			{
				return MsmqQueue.ReceiveResult.OperationCancelled;
			}
			if (num == -1072824313)
			{
				return MsmqQueue.ReceiveResult.OperationCancelled;
			}
			if (MsmqQueue.IsErrorDueToStaleHandle(num))
			{
				this.HandleIsStale(msmqQueueHandle);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqReceiveError", new object[]
			{
				MsmqError.GetErrorString(num)
			}), num));
		}

		// Token: 0x060057C5 RID: 22469 RVA: 0x0014258A File Offset: 0x0014078A
		public MsmqQueue.MoveReceiveResult TryReceiveByLookupId(long lookupId, NativeMsmqMessage message, MsmqTransactionMode transactionMode)
		{
			return this.TryReceiveByLookupId(lookupId, message, transactionMode, 1073741856);
		}

		// Token: 0x060057C6 RID: 22470 RVA: 0x0014259C File Offset: 0x0014079C
		public MsmqQueue.MoveReceiveResult TryReceiveByLookupId(long lookupId, NativeMsmqMessage message, MsmqTransactionMode transactionMode, int action)
		{
			MsmqQueueHandle msmqQueueHandle = this.GetHandle();
			int num = 0;
			for (;;)
			{
				try
				{
					num = this.ReceiveByLookupIdCore(msmqQueueHandle, lookupId, message, transactionMode, action);
				}
				catch (ObjectDisposedException ex)
				{
					MsmqDiagnostics.ExpectedException(ex);
					return MsmqQueue.MoveReceiveResult.Succeeded;
				}
				if (num == 0)
				{
					break;
				}
				if (!MsmqQueue.IsReceiveErrorDueToInsufficientBuffer(num))
				{
					goto IL_39;
				}
				message.GrowBuffers();
			}
			return MsmqQueue.MoveReceiveResult.Succeeded;
			IL_39:
			if (-1072824184 == num)
			{
				return MsmqQueue.MoveReceiveResult.MessageNotFound;
			}
			if (-1072824164 == num)
			{
				return MsmqQueue.MoveReceiveResult.MessageLockedUnderTransaction;
			}
			if (MsmqQueue.IsErrorDueToStaleHandle(num))
			{
				this.HandleIsStale(msmqQueueHandle);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqReceiveError", new object[]
			{
				MsmqError.GetErrorString(num)
			}), num));
		}

		// Token: 0x060057C7 RID: 22471 RVA: 0x00142640 File Offset: 0x00140840
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		protected int ReceiveByLookupIdCoreDtcTransacted(MsmqQueueHandle handle, long lookupId, NativeMsmqMessage message, MsmqTransactionMode transactionMode, int action)
		{
			IDtcTransaction nativeTransaction = this.GetNativeTransaction(transactionMode);
			IntPtr properties = message.Pin();
			int result;
			try
			{
				if (nativeTransaction != null)
				{
					try
					{
						return UnsafeNativeMethods.MQReceiveMessageByLookupId(handle, lookupId, action, properties, null, IntPtr.Zero, nativeTransaction);
					}
					finally
					{
						Marshal.ReleaseComObject(nativeTransaction);
					}
				}
				result = UnsafeNativeMethods.MQReceiveMessageByLookupId(handle, lookupId, action, properties, null, IntPtr.Zero, (IntPtr)this.GetTransactionConstant(transactionMode));
			}
			finally
			{
				message.Unpin();
			}
			return result;
		}

		// Token: 0x060057C8 RID: 22472 RVA: 0x001426C4 File Offset: 0x001408C4
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private int ReceiveByLookupIdCore(MsmqQueueHandle handle, long lookupId, NativeMsmqMessage message, MsmqTransactionMode transactionMode, int action)
		{
			if (this.RequiresDtcTransaction(transactionMode))
			{
				return this.ReceiveByLookupIdCoreDtcTransacted(handle, lookupId, message, transactionMode, action);
			}
			IntPtr properties = message.Pin();
			int result;
			try
			{
				result = UnsafeNativeMethods.MQReceiveMessageByLookupId(handle, lookupId, action, properties, null, IntPtr.Zero, (IntPtr)this.GetTransactionConstant(transactionMode));
			}
			finally
			{
				message.Unpin();
			}
			return result;
		}

		// Token: 0x060057C9 RID: 22473 RVA: 0x0014272C File Offset: 0x0014092C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private int ReceiveCoreDtcTransacted(MsmqQueueHandle handle, NativeMsmqMessage message, TimeSpan timeout, MsmqTransactionMode transactionMode, int action)
		{
			IDtcTransaction nativeTransaction = this.GetNativeTransaction(transactionMode);
			int timeout2 = TimeoutHelper.ToMilliseconds(timeout);
			IntPtr properties = message.Pin();
			int result;
			try
			{
				if (nativeTransaction != null)
				{
					try
					{
						return UnsafeNativeMethods.MQReceiveMessage(handle.DangerousGetHandle(), timeout2, action, properties, null, IntPtr.Zero, IntPtr.Zero, nativeTransaction);
					}
					finally
					{
						Marshal.ReleaseComObject(nativeTransaction);
					}
				}
				result = UnsafeNativeMethods.MQReceiveMessage(handle.DangerousGetHandle(), timeout2, action, properties, null, IntPtr.Zero, IntPtr.Zero, (IntPtr)this.GetTransactionConstant(transactionMode));
			}
			finally
			{
				message.Unpin();
			}
			return result;
		}

		// Token: 0x060057CA RID: 22474 RVA: 0x001427CC File Offset: 0x001409CC
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private int ReceiveCore(MsmqQueueHandle handle, NativeMsmqMessage message, TimeSpan timeout, MsmqTransactionMode transactionMode, int action)
		{
			if (this.RequiresDtcTransaction(transactionMode))
			{
				return this.ReceiveCoreDtcTransacted(handle, message, timeout, transactionMode, action);
			}
			int timeout2 = TimeoutHelper.ToMilliseconds(timeout);
			IntPtr properties = message.Pin();
			int result;
			try
			{
				result = UnsafeNativeMethods.MQReceiveMessage(handle.DangerousGetHandle(), timeout2, action, properties, null, IntPtr.Zero, IntPtr.Zero, (IntPtr)this.GetTransactionConstant(transactionMode));
			}
			finally
			{
				message.Unpin();
			}
			return result;
		}

		// Token: 0x060057CB RID: 22475 RVA: 0x00142844 File Offset: 0x00140A44
		protected IDtcTransaction GetNativeTransaction(MsmqTransactionMode transactionMode)
		{
			Transaction transaction = Transaction.Current;
			if (transaction != null)
			{
				return TransactionInterop.GetDtcTransaction(transaction);
			}
			if (transactionMode == MsmqTransactionMode.CurrentOrThrow)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqTransactionRequired")));
			}
			return null;
		}

		// Token: 0x060057CC RID: 22476 RVA: 0x00142886 File Offset: 0x00140A86
		public MsmqQueue.ReceiveResult TryPeek(NativeMsmqMessage message, TimeSpan timeout)
		{
			return this.TryReceiveInternal(message, timeout, MsmqTransactionMode.None, int.MinValue);
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x00142896 File Offset: 0x00140A96
		private bool RequiresDtcTransaction(MsmqTransactionMode transactionMode)
		{
			if (transactionMode <= MsmqTransactionMode.Single)
			{
				return false;
			}
			if (transactionMode - MsmqTransactionMode.CurrentOrSingle > 2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("transactionMode"));
			}
			return true;
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x001428BC File Offset: 0x00140ABC
		private int GetTransactionConstant(MsmqTransactionMode transactionMode)
		{
			switch (transactionMode)
			{
			case MsmqTransactionMode.None:
			case MsmqTransactionMode.CurrentOrNone:
				return 0;
			case MsmqTransactionMode.Single:
			case MsmqTransactionMode.CurrentOrSingle:
				return 3;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("transactionMode"));
			}
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x001428F0 File Offset: 0x00140AF0
		private int SendDtcTransacted(NativeMsmqMessage message, MsmqTransactionMode transactionMode)
		{
			IDtcTransaction nativeTransaction = this.GetNativeTransaction(transactionMode);
			MsmqQueueHandle msmqQueueHandle = this.GetHandle();
			IntPtr properties = message.Pin();
			int result;
			try
			{
				if (nativeTransaction != null)
				{
					try
					{
						return UnsafeNativeMethods.MQSendMessage(msmqQueueHandle, properties, nativeTransaction);
					}
					finally
					{
						Marshal.ReleaseComObject(nativeTransaction);
					}
				}
				result = UnsafeNativeMethods.MQSendMessage(msmqQueueHandle, properties, (IntPtr)this.GetTransactionConstant(transactionMode));
			}
			finally
			{
				message.Unpin();
			}
			return result;
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x00142964 File Offset: 0x00140B64
		public void Send(NativeMsmqMessage message, MsmqTransactionMode transactionMode)
		{
			int num = 0;
			if (this.RequiresDtcTransaction(transactionMode))
			{
				num = this.SendDtcTransacted(message, transactionMode);
			}
			else
			{
				MsmqQueueHandle msmqQueueHandle = this.GetHandle();
				IntPtr properties = message.Pin();
				try
				{
					num = UnsafeNativeMethods.MQSendMessage(msmqQueueHandle, properties, (IntPtr)this.GetTransactionConstant(transactionMode));
				}
				finally
				{
					message.Unpin();
				}
			}
			if (num != 0)
			{
				if (MsmqQueue.IsErrorDueToStaleHandle(num))
				{
					this.HandleIsStale(this.handle);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqSendError", new object[]
				{
					MsmqError.GetErrorString(num)
				}), num));
			}
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x00142A04 File Offset: 0x00140C04
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private unsafe int ReceiveCoreAsync(MsmqQueueHandle handle, IntPtr nativePropertiesPointer, TimeSpan timeout, int action, NativeOverlapped* nativeOverlapped, UnsafeNativeMethods.MQReceiveCallback receiveCallback)
		{
			int timeout2 = TimeoutHelper.ToMilliseconds(timeout);
			return UnsafeNativeMethods.MQReceiveMessage(handle, timeout2, action, nativePropertiesPointer, nativeOverlapped, receiveCallback, IntPtr.Zero, (IntPtr)0);
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x00142A31 File Offset: 0x00140C31
		public IAsyncResult BeginTryReceive(NativeMsmqMessage message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new MsmqQueue.TryReceiveAsyncResult(this, message, timeout, 0, callback, state);
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x00142A3F File Offset: 0x00140C3F
		public MsmqQueue.ReceiveResult EndTryReceive(IAsyncResult result)
		{
			return MsmqQueue.TryReceiveAsyncResult.End(result);
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x00142A47 File Offset: 0x00140C47
		public IAsyncResult BeginPeek(NativeMsmqMessage message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new MsmqQueue.TryReceiveAsyncResult(this, message, timeout, int.MinValue, callback, state);
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x00142A59 File Offset: 0x00140C59
		public MsmqQueue.ReceiveResult EndPeek(IAsyncResult result)
		{
			return MsmqQueue.TryReceiveAsyncResult.End(result);
		}

		// Token: 0x040035F3 RID: 13811
		private MsmqQueueHandle handle;

		// Token: 0x040035F4 RID: 13812
		private bool isBoundToCompletionPort;

		// Token: 0x040035F5 RID: 13813
		private bool isAsyncEnabled;

		// Token: 0x040035F6 RID: 13814
		protected int shareMode;

		// Token: 0x040035F7 RID: 13815
		protected string formatName;

		// Token: 0x040035F8 RID: 13816
		protected int accessMode;

		// Token: 0x02000D99 RID: 3481
		private class TryReceiveAsyncResult : AsyncResult
		{
			// Token: 0x06007EC8 RID: 32456 RVA: 0x001D8401 File Offset: 0x001D6601
			[SecuritySafeCritical]
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			public TryReceiveAsyncResult(MsmqQueue msmqQueue, NativeMsmqMessage message, TimeSpan timeout, int action, AsyncCallback callback, object state) : base(callback, state)
			{
				this.msmqQueue = msmqQueue;
				this.message = message;
				this.action = action;
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.StartReceive(true);
			}

			// Token: 0x06007EC9 RID: 32457 RVA: 0x001D8440 File Offset: 0x001D6640
			[SecuritySafeCritical]
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			~TryReceiveAsyncResult()
			{
				if (null != this.nativeOverlapped && !Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
				{
					Overlapped.Free(this.nativeOverlapped);
				}
			}

			// Token: 0x06007ECA RID: 32458 RVA: 0x001D8490 File Offset: 0x001D6690
			[SecuritySafeCritical]
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			private unsafe void StartReceive(bool synchronously)
			{
				bool flag;
				try
				{
					this.handle = this.msmqQueue.GetHandleForAsync(out flag);
				}
				catch (MsmqException ex)
				{
					this.OnCompletion(ex.ErrorCode, synchronously);
					return;
				}
				UIntPtr uintPtr = (UIntPtr)0;
				NativeOverlapped* ptr = this.nativeOverlapped;
				IntPtr nativePropertiesPointer = this.message.Pin();
				this.nativeOverlapped = new Overlapped(0, 0, IntPtr.Zero, this).UnsafePack(MsmqQueue.TryReceiveAsyncResult.onPortedCompletion, this.message.GetBuffersForAsync());
				int num;
				try
				{
					if (flag)
					{
						num = this.msmqQueue.ReceiveCoreAsync(this.handle, nativePropertiesPointer, this.timeoutHelper.RemainingTime(), this.action, this.nativeOverlapped, null);
					}
					else
					{
						if (MsmqQueue.TryReceiveAsyncResult.onNonPortedCompletion == null)
						{
							MsmqQueue.TryReceiveAsyncResult.onNonPortedCompletion = new UnsafeNativeMethods.MQReceiveCallback(MsmqQueue.TryReceiveAsyncResult.OnNonPortedCompletion);
						}
						num = this.msmqQueue.ReceiveCoreAsync(this.handle, nativePropertiesPointer, this.timeoutHelper.RemainingTime(), this.action, this.nativeOverlapped, MsmqQueue.TryReceiveAsyncResult.onNonPortedCompletion);
					}
				}
				catch (ObjectDisposedException ex2)
				{
					MsmqDiagnostics.ExpectedException(ex2);
					num = -1072824312;
				}
				if (num != 0 && num != 1074659334)
				{
					Overlapped.Free(this.nativeOverlapped);
					this.nativeOverlapped = null;
					GC.SuppressFinalize(this);
					this.OnCompletion(num, synchronously);
				}
			}

			// Token: 0x06007ECB RID: 32459 RVA: 0x001D85D8 File Offset: 0x001D67D8
			[SecuritySafeCritical]
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			private unsafe static void OnNonPortedCompletion(int error, IntPtr handle, int timeout, int action, IntPtr props, NativeOverlapped* nativeOverlapped, IntPtr cursor)
			{
				ThreadPool.UnsafeQueueNativeOverlapped(nativeOverlapped);
			}

			// Token: 0x06007ECC RID: 32460 RVA: 0x001D85E4 File Offset: 0x001D67E4
			[SecuritySafeCritical]
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			private unsafe static void OnPortedCompletion(uint error, uint numBytes, NativeOverlapped* nativeOverlapped)
			{
				Overlapped overlapped = Overlapped.Unpack(nativeOverlapped);
				MsmqQueue.TryReceiveAsyncResult tryReceiveAsyncResult = (MsmqQueue.TryReceiveAsyncResult)overlapped.AsyncResult;
				if (error != 0U)
				{
					error = (uint)UnsafeNativeMethods.MQGetOverlappedResult(nativeOverlapped);
				}
				Overlapped.Free(nativeOverlapped);
				tryReceiveAsyncResult.nativeOverlapped = null;
				GC.SuppressFinalize(tryReceiveAsyncResult);
				tryReceiveAsyncResult.OnCompletion((int)error, false);
			}

			// Token: 0x06007ECD RID: 32461 RVA: 0x001D862C File Offset: 0x001D682C
			private void OnCompletion(int error, bool completedSynchronously)
			{
				Exception exception = null;
				this.receiveResult = MsmqQueue.ReceiveResult.MessageReceived;
				try
				{
					if (error != 0)
					{
						if (error == -1072824293)
						{
							this.receiveResult = MsmqQueue.ReceiveResult.Timeout;
						}
						else if (error == -1072824312)
						{
							this.receiveResult = MsmqQueue.ReceiveResult.OperationCancelled;
						}
						else
						{
							if (MsmqQueue.IsReceiveErrorDueToInsufficientBuffer(error))
							{
								this.message.Unpin();
								this.message.GrowBuffers();
								this.StartReceive(completedSynchronously);
								return;
							}
							if (MsmqQueue.IsErrorDueToStaleHandle(error))
							{
								this.msmqQueue.HandleIsStale(this.handle);
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqReceiveError", new object[]
							{
								MsmqError.GetErrorString(error)
							}), error));
						}
					}
				}
				catch (Exception ex)
				{
					if (ex is NullReferenceException || ex is SEHException)
					{
						throw;
					}
					exception = ex;
				}
				this.message.Unpin();
				base.Complete(completedSynchronously, exception);
			}

			// Token: 0x06007ECE RID: 32462 RVA: 0x001D8710 File Offset: 0x001D6910
			public static MsmqQueue.ReceiveResult End(IAsyncResult result)
			{
				MsmqQueue.TryReceiveAsyncResult tryReceiveAsyncResult = AsyncResult.End<MsmqQueue.TryReceiveAsyncResult>(result);
				return tryReceiveAsyncResult.receiveResult;
			}

			// Token: 0x040048C5 RID: 18629
			private MsmqQueue msmqQueue;

			// Token: 0x040048C6 RID: 18630
			private int action;

			// Token: 0x040048C7 RID: 18631
			private TimeoutHelper timeoutHelper;

			// Token: 0x040048C8 RID: 18632
			private NativeMsmqMessage message;

			// Token: 0x040048C9 RID: 18633
			private unsafe NativeOverlapped* nativeOverlapped = null;

			// Token: 0x040048CA RID: 18634
			private MsmqQueueHandle handle;

			// Token: 0x040048CB RID: 18635
			private MsmqQueue.ReceiveResult receiveResult;

			// Token: 0x040048CC RID: 18636
			private static IOCompletionCallback onPortedCompletion = Fx.ThunkCallback(new IOCompletionCallback(MsmqQueue.TryReceiveAsyncResult.OnPortedCompletion));

			// Token: 0x040048CD RID: 18637
			private static UnsafeNativeMethods.MQReceiveCallback onNonPortedCompletion;
		}

		// Token: 0x02000D9A RID: 3482
		private class QueueTransactionProperties : NativeMsmqMessage
		{
			// Token: 0x06007ED0 RID: 32464 RVA: 0x001D8742 File Offset: 0x001D6942
			public QueueTransactionProperties() : base(1)
			{
				this.transaction = new NativeMsmqMessage.ByteProperty(this, 113);
			}

			// Token: 0x17001C3B RID: 7227
			// (get) Token: 0x06007ED1 RID: 32465 RVA: 0x001D8759 File Offset: 0x001D6959
			public NativeMsmqMessage.ByteProperty Transaction
			{
				get
				{
					return this.transaction;
				}
			}

			// Token: 0x040048CE RID: 18638
			private NativeMsmqMessage.ByteProperty transaction;
		}

		// Token: 0x02000D9B RID: 3483
		private class PrivateComputerProperties : NativeMsmqMessage
		{
			// Token: 0x06007ED2 RID: 32466 RVA: 0x001D8761 File Offset: 0x001D6961
			public PrivateComputerProperties() : base(2)
			{
				this.version = new NativeMsmqMessage.IntProperty(this, 5801);
				this.activeDirectory = new NativeMsmqMessage.BooleanProperty(this, 5802);
			}

			// Token: 0x17001C3C RID: 7228
			// (get) Token: 0x06007ED3 RID: 32467 RVA: 0x001D878C File Offset: 0x001D698C
			public NativeMsmqMessage.IntProperty Version
			{
				get
				{
					return this.version;
				}
			}

			// Token: 0x17001C3D RID: 7229
			// (get) Token: 0x06007ED4 RID: 32468 RVA: 0x001D8794 File Offset: 0x001D6994
			public NativeMsmqMessage.BooleanProperty ActiveDirectory
			{
				get
				{
					return this.activeDirectory;
				}
			}

			// Token: 0x040048CF RID: 18639
			private NativeMsmqMessage.IntProperty version;

			// Token: 0x040048D0 RID: 18640
			private NativeMsmqMessage.BooleanProperty activeDirectory;
		}

		// Token: 0x02000D9C RID: 3484
		public enum MoveReceiveResult
		{
			// Token: 0x040048D2 RID: 18642
			Unknown,
			// Token: 0x040048D3 RID: 18643
			Succeeded,
			// Token: 0x040048D4 RID: 18644
			MessageNotFound,
			// Token: 0x040048D5 RID: 18645
			MessageLockedUnderTransaction
		}

		// Token: 0x02000D9D RID: 3485
		internal enum ReceiveResult
		{
			// Token: 0x040048D7 RID: 18647
			Unknown,
			// Token: 0x040048D8 RID: 18648
			MessageReceived,
			// Token: 0x040048D9 RID: 18649
			Timeout,
			// Token: 0x040048DA RID: 18650
			OperationCancelled
		}
	}
}
