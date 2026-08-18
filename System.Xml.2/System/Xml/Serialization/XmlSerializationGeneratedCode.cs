using System;
using System.Reflection;
using System.Threading;

namespace System.Xml.Serialization
{
	// Token: 0x020001A7 RID: 423
	public abstract class XmlSerializationGeneratedCode
	{
		// Token: 0x06001C25 RID: 7205 RVA: 0x00083BA0 File Offset: 0x00081DA0
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

		// Token: 0x06001C26 RID: 7206 RVA: 0x00083BF1 File Offset: 0x00081DF1
		internal void Dispose()
		{
			if (this.assemblyResolver != null)
			{
				AppDomain.CurrentDomain.AssemblyResolve -= this.assemblyResolver;
			}
			this.assemblyResolver = null;
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x00083C12 File Offset: 0x00081E12
		internal Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (this.tempAssembly != null && Thread.CurrentThread.GetHashCode() == this.threadCode)
			{
				return this.tempAssembly.GetReferencedAssembly(args.Name);
			}
			return null;
		}

		// Token: 0x04000C3D RID: 3133
		private TempAssembly tempAssembly;

		// Token: 0x04000C3E RID: 3134
		private int threadCode;

		// Token: 0x04000C3F RID: 3135
		private ResolveEventHandler assemblyResolver;
	}
}
