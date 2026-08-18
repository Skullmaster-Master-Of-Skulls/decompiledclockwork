using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x02000662 RID: 1634
	public class ModelValidatorProviderCollection : Collection<ModelValidatorProvider>
	{
		// Token: 0x06005034 RID: 20532 RVA: 0x00115298 File Offset: 0x00113498
		public ModelValidatorProviderCollection()
		{
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x001152A0 File Offset: 0x001134A0
		public ModelValidatorProviderCollection(IList<ModelValidatorProvider> list) : base(list)
		{
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x001152A9 File Offset: 0x001134A9
		protected override void InsertItem(int index, ModelValidatorProvider item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x001152C1 File Offset: 0x001134C1
		protected override void SetItem(int index, ModelValidatorProvider item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x001152DC File Offset: 0x001134DC
		public IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ModelBindingExecutionContext context)
		{
			return this.SelectMany((ModelValidatorProvider provider) => provider.GetValidators(metadata, context));
		}
	}
}
