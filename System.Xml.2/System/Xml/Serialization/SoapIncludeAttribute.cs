using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000175 RID: 373
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	public class SoapIncludeAttribute : Attribute
	{
		// Token: 0x060018BA RID: 6330 RVA: 0x0006CA81 File Offset: 0x0006AC81
		public SoapIncludeAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x0006CA90 File Offset: 0x0006AC90
		// (set) Token: 0x060018BC RID: 6332 RVA: 0x0006CA98 File Offset: 0x0006AC98
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x04000B50 RID: 2896
		private Type type;
	}
}
