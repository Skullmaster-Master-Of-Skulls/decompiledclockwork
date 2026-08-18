using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x0200052B RID: 1323
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DesignerCategory("Component")]
	public class Component : MarshalByRefObject, IComponent, IDisposable
	{
		// Token: 0x0600320F RID: 12815 RVA: 0x000E0A64 File Offset: 0x000DEC64
		~Component()
		{
			this.Dispose(false);
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06003210 RID: 12816 RVA: 0x000E0A94 File Offset: 0x000DEC94
		protected virtual bool CanRaiseEvents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x000E0A97 File Offset: 0x000DEC97
		internal bool CanRaiseEventsInternal
		{
			get
			{
				return this.CanRaiseEvents;
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06003212 RID: 12818 RVA: 0x000E0A9F File Offset: 0x000DEC9F
		// (remove) Token: 0x06003213 RID: 12819 RVA: 0x000E0AB2 File Offset: 0x000DECB2
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler Disposed
		{
			add
			{
				this.Events.AddHandler(Component.EventDisposed, value);
			}
			remove
			{
				this.Events.RemoveHandler(Component.EventDisposed, value);
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06003214 RID: 12820 RVA: 0x000E0AC5 File Offset: 0x000DECC5
		protected EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList(this);
				}
				return this.events;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x000E0AE1 File Offset: 0x000DECE1
		// (set) Token: 0x06003216 RID: 12822 RVA: 0x000E0AE9 File Offset: 0x000DECE9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual ISite Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x000E0AF2 File Offset: 0x000DECF2
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x000E0B04 File Offset: 0x000DED04
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					if (this.site != null && this.site.Container != null)
					{
						this.site.Container.Remove(this);
					}
					if (this.events != null)
					{
						EventHandler eventHandler = (EventHandler)this.events[Component.EventDisposed];
						if (eventHandler != null)
						{
							eventHandler(this, EventArgs.Empty);
						}
					}
				}
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000E0B90 File Offset: 0x000DED90
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IContainer Container
		{
			get
			{
				ISite site = this.site;
				if (site != null)
				{
					return site.Container;
				}
				return null;
			}
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x000E0BB0 File Offset: 0x000DEDB0
		protected virtual object GetService(Type service)
		{
			ISite site = this.site;
			if (site != null)
			{
				return site.GetService(service);
			}
			return null;
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x000E0BD0 File Offset: 0x000DEDD0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected bool DesignMode
		{
			get
			{
				ISite site = this.site;
				return site != null && site.DesignMode;
			}
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000E0BF0 File Offset: 0x000DEDF0
		public override string ToString()
		{
			ISite site = this.site;
			if (site != null)
			{
				return site.Name + " [" + base.GetType().FullName + "]";
			}
			return base.GetType().FullName;
		}

		// Token: 0x04002961 RID: 10593
		private static readonly object EventDisposed = new object();

		// Token: 0x04002962 RID: 10594
		private ISite site;

		// Token: 0x04002963 RID: 10595
		private EventHandlerList events;
	}
}
