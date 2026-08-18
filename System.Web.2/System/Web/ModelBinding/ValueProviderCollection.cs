using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x0200066E RID: 1646
	public class ValueProviderCollection : Collection<IValueProvider>, IValueProvider, IUnvalidatedValueProvider
	{
		// Token: 0x06005059 RID: 20569 RVA: 0x0011567E File Offset: 0x0011387E
		public ValueProviderCollection()
		{
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x00115686 File Offset: 0x00113886
		public ValueProviderCollection(IList<IValueProvider> list) : base(list)
		{
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x00115690 File Offset: 0x00113890
		public virtual bool ContainsPrefix(string prefix)
		{
			return this.Any((IValueProvider vp) => vp.ContainsPrefix(prefix));
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x001156BC File Offset: 0x001138BC
		public virtual ValueProviderResult GetValue(string key)
		{
			return this.GetValue(key, false);
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x001156C8 File Offset: 0x001138C8
		public virtual ValueProviderResult GetValue(string key, bool skipValidation)
		{
			return (from provider in this
			let result = ValueProviderCollection.GetValueFromProvider(provider, key, skipValidation)
			where result != null
			select result).FirstOrDefault<ValueProviderResult>();
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x00115748 File Offset: 0x00113948
		internal static ValueProviderResult GetValueFromProvider(IValueProvider provider, string key, bool skipValidation)
		{
			IUnvalidatedValueProvider unvalidatedValueProvider = provider as IUnvalidatedValueProvider;
			if (unvalidatedValueProvider == null)
			{
				return provider.GetValue(key);
			}
			return unvalidatedValueProvider.GetValue(key, skipValidation);
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x0011576F File Offset: 0x0011396F
		protected override void InsertItem(int index, IValueProvider item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x00115787 File Offset: 0x00113987
		protected override void SetItem(int index, IValueProvider item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}
	}
}
