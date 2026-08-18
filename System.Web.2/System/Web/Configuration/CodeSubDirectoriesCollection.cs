using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006C1 RID: 1729
	[ConfigurationCollection(typeof(CodeSubDirectory), CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class CodeSubDirectoriesCollection : ConfigurationElementCollection
	{
		// Token: 0x06005353 RID: 21331 RVA: 0x001240D1 File Offset: 0x001222D1
		public CodeSubDirectoriesCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x170017B7 RID: 6071
		// (get) Token: 0x06005354 RID: 21332 RVA: 0x00124D88 File Offset: 0x00122F88
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CodeSubDirectoriesCollection._properties;
			}
		}

		// Token: 0x170017B8 RID: 6072
		public CodeSubDirectory this[int index]
		{
			get
			{
				return (CodeSubDirectory)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x00117E10 File Offset: 0x00116010
		public void Add(CodeSubDirectory codeSubDirectory)
		{
			this.BaseAdd(codeSubDirectory);
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x00117E3F File Offset: 0x0011603F
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x00117E19 File Offset: 0x00116019
		public void Remove(string directoryName)
		{
			base.BaseRemove(directoryName);
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x00117E22 File Offset: 0x00116022
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x0600535B RID: 21339 RVA: 0x00007722 File Offset: 0x00005922
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x00124D9D File Offset: 0x00122F9D
		protected override ConfigurationElement CreateNewElement()
		{
			return new CodeSubDirectory();
		}

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x0600535D RID: 21341 RVA: 0x00124DA4 File Offset: 0x00122FA4
		protected override string ElementName
		{
			get
			{
				return "add";
			}
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x00124DAB File Offset: 0x00122FAB
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CodeSubDirectory)element).DirectoryName;
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x00124DB8 File Offset: 0x00122FB8
		internal void EnsureRuntimeValidation()
		{
			if (this._didRuntimeValidation)
			{
				return;
			}
			foreach (object obj in this)
			{
				CodeSubDirectory codeSubDirectory = (CodeSubDirectory)obj;
				codeSubDirectory.DoRuntimeValidation();
			}
			this._didRuntimeValidation = true;
		}

		// Token: 0x04002BE2 RID: 11234
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04002BE3 RID: 11235
		private bool _didRuntimeValidation;
	}
}
