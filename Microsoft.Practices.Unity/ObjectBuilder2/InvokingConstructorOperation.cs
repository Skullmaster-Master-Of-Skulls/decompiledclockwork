using System;
using System.Globalization;
using Microsoft.Practices.Unity.Properties;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000056 RID: 86
	public class InvokingConstructorOperation : BuildOperation
	{
		// Token: 0x0600016F RID: 367 RVA: 0x0000546C File Offset: 0x0000366C
		public InvokingConstructorOperation(Type typeBeingConstructed, string constructorSignature) : base(typeBeingConstructed)
		{
			this.constructorSignature = constructorSignature;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000547C File Offset: 0x0000367C
		public string ConstructorSignature
		{
			get
			{
				return this.constructorSignature;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005484 File Offset: 0x00003684
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.InvokingConstructorOperation, new object[]
			{
				this.constructorSignature
			});
		}

		// Token: 0x04000051 RID: 81
		private readonly string constructorSignature;
	}
}
