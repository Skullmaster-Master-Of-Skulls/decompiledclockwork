using System;
using System.Collections;

namespace System.Security.AccessControl
{
	// Token: 0x02000906 RID: 2310
	public sealed class RawAcl : GenericAcl
	{
		// Token: 0x0600538B RID: 21387 RVA: 0x0012E1EC File Offset: 0x0012D1EC
		private static void VerifyHeader(byte[] binaryForm, int offset, out byte revision, out int count, out int length)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (binaryForm.Length - offset >= 8)
			{
				revision = binaryForm[offset];
				length = (int)binaryForm[offset + 2] + ((int)binaryForm[offset + 3] << 8);
				count = (int)binaryForm[offset + 4] + ((int)binaryForm[offset + 5] << 8);
				if (length <= binaryForm.Length - offset)
				{
					return;
				}
			}
			throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x0012E26C File Offset: 0x0012D26C
		private void MarshalHeader(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this.BinaryLength > GenericAcl.MaxBinaryLength)
			{
				throw new InvalidOperationException(Environment.GetResourceString("AccessControl_AclTooLong"));
			}
			if (binaryForm.Length - offset < this.BinaryLength)
			{
				throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
			}
			binaryForm[offset] = this.Revision;
			binaryForm[offset + 1] = 0;
			binaryForm[offset + 2] = (byte)this.BinaryLength;
			binaryForm[offset + 3] = (byte)(this.BinaryLength >> 8);
			binaryForm[offset + 4] = (byte)this.Count;
			binaryForm[offset + 5] = (byte)(this.Count >> 8);
			binaryForm[offset + 6] = 0;
			binaryForm[offset + 7] = 0;
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x0012E330 File Offset: 0x0012D330
		internal void SetBinaryForm(byte[] binaryForm, int offset)
		{
			int num;
			int num2;
			RawAcl.VerifyHeader(binaryForm, offset, out this._revision, out num, out num2);
			num2 += offset;
			offset += 8;
			this._aces = new ArrayList(num);
			this._binaryLength = 8;
			for (int i = 0; i < num; i++)
			{
				GenericAce genericAce = GenericAce.CreateFromBinaryForm(binaryForm, offset);
				int binaryLength = genericAce.BinaryLength;
				if (this._binaryLength + binaryLength > GenericAcl.MaxBinaryLength)
				{
					throw new ArgumentException(Environment.GetResourceString("ArgumentException_InvalidAclBinaryForm"), "binaryForm");
				}
				this._aces.Add(genericAce);
				if (binaryLength % 4 != 0)
				{
					throw new SystemException();
				}
				this._binaryLength += binaryLength;
				if (this._revision == GenericAcl.AclRevisionDS)
				{
					offset += (int)binaryForm[offset + 2] + ((int)binaryForm[offset + 3] << 8);
				}
				else
				{
					offset += binaryLength;
				}
				if (offset > num2)
				{
					throw new ArgumentException(Environment.GetResourceString("ArgumentException_InvalidAclBinaryForm"), "binaryForm");
				}
			}
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x0012E418 File Offset: 0x0012D418
		public RawAcl(byte revision, int capacity)
		{
			this._revision = revision;
			this._aces = new ArrayList(capacity);
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x0012E43A File Offset: 0x0012D43A
		public RawAcl(byte[] binaryForm, int offset)
		{
			this.SetBinaryForm(binaryForm, offset);
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06005390 RID: 21392 RVA: 0x0012E451 File Offset: 0x0012D451
		public override byte Revision
		{
			get
			{
				return this._revision;
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06005391 RID: 21393 RVA: 0x0012E459 File Offset: 0x0012D459
		public override int Count
		{
			get
			{
				return this._aces.Count;
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06005392 RID: 21394 RVA: 0x0012E466 File Offset: 0x0012D466
		public override int BinaryLength
		{
			get
			{
				return this._binaryLength;
			}
		}

		// Token: 0x06005393 RID: 21395 RVA: 0x0012E470 File Offset: 0x0012D470
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			this.MarshalHeader(binaryForm, offset);
			offset += 8;
			for (int i = 0; i < this.Count; i++)
			{
				GenericAce genericAce = this._aces[i] as GenericAce;
				genericAce.GetBinaryForm(binaryForm, offset);
				int binaryLength = genericAce.BinaryLength;
				if (binaryLength % 4 != 0)
				{
					throw new SystemException();
				}
				offset += binaryLength;
			}
		}

		// Token: 0x17000E6F RID: 3695
		public override GenericAce this[int index]
		{
			get
			{
				return this._aces[index] as GenericAce;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.BinaryLength % 4 != 0)
				{
					throw new SystemException();
				}
				int num = this.BinaryLength - ((index < this._aces.Count) ? (this._aces[index] as GenericAce).BinaryLength : 0) + value.BinaryLength;
				if (num > GenericAcl.MaxBinaryLength)
				{
					throw new OverflowException(Environment.GetResourceString("AccessControl_AclTooLong"));
				}
				this._aces[index] = value;
				this._binaryLength = num;
			}
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x0012E574 File Offset: 0x0012D574
		public void InsertAce(int index, GenericAce ace)
		{
			if (ace == null)
			{
				throw new ArgumentNullException("ace");
			}
			if (this._binaryLength + ace.BinaryLength > GenericAcl.MaxBinaryLength)
			{
				throw new OverflowException(Environment.GetResourceString("AccessControl_AclTooLong"));
			}
			this._aces.Insert(index, ace);
			this._binaryLength += ace.BinaryLength;
		}

		// Token: 0x06005397 RID: 21399 RVA: 0x0012E5DC File Offset: 0x0012D5DC
		public void RemoveAce(int index)
		{
			GenericAce genericAce = this._aces[index] as GenericAce;
			this._aces.RemoveAt(index);
			this._binaryLength -= genericAce.BinaryLength;
		}

		// Token: 0x04002B56 RID: 11094
		private byte _revision;

		// Token: 0x04002B57 RID: 11095
		private ArrayList _aces;

		// Token: 0x04002B58 RID: 11096
		private int _binaryLength = 8;
	}
}
