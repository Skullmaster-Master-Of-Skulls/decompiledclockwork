using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200005E RID: 94
	public abstract class PropertyOperation : BuildOperation
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00005FF2 File Offset: 0x000041F2
		protected PropertyOperation(Type typeBeingConstructed, string propertyName) : base(typeBeingConstructed)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00006002 File Offset: 0x00004202
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000600C File Offset: 0x0000420C
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, this.GetDescriptionFormat(), new object[]
			{
				base.TypeBeingConstructed.GetTypeInfo().Name,
				this.propertyName
			});
		}

		// Token: 0x060001A0 RID: 416
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This could theoretically be expensive, and is easier to override for clients if it's a method.")]
		protected abstract string GetDescriptionFormat();

		// Token: 0x04000061 RID: 97
		private readonly string propertyName;
	}
}
