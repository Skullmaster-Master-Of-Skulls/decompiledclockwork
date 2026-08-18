using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x0200058D RID: 1421
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.ComponentDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[DesignerCategory("Component")]
	[TypeConverter(typeof(ComponentConverter))]
	public class MarshalByValueComponent : IComponent, IDisposable, IServiceProvider
	{
		// Token: 0x06003469 RID: 13417 RVA: 0x000E5030 File Offset: 0x000E3230
		~MarshalByValueComponent()
		{
			this.Dispose(false);
		}

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x0600346A RID: 13418 RVA: 0x000E5060 File Offset: 0x000E3260
		// (remove) Token: 0x0600346B RID: 13419 RVA: 0x000E5073 File Offset: 0x000E3273
		public event EventHandler Disposed
		{
			add
			{
				this.Events.AddHandler(MarshalByValueComponent.EventDisposed, value);
			}
			remove
			{
				this.Events.RemoveHandler(MarshalByValueComponent.EventDisposed, value);
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x000E5086 File Offset: 0x000E3286
		protected EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000E50A1 File Offset: 0x000E32A1
		// (set) Token: 0x0600346E RID: 13422 RVA: 0x000E50A9 File Offset: 0x000E32A9
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

		// Token: 0x0600346F RID: 13423 RVA: 0x000E50B2 File Offset: 0x000E32B2
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x000E50C4 File Offset: 0x000E32C4
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
						EventHandler eventHandler = (EventHandler)this.events[MarshalByValueComponent.EventDisposed];
						if (eventHandler != null)
						{
							eventHandler(this, EventArgs.Empty);
						}
					}
				}
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000E5150 File Offset: 0x000E3350
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IContainer Container
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

		// Token: 0x06003472 RID: 13426 RVA: 0x000E516F File Offset: 0x000E336F
		public virtual object GetService(Type service)
		{
			if (this.site != null)
			{
				return this.site.GetService(service);
			}
			return null;
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06003473 RID: 13427 RVA: 0x000E5188 File Offset: 0x000E3388
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool DesignMode
		{
			get
			{
				ISite site = this.site;
				return site != null && site.DesignMode;
			}
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000E51A8 File Offset: 0x000E33A8
		public override string ToString()
		{
			ISite site = this.site;
			if (site != null)
			{
				return site.Name + " [" + base.GetType().FullName + "]";
			}
			return base.GetType().FullName;
		}

		// Token: 0x040029F8 RID: 10744
		private static readonly object EventDisposed = new object();

		// Token: 0x040029F9 RID: 10745
		private ISite site;

		// Token: 0x040029FA RID: 10746
		private EventHandlerList events;
	}
}
