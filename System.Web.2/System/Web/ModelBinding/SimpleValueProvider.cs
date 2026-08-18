using System;
using System.Globalization;

namespace System.Web.ModelBinding
{
	// Token: 0x0200068A RID: 1674
	public abstract class SimpleValueProvider : IValueProvider
	{
		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x0600510C RID: 20748 RVA: 0x00117534 File Offset: 0x00115734
		// (set) Token: 0x0600510D RID: 20749 RVA: 0x0011753C File Offset: 0x0011573C
		private protected ModelBindingExecutionContext ModelBindingExecutionContext { protected get; private set; }

		// Token: 0x0600510E RID: 20750 RVA: 0x00117545 File Offset: 0x00115745
		protected SimpleValueProvider(ModelBindingExecutionContext modelBindingExecutionContext) : this(modelBindingExecutionContext, CultureInfo.CurrentCulture)
		{
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x00117553 File Offset: 0x00115753
		protected SimpleValueProvider(ModelBindingExecutionContext modelBindingExecutionContext, CultureInfo cultureInfo)
		{
			this.ModelBindingExecutionContext = modelBindingExecutionContext;
			this._cultureInfo = cultureInfo;
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x00117569 File Offset: 0x00115769
		public virtual bool ContainsPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			return this.FetchValue(prefix) != null;
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x00117584 File Offset: 0x00115784
		public virtual ValueProviderResult GetValue(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			object obj = this.FetchValue(key);
			if (obj == null)
			{
				return null;
			}
			string attemptedValue = Convert.ToString(obj, this._cultureInfo);
			return new ValueProviderResult(obj, attemptedValue, this._cultureInfo);
		}

		// Token: 0x06005112 RID: 20754
		protected abstract object FetchValue(string key);

		// Token: 0x04002AE2 RID: 10978
		private CultureInfo _cultureInfo;
	}
}
