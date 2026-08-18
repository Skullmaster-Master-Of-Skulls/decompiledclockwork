using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Globalization;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002BE RID: 702
	internal class DataGridViewColumnDesigner : ComponentDesigner
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x000A8504 File Offset: 0x000A6704
		// (set) Token: 0x06001BDD RID: 7133 RVA: 0x000A8538 File Offset: 0x000A6738
		private string Name
		{
			get
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)base.Component;
				if (dataGridViewColumn.Site != null)
				{
					return dataGridViewColumn.Site.Name;
				}
				return dataGridViewColumn.Name;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)base.Component;
				if (dataGridViewColumn == null)
				{
					return;
				}
				if (string.Compare(value, dataGridViewColumn.Name, false, CultureInfo.InvariantCulture) == 0)
				{
					return;
				}
				DataGridView dataGridView = dataGridViewColumn.DataGridView;
				IDesignerHost designerHost = null;
				IContainer container = null;
				INameCreationService nameCreationService = null;
				if (dataGridView != null && dataGridView.Site != null)
				{
					designerHost = (dataGridView.Site.GetService(typeof(IDesignerHost)) as IDesignerHost);
					nameCreationService = (dataGridView.Site.GetService(typeof(INameCreationService)) as INameCreationService);
				}
				if (designerHost != null)
				{
					container = designerHost.Container;
				}
				string empty = string.Empty;
				if (dataGridView != null && !DataGridViewAddColumnDialog.ValidName(value, dataGridView.Columns, container, nameCreationService, (this.liveDataGridView != null) ? this.liveDataGridView.Columns : null, true, out empty))
				{
					if (dataGridView != null && dataGridView.Site != null)
					{
						IUIService uiService = (IUIService)dataGridView.Site.GetService(typeof(IUIService));
						DataGridViewDesigner.ShowErrorDialog(uiService, empty, this.liveDataGridView);
					}
					return;
				}
				if ((designerHost == null || (designerHost != null && !designerHost.Loading)) && base.Component.Site != null)
				{
					base.Component.Site.Name = value;
				}
				dataGridViewColumn.Name = value;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x000A866C File Offset: 0x000A686C
		public DataGridView LiveDataGridView
		{
			set
			{
				this.liveDataGridView = value;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x000A8675 File Offset: 0x000A6875
		// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x000A867D File Offset: 0x000A687D
		private bool UserAddedColumn
		{
			get
			{
				return this.userAddedColumn;
			}
			set
			{
				this.userAddedColumn = value;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x000A8688 File Offset: 0x000A6888
		// (set) Token: 0x06001BE2 RID: 7138 RVA: 0x000A86A8 File Offset: 0x000A68A8
		private int Width
		{
			get
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)base.Component;
				return dataGridViewColumn.Width;
			}
			set
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)base.Component;
				value = Math.Max(dataGridViewColumn.MinimumWidth, value);
				dataGridViewColumn.Width = value;
			}
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000A86D8 File Offset: 0x000A68D8
		public override void Initialize(IComponent component)
		{
			this.initializing = true;
			base.Initialize(component);
			if (component.Site != null)
			{
				this.selectionService = (this.GetService(typeof(ISelectionService)) as ISelectionService);
				this.behaviorService = (this.GetService(typeof(BehaviorService)) as BehaviorService);
				if (this.behaviorService != null && this.selectionService != null)
				{
					this.behavior = new DataGridViewColumnDesigner.FilterCutCopyPasteDeleteBehavior(true, this.behaviorService);
					this.UpdateBehavior();
					this.selectionService.SelectionChanged += this.selectionService_SelectionChanged;
				}
			}
			this.initializing = false;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000A8777 File Offset: 0x000A6977
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.PopBehavior();
				if (this.selectionService != null)
				{
					this.selectionService.SelectionChanged -= this.selectionService_SelectionChanged;
				}
				this.selectionService = null;
				this.behaviorService = null;
			}
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x000A87B0 File Offset: 0x000A69B0
		private void PushBehavior()
		{
			if (!this.behaviorPushed)
			{
				try
				{
					this.behaviorService.PushBehavior(this.behavior);
				}
				finally
				{
					this.behaviorPushed = true;
				}
			}
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000A87F0 File Offset: 0x000A69F0
		private void PopBehavior()
		{
			if (this.behaviorPushed)
			{
				try
				{
					this.behaviorService.PopBehavior(this.behavior);
				}
				finally
				{
					this.behaviorPushed = false;
				}
			}
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000A8834 File Offset: 0x000A6A34
		private void UpdateBehavior()
		{
			if (this.selectionService != null)
			{
				if (this.selectionService.PrimarySelection != null && base.Component.Equals(this.selectionService.PrimarySelection))
				{
					this.PushBehavior();
					return;
				}
				this.PopBehavior();
			}
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000A8870 File Offset: 0x000A6A70
		private void selectionService_SelectionChanged(object sender, EventArgs e)
		{
			this.UpdateBehavior();
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x000A8878 File Offset: 0x000A6A78
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["Width"];
			if (propertyDescriptor != null)
			{
				properties["Width"] = TypeDescriptor.CreateProperty(typeof(DataGridViewColumnDesigner), propertyDescriptor, new Attribute[0]);
			}
			propertyDescriptor = (PropertyDescriptor)properties["Name"];
			if (propertyDescriptor != null)
			{
				if (base.Component.Site == null)
				{
					properties["Name"] = TypeDescriptor.CreateProperty(typeof(DataGridViewColumnDesigner), propertyDescriptor, new Attribute[]
					{
						BrowsableAttribute.Yes,
						CategoryAttribute.Design,
						new DescriptionAttribute(SR.GetString("DesignerPropName")),
						new ParenthesizePropertyNameAttribute(true)
					});
				}
				else
				{
					properties["Name"] = TypeDescriptor.CreateProperty(typeof(DataGridViewColumnDesigner), propertyDescriptor, new Attribute[]
					{
						new ParenthesizePropertyNameAttribute(true)
					});
				}
			}
			properties["UserAddedColumn"] = TypeDescriptor.CreateProperty(typeof(DataGridViewColumnDesigner), "UserAddedColumn", typeof(bool), new Attribute[]
			{
				new DefaultValueAttribute(false),
				BrowsableAttribute.No,
				DesignOnlyAttribute.Yes
			});
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x000A89A8 File Offset: 0x000A6BA8
		private bool ShouldSerializeWidth()
		{
			DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)base.Component;
			return dataGridViewColumn.InheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.Fill && dataGridViewColumn.Width != 100;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000A89DC File Offset: 0x000A6BDC
		private bool ShouldSerializeName()
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost == null)
			{
				return false;
			}
			if (!this.initializing)
			{
				return base.ShadowProperties.ShouldSerializeValue("Name", null);
			}
			return base.Component != designerHost.RootComponent;
		}

		// Token: 0x040016B1 RID: 5809
		private const int DATAGRIDVIEWCOLUMN_defaultWidth = 100;

		// Token: 0x040016B2 RID: 5810
		private bool userAddedColumn;

		// Token: 0x040016B3 RID: 5811
		private bool initializing;

		// Token: 0x040016B4 RID: 5812
		private BehaviorService behaviorService;

		// Token: 0x040016B5 RID: 5813
		private ISelectionService selectionService;

		// Token: 0x040016B6 RID: 5814
		private DataGridViewColumnDesigner.FilterCutCopyPasteDeleteBehavior behavior;

		// Token: 0x040016B7 RID: 5815
		private bool behaviorPushed;

		// Token: 0x040016B8 RID: 5816
		private DataGridView liveDataGridView;

		// Token: 0x02000554 RID: 1364
		public class FilterCutCopyPasteDeleteBehavior : Behavior
		{
			// Token: 0x0600315B RID: 12635 RVA: 0x0010CF8B File Offset: 0x0010B18B
			public FilterCutCopyPasteDeleteBehavior(bool callParentBehavior, BehaviorService behaviorService) : base(callParentBehavior, behaviorService)
			{
			}

			// Token: 0x0600315C RID: 12636 RVA: 0x0010CF98 File Offset: 0x0010B198
			public override MenuCommand FindCommand(CommandID commandId)
			{
				if (commandId.ID == StandardCommands.Copy.ID && commandId.Guid == StandardCommands.Copy.Guid)
				{
					return new MenuCommand(new EventHandler(this.handler), StandardCommands.Copy)
					{
						Enabled = false
					};
				}
				if (commandId.ID == StandardCommands.Paste.ID && commandId.Guid == StandardCommands.Paste.Guid)
				{
					return new MenuCommand(new EventHandler(this.handler), StandardCommands.Paste)
					{
						Enabled = false
					};
				}
				if (commandId.ID == StandardCommands.Delete.ID && commandId.Guid == StandardCommands.Delete.Guid)
				{
					return new MenuCommand(new EventHandler(this.handler), StandardCommands.Delete)
					{
						Enabled = false
					};
				}
				if (commandId.ID == StandardCommands.Cut.ID && commandId.Guid == StandardCommands.Cut.Guid)
				{
					return new MenuCommand(new EventHandler(this.handler), StandardCommands.Cut)
					{
						Enabled = false
					};
				}
				return base.FindCommand(commandId);
			}

			// Token: 0x0600315D RID: 12637 RVA: 0x00003937 File Offset: 0x00001B37
			private void handler(object sender, EventArgs e)
			{
			}
		}
	}
}
