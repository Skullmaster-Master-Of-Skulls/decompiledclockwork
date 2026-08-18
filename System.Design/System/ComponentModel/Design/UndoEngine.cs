using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Diagnostics;
using System.Reflection;

namespace System.ComponentModel.Design
{
	// Token: 0x0200056D RID: 1389
	public abstract class UndoEngine : IDisposable
	{
		// Token: 0x0600310C RID: 12556 RVA: 0x00115440 File Offset: 0x00114440
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

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x0600310D RID: 12557 RVA: 0x0011559A File Offset: 0x0011459A
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

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x001155BC File Offset: 0x001145BC
		public bool UndoInProgress
		{
			get
			{
				return this._executingUnit != null;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x0600310F RID: 12559 RVA: 0x001155CA File Offset: 0x001145CA
		// (set) Token: 0x06003110 RID: 12560 RVA: 0x001155D2 File Offset: 0x001145D2
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

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06003111 RID: 12561 RVA: 0x001155DB File Offset: 0x001145DB
		// (remove) Token: 0x06003112 RID: 12562 RVA: 0x001155F4 File Offset: 0x001145F4
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

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06003113 RID: 12563 RVA: 0x0011560D File Offset: 0x0011460D
		// (remove) Token: 0x06003114 RID: 12564 RVA: 0x00115626 File Offset: 0x00114626
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

		// Token: 0x06003115 RID: 12565
		protected abstract void AddUndoUnit(UndoEngine.UndoUnit unit);

		// Token: 0x06003116 RID: 12566 RVA: 0x00115640 File Offset: 0x00114640
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

		// Token: 0x06003117 RID: 12567 RVA: 0x001156C4 File Offset: 0x001146C4
		protected virtual UndoEngine.UndoUnit CreateUndoUnit(string name, bool primary)
		{
			return new UndoEngine.UndoUnit(this, name);
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x001156CD File Offset: 0x001146CD
		internal IComponentChangeService ComponentChangeService
		{
			get
			{
				return this._componentChangeService;
			}
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x001156D5 File Offset: 0x001146D5
		protected virtual void DiscardUndoUnit(UndoEngine.UndoUnit unit)
		{
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x001156D7 File Offset: 0x001146D7
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x001156E0 File Offset: 0x001146E0
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

		// Token: 0x0600311C RID: 12572 RVA: 0x001157DC File Offset: 0x001147DC
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

		// Token: 0x0600311D RID: 12573 RVA: 0x0011584C File Offset: 0x0011484C
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

		// Token: 0x0600311E RID: 12574 RVA: 0x00115893 File Offset: 0x00114893
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

		// Token: 0x0600311F RID: 12575 RVA: 0x001158BC File Offset: 0x001148BC
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

		// Token: 0x06003120 RID: 12576 RVA: 0x00115924 File Offset: 0x00114924
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

		// Token: 0x06003121 RID: 12577 RVA: 0x001159E8 File Offset: 0x001149E8
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

		// Token: 0x06003122 RID: 12578 RVA: 0x00115A50 File Offset: 0x00114A50
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

		// Token: 0x06003123 RID: 12579 RVA: 0x00115B64 File Offset: 0x00114B64
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

		// Token: 0x06003124 RID: 12580 RVA: 0x00115C60 File Offset: 0x00114C60
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
								if (obj3 != null && object.ReferenceEquals(obj3, e.Component))
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

		// Token: 0x06003125 RID: 12581 RVA: 0x00115EF8 File Offset: 0x00114EF8
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

		// Token: 0x06003126 RID: 12582 RVA: 0x00115FAC File Offset: 0x00114FAC
		private void OnTransactionClosed(object sender, DesignerTransactionCloseEventArgs e)
		{
			if (this._executingUnit == null && this.CurrentUnit != null)
			{
				UndoEngine.PopUnitReason reason = e.TransactionCommitted ? UndoEngine.PopUnitReason.TransactionCommit : UndoEngine.PopUnitReason.TransactionCancel;
				this.CheckPopUnit(reason);
			}
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x00115FDD File Offset: 0x00114FDD
		private void OnTransactionOpening(object sender, EventArgs e)
		{
			if (this._enabled && this._executingUnit == null)
			{
				this._unitStack.Push(this.CreateUndoUnit(this._host.TransactionDescription, this._unitStack.Count == 0));
			}
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x00116019 File Offset: 0x00115019
		protected virtual void OnUndoing(EventArgs e)
		{
			if (this._undoingEvent != null)
			{
				this._undoingEvent(this, e);
			}
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x00116030 File Offset: 0x00115030
		protected virtual void OnUndone(EventArgs e)
		{
			if (this._undoneEvent != null)
			{
				this._undoneEvent(this, e);
			}
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x00116047 File Offset: 0x00115047
		[Conditional("DEBUG")]
		private static void Trace(string text, params object[] values)
		{
		}

		// Token: 0x040020D3 RID: 8403
		private static TraceSwitch traceUndo = new TraceSwitch("UndoEngine", "Trace UndoRedo");

		// Token: 0x040020D4 RID: 8404
		private IServiceProvider _provider;

		// Token: 0x040020D5 RID: 8405
		private Stack _unitStack;

		// Token: 0x040020D6 RID: 8406
		private UndoEngine.UndoUnit _executingUnit;

		// Token: 0x040020D7 RID: 8407
		private IDesignerHost _host;

		// Token: 0x040020D8 RID: 8408
		private ComponentSerializationService _serializationService;

		// Token: 0x040020D9 RID: 8409
		private EventHandler _undoingEvent;

		// Token: 0x040020DA RID: 8410
		private EventHandler _undoneEvent;

		// Token: 0x040020DB RID: 8411
		private IComponentChangeService _componentChangeService;

		// Token: 0x040020DC RID: 8412
		private Dictionary<IComponent, List<UndoEngine.ReferencingComponent>> _refToRemovedComponent;

		// Token: 0x040020DD RID: 8413
		private bool _enabled;

		// Token: 0x0200056E RID: 1390
		private enum PopUnitReason
		{
			// Token: 0x040020DF RID: 8415
			Normal,
			// Token: 0x040020E0 RID: 8416
			TransactionCommit,
			// Token: 0x040020E1 RID: 8417
			TransactionCancel
		}

		// Token: 0x0200056F RID: 1391
		private struct ReferencingComponent
		{
			// Token: 0x0600312C RID: 12588 RVA: 0x0011605F File Offset: 0x0011505F
			public ReferencingComponent(IComponent component, MemberDescriptor member)
			{
				this.component = component;
				this.member = member;
			}

			// Token: 0x040020E2 RID: 8418
			public IComponent component;

			// Token: 0x040020E3 RID: 8419
			public MemberDescriptor member;
		}

		// Token: 0x02000570 RID: 1392
		protected class UndoUnit
		{
			// Token: 0x0600312D RID: 12589 RVA: 0x00116070 File Offset: 0x00115070
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

			// Token: 0x17000930 RID: 2352
			// (get) Token: 0x0600312E RID: 12590 RVA: 0x00116158 File Offset: 0x00115158
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x17000931 RID: 2353
			// (get) Token: 0x0600312F RID: 12591 RVA: 0x00116160 File Offset: 0x00115160
			public virtual bool IsEmpty
			{
				get
				{
					return this._events == null || this._events.Count == 0;
				}
			}

			// Token: 0x17000932 RID: 2354
			// (get) Token: 0x06003130 RID: 12592 RVA: 0x0011617A File Offset: 0x0011517A
			protected UndoEngine UndoEngine
			{
				get
				{
					return this._engine;
				}
			}

			// Token: 0x06003131 RID: 12593 RVA: 0x00116182 File Offset: 0x00115182
			private void AddEvent(UndoEngine.UndoUnit.UndoEvent e)
			{
				if (this._events == null)
				{
					this._events = new ArrayList();
				}
				this._events.Add(e);
			}

			// Token: 0x06003132 RID: 12594 RVA: 0x001161A4 File Offset: 0x001151A4
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

			// Token: 0x06003133 RID: 12595 RVA: 0x00116284 File Offset: 0x00115284
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

			// Token: 0x06003134 RID: 12596 RVA: 0x0011630B File Offset: 0x0011530B
			public virtual void ComponentAdding(ComponentEventArgs e)
			{
				if (this._ignoreAddingList == null)
				{
					this._ignoreAddingList = new ArrayList();
				}
				this._ignoreAddingList.Add(e.Component);
			}

			// Token: 0x06003135 RID: 12597 RVA: 0x00116332 File Offset: 0x00115332
			private static bool ChangeEventsSymmetric(ComponentChangingEventArgs changing, ComponentChangedEventArgs changed)
			{
				return changing != null && changed != null && changing.Component == changed.Component && changing.Member == changed.Member;
			}

			// Token: 0x06003136 RID: 12598 RVA: 0x0011635C File Offset: 0x0011535C
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

			// Token: 0x06003137 RID: 12599 RVA: 0x001163FC File Offset: 0x001153FC
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

			// Token: 0x06003138 RID: 12600 RVA: 0x001164A8 File Offset: 0x001154A8
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

			// Token: 0x06003139 RID: 12601 RVA: 0x0011663C File Offset: 0x0011563C
			public virtual void ComponentRemoved(ComponentEventArgs e)
			{
				if (this._events != null)
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

			// Token: 0x0600313A RID: 12602 RVA: 0x0011671C File Offset: 0x0011571C
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

			// Token: 0x0600313B RID: 12603 RVA: 0x00116798 File Offset: 0x00115798
			public virtual void ComponentRename(ComponentRenameEventArgs e)
			{
				this.AddEvent(new UndoEngine.UndoUnit.RenameUndoEvent(e.OldName, e.NewName));
			}

			// Token: 0x0600313C RID: 12604 RVA: 0x001167B1 File Offset: 0x001157B1
			protected object GetService(Type serviceType)
			{
				return this._engine.GetService(serviceType);
			}

			// Token: 0x0600313D RID: 12605 RVA: 0x001167BF File Offset: 0x001157BF
			public override string ToString()
			{
				return this.Name;
			}

			// Token: 0x0600313E RID: 12606 RVA: 0x001167C8 File Offset: 0x001157C8
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

			// Token: 0x0600313F RID: 12607 RVA: 0x00116870 File Offset: 0x00115870
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

			// Token: 0x040020E4 RID: 8420
			private string _name;

			// Token: 0x040020E5 RID: 8421
			private UndoEngine _engine;

			// Token: 0x040020E6 RID: 8422
			private ArrayList _events;

			// Token: 0x040020E7 RID: 8423
			private ArrayList _changeEvents;

			// Token: 0x040020E8 RID: 8424
			private ArrayList _removeEvents;

			// Token: 0x040020E9 RID: 8425
			private ArrayList _ignoreAddingList;

			// Token: 0x040020EA RID: 8426
			private ArrayList _ignoreAddedList;

			// Token: 0x040020EB RID: 8427
			private bool _reverse;

			// Token: 0x040020EC RID: 8428
			private Hashtable _lastSelection;

			// Token: 0x02000571 RID: 1393
			private abstract class UndoEvent
			{
				// Token: 0x17000933 RID: 2355
				// (get) Token: 0x06003140 RID: 12608 RVA: 0x00116ABA File Offset: 0x00115ABA
				public virtual bool CausesSideEffects
				{
					get
					{
						return false;
					}
				}

				// Token: 0x06003141 RID: 12609 RVA: 0x00116ABD File Offset: 0x00115ABD
				public virtual void BeforeUndo(UndoEngine engine)
				{
				}

				// Token: 0x06003142 RID: 12610
				public abstract void Undo(UndoEngine engine);
			}

			// Token: 0x02000572 RID: 1394
			private sealed class AddRemoveUndoEvent : UndoEngine.UndoUnit.UndoEvent
			{
				// Token: 0x06003144 RID: 12612 RVA: 0x00116AC8 File Offset: 0x00115AC8
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

				// Token: 0x17000934 RID: 2356
				// (get) Token: 0x06003145 RID: 12613 RVA: 0x00116B48 File Offset: 0x00115B48
				internal bool Committed
				{
					get
					{
						return this._committed;
					}
				}

				// Token: 0x17000935 RID: 2357
				// (get) Token: 0x06003146 RID: 12614 RVA: 0x00116B50 File Offset: 0x00115B50
				internal IComponent OpenComponent
				{
					get
					{
						return this._openComponent;
					}
				}

				// Token: 0x17000936 RID: 2358
				// (get) Token: 0x06003147 RID: 12615 RVA: 0x00116B58 File Offset: 0x00115B58
				internal bool NextUndoAdds
				{
					get
					{
						return this._nextUndoAdds;
					}
				}

				// Token: 0x06003148 RID: 12616 RVA: 0x00116B60 File Offset: 0x00115B60
				internal void Commit(UndoEngine engine)
				{
					if (!this.Committed)
					{
						this._committed = true;
					}
				}

				// Token: 0x06003149 RID: 12617 RVA: 0x00116B74 File Offset: 0x00115B74
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

				// Token: 0x040020ED RID: 8429
				private SerializationStore _serializedData;

				// Token: 0x040020EE RID: 8430
				private string _componentName;

				// Token: 0x040020EF RID: 8431
				private bool _nextUndoAdds;

				// Token: 0x040020F0 RID: 8432
				private bool _committed;

				// Token: 0x040020F1 RID: 8433
				private IComponent _openComponent;
			}

			// Token: 0x02000573 RID: 1395
			private sealed class ChangeUndoEvent : UndoEngine.UndoUnit.UndoEvent
			{
				// Token: 0x0600314A RID: 12618 RVA: 0x00116C04 File Offset: 0x00115C04
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

				// Token: 0x17000937 RID: 2359
				// (get) Token: 0x0600314B RID: 12619 RVA: 0x00116C5E File Offset: 0x00115C5E
				public ComponentChangingEventArgs ComponentChangingEventArgs
				{
					get
					{
						return new ComponentChangingEventArgs(this._openComponent, this._member);
					}
				}

				// Token: 0x17000938 RID: 2360
				// (get) Token: 0x0600314C RID: 12620 RVA: 0x00116C71 File Offset: 0x00115C71
				public override bool CausesSideEffects
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17000939 RID: 2361
				// (get) Token: 0x0600314D RID: 12621 RVA: 0x00116C74 File Offset: 0x00115C74
				public bool Committed
				{
					get
					{
						return this._openComponent == null;
					}
				}

				// Token: 0x1700093A RID: 2362
				// (get) Token: 0x0600314E RID: 12622 RVA: 0x00116C7F File Offset: 0x00115C7F
				public object OpenComponent
				{
					get
					{
						return this._openComponent;
					}
				}

				// Token: 0x0600314F RID: 12623 RVA: 0x00116C87 File Offset: 0x00115C87
				public override void BeforeUndo(UndoEngine engine)
				{
					if (!this._savedAfterState)
					{
						this._savedAfterState = true;
						this.SaveAfterState(engine);
					}
				}

				// Token: 0x06003150 RID: 12624 RVA: 0x00116C9F File Offset: 0x00115C9F
				public bool ContainsChange(MemberDescriptor desc)
				{
					return this._member == null || (desc != null && desc.Equals(this._member));
				}

				// Token: 0x06003151 RID: 12625 RVA: 0x00116CBC File Offset: 0x00115CBC
				public void Commit(UndoEngine engine)
				{
					if (!this.Committed)
					{
						this._openComponent = null;
					}
				}

				// Token: 0x06003152 RID: 12626 RVA: 0x00116CD0 File Offset: 0x00115CD0
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

				// Token: 0x06003153 RID: 12627 RVA: 0x00116D50 File Offset: 0x00115D50
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

				// Token: 0x06003154 RID: 12628 RVA: 0x00116DBC File Offset: 0x00115DBC
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

				// Token: 0x040020F2 RID: 8434
				private object _openComponent;

				// Token: 0x040020F3 RID: 8435
				private string _componentName;

				// Token: 0x040020F4 RID: 8436
				private MemberDescriptor _member;

				// Token: 0x040020F5 RID: 8437
				private SerializationStore _before;

				// Token: 0x040020F6 RID: 8438
				private SerializationStore _after;

				// Token: 0x040020F7 RID: 8439
				private bool _savedAfterState;
			}

			// Token: 0x02000574 RID: 1396
			private sealed class RenameUndoEvent : UndoEngine.UndoUnit.UndoEvent
			{
				// Token: 0x06003155 RID: 12629 RVA: 0x00116E1B File Offset: 0x00115E1B
				public RenameUndoEvent(string before, string after)
				{
					this._before = before;
					this._after = after;
				}

				// Token: 0x06003156 RID: 12630 RVA: 0x00116E34 File Offset: 0x00115E34
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

				// Token: 0x040020F8 RID: 8440
				private string _before;

				// Token: 0x040020F9 RID: 8441
				private string _after;
			}
		}
	}
}
