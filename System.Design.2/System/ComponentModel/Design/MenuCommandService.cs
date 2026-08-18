using System;
using System.Collections;
using System.Collections.Generic;
using System.Design;
using System.Diagnostics;

namespace System.ComponentModel.Design
{
	// Token: 0x020001BA RID: 442
	public class MenuCommandService : IMenuCommandService, IDisposable
	{
		// Token: 0x06001006 RID: 4102 RVA: 0x0005AA7C File Offset: 0x00058C7C
		public MenuCommandService(IServiceProvider serviceProvider)
		{
			this._serviceProvider = serviceProvider;
			this._commandGroupsLock = new object();
			this._commandGroups = new Dictionary<Guid, ArrayList>();
			this._commandChangedHandler = new EventHandler(this.OnCommandChanged);
			TypeDescriptor.Refreshed += this.OnTypeRefreshed;
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06001007 RID: 4103 RVA: 0x0005AACF File Offset: 0x00058CCF
		// (remove) Token: 0x06001008 RID: 4104 RVA: 0x0005AAE8 File Offset: 0x00058CE8
		public event MenuCommandsChangedEventHandler MenuCommandsChanged
		{
			add
			{
				this._commandsChangedHandler = (MenuCommandsChangedEventHandler)Delegate.Combine(this._commandsChangedHandler, value);
			}
			remove
			{
				this._commandsChangedHandler = (MenuCommandsChangedEventHandler)Delegate.Remove(this._commandsChangedHandler, value);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0005AB01 File Offset: 0x00058D01
		public virtual DesignerVerbCollection Verbs
		{
			get
			{
				this.EnsureVerbs();
				return this._currentVerbs;
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0005AB10 File Offset: 0x00058D10
		public virtual void AddCommand(MenuCommand command)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if (((IMenuCommandService)this).FindCommand(command.CommandID) != null)
			{
				throw new ArgumentException(SR.GetString("MenuCommandService_DuplicateCommand", new object[]
				{
					command.CommandID.ToString()
				}));
			}
			object commandGroupsLock = this._commandGroupsLock;
			lock (commandGroupsLock)
			{
				ArrayList arrayList;
				if (!this._commandGroups.TryGetValue(command.CommandID.Guid, out arrayList))
				{
					arrayList = new ArrayList();
					arrayList.Add(command);
					this._commandGroups.Add(command.CommandID.Guid, arrayList);
				}
				else
				{
					arrayList.Add(command);
				}
			}
			command.CommandChanged += this._commandChangedHandler;
			this.OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandAdded, command));
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0005ABEC File Offset: 0x00058DEC
		public virtual void AddVerb(DesignerVerb verb)
		{
			if (verb == null)
			{
				throw new ArgumentNullException("verb");
			}
			if (this._globalVerbs == null)
			{
				this._globalVerbs = new ArrayList();
			}
			this._globalVerbs.Add(verb);
			this.OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandAdded, verb));
			this.EnsureVerbs();
			if (!((IMenuCommandService)this).Verbs.Contains(verb))
			{
				((IMenuCommandService)this).Verbs.Add(verb);
			}
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0005AC55 File Offset: 0x00058E55
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0005AC60 File Offset: 0x00058E60
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._selectionService != null)
				{
					this._selectionService.SelectionChanging -= this.OnSelectionChanging;
					this._selectionService = null;
				}
				if (this._serviceProvider != null)
				{
					this._serviceProvider = null;
					TypeDescriptor.Refreshed -= this.OnTypeRefreshed;
				}
				object commandGroupsLock = this._commandGroupsLock;
				lock (commandGroupsLock)
				{
					foreach (KeyValuePair<Guid, ArrayList> keyValuePair in this._commandGroups)
					{
						ArrayList value = keyValuePair.Value;
						foreach (object obj in value)
						{
							MenuCommand menuCommand = (MenuCommand)obj;
							menuCommand.CommandChanged -= this._commandChangedHandler;
						}
						value.Clear();
					}
				}
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0005AD84 File Offset: 0x00058F84
		protected void EnsureVerbs()
		{
			bool flag = false;
			if (this._currentVerbs == null && this._serviceProvider != null)
			{
				if (this._selectionService == null)
				{
					this._selectionService = (this.GetService(typeof(ISelectionService)) as ISelectionService);
					if (this._selectionService != null)
					{
						this._selectionService.SelectionChanging += this.OnSelectionChanging;
					}
				}
				int num = 0;
				DesignerVerbCollection designerVerbCollection = null;
				DesignerVerbCollection designerVerbCollection2 = new DesignerVerbCollection();
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (this._selectionService != null && designerHost != null && this._selectionService.SelectionCount == 1)
				{
					object primarySelection = this._selectionService.PrimarySelection;
					if (primarySelection is IComponent && !TypeDescriptor.GetAttributes(primarySelection).Contains(InheritanceAttribute.InheritedReadOnly))
					{
						flag = (primarySelection == designerHost.RootComponent);
						IDesigner designer = designerHost.GetDesigner((IComponent)primarySelection);
						if (designer != null)
						{
							designerVerbCollection = designer.Verbs;
							if (designerVerbCollection != null)
							{
								num += designerVerbCollection.Count;
								this._verbSourceType = primarySelection.GetType();
							}
							else
							{
								this._verbSourceType = null;
							}
						}
						DesignerActionService designerActionService = this.GetService(typeof(DesignerActionService)) as DesignerActionService;
						if (designerActionService != null)
						{
							DesignerActionListCollection componentActions = designerActionService.GetComponentActions(primarySelection as IComponent);
							if (componentActions != null)
							{
								foreach (object obj in componentActions)
								{
									DesignerActionList designerActionList = (DesignerActionList)obj;
									DesignerActionItemCollection sortedActionItems = designerActionList.GetSortedActionItems();
									if (sortedActionItems != null)
									{
										for (int i = 0; i < sortedActionItems.Count; i++)
										{
											DesignerActionMethodItem designerActionMethodItem = sortedActionItems[i] as DesignerActionMethodItem;
											if (designerActionMethodItem != null && designerActionMethodItem.IncludeAsDesignerVerb)
											{
												EventHandler handler = new EventHandler(designerActionMethodItem.Invoke);
												DesignerVerb value = new DesignerVerb(designerActionMethodItem.DisplayName, handler);
												designerVerbCollection2.Add(value);
												num++;
											}
										}
									}
								}
							}
						}
					}
				}
				if (flag && this._globalVerbs == null)
				{
					flag = false;
				}
				if (flag)
				{
					num += this._globalVerbs.Count;
				}
				Hashtable hashtable = new Hashtable(num, StringComparer.OrdinalIgnoreCase);
				ArrayList arrayList = new ArrayList(num);
				if (flag)
				{
					for (int j = 0; j < this._globalVerbs.Count; j++)
					{
						string text = ((DesignerVerb)this._globalVerbs[j]).Text;
						hashtable[text] = arrayList.Add(this._globalVerbs[j]);
					}
				}
				if (designerVerbCollection2.Count > 0)
				{
					for (int k = 0; k < designerVerbCollection2.Count; k++)
					{
						string text2 = designerVerbCollection2[k].Text;
						hashtable[text2] = arrayList.Add(designerVerbCollection2[k]);
					}
				}
				if (designerVerbCollection != null && designerVerbCollection.Count > 0)
				{
					for (int l = 0; l < designerVerbCollection.Count; l++)
					{
						string text3 = designerVerbCollection[l].Text;
						hashtable[text3] = arrayList.Add(designerVerbCollection[l]);
					}
				}
				DesignerVerb[] array = new DesignerVerb[hashtable.Count];
				int num2 = 0;
				for (int m = 0; m < arrayList.Count; m++)
				{
					DesignerVerb designerVerb = (DesignerVerb)arrayList[m];
					string text4 = designerVerb.Text;
					if ((int)hashtable[text4] == m)
					{
						array[num2] = designerVerb;
						num2++;
					}
				}
				this._currentVerbs = new DesignerVerbCollection(array);
			}
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0005B128 File Offset: 0x00059328
		public MenuCommand FindCommand(CommandID commandID)
		{
			return this.FindCommand(commandID.Guid, commandID.ID);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0005B13C File Offset: 0x0005933C
		protected MenuCommand FindCommand(Guid guid, int id)
		{
			object commandGroupsLock = this._commandGroupsLock;
			ArrayList arrayList;
			lock (commandGroupsLock)
			{
				this._commandGroups.TryGetValue(guid, out arrayList);
			}
			if (arrayList != null)
			{
				foreach (object obj in arrayList)
				{
					MenuCommand menuCommand = (MenuCommand)obj;
					if (menuCommand.CommandID.ID == id)
					{
						return menuCommand;
					}
				}
			}
			this.EnsureVerbs();
			if (this._currentVerbs != null)
			{
				int num = StandardCommands.VerbFirst.ID;
				foreach (object obj2 in this._currentVerbs)
				{
					DesignerVerb designerVerb = (DesignerVerb)obj2;
					CommandID commandID = designerVerb.CommandID;
					if (commandID.ID == id && commandID.Guid.Equals(guid))
					{
						return designerVerb;
					}
					if (num == id && commandID.Guid.Equals(guid))
					{
						return designerVerb;
					}
					if (commandID.Equals(StandardCommands.VerbFirst))
					{
						num++;
					}
				}
			}
			return null;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0005B2A8 File Offset: 0x000594A8
		protected ICollection GetCommandList(Guid guid)
		{
			ArrayList result = null;
			object commandGroupsLock = this._commandGroupsLock;
			lock (commandGroupsLock)
			{
				this._commandGroups.TryGetValue(guid, out result);
			}
			return result;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0005B2F4 File Offset: 0x000594F4
		protected object GetService(Type serviceType)
		{
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (this._serviceProvider != null)
			{
				return this._serviceProvider.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0005B320 File Offset: 0x00059520
		public virtual bool GlobalInvoke(CommandID commandID)
		{
			MenuCommand menuCommand = ((IMenuCommandService)this).FindCommand(commandID);
			if (menuCommand != null)
			{
				menuCommand.Invoke();
				return true;
			}
			return false;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0005B344 File Offset: 0x00059544
		public virtual bool GlobalInvoke(CommandID commandId, object arg)
		{
			MenuCommand menuCommand = ((IMenuCommandService)this).FindCommand(commandId);
			if (menuCommand != null)
			{
				menuCommand.Invoke(arg);
				return true;
			}
			return false;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0005B366 File Offset: 0x00059566
		private void OnCommandChanged(object sender, EventArgs e)
		{
			this.OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandChanged, (MenuCommand)sender));
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0005B37A File Offset: 0x0005957A
		protected virtual void OnCommandsChanged(MenuCommandsChangedEventArgs e)
		{
			if (this._commandsChangedHandler != null)
			{
				this._commandsChangedHandler(this, e);
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0005B391 File Offset: 0x00059591
		private void OnTypeRefreshed(RefreshEventArgs e)
		{
			if (this._verbSourceType != null && this._verbSourceType.IsAssignableFrom(e.TypeChanged))
			{
				this._currentVerbs = null;
			}
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0005B3BB File Offset: 0x000595BB
		private void OnSelectionChanging(object sender, EventArgs e)
		{
			if (this._currentVerbs != null)
			{
				this._currentVerbs = null;
				this.OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandChanged, null));
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0005B3DC File Offset: 0x000595DC
		public virtual void RemoveCommand(MenuCommand command)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			object commandGroupsLock = this._commandGroupsLock;
			lock (commandGroupsLock)
			{
				ArrayList arrayList;
				if (this._commandGroups.TryGetValue(command.CommandID.Guid, out arrayList))
				{
					int num = arrayList.IndexOf(command);
					if (-1 != num)
					{
						arrayList.RemoveAt(num);
						if (arrayList.Count == 0)
						{
							this._commandGroups.Remove(command.CommandID.Guid);
						}
						command.CommandChanged -= this._commandChangedHandler;
						this.OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandRemoved, command));
					}
				}
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0005B48C File Offset: 0x0005968C
		public virtual void RemoveVerb(DesignerVerb verb)
		{
			if (verb == null)
			{
				throw new ArgumentNullException("verb");
			}
			if (this._globalVerbs != null)
			{
				int num = this._globalVerbs.IndexOf(verb);
				if (num != -1)
				{
					this._globalVerbs.RemoveAt(num);
					this.EnsureVerbs();
					if (((IMenuCommandService)this).Verbs.Contains(verb))
					{
						((IMenuCommandService)this).Verbs.Remove(verb);
					}
					this.OnCommandsChanged(new MenuCommandsChangedEventArgs(MenuCommandsChangedType.CommandRemoved, verb));
				}
			}
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void ShowContextMenu(CommandID menuID, int x, int y)
		{
		}

		// Token: 0x04000952 RID: 2386
		private IServiceProvider _serviceProvider;

		// Token: 0x04000953 RID: 2387
		private Dictionary<Guid, ArrayList> _commandGroups;

		// Token: 0x04000954 RID: 2388
		private object _commandGroupsLock;

		// Token: 0x04000955 RID: 2389
		private EventHandler _commandChangedHandler;

		// Token: 0x04000956 RID: 2390
		private MenuCommandsChangedEventHandler _commandsChangedHandler;

		// Token: 0x04000957 RID: 2391
		private ArrayList _globalVerbs;

		// Token: 0x04000958 RID: 2392
		private ISelectionService _selectionService;

		// Token: 0x04000959 RID: 2393
		internal static TraceSwitch MENUSERVICE = new TraceSwitch("MENUSERVICE", "MenuCommandService: Track menu command routing");

		// Token: 0x0400095A RID: 2394
		private DesignerVerbCollection _currentVerbs;

		// Token: 0x0400095B RID: 2395
		private Type _verbSourceType;
	}
}
