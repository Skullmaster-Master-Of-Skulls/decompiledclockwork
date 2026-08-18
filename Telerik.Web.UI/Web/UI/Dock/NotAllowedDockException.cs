using System;

namespace Telerik.Web.UI.Dock
{
	// Token: 0x02000272 RID: 626
	public class NotAllowedDockException : ApplicationException
	{
		// Token: 0x060016C0 RID: 5824 RVA: 0x0004D0D4 File Offset: 0x0004B2D4
		public NotAllowedDockException()
		{
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0004D0DC File Offset: 0x0004B2DC
		public NotAllowedDockException(string message) : base(message)
		{
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0004D0E5 File Offset: 0x0004B2E5
		public NotAllowedDockException(string message, Exception ex) : base(message, ex)
		{
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0004D0EF File Offset: 0x0004B2EF
		public NotAllowedDockException(RadDockZone zone, RadDock dock) : this(string.Format("Dock '{0}' is not allowed in zone '{1}'. Please, make sure setup of docks is valid.", dock.ID, zone.ID))
		{
		}
	}
}
