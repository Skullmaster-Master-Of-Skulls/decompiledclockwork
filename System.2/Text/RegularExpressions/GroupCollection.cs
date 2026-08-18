using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000699 RID: 1689
	[__DynamicallyInvokable]
	[Serializable]
	public class GroupCollection : ICollection, IEnumerable
	{
		// Token: 0x06003ED5 RID: 16085 RVA: 0x00105AA9 File Offset: 0x00103CA9
		internal GroupCollection(Match match, Hashtable caps)
		{
			this._match = match;
			this._captureMap = caps;
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06003ED6 RID: 16086 RVA: 0x00105ABF File Offset: 0x00103CBF
		public object SyncRoot
		{
			get
			{
				return this._match;
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06003ED7 RID: 16087 RVA: 0x00105AC7 File Offset: 0x00103CC7
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06003ED8 RID: 16088 RVA: 0x00105ACA File Offset: 0x00103CCA
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06003ED9 RID: 16089 RVA: 0x00105ACD File Offset: 0x00103CCD
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this._match._matchcount.Length;
			}
		}

		// Token: 0x17000ECB RID: 3787
		[__DynamicallyInvokable]
		public Group this[int groupnum]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetGroup(groupnum);
			}
		}

		// Token: 0x17000ECC RID: 3788
		[__DynamicallyInvokable]
		public Group this[string groupname]
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._match._regex == null)
				{
					return Group._emptygroup;
				}
				return this.GetGroup(this._match._regex.GroupNumberFromName(groupname));
			}
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x00105B14 File Offset: 0x00103D14
		internal Group GetGroup(int groupnum)
		{
			if (this._captureMap != null)
			{
				object obj = this._captureMap[groupnum];
				if (obj == null)
				{
					return Group._emptygroup;
				}
				return this.GetGroupImpl((int)obj);
			}
			else
			{
				if (groupnum >= this._match._matchcount.Length || groupnum < 0)
				{
					return Group._emptygroup;
				}
				return this.GetGroupImpl(groupnum);
			}
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x00105B74 File Offset: 0x00103D74
		internal Group GetGroupImpl(int groupnum)
		{
			if (groupnum == 0)
			{
				return this._match;
			}
			if (this._groups == null)
			{
				this._groups = new Group[this._match._matchcount.Length - 1];
				for (int i = 0; i < this._groups.Length; i++)
				{
					string name = this._match._regex.GroupNameFromNumber(i + 1);
					this._groups[i] = new Group(this._match._text, this._match._matches[i + 1], this._match._matchcount[i + 1], name);
				}
			}
			return this._groups[groupnum - 1];
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x00105C18 File Offset: 0x00103E18
		public void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = arrayIndex;
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], num);
				num++;
			}
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x00105C58 File Offset: 0x00103E58
		[__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			return new GroupEnumerator(this);
		}

		// Token: 0x04002DE3 RID: 11747
		internal Match _match;

		// Token: 0x04002DE4 RID: 11748
		internal Hashtable _captureMap;

		// Token: 0x04002DE5 RID: 11749
		internal Group[] _groups;
	}
}
