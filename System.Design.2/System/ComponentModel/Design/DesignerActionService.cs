using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x020001A5 RID: 421
	public class DesignerActionService : IDisposable
	{
		// Token: 0x06000F95 RID: 3989 RVA: 0x0005919C File Offset: 0x0005739C
		public DesignerActionService(IServiceProvider serviceProvider)
		{
			if (serviceProvider != null)
			{
				this.serviceProvider = serviceProvider;
				IDesignerHost designerHost = (IDesignerHost)serviceProvider.GetService(typeof(IDesignerHost));
				designerHost.AddService(typeof(DesignerActionService), this);
				IComponentChangeService componentChangeService = (IComponentChangeService)serviceProvider.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentRemoved += this.OnComponentRemoved;
				}
				this.selSvc = (ISelectionService)serviceProvider.GetService(typeof(ISelectionService));
				ISelectionService selectionService = this.selSvc;
			}
			this.designerActionLists = new Hashtable();
			this.componentToVerbsEventHookedUp = new Hashtable();
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000F96 RID: 3990 RVA: 0x00059243 File Offset: 0x00057443
		// (remove) Token: 0x06000F97 RID: 3991 RVA: 0x0005925C File Offset: 0x0005745C
		public event DesignerActionListsChangedEventHandler DesignerActionListsChanged
		{
			add
			{
				this.designerActionListsChanged = (DesignerActionListsChangedEventHandler)Delegate.Combine(this.designerActionListsChanged, value);
			}
			remove
			{
				this.designerActionListsChanged = (DesignerActionListsChangedEventHandler)Delegate.Remove(this.designerActionListsChanged, value);
			}
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00059278 File Offset: 0x00057478
		public void Add(IComponent comp, DesignerActionListCollection designerActionListCollection)
		{
			if (comp == null)
			{
				throw new ArgumentNullException("comp");
			}
			if (designerActionListCollection == null)
			{
				throw new ArgumentNullException("designerActionListCollection");
			}
			DesignerActionListCollection designerActionListCollection2 = (DesignerActionListCollection)this.designerActionLists[comp];
			if (designerActionListCollection2 != null)
			{
				designerActionListCollection2.AddRange(designerActionListCollection);
			}
			else
			{
				this.designerActionLists.Add(comp, designerActionListCollection);
			}
			this.OnDesignerActionListsChanged(new DesignerActionListsChangedEventArgs(comp, DesignerActionListsChangedType.ActionListsAdded, this.GetComponentActions(comp)));
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000592E0 File Offset: 0x000574E0
		public void Add(IComponent comp, DesignerActionList actionList)
		{
			this.Add(comp, new DesignerActionListCollection(actionList));
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x000592F0 File Offset: 0x000574F0
		public void Clear()
		{
			if (this.designerActionLists.Count == 0)
			{
				return;
			}
			ArrayList arrayList = new ArrayList(this.designerActionLists.Count);
			foreach (object obj in this.designerActionLists)
			{
				arrayList.Add(((DictionaryEntry)obj).Key);
			}
			this.designerActionLists.Clear();
			foreach (object obj2 in arrayList)
			{
				Component component = (Component)obj2;
				this.OnDesignerActionListsChanged(new DesignerActionListsChangedEventArgs(component, DesignerActionListsChangedType.ActionListsRemoved, this.GetComponentActions(component)));
			}
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x000593D4 File Offset: 0x000575D4
		public bool Contains(IComponent comp)
		{
			if (comp == null)
			{
				throw new ArgumentNullException("comp");
			}
			return this.designerActionLists.Contains(comp);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x000593F0 File Offset: 0x000575F0
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x000593FC File Offset: 0x000575FC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.serviceProvider != null)
			{
				IDesignerHost designerHost = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					designerHost.RemoveService(typeof(DesignerActionService));
				}
				IComponentChangeService componentChangeService = (IComponentChangeService)this.serviceProvider.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
				}
			}
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00059472 File Offset: 0x00057672
		public DesignerActionListCollection GetComponentActions(IComponent component)
		{
			return this.GetComponentActions(component, ComponentActionsType.All);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0005947C File Offset: 0x0005767C
		public virtual DesignerActionListCollection GetComponentActions(IComponent component, ComponentActionsType type)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			DesignerActionListCollection designerActionListCollection = new DesignerActionListCollection();
			switch (type)
			{
			case ComponentActionsType.All:
				this.GetComponentDesignerActions(component, designerActionListCollection);
				this.GetComponentServiceActions(component, designerActionListCollection);
				break;
			case ComponentActionsType.Component:
				this.GetComponentDesignerActions(component, designerActionListCollection);
				break;
			case ComponentActionsType.Service:
				this.GetComponentServiceActions(component, designerActionListCollection);
				break;
			}
			return designerActionListCollection;
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x000594D8 File Offset: 0x000576D8
		protected virtual void GetComponentDesignerActions(IComponent component, DesignerActionListCollection actionLists)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (actionLists == null)
			{
				throw new ArgumentNullException("actionLists");
			}
			IServiceContainer serviceContainer = component.Site as IServiceContainer;
			if (serviceContainer != null)
			{
				DesignerCommandSet designerCommandSet = (DesignerCommandSet)serviceContainer.GetService(typeof(DesignerCommandSet));
				if (designerCommandSet != null)
				{
					DesignerActionListCollection actionLists2 = designerCommandSet.ActionLists;
					if (actionLists2 != null)
					{
						actionLists.AddRange(actionLists2);
					}
					if (actionLists.Count == 0)
					{
						DesignerVerbCollection verbs = designerCommandSet.Verbs;
						if (verbs != null && verbs.Count != 0)
						{
							ArrayList arrayList = new ArrayList();
							bool flag = this.componentToVerbsEventHookedUp[component] == null;
							if (flag)
							{
								this.componentToVerbsEventHookedUp[component] = true;
							}
							foreach (object obj in verbs)
							{
								DesignerVerb designerVerb = (DesignerVerb)obj;
								if (flag)
								{
									designerVerb.CommandChanged += this.OnVerbStatusChanged;
								}
								if (designerVerb.Enabled && designerVerb.Visible)
								{
									arrayList.Add(designerVerb);
								}
							}
							if (arrayList.Count != 0)
							{
								DesignerActionVerbList value = new DesignerActionVerbList((DesignerVerb[])arrayList.ToArray(typeof(DesignerVerb)));
								actionLists.Add(value);
							}
						}
					}
					if (actionLists2 != null)
					{
						foreach (object obj2 in actionLists2)
						{
							DesignerActionList designerActionList = (DesignerActionList)obj2;
							DesignerActionItemCollection sortedActionItems = designerActionList.GetSortedActionItems();
							if (sortedActionItems == null || sortedActionItems.Count == 0)
							{
								actionLists.Remove(designerActionList);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x000596A0 File Offset: 0x000578A0
		private void OnVerbStatusChanged(object sender, EventArgs args)
		{
			if (!this.reEntrantCode)
			{
				try
				{
					this.reEntrantCode = true;
					IComponent component = this.selSvc.PrimarySelection as IComponent;
					if (component != null)
					{
						IServiceContainer serviceContainer = component.Site as IServiceContainer;
						if (serviceContainer != null)
						{
							DesignerCommandSet designerCommandSet = (DesignerCommandSet)serviceContainer.GetService(typeof(DesignerCommandSet));
							foreach (object obj in designerCommandSet.Verbs)
							{
								DesignerVerb designerVerb = (DesignerVerb)obj;
								if (designerVerb == sender)
								{
									DesignerActionUIService designerActionUIService = (DesignerActionUIService)serviceContainer.GetService(typeof(DesignerActionUIService));
									if (designerActionUIService != null)
									{
										designerActionUIService.Refresh(component);
									}
								}
							}
						}
					}
				}
				finally
				{
					this.reEntrantCode = false;
				}
			}
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00059784 File Offset: 0x00057984
		protected virtual void GetComponentServiceActions(IComponent component, DesignerActionListCollection actionLists)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (actionLists == null)
			{
				throw new ArgumentNullException("actionLists");
			}
			DesignerActionListCollection designerActionListCollection = (DesignerActionListCollection)this.designerActionLists[component];
			if (designerActionListCollection != null)
			{
				actionLists.AddRange(designerActionListCollection);
				foreach (object obj in designerActionListCollection)
				{
					DesignerActionList designerActionList = (DesignerActionList)obj;
					DesignerActionItemCollection sortedActionItems = designerActionList.GetSortedActionItems();
					if (sortedActionItems == null || sortedActionItems.Count == 0)
					{
						actionLists.Remove(designerActionList);
					}
				}
			}
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00059828 File Offset: 0x00057A28
		private void OnComponentRemoved(object source, ComponentEventArgs ce)
		{
			this.Remove(ce.Component);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00059836 File Offset: 0x00057A36
		private void OnDesignerActionListsChanged(DesignerActionListsChangedEventArgs e)
		{
			if (this.designerActionListsChanged != null)
			{
				this.designerActionListsChanged(this, e);
			}
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0005984D File Offset: 0x00057A4D
		public void Remove(IComponent comp)
		{
			if (comp == null)
			{
				throw new ArgumentNullException("comp");
			}
			if (!this.designerActionLists.Contains(comp))
			{
				return;
			}
			this.designerActionLists.Remove(comp);
			this.OnDesignerActionListsChanged(new DesignerActionListsChangedEventArgs(comp, DesignerActionListsChangedType.ActionListsRemoved, this.GetComponentActions(comp)));
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0005988C File Offset: 0x00057A8C
		public void Remove(DesignerActionList actionList)
		{
			if (actionList == null)
			{
				throw new ArgumentNullException("actionList");
			}
			foreach (object obj in this.designerActionLists.Keys)
			{
				IComponent component = (IComponent)obj;
				if (((DesignerActionListCollection)this.designerActionLists[component]).Contains(actionList))
				{
					this.Remove(component, actionList);
					break;
				}
			}
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00059914 File Offset: 0x00057B14
		public void Remove(IComponent comp, DesignerActionList actionList)
		{
			if (comp == null)
			{
				throw new ArgumentNullException("comp");
			}
			if (actionList == null)
			{
				throw new ArgumentNullException("actionList");
			}
			if (!this.designerActionLists.Contains(comp))
			{
				return;
			}
			DesignerActionListCollection designerActionListCollection = (DesignerActionListCollection)this.designerActionLists[comp];
			if (!designerActionListCollection.Contains(actionList))
			{
				return;
			}
			if (designerActionListCollection.Count == 1)
			{
				this.Remove(comp);
				return;
			}
			ArrayList arrayList = new ArrayList(1);
			foreach (object obj in designerActionListCollection)
			{
				DesignerActionList designerActionList = (DesignerActionList)obj;
				if (actionList.Equals(designerActionList))
				{
					arrayList.Add(designerActionList);
				}
			}
			foreach (object obj2 in arrayList)
			{
				DesignerActionList value = (DesignerActionList)obj2;
				designerActionListCollection.Remove(value);
			}
			this.OnDesignerActionListsChanged(new DesignerActionListsChangedEventArgs(comp, DesignerActionListsChangedType.ActionListsRemoved, this.GetComponentActions(comp)));
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000FA8 RID: 4008 RVA: 0x00059A34 File Offset: 0x00057C34
		// (remove) Token: 0x06000FA9 RID: 4009 RVA: 0x00059A68 File Offset: 0x00057C68
		internal event DesignerActionUIStateChangeEventHandler DesignerActionUIStateChange
		{
			add
			{
				DesignerActionUIService designerActionUIService = (DesignerActionUIService)this.serviceProvider.GetService(typeof(DesignerActionUIService));
				if (designerActionUIService != null)
				{
					designerActionUIService.DesignerActionUIStateChange += value;
				}
			}
			remove
			{
				DesignerActionUIService designerActionUIService = (DesignerActionUIService)this.serviceProvider.GetService(typeof(DesignerActionUIService));
				if (designerActionUIService != null)
				{
					designerActionUIService.DesignerActionUIStateChange -= value;
				}
			}
		}

		// Token: 0x04000920 RID: 2336
		private Hashtable designerActionLists;

		// Token: 0x04000921 RID: 2337
		private DesignerActionListsChangedEventHandler designerActionListsChanged;

		// Token: 0x04000922 RID: 2338
		private IServiceProvider serviceProvider;

		// Token: 0x04000923 RID: 2339
		private ISelectionService selSvc;

		// Token: 0x04000924 RID: 2340
		private Hashtable componentToVerbsEventHookedUp;

		// Token: 0x04000925 RID: 2341
		private bool reEntrantCode;
	}
}
