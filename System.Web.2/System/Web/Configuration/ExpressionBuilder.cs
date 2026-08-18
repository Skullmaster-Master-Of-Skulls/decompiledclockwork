using System;
using System.Configuration;
using System.Web.Compilation;

namespace System.Web.Configuration
{
	// Token: 0x020006D8 RID: 1752
	public sealed class ExpressionBuilder : ConfigurationElement
	{
		// Token: 0x06005451 RID: 21585 RVA: 0x00127A78 File Offset: 0x00125C78
		static ExpressionBuilder()
		{
			ExpressionBuilder._properties = new ConfigurationPropertyCollection();
			ExpressionBuilder._properties.Add(ExpressionBuilder._propExpressionPrefix);
			ExpressionBuilder._properties.Add(ExpressionBuilder._propType);
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x00117E9E File Offset: 0x0011609E
		internal ExpressionBuilder()
		{
		}

		// Token: 0x06005453 RID: 21587 RVA: 0x00127AF0 File Offset: 0x00125CF0
		public ExpressionBuilder(string expressionPrefix, string theType)
		{
			this.ExpressionPrefix = expressionPrefix;
			this.Type = theType;
		}

		// Token: 0x17001806 RID: 6150
		// (get) Token: 0x06005454 RID: 21588 RVA: 0x00127B06 File Offset: 0x00125D06
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ExpressionBuilder._properties;
			}
		}

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x06005455 RID: 21589 RVA: 0x00127B0D File Offset: 0x00125D0D
		// (set) Token: 0x06005456 RID: 21590 RVA: 0x00127B1F File Offset: 0x00125D1F
		[ConfigurationProperty("expressionPrefix", IsRequired = true, IsKey = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string ExpressionPrefix
		{
			get
			{
				return (string)base[ExpressionBuilder._propExpressionPrefix];
			}
			set
			{
				base[ExpressionBuilder._propExpressionPrefix] = value;
			}
		}

		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x06005457 RID: 21591 RVA: 0x00127B2D File Offset: 0x00125D2D
		// (set) Token: 0x06005458 RID: 21592 RVA: 0x00127B3F File Offset: 0x00125D3F
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Type
		{
			get
			{
				return (string)base[ExpressionBuilder._propType];
			}
			set
			{
				base[ExpressionBuilder._propType] = value;
			}
		}

		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x06005459 RID: 21593 RVA: 0x00127B4D File Offset: 0x00125D4D
		internal Type TypeInternal
		{
			get
			{
				return CompilationUtil.LoadTypeWithChecks(this.Type, typeof(ExpressionBuilder), null, this, "type");
			}
		}

		// Token: 0x04002C49 RID: 11337
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002C4A RID: 11338
		private static readonly ConfigurationProperty _propExpressionPrefix = new ConfigurationProperty("expressionPrefix", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002C4B RID: 11339
		private static readonly ConfigurationProperty _propType = new ConfigurationProperty("type", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);
	}
}
