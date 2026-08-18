using System;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x02000089 RID: 137
	public class EdirEventSource : LdapEventSource
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000468 RID: 1128 RVA: 0x00014F04 File Offset: 0x00013F04
		// (remove) Token: 0x06000469 RID: 1129 RVA: 0x00014F30 File Offset: 0x00013F30
		public event EdirEventSource.EdirEventHandler EdirEvent
		{
			add
			{
				this.edir_event = (EdirEventSource.EdirEventHandler)Delegate.Combine(this.edir_event, value);
				base.ListenerAdded();
			}
			remove
			{
				this.edir_event = (EdirEventSource.EdirEventHandler)Delegate.Remove(this.edir_event, value);
				base.ListenerRemoved();
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00014F5C File Offset: 0x00013F5C
		protected override int GetListeners()
		{
			int result = 0;
			if (this.edir_event != null)
			{
				result = this.edir_event.GetInvocationList().Length;
			}
			return result;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00014F88 File Offset: 0x00013F88
		public EdirEventSource(EdirEventSpecifier[] specifier, LdapConnection conn)
		{
			if (specifier == null || conn == null)
			{
				throw new ArgumentException("Null argument specified");
			}
			this.mRequestOperation = new MonitorEventRequest(specifier);
			this.mConnection = conn;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00014FD0 File Offset: 0x00013FD0
		protected override void StartSearchAndPolling()
		{
			this.mQueue = this.mConnection.ExtendedOperation(this.mRequestOperation, null, null);
			int[] messageIDs = this.mQueue.MessageIDs;
			if (messageIDs.Length != 1)
			{
				throw new LdapException(null, 82, "Unable to Obtain Message Id");
			}
			base.StartEventPolling(this.mQueue, this.mConnection, messageIDs[0]);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001502C File Offset: 0x0001402C
		protected override void StopSearchAndPolling()
		{
			this.mConnection.Abandon(this.mQueue);
			base.StopEventPolling();
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00015050 File Offset: 0x00014050
		protected override bool NotifyEventListeners(LdapMessage sourceMessage, EventClassifiers aClassification, int nType)
		{
			bool result = false;
			if (this.edir_event != null && sourceMessage != null && sourceMessage.Type == 25 && sourceMessage is EdirEventIntermediateResponse)
			{
				this.edir_event(this, new EdirEventArgs(sourceMessage, EventClassifiers.CLASSIFICATION_EDIR_EVENT));
				result = true;
			}
			return result;
		}

		// Token: 0x04000327 RID: 807
		protected EdirEventSource.EdirEventHandler edir_event;

		// Token: 0x04000328 RID: 808
		protected LdapConnection mConnection;

		// Token: 0x04000329 RID: 809
		protected MonitorEventRequest mRequestOperation = null;

		// Token: 0x0400032A RID: 810
		protected LdapResponseQueue mQueue = null;

		// Token: 0x0200008A RID: 138
		// (Invoke) Token: 0x06000470 RID: 1136
		public delegate void EdirEventHandler(object source, EdirEventArgs objEdirEventArgs);
	}
}
