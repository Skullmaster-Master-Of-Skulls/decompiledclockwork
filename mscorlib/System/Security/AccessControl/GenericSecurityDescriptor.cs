using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200093B RID: 2363
	public abstract class GenericSecurityDescriptor
	{
		// Token: 0x06005520 RID: 21792 RVA: 0x00134878 File Offset: 0x00133878
		private static void MarshalInt(byte[] binaryForm, int offset, int number)
		{
			binaryForm[offset] = (byte)number;
			binaryForm[offset + 1] = (byte)(number >> 8);
			binaryForm[offset + 2] = (byte)(number >> 16);
			binaryForm[offset + 3] = (byte)(number >> 24);
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x0013489C File Offset: 0x0013389C
		internal static int UnmarshalInt(byte[] binaryForm, int offset)
		{
			return (int)binaryForm[offset] + ((int)binaryForm[offset + 1] << 8) + ((int)binaryForm[offset + 2] << 16) + ((int)binaryForm[offset + 3] << 24);
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06005523 RID: 21795
		internal abstract GenericAcl GenericSacl { get; }

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06005524 RID: 21796
		internal abstract GenericAcl GenericDacl { get; }

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06005525 RID: 21797 RVA: 0x001348C3 File Offset: 0x001338C3
		private bool IsCraftedAefaDacl
		{
			get
			{
				return this.GenericDacl is DiscretionaryAcl && (this.GenericDacl as DiscretionaryAcl).EveryOneFullAccessForNullDacl;
			}
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x001348E4 File Offset: 0x001338E4
		public static bool IsSddlConversionSupported()
		{
			return Win32.IsSddlConversionSupported();
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06005527 RID: 21799 RVA: 0x001348EB File Offset: 0x001338EB
		public static byte Revision
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06005528 RID: 21800
		public abstract ControlFlags ControlFlags { get; }

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06005529 RID: 21801
		// (set) Token: 0x0600552A RID: 21802
		public abstract SecurityIdentifier Owner { get; set; }

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x0600552B RID: 21803
		// (set) Token: 0x0600552C RID: 21804
		public abstract SecurityIdentifier Group { get; set; }

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x0600552D RID: 21805 RVA: 0x001348F0 File Offset: 0x001338F0
		public int BinaryLength
		{
			get
			{
				int num = 20;
				if (this.Owner != null)
				{
					num += this.Owner.BinaryLength;
				}
				if (this.Group != null)
				{
					num += this.Group.BinaryLength;
				}
				if ((this.ControlFlags & ControlFlags.SystemAclPresent) != ControlFlags.None && this.GenericSacl != null)
				{
					num += this.GenericSacl.BinaryLength;
				}
				if ((this.ControlFlags & ControlFlags.DiscretionaryAclPresent) != ControlFlags.None && this.GenericDacl != null && !this.IsCraftedAefaDacl)
				{
					num += this.GenericDacl.BinaryLength;
				}
				return num;
			}
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x00134984 File Offset: 0x00133984
		public string GetSddlForm(AccessControlSections includeSections)
		{
			byte[] binaryForm = new byte[this.BinaryLength];
			this.GetBinaryForm(binaryForm, 0);
			SecurityInfos securityInfos = (SecurityInfos)0;
			if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.Owner;
			}
			if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.Group;
			}
			if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.SystemAcl;
			}
			if ((includeSections & AccessControlSections.Access) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.DiscretionaryAcl;
			}
			string result;
			int num = Win32.ConvertSdToSddl(binaryForm, 1, securityInfos, out result);
			if (num == 87 || num == 1305)
			{
				throw new InvalidOperationException();
			}
			if (num != 0)
			{
				throw new InvalidOperationException();
			}
			return result;
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x001349F4 File Offset: 0x001339F4
		public void GetBinaryForm(byte[] binaryForm, int offset)
		{
			int num = offset;
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (binaryForm.Length - offset < this.BinaryLength)
			{
				throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
			}
			int binaryLength = this.BinaryLength;
			byte b = (this is RawSecurityDescriptor && (this.ControlFlags & ControlFlags.RMControlValid) != ControlFlags.None) ? (this as RawSecurityDescriptor).ResourceManagerControl : 0;
			int num2 = (int)this.ControlFlags;
			if (this.IsCraftedAefaDacl)
			{
				num2 &= -5;
			}
			binaryForm[offset] = GenericSecurityDescriptor.Revision;
			binaryForm[offset + 1] = b;
			binaryForm[offset + 2] = (byte)num2;
			binaryForm[offset + 3] = (byte)(num2 >> 8);
			int offset2 = offset + 4;
			int offset3 = offset + 8;
			int offset4 = offset + 12;
			int offset5 = offset + 16;
			offset += 20;
			if (this.Owner != null)
			{
				GenericSecurityDescriptor.MarshalInt(binaryForm, offset2, offset - num);
				this.Owner.GetBinaryForm(binaryForm, offset);
				offset += this.Owner.BinaryLength;
			}
			else
			{
				GenericSecurityDescriptor.MarshalInt(binaryForm, offset2, 0);
			}
			if (this.Group != null)
			{
				GenericSecurityDescriptor.MarshalInt(binaryForm, offset3, offset - num);
				this.Group.GetBinaryForm(binaryForm, offset);
				offset += this.Group.BinaryLength;
			}
			else
			{
				GenericSecurityDescriptor.MarshalInt(binaryForm, offset3, 0);
			}
			if ((this.ControlFlags & ControlFlags.SystemAclPresent) != ControlFlags.None && this.GenericSacl != null)
			{
				GenericSecurityDescriptor.MarshalInt(binaryForm, offset4, offset - num);
				this.GenericSacl.GetBinaryForm(binaryForm, offset);
				offset += this.GenericSacl.BinaryLength;
			}
			else
			{
				GenericSecurityDescriptor.MarshalInt(binaryForm, offset4, 0);
			}
			if ((this.ControlFlags & ControlFlags.DiscretionaryAclPresent) != ControlFlags.None && this.GenericDacl != null && !this.IsCraftedAefaDacl)
			{
				GenericSecurityDescriptor.MarshalInt(binaryForm, offset5, offset - num);
				this.GenericDacl.GetBinaryForm(binaryForm, offset);
				offset += this.GenericDacl.BinaryLength;
				return;
			}
			GenericSecurityDescriptor.MarshalInt(binaryForm, offset5, 0);
		}

		// Token: 0x04002C50 RID: 11344
		internal const int HeaderLength = 20;

		// Token: 0x04002C51 RID: 11345
		internal const int OwnerFoundAt = 4;

		// Token: 0x04002C52 RID: 11346
		internal const int GroupFoundAt = 8;

		// Token: 0x04002C53 RID: 11347
		internal const int SaclFoundAt = 12;

		// Token: 0x04002C54 RID: 11348
		internal const int DaclFoundAt = 16;
	}
}
