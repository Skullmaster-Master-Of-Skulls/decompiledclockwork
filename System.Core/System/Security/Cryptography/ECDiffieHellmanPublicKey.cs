using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x020000FA RID: 250
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public abstract class ECDiffieHellmanPublicKey : IDisposable
	{
		// Token: 0x06000812 RID: 2066 RVA: 0x0001B618 File Offset: 0x00019818
		protected ECDiffieHellmanPublicKey()
		{
			this.m_keyBlob = new byte[0];
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001B62C File Offset: 0x0001982C
		protected ECDiffieHellmanPublicKey(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			this.m_keyBlob = (keyBlob.Clone() as byte[]);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001B653 File Offset: 0x00019853
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001B65C File Offset: 0x0001985C
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001B65E File Offset: 0x0001985E
		public virtual byte[] ToByteArray()
		{
			return this.m_keyBlob.Clone() as byte[];
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001B670 File Offset: 0x00019870
		public virtual string ToXmlString()
		{
			throw new NotImplementedException(SR.GetString("NotSupported_SubclassOverride"));
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001B681 File Offset: 0x00019881
		public virtual ECParameters ExportParameters()
		{
			throw new NotSupportedException(SR.GetString("NotSupported_SubclassOverride"));
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001B692 File Offset: 0x00019892
		public virtual ECParameters ExportExplicitParameters()
		{
			throw new NotSupportedException(SR.GetString("NotSupported_SubclassOverride"));
		}

		// Token: 0x04000665 RID: 1637
		private byte[] m_keyBlob;
	}
}
