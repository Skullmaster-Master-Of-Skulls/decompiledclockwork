using System;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace WebGrease.Configuration
{
	// Token: 0x020000F8 RID: 248
	public class PreprocessingConfig : INamedConfig
	{
		// Token: 0x06000FD2 RID: 4050 RVA: 0x000480CB File Offset: 0x000462CB
		public PreprocessingConfig()
		{
			this.PreprocessingEngines = new Collection<string>();
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x000480E0 File Offset: 0x000462E0
		public PreprocessingConfig(XElement element) : this()
		{
			this.Name = (((string)element.Attribute("config")) ?? string.Empty);
			string text;
			if ((text = (string)element.Element("Engines")) == null)
			{
				text = (((string)element.Attribute("Engines")) ?? ((string)element.Attribute("engines")));
			}
			string text2 = text;
			if (!string.IsNullOrWhiteSpace(text2))
			{
				foreach (string item in text2.Split(new char[]
				{
					';'
				}, StringSplitOptions.RemoveEmptyEntries))
				{
					this.Enabled = true;
					this.PreprocessingEngines.Add(item);
				}
			}
			this.Element = element;
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x000481AD File Offset: 0x000463AD
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x000481B5 File Offset: 0x000463B5
		public XElement Element { get; private set; }

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x000481BE File Offset: 0x000463BE
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x000481C6 File Offset: 0x000463C6
		public bool Enabled { get; private set; }

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x000481CF File Offset: 0x000463CF
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x000481D7 File Offset: 0x000463D7
		public Collection<string> PreprocessingEngines { get; private set; }

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x000481E0 File Offset: 0x000463E0
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x000481E8 File Offset: 0x000463E8
		public string Name { get; private set; }
	}
}
