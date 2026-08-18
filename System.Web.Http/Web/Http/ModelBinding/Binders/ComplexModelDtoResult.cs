using System;
using System.Web.Http.Validation;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000142 RID: 322
	public sealed class ComplexModelDtoResult
	{
		// Token: 0x060007F2 RID: 2034 RVA: 0x0001A626 File Offset: 0x00018826
		public ComplexModelDtoResult(object model, ModelValidationNode validationNode)
		{
			if (validationNode == null)
			{
				throw Error.ArgumentNull("validationNode");
			}
			this.Model = model;
			this.ValidationNode = validationNode;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0001A64A File Offset: 0x0001884A
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x0001A652 File Offset: 0x00018852
		public object Model { get; private set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0001A65B File Offset: 0x0001885B
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0001A663 File Offset: 0x00018863
		public ModelValidationNode ValidationNode { get; private set; }
	}
}
