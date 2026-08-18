using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Principal
{
	// Token: 0x02000942 RID: 2370
	[ComVisible(false)]
	public sealed class NTAccount : IdentityReference
	{
		// Token: 0x06005588 RID: 21896 RVA: 0x00135DBC File Offset: 0x00134DBC
		public NTAccount(string domainName, string accountName)
		{
			if (accountName == null)
			{
				throw new ArgumentNullException("accountName");
			}
			if (accountName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), "accountName");
			}
			if (accountName.Length > 256)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_AccountNameTooLong"), "accountName");
			}
			if (domainName != null && domainName.Length > 255)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_DomainNameTooLong"), "domainName");
			}
			if (domainName == null || domainName.Length == 0)
			{
				this._Name = accountName;
				return;
			}
			this._Name = domainName + "\\" + accountName;
		}

		// Token: 0x06005589 RID: 21897 RVA: 0x00135E68 File Offset: 0x00134E68
		public NTAccount(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StringZeroLength"), "name");
			}
			if (name.Length > 512)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_AccountNameTooLong"), "name");
			}
			this._Name = name;
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x0600558A RID: 21898 RVA: 0x00135ECF File Offset: 0x00134ECF
		public override string Value
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x0600558B RID: 21899 RVA: 0x00135ED7 File Offset: 0x00134ED7
		public override bool IsValidTargetType(Type targetType)
		{
			return targetType == typeof(SecurityIdentifier) || targetType == typeof(NTAccount);
		}

		// Token: 0x0600558C RID: 21900 RVA: 0x00135EF8 File Offset: 0x00134EF8
		public override IdentityReference Translate(Type targetType)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (targetType == typeof(NTAccount))
			{
				return this;
			}
			if (targetType == typeof(SecurityIdentifier))
			{
				IdentityReferenceCollection identityReferenceCollection = NTAccount.Translate(new IdentityReferenceCollection(1)
				{
					this
				}, targetType, true);
				return identityReferenceCollection[0];
			}
			throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), "targetType");
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x00135F64 File Offset: 0x00134F64
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			NTAccount ntaccount = o as NTAccount;
			return !(ntaccount == null) && this == ntaccount;
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x00135F8F File Offset: 0x00134F8F
		public override int GetHashCode()
		{
			return StringComparer.InvariantCultureIgnoreCase.GetHashCode(this._Name);
		}

		// Token: 0x0600558F RID: 21903 RVA: 0x00135FA1 File Offset: 0x00134FA1
		public override string ToString()
		{
			return this._Name;
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x00135FAC File Offset: 0x00134FAC
		internal static IdentityReferenceCollection Translate(IdentityReferenceCollection sourceAccounts, Type targetType, bool forceSuccess)
		{
			bool flag = false;
			IdentityReferenceCollection identityReferenceCollection = NTAccount.Translate(sourceAccounts, targetType, out flag);
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

		// Token: 0x06005591 RID: 21905 RVA: 0x0013602C File Offset: 0x0013502C
		internal static IdentityReferenceCollection Translate(IdentityReferenceCollection sourceAccounts, Type targetType, out bool someFailed)
		{
			if (sourceAccounts == null)
			{
				throw new ArgumentNullException("sourceAccounts");
			}
			if (targetType == typeof(SecurityIdentifier))
			{
				return NTAccount.TranslateToSids(sourceAccounts, out someFailed);
			}
			throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), "targetType");
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x00136068 File Offset: 0x00135068
		public static bool operator ==(NTAccount left, NTAccount right)
		{
			return (left == null && right == null) || (left != null && right != null && left.ToString().Equals(right.ToString(), StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06005593 RID: 21907 RVA: 0x0013609B File Offset: 0x0013509B
		public static bool operator !=(NTAccount left, NTAccount right)
		{
			return !(left == right);
		}

		// Token: 0x06005594 RID: 21908 RVA: 0x001360A8 File Offset: 0x001350A8
		private static IdentityReferenceCollection TranslateToSids(IdentityReferenceCollection sourceAccounts, out bool someFailed)
		{
			if (!Win32.LsaApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_Win9x"));
			}
			SafeLsaPolicyHandle safeLsaPolicyHandle = SafeLsaPolicyHandle.InvalidHandle;
			SafeLsaMemoryHandle invalidHandle = SafeLsaMemoryHandle.InvalidHandle;
			SafeLsaMemoryHandle invalidHandle2 = SafeLsaMemoryHandle.InvalidHandle;
			int i = 0;
			if (sourceAccounts == null)
			{
				throw new ArgumentNullException("sourceAccounts");
			}
			if (sourceAccounts.Count == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EmptyCollection"), "sourceAccounts");
			}
			IdentityReferenceCollection result;
			try
			{
				Win32Native.UNICODE_STRING[] array = new Win32Native.UNICODE_STRING[sourceAccounts.Count];
				foreach (IdentityReference identityReference in sourceAccounts)
				{
					NTAccount ntaccount = identityReference as NTAccount;
					if (ntaccount == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_ImproperType"), "sourceAccounts");
					}
					array[i].Buffer = ntaccount.ToString();
					if (array[i].Buffer.Length * 2 + 2 > 65535)
					{
						throw new SystemException();
					}
					array[i].Length = (ushort)(array[i].Buffer.Length * 2);
					array[i].MaximumLength = array[i].Length + 2;
					i++;
				}
				safeLsaPolicyHandle = Win32.LsaOpenPolicy(null, PolicyRights.POLICY_LOOKUP_NAMES);
				someFailed = false;
				uint num;
				if (Win32.LsaLookupNames2Supported)
				{
					num = Win32Native.LsaLookupNames2(safeLsaPolicyHandle, 0, sourceAccounts.Count, array, ref invalidHandle, ref invalidHandle2);
				}
				else
				{
					num = Win32Native.LsaLookupNames(safeLsaPolicyHandle, sourceAccounts.Count, array, ref invalidHandle, ref invalidHandle2);
				}
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
				IdentityReferenceCollection identityReferenceCollection = new IdentityReferenceCollection(sourceAccounts.Count);
				if (num == 0U || num == 263U)
				{
					if (Win32.LsaLookupNames2Supported)
					{
						i = 0;
						while (i < sourceAccounts.Count)
						{
							Win32Native.LSA_TRANSLATED_SID2 lsa_TRANSLATED_SID = (Win32Native.LSA_TRANSLATED_SID2)Marshal.PtrToStructure(new IntPtr((long)invalidHandle2.DangerousGetHandle() + (long)(i * Marshal.SizeOf(typeof(Win32Native.LSA_TRANSLATED_SID2)))), typeof(Win32Native.LSA_TRANSLATED_SID2));
							switch (lsa_TRANSLATED_SID.Use)
							{
							case 1:
							case 2:
							case 4:
							case 5:
							case 9:
								identityReferenceCollection.Add(new SecurityIdentifier(lsa_TRANSLATED_SID.Sid, true));
								break;
							case 3:
							case 6:
							case 7:
							case 8:
								goto IL_287;
							default:
								goto IL_287;
							}
							IL_298:
							i++;
							continue;
							IL_287:
							someFailed = true;
							identityReferenceCollection.Add(sourceAccounts[i]);
							goto IL_298;
						}
					}
					else
					{
						Win32Native.LSA_REFERENCED_DOMAIN_LIST lsa_REFERENCED_DOMAIN_LIST = (Win32Native.LSA_REFERENCED_DOMAIN_LIST)Marshal.PtrToStructure(invalidHandle.DangerousGetHandle(), typeof(Win32Native.LSA_REFERENCED_DOMAIN_LIST));
						SecurityIdentifier[] array2 = new SecurityIdentifier[lsa_REFERENCED_DOMAIN_LIST.Entries];
						for (i = 0; i < lsa_REFERENCED_DOMAIN_LIST.Entries; i++)
						{
							array2[i] = new SecurityIdentifier(((Win32Native.LSA_TRUST_INFORMATION)Marshal.PtrToStructure(new IntPtr((long)lsa_REFERENCED_DOMAIN_LIST.Domains + (long)(i * Marshal.SizeOf(typeof(Win32Native.LSA_TRUST_INFORMATION)))), typeof(Win32Native.LSA_TRUST_INFORMATION))).Sid, true);
						}
						i = 0;
						while (i < sourceAccounts.Count)
						{
							Win32Native.LSA_TRANSLATED_SID lsa_TRANSLATED_SID2 = (Win32Native.LSA_TRANSLATED_SID)Marshal.PtrToStructure(new IntPtr((long)invalidHandle2.DangerousGetHandle() + (long)(i * Marshal.SizeOf(typeof(Win32Native.LSA_TRANSLATED_SID)))), typeof(Win32Native.LSA_TRANSLATED_SID));
							switch (lsa_TRANSLATED_SID2.Use)
							{
							case 1:
							case 2:
							case 4:
							case 5:
							case 9:
								identityReferenceCollection.Add(new SecurityIdentifier(array2[lsa_TRANSLATED_SID2.DomainIndex], lsa_TRANSLATED_SID2.Rid));
								break;
							case 3:
							case 6:
							case 7:
							case 8:
								goto IL_3CF;
							default:
								goto IL_3CF;
							}
							IL_3E0:
							i++;
							continue;
							IL_3CF:
							someFailed = true;
							identityReferenceCollection.Add(sourceAccounts[i]);
							goto IL_3E0;
						}
					}
				}
				else
				{
					for (i = 0; i < sourceAccounts.Count; i++)
					{
						identityReferenceCollection.Add(sourceAccounts[i]);
					}
				}
				result = identityReferenceCollection;
			}
			finally
			{
				safeLsaPolicyHandle.Dispose();
				invalidHandle.Dispose();
				invalidHandle2.Dispose();
			}
			return result;
		}

		// Token: 0x04002C65 RID: 11365
		internal const int MaximumAccountNameLength = 256;

		// Token: 0x04002C66 RID: 11366
		internal const int MaximumDomainNameLength = 255;

		// Token: 0x04002C67 RID: 11367
		private readonly string _Name;
	}
}
