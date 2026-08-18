using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005FC RID: 1532
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class MenuCommand
	{
		// Token: 0x0600386C RID: 14444 RVA: 0x000F130A File Offset: 0x000EF50A
		public MenuCommand(EventHandler handler, CommandID command)
		{
			this.execHandler = handler;
			this.commandID = command;
			this.status = 3;
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x0600386D RID: 14445 RVA: 0x000F1327 File Offset: 0x000EF527
		// (set) Token: 0x0600386E RID: 14446 RVA: 0x000F1334 File Offset: 0x000EF534
		public virtual bool Checked
		{
			get
			{
				return (this.status & 4) != 0;
			}
			set
			{
				this.SetStatus(4, value);
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x0600386F RID: 14447 RVA: 0x000F133E File Offset: 0x000EF53E
		// (set) Token: 0x06003870 RID: 14448 RVA: 0x000F134B File Offset: 0x000EF54B
		public virtual bool Enabled
		{
			get
			{
				return (this.status & 2) != 0;
			}
			set
			{
				this.SetStatus(2, value);
			}
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x000F1358 File Offset: 0x000EF558
		private void SetStatus(int mask, bool value)
		{
			int num = this.status;
			if (value)
			{
				num |= mask;
			}
			else
			{
				num &= ~mask;
			}
			if (num != this.status)
			{
				this.status = num;
				this.OnCommandChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06003872 RID: 14450 RVA: 0x000F1395 File Offset: 0x000EF595
		public virtual IDictionary Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new HybridDictionary();
				}
				return this.properties;
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x000F13B0 File Offset: 0x000EF5B0
		// (set) Token: 0x06003874 RID: 14452 RVA: 0x000F13BD File Offset: 0x000EF5BD
		public virtual bool Supported
		{
			get
			{
				return (this.status & 1) != 0;
			}
			set
			{
				this.SetStatus(1, value);
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06003875 RID: 14453 RVA: 0x000F13C7 File Offset: 0x000EF5C7
		// (set) Token: 0x06003876 RID: 14454 RVA: 0x000F13D5 File Offset: 0x000EF5D5
		public virtual bool Visible
		{
			get
			{
				return (this.status & 16) == 0;
			}
			set
			{
				this.SetStatus(16, !value);
			}
		}

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06003877 RID: 14455 RVA: 0x000F13E3 File Offset: 0x000EF5E3
		// (remove) Token: 0x06003878 RID: 14456 RVA: 0x000F13FC File Offset: 0x000EF5FC
		public event EventHandler CommandChanged
		{
			add
			{
				this.statusHandler = (EventHandler)Delegate.Combine(this.statusHandler, value);
			}
			remove
			{
				this.statusHandler = (EventHandler)Delegate.Remove(this.statusHandler, value);
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06003879 RID: 14457 RVA: 0x000F1415 File Offset: 0x000EF615
		public virtual CommandID CommandID
		{
			get
			{
				return this.commandID;
			}
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000F1420 File Offset: 0x000EF620
		public virtual void Invoke()
		{
			if (this.execHandler != null)
			{
				try
				{
					this.execHandler(this, EventArgs.Empty);
				}
				catch (CheckoutException ex)
				{
					if (ex != CheckoutException.Canceled)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x000F1468 File Offset: 0x000EF668
		public virtual void Invoke(object arg)
		{
			this.Invoke();
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x0600387C RID: 14460 RVA: 0x000F1470 File Offset: 0x000EF670
		public virtual int OleStatus
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x000F1478 File Offset: 0x000EF678
		protected virtual void OnCommandChanged(EventArgs e)
		{
			if (this.statusHandler != null)
			{
				this.statusHandler(this, e);
			}
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x000F1490 File Offset: 0x000EF690
		public override string ToString()
		{
			string text = this.CommandID.ToString() + " : ";
			if ((this.status & 1) != 0)
			{
				text += "Supported";
			}
			if ((this.status & 2) != 0)
			{
				text += "|Enabled";
			}
			if ((this.status & 16) == 0)
			{
				text += "|Visible";
			}
			if ((this.status & 4) != 0)
			{
				text += "|Checked";
			}
			return text;
		}

		// Token: 0x04002B14 RID: 11028
		private EventHandler execHandler;

		// Token: 0x04002B15 RID: 11029
		private EventHandler statusHandler;

		// Token: 0x04002B16 RID: 11030
		private CommandID commandID;

		// Token: 0x04002B17 RID: 11031
		private int status;

		// Token: 0x04002B18 RID: 11032
		private IDictionary properties;

		// Token: 0x04002B19 RID: 11033
		private const int ENABLED = 2;

		// Token: 0x04002B1A RID: 11034
		private const int INVISIBLE = 16;

		// Token: 0x04002B1B RID: 11035
		private const int CHECKED = 4;

		// Token: 0x04002B1C RID: 11036
		private const int SUPPORTED = 1;
	}
}
