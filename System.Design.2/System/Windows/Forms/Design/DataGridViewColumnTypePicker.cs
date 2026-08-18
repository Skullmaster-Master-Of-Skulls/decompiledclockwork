using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002C0 RID: 704
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	internal class DataGridViewColumnTypePicker : ContainerControl
	{
		// Token: 0x06001BF1 RID: 7153 RVA: 0x000A8B00 File Offset: 0x000A6D00
		public DataGridViewColumnTypePicker()
		{
			this.typesListBox = new ListBox();
			base.Size = this.typesListBox.Size;
			this.typesListBox.Dock = DockStyle.Fill;
			this.typesListBox.Sorted = true;
			this.typesListBox.HorizontalScrollbar = true;
			this.typesListBox.SelectedIndexChanged += this.typesListBox_SelectedIndexChanged;
			base.Controls.Add(this.typesListBox);
			this.BackColor = SystemColors.Control;
			base.ActiveControl = this.typesListBox;
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x000A8B92 File Offset: 0x000A6D92
		public Type SelectedType
		{
			get
			{
				return this.selectedType;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x000A8B9C File Offset: 0x000A6D9C
		private int PreferredWidth
		{
			get
			{
				int num = 0;
				Graphics graphics = this.typesListBox.CreateGraphics();
				try
				{
					for (int i = 0; i < this.typesListBox.Items.Count; i++)
					{
						DataGridViewColumnTypePicker.ListBoxItem listBoxItem = (DataGridViewColumnTypePicker.ListBoxItem)this.typesListBox.Items[i];
						num = Math.Max(num, Size.Ceiling(graphics.MeasureString(listBoxItem.ToString(), this.typesListBox.Font)).Width);
					}
				}
				finally
				{
					graphics.Dispose();
				}
				return num;
			}
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x000A8C30 File Offset: 0x000A6E30
		private void CloseDropDown()
		{
			if (this.edSvc != null)
			{
				this.edSvc.CloseDropDown();
			}
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x000A8C45 File Offset: 0x000A6E45
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((BoundsSpecified.Width & specified) == BoundsSpecified.Width)
			{
				width = Math.Max(width, 100);
			}
			if ((BoundsSpecified.Height & specified) == BoundsSpecified.Height)
			{
				height = Math.Max(height, 90);
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x000A8C78 File Offset: 0x000A6E78
		public void Start(IWindowsFormsEditorService edSvc, ITypeDiscoveryService discoveryService, Type defaultType)
		{
			this.edSvc = edSvc;
			this.typesListBox.Items.Clear();
			ICollection collection = DesignerUtils.FilterGenericTypes(discoveryService.GetTypes(DataGridViewColumnTypePicker.dataGridViewColumnType, false));
			foreach (object obj in collection)
			{
				Type type = (Type)obj;
				if (!(type == DataGridViewColumnTypePicker.dataGridViewColumnType) && !type.IsAbstract && (type.IsPublic || type.IsNestedPublic))
				{
					DataGridViewColumnDesignTimeVisibleAttribute dataGridViewColumnDesignTimeVisibleAttribute = TypeDescriptor.GetAttributes(type)[typeof(DataGridViewColumnDesignTimeVisibleAttribute)] as DataGridViewColumnDesignTimeVisibleAttribute;
					if (dataGridViewColumnDesignTimeVisibleAttribute == null || dataGridViewColumnDesignTimeVisibleAttribute.Visible)
					{
						this.typesListBox.Items.Add(new DataGridViewColumnTypePicker.ListBoxItem(type));
					}
				}
			}
			this.typesListBox.SelectedIndex = this.TypeToSelectedIndex(defaultType);
			this.selectedType = null;
			base.Width = Math.Max(base.Width, this.PreferredWidth + SystemInformation.VerticalScrollBarWidth * 2);
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x000A8D8C File Offset: 0x000A6F8C
		private void typesListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.selectedType = ((DataGridViewColumnTypePicker.ListBoxItem)this.typesListBox.SelectedItem).ColumnType;
			this.edSvc.CloseDropDown();
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x000A8DB4 File Offset: 0x000A6FB4
		private int TypeToSelectedIndex(Type type)
		{
			for (int i = 0; i < this.typesListBox.Items.Count; i++)
			{
				if (type == ((DataGridViewColumnTypePicker.ListBoxItem)this.typesListBox.Items[i]).ColumnType)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x040016BA RID: 5818
		private ListBox typesListBox;

		// Token: 0x040016BB RID: 5819
		private Type selectedType;

		// Token: 0x040016BC RID: 5820
		private IWindowsFormsEditorService edSvc;

		// Token: 0x040016BD RID: 5821
		private static Type dataGridViewColumnType = typeof(DataGridViewColumn);

		// Token: 0x040016BE RID: 5822
		private const int MinimumHeight = 90;

		// Token: 0x040016BF RID: 5823
		private const int MinimumWidth = 100;

		// Token: 0x02000555 RID: 1365
		private class ListBoxItem
		{
			// Token: 0x0600315E RID: 12638 RVA: 0x0010D0D0 File Offset: 0x0010B2D0
			public ListBoxItem(Type columnType)
			{
				this.columnType = columnType;
			}

			// Token: 0x0600315F RID: 12639 RVA: 0x0010D0DF File Offset: 0x0010B2DF
			public override string ToString()
			{
				return this.columnType.Name;
			}

			// Token: 0x1700098F RID: 2447
			// (get) Token: 0x06003160 RID: 12640 RVA: 0x0010D0EC File Offset: 0x0010B2EC
			public Type ColumnType
			{
				get
				{
					return this.columnType;
				}
			}

			// Token: 0x0400212E RID: 8494
			private Type columnType;
		}
	}
}
