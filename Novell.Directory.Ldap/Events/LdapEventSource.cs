using System;
using System.Threading;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x02000084 RID: 132
	public abstract class LdapEventSource
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00014A38 File Offset: 0x00013A38
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x00014A50 File Offset: 0x00013A50
		public int SleepInterval
		{
			get
			{
				return this.sleep_interval;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("SleepInterval", "cannot take the negative or zero values ");
				}
				this.sleep_interval = value;
			}
		}

		// Token: 0x06000447 RID: 1095
		protected abstract int GetListeners();

		// Token: 0x06000448 RID: 1096 RVA: 0x00014A78 File Offset: 0x00013A78
		protected LdapEventSource.LISTENERS_COUNT GetCurrentListenersState()
		{
			int num = 0;
			num += this.GetListeners();
			if (this.directory_event != null)
			{
				num += this.directory_event.GetInvocationList().Length;
			}
			if (this.directory_exception_event != null)
			{
				num += this.directory_exception_event.GetInvocationList().Length;
			}
			LdapEventSource.LISTENERS_COUNT result;
			if (num == 0)
			{
				result = LdapEventSource.LISTENERS_COUNT.ZERO;
			}
			else if (1 == num)
			{
				result = LdapEventSource.LISTENERS_COUNT.ONE;
			}
			else
			{
				result = LdapEventSource.LISTENERS_COUNT.MORE_THAN_ONE;
			}
			return result;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00014AD4 File Offset: 0x00013AD4
		protected void ListenerAdded()
		{
			switch (this.GetCurrentListenersState())
			{
			case LdapEventSource.LISTENERS_COUNT.ONE:
				this.StartSearchAndPolling();
				break;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00014B08 File Offset: 0x00013B08
		protected void ListenerRemoved()
		{
			switch (this.GetCurrentListenersState())
			{
			case LdapEventSource.LISTENERS_COUNT.ZERO:
				this.StopSearchAndPolling();
				break;
			}
		}

		// Token: 0x0600044B RID: 1099
		protected abstract void StartSearchAndPolling();

		// Token: 0x0600044C RID: 1100
		protected abstract void StopSearchAndPolling();

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600044D RID: 1101 RVA: 0x00014B3C File Offset: 0x00013B3C
		// (remove) Token: 0x0600044E RID: 1102 RVA: 0x00014B68 File Offset: 0x00013B68
		public event LdapEventSource.DirectoryEventHandler DirectoryEvent
		{
			add
			{
				this.directory_event = (LdapEventSource.DirectoryEventHandler)Delegate.Combine(this.directory_event, value);
				this.ListenerAdded();
			}
			remove
			{
				this.directory_event = (LdapEventSource.DirectoryEventHandler)Delegate.Remove(this.directory_event, value);
				this.ListenerRemoved();
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600044F RID: 1103 RVA: 0x00014B94 File Offset: 0x00013B94
		// (remove) Token: 0x06000450 RID: 1104 RVA: 0x00014BC0 File Offset: 0x00013BC0
		public event LdapEventSource.DirectoryExceptionEventHandler DirectoryExceptionEvent
		{
			add
			{
				this.directory_exception_event = (LdapEventSource.DirectoryExceptionEventHandler)Delegate.Combine(this.directory_exception_event, value);
				this.ListenerAdded();
			}
			remove
			{
				this.directory_exception_event = (LdapEventSource.DirectoryExceptionEventHandler)Delegate.Remove(this.directory_exception_event, value);
				this.ListenerRemoved();
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00014BEC File Offset: 0x00013BEC
		protected void StartEventPolling(LdapMessageQueue queue, LdapConnection conn, int msgid)
		{
			if (queue == null || conn == null)
			{
				throw new ArgumentException("No parameter can be Null.");
			}
			if (this.m_objEventsGenerator == null)
			{
				this.m_objEventsGenerator = new LdapEventSource.EventsGenerator(this, queue, conn, msgid);
				this.m_objEventsGenerator.SleepTime = this.sleep_interval;
				this.m_objEventsGenerator.StartEventPolling();
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00014C40 File Offset: 0x00013C40
		protected void StopEventPolling()
		{
			if (this.m_objEventsGenerator != null)
			{
				this.m_objEventsGenerator.StopEventPolling();
				this.m_objEventsGenerator = null;
			}
		}

		// Token: 0x06000453 RID: 1107
		protected abstract bool NotifyEventListeners(LdapMessage sourceMessage, EventClassifiers aClassification, int nType);

		// Token: 0x06000454 RID: 1108 RVA: 0x00014C68 File Offset: 0x00013C68
		protected void NotifyListeners(LdapMessage sourceMessage, EventClassifiers aClassification, int nType)
		{
			if (!this.NotifyEventListeners(sourceMessage, aClassification, nType))
			{
				this.NotifyDirectoryListeners(sourceMessage, aClassification);
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00014C8C File Offset: 0x00013C8C
		protected void NotifyDirectoryListeners(LdapMessage sourceMessage, EventClassifiers aClassification)
		{
			this.NotifyDirectoryListeners(new DirectoryEventArgs(sourceMessage, aClassification));
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00014CA8 File Offset: 0x00013CA8
		protected void NotifyDirectoryListeners(DirectoryEventArgs objDirectoryEventArgs)
		{
			if (this.directory_event != null)
			{
				this.directory_event(this, objDirectoryEventArgs);
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00014CCC File Offset: 0x00013CCC
		protected void NotifyExceptionListeners(LdapMessage sourceMessage, LdapException ldapException)
		{
			if (this.directory_exception_event != null)
			{
				this.directory_exception_event(this, new DirectoryExceptionEventArgs(sourceMessage, ldapException));
			}
		}

		// Token: 0x04000317 RID: 791
		protected internal const int EVENT_TYPE_UNKNOWN = -1;

		// Token: 0x04000318 RID: 792
		protected const int DEFAULT_SLEEP_TIME = 1000;

		// Token: 0x04000319 RID: 793
		protected int sleep_interval = 1000;

		// Token: 0x0400031A RID: 794
		protected LdapEventSource.DirectoryEventHandler directory_event;

		// Token: 0x0400031B RID: 795
		protected LdapEventSource.DirectoryExceptionEventHandler directory_exception_event;

		// Token: 0x0400031C RID: 796
		protected LdapEventSource.EventsGenerator m_objEventsGenerator = null;

		// Token: 0x02000085 RID: 133
		protected enum LISTENERS_COUNT
		{
			// Token: 0x0400031E RID: 798
			ZERO,
			// Token: 0x0400031F RID: 799
			ONE,
			// Token: 0x04000320 RID: 800
			MORE_THAN_ONE
		}

		// Token: 0x02000086 RID: 134
		// (Invoke) Token: 0x0600045A RID: 1114
		public delegate void DirectoryEventHandler(object source, DirectoryEventArgs objDirectoryEventArgs);

		// Token: 0x02000087 RID: 135
		// (Invoke) Token: 0x0600045E RID: 1118
		public delegate void DirectoryExceptionEventHandler(object source, DirectoryExceptionEventArgs objDirectoryExceptionEventArgs);

		// Token: 0x02000088 RID: 136
		protected class EventsGenerator
		{
			// Token: 0x17000130 RID: 304
			// (get) Token: 0x06000461 RID: 1121 RVA: 0x00014D1C File Offset: 0x00013D1C
			// (set) Token: 0x06000462 RID: 1122 RVA: 0x00014D34 File Offset: 0x00013D34
			public int SleepTime
			{
				get
				{
					return this.sleep_time;
				}
				set
				{
					this.sleep_time = value;
				}
			}

			// Token: 0x06000463 RID: 1123 RVA: 0x00014D48 File Offset: 0x00013D48
			public EventsGenerator(LdapEventSource objEventSource, LdapMessageQueue queue, LdapConnection conn, int msgid)
			{
				this.m_objLdapEventSource = objEventSource;
				this.searchqueue = queue;
				this.ldapconnection = conn;
				this.messageid = msgid;
				this.sleep_time = 1000;
			}

			// Token: 0x06000464 RID: 1124 RVA: 0x00014D8C File Offset: 0x00013D8C
			protected void Run()
			{
				while (this.isrunning)
				{
					LdapMessage ldapMessage = null;
					try
					{
						while (this.isrunning && !this.searchqueue.isResponseReceived(this.messageid))
						{
							try
							{
								Thread.Sleep(this.sleep_time);
							}
							catch (ThreadInterruptedException arg)
							{
								Console.WriteLine("EventsGenerator::Run Got ThreadInterruptedException e = {0}", arg);
							}
						}
						if (this.isrunning)
						{
							ldapMessage = this.searchqueue.getResponse(this.messageid);
						}
						if (ldapMessage != null)
						{
							this.processmessage(ldapMessage);
						}
					}
					catch (LdapException ldapException)
					{
						this.m_objLdapEventSource.NotifyExceptionListeners(ldapMessage, ldapException);
					}
				}
			}

			// Token: 0x06000465 RID: 1125 RVA: 0x00014E50 File Offset: 0x00013E50
			protected void processmessage(LdapMessage response)
			{
				if (response is LdapResponse)
				{
					try
					{
						((LdapResponse)response).chkResultCode();
						this.m_objLdapEventSource.NotifyEventListeners(response, EventClassifiers.CLASSIFICATION_UNKNOWN, -1);
					}
					catch (LdapException ldapException)
					{
						this.m_objLdapEventSource.NotifyExceptionListeners(response, ldapException);
					}
				}
				else
				{
					this.m_objLdapEventSource.NotifyEventListeners(response, EventClassifiers.CLASSIFICATION_UNKNOWN, -1);
				}
			}

			// Token: 0x06000466 RID: 1126 RVA: 0x00014EC0 File Offset: 0x00013EC0
			public void StartEventPolling()
			{
				this.isrunning = true;
				new Thread(new ThreadStart(this.Run)).Start();
			}

			// Token: 0x06000467 RID: 1127 RVA: 0x00014EEC File Offset: 0x00013EEC
			public void StopEventPolling()
			{
				this.isrunning = false;
			}

			// Token: 0x04000321 RID: 801
			private LdapEventSource m_objLdapEventSource;

			// Token: 0x04000322 RID: 802
			private LdapMessageQueue searchqueue;

			// Token: 0x04000323 RID: 803
			private int messageid;

			// Token: 0x04000324 RID: 804
			private LdapConnection ldapconnection;

			// Token: 0x04000325 RID: 805
			private volatile bool isrunning = true;

			// Token: 0x04000326 RID: 806
			private int sleep_time;
		}
	}
}
