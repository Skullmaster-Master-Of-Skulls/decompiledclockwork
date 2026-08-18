using System;

namespace System.Web
{
	// Token: 0x020000E7 RID: 231
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class PreApplicationStartMethodAttribute : Attribute
	{
		// Token: 0x06000E58 RID: 3672 RVA: 0x00028CBE File Offset: 0x00026EBE
		public PreApplicationStartMethodAttribute(Type type, string methodName)
		{
			this._type = type;
			this._methodName = methodName;
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x00028CD4 File Offset: 0x00026ED4
		public Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x00028CDC File Offset: 0x00026EDC
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x0400055E RID: 1374
		private readonly Type _type;

		// Token: 0x0400055F RID: 1375
		private readonly string _methodName;
	}
}
