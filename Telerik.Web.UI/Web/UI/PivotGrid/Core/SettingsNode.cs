using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x0200067B RID: 1659
	[DataContract]
	public abstract class SettingsNode : Cloneable, INotifyPropertyChanged, ISupportInitialize, IObservableServiceProvider, IServiceProvider, IEditable
	{
		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x06003C7F RID: 15487 RVA: 0x000C3DD8 File Offset: 0x000C1FD8
		// (remove) Token: 0x06003C80 RID: 15488 RVA: 0x000C3E10 File Offset: 0x000C2010
		public event EventHandler<SettingsChangedEventArgs> SettingsChanged;

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x06003C81 RID: 15489 RVA: 0x000C3E45 File Offset: 0x000C2045
		// (remove) Token: 0x06003C82 RID: 15490 RVA: 0x000C3E88 File Offset: 0x000C2088
		public event EventHandler<EventArgs> ServicesChanged
		{
			add
			{
				if (this.servicesChanged == null && this.parent != null)
				{
					this.parent.ServicesChanged += this.OnParentServicesChanged;
				}
				this.servicesChanged = (EventHandler<EventArgs>)Delegate.Combine(this.servicesChanged, value);
			}
			remove
			{
				bool flag = this.servicesChanged == null;
				this.servicesChanged = (EventHandler<EventArgs>)Delegate.Remove(this.servicesChanged, value);
				if (!flag && this.servicesChanged == null && this.parent != null)
				{
					this.parent.ServicesChanged -= this.OnParentServicesChanged;
				}
			}
		}

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x06003C83 RID: 15491 RVA: 0x000C3EE0 File Offset: 0x000C20E0
		// (remove) Token: 0x06003C84 RID: 15492 RVA: 0x000C3F18 File Offset: 0x000C2118
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x06003C85 RID: 15493 RVA: 0x000C3F4D File Offset: 0x000C214D
		private bool IsInEditScope
		{
			get
			{
				return this.editScopeLevel != 0 || this.isInitializing;
			}
		}

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x06003C86 RID: 15494 RVA: 0x000C3F5F File Offset: 0x000C215F
		// (set) Token: 0x06003C87 RID: 15495 RVA: 0x000C3F68 File Offset: 0x000C2168
		public SettingsNode Parent
		{
			get
			{
				return this.parent;
			}
			internal set
			{
				if (this.parent != value)
				{
					if (this.servicesChanged != null && this.parent != null)
					{
						this.parent.ServicesChanged -= this.OnParentServicesChanged;
					}
					this.parent = value;
					this.NotifyServicesChanged();
					if (this.servicesChanged != null && this.parent != null)
					{
						this.parent.ServicesChanged += this.OnParentServicesChanged;
					}
					this.OnPropertyChanged("Parent");
				}
			}
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x000C3FE4 File Offset: 0x000C21E4
		protected internal void NotifyServicesChanged()
		{
			if (this.servicesChanged != null && !this.isInitializing)
			{
				this.servicesChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x000C4008 File Offset: 0x000C2208
		protected internal void NotifySettingsChanged(SettingsChangedEventArgs args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			args.OriginalSource = this;
			SettingsNode settingsNode = this;
			while (settingsNode != null && !settingsNode.IsInEditScope)
			{
				settingsNode.RaiseSettingsChanged(args);
				settingsNode = settingsNode.Parent;
			}
			if (settingsNode != null)
			{
				settingsNode.AccumulateChanges();
			}
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x000C4050 File Offset: 0x000C2250
		internal static void ValidateChildForAssignment(SettingsNode child)
		{
			if (child.Parent != null)
			{
				throw new InvalidOperationException("Node already is a child of another node.");
			}
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x000C4065 File Offset: 0x000C2265
		private void OnParentServicesChanged(object sender, EventArgs e)
		{
			this.servicesChanged(this, e);
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x000C4074 File Offset: 0x000C2274
		private void AccumulateChanges()
		{
			if (this.accumulatedChanges == null)
			{
				this.accumulatedChanges = new SettingsChangedEventArgs();
			}
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x000C4089 File Offset: 0x000C2289
		protected virtual void OnSettingsChanged(SettingsChangedEventArgs args)
		{
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x000C408B File Offset: 0x000C228B
		private void RaiseSettingsChanged(SettingsChangedEventArgs args)
		{
			this.OnSettingsChanged(args);
			if (this.SettingsChanged != null)
			{
				this.SettingsChanged(this, args);
			}
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x000C40A9 File Offset: 0x000C22A9
		public IDisposable BeginEdit()
		{
			return new SettingsNode.EditScope(this);
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x000C40B1 File Offset: 0x000C22B1
		public void BeginInit()
		{
			if (this.isInitializing)
			{
				throw new InvalidOperationException("Can not start new initialization while already in initialization.");
			}
			this.isInitializing = true;
		}

		// Token: 0x06003C91 RID: 15505 RVA: 0x000C40CD File Offset: 0x000C22CD
		public void EndInit()
		{
			if (!this.isInitializing)
			{
				throw new InvalidOperationException("Can not end initialization because initialization was not started.");
			}
			this.isInitializing = false;
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x000C40EC File Offset: 0x000C22EC
		public object GetService(Type serviceType)
		{
			for (SettingsNode settingsNode = this; settingsNode != null; settingsNode = settingsNode.Parent)
			{
				object serviceOverride = settingsNode.GetServiceOverride(serviceType);
				if (serviceOverride != null)
				{
					return serviceOverride;
				}
			}
			return null;
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x000C4115 File Offset: 0x000C2315
		protected void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x000C4131 File Offset: 0x000C2331
		protected internal void RemoveSettingsChild(SettingsNode child)
		{
			if (this != child.Parent)
			{
				throw new InvalidOperationException("Trying to remove child node from parent that is not the actual parent of the child.");
			}
			child.Parent = null;
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x000C414E File Offset: 0x000C234E
		protected internal void AddSettingsChild(SettingsNode child)
		{
			SettingsNode.ValidateChildForAssignment(child);
			child.Parent = this;
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x000C415D File Offset: 0x000C235D
		protected virtual object GetServiceOverride(Type serviceType)
		{
			if (serviceType.IsAssignableFrom(base.GetType()))
			{
				return this;
			}
			return null;
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x000C4170 File Offset: 0x000C2370
		internal void ChangeSettingsProperty<T>(ref T childProperty, T newChild) where T : SettingsNode
		{
			if (childProperty != null)
			{
				childProperty.Parent = null;
			}
			if (newChild != null)
			{
				SettingsNode.ValidateChildForAssignment(newChild);
				newChild.Parent = this;
			}
			childProperty = newChild;
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x000C41BF File Offset: 0x000C23BF
		private void EnterEditScope()
		{
			this.editScopeLevel++;
			if (this.editScopeLevel == 1)
			{
				this.OnEnteredEditScope();
			}
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x000C41DE File Offset: 0x000C23DE
		private void ExitEditScope()
		{
			if (this.editScopeLevel == 1)
			{
				this.OnExitingEditScope();
			}
			this.editScopeLevel--;
			if (!this.IsInEditScope)
			{
				this.RaiseAccumulatedChanges();
			}
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x000C420B File Offset: 0x000C240B
		protected virtual void OnEnteredEditScope()
		{
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x000C420D File Offset: 0x000C240D
		protected virtual void OnExitingEditScope()
		{
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x000C420F File Offset: 0x000C240F
		private void RaiseAccumulatedChanges()
		{
			if (this.accumulatedChanges != null)
			{
				this.NotifySettingsChanged(this.accumulatedChanges);
				this.accumulatedChanges = null;
			}
		}

		// Token: 0x0400103F RID: 4159
		private bool isInitializing;

		// Token: 0x04001040 RID: 4160
		private int editScopeLevel;

		// Token: 0x04001041 RID: 4161
		private SettingsNode parent;

		// Token: 0x04001042 RID: 4162
		private SettingsChangedEventArgs accumulatedChanges;

		// Token: 0x04001043 RID: 4163
		private EventHandler<EventArgs> servicesChanged;

		// Token: 0x0200067C RID: 1660
		private sealed class EditScope : IDisposable
		{
			// Token: 0x06003C9E RID: 15518 RVA: 0x000C4234 File Offset: 0x000C2434
			public EditScope(SettingsNode settingsNode)
			{
				this.settingsNode = settingsNode;
				this.settingsNode.EnterEditScope();
			}

			// Token: 0x06003C9F RID: 15519 RVA: 0x000C424E File Offset: 0x000C244E
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06003CA0 RID: 15520 RVA: 0x000C4260 File Offset: 0x000C2460
			private void Dispose(bool disposing)
			{
				if (this.settingsNode == null)
				{
					throw new InvalidOperationException("Already disposed.");
				}
				if (!disposing)
				{
					return;
				}
				SettingsNode settingsNode = this.settingsNode;
				this.settingsNode = null;
				settingsNode.ExitEditScope();
			}

			// Token: 0x04001046 RID: 4166
			private SettingsNode settingsNode;
		}
	}
}
