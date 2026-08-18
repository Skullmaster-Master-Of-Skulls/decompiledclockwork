using System;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000030 RID: 48
	public sealed class HttpControllerSettings
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00006CFA File Offset: 0x00004EFA
		public HttpControllerSettings(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			this._configuration = configuration;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00006D17 File Offset: 0x00004F17
		public MediaTypeFormatterCollection Formatters
		{
			get
			{
				if (this._formatters == null)
				{
					this._formatters = new MediaTypeFormatterCollection(this._configuration.Formatters);
				}
				return this._formatters;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006D40 File Offset: 0x00004F40
		public ParameterBindingRulesCollection ParameterBindingRules
		{
			get
			{
				if (this._parameterBindingRules == null)
				{
					this._parameterBindingRules = new ParameterBindingRulesCollection();
					foreach (Func<HttpParameterDescriptor, HttpParameterBinding> item in this._configuration.ParameterBindingRules)
					{
						this._parameterBindingRules.Add(item);
					}
				}
				return this._parameterBindingRules;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00006DB0 File Offset: 0x00004FB0
		public ServicesContainer Services
		{
			get
			{
				if (this._services == null)
				{
					this._services = new ControllerServices(this._configuration.Services);
				}
				return this._services;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00006DD6 File Offset: 0x00004FD6
		internal bool IsFormatterCollectionInitialized
		{
			get
			{
				return this._formatters != null;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006DE4 File Offset: 0x00004FE4
		internal bool IsParameterBindingRuleCollectionInitialized
		{
			get
			{
				return this._parameterBindingRules != null;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00006DF2 File Offset: 0x00004FF2
		internal bool IsServiceCollectionInitialized
		{
			get
			{
				return this._services != null;
			}
		}

		// Token: 0x04000072 RID: 114
		private MediaTypeFormatterCollection _formatters;

		// Token: 0x04000073 RID: 115
		private ParameterBindingRulesCollection _parameterBindingRules;

		// Token: 0x04000074 RID: 116
		private ServicesContainer _services;

		// Token: 0x04000075 RID: 117
		private HttpConfiguration _configuration;
	}
}
