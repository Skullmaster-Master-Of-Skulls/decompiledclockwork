using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200069C RID: 1692
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class ContextAttribute : Attribute, IContextAttribute, IContextProperty
	{
		// Token: 0x06003D3F RID: 15679 RVA: 0x000D1C69 File Offset: 0x000D0C69
		public ContextAttribute(string name)
		{
			this.AttributeName = name;
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06003D40 RID: 15680 RVA: 0x000D1C78 File Offset: 0x000D0C78
		public virtual string Name
		{
			get
			{
				return this.AttributeName;
			}
		}

		// Token: 0x06003D41 RID: 15681 RVA: 0x000D1C80 File Offset: 0x000D0C80
		public virtual bool IsNewContextOK(Context newCtx)
		{
			return true;
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x000D1C83 File Offset: 0x000D0C83
		public virtual void Freeze(Context newContext)
		{
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x000D1C88 File Offset: 0x000D0C88
		public override bool Equals(object o)
		{
			IContextProperty contextProperty = o as IContextProperty;
			return contextProperty != null && this.AttributeName.Equals(contextProperty.Name);
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x000D1CB2 File Offset: 0x000D0CB2
		public override int GetHashCode()
		{
			return this.AttributeName.GetHashCode();
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x000D1CC0 File Offset: 0x000D0CC0
		public virtual bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
		{
			if (ctx == null)
			{
				throw new ArgumentNullException("ctx");
			}
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			if (!ctorMsg.ActivationType.IsContextful)
			{
				return true;
			}
			object property = ctx.GetProperty(this.AttributeName);
			return property != null && this.Equals(property);
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x000D1D14 File Offset: 0x000D0D14
		public virtual void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			ctorMsg.ContextProperties.Add(this);
		}

		// Token: 0x04001F68 RID: 8040
		protected string AttributeName;
	}
}
