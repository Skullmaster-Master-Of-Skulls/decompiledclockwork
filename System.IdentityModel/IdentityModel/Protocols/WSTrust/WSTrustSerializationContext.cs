using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x02000213 RID: 531
	public class WSTrustSerializationContext
	{
		// Token: 0x0600118B RID: 4491 RVA: 0x00048A58 File Offset: 0x00046C58
		public WSTrustSerializationContext() : this(SecurityTokenHandlerCollectionManager.CreateDefaultSecurityTokenHandlerCollectionManager())
		{
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00048A65 File Offset: 0x00046C65
		public WSTrustSerializationContext(SecurityTokenHandlerCollectionManager securityTokenHandlerCollectionManager) : this(securityTokenHandlerCollectionManager, EmptySecurityTokenResolver.Instance, EmptySecurityTokenResolver.Instance)
		{
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00048A78 File Offset: 0x00046C78
		public WSTrustSerializationContext(SecurityTokenHandlerCollectionManager securityTokenHandlerCollectionManager, SecurityTokenResolver securityTokenResolver, SecurityTokenResolver useKeyTokenResolver)
		{
			if (securityTokenHandlerCollectionManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenHandlerCollectionManager");
			}
			if (securityTokenResolver == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenResolver");
			}
			if (useKeyTokenResolver == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("useKeyTokenResolver");
			}
			this.securityTokenHandlerCollectionManager = securityTokenHandlerCollectionManager;
			this.securityTokenResolver = securityTokenResolver;
			this.useKeyTokenResolver = useKeyTokenResolver;
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00048AD9 File Offset: 0x00046CD9
		// (set) Token: 0x0600118F RID: 4495 RVA: 0x00048AE1 File Offset: 0x00046CE1
		public SecurityTokenResolver TokenResolver
		{
			get
			{
				return this.securityTokenResolver;
			}
			set
			{
				this.securityTokenResolver = value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00048AEA File Offset: 0x00046CEA
		// (set) Token: 0x06001191 RID: 4497 RVA: 0x00048AF2 File Offset: 0x00046CF2
		public SecurityTokenResolver UseKeyTokenResolver
		{
			get
			{
				return this.useKeyTokenResolver;
			}
			set
			{
				this.useKeyTokenResolver = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x00048AFB File Offset: 0x00046CFB
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x00048B03 File Offset: 0x00046D03
		public SecurityTokenHandlerCollectionManager SecurityTokenHandlerCollectionManager
		{
			get
			{
				return this.securityTokenHandlerCollectionManager;
			}
			set
			{
				this.securityTokenHandlerCollectionManager = value;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00048B0C File Offset: 0x00046D0C
		public SecurityTokenHandlerCollection SecurityTokenHandlers
		{
			get
			{
				return this.securityTokenHandlerCollectionManager[""];
			}
		}

		// Token: 0x04000ED6 RID: 3798
		private SecurityTokenResolver securityTokenResolver;

		// Token: 0x04000ED7 RID: 3799
		private SecurityTokenResolver useKeyTokenResolver;

		// Token: 0x04000ED8 RID: 3800
		private SecurityTokenHandlerCollectionManager securityTokenHandlerCollectionManager;
	}
}
