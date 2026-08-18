using System;
using System.Reflection;
using System.Threading;

namespace System.Xml.Serialization
{
	// Token: 0x02000323 RID: 803
	public abstract class XmlSerializationGeneratedCode
	{
		// Token: 0x06002659 RID: 9817 RVA: 0x000BAEF0 File Offset: 0x000B9EF0
		internal void Init(TempAssembly tempAssembly)
		{
			this.tempAssembly = tempAssembly;
			if (tempAssembly != null && tempAssembly.NeedAssembyResolve)
			{
				this.threadCode = Thread.CurrentThread.GetHashCode();
				this.assemblyResolver = new ResolveEventHandler(this.OnAssemblyResolve);
				AppDomain.CurrentDomain.AssemblyResolve += this.assemblyResolver;
			}
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000BAF41 File Offset: 0x000B9F41
		internal void Dispose()
		{
			if (this.assemblyResolver != null)
			{
				AppDomain.CurrentDomain.AssemblyResolve -= this.assemblyResolver;
			}
			this.assemblyResolver = null;
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000BAF62 File Offset: 0x000B9F62
		internal Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (this.tempAssembly != null && Thread.CurrentThread.GetHashCode() == this.threadCode)
			{
				return this.tempAssembly.GetReferencedAssembly(args.Name);
			}
			return null;
		}

		// Token: 0x040015D1 RID: 5585
		private TempAssembly tempAssembly;

		// Token: 0x040015D2 RID: 5586
		private int threadCode;

		// Token: 0x040015D3 RID: 5587
		private ResolveEventHandler assemblyResolver;
	}
}
