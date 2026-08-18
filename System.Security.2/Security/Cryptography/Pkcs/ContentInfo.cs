using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000083 RID: 131
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ContentInfo
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x00018341 File Offset: 0x00016541
		private ContentInfo() : this(Oid.FromOidValue("1.2.840.113549.1.7.1", OidGroup.ExtensionOrAttribute), new byte[0])
		{
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001835A File Offset: 0x0001655A
		public ContentInfo(byte[] content) : this(Oid.FromOidValue("1.2.840.113549.1.7.1", OidGroup.ExtensionOrAttribute), content)
		{
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001836E File Offset: 0x0001656E
		public ContentInfo(Oid contentType, byte[] content)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			this.m_contentType = contentType;
			this.m_content = content;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x000183AB File Offset: 0x000165AB
		public Oid ContentType
		{
			get
			{
				return this.m_contentType;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x000183B3 File Offset: 0x000165B3
		public byte[] Content
		{
			get
			{
				return this.m_content;
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000183BC File Offset: 0x000165BC
		[SecuritySafeCritical]
		~ContentInfo()
		{
			if (this.m_gcHandle.IsAllocated)
			{
				this.m_gcHandle.Free();
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x000183FC File Offset: 0x000165FC
		internal IntPtr pContent
		{
			[SecurityCritical]
			get
			{
				if (IntPtr.Zero == this.m_pContent && this.m_content != null && this.m_content.Length != 0)
				{
					this.m_gcHandle = GCHandle.Alloc(this.m_content, GCHandleType.Pinned);
					this.m_pContent = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_content, 0);
				}
				return this.m_pContent;
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00018458 File Offset: 0x00016658
		[SecuritySafeCritical]
		public static Oid GetContentType(byte[] encodedMessage)
		{
			if (encodedMessage == null)
			{
				throw new ArgumentNullException("encodedMessage");
			}
			SafeCryptMsgHandle safeCryptMsgHandle = CAPI.CAPISafe.CryptMsgOpenToDecode(65537U, 0U, 0U, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (safeCryptMsgHandle == null || safeCryptMsgHandle.IsInvalid)
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			if (!CAPI.CAPISafe.CryptMsgUpdate(safeCryptMsgHandle, encodedMessage, (uint)encodedMessage.Length, true))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			Oid result;
			switch (PkcsUtils.GetMessageType(safeCryptMsgHandle))
			{
			case 1U:
				result = Oid.FromOidValue("1.2.840.113549.1.7.1", OidGroup.ExtensionOrAttribute);
				break;
			case 2U:
				result = Oid.FromOidValue("1.2.840.113549.1.7.2", OidGroup.ExtensionOrAttribute);
				break;
			case 3U:
				result = Oid.FromOidValue("1.2.840.113549.1.7.3", OidGroup.ExtensionOrAttribute);
				break;
			case 4U:
				result = Oid.FromOidValue("1.2.840.113549.1.7.4", OidGroup.ExtensionOrAttribute);
				break;
			case 5U:
				result = Oid.FromOidValue("1.2.840.113549.1.7.5", OidGroup.ExtensionOrAttribute);
				break;
			case 6U:
				result = Oid.FromOidValue("1.2.840.113549.1.7.6", OidGroup.ExtensionOrAttribute);
				break;
			default:
				throw new CryptographicException(-2146889724);
			}
			safeCryptMsgHandle.Dispose();
			return result;
		}

		// Token: 0x0400050D RID: 1293
		private Oid m_contentType;

		// Token: 0x0400050E RID: 1294
		private byte[] m_content;

		// Token: 0x0400050F RID: 1295
		private IntPtr m_pContent = IntPtr.Zero;

		// Token: 0x04000510 RID: 1296
		private GCHandle m_gcHandle;
	}
}
