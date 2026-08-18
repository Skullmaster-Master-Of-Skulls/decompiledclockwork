using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web
{
	// Token: 0x02000F5B RID: 3931
	[SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Justification = "The composition of baseType, resourceName, and fullResourceName is available as ResourcePath")]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class ClientScriptResourceAttribute : Attribute
	{
		// Token: 0x17002F62 RID: 12130
		// (get) Token: 0x060095F5 RID: 38389 RVA: 0x00218524 File Offset: 0x00216724
		// (set) Token: 0x060095F6 RID: 38390 RVA: 0x0021852C File Offset: 0x0021672C
		public string ComponentType
		{
			get
			{
				return this._componentType;
			}
			set
			{
				this._componentType = value;
			}
		}

		// Token: 0x17002F63 RID: 12131
		// (get) Token: 0x060095F7 RID: 38391 RVA: 0x00218535 File Offset: 0x00216735
		// (set) Token: 0x060095F8 RID: 38392 RVA: 0x0021853D File Offset: 0x0021673D
		public int LoadOrder
		{
			get
			{
				return this._loadOrder;
			}
			set
			{
				this._loadOrder = value;
			}
		}

		// Token: 0x17002F64 RID: 12132
		// (get) Token: 0x060095F9 RID: 38393 RVA: 0x00218546 File Offset: 0x00216746
		// (set) Token: 0x060095FA RID: 38394 RVA: 0x0021854E File Offset: 0x0021674E
		public string ResourcePath
		{
			get
			{
				return this._resourcePath;
			}
			set
			{
				this._resourcePath = value;
			}
		}

		// Token: 0x060095FB RID: 38395 RVA: 0x00218557 File Offset: 0x00216757
		public ClientScriptResourceAttribute()
		{
		}

		// Token: 0x060095FC RID: 38396 RVA: 0x0021855F File Offset: 0x0021675F
		public ClientScriptResourceAttribute(string componentType)
		{
			this._componentType = componentType;
		}

		// Token: 0x060095FD RID: 38397 RVA: 0x00218570 File Offset: 0x00216770
		public ClientScriptResourceAttribute(string componentType, Type baseType, string resourceName)
		{
			if (baseType == null)
			{
				throw new ArgumentNullException("baseType");
			}
			if (resourceName == null)
			{
				throw new ArgumentNullException("resourceName");
			}
			string text = baseType.FullName;
			int num = text.LastIndexOf('.');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			this.ResourcePath = text + "." + resourceName;
			this._componentType = componentType;
		}

		// Token: 0x060095FE RID: 38398 RVA: 0x002185DB File Offset: 0x002167DB
		public ClientScriptResourceAttribute(string componentType, string fullResourceName) : this(componentType)
		{
			if (fullResourceName == null)
			{
				throw new ArgumentNullException("fullResourceName");
			}
			this.ResourcePath = fullResourceName;
		}

		// Token: 0x060095FF RID: 38399 RVA: 0x002185F9 File Offset: 0x002167F9
		public override bool IsDefaultAttribute()
		{
			return this.ComponentType == null && this.ResourcePath == null;
		}

		// Token: 0x04002ADC RID: 10972
		private string _resourcePath;

		// Token: 0x04002ADD RID: 10973
		private string _componentType;

		// Token: 0x04002ADE RID: 10974
		private int _loadOrder;
	}
}
