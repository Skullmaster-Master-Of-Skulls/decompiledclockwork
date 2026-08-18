using System;
using System.Collections;

namespace MailBee.Security
{
	// Token: 0x020000FE RID: 254
	public class CertificateCollection : CollectionBase
	{
		// Token: 0x170002AF RID: 687
		public Certificate this[int index]
		{
			get
			{
				return (Certificate)base.List[index];
			}
		}

		// Token: 0x170002B0 RID: 688
		public Certificate this[string issuedTo]
		{
			get
			{
				foreach (object obj in base.List)
				{
					Certificate certificate = (Certificate)obj;
					if (string.Compare(certificate.IssuedTo, issuedTo, true) == 0)
					{
						return certificate;
					}
				}
				return null;
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0002779C File Offset: 0x0002679C
		public void Add(Certificate cert)
		{
			if (cert == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Add(cert);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000277B6 File Offset: 0x000267B6
		public bool Remove(Certificate cert)
		{
			if (cert == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (this.Contains(cert))
			{
				base.List.Remove(cert);
				return true;
			}
			return false;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000277DC File Offset: 0x000267DC
		public bool Remove(string issuedTo)
		{
			if (issuedTo == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			bool result = false;
			for (int i = 0; i < base.List.Count; i++)
			{
				if (string.Compare(((Certificate)base.List[i]).IssuedTo, issuedTo, true) == 0)
				{
					base.List.RemoveAt(i);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0002783C File Offset: 0x0002683C
		public bool Contains(Certificate cert)
		{
			using (IEnumerator enumerator = base.List.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((Certificate)enumerator.Current == cert)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
