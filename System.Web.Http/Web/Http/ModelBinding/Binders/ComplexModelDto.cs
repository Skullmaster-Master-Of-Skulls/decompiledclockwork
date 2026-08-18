using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Http.Metadata;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x0200013F RID: 319
	public class ComplexModelDto
	{
		// Token: 0x060007E5 RID: 2021 RVA: 0x0001A490 File Offset: 0x00018690
		public ComplexModelDto(ModelMetadata modelMetadata, IEnumerable<ModelMetadata> propertyMetadata)
		{
			if (modelMetadata == null)
			{
				throw Error.ArgumentNull("modelMetadata");
			}
			if (propertyMetadata == null)
			{
				throw Error.ArgumentNull("propertyMetadata");
			}
			this.ModelMetadata = modelMetadata;
			this.PropertyMetadata = new Collection<ModelMetadata>(propertyMetadata.ToList<ModelMetadata>());
			this.Results = new Dictionary<ModelMetadata, ComplexModelDtoResult>();
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0001A4E2 File Offset: 0x000186E2
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x0001A4EA File Offset: 0x000186EA
		public ModelMetadata ModelMetadata { get; private set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0001A4F3 File Offset: 0x000186F3
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0001A4FB File Offset: 0x000186FB
		public Collection<ModelMetadata> PropertyMetadata { get; private set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0001A504 File Offset: 0x00018704
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x0001A50C File Offset: 0x0001870C
		public IDictionary<ModelMetadata, ComplexModelDtoResult> Results { get; private set; }
	}
}
