using System;
using System.Configuration;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x0200019E RID: 414
	internal class InterceptorElement : ConfigurationElement
	{
		// Token: 0x06000E16 RID: 3606 RVA: 0x0003E701 File Offset: 0x0003C901
		public InterceptorElement(int key)
		{
			this.Key = key;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0003E710 File Offset: 0x0003C910
		// (set) Token: 0x06000E18 RID: 3608 RVA: 0x0003E718 File Offset: 0x0003C918
		internal int Key { get; private set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0003E721 File Offset: 0x0003C921
		// (set) Token: 0x06000E1A RID: 3610 RVA: 0x0003E733 File Offset: 0x0003C933
		[ConfigurationProperty("type", IsRequired = true)]
		public virtual string TypeName
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0003E741 File Offset: 0x0003C941
		[ConfigurationProperty("parameters")]
		public virtual ParameterCollection Parameters
		{
			get
			{
				return (ParameterCollection)base["parameters"];
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0003E754 File Offset: 0x0003C954
		public virtual IDbInterceptor CreateInterceptor()
		{
			object obj;
			try
			{
				obj = Activator.CreateInstance(Type.GetType(this.TypeName, true), this.Parameters.GetTypedParameterValues());
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException(Strings.InterceptorTypeNotFound(this.TypeName), innerException);
			}
			IDbInterceptor dbInterceptor = obj as IDbInterceptor;
			if (dbInterceptor == null)
			{
				throw new InvalidOperationException(Strings.InterceptorTypeNotInterceptor(this.TypeName));
			}
			return dbInterceptor;
		}

		// Token: 0x040003C1 RID: 961
		private const string TypeKey = "type";

		// Token: 0x040003C2 RID: 962
		private const string ParametersKey = "parameters";
	}
}
