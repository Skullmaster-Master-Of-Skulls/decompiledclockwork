using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web
{
	// Token: 0x02000F5F RID: 3935
	[AttributeUsage(AttributeTargets.Property, Inherited = true)]
	public sealed class ClientControlEventAttribute : Attribute
	{
		// Token: 0x06009602 RID: 38402 RVA: 0x0021861E File Offset: 0x0021681E
		public ClientControlEventAttribute() : this(true)
		{
		}

		// Token: 0x06009603 RID: 38403 RVA: 0x00218627 File Offset: 0x00216827
		public ClientControlEventAttribute(bool isScriptEvent)
		{
			this._isScriptEvent = isScriptEvent;
		}

		// Token: 0x17002F65 RID: 12133
		// (get) Token: 0x06009604 RID: 38404 RVA: 0x00218636 File Offset: 0x00216836
		public bool IsScriptEvent
		{
			get
			{
				return this._isScriptEvent;
			}
		}

		// Token: 0x06009605 RID: 38405 RVA: 0x00218640 File Offset: 0x00216840
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(obj, this))
			{
				return true;
			}
			ClientControlEventAttribute clientControlEventAttribute = obj as ClientControlEventAttribute;
			return clientControlEventAttribute != null && clientControlEventAttribute._isScriptEvent == this._isScriptEvent;
		}

		// Token: 0x06009606 RID: 38406 RVA: 0x00218674 File Offset: 0x00216874
		public override int GetHashCode()
		{
			return this._isScriptEvent.GetHashCode();
		}

		// Token: 0x06009607 RID: 38407 RVA: 0x0021868F File Offset: 0x0021688F
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ClientControlEventAttribute.Default);
		}

		// Token: 0x04002ADF RID: 10975
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Exposing this for user convenience")]
		private static readonly ClientControlEventAttribute Yes = new ClientControlEventAttribute(true);

		// Token: 0x04002AE0 RID: 10976
		private static readonly ClientControlEventAttribute No = new ClientControlEventAttribute(false);

		// Token: 0x04002AE1 RID: 10977
		private static readonly ClientControlEventAttribute Default = ClientControlEventAttribute.No;

		// Token: 0x04002AE2 RID: 10978
		private readonly bool _isScriptEvent;
	}
}
