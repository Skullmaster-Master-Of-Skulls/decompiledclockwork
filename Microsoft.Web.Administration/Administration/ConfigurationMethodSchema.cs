using System;
using System.Diagnostics;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000039 RID: 57
	[DebuggerDisplay("Name = {Name}")]
	public sealed class ConfigurationMethodSchema
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x000070E1 File Offset: 0x000060E1
		internal ConfigurationMethodSchema(IAppHostMethodSchema schema)
		{
			this._schema = schema;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000070F0 File Offset: 0x000060F0
		public ConfigurationElementSchema InputSchema
		{
			get
			{
				if (this._inputSchema == null)
				{
					IAppHostElementSchema inputSchema = this._schema.InputSchema;
					if (inputSchema != null)
					{
						this._inputSchema = new ConfigurationElementSchema(inputSchema);
					}
				}
				return this._inputSchema;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00007126 File Offset: 0x00006126
		public string Name
		{
			get
			{
				return this._schema.Name;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00007134 File Offset: 0x00006134
		public ConfigurationElementSchema OutputSchema
		{
			get
			{
				if (this._outputSchema == null)
				{
					IAppHostElementSchema outputSchema = this._schema.OutputSchema;
					if (outputSchema != null)
					{
						this._outputSchema = new ConfigurationElementSchema(outputSchema);
					}
				}
				return this._outputSchema;
			}
		}

		// Token: 0x040000A1 RID: 161
		private IAppHostMethodSchema _schema;

		// Token: 0x040000A2 RID: 162
		private ConfigurationElementSchema _inputSchema;

		// Token: 0x040000A3 RID: 163
		private ConfigurationElementSchema _outputSchema;
	}
}
