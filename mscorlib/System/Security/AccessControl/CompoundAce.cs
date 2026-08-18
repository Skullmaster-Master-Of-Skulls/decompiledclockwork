using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020008FE RID: 2302
	public sealed class CompoundAce : KnownAce
	{
		// Token: 0x06005353 RID: 21331 RVA: 0x0012D60C File Offset: 0x0012C60C
		public CompoundAce(AceFlags flags, int accessMask, CompoundAceType compoundAceType, SecurityIdentifier sid) : base(AceType.AccessAllowedCompound, flags, accessMask, sid)
		{
			this._compoundAceType = compoundAceType;
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x0012D620 File Offset: 0x0012C620
		internal static bool ParseBinaryForm(byte[] binaryForm, int offset, out int accessMask, out CompoundAceType compoundAceType, out SecurityIdentifier sid)
		{
			GenericAce.VerifyHeader(binaryForm, offset);
			if (binaryForm.Length - offset >= 12 + SecurityIdentifier.MinBinaryLength)
			{
				int num = offset + 4;
				int num2 = 0;
				accessMask = (int)binaryForm[num] + ((int)binaryForm[num + 1] << 8) + ((int)binaryForm[num + 2] << 16) + ((int)binaryForm[num + 3] << 24);
				num2 += 4;
				compoundAceType = (CompoundAceType)((int)binaryForm[num + num2] + ((int)binaryForm[num + num2 + 1] << 8));
				num2 += 4;
				sid = new SecurityIdentifier(binaryForm, num + num2);
				return true;
			}
			accessMask = 0;
			compoundAceType = (CompoundAceType)0;
			sid = null;
			return false;
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06005355 RID: 21333 RVA: 0x0012D69A File Offset: 0x0012C69A
		// (set) Token: 0x06005356 RID: 21334 RVA: 0x0012D6A2 File Offset: 0x0012C6A2
		public CompoundAceType CompoundAceType
		{
			get
			{
				return this._compoundAceType;
			}
			set
			{
				this._compoundAceType = value;
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06005357 RID: 21335 RVA: 0x0012D6AB File Offset: 0x0012C6AB
		public override int BinaryLength
		{
			get
			{
				return 12 + base.SecurityIdentifier.BinaryLength;
			}
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x0012D6BC File Offset: 0x0012C6BC
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			base.MarshalHeader(binaryForm, offset);
			int num = offset + 4;
			int num2 = 0;
			binaryForm[num] = (byte)base.AccessMask;
			binaryForm[num + 1] = (byte)(base.AccessMask >> 8);
			binaryForm[num + 2] = (byte)(base.AccessMask >> 16);
			binaryForm[num + 3] = (byte)(base.AccessMask >> 24);
			num2 += 4;
			binaryForm[num + num2] = (byte)((ushort)this.CompoundAceType);
			binaryForm[num + num2 + 1] = (byte)((ushort)this.CompoundAceType >> 8);
			binaryForm[num + num2 + 2] = 0;
			binaryForm[num + num2 + 3] = 0;
			num2 += 4;
			base.SecurityIdentifier.GetBinaryForm(binaryForm, num + num2);
		}

		// Token: 0x04002B3C RID: 11068
		private const int AceTypeLength = 4;

		// Token: 0x04002B3D RID: 11069
		private CompoundAceType _compoundAceType;
	}
}
