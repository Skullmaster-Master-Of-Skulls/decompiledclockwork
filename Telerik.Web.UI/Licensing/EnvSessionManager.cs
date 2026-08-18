using System;

namespace Telerik.Licensing
{
	// Token: 0x02000428 RID: 1064
	internal class EnvSessionManager : ISessionManager
	{
		// Token: 0x0600262E RID: 9774 RVA: 0x0007D506 File Offset: 0x0007B706
		public EnvSessionManager(EnvDTEInterop dte, ISerializationService service)
		{
			this._dte = dte;
			this._service = service;
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x0007D51C File Offset: 0x0007B71C
		public EnvDTEInterop Environment
		{
			get
			{
				return this._dte;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x0007D524 File Offset: 0x0007B724
		protected ISerializationService SerializationService
		{
			get
			{
				return this._service;
			}
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x0007D52C File Offset: 0x0007B72C
		public Session GetSessionByName(SessionName name)
		{
			if (this._session == null)
			{
				if (this.Exists(name))
				{
					this._session = this.Load(name);
				}
				else
				{
					this._session = this.Create(name);
				}
			}
			this.EnsureNotExpired(this._session);
			return this._session;
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x0007D578 File Offset: 0x0007B778
		public Session GetCurrentSession()
		{
			return this.GetSessionByName(new SessionName(this.Environment.GetName(), false));
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x0007D591 File Offset: 0x0007B791
		private void EnsureNotExpired(Session session)
		{
			if (session.IsExpired())
			{
				session.Reset();
			}
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x0007D5A4 File Offset: 0x0007B7A4
		public Session Create(SessionName name)
		{
			Session session = new Session();
			session.SetName(name);
			session.SessionChanged += this.SessionChanged;
			return session;
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x0007D5D2 File Offset: 0x0007B7D2
		public bool Exists(SessionName name)
		{
			return this.Environment != null && this.Environment.GetViableExists(name.Name);
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x0007D5EF File Offset: 0x0007B7EF
		public void Save(Session session)
		{
			this.Environment.SetVariable(session.GetName().Name, this.SerializationService.Serialize<Session>(session));
			this.Environment.SetVariablePerists(session.GetName().Name, false);
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x0007D62C File Offset: 0x0007B82C
		public Session Load(SessionName name)
		{
			Session session = this.SerializationService.Deserialize<Session>((string)this.Environment.GetVariable(name.Name));
			session.SetName(name);
			session.SessionChanged += this.SessionChanged;
			return session;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x0007D676 File Offset: 0x0007B876
		private void SessionChanged(object sender, SessionChangedEventArgs e)
		{
			this.Save(e.Session);
		}

		// Token: 0x040009BD RID: 2493
		private readonly EnvDTEInterop _dte;

		// Token: 0x040009BE RID: 2494
		private readonly ISerializationService _service;

		// Token: 0x040009BF RID: 2495
		private Session _session;
	}
}
