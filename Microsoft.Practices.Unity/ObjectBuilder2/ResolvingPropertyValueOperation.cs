using System;
using Microsoft.Practices.Unity.Properties;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200005F RID: 95
	public class ResolvingPropertyValueOperation : PropertyOperation
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x0000604D File Offset: 0x0000424D
		public ResolvingPropertyValueOperation(Type typeBeingConstructed, string propertyName) : base(typeBeingConstructed, propertyName)
		{
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006057 File Offset: 0x00004257
		protected override string GetDescriptionFormat()
		{
			return Resources.ResolvingPropertyValueOperation;
		}
	}
}
