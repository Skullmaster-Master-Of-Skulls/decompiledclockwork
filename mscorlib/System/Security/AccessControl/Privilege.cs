using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x0200092F RID: 2351
	internal sealed class Privilege
	{
		// Token: 0x060054DC RID: 21724 RVA: 0x00133BB8 File Offset: 0x00132BB8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private static Win32Native.LUID LuidFromPrivilege(string privilege)
		{
			Win32Native.LUID luid;
			luid.LowPart = 0U;
			luid.HighPart = 0U;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Privilege.privilegeLock.AcquireReaderLock(-1);
				if (Privilege.luids.Contains(privilege))
				{
					luid = (Win32Native.LUID)Privilege.luids[privilege];
					Privilege.privilegeLock.ReleaseReaderLock();
				}
				else
				{
					Privilege.privilegeLock.ReleaseReaderLock();
					if (!Win32Native.LookupPrivilegeValue(null, privilege, ref luid))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error == 8)
						{
							throw new OutOfMemoryException();
						}
						if (lastWin32Error == 5)
						{
							throw new UnauthorizedAccessException();
						}
						if (lastWin32Error == 1313)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPrivilegeName", new object[]
							{
								privilege
							}));
						}
						throw new InvalidOperationException();
					}
					else
					{
						Privilege.privilegeLock.AcquireWriterLock(-1);
					}
				}
			}
			finally
			{
				if (Privilege.privilegeLock.IsReaderLockHeld)
				{
					Privilege.privilegeLock.ReleaseReaderLock();
				}
				if (Privilege.privilegeLock.IsWriterLockHeld)
				{
					if (!Privilege.luids.Contains(privilege))
					{
						Privilege.luids[privilege] = luid;
						Privilege.privileges[luid] = privilege;
					}
					Privilege.privilegeLock.ReleaseWriterLock();
				}
			}
			return luid;
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x00133CE4 File Offset: 0x00132CE4
		public Privilege(string privilegeName)
		{
			if (!WindowsIdentity.RunningOnWin2K)
			{
				throw new NotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresNT"));
			}
			if (privilegeName == null)
			{
				throw new ArgumentNullException("privilegeName");
			}
			this.luid = Privilege.LuidFromPrivilege(privilegeName);
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x00133D34 File Offset: 0x00132D34
		~Privilege()
		{
			if (this.needToRevert)
			{
				this.Revert();
			}
		}

		// Token: 0x060054DF RID: 21727 RVA: 0x00133D68 File Offset: 0x00132D68
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void Enable()
		{
			this.ToggleState(true);
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x060054E0 RID: 21728 RVA: 0x00133D71 File Offset: 0x00132D71
		public bool NeedToRevert
		{
			get
			{
				return this.needToRevert;
			}
		}

		// Token: 0x060054E1 RID: 21729 RVA: 0x00133D7C File Offset: 0x00132D7C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private void ToggleState(bool enable)
		{
			int num = 0;
			if (!this.currentThread.Equals(Thread.CurrentThread))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustBeSameThread"));
			}
			if (this.needToRevert)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustRevertPrivilege"));
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				try
				{
					this.tlsContents = (Thread.GetData(Privilege.tlsSlot) as Privilege.TlsContents);
					if (this.tlsContents == null)
					{
						this.tlsContents = new Privilege.TlsContents();
						Thread.SetData(Privilege.tlsSlot, this.tlsContents);
					}
					else
					{
						this.tlsContents.IncrementReferenceCount();
					}
					Win32Native.TOKEN_PRIVILEGE token_PRIVILEGE = default(Win32Native.TOKEN_PRIVILEGE);
					token_PRIVILEGE.PrivilegeCount = 1U;
					token_PRIVILEGE.Privilege.Luid = this.luid;
					token_PRIVILEGE.Privilege.Attributes = (enable ? 2U : 0U);
					Win32Native.TOKEN_PRIVILEGE token_PRIVILEGE2 = default(Win32Native.TOKEN_PRIVILEGE);
					uint num2 = 0U;
					if (!Win32Native.AdjustTokenPrivileges(this.tlsContents.ThreadHandle, false, ref token_PRIVILEGE, (uint)Marshal.SizeOf(token_PRIVILEGE2), ref token_PRIVILEGE2, ref num2))
					{
						num = Marshal.GetLastWin32Error();
					}
					else if (1300 == Marshal.GetLastWin32Error())
					{
						num = 1300;
					}
					else
					{
						this.initialState = ((token_PRIVILEGE2.Privilege.Attributes & 2U) != 0U);
						this.stateWasChanged = (this.initialState != enable);
						this.needToRevert = (this.tlsContents.IsImpersonating || this.stateWasChanged);
					}
				}
				finally
				{
					if (!this.needToRevert)
					{
						this.Reset();
					}
				}
			}
			if (num == 1300)
			{
				throw new PrivilegeNotHeldException(Privilege.privileges[this.luid] as string);
			}
			if (num == 8)
			{
				throw new OutOfMemoryException();
			}
			if (num == 5 || num == 1347)
			{
				throw new UnauthorizedAccessException();
			}
			if (num != 0)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060054E2 RID: 21730 RVA: 0x00133F70 File Offset: 0x00132F70
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void Revert()
		{
			int num = 0;
			if (!this.currentThread.Equals(Thread.CurrentThread))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustBeSameThread"));
			}
			if (!this.NeedToRevert)
			{
				return;
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				bool flag = true;
				try
				{
					if (this.stateWasChanged && (this.tlsContents.ReferenceCountValue > 1 || !this.tlsContents.IsImpersonating))
					{
						Win32Native.TOKEN_PRIVILEGE token_PRIVILEGE = default(Win32Native.TOKEN_PRIVILEGE);
						token_PRIVILEGE.PrivilegeCount = 1U;
						token_PRIVILEGE.Privilege.Luid = this.luid;
						token_PRIVILEGE.Privilege.Attributes = (this.initialState ? 2U : 0U);
						Win32Native.TOKEN_PRIVILEGE token_PRIVILEGE2 = default(Win32Native.TOKEN_PRIVILEGE);
						uint num2 = 0U;
						if (!Win32Native.AdjustTokenPrivileges(this.tlsContents.ThreadHandle, false, ref token_PRIVILEGE, (uint)Marshal.SizeOf(token_PRIVILEGE2), ref token_PRIVILEGE2, ref num2))
						{
							num = Marshal.GetLastWin32Error();
							flag = false;
						}
					}
				}
				finally
				{
					if (flag)
					{
						this.Reset();
					}
				}
			}
			if (num == 8)
			{
				throw new OutOfMemoryException();
			}
			if (num == 5)
			{
				throw new UnauthorizedAccessException();
			}
			if (num != 0)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x00134094 File Offset: 0x00133094
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void Reset()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this.stateWasChanged = false;
				this.initialState = false;
				this.needToRevert = false;
				if (this.tlsContents != null && this.tlsContents.DecrementReferenceCount() == 0)
				{
					this.tlsContents = null;
					Thread.SetData(Privilege.tlsSlot, null);
				}
			}
		}

		// Token: 0x04002BF5 RID: 11253
		public const string CreateToken = "SeCreateTokenPrivilege";

		// Token: 0x04002BF6 RID: 11254
		public const string AssignPrimaryToken = "SeAssignPrimaryTokenPrivilege";

		// Token: 0x04002BF7 RID: 11255
		public const string LockMemory = "SeLockMemoryPrivilege";

		// Token: 0x04002BF8 RID: 11256
		public const string IncreaseQuota = "SeIncreaseQuotaPrivilege";

		// Token: 0x04002BF9 RID: 11257
		public const string UnsolicitedInput = "SeUnsolicitedInputPrivilege";

		// Token: 0x04002BFA RID: 11258
		public const string MachineAccount = "SeMachineAccountPrivilege";

		// Token: 0x04002BFB RID: 11259
		public const string TrustedComputingBase = "SeTcbPrivilege";

		// Token: 0x04002BFC RID: 11260
		public const string Security = "SeSecurityPrivilege";

		// Token: 0x04002BFD RID: 11261
		public const string TakeOwnership = "SeTakeOwnershipPrivilege";

		// Token: 0x04002BFE RID: 11262
		public const string LoadDriver = "SeLoadDriverPrivilege";

		// Token: 0x04002BFF RID: 11263
		public const string SystemProfile = "SeSystemProfilePrivilege";

		// Token: 0x04002C00 RID: 11264
		public const string SystemTime = "SeSystemtimePrivilege";

		// Token: 0x04002C01 RID: 11265
		public const string ProfileSingleProcess = "SeProfileSingleProcessPrivilege";

		// Token: 0x04002C02 RID: 11266
		public const string IncreaseBasePriority = "SeIncreaseBasePriorityPrivilege";

		// Token: 0x04002C03 RID: 11267
		public const string CreatePageFile = "SeCreatePagefilePrivilege";

		// Token: 0x04002C04 RID: 11268
		public const string CreatePermanent = "SeCreatePermanentPrivilege";

		// Token: 0x04002C05 RID: 11269
		public const string Backup = "SeBackupPrivilege";

		// Token: 0x04002C06 RID: 11270
		public const string Restore = "SeRestorePrivilege";

		// Token: 0x04002C07 RID: 11271
		public const string Shutdown = "SeShutdownPrivilege";

		// Token: 0x04002C08 RID: 11272
		public const string Debug = "SeDebugPrivilege";

		// Token: 0x04002C09 RID: 11273
		public const string Audit = "SeAuditPrivilege";

		// Token: 0x04002C0A RID: 11274
		public const string SystemEnvironment = "SeSystemEnvironmentPrivilege";

		// Token: 0x04002C0B RID: 11275
		public const string ChangeNotify = "SeChangeNotifyPrivilege";

		// Token: 0x04002C0C RID: 11276
		public const string RemoteShutdown = "SeRemoteShutdownPrivilege";

		// Token: 0x04002C0D RID: 11277
		public const string Undock = "SeUndockPrivilege";

		// Token: 0x04002C0E RID: 11278
		public const string SyncAgent = "SeSyncAgentPrivilege";

		// Token: 0x04002C0F RID: 11279
		public const string EnableDelegation = "SeEnableDelegationPrivilege";

		// Token: 0x04002C10 RID: 11280
		public const string ManageVolume = "SeManageVolumePrivilege";

		// Token: 0x04002C11 RID: 11281
		public const string Impersonate = "SeImpersonatePrivilege";

		// Token: 0x04002C12 RID: 11282
		public const string CreateGlobal = "SeCreateGlobalPrivilege";

		// Token: 0x04002C13 RID: 11283
		public const string TrustedCredentialManagerAccess = "SeTrustedCredManAccessPrivilege";

		// Token: 0x04002C14 RID: 11284
		public const string ReserveProcessor = "SeReserveProcessorPrivilege";

		// Token: 0x04002C15 RID: 11285
		private static LocalDataStoreSlot tlsSlot = Thread.AllocateDataSlot();

		// Token: 0x04002C16 RID: 11286
		private static Hashtable privileges = new Hashtable();

		// Token: 0x04002C17 RID: 11287
		private static Hashtable luids = new Hashtable();

		// Token: 0x04002C18 RID: 11288
		private static ReaderWriterLock privilegeLock = new ReaderWriterLock();

		// Token: 0x04002C19 RID: 11289
		private bool needToRevert;

		// Token: 0x04002C1A RID: 11290
		private bool initialState;

		// Token: 0x04002C1B RID: 11291
		private bool stateWasChanged;

		// Token: 0x04002C1C RID: 11292
		private Win32Native.LUID luid;

		// Token: 0x04002C1D RID: 11293
		private readonly Thread currentThread = Thread.CurrentThread;

		// Token: 0x04002C1E RID: 11294
		private Privilege.TlsContents tlsContents;

		// Token: 0x02000930 RID: 2352
		private sealed class TlsContents : IDisposable
		{
			// Token: 0x060054E5 RID: 21733 RVA: 0x00134124 File Offset: 0x00133124
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			public TlsContents()
			{
				int num = 0;
				int num2 = 0;
				bool flag = true;
				if (Privilege.TlsContents.processHandle.IsInvalid)
				{
					lock (Privilege.TlsContents.syncRoot)
					{
						if (Privilege.TlsContents.processHandle.IsInvalid && !Win32Native.OpenProcessToken(Win32Native.GetCurrentProcess(), TokenAccessLevels.Duplicate, ref Privilege.TlsContents.processHandle))
						{
							num2 = Marshal.GetLastWin32Error();
							flag = false;
						}
					}
				}
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					try
					{
						SafeTokenHandle safeTokenHandle = this.threadHandle;
						num = Win32.OpenThreadToken(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges, WinSecurityContext.Process, out this.threadHandle);
						num &= 2147024895;
						if (num != 0)
						{
							if (flag)
							{
								this.threadHandle = safeTokenHandle;
								if (num != 1008)
								{
									flag = false;
								}
								if (flag)
								{
									num = 0;
									if (!Win32Native.DuplicateTokenEx(Privilege.TlsContents.processHandle, TokenAccessLevels.Impersonate | TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges, IntPtr.Zero, Win32Native.SECURITY_IMPERSONATION_LEVEL.Impersonation, System.Security.Principal.TokenType.TokenImpersonation, ref this.threadHandle))
									{
										num = Marshal.GetLastWin32Error();
										flag = false;
									}
								}
								if (flag)
								{
									num = Win32.SetThreadToken(this.threadHandle);
									num &= 2147024895;
									if (num != 0)
									{
										flag = false;
									}
								}
								if (flag)
								{
									this.isImpersonating = true;
								}
							}
							else
							{
								num = num2;
							}
						}
						else
						{
							flag = true;
						}
					}
					finally
					{
						if (!flag)
						{
							this.Dispose();
						}
					}
				}
				if (num == 8)
				{
					throw new OutOfMemoryException();
				}
				if (num == 5 || num == 1347)
				{
					throw new UnauthorizedAccessException();
				}
				if (num != 0)
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x060054E6 RID: 21734 RVA: 0x0013428C File Offset: 0x0013328C
			~TlsContents()
			{
				if (!this.disposed)
				{
					this.Dispose(false);
				}
			}

			// Token: 0x060054E7 RID: 21735 RVA: 0x001342C4 File Offset: 0x001332C4
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x060054E8 RID: 21736 RVA: 0x001342D3 File Offset: 0x001332D3
			private void Dispose(bool disposing)
			{
				if (this.disposed)
				{
					return;
				}
				if (disposing && this.threadHandle != null)
				{
					this.threadHandle.Dispose();
					this.threadHandle = null;
				}
				if (this.isImpersonating)
				{
					Win32.RevertToSelf();
				}
				this.disposed = true;
			}

			// Token: 0x060054E9 RID: 21737 RVA: 0x00134310 File Offset: 0x00133310
			public void IncrementReferenceCount()
			{
				this.referenceCount++;
			}

			// Token: 0x060054EA RID: 21738 RVA: 0x00134320 File Offset: 0x00133320
			public int DecrementReferenceCount()
			{
				int num = --this.referenceCount;
				if (num == 0)
				{
					this.Dispose();
				}
				return num;
			}

			// Token: 0x17000EA3 RID: 3747
			// (get) Token: 0x060054EB RID: 21739 RVA: 0x00134349 File Offset: 0x00133349
			public int ReferenceCountValue
			{
				get
				{
					return this.referenceCount;
				}
			}

			// Token: 0x17000EA4 RID: 3748
			// (get) Token: 0x060054EC RID: 21740 RVA: 0x00134351 File Offset: 0x00133351
			public SafeTokenHandle ThreadHandle
			{
				get
				{
					return this.threadHandle;
				}
			}

			// Token: 0x17000EA5 RID: 3749
			// (get) Token: 0x060054ED RID: 21741 RVA: 0x00134359 File Offset: 0x00133359
			public bool IsImpersonating
			{
				get
				{
					return this.isImpersonating;
				}
			}

			// Token: 0x04002C1F RID: 11295
			private bool disposed;

			// Token: 0x04002C20 RID: 11296
			private int referenceCount = 1;

			// Token: 0x04002C21 RID: 11297
			private SafeTokenHandle threadHandle = new SafeTokenHandle(IntPtr.Zero);

			// Token: 0x04002C22 RID: 11298
			private bool isImpersonating;

			// Token: 0x04002C23 RID: 11299
			private static SafeTokenHandle processHandle = new SafeTokenHandle(IntPtr.Zero);

			// Token: 0x04002C24 RID: 11300
			private static readonly object syncRoot = new object();
		}
	}
}
