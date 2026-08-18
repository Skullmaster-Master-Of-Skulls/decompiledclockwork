using System;
using System.Collections.Generic;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicFieldConversion;

namespace TechnoPro.Common.Core.DynamicForms
{
	// Token: 0x020000FC RID: 252
	public class DynamicFieldConversionManager : IDynamicFieldConversionManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000A19 RID: 2585 RVA: 0x00041236 File Offset: 0x0003F436
		public DynamicFieldConversionManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00041248 File Offset: 0x0003F448
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x00041250 File Offset: 0x0003F450
		public OperationContext OpContext { get; set; }

		// Token: 0x06000A1C RID: 2588 RVA: 0x0004125C File Offset: 0x0003F45C
		public DynamicDataConversionSet ConvertDynamicField(eDynamicFieldAvailableConversion Conversion, int ControlId, bool PreviewMode)
		{
			DynamicDataConversionSet dynamicDataConversionSet = new DynamicDataConversionSet();
			dynamicDataConversionSet.ControlId = ControlId;
			dynamicDataConversionSet.Conversion = Conversion;
			dynamicDataConversionSet.ConversionItems = this.GetConversionDataItems(ControlId);
			throw new NotImplementedException();
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00041294 File Offset: 0x0003F494
		private IList<DynamicDataConversionItem> GetConversionDataItems(int ControlId)
		{
			IList<string> list = this.FindAllDataTableSuffixesWhereControlIdHasData(ControlId);
			List<DynamicDataConversionItem> result = new List<DynamicDataConversionItem>();
			foreach (string text in list)
			{
			}
			return result;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x000412F0 File Offset: 0x0003F4F0
		public IList<string> FindAllDataTableSuffixesWhereControlIdHasData(int ControlId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000072EA File Offset: 0x000054EA
		private IList<string> FindAllDataTableSuffixesWhereControlIdHasData(int ControlId, string DataTablePrefix)
		{
			throw new NotImplementedException();
		}
	}
}
