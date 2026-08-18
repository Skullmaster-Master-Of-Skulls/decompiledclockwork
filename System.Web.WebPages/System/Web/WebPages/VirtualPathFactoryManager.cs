using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.WebPages
{
	// Token: 0x02000094 RID: 148
	public class VirtualPathFactoryManager : IVirtualPathFactory
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x0000E894 File Offset: 0x0000CA94
		internal VirtualPathFactoryManager(IVirtualPathFactory defaultFactory)
		{
			this._virtualPathFactories.AddFirst(defaultFactory);
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
		internal static VirtualPathFactoryManager Instance
		{
			get
			{
				return VirtualPathFactoryManager._instance.Value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		internal static Func<string, bool> InstancePathExists
		{
			get
			{
				if (VirtualPathFactoryManager._instancePathExists == null)
				{
					VirtualPathFactoryManager._instancePathExists = new Func<string, bool>(VirtualPathFactoryManager.Instance.Exists);
				}
				return VirtualPathFactoryManager._instancePathExists;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000E8E3 File Offset: 0x0000CAE3
		internal IEnumerable<IVirtualPathFactory> RegisteredFactories
		{
			get
			{
				return this._virtualPathFactories;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000E8EB File Offset: 0x0000CAEB
		public static void RegisterVirtualPathFactory(IVirtualPathFactory virtualPathFactory)
		{
			VirtualPathFactoryManager.Instance.RegisterVirtualPathFactoryInternal(virtualPathFactory);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000E8F8 File Offset: 0x0000CAF8
		internal void RegisterVirtualPathFactoryInternal(IVirtualPathFactory virtualPathFactory)
		{
			this._virtualPathFactories.AddBefore(this._virtualPathFactories.Last, virtualPathFactory);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000E914 File Offset: 0x0000CB14
		public bool Exists(string virtualPath)
		{
			foreach (IVirtualPathFactory virtualPathFactory in this._virtualPathFactories)
			{
				if (virtualPathFactory.Exists(virtualPath))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000E970 File Offset: 0x0000CB70
		public object CreateInstance(string virtualPath)
		{
			return this.CreateInstanceOfType<object>(virtualPath);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000E990 File Offset: 0x0000CB90
		internal T CreateInstanceOfType<T>(string virtualPath) where T : class
		{
			IVirtualPathFactory virtualPathFactory = this._virtualPathFactories.FirstOrDefault((IVirtualPathFactory f) => f.Exists(virtualPath));
			if (virtualPathFactory != null)
			{
				return virtualPathFactory.CreateInstance(virtualPath);
			}
			return default(T);
		}

		// Token: 0x04000143 RID: 323
		private static readonly Lazy<VirtualPathFactoryManager> _instance = new Lazy<VirtualPathFactoryManager>(() => new VirtualPathFactoryManager(new BuildManagerWrapper()));

		// Token: 0x04000144 RID: 324
		private static Func<string, bool> _instancePathExists;

		// Token: 0x04000145 RID: 325
		private readonly LinkedList<IVirtualPathFactory> _virtualPathFactories = new LinkedList<IVirtualPathFactory>();
	}
}
