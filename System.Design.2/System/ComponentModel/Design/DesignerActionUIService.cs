using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020001A8 RID: 424
	public sealed class DesignerActionUIService : IDisposable
	{
		// Token: 0x06000FAB RID: 4011 RVA: 0x00059AA8 File Offset: 0x00057CA8
		internal DesignerActionUIService(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
			if (serviceProvider != null)
			{
				this.serviceProvider = serviceProvider;
				IDesignerHost designerHost = (IDesignerHost)serviceProvider.GetService(typeof(IDesignerHost));
				designerHost.AddService(typeof(DesignerActionUIService), this);
				this.designerActionService = (serviceProvider.GetService(typeof(DesignerActionService)) as DesignerActionService);
			}
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00059B10 File Offset: 0x00057D10
		public void Dispose()
		{
			if (this.serviceProvider != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					designerHost.RemoveService(typeof(DesignerActionUIService));
				}
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000FAD RID: 4013 RVA: 0x00059B53 File Offset: 0x00057D53
		// (remove) Token: 0x06000FAE RID: 4014 RVA: 0x00059B6C File Offset: 0x00057D6C
		public event DesignerActionUIStateChangeEventHandler DesignerActionUIStateChange
		{
			add
			{
				this.designerActionUIStateChangedEventHandler = (DesignerActionUIStateChangeEventHandler)Delegate.Combine(this.designerActionUIStateChangedEventHandler, value);
			}
			remove
			{
				this.designerActionUIStateChangedEventHandler = (DesignerActionUIStateChangeEventHandler)Delegate.Remove(this.designerActionUIStateChangedEventHandler, value);
			}
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00059B85 File Offset: 0x00057D85
		public void HideUI(IComponent component)
		{
			this.OnDesignerActionUIStateChange(new DesignerActionUIStateChangeEventArgs(component, DesignerActionUIStateChangeType.Hide));
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00059B94 File Offset: 0x00057D94
		public void ShowUI(IComponent component)
		{
			this.OnDesignerActionUIStateChange(new DesignerActionUIStateChangeEventArgs(component, DesignerActionUIStateChangeType.Show));
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00059BA3 File Offset: 0x00057DA3
		public void Refresh(IComponent component)
		{
			this.OnDesignerActionUIStateChange(new DesignerActionUIStateChangeEventArgs(component, DesignerActionUIStateChangeType.Refresh));
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00059BB2 File Offset: 0x00057DB2
		private void OnDesignerActionUIStateChange(DesignerActionUIStateChangeEventArgs e)
		{
			if (this.designerActionUIStateChangedEventHandler != null)
			{
				this.designerActionUIStateChangedEventHandler(this, e);
			}
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00059BCC File Offset: 0x00057DCC
		public bool ShouldAutoShow(IComponent component)
		{
			if (this.serviceProvider != null)
			{
				DesignerOptionService designerOptionService = this.serviceProvider.GetService(typeof(DesignerOptionService)) as DesignerOptionService;
				if (designerOptionService != null)
				{
					PropertyDescriptor propertyDescriptor = designerOptionService.Options.Properties["ObjectBoundSmartTagAutoShow"];
					if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(bool) && !(bool)propertyDescriptor.GetValue(null))
					{
						return false;
					}
				}
			}
			if (this.designerActionService != null)
			{
				DesignerActionListCollection componentActions = this.designerActionService.GetComponentActions(component);
				if (componentActions != null && componentActions.Count > 0)
				{
					for (int i = 0; i < componentActions.Count; i++)
					{
						if (componentActions[i].AutoShow)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0400092A RID: 2346
		private DesignerActionUIStateChangeEventHandler designerActionUIStateChangedEventHandler;

		// Token: 0x0400092B RID: 2347
		private IServiceProvider serviceProvider;

		// Token: 0x0400092C RID: 2348
		private DesignerActionService designerActionService;
	}
}
