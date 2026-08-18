using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x02000940 RID: 2368
	[ComVisible(false)]
	public class IdentityReferenceCollection : ICollection<IdentityReference>, IEnumerable<IdentityReference>, IEnumerable
	{
		// Token: 0x06005571 RID: 21873 RVA: 0x001358F0 File Offset: 0x001348F0
		public IdentityReferenceCollection() : this(0)
		{
		}

		// Token: 0x06005572 RID: 21874 RVA: 0x001358F9 File Offset: 0x001348F9
		public IdentityReferenceCollection(int capacity)
		{
			this._Identities = new ArrayList(capacity);
		}

		// Token: 0x06005573 RID: 21875 RVA: 0x0013590D File Offset: 0x0013490D
		public void CopyTo(IdentityReference[] array, int offset)
		{
			this._Identities.CopyTo(0, array, offset, this.Count);
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06005574 RID: 21876 RVA: 0x00135923 File Offset: 0x00134923
		public int Count
		{
			get
			{
				return this._Identities.Count;
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06005575 RID: 21877 RVA: 0x00135930 File Offset: 0x00134930
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x00135933 File Offset: 0x00134933
		public void Add(IdentityReference identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this._Identities.Add(identity);
		}

		// Token: 0x06005577 RID: 21879 RVA: 0x00135956 File Offset: 0x00134956
		public bool Remove(IdentityReference identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (this.Contains(identity))
			{
				this._Identities.Remove(identity);
				return true;
			}
			return false;
		}

		// Token: 0x06005578 RID: 21880 RVA: 0x00135984 File Offset: 0x00134984
		public void Clear()
		{
			this._Identities.Clear();
		}

		// Token: 0x06005579 RID: 21881 RVA: 0x00135991 File Offset: 0x00134991
		public bool Contains(IdentityReference identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			return this._Identities.Contains(identity);
		}

		// Token: 0x0600557A RID: 21882 RVA: 0x001359B3 File Offset: 0x001349B3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600557B RID: 21883 RVA: 0x001359BB File Offset: 0x001349BB
		public IEnumerator<IdentityReference> GetEnumerator()
		{
			return new IdentityReferenceEnumerator(this);
		}

		// Token: 0x17000ED3 RID: 3795
		public IdentityReference this[int index]
		{
			get
			{
				return this._Identities[index] as IdentityReference;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._Identities[index] = value;
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x0600557E RID: 21886 RVA: 0x001359F9 File Offset: 0x001349F9
		internal ArrayList Identities
		{
			get
			{
				return this._Identities;
			}
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x00135A01 File Offset: 0x00134A01
		public IdentityReferenceCollection Translate(Type targetType)
		{
			return this.Translate(targetType, false);
		}

		// Token: 0x06005580 RID: 21888 RVA: 0x00135A0C File Offset: 0x00134A0C
		public IdentityReferenceCollection Translate(Type targetType, bool forceSuccess)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (!targetType.IsSubclassOf(typeof(IdentityReference)))
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_MustBeIdentityReference"), "targetType");
			}
			if (this.Identities.Count == 0)
			{
				return new IdentityReferenceCollection();
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.Identities.Count; i++)
			{
				Type type = this.Identities[i].GetType();
				if (type != targetType)
				{
					if (type == typeof(SecurityIdentifier))
					{
						num++;
					}
					else
					{
						if (type != typeof(NTAccount))
						{
							throw new SystemException();
						}
						num2++;
					}
				}
			}
			bool flag = false;
			IdentityReferenceCollection identityReferenceCollection = null;
			IdentityReferenceCollection identityReferenceCollection2 = null;
			if (num == this.Count)
			{
				flag = true;
				identityReferenceCollection = this;
			}
			else if (num > 0)
			{
				identityReferenceCollection = new IdentityReferenceCollection(num);
			}
			if (num2 == this.Count)
			{
				flag = true;
				identityReferenceCollection2 = this;
			}
			else if (num2 > 0)
			{
				identityReferenceCollection2 = new IdentityReferenceCollection(num2);
			}
			IdentityReferenceCollection identityReferenceCollection3 = null;
			if (!flag)
			{
				identityReferenceCollection3 = new IdentityReferenceCollection(this.Identities.Count);
				for (int j = 0; j < this.Identities.Count; j++)
				{
					IdentityReference identityReference = this[j];
					Type type2 = identityReference.GetType();
					if (type2 != targetType)
					{
						if (type2 == typeof(SecurityIdentifier))
						{
							identityReferenceCollection.Add(identityReference);
						}
						else
						{
							if (type2 != typeof(NTAccount))
							{
								throw new SystemException();
							}
							identityReferenceCollection2.Add(identityReference);
						}
					}
				}
			}
			bool flag2 = false;
			IdentityReferenceCollection identityReferenceCollection4 = null;
			IdentityReferenceCollection identityReferenceCollection5 = null;
			if (num > 0)
			{
				identityReferenceCollection4 = SecurityIdentifier.Translate(identityReferenceCollection, targetType, out flag2);
				if (flag && (!forceSuccess || !flag2))
				{
					identityReferenceCollection3 = identityReferenceCollection4;
				}
			}
			if (num2 > 0)
			{
				identityReferenceCollection5 = NTAccount.Translate(identityReferenceCollection2, targetType, out flag2);
				if (flag && (!forceSuccess || !flag2))
				{
					identityReferenceCollection3 = identityReferenceCollection5;
				}
			}
			if (forceSuccess && flag2)
			{
				identityReferenceCollection3 = new IdentityReferenceCollection();
				if (identityReferenceCollection4 != null)
				{
					foreach (IdentityReference identityReference2 in identityReferenceCollection4)
					{
						if (identityReference2.GetType() != targetType)
						{
							identityReferenceCollection3.Add(identityReference2);
						}
					}
				}
				if (identityReferenceCollection5 != null)
				{
					foreach (IdentityReference identityReference3 in identityReferenceCollection5)
					{
						if (identityReference3.GetType() != targetType)
						{
							identityReferenceCollection3.Add(identityReference3);
						}
					}
				}
				throw new IdentityNotMappedException(Environment.GetResourceString("IdentityReference_IdentityNotMapped"), identityReferenceCollection3);
			}
			if (!flag)
			{
				num = 0;
				num2 = 0;
				identityReferenceCollection3 = new IdentityReferenceCollection(this.Identities.Count);
				for (int k = 0; k < this.Identities.Count; k++)
				{
					IdentityReference identityReference4 = this[k];
					Type type3 = identityReference4.GetType();
					if (type3 == targetType)
					{
						identityReferenceCollection3.Add(identityReference4);
					}
					else if (type3 == typeof(SecurityIdentifier))
					{
						identityReferenceCollection3.Add(identityReferenceCollection4[num++]);
					}
					else
					{
						if (type3 != typeof(NTAccount))
						{
							throw new SystemException();
						}
						identityReferenceCollection3.Add(identityReferenceCollection5[num2++]);
					}
				}
			}
			return identityReferenceCollection3;
		}

		// Token: 0x04002C62 RID: 11362
		private ArrayList _Identities;
	}
}
