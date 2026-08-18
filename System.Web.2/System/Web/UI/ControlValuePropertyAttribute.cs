using System;
using System.ComponentModel;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000267 RID: 615
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ControlValuePropertyAttribute : Attribute
	{
		// Token: 0x06001D42 RID: 7490 RVA: 0x0005F1F1 File Offset: 0x0005D3F1
		public ControlValuePropertyAttribute(string name)
		{
			this._name = name;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0005F200 File Offset: 0x0005D400
		public ControlValuePropertyAttribute(string name, object defaultValue)
		{
			this._name = name;
			this._defaultValue = defaultValue;
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0005F218 File Offset: 0x0005D418
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

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x0005F25C File Offset: 0x0005D45C
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001D46 RID: 7494 RVA: 0x0005F264 File Offset: 0x0005D464
		public object DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0005F26C File Offset: 0x0005D46C
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

		// Token: 0x06001D48 RID: 7496 RVA: 0x0005F2BC File Offset: 0x0005D4BC
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes((this.Name != null) ? this.Name.GetHashCode() : 0, (this.DefaultValue != null) ? this.DefaultValue.GetHashCode() : 0);
		}

		// Token: 0x04001951 RID: 6481
		private readonly string _name;

		// Token: 0x04001952 RID: 6482
		private readonly object _defaultValue;
	}
}
