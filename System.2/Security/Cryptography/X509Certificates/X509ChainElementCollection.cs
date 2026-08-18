using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200046F RID: 1135
	public sealed class X509ChainElementCollection : ICollection, IEnumerable
	{
		// Token: 0x06002A43 RID: 10819 RVA: 0x000C119A File Offset: 0x000BF39A
		internal X509ChainElementCollection()
		{
			this.m_elements = new X509ChainElement[0];
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000C11B0 File Offset: 0x000BF3B0
		internal unsafe X509ChainElementCollection(IntPtr pSimpleChain)
		{
			CAPIBase.CERT_SIMPLE_CHAIN cert_SIMPLE_CHAIN = new CAPIBase.CERT_SIMPLE_CHAIN(Marshal.SizeOf(typeof(CAPIBase.CERT_SIMPLE_CHAIN)));
			uint num = (uint)Marshal.ReadInt32(pSimpleChain);
			if ((ulong)num > (ulong)((long)Marshal.SizeOf(cert_SIMPLE_CHAIN)))
			{
				num = (uint)Marshal.SizeOf(cert_SIMPLE_CHAIN);
			}
			X509Utils.memcpy(pSimpleChain, new IntPtr((void*)(&cert_SIMPLE_CHAIN)), num);
			this.m_elements = new X509ChainElement[cert_SIMPLE_CHAIN.cElement];
			for (int i = 0; i < this.m_elements.Length; i++)
			{
				this.m_elements[i] = new X509ChainElement(Marshal.ReadIntPtr(new IntPtr((long)cert_SIMPLE_CHAIN.rgpElement + (long)(i * Marshal.SizeOf(typeof(IntPtr))))));
			}
		}

		// Token: 0x17000A42 RID: 2626
		public X509ChainElement this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumNotStarted"));
				}
				if (index >= this.m_elements.Length)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("ArgumentOutOfRange_Index"));
				}
				return this.m_elements[index];
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x000C12A2 File Offset: 0x000BF4A2
		public int Count
		{
			get
			{
				return this.m_elements.Length;
			}
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000C12AC File Offset: 0x000BF4AC
		public X509ChainElementEnumerator GetEnumerator()
		{
			return new X509ChainElementEnumerator(this);
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000C12B4 File Offset: 0x000BF4B4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new X509ChainElementEnumerator(this);
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000C12BC File Offset: 0x000BF4BC
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

		// Token: 0x06002A4A RID: 10826 RVA: 0x000C1356 File Offset: 0x000BF556
		public void CopyTo(X509ChainElement[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x000C1360 File Offset: 0x000BF560
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06002A4C RID: 10828 RVA: 0x000C1363 File Offset: 0x000BF563
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0400260B RID: 9739
		private X509ChainElement[] m_elements;
	}
}
