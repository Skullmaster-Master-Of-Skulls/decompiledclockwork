using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design
{
	// Token: 0x0200056B RID: 1387
	internal sealed class SelectionService : ISelectionService, IDisposable
	{
		// Token: 0x060030F0 RID: 12528 RVA: 0x001149AF File Offset: 0x001139AF
		internal SelectionService(IServiceProvider provider)
		{
			this._provider = provider;
			this._state = default(BitVector32);
			this._events = new EventHandlerList();
			this._statusCommandUI = new StatusCommandUI(provider);
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x001149E4 File Offset: 0x001139E4
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

		// Token: 0x060030F2 RID: 12530 RVA: 0x00114A9E File Offset: 0x00113A9E
		private void FlushSelectionChanges()
		{
			if (!this._state[SelectionService.StateTransaction] && this._state[SelectionService.StateTransactionChange])
			{
				this._state[SelectionService.StateTransactionChange] = false;
				this.OnSelectionChanged();
			}
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x00114ADB File Offset: 0x00113ADB
		private object GetService(Type serviceType)
		{
			if (this._provider != null)
			{
				return this._provider.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x00114AF3 File Offset: 0x00113AF3
		private void OnComponentRemove(object sender, ComponentEventArgs ce)
		{
			if (this._selection != null && this._selection.Contains(ce.Component))
			{
				this.RemoveSelection(ce.Component);
				this.OnSelectionChanged();
			}
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x00114B24 File Offset: 0x00113B24
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

		// Token: 0x060030F6 RID: 12534 RVA: 0x00114BBC File Offset: 0x00113BBC
		private void OnTransactionClosed(object sender, DesignerTransactionCloseEventArgs e)
		{
			if (e.LastTransaction)
			{
				this._state[SelectionService.StateTransaction] = false;
				this.FlushSelectionChanges();
			}
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x00114BDD File Offset: 0x00113BDD
		private void OnTransactionOpened(object sender, EventArgs e)
		{
			this._state[SelectionService.StateTransaction] = true;
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x00114BF0 File Offset: 0x00113BF0
		internal void RemoveSelection(object sel)
		{
			if (this._selection != null)
			{
				this._selection.Remove(sel);
			}
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x00114C06 File Offset: 0x00113C06
		private void ApplicationIdle(object source, EventArgs args)
		{
			this.UpdateHelpKeyword(false);
			Application.Idle -= this.ApplicationIdle;
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x00114C20 File Offset: 0x00113C20
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

		// Token: 0x060030FB RID: 12539 RVA: 0x00114E08 File Offset: 0x00113E08
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

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x00114EB7 File Offset: 0x00113EB7
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

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060030FD RID: 12541 RVA: 0x00114EDD File Offset: 0x00113EDD
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

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x060030FE RID: 12542 RVA: 0x00114EF4 File Offset: 0x00113EF4
		// (remove) Token: 0x060030FF RID: 12543 RVA: 0x00114F07 File Offset: 0x00113F07
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

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06003100 RID: 12544 RVA: 0x00114F1A File Offset: 0x00113F1A
		// (remove) Token: 0x06003101 RID: 12545 RVA: 0x00114F2D File Offset: 0x00113F2D
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

		// Token: 0x06003102 RID: 12546 RVA: 0x00114F40 File Offset: 0x00113F40
		bool ISelectionService.GetComponentSelected(object component)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return this._selection != null && this._selection.Contains(component);
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x00114F68 File Offset: 0x00113F68
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

		// Token: 0x06003104 RID: 12548 RVA: 0x00114FA3 File Offset: 0x00113FA3
		void ISelectionService.SetSelectedComponents(ICollection components)
		{
			((ISelectionService)this).SetSelectedComponents(components, SelectionTypes.Auto);
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x00114FB0 File Offset: 0x00113FB0
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
							if (object.ReferenceEquals(obj4, obj3))
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

		// Token: 0x040020C7 RID: 8391
		private static readonly string[] SelectionKeywords = new string[]
		{
			"None",
			"Single",
			"Multiple"
		};

		// Token: 0x040020C8 RID: 8392
		private static readonly int StateTransaction = BitVector32.CreateMask();

		// Token: 0x040020C9 RID: 8393
		private static readonly int StateTransactionChange = BitVector32.CreateMask(SelectionService.StateTransaction);

		// Token: 0x040020CA RID: 8394
		private static readonly object EventSelectionChanging = new object();

		// Token: 0x040020CB RID: 8395
		private static readonly object EventSelectionChanged = new object();

		// Token: 0x040020CC RID: 8396
		private IServiceProvider _provider;

		// Token: 0x040020CD RID: 8397
		private BitVector32 _state;

		// Token: 0x040020CE RID: 8398
		private EventHandlerList _events;

		// Token: 0x040020CF RID: 8399
		private ArrayList _selection;

		// Token: 0x040020D0 RID: 8400
		private string[] _contextAttributes;

		// Token: 0x040020D1 RID: 8401
		private short _contextKeyword;

		// Token: 0x040020D2 RID: 8402
		private StatusCommandUI _statusCommandUI;
	}
}
