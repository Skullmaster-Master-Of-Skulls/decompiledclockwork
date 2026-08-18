using System;
using System.Collections.Generic;
using System.Globalization;

namespace iTextSharp.text.html.simpleparser
{
	// Token: 0x02000332 RID: 818
	public class ChainedProperties
	{
		// Token: 0x17000532 RID: 1330
		public string this[string key]
		{
			get
			{
				for (int i = this.chain.Count - 1; i >= 0; i--)
				{
					ChainedProperties.ChainedProperty chainedProperty = this.chain[i];
					Dictionary<string, string> property = chainedProperty.property;
					if (property.ContainsKey(key))
					{
						return property[key];
					}
				}
				return null;
			}
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x000B1A28 File Offset: 0x000B0A28
		public bool HasProperty(string key)
		{
			for (int i = this.chain.Count - 1; i >= 0; i--)
			{
				ChainedProperties.ChainedProperty chainedProperty = this.chain[i];
				Dictionary<string, string> property = chainedProperty.property;
				if (property.ContainsKey(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x000B1A70 File Offset: 0x000B0A70
		public void AddToChain(string key, Dictionary<string, string> prop)
		{
			string text;
			prop.TryGetValue("size", out text);
			if (text != null)
			{
				if (text.EndsWith("pt"))
				{
					prop["size"] = text.Substring(0, text.Length - 2);
				}
				else
				{
					int num = 0;
					if (text.StartsWith("+") || text.StartsWith("-"))
					{
						string text2 = this["basefontsize"];
						if (text2 == null)
						{
							text2 = "12";
						}
						float num2 = float.Parse(text2, NumberFormatInfo.InvariantInfo);
						int num3 = (int)num2;
						for (int i = ChainedProperties.fontSizes.Length - 1; i >= 0; i--)
						{
							if (num3 >= ChainedProperties.fontSizes[i])
							{
								num = i;
								break;
							}
						}
						int num4 = int.Parse(text.StartsWith("+") ? text.Substring(1) : text);
						num += num4;
					}
					else
					{
						try
						{
							num = int.Parse(text) - 1;
						}
						catch
						{
							num = 0;
						}
					}
					if (num < 0)
					{
						num = 0;
					}
					else if (num >= ChainedProperties.fontSizes.Length)
					{
						num = ChainedProperties.fontSizes.Length - 1;
					}
					prop["size"] = ChainedProperties.fontSizes[num].ToString();
				}
			}
			this.chain.Add(new ChainedProperties.ChainedProperty(key, prop));
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x000B1BB8 File Offset: 0x000B0BB8
		public void RemoveChain(string key)
		{
			for (int i = this.chain.Count - 1; i >= 0; i--)
			{
				if (key.Equals(this.chain[i].key))
				{
					this.chain.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x0400144F RID: 5199
		public static int[] fontSizes = new int[]
		{
			8,
			10,
			12,
			14,
			18,
			24,
			36
		};

		// Token: 0x04001450 RID: 5200
		public List<ChainedProperties.ChainedProperty> chain = new List<ChainedProperties.ChainedProperty>();

		// Token: 0x02000333 RID: 819
		public sealed class ChainedProperty
		{
			// Token: 0x06001D99 RID: 7577 RVA: 0x000B1C3C File Offset: 0x000B0C3C
			internal ChainedProperty(string key, Dictionary<string, string> property)
			{
				this.key = key;
				this.property = property;
			}

			// Token: 0x04001451 RID: 5201
			internal string key;

			// Token: 0x04001452 RID: 5202
			internal Dictionary<string, string> property;
		}
	}
}
