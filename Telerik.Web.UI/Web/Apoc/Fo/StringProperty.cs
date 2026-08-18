using System;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x02001452 RID: 5202
	internal class StringProperty : Property
	{
		// Token: 0x0600D3DC RID: 54236 RVA: 0x002F0860 File Offset: 0x002EEA60
		public StringProperty(string str)
		{
			this.str = str;
		}

		// Token: 0x0600D3DD RID: 54237 RVA: 0x002F086F File Offset: 0x002EEA6F
		public override object GetObject()
		{
			return this.str;
		}

		// Token: 0x0600D3DE RID: 54238 RVA: 0x002F0877 File Offset: 0x002EEA77
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x04003987 RID: 14727
		private string str;

		// Token: 0x02001453 RID: 5203
		internal class Maker : PropertyMaker
		{
			// Token: 0x0600D3DF RID: 54239 RVA: 0x002F087F File Offset: 0x002EEA7F
			public Maker(string propName) : base(propName)
			{
			}

			// Token: 0x0600D3E0 RID: 54240 RVA: 0x002F0888 File Offset: 0x002EEA88
			public override Property Make(PropertyList propertyList, string value, FObj fo)
			{
				int num = value.Length - 1;
				if (num > 0)
				{
					char c = value[0];
					if (c == '"' || c == '\'')
					{
						if (value[num] == c)
						{
							return new StringProperty(value.Substring(1, num - 2));
						}
						Console.WriteLine("Warning String-valued property starts with quote but doesn't end with quote: " + value);
					}
				}
				return new StringProperty(value);
			}
		}
	}
}
