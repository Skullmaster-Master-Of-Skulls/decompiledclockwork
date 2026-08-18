using System;
using System.Globalization;
using System.Web.Mvc.ExpressionUtil;
using System.Web.Razor.Generator;

namespace System.Web.Mvc.Razor
{
	// Token: 0x02000096 RID: 150
	internal class SetModelTypeCodeGenerator : SetBaseTypeCodeGenerator
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0000C347 File Offset: 0x0000A547
		public SetModelTypeCodeGenerator(string modelType, string genericTypeFormat) : base(modelType)
		{
			this._genericTypeFormat = genericTypeFormat;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000C358 File Offset: 0x0000A558
		protected override string ResolveType(CodeGeneratorContext context, string baseType)
		{
			return string.Format(CultureInfo.InvariantCulture, this._genericTypeFormat, new object[]
			{
				context.Host.DefaultBaseClass,
				baseType
			});
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000C390 File Offset: 0x0000A590
		public override bool Equals(object obj)
		{
			SetModelTypeCodeGenerator setModelTypeCodeGenerator = obj as SetModelTypeCodeGenerator;
			return setModelTypeCodeGenerator != null && base.Equals(obj) && string.Equals(this._genericTypeFormat, setModelTypeCodeGenerator._genericTypeFormat, StringComparison.Ordinal);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		public override int GetHashCode()
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			hashCodeCombiner.AddInt32(base.GetHashCode());
			hashCodeCombiner.AddObject(this._genericTypeFormat);
			return hashCodeCombiner.CombinedHash;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000C3F5 File Offset: 0x0000A5F5
		public override string ToString()
		{
			return "Model:" + base.BaseType;
		}

		// Token: 0x0400012A RID: 298
		private string _genericTypeFormat;
	}
}
