using System;
using System.Collections;

namespace System.Web.Profile
{
	// Token: 0x02000162 RID: 354
	[Serializable]
	public sealed class ProfileInfoCollection : IEnumerable, ICollection
	{
		// Token: 0x060013F4 RID: 5108 RVA: 0x0003A6AB File Offset: 0x000388AB
		public ProfileInfoCollection()
		{
			this._Hashtable = new Hashtable(10, StringComparer.CurrentCultureIgnoreCase);
			this._ArrayList = new ArrayList();
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0003A6D0 File Offset: 0x000388D0
		public void Add(ProfileInfo profileInfo)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			if (profileInfo == null || profileInfo.UserName == null)
			{
				throw new ArgumentNullException("profileInfo");
			}
			this._Hashtable.Add(profileInfo.UserName, this._CurPos);
			this._ArrayList.Add(profileInfo);
			this._CurPos++;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0003A738 File Offset: 0x00038938
		public void Remove(string name)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			object obj = this._Hashtable[name];
			if (obj == null)
			{
				return;
			}
			this._Hashtable.Remove(name);
			this._ArrayList[(int)obj] = null;
			this._NumBlanks++;
		}

		// Token: 0x17000609 RID: 1545
		public ProfileInfo this[string name]
		{
			get
			{
				object obj = this._Hashtable[name];
				if (obj == null)
				{
					return null;
				}
				return this._ArrayList[(int)obj] as ProfileInfo;
			}
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0003A7C5 File Offset: 0x000389C5
		public IEnumerator GetEnumerator()
		{
			this.DoCompact();
			return this._ArrayList.GetEnumerator();
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0003A7D8 File Offset: 0x000389D8
		public void SetReadOnly()
		{
			if (this._ReadOnly)
			{
				return;
			}
			this._ReadOnly = true;
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0003A7EA File Offset: 0x000389EA
		public void Clear()
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			this._Hashtable.Clear();
			this._ArrayList.Clear();
			this._CurPos = 0;
			this._NumBlanks = 0;
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x0003A81E File Offset: 0x00038A1E
		public int Count
		{
			get
			{
				return this._Hashtable.Count;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0003A82B File Offset: 0x00038A2B
		public void CopyTo(Array array, int index)
		{
			this.DoCompact();
			this._ArrayList.CopyTo(array, index);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0003A82B File Offset: 0x00038A2B
		public void CopyTo(ProfileInfo[] array, int index)
		{
			this.DoCompact();
			this._ArrayList.CopyTo(array, index);
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0003A840 File Offset: 0x00038A40
		private void DoCompact()
		{
			if (this._NumBlanks < 1)
			{
				return;
			}
			ArrayList arrayList = new ArrayList(this._CurPos - this._NumBlanks);
			int num = -1;
			for (int i = 0; i < this._CurPos; i++)
			{
				if (this._ArrayList[i] != null)
				{
					arrayList.Add(this._ArrayList[i]);
				}
				else if (num == -1)
				{
					num = i;
				}
			}
			this._NumBlanks = 0;
			this._ArrayList = arrayList;
			this._CurPos = this._ArrayList.Count;
			for (int j = num; j < this._CurPos; j++)
			{
				ProfileInfo profileInfo = this._ArrayList[j] as ProfileInfo;
				this._Hashtable[profileInfo.UserName] = j;
			}
		}

		// Token: 0x0400150E RID: 5390
		private Hashtable _Hashtable;

		// Token: 0x0400150F RID: 5391
		private ArrayList _ArrayList;

		// Token: 0x04001510 RID: 5392
		private bool _ReadOnly;

		// Token: 0x04001511 RID: 5393
		private int _CurPos;

		// Token: 0x04001512 RID: 5394
		private int _NumBlanks;
	}
}
