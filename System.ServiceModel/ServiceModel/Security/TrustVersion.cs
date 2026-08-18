using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000299 RID: 665
	[__DynamicallyInvokable]
	public abstract class TrustVersion
	{
		// Token: 0x0600142C RID: 5164 RVA: 0x0004C194 File Offset: 0x0004A394
		internal TrustVersion(XmlDictionaryString ns, XmlDictionaryString prefix)
		{
			this.trustNamespace = ns;
			this.prefix = prefix;
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0004C1AA File Offset: 0x0004A3AA
		[__DynamicallyInvokable]
		public XmlDictionaryString Namespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.trustNamespace;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0004C1B2 File Offset: 0x0004A3B2
		[__DynamicallyInvokable]
		public XmlDictionaryString Prefix
		{
			[__DynamicallyInvokable]
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0004C1BA File Offset: 0x0004A3BA
		[__DynamicallyInvokable]
		public static TrustVersion Default
		{
			[__DynamicallyInvokable]
			get
			{
				return TrustVersion.WSTrustFeb2005;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0004C1C1 File Offset: 0x0004A3C1
		[__DynamicallyInvokable]
		public static TrustVersion WSTrustFeb2005
		{
			[__DynamicallyInvokable]
			get
			{
				return TrustVersion.WSTrustVersionFeb2005.Instance;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0004C1C8 File Offset: 0x0004A3C8
		public static TrustVersion WSTrust13
		{
			get
			{
				return TrustVersion.WSTrustVersion13.Instance;
			}
		}

		// Token: 0x04001AA1 RID: 6817
		private readonly XmlDictionaryString trustNamespace;

		// Token: 0x04001AA2 RID: 6818
		private readonly XmlDictionaryString prefix;

		// Token: 0x02000B34 RID: 2868
		private class WSTrustVersionFeb2005 : TrustVersion
		{
			// Token: 0x06007089 RID: 28809 RVA: 0x001A3310 File Offset: 0x001A1510
			protected WSTrustVersionFeb2005() : base(XD.TrustFeb2005Dictionary.Namespace, XD.TrustFeb2005Dictionary.Prefix)
			{
			}

			// Token: 0x17001A40 RID: 6720
			// (get) Token: 0x0600708A RID: 28810 RVA: 0x001A332C File Offset: 0x001A152C
			public static TrustVersion Instance
			{
				get
				{
					return TrustVersion.WSTrustVersionFeb2005.instance;
				}
			}

			// Token: 0x04004004 RID: 16388
			private static readonly TrustVersion.WSTrustVersionFeb2005 instance = new TrustVersion.WSTrustVersionFeb2005();
		}

		// Token: 0x02000B35 RID: 2869
		private class WSTrustVersion13 : TrustVersion
		{
			// Token: 0x0600708C RID: 28812 RVA: 0x001A333F File Offset: 0x001A153F
			protected WSTrustVersion13() : base(DXD.TrustDec2005Dictionary.Namespace, DXD.TrustDec2005Dictionary.Prefix)
			{
			}

			// Token: 0x17001A41 RID: 6721
			// (get) Token: 0x0600708D RID: 28813 RVA: 0x001A335B File Offset: 0x001A155B
			public static TrustVersion Instance
			{
				get
				{
					return TrustVersion.WSTrustVersion13.instance;
				}
			}

			// Token: 0x04004005 RID: 16389
			private static readonly TrustVersion.WSTrustVersion13 instance = new TrustVersion.WSTrustVersion13();
		}
	}
}
