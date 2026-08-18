using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005DD RID: 1501
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignerCollection : ICollection, IEnumerable
	{
		// Token: 0x060037C9 RID: 14281 RVA: 0x000F11CE File Offset: 0x000EF3CE
		public DesignerCollection(IDesignerHost[] designers)
		{
			if (designers != null)
			{
				this.designers = new ArrayList(designers);
				return;
			}
			this.designers = new ArrayList();
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000F11F1 File Offset: 0x000EF3F1
		public DesignerCollection(IList designers)
		{
			this.designers = designers;
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x060037CB RID: 14283 RVA: 0x000F1200 File Offset: 0x000EF400
		public int Count
		{
			get
			{
				return this.designers.Count;
			}
		}

		// Token: 0x17000D6A RID: 3434
		public virtual IDesignerHost this[int index]
		{
			get
			{
				return (IDesignerHost)this.designers[index];
			}
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000F1220 File Offset: 0x000EF420
		public IEnumerator GetEnumerator()
		{
			return this.designers.GetEnumerator();
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x060037CE RID: 14286 RVA: 0x000F122D File Offset: 0x000EF42D
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060037CF RID: 14287 RVA: 0x000F1235 File Offset: 0x000EF435
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x000F1238 File Offset: 0x000EF438
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000F123B File Offset: 0x000EF43B
		void ICollection.CopyTo(Array array, int index)
		{
			this.designers.CopyTo(array, index);
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000F124A File Offset: 0x000EF44A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002B07 RID: 11015
		private IList designers;
	}
}
