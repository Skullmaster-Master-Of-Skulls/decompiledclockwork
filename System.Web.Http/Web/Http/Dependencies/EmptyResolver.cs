using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Http.Dependencies
{
	// Token: 0x0200011A RID: 282
	internal class EmptyResolver : IDependencyResolver, IDependencyScope, IDisposable
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x000169B2 File Offset: 0x00014BB2
		private EmptyResolver()
		{
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x000169BA File Offset: 0x00014BBA
		public static IDependencyResolver Instance
		{
			get
			{
				return EmptyResolver._instance;
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000169C1 File Offset: 0x00014BC1
		public IDependencyScope BeginScope()
		{
			return this;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000169C4 File Offset: 0x00014BC4
		public void Dispose()
		{
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000169C6 File Offset: 0x00014BC6
		public object GetService(Type serviceType)
		{
			return null;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000169C9 File Offset: 0x00014BC9
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return Enumerable.Empty<object>();
		}

		// Token: 0x040001E4 RID: 484
		private static readonly IDependencyResolver _instance = new EmptyResolver();
	}
}
