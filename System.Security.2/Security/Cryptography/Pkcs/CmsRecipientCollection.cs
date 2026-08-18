using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200006C RID: 108
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CmsRecipientCollection : ICollection, IEnumerable
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x00016612 File Offset: 0x00014812
		public CmsRecipientCollection()
		{
			this.m_recipients = new ArrayList();
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00016625 File Offset: 0x00014825
		public CmsRecipientCollection(CmsRecipient recipient)
		{
			this.m_recipients = new ArrayList(1);
			this.m_recipients.Add(recipient);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00016648 File Offset: 0x00014848
		public CmsRecipientCollection(SubjectIdentifierType recipientIdentifierType, X509Certificate2Collection certificates)
		{
			this.m_recipients = new ArrayList(certificates.Count);
			for (int i = 0; i < certificates.Count; i++)
			{
				this.m_recipients.Add(new CmsRecipient(recipientIdentifierType, certificates[i]));
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00016698 File Offset: 0x00014898
		private CmsRecipientCollection(CmsRecipientCollection other)
		{
			this.m_recipients = new ArrayList(other.m_recipients.Count);
			foreach (object obj in other.m_recipients)
			{
				CmsRecipient cmsRecipient = (CmsRecipient)obj;
				this.m_recipients.Add(new CmsRecipient(cmsRecipient.RecipientIdentifierType, new X509Certificate2(cmsRecipient.Certificate)));
			}
		}

		// Token: 0x170000CF RID: 207
		public CmsRecipient this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_recipients.Count)
				{
					throw new ArgumentOutOfRangeException("index", SecurityResources.GetResourceString("ArgumentOutOfRange_Index"));
				}
				return (CmsRecipient)this.m_recipients[index];
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00016762 File Offset: 0x00014962
		public int Count
		{
			get
			{
				return this.m_recipients.Count;
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001676F File Offset: 0x0001496F
		public int Add(CmsRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			return this.m_recipients.Add(recipient);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001678B File Offset: 0x0001498B
		public void Remove(CmsRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			this.m_recipients.Remove(recipient);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000167A7 File Offset: 0x000149A7
		public CmsRecipientEnumerator GetEnumerator()
		{
			return new CmsRecipientEnumerator(this);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000167A7 File Offset: 0x000149A7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new CmsRecipientEnumerator(this);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000167B0 File Offset: 0x000149B0
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", SecurityResources.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (index + this.Count > array.Length)
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Argument_InvalidOffLen"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000497A File Offset: 0x00002B7A
		public void CopyTo(CmsRecipient[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00004984 File Offset: 0x00002B84
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00004987 File Offset: 0x00002B87
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001684A File Offset: 0x00014A4A
		internal CmsRecipientCollection DeepCopy()
		{
			return new CmsRecipientCollection(this);
		}

		// Token: 0x040004BE RID: 1214
		private ArrayList m_recipients;
	}
}
