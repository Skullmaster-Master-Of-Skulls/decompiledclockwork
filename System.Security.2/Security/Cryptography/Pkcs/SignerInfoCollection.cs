using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000087 RID: 135
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SignerInfoCollection : ICollection, IEnumerable
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x0001B71B File Offset: 0x0001991B
		internal SignerInfoCollection()
		{
			this.m_signerInfos = new SignerInfo[0];
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001B730 File Offset: 0x00019930
		[SecuritySafeCritical]
		internal unsafe SignerInfoCollection(SignedCms signedCms)
		{
			uint num = 0U;
			uint num2 = (uint)Marshal.SizeOf(typeof(uint));
			SafeCryptMsgHandle cryptMsgHandle = signedCms.GetCryptMsgHandle();
			if (!CAPI.CAPISafe.CryptMsgGetParam(cryptMsgHandle, 5U, 0U, new IntPtr((void*)(&num)), new IntPtr((void*)(&num2))))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			SignerInfo[] array = new SignerInfo[num];
			int num3 = 0;
			while ((long)num3 < (long)((ulong)num))
			{
				uint num4 = 0U;
				if (!CAPI.CAPISafe.CryptMsgGetParam(cryptMsgHandle, 6U, (uint)num3, IntPtr.Zero, new IntPtr((void*)(&num4))))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(0U, new IntPtr((long)((ulong)num4)));
				if (!CAPI.CAPISafe.CryptMsgGetParam(cryptMsgHandle, 6U, (uint)num3, safeLocalAllocHandle, new IntPtr((void*)(&num4))))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				array[num3] = new SignerInfo(signedCms, safeLocalAllocHandle);
				num3++;
			}
			this.m_signerInfos = array;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001B804 File Offset: 0x00019A04
		[SecuritySafeCritical]
		internal SignerInfoCollection(SignedCms signedCms, SignerInfo signerInfo)
		{
			SignerInfo[] array = new SignerInfo[0];
			int num = 0;
			int num2 = 0;
			foreach (CryptographicAttributeObject cryptographicAttributeObject in signerInfo.UnsignedAttributes)
			{
				if (cryptographicAttributeObject.Oid.Value == "1.2.840.113549.1.9.6")
				{
					num += cryptographicAttributeObject.Values.Count;
				}
			}
			array = new SignerInfo[num];
			foreach (CryptographicAttributeObject cryptographicAttributeObject2 in signerInfo.UnsignedAttributes)
			{
				if (cryptographicAttributeObject2.Oid.Value == "1.2.840.113549.1.9.6")
				{
					for (int i = 0; i < cryptographicAttributeObject2.Values.Count; i++)
					{
						AsnEncodedData asnEncodedData = cryptographicAttributeObject2.Values[i];
						array[num2++] = new SignerInfo(signedCms, signerInfo, asnEncodedData.RawData);
					}
				}
			}
			this.m_signerInfos = array;
		}

		// Token: 0x17000117 RID: 279
		public SignerInfo this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_signerInfos.Length)
				{
					throw new ArgumentOutOfRangeException("index", SecurityResources.GetResourceString("ArgumentOutOfRange_Index"));
				}
				return this.m_signerInfos[index];
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001B91C File Offset: 0x00019B1C
		public int Count
		{
			get
			{
				return this.m_signerInfos.Length;
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001B926 File Offset: 0x00019B26
		public SignerInfoEnumerator GetEnumerator()
		{
			return new SignerInfoEnumerator(this);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001B926 File Offset: 0x00019B26
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SignerInfoEnumerator(this);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001B930 File Offset: 0x00019B30
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

		// Token: 0x06000534 RID: 1332 RVA: 0x0000497A File Offset: 0x00002B7A
		public void CopyTo(SignerInfo[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00004984 File Offset: 0x00002B84
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00004987 File Offset: 0x00002B87
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04000520 RID: 1312
		private SignerInfo[] m_signerInfos;
	}
}
