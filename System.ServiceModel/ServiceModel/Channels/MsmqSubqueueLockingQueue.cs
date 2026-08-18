using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008F8 RID: 2296
	internal class MsmqSubqueueLockingQueue : MsmqQueue, ILockingQueue
	{
		// Token: 0x06005797 RID: 22423 RVA: 0x00141530 File Offset: 0x0013F730
		public MsmqSubqueueLockingQueue(string formatName, string hostname, int accessMode) : base(formatName, accessMode)
		{
			if (string.Compare(hostname, string.Empty, StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.validHostName = MsmqSubqueueLockingQueue.TryGetHostName(formatName, out hostname);
			}
			else
			{
				this.validHostName = true;
			}
			this.disposed = false;
			this.lockQueueName = this.formatName + ";" + MsmqSubqueueLockingQueue.GenerateLockQueueName();
			this.lockQueueForReceive = new MsmqQueue(this.lockQueueName, 1, 1);
			this.lockQueueForMove = new MsmqQueue(this.lockQueueName, 4);
			this.mainQueueForMove = new MsmqQueue(this.formatName, 4);
			this.lockCollectionTimer = new IOThreadTimer(new Action<object>(this.OnCollectionTimer), null, false);
			if (string.Compare(hostname, "localhost", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.hostname = null;
				return;
			}
			this.hostname = hostname;
		}

		// Token: 0x06005798 RID: 22424 RVA: 0x00141618 File Offset: 0x0013F818
		private static string GenerateLockQueueName()
		{
			string text = Guid.NewGuid().ToString();
			return "lock_" + text.Substring(text.Length - 8, 8);
		}

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x06005799 RID: 22425 RVA: 0x00141652 File Offset: 0x0013F852
		public MsmqQueue LockQueueForReceive
		{
			get
			{
				return this.lockQueueForReceive;
			}
		}

		// Token: 0x0600579A RID: 22426 RVA: 0x0014165C File Offset: 0x0013F85C
		internal override MsmqQueueHandle OpenQueue()
		{
			if (!this.validHostName)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqOpenError", new object[]
				{
					MsmqError.GetErrorString(-1072824288)
				}), -1072824288));
			}
			this.EnsureLockQueuesOpen();
			this.mainQueueForMove.EnsureOpen();
			this.OnCollectionTimer(null);
			return base.OpenQueue();
		}

		// Token: 0x0600579B RID: 22427 RVA: 0x001416C4 File Offset: 0x0013F8C4
		internal void EnsureLockQueuesOpen()
		{
			int num = 0;
			for (;;)
			{
				try
				{
					this.lockQueueForReceive.EnsureOpen();
					break;
				}
				catch (MsmqException ex)
				{
					if (num >= 3)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
					}
					MsmqDiagnostics.ExpectedException(ex);
				}
				this.lockQueueForReceive.Dispose();
				this.lockQueueForMove.Dispose();
				this.lockQueueName = this.formatName + ";" + MsmqSubqueueLockingQueue.GenerateLockQueueName();
				this.lockQueueForReceive = new MsmqQueue(this.lockQueueName, 1, 1);
				this.lockQueueForMove = new MsmqQueue(this.lockQueueName, 4);
				num++;
			}
			this.lockQueueForMove.EnsureOpen();
		}

		// Token: 0x0600579C RID: 22428 RVA: 0x00141774 File Offset: 0x0013F974
		public override MsmqQueue.ReceiveResult TryReceive(NativeMsmqMessage message, TimeSpan timeout, MsmqTransactionMode transactionMode)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			bool flag = false;
			long lookupId = 0L;
			while (!flag)
			{
				using (MsmqSubqueueLockingQueue.MsmqMessageLookupId msmqMessageLookupId = new MsmqSubqueueLockingQueue.MsmqMessageLookupId())
				{
					MsmqQueue.ReceiveResult receiveResult = base.TryPeek(msmqMessageLookupId, timeoutHelper.RemainingTime());
					if (receiveResult != MsmqQueue.ReceiveResult.MessageReceived)
					{
						return receiveResult;
					}
					lookupId = msmqMessageLookupId.lookupId.Value;
				}
				try
				{
					MsmqQueue.MoveReceiveResult moveReceiveResult = base.TryMoveMessage(lookupId, this.lockQueueForMove, MsmqTransactionMode.None);
					if (moveReceiveResult == MsmqQueue.MoveReceiveResult.Succeeded)
					{
						flag = true;
					}
				}
				catch (MsmqException ex)
				{
					MsmqDiagnostics.ExpectedException(ex);
				}
			}
			MsmqQueue.MoveReceiveResult moveReceiveResult2;
			try
			{
				moveReceiveResult2 = this.lockQueueForReceive.TryReceiveByLookupId(lookupId, message, MsmqTransactionMode.None, 1073741840);
			}
			catch (MsmqException exception)
			{
				this.UnlockMessage(lookupId, TimeSpan.Zero);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
			if (moveReceiveResult2 == MsmqQueue.MoveReceiveResult.Succeeded)
			{
				return MsmqQueue.ReceiveResult.MessageReceived;
			}
			this.UnlockMessage(lookupId, TimeSpan.Zero);
			return MsmqQueue.ReceiveResult.OperationCancelled;
		}

		// Token: 0x0600579D RID: 22429 RVA: 0x0014186C File Offset: 0x0013FA6C
		public void DeleteMessage(long lookupId, TimeSpan timeout)
		{
			IPostRollbackErrorStrategy postRollbackErrorStrategy = new SimplePostRollbackErrorStrategy(lookupId);
			MsmqQueue.MoveReceiveResult moveReceiveResult;
			do
			{
				using (MsmqEmptyMessage msmqEmptyMessage = new MsmqEmptyMessage())
				{
					moveReceiveResult = this.lockQueueForReceive.TryReceiveByLookupId(lookupId, msmqEmptyMessage, MsmqTransactionMode.CurrentOrNone);
				}
			}
			while (moveReceiveResult == MsmqQueue.MoveReceiveResult.MessageLockedUnderTransaction && postRollbackErrorStrategy.AnotherTryNeeded());
			if (moveReceiveResult != MsmqQueue.MoveReceiveResult.Succeeded)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqReceiveContextMessageNotReceived", new object[]
				{
					lookupId.ToString(CultureInfo.InvariantCulture)
				})));
			}
		}

		// Token: 0x0600579E RID: 22430 RVA: 0x001418F0 File Offset: 0x0013FAF0
		public void UnlockMessage(long lookupId, TimeSpan timeout)
		{
			IPostRollbackErrorStrategy postRollbackErrorStrategy = new SimplePostRollbackErrorStrategy(lookupId);
			MsmqQueue.MoveReceiveResult moveReceiveResult;
			do
			{
				moveReceiveResult = this.lockQueueForReceive.TryMoveMessage(lookupId, this.mainQueueForMove, MsmqTransactionMode.None);
			}
			while (moveReceiveResult == MsmqQueue.MoveReceiveResult.MessageLockedUnderTransaction && postRollbackErrorStrategy.AnotherTryNeeded());
			if (moveReceiveResult != MsmqQueue.MoveReceiveResult.Succeeded)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqReceiveContextMessageNotMoved", new object[]
				{
					lookupId.ToString(CultureInfo.InvariantCulture)
				})));
			}
		}

		// Token: 0x0600579F RID: 22431 RVA: 0x00141958 File Offset: 0x0013FB58
		public override void CloseQueue()
		{
			object obj = this.timerLock;
			lock (obj)
			{
				if (!this.disposed)
				{
					this.disposed = true;
					this.lockCollectionTimer.Cancel();
					this.lockCollectionTimer = null;
				}
			}
			this.CollectLocks(this.lockQueueForReceive);
			this.mainQueueForMove.CloseQueue();
			this.lockQueueForMove.CloseQueue();
			this.lockQueueForReceive.CloseQueue();
			base.CloseQueue();
		}

		// Token: 0x060057A0 RID: 22432 RVA: 0x001419E8 File Offset: 0x0013FBE8
		private void OnCollectionTimer(object state)
		{
			object obj = this.timerLock;
			lock (obj)
			{
				if (!this.disposed)
				{
					List<string> list;
					if (this.TryEnumerateSubqueues(out list))
					{
						foreach (string text in list)
						{
							MsmqQueue lockQueue;
							if (text.StartsWith("lock_", StringComparison.OrdinalIgnoreCase) && this.TryOpenLockQueueForCollection(text, out lockQueue))
							{
								this.CollectLocks(lockQueue);
							}
						}
					}
					this.lockCollectionTimer.Set(this.lockCollectionInterval);
				}
			}
		}

		// Token: 0x060057A1 RID: 22433 RVA: 0x00141AA4 File Offset: 0x0013FCA4
		private bool TryOpenLockQueueForCollection(string subqueueName, out MsmqQueue lockQueue)
		{
			lockQueue = null;
			string formatName = this.formatName + ";" + subqueueName;
			int accessMode = 1;
			int shareMode = 1;
			try
			{
				int num = 0;
				if (MsmqQueue.IsQueueOpenable(formatName, accessMode, shareMode, out num))
				{
					lockQueue = new MsmqQueue(formatName, accessMode, shareMode);
					lockQueue.EnsureOpen();
				}
				else
				{
					if (num == -1072824311 || num == -1072824317)
					{
						return false;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MsmqException(SR.GetString("MsmqOpenError", new object[]
					{
						MsmqError.GetErrorString(num)
					}), num));
				}
			}
			catch (MsmqException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060057A2 RID: 22434 RVA: 0x00141B48 File Offset: 0x0013FD48
		private void CollectLocks(MsmqQueue lockQueue)
		{
			MsmqQueue.ReceiveResult receiveResult = MsmqQueue.ReceiveResult.MessageReceived;
			while (receiveResult == MsmqQueue.ReceiveResult.MessageReceived)
			{
				using (MsmqSubqueueLockingQueue.MsmqMessageLookupId msmqMessageLookupId = new MsmqSubqueueLockingQueue.MsmqMessageLookupId())
				{
					try
					{
						receiveResult = lockQueue.TryPeek(msmqMessageLookupId, TimeSpan.FromSeconds(0.0));
						if (receiveResult == MsmqQueue.ReceiveResult.MessageReceived)
						{
							lockQueue.TryMoveMessage(msmqMessageLookupId.lookupId.Value, this.mainQueueForMove, MsmqTransactionMode.None);
						}
					}
					catch (MsmqException ex)
					{
						MsmqDiagnostics.ExpectedException(ex);
						receiveResult = MsmqQueue.ReceiveResult.Unknown;
					}
				}
			}
		}

		// Token: 0x060057A3 RID: 22435 RVA: 0x00141BCC File Offset: 0x0013FDCC
		private bool TryEnumerateSubqueues(out List<string> subqueues)
		{
			subqueues = new List<string>();
			int[] array = new int[1];
			UnsafeNativeMethods.MQMSGPROPS mqmsgprops = new UnsafeNativeMethods.MQMSGPROPS();
			UnsafeNativeMethods.MQPROPVARIANT mqpropvariant = default(UnsafeNativeMethods.MQPROPVARIANT);
			GCHandle gchandle = GCHandle.Alloc(null, GCHandleType.Pinned);
			GCHandle gchandle2 = GCHandle.Alloc(null, GCHandleType.Pinned);
			GCHandle gchandle3 = GCHandle.Alloc(null, GCHandleType.Pinned);
			mqmsgprops.status = IntPtr.Zero;
			mqmsgprops.count = 1;
			array[0] = 27;
			mqpropvariant.vt = 1;
			try
			{
				gchandle.Target = mqmsgprops;
				gchandle2.Target = array;
				gchandle3.Target = mqpropvariant;
				mqmsgprops.variants = gchandle3.AddrOfPinnedObject();
				mqmsgprops.ids = gchandle2.AddrOfPinnedObject();
				if (UnsafeNativeMethods.MQMgmtGetInfo(this.hostname, "queue=" + this.formatName, gchandle.AddrOfPinnedObject()) != 0)
				{
					return false;
				}
				UnsafeNativeMethods.MQPROPVARIANT mqpropvariant2 = (UnsafeNativeMethods.MQPROPVARIANT)Marshal.PtrToStructure(mqmsgprops.variants, typeof(UnsafeNativeMethods.MQPROPVARIANT));
				IntPtr[] array2 = new IntPtr[mqpropvariant2.stringArraysValue.count];
				Marshal.Copy(mqpropvariant2.stringArraysValue.stringArrays, array2, 0, mqpropvariant2.stringArraysValue.count);
				for (int i = 0; i < mqpropvariant2.stringArraysValue.count; i++)
				{
					subqueues.Add(Marshal.PtrToStringUni(array2[i]));
					UnsafeNativeMethods.MQFreeMemory(array2[i]);
				}
				UnsafeNativeMethods.MQFreeMemory(mqpropvariant2.stringArraysValue.stringArrays);
			}
			finally
			{
				gchandle2.Target = null;
				gchandle.Target = null;
				gchandle3.Target = null;
			}
			return true;
		}

		// Token: 0x060057A4 RID: 22436 RVA: 0x00141D54 File Offset: 0x0013FF54
		private static bool TryGetHostName(string formatName, out string hostName)
		{
			string text = "DIRECT=";
			string value = "TCP:";
			string value2 = "OS:";
			hostName = null;
			if (!formatName.StartsWith(text, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			string text2 = formatName.Substring(text.Length, formatName.Length - text.Length);
			int num = text2.IndexOf(':') + 1;
			string text3 = text2.Substring(num, text2.IndexOf('\\') - num);
			if (text2.StartsWith(value, StringComparison.OrdinalIgnoreCase))
			{
				hostName = text3;
				return true;
			}
			if (text2.StartsWith(value2, StringComparison.OrdinalIgnoreCase))
			{
				if (text3.Equals("."))
				{
					hostName = "localhost";
				}
				else
				{
					hostName = text3;
				}
				return true;
			}
			return false;
		}

		// Token: 0x040035DF RID: 13791
		private string lockQueueName;

		// Token: 0x040035E0 RID: 13792
		private MsmqQueue mainQueueForMove;

		// Token: 0x040035E1 RID: 13793
		private MsmqQueue lockQueueForMove;

		// Token: 0x040035E2 RID: 13794
		private MsmqQueue lockQueueForReceive;

		// Token: 0x040035E3 RID: 13795
		private IOThreadTimer lockCollectionTimer;

		// Token: 0x040035E4 RID: 13796
		private TimeSpan lockCollectionInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x040035E5 RID: 13797
		private object timerLock = new object();

		// Token: 0x040035E6 RID: 13798
		private bool disposed;

		// Token: 0x040035E7 RID: 13799
		private string hostname;

		// Token: 0x040035E8 RID: 13800
		private bool validHostName;

		// Token: 0x040035E9 RID: 13801
		private const string LockSubqueuePrefix = "lock_";

		// Token: 0x02000D98 RID: 3480
		private class MsmqMessageLookupId : NativeMsmqMessage
		{
			// Token: 0x06007EC7 RID: 32455 RVA: 0x001D83EA File Offset: 0x001D65EA
			public MsmqMessageLookupId() : base(1)
			{
				this.lookupId = new NativeMsmqMessage.LongProperty(this, 60);
			}

			// Token: 0x040048C4 RID: 18628
			public NativeMsmqMessage.LongProperty lookupId;
		}
	}
}
