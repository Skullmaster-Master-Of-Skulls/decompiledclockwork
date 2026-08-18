using System;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;

namespace System.Web.Http.Validation
{
	// Token: 0x0200017A RID: 378
	public class ModelStateFormatterLogger : IFormatterLogger
	{
		// Token: 0x060009C8 RID: 2504 RVA: 0x0002026C File Offset: 0x0001E46C
		public ModelStateFormatterLogger(ModelStateDictionary modelState, string prefix)
		{
			if (modelState == null)
			{
				throw Error.ArgumentNull("modelState");
			}
			this._modelState = modelState;
			this._prefix = prefix;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00020290 File Offset: 0x0001E490
		public void LogError(string errorPath, string errorMessage)
		{
			if (errorPath == null)
			{
				throw Error.ArgumentNull("errorPath");
			}
			if (errorMessage == null)
			{
				throw Error.ArgumentNull("errorMessage");
			}
			string key = ModelBindingHelper.ConcatenateKeys(this._prefix, errorPath);
			this._modelState.AddModelError(key, errorMessage);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x000202D4 File Offset: 0x0001E4D4
		public void LogError(string errorPath, Exception exception)
		{
			if (errorPath == null)
			{
				throw Error.ArgumentNull("errorPath");
			}
			if (exception == null)
			{
				throw Error.ArgumentNull("exception");
			}
			string key = ModelBindingHelper.ConcatenateKeys(this._prefix, errorPath);
			this._modelState.AddModelError(key, exception);
		}

		// Token: 0x040002E6 RID: 742
		private readonly ModelStateDictionary _modelState;

		// Token: 0x040002E7 RID: 743
		private readonly string _prefix;
	}
}
