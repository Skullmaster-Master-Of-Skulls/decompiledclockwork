using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x020000A3 RID: 163
	[XmlRoot(Namespace = null, ElementName = "Droplist")]
	[Serializable]
	public class DroplistDef : ColumnTypeDef, ICloneable
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00032260 File Offset: 0x00031260
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x000322E0 File Offset: 0x000312E0
		[XmlElement("selection")]
		public string[] Selections
		{
			get
			{
				Dictionary<string, string>.KeyCollection keys = this.__selections.Keys;
				int count = keys.Count;
				string[] array = new string[count];
				int num = 0;
				foreach (string text in keys)
				{
					array[num++] = text;
				}
				return array;
			}
			set
			{
				this.__selections.Clear();
				for (int i = 0; i < value.Length; i++)
				{
					string key = value[i];
					this.__selections.Add(key, null);
				}
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00032323 File Offset: 0x00031323
		public void Clear()
		{
			this.__selections.Clear();
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00032332 File Offset: 0x00031332
		public void Add(string item)
		{
			this.__selections.Add(item, null);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00032344 File Offset: 0x00031344
		public object Clone()
		{
			return new DroplistDef(this.__selections);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00032361 File Offset: 0x00031361
		public DroplistDef()
		{
			this.__selections = new Dictionary<string, string>();
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00032378 File Offset: 0x00031378
		private DroplistDef(Dictionary<string, string> selections)
		{
			this.__selections = new Dictionary<string, string>();
			foreach (string key in selections.Keys)
			{
				this.__selections.Add(key, null);
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x000323F0 File Offset: 0x000313F0
		public DroplistDef(IEnumerable<string> selections)
		{
			this.__selections = new Dictionary<string, string>();
			foreach (string key in selections)
			{
				this.__selections.Add(key, null);
			}
		}

		// Token: 0x040004EA RID: 1258
		private Dictionary<string, string> __selections;
	}
}
