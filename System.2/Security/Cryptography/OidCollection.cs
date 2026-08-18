using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	// Token: 0x0200045F RID: 1119
	public sealed class OidCollection : ICollection, IEnumerable
	{
		// Token: 0x06002998 RID: 10648 RVA: 0x000BCC79 File Offset: 0x000BAE79
		public OidCollection()
		{
			this.m_list = new ArrayList();
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000BCC8C File Offset: 0x000BAE8C
		public int Add(Oid oid)
		{
			return this.m_list.Add(oid);
		}

		// Token: 0x17000A17 RID: 2583
		public Oid this[int index]
		{
			get
			{
				return this.m_list[index] as Oid;
			}
		}

		// Token: 0x17000A18 RID: 2584
		public Oid this[string oid]
		{
			get
			{
				string text = X509Utils.FindOidInfoWithFallback(2U, oid, OidGroup.All);
				if (text == null)
				{
					text = oid;
				}
				foreach (object obj in this.m_list)
				{
					Oid oid2 = (Oid)obj;
					if (oid2.Value == text)
					{
						return oid2;
					}
				}
				return null;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x000BCD28 File Offset: 0x000BAF28
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000BCD35 File Offset: 0x000BAF35
		public OidEnumerator GetEnumerator()
		{
			return new OidEnumerator(this);
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x000BCD3D File Offset: 0x000BAF3D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new OidEnumerator(this);
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x000BCD48 File Offset: 0x000BAF48
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.GetString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("ArgumentOutOfRange_Index"));
			}
			if (index + this.Count > array.Length)
			{
				throw new ArgumentException(SR.GetString("Argument_InvalidOffLen"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index);
				index++;
			}
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000BCDE2 File Offset: 0x000BAFE2
		public void CopyTo(Oid[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060029A1 RID: 10657 RVA: 0x000BCDEC File Offset: 0x000BAFEC
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060029A2 RID: 10658 RVA: 0x000BCDEF File Offset: 0x000BAFEF
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x040025A1 RID: 9633
		private ArrayList m_list;
	}
}
