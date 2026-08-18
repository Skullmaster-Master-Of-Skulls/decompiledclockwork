using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000640 RID: 1600
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
	public sealed class ExtensibleModelBinderAttribute : Attribute
	{
		// Token: 0x06004F33 RID: 20275 RVA: 0x00113310 File Offset: 0x00111510
		public ExtensibleModelBinderAttribute(Type binderType)
		{
			this.BinderType = binderType;
		}

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x06004F34 RID: 20276 RVA: 0x0011331F File Offset: 0x0011151F
		// (set) Token: 0x06004F35 RID: 20277 RVA: 0x00113327 File Offset: 0x00111527
		public Type BinderType { get; private set; }

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x06004F36 RID: 20278 RVA: 0x00113330 File Offset: 0x00111530
		// (set) Token: 0x06004F37 RID: 20279 RVA: 0x00113338 File Offset: 0x00111538
		public bool SuppressPrefixCheck { get; set; }
	}
}
