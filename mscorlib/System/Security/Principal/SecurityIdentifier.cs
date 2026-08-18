using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Principal
{
	// Token: 0x02000946 RID: 2374
	[ComVisible(false)]
	public sealed class SecurityIdentifier : IdentityReference, IComparable<SecurityIdentifier>
	{
		// Token: 0x06005595 RID: 21909 RVA: 0x00136518 File Offset: 0x00135518
		private void CreateFromParts(IdentifierAuthority identifierAuthority, int[] subAuthorities)
		{
			if (subAuthorities == null)
			{
				throw new ArgumentNullException("subAuthorities");
			}
			if (subAuthorities.Length > (int)SecurityIdentifier.MaxSubAuthorities)
			{
				throw new ArgumentOutOfRangeException("subAuthorities.Length", subAuthorities.Length, Environment.GetResourceString("IdentityReference_InvalidNumberOfSubauthorities", new object[]
				{
					SecurityIdentifier.MaxSubAuthorities
				}));
			}
			if (identifierAuthority < IdentifierAuthority.NullAuthority || identifierAuthority > (IdentifierAuthority)SecurityIdentifier.MaxIdentifierAuthority)
			{
				throw new ArgumentOutOfRangeException("identifierAuthority", identifierAuthority, Environment.GetResourceString("IdentityReference_IdentifierAuthorityTooLarge"));
			}
			this._IdentifierAuthority = identifierAuthority;
			this._SubAuthorities = new int[subAuthorities.Length];
			subAuthorities.CopyTo(this._SubAuthorities, 0);
			this._BinaryForm = new byte[8 + 4 * this.SubAuthorityCount];
			this._BinaryForm[0] = SecurityIdentifier.Revision;
			this._BinaryForm[1] = (byte)this.SubAuthorityCount;
			byte b;
			for (b = 0; b < 6; b += 1)
			{
				this._BinaryForm[(int)(2 + b)] = (byte)(this._IdentifierAuthority >> (int)((5 - b) * 8 & 63) & (IdentifierAuthority)255L);
			}
			b = 0;
			while ((int)b < this.SubAuthorityCount)
			{
				for (byte b2 = 0; b2 < 4; b2 += 1)
				{
					this._BinaryForm[(int)(8 + 4 * b + b2)] = (byte)((ulong)((long)this._SubAuthorities[(int)b]) >> (int)(b2 * 8));
				}
				b += 1;
			}
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x00136658 File Offset: 0x00135658
		private void CreateFromBinaryForm(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", offset, Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (binaryForm.Length - offset < SecurityIdentifier.MinBinaryLength)
			{
				throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
			}
			if (binaryForm[offset] != SecurityIdentifier.Revision)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_InvalidSidRevision"), "binaryForm");
			}
			if (binaryForm[offset + 1] > SecurityIdentifier.MaxSubAuthorities)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_InvalidNumberOfSubauthorities", new object[]
				{
					SecurityIdentifier.MaxSubAuthorities
				}), "binaryForm");
			}
			int num = (int)(8 + 4 * binaryForm[offset + 1]);
			if (binaryForm.Length - offset < num)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"), "binaryForm");
			}
			IdentifierAuthority identifierAuthority = (IdentifierAuthority)(((ulong)binaryForm[offset + 2] << 40) + ((ulong)binaryForm[offset + 3] << 32) + ((ulong)binaryForm[offset + 4] << 24) + ((ulong)binaryForm[offset + 5] << 16) + ((ulong)binaryForm[offset + 6] << 8) + (ulong)binaryForm[offset + 7]);
			int[] array = new int[(int)binaryForm[offset + 1]];
			for (byte b = 0; b < binaryForm[offset + 1]; b += 1)
			{
				array[(int)b] = (int)binaryForm[offset + 8 + (int)(4 * b)] + ((int)binaryForm[offset + 8 + (int)(4 * b) + 1] << 8) + ((int)binaryForm[offset + 8 + (int)(4 * b) + 2] << 16) + ((int)binaryForm[offset + 8 + (int)(4 * b) + 3] << 24);
			}
			this.CreateFromParts(identifierAuthority, array);
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x001367C8 File Offset: 0x001357C8
		public SecurityIdentifier(string sddlForm)
		{
			if (sddlForm == null)
			{
				throw new ArgumentNullException("sddlForm");
			}
			byte[] binaryForm;
			int num = Win32.CreateSidFromString(sddlForm, out binaryForm);
			if (num == 1337)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"), "sddlForm");
			}
			if (num == 8)
			{
				throw new OutOfMemoryException();
			}
			if (num != 0)
			{
				throw new SystemException(Win32Native.GetMessage(num));
			}
			this.CreateFromBinaryForm(binaryForm, 0);
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x00136830 File Offset: 0x00135830
		public SecurityIdentifier(byte[] binaryForm, int offset)
		{
			this.CreateFromBinaryForm(binaryForm, offset);
		}

		// Token: 0x06005599 RID: 21913 RVA: 0x00136840 File Offset: 0x00135840
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public SecurityIdentifier(IntPtr binaryForm) : this(binaryForm, true)
		{
		}

		// Token: 0x0600559A RID: 21914 RVA: 0x0013684A File Offset: 0x0013584A
		internal SecurityIdentifier(IntPtr binaryForm, bool noDemand) : this(Win32.ConvertIntPtrSidToByteArraySid(binaryForm), 0)
		{
		}

		// Token: 0x0600559B RID: 21915 RVA: 0x0013685C File Offset: 0x0013585C
		public SecurityIdentifier(WellKnownSidType sidType, SecurityIdentifier domainSid)
		{
			if (!Win32.WellKnownSidApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
			}
			if (sidType == WellKnownSidType.LogonIdsSid)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_CannotCreateLogonIdsSid"), "sidType");
			}
			if (sidType < WellKnownSidType.NullSid || sidType > WellKnownSidType.WinBuiltinTerminalServerLicenseServersSid)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"), "sidType");
			}
			if (sidType >= WellKnownSidType.AccountAdministratorSid && sidType <= WellKnownSidType.AccountRasAndIasServersSid)
			{
				if (domainSid == null)
				{
					throw new ArgumentNullException("domainSid", Environment.GetResourceString("IdentityReference_DomainSidRequired", new object[]
					{
						sidType
					}));
				}
				SecurityIdentifier left;
				int windowsAccountDomainSid = Win32.GetWindowsAccountDomainSid(domainSid, out left);
				if (windowsAccountDomainSid == 122)
				{
					throw new OutOfMemoryException();
				}
				if (windowsAccountDomainSid == 1257)
				{
					throw new ArgumentException(Environment.GetResourceString("IdentityReference_NotAWindowsDomain"), "domainSid");
				}
				if (windowsAccountDomainSid != 0)
				{
					throw new SystemException(Win32Native.GetMessage(windowsAccountDomainSid));
				}
				if (left != domainSid)
				{
					throw new ArgumentException(Environment.GetResourceString("IdentityReference_NotAWindowsDomain"), "domainSid");
				}
			}
			byte[] binaryForm;
			int num = Win32.CreateWellKnownSid(sidType, domainSid, out binaryForm);
			if (num == 87)
			{
				throw new ArgumentException(Win32Native.GetMessage(num), "sidType/domainSid");
			}
			if (num != 0)
			{
				throw new SystemException(Win32Native.GetMessage(num));
			}
			this.CreateFromBinaryForm(binaryForm, 0);
		}

		// Token: 0x0600559C RID: 21916 RVA: 0x00136998 File Offset: 0x00135998
		internal SecurityIdentifier(SecurityIdentifier domainSid, uint rid)
		{
			int[] array = new int[domainSid.SubAuthorityCount + 1];
			int i;
			for (i = 0; i < domainSid.SubAuthorityCount; i++)
			{
				array[i] = domainSid.GetSubAuthority(i);
			}
			array[i] = (int)rid;
			this.CreateFromParts(domainSid.IdentifierAuthority, array);
		}

		// Token: 0x0600559D RID: 21917 RVA: 0x001369E5 File Offset: 0x001359E5
		internal SecurityIdentifier(IdentifierAuthority identifierAuthority, int[] subAuthorities)
		{
			this.CreateFromParts(identifierAuthority, subAuthorities);
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x0600559E RID: 21918 RVA: 0x001369F5 File Offset: 0x001359F5
		internal static byte Revision
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x0600559F RID: 21919 RVA: 0x001369F8 File Offset: 0x001359F8
		internal byte[] BinaryForm
		{
			get
			{
				return this._BinaryForm;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x060055A0 RID: 21920 RVA: 0x00136A00 File Offset: 0x00135A00
		internal IdentifierAuthority IdentifierAuthority
		{
			get
			{
				return this._IdentifierAuthority;
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x060055A1 RID: 21921 RVA: 0x00136A08 File Offset: 0x00135A08
		internal int SubAuthorityCount
		{
			get
			{
				return this._SubAuthorities.Length;
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x060055A2 RID: 21922 RVA: 0x00136A12 File Offset: 0x00135A12
		public int BinaryLength
		{
			get
			{
				return this._BinaryForm.Length;
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x060055A3 RID: 21923 RVA: 0x00136A1C File Offset: 0x00135A1C
		public SecurityIdentifier AccountDomainSid
		{
			get
			{
				if (!this._AccountDomainSidInitialized)
				{
					this._AccountDomainSid = this.GetAccountDomainSid();
					this._AccountDomainSidInitialized = true;
				}
				return this._AccountDomainSid;
			}
		}

		// Token: 0x060055A4 RID: 21924 RVA: 0x00136A40 File Offset: 0x00135A40
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			SecurityIdentifier securityIdentifier = o as SecurityIdentifier;
			return !(securityIdentifier == null) && this == securityIdentifier;
		}

		// Token: 0x060055A5 RID: 21925 RVA: 0x00136A6B File Offset: 0x00135A6B
		public bool Equals(SecurityIdentifier sid)
		{
			return !(sid == null) && this == sid;
		}

		// Token: 0x060055A6 RID: 21926 RVA: 0x00136A80 File Offset: 0x00135A80
		public override int GetHashCode()
		{
			int num = ((long)this.IdentifierAuthority).GetHashCode();
			for (int i = 0; i < this.SubAuthorityCount; i++)
			{
				num ^= this.GetSubAuthority(i);
			}
			return num;
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x00136AB8 File Offset: 0x00135AB8
		public override string ToString()
		{
			if (this._SddlForm == null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("S-1-{0}", (long)this._IdentifierAuthority);
				for (int i = 0; i < this.SubAuthorityCount; i++)
				{
					stringBuilder.AppendFormat("-{0}", (uint)this._SubAuthorities[i]);
				}
				this._SddlForm = stringBuilder.ToString();
			}
			return this._SddlForm;
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x060055A8 RID: 21928 RVA: 0x00136B26 File Offset: 0x00135B26
		public override string Value
		{
			get
			{
				return this.ToString().ToUpper(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x060055A9 RID: 21929 RVA: 0x00136B38 File Offset: 0x00135B38
		internal static bool IsValidTargetTypeStatic(Type targetType)
		{
			return targetType == typeof(NTAccount) || targetType == typeof(SecurityIdentifier);
		}

		// Token: 0x060055AA RID: 21930 RVA: 0x00136B59 File Offset: 0x00135B59
		public override bool IsValidTargetType(Type targetType)
		{
			return SecurityIdentifier.IsValidTargetTypeStatic(targetType);
		}

		// Token: 0x060055AB RID: 21931 RVA: 0x00136B64 File Offset: 0x00135B64
		internal SecurityIdentifier GetAccountDomainSid()
		{
			SecurityIdentifier result;
			int windowsAccountDomainSid = Win32.GetWindowsAccountDomainSid(this, out result);
			if (windowsAccountDomainSid == 122)
			{
				throw new OutOfMemoryException();
			}
			if (windowsAccountDomainSid == 1257)
			{
				result = null;
			}
			else if (windowsAccountDomainSid != 0)
			{
				throw new SystemException(Win32Native.GetMessage(windowsAccountDomainSid));
			}
			return result;
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x00136BA1 File Offset: 0x00135BA1
		public bool IsAccountSid()
		{
			if (!this._AccountDomainSidInitialized)
			{
				this._AccountDomainSid = this.GetAccountDomainSid();
				this._AccountDomainSidInitialized = true;
			}
			return !(this._AccountDomainSid == null);
		}

		// Token: 0x060055AD RID: 21933 RVA: 0x00136BD0 File Offset: 0x00135BD0
		public override IdentityReference Translate(Type targetType)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (targetType == typeof(SecurityIdentifier))
			{
				return this;
			}
			if (targetType == typeof(NTAccount))
			{
				IdentityReferenceCollection identityReferenceCollection = SecurityIdentifier.Translate(new IdentityReferenceCollection(1)
				{
					this
				}, targetType, true);
				return identityReferenceCollection[0];
			}
			throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), "targetType");
		}

		// Token: 0x060055AE RID: 21934 RVA: 0x00136C3C File Offset: 0x00135C3C
		public static bool operator ==(SecurityIdentifier left, SecurityIdentifier right)
		{
			return (left == null && right == null) || (left != null && right != null && left.CompareTo(right) == 0);
		}

		// Token: 0x060055AF RID: 21935 RVA: 0x00136C67 File Offset: 0x00135C67
		public static bool operator !=(SecurityIdentifier left, SecurityIdentifier right)
		{
			return !(left == right);
		}

		// Token: 0x060055B0 RID: 21936 RVA: 0x00136C74 File Offset: 0x00135C74
		public int CompareTo(SecurityIdentifier sid)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			if (this.IdentifierAuthority < sid.IdentifierAuthority)
			{
				return -1;
			}
			if (this.IdentifierAuthority > sid.IdentifierAuthority)
			{
				return 1;
			}
			if (this.SubAuthorityCount < sid.SubAuthorityCount)
			{
				return -1;
			}
			if (this.SubAuthorityCount > sid.SubAuthorityCount)
			{
				return 1;
			}
			for (int i = 0; i < this.SubAuthorityCount; i++)
			{
				int num = this.GetSubAuthority(i) - sid.GetSubAuthority(i);
				if (num != 0)
				{
					return num;
				}
			}
			return 0;
		}

		// Token: 0x060055B1 RID: 21937 RVA: 0x00136CFC File Offset: 0x00135CFC
		internal int GetSubAuthority(int index)
		{
			return this._SubAuthorities[index];
		}

		// Token: 0x060055B2 RID: 21938 RVA: 0x00136D06 File Offset: 0x00135D06
		public bool IsWellKnown(WellKnownSidType type)
		{
			return Win32.IsWellKnownSid(this, type);
		}

		// Token: 0x060055B3 RID: 21939 RVA: 0x00136D0F File Offset: 0x00135D0F
		public void GetBinaryForm(byte[] binaryForm, int offset)
		{
			this._BinaryForm.CopyTo(binaryForm, offset);
		}

		// Token: 0x060055B4 RID: 21940 RVA: 0x00136D1E File Offset: 0x00135D1E
		public bool IsEqualDomainSid(SecurityIdentifier sid)
		{
			return Win32.IsEqualDomainSid(this, sid);
		}

		// Token: 0x060055B5 RID: 21941 RVA: 0x00136D28 File Offset: 0x00135D28
		private static IdentityReferenceCollection TranslateToNTAccounts(IdentityReferenceCollection sourceSids, out bool someFailed)
		{
			if (!Win32.LsaApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_Win9x"));
			}
			IntPtr[] array = new IntPtr[sourceSids.Count];
			GCHandle[] array2 = new GCHandle[sourceSids.Count];
			SafeLsaPolicyHandle safeLsaPolicyHandle = SafeLsaPolicyHandle.InvalidHandle;
			SafeLsaMemoryHandle invalidHandle = SafeLsaMemoryHandle.InvalidHandle;
			SafeLsaMemoryHandle invalidHandle2 = SafeLsaMemoryHandle.InvalidHandle;
			int i = 0;
			if (sourceSids == null)
			{
				throw new ArgumentNullException("sourceSids");
			}
			if (sourceSids.Count == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EmptyCollection"), "sourceSids");
			}
			IdentityReferenceCollection result;
			try
			{
				foreach (IdentityReference identityReference in sourceSids)
				{
					SecurityIdentifier securityIdentifier = identityReference as SecurityIdentifier;
					if (securityIdentifier == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_ImproperType"), "sourceSids");
					}
					array2[i] = GCHandle.Alloc(securityIdentifier.BinaryForm, GCHandleType.Pinned);
					array[i] = array2[i].AddrOfPinnedObject();
					i++;
				}
				safeLsaPolicyHandle = Win32.LsaOpenPolicy(null, PolicyRights.POLICY_LOOKUP_NAMES);
				someFailed = false;
				uint num = Win32Native.LsaLookupSids(safeLsaPolicyHandle, sourceSids.Count, array, ref invalidHandle, ref invalidHandle2);
				if (num == 3221225495U || num == 3221225626U)
				{
					throw new OutOfMemoryException();
				}
				if (num == 3221225506U)
				{
					throw new UnauthorizedAccessException();
				}
				if (num == 3221225587U || num == 263U)
				{
					someFailed = true;
				}
				else if (num != 0U)
				{
					int errorCode = Win32Native.LsaNtStatusToWinError((int)num);
					throw new SystemException(Win32Native.GetMessage(errorCode));
				}
				IdentityReferenceCollection identityReferenceCollection = new IdentityReferenceCollection(sourceSids.Count);
				if (num == 0U || num == 263U)
				{
					Win32Native.LSA_REFERENCED_DOMAIN_LIST lsa_REFERENCED_DOMAIN_LIST = (Win32Native.LSA_REFERENCED_DOMAIN_LIST)Marshal.PtrToStructure(invalidHandle.DangerousGetHandle(), typeof(Win32Native.LSA_REFERENCED_DOMAIN_LIST));
					string[] array3 = new string[lsa_REFERENCED_DOMAIN_LIST.Entries];
					for (i = 0; i < lsa_REFERENCED_DOMAIN_LIST.Entries; i++)
					{
						Win32Native.LSA_TRUST_INFORMATION lsa_TRUST_INFORMATION = (Win32Native.LSA_TRUST_INFORMATION)Marshal.PtrToStructure(new IntPtr((long)lsa_REFERENCED_DOMAIN_LIST.Domains + (long)(i * Marshal.SizeOf(typeof(Win32Native.LSA_TRUST_INFORMATION)))), typeof(Win32Native.LSA_TRUST_INFORMATION));
						array3[i] = Marshal.PtrToStringUni(lsa_TRUST_INFORMATION.Name.Buffer, (int)(lsa_TRUST_INFORMATION.Name.Length / 2));
					}
					i = 0;
					while (i < sourceSids.Count)
					{
						Win32Native.LSA_TRANSLATED_NAME lsa_TRANSLATED_NAME = (Win32Native.LSA_TRANSLATED_NAME)Marshal.PtrToStructure(new IntPtr((long)invalidHandle2.DangerousGetHandle() + (long)(i * Marshal.SizeOf(typeof(Win32Native.LSA_TRANSLATED_NAME)))), typeof(Win32Native.LSA_TRANSLATED_NAME));
						switch (lsa_TRANSLATED_NAME.Use)
						{
						case 1:
						case 2:
						case 4:
						case 5:
						case 9:
						{
							string accountName = Marshal.PtrToStringUni(lsa_TRANSLATED_NAME.Name.Buffer, (int)(lsa_TRANSLATED_NAME.Name.Length / 2));
							string domainName = array3[lsa_TRANSLATED_NAME.DomainIndex];
							identityReferenceCollection.Add(new NTAccount(domainName, accountName));
							break;
						}
						case 3:
						case 6:
						case 7:
						case 8:
							goto IL_2F0;
						default:
							goto IL_2F0;
						}
						IL_302:
						i++;
						continue;
						IL_2F0:
						someFailed = true;
						identityReferenceCollection.Add(sourceSids[i]);
						goto IL_302;
					}
				}
				else
				{
					for (i = 0; i < sourceSids.Count; i++)
					{
						identityReferenceCollection.Add(sourceSids[i]);
					}
				}
				result = identityReferenceCollection;
			}
			finally
			{
				for (i = 0; i < sourceSids.Count; i++)
				{
					if (array2[i].IsAllocated)
					{
						array2[i].Free();
					}
				}
				safeLsaPolicyHandle.Dispose();
				invalidHandle.Dispose();
				invalidHandle2.Dispose();
			}
			return result;
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x001370F4 File Offset: 0x001360F4
		internal static IdentityReferenceCollection Translate(IdentityReferenceCollection sourceSids, Type targetType, bool forceSuccess)
		{
			bool flag = false;
			IdentityReferenceCollection identityReferenceCollection = SecurityIdentifier.Translate(sourceSids, targetType, out flag);
			if (forceSuccess && flag)
			{
				IdentityReferenceCollection identityReferenceCollection2 = new IdentityReferenceCollection();
				foreach (IdentityReference identityReference in identityReferenceCollection)
				{
					if (identityReference.GetType() != targetType)
					{
						identityReferenceCollection2.Add(identityReference);
					}
				}
				throw new IdentityNotMappedException(Environment.GetResourceString("IdentityReference_IdentityNotMapped"), identityReferenceCollection2);
			}
			return identityReferenceCollection;
		}

		// Token: 0x060055B7 RID: 21943 RVA: 0x00137174 File Offset: 0x00136174
		internal static IdentityReferenceCollection Translate(IdentityReferenceCollection sourceSids, Type targetType, out bool someFailed)
		{
			if (sourceSids == null)
			{
				throw new ArgumentNullException("sourceSids");
			}
			if (targetType == typeof(NTAccount))
			{
				return SecurityIdentifier.TranslateToNTAccounts(sourceSids, out someFailed);
			}
			throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), "targetType");
		}

		// Token: 0x04002CBC RID: 11452
		internal static readonly long MaxIdentifierAuthority = 281474976710655L;

		// Token: 0x04002CBD RID: 11453
		internal static readonly byte MaxSubAuthorities = 15;

		// Token: 0x04002CBE RID: 11454
		public static readonly int MinBinaryLength = 8;

		// Token: 0x04002CBF RID: 11455
		public static readonly int MaxBinaryLength = (int)(8 + SecurityIdentifier.MaxSubAuthorities * 4);

		// Token: 0x04002CC0 RID: 11456
		private IdentifierAuthority _IdentifierAuthority;

		// Token: 0x04002CC1 RID: 11457
		private int[] _SubAuthorities;

		// Token: 0x04002CC2 RID: 11458
		private byte[] _BinaryForm;

		// Token: 0x04002CC3 RID: 11459
		private SecurityIdentifier _AccountDomainSid;

		// Token: 0x04002CC4 RID: 11460
		private bool _AccountDomainSidInitialized;

		// Token: 0x04002CC5 RID: 11461
		private string _SddlForm;
	}
}
