using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Win32;

namespace System.Security.AccessControl
{
	// Token: 0x0200093C RID: 2364
	public sealed class RawSecurityDescriptor : GenericSecurityDescriptor
	{
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06005530 RID: 21808 RVA: 0x00134BD3 File Offset: 0x00133BD3
		internal override GenericAcl GenericSacl
		{
			get
			{
				return this._sacl;
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06005531 RID: 21809 RVA: 0x00134BDB File Offset: 0x00133BDB
		internal override GenericAcl GenericDacl
		{
			get
			{
				return this._dacl;
			}
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x00134BE3 File Offset: 0x00133BE3
		private void CreateFromParts(ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
		{
			this.SetFlags(flags);
			this.Owner = owner;
			this.Group = group;
			this.SystemAcl = systemAcl;
			this.DiscretionaryAcl = discretionaryAcl;
			this.ResourceManagerControl = 0;
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x00134C11 File Offset: 0x00133C11
		public RawSecurityDescriptor(ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
		{
			this.CreateFromParts(flags, owner, group, systemAcl, discretionaryAcl);
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x00134C26 File Offset: 0x00133C26
		public RawSecurityDescriptor(string sddlForm) : this(RawSecurityDescriptor.BinaryFormFromSddlForm(sddlForm), 0)
		{
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x00134C38 File Offset: 0x00133C38
		public RawSecurityDescriptor(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (binaryForm.Length - offset < 20)
			{
				throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
			}
			if (binaryForm[offset] != GenericSecurityDescriptor.Revision)
			{
				throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("AccessControl_InvalidSecurityDescriptorRevision"));
			}
			byte resourceManagerControl = binaryForm[offset + 1];
			ControlFlags controlFlags = (ControlFlags)((int)binaryForm[offset + 2] + ((int)binaryForm[offset + 3] << 8));
			if ((controlFlags & ControlFlags.SelfRelative) == ControlFlags.None)
			{
				throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidSecurityDescriptorSelfRelativeForm"), "binaryForm");
			}
			int num = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 4);
			SecurityIdentifier owner;
			if (num != 0)
			{
				owner = new SecurityIdentifier(binaryForm, offset + num);
			}
			else
			{
				owner = null;
			}
			int num2 = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 8);
			SecurityIdentifier group;
			if (num2 != 0)
			{
				group = new SecurityIdentifier(binaryForm, offset + num2);
			}
			else
			{
				group = null;
			}
			int num3 = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 12);
			RawAcl systemAcl;
			if ((controlFlags & ControlFlags.SystemAclPresent) != ControlFlags.None && num3 != 0)
			{
				systemAcl = new RawAcl(binaryForm, offset + num3);
			}
			else
			{
				systemAcl = null;
			}
			int num4 = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 16);
			RawAcl discretionaryAcl;
			if ((controlFlags & ControlFlags.DiscretionaryAclPresent) != ControlFlags.None && num4 != 0)
			{
				discretionaryAcl = new RawAcl(binaryForm, offset + num4);
			}
			else
			{
				discretionaryAcl = null;
			}
			this.CreateFromParts(controlFlags, owner, group, systemAcl, discretionaryAcl);
			if ((controlFlags & ControlFlags.RMControlValid) != ControlFlags.None)
			{
				this.ResourceManagerControl = resourceManagerControl;
			}
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x00134D88 File Offset: 0x00133D88
		private static byte[] BinaryFormFromSddlForm(string sddlForm)
		{
			if (!GenericSecurityDescriptor.IsSddlConversionSupported())
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_Win9x"));
			}
			if (sddlForm == null)
			{
				throw new ArgumentNullException("sddlForm");
			}
			IntPtr zero = IntPtr.Zero;
			uint num = 0U;
			byte[] array = null;
			try
			{
				if (1 != Win32Native.ConvertStringSdToSd(sddlForm, (uint)GenericSecurityDescriptor.Revision, out zero, ref num))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == 87 || lastWin32Error == 1336 || lastWin32Error == 1338 || lastWin32Error == 1305)
					{
						throw new ArgumentException(Environment.GetResourceString("ArgumentException_InvalidSDSddlForm"), "sddlForm");
					}
					if (lastWin32Error == 8)
					{
						throw new OutOfMemoryException();
					}
					if (lastWin32Error == 1337)
					{
						throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidSidInSDDLString"), "sddlForm");
					}
					if (lastWin32Error != 0)
					{
						throw new SystemException();
					}
				}
				array = new byte[num];
				Marshal.Copy(zero, array, 0, (int)num);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Win32Native.LocalFree(zero);
				}
			}
			return array;
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06005537 RID: 21815 RVA: 0x00134E78 File Offset: 0x00133E78
		public override ControlFlags ControlFlags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06005538 RID: 21816 RVA: 0x00134E80 File Offset: 0x00133E80
		// (set) Token: 0x06005539 RID: 21817 RVA: 0x00134E88 File Offset: 0x00133E88
		public override SecurityIdentifier Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x0600553A RID: 21818 RVA: 0x00134E91 File Offset: 0x00133E91
		// (set) Token: 0x0600553B RID: 21819 RVA: 0x00134E99 File Offset: 0x00133E99
		public override SecurityIdentifier Group
		{
			get
			{
				return this._group;
			}
			set
			{
				this._group = value;
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x0600553C RID: 21820 RVA: 0x00134EA2 File Offset: 0x00133EA2
		// (set) Token: 0x0600553D RID: 21821 RVA: 0x00134EAA File Offset: 0x00133EAA
		public RawAcl SystemAcl
		{
			get
			{
				return this._sacl;
			}
			set
			{
				this._sacl = value;
			}
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x0600553E RID: 21822 RVA: 0x00134EB3 File Offset: 0x00133EB3
		// (set) Token: 0x0600553F RID: 21823 RVA: 0x00134EBB File Offset: 0x00133EBB
		public RawAcl DiscretionaryAcl
		{
			get
			{
				return this._dacl;
			}
			set
			{
				this._dacl = value;
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06005540 RID: 21824 RVA: 0x00134EC4 File Offset: 0x00133EC4
		// (set) Token: 0x06005541 RID: 21825 RVA: 0x00134ECC File Offset: 0x00133ECC
		public byte ResourceManagerControl
		{
			get
			{
				return this._rmControl;
			}
			set
			{
				this._rmControl = value;
			}
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x00134ED5 File Offset: 0x00133ED5
		public void SetFlags(ControlFlags flags)
		{
			this._flags = (flags | ControlFlags.SelfRelative);
		}

		// Token: 0x04002C55 RID: 11349
		private SecurityIdentifier _owner;

		// Token: 0x04002C56 RID: 11350
		private SecurityIdentifier _group;

		// Token: 0x04002C57 RID: 11351
		private ControlFlags _flags;

		// Token: 0x04002C58 RID: 11352
		private RawAcl _sacl;

		// Token: 0x04002C59 RID: 11353
		private RawAcl _dacl;

		// Token: 0x04002C5A RID: 11354
		private byte _rmControl;
	}
}
