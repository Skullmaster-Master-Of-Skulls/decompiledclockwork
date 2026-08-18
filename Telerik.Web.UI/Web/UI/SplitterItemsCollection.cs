using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000FC4 RID: 4036
	public class SplitterItemsCollection : ControlCollection, IStateManager
	{
		// Token: 0x06009CC8 RID: 40136 RVA: 0x0022EAC2 File Offset: 0x0022CCC2
		public SplitterItemsCollection(SplitterItemsContainer container) : base(container)
		{
			this._container = container;
		}

		// Token: 0x170031A7 RID: 12711
		// (get) Token: 0x06009CC9 RID: 40137 RVA: 0x0022EAD2 File Offset: 0x0022CCD2
		internal SplitterItemsContainer Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x06009CCA RID: 40138 RVA: 0x0022EADA File Offset: 0x0022CCDA
		public void Add(SplitterItem item)
		{
			this.AddAt(this.Container.Controls.Count, item, false);
		}

		// Token: 0x06009CCB RID: 40139 RVA: 0x0022EAF4 File Offset: 0x0022CCF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Add(Control child)
		{
			SplitterItem splitterItem = child as SplitterItem;
			if (splitterItem == null)
			{
				throw new ArgumentException("SplitterItemsCollection must contain SplitterItem objects");
			}
			this.Add(splitterItem);
		}

		// Token: 0x06009CCC RID: 40140 RVA: 0x0022EB1D File Offset: 0x0022CD1D
		public void Insert(int index, SplitterItem item)
		{
			this.AddAt(index, item, true);
		}

		// Token: 0x06009CCD RID: 40141 RVA: 0x0022EB28 File Offset: 0x0022CD28
		public void AddAt(int index, SplitterItem item)
		{
			this.Insert(index, item);
		}

		// Token: 0x06009CCE RID: 40142 RVA: 0x0022EB34 File Offset: 0x0022CD34
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void AddAt(int index, Control child)
		{
			SplitterItem splitterItem = child as SplitterItem;
			if (splitterItem == null)
			{
				throw new ArgumentException("SplitterItemsCollection must contain SplitterItem objects");
			}
			this.Insert(index, splitterItem);
		}

		// Token: 0x06009CCF RID: 40143 RVA: 0x0022EB5E File Offset: 0x0022CD5E
		public void AddAt(int index, SplitterItem item, bool regenerateIndexes)
		{
			if (index < this.Container.Controls.Count)
			{
				this.ShiftItemsIndex(index, 1);
			}
			base.AddAt(index, item);
			item.Index = index;
		}

		// Token: 0x06009CD0 RID: 40144 RVA: 0x0022EB8C File Offset: 0x0022CD8C
		private void ShiftItemsIndex(int index, int shiftOp)
		{
			for (int i = index; i < this.Container.Controls.Count; i++)
			{
				SplitterItem splitterItem = (SplitterItem)this.Container.Controls[i];
				splitterItem.Index += shiftOp;
			}
		}

		// Token: 0x06009CD1 RID: 40145 RVA: 0x0022EBD9 File Offset: 0x0022CDD9
		public int IndexOf(SplitterItem item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x06009CD2 RID: 40146 RVA: 0x0022EBE2 File Offset: 0x0022CDE2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int IndexOf(Control value)
		{
			return base.IndexOf(value);
		}

		// Token: 0x06009CD3 RID: 40147 RVA: 0x0022EBEB File Offset: 0x0022CDEB
		public bool Contains(SplitterItem item)
		{
			return base.Contains(item);
		}

		// Token: 0x06009CD4 RID: 40148 RVA: 0x0022EBF4 File Offset: 0x0022CDF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Contains(Control c)
		{
			return base.Contains(c);
		}

		// Token: 0x06009CD5 RID: 40149 RVA: 0x0022EBFD File Offset: 0x0022CDFD
		public void Remove(SplitterItem item)
		{
			this.RemoveAt(item.Index);
		}

		// Token: 0x06009CD6 RID: 40150 RVA: 0x0022EC0C File Offset: 0x0022CE0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Remove(Control value)
		{
			SplitterItem splitterItem = value as SplitterItem;
			if (splitterItem == null)
			{
				throw new ArgumentException("SplitterItemsCollection contains SplitterItem objects");
			}
			this.Remove(splitterItem);
		}

		// Token: 0x06009CD7 RID: 40151 RVA: 0x0022EC35 File Offset: 0x0022CE35
		public new void RemoveAt(int index)
		{
			if (index < this.Container.Controls.Count)
			{
				this.ShiftItemsIndex(index, -1);
			}
			base.RemoveAt(index);
		}

		// Token: 0x06009CD8 RID: 40152 RVA: 0x0022EC59 File Offset: 0x0022CE59
		public new void Clear()
		{
			base.Clear();
		}

		// Token: 0x170031A8 RID: 12712
		public SplitterItem this[int index]
		{
			get
			{
				return (SplitterItem)base[index];
			}
		}

		// Token: 0x170031A9 RID: 12713
		// (get) Token: 0x06009CDA RID: 40154 RVA: 0x0022EC6F File Offset: 0x0022CE6F
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x06009CDB RID: 40155 RVA: 0x0022EC78 File Offset: 0x0022CE78
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int num = Math.Min(this.Container.Controls.Count, array.Length);
				for (int i = 0; i < num; i++)
				{
					((IStateManager)this[i]).LoadViewState(array[i]);
				}
				for (int j = this.Container.Controls.Count; j < array.Length; j++)
				{
					if (array[j] != null)
					{
						RadPane radPane = new RadPane();
						this.Add(radPane);
						((IStateManager)radPane).LoadViewState(array[j]);
					}
				}
			}
		}

		// Token: 0x06009CDC RID: 40156 RVA: 0x0022ED0C File Offset: 0x0022CF0C
		object IStateManager.SaveViewState()
		{
			int count = this.Container.Controls.Count;
			object[] array = new object[count];
			bool flag = false;
			for (int i = 0; i < count; i++)
			{
				array[i] = ((IStateManager)this.Container.Controls[i]).SaveViewState();
				if (array[i] != null)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			return array;
		}

		// Token: 0x06009CDD RID: 40157 RVA: 0x0022ED6A File Offset: 0x0022CF6A
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
		}

		// Token: 0x04002C1B RID: 11291
		private bool _isTrackingViewState;

		// Token: 0x04002C1C RID: 11292
		private readonly SplitterItemsContainer _container;
	}
}
