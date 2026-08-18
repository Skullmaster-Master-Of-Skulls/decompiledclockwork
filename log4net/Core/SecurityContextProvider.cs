using System;
using log4net.Util;

namespace log4net.Core
{
	// Token: 0x02000075 RID: 117
	public class SecurityContextProvider
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000DF21 File Offset: 0x0000C121
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000DF28 File Offset: 0x0000C128
		public static SecurityContextProvider DefaultProvider
		{
			get
			{
				return SecurityContextProvider.s_defaultProvider;
			}
			set
			{
				SecurityContextProvider.s_defaultProvider = value;
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000DF30 File Offset: 0x0000C130
		protected SecurityContextProvider()
		{
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000DF38 File Offset: 0x0000C138
		public virtual SecurityContext CreateSecurityContext(object consumer)
		{
			return NullSecurityContext.Instance;
		}

		// Token: 0x040001CC RID: 460
		private static SecurityContextProvider s_defaultProvider = new SecurityContextProvider();
	}
}
