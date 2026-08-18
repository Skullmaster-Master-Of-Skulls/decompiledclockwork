using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x0200012F RID: 303
	public class BaseCollection : MarshalByRefObject, ICollection, IEnumerable
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0001E5DE File Offset: 0x0001C7DE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001E5EB File Offset: 0x0001C7EB
		public void CopyTo(Array ar, int index)
		{
			this.List.CopyTo(ar, index);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001E5FA File Offset: 0x0001C7FA
		public IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00011A20 File Offset: 0x0000FC20
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x00011A20 File Offset: 0x0000FC20
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00006C59 File Offset: 0x00004E59
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x00015ECC File Offset: 0x000140CC
		protected virtual ArrayList List
		{
			get
			{
				return null;
			}
		}
	}
}
