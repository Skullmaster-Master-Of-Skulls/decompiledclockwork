using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Web.ApplicationServices
{
	// Token: 0x02000124 RID: 292
	public class ValidatingPropertiesEventArgs : EventArgs
	{
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00036ADD File Offset: 0x00034CDD
		public IDictionary<string, object> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00036AE5 File Offset: 0x00034CE5
		public Collection<string> FailedProperties
		{
			get
			{
				return this._failedProperties;
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00035E5A File Offset: 0x0003405A
		internal ValidatingPropertiesEventArgs()
		{
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00036AED File Offset: 0x00034CED
		internal ValidatingPropertiesEventArgs(IDictionary<string, object> properties)
		{
			this._properties = properties;
			this._failedProperties = new Collection<string>();
		}

		// Token: 0x0400044B RID: 1099
		private IDictionary<string, object> _properties;

		// Token: 0x0400044C RID: 1100
		private Collection<string> _failedProperties;
	}
}
