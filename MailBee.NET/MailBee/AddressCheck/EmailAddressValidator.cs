using System;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using a.d;
using MailBee.DnsMX;
using MailBee.Mime;

namespace MailBee.AddressCheck
{
	// Token: 0x02000084 RID: 132
	public class EmailAddressValidator : IComponent
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x0000C524 File Offset: 0x0000B524
		public EmailAddressValidator() : this(null)
		{
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000C530 File Offset: 0x0000B530
		public EmailAddressValidator(string licenseKey)
		{
			this.m = new n(null, this, null);
			EmailAddressValidator.a(licenseKey);
			this.j = "user@domain.com";
			this.k = "^(([\\w]+['\\.\\-+])+[\\w]+|([\\w]+))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z0-9]+[\\w-]*\\.)+[a-zA-Z]{2,9})$";
			this.l = AddressValidationLevel.SendAttempt;
			this.c = null;
			this.n = false;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000C582 File Offset: 0x0000B582
		public Task VerifyAsync(DataTable emails, string columnName)
		{
			this.a(emails, null, columnName);
			return this.m.ab();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000C598 File Offset: 0x0000B598
		public Task VerifyAsync(IDataReader emails, string columnName)
		{
			this.a(null, emails, columnName);
			return this.m.ab();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000C5B0 File Offset: 0x0000B5B0
		public Task<AddressValidationLevel> VerifyAsync(string email)
		{
			if (email == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (!Regex.IsMatch(email, this.k))
			{
				return Task.FromResult<AddressValidationLevel>(AddressValidationLevel.RegexCheck);
			}
			if (this.l > AddressValidationLevel.RegexCheck)
			{
				return this.m.c(this.j, email, this.l);
			}
			return Task.FromResult<AddressValidationLevel>(AddressValidationLevel.OK);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000C605 File Offset: 0x0000B605
		public Task VerifyAsync(string[] emails)
		{
			return this.VerifyAsync(this.ArrayToDataTable(emails), "email");
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000C619 File Offset: 0x0000B619
		public int TrialDaysLeft
		{
			get
			{
				return Global.u.b();
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000C625 File Offset: 0x0000B625
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000C637 File Offset: 0x0000B637
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				return this.m.bp().d();
			}
			set
			{
				this.m.bp().a(value);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000C64A File Offset: 0x0000B64A
		public string Version
		{
			get
			{
				return Global.Version;
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000C651 File Offset: 0x0000B651
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000C65A File Offset: 0x0000B65A
		protected virtual void Dispose(bool disposing)
		{
			if (!this.n)
			{
				if (disposing)
				{
					this.m.bo();
					if (this.b != null)
					{
						this.b(this, EventArgs.Empty);
					}
				}
				this.n = true;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600045C RID: 1116 RVA: 0x0000C694 File Offset: 0x0000B694
		// (remove) Token: 0x0600045D RID: 1117 RVA: 0x0000C6CC File Offset: 0x0000B6CC
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

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000C701 File Offset: 0x0000B701
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0000C709 File Offset: 0x0000B709
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

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000C712 File Offset: 0x0000B712
		public bool IsBusy
		{
			get
			{
				return this.m.bc();
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000C71F File Offset: 0x0000B71F
		public void Abort()
		{
			this.m.bd();
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000C72C File Offset: 0x0000B72C
		public bool IsAborted
		{
			get
			{
				return this.m.bf();
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000C739 File Offset: 0x0000B739
		public Logger Log
		{
			get
			{
				return this.m.bi();
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000C746 File Offset: 0x0000B746
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x0000C753 File Offset: 0x0000B753
		public bool RaiseEvents
		{
			get
			{
				return this.m.bq();
			}
			set
			{
				this.m.k(value);
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000C761 File Offset: 0x0000B761
		internal bool e()
		{
			return this.d != null;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000C76C File Offset: 0x0000B76C
		protected internal void OnErrorOccurred(ErrorEventArgs args)
		{
			this.m.bp().a(this.d, this, args);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000C786 File Offset: 0x0000B786
		internal bool f()
		{
			return this.e != null;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000C791 File Offset: 0x0000B791
		protected internal void OnLogNewEntry(LogNewEntryEventArgs args)
		{
			this.m.bp().a(this.e, this, args);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000C7AB File Offset: 0x0000B7AB
		internal bool c()
		{
			return this.f != null;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000C7B6 File Offset: 0x0000B7B6
		protected internal void OnDataReceived(DataTransferEventArgs args)
		{
			this.m.bp().a(this.f, this, args);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000C7D0 File Offset: 0x0000B7D0
		internal bool a()
		{
			return this.g != null;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000C7DB File Offset: 0x0000B7DB
		protected internal void OnDataSent(DataTransferEventArgs args)
		{
			this.m.bp().a(this.g, this, args);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000C7F5 File Offset: 0x0000B7F5
		internal bool b()
		{
			return this.h != null;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000C800 File Offset: 0x0000B800
		protected internal void OnVerifying(VerifyingEventArgs args)
		{
			this.m.bp().a(this.h, this, args);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000C81A File Offset: 0x0000B81A
		internal bool d()
		{
			return this.i != null;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000C825 File Offset: 0x0000B825
		protected internal void OnVerified(VerifiedEventArgs args)
		{
			this.m.bp().a(this.i, this, args);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000C83F File Offset: 0x0000B83F
		private static void a(string A_0)
		{
			Global.a(typeof(EmailAddressValidator), A_0);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000C851 File Offset: 0x0000B851
		public void ResetState()
		{
			this.m.cb();
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0000C85E File Offset: 0x0000B85E
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0000C86B File Offset: 0x0000B86B
		public int MaxThreadCount
		{
			get
			{
				return this.m.m();
			}
			set
			{
				this.m.a(value);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000C879 File Offset: 0x0000B879
		public DnsServerCollection DnsServers
		{
			get
			{
				return this.m.aq();
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000C886 File Offset: 0x0000B886
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0000C88E File Offset: 0x0000B88E
		public string MailFrom
		{
			get
			{
				return this.j;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.j = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000C8A2 File Offset: 0x0000B8A2
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000C8AA File Offset: 0x0000B8AA
		public string RegexPattern
		{
			get
			{
				return this.k;
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				this.k = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000C8BE File Offset: 0x0000B8BE
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0000C8C6 File Offset: 0x0000B8C6
		public AddressValidationLevel ValidationLevel
		{
			get
			{
				return this.l;
			}
			set
			{
				this.l = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000C8CF File Offset: 0x0000B8CF
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000C8E1 File Offset: 0x0000B8E1
		public string HelloDomain
		{
			get
			{
				return this.m.aa().HelloDomain;
			}
			set
			{
				this.m.aa().HelloDomain = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000C8F4 File Offset: 0x0000B8F4
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000C906 File Offset: 0x0000B906
		public int SmtpTimeout
		{
			get
			{
				return this.m.aa().Timeout;
			}
			set
			{
				if (value < 0)
				{
					throw new MailBeeInvalidArgumentException(23);
				}
				this.m.aa().Timeout = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000C925 File Offset: 0x0000B925
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0000C937 File Offset: 0x0000B937
		public bool Pipelining
		{
			get
			{
				return this.m.aa().Pipelining;
			}
			set
			{
				this.m.aa().Pipelining = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000C94A File Offset: 0x0000B94A
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000C95C File Offset: 0x0000B95C
		public EndPoint LocalEndPoint
		{
			get
			{
				return this.m.aa().LocalEndPoint;
			}
			set
			{
				this.m.aa().LocalEndPoint = value;
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000C970 File Offset: 0x0000B970
		public AddressValidationLevel Verify(string email)
		{
			if (email == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (!Regex.IsMatch(email, this.k))
			{
				return AddressValidationLevel.RegexCheck;
			}
			if (this.l > AddressValidationLevel.RegexCheck)
			{
				return this.m.a(true, this.j, email, this.l);
			}
			return AddressValidationLevel.OK;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000C9BC File Offset: 0x0000B9BC
		public DataTable ArrayToDataTable(string[] emails)
		{
			if (emails == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("email", typeof(string));
			foreach (string value in emails)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["email"] = value;
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000CA2C File Offset: 0x0000BA2C
		public void Verify(string[] emails)
		{
			this.Verify(this.ArrayToDataTable(emails), "email");
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000CA40 File Offset: 0x0000BA40
		private void a(DataTable A_0, IDataReader A_1, string A_2)
		{
			int a_ = -1;
			if (A_2 == null || A_2 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			if (A_0 != null)
			{
				DataColumn dataColumn = A_0.Columns[A_2];
				if (dataColumn == null || dataColumn.DataType != typeof(string))
				{
					throw new MailBeeInvalidArgumentException(20);
				}
			}
			else
			{
				if (A_1 == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				try
				{
					a_ = A_1.GetOrdinal(A_2);
				}
				catch (ArgumentException)
				{
					throw new MailBeeInvalidArgumentException(20);
				}
				if (A_1.GetFieldType(a_) != typeof(string))
				{
					throw new MailBeeInvalidArgumentException(20);
				}
			}
			if (this.m.aq().Count == 0)
			{
				throw new MailBeeInvalidArgumentException(212);
			}
			this.m.s().Clear();
			this.m.ar().Clear();
			this.m.a(null, this.j, new EmailAddressCollection("##" + A_2 + "##"), A_0, null, A_1, this.l > AddressValidationLevel.DnsQuery, this.l > AddressValidationLevel.SmtpConnection, false, this.l, A_2, a_, new Regex(this.k), false, false);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000CB78 File Offset: 0x0000BB78
		public void Verify(DataTable emails, string columnName)
		{
			this.a(emails, null, columnName);
			this.m.y();
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000CB8F File Offset: 0x0000BB8F
		public void Verify(IDataReader emails, string columnName)
		{
			this.a(null, emails, columnName);
			this.m.y();
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000CBA6 File Offset: 0x0000BBA6
		[Obsolete("This method is obsolete in .NET 4.5+. Use VerifyAsync instead.")]
		public IAsyncResult BeginVerify(DataTable emailsAsTable, IDataReader emailsAsReader, string columnName, AsyncCallback callback, object state)
		{
			this.a(emailsAsTable, emailsAsReader, columnName);
			return this.m.d(callback, state);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000CBC0 File Offset: 0x0000BBC0
		public void EndVerify()
		{
			this.m.@as();
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600048D RID: 1165 RVA: 0x0000CBD0 File Offset: 0x0000BBD0
		// (remove) Token: 0x0600048E RID: 1166 RVA: 0x0000CC08 File Offset: 0x0000BC08
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

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600048F RID: 1167 RVA: 0x0000CC40 File Offset: 0x0000BC40
		// (remove) Token: 0x06000490 RID: 1168 RVA: 0x0000CC78 File Offset: 0x0000BC78
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

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000491 RID: 1169 RVA: 0x0000CCB0 File Offset: 0x0000BCB0
		// (remove) Token: 0x06000492 RID: 1170 RVA: 0x0000CCE8 File Offset: 0x0000BCE8
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

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000493 RID: 1171 RVA: 0x0000CD20 File Offset: 0x0000BD20
		// (remove) Token: 0x06000494 RID: 1172 RVA: 0x0000CD58 File Offset: 0x0000BD58
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

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000495 RID: 1173 RVA: 0x0000CD90 File Offset: 0x0000BD90
		// (remove) Token: 0x06000496 RID: 1174 RVA: 0x0000CDC8 File Offset: 0x0000BDC8
		public event VerifyingEventHandler Verifying
		{
			[CompilerGenerated]
			add
			{
				VerifyingEventHandler verifyingEventHandler = this.h;
				VerifyingEventHandler verifyingEventHandler2;
				do
				{
					verifyingEventHandler2 = verifyingEventHandler;
					VerifyingEventHandler value2 = (VerifyingEventHandler)Delegate.Combine(verifyingEventHandler2, value);
					verifyingEventHandler = Interlocked.CompareExchange<VerifyingEventHandler>(ref this.h, value2, verifyingEventHandler2);
				}
				while (verifyingEventHandler != verifyingEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				VerifyingEventHandler verifyingEventHandler = this.h;
				VerifyingEventHandler verifyingEventHandler2;
				do
				{
					verifyingEventHandler2 = verifyingEventHandler;
					VerifyingEventHandler value2 = (VerifyingEventHandler)Delegate.Remove(verifyingEventHandler2, value);
					verifyingEventHandler = Interlocked.CompareExchange<VerifyingEventHandler>(ref this.h, value2, verifyingEventHandler2);
				}
				while (verifyingEventHandler != verifyingEventHandler2);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000497 RID: 1175 RVA: 0x0000CE00 File Offset: 0x0000BE00
		// (remove) Token: 0x06000498 RID: 1176 RVA: 0x0000CE38 File Offset: 0x0000BE38
		public event VerifiedEventHandler Verified
		{
			[CompilerGenerated]
			add
			{
				VerifiedEventHandler verifiedEventHandler = this.i;
				VerifiedEventHandler verifiedEventHandler2;
				do
				{
					verifiedEventHandler2 = verifiedEventHandler;
					VerifiedEventHandler value2 = (VerifiedEventHandler)Delegate.Combine(verifiedEventHandler2, value);
					verifiedEventHandler = Interlocked.CompareExchange<VerifiedEventHandler>(ref this.i, value2, verifiedEventHandler2);
				}
				while (verifiedEventHandler != verifiedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				VerifiedEventHandler verifiedEventHandler = this.i;
				VerifiedEventHandler verifiedEventHandler2;
				do
				{
					verifiedEventHandler2 = verifiedEventHandler;
					VerifiedEventHandler value2 = (VerifiedEventHandler)Delegate.Remove(verifiedEventHandler2, value);
					verifiedEventHandler = Interlocked.CompareExchange<VerifiedEventHandler>(ref this.i, value2, verifiedEventHandler2);
				}
				while (verifiedEventHandler != verifiedEventHandler2);
			}
		}

		// Token: 0x0400020A RID: 522
		internal const string a = "^(([\\w]+['\\.\\-+])+[\\w]+|([\\w]+))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z0-9]+[\\w-]*\\.)+[a-zA-Z]{2,9})$";

		// Token: 0x0400020B RID: 523
		[CompilerGenerated]
		private EventHandler b;

		// Token: 0x0400020C RID: 524
		private ISite c;

		// Token: 0x0400020D RID: 525
		[CompilerGenerated]
		private ErrorEventHandler d;

		// Token: 0x0400020E RID: 526
		[CompilerGenerated]
		private LogNewEntryEventHandler e;

		// Token: 0x0400020F RID: 527
		[CompilerGenerated]
		private DataTransferEventHandler f;

		// Token: 0x04000210 RID: 528
		[CompilerGenerated]
		private DataTransferEventHandler g;

		// Token: 0x04000211 RID: 529
		[CompilerGenerated]
		private VerifyingEventHandler h;

		// Token: 0x04000212 RID: 530
		[CompilerGenerated]
		private VerifiedEventHandler i;

		// Token: 0x04000213 RID: 531
		private string j;

		// Token: 0x04000214 RID: 532
		private string k;

		// Token: 0x04000215 RID: 533
		private AddressValidationLevel l;

		// Token: 0x04000216 RID: 534
		private n m;

		// Token: 0x04000217 RID: 535
		private bool n;
	}
}
