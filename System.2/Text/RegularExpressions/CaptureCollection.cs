using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200068D RID: 1677
	[__DynamicallyInvokable]
	[Serializable]
	public class CaptureCollection : ICollection, IEnumerable
	{
		// Token: 0x06003DFB RID: 15867 RVA: 0x000FE05D File Offset: 0x000FC25D
		internal CaptureCollection(Group group)
		{
			this._group = group;
			this._capcount = this._group._capcount;
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06003DFC RID: 15868 RVA: 0x000FE07D File Offset: 0x000FC27D
		public object SyncRoot
		{
			get
			{
				return this._group;
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06003DFD RID: 15869 RVA: 0x000FE085 File Offset: 0x000FC285
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06003DFE RID: 15870 RVA: 0x000FE088 File Offset: 0x000FC288
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06003DFF RID: 15871 RVA: 0x000FE08B File Offset: 0x000FC28B
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this._capcount;
			}
		}

		// Token: 0x17000EB6 RID: 3766
		[__DynamicallyInvokable]
		public Capture this[int i]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetCapture(i);
			}
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x000FE09C File Offset: 0x000FC29C
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

		// Token: 0x06003E02 RID: 15874 RVA: 0x000FE0DC File Offset: 0x000FC2DC
		[__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			return new CaptureEnumerator(this);
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x000FE0E4 File Offset: 0x000FC2E4
		internal Capture GetCapture(int i)
		{
			if (i == this._capcount - 1 && i >= 0)
			{
				return this._group;
			}
			if (i >= this._capcount || i < 0)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			if (this._captures == null)
			{
				this._captures = new Capture[this._capcount];
				for (int j = 0; j < this._capcount - 1; j++)
				{
					this._captures[j] = new Capture(this._group._text, this._group._caps[j * 2], this._group._caps[j * 2 + 1]);
				}
			}
			return this._captures[i];
		}

		// Token: 0x04002D05 RID: 11525
		internal Group _group;

		// Token: 0x04002D06 RID: 11526
		internal int _capcount;

		// Token: 0x04002D07 RID: 11527
		internal Capture[] _captures;
	}
}
