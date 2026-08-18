using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design
{
	// Token: 0x020001D2 RID: 466
	internal sealed class SelectionService : ISelectionService, IDisposable
	{
		// Token: 0x0600114B RID: 4427 RVA: 0x0005F8B5 File Offset: 0x0005DAB5
		internal SelectionService(IServiceProvider provider)
		{
			this._provider = provider;
			this._state = default(BitVector32);
			this._events = new EventHandlerList();
			this._statusCommandUI = new StatusCommandUI(provider);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0005F8E8 File Offset: 0x0005DAE8
		internal void AddSelection(object sel)
		{
			if (this._selection == null)
			{
				this._selection = new ArrayList();
				IComponentChangeService componentChangeService = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				if (componentChangeService != null)
				{
					componentChangeService.ComponentRemoved += this.OnComponentRemove;
				}
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					designerHost.TransactionOpened += this.OnTransactionOpened;
					designerHost.TransactionClosed += this.OnTransactionClosed;
					if (designerHost.InTransaction)
					{
						this.OnTransactionOpened(designerHost, EventArgs.Empty);
					}
				}
			}
			if (!this._selection.Contains(sel))
			{
				this._selection.Add(sel);
			}
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0005F9A2 File Offset: 0x0005DBA2
		private void FlushSelectionChanges()
		{
			if (!this._state[SelectionService.StateTransaction] && this._state[SelectionService.StateTransactionChange])
			{
				this._state[SelectionService.StateTransactionChange] = false;
				this.OnSelectionChanged();
			}
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0005F9DF File Offset: 0x0005DBDF
		private object GetService(Type serviceType)
		{
			if (this._provider != null)
			{
				return this._provider.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0005F9F7 File Offset: 0x0005DBF7
		private void OnComponentRemove(object sender, ComponentEventArgs ce)
		{
			if (this._selection != null && this._selection.Contains(ce.Component))
			{
				this.RemoveSelection(ce.Component);
				this.OnSelectionChanged();
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0005FA28 File Offset: 0x0005DC28
		private void OnSelectionChanged()
		{
			if (this._state[SelectionService.StateTransaction])
			{
				this._state[SelectionService.StateTransactionChange] = true;
				return;
			}
			EventHandler eventHandler = this._events[SelectionService.EventSelectionChanging] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
			this.UpdateHelpKeyword(true);
			eventHandler = (this._events[SelectionService.EventSelectionChanged] as EventHandler);
			if (eventHandler != null)
			{
				try
				{
					eventHandler(this, EventArgs.Empty);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0005FAC0 File Offset: 0x0005DCC0
		private void OnTransactionClosed(object sender, DesignerTransactionCloseEventArgs e)
		{
			if (e.LastTransaction)
			{
				this._state[SelectionService.StateTransaction] = false;
				this.FlushSelectionChanges();
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0005FAE1 File Offset: 0x0005DCE1
		private void OnTransactionOpened(object sender, EventArgs e)
		{
			this._state[SelectionService.StateTransaction] = true;
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0005FAF4 File Offset: 0x0005DCF4
		internal void RemoveSelection(object sel)
		{
			if (this._selection != null)
			{
				this._selection.Remove(sel);
			}
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0005FB0A File Offset: 0x0005DD0A
		private void ApplicationIdle(object source, EventArgs args)
		{
			this.UpdateHelpKeyword(false);
			Application.Idle -= this.ApplicationIdle;
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0005FB24 File Offset: 0x0005DD24
		private void UpdateHelpKeyword(bool tryLater)
		{
			IHelpService helpService = this.GetService(typeof(IHelpService)) as IHelpService;
			if (helpService == null)
			{
				if (tryLater)
				{
					Application.Idle += this.ApplicationIdle;
				}
				return;
			}
			if (this._contextAttributes != null)
			{
				foreach (string value in this._contextAttributes)
				{
					helpService.RemoveContextAttribute("Keyword", value);
				}
				this._contextAttributes = null;
			}
			helpService.RemoveContextAttribute("Selection", SelectionService.SelectionKeywords[(int)this._contextKeyword]);
			bool flag = false;
			if (this._selection.Count == 0)
			{
				flag = true;
			}
			else if (this._selection.Count == 1)
			{
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null && this._selection.Contains(designerHost.RootComponent))
				{
					flag = true;
				}
			}
			this._contextAttributes = new string[this._selection.Count];
			for (int j = 0; j < this._selection.Count; j++)
			{
				object component = this._selection[j];
				string text = TypeDescriptor.GetClassName(component);
				HelpKeywordAttribute helpKeywordAttribute = (HelpKeywordAttribute)TypeDescriptor.GetAttributes(component)[typeof(HelpKeywordAttribute)];
				if (helpKeywordAttribute != null && !helpKeywordAttribute.IsDefaultAttribute())
				{
					text = helpKeywordAttribute.HelpKeyword;
				}
				this._contextAttributes[j] = text;
			}
			HelpKeywordType keywordType = flag ? HelpKeywordType.GeneralKeyword : HelpKeywordType.F1Keyword;
			foreach (string value2 in this._contextAttributes)
			{
				helpService.AddContextAttribute("Keyword", value2, keywordType);
			}
			int num = this._selection.Count;
			if (num == 1 && flag)
			{
				num--;
			}
			this._contextKeyword = (short)Math.Min(num, SelectionService.SelectionKeywords.Length - 1);
			helpService.AddContextAttribute("Selection", SelectionService.SelectionKeywords[(int)this._contextKeyword], HelpKeywordType.FilterKeyword);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0005FD0C File Offset: 0x0005DF0C
		void IDisposable.Dispose()
		{
			if (this._selection != null)
			{
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					designerHost.TransactionOpened -= this.OnTransactionOpened;
					designerHost.TransactionClosed -= this.OnTransactionClosed;
					if (designerHost.InTransaction)
					{
						this.OnTransactionClosed(designerHost, new DesignerTransactionCloseEventArgs(true, true));
					}
				}
				IComponentChangeService componentChangeService = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				if (componentChangeService != null)
				{
					componentChangeService.ComponentRemoved -= this.OnComponentRemove;
				}
				this._selection.Clear();
			}
			this._statusCommandUI = null;
			this._provider = null;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0005FDBB File Offset: 0x0005DFBB
		object ISelectionService.PrimarySelection
		{
			get
			{
				if (this._selection != null && this._selection.Count > 0)
				{
					return this._selection[0];
				}
				return null;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0005FDE1 File Offset: 0x0005DFE1
		int ISelectionService.SelectionCount
		{
			get
			{
				if (this._selection != null)
				{
					return this._selection.Count;
				}
				return 0;
			}
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06001159 RID: 4441 RVA: 0x0005FDF8 File Offset: 0x0005DFF8
		// (remove) Token: 0x0600115A RID: 4442 RVA: 0x0005FE0B File Offset: 0x0005E00B
		event EventHandler ISelectionService.SelectionChanged
		{
			add
			{
				this._events.AddHandler(SelectionService.EventSelectionChanged, value);
			}
			remove
			{
				this._events.RemoveHandler(SelectionService.EventSelectionChanged, value);
			}
		}

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x0600115B RID: 4443 RVA: 0x0005FE1E File Offset: 0x0005E01E
		// (remove) Token: 0x0600115C RID: 4444 RVA: 0x0005FE31 File Offset: 0x0005E031
		event EventHandler ISelectionService.SelectionChanging
		{
			add
			{
				this._events.AddHandler(SelectionService.EventSelectionChanging, value);
			}
			remove
			{
				this._events.RemoveHandler(SelectionService.EventSelectionChanging, value);
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0005FE44 File Offset: 0x0005E044
		bool ISelectionService.GetComponentSelected(object component)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return this._selection != null && this._selection.Contains(component);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0005FE6C File Offset: 0x0005E06C
		ICollection ISelectionService.GetSelectedComponents()
		{
			if (this._selection != null)
			{
				object[] array = new object[this._selection.Count];
				this._selection.CopyTo(array, 0);
				return array;
			}
			return new object[0];
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0005FEA7 File Offset: 0x0005E0A7
		void ISelectionService.SetSelectedComponents(ICollection components)
		{
			((ISelectionService)this).SetSelectedComponents(components, SelectionTypes.Auto);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0005FEB4 File Offset: 0x0005E0B4
		void ISelectionService.SetSelectedComponents(ICollection components, SelectionTypes selectionType)
		{
			bool flag = (selectionType & SelectionTypes.Toggle) == SelectionTypes.Toggle;
			bool flag2 = (selectionType & SelectionTypes.Click) == SelectionTypes.Click;
			bool flag3 = (selectionType & SelectionTypes.Add) == SelectionTypes.Add;
			bool flag4 = (selectionType & SelectionTypes.Remove) == SelectionTypes.Remove;
			bool flag5 = (selectionType & SelectionTypes.Replace) == SelectionTypes.Replace;
			bool flag6 = !flag && !flag3 && !flag4 && !flag5;
			if (components == null)
			{
				components = new object[0];
			}
			if (flag6)
			{
				flag = ((Control.ModifierKeys & (Keys.Shift | Keys.Control)) > Keys.None);
				flag3 |= (Control.ModifierKeys == Keys.Shift);
				if (flag || flag3)
				{
					flag2 = false;
				}
			}
			bool flag7 = false;
			object obj = null;
			if (flag2 && 1 == components.Count)
			{
				using (IEnumerator enumerator = components.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						obj = obj2;
						if (obj2 == null)
						{
							throw new ArgumentNullException("components");
						}
					}
				}
			}
			int num;
			if (obj != null && this._selection != null && (num = this._selection.IndexOf(obj)) != -1)
			{
				if (num != 0)
				{
					object value = this._selection[0];
					this._selection[0] = this._selection[num];
					this._selection[num] = value;
					flag7 = true;
				}
			}
			else
			{
				if (!flag && !flag3 && !flag4 && this._selection != null)
				{
					object[] array = new object[this._selection.Count];
					this._selection.CopyTo(array, 0);
					foreach (object obj3 in array)
					{
						bool flag8 = true;
						foreach (object obj4 in components)
						{
							if (obj4 == null)
							{
								throw new ArgumentNullException("components");
							}
							if (obj4 == obj3)
							{
								flag8 = false;
								break;
							}
						}
						if (flag8)
						{
							this.RemoveSelection(obj3);
							flag7 = true;
						}
					}
				}
				foreach (object obj5 in components)
				{
					if (obj5 == null)
					{
						throw new ArgumentNullException("components");
					}
					if (this._selection != null && this._selection.Contains(obj5))
					{
						if (flag || flag4)
						{
							this.RemoveSelection(obj5);
							flag7 = true;
						}
					}
					else if (!flag4)
					{
						this.AddSelection(obj5);
						flag7 = true;
					}
				}
			}
			if (flag7)
			{
				if (this._selection.Count > 0)
				{
					this._statusCommandUI.SetStatusInformation(this._selection[0] as Component);
				}
				else
				{
					this._statusCommandUI.SetStatusInformation(Rectangle.Empty);
				}
				this.OnSelectionChanged();
			}
		}

		// Token: 0x040009BB RID: 2491
		private static readonly string[] SelectionKeywords = new string[]
		{
			"None",
			"Single",
			"Multiple"
		};

		// Token: 0x040009BC RID: 2492
		private static readonly int StateTransaction = BitVector32.CreateMask();

		// Token: 0x040009BD RID: 2493
		private static readonly int StateTransactionChange = BitVector32.CreateMask(SelectionService.StateTransaction);

		// Token: 0x040009BE RID: 2494
		private static readonly object EventSelectionChanging = new object();

		// Token: 0x040009BF RID: 2495
		private static readonly object EventSelectionChanged = new object();

		// Token: 0x040009C0 RID: 2496
		private IServiceProvider _provider;

		// Token: 0x040009C1 RID: 2497
		private BitVector32 _state;

		// Token: 0x040009C2 RID: 2498
		private EventHandlerList _events;

		// Token: 0x040009C3 RID: 2499
		private ArrayList _selection;

		// Token: 0x040009C4 RID: 2500
		private string[] _contextAttributes;

		// Token: 0x040009C5 RID: 2501
		private short _contextKeyword;

		// Token: 0x040009C6 RID: 2502
		private StatusCommandUI _statusCommandUI;
	}
}
