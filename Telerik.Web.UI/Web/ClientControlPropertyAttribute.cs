using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web
{
	// Token: 0x02000F61 RID: 3937
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ClientControlPropertyAttribute : Attribute
	{
		// Token: 0x06009610 RID: 38416 RVA: 0x0021875E File Offset: 0x0021695E
		public ClientControlPropertyAttribute() : this(true)
		{
		}

		// Token: 0x06009611 RID: 38417 RVA: 0x00218767 File Offset: 0x00216967
		public ClientControlPropertyAttribute(bool isScriptProperty)
		{
			this._isScriptProperty = isScriptProperty;
		}

		// Token: 0x17002F67 RID: 12135
		// (get) Token: 0x06009612 RID: 38418 RVA: 0x00218776 File Offset: 0x00216976
		public bool IsScriptProperty
		{
			get
			{
				return this._isScriptProperty;
			}
		}

		// Token: 0x06009613 RID: 38419 RVA: 0x00218780 File Offset: 0x00216980
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(obj, this))
			{
				return true;
			}
			ClientControlPropertyAttribute clientControlPropertyAttribute = obj as ClientControlPropertyAttribute;
			return clientControlPropertyAttribute != null && clientControlPropertyAttribute._isScriptProperty == this._isScriptProperty;
		}

		// Token: 0x06009614 RID: 38420 RVA: 0x002187B4 File Offset: 0x002169B4
		public override int GetHashCode()
		{
			return this._isScriptProperty.GetHashCode();
		}

		// Token: 0x06009615 RID: 38421 RVA: 0x002187CF File Offset: 0x002169CF
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ClientControlPropertyAttribute.Default);
		}

		// Token: 0x04002AE7 RID: 10983
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Exposing this for user convenience")]
		private static readonly ClientControlPropertyAttribute Yes = new ClientControlPropertyAttribute(true);

		// Token: 0x04002AE8 RID: 10984
		private static readonly ClientControlPropertyAttribute No = new ClientControlPropertyAttribute(false);

		// Token: 0x04002AE9 RID: 10985
		private static readonly ClientControlPropertyAttribute Default = ClientControlPropertyAttribute.No;

		// Token: 0x04002AEA RID: 10986
		private readonly bool _isScriptProperty;
	}
}
