using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x02000635 RID: 1589
	public class ComplexModel
	{
		// Token: 0x06004EF5 RID: 20213 RVA: 0x00112C7C File Offset: 0x00110E7C
		public ComplexModel(ModelMetadata modelMetadata, IEnumerable<ModelMetadata> propertyMetadata)
		{
			if (modelMetadata == null)
			{
				throw new ArgumentNullException("modelMetadata");
			}
			if (propertyMetadata == null)
			{
				throw new ArgumentNullException("propertyMetadata");
			}
			this.ModelMetadata = modelMetadata;
			this.PropertyMetadata = new ReadOnlyCollection<ModelMetadata>(propertyMetadata.ToList<ModelMetadata>());
			this.Results = new Dictionary<ModelMetadata, ComplexModelResult>();
		}

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x06004EF6 RID: 20214 RVA: 0x00112CCE File Offset: 0x00110ECE
		// (set) Token: 0x06004EF7 RID: 20215 RVA: 0x00112CD6 File Offset: 0x00110ED6
		public ModelMetadata ModelMetadata { get; private set; }

		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x00112CDF File Offset: 0x00110EDF
		// (set) Token: 0x06004EF9 RID: 20217 RVA: 0x00112CE7 File Offset: 0x00110EE7
		public ReadOnlyCollection<ModelMetadata> PropertyMetadata { get; private set; }

		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x06004EFA RID: 20218 RVA: 0x00112CF0 File Offset: 0x00110EF0
		// (set) Token: 0x06004EFB RID: 20219 RVA: 0x00112CF8 File Offset: 0x00110EF8
		public IDictionary<ModelMetadata, ComplexModelResult> Results { get; private set; }
	}
}
