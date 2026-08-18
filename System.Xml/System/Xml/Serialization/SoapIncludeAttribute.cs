using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002EF RID: 751
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	public class SoapIncludeAttribute : Attribute
	{
		// Token: 0x06002303 RID: 8963 RVA: 0x000A461D File Offset: 0x000A361D
		public SoapIncludeAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x000A462C File Offset: 0x000A362C
		// (set) Token: 0x06002305 RID: 8965 RVA: 0x000A4634 File Offset: 0x000A3634
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

		// Token: 0x040014E4 RID: 5348
		private Type type;
	}
}
