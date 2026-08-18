using System;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000369 RID: 873
	public class ModelDataMethodResult
	{
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x000829C7 File Offset: 0x00080BC7
		// (set) Token: 0x0600285C RID: 10332 RVA: 0x000829CF File Offset: 0x00080BCF
		public object ReturnValue { get; private set; }

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x000829D8 File Offset: 0x00080BD8
		public OrderedDictionary OutputParameters
		{
			get
			{
				return this._outputParameters;
			}
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000829E0 File Offset: 0x00080BE0
		public ModelDataMethodResult(object returnValue, OrderedDictionary outputParameters)
		{
			this.ReturnValue = returnValue;
			outputParameters = (outputParameters ?? new OrderedDictionary(StringComparer.OrdinalIgnoreCase));
			this._outputParameters = outputParameters.AsReadOnly();
		}

		// Token: 0x04001DF0 RID: 7664
		private OrderedDictionary _outputParameters;
	}
}
