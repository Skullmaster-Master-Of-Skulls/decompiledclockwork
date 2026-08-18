using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000077 RID: 119
	public abstract class UnityContainerExtension : IUnityContainerExtensionConfigurator
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x000073AC File Offset: 0x000055AC
		[SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "context", Justification = "Names are the same deliberately, as the property gets set from the parameter")]
		public void InitializeExtension(ExtensionContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.container = context.Container;
			this.context = context;
			this.Initialize();
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000073D5 File Offset: 0x000055D5
		public IUnityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000073DD File Offset: 0x000055DD
		protected ExtensionContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x060001FB RID: 507
		protected abstract void Initialize();

		// Token: 0x060001FC RID: 508 RVA: 0x000073E5 File Offset: 0x000055E5
		public virtual void Remove()
		{
		}

		// Token: 0x04000073 RID: 115
		private IUnityContainer container;

		// Token: 0x04000074 RID: 116
		private ExtensionContext context;
	}
}
