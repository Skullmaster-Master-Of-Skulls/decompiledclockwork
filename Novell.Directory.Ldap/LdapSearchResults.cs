using System;
using System.Collections;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000047 RID: 71
	public class LdapSearchResults
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000E30C File Offset: 0x0000D30C
		public virtual int Count
		{
			get
			{
				int count = this.queue.MessageAgent.Count;
				return this.entryCount - this.entryIndex + this.referenceCount - this.referenceIndex + count;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000E34C File Offset: 0x0000D34C
		public virtual LdapControl[] ResponseControls
		{
			get
			{
				return this.controls;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000E364 File Offset: 0x0000D364
		private bool BatchOfResults
		{
			get
			{
				int i = 0;
				while (i < this.batchSize)
				{
					try
					{
						LdapMessage response;
						if ((response = this.queue.getResponse()) == null)
						{
							LdapException value = new LdapException(null, 85, null);
							this.entries.Add(value);
							break;
						}
						LdapControl[] array = response.Controls;
						if (array != null)
						{
							this.controls = array;
						}
						if (response is LdapSearchResult)
						{
							object entry = ((LdapSearchResult)response).Entry;
							this.entries.Add(entry);
							i++;
							this.entryCount++;
						}
						else if (response is LdapSearchResultReference)
						{
							string[] referrals = ((LdapSearchResultReference)response).Referrals;
							if (!this.cons.ReferralFollowing)
							{
								this.references.Add(referrals);
								this.referenceCount++;
							}
						}
						else
						{
							LdapResponse ldapResponse = (LdapResponse)response;
							int num = ldapResponse.ResultCode;
							if (ldapResponse.hasException())
							{
								num = 91;
							}
							if (num != 10 || !this.cons.ReferralFollowing)
							{
								if (num != 0)
								{
									this.entries.Add(ldapResponse);
									this.entryCount++;
								}
							}
							int[] messageIDs = this.queue.MessageIDs;
							if (messageIDs.Length == 0)
							{
								return true;
							}
						}
					}
					catch (LdapException value2)
					{
						this.entries.Add(value2);
					}
				}
				return false;
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000E4E4 File Offset: 0x0000D4E4
		internal LdapSearchResults(LdapConnection conn, LdapSearchQueue queue, LdapSearchConstraints cons)
		{
			this.conn = conn;
			this.cons = cons;
			int num = cons.BatchSize;
			int num2 = (num == 0) ? 64 : 0;
			this.entries = new ArrayList((num == 0) ? 64 : num);
			this.entryCount = 0;
			this.entryIndex = 0;
			this.references = new ArrayList(5);
			this.referenceCount = 0;
			this.referenceIndex = 0;
			this.queue = queue;
			this.batchSize = ((num == 0) ? int.MaxValue : num);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000E580 File Offset: 0x0000D580
		public virtual bool hasMore()
		{
			bool result = false;
			if (this.entryIndex < this.entryCount || this.referenceIndex < this.referenceCount)
			{
				result = true;
			}
			else if (!this.completed)
			{
				this.resetVectors();
				result = (this.entryIndex < this.entryCount || this.referenceIndex < this.referenceCount);
			}
			return result;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000E5E4 File Offset: 0x0000D5E4
		private void resetVectors()
		{
			if (!this.completed)
			{
				if (this.referenceIndex != 0 && this.referenceIndex >= this.referenceCount)
				{
					SupportClass.SetSize(this.references, 0);
					this.referenceCount = 0;
					this.referenceIndex = 0;
				}
				if (this.entryIndex != 0 && this.entryIndex >= this.entryCount)
				{
					SupportClass.SetSize(this.entries, 0);
					this.entryCount = 0;
					this.entryIndex = 0;
				}
				if (this.referenceIndex == 0 && this.referenceCount == 0 && this.entryIndex == 0 && this.entryCount == 0)
				{
					this.completed = this.BatchOfResults;
				}
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000E68C File Offset: 0x0000D68C
		public virtual LdapEntry next()
		{
			if (this.completed && this.entryIndex >= this.entryCount && this.referenceIndex >= this.referenceCount)
			{
				throw new ArgumentOutOfRangeException("LdapSearchResults.next() no more results");
			}
			this.resetVectors();
			if (this.referenceIndex < this.referenceCount)
			{
				string[] referrals = (string[])this.references[this.referenceIndex++];
				LdapReferralException ex = new LdapReferralException("REFERENCE_NOFOLLOW");
				ex.setReferrals(referrals);
				throw ex;
			}
			if (this.entryIndex < this.entryCount)
			{
				object obj = this.entries[this.entryIndex++];
				if (obj is LdapResponse)
				{
					if (((LdapResponse)obj).hasException())
					{
						LdapResponse ldapResponse = (LdapResponse)obj;
						ReferralInfo activeReferral = ldapResponse.ActiveReferral;
						if (activeReferral != null)
						{
							LdapReferralException ex2 = new LdapReferralException("REFERENCE_ERROR", ldapResponse.Exception);
							ex2.setReferrals(activeReferral.ReferralList);
							ex2.FailedReferral = activeReferral.ReferralUrl.ToString();
							throw ex2;
						}
					}
					((LdapResponse)obj).chkResultCode();
				}
				else if (obj is LdapException)
				{
					throw (LdapException)obj;
				}
				return (LdapEntry)obj;
			}
			throw new LdapException("REFERRAL_LOCAL", new object[]
			{
				"next"
			}, 82, null);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000E7F0 File Offset: 0x0000D7F0
		internal virtual void Abandon()
		{
			this.queue.MessageAgent.AbandonAll();
			this.resetVectors();
			this.completed = true;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000E81C File Offset: 0x0000D81C
		static LdapSearchResults()
		{
			LdapSearchResults.nameLock = new object();
		}

		// Token: 0x0400014F RID: 335
		private ArrayList entries;

		// Token: 0x04000150 RID: 336
		private int entryCount;

		// Token: 0x04000151 RID: 337
		private int entryIndex;

		// Token: 0x04000152 RID: 338
		private ArrayList references;

		// Token: 0x04000153 RID: 339
		private int referenceCount;

		// Token: 0x04000154 RID: 340
		private int referenceIndex;

		// Token: 0x04000155 RID: 341
		private int batchSize;

		// Token: 0x04000156 RID: 342
		private bool completed = false;

		// Token: 0x04000157 RID: 343
		private LdapControl[] controls = null;

		// Token: 0x04000158 RID: 344
		private LdapSearchQueue queue;

		// Token: 0x04000159 RID: 345
		private static object nameLock;

		// Token: 0x0400015A RID: 346
		private static int resultsNum = 0;

		// Token: 0x0400015B RID: 347
		private string name;

		// Token: 0x0400015C RID: 348
		private LdapConnection conn;

		// Token: 0x0400015D RID: 349
		private LdapSearchConstraints cons;

		// Token: 0x0400015E RID: 350
		private ArrayList referralConn = null;
	}
}
