using System;
using System.Collections.Specialized;
using System.Reflection;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200036A RID: 874
	public class ModelDataSourceMethod
	{
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x0600285F RID: 10335 RVA: 0x00082A0C File Offset: 0x00080C0C
		// (set) Token: 0x06002860 RID: 10336 RVA: 0x00082A14 File Offset: 0x00080C14
		public object Instance { get; internal set; }

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x00082A1D File Offset: 0x00080C1D
		public OrderedDictionary Parameters
		{
			get
			{
				return this._parameters.Value;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06002862 RID: 10338 RVA: 0x00082A2A File Offset: 0x00080C2A
		// (set) Token: 0x06002863 RID: 10339 RVA: 0x00082A32 File Offset: 0x00080C32
		public MethodInfo MethodInfo { get; private set; }

		// Token: 0x06002864 RID: 10340 RVA: 0x00082A3B File Offset: 0x00080C3B
		public ModelDataSourceMethod(object instance, MethodInfo methodInfo)
		{
			if (methodInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			this.Instance = instance;
			this.MethodInfo = methodInfo;
		}

		// Token: 0x04001DF2 RID: 7666
		private Lazy<OrderedDictionary> _parameters = new Lazy<OrderedDictionary>();
	}
}
