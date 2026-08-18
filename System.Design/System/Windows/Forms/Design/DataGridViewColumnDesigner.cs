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
	// Token: 0x020001E8 RID: 488
	internal class DataGridViewColumnDesigner : ComponentDesigner
	{
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0006012C File Offset: 0x0005F12C
		// (set) Token: 0x060012D3 RID: 4819 RVA: 0x00060160 File Offset: 0x0005F160
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

		// Token: 0x170002FB RID: 763
		// (set) Token: 0x060012D4 RID: 4820 RVA: 0x00060294 File Offset: 0x0005F294
		public DataGridView LiveDataGridView
		{
			set
			{
				this.liveDataGridView = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0006029D File Offset: 0x0005F29D
		// (set) Token: 0x060012D6 RID: 4822 RVA: 0x000602A5 File Offset: 0x0005F2A5
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

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x000602B0 File Offset: 0x0005F2B0
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x000602D0 File Offset: 0x0005F2D0
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

		// Token: 0x060012D9 RID: 4825 RVA: 0x00060300 File Offset: 0x0005F300
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

		// Token: 0x060012DA RID: 4826 RVA: 0x0006039F File Offset: 0x0005F39F
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

		// Token: 0x060012DB RID: 4827 RVA: 0x000603D8 File Offset: 0x0005F3D8
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

		// Token: 0x060012DC RID: 4828 RVA: 0x00060418 File Offset: 0x0005F418
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

		// Token: 0x060012DD RID: 4829 RVA: 0x0006045C File Offset: 0x0005F45C
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

		// Token: 0x060012DE RID: 4830 RVA: 0x00060498 File Offset: 0x0005F498
		private void selectionService_SelectionChanged(object sender, EventArgs e)
		{
			this.UpdateBehavior();
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x000604A0 File Offset: 0x0005F4A0
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

		// Token: 0x060012E0 RID: 4832 RVA: 0x000605D4 File Offset: 0x0005F5D4
		private bool ShouldSerializeWidth()
		{
			DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)base.Component;
			return dataGridViewColumn.InheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.Fill && dataGridViewColumn.Width != 100;
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00060608 File Offset: 0x0005F608
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

		// Token: 0x0400117B RID: 4475
		private const int DATAGRIDVIEWCOLUMN_defaultWidth = 100;

		// Token: 0x0400117C RID: 4476
		private bool userAddedColumn;

		// Token: 0x0400117D RID: 4477
		private bool initializing;

		// Token: 0x0400117E RID: 4478
		private BehaviorService behaviorService;

		// Token: 0x0400117F RID: 4479
		private ISelectionService selectionService;

		// Token: 0x04001180 RID: 4480
		private DataGridViewColumnDesigner.FilterCutCopyPasteDeleteBehavior behavior;

		// Token: 0x04001181 RID: 4481
		private bool behaviorPushed;

		// Token: 0x04001182 RID: 4482
		private DataGridView liveDataGridView;

		// Token: 0x020001E9 RID: 489
		public class FilterCutCopyPasteDeleteBehavior : Behavior
		{
			// Token: 0x060012E3 RID: 4835 RVA: 0x00060663 File Offset: 0x0005F663
			public FilterCutCopyPasteDeleteBehavior(bool callParentBehavior, BehaviorService behaviorService) : base(callParentBehavior, behaviorService)
			{
			}

			// Token: 0x060012E4 RID: 4836 RVA: 0x00060670 File Offset: 0x0005F670
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

			// Token: 0x060012E5 RID: 4837 RVA: 0x000607A8 File Offset: 0x0005F7A8
			private void handler(object sender, EventArgs e)
			{
			}
		}
	}
}
