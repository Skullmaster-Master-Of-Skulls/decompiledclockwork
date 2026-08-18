using System;
using System.Globalization;
using System.Reflection;
using Microsoft.Practices.Unity.Properties;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200005B RID: 91
	public class InvokingMethodOperation : BuildOperation
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00005920 File Offset: 0x00003B20
		public InvokingMethodOperation(Type typeBeingConstructed, string methodSignature) : base(typeBeingConstructed)
		{
			this.methodSignature = methodSignature;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00005930 File Offset: 0x00003B30
		public string MethodSignature
		{
			get
			{
				return this.methodSignature;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005938 File Offset: 0x00003B38
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.InvokingMethodOperation, new object[]
			{
				base.TypeBeingConstructed.GetTypeInfo().Name,
				this.methodSignature
			});
		}

		// Token: 0x0400005A RID: 90
		private readonly string methodSignature;
	}
}
