using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using a;
using a.f;
using MailBee.Mime;
using MailBee.Proxy;
using MailBee.Security;

namespace MailBee.ImapMail
{
	// Token: 0x02000179 RID: 377
	public class Imap : IComponent
	{
		// Token: 0x06000CEC RID: 3308 RVA: 0x0003337C File Offset: 0x0003237C
		public Imap() : this(null)
		{
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00033388 File Offset: 0x00032388
		public Imap(string licenseKey)
		{
			this.v = new global::a.f.o(this);
			this.a = false;
			Imap.a(licenseKey);
			this.c = null;
			this.w = false;
			this.b = null;
			this.d = null;
			this.e = null;
			this.f = null;
			this.g = null;
			this.h = null;
			this.i = null;
			this.j = null;
			this.k = null;
			this.l = null;
			this.m = null;
			this.n = null;
			this.o = null;
			this.p = null;
			this.q = null;
			this.r = null;
			this.s = null;
			this.t = null;
			this.u = null;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00033447 File Offset: 0x00032447
		public Task<bool> DisconnectAsync()
		{
			return this.v.my();
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00033454 File Offset: 0x00032454
		public Task<bool> NoopAsync()
		{
			return this.v.mz();
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00033461 File Offset: 0x00032461
		public Task<bool> ConnectAsync(string serverName)
		{
			return this.v.t(serverName);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0003346F File Offset: 0x0003246F
		public Task<bool> ConnectAsync(string serverName, int port)
		{
			return this.v.a(serverName, port);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0003347E File Offset: 0x0003247E
		public Task<bool> ConnectAsync(string serverName, int port, Socket socketToUse, EndPoint localEndPoint)
		{
			return this.v.a(serverName, port, Global.Pipelining, socketToUse, localEndPoint);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00033495 File Offset: 0x00032495
		public Task<bool> StartTlsAsync()
		{
			return this.v.m0();
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x000334A2 File Offset: 0x000324A2
		public Task<bool> LoginAsync(string targetName, string domain, string accountName, string password, AuthenticationMethods authMethods, AuthenticationOptions authOptions, SaslMethod authUserDefined)
		{
			return this.v.a(targetName, domain, accountName, password, authMethods, authOptions, authUserDefined);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x000334BA File Offset: 0x000324BA
		public Task<bool> LoginAsync(string accountName, string password, AuthenticationMethods authMethods, AuthenticationOptions authOptions, SaslMethod authUserDefined)
		{
			return this.v.a(accountName, password, authMethods, authOptions, authUserDefined);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x000334CE File Offset: 0x000324CE
		public Task<bool> LoginAsync(string accountName, string password, AuthenticationMethods authMethods)
		{
			return this.v.a(accountName, password, authMethods);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x000334DE File Offset: 0x000324DE
		public Task<bool> LoginAsync(string accountName, string password)
		{
			return this.v.g(accountName, password);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000334ED File Offset: 0x000324ED
		public Task<bool> ExecuteCustomCommandAsync(string command, string commandID)
		{
			return this.v.e(command, commandID);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000334FC File Offset: 0x000324FC
		public Task<bool> CreateFolderAsync(string folderName)
		{
			return this.v.p(folderName);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0003350A File Offset: 0x0003250A
		public Task<bool> DeleteFolderAsync(string folderName)
		{
			return this.v.m(folderName);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00033518 File Offset: 0x00032518
		public Task<bool> RenameFolderAsync(string oldName, string newName)
		{
			return this.v.f(oldName, newName);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00033527 File Offset: 0x00032527
		public Task<bool> SubscribeFolderAsync(string folderName)
		{
			return this.v.q(folderName);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00033535 File Offset: 0x00032535
		public Task<bool> UnsubscribeFolderAsync(string folderName)
		{
			return this.v.o(folderName);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00033543 File Offset: 0x00032543
		public Task<bool> ExamineFolderAsync(string folderName)
		{
			return this.v.h(folderName, true);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00033552 File Offset: 0x00032552
		public Task<bool> SelectFolderAsync(string folderName)
		{
			return this.v.h(folderName, false);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00033561 File Offset: 0x00032561
		public Task<bool> CloseAsync(bool expungeDeleted)
		{
			return this.v.g(expungeDeleted);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0003356F File Offset: 0x0003256F
		public Task<bool> CloseAsync()
		{
			return this.v.g(true);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0003357D File Offset: 0x0003257D
		public Task<bool> ExpungeAsync(string uidSet, bool forceUidExpunge)
		{
			return this.v.i(uidSet, forceUidExpunge);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0003358C File Offset: 0x0003258C
		public Task<bool> ExpungeAsync()
		{
			return this.ExpungeAsync(null, false);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00033596 File Offset: 0x00032596
		public Task<FolderStatus> GetFolderStatusAsync(string folderName)
		{
			return this.v.r(folderName);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000335A4 File Offset: 0x000325A4
		public Task<FolderQuota> GetFolderQuotaAsync(string folderName)
		{
			return this.v.n(folderName);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000335B2 File Offset: 0x000325B2
		public Task<FolderQuota> GetAccountQuotaAsync()
		{
			return this.GetFolderQuotaAsync(string.Empty);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000335BF File Offset: 0x000325BF
		public Task<FolderCollection> DownloadFoldersAsync(bool subscribedOnly, string parentFolderName, string pattern)
		{
			return this.v.d(subscribedOnly, parentFolderName, pattern);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x000335CF File Offset: 0x000325CF
		public Task<FolderCollection> DownloadFoldersAsync(bool subscribedOnly)
		{
			return this.DownloadFoldersAsync(subscribedOnly, null, null);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000335DA File Offset: 0x000325DA
		public Task<FolderCollection> DownloadFoldersAsync()
		{
			return this.DownloadFoldersAsync(false);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000335E3 File Offset: 0x000325E3
		public Task<MessageIndexCollection> SearchAsync(bool returnUids, string searchCondition, string charset)
		{
			return this.v.c(returnUids, searchCondition, charset, null);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000335F4 File Offset: 0x000325F4
		public Task<UidCollection> SearchAsync()
		{
			Imap.a a;
			a.c = this;
			a.b = AsyncTaskMethodBuilder<UidCollection>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<UidCollection> asyncTaskMethodBuilder = a.b;
			asyncTaskMethodBuilder.Start<Imap.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00033639 File Offset: 0x00032639
		public Task<MessageIndexCollection> SortedSearchAsync(bool returnUids, string searchCondition, string charset, string orderBy)
		{
			return this.v.c(returnUids, searchCondition, charset, orderBy);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0003364B File Offset: 0x0003264B
		public Task<EnvelopeCollection> DownloadEnvelopesAsync(string messageIndexSet, bool indexIsUid, EnvelopeParts parts, int bodyPreviewSize, string[] extraHeaders, string[] extraItems)
		{
			return this.v.c(messageIndexSet, indexIsUid, parts, bodyPreviewSize, extraHeaders, extraItems);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00033661 File Offset: 0x00032661
		public Task<EnvelopeCollection> DownloadEnvelopesAsync(string messageIndexSet, bool indexIsUid, EnvelopeParts parts, int bodyPreviewSize)
		{
			return this.DownloadEnvelopesAsync(messageIndexSet, indexIsUid, parts, bodyPreviewSize, null, null);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00033670 File Offset: 0x00032670
		public Task<EnvelopeCollection> DownloadEnvelopesAsync(string messageIndexSet, bool indexIsUid)
		{
			return this.DownloadEnvelopesAsync(messageIndexSet, indexIsUid, EnvelopeParts.MailBeeEnvelope, 0);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0003367D File Offset: 0x0003267D
		public Task<MailMessage> DownloadEntireMessageAsync(long messageIndex, bool indexIsUid)
		{
			return this.v.c(messageIndex, indexIsUid, this.SetSeenForEntireMessages ? -1 : -2);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00033699 File Offset: 0x00032699
		public Task<EnvelopeCollection> DownloadEnvelopesExAsync(long[] messageIndices, bool indicesAreUids, EnvelopeParts[] parts, int[] bodyPreviewSizes, string[][] extraHeaders, string[][] extraItems)
		{
			return this.v.c(messageIndices, indicesAreUids, parts, bodyPreviewSizes, extraHeaders, extraItems);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x000336AF File Offset: 0x000326AF
		public Task<MailMessageCollection> DownloadEntireMessagesAsync(string messageIndexSet, bool indexIsUid)
		{
			return this.v.c(messageIndexSet, indexIsUid, this.SetSeenForEntireMessages ? -1 : -2);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x000336CB File Offset: 0x000326CB
		public Task<MailMessageCollection> DownloadMessageHeadersAsync(string messageIndexSet, bool indexIsUid)
		{
			return this.v.c(messageIndexSet, indexIsUid, 0);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x000336DB File Offset: 0x000326DB
		public Task<long> GetFolderSizeAsync()
		{
			return this.v.q();
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000336E8 File Offset: 0x000326E8
		public Task<ImapNamespaceCollectionSet> GetNamespacesAsync()
		{
			return this.v.t();
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x000336F5 File Offset: 0x000326F5
		public Task<bool> UploadMessageAsync(MailMessage msg, string folderName, string flags, string dateTimeString, bool batchMode, UidPlusResult result)
		{
			return this.v.c(msg, folderName, flags, dateTimeString, batchMode, result);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0003370B File Offset: 0x0003270B
		public Task<bool> UploadMessageAsync(MailMessage msg, string folderName, string flags, string dateTimeString)
		{
			return this.UploadMessageAsync(msg, folderName, flags, dateTimeString, true, null);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0003371A File Offset: 0x0003271A
		public Task<bool> UploadMessageAsync(MailMessage msg, string folderName, string flags, DateTime dt)
		{
			return this.UploadMessageAsync(msg, folderName, flags, (dt == DateTime.MinValue) ? null : ImapUtils.GetImapDateTimeString(dt));
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0003373D File Offset: 0x0003273D
		public Task<bool> UploadMessageAsync(MailMessage msg, string folderName, SystemMessageFlags systemFlags)
		{
			return this.UploadMessageAsync(msg, folderName, MessageFlagSet.SystemFlagsToString(systemFlags), DateTime.Now);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00033752 File Offset: 0x00032752
		public Task<bool> UploadMessageAsync(MailMessage msg, string folderName)
		{
			return this.UploadMessageAsync(msg, folderName, null, DateTime.Now);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00033762 File Offset: 0x00032762
		public Task<bool> DeleteMessagesAsync(string messageIndexSet, bool indexIsUid)
		{
			return this.v.g(messageIndexSet, indexIsUid);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00033771 File Offset: 0x00032771
		public Task<bool> SetMessageFlagsAsync(string messageIndexSet, bool indexIsUid, string flags, MessageFlagAction action, bool silentMode)
		{
			return this.v.c(messageIndexSet, indexIsUid, flags, action, silentMode);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00033785 File Offset: 0x00032785
		public Task<bool> SetMessageFlagsAsync(string messageIndexSet, bool indexIsUid, SystemMessageFlags systemFlags, MessageFlagAction action)
		{
			return this.SetMessageFlagsAsync(messageIndexSet, indexIsUid, global::a.f.b.a(systemFlags), action, true);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00033798 File Offset: 0x00032798
		public Task<bool> CopyMessagesAsync(string messageIndexSet, bool indexIsUid, string targetFolderName, UidPlusResult result)
		{
			return this.v.f(messageIndexSet, indexIsUid, targetFolderName, result);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000337AA File Offset: 0x000327AA
		public Task<bool> CopyMessagesAsync(string messageIndexSet, bool indexIsUid, string targetFolderName)
		{
			return this.CopyMessagesAsync(messageIndexSet, indexIsUid, targetFolderName, null);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000337B6 File Offset: 0x000327B6
		public Task<bool> MoveMessagesAsync(string messageIndexSet, bool indexIsUid, string targetFolderName, UidPlusResult result)
		{
			return this.v.e(messageIndexSet, indexIsUid, targetFolderName, result);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000337C8 File Offset: 0x000327C8
		public Task<bool> MoveMessagesAsync(string messageIndexSet, bool indexIsUid, string targetFolderName)
		{
			return this.MoveMessagesAsync(messageIndexSet, indexIsUid, targetFolderName, null);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000337D4 File Offset: 0x000327D4
		public Task<bool> IdleAsync()
		{
			return this.v.aj();
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x000337E1 File Offset: 0x000327E1
		// (set) Token: 0x06000D24 RID: 3364 RVA: 0x000337ED File Offset: 0x000327ED
		[Obsolete("This property is obsolete. Use MailBee.Global.LicenseKey instead.")]
		public static string LicenseKey
		{
			get
			{
				return Resources.Instance.LicenseKeyIsWriteOnlyWarning;
			}
			set
			{
				Global.u = bn.a(value, typeof(Imap));
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00033804 File Offset: 0x00032804
		public int TrialDaysLeft
		{
			get
			{
				return Global.u.b();
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00033810 File Offset: 0x00032810
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x00033822 File Offset: 0x00032822
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				return this.v.bp().d();
			}
			set
			{
				this.v.bp().a(value);
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00033835 File Offset: 0x00032835
		public string Version
		{
			get
			{
				return Global.Version;
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0003383C File Offset: 0x0003283C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00033845 File Offset: 0x00032845
		protected virtual void Dispose(bool disposing)
		{
			if (!this.w)
			{
				if (disposing)
				{
					this.v.bo();
					if (this.b != null)
					{
						this.b(this, EventArgs.Empty);
					}
				}
				this.w = true;
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000D2B RID: 3371 RVA: 0x00033880 File Offset: 0x00032880
		// (remove) Token: 0x06000D2C RID: 3372 RVA: 0x000338B8 File Offset: 0x000328B8
		public event EventHandler Disposed
		{
			[CompilerGenerated]
			add
			{
				EventHandler eventHandler = this.b;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.b, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler eventHandler = this.b;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.b, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x000338ED File Offset: 0x000328ED
		// (set) Token: 0x06000D2E RID: 3374 RVA: 0x000338F5 File Offset: 0x000328F5
		public virtual ISite Site
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x000338FE File Offset: 0x000328FE
		internal bool o()
		{
			return this.d != null;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00033909 File Offset: 0x00032909
		protected internal void OnErrorOccurred(ErrorEventArgs args)
		{
			this.v.bp().a(this.d, this, args);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00033923 File Offset: 0x00032923
		internal bool r()
		{
			return this.e != null;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0003392E File Offset: 0x0003292E
		protected internal void OnLogNewEntry(LogNewEntryEventArgs args)
		{
			this.v.bp().a(this.e, this, args);
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x00033948 File Offset: 0x00032948
		public bool IsBusy
		{
			get
			{
				return this.v.bc();
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00033955 File Offset: 0x00032955
		public void Abort()
		{
			this.v.bd();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00033962 File Offset: 0x00032962
		[Obsolete("This method is obsolete in .NET 4.5+.")]
		public void Wait()
		{
			this.v.bg();
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0003396F File Offset: 0x0003296F
		[Obsolete("This method is obsolete in .NET 4.5+.")]
		public bool Wait(int timeoutInterval)
		{
			return this.v.g(timeoutInterval);
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0003397D File Offset: 0x0003297D
		public bool IsAborted
		{
			get
			{
				return this.v.bf();
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0003398A File Offset: 0x0003298A
		public string GetErrorDescription()
		{
			return this.v.l1();
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x00033997 File Offset: 0x00032997
		public int LastResult
		{
			get
			{
				return this.v.l2();
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x000339A4 File Offset: 0x000329A4
		public Logger Log
		{
			get
			{
				return this.v.bi();
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x000339B1 File Offset: 0x000329B1
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x000339BE File Offset: 0x000329BE
		public bool RaiseEvents
		{
			get
			{
				return this.v.bq();
			}
			set
			{
				this.v.k(value);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x000339CC File Offset: 0x000329CC
		// (set) Token: 0x06000D3E RID: 3390 RVA: 0x000339D9 File Offset: 0x000329D9
		public bool RaiseEventsViaMessageLoop
		{
			get
			{
				return this.v.bb();
			}
			set
			{
				this.v.j(value);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x000339E7 File Offset: 0x000329E7
		// (set) Token: 0x06000D40 RID: 3392 RVA: 0x000339F4 File Offset: 0x000329F4
		public Encoding RequestEncoding
		{
			get
			{
				return this.v.bk();
			}
			set
			{
				this.v.lt(value);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00033A02 File Offset: 0x00032A02
		// (set) Token: 0x06000D42 RID: 3394 RVA: 0x00033A0F File Offset: 0x00032A0F
		public Encoding ResponseEncoding
		{
			get
			{
				return this.v.bm();
			}
			set
			{
				this.v.lu(value);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00033A1D File Offset: 0x00032A1D
		// (set) Token: 0x06000D44 RID: 3396 RVA: 0x00033A2A File Offset: 0x00032A2A
		public bool ThrowExceptions
		{
			get
			{
				return this.v.be();
			}
			set
			{
				this.v.ls(value);
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00033A38 File Offset: 0x00032A38
		internal bool a()
		{
			return this.f != null;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00033A43 File Offset: 0x00032A43
		protected internal void OnDataReceived(DataTransferEventArgs args)
		{
			this.v.bp().a(this.f, this, args);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00033A5D File Offset: 0x00032A5D
		internal bool f()
		{
			return this.g != null;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00033A68 File Offset: 0x00032A68
		protected internal void OnDataSent(DataTransferEventArgs args)
		{
			this.v.bp().a(this.g, this, args);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00033A82 File Offset: 0x00032A82
		public Socket GetSocket()
		{
			return this.v.lv();
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00033A8F File Offset: 0x00032A8F
		public Stream GetStream()
		{
			return this.v.ba();
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00033A9C File Offset: 0x00032A9C
		public int GetSocketError()
		{
			return this.v.lw();
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00033AA9 File Offset: 0x00032AA9
		internal bool h()
		{
			return this.h != null;
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00033AB4 File Offset: 0x00032AB4
		protected internal void OnLowLevelDataReceived(DataTransferEventArgs args)
		{
			this.v.bp().a(this.h, this, args);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00033ACE File Offset: 0x00032ACE
		internal bool c()
		{
			return this.i != null;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00033AD9 File Offset: 0x00032AD9
		protected internal void OnLowLevelDataSent(DataTransferEventArgs args)
		{
			this.v.bp().a(this.i, this, args);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00033AF3 File Offset: 0x00032AF3
		internal bool e()
		{
			return this.j != null;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00033AFE File Offset: 0x00032AFE
		protected internal void OnHostResolved(HostResolvedEventArgs args)
		{
			this.v.bp().a(this.j, this, args);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00033B18 File Offset: 0x00032B18
		internal bool m()
		{
			return this.k != null;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00033B23 File Offset: 0x00032B23
		protected internal void OnSocketCreating(SocketCreatingEventArgs args)
		{
			this.v.bp().a(this.k, this, args);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00033B3D File Offset: 0x00032B3D
		internal bool j()
		{
			return this.l != null;
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00033B48 File Offset: 0x00032B48
		protected internal void OnSocketConnected(SocketConnectedEventArgs args)
		{
			this.v.bp().a(this.l, this, args);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00033B62 File Offset: 0x00032B62
		internal bool d()
		{
			return this.m != null;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00033B6D File Offset: 0x00032B6D
		protected internal void OnConnected(ConnectedEventArgs args)
		{
			this.v.bp().a(this.m, this, args);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00033B87 File Offset: 0x00032B87
		internal bool k()
		{
			return this.n != null;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00033B92 File Offset: 0x00032B92
		protected internal void OnDisconnected(DisconnectedEventArgs args)
		{
			this.v.bp().a(this.n, this, args);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00033BAC File Offset: 0x00032BAC
		internal bool n()
		{
			return this.o != null;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00033BB7 File Offset: 0x00032BB7
		protected internal void OnTlsStarted(TlsStartedEventArgs args)
		{
			this.v.bp().a(this.o, this, args);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00033BD1 File Offset: 0x00032BD1
		internal bool g()
		{
			return this.p != null;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00033BDC File Offset: 0x00032BDC
		protected internal void OnLoggedIn(LoggedInEventArgs args)
		{
			this.v.bp().a(this.p, this, args);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00033BF6 File Offset: 0x00032BF6
		public bool Disconnect()
		{
			return this.v.lo(true);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00033C04 File Offset: 0x00032C04
		[Obsolete("This method is obsolete in .NET 4.5+. Use DisconnectAsync instead.")]
		public IAsyncResult BeginDisconnect(AsyncCallback callback, object state)
		{
			return this.v.lp(callback, state);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00033C13 File Offset: 0x00032C13
		public bool EndDisconnect()
		{
			return this.v.az();
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00033C20 File Offset: 0x00032C20
		public bool Noop()
		{
			return this.v.lq(true);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00033C2E File Offset: 0x00032C2E
		public void ResetState()
		{
			this.v.cb();
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00033C3B File Offset: 0x00032C3B
		public StringDictionary GetExtensions()
		{
			return this.v.ke();
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00033C48 File Offset: 0x00032C48
		public string GetExtension(string name)
		{
			return this.v.kf(name);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00033C56 File Offset: 0x00032C56
		public string GetServerResponse()
		{
			return this.v.l0();
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00033C63 File Offset: 0x00032C63
		public string[] GetServerResponses(string responseName)
		{
			return this.v.s(responseName);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00033C71 File Offset: 0x00032C71
		public AuthenticationMethods GetSupportedAuthMethods()
		{
			return this.v.kh();
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00033C7E File Offset: 0x00032C7E
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x00033C8B File Offset: 0x00032C8B
		public SslStartupMode SslMode
		{
			get
			{
				return this.v.aq();
			}
			set
			{
				this.v.a(value);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00033C99 File Offset: 0x00032C99
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x00033CA6 File Offset: 0x00032CA6
		public SecurityProtocol SslProtocol
		{
			get
			{
				return this.v.@as();
			}
			set
			{
				this.v.a(value);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x00033CB4 File Offset: 0x00032CB4
		public ClientServerCertificates SslCertificates
		{
			get
			{
				return this.v.at();
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x00033CC1 File Offset: 0x00032CC1
		public ProxyServer Proxy
		{
			get
			{
				return this.v.ap();
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00033CCE File Offset: 0x00032CCE
		public bool IsConnected
		{
			get
			{
				return this.v.lx();
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x00033CDB File Offset: 0x00032CDB
		public bool IsSslConnection
		{
			get
			{
				return this.v.ly();
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x00033CE8 File Offset: 0x00032CE8
		public bool IsLoggedIn
		{
			get
			{
				return this.v.lz();
			}
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00033CF5 File Offset: 0x00032CF5
		public bool Connect(string serverName, int port, Socket socketToUse, EndPoint localEndPoint)
		{
			return this.v.a(true, serverName, port, Global.Pipelining, socketToUse, localEndPoint);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00033D0D File Offset: 0x00032D0D
		public bool Connect(string serverName, int port)
		{
			return this.v.a(true, serverName, port);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00033D1D File Offset: 0x00032D1D
		public bool Connect(string serverName)
		{
			return this.v.g(true, serverName);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00033D2C File Offset: 0x00032D2C
		[Obsolete("This method is obsolete in .NET 4.5+. Use ConnectAsync instead.")]
		public IAsyncResult BeginConnect(string serverName, int port, Socket socketToUse, EndPoint localEndPoint, AsyncCallback callback, object state)
		{
			return this.v.a(serverName, port, true, socketToUse, localEndPoint, callback, state);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00033D43 File Offset: 0x00032D43
		public bool EndConnect()
		{
			return this.v.ar();
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00033D50 File Offset: 0x00032D50
		public bool StartTls()
		{
			return this.v.lr(true);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00033D5E File Offset: 0x00032D5E
		[Obsolete("This method is obsolete in .NET 4.5+. Use StartTlsAsync instead.")]
		public IAsyncResult BeginStartTls(AsyncCallback callback, object state)
		{
			return this.v.e(callback, state);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00033D6D File Offset: 0x00032D6D
		public bool EndStartTls()
		{
			return this.v.a8();
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00033D7C File Offset: 0x00032D7C
		public bool Login(string targetName, string domain, string accountName, string password, AuthenticationMethods authMethods, AuthenticationOptions authOptions, SaslMethod authUserDefined)
		{
			return this.v.a(true, targetName, domain, accountName, password, authMethods, authOptions, authUserDefined);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00033DA0 File Offset: 0x00032DA0
		public bool Login(string accountName, string password, AuthenticationMethods authMethods, AuthenticationOptions authOptions, SaslMethod authUserDefined)
		{
			return this.v.a(true, accountName, password, authMethods, authOptions, authUserDefined);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00033DB5 File Offset: 0x00032DB5
		public bool Login(string accountName, string password, AuthenticationMethods authMethods)
		{
			return this.v.a(true, accountName, password, authMethods);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00033DC6 File Offset: 0x00032DC6
		public bool Login(string accountName, string password)
		{
			return this.v.f(true, accountName, password);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00033DD8 File Offset: 0x00032DD8
		[Obsolete("This method is obsolete in .NET 4.5+. Use LoginAsync instead.")]
		public IAsyncResult BeginLogin(string targetName, string domain, string accountName, string password, AuthenticationMethods authMethods, AuthenticationOptions authOptions, SaslMethod authUserDefined, AsyncCallback callback, object state)
		{
			return this.v.a(targetName, domain, accountName, password, authMethods, authOptions, authUserDefined, callback, state);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00033DFF File Offset: 0x00032DFF
		public bool EndLogin()
		{
			return this.v.au();
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x00033E0C File Offset: 0x00032E0C
		// (set) Token: 0x06000D80 RID: 3456 RVA: 0x00033E19 File Offset: 0x00032E19
		public int Timeout
		{
			get
			{
				return this.v.ao();
			}
			set
			{
				this.v.f(value);
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00033E27 File Offset: 0x00032E27
		internal bool q()
		{
			return this.q != null;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00033E32 File Offset: 0x00032E32
		protected internal void OnEnvelopeDownloaded(ImapEnvelopeDownloadedEventArgs args)
		{
			this.v.bp().a(this.q, this, args);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00033E4C File Offset: 0x00032E4C
		internal bool p()
		{
			return this.r != null;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00033E57 File Offset: 0x00032E57
		protected internal void OnEnvelopeDataChunkReceived(ImapEnvelopeDataChunkReceivedEventArgs args)
		{
			this.v.bp().a(this.r, this, args);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00033E71 File Offset: 0x00032E71
		internal bool b()
		{
			return this.s != null;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00033E7C File Offset: 0x00032E7C
		protected internal void OnServerStatus(ImapServerStatusEventArgs args)
		{
			this.v.bp().a(this.s, this, args);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00033E96 File Offset: 0x00032E96
		internal bool l()
		{
			return this.t != null;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00033EA1 File Offset: 0x00032EA1
		protected internal void OnMessageStatus(ImapMessageStatusEventArgs args)
		{
			this.v.bp().a(this.t, this, args);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00033EBB File Offset: 0x00032EBB
		internal bool i()
		{
			return this.u != null;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00033EC6 File Offset: 0x00032EC6
		protected internal void OnIdling(ImapIdlingEventArgs args)
		{
			this.v.bp().a(this.u, this, args);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00033EE0 File Offset: 0x00032EE0
		public bool ExecuteCustomCommand(string command, string commandID)
		{
			return this.v.e(true, command, commandID);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00033EF0 File Offset: 0x00032EF0
		[Obsolete("This method is obsolete in .NET 4.5+. Use ExecuteCustomCommanAsync instead.")]
		public IAsyncResult BeginExecuteCustomCommand(string command, string commandID, AsyncCallback callback, object state)
		{
			return this.v.a(command, commandID, callback, state);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00033F02 File Offset: 0x00032F02
		public bool EndExecuteCustomCommand()
		{
			return this.v.p();
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00033F0F File Offset: 0x00032F0F
		public bool CreateFolder(string folderName)
		{
			return this.v.a(true, folderName);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00033F1E File Offset: 0x00032F1E
		public bool DeleteFolder(string folderName)
		{
			return this.v.f(true, folderName);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00033F2D File Offset: 0x00032F2D
		public bool RenameFolder(string oldName, string newName)
		{
			return this.v.c(true, oldName, newName);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00033F3D File Offset: 0x00032F3D
		public bool SubscribeFolder(string folderName)
		{
			return this.v.b(true, folderName);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00033F4C File Offset: 0x00032F4C
		public bool UnsubscribeFolder(string folderName)
		{
			return this.v.d(true, folderName);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00033F5B File Offset: 0x00032F5B
		public bool ExamineFolder(string folderName)
		{
			return this.v.a(true, folderName, true);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00033F6B File Offset: 0x00032F6B
		public bool SelectFolder(string folderName)
		{
			return this.v.a(true, folderName, false);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00033F7B File Offset: 0x00032F7B
		[Obsolete("This method is obsolete in .NET 4.5+. Use SelectFolderAsync instead.")]
		public IAsyncResult BeginSelectFolder(string folderName, bool readOnly, AsyncCallback callback, object state)
		{
			return this.v.a(folderName, readOnly, callback, state);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00033F8D File Offset: 0x00032F8D
		public bool EndSelectFolder()
		{
			return this.v.v();
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00033F9A File Offset: 0x00032F9A
		public bool Close(bool expungeDeleted)
		{
			return this.v.a(true, expungeDeleted);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00033FA9 File Offset: 0x00032FA9
		public bool Close()
		{
			return this.v.a(true, true);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00033FB8 File Offset: 0x00032FB8
		[Obsolete("This method is obsolete in .NET 4.5+. Use CloseAsync instead.")]
		public IAsyncResult BeginClose(bool expungeDeleted, AsyncCallback callback, object state)
		{
			return this.v.a(expungeDeleted, callback, state);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00033FC8 File Offset: 0x00032FC8
		public bool EndClose()
		{
			return this.v.ak();
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00033FD5 File Offset: 0x00032FD5
		public bool Expunge(string uidSet, bool forceUidExpunge)
		{
			return this.v.b(true, uidSet, forceUidExpunge);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00033FE5 File Offset: 0x00032FE5
		public bool Expunge()
		{
			return this.Expunge(null, false);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00033FEF File Offset: 0x00032FEF
		public FolderStatus GetFolderStatus(string folderName)
		{
			return this.v.e(true, folderName);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00033FFE File Offset: 0x00032FFE
		public FolderQuota GetFolderQuota(string folderName)
		{
			return this.v.c(true, folderName);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0003400D File Offset: 0x0003300D
		public FolderQuota GetAccountQuota()
		{
			return this.GetFolderQuota(string.Empty);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003401A File Offset: 0x0003301A
		public FolderCollection DownloadFolders(bool subscribedOnly, string parentFolderName, string pattern)
		{
			return this.v.a(true, subscribedOnly, parentFolderName, pattern);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003402B File Offset: 0x0003302B
		public FolderCollection DownloadFolders(bool subscribedOnly)
		{
			return this.DownloadFolders(subscribedOnly, null, null);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00034036 File Offset: 0x00033036
		public FolderCollection DownloadFolders()
		{
			return this.DownloadFolders(false);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003403F File Offset: 0x0003303F
		[Obsolete("This method is obsolete in .NET 4.5+. Use DownloadFoldersAsync instead.")]
		public IAsyncResult BeginDownloadFolders(bool subscribedOnly, string parentFolderName, string pattern, AsyncCallback callback, object state)
		{
			return this.v.a(subscribedOnly, parentFolderName, pattern, callback, state);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00034053 File Offset: 0x00033053
		public FolderCollection EndDownloadFolders()
		{
			return this.v.ae();
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00034060 File Offset: 0x00033060
		public Folder GetSpecialFolder(FolderCollection folders, FolderFlags flag)
		{
			if (folders == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			foreach (object obj in folders)
			{
				Folder folder = (Folder)obj;
				if ((folder.Flags & flag) > FolderFlags.None)
				{
					return folder;
				}
			}
			return null;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000340CC File Offset: 0x000330CC
		public MessageIndexCollection Search(bool returnUids, string searchCondition, string charset)
		{
			return this.v.a(true, returnUids, searchCondition, charset, null);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x000340DE File Offset: 0x000330DE
		public UidCollection Search()
		{
			return (UidCollection)this.v.a(true, true, null, null, null);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x000340F5 File Offset: 0x000330F5
		[Obsolete("This method is obsolete in .NET 4.5+. Use SearchAsync instead.")]
		public IAsyncResult BeginSearch(bool returnUids, string searchCondition, string charset, AsyncCallback callback, object state)
		{
			return this.v.a(returnUids, searchCondition, charset, null, callback, state);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003410A File Offset: 0x0003310A
		public MessageIndexCollection EndSearch()
		{
			return this.v.ai();
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00034117 File Offset: 0x00033117
		public MessageIndexCollection SortedSearch(bool returnUids, string searchCondition, string charset, string orderBy)
		{
			return this.v.a(true, returnUids, searchCondition, charset, orderBy);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0003412A File Offset: 0x0003312A
		[Obsolete("This method is obsolete in .NET 4.5+. Use SortedSearchAsync instead.")]
		public IAsyncResult BeginSortedSearch(bool returnUids, string searchCondition, string charset, string orderBy, AsyncCallback callback, object state)
		{
			return this.v.a(returnUids, searchCondition, charset, orderBy, callback, state);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00034140 File Offset: 0x00033140
		public MessageIndexCollection EndSortedSearch()
		{
			return this.v.ai();
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0003414D File Offset: 0x0003314D
		public EnvelopeCollection DownloadEnvelopes(string messageIndexSet, bool indexIsUid, EnvelopeParts parts, int bodyPreviewSize, string[] extraHeaders, string[] extraItems)
		{
			return this.v.a(true, messageIndexSet, indexIsUid, parts, bodyPreviewSize, extraHeaders, extraItems);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00034164 File Offset: 0x00033164
		public EnvelopeCollection DownloadEnvelopesEx(long[] messageIndices, bool indicesAreUids, EnvelopeParts[] parts, int[] bodyPreviewSizes, string[][] extraHeaders, string[][] extraItems)
		{
			return this.v.a(true, messageIndices, indicesAreUids, parts, bodyPreviewSizes, extraHeaders, extraItems);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003417B File Offset: 0x0003317B
		public EnvelopeCollection DownloadEnvelopes(string messageIndexSet, bool indexIsUid, EnvelopeParts parts, int bodyPreviewSize)
		{
			return this.DownloadEnvelopes(messageIndexSet, indexIsUid, parts, bodyPreviewSize, null, null);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0003418A File Offset: 0x0003318A
		public EnvelopeCollection DownloadEnvelopes(string messageIndexSet, bool indexIsUid)
		{
			return this.DownloadEnvelopes(messageIndexSet, indexIsUid, EnvelopeParts.MailBeeEnvelope, 0);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00034198 File Offset: 0x00033198
		[Obsolete("This method is obsolete in .NET 4.5+. Use DownloadEnvelopesAsync instead.")]
		public IAsyncResult BeginDownloadEnvelopes(string messageIndexSet, bool indexIsUid, EnvelopeParts parts, int bodyPreviewSize, string[] extraHeaders, string[] extraItems, AsyncCallback callback, object state)
		{
			return this.v.a(messageIndexSet, indexIsUid, parts, bodyPreviewSize, extraHeaders, extraItems, callback, state);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000341BD File Offset: 0x000331BD
		public EnvelopeCollection EndDownloadEnvelopes()
		{
			return this.v.s();
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000341CC File Offset: 0x000331CC
		[Obsolete("This method is obsolete in .NET 4.5+. Use DownloadEnvelopesExAsync instead.")]
		public IAsyncResult BeginDownloadEnvelopesEx(long[] messageIndices, bool indicesAreUids, EnvelopeParts[] parts, int[] bodyPreviewSizes, string[][] extraHeaders, string[][] extraItems, AsyncCallback callback, object state)
		{
			return this.v.a(messageIndices, indicesAreUids, parts, bodyPreviewSizes, extraHeaders, extraItems, callback, state);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000341F1 File Offset: 0x000331F1
		public EnvelopeCollection EndDownloadEnvelopesEx()
		{
			return this.v.u();
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x000341FE File Offset: 0x000331FE
		public MailMessage DownloadEntireMessage(long messageIndex, bool indexIsUid)
		{
			return this.v.a(true, messageIndex, indexIsUid, this.SetSeenForEntireMessages ? -1 : -2);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0003421B File Offset: 0x0003321B
		public MailMessageCollection DownloadEntireMessages(string messageIndexSet, bool indexIsUid)
		{
			return this.v.a(true, messageIndexSet, indexIsUid, this.SetSeenForEntireMessages ? -1 : -2);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00034238 File Offset: 0x00033238
		public MailMessageCollection DownloadMessageHeaders(string messageIndexSet, bool indexIsUid)
		{
			return this.v.a(true, messageIndexSet, indexIsUid, 0);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00034249 File Offset: 0x00033249
		public long GetFolderSize()
		{
			return this.v.e(true);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00034257 File Offset: 0x00033257
		public ImapNamespaceCollectionSet GetNamespaces()
		{
			return this.v.i(true);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00034265 File Offset: 0x00033265
		public bool UploadMessage(MailMessage msg, string folderName, string flags, string dateTimeString, bool batchMode, UidPlusResult result)
		{
			return this.v.a(true, msg, folderName, flags, dateTimeString, batchMode, result);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0003427C File Offset: 0x0003327C
		public bool UploadMessage(MailMessage msg, string folderName, string flags, string dateTimeString)
		{
			return this.UploadMessage(msg, folderName, flags, dateTimeString, true, null);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0003428B File Offset: 0x0003328B
		public bool UploadMessage(MailMessage msg, string folderName, string flags, DateTime dt)
		{
			return this.UploadMessage(msg, folderName, flags, (dt == DateTime.MinValue) ? null : ImapUtils.GetImapDateTimeString(dt));
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000342AE File Offset: 0x000332AE
		public bool UploadMessage(MailMessage msg, string folderName, SystemMessageFlags systemFlags)
		{
			return this.UploadMessage(msg, folderName, MessageFlagSet.SystemFlagsToString(systemFlags), DateTime.Now);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000342C3 File Offset: 0x000332C3
		public bool UploadMessage(MailMessage msg, string folderName)
		{
			return this.UploadMessage(msg, folderName, null, DateTime.Now);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000342D4 File Offset: 0x000332D4
		[Obsolete("This method is obsolete in .NET 4.5+. Use UploadMessageAsync instead.")]
		public IAsyncResult BeginUploadMessage(MailMessage msg, string folderName, string flags, string dateTimeString, bool batchMode, UidPlusResult result, AsyncCallback callback, object state)
		{
			return this.v.a(msg, folderName, flags, dateTimeString, batchMode, result, callback, state);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000342F9 File Offset: 0x000332F9
		public bool EndUploadMessage()
		{
			return this.v.y();
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00034306 File Offset: 0x00033306
		public bool DeleteMessages(string messageIndexSet, bool indexIsUid)
		{
			return this.v.c(true, messageIndexSet, indexIsUid);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00034316 File Offset: 0x00033316
		public bool SetMessageFlags(string messageIndexSet, bool indexIsUid, string flags, MessageFlagAction action, bool silentMode)
		{
			return this.v.a(true, messageIndexSet, indexIsUid, flags, action, silentMode);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003432B File Offset: 0x0003332B
		public bool SetMessageFlags(string messageIndexSet, bool indexIsUid, SystemMessageFlags systemFlags, MessageFlagAction action)
		{
			return this.SetMessageFlags(messageIndexSet, indexIsUid, global::a.f.b.a(systemFlags), action, true);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0003433E File Offset: 0x0003333E
		[Obsolete("This method is obsolete in .NET 4.5+. Use SetMessageFlagsAsync instead.")]
		public IAsyncResult BeginSetMessageFlags(string messageIndexSet, bool indexIsUid, string flags, MessageFlagAction action, bool silentMode, AsyncCallback callback, object state)
		{
			return this.v.a(messageIndexSet, indexIsUid, flags, action, silentMode, callback, state);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00034356 File Offset: 0x00033356
		public bool EndSetMessageFlags()
		{
			return this.v.ah();
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00034363 File Offset: 0x00033363
		public bool CopyMessages(string messageIndexSet, bool indexIsUid, string targetFolderName, UidPlusResult result)
		{
			return this.v.b(true, messageIndexSet, indexIsUid, targetFolderName, result);
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00034376 File Offset: 0x00033376
		public bool CopyMessages(string messageIndexSet, bool indexIsUid, string targetFolderName)
		{
			return this.CopyMessages(messageIndexSet, indexIsUid, targetFolderName, null);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00034382 File Offset: 0x00033382
		public bool MoveMessages(string messageIndexSet, bool indexIsUid, string targetFolderName, UidPlusResult result)
		{
			return this.v.a(true, messageIndexSet, indexIsUid, targetFolderName, result);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00034395 File Offset: 0x00033395
		public bool MoveMessages(string messageIndexSet, bool indexIsUid, string targetFolderName)
		{
			return this.MoveMessages(messageIndexSet, indexIsUid, targetFolderName, null);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x000343A1 File Offset: 0x000333A1
		[Obsolete("This method is obsolete in .NET 4.5+. Use CopyMessagesAsync or MoveMessagesAsync instead.")]
		public IAsyncResult BeginCopyOrMoveMessages(string messageIndexSet, bool indexIsUid, string targetFolderName, UidPlusResult result, bool move, AsyncCallback callback, object state)
		{
			return this.v.a(messageIndexSet, indexIsUid, targetFolderName, result, move, callback, state);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x000343B9 File Offset: 0x000333B9
		public bool EndCopyOrMoveMessages()
		{
			return this.v.z();
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x000343C6 File Offset: 0x000333C6
		public bool Idle()
		{
			return this.v.f(true);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000343D4 File Offset: 0x000333D4
		[Obsolete("This method is obsolete in .NET 4.5+. Use IdleAsync instead.")]
		public IAsyncResult BeginIdle(AsyncCallback callback, object state)
		{
			return this.v.a(callback, state);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000343E3 File Offset: 0x000333E3
		public bool EndIdle()
		{
			return this.v.am();
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x000343F0 File Offset: 0x000333F0
		public void StopIdle()
		{
			this.v.ad();
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x000343FD File Offset: 0x000333FD
		public bool IsIdle
		{
			get
			{
				return this.v.aa();
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0003440A File Offset: 0x0003340A
		public bool IsFolderSelected
		{
			get
			{
				return this.v.al();
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00034417 File Offset: 0x00033417
		public int MessageCount
		{
			get
			{
				return this.v.o();
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x00034424 File Offset: 0x00033424
		public int RecentCount
		{
			get
			{
				return this.v.w();
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00034431 File Offset: 0x00033431
		public int Unseen
		{
			get
			{
				return this.v.ac();
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0003443E File Offset: 0x0003343E
		public long UidValidity
		{
			get
			{
				return this.v.ag();
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0003444B File Offset: 0x0003344B
		public long UidNext
		{
			get
			{
				return this.v.m();
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00034458 File Offset: 0x00033458
		public MessageFlagSet Flags
		{
			get
			{
				return this.v.x();
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00034465 File Offset: 0x00033465
		public MessageFlagSet PermanentFlags
		{
			get
			{
				return this.v.af();
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00034472 File Offset: 0x00033472
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x0003447F File Offset: 0x0003347F
		public bool Utf7EncodeFolderNames
		{
			get
			{
				return this.v.an();
			}
			set
			{
				this.v.h(value);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0003448D File Offset: 0x0003348D
		// (set) Token: 0x06000DDC RID: 3548 RVA: 0x0003449A File Offset: 0x0003349A
		public bool UseXList
		{
			get
			{
				return this.v.n();
			}
			set
			{
				this.v.c(value);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x000344A8 File Offset: 0x000334A8
		// (set) Token: 0x06000DDE RID: 3550 RVA: 0x000344B0 File Offset: 0x000334B0
		public bool SetSeenForEntireMessages
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x000344B9 File Offset: 0x000334B9
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x000344C6 File Offset: 0x000334C6
		public bool EnableLastDownloaded
		{
			get
			{
				return this.v.r();
			}
			set
			{
				this.v.d(value);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x000344D4 File Offset: 0x000334D4
		public EnvelopeCollection LastDownloadedEnvelopes
		{
			get
			{
				return this.v.ab();
			}
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x000344E1 File Offset: 0x000334E1
		private static void a(string A_0)
		{
			Global.a(typeof(Imap), A_0);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x000344F4 File Offset: 0x000334F4
		public static MailMessage QuickDownloadMessage(string serverName, string accountName, string password, string folderName, int messageNumber, int bodyPreviewSize)
		{
			Imap.a(null);
			global::a.f.t t = new global::a.f.t(null, null, new Logger(null), 0);
			t.av().e(serverName);
			t.av().c(accountName);
			t.av().d(password);
			t.fy();
			t.fo();
			t.f(folderName, false);
			MailMessage result = t.b((long)messageNumber, false, bodyPreviewSize, false);
			t.fz(true);
			return result;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00034562 File Offset: 0x00033562
		public static MailMessage QuickDownloadMessage(string serverName, string accountName, string password, string folderName, int messageNumber)
		{
			return Imap.QuickDownloadMessage(serverName, accountName, password, folderName, messageNumber, -1);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00034570 File Offset: 0x00033570
		public static MailMessageCollection QuickDownloadMessages(string serverName, string accountName, string password, string folderName, int bodyPreviewSize)
		{
			Imap.a(null);
			global::a.f.t t = new global::a.f.t(null, null, new Logger(null), 0);
			t.av().e(serverName);
			t.av().c(accountName);
			t.av().d(password);
			t.fy();
			t.fo();
			t.f(folderName, false);
			MailMessageCollection result = t.b("1:*", false, bodyPreviewSize, false);
			t.fz(true);
			return result;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x000345E0 File Offset: 0x000335E0
		public static MailMessageCollection QuickDownloadMessages(string serverName, string accountName, string password, string folderName)
		{
			return Imap.QuickDownloadMessages(serverName, accountName, password, folderName, -1);
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000DE7 RID: 3559 RVA: 0x000345EC File Offset: 0x000335EC
		// (remove) Token: 0x06000DE8 RID: 3560 RVA: 0x00034624 File Offset: 0x00033624
		public event ErrorEventHandler ErrorOccurred
		{
			[CompilerGenerated]
			add
			{
				ErrorEventHandler errorEventHandler = this.d;
				ErrorEventHandler errorEventHandler2;
				do
				{
					errorEventHandler2 = errorEventHandler;
					ErrorEventHandler value2 = (ErrorEventHandler)Delegate.Combine(errorEventHandler2, value);
					errorEventHandler = Interlocked.CompareExchange<ErrorEventHandler>(ref this.d, value2, errorEventHandler2);
				}
				while (errorEventHandler != errorEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ErrorEventHandler errorEventHandler = this.d;
				ErrorEventHandler errorEventHandler2;
				do
				{
					errorEventHandler2 = errorEventHandler;
					ErrorEventHandler value2 = (ErrorEventHandler)Delegate.Remove(errorEventHandler2, value);
					errorEventHandler = Interlocked.CompareExchange<ErrorEventHandler>(ref this.d, value2, errorEventHandler2);
				}
				while (errorEventHandler != errorEventHandler2);
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000DE9 RID: 3561 RVA: 0x0003465C File Offset: 0x0003365C
		// (remove) Token: 0x06000DEA RID: 3562 RVA: 0x00034694 File Offset: 0x00033694
		public event LogNewEntryEventHandler LogNewEntry
		{
			[CompilerGenerated]
			add
			{
				LogNewEntryEventHandler logNewEntryEventHandler = this.e;
				LogNewEntryEventHandler logNewEntryEventHandler2;
				do
				{
					logNewEntryEventHandler2 = logNewEntryEventHandler;
					LogNewEntryEventHandler value2 = (LogNewEntryEventHandler)Delegate.Combine(logNewEntryEventHandler2, value);
					logNewEntryEventHandler = Interlocked.CompareExchange<LogNewEntryEventHandler>(ref this.e, value2, logNewEntryEventHandler2);
				}
				while (logNewEntryEventHandler != logNewEntryEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				LogNewEntryEventHandler logNewEntryEventHandler = this.e;
				LogNewEntryEventHandler logNewEntryEventHandler2;
				do
				{
					logNewEntryEventHandler2 = logNewEntryEventHandler;
					LogNewEntryEventHandler value2 = (LogNewEntryEventHandler)Delegate.Remove(logNewEntryEventHandler2, value);
					logNewEntryEventHandler = Interlocked.CompareExchange<LogNewEntryEventHandler>(ref this.e, value2, logNewEntryEventHandler2);
				}
				while (logNewEntryEventHandler != logNewEntryEventHandler2);
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000DEB RID: 3563 RVA: 0x000346CC File Offset: 0x000336CC
		// (remove) Token: 0x06000DEC RID: 3564 RVA: 0x00034704 File Offset: 0x00033704
		public event DataTransferEventHandler DataReceived
		{
			[CompilerGenerated]
			add
			{
				DataTransferEventHandler dataTransferEventHandler = this.f;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Combine(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.f, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DataTransferEventHandler dataTransferEventHandler = this.f;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Remove(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.f, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000DED RID: 3565 RVA: 0x0003473C File Offset: 0x0003373C
		// (remove) Token: 0x06000DEE RID: 3566 RVA: 0x00034774 File Offset: 0x00033774
		public event DataTransferEventHandler DataSent
		{
			[CompilerGenerated]
			add
			{
				DataTransferEventHandler dataTransferEventHandler = this.g;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Combine(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.g, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DataTransferEventHandler dataTransferEventHandler = this.g;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Remove(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.g, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000DEF RID: 3567 RVA: 0x000347AC File Offset: 0x000337AC
		// (remove) Token: 0x06000DF0 RID: 3568 RVA: 0x000347E4 File Offset: 0x000337E4
		public event DataTransferEventHandler LowLevelDataReceived
		{
			[CompilerGenerated]
			add
			{
				DataTransferEventHandler dataTransferEventHandler = this.h;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Combine(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.h, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DataTransferEventHandler dataTransferEventHandler = this.h;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Remove(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.h, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000DF1 RID: 3569 RVA: 0x0003481C File Offset: 0x0003381C
		// (remove) Token: 0x06000DF2 RID: 3570 RVA: 0x00034854 File Offset: 0x00033854
		public event DataTransferEventHandler LowLevelDataSent
		{
			[CompilerGenerated]
			add
			{
				DataTransferEventHandler dataTransferEventHandler = this.i;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Combine(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.i, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DataTransferEventHandler dataTransferEventHandler = this.i;
				DataTransferEventHandler dataTransferEventHandler2;
				do
				{
					dataTransferEventHandler2 = dataTransferEventHandler;
					DataTransferEventHandler value2 = (DataTransferEventHandler)Delegate.Remove(dataTransferEventHandler2, value);
					dataTransferEventHandler = Interlocked.CompareExchange<DataTransferEventHandler>(ref this.i, value2, dataTransferEventHandler2);
				}
				while (dataTransferEventHandler != dataTransferEventHandler2);
			}
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000DF3 RID: 3571 RVA: 0x0003488C File Offset: 0x0003388C
		// (remove) Token: 0x06000DF4 RID: 3572 RVA: 0x000348C4 File Offset: 0x000338C4
		public event HostResolvedEventHandler HostResolved
		{
			[CompilerGenerated]
			add
			{
				HostResolvedEventHandler hostResolvedEventHandler = this.j;
				HostResolvedEventHandler hostResolvedEventHandler2;
				do
				{
					hostResolvedEventHandler2 = hostResolvedEventHandler;
					HostResolvedEventHandler value2 = (HostResolvedEventHandler)Delegate.Combine(hostResolvedEventHandler2, value);
					hostResolvedEventHandler = Interlocked.CompareExchange<HostResolvedEventHandler>(ref this.j, value2, hostResolvedEventHandler2);
				}
				while (hostResolvedEventHandler != hostResolvedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				HostResolvedEventHandler hostResolvedEventHandler = this.j;
				HostResolvedEventHandler hostResolvedEventHandler2;
				do
				{
					hostResolvedEventHandler2 = hostResolvedEventHandler;
					HostResolvedEventHandler value2 = (HostResolvedEventHandler)Delegate.Remove(hostResolvedEventHandler2, value);
					hostResolvedEventHandler = Interlocked.CompareExchange<HostResolvedEventHandler>(ref this.j, value2, hostResolvedEventHandler2);
				}
				while (hostResolvedEventHandler != hostResolvedEventHandler2);
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000DF5 RID: 3573 RVA: 0x000348FC File Offset: 0x000338FC
		// (remove) Token: 0x06000DF6 RID: 3574 RVA: 0x00034934 File Offset: 0x00033934
		public event SocketCreatingEventHandler SocketCreating
		{
			[CompilerGenerated]
			add
			{
				SocketCreatingEventHandler socketCreatingEventHandler = this.k;
				SocketCreatingEventHandler socketCreatingEventHandler2;
				do
				{
					socketCreatingEventHandler2 = socketCreatingEventHandler;
					SocketCreatingEventHandler value2 = (SocketCreatingEventHandler)Delegate.Combine(socketCreatingEventHandler2, value);
					socketCreatingEventHandler = Interlocked.CompareExchange<SocketCreatingEventHandler>(ref this.k, value2, socketCreatingEventHandler2);
				}
				while (socketCreatingEventHandler != socketCreatingEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SocketCreatingEventHandler socketCreatingEventHandler = this.k;
				SocketCreatingEventHandler socketCreatingEventHandler2;
				do
				{
					socketCreatingEventHandler2 = socketCreatingEventHandler;
					SocketCreatingEventHandler value2 = (SocketCreatingEventHandler)Delegate.Remove(socketCreatingEventHandler2, value);
					socketCreatingEventHandler = Interlocked.CompareExchange<SocketCreatingEventHandler>(ref this.k, value2, socketCreatingEventHandler2);
				}
				while (socketCreatingEventHandler != socketCreatingEventHandler2);
			}
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06000DF7 RID: 3575 RVA: 0x0003496C File Offset: 0x0003396C
		// (remove) Token: 0x06000DF8 RID: 3576 RVA: 0x000349A4 File Offset: 0x000339A4
		public event SocketConnectedEventHandler SocketConnected
		{
			[CompilerGenerated]
			add
			{
				SocketConnectedEventHandler socketConnectedEventHandler = this.l;
				SocketConnectedEventHandler socketConnectedEventHandler2;
				do
				{
					socketConnectedEventHandler2 = socketConnectedEventHandler;
					SocketConnectedEventHandler value2 = (SocketConnectedEventHandler)Delegate.Combine(socketConnectedEventHandler2, value);
					socketConnectedEventHandler = Interlocked.CompareExchange<SocketConnectedEventHandler>(ref this.l, value2, socketConnectedEventHandler2);
				}
				while (socketConnectedEventHandler != socketConnectedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SocketConnectedEventHandler socketConnectedEventHandler = this.l;
				SocketConnectedEventHandler socketConnectedEventHandler2;
				do
				{
					socketConnectedEventHandler2 = socketConnectedEventHandler;
					SocketConnectedEventHandler value2 = (SocketConnectedEventHandler)Delegate.Remove(socketConnectedEventHandler2, value);
					socketConnectedEventHandler = Interlocked.CompareExchange<SocketConnectedEventHandler>(ref this.l, value2, socketConnectedEventHandler2);
				}
				while (socketConnectedEventHandler != socketConnectedEventHandler2);
			}
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000DF9 RID: 3577 RVA: 0x000349DC File Offset: 0x000339DC
		// (remove) Token: 0x06000DFA RID: 3578 RVA: 0x00034A14 File Offset: 0x00033A14
		public event ConnectedEventHandler Connected
		{
			[CompilerGenerated]
			add
			{
				ConnectedEventHandler connectedEventHandler = this.m;
				ConnectedEventHandler connectedEventHandler2;
				do
				{
					connectedEventHandler2 = connectedEventHandler;
					ConnectedEventHandler value2 = (ConnectedEventHandler)Delegate.Combine(connectedEventHandler2, value);
					connectedEventHandler = Interlocked.CompareExchange<ConnectedEventHandler>(ref this.m, value2, connectedEventHandler2);
				}
				while (connectedEventHandler != connectedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ConnectedEventHandler connectedEventHandler = this.m;
				ConnectedEventHandler connectedEventHandler2;
				do
				{
					connectedEventHandler2 = connectedEventHandler;
					ConnectedEventHandler value2 = (ConnectedEventHandler)Delegate.Remove(connectedEventHandler2, value);
					connectedEventHandler = Interlocked.CompareExchange<ConnectedEventHandler>(ref this.m, value2, connectedEventHandler2);
				}
				while (connectedEventHandler != connectedEventHandler2);
			}
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000DFB RID: 3579 RVA: 0x00034A4C File Offset: 0x00033A4C
		// (remove) Token: 0x06000DFC RID: 3580 RVA: 0x00034A84 File Offset: 0x00033A84
		public event DisconnectedEventHandler Disconnected
		{
			[CompilerGenerated]
			add
			{
				DisconnectedEventHandler disconnectedEventHandler = this.n;
				DisconnectedEventHandler disconnectedEventHandler2;
				do
				{
					disconnectedEventHandler2 = disconnectedEventHandler;
					DisconnectedEventHandler value2 = (DisconnectedEventHandler)Delegate.Combine(disconnectedEventHandler2, value);
					disconnectedEventHandler = Interlocked.CompareExchange<DisconnectedEventHandler>(ref this.n, value2, disconnectedEventHandler2);
				}
				while (disconnectedEventHandler != disconnectedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DisconnectedEventHandler disconnectedEventHandler = this.n;
				DisconnectedEventHandler disconnectedEventHandler2;
				do
				{
					disconnectedEventHandler2 = disconnectedEventHandler;
					DisconnectedEventHandler value2 = (DisconnectedEventHandler)Delegate.Remove(disconnectedEventHandler2, value);
					disconnectedEventHandler = Interlocked.CompareExchange<DisconnectedEventHandler>(ref this.n, value2, disconnectedEventHandler2);
				}
				while (disconnectedEventHandler != disconnectedEventHandler2);
			}
		}

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06000DFD RID: 3581 RVA: 0x00034ABC File Offset: 0x00033ABC
		// (remove) Token: 0x06000DFE RID: 3582 RVA: 0x00034AF4 File Offset: 0x00033AF4
		public event TlsStartedEventHandler TlsStarted
		{
			[CompilerGenerated]
			add
			{
				TlsStartedEventHandler tlsStartedEventHandler = this.o;
				TlsStartedEventHandler tlsStartedEventHandler2;
				do
				{
					tlsStartedEventHandler2 = tlsStartedEventHandler;
					TlsStartedEventHandler value2 = (TlsStartedEventHandler)Delegate.Combine(tlsStartedEventHandler2, value);
					tlsStartedEventHandler = Interlocked.CompareExchange<TlsStartedEventHandler>(ref this.o, value2, tlsStartedEventHandler2);
				}
				while (tlsStartedEventHandler != tlsStartedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				TlsStartedEventHandler tlsStartedEventHandler = this.o;
				TlsStartedEventHandler tlsStartedEventHandler2;
				do
				{
					tlsStartedEventHandler2 = tlsStartedEventHandler;
					TlsStartedEventHandler value2 = (TlsStartedEventHandler)Delegate.Remove(tlsStartedEventHandler2, value);
					tlsStartedEventHandler = Interlocked.CompareExchange<TlsStartedEventHandler>(ref this.o, value2, tlsStartedEventHandler2);
				}
				while (tlsStartedEventHandler != tlsStartedEventHandler2);
			}
		}

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06000DFF RID: 3583 RVA: 0x00034B2C File Offset: 0x00033B2C
		// (remove) Token: 0x06000E00 RID: 3584 RVA: 0x00034B64 File Offset: 0x00033B64
		public event LoggedInEventHandler LoggedIn
		{
			[CompilerGenerated]
			add
			{
				LoggedInEventHandler loggedInEventHandler = this.p;
				LoggedInEventHandler loggedInEventHandler2;
				do
				{
					loggedInEventHandler2 = loggedInEventHandler;
					LoggedInEventHandler value2 = (LoggedInEventHandler)Delegate.Combine(loggedInEventHandler2, value);
					loggedInEventHandler = Interlocked.CompareExchange<LoggedInEventHandler>(ref this.p, value2, loggedInEventHandler2);
				}
				while (loggedInEventHandler != loggedInEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				LoggedInEventHandler loggedInEventHandler = this.p;
				LoggedInEventHandler loggedInEventHandler2;
				do
				{
					loggedInEventHandler2 = loggedInEventHandler;
					LoggedInEventHandler value2 = (LoggedInEventHandler)Delegate.Remove(loggedInEventHandler2, value);
					loggedInEventHandler = Interlocked.CompareExchange<LoggedInEventHandler>(ref this.p, value2, loggedInEventHandler2);
				}
				while (loggedInEventHandler != loggedInEventHandler2);
			}
		}

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06000E01 RID: 3585 RVA: 0x00034B9C File Offset: 0x00033B9C
		// (remove) Token: 0x06000E02 RID: 3586 RVA: 0x00034BD4 File Offset: 0x00033BD4
		public event ImapEnvelopeDownloadedEventHandler EnvelopeDownloaded
		{
			[CompilerGenerated]
			add
			{
				ImapEnvelopeDownloadedEventHandler imapEnvelopeDownloadedEventHandler = this.q;
				ImapEnvelopeDownloadedEventHandler imapEnvelopeDownloadedEventHandler2;
				do
				{
					imapEnvelopeDownloadedEventHandler2 = imapEnvelopeDownloadedEventHandler;
					ImapEnvelopeDownloadedEventHandler value2 = (ImapEnvelopeDownloadedEventHandler)Delegate.Combine(imapEnvelopeDownloadedEventHandler2, value);
					imapEnvelopeDownloadedEventHandler = Interlocked.CompareExchange<ImapEnvelopeDownloadedEventHandler>(ref this.q, value2, imapEnvelopeDownloadedEventHandler2);
				}
				while (imapEnvelopeDownloadedEventHandler != imapEnvelopeDownloadedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ImapEnvelopeDownloadedEventHandler imapEnvelopeDownloadedEventHandler = this.q;
				ImapEnvelopeDownloadedEventHandler imapEnvelopeDownloadedEventHandler2;
				do
				{
					imapEnvelopeDownloadedEventHandler2 = imapEnvelopeDownloadedEventHandler;
					ImapEnvelopeDownloadedEventHandler value2 = (ImapEnvelopeDownloadedEventHandler)Delegate.Remove(imapEnvelopeDownloadedEventHandler2, value);
					imapEnvelopeDownloadedEventHandler = Interlocked.CompareExchange<ImapEnvelopeDownloadedEventHandler>(ref this.q, value2, imapEnvelopeDownloadedEventHandler2);
				}
				while (imapEnvelopeDownloadedEventHandler != imapEnvelopeDownloadedEventHandler2);
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000E03 RID: 3587 RVA: 0x00034C0C File Offset: 0x00033C0C
		// (remove) Token: 0x06000E04 RID: 3588 RVA: 0x00034C44 File Offset: 0x00033C44
		public event ImapEnvelopeDataChunkReceivedEventHandler EnvelopeDataChunkReceived
		{
			[CompilerGenerated]
			add
			{
				ImapEnvelopeDataChunkReceivedEventHandler imapEnvelopeDataChunkReceivedEventHandler = this.r;
				ImapEnvelopeDataChunkReceivedEventHandler imapEnvelopeDataChunkReceivedEventHandler2;
				do
				{
					imapEnvelopeDataChunkReceivedEventHandler2 = imapEnvelopeDataChunkReceivedEventHandler;
					ImapEnvelopeDataChunkReceivedEventHandler value2 = (ImapEnvelopeDataChunkReceivedEventHandler)Delegate.Combine(imapEnvelopeDataChunkReceivedEventHandler2, value);
					imapEnvelopeDataChunkReceivedEventHandler = Interlocked.CompareExchange<ImapEnvelopeDataChunkReceivedEventHandler>(ref this.r, value2, imapEnvelopeDataChunkReceivedEventHandler2);
				}
				while (imapEnvelopeDataChunkReceivedEventHandler != imapEnvelopeDataChunkReceivedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ImapEnvelopeDataChunkReceivedEventHandler imapEnvelopeDataChunkReceivedEventHandler = this.r;
				ImapEnvelopeDataChunkReceivedEventHandler imapEnvelopeDataChunkReceivedEventHandler2;
				do
				{
					imapEnvelopeDataChunkReceivedEventHandler2 = imapEnvelopeDataChunkReceivedEventHandler;
					ImapEnvelopeDataChunkReceivedEventHandler value2 = (ImapEnvelopeDataChunkReceivedEventHandler)Delegate.Remove(imapEnvelopeDataChunkReceivedEventHandler2, value);
					imapEnvelopeDataChunkReceivedEventHandler = Interlocked.CompareExchange<ImapEnvelopeDataChunkReceivedEventHandler>(ref this.r, value2, imapEnvelopeDataChunkReceivedEventHandler2);
				}
				while (imapEnvelopeDataChunkReceivedEventHandler != imapEnvelopeDataChunkReceivedEventHandler2);
			}
		}

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000E05 RID: 3589 RVA: 0x00034C7C File Offset: 0x00033C7C
		// (remove) Token: 0x06000E06 RID: 3590 RVA: 0x00034CB4 File Offset: 0x00033CB4
		public event ImapServerStatusEventHandler ServerStatus
		{
			[CompilerGenerated]
			add
			{
				ImapServerStatusEventHandler imapServerStatusEventHandler = this.s;
				ImapServerStatusEventHandler imapServerStatusEventHandler2;
				do
				{
					imapServerStatusEventHandler2 = imapServerStatusEventHandler;
					ImapServerStatusEventHandler value2 = (ImapServerStatusEventHandler)Delegate.Combine(imapServerStatusEventHandler2, value);
					imapServerStatusEventHandler = Interlocked.CompareExchange<ImapServerStatusEventHandler>(ref this.s, value2, imapServerStatusEventHandler2);
				}
				while (imapServerStatusEventHandler != imapServerStatusEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ImapServerStatusEventHandler imapServerStatusEventHandler = this.s;
				ImapServerStatusEventHandler imapServerStatusEventHandler2;
				do
				{
					imapServerStatusEventHandler2 = imapServerStatusEventHandler;
					ImapServerStatusEventHandler value2 = (ImapServerStatusEventHandler)Delegate.Remove(imapServerStatusEventHandler2, value);
					imapServerStatusEventHandler = Interlocked.CompareExchange<ImapServerStatusEventHandler>(ref this.s, value2, imapServerStatusEventHandler2);
				}
				while (imapServerStatusEventHandler != imapServerStatusEventHandler2);
			}
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000E07 RID: 3591 RVA: 0x00034CEC File Offset: 0x00033CEC
		// (remove) Token: 0x06000E08 RID: 3592 RVA: 0x00034D24 File Offset: 0x00033D24
		public event ImapMessageStatusEventHandler MessageStatus
		{
			[CompilerGenerated]
			add
			{
				ImapMessageStatusEventHandler imapMessageStatusEventHandler = this.t;
				ImapMessageStatusEventHandler imapMessageStatusEventHandler2;
				do
				{
					imapMessageStatusEventHandler2 = imapMessageStatusEventHandler;
					ImapMessageStatusEventHandler value2 = (ImapMessageStatusEventHandler)Delegate.Combine(imapMessageStatusEventHandler2, value);
					imapMessageStatusEventHandler = Interlocked.CompareExchange<ImapMessageStatusEventHandler>(ref this.t, value2, imapMessageStatusEventHandler2);
				}
				while (imapMessageStatusEventHandler != imapMessageStatusEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ImapMessageStatusEventHandler imapMessageStatusEventHandler = this.t;
				ImapMessageStatusEventHandler imapMessageStatusEventHandler2;
				do
				{
					imapMessageStatusEventHandler2 = imapMessageStatusEventHandler;
					ImapMessageStatusEventHandler value2 = (ImapMessageStatusEventHandler)Delegate.Remove(imapMessageStatusEventHandler2, value);
					imapMessageStatusEventHandler = Interlocked.CompareExchange<ImapMessageStatusEventHandler>(ref this.t, value2, imapMessageStatusEventHandler2);
				}
				while (imapMessageStatusEventHandler != imapMessageStatusEventHandler2);
			}
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000E09 RID: 3593 RVA: 0x00034D5C File Offset: 0x00033D5C
		// (remove) Token: 0x06000E0A RID: 3594 RVA: 0x00034D94 File Offset: 0x00033D94
		public event ImapIdlingEventHandler Idling
		{
			[CompilerGenerated]
			add
			{
				ImapIdlingEventHandler imapIdlingEventHandler = this.u;
				ImapIdlingEventHandler imapIdlingEventHandler2;
				do
				{
					imapIdlingEventHandler2 = imapIdlingEventHandler;
					ImapIdlingEventHandler value2 = (ImapIdlingEventHandler)Delegate.Combine(imapIdlingEventHandler2, value);
					imapIdlingEventHandler = Interlocked.CompareExchange<ImapIdlingEventHandler>(ref this.u, value2, imapIdlingEventHandler2);
				}
				while (imapIdlingEventHandler != imapIdlingEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ImapIdlingEventHandler imapIdlingEventHandler = this.u;
				ImapIdlingEventHandler imapIdlingEventHandler2;
				do
				{
					imapIdlingEventHandler2 = imapIdlingEventHandler;
					ImapIdlingEventHandler value2 = (ImapIdlingEventHandler)Delegate.Remove(imapIdlingEventHandler2, value);
					imapIdlingEventHandler = Interlocked.CompareExchange<ImapIdlingEventHandler>(ref this.u, value2, imapIdlingEventHandler2);
				}
				while (imapIdlingEventHandler != imapIdlingEventHandler2);
			}
		}

		// Token: 0x040008D4 RID: 2260
		public const string AllMessages = "1:*";

		// Token: 0x040008D5 RID: 2261
		private bool a;

		// Token: 0x040008D6 RID: 2262
		[CompilerGenerated]
		private EventHandler b;

		// Token: 0x040008D7 RID: 2263
		private ISite c;

		// Token: 0x040008D8 RID: 2264
		[CompilerGenerated]
		private ErrorEventHandler d;

		// Token: 0x040008D9 RID: 2265
		[CompilerGenerated]
		private LogNewEntryEventHandler e;

		// Token: 0x040008DA RID: 2266
		[CompilerGenerated]
		private DataTransferEventHandler f;

		// Token: 0x040008DB RID: 2267
		[CompilerGenerated]
		private DataTransferEventHandler g;

		// Token: 0x040008DC RID: 2268
		[CompilerGenerated]
		private DataTransferEventHandler h;

		// Token: 0x040008DD RID: 2269
		[CompilerGenerated]
		private DataTransferEventHandler i;

		// Token: 0x040008DE RID: 2270
		[CompilerGenerated]
		private HostResolvedEventHandler j;

		// Token: 0x040008DF RID: 2271
		[CompilerGenerated]
		private SocketCreatingEventHandler k;

		// Token: 0x040008E0 RID: 2272
		[CompilerGenerated]
		private SocketConnectedEventHandler l;

		// Token: 0x040008E1 RID: 2273
		[CompilerGenerated]
		private ConnectedEventHandler m;

		// Token: 0x040008E2 RID: 2274
		[CompilerGenerated]
		private DisconnectedEventHandler n;

		// Token: 0x040008E3 RID: 2275
		[CompilerGenerated]
		private TlsStartedEventHandler o;

		// Token: 0x040008E4 RID: 2276
		[CompilerGenerated]
		private LoggedInEventHandler p;

		// Token: 0x040008E5 RID: 2277
		[CompilerGenerated]
		private ImapEnvelopeDownloadedEventHandler q;

		// Token: 0x040008E6 RID: 2278
		[CompilerGenerated]
		private ImapEnvelopeDataChunkReceivedEventHandler r;

		// Token: 0x040008E7 RID: 2279
		[CompilerGenerated]
		private ImapServerStatusEventHandler s;

		// Token: 0x040008E8 RID: 2280
		[CompilerGenerated]
		private ImapMessageStatusEventHandler t;

		// Token: 0x040008E9 RID: 2281
		[CompilerGenerated]
		private ImapIdlingEventHandler u;

		// Token: 0x040008EA RID: 2282
		private global::a.f.o v;

		// Token: 0x040008EB RID: 2283
		private bool w;
	}
}
