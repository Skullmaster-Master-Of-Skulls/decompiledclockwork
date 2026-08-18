using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200047C RID: 1148
	public sealed class X509ExtensionCollection : ICollection, IEnumerable
	{
		// Token: 0x06002A8F RID: 10895 RVA: 0x000C2120 File Offset: 0x000C0320
		public X509ExtensionCollection()
		{
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000C2134 File Offset: 0x000C0334
		internal unsafe X509ExtensionCollection(SafeCertContextHandle safeCertContextHandle)
		{
			using (SafeCertContextHandle safeCertContextHandle2 = CAPI.CertDuplicateCertificateContext(safeCertContextHandle))
			{
				CAPIBase.CERT_CONTEXT cert_CONTEXT = *(CAPIBase.CERT_CONTEXT*)((void*)safeCertContextHandle2.DangerousGetHandle());
				CAPIBase.CERT_INFO cert_INFO = (CAPIBase.CERT_INFO)Marshal.PtrToStructure(cert_CONTEXT.pCertInfo, typeof(CAPIBase.CERT_INFO));
				uint cExtension = cert_INFO.cExtension;
				IntPtr rgExtension = cert_INFO.rgExtension;
				for (uint num = 0U; num < cExtension; num += 1U)
				{
					X509Extension x509Extension = new X509Extension(new IntPtr((long)rgExtension + (long)((ulong)num * (ulong)((long)Marshal.SizeOf(typeof(CAPIBase.CERT_EXTENSION))))));
					X509Extension x509Extension2 = CryptoConfig.CreateFromName(x509Extension.Oid.Value) as X509Extension;
					if (x509Extension2 != null)
					{
						x509Extension2.CopyFrom(x509Extension);
						x509Extension = x509Extension2;
					}
					this.Add(x509Extension);
				}
			}
		}

		// Token: 0x17000A57 RID: 2647
		public X509Extension this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumNotStarted"));
				}
				if (index >= this.m_list.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("ArgumentOutOfRange_Index"));
				}
				return (X509Extension)this.m_list[index];
			}
		}

		// Token: 0x17000A58 RID: 2648
		public X509Extension this[string oid]
		{
			get
			{
				string text = X509Utils.FindOidInfoWithFallback(2U, oid, OidGroup.ExtensionOrAttribute);
				if (text == null)
				{
					text = oid;
				}
				foreach (object obj in this.m_list)
				{
					X509Extension x509Extension = (X509Extension)obj;
					if (string.Compare(x509Extension.Oid.Value, text, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return x509Extension;
					}
				}
				return null;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06002A93 RID: 10899 RVA: 0x000C22F4 File Offset: 0x000C04F4
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000C2301 File Offset: 0x000C0501
		public int Add(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			return this.m_list.Add(extension);
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000C231D File Offset: 0x000C051D
		public X509ExtensionEnumerator GetEnumerator()
		{
			return new X509ExtensionEnumerator(this);
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000C2325 File Offset: 0x000C0525
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new X509ExtensionEnumerator(this);
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x000C2330 File Offset: 0x000C0530
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.GetString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("ArgumentOutOfRange_Index"));
			}
			if (index + this.Count > array.Length)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x000C23CA File Offset: 0x000C05CA
		public void CopyTo(X509Extension[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x000C23D4 File Offset: 0x000C05D4
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x000C23D7 File Offset: 0x000C05D7
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04002647 RID: 9799
		private ArrayList m_list = new ArrayList();
	}
}
