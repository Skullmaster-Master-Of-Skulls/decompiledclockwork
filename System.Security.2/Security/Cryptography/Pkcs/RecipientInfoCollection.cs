using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200007A RID: 122
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class RecipientInfoCollection : ICollection, IEnumerable
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x00017539 File Offset: 0x00015739
		[SecuritySafeCritical]
		internal RecipientInfoCollection()
		{
			this.m_safeCryptMsgHandle = SafeCryptMsgHandle.InvalidHandle;
			this.m_recipientInfos = new ArrayList();
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00017557 File Offset: 0x00015757
		[SecuritySafeCritical]
		internal RecipientInfoCollection(RecipientInfo recipientInfo)
		{
			this.m_safeCryptMsgHandle = SafeCryptMsgHandle.InvalidHandle;
			this.m_recipientInfos = new ArrayList(1);
			this.m_recipientInfos.Add(recipientInfo);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00017584 File Offset: 0x00015784
		[SecurityCritical]
		internal unsafe RecipientInfoCollection(SafeCryptMsgHandle safeCryptMsgHandle)
		{
			bool flag = PkcsUtils.CmsSupported();
			uint num = 0U;
			uint num2 = (uint)Marshal.SizeOf(typeof(uint));
			if (flag)
			{
				if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 33U, 0U, new IntPtr((void*)(&num)), new IntPtr((void*)(&num2))))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			else if (!CAPI.CAPISafe.CryptMsgGetParam(safeCryptMsgHandle, 17U, 0U, new IntPtr((void*)(&num)), new IntPtr((void*)(&num2))))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			this.m_recipientInfos = new ArrayList();
			for (uint num3 = 0U; num3 < num; num3 += 1U)
			{
				if (flag)
				{
					SafeLocalAllocHandle safeLocalAllocHandle;
					uint num4;
					PkcsUtils.GetParam(safeCryptMsgHandle, 36U, num3, out safeLocalAllocHandle, out num4);
					CAPI.CMSG_CMS_RECIPIENT_INFO cmsg_CMS_RECIPIENT_INFO = (CAPI.CMSG_CMS_RECIPIENT_INFO)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPI.CMSG_CMS_RECIPIENT_INFO));
					uint dwRecipientChoice = cmsg_CMS_RECIPIENT_INFO.dwRecipientChoice;
					if (dwRecipientChoice != 1U)
					{
						if (dwRecipientChoice != 2U)
						{
							throw new CryptographicException(-2147483647);
						}
						CAPI.CMSG_KEY_AGREE_RECIPIENT_INFO cmsg_KEY_AGREE_RECIPIENT_INFO = (CAPI.CMSG_KEY_AGREE_RECIPIENT_INFO)Marshal.PtrToStructure(cmsg_CMS_RECIPIENT_INFO.pRecipientInfo, typeof(CAPI.CMSG_KEY_AGREE_RECIPIENT_INFO));
						uint dwOriginatorChoice = cmsg_KEY_AGREE_RECIPIENT_INFO.dwOriginatorChoice;
						if (dwOriginatorChoice != 1U)
						{
							if (dwOriginatorChoice != 2U)
							{
								throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Cms_Invalid_Originator_Identifier_Choice"), cmsg_KEY_AGREE_RECIPIENT_INFO.dwOriginatorChoice.ToString(CultureInfo.CurrentCulture));
							}
							CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO cmsg_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO = (CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO)Marshal.PtrToStructure(cmsg_CMS_RECIPIENT_INFO.pRecipientInfo, typeof(CAPI.CMSG_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO));
							for (uint num5 = 0U; num5 < cmsg_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO.cRecipientEncryptedKeys; num5 += 1U)
							{
								this.m_recipientInfos.Add(new KeyAgreeRecipientInfo(safeLocalAllocHandle, cmsg_KEY_AGREE_PUBLIC_KEY_RECIPIENT_INFO, num3, num5));
							}
						}
						else
						{
							CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO cmsg_KEY_AGREE_CERT_ID_RECIPIENT_INFO = (CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO)Marshal.PtrToStructure(cmsg_CMS_RECIPIENT_INFO.pRecipientInfo, typeof(CAPI.CMSG_KEY_AGREE_CERT_ID_RECIPIENT_INFO));
							for (uint num6 = 0U; num6 < cmsg_KEY_AGREE_CERT_ID_RECIPIENT_INFO.cRecipientEncryptedKeys; num6 += 1U)
							{
								this.m_recipientInfos.Add(new KeyAgreeRecipientInfo(safeLocalAllocHandle, cmsg_KEY_AGREE_CERT_ID_RECIPIENT_INFO, num3, num6));
							}
						}
					}
					else
					{
						CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO keyTrans = (CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO)Marshal.PtrToStructure(cmsg_CMS_RECIPIENT_INFO.pRecipientInfo, typeof(CAPI.CMSG_KEY_TRANS_RECIPIENT_INFO));
						this.m_recipientInfos.Add(new KeyTransRecipientInfo(safeLocalAllocHandle, keyTrans, num3));
					}
				}
				else
				{
					SafeLocalAllocHandle safeLocalAllocHandle2;
					uint num7;
					PkcsUtils.GetParam(safeCryptMsgHandle, 19U, num3, out safeLocalAllocHandle2, out num7);
					CAPI.CERT_INFO certInfo = (CAPI.CERT_INFO)Marshal.PtrToStructure(safeLocalAllocHandle2.DangerousGetHandle(), typeof(CAPI.CERT_INFO));
					this.m_recipientInfos.Add(new KeyTransRecipientInfo(safeLocalAllocHandle2, certInfo, num3));
				}
			}
			this.m_safeCryptMsgHandle = safeCryptMsgHandle;
		}

		// Token: 0x170000F8 RID: 248
		public RecipientInfo this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_recipientInfos.Count)
				{
					throw new ArgumentOutOfRangeException("index", SecurityResources.GetResourceString("ArgumentOutOfRange_Index"));
				}
				return (RecipientInfo)this.m_recipientInfos[index];
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001781A File Offset: 0x00015A1A
		public int Count
		{
			get
			{
				return this.m_recipientInfos.Count;
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00017827 File Offset: 0x00015A27
		public RecipientInfoEnumerator GetEnumerator()
		{
			return new RecipientInfoEnumerator(this);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00017827 File Offset: 0x00015A27
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new RecipientInfoEnumerator(this);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00017830 File Offset: 0x00015A30
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

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000497A File Offset: 0x00002B7A
		public void CopyTo(RecipientInfo[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00004984 File Offset: 0x00002B84
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00004987 File Offset: 0x00002B87
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x040004F2 RID: 1266
		[SecurityCritical]
		private SafeCryptMsgHandle m_safeCryptMsgHandle;

		// Token: 0x040004F3 RID: 1267
		private ArrayList m_recipientInfos;
	}
}
