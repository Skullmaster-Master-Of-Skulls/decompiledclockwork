using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Diagnostics;
using System.Reflection;

namespace System.ComponentModel.Design
{
	// Token: 0x020001D4 RID: 468
	public abstract class UndoEngine : IDisposable
	{
		// Token: 0x06001167 RID: 4455 RVA: 0x00060338 File Offset: 0x0005E538
		protected UndoEngine(IServiceProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this._provider = provider;
			this._unitStack = new Stack();
			this._enabled = true;
			this._host = (this.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost);
			this._componentChangeService = (this.GetRequiredService(typeof(IComponentChangeService)) as IComponentChangeService);
			this._serializationService = (this.GetRequiredService(typeof(ComponentSerializationService)) as ComponentSerializationService);
			this._host.TransactionOpening += this.OnTransactionOpening;
			this._host.TransactionClosed += this.OnTransactionClosed;
			this._componentChangeService.ComponentAdding += this.OnComponentAdding;
			this._componentChangeService.ComponentChanging += this.OnComponentChanging;
			this._componentChangeService.ComponentRemoving += this.OnComponentRemoving;
			this._componentChangeService.ComponentAdded += this.OnComponentAdded;
			this._componentChangeService.ComponentChanged += this.OnComponentChanged;
			this._componentChangeService.ComponentRemoved += this.OnComponentRemoved;
			this._componentChangeService.ComponentRename += this.OnComponentRename;
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x00060492 File Offset: 0x0005E692
		private UndoEngine.UndoUnit CurrentUnit
		{
			get
			{
				if (this._unitStack.Count > 0)
				{
					return (UndoEngine.UndoUnit)this._unitStack.Peek();
				}
				return null;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x000604B4 File Offset: 0x0005E6B4
		public bool UndoInProgress
		{
			get
			{
				return this._executingUnit != null;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x000604BF File Offset: 0x0005E6BF
		// (set) Token: 0x0600116B RID: 4459 RVA: 0x000604C7 File Offset: 0x0005E6C7
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
			}
		}

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600116C RID: 4460 RVA: 0x000604D0 File Offset: 0x0005E6D0
		// (remove) Token: 0x0600116D RID: 4461 RVA: 0x000604E9 File Offset: 0x0005E6E9
		public event EventHandler Undoing
		{
			add
			{
				this._undoingEvent = (EventHandler)Delegate.Combine(this._undoingEvent, value);
			}
			remove
			{
				this._undoingEvent = (EventHandler)Delegate.Remove(this._undoingEvent, value);
			}
		}

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x0600116E RID: 4462 RVA: 0x00060502 File Offset: 0x0005E702
		// (remove) Token: 0x0600116F RID: 4463 RVA: 0x0006051B File Offset: 0x0005E71B
		public event EventHandler Undone
		{
			add
			{
				this._undoneEvent = (EventHandler)Delegate.Combine(this._undoneEvent, value);
			}
			remove
			{
				this._undoneEvent = (EventHandler)Delegate.Remove(this._undoneEvent, value);
			}
		}

		// Token: 0x06001170 RID: 4464
		protected abstract void AddUndoUnit(UndoEngine.UndoUnit unit);

		// Token: 0x06001171 RID: 4465 RVA: 0x00060534 File Offset: 0x0005E734
		private void CheckPopUnit(UndoEngine.PopUnitReason reason)
		{
			if (reason != UndoEngine.PopUnitReason.Normal || !this._host.InTransaction)
			{
				UndoEngine.UndoUnit undoUnit = (UndoEngine.UndoUnit)this._unitStack.Pop();
				if (!undoUnit.IsEmpty)
				{
					undoUnit.Close();
					if (reason == UndoEngine.PopUnitReason.TransactionCancel)
					{
						undoUnit.Undo();
						if (this._unitStack.Count == 0)
						{
							this.DiscardUndoUnit(undoUnit);
							return;
						}
					}
					else if (this._unitStack.Count == 0)
					{
						this.AddUndoUnit(undoUnit);
						return;
					}
				}
				else if (this._unitStack.Count == 0)
				{
					this.DiscardUndoUnit(undoUnit);
				}
			}
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x000605B8 File Offset: 0x0005E7B8
		protected virtual UndoEngine.UndoUnit CreateUndoUnit(string name, bool primary)
		{
			return new UndoEngine.UndoUnit(this, name);
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x000605C1 File Offset: 0x0005E7C1
		internal IComponentChangeService ComponentChangeService
		{
			get
			{
				return this._componentChangeService;
			}
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void DiscardUndoUnit(UndoEngine.UndoUnit unit)
		{
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000605C9 File Offset: 0x0005E7C9
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000605D4 File Offset: 0x0005E7D4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._host != null)
				{
					this._host.TransactionOpening -= this.OnTransactionOpening;
					this._host.TransactionClosed -= this.OnTransactionClosed;
				}
				if (this._componentChangeService != null)
				{
					this._componentChangeService.ComponentAdding -= this.OnComponentAdding;
					this._componentChangeService.ComponentChanging -= this.OnComponentChanging;
					this._componentChangeService.ComponentRemoving -= this.OnComponentRemoving;
					this._componentChangeService.ComponentAdded -= this.OnComponentAdded;
					this._componentChangeService.ComponentChanged -= this.OnComponentChanged;
					this._componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
					this._componentChangeService.ComponentRename -= this.OnComponentRename;
				}
				this._provider = null;
			}
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x000606D0 File Offset: 0x0005E8D0
		internal string GetName(object obj, bool generateNew)
		{
			string text = null;
			if (obj != null)
			{
				IReferenceService referenceService = this.GetService(typeof(IReferenceService)) as IReferenceService;
				if (referenceService != null)
				{
					text = referenceService.GetName(obj);
				}
				else
				{
					IComponent component = obj as IComponent;
					if (component != null)
					{
						ISite site = component.Site;
						if (site != null)
						{
							text = site.Name;
						}
					}
				}
			}
			if (text == null && generateNew)
			{
				if (obj == null)
				{
					text = "(null)";
				}
				else
				{
					text = obj.GetType().Name;
				}
			}
			return text;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00060740 File Offset: 0x0005E940
		protected object GetRequiredService(Type serviceType)
		{
			object service = this.GetService(serviceType);
			if (service == null)
			{
				throw new InvalidOperationException(SR.GetString("UndoEngineMissingService", new object[]
				{
					serviceType.Name
				}))
				{
					HelpLink = "UndoEngineMissingService"
				};
			}
			return service;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00060785 File Offset: 0x0005E985
		protected object GetService(Type serviceType)
		{
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (this._provider != null)
			{
				return this._provider.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x000607B4 File Offset: 0x0005E9B4
		private void OnComponentAdded(object sender, ComponentEventArgs e)
		{
			foreach (object obj in this._unitStack)
			{
				UndoEngine.UndoUnit undoUnit = (UndoEngine.UndoUnit)obj;
				undoUnit.ComponentAdded(e);
			}
			if (this.CurrentUnit != null)
			{
				this.CheckPopUnit(UndoEngine.PopUnitReason.Normal);
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0006081C File Offset: 0x0005EA1C
		private void OnComponentAdding(object sender, ComponentEventArgs e)
		{
			if (this._enabled && this._executingUnit == null && this._unitStack.Count == 0)
			{
				string @string;
				if (e.Component != null)
				{
					@string = SR.GetString("UndoEngineComponentAdd1", new object[]
					{
						this.GetName(e.Component, true)
					});
				}
				else
				{
					@string = SR.GetString("UndoEngineComponentAdd0");
				}
				this._unitStack.Push(this.CreateUndoUnit(@string, true));
			}
			foreach (object obj in this._unitStack)
			{
				UndoEngine.UndoUnit undoUnit = (UndoEngine.UndoUnit)obj;
				undoUnit.ComponentAdding(e);
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000608DC File Offset: 0x0005EADC
		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			foreach (object obj in this._unitStack)
			{
				UndoEngine.UndoUnit undoUnit = (UndoEngine.UndoUnit)obj;
				undoUnit.ComponentChanged(e);
			}
			if (this.CurrentUnit != null)
			{
				this.CheckPopUnit(UndoEngine.PopUnitReason.Normal);
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00060944 File Offset: 0x0005EB44
		private void OnComponentChanging(object sender, ComponentChangingEventArgs e)
		{
			if (this._enabled && this._executingUnit == null && this._unitStack.Count == 0)
			{
				string @string;
				if (e.Member != null && e.Component != null)
				{
					@string = SR.GetString("UndoEngineComponentChange2", new object[]
					{
						this.GetName(e.Component, true),
						e.Member.Name
					});
				}
				else if (e.Component != null)
				{
					@string = SR.GetString("UndoEngineComponentChange1", new object[]
					{
						this.GetName(e.Component, true)
					});
				}
				else
				{
					@string = SR.GetString("UndoEngineComponentChange0");
				}
				this._unitStack.Push(this.CreateUndoUnit(@string, true));
			}
			foreach (object obj in this._unitStack)
			{
				UndoEngine.UndoUnit undoUnit = (UndoEngine.UndoUnit)obj;
				undoUnit.ComponentChanging(e);
			}
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00060A50 File Offset: 0x0005EC50
		private void OnComponentRemoved(object sender, ComponentEventArgs e)
		{
			foreach (object obj in this._unitStack)
			{
				UndoEngine.UndoUnit undoUnit = (UndoEngine.UndoUnit)obj;
				undoUnit.ComponentRemoved(e);
			}
			if (this.CurrentUnit != null)
			{
				this.CheckPopUnit(UndoEngine.PopUnitReason.Normal);
			}
			List<UndoEngine.ReferencingComponent> list = null;
			if (this._refToRemovedComponent != null && this._refToRemovedComponent.TryGetValue(e.Component, out list) && list != null && this._componentChangeService != null)
			{
				foreach (UndoEngine.ReferencingComponent referencingComponent in list)
				{
					this._componentChangeService.OnComponentChanged(referencingComponent.component, referencingComponent.member, null, null);
				}
				this._refToRemovedComponent.Remove(e.Component);
			}
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00060B48 File Offset: 0x0005ED48
		private void OnComponentRemoving(object sender, ComponentEventArgs e)
		{
			if (this._enabled && this._executingUnit == null && this._unitStack.Count == 0)
			{
				string @string;
				if (e.Component != null)
				{
					@string = SR.GetString("UndoEngineComponentRemove1", new object[]
					{
						this.GetName(e.Component, true)
					});
				}
				else
				{
					@string = SR.GetString("UndoEngineComponentRemove0");
				}
				this._unitStack.Push(this.CreateUndoUnit(@string, true));
			}
			if (this._enabled && this._host != null && this._host.Container != null && this._componentChangeService != null)
			{
				List<UndoEngine.ReferencingComponent> list = null;
				foreach (object obj in this._host.Container.Components)
				{
					IComponent component = (IComponent)obj;
					if (component != e.Component)
					{
						PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
						foreach (object obj2 in properties)
						{
							PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
							if (propertyDescriptor.PropertyType.IsAssignableFrom(e.Component.GetType()) && !propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Hidden) && !propertyDescriptor.IsReadOnly)
							{
								object obj3 = null;
								try
								{
									obj3 = propertyDescriptor.GetValue(component);
								}
								catch (TargetInvocationException)
								{
									continue;
								}
								if (obj3 != null && obj3 == e.Component)
								{
									if (list == null)
									{
										list = new List<UndoEngine.ReferencingComponent>();
										if (this._refToRemovedComponent == null)
										{
											this._refToRemovedComponent = new Dictionary<IComponent, List<UndoEngine.ReferencingComponent>>();
										}
										this._refToRemovedComponent[e.Component] = list;
									}
									this._componentChangeService.OnComponentChanging(component, propertyDescriptor);
									list.Add(new UndoEngine.ReferencingComponent(component, propertyDescriptor));
								}
							}
						}
					}
				}
			}
			foreach (object obj4 in this._unitStack)
			{
				UndoEngine.UndoUnit undoUnit = (UndoEngine.UndoUnit)obj4;
				undoUnit.ComponentRemoving(e);
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00060DD4 File Offset: 0x0005EFD4
		private void OnComponentRename(object sender, ComponentRenameEventArgs e)
		{
			if (this._enabled && this._executingUnit == null && this._unitStack.Count == 0)
			{
				string @string = SR.GetString("UndoEngineComponentRename", new object[]
				{
					e.OldName,
					e.NewName
				});
				this._unitStack.Push(this.CreateUndoUnit(@string, true));
			}
			foreach (object obj in this._unitStack)
			{
				UndoEngine.UndoUnit undoUnit = (UndoEngine.UndoUnit)obj;
				undoUnit.ComponentRename(e);
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00060E80 File Offset: 0x0005F080
		private void OnTransactionClosed(object sender, DesignerTransactionCloseEventArgs e)
		{
			if (this._executingUnit == null && this.CurrentUnit != null)
			{
				UndoEngine.PopUnitReason reason = e.TransactionCommitted ? UndoEngine.PopUnitReason.TransactionCommit : UndoEngine.PopUnitReason.TransactionCancel;
				this.CheckPopUnit(reason);
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00060EB1 File Offset: 0x0005F0B1
		private void OnTransactionOpening(object sender, EventArgs e)
		{
			if (this._enabled && this._executingUnit == null)
			{
				this._unitStack.Push(this.CreateUndoUnit(this._host.TransactionDescription, this._unitStack.Count == 0));
			}
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00060EED File Offset: 0x0005F0ED
		protected virtual void OnUndoing(EventArgs e)
		{
			if (this._undoingEvent != null)
			{
				this._undoingEvent(this, e);
			}
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00060F04 File Offset: 0x0005F104
		protected virtual void OnUndone(EventArgs e)
		{
			if (this._undoneEvent != null)
			{
				this._undoneEvent(this, e);
			}
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00003937 File Offset: 0x00001B37
		[Conditional("DEBUG")]
		private static void Trace(string text, params object[] values)
		{
		}

		// Token: 0x040009C7 RID: 2503
		private static TraceSwitch traceUndo = new TraceSwitch("UndoEngine", "Trace UndoRedo");

		// Token: 0x040009C8 RID: 2504
		private IServiceProvider _provider;

		// Token: 0x040009C9 RID: 2505
		private Stack _unitStack;

		// Token: 0x040009CA RID: 2506
		private UndoEngine.UndoUnit _executingUnit;

		// Token: 0x040009CB RID: 2507
		private IDesignerHost _host;

		// Token: 0x040009CC RID: 2508
		private ComponentSerializationService _serializationService;

		// Token: 0x040009CD RID: 2509
		private EventHandler _undoingEvent;

		// Token: 0x040009CE RID: 2510
		private EventHandler _undoneEvent;

		// Token: 0x040009CF RID: 2511
		private IComponentChangeService _componentChangeService;

		// Token: 0x040009D0 RID: 2512
		private Dictionary<IComponent, List<UndoEngine.ReferencingComponent>> _refToRemovedComponent;

		// Token: 0x040009D1 RID: 2513
		private bool _enabled;

		// Token: 0x0200049F RID: 1183
		private enum PopUnitReason
		{
			// Token: 0x04001E33 RID: 7731
			Normal,
			// Token: 0x04001E34 RID: 7732
			TransactionCommit,
			// Token: 0x04001E35 RID: 7733
			TransactionCancel
		}

		// Token: 0x020004A0 RID: 1184
		private struct ReferencingComponent
		{
			// Token: 0x06002B88 RID: 11144 RVA: 0x00103FE9 File Offset: 0x001021E9
			public ReferencingComponent(IComponent component, MemberDescriptor member)
			{
				this.component = component;
				this.member = member;
			}

			// Token: 0x04001E36 RID: 7734
			public IComponent component;

			// Token: 0x04001E37 RID: 7735
			public MemberDescriptor member;
		}

		// Token: 0x020004A1 RID: 1185
		protected class UndoUnit
		{
			// Token: 0x06002B89 RID: 11145 RVA: 0x00103FFC File Offset: 0x001021FC
			public UndoUnit(UndoEngine engine, string name)
			{
				if (engine == null)
				{
					throw new ArgumentNullException("engine");
				}
				if (name == null)
				{
					name = string.Empty;
				}
				this._name = name;
				this._engine = engine;
				this._reverse = true;
				ISelectionService selectionService = this._engine.GetService(typeof(ISelectionService)) as ISelectionService;
				if (selectionService != null)
				{
					ICollection selectedComponents = selectionService.GetSelectedComponents();
					Hashtable hashtable = new Hashtable();
					foreach (object obj in selectedComponents)
					{
						IComponent component = obj as IComponent;
						if (component != null && component.Site != null)
						{
							hashtable[component.Site.Name] = component.Site.Container;
						}
					}
					this._lastSelection = hashtable;
				}
			}

			// Token: 0x17000933 RID: 2355
			// (get) Token: 0x06002B8A RID: 11146 RVA: 0x001040E4 File Offset: 0x001022E4
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x17000934 RID: 2356
			// (get) Token: 0x06002B8B RID: 11147 RVA: 0x001040EC File Offset: 0x001022EC
			public virtual bool IsEmpty
			{
				get
				{
					return this._events == null || this._events.Count == 0;
				}
			}

			// Token: 0x17000935 RID: 2357
			// (get) Token: 0x06002B8C RID: 11148 RVA: 0x00104106 File Offset: 0x00102306
			protected UndoEngine UndoEngine
			{
				get
				{
					return this._engine;
				}
			}

			// Token: 0x06002B8D RID: 11149 RVA: 0x0010410E File Offset: 0x0010230E
			private void AddEvent(UndoEngine.UndoUnit.UndoEvent e)
			{
				if (this._events == null)
				{
					this._events = new ArrayList();
				}
				this._events.Add(e);
			}

			// Token: 0x06002B8E RID: 11150 RVA: 0x00104130 File Offset: 0x00102330
			public virtual void Close()
			{
				if (this._changeEvents != null)
				{
					foreach (object obj in this._changeEvents)
					{
						UndoEngine.UndoUnit.ChangeUndoEvent changeUndoEvent = (UndoEngine.UndoUnit.ChangeUndoEvent)obj;
						changeUndoEvent.Commit(this._engine);
					}
				}
				if (this._removeEvents != null)
				{
					foreach (object obj2 in this._removeEvents)
					{
						UndoEngine.UndoUnit.AddRemoveUndoEvent addRemoveUndoEvent = (UndoEngine.UndoUnit.AddRemoveUndoEvent)obj2;
						addRemoveUndoEvent.Commit(this._engine);
					}
				}
				this._changeEvents = null;
				this._removeEvents = null;
				this._ignoreAddingList = null;
				this._ignoreAddedList = null;
			}

			// Token: 0x06002B8F RID: 11151 RVA: 0x0010420C File Offset: 0x0010240C
			public virtual void ComponentAdded(ComponentEventArgs e)
			{
				if (e.Component.Site == null || !(e.Component.Site.Container is INestedContainer))
				{
					this.AddEvent(new UndoEngine.UndoUnit.AddRemoveUndoEvent(this._engine, e.Component, true));
				}
				if (this._ignoreAddingList != null)
				{
					this._ignoreAddingList.Remove(e.Component);
				}
				if (this._ignoreAddedList == null)
				{
					this._ignoreAddedList = new ArrayList();
				}
				this._ignoreAddedList.Add(e.Component);
			}

			// Token: 0x06002B90 RID: 11152 RVA: 0x00104293 File Offset: 0x00102493
			public virtual void ComponentAdding(ComponentEventArgs e)
			{
				if (this._ignoreAddingList == null)
				{
					this._ignoreAddingList = new ArrayList();
				}
				this._ignoreAddingList.Add(e.Component);
			}

			// Token: 0x06002B91 RID: 11153 RVA: 0x001042BA File Offset: 0x001024BA
			private static bool ChangeEventsSymmetric(ComponentChangingEventArgs changing, ComponentChangedEventArgs changed)
			{
				return changing != null && changed != null && changing.Component == changed.Component && changing.Member == changed.Member;
			}

			// Token: 0x06002B92 RID: 11154 RVA: 0x001042E4 File Offset: 0x001024E4
			private bool CanRepositionEvent(int startIndex, ComponentChangedEventArgs e)
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				for (int i = startIndex + 1; i < this._events.Count; i++)
				{
					UndoEngine.UndoUnit.AddRemoveUndoEvent addRemoveUndoEvent = this._events[i] as UndoEngine.UndoUnit.AddRemoveUndoEvent;
					UndoEngine.UndoUnit.RenameUndoEvent renameUndoEvent = this._events[i] as UndoEngine.UndoUnit.RenameUndoEvent;
					UndoEngine.UndoUnit.ChangeUndoEvent changeUndoEvent = this._events[i] as UndoEngine.UndoUnit.ChangeUndoEvent;
					if (addRemoveUndoEvent != null && !addRemoveUndoEvent.NextUndoAdds)
					{
						flag = true;
					}
					else if (changeUndoEvent != null && UndoEngine.UndoUnit.ChangeEventsSymmetric(changeUndoEvent.ComponentChangingEventArgs, e))
					{
						flag3 = true;
					}
					else if (renameUndoEvent != null)
					{
						flag2 = true;
					}
				}
				return flag && !flag2 && !flag3;
			}

			// Token: 0x06002B93 RID: 11155 RVA: 0x00104384 File Offset: 0x00102584
			public virtual void ComponentChanged(ComponentChangedEventArgs e)
			{
				if (this._events != null && e != null)
				{
					for (int i = 0; i < this._events.Count; i++)
					{
						UndoEngine.UndoUnit.ChangeUndoEvent changeUndoEvent = this._events[i] as UndoEngine.UndoUnit.ChangeUndoEvent;
						if (changeUndoEvent != null && UndoEngine.UndoUnit.ChangeEventsSymmetric(changeUndoEvent.ComponentChangingEventArgs, e) && i != this._events.Count - 1 && e.Member != null && e.Member.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content) && this.CanRepositionEvent(i, e))
						{
							this._events.RemoveAt(i);
							this._events.Add(changeUndoEvent);
						}
					}
				}
			}

			// Token: 0x06002B94 RID: 11156 RVA: 0x00104430 File Offset: 0x00102630
			public virtual void ComponentChanging(ComponentChangingEventArgs e)
			{
				if (this._ignoreAddingList != null && this._ignoreAddingList.Contains(e.Component))
				{
					return;
				}
				if (this._changeEvents == null)
				{
					this._changeEvents = new ArrayList();
				}
				if (this._engine != null && this._engine.GetName(e.Component, false) != null)
				{
					IComponent component = e.Component as IComponent;
					bool flag = false;
					for (int i = 0; i < this._changeEvents.Count; i++)
					{
						UndoEngine.UndoUnit.ChangeUndoEvent changeUndoEvent = (UndoEngine.UndoUnit.ChangeUndoEvent)this._changeEvents[i];
						if (changeUndoEvent.OpenComponent == e.Component && changeUndoEvent.ContainsChange(e.Member))
						{
							flag = true;
							break;
						}
					}
					if (!flag || (e.Member != null && e.Member.Attributes != null && e.Member.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content)))
					{
						UndoEngine.UndoUnit.ChangeUndoEvent changeUndoEvent2 = null;
						bool serializeBeforeState = true;
						if (this._ignoreAddedList != null && this._ignoreAddedList.Contains(e.Component))
						{
							serializeBeforeState = false;
						}
						if (component != null && component.Site != null)
						{
							changeUndoEvent2 = new UndoEngine.UndoUnit.ChangeUndoEvent(this._engine, e, serializeBeforeState);
						}
						else if (e.Component != null)
						{
							IReferenceService referenceService = this.GetService(typeof(IReferenceService)) as IReferenceService;
							if (referenceService != null)
							{
								IComponent component2 = referenceService.GetComponent(e.Component);
								if (component2 != null)
								{
									changeUndoEvent2 = new UndoEngine.UndoUnit.ChangeUndoEvent(this._engine, new ComponentChangingEventArgs(component2, null), serializeBeforeState);
								}
							}
						}
						if (changeUndoEvent2 != null)
						{
							this.AddEvent(changeUndoEvent2);
							this._changeEvents.Add(changeUndoEvent2);
						}
					}
				}
			}

			// Token: 0x06002B95 RID: 11157 RVA: 0x001045C4 File Offset: 0x001027C4
			public virtual void ComponentRemoved(ComponentEventArgs e)
			{
				if (this._events != null && e != null)
				{
					UndoEngine.UndoUnit.ChangeUndoEvent changeUndoEvent = null;
					int num = -1;
					int i = this._events.Count - 1;
					while (i >= 0)
					{
						UndoEngine.UndoUnit.AddRemoveUndoEvent addRemoveUndoEvent = this._events[i] as UndoEngine.UndoUnit.AddRemoveUndoEvent;
						if (changeUndoEvent == null)
						{
							changeUndoEvent = (this._events[i] as UndoEngine.UndoUnit.ChangeUndoEvent);
							num = i;
						}
						if (addRemoveUndoEvent != null && addRemoveUndoEvent.OpenComponent == e.Component)
						{
							addRemoveUndoEvent.Commit(this._engine);
							if (i == this._events.Count - 1 || changeUndoEvent == null)
							{
								break;
							}
							bool flag = true;
							for (int j = i + 1; j < num; j++)
							{
								if (!(this._events[j] is UndoEngine.UndoUnit.ChangeUndoEvent))
								{
									flag = false;
									break;
								}
							}
							if (flag)
							{
								this._events.RemoveAt(i);
								this._events.Insert(num, addRemoveUndoEvent);
								return;
							}
							break;
						}
						else
						{
							i--;
						}
					}
				}
			}

			// Token: 0x06002B96 RID: 11158 RVA: 0x001046AC File Offset: 0x001028AC
			public virtual void ComponentRemoving(ComponentEventArgs e)
			{
				if (e.Component.Site != null && e.Component.Site is INestedContainer)
				{
					return;
				}
				if (this._removeEvents == null)
				{
					this._removeEvents = new ArrayList();
				}
				try
				{
					UndoEngine.UndoUnit.AddRemoveUndoEvent addRemoveUndoEvent = new UndoEngine.UndoUnit.AddRemoveUndoEvent(this._engine, e.Component, false);
					this.AddEvent(addRemoveUndoEvent);
					this._removeEvents.Add(addRemoveUndoEvent);
				}
				catch (TargetInvocationException)
				{
				}
			}

			// Token: 0x06002B97 RID: 11159 RVA: 0x0010472C File Offset: 0x0010292C
			public virtual void ComponentRename(ComponentRenameEventArgs e)
			{
				this.AddEvent(new UndoEngine.UndoUnit.RenameUndoEvent(e.OldName, e.NewName));
			}

			// Token: 0x06002B98 RID: 11160 RVA: 0x00104745 File Offset: 0x00102945
			protected object GetService(Type serviceType)
			{
				return this._engine.GetService(serviceType);
			}

			// Token: 0x06002B99 RID: 11161 RVA: 0x00104753 File Offset: 0x00102953
			public override string ToString()
			{
				return this.Name;
			}

			// Token: 0x06002B9A RID: 11162 RVA: 0x0010475C File Offset: 0x0010295C
			public void Undo()
			{
				UndoEngine.UndoUnit executingUnit = this._engine._executingUnit;
				this._engine._executingUnit = this;
				DesignerTransaction designerTransaction = null;
				try
				{
					if (executingUnit == null)
					{
						this._engine.OnUndoing(EventArgs.Empty);
					}
					designerTransaction = this._engine._host.CreateTransaction();
					this.UndoCore();
				}
				catch (CheckoutException)
				{
					designerTransaction.Cancel();
					designerTransaction = null;
					throw;
				}
				finally
				{
					if (designerTransaction != null)
					{
						designerTransaction.Commit();
					}
					this._engine._executingUnit = executingUnit;
					if (executingUnit == null)
					{
						this._engine.OnUndone(EventArgs.Empty);
					}
				}
			}

			// Token: 0x06002B9B RID: 11163 RVA: 0x00104800 File Offset: 0x00102A00
			protected virtual void UndoCore()
			{
				if (this._events != null)
				{
					if (this._reverse)
					{
						for (int i = this._events.Count - 1; i >= 0; i--)
						{
							int num = i;
							int num2 = i;
							while (num2 >= 0 && ((UndoEngine.UndoUnit.UndoEvent)this._events[num2]).CausesSideEffects)
							{
								num = num2;
								num2--;
							}
							for (int j = i; j >= num; j--)
							{
								((UndoEngine.UndoUnit.UndoEvent)this._events[j]).BeforeUndo(this._engine);
							}
							for (int k = i; k >= num; k--)
							{
								((UndoEngine.UndoUnit.UndoEvent)this._events[k]).Undo(this._engine);
							}
							i = num;
						}
						if (this._lastSelection != null)
						{
							ISelectionService selectionService = this._engine.GetService(typeof(ISelectionService)) as ISelectionService;
							if (selectionService != null)
							{
								string[] array = new string[this._lastSelection.Keys.Count];
								this._lastSelection.Keys.CopyTo(array, 0);
								ArrayList arrayList = new ArrayList(array.Length);
								foreach (string text in array)
								{
									if (text != null)
									{
										object obj = ((Container)this._lastSelection[text]).Components[text];
										if (obj != null)
										{
											arrayList.Add(obj);
										}
									}
								}
								selectionService.SetSelectedComponents(arrayList, SelectionTypes.Replace);
							}
						}
					}
					else
					{
						int count = this._events.Count;
						for (int m = 0; m < count; m++)
						{
							int num3 = m;
							int num4 = m;
							while (num4 < count && ((UndoEngine.UndoUnit.UndoEvent)this._events[num4]).CausesSideEffects)
							{
								num3 = num4;
								num4++;
							}
							for (int n = m; n <= num3; n++)
							{
								((UndoEngine.UndoUnit.UndoEvent)this._events[n]).BeforeUndo(this._engine);
							}
							for (int num5 = m; num5 <= num3; num5++)
							{
								((UndoEngine.UndoUnit.UndoEvent)this._events[num5]).Undo(this._engine);
							}
							m = num3;
						}
					}
				}
				this._reverse = !this._reverse;
			}

			// Token: 0x04001E38 RID: 7736
			private string _name;

			// Token: 0x04001E39 RID: 7737
			private UndoEngine _engine;

			// Token: 0x04001E3A RID: 7738
			private ArrayList _events;

			// Token: 0x04001E3B RID: 7739
			private ArrayList _changeEvents;

			// Token: 0x04001E3C RID: 7740
			private ArrayList _removeEvents;

			// Token: 0x04001E3D RID: 7741
			private ArrayList _ignoreAddingList;

			// Token: 0x04001E3E RID: 7742
			private ArrayList _ignoreAddedList;

			// Token: 0x04001E3F RID: 7743
			private bool _reverse;

			// Token: 0x04001E40 RID: 7744
			private Hashtable _lastSelection;

			// Token: 0x020005D9 RID: 1497
			private sealed class AddRemoveUndoEvent : UndoEngine.UndoUnit.UndoEvent
			{
				// Token: 0x06003468 RID: 13416 RVA: 0x0011D0AC File Offset: 0x0011B2AC
				public AddRemoveUndoEvent(UndoEngine engine, IComponent component, bool add)
				{
					this._componentName = component.Site.Name;
					this._nextUndoAdds = !add;
					this._openComponent = component;
					using (this._serializedData = engine._serializationService.CreateStore())
					{
						engine._serializationService.Serialize(this._serializedData, component);
					}
					this._committed = add;
				}

				// Token: 0x17000A22 RID: 2594
				// (get) Token: 0x06003469 RID: 13417 RVA: 0x0011D12C File Offset: 0x0011B32C
				internal bool Committed
				{
					get
					{
						return this._committed;
					}
				}

				// Token: 0x17000A23 RID: 2595
				// (get) Token: 0x0600346A RID: 13418 RVA: 0x0011D134 File Offset: 0x0011B334
				internal IComponent OpenComponent
				{
					get
					{
						return this._openComponent;
					}
				}

				// Token: 0x17000A24 RID: 2596
				// (get) Token: 0x0600346B RID: 13419 RVA: 0x0011D13C File Offset: 0x0011B33C
				internal bool NextUndoAdds
				{
					get
					{
						return this._nextUndoAdds;
					}
				}

				// Token: 0x0600346C RID: 13420 RVA: 0x0011D144 File Offset: 0x0011B344
				internal void Commit(UndoEngine engine)
				{
					if (!this.Committed)
					{
						this._committed = true;
					}
				}

				// Token: 0x0600346D RID: 13421 RVA: 0x0011D158 File Offset: 0x0011B358
				public override void Undo(UndoEngine engine)
				{
					if (this._nextUndoAdds)
					{
						IDesignerHost designerHost = engine.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost;
						if (designerHost != null)
						{
							engine._serializationService.DeserializeTo(this._serializedData, designerHost.Container);
						}
					}
					else
					{
						IDesignerHost designerHost2 = engine.GetRequiredService(typeof(IDesignerHost)) as IDesignerHost;
						IComponent component = designerHost2.Container.Components[this._componentName];
						if (component != null)
						{
							designerHost2.DestroyComponent(component);
						}
					}
					this._nextUndoAdds = !this._nextUndoAdds;
				}

				// Token: 0x0400230B RID: 8971
				private SerializationStore _serializedData;

				// Token: 0x0400230C RID: 8972
				private string _componentName;

				// Token: 0x0400230D RID: 8973
				private bool _nextUndoAdds;

				// Token: 0x0400230E RID: 8974
				private bool _committed;

				// Token: 0x0400230F RID: 8975
				private IComponent _openComponent;
			}

			// Token: 0x020005DA RID: 1498
			private sealed class ChangeUndoEvent : UndoEngine.UndoUnit.UndoEvent
			{
				// Token: 0x0600346E RID: 13422 RVA: 0x0011D1E8 File Offset: 0x0011B3E8
				public ChangeUndoEvent(UndoEngine engine, ComponentChangingEventArgs e, bool serializeBeforeState)
				{
					this._componentName = engine.GetName(e.Component, true);
					this._openComponent = e.Component;
					this._member = e.Member;
					if (serializeBeforeState)
					{
						this._before = this.Serialize(engine, this._openComponent, this._member);
					}
				}

				// Token: 0x17000A25 RID: 2597
				// (get) Token: 0x0600346F RID: 13423 RVA: 0x0011D242 File Offset: 0x0011B442
				public ComponentChangingEventArgs ComponentChangingEventArgs
				{
					get
					{
						return new ComponentChangingEventArgs(this._openComponent, this._member);
					}
				}

				// Token: 0x17000A26 RID: 2598
				// (get) Token: 0x06003470 RID: 13424 RVA: 0x00003B0F File Offset: 0x00001D0F
				public override bool CausesSideEffects
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17000A27 RID: 2599
				// (get) Token: 0x06003471 RID: 13425 RVA: 0x0011D255 File Offset: 0x0011B455
				public bool Committed
				{
					get
					{
						return this._openComponent == null;
					}
				}

				// Token: 0x17000A28 RID: 2600
				// (get) Token: 0x06003472 RID: 13426 RVA: 0x0011D260 File Offset: 0x0011B460
				public object OpenComponent
				{
					get
					{
						return this._openComponent;
					}
				}

				// Token: 0x06003473 RID: 13427 RVA: 0x0011D268 File Offset: 0x0011B468
				public override void BeforeUndo(UndoEngine engine)
				{
					if (!this._savedAfterState)
					{
						this._savedAfterState = true;
						this.SaveAfterState(engine);
					}
				}

				// Token: 0x06003474 RID: 13428 RVA: 0x0011D280 File Offset: 0x0011B480
				public bool ContainsChange(MemberDescriptor desc)
				{
					return this._member == null || (desc != null && desc.Equals(this._member));
				}

				// Token: 0x06003475 RID: 13429 RVA: 0x0011D29D File Offset: 0x0011B49D
				public void Commit(UndoEngine engine)
				{
					if (!this.Committed)
					{
						this._openComponent = null;
					}
				}

				// Token: 0x06003476 RID: 13430 RVA: 0x0011D2B0 File Offset: 0x0011B4B0
				private void SaveAfterState(UndoEngine engine)
				{
					object obj = null;
					IReferenceService referenceService = engine.GetService(typeof(IReferenceService)) as IReferenceService;
					if (referenceService != null)
					{
						obj = referenceService.GetReference(this._componentName);
					}
					else
					{
						IDesignerHost designerHost = engine.GetService(typeof(IDesignerHost)) as IDesignerHost;
						if (designerHost != null)
						{
							obj = designerHost.Container.Components[this._componentName];
						}
					}
					if (obj != null)
					{
						this._after = this.Serialize(engine, obj, this._member);
					}
				}

				// Token: 0x06003477 RID: 13431 RVA: 0x0011D330 File Offset: 0x0011B530
				private SerializationStore Serialize(UndoEngine engine, object component, MemberDescriptor member)
				{
					SerializationStore serializationStore2;
					SerializationStore serializationStore = serializationStore2 = engine._serializationService.CreateStore();
					try
					{
						if (member != null && !member.Attributes.Contains(DesignerSerializationVisibilityAttribute.Hidden))
						{
							engine._serializationService.SerializeMemberAbsolute(serializationStore, component, member);
						}
						else
						{
							engine._serializationService.SerializeAbsolute(serializationStore, component);
						}
					}
					finally
					{
						if (serializationStore2 != null)
						{
							((IDisposable)serializationStore2).Dispose();
						}
					}
					return serializationStore;
				}

				// Token: 0x06003478 RID: 13432 RVA: 0x0011D39C File Offset: 0x0011B59C
				public override void Undo(UndoEngine engine)
				{
					if (this._before != null)
					{
						IDesignerHost designerHost = engine.GetService(typeof(IDesignerHost)) as IDesignerHost;
						if (designerHost != null)
						{
							engine._serializationService.DeserializeTo(this._before, designerHost.Container);
						}
					}
					SerializationStore after = this._after;
					this._after = this._before;
					this._before = after;
				}

				// Token: 0x04002310 RID: 8976
				private object _openComponent;

				// Token: 0x04002311 RID: 8977
				private string _componentName;

				// Token: 0x04002312 RID: 8978
				private MemberDescriptor _member;

				// Token: 0x04002313 RID: 8979
				private SerializationStore _before;

				// Token: 0x04002314 RID: 8980
				private SerializationStore _after;

				// Token: 0x04002315 RID: 8981
				private bool _savedAfterState;
			}

			// Token: 0x020005DB RID: 1499
			private sealed class RenameUndoEvent : UndoEngine.UndoUnit.UndoEvent
			{
				// Token: 0x06003479 RID: 13433 RVA: 0x0011D3FB File Offset: 0x0011B5FB
				public RenameUndoEvent(string before, string after)
				{
					this._before = before;
					this._after = after;
				}

				// Token: 0x0600347A RID: 13434 RVA: 0x0011D414 File Offset: 0x0011B614
				public override void Undo(UndoEngine engine)
				{
					IComponent component = engine._host.Container.Components[this._after];
					if (component != null)
					{
						engine.ComponentChangeService.OnComponentChanging(component, null);
						component.Site.Name = this._before;
						string after = this._after;
						this._after = this._before;
						this._before = after;
					}
				}

				// Token: 0x04002316 RID: 8982
				private string _before;

				// Token: 0x04002317 RID: 8983
				private string _after;
			}

			// Token: 0x020005DC RID: 1500
			private abstract class UndoEvent
			{
				// Token: 0x17000A29 RID: 2601
				// (get) Token: 0x0600347B RID: 13435 RVA: 0x0000445B File Offset: 0x0000265B
				public virtual bool CausesSideEffects
				{
					get
					{
						return false;
					}
				}

				// Token: 0x0600347C RID: 13436 RVA: 0x00003937 File Offset: 0x00001B37
				public virtual void BeforeUndo(UndoEngine engine)
				{
				}

				// Token: 0x0600347D RID: 13437
				public abstract void Undo(UndoEngine engine);
			}
		}
	}
}
