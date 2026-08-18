using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web
{
	// Token: 0x02000F59 RID: 3929
	[SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Justification = "The composition of baseType and resourceName is available as ResourcePath")]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	internal sealed class ClientCssResourceAttribute : Attribute
	{
		// Token: 0x060095EC RID: 38380 RVA: 0x0021845C File Offset: 0x0021665C
		public ClientCssResourceAttribute(Type baseType, string resourceName)
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
			this._resourcePath = text + '.' + resourceName;
		}

		// Token: 0x060095ED RID: 38381 RVA: 0x002184C2 File Offset: 0x002166C2
		public ClientCssResourceAttribute(string fullResourceName)
		{
			if (fullResourceName == null)
			{
				throw new ArgumentNullException("fullResourceName");
			}
			this._resourcePath = fullResourceName;
		}

		// Token: 0x17002F5F RID: 12127
		// (get) Token: 0x060095EE RID: 38382 RVA: 0x002184DF File Offset: 0x002166DF
		public string ResourcePath
		{
			get
			{
				return this._resourcePath;
			}
		}

		// Token: 0x17002F60 RID: 12128
		// (get) Token: 0x060095EF RID: 38383 RVA: 0x002184E7 File Offset: 0x002166E7
		// (set) Token: 0x060095F0 RID: 38384 RVA: 0x002184EF File Offset: 0x002166EF
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

		// Token: 0x04002AD9 RID: 10969
		private readonly string _resourcePath;

		// Token: 0x04002ADA RID: 10970
		private int _loadOrder;
	}
}
