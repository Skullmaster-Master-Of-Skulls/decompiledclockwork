using System;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor
{
	// Token: 0x02000051 RID: 81
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public sealed class RazorDirectiveAttribute : Attribute
	{
		// Token: 0x060003BC RID: 956 RVA: 0x00010A90 File Offset: 0x0000EC90
		public RazorDirectiveAttribute(string name, string value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00010AC9 File Offset: 0x0000ECC9
		public override object TypeId
		{
			get
			{
				return this._typeId;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00010AD1 File Offset: 0x0000ECD1
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00010AD9 File Offset: 0x0000ECD9
		public string Name { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00010AE2 File Offset: 0x0000ECE2
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00010AEA File Offset: 0x0000ECEA
		public string Value { get; private set; }

		// Token: 0x060003C2 RID: 962 RVA: 0x00010AF4 File Offset: 0x0000ECF4
		public override bool Equals(object obj)
		{
			RazorDirectiveAttribute razorDirectiveAttribute = obj as RazorDirectiveAttribute;
			return razorDirectiveAttribute != null && this.Name.Equals(razorDirectiveAttribute.Name, StringComparison.OrdinalIgnoreCase) && StringComparer.OrdinalIgnoreCase.Equals(this.Value, razorDirectiveAttribute.Value);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00010B37 File Offset: 0x0000ED37
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this.Name) * 31 + ((this.Value == null) ? 0 : StringComparer.OrdinalIgnoreCase.GetHashCode(this.Value));
		}

		// Token: 0x04000103 RID: 259
		private readonly object _typeId = new object();
	}
}
