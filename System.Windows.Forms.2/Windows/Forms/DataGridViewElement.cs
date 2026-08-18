using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020001D5 RID: 469
	public class DataGridViewElement
	{
		// Token: 0x0600207E RID: 8318 RVA: 0x0009BA93 File Offset: 0x00099C93
		public DataGridViewElement()
		{
			this.state = DataGridViewElementStates.Visible;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x0009BAA3 File Offset: 0x00099CA3
		internal DataGridViewElement(DataGridViewElement dgveTemplate)
		{
			this.state = (dgveTemplate.State & (DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.ResizableSet | DataGridViewElementStates.Visible));
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x0009BABA File Offset: 0x00099CBA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual DataGridViewElementStates State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (set) Token: 0x06002081 RID: 8321 RVA: 0x0009BAC2 File Offset: 0x00099CC2
		internal DataGridViewElementStates StateInternal
		{
			set
			{
				this.state = value;
			}
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0009BACB File Offset: 0x00099CCB
		internal bool StateIncludes(DataGridViewElementStates elementState)
		{
			return (this.State & elementState) == elementState;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x0009BAD8 File Offset: 0x00099CD8
		internal bool StateExcludes(DataGridViewElementStates elementState)
		{
			return (this.State & elementState) == DataGridViewElementStates.None;
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x0009BAE5 File Offset: 0x00099CE5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataGridView DataGridView
		{
			get
			{
				return this.dataGridView;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (set) Token: 0x06002085 RID: 8325 RVA: 0x0009BAED File Offset: 0x00099CED
		internal DataGridView DataGridViewInternal
		{
			set
			{
				if (this.DataGridView != value)
				{
					this.dataGridView = value;
					this.OnDataGridViewChanged();
				}
			}
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnDataGridViewChanged()
		{
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x0009BB05 File Offset: 0x00099D05
		protected void RaiseCellClick(DataGridViewCellEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.OnCellClickInternal(e);
			}
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x0009BB1B File Offset: 0x00099D1B
		protected void RaiseCellContentClick(DataGridViewCellEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.OnCellContentClickInternal(e);
			}
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x0009BB31 File Offset: 0x00099D31
		protected void RaiseCellContentDoubleClick(DataGridViewCellEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.OnCellContentDoubleClickInternal(e);
			}
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x0009BB47 File Offset: 0x00099D47
		protected void RaiseCellValueChanged(DataGridViewCellEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.OnCellValueChangedInternal(e);
			}
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x0009BB5D File Offset: 0x00099D5D
		protected void RaiseDataError(DataGridViewDataErrorEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.OnDataErrorInternal(e);
			}
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x0009BB73 File Offset: 0x00099D73
		protected void RaiseMouseWheel(MouseEventArgs e)
		{
			if (this.dataGridView != null)
			{
				this.dataGridView.OnMouseWheelInternal(e);
			}
		}

		// Token: 0x04000DBE RID: 3518
		private DataGridViewElementStates state;

		// Token: 0x04000DBF RID: 3519
		private DataGridView dataGridView;
	}
}
