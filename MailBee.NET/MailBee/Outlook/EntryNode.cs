using System;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x020002CC RID: 716
	[Serializable]
	internal abstract class EntryNode : e1
	{
		// Token: 0x060018D4 RID: 6356 RVA: 0x0006F421 File Offset: 0x0006E421
		protected EntryNode() : this(null, null)
		{
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0006F42B File Offset: 0x0006E42B
		protected EntryNode(ed A_0, DirectoryNode A_1)
		{
			this._property = A_0;
			this._parent = A_1;
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x0006F441 File Offset: 0x0006E441
		public ed Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0006F449 File Offset: 0x0006E449
		protected bool IsRoot
		{
			get
			{
				return this._parent == null;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060018D8 RID: 6360
		protected abstract bool IsDeleteOK { get; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x0006F454 File Offset: 0x0006E454
		public string Name
		{
			get
			{
				return this._property.f();
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0006F461 File Offset: 0x0006E461
		public virtual bool IsDirectoryEntry
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0006F464 File Offset: 0x0006E464
		public virtual bool IsDocumentEntry
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x0006F467 File Offset: 0x0006E467
		public ig Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0006F470 File Offset: 0x0006E470
		public bool u()
		{
			bool result = false;
			if (!this.IsRoot && this.IsDeleteOK)
			{
				result = this._parent.a(this);
			}
			return result;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0006F4A0 File Offset: 0x0006E4A0
		public bool v(string A_0)
		{
			bool result = false;
			if (!this.IsRoot)
			{
				result = this._parent.a(this.Name, A_0);
			}
			return result;
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0006F4CB File Offset: 0x0006E4CB
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04001246 RID: 4678
		protected ed _property;

		// Token: 0x04001247 RID: 4679
		protected DirectoryNode _parent;
	}
}
