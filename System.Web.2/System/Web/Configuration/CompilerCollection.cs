using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020006C5 RID: 1733
	[ConfigurationCollection(typeof(Compiler), AddItemName = "compiler", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class CompilerCollection : ConfigurationElementCollection
	{
		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x060053B6 RID: 21430 RVA: 0x001265A8 File Offset: 0x001247A8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilerCollection._properties;
			}
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x001240D1 File Offset: 0x001222D1
		public CompilerCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x060053B8 RID: 21432 RVA: 0x00124AED File Offset: 0x00122CED
		public string[] AllKeys
		{
			get
			{
				return StringUtil.ObjectArrayToStringArray(base.BaseGetAllKeys());
			}
		}

		// Token: 0x170017E4 RID: 6116
		public Compiler this[string language]
		{
			get
			{
				return (Compiler)base.BaseGet(language);
			}
		}

		// Token: 0x170017E5 RID: 6117
		public Compiler this[int index]
		{
			get
			{
				return (Compiler)base.BaseGet(index);
			}
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x001265CB File Offset: 0x001247CB
		protected override ConfigurationElement CreateNewElement()
		{
			return new Compiler();
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x001265D2 File Offset: 0x001247D2
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((Compiler)element).Language;
		}

		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x060053BD RID: 21437 RVA: 0x001265DF File Offset: 0x001247DF
		protected override string ElementName
		{
			get
			{
				return "compiler";
			}
		}

		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x060053BE RID: 21438 RVA: 0x00007722 File Offset: 0x00005922
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x001265BD File Offset: 0x001247BD
		public Compiler Get(int index)
		{
			return (Compiler)base.BaseGet(index);
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x001265AF File Offset: 0x001247AF
		public Compiler Get(string language)
		{
			return (Compiler)base.BaseGet(language);
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x00124AFA File Offset: 0x00122CFA
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x04002C15 RID: 11285
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();
	}
}
