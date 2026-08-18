using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using a.l;
using MailBee.Mime;
using Microsoft.Exchange.WebServices.Data;

namespace MailBee.EwsMail
{
	// Token: 0x02000522 RID: 1314
	public class Ews : IComponent
	{
		// Token: 0x06002AD6 RID: 10966 RVA: 0x000CBD74 File Offset: 0x000CAD74
		public Ews() : this(null)
		{
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000CBD7D File Offset: 0x000CAD7D
		public Ews(string licenseKey)
		{
			this.e = new c(this);
			Ews.a(licenseKey);
			this.b = null;
			this.f = false;
			this.a = null;
			this.c = null;
			this.d = null;
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x000CBDBA File Offset: 0x000CADBA
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000CBDC3 File Offset: 0x000CADC3
		protected virtual void Dispose(bool disposing)
		{
			if (!this.f)
			{
				if (disposing && this.a != null)
				{
					this.a(this, EventArgs.Empty);
				}
				this.f = true;
			}
		}

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06002ADA RID: 10970 RVA: 0x000CBDF0 File Offset: 0x000CADF0
		// (remove) Token: 0x06002ADB RID: 10971 RVA: 0x000CBE28 File Offset: 0x000CAE28
		public event EventHandler Disposed
		{
			[CompilerGenerated]
			add
			{
				EventHandler eventHandler = this.a;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.a, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler eventHandler = this.a;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.a, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06002ADC RID: 10972 RVA: 0x000CBE5D File Offset: 0x000CAE5D
		// (set) Token: 0x06002ADD RID: 10973 RVA: 0x000CBE65 File Offset: 0x000CAE65
		public virtual ISite Site
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x000CBE6E File Offset: 0x000CAE6E
		internal bool a()
		{
			return this.c != null;
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x000CBE79 File Offset: 0x000CAE79
		protected internal void OnErrorOccurred(ErrorEventArgs args)
		{
			this.e.bp().a(this.c, this, args);
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000CBE93 File Offset: 0x000CAE93
		internal bool b()
		{
			return this.d != null;
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000CBE9E File Offset: 0x000CAE9E
		protected internal void OnLogNewEntry(LogNewEntryEventArgs args)
		{
			this.e.bp().a(this.d, this, args);
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000CBEB8 File Offset: 0x000CAEB8
		public string GetErrorDescription()
		{
			return this.e.l1();
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x000CBEC5 File Offset: 0x000CAEC5
		public int LastResult
		{
			get
			{
				return this.e.l2();
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x000CBED2 File Offset: 0x000CAED2
		public Logger Log
		{
			get
			{
				return this.e.bi();
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x000CBEDF File Offset: 0x000CAEDF
		// (set) Token: 0x06002AE6 RID: 10982 RVA: 0x000CBEEC File Offset: 0x000CAEEC
		public bool ThrowExceptions
		{
			get
			{
				return this.e.be();
			}
			set
			{
				this.e.ls(value);
			}
		}

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06002AE7 RID: 10983 RVA: 0x000CBEFC File Offset: 0x000CAEFC
		// (remove) Token: 0x06002AE8 RID: 10984 RVA: 0x000CBF34 File Offset: 0x000CAF34
		public event ErrorEventHandler ErrorOccurred
		{
			[CompilerGenerated]
			add
			{
				ErrorEventHandler errorEventHandler = this.c;
				ErrorEventHandler errorEventHandler2;
				do
				{
					errorEventHandler2 = errorEventHandler;
					ErrorEventHandler value2 = (ErrorEventHandler)Delegate.Combine(errorEventHandler2, value);
					errorEventHandler = Interlocked.CompareExchange<ErrorEventHandler>(ref this.c, value2, errorEventHandler2);
				}
				while (errorEventHandler != errorEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ErrorEventHandler errorEventHandler = this.c;
				ErrorEventHandler errorEventHandler2;
				do
				{
					errorEventHandler2 = errorEventHandler;
					ErrorEventHandler value2 = (ErrorEventHandler)Delegate.Remove(errorEventHandler2, value);
					errorEventHandler = Interlocked.CompareExchange<ErrorEventHandler>(ref this.c, value2, errorEventHandler2);
				}
				while (errorEventHandler != errorEventHandler2);
			}
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06002AE9 RID: 10985 RVA: 0x000CBF6C File Offset: 0x000CAF6C
		// (remove) Token: 0x06002AEA RID: 10986 RVA: 0x000CBFA4 File Offset: 0x000CAFA4
		public event LogNewEntryEventHandler LogNewEntry
		{
			[CompilerGenerated]
			add
			{
				LogNewEntryEventHandler logNewEntryEventHandler = this.d;
				LogNewEntryEventHandler logNewEntryEventHandler2;
				do
				{
					logNewEntryEventHandler2 = logNewEntryEventHandler;
					LogNewEntryEventHandler value2 = (LogNewEntryEventHandler)Delegate.Combine(logNewEntryEventHandler2, value);
					logNewEntryEventHandler = Interlocked.CompareExchange<LogNewEntryEventHandler>(ref this.d, value2, logNewEntryEventHandler2);
				}
				while (logNewEntryEventHandler != logNewEntryEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				LogNewEntryEventHandler logNewEntryEventHandler = this.d;
				LogNewEntryEventHandler logNewEntryEventHandler2;
				do
				{
					logNewEntryEventHandler2 = logNewEntryEventHandler;
					LogNewEntryEventHandler value2 = (LogNewEntryEventHandler)Delegate.Remove(logNewEntryEventHandler2, value);
					logNewEntryEventHandler = Interlocked.CompareExchange<LogNewEntryEventHandler>(ref this.d, value2, logNewEntryEventHandler2);
				}
				while (logNewEntryEventHandler != logNewEntryEventHandler2);
			}
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x000CBFD9 File Offset: 0x000CAFD9
		private static void a(string A_0)
		{
			Global.a(typeof(Ews), A_0);
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x000CBFEB File Offset: 0x000CAFEB
		public void InitEwsClient()
		{
			this.e.e();
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x000CBFF8 File Offset: 0x000CAFF8
		public void InitEwsClient(ExchangeVersion version)
		{
			this.e.a(version);
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x000CC006 File Offset: 0x000CB006
		public void InitEwsClient(ExchangeVersion version, TimeZoneInfo timeZone)
		{
			this.e.a(version, timeZone);
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000CC015 File Offset: 0x000CB015
		public bool Autodiscover(string emailAddress)
		{
			return this.e.b(true, emailAddress);
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x000CC024 File Offset: 0x000CB024
		// (set) Token: 0x06002AF1 RID: 10993 RVA: 0x000CC036 File Offset: 0x000CB036
		public bool RequireHttps
		{
			get
			{
				return this.e.c().g();
			}
			set
			{
				this.e.c().e(value);
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000CC049 File Offset: 0x000CB049
		// (set) Token: 0x06002AF3 RID: 10995 RVA: 0x000CC050 File Offset: 0x000CB050
		public static bool EnableSslCertValidation
		{
			get
			{
				return global::a.l.d.c();
			}
			set
			{
				global::a.l.d.c(value);
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x000CC058 File Offset: 0x000CB058
		// (set) Token: 0x06002AF5 RID: 10997 RVA: 0x000CC05F File Offset: 0x000CB05F
		public static bool EnableCompatibilityMode
		{
			get
			{
				return global::a.l.d.c();
			}
			set
			{
				global::a.l.d.c(value);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000CC067 File Offset: 0x000CB067
		// (set) Token: 0x06002AF7 RID: 10999 RVA: 0x000CC079 File Offset: 0x000CB079
		public char FolderLevelDelimiter
		{
			get
			{
				return this.e.c().o();
			}
			set
			{
				this.e.c().a(value);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x000CC08C File Offset: 0x000CB08C
		// (set) Token: 0x06002AF9 RID: 11001 RVA: 0x000CC09E File Offset: 0x000CB09E
		public bool CalculateFolderSizeOnDownload
		{
			get
			{
				return this.e.c().l();
			}
			set
			{
				this.e.c().f(value);
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x000CC0B1 File Offset: 0x000CB0B1
		// (set) Token: 0x06002AFB RID: 11003 RVA: 0x000CC0C3 File Offset: 0x000CB0C3
		public WellKnownFolderName RootFolderType
		{
			get
			{
				return this.e.c().h();
			}
			set
			{
				this.e.c().a(value);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x000CC0D6 File Offset: 0x000CB0D6
		// (set) Token: 0x06002AFD RID: 11005 RVA: 0x000CC0E8 File Offset: 0x000CB0E8
		public DeleteMode DeleteMethod
		{
			get
			{
				return this.e.c().n();
			}
			set
			{
				this.e.c().a(value);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x000CC0FB File Offset: 0x000CB0FB
		// (set) Token: 0x06002AFF RID: 11007 RVA: 0x000CC10D File Offset: 0x000CB10D
		public string DefaultFolderClass
		{
			get
			{
				return this.e.c().m();
			}
			set
			{
				this.e.c().e(value);
			}
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000CC120 File Offset: 0x000CB120
		public void SetCredentials(string accountName, string password)
		{
			this.e.c().b(accountName, password);
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000CC134 File Offset: 0x000CB134
		public void SetServerUrl(string url)
		{
			this.e.c().h(url);
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000CC147 File Offset: 0x000CB147
		public FolderView GetFolderView(bool includeSubfolders)
		{
			return this.e.c().d(includeSubfolders);
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06002B03 RID: 11011 RVA: 0x000CC15A File Offset: 0x000CB15A
		public SearchFilter MessageFoldersFilter
		{
			get
			{
				return this.e.c().f();
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x000CC16C File Offset: 0x000CB16C
		public ExchangeService Service
		{
			get
			{
				return this.e.c().k();
			}
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000CC17E File Offset: 0x000CB17E
		public bool TestConnection()
		{
			return this.e.a(true);
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000CC18C File Offset: 0x000CB18C
		public List<EwsFolder> DownloadFolders(bool includeSubfolders)
		{
			return this.e.a(true, false, includeSubfolders);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000CC19C File Offset: 0x000CB19C
		public List<EwsFolder> DownloadFolders(FolderId parentFolderId, bool includeParentFolder, bool includeSubfolders)
		{
			return this.e.a(true, parentFolderId, includeParentFolder, includeSubfolders);
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000CC1AD File Offset: 0x000CB1AD
		public List<EwsFolder> DownloadFolders(FolderId parentFolderId, FolderView view, SearchFilter filter, bool includeParentFolder)
		{
			return this.e.a(true, parentFolderId, view, filter, includeParentFolder);
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000CC1C0 File Offset: 0x000CB1C0
		public bool FolderExists(FolderId parentFolderId, string folderName)
		{
			return this.e.d(true, parentFolderId, folderName);
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000CC1D0 File Offset: 0x000CB1D0
		public bool FolderExists(string folderFullName)
		{
			return this.e.c(true, folderFullName);
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000CC1DF File Offset: 0x000CB1DF
		public EwsFolder DownloadFolderByShortName(FolderId parentFolderId, string folderName)
		{
			return this.e.c(true, parentFolderId, folderName);
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000CC1EF File Offset: 0x000CB1EF
		public FolderId FindFolderIdByShortName(FolderId parentFolderId, string folderName)
		{
			return this.e.a(true, parentFolderId, folderName);
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000CC1FF File Offset: 0x000CB1FF
		public EwsFolder DownloadFolderByFullName(FolderId containingFolderId, string folderFullName)
		{
			return this.e.b(true, containingFolderId, folderFullName);
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000CC20F File Offset: 0x000CB20F
		public FolderId FindFolderIdByFullName(FolderId containingFolderId, string folderFullName)
		{
			return this.e.e(true, containingFolderId, folderFullName);
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000CC21F File Offset: 0x000CB21F
		public EwsFolder DownloadFolderByFullName(string folderFullName)
		{
			return this.e.b(true, null, folderFullName);
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000CC22F File Offset: 0x000CB22F
		public FolderId FindFolderIdByFullName(string folderFullName)
		{
			return this.e.e(true, null, folderFullName);
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000CC23F File Offset: 0x000CB23F
		public EwsFolder DownloadFolderByFullName(string folderFullName, int recursionLevel)
		{
			return this.e.a(true, folderFullName, recursionLevel);
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000CC24F File Offset: 0x000CB24F
		public FolderId FindFolderIdByFullName(string folderFullName, int recursionLevel)
		{
			return this.e.b(true, folderFullName, recursionLevel);
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000CC25F File Offset: 0x000CB25F
		public EwsFolder DownloadFolderById(FolderId id, PropertySet propSet)
		{
			return this.e.a(true, id, propSet);
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000CC26F File Offset: 0x000CB26F
		public EwsFolder DownloadFolderById(FolderId id)
		{
			return this.e.a(true, id);
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000CC27E File Offset: 0x000CB27E
		public EwsFolder GetFolderByName(string folderFullName, List<EwsFolder> folders)
		{
			return this.e.c().a(folderFullName, folders);
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000CC292 File Offset: 0x000CB292
		public bool CreateFolder(string newFolderName, FolderId parentFolderId)
		{
			return this.e.b(true, newFolderName, parentFolderId);
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000CC2A2 File Offset: 0x000CB2A2
		public bool RenameFolder(string folderNewName, FolderId id)
		{
			return this.e.a(true, folderNewName, id);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000CC2B2 File Offset: 0x000CB2B2
		public bool MoveFolder(FolderId id, FolderId destinationFolderId)
		{
			return this.e.a(true, id, destinationFolderId);
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000CC2C2 File Offset: 0x000CB2C2
		public bool RenameOrMoveFolder(string folderOldFullName, string folderNewFullName, List<EwsFolder> allFolders)
		{
			return this.e.a(true, folderOldFullName, folderNewFullName, allFolders);
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000CC2D3 File Offset: 0x000CB2D3
		public bool DeleteFolder(FolderId id)
		{
			return this.e.b(true, id);
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000CC2E2 File Offset: 0x000CB2E2
		public bool EmptyFolder(FolderId id, bool deleteSubFolders)
		{
			return this.e.b(true, id, deleteSubFolders);
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000CC2F2 File Offset: 0x000CB2F2
		public PropertySet CreatePropSet(EwsItemParts parts)
		{
			return this.e.c().a(parts);
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000CC305 File Offset: 0x000CB305
		public EwsItemList DownloadItems(FolderId id, bool unreadOnly)
		{
			return this.e.a(true, id, unreadOnly);
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000CC315 File Offset: 0x000CB315
		public EwsItemList DownloadItems(FolderId id, ItemView view, bool unreadOnly)
		{
			return this.e.b(true, id, view, unreadOnly);
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000CC326 File Offset: 0x000CB326
		public EwsItemList DownloadItems(FolderId id, ItemView view, bool unreadOnly, PropertySet properties)
		{
			return this.e.a(true, id, view, unreadOnly, properties);
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x000CC339 File Offset: 0x000CB339
		public EwsItemList DownloadItems(FolderId id, ItemView view, bool unreadOnly, EwsItemParts parts)
		{
			return this.e.a(true, id, view, unreadOnly, parts);
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x000CC34C File Offset: 0x000CB34C
		public EwsItemList DownloadItems(FolderId id, int startIndex, int count, bool unreadOnly)
		{
			return this.e.a(true, id, startIndex, count, unreadOnly);
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000CC35F File Offset: 0x000CB35F
		public EwsItemList DownloadItems(FolderId id, int startIndex, int count, bool unreadOnly, PropertySet properties)
		{
			return this.e.a(true, id, startIndex, count, unreadOnly, properties);
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000CC374 File Offset: 0x000CB374
		public EwsItemList DownloadItems(FolderId id, int startIndex, int count, bool unreadOnly, EwsItemParts parts)
		{
			return this.e.a(true, id, startIndex, count, unreadOnly, parts);
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000CC389 File Offset: 0x000CB389
		public EwsItem DownloadItem(FolderId id, int index)
		{
			return this.e.a(true, id, index);
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000CC399 File Offset: 0x000CB399
		public EwsItem DownloadItem(FolderId id, int index, PropertySet properties)
		{
			return this.e.a(true, id, index, properties);
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000CC3AA File Offset: 0x000CB3AA
		public EwsItem DownloadItem(FolderId id, int index, EwsItemParts parts)
		{
			return this.e.a(true, id, index, parts);
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x000CC3BB File Offset: 0x000CB3BB
		public EwsItemList DownloadItems(IEnumerable<EwsItem> itemIds, PropertySet properties)
		{
			return this.e.a(true, itemIds, properties);
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000CC3CB File Offset: 0x000CB3CB
		public EwsItemList DownloadItems(IEnumerable<EwsItem> itemIds, EwsItemParts parts)
		{
			return this.e.a(true, itemIds, parts);
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x000CC3DB File Offset: 0x000CB3DB
		public EwsItemList DownloadItemIds(FolderId id, bool unreadOnly)
		{
			return this.e.c(true, id, unreadOnly);
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x000CC3EB File Offset: 0x000CB3EB
		public EwsItemList DownloadItemIds(FolderId id, ItemView view, bool unreadOnly)
		{
			return this.e.a(true, id, view, unreadOnly);
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x000CC3FC File Offset: 0x000CB3FC
		public EwsItem DownloadItem(ItemId id, EwsItemParts parts)
		{
			return this.e.a(true, id, parts);
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x000CC40C File Offset: 0x000CB40C
		public EwsItem DownloadItem(ItemId id, PropertySet properties)
		{
			return this.e.a(true, id, properties);
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x000CC41C File Offset: 0x000CB41C
		public MailMessage DownloadEntireMessage(ItemId id)
		{
			return this.e.c(true, id);
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x000CC42B File Offset: 0x000CB42B
		public List<Microsoft.Exchange.WebServices.Data.Attachment> DownloadNativeAttachments(string[] attachmentIds, bool ignoreInlineAttachments)
		{
			return this.e.a(true, attachmentIds, ignoreInlineAttachments);
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x000CC43B File Offset: 0x000CB43B
		public FileAttachment AddAttachmentToItem(ItemId id, string filename, string targetFilename)
		{
			return this.e.a(true, id, filename, targetFilename);
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x000CC44C File Offset: 0x000CB44C
		public bool DeleteAttachments(ItemId id)
		{
			return this.e.b(true, id);
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x000CC45B File Offset: 0x000CB45B
		public int DeleteAttachment(ItemId id, string filenameOrAttachId, bool isAttachId, bool deleteFirstOccurenceOnly)
		{
			return this.e.a(true, id, filenameOrAttachId, isAttachId, deleteFirstOccurenceOnly);
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x000CC46E File Offset: 0x000CB46E
		public bool UpdateItem(EwsItem item)
		{
			return this.e.a(true, item);
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x000CC47D File Offset: 0x000CB47D
		public bool UploadMessage(FolderId id, MailMessage msg, bool isDraft)
		{
			return this.e.a(true, id, msg, isDraft);
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x000CC48E File Offset: 0x000CB48E
		public bool UploadMessage(FolderId id, byte[] bytes, bool isDraft)
		{
			return this.e.a(true, id, bytes, isDraft);
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x000CC49F File Offset: 0x000CB49F
		public ItemId CopyItem(ItemId copiedItemId, FolderId targetFolderId)
		{
			return this.e.b(true, copiedItemId, targetFolderId);
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x000CC4AF File Offset: 0x000CB4AF
		public ItemId MoveItem(ItemId movedItemId, FolderId targetFolderId)
		{
			return this.e.a(true, movedItemId, targetFolderId);
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x000CC4BF File Offset: 0x000CB4BF
		public bool DeleteItem(ItemId id)
		{
			return this.e.a(true, id);
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x000CC4CE File Offset: 0x000CB4CE
		public List<ItemId> DeleteItems(IEnumerable<ItemId> ids)
		{
			return this.e.a(true, ids);
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x000CC4DD File Offset: 0x000CB4DD
		public EwsItemList Search(FolderId id, SearchFilter filter, ItemView view)
		{
			return this.e.a(true, id, filter, view);
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x000CC4EE File Offset: 0x000CB4EE
		public EwsItemList Search(FolderId id, SearchFilter filter)
		{
			return this.e.a(true, id, filter);
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x000CC4FE File Offset: 0x000CB4FE
		public static List<ItemId> EwsItemsToItemIds(IEnumerable<EwsItem> items)
		{
			return global::a.l.d.a(items);
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x000CC506 File Offset: 0x000CB506
		public bool SendMessage(MailMessage msg)
		{
			return this.e.a(true, msg);
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x000CC515 File Offset: 0x000CB515
		public bool SendMessageAndSaveCopy(MailMessage msg, FolderId sentMessagesFolderId)
		{
			return this.e.a(true, msg, sentMessagesFolderId);
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x000CC525 File Offset: 0x000CB525
		public MailBee.Mime.EmailAddressCollection ResolveName(string name)
		{
			return this.e.a(true, name);
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x000CC534 File Offset: 0x000CB534
		public string GetExchangeVersionString()
		{
			return this.e.c().j();
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x000CC546 File Offset: 0x000CB546
		public int TrialDaysLeft
		{
			get
			{
				return Global.u.b();
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x000CC552 File Offset: 0x000CB552
		public string Version
		{
			get
			{
				return Global.Version;
			}
		}

		// Token: 0x04001DD6 RID: 7638
		[CompilerGenerated]
		private EventHandler a;

		// Token: 0x04001DD7 RID: 7639
		private ISite b;

		// Token: 0x04001DD8 RID: 7640
		[CompilerGenerated]
		private ErrorEventHandler c;

		// Token: 0x04001DD9 RID: 7641
		[CompilerGenerated]
		private LogNewEntryEventHandler d;

		// Token: 0x04001DDA RID: 7642
		private c e;

		// Token: 0x04001DDB RID: 7643
		private bool f;
	}
}
