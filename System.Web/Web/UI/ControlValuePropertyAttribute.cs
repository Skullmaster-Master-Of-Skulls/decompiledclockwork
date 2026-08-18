using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020003C9 RID: 969
	[AttributeUsage(AttributeTargets.Class)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ControlValuePropertyAttribute : Attribute
	{
		// Token: 0x06002F4D RID: 12109 RVA: 0x000D2E08 File Offset: 0x000D1E08
		public ControlValuePropertyAttribute(string name)
		{
			this._name = name;
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x000D2E17 File Offset: 0x000D1E17
		public ControlValuePropertyAttribute(string name, object defaultValue)
		{
			this._name = name;
			this._defaultValue = defaultValue;
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000D2E30 File Offset: 0x000D1E30
		public ControlValuePropertyAttribute(string name, Type type, string defaultValue)
		{
			this._name = name;
			try
			{
				this._defaultValue = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(defaultValue);
			}
			catch
			{
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x000D2E74 File Offset: 0x000D1E74
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06002F51 RID: 12113 RVA: 0x000D2E7C File Offset: 0x000D1E7C
		public object DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000D2E84 File Offset: 0x000D1E84
		public override bool Equals(object obj)
		{
			ControlValuePropertyAttribute controlValuePropertyAttribute = obj as ControlValuePropertyAttribute;
			if (controlValuePropertyAttribute == null || !string.Equals(this._name, controlValuePropertyAttribute.Name, StringComparison.Ordinal))
			{
				return false;
			}
			if (this._defaultValue != null)
			{
				return this._defaultValue.Equals(controlValuePropertyAttribute.DefaultValue);
			}
			return controlValuePropertyAttribute.DefaultValue == null;
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000D2ED4 File Offset: 0x000D1ED4
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes((this.Name != null) ? this.Name.GetHashCode() : 0, (this.DefaultValue != null) ? this.DefaultValue.GetHashCode() : 0);
		}

		// Token: 0x040021D9 RID: 8665
		private readonly string _name;

		// Token: 0x040021DA RID: 8666
		private readonly object _defaultValue;
	}
}
