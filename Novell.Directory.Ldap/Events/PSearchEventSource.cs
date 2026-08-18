using System;
using Novell.Directory.Ldap.Controls;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x02000092 RID: 146
	public class PSearchEventSource : LdapEventSource
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000482 RID: 1154 RVA: 0x00015514 File Offset: 0x00014514
		// (remove) Token: 0x06000483 RID: 1155 RVA: 0x00015540 File Offset: 0x00014540
		public event PSearchEventSource.SearchResultEventHandler SearchResultEvent
		{
			add
			{
				this.search_result_event = (PSearchEventSource.SearchResultEventHandler)Delegate.Combine(this.search_result_event, value);
				base.ListenerAdded();
			}
			remove
			{
				this.search_result_event = (PSearchEventSource.SearchResultEventHandler)Delegate.Remove(this.search_result_event, value);
				base.ListenerRemoved();
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000484 RID: 1156 RVA: 0x0001556C File Offset: 0x0001456C
		// (remove) Token: 0x06000485 RID: 1157 RVA: 0x00015598 File Offset: 0x00014598
		public event PSearchEventSource.SearchReferralEventHandler SearchReferralEvent
		{
			add
			{
				this.search_referral_event = (PSearchEventSource.SearchReferralEventHandler)Delegate.Combine(this.search_referral_event, value);
				base.ListenerAdded();
			}
			remove
			{
				this.search_referral_event = (PSearchEventSource.SearchReferralEventHandler)Delegate.Remove(this.search_referral_event, value);
				base.ListenerRemoved();
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000155C4 File Offset: 0x000145C4
		protected override int GetListeners()
		{
			int num = 0;
			if (this.search_result_event != null)
			{
				num = this.search_result_event.GetInvocationList().Length;
			}
			if (this.search_referral_event != null)
			{
				num += this.search_referral_event.GetInvocationList().Length;
			}
			return num;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00015608 File Offset: 0x00014608
		public PSearchEventSource(LdapConnection conn, string searchBase, int scope, string filter, string[] attrs, bool typesOnly, LdapSearchConstraints constraints, LdapEventType eventchangetype, bool changeonly)
		{
			if (conn == null || searchBase == null || filter == null || attrs == null)
			{
				throw new ArgumentException("Null argument specified");
			}
			this.mConnection = conn;
			this.mSearchBase = searchBase;
			this.mScope = scope;
			this.mFilter = filter;
			this.mAttrs = attrs;
			this.mTypesOnly = typesOnly;
			this.mEventChangeType = eventchangetype;
			if (constraints == null)
			{
				this.mSearchConstraints = new LdapSearchConstraints();
			}
			else
			{
				this.mSearchConstraints = constraints;
			}
			LdapPersistSearchControl controls = new LdapPersistSearchControl((int)eventchangetype, changeonly, true, true);
			this.mSearchConstraints.setControls(controls);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001569C File Offset: 0x0001469C
		protected override void StartSearchAndPolling()
		{
			this.mQueue = this.mConnection.Search(this.mSearchBase, this.mScope, this.mFilter, this.mAttrs, this.mTypesOnly, null, this.mSearchConstraints);
			int[] messageIDs = this.mQueue.MessageIDs;
			if (messageIDs.Length != 1)
			{
				throw new LdapException(null, 82, "Unable to Obtain Message Id");
			}
			base.StartEventPolling(this.mQueue, this.mConnection, messageIDs[0]);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00015714 File Offset: 0x00014714
		protected override void StopSearchAndPolling()
		{
			this.mConnection.Abandon(this.mQueue);
			base.StopEventPolling();
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00015738 File Offset: 0x00014738
		protected override bool NotifyEventListeners(LdapMessage sourceMessage, EventClassifiers aClassification, int nType)
		{
			bool flag = false;
			bool result;
			if (sourceMessage == null)
			{
				result = flag;
			}
			else
			{
				int i = sourceMessage.Type;
				switch (i)
				{
				case 4:
					if (this.search_result_event != null)
					{
						LdapEventType aType = LdapEventType.TYPE_UNKNOWN;
						LdapControl[] controls = sourceMessage.Controls;
						foreach (LdapControl ldapControl in controls)
						{
							if (ldapControl is LdapEntryChangeControl)
							{
								aType = (LdapEventType)((LdapEntryChangeControl)ldapControl).ChangeType;
							}
						}
						this.search_result_event(this, new SearchResultEventArgs(sourceMessage, aClassification, aType));
						flag = true;
					}
					break;
				case 5:
					base.NotifyDirectoryListeners(new LdapEventArgs(sourceMessage, EventClassifiers.CLASSIFICATION_LDAP_PSEARCH, LdapEventType.LDAP_PSEARCH_ANY));
					flag = true;
					break;
				default:
					if (i == 19)
					{
						if (this.search_referral_event != null)
						{
							this.search_referral_event(this, new SearchReferralEventArgs(sourceMessage, aClassification, (LdapEventType)nType));
							flag = true;
						}
					}
					break;
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x0400033C RID: 828
		protected PSearchEventSource.SearchResultEventHandler search_result_event;

		// Token: 0x0400033D RID: 829
		protected PSearchEventSource.SearchReferralEventHandler search_referral_event;

		// Token: 0x0400033E RID: 830
		protected LdapConnection mConnection;

		// Token: 0x0400033F RID: 831
		protected string mSearchBase;

		// Token: 0x04000340 RID: 832
		protected int mScope;

		// Token: 0x04000341 RID: 833
		protected string[] mAttrs;

		// Token: 0x04000342 RID: 834
		protected string mFilter;

		// Token: 0x04000343 RID: 835
		protected bool mTypesOnly;

		// Token: 0x04000344 RID: 836
		protected LdapSearchConstraints mSearchConstraints;

		// Token: 0x04000345 RID: 837
		protected LdapEventType mEventChangeType;

		// Token: 0x04000346 RID: 838
		protected LdapSearchQueue mQueue;

		// Token: 0x02000093 RID: 147
		// (Invoke) Token: 0x0600048C RID: 1164
		public delegate void SearchResultEventHandler(object source, SearchResultEventArgs objArgs);

		// Token: 0x02000094 RID: 148
		// (Invoke) Token: 0x06000490 RID: 1168
		public delegate void SearchReferralEventHandler(object source, SearchReferralEventArgs objArgs);
	}
}
