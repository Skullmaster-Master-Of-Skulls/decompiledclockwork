using System;
using System.Configuration;

namespace System.Web.ModelBinding
{
	// Token: 0x02000683 RID: 1667
	public sealed class ProfileValueProvider : SimpleValueProvider
	{
		// Token: 0x060050EC RID: 20716 RVA: 0x0011712A File Offset: 0x0011532A
		public ProfileValueProvider(ModelBindingExecutionContext modelBindingExecutionContext) : base(modelBindingExecutionContext)
		{
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x00117134 File Offset: 0x00115334
		protected override object FetchValue(string key)
		{
			object result = null;
			try
			{
				result = base.ModelBindingExecutionContext.HttpContext.Profile[key];
			}
			catch (SettingsPropertyNotFoundException)
			{
			}
			return result;
		}
	}
}
