using System;
using System.Collections;

namespace System.Security.AccessControl
{
	// Token: 0x02000905 RID: 2309
	public abstract class GenericAcl : ICollection, IEnumerable
	{
		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x0600537E RID: 21374
		public abstract byte Revision { get; }

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x0600537F RID: 21375
		public abstract int BinaryLength { get; }

		// Token: 0x17000E68 RID: 3688
		public abstract GenericAce this[int index]
		{
			get;
			set;
		}

		// Token: 0x06005382 RID: 21378
		public abstract void GetBinaryForm(byte[] binaryForm, int offset);

		// Token: 0x06005383 RID: 21379 RVA: 0x0012E11C File Offset: 0x0012D11C
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new RankException(Environment.GetResourceString("Rank_MultiDimNotSupported"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentOutOfRangeException("array", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x0012E1AF File Offset: 0x0012D1AF
		public void CopyTo(GenericAce[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06005385 RID: 21381
		public abstract int Count { get; }

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06005386 RID: 21382 RVA: 0x0012E1B9 File Offset: 0x0012D1B9
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06005387 RID: 21383 RVA: 0x0012E1BC File Offset: 0x0012D1BC
		public object SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x0012E1BF File Offset: 0x0012D1BF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new AceEnumerator(this);
		}

		// Token: 0x06005389 RID: 21385 RVA: 0x0012E1C7 File Offset: 0x0012D1C7
		public AceEnumerator GetEnumerator()
		{
			return ((IEnumerable)this).GetEnumerator() as AceEnumerator;
		}

		// Token: 0x04002B52 RID: 11090
		internal const int HeaderLength = 8;

		// Token: 0x04002B53 RID: 11091
		public static readonly byte AclRevision = 2;

		// Token: 0x04002B54 RID: 11092
		public static readonly byte AclRevisionDS = 4;

		// Token: 0x04002B55 RID: 11093
		public static readonly int MaxBinaryLength = 65535;
	}
}
