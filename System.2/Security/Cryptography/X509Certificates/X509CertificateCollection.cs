using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000483 RID: 1155
	[Serializable]
	public class X509CertificateCollection : CollectionBase
	{
		// Token: 0x06002AD4 RID: 10964 RVA: 0x000C340F File Offset: 0x000C160F
		public X509CertificateCollection()
		{
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000C3417 File Offset: 0x000C1617
		public X509CertificateCollection(X509CertificateCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000C3426 File Offset: 0x000C1626
		public X509CertificateCollection(X509Certificate[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000A62 RID: 2658
		public X509Certificate this[int index]
		{
			get
			{
				return (X509Certificate)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000C3457 File Offset: 0x000C1657
		public int Add(X509Certificate value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000C3468 File Offset: 0x000C1668
		public void AddRange(X509Certificate[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x000C349C File Offset: 0x000C169C
		public void AddRange(X509CertificateCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Count; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x000C34D8 File Offset: 0x000C16D8
		public bool Contains(X509Certificate value)
		{
			foreach (object obj in base.List)
			{
				X509Certificate x509Certificate = (X509Certificate)obj;
				if (x509Certificate.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x000C353C File Offset: 0x000C173C
		public void CopyTo(X509Certificate[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x000C354B File Offset: 0x000C174B
		public int IndexOf(X509Certificate value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x000C3559 File Offset: 0x000C1759
		public void Insert(int index, X509Certificate value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000C3568 File Offset: 0x000C1768
		public new X509CertificateCollection.X509CertificateEnumerator GetEnumerator()
		{
			return new X509CertificateCollection.X509CertificateEnumerator(this);
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000C3570 File Offset: 0x000C1770
		public void Remove(X509Certificate value)
		{
			base.List.Remove(value);
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000C3580 File Offset: 0x000C1780
		public override int GetHashCode()
		{
			int num = 0;
			foreach (X509Certificate x509Certificate in this)
			{
				num += x509Certificate.GetHashCode();
			}
			return num;
		}

		// Token: 0x0200087B RID: 2171
		public class X509CertificateEnumerator : IEnumerator
		{
			// Token: 0x06004577 RID: 17783 RVA: 0x00121DB9 File Offset: 0x0011FFB9
			public X509CertificateEnumerator(X509CertificateCollection mappings)
			{
				this.temp = mappings;
				this.baseEnumerator = this.temp.GetEnumerator();
			}

			// Token: 0x17000FB7 RID: 4023
			// (get) Token: 0x06004578 RID: 17784 RVA: 0x00121DD9 File Offset: 0x0011FFD9
			public X509Certificate Current
			{
				get
				{
					return (X509Certificate)this.baseEnumerator.Current;
				}
			}

			// Token: 0x17000FB8 RID: 4024
			// (get) Token: 0x06004579 RID: 17785 RVA: 0x00121DEB File Offset: 0x0011FFEB
			object IEnumerator.Current
			{
				get
				{
					return this.baseEnumerator.Current;
				}
			}

			// Token: 0x0600457A RID: 17786 RVA: 0x00121DF8 File Offset: 0x0011FFF8
			public bool MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}

			// Token: 0x0600457B RID: 17787 RVA: 0x00121E05 File Offset: 0x00120005
			bool IEnumerator.MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}

			// Token: 0x0600457C RID: 17788 RVA: 0x00121E12 File Offset: 0x00120012
			public void Reset()
			{
				this.baseEnumerator.Reset();
			}

			// Token: 0x0600457D RID: 17789 RVA: 0x00121E1F File Offset: 0x0012001F
			void IEnumerator.Reset()
			{
				this.baseEnumerator.Reset();
			}

			// Token: 0x0400372E RID: 14126
			private IEnumerator baseEnumerator;

			// Token: 0x0400372F RID: 14127
			private IEnumerable temp;
		}
	}
}
