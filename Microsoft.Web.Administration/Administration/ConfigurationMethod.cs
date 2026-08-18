using System;
using System.Diagnostics;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000036 RID: 54
	[DebuggerDisplay("Name={Name}")]
	public sealed class ConfigurationMethod
	{
		// Token: 0x060001CD RID: 461 RVA: 0x00006E8C File Offset: 0x00005E8C
		internal ConfigurationMethod(IAppHostMethod method)
		{
			this._method = method;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00006E9B File Offset: 0x00005E9B
		public string Name
		{
			get
			{
				return this._method.Name;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00006EA8 File Offset: 0x00005EA8
		public ConfigurationMethodSchema Schema
		{
			get
			{
				if (this._schema == null)
				{
					IAppHostMethodSchema schema = this._method.Schema;
					if (schema != null)
					{
						this._schema = new ConfigurationMethodSchema(schema);
					}
				}
				return this._schema;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006EE0 File Offset: 0x00005EE0
		public ConfigurationMethodInstance CreateInstance()
		{
			IAppHostMethodInstance appHostMethodInstance = this._method.CreateInstance();
			if (appHostMethodInstance == null)
			{
				return null;
			}
			return new ConfigurationMethodInstance(appHostMethodInstance);
		}

		// Token: 0x0400009B RID: 155
		private IAppHostMethod _method;

		// Token: 0x0400009C RID: 156
		private ConfigurationMethodSchema _schema;
	}
}
